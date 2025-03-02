using fx.Common;
using fx.Bars;

using DevExpress.Mvvm;

using fx.TimePeriod;
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
using System.Collections.Generic;

#pragma warning disable 414

namespace fx.Indicators
{
    public partial class FreemindIndicator : CustomPlatformIndicator
    {
        private PivotPointsCustom _dailyPivotIndicator = null;
        private PivotPointsCustom _weeklyPivotIndicator = null;
        private PivotPointsCustom _monthlyPivotIndicator = null;

        static bool _showPivotPoints = false;

        protected Task AdvancedPivotPointRSTasks( bool fullRecalculation, DataBarUpdateType? updateType, ref PooledList<Task> tasksList, int barsCountBeforeCalculation )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }            

            Task first = new Task(() => DetectPivotPointResistanceSupportFromZigZag( fullRecalculation, updateType, IndicatorExitToken ), IndicatorExitToken);
            Task second = first.ContinueWith(
                                                antecedent =>
                                                {
                                                    if (antecedent.Status == TaskStatus.Faulted)
                                                    {
                                                        this.LogError("CalculateMACD() caused Exception. Stack = " + antecedent.Exception.InnerException.StackTrace);
                                                    }
                                                    else
                                                    {
                                                        DetectCandleSticksFromZigZag( fullRecalculation, updateType, barsCountBeforeCalculation);
                                                    }

                                                }, IndicatorExitToken);

            tasksList.Add( first );
            tasksList.Add( second );

