using System;

namespace StockSharp.Xaml.Charting;

/// <summary>
/// Some Timespan extension methods.
/// </summary>
internal static class TimeSpanExtensions
{
    internal const double DaysInYear = 365.2425;
    internal const double DaysInMonth = 30.436875;

    internal static bool IsZero(this TimeSpan timeSpan)
    {
        return timeSpan == TimeSpan.Zero;
    }

    internal static TimeSpan FromMonths(int numberMonths)
    {
        return TimeSpan.FromDays((double) numberMonths * 30.436875);
    }

    internal static TimeSpan FromWeeks(int numberWeeks)
    {
        return TimeSpan.FromDays((double) (numberWeeks * 7));
    }

    public static TimeSpan FromYears(int numberYears)
    {
        return TimeSpan.FromDays((double) numberYears * 365.2425);
    }

    public static bool IsDivisibleBy(this TimeSpan current, TimeSpan other)
    {
        return NumberUtil.IsDivisibleBy((double) current.Ticks, (double) other.Ticks);
    }

    internal static bool IsAdditionValid(this TimeSpan current, TimeSpan delta)
    {
        bool flag = false;
        if(current + delta < TimeSpan.MaxValue)
            flag = true;
        return flag;
    }
}
