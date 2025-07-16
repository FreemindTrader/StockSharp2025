// Decompiled with JetBrains decompiler
// Type: #=zJ9vSi7sIwIEed80npzusCBIsk9iDYaB43AY2Ep7_kjoD
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
public struct \u0023\u003DzJ9vSi7sIwIEed80npzusCBIsk9iDYaB43AY2Ep7_kjoD : 
  IComparable,
  \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSesUJ7CmHq1ptV2iVGi9XvX4<double>
{
  
  private readonly double \u0023\u003Dzsaq6Yz0\u003D;
  
  private readonly double \u0023\u003DzHtUeg\u0024w\u003D;

  public \u0023\u003DzJ9vSi7sIwIEed80npzusCBIsk9iDYaB43AY2Ep7_kjoD(double _param1, double _param2)
    : this()
  {
    this.\u0023\u003Dzsaq6Yz0\u003D = _param1;
    this.\u0023\u003DzHtUeg\u0024w\u003D = _param2;
  }

  public int CompareTo(object _param1)
  {
    \u0023\u003DzJ9vSi7sIwIEed80npzusCBIsk9iDYaB43AY2Ep7_kjoD dyaB43Ay2Ep7KjoD = (\u0023\u003DzJ9vSi7sIwIEed80npzusCBIsk9iDYaB43AY2Ep7_kjoD) _param1;
    if (this.\u0023\u003Dz1iB_fGLmDWyy() > dyaB43Ay2Ep7KjoD.\u0023\u003Dzg1M\u0024G_5sXlam() || this.\u0023\u003DzZB\u0024O5xT4bzKv() > dyaB43Ay2Ep7KjoD.\u0023\u003Dzg1M\u0024G_5sXlam())
      return 1;
    return this.\u0023\u003Dz1iB_fGLmDWyy() < dyaB43Ay2Ep7KjoD.\u0023\u003DzKrTvxa8MJ66h() || this.\u0023\u003DzZB\u0024O5xT4bzKv() < dyaB43Ay2Ep7KjoD.\u0023\u003DzKrTvxa8MJ66h() ? -1 : 0;
  }

  public double \u0023\u003Dz1iB_fGLmDWyy() => this.\u0023\u003Dzsaq6Yz0\u003D;

  public double \u0023\u003DzZB\u0024O5xT4bzKv() => this.\u0023\u003DzHtUeg\u0024w\u003D;

  [SpecialName]
  public double \u0023\u003Dzg1M\u0024G_5sXlam()
  {
    return Math.Max(this.\u0023\u003Dzsaq6Yz0\u003D, this.\u0023\u003DzHtUeg\u0024w\u003D);
  }

  [SpecialName]
  public double \u0023\u003DzKrTvxa8MJ66h()
  {
    return Math.Min(this.\u0023\u003Dzsaq6Yz0\u003D, this.\u0023\u003DzHtUeg\u0024w\u003D);
  }

  [SpecialName]
  public double \u0023\u003Dzu7q98_E\u003D() => this.\u0023\u003Dz1iB_fGLmDWyy();
}
