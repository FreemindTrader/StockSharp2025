// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.IchimokuPainter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using StockSharp.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

[Indicator(typeof (Ichimoku))]
public class IchimokuPainter : BaseChartIndicatorPainter<Ichimoku>
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IChartLineElement \u0023\u003Dz5_AjnFw1QFtxtJC_mWAb1vI\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IChartLineElement \u0023\u003DzTNYUJPFxGP2E98LvqJ2ne4I\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IChartLineElement \u0023\u003Dzzxu0w2zkh6XPS0WbXgPV3KE\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IChartBandElement \u0023\u003Dz0S4of5OfSeLaWsWJ2rBaurI\u003D;

  public IchimokuPainter()
  {
    IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<Ichimoku>.\u0023\u003Dzl7RImWAQVb2K();
    ChartBandElement chartBandElement = new ChartBandElement();
    ChartLineElement chartLineElement1 = new ChartLineElement();
    ChartLineElement chartLineElement2 = new ChartLineElement();
    ChartLineElement chartLineElement3 = new ChartLineElement();
    chartBandElement.Line1.Color = indicatorColorProvider.GetNextColor();
    chartBandElement.Line2.Color = indicatorColorProvider.GetNextColor();
    chartBandElement.Line1.AdditionalColor = chartBandElement.Line2.AdditionalColor = chartBandElement.Line2.Color.ToTransparent((byte) 30);
    chartLineElement1.Color = indicatorColorProvider.GetNextColor();
    chartLineElement1.AdditionalColor = chartLineElement1.Color.ToTransparent((byte) 30);
    chartLineElement2.Color = indicatorColorProvider.GetNextColor();
    chartLineElement2.AdditionalColor = chartLineElement2.Color.ToTransparent((byte) 30);
    chartLineElement3.Color = indicatorColorProvider.GetNextColor();
    chartLineElement3.AdditionalColor = chartLineElement3.Color.ToTransparent((byte) 30);
    this.AddChildElement((IChartElement) (this.\u0023\u003Dz5_AjnFw1QFtxtJC_mWAb1vI\u003D = (IChartLineElement) chartLineElement1));
    this.AddChildElement((IChartElement) (this.\u0023\u003DzTNYUJPFxGP2E98LvqJ2ne4I\u003D = (IChartLineElement) chartLineElement2));
    this.AddChildElement((IChartElement) (this.\u0023\u003Dzzxu0w2zkh6XPS0WbXgPV3KE\u003D = (IChartLineElement) chartLineElement3));
    this.AddChildElement((IChartElement) (this.\u0023\u003Dz0S4of5OfSeLaWsWJ2rBaurI\u003D = (IChartBandElement) chartBandElement));
    chartBandElement.\u0023\u003Dz9i5WbtNpD44L((IChartElement) this.Senkou.Line1, "SenkouA");
    chartBandElement.\u0023\u003Dz9i5WbtNpD44L((IChartElement) this.Senkou.Line2, "SenkouB");
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "TenkanLine", Description = "TenkanLine")]
  public IChartLineElement Tenkan => this.\u0023\u003Dz5_AjnFw1QFtxtJC_mWAb1vI\u003D;

  [Display(ResourceType = typeof (LocalizedStrings), Name = "KijunLine", Description = "KijunLine")]
  public IChartLineElement Kijun => this.\u0023\u003DzTNYUJPFxGP2E98LvqJ2ne4I\u003D;

  [Display(ResourceType = typeof (LocalizedStrings), Name = "ChinkouLine", Description = "ChinkouLine")]
  public IChartLineElement Chinkou => this.\u0023\u003Dzzxu0w2zkh6XPS0WbXgPV3KE\u003D;

  [Display(ResourceType = typeof (LocalizedStrings), Name = "SenkouRange", Description = "SenkouRange")]
  public IChartBandElement Senkou => this.\u0023\u003Dz0S4of5OfSeLaWsWJ2rBaurI\u003D;

  protected override bool OnDraw(
    Ichimoku indicator,
    IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
  {
    return (0 | (this.DrawValues(data[(IIndicator) indicator.Tenkan], (IChartElement) this.Tenkan) ? 1 : 0) | (this.DrawValues(data[(IIndicator) indicator.Kijun], (IChartElement) this.Kijun) ? 1 : 0) | (this.DrawValues(data[(IIndicator) indicator.Chinkou], (IChartElement) this.Chinkou) ? 1 : 0) | (this.DrawValues(data[(IIndicator) indicator.SenkouA], data[(IIndicator) indicator.SenkouB], (IChartElement) this.Senkou) ? 1 : 0)) != 0;
  }

  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    PersistableHelper.Load((IPersistable) this.Tenkan, storage, "Tenkan");
    PersistableHelper.Load((IPersistable) this.Kijun, storage, "Kijun");
    PersistableHelper.Load((IPersistable) this.Chinkou, storage, "Chinkou");
    PersistableHelper.Load((IPersistable) this.Senkou, storage, "Senkou");
  }

  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<SettingsStorage>("Tenkan", PersistableHelper.Save((IPersistable) this.Tenkan));
    storage.SetValue<SettingsStorage>("Kijun", PersistableHelper.Save((IPersistable) this.Kijun));
    storage.SetValue<SettingsStorage>("Chinkou", PersistableHelper.Save((IPersistable) this.Chinkou));
    storage.SetValue<SettingsStorage>("Senkou", PersistableHelper.Save((IPersistable) this.Senkou));
  }
}
