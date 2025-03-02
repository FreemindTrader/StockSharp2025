using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Docking;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Configuration;
using Ecng.Serialization;
using Ecng.Xaml;
using fx.Algorithm;
using fx.Bars;
using fx.Charting;
using fx.Collections;
using fx.Common;
using fx.Definitions;
using fx.Indicators;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Candles.Compression;
using StockSharp.Algo.Indicators;
using StockSharp.Algo.Storages;
using StockSharp.Algo.Testing;
using StockSharp.BusinessEntities;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Studio.Core.Configuration;
using StockSharp.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;

#pragma warning disable CS0618

namespace FreemindAITrade.ViewModels
{
    public partial class BackTesterViewModel : ChartTabViewModelBase
    {
        public BackTesterViewModel( IMutltiTimeFrameSessionDataRepo dataRepo, fxHistoricEmulationConnector connector, CandleManager candleManager, string caption, string imagePath, TimeSpan reponsible, Security sec, Portfolio portfolio, DateTime startDate, DateTime endDate, CancellationTokenSource exitSource )
        {
            Caption = caption + "Test";
            Glyph = GlyphHelper.GetSvgImage( imagePath );
            _waveScenarioNumber = 1;

            ResponsibleTF = reponsible;
            SelectedSecurity = sec;
            SelectedPortfolio = portfolio;

            OrderSettings = new ChartPanelOrderSettings();
            OrderSettings.Security = SelectedSecurity;
            OrderSettings.Portfolio = SelectedPortfolio;

            _connector = connector;
            _candleManager = candleManager;

            _subscriptionManager = new SubscriptionControlManager( _connector );

            _bars = SymbolsMgr.Instance.CreateOrGetDatabarRepo( SelectedSecurity, ResponsibleTF );

            _bars.CreateDataBarCacheFriendlyStorage( SelectedSecurity, ResponsibleTF, startDate, endDate );


            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( SelectedSecurity.Code );

            if ( aa != null )
            {
                _hews = ( HewManager )aa.HewManager;
            }


            if ( aa != null )
            {
                _freemindIndicator = ( FreemindIndicator )aa.GetFreemindIndicator( ResponsibleTF );
                _freemindIndicator.IsNonVisual = false;
                _freemindIndicator.IndicatorExitTokenSource = exitSource;
                _freemindIndicator.AttachDatasource( _bars );

                _bars.Indicators.AddIndicator( _freemindIndicator );

                if ( reponsible >= TimeSpan.FromDays( 1 ) )
                {
                    _pivotPointIndicator = ( PivotPointsCustom )aa.GetPivotPoint( ResponsibleTF );
                    _pivotPointIndicator.AttachDatasource( _bars );

                    _pivotPointIndicator.PivotTimeSpan = reponsible;
                    _pivotPointIndicator.Security = SelectedSecurity;

                    _bars.Indicators.AddIndicator( _pivotPointIndicator );
                }
            }

            aa.PivotPointChangedEvent += AaMgr_PivotPointChangedEvent;

            //_bars.DailyPivotsUpdateEvent   += DataBarHistory_DailyPivotsUpdateEvent;
            //_bars.WeeklyPivotsUpdateEvent  += DataBarHistory_WeeklyPivotsUpdateEvent;
            //_bars.MonthlyPivotsUpdateEvent += DataBarHistory_MonthlyPivotsUpdateEvent;

            var timerName = "BackTesterViewModel";

            _drawTimer = new ResettableTimer( TimeSpan.FromSeconds( 0.5 ), timerName );
            _drawTimer.Elapsed += new Action<Func<bool>>( TonyChartPaneBackgroundWorkTimer );

            StartDate = startDate;
            EndDate = endDate;
        }

        private void AaMgr_PivotPointChangedEvent( object sender, PPChangedEventArgs e )
        {

        }

        private void DataBarHistory_DailyPivotsUpdateEvent( fxHistoricBarsRepo provider, IList<SRlevel> newLevels )
        {
            _chartVM.ClearDailyPivots();

            if ( newLevels.Count > 0 )
            {
                _chartVM.AddDailyPivots( SelectedSecurity, newLevels, ref _bars.NullBar );

                //if ( !_disableAllDrawing )
                //{
                //    WinFormsHelper.BeginManagedInvoke( this, new GeneralHelper.GenericDelegate<bool, bool>( _mainChartControl.MasterPane.TonyBestFitDrawingSpaceToScreen ), true, true );
                //}
            }
        }

