// Decompiled with JetBrains decompiler
// Type: #=zm$__dHBBbeN8TiOszDZ4threxLLudETYN2pgrj0XJaQz
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public static class TimeSpanExtensions
{
  public static bool \u0023\u003DzhilcjqQ\u003D(this TimeSpan _param0)
  {
    return _param0 == TimeSpan.Zero;
  }

  public static TimeSpan FromMonths(int _param0)
  {
    return TimeSpan.FromDays((double) _param0 * 30.436875);
  }

  public static TimeSpan \u0023\u003Dzop7Mb7nz1Slh(int _param0)
  {
    return TimeSpan.FromDays((double) (_param0 * 7));
  }

  public static TimeSpan FromYears(int _param0)
  {
    return TimeSpan.FromDays((double) _param0 * 365.2425);
  }

  public static bool IsDivisibleBy(this TimeSpan _param0, TimeSpan _param1)
  {
    return NumberUtil.IsDivisibleBy((double) _param0.Ticks, (double) _param1.Ticks);
  }

  public static bool \u0023\u003Dzl5VrLhRrr5CB(this TimeSpan _param0, TimeSpan _param1)
  {
    bool flag = false;
    if (_param0 + _param1 < TimeSpan.MaxValue)
      flag = true;
    return flag;
  }
}
