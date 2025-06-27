using fx.Common;


using DevExpress.Mvvm;
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
        double _currentEma13Value = 0d;

        protected Task BasicEmaCrossTask( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount, ref PooledList<Task> tasksList )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }


            //ThreadHelper.UpdateThreadName( "BasicEmaCrossTask" );
            

            Task first = new Task(() => CalculateEMAs( fullRecalculation, updateType, curIterationBarcount ), IndicatorExitToken);
            Task second = first.ContinueWith(
                                                antecedent =>
                                                {
                                                    if (antecedent.Status == TaskStatus.Faulted)
                                                    {
                                                        this.LogError("CalculateEMAs() caused Exception. Stack = " + antecedent.Exception.InnerException.StackTrace);
                                                    }
                                                    else
                                                    {
                                                        GetEMAsCrossover( fullRecalculation, updateType, curIterationBarcount );
                                                    }

                                                }, IndicatorExitToken);

            tasksList.Add( first );
            tasksList.Add( second );

            return first;
        }
        protected void CalculateEMAs( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount )
        {            
            int ema5Length        = IndicatorResult["EMA5"].Count;
            int ema13Length       = IndicatorResult["EMA13"].Count;
            int ema144Length       = IndicatorResult["EMA144"].Count;

            if ( ema5Length == 0 )
            {
                ProcessNewIndicatorBuffer( curIterationBarcount, 5, "EMA5" );
            }
            else
            {
                ProcessExistingBuffer( updateType, curIterationBarcount, ema5Length, 5, "EMA5" );
            }

            if ( ema13Length == 0 )
            {
                ProcessNewEma13IndicatorBuffer( curIterationBarcount, 13, "EMA13", curIterationBarcount );
            }
            else
            {
                ProcessEma13ExistingBuffer( updateType, curIterationBarcount, ema5Length, 13, "EMA13", curIterationBarcount );
            }

            if ( ema144Length == 0 )
            {
                ProcessNewIndicatorBuffer( curIterationBarcount, 144, "EMA144" );
            }
            else
            {
                ProcessExistingBuffer( updateType, curIterationBarcount, ema5Length, 144, "EMA144" );
            }
        }

        private void ProcessNewIndicatorBuffer( int barB4Calculation, int emaLength, string emaBuffer )
        {
            int startIndex    = 0;
            int endIndex      = barB4Calculation - 1;
            int indexCount    = endIndex - startIndex + 1;

            var ema          = new double [ indexCount ];

            var outBeginIdx   = 0;
            var outNBElement = 0;

            Core.Ema( Bars, 0, indexCount - 1, ema, out outBeginIdx, out outNBElement, emaLength );

            lock ( IndicatorResult )
            {
                IndicatorResult.AddSetValues( emaBuffer, outBeginIdx, outNBElement, true, ema );
            }
        }

        private void ProcessNewEma13IndicatorBuffer( int barB4Calculation, int emaLength, string emaBuffer, int curIterationBarcount )
        {
            int startIndex    = 0;
            int endIndex      = barB4Calculation - 1;
            int indexCount    = endIndex - startIndex + 1;

            var ema          = new double [ indexCount ];

            var outBeginIdx   = 0;
            var outNBElement = 0;

            Core.Ema( Bars, 0, indexCount - 1, ema, out outBeginIdx, out outNBElement, emaLength );
            

            if ( outNBElement > 1 )
            {
                lock ( IndicatorResult )
                {
                    IndicatorResult.AddSetValues( emaBuffer, outBeginIdx, outNBElement, true, ema );
                }

                _lastEma13Value    = ema [ outNBElement - 2 ];
                _currentEma13Value = ema [ outNBElement - 1 ];

                if ( _fxTradingEventsBindingList == null )
                {
                    var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

                    if ( aa == null )
                        return;

                    _fxTradingEventsBindingList = aa.TradingEventsBindingList;
                }

                _fxTradingEventsBindingList.AddEmaValues( Bars.Period.Value, curIterationBarcount, _currentEma13Value, _lastEma13Value, curIterationBarcount );                
            }

            
        }

        private void ProcessExistingBuffer( DataBarUpdateType? updateType, int barB4Calculation, int resultSetLength, int emaLength, string emaBuffer )
        {
            var startIndex = resultSetLength - 1;
            var endIndex   = barB4Calculation - 1;
            var indexCount = endIndex - startIndex + 1;

            if ( endIndex < 0 || indexCount < 0 )
            {
                return;
            }

            int outBeginIdx  = 0;
            int outNBElement = 0;

            var ema          = new double [ indexCount ];

            Core.Ema( Bars, startIndex, endIndex, ema, out outBeginIdx, out outNBElement, emaLength );
            

            lock ( IndicatorResult )
            {
                IndicatorResult.AddSetValues( emaBuffer, outBeginIdx, outNBElement, true, ema );
            }
        }

        private void ProcessEma13ExistingBuffer( DataBarUpdateType? updateType, int barB4Calculation, int resultSetLength, int emaLength, string emaBuffer, int curIterationBarcount )
        {
            var startIndex = resultSetLength - 1;
            var endIndex   = barB4Calculation - 1;
            var indexCount = endIndex - startIndex + 1;


            if ( endIndex < 0 || indexCount < 0 )
            {
                return;
            }

            int outBeginIdx  = 0;
            int outNBElement = 0;

            var ema          = new double [ indexCount ];

            Core.Ema( Bars, startIndex, endIndex, ema, out outBeginIdx, out outNBElement, emaLength );

            if ( outNBElement > 0 )
            {
                lock ( IndicatorResult )
                {
                    IndicatorResult.AddSetValues( emaBuffer, outBeginIdx, outNBElement, true, ema );
                }

                var ema13Length = IndicatorResult [ "EMA13" ].Count;

                if ( ema13Length > 1 )
                {
                    _lastEma13Value    = IndicatorResult [ "EMA13" ] [ ema13Length - 2 ];
                    _currentEma13Value = IndicatorResult [ "EMA13" ] [ ema13Length - 1 ];

                    if ( _fxTradingEventsBindingList == null )
                    {
                        var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

                        if ( aa == null )
                            return;

                        _fxTradingEventsBindingList = aa.TradingEventsBindingList;
                    }

                    _fxTradingEventsBindingList.AddEmaValues( Bars.Period.Value, curIterationBarcount, _currentEma13Value, _lastEma13Value, curIterationBarcount );
                }                
            }            
        }


        protected linesCrossEnum LineCross( double prevFast, double prevSlow, double currentFast, double currentSlow )
        {            
            linesCrossEnum flag= linesCrossEnum.Unknown;

            if( ( currentFast > currentSlow ) && ( prevFast > prevSlow ) )
                flag = linesCrossEnum.UpTrend;

            if ( ( currentFast < currentSlow ) && ( prevFast < prevSlow ) )
                flag = linesCrossEnum.DownTrend;

            if( ( currentFast > currentSlow ) && ( prevFast < prevSlow ) )
                flag = linesCrossEnum.CrossUp;

            if ( ( currentFast < currentSlow ) && ( prevFast > prevSlow ) )
                flag = linesCrossEnum.CrossDown;

            return ( flag );
        } 

        protected void GetEMAsCrossover( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return;
            }

            _hasNewEmaCross = false;

            double[] ema5, ema13, ema144;
                        
            ema5   = GeneralHelper.EnumerableToArray( IndicatorResult[ "EMA5" ] );
            ema13  = GeneralHelper.EnumerableToArray( IndicatorResult[ "EMA13" ] );
            ema144 = GeneralHelper.EnumerableToArray( IndicatorResult[ "EMA144" ] );

            if ( ema5.Length == 0 || ema13.Length == 0 || ema144.Length == 0 )
            {
                return;
            }

            if ( ( ema5.Length != ema13.Length ) || ( ema5.Length != ema144.Length ) )
            {
                return;
            }


            int startingIndex = Math.Max(1, _lastEmaCrossIndex - 5);                
            int endIndex      = ema144.Length;
                

            _lastEmaDirection = MarketDirection.Unknown;            

            if ( Bars.Period.Value == TimeSpan.FromHours( 1 ) )
            {

            }

            for ( int k = startingIndex; k < curIterationBarcount; k++ )
            {
                var currentBar = Bars.GetBarByIndex( k );

                bool foundTrend = false;

                var currentEma5    = ema5[ k ];
                var currentEma13   = ema13[ k ];
                var currentEma144  = ema144[ k ];

                var previousEma5   = ema5[ k - 1 ];
                var previousEma13  = ema13[ k - 1 ];
                var previousEma144 = ema144[ k - 1 ];

                var mainTrend      = LineCross( previousEma13, previousEma144, currentEma13, currentEma144 );
                var fastTrend      = LineCross( previousEma5, previousEma13, currentEma5, currentEma13 );

                if ( mainTrend == linesCrossEnum.UpTrend && fastTrend == linesCrossEnum.CrossUp )
                {
                    if ( _lastEmaDirection != MarketDirection.Bullish )
                    {
                        StoreLastEmaCrossSignal( k, currentBar.BarTime, currentEma5, currentEma13, currentEma144, MarketDirection.Bullish );                        
                        foundTrend = true;
                    }
                }

                if ( mainTrend == linesCrossEnum.DownTrend && fastTrend == linesCrossEnum.CrossDown )
                {
                    if ( _lastEmaDirection != MarketDirection.Bearish )
                    {
                        StoreLastEmaCrossSignal( k, currentBar.BarTime, currentEma5, currentEma13, currentEma144, MarketDirection.Bearish );                        
                        foundTrend         = true;
                    }
                }


                // sometimes,the market moves fast in one way.In this case,MA1 will cross MA3 earlier than MA2 cross MA3
                // when MA2 is crossing MA3,the MA1 is on the blow or above of MA3 
                // so there are 2 cases
                // case1:
                var suddenTrend       = LineCross( previousEma5, previousEma144, currentEma5, currentEma144 );

                if ( ( suddenTrend == linesCrossEnum.DownTrend ) && ( mainTrend == linesCrossEnum.CrossDown ) )
                {
                    if ( _lastEmaDirection != MarketDirection.Bearish )
                    {
                        StoreLastEmaCrossSignal( k, currentBar.BarTime, currentEma5, currentEma13, currentEma144, MarketDirection.Bearish );
                        foundTrend = true;
                    }
                }

                if ( ( suddenTrend == linesCrossEnum.UpTrend ) && ( mainTrend == linesCrossEnum.CrossUp ) )
                {
                    if ( _lastEmaDirection != MarketDirection.Bullish )
                    {
                        StoreLastEmaCrossSignal( k, currentBar.BarTime, currentEma5, currentEma13, currentEma144, MarketDirection.Bullish );                        
                        foundTrend = true;
                    }
                }

                if ( foundTrend == false )
                {
                    if ( mainTrend == linesCrossEnum.UpTrend && fastTrend == linesCrossEnum.CrossDown )
                    {
                        StoreLastEmaCrossSignal( k, currentBar.BarTime, currentEma5, currentEma13, currentEma144, MarketDirection.BullishCorrection );
                        //_lastEmaDirection =  MarketDirection.BullishCorrection;                        
                    }

                    if ( mainTrend == linesCrossEnum.DownTrend && fastTrend == linesCrossEnum.CrossUp )
                    {
                        StoreLastEmaCrossSignal( k, currentBar.BarTime, currentEma5, currentEma13, currentEma144, MarketDirection.BearishCorrection );
                        //_lastEmaDirection = MarketDirection.BearishCorrection;                        
                    }
                }
            }

            if ( _lastEmaCrossIndex > 1 )
            {
                var newEvent = new EmaCrossEventArgs( Bars.Security.Code, Bars.Period.Value, _lastEmaCrossTime, _lastEmaCrossIndex, _lastEmaDirection, curIterationBarcount, _lastEma5Value, _lastEma13Value, _lastEma144Value );

                if ( _lastEmaCrossEvent != newEvent )
                {
                    _hasNewEmaCross = true;
                    _lastEmaCrossEvent = newEvent;

                    var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

                    if ( aa == null )
                        return;

                    aa.AddEmaCrossSignal( _lastEmaCrossEvent );
                }
            }            
        }

        protected void StoreLastEmaCrossSignal( int k, DateTime barTime, double ema5, double ema13, double ema144, MarketDirection direction )
        {
            _lastEmaCrossIndex = k;
            _lastEmaCrossTime  = barTime;
            _lastEma5Value     = ema5;
            _lastEma13Value    = ema13;
            _lastEma144Value   = ema144;
            _lastEmaDirection  = direction;
        }

    }
}
