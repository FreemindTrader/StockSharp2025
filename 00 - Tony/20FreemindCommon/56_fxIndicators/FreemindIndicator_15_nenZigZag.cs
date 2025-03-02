//using System;
//using fx.Collections;
//using System.Diagnostics;
//using System.Drawing;
//using System.Globalization;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Threading;
//using System.Threading.Tasks;

//using fx.Common;
//
//using fx.Algorithm;
//using fx.TALib;
//using fx.Definitions;
//using System.Data;
//using fx.Database;
//using FreemindAlgo.Collections;

//#pragma warning disable 414

//namespace fx.Indicators
//{
//    public partial class FreemindIndicator : CustomPlatformIndicator
//    {
//        protected Task BasicnenZigZagTask( bool fullRecalculation, DataBarUpdateType? updateType, int extDepth, int extBackStep, NeoSwingVariables neoV, ref PooledList<Task> tasksList )
//        {
//            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
//            {
//                return null;
//            }

//            //ThreadHelper.UpdateThreadName( "BasicWaveZigZagTask" );            

//            Task first = new Task(() => nenZigZag( fullRecalculation, updateType, extDepth, extBackStep, neoV        ), IndicatorExitToken);

//            tasksList.Add( first );

//            return first;
//        }

//        public void nenZigZag( bool fullRecalculation, DataBarUpdateType? updateType, int zigZagMinSeperation, int extremumMinSeperation, NeoSwingVariables neoV )
//        {
//            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
//            {
//                return;
//            }

//            long timeFirstBar=0;

//            var barB4Calculation  = _barCountBeforeCalculation;

//            if ( barB4Calculation - 1 < zigZagMinSeperation ) return;
//            
//            if ( ( updateType == DataBarUpdateType.Initial ) || ( updateType == DataBarUpdateType.HistoryUpdate ) || ( updateType == DataBarUpdateType.Reloaded ) || updateType == DataBarUpdateType.NewPeriod )
//            {
//                int MaxBar,limit,supr2_bar,supr3_bar,supr4_bar;

//                if ( _lastNenZZCountedBars < 0 ) return;
//                //---- the last calculated bar must be recalculated
//                if ( _lastNenZZCountedBars > 0 ) _lastNenZZCountedBars--;

//                MaxBar = barB4Calculation-zigZagMinSeperation;

//                if ( _lastNenZZCountedBars == 0 || barB4Calculation - _lastNenZZCountedBars > 2 )
//                {
//                    limit = MaxBar;
//                }
//                else
//                {                    
//                    supr2_bar = IndicatorBarsRepo.GetIndexByTime( neoV.Time2 );
//                    supr3_bar = IndicatorBarsRepo.GetIndexByTime( neoV.Time3 );
//                    supr4_bar = IndicatorBarsRepo.GetIndexByTime( neoV.Time4 );
//                    
//                    limit = supr3_bar;
//                    if ( ( supr2_bar < 0 ) || ( supr3_bar < 0 ) || ( supr4_bar < 0 ) )
//                    {
//                        limit = MaxBar;
//                    }
//                }

//                //---- zero initialization
//                if ( limit >= MaxBar || timeFirstBar != IndicatorBarsRepo.TimeArray[ barB4Calculation - 1 ] )
//                {
//                    timeFirstBar = IndicatorBarsRepo.TimeArray[ barB4Calculation - 1 ];
//                    limit = MaxBar;
//                }

//                //----  

//                //---- resizing temporary buffer

//                int bufferSize = 0;
//                
//                if ( limit == MaxBar )
//                {
//                    bufferSize = barB4Calculation;
//                }
//                else
//                {
//                    bufferSize = limit + extremumMinSeperation + 1;
//                }

//                var TempBuffer     = new double[ bufferSize ];
//                



//                TASignal old;
//                _last3rdRedTime = -1;
//                var nenZigZag = new ThreadSafeDictionary<long, TASignal>( );

