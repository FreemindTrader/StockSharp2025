using fx.Common;
using fx.Bars;
using StockSharp.BusinessEntities;

using DevExpress.Mvvm;

using fx.TimePeriod;
using fx.Definitions;
using fx.Algorithm;
using System;
using fx.Collections;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using fx.TALib;
using System.Collections.Generic;

#pragma warning disable 414

namespace fx.Indicators
{
    public partial class FreemindIndicator : CustomPlatformIndicator
    {

        private void DetectCandleSticksFromZigZag( bool fullRecalculation, DataBarUpdateType? updateType, int barsCountBeforeCalculation )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return;
            }

            //ThreadHelper.UpdateThreadName( "DetectCandleSticksFromZigZag" );

            var symbol = Bars.Security;
            var period = Bars.Period.Value;

            if ( _hews != null )
            {
                var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );
                if ( aa == null )
                    return;

                var taManager = ( PeriodXTaManager )aa.GetPeriodXTa( period );

                var waveImportance       = _hews.GetAscendingWaveImportanceClone(period);

                int waveImportanceByTime = GetWaveImportanceForCandlestickByTime( period );

                /* --------------------------------------------------------------------------------------------------------------------------
                 *              
                 * 1.   Any long as there is any major turning points, we will check for CandleStick Formation, Waiting for cross over 
                 *      might be too slow.
                 * 
                 * --------------------------------------------------------------------------------------------------------------------------
                */
                var waves = waveImportance.Where(   x =>
                                                    {
                                                        if ( x.Value.WaveImportance >= waveImportanceByTime )
                                                        {
                                                            ref SBar bar = ref Bars.GetBarByTime( x.Key );

                                                            if ( bar == SBar.EmptySBar  )
                                                                return false;

                                                            
                                                            if ( bar.Index > _lastCandleCheckIndex )
                                                            {
                                                                if ( ( int )bar.BarIndex < _lastMacdCrossIndex )
                                                                {
                                                                    return true;
                                                                }
                                                            }
                                                        }

                                                        return false;
                                                    }
                                                );

