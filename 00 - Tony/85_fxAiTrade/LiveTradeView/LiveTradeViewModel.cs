using DevExpress.Mvvm;
using DevExpress.Xpf.Docking;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Serialization;
using fx.Algorithm;
using fx.Bars;
using fx.Charting;
using fx.Collections;
using fx.Common;
using fx.Database;
using fx.Database.Common.DataModel;
using fx.Database.ForexDatabarsDataModel;
using fx.Definitions;
using fx.Indicators;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Studio.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;

namespace FreemindAITrade.ViewModels
{
    public class CandleSeriesData
    {
        public CandleSeriesData( CandlestickUI candleUI, fxHistoricBarsRepo bars, Subscription subs, DateTimeOffset barTime )
        {
            CandleUI = candleUI;
            BarsRepo = bars;
            Subscription = subs;
            LastBarTime = barTime;
        }

        public CandlestickUI CandleUI { get; set; }

        public fxHistoricBarsRepo BarsRepo { get; set; }

        public Subscription Subscription { get; set; }

        public DateTimeOffset LastBarTime { get; set; }
    }

    /// <summary>
    /// I set ================> DataContext="VM:LiveTradeViewModel" in the View which cause two viewModels to be created.
    /// One I setup in the TradeStationViewModel with 
    /// _1secVm    = new LiveTradeViewModel( "1 Sec"  , "SvgImages/Business Objects/BO_Scheduler.svg", TimeSpan.FromSeconds( 1 ),             security );
    /// 
    /// The other one is from the View Itself, which doesn't take parameters. Guess the Later one will take precedence over the formerly created one, so 
    /// the XAML don't see the AddAreaCommand and SetupChartCommand
    /// </summary>
    public partial class LiveTradeViewModel : ChartTabViewModelBase
    {
        protected IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(); } }

        private readonly PooledDictionary<CandleSeries, CandleSeriesData> _candles = new PooledDictionary<CandleSeries, CandleSeriesData>();
        private readonly PooledDictionary<IndicatorUI, IndicatorPair> _indicators = new PooledDictionary<IndicatorUI, IndicatorPair>();

        private readonly PooledDictionary<CandleSeries, Tuple<IndicatorUI, IndicatorPair>[ ]> _indicatorsBySeries = new PooledDictionary<CandleSeries, Tuple<IndicatorUI, IndicatorPair>[ ]>();

        private readonly PooledDictionary<Order, ChartActiveOrderInfo> _chartOrders = new PooledDictionary<Order, ChartActiveOrderInfo>();
        private readonly PooledDictionary<AnnotationUI, ChartDrawDataEx.sAnnotation> _annotations = new PooledDictionary<AnnotationUI, ChartDrawDataEx.sAnnotation>();

        private CandleSeries _candlesSeries;
        private CandleSeries _drawSeries;

        private bool _isBarIntegrityCheck = false;

        private IStudioCommandService _commandService;
        private bool _loading = false;

        private readonly IStorageRegistry _storageRegistry;
        private IMarketDataDrive _drive;
        private bool _initializing;




        private IRepository<DbElliottWave, long> _dbElliottWaveRepo;
        private IUnitOfWorkFactory<IForexDatabarsUnitOfWork> _unitOfWorkFactory = null;

        private IForexDatabarsUnitOfWork _unitOfWork;

        private Connector _connector;

        private bool _isNonVisual = false;

        private bool _loadAllBars = false;



        private CancellationTokenSource _exitSource;

        public CancellationTokenSource LiveTradeExitTokenSource
        {
            get
            {
                return _exitSource;
            }

            set
            {
                _exitSource = value;
            }
        }

        public CancellationToken LiveTradeExitToken
        {
            get
            {
                return _exitSource.Token;
            }
        }


        public LiveTradeViewModel( IMutltiTimeFrameSessionDataRepo dataRepo, string caption, string imagePath, TimeSpan reponsible, Security sec, Connector connector, bool isBarIntegrityCheck, int waveScenarioCount, bool isNonVisual, bool loadAll, CancellationTokenSource exitSource )
        {
            _isNonVisual         = isNonVisual;
            _waveScenarioNumber  = waveScenarioCount;
            _unitOfWorkFactory   = UnitOfWorkSource.GetUnitOfWorkFactory();
            _unitOfWork          = _unitOfWorkFactory.CreateUnitOfWork();
            _dbElliottWaveRepo   = _unitOfWork.ELLIOTTWAVES;
            _isBarIntegrityCheck = isBarIntegrityCheck;
            _exitSource          = exitSource;

            Caption              = caption;
            Glyph                = GlyphHelper.GetSvgImage( imagePath );
            Text                 = String.Format( "Document text ({0})", caption );
            ResponsibleTF        = reponsible;
            SelectedSecurity     = sec;
            _loadAllBars         = loadAll;

            if ( !_isNonVisual )
            {
                var timerName = "ChartTabVM_" + reponsible.ToReadable();
                _drawTimer = new ResettableTimer( TimeSpan.FromSeconds( 0.5 ), timerName );
                _drawTimer.Elapsed += new Action<Func<bool>>( TonyChartPaneBackgroundWorkTimer );

            }

            _connector                      = connector;

            _storageRegistry                = ServicesRegistry.StorageRegistry;

            AddAreaCommand                  = new DelegateCommand( Step01_ExecuteAddChartArea, CanAddArea );

            QuickOrderSettingChangedCommand = new DelegateCommand( ExecuteQuickOrderSettingChanged );

            _name                           = GetType().GetDisplayName();

            _bars                           = SymbolsMgr.Instance.CreateOrGetDatabarRepo( SelectedSecurity, ResponsibleTF );
            _bars.HistoricBarUpdateEvent += _bars_HistoricBarUpdateEvent;

            var aaMgr = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( SelectedSecurity.Code );

            if ( aaMgr != null )
            {
                _hews = ( HewManager )aaMgr.HewManager;
            }

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( SelectedSecurity );

            if ( aa != null )
            {
                _freemindIndicator = ( FreemindIndicator )aa.GetFreemindIndicator( ResponsibleTF );
                _freemindIndicator.IsNonVisual = isNonVisual;
                _freemindIndicator.IndicatorExitTokenSource = exitSource;

                if ( _waveScenarioNumber == 1 )
                {
                    _bars.AddIndicator( _freemindIndicator, true );
                }


                if ( reponsible >= TimeSpan.FromDays( 1 ) )
                {
                    _pivotPointIndicator = ( PivotPointsCustom )aa.GetPivotPoint( ResponsibleTF );

                    _pivotPointIndicator.PivotTimeSpan = reponsible;
                    _pivotPointIndicator.Security = SelectedSecurity;

                    if ( _waveScenarioNumber == 1 ) _bars.Indicators.AddIndicator( _pivotPointIndicator );
                }
            }

            _bars.DailyPivotsUpdateEvent += new SupportResistanceLevelsUpdateDelege( DataBarHistory_DailyPivotsUpdateEvent );
            _bars.WeeklyPivotsUpdateEvent += new SupportResistanceLevelsUpdateDelege( DataBarHistory_WeeklyPivotsUpdateEvent );
            _bars.MonthlyPivotsUpdateEvent += new SupportResistanceLevelsUpdateDelege( DataBarHistory_MonthlyPivotsUpdateEvent );



            OrderSettings = new ChartPanelOrderSettings();
            OrderSettings.PropertyChanged += ( ( s, e ) => SettingsChanged?.Invoke() );            
        }

        private void _bars_HistoricBarUpdateEvent( object sender, HistoricBarsUpdateEventArg e )
        {
            if ( !_candles.TryGetValue( _candlesSeries, out CandleSeriesData series ) )
            {
                return;
            }

            InternalDrawCandles( _drawSeries, series.CandleUI, _bars.MainDataBars, (e.BeginIndex, e.EndIndex) );
        }






        //bool IStudioCommandScope.UseParentScope
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}

        //bool IStudioCommandScope.RouteToGlobalScope
        //{
        //    get
        //    {
        //        return false;
        //    }
        //}








        public void RegisterCommandsAndEvents()
        {
            _commandService = ConfigManager.GetService<IStudioCommandService>();

            #region COMMAND             
            _commandService.Register<SelectCommand>( this, true, OnSelectCommand );
            _commandService.Register<OrderCommand>( this, true, OnOrderCommand );
            _commandService.Register<OrderFailCommand>( this, true, OnOrderFailCommand );
            _commandService.Register<ChartAddElementExCommand>( this, false, Step5_OnChartAddUIsToChart );
            _commandService.Register<ChartRemoveElementExCommand>( this, false, OnChartRemoveElementCommand );
            _commandService.Register<ChartResetElementExCommand>( this, false, OnChartResetElementCommand );
            #endregion ------------------------------------------------------COMMAND --------------------------------------------------------

            ChartVM.ScichartSurfaceViewModels.CollectionChanged += Step03_ChartOrIndicatorPanesAdded;

            ChartVM.CreateOrder += TonyOnCreateOrder;
            ChartVM.MoveOrder += TonyOnMoveOrder;
            ChartVM.CancelOrder += TonyOnCancelOrder;
            ChartVM.AnnotationCreated += TonyOnAnnotationCreated;
            ChartVM.AnnotationModified += TonyOnAnnotationModified;
            ChartVM.AnnotationDeleted += TonyOnAnnotationDeleted;
            ChartVM.SubscribeCandleElement += Step4_SubscribeCandleUiEventHandler;
            ChartVM.SubscribeIndicatorElement += Step08_SubscribeIndicatorUIEventHandler;
            ChartVM.SubscribeOrderElement += TonyOnSubscribeOrderElement;
            ChartVM.SubscribeTradeElement += TonyOnSubscribeTradeElement;
            ChartVM.UnSubscribeElement += TonyOnUnSubscribeElement;



            ChartVM.RegisterOrder += TonyOnRegisterOrder;
            ChartVM.SettingsChanged += TonyOnSettingsChanged;
            ChartVM.ChartAreas.Added += new Action<ChartArea>( TonyOnAreasAdded );
            ChartVM.ChartAreas.Removed += new Action<ChartArea>( TonyOnAreasRemoved );
            ChartVM.ChartAreas.Cleared += new Action( TonyOnAreaCleared );
        }


        public override void UnRegisterCommandsAndEvents()
        {
            _commandService = ConfigManager.GetService<IStudioCommandService>();
            #region COMMAND 
            _commandService.UnRegister<CandleCommand>( this );
            _commandService.UnRegister<SelectCommand>( this );
            _commandService.UnRegister<OrderCommand>( this );
            _commandService.UnRegister<OrderFailCommand>( this );
            _commandService.UnRegister<ChartAddElementExCommand>( this );
            _commandService.UnRegister<ChartRemoveElementExCommand>( this );
            _commandService.UnRegister<ChartResetElementExCommand>( this );
            #endregion ------------------------------------------------------COMMAND --------------------------------------------------------

            ChartVM.ScichartSurfaceViewModels.CollectionChanged -= Step03_ChartOrIndicatorPanesAdded;

            ChartVM.CreateOrder -= TonyOnCreateOrder;
            ChartVM.MoveOrder -= TonyOnMoveOrder;
            ChartVM.CancelOrder -= TonyOnCancelOrder;
            ChartVM.AnnotationCreated -= TonyOnAnnotationCreated;
            ChartVM.AnnotationModified -= TonyOnAnnotationModified;
            ChartVM.AnnotationDeleted -= TonyOnAnnotationDeleted;
            ChartVM.SubscribeCandleElement -= Step4_SubscribeCandleUiEventHandler;
            ChartVM.SubscribeIndicatorElement -= Step08_SubscribeIndicatorUIEventHandler;
            ChartVM.SubscribeOrderElement -= TonyOnSubscribeOrderElement;
            ChartVM.SubscribeTradeElement -= TonyOnSubscribeTradeElement;
            ChartVM.UnSubscribeElement -= TonyOnUnSubscribeElement;



            ChartVM.RegisterOrder -= TonyOnRegisterOrder;
            ChartVM.SettingsChanged -= TonyOnSettingsChanged;
            ChartVM.ChartAreas.Added -= new Action<ChartArea>( TonyOnAreasAdded );
            ChartVM.ChartAreas.Removed -= new Action<ChartArea>( TonyOnAreasRemoved );
            ChartVM.ChartAreas.Cleared -= new Action( TonyOnAreaCleared );
        }

        protected override void InitializeChart()
        {
            RegisterCommandsAndEvents();

            if ( IsActive )
            {
                ChartVM.IsActive = IsActive;
            }

            ChartVM.SelectedSecurity = SelectedSecurity;

            MinimumRange = 600;
            IsInteractive = true;
            IsProgrammable = true;

            FillIndicators();
        }

        private void TonyOnSubscribeTradeElement( TradesUI arg1, Security arg2 )
        {
            throw new NotImplementedException();
        }

        private void TonyOnSubscribeOrderElement( OrdersUI arg1, Security arg2 )
        {
            throw new NotImplementedException();
        }

        private void TonyOnCreateOrder( ChartArea arg1, Order arg2 )
        {
            throw new NotImplementedException();
        }

        private void TonyOnUnSubscribeElement( IfxChartElement element )
        {
            var command = new ChartRemoveElementExCommand( element, ChartVM.GetSource( element ) );
            
            if ( _loading )
            {
                OnChartRemoveElementCommand( command );
            }
            else
            {
                command.Process( this, false );
            }

            if ( !CanTrackChanges() )
            {
                return;
            }

            RaiseChangedCommand();
        }


        public override void Step01_ExecuteAddChartArea()
        {
            _stopWatch.Start();

            if ( ChartVM.CanAddArea() )
            {
                ChartVM.Step01_AddChartArea();
            }
        }

        public override void Step01_ExecuteAddIndicatorArea()
        {
            _stopWatch.Start();

            if ( ChartVM.CanAddArea() )
            {
                ChartVM.Step01_AddIndicatorArea();
            }
        }

        public bool CanAddArea()
        {
            return ( ( ChartVM != null ) && IsInteractive );
        }

        private void ExecuteQuickOrderSettingChanged()
        {

        }

        protected override void OnInitializeInDesignMode()
        {
            base.OnInitializeInDesignMode();
        }

        protected override void OnInitializeInRuntime()
        {
            base.OnInitializeInRuntime();

        }




        public event Action SettingsChanged;

        private void OnUiChanged()
        {
            if ( !CanTrackChanges() )
            {
                return;
            }

            RaiseChangedCommand();
        }

        private void TonyOnAreaCleared()
        {
            OnUiChanged();
        }

        private void TonyOnAreasRemoved( ChartArea area )
        {
            OnUiChanged();
        }

        private void TonyOnAreasAdded( ChartArea area )
        {
            OnUiChanged();
        }


        private void TonyOnSettingsChanged()
        {
            OnUiChanged();
        }

        private void TonyOnRegisterOrder( ChartArea area, Order orderDraft )
        {
            Security security = orderDraft.Security;

            if ( security == null )
            {
                security = area.Elements.OfType<CandlestickUI>().Select( e => ( ChartVM.GetSource( e ) as CandleSeries )?.Security ).FirstOrDefault( s => s != null );
                if ( security == null )
                {
                    MessageBoxService.Show( messageBoxText: LocalizedStrings.Str1380, caption: "Register Order", button: MessageBoxButton.OK );
                    return;
                }
            }

            if ( orderDraft.Portfolio == null )
            {
                MessageBoxService.Show( messageBoxText: LocalizedStrings.Str1381, caption: "Register Order", button: MessageBoxButton.OK );
            }
            else
            {
                Order order = new Order()
                {
                    Type = new OrderTypes?( OrderTypes.Limit ),
                    Volume = orderDraft.Volume,
                    Direction = orderDraft.Direction,
                    Security = security,
                    Portfolio = orderDraft.Portfolio,
                    Price = security.ShrinkPrice( orderDraft.Price, ShrinkRules.Auto )
                };

                new RegisterOrderCommand( order ).Process( this, false );

                ActiveOrdersUI activeOrdersElement = area.Elements.OfType<ActiveOrdersUI>().FirstOrDefault();
                if ( activeOrdersElement == null )
                {
                    activeOrdersElement = new ActiveOrdersUI();
                    ChartVM.AddElement( area, activeOrdersElement );
                }

                ChartActiveOrderInfo chartActiveOrderInfo = new ChartActiveOrderInfo();
                _chartOrders.Add( order, chartActiveOrderInfo );
                activeOrdersElement.Orders.Add( chartActiveOrderInfo );
                chartActiveOrderInfo.UpdateOrderState( order, false, true );
            }
        }

        private void TonyOnMoveOrder( Order order, Decimal price )
        {
            Order orderClone = order.ReRegisterClone( new Decimal?( price ), new Decimal?() );
            ChartActiveOrderInfo activeOrderInfo = _chartOrders.TryGetValue( order );

            if ( activeOrderInfo != null )
            {
                activeOrderInfo.IsFrozen = true;
                var newOrderInfo = new ChartActiveOrderInfo()
                {
                    AutoRemoveFromChart = activeOrderInfo.AutoRemoveFromChart,
                    ChartX = activeOrderInfo.ChartX
                };

                _chartOrders.Add( orderClone, newOrderInfo );
                activeOrderInfo.Element.Orders.Add( newOrderInfo );
                newOrderInfo.UpdateOrderState( orderClone, false, true );
            }

            new ReRegisterOrderCommand( order, orderClone ).Process( this, false );
        }


        private void TonyOnCancelOrder( Order order )
        {
            ChartActiveOrderInfo chartActiveOrderInfo = _chartOrders.TryGetValue( order );
            if ( chartActiveOrderInfo != null )
            {
                chartActiveOrderInfo.IsFrozen = true;
            }

            new CancelOrderCommand( order ).Process( this, false );
        }

        private void TonyOnAnnotationCreated( AnnotationUI annotation )
        {
            OnUiChanged();
        }

        private void TonyOnAnnotationDeleted( AnnotationUI annotation )
        {
            _annotations.Remove( annotation );

            OnUiChanged();
        }

        private void TonyOnAnnotationModified( AnnotationUI annotation, ChartDrawDataEx.sAnnotation data )
        {
            if ( _loading || data.X1 == null && data.X2 == null && ( data.Y1 == null && data.Y2 == null ) )
            {
                return;
            }

            _annotations[annotation] = data;

            OnUiChanged();
        }

        private void OnChartResetElementCommand( ChartResetElementExCommand command )
        {
            ChartVM.Reset( new IfxChartElement[1] { command.Element } );
            IndicatorPair tag = ( IndicatorPair )command.Tag;
            tag.MyIndicator.Load( tag.UI.Save() );

            var candleSeriesData = _candles.TryGetValue( tag.Series );
            if ( candleSeriesData == null )
            {
                return;
            }

            var data = new ChartDrawDataEx();

            var indicatorUI = ( IndicatorUI )command.Element;
            var indicator = tag.MyIndicator;
            var count = candleSeriesData.BarsRepo.MainDataBars.Count;
            var indicatorList = data.SetIndicatorSource( indicatorUI, count );

            var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( tag.Series.Security );

            //FreemindIndicator fmIndicator = null;

            //if ( aa != null )
            //{
            //    fmIndicator = ( FreemindIndicator )aa.GetFreemindIndicator( _bars.Period.Value );
            //}

            for ( int i = 0; i < count; i++ )
            {
                ref SBar bar = ref candleSeriesData.BarsRepo[i];

                var indicatorFm = indicator.Process( ref bar );
                
                indicatorList.SetIndicatorValue( bar.BarTime, indicatorFm );
            }

            ChartVM.Draw( data );

            OnUiChanged();
        }

        private void OnChartRemoveElementCommand( ChartRemoveElementExCommand command )
        {

            var ind = command.Element as IndicatorUI;

            if ( ind != null )
            {
                _indicators.Remove( ind );
                foreach ( KeyValuePair<CandleSeries, Tuple<IndicatorUI, IndicatorPair>[ ]> keyValuePair in _indicatorsBySeries.ToArray() )
                {
                    Tuple<IndicatorUI, IndicatorPair>[ ] array = keyValuePair.Value.Where( t => t.Item1 != ind ).ToArray();
                    if ( array.Length == 0 )
                        _indicatorsBySeries.Remove( keyValuePair.Key );
                    else
                        _indicatorsBySeries[keyValuePair.Key] = array;
                }
            }
            else
            {
                if ( !( command.Element is CandlestickUI ) )
                    return;
                CandleSeries source = ( CandleSeries )command.Source;
                CandleSeriesData candleSeriesData;
                if ( !_candles.TryGetAndRemove( source, out candleSeriesData ) )
                    return;

                new UnSubscribeCommand( candleSeriesData.Subscription ).Process( this, false );
                //if ( _candles.Count != 0 || source.Security != ChartVM.OrderSettings.Security )
                //    return;

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 *  Need to work on Active Orders on the Chart
                 * 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- */

                //ChartVM.OrderSettings.Security = null;
                //foreach ( ChartActiveOrdersElement activeOrdersElement in command.Element.PersistantChartArea.Elements.OfType<ChartActiveOrdersElement>( ) )
                //{
                //    LiveTradeViewModel.Reset( new ChartActiveOrdersElement[ 1 ] { activeOrdersElement } );

                //    foreach ( KeyValuePair<Order, ChartActiveOrdersElement> keyValuePair in _chartOrders.ToArray( ) )
                //    {
                //        if ( keyValuePair.Value == activeOrdersElement )
                //            _chartOrders.Remove( keyValuePair.Key );
                //    }
                //}
            }


        }

        private void OnOrderFailCommand( OrderFailCommand cmd )
        {
            //ChartActiveOrderInfo chartActiveOrderInfo = _chartOrders.TryGetValue( cmd.Fail.Order );
            //if ( chartActiveOrderInfo == null )
            //{
            //    return;
            //}

            //chartActiveOrderInfo.UpdateOrderState( cmd.Fail.Order, true, false );
            //if ( !cmd.IsRegistering )
            //{
            //    return;
            //}

            //chartActiveOrderInfo.Element.Orders.Remove( chartActiveOrderInfo );
        }

        private void OnSelectCommand( SelectCommand cmd )
        {
            Security instance;
            if ( OrderSettings.Security != null || ( instance = cmd.Instance as Security ) == null )
            {
                return;
            }

            OrderSettings.Security = instance;
        }

        private void OnOrderCommand( OrderCommand cmd )
        {
            //Order order = cmd.Order;
            //ChartActiveOrderInfo activeOrderInfo = _chartOrders.TryGetValue( order );

            //if ( activeOrderInfo == null )
            //{
            //    if ( !ChartVM.OrderCreationMode || order.State == OrderStates.Done || order.State == OrderStates.Failed )
            //    {
            //        return;
            //    }

            //    CandlestickUI candleStick = ChartVM.Elements.OfType<CandlestickUI>( ).FirstOrDefault( e => ( ( CandleSeries )ChartVM.GetSource( e ) )?.Security == order.Security );
            //    if ( candleStick == null )
            //    {
            //        return;
            //    }

            //    ActiveOrdersUI activeOrdersElement = candleStick.ChartArea.Elements.OfType<ActiveOrdersUI>( ).FirstOrDefault( );
            //    if ( activeOrdersElement == null )
            //    {
            //        activeOrdersElement = new ActiveOrdersUI( );
            //        ChartVM.AddElement( candleStick.ChartArea, activeOrdersElement );
            //    }

            //    ChartActiveOrderInfo newOrderInfo = new ChartActiveOrderInfo( );
            //    _chartOrders.Add( order, newOrderInfo );
            //    activeOrdersElement.Orders.Add( newOrderInfo );

            //    newOrderInfo.UpdateOrderState( order, false, false );
            //}
            //else
            //{
            //    activeOrderInfo.UpdateOrderState( order, false, false );
            //}
        }





        private bool CanTrackChanges()
        {
            return ChartVM.IsInteracted && !_loading;
        }

        protected void RaiseChangedCommand()
        {
            //new ControlChangedCommand( this ).Process( this, false );
        }





        #region REGISTERED COMMAND 

        // Tony: Using QuoteCommand is too slow, instead I use DevExpress Messenger to send message.s
        ////private void OnQuoteCommand( QuoteCommand quote )
        ////{
        ////    if ( quote.Security == SelectedSecurity )
        ////    {
        ////        if ( _isActive )
        ////        {
        ////            if ( _firstQuote )
        ////            {
        ////                _firstQuote = false;

        ////                AddQuoteToChart( quote.Bid, quote.Ask );
        ////            }
        ////            else
        ////            {
        ////                UpdateQuote( quote.QuoteTime, quote.Bid, quote.Ask );
        ////            }
        ////        }
        ////        
        ////    }                       
        ////}

        //private void AddQuoteToChart( double bid, double ask )
        //{
        //    var surfaceVM = ChartVM.ScichartSurfaceViewModels;

        //    if ( surfaceVM.Count > 0 )
        //    {
        //        var pane = surfaceVM.First( );

        //        if ( pane != null )
        //        {
        //            if ( ChartVM.CanExecuteAddQuotes( ) )
        //            {
        //                ChartVM.ExecuteAddQuotes( pane.Area, new Tuple< double, double>( bid, ask)  );
        //            }
        //        }
        //    }
        //}

        //private void UpdateQuote( DateTime offerTime, double bid, double ask )
        //{
        //    if ( ChartVM.CanUpdateQuotes( ) )
        //    {
        //        ChartVM.UpdateQuotes( offerTime,  bid, ask );
        //    }
        //}




        #endregion

        private void Step04_PassCandleSeriesToIndicator( int paneNo )
        {
            var surfaceVM = ChartVM.ScichartSurfaceViewModels;

            int paneCount = 0;

            if ( surfaceVM.Count > 0 )
            {
                foreach ( var pane in surfaceVM )
                {
                    if ( paneCount == paneNo )
                    {
                        if ( pane != null )
                        {
                            if ( ChartVM.CanExecuteAddCandlesProgramatically() )
                            {
                                ChartVM.Step05_ExecuteAddIndicatorsProgramatically( pane.Area, _candlesSeries );
                            }
                        }
                    }
                    else
                    {
                        paneCount++;
                    }
                }
            }
        }

        private void Step04_LoadCandlesFromLocalStorage()
        {
            _stopWatch.Restart();

            _initializing = true;

            var startDate = DateTime.MinValue;
            var endDate = DateTime.MinValue;

            _candlesSeries = new CandleSeries( typeof( TimeFrameCandle ), SelectedSecurity, ResponsibleTF );

            if ( _waveScenarioNumber == 1 )
            {
                ParentViewModel.LinkSeriesWithVM( _candlesSeries, this );
            }

            if ( _loadAllBars )
            {
                _candleStorage = _storageRegistry.GetCandleMessageStorage( typeof( TimeFrameCandleMessage ), SelectedSecurity.ToSecurityId(), ResponsibleTF, Drive, Format );
                var start = _candleStorage.GetFromDate();
                if ( start.HasValue )
                {
                    startDate = start.Value;
                }
                else
                {
                    ForexHelper.GetStartAndEndDateForDatabar( ResponsibleTF, out startDate, out endDate );
                }

                endDate = DateTime.UtcNow.AddMinutes( 5 );
            }
            else
            {
                ForexHelper.GetStartAndEndDateForDatabar( ResponsibleTF, out startDate, out endDate );
            }



            _candlesSeries.From = startDate;
            _candlesSeries.To = endDate;

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  Mistakes 1----------> Finally found out why when loading data, the software is taking up so much time CPU time
             *                        If we don't set AllowBuildFromSmallerTimeFrame to false, we will be using GetCandleMessageBuildableStorage
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- 
             */
            var shouldBuild = ShouldBuilldFromLowerTimerFrame( ResponsibleTF );

            _candlesSeries.AllowBuildFromSmallerTimeFrame = shouldBuild;

            if ( shouldBuild )
            {
                if ( ResponsibleTF == TimeSpan.FromSeconds( 1 ) )
                {
                    _candlesSeries.BuildCandlesFrom2 = DataType.Level1;
                    _candlesSeries.BuildCandlesField = Level1Fields.SpreadMiddle;
                }
                else if ( ResponsibleTF == TimeSpan.FromMinutes( 4 ) )
                {
                    _candlesSeries.BuildCandlesFrom2 = DataType.CandleTimeFrame;
                }

            }

            var surfaceVM = ChartVM.ScichartSurfaceViewModels;

            if ( surfaceVM.Count > 0 )
            {
                var pane = surfaceVM.First();

                if ( pane != null )
                {
                    if ( ChartVM.CanExecuteAddCandlesProgramatically() )
                    {
                        ChartVM.Step05_ExecuteAddCandlesProgramatically( pane.Area, _candlesSeries );
                    }
                }
            }
        }



        private static bool ShouldBuilldFromLowerTimerFrame( TimeSpan tf )
        {
            if ( tf == TimeSpan.FromSeconds( 1 ) )
            {
                return true;
            }
            else if ( tf == TimeSpan.FromMinutes( 4 ) )
            {
                return true;
            }

            return false;
        }

        public static void GetStartAndEndDateForDatabar( TimeSpan period, out DateTime startDate, out DateTime endDate )
        {
            startDate = DateTime.UtcNow.Date;
            endDate = DateTime.UtcNow.AddMinutes( 5 );

            var totalbars = ForexHelper.CalculateStorageSize( period );

            if ( period.Days == 30 )
            {
                startDate = DateTime.Today.AddMonths( -totalbars );
                endDate = DateTime.Now.AddDays( 1 );
            }
            else if ( period.Days == 7 )
            {
                startDate = DateTime.Today.AddDays( -totalbars * period.TotalDays );
            }
            else if ( period.Days == 1 )
            {
                startDate = DateTime.Today.AddDays( -totalbars * period.TotalDays );
            }
            else if ( period.Seconds == 1 )
            {
                endDate = DateTime.UtcNow.AddDays( 5 );
                startDate = DateTime.UtcNow.AddSeconds( -totalbars * period.TotalSeconds );
            }
            else if ( period.Ticks == 1 )
            {
                endDate = DateTime.UtcNow.AddMinutes( 5 );
                startDate = endDate.AddHours( -6 );
            }
            else
            {
                var d = DateTime.Today.AddMinutes( -( totalbars * period.TotalMinutes ) );
                startDate = new DateTime( d.Year, d.Month, d.Day, d.Hour, 0, 0, DateTimeKind.Utc );
            }
        }

        private void Step03_ChartOrIndicatorPanesAdded( object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e )
        {
            if ( e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add )
            {
                if ( e.NewStartingIndex == 0 )
                {
                    _stopWatch.Stop();

                    string msg = string.Format( "Step1 takes {0} ms", _stopWatch.Elapsed.TotalMilliseconds );

                    this.AddWarningLog( msg );

                    Step04_LoadCandlesFromLocalStorage();
                }
                else
                {
                    _stopWatch.Stop();

                    string msg = string.Format( "Adding Indicator Panel takes {0} ms", _stopWatch.Elapsed.TotalMilliseconds );

                    this.AddWarningLog( msg );

                    Step04_PassCandleSeriesToIndicator( e.NewStartingIndex );
                }
            }
        }

        public bool IsInteractive
        {
            get
            {
                return ChartVM.IsInteracted;
            }

            set
            {
                ChartVM.IsInteracted = value;
            }
        }

        public bool IsProgrammable
        {
            get
            {
                return ChartVM.IsProgrammable;
            }

            set
            {
                ChartVM.IsProgrammable = value;
            }
        }

        public int MinimumRange
        {
            get
            {
                return ChartVM.MinimumRange;
            }
            set
            {
                ChartVM.MinimumRange = value;
            }
        }


        protected static ISecurityProvider SecurityProvider
        {
            get
            {
                return ServicesRegistry.SecurityProvider;
            }
        }

        protected static IStudioCommandService CommandService
        {
            get
            {
                return ConfigManager.GetService<IStudioCommandService>();
            }
        }






        private static void DrawIndicators( Func<bool> canProcess )
        {
            //if ( _indicatorValues == null )
            //{
            //    throw new InvalidOperationException( );
            //}

            //foreach ( IEnumerable<IndicatorValue> indicatorValues in _indicatorValues.Batch( 50 ) )
            //{
            //    if ( !canProcess( ) )
            //    {
            //        break;
            //    }

            //    ChartDrawDataEx data = new ChartDrawDataEx( );
            //    foreach ( IndicatorValue indicatorValue in indicatorValues )
            //    {
            //        data.Group( indicatorValue.Time ).Add( _indicatorElement, indicatorValue.Value );
            //    }

            //    LiveTradeViewModel.Draw( data );
            //}
        }


        private void DrawQuotes( Func<bool> canProcess )
        {
            //foreach ( IEnumerable<QuoteChangeMessage> quoteChangeMessages in _quotes.Batch( 50 ) )
            //{
            //    if ( !canProcess( ) )
            //    {
            //        break;
            //    }

            //    ChartDrawDataEx data = new ChartDrawDataEx( );
            //    foreach ( QuoteChangeMessage quoteChangeMessage in quoteChangeMessages )
            //    {
            //        QuoteChange bids = quoteChangeMessage.Bids.FirstOrDefault( );
            //        QuoteChange asks = quoteChangeMessage.Asks.FirstOrDefault( );

            //        if ( ( bids != null ) || ( asks != null ) )
            //        {
            //            ChartDrawDataEx.ChartDrawDataItem chartDrawDataItem1 = data.Group( quoteChangeMessage.ServerTime );
            //            if ( bids != null )
            //            {
            //                chartDrawDataItem1.Add( _bids.Item1, _bids.Item2.Process( bids.Price, true ) );
            //            }

            //            if ( asks != null )
            //            {
            //                chartDrawDataItem1.Add( _asks.Item1, _asks.Item2.Process( asks.Price, true ) );
            //            }

            //            ChartDrawDataEx.ChartDrawDataItem chartDrawDataItem2 = chartDrawDataItem1;
            //            IndicatorUI element = _volumes.Item1;
            //            SimpleMovingAverage indicator = _volumes.Item2;
            //            Decimal num1;
            //            if ( bids == null )
            //            {
            //                Decimal? volume = asks?.Volume;
            //                num1 = ( volume.HasValue ? new Decimal?( new Decimal( ) + volume.GetValueOrDefault( ) ) : new Decimal?( ) ) ?? Decimal.Zero;
            //            }
            //            else
            //            {
            //                num1 = bids.Volume;
            //            }

            //            int num2 = 1;
            //            IIndicatorValue indicatorValue = indicator.Process( num1, num2 != 0 );
            //            chartDrawDataItem2.Add( element, indicatorValue );
            //        }
            //    }
            //    LiveTradeViewModel.Draw( data );
            //}
        }
        

    }


    public class IndicatorPair
    {
        private readonly ChartTabViewModelBase _parent;
        private readonly IndicatorUI _elem;

        public IIndicator UI { get; }

        public CandleSeries Series { get; }

        public IIndicator MyIndicator { get; }

        public IndicatorPair( ChartTabViewModelBase parent, IndicatorUI elem, IIndicator ui, CandleSeries series )
        {
            var interactiveChart = parent;
            if ( interactiveChart == null )
                throw new ArgumentNullException( nameof( parent ) );
            _parent = interactiveChart;

            var indicatorElement = elem;
            if ( indicatorElement == null )
                throw new ArgumentNullException( nameof( elem ) );
            _elem = indicatorElement;

            IIndicator indicator = ui;
            if ( indicator == null )
                throw new ArgumentNullException( nameof( ui ) );
            UI = indicator;

            CandleSeries candleSeries = series;
            if ( candleSeries == null )
                throw new ArgumentNullException( nameof( series ) );

            Series = candleSeries;
            MyIndicator = UI.Clone();
            UI.Reseted += new Action( OnReseted );
        }

        private void OnReseted()
        {
            _elem.FullTitle = UI.ToString();
            new ChartResetElementExCommand( _elem, this ).Process( _parent, false );
        }
    }

    public class LongDownloadTaskInfo
    {
        public LongDownloadTaskInfo( CandleSeries candleSeries, DateTimeOffset? from, DateTimeOffset? to, int priority )
        {
            CandleSeries = candleSeries;
            From = from;
            To = to;
            Priority = priority;
        }

        public LongDownloadTaskInfo( bool isTick, DateTimeOffset? from, DateTimeOffset? to, int priority )
        {
            CandleSeries = null;
            IsTick = isTick;
            From = from;
            To = to;
            Priority = priority;
        }

        public int Priority { get; set; }

        public CandleSeries CandleSeries
        {
            get; set;
        }




        public bool IsTick { get; set; }


        public DateTimeOffset? From { get; set; }

        public DateTimeOffset? To { get; set; }
    }
}