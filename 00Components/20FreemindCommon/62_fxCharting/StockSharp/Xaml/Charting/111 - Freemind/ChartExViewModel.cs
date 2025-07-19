using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.Collections;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Serialization;

using SciChart.Charting.Visuals.TradeChart;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic; 
using fx.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.Mvvm.UI;
using StockSharp.Xaml.Charting.Definitions;
using System.Windows;
using DevExpress.Xpf.Grid;
using SciChart.Charting.Visuals;
using fx.Common;
using StockSharp.Xaml;
using System.Threading;
using ViewModelBase = DevExpress.Mvvm.ViewModelBase;
using StockSharp.Charting;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Charts;


#pragma warning disable 067

namespace StockSharp.Xaml.Charting
{
    public partial class ChartExViewModel : ViewModelBase, IPersistable, IThemeableChart
    {
        protected TimeSpan _period;
        private sealed class NotifyPropertiesMap
        {
            public string[] _stringArray = null;
            public ChartExViewModel _chartVM = null;

            internal void NotifyAllProperties( )
            {
                Ecng.Collections.CollectionHelper.ForEach( _stringArray, p => ( ( INotifyPropertyChangedEx ) _chartVM ).NotifyPropertyChanged( p ) );                
            }
        }

        



        protected IDispatcherService DispatcherService       { get { return GetService<IDispatcherService>( ); } }
        protected IShowWindowService CustomShowWindowService { get { return GetService<IShowWindowService>( );      } }
        protected IMessageBoxService MessageBoxService       { get { return GetService<IMessageBoxService>( );      } }

        protected Style _gripStyle;

        public ChartExViewModel( )
        {                                                               
            RemoveAxisCommand         = new DelegateCommand<ChartAxis> ( ExecuteRemoveAxisCommand  , CanExecuteRemoveAxisCommand  );
            ClosePaneCommand          = new DelegateCommand<IChildPane>( ExecuteClosePaneCommand   , CanExecuteClosePaneCommand   );

            ScichartSurfaceViewModels = new ObservableCollection<ScichartSurfaceMVVM>( );
            MinimumRange              = 50;
            IsAutoScroll              = true;
            ShowOverview              = false;
            ShowLegend                = true;
            IndicatorTypes            = new ObservableCollection<IndicatorType>( );     

            
            InitVisibleRangeDP( );

            _chartAreas               = new ChartAreasList( this );

            ChartAreas.Added         += OnNewAreaAddedToChartArea;
            ChartAreas.Removed       += OnAreaRemovedFromChartArea;
            ChartAreas.Clearing      += OnClearChartArea;
            AreaAddedEvent           += Step02_OnChartAreaAddedEventInvoke;
            IndicatorAreaAddedEvent  += Step02_OnIndicatorAreaAddedEventInvoke;
            AddCandlesEvent          += OnAddCandlesArea;
            RebuildCandles           += OnRebuildCandles;
            AddIndicatorEvent        += OnAddIndicatorArea;
            AddIndicatorFifoEvent    += OnAddIndicatorFifoEvent;
            AddOrdersEvent           += OnAddOrdersArea;
            AddTradesEvent           += OnAddTradesArea;
            RemoveElementEvent       += OnRemoveElement;

            AddQuotesEvent           += OnAddQuotesEvent;
            CodingAddCandlesEvent    += Step06_OnCodingAddCandles;
            CodingAddIndicatorsEvent += Step06_OnCodingAddIndicators;
            

            var rd = new ResourceDictionary( );
            rd.Source = new Uri( "pack://application:,,,/StockSharp.Xaml.Charting;component/ChartExViewRes.xaml" );

            if ( rd.Contains( "GripStyle" ) )
            {
                _gripStyle = ( Style )rd[ "GripStyle" ];                
            }

            var propertyMap = new NotifyPropertiesMap();
        }

