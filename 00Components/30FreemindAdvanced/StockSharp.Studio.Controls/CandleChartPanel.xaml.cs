// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.CandleChartPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Windows.Markup;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Algo.Strategies;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Studio.Controls.Commands;
using StockSharp.Studio.Core.Commands;

#nullable disable
namespace StockSharp.Studio.Controls;

[Display(ResourceType = typeof(LocalizedStrings), Name = "Chart", Description = "CandleChartPanel")]
[VectorIcon("CandleStick")]
[Doc("topics/designer/user_interface/components/chart.html")]
[Guid("1792B83D-6D34-4D83-B8F3-8668EE8CC4DF")]
public partial class CandleChartPanel : BaseStudioControl, IComponentConnector
{
    private Strategy _strategy;

    public CandleChartPanel()
    {
        this.InitializeComponent();
        this.Register<BindCommand>((object)this, true, (Action<BindCommand>)(cmd =>
        {
            if (!cmd.CheckControl((IStudioControl)this))
                return;
            this.ChartPanel.IsInteracted = cmd.IsInteractive;
            cmd.Binder.Init((Action<Strategy>)(s =>
        {
            this._strategy = s;
            this._strategy.SetChart((IChart)this.ChartPanel);
        }), (Action<Strategy>)(s =>
        {
            if (this._strategy == null || this._strategy != s)
                return;
            this._strategy.SetChart((IChart)null);
            this._strategy = (Strategy)null;
        }));
        }));
        this.ChartPanel.MinimumRange = 200;
        this.WhenLoaded((Action)(() => new RequestBindSource((IStudioControl)this).SyncProcess((object)this)));
    }

    public override void Dispose(CloseReason reason)
    {
        this.ChartPanel.ClearAreas();
        this._strategy?.SetChart((IChart)null);
        this._strategy = (Strategy)null;
        base.Dispose(reason);
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        PersistableHelper.LoadIfNotNull((IPersistable)this.ChartPanel, storage, "ChartPanel");
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("ChartPanel", PersistableHelper.Save((IPersistable)this.ChartPanel));
    }


}
