// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.FileProgressWindow
// Assembly: StockSharp.Studio.WebApi.UI, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5447883D-4177-4B33-881A-41D29A366FB7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.WebApi.UI.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Threading;
using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.Logging;
using Ecng.Xaml;
using StockSharp.Localization;

#nullable enable
namespace StockSharp.Studio.Controls;

public partial class FileProgressWindow : ThemedWindow, IComponentConnector
{
    private readonly
#nullable disable
    CancellationTokenSource _cts = new CancellationTokenSource();
    private bool _isProcessing;
    
    public FileProgressWindow() => this.InitializeComponent();

    public (string name, byte[] body, long id) File { get; set; }

    public event Func<string, byte[], Action<long>, CancellationToken, Task<StockSharp.Web.DomainModel.File>> FileProcessing;

    private void UpdateProgressBar(long current)
    {
        int total = this.File.body.Length;
        long per = current * 100L / (long)total;
        ((DispatcherObject)this).GuiSync<string>((Func<string>)(() => this.ProgressText.Text = $"{current}/{total} ({per}%)"));
    }

    private void Cancel_OnClick(object sender, RoutedEventArgs e) => this.TryCancel();

    private void TryCancel()
    {
        if (new MessageBoxBuilder().Caption(this.Title).Question().Text(LocalizedStrings.CancelOperationQuestion).YesNo().Owner((Window)this).Show() != MessageBoxResult.Yes)
            return;
        this._cts.Cancel();
    }

    private void FileProgressWindow_OnClosing(object sender, CancelEventArgs e)
    {
        e.Cancel = this._isProcessing;
        if (!this._isProcessing)
            return;
        this.TryCancel();
    }

    private void FileProgressWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        (string name, byte[] body, long id) file = this.File;
        Func<string, byte[], Action<long>, CancellationToken, Task<StockSharp.Web.DomainModel.File>> evt = this.FileProcessing;
        (string name, byte[] body, long id) = file;
        if (name == (string)null && body == null && id == 0L || evt == null)
            return;
        this.Progress.Value = 0.0;
        this.ProgressText.Text = $"0/{file.body.Length}";
        this.ProcessAction((Func<CancellationToken, Task<StockSharp.Web.DomainModel.File>>)(token => evt(file.name, file.body, new Action<long>(this.UpdateProgressBar), token)));
    }

    private void ProcessAction(Func<CancellationToken, Task<StockSharp.Web.DomainModel.File>> action)
    {
        if (action == null)
            throw new ArgumentNullException(nameof(action));
        this._isProcessing = true;
        ThreadingHelper.Launch(ThreadingHelper.Thread((Action)(async () =>
        {
            Exception error = (Exception)null;
            CancellationToken token = this._cts.Token;
            try
            {
                StockSharp.Web.DomainModel.File file = await action(token);
                this.File = this.File with { Item3 = file.Id };
            }
            catch (Exception ex)
            {
                if (!token.IsCancellationRequested)
                {
                    LoggingHelper.LogError(ex, (string)null);
                    error = ex;
                }
            }
            this._isProcessing = false;
            ((DispatcherObject)this).GuiSync((Action)(() =>
        {
            if (error != null)
            {
                int num = (int)new MessageBoxBuilder().Caption(this.Title).Error().Text(error.Message).Owner((Window)this).Show();
            }
            this.DialogResult = new bool?(error == null && !this._cts.IsCancellationRequested);
        }));
            token = new CancellationToken();
        })));
    }
}
