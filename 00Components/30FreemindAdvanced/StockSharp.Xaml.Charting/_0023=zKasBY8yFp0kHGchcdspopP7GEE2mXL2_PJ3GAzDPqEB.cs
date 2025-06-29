// Decompiled with JetBrains decompiler
// Type: #=zKasBY8yFp0kHGchcdspopP7GEE2mXL2_PJ3GAzDPqEBp355x434uR$Xvxl7l
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Windows;

#nullable disable
internal sealed class \u0023\u003DzKasBY8yFp0kHGchcdspopP7GEE2mXL2_PJ3GAzDPqEBp355x434uR\u0024Xvxl7l
{
  private readonly Point \u0023\u003DzaW71LKQ\u003D;

  public \u0023\u003DzKasBY8yFp0kHGchcdspopP7GEE2mXL2_PJ3GAzDPqEBp355x434uR\u0024Xvxl7l(
    Point _param1)
  {
    this.\u0023\u003DzaW71LKQ\u003D = _param1;
  }

  public \u0023\u003DzKasBY8yFp0kHGchcdspopP7GEE2mXL2_PJ3GAzDPqEBp355x434uR\u0024Xvxl7l(
    double _param1,
    double _param2)
  {
    this.\u0023\u003DzaW71LKQ\u003D = new Point(_param1 / 2.0, _param2 / 2.0);
  }

  public Point \u0023\u003DzseEeoNXm3G\u0024B() => this.\u0023\u003DzaW71LKQ\u003D;

  public Point \u0023\u003DztSU4xSLxm6xoY2ZEZw\u003D\u003D(double _param1, double _param2)
  {
    _param1 *= Math.PI / 180.0;
    Point zaW71Lkq = this.\u0023\u003DzaW71LKQ\u003D;
    double num1 = zaW71Lkq.X + _param2 * Math.Cos(_param1);
    zaW71Lkq = this.\u0023\u003DzaW71LKQ\u003D;
    double num2 = zaW71Lkq.Y + _param2 * Math.Sin(_param1);
    return new Point(num1, num2);
  }

  public Point \u0023\u003DzEQGlg1WyJo2b(double _param1, double _param2)
  {
    if (_param1.Equals(this.\u0023\u003DzaW71LKQ\u003D.X) && _param2.Equals(this.\u0023\u003DzaW71LKQ\u003D.Y))
      return new Point(0.0, 0.0);
    _param1 -= this.\u0023\u003DzaW71LKQ\u003D.X;
    _param2 -= this.\u0023\u003DzaW71LKQ\u003D.Y;
    double num1 = Math.Sqrt(_param1 * _param1 + _param2 * _param2);
    double num2 = Math.Atan(_param2 / _param1) / (Math.PI / 180.0);
    if (_param1 < 0.0)
      num2 += 180.0;
    else if (_param2 < 0.0)
      num2 += 360.0;
    return new Point(num2, num1);
  }
}
