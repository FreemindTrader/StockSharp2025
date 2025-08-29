using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSharp.Xaml.Charting;
internal static class IComparableExtensions
{
    internal static bool IsDefined(this IComparable c)
    {
        if ( c != null )
            return ComparableUtil.IsDefined(c);
        return false;
    }

    internal static double ToDouble(this double c)
    {
        return c;
    }

    internal static double ToDouble(this IComparable c)
    {
        if ( c is double )
            return (double)c;
        return ComparableUtil.ToDouble(c);
    }

    internal static double[] ToDoubleArray<T>(this T[] inputArray) where T : IComparable
    {
        return ( (IEnumerable<T>)inputArray ).Select<T, double>((Func<T, double>)( x => x.ToDouble() )).ToArray<double>();
    }

    internal static DateTime ToDateTime(this IComparable c)
    {
        if ( c is DateTime )
            return (DateTime)c;
        if ( c is TimeSpan )
            return new DateTime(( (TimeSpan)c ).Ticks);
        if ( !c.IsDefined() )
            return new DateTime();
        long num = (long)Convert.ChangeType((object)c, typeof(long), (IFormatProvider)CultureInfo.InvariantCulture);
        DateTime dateTime = DateTime.MinValue;
        long ticks1 = dateTime.Ticks;
        dateTime = DateTime.MaxValue;
        long ticks2 = dateTime.Ticks;
        return new DateTime(NumberUtil.Constrain(num, ticks1, ticks2));
    }

    internal static TimeSpan ToTimeSpan(this IComparable c)
    {
        if ( c is TimeSpan )
            return (TimeSpan)c;
        if ( c is DateTime )
            return new TimeSpan(( (DateTime)c ).Ticks);
        if ( !c.IsDefined() )
            return new TimeSpan();
        long num = (long)Convert.ChangeType((object)c, typeof(long), (IFormatProvider)CultureInfo.InvariantCulture);
        TimeSpan timeSpan = TimeSpan.MinValue;
        long ticks1 = timeSpan.Ticks;
        timeSpan = TimeSpan.MaxValue;
        long ticks2 = timeSpan.Ticks;
        return new TimeSpan(NumberUtil.Constrain(num, ticks1, ticks2));
    }
}