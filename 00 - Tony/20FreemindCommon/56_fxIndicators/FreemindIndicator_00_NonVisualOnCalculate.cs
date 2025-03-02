using fx.Common;
using fx.Definitions;
using fx.Algorithm;

using System;
using fx.Collections;
using System.Linq;
using System.Threading.Tasks;
using fx.Bars;
#pragma warning disable 414

namespace fx.Indicators
{
    public partial class FreemindIndicator : CustomPlatformIndicator /*, ILogSource, ILogReceiver*/
    {
        private void NonVisualOnCalculate( bool fullRecalculation, HistoricBarsUpdateEventArg e )
        {
            var updateType = e.UpdateType;

            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return;
            }

            fullRecalculation = false;

            var barCount = Bars.TotalBarCount;
            _barCountBeforeCalculation = barCount;

            var period = Bars.Period.Value;

            _currentPeriod = period;


            // Tony:    Since I am ignoring the CurrentBarUpdate, there will be no need of safe gurading multiple threads that will call OnCalculate().
            //          This function gets called multiple times because of the tick data update.
            
            if ( updateType == DataBarUpdateType.Initial || updateType == DataBarUpdateType.HistoryUpdate || updateType == DataBarUpdateType.NewPeriod )
            {
                NonVisual_Step1_MustRunTasks( fullRecalculation, updateType, period, barCount );

                if ( _hasNewEmaCross || _hasNewMacdCross || _hasNewSmaCross )
                {
                    this.LogInfo( _currentPeriod.ToReadable() + "Performing After Crossing Tasks" );
                    NonVisual_Step2_RunAfterCrossingTasks( fullRecalculation, updateType, _currentPeriod, barCount );

                    this.LogInfo( _currentPeriod.ToReadable() + "Performing Advanced Tasks" );
                    NonVisual_Step3_CalculateAdvancedTasks( fullRecalculation, updateType, _currentPeriod, barCount );
                }

                _lastCandleCheckIndex = Bars.TotalBarCount - 2;
            }
            else if ( updateType == DataBarUpdateType.CurrentBarUpdate )
            {
                OnCalculateBasicTasks_CurrentBarUpdate( fullRecalculation, updateType, period );
            }
        }

