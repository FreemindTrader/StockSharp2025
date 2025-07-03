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

#nullable disable
/// <summary>The chart element representing a band.</summary>
public sealed class ChartBandElement : ChartElement<ChartBandElement>,
  IDrawableChartElement,
  IfxChartElement,
  IChartElement,
  IChartPart<IChartElement>,
  INotifyPropertyChanging,
  INotifyPropertyChanged,
  IPersistable,
  IChartBandElement
{
  
  private DrawStyles \u0023\u003DzOoq7N0E\u003D = DrawStyles.Band;
  
  private readonly ChartLineElement \u0023\u003Dzoc_Q8vYHoZIFp8UDSw\u003D\u003D;
  
  private readonly ChartLineElement \u0023\u003Dzt\u0024HuscgmJGKgSzXy9g\u003D\u003D;
  
  private UIBaseVM \u0023\u003Dz2YSX_Z4\u003D;

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

  Color IDrawableChartElement.\u0023\u003Dz1qjZGbvRwQyP7Hs8e\u00243Q87Cexh3FHl_dIyWPqRctd8v9ZEu\u00241w\u003D\u003D()
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
      this.RaisePropertyValueChanging("", (object) value);
      this.\u0023\u003DzOoq7N0E\u003D = value;
      this.RaisePropertyChanged("");
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

  UIBaseVM IDrawableChartElement.\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy2NIVeJ\u0024WEKCPOgxige9iqo_yKcrMQ\u003D\u003D(
    ScichartSurfaceMVVM _param1)
  {
    this.\u0023\u003Dz2YSX_Z4\u003D = _param1.Area.XAxisType == ChartAxisType.Numeric ? (UIBaseVM) new \u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<double>(this) : (UIBaseVM) new \u0023\u003DztorG3HTUDpMsfjPqFEEe9I55QlyU1R34a\u0024\u0024M2u5Uiq7Pu7_oc1A1JQ8nQQRm<DateTime>(this);
    return this.\u0023\u003Dz2YSX_Z4\u003D;
  }

  bool IDrawableChartElement.\u0023\u003DzJXDjnZfs8tGoFCupfSBAn4fwfCXfeCPpi\u0024rZmqxbRCtxRCyVSA\u003D\u003D(
    IEnumerableEx<ChartDrawData.IDrawValue> _param1)
  {
    return this.\u0023\u003Dz2YSX_Z4\u003D.Draw(_param1);
  }

  void IDrawableChartElement.\u0023\u003DzolvWmzKCnovSLB\u0024fEd65U8XPmuyOBlZpMiNagFIxa3issk4ACmj9rvI\u003D()
  {
    this.\u0023\u003Dz2YSX_Z4\u003D.Draw(CollectionHelper.ToEx<ChartDrawData.IDrawValue>(Enumerable.Empty<ChartDrawData.IDrawValue>(), 0));
  }

  /// <inheritdoc />
  protected override bool OnDraw(ChartDrawData data)
  {
    IEnumerableEx<ChartDrawData.IDrawValue> ienumerableEx = data.GetCandleRelatedData((IChartBandElement) this);
    return ienumerableEx != null && !CollectionHelper.IsEmpty<ChartDrawData.IDrawValue>((IEnumerable<ChartDrawData.IDrawValue>) ienumerableEx) && ((IDrawableChartElement) this).StartDrawing(ienumerableEx);
  }

  /// <summary>Load settings.</summary>
  /// <param name="storage">Settings storage.</param>
  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    if (((SynchronizedDictionary<string, object>) storage).ContainsKey(""))
    {
      this.RemoveChildElement((IChartElement) this.Line1);
      PersistableHelper.Load((IPersistable) this.Line1, storage, "");
      this.AddChildElement((IChartElement) this.Line1, true);
    }
    if (!((SynchronizedDictionary<string, object>) storage).ContainsKey(""))
      return;
    this.RemoveChildElement((IChartElement) this.Line2);
    PersistableHelper.Load((IPersistable) this.Line2, storage, "");
    this.AddChildElement((IChartElement) this.Line2, true);
  }

  /// <summary>Save settings.</summary>
  /// <param name="storage">Settings storage.</param>
  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<SettingsStorage>("", PersistableHelper.Save((IPersistable) this.Line1));
    storage.SetValue<SettingsStorage>("", PersistableHelper.Save((IPersistable) this.Line2));
  }

  internal override ChartBandElement Clone(ChartBandElement _param1)
  {
    _param1 = base.Clone(_param1);
    this.Line1.Clone(_param1.Line1);
    this.Line2.Clone(_param1.Line2);
    return _param1;
  }

  private void \u0023\u003DzYOVs02TTWUSQMqJ1zDI_dVw\u003D(
    #nullable enable
    object? _param1,
    PropertyChangedEventArgs _param2)
  {
    if (!(_param2.PropertyName == ""))
      return;
    this.RaisePropertyChanged("");
  }
}
