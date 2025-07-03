// Decompiled with JetBrains decompiler
// Type: -.dje_zU6LW4MFFH4RUUNQUBP297YJBKYUAEUKG3SLMFR7LPB76KDQ8DS5VW_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zU6LW4MFFH4RUUNQUBP297YJBKYUAEUKG3SLMFR7LPB76KDQ8DS5VW_ejd : 
  dje_z89FJ3UY6BDUNEYRAHTXB4XHX3L65Z9F7D67T9VT5YAN2QMSJAGHFX_ejd
{
  
  private float \u0023\u003DzU7DqSJMJO2bJ;
  
  private float \u0023\u003Dzdr7IdQadikmk;

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
    _param1.\u0023\u003Dzk8_eoWQ\u003D(_param4, new Point(_param2 - (double) this.\u0023\u003DzU7DqSJMJO2bJ, _param3), new Point(_param2 + (double) this.\u0023\u003DzU7DqSJMJO2bJ, _param3));
    _param1.\u0023\u003Dzk8_eoWQ\u003D(_param4, new Point(_param2, _param3 - (double) this.\u0023\u003Dzdr7IdQadikmk), new Point(_param2, _param3 + (double) this.\u0023\u003Dzdr7IdQadikmk));
  }

  protected override void \u0023\u003Dz15moWio\u003D(
    DependencyObject _param1,
    DependencyPropertyChangedEventArgs _param2)
  {
    this.\u0023\u003DzU7DqSJMJO2bJ = (float) (this.Width * 0.5);
    this.\u0023\u003Dzdr7IdQadikmk = (float) (this.Height * 0.5);
    base.\u0023\u003Dz15moWio\u003D(_param1, _param2);
  }
}
