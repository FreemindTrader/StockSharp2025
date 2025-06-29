// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.IchimokuPainter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

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

/// <summary>
/// The chart element for <see cref="T:StockSharp.Algo.Indicators.Ichimoku" />.
/// </summary>
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

  /// <summary>Create instance.</summary>
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
    chartBandElement.\u0023\u003Dz9i5WbtNpD44L((IChartElement) this.Senkou.Line1, XXX.SSS(-539443033));
    chartBandElement.\u0023\u003Dz9i5WbtNpD44L((IChartElement) this.Senkou.Line2, XXX.SSS(-539443019));
  }

  /// <summary>
  /// <see cref="P:StockSharp.Algo.Indicators.Ichimoku.Tenkan" /> line.
  ///     </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "TenkanLine", Description = "TenkanLine")]
  public IChartLineElement Tenkan => this.\u0023\u003Dz5_AjnFw1QFtxtJC_mWAb1vI\u003D;

  /// <summary>
  /// <see cref="P:StockSharp.Algo.Indicators.Ichimoku.Kijun" /> line.
  ///     </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "KijunLine", Description = "KijunLine")]
  public IChartLineElement Kijun => this.\u0023\u003DzTNYUJPFxGP2E98LvqJ2ne4I\u003D;

  /// <summary>
  /// <see cref="P:StockSharp.Algo.Indicators.Ichimoku.Chinkou" /> line.
  ///     </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "ChinkouLine", Description = "ChinkouLine")]
  public IChartLineElement Chinkou => this.\u0023\u003Dzzxu0w2zkh6XPS0WbXgPV3KE\u003D;

  /// <summary>Senkou range.</summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "SenkouRange", Description = "SenkouRange")]
  public IChartBandElement Senkou => this.\u0023\u003Dz0S4of5OfSeLaWsWJ2rBaurI\u003D;

  /// <inheritdoc />
  protected override bool OnDraw(
    Ichimoku indicator,
    IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
  {
    return (0 | (this.DrawValues(data[(IIndicator) indicator.Tenkan], (IChartElement) this.Tenkan) ? 1 : 0) | (this.DrawValues(data[(IIndicator) indicator.Kijun], (IChartElement) this.Kijun) ? 1 : 0) | (this.DrawValues(data[(IIndicator) indicator.Chinkou], (IChartElement) this.Chinkou) ? 1 : 0) | (this.DrawValues(data[(IIndicator) indicator.SenkouA], data[(IIndicator) indicator.SenkouB], (IChartElement) this.Senkou) ? 1 : 0)) != 0;
  }

  /// <inheritdoc />
  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    PersistableHelper.Load((IPersistable) this.Tenkan, storage, XXX.SSS(-539443061));
    PersistableHelper.Load((IPersistable) this.Kijun, storage, XXX.SSS(-539443042));
    PersistableHelper.Load((IPersistable) this.Chinkou, storage, XXX.SSS(-539443054));
    PersistableHelper.Load((IPersistable) this.Senkou, storage, XXX.SSS(-539442336));
  }

  /// <inheritdoc />
  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<SettingsStorage>(XXX.SSS(-539443061), PersistableHelper.Save((IPersistable) this.Tenkan));
    storage.SetValue<SettingsStorage>(XXX.SSS(-539443042), PersistableHelper.Save((IPersistable) this.Kijun));
    storage.SetValue<SettingsStorage>(XXX.SSS(-539443054), PersistableHelper.Save((IPersistable) this.Chinkou));
    storage.SetValue<SettingsStorage>(XXX.SSS(-539442336), PersistableHelper.Save((IPersistable) this.Senkou));
  }
}
