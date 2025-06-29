// Decompiled with JetBrains decompiler
// Type: -.dje_z42AUKFNJR7FNKCN2ULZW4YEVA5L5VDXSWKUAX8KHRAKNTZLMV2GRX_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_z42AUKFNJR7FNKCN2ULZW4YEVA5L5VDXSWKUAX8KHRAKNTZLMV2GRX_ejd : 
  dje_z89FJ3UY6BDUNEYRAHTXB4XHX3L65Z9F7D67T9VT5YAN2QMSJAGHFX_ejd
{
  
  private float \u0023\u003Dz39nD3feKm878;
  
  private float \u0023\u003DzynwqZ7\u00241dLS6;

  protected override void \u0023\u003DzdL613chSNlLB(
    \u0023\u003DzlIIQe9QryEp0zlHhxjV_2ax18wWJMvXdEDq1k7UiFd2I _param1,
    IEnumerable<Point> _param2,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param3,
    \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpXRgcdFB\u0024SdWGN9GCT8\u003D _param4)
  {
    foreach (Point point in _param2)
      this.\u0023\u003DzdL613chSNlLB(_param1, point.X, point.Y, _param3, _param4);
  }

  protected override void \u0023\u003DzdL613chSNlLB(
    \u0023\u003DzlIIQe9QryEp0zlHhxjV_2ax18wWJMvXdEDq1k7UiFd2I _param1,
    double _param2,
    double _param3,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param4,
    \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpXRgcdFB\u0024SdWGN9GCT8\u003D _param5)
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

  protected override void \u0023\u003Dz15moWio\u003D(
    DependencyObject _param1,
    DependencyPropertyChangedEventArgs _param2)
  {
    this.\u0023\u003Dz39nD3feKm878 = (float) (this.Width * 0.5);
    this.\u0023\u003DzynwqZ7\u00241dLS6 = (float) (this.Height * 0.5);
    base.\u0023\u003Dz15moWio\u003D(_param1, _param2);
  }
}