                foreach ( KeyValuePair<long, WavePointImportance> wave in waves )
                {
                    if ( wave.Value.Signal == TASignal.WAVE_PEAK )
                    {
                        FindCandlestickPatternForPeakBar( symbol, wave, taManager, barsCountBeforeCalculation );
                    }
                    // Downtrend
                    else if ( wave.Value.Signal == TASignal.WAVE_TROUGH )
                    {
                        FindCandlestickPatternForTroughBar( symbol, wave, taManager, barsCountBeforeCalculation );
                    }
                }
            }
        }



        private void FindCandlestickPatternForTroughBar( Security symbol, KeyValuePair<long, WavePointImportance> wave, PeriodXTaManager taManager, int barB4Calculation )
        {
            ref SBar troughBar = ref Bars.GetBarByTime(  wave.Key );
            var barIndex  = troughBar.Index;

            if ( troughBar.LinuxTime == 1578440700000 && troughBar.BarPeriod == TimeSpan.FromMinutes( 5 ) )
            {

            }

            CandleFormation candleFormation = null;

            if ( troughBar != SBar.EmptySBar )
            {
                int startIndex         = barIndex;

                if ( _fxTradingEventsBindingList == null )
                {
                    var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

                    if ( aa == null )
                        return;

                    _fxTradingEventsBindingList = aa.TradingEventsBindingList;
                }


                if ( ( candleFormation = TriStarsAtTrough( barB4Calculation, startIndex, ( _lastCandleCheckIndex = ( barIndex + 3 < barB4Calculation ) ? barIndex + 3 : barB4Calculation - 1 ) ) ) != null )
                {
                    Bars.ApplyPatternsToBars( candleFormation );

                    _fxTradingEventsBindingList.AddCandleValue( Bars.Period.Value, barIndex, candleFormation, barB4Calculation );
                }
                else if ( ( candleFormation = MorningStarAtTrough( barB4Calculation, startIndex, ( _lastCandleCheckIndex = ( barIndex + 3 < barB4Calculation ) ? barIndex + 3 : barB4Calculation - 1 ) ) ) != null )
                {
                    Bars.ApplyPatternsToBars( candleFormation );

                    _fxTradingEventsBindingList.AddCandleValue( Bars.Period.Value, barIndex, candleFormation, barB4Calculation );
                }
                else if ( ( candleFormation = BullishEngulfingAtTrough( barB4Calculation, startIndex, ( _lastCandleCheckIndex = ( barIndex + 1 < barB4Calculation ) ? barIndex + 1 : barB4Calculation - 1 ) ) ) != null )
                {
                    Bars.ApplyPatternsToBars( candleFormation );

                    _fxTradingEventsBindingList.AddCandleValue( Bars.Period.Value, barIndex, candleFormation, barB4Calculation );
                }
                else if ( ( candleFormation = PiercingAtTrough( barB4Calculation, startIndex, ( _lastCandleCheckIndex = ( barIndex + 1 < barB4Calculation ) ? barIndex + 1 : barB4Calculation - 1 ) ) ) != null )
                {
                    Bars.ApplyPatternsToBars( candleFormation );

                    _fxTradingEventsBindingList.AddCandleValue( Bars.Period.Value, barIndex, candleFormation, barB4Calculation );
                }

            }


        }

        private void FindCandlestickPatternForPeakBar( Security symbol, KeyValuePair<long, WavePointImportance> wave, PeriodXTaManager taManager, int barB4Calculation )
        {
            ref SBar peakBar = ref Bars.GetBarByTime(  wave.Key );
            var barIndex = peakBar.Index;

            CandleFormation candleFormation = null;

            if ( peakBar != SBar.EmptySBar )
            {
                int startIndex         = barIndex;

                if ( _fxTradingEventsBindingList == null )
                {
                    var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

                    if ( aa == null )
                        return;

                    _fxTradingEventsBindingList = aa.TradingEventsBindingList;
                }

                if ( ( candleFormation = TriStarsAtPeak( barB4Calculation, startIndex, ( _lastCandleCheckIndex = ( barIndex + 3 < barB4Calculation ) ? barIndex + 3 : barB4Calculation - 1 ) ) ) != null )
                {
                    Bars.ApplyPatternsToBars( candleFormation );

                    _fxTradingEventsBindingList.AddCandleValue( Bars.Period.Value, barIndex, candleFormation, barB4Calculation );
                }
                else if ( ( candleFormation = EveningStarAtPeak( barB4Calculation, startIndex, ( _lastCandleCheckIndex = ( barIndex + 3 < barB4Calculation ) ? barIndex + 3 : barB4Calculation - 1 ) ) ) != null )
                {
                    Bars.ApplyPatternsToBars( candleFormation );

                    _fxTradingEventsBindingList.AddCandleValue( Bars.Period.Value, barIndex, candleFormation, barB4Calculation );
                }
                else if ( ( candleFormation = BearishEngulfingAtPeak( barB4Calculation, startIndex, ( _lastCandleCheckIndex = ( barIndex + 1 < barB4Calculation ) ? barIndex + 1 : barB4Calculation - 1 ) ) ) != null )
                {
                    Bars.ApplyPatternsToBars( candleFormation );

                    _fxTradingEventsBindingList.AddCandleValue( Bars.Period.Value, barIndex, candleFormation, barB4Calculation );
                }
                else if ( ( candleFormation = DarkCloudCoverAtPeak( barB4Calculation, startIndex, ( _lastCandleCheckIndex = ( barIndex + 1 < barB4Calculation ) ? barIndex + 1 : barB4Calculation - 1 ) ) ) != null )
                {
                    Bars.ApplyPatternsToBars( candleFormation );

                    _fxTradingEventsBindingList.AddCandleValue( Bars.Period.Value, barIndex, candleFormation, barB4Calculation );
                }
            }
        }

        private int GetWaveImportanceForCandlestickByTime( TimeSpan period )
        {
            if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                return 34;
            }
            else if ( period == TimeSpan.FromMinutes( 4 ) )
            {
                return 21;
            }
            else if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                return 21;
            }
            else if ( period == TimeSpan.FromMinutes( 15 ) )
            {
                return 13;
            }
            else if ( period == TimeSpan.FromMinutes( 30 ) )
            {
                return 13;
            }
            else if ( period >= TimeSpan.FromHours( 1 ) )
            {
                return 8;
            }
            else if ( period >= TimeSpan.FromDays( 1 ) )
            {
                return 5;
            }


            return 89;
        }

        #region Candle Sticks Pattern

        /// <summary>
		/// <image url="$(SolutionDir)\..\..\30 - CommonImages\bullish_tri_star.jpg"/>
		/// After an established downtrend
		/// There dojis (open and close at identical price) occur on consecutive trading days
		/// 
		/// In a long bearish market the strength of trend shows weakness as candle bodies grow progressively smaller, eventually forming three consecutive dojis.
		/// 
		/// A doji candle reveals market indecision, since neither buyers nor sellers prove able to move the close price away from the open. 
		/// This kind of price action is quite common during periods with limited market activity like holidays. 
		/// But after a protracted downtrend or during periods of high trading volume a number of dojis can suggest a reversal in market trend.
		/// 
		/// Realistically translating the same price-action from non-FX to Forex Markets would allow some leeway in the appearance of the three dojis, 
		/// possibly morphing them into star candles with limited ranges.
		/// </summary>

        public CandleFormation TriStarsAtTrough( int barB4Calculation, int startIdx, int endIdx )
        {
            int i, outIdx, lookbackCount;

            if ( startIdx < 0 ) return null;

            if ( ( endIdx < 0 ) || ( ( endIdx + 1 ) < startIdx ) ) return null;

            lookbackCount = Core.BbandsLookback( Core.MAType.Ema, 20 );

            if ( startIdx < lookbackCount ) startIdx = lookbackCount;

            if ( startIdx > endIdx )
            {
                return null;
            }

            CandleFormation output = null;

            i = startIdx;

            outIdx = 0;

            do
            {
                if (
                        ( IsDojiCandle( i - 3 ) && IsDojiCandle( i - 2 ) && IsDojiCandle( i - 1 ) && IsDojiCandle( i ) ) ||
                        ( IsDojiStar( i - 3 ) && IsDojiStar( i - 2 ) && IsDojiStar( i - 1 ) && IsDojiStar( i ) )
                   )
                {
                    if ( i + 1 < endIdx )
                    {
                        output = new CandleFormation( _currentPeriod, TACandle.CdlTriStarBull, 0, i - 3, i );

                        if ( CandleColor( i - 4 ) == -CandleColor( i + 1 ) )
                        {
                            var priBin = GetRealBodyBinLocation( i - 4 );
                            var curBin = GetRealBodyBinLocation( i + 1 );

                            output.Strength = ( priBin + curBin );
                        }
                    }
                }
                else if (
                            ( IsDojiCandle( i - 2 ) && IsDojiCandle( i - 1 ) && IsDojiCandle( i ) ) ||                                      // They are opposite color                                                
                            ( IsDojiStar( i - 2 ) && IsDojiStar( i - 1 ) && IsDojiStar( i ) )
                        )
                {
                    if ( i + 1 < endIdx )
                    {
                        output = new CandleFormation( _currentPeriod, TACandle.CdlTriStarBull, 0, i - 2, i );

                        if ( CandleColor( i - 2 ) == -CandleColor( i + 1 ) )
                        {
                            var priBin = GetRealBodyBinLocation( i - 2 );
                            var curBin = GetRealBodyBinLocation( i + 1 );

                            output.Strength = ( priBin + curBin );
                        }
                    }
                }

                outIdx++;
                i++;

            } while ( ( i + 1 ) < endIdx );


            return output;
        }


        public CandleFormation PiercingAtTrough( int barB4Calculation, int startIdx, int endIdx )
        {
            int i, outIdx, lockbackcount;

            if ( startIdx < 0 ) return null;

            if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return null;

            lockbackcount = PiercingOrDarkCloudCoverLookBack();

            if ( startIdx < lockbackcount ) startIdx = lockbackcount;

            if ( startIdx > endIdx )
            {
                return null;
            }

            CandleFormation output = null;
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
                        output = new CandleFormation( _currentPeriod, TACandle.CdlPiercing, 0, i - 1, i );

                        double innerBBLower  = IndicatorResult[ "InnerBBLower" ][ i - 1 ];
                        double outerBBLower  = IndicatorResult[ "OuterBBLower" ][ i - 1 ];

                        if ( Bars[ i - 1 ].Close <= outerBBLower )
                        {
                            var priBin      = GetRealBodyBinLocation( i - 1 );
                            var curBin      = GetRealBodyBinLocation( i );

                            output.Strength = ( priBin + curBin );
                        }
                        else if ( ( Bars[ i - 1 ].Low <= outerBBLower ) &&
                                  ( Bars[ i - 1 ].High >= innerBBLower ) )
                        {
                            var priBin      = GetRealBodyBinLocation( i - 1 );
                            var curBin      = GetRealBodyBinLocation( i );

                            output.Strength = ( priBin + curBin );
                        }
                        else if ( ( Bars[ i - 1 ].Close - outerBBLower ) < ( innerBBLower - Bars[ i - 1 ].Close ) )
                        {
                            var priBin      = GetRealBodyBinLocation( i - 1 );
                            var curBin      = GetRealBodyBinLocation( i );

                            output.Strength = ( priBin + curBin );
                        }
                    }
                }

                i++;
                outIdx++;

            } while ( i <= endIdx );

            return output;
        }

        public CandleFormation DarkCloudCoverAtPeak( int barB4Calculation, int startIdx, int endIdx )
        {
            int i, outIdx, lockbackcount;

            if ( startIdx < 0 ) return null;

            if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return null;

            lockbackcount = PiercingOrDarkCloudCoverLookBack();

            if ( startIdx < lockbackcount ) startIdx = lockbackcount;

            if ( startIdx > endIdx )
            {
                return null;
            }

            CandleFormation output = null;
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

                    if ( Bars[ i ].Close > Bars[ i - 1 ].Open && Bars[ i ].Close < Bars[ i - 1 ].Close - RealBodyLength( i - 1 ) * 0.5 )
                    {
                        output = new CandleFormation( _currentPeriod, TACandle.CdlDarkCloudCover, 0, i - 1, i );

                        double innerBBUpper  = IndicatorResult[ "InnerBBUpper" ][ i - 1 ];
                        double outerBBUpper  = IndicatorResult[ "OuterBBUpper" ][ i - 1 ];

                        if ( Bars[ i - 1 ].Close >= outerBBUpper )
                        {
                            var priBin      = GetRealBodyBinLocation( i - 1 );
                            var curBin      = GetRealBodyBinLocation( i );

                            output.Strength = ( priBin + curBin );
                        }
                        else if ( ( Bars[ i - 1 ].High >= outerBBUpper ) &&
                                  ( Bars[ i - 1 ].Low <= innerBBUpper ) )
                        {
                            var priBin      = GetRealBodyBinLocation( i - 1 );
                            var curBin      = GetRealBodyBinLocation( i );

                            output.Strength = ( priBin + curBin );
                        }
                        else if ( ( outerBBUpper - Bars[ i - 1 ].Close ) < ( Bars[ i - 1 ].Close - innerBBUpper ) )
                        {
                            var priBin      = GetRealBodyBinLocation( i - 1 );
                            var curBin      = GetRealBodyBinLocation( i );

                            output.Strength = ( priBin + curBin );
                        }

                    }
                }

                i++;
                outIdx++;

            } while ( i <= endIdx );

            return output;
        }



        public CandleFormation BullishEngulfingAtTrough( int barB4Calculation, int startIdx, int endIdx )
        {
            int i, outIdx, lookbackCount;

            if ( startIdx < 0 ) return null;

            if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return null;

            lookbackCount = EngulfingLookBack();

            if ( startIdx < lookbackCount ) startIdx = lookbackCount;

            if ( startIdx > endIdx )
            {
                return null;
            }

            CandleFormation output = null;

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
                    output = new CandleFormation( _currentPeriod, TACandle.CdlEngulfingBull, 0, i - 1, i );

                    var priBin = GetRealBodyBinLocation( i - 1 );
                    var curBin = GetRealBodyBinLocation( i );

                    output.Strength = ( priBin + curBin );

                }

                outIdx++;
                i++;

            } while ( i <= endIdx );

            return output;
        }

        public CandleFormation BearishEngulfingAtPeak( int barB4Calculation, int startIdx, int endIdx )
        {
            int i, outIdx, lookbackCount;

            if ( startIdx < 0 ) return null;

            if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return null;

            lookbackCount = EngulfingLookBack();

            if ( startIdx < lookbackCount ) startIdx = lookbackCount;

            if ( startIdx > endIdx )
            {
                return null;
            }

            CandleFormation output = null;

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
                if ( ( IsWhiteCandle( i - 1 ) && IsBlackCandle( i ) &&
                       ( Bars[ i ].Close < Bars[ i - 1 ].Open ) ||
                       ( Bars[ i ].Close == Bars[ i - 1 ].Open && ( Bars[ i ].Low < Bars[ i - 1 ].Low || Bars[ i ].High > Bars[ i - 1 ].High ) ) ) )                                                                                                                                                                    // black engulfs white                          
                {
                    output = new CandleFormation( _currentPeriod, TACandle.CdlEngulfingBear, 0, i - 1, i );

                    var priBin = GetRealBodyBinLocation( i - 1 );
                    var curBin = GetRealBodyBinLocation( i );

                    output.Strength = ( priBin + curBin );

                }

                outIdx++;
                i++;

            } while ( i <= endIdx );

            return output;
        }

        public CandleFormation MorningStarAtTrough( int barB4Calculation, int startIdx, int endIdx )
        {
            int i, outIdx, lookbackCount;

            if ( startIdx < 0 ) return null;

            if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return null;

            lookbackCount = MorningStarOrDojiLookback();

            if ( startIdx < lookbackCount ) startIdx = lookbackCount;

            if ( startIdx > endIdx )
            {
                return null;
            }

            i = startIdx;

            CandleFormation output = null;

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

                    output = new CandleFormation( _currentPeriod, TACandle.CdlMorningStar, 0, i - 3, i );

                    var priBin = GetRealBodyBinLocation( i - 3 );
                    var curBin = GetRealBodyBinLocation( i );

                    output.Strength = ( priBin + curBin );

                    if ( IsDojiCandle( i - 1 ) )
                    {
                        output.Strength += 2;
                    }
                    else if ( IsDojiStar( i - 1 ) )
                    {
                        output.Strength += 1;
                    }

                    if ( IsDojiCandle( i - 2 ) )
                    {
                        output.Strength += 2;
                    }
                    else if ( IsDojiStar( i - 2 ) )
                    {
                        output.Strength += 1;
                    }
                }
                else if ( (
                                IsBlackCandle( i - 2 ) &&
                                IsAboveAverageCandle( i - 2 ) &&                  // 1st: long                                               
                                Bars[ i - 1 ].High < Bars[ i - 2 ].High &&
                                IsShortCandle( i - 1 ) &&
                                IsLongerThanShortCandle( i ) &&
                                IsWhiteCandle( i )
                             )
                         ||
                            (
                                IsBlackCandle( i - 2 ) &&
                                IsLongerThanShortCandle( i - 2 ) &&                  // 1st: long                                               
                                Bars[ i - 1 ].High < Bars[ i - 2 ].High &&
                                IsShortCandle( i - 1 ) &&
                                IsAboveAverageCandle( i ) &&
                                 IsWhiteCandle( i )
                             )
                        )
                {
                    output = new CandleFormation( _currentPeriod, TACandle.CdlMorningStar, 0, i - 2, i );

                    var priBin = GetRealBodyBinLocation( i - 2 );
                    var curBin = GetRealBodyBinLocation( i );

                    output.Strength = ( priBin + curBin );

                    if ( IsDojiCandle( i - 1 ) )
                    {
                        output.Strength += 2;
                    }
                    else if ( IsDojiStar( i - 1 ) )
                    {
                        output.Strength += 1;
                    }
                }


                /* add the current range and subtract the first range: this is done after the pattern recognition 
				 * when avgPeriod is not 0, that means "compare with the previous candles" (it excludes the current candle)
				 */
                i++;
                outIdx++;

            } while ( i <= endIdx );



            return output;
        }





        public CandleFormation TriStarsAtPeak( int barB4Calculation, int startIdx, int endIdx )
        {
            int i, outIdx, lookbackCount;

            if ( startIdx < 0 ) return null;

            if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return null;

            lookbackCount = Core.BbandsLookback( Core.MAType.Ema, 20 );

            if ( startIdx < lookbackCount ) startIdx = lookbackCount;

            if ( startIdx > endIdx )
            {
                return null;
            }

            CandleFormation output = null;

            i = startIdx;

            outIdx = 0;

            //bool found = false;

            /* Proceed with the calculation for the requested range.
			 * Must have:
			 * - first candle:      marubozu
			 * - second candle:     opposite color marubozu
			 * -                    upside (downside) gap between the first and the second real bodies
			 * - gap between the two candles: upside gap if black then white, downside gap if white then black
			 *              
			 */
            do
            {
                double bbMean  = IndicatorResult[ "BBMean" ][ i ];

                if (
                        ( IsDojiCandle( i - 3 ) && IsDojiCandle( i - 2 ) && IsDojiCandle( i - 1 ) && IsDojiCandle( i ) ) ||
                        ( IsDojiStar( i - 3 ) && IsDojiStar( i - 2 ) && IsDojiStar( i - 1 ) && IsDojiStar( i ) )
                   )                                       // They are opposite color                                                
                {

                    output = new CandleFormation( _currentPeriod, TACandle.CdlTriStarBear, 0, i - 3, i );

                    if ( ( i + 1 ) < endIdx && CandleColor( i - 4 ) == -CandleColor( i + 1 ) )
                    {
                        var priBin = GetRealBodyBinLocation( i - 4 );
                        var curBin = GetRealBodyBinLocation( i + 1 );

                        output.Strength = ( priBin + curBin );
                    }
                }
                else if (
                            ( IsDojiCandle( i - 2 ) && IsDojiCandle( i - 1 ) && IsDojiCandle( i ) ) ||                                      // They are opposite color                                                
                            ( IsDojiStar( i - 2 ) && IsDojiStar( i - 1 ) && IsDojiStar( i ) )
                        )
                {
                    output = new CandleFormation( _currentPeriod, TACandle.CdlTriStarBull, 0, i - 2, i );

                    if ( ( i + 1 ) < endIdx && CandleColor( i - 2 ) == -CandleColor( i + 1 ) )
                    {
                        var priBin = GetRealBodyBinLocation( i - 2 );
                        var curBin = GetRealBodyBinLocation( i + 1 );

                        output.Strength = ( priBin + curBin );
                    }
                }

                outIdx++;
                i++;

            } while ( ( i + 1 ) <= endIdx );


            return output;
        }


        public CandleFormation EveningStarAtPeak( int barB4Calculation, int startIdx, int endIdx )
        {
            int i, outIdx, lookbackCount;

            if ( startIdx < 0 ) return null;

            if ( ( endIdx < 0 ) || ( endIdx < startIdx ) ) return null;

            lookbackCount = MorningStarOrDojiLookback();

            if ( startIdx < lookbackCount ) startIdx = lookbackCount;

            if ( startIdx > endIdx )
            {
                return null;
            }

            i = startIdx;

            CandleFormation output = null;

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

                    output = new CandleFormation( _currentPeriod, TACandle.CdlEveningStar, 0, i - 3, i );

                    var priBin = GetRealBodyBinLocation( i - 3 );
                    var curBin = GetRealBodyBinLocation( i );

                    output.Strength = ( priBin + curBin );

                    if ( IsDojiCandle( i - 1 ) )
                    {
                        output.Strength += 2;
                    }
                    else if ( IsDojiStar( i - 1 ) )
                    {
                        output.Strength += 1;
                    }

                    if ( IsDojiCandle( i - 2 ) )
                    {
                        output.Strength += 2;
                    }
                    else if ( IsDojiStar( i - 2 ) )
                    {
                        output.Strength += 1;
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
                    output = new CandleFormation( _currentPeriod, TACandle.CdlEveningStar, 0, i - 2, i );

                    var priBin = GetRealBodyBinLocation( i - 2 );
                    var curBin = GetRealBodyBinLocation( i );

                    output.Strength = ( priBin + curBin );

                    if ( IsDojiCandle( i - 1 ) )
                    {
                        output.Strength += 2;
                    }
                    else if ( IsDojiStar( i - 1 ) )
                    {
                        output.Strength += 1;
                    }
                }


                /* add the current range and subtract the first range: this is done after the pattern recognition 
				 * when avgPeriod is not 0, that means "compare with the previous candles" (it excludes the current candle)
				 */
                i++;
                outIdx++;

            } while ( i <= endIdx );



            return output;
        }

        #endregion  
    }
}
