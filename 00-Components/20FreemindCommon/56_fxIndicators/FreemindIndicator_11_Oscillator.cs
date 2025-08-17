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
using System.Data;


#pragma warning disable 414

namespace fx.Indicators
{
    public partial class FreemindIndicator : CustomPlatformIndicator
    {
        private double _latestStoValue = 0.0;
        

        protected Task BasicOscillatorTask( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount, ref PooledList<Task> tasksList )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            //ThreadHelper.UpdateThreadName( "BasicOscillatorTask" );
            

            Task first = new Task(() => CalculateOscillator( fullRecalculation, updateType, curIterationBarcount ), IndicatorExitToken);
            Task second = first.ContinueWith(
                                                antecedent =>
                                                {
                                                    if (antecedent.Status == TaskStatus.Faulted)
                                                    {
                                                        this.LogError("CalculateOscillator() caused Exception. Stack = " + antecedent.Exception.InnerException.StackTrace);
                                                    }
                                                    else
                                                    {
                                                        GetOverBoughtOverSold( fullRecalculation, updateType, curIterationBarcount );
                                                    }

                                                }, IndicatorExitToken);

            tasksList.Add( first );
            tasksList.Add( second );

            return first;
        }

        protected void CalculateOscillator( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount )
        {
            int resultSetLength   = IndicatorResult["K"].Count;

            if ( resultSetLength == 0 )
            {
                ProcessOscNewIndicatorBuffer( curIterationBarcount );
            }
            else
            {
                ProcessOscExistingBuffer( updateType, curIterationBarcount, resultSetLength );
            }
        }

        private void ProcessOscExistingBuffer( DataBarUpdateType? updateType, int barB4Calculation, int resultSetLength )
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

            double[] valueK = new double[indexCount];
            double[] valueD = new double[indexCount];


            Core.Stoch( Bars, startIndex, endIndex, valueK, valueD, out outBeginIdx, out outNBElement, Core.MAType.Sma, Core.MAType.Sma, 14, 5, 5 );

            IndicatorResult.AddSetValues( "K", outBeginIdx, outNBElement, true, valueK );
            IndicatorResult.AddSetValues( "D", outBeginIdx, outNBElement, true, valueD );

