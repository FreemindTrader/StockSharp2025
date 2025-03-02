using DevExpress.Mvvm;
using fx.Common;
using fx.Bars;
using fx.Definitions;
using fx.Algorithm;

using System;
using fx.Collections;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using fx.TALib;
using System.Threading;

#pragma warning disable 414

namespace fx.Indicators
{
    public partial class FreemindIndicator : CustomPlatformIndicator
    {                
        protected Task TaskMACD( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }            

            Task first = new Task(() => GetMACDCrossOver(fullRecalculation, updateType, curIterationBarcount, IndicatorExitToken ), IndicatorExitToken);
            Task second = first.ContinueWith(
                                                antecedent =>
                                                {
                                                    if (antecedent.Status == TaskStatus.Faulted)
                                                    {
                                                        this.LogError( "GetMACDCrossOver() caused Exception. Stack = " + antecedent.Exception.InnerException.StackTrace);
                                                    }
                                                    else
                                                    {
                                                        DetectDivergenceFromZigZag( fullRecalculation, updateType, curIterationBarcount, IndicatorExitToken);
                                                        DetectWaveRotationFromZigZag( fullRecalculation, updateType, IndicatorExitToken );
                                                        DetectGannSquareFromZigZag( fullRecalculation, updateType, IndicatorExitToken );
                                                        DetectPivotPointResistanceSupportFromZigZag( fullRecalculation, updateType, IndicatorExitToken );
                                                    }

                                                }, IndicatorExitToken);



            first.Start( );

            return first;
        }

        protected linesCrossEnum MacdCross( double previousFastMacd, double previousSlowMacd, double currentFastMacd, double currentSlowMacd )
        {
            linesCrossEnum flag = linesCrossEnum.Unknown;

            if ( ( previousFastMacd >= previousSlowMacd && currentFastMacd <= currentSlowMacd ) )
            {
                flag = linesCrossEnum.CrossDown;
            }

            // Macd is crossing up
            if ( previousFastMacd <= previousSlowMacd && currentFastMacd >= currentSlowMacd )
            {
                flag = linesCrossEnum.CrossUp;
            }

            if ( ( currentFastMacd > currentSlowMacd && previousFastMacd > previousSlowMacd ) )
            {
                flag = linesCrossEnum.UpTrend;
            }


            if ( ( currentFastMacd < currentSlowMacd && previousFastMacd < previousSlowMacd ) )
            {
                flag = linesCrossEnum.DownTrend;
            }            

            return ( flag );
        }

        public struct MacdRegionalData
        {
            public double highestMacd;

            public double lowestMacd;

            public double highestPrice;

            public double lowestPrice;

            public int highestPriceIndex;

            public int lowestPriceIndex;

            public double lastMacdExtremumVal;

            public double lastPriceExtremeVal;
            public int lowerLowInUptrend;
            public int higherHighInDownTrend;

            public void Init()
            {
                highestMacd           = double.MinValue;
                lowestMacd            = double.MaxValue;
                highestPrice          = double.MinValue;
                lowestPrice           = double.MaxValue;
                highestPriceIndex     = -1;
                lowestPriceIndex      = -1;
                lastMacdExtremumVal   = 0;
                lastPriceExtremeVal   = 0;
                lowerLowInUptrend     = -1;
                higherHighInDownTrend = -1;

            }
            
            public void SetMaxMin( int k, double currentFastMacd, double barHigh, double barLow )
            {
                if ( currentFastMacd > highestMacd )
                {
                    highestMacd = currentFastMacd;
                }

                if ( currentFastMacd < lowestMacd )
                {
                    lowestMacd = currentFastMacd;
                }

                if ( barHigh > highestPrice )
                {
                    highestPrice = barHigh;
                    highestPriceIndex = k;
                }

                if ( barLow < lowestPrice )
                {
                    lowestPrice = barLow;
                    lowestPriceIndex = k;
                }
            }

            public void SetLow( int k, double barHigh )
            {
                lastMacdExtremumVal = lowestMacd;
                lastPriceExtremeVal = lowestPrice;
                highestPrice      = barHigh;
                highestPriceIndex = k;

                lowestPrice       = double.MaxValue;
                lowestMacd        = double.MaxValue;

                lowestPriceIndex  = -1;
            }

            public void SetHigh( int k, double barLow )
            {
                lastMacdExtremumVal = highestMacd;
                lastPriceExtremeVal = highestPrice;
                lowestPrice = barLow;
                lowestPriceIndex = k;

                highestPrice = double.MinValue;
                highestMacd = double.MinValue;

                highestPriceIndex = -1;
            }

