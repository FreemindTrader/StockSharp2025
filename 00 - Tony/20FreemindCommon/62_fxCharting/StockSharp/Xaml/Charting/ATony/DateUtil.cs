﻿using fx.Charting.ATony;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Charting.AUtils
{
    internal static class TimeSpanExtensions
    {
        internal const double DaysInYear = 365.2425;
        internal const double DaysInMonth = 30.436875;

        internal static bool IsZero( this TimeSpan timeSpan )
        {
            return timeSpan == TimeSpan.Zero;
        }

        internal static TimeSpan FromMonths( int numberMonths )
        {
            return TimeSpan.FromDays( numberMonths * 30.436875 );
        }

        internal static TimeSpan FromWeeks( int numberWeeks )
        {
            return TimeSpan.FromDays( numberWeeks * 7 );
        }

        public static TimeSpan FromYears( int numberYears )
        {
            return TimeSpan.FromDays( numberYears * 365.2425 );
        }

        public static bool IsDivisibleBy( this TimeSpan current, TimeSpan other )
        {
            return NumberUtil.IsDivisibleBy( current.Ticks, ( double )other.Ticks );
        }

        internal static bool IsAdditionValid( this TimeSpan current, TimeSpan delta )
        {
            bool flag = false;
            if ( current + delta < TimeSpan.MaxValue )
            {
                flag = true;
            }

            return flag;
        }
    }
    internal static class DateUtil
    {
        private static readonly PooledList<int> QuarterMonths = new PooledList<int>(   new int[ 5 ]
        {
          1,
          4,
          7,
          10,
          13
        } );

        private static readonly PooledList<int> HalfMonths = new PooledList<int>(   new int[ 3 ]
        {
          1,
          7,
          13
        } );

        private static readonly PooledList<int> BiMonths = new PooledList<int>(   new int[ 7 ]
        {
          1,
          3,
          5,
          7,
          9,
          11,
          13
        } );

        private static readonly TimeSpan OneYear = TimeSpanExtensions.FromYears( 1 );
        private static readonly TimeSpan SixMonths = TimeSpanExtensions.FromMonths( 6 );
        private static readonly TimeSpan ThreeMonths = TimeSpanExtensions.FromMonths( 3 );
        private static readonly TimeSpan OneMonth = TimeSpanExtensions.FromMonths( 1 );

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
            if ( dateSpan.Ticks % OneYear.Ticks == 0L && current.Day == 1 && current.Month == 1 || dateSpan.Ticks % TimeSpanExtensions.FromMonths( 1 ).Ticks == 0L && current.Day == 1 )
            {
                return true;
            }

            return RoundUp( current, dateSpan ).Equals( current );
        }

        internal static DateTime RoundUp( DateTime current, TimeSpan dateSpan )
        {
            if ( dateSpan.IsDivisibleBy( OneYear ) )
            {
                long num = dateSpan.Ticks / OneYear.Ticks;
                if ( current.Day == 1 && current.Month == 1 && NumberUtil.IsDivisibleBy( current.Year, ( double )num ) )
                {
                    return current;
                }

                return new DateTime( ( int )NumberUtil.RoundUp( current.Year + 1, ( double )num ), 1, 1 );
            }
            if ( dateSpan.IsDivisibleBy( SixMonths ) )
            {
                if ( current.Day == 1 && HalfMonths.Contains( current.Month ) )
                {
                    return current;
                }

                int newMonth = current.Month < 7 ? 7 : 13;
                return NewAlignedDateTime( current.Year, newMonth );
            }
            if ( dateSpan.IsDivisibleBy( ThreeMonths ) )
            {
                if ( current.Day == 1 && QuarterMonths.Contains( current.Month ) )
                {
                    return current;
                }

                int newMonth = current.Month < 4 ? 4 : ( current.Month < 7 ? 7 : ( current.Month < 10 ? 10 : 13 ) );
                return NewAlignedDateTime( current.Year, newMonth );
            }
            if ( !dateSpan.IsDivisibleBy( OneMonth ) )
            {
                return new DateTime( ( long )NumberUtil.RoundUp( current.Ticks, ( double )dateSpan.Ticks ) );
            }

            if ( current.Day == 1 )
            {
                return current;
            }

            long num1 =   ( int )( dateSpan.Ticks / OneMonth.Ticks );
            int newMonth1 = ( int )NumberUtil.RoundUp(    current.Month + 1 , ( double )num1 );
            return NewAlignedDateTime( current.Year, newMonth1 );
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

            int num2 = year % ( int )amount;
            return new DateTime( year + ( ( int )amount - num2 ), 1, 1 );
        }

        private static DateTime AlignToMonths( DateTime current, Decimal amount )
        {
            int num1 = ( int )amount;
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

                int num3 = month1 % ( int )amount;
                num2 = month1 + ( num3 + 1 );
            }
            else
            {
                PooledList<int> source = BiMonths;
                if ( num1 == 2 )
                {
                    source = BiMonths;
                }

                if ( num1 == 3 )
                {
                    source = QuarterMonths;
                }

                if ( num1 == 6 )
                {
                    source = HalfMonths;
                }

                num2 = source.First( x => x > current.Month );
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
            int month2 = NumberUtil.Constrain( num2, 1, 12 );
            return new DateTime( year, month2, 1 );
        }

        private static DateTime AlignToWeeks( DateTime current, Decimal amount )
        {
            if ( current.DayOfWeek == DayOfWeek.Monday )
            {
                return current;
            }

            return current.AddDays( ( double )( 8 - current.DayOfWeek ) ).Date;
        }

        public static int WeekNumber( this DateTime dt )
        {
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear( dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday );
        }

        private static DateTime AlignToDays( DateTime current, Decimal amount )
        {
            TimeSpan timeSpan = TimeSpan.FromDays( ( double )amount );
            return new DateTime( ( long )NumberUtil.RoundUp( current.Ticks, ( double )timeSpan.Ticks ) );
        }

        private static DateTime AlignToSeconds( DateTime current, Decimal amount )
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds( ( double )amount );
            return new DateTime( ( long )NumberUtil.RoundUp( current.Ticks, ( double )timeSpan.Ticks ) );
        }
    }
}

