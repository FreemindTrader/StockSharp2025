using System;
using System.Collections;
using fx.Collections;
using System.Drawing;
using fx.Definitions;
using fx.TALib;
using fx.TimePeriod;
using fx.Algorithm;
using DevExpress.Mvvm;
using fx.Bars;
using StockSharp.BusinessEntities;
using System.Linq;


namespace fx.Indicators
{
    /// <summary>
    /// This indicator is not tradeable since it changes its symbolPositionSummary and requres future looking.
    /// Indicator written manually no external lib used.
    /// </summary>
    [Serializable]
    [UserFriendlyName( "fxtrader.hk Pivot Points" )]
    public class PivotPointsCustom : CustomPlatformIndicator, IPivotPointIndicator
    {
        private OrderedDictionary< TimeBlockEx, PivotPointsInfo> _pivotPointsForDates       = new OrderedDictionary<TimeBlockEx, PivotPointsInfo>( );

        private PooledList< TimeBlockEx > _ppSearchTimeBlock = new PooledList< TimeBlockEx >( );

        
        DateTime _lastBarTime = DateTime.MinValue;

        
        private TimeSpan _pivotTimeSpan;
        
        DateTime _lastCheckBarTime = DateTime.MinValue;
        int _lastCheckBarIndex = 0;

        PivotPointsInfo _yesterdaysPPinfo = null;

        public TimeSpan PivotTimeSpan
        {
            get
            {
                return _pivotTimeSpan;
            }

            set
            {
                _pivotTimeSpan = value;
            }
        }


        public Security Security
        {
            get
            {
                return _indicatorSecurity;
            }

            set
            {
                _indicatorSecurity = value;
            }
        }


        // This is the focal price level or the mean that is derivedfrom the collective market data from the prior session’s high, low, and close. 
        // It is the strongest of the support and resistance numbers. Prices normally trade above or below this area before breaking in one direction or the other. 
        // As a general guideline, if the market opens above the primary pivot, be a buyer on dips. If the market opens below this level, look to sell rallies.
        public double Pivot
        {
            // Tony: For some weird reason, when we get the monthly DataBar, it doesn't include the current monthly NOT finish databar, so we have to return the last bar instead of the second last.
            get
            {
                var tb = GetPivotPointsAt( DateTime.UtcNow, out TimeBlockEx ignore );

                if ( tb != null )
                {
                    return tb.Pivot;
                }

                return 0;
            }
        }

        //public DateTime BarTime
        //{
        //    get
        //    {
        //        var tb = GetPivotPointsAt( DateTime.UtcNow, out TimeBlockEx ignore );

        //        if ( tb != null )
        //        {
        //            return tb.DataTime;
        //        }

        //        return 0;

        //        int index = GetLatestPivotIndex( );

        //        if ( index > 0 )
        //        {
        //            var bar = IndicatorBarsRepo[ index ];

        //            if ( bar != null )
        //            {
        //                return bar.BarTime;
        //            }
        //        }

        //        return DateTime.MinValue;
        //    }
        //}



        // Mild bullish to bearish projected high target number. In low volume or light volatility sessions or 
        // in consolidating trading periods, this often acts as the high of a given session. 
        // In a bearish market condition, prices will try to come close to this level but most times fail
        public double R1
        {
            get
            {
                var tb = GetPivotPointsAt( DateTime.UtcNow, out TimeBlockEx ignore );

                if ( tb != null )
                {
                    return tb.R1;
                }
                
                return 0;
            }

        }

        // Bullish market price objective or target high number for a trading session. It generally establishes the high of a given time period. 
        // The market often sees significant resistance at this price level and will provide an exit target for long positions.
        public double R2
        {
            get
            {
                var tb = GetPivotPointsAt( DateTime.UtcNow, out TimeBlockEx ignore );

                if ( tb != null )
                {
                    return tb.R2;
                }

                return 0;
            }
        }

        // Extreme bullish market condition generally created by news-driven price shock. 
        // This is where a market is at an overbought condition and may offer a day trader a quick reversal scalp trade.
        public double R3
        {
            get
            {
                var tb = GetPivotPointsAt( DateTime.UtcNow, out TimeBlockEx ignore );

                if ( tb != null )
                {
                    return tb.R3;
                }

                return 0;
            }
        }

