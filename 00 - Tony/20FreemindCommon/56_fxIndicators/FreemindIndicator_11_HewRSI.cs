using System;
using fx.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using fx.Algorithm;
using System.Threading;
using System.Threading.Tasks;
using fx.TALib;
using fx.Definitions;
using System.Data;

using fx.Common;
using fx.Bars;

#pragma warning disable 414

namespace fx.Indicators
{
    public partial class FreemindIndicator : CustomPlatformIndicator
    {
        private int _hewRsiLookback = -1;

        private double _latestRsi = 0.0;
        protected Task BasicHewRsiTask( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount, ref PooledList<Task> tasksList )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            //ThreadHelper.UpdateThreadName( "BasicHewRsiTask" );
            
            Task first = new Task(() => CalculateHewRsi( fullRecalculation, updateType, curIterationBarcount ), IndicatorExitToken);
            Task second = first.ContinueWith(
                                                antecedent =>
                                                {
                                                    if (antecedent.Status == TaskStatus.Faulted)
                                                    {
                                                        this.LogError("CalculateOscillator() caused Exception. Stack = " + antecedent.Exception.InnerException.StackTrace);
                                                    }
                                                    else
                                                    {
                                                        GetHewRsiOverBoughtOverSold( fullRecalculation, updateType, curIterationBarcount );
                                                    }

                                                }, IndicatorExitToken);

            tasksList.Add( first );
            tasksList.Add( second );

            return first;
        }

        protected void CalculateHewRsi( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount )
        {
            _hewRsiLookback = Core.RsiLookback( 14 );

            int resultSetLength   = IndicatorResult["FreemindRsi"].Count;

            if ( resultSetLength == 0 )
            {
                ProcessNewIndicatorBuffer( curIterationBarcount );
            }
            else
            {
                ProcessExistingBuffer( updateType, curIterationBarcount, resultSetLength );
            }
        }

        private void ProcessNewIndicatorBuffer( int barB4Calculation )
        {
            int startIndex    = 0;
            int endIndex      = barB4Calculation - 1;
            int indexCount    = endIndex - startIndex + 1;

            var RS            = new double [ indexCount ];

            var fixedCount    = Math.Max( indexCount, IndicatorResult.SetLength );

            var overSold      = MathHelper.CreateFixedLineResultLength(30, fixedCount);
            var overBought    = MathHelper.CreateFixedLineResultLength(70, fixedCount);

            var outBeginIdx   = 0;
            var outNBElement = 0;

            HewRsi( startIndex, endIndex, Bars, 14, out outBeginIdx, out outNBElement, RS );

            lock ( IndicatorResult )
            {
                IndicatorResult.AddSetValues( "FreemindRsi", outBeginIdx, outNBElement, true, RS );
                IndicatorResult.AddSetValues( "RsiOverBought", outBeginIdx, outNBElement, true, overBought );
                IndicatorResult.AddSetValues( "RsiOverSold", outBeginIdx, outNBElement, true, overSold );
            }

            if ( _fxTradingEventsBindingList == null )
            {
                var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

                if ( aa == null )
                    return;

                _fxTradingEventsBindingList = aa.TradingEventsBindingList;
            }

            _fxTradingEventsBindingList.AddHewRsiValue( Bars.Period.Value, barB4Calculation, ( int )_latestRsi, barB4Calculation );
        }