        private void DataBarHistory_WeeklyPivotsUpdateEvent( fxHistoricBarsRepo provider, IList<SRlevel> newLevels )
        {
            _chartVM.ClearWeeklyPivots();

            if ( newLevels.Count > 0 )
            {
                _chartVM.AddWeeklyPivots( SelectedSecurity, newLevels, ref _bars.NullBar );

                //if ( !_disableAllDrawing )
                //{
                //    WinFormsHelper.BeginManagedInvoke( this, new GeneralHelper.GenericDelegate<bool, bool>( _mainChartControl.MasterPane.TonyBestFitDrawingSpaceToScreen ), true, true );
                //}
            }
        }

        private void DataBarHistory_MonthlyPivotsUpdateEvent( fxHistoricBarsRepo provider, IList<SRlevel> newLevels )
        {
            _chartVM.ClearMonthlyPivots();

            if ( newLevels.Count > 0 )
            {
                _chartVM.AddMonthlyPivots( SelectedSecurity, newLevels, ref _bars.NullBar );

                //if ( !_disableAllDrawing )
                //{
                //    WinFormsHelper.BeginManagedInvoke( this, new GeneralHelper.GenericDelegate<bool, bool>( _mainChartControl.MasterPane.TonyBestFitDrawingSpaceToScreen ), true, true );
                //}

            }
        }

        protected override void InitializeChart()
        {
            RegisterCommandsAndEvents();

            if ( IsActive )
            {
                ChartVM.IsActive = IsActive;
            }

            ChartVM.SelectedSecurity = SelectedSecurity;
            ChartVM.CrossHair = true;
            ChartVM.CrossHairTooltip = true;
            ChartVM.MinimumRange = 80;
            ChartVM.IsInteracted = true;
            ChartVM.IsProgrammable = true;

            FillIndicators();

            InitializeConnector();
        }

        public void RegisterCommandsAndEvents()
        {
            ChartVM.ClearAreas();
            ChartVM.ScichartSurfaceViewModels.CollectionChanged += Step03_ChartOrIndicatorPanesAdded;
            ChartVM.SubscribeCandleElement += Step4_SubscribeCandleUiEventHandler;
            ChartVM.SubscribeIndicatorElement += ManuallyAddIndicators;

            _commandService = ConfigManager.GetService<IStudioCommandService>();
            #region COMMAND 
            _commandService.Register<ChartAddElementExCommand>( this, false, Step5_OnChartAddUIsToChart );
            _commandService.Register<SubscribeCommand>( this, false, Step6_OnSubscribeToSymbol, null );

            #endregion ------------------------------------------------------COMMAND --------------------------------------------------------



            //ChartVM.ScichartSurfaceViewModels.CollectionChanged += Step2_ChartPaneAdded;

            //ChartVM.CreateOrder += TonyOnCreateOrder;
            //ChartVM.MoveOrder += TonyOnMoveOrder;
            //ChartVM.CancelOrder += TonyOnCancelOrder;
            //ChartVM.AnnotationCreated += TonyOnAnnotationCreated;
            //ChartVM.AnnotationModified += TonyOnAnnotationModified;
            //ChartVM.AnnotationDeleted += TonyOnAnnotationDeleted;
            //ChartVM.SubscribeCandleElement += Step4_SubscribeCandleUiEventHandler;
            //ChartVM.SubscribeIndicatorElement += TonyOnSubscribeIndicatorElement;
            //ChartVM.SubscribeOrderElement += TonyOnSubscribeOrderElement;
            //ChartVM.SubscribeTradeElement += TonyOnSubscribeTradeElement;
            //ChartVM.UnSubscribeElement += TonyOnUnSubscribeElement;



            //ChartVM.RegisterOrder += TonyOnRegisterOrder;
            //ChartVM.SettingsChanged += TonyOnSettingsChanged;
            //ChartVM.ChartAreas.Added += new Action<ChartArea>(TonyOnAreasAdded);
            //ChartVM.ChartAreas.Removed += new Action<ChartArea>(TonyOnAreasRemoved);
            //ChartVM.ChartAreas.Cleared += new Action(TonyOnAreaCleared);
        }

