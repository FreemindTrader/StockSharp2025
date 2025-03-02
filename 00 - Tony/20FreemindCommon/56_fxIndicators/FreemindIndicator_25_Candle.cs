using System;
using fx.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

using fx.Common;

using fx.Algorithm;
using fx.TALib;
using fx.Definitions;

#pragma warning disable 414

namespace fx.Indicators
{
    public partial class FreemindIndicator : CustomPlatformIndicator
    {
        protected Task TaskEngulfing( bool fullRecalculation, DataBarUpdateType? updateType, ref PooledList<Task> tasksList, int barB4Calculation )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }


            Task first = new Task(
                                            () =>
                                            {
                                                //ThreadHelper.UpdateThreadName( "TaskEngulfing" );

                                                TACandle[ ] CandleStickPattern;

                                                int repoStartingIndex = 0;

                                                if (fullRecalculation == false)
                                                {
                                                    // I only need to trace back like 15 candles as the most candles that I will need to compare is 13
                                                    repoStartingIndex = Math.Max(0, _lastCandleCheckIndex - EngulfingLookBack() - 1);
                                                }

                                                int startIndex     = repoStartingIndex;
                                                int endIndex       = barB4Calculation - 2;
                                                int indexCount     = endIndex - startIndex + 1;
                                                int outBeginIdx    = 0;
                                                int outNBElement   = 0;
                                                CandleStickPattern = new TACandle[indexCount];
                                                CandleStrength strength = CandleStrength.UNKNOWN;

                                                Engulfing2WithBollinger(barB4Calculation, startIndex, endIndex, out outBeginIdx, out outNBElement, ref CandleStickPattern, out strength );
                                                Bars.ApplyPatternsToBars(outBeginIdx, outNBElement, CandleStickPattern);

                                            }, IndicatorExitToken
                                        );
            tasksList.Add( first );