        // Mild bearish to bullish projected low target number in light volume or low volatility sessions or in consolidating trading periods.
        // Prices tend to reverse at or near this level in bullish market conditions but most times fall short of hitting this number
        public double S1
        {
            get
            {
                var tb = GetPivotPointsAt( DateTime.UtcNow, out TimeBlockEx ignore );

                if ( tb != null )
                {
                    return tb.S1;
                }

                return 0;
            }
        }

        // Bearish market price objective or targeted low number. The market often sees significant support at or near this level in a bearish market condition and 
        // is a likely target level to cover shorts.
        public double S2
        {
            get
            {
                var tb = GetPivotPointsAt( DateTime.UtcNow, out TimeBlockEx ignore );

                if ( tb != null )
                {
                    return tb.S2;
                }

                return 0;


            }
        }

        // Extreme bearish market condition generally created by a news-driven price shock. 
        // This level will act as the projected target low or support area. 
        // This is where a market is at an oversold condition and may offer a day trader a quick reversal scalp trade
        public double S3
        {
            get
            {
                var tb = GetPivotPointsAt( DateTime.UtcNow, out TimeBlockEx ignore );

                if ( tb != null )
                {
                    return tb.S3;
                }

                return 0;
            }
        }


        private volatile bool _doneInitialDataLoad = false;

        public bool DoneInitialDataLoad
        {
            get
            {
                return _doneInitialDataLoad;
            }
        }
        

        public PivotPointsInfo GetPivotPointsAt( long barTime, out TimeBlockEx responsibleBlock )
        {
            var barTimeDt = barTime.FromLinuxTime( );

            return GetPivotPointsAt( barTimeDt, out responsibleBlock );
        }

        public TimeBlockEx GetTimeBlock( DateTime barTime )
        {
            if( _ppSearchTimeBlock.Count == 0 )
                return TimeBlockEx.EmptyBlock;

            if ( barTime > _ppSearchTimeBlock[0].End )
            {
                return TimeBlockEx.EmptyBlock;
            }
            
            foreach ( var timeBlock in _ppSearchTimeBlock )
            {
                if ( timeBlock.HasInside( barTime ) )
                {
                    return timeBlock;
                }                                
            }


            return TimeBlockEx.EmptyBlock;
        }

        public PivotPointsInfo GetPivotPointsAt( DateTime barTime, out TimeBlockEx responsibleBlock )
        {
            responsibleBlock = TimeBlockEx.EmptyBlock;

            if ( !_doneInitialDataLoad )
                return null;

            var timeBlock = GetTimeBlock( barTime );

            if ( timeBlock != TimeBlockEx.EmptyBlock )
            {
                return _pivotPointsForDates[ timeBlock ];
            }
            
            return null;
        }

        public double M3
        {
            get
            {
                return ( Pivot + R1 ) / 2;
            }
        }

        public double M2
        {
            get
            {
                return ( Pivot + S1 ) / 2;
            }
        }

        public double M4
        {
            get
            {
                return ( R2 + R1 ) / 2;
            }
        }

        public double M1
        {
            get
            {
                return ( S2 + S1 ) / 2;
            }
        }

        public double M5
        {
            get
            {
                return ( R2 + R3 ) / 2;
            }
        }

        public double M0
        {
            get
            {
                return ( S2 + S3 ) / 2;
            }
        }


        /// <summary>
        /// Tony: 
        /// Need to test this indicator to see if it works or not.
        /// </summary>
        public PivotPointsCustom( ) : base( typeof( PivotPointsCustom ).Name, false, false, true, new string[ ] { "R3", "R2", "R1", "Pivot", "S1", "S2", "S3", "MarketDirectionNo" } )

        {
        }

        

