//using Ecng.ComponentModel;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Nager.Date;

//namespace FreemindAITrade.Helpers
//{
//    public static class DatesHelper
//    {
//        public static bool GetDatesForMissingCandles( IEnumerable<DateTime> candleDates, DateTime? taskBegin, DateTime? taskEnd, ref PooledList<Range<DateTime>> missingDates )
//        {
//            var downloadRange = new Range<DateTime>( taskBegin.Value, taskEnd.HasValue ? taskEnd.Value : DateTime.UtcNow );

//            var days = ( downloadRange.Max - downloadRange.Min ).Days + 1;

//            IEnumerable<DateTime> allDates = Enumerable.Range( 0, days ).Select( n => downloadRange.Min.Date.AddDays( n ) );

//            var missedDates = allDates.Except( candleDates ).OrderBy( x => x );

//            PooledList<Range<DateTime>> continuousRange = new PooledList<Range<DateTime>>( );

//            Range<DateTime> currentRange = null;

//            foreach ( var missDate in missedDates )
//            {
//                if ( currentRange == null || missDate.Date != currentRange.Max.AddSeconds( 1 ).Date )
//                {
//                    var checkingDate = missDate.Date.AddDays( 1 );

//                    if ( DateSystem.IsPublicHoliday( checkingDate.Date, CountryCode.US ) || checkingDate.DayOfWeek == DayOfWeek.Saturday || checkingDate.DayOfWeek == DayOfWeek.Sunday )
//                    {

//                    }
//                    else
//                    {
//                        currentRange = new Range<DateTime>( missDate.Date, missDate.AddDays( 1 ).Date.AddSeconds( -1 ) );
//                        continuousRange.Add( currentRange );
//                    }
//                }
//                else
//                {
//                    // add to current group
//                    currentRange.Max = missDate.AddDays( 1 ).Date.AddSeconds( -1 );
//                }
//            }

//            missingDates = continuousRange;

//            return true;
//        }
//    }
//}