            public bool FoundRareConditions( ref SBar myBar, int k, TASignal _lastMacdSignal )
            {
                var foundRare = false;
                
                // The following code, I want to get the local Maximum or Local Minimum
                if ( _lastMacdSignal == TASignal.HAS_BOTTOMING_SIGNAL )
                {
                    var currentLow = myBar.Low;

                    if ( myBar.IsWaveTrough( ) )
                    {
                        if ( currentLow < lastPriceExtremeVal )
                        {
                            lastPriceExtremeVal = currentLow;
                            lowerLowInUptrend = k;
                            foundRare = true;
                        }
                    }
                }
                else if ( _lastMacdSignal == TASignal.HAS_TOPPING_SIGNAL )
                {
                    var currentHigh = myBar.High;

                    if ( myBar.IsWavePeak( ) )
                    {
                        if ( currentHigh > lastPriceExtremeVal )
                        {
                            lastPriceExtremeVal = currentHigh;
                            higherHighInDownTrend = k;
                            foundRare = true;
                        }
                    }
                }

                return foundRare;
            }
        }

        protected void StoreLastMacdCrossUp( int k, DateTime barTime,  double currentFastMacd, double currentSlowMacd )
        {
            _lastMacdCrossIndex    = k;
            _lastMacdCrossTime     = barTime;
            _lastMacdCrossFastMacd = currentFastMacd;
            _lastMacdCrossSlowMacd = currentSlowMacd;
            _lastMacdCrossUpIndex  = k;
            _lastMacdSignal        = TASignal.HAS_BOTTOMING_SIGNAL;

            if ( currentFastMacd < 0 )
            {
                _lastMacdDirection = MarketDirection.BearishCorrection;
            }
            else if ( currentFastMacd > 0 )
            {
                _lastMacdDirection = MarketDirection.Bullish;
            }
        }

        protected void StoreLastMacdCrossDown( int k, DateTime barTime, double currentFastMacd, double currentSlowMacd )
        {
            _lastMacdCrossIndex     = k;
            _lastMacdCrossTime      = barTime;
            _lastMacdCrossFastMacd  = currentFastMacd;
            _lastMacdCrossSlowMacd  = currentSlowMacd;
            _lastMacdCrossDownIndex = k;
            _lastMacdSignal         = TASignal.HAS_TOPPING_SIGNAL;

            if ( currentFastMacd < 0 )
            {
                _lastMacdDirection = MarketDirection.Bearish;
            }
            else if ( currentFastMacd > 0 )
            {
                _lastMacdDirection = MarketDirection.BullishCorrection;
            }
        }

