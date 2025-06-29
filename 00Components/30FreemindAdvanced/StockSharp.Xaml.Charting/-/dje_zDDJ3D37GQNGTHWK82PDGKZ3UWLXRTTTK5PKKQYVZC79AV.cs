// Decompiled with JetBrains decompiler
// Type: -.dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd : 
  ContentControl
{
  
  public static readonly DependencyProperty \u0023\u003DzYjQ2xl9o319L = DependencyProperty.Register("", typeof (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D), typeof (dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd.\u0023\u003Dz4MpbQOLMpYtswY0tvA\u003D\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzkqE1t18ru2zs = DependencyProperty.Register("", typeof (double), typeof (dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd), new PropertyMetadata((object) 0.0));
  
  public static readonly DependencyProperty \u0023\u003DzKn3156UzRCt\u0024AII4BUa9Wfw\u003D = DependencyProperty.Register("", typeof (double), typeof (dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd), new PropertyMetadata((object) 0.0));
  
  public static readonly DependencyProperty \u0023\u003DzlxwHaE7e3X98kFSxiA\u003D\u003D = DependencyProperty.Register("", typeof (\u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IV6_IiM70jMp5uwjMXR4ajr_<double, double>), typeof (dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd), new PropertyMetadata((object) null));
  
  public static readonly DependencyProperty \u0023\u003Dzj5GKwE9GOTZLXrb8\u0024MTNNy3saxHt = DependencyProperty.Register("", typeof (\u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IV6_IiM70jMp5uwjMXR4ajr_<double, double>), typeof (dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd), new PropertyMetadata((object) null));
  
  public static readonly DependencyProperty \u0023\u003Dzrh_06bSTsdjtRRWPZW671SE\u003D = DependencyProperty.Register("", typeof (int), typeof (dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd), new PropertyMetadata((object) 20));
  
  public static readonly DependencyProperty \u0023\u003Dzi_FN3ig480bq = DependencyProperty.Register("", typeof (int), typeof (dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd), new PropertyMetadata((object) 0));
  
  public static readonly DependencyProperty \u0023\u003DzT_iaUxXXWqlulr95Hw\u003D\u003D = DependencyProperty.Register("", typeof (Visibility), typeof (dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd), new PropertyMetadata((object) Visibility.Visible));
  
  private Stopwatch \u0023\u003DzWewHyEvM_sOk;
  
  private double \u0023\u003Dz7DSowRM8YI8lUCePfEYzKln_4DSk;
  
  private double \u0023\u003DzCc9QDFMMWYXAfyk0nA\u003D\u003D;

  public dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd()
  {
    this.DefaultStyleKey = (object) typeof (dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd);
  }

  public Visibility ChartsVisibility
  {
    get
    {
      return (Visibility) this.GetValue(dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd.\u0023\u003DzT_iaUxXXWqlulr95Hw\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd.\u0023\u003DzT_iaUxXXWqlulr95Hw\u003D\u003D, (object) value);
    }
  }

  public int TotalPointCount
  {
    get
    {
      return (int) this.GetValue(dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd.\u0023\u003Dzi_FN3ig480bq);
    }
    set
    {
      this.SetValue(dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd.\u0023\u003Dzi_FN3ig480bq, (object) value);
    }
  }

  public int SmoothingWindowSize
  {
    get
    {
      return (int) this.GetValue(dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd.\u0023\u003Dzrh_06bSTsdjtRRWPZW671SE\u003D);
    }
    set
    {
      this.SetValue(dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd.\u0023\u003Dzrh_06bSTsdjtRRWPZW671SE\u003D, (object) value);
    }
  }

  public \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IV6_IiM70jMp5uwjMXR4ajr_<double, double> UltrachartFpsSeries
  {
    get
    {
      return (\u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IV6_IiM70jMp5uwjMXR4ajr_<double, double>) this.GetValue(dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd.\u0023\u003Dzj5GKwE9GOTZLXrb8\u0024MTNNy3saxHt);
    }
    set
    {
      this.SetValue(dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd.\u0023\u003Dzj5GKwE9GOTZLXrb8\u0024MTNNy3saxHt, (object) value);
    }
  }

  public \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IV6_IiM70jMp5uwjMXR4ajr_<double, double> WpfFpsSeries
  {
    get
    {
      return (\u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IV6_IiM70jMp5uwjMXR4ajr_<double, double>) this.GetValue(dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd.\u0023\u003DzlxwHaE7e3X98kFSxiA\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd.\u0023\u003DzlxwHaE7e3X98kFSxiA\u003D\u003D, (object) value);
    }
  }

  public double WpfFps
  {
    get
    {
      return (double) this.GetValue(dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd.\u0023\u003DzkqE1t18ru2zs);
    }
    set
    {
      this.SetValue(dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd.\u0023\u003DzkqE1t18ru2zs, (object) value);
    }
  }

  public \u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D TargetSurface
  {
    get
    {
      return (\u0023\u003DzVWRskdf0yEAwtZYFZxzKpeavUg1Y5II8u0KOV3jCAMd\u0024YpfetQ\u003D\u003D) this.GetValue(dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd.\u0023\u003DzYjQ2xl9o319L);
    }
    set
    {
      this.SetValue(dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd.\u0023\u003DzYjQ2xl9o319L, (object) value);
    }
  }

  public double UltrachartFps
  {
    get
    {
      return (double) this.GetValue(dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd.\u0023\u003DzKn3156UzRCt\u0024AII4BUa9Wfw\u003D);
    }
    set
    {
      this.SetValue(dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd.\u0023\u003DzKn3156UzRCt\u0024AII4BUa9Wfw\u003D, (object) value);
    }
  }

  public override void OnApplyTemplate() => base.OnApplyTemplate();

  private static void \u0023\u003Dz4MpbQOLMpYtswY0tvA\u003D\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd rf7U8Nh6NmgmaEjd = (dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd) _param0;
    if (!(_param1.NewValue is dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd newValue))
      return;
    newValue.Loaded -= new RoutedEventHandler(rf7U8Nh6NmgmaEjd.\u0023\u003DzSwq\u0024W16DpTIPh6J9Mw3YqhI\u003D);
    newValue.Loaded += new RoutedEventHandler(rf7U8Nh6NmgmaEjd.\u0023\u003DzSwq\u0024W16DpTIPh6J9Mw3YqhI\u003D);
    newValue.Unloaded -= new RoutedEventHandler(rf7U8Nh6NmgmaEjd.\u0023\u003Dz90WLXdWl_vULnMk6kNFZNC4\u003D);
    newValue.Unloaded += new RoutedEventHandler(rf7U8Nh6NmgmaEjd.\u0023\u003Dz90WLXdWl_vULnMk6kNFZNC4\u003D);
    if (!newValue.IsLoaded)
      return;
    rf7U8Nh6NmgmaEjd.\u0023\u003DzSwq\u0024W16DpTIPh6J9Mw3YqhI\u003D((object) newValue, (RoutedEventArgs) null);
  }

  private void \u0023\u003Dz90WLXdWl_vULnMk6kNFZNC4\u003D(object _param1, RoutedEventArgs _param2)
  {
    dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd elwvdvgwnmJ5AjuaEjd = _param1 as dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd;
    CompositionTarget.Rendering -= new EventHandler(this.\u0023\u003DzzYypnyJA76yR);
    EventHandler<EventArgs> eventHandler = new EventHandler<EventArgs>(this.\u0023\u003DzAYDiRNgmrWdYtBKmdijQiGQ\u003D);
    elwvdvgwnmJ5AjuaEjd.\u0023\u003DzrRRdxqQwy\u0024OJ(eventHandler);
    this.\u0023\u003DzWewHyEvM_sOk.Stop();
  }

  private void \u0023\u003DzSwq\u0024W16DpTIPh6J9Mw3YqhI\u003D(
    object _param1,
    RoutedEventArgs _param2)
  {
    dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd elwvdvgwnmJ5AjuaEjd = _param1 as dje_zT5LWWY2ES5P78EADY3KXQ8WJ3WLKKMBZV5NL8KM7QST7ELWVDVGWNMJ5AJUA_ejd;
    this.\u0023\u003DzWewHyEvM_sOk = Stopwatch.StartNew();
    this.\u0023\u003Dz7DSowRM8YI8lUCePfEYzKln_4DSk = 0.0;
    this.\u0023\u003DzCc9QDFMMWYXAfyk0nA\u003D\u003D = 0.0;
    \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IV6_IiM70jMp5uwjMXR4ajr_<double, double> m70jMp5uwjMxR4ajr1 = new \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IV6_IiM70jMp5uwjMXR4ajr_<double, double>();
    m70jMp5uwjMxR4ajr1.FifoCapacity = new int?(this.SmoothingWindowSize);
    this.UltrachartFpsSeries = m70jMp5uwjMxR4ajr1;
    \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IV6_IiM70jMp5uwjMXR4ajr_<double, double> m70jMp5uwjMxR4ajr2 = new \u0023\u003DzdPAQRlt3VWWvvKbSPLZ0IV6_IiM70jMp5uwjMXR4ajr_<double, double>();
    m70jMp5uwjMxR4ajr2.FifoCapacity = new int?(this.SmoothingWindowSize);
    this.WpfFpsSeries = m70jMp5uwjMxR4ajr2;
    this.TotalPointCount = 0;
    CompositionTarget.Rendering -= new EventHandler(this.\u0023\u003DzzYypnyJA76yR);
    CompositionTarget.Rendering += new EventHandler(this.\u0023\u003DzzYypnyJA76yR);
    EventHandler<EventArgs> eventHandler = new EventHandler<EventArgs>(this.\u0023\u003DzAYDiRNgmrWdYtBKmdijQiGQ\u003D);
    elwvdvgwnmJ5AjuaEjd.\u0023\u003DzKPHSi1vgK\u0024Fx(eventHandler);
  }

  private void \u0023\u003DzAYDiRNgmrWdYtBKmdijQiGQ\u003D(object _param1, EventArgs _param2)
  {
    double num = 1000.0 / ((double) this.\u0023\u003DzWewHyEvM_sOk.ElapsedMilliseconds - this.\u0023\u003Dz7DSowRM8YI8lUCePfEYzKln_4DSk);
    this.\u0023\u003Dz7DSowRM8YI8lUCePfEYzKln_4DSk = (double) this.\u0023\u003DzWewHyEvM_sOk.ElapsedMilliseconds;
    this.UltrachartFpsSeries.Append(this.UltrachartFpsSeries.Count == 0 ? 0.0 : (double) (int) (this.UltrachartFpsSeries.XValues.Last<double>() + 1.0), num);
    this.UltrachartFps = this.UltrachartFpsSeries.YValues.Sum() / (double) this.UltrachartFpsSeries.Count;
    this.TotalPointCount = this.TargetSurface.get_RenderableSeries().Sum<IRenderableSeries>(dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzoANln2qVkbS_vrdROw\u003D\u003D ?? (dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzoANln2qVkbS_vrdROw\u003D\u003D = new Func<IRenderableSeries, int>(dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzElyHXfPhEvYEiDkhXXnNS27lyPhdoMOxZwPR79c\u003D)));
  }

  private void \u0023\u003DzzYypnyJA76yR(object _param1, EventArgs _param2)
  {
    double d = 1000.0 / ((double) this.\u0023\u003DzWewHyEvM_sOk.ElapsedMilliseconds - this.\u0023\u003DzCc9QDFMMWYXAfyk0nA\u003D\u003D);
    if (double.IsInfinity(d))
      return;
    this.\u0023\u003DzCc9QDFMMWYXAfyk0nA\u003D\u003D = (double) this.\u0023\u003DzWewHyEvM_sOk.ElapsedMilliseconds;
    this.WpfFpsSeries.Append(this.WpfFpsSeries.Count == 0 ? 0.0 : (double) (int) (this.WpfFpsSeries.XValues.Last<double>() + 1.0), d);
    this.WpfFps = this.WpfFpsSeries.YValues.Sum() / (double) this.WpfFpsSeries.Count;
  }

  [Serializable]
  private sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new dje_zDDJ3D37GQNGTHWK82PDGKZ3UWLXRTTTK5PKKQYVZC79AVF7RF7U8NH6NMGMA_ejd.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<IRenderableSeries, int> \u0023\u003DzoANln2qVkbS_vrdROw\u003D\u003D;

    internal int \u0023\u003DzElyHXfPhEvYEiDkhXXnNS27lyPhdoMOxZwPR79c\u003D(
      IRenderableSeries _param1)
    {
      return _param1.get_DataSeries() == null ? 0 : _param1.get_DataSeries().get_Count();
    }
  }
}
