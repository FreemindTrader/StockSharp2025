// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.QuestionWindow
// Assembly: StockSharp.Studio.WebApi.UI, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A13266F-BFDB-4F15-BB1A-891982F4135B
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.WebApi.UI.dll

using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.Logging;
using Ecng.Xaml;
using StockSharp.Localization;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace StockSharp.Studio.Controls
{
    public partial class QuestionWindow : ThemedWindow, IComponentConnector
    {
        public static readonly RoutedCommand OkCommand = new RoutedCommand();
        private readonly Brush _negativeForeground = (Brush) new SolidColorBrush(Colors.Red);
        private bool _wasPos = true;
        private readonly Brush _positiveForeground;
        private const int _maxTextLen = 1000;
        
        public string MessagePrompt { get; set; } = LocalizedStrings.DescribeTheQuestionInDetails;

        public string AttachPath
        {
            get
            {
                return this.LinkTxt.Text;
            }
            set
            {
                this.LinkTxt.Text = value;
            }
        }

        public string Caption
        {
            get
            {
                return this.TitleCtrl.Text;
            }
            set
            {
                this.TitleCtrl.Text = value;
            }
        }

        public string Text
        {
            get
            {
                return this.TextCtrl.Text.Trim();
            }
            set
            {
                this.TextCtrl.Text = value;
                this.TextCtrl.CaretIndex = this.Text.Length;
            }
        }

        public QuestionWindow()
        {
            this.InitializeComponent();
            this._positiveForeground = this.LeftToEnter.Foreground;
            this.LeftToEnter.Text = ( string ) Converter.To<string>( ( object ) 1000 );
        }

        private void OkCommand_OnCanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = this.TextCtrl != null && this.Text.Length >= 20 && this.GetLeftToEnter() >= 0;
        }

        public event Func<string, byte[], Action<long>, CancellationToken, Task<StockSharp.Web.DomainModel.File>> FileProcessing;

        private void OkCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            string attachPath = this.AttachPath;
            if ( !StringHelper.IsEmpty( attachPath ) )
            {
                Uri result;
                int num = !Uri.TryCreate(attachPath, UriKind.Absolute, out result) ? 0 : (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps ? 1 : (result.Scheme == Uri.UriSchemeFtp ? 1 : 0));
                Func<string, byte[], Action<long>, CancellationToken, Task<StockSharp.Web.DomainModel.File>> fileProcessing = this.FileProcessing;
                if ( num != 0 )
                {
                    string text = this.Text;
                    string newLine1 = Environment.NewLine;
                    string newLine2 = Environment.NewLine;
                    DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 2);
                    interpolatedStringHandler.AppendLiteral( "[i]" );
                    interpolatedStringHandler.AppendFormatted( LocalizedStrings.Link );
                    interpolatedStringHandler.AppendLiteral( ": " );
                    interpolatedStringHandler.AppendFormatted( attachPath );
                    interpolatedStringHandler.AppendLiteral( "[/i]" );
                    string stringAndClear = interpolatedStringHandler.ToStringAndClear();
                    this.Text = text + newLine1 + newLine2 + stringAndClear;
                }
                else if ( fileProcessing != null )
                {
                    if ( System.IO.File.Exists( attachPath ) )
                    {
                        try
                        {
                            FileProgressWindow wnd = new FileProgressWindow();
                            wnd.File = new ValueTuple<string, byte [ ], long>( Path.GetFileName( attachPath ), System.IO.File.ReadAllBytes( attachPath ), 0L );
                            wnd.FileProcessing += fileProcessing;
                            if ( !wnd.ShowModal( ( Window ) this ) )
                                return;
                        }
                        catch ( Exception ex )
                        {
                            string str = "Uploading " + attachPath + " file error: {0}";
                            LoggingHelper.LogError( ex, str );
                        }
                    }
                }
            }
            this.DialogResult = new bool?( true );
        }

        private void TextCtrl_OnTextChanged( object sender, TextChangedEventArgs e )
        {
            int leftToEnter1 = this.GetLeftToEnter();
            TextBlock leftToEnter2 = this.LeftToEnter;
            DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
            interpolatedStringHandler.AppendFormatted<int>( leftToEnter1 );
            string stringAndClear = interpolatedStringHandler.ToStringAndClear();
            leftToEnter2.Text = stringAndClear;
            bool flag = leftToEnter1 >= 0;
            if ( this._wasPos == flag )
                return;
            this.LeftToEnter.Foreground = flag ? this._positiveForeground : this._negativeForeground;
            this._wasPos = flag;
        }

        private int GetLeftToEnter()
        {
            return 1000 - this.Text.Length;
        }

        
    }
}