        protected override void OnCalculate( bool fullRecalculation, HistoricBarsUpdateEventArg e )
        {
            if ( ( e.UpdateType == DataBarUpdateType.Initial ) || ( e.UpdateType == DataBarUpdateType.HistoryUpdate ) )
            {
                _doneInitialDataLoad = false;
                               

                DateTime barDateUTC = DateTime.MinValue;

                _lastBarTime = DateTime.MinValue;

                _yesterdaysPPinfo = null;

                /* --------------------------------------------------------------------------------------------------------------
                 * index =  0        1        2       3       4
                 * Time  =  4/3      5/3      6/3     7/3     8/3
                 * Data  =  4/3      5/3      6/3     7/3     8/3
                 * Pivot =  5/3Piv   6/3Piv   7/3Piv  8/3Piv  11/3Piv  
                 * Block =  [4-5>/3  [5-6>/3  [6-7>/3 [7-8>/3 [8-11>
                 * --------------------------------------------------------------------------------------------------------------
                 */

                var beginIndex = Math.Max( 0, e.BeginIndex );
                var endIndex   = Math.Max( Bars.TotalBarCount - 1, e.EndIndex );

                for ( int i = (int)beginIndex; i <= endIndex; i++ )
                {
                    long barDateLinux = Bars[ i ].LinuxTime;
                    barDateUTC        = barDateLinux.FromLinuxTime( );

                    if ( _pivotTimeSpan == TimeSpan.FromDays( 1 ) && barDateUTC.DayOfWeek == DayOfWeek.Saturday )
                    {
                        continue;
                    }

                    if ( _pivotTimeSpan == TimeSpan.FromDays( 30 ) )
                    {
                        //continue;
                    }

                    var todaysPPInfo = GetPivotPointInfo( i, barDateUTC );

                    if ( _yesterdaysPPinfo != null )
                    {
                        if ( _yesterdaysPPinfo.DataTime < todaysPPInfo.DataTime )
                        {
                            /* -------------------------------------------------------------------------------------------------------------------------------------------
                             * 
                             *  Calculated Pivot Points from Previous trading day's data will be used for today's trading time
                             * 
                             * ------------------------------------------------------------------------------------------------------------------------------------------- */

                            var currentRange = GetPivotTimeBlock( todaysPPInfo.DataTime );
                            
                            _pivotPointsForDates.TryAdd( currentRange, _yesterdaysPPinfo );

                            var test = _pivotPointsForDates[currentRange];


                            if ( i == endIndex )
                            {
                                _ppSearchTimeBlock = _pivotPointsForDates.Keys.OrderByDescending( x => x.Start ).ToPooledList( );
                            }
                            
                        }   
                        else
                        {
                            /* -------------------------------------------------------------------------------------------------------------------------------------------
                             * 
                             *  We have an update of some Databars, most likely the reload of incomplete databars.
                             * 
                             * ------------------------------------------------------------------------------------------------------------------------------------------- */
                            var timeBlock = GetTimeBlock( barDateUTC );

                            if ( timeBlock != TimeBlockEx.EmptyBlock )
                            {
                                _pivotPointsForDates[ timeBlock ] = todaysPPInfo;
                            }
                        }
                    }                                                          

                    _lastBarTime           = barDateUTC;
                    _yesterdaysPPinfo      = todaysPPInfo;                    
                    _lastCheckBarIndex     = i;
                }                

                _doneInitialDataLoad = true;

                RaisePivotPointChanged( );
            }


        }

        private PivotPointsInfo GetPivotPointInfo( int i, DateTime dataTime )
        {
            var myPivot = Math.Round( ( Bars[ i ].High + Bars[ i ].Low + Bars[ i ].Close ) / 3, 5 );

            var pivot   = myPivot;
            var r1      = ( 2 * myPivot ) - Bars[ i ].Low;
            var r2      = myPivot + ( Bars[ i ].High - Bars[ i ].Low );
            var r3      = Bars[ i ].High + 2 * ( myPivot - Bars[ i ].Low );
            var s1      = ( 2 * myPivot ) - Bars[ i ].High;
            var s2      = myPivot - ( Bars[ i ].High - Bars[ i ].Low );
            var s3      = Bars[ i ].Low - 2 * ( Bars[ i ].High - myPivot );

            var m5      = ( r2 + r3 ) / 2;
            var m4      = ( r2 + r1 ) / 2;
            var m3      = ( pivot + r1 ) / 2;
            var m2      = ( pivot + s1 ) / 2;
            var m1      = ( s2 + s1 ) / 2;
            var m0      = ( s2 + s3 ) / 2;

            var newPP   = new PivotPointsInfo( dataTime, PivotTimeSpan, r3, m5, r2, m4, r1, m3, pivot, m2, s1, m1, s2, m0, s3, 0 );

            return newPP;
        }