        private void NonVisual_Step1_MustRunTasks( bool fullRecalculation, DataBarUpdateType? updateType, TimeSpan period, int curIterationBarcount )
        {
            if ( !VerifyIndicatorsCalculationResult() )
            {
                this.LogWarning( "Indicator Count not equal" );
            }

            var token = IndicatorExitToken;

            if ( token.IsCancellationRequested )
            {
                return;
            }


            //! --------------------------------------------------------------------------------------------------------------------------
            //!
            //! 1. * First we have to do all the most basic calculation including 
            //!
            //!     a) Calculate Macd and get all the cross over
            //!     b) Calculate SMA and get the price cross over the SMA line
            //!     c) Calculate Oscillator and get the overbought and oversold
            //!
            //!--------------------------------------------------------------------------------------------------------------------------

            var tasksToWait = new PooledList<Task>();

            var tasks = new Task[] {
                                        BasicPriceTask     ( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                        BasicMacdTask      ( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                        BasicOscillatorTask( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                        BasicSmaTask       ( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                        BasicEmaCrossTask  ( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                   };




            try
            {
                for ( int i = 0; i < tasks.Count(); i++ )
                {
                    if ( tasks[ i ] != null )
                    {
                        tasks[ i ].Start();
                    }
                }

                Task.WaitAll( tasksToWait.ToArray() );
            }
            catch ( AggregateException ex )
            {
                // enumerate the exceptions that have been aggregated                 
                foreach ( Exception inner in ex.InnerExceptions )
                {
                    string error = string.Format("Exception type {0} from {1}.\nStack Trace = {2}\n\n", inner.GetType(), inner.Source, inner.StackTrace);

                    this.LogWarning( error );
                }
            }
            finally
            {
                foreach ( var task in tasks )
                {
                    task.Dispose();
                }
            }
        }

        private void NonVisual_Step2_RunAfterCrossingTasks( bool fullRecalculation, DataBarUpdateType? updateType, TimeSpan period, int curIterationBarcount )
        {
            var token = IndicatorExitToken;

            if ( token.IsCancellationRequested )
            {
                return;
            }

            if ( !VerifyIndicatorsCalculationResult() )
            {
                this.LogWarning( "Indicator Count not equal" );
            }

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

            if ( aa != null )
                aa.SetWaveImportanceCalculationStatus( period, WorkFlowStatus.StartWork );
            else
                return;

            //! --------------------------------------------------------------------------------------------------------------------------
            //!
            //! 2. When there is SMA50, MACD, EMAs, or Oscillator cross, then we will do more detailed Calculation
            //!
            //!     d) Calculate all the Wave ZigZag
            //!     e) Calculate all the gann Swing.
            //!
            //!--------------------------------------------------------------------------------------------------------------------------

            var tasksToWait = new PooledList<Task>();

            var tasks = new Task[]  {
                                            BasicAroonCrossTask( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                            BasicHewRsiTask    ( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                            BasicSarTask       ( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                            BasicMfiTask       ( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                            BasicCciTask       ( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                            BasicBollingerTask ( fullRecalculation, updateType, ref tasksToWait ),
                                            BasicWaveZigZagTask( fullRecalculation, updateType, GlobalConstants.HRS08IMPT, 5, _neoHrs08, ref tasksToWait ),
                                            BasicWaveZigZagTask( fullRecalculation, updateType, GlobalConstants.DAILYIMPT, 5, _neoDaily, ref tasksToWait ),
                                            BasicWaveZigZagTask( fullRecalculation, updateType, GlobalConstants.WEEKLYIMPT, 5, _neoWeekly, ref tasksToWait ),
                                            BasicWaveZigZagTask( fullRecalculation, updateType, GlobalConstants.MONTHLYIMPT, 5, _neoMonthly, ref tasksToWait )
                                    };


            try
            {
                for ( int i = 0; i < tasks.Count(); i++ )
                {
                    if ( tasks[ i ] != null )
                    {
                        tasks[ i ].Start();
                    }
                }

                Task.WaitAll( tasksToWait.ToArray() );
            }
            catch ( AggregateException ex )
            {
                // enumerate the exceptions that have been aggregated                 
                foreach ( Exception inner in ex.InnerExceptions )
                {
                    string error = string.Format("Exception type {0} from {1}.\nStack Trace = {2}\n\n", inner.GetType(), inner.Source, inner.StackTrace);

                    this.LogWarning( error );
                }
            }
            finally
            {
                if ( aa != null )
                    aa.SetWaveImportanceCalculationStatus( period, WorkFlowStatus.DoneWork );

                foreach ( var task in tasks )
                {
                    task.Dispose();
                }
            }
        }

        private void NonVisual_Step3_CalculateAdvancedTasks( bool fullRecalculation, DataBarUpdateType? updateType, TimeSpan period, int barsCountBeforeCalculation )
        {
            var token = IndicatorExitToken;

            if ( token.IsCancellationRequested )
            {
                return;
            }

            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return;
            }

            if ( !VerifyIndicatorsCalculationResult() )
            {
                this.LogWarning( "Indicator Count not equal" );
            }

            //! --------------------------------------------------------------------------------------------------------------------------
            //!
            //! 2. Secondly we want to detect all the following advanced features.
            //!
            //!     a) Using the Zig Zag calculation to get all the MACD and Price Divergence.
            //!     b) Do wave Rotation
            //!     c) Do Gann Price Square.
            //!     d) Figure out if the Zig Zag are touching any significant Pivot Point
            //!     e) Figure out the calendar still pattern associated with the Zig Zag wave Importance.
            //!
            //!--------------------------------------------------------------------------------------------------------------------------

            var tasksToWait = new PooledList<Task>();

            var advancedTasks = new Task[]
                                            {
                                                NonVisual_AdvancedMacdDivergenceTasks( fullRecalculation, updateType, ref tasksToWait, barsCountBeforeCalculation ),
                 
                                            };



            try
            {
                for ( int i = 0; i < advancedTasks.Count(); i++ )
                {
                    if ( advancedTasks[ i ] != null )
                    {
                        advancedTasks[ i ].Start();
                    }
                }


                Task.WaitAll( tasksToWait.ToArray() );
            }
            catch ( AggregateException ex )
            {
                // enumerate the exceptions that have been aggregated                 
                foreach ( Exception inner in ex.InnerExceptions )
                {
                    string error = string.Format("Exception type {0} from {1}.\nStack Trace = {2}\n\n", inner.GetType(), inner.Source, inner.StackTrace);

                    this.LogWarning( error );
                }
            }
            finally
            {
                foreach ( var task in advancedTasks )
                {
                    task.Dispose();
                }
            }
        }

        protected Task NonVisual_AdvancedMacdDivergenceTasks( bool fullRecalculation, DataBarUpdateType? updateType, ref PooledList<Task> tasksList, int barB4Calculation )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            Task first = new Task(() => DetectDivergenceFromZigZag( fullRecalculation, updateType, barB4Calculation, IndicatorExitToken ), IndicatorExitToken);

            tasksList.Add( first );

            return first;
        }

    }
}