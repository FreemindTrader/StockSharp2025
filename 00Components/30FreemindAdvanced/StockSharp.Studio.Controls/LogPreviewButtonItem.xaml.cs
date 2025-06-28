// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.LogPreviewButtonItem
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using DevExpress.Xpf.Bars;
using Ecng.Logging;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Studio.Controls.Commands;
using StockSharp.Studio.Core.Commands;

#nullable disable
namespace StockSharp.Studio.Controls;

public partial class LogPreviewButtonItem :
  BarButtonItem,
  ILogListener,
  IPersistable,
  IDisposable,
  IStudioControl,
  IComponentConnector
{
    private bool _logControlOpened;
    public static readonly DependencyProperty HasErrorsProperty = DependencyProperty.Register(nameof(HasErrors), typeof(bool), typeof(LogPreviewButtonItem), new PropertyMetadata((object)false));
    

    private bool HasErrors
    {
        get => (bool)this.GetValue(LogPreviewButtonItem.HasErrorsProperty);
        set => this.SetValue(LogPreviewButtonItem.HasErrorsProperty, (object)value);
    }

    public LogPreviewButtonItem()
    {
        this.InitializeComponent();
        if (this.IsDesignMode())
            return;
        StudioServicesRegistry.CommandService.Register<ControlOpenedCommand>((object)this, false, (Action<ControlOpenedCommand>)(cmd =>
        {
            this._logControlOpened = cmd.Control.GetType() == typeof(LogManagerPanel);
            if (!this._logControlOpened)
                return;
            GuiDispatcher.GlobalDispatcher.AddAction((Action)(() => this.HasErrors = false));
        }));
    }

    private void LogPreviewButtonItem_OnItemClick(object sender, ItemClickEventArgs e)
    {
        new OpenWindowCommand(typeof(LogManagerPanel), true).SyncProcess((object)this);
        this.HasErrors = false;
    }

    bool ILogListener.CanSave => false;

    void ILogListener.WriteMessages(IEnumerable<LogMessage> messages)
    {
        if (this._logControlOpened)
            return;
        foreach (LogMessage message in messages)
        {
            if (message.Level == LogLevels.Error)
            {
                this._logControlOpened = true;
                GuiDispatcher.GlobalDispatcher.AddAction((Action)(() => this.HasErrors = true));
            }
        }
    }

    void IPersistable.Load(SettingsStorage storage)
    {
    }

    void IPersistable.Save(SettingsStorage storage)
    {
    }

    void IDisposable.Dispose() => GC.SuppressFinalize((object)this);

    string IStudioControl.Title
    {
        get => (string)null;
        set
        {
        }
    }

    void IStudioControl.FirstTimeInit()
    {
    }

    void IStudioControl.SendCommand(IStudioCommand command)
    {
    }

    ImageSource IStudioControl.Icon => (ImageSource)null;

    string IStudioControl.Key { get; set; }

    string IStudioControl.DocUrl => (string)null;

    bool IStudioControl.SaveWithLayout => false;

    bool IStudioControl.IsTitleEditable => false;

    
}
