// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.DefaultPainter
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
using System.Drawing;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

/// <summary>Indicator painter which is used by default.</summary>
public class DefaultPainter : BaseChartIndicatorPainter<IIndicator>
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IChartLineElement \u0023\u003Dzc5oaf3eKyon\u00249mHymQ\u003D\u003D;

  /// <summary>Create instance.</summary>
  public DefaultPainter()
  {
    IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<IIndicator>.\u0023\u003Dzl7RImWAQVb2K();
    this.\u0023\u003Dzc5oaf3eKyon\u00249mHymQ\u003D\u003D = (IChartLineElement) new ChartLineElement()
    {
      Color = indicatorColorProvider.GetNextColor()
    };
    this.AddChildElement((IChartElement) this.Line);
  }

  /// <summary>Default indicator line element.</summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "Line2", Description = "Line2")]
  public IChartLineElement Line => this.\u0023\u003Dzc5oaf3eKyon\u00249mHymQ\u003D\u003D;

  /// <inheritdoc />
  protected override bool OnDraw(
    IIndicator indicator,
    IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
  {
    if (!(indicator is IComplexIndicator complexIndicator))
      return this.DrawValues(data[indicator], (IChartElement) this.Line);
    IReadOnlyList<IIndicator> innerIndicators = complexIndicator.InnerIndicators;
    int count1 = innerIndicators.Count;
    int count2 = this.InnerElements.Count;
    int num1 = count2;
    int num2 = count1 - num1;
    if (num2 > 0)
    {
      IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<IIndicator>.\u0023\u003Dzl7RImWAQVb2K();
      for (int index = 0; index < num2; ++index)
      {
        IIndicator indicator1 = innerIndicators[count2 + index];
        ChartLineElement element = new ChartLineElement();
        Color? color = indicator1.Color;
        ref Color? local = ref color;
        element.Color = local.HasValue ? local.GetValueOrDefault().ToWpf() : indicatorColorProvider.GetNextColor();
        element.Style = indicator1.Style;
        this.AddChildElement((IChartElement) element);
      }
    }
    else if (num2 < 0)
    {
      int num3 = -num2;
      for (int index = 0; index < num3; ++index)
        this.RemoveChildElement(this.InnerElements[this.InnerElements.Count - 1]);
    }
    bool flag = false;
    int num4 = 0;
    foreach (IIndicator key in (IEnumerable<IIndicator>) innerIndicators)
      flag |= this.DrawValues(data[key], this.InnerElements[num4++]);
    return flag;
  }

  /// <inheritdoc />
  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    PersistableHelper.Load((IPersistable) this.Line, storage, \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539443181));
  }

  /// <inheritdoc />
  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<SettingsStorage>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539443181), PersistableHelper.Save((IPersistable) this.Line));
  }
}
