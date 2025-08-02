// Decompiled with JetBrains decompiler
// Type: -.BasePointMarker
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Xaml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Charting;

public abstract class BasePointMarker : 
  ContentControl,
  \u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT
{
  
  public static readonly DependencyProperty \u0023\u003DzNGe3htdX6rpV = DependencyProperty.Register(nameof (PointMarkerTemplate), typeof (ControlTemplate), typeof (BasePointMarker), new PropertyMetadata((object) null, new PropertyChangedCallback(BasePointMarker.SomeClass34343383.SomeMethond0343.\u0023\u003DzKNLry2NAzTE0YP2LWzHy77o\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzxglZwT0\u003D = DependencyProperty.Register(nameof (Stroke), typeof (Color), typeof (BasePointMarker), new PropertyMetadata((object) new Color(), new PropertyChangedCallback(BasePointMarker.SomeClass34343383.SomeMethond0343.\u0023\u003DzACYYWwoqJLWh\u0024F6GbuiAdRM\u003D)));
  
  public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(nameof (StrokeThickness), typeof (double), typeof (BasePointMarker), new PropertyMetadata((object) 1.0, new PropertyChangedCallback(BasePointMarker.SomeClass34343383.SomeMethond0343.\u0023\u003DzYNHwQ174F89tEKHpFedHL4k\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzuzLf_8s\u003D = DependencyProperty.Register(nameof (Fill), typeof (Color), typeof (BasePointMarker), new PropertyMetadata((object) new Color(), new PropertyChangedCallback(BasePointMarker.SomeClass34343383.SomeMethond0343.\u0023\u003Dzvp_fMzphxDgUj7M8713t5tY\u003D)));
  
  public static readonly DependencyProperty AntiAliasingProperty = DependencyProperty.Register(nameof (AntiAliasing), typeof (bool), typeof (BasePointMarker), new PropertyMetadata((object) true, new PropertyChangedCallback(BasePointMarker.SomeClass34343383.SomeMethond0343.\u0023\u003DzwwnfoUeY2t4J2Aquog1ti\u0024w\u003D)));
  
  private \u0023\u003Dzh5FljKv\u0024Q_lDTADyTGyZRTX9mDWGkJVnFV25iog\u003D<\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J> \u0023\u003DzFoYj4ei_SpD\u0024;
  
  private \u0023\u003Dzh5FljKv\u0024Q_lDTADyTGyZRTX9mDWGkJVnFV25iog\u003D<IBrush2D> \u0023\u003DzdLqflxMU6QH8;
  
  private Type \u0023\u003DzMi02w5aVhHynsfiJiA\u003D\u003D;
  
  private Ecng.Xaml.PropertyChangeNotifier \u0023\u003DzubSLsKkd2jzj;
  
  private Ecng.Xaml.PropertyChangeNotifier \u0023\u003Dzl3E1ABn8TjRR;
  
  private static bool \u0023\u003DzKRRDe7lSf19J3xc8SEr2Ml9bd54lopKnRkf6wFM\u003D;

  protected BasePointMarker()
  {
    this.DefaultStyleKey = (object) ((object) this).GetType();
    this.\u0023\u003DzubSLsKkd2jzj = new Ecng.Xaml.PropertyChangeNotifier((DependencyObject) this, FrameworkElement.WidthProperty);
    this.\u0023\u003Dzl3E1ABn8TjRR = new Ecng.Xaml.PropertyChangeNotifier((DependencyObject) this, FrameworkElement.HeightProperty);
    this.\u0023\u003DzubSLsKkd2jzj.ValueChanged += new Action(this.\u0023\u003DzGDG1SrCuVxZHIZFp\u00248qj2tc\u003D);
    this.\u0023\u003Dzl3E1ABn8TjRR.ValueChanged += new Action(this.\u0023\u003Dz9iWzgKFxwwYVJdwP50lpX9c\u003D);
  }

  public static bool \u0023\u003DzQ13yAPTKb1E11CW0VICNGPOoMM38()
  {
    return BasePointMarker.\u0023\u003DzKRRDe7lSf19J3xc8SEr2Ml9bd54lopKnRkf6wFM\u003D;
  }

  public static void \u0023\u003DzdovIJ7SvqmRsYxB6f8fHLcqhVX0f(bool _param0)
  {
    BasePointMarker.\u0023\u003DzKRRDe7lSf19J3xc8SEr2Ml9bd54lopKnRkf6wFM\u003D = _param0;
  }

  public ControlTemplate PointMarkerTemplate
  {
    get
    {
      return (ControlTemplate) this.GetValue(BasePointMarker.\u0023\u003DzNGe3htdX6rpV);
    }
    set
    {
      this.SetValue(BasePointMarker.\u0023\u003DzNGe3htdX6rpV, (object) value);
    }
  }

  public Color Stroke
  {
    get
    {
      return (Color) this.GetValue(BasePointMarker.\u0023\u003DzxglZwT0\u003D);
    }
    set
    {
      this.SetValue(BasePointMarker.\u0023\u003DzxglZwT0\u003D, (object) value);
    }
  }

  public Color Fill
  {
    get
    {
      return (Color) this.GetValue(BasePointMarker.\u0023\u003DzuzLf_8s\u003D);
    }
    set
    {
      this.SetValue(BasePointMarker.\u0023\u003DzuzLf_8s\u003D, (object) value);
    }
  }

  public double StrokeThickness
  {
    get
    {
      return (double) this.GetValue(BasePointMarker.StrokeThicknessProperty);
    }
    set
    {
      this.SetValue(BasePointMarker.StrokeThicknessProperty, (object) value);
    }
  }

  public bool AntiAliasing
  {
    get
    {
      return (bool) this.GetValue(BasePointMarker.AntiAliasingProperty);
    }
    set
    {
      this.SetValue(BasePointMarker.AntiAliasingProperty, (object) value);
    }
  }

  public virtual void Draw(
    IRenderContext2D _param1,
    IEnumerable<Point> _param2)
  {
    this.\u0023\u003DzdL613chSNlLB(_param1, _param2, this.\u0023\u003DzFoYj4ei_SpD\u0024.\u0023\u003DzQAdOJsjJeOwf(), this.\u0023\u003DzdLqflxMU6QH8.\u0023\u003DzQAdOJsjJeOwf());
  }

  public virtual void Draw(
    IRenderContext2D _param1,
    double _param2,
    double _param3,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param4,
    IBrush2D _param5)
  {
    this.\u0023\u003DzdL613chSNlLB(_param1, _param2, _param3, this.\u0023\u003Dzc8S9rSE\u003D(_param4), this.\u0023\u003DzNryPIU0\u003D(_param5));
  }

  private \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J \u0023\u003Dzc8S9rSE\u003D(
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param1)
  {
    return _param1 ?? this.\u0023\u003DzFoYj4ei_SpD\u0024.\u0023\u003DzQAdOJsjJeOwf();
  }

  private IBrush2D \u0023\u003DzNryPIU0\u003D(
    IBrush2D _param1)
  {
    return _param1 ?? this.\u0023\u003DzdLqflxMU6QH8.\u0023\u003DzQAdOJsjJeOwf();
  }

  public virtual void \u0023\u003DzTuM3X1E\u003D()
  {
    this.\u0023\u003DzMi02w5aVhHynsfiJiA\u003D\u003D = (Type) null;
    if (this.\u0023\u003DzdLqflxMU6QH8 != null)
    {
      this.\u0023\u003DzdLqflxMU6QH8.Dispose();
      this.\u0023\u003DzdLqflxMU6QH8 = (\u0023\u003Dzh5FljKv\u0024Q_lDTADyTGyZRTX9mDWGkJVnFV25iog\u003D<IBrush2D>) null;
    }
    if (this.\u0023\u003DzFoYj4ei_SpD\u0024 == null)
      return;
    this.\u0023\u003DzFoYj4ei_SpD\u0024.Dispose();
    this.\u0023\u003DzFoYj4ei_SpD\u0024 = (\u0023\u003Dzh5FljKv\u0024Q_lDTADyTGyZRTX9mDWGkJVnFV25iog\u003D<\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J>) null;
  }

  protected abstract void \u0023\u003DzdL613chSNlLB(
    IRenderContext2D _param1,
    IEnumerable<Point> _param2,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param3,
    IBrush2D _param4);

  protected abstract void \u0023\u003DzdL613chSNlLB(
    IRenderContext2D _param1,
    double _param2,
    double _param3,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param4,
    IBrush2D _param5);

  protected virtual void OnPropertyChanged(
    DependencyObject _param1,
    DependencyPropertyChangedEventArgs _param2)
  {
    ((BasePointMarker) _param1).\u0023\u003DzTuM3X1E\u003D();
    ((BasePointMarker) _param1).\u0023\u003Dzo1moqKMmSMi6();
  }

  private void \u0023\u003Dzo1moqKMmSMi6()
  {
    if (!(this.DataContext is BaseRenderableSeries dataContext))
      return;
    dataContext.OnInvalidateParentSurface();
  }

  private void \u0023\u003DzHns9YfFfnZla(
    IRenderContext2D _param1)
  {
    if (!(_param1.GetType() != this.\u0023\u003DzMi02w5aVhHynsfiJiA\u003D\u003D) && this.\u0023\u003DzFoYj4ei_SpD\u0024 != null && this.\u0023\u003DzdLqflxMU6QH8 != null)
      return;
    this.\u0023\u003DzTuM3X1E\u003D();
    this.\u0023\u003DzdLqflxMU6QH8 = new \u0023\u003Dzh5FljKv\u0024Q_lDTADyTGyZRTX9mDWGkJVnFV25iog\u003D<IBrush2D>(_param1.\u0023\u003Dze8WyDhI\u003D(this.Fill, 1.0, new bool?()));
    this.\u0023\u003DzFoYj4ei_SpD\u0024 = new \u0023\u003Dzh5FljKv\u0024Q_lDTADyTGyZRTX9mDWGkJVnFV25iog\u003D<\u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J>(_param1.\u0023\u003DzL3In9ls\u003D(this.Stroke, this.AntiAliasing, (float) this.StrokeThickness, 1.0, (double[]) null, PenLineCap.Round));
    this.\u0023\u003DzMi02w5aVhHynsfiJiA\u003D\u003D = _param1.GetType();
  }

  public virtual void \u0023\u003Dz7ZSU06M\u003D(
    IRenderContext2D _param1,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param2,
    IBrush2D _param3)
  {
    _param1.\u0023\u003DzjyCoorxnWjneJ7dCR\u0024Tiiog\u003D(true);
    this.\u0023\u003DzHns9YfFfnZla(_param1);
  }

  public virtual void \u0023\u003DzBNsE20w\u003D(
    IRenderContext2D _param1)
  {
    _param1.\u0023\u003DzjyCoorxnWjneJ7dCR\u0024Tiiog\u003D(false);
  }

  double \u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT.\u0023\u003Dzq8lPttT4Qpp4TSswk_CaTXYaIYE5L9RmPjgx7zUCnKHWczzViA\u003D\u003D()
  {
    return this.Width;
  }

  void \u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT.\u0023\u003DzlV2bk__vwLQKcXFMwtbJEZ05bo9UUG86JgHRsJVb2fttPBgC1g\u003D\u003D(
    double _param1)
  {
    this.Width = _param1;
  }

  double \u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT.\u0023\u003DzDQ9laCDp0uqTj5f6eNPqK6lS6gvJtIQrfPWVI_dM\u00245v\u0024Olm0KA\u003D\u003D()
  {
    return this.Height;
  }

  void \u0023\u003DzTirsw8K0cFwomstKh6_6HW1ki13vvK4WxOGoljkHYInT.\u0023\u003Dz0KO1aCqgrzg4VgQHs26xbbW_2MHjmDKg9nVh\u0024ZzZ0noOJEFdxA\u003D\u003D(
    double _param1)
  {
    this.Height = _param1;
  }

  private void \u0023\u003DzGDG1SrCuVxZHIZFp\u00248qj2tc\u003D()
  {
    this.OnPropertyChanged((DependencyObject) this, new DependencyPropertyChangedEventArgs());
  }

  private void \u0023\u003Dz9iWzgKFxwwYVJdwP50lpX9c\u003D()
  {
    this.OnPropertyChanged((DependencyObject) this, new DependencyPropertyChangedEventArgs());
  }

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly BasePointMarker.SomeClass34343383 SomeMethond0343 = new BasePointMarker.SomeClass34343383();

    public void \u0023\u003DzKNLry2NAzTE0YP2LWzHy77o\u003D(
      DependencyObject _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      ((BasePointMarker) _param1).OnPropertyChanged(_param1, _param2);
    }

    public void \u0023\u003DzACYYWwoqJLWh\u0024F6GbuiAdRM\u003D(
      DependencyObject _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      ((BasePointMarker) _param1).OnPropertyChanged(_param1, _param2);
    }

    public void \u0023\u003DzYNHwQ174F89tEKHpFedHL4k\u003D(
      DependencyObject _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      ((BasePointMarker) _param1).OnPropertyChanged(_param1, _param2);
    }

    public void \u0023\u003Dzvp_fMzphxDgUj7M8713t5tY\u003D(
      DependencyObject _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      ((BasePointMarker) _param1).OnPropertyChanged(_param1, _param2);
    }

    public void \u0023\u003DzwwnfoUeY2t4J2Aquog1ti\u0024w\u003D(
      DependencyObject _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      ((BasePointMarker) _param1).OnPropertyChanged(_param1, _param2);
    }
  }
}