//                int startIndex      = 0;
//                //int startClearing = 0;
//                string zigZagBoth   = "ZigZagNen" + zigZagMinSeperation.ToString( "D2" );
//                string zigZagHigh   = zigZagBoth + "High";
//                string zigZagLow    = zigZagBoth + "Low";
//                int resultSetLength = IndicatorResult[ zigZagBoth ].Count;
//                var period          = IndicatorBarsRepo.Period.Value;
//                int endIndex        = 0;
//                int indexCount      = 0;
//                int i               = 0;
//                int back            = 0;
//                int lastHighIndex   = 0;
//                int lastLowIndex    = 0;
//                int lowestIndex     = -1;
//                int highestIndex    = -1;
//                double newExtreme   = 0.0;
//                double curLow       = 0.0;
//                double curHigh      = 0.0;
//                double lastHigh     = 0.0;
//                double lastLow      = 0.0;

//                


//                if ( fullRecalculation )
//                {
//                    startIndex = 0;

//                    ResetNenVariablesSnapshot();
//                }
//                else
//                {
//                    if ( zigZagMinSeperation == 5 )
//                    {
//                        /* 
//                           ------------------------------------------------------------------------------------------------------------------------------------
//                           --
//                           -- We don't need all extDepth thread to do calculation of Second last and the last Red dot, So I select the most PTs extDepth = 5.
//                           -- To reduce all the non-neccessary calculation, I start calculating PTs from the last 2nd red dots.
//                           --
//                           ------------------------------------------------------------------------------------------------------------------------------------
//                       */
//                        var last3rdTime = _hews.GetLinuxTimeOf3rdLastRedDot( period );

//                        if ( last3rdTime > 0 )
//                        {
//                            startIndex = IndicatorBarsRepo.GetIndexByTime( last3rdTime );

//                            if ( startIndex < 0 && resultSetLength > 0 )
//                            {
//                                startIndex = _last3rdRedIndex > 0 ? _last3rdRedIndex : 0;
//                            }

//                            _last3rdRedTime = last3rdTime;
//                            _last3rdRedIndex = startIndex;
//                            startIndex = Math.Max( 0, startIndex - zigZagMinSeperation );
//                        }
//                    }
//                }

//                if ( startIndex <= 0 && resultSetLength > 0 )
//                {
//                    startIndex = _last3rdRedIndex > 0 ? _last3rdRedIndex : 0;

//                    startIndex = Math.Max( 0, startIndex - zigZagMinSeperation );
//                }

//                endIndex = barB4Calculation;
//                indexCount = endIndex - startIndex;

//                if ( indexCount <= 0 ) return;


//                var highBuffer     = new double[ indexCount  ];
//                var low_Buffer     = new double[ indexCount  ];



//                /* 
//                    ------------------------------------------------------------------------------------------------------------------------------------
//                    --
//                    --               the beginning of the first great cycle
//                    --                
//                    -- This cycle is very simple, it loop thru all the bars and find the highest high within the total number of extBackStep bar
//                    -- and the lowest low within a group of extBackStep bars
//                    ------------------------------------------------------------------------------------------------------------------------------------
//                */
//                for ( i = startIndex + zigZagMinSeperation; i < endIndex; i++ )
//                {

//                    /* -------------------------------------------------------------------------------------------------------------------------------------------
//                     * 
//                     *  currentBufferIndex is the mapping between value of i which can start at 1000, but our buffer will always be zero-index based.
//                     * 
//                     * ------------------------------------------------------------------------------------------------------------------------------------------- */

//                    var currentBufferIndex = i - startIndex;

//                    int searchBegin = i - zigZagMinSeperation + 1;
//                    if ( lowestIndex < 0 )
//                    {
//                        /* -------------------------------------------------------------------------------------------------------------------------------------------
//                         * 
//                         *  For the first search, we will search the lowest from searchBegin to i
//                         * 
//                         * ------------------------------------------------------------------------------------------------------------------------------------------- */

