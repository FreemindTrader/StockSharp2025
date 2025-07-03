// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.RelativeVigorIndexPainter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using StockSharp.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

[Display(ResourceType = typeof (LocalizedStrings), Name = "RVI")]
[Indicator(typeof (RelativeVigorIndex))]
public class RelativeVigorIndexPainter : BaseChartIndicatorPainter<RelativeVigorIndex>
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IChartLineElement \u0023\u003DzCN3S7rEn1IGLGTzJlg\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IChartLineElement \u0023\u003DzgPhsiGaFv1khCrU7gg\u003D\u003D;

  public RelativeVigorIndexPainter()
  {
    IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<RelativeVigorIndex>.\u0023\u003Dzl7RImWAQVb2K();
    this.\u0023\u003DzCN3S7rEn1IGLGTzJlg\u003D\u003D = (IChartLineElement) new ChartLineElement()
    {
      Color = indicatorColorProvider.GetNextColor()
    };
    this.\u0023\u003DzgPhsiGaFv1khCrU7gg\u003D\u003D = (IChartLineElement) new ChartLineElement()
    {
      Color = indicatorColorProvider.GetNextColor()
    };
    this.AddChildElement((IChartElement) this.Signal);
    this.AddChildElement((IChartElement) this.Average);
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Signal", Description = "SignalPart")]
  public IChartLineElement Signal => this.\u0023\u003DzCN3S7rEn1IGLGTzJlg\u003D\u003D;

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Average", Description = "AveragePart")]
  public IChartLineElement Average => this.\u0023\u003DzgPhsiGaFv1khCrU7gg\u003D\u003D;

  protected override bool OnDraw(
    RelativeVigorIndex indicator,
    IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
  {
    bool flag = false | this.DrawValues(data[(IIndicator) indicator.Average], (IChartElement) this.Average);
    IList<ChartDrawData.IndicatorData> vals;
    if (data.TryGetValue((IIndicator) indicator.Signal, out vals))
      flag |= this.DrawValues(vals, (IChartElement) this.Signal);
    return flag;
  }

  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    PersistableHelper.Load((IPersistable) this.Signal, storage, "Signal");
    PersistableHelper.Load((IPersistable) this.Average, storage, "Average");
  }

  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<SettingsStorage>("Signal", PersistableHelper.Save((IPersistable) this.Signal));
    storage.SetValue<SettingsStorage>("Average", PersistableHelper.Save((IPersistable) this.Average));
  }
}
