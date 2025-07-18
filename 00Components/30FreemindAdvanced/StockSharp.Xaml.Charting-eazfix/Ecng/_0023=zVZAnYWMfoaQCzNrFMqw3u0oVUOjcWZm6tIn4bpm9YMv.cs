// Decompiled with JetBrains decompiler
// Type: #=zVZAnYWMfoaQCzNrFMqw3u0oVUOjcWZm6tIn4bpm9YMvj_jwo7f3RMYA=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;

#nullable disable
public class \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u0oVUOjcWZm6tIn4bpm9YMvj_jwo7f3RMYA\u003D : 
  \u0023\u003DziARJyOecclYiJO5UbZqQJ8tTDgXi4mHI9\u0024dB3n962FPd
{
  private readonly string[] \u0023\u003Dz4HfK1guJY2ey = new string[4]
  {
    "MMM {0}",
    "yyyy {0}",
    "{0} yyyy",
    "dd MMM {0}"
  };
  private readonly string[] \u0023\u003Dz_x3fDihuHF1f = new string[4]
  {
    "yyyy",
    "MMM",
    "dd MMM",
    "HH:mm"
  };
  private int \u0023\u003DzTs5gEHtGGnsI;

  public override void \u0023\u003DzWzUaFxw\u003D(
    IAxis _param1)
  {
    if (!(_param1 is CategoryDateTimeAxis))
      throw new InvalidOperationException("The TradeChartAxisLabelFormatter is only valid on instances of CategoryDateTimeAxis");
    base.\u0023\u003DzWzUaFxw\u003D(_param1);
  }

  public override void \u0023\u003DzI_kEht21kBsX()
  {
    CategoryDateTimeAxis nu9622VfydaypdeqEjd = (CategoryDateTimeAxis) this.\u0023\u003DzHZDgUSdfqmkx();
    double num1 = nu9622VfydaypdeqEjd.\u0023\u003DzbY7N\u0024Xk2WSr8();
    \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D dx26vpI1xWpwwNqJw = nu9622VfydaypdeqEjd.\u0023\u003DzFL7WRclCPBWI((IndexRange ) nu9622VfydaypdeqEjd.VisibleRange);
    this.\u0023\u003DzTs5gEHtGGnsI = 3;
    if (dx26vpI1xWpwwNqJw != null && dx26vpI1xWpwwNqJw.IsDefined)
    {
      long ticks1 = dx26vpI1xWpwwNqJw.Diff.Ticks;
      long num2 = ticks1;
      TimeSpan timeSpan = TimeSpanExtensions.FromYears(2);
      long ticks2 = timeSpan.Ticks;
      if (num2 > ticks2)
      {
        this.\u0023\u003DzTs5gEHtGGnsI = 0;
      }
      else
      {
        long num3 = ticks1;
        timeSpan = TimeSpan.FromDays(14.0);
        long ticks3 = timeSpan.Ticks;
        if (num3 <= ticks3)
        {
          double num4 = num1;
          timeSpan = TimeSpan.FromDays(1.0);
          double ticks4 = (double) timeSpan.Ticks;
          if (num4 < ticks4)
            goto label_6;
        }
        this.\u0023\u003DzTs5gEHtGGnsI = -1;
      }
    }
label_6:
    base.\u0023\u003DzI_kEht21kBsX();
  }

  public override string \u0023\u003Dz\u0024WinkXTLMGVP(IComparable _param1, bool _param2)
  {
    DateTime dateTime = _param1.\u0023\u003Dzxuo5aY4wjkaI();
    string str;
    if (this.\u0023\u003DzHZDgUSdfqmkx().get_CursorTextFormatting().\u0023\u003DzCCMM80zDpO6N<char>())
    {
      int index = this.\u0023\u003DztY4N5eKR6isL(dateTime, true);
      str = string.Format(dateTime.ToString(this.\u0023\u003Dz4HfK1guJY2ey[index]), (object) dateTime.ToString(this.\u0023\u003Dz_x3fDihuHF1f[index]));
    }
    else
      str = dateTime.ToString(this.\u0023\u003DzHZDgUSdfqmkx().get_CursorTextFormatting());
    return str;
  }

  public override string \u0023\u003DzkqN2vZ4\u003D(IComparable _param1)
  {
    DateTime dateTime = _param1.\u0023\u003Dzxuo5aY4wjkaI();
    string format = this.\u0023\u003Dz_x3fDihuHF1f[this.\u0023\u003DztY4N5eKR6isL(dateTime, false)];
    return dateTime.ToString(format);
  }

  private int \u0023\u003DztY4N5eKR6isL(DateTime _param1, bool _param2)
  {
    int num = this.\u0023\u003DzTs5gEHtGGnsI;
    if (num < 0)
    {
      num = 2;
      if (_param1.Day == 1 && !_param2)
        num = 1;
    }
    return num;
  }
}