        private void OnAddIndicatorFifoEvent( object sender, AddIndicatorFifoEventArgs e )
        {                        
            var indicatorPicker = CustomShowWindowService.ShowIndicatorWindow( true, IndicatorTypes );

            var array   = Elements.OfType<ChartCandleElement>( ).ToArray( );
            var chartUi = e.ChartArea.Elements.OfType<ChartCandleElement>( ).Concat( array ).FirstOrDefault( );

            if ( chartUi == null )
            {
                MessageBoxResult canCloseDocument = MessageBoxService.Show( messageBoxText: LocalizedStrings.Close, caption: "Add Indicator", button: MessageBoxButton.OK );
            }
            else
            {
                if ( !indicatorPicker.AutoSelectCandles )
                {
                    var selectedElement = CustomShowWindowService.ShowCandlestickUIPicker( array, chartUi );

                    chartUi = selectedElement;
                }

                /* ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    * 
                    * Tony Indicator Step 1: The above code will show the indicator Window and let us select the wanted indicator and the following will create the Indicator and the 
                    *                  corresponding indicator painter.
                    *                  
                    * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------                 
                    */

                if ( indicatorPicker.SelectedIndicatorType == null )
                    return;

                var tonyCandleSeries         = GetSeries<CandleSeries>( chartUi );

                var period = (TimeSpan) tonyCandleSeries.Arg;

                

                ChartIndicatorElement indicatorUI = null;

                if ( e.UseFifo )
                {
                    var capacity = ChartHelper.GetFifoCapcity( period );
                    indicatorUI = new ChartIndicatorElement( capacity );
                }
                else
                {
                    indicatorUI = new ChartIndicatorElement();
                }

                var indicatorPainter                  = indicatorPicker.SelectedIndicatorType.CreatePainter();

                //var indicatorPainter         = ( object )painter != null ? painter.CreateInstance<IChartIndicatorPainter>( ) : null;

                indicatorUI.IndicatorPainter = ( StockSharp.Xaml.Charting.IChartIndicatorPainter ) indicatorPainter;

                

                AddElement( e.ChartArea, indicatorUI, tonyCandleSeries, indicatorPicker.Indicator );
            }
            
        }

        public void Refresh()
        {
            _drawSurface.Refresh( );
        }

        


        internal int InstanceCount( )
        {
            return _instanceCount;
        }

       
        

        protected override void OnInitializeInDesignMode( )
        {
            base.OnInitializeInDesignMode( );
        }

        protected override void OnInitializeInRuntime( )
        {
            base.OnInitializeInRuntime( );

            ChangeApplicationTheme( );
            ThemeManager.ApplicationThemeChanged += ThemeManager_ApplicationThemeChanged;
        }

        private void ThemeManager_ApplicationThemeChanged( DependencyObject sender, ThemeChangedRoutedEventArgs e )
        {
            ChangeApplicationTheme( );
        }

        private void ChangeApplicationTheme( )
        {
            SelectedTheme = ApplicationThemeHelper.ApplicationThemeName.ToChartTheme( );
        }


        


        bool _isAutoScroll = false;

        public bool IsAutoScroll
        {
            get { return _isAutoScroll; }
            set { SetValue( ref _isAutoScroll, value ); }
        }

        

        


        

        

        int _rightMarginBars = 150;
        public int RightMarginBars
        {
            get { return _rightMarginBars; }
            set { SetValue( ref _rightMarginBars, value ); }
        }

        

        public void CheckAndShowFibonacci( )
        {
            _candleStickUI.CheckAndShowFibonacci( );
        }

        

        bool _isAutoRangeByAnnotations = false;
        public bool AutoRangeByAnnotations
        {
            get { return _isAutoRangeByAnnotations; }
            set { SetValue( ref _isAutoRangeByAnnotations, value ); }
        }


        bool _isCrossHair = false;
        public bool CrossHair
        {
            get { return _isCrossHair; }
            set { SetValue( ref _isCrossHair, value ); }
        }

        bool _isCrossHairTooltip = false;
        public bool CrossHairTooltip
        {
            get { return _isCrossHairTooltip; }
            set { SetValue( ref _isCrossHairTooltip, value ); }
        }

        bool _isCrossHairAxisLabels = true;
        public bool CrossHairAxisLabels
        {
            get { return _isCrossHairAxisLabels; }
            set { SetValue( ref _isCrossHairAxisLabels, value ); }
        }

