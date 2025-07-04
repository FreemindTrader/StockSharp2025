// Decompiled with JetBrains decompiler
// Type: -.dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Xaml;
using StockSharp.Xaml.Charting.Visuals.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace \u002D;

internal sealed class dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd : 
  ChartModifierBase
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private ObservableCollection<IRenderableSeries> \u0023\u003Dzmhay2J9Ys\u0024BH;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly IDictionary<IRenderableSeries, IAnnotation> \u0023\u003DzJVKVgJSKleLL = (IDictionary<IRenderableSeries, IAnnotation>) new Dictionary<IRenderableSeries, IAnnotation>();
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private PropertyChangeNotifier \u0023\u003DzMFucnUdhtZnJ;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dzvp8Q5iyLlWID = DependencyProperty.Register(nameof (AxisMarkerStyle), typeof (Style), typeof (dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd), new PropertyMetadata((PropertyChangedCallback) null));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DziAqnE8_\u0024SBDB = DependencyProperty.Register(nameof (YAxisId), typeof (string), typeof (dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd), new PropertyMetadata((object) "DefaultAxisId", new PropertyChangedCallback(dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd.\u0023\u003DzdXB_wQ4DNB7BU25L6g\u003D\u003D)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzNnZwaoee8xziVPglsg\u003D\u003D = DependencyProperty.RegisterAttached("IsSeriesValueModifierEnabled", typeof (bool), typeof (dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd), new PropertyMetadata((object) true));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzEf91H67L3cmpXtvCzf\u0024fF8UYUMr2 = DependencyProperty.RegisterAttached("IsRenderableSeriesInViewport", typeof (bool), typeof (dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd), new PropertyMetadata((object) false));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dz8BRRl9X\u00241GaoKzQFLQ\u003D\u003D = DependencyProperty.RegisterAttached("IsLastPointInViewport", typeof (bool), typeof (dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd), new PropertyMetadata((object) false));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzRuUiUh8wem3V = DependencyProperty.RegisterAttached("SeriesMarkerColor", typeof (Color), typeof (dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd), new PropertyMetadata((object) new Color()));

  public dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd()
  {
    this.\u0023\u003Dz3aV1iPcGyuhxDI4kpQEmSBg\u003D(false);
  }

  public static void SetIsSeriesValueModifierEnabled(UIElement _param0, bool _param1)
  {
    _param0.SetValue(dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd.\u0023\u003DzNnZwaoee8xziVPglsg\u003D\u003D, (object) _param1);
  }

  public static bool GetIsSeriesValueModifierEnabled(UIElement _param0)
  {
    return (bool) _param0.GetValue(dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd.\u0023\u003DzNnZwaoee8xziVPglsg\u003D\u003D);
  }

  public static bool GetIsRenderableSeriesInViewport(DependencyObject _param0)
  {
    return (bool) _param0.GetValue(dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd.\u0023\u003DzEf91H67L3cmpXtvCzf\u0024fF8UYUMr2);
  }

  public static void SetIsRenderableSeriesInViewport(DependencyObject _param0, bool _param1)
  {
    _param0.SetValue(dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd.\u0023\u003DzEf91H67L3cmpXtvCzf\u0024fF8UYUMr2, (object) _param1);
  }

  public static bool GetIsLastPointInViewport(DependencyObject _param0)
  {
    return (bool) _param0.GetValue(dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd.\u0023\u003Dz8BRRl9X\u00241GaoKzQFLQ\u003D\u003D);
  }

  public static void SetIsLastPointInViewport(DependencyObject _param0, bool _param1)
  {
    _param0.SetValue(dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd.\u0023\u003Dz8BRRl9X\u00241GaoKzQFLQ\u003D\u003D, (object) _param1);
  }

  public static Color GetSeriesMarkerColor(DependencyObject _param0)
  {
    return (Color) _param0.GetValue(dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd.\u0023\u003DzRuUiUh8wem3V);
  }

  public static void SetSeriesMarkerColor(DependencyObject _param0, Color _param1)
  {
    _param0.SetValue(dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd.\u0023\u003DzRuUiUh8wem3V, (object) _param1);
  }

  protected IDictionary<IRenderableSeries, IAnnotation> \u0023\u003Dz09yhBaeJjdAE()
  {
    return this.\u0023\u003DzJVKVgJSKleLL;
  }

  public Style AxisMarkerStyle
  {
    get
    {
      return (Style) this.GetValue(dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd.\u0023\u003Dzvp8Q5iyLlWID);
    }
    set
    {
      this.SetValue(dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd.\u0023\u003Dzvp8Q5iyLlWID, (object) value);
    }
  }

  public string YAxisId
  {
    get
    {
      return (string) this.GetValue(dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd.\u0023\u003DziAqnE8_\u0024SBDB);
    }
    set
    {
      this.SetValue(dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd.\u0023\u003DziAqnE8_\u0024SBDB, (object) value);
    }
  }

  public override void OnAttached()
  {
    base.OnAttached();
    if (this.ParentSurface is SciChartSurface parentSurface)
    {
      this.\u0023\u003DzMFucnUdhtZnJ = new PropertyChangeNotifier((DependencyObject) parentSurface, SciChartSurface.\u0023\u003Dzda5ZTgpF7nPj_QX8WWDrVmQ\u003D);
      this.\u0023\u003DzMFucnUdhtZnJ.ValueChanged += new Action(this.\u0023\u003Dz6kEcEuixivshQn\u0024YZtma0mabmk734AF5o\u00243V64I\u003D);
    }
    this.\u0023\u003Dz6kEcEuixivshQn\u0024YZtma0mabmk734AF5o\u00243V64I\u003D();
  }

  private void \u0023\u003Dz6kEcEuixivshQn\u0024YZtma0mabmk734AF5o\u00243V64I\u003D()
  {
    this.\u0023\u003DzDSeDnKxL3bvr();
    if (this.\u0023\u003Dzmhay2J9Ys\u0024BH != null)
      this.\u0023\u003Dzmhay2J9Ys\u0024BH.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.\u0023\u003DzlRPtJNE7UDOTZfXPK13GjY4\u003D);
    this.\u0023\u003Dzmhay2J9Ys\u0024BH = this.ParentSurface.get_RenderableSeries();
    if (this.\u0023\u003Dzmhay2J9Ys\u0024BH == null)
      return;
    this.\u0023\u003Dzmhay2J9Ys\u0024BH.CollectionChanged += new NotifyCollectionChangedEventHandler(this.\u0023\u003DzlRPtJNE7UDOTZfXPK13GjY4\u003D);
  }

  private void \u0023\u003DzlRPtJNE7UDOTZfXPK13GjY4\u003D(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    if (_param2.Action == NotifyCollectionChangedAction.Reset)
    {
      this.\u0023\u003DzDSeDnKxL3bvr();
    }
    else
    {
      if (this.ParentSurface == null || this.ParentSurface.get_Annotations() == null)
        return;
      _param2.OldItems.\u0023\u003Dz30RSSSygABj_<IRenderableSeries>(new Action<IRenderableSeries>(this.\u0023\u003Dzf3E4HwK7rBYk));
      _param2.NewItems.\u0023\u003Dz30RSSSygABj_<IRenderableSeries>(new Action<IRenderableSeries>(this.\u0023\u003DzkdGTy1I9DEFI));
    }
  }

  private void \u0023\u003DzDSeDnKxL3bvr()
  {
    if (this.ParentSurface == null || this.ParentSurface.get_Annotations() == null)
      return;
    this.\u0023\u003DzH9m5_dD9yxBB();
    if (!this.IsEnabled)
      return;
    this.ParentSurface.get_RenderableSeries().\u0023\u003Dz30RSSSygABj_<IRenderableSeries>(new Action<IRenderableSeries>(this.\u0023\u003DzkdGTy1I9DEFI));
  }

  private void \u0023\u003DzH9m5_dD9yxBB()
  {
    if (this.ParentSurface != null && this.ParentSurface.get_Annotations() != null)
      this.\u0023\u003DzJVKVgJSKleLL.\u0023\u003Dz30RSSSygABj_<KeyValuePair<IRenderableSeries, IAnnotation>>(new Action<KeyValuePair<IRenderableSeries, IAnnotation>>(this.\u0023\u003DzhCihYq\u0024JqXmwHUXOAGAdQ_w\u003D));
    this.\u0023\u003DzJVKVgJSKleLL.Clear();
  }

  private void \u0023\u003DzkdGTy1I9DEFI(
    IRenderableSeries _param1)
  {
    if (!(_param1.get_YAxisId() == this.YAxisId) || this.\u0023\u003DzJVKVgJSKleLL.ContainsKey(_param1))
      return;
    SeriesValueAxisMarkerAnnotation markerAnnotation1 = new SeriesValueAxisMarkerAnnotation();
    markerAnnotation1.Style = this.AxisMarkerStyle;
    markerAnnotation1.DataContext = (object) _param1;
    markerAnnotation1.Y1 = _param1.get_DataSeries() != null ? _param1.get_DataSeries().get_LatestYValue() : (IComparable) null;
    markerAnnotation1.XAxisId = _param1.get_XAxisId();
    markerAnnotation1.YAxisId = _param1.get_YAxisId();
    SeriesValueAxisMarkerAnnotation markerAnnotation2 = markerAnnotation1;
    this.ParentSurface.get_Annotations().Add((IAnnotation) markerAnnotation2);
    this.\u0023\u003DzJVKVgJSKleLL.Add(_param1, (IAnnotation) markerAnnotation2);
  }

  private void \u0023\u003Dzf3E4HwK7rBYk(
    IRenderableSeries _param1)
  {
    IAnnotation hhh93Q0DqkV5Sv90k;
    if (!this.\u0023\u003DzJVKVgJSKleLL.TryGetValue(_param1, out hhh93Q0DqkV5Sv90k))
      return;
    this.ParentSurface.get_Annotations().Remove(hhh93Q0DqkV5Sv90k);
    this.\u0023\u003DzJVKVgJSKleLL.Remove(_param1);
  }

  public override void OnDetached()
  {
    this.\u0023\u003DzH9m5_dD9yxBB();
    if (this.\u0023\u003Dzmhay2J9Ys\u0024BH != null)
    {
      this.\u0023\u003Dzmhay2J9Ys\u0024BH.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.\u0023\u003DzlRPtJNE7UDOTZfXPK13GjY4\u003D);
      this.\u0023\u003Dzmhay2J9Ys\u0024BH = (ObservableCollection<IRenderableSeries>) null;
    }
    if (this.\u0023\u003DzMFucnUdhtZnJ == null)
      return;
    this.\u0023\u003DzMFucnUdhtZnJ.ValueChanged -= new Action(this.\u0023\u003Dz6kEcEuixivshQn\u0024YZtma0mabmk734AF5o\u00243V64I\u003D);
    this.\u0023\u003DzMFucnUdhtZnJ = (PropertyChangeNotifier) null;
  }

  protected override void \u0023\u003DzCM2UQyuakisf()
  {
    base.\u0023\u003DzCM2UQyuakisf();
    this.\u0023\u003DzDSeDnKxL3bvr();
  }

  protected override void \u0023\u003Dzok6jmLaiH5ai(
    object _param1,
    NotifyCollectionChangedEventArgs _param2)
  {
    base.\u0023\u003Dzok6jmLaiH5ai(_param1, _param2);
    if (_param2.Action != NotifyCollectionChangedAction.Reset)
      return;
    this.\u0023\u003DzDSeDnKxL3bvr();
  }

  public override void \u0023\u003DzY1JcdEJm3Ryc(
    \u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAUdzyaQmAfsVzCpDWg08TVWt _param1)
  {
    base.\u0023\u003DzY1JcdEJm3Ryc(_param1);
    if (!this.IsEnabled)
      return;
    if (this.ParentSurface.get_RenderableSeries().Count<IRenderableSeries>(new Func<IRenderableSeries, bool>(this.\u0023\u003DzpxgQbv1sI7jsqDO4U7na\u0024\u0024I\u003D)) != this.\u0023\u003DzJVKVgJSKleLL.Count)
      this.\u0023\u003DzDSeDnKxL3bvr();
    foreach (IRenderableSeries key in this.ParentSurface.get_RenderableSeries().Where<IRenderableSeries>(new Func<IRenderableSeries, bool>(this.\u0023\u003DzXKvQXbvtr98ebQ_YJw\u003D\u003D)))
    {
      AxisMarkerAnnotation markerAnnotation = (AxisMarkerAnnotation) this.\u0023\u003DzJVKVgJSKleLL[key];
      IRange visibleRange = key.XAxis.VisibleRange;
      IndexRange  indicesRange = key.get_DataSeries().GetIndicesRange(visibleRange);
      \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D zldchDrVsrVyHh6WyiGy = \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D.\u0023\u003Dzz_6Dy9M\u003D;
      IComparable comparable1 = key.get_DataSeries().get_LatestYValue();
      bool isDefined = indicesRange.IsDefined;
      if (isDefined)
      {
        IComparable comparable2 = (IComparable) key.get_DataSeries().\u0023\u003DzwQnyySN6xaVC()[indicesRange.Max];
        if (!visibleRange.AsDoubleRange().\u0023\u003DzU0feMzXFLecQ((IComparable) comparable2.ToDouble()))
        {
          double num = this.ModifierSurface.ActualWidth - 1.0;
          Point point = this.XAxis.IsHorizontalAxis ? new Point(num, 0.0) : new Point(0.0, num);
          zldchDrVsrVyHh6WyiGy = key.\u0023\u003DznVLFa68vHPHy(point, true);
          comparable1 = zldchDrVsrVyHh6WyiGy.\u0023\u003Dzd9IAScWutAfJ();
        }
      }
      IComparable latestYvalue = key.get_DataSeries().get_LatestYValue();
      bool flag = latestYvalue != null && comparable1 != null && latestYvalue.CompareTo((object) comparable1) == 0;
      if (key is BaseRenderableSeries ls4St64EqzfbaEjd)
      {
        ls4St64EqzfbaEjd.SetValue(dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd.\u0023\u003DzEf91H67L3cmpXtvCzf\u0024fF8UYUMr2, (object) isDefined);
        ls4St64EqzfbaEjd.SetValue(dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd.\u0023\u003Dz8BRRl9X\u00241GaoKzQFLQ\u003D\u003D, (object) flag);
        Color color = !zldchDrVsrVyHh6WyiGy.\u0023\u003DzMeGSfVE\u003D() ? key.\u0023\u003Dz3_4\u0024b5dxRhLlXyFK3Q\u003D\u003D(zldchDrVsrVyHh6WyiGy) : ls4St64EqzfbaEjd.SeriesColor;
        ls4St64EqzfbaEjd.SetValue(dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd.\u0023\u003DzRuUiUh8wem3V, (object) color);
      }
      markerAnnotation.Y1 = comparable1;
      if (key.YAxis != null)
        markerAnnotation.FormattedValue = this.\u0023\u003DzMXsY2TplcAmh(key, comparable1);
    }
  }

  protected virtual string \u0023\u003DzMXsY2TplcAmh(
    IRenderableSeries _param1,
    IComparable _param2)
  {
    return ((dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd) _param1.YAxis).\u0023\u003DzRQVMnjXxoCTF(_param2, false);
  }

  private bool \u0023\u003DzXKvQXbvtr98ebQ_YJw\u003D\u003D(
    IRenderableSeries _param1)
  {
    return _param1.get_DataSeries() != null && _param1.XAxis != null && _param1.get_YAxisId() == this.YAxisId;
  }

  private static void \u0023\u003DzdXB_wQ4DNB7BU25L6g\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is dje_z933SLYLF5APGJGPC7WTXVPU3DU6UBBAQXSKCA3KUG553MXZ_ejd ubbaqxskcA3KuG553MxzEjd) || ubbaqxskcA3KuG553MxzEjd.ParentSurface == null)
      return;
    ubbaqxskcA3KuG553MxzEjd.\u0023\u003DzDSeDnKxL3bvr();
  }

  private void \u0023\u003DzhCihYq\u0024JqXmwHUXOAGAdQ_w\u003D(
    KeyValuePair<IRenderableSeries, IAnnotation> _param1)
  {
    this.ParentSurface.get_Annotations().Remove(_param1.Value);
  }

  private bool \u0023\u003DzpxgQbv1sI7jsqDO4U7na\u0024\u0024I\u003D(
    IRenderableSeries _param1)
  {
    return _param1.get_YAxisId() == this.YAxisId;
  }
}