            return first;
        }
        private void DetectPivotPointResistanceSupportFromZigZag( bool fullRecalculation, DataBarUpdateType? updateType, CancellationToken token )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return;
            }

            //ThreadHelper.UpdateThreadName( "DetectPivotPointResistanceSupportFromZigZag" );

            var symbol             = Bars.Security;
            var period             = Bars.Period.Value;

            var aa = ( AdvancedAnalysisManager) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security );
            if ( aa == null )
                return;            

            _dailyPivotIndicator   = ( PivotPointsCustom ) aa.DailyPivotPoint;
            _weeklyPivotIndicator  = ( PivotPointsCustom ) aa.WeeklyPivotPoint;
            _monthlyPivotIndicator = ( PivotPointsCustom ) aa.MonthlyPivotPoint;

            if ( _dailyPivotIndicator == null || _weeklyPivotIndicator == null || _monthlyPivotIndicator == null )
            {
                return;
            }

            if ( _dailyPivotIndicator.DoneInitialDataLoad == false )
            {
                return;
            }

            
            if ( _weeklyPivotIndicator.DoneInitialDataLoad == false )
            {
                return;
            }

            
            if ( _monthlyPivotIndicator.DoneInitialDataLoad == false )
            {
                return;
            }

            if ( ! _showPivotPoints  )
            {
                var calcDate = DateTime.UtcNow;                

                var currentTime = _dailyPivotIndicator.GetTimeBlock( calcDate );

                if ( currentTime != TimeBlockEx.EmptyBlock )
                {
                    Messenger.Default.Send( new PivotsPointUpdateMessage( symbol.Code, ShowPivotPoints.SHOWTODAY ) );

                    _showPivotPoints = true;
                }                
            }


            if ( _hews != null )
            {
                aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );
                if ( aa == null )
                    return;

                var taManager = ( PeriodXTaManager )aa.GetPeriodXTa( period );
                
                var waveImportance = _hews.GetAscendingWaveImportanceClone(period);

                int waveImportanceByTime = GetWaveImportanceForPivotPointByTime( period );

                var waves = waveImportance.Where(x =>
                                                        {
                                                            if (x.Value.WaveImportance >= waveImportanceByTime )
                                                            {
                                                                ref SBar bar = ref Bars.GetBarByTime( x.Key );
                                                                if( bar == SBar.EmptySBar  )
                                                                    return false;

                                                                if ( (int)bar.BarIndex < _lastMacdCrossIndex )
                                                                {
                                                                    return true;
                                                                }
                                                            }
                                                            return false;
                                                        }
                                               );
                
                var peakTroughCounts = waves.Count();

                /* --------------------------------------------------------------------------------------------------------------------------
                 *              
                 * 1. * First we find all the Red Dots and find its corresponding Pivots.
                 * 
                 * --------------------------------------------------------------------------------------------------------------------------
                */

                foreach ( KeyValuePair<long, WavePointImportance> wave in waves )
                {                    
                    if ( wave.Value.Signal == TASignal.WAVE_PEAK )
                    {                        
                        FindPeakBarRelationshipToPivotPoints( symbol, wave, taManager, token );
                    }
                    // Downtrend
                    else if ( wave.Value.Signal == TASignal.WAVE_TROUGH )
                    {
                        FindTroughBarRelationShipToPivotPoints( symbol, wave, taManager, token );                        
                    }
                }               
            }
        }

        

        private void FindTroughBarRelationShipToPivotPoints( Security symbol, KeyValuePair<long, WavePointImportance> wave, PeriodXTaManager taManager, CancellationToken token )
        {
            ref SBar troughBar = ref Bars.GetBarByTime( wave.Key );

            var period           = Bars.Period.Value;            

            TimeBlockEx timePeriod;            

            _dailyPivotPoints    = _dailyPivotIndicator.GetPivotPointsAt( troughBar.BarTime, out timePeriod );
            _weeklyPivotPoints   = _weeklyPivotIndicator.GetPivotPointsAt( troughBar.BarTime, out timePeriod );
            _monthlyPivotPoints  = _monthlyPivotIndicator.GetPivotPointsAt( troughBar.BarTime, out timePeriod );

            if ( _dailyPivotPoints != null )
            {
                var dailyRe         = _dailyPivotPoints.GetWaveTroughToPivotStatus( ref troughBar );

                if ( dailyRe.Count > 0 )
                {
                    taManager.AddPivotRelationship( ref troughBar, dailyRe );
                }
            }


            if ( _weeklyPivotPoints != null )
            {
                var weeklyRe        = _weeklyPivotPoints.GetWaveTroughToPivotStatus( ref troughBar );

                if ( weeklyRe.Count > 0 )
                {
                    taManager.AddPivotRelationship( ref troughBar, weeklyRe );
                }
            }

            if ( period == TimeSpan.FromMinutes( 5 ) && troughBar.BarIndex > 9990 )
            {

            }

            if ( _monthlyPivotPoints != null )
            {
                var monthlyRe       = _monthlyPivotPoints.GetWaveTroughToPivotStatus( ref troughBar );

                if ( monthlyRe.Count > 0 )
                {
                    taManager.AddPivotRelationship( ref troughBar, monthlyRe );
                }
            }            
        }

        private void FindPeakBarRelationshipToPivotPoints( Security symbol, KeyValuePair<long, WavePointImportance> wave, PeriodXTaManager taManager, CancellationToken token )
        {
            ref SBar peakBar = ref Bars.GetBarByTime( wave.Key );

            var period           = Bars.Period.Value;

            TimeBlockEx timePeriod;

            _dailyPivotPoints    = _dailyPivotIndicator.GetPivotPointsAt( peakBar.BarTime, out timePeriod );
            _weeklyPivotPoints   = _weeklyPivotIndicator.GetPivotPointsAt( peakBar.BarTime, out timePeriod );
            _monthlyPivotPoints  = _monthlyPivotIndicator.GetPivotPointsAt( peakBar.BarTime, out timePeriod );

            if ( _dailyPivotPoints != null )
            {
                var dailyRe         = _dailyPivotPoints.GetWavePeakToPivotStatus(  ref peakBar );

                if ( dailyRe.Count > 0 )
                {
                    taManager.AddPivotRelationship( ref peakBar, dailyRe );
                }
            }


            if ( _weeklyPivotPoints != null )
            {
                var weeklyRe        = _weeklyPivotPoints.GetWavePeakToPivotStatus(  ref peakBar );

                if ( weeklyRe.Count > 0 )
                {
                    taManager.AddPivotRelationship( ref peakBar, weeklyRe );
                }
            }

            if ( _monthlyPivotPoints != null )
            {
                var monthlyRe       = _monthlyPivotPoints.GetWavePeakToPivotStatus(  ref peakBar );

                if ( monthlyRe.Count > 0 )
                {
                    taManager.AddPivotRelationship( ref peakBar, monthlyRe );
                }
            }            
        }

        private int GetWaveImportanceForPivotPointByTime( TimeSpan period )
        {
            if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                return 34;
            }
            else if ( period == TimeSpan.FromMinutes( 4 ) )
            {
                return 21;
            }
            else if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                return 21;
            }
            else if ( period == TimeSpan.FromMinutes( 15 ) )
            {
                return 13;
            }
            else if ( period == TimeSpan.FromMinutes( 30 ) )
            {
                return 13;
            }
            else if ( period >= TimeSpan.FromHours( 1 ) )
            {
                return 8;
            }            
            else if ( period >= TimeSpan.FromDays( 1 ) )
            {
                return 5;
            }
            

            return 89;
        }
    }
}