        protected void GetMACDCrossOver( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount, CancellationToken token )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return;
            }

            var symbol    = Bars.Security.Code;
            var period    = Bars.Period.Value;

            var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

            if ( aa == null )
                return;

            _hasNewMacdCross = false;

            var taManager = ( PeriodXTaManager ) aa.GetPeriodXTa( period );            
            var fastMacd  = GeneralHelper.EnumerableToArray( IndicatorResult["MACD"]       );
            var slowMacd  = GeneralHelper.EnumerableToArray( IndicatorResult["MACDSignal"] );

            if ( fastMacd.Length == 0 || slowMacd.Length == 0 )
            {
                return;
            }

            if ( fastMacd.Length != slowMacd.Length )
            {
                return;
            }

            int startingIndex         = Math.Max(1, _lastMacdCrossIndex - 5);
            int endIndex              = Math.Min( curIterationBarcount, fastMacd.Length );
            bool waitSignificantCross = false;
            int currentMacdCross      = 0;                       

            MacdRegionalData d = new MacdRegionalData( );

            d.Init( );
                                               

            for ( int k = startingIndex; k < endIndex; k++ )             // Tony: We need to ignore the last bar as it is still updating
            {
                if ( token.IsCancellationRequested )
                {
                    token.ThrowIfCancellationRequested();
                }

                DateTime? barTime    = Bars.GetTimeAtIndex(k);

                var currentFastMacd  = fastMacd[ k ];
                var currentSlowMacd  = slowMacd[ k ];
                var previousFastMacd = fastMacd[ k - 1 ];
                var previousSlowMacd = slowMacd[ k - 1 ];

                var fastMacdCross    = MacdCross( previousFastMacd, previousSlowMacd, currentFastMacd, currentSlowMacd );

                if ( fastMacdCross == linesCrossEnum.CrossDown || fastMacdCross == linesCrossEnum.CrossUp  )
                {
                    currentMacdCross = k;
                    waitSignificantCross = true;
                }

                var currentBar = Bars.GetBarByIndex( k );

                if ( currentBar == SBar.EmptySBar)
                    continue;

                d.SetMaxMin( k, currentFastMacd, currentBar.High, currentBar.Low );
                

                if (_lastMacdDirection == MarketDirection.BearishCorrection && currentFastMacd > 0 )
                {
                    _lastMacdDirection = MarketDirection.Bullish;
                }
                else if (_lastMacdDirection == MarketDirection.BullishCorrection && currentFastMacd < 0)
                {
                    _lastMacdDirection = MarketDirection.Bearish;
                }


                if ( ( k >= currentMacdCross ) && ( waitSignificantCross == true ) )
                {                   
                    if ( (currentFastMacd - currentSlowMacd) >= 0.000012 )
                    {
                        int lastKey = -1;
                        if ( _macdSignificantCross.Count > 0 )
                        {
                            lastKey = _macdSignificantCross.Keys[ _macdSignificantCross.Count - 1 ];
                        }

                        if ( k > lastKey && ( _lastMacdSignal == TASignal.HAS_TOPPING_SIGNAL || _lastMacdSignal == TASignal.NONE ) )
                        {
                            _macdSignificantCross.TryAdd( k, TASignal.HAS_BOTTOMING_SIGNAL );
                            _macdExtremumDict.TryAdd( d.lowestPriceIndex, new Tuple<MacdSignal, double>( MacdSignal.LowestPoint_MacdDowntrend, d.lowestMacd ) );

                            d.SetLow( k, currentBar.High );

                            StoreLastMacdCrossUp( k, currentBar.BarTime, currentFastMacd, currentSlowMacd );

                            waitSignificantCross = false;

                            taManager.AddBottomingSignal( ref currentBar, TABottomingSignal.MACD_CROSS_UP );
                        }
                    }
                    else if ( ( fastMacd [ k ] - slowMacd [ k ] ) <= -0.000012 )
                    {
                        int lastKey = -1;
                        if ( _macdSignificantCross.Count > 0 )
                        {
                            lastKey = _macdSignificantCross.Keys[ _macdSignificantCross.Count - 1 ];
                        }                        

                        if ( k > lastKey && ( _lastMacdSignal == TASignal.HAS_BOTTOMING_SIGNAL || _lastMacdSignal == TASignal.NONE ) )
                        {
                            _macdSignificantCross.TryAdd( k, TASignal.HAS_TOPPING_SIGNAL );
                            _macdExtremumDict.TryAdd( d.highestPriceIndex, new Tuple<MacdSignal, double>( MacdSignal.HighestPoint_MacdUptrend, d.highestMacd ) );
                            
                            d.SetHigh( k, currentBar.Low );

                            StoreLastMacdCrossDown( k, currentBar.BarTime, currentFastMacd, currentSlowMacd );

                            waitSignificantCross      = false;
                            
                            taManager.AddToppingSignal( ref currentBar, TAToppingSignal.MACD_CROSS_DOWN );
                        }
                    }
                }
                                
                // The following code, I want to get the local Maximum or Local Minimum
                if ( d.FoundRareConditions( ref Bars.GetBarByIndex( k ), k, _lastMacdSignal ) )
                {
                    // So if after 5 bars, the trend still havn't changed, this should be considered a spike down and recover
                    if ( d.lowerLowInUptrend > 0 )
                    {
                        _macdExtremumDict.TryAdd( d.lowerLowInUptrend, new Tuple<MacdSignal, double>( MacdSignal.LowerLow_MacdUptrend, d.lastMacdExtremumVal ) );
                        
                        d.lowerLowInUptrend = -1;
                    }

                    // So if after 5 bars, the trend still havn't changed, this should be considered a spike up and keep goes down.
                    if ( d.higherHighInDownTrend > 0 )
                    {
                        _macdExtremumDict.TryAdd( d.higherHighInDownTrend, new Tuple<MacdSignal, double>( MacdSignal.HigherHigh_MacdDowntrend, d.lastMacdExtremumVal ) );
                        
                        d.higherHighInDownTrend = -1;
                    }
                }
            }

            /// <image url="$(SolutionDir)\..\..\30 - CommonImages\ExtremumLowRare2.png" /> 

            Bars.AddSignalsToDataBar( _macdSignificantCross );

            if ( _lastMacdCrossIndex > 1 )
            {
                var newEvent = new MacdCrossEventArgs( Bars.Security.Code, Bars.Period.Value, _lastMacdCrossTime, _lastMacdCrossIndex, _lastMacdDirection, curIterationBarcount, _lastMacdCrossFastMacd, _lastMacdCrossSlowMacd );

                if ( _lastMacdCrossEvent != newEvent )
                {
                    _hasNewMacdCross = true;
                    _lastMacdCrossEvent = newEvent;

                    aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

                    if ( aa == null )
                        return;

                    aa.AddMacdCrossSignal( _lastMacdCrossEvent );
                }
            }                        
        }
    }
}