// Decompiled with JetBrains decompiler
// Type: -.dje_zYMPR4HCN9F644M5VZMDCZXZFKRV9XS927UKKJBCH3D4AHFK66TNJA_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

#nullable disable
namespace \u002D;

internal sealed class dje_zYMPR4HCN9F644M5VZMDCZXZFKRV9XS927UKKJBCH3D4AHFK66TNJA_ejd : 
  dje_z89FJ3UY6BDUNEYRAHTXB4XHX3L65Z9F7D67T9VT5YAN2QMSJAGHFX_ejd
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
    _param1.\u0023\u003DzVRUUvzhAr5SR(_param5, new Point(_param2 - (double) this.\u0023\u003Dz39nD3feKm878, _param3 - (double) this.\u0023\u003DzynwqZ7\u00241dLS6), new Point(_param2 + (double) this.\u0023\u003Dz39nD3feKm878, _param3 + (double) this.\u0023\u003DzynwqZ7\u00241dLS6), 0.0);
    _param1.\u0023\u003Dz7zUbWtTKc3tA(_param4, new Point(_param2 - (double) this.\u0023\u003Dz39nD3feKm878, _param3 - (double) this.\u0023\u003DzynwqZ7\u00241dLS6), new Point(_param2 + (double) this.\u0023\u003Dz39nD3feKm878, _param3 + (double) this.\u0023\u003DzynwqZ7\u00241dLS6));
  }

  protected override void \u0023\u003Dz15moWio\u003D(
    DependencyObject _param1,
    DependencyPropertyChangedEventArgs _param2)
  {
    this.\u0023\u003Dz39nD3feKm878 = (float) (this.Width * 0.5);
    this.\u0023\u003DzynwqZ7\u00241dLS6 = (float) (this.Height * 0.5);
    base.\u0023\u003Dz15moWio\u003D(_param1, _param2);
  }
}
