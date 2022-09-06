using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Docking.Base;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using fx.Definitions;
using fx.Common;
using fx.Definitions.UndoRedo;
using fx.Algorithm;
using Ecng.Collections;
using StockSharp.Logging;
using fx.Indicators;
using StockSharp.Localization;
using fx.Charting;
using System.Windows.Media;
using StockSharp.Xaml;
using StockSharp.Studio.Core.Configuration;
using Ecng.Common;
using StockSharp.Studio.Core.Services;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Candles;
using Freemind.Strategies;
using Ecng.Configuration;
using Disruptor;
using StockSharp.Messages;

namespace FreemindAITrade.ViewModels
{
    public partial class TradeStationViewModel
    {
        #region POCO properties for the VIEW        

        public virtual ChartPanelOrderSettings OrderSettings
        {
            get; set;
        } = new ChartPanelOrderSettings( );

        public virtual CandleDrawStyleImage SelectedCandleStyle
        {
            get;
            set;
        } = CandleDrawStyleImages[ 1 ];

        public virtual WaveCycleImage SelectedWaveCycle
        {
            get;
            set;
        } = WaveCycleImages[ 14 ];

        protected void OnSelectedWaveCycleChanging( WaveCycleImage impt )
        {
            foreach ( var vm in _allVisibleViewModels )
            {
                vm.ChartVM.ShowWaveCycle( impt.WaveCycle );
            }

            _selectedViewModel.Refresh();
        }

        public virtual WaveImptImage SelectedWaveImpt
        {
            get;
            set;
        } = WaveImptImages[ 1 ];

        protected void OnSelectedWaveImptChanging( WaveImptImage impt )
        {
            foreach ( var vm in _allVisibleViewModels )
            {
                vm.ChartVM.ShowWaveImportance( impt.WaveImportance );
            }

            _selectedViewModel.Refresh();
        }

        /* -------------------------------------------------------------------------------------------------------------------------------------------
         * 
         *  When user select a different Candle Visual Styple, the following method will be called.
         *  
         *  DevExpress POCO MVVM =  On + PropertyName + Changing
         * 
         * ------------------------------------------------------------------------------------------------------------------------------------------- */
        protected void OnSelectedCandleStyleChanging( CandleDrawStyleImage drawStyel )
        {

        }

        

        public virtual double MonthlyOpacityValue { get; set; } = 50;
        protected void OnMonthlyOpacityValueChanging( double newOpacity )
        {
            Messenger.Default.Send( new PivotsPointOpacityMessage( _security, newOpacity / 100, TimeSpan.FromDays( 30 ) ) );
        }

        public virtual double WeeklyOpacityValue { get; set; } = 50;
        protected void OnWeeklyOpacityValueChanging( double newOpacity )
        {
            Messenger.Default.Send( new PivotsPointOpacityMessage( _security, newOpacity / 100, TimeSpan.FromDays( 7 ) ) );
        }

        public virtual double DailyOpacityValue { get; set; } = 50;
        protected void OnDailyOpacityValueChanging( double newOpacity )
        {
            Messenger.Default.Send( new PivotsPointOpacityMessage( _security, newOpacity / 100, TimeSpan.FromDays( 1 ) ) );
        }

        public virtual int DataBarIndex { get; set; }



        public virtual bool ShowCandlePattern { get; set; }
        protected void OnShowCandlePatternChanging( bool newBool )
        {
            foreach ( var vm in _allVisibleViewModels )
            {
                if ( vm.ChartVM != null )
                    vm.ChartVM.ShowCandlePattern( newBool );
            }

            _selectedViewModel.Refresh( );
        }
        

        public virtual bool ShowTradingTime { get; set; }
        protected void OnShowTradingTimeChanging( bool newBool )
        {
            foreach (var vm in _intradayViewModels )
            {
                if ( vm.ChartVM != null )
                    vm.ChartVM.ShowTradingTime( newBool );
            }

            _selectedViewModel.Refresh( );
        }