        bool _clickOnChartToCreateOrder = false;
        /* -------------------------------------------------------------------------------------------------------------------------------------------
         * 
         *  Tony: OrderCreationMode should be name ClickOnChartToCreateOrder
         * 
         * ------------------------------------------------------------------------------------------------------------------------------------------- */

        public bool OrderCreationMode
        {
            get { return _clickOnChartToCreateOrder; }
            set { SetValue( ref _clickOnChartToCreateOrder, value ); }
        }

        


        TimeZoneInfo _timeZoneInfo = null;
        public TimeZoneInfo TimeZone
        {
            get { return _timeZoneInfo; }
            set { SetValue( ref _timeZoneInfo, value ); }            
        }

        bool _showPerfStats;
        public bool ShowPerfStats
        {
            get { return _showPerfStats; }
            set { SetValue( ref _showPerfStats, value ); }
        }

        TimeSpan _autoRangeInterval;
        public TimeSpan AutoRangeInterval
        {
            get { return _autoRangeInterval; }
            set { SetValue( ref _autoRangeInterval, value ); }
        }

        ChartAnnotationTypes _annotationTypes = ChartAnnotationTypes.None;
        
        // Tony: Don't know why when I changed from DevExpress bindable way of doing Notify back to 
        
        public ChartAnnotationTypes AnnotationType
        {
            get { return _annotationTypes; }
            set { SetValue( ref _annotationTypes, value, changedCallback: AnnotationTypeChanged ); }
        }

        //public ChartAnnotationTypes AnnotationType
        //{
        //    get
        //    {
        //        return _annotationTypes;
        //    }
        //    set
        //    {
        //        _annotationTypes = value;
        //        this.Notify( nameof( AnnotationType ) );
        //    }
        //}

        void AnnotationTypeChanged( )
        {
            RaisePropertyChanged( nameof( AnnotationType ) );
        }

        public void InitVisibleRangeDP( )
        {
            VisibleRangeDpo.InitRangeDepProperty( this );
        }



        #region Public Events
        public event Action AreaAddedEvent;

        public event Action IndicatorAreaAddedEvent;

        public event Action<ChartArea> AddCandlesEvent;

        public event Action< ChartArea, Tuple< double, double > > AddQuotesEvent;

        public event EventHandler< AddCandlesEventArgs >              CodingAddCandlesEvent;
        public event EventHandler< AddIndicatorEventArgs >              CodingAddIndicatorsEvent;
        public event EventHandler< AddIndicatorFifoEventArgs >        AddIndicatorFifoEvent;

        public event Action<ChartArea>                            AddIndicatorEvent;

        public event Action< IChartElement, CandleSeries > RebuildCandles;

        

        public event Action<ChartArea> AddOrdersEvent;

        public event Action<ChartArea> AddTradesEvent;

        public event Action<IChartElement> RemoveElementEvent;

        public event Action SettingsChanged;

        public event Action<ChartArea, Order> RegisterOrder;

        public event Action<ChartArea, Order> CreateOrder;

        public event Action<Order, Decimal> MoveOrder;

        public event Action<Order> CancelOrder;

        public event Action<ChartAnnotation> AnnotationCreated;

        public event Action<ChartAnnotation, ChartDrawData.AnnotationData> AnnotationModified;

        public event Action<ChartAnnotation> AnnotationDeleted;

        public event Action<ChartAnnotation, ChartDrawData.AnnotationData> AnnotationSelected;

        public event Action<ChartCandleElement, CandleSeries> SubscribeCandleElement;        

        public event Action<ChartIndicatorElement, CandleSeries, IIndicator> SubscribeIndicatorElement;

        public event Action<OrdersUI, Security> SubscribeOrderElement;

        public event Action<TradesUI, Security> SubscribeTradeElement;

        public event Action<IChartElement> UnSubscribeElement;

        public static event Action RefreshEvent;

        private Action<Order> CancelActiveOrderEvent = null;

        public void InvokeRemoveElementEvent( IChartElement element )
        {
            RemoveElementEvent?.Invoke( element );
        }

        #endregion

        #region Commands
        
        

        

        

