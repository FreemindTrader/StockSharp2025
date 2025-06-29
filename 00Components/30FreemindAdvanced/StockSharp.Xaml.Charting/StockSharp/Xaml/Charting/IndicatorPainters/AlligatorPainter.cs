// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.AlligatorPainter
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
/// Chart painter for <see cref="T:StockSharp.Algo.Indicators.Alligator" /> indicator.
/// </summary>
[Display(ResourceType = typeof (LocalizedStrings), Name = "Alligator")]
[Indicator(typeof (Alligator))]
public class AlligatorPainter : BaseChartIndicatorPainter<Alligator>
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IChartLineElement \u0023\u003Dzhbxo8_u3o4LM4hVvNIChcJE\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IChartLineElement \u0023\u003Dz2JC2sIKxOe2wn1s69jRzXRA\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IChartLineElement \u0023\u003DzCmkJSWMuSM6EoQwLKA\u003D\u003D;

  /// <summary>Create instance.</summary>
  public AlligatorPainter()
  {
    IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<Alligator>.\u0023\u003Dzl7RImWAQVb2K();
    this.\u0023\u003Dzhbxo8_u3o4LM4hVvNIChcJE\u003D = (IChartLineElement) new ChartLineElement()
    {
      Color = indicatorColorProvider.GetNextColor()
    };
    this.\u0023\u003Dz2JC2sIKxOe2wn1s69jRzXRA\u003D = (IChartLineElement) new ChartLineElement()
    {
      Color = indicatorColorProvider.GetNextColor()
    };
    this.\u0023\u003DzCmkJSWMuSM6EoQwLKA\u003D\u003D = (IChartLineElement) new ChartLineElement()
    {
      Color = indicatorColorProvider.GetNextColor()
    };
    this.AddChildElement((IChartElement) this.Lips);
    this.AddChildElement((IChartElement) this.Teeth);
    this.AddChildElement((IChartElement) this.Jaw);
  }

  /// <summary>
  /// <see cref="P:StockSharp.Algo.Indicators.Alligator.Lips" />.
  ///     </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "Lips", Description = "Lips")]
  public IChartLineElement Lips => this.\u0023\u003Dzhbxo8_u3o4LM4hVvNIChcJE\u003D;

  /// <summary>
  /// <see cref="P:StockSharp.Algo.Indicators.Alligator.Teeth" />.
  ///     </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "Teeth", Description = "Teeth")]
  public IChartLineElement Teeth => this.\u0023\u003Dz2JC2sIKxOe2wn1s69jRzXRA\u003D;

  /// <summary>
  /// <see cref="P:StockSharp.Algo.Indicators.Alligator.Jaw" />.
  ///     </summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "Jaw", Description = "Jaw")]
  public IChartLineElement Jaw => this.\u0023\u003DzCmkJSWMuSM6EoQwLKA\u003D\u003D;

  /// <inheritdoc />
  protected override bool OnDraw(
    Alligator indicator,
    IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
  {
    return (0 | (this.DrawValues(data[(IIndicator) indicator.Lips], (IChartElement) this.Lips) ? 1 : 0) | (this.DrawValues(data[(IIndicator) indicator.Teeth], (IChartElement) this.Teeth) ? 1 : 0) | (this.DrawValues(data[(IIndicator) indicator.Jaw], (IChartElement) this.Jaw) ? 1 : 0)) != 0;
  }

  /// <inheritdoc />
  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    PersistableHelper.Load((IPersistable) this.Lips, storage, \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539442714));
    PersistableHelper.Load((IPersistable) this.Teeth, storage, \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539442693));
    PersistableHelper.Load((IPersistable) this.Jaw, storage, \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539442737));
  }

  /// <inheritdoc />
  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<SettingsStorage>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539442714), PersistableHelper.Save((IPersistable) this.Lips));
    storage.SetValue<SettingsStorage>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539442693), PersistableHelper.Save((IPersistable) this.Teeth));
    storage.SetValue<SettingsStorage>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539442737), PersistableHelper.Save((IPersistable) this.Jaw));
  }
}
