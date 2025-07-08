// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.FractalsPainter
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

[Indicator(typeof (Fractals))]
public class FractalsPainter : BaseChartIndicatorPainter<Fractals>
{
  
  private readonly IChartLineElement \u0023\u003DzwcwvWrMgvug7eh60CA\u003D\u003D;
  
  private readonly IChartLineElement \u0023\u003Dz46TP2F\u0024kN_M\u0024pspP\u0024g\u003D\u003D;

  public FractalsPainter()
  {
    IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<Fractals>.GetColorProvider();
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

  [Display(ResourceType = typeof (LocalizedStrings), Name = "UpColor", Description = "UpLineColor")]
  public IChartLineElement Up => this.\u0023\u003DzwcwvWrMgvug7eh60CA\u003D\u003D;

  [Display(ResourceType = typeof (LocalizedStrings), Name = "DownColor", Description = "DownLineColor")]
  public IChartLineElement Down => this.\u0023\u003Dz46TP2F\u0024kN_M\u0024pspP\u0024g\u003D\u003D;

  protected override bool OnDraw(
    Fractals indicator,
    IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
  {
    return (0 | (this.DrawValues(data[(IIndicator) indicator.Down], (IChartElement) this.Down) ? 1 : 0) | (this.DrawValues(data[(IIndicator) indicator.Up], (IChartElement) this.Up) ? 1 : 0)) != 0;
  }

  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    PersistableHelper.Load((IPersistable) this.Up, storage, "Up");
    PersistableHelper.Load((IPersistable) this.Down, storage, "Down");
  }

  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<SettingsStorage>("Up", PersistableHelper.Save((IPersistable) this.Up));
    storage.SetValue<SettingsStorage>("Down", PersistableHelper.Save((IPersistable) this.Down));
  }
}
