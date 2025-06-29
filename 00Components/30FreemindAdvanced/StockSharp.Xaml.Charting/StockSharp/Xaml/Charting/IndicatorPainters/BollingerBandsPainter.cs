// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.BollingerBandsPainter
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
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

/// <summary>
/// The chart element for <see cref="T:StockSharp.Algo.Indicators.BollingerBands" />.
/// </summary>
[Indicator(typeof (BollingerBands))]
public class BollingerBandsPainter : BaseChartIndicatorPainter<BollingerBands>
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IChartBandElement \u0023\u003DzyDIAbxWLefjyyLjECw\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IChartLineElement \u0023\u003DzwZGesrtF1zqkW0dOkS\u0024KEVs\u003D;

  /// <summary>Create instance.</summary>
  public BollingerBandsPainter()
  {
    IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<BollingerBands>.\u0023\u003Dzl7RImWAQVb2K();
    Color nextColor1 = indicatorColorProvider.GetNextColor();
    Color nextColor2 = indicatorColorProvider.GetNextColor();
    ChartBandElement chartBandElement = new ChartBandElement();
    chartBandElement.Line1.AdditionalColor = chartBandElement.Line2.AdditionalColor = nextColor1.ToTransparent((byte) 30);
    chartBandElement.Line1.Color = chartBandElement.Line2.Color = nextColor1;
    ChartLineElement chartLineElement = new ChartLineElement()
    {
      Color = nextColor2,
      AdditionalColor = nextColor2.ToTransparent((byte) 30)
    };
    this.AddChildElement((IChartElement) (this.\u0023\u003DzyDIAbxWLefjyyLjECw\u003D\u003D = (IChartBandElement) chartBandElement));
    this.AddChildElement((IChartElement) (this.\u0023\u003DzwZGesrtF1zqkW0dOkS\u0024KEVs\u003D = (IChartLineElement) chartLineElement));
    chartBandElement.\u0023\u003Dz9i5WbtNpD44L((IChartElement) chartBandElement.Line1, LocalizedStrings.UpperLine);
    chartBandElement.\u0023\u003Dz9i5WbtNpD44L((IChartElement) chartBandElement.Line2, LocalizedStrings.LowerLine);
    chartBandElement.Line2.\u0023\u003DziQx4gl4\u003D(XXX.SSS(-539433418));
  }

  /// <summary>
  /// <see cref="P:StockSharp.Xaml.Charting.IndicatorPainters.BollingerBandsPainter.Band" />.
  ///     </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "Band", Description = "Band")]
  public IChartBandElement Band => this.\u0023\u003DzyDIAbxWLefjyyLjECw\u003D\u003D;

  /// <summary>
  /// <see cref="P:StockSharp.Xaml.Charting.IndicatorPainters.BollingerBandsPainter.MovingAverage" />.
  ///     </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "MovingAverage", Description = "MovingAverage")]
  public IChartLineElement MovingAverage => this.\u0023\u003DzwZGesrtF1zqkW0dOkS\u0024KEVs\u003D;

  /// <inheritdoc />
  protected override bool OnDraw(
    BollingerBands indicator,
    IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
  {
    return (0 | (this.DrawValues(data[(IIndicator) indicator.UpBand], data[(IIndicator) indicator.LowBand], (IChartElement) this.Band) ? 1 : 0) | (this.DrawValues(data[(IIndicator) indicator.MovingAverage], (IChartElement) this.MovingAverage) ? 1 : 0)) != 0;
  }

  /// <inheritdoc />
  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    PersistableHelper.Load((IPersistable) this.Band, storage, XXX.SSS(-539443161));
    PersistableHelper.Load((IPersistable) this.MovingAverage, storage, XXX.SSS(-539443144));
  }

  /// <inheritdoc />
  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<SettingsStorage>(XXX.SSS(-539443161), PersistableHelper.Save((IPersistable) this.Band));
    storage.SetValue<SettingsStorage>(XXX.SSS(-539443144), PersistableHelper.Save((IPersistable) this.MovingAverage));
  }
}
