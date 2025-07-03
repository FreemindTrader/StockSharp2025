// Decompiled with JetBrains decompiler
// Type: #=zAF1f$KZyh1dFR1SFJ2ERzgOjwIOkJQPp1$lBebSIObcmcDcLq1GF2ibnsblmQLwieoOzJMAEWewf
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
internal sealed class \u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzgOjwIOkJQPp1\u0024lBebSIObcmcDcLq1GF2ibnsblmQLwieoOzJMAEWewf : 
  \u0023\u003DzRqOhEDBos0N6g4v4zYOaM9GR9bn8mvj2yw4D0iSSz7\u00247IVjNcUWYRVrjRbV\u0024QDTRFg\u003D\u003D
{
  private double \u0023\u003Dznb94gaZaZtxqJOrpKA\u003D\u003D;

  public \u0023\u003DzAF1f\u0024KZyh1dFR1SFJ2ERzgOjwIOkJQPp1\u0024lBebSIObcmcDcLq1GF2ibnsblmQLwieoOzJMAEWewf(
    double _param1)
  {
    this.\u0023\u003Dznb94gaZaZtxqJOrpKA\u003D\u003D = _param1 < 2.0 ? 2.0 : _param1;
  }

  public double \u0023\u003Dzh1hhOkJ3kH4Y() => this.\u0023\u003Dznb94gaZaZtxqJOrpKA\u003D\u003D;

  public double \u0023\u003DzG17fc7\u0024pCNOA(double _param1)
  {
    if (_param1 == 0.0)
      return 1.0;
    if (_param1 > this.\u0023\u003Dznb94gaZaZtxqJOrpKA\u003D\u003D)
      return 0.0;
    _param1 *= Math.PI;
    double d = _param1 / this.\u0023\u003Dznb94gaZaZtxqJOrpKA\u003D\u003D;
    return Math.Sin(_param1) / _param1 * (0.42 + 0.5 * Math.Cos(d) + 0.08 * Math.Cos(2.0 * d));
  }
}
