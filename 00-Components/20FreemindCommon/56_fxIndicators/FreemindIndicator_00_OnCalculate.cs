using DevExpress.Mvvm;

using fx.Common;
using fx.Definitions;
using fx.Algorithm;

using System;
using fx.Collections;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using fx.Bars;

#pragma warning disable 414

namespace fx.Indicators
{
    /// <summary>
    /// This indicator is a integrated for all the indicator that will be required for the major daily trend.	
    /// </summary>
    [Serializable]
    [UserFriendlyName( "Freemind Indicator" )]
    public partial class FreemindIndicator : CustomPlatformIndicator /*, ILogSource, ILogReceiver*/
    {        
        
        protected override void OnCalculate( bool fullRecalculation, HistoricBarsUpdateEventArg e )
        {
            _indicatorProcessFunc?.Invoke( fullRecalculation, e );
        }

        private void VisualOnCalculate( bool fullRecalculation, HistoricBarsUpdateEventArg e )
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
                Step1_MustRunTasks( fullRecalculation, updateType, period, barCount );

                if ( updateType == DataBarUpdateType.Initial || _hasNewEmaCross || _hasNewMacdCross || _hasNewSmaCross )
                {
                    this.LogInfo( _currentPeriod.ToReadable() + "Performing After Crossing Tasks" );
                    Step2_RunAfterCrossingTasks( fullRecalculation, updateType, _currentPeriod, barCount );

                    this.LogInfo( _currentPeriod.ToReadable() + "Performing Advanced Tasks" );
                    Step3_CalculateAdvancedTasks( fullRecalculation, updateType, _currentPeriod, barCount );

                    this.LogInfo( _currentPeriod.ToReadable() + "Identifying Elliott Waves Tasks" );
                    OnIdentifyElliottWave( fullRecalculation, updateType, period, barCount );
                }

                if ( e.UpdateType == DataBarUpdateType.Initial  )
                {
                    RaiseFullCalculationDoneEvent();
                }

