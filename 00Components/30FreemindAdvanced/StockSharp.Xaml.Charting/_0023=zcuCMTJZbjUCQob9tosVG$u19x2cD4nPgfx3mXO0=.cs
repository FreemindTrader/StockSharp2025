// Decompiled with JetBrains decompiler
// Type: #=zcuCMTJZbjUCQob9tosVG$u19x2cD4nPgfx3mXO0=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#nullable disable
internal static class \u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u19x2cD4nPgfx3mXO0\u003D
{
  private static readonly List<int> \u0023\u003Dzq4vxqfnRtObeNizb8AgRA08\u003D = ((IEnumerable<int>) new int[5]
  {
    1,
    4,
    7,
    10,
    13
  }).ToList<int>();
  private static readonly List<int> \u0023\u003Dz9qzqLQiyvbrr2hRQCQ\u003D\u003D = ((IEnumerable<int>) new int[3]
  {
    1,
    7,
    13
  }).ToList<int>();
  private static readonly List<int> \u0023\u003DzFF\u0024MshfJr7WT97G2Tw\u003D\u003D = ((IEnumerable<int>) new int[7]
  {
    1,
    3,
    5,
    7,
    9,
    11,
    13
  }).ToList<int>();
  private static readonly TimeSpan \u0023\u003DzznyrXvnCWi7T = \u0023\u003Dzm\u0024__dHBBbeN8TiOszDZ4threxLLudETYN2pgrj0XJaQz.\u0023\u003DzKGRRRbI\u003D(1);
  private static readonly TimeSpan \u0023\u003DzAdrg_08mpWO\u0024EbvWEg\u003D\u003D = \u0023\u003Dzm\u0024__dHBBbeN8TiOszDZ4threxLLudETYN2pgrj0XJaQz.\u0023\u003DzmlhV\u0024ev7WhIH(6);
  private static readonly TimeSpan \u0023\u003DzCxemqM9TrjL\u0024OktvQA\u003D\u003D = \u0023\u003Dzm\u0024__dHBBbeN8TiOszDZ4threxLLudETYN2pgrj0XJaQz.\u0023\u003DzmlhV\u0024ev7WhIH(3);
  private static readonly TimeSpan \u0023\u003DzrbkbSSnCClrh = \u0023\u003Dzm\u0024__dHBBbeN8TiOszDZ4threxLLudETYN2pgrj0XJaQz.\u0023\u003DzmlhV\u0024ev7WhIH(1);

  internal static DateTime Max(DateTime _param0, DateTime _param1)
  {
    return _param0.Ticks <= _param1.Ticks ? _param1 : _param0;
  }

  internal static DateTime Min(DateTime _param0, DateTime _param1)
  {
    return _param0.Ticks >= _param1.Ticks ? _param1 : _param0;
  }

  internal static bool \u0023\u003Dzpz5fd0zg24Kt(DateTime _param0, TimeSpan _param1)
  {
    return _param1.Ticks % \u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u19x2cD4nPgfx3mXO0\u003D.\u0023\u003DzznyrXvnCWi7T.Ticks == 0L && _param0.Day == 1 && _param0.Month == 1 || _param1.Ticks % \u0023\u003Dzm\u0024__dHBBbeN8TiOszDZ4threxLLudETYN2pgrj0XJaQz.\u0023\u003DzmlhV\u0024ev7WhIH(1).Ticks == 0L && _param0.Day == 1 || \u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u19x2cD4nPgfx3mXO0\u003D.\u0023\u003Dz1bwNIVeIfnJ7(_param0, _param1).Equals(_param0);
  }

  internal static DateTime \u0023\u003Dz1bwNIVeIfnJ7(DateTime _param0, TimeSpan _param1)
  {
    if (_param1.\u0023\u003Dzpz5fd0zg24Kt(\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u19x2cD4nPgfx3mXO0\u003D.\u0023\u003DzznyrXvnCWi7T))
    {
      long num = _param1.Ticks / \u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u19x2cD4nPgfx3mXO0\u003D.\u0023\u003DzznyrXvnCWi7T.Ticks;
      return _param0.Day == 1 && _param0.Month == 1 && \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003Dzpz5fd0zg24Kt((double) _param0.Year, (double) num) ? _param0 : new DateTime((int) \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003Dz1bwNIVeIfnJ7((double) (_param0.Year + 1), (double) num), 1, 1);
    }
    if (_param1.\u0023\u003Dzpz5fd0zg24Kt(\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u19x2cD4nPgfx3mXO0\u003D.\u0023\u003DzAdrg_08mpWO\u0024EbvWEg\u003D\u003D))
    {
      if (_param0.Day == 1 && \u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u19x2cD4nPgfx3mXO0\u003D.\u0023\u003Dz9qzqLQiyvbrr2hRQCQ\u003D\u003D.Contains(_param0.Month))
        return _param0;
      int num = _param0.Month < 7 ? 7 : 13;
      return \u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u19x2cD4nPgfx3mXO0\u003D.\u0023\u003DzmSmiOHcuXkKI(_param0.Year, num);
    }
    if (_param1.\u0023\u003Dzpz5fd0zg24Kt(\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u19x2cD4nPgfx3mXO0\u003D.\u0023\u003DzCxemqM9TrjL\u0024OktvQA\u003D\u003D))
    {
      if (_param0.Day == 1 && \u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u19x2cD4nPgfx3mXO0\u003D.\u0023\u003Dzq4vxqfnRtObeNizb8AgRA08\u003D.Contains(_param0.Month))
        return _param0;
      int num = _param0.Month < 4 ? 4 : (_param0.Month < 7 ? 7 : (_param0.Month < 10 ? 10 : 13));
      return \u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u19x2cD4nPgfx3mXO0\u003D.\u0023\u003DzmSmiOHcuXkKI(_param0.Year, num);
    }
    if (!_param1.\u0023\u003Dzpz5fd0zg24Kt(\u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u19x2cD4nPgfx3mXO0\u003D.\u0023\u003DzrbkbSSnCClrh))
      return new DateTime((long) \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003Dz1bwNIVeIfnJ7((double) _param0.Ticks, (double) _param1.Ticks));
    if (_param0.Day == 1)
      return _param0;
    long num1 = (long) (int) (_param1.Ticks / \u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u19x2cD4nPgfx3mXO0\u003D.\u0023\u003DzrbkbSSnCClrh.Ticks);
    int num2 = (int) \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003Dz1bwNIVeIfnJ7((double) (_param0.Month + 1), (double) num1);
    return \u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u19x2cD4nPgfx3mXO0\u003D.\u0023\u003DzmSmiOHcuXkKI(_param0.Year, num2);
  }

