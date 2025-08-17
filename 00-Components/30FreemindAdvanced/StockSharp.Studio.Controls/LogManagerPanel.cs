// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.LogManagerPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Ecng.ComponentModel;
using Ecng.Logging;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Localization;
using StockSharp.Xaml;

#nullable disable
namespace StockSharp.Studio.Controls;

[Display(ResourceType = typeof(LocalizedStrings), Name = "Logs", Description = "Logs")]
[Guid("F97DCB8B-2104-4DF3-B6C5-CBB2B8B3B704")]
[ToolWindow]
[VectorIcon("Logs")]
[Doc("topics/designer/user_interface/logs.html")]
public class LogManagerPanel : BaseStudioControl
{
    private readonly Monitor _monitor = new Monitor();
    private readonly GuiLogListener _listener;

    public LogManagerPanel()
    {
        this.Content = (object)this._monitor;
        this._listener = new GuiLogListener((ILogListener)this._monitor);
        if (this.IsDesignMode())
            return;
        BaseStudioControl.LogManager.Listeners.Add((ILogListener)this._listener);
        this._monitor.LayoutChanged += RaiseChangedCommand;
    }

    public bool ShowStrategies
    {
        get => this._monitor.ShowStrategies;
        set => this._monitor.ShowStrategies = value;
    }

    public override void Load(SettingsStorage storage) => this._monitor.Load(storage);

    public override void Save(SettingsStorage storage) => this._monitor.Save(storage);

    public override void Dispose(CloseReason reason)
    {
        this._monitor.LayoutChanged -= RaiseChangedCommand;
        BaseStudioControl.LogManager.Listeners.Remove((ILogListener)this._listener);
        base.Dispose(reason);
    }
}
