// Decompiled with JetBrains decompiler
// Type: #=zGULZ_B3lGVEDiq9xPbVQjhE5szIVFtMAFcvWbis=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public static class \u0023\u003DzGULZ_B3lGVEDiq9xPbVQjhE5szIVFtMAFcvWbis\u003D
{
  public static double \u0023\u003Dz\u0024DV3des\u003D(this double _param0, double _param1)
  {
    return !_param0.IsNaN() ? _param0 : _param1;
  }

  public static bool \u0023\u003Dz_Bj0HmLWq3hY(this double _param0)
  {
    if (!double.IsNaN(_param0) && !double.IsInfinity(_param0))
    {
      double num = double.MaxValue;
      if (!num.Equals(_param0))
      {
        num = double.MinValue;
        return !num.Equals(_param0);
      }
    }
    return false;
  }

  public static bool IsNaN(this double _param0) => _param0 != _param0;

  public static double \u0023\u003DzZsq6ZfbZQvsf(this double _param0) => Math.Round(_param0);

  public static double \u0023\u003Dze1\u0024Ye41h1uhB(this double _param0)
  {
    return Math.Ceiling(_param0);
  }

  public static double \u0023\u003Dz0lUatTkkbf_v(this double _param0) => Math.Floor(_param0);

  public static double \u0023\u003DzZsq6ZfbZQvsf(this double _param0, MidpointRounding _param1)
  {
    return _param0.\u0023\u003DzZsq6ZfbZQvsf(0, _param1);
  }

  public static double \u0023\u003DzZsq6ZfbZQvsf(
    this double _param0,
    int _param1,
    MidpointRounding _param2)
  {
    if (_param2 == MidpointRounding.ToEven)
      return Math.Round(_param0, _param1);
    Decimal num1 = Convert.ToDecimal(Math.Pow(10.0, (double) _param1));
    int num2 = Math.Sign(_param0);
    return (double) (Decimal.Truncate((Decimal) _param0 * num1 + 0.5M * (Decimal) num2) / num1);
  }

  public static DateTime \u0023\u003Dzxuo5aY4wjkaI(this double _param0)
  {
    return new DateTime(NumberUtil.Constrain((long) _param0, DateTime.MinValue.Ticks, DateTime.MaxValue.Ticks));
  }

  public static double \u0023\u003DzcYUW_6FX9t5L(this double _param0)
  {
    if (_param0 > (double) int.MaxValue)
      return (double) int.MaxValue;
    return _param0 < (double) int.MinValue ? (double) int.MinValue : _param0;
  }

  public static int \u0023\u003DzYNd6r7dW43yr(this double _param0)
  {
    if (_param0 > (double) int.MaxValue)
      return int.MaxValue;
    return _param0 < (double) int.MinValue ? int.MinValue : (int) _param0;
  }
}
