using System;
using System.Collections.Generic;


namespace fx.Definitions
{
    public struct WavePriceTimeInfo : IEquatable<WavePriceTimeInfo>
    {
        public readonly int TrendBeginIndex;
        public readonly int TrendEndIndex;
        public readonly int CounterTrendEndIndex;
        public readonly TrendDirection FibTrendDirection;
        public RangeEx< double > TrendMoveRange;
        public RangeEx< double > CounterTrendMovedRange;
        public bool NonOverlapped;

        public HHLL HHs;
        public HHLL HHLL2;



        /// <summary>
        /// Initializes a new instance of the <see cref="WavePriceTimeInfo"/> class.
        /// </summary>
        /// <param name="trendDirection"></param>
        /// <param name="trendBeginTime"></param>
        /// <param name="trendEndTime"></param>
        /// <param name="counterTrendEndTime"></param>
        /// <param name="trendMovedPips"></param>
        /// <param name="counterTrendMovedPips"></param>
        public WavePriceTimeInfo(
                                    TrendDirection trendDirection,
                                    int trendBeginTime,
                                    int trendEndTime,
                                    int counterTrendEndTime,
                                    RangeEx<double> trendMovedRange,
                                    RangeEx<double> counterTrendMovedRange,
                                    HHLL hhs,
                                    HHLL hhll2
                                )
        {
            FibTrendDirection      = trendDirection;

            TrendBeginIndex        = trendBeginTime;
            TrendEndIndex          = trendEndTime;
            CounterTrendEndIndex   = counterTrendEndTime;
            TrendMoveRange         = trendMovedRange;
            CounterTrendMovedRange = counterTrendMovedRange;
            NonOverlapped          = false;
            HHs                  = hhs;
            HHLL2                  = hhll2;
        }

        //public WavePriceTimeInfo( ElliottWaveCycle waveCycle,
        //                            TrendDirection trendDirection,
        //                            TimeSpan period,
        //                            long trendBeginTime,
        //                            int trendBeginIndex,
        //                            long trendEndTime,
        //                            int trendEndIndex,                                    
        //                            long counterTrendEndTime,
        //                            int counterTrendEndIndex,
        //                            RangeEx<double> trendMovedRange,
        //                            double trendMovedPips,
        //                            RangeEx<double> counterTrendMovedRange,
        //                            double counterTrendMovedPips )
        //{
        //    _waveCycle              = waveCycle;
        //    FibTrendDirection                    = trendDirection;
        //    _trendBeginTime         = trendBeginTime;
        //    _trendEndTime           = trendEndTime;
        //    _counterTrendEndTime    = counterTrendEndTime;
        //    _trendMoveRange         = trendMovedRange;
        //    _counterTrendMovedRange = counterTrendMovedRange;
        //    _trendMovedPips         = trendMovedPips;
        //    _counterTrendMovedPips  = counterTrendMovedPips;

        //    _trendBeginIndex        = trendBeginIndex;
        //    _trendEndIndex          = trendEndIndex;
        //    _counterTrendEndIndex   = counterTrendEndIndex;

        //    _period = period;

        //    if ( _trendMovedPips > 0 )
        //    {
        //        _retracementPercentage = _counterTrendMovedPips / _trendMovedPips * 100;
        //    }
        //}


        public double RetracementPercentage
        {
            get
            {
                var trendMove = Math.Abs( TrendMoveRange.UpperBound - TrendMoveRange.LowerBound );
                var retrace   = Math.Abs( CounterTrendMovedRange.UpperBound - CounterTrendMovedRange.LowerBound );

                if ( trendMove > 0 )
                {
                    return ( retrace / trendMove * 100 );
                }

                return 0;
            }
        }

        public double TrendMovedPips
        {
            get
            {
                return Math.Abs( TrendMoveRange.UpperBound - TrendMoveRange.LowerBound );
            }
        }

        public double CounterTrendMovedPips
        {
            get
            {
                return Math.Abs( CounterTrendMovedRange.UpperBound - CounterTrendMovedRange.LowerBound );
            }
        }




        public override string ToString()
        {
            string output = "";

            if ( TrendBeginIndex > 0 && TrendEndIndex > 0 && CounterTrendEndIndex > 0 )
            {
                if ( FibTrendDirection == TrendDirection.Uptrend )
                {
                    output = "[" + GlobalConstants.UpTrend + "] " + TrendBeginIndex + GlobalConstants.UpTrendArrow + TrendEndIndex + GlobalConstants.UpTrendRetracement + CounterTrendEndIndex;
                }
                else
                {
                    output = "[" + GlobalConstants.DownTrend + "] " + TrendBeginIndex + GlobalConstants.DownTrendRetracement + TrendEndIndex + GlobalConstants.DownTrendArrow + CounterTrendEndIndex;
                }
            }

            double percentage   = Math.Round( RetracementPercentage, 2 );

            var trendPips   = Math.Round( TrendMovedPips, 2 );
            var counterPips = Math.Round( CounterTrendMovedPips, 2 );

            if ( percentage > 0 && trendPips > 0 && counterPips > 0 )
            {
                string output2 = string.Format( " : Retracement = {1}/{2} = {0}%", percentage, counterPips, trendPips );

                output += output2;
            }

            return output;
        }

        public bool HasPriceTimeInfo()
        {
            return ( TrendBeginIndex > 0 && TrendEndIndex > 0 );
        }

