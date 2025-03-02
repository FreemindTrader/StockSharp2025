
using fx.Common;
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
        protected Task BasicAroonCrossTask( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount, ref PooledList<Task> tasksList )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            //ThreadHelper.UpdateThreadName( "BasicAroonCrossTask" );


            Task first = new Task(() => CalculateAroon(fullRecalculation, updateType, curIterationBarcount, IndicatorExitToken), IndicatorExitToken);
            Task second = first.ContinueWith(
                                                antecedent =>
                                                {
                                                    if (antecedent.Status == TaskStatus.Faulted)
                                                    {
                                                        this.LogError("CalculateEMAs() caused Exception. Stack = " + antecedent.Exception.InnerException.StackTrace);
                                                    }
                                                    else
                                                    {
                                                        GetAroonCrossover(fullRecalculation, updateType, curIterationBarcount, IndicatorExitToken);
                                                    }

                                                }, IndicatorExitToken);

            tasksList.Add( first );
            tasksList.Add( second );

            return first;
        }
        protected void CalculateAroon( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount, CancellationToken token )
        {
            int aroonLength = IndicatorResult["AroonUp"].Count;

            if ( aroonLength == 0 )
            {
                ProcessNewAroonIndicatorBuffer( curIterationBarcount, token );
            }
            else
            {
                ProcessExistingAroonBuffer( updateType, curIterationBarcount, aroonLength, token );
            }
        }

        private void ProcessNewAroonIndicatorBuffer( int barB4Calculation, CancellationToken token )
        {
            int startIndex = 0;
            int endIndex   = barB4Calculation - 1;
            int indexCount = endIndex - startIndex + 1;

            var aroonUp    = new double[indexCount];
            var aroonDown  = new double[indexCount];

            var outBeginIdx = 0;
            var outNBElement = 0;

            if ( token.IsCancellationRequested )
            {
                token.ThrowIfCancellationRequested();
            }

            Core.Aroon( Bars, startIndex, endIndex, aroonDown, aroonUp, out outBeginIdx, out outNBElement, 25 );

            lock ( IndicatorResult )
            {
                IndicatorResult.AddSetValues( "AroonUp", outBeginIdx, outNBElement, true, aroonUp );
                IndicatorResult.AddSetValues( "AroonDown", outBeginIdx, outNBElement, true, aroonDown );
            }
        }


        private void ProcessExistingAroonBuffer( DataBarUpdateType? updateType, int barB4Calculation, int resultSetLength, CancellationToken token )
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

            var aroonUp      = new double[indexCount];
            var aroonDown    = new double[indexCount];

            if ( token.IsCancellationRequested )
            {
                token.ThrowIfCancellationRequested();
            }

            Core.Aroon( Bars, startIndex, endIndex, aroonDown, aroonUp, out outBeginIdx, out outNBElement, 25 );

            if ( outNBElement > 0 )
            {
                lock ( IndicatorResult )
                {
                    IndicatorResult.AddSetValues( "AroonUp",   outBeginIdx, outNBElement, true, aroonUp );
                    IndicatorResult.AddSetValues( "AroonDown", outBeginIdx, outNBElement, true, aroonDown );
                }
            }
        }



        protected linesCrossEnum AroonCross( double prevUp, double prevDown, double currentUp, double currentDown )
        {
            linesCrossEnum flag = linesCrossEnum.Unknown;

            if ( ( currentUp > currentDown ) && ( prevUp > prevDown ) )
                flag = linesCrossEnum.UpTrend;

            if ( ( currentUp < currentDown ) && ( prevUp < prevDown ) )
                flag = linesCrossEnum.DownTrend;

            if ( ( currentUp > currentDown ) && ( prevUp < prevDown ) )
                flag = linesCrossEnum.CrossUp;

            if ( ( currentUp < currentDown ) && ( prevUp > prevDown ) )
                flag = linesCrossEnum.CrossDown;

            return ( flag );
        }


        protected void GetAroonCrossover( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount, CancellationToken token )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return;
            }


            double[] aroonUp, aroonDown;

            aroonUp   = GeneralHelper.EnumerableToArray( IndicatorResult[ "AroonUp" ]   );
            aroonDown = GeneralHelper.EnumerableToArray( IndicatorResult[ "AroonDown" ] );


            if ( aroonUp.Length == 0 || aroonDown.Length == 0 )
            {
                return;
            }

            if ( ( aroonUp.Length != aroonDown.Length ) )
            {
                return;
            }


            int startingIndex = Math.Max(1, _indexOfLastAroonCross - 5);
            int endIndex      = aroonUp.Length;


            var direction     = MarketDirection.Unknown;

            int signalBar     = 0;

            if ( Bars.Period.Value == TimeSpan.FromHours( 1 ) )
            {

            }

            for ( int k = startingIndex; k < curIterationBarcount; k++ )
            {
                if ( token.IsCancellationRequested )
                {
                    token.ThrowIfCancellationRequested();
                }

                var currentAroonUp    = aroonUp[k];
                var currentAroonDown  = aroonDown[k];

                var previousAroonUp   = aroonUp[k - 1];
                var previousAroonDown = aroonDown[k - 1];

                var fastTrend         = AroonCross(previousAroonUp, previousAroonDown, currentAroonUp, currentAroonDown);

                if ( fastTrend == linesCrossEnum.CrossUp )
                {
                    if ( direction != MarketDirection.Bullish )
                    {
                        direction = MarketDirection.Bullish;
                        signalBar = k;
                        _indexOfLastAroonCross = k;
                        //foundTrend             = true;
                    }
                }

                if ( fastTrend == linesCrossEnum.CrossDown )
                {
                    if ( direction != MarketDirection.Bearish )
                    {
                        direction = MarketDirection.Bearish;
                        signalBar = k;
                        _indexOfLastAroonCross = k;
                        //foundTrend             = true;
                    }
                }
            }

            if ( direction != MarketDirection.Unknown )
            {
                if ( Bars.Period.Value == TimeSpan.FromHours( 1 ) )
                {

                }

                if ( _fxTradingEventsBindingList == null )
                {
                    var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

                    if ( aa == null )
                        return;

                    _fxTradingEventsBindingList = aa.TradingEventsBindingList;
                }

                _fxTradingEventsBindingList.AddAroonCrossSignal( Bars.Period.Value, signalBar, direction, curIterationBarcount );
            }
        }

    }
}
