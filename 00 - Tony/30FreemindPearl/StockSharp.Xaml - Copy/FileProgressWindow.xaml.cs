using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.Community;
using StockSharp.Localization;
using StockSharp.Logging;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Threading;

namespace StockSharp.Xaml
{
    public partial class FileProgressWindow : DXWindow
    {
        private readonly long _fileId;
        private readonly string _filePath;
        
        private bool _shouldCancel;

        private bool _isRunning;

        public FileProgressWindow( long fileId )
        {
            
            this._fileId = fileId;
            this.InitializeComponent();
        }

        public FileProgressWindow( string path )
        {                        
            if ( path == null )
                throw new ArgumentNullException( nameof( path ) );
            this._filePath = path;
            this.InitializeComponent();
        }

        public FileData File { get; set; }

        private void DownloadByFileId( long fileId )
        {
            using ( FileClient fileClient = new FileClient() )
            {
                File = fileClient.GetFileInfo( fileId );
            }
                
            this.Progress.Value = 0.0;
            this.ProgressText.Text = string.Format( "0/{0}", this.File.BodyLength );

            this._shouldCancel = false;
            this._isRunning = true;

            ThreadingHelper.Launch( ThreadingHelper.Thread( DownloadFileThread ) );
        }

        private void DownloadFileThread( )
        {
            bool hasException = false;

            Exception downloadException = null;

            try
            {
                using ( FileClient fileClient = new FileClient() )
                {
                    fileClient.Download( this.File, x =>
                    {
                        Action tobeDone = delegate()
                        {
                            var fileBodyLength = this.File.BodyLength;

                            var percentage = x * 100 / fileBodyLength;

                            ProgressText.Text = string.Format("{0}/{1} ({2}%)", x, fileBodyLength, percentage );
                        };
                        this.GuiSync( tobeDone );
                    },

                    ( ) => { return _shouldCancel; } );
                }
            }
            catch( Exception ex )
            {
                LoggingHelper.LogError( ex, null );

                downloadException = ex;

                hasException = true;
            }

            _isRunning = false;

            if ( hasException )
            {
                this.GuiSync( () => new MessageBoxBuilder().Caption( Title ).Error().Text( downloadException.Message ).Owner( this ).Show() );
            }
            
        }

        private void FileProgressWindowLoaded( object sender, RoutedEventArgs e )
        {
            if ( !_filePath.IsEmpty( ) )
                this.UploadByFilePath( this._filePath );
            else
                this.DownloadByFileId( this._fileId );
        }

        private void UploadByFilePath( string filePath )
        {
            if ( filePath.IsEmpty() )
                throw new ArgumentNullException( "path" );

            var uploadFile           = new UploadFileInfo( );

            uploadFile._parentWindow = this;
            uploadFile._fileByte     = System.IO.File.ReadAllBytes( filePath ); 
            uploadFile._fileName     = Path.GetFileName( filePath );
            
            Progress.Value         = 0.0;
            ProgressText.Text      = string.Format( "0/{0}", uploadFile._fileByte.Length );

            this._shouldCancel     = false;
            this._isRunning        = true;

            ThreadingHelper.Launch( ThreadingHelper.Thread( uploadFile.Run ) );
        }

        private sealed class UploadFileInfo
        {
            public string _fileName;
            public byte[] _fileByte;
            public FileProgressWindow _parentWindow;            
            
            internal void Run()
            {
                bool hasException = false;

                Exception downloadException = null;

                try
                {
                    using ( FileClient fileClient = new FileClient() )
                    {
                        //fileClient.Download( this.File, 
                        

                        FileData data = fileClient.Upload(  this._fileName, 
                                                            this._fileByte, 
                                                            false,
                                                            x =>
                                                            {
                                                                Action tobeDone = delegate()
                                                                {
                                                                    var fileBodyLength = _fileByte.Length;

                                                                    var percentage = x * 100 / fileBodyLength;

                                                                    _parentWindow.ProgressText.Text = string.Format("{0}/{1} ({2}%)", x, fileBodyLength, percentage );
                                                                };
                                                                _parentWindow.GuiSync( tobeDone );
                                                            }, 
                                                            () => _parentWindow._shouldCancel
                                                            );
                        if ( data != null )
                        {
                            _parentWindow.File = data;
                        }                            
                    }
                }
                catch ( Exception ex )
                {
                    LoggingHelper.LogError( ex, null );

                    downloadException = ex;

                    hasException = true;
                }

                _parentWindow._isRunning = false;

                if ( hasException )
                {
                    _parentWindow.GuiSync( ( ) => new MessageBoxBuilder().Caption( _parentWindow.Title ).Error().Text( downloadException.Message ).Owner( _parentWindow ).Show() );
                }
            }
        }

        
        private void FileProgressWindowClosing( object sender, CancelEventArgs e )
        {
            e.Cancel = this._isRunning;
            if ( !this._isRunning )
                return;

            AskShouldCancel( );
        }

        private void Button_Click( object sender, RoutedEventArgs e )
        {
            AskShouldCancel( );
        }

        private void AskShouldCancel()
        {            
            if ( new MessageBoxBuilder().Caption( Title ).Question().Text( LocalizedStrings.CancelOperationQuestion ).YesNo().Owner( this ).Show() != MessageBoxResult.Yes )
                return;

            this._shouldCancel = true;
        }
    }
}