        //protected void OnCalculate2( bool fullRecalculation, DataBarUpdateType? updateType )
        //{
        //    if ( ( updateType == DataBarUpdateType.Initial ) || ( updateType == DataBarUpdateType.HistoryUpdate ) )
        //    {
        //        if ( _pivotTimeSpan == TimeSpan.FromDays( 30 ) )
        //        {
        //            OnCalculate2( fullRecalculation, updateType );
        //        }
        //        //IList< double > closeArray, highArray, lowArray;
        //        _doneInitialDataLoad = false;

        //        int repoStartingIndex       = 0;
        //        int resultSetLength         = IndicatorResult[ "Pivot" ].Count;
        //        int startIndex              = 0;
        //        int endIndex                = 0;
        //        int outBeginIdx             = 0;
        //        int outNBElement            = 0;
        //        int indexCount              = 0;

        //        startIndex = 0;
        //        endIndex = IndicatorBarsRepo.TotalBarCount - repoStartingIndex - 1;
        //        indexCount = endIndex - startIndex + 1;
        //        outBeginIdx = 0;
        //        outNBElement = 0;

        //        double[ ] closeArray        = IndicatorBarsRepo.CloseArray;
        //        double[ ] highArray         = IndicatorBarsRepo.HighArray;
        //        double[ ] lowArray          = IndicatorBarsRepo.LowArray;
        //        long[ ] dateArray           = IndicatorBarsRepo.TimeArray;

        //        double[ ] pivot             = new double[ indexCount ];
        //        double[ ] r3                = new double[ indexCount ];
        //        double[ ] r2                = new double[ indexCount ];
        //        double[ ] r1                = new double[ indexCount ];
        //        double[ ] s3                = new double[ indexCount ];
        //        double[ ] s2                = new double[ indexCount ];
        //        double[ ] s1                = new double[ indexCount ];
        //        double[ ] marketDirectionNo = new double[ indexCount ];

        //        DateTime pivotTime = DateTime.MinValue;

        //        _lastBarTime = DateTime.MinValue;

        //        /* --------------------------------------------------------------------------------------------------------------
        //         * index =  0        1        2       3       4
        //         * Time  =  4/3      5/3      6/3     7/3     8/3
        //         * Data  =  4/3      5/3      6/3     7/3     8/3
        //         * Pivot =  5/3Piv   6/3Piv   7/3Piv  8/3Piv  11/3Piv  
        //         * Block =  [4-5>/3  [5-6>/3  [6-7>/3 [7-8>/3 [8-11>
        //         * --------------------------------------------------------------------------------------------------------------
        //         */

        //        _cacheTimePeriodToIndex.Clear( );

        //        int index = 0;



        //        for ( int i = startIndex; i <= endIndex; i++ )
        //        {
        //            long pivotDate = dateArray[ i ];
        //            pivotTime = pivotDate.FromLinuxTime( );

        //            if ( _pivotTimeSpan == TimeSpan.FromDays( 1 ) && pivotTime.DayOfWeek == DayOfWeek.Saturday )
        //            {
        //                continue;
        //            }

        //            var myPivot    = Math.Round( ( IndicatorBarsRepo[ i ].High + IndicatorBarsRepo[ i ].Low + IndicatorBarsRepo[ i ].Close ) / 3, 5 );

        //            pivot[ index ] = myPivot;
        //            r1[ index ] = ( 2 * myPivot ) - IndicatorBarsRepo[ i ].Low;
        //            r2[ index ] = myPivot + ( IndicatorBarsRepo[ i ].High - IndicatorBarsRepo[ i ].Low );
        //            r3[ index ] = IndicatorBarsRepo[ i ].High + 2 * ( myPivot - IndicatorBarsRepo[ i ].Low );
        //            s1[ index ] = ( 2 * myPivot ) - IndicatorBarsRepo[ i ].High;
        //            s2[ index ] = myPivot - ( IndicatorBarsRepo[ i ].High - IndicatorBarsRepo[ i ].Low );
        //            s3[ index ] = IndicatorBarsRepo[ i ].Low - 2 * ( IndicatorBarsRepo[ i ].High - myPivot );

        //            UpdateTimePeriodCache( i, pivotTime );

        //            if ( i == endIndex )
        //            {
        //                AddLastTimeBlock( i, pivotTime );
        //            }

