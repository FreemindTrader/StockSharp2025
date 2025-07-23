// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.StochasticOscillatorPainter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using StockSharp.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

[Indicator(typeof(StochasticOscillator))]
public class StochasticOscillatorPainter : BaseChartIndicatorPainter<StochasticOscillator>
{

    private readonly IChartLineElement _lineK;

    private readonly IChartLineElement _lineD;

    public StochasticOscillatorPainter()
    {
        IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<StochasticOscillator>.GetColorProvider();
        this._lineK = (IChartLineElement)new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        this._lineD = (IChartLineElement)new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        this.AddChildElement((IChartElement)this.K);
        this.AddChildElement((IChartElement)this.D);
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "SOK", Description = "SOK")]
    public IChartLineElement K => this._lineK;

    [Display(ResourceType = typeof(LocalizedStrings), Name = "SOD", Description = "SOD")]
    public IChartLineElement D => this._lineD;

    protected override bool OnDraw(
      StochasticOscillator indicator,
      IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
    {
        bool flag = false | this.DrawValues(data[(IIndicator)indicator.K], (IChartElement)this.K);
        IList<ChartDrawData.IndicatorData> vals;
        if ( data.TryGetValue((IIndicator)indicator.D, out vals) )
            flag |= this.DrawValues(vals, (IChartElement)this.D);
        return flag;
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        PersistableHelper.Load((IPersistable)this.K, storage, "K");
        PersistableHelper.Load((IPersistable)this.D, storage, "D");
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("K", PersistableHelper.Save((IPersistable)this.K));
        storage.SetValue<SettingsStorage>("D", PersistableHelper.Save((IPersistable)this.D));
    }
}
