// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartBandElement
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Collections;
using Ecng.Common;
using Ecng.Drawing;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Charting;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media;

#nullable enable
namespace StockSharp.Xaml.Charting;

/// <summary>The chart element representing a band.</summary>
public sealed class ChartBandElement : 
  ChartElement<
  #nullable disable
  ChartBandElement>,
  \u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X,
  \u0023\u003DzK74oGPE3yyB7zop8uDdznyiGMD\u0024RlAEvOQ\u003D\u003D,
  IChartElement,
  IChartPart<IChartElement>,
  INotifyPropertyChanging,
  INotifyPropertyChanged,
  IPersistable,
  IChartBandElement
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private DrawStyles \u0023\u003DzOoq7N0E\u003D = DrawStyles.Band;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly ChartLineElement \u0023\u003Dzoc_Q8vYHoZIFp8UDSw\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly ChartLineElement \u0023\u003Dzt\u0024HuscgmJGKgSzXy9g\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IZuSESVgU8LW8DvId9tdE7eLQoPdEDqa2l4\u003D \u0023\u003Dz2YSX_Z4\u003D;

  /// <summary>Create instance.</summary>
  public ChartBandElement()
  {
    this.\u0023\u003Dzoc_Q8vYHoZIFp8UDSw\u003D\u003D = new ChartLineElement()
    {
      Color = Colors.DarkGreen,
      AdditionalColor = Colors.DarkGreen.ToTransparent((byte) 50)
    };
    this.\u0023\u003Dzt\u0024HuscgmJGKgSzXy9g\u003D\u003D = new ChartLineElement()
    {
      Color = Colors.DarkGreen,
      AdditionalColor = Colors.DarkGreen.ToTransparent((byte) 50)
    };
    this.Line1.PropertyChanged += new PropertyChangedEventHandler(this.\u0023\u003DzYOVs02TTWUSQMqJ1zDI_dVw\u003D);
    this.AddChildElement((IChartElement) this.Line1, true);
    this.AddChildElement((IChartElement) this.Line2, true);
  }

  Color \u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X.\u0023\u003Dz1qjZGbvRwQyP7Hs8e\u00243Q87Cexh3FHl_dIyWPqRctd8v9ZEu\u00241w\u003D\u003D()
  {
    return this.Line1.AdditionalColor;
  }

  /// <summary>
  /// The band drawing style. The default is <see cref="F:Ecng.Drawing.DrawStyles.Band" />. Can also be <see cref="F:Ecng.Drawing.DrawStyles.BandOneValue" />.
  /// </summary>
  [Browsable(false)]
  public DrawStyles Style
  {
    get => this.\u0023\u003DzOoq7N0E\u003D;
    set
    {
      if (this.\u0023\u003DzOoq7N0E\u003D == value)
        return;
      if (value != DrawStyles.Band && value != DrawStyles.BandOneValue)
        throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.UnsupportedType, new object[1]
        {
          (object) value
        }));
      this.RaisePropertyValueChanging(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433382), (object) value);
      this.\u0023\u003DzOoq7N0E\u003D = value;
      this.RaisePropertyChanged(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433382));
    }
  }

  /// <summary>
  /// <see cref="P:StockSharp.Xaml.Charting.ChartBandElement.Line1" />.
  ///     </summary>
  public ChartLineElement Line1 => this.\u0023\u003Dzoc_Q8vYHoZIFp8UDSw\u003D\u003D;

  /// <summary>
  /// <see cref="P:StockSharp.Xaml.Charting.ChartBandElement.Line2" />.
  ///     </summary>
  public ChartLineElement Line2 => this.\u0023\u003Dzt\u0024HuscgmJGKgSzXy9g\u003D\u003D;

  IChartLineElement IChartBandElement.Line1 => (IChartLineElement) this.Line1;

  IChartLineElement IChartBandElement.Line2 => (IChartLineElement) this.Line2;

  /// <inheritdoc />
  public override bool CheckAxesCompatible(ChartAxisType? xType, ChartAxisType? yType)
  {
    return !yType.HasValue || yType.GetValueOrDefault() == ChartAxisType.Numeric;
  }

  \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IZuSESVgU8LW8DvId9tdE7eLQoPdEDqa2l4\u003D \u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X.\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy2NIVeJ\u0024WEKCPOgxige9iqo_yKcrMQ\u003D\u003D(
    \u0023\u003DzJ9vSi7sIwIEed80npzusCHkUgplLrVxmg1iWODdl3TDNKj06Uu87_wzk09Wj _param1)
  {
    this.\u0023\u003Dz2YSX_Z4\u003D = _param1.Area.XAxisType == ChartAxisType.Numeric ? (\u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IZuSESVgU8LW8DvId9tdE7eLQoPdEDqa2l4\u003D) new \u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<double>(this) : (\u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IZuSESVgU8LW8DvId9tdE7eLQoPdEDqa2l4\u003D) new \u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<DateTime>(this);
    return this.\u0023\u003Dz2YSX_Z4\u003D;
  }

  bool \u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X.\u0023\u003DzJXDjnZfs8tGoFCupfSBAn4fwfCXfeCPpi\u0024rZmqxbRCtxRCyVSA\u003D\u003D(
    IEnumerableEx<ChartDrawData.IDrawValue> _param1)
  {
    return this.\u0023\u003Dz2YSX_Z4\u003D.\u0023\u003DzjgUUUJE\u003D(_param1);
  }

  void \u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X.\u0023\u003DzolvWmzKCnovSLB\u0024fEd65U8XPmuyOBlZpMiNagFIxa3issk4ACmj9rvI\u003D()
  {
    this.\u0023\u003Dz2YSX_Z4\u003D.\u0023\u003DzjgUUUJE\u003D(CollectionHelper.ToEx<ChartDrawData.IDrawValue>(Enumerable.Empty<ChartDrawData.IDrawValue>(), 0));
  }

  /// <inheritdoc />
  protected override bool OnDraw(ChartDrawData data)
  {
    IEnumerableEx<ChartDrawData.IDrawValue> ienumerableEx = data.\u0023\u003DzaZ5Qc3xeNY95((IChartBandElement) this);
    return ienumerableEx != null && !CollectionHelper.IsEmpty<ChartDrawData.IDrawValue>((IEnumerable<ChartDrawData.IDrawValue>) ienumerableEx) && ((\u0023\u003DzbZGwufOdFTewaG24h4AgEiDjYj9UUxsVv2V6fHz4VM4X) this).\u0023\u003Dz2dQykb\u0024x9fU4(ienumerableEx);
  }

  /// <summary>Load settings.</summary>
  /// <param name="storage">Settings storage.</param>
  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    if (((SynchronizedDictionary<string, object>) storage).ContainsKey(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433426)))
    {
      this.RemoveChildElement((IChartElement) this.Line1);
      PersistableHelper.Load((IPersistable) this.Line1, storage, \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433426));
      this.AddChildElement((IChartElement) this.Line1, true);
    }
    if (!((SynchronizedDictionary<string, object>) storage).ContainsKey(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433438)))
      return;
    this.RemoveChildElement((IChartElement) this.Line2);
    PersistableHelper.Load((IPersistable) this.Line2, storage, \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433438));
    this.AddChildElement((IChartElement) this.Line2, true);
  }

  /// <summary>Save settings.</summary>
  /// <param name="storage">Settings storage.</param>
  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<SettingsStorage>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433426), PersistableHelper.Save((IPersistable) this.Line1));
    storage.SetValue<SettingsStorage>(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433438), PersistableHelper.Save((IPersistable) this.Line2));
  }

  internal override ChartBandElement \u0023\u003Dz3MbNd8U\u003D(ChartBandElement _param1)
  {
    _param1 = base.\u0023\u003Dz3MbNd8U\u003D(_param1);
    this.Line1.\u0023\u003Dz3MbNd8U\u003D(_param1.Line1);
    this.Line2.\u0023\u003Dz3MbNd8U\u003D(_param1.Line2);
    return _param1;
  }

  private void \u0023\u003DzYOVs02TTWUSQMqJ1zDI_dVw\u003D(
    #nullable enable
    object? _param1,
    PropertyChangedEventArgs _param2)
  {
    if (!(_param2.PropertyName == \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433418)))
      return;
    this.RaisePropertyChanged(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433444));
  }
}
