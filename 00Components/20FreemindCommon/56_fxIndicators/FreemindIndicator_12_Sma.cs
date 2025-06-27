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
        private int _smaLookback = -1;

        protected Task BasicSmaTask( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount, ref PooledList<Task> tasksList )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            //ThreadHelper.UpdateThreadName( "BasicSmaTask" );
            

            Task first = new Task(() => CalculateSMA( fullRecalculation, updateType, curIterationBarcount ), IndicatorExitToken);
            Task second = first.ContinueWith(
                                                antecedent =>
                                                {
                                                    if (antecedent.Status == TaskStatus.Faulted)
                                                    {
                                                        this.LogError("CalculateSMA() caused Exception. Stack = " + antecedent.Exception.InnerException.StackTrace);
                                                    }
                                                    else
                                                    {
                                                        GetSMACrossover( fullRecalculation, updateType, curIterationBarcount );
                                                    }

                                                }, IndicatorExitToken);

            tasksList.Add( first );
            tasksList.Add( second );

            return first;
        }

        protected void CalculateSMA( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount )
        {
            _smaLookback = Core.SmaLookback( 55 );

            int smaLength = IndicatorResult[ "SMA" ].Count;

            if ( smaLength == 0 )
            {
                ProcessNewSmaIndicatorBuffer( curIterationBarcount );
            }
            else
            {
                ProcessExistingSmaBuffer( updateType, curIterationBarcount, smaLength );
            }
        }

        private void ProcessNewSmaIndicatorBuffer( int barB4Calculation )
        {
            int startIndex  = 0;
            int endIndex    = barB4Calculation - 1;
            int indexCount  = endIndex - startIndex + 1;

            var sma        = new double[ indexCount ];

            var outBeginIdx = 0;
            var outNBElement = 0;

            Core.Sma( Bars, startIndex, endIndex, sma, out outBeginIdx, out outNBElement, 55 );

            if ( outNBElement > 0 )
            {
                lock ( IndicatorResult )
                {
                    IndicatorResult.AddSetValues( "SMA", outBeginIdx, outNBElement, true, sma );
                }

                SymbolsMgr.Instance.UpdateSma( Bars.Security, Bars.Period.Value, Bars.GetBarByIndex( endIndex ).BarTime );
            }
        }

        private void ProcessExistingSmaBuffer( DataBarUpdateType? updateType, int barB4Calculation, int resultSetLength )
        {
            var startIndex = resultSetLength - 1;
            var endIndex   = barB4Calculation - 1;
            var indexCount = endIndex - startIndex + 1;

            if ( endIndex < 0 || indexCount < 0 )
            {
                return;
            }

            var sma        = new double[ indexCount ];

            var outBeginIdx = 0;
            var outNBElement = 0;

            Core.Sma( Bars, startIndex, endIndex, sma, out outBeginIdx, out outNBElement, 55 );

            if ( outNBElement > 0 )
            {
                lock ( IndicatorResult )
                {
                    IndicatorResult.AddSetValues( "SMA", outBeginIdx, outNBElement, true, sma );
                }

                SymbolsMgr.Instance.UpdateSma( Bars.Security, Bars.Period.Value, Bars.GetBarByIndex( endIndex ).BarTime );
            }
        }        

        protected void GetSMACrossover( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return;
            }

            _hasNewSmaCross = false;
                                      
            

            var sma = GeneralHelper.EnumerableToArray( IndicatorResult[ "SMA" ] );

            if ( sma.Length == 0  )
            {
                return;
            }

            int startIndex    = Math.Max(1, _lastSmaCrossIndex - 5);
            int endIndex      = Math.Min( curIterationBarcount, sma.Length );
            int indexCount    = endIndex - startIndex + 1;            

            
            if ( indexCount < 0 )
                return;

            
            

            _lastSmaAboveCount = 0;
            _lastSmaBelowCount = 0;

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

            if ( aa == null )
                return;

            int requiredCount = aa.GetNumOfBarsForSmaCross( _currentPeriod );


            _lastSmaCrossSignal = MarketDirection.Unknown;

            _lastSmaCrossIndex = -1;

            double currentSma = 0.0;
            double previousSma = 0.0;

            for ( int i = startIndex; i < endIndex; i++ )
            {
                var prevIndex = i - 1;

                var currentBar = Bars.GetBarByIndex( i );

                if ( prevIndex >= 0 )
                {
                    previousSma = sma[ prevIndex];
                }

                currentSma = sma[ i ];

                if ( Bars[ i ].Close > currentSma )
                {
                    _lastSmaAboveCount++;
                    _lastSmaBelowCount = 0;
                }
                else if ( Bars[ i ].Close < currentSma )
                {
                    _lastSmaAboveCount = 0;
                    _lastSmaBelowCount++;
                }

                if ( _lastSmaAboveCount >= requiredCount )
                {
                    if ( currentSma > previousSma )
                    {
                        if ( _lastSmaCrossSignal != MarketDirection.Bullish)
                        {
                            StoreLastSmaCrossSignal( i, currentBar.BarTime, currentSma, _lastSmaAboveCount, 0, MarketDirection.Bullish );                                
                        }
                    }                       
                }

                if ( _lastSmaBelowCount >= requiredCount )
                {
                    if (currentSma < previousSma)
                    {
                        if ( _lastSmaCrossSignal != MarketDirection.Bearish)
                        {
                            StoreLastSmaCrossSignal( i, currentBar.BarTime, currentSma, 0, _lastSmaBelowCount, MarketDirection.Bearish );                                
                        }
                    }
                }
            }

            if ( _lastSmaCrossIndex  > 1 )
            {
                var newEvent = new SmaCrossEventArgs( Bars.Security.Code, Bars.Period.Value, _lastSmaCrossTime, _lastSmaCrossIndex, _lastSmaCrossSignal, curIterationBarcount, _lastSma55Value, _lastSmaAboveCount, _lastSmaBelowCount );

                if ( _lastSmaCrossEvent != newEvent )
                {
                    _hasNewSmaCross = true;
                    _lastSmaCrossEvent = newEvent;

                    if ( aa == null )
                        return;

                    aa.AddSmaCrossSignal( _lastSmaCrossEvent );
                }
            }                
            
        }

        protected void StoreLastSmaCrossSignal( int k, DateTime barTime, double sma55, int barAbove, int barBelow, MarketDirection direction )
        {
            _lastSmaCrossIndex = k;
            _lastSmaCrossTime  = barTime;
            _lastSma55Value    = sma55;
            _lastSmaBelowCount = barBelow;
            _lastSmaAboveCount = barAbove;
            _lastSmaCrossSignal = direction;
        }
    }
}