        private void ProcessExistingBuffer( DataBarUpdateType? updateType, int barB4Calculation, int resultSetLength )
        {
            var startIndex = resultSetLength - 1;
            var endIndex   = barB4Calculation - 1;
            var indexCount = endIndex - startIndex + 1;

            if ( endIndex < 0 || indexCount < 0 )
            {
                return;
            }

            int outBeginIdx       = 0;
            int outNBElement      = 0;

            var RS            = new double [ indexCount ];

            var overSold      = MathHelper.CreateFixedLineResultLength(30, indexCount);
            var overBought    = MathHelper.CreateFixedLineResultLength(70, indexCount);

            HewRsi( startIndex, endIndex, Bars, 14, out outBeginIdx, out outNBElement, RS );

            lock ( IndicatorResult )
            {
                IndicatorResult.AddSetValues( "FreemindRsi",   outBeginIdx, outNBElement, true, RS );
                IndicatorResult.AddSetValues( "RsiOverBought", outBeginIdx, outNBElement, true, overBought );
                IndicatorResult.AddSetValues( "RsiOverSold",   outBeginIdx, outNBElement, true, overSold );
            }

            if ( _fxTradingEventsBindingList == null )
            {
                var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

                if ( aa == null )
                    return;

                _fxTradingEventsBindingList = aa.TradingEventsBindingList;
            }

            _fxTradingEventsBindingList.AddHewRsiValue( Bars.Period.Value, barB4Calculation, ( int )_latestRsi, barB4Calculation );
        }

        public IndicatorReturn HewRsi( int startIdx, int endIdx, IHistoricBarsRepo bars, int optInTimePeriod, out int outBegIdx, out int outNBElement, double[ ] outReal )
        {
            outNBElement = 0;
            outBegIdx = 0;

            int outIdx;

            int today, lookbackTotal, unstablePeriod, i;
            double prevGain, prevLoss;
            double previousHigh, previousLow, currentHigh, currentLow;

            double lowDiff, highDiff;

            if ( startIdx < 0 )
            {
                return IndicatorReturn.OutOfRangeStartIndex;
            }

            if ( ( endIdx < 0 ) || ( endIdx < startIdx ) )
            {
                return IndicatorReturn.OutOfRangeEndIndex;
            }


            if ( optInTimePeriod <= 0 )
            {
                optInTimePeriod = 14;
            }
            else if ( ( optInTimePeriod < 2 ) || ( optInTimePeriod > 100000 ) )
            {
                return IndicatorReturn.BadParam;
            }

            if ( outReal == null )
            {
                return IndicatorReturn.BadParam;
            }

            lookbackTotal = Core.RsiLookback( 14 );

            if ( startIdx < lookbackTotal )
            {
                startIdx = lookbackTotal;
            }

            if ( startIdx > endIdx )
            {
                return IndicatorReturn.Success;
            }

            unstablePeriod = ( int )Core.GetUnstablePeriod( Core.FuncUnstId.Rsi );

            outIdx = 0;

            today = startIdx - lookbackTotal;
            previousHigh = bars[ today ].High;
            previousLow = bars[ today ].Low;

            prevGain = 0.0;
            prevLoss = 0.0;

            today++;

            for ( i = optInTimePeriod; i > 0; i-- )
            {
                currentHigh = bars[ today ].High;
                currentLow = bars[ today ].Low;
                highDiff = currentHigh - previousHigh;
                lowDiff = previousLow - currentLow;
                previousHigh = currentHigh;
                previousLow = currentLow;
                today++;

                if ( ( highDiff > 0 ) && ( highDiff > lowDiff ) )
                {
                    prevGain = prevGain + highDiff;
                }

                if ( ( lowDiff > 0 ) && ( lowDiff > highDiff ) )
                {
                    prevLoss = prevLoss + lowDiff;
                }
            }

            /* Subsequent DownSum and UpSum are smoothed
                * using the previous values (Wilder's approach).
                *  1) Multiply the previous by 'period-1'. 
                *  2) Add barIndex value.
                *  3) Divide by 'period'.
                */
            prevLoss /= optInTimePeriod;
            prevGain /= optInTimePeriod;

            /* Often documentation present the RSI calculation as follow:
             *    RSI = 100 - (100 / 1 + (UpSum/DownSum))
             *
             * The following is equivalent:
             *    RSI = 100 * (UpSum/(UpSum+DownSum))
             *
             * The second equation is used here for speed optimization.
             */
            if ( today > startIdx )
            {
                if ( prevGain + prevLoss != 0 )
                {
                    outReal[ outIdx++ ] = 100.0 * ( prevGain / ( prevGain + prevLoss ) );
                }
                else
                {
                    outReal[ outIdx++ ] = 0.0;
                }
            }
            else
            {
                /* Skip the unstable period. Do the processing 
                 * but do not write it in the output.
                 */
                while ( today < startIdx )
                {
                    currentHigh  = bars[ today ].High;
                    currentLow   = bars[ today ].Low;
                    highDiff     = currentHigh - previousHigh;
                    lowDiff      = previousLow - currentLow;
                    previousHigh = currentHigh;
                    previousLow  = currentLow;


                    // This is very important. This is equivalent to dropping the first element of past optInTimePeriod for averaging
                    prevLoss *= ( optInTimePeriod - 1 );
                    prevGain *= ( optInTimePeriod - 1 );


                    if ( ( highDiff > 0 ) && ( highDiff > lowDiff ) )
                    {
                        prevGain = prevGain + highDiff;
                    }

                    if ( ( lowDiff > 0 ) && ( lowDiff > highDiff ) )
                    {
                        prevLoss = prevLoss + lowDiff;
                    }

                    prevLoss /= optInTimePeriod;
                    prevGain /= optInTimePeriod;

                    today++;
                }
            }

            /* Unstable period skipped... now continue
             * processing if needed.
             */
            while ( today <= endIdx )
            {
                currentHigh  = bars[ today ].High;
                currentLow   = bars[ today ].Low;
                highDiff     = currentHigh - previousHigh;
                lowDiff      = previousLow - currentLow;
                previousHigh = currentHigh;
                previousLow  = currentLow;
                today++;

                prevLoss *= ( optInTimePeriod - 1 );
                prevGain *= ( optInTimePeriod - 1 );

                if ( ( highDiff > 0 ) && ( highDiff > lowDiff ) )
                {
                    prevGain = prevGain + highDiff;
                }

                if ( ( lowDiff > 0 ) && ( lowDiff > highDiff ) )
                {
                    prevLoss = prevLoss + lowDiff;
                }

                prevLoss /= optInTimePeriod;
                prevGain /= optInTimePeriod;


                if ( prevGain + prevLoss != 0 )
                {
                    _latestRsi = 100.0 * ( prevGain / ( prevGain + prevLoss ) );
                    outReal[ outIdx++ ] = _latestRsi;
                }
                else
                {
                    outReal[ outIdx++ ] = 0.0;
                }
            }

            outBegIdx = startIdx;
            outNBElement = outIdx;

            return IndicatorReturn.Success;
        }

