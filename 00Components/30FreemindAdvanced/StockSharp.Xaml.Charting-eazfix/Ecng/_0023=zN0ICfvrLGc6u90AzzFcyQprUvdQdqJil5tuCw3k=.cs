// Decompiled with JetBrains decompiler
// Type: #=zN0ICfvrLGc6u90AzzFcyQprUvdQdqJil5tuCw3k=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Windows;

#nullable disable
public static class \u0023\u003DzN0ICfvrLGc6u90AzzFcyQprUvdQdqJil5tuCw3k\u003D
{
  public static bool \u0023\u003DzB4GssLEPDUHfRR_NuUKVKvc\u003D(
    this IAxis _param0,
    IRange _param1,
    TimeSpan _param2)
  {
    if (_param0.get_AutoRange() == dje_zYGCX6K4J87LQZ9RSX9K3KJFMDBT5XCBUXSB93QCTSXU83FDJRBTJV_ejd.Always || !_param0.\u0023\u003Dz2OKbyRBzRCBL(_param1) || _param1 == null || _param1.Equals((object) _param0.VisibleRange))
      return false;
    if (_param2.\u0023\u003DzhilcjqQ\u003D())
      _param0.VisibleRange = _param1;
    else
      _param0.\u0023\u003DzwrnVUenT8f7v7FlPviBwd40\u003D(_param1, _param2);
    return true;
  }

  public static void \u0023\u003Dz07\u0024lhqBRmUuR(
    this IAxis _param0,
    FrameworkElement _param1,
    Point _param2)
  {
    _param1.SetValue(AxisCanvas.\u0023\u003DzZpWLYz8\u003D, (object) double.NaN);
    _param1.SetValue(AxisCanvas.\u0023\u003DzLd8ENL0vP3HT, (object) double.NaN);
    if (_param0.IsHorizontalAxis)
    {
      DependencyProperty dependencyProperty = _param0.get_AxisAlignment() == dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Bottom ? AxisCanvas.\u0023\u003DzZpWLYz8\u003D : AxisCanvas.\u0023\u003DzasJeVgQ\u003D;
      if (_param1.ActualHeight >= _param0.Height)
        dependencyProperty = dependencyProperty == AxisCanvas.\u0023\u003DzZpWLYz8\u003D ? AxisCanvas.\u0023\u003DzasJeVgQ\u003D : AxisCanvas.\u0023\u003DzZpWLYz8\u003D;
      _param1.SetValue(dependencyProperty, (object) 0.0);
    }
    else
      AxisCanvas.SetCenterTop((UIElement) _param1, _param2.Y);
  }

  public static void \u0023\u003DzXOlF_vImljp4(
    this IAxis _param0,
    FrameworkElement _param1,
    Point _param2)
  {
    _param1.SetValue(AxisCanvas.\u0023\u003DztX3bWaM\u003D, (object) double.NaN);
    _param1.SetValue(AxisCanvas.\u0023\u003DzHEgPKfijwe68, (object) double.NaN);
    if (_param0.IsHorizontalAxis)
    {
      AxisCanvas.SetCenterLeft((UIElement) _param1, _param2.X);
    }
    else
    {
      DependencyProperty dependencyProperty = _param0.get_AxisAlignment() == dje_zCT38HR56LBNAEYCND4R6F7KK29QLC68GPV3JWM42DEMYDMPA2K68Q_ejd.Right ? AxisCanvas.\u0023\u003DztX3bWaM\u003D : AxisCanvas.\u0023\u003DzQLHMxl4\u003D;
      if (_param1.ActualWidth >= _param0.ActualWidth)
        dependencyProperty = dependencyProperty == AxisCanvas.\u0023\u003DztX3bWaM\u003D ? AxisCanvas.\u0023\u003DzQLHMxl4\u003D : AxisCanvas.\u0023\u003DztX3bWaM\u003D;
      _param1.SetValue(dependencyProperty, (object) 0.0);
    }
  }
}
