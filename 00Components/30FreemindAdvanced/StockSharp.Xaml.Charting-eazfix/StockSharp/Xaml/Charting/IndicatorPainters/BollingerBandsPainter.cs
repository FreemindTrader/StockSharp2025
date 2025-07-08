// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.BollingerBandsPainter
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
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

[Indicator(typeof (BollingerBands))]
public class BollingerBandsPainter : BaseChartIndicatorPainter<BollingerBands>
{
  
  private readonly IChartBandElement \u0023\u003DzyDIAbxWLefjyyLjECw\u003D\u003D;
  
  private readonly IChartLineElement \u0023\u003DzwZGesrtF1zqkW0dOkS\u0024KEVs\u003D;

  public BollingerBandsPainter()
  {
    IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<BollingerBands>.GetColorProvider();
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
    chartBandElement.SetName((IChartElement) chartBandElement.Line1, LocalizedStrings.UpperLine);
    chartBandElement.SetName((IChartElement) chartBandElement.Line2, LocalizedStrings.LowerLine);
    chartBandElement.Line2.AddExtraName("AdditionalColor");
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Band", Description = "Band")]
  public IChartBandElement Band => this.\u0023\u003DzyDIAbxWLefjyyLjECw\u003D\u003D;

  [Display(ResourceType = typeof (LocalizedStrings), Name = "MovingAverage", Description = "MovingAverage")]
  public IChartLineElement MovingAverage => this.\u0023\u003DzwZGesrtF1zqkW0dOkS\u0024KEVs\u003D;

  protected override bool OnDraw(
    BollingerBands indicator,
    IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
  {
    return (0 | (this.DrawValues(data[(IIndicator) indicator.UpBand], data[(IIndicator) indicator.LowBand], (IChartElement) this.Band) ? 1 : 0) | (this.DrawValues(data[(IIndicator) indicator.MovingAverage], (IChartElement) this.MovingAverage) ? 1 : 0)) != 0;
  }

  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    PersistableHelper.Load((IPersistable) this.Band, storage, "Band");
    PersistableHelper.Load((IPersistable) this.MovingAverage, storage, "MovingAverage");
  }

  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<SettingsStorage>("Band", PersistableHelper.Save((IPersistable) this.Band));
    storage.SetValue<SettingsStorage>("MovingAverage", PersistableHelper.Save((IPersistable) this.MovingAverage));
  }
}