        private void InitializeConnector()
        {
            if ( SelectedPortfolio != null )
            {
                _storageRegistry = ServicesRegistry.StorageRegistry;
            }
            else
            {
                if ( ParentViewModel != null )
                {
                    ParentViewModel.SplashScreenService.HideSplashScreen();

                    var PortfolioSource = ConfigManager.GetService<PortfolioDataSource>();

                    var setupOkay = ShowPortfolioDialog( PortfolioSource );

                    if ( setupOkay == false )
                        return;
                }
            }

            SetupCandSeries();

            ParentViewModel.LinkSeriesWithVM( _candlesSeries, this );

            _connector.SecurityReceived += Step7_connector_NewSecurity; ;
            _connector.MarketDepthReceived += _connector_MarketDepthReceived;
        }

        private void _connector_MarketDepthReceived( Subscription arg1, MarketDepth arg2 )
        {
            throw new NotImplementedException();
        }

        private void Step7_connector_NewSecurity( Subscription arg1, Security security )
        {
            _connector.SubscribeTrades( security );
        }



        void SetupCandSeries()
        {
            _candlesSeries = new CandleSeries( SeriesSetting.CandleType, _security, ResponsibleTF )
            {
                BuildCandlesMode = MarketDataBuildModes.Load,
                BuildCandlesFrom2 = DataType.Transactions,
                From = StartDate,
                To = EndDate
            };
        }

        [Command]
        public void StartStrategy()
        {
            bool setupOkay = false;

            if ( OrderSettings.Portfolio == null )
            {
                var PortfolioSource = ConfigManager.GetService<PortfolioDataSource>();

                setupOkay = ShowPortfolioDialog( PortfolioSource );
            }
        }


        /* -------------------------------------------------------------------------------------------------------------------------------------------
        * 
        *  After BackTester Control has been loaded, the following will be called automatically
        * 
        * ------------------------------------------------------------------------------------------------------------------------------------------- */
        public override void Step01_ExecuteAddChartArea()
        {
            if ( ChartVM.CanAddArea() )
            {
                ChartVM.Step01_AddChartArea();
            }
        }

        private void Step03_ChartOrIndicatorPanesAdded( object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e )
        {
            if ( e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add )
            {
                if ( e.NewStartingIndex == 0 )
                {
                    Step04_ProgrammaticallyAddCandlesToSurface();
                }
            }
        }

        private void Step04_ProgrammaticallyAddCandlesToSurface()
        {
            var surfaceVM = ChartVM.ScichartSurfaceViewModels;

            if ( surfaceVM.Count > 0 )
            {
                var pane = surfaceVM.First();

                if ( pane != null )
                {
                    if ( ChartVM.CanExecuteAddCandlesProgramatically() )
                    {
                        ChartVM.Step05_ExecuteAddCandlesProgramatically( pane.Area, _candlesSeries, ChartHelper.GetFifoCapcity( ResponsibleTF ) );
                    }
                }
            }
        }

        private void Step4_SubscribeCandleUiEventHandler( CandlestickUI element, CandleSeries series )
        {
            var command = new ChartAddElementExCommand( element, series );

            if ( _loadingSettings )
            {
                Step5_OnChartAddUIsToChart( command );
            }
            else
            {
                command.Process( this, false );
            }

            if ( !CanTrackChanges() )
                return;

            if ( OrderSettings.Security == null )
                OrderSettings.Security = series.Security;

            RaiseChangedCommand();
        }

        private void ManuallyAddIndicators( IndicatorUI element, CandleSeries series, IIndicator indicator )
        {
            var command = new ChartAddElementExCommand( element, series, indicator );
            command.Process( this, false );
        }