        /* -----------------------------------------------------------------------------------------------------------------*/
        public DelegateCommand<Tuple<ChartArea, CandleSeries>> AddProgramCandlesCommand
        {
            get;
            private set;
        }

        private void ExecuteAddProgramCandlesCommand( Tuple<ChartArea, CandleSeries> tuple )
        {
            CodingAddCandlesEvent?.Invoke( this, new AddCandlesEventArgs( tuple.Item1, tuple.Item2 ) );
        }

        public bool CanExecuteAddProgramCandlesCommand( Tuple<ChartArea, CandleSeries> tuple )
        {
            return IsProgrammable;
        }

        



        #endregion

        #region Properties
        private ChartAxisType _xAxisType = ChartAxisType.CategoryDateTime;

        public ChartAxisType XAxisType
        {
            get
            {
                return _xAxisType;
            }
            set
            {
                if ( _xAxisType == value )
                {
                    return;
                }

                if ( ChartAreas.SelectMany( a => a.Elements ).Any( ) )
                {
                    throw new InvalidOperationException( LocalizedStrings.AxisTypeCantBeSet );
                }

                _xAxisType = value;

                foreach ( ChartArea chartArea in ChartAreas )
                {
                    chartArea.XAxisType = _xAxisType;
                }
            }
        }

        public bool IsAutoRange
        {
            get
            {
                return GetValue<bool>( );
            }

            set
            {
                SetValue( value );

                foreach ( ChartArea area in ChartAreas )
                {
                    Ecng.Collections.CollectionHelper.ForEach( area.XAxises, ax => ax.AutoRange = value );
                    
                }

                RaisePropertyChanged( nameof( IsAutoRange ) );                
            }
        }

        

        public bool YAxisIsAutoRange
        {
            get
            {
                return GetValue<bool>( );
            }

            set
            {
                SetValue( value );

                foreach ( ChartArea area in ChartAreas )
                {
                    Ecng.Collections.CollectionHelper.ForEach( area.YAxises, ax => ax.AutoRange = value );                    
                }

                RaisePropertyChanged( nameof( YAxisIsAutoRange ) );
            }
        }

        private ISecurityProvider _securityProvider;

        public ISecurityProvider SecurityProvider
        {
            get
            {
                return _securityProvider;
            }
            set
            {
                _securityProvider = value;
            }
        }

        private bool _disableIndicatorReset;

        public bool DisableIndicatorReset
        {
            get
            {
                return _disableIndicatorReset;
            }
            set
            {
                _disableIndicatorReset = value;
            }
        }

        
        #endregion


        #region ChartAreaList

        private sealed class ChartAreasList : BaseList<ChartArea>, IPersistable
        {
            private readonly ChartExViewModel _chartExViewModel;

            public ChartAreasList( ChartExViewModel viewModel )
            {
                ChartExViewModel vm = viewModel;
                if ( vm == null )
                {
                    throw new ArgumentNullException( "chart" );
                }

                _chartExViewModel = vm;
            }

            protected override bool OnAdding( ChartArea newArea )
            {
                Ecng.Xaml.XamlHelper.EnsureUIThread( _chartExViewModel );

                if ( newArea.Chart != null )
                {
                    lock ( newArea.GetStackTrace( ).SyncRoot )
                    {
                        ;
                    }

                    throw new ArgumentException( "area" );
                }
                if ( newArea == null || Contains( newArea ) )
                {
                    throw new ArgumentException( "area2" );
                }

                if ( newArea.Elements.IsEmpty( ) )
                {
                    newArea.XAxisType = _chartExViewModel.XAxisType;
                }
                else if ( newArea.XAxisType != _chartExViewModel.XAxisType )
                {
                    throw new InvalidOperationException( LocalizedStrings.InvalidAxisType );
                }

                return base.OnAdding( newArea );
            }

            protected override void OnAdded( ChartArea area )
            {
                foreach ( ChartAxis xAxis in area.XAxises )
                {
                    xAxis.AutoRange = _chartExViewModel.IsAutoRange;
                }

                area.PropertyChanged += new PropertyChangedEventHandler( ChartAreaHeightPropertyChanged );
                base.OnAdded( area );
            }

