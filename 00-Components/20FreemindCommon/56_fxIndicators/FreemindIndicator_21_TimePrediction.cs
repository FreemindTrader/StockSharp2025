using fx.Common;
using fx.Bars;

using DevExpress.Mvvm;

using fx.Definitions;
using fx.Algorithm;
using System;
using fx.Collections;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using fx.TALib;
using StockSharp.BusinessEntities;
using System.Threading;
using fx.Definitions.Collections;
using System.Collections.Generic;

#pragma warning disable 414

namespace fx.Indicators
{
    public partial class FreemindIndicator : CustomPlatformIndicator
    {
        protected Task AdvancedWaveRotationTasks( bool fullRecalculation, DataBarUpdateType? updateType, ref PooledList<Task> tasksList )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            Task first = new Task(() => DetectWaveRotationFromZigZag( fullRecalculation, updateType, IndicatorExitToken ), IndicatorExitToken);

            tasksList.Add( first );

            return first;
        }
        protected void DetectWaveRotationFromZigZag( bool fullRecalculation, DataBarUpdateType? updateType, CancellationToken token )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return;
            }

            //ThreadHelper.UpdateThreadName( "DetectWaveRotationFromZigZag" );

            var barB4Calculation = _barCountBeforeCalculation;
            //var lastSignal       = TASignal.NONE;
            //var ithBar           = -1;
            //var foundDivergence  = false;

            var symbol           = Bars.Security;
            var period           = Bars.Period.Value;

            if ( _hews != null )
            {
                var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( symbol );
                if ( aa == null )
                    return;

                var taManager = ( PeriodXTaManager )aa.GetPeriodXTa( period );
                var copy  = _hews.GetAscendingWaveImportanceClone(period);


                DetectMostRecentWaveRotation( symbol, period, taManager, copy, token );
                DetectLongTermR2RWaveRotation( symbol, period, taManager, copy, token );
                DetectShortTermR2RWaveRotation( symbol, period, taManager, copy, token );
            }
        }



        protected void DetectLongTermR2RWaveRotation( Security symbol, TimeSpan period, PeriodXTaManager taManager, BTreeDictionary<long, WavePointImportance> waveImportance, CancellationToken token )
        {
            var waves           = waveImportance.Where(x => x.Value.WaveImportance >= GlobalConstants.HRS08IMPT ).ToArray();
            var peakTroughCount = waves.Count();

            if ( period == TimeSpan.FromMinutes( 5 ) )
            {

            }

            for ( int i = peakTroughCount - 1; i >= 0; i-- )
            {
                if ( ( i - 3 ) >= 0 && i > _last3rdRedIndex )
                {
                    if ( token.IsCancellationRequested )
                    {
                        token.ThrowIfCancellationRequested( );
                    }

                    var first  = waves[ i ];
                    var second = waves[ i - 1 ];
                    var third  = waves[ i - 2 ];
                    var fourth = waves[ i - 3 ];

                    ref SBar latestBar = ref Bars.GetBarByTime( first.Key );

                    if ( latestBar.BarIndex >= _lastMacdCrossIndex ) continue;

                    CheckForWaveRotation( period, taManager, first, second, true, token );     // Here we check wave rotation for Peak to Trough
                    CheckForWaveRotation( period, taManager, first, third, true, token );      // Here we check wave rotation for Peak to Peak or Trough to trough
                    CheckForAbcWaveRotation( period, taManager, first, second, third, fourth, true, token );
                }
            }
        }

        protected void DetectShortTermR2RWaveRotation( Security symbol, TimeSpan period, PeriodXTaManager taManager, BTreeDictionary<long, WavePointImportance> waveImportance, CancellationToken token )
        {
            var redWaves = waveImportance.Where(x => x.Value.WaveImportance >= GlobalConstants.HRS08IMPT ).ToArray();

            var redWaveCount = redWaves.Count();


            for ( int i = 0; i < redWaveCount; i++ )
            {
                if ( ( i + 1 ) < redWaveCount && ( i + 1 ) > _last3rdRedIndex )
                {
                    var latest   = redWaves[i + 1];
                    var previous = redWaves[i];

                    //var debugBar = DatabarsRepo.GetBarByTime(previous.Key);

                    //if ( debugBar.BarIndex > 8735 && period == TimeSpan.FromMinutes( 5 ) )
                    //{

                    //}

                    if ( latest.Value.Signal == TASignal.WAVE_PEAK && previous.Value.Signal == TASignal.WAVE_TROUGH )
                    {
                        DetectLocalWaveRotationInUptrend( symbol, period, taManager, previous, latest, waveImportance, token );
                    }
                    // This is a local downtrend
                    else if ( latest.Value.Signal == TASignal.WAVE_TROUGH && previous.Value.Signal == TASignal.WAVE_PEAK )
                    {
                        DetectLocalWaveRotationInDowntrend( symbol, period, taManager, previous, latest, waveImportance, token );
                    }



                }
            }
        }

        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\WaveRotationLogic.png" />
        private void DetectLocalWaveRotationInDowntrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> previous, KeyValuePair<long, WavePointImportance> latest, BTreeDictionary<long, WavePointImportance> waveImportance, CancellationToken token )
        {
            /* --------------------------------------------------------------------------------------------------------------------------
             * 
             * First find all the local troughs with respect to the TOP
             * 1. Firstly, we need to determine the total number of bars from the TOP for all the troughs
             * 
             * --------------------------------------------------------------------------------------------------------------------------
            */
            var innerLowerLowTroughs = GetLowerLowTroughsBtwXY_wLatest( period, waveImportance, latest, previous );
            var troughsCount         = innerLowerLowTroughs.Count();
            int j                    = troughsCount - 1;

            while ( j >= 0 )
            {
                if ( token.IsCancellationRequested )
                {
                    token.ThrowIfCancellationRequested( );
                }

                var currentTrough = innerLowerLowTroughs[j];

                CheckForWaveRotation( period, taManager, currentTrough, previous, TaWaveRotation.LOCAL_BEAR_TOTALBARS );

                j--;
            }

            /* --------------------------------------------------------------------------------------------------------------------------
             * 
             * 2. Find all the local Top with respect to the TOP, and keep tracks of the count
             *              
             * --------------------------------------------------------------------------------------------------------------------------
            */
            var newInnerLowerLowPeaks = GetLowerLowPeaksBtwXY_NoLatest( period, waveImportance, latest, previous );
            var peaksCount            = newInnerLowerLowPeaks.Count();
            j = peaksCount - 1;
            while ( j >= 0 )
            {
                if ( token.IsCancellationRequested )
                {
                    token.ThrowIfCancellationRequested( );
                }

                var currentPeak = newInnerLowerLowPeaks[j];

                CheckForWaveRotation( period, taManager, currentPeak, previous, TaWaveRotation.LOCAL_BEAR_TOTALBARS );

                j--;
            }

            /* --------------------------------------------------------------------------------------------------------------------------
             * 
             * 3. Find all the short term bounce with respect to its troughs. Here we find all the highest peaks between two troughs
             *              
             * --------------------------------------------------------------------------------------------------------------------------
            */
            troughsCount = innerLowerLowTroughs.Count( );
            j = troughsCount - 1;

            while ( j >= 0 )
            {
                var currentTrough = innerLowerLowTroughs[j];
                var n = j - 1;

                if ( n >= 0 )
                {
                    if ( token.IsCancellationRequested )
                    {
                        token.ThrowIfCancellationRequested( );
                    }

                    var previousTrough = innerLowerLowTroughs[n];

                    var inBtwPeaks = GetExtremumBetweenXY( waveImportance, currentTrough, previousTrough, TASignal.WAVE_PEAK );

                    foreach ( KeyValuePair<long, WavePointImportance> peak in inBtwPeaks )
                    {
                        CheckForWaveRotation( period, taManager, peak, previousTrough, TaWaveRotation.LOCAL_BEAR_CORRECTION );
                    }

                    n--;
                }

                j--;
            }
        }



        private void DetectLocalWaveRotationInUptrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> previous, KeyValuePair<long, WavePointImportance> latest, BTreeDictionary<long, WavePointImportance> waveImportance, CancellationToken token )
        {
            /* --------------------------------------------------------------------------------------------------------------------------
             * 
             * First find all the local Top with respect to the BOTTOM
             * 1. Firstly, we need to determine the total number of bars from the BOTTOM for all the local TOP
             * 
             * --------------------------------------------------------------------------------------------------------------------------
            */

            var innerHigherHighPeaks = GetHigherHighPeaksBtwXY_wLatest( period, waveImportance, latest, previous );
            var peaksCount           = innerHigherHighPeaks.Count();
            int j                    = peaksCount - 1;

            while ( j >= 0 )
            {
                var currentPeak = innerHigherHighPeaks[j];

                if ( token.IsCancellationRequested )
                {
                    token.ThrowIfCancellationRequested( );
                }

                CheckForWaveRotation( period, taManager, currentPeak, previous, TaWaveRotation.LOCAL_BULL_TOTALBARS );

                j--;
            }

            /* --------------------------------------------------------------------------------------------------------------------------
             * 
             * 2. Find all the local Top with respect to the TOP, and keep tracks of the count
             *              
             * --------------------------------------------------------------------------------------------------------------------------
            */
            var innerTroughs = GetHigherHighTroughsBtwXY_NoLatest( period, waveImportance, latest, previous );
            var troughsCount = innerTroughs.Count();
            j = troughsCount - 1;

            while ( j >= 0 )
            {
                if ( token.IsCancellationRequested )
                {
                    token.ThrowIfCancellationRequested( );
                }

                var currentTrough = innerTroughs[j];

                CheckForWaveRotation( period, taManager, currentTrough, previous, TaWaveRotation.LOCAL_BULL_TOTALBARS );

                j--;
            }

            /* --------------------------------------------------------------------------------------------------------------------------
             * 
             * 3. Find all the short term Drop with respect to its LOCAL TOPS. Here we find all the lowest Troughs between two PEAKS
             *              
             * --------------------------------------------------------------------------------------------------------------------------
            */
            peaksCount = innerHigherHighPeaks.Count( );
            j = peaksCount - 1;

            while ( j >= 0 )
            {
                var currentPeak = innerHigherHighPeaks[j];
                var n = j - 1;

                if ( n >= 0 )
                {
                    if ( token.IsCancellationRequested )
                    {
                        token.ThrowIfCancellationRequested( );
                    }

                    var previousPeak = innerHigherHighPeaks[n];
                    var inBtwTroughs = GetExtremumBetweenXY( waveImportance, currentPeak, previousPeak, TASignal.WAVE_TROUGH );

                    foreach ( KeyValuePair<long, WavePointImportance> trough in inBtwTroughs )
                    {
                        CheckForWaveRotation( period, taManager, trough, previousPeak, TaWaveRotation.LOCAL_BULL_CORRECTION );
                    }

                    n--;
                }

                j--;
            }
        }




        protected void DetectMostRecentWaveRotation( Security symbol, TimeSpan period, PeriodXTaManager taManager, BTreeDictionary<long, WavePointImportance> waveImportance, CancellationToken token )
        {
            /* -------------------------------------------------------------------------------------------------------------------------------------------------------
             * 
             * For the most recent databars, we need to check for both positive and negative divergence as this might be the double top or the double bottom.
             * 
             * -------------------------------------------------------------------------------------------------------------------------------------------------------
             */

            var redWaves             = waveImportance.Where(x => x.Value.WaveImportance >= GlobalConstants.DAILYIMPT ).ToArray();
            var redWaveCount         = redWaves.Count();

            if ( redWaveCount < 1 ) return;

            var mostRecentRed        = redWaves[ redWaveCount - 1 ];

            var macdSignificantCross = _macdSignificantCross;

            int lastMacdCross        = -1;

            if ( macdSignificantCross.Count > 0 )
            {
                lastMacdCross = macdSignificantCross.Keys[ macdSignificantCross.Count - 1 ];
            }

            ref SBar bar = ref Bars.GetBarByTime( mostRecentRed.Key );

            if ( period == TimeSpan.FromHours( 2 ) )
            {

            }

            if ( lastMacdCross > bar.BarIndex )
            {
                // The last Macd cross is newer than our last red dot. When there is no cross, all calculation is not important.
                var newestWaves          = waveImportance.Where(x => x.Key >= mostRecentRed.Key  ).OrderBy(x => x.Key).ToArray();

                /*
                 *  DetectLongTermR2RDivergence( symbol, period, taManager, waveImportance );
                    DetectShortTermLocalDivergence( symbol, period, taManager, waveImportance );
                    DetectLongTermR2RHiddenDivergence( symbol, period, taManager, waveImportance );
                    DetectShortTermHiddenDivergence( symbol, period, taManager, waveImportance ); 
                 * 
                 * 
                 */

                if ( newestWaves.Count( ) > 1 )
                {
                    if ( mostRecentRed.Value.Signal == TASignal.WAVE_PEAK )
                    {
                        // Here we have a Local down Trend

                    }
                    else if ( mostRecentRed.Value.Signal == TASignal.WAVE_TROUGH )
                    {
                        // Here we have a Local Up Trend
                    }
                }
            }
        }
    }
}
