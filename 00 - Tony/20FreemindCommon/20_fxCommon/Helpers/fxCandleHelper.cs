using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fx.Collections;
using Ecng.Collections;
using Nager.Date;
using StockSharp.Algo.Storages;
using StockSharp.Messages;

namespace fx.Common
{
    public static class fxCandleHelper
    {
        public static Tuple<DateTimeOffset, DateTimeOffset> GetRange( IMarketDataStorage storage, DateTimeOffset? from, DateTimeOffset? to, TimeSpan daysLoad )
        {
            var last = storage.Dates.LastOr( );

            if ( last == null )
            {
                return null;
            }
            if ( to == null )
            {
                to = last.Value;
            }
            if ( from == null )
            {
                from = to.Value - daysLoad;
            }
            return Tuple.Create( from.Value, to.Value );
        }

        public static Tuple< PooledList<DownloadRange>, PooledList<DownloadRange>> PrepareDownloadRange( IMarketDataStorage<CandleMessage> storage, TimeSpan period, DateTime beginTime, DateTime? endTime )
        {
            var downloadBackward = new PooledList<DownloadRange>( );
            var downloadForward  = new PooledList<DownloadRange>( );

            if ( period == TimeSpan.FromMinutes( 1 ) )
            {

            }

            var candleDates      = storage.Dates;
            var currentTime      = DateTime.UtcNow;

            var taskBegin        = beginTime;
            var taskEnd          = endTime.HasValue ? endTime.Value : currentTime;

            var storeLast        = candleDates.LastOr( );
            var storeFirst       = candleDates.FirstOr( );
            var lastBarInStorage = DateTime.MinValue;

            IMarketDataMetaInfo meta = null;
            bool firstTimeWrong = false;
            bool endTimeWrong = false;
            bool sizeWrong = false;

            if ( period != TimeSpan.FromMinutes( 4 ) && period != TimeSpan.FromSeconds( 1 ) )
            {
                if ( storeLast.HasValue )
                {
                    if ( HolidaySystem.IsPublicHoliday( storeLast.Value.Date, CountryCode.US ) || storeLast.Value.DayOfWeek == DayOfWeek.Saturday || storeLast.Value.DayOfWeek == DayOfWeek.Sunday )
                    {
                        goto DontNeedToCheck;
                    }

                    meta = storage.GetMetaInfo( storeLast.Value );

                    if ( meta != null )
                    {
                        var first   = meta.FirstTime;
                        var last    = meta.LastTime;

                        var nextDay = storeLast.Value.AddDays( 1 );
                        var prevDay = storeLast.Value.AddDays( -1 );

                        // Pre Holiday or Friday, exchange normally close at 5pm EST or even earlier.
                        if ( HolidaySystem.IsPublicHoliday( nextDay, CountryCode.US ) || storeLast.Value.DayOfWeek == DayOfWeek.Friday )
                        {
                            goto DontNeedToCheck;
                        }

                        var normalLastDate = GetLastBarOfNormalDay( last, period );

                        // Yesterday was holiday, today we should be back to normal, so closing time should be the same now.
                        if ( HolidaySystem.IsPublicHoliday( prevDay, CountryCode.US ) )
                        {
                            goto DontNeedToCheck; 
                        }

                        var firstTimeOfDay = first.TimeOfDay;

                        if ( firstTimeOfDay.TotalSeconds != 0 )
                        {
                            var diff = firstTimeOfDay;
                            var allowance = TimeSpan.FromTicks( period.Ticks * GetBarCountErrorAllowance( period ) );

                            if ( diff > allowance )
                            {
                                firstTimeWrong = true;
                            }
                        }

                        var timeDiff = last - first;
                        var barCnt = timeDiff.TotalSeconds / period.TotalSeconds + 1;

                        if ( barCnt < GetMininumBar( period ) )
                        {
                            sizeWrong = true;
                        }


                        if ( last != normalLastDate )
                        {
                            var diff2 = normalLastDate - last;
                            var allowance = TimeSpan.FromTicks( period.Ticks * GetBarCountErrorAllowance( period ) );

                            if ( diff2 > allowance )
                            {
                                endTimeWrong = true;
                            }
                        }

                        if ( ( endTimeWrong || firstTimeWrong ) && sizeWrong )
                        {
                            // Don't care about everything pre 2010. 
                            if ( first.Year > 2010 )
                            {
                                storage.Delete( storeLast.Value );
                                //missingDates.Add( new MissingBarInfo( period, first, last, meta.Count ) );
                            }
                        }
                        else
                        {
                            lastBarInStorage = last;
                        }
                    }
                }
            }

DontNeedToCheck:  
            PooledList<Ecng.ComponentModel.Range<DateTime>> missingDates = new PooledList<Ecng.ComponentModel.Range<DateTime>>( );

            if ( storeLast == null && storeFirst == null )
            {
                downloadBackward.Add( new DownloadRange( taskBegin, DateTime.UtcNow, 1 ) );
            }
            else if ( storeFirst.HasValue && storeLast.HasValue )
            {
                /* --------------------------------------------------------------------------------------------------------------------------
                * This case means we have date in the storage.
                * 
                *  1) We want to Start downloading from the most recent candles in storage to Now. 
                * 
                * --------------------------------------------------------------------------------------------------------------------------*/

                if ( lastBarInStorage != DateTime.MinValue )
                {
                    var start = lastBarInStorage.AddTicks( 1 );
                    downloadForward.Add( new DownloadRange( start, DateTime.UtcNow, 1 ) );
                }
                else
                {
                    storeLast = storage.Dates.LastOr( );
                    downloadForward.Add( new DownloadRange( storeLast.Value, DateTime.UtcNow, 1 ) );
                }

                

                DatesHelper.GetDatesRangeForMissingBars( period,  candleDates, taskBegin, storeLast.Value, ref missingDates );

                /* --------------------------------------------------------------------------------------------------------------------------                             
                * 
                *  2) We want to check that whatever in our storage is of continous nature and refill those that are missing data. 
                * 
                * --------------------------------------------------------------------------------------------------------------------------*/
                if ( missingDates != null )
                {
                    var closestFirst = missingDates.OrderByDescending( x => x.Min );

                    int proirity = 1;

                    foreach ( var missingDate in closestFirst )
                    {
                        downloadBackward.Add( new DownloadRange( missingDate.Min, missingDate.Max, proirity++ ) );
                    }
                }
            }

            return new Tuple< PooledList< DownloadRange >, PooledList< DownloadRange > >( downloadForward, downloadBackward );
        }