            protected override void OnRemoved( ChartArea area )
            {
                Ecng.Xaml.XamlHelper.EnsureUIThread( _chartExViewModel );
                area.PropertyChanged -= new PropertyChangedEventHandler( ChartAreaHeightPropertyChanged );
                base.OnRemoved( area );
            }

            private void ChartAreaHeightPropertyChanged( object sender, PropertyChangedEventArgs e )
            {
                ChartArea chartArea = ( ChartArea )sender;
                if ( !( e.PropertyName == "Height" ) )
                {
                    return;
                }

                chartArea.ViewModel.Height = chartArea.Height;
            }

            public void DrawChartAreas( ChartDrawData drawData )
            {
                if ( drawData == null )
                {
                    throw new ArgumentNullException( "data" );
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                * 
                *  Step 7----------> 9 All the Scichart Area will use the candle to draw
                *                           
                * ------------------------------------------------------------------------------------------------------------------------------------------- 
                */
                foreach ( ChartArea chartArea in this )
                {
                    chartArea.ViewModel.Draw( drawData );
                }
            }

            public void UpdateQuotes( DateTime offerTime, double bid, double ask )
            {
                foreach ( ChartArea chartArea in this )
                {
                    chartArea.ViewModel.UpdateQuote( offerTime,  bid, ask );
                }
            }

            public void ResetChartAreas( IChartElement[ ] element )
            {
                foreach ( ChartArea chartArea in this )
                {
                    chartArea.ViewModel.Reset( element );
                }
            }

            /// <summary>
            /// Tony: The following function will try to DeSerialize and reStore the ChartAreas with Candles and Indicators
            /// </summary>
            /// <param name="settings"></param>
            /// <param name="candleSeries"></param>
            /// <param name="indicatorSeries"></param>
            public void GetAreas( SettingsStorage settings,
                                  IDictionary<Guid, object> candleSeries,
                                  IDictionary<Guid, IIndicator> indicatorSeries )
            {
                Clear( );

                foreach ( SettingsStorage storage in settings.GetValue<IEnumerable<SettingsStorage>>( "Areas", null ) )
                {
                    ChartArea chartArea = new ChartArea( );
                    var viewModel = new ScichartSurfaceMVVM( chartArea, true );
                    chartArea.ViewModel = viewModel;

                    chartArea.Load( storage );

                    foreach ( IChartElement element in chartArea.Elements )
                    {
                        object candle = Ecng.Collections.CollectionHelper.TryGetValue( candleSeries, element.Id );
                        if ( candle != null )
                        {
                            _chartExViewModel._uiDatasource.Add( element, candle );
                        }

                        if ( element is ChartIndicatorElement key )
                        {
                            IIndicator iindicator = Ecng.Collections.CollectionHelper.TryGetValue(indicatorSeries, key.Id );
                            if ( candle != null )
                            {
                                _chartExViewModel._indicators.Add( key, iindicator );
                            }
                        }
                    }
                    Add( chartArea );
                    chartArea.ViewModel.Height = storage.GetValue( "Height", double.NaN );
                }
            }

            void IPersistable.Load( SettingsStorage settings )
            {
                throw new NotSupportedException( );
            }

            public void Save( SettingsStorage settings )
            {
                settings.SetValue( "Areas", this.Select( a =>
                {
                    var s = a.Save( );
                    s.SetValue( "Height", a.ViewModel.Height );
                    return s;
                } ).ToArray( ) );
            }


        }

        #endregion


        #region IChart

        

        IList<IndicatorType> IChart.IndicatorTypes => IndicatorTypes;

        public string ChartTheme
        {
            get
            {
                return SelectedTheme;
            }
            set
            {
                SelectedTheme = value;
            }
        }

        public void ClearAreas( )
        {
            DispatcherService.BeginInvoke( ( ) => _chartAreas.Clear( ) );            
        }