        public virtual bool ShowFreemindIndicators { get; set; }
        protected void OnShowFreemindIndicatorsChanging( bool newBool )
        {
            if ( newBool )
            {
                if ( _selectedViewModel.ChartVM != null )
                {
                    _selectedViewModel.Step01_ExecuteAddIndicatorArea( );
                    
                }
            }
            else
            {
                if ( _selectedViewModel.ChartVM != null )
                {
                    _selectedViewModel.ChartVM.RemoveIndicatorArea();
                }
            }

            foreach (var vm in _allVisibleViewModels )
            {
                if ( vm.ChartVM != null )
                    vm.ChartVM.ShowFreemindIndicators( newBool );
            }

            _selectedViewModel.Refresh( );
        }

        public virtual bool YAxisAutoRange { get; set; } = true;
        protected void OnYAxisAutoRangeChanging( bool newBool )
        {
            foreach (var vm in _allVisibleViewModels )
            {
                if ( vm.ChartVM != null )
                    vm.ChartVM.YAxisAutoRange( newBool );
            }

            _selectedViewModel.Refresh( );
        }



        public virtual bool ShowElliottWave { get; set; }
        protected void OnShowElliottWaveChanging( bool newBool )
        {
            foreach (var vm in _allVisibleViewModels )
            {
                if ( vm.ChartVM != null )
                    vm.ChartVM.ShowElliottWave( newBool );
            }

            _selectedViewModel.Refresh( );
        }

        public virtual bool StepByStepChecked { get; set; }
        protected void OnStepByStepCheckedChanging( bool newBool )
        {
            foreach ( var vm in _allVisibleViewModels )
            {
                vm.StepByStep =  newBool;
            }

            _selectedViewModel.Refresh();
        }

        public virtual bool ShowMonoWave { get; set; }
        protected void OnShowMonoWaveChanging( bool newBool )
        {
            foreach (var vm in _allVisibleViewModels )
            {
                if ( vm.ChartVM != null )
                    vm.ChartVM.ShowMonoWave( newBool );
            }

            _selectedViewModel.Refresh( );
        }



        public virtual bool ShowHewDetection { get; set; }
        protected void OnShowHewDetectionChanging( bool newBool )
        {
            foreach ( var vm in _allVisibleViewModels )
            {
                if ( vm.ChartVM != null )
                    vm.ChartVM.ShowHewDetection( newBool );
            }

            _selectedViewModel.Refresh( );
        }

        public virtual bool ShowDivergence { get; set; }
        protected void OnShowDivergenceChanging( bool newBool )
        {
            foreach ( var vm in _allVisibleViewModels )
            {
                if ( vm.ChartVM != null )
                    vm.ChartVM.ShowDivergence( newBool );
            }

            _selectedViewModel.Refresh( );
        }

        public virtual bool ShowSmallTradingEvent { get; set; }
        protected void OnShowSmallTradingEventChanging( bool newBool )
        {
            foreach ( var vm in _allVisibleViewModels )
            {
                if ( vm.ChartVM != null )
                    vm.ChartVM.ShowSmallTradingEvent( newBool );
            }

            _selectedViewModel.Refresh( );
        }




        public virtual bool ShowGannPriceTime { get; set; }
        protected void OnShowGannPriceTimeChanging( bool newBool )
        {
            foreach ( var vm in _allVisibleViewModels )
            {
                if ( vm.ChartVM != null )
                    vm.ChartVM.ShowGannPriceTime( newBool );
            }

            _selectedViewModel.Refresh( );
        }

        public virtual object SelectedPanel { get; set; }

        public void LayoutItemSelection( DockItemActivatedEventArgs args )
        {
            if ( args.Item == null ) return;

            SelectedPanel = args.Item.DataContext;

            if ( SelectedPanel is IChartTabViewModel liveTradeView )
            {
                _selectedViewModel = liveTradeView;

                if ( _selectedViewModel != null )
                {
                    _selectedViewModel.StartChartDrawingTimerThread( );

                    var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

                    HewManager elliottWaveManager = null;

                    if ( aa != null )
                    {
                        elliottWaveManager = ( HewManager ) aa.HewManager;
                    }
                    else
                    {
                        return;
                    }

                    elliottWaveManager.SwitchTimeFrame( this, _selectedViewModel.ResponsibleTF );

                    _selectedUndoRedoArea = elliottWaveManager.GetSelectedUndoRedoArea( _selectedViewModel.ResponsibleTF );

                    _selectedViewModel.OnChartTabSelected( );
                }
            }
        }
        #endregion

