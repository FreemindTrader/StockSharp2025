// Decompiled with JetBrains decompiler
// Type: -.RubberBandXyZoomModifier
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

#nullable disable
namespace SciChart.Charting;

internal sealed class RubberBandXyZoomModifier : 
  ChartModifierBase
{
  
  public static readonly DependencyProperty \u0023\u003DzEiZKgbSC39dw = DependencyProperty.Register(nameof (IsAnimated), typeof (bool), typeof (RubberBandXyZoomModifier), new PropertyMetadata((object) true));
  
  public static readonly DependencyProperty \u0023\u003Dz3W_EWQ2DR449GpXhFw\u003D\u003D = DependencyProperty.Register(nameof (RubberBandFill), typeof (Brush), typeof (RubberBandXyZoomModifier), new PropertyMetadata((PropertyChangedCallback) null));
  
  public static readonly DependencyProperty \u0023\u003DzOtFah0x3lVGit4IaPQ\u003D\u003D = DependencyProperty.Register(nameof (RubberBandStroke), typeof (Brush), typeof (RubberBandXyZoomModifier), new PropertyMetadata((PropertyChangedCallback) null));
  
  public static readonly DependencyProperty \u0023\u003Dz1YLfO3AlYAqEWcDv5Z_lkpc\u003D = DependencyProperty.Register(nameof (RubberBandStrokeDashArray), typeof (DoubleCollection), typeof (RubberBandXyZoomModifier), new PropertyMetadata((PropertyChangedCallback) null));
  
  public static readonly DependencyProperty \u0023\u003DzfKMrdOp8PEWK = DependencyProperty.Register(nameof (IsXAxisOnly), typeof (bool), typeof (RubberBandXyZoomModifier), new PropertyMetadata((object) false));
  
  public static readonly DependencyProperty \u0023\u003DzNWOI1oh1IMgh = DependencyProperty.Register(nameof (ZoomExtentsY), typeof (bool), typeof (RubberBandXyZoomModifier), new PropertyMetadata((object) true));
  
  public static readonly DependencyProperty \u0023\u003Dzz8hz3gmofbMT = DependencyProperty.Register(nameof (MinDragSensitivity), typeof (double), typeof (RubberBandXyZoomModifier), new PropertyMetadata((object) 10.0));
  
  private \u0023\u003Dz7tyVhFVuY8D5V\u0024lqfWwb5UciZH9y99D\u0024o1ZNN\u0024yRVxOxiUGZk\u0024GoAaE\u003D \u0023\u003DztZ_Bn6fiXvhgUvyZicnn4\u00244\u003D;
  
  private Point \u0023\u003DzhcsKvLfw_p5c;
  
  private Point \u0023\u003Dzm1SIm_yjNDHz;
  
  private bool \u0023\u003DzqGwbHdeZ8yMA;
  
  private Shape \u0023\u003DztHc2gS4\u003D;

  public RubberBandXyZoomModifier()
  {
    this.DefaultStyleKey = (object) typeof (RubberBandXyZoomModifier);
  }

  public bool IsAnimated
  {
    get
    {
      return (bool) this.GetValue(RubberBandXyZoomModifier.\u0023\u003DzEiZKgbSC39dw);
    }
    set
    {
      this.SetValue(RubberBandXyZoomModifier.\u0023\u003DzEiZKgbSC39dw, (object) value);
    }
  }

  public Brush RubberBandFill
  {
    get
    {
      return (Brush) this.GetValue(RubberBandXyZoomModifier.\u0023\u003Dz3W_EWQ2DR449GpXhFw\u003D\u003D);
    }
    set
    {
      this.SetValue(RubberBandXyZoomModifier.\u0023\u003Dz3W_EWQ2DR449GpXhFw\u003D\u003D, (object) value);
    }
  }

  public Brush RubberBandStroke
  {
    get
    {
      return (Brush) this.GetValue(RubberBandXyZoomModifier.\u0023\u003DzOtFah0x3lVGit4IaPQ\u003D\u003D);
    }
    set
    {
      this.SetValue(RubberBandXyZoomModifier.\u0023\u003DzOtFah0x3lVGit4IaPQ\u003D\u003D, (object) value);
    }
  }

  public DoubleCollection RubberBandStrokeDashArray
  {
    get
    {
      return (DoubleCollection) this.GetValue(RubberBandXyZoomModifier.\u0023\u003Dz1YLfO3AlYAqEWcDv5Z_lkpc\u003D);
    }
    set
    {
      this.SetValue(RubberBandXyZoomModifier.\u0023\u003Dz1YLfO3AlYAqEWcDv5Z_lkpc\u003D, (object) value);
    }
  }

  public bool IsXAxisOnly
  {
    get
    {
      return (bool) this.GetValue(RubberBandXyZoomModifier.\u0023\u003DzfKMrdOp8PEWK);
    }
    set
    {
      this.SetValue(RubberBandXyZoomModifier.\u0023\u003DzfKMrdOp8PEWK, (object) value);
    }
  }

  public bool ZoomExtentsY
  {
    get
    {
      return (bool) this.GetValue(RubberBandXyZoomModifier.\u0023\u003DzNWOI1oh1IMgh);
    }
    set
    {
      this.SetValue(RubberBandXyZoomModifier.\u0023\u003DzNWOI1oh1IMgh, (object) value);
    }
  }

  public double MinDragSensitivity
  {
    get
    {
      return (double) this.GetValue(RubberBandXyZoomModifier.\u0023\u003Dzz8hz3gmofbMT);
    }
    set
    {
      this.SetValue(RubberBandXyZoomModifier.\u0023\u003Dzz8hz3gmofbMT, (object) value);
    }
  }

  public bool IsDragging => this.\u0023\u003DzqGwbHdeZ8yMA;

  public override void OnAttached()
  {
    base.OnAttached();
    this.\u0023\u003DzIew7YFe_5nHVEkzlrQ\u003D\u003D();
  }

  public override void OnDetached()
  {
    base.OnDetached();
    this.\u0023\u003DzIew7YFe_5nHVEkzlrQ\u003D\u003D();
  }

  public override void OnModifierMouseDown(
    ModifierMouseArgs _param1)
  {
    base.OnModifierMouseDown(_param1);
    if (this.\u0023\u003DzqGwbHdeZ8yMA || !this.MatchesExecuteOn(_param1.MouseButtons(), this.ExecuteOn))
      return;
    _param1.Handled(true);
    if (!(_param1.\u0023\u003DzRo7rSFU\u003D() is IChartModifier) || !this.ModifierSurface.GetBoundsRelativeTo((IHitTestable) this.\u0023\u003Dzwc4Gzka23TGB()).Contains(_param1.MousePoint()))
      return;
    if (_param1.IsMaster())
      this.ModifierSurface.CaptureMouse();
    this.\u0023\u003DzhcsKvLfw_p5c = this.GetPointRelativeTo(_param1.MousePoint(), (IHitTestable) this.ModifierSurface);
    this.\u0023\u003DztZ_Bn6fiXvhgUvyZicnn4\u00244\u003D = this.\u0023\u003DzsuPwLoadtv7J();
    this.\u0023\u003DztHc2gS4\u003D = this.\u0023\u003DztZ_Bn6fiXvhgUvyZicnn4\u00244\u003D.\u0023\u003Dz5G3eIQ8\u003D(this.RubberBandFill, this.RubberBandStroke, this.RubberBandStrokeDashArray);
    this.\u0023\u003DztZ_Bn6fiXvhgUvyZicnn4\u00244\u003D.\u0023\u003Dzz11HY86OhYRe(this.IsXAxisOnly, this.\u0023\u003DzhcsKvLfw_p5c, this.\u0023\u003DzhcsKvLfw_p5c);
    this.ModifierSurface.Children().Add((UIElement) this.\u0023\u003DztHc2gS4\u003D);
    this.\u0023\u003DzqGwbHdeZ8yMA = true;
  }

  private \u0023\u003Dz7tyVhFVuY8D5V\u0024lqfWwb5UciZH9y99D\u0024o1ZNN\u0024yRVxOxiUGZk\u0024GoAaE\u003D \u0023\u003DzsuPwLoadtv7J()
  {
    \u0023\u003Dz7tyVhFVuY8D5V\u0024lqfWwb5UciZH9y99D\u0024o1ZNN\u0024yRVxOxiUGZk\u0024GoAaE\u003D yRvxOxiUgZkGoAaE;
    if (this.XAxis != null && this.XAxis.get_IsPolarAxis())
    {
      if (!(this.\u0023\u003DztZ_Bn6fiXvhgUvyZicnn4\u00244\u003D is \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhivTIORxM4pFaWkWD6T7Je4n45z8o20NaGXPXIGD_ je4n45z8o20NaGxpxigd))
        je4n45z8o20NaGxpxigd = new \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhivTIORxM4pFaWkWD6T7Je4n45z8o20NaGXPXIGD_((IChartModifier) this);
      yRvxOxiUgZkGoAaE = (\u0023\u003Dz7tyVhFVuY8D5V\u0024lqfWwb5UciZH9y99D\u0024o1ZNN\u0024yRVxOxiUGZk\u0024GoAaE\u003D) je4n45z8o20NaGxpxigd;
    }
    else
    {
      if (!(this.\u0023\u003DztZ_Bn6fiXvhgUvyZicnn4\u00244\u003D is \u0023\u003DzSseiGdgwJmJ1pkmz7CEFfwuk7mZpVT219h_NwMrKXT5sYdAawRHD8Yx_rVjVyiLHpg\u003D\u003D rhD8YxRVjVyiLhpg))
        rhD8YxRVjVyiLhpg = new \u0023\u003DzSseiGdgwJmJ1pkmz7CEFfwuk7mZpVT219h_NwMrKXT5sYdAawRHD8Yx_rVjVyiLHpg\u003D\u003D((IChartModifier) this);
      yRvxOxiUgZkGoAaE = (\u0023\u003Dz7tyVhFVuY8D5V\u0024lqfWwb5UciZH9y99D\u0024o1ZNN\u0024yRVxOxiUGZk\u0024GoAaE\u003D) rhD8YxRVjVyiLhpg;
    }
    return yRvxOxiUgZkGoAaE;
  }

  public override void OnModifierMouseMove(
    ModifierMouseArgs _param1)
  {
    if (!this.\u0023\u003DzqGwbHdeZ8yMA)
      return;
    base.OnModifierMouseMove(_param1);
    _param1.Handled(true);
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("{0} MouseMove: x={1}, y={2}", new object[3]
    {
      (object) ((object) this).GetType().Name,
      (object) _param1.MousePoint().X,
      (object) _param1.MousePoint().Y
    });
    this.\u0023\u003DztZ_Bn6fiXvhgUvyZicnn4\u00244\u003D.\u0023\u003DzuPC9WaE\u003D(this.IsXAxisOnly, this.\u0023\u003DzhcsKvLfw_p5c, this.GetPointRelativeTo(_param1.MousePoint(), (IHitTestable) this.ModifierSurface));
  }

  public override void OnModifierMouseUp(
    ModifierMouseArgs _param1)
  {
    if (!this.\u0023\u003DzqGwbHdeZ8yMA)
      return;
    base.OnModifierMouseUp(_param1);
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("{0} MouseUp: x={1}, y={2}", new object[3]
    {
      (object) ((object) this).GetType().Name,
      (object) _param1.MousePoint().X,
      (object) _param1.MousePoint().Y
    });
    Point point1 = this.GetPointRelativeTo(_param1.MousePoint(), (IHitTestable) this.ModifierSurface);
    this.\u0023\u003Dzm1SIm_yjNDHz = this.\u0023\u003DztZ_Bn6fiXvhgUvyZicnn4\u00244\u003D.\u0023\u003DzuPC9WaE\u003D(this.IsXAxisOnly, this.\u0023\u003DzhcsKvLfw_p5c, point1);
    \u0023\u003DzUJpBz2W8IzAtBIqVtQXHB99xo8DgCb_3ha_wTIg\u003D b99xo8DgCb3haWTig = this.Services.GetService<\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSXWkz2jl56XJoPdfqB4\u003D>().\u0023\u003DzhGnS3f5TTzO8();
    Point point2 = b99xo8DgCb3haWTig.\u0023\u003Dz8miGAzg\u003D(this.\u0023\u003DzhcsKvLfw_p5c);
    Point point3 = b99xo8DgCb3haWTig.\u0023\u003Dz8miGAzg\u003D(this.\u0023\u003Dzm1SIm_yjNDHz);
    Point point4 = b99xo8DgCb3haWTig.\u0023\u003Dz8miGAzg\u003D(point1);
    if (this.\u0023\u003DztZ_Bn6fiXvhgUvyZicnn4\u00244\u003D.\u0023\u003DzBBczPQeswUUn(point2, point4) > this.MinDragSensitivity)
    {
      this.\u0023\u003DzIjNc90j5mMD8(point2, point3);
      _param1.Handled(true);
    }
    else
      this.\u0023\u003DzIew7YFe_5nHVEkzlrQ\u003D\u003D();
    this.\u0023\u003DzqGwbHdeZ8yMA = false;
    if (!_param1.IsMaster())
      return;
    this.ModifierSurface.ReleaseMouseCapture();
  }

  private void \u0023\u003DzIew7YFe_5nHVEkzlrQ\u003D\u003D()
  {
    if (this.ModifierSurface == null || this.\u0023\u003DztHc2gS4\u003D == null)
      return;
    this.ModifierSurface.Children().Remove((UIElement) this.\u0023\u003DztHc2gS4\u003D);
    this.\u0023\u003DztHc2gS4\u003D = (Shape) null;
    this.\u0023\u003DzqGwbHdeZ8yMA = false;
  }

  internal void \u0023\u003DzIjNc90j5mMD8(Point _param1, Point _param2)
  {
    this.\u0023\u003DzIew7YFe_5nHVEkzlrQ\u003D\u003D();
    if (Math.Abs(_param1.X - _param2.X) < double.Epsilon || Math.Abs(_param1.Y - _param2.Y) < double.Epsilon || this.XAxes.\u0023\u003DzCCMM80zDpO6N<IAxis>() || this.YAxes.\u0023\u003DzCCMM80zDpO6N<IAxis>())
      return;
    Rect rect = new Rect(_param1, _param2);
    using (this.ParentSurface.SuspendUpdates())
    {
      Dictionary<string, IRange> dictionary = new Dictionary<string, IRange>();
      foreach (IAxis xax in this.XAxes)
      {
        int num1 = xax.IsHorizontalAxis ? 1 : 0;
        bool? isHorizontalAxis = this.XAxis?.IsHorizontalAxis;
        int num2 = isHorizontalAxis.GetValueOrDefault() ? 1 : 0;
        if (num1 == num2 & isHorizontalAxis.HasValue)
        {
          IRange abyLt9clZggmJsWhw = this.\u0023\u003Dzz5GxOuxHkE8Y(xax, rect);
          if (abyLt9clZggmJsWhw != null && !abyLt9clZggmJsWhw.IsZero)
            dictionary.Add(xax.Id, abyLt9clZggmJsWhw);
        }
      }
      if (this.IsXAxisOnly)
      {
        if (!this.ZoomExtentsY)
          return;
        foreach (IAxis yax in this.YAxes)
          yax.\u0023\u003DzB4GssLEPDUHfRR_NuUKVKvc\u003D(yax.\u0023\u003DzIvBsiY\u0024C5tRlcFKGo7\u002430Ac\u003D((IDictionary<string, IRange>) dictionary), this.IsAnimated ? TimeSpan.FromMilliseconds(500.0) : TimeSpan.Zero);
      }
      else
      {
        foreach (IAxis yax in this.YAxes)
          this.\u0023\u003Dzz5GxOuxHkE8Y(yax, rect);
      }
    }
  }

  private IRange \u0023\u003Dzz5GxOuxHkE8Y(
    IAxis _param1,
    Rect _param2)
  {
    double num1 = _param1.IsHorizontalAxis ? _param2.Left : _param2.Bottom;
    double num2 = _param1.IsHorizontalAxis ? _param2.Right : _param2.Top;
    return this.\u0023\u003Dzz5GxOuxHkE8Y(_param1, num1, num2);
  }

  internal IRange \u0023\u003Dzz5GxOuxHkE8Y(
    IAxis _param1,
    double _param2,
    double _param3)
  {
    if (_param1 == null)
      return (IRange) null;
    \u0023\u003DzFDK4fEILkMRswIjIg1\u0024y3MZjK6kEswW_XYNXMkMl\u0024H7TxZaHyXLiZ9wXJZ_c txZaHyXliZ9wXjzC = _param1.\u0023\u003DzLwX32hiDT0l2MBUaLIQGQLie6ie0();
    if (txZaHyXliZ9wXjzC == null)
      return (IRange) null;
    IRange abyLt9clZggmJsWhw = txZaHyXliZ9wXjzC.\u0023\u003DzQdR08KQ\u003D(_param1.VisibleRange, _param2, _param3 - 1.0);
    _param1.\u0023\u003DzB4GssLEPDUHfRR_NuUKVKvc\u003D(abyLt9clZggmJsWhw, this.IsAnimated ? TimeSpan.FromMilliseconds(500.0) : TimeSpan.Zero);
    return abyLt9clZggmJsWhw;
  }

  private void \u0023\u003DziqNXXkGKHwlT(double _param1, double _param2, Rect _param3)
  {
  }

  internal \u0023\u003Dz7tyVhFVuY8D5V\u0024lqfWwb5UciZH9y99D\u0024o1ZNN\u0024yRVxOxiUGZk\u0024GoAaE\u003D \u0023\u003Dz1Ie6c4NvtQ\u00248()
  {
    return this.\u0023\u003DztZ_Bn6fiXvhgUvyZicnn4\u00244\u003D;
  }
}
