using fx.Collections;
using Ecng.ComponentModel;
using Nager.Date;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Common
{
    public static class DatesHelper
    {
        //public static TimeSpan ToTimeSpan( this BarPeriod period )
        //{
        //    TimeSpan output = TimeSpan.FromTicks( 1 );

        //    if ( period == BarPeriod.S1 )
        //    {
        //        output = TimeSpan.FromSeconds( 1 );
        //    }
        //    else if ( period == BarPeriod.m1 )
        //    {
        //        output = TimeSpan.FromMinutes( 1 );
        //    }
        //    else if ( period == BarPeriod.m4 )
        //    {
        //        output = TimeSpan.FromMinutes( 4 );
        //    }
        //    else if ( period == BarPeriod.m5 )
        //    {
        //        output = TimeSpan.FromMinutes( 5 );
        //    }
        //    else if ( period == BarPeriod.m15 )
        //    {
        //        output = TimeSpan.FromMinutes( 15 );
        //    }
        //    else if ( period == BarPeriod.m30 )
        //    {
        //        output = TimeSpan.FromMinutes( 30 );
        //    }
        //    else if ( period == BarPeriod.H1 )
        //    {
        //        output = TimeSpan.FromHours( 1 );
        //    }
        //    else if ( period == BarPeriod.H2 )
        //    {
        //        output = TimeSpan.FromHours( 2 );
        //    }
        //    else if ( period == BarPeriod.H3 )
        //    {
        //        output = TimeSpan.FromHours( 3 );
        //    }
        //    else if ( period == BarPeriod.H4 )
        //    {
        //        output = TimeSpan.FromHours( 4 );
        //    }
        //    else if ( period == BarPeriod.H6 )
        //    {
        //        output = TimeSpan.FromHours( 6 );
        //    }
        //    else if ( period == BarPeriod.H8 )
        //    {
        //        output = TimeSpan.FromHours( 8 );
        //    }
        //    else if ( period == BarPeriod.D1 )
        //    {
        //        output = TimeSpan.FromDays( 1 );
        //    }
        //    else if ( period == BarPeriod.W1 )
        //    {
        //        output = TimeSpan.FromDays( 7 );
        //    }
        //    else if ( period == BarPeriod.M1 )
        //    {
        //        output = TimeSpan.FromDays( 30 );
        //    }

        //    return output;
        //}

        public static bool GetDatesForMissingTicks( IEnumerable<DateTime> candleDates, DateTime? taskBegin, DateTime? taskEnd, ref PooledList<Range<DateTime>> missingDates )
        {
            var downloadRange = new Range<DateTime>( taskBegin.Value, taskEnd.HasValue ? taskEnd.Value : DateTime.UtcNow );

            var days = ( downloadRange.Max - downloadRange.Min ).Days + 1;

            IEnumerable<DateTime> allDates = Enumerable.Range( 0, days ).Select( n => downloadRange.Min.Date.AddDays( n ) );

            var missedDates = allDates.Except( candleDates ).OrderBy( x => x );

            PooledList<Range<DateTime>> continuousRange = new PooledList<Range<DateTime>>( );

            Range<DateTime> currentRange = null;

            foreach ( var missDate in missedDates )
            {
                if ( currentRange == null || missDate.Date != currentRange.Max.AddSeconds( 1 ).Date )
                {
                    var checkingDate = missDate.Date.AddDays( 1 );

                    if ( DateSystem.IsPublicHoliday( checkingDate.Date, Nager.Date.CountryCode.US ) || checkingDate.DayOfWeek == DayOfWeek.Saturday || checkingDate.DayOfWeek == DayOfWeek.Sunday )
                    {

                    }
                    else
                    {
                        currentRange = new Range<DateTime>( missDate.Date, missDate.AddDays( 1 ).Date.AddSeconds( -1 ) );
                        continuousRange.Add( currentRange );
                    }
                }
                else
                {
                    // add to current group
                    currentRange.Max = missDate.AddDays( 1 ).Date.AddSeconds( -1 );
                }
            }

            missingDates = continuousRange;

            return true;
        }

        public static bool GetDatesRangeForMissingBars( TimeSpan period, IEnumerable<DateTime> candleDates, DateTime? taskBegin, DateTime? taskEnd, ref PooledList<Range<DateTime>> missingDates )
        {
            var downloadRange = new Range<DateTime>( taskBegin.Value, taskEnd.HasValue ? taskEnd.Value : DateTime.UtcNow );

            var days = ( downloadRange.Max - downloadRange.Min ).Days + 1;

            IEnumerable<DateTime> allDates = Enumerable.Range( 0, days ).Select( n => downloadRange.Min.Date.AddDays( n ) );

            var missedDates = allDates.Except( candleDates ).OrderBy( x => x );

            if ( period > TimeSpan.FromDays( 1 ) )
            {
                if ( missedDates.Count() > 0  )
                {
                    var range = new Range< DateTime >( missedDates.First( ), candleDates.First() );
                    missingDates.Add( range );
                }
                else
                {
                    var range = new Range< DateTime >(taskBegin.Value, candleDates.First() );
                    missingDates.Add( range );
                }
                

                return true;
            }


            PooledList<Range<DateTime>> continuousRange = new PooledList<Range<DateTime>>( );

            Range<DateTime> currentRange = null;

            foreach ( var missDate in missedDates )
            {
                if ( currentRange == null || missDate.Date != currentRange.Max.AddSeconds( 1 ).Date )
                {
                    var checkingDate = missDate.Date;

                    if ( DateSystem.IsPublicHoliday( checkingDate.Date, Nager.Date.CountryCode.US ) || checkingDate.DayOfWeek == DayOfWeek.Saturday || checkingDate.DayOfWeek == DayOfWeek.Sunday )
                    {

                    }
                    else
                    {
                        currentRange = new Range<DateTime>( missDate.Date, missDate.AddDays( 1 ).Date.AddSeconds( -1 ) );
                        continuousRange.Add( currentRange );
                    }
                }
                else
                {
                    // add to current group
                    currentRange.Max = missDate.AddDays( 1 ).Date.AddSeconds( -1 );
                }
            }

            missingDates = continuousRange;

            return true;
        }

        public static bool GetDatesListForMissingBars( IEnumerable<DateTime> candleDates, DateTime? taskBegin, DateTime? taskEnd, ref PooledList<DateTime> missingDates )
        {
            var downloadRange = new Range<DateTime>( taskBegin.Value, taskEnd.HasValue ? taskEnd.Value : DateTime.UtcNow );

            var days = ( downloadRange.Max - downloadRange.Min ).Days + 1;

            IEnumerable<DateTime> allDates = Enumerable.Range( 0, days ).Select( n => downloadRange.Min.Date.AddDays( n ) );

            var missedDates = allDates.Except( candleDates ).OrderBy( x => x );

            PooledList<DateTime> continuousRange = new PooledList<DateTime>();

            DateTime iterateDate = DateTime.MinValue;

            foreach ( var missDate in missedDates )
            {
                if ( DateSystem.IsPublicHoliday( missDate.Date, Nager.Date.CountryCode.US ) || missDate.DayOfWeek == DayOfWeek.Saturday || missDate.DayOfWeek == DayOfWeek.Sunday )
                {

                }
                else
                {
                    continuousRange.Add( missDate );
                }
            }

            missingDates = continuousRange;

            return true;
        }

        public static bool isPublicHoliday( string symbol, DateTime date )
        {
            switch ( symbol )
            {
                case "EUR/USD":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.DE ) )
                    {
                        return true;
                    }
                }
                break;


                case "CHF/JPY":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.CH ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.JP ) )
                    {
                        return true;
                    }
                }
                break;

                case "GBP/CHF":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.CH ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.GB ) )
                    {
                        return true;
                    }
                }
                break;

                case "EUR/AUD":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.DE ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.AU ) )
                    {
                        return true;
                    }
                }
                break;

                case "EUR/CAD":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.DE ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.CA ) )
                    {
                        return true;
                    }
                }
                break;

                case "AUD/CAD":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.AU ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.CA ) )
                    {
                        return true;
                    }
                }
                break;

                case "CAD/JPY":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.JP ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.CA ) )
                    {
                        return true;
                    }
                }
                break;
                case "NZD/JPY":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.JP ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.NZ ) )
                    {
                        return true;
                    }
                }
                break;

                case "GBP/CAD":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.GB ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.CA ) )
                    {
                        return true;
                    }
                }
                break;

                case "AUD/NZD":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.AU ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.NZ ) )
                    {
                        return true;
                    }
                }
                break;

                case "USD/SEK":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.SE ) )
                    {
                        return true;
                    }
                }

                break;
                case "USD/DDK":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.DK ) )
                    {
                        return true;
                    }
                }

                break;
                case "EUR/SEK":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.DE ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.SE ) )
                    {
                        return true;
                    }
                }
                break;

                case "EUR/NOK":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.DE ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.NO ) )
                    {
                        return true;
                    }
                }
                break;

                case "USD/NOK":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.NO ) )
                    {
                        return true;
                    }
                }
                break;

                case "USD/MXN":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.MX ) )
                    {
                        return true;
                    }
                }
                break;
                case "AUD/CHF":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.AU ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.CH ) )
                    {
                        return true;
                    }
                }
                break;


                case "EUR/NZD":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.DE ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.NZ ) )
                    {
                        return true;
                    }
                }
                break;


                case "EUR/PLN":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.DE ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.PL ) )
                    {
                        return true;
                    }
                }
                break;

                case "USD/PLN":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.PL ) )
                    {
                        return true;
                    }
                }
                break;

                case "EUR/CZK":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.DE ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.CZ ) )
                    {
                        return true;
                    }
                }
                break;

                case "USD/CZK":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.CZ ) )
                    {
                        return true;
                    }
                }
                break;

                case "USD/ZAR":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.ZA ) )
                    {
                        return true;
                    }
                }
                break;

                case "USD/SGD":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.SG ) )
                    {
                        return true;
                    }
                }
                break;

                case "USD/HKD":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.HK ) )
                    {
                        return true;
                    }
                }
                break;

                case "EUR/DKK":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.DE ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.DK ) )
                    {
                        return true;
                    }
                }
                break;

                case "GBP/SEK":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.GB ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.SE ) )
                    {
                        return true;
                    }
                }
                break;

                case "NOK/JPY":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.NO ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.JP ) )
                    {
                        return true;
                    }
                }
                break;

                case "SEK/JPY":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.SE ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.JP ) )
                    {
                        return true;
                    }
                }
                break;

                case "SGD/JPY":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.SG ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.JP ) )
                    {
                        return true;
                    }
                }
                break;

                case "HKD/JPY":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.HK ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.JP ) )
                    {
                        return true;
                    }
                }
                break;

                case "ZAR/JPY":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.ZA ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.JP ) )
                    {
                        return true;
                    }
                }
                break;

                case "USD/TRY":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.TR ) )
                    {
                        return true;
                    }
                }
                break;

                case "EUR/TRY":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.DE ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.TR ) )
                    {
                        return true;
                    }
                }
                break;

                case "NZD/CHF":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.NZ ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.CH ) )
                    {
                        return true;
                    }
                }
                break;

                case "CAD/CHF":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.CA ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.CH ) )
                    {
                        return true;
                    }
                }
                break;

                case "NZD/CAD":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.NZ ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.CA ) )
                    {
                        return true;
                    }
                }
                break;


                case "CHF/SEK":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.CH ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.SE ) )
                    {
                        return true;
                    }
                }
                break;

                case "CHF/NOK":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.CH ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.NO ) )
                    {
                        return true;
                    }
                }
                break;

                case "EUR/HUF":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.DE ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.HU ) )
                    {
                        return true;
                    }
                }
                break;

                case "USD/HUF":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.HU ) )
                    {
                        return true;
                    }
                }
                break;

                case "TRY/JPY":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.TR ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.JP ) )
                    {
                        return true;
                    }
                }
                break;

                case "GBP/USD":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.GB ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) )
                    {
                        return true;
                    }
                }
                break;

                case "USD/CNH":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.CN ) )
                    {
                        return true;
                    }
                }

                break;

                case "EUR/JPY":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.DE ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.JP ) )
                    {
                        return true;
                    }
                }
                break;

                case "USD/JPY":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.JP ) )
                    {
                        return true;
                    }
                }
                break;

                case "GBP/JPY":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.GB ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.JP ) )
                    {
                        return true;
                    }
                }
                break;

                case "AUD/JPY":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.AU ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.JP ) )
                    {
                        return true;
                    }
                }
                break;

                case "USD/CHF":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.CH ) )
                    {
                        return true;
                    }
                }
                break;

                case "AUD/USD":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.AU ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) )
                    {
                        return true;
                    }
                }
                break;

                case "EUR/CHF":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.DE ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.CH ) )
                    {
                        return true;
                    }
                }
                break;

                case "EUR/GBP":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.DE ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.GB ) )
                    {
                        return true;
                    }
                }
                break;

                case "NZD/USD":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.NZ ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) )
                    {
                        return true;
                    }
                }
                break;

                case "USD/CAD":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.CA ) )
                    {
                        return true;
                    }
                }
                break;

                case "GBP/AUD":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.GB ) || DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.AU ) )
                    {
                        return true;
                    }
                }
                break;

                case "XAG/USD":
                {
                    if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) )
                    {
                        return true;
                    }
                }
                break;

                case "XAU/USD":
                if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) )
                {
                    return true;
                }
                break;

                case "UK100":
                if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.GB ) )
                {
                    return true;
                }
                break;

                case "USDOLLAR":
                if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) )
                {
                    return true;
                }
                break;

                case "GER30":
                if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.DE ) )
                {
                    return true;
                }
                break;

                case "FRA40":
                if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.FR ) )
                {
                    return true;
                }
                break;

                case "AUS200":
                if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.AU ) )
                {
                    return true;
                }
                break;

                case "ESP35":
                if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.ES ) )
                {
                    return true;
                }
                break;

                case "HKG33":
                if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.HK ) )
                {
                    return true;
                }
                break;

                case "ITA40":
                if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.IT ) )
                {
                    return true;
                }
                break;

                case "JPN225":
                if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.JP ) )
                {
                    return true;
                }
                break;

                case "NAS100":
                if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) )
                {
                    return true;
                }
                break;

                case "SPX500":
                if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) )
                {
                    return true;
                }
                break;

                case "SUI20":
                if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.CH ) )
                {
                    return true;
                }
                break;

                case "Copper":
                if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) )
                {
                    return true;
                }
                break;

                case "EUSTX50":
                if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.DE ) )
                {
                    return true;
                }
                break;

                case "US30":
                if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) )
                {
                    return true;
                }
                break;

                case "USOIL":
                if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) )
                {
                    return true;
                }
                break;

                case "UKOIL":
                if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.GB ) )
                {
                    return true;
                }
                break;

                case "NGAS":
                if ( DateSystem.IsPublicHoliday( date, Nager.Date.CountryCode.US ) )
                {
                    return true;
                }
                break;

            }

            return false;
        }

        public static int DailyMaxBars( this TimeSpan period )
        {
            if ( period == TimeSpan.FromSeconds( 1 ) )
            {
                return 24 * 60 * 60;
            }
            else if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                return 24 * 60;
            }
            else if ( period == TimeSpan.FromMinutes( 4 ) )
            {
                return 24 * 60 / 4 ;
            }
            else if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                return 24 * 60 / 5;
            }
            else if ( period == TimeSpan.FromMinutes( 15 ) )
            {
                return 24 * 60 / 15;
            }
            else if ( period == TimeSpan.FromMinutes( 30 ) )
            {
                return 24 * 60 / 30;
            }
            else if ( period == TimeSpan.FromHours( 1 ) )
            {
                return 24;
            }
            else if ( period == TimeSpan.FromHours( 2 ) )
            {
                return 12;
            }
            else if ( period == TimeSpan.FromHours( 3 ) )
            {
                return 8;
            }
            else if ( period == TimeSpan.FromHours( 4 ) )
            {
                return 6;
            }
            else if ( period == TimeSpan.FromHours( 6 ) )
            {
                return 4;
            }
            else if ( period == TimeSpan.FromHours( 8 ) )
            {
                return 3;
            }
            else if ( period == TimeSpan.FromDays( 1 ) )
            {
                return 1;
            }
            else if ( period == TimeSpan.FromDays( 7 ) )
            {
                return 1;
            }
            else if ( period == TimeSpan.FromDays( 30 ) )
            {
                return 1; 
            }
            else
            {
                return -1;
            }
        }
    }
}