        //            index++;

        //            _lastBarTime = pivotTime;
        //        }


        //        IndicatorResult.AddSetValues( "R3", repoStartingIndex + startIndex, endIndex + 1, true, r3 );
        //        IndicatorResult.AddSetValues( "R2", repoStartingIndex + startIndex, endIndex + 1, true, r2 );
        //        IndicatorResult.AddSetValues( "R1", repoStartingIndex + startIndex, endIndex + 1, true, r1 );
        //        IndicatorResult.AddSetValues( "Pivot", repoStartingIndex + startIndex, endIndex + 1, true, pivot );
        //        IndicatorResult.AddSetValues( "S1", repoStartingIndex + startIndex, endIndex + 1, true, s1 );
        //        IndicatorResult.AddSetValues( "S2", repoStartingIndex + startIndex, endIndex + 1, true, s2 );
        //        IndicatorResult.AddSetValues( "S3", repoStartingIndex + startIndex, endIndex + 1, true, s3 );



        //        outBeginIdx = 0;
        //        outNBElement = 0;
        //        fx.TALib.Core.Sma( pivot, startIndex, endIndex, marketDirectionNo, out outBeginIdx, out outNBElement, 3 );
        //        IndicatorResult.AddSetValues( "MarketDirectionNo", repoStartingIndex + outBeginIdx, outNBElement, true, marketDirectionNo );

        //        string msg = IndicatorBarsRepo.Period.Value.ToReadable( ) + " pivot points " + " are [ R3 = " + r3[ indexCount - 1 ] + ", R2 = " + r2[ indexCount - 1 ] + ", R1 = " + r1[ indexCount - 1 ] + ", Pivot = " + pivot[ indexCount - 1 ] + ", S1 = " + s1[ indexCount - 1 ] + ", S2 = " + s2[ indexCount - 1 ] + ", S3 = " + s3[ indexCount - 1 ] + " ] ";

        //        var pivotlevels = new PooledList<SRlevel>( 15 );

        //        if ( Pivot > 0 )
        //        {
        //            pivotlevels.Add( new SRlevel( _lastTimeBlock, PivotTimeSpan, R3, ( int ) ( ( R3 - Pivot ) * 10000 ), SR3rdType.R3 ) );
        //            pivotlevels.Add( new SRlevel( _lastTimeBlock, PivotTimeSpan, M5, ( int ) ( ( M5 - Pivot ) * 10000 ), SR3rdType.M5 ) );
        //            pivotlevels.Add( new SRlevel( _lastTimeBlock, PivotTimeSpan, R2, ( int ) ( ( R2 - Pivot ) * 10000 ), SR3rdType.R2 ) );
        //            pivotlevels.Add( new SRlevel( _lastTimeBlock, PivotTimeSpan, M4, ( int ) ( ( M4 - Pivot ) * 10000 ), SR3rdType.M4 ) );
        //            pivotlevels.Add( new SRlevel( _lastTimeBlock, PivotTimeSpan, R1, ( int ) ( ( R1 - Pivot ) * 10000 ), SR3rdType.R1 ) );
        //            pivotlevels.Add( new SRlevel( _lastTimeBlock, PivotTimeSpan, M3, ( int ) ( ( M3 - Pivot ) * 10000 ), SR3rdType.M3 ) );
        //            pivotlevels.Add( new SRlevel( _lastTimeBlock, PivotTimeSpan, Pivot, ( int ) ( ( M3 - Pivot ) / 2 * 10000 ), SR3rdType.PIVOT ) );
        //            pivotlevels.Add( new SRlevel( _lastTimeBlock, PivotTimeSpan, M2, ( int ) ( ( Pivot - M2 ) * 10000 ), SR3rdType.M2 ) );
        //            pivotlevels.Add( new SRlevel( _lastTimeBlock, PivotTimeSpan, S1, ( int ) ( ( Pivot - S1 ) * 10000 ), SR3rdType.S1 ) );
        //            pivotlevels.Add( new SRlevel( _lastTimeBlock, PivotTimeSpan, M1, ( int ) ( ( Pivot - M1 ) * 10000 ), SR3rdType.M1 ) );
        //            pivotlevels.Add( new SRlevel( _lastTimeBlock, PivotTimeSpan, S2, ( int ) ( ( Pivot - S2 ) * 10000 ), SR3rdType.S2 ) );
        //            pivotlevels.Add( new SRlevel( _lastTimeBlock, PivotTimeSpan, M0, ( int ) ( ( Pivot - M0 ) * 10000 ), SR3rdType.M0 ) );
        //            pivotlevels.Add( new SRlevel( _lastTimeBlock, PivotTimeSpan, S3, ( int ) ( ( Pivot - S3 ) * 10000 ), SR3rdType.S3 ) );
        //            pivotlevels.Add( new SRlevel( _lastTimeBlock, PivotTimeSpan, Mdn, ( int ) ( Math.Abs( Mdn - Pivot ) * 10000 ), SR3rdType.MDN ) );
        //            Messenger.Default.Send( new PivotPointMessage( PivotTimeSpan, _lastBarTime, pivotlevels ) );
        //        }

