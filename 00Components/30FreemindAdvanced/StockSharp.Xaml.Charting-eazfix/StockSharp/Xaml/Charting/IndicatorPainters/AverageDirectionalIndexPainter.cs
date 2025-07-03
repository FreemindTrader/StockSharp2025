// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.AverageDirectionalIndexPainter
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

[Indicator(typeof (AverageDirectionalIndex))]
public class AverageDirectionalIndexPainter : BaseChartIndicatorPainter<AverageDirectionalIndex>
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IChartLineElement \u0023\u003DzVMUE9pivzG3g3vV8wlSj5XA\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IChartLineElement \u0023\u003DzjH1IaGlfmFp\u0024VdbCE5Sqc5w\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IChartLineElement \u0023\u003DzzFD5DAgXC8w5n5O1Xw\u003D\u003D;

  public AverageDirectionalIndexPainter()
  {
    IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<AverageDirectionalIndex>.\u0023\u003Dzl7RImWAQVb2K();
    this.\u0023\u003DzVMUE9pivzG3g3vV8wlSj5XA\u003D = (IChartLineElement) new ChartLineElement()
    {
      Color = indicatorColorProvider.GetNextColor()
    };
    this.\u0023\u003DzjH1IaGlfmFp\u0024VdbCE5Sqc5w\u003D = (IChartLineElement) new ChartLineElement()
    {
      Color = indicatorColorProvider.GetNextColor()
    };
    this.\u0023\u003DzzFD5DAgXC8w5n5O1Xw\u003D\u003D = (IChartLineElement) new ChartLineElement()
    {
      Color = indicatorColorProvider.GetNextColor()
    };
    this.AddChildElement((IChartElement) this.DiPlus);
    this.AddChildElement((IChartElement) this.DiMinus);
    this.AddChildElement((IChartElement) this.Adx);
  }

  [Display(ResourceType = typeof (LocalizedStrings), Name = "DiPlus", Description = "DiPlusLine")]
  public IChartLineElement DiPlus => this.\u0023\u003DzVMUE9pivzG3g3vV8wlSj5XA\u003D;

  [Display(ResourceType = typeof (LocalizedStrings), Name = "DiMinus", Description = "DiMinusLine")]
  public IChartLineElement DiMinus => this.\u0023\u003DzjH1IaGlfmFp\u0024VdbCE5Sqc5w\u003D;

  [Display(ResourceType = typeof (LocalizedStrings), Name = "Adx", Description = "AdxLine")]
  public IChartLineElement Adx => this.\u0023\u003DzzFD5DAgXC8w5n5O1Xw\u003D\u003D;

  protected override bool OnDraw(
    AverageDirectionalIndex indicator,
    IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
  {
    bool flag = false | this.DrawValues(data[(IIndicator) indicator.Dx.Plus], (IChartElement) this.DiPlus) | this.DrawValues(data[(IIndicator) indicator.Dx.Minus], (IChartElement) this.DiMinus);
    IList<ChartDrawData.IndicatorData> vals;
    if (data.TryGetValue((IIndicator) indicator.MovingAverage, out vals))
      flag |= this.DrawValues(vals, (IChartElement) this.Adx);
    return flag;
  }

  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    PersistableHelper.Load((IPersistable) this.DiPlus, storage, "DiPlus");
    PersistableHelper.Load((IPersistable) this.DiMinus, storage, "DiMinus");
    PersistableHelper.Load((IPersistable) this.Adx, storage, "Adx");
  }

  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<SettingsStorage>("DiPlus", PersistableHelper.Save((IPersistable) this.DiPlus));
    storage.SetValue<SettingsStorage>("DiMinus", PersistableHelper.Save((IPersistable) this.DiMinus));
    storage.SetValue<SettingsStorage>("Adx", PersistableHelper.Save((IPersistable) this.Adx));
  }
}