        //public static DateTime GetFirstBarOfNormalDay( DateTime first, TimeSpan period )
        //{
        //    DateTime firstBarDate = first;

        //    if ( DateSystem.IsPublicHoliday( first, Nager.Date.CountryCode.US ) || first.DayOfWeek == DayOfWeek.Saturday || first.DayOfWeek == DayOfWeek.Sunday )
        //    {
        //        // Since it special day, so the open and close time can be different.

        //        return first;
        //    }
        //    else if ( first.DayOfWeek == DayOfWeek.Friday )
        //    {
        //        TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById( "Eastern Standard Time" );
        //        DateTime barTime = TimeZoneInfo.ConvertTimeFromUtc( first, tz );

        //        DateTime date = barTime.Date;

        //        DateTime estDate = date;

        //        if ( period == TimeSpan.FromSeconds( 1 ) )
        //        {
        //            estDate = date.AddHours( 16 ).AddMinutes( 59 ).AddSeconds( 59 );
        //        }
        //        else if ( period == TimeSpan.FromMinutes( 1 ) )
        //        {
        //            estDate = date.AddHours( 16 ).AddMinutes( 59 );
        //        }
        //        else if ( period == TimeSpan.FromMinutes( 5 ) )
        //        {
        //            estDate = date.AddHours( 16 ).AddMinutes( 55 );
        //        }
        //        else if ( period == TimeSpan.FromMinutes( 15 ) )
        //        {
        //            estDate = date.AddHours( 16 ).AddMinutes( 45 );
        //        }
        //        else if ( period == TimeSpan.FromMinutes( 30 ) )
        //        {
        //            estDate = date.AddHours( 16 ).AddMinutes( 30 );
        //        }
        //        else if ( period == TimeSpan.FromHours( 1 ) )
        //        {
        //            estDate = date.AddHours( 16 );
        //        }
        //        else if ( period == TimeSpan.FromHours( 2 ) )
        //        {
        //            estDate = date.AddHours( 16 );
        //        }
        //        else if ( period == TimeSpan.FromHours( 3 ) )
        //        {
        //            estDate = date.AddHours( 15 );
        //        }
        //        else if ( period == TimeSpan.FromHours( 4 ) )
        //        {
        //            estDate = date.AddHours( 16 );
        //        }
        //        else if ( period == TimeSpan.FromHours( 6 ) )
        //        {
        //            estDate = date.AddHours( 12 );
        //        }
        //        else if ( period == TimeSpan.FromHours( 8 ) )
        //        {
        //            estDate = date.AddHours( 16 );
        //        }