        void IChart.RemoveElement( ChartArea area, IChartElement element )
        {
            if ( area == null )
            {
                throw new ArgumentNullException( nameof( area ) );
            }

            if ( element == null )
            {
                throw new ArgumentNullException( nameof( element ) );
            }

            ChartIndicatorElement indicator = element as ChartIndicatorElement;
            if ( indicator != null )
            {
                _indicators.Remove( indicator );
            }

            if ( Thread.CurrentThread == Application.Current.Dispatcher.Thread )
            {
                area.Elements.Remove( element );
                _uiDatasource.Remove( element );
            }                
            else
            {
                DispatcherService.BeginInvoke( ( ) =>
                                                    {
                                                        area.Elements.Remove( element );
                                                        _uiDatasource.Remove( element );
                                                    }
                                                );
            }

                      

            
        }

        public void Draw( ChartDrawData data )
        {
            /* -------------------------------------------------------------------------------------------------------------------------------------------
            * 
            *  Step 7----------> 8 The Single Candle is passed to DrawChartAreas
            *                           
            * ------------------------------------------------------------------------------------------------------------------------------------------- 
            */
            _chartAreas.DrawChartAreas( data );
        }

        public void UpdateQuotes( DateTime offerTime, double bid, double ask )
        {
            _chartAreas.UpdateQuotes( offerTime, bid, ask );
        }

        public void Load( SettingsStorage storage )
        {
            IsAutoScroll           = storage.GetValue( "IsAutoScroll"          , IsAutoScroll );
            IsAutoRange            = storage.GetValue( "IsAutoRange"           , IsAutoRange );
            XAxisType              = storage.GetValue( "XAxisType"             , ChartAxisType.CategoryDateTime );
            AutoRangeByAnnotations = storage.GetValue( "AutoRangeByAnnotations", AutoRangeByAnnotations );
            ShowOverview           = storage.GetValue( "ShowOverview"          , ShowOverview );
            ShowLegend             = storage.GetValue( "ShowLegend"            , ShowLegend );
            CrossHair              = storage.GetValue( "CrossHair"             , CrossHair );
            CrossHairTooltip       = storage.GetValue( "CrossHairTooltip"      , CrossHairTooltip );
            CrossHairAxisLabels    = storage.GetValue( "CrossHairAxisLabels"   , CrossHairAxisLabels );
            OrderCreationMode      = storage.GetValue( "OrderCreationMode"     , OrderCreationMode );
            TimeZone = MayBe.With( storage.GetValue<string>( "TimeZone", null ), new Func<string, TimeZoneInfo>( TimeZoneInfo.FindSystemTimeZoneById ) ) ?? TimeZoneInfo.Local;
            //TimeZone               = MayBe.With(storage.GetValue<string>("TimeZone", null), new Func<string, TimeZoneInfo>( TimeZoneInfo.FindSystemTimeZoneById ) ) ?? TimeZoneInfo.Local;

            if ( !IsInteracted )
            {
                return;
            }

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  Step 7A----------> 0 All the CandleSeries and IndicatorSeries are loaded from SettingStorage
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- 
             */
            var candleSeries       = GetCandleSeries( storage.GetValue<SettingsStorage>( "Sources", null ) );
            var indicatorSeries    = GetIndicators( storage.GetValue<SettingsStorage>( "Indicators", null ) );
            _chartAreas.GetAreas( storage.GetValue<SettingsStorage>( "Areas", null ), candleSeries, indicatorSeries );
        }

        public void Save( SettingsStorage storage )
        {
            storage.SetValue( "IsAutoScroll", IsAutoScroll );
            storage.SetValue( "IsAutoRange", IsAutoRange );
            storage.SetValue( "XAxisType", XAxisType );
            storage.SetValue( "AutoRangeByAnnotations", AutoRangeByAnnotations );
            storage.SetValue( "ShowOverview", ShowOverview );
            storage.SetValue( "ShowLegend", ShowLegend );
            storage.SetValue( "CrossHair", CrossHair );
            storage.SetValue( "CrossHairTooltip", CrossHairTooltip );
            storage.SetValue( "CrossHairAxisLabels", CrossHairAxisLabels );
            storage.SetValue( "OrderCreationMode", OrderCreationMode );
            storage.SetValue( "TimeZone", TimeZone?.Id );

            if ( !IsInteracted )
            {
                return;
            }

            storage.SetValue( "Sources", SaveCandleSeries( ) );
            storage.SetValue( "Indicators", SaveIndicators( ) );
            storage.SetValue( "Areas", _chartAreas.Save( ) );
        }

