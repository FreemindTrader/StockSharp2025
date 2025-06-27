
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
        private int _macdLookback = -1;
        private double _latestMacd = 0.0;
        private double _latestMacdSig = 0.0;

        protected Task BasicMacdTask( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount, ref PooledList<Task> tasksList )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            //ThreadHelper.UpdateThreadName( "BasicMacdTask" );

            Task first = new Task(() => CalculateMACD( fullRecalculation, updateType, curIterationBarcount, IndicatorExitToken ), IndicatorExitToken);
            Task second = first.ContinueWith(
                                                antecedent =>
                                                {
                                                    if (antecedent.Status == TaskStatus.Faulted)
                                                    {
                                                        this.LogError("CalculateMACD() caused Exception. Stack = " + antecedent.Exception.InnerException.StackTrace);
                                                    }
                                                    else
                                                    {
                                                        GetMACDCrossOver( fullRecalculation, updateType, curIterationBarcount, IndicatorExitToken );
                                                    }

                                                }, IndicatorExitToken);

            tasksList.Add( first );
            tasksList.Add( second );

            return first;
        }

        protected void CalculateMACD( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount, CancellationToken token )
        {
            if ( Bars.Period < TimeSpan.FromHours( 1 ) )
            {
                _macdLookback = Core.MacdExtLookback( Core.MAType.Ema, Core.MAType.Ema, Core.MAType.Ema, 20, 40, 10 );
            }
            else
            {
                _macdLookback = Core.MacdExtLookback( Core.MAType.Ema, Core.MAType.Ema, Core.MAType.Ema, 12, 26, 9 );
            }



            int macdLength = IndicatorResult["MACD"].Count;

            if ( macdLength == 0 )
            {
                ProcessNewMacdIndicatorBuffer( curIterationBarcount );
            }
            else
            {
                ProcessExistingMacdBuffer( updateType, curIterationBarcount, macdLength );
            }
        }

        private void ProcessNewMacdIndicatorBuffer( int barB4Calculation )
        {
            
            int startIndex  = 0;
            int endIndex    = barB4Calculation - 1;
            int indexCount  = endIndex - startIndex + 1;

            var macd        = new double[ indexCount ];
            var macdSignal  = new double[ indexCount ];
            var macdHistory = new double[ indexCount ];

            
            long unstablePeriod = 120;
            Core.RetCode retCode;

            if ( _macdLookback > barB4Calculation )
            {
                unstablePeriod = Core.GetUnstablePeriod( Core.FuncUnstId.Ema );

                retCode = Core.SetUnstablePeriod( Core.FuncUnstId.Ema, 30 );

                if ( retCode != Core.RetCode.Success )
                {
                    return;
                }
            }

            var outBeginIdx = 0;
            var outNBElement = 0;

            if ( Bars.Period < TimeSpan.FromHours( 1 ) )
            {
                Core.MacdExt( Bars, startIndex, endIndex, macd, macdSignal, macdHistory, out outBeginIdx, out outNBElement, Core.MAType.Ema, Core.MAType.Ema, Core.MAType.Ema, 20, 40, 10 );
            }
            else
            {
                Core.MacdExt( Bars, startIndex, endIndex, macd, macdSignal, macdHistory, out outBeginIdx, out outNBElement, Core.MAType.Ema, Core.MAType.Ema, Core.MAType.Ema, 12, 26, 9 );
            }

            if ( _macdLookback > barB4Calculation )
            {
                Core.SetUnstablePeriod( Core.FuncUnstId.Ema, unstablePeriod );
            }

            if ( outNBElement > 0 )
            {
                IndicatorResult.AddSetValues( "MACD", outBeginIdx, outNBElement, true, macd );
                IndicatorResult.AddSetValues( "MACDSignal", outBeginIdx, outNBElement, true, macdSignal );

                var lastMacd    = macd[ outNBElement - 1 ];
                var lastMacdSig = macdSignal[ outNBElement - 1 ];

                if ( ( lastMacd != _latestMacd ) && ( lastMacdSig != _latestMacdSig ) )
                {
                    _latestMacd = lastMacd;
                    _latestMacdSig = lastMacdSig;
                    var lastIndex = outBeginIdx + outNBElement -1;

                    SymbolsMgr.Instance.UpdateMacd( Bars.Security, Bars.Period.Value, Bars.GetBarByIndex( lastIndex ).BarTime, _latestMacd, _latestMacdSig );
                }


            }
        }

        

        private void ProcessExistingMacdBuffer( DataBarUpdateType? updateType, int barB4Calculation, int resultSetLength )
        {
            // Since we are giving the whole IndicatorBarsRepo to the TaLib function, and its internal code
            // will take care of the loopBack so there is no need to include the loopback in the calcualtion

            var startIndex = resultSetLength - 1;
            var endIndex   = barB4Calculation - 1;
            var indexCount = endIndex - startIndex + 1;

            if ( endIndex < 0 || indexCount < 0 )
            {
                return;
            }

            int outBeginIdx  = 0;
            int outNBElement = 0;

            long unstablePeriod = 120;
            Core.RetCode retCode;

            if ( _macdLookback > barB4Calculation )
            {
                unstablePeriod = Core.GetUnstablePeriod( Core.FuncUnstId.Ema );

                retCode = Core.SetUnstablePeriod( Core.FuncUnstId.Ema, 30 );

                if ( retCode != Core.RetCode.Success )
                {
                    return;
                }
            }

            var macd        = new double[ indexCount ];
            var macdSignal  = new double[ indexCount ];
            var macdHistory = new double[ indexCount ];
            

            if ( Bars.Period < TimeSpan.FromHours( 1 ) )
            {
                Core.MacdExt( Bars, startIndex, endIndex, macd, macdSignal, macdHistory, out outBeginIdx, out outNBElement, Core.MAType.Ema, Core.MAType.Ema, Core.MAType.Ema, 20, 40, 10 );
            }
            else
            {
                Core.MacdExt( Bars, startIndex, endIndex, macd, macdSignal, macdHistory, out outBeginIdx, out outNBElement, Core.MAType.Ema, Core.MAType.Ema, Core.MAType.Ema, 12, 26, 9 );
            }

            if ( _macdLookback > barB4Calculation )
            {
                Core.SetUnstablePeriod( Core.FuncUnstId.Ema, unstablePeriod );
            }

            if ( outNBElement > 0 )
            {
                IndicatorResult.AddSetValues( "MACD",       outBeginIdx, outNBElement, true, macd );
                IndicatorResult.AddSetValues( "MACDSignal", outBeginIdx, outNBElement, true, macdSignal );

                var lastMacd    = macd[ outNBElement - 1 ];
                var lastMacdSig = macdSignal[ outNBElement - 1 ];

                if ( ( lastMacd != _latestMacd ) && ( lastMacdSig != _latestMacdSig ) )
                {
                    _latestMacd = lastMacd;
                    _latestMacdSig = lastMacdSig;
                    var lastIndex = outBeginIdx + outNBElement -1;

                    SymbolsMgr.Instance.UpdateMacd( Bars.Security, Bars.Period.Value, Bars.GetBarByIndex( lastIndex ).BarTime, _latestMacd, _latestMacdSig );
                }
            }
        }
    }
}
