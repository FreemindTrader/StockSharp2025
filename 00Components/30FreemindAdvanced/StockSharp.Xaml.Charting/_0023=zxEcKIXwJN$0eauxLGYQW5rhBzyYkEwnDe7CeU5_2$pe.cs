// Decompiled with JetBrains decompiler
// Type: #=zxEcKIXwJN$0eauxLGYQW5rhBzyYkEwnDe7CeU5_2$pe6
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;

#nullable disable
internal static class \u0023\u003DzxEcKIXwJN\u00240eauxLGYQW5rhBzyYkEwnDe7CeU5_2\u0024pe6
{
  internal static bool IsFiniteNumber(this DateTime _param0)
  {
    return _param0 != DateTime.MaxValue && _param0 != DateTime.MinValue;
  }

  internal static bool \u0023\u003Dzl5VrLhRrr5CB(this DateTime _param0, TimeSpan _param1)
  {
    bool flag = false;
    if ((double) _param0.Year + _param1.TotalDays / 365.0 < (double) DateTime.MaxValue.Year)
      flag = true;
    return flag;
  }

  internal static DateTime \u0023\u003Dz8ly0q7w\u003D(this DateTime _param0, TimeSpan _param1)
  {
    if (_param1.\u0023\u003Dzpz5fd0zg24Kt(\u0023\u003Dzm\u0024__dHBBbeN8TiOszDZ4threxLLudETYN2pgrj0XJaQz.\u0023\u003DzKGRRRbI\u003D(1)))
      return _param0.AddYears((int) (_param1.Ticks / \u0023\u003Dzm\u0024__dHBBbeN8TiOszDZ4threxLLudETYN2pgrj0XJaQz.\u0023\u003DzKGRRRbI\u003D(1).Ticks));
    return _param1.\u0023\u003Dzpz5fd0zg24Kt(\u0023\u003Dzm\u0024__dHBBbeN8TiOszDZ4threxLLudETYN2pgrj0XJaQz.\u0023\u003DzmlhV\u0024ev7WhIH(1)) ? _param0.AddMonths((int) (_param1.Ticks / \u0023\u003Dzm\u0024__dHBBbeN8TiOszDZ4threxLLudETYN2pgrj0XJaQz.\u0023\u003DzmlhV\u0024ev7WhIH(1).Ticks)) : _param0.Add(_param1);
  }

  internal static DateTime \u0023\u003DzK2E9JHa8v0UT(this DateTime _param0, int _param1)
  {
    int num1 = 0;
    int num2;
    for (num2 = _param1; num2 > 12; num2 -= 12)
      ++num1;
    return new DateTime(_param0.Year + num1, _param0.Month + num2, _param0.Day, _param0.Hour, _param0.Minute, _param0.Second, _param0.Millisecond);
  }

  internal static DateTime \u0023\u003Dzx5OwI1M\u003D(this DateTime _param0, int _param1)
  {
    return new DateTime(_param0.Year + _param1, _param0.Month, _param0.Day, _param0.Hour, _param0.Minute, _param0.Second, _param0.Millisecond);
  }
}
