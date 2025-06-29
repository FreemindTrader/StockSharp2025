// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.VolumePainter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Drawing;
using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

/// <summary>
/// The chart element for <see cref="T:StockSharp.Algo.Indicators.VolumeIndicator" />.
/// </summary>
[Indicator(typeof (VolumeIndicator))]
public class VolumePainter : BaseChartIndicatorPainter<VolumeIndicator>
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IChartLineElement \u0023\u003DzA_pKfoSCgXJNJvgk60qKe4k\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IChartLineElement \u0023\u003DzpZ7H_10udEV9\u0024GGb0g555yM\u003D;

  /// <summary>Create instance.</summary>
  public VolumePainter()
  {
    this.\u0023\u003DzA_pKfoSCgXJNJvgk60qKe4k\u003D = (IChartLineElement) new ChartLineElement()
    {
      Color = Colors.Green
    };
    this.\u0023\u003DzpZ7H_10udEV9\u0024GGb0g555yM\u003D = (IChartLineElement) new ChartLineElement()
    {
      Color = Colors.Red
    };
    this.UpVolume.Style = this.DownVolume.Style = DrawStyles.Histogram;
    this.AddChildElement((IChartElement) this.UpVolume);
    this.AddChildElement((IChartElement) this.DownVolume);
  }

  /// <summary>Up line color.</summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "UpColor", Description = "UpCandleColor")]
  public IChartLineElement UpVolume => this.\u0023\u003DzA_pKfoSCgXJNJvgk60qKe4k\u003D;

  /// <summary>Down line color.</summary>
  [Display(ResourceType = typeof (LocalizedStrings), Name = "DownColor", Description = "DownCandleColor")]
  public IChartLineElement DownVolume => this.\u0023\u003DzpZ7H_10udEV9\u0024GGb0g555yM\u003D;

  /// <inheritdoc />
  protected override bool OnDraw(
    VolumeIndicator indicator,
    IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
  {
    return (0 | (this.DrawValues(data[(IIndicator) indicator], (IChartElement) this.UpVolume, VolumePainter.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzwZAh_kxQ5NwXvdudRw\u003D\u003D ?? (VolumePainter.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzwZAh_kxQ5NwXvdudRw\u003D\u003D = new Func<ChartDrawData.IndicatorData, double>(VolumePainter.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzCqJJ\u0024wgpxEVd_TJWqw\u003D\u003D))) ? 1 : 0) | (this.DrawValues(data[(IIndicator) indicator], (IChartElement) this.DownVolume, VolumePainter.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz9FkMhQuPxTObvFx1sg\u003D\u003D ?? (VolumePainter.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003Dz9FkMhQuPxTObvFx1sg\u003D\u003D = new Func<ChartDrawData.IndicatorData, double>(VolumePainter.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dz2dmg6iDJoRoCFC4spg\u003D\u003D))) ? 1 : 0)) != 0;
  }

  /// <inheritdoc />
  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    PersistableHelper.Load((IPersistable) this.UpVolume, storage, XXX.SSS(-539442430));
    PersistableHelper.Load((IPersistable) this.DownVolume, storage, XXX.SSS(-539442413));
  }

  /// <inheritdoc />
  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<SettingsStorage>(XXX.SSS(-539442430), PersistableHelper.Save((IPersistable) this.UpVolume));
    storage.SetValue<SettingsStorage>(XXX.SSS(-539442413), PersistableHelper.Save((IPersistable) this.DownVolume));
  }

  internal static bool \u0023\u003DqEc67BxG9dg23wtiNP0FWDik0fpoX7z9zoraohHfyiMo\u003D(
    IIndicatorValue _param0)
  {
    if (_param0 == null)
      return false;
    ICandleMessage icandleMessage = _param0.GetValue<ICandleMessage>();
    return icandleMessage == null || icandleMessage.OpenPrice < icandleMessage.ClosePrice;
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly VolumePainter.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new VolumePainter.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<ChartDrawData.IndicatorData, double> \u0023\u003DzwZAh_kxQ5NwXvdudRw\u003D\u003D;
    public static Func<ChartDrawData.IndicatorData, double> \u0023\u003Dz9FkMhQuPxTObvFx1sg\u003D\u003D;

    internal double \u0023\u003DzCqJJ\u0024wgpxEVd_TJWqw\u003D\u003D(
      ChartDrawData.IndicatorData _param1)
    {
      return !VolumePainter.\u0023\u003DqEc67BxG9dg23wtiNP0FWDik0fpoX7z9zoraohHfyiMo\u003D(_param1.Value) ? double.NaN : (double) _param1.Value.ToDecimal();
    }

    internal double \u0023\u003Dz2dmg6iDJoRoCFC4spg\u003D\u003D(ChartDrawData.IndicatorData _param1)
    {
      return !VolumePainter.\u0023\u003DqEc67BxG9dg23wtiNP0FWDik0fpoX7z9zoraohHfyiMo\u003D(_param1.Value) ? (double) _param1.Value.ToDecimal() : double.NaN;
    }
  }
}
