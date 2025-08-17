using System;
using fx.Database.Common.DataModel;
using fx.Database.ForexDatabarsDataModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Linq;
using fx.Definitions;
using fx.Database;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Collections;
using fx.Definitions.Collections;
using StockSharp.BusinessEntities;
using Ecng.Common;
using fx.TimePeriod;
using fx.Collections;
using fx.Bars;


namespace fx.Algorithm
{
    public partial class HewManager
    {
        private void AnalyseWave5ProjectionTarget( int waveScenarioNo, TimeSpan period, fxHistoricBarsRepo bars, long selectedBarTime, ElliottWaveCycle waveDegree )
        {
            /*
            var aa     = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( bars.Security );

            var symbol = bars.Security.Code;
            
            if ( aa == null )
            {
                return;
            }

            var waveKey = new WaveModelKey( bars.Security, period, waveScenarioNo, selectedBarTime );

            var wave4PredictWave5Target    = aa.GetOrCreateWaveModel( waveKey, bars, this );

            wave4PredictWave5Target.AnalysePastImpulsiveMove( waveScenarioNo, period, bars, selectedBarTime, ElliottWaveEnum.Wave4, waveDegree );
            wave4PredictWave5Target.AnalyseWave5Movement( waveScenarioNo, period, bars, selectedBarTime, ElliottWaveEnum.Wave4, waveDegree );
           
            var wave4Targets      = wave4PredictWave5Target.LargerTargets;

            if ( wave4Targets == null )
            {
                return;
            }

            var dailyPP           = ( IPivotPointIndicator ) aa.DailyPivotPoint;
            var weeklyPP          = ( IPivotPointIndicator ) aa.WeeklyPivotPoint;
            var monthlyPP         = ( IPivotPointIndicator ) aa.MonthlyPivotPoint;

            PivotPointsInfo monthlyPivotPoint = null;

            if ( monthlyPP == null )
            {
                this.LogError( "Monthly Pivot Point is missing, Calculating Target will be miinaccuratessing" );
            }
            else
            {
                monthlyPivotPoint = monthlyPP.GetPivotPointsAt( selectedBarTime.FromLinuxTime(), out TimeBlockEx temp3 );

                if ( monthlyPivotPoint == null )
                {
                    this.LogError( "Monthly Pivot Point is missing, Calculating Target will be miinaccuratessing" );
                }
                else
                {
                    foreach ( var pp in monthlyPivotPoint.AllPivotPoints )
                    {
                        var index = wave4Targets.FindIndex( x => Math.Abs( x.FibLevel - pp.Value ) < bars.PipsAllowance() );

                        if ( index > -1 )
                        {
                            var wave4CalcTarget = wave4Targets[ index ];

                            wave4CalcTarget.DynamicSRLinesRating += 30;
                        }
                    }
                }
            }

            PivotPointsInfo weeklyPivotPoint = null;

            if ( weeklyPP == null )
            {
                this.LogError( "Weekly Pivot Point is missing, Calculating Target will be inaccurate" );
            }
            else
            {
                weeklyPivotPoint = weeklyPP.GetPivotPointsAt( selectedBarTime.FromLinuxTime(), out TimeBlockEx temp2 );

                if ( weeklyPivotPoint == null )
                {
                    this.LogError( "Weekly Pivot Point is missing, Calculating Target will be inaccurate" );
                }
                else
                {
                    foreach ( var pp in weeklyPivotPoint.AllPivotPoints )
                    {
                        var index = wave4Targets.FindIndex( x => Math.Abs( x.FibLevel - pp.Value ) < bars.PipsAllowance() );

                        if ( index > -1 )
                        {
                            var wave4CalcTarget = wave4Targets[ index ];

                            wave4CalcTarget.DynamicSRLinesRating += 7;
                        }
                    }
                }
            }
            
            var today = DateTime.UtcNow;

            if ( dailyPP == null )
            {
                this.LogError( "Daily Pivot Point is missing, Calculating Target will be inaccurate" );
            }
            else
            {
                var dailyPivotPoint = dailyPP.GetPivotPointsAt( today, out TimeBlockEx temp1 );

                if ( dailyPivotPoint == null )
                {
                    this.LogError( "Daily Pivot Point is missing, Calculating Target will be inaccurate" );
                }
                else
                {
                    foreach ( var pp in dailyPivotPoint.AllPivotPoints )
                    {
                        var index = wave4Targets.FindIndex( x => Math.Abs( x.FibLevel - pp.Value ) < bars.PipsAllowance() );

                        if ( index > -1 )
                        {
                            var wave4CalcTarget = wave4Targets[ index ];

                            wave4CalcTarget.DynamicSRLinesRating += 1;
                        }
                    }
                }
            }

                


            WaveTargetsTsoCollection waveTargets = aa.WaveTargetBindingList;

            waveTargets.Clear( );


            foreach ( var target in wave4Targets )
            {
                waveTargets.Add( target );
            }

            Messenger.Default.Send( new WavePredictionMessage( _security, selectedBarTime ) );
            */
        }
    }

}