        //        _doneInitialDataLoad = true;

        //        RaisePivotPointChanged( );
        //    }


        //}

        //void UpdateTimePeriodCache( int i, DateTime pivotTime )
        //{
        //    if ( i > 0 )
        //    {
        //        if ( _cacheTimePeriodToIndex.Count == 0 )
        //        {
        //            _lastTimeBlock = new TimeBlock( _lastBarTime, pivotTime );

        //            _cacheTimePeriodToIndex.Add( _lastTimeBlock );
        //        }
        //        else
        //        {
        //            var lastEntry = _cacheTimePeriodToIndex[ _cacheTimePeriodToIndex.Count - 1 ];

        //            if ( lastEntry.End <= _lastBarTime )
        //            {
        //                _lastTimeBlock = new TimeBlock( _lastBarTime, pivotTime );
        //                _cacheTimePeriodToIndex.Add( _lastTimeBlock );
        //            }
        //            else
        //            {
        //                //throw new InvalidMomentException( lastEntry.End );
        //            }
        //        }
        //    }
        //}

        private TimeBlockEx GetPivotTimeBlock( DateTime databarStartTime )
        {
            TimeBlockEx output = TimeBlockEx.EmptyBlock;

            if ( _pivotTimeSpan == TimeSpan.FromDays( 30 ) )
            {
                /*-------------------------------------------------------------------------
                 * 4/30/2021 Monthly Databar is indeed the databar for the month of
                 *  
                 *  5/1/2021 to 5/31/2021
                 *  
                 *  Which will be used to calc pivot point for 6/1/2021 to the end of June
                 * 
                 * -------------------------------------------------------------------------
                 * */
                var endOfMonth = databarStartTime.AddDays( 1 ).GetLastDayOfMonth( );

                return new TimeBlockEx( databarStartTime, endOfMonth );
            }
            else if ( _pivotTimeSpan == TimeSpan.FromDays( 7 ) )
            {
                return new TimeBlockEx( databarStartTime, _pivotTimeSpan );

            }
            else if ( _pivotTimeSpan == TimeSpan.FromDays( 1 ) )
            {
                return new TimeBlockEx( databarStartTime, _pivotTimeSpan );
            }

            return output;
        }

        //private TimeBlock NewLastTimeBlock( DateTime databarStartTime )
        //{
        //    TimeBlock output = null;

        //    if ( _pivotTimeSpan == TimeSpan.FromDays( 30 ) )
        //    {
        //        var endOfMonth = databarStartTime.AddDays( 1 ).GetLastDayOfMonth( );

        //        output = new TimeBlock( databarStartTime, endOfMonth );                
        //    }
        //    else if ( _pivotTimeSpan == TimeSpan.FromDays( 7 ) )
        //    {
        //        output = new TimeBlock( databarStartTime, _pivotTimeSpan );
        //        
        //    }
        //    else if ( _pivotTimeSpan == TimeSpan.FromDays( 1 ) )
        //    {
        //        /* -------------------------------------------------------------------------------------------------------------------------------------------
        //         * 
        //         *  This bar is the bar that Start from Thursday GMT time
        //         * 
        //         * ------------------------------------------------------------------------------------------------------------------------------------------- */

        //        var dataBarCloseTime = databarStartTime + _pivotTimeSpan;                

