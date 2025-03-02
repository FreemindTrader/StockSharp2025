using System;
using fx.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using fx.Algorithm;
using fx.TALib;
using fx.Definitions;
using System.Data;

using fx.Common;

#pragma warning disable 414

namespace fx.Indicators
{
    public partial class FreemindIndicator : CustomPlatformIndicator
    {
        private int _cciLookback = -1;

        private double _latestCci = 0.0;
        protected Task BasicCciTask( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount, ref PooledList<Task> tasksList )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            //ThreadHelper.UpdateThreadName( "BasicCciTask" );            

            Task first = new Task(() => CalculateCci( fullRecalculation, updateType, curIterationBarcount ), IndicatorExitToken);
            Task second = first.ContinueWith(
                                                antecedent =>
                                                {
                                                    if (antecedent.Status == TaskStatus.Faulted)
                                                    {
                                                        this.LogError("CalculateOscillator() caused Exception. Stack = " + antecedent.Exception.InnerException.StackTrace);
                                                    }
                                                    else
                                                    {
                                                        GetCciOverBoughtOverSold( fullRecalculation, updateType, curIterationBarcount );
                                                    }

                                                }, IndicatorExitToken);

            tasksList.Add( first );
            tasksList.Add( second );

            return first;
        }

        protected void CalculateCci( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount )
        {
            _cciLookback = Core.CciLookback( 21 );

            int resultSetLength   = IndicatorResult["CCI"].Count;

            if ( resultSetLength == 0 )
            {
                ProcessCciNewIndicatorBuffer( curIterationBarcount );
            }
            else
            {
                ProcessCciExistingBuffer( updateType, curIterationBarcount, resultSetLength );
            }
        }

        private void ProcessCciNewIndicatorBuffer( int barB4Calculation )
        {
            int startIndex     = 0;
            int endIndex       = barB4Calculation - 1;
            int indexCount     = endIndex - startIndex + 1;

            if( endIndex <= startIndex )
                return;

            var myCci            = new double [ indexCount ];

            var outBeginIdx   = 0;
            var outNBElement = 0;

            Core.Cci( Bars, startIndex, endIndex, myCci, out outBeginIdx, out outNBElement, 21 );

            if( outNBElement == 0 )
                return;


            lock ( IndicatorResult )
            {
                IndicatorResult.AddSetValues( "CCI", outBeginIdx, outNBElement, true, myCci );
            }

            _latestCci = myCci[ outNBElement - 1 ];

            if ( _fxTradingEventsBindingList == null )
            {
                var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

                if ( aa == null )
                    return;

                _fxTradingEventsBindingList = aa.TradingEventsBindingList;
            }            
            
            _fxTradingEventsBindingList.AddCciValue( Bars.Period.Value, barB4Calculation, ( int )_latestCci, barB4Calculation );
        }

        private void ProcessCciExistingBuffer( DataBarUpdateType? updateType, int barB4Calculation, int resultSetLength )
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

            var myCci            = new double [ indexCount ];

            Core.Cci( Bars, startIndex, endIndex, myCci, out outBeginIdx, out outNBElement, 21 );            

            if ( outNBElement > 0 )
            {
                lock ( IndicatorResult )
                {
                    IndicatorResult.AddSetValues( "CCI", outBeginIdx, outNBElement, true, myCci );
                }

                _latestCci = myCci[ outNBElement - 1 ];

                if ( _fxTradingEventsBindingList == null )
                {
                    var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

                    if ( aa == null )
                        return;

                    _fxTradingEventsBindingList = aa.TradingEventsBindingList;
                }

                _fxTradingEventsBindingList.AddCciValue( Bars.Period.Value, barB4Calculation, ( int )_latestCci, barB4Calculation );
            }
        }


        protected void GetCciOverBoughtOverSold( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount )
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
