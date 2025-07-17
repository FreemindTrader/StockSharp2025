// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Utility.DateUtil
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
namespace Ecng.Xaml.Charting
{
    internal static class DateUtil
    {
        private static readonly List<int> QuarterMonths = new List<int>((IEnumerable<int>) new int[5]
        {
          1,
          4,
          7,
          10,
          13
        });

        private static readonly List<int> HalfMonths = new List<int>((IEnumerable<int>) new int[3]
        {
          1,
          7,
          13
        });

        private static readonly List<int> BiMonths = new List<int>((IEnumerable<int>) new int[7]
        {
          1,
          3,
          5,
          7,
          9,
          11,
          13
        });

        private static readonly TimeSpan OneYear     = TimeSpanExtensions.FromYears(1);
        private static readonly TimeSpan SixMonths   = TimeSpanExtensions.FromMonths(6);
        private static readonly TimeSpan ThreeMonths = TimeSpanExtensions.FromMonths(3);
        private static readonly TimeSpan OneMonth    = TimeSpanExtensions.FromMonths(1);

        internal static DateTime Max( DateTime a, DateTime b )
        {
            if ( a.Ticks <= b.Ticks )
            {
                return b;
            }

            return a;
        }

        internal static DateTime Min( DateTime a, DateTime b )
        {
            if ( a.Ticks >= b.Ticks )
            {
                return b;
            }

            return a;
        }

        internal static bool IsDivisibleBy( DateTime current, TimeSpan dateSpan )
        {
            if ( dateSpan.Ticks % DateUtil.OneYear.Ticks == 0L && current.Day == 1 && current.Month == 1 || dateSpan.Ticks % TimeSpanExtensions.FromMonths( 1 ).Ticks == 0L && current.Day == 1 )
            {
                return true;
            }

            return DateUtil.RoundUp( current, dateSpan ).Equals( current );
        }

        internal static DateTime RoundUp( DateTime current, TimeSpan dateSpan )
        {
            if ( dateSpan.IsDivisibleBy( DateUtil.OneYear ) )
            {
                long num = dateSpan.Ticks / DateUtil.OneYear.Ticks;
                if ( current.Day == 1 && current.Month == 1 && NumberUtil.IsDivisibleBy( ( double ) current.Year, ( double ) num ) )
                {
                    return current;
                }

                return new DateTime( ( int ) NumberUtil.RoundUp( ( double ) ( current.Year + 1 ), ( double ) num ), 1, 1 );
            }
            if ( dateSpan.IsDivisibleBy( DateUtil.SixMonths ) )
            {
                if ( current.Day == 1 && DateUtil.HalfMonths.Contains( current.Month ) )
                {
                    return current;
                }

                int newMonth = current.Month < 7 ? 7 : 13;
                return DateUtil.NewAlignedDateTime( current.Year, newMonth );
            }
            if ( dateSpan.IsDivisibleBy( DateUtil.ThreeMonths ) )
            {
                if ( current.Day == 1 && DateUtil.QuarterMonths.Contains( current.Month ) )
                {
                    return current;
                }

                int newMonth = current.Month < 4 ? 4 : (current.Month < 7 ? 7 : (current.Month < 10 ? 10 : 13));
                return DateUtil.NewAlignedDateTime( current.Year, newMonth );
            }
            if ( !dateSpan.IsDivisibleBy( DateUtil.OneMonth ) )
            {
                return new DateTime( ( long ) NumberUtil.RoundUp( ( double ) current.Ticks, ( double ) dateSpan.Ticks ) );
            }

            if ( current.Day == 1 )
            {
                return current;
            }

            long num1 = (long) (int) (dateSpan.Ticks / DateUtil.OneMonth.Ticks);
            int newMonth1 = (int) NumberUtil.RoundUp((double) (current.Month + 1), (double) num1);
            return DateUtil.NewAlignedDateTime( current.Year, newMonth1 );
        }

        private static DateTime NewAlignedDateTime( int newYear, int newMonth )
        {
            while ( newMonth > 12 )
            {
                newMonth -= 12;
                ++newYear;
            }
            return new DateTime( newYear, newMonth, 1 );
        }

        private static DateTime AlignToYears( DateTime current, Decimal amount )
        {
            int day = current.Day;
            int month = current.Month;
            int year = current.Year;
            int num1 = 1;
            if ( day == num1 && month == 1 )
            {
                --year;
            }

            int num2 = year % (int) amount;
            return new DateTime( year + ( ( int ) amount - num2 ), 1, 1 );
        }

        private static DateTime AlignToMonths( DateTime current, Decimal amount )
        {
            int num1 = (int) amount;
            int year = current.Year;
            int month1 = current.Month;
            int day = current.Day;
            int num2;
            if ( num1 == 1 )
            {
                if ( day == 1 )
                {
                    return current;
                }

                int num3 = month1 % (int) amount;
                num2 = month1 + ( num3 + 1 );
            }
            else
            {
                List<int> source = DateUtil.BiMonths;
                if ( num1 == 2 )
                {
                    source = DateUtil.BiMonths;
                }

                if ( num1 == 3 )
                {
                    source = DateUtil.QuarterMonths;
                }

                if ( num1 == 6 )
                {
                    source = DateUtil.HalfMonths;
                }

                num2 = source.First<int>( ( Func<int, bool> ) ( x => x > current.Month ) );
                if ( day == 1 )
                {
                    num2 -= num1;
                }
            }
            if ( num2 > 12 )
            {
                num2 -= 12;
                ++year;
            }
            int month2 = NumberUtil.Constrain(num2, 1, 12);
            return new DateTime( year, month2, 1 );
        }

        private static DateTime AlignToWeeks( DateTime current, Decimal amount )
        {
            if ( current.DayOfWeek == DayOfWeek.Monday )
            {
                return current;
            }

            return current.AddDays( ( double ) ( 8 - current.DayOfWeek ) ).Date;
        }

        public static int WeekNumber( this DateTime dt )
        {
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear( dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday );
        }

        private static DateTime AlignToDays( DateTime current, Decimal amount )
        {
            TimeSpan timeSpan = TimeSpan.FromDays((double) amount);
            return new DateTime( ( long ) NumberUtil.RoundUp( ( double ) current.Ticks, ( double ) timeSpan.Ticks ) );
        }

        private static DateTime AlignToSeconds( DateTime current, Decimal amount )
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds((double) amount);
            return new DateTime( ( long ) NumberUtil.RoundUp( ( double ) current.Ticks, ( double ) timeSpan.Ticks ) );
        }
    }
}
