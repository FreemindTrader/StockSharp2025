// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.QuestionWindow
// Assembly: StockSharp.Studio.WebApi.UI, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5447883D-4177-4B33-881A-41D29A366FB7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.WebApi.UI.dll

using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.Logging;
using StockSharp.Localization;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Studio.Controls;

public partial class QuestionWindow : ThemedWindow, IComponentConnector
{
  public static readonly RoutedCommand OkCommand = new RoutedCommand();
  private readonly Brush _positiveForeground;
  private readonly Brush _negativeForeground = (Brush) new SolidColorBrush(Colors.Red);
  private const int _maxTextLen = 1000;
  private bool _wasPos = true;
  internal TextBox TitleCtrl;
  internal TextBlock LeftToEnter;
  internal TextBox TextCtrl;
  internal TextBox LinkTxt;
  private bool _contentLoaded;

  public string MessagePrompt { get; init; } = LocalizedStrings.DescribeTheQuestionInDetails;

  public string AttachPath
  {
    get => this.LinkTxt.Text;
    set => this.LinkTxt.Text = value;
  }

  public string Caption
  {
    get => this.TitleCtrl.Text;
    set => this.TitleCtrl.Text = value;
  }

  public string Text
  {
    get => this.TextCtrl.Text.Trim();
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
    this.LeftToEnter.Text = Converter.To<string>((object) 1000);
  }

  private void OkCommand_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
  {
    e.CanExecute = this.TextCtrl != null && this.Text.Length >= 20 && this.GetLeftToEnter() >= 0;
  }

  public event Func<string, byte[], Action<long>, CancellationToken, Task<StockSharp.Web.DomainModel.File>> FileProcessing;

  private void OkCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
  {
    string attachPath = this.AttachPath;
    if (!StringHelper.IsEmpty(attachPath))
    {
      Uri result;
      int num = !Uri.TryCreate(attachPath, UriKind.Absolute, out result) ? 0 : (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps ? 1 : (result.Scheme == Uri.UriSchemeFtp ? 1 : 0));
      Func<string, byte[], Action<long>, CancellationToken, Task<StockSharp.Web.DomainModel.File>> fileProcessing = this.FileProcessing;
      if (num != 0)
        this.Text = this.Text + Environment.NewLine + Environment.NewLine + $"[i]{LocalizedStrings.Link}: {attachPath}[/i]";
      else if (fileProcessing != null)
      {
        if (System.IO.File.Exists(attachPath))
        {
          try
          {
            FileProgressWindow wnd = new FileProgressWindow();
            wnd.File = (Path.GetFileName(attachPath), System.IO.File.ReadAllBytes(attachPath), 0L);
            wnd.FileProcessing += fileProcessing;
            if (!wnd.ShowModal((Window) this))
              return;
          }
          catch (Exception ex)
          {
            string str = $"Uploading {attachPath} file error: {{0}}";
            LoggingHelper.LogError(ex, str);
          }
        }
      }
    }
    this.DialogResult = new bool?(true);
  }

  private void TextCtrl_OnTextChanged(object sender, TextChangedEventArgs e)
  {
    int leftToEnter = this.GetLeftToEnter();
    this.LeftToEnter.Text = $"{leftToEnter}";
    bool flag = leftToEnter >= 0;
    if (this._wasPos == flag)
      return;
    this.LeftToEnter.Foreground = flag ? this._positiveForeground : this._negativeForeground;
    this._wasPos = flag;
  }

  private int GetLeftToEnter() => 1000 - this.Text.Length;

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/StockSharp.Studio.WebApi.UI;V5.0.0;component/questionwindow.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    switch (connectionId)
    {
      case 1:
        ((CommandBinding) target).CanExecute += new CanExecuteRoutedEventHandler(this.OkCommand_OnCanExecute);
        ((CommandBinding) target).Executed += new ExecutedRoutedEventHandler(this.OkCommand_OnExecuted);
        break;
      case 2:
        this.TitleCtrl = (TextBox) target;
        break;
      case 3:
        this.LeftToEnter = (TextBlock) target;
        break;
      case 4:
        this.TextCtrl = (TextBox) target;
        this.TextCtrl.TextChanged += new TextChangedEventHandler(this.TextCtrl_OnTextChanged);
        break;
      case 5:
        this.LinkTxt = (TextBox) target;
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
