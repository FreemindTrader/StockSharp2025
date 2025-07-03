// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartAnnotation
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using Ecng.Collections;
using Ecng.Serialization;
using StockSharp.Charting;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>Annotation.</summary>
public class ChartAnnotation : 
  ChartElement<ChartAnnotation>,
  IDrawableChartElement,
  IfxChartElement,
  IChartElement,
  IChartPart<IChartElement>,
  INotifyPropertyChanging,
  INotifyPropertyChanged,
  IPersistable,
  IChartAnnotationElement
{
  
  private ChartAnnotationTypes \u0023\u003Dzl4Zvkho\u003D;
  
  private \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh9ket9MPulhZRJwbB45M1w92HjAe5qWGx_96jzkY \u0023\u003Dz2YSX_Z4\u003D;

  /// <summary>
  /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.ChartAnnotation" />.
  /// </summary>
  public ChartAnnotation() => this.IsLegend = false;

  /// <summary>Annotation type.</summary>
  [Browsable(false)]
  public ChartAnnotationTypes Type
  {
    get => this.\u0023\u003Dzl4Zvkho\u003D;
    set
    {
      if (this.\u0023\u003Dzl4Zvkho\u003D == value)
        return;
      this.\u0023\u003Dzl4Zvkho\u003D = this.\u0023\u003Dzl4Zvkho\u003D == ChartAnnotationTypes.None ? value : throw new InvalidOperationException(LocalizedStrings.AnnotationTypeCantBeChanged);
    }
  }

  Color IDrawableChartElement.\u0023\u003Dz1qjZGbvRwQyP7Hs8e\u00243Q87Cexh3FHl_dIyWPqRctd8v9ZEu\u00241w\u003D\u003D()
  {
    return Colors.Transparent;
  }

  UIBaseVM IDrawableChartElement.\u0023\u003Dz\u0024rSV2280vAtTYxM9FrXMy2NIVeJ\u0024WEKCPOgxige9iqo_yKcrMQ\u003D\u003D(
    ScichartSurfaceMVVM _param1)
  {
    if (this.Type == ChartAnnotationTypes.None)
      throw new InvalidOperationException("");
    return (UIBaseVM) (this.\u0023\u003Dz2YSX_Z4\u003D = new \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh9ket9MPulhZRJwbB45M1w92HjAe5qWGx_96jzkY(this));
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

  bool IfxChartElement.\u0023\u003Dzo13esGCwfQJn\u0024h2kOXY\u0024_eaJHoQjhAXNN2tnYbe19cEa5grTJw\u003D\u003D(
    ChartAxisType? _param1,
    ChartAxisType? _param2)
  {
    return !_param2.HasValue || _param2.GetValueOrDefault() == ChartAxisType.Numeric;
  }

  /// <inheritdoc />
  protected override bool OnDraw(ChartDrawData data)
  {
    ChartDrawData.AnnotationData annotationData = data.GetAnnotation((IChartAnnotationElement) this);
    if (annotationData == null)
      return false;
    return ((IDrawableChartElement) this).StartDrawing(CollectionHelper.ToEx<ChartDrawData.IDrawValue>((IEnumerable<ChartDrawData.IDrawValue>) new ChartDrawData.IDrawValue[1]
    {
      (ChartDrawData.IDrawValue) annotationData
    }, 1));
  }

  internal override ChartAnnotation Clone(ChartAnnotation _param1)
  {
    base.Clone(_param1);
    _param1.Type = _param1.Type;
    return _param1;
  }

  /// <inheritdoc />
  public override void Load(SettingsStorage storage)
  {
    base.Load(storage);
    this.Type = storage.GetValue<ChartAnnotationTypes>("", this.Type);
  }

  /// <inheritdoc />
  public override void Save(SettingsStorage storage)
  {
    base.Save(storage);
    storage.SetValue<ChartAnnotationTypes>("", this.Type);
  }
}