//                        lowestIndex = IndicatorBarsRepo.iLowest( searchBegin, i );
//                    }
//                    else
//                    {
//                        /// <summary>
//                        /// This is the scenario
//                        /// We want to find the lowest within a Range and advance one Bar by a time.
//                        /// 
//                        /// B1 B2 B3 B4 B5 B6 B7 B8 B9 B10 B11 B12 B13
//                        /// (         L                  )
//                        ///    (      L                     N )   - In this case, we only need to check the last N to see if it is lower than L
//                        ///        (  L                         N )
//                        ///           (                              ) - In this case, L is no longer in the range, we will have to loop thru all the data to find the lowest.
//                        /// </summary>
//                        lowestIndex = IndicatorBarsRepo.iLowest1MoreBar( searchBegin, i, lowestIndex );
//                    }


//                    /* -------------------------------------------------------------------------------------------------------------------------------------------
//                     * StartIndex = S           ExtDepth = E = 5, L = lowest during the search
//                     *  Iterartion  1 2 3 4 5 6 7 8 9 10 11 12 13 14 15
//                     *              S     L   E
//                     *                S   L     E
//                     *                  S         E/NL
//                     *  The following code will zero out 4 when we have find a new low at 8
//                     *              
//                     * ------------------------------------------------------------------------------------------------------------------------------------------- */

//                    if ( lowestIndex == i )         // Here we found a lowest point for the local range at current iteration i
//                    {
//                        newExtreme = IndicatorBarsRepo.LowArray[ lowestIndex ];

//                        if ( newExtreme == lastLow )       // If our low at point 8 = low at point 4, we will zero out the 
//                        {
//                            newExtreme = 0.0;
//                        }
//                        else
//                        {
//                            /* -------------------------------------------------------------------------------------------------------------------------------------------
//                             * StartIndex = S           ExtDepth = E = 5, L = lowest during the search
//                             *  Iterartion  1 2 3 4 5 6 7 8 9 10 11 12 13 14 15
//                             *              S     L   E
//                             *                S   L     E
//                             *                  S         E/NL
//                             *  The following code will zero out 4 when we have find a new low at 8
//                             *              
//                             * ------------------------------------------------------------------------------------------------------------------------------------------- */
//                            lastLow = newExtreme;

//                            var begin = Math.Max( currentBufferIndex - extremumMinSeperation, 0 );
//                            var end   = Math.Max( currentBufferIndex - 1, 0 );

//                            for ( back = begin; back <= end; back++ )
//                            {
//                                var checkedValue = low_Buffer[ back ];

//                                if ( checkedValue != 0 )
//                                {
//                                    if ( newExtreme < checkedValue )
//                                    {
//                                        low_Buffer[ back ] = 0.0;
//                                    }
//                                }
//                            }
//                        }


//                        /* -------------------------------------------------------------------------------------------------------------------------------------------
//                         * Since we begin from StartIndex ( in our case, let's say 1000 ), but our low_BufCpy and low_Buffer start at 0
//                         * 
//                         * StartIndex = S = 1000          ExtDepth = E = 5, L = lowest during the search
//                         *  Iterartion  1001 1002 1003 1004 1005 1006 1007 1008 1009 10010 1011 1012 1013 1014 1015
//                         *               S               L        E
//                         * low_BufCpy     0     0    0   1.034     0    
//                         * 
//                         *                     S         L             E
//                         * low_BufCpy     0     0    0   1.034     0    0                         
//                         * 
//                         *                          S    L                  E/NL
//                         * low_BufCpy     0     0    0   1.034     0    0    1.012
//                         * low_BufCpy     0     0    0    0        0    0    1.012
//                         * 
//                         *  The following code will zero out 1001, 1002, 1004, 1005, 1006, 1007 
//                         *  and set the new Low at 1008
//                         *  
//                         *  i = 1008 and currentBufferIndex = 8
//                         *  Iterartion  1001 1002 1003  1004   1005 1006 1007 1008 1009 10010 1011 1012 1013 1014 1015
//                         
//                         * 
//                         * ------------------------------------------------------------------------------------------------------------------------------------------- */
//                        low_Buffer[ currentBufferIndex ] = newExtreme;
//                    }