            return first;
        }

        protected Task TaskPiercingOrDarkCloudCover( bool fullRecalculation, DataBarUpdateType? updateType, ref PooledList<Task> tasksList, int barB4Calculation )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            Task first = new Task(
                                            () =>
                                            {
                                                //ThreadHelper.UpdateThreadName( "TaskPiercingOrDarkCloudCover" );

                                                TACandle[ ] CandleStickPattern;

                                                int repoStartingIndex = 0;

                                                if (fullRecalculation == false)
                                                {
                                                    // I only need to trace back like 15 candles as the most candles that I will need to compare is 13
                                                    repoStartingIndex = Math.Max(0, _lastCandleCheckIndex - PiercingOrDarkCloudCoverLookBack() - 1);
                                                }

                                                int startIndex     = repoStartingIndex;
                                                int endIndex       = barB4Calculation - 2;
                                                int indexCount     = endIndex - startIndex + 1;
                                                int outBeginIdx    = 0;
                                                int outNBElement   = 0;
                                                CandleStickPattern = new TACandle[indexCount];

                                                PiercingOrDarkCloudCover2(startIndex, endIndex, out outBeginIdx, out outNBElement, ref CandleStickPattern);
                                                Bars.ApplyPatternsToBars(outBeginIdx, outNBElement, CandleStickPattern);

                                            }, IndicatorExitToken
                                        );

            tasksList.Add( first );

            return first;
        }


        protected Task TaskMorningStarOrDoji( bool fullRecalculation, DataBarUpdateType? updateType, ref PooledList<Task> tasksList, int barB4Calculation )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            Task first = new Task(
                                            () =>
                                            {
                                                //ThreadHelper.UpdateThreadName( "TaskMorningStarOrDoji" );

                                                TACandle[ ] CandleStickPattern;

                                                int repoStartingIndex = 0;

                                                if (fullRecalculation == false)
                                                {
                                                    // I only need to trace back like 15 candles as the most candles that I will need to compare is 13
                                                    repoStartingIndex = Math.Max(0, _lastCandleCheckIndex - MorningStarOrDojiLookback() - 1);
                                                }

                                                int startIndex = repoStartingIndex;
                                                int endIndex   = barB4Calculation - 2;
                                                int indexCount = endIndex - startIndex + 1;

                                                if (indexCount < 0)
                                                    return;

                                                int outBeginIdx = 0;
                                                int outNBElement = 0;
                                                CandleStickPattern = new TACandle[indexCount];
                                                CandleStrength strength = CandleStrength.UNKNOWN;

                                                MorningStarOrDoji34(barB4Calculation, startIndex, endIndex, out outBeginIdx, out outNBElement, ref CandleStickPattern, out strength );
                                                Bars.ApplyPatternsToBars(outBeginIdx, outNBElement, CandleStickPattern);

                                            }, IndicatorExitToken
                                        );

            tasksList.Add( first );

            return first;
        }

        protected Task TaskEveningStarOrDoji( bool fullRecalculation, DataBarUpdateType? updateType, ref PooledList<Task> tasksList, int barB4Calculation )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }


            Task first = new Task(
                                            () =>
                                            {
                                                //ThreadHelper.UpdateThreadName( "TaskEveningStarOrDoji" );

                                                TACandle[ ] CandleStickPattern;

                                                int repoStartingIndex = 0;

                                                if (fullRecalculation == false)
                                                {
                                                    // I only need to trace back like 15 candles as the most candles that I will need to compare is 13
                                                    repoStartingIndex = Math.Max(0, _lastCandleCheckIndex - EveningStarOrDojiLookback() - 1);
                                                }

                                                int startIndex     = repoStartingIndex;
                                                int endIndex       = barB4Calculation - 2;
                                                int indexCount     = endIndex - startIndex + 1;
                                                int outBeginIdx    = 0;
                                                int outNBElement   = 0;
                                                CandleStickPattern = new TACandle[indexCount];
                                                CandleStrength strength = CandleStrength.UNKNOWN;

                                                EveningStarOrDoji34(barB4Calculation, startIndex, endIndex, out outBeginIdx, out outNBElement, ref CandleStickPattern, out strength);
                                                Bars.ApplyPatternsToBars(outBeginIdx, outNBElement, CandleStickPattern);

                                            }, IndicatorExitToken
                                        );

            tasksList.Add( first );

            return first;
        }

        protected Task TaskAdvanceBlock( bool fullRecalculation, DataBarUpdateType? updateType )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            Task detectTask  = new Task(
                                            () =>
                                            {
                                                //ThreadHelper.UpdateThreadName( "TaskAdvanceBlock" );

                                                TACandle[ ] CandleStickPattern;

                                                int repoStartingIndex = 0;

                                                if ( fullRecalculation == false )
                                                {
                                                    // I only need to trace back like 15 candles as the most candles that I will need to compare is 13
                                                    repoStartingIndex = Math.Max( 0, _lastCandleCheckIndex - AdvanceBlockLookBack() - 1 );
                                                }

                                                int startIndex     = repoStartingIndex;
                                                int endIndex       = _barCountBeforeCalculation - 2;
                                                int indexCount     = endIndex - startIndex + 1;
                                                int outBeginIdx    = 0;
                                                int outNBElement  = 0;
                                                CandleStickPattern = new TACandle[ indexCount ];

                                                AdvanceBlock3( startIndex, endIndex, out outBeginIdx, out outNBElement, ref CandleStickPattern );
                                                Bars.ApplyPatternsToBars( outBeginIdx, outNBElement, CandleStickPattern );

                                            }, IndicatorExitToken
                                        );

            detectTask.Start();

            return detectTask;
        }




        protected Task TaskDojiOrHammer( bool fullRecalculation, DataBarUpdateType? updateType )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            Task detectTask  = new Task(
                                            () =>
                                            {
                                                //ThreadHelper.UpdateThreadName( "TaskDojiOrHammer" );

                                                TACandle[ ] CandleStickPattern;

                                                int repoStartingIndex = 0;

                                                if ( fullRecalculation == false )
                                                {
                                                    // I only need to trace back like 15 candles as the most candles that I will need to compare is 13
                                                    repoStartingIndex = Math.Max( 0, _lastCandleCheckIndex - DojiOrHammerLookBack() - 1 );
                                                }

                                                int startIndex     = repoStartingIndex;
                                                int endIndex       = _barCountBeforeCalculation - 2;
                                                int indexCount     = endIndex - startIndex + 1;
                                                int outBeginIdx    = 0;
                                                int outNBElement  = 0;
                                                CandleStickPattern = new TACandle[ indexCount ];

                                                DojiOrHammer1WithBollinger( startIndex, endIndex, out outBeginIdx, out outNBElement, ref CandleStickPattern );
                                                Bars.ApplyPatternsToBars( outBeginIdx, outNBElement, CandleStickPattern );

                                            }, IndicatorExitToken
                                        );

            detectTask.Start();

            return detectTask;
        }

        protected Task TaskThreeStarsInSouth( bool fullRecalculation, DataBarUpdateType? updateType )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            Task detectTask  = new Task(
                                            () =>
                                            {
                                                //ThreadHelper.UpdateThreadName( "TaskThreeStarsInSouth" );

                                                TACandle[ ] CandleStickPattern;

                                                int repoStartingIndex = 0;

                                                if ( fullRecalculation == false )
                                                {
                                                    // I only need to trace back like 15 candles as the most candles that I will need to compare is 13
                                                    repoStartingIndex = Math.Max( 0, _lastCandleCheckIndex - ThreeStarsInSouthLookBack() - 1 );
                                                }

                                                int startIndex     = repoStartingIndex;
                                                int endIndex       = _barCountBeforeCalculation - 2;
                                                int indexCount     = endIndex - startIndex + 1;
                                                int outBeginIdx    = 0;
                                                int outNBElement  = 0;
                                                CandleStickPattern = new TACandle[ indexCount ];

                                                ThreeStarsInSouth3( startIndex, endIndex, out outBeginIdx, out outNBElement, ref CandleStickPattern );
                                                Bars.ApplyPatternsToBars( outBeginIdx, outNBElement, CandleStickPattern );

                                            }, IndicatorExitToken
                                        );

            detectTask.Start();

            return detectTask;
        }


        protected Task TaskCheckElliottWaveValidity( bool fullRecalculation, DataBarUpdateType? updateType )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            Task detectTask  = new Task(
                                            () =>
                                            {
                                                CheckElliottWaveValidity();

                                            }, IndicatorExitToken
                                        );

            detectTask.Start();

            return detectTask;
        }



        protected Task TaskThreeLineStrike( bool fullRecalculation, DataBarUpdateType? updateType )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            Task detectTask  = new Task(
                                            () =>
                                            {
                                                //ThreadHelper.UpdateThreadName( "TaskThreeLineStrike" );

                                                TACandle[ ] CandleStickPattern;

                                                int repoStartingIndex = 0;

                                                if ( fullRecalculation == false )
                                                {
                                                    // I only need to trace back like 15 candles as the most candles that I will need to compare is 13
                                                    repoStartingIndex = Math.Max( 0, _lastCandleCheckIndex - ThreeLineStrikeLookBack() - 1 );
                                                }

                                                int startIndex     = repoStartingIndex;
                                                int endIndex       = _barCountBeforeCalculation - 2;
                                                int indexCount     = endIndex - startIndex + 1;
                                                int outBeginIdx    = 0;
                                                int outNBElement  = 0;
                                                CandleStickPattern = new TACandle[ indexCount ];

                                                ThreeLineStrike4( startIndex, endIndex, out outBeginIdx, out outNBElement, ref CandleStickPattern );
                                                Bars.ApplyPatternsToBars( outBeginIdx, outNBElement, CandleStickPattern );

                                            }, IndicatorExitToken
                                        );

            detectTask.Start();

            return detectTask;
        }

        protected Task TaskLadderBottom( bool fullRecalculation, DataBarUpdateType? updateType )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            Task detectTask  = new Task(
                                            () =>
                                            {
                                                //ThreadHelper.UpdateThreadName( "TaskLadderBottom" );

                                                TACandle[ ] CandleStickPattern;

                                                int repoStartingIndex = 0;

                                                if ( fullRecalculation == false )
                                                {
                                                    // I only need to trace back like 15 candles as the most candles that I will need to compare is 13
                                                    repoStartingIndex = Math.Max( 0, _lastCandleCheckIndex - LadderBottomLookBack() - 1 );
                                                }

                                                int startIndex     = repoStartingIndex;
                                                int endIndex       = _barCountBeforeCalculation - 2;
                                                int indexCount     = endIndex - startIndex + 1;
                                                int outBeginIdx    = 0;
                                                int outNBElement  = 0;
                                                CandleStickPattern = new TACandle[ indexCount ];

                                                LadderBottom5( startIndex, endIndex, out outBeginIdx, out outNBElement, ref CandleStickPattern );
                                                Bars.ApplyPatternsToBars( outBeginIdx, outNBElement, CandleStickPattern );

                                            }, IndicatorExitToken
                                        );

            detectTask.Start();

            return detectTask;
        }


        protected Task TaskRisingOrFallingThree( bool fullRecalculation, DataBarUpdateType? updateType )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            Task detectTask  = new Task(
                                            () =>
                                            {
                                                //ThreadHelper.UpdateThreadName( "TaskRisingOrFallingThree" );

                                                TACandle[ ] CandleStickPattern;

                                                int repoStartingIndex = 0;

                                                if ( fullRecalculation == false )
                                                {
                                                    // I only need to trace back like 15 candles as the most candles that I will need to compare is 13
                                                    repoStartingIndex = Math.Max( 0, _lastCandleCheckIndex - RisingOrFallingLoookBack() - 1 );
                                                }

                                                int startIndex     = repoStartingIndex;
                                                int endIndex       = _barCountBeforeCalculation - 2;
                                                int indexCount     = endIndex - startIndex + 1;
                                                int outBeginIdx    = 0;
                                                int outNBElement  = 0;
                                                CandleStickPattern = new TACandle[ indexCount ];

                                                RisingOrFalling3Methods456( startIndex, endIndex, out outBeginIdx, out outNBElement, ref CandleStickPattern );
                                                Bars.ApplyPatternsToBars( outBeginIdx, outNBElement, CandleStickPattern );

                                            }, IndicatorExitToken
                                        );

            detectTask.Start();

            return detectTask;
        }

        protected Task TaskInvertedHammer( bool fullRecalculation, DataBarUpdateType? updateType )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            Task detectTask  = new Task(
                                            () =>
                                            {
                                                //ThreadHelper.UpdateThreadName( "TaskInvertedHammer" );

                                                TACandle[ ] CandleStickPattern;

                                                int repoStartingIndex = 0;

                                                if ( fullRecalculation == false )
                                                {
                                                    // I only need to trace back like 15 candles as the most candles that I will need to compare is 13
                                                    repoStartingIndex = Math.Max( 0, _lastCandleCheckIndex - InvertedHammerLookBack() - 1 );
                                                }

                                                int startIndex     = repoStartingIndex;
                                                int endIndex       = _barCountBeforeCalculation - 2;
                                                int indexCount     = endIndex - startIndex + 1;
                                                int outBeginIdx    = 0;
                                                int outNBElement  = 0;
                                                CandleStickPattern = new TACandle[ indexCount ];

                                                InvertedHammer2( startIndex, endIndex, out outBeginIdx, out outNBElement, ref CandleStickPattern );
                                                Bars.ApplyPatternsToBars( outBeginIdx, outNBElement, CandleStickPattern );

                                            }, IndicatorExitToken
                                        );

            detectTask.Start();

            return detectTask;
        }



        protected Task TaskThreeBlackCrowOrWhiteSoliders( bool fullRecalculation, DataBarUpdateType? updateType )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            Task detectTask  = new Task(
                                            () =>
                                            {
                                                //ThreadHelper.UpdateThreadName( "TaskThreeBlackCrowOrWhiteSoliders" );

                                                TACandle[ ] CandleStickPattern;

                                                int repoStartingIndex = 0;

                                                if ( fullRecalculation == false )
                                                {
                                                    // I only need to trace back like 15 candles as the most candles that I will need to compare is 13
                                                    repoStartingIndex = Math.Max( 0, _lastCandleCheckIndex - ThreeBlackCrowsOrWhiteSoldiersLookback() - 1 );
                                                }

                                                int startIndex     = repoStartingIndex;
                                                int endIndex       = _barCountBeforeCalculation - 2;
                                                int indexCount     = endIndex - startIndex + 1;
                                                int outBeginIdx    = 0;
                                                int outNBElement  = 0;
                                                CandleStickPattern = new TACandle[ indexCount ];

                                                ThreeBlackCrowsOrWhiteSoldiers4( startIndex, endIndex, out outBeginIdx, out outNBElement, ref CandleStickPattern );
                                                Bars.ApplyPatternsToBars( outBeginIdx, outNBElement, CandleStickPattern );

                                            }, IndicatorExitToken
                                        );

            detectTask.Start();

            return detectTask;
        }



        protected Task TaskBreakaway( bool fullRecalculation, DataBarUpdateType? updateType )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            Task detectTask  = new Task(
                                            () =>
                                            {
                                                //ThreadHelper.UpdateThreadName( "TaskBreakaway" );

                                                TACandle[ ] CandleStickPattern;

                                                int repoStartingIndex = 0;

                                                if ( fullRecalculation == false )
                                                {
                                                    // I only need to trace back like 15 candles as the most candles that I will need to compare is 13
                                                    repoStartingIndex = Math.Max( 0, _lastCandleCheckIndex - BreakAwayLookBack() - 1 );
                                                }

                                                int startIndex     = repoStartingIndex;
                                                int endIndex       = _barCountBeforeCalculation - 2;
                                                int indexCount     = endIndex - startIndex + 1;
                                                int outBeginIdx    = 0;
                                                int outNBElement  = 0;
                                                CandleStickPattern = new TACandle[ indexCount ];

                                                BreakAway4567( startIndex, endIndex, out outBeginIdx, out outNBElement, ref CandleStickPattern );
                                                Bars.ApplyPatternsToBars( outBeginIdx, outNBElement, CandleStickPattern );

                                            }, IndicatorExitToken
                                        );

            detectTask.Start();

            return detectTask;
        }

        //protected Task TaskMeetingLinesPattern( bool fullRecalculation )
        //{
        //    Task detectTask  = new Task(
        //                                    () =>
        //                                    {
        //                                        TACandle[ ] CandleStickPattern;

        //                                        int repoStartingIndex = 0;

        //                                        if ( fullRecalculation == false )
        //                                        {
        //                                            // I only need to trace back like 15 candles as the most candles that I will need to compare is 13
        //                                            repoStartingIndex = Math.Max( 0, _lastCandleCheckIndex - MeetingLinesLoopBack() - 1 );
        //                                        }

        //                                        int startIndex     = repoStartingIndex;
        //                                        int endIndex       = _barCountBeforeCalculation - 2;
        //                                        int indexCount     = endIndex - startIndex + 1;
        //                                        int outBeginIdx    = 0;
        //                                        int outNBElement  = 0;
        //                                        CandleStickPattern = new TACandle[ indexCount ];

        //                                        MeetingLinesBear2( startIndex, endIndex, out outBeginIdx, out outNBElement, ref CandleStickPattern );
        //                                        DatabarsRepo.ApplyPatternsToBars( outBeginIdx, outNBElement, CandleStickPattern );

        //                                    }, IndicatorExitToken
        //                                );

        //    detectTask.Start();

        //    return detectTask;
        //}

        //protected Task TaskKicking( bool fullRecalculation )
        //{
        //    Task detectTask  = new Task(
        //                                    () =>
        //                                    {
        //                                        TACandle[ ] CandleStickPattern;

        //                                        int repoStartingIndex = 0;

        //                                        if ( fullRecalculation == false )
        //                                        {
        //                                            // I only need to trace back like 15 candles as the most candles that I will need to compare is 13
        //                                            repoStartingIndex = Math.Max( 0, _lastCandleCheckIndex - KickingLoopBack() - 1 );
        //                                        }

        //                                        int startIndex     = repoStartingIndex;
        //                                        int endIndex       = _barCountBeforeCalculation - 2;
        //                                        int indexCount     = endIndex - startIndex + 1;
        //                                        int outBeginIdx    = 0;
        //                                        int outNBElement  = 0;
        //                                        CandleStickPattern = new TACandle[ indexCount ];


        //                                        //for ( int i = 0; i < indexCount; i++ ) CandleStickPattern[ i ] = CandlestickPatterns.None;
        //                                        //outBeginIdx   = 0;
        //                                        //outNBElement = 0;
        //                                        //Kicking2( repoStartingIndex, startIndex, endIndex, out outBeginIdx, out outNBElement, ref CandleStickPattern );
        //                                        //         MutltiTimeFrameSessionDataRepo.ApplyPatternsToBars( repoStartingIndex + outBeginIdx, outNBElement, CandleStickPattern );

        //                                        Kicking2( startIndex, endIndex, out outBeginIdx, out outNBElement, ref CandleStickPattern );
        //                                        DatabarsRepo.ApplyPatternsToBars( outBeginIdx, outNBElement, CandleStickPattern );

        //                                    }, IndicatorExitToken
        //                                );

        //    detectTask.Start();

        //    return detectTask;
        //}



        /// <summary>
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\bearish_three_line_strike.jpg" />
        /// After an established downtrend three long red days in a row continue this move, each closing lower than the previous day
        /// 
        /// Day-four is blue candle that closes near the open of the first day
        /// 
        /// So long as the previous downtrend is an established one, candlestick analysts view this formation as a sign that the downtrend may still continue.
        /// 
        /// The first three days serve as a fairly clear bearish move. Up to day-three in fact we have a Three Black Crows formation which is a strong bearish signal. 
        /// One day of rally that only goes up to the open price of the patterns start is considered to be more sellers covering their positions than any true sign of a reversal. 
        /// Thus traders will watch for short entry opportunities to come.
        /// 
        /// Since the overall signal is fairly weak, most will want confirmation in the form of bearish price action the next day

        /// Proceed with the calculation for the requested range.
        ///     Must have:
        ///     three white soldiers (three black crows): three white (black) candlesticks with consecutively higher (lower) closes,
        ///     each opening within or near the previous real body
        ///     fourth candle: black (white) candle that opens above (below) prior candle's close and closes below (above) 
        ///     the first candle's open
        ///      The meaning of "near" is specified with TA_SetCandleSettings;
        ///      outInteger is positive (1 to 100) when bullish or negative (-1 to -100) when bearish;
        ///      the user should consider that 3-line strike is significant when it appears in a trend in the same direction of
        ///      the first three candles, while this function does not consider it
        /// </summary>         

        public int ThreeLineStrikeLookBack()
        {
            return Core.Cdl3LineStrikeLookback();
        }

        public RetCode ThreeLineStrike4( int startIdx, int endIdx, out int outBegIdx, out int outNBElement, ref TACandle[ ] buffer )
        {
            double[] NearPeriodTotal = new double[4];

            int i, outIdx, totIdx, NearTrailingIdx, lookbackTotal;

            outNBElement = 0;
            outBegIdx = 0;

            if ( startIdx < 0 ) return RetCode.OutOfRangeStartIndex;

            if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return RetCode.OutOfRangeEndIndex;

            lookbackTotal = ThreeLineStrikeLookBack();

            if ( startIdx < lookbackTotal ) startIdx = lookbackTotal;

            if ( startIdx > endIdx )
            {
                return RetCode.Success;
            }

            NearPeriodTotal[ 3 ] = 0;
            NearPeriodTotal[ 2 ] = 0;
            NearTrailingIdx = startIdx - CandleAvgPeriod( CandleSettingEnum.Near );

            i = NearTrailingIdx;
            while ( i < startIdx )
            {
                NearPeriodTotal[ 3 ] += CandleRange( CandleSettingEnum.Near, i - 3 );
                NearPeriodTotal[ 2 ] += CandleRange( CandleSettingEnum.Near, i - 2 );
                i++;
            }
            i = startIdx;

            outIdx = 0;
            do
            {

                if ( CandleColor( i - 3 ) == CandleColor( i - 2 ) &&        // three with same color
                     CandleColor( i - 2 ) == CandleColor( i - 1 ) &&
                     CandleColor( i ) == -CandleColor( i - 1 ) )            // 4th opposite color
                {
                    double bar3NearAvg = CandleAverage( CandleSettingEnum.Near, NearPeriodTotal[ 3 ], i - 3 );
                    double bar2NearAvg = CandleAverage( CandleSettingEnum.Near, NearPeriodTotal[ 2 ], i - 2 );

                    if ( Bars[ i - 2 ].Open >= Math.Min( Bars[ i - 3 ].Open, Bars[ i - 3 ].Close ) - bar3NearAvg &&
                         Bars[ i - 2 ].Open <= Math.Max( Bars[ i - 3 ].Open, Bars[ i - 3 ].Close ) + bar3NearAvg &&
                         Bars[ i - 1 ].Open >= Math.Min( Bars[ i - 2 ].Open, Bars[ i - 2 ].Close ) - bar2NearAvg &&
                         Bars[ i - 1 ].Open <= Math.Max( Bars[ i - 2 ].Open, Bars[ i - 2 ].Close ) + bar2NearAvg )
                    {
                        if ( // if three white
                                CandleColor( i - 1 ) == 1 && Bars[ i - 1 ].Close > Bars[ i - 2 ].Close && Bars[ i - 2 ].Close > Bars[ i - 3 ].Close &&      // consecutive higher closes
                                Bars[ i ].High > Bars[ i - 1 ].Close &&                                                                             // 4th opens above prior close
                                Bars[ i ].Close < Bars[ i - 3 ].Open// 4th closes below 1st open
                           )
                        {
                            if ( outIdx - 3 >= 0 )
                            {
                                buffer[ outIdx - 1 ] = TACandle.Cdl3LineStrikeBear;
                                buffer[ outIdx - 2 ] = TACandle.Cdl3LineStrikeBear;
                                buffer[ outIdx - 3 ] = TACandle.Cdl3LineStrikeBear;
                                buffer[ outIdx ] = TACandle.Cdl3LineStrikeBear;
                            }
                            else
                            {
                                buffer[ outIdx ] = TACandle.Cdl3LineStrikeBear;
                            }

                        }
                        else if
                             ( // if three black
                                 CandleColor( i - 1 ) == -1 && Bars[ i - 1 ].Close < Bars[ i - 2 ].Close && Bars[ i - 2 ].Close < Bars[ i - 3 ].Close &&    // consecutive lower closes
                                 Bars[ i ].Low < Bars[ i - 1 ].Close &&                                                                             // 4th opens below prior close
                                 Bars[ i ].Close > Bars[ i - 3 ].Open// 4th closes above 1st open
                             )
                        {
                            if ( outIdx - 3 >= 0 )
                            {
                                buffer[ outIdx - 1 ] = TACandle.Cdl3LineStrikeBull;
                                buffer[ outIdx - 2 ] = TACandle.Cdl3LineStrikeBull;
                                buffer[ outIdx - 3 ] = TACandle.Cdl3LineStrikeBull;
                                buffer[ outIdx ] = TACandle.Cdl3LineStrikeBull;
                            }
                            else
                            {
                                buffer[ outIdx ] = TACandle.Cdl3LineStrikeBull;
                            }
                        }
                        else
                        {
                            buffer[ outIdx ] = 0;
                        }
                    }
                }

                outIdx++;
                /* add the current range and subtract the first range: this is done after the pattern recognition 
				 * when avgPeriod is not 0, that means "compare with the previous candles" (it excludes the current candle)
				 */
                for ( totIdx = 3; totIdx >= 2; --totIdx )
                {
                    NearPeriodTotal[ totIdx ] += CandleRange( CandleSettingEnum.Near, i - totIdx ) - CandleRange( CandleSettingEnum.Near, NearTrailingIdx - totIdx );
                }

                i++;
                NearTrailingIdx++;
            } while ( i <= endIdx );

            outNBElement = outIdx;
            outBegIdx = startIdx;

            return RetCode.Success;
        }



        /// <summary>
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\bullish_three_white_soldiers.jpg" />
        public int ThreeBlackCrowsOrWhiteSoldiersLookback()
        {
            return ( Core.BbandsLookback( Core.MAType.Ema, 20 ) );
        }

        public RetCode ThreeBlackCrowsOrWhiteSoldiers4( int startIdx, int endIdx, out int outBegIdx, out int outNBElement, ref TACandle[ ] buffer )
        {
            int i, outIdx, lookbackCount;

            outNBElement = 0;
            outBegIdx = 0;

            if ( startIdx < 0 ) return RetCode.OutOfRangeStartIndex;

            if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return RetCode.OutOfRangeEndIndex;

            lookbackCount = ThreeBlackCrowsOrWhiteSoldiersLookback();

            if ( startIdx < lookbackCount ) startIdx = lookbackCount;

            if ( startIdx > endIdx )
            {
                return RetCode.Success;
            }


            i = startIdx;

            /* Proceed with the calculation for the requested range.
			* Must have:
			* - three consecutive and declining black candlesticks
			* - each candle must have no or very short lower shadow
			* - each candle after the first must open within the prior candle's real body
			* - the first candle's close should be under the prior white candle's high
			* The meaning of "very short" is specified with TA_SetCandleSettings
			* outInteger is negative (-1 to -100): three black crows is always bearish; 
			* the user should consider that 3 black crows is significant when it appears after a mature advance or at high levels, 
			* while this function does not consider it
			*/
            outIdx = 0;
            do
            {
                if ( CandleColor( i - 3 ) == 1 &&       // white
                     CandleColor( i - 2 ) == -1 &&      // 1st black                     
                     CandleColor( i - 1 ) == -1 &&      // 2nd black                     
                     CandleColor( i ) == -1 )           // 3rd black                     
                {

                    if ( Bars[ i ].Open < Bars[ i - 1 ].Open && Bars[ i - 1 ].Open < Bars[ i - 2 ].Open &&
                            Bars[ i ].Close < Bars[ i - 1 ].Close && Bars[ i - 1 ].Close < Bars[ i - 2 ].Close &&                 // three consecutive and declining black candlesticks                            
                            !IsShortCandle( i - 2 ) &&
                            !IsShortCandle( i - 1 ) &&
                            !IsShortCandle( i )               // 1st black closes under prior candle's high
                        )
                    {
                        DateTime? barTime = Bars.GetTimeAtIndex( i - 3 );


                        // Tony:    Here I would like to check to see if MACD is buy at the i-3 candle.
                        //          and that i-3 candle is above the inner bollinger band

                        double innerBBUpper = IndicatorResult[ "InnerBBUpper" ][ i - 3 ];
                        double macd         = IndicatorResult[ "MACD" ][ i - 3 ];
                        double macdSignal   = IndicatorResult[ "MACDSignal" ][ i - 3 ];

                        //if ( ( _close[ i - 3 ] > innerBBUpper ) )
                        {
                            if ( IsAboveAverageCandle( i - 2 ) && // each candle must have no or very short lower shadow
                                 IsAboveAverageCandle( i - 1 ) &&
                                 IsAboveAverageCandle( i ) )
                            {
                                if ( outIdx - 3 >= 0 )
                                {
                                    buffer[ outIdx - 3 ] = TACandle.Cdl3BlackCrows;
                                    buffer[ outIdx - 2 ] = TACandle.Cdl3BlackCrows;
                                    buffer[ outIdx - 1 ] = TACandle.Cdl3BlackCrows;
                                    buffer[ outIdx ] = TACandle.Cdl3BlackCrows;
                                }
                                else
                                {
                                    buffer[ outIdx ] = TACandle.Cdl3BlackCrows;
                                }


                            }
                            else if ( IsLowerShadowShort( i - 2 ) && // each candle must have no or very short lower shadow
                                        IsLowerShadowShort( i - 1 ) &&
                                        IsLowerShadowShort( i ) )
                            {
                                if ( outIdx - 3 >= 0 )
                                {
                                    buffer[ outIdx - 3 ] = TACandle.Cdl3BlackCrows;
                                    buffer[ outIdx - 2 ] = TACandle.Cdl3BlackCrows;
                                    buffer[ outIdx - 1 ] = TACandle.Cdl3BlackCrows;
                                    buffer[ outIdx ] = TACandle.Cdl3BlackCrows;
                                }
                                else
                                {
                                    buffer[ outIdx ] = TACandle.Cdl3BlackCrows;
                                }
                            }
                            else
                            {
                                // Here I want to determine if at least two are short, the other one as long as it is not long, we can still accept this pattern.

                                int notShort = -1;

                                int index = 0;

                                if ( IsLowerShadowShort( i - 2 ) )
                                {
                                    index++;
                                }
                                else
                                {
                                    notShort = i - 2;
                                }

                                if ( IsLowerShadowShort( i - 1 ) )
                                {
                                    index++;
                                }
                                else
                                {
                                    notShort = i - 1;
                                }

                                if ( IsLowerShadowShort( i ) )
                                {
                                    index++;
                                }
                                else
                                {
                                    notShort = i;
                                }

                                if ( index == 2 )
                                {
                                    if ( IsLowerShadowNotLong( notShort ) )
                                    {
                                        if ( outIdx - 3 >= 0 )
                                        {
                                            buffer[ outIdx - 3 ] = TACandle.Cdl3BlackCrows;
                                            buffer[ outIdx - 2 ] = TACandle.Cdl3BlackCrows;
                                            buffer[ outIdx - 1 ] = TACandle.Cdl3BlackCrows;
                                            buffer[ outIdx ] = TACandle.Cdl3BlackCrows;
                                        }
                                        else
                                        {
                                            buffer[ outIdx ] = TACandle.Cdl3BlackCrows;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                /* Proceed with the calculation for the requested range.
				* Must have:
				* - three white candlesticks with consecutively higher closes
				* - Greg Morris wants them to be long, Steve Nison doesn't; anyway they should not be short
				* - each candle opens within or near the previous white real body 
				* - each candle must have no or very short upper shadow
				* - to differentiate this pattern from advance block, each candle must not be far shorter than the prior candle
				* The meanings of "not short", "very short shadow", "far" and "near" are specified with TA_SetCandleSettings;
				* here the 3 candles must be not short, if you want them to be long use TA_SetCandleSettings on BodyShort;
				* outInteger is positive (1 to 100): advancing 3 white soldiers is always bullish;
				* the user should consider that 3 white soldiers is significant when it appears in downtrend, while this function 
				* does not consider it
				*/
                else if ( CandleColor( i - 3 ) == -1 &&       // Black
                            CandleColor( i - 2 ) == 1 &&      // 1st white                     
                            CandleColor( i - 1 ) == 1 &&      // 2nd white                     
                            CandleColor( i ) == 1
                        )
                {
                    if ( Bars[ i ].Close > Bars[ i - 1 ].Close && Bars[ i - 1 ].Close > Bars[ i - 2 ].Close &&              // Consecutive higher closes                                             
                            Bars[ i ].Open > Bars[ i - 1 ].Open && Bars[ i - 1 ].Open > Bars[ i - 2 ].Open &&
                            !IsShortCandle( i - 2 ) &&
                            !IsShortCandle( i - 1 ) &&
                            !IsShortCandle( i )
                        )
                    {
                        DateTime? barTime = Bars.GetTimeAtIndex( i - 3 );


                        // Tony:    Here I would like to check to see if MACD is buy at the i-3 candle.
                        //          and that i-3 candle is above the inner bollinger band

                        //double innerBBLower = Results[ "InnerBBLower" ][ i - 3 ];
                        //double macd         = Results[ "MACD" ][ i - 3 ];
                        //double macdSignal   = Results[ "MACDSignal" ][ i - 3 ];

                        //if ( ( _close[ i - 3 ] < innerBBLower ) ) // &&( macd < macdSignal ) )

                        {
                            if ( IsAboveAverageCandle( i - 2 ) && // each candle must have no or very short lower shadow
                                 IsAboveAverageCandle( i - 1 ) &&
                                 IsAboveAverageCandle( i ) )
                            {
                                if ( outIdx - 3 >= 0 )
                                {
                                    buffer[ outIdx - 3 ] = TACandle.Cdl3WhiteSoldiers;
                                    buffer[ outIdx - 2 ] = TACandle.Cdl3WhiteSoldiers;
                                    buffer[ outIdx - 1 ] = TACandle.Cdl3WhiteSoldiers;
                                    buffer[ outIdx ] = TACandle.Cdl3WhiteSoldiers;
                                }
                                else
                                {
                                    buffer[ outIdx ] = TACandle.Cdl3WhiteSoldiers;
                                }


                            }

                            else if ( IsUpperShadowShort( i - 2 ) && // each candle must have no or very short lower shadow
                                 IsUpperShadowShort( i - 1 ) &&
                                 IsUpperShadowShort( i ) )
                            {
                                if ( outIdx - 3 >= 0 )
                                {
                                    buffer[ outIdx - 3 ] = TACandle.Cdl3WhiteSoldiers;
                                    buffer[ outIdx - 2 ] = TACandle.Cdl3WhiteSoldiers;
                                    buffer[ outIdx - 1 ] = TACandle.Cdl3WhiteSoldiers;
                                    buffer[ outIdx ] = TACandle.Cdl3WhiteSoldiers;
                                }
                                else
                                {
                                    buffer[ outIdx ] = TACandle.Cdl3WhiteSoldiers;
                                }
                            }
                            else
                            {
                                // Here I want to determine if at least two are short, the other one as long as it is not long, we can still accept this pattern.

                                int notShort = -1;

                                int index = 0;

                                if ( IsUpperShadowShort( i - 2 ) )
                                {
                                    index++;
                                }
                                else
                                {
                                    notShort = i - 2;
                                }

                                if ( IsUpperShadowShort( i - 1 ) )
                                {
                                    index++;
                                }
                                else
                                {
                                    notShort = i - 1;
                                }

                                if ( IsUpperShadowShort( i ) )
                                {
                                    index++;
                                }
                                else
                                {
                                    notShort = i;
                                }

                                if ( index == 2 )
                                {
                                    if ( IsUpperShadowNotLong( notShort ) )
                                    {
                                        if ( outIdx - 3 >= 0 )
                                        {
                                            buffer[ outIdx - 3 ] = TACandle.Cdl3WhiteSoldiers;
                                            buffer[ outIdx - 2 ] = TACandle.Cdl3WhiteSoldiers;
                                            buffer[ outIdx - 1 ] = TACandle.Cdl3WhiteSoldiers;
                                            buffer[ outIdx ] = TACandle.Cdl3WhiteSoldiers;
                                        }
                                        else
                                        {
                                            buffer[ outIdx ] = TACandle.Cdl3WhiteSoldiers;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                outIdx++;
                i++;

            } while ( i <= endIdx );

            outNBElement = outIdx;
            outBegIdx = startIdx;

            return RetCode.Success;
        }


        /// <summary>
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\bullish_morning_star.jpg" />
        /// /// <image url="$(SolutionDir)\..\..\30 - CommonImages\bullish_abandoned_baby.jpg" />
        /// After an established downtrend, day-one is a long red day
        /// Day-two is a short candle or Star candle.
        /// Day-three is a blue candle
        /// 
        /// Morning Stars start with a continuation of the bearish move. The second day sees a continuation of the move down, but a rally makes the market close at or near the open for the day. 
        /// The first two candles weakly suggest a loss of bearish momentum. In fact up to day two this formation looks close to the Bullish Hammer moderate strength reversal pattern.
        /// 
        /// The Bullish Hammer alone is decent signals for a rally on day-three. But since the certainty for a Hammer indicator is low, 
        /// the trend reversal should be confirmed by a blue candlestick the next day. The higher price is able to move up on day-three, the stronger the reversal signal.
        /// 
        /// The Forex Market version of this formation would share the same market close price on day one, and then start day twos sell-off from there. 
        /// Day twos close would be the same whether in FX or any other market restricted to fixed exchange hours, 
        /// forming a candle similar to the Hammer. Thus this formation might more aptly be called Evening Hammer Star when applied to the Foreign Exchange Market.
        /// </summary>

        public int MorningStarOrDojiLookback()
        {
            return ( Core.BbandsLookback( Core.MAType.Ema, 20 ) );
        }

        public bool MorningStarOrDoji34( int barB4Calculation, int startIdx, int endIdx, out int outBegIdx, out int outNBElement, ref TACandle[ ] buffer, out CandleStrength strength )
        {
            int i, outIdx, lookbackCount;

            outNBElement = 0;
            outBegIdx = 0;
            strength = CandleStrength.UNKNOWN;

            if ( startIdx < 0 ) return false;

            if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return false;

            lookbackCount = MorningStarOrDojiLookback();

            if ( startIdx < lookbackCount ) startIdx = lookbackCount;

            if ( startIdx > endIdx )
            {
                return false;
            }

            i = startIdx;

            bool found = false;

            /* Proceed with the calculation for the requested range.
				* Must have:
				* - first candle: long white real body
				* - second candle: star (short real body gapping up)
				* - third candle: black real body that moves well within the first candle's real body
				* The meaning of "short" and "long" is specified with TA_SetCandleSettings
				* The meaning of "moves well within" is specified with optInPenetration and "moves" should mean the real body should
				* not be short ("short" is specified with TA_SetCandleSettings) - Greg Morris wants it to be long, someone else want
				* it to be relatively long
				* outInteger is negative (-1 to -100): evening star is always bearish; 
				* the user should consider that an evening star is significant when it appears in an uptrend, 
				* while this function does not consider the trend
				*/
            outIdx = 0;
            do
            {
                DateTime? barTime = Bars.GetTimeAtIndex( i );
                var period    = Bars.Period.Value;

                if ( ( IsBlackCandle( i ) &&
                         IsAboveAverageCandle( i - 3 ) &&                               // 1st: long                     
                         IsShortCandle( i - 1 ) &&
                         IsShortCandle( i - 2 ) &&
                         IsLongerThanShortCandle( i ) &&
                         Bars[ i - 2 ].High < Bars[ i - 3 ].High &&
                         Bars[ i - 1 ].High < Bars[ i - 3 ].High &&
                         IsWhiteCandle( i )
                     ) ||

                    ( IsBlackCandle( i - 3 ) &&
                         IsLongerThanShortCandle( i - 3 ) &&                               // 1st: long                     
                         IsShortCandle( i - 1 ) &&
                         IsShortCandle( i - 2 ) &&
                         Bars[ i - 2 ].High < Bars[ i - 3 ].High &&
                         Bars[ i - 1 ].High < Bars[ i - 3 ].High &&
                         IsAboveAverageCandle( i ) &&
                         IsWhiteCandle( i )
                     )
                    )
                {


                    double innerBBLower  = IndicatorResult[ "InnerBBLower" ][ i - 1 ];
                    double outerBBLower  = IndicatorResult[ "OuterBBLower" ][ i - 1 ];
                    double innerBBLower2 = IndicatorResult[ "InnerBBLower" ][ i - 2 ];
                    double outerBBLower2 = IndicatorResult[ "OuterBBLower" ][ i - 2 ];

                    if ( ( Bars[ i ].Close > Bars[ i - 3 ].Close + RealBodyLength( i - 3 ) * 0.5 ) || ( IsLongCandle( i - 3 ) && IsAboveAverageCandle( i ) ) )                                     // closing well within 1st rb 
                    {
                        if ( ( Bars[ i - 1 ].Close < innerBBLower || Bars[ i - 1 ].Low < outerBBLower ) ||
                             ( Bars[ i - 2 ].Close < innerBBLower2 || Bars[ i - 2 ].Low < outerBBLower2 ) )
                        {
                            if ( IsDojiCandle( i - 1 ) &&
                                 IsDojiCandle( i - 2 ) )
                            {
                                if ( outIdx - 3 >= 0 )
                                {
                                    buffer[ outIdx - 3 ] = TACandle.CdlMorningStar;
                                    buffer[ outIdx - 2 ] = TACandle.CdlDoji;
                                    buffer[ outIdx - 1 ] = TACandle.CdlDoji;
                                    buffer[ outIdx ] = TACandle.CdlMorningStar;
                                }
                                else
                                {
                                    buffer[ outIdx ] = TACandle.CdlMorningStar;
                                }

                                found = true;

                            }
                            else if ( ( ( RealBodyLength( i - 2 ) + RealBodyLength( i - 1 ) ) < RealBodyLength( i - 3 ) / 2 ) ||
                                      ( ( RealBodyLength( i - 2 ) + RealBodyLength( i - 1 ) ) < RealBodyLength( i ) / 2 )
                                    )
                            {
                                if ( outIdx - 3 >= 0 )
                                {
                                    buffer[ outIdx - 3 ] = TACandle.CdlMorningStar;
                                    buffer[ outIdx - 2 ] = TACandle.CdlMorningStar;
                                    buffer[ outIdx - 1 ] = TACandle.CdlMorningStar;
                                    buffer[ outIdx ] = TACandle.CdlMorningStar;
                                }
                                else
                                {
                                    buffer[ outIdx ] = TACandle.CdlMorningStar;
                                }

                                found = true;
                            }
                        }
                        else
                        {
                            if ( IsDojiCandle( i - 1 ) &&
                                 IsDojiCandle( i - 2 ) )
                            {
                                if ( outIdx - 3 >= 0 )
                                {
                                    buffer[ outIdx - 3 ] = TACandle.CdlMorningStar;
                                    buffer[ outIdx - 2 ] = TACandle.CdlDoji;
                                    buffer[ outIdx - 1 ] = TACandle.CdlDoji;
                                    buffer[ outIdx ] = TACandle.CdlMorningStar;
                                }
                                else
                                {
                                    buffer[ outIdx ] = TACandle.CdlMorningStar;
                                }

                                found = true;
                            }
                            else if ( ( ( RealBodyLength( i - 2 ) + RealBodyLength( i - 1 ) ) < RealBodyLength( i - 3 ) / 2 ) ||
                                      ( ( RealBodyLength( i - 2 ) + RealBodyLength( i - 1 ) ) < RealBodyLength( i ) / 2 )
                                    )
                            {
                                if ( outIdx - 3 >= 0 )
                                {
                                    buffer[ outIdx - 3 ] = TACandle.CdlMorningStar;
                                    buffer[ outIdx - 2 ] = TACandle.CdlMorningStar;
                                    buffer[ outIdx - 1 ] = TACandle.CdlMorningStar;
                                    buffer[ outIdx ] = TACandle.CdlMorningStar;
                                }
                                else
                                {
                                    buffer[ outIdx ] = TACandle.CdlMorningStar;
                                }

                                found = true;
                            }
                        }
                    }
                }
                else if ( ( IsBlackCandle( i - 2 ) &&
                                IsAboveAverageCandle( i - 2 ) &&                  // 1st: long                                               
                                Bars[ i - 1 ].High < Bars[ i - 2 ].High &&
                                IsLongerThanShortCandle( i ) &&
                                IsWhiteCandle( i )
                             )
                         ||
                             (
                                IsBlackCandle( i - 2 ) &&
                                IsLongerThanShortCandle( i - 2 ) &&                  // 1st: long                                               
                                Bars[ i - 1 ].High < Bars[ i - 2 ].High &&
                                IsAboveAverageCandle( i ) &&
                                 IsWhiteCandle( i )
                             )
                        )
                {
                    if ( Bars[ i ].Close > Bars[ i - 2 ].Close + RealBodyLength( i - 2 ) * 0.5 || ( IsLongCandle( i - 2 ) && IsAboveAverageCandle( i ) ) )                                         // closing well within 1st rb
                    {
                        double innerBBLower  = IndicatorResult[ "InnerBBLower" ][ i - 1 ];
                        double outerBBLower  = IndicatorResult[ "OuterBBLower" ][ i - 1 ];

                        if ( Bars[ i - 1 ].Close < innerBBLower ||
                             Bars[ i - 1 ].Low < outerBBLower )
                        {
                            if ( IsShortCandle( i - 1 ) ) // 2nd: short
                            {
                                if ( IsDojiCandle( i - 1 ) )
                                {
                                    if ( outIdx - 2 >= 0 )
                                    {
                                        buffer[ outIdx - 2 ] = TACandle.CdlMorningStar;
                                        buffer[ outIdx - 1 ] = TACandle.CdlDoji;
                                        buffer[ outIdx ] = TACandle.CdlMorningStar;
                                    }
                                    else
                                    {
                                        buffer[ outIdx ] = TACandle.CdlMorningStar;
                                    }

                                    found = true;
                                }
                                else
                                {
                                    if ( outIdx - 2 >= 0 )
                                    {
                                        buffer[ outIdx - 2 ] = TACandle.CdlMorningStar;
                                        buffer[ outIdx - 1 ] = TACandle.CdlMorningStar;
                                        buffer[ outIdx ] = TACandle.CdlMorningStar;
                                    }
                                    else
                                    {
                                        buffer[ outIdx ] = TACandle.CdlMorningStar;
                                    }

                                    found = true;
                                }
                            }
                            //else if ( ( RealBodyLength( i - 1 ) < RealBodyLength( i ) / 2 ) &&
                            //          ( RealBodyLength( i - 1 ) < RealBodyLength( i - 2 ) / 2 ) )
                            //{
                            //    buffer[ outIdx - 2 ] = CandlestickPatterns.CdlMorningStar;
                            //    buffer[ outIdx - 1 ] = CandlestickPatterns.CdlMorningStar;
                            //    buffer[ outIdx ]     = CandlestickPatterns.CdlMorningStar;
                            //}
                        }
                        else
                        {
                            if ( IsShortCandle( i - 1 ) ) // 2nd: short
                            {
                                if ( IsDojiCandle( i - 1 ) )
                                {
                                    if ( outIdx - 2 >= 0 )
                                    {
                                        buffer[ outIdx - 2 ] = TACandle.CdlMorningStar;
                                        buffer[ outIdx - 1 ] = TACandle.CdlDoji;
                                        buffer[ outIdx ] = TACandle.CdlMorningStar;
                                    }
                                    else
                                    {
                                        buffer[ outIdx ] = TACandle.CdlMorningStar;
                                    }

                                    found = true;
                                }
                                else
                                {
                                    if ( outIdx - 2 >= 0 )
                                    {
                                        buffer[ outIdx - 2 ] = TACandle.CdlMorningStar;
                                        buffer[ outIdx - 1 ] = TACandle.CdlMorningStar;
                                        buffer[ outIdx ] = TACandle.CdlMorningStar;
                                    }
                                    else
                                    {
                                        buffer[ outIdx ] = TACandle.CdlMorningStar;
                                    }

                                    found = true;
                                }
                            }
                        }
                    }
                }


                /* add the current range and subtract the first range: this is done after the pattern recognition 
				 * when avgPeriod is not 0, that means "compare with the previous candles" (it excludes the current candle)
				 */
                i++;
                outIdx++;

            } while ( i <= endIdx );

            outNBElement = outIdx;
            outBegIdx = startIdx;

            return found;
        }


        public int EveningStarOrDojiLoopBack()
        {
            return ( Core.BbandsLookback( Core.MAType.Ema, 20 ) );
        }

        /// <summary>
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\bearish_evening_star.jpg" />
        /// 
        /// Evening Stars start with a continuation of the bullish move. The second day sees a continuation of the move up, but a sell-off makes the market close at or near the open for the day. 
        /// The first two candles meekly suggest a loss of bullish momentum. In fact up to day two this formation matches the Bearish Shooting Star weak-to-moderate strength reversal pattern.
        ///
        /// Although the example above is a blue shooting star, the shooting star can really be any color. 
        /// 
        /// Bearish Shooting Stars alone are decent signals for additional sell-offs on day three. Since the certainty for a shooting star indicator is low, 
        /// the trend reversal should be confirmed by a red candlestick the next day.
        /// 
        /// Thus Bearish Evening Stars require on day three a sharp sell-off after the market open. 
        /// Analysts want day threes high to be near equal to its open price, suggesting the market sell-off has no uncertainty in the new direction.        
        /// </summary>

        public int EveningStarOrDojiLookback()
        {
            return ( Core.BbandsLookback( Core.MAType.Ema, 20 ) );
        }

        public bool EveningStarOrDoji34( int barB4Calculation, int startIdx, int endIdx, out int outBegIdx, out int outNBElement, ref TACandle[ ] buffer, out CandleStrength strength )
        {
            int i, outIdx, loopback;

            outNBElement = 0;
            outBegIdx = 0;

            strength = CandleStrength.UNKNOWN;


            if ( startIdx < 0 ) return false;

            if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return false;

            loopback = EveningStarOrDojiLoopBack();

            if ( startIdx < loopback ) startIdx = loopback;

            if ( startIdx > endIdx )
            {
                return false;
            }

            bool found = false;

            i = startIdx;

            /* Proceed with the calculation for the requested range.
				* Must have:
				* - first candle: long white real body
				* - second candle: star (short real body gapping up)
				* - third candle: black real body that moves well within the first candle's real body
				* The meaning of "short" and "long" is specified with TA_SetCandleSettings
				* The meaning of "moves well within" is specified with optInPenetration and "moves" should mean the real body should
				* not be short ("short" is specified with TA_SetCandleSettings) - Greg Morris wants it to be long, someone else want
				* it to be relatively long
				* outInteger is negative (-1 to -100): evening star is always bearish; 
				* the user should consider that an evening star is significant when it appears in an uptrend, 
				* while this function does not consider the trend
				*/
            outIdx = 0;
            do
            {
                DateTime? barTime = Bars.GetTimeAtIndex( i - 1 );

                if ( ( IsWhiteCandle( i - 3 ) &&
                        IsAboveAverageWhiteCandle( i - 3 ) &&                               // 1st: long                     
                        IsShortCandle( i - 1 ) &&
                        IsShortCandle( i - 2 ) &&
                        Bars[ i - 2 ].Low > Bars[ i - 3 ].Low &&
                        Bars[ i - 1 ].Low > Bars[ i - 3 ].Low &&
                        IsLongerThanShortCandle( i ) &&
                        IsBlackCandle( i )
                      ) ||                                    // 3rd: longer than short

                     ( IsWhiteCandle( i - 3 ) &&
                        IsLongerThanShortCandle( i - 3 ) &&                               // 1st: long                     
                        IsShortCandle( i - 1 ) &&
                        IsShortCandle( i - 2 ) &&
                        Bars[ i - 2 ].Low > Bars[ i - 3 ].Low &&
                        Bars[ i - 1 ].Low > Bars[ i - 3 ].Low &&
                        IsAboveAverageWhiteCandle( i ) &&
                        IsBlackCandle( i )
                      )
                    )
                {
                    if ( Bars[ i ].Close < Bars[ i - 3 ].Close - RealBodyLength( i - 3 ) * 0.5 || ( IsLongCandle( i - 3 ) && IsAboveAverageCandle( i ) ) )                                         // closing well within 1st rb
                    {
                        double innerBBUpper  = IndicatorResult[ "InnerBBUpper" ][ i - 1 ];
                        double outerBBUpper  = IndicatorResult[ "OuterBBUpper" ][ i - 1 ];
                        double innerBBUpper2 = IndicatorResult[ "InnerBBUpper" ][ i - 2 ];
                        double outerBBUpper2 = IndicatorResult[ "OuterBBUpper" ][ i - 2 ];

                        if ( ( Bars[ i - 1 ].Close > innerBBUpper || Bars[ i - 1 ].High > outerBBUpper ) ||
                             ( Bars[ i - 2 ].Close > innerBBUpper2 || Bars[ i - 2 ].High > outerBBUpper2 ) )
                        {
                            if ( IsDojiCandle( i - 1 ) &&
                                 IsDojiCandle( i - 2 ) )
                            {
                                if ( outIdx - 3 >= 0 )
                                {
                                    buffer[ outIdx - 3 ] = TACandle.CdlEveningStar;
                                    buffer[ outIdx - 2 ] = TACandle.CdlDoji;
                                    buffer[ outIdx - 1 ] = TACandle.CdlDoji;
                                    buffer[ outIdx ] = TACandle.CdlEveningStar;
                                }
                                else
                                {
                                    buffer[ outIdx ] = TACandle.CdlEveningStar;
                                }

                                found = true;

                            }
                            else if ( ( RealBodyLength( i - 2 ) + RealBodyLength( i - 1 ) ) < RealBodyLength( i - 3 ) / 2 )
                            {
                                if ( outIdx - 3 >= 0 )
                                {
                                    buffer[ outIdx - 3 ] = TACandle.CdlEveningStar;
                                    buffer[ outIdx - 2 ] = TACandle.CdlEveningStar;
                                    buffer[ outIdx - 1 ] = TACandle.CdlEveningStar;
                                    buffer[ outIdx ] = TACandle.CdlEveningStar;
                                }
                                else
                                {
                                    buffer[ outIdx ] = TACandle.CdlEveningStar;
                                }

                                found = true;
                            }
                        }
                        else
                        {
                            if ( IsDojiCandle( i - 1 ) &&
                                 IsDojiCandle( i - 2 ) )
                            {
                                if ( outIdx - 3 >= 0 )
                                {
                                    buffer[ outIdx - 3 ] = TACandle.CdlEveningStar;
                                    buffer[ outIdx - 2 ] = TACandle.CdlDoji;
                                    buffer[ outIdx - 1 ] = TACandle.CdlDoji;
                                    buffer[ outIdx ] = TACandle.CdlEveningStar;
                                }
                                else
                                {
                                    buffer[ outIdx ] = TACandle.CdlEveningStar;
                                }

                                found = true;
                            }
                            else if ( ( RealBodyLength( i - 2 ) + RealBodyLength( i - 1 ) ) < RealBodyLength( i - 3 ) / 2 )
                            {
                                if ( outIdx - 3 >= 0 )
                                {
                                    buffer[ outIdx - 3 ] = TACandle.CdlEveningStar;
                                    buffer[ outIdx - 2 ] = TACandle.CdlEveningStar;
                                    buffer[ outIdx - 1 ] = TACandle.CdlEveningStar;
                                    buffer[ outIdx ] = TACandle.CdlEveningStar;
                                }
                                else
                                {
                                    buffer[ outIdx ] = TACandle.CdlEveningStar;
                                }

                                found = true;
                            }
                        }
                    }
                }
                else if ( ( IsWhiteCandle( i - 2 ) &&                 // 1st: long            
                            IsAboveAverageCandle( i - 2 ) &&
                            Bars[ i - 1 ].Low > Bars[ i - 2 ].Low &&
                            IsLongerThanShortCandle( i ) &&
                            IsShortCandle( i - 1 ) &&
                            IsBlackCandle( i )
                         ) ||                                  // 3rd: longer than short
                         ( IsWhiteCandle( i - 2 ) &&                 // 1st: long            
                            IsLongerThanShortCandle( i - 2 ) &&
                            Bars[ i - 1 ].Low > Bars[ i - 2 ].Low &&
                            IsShortCandle( i - 1 ) &&
                            IsAboveAverageCandle( i ) &&
                            IsBlackCandle( i )
                         )
                    )
                {
                    if ( Bars[ i ].Close < Bars[ i - 2 ].Close - RealBodyLength( i - 2 ) * 0.5 || ( IsLongCandle( i - 2 ) && IsAboveAverageCandle( i ) ) )                                         // closing well within 1st rb
                    {
                        double innerBBUpper  = IndicatorResult[ "InnerBBUpper" ][ i - 1 ];
                        double outerBBUpper  = IndicatorResult[ "OuterBBUpper" ][ i - 1 ];

                        if ( Bars[ i - 1 ].Close > innerBBUpper ||
                             Bars[ i - 1 ].High > outerBBUpper )
                        {
                            if ( IsShortCandle( i - 1 ) ) // 2nd: short
                            {
                                if ( IsDojiCandle( i - 1 ) )
                                {
                                    if ( outIdx - 2 >= 0 )
                                    {
                                        buffer[ outIdx - 2 ] = TACandle.CdlEveningStar;
                                        buffer[ outIdx - 1 ] = TACandle.CdlDoji;
                                        buffer[ outIdx ] = TACandle.CdlEveningStar;
                                    }
                                    else
                                    {
                                        buffer[ outIdx ] = TACandle.CdlEveningStar;
                                    }

                                    found = true;
                                }
                                else
                                {
                                    if ( outIdx - 2 >= 0 )
                                    {
                                        buffer[ outIdx - 2 ] = TACandle.CdlEveningStar;
                                        buffer[ outIdx - 1 ] = TACandle.CdlEveningStar;
                                        buffer[ outIdx ] = TACandle.CdlEveningStar;
                                    }
                                    else
                                    {
                                        buffer[ outIdx ] = TACandle.CdlEveningStar;
                                    }

                                    found = true;
                                }
                            }
                        }
                        else
                        {
                            if ( IsShortCandle( i - 1 ) ) // 2nd: short
                            {
                                if ( IsDojiCandle( i - 1 ) )
                                {
                                    if ( outIdx - 2 >= 0 )
                                    {
                                        buffer[ outIdx - 2 ] = TACandle.CdlEveningStar;
                                        buffer[ outIdx - 1 ] = TACandle.CdlDoji;
                                        buffer[ outIdx ] = TACandle.CdlEveningStar;
                                    }
                                    else
                                    {
                                        buffer[ outIdx ] = TACandle.CdlEveningStar;
                                    }

                                    found = true;
                                }
                                else
                                {
                                    if ( outIdx - 2 >= 0 )
                                    {
                                        buffer[ outIdx - 2 ] = TACandle.CdlEveningStar;
                                        buffer[ outIdx - 1 ] = TACandle.CdlEveningStar;
                                        buffer[ outIdx ] = TACandle.CdlEveningStar;
                                    }
                                    else
                                    {
                                        buffer[ outIdx ] = TACandle.CdlEveningStar;
                                    }

                                    found = true;
                                }
                            }
                        }
                    }
                }


                /* add the current range and subtract the first range: this is done after the pattern recognition 
				 * when avgPeriod is not 0, that means "compare with the previous candles" (it excludes the current candle)
				 */
                i++;
                outIdx++;

            } while ( i <= endIdx );

            outNBElement = outIdx;
            outBegIdx = startIdx;

            return found;
        }

        /// <summary>
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\tasuki_gap.png" />
        /// The first two candles of this formation continue a downtrend with the second red candle gapping, suggesting strong bearish sentiment. 
        /// A blue candle on day three indicates investors taking advantage of low prices to buying. But when the third days price action does not fill the gap created between 
        /// the first and second days candles, traders take this formation as a sign that the downward trend may likely still continue.
        /// 
        /// Although, the gap between the first and second candles simply suggests that downward price momentum continued after exchange hours, something to take advantage of in FX Trading. 
        /// An FX version of this pattern would have two large red candles suggesting strong downward momentum. 
        /// The pattern being finished by a third blue day that should not close above the open of the previous candle. 
        /// This FX version would be far less significant and quite common, requiring additional confirmation.        
        /// </summary>        
        //public RetCode DownsideTasukiGap3( int startIdx, int endIdx, out int outBegIdx, out int outNBElement, ref TACandle[] buffer )
        //{           
        //	int i, outIdx, lookbackCount;

        //	outNBElement = 0;
        //	outBegIdx = 0;

        //	if ( startIdx < 0 ) return RetCode.OutOfRangeStartIndex;

        //	if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return RetCode.OutOfRangeEndIndex;

        //	lookbackCount = fx.TALib.Core.CdlTasukiGapLookback( );

        //	if ( startIdx < lookbackCount ) startIdx = lookbackCount;

        //	if ( startIdx > endIdx )
        //	{
        //		return RetCode.Success;
        //	}
        // 
        //	i = startIdx;

        //	  /* Proceed with the calculation for the requested range.
        //	   * Must have:
        //	   * - upside (downside) gap
        //	   * - first candle after the window: white (black) candlestick
        //	   * - second candle: black (white) candlestick that opens within the previous real body and closes under (above)
        //	   *   the previous real body inside the gap
        //	   * - the size of two real bodies should be near the same
        //	   * The meaning of "near" is specified with TA_SetCandleSettings
        //	   * outInteger is positive (1 to 100) when bullish or negative (-1 to -100) when bearish;
        //	   * the user should consider that tasuki gap is significant when it appears in a trend, while this function does 
        //	   * not consider it
        //	   */
        //	outIdx = 0;
        //	do
        //	{
        //		if ( IsLongWhiteCandle( i - 2 ) && // 1st Long White
        //			 IsLongWhiteCandle( i - 1 ) && // 2nd Long white
        //			 IsBlackCandle( i ) && // 3rd Black
        //			 IndicatorBarsRepo[ i ] .Close> IndicatorBarsRepo[ i - 1 ] .Open&& //      and closes above the black rb
        //			 IndicatorBarsRepo[ i ] .Close< IndicatorBarsRepo[ i - 1 ] - RealBodyLength.Close( i - 1 ) * 0.5 )
        //		{
        //			DateTime? barTime = DatabarsRepo.GetTimeAtIndex( i );

        //                  if ( outIdx - 2 >= 0 )
        //                  {
        //                      buffer[ outIdx - 2 ] = TACandle.CdlUpsideTasukiGap;
        //                      buffer[ outIdx - 1 ] = TACandle.CdlUpsideTasukiGap;
        //                      buffer[ outIdx ] = TACandle.CdlUpsideTasukiGap;
        //                  }
        //                  else
        //                  {
        //                      buffer[ outIdx ] = TACandle.CdlUpsideTasukiGap;
        //                  }

        //			
        //		}
        //		else if (   IsLongBlackCandle( i - 2 ) &&                                            // downside gap
        //					IsLongBlackCandle( i - 1 ) &&                                                   // 1st: black
        //					IsWhiteCandle( i )  &&                                                        // 2nd: white
        //					IndicatorBarsRepo[ i ] .Close< IndicatorBarsRepo[ i - 1 ] .Open&&                                                 //      and closes above the black rb
        //					IndicatorBarsRepo[ i ] .Close> IndicatorBarsRepo[ i - 1 ] .Close+ RealBodyLength( i - 1 )* 0.5  
        //				)                     
        //		{
        //			DateTime? barTime = DatabarsRepo.GetTimeAtIndex( i );

        //                  if ( outIdx - 2 >= 0 )
        //                  {
        //                      buffer[ outIdx - 2 ] = TACandle.CdlDownsideTasukiGap;
        //                      buffer[ outIdx - 1 ] = TACandle.CdlDownsideTasukiGap;
        //                      buffer[ outIdx ] = TACandle.CdlDownsideTasukiGap;
        //                  }
        //                  else
        //                  {
        //                      buffer[ outIdx ] = TACandle.CdlDownsideTasukiGap;
        //                  }
        //		}

        //		i++;
        //		outIdx++;

        //	} while ( i <= endIdx );

        //	outNBElement = outIdx;
        //	outBegIdx    = startIdx;

        //	return RetCode.Success;
        //}

        /// <summary>
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\bullish_inverted_hammer.jpg" />
        /// Day-one is a red day, continuing an established trend and closing at the lower trading range near the days low
        /// The second day is red or blue day that also trades at a lower range with the opening and closing near each other.
        /// The upper wick of the second day should be at least twice as long as the body
        /// The lower wick of the second day should be non-existent or very little.
        /// 
        /// The Inverted Hammer appears in a market that opens at or near its low, creating a candle with a small real body. 
        /// During the day buyers rallied price fairly high, but were unable to sustain the rally.
        /// 
        /// In a market characterized by downtrend, bulls are able to rally price up briefly, but not enough to close above the days open. 
        /// This can be a warning for shorts to anticipate a further, more sustainable bullish rally. The reversal trend is confirmed by bullish moves the next day.
        /// 
        /// In day-three the higher the candle holds above day-twos body, the more likely the shorts will cover their positions, hence leading to the weakening of a bearish market. 
        /// Many bottom pickers will start longing the market once that occurs, leading to a bullish reversal. 
        /// Confirmation for the Inverted Hammer pattern is strongly suggested for this pattern.
        /// 
        /// The strong bullish Gravestone Doji1 pattern is similar to the Inverted Hammer pattern, except Gravestone Dojis second day is characterized by a clear doji where open 
        /// and close prices equal each other, rather than a small body.
        /// </summary>

        public int InvertedHammerLookBack()
        {
            return Core.CdlInvertedHammerLookback();
        }

        public RetCode InvertedHammer2( int startIdx, int endIdx, out int outBegIdx, out int outNBElement, ref TACandle[ ] buffer )
        {
            int i, outIdx, lookbackCount;

            outNBElement = 0;
            outBegIdx = 0;

            if ( startIdx < 0 ) return RetCode.OutOfRangeStartIndex;

            if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return RetCode.OutOfRangeEndIndex;

            lookbackCount = InvertedHammerLookBack();

            if ( startIdx < lookbackCount ) startIdx = lookbackCount;

            if ( startIdx > endIdx )
            {
                return RetCode.Success;
            }

            i = startIdx;

            outIdx = 0;
            do
            {
                if ( IsAboveAverageBlackCandle( i - 1 ) &&                                               // 1st Long White
                     IsShortCandle( i ) &&                                                       // 2nd Long white
                     UpperShadowLengthAsPip( i ) > 2 * RealBodyAsPip( i ) &&
                     IsLowerShadowShort( i )
                     )
                {
                    DateTime? barTime = Bars.GetTimeAtIndex( i );

                    if ( outIdx - 1 >= 0 )
                    {
                        buffer[ outIdx - 1 ] = TACandle.CdlInvertedHammer;
                        buffer[ outIdx ] = TACandle.CdlInvertedHammer;
                    }
                    else
                    {
                        buffer[ outIdx ] = TACandle.CdlInvertedHammer;
                    }


                }

                i++;
                outIdx++;

            } while ( i <= endIdx );

            outNBElement = outIdx;
            outBegIdx = startIdx;

            return RetCode.Success;
        }

        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\matching_low.png" />
        /// </summary>
        //public RetCode MatchingLow2( int startIdx, int endIdx, out int outBegIdx, out int outNBElement, ref TACandle[] buffer )
        //{
        //	double EqualPeriodTotal;
        //          int i, outIdx, EqualTrailingIdx, lookbackCount;

        //	outNBElement = 0;
        //	outBegIdx = 0;

        //	if ( startIdx < 0 ) return RetCode.OutOfRangeStartIndex;

        //	if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return RetCode.OutOfRangeEndIndex;

        //	lookbackCount = fx.TALib.Core.CdlMatchingLowLookback();

        //	if ( startIdx < lookbackCount ) startIdx = lookbackCount;

        //	if ( startIdx > endIdx )
        //	{
        //		return RetCode.Success;
        //	}

        //	EqualPeriodTotal = 0;
        //	EqualTrailingIdx = startIdx - CandleAvgPeriod( CandleSettingEnum.Equal );

        //	i = EqualTrailingIdx;
        //	while ( i < startIdx )
        //	{
        //		EqualPeriodTotal += CandleRange( CandleSettingEnum.Equal, i - 1 );
        //		i++;
        //	}

        //	i = startIdx;
        //	
        //	outIdx = 0;
        //	do
        //	{
        //		if ( IsAboveAverageBlackCandle( i - 1 ) &&                                               // 1st Long White
        //			 IsBlackCandle( i ) &&                                                       // 2nd Long white
        //			 IndicatorBarsRepo[ i ] .Close<= IndicatorBarsRepo[ i - 1 ] .Close+ CandleAverage( CandleSettingEnum.Equal, EqualPeriodTotal, i - 1 ) && // 1st and 2nd same close
        //			 IndicatorBarsRepo[ i ] .Close>= IndicatorBarsRepo[ i - 1 ] - CandleAverage.Close( CandleSettingEnum.Equal, EqualPeriodTotal, i - 1 )
        //		   )
        //		{
        //			DateTime? barTime = DatabarsRepo.GetTimeAtIndex( i );

        //                  if ( outIdx - 1 >= 0 )
        //                  {
        //                      buffer[ outIdx - 1 ] = TACandle.CdlMatchingLow;
        //                      buffer[ outIdx ] = TACandle.CdlMatchingLow;
        //                  }
        //                  else
        //                  {
        //                      buffer[ outIdx ] = TACandle.CdlMatchingLow;
        //                  }
        //		}

        //		EqualPeriodTotal += CandleRange( CandleSettingEnum.Equal, i - 1 ) - CandleRange( CandleSettingEnum.Equal, EqualTrailingIdx - 1 );
        //		i++;
        //		EqualTrailingIdx++;
        //		outIdx++;

        //	} while ( i <= endIdx );

        //	outNBElement = outIdx;
        //	outBegIdx = startIdx;

        //	return RetCode.Success;
        //}

        /// <summary>
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\bullish_breakaway.jpg" />
        /// The first day is a long red day continuing an established down trend.
        /// The second, third and forth days all continue in the same direction with lower closes, but more weakly than the first bearish push.
        /// The fifth day is a long blue day that closes into the body of the first or second days.
        /// 
        /// In fact the first three to four days matches the bullish Three Stars in the South formation. 
        /// The Three Stars in the South Pattern is not normally a strong reversal pattern and traders usually wait for additional confirmation the next day.
        /// 
        /// The last candle of the Bullish Breakaway offers exactly that confirmation. After the few days of deteriorating bearish trend, 
        /// a clear bullish candle emerges to reorient the market’s direction. Markets typically want to see the bullish candle that moves at least above the previous two or three day’s body 
        /// for ideal confirmation.
        /// 
        /// Ideally we’d see four red candles before the large bullish move, but realistically there could be two, three or even five candles that communicate the same information of 
        /// weakening bearish trend. Thus traders look for four candles when identifying this formation, but similar formations will be accepted.
        /// </summary>

        public int BreakAwayLookBack()
        {
            return Core.CdlBreakawayLookback();
        }

        public RetCode BreakAway4567( int startIdx, int endIdx, out int outBegIdx, out int outNBElement, ref TACandle[ ] buffer )
        {
            int i, outIdx, lookbackCount;

            outNBElement = 0;
            outBegIdx = 0;

            if ( startIdx < 0 ) return RetCode.OutOfRangeStartIndex;

            if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return RetCode.OutOfRangeEndIndex;

            lookbackCount = Core.CdlInvertedHammerLookback();

            if ( startIdx < lookbackCount ) startIdx = lookbackCount;

            if ( startIdx > endIdx )
            {
                return RetCode.Success;
            }

            i = startIdx;

            outIdx = 0;

            /* Proceed with the calculation for the requested range.
			* Must have:
			* - first candle: long black (white)
			* - second candle: black (white) day whose body gaps down (up)
			* - third candle: black or white day with lower (higher) high and lower (higher) low than prior candle's
			* - fourth candle: black (white) day with lower (higher) high and lower (higher) low than prior candle's
			* - fifth candle: white (black) day that closes inside the gap, erasing the prior 3 days
			* The meaning of "long" is specified with TA_SetCandleSettings
			* outInteger is positive (1 to 100) when bullish or negative (-1 to -100) when bearish;
			* the user should consider that breakaway is significant in a trend opposite to the last candle, while this 
			* function does not consider it
			*/
            do
            {
                if ( IsLongCandle( i - 6 ) && // 1st Long candle
                     CandleColor( i - 6 ) == CandleColor( i - 5 ) &&
                     CandleColor( i - 5 ) == CandleColor( i - 1 ) &&
                     CandleColor( i - 1 ) == -CandleColor( i ) &&
                     IsShortCandle( i - 2 ) &&                                                          // Make sure that the last two candles before Reversal is short.
                     IsShortCandle( i - 1 ) &&
                     IsLongCandle( i ) )
                {
                    DateTime? barTime = Bars.GetTimeAtIndex( i );

                    if ( ( Bars[ i - 1 ].Close > Bars[ i - 2 ].Close && Bars[ i - 1 ].Open > Bars[ i - 2 ].Open &&
                            Bars[ i - 2 ].Close > Bars[ i - 3 ].Close && Bars[ i - 2 ].Open > Bars[ i - 3 ].Open &&
                            Bars[ i - 3 ].Close > Bars[ i - 4 ].Close && Bars[ i - 3 ].Open > Bars[ i - 4 ].Open &&
                            Bars[ i - 4 ].Close > Bars[ i - 5 ].Close && Bars[ i - 4 ].Open > Bars[ i - 5 ].Open &&
                            Bars[ i - 5 ].Close > Bars[ i - 6 ].Close && Bars[ i - 5 ].Open > Bars[ i - 6 ].Open
                       ) ||
                        ( Bars[ i - 1 ].High > Bars[ i - 2 ].High && Bars[ i - 1 ].Low > Bars[ i - 2 ].Low &&
                            Bars[ i - 2 ].High > Bars[ i - 3 ].High && Bars[ i - 2 ].Low > Bars[ i - 3 ].Low &&
                            Bars[ i - 3 ].High > Bars[ i - 4 ].High && Bars[ i - 3 ].Low > Bars[ i - 4 ].Low &&
                            Bars[ i - 4 ].High > Bars[ i - 5 ].High && Bars[ i - 4 ].Low > Bars[ i - 5 ].Low &&
                            Bars[ i - 5 ].High > Bars[ i - 6 ].High && Bars[ i - 5 ].Low > Bars[ i - 6 ].Low
                        ) )
                    {
                        if ( Bars[ i ].Close < Bars[ i - 2 ].Open )
                        {
                            if ( outIdx - 6 >= 0 )
                            {
                                buffer[ outIdx - 6 ] = TACandle.CdlBreakAwayBull;
                                buffer[ outIdx - 5 ] = TACandle.CdlBreakAwayBull;
                                buffer[ outIdx - 4 ] = TACandle.CdlBreakAwayBull;
                                buffer[ outIdx - 3 ] = TACandle.CdlBreakAwayBull;
                                buffer[ outIdx - 2 ] = TACandle.CdlBreakAwayBull;
                                buffer[ outIdx - 1 ] = TACandle.CdlBreakAwayBull;
                                buffer[ outIdx ] = TACandle.CdlBreakAwayBull;
                            }
                            else
                            {
                                buffer[ outIdx ] = TACandle.CdlBreakAwayBull;
                            }
                        }
                    }
                    else if ( ( Bars[ i - 1 ].Close < Bars[ i - 2 ].Close && Bars[ i - 1 ].Open < Bars[ i - 2 ].Open &&
                                 Bars[ i - 2 ].Close < Bars[ i - 3 ].Close && Bars[ i - 2 ].Open < Bars[ i - 3 ].Open &&
                                 Bars[ i - 3 ].Close < Bars[ i - 4 ].Close && Bars[ i - 3 ].Open < Bars[ i - 4 ].Open &&
                                 Bars[ i - 4 ].Close < Bars[ i - 5 ].Close && Bars[ i - 4 ].Open < Bars[ i - 5 ].Open &&
                                 Bars[ i - 5 ].Close < Bars[ i - 6 ].Close && Bars[ i - 5 ].Open < Bars[ i - 6 ].Open
                       ) ||
                        ( Bars[ i - 1 ].High < Bars[ i - 2 ].High && Bars[ i - 1 ].Low < Bars[ i - 2 ].Low &&
                            Bars[ i - 2 ].High < Bars[ i - 3 ].High && Bars[ i - 2 ].Low < Bars[ i - 3 ].Low &&
                            Bars[ i - 3 ].High < Bars[ i - 4 ].High && Bars[ i - 3 ].Low < Bars[ i - 4 ].Low &&
                            Bars[ i - 4 ].High < Bars[ i - 5 ].High && Bars[ i - 4 ].Low < Bars[ i - 5 ].Low &&
                            Bars[ i - 5 ].High < Bars[ i - 6 ].High && Bars[ i - 5 ].Low < Bars[ i - 6 ].Low
                        ) )
                    {
                        if ( Bars[ i ].Close > Bars[ i - 2 ].Open )
                        {
                            if ( outIdx - 6 >= 0 )
                            {
                                buffer[ outIdx - 6 ] = TACandle.CdlBreakAwayBear;
                                buffer[ outIdx - 5 ] = TACandle.CdlBreakAwayBear;
                                buffer[ outIdx - 4 ] = TACandle.CdlBreakAwayBear;
                                buffer[ outIdx - 3 ] = TACandle.CdlBreakAwayBear;
                                buffer[ outIdx - 2 ] = TACandle.CdlBreakAwayBear;
                                buffer[ outIdx - 1 ] = TACandle.CdlBreakAwayBear;
                                buffer[ outIdx ] = TACandle.CdlBreakAwayBear;
                            }
                            else
                            {
                                buffer[ outIdx ] = TACandle.CdlBreakAwayBear;
                            }


                        }
                    }
                }
                else if ( IsLongCandle( i - 5 ) &&                                               // 1st Long candle
                            CandleColor( i - 5 ) == CandleColor( i - 4 ) &&                        // 1st, 2nd, 5th same color, 6th opposite
                            CandleColor( i - 4 ) == CandleColor( i - 1 ) &&
                            CandleColor( i - 1 ) == -CandleColor( i ) &&
                            IsShortCandle( i - 2 ) &&                                                          // Make sure that the last two candles before Reversal is short.
                            IsShortCandle( i - 1 ) &&
                            IsLongCandle( i )
                   )
                {
                    DateTime? barTime = Bars.GetTimeAtIndex( i );

                    if ( ( Bars[ i - 1 ].Close > Bars[ i - 2 ].Close && Bars[ i - 1 ].Open > Bars[ i - 2 ].Open &&
                            Bars[ i - 2 ].Close > Bars[ i - 3 ].Close && Bars[ i - 2 ].Open > Bars[ i - 3 ].Open &&
                            Bars[ i - 3 ].Close > Bars[ i - 4 ].Close && Bars[ i - 3 ].Open > Bars[ i - 4 ].Open &&
                            Bars[ i - 4 ].Close > Bars[ i - 5 ].Close && Bars[ i - 4 ].Open > Bars[ i - 5 ].Open
                       ) ||
                        ( Bars[ i - 1 ].High > Bars[ i - 2 ].High && Bars[ i - 1 ].Low > Bars[ i - 2 ].Low &&
                            Bars[ i - 2 ].High > Bars[ i - 3 ].High && Bars[ i - 2 ].Low > Bars[ i - 3 ].Low &&
                            Bars[ i - 3 ].High > Bars[ i - 4 ].High && Bars[ i - 3 ].Low > Bars[ i - 4 ].Low &&
                            Bars[ i - 4 ].High > Bars[ i - 5 ].High && Bars[ i - 4 ].Low > Bars[ i - 5 ].Low
                        ) )
                    {
                        if ( Bars[ i ].Close < Bars[ i - 2 ].Open )
                        {
                            if ( outIdx - 5 >= 0 )
                            {
                                buffer[ outIdx - 5 ] = TACandle.CdlBreakAwayBull;
                                buffer[ outIdx - 4 ] = TACandle.CdlBreakAwayBull;
                                buffer[ outIdx - 3 ] = TACandle.CdlBreakAwayBull;
                                buffer[ outIdx - 2 ] = TACandle.CdlBreakAwayBull;
                                buffer[ outIdx - 1 ] = TACandle.CdlBreakAwayBull;
                                buffer[ outIdx ] = TACandle.CdlBreakAwayBull;
                            }
                            else
                            {
                                buffer[ outIdx ] = TACandle.CdlBreakAwayBull;
                            }
                        }
                    }
                    else if ( ( Bars[ i - 1 ].Close < Bars[ i - 2 ].Close && Bars[ i - 1 ].Open < Bars[ i - 2 ].Open &&
                                 Bars[ i - 2 ].Close < Bars[ i - 3 ].Close && Bars[ i - 2 ].Open < Bars[ i - 3 ].Open &&
                                 Bars[ i - 3 ].Close < Bars[ i - 4 ].Close && Bars[ i - 3 ].Open < Bars[ i - 4 ].Open &&
                                 Bars[ i - 4 ].Close < Bars[ i - 5 ].Close && Bars[ i - 4 ].Open < Bars[ i - 5 ].Open
                       ) ||
                        ( Bars[ i - 1 ].High < Bars[ i - 2 ].High && Bars[ i - 1 ].Low < Bars[ i - 2 ].Low &&
                            Bars[ i - 2 ].High < Bars[ i - 3 ].High && Bars[ i - 2 ].Low < Bars[ i - 3 ].Low &&
                            Bars[ i - 3 ].High < Bars[ i - 4 ].High && Bars[ i - 3 ].Low < Bars[ i - 4 ].Low &&
                            Bars[ i - 4 ].High < Bars[ i - 5 ].High && Bars[ i - 4 ].Low < Bars[ i - 5 ].Low
                        ) )
                    {
                        if ( Bars[ i ].Close > Bars[ i - 2 ].Open )
                        {
                            if ( outIdx - 5 >= 0 )
                            {
                                buffer[ outIdx - 5 ] = TACandle.CdlBreakAwayBear;
                                buffer[ outIdx - 4 ] = TACandle.CdlBreakAwayBear;
                                buffer[ outIdx - 3 ] = TACandle.CdlBreakAwayBear;
                                buffer[ outIdx - 2 ] = TACandle.CdlBreakAwayBear;
                                buffer[ outIdx - 1 ] = TACandle.CdlBreakAwayBear;
                                buffer[ outIdx ] = TACandle.CdlBreakAwayBear;
                            }
                            else
                            {
                                buffer[ outIdx ] = TACandle.CdlBreakAwayBear;
                            }
                        }
                    }
                }
                else if ( IsLongCandle( i - 4 ) &&                                               // 1st Long candle
                            CandleColor( i - 4 ) == CandleColor( i - 3 ) &&                        // 1st, 2nd, 4th same color, 5th opposite
                            CandleColor( i - 3 ) == CandleColor( i - 1 ) &&
                            CandleColor( i - 1 ) == -CandleColor( i ) &&
                            IsShortCandle( i - 2 ) &&                                                          // Make sure that the last two candles before Reversal is short.
                            IsShortCandle( i - 1 ) &&
                            IsLongCandle( i )
                   )
                {
                    DateTime? barTime = Bars.GetTimeAtIndex( i );

                    if ( ( Bars[ i - 1 ].Close > Bars[ i - 2 ].Close && Bars[ i - 1 ].Open > Bars[ i - 2 ].Open &&
                            Bars[ i - 2 ].Close > Bars[ i - 3 ].Close && Bars[ i - 2 ].Open > Bars[ i - 3 ].Open &&
                            Bars[ i - 3 ].Close > Bars[ i - 4 ].Close && Bars[ i - 3 ].Open > Bars[ i - 4 ].Open
                       ) ||
                        ( Bars[ i - 1 ].High > Bars[ i - 2 ].High && Bars[ i - 1 ].Low > Bars[ i - 2 ].Low &&
                            Bars[ i - 2 ].High > Bars[ i - 3 ].High && Bars[ i - 2 ].Low > Bars[ i - 3 ].Low &&
                            Bars[ i - 3 ].High > Bars[ i - 4 ].High && Bars[ i - 3 ].Low > Bars[ i - 4 ].Low
                        ) )
                    {
                        if ( Bars[ i ].Close < Bars[ i - 2 ].Open )
                        {
                            if ( outIdx - 4 >= 0 )
                            {
                                buffer[ outIdx - 4 ] = TACandle.CdlBreakAwayBull;
                                buffer[ outIdx - 3 ] = TACandle.CdlBreakAwayBull;
                                buffer[ outIdx - 2 ] = TACandle.CdlBreakAwayBull;
                                buffer[ outIdx - 1 ] = TACandle.CdlBreakAwayBull;
                                buffer[ outIdx ] = TACandle.CdlBreakAwayBull;
                            }
                            else
                            {
                                buffer[ outIdx ] = TACandle.CdlBreakAwayBull;
                            }

                        }
                    }
                    else if ( ( Bars[ i - 1 ].Close < Bars[ i - 2 ].Close && Bars[ i - 1 ].Open < Bars[ i - 2 ].Open &&
                                 Bars[ i - 2 ].Close < Bars[ i - 3 ].Close && Bars[ i - 2 ].Open < Bars[ i - 3 ].Open &&
                                 Bars[ i - 3 ].Close < Bars[ i - 4 ].Close && Bars[ i - 3 ].Open < Bars[ i - 4 ].Open
                       ) ||
                        ( Bars[ i - 1 ].High < Bars[ i - 2 ].High && Bars[ i - 1 ].Low < Bars[ i - 2 ].Low &&
                            Bars[ i - 2 ].High < Bars[ i - 3 ].High && Bars[ i - 2 ].Low < Bars[ i - 3 ].Low &&
                            Bars[ i - 3 ].High < Bars[ i - 4 ].High && Bars[ i - 3 ].Low < Bars[ i - 4 ].Low
                        ) )
                    {
                        if ( Bars[ i ].Close > Bars[ i - 2 ].Open )
                        {
                            if ( outIdx - 4 >= 0 )
                            {
                                buffer[ outIdx - 4 ] = TACandle.CdlBreakAwayBear;
                                buffer[ outIdx - 3 ] = TACandle.CdlBreakAwayBear;
                                buffer[ outIdx - 2 ] = TACandle.CdlBreakAwayBear;
                                buffer[ outIdx - 1 ] = TACandle.CdlBreakAwayBear;
                                buffer[ outIdx ] = TACandle.CdlBreakAwayBear;
                            }
                            else
                            {
                                buffer[ outIdx ] = TACandle.CdlBreakAwayBear;
                            }
                        }
                    }
                }
                if ( IsLongCandle( i - 3 ) &&                                               // 1st Long candle
                     CandleColor( i - 3 ) == CandleColor( i - 2 ) &&                        // 1st, 2nd, 4th same color, 5th opposite
                     CandleColor( i - 2 ) == CandleColor( i - 1 ) &&
                     CandleColor( i - 1 ) == -CandleColor( i ) &&
                     IsLongCandle( i )
                   )
                {
                    DateTime? barTime = Bars.GetTimeAtIndex( i );

                    if ( ( Bars[ i - 1 ].Close > Bars[ i - 2 ].Close && Bars[ i - 1 ].Open > Bars[ i - 2 ].Open &&
                            Bars[ i - 2 ].Close > Bars[ i - 3 ].Close && Bars[ i - 2 ].Open > Bars[ i - 3 ].Open
                       ) ||
                        ( Bars[ i - 1 ].High > Bars[ i - 2 ].High && Bars[ i - 1 ].Low > Bars[ i - 2 ].Low &&
                            Bars[ i - 2 ].High > Bars[ i - 3 ].High && Bars[ i - 2 ].Low > Bars[ i - 3 ].Low
                        ) )
                    {
                        if ( Bars[ i ].Close < Bars[ i - 2 ].Open )
                        {
                            if ( outIdx - 3 >= 0 )
                            {
                                buffer[ outIdx - 3 ] = TACandle.CdlBreakAwayBull;
                                buffer[ outIdx - 2 ] = TACandle.CdlBreakAwayBull;
                                buffer[ outIdx - 1 ] = TACandle.CdlBreakAwayBull;
                                buffer[ outIdx ] = TACandle.CdlBreakAwayBull;
                            }
                            else
                            {
                                buffer[ outIdx ] = TACandle.CdlBreakAwayBull;
                            }

                        }
                    }
                    else if ( ( Bars[ i - 1 ].Close < Bars[ i - 2 ].Close && Bars[ i - 1 ].Open < Bars[ i - 2 ].Open &&
                                 Bars[ i - 2 ].Close < Bars[ i - 3 ].Close && Bars[ i - 2 ].Open < Bars[ i - 3 ].Open
                       ) ||
                        ( Bars[ i - 1 ].High < Bars[ i - 2 ].High && Bars[ i - 1 ].Low < Bars[ i - 2 ].Low &&
                            Bars[ i - 2 ].High < Bars[ i - 3 ].High && Bars[ i - 2 ].Low < Bars[ i - 3 ].Low
                        ) )
                    {
                        if ( Bars[ i ].Close > Bars[ i - 2 ].Open )
                        {
                            if ( outIdx - 3 >= 0 )
                            {
                                buffer[ outIdx - 3 ] = TACandle.CdlBreakAwayBear;
                                buffer[ outIdx - 2 ] = TACandle.CdlBreakAwayBear;
                                buffer[ outIdx - 1 ] = TACandle.CdlBreakAwayBear;
                                buffer[ outIdx ] = TACandle.CdlBreakAwayBear;
                            }
                            else
                            {
                                buffer[ outIdx ] = TACandle.CdlBreakAwayBear;
                            }
                        }
                    }
                }

                i++;
                outIdx++;

            } while ( i <= endIdx );

            outNBElement = outIdx;
            outBegIdx = startIdx;

            return RetCode.Success;
        }


        /// <summary>
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\bullish_piercing_line.jpg" />
        /// After an established trend, day-one is a long red day
        /// The second day is a blue candle that closes above the midpoint of the first days body.
        /// 
        /// The market continues the downtrend on the first day. By day-two buyers take price up to close near the open of the previous day.
        /// In FX, traders view the lower the second day low the better, since the bigger the sell-off after the open the more buyers were able to drive price back up.
        /// 
        /// This formation suggests bulls have begun to take charge of the market, and shorts have been shaken by the sudden lost of bearish momentum. 
        /// Rallying days are common after this formation as more buyers confidently to enter the market with a clear stop benchmark at the second day low.
        /// 
        /// The higher day-two closes into the first day candlestick body, the higher the chance of the downtrend bottoming out. 
        /// If the second day candle does not trade above the midpoint of the first day body, traders typically feel it safer to wait for confirmation on the third day.
        /// 
        /// Some traders wait for confirmation regardless of how deep the bullish Piercing Line penetrates the second day.
        /// 
        /// In non-FX markets, traders want to see the second day gap down, opening below the close of the previous day. Because the Forex Market offers continues 24 hour markets, 
        /// such gaps are not typically possible. But FX traders will turn to the low of the second day to indicate how strong the opening sell-off is, 
        /// to gauge the strength of the subsequent bull move.
        /// </summary>

        public int PiercingOrDarkCloudCoverLookBack()
        {
            return ( Math.Max( Core.CdlPiercingLookback(), Core.BbandsLookback( Core.MAType.Ema, 20 ) ) );
        }

        public RetCode PiercingOrDarkCloudCover2( int startIdx, int endIdx, out int outBegIdx, out int outNBElement, ref TACandle[ ] buffer )
        {
            int i, outIdx, lockbackcount;

            outNBElement = 0;
            outBegIdx = 0;

            if ( startIdx < 0 ) return RetCode.OutOfRangeStartIndex;

            if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return RetCode.OutOfRangeEndIndex;

            lockbackcount = PiercingOrDarkCloudCoverLookBack();

            if ( startIdx < lockbackcount ) startIdx = lockbackcount;

            if ( startIdx > endIdx )
            {
                return RetCode.Success;
            }

            i = startIdx;

            outIdx = 0;
            do
            {
                /* Proceed with the calculation for the requested range.
                 * Must have:
                 * - first candle:       long black candle
                 * - second candle:      long white candle with open below previous day low and close at least at 50% of previous day 
                 *                       real body
                 *
                 * the user should consider that a piercing pattern is significant when it appears in a downtrend, while 
                 * this function does not consider it
                 */
                if ( CandleColor( i - 1 ) == -CandleColor( i ) &&                                               // 1st Long White
                     IsAboveAverageCandle( i - 1 )
                   )
                {
                    DateTime? barTime = Bars.GetTimeAtIndex( i );

                    if ( Bars[ i ].Close < Bars[ i - 1 ].Open && Bars[ i ].Close > Bars[ i - 1 ].Close + RealBodyLength( i - 1 ) * 0.5 )
                    {
                        double innerBBLower  = IndicatorResult[ "InnerBBLower" ][ i - 1 ];
                        double outerBBLower  = IndicatorResult[ "OuterBBLower" ][ i - 1 ];

                        if ( Bars[ i - 1 ].Close <= outerBBLower )
                        {
                            if ( outIdx - 1 >= 0 )
                            {
                                buffer[ outIdx - 1 ] = TACandle.CdlPiercing;
                                buffer[ outIdx ] = TACandle.CdlPiercing;
                            }
                            else
                            {
                                buffer[ outIdx ] = TACandle.CdlPiercing;
                            }


                        }
                        else if ( ( Bars[ i - 1 ].Low <= outerBBLower ) &&
                                  ( Bars[ i - 1 ].High >= innerBBLower ) )
                        {
                            if ( outIdx - 1 >= 0 )
                            {
                                buffer[ outIdx - 1 ] = TACandle.CdlPiercing;
                                buffer[ outIdx ] = TACandle.CdlPiercing;
                            }
                            else
                            {
                                buffer[ outIdx ] = TACandle.CdlPiercing;
                            }
                        }
                        else if ( ( Bars[ i - 1 ].Close - outerBBLower ) < ( innerBBLower - Bars[ i - 1 ].Close ) )
                        {
                            if ( outIdx - 1 >= 0 )
                            {
                                buffer[ outIdx - 1 ] = TACandle.CdlPiercing;
                                buffer[ outIdx ] = TACandle.CdlPiercing;
                            }
                            else
                            {
                                buffer[ outIdx ] = TACandle.CdlPiercing;
                            }
                        }
                    }
                    else if ( Bars[ i ].Close > Bars[ i - 1 ].Open && Bars[ i ].Close < Bars[ i - 1 ].Close - RealBodyLength( i - 1 ) * 0.5 )
                    {
                        double innerBBUpper  = IndicatorResult[ "InnerBBUpper" ][ i - 1 ];
                        double outerBBUpper  = IndicatorResult[ "OuterBBUpper" ][ i - 1 ];

                        if ( Bars[ i - 1 ].Close >= outerBBUpper )
                        {
                            if ( outIdx - 1 >= 0 )
                            {
                                buffer[ outIdx - 1 ] = TACandle.CdlDarkCloudCover;
                                buffer[ outIdx ] = TACandle.CdlDarkCloudCover;
                            }
                            else
                            {
                                buffer[ outIdx ] = TACandle.CdlDarkCloudCover;
                            }
                        }
                        else if ( ( Bars[ i - 1 ].High >= outerBBUpper ) &&
                                  ( Bars[ i - 1 ].Low <= innerBBUpper ) )
                        {
                            if ( outIdx - 1 >= 0 )
                            {
                                buffer[ outIdx - 1 ] = TACandle.CdlDarkCloudCover;
                                buffer[ outIdx ] = TACandle.CdlDarkCloudCover;
                            }
                            else
                            {
                                buffer[ outIdx ] = TACandle.CdlDarkCloudCover;
                            }
                        }
                        else if ( ( outerBBUpper - Bars[ i - 1 ].Close ) < ( Bars[ i - 1 ].Close - innerBBUpper ) )
                        {
                            if ( outIdx - 1 >= 0 )
                            {
                                buffer[ outIdx - 1 ] = TACandle.CdlDarkCloudCover;
                                buffer[ outIdx ] = TACandle.CdlDarkCloudCover;
                            }
                            else
                            {
                                buffer[ outIdx ] = TACandle.CdlDarkCloudCover;
                            }
                        }

                    }
                }

                i++;
                outIdx++;

            } while ( i <= endIdx );

            outNBElement = outIdx;
            outBegIdx = startIdx;

            return RetCode.Success;
        }

        /// <summary>
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\bullish_stick_sandwich.jpg" />
        /// 
        /// Day-one is a red day that continues the established trend to a new low
        /// Day-two is a blue day that trades up to or above day-one
        /// Day-three is a red day that closes near the close of the first day
        /// 
        /// All three candles establish a firm support line, while forming consecutive higher highs.
        /// 
        /// The Bullish Stick Sandwich pattern shows three days that establish a solid support level, while each new day forms higher highs. 
        /// This suggests the bearish trend has bottomed out while each new high implies buyers are able to mount more control of the market. 
        /// Candlestick Analysts who are short would look to cover their short positions. While those looking to long will for buying opportunities, 
        /// and use the shared bottom as a solid support.
        /// </summary>
        //public RetCode StickSandwichBull35( int startIdx, int endIdx, out int outBegIdx, out int outNBElement, ref TACandle[] buffer )
        //{
        //	int i, outIdx, lockbackcount;

        //	outNBElement = 0;
        //	outBegIdx = 0;

        //	if ( startIdx < 0 ) return RetCode.OutOfRangeStartIndex;

        //	if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return RetCode.OutOfRangeEndIndex;

        //	lockbackcount = Math.Max( fx.TALib.Core.CdlStickSandwhichLookback(), fx.TALib.Core.BbandsLookback( Core.MAType.Ema, 20 ) );

        //	if ( startIdx < lockbackcount ) startIdx = lockbackcount;

        //	if ( startIdx > endIdx )
        //	{
        //		return RetCode.Success;
        //	}

        //	double EqualPeriodTotal = 0;
        //	int EqualTrailingIdx = startIdx - CandleAvgPeriod( CandleSettingEnum.Equal );

        //	i = EqualTrailingIdx;
        //	while ( i < startIdx )
        //	{
        //		EqualPeriodTotal += CandleRange( CandleSettingEnum.Equal, i - 1 );
        //		i++;
        //	}

        //	i = startIdx;

        //	outIdx = 0;
        //	do
        //	{
        //	   /* Proceed with the calculation for the requested range.
        //		* Must have:
        //		* - first candle:       black candle
        //		* - second candle:      white candle that trades only above the prior close (low > prior close)
        //		* - third candle:       black candle with the close equal to the first candle's close                
        //		*/

        //		if (
        //			 IsBlackCandle( i - 4 ) && // first black
        //			 IsWhiteCandle( i - 3 ) &&
        //			 IsBlackCandle( i - 2 ) && // first black
        //			 IsWhiteCandle( i - 1 ) &&
        //			 IsBlackCandle( i ) &&
        //			 !IsDojiCandle( i - 4 ) &&
        //			 !IsDojiCandle( i - 3 ) &&
        //			 !IsDojiCandle( i - 2 ) &&
        //			 !IsDojiCandle( i - 1 ) &&
        //			 !IsDojiCandle( i )
        //			)
        //		{
        //			DateTime? barTime     = DatabarsRepo.GetTimeAtIndex( i );
        //			double innerBBLoweri  = IndicatorResult[ "InnerBBLower" ][ i ];
        //			double innerBBLower2  = IndicatorResult[ "InnerBBLower" ][ i - 2 ];
        //			double innerBBLower4  = IndicatorResult[ "InnerBBLower" ][ i - 4 ];

        //			if ( IndicatorBarsRepo[ i ] .Close<= IndicatorBarsRepo[ i - 2 ] .Close+ CandleAverage( CandleSettingEnum.Equal, EqualPeriodTotal, i - 2 ) &&
        //				 IndicatorBarsRepo[ i ] .Close>= IndicatorBarsRepo[ i - 2 ] - CandleAverage.Close( CandleSettingEnum.Equal, EqualPeriodTotal, i - 2 ) &&
        //				 IndicatorBarsRepo[ i - 2 ] .Close<= IndicatorBarsRepo[ i - 4 ] .Close+ CandleAverage( CandleSettingEnum.Equal, EqualPeriodTotal, i - 4 ) &&
        //				 IndicatorBarsRepo[ i - 2 ] .Close>= IndicatorBarsRepo[ i - 4 ] - CandleAverage.Close( CandleSettingEnum.Equal, EqualPeriodTotal, i - 4 ) )
        //			{
        //                      if ( outIdx - 4 >= 0 )
        //                      {
        //                          buffer[ outIdx - 4 ] = TACandle.CdlStickSandwichBull;
        //                          buffer[ outIdx - 3 ] = TACandle.CdlStickSandwichBull;
        //                          buffer[ outIdx - 2 ] = TACandle.CdlStickSandwichBull;
        //                          buffer[ outIdx - 1 ] = TACandle.CdlStickSandwichBull;
        //                          buffer[ outIdx ]     = TACandle.CdlStickSandwichBull;
        //                      }
        //                      else
        //                      {
        //                          buffer[ outIdx ] = TACandle.CdlStickSandwichBull;
        //                      }
        //				
        //			}
        //		}
        //		else if ( IsWhiteCandle( i - 4 ) && // first black
        //				  IsBlackCandle( i - 3 ) &&
        //				  IsWhiteCandle( i - 2 ) && // first black
        //				  IsBlackCandle( i - 1 ) &&
        //				  IsWhiteCandle( i ) &&
        //				  !IsDojiCandle( i - 4 ) &&
        //				  !IsDojiCandle( i - 3 ) &&
        //				  !IsDojiCandle( i - 2 ) &&
        //				  !IsDojiCandle( i - 1 ) &&
        //				  !IsDojiCandle( i ) )
        //		{
        //			DateTime? barTime     = DatabarsRepo.GetTimeAtIndex( i );
        //			double innerBBUpperi  = IndicatorResult[ "InnerBBUpper" ][ i ];
        //			double innerBBUpper2  = IndicatorResult[ "InnerBBUpper" ][ i - 2 ];
        //			double innerBBUpper4  = IndicatorResult[ "InnerBBUpper" ][ i - 4 ];

        //			if ( IndicatorBarsRepo[ i ] .Close<= IndicatorBarsRepo[ i - 2 ] .Close+ CandleAverage( CandleSettingEnum.Equal, EqualPeriodTotal, i - 2 ) &&
        //				 IndicatorBarsRepo[ i ] .Close>= IndicatorBarsRepo[ i - 2 ] - CandleAverage.Close( CandleSettingEnum.Equal, EqualPeriodTotal, i - 2 ) &&
        //				 IndicatorBarsRepo[ i - 2 ] .Close<= IndicatorBarsRepo[ i - 4 ] .Close+ CandleAverage( CandleSettingEnum.Equal, EqualPeriodTotal, i - 4 ) &&
        //				 IndicatorBarsRepo[ i - 2 ] .Close>= IndicatorBarsRepo[ i - 4 ] - CandleAverage.Close( CandleSettingEnum.Equal, EqualPeriodTotal, i - 4 ) )
        //			{
        //                      if ( outIdx - 4 >= 0 )
        //                      {
        //                          buffer[ outIdx - 4 ] = TACandle.CdlStickSandwichBear;
        //                          buffer[ outIdx - 3 ] = TACandle.CdlStickSandwichBear;
        //                          buffer[ outIdx - 2 ] = TACandle.CdlStickSandwichBear;
        //                          buffer[ outIdx - 1 ] = TACandle.CdlStickSandwichBear;
        //                          buffer[ outIdx ]     = TACandle.CdlStickSandwichBear;
        //                      }
        //                      else
        //                      {
        //                          buffer[ outIdx ] = TACandle.CdlStickSandwichBear;
        //                      }
        //			}
        //		}
        //		else if ( IsBlackCandle( i - 2 ) &&                                              // first black
        //				  IsWhiteCandle( i - 1 ) &&
        //				  IsBlackCandle( i )     &&
        //				  !IsDojiCandle( i - 2 ) &&
        //				  !IsDojiCandle( i - 1 ) &&
        //				  !IsDojiCandle( i ) 
        //		   )
        //		{
        //			DateTime? barTime     = DatabarsRepo.GetTimeAtIndex( i );
        //			double innerBBLoweri  = IndicatorResult[ "InnerBBLower" ][ i ];                    
        //			double innerBBLower2  = IndicatorResult[ "InnerBBLower" ][ i - 2 ];
        //			

        //			
        //			if ( IndicatorBarsRepo[ i ] .Close<= IndicatorBarsRepo[ i - 2 ] .Close+ CandleAverage( CandleSettingEnum.Equal, EqualPeriodTotal, i - 2 ) &&
        //					IndicatorBarsRepo[ i ] .Close>= IndicatorBarsRepo[ i - 2 ] - CandleAverage.Close( CandleSettingEnum.Equal, EqualPeriodTotal, i - 2 ) )
        //			{
        //				// the user should consider that stick sandwich is significant when coming in a downtrend, 

        //				//if ( _close[ i ] < innerBBLoweri &&
        //				//     _close[ i - 2 ] < innerBBLower2 ) {}

        //				// Tony: Need to code for the consideration of the downtrend.
        //                      if ( outIdx - 2 >= 0 )
        //                      {
        //                          buffer[ outIdx - 2 ] = TACandle.CdlStickSandwichBull;
        //                          buffer[ outIdx - 1 ] = TACandle.CdlStickSandwichBull;
        //                          buffer[ outIdx ]     = TACandle.CdlStickSandwichBull;
        //                      }
        //                      else
        //                      {
        //                          buffer[ outIdx ] = TACandle.CdlStickSandwichBull;
        //                      }							
        //			}    
        //		}
        //		else if ( IsWhiteCandle( i - 2 ) &&                                              // first black
        //				  IsBlackCandle( i - 1 ) &&
        //				  IsWhiteCandle( i ) &&
        //				  !IsDojiCandle( i - 2 ) &&
        //				  !IsDojiCandle( i - 1 ) &&
        //				  !IsDojiCandle( i ) )
        //		{
        //			DateTime? barTime     = DatabarsRepo.GetTimeAtIndex( i );                    
        //			double innerBBUpperi  = IndicatorResult[ "InnerBBUpper" ][ i ];                    
        //			double innerBBUpper2  = IndicatorResult[ "InnerBBUpper" ][ i - 2 ];

        //			//if (   _close[ i ]      > innerBBUpperi &&                           
        //			//       _close[ i - 2 ]  > innerBBUpper2 )
        //			
        //			{
        //				if ( 
        //						IndicatorBarsRepo[ i ] .Close<= IndicatorBarsRepo[ i - 2 ] .Close+ CandleAverage( CandleSettingEnum.Equal, EqualPeriodTotal, i - 2 ) && // 1st and 3rd same close
        //						IndicatorBarsRepo[ i ] .Close>= IndicatorBarsRepo[ i - 2 ] - CandleAverage.Close( CandleSettingEnum.Equal, EqualPeriodTotal, i - 2 ) 
        //					)
        //				{
        //					// the user should consider that stick sandwich is significant when coming in a downtrend, 

        //					// Tony: Need to code for the consideration of the downtrend.
        //                          if ( outIdx - 2 >= 0 )
        //                          {
        //                              buffer[ outIdx - 2 ] = TACandle.CdlStickSandwichBear;
        //                              buffer[ outIdx - 1 ] = TACandle.CdlStickSandwichBear;
        //                              buffer[ outIdx ]     = TACandle.CdlStickSandwichBear;
        //                          }
        //                          else
        //                          {
        //                              buffer[ outIdx ] = TACandle.CdlStickSandwichBear;
        //                          }							
        //				}   						
        //			}					
        //		}
        //		
        //		outIdx++;
        //		EqualPeriodTotal += CandleRange( CandleSettingEnum.Equal, i - 2 ) - CandleRange( CandleSettingEnum.Equal, EqualTrailingIdx - 2 );
        //		i++;
        //		EqualTrailingIdx++;

        //	} while ( i <= endIdx );

        //	outNBElement = outIdx;
        //	outBegIdx = startIdx;

        //	return RetCode.Success;
        //}


        /// <summary>
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\bearish_thrusting.jpg" />
        /// In an established downtrend, day-one of the pattern is a long red candle continuing the trend
        /// Day-two is a blue day that closes into the body (below midpoint) of the previous day
        /// 
        /// The Thrusting pattern starts by a continuation of the established move. 
        /// Day two reflects a bullish rally that closes into the body of the previous day, but is not able to trade above the midpoint.
        /// 
        /// The pattern suggests that sellers have not been weakened by the bull rally and if anything, shorts have simply covered their positions allowing price to rise slightly. 
        /// Going forward the lack of strength exhibited by buyers would discourage longs to enter the market, and allow the continuation of the downtrend.
        /// 
        /// Because this patterns signal is so weak, analysts will look for confirmation from bearish moves the following day.
        /// 
        /// The Bearish Thrusting Pattern is very similar, but weaker than the Bullish Harami and Bullish Engulfing2 reversal patterns. 
        /// Where the day-two close on the Thrusting pattern closes below the midpoint, Haramis close hits at or above the midpoint, 
        /// the Engulfing2 patterns’ day-two close reach above day-ones open.
        /// 
        /// Bearish Thrustin vs In Neck and On Neck Continuation Patterns Visually, the bearish Thrusting looks similar to the On Neck and In Neck Patterns as well.
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\bearish_continuation_on_neck.jpg" />
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\bearish_continuation_in_neck.jpg" />
        /// </summary>
        //public RetCode Thrusting2( int startIdx, int endIdx, out int outBegIdx, out int outNBElement, ref TACandle[] buffer )
        //{
        //	int i, outIdx, lookbackCount;

        //	outNBElement = 0;
        //	outBegIdx = 0;

        //	if ( startIdx < 0 ) return RetCode.OutOfRangeStartIndex;

        //	if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return RetCode.OutOfRangeEndIndex;

        //	lookbackCount = fx.TALib.Core.CdlThrustingLookback();

        //	if ( startIdx < lookbackCount ) startIdx = lookbackCount;

        //	if ( startIdx > endIdx )
        //	{
        //		return RetCode.Success;
        //	}

        //	i = startIdx;

        //	outIdx = 0;

        //	/* Proceed with the calculation for the requested range.
        //	 * Must have:
        //	 * - first candle:      long black candle
        //	 * - second candle:     white candle with open below previous day low and close into previous day body under the midpoint;
        //	 *                      to differentiate it from in-neck the close should not be equal to the black candle's close
        //	 */
        //	do
        //	{
        //		if ( IsLongBlackCandle( i - 1 ) &&                                               // 1st Long Black
        //			 IsWhiteCandle( i ) &&                                                       // 2nd white
        //			 IndicatorBarsRepo[ i ]      .Low< IndicatorBarsRepo[ i - 1 ] .Low&&
        //			 IndicatorBarsRepo[ i ]    .Close<= IndicatorBarsRepo[ i - 1 ] .Close+ RealBodyLength( i - 1 ) * 0.5
        //			 )
        //		{
        //			DateTime? barTime = DatabarsRepo.GetTimeAtIndex( i );

        //			/* the user should consider that the thrusting pattern is significant when it appears in a downtrend and it could be 
        //			 * even bullish "when coming in an uptrend or occurring twice within several days" (Steve Nison says), while this 
        //			 * function does not consider the trend
        //			 */
        //			double macd         = IndicatorResult[ "MACD" ][ i - 3 ];
        //			double macdSignal   = IndicatorResult[ "MACDSignal" ][ i - 3 ];

        //                  if ( outIdx - 1 >= 0 )
        //                  {
        //                      buffer[ outIdx - 1 ] = TACandle.CdlThrusting;
        //                      buffer[ outIdx ]     = TACandle.CdlThrusting; 
        //                  }
        //                  else
        //                  {
        //                      buffer[ outIdx ] = TACandle.CdlThrusting;
        //                  }
        //		}

        //		i++;
        //		outIdx++;

        //	} while ( i <= endIdx );

        //	outNBElement = outIdx;
        //	outBegIdx = startIdx;

        //	return RetCode.Success;
        //}

        /// <summary>
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\bearish_meeting_lines.jpg" />
        /// Day-one is long blue day.
        /// Day-two is long red day • Both days close at the same price After an uptrend, the second days candle open above the previous close. 
        /// 
        /// in FX the open price on day-two would match days ones close. Thus instead of seeing a large red candle on day two FX traders following the same price action would see a Gravestone Doji1, 
        /// moderate strength reversal.
        /// </summary>

        public int MeetingLinesLoopBack()
        {
            return Core.MacdExtLookback( Core.MAType.Ema, Core.MAType.Ema, Core.MAType.Ema, 20, 40, 10 );
        }

        //public RetCode MeetingLinesBear2( int startIdx, int endIdx, out int outBegIdx, out int outNBElement, ref TACandle[] buffer )
        //{
        //	int i, outIdx, lookbackCount;

        //	outNBElement = 0;
        //	outBegIdx = 0;

        //	if ( startIdx < 0 ) return RetCode.OutOfRangeStartIndex;

        //	if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return RetCode.OutOfRangeEndIndex;

        //          lookbackCount = MeetingLinesLoopBack();

        //	if ( startIdx < lookbackCount ) startIdx = lookbackCount;

        //	if ( startIdx > endIdx )
        //	{
        //		return RetCode.Success;
        //	}

        //	double EqualPeriodTotal = 0;
        //	int EqualTrailingIdx    = startIdx - CandleAvgPeriod( CandleSettingEnum.Equal );

        //	i = EqualTrailingIdx;
        //	while ( i < startIdx )
        //	{
        //		EqualPeriodTotal += CandleRange( CandleSettingEnum.Equal, i - 1 );
        //		i++;
        //	}

        //          i      = startIdx;
        //	outIdx = 0;

        //	/* Proceed with the calculation for the requested range.
        //	 * Must have:
        //	 * - first candle:      long white candle
        //	 * - second candle:     a gravestone doji
        //	 */
        //	do
        //	{
        //		if ( 
        //			    IsLongWhiteCandle( i - 1 ) &&                                               // 1st Long Black
        //			    IsGraveStoneDoji( i )                                                       // 2nd white					 
        //		   )
        //		{
        //				DateTime? barTime   = DatabarsRepo.GetTimeAtIndex( i );
        //		
        //				if ( IndicatorBarsRepo[ i ] .Close<= IndicatorBarsRepo[ i - 1 ] .Close+ CandleAverage( CandleSettingEnum.Equal, EqualPeriodTotal, i - 1 ) && // 1st and 3rd same close
        //					 IndicatorBarsRepo[ i ] .Close>= IndicatorBarsRepo[ i - 1 ] - CandleAverage.Close( CandleSettingEnum.Equal, EqualPeriodTotal, i - 1 ) )
        //				{
        //					
        //					double macd         = IndicatorResult[ "MACD" ][ i - 1 ];
        //					double macdSignal   = IndicatorResult[ "MACDSignal" ][ i - 1 ];
        //			
        //					if ( macd > macdSignal )
        //					{
        //                              if ( outIdx - 1 >= 0 )
        //                              {
        //                                  buffer[ outIdx - 1 ] = TACandle.CdlMeetingLinesBear;
        //                                  buffer[ outIdx ]     = TACandle.CdlMeetingLinesBear;
        //                              }
        //                              else
        //                              {
        //                                  buffer[ outIdx ] = TACandle.CdlMeetingLinesBear;
        //                              }
        //					}   
        //				}					
        //			}

        //		outIdx++;
        //		EqualPeriodTotal += CandleRange( CandleSettingEnum.Equal, i - 2 ) - CandleRange( CandleSettingEnum.Equal, EqualTrailingIdx - 2 );
        //		i++;
        //		EqualTrailingIdx++;

        //          } while ( i <= endIdx  );

        //	outNBElement = outIdx;
        //	outBegIdx    = startIdx;

        //	return RetCode.Success;
        //}

        /// <summary>
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\bullish_three_stars_in_the_south.jpg" />
        /// After an established downtrend, day-one is a long red day with a long lower shadow
        /// Day-two is also a red day similar to the first, only with a smaller body and shorter bottom Shadow
        /// Day-three trades within the second days range and has a small red body with no wick at all (Red Marubozu)
        /// 
        /// The Bullish Three Stars in the South formation suggests weakening in the established downtrend. Although each new day is able to close lower, 
        /// and despite the fact that sellers are able to drive price down illustrated by the lower wicks, those short positions are not able to get the close price to continue the strong bearish trend.
        /// 
        /// While the pattern predicts a reversal, it may only reflect shorts paring off their symbolPositionSummary (just a delay or respite in the downtrend). 
        /// Thus analysts do not usually take the Bullish Three Stars in the South as a strong enough buy signal in itself. 
        /// Instead analysts use it as an indication to liquidate short positions and watch for buying opportunities.
        /// </summary>

        public int ThreeStarsInSouthLookBack()
        {
            return Core.BbandsLookback( Core.MAType.Ema, 20 );
        }

        public RetCode ThreeStarsInSouth3( int startIdx, int endIdx, out int outBegIdx, out int outNBElement, ref TACandle[ ] buffer )
        {
            int i, outIdx, lookbackCount;

            outNBElement = 0;
            outBegIdx = 0;

            if ( startIdx < 0 ) return RetCode.OutOfRangeStartIndex;

            if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return RetCode.OutOfRangeEndIndex;

            lookbackCount = ThreeStarsInSouthLookBack();

            if ( startIdx < lookbackCount ) startIdx = lookbackCount;

            if ( startIdx > endIdx )
            {
                return RetCode.Success;
            }

            i = startIdx;

            outIdx = 0;

            /* Proceed with the calculation for the requested range.
			   * Must have:
			   * - first candle:        long black candle with long lower shadow
			   * - second candle:       smaller black candle that opens higher than prior close but within prior candle's range 
			   *                        and trades lower than prior close but not lower than prior low and closes off of its low (it has a shadow)
			   * - third candle:        small black marubozu (or candle with very short shadows) engulfed by prior candle's range
			   * 
			   * the user should consider that 3 stars in the south is significant when it appears in downtrend, while this function 
			   * does not consider it
			*/
            do
            {
                if (
                        CandleColor( i - 2 ) == -1 &&                                    // 1st black
                        CandleColor( i - 1 ) == -1 &&                                    // 2nd black
                        CandleColor( i ) == -1                                         // 3rd black
                   )
                {
                    DateTime? barTime = Bars.GetTimeAtIndex( i );

                    if ( IsLongCandle( i - 2 ) && IsLowerShadowAboveAverage( i - 2 ) )
                    {
                        if ( RealBodyLength( i - 1 ) < RealBodyLength( i - 2 ) )                // 2nd: Smaller Candle
                        {
                            if ( Bars[ i - 1 ].Open <= Bars[ i - 2 ].High &&                //      that opens higher but within 1st range
                                 Bars[ i - 1 ].Low < Bars[ i - 2 ].Close &&               //      and trades lower than 1st close
                                 Bars[ i - 1 ].Low >= Bars[ i - 2 ].Low &&
                                 IsLowerShadowVeryShort( i - 1 ) == false )         //      and has a lower shadow
                            {
                                if ( IsShortCandle( i ) &&
                                     IsLowerShadowShort( i ) &&
                                     IsUpperShadowShort( i ) &&
                                     Bars[ i ].Low > Bars[ i - 1 ].Low && Bars[ i ].High < Bars[ i - 1 ].High )
                                {
                                    if ( outIdx - 2 >= 0 )
                                    {
                                        buffer[ outIdx - 2 ] = TACandle.CdlMeetingLinesBear;
                                        buffer[ outIdx - 1 ] = TACandle.CdlMeetingLinesBear;
                                        buffer[ outIdx ] = TACandle.CdlMeetingLinesBear;
                                    }
                                    else
                                    {
                                        buffer[ outIdx ] = TACandle.CdlMeetingLinesBear;
                                    }

                                }
                            }
                        }
                    }
                }

                outIdx++;
                i++;

            } while ( i <= endIdx );

            outNBElement = outIdx;
            outBegIdx = startIdx;

            return RetCode.Success;
        }

        /// <summary>
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\engulfing.png" />
        ///</summary>

        public int EngulfingLookBack()
        {
            return Core.BbandsLookback( Core.MAType.Ema, 20 );
        }
        public bool Engulfing2WithBollinger( int barB4Calculation, int startIdx, int endIdx, out int outBegIdx, out int outNBElement, ref TACandle[ ] buffer, out CandleStrength strength )
        {
            int i, outIdx, lookbackCount;

            outNBElement = 0;
            outBegIdx = 0;

            strength = CandleStrength.UNKNOWN;


            if ( startIdx < 0 ) return false;

            if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return false;

            lookbackCount = EngulfingLookBack();

            if ( startIdx < lookbackCount ) startIdx = lookbackCount;

            if ( startIdx > endIdx )
            {
                return false;
            }

            bool found = false;

            i = startIdx;

            outIdx = 0;

            /* Proceed with the calculation for the requested range.
             * Must have:
             * - first: black (white) real body
             * - second: white (black) real body that engulfs the prior real body
             * 
             * the user should consider that an engulfing must appear in a downtrend if bullish or in an uptrend if bearish,
             * while this function does not consider it
             */
            do
            {
                if ( ( IsBlackCandle( i - 1 ) && IsWhiteCandle( i ) &&
                       ( Bars[ i ].Close > Bars[ i - 1 ].Open ) ||
                       ( Bars[ i ].Close == Bars[ i - 1 ].Open && ( Bars[ i ].Low < Bars[ i - 1 ].Low || Bars[ i ].High > Bars[ i - 1 ].High ) )
                     )
                   )                                                                                                                                                                    // black engulfs white                          
                {
                    DateTime? barTime = Bars.GetTimeAtIndex( i );

                    if ( IsAboveAverageCandle( i ) )
                    {
                        double outerBBLower          = IndicatorResult[ "OuterBBLower" ][ i - 1 ];
                        double innerBBLower2         = IndicatorResult[ "InnerBBLower" ][ i  ];

                        double macd                  = IndicatorResult[ "MACD" ][ i - 1 ];
                        double macdSignal            = IndicatorResult[ "MACDSignal" ][ i - 1 ];

                        if ( Bars[ i - 1 ].Close < outerBBLower && ( Math.Abs( Bars[ i - 1 ].Close - outerBBLower ) > Math.Abs( Bars[ i - 1 ].Open - outerBBLower ) ) )
                        {
                            if ( outIdx - 1 >= 0 )
                            {
                                buffer[ outIdx - 1 ] = TACandle.CdlEngulfingBull;
                                buffer[ outIdx ] = TACandle.CdlEngulfingBull;
                            }
                            else
                            {
                                buffer[ outIdx ] = TACandle.CdlEngulfingBull;
                            }

                            found = true;
                        }
                        else if ( Bars[ i ].Open < innerBBLower2 && macd < macdSignal )
                        {
                            if ( outIdx - 1 >= 0 )
                            {
                                buffer[ outIdx - 1 ] = TACandle.CdlEngulfingBull;
                                buffer[ outIdx ] = TACandle.CdlEngulfingBull;
                            }
                            else
                            {
                                buffer[ outIdx ] = TACandle.CdlEngulfingBull;
                            }

                            found = true;
                        }

                    }
                }
                else if ( ( IsWhiteCandle( i - 1 ) && IsBlackCandle( i ) && ( Bars[ i ].Close < Bars[ i - 1 ].Open ) ||
                            ( Bars[ i ].Close == Bars[ i - 1 ].Open && ( Bars[ i ].Low < Bars[ i - 1 ].Low || Bars[ i ].High > Bars[ i - 1 ].High ) ) ) )
                {
                    if ( IsAboveAverageCandle( i ) )
                    {
                        double outerBBUpper  = IndicatorResult[ "OuterBBUpper" ][ i - 1 ];
                        double innerBBUpper2 = IndicatorResult[ "InnerBBUpper" ][ i ];
                        double macd          = IndicatorResult[ "MACD" ][ i ];
                        double macdSignal    = IndicatorResult[ "MACDSignal" ][ i ];

                        if ( Bars[ i - 1 ].Close > outerBBUpper && ( Math.Abs( Bars[ i - 1 ].Close - outerBBUpper ) > Math.Abs( Bars[ i - 1 ].Open - outerBBUpper ) ) )
                        {
                            if ( outIdx - 1 >= 0 )
                            {
                                buffer[ outIdx - 1 ] = TACandle.CdlEngulfingBear;
                                buffer[ outIdx ] = TACandle.CdlEngulfingBear;
                            }
                            else
                            {
                                buffer[ outIdx ] = TACandle.CdlEngulfingBear;
                            }

                            found = true;
                        }
                        else if ( Bars[ i ].Open > innerBBUpper2 &&
                                  macd > macdSignal )
                        {
                            if ( outIdx - 1 >= 0 )
                            {
                                buffer[ outIdx - 1 ] = TACandle.CdlEngulfingBear;
                                buffer[ outIdx ] = TACandle.CdlEngulfingBear;
                            }
                            else
                            {
                                buffer[ outIdx ] = TACandle.CdlEngulfingBear;
                            }

                            found = true;
                        }
                    }

                }

                outIdx++;
                i++;

            } while ( i <= endIdx );

            outNBElement = outIdx;
            outBegIdx = startIdx;

            return found;
        }

        /// <summary>
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\bullish_three_outside_up.jpg" />
        /// After an established downtrend, day-one continues the trend with a red candle
        /// Day-two is a long blue day that engulfs the body of the first day, closing well above the previous days open.
        /// The third day is a blue day with an even higher close than the second day.
        /// 
        /// The Bullish Three Outside Up pattern is one of the more clear-cut three day bullish reversal patterns. 
        /// The formation reflects buyers overtaking selling strength, and often precedes a continued rally in price. 
        /// In fact up to day-two we have a bullish Engulfing2 Pattern, itself a strong two-day reversal pattern.
        ///</summary>
        //public RetCode ThreeOutsideUpDown3WithBollinger( int startIdx, int endIdx, out int outBegIdx, out int outNBElement, ref TACandle[] buffer )
        //{
        //	int i, outIdx, lookbackCount;

        //	outNBElement = 0;
        //	outBegIdx = 0;

        //	if ( startIdx < 0 ) return RetCode.OutOfRangeStartIndex;

        //	if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return RetCode.OutOfRangeEndIndex;

        //	lookbackCount = fx.TALib.Core.BbandsLookback( Core.MAType.Ema, 20 );

        //	if ( startIdx < lookbackCount ) startIdx = lookbackCount;

        //	if ( startIdx > endIdx )
        //	{
        //		return RetCode.Success;
        //	}

        //	i = startIdx;

        //	outIdx = 0;

        //   /* Proceed with the calculation for the requested range.
        //	* Must have:
        //	* - first:      black (white) real body
        //	* - second:     white (black) real body that engulfs the prior real body
        //	* - third:      candle that closes higher (lower) than the second candle
        //	* 
        //	* the user should consider that a three outside up must appear in a downtrend and three outside down must appear
        //	* in an uptrend, while this function does not consider it
        //	*/
        //	do
        //	{
        //		if ( IsBlackCandle( i - 2 ) &&
        //			 IsWhiteCandle( i - 1 ) &&
        //			 IsWhiteCandle( i ) )
        //		{
        //			DateTime? barTime    = DatabarsRepo.GetTimeAtIndex( i - 1 );

        //			if ( 
        //					 IndicatorBarsRepo[ i - 1 ] .Close> IndicatorBarsRepo[ i - 2 ] .Open&&
        //					 IndicatorBarsRepo[ i ] .Close> IndicatorBarsRepo[ i - 1 ] .Close
        //			   )
        //			{
        //				double innerBBLower  = IndicatorResult[ "InnerBBLower" ][ i - 2 ];

        //				if ( IndicatorBarsRepo[ i - 2 ] .Close<= innerBBLower )
        //				{
        //                          if ( outIdx - 2 >= 0 )
        //                          {
        //                              buffer[ outIdx - 2 ] = TACandle.Cdl3OutsideUp;
        //                              buffer[ outIdx - 1 ] = TACandle.Cdl3OutsideUp;
        //                              buffer[ outIdx ]     = TACandle.Cdl3OutsideUp;
        //                          }
        //                          else
        //                          {
        //                              buffer[ outIdx ] = TACandle.Cdl3OutsideUp;
        //                          }

        //					
        //				}
        //			}
        //		}               
        //		else if ( 
        //				  IsWhiteCandle( i - 2 ) &&
        //				  IsBlackCandle( i - 1 ) &&
        //				  IsBlackCandle( i ) )
        //		{
        //			DateTime? barTime = DatabarsRepo.GetTimeAtIndex( i - 1 );

        //			if (
        //					IndicatorBarsRepo[ i - 1 ] .Close< IndicatorBarsRepo[ i - 2 ] .Open&&
        //					IndicatorBarsRepo[ i ]    .Close<  IndicatorBarsRepo[ i - 1 ]  .Close
        //				)                                    // third candle lower
        //			{
        //				

        //				double innerBBUpper  = IndicatorResult[ "InnerBBUpper" ][ i - 2 ];

        //				if ( IndicatorBarsRepo[ i - 2 ] .Close> innerBBUpper )
        //				{
        //                          if ( outIdx - 2 >= 0 )
        //                          {
        //                              buffer[ outIdx - 2 ] = TACandle.Cdl3OutsideDown;
        //                              buffer[ outIdx - 1 ] = TACandle.Cdl3OutsideDown;
        //                              buffer[ outIdx ]     = TACandle.Cdl3OutsideDown;
        //                          }
        //                          else
        //                          {
        //                              buffer[ outIdx ] = TACandle.Cdl3OutsideUp;
        //                          }
        //				}                    
        //			}
        //		}
        //			

        //		outIdx++;
        //		i++;

        //	} while ( i <= endIdx );

        //	outNBElement = outIdx;
        //	outBegIdx = startIdx;

        //	return RetCode.Success;
        //}

        /// <summary>
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\bearish_belt_hold.jpg" />
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\bullish_belt_hold.jpg" />
        /// In an uptrend, a long red candle occurs
        /// The red candle has an open price that is very close to the high of the day. Little or no upper wick. After a stretch of bullish candlesticks, a strong bearish candle forms. 
        /// The bearish candle opens at the days high and closes significantly lower. The result is a long red candlestick with a short lower wick or no wick at all. 
        /// This may signify a bearish trend ahead. 
        /// The third day is a blue day with an even higher close than the second day.
        /// 
        /// The Bullish Three Outside Up pattern is one of the more clear-cut three day bullish reversal patterns. 
        /// The formation reflects buyers overtaking selling strength, and often precedes a continued rally in price. 
        /// In fact up to day-two we have a bullish Engulfing2 Pattern, itself a strong two-day reversal pattern.
        ///</summary>
        //public RetCode BeltHold1( int startIdx, int endIdx, out int outBegIdx, out int outNBElement, ref TACandle[] buffer )
        //{
        //	int i, outIdx, lookbackCount;

        //	outNBElement = 0;
        //	outBegIdx = 0;

        //	if ( startIdx < 0 ) return RetCode.OutOfRangeStartIndex;

        //	if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return RetCode.OutOfRangeEndIndex;

        //	lookbackCount = fx.TALib.Core.BbandsLookback( Core.MAType.Ema, 20 );

        //	if ( startIdx < lookbackCount ) startIdx = lookbackCount;

        //	if ( startIdx > endIdx )
        //	{
        //		return RetCode.Success;
        //	}

        //	i = startIdx;

        //	outIdx = 0;

        //	/* Proceed with the calculation for the requested range.
        //	 * Must have:
        //	 * - first:      black (white) real body
        //	 * - second:     white (black) real body that engulfs the prior real body
        //	 * - third:      candle that closes higher (lower) than the second candle
        //	 * 
        //	 * the user should consider that a three outside up must appear in a downtrend and three outside down must appear
        //	 * in an uptrend, while this function does not consider it
        //	 */
        //	do
        //	{
        //		if ( IsLongCandle( i ) )                                             // third candle higher                                                     
        //		{
        //			if ( IsWhiteCandle( i ) )
        //			{
        //				if ( IsLowerShadowVeryShort( i ) )
        //				{
        //					DateTime? barTime = DatabarsRepo.GetTimeAtIndex( i );

        //					buffer[ outIdx ] = TACandle.CdlBeltHoldBull;
        //				}
        //			}
        //			else if ( IsBlackCandle( i ) )
        //			{
        //				if ( IsUpperShadowVeryShort( i ) )
        //				{
        //					DateTime? barTime = DatabarsRepo.GetTimeAtIndex( i );

        //					buffer[ outIdx ] = TACandle.CdlBeltHoldBull;
        //				}
        //			}
        //		}                

        //		outIdx++;
        //		i++;

        //	} while ( i <= endIdx );

        //	outNBElement = outIdx;
        //	outBegIdx = startIdx;

        //	return RetCode.Success;
        //}


        /// <summary>
        /// /// <image url="$(SolutionDir)\..\..\30 - CommonImages\bearish_advance_block.jpg" />
        /// Three consecutive blue days, each with a higher close than the previous day
        /// For each new day the body is significantly smaller than the previous days body
        /// The second and third days should also show long upper wicks
        /// 
        /// After an uptrend, three consecutive blue days close higher everyday. Three blue days in a row typically reflect the very definition of a bullish trend. 
        /// But with the Advance Line each successive bull candle is weaker than the preceding day. Often the second and third days will have longer upper wicks, 
        /// indicative the market striving for higher, unsustainable highs; a hallmark of a weakening uptrend. 
        /// Taken as a whole this scenario suggests that the previous upward rally is losing steam and preparing for a reversal.
        /// 
        /// Long symbolPositionSummary holders need to be careful when the bull candle bodies are progressively getting smaller or showing relatively long upper wick. 
        /// They may want to consider protecting their positions from future price weakening.
        /// 
        /// The Bearish Advance Block Pattern is not normally a strong reversal pattern, but it can potentially predict a price decline. 
        /// This pattern’s signal is stronger when a previous uptrend has pushed prices to new highs. The pattern suggests that buy positions liquidate their positions, 
        /// though it is not yet clear that an opportunity to short exists. A fourth day reversal (red candle) would act as confirmation for short traders who are looking to enter the market.
        /// </summary>
        public int AdvanceBlockLookBack()
        {
            return Core.CdlAdvanceBlockLookback();
        }

        public RetCode AdvanceBlock3( int startIdx, int endIdx, out int outBegIdx, out int outNBElement, ref TACandle[ ] buffer )
        {
            int i, outIdx, lookbackCount, NearTrailingIdx, FarTrailingIdx;

            double [] NearPeriodTotal = new double[ 3 ];
            double [] FarPeriodTotal  = new double[ 3 ];

            outNBElement = 0;
            outBegIdx = 0;

            if ( startIdx < 0 ) return RetCode.OutOfRangeStartIndex;

            if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return RetCode.OutOfRangeEndIndex;

            lookbackCount = AdvanceBlockLookBack();

            if ( startIdx < lookbackCount ) startIdx = lookbackCount;

            if ( startIdx > endIdx )
            {
                return RetCode.Success;
            }

            NearTrailingIdx = startIdx - CandleAvgPeriod( CandleSettingEnum.Near );
            FarTrailingIdx = startIdx - CandleAvgPeriod( CandleSettingEnum.Far );

            i = NearTrailingIdx;
            while ( i < startIdx )
            {
                NearPeriodTotal[ 2 ] += CandleRange( CandleSettingEnum.Near, i - 2 );
                NearPeriodTotal[ 1 ] += CandleRange( CandleSettingEnum.Near, i - 1 );
                i++;
            }

            i = FarTrailingIdx;
            while ( i < startIdx )
            {
                FarPeriodTotal[ 2 ] += CandleRange( CandleSettingEnum.Far, i - 2 );
                FarPeriodTotal[ 1 ] += CandleRange( CandleSettingEnum.Far, i - 1 );
                i++;
            }

            i = startIdx;

            outIdx = 0;

            /* Proceed with the calculation for the requested range.
             * Must have:
             * - three white candlesticks with consecutively higher closes
             * - each candle opens within or near the previous white real body 
             * - first candle: long white with no or very short upper shadow (a short shadow is accepted too for more flexibility)
             * - second and third candles, or only third candle, show signs of weakening: progressively smaller white real bodies 
             * and/or relatively long upper shadows; see below for specific conditions
             * 
             * Advance block is always bearish;
             * the user should consider that advance block is significant when it appears in uptrend, while this function 
             * does not consider it
             */
            do
            {
                if ( IsWhiteCandle( i - 2 ) && IsWhiteCandle( i - 1 ) && IsWhiteCandle( i ) )
                {
                    if ( Bars[ i ].Close > Bars[ i - 1 ].Close &&                               // consecutive higher closes
                         Bars[ i - 1 ].Close > Bars[ i - 2 ].Close )
                    {
                        if ( IsAboveAverageCandle( i - 2 ) &&                               // 1st: long real body with short upper shadow
                             IsUpperShadowShort( i - 2 ) )
                        {
                            if ( ( RealBodyLength( i - 1 ) < RealBodyLength( i - 2 ) - CandleAverage( CandleSettingEnum.Far, FarPeriodTotal[ 2 ], i - 2 ) ) &&
                                 ( RealBodyLength( i ) < RealBodyLength( i - 1 ) + CandleAverage( CandleSettingEnum.Far, FarPeriodTotal[ 1 ], i - 1 ) ) )
                            {
                                // ( 2 far smaller than 1 && 3 not longer than 2 )
                                // advance blocked with the 2nd, 3rd must not carry on the advance
                                DateTime? barTime = Bars.GetTimeAtIndex( i );

                                if ( outIdx - 2 >= 0 )
                                {
                                    buffer[ outIdx - 2 ] = TACandle.CdlAdvanceBlock;
                                    buffer[ outIdx - 1 ] = TACandle.CdlAdvanceBlock;
                                    buffer[ outIdx ] = TACandle.CdlAdvanceBlock;
                                }
                                else
                                {
                                    buffer[ outIdx ] = TACandle.CdlAdvanceBlock;
                                }
                            }
                            else if ( RealBodyLength( i ) < RealBodyLength( i - 1 ) - CandleAverage( CandleSettingEnum.Far, FarPeriodTotal[ 1 ], i - 1 ) )
                            {
                                // 3 far smaller than 2
                                // advance blocked with the 3rd
                                DateTime? barTime = Bars.GetTimeAtIndex( i );

                                if ( outIdx - 2 >= 0 )
                                {
                                    buffer[ outIdx - 2 ] = TACandle.CdlAdvanceBlock;
                                    buffer[ outIdx - 1 ] = TACandle.CdlAdvanceBlock;
                                    buffer[ outIdx ] = TACandle.CdlAdvanceBlock;
                                }
                                else
                                {
                                    buffer[ outIdx ] = TACandle.CdlAdvanceBlock;
                                }
                            }
                            else if ( RealBodyLength( i ) < RealBodyLength( i - 1 ) &&
                                      RealBodyLength( i - 1 ) < RealBodyLength( i - 2 ) &&
                                      ( IsUpperShadowAboveAverage( i ) || IsUpperShadowAboveAverage( i - 1 ) )
                                )
                            {
                                DateTime? barTime = Bars.GetTimeAtIndex( i );

                                if ( outIdx - 2 >= 0 )
                                {
                                    buffer[ outIdx - 2 ] = TACandle.CdlAdvanceBlock;
                                    buffer[ outIdx - 1 ] = TACandle.CdlAdvanceBlock;
                                    buffer[ outIdx ] = TACandle.CdlAdvanceBlock;
                                }
                                else
                                {
                                    buffer[ outIdx ] = TACandle.CdlAdvanceBlock;
                                }
                            }
                            else if ( RealBodyLength( i ) < RealBodyLength( i - 1 ) &&
                                      IsUpperShadowLong( i ) )
                            {
                                DateTime? barTime = Bars.GetTimeAtIndex( i );

                                if ( outIdx - 2 >= 0 )
                                {
                                    buffer[ outIdx - 2 ] = TACandle.CdlAdvanceBlock;
                                    buffer[ outIdx - 1 ] = TACandle.CdlAdvanceBlock;
                                    buffer[ outIdx ] = TACandle.CdlAdvanceBlock;
                                }
                                else
                                {
                                    buffer[ outIdx ] = TACandle.CdlAdvanceBlock;
                                }
                            }
                        }
                    }
                }


                for ( int totIdx = 2; totIdx >= 1; --totIdx )
                {
                    FarPeriodTotal[ totIdx ] += CandleRange( CandleSettingEnum.Far, i - totIdx ) - CandleRange( CandleSettingEnum.Far, FarTrailingIdx - totIdx );
                    NearPeriodTotal[ totIdx ] += CandleRange( CandleSettingEnum.Near, i - totIdx ) - CandleRange( CandleSettingEnum.Near, NearTrailingIdx - totIdx );
                }

                NearTrailingIdx++;
                FarTrailingIdx++;
                outIdx++;
                i++;

            } while ( i <= endIdx );

            outNBElement = outIdx;
            outBegIdx = startIdx;

            return RetCode.Success;
        }

        public int DojiOrHammerLookBack()
        {
            return Core.BbandsLookback( Core.MAType.Ema, 20 );
        }

        public RetCode DojiOrHammer1WithBollinger( int startIdx, int endIdx, out int outBegIdx, out int outNBElement, ref TACandle[ ] buffer )
        {
            int i, outIdx, lookbackCount;

            outNBElement = 0;
            outBegIdx = 0;

            if ( startIdx < 0 ) return RetCode.OutOfRangeStartIndex;

            if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return RetCode.OutOfRangeEndIndex;

            lookbackCount = DojiOrHammerLookBack();

            if ( startIdx < lookbackCount ) startIdx = lookbackCount;

            if ( startIdx > endIdx )
            {
                return RetCode.Success;
            }

            i = startIdx;

            /* Proceed with the calculation for the requested range.
			 * Must have:
			 * - first candle: long real body
			 * - second candle: star (open gapping up in an uptrend or down in a downtrend) with a doji
			 * The meaning of "doji" and "long" is specified with TA_SetCandleSettings
			 * 
			 * it's defined bullish when the long candle is white and the star gaps up, bearish when the long candle 
			 * is black and the star gaps down; the user should consider that a doji star is bullish when it appears 
			 * in an uptrend and it's bearish when it appears in a downtrend, so to determine the bullishness or 
			 * bearishness of the pattern the trend must be analyzed
			 */
            outIdx = 0;
            do
            {
                if ( IsDojiCandle( i ) )
                {
                    double innerBBUpper  = IndicatorResult[ "InnerBBUpper" ][ i - 1 ];
                    double outerBBUpper  = IndicatorResult[ "OuterBBUpper" ][ i - 1 ];
                    double innerBBLower  = IndicatorResult[ "InnerBBLower" ][ i - 1 ];
                    double outerBBLower  = IndicatorResult[ "OuterBBLower" ][ i - 1 ];

                    buffer[ outIdx ] = TACandle.CdlDoji;

                    if ( IsWhiteCandle( i - 1 ) && ( Bars[ i - 1 ].Close > innerBBUpper || Bars[ i - 1 ].High > outerBBUpper ) )
                    {
                        if ( outIdx - 1 >= 0 )
                        {
                            buffer[ outIdx - 1 ] = TACandle.CdlDojiStarBear;
                        }
                    }
                    else if ( IsBlackCandle( i - 1 ) && ( Bars[ i - 1 ].Close < innerBBLower || Bars[ i - 1 ].Low < outerBBLower ) )
                    {
                        if ( outIdx - 1 >= 0 )
                        {
                            buffer[ outIdx - 1 ] = TACandle.CdlDojiStarBull;
                        }
                    }
                }
                else if ( IsHammerCandle( i ) )
                {
                    double Mean  = IndicatorResult[ "BBMean" ][ i ];


                    if ( Bars[ i ].Close < Mean )
                    {
                        buffer[ outIdx ] = TACandle.CdlHammer;
                    }
                    else
                    {
                        buffer[ outIdx ] = TACandle.CdlHangingMan;
                    }

                }
                else if ( IsShootingStar( i ) )
                {
                    double Mean  = IndicatorResult[ "BBMean" ][ i ];

                    if ( Bars[ i ].Close > Mean )
                    {
                        buffer[ outIdx ] = TACandle.CdlShootingStar;
                    }
                    else
                    {
                        buffer[ outIdx ] = TACandle.CdlInvertedHammer;
                    }
                }

                /* add the current range and subtract the first range: this is done after the pattern recognition 
				 * when avgPeriod is not 0, that means "compare with the previous candles" (it excludes the current candle)
				 */
                i++;
                outIdx++;

            } while ( i <= endIdx );

            outNBElement = outIdx;
            outBegIdx = startIdx;

            return RetCode.Success;
        }
        /// <summary>
        /// /// <image url="$(SolutionDir)\..\..\30 - CommonImages\bearish_doji_star.jpg" />
        /// The first day is long blue day
        /// Second day is a doji that opens at the previous days close
        /// The doji wicks should not be long
        /// 
        /// The Doji1 Star formation starts as the bull market continues with a strong blue day. The second day however, trades within a small range and closes at or near its open, 
        /// implying the sellers and buyers are in deadlock. This is taken as a sign that buyers are losing control and bullish momentum is weakening.
        /// 
        /// For strong confirmation of trend reversal, watch for a red day with a lower close on the third trading day. 
        /// Such a formation on the third day would be the strong Bearish Abandoned Baby or Evening Doji1 Star.
        /// </summary>

        public RetCode DojiStar2WithBollinger( int startIdx, int endIdx, out int outBegIdx, out int outNBElement, ref TACandle[ ] buffer )
        {
            int i, outIdx, lookbackCount;

            outNBElement = 0;
            outBegIdx = 0;

            if ( startIdx < 0 ) return RetCode.OutOfRangeStartIndex;

            if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return RetCode.OutOfRangeEndIndex;

            lookbackCount = Core.BbandsLookback( Core.MAType.Ema, 20 );

            if ( startIdx < lookbackCount ) startIdx = lookbackCount;

            if ( startIdx > endIdx )
            {
                return RetCode.Success;
            }

            i = startIdx;

            /* Proceed with the calculation for the requested range.
             * Must have:
             * - first candle: long real body
             * - second candle: star (open gapping up in an uptrend or down in a downtrend) with a doji
             * The meaning of "doji" and "long" is specified with TA_SetCandleSettings
             * 
             * it's defined bullish when the long candle is white and the star gaps up, bearish when the long candle 
             * is black and the star gaps down; the user should consider that a doji star is bullish when it appears 
             * in an uptrend and it's bearish when it appears in a downtrend, so to determine the bullishness or 
             * bearishness of the pattern the trend must be analyzed
             */
            outIdx = 0;
            do
            {
                if (
                        IsAboveAverageWhiteCandle( i - 1 ) &&                               // 1st: long                     
                        IsDojiCandle( i )
                    )
                {
                    DateTime? barTime = Bars.GetTimeAtIndex( i - 1 );


                    double innerBBUpper  = IndicatorResult[ "InnerBBUpper" ][ i - 1 ];
                    double outerBBUpper  = IndicatorResult[ "OuterBBUpper" ][ i - 1 ];
                    double innerBBLower  = IndicatorResult[ "InnerBBLower" ][ i - 2 ];
                    double outerBBLower  = IndicatorResult[ "OuterBBLower" ][ i - 2 ];

                    if ( IsWhiteCandle( i - 1 ) &&
                         ( Bars[ i - 1 ].Close > innerBBUpper || Bars[ i - 1 ].High > outerBBUpper ) )
                    {
                        if ( outIdx - 1 >= 0 )
                        {
                            buffer[ outIdx - 1 ] = TACandle.CdlDojiStarBear;
                            buffer[ outIdx ] = TACandle.CdlDojiStarBear;
                        }
                        else
                        {
                            buffer[ outIdx ] = TACandle.CdlDojiStarBear;
                        }


                    }
                    else if ( IsBlackCandle( i - 1 ) &&
                         ( Bars[ i - 1 ].Close < innerBBLower || Bars[ i - 1 ].Low < outerBBLower ) )
                    {
                        if ( outIdx - 1 >= 0 )
                        {
                            buffer[ outIdx - 1 ] = TACandle.CdlDojiStarBull;
                            buffer[ outIdx ] = TACandle.CdlDojiStarBull;
                        }
                        else
                        {
                            buffer[ outIdx ] = TACandle.CdlDojiStarBull;
                        }


                    }
                }

                /* add the current range and subtract the first range: this is done after the pattern recognition 
				 * when avgPeriod is not 0, that means "compare with the previous candles" (it excludes the current candle)
				 */
                i++;
                outIdx++;

            } while ( i <= endIdx );

            outNBElement = outIdx;
            outBegIdx = startIdx;

            return RetCode.Success;
        }

        /// 
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\bullish-upside-gap-three-methods.gif" />
        //public RetCode XsideGap3Methods3( int startIdx, int endIdx, out int outBegIdx, out int outNBElement, ref TACandle[] buffer )
        //{
        //	int i, outIdx, lookbackCount;

        //	outNBElement = 0;
        //	outBegIdx = 0;

        //	if ( startIdx < 0 ) return RetCode.OutOfRangeStartIndex;

        //	if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return RetCode.OutOfRangeEndIndex;

        //	lookbackCount = fx.TALib.Core.BbandsLookback( Core.MAType.Ema, 20 );

        //	if ( startIdx < lookbackCount ) startIdx = lookbackCount;

        //	if ( startIdx > endIdx )
        //	{
        //		return RetCode.Success;
        //	}

        //	i = startIdx;

        //	outIdx = 0;

        //	/* Proceed with the calculation for the requested range.
        //	 * Must have:
        //	 * - first candle:      white (black) candle
        //	 * - second candle:     white (black) candle
        //	 * -                    upside (downside) gap between the first and the second real bodies
        //	 * - third candle:      black (white) candle that opens within the second real body and closes within the first real body
        //	 * 
        //	 * the user should consider that up/downside gap 3 methods is significant when it appears in a trend, while this 
        //	 * function does not consider it
        //	 */
        //	do
        //	{
        //		if ( CandleColor( i - 2 ) == CandleColor( i - 1 ) && CandleColor( i - 1 ) == -CandleColor( i ) )                                       // 3 black candlesticks                                                
        //		{
        //			if ( IsLongCandle( i - 2 ) &&
        //				 IsLongCandle( i - 1 ) &&
        //				 IsLongCandle( i ) )
        //			{
        //				if ( IsWhiteCandle( i - 2 ) )
        //				{
        //					if ( IndicatorBarsRepo[ i ] .Close< IndicatorBarsRepo[ i - 2 ] - 0.Close.5 * RealBodyLength( i - 2 ) )
        //					{
        //						DateTime? barTime = DatabarsRepo.GetTimeAtIndex( i );

        //                              if ( outIdx - 2 >= 0 )
        //                              {
        //                                  buffer[ outIdx - 2 ] = TACandle.CdlUpsideGap3;
        //                                  buffer[ outIdx - 1 ] = TACandle.CdlUpsideGap3;
        //                                  buffer[ outIdx ]     = TACandle.CdlUpsideGap3;
        //                              }
        //                              else
        //                              {
        //                                  buffer[ outIdx ] = TACandle.CdlUpsideGap3;
        //                              }
        //					}
        //				}
        //				else if ( IsBlackCandle( i - 2 ) )
        //				{
        //					if ( IndicatorBarsRepo[ i ] .Close> IndicatorBarsRepo[ i - 2 ] .Close+ 0.5 * RealBodyLength( i -2  ) )
        //					{
        //						DateTime? barTime = DatabarsRepo.GetTimeAtIndex( i );

        //                              if ( outIdx - 2 >= 0 )
        //                              {
        //                                  buffer[ outIdx - 2 ] = TACandle.CdlDownSideGap3;
        //                                  buffer[ outIdx - 1 ] = TACandle.CdlDownSideGap3;
        //                                  buffer[ outIdx ]     = TACandle.CdlDownSideGap3;
        //                              }
        //                              else
        //                              {
        //                                  buffer[ outIdx ] = TACandle.CdlDownSideGap3;
        //                              }
        //					}
        //				}
        //			}
        //		}

        //		outIdx++;
        //		i++;

        //	} while ( i <= endIdx );

        //	outNBElement = outIdx;
        //	outBegIdx = startIdx;

        //	return RetCode.Success;
        //}



        public int KickingLoopBack()
        {
            return ( Core.CdlKickingLookback() );
        }
        /// <summary>
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\BullishKicker.png" scale="0.7"/>
        /// 
        /// </summary>

        //public RetCode Kicking2( int startIdx, int endIdx, out int outBegIdx, out int outNBElement, ref TACandle[] buffer )
        //{
        //	int i, outIdx, lookbackCount;

        //	outNBElement = 0;
        //	outBegIdx = 0;

        //	if ( startIdx < 0 ) return RetCode.OutOfRangeStartIndex;

        //	if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return RetCode.OutOfRangeEndIndex;

        //          lookbackCount = KickingLoopBack();

        //	if ( startIdx < lookbackCount ) startIdx = lookbackCount;

        //	if ( startIdx > endIdx )
        //	{
        //		return RetCode.Success;
        //	}

        //	i = startIdx;

        //	outIdx = 0;

        //	/* Proceed with the calculation for the requested range.
        //	 * Must have:
        //	 * - first candle:      marubozu
        //	 * - second candle:     opposite color marubozu
        //	 * -                    upside (downside) gap between the first and the second real bodies
        //	 * - gap between the two candles: upside gap if black then white, downside gap if white then black
        //	 *              
        //	 */
        //	do
        //	{
        //		if ( CandleColor( i - 1 ) == -CandleColor( i ) )                                       // They are opposite color                                                
        //		{
        //			if ( 
        //				  IsMarubozuCandle( i - 1 ) &&
        //				  IsMarubozuCandle( i )  
        //			   )
        //			{
        //				// if ( RealBodyLength( i - 1 )  < RealBodyLength( i ) )
        //				if ( RealBodyLength( i - 1 ) * 2 < RealBodyLength( i ) )
        //				{
        //					DateTime? barTime = DatabarsRepo.GetTimeAtIndex( i );

        //					if ( IsBlackCandle( i ) )
        //					{
        //                              if ( outIdx - 1 >= 0 )
        //                              {
        //                                  buffer[ outIdx - 1 ] = TACandle.CdlKickingBear;
        //                                  buffer[ outIdx ]     = TACandle.CdlKickingBear;    
        //                              }
        //                              else
        //                              {
        //                                  buffer[ outIdx ] = TACandle.CdlKickingBear;
        //                              }
        //					}
        //					else if ( IsWhiteCandle( i ) )
        //					{
        //                              if ( outIdx - 1 >= 0 )
        //                              {
        //                                  buffer[ outIdx - 1 ] = TACandle.CdlKickingBull;
        //                                  buffer[ outIdx ]     = TACandle.CdlKickingBull;
        //                              }
        //                              else
        //                              {
        //                                  buffer[ outIdx ] = TACandle.CdlKickingBear;
        //                              }
        //					}
        //				}                        
        //			}
        //		}

        //		outIdx++;
        //		i++;

        //	} while ( i <= endIdx );

        //	outNBElement = outIdx;
        //	outBegIdx    = startIdx;

        //	return RetCode.Success;
        //}



        //public RetCode ThirteenNewPriceLines13( int startIdx, int endIdx, out int outBegIdx, out int outNBElement, ref TACandle[] buffer )
        //{
        //	int i, outIdx, lookbackCount;

        //	outNBElement = 0;
        //	outBegIdx    = 0;

        //	if ( startIdx < 0 ) return RetCode.OutOfRangeStartIndex;

        //	if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return RetCode.OutOfRangeEndIndex;

        //	lookbackCount = fx.TALib.Core.BbandsLookback( Core.MAType.Ema, 20 );

        //	if ( startIdx < lookbackCount ) startIdx = lookbackCount;

        //	if ( startIdx > endIdx )
        //	{
        //		return RetCode.Success;
        //	}

        //	i = startIdx;

        //	outIdx = 0;

        //	/* Proceed with the calculation for the requested range.
        //	 * Must have:
        //	 * - first candle:      marubozu
        //	 * - second candle:     opposite color marubozu
        //	 * -                    upside (downside) gap between the first and the second real bodies
        //	 * - gap between the two candles: upside gap if black then white, downside gap if white then black
        //	 *              
        //	 */
        //	do
        //	{
        //		if ( IndicatorBarsRepo[ i ] .High> IndicatorBarsRepo[ i - 1 ] .High&&
        //			 IndicatorBarsRepo[ i - 1 ] .High> IndicatorBarsRepo[ i - 2 ] .High&&
        //			 IndicatorBarsRepo[ i - 2 ] .High> IndicatorBarsRepo[ i - 3 ] .High&&
        //			 IndicatorBarsRepo[ i - 3 ] .High> IndicatorBarsRepo[ i - 4 ] .High&&
        //			 IndicatorBarsRepo[ i - 4 ] .High> IndicatorBarsRepo[ i - 5 ] .High&&
        //			 IndicatorBarsRepo[ i - 5 ] .High> IndicatorBarsRepo[ i - 6 ] .High&&
        //			 IndicatorBarsRepo[ i - 6 ] .High> IndicatorBarsRepo[ i - 7 ] .High&&
        //			 IndicatorBarsRepo[ i - 7 ] .High> IndicatorBarsRepo[ i - 8 ] .High&&
        //			 IndicatorBarsRepo[ i - 8 ] .High> IndicatorBarsRepo[ i - 9 ] .High&&
        //			 IndicatorBarsRepo[ i - 9 ] .High> IndicatorBarsRepo[ i - 10 ] .High&&
        //			 IndicatorBarsRepo[ i - 10 ] .High> IndicatorBarsRepo[ i - 11 ] .High&&
        //			 IndicatorBarsRepo[ i - 11 ] .High> IndicatorBarsRepo[ i - 12 ] .High&&
        //			 IndicatorBarsRepo[ i - 12 ] .High> IndicatorBarsRepo[ i - 13 ] .High)
        //		{
        //			DateTime? barTime = DatabarsRepo.GetTimeAtIndex( i );

        //                  if ( outIdx - 12 >= 0 )
        //                  {
        //                      buffer[ outIdx - 12 ] = TACandle.Cdl13NewPriceLinesBull;
        //                      buffer[ outIdx - 11 ] = TACandle.Cdl13NewPriceLinesBull;
        //                      buffer[ outIdx - 10 ] = TACandle.Cdl13NewPriceLinesBull;
        //                      buffer[ outIdx - 9 ]  = TACandle.Cdl13NewPriceLinesBull;
        //                      buffer[ outIdx - 8 ]  = TACandle.Cdl13NewPriceLinesBull;
        //                      buffer[ outIdx - 7 ]  = TACandle.Cdl13NewPriceLinesBull;
        //                      buffer[ outIdx - 6 ]  = TACandle.Cdl13NewPriceLinesBull;
        //                      buffer[ outIdx - 5 ]  = TACandle.Cdl13NewPriceLinesBull;
        //                      buffer[ outIdx - 4 ]  = TACandle.Cdl13NewPriceLinesBull;
        //                      buffer[ outIdx - 3 ]  = TACandle.Cdl13NewPriceLinesBull;
        //                      buffer[ outIdx - 2 ]  = TACandle.Cdl13NewPriceLinesBull;
        //                      buffer[ outIdx - 1 ]  = TACandle.Cdl13NewPriceLinesBull;
        //                      buffer[ outIdx ]      = TACandle.Cdl13NewPriceLinesBull;
        //                  }
        //                  else
        //                  {
        //                      buffer[ outIdx ] = TACandle.Cdl13NewPriceLinesBull;
        //                  }
        //		}
        //		else if (   IndicatorBarsRepo[ i ] .Low> IndicatorBarsRepo[ i - 1 ] .Low&&
        //					IndicatorBarsRepo[ i - 1 ] .Low> IndicatorBarsRepo[ i - 2 ] .Low&&
        //					IndicatorBarsRepo[ i - 2 ] .Low> IndicatorBarsRepo[ i - 3 ] .Low&&
        //					IndicatorBarsRepo[ i - 3 ] .Low> IndicatorBarsRepo[ i - 4 ] .Low&&
        //					IndicatorBarsRepo[ i - 4 ] .Low> IndicatorBarsRepo[ i - 5 ] .Low&&
        //					IndicatorBarsRepo[ i - 5 ] .Low> IndicatorBarsRepo[ i - 6 ] .Low&&
        //					IndicatorBarsRepo[ i - 6 ] .Low> IndicatorBarsRepo[ i - 7 ] .Low&&
        //					IndicatorBarsRepo[ i - 7 ] .Low> IndicatorBarsRepo[ i - 8 ] .Low&&
        //					IndicatorBarsRepo[ i - 8 ] .Low> IndicatorBarsRepo[ i - 9 ] .Low&&
        //					IndicatorBarsRepo[ i - 9 ] .Low> IndicatorBarsRepo[ i - 10 ] .Low&&
        //					IndicatorBarsRepo[ i - 10 ] .Low> IndicatorBarsRepo[ i - 11 ] .Low&&
        //					IndicatorBarsRepo[ i - 11 ] .Low> IndicatorBarsRepo[ i - 12 ] .Low&&
        //					IndicatorBarsRepo[ i - 12 ] .Low> IndicatorBarsRepo[ i - 13 ] .Low
        //			)
        //		{
        //			DateTime? barTime = DatabarsRepo.GetTimeAtIndex( i );

        //                  if ( outIdx - 12 >= 0 )
        //                  {
        //                      buffer[ outIdx - 12 ] = TACandle.Cdl13NewPriceLinesBear;
        //                      buffer[ outIdx - 11 ] = TACandle.Cdl13NewPriceLinesBear;
        //                      buffer[ outIdx - 10 ] = TACandle.Cdl13NewPriceLinesBear;
        //                      buffer[ outIdx - 9 ]  = TACandle.Cdl13NewPriceLinesBear;
        //                      buffer[ outIdx - 8 ]  = TACandle.Cdl13NewPriceLinesBear;
        //                      buffer[ outIdx - 7 ]  = TACandle.Cdl13NewPriceLinesBear;
        //                      buffer[ outIdx - 6 ]  = TACandle.Cdl13NewPriceLinesBear;
        //                      buffer[ outIdx - 5 ]  = TACandle.Cdl13NewPriceLinesBear;
        //                      buffer[ outIdx - 4 ]  = TACandle.Cdl13NewPriceLinesBear;
        //                      buffer[ outIdx - 3 ]  = TACandle.Cdl13NewPriceLinesBear;
        //                      buffer[ outIdx - 2 ]  = TACandle.Cdl13NewPriceLinesBear;
        //                      buffer[ outIdx - 1 ]  = TACandle.Cdl13NewPriceLinesBear;
        //                      buffer[ outIdx ]      = TACandle.Cdl13NewPriceLinesBear;   
        //                  }
        //                  else
        //                  {
        //                      buffer[ outIdx ] = TACandle.Cdl13NewPriceLinesBear;
        //                  }

        //			                 
        //		}
        //			
        //		outIdx++;
        //		i++;

        //	} while ( i <= endIdx );

        //	outNBElement = outIdx;
        //	outBegIdx = startIdx;

        //	return RetCode.Success;
        //}




        /// <summary>
        /// /// <image url="$(SolutionDir)\..\..\30 - CommonImages\bearish_falling_three_methods.jpg" />
        /// 
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\bullish-mat-hold.gif" />
        /// In a downtrend, a long red day occurs
        /// The second, third and fourth days are short blue days that fall within the range of the first day
        /// The fifth day continues the downtrend with a long red candle that creates new lows
        /// 
        /// The Falling Three Methods pattern occurs in a bear market, where during a downtrend the market rests before resuming the trend. 
        /// The bearish trends break is reflected by small candles that all stick to a strict market range formed by the aggressive move on day one.
        /// 
        /// A typical explanation for this type of formation might that the market is slowly digesting the relatively larger move in day-one. 
        /// These small daily ranges often precede significant economic reports. Such periods of relative inactivity and tight trading are common in markets. 
        /// Falling Three Methods is confirmed where a red candle dives down to new lows reinstituting the bearish trend.
        /// 
        /// Number of Middle Candles – In a picture perfect formation the middle candles number three. But realistically the pattern may have two, four or even five candles. 
        /// Individually each middle candle may be a star or doji, red or blue.
        /// 
        /// Middle Candle Wicks – Important to note is that each middle candle wick needs to stay within the first candles high/low range to signal a strong continuation signal. 
        /// With the bearish Falling Three Methods this is especially important for the highs. Should a wick trade to a high above the first large red candles high, 
        /// it casts doubt over the strength of the established down trend.
        /// </summary>

        public int RisingOrFallingLoookBack()
        {
            return Core.CdlRiseFall3MethodsLookback();
        }

        public RetCode RisingOrFalling3Methods456( int startIdx, int endIdx, out int outBegIdx, out int outNBElement, ref TACandle[ ] buffer )
        {
            int i, outIdx, lookbackCount;

            outNBElement = 0;
            outBegIdx = 0;

            if ( startIdx < 0 ) return RetCode.OutOfRangeStartIndex;

            if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return RetCode.OutOfRangeEndIndex;

            lookbackCount = RisingOrFallingLoookBack();

            if ( startIdx < lookbackCount ) startIdx = lookbackCount;

            if ( startIdx > endIdx )
            {
                return RetCode.Success;
            }

            i = startIdx;

            outIdx = 0;

            /* Proceed with the calculation for the requested range.
			 * Must have:
			 * - first:      black (white) real body
			 * - second:     white (black) real body that engulfs the prior real body
			 * - third:      candle that closes higher (lower) than the second candle
			 * 
			 * the user should consider that a three outside up must appear in a downtrend and three outside down must appear
			 * in an uptrend, while this function does not consider it
			 */
            do
            {
                if ( IsLongCandle( i - 5 ) &&
                     IsShortCandle( i - 4 ) &&
                     IsShortCandle( i - 3 ) &&
                     IsShortCandle( i - 2 ) &&
                     IsShortCandle( i - 1 ) &&
                     IsLongCandle( i ) )
                {
                    if ( CandleColor( i - 5 ) == -CandleColor( i - 4 ) &&
                         CandleColor( i - 4 ) == CandleColor( i - 1 ) &&
                         CandleColor( i - 1 ) == -CandleColor( i ) )
                    {
                        if ( Math.Min( Bars[ i - 4 ].Open, Bars[ i - 4 ].Close ) < Bars[ i - 5 ].High &&
                             Math.Max( Bars[ i - 4 ].Open, Bars[ i - 4 ].Close ) > Bars[ i - 5 ].Low &&
                             Math.Min( Bars[ i - 3 ].Open, Bars[ i - 3 ].Close ) < Bars[ i - 5 ].High &&
                             Math.Max( Bars[ i - 3 ].Open, Bars[ i - 3 ].Close ) > Bars[ i - 5 ].Low &&
                             Math.Min( Bars[ i - 2 ].Open, Bars[ i - 2 ].Close ) < Bars[ i - 5 ].High &&
                             Math.Max( Bars[ i - 2 ].Open, Bars[ i - 2 ].Close ) > Bars[ i - 5 ].Low &&
                             Math.Min( Bars[ i - 1 ].Open, Bars[ i - 1 ].Close ) < Bars[ i - 5 ].High &&
                             Math.Max( Bars[ i - 1 ].Open, Bars[ i - 1 ].Close ) > Bars[ i - 5 ].Low )
                        {
                            if ( Bars[ i - 4 ].Close > Bars[ i - 1 ].Close && Bars[ i ].Close > Bars[ i - 5 ].Close )
                            {
                                DateTime? barTime = Bars.GetTimeAtIndex( i );
                                if ( outIdx - 5 >= 0 )
                                {
                                    buffer[ outIdx - 5 ] = TACandle.CdlRising3Methods;
                                    buffer[ outIdx - 4 ] = TACandle.CdlRising3Methods;
                                    buffer[ outIdx - 3 ] = TACandle.CdlRising3Methods;
                                    buffer[ outIdx - 2 ] = TACandle.CdlRising3Methods;
                                    buffer[ outIdx - 1 ] = TACandle.CdlRising3Methods;
                                    buffer[ outIdx ] = TACandle.CdlRising3Methods;
                                }
                                else
                                {
                                    buffer[ outIdx ] = TACandle.CdlRising3Methods;
                                }
                            }
                            else if ( Bars[ i - 4 ].Close < Bars[ i - 1 ].Close && Bars[ i ].Close < Bars[ i - 5 ].Close )
                            {
                                DateTime? barTime = Bars.GetTimeAtIndex( i );

                                if ( outIdx - 5 >= 0 )
                                {
                                    buffer[ outIdx - 5 ] = TACandle.CdlFalling3Methods;
                                    buffer[ outIdx - 4 ] = TACandle.CdlFalling3Methods;
                                    buffer[ outIdx - 3 ] = TACandle.CdlFalling3Methods;
                                    buffer[ outIdx - 2 ] = TACandle.CdlFalling3Methods;
                                    buffer[ outIdx - 1 ] = TACandle.CdlFalling3Methods;
                                    buffer[ outIdx ] = TACandle.CdlFalling3Methods;
                                }
                                else
                                {
                                    buffer[ outIdx ] = TACandle.CdlFalling3Methods;
                                }


                            }
                        }
                    }
                }
                else if ( IsLongCandle( i - 4 ) && IsShortCandle( i - 3 ) && IsShortCandle( i - 2 ) && IsShortCandle( i - 1 ) && IsLongCandle( i ) )          // third candle higher                                                     
                {
                    if ( CandleColor( i - 4 ) == -CandleColor( i - 3 ) &&
                         CandleColor( i - 3 ) == CandleColor( i - 1 ) &&
                         CandleColor( i - 1 ) == -CandleColor( i ) )
                    {
                        // 2nd to 4th hold within 1st: a part of the real body must be within 1st range
                        if (
                                Math.Min( Bars[ i - 3 ].Open, Bars[ i - 3 ].Close ) < Bars[ i - 4 ].High &&
                                Math.Max( Bars[ i - 3 ].Open, Bars[ i - 3 ].Close ) > Bars[ i - 4 ].Low &&
                                Math.Min( Bars[ i - 2 ].Open, Bars[ i - 2 ].Close ) < Bars[ i - 4 ].High &&
                                Math.Max( Bars[ i - 2 ].Open, Bars[ i - 2 ].Close ) > Bars[ i - 4 ].Low &&
                                Math.Min( Bars[ i - 1 ].Open, Bars[ i - 1 ].Close ) < Bars[ i - 4 ].High &&
                                Math.Max( Bars[ i - 1 ].Open, Bars[ i - 1 ].Close ) > Bars[ i - 4 ].Low
                            )
                        {
                            // 2nd to 4th are falling (rising)

                            if ( Bars[ i - 3 ].Close > Bars[ i - 1 ].Close && Bars[ i ].Close > Bars[ i - 4 ].Close )
                            {
                                DateTime? barTime = Bars.GetTimeAtIndex( i );

                                if ( outIdx - 4 >= 0 )
                                {
                                    buffer[ outIdx - 4 ] = TACandle.CdlRising3Methods;
                                    buffer[ outIdx - 3 ] = TACandle.CdlRising3Methods;
                                    buffer[ outIdx - 2 ] = TACandle.CdlRising3Methods;
                                    buffer[ outIdx - 1 ] = TACandle.CdlRising3Methods;
                                    buffer[ outIdx ] = TACandle.CdlRising3Methods;
                                }
                                else
                                {
                                    buffer[ outIdx ] = TACandle.CdlRising3Methods;
                                }


                            }
                            else if ( Bars[ i - 3 ].Close < Bars[ i - 1 ].Close && Bars[ i ].Close < Bars[ i - 4 ].Close )
                            {
                                DateTime? barTime = Bars.GetTimeAtIndex( i );

                                if ( outIdx - 4 >= 0 )
                                {
                                    buffer[ outIdx - 4 ] = TACandle.CdlFalling3Methods;
                                    buffer[ outIdx - 3 ] = TACandle.CdlFalling3Methods;
                                    buffer[ outIdx - 2 ] = TACandle.CdlFalling3Methods;
                                    buffer[ outIdx - 1 ] = TACandle.CdlFalling3Methods;
                                    buffer[ outIdx ] = TACandle.CdlFalling3Methods;
                                }
                                else
                                {
                                    buffer[ outIdx ] = TACandle.CdlFalling3Methods;
                                }


                            }
                        }
                    }
                }
                else if ( IsLongCandle( i - 3 ) && IsShortCandle( i - 2 ) && IsShortCandle( i - 1 ) && IsLongCandle( i ) )          // third candle higher                                                     
                {
                    if ( CandleColor( i - 3 ) == -CandleColor( i - 2 ) &&
                         CandleColor( i - 2 ) == CandleColor( i - 1 ) &&
                         CandleColor( i - 1 ) == -CandleColor( i ) )
                    {
                        // 2nd to 4th hold within 1st: a part of the real body must be within 1st range
                        if (
                                Math.Min( Bars[ i - 2 ].Open, Bars[ i - 2 ].Close ) < Bars[ i - 3 ].High &&
                                Math.Max( Bars[ i - 2 ].Open, Bars[ i - 2 ].Close ) > Bars[ i - 3 ].Low &&
                                Math.Min( Bars[ i - 1 ].Open, Bars[ i - 1 ].Close ) < Bars[ i - 3 ].High &&
                                Math.Max( Bars[ i - 1 ].Open, Bars[ i - 1 ].Close ) > Bars[ i - 3 ].Low
                            )
                        {
                            // 2nd to 4th are falling (rising)

                            if ( Bars[ i - 2 ].Close > Bars[ i - 1 ].Close && Bars[ i ].Close > Bars[ i - 3 ].Close )
                            {
                                DateTime? barTime = Bars.GetTimeAtIndex( i );

                                if ( outIdx - 3 >= 0 )
                                {
                                    buffer[ outIdx - 3 ] = TACandle.CdlRising3Methods;
                                    buffer[ outIdx - 2 ] = TACandle.CdlRising3Methods;
                                    buffer[ outIdx - 1 ] = TACandle.CdlRising3Methods;
                                    buffer[ outIdx ] = TACandle.CdlRising3Methods;
                                }
                                else
                                {
                                    buffer[ outIdx ] = TACandle.CdlRising3Methods;
                                }


                            }
                            else if ( Bars[ i - 2 ].Close < Bars[ i - 1 ].Close && Bars[ i ].Close < Bars[ i - 3 ].Close )
                            {
                                DateTime? barTime = Bars.GetTimeAtIndex( i );

                                if ( outIdx - 3 >= 0 )
                                {
                                    buffer[ outIdx - 3 ] = TACandle.CdlFalling3Methods;
                                    buffer[ outIdx - 2 ] = TACandle.CdlFalling3Methods;
                                    buffer[ outIdx - 1 ] = TACandle.CdlFalling3Methods;
                                    buffer[ outIdx ] = TACandle.CdlFalling3Methods;
                                }
                                else
                                {
                                    buffer[ outIdx ] = TACandle.CdlFalling3Methods;
                                }
                            }
                        }
                    }
                }

                outIdx++;
                i++;

            } while ( i <= endIdx );

            outNBElement = outIdx;
            outBegIdx = startIdx;

            return RetCode.Success;
        }


        /// <summary>
        /// /// <image url="$(SolutionDir)\..\..\30 - CommonImages\bullish_ladder_bottom.jpg" />
        /// 
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\concealing-baby-swallow.preview.png" scale="0.4"/>
        /// 
        /// After an established downtrend, three red days with consecutively lower closes occur
        /// The fourth day is a red day with an upper wick. An Inverted Hammer
        /// The fifth day is a blue day
        /// 
        /// The first several days establish a consistent downtrend. As time progresses however shorts take the opportunity to par-off their positions and take profit. 
        /// This is illustrated in the fourth days red Inverted Hammer candle. As prices are bid up, the high is pushed up. 
        /// But in this formation sellers are still able to drive price down to levels nearer the open creating a small body.
        /// 
        /// Up to day-four in the formation this just suggests that sellers are losing firm control of the market. 
        /// By the fifth day, a bullish rally creates the long blue candle. Candlestick analysts would look for buy entry opportunities to come.
        /// </summary>

        public int LadderBottomLookBack()
        {
            return Core.CdlLadderBottomLookback();
        }

        public RetCode LadderBottom5( int startIdx, int endIdx, out int outBegIdx, out int outNBElement, ref TACandle[ ] buffer )
        {
            int i, outIdx, lookbackCount;

            outNBElement = 0;
            outBegIdx = 0;

            if ( startIdx < 0 ) return RetCode.OutOfRangeStartIndex;

            if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return RetCode.OutOfRangeEndIndex;

            lookbackCount = LadderBottomLookBack();

            if ( startIdx < lookbackCount ) startIdx = lookbackCount;

            if ( startIdx > endIdx )
            {
                return RetCode.Success;
            }

            i = startIdx;

            outIdx = 0;

            /* Proceed with the calculation for the requested range.
             * Must have:
             * - three black candlesticks with consecutively lower opens and closes
             * - fourth candle: black candle with an upper shadow (it's supposed to be not very short)
             * - fifth candle: white candle that opens above prior candle's body and closes above prior candle's high
             * 
             * 
             * the user should consider that ladder bottom is significant when it appears in a downtrend, 
             * while this function does not consider it
             */
            do
            {
                if ( IsBlackCandle( i - 4 ) && IsBlackCandle( i - 3 ) && IsBlackCandle( i - 2 ) )                                       // 3 black candlesticks                                                
                {
                    if ( ( Bars[ i - 4 ].Open > Bars[ i - 3 ].Open && Bars[ i - 3 ].Open > Bars[ i - 2 ].Open ) &&                                      // with consecutively lower opens and closes
                         ( Bars[ i - 4 ].Close > Bars[ i - 3 ].Close && Bars[ i - 3 ].Close > Bars[ i - 2 ].Close )
                       )
                    {
                        if ( IsBlackCandle( i - 1 ) &&
                             IsUpperShadowAboveAverage( i - 1 ) )
                        {
                            if ( IsWhiteCandle( i ) &&
                                 Bars[ i ].Close > Bars[ i - 1 ].High )
                            {
                                DateTime? barTime = Bars.GetTimeAtIndex( i );

                                if ( outIdx - 4 >= 0 )
                                {
                                    buffer[ outIdx - 4 ] = TACandle.CdlLadderBottom;
                                    buffer[ outIdx - 3 ] = TACandle.CdlLadderBottom;
                                    buffer[ outIdx - 2 ] = TACandle.CdlLadderBottom;
                                    buffer[ outIdx - 1 ] = TACandle.CdlLadderBottom;
                                    buffer[ outIdx ] = TACandle.CdlLadderBottom;
                                }
                                else
                                {
                                    buffer[ outIdx ] = TACandle.CdlLadderBottom;
                                }
                            }
                        }
                    }
                }

                outIdx++;
                i++;

            } while ( i <= endIdx );

            outNBElement = outIdx;
            outBegIdx = startIdx;

            return RetCode.Success;
        }

        public bool Engulfing( int previous, int current )
        {
            if ( Bars[ current ].Open >= Bars[ previous ].Close &&
                 Bars[ current ].Close < Bars[ previous ].Open ) return true;

            if ( Bars[ current ].Open <= Bars[ previous ].Close &&
                 Bars[ current ].Close > Bars[ previous ].Open ) return true;

            return false;
        }

        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\GraveStoneDoji.jpg" />
		public bool IsGraveStoneDoji( int index )
        {
            return ( IsDojiCandle( index ) && IsUpperShadowLong( index ) && IsLowerShadowVeryShort( index ) );
        }

        public bool IsDragonFlyDoji( int index )
        {
            return ( IsDojiCandle( index ) && IsUpperShadowShort( index ) && IsLowerShadowLong( index ) );
        }

        public bool IsLongLeggedDoji( int index )
        {
            if ( IsDojiCandle( index ) )
            {
                double range    = RangeAsPip( index );

                int i = 0;

                var binCount = _realBodyHistogram.Count( );

                foreach ( var bin in _realBodyHistogram )
                {
                    if ( bin.Range.Max >= range && bin.Range.Min <= range )
                    {
                        if ( i >= binCount - 2 )
                        {
                            return true;
                        }
                    }

                    i++;
                }
            }

            return ( false );
        }




        public bool IsWhiteCandle( int index )
        {
            return ( CandleColor( index ) == 1 );
        }

        public bool IsBlackCandle( int index )
        {
            return ( CandleColor( index ) == -1 );
        }

        public bool IsLongWhiteCandle( int index )
        {
            if ( CandleColor( index ) == 1 && IsLongCandle( index ) )
            {
                return true;
            }
            return false;
        }

        public bool IsAboveAverageWhiteCandle( int index )
        {
            if ( CandleColor( index ) == 1 && IsAboveAverageCandle( index ) )
            {
                return true;
            }
            return false;
        }

        public bool IsAboveAverageBlackCandle( int index )
        {
            if ( CandleColor( index ) == -1 && IsAboveAverageCandle( index ) )
            {
                return true;
            }
            return false;
        }



        public bool IsLongBlackCandle( int index )
        {
            if ( CandleColor( index ) == -1 && IsLongCandle( index ) )
            {
                return true;
            }
            return false;
        }



        public bool IsLongCandle( int index )
        {
            double realBody = RealBodyAsPip( index );

            int i = 0;

            var binCount = _realBodyHistogram.Count( );

            foreach ( var bin in _realBodyHistogram )
            {
                if ( bin.Range.Max >= realBody && bin.Range.Min <= realBody )
                {
                    if ( i >= binCount - 2 )
                    {
                        return true;
                    }
                }

                i++;
            }

            return false;
        }

        public bool IsAboveAverageCandle( int index )
        {
            double realBody = RealBodyAsPip( index );

            if ( realBody > _realBodyHeightAverage )
            {
                return true;
            }

            return false;
        }

        public bool IsAboveAverageRange( int index )
        {
            double range = RangeAsPip( index );

            if ( range > _dataBarRangeAverage )
            {
                return true;
            }

            return false;
        }

        public bool IsUpperShadowAboveAverage( int index )
        {
            double upperShadow = UpperShadowLengthAsPip( index );

            if ( upperShadow > _topShadowAverage )
            {
                return true;
            }

            return false;
        }

        public bool IsVeryLongCandle( int index )
        {
            double realBody = RealBodyAsPip( index );

            int i = 0;

            var binCount = _realBodyHistogram.Count( );

            foreach ( var bin in _realBodyHistogram )
            {
                if ( bin.Range.Max >= realBody && bin.Range.Min <= realBody )
                {
                    if ( i == binCount - 1 )
                    {
                        return true;
                    }
                }

                i++;
            }

            return false;
        }

        public bool IsVeryShortCandle( int index )
        {
            double realBody = RealBodyAsPip( index );

            int i = 0;

            foreach ( var bin in _realBodyHistogram )
            {
                if ( bin.Range.Max >= realBody && bin.Range.Min <= realBody )
                {
                    if ( i == 0 )
                        return true;
                }

                i++;
            }

            return false;
        }

        public bool IsShortCandle( int index )
        {
            double realBody = RealBodyAsPip( index );

            int i = 0;

            foreach ( var bin in _realBodyHistogram )
            {
                if ( bin.Range.Max >= realBody && bin.Range.Min <= realBody )
                {
                    if ( i <= 1 )
                        return true;
                }

                i++;
            }

            return false;
        }

        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\doji.jpg" />
		public bool IsDojiCandle( int index )
        {
            double realBody = RealBodyAsPip( index );
            double range    = RangeAsPip( index );

            if ( realBody <= range * 0.08 ) return true;

            //foreach ( var bin in _realBodyHistogram )
            //{
            //	if ( bin.Range.Max >= realBody && bin.Range.Min <= realBody )
            //	{
            //		if ( bin.RepresentativeValue < -1 )
            //		{
            //			return true;
            //		}
            //		else if ( bin.RepresentativeValue == -1 )
            //		{
            //                     //!+ If after a long range trading session the close is less than 8 percent of the overall high and low, 
            //                     //!+ I consider it a doji
            //			double range        = RangeAsPip( index );

            //		    if ( realBody <= range*0.08 ) return true;
            //		}                    
            //	}
            //}

            return false;
        }

        /// <summary>
        /// 1.  The real body is at the upper end of the trading range; the color (white or black) is not important. 
        /// 2.  The lower part, or the “shadow,” should be at least twice the length of the real body. 
        /// 3.  It should have little or no upper shadow, like a shaved head candle. 
        /// </summary>        
        public bool IsHammerCandle( int index )
        {
            double realBody    = RealBodyAsPip( index );
            double lowerShadow = LowerShadowLengthAsPip( index );
            double upperShadow = UpperShadowLengthAsPip( index );

            if ( IsLongRangleCandle( index ) )
            {
                if ( lowerShadow >= 1.5 * realBody )
                {
                    if ( lowerShadow > 3 * upperShadow ) return true;
                }
            }
            else if ( IsAboveAverageRange( index ) )
            {
                if ( lowerShadow >= 2 * realBody )
                {
                    if ( lowerShadow > 3 * upperShadow ) return true;
                }
            }

            return false;
        }

        private bool IsShootingStar( int index )
        {
            double realBody    = RealBodyAsPip( index );
            double lowerShadow = LowerShadowLengthAsPip( index );
            double upperShadow = UpperShadowLengthAsPip( index );

            if ( IsLongRangleCandle( index ) )
            {
                if ( upperShadow >= 1.5 * realBody )
                {
                    if ( upperShadow > 3 * lowerShadow ) return true;
                }
            }
            else if ( IsAboveAverageRange( index ) )
            {
                if ( upperShadow >= 2 * realBody )
                {
                    if ( upperShadow > 3 * lowerShadow ) return true;
                }
            }

            return false;
        }

        public int GetRealBodyBinLocation( int index )
        {
            double realBody = RealBodyAsPip( index );

            int i = 0;

            foreach ( var bin in _realBodyHistogram )
            {
                if ( bin.Range.Max >= realBody && bin.Range.Min <= realBody )
                {
                    return i;
                }

                i++;
            }

            return -1;
        }

        public bool IsDojiStar( int index )
        {
            double realBody = RealBodyAsPip( index );

            int i = 0;

            foreach ( var bin in _realBodyHistogram )
            {
                if ( bin.Range.Max >= realBody && bin.Range.Min <= realBody )
                {
                    if ( i == 0 )
                    {
                        return true;
                    }
                    else if ( i == 1 )
                    {
                        double uppperShadow = UpperShadowLengthAsPip( index );
                        double lowershadow  = LowerShadowLengthAsPip( index );

                        if ( realBody <= ( uppperShadow + lowershadow ) / 5 ) return true;
                    }
                }

                i++;
            }



            return false;
        }

        public bool IsMarubozuCandle( int index )
        {
            double bodyRange    = RangeAsPip( index );
            double upperShadow = UpperShadowLengthAsPip( index );
            double lowerShadow = LowerShadowLengthAsPip( index );

            if ( IsAboveAverageCandle( index ) )
            {
                if ( IsUpperShadowVeryShort( index ) &&
                     IsLowerShadowVeryShort( index ) )
                {
                    return true;
                }

                if ( ( upperShadow + lowerShadow ) < ( bodyRange / 10 ) )
                {
                    return true;
                }
            }

            return false;
        }


        public bool IsLongerThanShortCandle( int index )
        {
            double realBody = RealBodyAsPip( index );

            int i = 0;

            foreach ( var bin in _realBodyHistogram )
            {
                if ( bin.Range.Max >= realBody && bin.Range.Min <= realBody )
                {
                    if ( i > 1 )
                        return true;
                }

                i++;
            }


            return false;
        }

        public bool IsLongRangleCandle( int index )
        {
            double rangeBody = RangeAsPip( index );

            int i = 0;

            foreach ( var bin in _realBodyHistogram )
            {
                if ( bin.Range.Max >= rangeBody && bin.Range.Min <= rangeBody )
                {
                    if ( i >= 4 )
                        return true;
                }

                i++;
            }

            //foreach ( var bin in _bodyRangeHistogram )
            //{
            //    if ( bin.Range.Max >= rangeBody && bin.Range.Min <= rangeBody )
            //    {
            //        return ( bin.RepresentativeValue >= 1 ) ? true : false;
            //    }
            //}

            return false;
        }

        public bool IsLongFallingCandle( int index )
        {
            if ( CandleColor( index ) == -1 && IsLongCandle( index ) )
            {
                return true;
            }
            return false;
        }

        public bool IsLowerShadowVeryShort( int index )
        {
            double lowerShadow = LowerShadowLengthAsPip( index );

            int i = 0;

            foreach ( var bin in _lowerShadowHistogram )
            {
                if ( bin.Range.Max >= lowerShadow && bin.Range.Min <= lowerShadow )
                {
                    if ( i == 0 )
                        return true;
                }

                i++;
            }

            //         foreach ( var bin in _lowerShadowHistogram )
            //{
            //	if ( bin.Range.Max >= lowerShadow && bin.Range.Min <= lowerShadow )
            //	{
            //		return ( bin.RepresentativeValue < -1 ) ? true : false;                    
            //	}
            //}

            return false;
        }

        public bool IsLowerShadowShort( int index )
        {
            double lowerShadow = LowerShadowLengthAsPip( index );

            int i = 0;

            foreach ( var bin in _lowerShadowHistogram )
            {
                if ( bin.Range.Max >= lowerShadow && bin.Range.Min <= lowerShadow )
                {
                    if ( i < 2 )
                        return true;
                }

                i++;
            }

            //         foreach ( var bin in _lowerShadowHistogram )
            //{
            //	if ( bin.Range.Contains( lowerShadow  ) )
            //	{
            //		return ( bin.RepresentativeValue < 0 ) ? true : false;                    
            //	}
            //}

            return false;
        }



        public bool IsLowerShadowNotLong( int index )
        {
            double lowerShadow = LowerShadowLengthAsPip( index );

            int i = 0;

            foreach ( var bin in _lowerShadowHistogram )
            {
                if ( bin.Range.Max >= lowerShadow && bin.Range.Min <= lowerShadow )
                {
                    if ( i <= 2 )
                        return true;
                }

                i++;
            }

            //         foreach ( var bin in _lowerShadowHistogram )
            //{
            //	if ( bin.Range.Max >= lowerShadow && bin.Range.Min <= lowerShadow )
            //	{
            //		return ( bin.RepresentativeValue <= 0 ) ? true : false;
            //	}
            //}

            return false;
        }

        public bool IsLowerShadowAboveAverage( int index )
        {
            double lowerShadow = LowerShadowLengthAsPip( index );

            if ( lowerShadow > _bottomShadowAverage )
            {
                return true;
            }

            return false;
        }

        public bool IsLowerShadowLong( int index )
        {
            double lowerShadow = LowerShadowLengthAsPip( index );

            int i = 0;

            foreach ( var bin in _lowerShadowHistogram )
            {
                if ( bin.Range.Max >= lowerShadow && bin.Range.Min <= lowerShadow )
                {
                    if ( i > 3 )
                        return true;
                }

                i++;
            }

            //         foreach ( var bin in _lowerShadowHistogram )
            //{
            //	if ( bin.Range.Max >= lowerShadow && bin.Range.Min <= lowerShadow )
            //	{
            //		return ( bin.RepresentativeValue > 0 ) ? true : false;
            //	}
            //}

            return false;
        }


        public bool IsUpperShadowVeryShort( int index )
        {
            double topShadow = UpperShadowLengthAsPip( index );

            int i = 0;

            foreach ( var bin in _topShadowHistogram )
            {
                if ( bin.Range.Max >= topShadow && bin.Range.Min <= topShadow )
                {
                    if ( i == 0 )
                        return true;
                }

                i++;
            }

            //         foreach ( var bin in _topShadowHistogram )
            //{
            //	if ( bin.Range.Max >= topShadow && bin.Range.Min <= topShadow )
            //	{
            //		return ( bin.RepresentativeValue < -1 ) ? true : false;
            //	}
            //}

            return false;
        }

        public bool IsUpperShadowShort( int index )
        {
            double topShadow = UpperShadowLengthAsPip( index );

            int i = 0;

            foreach ( var bin in _topShadowHistogram )
            {
                if ( bin.Range.Max >= topShadow && bin.Range.Min <= topShadow )
                {
                    if ( i < 2 )
                        return true;
                }

                i++;
            }

            //         foreach ( var bin in _topShadowHistogram )
            //{
            //	if ( bin.Range.Max >= topShadow && bin.Range.Min <= topShadow )
            //	{
            //		return ( bin.RepresentativeValue < 0 ) ? true : false;
            //	}
            //}

            return false;
        }


        public bool IsUpperShadowLong( int index )
        {
            double topShadow = UpperShadowLengthAsPip( index );

            int i = 0;

            foreach ( var bin in _topShadowHistogram )
            {
                if ( bin.Range.Max >= topShadow && bin.Range.Min <= topShadow )
                {
                    if ( i >= 3 )
                        return true;
                }

                i++;
            }

            //         foreach ( var bin in _topShadowHistogram )
            //{
            //	if ( bin.Range.Max >= topShadow && bin.Range.Min <= topShadow )
            //	{
            //		return ( bin.RepresentativeValue > 0 ) ? true : false;
            //	}
            //}

            return false;
        }

        public bool IsUpperShadowVeryLong( int index )
        {
            double topShadow = UpperShadowLengthAsPip( index );

            int i = 0;

            foreach ( var bin in _topShadowHistogram )
            {
                if ( bin.Range.Max >= topShadow && bin.Range.Min <= topShadow )
                {
                    if ( i >= 4 )
                        return true;
                }

                i++;
            }

            //         foreach ( var bin in _topShadowHistogram )
            //{
            //	if ( bin.Range.Max >= topShadow && bin.Range.Min <= topShadow )
            //	{
            //		return ( bin.RepresentativeValue > 1 ) ? true : false;
            //	}
            //}

            return false;
        }


        public bool IsUpperShadowNotLong( int index )
        {
            double topShadow = UpperShadowLengthAsPip( index );

            int i = 0;

            foreach ( var bin in _topShadowHistogram )
            {
                if ( bin.Range.Max >= topShadow && bin.Range.Min <= topShadow )
                {
                    if ( i < 3 )
                        return true;
                }

                i++;
            }

            //         foreach ( var bin in _topShadowHistogram )
            //{
            //	if ( bin.Range.Max >= topShadow && bin.Range.Min <= topShadow )
            //	{
            //		return ( bin.RepresentativeValue <= 0 ) ? true : false;
            //	}
            //}

            return false;
        }


        public bool SetCandleSettings( CandleSettingEnum settingType,
                                        RangeEnum rangeType,
                                        int avgPeriod,
                                        double factor )
        {
            if ( settingType >= CandleSettingEnum.AllCandleSettings )
            {
                return false;
            }

            if ( CandleSettings.Count == 0 ) return false;

            if ( CandleSettings.ContainsKey( settingType ) )
            {
                CandleSettings[ settingType ] = new CandleSetting( rangeType, avgPeriod, factor );
            }
            else
            {
                CandleSettings.Add( settingType, new CandleSetting( rangeType, avgPeriod, factor ) );
            }

            return true;
        }


        public int CandleAvgPeriod( CandleSettingEnum input )
        {
            if ( CandleSettings.Count == 0 ) return -1;

            if ( CandleSettings.ContainsKey( input ) )
            {
                CandleSetting value = CandleSettings[ input ];

                return value.AvgPeriod;
            }

            return -1;
        }

        public RangeEnum CandleRangeType( CandleSettingEnum input )
        {
            if ( CandleSettings.Count == 0 ) return RangeEnum.NoAvailable;

            if ( CandleSettings.ContainsKey( input ) )
            {
                CandleSetting value = CandleSettings[ input ];

                return value.RangeType;
            }

            return RangeEnum.NoAvailable;
        }

        public double CandleFactor( CandleSettingEnum input )
        {
            if ( CandleSettings.Count == 0 ) return -1;

            if ( CandleSettings.ContainsKey( input ) )
            {
                CandleSetting value = CandleSettings[ input ];

                return value.Factor;
            }

            return -1;
        }


        public double RealBodyLength( int index )
        {
            if ( index > _barCountBeforeCalculation - 1 || index < 0 ) return -1;

            return ( Math.Abs( Bars[ index ].Open - Bars[ index ].Close ) );
        }

        public double CandleLength( int index )
        {
            if ( index > _barCountBeforeCalculation - 1 || index < 0 ) return -1;

            return ( Math.Abs( Bars[ index ].High - Bars[ index ].Low ) );
        }

        public double RealBodyAsPip( int index )
        {
            if ( _instrumentPointSize != 0 )
            {
                return RealBodyLength( index ) / _instrumentPointSize;
            }

            throw new InvalidProgramException();
        }

        public double RangeAsPip( int index )
        {
            if ( _instrumentPointSize != 0 )
            {
                return CandleLength( index ) / _instrumentPointSize;
            }

            throw new InvalidProgramException();
        }

        public double CandleAverage( CandleSettingEnum input, double sum, int index )
        {
            double candleRange = 0.0;

            candleRange = CandleFactor( input ) * ( ( CandleAvgPeriod( input ) != 0 ) ? sum / CandleAvgPeriod( input ) : CandleRange( input, index ) );

            if ( CandleRangeType( input ) == RangeEnum.Shadows ) candleRange /= 2;

            return candleRange;
        }

        public double CandleRange( CandleSettingEnum range, int index )
        {
            if ( index > _barCountBeforeCalculation - 1 || index < 0 ) return -1;

            RangeEnum rangeEnum =  CandleRangeType( range );

            double candleRange = 0;

            switch ( rangeEnum )
            {
                case RangeEnum.HighLow:
                    candleRange = Bars[ index ].High - Bars[ index ].Low;
                    break;

                case RangeEnum.RealBodyLength:
                    candleRange = Math.Abs( Bars[ index ].Open - Bars[ index ].Close );
                    break;

                case RangeEnum.Shadows:
                    candleRange = UpperShadowLength( index ) + LowerShadowLength( index );
                    break;

            }

            return candleRange;
        }

        public double UpperShadowLength( int index )
        {
            if ( index > _barCountBeforeCalculation - 1 || index < 0 ) return -1;

            var candleClose = Bars[ index ].Close >= Bars[ index ].Open ? Bars[ index ].Close : Bars[ index ].Open;

            return ( Bars[ index ].High - candleClose );
        }


        public double LowerShadowLength( int index )
        {
            if ( index > _barCountBeforeCalculation - 1 || index < 0 ) return -1;

            var candleClose = ( Bars[ index ].Close >= Bars[ index ].Open ? Bars[ index ].Open : Bars[ index ].Close );

            return ( candleClose - Bars[ index ].Low );
        }


        public double UpperShadowLengthAsPip( int index )
        {
            if ( _instrumentPointSize != 0 )
            {
                return UpperShadowLength( index ) / _instrumentPointSize;
            }

            throw new InvalidProgramException();
        }

        

        

        public double LowerShadowLengthAsPip( int index )
        {
            if ( _instrumentPointSize != 0 )
            {
                return LowerShadowLength( index ) / _instrumentPointSize;
            }

            throw new InvalidProgramException();
        }

        public double HighLowRange( int index )
        {
            if ( index > _barCountBeforeCalculation - 1 || index < 0 ) return -1;

            return ( Bars[ index ].High - Bars[ index ].Low );
        }

        public bool IsRising( int index )
        {
            return ( Bars[ index ].Close >= Bars[ index ].Open );
        }

        public bool IsFalling( int index )
        {
            return ( Bars[ index ].Close < Bars[ index ].Open );
        }

        public bool IsRealBodyGapUp( int index2, int index1 )
        {
            double index2Min = Math.Min( Bars[ index2 ].Open, Bars[ index2 ] .Close);
            double index1Max = Math.Max( Bars[ index1 ].Open, Bars[ index1 ] .Close);

            return ( index2Min >= index1Max );
        }

        public bool IsRealBodyGapDown( int index2, int index1 )
        {
            double index2Max = Math.Max( Bars[ index2 ].Open, Bars[ index2 ] .Close);
            double index1Min = Math.Min( Bars[ index1 ].Open, Bars[ index1 ] .Close);

            return ( index2Max <= index1Min );
        }

        public bool IsCandleGapUp( int index2, int index1 )
        {
            return ( Bars[ index2 ].Low >= Bars[ index1 ].High );
        }

        public bool IsCandleGapDown( int index2, int index1 )
        {
            return ( Bars[ index2 ].High <= Bars[ index1 ].Low );
        }

        public int CandleColor( int index )
        {
            return ( Bars[ index ].Close > Bars[ index ].Open ? 1 : -1 );
        }


    }
}