                _lastCandleCheckIndex = Bars.TotalBarCount - 2;
            }
            else if ( updateType == DataBarUpdateType.CurrentBarUpdate )
            {
                OnCalculateBasicTasks_CurrentBarUpdate( fullRecalculation, updateType, period );
            }
        }

        

        private void OnCalculateBasicTasks_CurrentBarUpdate( bool fullRecalculation, DataBarUpdateType? updateType, TimeSpan period )
        {
            if ( period == TimeSpan.FromMinutes( 5 ) )
            {

            }

            if ( period > TimeSpan.FromMinutes( 5 ) && period < TimeSpan.FromDays( 1 ) )
            {
                var currentTime = DateTime.UtcNow;

                if ( currentTime > ( _lastCurrentBarUpdate + TimeSpan.FromMinutes( 1 ) ) )
                {
                    var curIterationBarcount = Bars.TotalBarCount;

                    _lastCurrentBarUpdate = _lastCurrentBarUpdate + TimeSpan.FromMinutes( 1 );

                    var tasksToWait = new PooledList<Task>();

                    var tasks = new Task[] 
                                            {
                                                BasicPriceTask( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                                BasicAroonCrossTask( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                                BasicMacdTask      ( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                                BasicOscillatorTask( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                                BasicHewRsiTask    ( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                                BasicSmaTask       ( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                                BasicSarTask       ( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                                BasicMfiTask       ( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                                BasicCciTask       ( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                                BasicEmaCrossTask  ( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                                BasicBollingerTask ( fullRecalculation, updateType, ref tasksToWait )
                                            
                                            };

                    for ( int i = 0; i < tasks.Count( ); i++ )
                    {
                        tasks [ i ].Start( );
                    }


                    try
                    {
                        Task.WaitAll( tasksToWait.ToArray( ) );
                    }
                    catch ( AggregateException ex )
                    {
                        // enumerate the exceptions that have been aggregated                 
                        foreach ( Exception inner in ex.InnerExceptions )
                        {
                            string error = string.Format("Exception type {0} from {1}.\nStack Trace = {2}\n\n", inner.GetType(), inner.Source, inner.StackTrace);
                           
                            this.LogError( error );
                        }
                    }
                    finally
                    {
                        foreach ( var task in tasks )
                        {
                            task.Dispose( );
                        }
                    }                    
                }
            }

            if ( _needToRebuildWaveImportance )
            {
                var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

                if( aa != null )
                    aa.SetWaveImportanceCalculationStatus( period, WorkFlowStatus.StartWork );
                else
                    return;

                //! --------------------------------------------------------------------------------------------------------------------------
                //!
                //! 1. * First we have to do all the most basic calculation including 
                //!
                //!     a) Calculate Macd and get all the cross over
                //!     b) Calculate SMA and get the price cross over the SMA line
                //!     c) Calculate Oscillator and get the overbought and oversold
                //!     d) Calculate all the Wave ZigZag
                //!     e) Calculate all the gann Swing.
                //!
                //!--------------------------------------------------------------------------------------------------------------------------

                var tasksToWait = new PooledList<Task>( );

                var tasks = new Task[ ] {
                                            BasicGannZigZagTask( fullRecalculation, updateType, 0, _gannVariables0, ref tasksToWait),
                                            BasicGannZigZagTask( fullRecalculation, updateType, 1, _gannVariables1, ref tasksToWait),
                                            BasicGannZigZagTask( fullRecalculation, updateType, 2, _gannVariables2, ref tasksToWait),
                                            BasicGannZigZagTask( fullRecalculation, updateType, 3, _gannVariables3, ref tasksToWait),
                                            BasicGannZigZagTask( fullRecalculation, updateType, 4, _gannVariables4, ref tasksToWait),
                                            BasicGannZigZagTask( fullRecalculation, updateType, 5, _gannVariables5, ref tasksToWait),
                                            BasicWaveZigZagTask( fullRecalculation, updateType, 5, 5, _neoVariables5, ref tasksToWait ),
                                            BasicWaveZigZagTask( fullRecalculation, updateType, 8 ,8, _neoVariables8, ref tasksToWait ),
                                            BasicWaveZigZagTask( fullRecalculation, updateType, 13, 13, _neoVariables13, ref tasksToWait ),
                                            BasicWaveZigZagTask( fullRecalculation, updateType, 21, 21, _neoVariables21, ref tasksToWait ),
                                            BasicWaveZigZagTask( fullRecalculation, updateType, 34, 34, _neoVariables34, ref tasksToWait ),
                                            BasicWaveZigZagTask( fullRecalculation, updateType, 55, 55, _neoVariables55, ref tasksToWait ),
                                            BasicWaveZigZagTask( fullRecalculation, updateType, GlobalConstants.HRS08IMPT, 5, _neoHrs08, ref tasksToWait ),
                                            BasicWaveZigZagTask( fullRecalculation, updateType, GlobalConstants.DAILYIMPT, 5, _neoDaily, ref tasksToWait )
                                        };

                for ( int i = 0; i < tasks.Count( ); i++ )
                {
                    if ( tasks[ i ] != null )
                    {
                        tasks[ i ].Start( );
                    }
                }


                try
                {
                    Task.WaitAll( tasksToWait.ToArray( ) );
                }
                catch ( AggregateException ex )
                {
                    // enumerate the exceptions that have been aggregated                 
                    foreach ( Exception inner in ex.InnerExceptions )
                    {
                        string error = string.Format( "Exception type {0} from {1}.\nStack Trace = {2}\n\n", inner.GetType( ), inner.Source, inner.StackTrace );

                        this.LogWarning( error );
                    }
                }
                finally
                {                    
                    if ( aa != null )
                        aa.SetWaveImportanceCalculationStatus( period, WorkFlowStatus.DoneWork );                    

                    

                    foreach ( var task in tasks )
                    {
                        task.Dispose( );
                    }
                }
            }
        }

        private void Step1_MustRunTasks( bool fullRecalculation, DataBarUpdateType? updateType, TimeSpan period, int curIterationBarcount )
        {
            if ( !VerifyIndicatorsCalculationResult( ) )
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

            _fastZZ5.Bars   = Bars;
            _fastZZ8.Bars   = Bars;
            _fastZZ13.Bars  = Bars;
            _fastZZ21.Bars  = Bars;
            _fastZZ34.Bars  = Bars;
            _fastZZ55.Bars  = Bars;
            _fastZZ89.Bars  = Bars;
            _fastZZ144.Bars = Bars;

            var tasksToWait = new PooledList<Task>();            

            var tasks = new Task[] {
                                            BasicPriceTask     ( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                            BasicMacdTask      ( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                            BasicOscillatorTask( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                            BasicSmaTask       ( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                            BasicEmaCrossTask  ( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                            BasicWaveZigZagTask( fullRecalculation, updateType, GlobalConstants.MINS05IMPT ,1, _neoVariables5, ref tasksToWait ),
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

                Task.WaitAll( tasksToWait.ToArray( ) );
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
                    task.Dispose( );
                }
            }
        }

        private void Step2_RunAfterCrossingTasks( bool fullRecalculation, DataBarUpdateType? updateType, TimeSpan period, int curIterationBarcount )
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

            var tasks = new Task[] {
                                            BasicAroonCrossTask( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                            BasicHewRsiTask    ( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                            BasicSarTask       ( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                            BasicMfiTask       ( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                            BasicCciTask       ( fullRecalculation, updateType, curIterationBarcount, ref tasksToWait ),
                                            BasicBollingerTask ( fullRecalculation, updateType, ref tasksToWait ),
                                            
                                            BasicWaveZigZagTask( fullRecalculation, updateType, GlobalConstants.MINS15IMPT ,5, _neoVariables8, ref tasksToWait ),
                                            BasicWaveZigZagTask( fullRecalculation, updateType, GlobalConstants.MINS30IMPT, 5, _neoVariables13, ref tasksToWait ),
                                            BasicWaveZigZagTask( fullRecalculation, updateType, GlobalConstants.HRS01IMPT, 5, _neoVariables21, ref tasksToWait ),
                                            BasicWaveZigZagTask( fullRecalculation, updateType, GlobalConstants.HRS02IMPT, 5, _neoVariables34, ref tasksToWait ),
                                            BasicWaveZigZagTask( fullRecalculation, updateType, GlobalConstants.HRS04IMPT, 5, _neoVariables55, ref tasksToWait ),
                                            BasicWaveZigZagTask( fullRecalculation, updateType, GlobalConstants.HRS08IMPT, 5, _neoHrs08, ref tasksToWait ),
                                            BasicWaveZigZagTask( fullRecalculation, updateType, GlobalConstants.DAILYIMPT, 5, _neoDaily, ref tasksToWait ),
                                            BasicWaveZigZagTask( fullRecalculation, updateType, GlobalConstants.WEEKLYIMPT, 5, _neoWeekly, ref tasksToWait )

                                            //BasicGannZigZagTask( fullRecalculation, updateType, 0, _gannVariables0, ref tasksToWait),
                                            //BasicGannZigZagTask( fullRecalculation, updateType, 1, _gannVariables1, ref tasksToWait),
                                            //BasicGannZigZagTask( fullRecalculation, updateType, 2, _gannVariables2, ref tasksToWait),
                                            //BasicGannZigZagTask( fullRecalculation, updateType, 3, _gannVariables3, ref tasksToWait),
                                            //BasicGannZigZagTask( fullRecalculation, updateType, 4, _gannVariables4, ref tasksToWait),
                                            //BasicGannZigZagTask( fullRecalculation, updateType, 5, _gannVariables5, ref tasksToWait),
                                            //BasicFastZigZagTask( fullRecalculation, updateType, 1, 5, _fastZZ5,   ref tasksToWait ),                                            
                                            //BasicFastZigZagTask( fullRecalculation, updateType, 8 ,13, _fastZZ13, ref tasksToWait ),
                                            //BasicFastZigZagTask( fullRecalculation, updateType, 13, 21, _fastZZ21, ref tasksToWait ),
                                            //BasicFastZigZagTask( fullRecalculation, updateType, 21, 34, _fastZZ34, ref tasksToWait ),
                                            //BasicFastZigZagTask( fullRecalculation, updateType, 34, 55, _fastZZ55, ref tasksToWait ),
                                            //BasicFastZigZagTask( fullRecalculation, updateType, 55, 89, _fastZZ89, ref tasksToWait ),
                                            //BasicFastZigZagTask( fullRecalculation, updateType, 89, 144, _fastZZ144, ref tasksToWait ),

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

        

        private void Step3_CalculateAdvancedTasks( bool fullRecalculation, DataBarUpdateType? updateType, TimeSpan period, int barsCountBeforeCalculation )
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

            if ( !VerifyIndicatorsCalculationResult( ) )
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
                                                AdvancedMacdDivergenceTasks( fullRecalculation, updateType, ref tasksToWait, barsCountBeforeCalculation ),
                                                AdvancedWaveRotationTasks( fullRecalculation, updateType, ref tasksToWait ),
                                                AdvancedGannPriceTimeTasks( fullRecalculation, updateType, ref tasksToWait ),
                                                AdvancedPivotPointRSTasks( fullRecalculation, updateType, ref tasksToWait, barsCountBeforeCalculation ),                                            
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


                Task.WaitAll( tasksToWait.ToArray( ) );
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
                    task.Dispose( );
                }
            }            
        }

        private void OnDetectCandleTasks(bool fullRecalculation, DataBarUpdateType? updateType, TimeSpan period, int barsCountBeforeCalculation)
        {
            if (_noCalculationAllowed && (updateType != DataBarUpdateType.Reloaded))
            {
                return;
            }

            if (!VerifyIndicatorsCalculationResult())
            {
                this.LogWarning("Indicator Count not equal");
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
                                                TaskEngulfing( fullRecalculation, updateType, ref tasksToWait, barsCountBeforeCalculation ),
                                                TaskMorningStarOrDoji( fullRecalculation, updateType, ref tasksToWait, barsCountBeforeCalculation ),
                                                TaskEveningStarOrDoji( fullRecalculation, updateType, ref tasksToWait, barsCountBeforeCalculation ),
                                                TaskPiercingOrDarkCloudCover( fullRecalculation, updateType, ref tasksToWait, barsCountBeforeCalculation ),
                                            };

            for (int i = 0; i < advancedTasks.Count(); i++)
            {
                if (advancedTasks[i] != null)
                {
                    advancedTasks[i].Start();
                }
            }


            try
            {
                Task.WaitAll(tasksToWait.ToArray());
            }
            catch (AggregateException ex)
            {
                // enumerate the exceptions that have been aggregated                 
                foreach (Exception inner in ex.InnerExceptions)
                {
                    string error = string.Format("Exception type {0} from {1}.\nStack Trace = {2}\n\n", inner.GetType(), inner.Source, inner.StackTrace);

                    this.LogWarning(error);
                }
            }
            finally
            {
                foreach (var task in advancedTasks)
                {
                    task.Dispose();
                }
            }
        }



        private void IndicatorDynamicParametersUpdateEvent( string name, object value )
        {
            if ( name == "fxGannSwingBarCount" )
            {
                _numberOfBarsRequired = ( int ) value;

                //HasStartedPerformFullCalculationTask( );
            }
            else if ( name == "fxFastMA" )
            {

            }
            else if ( name == "fxSlowMA" )
            {

            }
            else if ( name == "fxSignal" )
            {

            }
            else if ( name == "ExtDepth" )
            {
                _extDepth = ( int ) value;
            }
            else if ( name == "ExtBackstep" )
            {
                _extBackstep = ( int ) value;
            }
            else if ( name == "RecoverFilter" )
            {
                _recoverFilter = ( bool ) value;
            }
        }

        public override bool StartPerformingLongCalculationTask( string name, object value )
        {
            if ( name == "ExtDepth" )
            {

            }
            else if ( name == "ExtBackstep" )
            {
                //Task detectTask  = new Task(
                //                                () =>
                //                                {
                //                                    RecalculateElliottWaveZigZag( );

                //                                }, IndicatorExitToken
                //                        );

                //detectTask.Start( );
            }
            else if ( name == "FullCalculation" )
            {
                Task detectTask = new Task(
                                            () =>
                                            {
                                                PerformFullCalculation();

                                            }, IndicatorExitToken
                                        );

                detectTask.Start();
            }



            return true;
        }

        public bool PerformFullCalculationTask( DoneReloadDatabars update )
        {
            Task detectTask = new Task(
                                            () =>
                                            {
                                                PerformFullCalculation(update);

                                            }, IndicatorExitToken
                                        );

            detectTask.Start();

            return true;
        }


        public override void HandleDynamicParametersUpdate( string name, object value )
        {

        }


        #region Public Methods

        public TASignal LastCrossingDirection
        {
            get { return _lastCrossingDirection; }
        }

        public bool IsBBandBuy( )
        {
            return ( _crossBollinger [ _crossBollinger.Count - 1 ] > 10 );
        }

        public bool IsBBandSell( )
        {
            return ( _crossBollinger [ _crossBollinger.Count - 1 ] < 10 );
        }

        public double AverageStrength( )
        {
            return ( _crossBollinger.Count > 0 ? _crossBollinger [ _crossBollinger.Count - 1 ] / ( _crossBollinger.Count - _currentBBandCross ) : 0 );
        }

        public bool IsLongTrendingStrongly( )
        {
            BBANDSStates lastBar = _signalBollinger[Bars.TotalBarCount - 1];
            return ( lastBar == BBANDSStates.SuperStrongBuy || lastBar == BBANDSStates.StrongBuy );
        }

        public bool IsShortTrendingStrongly( )
        {
            BBANDSStates lastBar = _signalBollinger[Bars.TotalBarCount - 1];
            return ( lastBar == BBANDSStates.SuperStrongSell || lastBar == BBANDSStates.StrongSell );
        }
        public bool IsLong( )
        {
            BBANDSStates lastBar = _signalBollinger[Bars.TotalBarCount - 1];
            return ( lastBar == BBANDSStates.SuperStrongBuy || lastBar == BBANDSStates.StrongBuy || lastBar == BBANDSStates.Buy );
        }

        public bool IsShort( )
        {
            BBANDSStates lastBar = _signalBollinger[Bars.TotalBarCount - 1];
            return ( lastBar == BBANDSStates.SuperStrongSell || lastBar == BBANDSStates.StrongSell || lastBar == BBANDSStates.Sell );
        }

        public int GetLongStrongTrendBarCount( )
        {
            int count = 0;

            for ( int i = Bars.TotalBarCount - 1; i > 0; i-- )
            {
                if ( _signalBollinger [ i ] == BBANDSStates.SuperStrongBuy || _signalBollinger [ i ] == BBANDSStates.StrongBuy )
                {
                    count++;
                }
                else
                {
                    return count;
                }
            }

            return count;
        }

        public int GetShortStrongTrendBarCount( )
        {
            int count = 0;

            for ( int i = Bars.TotalBarCount - 1; i > 0; i-- )
            {
                if ( _signalBollinger [ i ] == BBANDSStates.SuperStrongSell || _signalBollinger [ i ] == BBANDSStates.StrongSell )
                {
                    count++;
                }
                else
                {
                    return count;
                }
            }

            return count;
        }

        public int GetLongTrendBarCount( )
        {
            int count = 0;

            for ( int i = Bars.TotalBarCount - 1; i > 0; i-- )
            {
                if ( _signalBollinger [ i ] == BBANDSStates.SuperStrongBuy || _signalBollinger [ i ] == BBANDSStates.StrongBuy || _signalBollinger [ i ] == BBANDSStates.Buy )
                {
                    count++;
                }
                else
                {
                    return count;
                }
            }

            return count;
        }

        public int GetShortTrendBarCount( )
        {
            int count = 0;

            for ( int i = Bars.TotalBarCount - 1; i > 0; i-- )
            {
                if ( _signalBollinger [ i ] == BBANDSStates.SuperStrongSell || _signalBollinger [ i ] == BBANDSStates.StrongSell || _signalBollinger [ i ] == BBANDSStates.Sell )
                {
                    count++;
                }
                else
                {
                    return count;
                }
            }

            return count;
        }

        public bool JustExitOuterBollinger( )
        {
            if ( _signalBollinger [ Bars.TotalBarCount - 2 ] == BBANDSStates.SuperStrongSell ||
                 _signalBollinger [ Bars.TotalBarCount - 2 ] == BBANDSStates.StrongSell &&
                 _signalBollinger [ Bars.TotalBarCount - 1 ] == BBANDSStates.Sell )
            {
                return true;
            }

            if ( _signalBollinger [ Bars.TotalBarCount - 2 ] == BBANDSStates.SuperStrongBuy ||
                 _signalBollinger [ Bars.TotalBarCount - 2 ] == BBANDSStates.StrongBuy &&
                 _signalBollinger [ Bars.TotalBarCount - 1 ] == BBANDSStates.Buy )
            {
                return true;
            }

            return false;
        }

        

        

        #endregion










        protected PooledDictionary<CandleSettingEnum, CandleSetting> CandleSettings = new PooledDictionary<CandleSettingEnum, CandleSetting>();

        
               

        private IEnumerable<Bin> _realBodyHistogram;
        private IEnumerable<Bin> _bodyRangeHistogram;
        private IEnumerable<Bin> _topShadowHistogram;
        private IEnumerable<Bin> _lowerShadowHistogram;



        //private IEnumerable<Bin>    _realBodyHistogramNonActive;
        //private IEnumerable<Bin>    _bodyRangeHistogramNonActive;
        //private IEnumerable<Bin>    _topShadowHistogramNonActive;
        //private IEnumerable<Bin>    _lowerShadowHistogramNonActive;

        // Tony: Here I want to calculate the standard deviation         
        public override void OnInitialDataBarUpdatePreCalculation( bool fullRecalculation )
        {
            _doneFetchingFromDatabase = false;            

            _instrumentPointSize      = _indicatorSecurity.PriceStep.HasValue ? ( double ) _indicatorSecurity.PriceStep.Value : 1;
            _instrumentDigits         = _indicatorSecurity.Decimals.HasValue ? _indicatorSecurity.Decimals.Value : 5;

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

            if ( aa != null )
                aa.InitializeWaveImportanceCalculationStatus( );
            else
                return;            

            _currentPeriod            = Bars.Period.Value;
            _msgBindingList           = aa.GetTradingEventsMsgBindingList( _indicatorSecurity.Code );
                        
            ResetVariables( );

            _noCalculationAllowed = false;
            _doneFetchingFromDatabase = true;

            InitializeIndicatorParameters( );
        }




        private void InitializeIndicatorParameters( )
        {
            _isNotAboveHourlyBars = ( Bars.Period < TimeSpan.FromHours( 1 ) );

            
            var realBodyHeight = Bars.GetRealBodyAsPips();

            _realBodyHeightAverage = realBodyHeight.Average( );

            var dataBarRange   = Bars.GetRangeAsPips();
            _dataBarRangeAverage = dataBarRange.Average( );

            var topShadow      = Bars.GetUpperShadowAsPips();
            _topShadowAverage = topShadow.Average( );

            var bottomShadow   = Bars.GetLowerShadowAsPips();
            _bottomShadowAverage = bottomShadow.Average();



            if (Bars.Period == TimeSpan.FromHours( 1 ) )
            {

            }

            if ( Bars.TotalBarCount > 5 )
            {
                _realBodyHistogram    = EnumerableStats.Histogram( realBodyHeight, 5  );
                _bodyRangeHistogram   = EnumerableStats.Histogram( dataBarRange, 5 ); 
                _topShadowHistogram   = EnumerableStats.Histogram( topShadow, 5 ); 
                _lowerShadowHistogram = EnumerableStats.Histogram( bottomShadow, 5 ); 
            }
                

            if ( _macdSignificantCross == null )
            {
                _macdSignificantCross = new fx.Collections.OrderedDictionary<int, TASignal>();
            }

            
            

            if ( _macdExtremumDict == null )
            {
                _macdExtremumDict = new fx.Collections.OrderedDictionary<int, Tuple<MacdSignal, double>>();
            }

            //if ( _smoothedMacdDictionary == null )
            //{
            //    _smoothedMacdDictionary = new ThreadSafeIndexedDictionary<int, TASignal>( );
            //}

            if ( _stochasticsDictionary == null )
            {
                _stochasticsDictionary = new fx.Collections.OrderedDictionary<int, TASignal>();
            }


            if ( _smoothedStochasticsDictionary == null )
            {
                _smoothedStochasticsDictionary = new fx.Collections.OrderedDictionary<int, TASignal>();
            }            
            

            if ( _overBoughtSoldDictionary == null )
            {
                _overBoughtSoldDictionary = new fx.Collections.OrderedDictionary<int, TASignal>();
            }

            if ( _comasDictionary == null )
            {
                _comasDictionary = new DictionarySlim< int, TASignal >( );
            }            

            if ( _extremumsValueDictionary == null )
            {
                _extremumsValueDictionary = new DictionarySlim< int, Tuple< int, double, double > >( );
            }
            
            if ( _signalBollinger == null )
            {
                _signalBollinger = new ThreadSafeDictionary<int, BBANDSStates>();
            }

            if ( _crossBollinger == null )
            {
                _crossBollinger = new PooledList<Int32>();
            }

            if ( _macdDivergence == null )
            {
                _macdDivergence = new DictionarySlim< int, TASignal >( );
            }
            
            _numberOfBarsRequired = GetConsectiveNthBarBeforeTrendChange( Bars.Period.Value );

            for ( int i = 0; i < TALib.Core.BbandsLookback( ); i++ )
            {
                _signalBollinger.Add( i, BBANDSStates.Default );
                _crossBollinger.Add( 0 );
            }

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( Bars.Security.Code );

            if( aa == null )
                return;

            if ( _fxTradingEventsBindingList == null )
            {
                _fxTradingEventsBindingList = aa.TradingEventsBindingList;
            }

            if ( _fxBarPercentBindingList == null )
            {
                _fxBarPercentBindingList = aa.BarPercentageBindingList;
            }

            if ( _fxBBstrengthList == null )
            {
                _fxBBstrengthList = aa.BBstrengthBindingList;
            }

            if ( _periodXTaManager == null )
            {
                _periodXTaManager = ( PeriodXTaManager ) aa.GetPeriodXTa( _currentPeriod );
            }
            
            if ( null == _hews )
            {
                _hews = (HewManager) aa.HewManager;
            }
        }



        public override void OnHistoryBarUpdatePreCalculation( bool fullRecalculation )
        {
            InitializeIndicatorParameters();
        }

        public override void OnNewBarArrivesPreCalculation( bool fullRecalculation )
        {
            InitializeIndicatorParameters( );
        }

        private void InitializeVariables( )
        {
            _macdSignificantCross                = new fx.Collections.OrderedDictionary<int, TASignal>();
            _macdExtremumDict                    = new fx.Collections.OrderedDictionary<int, Tuple<MacdSignal, double>>( );            
            _stochasticsDictionary               = new fx.Collections.OrderedDictionary<int, TASignal>();
            _smoothedStochasticsDictionary       = new fx.Collections.OrderedDictionary<int, TASignal>();

            
            

            _overBoughtSoldDictionary            = new fx.Collections.OrderedDictionary<int, TASignal>();

            _comasDictionary = new DictionarySlim< int, TASignal >( );
            //_smoothedCosmaDictionary             = new ThreadSafeDictionary<int, TASignal>();
            _extremumsValueDictionary            = new DictionarySlim<int, Tuple<int, double, double>>();
            
            _signalBollinger                     = new ThreadSafeDictionary< int, BBANDSStates >( );
            _crossBollinger                      = new PooledList<Int32>();
            _macdDivergence                      = new DictionarySlim<int, TASignal>();
            

            _isNotAboveHourlyBars               = true;
            _lastMacdMaxIndex                   = 0;
            _lastMacdMinIndex                   = 0;

            _previousBBandCross                 = 0;
            _currentBBandCross                  = 0;

            _lastMacdCrossIndex               = 1;
            _lastMacdCrossUpIndex = 1;
            _lastMacdCrossDownIndex = 1;
            _indexOfLastSmoothedMacd            = 1;

            _lastOscillatorCrossIndex         = 1;
            _indexOfLastSmoothedOscillator      = 1;

            _indexOfLastComasTuringPoint        = 1;

            _indexOfLastBollingBandCross        = 0;

            _indexOfLastMaxValuesBtMacdPoints   = 1;
            _indexOfLastMacdNegDivergence       = 1;
            _indexOfLastMacdPosDivergence       = 1;

            _indexOfLastMaxValuesBtOscillator   = 1;
            _indexOfLastOscNegDivergence        = 1;
            _indexOfLastOscPosDivergence        = 1;

            _lastCandleCheckIndex               = 0;
            _currentComasSignificantPoint       = 0;
            _currentOscillatorCross             = 0;
            _exitOverBoughtIndex                = 0;
            _exitOverSoldIndex                  = 0;
            _lastCrossingDirection              = TASignal.NONE;

            _gannVariables0.ResetVariables();
            _gannVariables1.ResetVariables();
            _gannVariables2.ResetVariables();
            _gannVariables3.ResetVariables();
            _gannVariables4.ResetVariables();
            _gannVariables5.ResetVariables();


            for ( int i = 0; i < TALib.Core.BbandsLookback( ); i++ )
            {
                _signalBollinger.TryAdd( i, BBANDSStates.Default );
                _crossBollinger.Add( 0 );
            }
        }

        private void ResetVariables( )
        {
            var realBodyHeight = Bars.GetRealBodyAsPips();
            _realBodyHeightAverage = realBodyHeight.Average();

            var dataBarRange   = Bars.GetRangeAsPips();
            _dataBarRangeAverage = dataBarRange.Average();

            var topShadow      = Bars.GetUpperShadowAsPips();
            _topShadowAverage = topShadow.Average();

            var bottomShadow   = Bars.GetLowerShadowAsPips();
            _bottomShadowAverage = bottomShadow.Average( );


            if ( Bars.TotalBarCount > 5 )
            {
                _realBodyHistogram = EnumerableStats.Histogram( realBodyHeight, 5 );
                _bodyRangeHistogram = EnumerableStats.Histogram( dataBarRange, 5 );
                _topShadowHistogram = EnumerableStats.Histogram( topShadow, 5 );
                _lowerShadowHistogram = EnumerableStats.Histogram( bottomShadow, 5 );                

                Debug.Assert( _realBodyHistogram != null );
                Debug.Assert( _bodyRangeHistogram != null );
                Debug.Assert( _topShadowHistogram != null );
                Debug.Assert( _lowerShadowHistogram != null );
            }

            

            _macdSignificantCross          = new fx.Collections.OrderedDictionary<int, TASignal>();
            _macdExtremumDict              = new fx.Collections.OrderedDictionary<int, Tuple<MacdSignal, double>>( );
            
            _stochasticsDictionary         = new fx.Collections.OrderedDictionary<int, TASignal>();
            _smoothedStochasticsDictionary = new fx.Collections.OrderedDictionary<int, TASignal>();

            
            

            _overBoughtSoldDictionary      = new fx.Collections.OrderedDictionary<int, TASignal>();

            _comasDictionary = new DictionarySlim< int, TASignal >( );
            //_smoothedCosmaDictionary       = new ThreadSafeDictionary<int, TASignal>();
            _extremumsValueDictionary      = new DictionarySlim<int, Tuple<int, double, double>>();
            
            _signalBollinger               = new ThreadSafeDictionary<int, BBANDSStates>( );
            _crossBollinger                = new PooledList<Int32>();
            _macdDivergence = new DictionarySlim< int, TASignal >( );
            


            for ( int i = 0; i < TALib.Core.BbandsLookback( ); i++ )
            {
                _signalBollinger.TryAdd( i, BBANDSStates.Default );
                _crossBollinger.Add( 0 );
            }

            _isNotAboveHourlyBars             = true;

            _lastMacdMaxIndex                 = 0;
            _lastMacdMinIndex                 = 0;


            _previousBBandCross               = 0;
            _currentBBandCross                = 0;

            _lastMacdCrossIndex             = 1;
            _lastMacdCrossUpIndex           = 1;
            _lastMacdCrossDownIndex         = 1;
            _indexOfLastSmoothedMacd          = 1;

            _lastOscillatorCrossIndex       = 1;
            _indexOfLastSmoothedOscillator    = 1;

            _indexOfLastComasTuringPoint      = 1;

            _indexOfLastBollingBandCross      = 0;

            _indexOfLastMaxValuesBtMacdPoints = 1;
            _indexOfLastMacdNegDivergence     = 1;
            _indexOfLastMacdPosDivergence     = 1;

            _indexOfLastMaxValuesBtOscillator = 1;
            _indexOfLastOscNegDivergence      = 1;
            _indexOfLastOscPosDivergence      = 1;

            _lastCandleCheckIndex             = 0;
            _currentComasSignificantPoint     = 0;
            _currentOscillatorCross           = 0;
            _exitOverBoughtIndex              = 0;
            _exitOverSoldIndex                = 0;
            _lastCrossingDirection            = TASignal.NONE;

            _gannVariables0.ResetVariables();
            _gannVariables1.ResetVariables();
            _gannVariables2.ResetVariables();
            _gannVariables3.ResetVariables();
            _gannVariables4.ResetVariables();
            _gannVariables5.ResetVariables();

        }

        #region MACD                

        //protected int Find

        //protected void FindImpulsiveAndCorrectiveWaves( )
        //{
        //    int indexOfHighest = IndicatorBarsRepo.IndexOfHighestOfSession;
        //    int indexOfLowest  = IndicatorBarsRepo.IndexOfLowestOfSession;

        //    int startingIndex = (indexOfHighest > indexOfLowest) ? indexOfHighest : indexOfLowest;

        //    //int startIndexOfGannSwing = 0;

        //    for ( int i = 1; i < _extremumsValueDictionary.Count; i++ )
        //    {
        //        var previousExtremum = _extremumsValueDictionary.ElementAt(i - 1);
        //        var currentExtremum = _extremumsValueDictionary.ElementAt(i);
        //    }
            
        //}

        


        #endregion

        #region Oscillator

        

        


        
        #endregion

        #region COMAS

        public double MarketDirectionNumber
        {
            get
            {
                if ( Bars.TotalBarCount > 0 )
                {
                    return IndicatorResult [ "MarketDirectionNo" ] [ Bars.TotalBarCount - 2 ];
                }

                return 0;
            }
        }

        protected void CalculateCOMAS( bool fullRecalculation, DataBarUpdateType? updateType, int barB4Calculation )
        {
            if ( ( updateType == DataBarUpdateType.Initial ) ||
                 ( updateType == DataBarUpdateType.HistoryUpdate ) )
            {                
                double[] slowComas,
                          fastComas,
                          marketDirectionNo,
                          pivot;
                

                int repoStartingIndex = 0;
                int resultSetLength = IndicatorResult["SlowComas"].Count;
                int startIndex = 0;
                int endIndex = 0;
                int outBeginIdx = 0;
                int outNBElement = 0;
                int indexCount = 0;

                startIndex = 0;
                endIndex = barB4Calculation - repoStartingIndex - 1;
                indexCount = endIndex - startIndex + 1;

                if ( endIndex < 0 || indexCount < 0 )
                {
                    return;
                }

                outBeginIdx = 0;
                outNBElement = 0;

                slowComas = new double [ indexCount ];
                fastComas = new double [ indexCount ];
                pivot = new double [ indexCount ];
                marketDirectionNo = new double [ indexCount ];


                for ( int i = startIndex; i <= endIndex; i++ )
                {
                    pivot [ i ] = ( Bars[ i ].High + Bars[ i ].Low + Bars[ i ].Close ) / 3;
                }

                IndicatorResult.AddSetValues( "PivotPoint", repoStartingIndex + startIndex, endIndex + 1, true, pivot );

                outBeginIdx = 0;
                outNBElement = 0;

                TALib.Core.Sma( pivot, startIndex, endIndex,  fastComas, out outBeginIdx, out outNBElement, 2 );
                IndicatorResult.AddSetValues( "FastComas", repoStartingIndex + outBeginIdx, outNBElement, true, fastComas );
                //Results.AddSetValues( "PivotPoint", repoStartingIndex + startIndex, endIndex + 1, true, pivot );

                outBeginIdx = 0;
                outNBElement = 0;
                TALib.Core.Sma( pivot, startIndex, endIndex, marketDirectionNo, out outBeginIdx, out outNBElement, 3 );
                IndicatorResult.AddSetValues( "MarketDirectionNo", repoStartingIndex + outBeginIdx, outNBElement, true, marketDirectionNo );

                outBeginIdx = 0;
                outNBElement = 0;

                TALib.Core.Sma( pivot, startIndex, endIndex, slowComas, out outBeginIdx, out outNBElement, 5 );

                IndicatorResult.AddSetValues( "SlowComas", repoStartingIndex + outBeginIdx, outNBElement, true, slowComas );
            }
        }

        protected void GetComasTurningPoint( )
        {
            double[] PivotPointSMA5;
            

            PivotPointSMA5 = GeneralHelper.EnumerableToArray( IndicatorResult [ "SlowComas" ] );


            if ( PivotPointSMA5.Length == 0 )
            {
                return;
            }

            bool[] sign = new bool[PivotPointSMA5.Length];
            double[] slope = new double[PivotPointSMA5.Length];

            if ( _indexOfLastComasTuringPoint < 1 )
            {
                return;
            }

            for ( int j = _indexOfLastComasTuringPoint; j < PivotPointSMA5.Length - 1; j++ )
            {
                slope [ j ] = ( PivotPointSMA5 [ j ] - PivotPointSMA5 [ j - 1 ] ) * 10000;
                sign [ j ] = ( PivotPointSMA5 [ j ] - PivotPointSMA5 [ j - 1 ] > 0 ) ? true : false;
            }

            bool waitSignificantCross = false;
            int currentComasSignificantPoint = 0;

            for ( int i = _indexOfLastComasTuringPoint; i < PivotPointSMA5.Length - 1; i++ )
            {
                if (
                        ( slope [ i - 1 ] > 0 && slope [ i ] < 0 ) ||
                        ( slope [ i - 1 ] < 0 && slope [ i ] > 0 )
                    )
                {
                    DateTime? barTime = Bars.GetTimeAtIndex(i);
                    currentComasSignificantPoint = i - 1;
                    waitSignificantCross = true;
                }

                if ( ( i >= currentComasSignificantPoint ) && ( waitSignificantCross == true ) )
                {
                    if ( ( PivotPointSMA5 [ i ] - PivotPointSMA5 [ currentComasSignificantPoint ] ) * 10000 > 5 )
                    {
                        _comasDictionary.GetOrAddValueRef( i ) = TASignal.ComasTurnUp;

                        _indexOfLastComasTuringPoint = currentComasSignificantPoint;
                        waitSignificantCross = false;
                    }
                    else if ( ( PivotPointSMA5 [ i ] - PivotPointSMA5 [ currentComasSignificantPoint ] ) * 10000 < -5 )
                    {
                        _comasDictionary.GetOrAddValueRef( i ) = TASignal.ComasTurnDown;

                        _indexOfLastComasTuringPoint = currentComasSignificantPoint;
                        waitSignificantCross = false;
                    }
                }
            }

            Bars.AddSignalsToDataBar( _comasDictionary );
        }

        protected void GetComasCrossover( )
        {
            double[] pivotPointDoubles;
            double[] PivotPointSMA5;
            


            pivotPointDoubles = GeneralHelper.EnumerableToArray( IndicatorResult [ "FastComas" ] );
            PivotPointSMA5 = GeneralHelper.EnumerableToArray( IndicatorResult [ "SlowComas" ] );


            if ( pivotPointDoubles.Length == 0 || PivotPointSMA5.Length == 0 )
            {
                return;
            }

            for ( int k = _indexOfLastComasTuringPoint; k < pivotPointDoubles.Length - 1; k++ )
            {
                if ( k == 0 )
                {
                    if ( pivotPointDoubles [ k ] == PivotPointSMA5 [ k ] )
                    {
                        if ( ( k + 1 ) <= pivotPointDoubles.Length )
                        {
                            if ( pivotPointDoubles [ k + 1 ] > PivotPointSMA5 [ k + 1 ] )
                            {
                                _comasDictionary.GetOrAddValueRef( k ) = TASignal.ComasCrossUp;
                            }
                            else
                            {
                                _comasDictionary.GetOrAddValueRef( k ) = TASignal.ComasCrossDown;
                            }

                            _currentComasSignificantPoint = k;
                        }

                    }
                }
                else
                {
                    if ( ( pivotPointDoubles [ k - 1 ] >= PivotPointSMA5 [ k - 1 ] && pivotPointDoubles [ k ] <= PivotPointSMA5 [ k ] ) )
                    {
                        _comasDictionary.GetOrAddValueRef( k ) = TASignal.ComasCrossDown;

                        _currentComasSignificantPoint = k;
                    }
                    else if ( pivotPointDoubles [ k - 1 ] <= PivotPointSMA5 [ k - 1 ] && pivotPointDoubles [ k ] >= PivotPointSMA5 [ k ] )
                    {
                        _comasDictionary.GetOrAddValueRef( k ) = TASignal.ComasCrossUp;

                        _currentComasSignificantPoint = k;
                    }
                }
            }

            Bars.AddSignalsToDataBar( _comasDictionary );

            //int startingIndex = Math.Max(1, _smoothedCosmaDictionary.Count > 0 ? _smoothedCosmaDictionary.Keys.Last() : 0);

            //if ( _smoothedCosmaDictionary.Count == 0 )
            //{
            //    _smoothedCosmaDictionary.TryAdd( _comasDictionary.Keys.First(), _comasDictionary.Values.First() );
            //}

            //for ( int i = startingIndex; i < _comasDictionary.Count; i++ )
            //{
            //    var currentItem = _comasDictionary.ElementAt(i);
            //    var previousItem = _comasDictionary.ElementAt(i - 1);

            //    if ( ( currentItem.Key - previousItem.Key ) >= 5 )
            //    {
            //        if ( _smoothedCosmaDictionary.Values.Last() == currentItem.Value )
            //        {
            //            if ( _smoothedCosmaDictionary.Count > 1 )
            //            {
            //                _smoothedCosmaDictionary.Remove( _smoothedCosmaDictionary.Keys.Last() );
            //            }
            //        }

            //        if ( _smoothedCosmaDictionary.ContainsKey( currentItem.Key ) )
            //        {
            //            _smoothedCosmaDictionary [ currentItem.Key ] = currentItem.Value;
            //        }
            //        else
            //        {
            //            _smoothedCosmaDictionary.TryAdd( currentItem.Key, currentItem.Value );
            //        }
            //    }
            //}

            //IndicatorBarsRepo.AddSignalsToDataBar( _smoothedCosmaDictionary );
        }
        #endregion

        

        #region SMA

        protected void GetSlowComasTurningPoint( bool fullRecalculation, DataBarUpdateType? updateType )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return;
            }

            double[] PivotPointSMA5;

            

            PivotPointSMA5 = GeneralHelper.EnumerableToArray( IndicatorResult [ "SlowComas" ] );


            if ( PivotPointSMA5.Length == 0 )
            {
                return;
            }

            bool[] sign = new bool[PivotPointSMA5.Length];
            double[] slope = new double[PivotPointSMA5.Length];

            if ( _indexOfLastComasTuringPoint < 1 )
            {
                return;
            }

            for ( int i = _indexOfLastComasTuringPoint; i < PivotPointSMA5.Length - 1; i++ )
            {
                slope [ i ] = ( PivotPointSMA5 [ i ] - PivotPointSMA5 [ i - 1 ] ) * 10000;
                sign [ i ] = ( PivotPointSMA5 [ i ] - PivotPointSMA5 [ i - 1 ] > 0 ) ? true : false;
            }

            bool waitSignificantCross = false;
            int currentComasSignificantPoint = 0;

            for ( int i = _indexOfLastComasTuringPoint; i < PivotPointSMA5.Length - 1; i++ )
            {
                if (
                        ( slope [ i - 1 ] > 0 && slope [ i ] < 0 ) ||
                        ( slope [ i - 1 ] < 0 && slope [ i ] > 0 )
                    )
                {
                    DateTime? barTime = Bars.GetTimeAtIndex(i);
                    currentComasSignificantPoint = i - 1;
                    waitSignificantCross = true;
                }

                if ( ( i >= currentComasSignificantPoint ) && ( waitSignificantCross == true ) )
                {
                    if ( ( PivotPointSMA5 [ i ] - PivotPointSMA5 [ currentComasSignificantPoint ] ) * 10000 > 5 )
                    {
                        _comasDictionary.GetOrAddValueRef( i ) = TASignal.ComasTurnUp;

                        _indexOfLastComasTuringPoint = currentComasSignificantPoint;
                        waitSignificantCross = false;
                    }
                    else if ( ( PivotPointSMA5 [ i ] - PivotPointSMA5 [ currentComasSignificantPoint ] ) * 10000 < -5 )
                    {
                        _comasDictionary.GetOrAddValueRef( i ) = TASignal.ComasTurnDown;

                        _indexOfLastComasTuringPoint = currentComasSignificantPoint;
                        waitSignificantCross = false;
                    }
                }
            }

            Bars.AddSignalsToDataBar( _comasDictionary );
        }

        

        //protected void GetSMACrossover( bool fullRecalculation, DataBarUpdateType? updateType )
        //{
        //    if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
        //    {
        //        return;
        //    }
        //double[ ] pivotPointDoubles;
        //double[ ] PivotPointSMA5;

        //double minMacd   = double.MaxValue;
        //int lastMin      = -1;
        //double maxMacd   = double.MinValue;
        //int lastMax      = -1;


        //pivotPointDoubles = GeneralHelper.EnumerableToArray( Results[ "FastComas" ] );
        //PivotPointSMA5 = GeneralHelper.EnumerableToArray( Results[ "SlowComas" ] );


        //if ( pivotPointDoubles.Length == 0 || PivotPointSMA5.Length == 0 )
        //{
        //    return;
        //}

        //for ( int k = _indexOfLastComasTuringPoint; k < pivotPointDoubles.Length - 1; k++ )
        //{
        //    if ( k == 0 )
        //    {
        //        if ( pivotPointDoubles[ k ] == PivotPointSMA5[ k ] )
        //        {
        //            if ( ( k + 1 ) <= pivotPointDoubles.Length )
        //            {
        //                if ( pivotPointDoubles[ k + 1 ] > PivotPointSMA5[ k + 1 ] )
        //                {
        //                    if ( _comasDictionary.ContainsKey( k ) )
        //                    {
        //                        _comasDictionary[ k ] = TASignal.ComasCrossUp;
        //                    }
        //                    else
        //                    {
        //                        _comasDictionary.Add( k, TASignal.ComasCrossUp );
        //                    }
        //                }
        //                else
        //                {
        //                    if ( _comasDictionary.ContainsKey( k ) )
        //                    {
        //                        _comasDictionary[ k ] = TASignal.ComasCrossDown;
        //                    }
        //                    else
        //                    {
        //                        _comasDictionary.Add( k, TASignal.ComasCrossDown );
        //                    }
        //                }

        //                _currentComasSignificantPoint = k;
        //            }

        //        }
        //    }
        //    else
        //    {
        //        if ( ( pivotPointDoubles[ k - 1 ] >= PivotPointSMA5[ k - 1 ] && pivotPointDoubles[ k ] <= PivotPointSMA5[ k ] ) )
        //        {
        //            if ( _comasDictionary.ContainsKey( k ) )
        //            {
        //                _comasDictionary[ k ] = TASignal.ComasCrossDown;
        //            }
        //            else
        //            {
        //                _comasDictionary.Add( k, TASignal.ComasCrossDown );
        //            }

        //            _currentComasSignificantPoint = k;
        //        }
        //        else if ( pivotPointDoubles[ k - 1 ] <= PivotPointSMA5[ k - 1 ] && pivotPointDoubles[ k ] >= PivotPointSMA5[ k ] )
        //        {
        //            if ( _comasDictionary.ContainsKey( k ) )
        //            {
        //                _comasDictionary[ k ] = TASignal.ComasCrossUp;
        //            }
        //            else
        //            {
        //                _comasDictionary.Add( k, TASignal.ComasCrossUp );
        //            }

        //            _currentComasSignificantPoint = k;
        //        }
        //    }
        //}

        //MutltiTimeFrameSessionDataRepo.AddSignalsToDataBar( _comasDictionary );

        //   int startingIndex = Math.Max( 1, _smoothedCosmaDictionary.Count > 0 ? _smoothedCosmaDictionary.Keys.Last() : 0 );

        //if ( _smoothedCosmaDictionary.Count == 0 )
        //{
        //       _smoothedCosmaDictionary.Add( _comasDictionary.Keys.First(), _comasDictionary.Values.First() );    
        //}

        //   for ( int i = startingIndex; i < _comasDictionary.Count; i++ )
        //   {
        //       var currentItem  = _comasDictionary.ElementAt( i );
        //       var previousItem = _comasDictionary.ElementAt( i - 1 );

        //       if ( ( currentItem.Key - previousItem.Key ) >= 5 )
        //       {
        //           if ( _smoothedCosmaDictionary.Values.Last( ) == currentItem.Value )
        //           {
        //               if ( _smoothedCosmaDictionary.Count > 1 )
        //               {
        //                   _smoothedCosmaDictionary.Remove( _smoothedCosmaDictionary.Keys.Last() );    
        //               }                        
        //           }

        //           if ( _smoothedCosmaDictionary.ContainsKey( currentItem.Key ) )
        //           {
        //               _smoothedCosmaDictionary[ currentItem.Key ] = currentItem.Value;
        //           }
        //           else
        //           {
        //               _smoothedCosmaDictionary.Add( currentItem.Key, currentItem.Value );
        //           }
        //       }
        //   }

        //   MutltiTimeFrameSessionDataRepo.AddSignalsToDataBar( _smoothedCosmaDictionary );
        //}
        #endregion


        


        public override void OnInitialDataBarUpdatePostCalculation( bool fullRecalculation )
        {
            if ( !_noCalculationAllowed )
            {
                string message = string.Format("Technical Analysis on {0} databars ....... DONE", FinancialHelper.GetPeriodString(Bars.Period.Value));

                Messenger.Default.Send( new WorkDoneMessage( message ) );

                _fxTradingEventsBindingList?.CheckIfAllIndicatorResultsReceived( Bars.Period.Value );

            }
        }



        public override void OnNewBarArrivesPostCalculation( bool fullRecalculation )
        {
            //_detectMacdCrossIndex = Math.Max( _indexOfLastMacdCross - 5, 0 );
            //GetMACDCrossOver( );
            //DetectMacdDivergence( );

            //PartialBBandsCrossOver( );
        }

        public override void OnHistoryBarUpdatePostCalculation( bool fullRecalculation )
        {
            
            //_indicatorSecurityCode = IndicatorBarsRepo.Security.Code;

            //_currentPeriod = IndicatorBarsRepo.Period.Value;

            //SaveNewlyCalculatedSignalToInfluxDBAsync( _databaseName, _currentIndicatorSymbol, _currentPeriod );
        }

        #region 00 MultiTasking Tasks

        


        protected Task TaskSMA( bool fullRecalculation, DataBarUpdateType? updateType, int curIterationBarcount )
        {
            if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
            {
                return null;
            }

            Task first = new Task(() => GetSMACrossover(fullRecalculation, updateType, curIterationBarcount), IndicatorExitToken);
            Task second = first.ContinueWith(
                                                antecedent =>
                                                {
                                                    if (antecedent.Status == TaskStatus.Faulted)
                                                    {
                                                        this.LogError("CalculateSMA caused Exception. Stack = " + antecedent.Exception.InnerException.StackTrace);
                                                    }
                                                    else
                                                    {
                                                        GetSlowComasTurningPoint(fullRecalculation, updateType);
                                                    }

                                                }, IndicatorExitToken);

            first.Start();

            return second;
        }
        

        //protected Task TaskDetectWaveRotation( bool fullRecalculation, DataBarUpdateType? updateType )
        //{
        //    if ( _noCalculationAllowed && ( updateType != DataBarUpdateType.Reloaded ) )
        //    {
        //        return null;
        //    }

        //    Task first = new Task(() => DetectWaveRotationFromZigZag(fullRecalculation, updateType), IndicatorExitToken);

        //    first.Start();

        //    return first;
        //}


        
        

        protected Task TaskCOMAS( bool fullRecalculation, DataBarUpdateType? updateType )
        {
            Task first = new Task(() => GetComasTurningPoint(), IndicatorExitToken);

            first.Start();

            return first;
        }




        #endregion



        /// <summary>
        ///  Tony Lam: Now that the detection of all the pattern and technical analysis is done, I want to search for the last few significant signal to display in the bindingList
        /// </summary>
        /// <param name="fullRecalculation"></param>
        /// <param name="updateType"></param>
        protected override void OnFinalCalculate( bool fullRecalculation, DataBarUpdateType? updateType )
        {
            if ( updateType == DataBarUpdateType.Initial || updateType == DataBarUpdateType.HistoryUpdate || updateType == DataBarUpdateType.NewPeriod )
            {
                _lastCandleCheckIndex = Bars.TotalBarCount - 2;

                var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

                if( aa == null )
                    return;

                var fxTradingEventsBindingList = aa.TradingEventsBindingList;

                string myUpdate = string.Empty;

                var message = string.Format("Done {0} databars Technical Analysis.", FinancialHelper.GetPeriodString(Bars.Period.Value));

                if ( updateType.HasValue )
                {
                    myUpdate = updateType.Value.ToDescription( );

                    message += " UpdateType = " + myUpdate;
                }

                _currentPeriod = Bars.Period.Value;

                if ( _msgBindingList != null )
                {
                    _msgBindingList.AddMessage( _currentPeriod, fxMsgType.DONE, message );
                }               
            }            
        }






        #region Helper Functions

        protected double FindExtremumBetweenPoints( int first, int second, bool minOrMax, ref int index )              // minOrMax, false = look for minimum,  true = look for maximum
        {
            //Debug.Assert( first >= 0 && second < DatabarsRepo.TotalBarCount, "index excess Total Databar Count !" );

            if ( first < 0 || second >= Bars.TotalBarCount )
            {
                return -1;
            }

            //Debug.Assert( second > first, "First must be less than second" );

            if ( second < first )
            {
                return -1;
            }


            double extremum = minOrMax ? double.MinValue : double.MaxValue;

            

            int count = second - first + 1;


            


            for ( int i = first; i <= second; i++ )
            {
                if ( minOrMax )
                {
                    if ( Bars[ i ].High > extremum )
                    {
                        extremum = Bars[ i ].High;
                        index = first + i;
                    }
                }
                else
                {
                    if ( Bars[ i ].Low < extremum )
                    {
                        extremum = Bars[ i ].Low;
                        index = first + i;
                    }
                }
            }

            return extremum;
        }


        #endregion

        /// <summary>
        /// Simple clone implementation will not clone results and signals, only parameters.
        /// </summary>
        /// <returns></returns>
        public override PlatformIndicator OnSimpleClone( )
        {
            FreemindIndicator result = new FreemindIndicator();

            result._description = _description;
            result._name = _name;

            return result;
        }

        private void RecalculateElliottWaveZigZag( )
        {
            //_performingElliottWaveReculation = true;

            //ResetNenZigZagVariables( );

            //nenZigZag( true , DataBarUpdateType.Initial , _extDepth , _extBackstep );

            //_performingElliottWaveReculation = false;
        }

        public void HaltCalculating( )
        {
            _noCalculationAllowed = true;
        }

        public void ResumeCalculating( )
        {
            _noCalculationAllowed = false;
        }

        private void PerformFullCalculation( )
        {
            HaltCalculating();

            ResetVariables();

            Messenger.Default.Send( new fxStationMessage( "Performing Full calculation on FreemindIndicator .....", string.Empty ) );

            var fullRecalculation = true;
            var updateType = DataBarUpdateType.Reloaded;

            var curIterationBarcount = Bars.TotalBarCount;

            this.LogInfo(_currentPeriod.ToReadable() + "Calculating Must Run Tasks");
            Step1_MustRunTasks(fullRecalculation, updateType, _currentPeriod, curIterationBarcount);

            if ( _hasNewEmaCross || _hasNewMacdCross || _hasNewOscillatorCross || _hasNewSmaCross )
            {
                this.LogInfo( _currentPeriod.ToReadable() + "Performing After Crossing Tasks" );
                Step2_RunAfterCrossingTasks( fullRecalculation, updateType, _currentPeriod, curIterationBarcount );

                this.LogInfo( _currentPeriod.ToReadable() + "Performing Advanced Tasks" );
                Step3_CalculateAdvancedTasks( fullRecalculation, updateType, _currentPeriod, curIterationBarcount );
            }
            
            

            //_msgBindingList?.AddMessage(_currentPeriod, fxMsgType.AdvancedTask, "Performing CandleStick Tasks");
            //OnDetectCandleTasks(fullRecalculation, updateType, _currentPeriod, curIterationBarcount);

            _msgBindingList?.AddMessage(_currentPeriod, fxMsgType.EWTask, "Indentifying Elliott Wave");
            OnIdentifyElliottWave(fullRecalculation, updateType, _currentPeriod, curIterationBarcount);

            _lastCandleCheckIndex = Bars.TotalBarCount - 2;


            Messenger.Default.Send( new fxStationMessage( "Technical Analysis Done.", string.Empty ) );

            RaiseFullCalculationDoneEvent();

            ResumeCalculating();

            _lastCandleCheckIndex = Bars.TotalBarCount - 2;
        }

        private void PerformFullCalculation( DoneReloadDatabars update )
        {
            HaltCalculating();

            ResetVariables();

            var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

            HewManager elliottWaveManager = null;

            if ( aa != null )
            {
                elliottWaveManager = (HewManager) aa.HewManager;
            }
            else
            {
                return;
            }            

            if ( elliottWaveManager != null )
            {
                elliottWaveManager.ResetWaveImportance();
            }

            Messenger.Default.Send( new fxStationMessage( "Peformaning Full calculation on FreemindIndicator .....", string.Empty ) );

            var fullRecalculation = true;

            var updateType = DataBarUpdateType.Reloaded;

            _barCountBeforeCalculation = Bars.TotalBarCount;

            var curIterationBarcount = Bars.TotalBarCount;

            _msgBindingList?.AddMessage(_currentPeriod, fxMsgType.BasicTask, "Calculating Basic Tasks");
            Step1_MustRunTasks(fullRecalculation, updateType, _currentPeriod, curIterationBarcount);

            _msgBindingList?.AddMessage(_currentPeriod, fxMsgType.AdvancedTask, "Performing Advanced Tasks");
            Step3_CalculateAdvancedTasks(fullRecalculation, updateType, _currentPeriod, curIterationBarcount);

            //_msgBindingList?.AddMessage(_currentPeriod, fxMsgType.AdvancedTask, "Performing CandleStick Tasks");
            //OnDetectCandleTasks(fullRecalculation, updateType, _currentPeriod, curIterationBarcount);

            _msgBindingList?.AddMessage(_currentPeriod, fxMsgType.EWTask, "Indentifying Elliott Wave");
            OnIdentifyElliottWave(fullRecalculation, updateType, _currentPeriod, curIterationBarcount);

            Messenger.Default.Send( new fxStationMessage( "Technical Analysis Done.", string.Empty ) );            

            RaiseFullCalculationDoneEvent();

            ResumeCalculating();

            _lastCandleCheckIndex = Bars.TotalBarCount - 2;
        }

        private int GetConsectiveNthBarBeforeTrendChange( TimeSpan period )
        {
            if ( period == TimeSpan.FromTicks( 1 ) )
            {
                _numberOfBarsRequired = 3;
            }
            else if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                _numberOfBarsRequired = 2;
            }
            else if ( period == TimeSpan.FromMinutes( 4 ) )
            {
                _numberOfBarsRequired = 1;
            }
            else if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                _numberOfBarsRequired = 1;
            }
            else if ( period == TimeSpan.FromMinutes( 15 ) )
            {
                _numberOfBarsRequired = 2;
            }
            else if ( period == TimeSpan.FromMinutes( 30 ) )
            {
                _numberOfBarsRequired = 2;
            }
            else if ( period == TimeSpan.FromHours( 4 ) )
            {
                _numberOfBarsRequired = 1;
            }
            else if ( period == TimeSpan.FromHours( 2 ) )
            {
                _numberOfBarsRequired = 2;
            }
            else if ( period == TimeSpan.FromHours( 1 ) )
            {
                _numberOfBarsRequired = 2;
            }
            else if ( period == TimeSpan.FromDays( 1 ) )
            {
                _numberOfBarsRequired = 1;
            }
            else if ( period == TimeSpan.FromDays( 7 ) )
            {
                _numberOfBarsRequired = 1;
            }
            else if ( period == TimeSpan.FromDays( 30 ) )
            {
                _numberOfBarsRequired = 1;
            }

            return _numberOfBarsRequired;
        }

        public new void RaiseFullCalculationDoneEvent( )
        {
            base.RaiseFullCalculationDoneEvent();
        }

        



        void SetGannPeak( int gannImportance, int index )
        {
            switch ( gannImportance )
            {
                case 0:
                    _gannVariables0._newLastHigherHighIndex = index;
                    _gannVariables0._newLastGannSwingExtremumIndex = _gannVariables0._lastHigherHighIndex;
                    _gannVariables0._newTrendDirection = TrendDirection.DownTrend;
                    break;

                case 1:
                    _gannVariables0._newLastHigherHighIndex = index;
                    _gannVariables0._newLastGannSwingExtremumIndex = _gannVariables0._lastHigherHighIndex;
                    _gannVariables0._newTrendDirection = TrendDirection.DownTrend;
                    _gannVariables1._newLastHigherHighIndex = index;
                    _gannVariables1._newLastGannSwingExtremumIndex = _gannVariables1._lastHigherHighIndex;
                    _gannVariables1._newTrendDirection = TrendDirection.DownTrend;
                    break;

                case 2:
                    _gannVariables0._newLastHigherHighIndex = index;
                    _gannVariables0._newLastGannSwingExtremumIndex = _gannVariables0._lastHigherHighIndex;
                    _gannVariables0._newTrendDirection = TrendDirection.DownTrend;
                    _gannVariables1._newLastHigherHighIndex = index;
                    _gannVariables1._newLastGannSwingExtremumIndex = _gannVariables1._lastHigherHighIndex;
                    _gannVariables1._newTrendDirection = TrendDirection.DownTrend;
                    _gannVariables2._newLastHigherHighIndex = index;
                    _gannVariables2._newLastGannSwingExtremumIndex = _gannVariables2._lastHigherHighIndex;
                    _gannVariables2._newTrendDirection = TrendDirection.DownTrend;
                    break;

                case 3:
                    _gannVariables0._newLastHigherHighIndex = index;
                    _gannVariables0._newLastGannSwingExtremumIndex = _gannVariables0._lastHigherHighIndex;
                    _gannVariables0._newTrendDirection = TrendDirection.DownTrend;
                    _gannVariables1._newLastHigherHighIndex = index;
                    _gannVariables1._newLastGannSwingExtremumIndex = _gannVariables1._lastHigherHighIndex;
                    _gannVariables1._newTrendDirection = TrendDirection.DownTrend;
                    _gannVariables2._newLastHigherHighIndex = index;
                    _gannVariables2._newLastGannSwingExtremumIndex = _gannVariables2._lastHigherHighIndex;
                    _gannVariables2._newTrendDirection = TrendDirection.DownTrend;
                    _gannVariables3._newLastHigherHighIndex = index;
                    _gannVariables3._newLastGannSwingExtremumIndex = _gannVariables3._lastHigherHighIndex;
                    _gannVariables3._newTrendDirection = TrendDirection.DownTrend;
                    break;

                case 4:
                    _gannVariables0._newLastHigherHighIndex = index;
                    _gannVariables0._newLastGannSwingExtremumIndex = _gannVariables0._lastHigherHighIndex;
                    _gannVariables0._newTrendDirection = TrendDirection.DownTrend;
                    _gannVariables1._newLastHigherHighIndex = index;
                    _gannVariables1._newLastGannSwingExtremumIndex = _gannVariables1._lastHigherHighIndex;
                    _gannVariables1._newTrendDirection = TrendDirection.DownTrend;
                    _gannVariables2._newLastHigherHighIndex = index;
                    _gannVariables2._newLastGannSwingExtremumIndex = _gannVariables2._lastHigherHighIndex;
                    _gannVariables2._newTrendDirection = TrendDirection.DownTrend;
                    _gannVariables3._newLastHigherHighIndex = index;
                    _gannVariables3._newLastGannSwingExtremumIndex = _gannVariables3._lastHigherHighIndex;
                    _gannVariables3._newTrendDirection = TrendDirection.DownTrend;
                    _gannVariables4._newLastHigherHighIndex = index;
                    _gannVariables4._newLastGannSwingExtremumIndex = _gannVariables4._lastHigherHighIndex;
                    _gannVariables4._newTrendDirection = TrendDirection.DownTrend;
                    break;

                case 5:
                    _gannVariables0._newLastHigherHighIndex = index;
                    _gannVariables0._newLastGannSwingExtremumIndex = _gannVariables0._lastHigherHighIndex;
                    _gannVariables0._newTrendDirection = TrendDirection.DownTrend;
                    _gannVariables1._newLastHigherHighIndex = index;
                    _gannVariables1._newLastGannSwingExtremumIndex = _gannVariables1._lastHigherHighIndex;
                    _gannVariables1._newTrendDirection = TrendDirection.DownTrend;
                    _gannVariables2._newLastHigherHighIndex = index;
                    _gannVariables2._newLastGannSwingExtremumIndex = _gannVariables2._lastHigherHighIndex;
                    _gannVariables2._newTrendDirection = TrendDirection.DownTrend;
                    _gannVariables3._newLastHigherHighIndex = index;
                    _gannVariables3._newLastGannSwingExtremumIndex = _gannVariables3._lastHigherHighIndex;
                    _gannVariables3._newTrendDirection = TrendDirection.DownTrend;
                    _gannVariables4._newLastHigherHighIndex = index;
                    _gannVariables4._newLastGannSwingExtremumIndex = _gannVariables4._lastHigherHighIndex;
                    _gannVariables4._newTrendDirection = TrendDirection.DownTrend;
                    _gannVariables5._newLastHigherHighIndex = index;
                    _gannVariables5._newLastGannSwingExtremumIndex = _gannVariables5._lastHigherHighIndex;
                    _gannVariables5._newTrendDirection = TrendDirection.DownTrend;
                    break;

            }
            //_newLastHigherHighIndex = index;
            //_newLastGannSwingExtremumIndex = _lastHigherHighIndex;
            //_newTrendDirection = TrendDirection.DownTrend;
        }

        private void SetGannTrough( int gannImportance, int index )
        {
            switch ( gannImportance )
            {
                case 0:
                    _gannVariables0._newLastLowerLowIndex = index;
                    _gannVariables0._newLastGannSwingExtremumIndex = _gannVariables0._lastLowerLowIndex;
                    _gannVariables0._newTrendDirection = TrendDirection.Uptrend;
                    break;

                case 1:
                    _gannVariables0._newLastLowerLowIndex = index;
                    _gannVariables0._newLastGannSwingExtremumIndex = _gannVariables0._lastLowerLowIndex;
                    _gannVariables0._newTrendDirection = TrendDirection.Uptrend;
                    _gannVariables1._newLastLowerLowIndex = index;
                    _gannVariables1._newLastGannSwingExtremumIndex = _gannVariables1._lastLowerLowIndex;
                    _gannVariables1._newTrendDirection = TrendDirection.Uptrend;
                    break;

                case 2:
                    _gannVariables0._newLastLowerLowIndex = index;
                    _gannVariables0._newLastGannSwingExtremumIndex = _gannVariables0._lastLowerLowIndex;
                    _gannVariables0._newTrendDirection = TrendDirection.Uptrend;
                    _gannVariables1._newLastLowerLowIndex = index;
                    _gannVariables1._newLastGannSwingExtremumIndex = _gannVariables1._lastLowerLowIndex;
                    _gannVariables1._newTrendDirection = TrendDirection.Uptrend;
                    _gannVariables2._newLastLowerLowIndex = index;
                    _gannVariables2._newLastGannSwingExtremumIndex = _gannVariables2._lastLowerLowIndex;
                    _gannVariables2._newTrendDirection = TrendDirection.Uptrend;
                    break;

                case 3:
                    _gannVariables0._newLastLowerLowIndex = index;
                    _gannVariables0._newLastGannSwingExtremumIndex = _gannVariables0._lastLowerLowIndex;
                    _gannVariables0._newTrendDirection = TrendDirection.Uptrend;
                    _gannVariables1._newLastLowerLowIndex = index;
                    _gannVariables1._newLastGannSwingExtremumIndex = _gannVariables1._lastLowerLowIndex;
                    _gannVariables1._newTrendDirection = TrendDirection.Uptrend;
                    _gannVariables2._newLastLowerLowIndex = index;
                    _gannVariables2._newLastGannSwingExtremumIndex = _gannVariables2._lastLowerLowIndex;
                    _gannVariables2._newTrendDirection = TrendDirection.Uptrend;
                    _gannVariables3._newLastLowerLowIndex = index;
                    _gannVariables3._newLastGannSwingExtremumIndex = _gannVariables3._lastLowerLowIndex;
                    _gannVariables3._newTrendDirection = TrendDirection.Uptrend;
                    break;

                case 4:
                    _gannVariables0._newLastLowerLowIndex = index;
                    _gannVariables0._newLastGannSwingExtremumIndex = _gannVariables0._lastLowerLowIndex;
                    _gannVariables0._newTrendDirection = TrendDirection.Uptrend;
                    _gannVariables1._newLastLowerLowIndex = index;
                    _gannVariables1._newLastGannSwingExtremumIndex = _gannVariables1._lastLowerLowIndex;
                    _gannVariables1._newTrendDirection = TrendDirection.Uptrend;
                    _gannVariables2._newLastLowerLowIndex = index;
                    _gannVariables2._newLastGannSwingExtremumIndex = _gannVariables2._lastLowerLowIndex;
                    _gannVariables2._newTrendDirection = TrendDirection.Uptrend;
                    _gannVariables3._newLastLowerLowIndex = index;
                    _gannVariables3._newLastGannSwingExtremumIndex = _gannVariables3._lastLowerLowIndex;
                    _gannVariables3._newTrendDirection = TrendDirection.Uptrend;
                    _gannVariables4._newLastLowerLowIndex = index;
                    _gannVariables4._newLastGannSwingExtremumIndex = _gannVariables4._lastLowerLowIndex;
                    _gannVariables4._newTrendDirection = TrendDirection.Uptrend;
                    break;

                case 5:
                    _gannVariables0._newLastLowerLowIndex = index;
                    _gannVariables0._newLastGannSwingExtremumIndex = _gannVariables0._lastLowerLowIndex;
                    _gannVariables0._newTrendDirection = TrendDirection.Uptrend;
                    _gannVariables1._newLastLowerLowIndex = index;
                    _gannVariables1._newLastGannSwingExtremumIndex = _gannVariables1._lastLowerLowIndex;
                    _gannVariables1._newTrendDirection = TrendDirection.Uptrend;
                    _gannVariables2._newLastLowerLowIndex = index;
                    _gannVariables2._newLastGannSwingExtremumIndex = _gannVariables2._lastLowerLowIndex;
                    _gannVariables2._newTrendDirection = TrendDirection.Uptrend;
                    _gannVariables3._newLastLowerLowIndex = index;
                    _gannVariables3._newLastGannSwingExtremumIndex = _gannVariables3._lastLowerLowIndex;
                    _gannVariables3._newTrendDirection = TrendDirection.Uptrend;
                    _gannVariables4._newLastLowerLowIndex = index;
                    _gannVariables4._newLastGannSwingExtremumIndex = _gannVariables4._lastLowerLowIndex;
                    _gannVariables4._newTrendDirection = TrendDirection.Uptrend;
                    _gannVariables5._newLastLowerLowIndex = index;
                    _gannVariables5._newLastGannSwingExtremumIndex = _gannVariables5._lastLowerLowIndex;
                    _gannVariables5._newTrendDirection = TrendDirection.Uptrend;
                    break;

            }
            //_newLastLowerLowIndex = index;
            //_newLastGannSwingExtremumIndex = _lastLowerLowIndex;
            //_newTrendDirection = TrendDirection.Uptrend;
        }

        

        //public async void SaveNewlyCalculatedSignalToInfluxDBAsync( string databaseName, string instrument, TimeSpan period )
        //{
        //    var influxDbClient = InfluxDBHelper.GetInfluxDbConnection();

        //    DateTime startDate = DateTime.MinValue;

        //    if ( influxDbClient != null )
        //    {
        //        var periodStr = FinancialHelper.GetPeriodId(period);

        //        var symbolStr = FinancialHelper.GetSymbolEnum(instrument).ToString();

        //        var tableName = symbolStr + periodStr + "TA";

        //        var query = string.Format("select last( MACD ) from {0}", tableName);

        //        var response = await influxDbClient.Client.QueryAsync(query, databaseName);

        //        var enumerator = response.GetEnumerator();

        //        if ( enumerator.MoveNext() )
        //        {
        //            var current = enumerator.Current;

        //            if ( current.Values.Count > 0 )
        //            {
        //                foreach ( var values in current.Values )
        //                {
        //                    if ( values.Count > 0 )
        //                    {
        //                        if ( values [ 0 ].IsDate() )
        //                        {
        //                            startDate = ( DateTime ) values [ 0 ] - period;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            startDate = DatabarsRepo.FirstBarTime.Value;
        //        }


        //        if ( DatabarsRepo.LastBarTime >= startDate )
        //        {

        //            PooledList<InfluxData.Net.InfluxDb.Models.Point> barsInfo = DatabarsRepo.PrepareBarDatabaseInfo(tableName, startDate);

        //            if ( barsInfo.Count > 5000 )
        //            {
        //                var batchWriter = influxDbClient.Serie.CreateBatchWriter(databaseName);

        //                batchWriter.SetMaxBatchSize( 10000 );

        //                batchWriter.OnError += BatchWriter_OnError;

        //                batchWriter.AddPoints( barsInfo );

        //                batchWriter.Start( 5000 );
        //            }
        //            else
        //            {
        //                var response2 = await influxDbClient.Client.WriteAsync(barsInfo, databaseName);
        //            }


        //        }
        //    }
        //}

        private void BatchWriter_OnError( object sender, Exception e )
        {
            //throw new NotImplementedException( );
        }

        private void Writer_OnError( object sender, Exception e )
        {
            throw new NotImplementedException();
        }
        public bool VerifyIndicatorsCalculationResult( )
        {
            var bbMean       = IndicatorResult["BBMean"].Count;

            var innerBBUpper = IndicatorResult["InnerBBUpper"].Count;

            var InnerBBLower = IndicatorResult["InnerBBLower"].Count;

            var OuterBBUpper = IndicatorResult["OuterBBUpper"].Count;

            var OuterBBLower = IndicatorResult["OuterBBLower"].Count;

            var MACD         = IndicatorResult["MACD"].Count;

            var MACDSignal   = IndicatorResult["MACDSignal"].Count;

            var K            = IndicatorResult["K"].Count;

            var D            = IndicatorResult["D"].Count;

            var SMA          = IndicatorResult["SMA"].Count;

            var ZigZagS0     = IndicatorResult["ZigZagS0"].Count;

            var ZigZagS0High = IndicatorResult["ZigZagS0High"].Count;

            var ZigZagS0Low  = IndicatorResult["ZigZagS0Low"].Count;


            if ( ( bbMean == innerBBUpper ) && ( innerBBUpper == InnerBBLower ) && ( InnerBBLower == OuterBBUpper ) && ( OuterBBUpper == OuterBBLower ) && ( OuterBBLower == MACD ) && ( MACD == MACDSignal ) && ( MACDSignal == K ) && ( K == D ) && ( D == SMA ) && ( SMA == ZigZagS0 ) && ( ZigZagS0High == ZigZagS0 ) && ( ZigZagS0High == ZigZagS0Low ) )
            {
                return true;
            }


            return false;
        }

        // Tony: Here I want to calculate the standard deviation         
        public override void OnDataBarsReloadedPreCalculation( bool fullRecalculation )
        {
            _doneFetchingFromDatabase = false;                        

            _instrumentPointSize = _indicatorSecurity.PriceStep.HasValue ? ( double )_indicatorSecurity.PriceStep.Value : 1;
            _instrumentDigits = _indicatorSecurity.Decimals.HasValue ? _indicatorSecurity.Decimals.Value : 5;

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _indicatorSecurity );

            if( aa == null )
                return;
            
            aa.InitializeWaveImportanceCalculationStatus( );
            
            _currentPeriod = Bars.Period.Value;


            _msgBindingList = aa.GetTradingEventsMsgBindingList( _indicatorSecurity );            

            ResetVariables( );

            _noCalculationAllowed = false;
            _doneFetchingFromDatabase = true;

            InitializeIndicatorParameters( );
        }

        public override void OnDataBarsReloadedPostCalculation( bool fullRecalculation )
        {
            if ( _doneFetchingFromDatabase )
            {
                string message = string.Format("Technical Analysis on {0} databars ....... DONE", FinancialHelper.GetPeriodString(Bars.Period.Value));

                Messenger.Default.Send( new WorkDoneMessage( message ) );

                _fxTradingEventsBindingList?.CheckIfAllIndicatorResultsReceived( Bars.Period.Value );

            }
        }

    }
}