//                    searchBegin = i - zigZagMinSeperation + 1;

//                    if ( highestIndex > -1 )
//                    {
//                        // Find the highest bar starting from index and search back to extDepth bars
//                        highestIndex = IndicatorBarsRepo.iHighest( searchBegin, i );
//                    }
//                    else
//                    {
//                        highestIndex = IndicatorBarsRepo.iHighest1MoreBar( searchBegin, i, lowestIndex );
//                    }

//                    /* -------------------------------------------------------------------------------------------------------------------------------------------
//                     * 
//                     *  The reason we need the following is to make sure that when we make higher high, we will clear out the previous high's data.
//                     * 
//                     * ------------------------------------------------------------------------------------------------------------------------------------------- */

//                    if ( highestIndex == i )
//                    {
//                        newExtreme = IndicatorBarsRepo.HighArray[ highestIndex ];

//                        if ( newExtreme == lastHigh )
//                        {
//                            newExtreme = 0.0;
//                        }
//                        else
//                        {
//                            lastHigh = newExtreme;
//                            var begin = Math.Max( currentBufferIndex - extremumMinSeperation, 0 );
//                            var end   = Math.Max( currentBufferIndex - 1, 0 );

//                            //Step back extBackStep bars to see if we are the highest, zero out all those that are lowest than this highest point
//                            for ( back = begin; back <= end; back++ )
//                            {
//                                var checkedValue = highBuffer[ back ];

//                                if ( checkedValue != 0 )
//                                {
//                                    if ( newExtreme > checkedValue )
//                                    {
//                                        highBuffer[ back ] = 0.0;
//                                    }
//                                }
//                            }
//                        }

//                        highBuffer[ currentBufferIndex ] = newExtreme;
//                    }
//                }
//                //++ the beginning of the first great cycle

//                // final cutting 
//                lastHigh = -1;
//                lastHighIndex = -1;
//                lastLow = -1;
//                lastLowIndex = -1;

//                /* 
//                    ------------------------------------------------------------------------------------------------------------------------------------
//                    --
//                    --               the beginning of the Second great cycle
//                    --
//                    -- The following code is used to find Consecutive Lows and removed the one that's not the lowest
//                    --
//                    ------------------------------------------------------------------------------------------------------------------------------------
//                */
//                for ( i = startIndex + zigZagMinSeperation; i < endIndex; i++ )
//                {
//                    var currentBufferIndex  = i - startIndex;
//                    var lastHighBufferIndex = lastHighIndex - startIndex;
//                    var lastLowBufferIndex  = lastLowIndex - startIndex;

//                    /* -------------------------------------------------------------------------------------------------------------------------------------------
//                     * 
//                     *  In the previous Cycle, we use low_BufCpy to store the same content as the low_Buffer and
//                     *                         we use highBufCpy to store the same contect as the HighBuffer
//                     * ------------------------------------------------------------------------------------------------------------------------------------------- */

//                    curLow = low_Buffer[ currentBufferIndex ];
//                    curHigh = highBuffer[ currentBufferIndex ];

//                    if ( ( curLow == 0 ) && ( curHigh == 0 ) ) continue;

//                    /* -------------------------------------------------------------------------------------------------------------------------------------------
//                     * 
//                     *  If we found a high first, we record the high value and the high index and 
//                     * 
//                     * ------------------------------------------------------------------------------------------------------------------------------------------- */