        //        firstBarDate = TimeZoneInfo.ConvertTimeToUtc( estDate, tz );
        //    }
        //    else
        //    {
        //        DateTime date = first.Date;

        //        if ( period == TimeSpan.FromSeconds( 1 ) )
        //        {
        //            firstBarDate = date.AddHours( 23 ).AddMinutes( 59 ).AddSeconds( 59 );
        //        }
        //        else if ( period == TimeSpan.FromMinutes( 1 ) )
        //        {
        //            firstBarDate = date.AddHours( 23 ).AddMinutes( 59 );
        //        }
        //        else if ( period == TimeSpan.FromMinutes( 5 ) )
        //        {
        //            firstBarDate = date.AddHours( 23 ).AddMinutes( 55 );
        //        }
        //        else if ( period == TimeSpan.FromMinutes( 15 ) )
        //        {
        //            firstBarDate = date.AddHours( 23 ).AddMinutes( 45 );
        //        }
        //        else if ( period == TimeSpan.FromMinutes( 30 ) )
        //        {
        //            firstBarDate = date.AddHours( 23 ).AddMinutes( 30 );
        //        }
        //        else if ( period == TimeSpan.FromHours( 1 ) )
        //        {
        //            firstBarDate = date.AddHours( 23 );
        //        }
        //        else if ( period == TimeSpan.FromHours( 2 ) )
        //        {
        //            firstBarDate = date.AddHours( 22 );
        //        }
        //        else if ( period == TimeSpan.FromHours( 3 ) )
        //        {
        //            firstBarDate = date.AddHours( 21 );
        //        }
        //        else if ( period == TimeSpan.FromHours( 4 ) )
        //        {
        //            firstBarDate = date.AddHours( 20 );
        //        }
        //        else if ( period == TimeSpan.FromHours( 6 ) )
        //        {
        //            firstBarDate = date.AddHours( 18 );
        //        }
        //        else if ( period == TimeSpan.FromHours( 8 ) )
        //        {
        //            firstBarDate = date.AddHours( 16 );
        //        }
        //    }

        //    return firstBarDate;
        //}

        public static int GetBarCountErrorAllowance( TimeSpan period )
        {
            if ( period == TimeSpan.FromSeconds( 1 ) )
            {
                return 60;
            }
            else if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                return 15;
            }
            else if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                return 2;
            }            

