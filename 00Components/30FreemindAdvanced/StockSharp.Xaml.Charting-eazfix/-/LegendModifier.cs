// Decompiled with JetBrains decompiler
// Type: -.LegendModifier
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace StockSharp.Charting;

public sealed class LegendModifier : 
  InspectSeriesModifierBase
{
  
  public static readonly DependencyProperty \u0023\u003DzfMw6oHlwmSrk = DependencyProperty.Register(nameof (LegendData), typeof (\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSv1H1ORLBnhjlg\u003D\u003D), typeof (LegendModifier), new PropertyMetadata((PropertyChangedCallback) null));
  
  public static readonly DependencyProperty \u0023\u003Dzo\u0024LE3hIlM1nJ = DependencyProperty.Register(nameof (GetLegendDataFor), typeof (\u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxOGwev9Q6t\u00245QdLx8qQ\u003D), typeof (LegendModifier), new PropertyMetadata((object) \u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxOGwev9Q6t\u00245QdLx8qQ\u003D.AllSeries, new PropertyChangedCallback(LegendModifier.SomeClass34343383.SomeMethond0343.\u0023\u003DziRnnm2fvgSGDVB\u0024GmpSBXaY\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzjgUJSc8xvO\u00243 = DependencyProperty.Register(nameof (LegendPlacement), typeof (LegendPlacement), typeof (LegendModifier), new PropertyMetadata((object) LegendPlacement.Inside));
  
  public static readonly DependencyProperty \u0023\u003DziGoZCH2CaVRc = DependencyProperty.Register(nameof (LegendItemTemplate), typeof (DataTemplate), typeof (LegendModifier), new PropertyMetadata((PropertyChangedCallback) null));
  
  public static readonly DependencyProperty \u0023\u003DzXMV_skc\u003D = DependencyProperty.Register(nameof (Orientation), typeof (Orientation), typeof (LegendModifier), new PropertyMetadata((object) Orientation.Vertical));
  
  public static readonly DependencyProperty \u0023\u003DzGEpRUSytcG_B = DependencyProperty.Register(nameof (ShowSeriesMarkers), typeof (bool), typeof (LegendModifier), new PropertyMetadata((object) true));
  
  public static readonly DependencyProperty \u0023\u003DzR_CKkzERxHn9SGnaiYrSxEE\u003D = DependencyProperty.Register(nameof (ShowVisibilityCheckboxes), typeof (bool), typeof (LegendModifier), new PropertyMetadata((object) true));
  
  public static readonly DependencyProperty ShowLegendProperty = DependencyProperty.Register(nameof (ShowLegend), typeof (bool), typeof (LegendModifier), new PropertyMetadata((object) false, new PropertyChangedCallback(LegendModifier.\u0023\u003Dzg1\u0024yrcyUCpiA)));
  
  public static readonly DependencyProperty \u0023\u003DzbE0mDiZUf80q = DependencyProperty.Register(nameof (LegendTemplate), typeof (ControlTemplate), typeof (LegendModifier), new PropertyMetadata(new PropertyChangedCallback(LegendModifier.\u0023\u003DzdGdWMjyO6eRA)));
  
  private FrameworkElement \u0023\u003DzrA5Sb8Rte_9L;

  public LegendModifier()
  {
    this.DefaultStyleKey = (object) typeof (LegendModifier);
    this.SetCurrentValue(InspectSeriesModifierBase.\u0023\u003DzE7h5hUE7Vu4g, (object) new \u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSv1H1ORLBnhjlg\u003D\u003D());
  }

  public bool ShowVisibilityCheckboxes
  {
    get
    {
      return (bool) this.GetValue(LegendModifier.\u0023\u003DzR_CKkzERxHn9SGnaiYrSxEE\u003D);
    }
    set
    {
      this.SetValue(LegendModifier.\u0023\u003DzR_CKkzERxHn9SGnaiYrSxEE\u003D, (object) value);
    }
  }

  public bool ShowSeriesMarkers
  {
    get
    {
      return (bool) this.GetValue(LegendModifier.\u0023\u003DzGEpRUSytcG_B);
    }
    set
    {
      this.SetValue(LegendModifier.\u0023\u003DzGEpRUSytcG_B, (object) value);
    }
  }

  public LegendPlacement LegendPlacement
  {
    get
    {
      return (LegendPlacement) this.GetValue(LegendModifier.\u0023\u003DzjgUJSc8xvO\u00243);
    }
    set
    {
      this.SetValue(LegendModifier.\u0023\u003DzjgUJSc8xvO\u00243, (object) value);
    }
  }

  public DataTemplate LegendItemTemplate
  {
    get
    {
      return (DataTemplate) this.GetValue(LegendModifier.\u0023\u003DziGoZCH2CaVRc);
    }
    set
    {
      this.SetValue(LegendModifier.\u0023\u003DziGoZCH2CaVRc, (object) value);
    }
  }

  public \u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSv1H1ORLBnhjlg\u003D\u003D LegendData
  {
    get
    {
      return (\u0023\u003DzzD2ECOV\u00240uL7JoS8n7YFSv1H1ORLBnhjlg\u003D\u003D) this.GetValue(LegendModifier.\u0023\u003DzfMw6oHlwmSrk);
    }
    set
    {
      this.SetValue(LegendModifier.\u0023\u003DzfMw6oHlwmSrk, (object) value);
    }
  }

  public Orientation Orientation
  {
    get
    {
      return (Orientation) this.GetValue(LegendModifier.\u0023\u003DzXMV_skc\u003D);
    }
    set
    {
      this.SetValue(LegendModifier.\u0023\u003DzXMV_skc\u003D, (object) value);
    }
  }

  public bool ShowLegend
  {
    get
    {
      return (bool) this.GetValue(LegendModifier.ShowLegendProperty);
    }
    set
    {
      this.SetValue(LegendModifier.ShowLegendProperty, (object) value);
    }
  }

  public ControlTemplate LegendTemplate
  {
    get
    {
      return (ControlTemplate) this.GetValue(LegendModifier.\u0023\u003DzbE0mDiZUf80q);
    }
    set
    {
      this.SetValue(LegendModifier.\u0023\u003DzbE0mDiZUf80q, (object) value);
    }
  }

  public \u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxOGwev9Q6t\u00245QdLx8qQ\u003D GetLegendDataFor
  {
    get
    {
      return (\u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxOGwev9Q6t\u00245QdLx8qQ\u003D) this.GetValue(LegendModifier.\u0023\u003Dzo\u0024LE3hIlM1nJ);
    }
    set
    {
      this.SetValue(LegendModifier.\u0023\u003Dzo\u0024LE3hIlM1nJ, (object) value);
    }
  }

  public override void OnAttached()
  {
    if (this.ShowLegend)
      this.ParentSurface.\u0023\u003Dzwc4Gzka23TGB().\u0023\u003DzH0osWQkV_Y8_((object) this.\u0023\u003DzrA5Sb8Rte_9L, -1);
    base.OnAttached();
  }

  public override void OnDetached()
  {
    this.ParentSurface.\u0023\u003Dzwc4Gzka23TGB().\u0023\u003DziYdJ\u00246cCiBha((object) this.\u0023\u003DzrA5Sb8Rte_9L);
    base.OnDetached();
  }

  protected override void \u0023\u003DzleRWWIS9Sb_X()
  {
  }

  protected override void \u0023\u003Dz_wtru8oSZoY9(Point _param1)
  {
  }

  protected override void \u0023\u003Dz1z_ZexRGbAiN91rPDA\u003D\u003D(Point _param1)
  {
  }

  public override void OnModifierMouseMove(
    ModifierMouseArgs _param1)
  {
  }

  public override void \u0023\u003DzY1JcdEJm3Ryc(
    \u0023\u003Dzd9zvJtNUzU9n1Kn1rKAcAUdzyaQmAfsVzCpDWg08TVWt _param1)
  {
    base.\u0023\u003DzY1JcdEJm3Ryc(_param1);
    this.\u0023\u003DzVyNtPuM\u003D();
  }

  public virtual void \u0023\u003DzVyNtPuM\u003D()
  {
    if (!this.IsEnabled || !this.IsAttached || this.ParentSurface == null || this.ParentSurface.get_RenderableSeries() == null)
      return;
    LegendModifier.\u0023\u003DzSomy1CqId_K1JNYi\u0024vmePXs\u003D cqIdK1JnYiVmePxs = new LegendModifier.\u0023\u003DzSomy1CqId_K1JNYi\u0024vmePXs\u003D();
    ObservableCollection<SeriesInfo> source = this.\u0023\u003DzZZbJdAS6fDJ\u0024(this.ParentSurface.get_RenderableSeries().Where<IRenderableSeries>(new Func<IRenderableSeries, bool>(this.\u0023\u003DzaBvGZQmHUOsn)));
    ObservableCollection<SeriesInfo> seriesInfo = this.SeriesData.SeriesInfo;
    cqIdK1JnYiVmePxs.\u0023\u003DzcntTewCERpVdST0tmR5oDSM\u003D = source.Select<SeriesInfo, IRenderableSeries>(LegendModifier.SomeClass34343383.\u0023\u003Dz9HSFgt5cVDGouicyKg\u003D\u003D ?? (LegendModifier.SomeClass34343383.\u0023\u003Dz9HSFgt5cVDGouicyKg\u003D\u003D = new Func<SeriesInfo, IRenderableSeries>(LegendModifier.SomeClass34343383.SomeMethond0343.\u0023\u003Dz\u0024gAhhp6z9Gkiqek7qvjGZrk\u003D))).ToArray<IRenderableSeries>();
    seriesInfo.\u0023\u003DzmFyFyI4\u003D<SeriesInfo>(new Predicate<SeriesInfo>(cqIdK1JnYiVmePxs.\u0023\u003DzO1RhVl4gz8WF6_dF6w\u003D\u003D));
    foreach (SeriesInfo vdj8C0KctI6r27Gg1 in (Collection<SeriesInfo>) source)
    {
      LegendModifier.\u0023\u003DzwvYXkKjOuvyd5aJBEw4pWME\u003D ouvyd5aJbEw4pWme = new LegendModifier.\u0023\u003DzwvYXkKjOuvyd5aJBEw4pWME\u003D();
      ouvyd5aJbEw4pWme.\u0023\u003DzpZB0nPaJCOgn = vdj8C0KctI6r27Gg1;
      SeriesInfo vdj8C0KctI6r27Gg2 = seriesInfo.FirstOrDefault<SeriesInfo>(new Func<SeriesInfo, bool>(ouvyd5aJbEw4pWme.\u0023\u003Dz5ZRAyNj\u0024d5\u0024CpBRWyQ\u003D\u003D));
      if (vdj8C0KctI6r27Gg2 != null)
        LegendModifier.UpdateSeries(vdj8C0KctI6r27Gg2, ouvyd5aJbEw4pWme.\u0023\u003DzpZB0nPaJCOgn);
      else
        seriesInfo.Add(ouvyd5aJbEw4pWme.\u0023\u003DzpZB0nPaJCOgn);
    }
  }

  private new bool \u0023\u003DzaBvGZQmHUOsn(
    IRenderableSeries _param1)
  {
    return _param1 != null && this.\u0023\u003DzQciw5LQGN0mc(_param1) && _param1.get_DataSeries() != null;
  }

  private bool \u0023\u003DzQciw5LQGN0mc(
    IRenderableSeries _param1)
  {
    if (this.GetLegendDataFor == \u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxOGwev9Q6t\u00245QdLx8qQ\u003D.AllSeries || _param1.IsVisible && this.GetLegendDataFor == \u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxOGwev9Q6t\u00245QdLx8qQ\u003D.AllVisibleSeries || _param1.get_IsSelected() && this.GetLegendDataFor == \u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxOGwev9Q6t\u00245QdLx8qQ\u003D.SelectedSeries)
      return true;
    return !_param1.get_IsSelected() && this.GetLegendDataFor == \u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxOGwev9Q6t\u00245QdLx8qQ\u003D.UnselectedSeries;
  }

  protected virtual ObservableCollection<SeriesInfo> \u0023\u003DzZZbJdAS6fDJ\u0024(
    IEnumerable<IRenderableSeries> _param1)
  {
    ObservableCollection<SeriesInfo> observableCollection = new ObservableCollection<SeriesInfo>();
    if (_param1 != null)
    {
      foreach (IRenderableSeries s1JolYrWoYpqmQ6ug in _param1)
      {
        SeriesInfo vdj8C0KctI6r27Gg = s1JolYrWoYpqmQ6ug.\u0023\u003DzZZbJdAS6fDJ\u0024(s1JolYrWoYpqmQ6ug.\u0023\u003DzjuB\u0024Pa8\u003D(new Point(this.ModifierSurface.ActualWidth, 0.0), false));
        observableCollection.Add(vdj8C0KctI6r27Gg);
      }
    }
    return observableCollection;
  }

  private static void UpdateSeries(
    SeriesInfo _param0,
    SeriesInfo _param1)
  {
    _param0.DataSeriesIndex = _param1.DataSeriesIndex;
    _param0.DataSeriesType = _param1.DataSeriesType;
    _param0.IsHit = _param1.IsHit;
    _param0.SeriesColor = _param1.SeriesColor;
    _param0.SeriesName = _param1.SeriesName;
    _param0.Value = _param1.Value;
    _param0.XValue = _param1.XValue;
    _param0.YValue = _param1.YValue;
    _param0.XyCoordinate = _param1.XyCoordinate;
  }

  private static void \u0023\u003Dzg1\u0024yrcyUCpiA(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is LegendModifier w3M9Vc8LppfqqEjd) || w3M9Vc8LppfqqEjd.ParentSurface == null || w3M9Vc8LppfqqEjd.LegendTemplate == null)
      return;
    if (w3M9Vc8LppfqqEjd.ShowLegend)
    {
      w3M9Vc8LppfqqEjd.\u0023\u003DzrA5Sb8Rte_9L.DataContext = (object) w3M9Vc8LppfqqEjd;
      w3M9Vc8LppfqqEjd.ParentSurface.\u0023\u003Dzwc4Gzka23TGB().\u0023\u003DzH0osWQkV_Y8_((object) w3M9Vc8LppfqqEjd.\u0023\u003DzrA5Sb8Rte_9L, -1);
    }
    else
      w3M9Vc8LppfqqEjd.ParentSurface.\u0023\u003Dzwc4Gzka23TGB().\u0023\u003DziYdJ\u00246cCiBha((object) w3M9Vc8LppfqqEjd.\u0023\u003DzrA5Sb8Rte_9L);
  }

  private static void \u0023\u003DzdGdWMjyO6eRA(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is LegendModifier w3M9Vc8LppfqqEjd) || _param1.NewValue == null)
      return;
    w3M9Vc8LppfqqEjd.\u0023\u003DzrA5Sb8Rte_9L = w3M9Vc8LppfqqEjd.\u0023\u003DzrA5Sb8Rte_9L ?? (FrameworkElement) new LegendPlaceholder();
    w3M9Vc8LppfqqEjd.\u0023\u003DzrA5Sb8Rte_9L.DataContext = (object) w3M9Vc8LppfqqEjd;
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly LegendModifier.SomeClass34343383 SomeMethond0343 = new LegendModifier.SomeClass34343383();
    public static Func<SeriesInfo, IRenderableSeries> \u0023\u003Dz9HSFgt5cVDGouicyKg\u003D\u003D;

    public IRenderableSeries \u0023\u003Dz\u0024gAhhp6z9Gkiqek7qvjGZrk\u003D(
      SeriesInfo _param1)
    {
      return _param1.RenderableSeries;
    }

    public void \u0023\u003DziRnnm2fvgSGDVB\u0024GmpSBXaY\u003D(
      DependencyObject _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      ((LegendModifier) _param1).\u0023\u003DzVyNtPuM\u003D();
    }
  }

  private sealed class \u0023\u003DzSomy1CqId_K1JNYi\u0024vmePXs\u003D
  {
    public IRenderableSeries[] \u0023\u003DzcntTewCERpVdST0tmR5oDSM\u003D;

    public bool \u0023\u003DzO1RhVl4gz8WF6_dF6w\u003D\u003D(
      SeriesInfo _param1)
    {
      return !((IEnumerable<IRenderableSeries>) this.\u0023\u003DzcntTewCERpVdST0tmR5oDSM\u003D).Contains<IRenderableSeries>(_param1.RenderableSeries);
    }
  }

  private sealed class \u0023\u003DzwvYXkKjOuvyd5aJBEw4pWME\u003D
  {
    public SeriesInfo \u0023\u003DzpZB0nPaJCOgn;

    public bool \u0023\u003Dz5ZRAyNj\u0024d5\u0024CpBRWyQ\u003D\u003D(
      SeriesInfo _param1)
    {
      return _param1.RenderableSeries.Equals((object) this.\u0023\u003DzpZB0nPaJCOgn.RenderableSeries);
    }
  }
}
