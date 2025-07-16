// Decompiled with JetBrains decompiler
// Type: -.MouseWheelZoomModifier
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Windows;

#nullable disable
namespace StockSharp.Charting;

public sealed class MouseWheelZoomModifier : 
  RelativeZoomModifierBase
{
  
  public static readonly DependencyProperty \u0023\u003Dzp3T0E8s\u003D = DependencyProperty.Register(nameof (ActionType), typeof (ActionType), typeof (MouseWheelZoomModifier), new PropertyMetadata((object) ActionType.Zoom, new PropertyChangedCallback(MouseWheelZoomModifier.SomeClass34343383.SomeMethond0343.\u0023\u003Dzm27O\u0024m4PgoEaaJOSFwKn\u0024So\u003D)));
  
  private Action<Point, double> \u0023\u003DzE5Q0sUQO_NoQ;

  public MouseWheelZoomModifier()
  {
    this.GrowFactor = 0.1;
    this.\u0023\u003DzE5Q0sUQO_NoQ = new Action<Point, double>(this.\u0023\u003DzIjNc90j5mMD8);
  }

  public ActionType ActionType
  {
    get
    {
      return (ActionType) this.GetValue(MouseWheelZoomModifier.\u0023\u003Dzp3T0E8s\u003D);
    }
    set
    {
      this.SetValue(MouseWheelZoomModifier.\u0023\u003Dzp3T0E8s\u003D, (object) value);
    }
  }

  private void \u0023\u003DzIjNc90j5mMD8(Point _param1, double _param2)
  {
    this.\u0023\u003DzIjNc90j5mMD8(_param1, _param2, _param2);
  }

  public override void \u0023\u003DzQTINWhMByBmJ(
    ModifierMouseArgs _param1)
  {
    base.\u0023\u003DzQTINWhMByBmJ(_param1);
    _param1.Handled(true);
    using (this.ParentSurface.SuspendUpdates())
    {
      double num = (double) -_param1.\u0023\u003DzDuDuL4DDV5GL() / 120.0;
      XyDirection xyDirection = this.XyDirection;
      ActionType actionType = this.ActionType;
      if (_param1.Modifier() != MouseModifier.None)
      {
        this.SetCurrentValue(MouseWheelZoomModifier.\u0023\u003Dzp3T0E8s\u003D, (object) ActionType.Pan);
        if (_param1.Modifier() == MouseModifier.Ctrl)
          this.SetCurrentValue(RelativeZoomModifierBase.\u0023\u003DzcN3lc2NJhvnw, (object) XyDirection.YDirection);
        else if (_param1.Modifier() == MouseModifier.Shift)
          this.SetCurrentValue(RelativeZoomModifierBase.\u0023\u003DzcN3lc2NJhvnw, (object) XyDirection.XDirection);
      }
      this.\u0023\u003DzE5Q0sUQO_NoQ(this.GetPointRelativeTo(_param1.MousePoint(), (IHitTestable) this.ModifierSurface), num);
      this.SetCurrentValue(RelativeZoomModifierBase.\u0023\u003DzcN3lc2NJhvnw, (object) xyDirection);
      this.SetCurrentValue(MouseWheelZoomModifier.\u0023\u003Dzp3T0E8s\u003D, (object) actionType);
    }
  }

  private void \u0023\u003Dz6fc78SIV6E\u0024a(Point _param1, double _param2)
  {
    if (this.XyDirection == XyDirection.YDirection || this.XyDirection == XyDirection.XYDirection)
    {
      foreach (IAxis yax in this.YAxes)
      {
        double num1 = this.\u0023\u003DzeuxrJCE00Q0n(yax);
        double num2 = _param2 * this.GrowFactor * num1;
        yax.\u0023\u003DzquLnA5Y\u003D(num2, ClipMode.None);
      }
      \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("Growing YRange: {0}", new object[1]
      {
        (object) _param2
      });
    }
    if (this.XyDirection != XyDirection.XDirection && this.XyDirection != XyDirection.XYDirection)
      return;
    foreach (IAxis xax in this.XAxes)
    {
      int num3 = xax.IsHorizontalAxis ? 1 : 0;
      bool? isHorizontalAxis = this.XAxis?.IsHorizontalAxis;
      int num4 = isHorizontalAxis.GetValueOrDefault() ? 1 : 0;
      if (num3 == num4 & isHorizontalAxis.HasValue)
      {
        double num5 = this.\u0023\u003DzeuxrJCE00Q0n(xax);
        double num6 = -_param2 * this.GrowFactor * num5;
        xax.\u0023\u003DzquLnA5Y\u003D(num6, ClipMode.None);
      }
      else
        break;
    }
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("Growing XRange: {0}", new object[1]
    {
      (object) (int) _param2
    });
  }

  private double \u0023\u003DzeuxrJCE00Q0n(
    IAxis _param1)
  {
    double num = _param1.IsHorizontalAxis ? _param1.Width : _param1.Height;
    if (Math.Abs(num) < double.Epsilon && this.ParentSurface != null && this.ParentSurface.get_RenderSurface() != null)
      num = _param1.IsHorizontalAxis ? this.ParentSurface.get_RenderSurface().ActualWidth : this.ParentSurface.get_RenderSurface().ActualHeight;
    if (_param1.get_IsPolarAxis())
      num /= 2.0;
    return num;
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly MouseWheelZoomModifier.SomeClass34343383 SomeMethond0343 = new MouseWheelZoomModifier.SomeClass34343383();

    public void \u0023\u003Dzm27O\u0024m4PgoEaaJOSFwKn\u0024So\u003D(
      DependencyObject _param1,
      DependencyPropertyChangedEventArgs _param2)
    {
      MouseWheelZoomModifier kufjwuuvR4YbN3Ejd = _param1 as MouseWheelZoomModifier;
      ActionType newValue = (ActionType) _param2.NewValue;
      if (kufjwuuvR4YbN3Ejd == null)
        return;
      kufjwuuvR4YbN3Ejd.\u0023\u003DzE5Q0sUQO_NoQ = newValue == ActionType.Pan ? new Action<Point, double>(kufjwuuvR4YbN3Ejd.\u0023\u003Dz6fc78SIV6E\u0024a) : new Action<Point, double>(kufjwuuvR4YbN3Ejd.\u0023\u003DzIjNc90j5mMD8);
    }
  }
}