//                    if ( curHigh != 0 )
//                    {
//                        if ( lastHigh > 0 )
//                        {
//                            if ( lastHigh < curHigh )
//                            {
//                                // found a new high, set the last high extremum to zero                                
//                                highBuffer[ lastHighBufferIndex ] = 0;
//                            }
//                            else
//                            {
//                                // previous high is higher than current high, remove current high                                 
//                                highBuffer[ currentBufferIndex ] = 0;
//                            }
//                        }

//                        // Found first high extremum or a new high, store the index and the value to lastLow
//                        if ( lastHigh < curHigh || lastHigh < 0 )
//                        {
//                            lastHigh = curHigh;
//                            lastHighIndex = i;
//                        }

//                        lastLow = -1;
//                    }

//                    if ( curLow != 0 )
//                    {
//                        if ( lastLow > 0 )
//                        {
//                            if ( lastLow > curLow )
//                            {
//                                // found a new low, set the last low extremum to zero                                
//                                low_Buffer[ lastLowBufferIndex ] = 0;
//                            }
//                            else
//                            {
//                                // previous low is LOWER than current low, remove current low extremum                                
//                                low_Buffer[ currentBufferIndex ] = 0;
//                            }
//                        }

//                        // Found the first low extremum or a new low, store the index and the value to lastLow
//                        if ( ( curLow < lastLow ) || ( lastLow < 0 ) )
//                        {
//                            lastLow = curLow;
//                            lastLowIndex = i;
//                        }

//                        lastHigh = -1;
//                    }
//                }


//                lastHigh = -1;
//                lastHighIndex = -1;
//                lastLow = -1;

//                for ( i = startIndex; i < endIndex; i++ )
//                {
//                    var currentBufferIndex  = i - startIndex;
//                    var lastLowBufferIndex  = lastLowIndex - startIndex;
//                    var lastHighBufferIndex = lastHighIndex - startIndex;

//                    if ( IndicatorBarsRepo.LowArray[ i ] == low_Buffer[ currentBufferIndex ] )
//                    {
//                        lastHigh = -1;
//                        lastLowIndex = i;
//                        lastLow = low_Buffer[ currentBufferIndex ];
//                    }

//                    if ( IndicatorBarsRepo.HighArray[ i ] == highBuffer[ currentBufferIndex ] )
//                    {
//                        lastLow = -1;
//                        lastHighIndex = i;
//                        lastHigh = highBuffer[ currentBufferIndex ];
//                    }

//                    if ( lastLow > 0 )
//                    {
//                        //![](79326B23A6449265826A94FA6F45D4A2.png;;;0.02066,0.01624)
//                        // So this block of code will find the lowest between two Peaks.
//                        if ( lastLow > IndicatorBarsRepo.LowArray[ i ] )
//                        {
//                            // Here we find a lower low
//                            lastLow = IndicatorBarsRepo.LowArray[ i ];
//                            low_Buffer[ currentBufferIndex ] = IndicatorBarsRepo.LowArray[ i ];
//                            low_Buffer[ lastLowBufferIndex ] = 0;

//                            lastLowIndex = i;
//                        }
//                    }

//                    if ( lastHigh > 0 )
//                    {
//                        if ( lastHigh < IndicatorBarsRepo.HighArray[ i ] )
//                        {
//                            // Here we find a Higher high
//                            lastHigh = IndicatorBarsRepo.HighArray[ i ];
//                            highBuffer[ currentBufferIndex ] = IndicatorBarsRepo.HighArray[ i ];
//                            highBuffer[ lastHighBufferIndex ] = 0;

//                            lastHighIndex = i;
//                        }
//                    }
//                }


//                for ( int j = 0; j < indexCount; j++ )
//                {
//                    int updateIndex = startIndex + j;