        public WaveLabelPosition RetracmentInfoPosition( long barTime )
        {
            if ( barTime == TrendBeginIndex )
            {
                if ( FibTrendDirection == TrendDirection.Uptrend )
                {
                    return WaveLabelPosition.BOTTOM;
                }
                else if ( FibTrendDirection == TrendDirection.DownTrend )
                {
                    return WaveLabelPosition.TOP;
                }
            }
            else if ( barTime == TrendEndIndex )
            {
                if ( FibTrendDirection == TrendDirection.Uptrend )
                {
                    return WaveLabelPosition.TOP;
                }
                else if ( FibTrendDirection == TrendDirection.DownTrend )
                {
                    return WaveLabelPosition.BOTTOM;
                }
            }
            else if ( barTime == CounterTrendEndIndex )
            {
                if ( FibTrendDirection == TrendDirection.Uptrend )
                {
                    return WaveLabelPosition.BOTTOM;
                }
                else if ( FibTrendDirection == TrendDirection.DownTrend )
                {
                    return WaveLabelPosition.TOP;
                }
            }

            return WaveLabelPosition.UNKNOWN;
        }

        public string GetPriceTimeInfoString( long barTime )
        {
            string output = "";

            if ( barTime == CounterTrendEndIndex )
            {
                output = string.Format( "{0}%", Math.Round( RetracementPercentage, 2 ) );
            }

            //if ( HighToHigh > 0 )
            //{
            //    if ( output.Length > 0 )
            //    {
            //        output = output + Environment.NewLine + "HH-" + HighToHigh;
            //    }

            //    if ( _period == TimeSpan.FromDays( 1 ) )
            //    {
            //        TimeSpan difference = TimeSpan.Zero;

            //        if ( _counterTrendEndTime != -1 )
            //        {
            //            difference = _counterTrendEndTime.FromLinuxTime( ) - _trendBeginTime.FromLinuxTime( );
            //        }
            //        else
            //        {
            //            difference = _trendEndTime.FromLinuxTime( ) - _trendBeginTime.FromLinuxTime( );
            //        }


            //        output = output + ", C-" + difference.Days;
            //    }
            //}

            //if ( HighToLow > 0 )
            //{
            //    if ( output.Length > 0 )
            //    {
            //        output = output + Environment.NewLine + "HL-" + HighToLow;
            //    }

            //    if ( _period == TimeSpan.FromDays( 1 ) )
            //    {
            //        TimeSpan difference = TimeSpan.Zero;

            //        if ( _counterTrendEndTime != -1 )
            //        {
            //            difference = _counterTrendEndTime.FromLinuxTime( ) - _trendEndTime.FromLinuxTime( );
            //        }
            //        else
            //        {
            //            difference = _trendEndTime.FromLinuxTime( ) - _trendBeginTime.FromLinuxTime( );
            //        }


            //        output = output + ", C-" + difference.Days;
            //    }
            //}

            //if ( LowToLow > 0 )
            //{
            //    if ( output.Length > 0 )
            //    {
            //        output = output + Environment.NewLine + "LL-" + LowToLow;
            //    }

            //    if ( _period == TimeSpan.FromDays( 1 ) )
            //    {
            //        TimeSpan difference = TimeSpan.Zero;

            //        if ( _counterTrendEndTime != -1 )
            //        {
            //            difference = _counterTrendEndTime.FromLinuxTime( ) - _trendBeginTime.FromLinuxTime( );
            //        }
            //        else
            //        {
            //            difference = _trendEndTime.FromLinuxTime( ) - _trendBeginTime.FromLinuxTime( );
            //        }


            //        output = output + ", C-" + difference.Days;
            //    }
            //}

            //if ( LowToHigh > 0 )
            //{
            //    if ( output.Length > 0 )
            //    {
            //        output = output + Environment.NewLine + "LH-" + LowToHigh;
            //    }

            //    if ( _period == TimeSpan.FromDays( 1 ) )
            //    {
            //        TimeSpan difference = TimeSpan.Zero;

            //        if ( _counterTrendEndTime != -1 )
            //        {
            //            difference = _counterTrendEndTime.FromLinuxTime( ) - _trendEndTime.FromLinuxTime( );
            //        }
            //        else
            //        {
            //            difference = _trendEndTime.FromLinuxTime( ) - _trendBeginTime.FromLinuxTime( );
            //        }


            //        output = output + ", C-" + difference.Days;
            //    }
            //}

            return output;
        }

        public override bool Equals( object obj )
        {
            if ( obj is WavePriceTimeInfo )
            {
                return Equals( ( WavePriceTimeInfo ) obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( WavePriceTimeInfo first, WavePriceTimeInfo second )
        {
            return first.Equals( second );
        }

        public static bool operator !=( WavePriceTimeInfo first, WavePriceTimeInfo second )
        {
            return !( first == second );
        }

        public bool Equals( WavePriceTimeInfo other )
        {
            return TrendBeginIndex.Equals( other.TrendBeginIndex ) && TrendEndIndex.Equals( other.TrendEndIndex ) && CounterTrendEndIndex.Equals( other.CounterTrendEndIndex ) && FibTrendDirection.Equals( other.FibTrendDirection ) ;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = ( hashCode * 53 ) ^ TrendBeginIndex.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ TrendEndIndex.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ CounterTrendEndIndex.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ ( int ) FibTrendDirection;
                if ( TrendMoveRange != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<RangeEx<double>>.Default.GetHashCode( TrendMoveRange );
                }

                if ( CounterTrendMovedRange != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<RangeEx<double>>.Default.GetHashCode( CounterTrendMovedRange );
                }

                hashCode = ( hashCode * 53 ) ^ NonOverlapped.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<HHLL>.Default.GetHashCode( HHs );
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<HHLL>.Default.GetHashCode( HHLL2 );
                hashCode = ( hashCode * 53 ) ^ RetracementPercentage.GetHashCode();
                return hashCode;
            }
        }
    }
}

