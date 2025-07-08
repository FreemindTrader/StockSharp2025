// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.FractalPartPainter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

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

[Indicator(typeof (FractalPart))]
public class FractalPartPainter : BaseChartIndicatorPainter<FractalPart>
{
  
  private readonly IChartLineElement \u0023\u003DzMcMPnDfns7teZ8Zf3Q\u003D\u003D;

  public FractalPartPainter()
  {
    IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<FractalPart>.GetColorProvider();
    this.\u0023\u003DzMcMPnDfns7teZ8Zf3Q\u003D\u003D = (IChartLineElement) new ChartLineElement()
    {
      Color = indicatorColorProvider.GetNextColor(),
      Style = DrawStyles.Dot,
      StrokeThickness = 8
    };
    this.AddChildElement((IChartElement) this.Part);
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "DownColor", Description = "DownLineColor")]
  public IChartLineElement Part => this.\u0023\u003DzMcMPnDfns7teZ8Zf3Q\u003D\u003D;

  protected override bool OnDraw(
    FractalPart indicator,
    IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
  {
    return (0 | (this.DrawValues(data[(IIndicator) indicator], (IChartElement) this.Part) ? 1 : 0)) != 0;
  }

  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    PersistableHelper.Load((IPersistable) this.Part, storage, "Part");
  }

  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<SettingsStorage>("Part", PersistableHelper.Save((IPersistable) this.Part));
  }
}