        protected void GetHewRsiOverBoughtOverSold( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount )
        {
        //    if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
        //    {
        //        return;
        //    }

        //    double[] kline;
        //    double[] dline;

        //    TASignal lastSignal = TASignal.NONE;
        //    int ithBar = -1;


        //    kline = GeneralHelper.EnumerableToArray( IndicatorResult[ "K" ] );
        //    dline = GeneralHelper.EnumerableToArray( IndicatorResult[ "D" ] );


        //    if ( kline.Length == 0 || dline.Length == 0 )
        //    {
        //        return;
        //    }

        //    if ( kline.Length != dline.Length && dline.Length > curIterationBarcount )
        //    {
        //        throw new NotSupportedException( );
        //    }

        //    var symbol    = DatabarsRepo.Security.Code;
        //    var period    = DatabarsRepo.Period.Value;
        //    var taManager = GlobalTechnicalAnalysisManager.GetPeriodXTa(symbol, period);


        //    int startingIndex = Math.Max(1, _indexOfLastOscillatorCross - 5);

        //    bool waitSignificantCross = false;

        //    for ( int i = startingIndex; i < curIterationBarcount; i++ )               // Tony: We need to ignore the last bar as it is still updating
        //    {
        //        var currentBar = DatabarsRepo.GetBarByIndex( i );

        //        if (
        //                ( kline[ i - 1 ] > dline[ i - 1 ] && kline[ i ] < dline[ i ] ) ||
        //                ( kline[ i - 1 ] < dline[ i - 1 ] && kline[ i ] > dline[ i ] )
        //            )
        //        {
        //            DateTime? barTime = DatabarsRepo.GetTimeAtIndex(i);
        //            _currentOscillatorCross = i;
        //            waitSignificantCross = true;
        //        }

        //        if ( ( i >= _currentOscillatorCross ) && ( waitSignificantCross == true ) )
        //        {
        //            if ( ( dline[ i ] - 3 ) > kline[ i ] )
        //            {
        //                var lastKey = _stochasticsDictionary.Count > 0 ? _stochasticsDictionary.LastKey : -1;



        //                if ( i > lastKey )
        //                {
        //                    _stochasticsDictionary.TryAdd( i, TASignal.HAS_TOPPING_SIGNAL );
        //                    taManager.AddToppingSignal( currentBar, TAToppingSignal.OscillatorCrossDown );
        //                }
        //                //else if ( i < lastKey )
        //                //{
        //                //    string error = "something wrong";
        //                //}



        //                _indexOfLastOscillatorCross = _currentOscillatorCross;
        //                waitSignificantCross = false;
        //            }
        //            else if ( ( kline[ i ] - 3 ) > dline[ i ] )
        //            {
        //                var lastKey = _stochasticsDictionary.Count > 0 ? _stochasticsDictionary.LastKey : -1;

        //                if ( i > lastKey )
        //                {
        //                    _stochasticsDictionary.TryAdd( i, TASignal.HAS_BOTTOMING_SIGNAL );
        //                    taManager.AddBottomingSignal( currentBar, TABottomingSignal.OscillatorCrossUp );
        //                }
        //                //else if ( i < lastKey )
        //                //{
        //                //    string error = "something wrong";
        //                //}



        //                _indexOfLastOscillatorCross = _currentOscillatorCross;
        //                waitSignificantCross = false;
        //            }
        //        }


        //        //!+ When using stochastics as a confirming tool, I see the indicator corroborate the timing of a market turn                 
        //        //!+ associated with a higher closing high, especially when it is in proximity of a targeted support level based off 
        //        //!+ a pivot point level. These rules will help to make better trading triggers for buy and sell signals. 
        //        //!+      When the readings are above 80 percent and %K crosses below the %D line and both lines close back down  
        //        //!+      below the 80 percent line, then a “hook” sell signal is generated. 
        //        //
        //        //!+      When the readings are below 20 percent, once %K crosses above %D, and once both lines close back up  
        //        //!+      above the 20 percent level, then a “hook” buy signal is generated.

        //        if ( kline[ i - 1 ] > 80 && kline[ i ] < 80 )
        //        {
        //            var lastKey = _overBoughtSoldDictionary.Count > 0 ? _overBoughtSoldDictionary.LastKey : -1;

        //            if ( i > lastKey )
        //            {
        //                _overBoughtSoldDictionary.TryAdd( i, TASignal.HAS_TOPPING_SIGNAL );

        //                taManager.AddToppingSignal( currentBar, TAToppingSignal.ExitOverBought );
        //            }


        //            lastSignal = TASignal.ExitOverBought;
        //            ithBar = i;
        //        }
        //        else if ( kline[ i - 1 ] < 20 && kline[ i ] > 20 )
        //        {
        //            var lastKey = _overBoughtSoldDictionary.Count > 0 ? _overBoughtSoldDictionary.LastKey : -1;

        //            if ( i > lastKey )
        //            {
        //                _overBoughtSoldDictionary.TryAdd( i, TASignal.HAS_BOTTOMING_SIGNAL );

        //                taManager.AddBottomingSignal( currentBar, TABottomingSignal.ExitOverSold );
        //            }

        //            lastSignal = TASignal.ExitOverSold;
        //            ithBar = i;
        //        }
        //    }

        //    ThreadSafeIndexedDictionary<int, TASignal> tempDict = new ThreadSafeIndexedDictionary<int, TASignal>();

        //    if ( tempDict.Count == 0 && _stochasticsDictionary.Count > 0 )
        //    {
        //        var smoothValue = TASignal.NONE;

        //        if ( _stochasticsDictionary.ValueAt( 0 ) == TASignal.OscillatorCrossDown )
        //        {
        //            smoothValue = TASignal.OscillatorSmoothDown;
        //        }
        //        else if ( _stochasticsDictionary.ValueAt( 0 ) == TASignal.OscillatorCrossUp )
        //        {
        //            smoothValue = TASignal.OscillatorSmoothUp;
        //        }

        //        tempDict.TryAdd( _stochasticsDictionary.KeyAt( 0 ), smoothValue );
        //    }

        //    // Remove duplicate in the same direction
        //    for ( int i = 1; i < _stochasticsDictionary.Count; i++ )
        //    {
        //        if ( _stochasticsDictionary.ValueAt( i ) != _stochasticsDictionary.ValueAt( i - 1 ) )
        //        {
        //            var smoothValue = TASignal.NONE;

        //            if ( _stochasticsDictionary.ValueAt( i ) == TASignal.OscillatorCrossDown )
        //            {
        //                smoothValue = TASignal.OscillatorSmoothDown;
        //            }
        //            else if ( _stochasticsDictionary.ValueAt( i ) == TASignal.OscillatorCrossUp )
        //            {
        //                smoothValue = TASignal.OscillatorSmoothUp;
        //            }

        //            tempDict.TryAdd( _stochasticsDictionary.KeyAt( i ), smoothValue );
        //        }
        //    }

        //    startingIndex = Math.Max( 1, _smoothedStochasticsDictionary.Count > 0 ? _smoothedStochasticsDictionary.LastKey : 0 );

        //    if ( _smoothedStochasticsDictionary.Count == 0 )
        //    {
        //        if ( tempDict.Count > 0 )
        //        {
        //            _smoothedStochasticsDictionary.TryAdd( tempDict.KeyAt( 0 ), tempDict.ValueAt( 0 ) );
        //        }
        //    }

        //    for ( int i = _indexOfLastSmoothedOscillator; i < tempDict.Count; i++ )
        //    {
        //        //Tony: There is a bug here somewhere
        //        if ( ( tempDict.KeyAt( i ) - tempDict.KeyAt( i - 1 ) ) >= 3 )
        //        {
        //            if ( _smoothedStochasticsDictionary.LastValue == tempDict.ValueAt( i ) )
        //            {
        //                if ( _smoothedStochasticsDictionary.Count > 1 )
        //                {
        //                    _smoothedStochasticsDictionary[ _smoothedStochasticsDictionary.LastKey ] = TASignal.NONE;
        //                }
        //            }
        //            _smoothedStochasticsDictionary[ tempDict.KeyAt( i ) ] = tempDict.ValueAt( i );
        //            _indexOfLastSmoothedOscillator = i;

        //            if ( tempDict.KeyAt( i ) >= ithBar )
        //            {
        //                ithBar = tempDict.KeyAt( i );
        //                lastSignal = tempDict.ValueAt( i );
        //            }
        //        }
        //    }

        //    DatabarsRepo.AddSignalsToDataBar( _smoothedStochasticsDictionary );
        //    DatabarsRepo.AddSignalsToDataBar( _stochasticsDictionary );
        //    DatabarsRepo.AddSignalsToDataBar( _overBoughtSoldDictionary );

        //    _fxTradingEventsBindingList = GlobalTechnicalAnalysisManager.TradingEventsBindingList( DatabarsRepo.Security.Code );
        //    _fxTradingEventsBindingList.AddWarning( DatabarsRepo.Period.Value, ithBar, ( int )lastSignal );
        }



        
    }
}
