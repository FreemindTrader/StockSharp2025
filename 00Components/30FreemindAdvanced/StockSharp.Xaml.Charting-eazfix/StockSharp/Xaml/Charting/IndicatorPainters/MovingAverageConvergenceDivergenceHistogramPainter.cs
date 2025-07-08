// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.MovingAverageConvergenceDivergenceHistogramPainter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Drawing;
using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

[Indicator(typeof (MovingAverageConvergenceDivergenceHistogram))]
public class MovingAverageConvergenceDivergenceHistogramPainter : 
  BaseChartIndicatorPainter<MovingAverageConvergenceDivergenceHistogram>
{
  
  private readonly IChartLineElement \u0023\u003Dzp7lb78Uy6qMoJVXiX8l3ZdU\u003D;
  
  private readonly IChartLineElement \u0023\u003DzCN3S7rEn1IGLGTzJlg\u003D\u003D;
  
  private readonly IChartLineElement \u0023\u003DzzDv8WsjezxtjXG3nKA\u003D\u003D;

  public MovingAverageConvergenceDivergenceHistogramPainter()
  {
    IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<MovingAverageConvergenceDivergenceHistogram>.GetColorProvider();
    this.\u0023\u003Dzp7lb78Uy6qMoJVXiX8l3ZdU\u003D = (IChartLineElement) new ChartLineElement()
    {
      Color = indicatorColorProvider.GetNextColor()
    };
    this.\u0023\u003DzCN3S7rEn1IGLGTzJlg\u003D\u003D = (IChartLineElement) new ChartLineElement()
    {
      Color = indicatorColorProvider.GetNextColor()
    };
    this.\u0023\u003DzzDv8WsjezxtjXG3nKA\u003D\u003D = (IChartLineElement) new ChartLineElement()
    {
      Style = DrawStyles.Histogram
    };
    this.AddChildElement((IChartElement) this.Macd);
    this.AddChildElement((IChartElement) this.Signal);
    this.AddChildElement((IChartElement) this.Histogram);
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "MACD", Description = "SignalMaDesc")]
  public IChartLineElement Macd => this.\u0023\u003Dzp7lb78Uy6qMoJVXiX8l3ZdU\u003D;

  [Display(ResourceType = typeof (LocalizedStrings), Name = "SignalMa", Description = "SignalMaDesc")]
  public IChartLineElement Signal => this.\u0023\u003DzCN3S7rEn1IGLGTzJlg\u003D\u003D;

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Histogram", Description = "HistogramDesc")]
  public IChartLineElement Histogram => this.\u0023\u003DzzDv8WsjezxtjXG3nKA\u003D\u003D;

  protected override bool OnDraw(
    MovingAverageConvergenceDivergenceHistogram indicator,
    IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
  {
    return (0 | (this.DrawValues(data[(IIndicator) indicator.Macd], (IChartElement) this.Macd) ? 1 : 0) | (this.DrawValues(data[(IIndicator) indicator.SignalMa], (IChartElement) this.Signal) ? 1 : 0) | (this.DrawValues(data[(IIndicator) indicator.Macd], data[(IIndicator) indicator.SignalMa], (IChartElement) this.Histogram, MovingAverageConvergenceDivergenceHistogramPainter.SomeClass34343383.\u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D ?? (MovingAverageConvergenceDivergenceHistogramPainter.SomeClass34343383.\u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D = new Func<double, double, double>(MovingAverageConvergenceDivergenceHistogramPainter.SomeClass34343383.SomeMethond0343.\u0023\u003DzfNQ20tJQMKdiKg4IJQRgukE\u003D))) ? 1 : 0)) != 0;
  }

  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    PersistableHelper.Load((IPersistable) this.Macd, storage, "Macd");
    PersistableHelper.Load((IPersistable) this.Signal, storage, "Signal");
    PersistableHelper.Load((IPersistable) this.Histogram, storage, "Histogram");
  }

  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<SettingsStorage>("Macd", PersistableHelper.Save((IPersistable) this.Macd));
    storage.SetValue<SettingsStorage>("Signal", PersistableHelper.Save((IPersistable) this.Signal));
    storage.SetValue<SettingsStorage>("Histogram", PersistableHelper.Save((IPersistable) this.Histogram));
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly MovingAverageConvergenceDivergenceHistogramPainter.SomeClass34343383 SomeMethond0343 = new MovingAverageConvergenceDivergenceHistogramPainter.SomeClass34343383();
    public static Func<double, double, double> \u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D;

    internal double \u0023\u003DzfNQ20tJQMKdiKg4IJQRgukE\u003D(double _param1, double _param2)
    {
      return _param1 - _param2;
    }
  }
}
