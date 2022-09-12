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
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace StockSharp.Xaml
{
    public partial class QuestionWindow : DXWindow
    {
        public static readonly RoutedCommand OkCommand = new RoutedCommand();
        private NotificationClient _notificationClient;
        private readonly Brush _foregroundBrush;
        private readonly Brush _redBrush = new SolidColorBrush( Colors.Red);
        private bool _hasChange;             

        public QuestionWindow( )
        {
            InitializeComponent();
            _foregroundBrush = LeftToEnter.Foreground;
            LeftToEnter.Text = ( string ) Converter.To<string>(  1000 );
        }

        public NotificationClient NotificationClient
        {
            get
            {
                return _notificationClient;
            }
            set
            {
                _notificationClient = value;
            }
        }

        public string Link
        {
            get
            {
                return LinkTxt.Text;
            }
            set
            {
                LinkTxt.Text = value;
            }
        }
        

        public void Init( Products product, Version version, string caption = null, string text = null )
        {
            TitleTxt.Text = string.Format( "[{0}] {1}", product, caption  );
            TextTxt.Text = string.Format( "[i]{0}: {1}\r\n{2}: {3}\r\n{4}: {5}[/i]\r\n-----------\r\n{6}",  LocalizedStrings.Version,  version,  LocalizedStrings.XamlStr47,  Environment.Is64BitProcess,  LocalizedStrings.OSVersion,  Environment.OSVersion,  text );
            TextTxt.CaretIndex = TextTxt.Text.Length;
        }

        private void CanExecuteOkCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = TextTxt != null && !StringHelper.IsEmpty( TextTxt.Text )  && ( 1000 - TextTxt.Text.Length ) >= 0;
        }

        private void ExecuteOkCommand( object sender, ExecutedRoutedEventArgs e )
        {
            //string Link = Link;
            FileData fileData = null;
            Uri result;
            if ( ! Link.IsEmpty( ) && ( !Uri.TryCreate( Link, UriKind.Absolute, out result ) ? 0 : ( result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps ? 1 : ( result.Scheme == Uri.UriSchemeFtp ? 1 : 0 ) ) ) == 0 )
            {
                if ( File.Exists( Link ) )
                {
                    try
                    {
                        FileProgressWindow fileProgressWindow = new FileProgressWindow(Link);

                        if ( !fileProgressWindow.ShowModal( this ) )
                        {
                            return;
                        }

                        fileData = fileProgressWindow.File;
                        Link = string.Format( "https://stocksharp.{0}/file/{1}/",  LocalizedStrings.Domain,  fileData.Id );
                    }
                    catch ( Exception ex )
                    {
                        string msg = string.Format("Uploading {0} file error: {{0}}", Link);
                        LoggingHelper.LogError( ex, msg );
                        Link = null;
                    }
                }
            }
            //string str3 = TextTxt.Text;
            if ( !StringHelper.IsEmpty( Link ) && fileData == null )
            {
                TextTxt.Text = TextTxt.Text + Environment.NewLine + Environment.NewLine + string.Format( "[i]{0}: {1}[/i]",  LocalizedStrings.Str221,  Link );
            }

            try
            {
                NotificationClient notificationClient = NotificationClient;
                //string str2 = TitleTxt.Text; 
                
                FileData[] fileArray;

                if ( fileData != null )
                {
                    fileArray = new FileData[ 1 ] { fileData };
                }
                else
                {
                    fileArray = ArrayHelper.Empty<FileData>();
                }

                notificationClient.SendMessage( TitleTxt.Text, TextTxt.Text, ( FileData[ ] ) fileArray );
            }
            catch ( Exception ex )
            {                
                LoggingHelper.LogError( ex, null );
            }
            int num = (int) new MessageBoxBuilder().Text(LocalizedStrings.ThankYouForQuestion ).Owner((Window) this).Show();

            DialogResult = true;
        }

        private void textBox_1_TextChanged( object sender, TextChangedEventArgs e )
        {
            int num = ( 1000 - TextTxt.Text.Length );
            
            LeftToEnter.Text = string.Format( "{0}",  num );

            bool flag = num >= 0;

            if ( _hasChange == flag )
            {
                return;
            }

            LeftToEnter.Foreground = flag ? _foregroundBrush : _redBrush;
            _hasChange = flag;
        }

        
        
    }
}