        //        if ( dataBarCloseTime.DayOfWeek == DayOfWeek.Friday )
        //        {
        //            var nextMondayTime = dataBarCloseTime.AddDays( 2 );
        //            
        //            output = new TimeBlock( nextMondayTime.AddTicks( 1 ), _pivotTimeSpan );
        //            
        //        }
        //        else
        //        {
        //            output = new TimeBlock( dataBarCloseTime.AddTicks( 1 ), _pivotTimeSpan );                    
        //        }
        //    }

        //    return output;
        //}

        //private void AddLastTimeBlock( int i, DateTime pivotTime )
        //{
        //    if ( _pivotTimeSpan == TimeSpan.FromDays( 30 ) )
        //    {
        //        var endOfMonth = pivotTime.AddDays( 1 ).GetLastDayOfMonth( );

        //        _lastTimeBlock = new TimeBlock( pivotTime, endOfMonth );

        //        _cacheTimePeriodToIndex.Add( _lastTimeBlock );

        //        var currentTime = DateTime.UtcNow;

        //        if ( currentTime > endOfMonth )
        //        {
        //            var endOfNextMonth = endOfMonth.AddDays( 1 ).GetLastDayOfMonth( );

        //            _lastTimeBlock = new TimeBlock( endOfMonth, endOfNextMonth );

        //            _cacheTimePeriodToIndex.Add( _lastTimeBlock );
        //        }
        //    }
        //    else if ( _pivotTimeSpan == TimeSpan.FromDays( 7 ) )
        //    {
        //        _lastTimeBlock = new TimeBlock( pivotTime, _pivotTimeSpan );

        //        _cacheTimePeriodToIndex.Add( _lastTimeBlock );

        //        var currentTime = DateTime.UtcNow;

        //        var latestWeekSpan = pivotTime + _pivotTimeSpan;

        //        if ( currentTime > latestWeekSpan )
        //        {
        //            _lastTimeBlock = new TimeBlock( latestWeekSpan, _pivotTimeSpan );

        //            _cacheTimePeriodToIndex.Add( _lastTimeBlock );
        //        }
        //    }
        //    else if ( _pivotTimeSpan == TimeSpan.FromDays( 1 ) )
        //    {


        //        if ( pivotTime.DayOfWeek == DayOfWeek.Thursday )
        //        {
        //            _lastTimeBlock = new TimeBlock( pivotTime, TimeSpan.FromDays( 3 ) );
        //            _cacheTimePeriodToIndex.Add( _lastTimeBlock );
        //        }
        //        else
        //        {
        //            _lastTimeBlock = new TimeBlock( pivotTime, _pivotTimeSpan );
        //            _cacheTimePeriodToIndex.Add( _lastTimeBlock );
        //        }
        //    }

        //}

        private void RaisePivotPointChanged( )
        {
            TimeBlockEx temp;
            var pp = GetPivotPointsAt( _lastBarTime, out temp );

            if ( pp != null )
            {
                var ppEvt = pp.ToEventArgs( );
                ppEvt.Security = Security.Code;

                var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );
                if ( aa == null )
                    return;

                aa.RaisePivotPointsChanged( _indicatorSecurity, ppEvt );
            }
        }



        public override PlatformIndicator OnSimpleClone( )
        {
            PivotPointsCustom result = new PivotPointsCustom( );
            result._description = _description;
            result._name = _name;

            return result;
        }

        protected override void OnFinalCalculate( bool fullRecalculation, DataBarUpdateType? updateType )
        {

        }

