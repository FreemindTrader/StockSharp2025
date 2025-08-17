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
        protected Task AdvancedGannPriceTimeTasks( bool fullRecalculation, DataBarUpdateType? updateType, ref PooledList<Task> tasksList )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            Task first = new Task(() => DetectGannSquareFromZigZag( fullRecalculation, updateType, IndicatorExitToken ), IndicatorExitToken);

            tasksList.Add( first );

            return first;
        }

        protected void DetectGannSquareFromZigZag( bool fullRecalculation, DataBarUpdateType? updateType, CancellationToken token )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return;
            }

            //ThreadHelper.UpdateThreadName( "DetectGannSquareFromZigZag" );

            var symbol           = Bars.Security;
            var period           = Bars.Period.Value;

            var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );
            if ( aa == null )
                return;

            if ( _hews != null )
            {
                var taManager = ( PeriodXTaManager )aa.GetPeriodXTa( period );
                var waveImportance = _hews.GetAscendingWaveImportanceClone(period);

                Square_CurrentPrice_TimeElapsed( symbol, period, taManager, waveImportance );
                Square_PriorTrendTime_CurrentPriceRange( symbol, period, taManager, waveImportance );
                Square_PriorPriceRange_CurrentTrendTime( symbol, period, taManager, waveImportance );
                Square_PriorTrendEndPrice_CurrentTrendTime( symbol, period, taManager, waveImportance );
                Square_CurrentTrendPriceRange_CurrentTrendTime( symbol, period, taManager, waveImportance );
            }
        }

        private void Square_CurrentTrendPriceRange_CurrentTrendTime( Security symbol, TimeSpan period, PeriodXTaManager taManager, BTreeDictionary<long, WavePointImportance> waveImportance )
        {
            /* --------------------------------------------------------------------------------------------------------------------------
             *              
             * 1. * First we find all the big trends and do the Price and time squaring
             * 
             * --------------------------------------------------------------------------------------------------------------------------
            */
            var waves = waveImportance.Where(x =>
                                                {
                                                    if ( x.Value.WaveImportance >= GlobalConstants.DAILYIMPT )
                                                    {
                                                        if ( x.Key > _last3rdRedTime )
                                                        {
                                                            ref SBar bar = ref Bars.GetBarByTime(  x.Key );
                                                            if ( bar == SBar.EmptySBar  )
                                                                return false;

                                                            if ( ( int )bar.BarIndex < _lastMacdCrossIndex )
                                                            {
                                                                return true;
                                                            }
                                                        }
                                                    }

                                                    return false;
                                                }
                                            ).ToArray();

            var peakTroughCounts = waves.Count();

            if ( peakTroughCounts > 1 )
            {
                var oldestWave = waves[ 0 ];
                var latestWave = waves[ peakTroughCounts - 1 ];

                if ( ( oldestWave.Key == _priceRange_Time_oldestWave.Key ) && ( oldestWave.Value == _priceRange_Time_oldestWave.Value ) &&
                     ( latestWave.Key == _priceRange_Time_latestWave.Key ) && ( latestWave.Value == _priceRange_Time_latestWave.Value ) )
                {
                    return;
                }

                _priceRange_Time_oldestWave = oldestWave;
                _priceRange_Time_latestWave = latestWave;

                if ( latestWave.Value.Signal == TASignal.WAVE_PEAK )
                {
                    Square_CurrentTrendPriceRange_CurrentTrendTime_MajorUpTrend( symbol, period, taManager, oldestWave, latestWave, waves );       // Uptrend
                }
                else if ( latestWave.Value.Signal == TASignal.WAVE_TROUGH )
                {
                    Square_CurrentTrendPriceRange_CurrentTrendTime_MajorDownTrend( symbol, period, taManager, oldestWave, latestWave, waves );     // Downtrend
                }

                var redWaveCount = waves.Count();

                /* --------------------------------------------------------------------------------------------------------------------------
                 *              
                 * 2. We do price squaring for all the Red Dots
                 * 
                 * --------------------------------------------------------------------------------------------------------------------------
                */
                for ( int i = 0; i < redWaveCount; i++ )
                {
                    if ( ( i + 1 ) < redWaveCount )
                    {
                        var currentTrendEnd = waves[i + 1];
                        var currentTrendBegin = waves[i];

                        var trend = currentTrendEnd.Value.Signal == TASignal.WAVE_PEAK ? TrendDirection.Uptrend : TrendDirection.DownTrend;

                        CheckIfCurrentTrendPriceRangeSquareWithCurrentTrendTime( period, taManager, currentTrendBegin, currentTrendEnd, trend, true, TaGannPriceTimeType.CurrentTrendRange_CurrentTrendTime_RedDot );
                    }
                }

                /* --------------------------------------------------------------------------------------------------------------------------
                  *              
                  * 3. We do price squaring for all the other significant dots between Red Dots
                  * 
                  * --------------------------------------------------------------------------------------------------------------------------
              */
                for ( int i = 0; i < redWaveCount; i++ )
                {
                    if ( ( i + 1 ) < redWaveCount )
                    {
                        var latest   = waves[i + 1];
                        var previous = waves[i];

                        var lower = FinancialHelper.GetWaveImportanceDegreeLower( GlobalConstants.DAILYIMPT );

                        Square_CurrentTrendPriceRange_CurrentTrendTime_ForLowerWaves( symbol, period, taManager, lower, previous, latest, waveImportance );
                    }
                }

                /* --------------------------------------------------------------------------------------------------------------------------
                *              
                * 4. We do price squaring for the latest trend with the Last Red Dot
                * 
                * --------------------------------------------------------------------------------------------------------------------------
                */

                if ( redWaveCount > 2 )
                {
                    var endOfLatestTrend   = waves[ redWaveCount - 1 ];

                    if ( endOfLatestTrend.Value.Signal == TASignal.WAVE_PEAK )
                    {
                        // Current Trend is Downtrend
                        var lowestTrough  = GetLowestExtremumAfterX( waveImportance, endOfLatestTrend.Key, TASignal.WAVE_TROUGH );

                        if ( lowestTrough.HasValue )
                        {
                            CheckIfCurrentTrendPriceRangeSquareWithCurrentTrendTime( period, taManager, endOfLatestTrend, lowestTrough.Value, TrendDirection.DownTrend, false, TaGannPriceTimeType.CurrentTrendRange_CurrentTrendTime_Local );
                        }

                    }
                    else if ( endOfLatestTrend.Value.Signal == TASignal.WAVE_TROUGH )
                    {
                        if ( period == TimeSpan.FromMinutes( 30 ) )
                        {

                        }
                        // Current Trend is Uptrend
                        var highestPeak  = GetHighestExtremumAfterX( waveImportance, endOfLatestTrend.Key, TASignal.WAVE_PEAK );

                        if ( highestPeak.HasValue )
                        {
                            CheckIfCurrentTrendPriceRangeSquareWithCurrentTrendTime( period, taManager, endOfLatestTrend, highestPeak.Value, TrendDirection.Uptrend, false, TaGannPriceTimeType.CurrentTrendRange_CurrentTrendTime_Local );
                        }
                    }
                }
            }


        }

        private void Square_CurrentTrendPriceRange_CurrentTrendTime_ForLowerWaves( Security symbol, TimeSpan period, PeriodXTaManager taManager, int lower, KeyValuePair<long, WavePointImportance> begin, KeyValuePair<long, WavePointImportance> end, BTreeDictionary<long, WavePointImportance> waveImportance )
        {
            if ( lower < 34 )
                return;



            var waves = waveImportance.Where(x => ( x.Key > begin.Key && x.Key < end.Key && x.Value.WaveImportance == lower ) ).ToArray();

            int count = waves.Count( );

            if ( count > 0 )
            {
                ref SBar beginBar = ref
                Bars.GetBarByTime(  begin.Key );

                KeyValuePair<long, WavePointImportance> previous = begin;

                int i = 0;

                while ( i < count )
                {
                    ref SBar previousBar = ref Bars.GetBarByTime(  previous.Key );
                    var currentWave = waves[ i ];
                    ref SBar currentBar = ref Bars.GetBarByTime( currentWave.Key );
                    var trend       = previous.Value.Signal == TASignal.WAVE_PEAK ? TrendDirection.DownTrend : TrendDirection.Uptrend;

                    CheckIfPriorTrendEndPriceSquareWithCurrentTrendTime( period, taManager, previous, currentWave, trend, false, TaGannPriceTimeType.PriorTrendEndPrice_CurrentTrendTime_Local );

                    var oneDegreelower = FinancialHelper.GetWaveImportanceDegreeLower( lower );

                    Square_CurrentTrendPriceRange_CurrentTrendTime_ForLowerWaves( symbol, period, taManager, oneDegreelower, previous, currentWave, waveImportance );

                    i++;

                    previous = currentWave;
                }
            }
            else
            {
                var oneDegreelower = FinancialHelper.GetWaveImportanceDegreeLower( lower );

                Square_CurrentTrendPriceRange_CurrentTrendTime_ForLowerWaves( symbol, period, taManager, oneDegreelower, begin, end, waveImportance );
            }
        }



        private void Square_CurrentTrendPriceRange_CurrentTrendTime_MajorDownTrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> oldestWave, KeyValuePair<long, WavePointImportance> latestWave, KeyValuePair<long, WavePointImportance>[ ] selectedWaves )
        {
            var beginOfDowntrend = GetBeginningOfDowntrend( symbol, period, taManager, latestWave, selectedWaves );

            if ( beginOfDowntrend.HasValue )
            {
                CheckIfCurrentTrendPriceRangeSquareWithCurrentTrendTime( period, taManager, beginOfDowntrend.Value, latestWave, TrendDirection.DownTrend, true, TaGannPriceTimeType.CurrentTrendRange_CurrentTrendTime_Major );

                if ( beginOfDowntrend.Value.Key > oldestWave.Key )
                {
                    var waves = selectedWaves.Where( x => x.Key >= oldestWave.Key && x.Key <= beginOfDowntrend.Value.Key ).ToArray( );

                    Square_CurrentTrendPriceRange_CurrentTrendTime_MajorDownTrend( symbol, period, taManager, oldestWave, beginOfDowntrend.Value, waves );
                }
            }
        }

        private void Square_CurrentTrendPriceRange_CurrentTrendTime_MajorUpTrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> oldestWave, KeyValuePair<long, WavePointImportance> latestWave, KeyValuePair<long, WavePointImportance>[ ] selectedWaves )
        {
            var beginOfUptrend = GetBeginningOfUptrend( symbol, period, taManager, latestWave, selectedWaves );

            if ( beginOfUptrend.HasValue )
            {
                CheckIfCurrentTrendPriceRangeSquareWithCurrentTrendTime( period, taManager, beginOfUptrend.Value, latestWave, TrendDirection.Uptrend, true, TaGannPriceTimeType.CurrentTrendRange_CurrentTrendTime_Major );

                if ( beginOfUptrend.Value.Key > oldestWave.Key )
                {
                    var waves = selectedWaves.Where( x => x.Key >= oldestWave.Key && x.Key <= beginOfUptrend.Value.Key ).ToArray( );

                    Square_CurrentTrendPriceRange_CurrentTrendTime_MajorUpTrend( symbol, period, taManager, oldestWave, beginOfUptrend.Value, waves );
                }
            }
            else
            {

            }
        }



        private void Square_PriorTrendEndPrice_CurrentTrendTime( Security symbol, TimeSpan period, PeriodXTaManager taManager, BTreeDictionary<long, WavePointImportance> waveImportance )
        {
            if ( period == TimeSpan.FromMinutes( 4 ) )
            {

            }

            /* --------------------------------------------------------------------------------------------------------------------------
             *              
             * 1. * First we find all the big trends and do the Price and time squaring
             * 
             * --------------------------------------------------------------------------------------------------------------------------
            */
            var waves = waveImportance.Where(   x =>
                                                {
                                                    if ( x.Value.WaveImportance >= GlobalConstants.DAILYIMPT )
                                                    {
                                                        if ( x.Key > _last3rdRedTime )
                                                        {
                                                            ref SBar bar = ref Bars.GetBarByTime(  x.Key );
                                                            if ( bar == SBar.EmptySBar  )
                                                                return false;

                                                            if ( ( int )bar.BarIndex < _lastMacdCrossIndex )
                                                            {
                                                                return true;
                                                            }
                                                        }

                                                    }

                                                    return false;
                                                }
                                             ).ToArray();
            var peakTroughCounts = waves.Count();

            if ( peakTroughCounts > 1 )
            {
                var oldestWave = waves[ 0 ];
                var latestWave = waves[ peakTroughCounts - 1 ];

                if ( latestWave.Value.Signal == TASignal.WAVE_PEAK )
                {
                    Square_PriorTrendEndPrice_CurrentTrendTime_MajorUpTrend( symbol, period, taManager, oldestWave, latestWave, waves );       // Uptrend
                }
                else if ( latestWave.Value.Signal == TASignal.WAVE_TROUGH )
                {
                    Square_PriorTrendEndPrice_CurrentTrendTime_MajorDownTrend( symbol, period, taManager, oldestWave, latestWave, waves );     // Downtrend
                }
            }

            var redWaveCount = waves.Count();

            /* --------------------------------------------------------------------------------------------------------------------------
             *              
             * 2. We do price squaring for all the Red Dots
             * 
             * --------------------------------------------------------------------------------------------------------------------------
            */
            for ( int i = 0; i < redWaveCount; i++ )
            {
                if ( ( i + 1 ) < redWaveCount )
                {
                    var latestWave       = waves[i + 1];
                    var endOfLastTrend   = waves[i];

                    var trend = latestWave.Value.Signal == TASignal.WAVE_PEAK ? TrendDirection.Uptrend : TrendDirection.DownTrend;

                    CheckIfPriorTrendEndPriceSquareWithCurrentTrendTime( period, taManager, endOfLastTrend, latestWave, trend, true, TaGannPriceTimeType.PriorEndingPrice_CurrentTrendTime_RedDot );
                }
            }

            /* --------------------------------------------------------------------------------------------------------------------------
               *              
               * 3. We do price squaring for all the other significant dots between Red Dots
               * 
               * --------------------------------------------------------------------------------------------------------------------------
           */
            for ( int i = 0; i < redWaveCount; i++ )
            {
                if ( ( i + 1 ) < redWaveCount )
                {
                    var latest   = waves[i + 1];
                    var previous = waves[i];

                    var lower = FinancialHelper.GetWaveImportanceDegreeLower( GlobalConstants.DAILYIMPT );

                    if ( period == TimeSpan.FromMinutes( 5 ) )
                    {

                    }

                    // This is a local uptrend
                    if ( latest.Value.Signal == TASignal.WAVE_PEAK && previous.Value.Signal == TASignal.WAVE_TROUGH )
                    {
                        Square_PriorTrendEndPrice_CurrentTrendTime_ForLowerWaves_UpTrend( symbol, period, taManager, lower, previous, latest, waveImportance );
                    }
                    // This is a local downtrend
                    else if ( latest.Value.Signal == TASignal.WAVE_TROUGH && previous.Value.Signal == TASignal.WAVE_PEAK )
                    {
                        Square_PriorTrendEndPrice_CurrentTrendTime_ForLowerWaves_Downtrend( symbol, period, taManager, lower, previous, latest, waveImportance );
                    }
                }
            }

            /* --------------------------------------------------------------------------------------------------------------------------
            *              
            * 4. We do price squaring for the latest trend with the Last Red Dot
            * 
            * --------------------------------------------------------------------------------------------------------------------------
            */

            if ( redWaveCount > 2 )
            {
                var endOfLatestTrend   = waves[ redWaveCount - 1 ];
                var beginOfLatestTrend = waves[ redWaveCount - 2 ];

                if ( endOfLatestTrend.Value.Signal == TASignal.WAVE_PEAK )
                {
                    // Current Trend is Downtrend
                    var lowestTrough  = GetLowestExtremumAfterX( waveImportance, endOfLatestTrend.Key, TASignal.WAVE_TROUGH );

                    if ( lowestTrough.HasValue )
                    {
                        CheckIfPriorTrendEndPriceSquareWithCurrentTrendTime( period, taManager, endOfLatestTrend, lowestTrough.Value, TrendDirection.DownTrend, false, TaGannPriceTimeType.PriorTrendEndPrice_CurrentTrendTime_Local );
                    }

                }
                else if ( endOfLatestTrend.Value.Signal == TASignal.WAVE_TROUGH )
                {
                    if ( period == TimeSpan.FromMinutes( 30 ) )
                    {

                    }
                    // Current Trend is Uptrend
                    var highestPeak  = GetHighestExtremumAfterX( waveImportance, endOfLatestTrend.Key, TASignal.WAVE_PEAK );

                    if ( highestPeak.HasValue )
                    {
                        CheckIfPriorTrendEndPriceSquareWithCurrentTrendTime( period, taManager, endOfLatestTrend, highestPeak.Value, TrendDirection.Uptrend, false, TaGannPriceTimeType.PriorTrendEndPrice_CurrentTrendTime_Local );
                    }
                }
            }
        }

        private void Square_PriorTrendEndPrice_CurrentTrendTime_LocalDownTrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> previous, KeyValuePair<long, WavePointImportance> latest, BTreeDictionary<long, WavePointImportance> selectedWaves )
        {
            var largest = GetLargestWaveImportance( previous, latest, selectedWaves );

            if ( largest.HasValue )
            {
                KeyValuePair< long, WavePointImportance >? counter = null;

                if ( largest.Value.Value.Signal == TASignal.WAVE_PEAK )
                {
                    counter = GetTroughOfHighestWaveImportance( previous, latest, selectedWaves );
                }
                else if ( largest.Value.Value.Signal == TASignal.WAVE_TROUGH )
                {
                    counter = GetPeakOfHighestWaveImportance( previous, latest, selectedWaves );
                }

                if ( counter.HasValue )
                {
                    var endOfPriodTrend   = counter.Value.Key > largest.Value.Key ? counter : largest;
                    var beginOfPriodTrend = counter.Value.Key > largest.Value.Key ? largest : counter;

                    CheckIfPriorTrendEndPriceSquareWithCurrentTrendTime( period, taManager, endOfPriodTrend.Value, latest, TrendDirection.DownTrend, false, TaGannPriceTimeType.PriorTrendEndPrice_CurrentTrendTime_Local );
                }
            }
        }

        private void Square_PriorTrendEndPrice_CurrentTrendTime_ForLowerWaves_Downtrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, int lower, KeyValuePair<long, WavePointImportance> begin, KeyValuePair<long, WavePointImportance> end, BTreeDictionary<long, WavePointImportance> waveImportance )
        {
            if ( lower < 34 )
                return;

            if ( lower == 34 )
            {
                ref SBar beginBar = ref Bars.GetBarByTime(  begin.Key );
                ref SBar endBar = ref Bars.GetBarByTime(  end.Key );

                if ( beginBar == SBar.EmptySBar ||
                     endBar == SBar.EmptySBar )
                    return;

                if ( endBar.BarIndex - beginBar.BarIndex < 55 )
                {
                    return;
                }

            }

            var waves = waveImportance.Where(x => ( x.Key > begin.Key && x.Key < end.Key && x.Value.WaveImportance == lower ) ).ToArray();

            int count = waves.Count( );

            if ( count >= 2 )
            {
                ref SBar beginBar = ref Bars.GetBarByTime(  begin.Key );

                KeyValuePair<long, WavePointImportance> previous = begin;

                int i = 0;

                while ( ( i + 1 ) < count )
                {
                    ref SBar previousBar = ref Bars.GetBarByTime(  previous.Key );
                    var firstWave   = waves[ i ];
                    var secondWave  = waves[ i + 1 ];

                    ref SBar firstBar = ref Bars.GetBarByTime( firstWave.Key );
                    ref SBar secondBar = ref Bars.GetBarByTime( secondWave.Key );

                    CheckIfPriorTrendEndPriceSquareWithCurrentTrendTime( period, taManager, firstWave, secondWave, TrendDirection.DownTrend, false, TaGannPriceTimeType.PriorTrendEndPrice_CurrentTrendTime_Local );

                    var oneDegreelower = FinancialHelper.GetWaveImportanceDegreeLower( lower );

                    Square_PriorTrendEndPrice_CurrentTrendTime_ForLowerWaves_Downtrend( symbol, period, taManager, oneDegreelower, previous, firstWave, waveImportance );

                    previous = firstWave;

                    i++;
                }
            }
            else
            {
                var oneDegreelower = FinancialHelper.GetWaveImportanceDegreeLower( lower );

                Square_PriorTrendEndPrice_CurrentTrendTime_ForLowerWaves_Downtrend( symbol, period, taManager, oneDegreelower, begin, end, waveImportance );
            }
        }

        private void Square_PriorTrendEndPrice_CurrentTrendTime_LocalUpTrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> previous, KeyValuePair<long, WavePointImportance> latest, BTreeDictionary<long, WavePointImportance> selectedWaves )
        {
            var largest = GetLargestWaveImportance( previous, latest, selectedWaves );

            if ( largest.HasValue )
            {
                KeyValuePair< long, WavePointImportance >? counter = null;

                if ( largest.Value.Value.Signal == TASignal.WAVE_PEAK )
                {
                    counter = GetTroughOfHighestWaveImportance( previous, latest, selectedWaves );
                }
                else if ( largest.Value.Value.Signal == TASignal.WAVE_TROUGH )
                {
                    counter = GetPeakOfHighestWaveImportance( previous, latest, selectedWaves );
                }

                if ( counter.HasValue )
                {
                    var endOfPriodTrend   = counter.Value.Key > largest.Value.Key ? counter : largest;
                    var beginOfPriodTrend = counter.Value.Key > largest.Value.Key ? largest : counter;

                    CheckIfPriorTrendEndPriceSquareWithCurrentTrendTime( period, taManager, endOfPriodTrend.Value, latest, TrendDirection.Uptrend, false, TaGannPriceTimeType.PriorTrendEndPrice_CurrentTrendTime_Local );
                }

            }
        }

        private void Square_PriorTrendEndPrice_CurrentTrendTime_ForLowerWaves_UpTrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, int lower, KeyValuePair<long, WavePointImportance> begin, KeyValuePair<long, WavePointImportance> end, BTreeDictionary<long, WavePointImportance> waveImportance )
        {
            if ( lower < 34 )
                return;

            if ( lower == 34 )
            {
                ref SBar beginBar = ref Bars.GetBarByTime(  begin.Key );
                ref SBar endBar = ref Bars.GetBarByTime(  end.Key );

                if ( endBar.BarIndex - beginBar.BarIndex < 55 )
                {
                    return;
                }

            }

            var waves = waveImportance.Where(x => ( x.Key > begin.Key && x.Key < end.Key && x.Value.WaveImportance == lower ) ).ToArray();

            int count = waves.Count( );

            if ( count >= 2 )
            {
                ref SBar beginBar = ref Bars.GetBarByTime(  begin.Key );

                KeyValuePair<long, WavePointImportance> previous = begin;

                int i = 0;

                while ( ( i + 1 ) < count )
                {
                    ref SBar previousBar = ref Bars.GetBarByTime(  previous.Key );
                    var firstWave   = waves[ i ];
                    var secondWave  = waves[ i + 1 ];

                    ref SBar firstBar = ref Bars.GetBarByTime( firstWave.Key );
                    ref SBar secondBar = ref Bars.GetBarByTime( secondWave.Key );

                    CheckIfPriorTrendEndPriceSquareWithCurrentTrendTime( period, taManager, firstWave, secondWave, TrendDirection.Uptrend, false, TaGannPriceTimeType.PriorTrendEndPrice_CurrentTrendTime_Local );

                    var oneDegreelower = FinancialHelper.GetWaveImportanceDegreeLower( lower );

                    Square_PriorTrendEndPrice_CurrentTrendTime_ForLowerWaves_UpTrend( symbol, period, taManager, oneDegreelower, previous, firstWave, waveImportance );

                    previous = firstWave;

                    i++;
                }
            }
            else
            {
                var oneDegreelower = FinancialHelper.GetWaveImportanceDegreeLower( lower );

                Square_PriorPriceRange_CurrentTrendTime_ForLowerWaves_UpTrend( symbol, period, taManager, oneDegreelower, begin, end, waveImportance );
            }
        }

        private void Square_PriorTrendEndPrice_CurrentTrendTime_MajorDownTrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> oldestWave, KeyValuePair<long, WavePointImportance> latestWave, KeyValuePair<long, WavePointImportance>[ ] selectedWaves )
        {
            var beginOfDowntrend = GetBeginningOfDowntrend( symbol, period, taManager, latestWave, selectedWaves );

            if ( beginOfDowntrend.HasValue )
            {
                CheckIfPriorTrendEndPriceSquareWithCurrentTrendTime( period, taManager, beginOfDowntrend.Value, latestWave, TrendDirection.DownTrend, true, TaGannPriceTimeType.PriorEndingPrice_CurrentTrendTime_Major );

                if ( beginOfDowntrend.Value.Key > oldestWave.Key )
                {
                    var waves = selectedWaves.Where( x => x.Key >= oldestWave.Key && x.Key <= beginOfDowntrend.Value.Key ).ToArray( );

                    Square_PriorTrendEndPrice_CurrentTrendTime_MajorDownTrend( symbol, period, taManager, oldestWave, beginOfDowntrend.Value, waves );
                }
            }
        }

        private void Square_PriorTrendEndPrice_CurrentTrendTime_MajorUpTrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> oldestWave, KeyValuePair<long, WavePointImportance> latestWave, KeyValuePair<long, WavePointImportance>[ ] selectedWaves )
        {
            var beginOfUptrend = GetBeginningOfUptrend( symbol, period, taManager, latestWave, selectedWaves );

            if ( period == TimeSpan.FromMinutes( 5 ) )
            {

            }

            if ( beginOfUptrend.HasValue )
            {
                CheckIfPriorTrendEndPriceSquareWithCurrentTrendTime( period, taManager, beginOfUptrend.Value, latestWave, TrendDirection.Uptrend, true, TaGannPriceTimeType.PriorEndingPrice_CurrentTrendTime_Major );


                if ( beginOfUptrend.Value.Key > oldestWave.Key )
                {
                    var waves = selectedWaves.Where( x => x.Key >= oldestWave.Key && x.Key <= beginOfUptrend.Value.Key ).ToArray( );

                    Square_PriorTrendEndPrice_CurrentTrendTime_MajorUpTrend( symbol, period, taManager, oldestWave, beginOfUptrend.Value, waves );
                }
            }
            else
            {

            }
        }



        private void Square_PriorPriceRange_CurrentTrendTime( Security symbol, TimeSpan period, PeriodXTaManager taManager, BTreeDictionary<long, WavePointImportance> waveImportance )
        {
            /* --------------------------------------------------------------------------------------------------------------------------
             *              
             * 1. * First we find all the big trends and do the Price and time squaring
             * 
             * --------------------------------------------------------------------------------------------------------------------------
            */

            if ( period == TimeSpan.FromMinutes( 5 ) )
            {

            }

            var waves = waveImportance.Where(x =>
            {
                if ( x.Value.WaveImportance >= GlobalConstants.DAILYIMPT )
                {
                    if ( x.Key > _last3rdRedTime )
                    {
                        ref SBar bar = ref Bars.GetBarByTime(  x.Key );
                        if ( bar == SBar.EmptySBar )
                            return false;

                        if ( ( int )bar.BarIndex < _lastMacdCrossIndex )
                        {
                            return true;
                        }
                    }

                }

                return false;
            }
                                           ).ToArray();

            var peakTroughCounts = waves.Count();

            if ( peakTroughCounts > 1 )
            {
                var oldestWave = waves[ 0 ];
                var latestWave = waves[ peakTroughCounts - 1 ];

                if ( ( oldestWave.Key == _priorPrice_TrendTime_oldestWave.Key ) && ( oldestWave.Value == _priorPrice_TrendTime_oldestWave.Value ) &&
                     ( latestWave.Key == _priorPrice_TrendTime_latestWave.Key ) && ( latestWave.Value == _priorPrice_TrendTime_latestWave.Value ) )
                {
                    return;
                }

                _priorPrice_TrendTime_oldestWave = oldestWave;
                _priorPrice_TrendTime_latestWave = latestWave;

                if ( latestWave.Value.Signal == TASignal.WAVE_PEAK )
                {
                    Square_PriorPriceRange_CurrentTrendTime_MajorUpTrend( symbol, period, taManager, oldestWave, latestWave, waves );       // Uptrend
                }
                else if ( latestWave.Value.Signal == TASignal.WAVE_TROUGH )
                {
                    Square_PriorPriceRange_CurrentTrendTime_MajorDownTrend( symbol, period, taManager, oldestWave, latestWave, waves );     // Downtrend
                }
            }

            var redWaveCount = waves.Count();

            /* --------------------------------------------------------------------------------------------------------------------------
             *              
             * 2. We do price squaring for all the Red Dots
             * 
             * --------------------------------------------------------------------------------------------------------------------------
            */
            for ( int i = 0; i < redWaveCount; i++ )
            {
                if ( ( i + 2 ) < redWaveCount )
                {
                    var latestWave       = waves[i + 2];
                    var endOfLastTrend   = waves[i + 1];
                    var beginOfLastTrend = waves[i];

                    var priorTrend = endOfLastTrend.Value.Signal == TASignal.WAVE_PEAK ? TrendDirection.Uptrend : TrendDirection.DownTrend;

                    CheckIfPriorPriceRangeSquareWithCurrentTrendTime( period, taManager, beginOfLastTrend, endOfLastTrend, latestWave, priorTrend, true, TaGannPriceTimeType.PriorPriceRange_CurrentTrendTime_RedDot );
                }
            }

            /* --------------------------------------------------------------------------------------------------------------------------
               *              
               * 3. We do price squaring for all the other significant dots between Red Dots
               * 
               * --------------------------------------------------------------------------------------------------------------------------
           */
            for ( int i = 0; i < redWaveCount; i++ )
            {
                if ( period == TimeSpan.FromMinutes( 5 ) )
                {

                }

                if ( ( i + 1 ) < redWaveCount )
                {
                    var latest   = waves[i + 1];
                    var previous = waves[i];
                    var lower = FinancialHelper.GetWaveImportanceDegreeLower( GlobalConstants.DAILYIMPT );

                    // This is a local uptrend
                    if ( latest.Value.Signal == TASignal.WAVE_PEAK && previous.Value.Signal == TASignal.WAVE_TROUGH )
                    {
                        Square_PriorPriceRange_CurrentTrendTime_ForLowerWaves_UpTrend( symbol, period, taManager, lower, previous, latest, waveImportance );
                    }
                    // This is a local downtrend
                    else if ( latest.Value.Signal == TASignal.WAVE_TROUGH && previous.Value.Signal == TASignal.WAVE_PEAK )
                    {
                        Square_PriorPriceRange_CurrentTrendTime_ForLowerWaves_DownTrend( symbol, period, taManager, lower, previous, latest, waveImportance );
                    }
                }
            }

            /* --------------------------------------------------------------------------------------------------------------------------
            *              
            * 4. We do price squaring for the latest trend with the Last Red Dot
            * 
            * --------------------------------------------------------------------------------------------------------------------------
            */

            if ( redWaveCount > 2 )
            {
                var endOfLatestTrend   = waves[ redWaveCount - 1 ];
                var beginOfLatestTrend = waves[ redWaveCount - 2 ];
                var priorTrend = endOfLatestTrend.Value.Signal == TASignal.WAVE_PEAK ? TrendDirection.Uptrend : TrendDirection.DownTrend;

                if ( endOfLatestTrend.Value.Signal == TASignal.WAVE_PEAK )
                {
                    // Current Trend is Downtrend
                    var lowestTrough  = GetLowestExtremumAfterX( waveImportance, endOfLatestTrend.Key, TASignal.WAVE_TROUGH );

                    if ( lowestTrough.HasValue )
                    {
                        CheckIfPriorPriceRangeSquareWithCurrentTrendTime( period, taManager, beginOfLatestTrend, endOfLatestTrend, lowestTrough.Value, priorTrend, true, TaGannPriceTimeType.PriorPriceRange_CurrentTrendTime_Local );
                    }

                }
                else if ( endOfLatestTrend.Value.Signal == TASignal.WAVE_TROUGH )
                {
                    if ( period == TimeSpan.FromMinutes( 30 ) )
                    {

                    }
                    // Current Trend is Uptrend
                    var highestPeak  = GetHighestExtremumAfterX( waveImportance, endOfLatestTrend.Key, TASignal.WAVE_PEAK );

                    if ( highestPeak.HasValue )
                    {
                        CheckIfPriorPriceRangeSquareWithCurrentTrendTime( period, taManager, beginOfLatestTrend, endOfLatestTrend, highestPeak.Value, priorTrend, true, TaGannPriceTimeType.PriorPriceRange_CurrentTrendTime_Local );
                    }
                }
            }
        }



        private void Square_PriorPriceRange_CurrentTrendTime_ForLowerWaves_DownTrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, int lower, KeyValuePair<long, WavePointImportance> begin, KeyValuePair<long, WavePointImportance> end, BTreeDictionary<long, WavePointImportance> waveImportance )
        {
            if ( lower < 34 )
                return;

            if ( lower == 34 )
            {
                ref SBar beginBar = ref Bars.GetBarByTime(  begin.Key );
                ref SBar endBar = ref Bars.GetBarByTime(  end.Key );

                if ( endBar.BarIndex - beginBar.BarIndex < 55 )
                {
                    return;
                }

            }

            var waves = waveImportance.Where(x => ( x.Key > begin.Key && x.Key < end.Key && x.Value.WaveImportance == lower ) ).ToArray();

            int count = waves.Count( );

            if ( count >= 2 )
            {
                ref SBar beginBar = ref Bars.GetBarByTime(  begin.Key );

                KeyValuePair<long, WavePointImportance> previous = begin;

                int i = 0;

                while ( ( i + 1 ) < count )
                {
                    ref SBar previousBar = ref Bars.GetBarByTime(  previous.Key );
                    var firstWave   = waves[ i ];
                    var secondWave  = waves[ i + 1 ];

                    ref SBar firstBar = ref Bars.GetBarByTime( firstWave.Key );
                    ref SBar secondBar = ref Bars.GetBarByTime( secondWave.Key );

                    if ( period == TimeSpan.FromMinutes( 5 ) )
                    {

                    }

                    var priorTrend = firstWave.Value.Signal == TASignal.WAVE_PEAK ? TrendDirection.Uptrend : TrendDirection.DownTrend;

                    CheckIfPriorPriceRangeSquareWithCurrentTrendTime( period, taManager, previous, firstWave, secondWave, priorTrend, false, TaGannPriceTimeType.PriorPriceRange_CurrentTrendTime_Local );

                    var oneDegreelower = FinancialHelper.GetWaveImportanceDegreeLower( lower );

                    Square_PriorPriceRange_CurrentTrendTime_ForLowerWaves_DownTrend( symbol, period, taManager, oneDegreelower, previous, firstWave, waveImportance );

                    previous = secondWave;

                    i = i + 2;
                }

            }
            else
            {
                var oneDegreelower = FinancialHelper.GetWaveImportanceDegreeLower( lower );

                Square_PriorPriceRange_CurrentTrendTime_ForLowerWaves_DownTrend( symbol, period, taManager, oneDegreelower, begin, end, waveImportance );
            }
        }

        private void Square_PriorPriceRange_CurrentTrendTime_ForLowerWaves_UpTrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, int lower, KeyValuePair<long, WavePointImportance> begin, KeyValuePair<long, WavePointImportance> end, BTreeDictionary<long, WavePointImportance> waveImportance )
        {
            if ( lower < 34 )
                return;

            if ( lower == 34 )
            {
                ref SBar beginBar = ref Bars.GetBarByTime(  begin.Key );
                ref SBar endBar = ref Bars.GetBarByTime(  end.Key );

                if ( beginBar == SBar.EmptySBar || endBar == SBar.EmptySBar )
                    return;

                if ( endBar.BarIndex - beginBar.BarIndex < 55 )
                {
                    return;
                }

            }
            var waves = waveImportance.Where(x => ( x.Key > begin.Key && x.Key < end.Key && x.Value.WaveImportance == lower ) ).ToArray();

            int count = waves.Count( );

            if ( count >= 2 )
            {
                ref SBar beginBar = ref Bars.GetBarByTime(  begin.Key );

                KeyValuePair<long, WavePointImportance> previous = begin;

                int i = 0;

                while ( ( i + 1 ) < count )
                {
                    ref SBar previousBar = ref Bars.GetBarByTime(  previous.Key );
                    var firstWave   = waves[ i ];
                    var secondWave  = waves[ i + 1 ];

                    ref SBar firstBar = ref Bars.GetBarByTime( firstWave.Key );
                    ref SBar secondBar = ref Bars.GetBarByTime( secondWave.Key );

                    var priorTrend  = firstWave.Value.Signal == TASignal.WAVE_PEAK ? TrendDirection.Uptrend : TrendDirection.DownTrend;

                    CheckIfPriorPriceRangeSquareWithCurrentTrendTime( period, taManager, previous, firstWave, secondWave, priorTrend, false, TaGannPriceTimeType.PriorPriceRange_CurrentTrendTime_Local );

                    var oneDegreelower = FinancialHelper.GetWaveImportanceDegreeLower( lower );

                    Square_PriorPriceRange_CurrentTrendTime_ForLowerWaves_UpTrend( symbol, period, taManager, oneDegreelower, previous, firstWave, waveImportance );

                    previous = secondWave;

                    i = i + 2;
                }

            }
            else
            {
                var oneDegreelower = FinancialHelper.GetWaveImportanceDegreeLower( lower );

                Square_PriorPriceRange_CurrentTrendTime_ForLowerWaves_UpTrend( symbol, period, taManager, oneDegreelower, begin, end, waveImportance );
            }
        }

        private void Square_PriorPriceRange_CurrentTrendTime_MajorDownTrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> oldestWave, KeyValuePair<long, WavePointImportance> latestWave, KeyValuePair<long, WavePointImportance>[ ] selectedWaves )
        {
            var beginOfDowntrend = GetBeginningOfDowntrend( symbol, period, taManager, latestWave, selectedWaves );

            if ( beginOfDowntrend.HasValue )
            {
                if ( beginOfDowntrend.Value.Key > oldestWave.Key )
                {
                    var waves = selectedWaves.Where(x => x.Key >= oldestWave.Key && x.Key <= beginOfDowntrend.Value.Key  ).ToArray();

                    var beginOfUptrend = GetBeginningOfUptrend( symbol, period, taManager, beginOfDowntrend.Value, waves );

                    if ( beginOfUptrend.HasValue )
                    {
                        var priorTrend = beginOfDowntrend.Value.Value.Signal == TASignal.WAVE_PEAK ? TrendDirection.Uptrend : TrendDirection.DownTrend;

                        CheckIfPriorPriceRangeSquareWithCurrentTrendTime( period, taManager, beginOfUptrend.Value, beginOfDowntrend.Value, latestWave, priorTrend, true, TaGannPriceTimeType.PriorPriceRange_CurrentTrendTime_Major );

                        if ( beginOfUptrend.Value.Key > oldestWave.Key )
                        {
                            waves = selectedWaves.Where( x => x.Key >= oldestWave.Key && x.Key <= beginOfUptrend.Value.Key ).ToArray( );

                            Square_PriorPriceRange_CurrentTrendTime_MajorDownTrend( symbol, period, taManager, oldestWave, beginOfUptrend.Value, waves );
                        }
                    }
                }
            }
        }


        private void Square_PriorPriceRange_CurrentTrendTime_MajorUpTrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> oldestWave, KeyValuePair<long, WavePointImportance> latestWave, KeyValuePair<long, WavePointImportance>[ ] selectedWaves )
        {
            var beginOfUptrend = GetBeginningOfUptrend( symbol, period, taManager, latestWave, selectedWaves );

            if ( beginOfUptrend.HasValue )
            {
                if ( beginOfUptrend.Value.Key > oldestWave.Key )
                {
                    var waves = selectedWaves.Where(x => x.Key >= oldestWave.Key && x.Key <= beginOfUptrend.Value.Key  ).ToArray();

                    var beginOfDowntrend = GetBeginningOfDowntrend( symbol, period, taManager, beginOfUptrend.Value, waves );

                    if ( beginOfDowntrend.HasValue )
                    {
                        var priorTrend = beginOfUptrend.Value.Value.Signal == TASignal.WAVE_PEAK ? TrendDirection.Uptrend : TrendDirection.DownTrend;

                        CheckIfPriorPriceRangeSquareWithCurrentTrendTime( period, taManager, beginOfDowntrend.Value, beginOfUptrend.Value, latestWave, priorTrend, true, TaGannPriceTimeType.PriorPriceRange_CurrentTrendTime_Major );

                        if ( beginOfDowntrend.Value.Key > oldestWave.Key )
                        {
                            waves = selectedWaves.Where( x => x.Key >= oldestWave.Key && x.Key <= beginOfDowntrend.Value.Key ).ToArray( );

                            Square_PriorPriceRange_CurrentTrendTime_MajorUpTrend( symbol, period, taManager, oldestWave, beginOfDowntrend.Value, waves );
                        }
                    }
                }
            }
            else
            {

            }
        }

        private void Square_PriorTrendTime_CurrentPriceRange( Security symbol, TimeSpan period, PeriodXTaManager taManager, BTreeDictionary<long, WavePointImportance> waveImportance )
        {
            /* --------------------------------------------------------------------------------------------------------------------------
             *              
             * 1. * First we find all the big trends and do the Price and time squaring
             * 
             * --------------------------------------------------------------------------------------------------------------------------
            */

            var waves            = waveImportance.Where(x =>
            {
                if ( x.Value.WaveImportance >= GlobalConstants.DAILYIMPT )
                {
                    if ( x.Key > _last3rdRedTime )
                    {
                        ref SBar bar = ref Bars.GetBarByTime(  x.Key );
                        if ( bar == SBar.EmptySBar  )
                            return false;

                        if ( ( int )bar.BarIndex < _lastMacdCrossIndex )
                        {
                            return true;
                        }
                    }

                }

                return false;
            }
                                                        ).ToArray();
            var peakTroughCounts = waves.Count();

            if ( peakTroughCounts > 1 )
            {
                var oldestWave = waves[ 0 ];
                var latestWave = waves[ peakTroughCounts - 1 ];

                if ( ( oldestWave.Key == _priorTime_CurrentPrice_oldestWave.Key ) && ( oldestWave.Value == _priorTime_CurrentPrice_oldestWave.Value ) &&
                     ( latestWave.Key == _priorTime_CurrentPrice_latestWave.Key ) && ( latestWave.Value == _priorTime_CurrentPrice_latestWave.Value ) )
                {
                    return;
                }

                _priorTime_CurrentPrice_oldestWave = oldestWave;
                _priorTime_CurrentPrice_latestWave = latestWave;

                if ( latestWave.Value.Signal == TASignal.WAVE_PEAK )
                {
                    Square_PriorTrendTime_CurrentPriceRange_MajorUpTrend( symbol, period, taManager, oldestWave, latestWave, waves );       // Uptrend
                }
                else if ( latestWave.Value.Signal == TASignal.WAVE_TROUGH )
                {
                    Square_PriorTrendTime_CurrentPriceRange_MajorDownTrend( symbol, period, taManager, oldestWave, latestWave, waves );     // Downtrend
                }

                var redWaveCount = waves.Count();

                /* --------------------------------------------------------------------------------------------------------------------------
                 *              
                 * 2. We do price squaring for all the Red Dots
                 * 
                 * --------------------------------------------------------------------------------------------------------------------------
                */
                for ( int i = 0; i < redWaveCount; i++ )
                {
                    if ( ( i + 2 ) < redWaveCount )
                    {
                        var latestWave2       = waves[i + 2];
                        var endOfLastTrend   = waves[i + 1];
                        var beginOfLastTrend = waves[i];

                        var trend = latestWave2.Value.Signal == TASignal.WAVE_PEAK ? TrendDirection.Uptrend : TrendDirection.DownTrend;

                        CheckIfPriorTrendTimeSquareWithCurrentPriceRange( period, taManager, beginOfLastTrend, endOfLastTrend, latestWave2, trend, true, TaGannPriceTimeType.PriorTrendTime_CurrentTrendRange_RedDot );
                    }
                }

                /* --------------------------------------------------------------------------------------------------------------------------
                   *              
                   * 3. We do price squaring for all the other significant dots between Red Dots
                   * 
                   * --------------------------------------------------------------------------------------------------------------------------
               */
                for ( int i = 0; i < redWaveCount; i++ )
                {
                    if ( period == TimeSpan.FromMinutes( 5 ) )
                    {

                    }

                    if ( ( i + 1 ) < redWaveCount )
                    {
                        var latest   = waves[i + 1];
                        var previous = waves[i];

                        var lower = FinancialHelper.GetWaveImportanceDegreeLower( GlobalConstants.DAILYIMPT );

                        // This is a local uptrend
                        if ( latest.Value.Signal == TASignal.WAVE_PEAK && previous.Value.Signal == TASignal.WAVE_TROUGH )
                        {
                            Square_PriorTrendTime_CurrentPriceRange_ForLowerWaves_UpTrend( symbol, period, taManager, lower, previous, latest, waveImportance );
                        }
                        // This is a local downtrend
                        else if ( latest.Value.Signal == TASignal.WAVE_TROUGH && previous.Value.Signal == TASignal.WAVE_PEAK )
                        {
                            Square_PriorTrendTime_CurrentPriceRange_ForLowerWaves_DownTrend( symbol, period, taManager, lower, previous, latest, waveImportance );
                        }
                    }
                }

                /* --------------------------------------------------------------------------------------------------------------------------
                *              
                * 4. We do price squaring for the latest trend with the Last Red Dot
                * 
                * --------------------------------------------------------------------------------------------------------------------------
                */



                if ( redWaveCount > 2 )
                {
                    var endOfLatestTrend = waves[ redWaveCount - 1 ];
                    var beginOfLatestTrend = waves[ redWaveCount - 2 ];

                    if ( endOfLatestTrend.Value.Signal == TASignal.WAVE_PEAK )
                    {
                        // Current Trend is Downtrend
                        var lowestTrough  = GetLowestExtremumAfterX( waveImportance, endOfLatestTrend.Key, TASignal.WAVE_TROUGH );

                        if ( lowestTrough.HasValue )
                        {
                            CheckIfPriorTrendTimeSquareWithCurrentPriceRange( period, taManager, beginOfLatestTrend, endOfLatestTrend, lowestTrough.Value, TrendDirection.DownTrend, true, TaGannPriceTimeType.PriorTrendTime_CurrentTrendRange_Local );
                        }

                    }
                    else if ( endOfLatestTrend.Value.Signal == TASignal.WAVE_TROUGH )
                    {
                        if ( period == TimeSpan.FromMinutes( 30 ) )
                        {

                        }
                        // Current Trend is Uptrend
                        var highestPeak  = GetHighestExtremumAfterX( waveImportance, endOfLatestTrend.Key, TASignal.WAVE_PEAK );

                        if ( highestPeak.HasValue )
                        {
                            CheckIfPriorTrendTimeSquareWithCurrentPriceRange( period, taManager, beginOfLatestTrend, endOfLatestTrend, highestPeak.Value, TrendDirection.Uptrend, true, TaGannPriceTimeType.PriorTrendTime_CurrentTrendRange_Local );
                        }
                    }
                }
            }
        }

        private void Square_PriorTrendTime_CurrentPriceRange_ForLowerWaves_DownTrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, int lower, KeyValuePair<long, WavePointImportance> begin, KeyValuePair<long, WavePointImportance> end, BTreeDictionary<long, WavePointImportance> waveImportance )
        {
            if ( lower < 34 )
                return;

            if ( lower == 34 )
            {
                ref SBar beginBar = ref Bars.GetBarByTime(  begin.Key );
                ref SBar endBar = ref Bars.GetBarByTime(  end.Key );

                if ( endBar.BarIndex - beginBar.BarIndex < 55 )
                {
                    return;
                }

            }

            var waves = waveImportance.Where(x => ( x.Key > begin.Key && x.Key < end.Key && x.Value.WaveImportance == lower ) ).ToArray();

            int count = waves.Count( );

            if ( count >= 2 )
            {
                ref SBar beginBar = ref Bars.GetBarByTime(  begin.Key );

                KeyValuePair<long, WavePointImportance> previous = begin;

                int i = 0;

                while ( ( i + 1 ) < count )
                {
                    ref SBar previousBar = ref Bars.GetBarByTime(  previous.Key );
                    var firstWave   = waves[ i ];
                    var secondWave  = waves[ i + 1 ];

                    ref SBar firstBar = ref Bars.GetBarByTime( firstWave.Key );
                    ref SBar secondBar = ref Bars.GetBarByTime( secondWave.Key );

                    if ( period == TimeSpan.FromMinutes( 5 ) )
                    {

                    }

                    CheckIfPriorTrendTimeSquareWithCurrentPriceRange( period, taManager, previous, firstWave, secondWave, TrendDirection.DownTrend, false, TaGannPriceTimeType.PriorTrendTime_CurrentTrendRange_Local );

                    var oneDegreelower = FinancialHelper.GetWaveImportanceDegreeLower( lower );

                    Square_PriorTrendTime_CurrentPriceRange_ForLowerWaves_DownTrend( symbol, period, taManager, oneDegreelower, previous, firstWave, waveImportance );

                    previous = secondWave;

                    i = i + 2;
                }

            }
            else
            {
                var oneDegreelower = FinancialHelper.GetWaveImportanceDegreeLower( lower );

                Square_PriorTrendTime_CurrentPriceRange_ForLowerWaves_DownTrend( symbol, period, taManager, oneDegreelower, begin, end, waveImportance );
            }
        }

        private void Square_PriorTrendTime_CurrentPriceRange_ForLowerWaves_UpTrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, int lower, KeyValuePair<long, WavePointImportance> begin, KeyValuePair<long, WavePointImportance> end, BTreeDictionary<long, WavePointImportance> waveImportance )
        {
            if ( lower < 34 )
                return;

            if ( lower == 34 )
            {
                ref SBar beginBar = ref Bars.GetBarByTime(  begin.Key );
                ref SBar endBar = ref Bars.GetBarByTime(  end.Key );

                if ( endBar.BarIndex - beginBar.BarIndex < 55 )
                {
                    return;
                }
            }

            var waves = waveImportance.Where(x => ( x.Key > begin.Key && x.Key < end.Key && x.Value.WaveImportance == lower ) ).ToArray();

            int count = waves.Count( );

            if ( count >= 2 )
            {
                ref SBar beginBar = ref Bars.GetBarByTime(  begin.Key );

                KeyValuePair<long, WavePointImportance> previous = begin;

                int i = 0;

                while ( ( i + 1 ) < count )
                {
                    ref SBar previousBar = ref Bars.GetBarByTime(  previous.Key );
                    var firstWave   = waves[ i ];
                    var secondWave  = waves[ i + 1 ];

                    ref SBar firstBar = ref Bars.GetBarByTime( firstWave.Key );
                    ref SBar secondBar = ref Bars.GetBarByTime( secondWave.Key );

                    CheckIfPriorTrendTimeSquareWithCurrentPriceRange( period, taManager, previous, firstWave, secondWave, TrendDirection.Uptrend, false, TaGannPriceTimeType.PriorTrendTime_CurrentTrendRange_Local );

                    var oneDegreelower = FinancialHelper.GetWaveImportanceDegreeLower( lower );

                    Square_PriorTrendTime_CurrentPriceRange_ForLowerWaves_UpTrend( symbol, period, taManager, oneDegreelower, previous, firstWave, waveImportance );

                    previous = secondWave;

                    i = i + 2;
                }

            }
            else
            {
                var oneDegreelower = FinancialHelper.GetWaveImportanceDegreeLower( lower );

                Square_PriorTrendTime_CurrentPriceRange_ForLowerWaves_UpTrend( symbol, period, taManager, oneDegreelower, begin, end, waveImportance );
            }
        }



        private void Square_PriorTrendTime_CurrentPriceRange_MajorDownTrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> oldestWave, KeyValuePair<long, WavePointImportance> latestWave, KeyValuePair<long, WavePointImportance>[ ] selectedWaves )
        {
            var beginOfDowntrend = GetBeginningOfDowntrend( symbol, period, taManager, latestWave, selectedWaves );

            if ( beginOfDowntrend.HasValue )
            {
                if ( beginOfDowntrend.Value.Key > oldestWave.Key )
                {
                    var waves = selectedWaves.Where(x => x.Key >= oldestWave.Key && x.Key <= beginOfDowntrend.Value.Key  ).ToArray();

                    var beginOfUptrend = GetBeginningOfUptrend( symbol, period, taManager, beginOfDowntrend.Value, waves );

                    if ( beginOfUptrend.HasValue )
                    {
                        CheckIfPriorTrendTimeSquareWithCurrentPriceRange( period, taManager, beginOfUptrend.Value, beginOfDowntrend.Value, latestWave, TrendDirection.DownTrend, true, TaGannPriceTimeType.PriorTrendTime_CurrentTrendRange_Major );

                        if ( beginOfUptrend.Value.Key > oldestWave.Key )
                        {
                            waves = selectedWaves.Where( x => x.Key >= oldestWave.Key && x.Key <= beginOfUptrend.Value.Key ).ToArray( );

                            Square_PriorTrendTime_CurrentPriceRange_MajorDownTrend( symbol, period, taManager, oldestWave, beginOfUptrend.Value, waves );
                        }
                    }
                }
            }
        }

        private void Square_PriorTrendTime_CurrentPriceRange_MajorUpTrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> oldestWave, KeyValuePair<long, WavePointImportance> latestWave, KeyValuePair<long, WavePointImportance>[ ] selectedWaves )
        {
            var beginOfUptrend = GetBeginningOfUptrend( symbol, period, taManager, latestWave, selectedWaves );

            if ( beginOfUptrend.HasValue )
            {
                if ( beginOfUptrend.Value.Key > oldestWave.Key )
                {
                    var waves = selectedWaves.Where(x => x.Key >= oldestWave.Key && x.Key <= beginOfUptrend.Value.Key  ).ToArray();

                    var beginOfDowntrend = GetBeginningOfDowntrend( symbol, period, taManager, beginOfUptrend.Value, waves );

                    if ( beginOfDowntrend.HasValue )
                    {
                        CheckIfPriorTrendTimeSquareWithCurrentPriceRange( period, taManager, beginOfDowntrend.Value, beginOfUptrend.Value, latestWave, TrendDirection.Uptrend, true, TaGannPriceTimeType.PriorTrendTime_CurrentTrendRange_Major );

                        if ( beginOfDowntrend.Value.Key > oldestWave.Key )
                        {
                            waves = selectedWaves.Where( x => x.Key >= oldestWave.Key && x.Key <= beginOfDowntrend.Value.Key ).ToArray( );

                            Square_PriorTrendTime_CurrentPriceRange_MajorUpTrend( symbol, period, taManager, oldestWave, beginOfDowntrend.Value, waves );
                        }
                    }
                }
            }
            else
            {

            }
        }

        protected void Square_CurrentPrice_TimeElapsed( Security symbol, TimeSpan period, PeriodXTaManager taManager, BTreeDictionary<long, WavePointImportance> waveImportance )
        {
            var waves            = waveImportance.Where(x => x.Value.WaveImportance >= GlobalConstants.DAILYIMPT && x.Key > _last3rdRedTime ).ToArray();
            var peakTroughCounts = waves.Count();

            /* --------------------------------------------------------------------------------------------------------------------------
             *              
             * 1. * First we find all the big trends and do the Price and time squaring
             * 
             * --------------------------------------------------------------------------------------------------------------------------
            */
            if ( peakTroughCounts > 1 )
            {
                var oldestWave = waves[ 0 ];
                var latestWave = waves[ peakTroughCounts - 1 ];

                if ( ( oldestWave.Key == _price_TimeElapsed_oldestWave.Key ) && ( oldestWave.Value == _price_TimeElapsed_oldestWave.Value ) && ( latestWave.Key == _price_TimeElapsed_latestWave.Key ) && ( latestWave.Value == _price_TimeElapsed_latestWave.Value ) )
                {
                    return;
                }

                _price_TimeElapsed_oldestWave = oldestWave;
                _price_TimeElapsed_latestWave = latestWave;

                // Uptrend
                if ( latestWave.Value.Signal == TASignal.WAVE_PEAK )
                {
                    Square_CurrentPrice_TimeElapsed_MajorUpTrend( symbol, period, taManager, oldestWave, latestWave, waves );
                }
                // Downtrend
                else if ( latestWave.Value.Signal == TASignal.WAVE_TROUGH )
                {
                    Square_CurrentPrice_TimeElapsed_MajorDownTrend( symbol, period, taManager, oldestWave, latestWave, waves );
                }

                var redWaveCount = waves.Count();

                /* --------------------------------------------------------------------------------------------------------------------------
                 *              
                 * 2. We do price squaring for all the Red Dots
                 * 
                 * --------------------------------------------------------------------------------------------------------------------------
                */
                //for ( int i = 0; i < redWaveCount; i++ )
                //{
                //    if ( ( i + 1 ) < redWaveCount )
                //    {
                //        var latest   = waves[i + 1];
                //        var previous = waves[i];

                //        ref SBar olderBar = ref IndicatorBarsRepo.GetBarByTime( previous.Key );
                //        ref SBar newerBar = ref IndicatorBarsRepo.GetBarByTime( latest.Key );

                //        CheckIfPriceSquareWithTimeElapsed( period, taManager, ref olderBar, ref newerBar, true, TaGannPriceTimeType.CurrentPrice_TimeElapsed_RedDot );
                //    }
                //}


                /* --------------------------------------------------------------------------------------------------------------------------
                    *              
                    * 3. We do price squaring for all the other significant dots between Red Dots
                    * 
                    * --------------------------------------------------------------------------------------------------------------------------
                */
                //for ( int i = 0; i < redWaveCount; i++ )
                //{
                //    if ( period == TimeSpan.FromMinutes( 5 ) )
                //    {

                //    }

                //    if ( ( i + 1 ) < redWaveCount )
                //    {
                //        var latest   = waves[i + 1];
                //        var previous = waves[i];

                //        var lower = FinancialHelper.GetWaveImportanceDegreeLower( GlobalConstants.DAILYIMPT );

                //        Square_CurrentPrice_TimeElapsed_ForLowerWaves( symbol, period, taManager, lower, previous, latest, waveImportance );
                //    }
                //}
            }


        }

        private void Square_CurrentPrice_TimeElapsed_ForLowerWaves( Security symbol, TimeSpan period, PeriodXTaManager taManager, int lower, KeyValuePair<long, WavePointImportance> begin, KeyValuePair<long, WavePointImportance> end, BTreeDictionary<long, WavePointImportance> waveImportance )
        {
            if ( lower < 55 )
                return;

            if ( period == TimeSpan.FromMinutes( 5 ) )
            {

            }

            var waves = waveImportance.Where(x => ( x.Key > begin.Key && x.Key < end.Key && x.Value.WaveImportance == lower ) ).ToArray();

            int count = waves.Count( );

            if ( count > 0 )
            {
                ref SBar beginBar = ref Bars.GetBarByTime(  begin.Key );

                KeyValuePair<long, WavePointImportance> previous = begin;

                int i = 0;

                while ( i < count )
                {
                    ref SBar previousBar = ref Bars.GetBarByTime(  previous.Key );
                    var currentWave = waves[ i ];

                    ref SBar currentBar = ref Bars.GetBarByTime( currentWave.Key );

                    CheckIfPriceSquareWithTimeElapsed( period, taManager, ref previousBar, ref currentBar, false, TaGannPriceTimeType.CurrentPrice_TimeElapsed_RedDot );

                    var oneDegreelower = FinancialHelper.GetWaveImportanceDegreeLower( lower );

                    Square_CurrentPrice_TimeElapsed_ForLowerWaves( symbol, period, taManager, oneDegreelower, previous, currentWave, waveImportance );

                    i++;

                    previous = currentWave;
                }

            }
            else
            {
                var oneDegreelower = FinancialHelper.GetWaveImportanceDegreeLower( lower );

                Square_CurrentPrice_TimeElapsed_ForLowerWaves( symbol, period, taManager, oneDegreelower, begin, end, waveImportance );
            }
        }

        private void Square_CurrentPrice_TimeElapsed_LocalDownTrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> previous, KeyValuePair<long, WavePointImportance> latest, BTreeDictionary<long, WavePointImportance> waveImportance )
        {
            var innerLowerLowTroughs     = GetLowerLowTroughsBtwXY_wLatest( period, waveImportance, latest, previous );
            var troughsCount             = innerLowerLowTroughs.Count();
            int j                        = troughsCount - 1;


            /* --------------------------------------------------------------------------------------------------------------------------
             * 
             * First find all the local troughs with respect to the TOP
             * 1. Firstly, we need to determine the total number of bars from the TOP for all the troughs
             * 
             * --------------------------------------------------------------------------------------------------------------------------
            */
            ref SBar theTopBar = ref Bars.GetBarByTime(  previous.Key );

            while ( j >= 0 )
            {
                var currentTrough = innerLowerLowTroughs[j];
                ref SBar barCurrent = ref Bars.GetBarByTime( currentTrough.Key );

                if ( barCurrent.BarIndex < _lastMacdCrossIndex )
                {
                    CheckIfPriceSquareWithTimeElapsed( period, taManager, ref theTopBar, ref barCurrent, false, TaGannPriceTimeType.CurrentPrice_TimeElapsed_Local );
                }

                j--;
            }
        }

        private void Square_CurrentPrice_TimeElapsed_LocalUpTrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> previous, KeyValuePair<long, WavePointImportance> latest, BTreeDictionary<long, WavePointImportance> waveImportance )
        {
            var higherHighPeaks = GetHigherHighPeaksBtwXY_wLatest( period, waveImportance, latest, previous );
            var peaksCount      = higherHighPeaks.Count();
            int j               = peaksCount - 1;

            /* --------------------------------------------------------------------------------------------------------------------------
             * 
             * First find all the local troughs with respect to the TOP
             * 1. Firstly, we need to determine the total number of bars from the TOP for all the troughs
             * 
             * --------------------------------------------------------------------------------------------------------------------------
            */
            ref SBar bottomBar = ref Bars.GetBarByTime(  previous.Key );

            while ( j >= 0 )
            {
                var currentPeak = higherHighPeaks[j];
                ref SBar barCurrent = ref Bars.GetBarByTime( currentPeak.Key );

                if ( barCurrent.BarIndex < _lastMacdCrossIndex )
                {
                    CheckIfPriceSquareWithTimeElapsed( period, taManager, ref bottomBar, ref barCurrent, false, TaGannPriceTimeType.CurrentPrice_TimeElapsed_Local );
                }

                j--;
            }
        }



        private void Square_CurrentPrice_TimeElapsed_MajorDownTrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> oldestWave, KeyValuePair<long, WavePointImportance> latestWave, KeyValuePair<long, WavePointImportance>[ ] selectedWaves )
        {
            var innerPeaks           = new PooledList<KeyValuePair<long, WavePointImportance>>();
            var innerCheckingTroughs = new PooledList<KeyValuePair<long, WavePointImportance>>();

            GetLowerLowPeaksFromX_wTroughs( symbol, period, selectedWaves, latestWave, ref innerPeaks, ref innerCheckingTroughs );

            KeyValuePair< long, WavePointImportance >? lastExtremum = null;

            int i = 0;
            int t = 0;

            var troughsCount = innerCheckingTroughs.Count();
            var peaksCount = innerPeaks.Count();

            while ( t < troughsCount )
            {
                var trough = innerCheckingTroughs[ t ];
                ref SBar troughBar = ref Bars.GetBarByTime(  trough.Key );

                var peaksToCheck = innerPeaks.Where( x => x.Key < trough.Key ).ToList();

                var peaksToCheckCount = peaksToCheck.Count();

                i = 0;

                if ( peaksToCheckCount > 0 )
                {
                    while ( i < peaksToCheckCount )
                    {
                        var currentPeak = peaksToCheck[ i ];
                        lastExtremum = currentPeak;

                        ref SBar barCurrent = ref Bars.GetBarByTime( currentPeak.Key );

                        CheckIfPriceSquareWithTimeElapsed( period, taManager, ref barCurrent, ref troughBar, true, TaGannPriceTimeType.CurrentPrice_TimeElapsed_Major );

                        i++;
                    }
                }

                t++;
            }

            if ( lastExtremum.HasValue && lastExtremum.Value.Key > oldestWave.Key )
            {
                var waves = selectedWaves.Where( x => x.Key >= oldestWave.Key && x.Key <= lastExtremum.Value.Key ).ToArray( );

                Square_CurrentPrice_TimeElapsed_MajorUpTrend( symbol, period, taManager, oldestWave, lastExtremum.Value, waves );
            }

            if ( troughsCount == 0 && peaksCount == 1 )
            {
                var peak = innerPeaks[ 0 ];

                ref SBar newerBar = ref Bars.GetBarByTime( latestWave.Key );
                ref SBar olderBar = ref Bars.GetBarByTime( peak.Key );

                if ( newerBar.BarIndex < _lastMacdCrossIndex )
                {
                    CheckIfPriceSquareWithTimeElapsed( period, taManager, ref olderBar, ref newerBar, true, TaGannPriceTimeType.CurrentPrice_TimeElapsed_Major );
                }

                if ( peak.Key > oldestWave.Key )
                {
                    var waves = selectedWaves.Where( x => x.Key >= oldestWave.Key && x.Key <= peak.Key ).ToArray( );

                    Square_CurrentPrice_TimeElapsed_MajorUpTrend( symbol, period, taManager, oldestWave, peak, waves );
                }

            }
        }

        private void Square_CurrentPrice_TimeElapsed_MajorUpTrend( Security symbol, TimeSpan period, PeriodXTaManager taManager, KeyValuePair<long, WavePointImportance> oldestWave, KeyValuePair<long, WavePointImportance> latestWave, KeyValuePair<long, WavePointImportance>[ ] selectedWaves )
        {
            var innerTroughs = new PooledList<KeyValuePair<long, WavePointImportance>>();

            var innerCheckingPeaks = new PooledList<KeyValuePair<long, WavePointImportance>>();

            GetHigherHighTroughsFromX_wPeaks( symbol, period, selectedWaves, latestWave, ref innerTroughs, ref innerCheckingPeaks );

            if ( period == TimeSpan.FromMinutes( 5 ) )
            {

            }

            KeyValuePair< long, WavePointImportance >? lastExtremum = null;

            int i = 0;
            int p = 0;

            var peaksCount   = innerCheckingPeaks.Count();
            var troughsCount = innerTroughs.Count();

            while ( p < peaksCount )
            {
                i = 0;

                var peak                = innerCheckingPeaks[ p ];
                ref SBar peakBar = ref Bars.GetBarByTime(  peak.Key );

                var troughsToCheck      = innerTroughs.Where( x => x.Key < peak.Key ).ToList();

                var troughsToCheckCount = troughsToCheck.Count();

                if ( troughsToCheckCount > 0 )
                {
                    while ( i < troughsToCheckCount )
                    {
                        var currentTrough = troughsToCheck[ i ];
                        lastExtremum = currentTrough;

                        ref SBar troughBar = ref Bars.GetBarByTime( currentTrough.Key );

                        CheckIfPriceSquareWithTimeElapsed( period, taManager, ref troughBar, ref peakBar, true, TaGannPriceTimeType.CurrentPrice_TimeElapsed_Major );

                        i++;
                    }
                }

                p++;
            }


            if ( lastExtremum.HasValue && lastExtremum.Value.Key > oldestWave.Key )
            {
                var waves = selectedWaves.Where( x => x.Key >= oldestWave.Key && x.Key <= lastExtremum.Value.Key ).ToArray( );

                Square_CurrentPrice_TimeElapsed_MajorDownTrend( symbol, period, taManager, oldestWave, lastExtremum.Value, waves );
            }

            if ( peaksCount == 0 && troughsCount == 1 )
            {
                var trough   = innerTroughs[ 0 ];

                ref SBar newerBar = ref Bars.GetBarByTime( latestWave.Key );
                ref SBar olderBar = ref Bars.GetBarByTime( trough.Key );

                if ( newerBar.BarIndex < _lastMacdCrossIndex )
                {
                    CheckIfPriceSquareWithTimeElapsed( period, taManager, ref olderBar, ref newerBar, true, TaGannPriceTimeType.CurrentPrice_TimeElapsed_Major );
                }

                if ( trough.Key > oldestWave.Key )
                {
                    var waves = selectedWaves.Where( x => x.Key >= oldestWave.Key && x.Key <= trough.Key ).ToArray( );

                    Square_CurrentPrice_TimeElapsed_MajorDownTrend( symbol, period, taManager, oldestWave, trough, waves );
                }
            }
        }
    }
}