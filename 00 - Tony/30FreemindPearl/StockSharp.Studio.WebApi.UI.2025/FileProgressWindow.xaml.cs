// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.FileProgressWindow
// Assembly: StockSharp.Studio.WebApi.UI, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A13266F-BFDB-4F15-BB1A-891982F4135B
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.WebApi.UI.dll

using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.Logging;
using Ecng.Xaml;
using StockSharp.Localization;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace StockSharp.Studio.Controls
{
    public partial class FileProgressWindow : ThemedWindow
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private bool _isProcessing;

        public FileProgressWindow()
        {
            this.InitializeComponent();
        }

        public (string name, byte [ ] body, long id) File { get; set; }

        public event Func<string, byte[], Action<long>, CancellationToken, Task<StockSharp.Web.DomainModel.File>> FileProcessing;

        private void UpdateProgressBar( long current )
        {
            int total = this.File.Item2.Length;
            long per = current * 100L / (long) total;
            ( ( DispatcherObject ) this ).GuiSync<string>( ( Func<string> ) ( () =>
            {
                TextBlock progressText = this.ProgressText;
                DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 3);
                interpolatedStringHandler.AppendFormatted<long>( current );
                interpolatedStringHandler.AppendLiteral( "/" );
                interpolatedStringHandler.AppendFormatted<int>( total );
                interpolatedStringHandler.AppendLiteral( " (" );
                interpolatedStringHandler.AppendFormatted<long>( per );
                interpolatedStringHandler.AppendLiteral( "%)" );
                string stringAndClear;
                string str = stringAndClear = interpolatedStringHandler.ToStringAndClear();
                progressText.Text = stringAndClear;
                return str;
            } ) );
        }

        private void Cancel_OnClick( object sender, RoutedEventArgs e )
        {
            this.TryCancel();
        }

        private void TryCancel()
        {
            if ( new MessageBoxBuilder().Caption( this.Title ).Question().Text( LocalizedStrings.CancelOperationQuestion ).YesNo().Owner( ( Window ) this ).Show() != MessageBoxResult.Yes )
                return;
            this._cts.Cancel();
        }

        private void FileProgressWindow_OnClosing( object sender, CancelEventArgs e )
        {
            e.Cancel = this._isProcessing;
            if ( !this._isProcessing )
                return;
            this.TryCancel();
        }

        private void FileProgressWindow_OnLoaded( object sender, RoutedEventArgs e )
        {
            ValueTuple<string, byte[], long> file = this.File;
            Func<string, byte[], Action<long>, CancellationToken, Task<StockSharp.Web.DomainModel.File>> evt = this.FileProcessing;
            ValueTuple<string, byte[], long> valueTuple = file;
            string str = valueTuple.Item1;
            byte[] numArray = valueTuple.Item2;
            long num = valueTuple.Item3;
            if ( str == ( string ) null && numArray == null && num == 0L || evt == null )
                return;
            this.Progress.Value = 0.0;
            TextBlock progressText = this.ProgressText;
            DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 1);
            interpolatedStringHandler.AppendLiteral( "0/" );
            interpolatedStringHandler.AppendFormatted<int>( file.Item2.Length );
            string stringAndClear = interpolatedStringHandler.ToStringAndClear();
            progressText.Text = stringAndClear;
            this.ProcessAction( ( Func<CancellationToken, Task<StockSharp.Web.DomainModel.File>> ) ( token => evt( file.Item1, file.Item2, new Action<long>( this.UpdateProgressBar ), token ) ) );
        }

        private void ProcessAction( Func<CancellationToken, Task<StockSharp.Web.DomainModel.File>> action )
        {
            if ( action == null )
                throw new ArgumentNullException( nameof( action ) );
            this._isProcessing = true;

            ThreadingHelper.Launch( ThreadingHelper.Thread( ( Action ) ( async () =>
            {
                CancellationToken token;
                try
                {
                    Exception error = (Exception) null;
                    token = this._cts.Token;
                    try
                    {
                        StockSharp.Web.DomainModel.File result = await action( token );

                        var file = this.File;
                        file.Item3 = result.Id;
                        this.File = file;
                    }
                    catch ( Exception ex )
                    {
                        if ( !token.IsCancellationRequested )
                        {
                            LoggingHelper.LogError( ex, ( string ) null );
                            error = ex;
                        }
                    }
                    this._isProcessing = false;
                    this.GuiSync( () =>
                    {
                        if ( error != null )
                        {
                            new MessageBoxBuilder().Caption( this.Title ).Error().Text( error.Message ).Owner( ( Window ) this ).Show();
                        }
                        this.DialogResult = ( error == null && !this._cts.IsCancellationRequested );
                    } );
                }
                catch ( Exception ex )
                {

                }


            } ) ) );
        }


    }
}