  private static DateTime \u0023\u003DzmSmiOHcuXkKI(int _param0, int _param1)
  {
    while (_param1 > 12)
    {
      _param1 -= 12;
      ++_param0;
    }
    return new DateTime(_param0, _param1, 1);
  }

  private static DateTime \u0023\u003DzNtAWa4FrGbyi(DateTime _param0, Decimal _param1)
  {
    int day = _param0.Day;
    int month = _param0.Month;
    int year = _param0.Year;
    if (day == 1 && month == 1)
      --year;
    int num = year % (int) _param1;
    return new DateTime(year + ((int) _param1 - num), 1, 1);
  }

  private static DateTime \u0023\u003DzNDv8Ani3xluw3JCz8A\u003D\u003D(
    DateTime _param0,
    Decimal _param1)
  {
    \u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u19x2cD4nPgfx3mXO0\u003D.\u0023\u003Dz4sSy7LmbTiAIt\u0024fc7nJ88\u00244\u003D lmbTiAitFc7nJ884 = new \u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u19x2cD4nPgfx3mXO0\u003D.\u0023\u003Dz4sSy7LmbTiAIt\u0024fc7nJ88\u00244\u003D();
    lmbTiAitFc7nJ884.\u0023\u003Dz2vPFHdg\u003D = _param0;
    int num1 = (int) _param1;
    int year = lmbTiAitFc7nJ884.\u0023\u003Dz2vPFHdg\u003D.Year;
    int month1 = lmbTiAitFc7nJ884.\u0023\u003Dz2vPFHdg\u003D.Month;
    int day = lmbTiAitFc7nJ884.\u0023\u003Dz2vPFHdg\u003D.Day;
    int num2;
    if (num1 == 1)
    {
      if (day == 1)
        return lmbTiAitFc7nJ884.\u0023\u003Dz2vPFHdg\u003D;
      int num3 = month1 % (int) _param1;
      num2 = month1 + (num3 + 1);
    }
    else
    {
      List<int> source = \u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u19x2cD4nPgfx3mXO0\u003D.\u0023\u003DzFF\u0024MshfJr7WT97G2Tw\u003D\u003D;
      if (num1 == 2)
        source = \u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u19x2cD4nPgfx3mXO0\u003D.\u0023\u003DzFF\u0024MshfJr7WT97G2Tw\u003D\u003D;
      if (num1 == 3)
        source = \u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u19x2cD4nPgfx3mXO0\u003D.\u0023\u003Dzq4vxqfnRtObeNizb8AgRA08\u003D;
      if (num1 == 6)
        source = \u0023\u003DzcuCMTJZbjUCQob9tosVG\u0024u19x2cD4nPgfx3mXO0\u003D.\u0023\u003Dz9qzqLQiyvbrr2hRQCQ\u003D\u003D;
      num2 = source.First<int>(new Func<int, bool>(lmbTiAitFc7nJ884.\u0023\u003DzbFtMCwlI5fnLxwBa50UJnCg\u003D));
      if (day == 1)
        num2 -= num1;
    }
    if (num2 > 12)
    {
      num2 -= 12;
      ++year;
    }
    int month2 = \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzOS5Il8E\u003D(num2, 1, 12);
    return new DateTime(year, month2, 1);
  }

  private static DateTime \u0023\u003DzhdsrAr1RV3LfG9KiHA\u003D\u003D(
    DateTime _param0,
    Decimal _param1)
  {
    return _param0.DayOfWeek == DayOfWeek.Monday ? _param0 : _param0.AddDays((double) (8 - _param0.DayOfWeek)).Date;
  }

  public static int \u0023\u003DzmFqiVl_wmxhe(this DateTime _param0)
  {
    return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(_param0, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
  }

  private static DateTime \u0023\u003Dz4fQmoK4gPpWA(DateTime _param0, Decimal _param1)
  {
    TimeSpan timeSpan = TimeSpan.FromDays((double) _param1);
    return new DateTime((long) \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003Dz1bwNIVeIfnJ7((double) _param0.Ticks, (double) timeSpan.Ticks));
  }

  private static DateTime \u0023\u003DzhC_fRXkiXxoh(DateTime _param0, Decimal _param1)
  {
    TimeSpan timeSpan = TimeSpan.FromSeconds((double) _param1);
    return new DateTime((long) \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003Dz1bwNIVeIfnJ7((double) _param0.Ticks, (double) timeSpan.Ticks));
  }

  private sealed class \u0023\u003Dz4sSy7LmbTiAIt\u0024fc7nJ88\u00244\u003D
  {
    public DateTime \u0023\u003Dz2vPFHdg\u003D;

    internal bool \u0023\u003DzbFtMCwlI5fnLxwBa50UJnCg\u003D(int _param1)
    {
      return _param1 > this.\u0023\u003Dz2vPFHdg\u003D.Month;
    }
  }
}