            return 0;
        }

        public static int GetMininumBar( TimeSpan period )
        {
            if ( period == TimeSpan.FromSeconds( 1 ) )
            {
                return 60000;
            }
            else if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                return 1000;
            }
            else if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                return 288;
            }
            else if ( period == TimeSpan.FromMinutes( 15 ) )
            {
                return 96;
            }
            else if ( period == TimeSpan.FromMinutes( 30 ) )
            {
                return 48;
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

            return 1;
        }


        


        public static DateTime GetLastBarOfNormalDay( DateTime last, TimeSpan period )
        {
            DateTime lastBar = last;

            if ( HolidaySystem.IsPublicHoliday( last, CountryCode.US ) || last.DayOfWeek == DayOfWeek.Saturday || last.DayOfWeek == DayOfWeek.Sunday )
            {
                // Since it special day, so the open and close time can be different.

                return last;
            }
            else if ( last.DayOfWeek == DayOfWeek.Friday )
            {
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById( "Eastern Standard Time" );
                DateTime barTime = TimeZoneInfo.ConvertTimeFromUtc( last, tz );

                DateTime date = barTime.Date;

                DateTime estDate = date;

                if ( period == TimeSpan.FromSeconds( 1 ) )
                {
                    estDate = date.AddHours( 16 ).AddMinutes( 59 ).AddSeconds( 59 );
                }
                else if ( period == TimeSpan.FromMinutes( 1 ) )
                {
                    estDate = date.AddHours( 16 ).AddMinutes( 59 );
                }
                else if ( period == TimeSpan.FromMinutes( 5 ) )
                {
                    estDate = date.AddHours( 16 ).AddMinutes( 55 );
                }
                else if ( period == TimeSpan.FromMinutes( 15 ) )
                {
                    estDate = date.AddHours( 16 ).AddMinutes( 45 );
                }
                else if ( period == TimeSpan.FromMinutes( 30 ) )
                {
                    estDate = date.AddHours( 16 ).AddMinutes( 30 );
                }
                else if ( period == TimeSpan.FromHours( 1 ) )
                {
                    estDate = date.AddHours( 16 );
                }
                else if ( period == TimeSpan.FromHours( 2 ) )
                {
                    estDate = date.AddHours( 16 );
                }
                else if ( period == TimeSpan.FromHours( 3 ) )
                {
                    estDate = date.AddHours( 15 );
                }
                else if ( period == TimeSpan.FromHours( 4 ) )
                {
                    estDate = date.AddHours( 16 );
                }
                else if ( period == TimeSpan.FromHours( 6 ) )
                {
                    estDate = date.AddHours( 12 );
                }
                else if ( period == TimeSpan.FromHours( 8 ) )
                {
                    estDate = date.AddHours( 16 );
                }

                lastBar = TimeZoneInfo.ConvertTimeToUtc( estDate, tz );
            }        
            else
            {
                DateTime date = last.Date;

                if ( period == TimeSpan.FromSeconds( 1 ) )
                {
                    lastBar = date.AddHours( 23 ).AddMinutes( 59 ).AddSeconds( 59 );
                }
                else if ( period == TimeSpan.FromMinutes( 1 ) )
                {
                    lastBar = date.AddHours( 23 ).AddMinutes( 59 );
                }
                else if ( period == TimeSpan.FromMinutes( 5 ) )
                {
                    lastBar = date.AddHours( 23 ).AddMinutes( 55 );
                }
                else if ( period == TimeSpan.FromMinutes( 15 ) )
                {
                    lastBar = date.AddHours( 23 ).AddMinutes( 45 );
                }
                else if ( period == TimeSpan.FromMinutes( 30 ) )
                {
                    lastBar = date.AddHours( 23 ).AddMinutes( 30 );
                }
                else if ( period == TimeSpan.FromHours( 1 ) )
                {
                    lastBar = date.AddHours( 23 );
                }
                else if ( period == TimeSpan.FromHours( 2 ) )
                {
                    lastBar = date.AddHours( 22 );
                }
                else if ( period == TimeSpan.FromHours( 3 ) )
                {
                    lastBar = date.AddHours( 21 );
                }
                else if ( period == TimeSpan.FromHours( 4 ) )
                {
                    lastBar = date.AddHours( 22 );
                }
                else if ( period == TimeSpan.FromHours( 6 ) )
                {
                    lastBar = date.AddHours( 18 );
                }
                else if ( period == TimeSpan.FromHours( 8 ) )
                {
                    lastBar = date.AddHours( 16 );
                }
            }

            return lastBar;
        }
    }

    public struct DownloadRange
    {
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; }

        public DownloadRange( DateTime beginDate, DateTime endDate, int priority)
        {
            BeginDate = beginDate;
            EndDate   = endDate;
            Priority  = priority;
        }
    }
}
