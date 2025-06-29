// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.FractalsPainter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Drawing;
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
/// The chart element for <see cref="T:StockSharp.Algo.Indicators.Fractals" />.
/// </summary>
[Indicator(typeof (Fractals))]
public class FractalsPainter : BaseChartIndicatorPainter<Fractals>
{
  
  private readonly IChartLineElement \u0023\u003DzwcwvWrMgvug7eh60CA\u003D\u003D;
  
  private readonly IChartLineElement \u0023\u003Dz46TP2F\u0024kN_M\u0024pspP\u0024g\u003D\u003D;

  /// <summary>Create instance.</summary>
  public FractalsPainter()
  {
    IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<Fractals>.GetIndicatorColorProvider();
    this.\u0023\u003DzwcwvWrMgvug7eh60CA\u003D\u003D = (IChartLineElement) new ChartLineElement()
    {
      Color = indicatorColorProvider.GetNextColor()
    };
    this.\u0023\u003Dz46TP2F\u0024kN_M\u0024pspP\u0024g\u003D\u003D = (IChartLineElement) new ChartLineElement()
    {
      Color = indicatorColorProvider.GetNextColor()
    };
    this.Up.Style = this.Down.Style = DrawStyles.Dot;
    this.Up.StrokeThickness = this.Down.StrokeThickness = 8;
    this.AddChildElement((IChartElement) this.Up);
    this.AddChildElement((IChartElement) this.Down);
  }

  /// <summary>
  /// <see cref="P:StockSharp.Algo.Indicators.Fractals.Up" /> dots color.
  ///     </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "UpColor", Description = "UpLineColor")]
  public IChartLineElement Up => this.\u0023\u003DzwcwvWrMgvug7eh60CA\u003D\u003D;

  /// <summary>
  /// <see cref="P:StockSharp.Algo.Indicators.Fractals.Down" /> dots color.
  ///     </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "DownColor", Description = "DownLineColor")]
  public IChartLineElement Down => this.\u0023\u003Dz46TP2F\u0024kN_M\u0024pspP\u0024g\u003D\u003D;

  /// <inheritdoc />
  protected override bool OnDraw(
    Fractals indicator,
    IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
  {
    return (0 | (this.DrawValues(data[(IIndicator) indicator.Down], (IChartElement) this.Down) ? 1 : 0) | (this.DrawValues(data[(IIndicator) indicator.Up], (IChartElement) this.Up) ? 1 : 0)) != 0;
  }

  /// <inheritdoc />
  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    PersistableHelper.Load((IPersistable) this.Up, storage, "");
    PersistableHelper.Load((IPersistable) this.Down, storage, "");
  }

  /// <inheritdoc />
  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<SettingsStorage>("", PersistableHelper.Save((IPersistable) this.Up));
    storage.SetValue<SettingsStorage>("", PersistableHelper.Save((IPersistable) this.Down));
  }
}