        public override void Load( SettingsStorage storage )
        {
            ShowElliottWave        = storage.GetValue<bool>( nameof( ShowElliottWave        ) );
            ShowCandlePattern      = storage.GetValue<bool>( nameof( ShowCandlePattern      ) );
            ShowTradingTime        = storage.GetValue<bool>( nameof( ShowTradingTime        ) );
            ShowFreemindIndicators = storage.GetValue<bool>( nameof( ShowFreemindIndicators ) );
            YAxisAutoRange         = storage.GetValue<bool>( nameof( YAxisAutoRange         ) );
            ShowMonoWave           = storage.GetValue<bool>( nameof( ShowMonoWave           ) );
            ShowHewDetection       = storage.GetValue<bool>( nameof( ShowHewDetection       ) );
            ShowDivergence         = storage.GetValue<bool>( nameof( ShowDivergence         ) );
            ShowSmallTradingEvent  = storage.GetValue<bool>( nameof( ShowSmallTradingEvent  ) );
            ShowGannPriceTime      = storage.GetValue<bool>( nameof( ShowGannPriceTime      ) );

            var waveCycle = storage.GetValue<ElliottWaveCycle>( nameof( SelectedWaveCycle ) );

            var index = WaveCycleImages.FindIndex( x => x.WaveCycle == waveCycle );

            if ( index > -1 )
            {
                SelectedWaveCycle = WaveCycleImages[ index ];
            }

            var waveImpt = storage.GetValue<int>( nameof( SelectedWaveImpt ) );

            var index2 = WaveImptImages.FindIndex( x => x.WaveImportance == waveImpt );

            if ( index2 > -1 )
            {
                SelectedWaveImpt = WaveImptImages[ index2 ];
            }
        }

        public override void Save( SettingsStorage storage )
        {
            if ( storage is null )
                throw new ArgumentNullException( nameof( storage ) );

            storage.SetValue( nameof( ShowElliottWave        ), ShowElliottWave        );
            storage.SetValue( nameof( ShowCandlePattern      ), ShowCandlePattern      );
            storage.SetValue( nameof( ShowTradingTime        ), ShowTradingTime        );
            storage.SetValue( nameof( ShowFreemindIndicators ), ShowFreemindIndicators );
            storage.SetValue( nameof( YAxisAutoRange         ), YAxisAutoRange         );
            storage.SetValue( nameof( ShowMonoWave           ), ShowMonoWave           );
            storage.SetValue( nameof( ShowHewDetection       ), ShowHewDetection       );
            storage.SetValue( nameof( ShowDivergence         ), ShowDivergence         );
            storage.SetValue( nameof( ShowSmallTradingEvent  ), ShowSmallTradingEvent  );
            storage.SetValue( nameof( ShowGannPriceTime      ), ShowGannPriceTime      );
            storage.SetValue( nameof( SelectedWaveCycle      ), SelectedWaveCycle.WaveCycle );
            storage.SetValue( nameof( SelectedWaveImpt       ), SelectedWaveImpt.WaveImportance );
        }

        public void SaveSettings( )
        {
            StudioUserConfig userConfig = UserConfig;                        

            UserConfig.SetDelayValue( "TradeStationChartsSettings", ( ) => GuiDispatcher.GlobalDispatcher.AddSyncAction( ( ) => this.Save( ) ) );
        }

        private void TonyOnRefreshEvent( )
        {
            foreach ( var vm in _allVisibleViewModels )
            {
                vm.SetQuickOrderPanel( _security );
            }
            
            UpdateTimeframe( );
        }


        private bool _isUpdatingTfSelecto;

