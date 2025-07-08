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

[Indicator(typeof (StochasticOscillator))]
public class StochasticOscillatorPainter : BaseChartIndicatorPainter<StochasticOscillator>
{
  
  private readonly IChartLineElement \u0023\u003DzSFmzZHfa8iS1i6kLog\u003D\u003D;
  
  private readonly IChartLineElement \u0023\u003DzQ1Aqc6A\u0024OGXVEu80\u0024g\u003D\u003D;

  public StochasticOscillatorPainter()
  {
    IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<StochasticOscillator>.GetColorProvider();
    this.\u0023\u003DzSFmzZHfa8iS1i6kLog\u003D\u003D = (IChartLineElement) new ChartLineElement()
    {
      Color = indicatorColorProvider.GetNextColor()
    };
    this.\u0023\u003DzQ1Aqc6A\u0024OGXVEu80\u0024g\u003D\u003D = (IChartLineElement) new ChartLineElement()
    {
      Color = indicatorColorProvider.GetNextColor()
    };
    this.AddChildElement((IChartElement) this.K);
    this.AddChildElement((IChartElement) this.D);
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "SOK", Description = "SOK")]
  public IChartLineElement K => this.\u0023\u003DzSFmzZHfa8iS1i6kLog\u003D\u003D;

  [Display(ResourceType = typeof (LocalizedStrings), Name = "SOD", Description = "SOD")]
  public IChartLineElement D => this.\u0023\u003DzQ1Aqc6A\u0024OGXVEu80\u0024g\u003D\u003D;

  protected override bool OnDraw(
    StochasticOscillator indicator,
    IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
  {
    bool flag = false | this.DrawValues(data[(IIndicator) indicator.K], (IChartElement) this.K);
    IList<ChartDrawData.IndicatorData> vals;
    if (data.TryGetValue((IIndicator) indicator.D, out vals))
      flag |= this.DrawValues(vals, (IChartElement) this.D);
    return flag;
  }

  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    PersistableHelper.Load((IPersistable) this.K, storage, "K");
    PersistableHelper.Load((IPersistable) this.D, storage, "D");
  }

  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<SettingsStorage>("K", PersistableHelper.Save((IPersistable) this.K));
    storage.SetValue<SettingsStorage>("D", PersistableHelper.Save((IPersistable) this.D));
  }
}
