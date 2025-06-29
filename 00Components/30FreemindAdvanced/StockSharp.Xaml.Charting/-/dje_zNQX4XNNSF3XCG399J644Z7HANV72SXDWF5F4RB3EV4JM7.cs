// Decompiled with JetBrains decompiler
// Type: -.dje_zNQX4XNNSF3XCG399J644Z7HANV72SXDWF5F4RB3EV4JM7MZRCEPSD_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_zNQX4XNNSF3XCG399J644Z7HANV72SXDWF5F4RB3EV4JM7MZRCEPSD_ejd : 
  dje_z89FJ3UY6BDUNEYRAHTXB4XHX3L65Z9F7D67T9VT5YAN2QMSJAGHFX_ejd
{
  
  private float \u0023\u003DzxL_Q2Yk\u003D;
  
  private float \u0023\u003DzqcgkI5Q\u003D;

  protected override void \u0023\u003DzdL613chSNlLB(
    \u0023\u003DzlIIQe9QryEp0zlHhxjV_2ax18wWJMvXdEDq1k7UiFd2I _param1,
    IEnumerable<Point> _param2,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param3,
    \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpXRgcdFB\u0024SdWGN9GCT8\u003D _param4)
  {
    _param1.\u0023\u003DzoUffnI1MQnWod0s9Xg\u003D\u003D(_param3, _param4, _param2, (double) this.\u0023\u003DzxL_Q2Yk\u003D, (double) this.\u0023\u003DzqcgkI5Q\u003D);
  }

  protected override void \u0023\u003DzdL613chSNlLB(
    \u0023\u003DzlIIQe9QryEp0zlHhxjV_2ax18wWJMvXdEDq1k7UiFd2I _param1,
    double _param2,
    double _param3,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param4,
    \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpXRgcdFB\u0024SdWGN9GCT8\u003D _param5)
  {
    _param1.\u0023\u003DzIZCdW2WR6Rxw(_param4, _param5, new Point(_param2, _param3), (double) this.\u0023\u003DzxL_Q2Yk\u003D, (double) this.\u0023\u003DzqcgkI5Q\u003D);
  }

  protected override void \u0023\u003Dz15moWio\u003D(
    DependencyObject _param1,
    DependencyPropertyChangedEventArgs _param2)
  {
    this.\u0023\u003DzxL_Q2Yk\u003D = (float) this.Width;
    this.\u0023\u003DzqcgkI5Q\u003D = (float) this.Height;
    base.\u0023\u003Dz15moWio\u003D(_param1, _param2);
  }
}
