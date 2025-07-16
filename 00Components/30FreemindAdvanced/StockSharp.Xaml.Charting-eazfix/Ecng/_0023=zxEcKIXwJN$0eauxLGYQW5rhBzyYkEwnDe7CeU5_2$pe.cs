// Decompiled with JetBrains decompiler
// Type: #=zxEcKIXwJN$0eauxLGYQW5rhBzyYkEwnDe7CeU5_2$pe6
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public static class \u0023\u003DzxEcKIXwJN\u00240eauxLGYQW5rhBzyYkEwnDe7CeU5_2\u0024pe6
{
  public static bool IsDefined(this DateTime _param0)
  {
    return _param0 != DateTime.MaxValue && _param0 != DateTime.MinValue;
  }

  public static bool \u0023\u003Dzl5VrLhRrr5CB(this DateTime _param0, TimeSpan _param1)
  {
    bool flag = false;
    if ((double) _param0.Year + _param1.TotalDays / 365.0 < (double) DateTime.MaxValue.Year)
      flag = true;
    return flag;
  }

  public static DateTime \u0023\u003Dz8ly0q7w\u003D(this DateTime _param0, TimeSpan _param1)
  {
    if (_param1.IsDivisibleBy(TimeSpanExtensions.FromYears(1)))
      return _param0.AddYears((int) (_param1.Ticks / TimeSpanExtensions.FromYears(1).Ticks));
    return _param1.IsDivisibleBy(TimeSpanExtensions.FromMonths(1)) ? _param0.AddMonths((int) (_param1.Ticks / TimeSpanExtensions.FromMonths(1).Ticks)) : _param0.Add(_param1);
  }

  public static DateTime \u0023\u003DzK2E9JHa8v0UT(this DateTime _param0, int _param1)
  {
    int num1 = 0;
    int num2;
    for (num2 = _param1; num2 > 12; num2 -= 12)
      ++num1;
    return new DateTime(_param0.Year + num1, _param0.Month + num2, _param0.Day, _param0.Hour, _param0.Minute, _param0.Second, _param0.Millisecond);
  }

  public static DateTime \u0023\u003Dzx5OwI1M\u003D(this DateTime _param0, int _param1)
  {
    return new DateTime(_param0.Year + _param1, _param0.Month, _param0.Day, _param0.Hour, _param0.Minute, _param0.Second, _param0.Millisecond);
  }
}
