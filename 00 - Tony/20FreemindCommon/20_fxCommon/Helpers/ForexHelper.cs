using fx.Definitions;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Common
{
    public static class ForexHelper
    {        
        

        public static bool IsForexPair( this Security sec )
        {
            if ( sec.Type == SecurityTypes.Currency )
            {
                return true;
            }

            return false;
        }

        public static int CalculateStorageSize( TimeSpan period )
        {
            int totalStorge = 0;

            if ( period == TimeSpan.FromTicks( 1 ) )
            {
                return ( GlobalConstants.TICKS_BARS_PER_DAY );
            }

            int extraBars = GetBarsBetweenLastBarAndEndOfWeek( DateTime.UtcNow, period );
            var hourlyBars = ( GlobalConstants.HOURLY_BARS_QUATERLY + GlobalConstants.HOURLY_BARS_PER_WEEK + extraBars + 1 );

            if ( period == TimeSpan.FromSeconds( 1 ) )
            {
                totalStorge = 100000;
            }
            else if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                totalStorge = hourlyBars * 10;
            }
            else if ( period == TimeSpan.FromMinutes( 4 ) )
            {
                totalStorge = hourlyBars * 5;
            }
            else if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                totalStorge = hourlyBars * 5;
            }
            else if ( period == TimeSpan.FromMinutes( 15 ) )
            {
                totalStorge = hourlyBars * 2;
            }
            else if ( period == TimeSpan.FromMinutes( 30 ) )
            {
                totalStorge = hourlyBars * 2;
            }
            else if ( period == TimeSpan.FromHours( 1 ) )
            {
                totalStorge = hourlyBars;
            }
            else if ( period == TimeSpan.FromHours( 2 ) )
            {
                totalStorge = hourlyBars;
            }
            else if ( period == TimeSpan.FromHours( 3 ) )
            {
                totalStorge = hourlyBars;
            }
            else if ( period == TimeSpan.FromHours( 4 ) )
            {
                totalStorge = hourlyBars;
            }
            else if ( period == TimeSpan.FromHours( 6 ) )
            {
                totalStorge = hourlyBars;
            }
            else if ( period == TimeSpan.FromHours( 8 ) )
            {
                totalStorge = hourlyBars;
            }
            else if ( period == TimeSpan.FromDays( 1 ) )
            {
                totalStorge = GlobalConstants.DAILY_BARS_50YEARS + GlobalConstants.DAILY_BARS_PER_WEEK + extraBars + 1;
            }
            else if ( period == TimeSpan.FromDays( 7 ) )
            {
                totalStorge = ( GlobalConstants.WEEKLY_BARS_30YEARS + 1 );
            }
            else if ( period == TimeSpan.FromDays( 30 ) )
            {
                totalStorge = GlobalConstants.MONTHLY_BARS_30YEARS;
            }
            else
            {
                totalStorge = GlobalConstants.ONE_MINUTES_BARS_PER_MONTH * 2 + 1;
            }

            return totalStorge;
        }

        public static int CalculateStorageSize( TimeSpan period, int miniBarCount )
        {
            int totalStorge = CalculateStorageSize( period );

            if ( totalStorge < miniBarCount && period != TimeSpan.FromTicks( 1 ) )
            {
                totalStorge = miniBarCount + GetBarsBetweenLastBarAndEndOfWeek( DateTime.UtcNow, period );
            }


            return totalStorge;
        }

        public static int GetBarsBetweenLastBarAndEndOfWeek( DateTime lastBarTime, TimeSpan period )
        {
            var comingFridayUTC = DateTimeHelper.ReturnNextNthWeekdaysOfMonth( DateTime.UtcNow, DayOfWeek.Friday, 1 ).First( );
            comingFridayUTC = comingFridayUTC.AddHours( 13 ); // now we have 1 pm UTC on certain Friday

            comingFridayUTC = DateTime.SpecifyKind( comingFridayUTC, DateTimeKind.Utc );
            var tz = TimeZoneInfo.FindSystemTimeZoneById( "Eastern Standard Time" );
            var barTime = TimeZoneInfo.ConvertTimeFromUtc( comingFridayUTC, tz );

            while ( barTime.Hour < 17 )
            {
                barTime = barTime.AddHours( 1 );
            }

            var closingTime = barTime.Date.AddHours( barTime.Hour );

            var utcTimeZone = TimeZoneInfo.FindSystemTimeZoneById( "UTC" );
            var estTimeZone = TimeZoneInfo.FindSystemTimeZoneById( "Eastern Standard Time" );

            var marketClosingTime = TimeZoneInfo.ConvertTime( closingTime, estTimeZone, utcTimeZone );

            var differenceTimeSpan = marketClosingTime - lastBarTime;

            long remainder;
            int count = ( int )Math.DivRem( differenceTimeSpan.Ticks, period.Ticks, out remainder );

            return count;
        }


        public static void GetStartAndEndDateForDatabar( TimeSpan period, out DateTime startDate, out DateTime endDate )
        {
            startDate = DateTime.UtcNow.Date;
            endDate = DateTime.UtcNow.AddMinutes( 5 );

            var totalbars = CalculateStorageSize( period );

            if ( period.Days == 30 )
            {
                startDate = DateTime.Today.AddMonths( -totalbars );
                endDate = DateTime.Now.AddDays( 1 );
            }
            else if ( period.Days == 7 )
            {
                startDate = DateTime.Today.AddDays( -totalbars * period.TotalDays );
            }
            else if ( period.Days == 1 )
            {
                startDate = DateTime.Today.AddDays( -totalbars * period.TotalDays );
            }
            else if ( period.Seconds == 1 )
            {
                endDate = DateTime.UtcNow.AddDays( 5 );
                startDate = DateTime.UtcNow.AddSeconds( -totalbars * period.TotalSeconds );
            }
            else if ( period.Ticks == 1 )
            {
                endDate = DateTime.UtcNow.AddMinutes( 5 );
                startDate = endDate.AddHours( -6 );
            }
            else
            {
                var d     = DateTime.Today.AddMinutes( -( totalbars * period.TotalMinutes ) );
                startDate = new DateTime( d.Year, d.Month, d.Day, d.Hour, 0, 0, DateTimeKind.Utc );
            }
        }

        public static string GetPeriodString( TimeSpan period )
        {
            string sPeriodId = "t1";

            if ( period.Days == 30 )
            {
                sPeriodId = "Monthly";
            }
            else if ( period.Days == 7 )
            {
                sPeriodId = "Weekly";
            }
            else if ( period.Days == 1 )
            {
                sPeriodId = "Daily";
            }
            else if ( period.Hours == 1 )
            {
                sPeriodId = "1 Hr";
            }
            else if ( period.Hours == 2 )
            {
                sPeriodId = "2 Hrs";
            }
            else if ( period.Hours == 3 )
            {
                sPeriodId = "3 Hrs";
            }
            else if ( period.Hours == 4 )
            {
                sPeriodId = "4 Hrs";
            }
            else if ( period.Hours == 6 )
            {
                sPeriodId = "6 Hrs";
            }
            else if ( period.Hours == 8 )
            {
                sPeriodId = "8 Hrs";
            }
            else if ( period.Minutes == 1 )
            {
                sPeriodId = "1 Min";
            }
            else if ( period.Minutes == 4 )
            {
                sPeriodId = "4 Min";
            }
            else if ( period.Minutes == 5 )
            {
                sPeriodId = "5 Min";
            }
            else if ( period.Minutes == 15 )
            {
                sPeriodId = "15 Min";
            }
            else if ( period.Minutes == 30 )
            {
                sPeriodId = "30 Min";
            }
            else if ( period.Ticks == 1 )
            {
                sPeriodId = "Tick";
            }

            return sPeriodId;
        }

        
    }
}