            if ( outNBElement > 0 )
            {
                _latestStoValue = valueD[ outNBElement - 1 ];

                if ( _fxTradingEventsBindingList == null )
                {
                    var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

                    if ( aa == null )
                        return;

                    _fxTradingEventsBindingList = aa.TradingEventsBindingList;
                }

                _fxTradingEventsBindingList.AddStochValue( Bars.Period.Value, barB4Calculation, ( int ) _latestStoValue, barB4Calculation );
            }
        }

        private void ProcessOscNewIndicatorBuffer( int barB4Calculation )
        {
            int startIndex     = 0;
            int endIndex       = barB4Calculation - 1;
            int indexCount     = endIndex - startIndex + 1;

            int outBeginIdx = 0;
            int outNBElement = 0;

            double[] valueK = new double[indexCount];
            double[] valueD = new double[indexCount];


            Core.Stoch( Bars, startIndex, endIndex, valueK, valueD, out outBeginIdx, out outNBElement, Core.MAType.Sma, Core.MAType.Sma, 14, 5, 5 );

            IndicatorResult.AddSetValues( "K", outBeginIdx, outNBElement, true, valueK );
            IndicatorResult.AddSetValues( "D", outBeginIdx, outNBElement, true, valueD );

            if ( outNBElement > 0 )
            {
                _latestStoValue = valueD[ outNBElement - 1 ];

                if ( _fxTradingEventsBindingList == null )
                {
                    var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

                    if ( aa == null )
                        return;

                    _fxTradingEventsBindingList = aa.TradingEventsBindingList;
                }

                _fxTradingEventsBindingList.AddStochValue( Bars.Period.Value, barB4Calculation, ( int ) _latestStoValue, barB4Calculation );
            }
        }

        

        protected void StoreLastOscillatorSignal( int k, DateTime barTime, double kValue, double dValue, MarketDirection direction )
        {
            _lastOscillatorCrossIndex = k;
            _lastOscillatorCrossTime  = barTime;
            _lastOscillatorKValue     = kValue;
            _lastOscillatorDValue     = dValue;
            _lastOscillatorDirection     = direction;            
        }

        protected void GetOverBoughtOverSold( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return;
            }

            _hasNewOscillatorCross = false;

            double[] kline;
            double[] dline;

            _lastOscillatorSignal = TASignal.NONE;
            int ithBar = -1;


            kline = GeneralHelper.EnumerableToArray( IndicatorResult [ "K" ] );
            dline = GeneralHelper.EnumerableToArray( IndicatorResult [ "D" ] );


            if ( kline.Length == 0 || dline.Length == 0 )
            {
                return;
            }

            if ( kline.Length != dline.Length  )
            {
                
            }

            var symbol        = Bars.Security.Code;
            var period        = Bars.Period.Value;
            
            var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

            if ( aa == null )
            {
                return;                
            }

            var taManager     = ( PeriodXTaManager ) aa.GetPeriodXTa( period );

            int startingIndex = Math.Max(1, _lastOscillatorCrossIndex - 5);

            bool waitSignificantCross = false;

            for ( int i = startingIndex; i < curIterationBarcount; i++ )               // Tony: We need to ignore the last bar as it is still updating
            {
                var currentBar = Bars.GetBarByIndex( i );

                if  ( ( kline [ i - 1 ] > dline [ i - 1 ] && kline [ i ] < dline [ i ] ) || ( kline [ i - 1 ] < dline [ i - 1 ] && kline [ i ] > dline [ i ] ) )
                {
                    DateTime? barTime = Bars.GetTimeAtIndex(i);
                    _currentOscillatorCross = i;
                    waitSignificantCross = true;
                }

                if ( ( i >= _currentOscillatorCross ) && ( waitSignificantCross == true ) )
                {
                    if ( ( dline [ i ] - 3 ) > kline [ i ] )
                    {
                        var lastKey = _stochasticsDictionary.Count > 0 ? _stochasticsDictionary.Keys[ _stochasticsDictionary.Count - 1 ] : -1;

                        if ( i > lastKey )
                        {
                            _stochasticsDictionary.TryAdd( i, TASignal.HAS_TOPPING_SIGNAL );
                            taManager.AddToppingSignal( ref currentBar, TAToppingSignal.OscillatorCrossDown );

                            if ( kline[ i ] > 80 )
                            {
                                StoreLastOscillatorSignal( _currentOscillatorCross, currentBar.BarTime, kline[ i ], dline[ i ], MarketDirection.BullishCorrection );                                
                            }
                            else
                            {
                                StoreLastOscillatorSignal( _currentOscillatorCross, currentBar.BarTime, kline[ i ], dline[ i ], MarketDirection.Bearish );                                
                            }                            
                        }                        
                        
                        waitSignificantCross = false;
                    }
                    else if ( ( kline [ i ] - 3 ) > dline [ i ] )
                    {
                        var lastKey = _stochasticsDictionary.Count > 0 ? _stochasticsDictionary.Keys[ _stochasticsDictionary.Count - 1 ] : -1;

                        if ( i > lastKey )
                        {
                            _stochasticsDictionary.TryAdd( i, TASignal.HAS_BOTTOMING_SIGNAL );
                            taManager.AddBottomingSignal( ref currentBar, TABottomingSignal.OscillatorCrossUp );

                            if (kline[i] > 20)
                            {
                                StoreLastOscillatorSignal( _currentOscillatorCross, currentBar.BarTime, kline[ i ], dline[ i ], MarketDirection.Bullish );                                
                            }
                            else
                            {
                                StoreLastOscillatorSignal( _currentOscillatorCross, currentBar.BarTime, kline[ i ], dline[ i ], MarketDirection.BearishCorrection );                                
                            }
                        }                        
                        
                        waitSignificantCross = false;
                    }
                }


                //!+ When using stochastics as a confirming tool, I see the indicator corroborate the timing of a market turn                 
                //!+ associated with a higher closing high, especially when it is in proximity of a targeted support level based off 
                //!+ a pivot point level. These rules will help to make better trading triggers for buy and sell signals. 
                //!+      When the readings are above 80 percent and %K crosses below the %D line and both lines close back down  
                //!+      below the 80 percent line, then a “hook” sell signal is generated. 
                //
                //!+      When the readings are below 20 percent, once %K crosses above %D, and once both lines close back up  
                //!+      above the 20 percent level, then a “hook” buy signal is generated.

                if ( kline [ i - 1 ] > 80 && kline [ i ] < 80 )
                {
                    var lastKey = _overBoughtSoldDictionary.Count > 0 ? _overBoughtSoldDictionary.Keys[ _overBoughtSoldDictionary.Count - 1 ] : -1;

                    if ( i > lastKey )
                    {
                        _overBoughtSoldDictionary.TryAdd( i, TASignal.HAS_TOPPING_SIGNAL );

                        taManager.AddToppingSignal( ref currentBar, TAToppingSignal.ExitOverBought );

                        StoreLastOscillatorSignal( _currentOscillatorCross, currentBar.BarTime, kline[ i ], dline[ i ], MarketDirection.Bearish );

                        _lastOscillatorExitOverBought = i;
                        _lastOscillatorExitOverSold = 0;
                    }


                    _lastOscillatorSignal = TASignal.ExitOverBought;
                    ithBar = i;
                }
                else if ( kline [ i - 1 ] < 20 && kline [ i ] > 20 )
                {
                    var lastKey = _overBoughtSoldDictionary.Count > 0 ? _overBoughtSoldDictionary.Keys[ _overBoughtSoldDictionary.Count - 1 ] : -1;

                    if ( i > lastKey )
                    {
                        StoreLastOscillatorSignal( _currentOscillatorCross, currentBar.BarTime, kline[ i ], dline[ i ], MarketDirection.Bullish );

                        _overBoughtSoldDictionary.TryAdd( i, TASignal.HAS_BOTTOMING_SIGNAL );

                        taManager.AddBottomingSignal( ref currentBar, TABottomingSignal.ExitOverSold );

                        _lastOscillatorExitOverBought = 0;
                        _lastOscillatorExitOverSold   = i;
                    }

                    _lastOscillatorSignal = TASignal.ExitOverSold;
                    ithBar = i;
                }
            }

            //ThreadSafeIndexedDictionary<int, TASignal> tempDict = new ThreadSafeIndexedDictionary<int, TASignal>();

            //if ( tempDict.Count == 0 && _stochasticsDictionary.Count > 0 )
            //{
            //    var smoothValue = TASignal.NONE;

            //    if ( _stochasticsDictionary.ValueAt( 0 ) == TASignal.OscillatorCrossDown )
            //    {
            //        smoothValue = TASignal.OscillatorSmoothDown;
            //    }
            //    else if ( _stochasticsDictionary.ValueAt( 0 ) == TASignal.OscillatorCrossUp )
            //    {
            //        smoothValue = TASignal.OscillatorSmoothUp;
            //    }

            //    tempDict.TryAdd( _stochasticsDictionary.KeyAt( 0 ), smoothValue );
            //}

            //// Remove duplicate in the same direction
            //for ( int i = 1; i < _stochasticsDictionary.Count; i++ )
            //{
            //    if ( _stochasticsDictionary.ValueAt( i ) != _stochasticsDictionary.ValueAt( i - 1 ) )
            //    {
            //        var smoothValue = TASignal.NONE;

            //        if ( _stochasticsDictionary.ValueAt( i ) == TASignal.OscillatorCrossDown )
            //        {
            //            smoothValue = TASignal.OscillatorSmoothDown;
            //        }
            //        else if ( _stochasticsDictionary.ValueAt( i ) == TASignal.OscillatorCrossUp )
            //        {
            //            smoothValue = TASignal.OscillatorSmoothUp;
            //        }

            //        tempDict.TryAdd( _stochasticsDictionary.KeyAt( i ), smoothValue );
            //    }
            //}

            //startingIndex = Math.Max( 1, _smoothedStochasticsDictionary.Count > 0 ? _smoothedStochasticsDictionary.LastKey : 0 );

            //if ( _smoothedStochasticsDictionary.Count == 0 )
            //{
            //    if ( tempDict.Count > 0 )
            //    {
            //        _smoothedStochasticsDictionary.TryAdd( tempDict.KeyAt( 0 ), tempDict.ValueAt( 0 ) );
            //    }
            //}

            //for ( int i = _indexOfLastSmoothedOscillator; i < tempDict.Count; i++ )
            //{
            //    //Tony: There is a bug here somewhere
            //    if ( ( tempDict.KeyAt( i ) - tempDict.KeyAt( i - 1 ) ) >= 3 )
            //    {
            //        if ( _smoothedStochasticsDictionary.LastValue == tempDict.ValueAt( i ) )
            //        {
            //            if ( _smoothedStochasticsDictionary.Count > 1 )
            //            {
            //                _smoothedStochasticsDictionary [ _smoothedStochasticsDictionary.LastKey ] = TASignal.NONE;
            //            }
            //        }
            //        _smoothedStochasticsDictionary [ tempDict.KeyAt( i ) ] = tempDict.ValueAt( i );
            //        _indexOfLastSmoothedOscillator = i;

            //        if ( tempDict.KeyAt( i ) >= ithBar )
            //        {
            //            ithBar = tempDict.KeyAt( i );
            //            _lastOscillatorSignal = tempDict.ValueAt( i );
            //        }
            //    }
            //}

            //IndicatorBarsRepo.AddSignalsToDataBar( _smoothedStochasticsDictionary );
            Bars.AddSignalsToDataBar( _stochasticsDictionary );
            Bars.AddSignalsToDataBar( _overBoughtSoldDictionary );

            if ( _lastOscillatorCrossIndex  > 1 )
            {
                var newEvent = new OscillatorCrossEventArgs(    Bars.Security.Code, 
                                                                Bars.Period.Value, 
                                                                _lastOscillatorCrossTime, 
                                                                _lastOscillatorCrossIndex, 
                                                                _lastOscillatorSignal,
                                                                _lastOscillatorDirection,
                                                                _lastOscillatorExitOverBought,
                                                                _lastOscillatorExitOverSold,
                                                                curIterationBarcount, 
                                                                _lastOscillatorKValue, 
                                                                _lastOscillatorDValue );

                if ( _lastOscillatorCrossEvent != newEvent )
                {
                    _hasNewOscillatorCross = true;
                    _lastOscillatorCrossEvent = newEvent;

                    aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

                    if ( aa == null )
                        return;

                    aa.AddOscillatorCrossSignal( _lastOscillatorCrossEvent );
                }
            }                                    
        }
       
        protected Task TaskOscillator( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            Task first = new Task(() => GetOverBoughtOverSold(fullRecalculation, updateType, curIterationBarcount), IndicatorExitToken);

            first.Start( );

            return first;
        }
    }
}
