// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.GatorOscillatorPainter
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

[Indicator(typeof (GatorOscillator))]
public class GatorOscillatorPainter : BaseChartIndicatorPainter<GatorOscillator>
{
  
  private readonly IChartLineElement \u0023\u003Dzhomr8GNw5jahnu6U\u0024A\u003D\u003D;
  
  private readonly IChartLineElement \u0023\u003DzAUSthr5NvxGtnAJT\u0024g\u003D\u003D;

  public GatorOscillatorPainter()
  {
    IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<GatorOscillator>.GetColorProvider();
    this.\u0023\u003Dzhomr8GNw5jahnu6U\u0024A\u003D\u003D = (IChartLineElement) new ChartLineElement()
    {
      Color = indicatorColorProvider.GetNextColor()
    };
    this.\u0023\u003DzAUSthr5NvxGtnAJT\u0024g\u003D\u003D = (IChartLineElement) new ChartLineElement()
    {
      Color = indicatorColorProvider.GetNextColor()
    };
    this.Histogram1.Style = this.Histogram2.Style = DrawStyles.Histogram;
    this.Histogram1.StrokeThickness = this.Histogram2.StrokeThickness = 4;
    this.AddChildElement((IChartElement) this.Histogram1);
    this.AddChildElement((IChartElement) this.Histogram2);
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Up", Description = "TopHistogram")]
  public IChartLineElement Histogram1 => this.\u0023\u003Dzhomr8GNw5jahnu6U\u0024A\u003D\u003D;

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Down", Description = "LowHistogram")]
  public IChartLineElement Histogram2 => this.\u0023\u003DzAUSthr5NvxGtnAJT\u0024g\u003D\u003D;

  protected override bool OnDraw(
    GatorOscillator indicator,
    IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
  {
    return (0 | (this.DrawValues(data[(IIndicator) indicator.Histogram1], (IChartElement) this.Histogram1) ? 1 : 0) | (this.DrawValues(data[(IIndicator) indicator.Histogram2], (IChartElement) this.Histogram2) ? 1 : 0)) != 0;
  }

  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    PersistableHelper.Load((IPersistable) this.Histogram1, storage, "Histogram1");
    PersistableHelper.Load((IPersistable) this.Histogram2, storage, "Histogram2");
  }

  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<SettingsStorage>("Histogram1", PersistableHelper.Save((IPersistable) this.Histogram1));
    storage.SetValue<SettingsStorage>("Histogram2", PersistableHelper.Save((IPersistable) this.Histogram2));
  }
}