//                    // This mean it is an outside bar, it consists of both Wave Trough and Wave Bottom
//                    if ( highBuffer[ j ] > 0 && low_Buffer[ j ] > 0 )
//                    {
//                        nenZigZag.TryGetValueAndAddOrReplace( IndicatorBarsRepo.TimeArray[ updateIndex ], TASignal.WAVE_PEAK | TASignal.WAVE_TROUGH, out old );
//                    }
//                    else if ( highBuffer[ j ] > 0 )
//                    {
//                        nenZigZag.TryGetValueAndAddOrReplace( IndicatorBarsRepo.TimeArray[ updateIndex ], TASignal.WAVE_PEAK, out old );
//                    }
//                    else if ( low_Buffer[ j ] > 0 )
//                    {
//                        nenZigZag.TryGetValueAndAddOrReplace( IndicatorBarsRepo.TimeArray[ updateIndex ], TASignal.WAVE_TROUGH, out old );
//                    }
//                    else
//                    {
//                        TASignal result = TASignal.NONE;

//                        nenZigZag.TryGetValueAndRemove( IndicatorBarsRepo.TimeArray[ updateIndex ], out result );
//                    }
//                }

//                if ( _hews != null && IndicatorBarsRepo.Period.HasValue )
//                {
//                    _hews.BuildWavesImportance( IndicatorBarsRepo, IndicatorBarsRepo.Period.Value, zigZagMinSeperation, nenZigZag );
//                }

//                var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

//                if ( aa == null )
//                {
//                    return;
//                }

//                var taManager = aa.GetPeriodXTa( period );



//                if ( zigZagMinSeperation == 5 )
//                {
//                    taManager.AddZigZagInfo( _last3rdRedTime, nenZigZag );

//                    if ( _lastRedIndex <= 0 )
//                    {

//                    }
//                    else
//                    {
//                        IndicatorBarsRepo.RemoveSignalsStartingFromIndex( _lastRedIndex + 1, TASignal.WAVE_PEAK, TASignal.WAVE_TROUGH );
//                    }

//                    IndicatorBarsRepo.AddSignalsToDataBar( nenZigZag );
//                }

//                if ( zigZagMinSeperation == 144 )
//                {
//                    _needToRebuildWaveImportance = false;
//                    var lastRedTime = _hews.GetLinuxTimeOfLastRedDot( period );
//                    var lastRedIndex = IndicatorBarsRepo.GetIndexByTime( lastRedTime );

//                    if ( _lastRedTime < 0 )
//                    {
//                        _lastRedTime = lastRedTime;
//                        _lastRedIndex = lastRedIndex;
//                    }
//                    else if ( lastRedTime > _lastRedTime )
//                    {
//                        /* 
//                           ------------------------------------------------------------------------------------------------------------------------------------
//                           --
//                           -- Whenever there is a break of low or high, internally there are a lot of PTs that need to be update. so I reset all those existing
//                           -- PTs back to Wave importance of 5.
//                           --
//                           ------------------------------------------------------------------------------------------------------------------------------------
//                           */
//                        var last3rdTime = _hews.GetLinuxTimeOf3rdLastRedDot( period );

//                        if ( last3rdTime > 0 )
//                        {
//                            _hews.ResetWavesImportance( IndicatorBarsRepo, IndicatorBarsRepo.Period.Value, last3rdTime );

//                            _needToRebuildWaveImportance = true;
//                        }

//                        _lastRedTime = lastRedTime;
//                        _lastRedIndex = lastRedIndex;
//                    }
//                }

//                if ( _needToRebuildWaveImportance )
//                {
//                    if ( _hews != null && IndicatorBarsRepo.Period.HasValue )
//                    {
//                        _hews.BuildWavesImportance( IndicatorBarsRepo, IndicatorBarsRepo.Period.Value, zigZagMinSeperation, nenZigZag );
//                    }
//                }

//                IndicatorResult.AddSetValues( zigZagHigh, startIndex, indexCount, true, highBuffer );
//                IndicatorResult.AddSetValues( zigZagLow, startIndex, indexCount, true, low_Buffer );
//            }
//        }

//    }
//}
