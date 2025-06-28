// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.StatusBarPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Logging;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Testing;
using StockSharp.BusinessEntities;
using StockSharp.Studio.Core.Commands;
using StockSharp.Studio.Core.Configuration;
using StockSharp.Studio.WebApi;
using StockSharp.Web.DomainModel;
using StockSharp.Xaml;

#nullable disable
namespace StockSharp.Studio.Controls;

public partial class StatusBarPanel :
  BaseStudioControl,
  ILogListener,
  IPersistable,
  IDisposable,
  IComponentConnector
{
    public static readonly RoutedCommand CopyToBufferCommand = new RoutedCommand();
    public static readonly RoutedCommand LogDirectoryCommand = new RoutedCommand();
    public static readonly DependencyProperty StatusProperty = DependencyProperty.Register(nameof(Status), typeof(string), typeof(StatusBarPanel));
    public static readonly DependencyProperty LastLogInfoProperty = DependencyProperty.Register(nameof(LastLogInfo), typeof(StatusBarPanel.LogInfo), typeof(StatusBarPanel), new PropertyMetadata((object)new StatusBarPanel.LogInfo()));
    public static readonly DependencyProperty LoginInfoProperty = DependencyProperty.Register(nameof(LoginInfo), typeof(string), typeof(StatusBarPanel));
    
    public string Status
    {
        get => (string)this.GetValue(StatusBarPanel.StatusProperty);
        set => this.SetValue(StatusBarPanel.StatusProperty, (object)value);
    }

    public StatusBarPanel.LogInfo LastLogInfo
    {
        get => (StatusBarPanel.LogInfo)this.GetValue(StatusBarPanel.LastLogInfoProperty);
        set => this.SetValue(StatusBarPanel.LastLogInfoProperty, (object)value);
    }

    public string LoginInfo
    {
        get => (string)this.GetValue(StatusBarPanel.LoginInfoProperty);
        set => this.SetValue(StatusBarPanel.LoginInfoProperty, (object)value);
    }

    public StatusBarPanel()
    {
        this.InitializeComponent();
        if (this.IsDesignMode())
            return;
        this.Register<EntityCommand<News>>((object)this, true, (Action<EntityCommand<News>>)(cmd =>
        {
            if (!cmd.Entity.IsStockSharp())
                return;
            this.AdvertisePanel.AddNews(cmd.Entity);
        }));
        WebApiHelper.ProfileChanged += new Action(this.RefreshLoginString);
    }

    public override void Dispose(CloseReason reason)
    {
        WebApiHelper.ProfileChanged -= new Action(this.RefreshLoginString);
        base.Dispose(reason);
    }

    public void ResetLogsImages()
    {
        this.LastLogInfo.LastWarnVisible = Visibility.Collapsed;
        this.LastLogInfo.LastErrorVisible = Visibility.Collapsed;
        this.LastLogInfo.LastLogMessage = string.Empty;
    }

    private void RefreshLoginString()
    {
        ((DispatcherObject)this).GuiAsync((Action)(() => this.LoginInfo = WebApiHelper.Profile?.DisplayName));
    }

    private void CopyToBufferCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = !StringHelper.IsEmpty(this.LastLogInfo.LastLogMessage);
    }

    private void CopyToBufferCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        this.LastLogInfo.LastLogMessage.CopyToClipboard<string>();
    }

    private void LogDirectoryCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = true;
    }

    private void LogDirectoryCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        this.ResetLogsImages();
        string logsDir = StudioUserConfig.Instance.LogConfig.LogsDir;
        if (!Directory.Exists(logsDir))
            return;
        logsDir.TryOpenLink((DependencyObject)this);
    }

    bool ILogListener.CanSave => false;

    void ILogListener.WriteMessages(IEnumerable<LogMessage> messages)
    {
        foreach (LogMessage message1 in messages)
        {
            LogMessage message = message1;
            if ((message.Level == LogLevels.Warning || message.Level == LogLevels.Error ) && !(message.Source is HistoryEmulationConnector) && !(message.Source is HistoryMessageAdapter) && !(message.Source is EmulationMessageAdapter) && !(message.Source is MarketEmulator))
            {
                Task<ProductBugReport> task = message.TrySendBugReport(new CancellationToken());
                if (task != null)
                    LoggingHelper.ObserveErrorAndTrace((Task)task);
                GuiDispatcher.GlobalDispatcher.AddAction((Action)(() =>
                {
                    this.LastLogInfo.LastLogMessage = $"{message.Time:HH:mm:ss}  {message.Message}";
                    if (message.Level == LogLevels.Warning)
                    {
                        this.LastLogInfo.LastWarnVisible = Visibility.Visible;
                        this.LastLogInfo.LastErrorVisible = Visibility.Collapsed;
                    }
                    else
                    {
                        this.LastLogInfo.LastWarnVisible = Visibility.Collapsed;
                        this.LastLogInfo.LastErrorVisible = Visibility.Visible;
                    }
                }));
            }
        }
    }

    

    public class LogInfo : NotifiableObject
    {
        private Visibility _lastErrorVisible = Visibility.Collapsed;
        private Visibility _lastWarnVisible = Visibility.Collapsed;
        private string _lastLogMessage;

        public Visibility LastErrorVisible
        {
            get => this._lastErrorVisible;
            set
            {
                if (this._lastErrorVisible == value)
                    return;
                this._lastErrorVisible = value;
                this.NotifyChanged(nameof(LastErrorVisible));
            }
        }

        public Visibility LastWarnVisible
        {
            get => this._lastWarnVisible;
            set
            {
                if (this._lastWarnVisible == value)
                    return;
                this._lastWarnVisible = value;
                this.NotifyChanged(nameof(LastWarnVisible));
            }
        }

        public string LastLogMessage
        {
            get => this._lastLogMessage;
            set
            {
                if (this._lastLogMessage == value)
                    return;
                this._lastLogMessage = value;
                this.NotifyChanged(nameof(LastLogMessage));
            }
        }
    }
}
