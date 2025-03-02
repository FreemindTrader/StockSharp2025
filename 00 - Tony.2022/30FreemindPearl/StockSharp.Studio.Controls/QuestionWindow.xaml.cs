// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.QuestionWindow
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.Localization;
using StockSharp.Logging;
using System;
using System.IO;
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
        private readonly Brush _negativeForeground = new SolidColorBrush( Colors.Red );
        private bool _wasPos = true;
        private readonly Brush _positiveForeground;
        private const int _maxTextLen = 1000;
        

        public string MessagePrompt { get; set; } = LocalizedStrings.DescribeTheQuestionInDetails;

        public string AttachPath
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

        public string Caption
        {
            get
            {
                return TitleCtrl.Text;
            }
            set
            {
                TitleCtrl.Text = value;
            }
        }

        public string Text
        {
            get
            {
                return TextCtrl.Text.Trim();
            }
            set
            {
                TextCtrl.Text = value;
                TextCtrl.CaretIndex = Text.Length;
            }
        }

        public string TextInfo
        {
            get
            {
                return TextInfoCtrl.Text;
            }
            set
            {
                TextInfoCtrl.Text = value;
            }
        }

        public QuestionWindow()
        {
            InitializeComponent();
            _positiveForeground = LeftToEnter.Foreground;
            LeftToEnter.Text = 1000.To<string>();
        }

        private void OkCommand_OnCanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = TextCtrl != null && Text.Length >= 20 && GetLeftToEnter() >= 0;
        }

        public event Func<string, byte[ ], Action<long>, CancellationToken, Task<Web.DomainModel.File>> FileProcessing;

        private void OkCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            string attachPath = AttachPath;
            if ( !attachPath.IsEmpty() )
            {
                Uri result;
                int num = !Uri.TryCreate( attachPath, UriKind.Absolute, out result ) ? 0 : ( result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps ? 1 : ( result.Scheme == Uri.UriSchemeFtp ? 1 : 0 ) );
                Func<string, byte[ ], Action<long>, CancellationToken, Task<Web.DomainModel.File>> fileProcessing = FileProcessing;
                if ( num != 0 )
                    Text = Text + Environment.NewLine + Environment.NewLine + "[i]" + LocalizedStrings.Str221 + ": " + attachPath + "[/i]";
                else if ( fileProcessing != null )
                {
                    if ( File.Exists( attachPath ) )
                    {
                        try
                        {
                            FileProgressWindow wnd = new FileProgressWindow();
                            wnd.File = new ValueTuple<string, byte[ ], long>( Path.GetFileName( attachPath ), File.ReadAllBytes( attachPath ), 0L );
                            wnd.FileProcessing += fileProcessing;
                            if ( !wnd.ShowModal( this ) )
                                return;
                        }
                        catch ( Exception ex )
                        {
                            string format = "Uploading " + attachPath + " file error: {0}";
                            ex.LogError( format );
                        }
                    }
                }
            }
            DialogResult = new bool?( true );
        }

        private void TextCtrl_OnTextChanged( object sender, TextChangedEventArgs e )
        {
            int leftToEnter = GetLeftToEnter();
            LeftToEnter.Text = string.Format( "{0}", leftToEnter );
            bool flag = leftToEnter >= 0;
            if ( _wasPos == flag )
                return;
            LeftToEnter.Foreground = flag ? _positiveForeground : _negativeForeground;
            _wasPos = flag;
        }

        private int GetLeftToEnter()
        {
            return 1000 - Text.Length;
        }


    }
}