        private SettingsStorage SaveCandleSeries( )
        {
            SettingsStorage storage = new SettingsStorage( );
            PooledDictionary<CandleSeries, Guid> dictionary = new PooledDictionary<CandleSeries, Guid>( );

            foreach ( var pair in _uiDatasource.SyncGet( d => d.ToArray( ) ) )
            {
                string elementGuid = pair.Key.Id.ToString( );

                if ( pair.Value is CandleSeries key )
                {
                    Guid guid = dictionary.SafeAdd( key, ( s => Guid.NewGuid( ) ) );
                    storage.SetValue( elementGuid, guid );
                }
                else
                {
                    storage.SetValue( elementGuid, ( ( Security )pair.Value ).Id );
                }
            }

            if ( dictionary.Count > 0 )
            {
                SettingsStorage candleSeriesStorage = new SettingsStorage( );

                foreach ( KeyValuePair<CandleSeries, Guid> candleSeries in dictionary )
                {
                    // This setting is the reverse of the above setting, it saves the CandleSeries map to Guid 
                    candleSeriesStorage.Add( candleSeries.Value.ToString( ), candleSeries.Key.Save( ) );
                }
                storage.SetValue( "CandleSeries", candleSeriesStorage );
            }
            return storage;
        }

        private SettingsStorage SaveIndicators( )
        {
            SettingsStorage settingsStorage = new SettingsStorage( );

            foreach ( var pair in _indicators.SyncGet( d => d.ToArray( ) ) )
            {
                settingsStorage.SetValue( pair.Key.Id.ToString( ), pair.Value.SaveEntire( false ) );
            }

            return settingsStorage;
        }

        private IDictionary<Guid, object> GetCandleSeries( SettingsStorage settings )
        {
            PooledDictionary<Guid, object> output = new PooledDictionary<Guid, object>( );
            if ( settings == null )
            {
                return output;
            }

            var candleSeriesMap = new PooledDictionary<Guid, CandleSeries>( );
            var candleStorage = settings.GetValue<SettingsStorage>( "CandleSeries", null );

            if ( candleStorage != null )
            {
                foreach ( KeyValuePair<string, object> keyValuePair in candleStorage )
                {
                    Guid key = keyValuePair.Key.To<Guid>( );
                    CandleSeries candleSeries = ( ( SettingsStorage )keyValuePair.Value ).Load<CandleSeries>( );
                    candleSeries.PropertyChanged += new PropertyChangedEventHandler( OnCandleSeriesPropertyChanged );
                    candleSeriesMap.Add( key, candleSeries );
                }
            }

            foreach ( KeyValuePair<string, object> keyValuePair in settings )
            {
                if ( !( keyValuePair.Key == "CandleSeries" ) )
                {
                    object obj;
                    if ( keyValuePair.Value is SettingsStorage storage )
                    {
                        CandleSeries candleSeries = storage.Load<CandleSeries>( );
                        candleSeries.PropertyChanged += new PropertyChangedEventHandler( OnCandleSeriesPropertyChanged );
                        obj = candleSeries;
                    }
                    else
                    {
                        obj = !( keyValuePair.Value is Guid index )
                            ? ( SecurityProvider ?? ServicesRegistry.SecurityProvider ).LookupById( ( string )keyValuePair.Value )
                            : ( object )candleSeriesMap[ index ];
                    }

                    output.Add( keyValuePair.Key.To<Guid>( ), obj );
                }
            }
            return output;
        }

        private static IDictionary<Guid, IIndicator> GetIndicators( SettingsStorage settings )
        {
            PooledDictionary<Guid, IIndicator> dictionary = new PooledDictionary<Guid, IIndicator>( );
            if ( settings == null )
            {
                return dictionary;
            }

            foreach ( KeyValuePair<string, object> keyValuePair in settings )
            {
                dictionary.Add( keyValuePair.Key.To<Guid>( ), ( ( SettingsStorage )keyValuePair.Value ).LoadEntire<IIndicator>( ) );
            }

            return dictionary;
        }
        #endregion