        /* -------------------------------------------------------------------------------------------------------------------------------------------
           *  Since we are working with Emulation, I do want the candles to come in one by one so that I can test them sequentially.
           *  
           *  Step A ----------> 6 StudioCommanService Route the command to our OnChartAddElementCommand Handler which will request for Market Data. 
           * 
           * ------------------------------------------------------------------------------------------------------------------------------------------- 
           */
        private void Step5_OnChartAddUIsToChart( ChartAddElementExCommand command )
        {
            var candleSeries = ( CandleSeries )command.Source;

            if ( _candlesSeries != candleSeries )
            {
                return;
            }

            _candleUI = command.Element as CandlestickUI;

            if ( _candleUI != null )
            {
                ChartVM.ShowElliottWave( false );
                ChartVM.ShowFreemindIndicators( false );
                ChartVM.ShowDivergence( false );
                ChartVM.ShowCandlePattern( false );
                ChartVM.ShowGannPriceTime( false );
                ChartVM.IsSimulation( true );

                _chartVM.WaveScenarioNo( _waveScenarioNumber );

                CandleSeries myCandles = ( CandleSeries )command.Source;
                Subscription subscription = new Subscription( myCandles );

                var candleSeriesData = new CandleSeriesData( _candleUI, _bars, subscription, DateTimeOffset.MinValue );

                if ( !_candles.TryAdd( myCandles, candleSeriesData ) )
                {
                    return;
                }

                new SubscribeCommand( subscription ).Process( this, false );
            }
            else
            {
                var indicatorUI = command.Element as IndicatorUI;

                if ( _indicators.ContainsKey( indicatorUI ) )
                {
                    return;
                }

                CandleSeries myCandles = ( CandleSeries )command.Source;
                CandleSeriesData candleSeriesData;

                if ( !_candles.TryGetValue( myCandles, out candleSeriesData ) )
                {
                    return;
                }


                var indicatorPair = new IndicatorPair( this, indicatorUI, command.Indicator, _drawSeries );
                _indicators.Add( indicatorUI, indicatorPair );

                var first = _indicatorsBySeries.SafeAdd( myCandles, key => Array.Empty<Tuple<IndicatorUI, IndicatorPair>>() );
                _indicatorsBySeries[myCandles] = first.Concat( new Tuple<IndicatorUI, IndicatorPair>[1] { Tuple.Create( indicatorUI, indicatorPair ) } ).ToArray();

                var data = new ChartDrawDataEx();

                var indicator = indicatorPair.MyIndicator;
                var count = candleSeriesData.BarsRepo.MainDataBars.Count;
                var indicatorList = data.SetIndicatorSource( indicatorUI, count );

                for ( int i = 0; i < count; i++ )
                {
                    ref SBar bar = ref candleSeriesData.BarsRepo[i];

                    var indicatorRes = indicator.Process( ref bar );

                    indicatorList.SetIndicatorValue( bar.BarTime, indicatorRes );
                }

                _chartVM.Draw( data );
            }
            //else
            //{
            //    var indicatorUI = command.Element as IndicatorUI;

            //    if ( indicatorUI == null || _indicators.ContainsKey( indicatorUI ) )
            //        return;

            //    CandleSeries myCandles = ( CandleSeries ) command.Source;
            //    CandleSeriesData candleSeriesData;

            //    if ( !_candles.TryGetValue( myCandles, out candleSeriesData ) )
            //    {
            //        return;
            //    }

            //    //CandleSeries myCandles = (CandleSeries) command.Source;
            //    //RefQuadruple<CandlestickUI, SortedDictionary<DateTimeOffset, Candle>, Subscription, DateTimeOffset> refQuadruple;

            //    //if ( !_candles.TryGetValue( myCandles, out refQuadruple ) )
            //    //    return;

            //    IndicatorPair indicatorPair = new IndicatorPair(this, indicatorUI, command.Indicator, myCandles);
            //    _indicators.Add( indicatorUI, indicatorPair );
            //    Tuple<IndicatorUI, IndicatorPair>[] first = _indicatorsBySeries.SafeAdd(myCandles,  key => ArrayHelper.Empty<Tuple<IndicatorUI, IndicatorPair>>());
            //    _indicatorsBySeries[ myCandles ] = ( ( IEnumerable<Tuple<IndicatorUI, IndicatorPair>> ) first.Concat( new Tuple<IndicatorUI, IndicatorPair>[ 1 ] { Tuple.Create( indicatorUI, indicatorPair ) } ) ).ToArray( );
            //    ChartDrawDataEx data = new ChartDrawDataEx();

            //    foreach ( KeyValuePair<DateTimeOffset, Candle> keyValuePair in candleSeriesData.Second )
            //    {
            //        Candle candle = keyValuePair.Value;
            //        data.Group( candle.OpenTime ).Add( indicatorUI, indicatorPair.MyIndicator.Process( candle ) );
            //    }
            //    _chartVM.Draw( data );
            //}
        }