        private void UpdateTimeframe( )
        {
            if ( _isUpdatingTfSelecto )
                return;
            _isUpdatingTfSelecto = true;
            try
            {
                if ( _selectedViewModel  != null )
                {
                    CandleSeries rebuiltSeries = _selectedViewModel.ChartVM.CandleSeriesRebuilt;

                    if ( rebuiltSeries != null )
                    {
                        object period = rebuiltSeries.Arg;
                        if ( period is TimeSpan )
                        {
                            TimeSpan timeSpan = (TimeSpan) period;
                            if ( _timeFrames.Contains( timeSpan ) )
                            {
                                SelectedTimeFrame = timeSpan;
                                return;
                            }

                            SelectedTimeFrame = _timeFrames[ 0 ];
                            return;
                        }
                    }

                    SelectedTimeFrame = _timeFrames[ 0 ];
                }                
            }
            finally
            {
                _isUpdatingTfSelecto = false;
            }
        }

        public virtual TimeSpan SelectedTimeFrame { get; set; } = TimeSpan.FromMinutes( 5 );

        protected void OnSelectedTimeFrameChanging( TimeSpan timeSpan )
        {
            if ( _isUpdatingTfSelecto )
                return;
            _isUpdatingTfSelecto = true;
            try
            {                
                if ( !( timeSpan > TimeSpan.Zero ) )
                    return;

                _selectedViewModel.SwitchToTimeFrameX( timeSpan );

                var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security );

                HewManager elliottWaveManager = null;

                if ( aa != null )
                {
                    elliottWaveManager = ( HewManager ) aa.HewManager;
                }
                else
                {
                    return;
                }

                elliottWaveManager.SwitchTimeFrame( this, timeSpan );

                _selectedUndoRedoArea = elliottWaveManager.GetSelectedUndoRedoArea( timeSpan );                

                _selectedViewModel.ChartVM.LoadCandlesOfXTimeFrame( timeSpan );
            }
            finally
            {
                _isUpdatingTfSelecto = false;
            }
        }

        private FreemindStrategy _fxStrategy;

        public void StartStrategy()
        {
            this.AddInfoLog( "FreemindStartgy Started" );

            bool setupOkay = false;

            if ( OrderSettings.Portfolio == null )
            {
                var PortfolioSource = ConfigManager.GetService< PortfolioDataSource >( );

                setupOkay = ShowPortfolioDialog( PortfolioSource );
                
                //if ( wnd.ShowModal( this ) )
                //    OrderSettings.Portfolio = wnd.SelectedPortfolio;
            }

            if ( setupOkay )
            {
                _fxStrategy = new FreemindStrategy( ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _security ) )
                {
                    Security = _security,
                    Connector = _liveTradingConnector,
                    Portfolio = _selectedPortfolio
                };
                
                _fxStrategy.Start( );
            }

            
        }

        public void StopStrategy()
        {
            if ( _fxStrategy != null )
            {
                _fxStrategy.Stop( );
            }

        }

        public const int BUFFER_SIZE = 1024 * 64;

        public void StartSimulation()
        {
            _backTestingConnector.Connect();
            _backTestingConnector.Start();

            StartRingBuffer( );

            _backTestingConnector.CandleSeriesProcessing += RingBufferCandleSeriesProcessing;

            _selectedViewModel.Refresh();
        }

        public bool CanStartSimulation()
        {
            return _symbolSelected;
        }

        

        public void PauseSimulation()
        {
            if ( SimulationState == ChannelStates.Suspended )
            {                
                SimulationState = ChannelStates.Starting;
                _backTestingConnector.Start( );
                _candlesDisruptor.Resume( );
            }
            else
            {
                SimulationState = ChannelStates.Suspended;
                _backTestingConnector.Suspend();
                _candlesDisruptor.Halt( );
            }            
        }        

        public void StepByStep()
        {

        }

        public bool CanStepByStep()
        {
            return _symbolSelected;
        }

        public bool CanPauseSimulation()
        {
            return _symbolSelected;
        }

        public void StopSimulation()
        {
            //_backTestingConnector.Stop();
        }

        public bool CanStopSimulation()
        {
            return _symbolSelected;
        }

        private ChannelStates _state = ChannelStates.Stopped;

        /// <inheritdoc />
        public ChannelStates SimulationState
        {
            get => _state;
            private set
            {
                if ( _state == value )
                    return;

                _state = value;                
            }
        }
    }
}