        #region Invoke Class Events

        public void InvokeCreateOrderEvent( ChartArea area, Order order )
        {
            CreateOrder?.Invoke( area, order );
        }

        public void InvokeMoveOrderEvent( Order order, Decimal valueOne )
        {
            MoveOrder?.Invoke( order, valueOne );
        }

        public void InvokeCancelOrderEvent( Order order )
        {
            CancelOrder?.Invoke( order );
        }

        public void InvokeAnnotationCreatedEvent( ChartAnnotation annotation )
        {
            AnnotationCreated?.Invoke( annotation );
        }

        public void InvokeAnnotationModifiedEvent( ChartAnnotation annotation, ChartDrawData.AnnotationData aData )
        {
            AnnotationModified?.Invoke( annotation, aData );
        }

        public void InvokeAnnotationDeletedEvent( ChartAnnotation annotation )
        {
            AnnotationDeleted?.Invoke( annotation );
        }

        public void InvokeAnnotationSelectedEvent( ChartAnnotation annotation, ChartDrawData.AnnotationData aData )
        {
            AnnotationSelected?.Invoke( annotation, aData );
        }
        #endregion

        #region Initialization Stuff

        // Tony: The fucking reason why AnnotationType is not working is because we have two PropertyChanged event handlers in this code.
        // 1 from the following
        // 2 from the Bindable base.
        // This is so fucked up.


        //private PropertyChangedEventHandler _propertyChangedEventHandler;
        //event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        //{
        //    add
        //    {
        //        _propertyChangedEventHandler += value;
        //    }
        //    remove
        //    {
        //        _propertyChangedEventHandler -= value;
        //    }
        //}

        //void INotifyPropertyChangedEx.NotifyPropertyChanged( string propertyName )
        //{
        //    PropertyChangedEventHandler handler = _propertyChangedEventHandler;
        //    if ( handler == null )
        //    {
        //        return;
        //    }

        //    handler.Invoke( this, propertyName );
        //}


        #endregion

        

    }



    public interface IShowWindowService
    {
        CandleSeries ShowCandleWindow( CandleSeries series );

        IndicatorPickerWindow ShowIndicatorWindow( bool autoSelectCandles, IList< IndicatorType > indicatorTypes );

        ChartCandleElement ShowCandlestickUIPicker( ChartCandleElement[ ] array, ChartCandleElement ChartCandleElement );

        Security ShowSecurityPickerWindow( MultiSelectMode SelectionMode );
    }

    /// <summary>
    /// This custom Show Window Service enble 
    /// </summary>
    public class CustomShowWindowService : ServiceBase, IShowWindowService
    {
        public CandleSeries ShowCandleWindow( CandleSeries series )
        {
            var w = new CandleSettingsWindow( );

            w.Series = series;
            w.SecurityProvider = ServicesRegistry.SecurityProvider;
            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            w.ShowDialog( );

            return w.Series;
        }

        public IndicatorPickerWindow ShowIndicatorWindow( bool autoSelectCandles, IList<IndicatorType> indicatorTypes )
        {
            IndicatorPickerWindow w = new IndicatorPickerWindow( )
            {
                AutoSelectCandles = autoSelectCandles,
                IndicatorTypes    = indicatorTypes
            };

            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            w.ShowDialog( );

            return w;
        }

        public ChartCandleElement ShowCandlestickUIPicker( ChartCandleElement[ ] array, ChartCandleElement ChartCandleElement )
        {
            CandlestickUIPicker w = new CandlestickUIPicker( )
            {
                Elements = array,
                SelectedElement = ChartCandleElement
            };

            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            w.ShowDialog( );

            return w.SelectedElement;
        }

        public Security ShowSecurityPickerWindow( MultiSelectMode SelectionMode )
        {
            var w                   = new SecurityPickerWindow( );
            w.SelectionMode         = SelectionMode;
            w.SecurityProvider      = ServicesRegistry.SecurityProvider;
            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            w.ShowDialog( );

            return w.SelectedSecurity;
        }

        
    }
}
