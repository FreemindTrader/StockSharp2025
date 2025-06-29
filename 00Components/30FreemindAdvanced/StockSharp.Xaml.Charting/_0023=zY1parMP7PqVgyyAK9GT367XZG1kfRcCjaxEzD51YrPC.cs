// Decompiled with JetBrains decompiler
// Type: #=zY1parMP7PqVgyyAK9GT367XZG1kfRcCjaxEzD51YrPC6
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
internal struct \u0023\u003DzY1parMP7PqVgyyAK9GT367XZG1kfRcCjaxEzD51YrPC6 : 
  \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSesUJ7CmHq1ptV2iVGi9XvX4<double>,
  IComparable
{
  
  private readonly double \u0023\u003Dz0HHYg1s\u003D;
  
  private readonly double \u0023\u003DzN7MEa5Q\u003D;

  public \u0023\u003DzY1parMP7PqVgyyAK9GT367XZG1kfRcCjaxEzD51YrPC6(double _param1, double _param2)
    : this()
  {
    this.\u0023\u003Dz0HHYg1s\u003D = _param1;
    this.\u0023\u003DzN7MEa5Q\u003D = _param2;
  }

  public int CompareTo(object _param1)
  {
    \u0023\u003DzY1parMP7PqVgyyAK9GT367XZG1kfRcCjaxEzD51YrPC6 rcCjaxEzD51YrPc6 = (\u0023\u003DzY1parMP7PqVgyyAK9GT367XZG1kfRcCjaxEzD51YrPC6) _param1;
    if (this.\u0023\u003Dzg1M\u0024G_5sXlam() > rcCjaxEzD51YrPc6.\u0023\u003Dzg1M\u0024G_5sXlam())
      return 1;
    return this.\u0023\u003DzKrTvxa8MJ66h() < rcCjaxEzD51YrPc6.\u0023\u003DzKrTvxa8MJ66h() ? -1 : 0;
  }

  [SpecialName]
  public double \u0023\u003Dzu7q98_E\u003D() => this.\u0023\u003Dz0HHYg1s\u003D;

  public double \u0023\u003DzM49\u0024G3E\u003D() => this.\u0023\u003DzN7MEa5Q\u003D;

  [SpecialName]
  public double \u0023\u003Dzg1M\u0024G_5sXlam() => this.\u0023\u003Dz0HHYg1s\u003D;

  [SpecialName]
  public double \u0023\u003DzKrTvxa8MJ66h() => this.\u0023\u003Dz0HHYg1s\u003D;
}
