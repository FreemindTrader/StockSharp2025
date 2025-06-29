// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.RelativeVigorIndexPainter
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
/// The chart element for <see cref="T:StockSharp.Algo.Indicators.RelativeVigorIndex" />.
/// </summary>
[Display(ResourceType = typeof (LocalizedStrings), Name = "RVI")]
[Indicator(typeof (RelativeVigorIndex))]
public class RelativeVigorIndexPainter : BaseChartIndicatorPainter<RelativeVigorIndex>
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IChartLineElement \u0023\u003DzCN3S7rEn1IGLGTzJlg\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IChartLineElement \u0023\u003DzgPhsiGaFv1khCrU7gg\u003D\u003D;

  /// <summary>Create instance.</summary>
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

  /// <summary>
  /// <see cref="P:StockSharp.Algo.Indicators.RelativeVigorIndex.Signal" />.
  ///     </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "Signal", Description = "SignalPart")]
  public IChartLineElement Signal => this.\u0023\u003DzCN3S7rEn1IGLGTzJlg\u003D\u003D;

  /// <summary>
  /// <see cref="P:StockSharp.Algo.Indicators.RelativeVigorIndex.Average" />.
  ///     </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "Average", Description = "AveragePart")]
  public IChartLineElement Average => this.\u0023\u003DzgPhsiGaFv1khCrU7gg\u003D\u003D;

  /// <inheritdoc />
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

  /// <inheritdoc />
  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    PersistableHelper.Load((IPersistable) this.Signal, storage, \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539442360));
    PersistableHelper.Load((IPersistable) this.Average, storage, \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539442372));
  }

  /// <inheritdoc />
  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<SettingsStorage>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539442360), PersistableHelper.Save((IPersistable) this.Signal));
    storage.SetValue<SettingsStorage>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539442372), PersistableHelper.Save((IPersistable) this.Average));
  }
}
