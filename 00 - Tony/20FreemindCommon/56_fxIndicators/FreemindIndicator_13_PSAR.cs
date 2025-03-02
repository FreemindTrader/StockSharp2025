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
        double _PSAR_Step = 0.001;
        double _PSAR_Max = 0.2;


        protected double GetSarStep( TimeSpan responsibleForWhatTimeFrame )
        {
            if ( responsibleForWhatTimeFrame == TimeSpan.FromDays( 30 ) )
            {
                return 0.2;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromDays( 7 ) )
            {
                return 0.2;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromDays( 1 ) )
            {
                return 0.2;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 4 ) )
            {
                return 0.01;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 2 ) )
            {
                return 0.006;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 1 ) )
            {
                return 0.005;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 30 ) )
            {
                return 0.004;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 15 ) )
            {
                return 0.002;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 5 ) )
            {
                return 0.001;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 1 ) )
            {
                return 0.001;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromTicks( 1 ) )
            {
                return 0.001;
            }

            return 0.001;
        }

        protected Task BasicSarTask( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount, ref PooledList<Task> tasksList )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }


            //ThreadHelper.UpdateThreadName( "BasicSarTask" );
            

            Task first = new Task(() => CalculatePSAR( fullRecalculation, updateType, curIterationBarcount ), IndicatorExitToken);
            Task second = first.ContinueWith(
                                                antecedent =>
                                                {
                                                    if (antecedent.Status == TaskStatus.Faulted)
                                                    {
                                                        this.LogError("CalculateOscillator() caused Exception. Stack = " + antecedent.Exception.InnerException.StackTrace);
                                                    }
                                                    else
                                                    {
                                                        GetSarDirection( fullRecalculation, updateType );
                                                    }

                                                }, IndicatorExitToken);

            tasksList.Add( first );
            tasksList.Add( second );

            return first;
        }

        protected void CalculatePSAR( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount )
        {
            int resultSetLength   = IndicatorResult["K"].Count;

            if ( resultSetLength == 0 )
            {
                ProcessPSarNewIndicatorBuffer( curIterationBarcount );
            }
            else
            {
                ProcessPSarExistingBuffer( updateType, curIterationBarcount, resultSetLength );
            }         
        }

        private void ProcessPSarExistingBuffer( DataBarUpdateType? updateType, int barB4Calculation, int resultSetLength )
        {
            var startIndex = resultSetLength - 1;
            var endIndex   = barB4Calculation - 1;
            var indexCount = endIndex - startIndex + 1;

            var period = Bars.Period.Value;

            _PSAR_Step = GetSarStep( period );

            int outBeginIdx       = 0;
            int outNBElement     = 0;

            if ( endIndex < 0 || indexCount < 0 )
            {
                return;
            }

            double[] pSAR   = new double[indexCount];

            Core.SarExt( Bars, startIndex, endIndex, pSAR, out outBeginIdx, out outNBElement, 0, 0, _PSAR_Step, _PSAR_Step, _PSAR_Max, _PSAR_Step, _PSAR_Step, _PSAR_Max );

            IndicatorResult.AddSetValues( "PSAR", outBeginIdx, outNBElement, true, pSAR );
        }

        private void ProcessPSarNewIndicatorBuffer( int barB4Calculation )
        {
            int startIndex     = 0;
            int endIndex       = barB4Calculation - 1;
            int indexCount     = endIndex - startIndex + 1;

            var period = Bars.Period.Value;

            _PSAR_Step = GetSarStep( period );

            int outBeginIdx       = 0;
            int outNBElement     = 0;

            if ( endIndex < 0 || indexCount < 0 )
            {
                return;
            }

            double[] pSAR   = new double[indexCount];

            Core.SarExt( Bars, startIndex, endIndex, pSAR, out outBeginIdx, out outNBElement, 0, 0, _PSAR_Step, _PSAR_Step, _PSAR_Max, _PSAR_Step, _PSAR_Step, _PSAR_Max );

            IndicatorResult.AddSetValues( "PSAR", outBeginIdx, outNBElement, true, pSAR );
        }

        protected void GetSarDirection( bool fullRecalculation, DataBarUpdateType? updateType )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return;
            }

            var period = Bars.Period.Value;

            if ( period == TimeSpan.FromMinutes( 1 ) )
            {

            }

            var barB4Calculation = _barCountBeforeCalculation;
            var psar = GeneralHelper.EnumerableToArray( IndicatorResult[ "PSAR" ] );
            

            if ( psar.Length == 0 )
            {
                return;
            }

            int startingIndex = Math.Max(1, _indexOfLastSarBreak - 5);
            int endIndex = psar.Length;
            
            
            int signalBar = 0;
            var direction = MarketDirection.Unknown;

            for ( int k = startingIndex; k < endIndex; k++ )
            {
                var PSARCurrent = Math.Abs( psar[ k ] );

                if ( Bars[ k ].Close > PSARCurrent )
                {
                    if ( direction != MarketDirection.Bullish )
                    {
                        direction = MarketDirection.Bullish;                        
                        signalBar = k;

                        _indexOfSecondLastSarBreak = _indexOfLastSarBreak;                        
                        _indexOfLastSarBreak = k;
                    }
                }
                else if ( Bars[ k ].Close < PSARCurrent )
                {
                    if ( direction != MarketDirection.Bearish )
                    {
                        direction = MarketDirection.Bearish;                        
                        signalBar = k;

                        _indexOfSecondLastSarBreak = _indexOfLastSarBreak;
                        _indexOfLastSarBreak = k;
                    }
                }
            }

            if ( period == TimeSpan.FromMinutes( 1 ) )
            {

            }

            if ( direction != MarketDirection.Unknown )
            {
                if ( _fxTradingEventsBindingList == null )
                {
                    var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

                    if ( aa == null )
                        return;

                    _fxTradingEventsBindingList = aa.TradingEventsBindingList;
                }

                _fxTradingEventsBindingList.AddPsarSignal( Bars.Period.Value, signalBar, direction, barB4Calculation );
            }
        }
    



        
    }
}
