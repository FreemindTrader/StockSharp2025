// Decompiled with JetBrains decompiler
// Type: #=zduViKcXTrKCfnYwdbArizhKjvBq3lpdPWarUHV$07Pk$
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#nullable disable
internal static class \u0023\u003DzduViKcXTrKCfnYwdbArizhKjvBq3lpdPWarUHV\u002407Pk\u0024
{
  internal static bool \u0023\u003DzutrFxOU\u003D(this IComparable _param0)
  {
    return _param0 != null && \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNXCE4EvjzL\u0024mX84Druo\u003D.\u0023\u003DzutrFxOU\u003D(_param0);
  }

  internal static double \u0023\u003Dzb9UCYbo\u003D(this double _param0) => _param0;

  internal static double \u0023\u003Dzb9UCYbo\u003D(this IComparable _param0)
  {
    return _param0 is double num ? num : \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNXCE4EvjzL\u0024mX84Druo\u003D.\u0023\u003Dzb9UCYbo\u003D(_param0);
  }

  internal static double[] \u0023\u003DzvczFDIa7rqI9<T>(this T[] _param0) where T : IComparable
  {
    return ((IEnumerable<T>) _param0).Select<T, double>(\u0023\u003DzduViKcXTrKCfnYwdbArizhKjvBq3lpdPWarUHV\u002407Pk\u0024.\u0023\u003DzHKPvivPHfuGd30DNcQ\u003D\u003D<T>.\u0023\u003DzJ1auo2GPZ5hDBQwI8w\u003D\u003D ?? (\u0023\u003DzduViKcXTrKCfnYwdbArizhKjvBq3lpdPWarUHV\u002407Pk\u0024.\u0023\u003DzHKPvivPHfuGd30DNcQ\u003D\u003D<T>.\u0023\u003DzJ1auo2GPZ5hDBQwI8w\u003D\u003D = new Func<T, double>(\u0023\u003DzduViKcXTrKCfnYwdbArizhKjvBq3lpdPWarUHV\u002407Pk\u0024.\u0023\u003DzHKPvivPHfuGd30DNcQ\u003D\u003D<T>.\u0023\u003DzhxV_97w\u003D.\u0023\u003DzGBBVR4\u0024_hO74jzC_CeEppVE\u003D))).ToArray<double>();
  }

  internal static DateTime \u0023\u003Dzxuo5aY4wjkaI(this IComparable _param0)
  {
    if (_param0 is DateTime dateTime)
      return dateTime;
    if (_param0 is TimeSpan timeSpan)
      return new DateTime(timeSpan.Ticks);
    return _param0.\u0023\u003DzutrFxOU\u003D() ? new DateTime(\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzOS5Il8E\u003D((long) Convert.ChangeType((object) _param0, typeof (long), (IFormatProvider) CultureInfo.InvariantCulture), DateTime.MinValue.Ticks, DateTime.MaxValue.Ticks)) : new DateTime();
  }

  internal static TimeSpan \u0023\u003Dzto51K8pl8UAh(this IComparable _param0)
  {
    if (_param0 is TimeSpan timeSpan)
      return timeSpan;
    if (_param0 is DateTime dateTime)
      return new TimeSpan(dateTime.Ticks);
    return _param0.\u0023\u003DzutrFxOU\u003D() ? new TimeSpan(\u0023\u003DzNCoz_cr7eiA6K6bzw3PTSRjrsuvJB3oFJPVFS7w\u003D.\u0023\u003DzOS5Il8E\u003D((long) Convert.ChangeType((object) _param0, typeof (long), (IFormatProvider) CultureInfo.InvariantCulture), TimeSpan.MinValue.Ticks, TimeSpan.MaxValue.Ticks)) : new TimeSpan();
  }

  [Serializable]
  private sealed class \u0023\u003DzHKPvivPHfuGd30DNcQ\u003D\u003D<\u0023\u003DzH9HNkng\u003D> where \u0023\u003DzH9HNkng\u003D : IComparable
  {
    public static readonly \u0023\u003DzduViKcXTrKCfnYwdbArizhKjvBq3lpdPWarUHV\u002407Pk\u0024.\u0023\u003DzHKPvivPHfuGd30DNcQ\u003D\u003D<\u0023\u003DzH9HNkng\u003D> \u0023\u003DzhxV_97w\u003D = new \u0023\u003DzduViKcXTrKCfnYwdbArizhKjvBq3lpdPWarUHV\u002407Pk\u0024.\u0023\u003DzHKPvivPHfuGd30DNcQ\u003D\u003D<\u0023\u003DzH9HNkng\u003D>();
    public static Func<\u0023\u003DzH9HNkng\u003D, double> \u0023\u003DzJ1auo2GPZ5hDBQwI8w\u003D\u003D;

    internal double \u0023\u003DzGBBVR4\u0024_hO74jzC_CeEppVE\u003D(
      \u0023\u003DzH9HNkng\u003D _param1)
    {
      return _param1.\u0023\u003Dzb9UCYbo\u003D();
    }
  }
}
