// Decompiled with JetBrains decompiler
// Type: #=zUib3SzczDtLU7txM4YiSeKmW3DwiRbwEiC7JBb4bkPqt
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
internal struct \u0023\u003DzUib3SzczDtLU7txM4YiSeKmW3DwiRbwEiC7JBb4bkPqt : 
  \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSesUJ7CmHq1ptV2iVGi9XvX4<double>,
  IComparable
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003DzXD61jixfGgmLDHbt\u0024g\u003D\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003DzuGqFemmPwd21fB7N9edfzbI\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private double \u0023\u003DzbOZctV9h9Ot9C60R89VsJXs\u003D;

  public \u0023\u003DzUib3SzczDtLU7txM4YiSeKmW3DwiRbwEiC7JBb4bkPqt(
    double _param1,
    double _param2,
    double _param3)
    : this()
  {
    this.\u0023\u003DzP0rPObw\u003D(_param1);
    this.\u0023\u003Dzc1dyB20lsR_b(_param2);
    this.\u0023\u003DzyUCzfl3w_XuI(_param3);
  }

  [SpecialName]
  public double \u0023\u003Dzg1M\u0024G_5sXlam() => this.\u0023\u003Dz\u0024ktIt4bbVFKI();

  [SpecialName]
  public double \u0023\u003DzKrTvxa8MJ66h() => this.\u0023\u003Dzasn0Azw0UwTS();

  [CompilerGenerated]
  [SpecialName]
  public readonly double \u0023\u003Dzu7q98_E\u003D()
  {
    return this.\u0023\u003DzXD61jixfGgmLDHbt\u0024g\u003D\u003D;
  }

  private void \u0023\u003DzP0rPObw\u003D(double _param1)
  {
    this.\u0023\u003DzXD61jixfGgmLDHbt\u0024g\u003D\u003D = _param1;
  }

  public readonly double \u0023\u003Dz\u0024ktIt4bbVFKI()
  {
    return this.\u0023\u003DzuGqFemmPwd21fB7N9edfzbI\u003D;
  }

  private void \u0023\u003Dzc1dyB20lsR_b(double _param1)
  {
    this.\u0023\u003DzuGqFemmPwd21fB7N9edfzbI\u003D = _param1;
  }

  public readonly double \u0023\u003Dzasn0Azw0UwTS()
  {
    return this.\u0023\u003DzbOZctV9h9Ot9C60R89VsJXs\u003D;
  }

  private void \u0023\u003DzyUCzfl3w_XuI(double _param1)
  {
    this.\u0023\u003DzbOZctV9h9Ot9C60R89VsJXs\u003D = _param1;
  }

  public int CompareTo(object _param1)
  {
    \u0023\u003DzUib3SzczDtLU7txM4YiSeKmW3DwiRbwEiC7JBb4bkPqt rbwEiC7Jbb4bkPqt = (\u0023\u003DzUib3SzczDtLU7txM4YiSeKmW3DwiRbwEiC7JBb4bkPqt) _param1;
    if (this.\u0023\u003Dzg1M\u0024G_5sXlam() > rbwEiC7Jbb4bkPqt.\u0023\u003Dzg1M\u0024G_5sXlam())
      return 1;
    return this.\u0023\u003DzKrTvxa8MJ66h() >= rbwEiC7Jbb4bkPqt.\u0023\u003DzKrTvxa8MJ66h() ? 0 : -1;
  }
}
