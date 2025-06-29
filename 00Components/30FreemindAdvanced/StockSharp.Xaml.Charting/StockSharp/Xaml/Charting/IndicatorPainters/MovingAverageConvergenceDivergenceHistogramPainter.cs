// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.MovingAverageConvergenceDivergenceHistogramPainter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

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

/// <summary>
/// The chart element for <see cref="T:StockSharp.Algo.Indicators.MovingAverageConvergenceDivergenceHistogram" />.
/// </summary>
[Indicator(typeof (MovingAverageConvergenceDivergenceHistogram))]
public class MovingAverageConvergenceDivergenceHistogramPainter : 
  BaseChartIndicatorPainter<MovingAverageConvergenceDivergenceHistogram>
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IChartLineElement \u0023\u003Dzp7lb78Uy6qMoJVXiX8l3ZdU\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IChartLineElement \u0023\u003DzCN3S7rEn1IGLGTzJlg\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IChartLineElement \u0023\u003DzzDv8WsjezxtjXG3nKA\u003D\u003D;

  /// <summary>Create instance.</summary>
  public MovingAverageConvergenceDivergenceHistogramPainter()
  {
    IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<MovingAverageConvergenceDivergenceHistogram>.\u0023\u003Dzl7RImWAQVb2K();
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

  /// <summary>
  /// <see cref="P:StockSharp.Xaml.Charting.IndicatorPainters.MovingAverageConvergenceDivergenceHistogramPainter.Macd" /> line.
  ///     </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "MACD", Description = "SignalMaDesc")]
  public IChartLineElement Macd => this.\u0023\u003Dzp7lb78Uy6qMoJVXiX8l3ZdU\u003D;

  /// <summary>
  /// <see cref="P:StockSharp.Xaml.Charting.IndicatorPainters.MovingAverageConvergenceDivergenceHistogramPainter.Signal" /> line.
  ///     </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "SignalMa", Description = "SignalMaDesc")]
  public IChartLineElement Signal => this.\u0023\u003DzCN3S7rEn1IGLGTzJlg\u003D\u003D;

  /// <summary>
  /// <see cref="P:StockSharp.Xaml.Charting.IndicatorPainters.MovingAverageConvergenceDivergenceHistogramPainter.Histogram" /> line.
  ///     </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "Histogram", Description = "HistogramDesc")]
  public IChartLineElement Histogram => this.\u0023\u003DzzDv8WsjezxtjXG3nKA\u003D\u003D;

  /// <inheritdoc />
  protected override bool OnDraw(
    MovingAverageConvergenceDivergenceHistogram indicator,
    IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
  {
    return (0 | (this.DrawValues(data[(IIndicator) indicator.Macd], (IChartElement) this.Macd) ? 1 : 0) | (this.DrawValues(data[(IIndicator) indicator.SignalMa], (IChartElement) this.Signal) ? 1 : 0) | (this.DrawValues(data[(IIndicator) indicator.Macd], data[(IIndicator) indicator.SignalMa], (IChartElement) this.Histogram, MovingAverageConvergenceDivergenceHistogramPainter.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D ?? (MovingAverageConvergenceDivergenceHistogramPainter.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D = new Func<double, double, double>(MovingAverageConvergenceDivergenceHistogramPainter.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzfNQ20tJQMKdiKg4IJQRgukE\u003D))) ? 1 : 0)) != 0;
  }

  /// <inheritdoc />
  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    PersistableHelper.Load((IPersistable) this.Macd, storage, \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539442313));
    PersistableHelper.Load((IPersistable) this.Signal, storage, \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539442360));
    PersistableHelper.Load((IPersistable) this.Histogram, storage, \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539442337));
  }

  /// <inheritdoc />
  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<SettingsStorage>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539442313), PersistableHelper.Save((IPersistable) this.Macd));
    storage.SetValue<SettingsStorage>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539442360), PersistableHelper.Save((IPersistable) this.Signal));
    storage.SetValue<SettingsStorage>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539442337), PersistableHelper.Save((IPersistable) this.Histogram));
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly MovingAverageConvergenceDivergenceHistogramPainter.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new MovingAverageConvergenceDivergenceHistogramPainter.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<double, double, double> \u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D;

    internal double \u0023\u003DzfNQ20tJQMKdiKg4IJQRgukE\u003D(double _param1, double _param2)
    {
      return _param1 - _param2;
    }
  }
}