        public PivotPointEnum GetClosestPivotPoints( decimal currentPrice, ref bool aboveOrbelow )
        {
            PivotPointEnum closestPivot = PivotPointEnum.NONE;

            //if ( currentPrice >= Pivot )        // Above Pivot Point
            //{
            //    if ( currentPrice >= R1 )
            //    {
            //        if ( currentPrice >= R2 )
            //        {
            //            if ( currentPrice >= R3 )
            //            {
            //                closestPivot = PivotPointsCustom.PivotPointEnum.R3;
            //                aboveOrbelow = true;
            //            }
            //            else
            //            {
            //                closestPivot = ( ( R3 - currentPrice ) > ( currentPrice - R2  ) ) ? PivotPointsCustom.PivotPointEnum.R2 : PivotPointsCustom.PivotPointEnum.R3;                            
            //                aboveOrbelow = ( closestPivot == PivotPointEnum.R2 );
            //            }
            //        }
            //        else
            //        {
            //            closestPivot     = ( ( R2 - currentPrice ) > ( currentPrice - R1 ) ) ? PivotPointsCustom.PivotPointEnum.R1 : PivotPointsCustom.PivotPointEnum.R2;
            //            aboveOrbelow     = ( closestPivot == PivotPointEnum.R1 );
            //        }
            //    }
            //    else
            //    {
            //        closestPivot         = ( ( R1 - currentPrice ) > ( currentPrice - Pivot ) ) ? PivotPointsCustom.PivotPointEnum.PIVOT : PivotPointsCustom.PivotPointEnum.R1;
            //        aboveOrbelow         = ( closestPivot == PivotPointEnum.PIVOT );
            //    }
            //}
            //else  // Below pivot point
            //{
            //    if ( currentPrice <= S1 )
            //    {
            //        if ( currentPrice <= S2 )
            //        {
            //            if ( currentPrice <= S3 )
            //            {
            //                closestPivot = PivotPointsCustom.PivotPointEnum.S3;
            //                aboveOrbelow = false;                            
            //            }
            //            else
            //            {
            //                closestPivot = (  ( S2 - currentPrice ) > ( currentPrice - S3 ) ) ? PivotPointsCustom.PivotPointEnum.S3 : PivotPointsCustom.PivotPointEnum.S2;
            //                aboveOrbelow = ( closestPivot == PivotPointEnum.S3 );
            //            }
            //        }
            //        else
            //        {
            //            closestPivot     = ( ( S1 - currentPrice ) > ( currentPrice - S2 ) ) ? PivotPointsCustom.PivotPointEnum.S2 : PivotPointsCustom.PivotPointEnum.S1;
            //            aboveOrbelow     = ( closestPivot == PivotPointEnum.S2 );
            //        }
            //    }
            //    else
            //    {
            //        closestPivot         = ( ( Pivot - currentPrice ) > ( currentPrice - S1 ) ) ? PivotPointsCustom.PivotPointEnum.S1 : PivotPointsCustom.PivotPointEnum.PIVOT;
            //        aboveOrbelow         = ( closestPivot == PivotPointEnum.S1 );
            //    }
            //}

            return ( closestPivot );
        }

        //public int GetLatestPivotIndex( )
        //{
        //    var currentTime = DateTime.UtcNow;

        //    return ( ( int ) GetIndexByTime( currentTime ) );
        //}

        //public DateTime GetPivotBartime( )
        //{
        //    var period = IndicatorBarsRepo.Period.Value;

        //    var currentTime = DateTime.UtcNow;

        //    int index = IndicatorBarsRepo.TotalBarCount - 1;

        //    var bar = IndicatorBarsRepo[ index ];

        //    return bar.BarTime;
        //}

        //public int GetPreviousTimeBlockAndIndex( int oldIndex, out TimeBlock previousTimeBlock )
        //{
        //    previousTimeBlock = null;
        //    
        //    int index = oldIndex - 1;

        //    if ( index >= 0 )
        //    {
        //        previousTimeBlock = ( TimeBlock ) _cacheTimePeriodToIndex [ index ];
        //    }

        //    return index;
        //}

        //public int GetPreviousTimeBlockAndIndex( TimeBlock lastTimeBlock, out TimeBlock previousTimeBlock )
        //{
        //    previousTimeBlock = null;

        //    if ( lastTimeBlock == null ) return -1;

        //    var oldIndex = _cacheTimePeriodToIndex.IndexOf( lastTimeBlock );
        //    int index = oldIndex - 1;

        //    if ( index >= 0 )
        //    {
        //        previousTimeBlock = ( TimeBlock ) _cacheTimePeriodToIndex[ index ];
        //    }

        //    return ( index - 1 );
        //}



        //public long GetIndexByTime( DateTime theDate )
        //{
        //    _pivotPointsForDates.LastKey;

        //    var interests = _cacheTimePeriodToIndex.IntersectionPeriods( theDate );

        //    foreach ( ITimePeriod interest in interests )
        //    {
        //        var intIndex = _cacheTimePeriodToIndex.IndexOf( interest );

        //        return ( intIndex - 1 );
        //    }

        //    return -1;
        //}
    }
}