        private void Step6_OnSubscribeToSymbol( object sender, SubscribeCommand cmd )
        {
            if ( _candlesSeries != cmd.Subscription.CandleSeries )
            {
                return;
            }

            _subscriptionManager.Subscribe( sender, cmd.Subscription );

            _subscriptionManager.Subscribe( sender, new Subscription( _candlesSeries ) );


            _candleManager.Start( _candlesSeries );

            RaiseDoneDownloadBarsEvent();
        }





        public override void StartSimulation()
        {
            _connector.Connect();
            _connector.Start();
        }






        protected void RaiseChangedCommand()
        {
            //new ControlChangedCommand( this ).Process( this, false );
        }

        private bool CanTrackChanges()
        {
            return _chartVM.IsInteracted && !_loadingSettings;
        }







        private DateTimeOffset? LoadCandles( IMarketDataStorage<CandleMessage> storage, CandleSeries series, TimeSpan daysLoad )
        {
            var taskBegin = series.From.Value.DateTime;
            var taskEnd = series.To.HasValue ? series.To.Value.DateTime : DateTime.UtcNow;
            var period = ( TimeSpan )series.Arg;

            var range = fxCandleHelper.GetRange( storage, series.From, series.To, daysLoad );

            if ( range == null )
            {
                return null;
            }

            if ( ResponsibleTF == TimeSpan.FromSeconds( 1 ) )
            {
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
            * 
            *  Mistakes 2           ----------> Need to be super careful about manipulating DateTimeOffset and DateTime. The following
            *                       Correct            DateTimeOffset begin = range.Item1.UtcDateTime.Date;       
            *                       Wrong              var  begin = range.Item1.Date ( this produce a DateTime which is Unspecified Time )
            * ------------------------------------------------------------------------------------------------------------------------------------------- 
            */
            DateTimeOffset begin = range.Item1.UtcDateTime.Date;
            DateTimeOffset end = range.Item2.UtcDateTime.Date.EndOfDay();

            var lastEntry = storage.GetToTime();

            var messages = storage.Load( begin, end );

            _drawCandles = messages.ToCandles<Candle>( series.Security );

            if ( _drawCandles != null )
            {
                var last = _drawCandles.FirstOrDefault();

                if ( last != null )
                {
                    return last.OpenTime;
                }
            }

            return null;
        }

        /* -------------------------------------------------------------------------------------------------------------------------------------------
        * 
        *  Mistakes 1----------> 1  Finally found out why when loading data, the software is taking up so much time CPU time
        *                           If we don't set AllowBuildFromSmallerTimeFrame to false, we will be using GetCandleMessageBuildableStorage
        * 
        * ------------------------------------------------------------------------------------------------------------------------------------------- 
        */
        private IMarketDataStorage<CandleMessage> GetTimeFrameCandleMessageStorage( Security security, TimeSpan timeFrame, bool allowBuildFromSmallerTimeFrame )
        {
            if ( ResponsibleTF == TimeSpan.FromSeconds( 1 ) )
            {
            }

            if ( !allowBuildFromSmallerTimeFrame )
            {
                return _storageRegistry.GetCandleMessageStorage( typeof( TimeFrameCandleMessage ), security.ToSecurityId(), timeFrame, Drive, Format );
            }


            var candleBuilderProvider = ConfigManager.TryGetService<CandleBuilderProvider>() ?? new CandleBuilderProvider( ServicesRegistry.EnsureGetExchangeInfoProvider() );

            var smallerTFSource = StorageHelper.GetCandleMessageBuildableStorage( candleBuilderProvider, _storageRegistry, security.ToSecurityId(), timeFrame, Drive, Format );

            //if ( CacheBuildableCandles )
            //{
            //    var cacheStorage = _storageRegistry.GetCandleMessageStorage( typeof( TimeFrameCandleMessage ), security.ToSecurityId(), timeFrame, Drive, Format );
            //    smallerTFSource  = new CacheableMarketDataStorage<CandleMessage>( smallerTFSource, cacheStorage );
            //}

            return smallerTFSource;
        }



        public bool CanStartStrategy()
        {
            return true;
        }

        public bool ShowPortfolioDialog( PortfolioDataSource portfolioDataSource )
        {
            var service = GetService<IDialogService>( "SelectPortfolioService" );


            // Tony: https://www.devexpress.com/Support/Center/Question/Details/T315317/how-to-pass-param-to-dialog-in-wpf-mvvm
            // The dialog that's shown has its own DataContext bind to SymbolSelectViewModel and I am creating a ViewModel here.
            // So there are two view model and the second view model is empty without 


            var portfolioVM = PortfolioPickerWindowViewModel.Create();

            portfolioVM.PortfolioDataSource = portfolioDataSource;

            UICommand registerCommand = new UICommand()
            {
                Caption = "Okay",
                IsCancel = false,
                IsDefault = true,
                Command = new DevExpress.Mvvm.DelegateCommand<CancelEventArgs>( x => myExecuteMethod(), x => portfolioVM.SelectedPortfolio != null ),
            };

            UICommand cancelCommand = new UICommand()
            {
                Id = MessageBoxResult.Cancel,
                Caption = "Cancel",
                IsCancel = true,
                IsDefault = false,
            };

            UICommand result = service.ShowDialog(
                                                        dialogCommands: new List<UICommand>() { registerCommand, cancelCommand },
                                                        title: "Select Portfolio",
                                                        documentType: "PortfolioPickerView",
                                                        viewModel: portfolioVM,
                                                        parameter: null,
                                                        parentViewModel: this
                                                      );

            if ( result == cancelCommand || result == null )
            {
                return false;
            }

            UserConfig.SetDelayValue( "PortfolioPickerView", () => GuiDispatcher.GlobalDispatcher.AddSyncAction( () => portfolioVM.Save() ) );

            SelectedPortfolio = portfolioVM.SelectedPortfolio;

            return true;
        }

        private void myExecuteMethod()
        {
            //Debug.Print( "Registration complete" );
        }

        private static StudioUserConfig UserConfig
        {
            get
            {
                return BaseUserConfig<StudioUserConfig>.Instance;
            }
        }

        private ChartPanelOrderSettings _orderSettings = null;

        public ChartPanelOrderSettings OrderSettings
        {
            get
            {
                return _orderSettings;
            }

            set { SetValue( ref _orderSettings, value ); }
        }







        private void TonyChartPaneBackgroundWorkTimer( Func<bool> canProcess )
        {
            try
            {
                if ( IsActive )
                {
                    if ( _drawCandles != null )
                    {
                        DrawCandles( _drawSeries, _drawCandles );
                    }
                }

            }
            catch ( Exception ex )
            {
                ex.LogError( null );
            }
            finally
            {
            }
        }

        private DateTimeOffset? DrawCandles( CandleSeries series, IEnumerable<Candle> allCandles )
        {
            if ( _doneDrawing )
            {
                return null;
            }

            CandleSeriesData candleSeriesData;

            if ( !_candles.TryGetValue( series, out candleSeriesData ) )
            {
                return null;
            }

            if ( !_isDrawingCandles )
            {
                _isDrawingCandles = true;
            }

            var drawData = new ChartDrawDataEx();

            int offerId = SymbolsMgr.Instance.GetOfferId( SelectedSecurity );

            PooledList<Candle> holder = new PooledList<Candle>();

            foreach ( Candle candle in _drawCandles )
            {
                holder.Add( candle );

                DateTimeOffset openTime = candle.OpenTime;

                if ( openTime < candleSeriesData.LastBarTime )
                {
                    continue;
                }

                candleSeriesData.LastBarTime = openTime;
            }

            var candleUI = candleSeriesData.CandleUI;


            Tuple<IndicatorUI, IndicatorPair>[ ] indicatorTuple;

            var ssIndicators = _indicatorsBySeries.TryGetValue( series, out indicatorTuple );

            //var candles = candleQuadruple.Second.Values.ToList( );

            foreach ( var candle in allCandles )
            {
                var lastBarTime = candle.OpenTime;

                var drawDataItem = drawData.Group( lastBarTime ).Add( candleUI, candle );

                if ( ssIndicators )
                {
                    foreach ( var indicatorValue in indicatorTuple )
                    {
                        var indicatorUI = indicatorValue.Item1;
                        var indicatorRes = indicatorValue.Item2.MyIndicator.Process( candle );

                        drawDataItem.Add( indicatorUI, indicatorRes );
                    }
                }
            }

            _chartVM.Draw( drawData );

            _doneDrawing = true;

            return _lastBarTime;
        }
    }
}
