// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.FractalPartPainter
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
/// The chart element for <see cref="T:StockSharp.Algo.Indicators.FractalPart" />.
/// </summary>
[Indicator(typeof (FractalPart))]
public class FractalPartPainter : BaseChartIndicatorPainter<FractalPart>
{
  
  private readonly IChartLineElement \u0023\u003DzMcMPnDfns7teZ8Zf3Q\u003D\u003D;

  /// <summary>Create instance.</summary>
  public FractalPartPainter()
  {
    IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<FractalPart>.GetIndicatorColorProvider();
    this.\u0023\u003DzMcMPnDfns7teZ8Zf3Q\u003D\u003D = (IChartLineElement) new ChartLineElement()
    {
      Color = indicatorColorProvider.GetNextColor(),
      Style = DrawStyles.Dot,
      StrokeThickness = 8
    };
    this.AddChildElement((IChartElement) this.Part);
  }

  /// <summary>
  /// <see cref="T:StockSharp.Algo.Indicators.FractalPart" /> dots color.
  ///     </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "DownColor", Description = "DownLineColor")]
  public IChartLineElement Part => this.\u0023\u003DzMcMPnDfns7teZ8Zf3Q\u003D\u003D;

  /// <inheritdoc />
  protected override bool OnDraw(
    FractalPart indicator,
    IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
  {
    return (0 | (this.DrawValues(data[(IIndicator) indicator], (IChartElement) this.Part) ? 1 : 0)) != 0;
  }

  /// <inheritdoc />
  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    PersistableHelper.Load((IPersistable) this.Part, storage, XXX.SSS(-539442972));
  }

  /// <inheritdoc />
  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<SettingsStorage>(XXX.SSS(-539442972), PersistableHelper.Save((IPersistable) this.Part));
  }
}
