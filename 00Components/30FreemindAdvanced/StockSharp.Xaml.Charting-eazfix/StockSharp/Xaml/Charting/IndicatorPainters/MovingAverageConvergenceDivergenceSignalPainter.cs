// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.MovingAverageConvergenceDivergenceSignalPainter
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

[Indicator(typeof(MovingAverageConvergenceDivergenceSignal))]
public class MovingAverageConvergenceDivergenceSignalPainter :
  BaseChartIndicatorPainter<MovingAverageConvergenceDivergenceSignal>
{

    private readonly IChartLineElement _macd;

    private readonly IChartLineElement _signal;

    public MovingAverageConvergenceDivergenceSignalPainter()
    {
        IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<MovingAverageConvergenceDivergenceSignal>.GetColorProvider();
        this._macd = (IChartLineElement)new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        this._signal = (IChartLineElement)new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        this.AddChildElement((IChartElement)this.Macd);
        this.AddChildElement((IChartElement)this.SignalMa);
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "MACD", Description = "MACDDesc")]
    public IChartLineElement Macd => this._macd;

    [Display(ResourceType = typeof(LocalizedStrings), Name = "SignalMa", Description = "SignalMaDesc")]
    public IChartLineElement SignalMa => this._signal;

    protected override bool OnDraw(
      MovingAverageConvergenceDivergenceSignal indicator,
      IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
    {
        bool flag = false | this.DrawValues(data[(IIndicator)indicator.Macd], (IChartElement)this.Macd);
        IList<ChartDrawData.IndicatorData> vals;
        if ( data.TryGetValue((IIndicator)indicator.SignalMa, out vals) )
            flag |= this.DrawValues(vals, (IChartElement)this.SignalMa);
        return flag;
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        PersistableHelper.Load((IPersistable)this.Macd, storage, "Macd");
        PersistableHelper.Load((IPersistable)this.SignalMa, storage, "SignalMa");
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("Macd", PersistableHelper.Save((IPersistable)this.Macd));
        storage.SetValue<SettingsStorage>("SignalMa", PersistableHelper.Save((IPersistable)this.SignalMa));
    }
}
