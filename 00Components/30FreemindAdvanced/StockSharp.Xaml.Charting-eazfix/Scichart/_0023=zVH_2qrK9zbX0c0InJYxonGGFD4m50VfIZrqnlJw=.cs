// Decompiled with JetBrains decompiler
// Type: #=zVH_2qrK9zbX0c0InJYxonGGFD4m50VfIZrqnlJw=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
internal static class \u0023\u003DzVH_2qrK9zbX0c0InJYxonGGFD4m50VfIZrqnlJw\u003D
{
  internal static Decimal \u0023\u003DzZsq6ZfbZQvsf(this Decimal _param0)
  {
    return Decimal.Round(_param0, 0);
  }

  internal static Decimal \u0023\u003DzZsq6ZfbZQvsf(this Decimal _param0, MidpointRounding _param1)
  {
    return _param0.\u0023\u003DzZsq6ZfbZQvsf(0, _param1);
  }

  internal static Decimal \u0023\u003DzZsq6ZfbZQvsf(
    this Decimal _param0,
    int _param1,
    MidpointRounding _param2)
  {
    if (_param2 == MidpointRounding.ToEven)
      return Decimal.Round(_param0, _param1);
    Decimal num1 = Convert.ToDecimal(Math.Pow(10.0, (double) _param1));
    int num2 = Math.Sign(_param0);
    return Decimal.Truncate(_param0 * num1 + 0.5M * (Decimal) num2) / num1;
  }

  internal static DateTime \u0023\u003Dzxuo5aY4wjkaI(this Decimal _param0)
  {
    return new DateTime((long) _param0.\u0023\u003DzZsq6ZfbZQvsf(MidpointRounding.AwayFromZero));
  }
}
