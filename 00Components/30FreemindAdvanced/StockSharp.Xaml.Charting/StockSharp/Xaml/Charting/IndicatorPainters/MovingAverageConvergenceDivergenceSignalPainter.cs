// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.MovingAverageConvergenceDivergenceSignalPainter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using StockSharp.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

/// <summary>
/// The chart element for <see cref="T:StockSharp.Algo.Indicators.MovingAverageConvergenceDivergenceSignal" />.
/// </summary>
[Indicator(typeof (MovingAverageConvergenceDivergenceSignal))]
public class MovingAverageConvergenceDivergenceSignalPainter : 
  BaseChartIndicatorPainter<MovingAverageConvergenceDivergenceSignal>
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IChartLineElement \u0023\u003Dzp7lb78Uy6qMoJVXiX8l3ZdU\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IChartLineElement \u0023\u003DzyDj2A4Ua\u0024B5a1WsoNzN9XeU\u003D;

  /// <summary>Create instance.</summary>
  public MovingAverageConvergenceDivergenceSignalPainter()
  {
    IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<MovingAverageConvergenceDivergenceSignal>.\u0023\u003Dzl7RImWAQVb2K();
    this.\u0023\u003Dzp7lb78Uy6qMoJVXiX8l3ZdU\u003D = (IChartLineElement) new ChartLineElement()
    {
      Color = indicatorColorProvider.GetNextColor()
    };
    this.\u0023\u003DzyDj2A4Ua\u0024B5a1WsoNzN9XeU\u003D = (IChartLineElement) new ChartLineElement()
    {
      Color = indicatorColorProvider.GetNextColor()
    };
    this.AddChildElement((IChartElement) this.Macd);
    this.AddChildElement((IChartElement) this.SignalMa);
  }

  /// <summary>
  /// <see cref="P:StockSharp.Algo.Indicators.MovingAverageConvergenceDivergenceSignal.Macd" /> line color.
  ///     </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "MACD", Description = "MACDDesc")]
  public IChartLineElement Macd => this.\u0023\u003Dzp7lb78Uy6qMoJVXiX8l3ZdU\u003D;

  /// <summary>
  /// <see cref="P:StockSharp.Algo.Indicators.MovingAverageConvergenceDivergenceSignal.SignalMa" /> line color.
  ///     </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "SignalMa", Description = "SignalMaDesc")]
  public IChartLineElement SignalMa => this.\u0023\u003DzyDj2A4Ua\u0024B5a1WsoNzN9XeU\u003D;

  /// <inheritdoc />
  protected override bool OnDraw(
    MovingAverageConvergenceDivergenceSignal indicator,
    IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
  {
    bool flag = false | this.DrawValues(data[(IIndicator) indicator.Macd], (IChartElement) this.Macd);
    IList<ChartDrawData.IndicatorData> vals;
    if (data.TryGetValue((IIndicator) indicator.SignalMa, out vals))
      flag |= this.DrawValues(vals, (IChartElement) this.SignalMa);
    return flag;
  }

  /// <inheritdoc />
  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    PersistableHelper.Load((IPersistable) this.Macd, storage, XXX.SSS(-539442313));
    PersistableHelper.Load((IPersistable) this.SignalMa, storage, XXX.SSS(-539442385));
  }

  /// <inheritdoc />
  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<SettingsStorage>(XXX.SSS(-539442313), PersistableHelper.Save((IPersistable) this.Macd));
    storage.SetValue<SettingsStorage>(XXX.SSS(-539442385), PersistableHelper.Save((IPersistable) this.SignalMa));
  }
}
