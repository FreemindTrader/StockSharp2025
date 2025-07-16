// Decompiled with JetBrains decompiler
// Type: -.TrianglePointMarker
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

#nullable disable
namespace SciChart.Charting;

internal sealed class TrianglePointMarker : 
  BasePointMarker
{
  
  private float \u0023\u003Dz39nD3feKm878;
  
  private float \u0023\u003DzynwqZ7\u00241dLS6;

  protected override void \u0023\u003DzdL613chSNlLB(
    IRenderContext2D _param1,
    IEnumerable<Point> _param2,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param3,
    IBrush2D _param4)
  {
    foreach (Point point in _param2)
      this.\u0023\u003DzdL613chSNlLB(_param1, point.X, point.Y, _param3, _param4);
  }

  protected override void \u0023\u003DzdL613chSNlLB(
    IRenderContext2D _param1,
    double _param2,
    double _param3,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param4,
    IBrush2D _param5)
  {
    Point[] pointArray = new Point[4]
    {
      new Point(_param2 - (double) this.\u0023\u003Dz39nD3feKm878, _param3 - (double) this.\u0023\u003DzynwqZ7\u00241dLS6),
      new Point(_param2 + (double) this.\u0023\u003Dz39nD3feKm878, _param3 - (double) this.\u0023\u003DzynwqZ7\u00241dLS6),
      new Point(_param2, _param3 + (double) this.\u0023\u003DzynwqZ7\u00241dLS6),
      new Point(_param2 - (double) this.\u0023\u003Dz39nD3feKm878, _param3 - (double) this.\u0023\u003DzynwqZ7\u00241dLS6)
    };
    _param1.\u0023\u003Dz_I15ZX7u91\u0024T(_param5, (IEnumerable<Point>) pointArray);
    _param1.\u0023\u003DzKbzNI_AipRxe(_param4, (IEnumerable<Point>) pointArray);
  }

  protected override void OnPropertyChanged(
    DependencyObject _param1,
    DependencyPropertyChangedEventArgs _param2)
  {
    this.\u0023\u003Dz39nD3feKm878 = (float) (this.Width * 0.5);
    this.\u0023\u003DzynwqZ7\u00241dLS6 = (float) (this.Height * 0.5);
    base.OnPropertyChanged(_param1, _param2);
  }
}
