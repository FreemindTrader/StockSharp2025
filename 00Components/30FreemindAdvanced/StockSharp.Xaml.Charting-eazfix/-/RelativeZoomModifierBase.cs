// Decompiled with JetBrains decompiler
// Type: -.RelativeZoomModifierBase
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

#nullable disable
namespace StockSharp.Charting;

public abstract class RelativeZoomModifierBase : 
  ChartModifierBase
{
  
  public static readonly DependencyProperty \u0023\u003DzcN3lc2NJhvnw = DependencyProperty.Register(nameof (XyDirection), typeof (XyDirection), typeof (RelativeZoomModifierBase), new PropertyMetadata((object) XyDirection.XYDirection));
  
  private double \u0023\u003DzgTKlgLAfrwWL;

  public XyDirection XyDirection
  {
    get
    {
      return (XyDirection) this.GetValue(RelativeZoomModifierBase.\u0023\u003DzcN3lc2NJhvnw);
    }
    set
    {
      this.SetValue(RelativeZoomModifierBase.\u0023\u003DzcN3lc2NJhvnw, (object) value);
    }
  }

  public double GrowFactor
  {
    get => this.\u0023\u003DzgTKlgLAfrwWL;
    set
    {
      \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzlTskcr4\u003D((IComparable) value, nameof (GrowFactor)).\u0023\u003Dzc5Mtj4NgZLWC((IComparable) 0.0);
      this.\u0023\u003DzgTKlgLAfrwWL = value;
    }
  }

  protected virtual void \u0023\u003DzIjNc90j5mMD8(Point _param1, double _param2, double _param3)
  {
    if (this.XyDirection == XyDirection.YDirection || this.XyDirection == XyDirection.XYDirection)
      this.\u0023\u003DzoI7ONAZ_gOY2(this.GrowFactor * _param3, _param1, this.YAxes, "Growing YRange: {0}");
    if (this.XyDirection != XyDirection.XDirection && this.XyDirection != XyDirection.XYDirection)
      return;
    this.\u0023\u003DzoI7ONAZ_gOY2(this.GrowFactor * _param2, _param1, this.XAxes, "Growing XRange: {0}");
  }

  private void \u0023\u003DzoI7ONAZ_gOY2(
    double _param1,
    Point _param2,
    IEnumerable<IAxis> _param3,
    string _param4)
  {
    foreach (IAxis dynWmoFzgH4RlWB0lB in _param3)
      this.GrowBy(_param2, dynWmoFzgH4RlWB0lB, _param1);
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D(_param4, new object[1]
    {
      (object) _param1
    });
  }

  protected void GrowBy(
    Point _param1,
    IAxis _param2,
    double _param3)
  {
    double num1 = this.\u0023\u003Dz1f1oGj4PehI\u0024(_param2);
    double num2 = _param2.IsHorizontalAxis ? _param1.X : num1 - _param1.Y;
    double num3 = num2 / num1 * _param3;
    double num4 = (1.0 - num2 / num1) * _param3;
    bool flag = _param2.IsHorizontalAxis && !_param2.\u0023\u003DzFrVmckt\u0024NpG6() || !_param2.IsHorizontalAxis && _param2.\u0023\u003DzFrVmckt\u0024NpG6();
    if ((!flag || _param2.get_FlipCoordinates() ? (flag ? 0 : (_param2.get_FlipCoordinates() ? 1 : 0)) : 1) != 0)
      NumberUtil.Swap(ref num3, ref num4);
    _param2.\u0023\u003Dz40HnRQM\u003D(num3, num4);
  }

  private double \u0023\u003Dz1f1oGj4PehI\u0024(
    IAxis _param1)
  {
    double num = _param1.IsHorizontalAxis ? _param1.Width : _param1.Height;
    SciChartSurface parentSurface = _param1.get_ParentSurface() as SciChartSurface;
    if (_param1.get_Visibility() == Visibility.Collapsed && parentSurface != null)
      num = _param1.IsHorizontalAxis ? parentSurface.ActualWidth : parentSurface.ActualHeight;
    return num;
  }
}
