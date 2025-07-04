using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using SciChart.Charting.Common;
using SciChart.Charting.Visuals;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using System;
using System.Collections.Generic; 
using fx.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

using fx.Charting.Definitions;
using StockSharp.Xaml;
using DevExpress.Mvvm.Native;
using StockSharp.Charting;

#pragma warning disable CA1416

namespace fx.Charting
{
    public partial class Chart : UserControl, INotifyPropertyChanged, IComponentConnector, IPersistable, INotifyPropertyChangedEx, IChart, IThemeableChart
    {
        private readonly SynchronizedDictionary<IndicatorUI, IIndicator> _indicators   = new SynchronizedDictionary<IndicatorUI, IIndicator>( );
        private readonly SynchronizedDictionary<IChartElement, object>   _uiDatasource = new SynchronizedDictionary<IChartElement, object>( );
        private readonly CachedSynchronizedList<IChartElement>           _uiList       = new CachedSynchronizedList<IChartElement>( );

        private static int staticChartCount;

        private readonly int                _instanceCount = ++staticChartCount;       
        private bool                        _isAutoScroll = true;
        private bool                        _crossHairAxisLabels = true;
        private bool                        _crossHair = true;
        private ChartAxisType               _xAxisType = ChartAxisType.CategoryDateTime;
        private TimeSpan                    _autoRangeInterval = TimeSpan.FromMilliseconds( 200.0 );
        private TimeZoneInfo                _timeZoneInfo = TimeZoneInfo.Local;

        private CandleSeries                _candleSeries;
        private readonly ChartViewModel     _chartViewModel;
        private readonly ChartAreasList     _chartAreas;
        private ChartAnnotationTypes        _annotationTypes;
        private bool                        _crossHairTooltip;
        private bool                        _orderCreationMode;
        private bool                        _isAutoRange;
        private bool                        _autoRangeByAnnotations;
        private bool                        _showPerfStats;
        private ISecurityProvider           _securityProvider;
        private bool                        _disableIndicatorReset;
        private PropertyChangedEventHandler _propertyChangedEventHandler;

        static Chart( )
        {
            LicenseManager.CreateInstance( );
        }

        public Chart( )
        {
            InitializeComponent( );

            _chartAreas          = new ChartAreasList( this );
            DataContext          = _chartViewModel = new ChartViewModel( );

            ChartAreas.Added    += OnNewAreaAddedToChartArea;
            ChartAreas.Removed  += OnAreaRemovedFromChartArea;
            ChartAreas.Clearing += OnClearChartArea;
            AreaAdded           += OnAreaAdded;
            AddCandles          += OnAddCandlesArea;            
            AddIndicator        += OnAddIndicatorArea;
            AddOrders           += OnAddOrdersArea;
            AddTrades           += OnAddTradesArea;
            RemoveElement       += OnRemoveElement;

            CodingAddCandles    += OnCodingAddCandles;
        }

        #region Tony
        /* -------------------------------------------------------------------------------------------------------------------------------------------
         *  When we clicked on the Add Candle Command from the context menu, the following function get executed.
         *  
         *  Step A ----------> 1
         * 
         * ------------------------------------------------------------------------------------------------------------------------------------------- 
         */
        private void OnAddCandlesArea( ChartArea candleArea )
        {
            CandlestickUI candleUI = new CandlestickUI( );

            if ( _candleSeries == null )
            {
                var candleSeries = new CandleSeries( );
                candleSeries.CandleType = ( typeof( TimeFrameCandle ) );
                candleSeries.Arg = TimeSpan.FromMinutes( 5.0 );
                _candleSeries = candleSeries;
            }

            CandleSettingsWindow wnd = new CandleSettingsWindow( )
            {
                Series = _candleSeries
            };

            if ( SecurityProvider != null )
            {
                wnd.SecurityProvider = SecurityProvider;
            }

            if ( !wnd.ShowModal( this ) )
            {
                return;
            }

            CandleSeries userSelectedSeries = wnd.Series;
            _candleSeries = userSelectedSeries.Clone( );
            _candleSeries.Security = null;

            AddElement( candleArea, candleUI, userSelectedSeries );
            userSelectedSeries.PropertyChanged += new PropertyChangedEventHandler( OnCandleSeriesPropertyChanged );
        }

        private void OnCodingAddCandles( object sender, AddCandlesEventArgs e )
        {
            CandlestickUI candleUI = null;
            if ( e.UseFifo )
            {
                candleUI = new CandlestickUI() { FifoCapacity = e.FifoCapcity };
            }
            else
            {
                candleUI = new CandlestickUI();
            }

            
            _candleSeries          = e.CandleSerie.Clone( );
            _candleSeries.Security = null;

            AddElement( e.ChartArea, candleUI, e.CandleSerie );
            e.CandleSerie.PropertyChanged += new PropertyChangedEventHandler( OnCandleSeriesPropertyChanged );
        }

        #endregion


        private bool Areas_Clearing( )
        {
            throw new NotImplementedException( );
        }

        internal int InstanceCount( )
        {
            return _instanceCount;
        }

        public ChartViewModel ViewModel
        {
            get
            {
                return _chartViewModel;
            }
        }

        public INotifyList<ChartArea> ChartAreas
        {
            get
            {
                return _chartAreas;
            }
        }

        public bool IsAutoScroll
        {
            get
            {
                return _isAutoScroll;
            }
            set
            {
                _isAutoScroll = value;
                this.Notify( nameof( IsAutoScroll ) );
            }
        }

        public bool IsAutoRange
        {
            get
            {
                return _isAutoRange;
            }

            set
            {
                _isAutoRange = value;

                foreach ( ChartArea area in ChartAreas )
                {
                    Ecng.Collections.CollectionHelper.ForEach( area.XAxises, ax => ax.AutoRange = value  );
                   
                }

                this.Notify( nameof( IsAutoRange ) );
            }
        }

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

        public TimeSpan AutoRangeInterval
        {
            get
            {
                return _autoRangeInterval;
            }
            set
            {
                if ( value <= TimeSpan.Zero )
                {
                    throw new ArgumentOutOfRangeException( nameof( AutoRangeInterval ) );
                }

                _autoRangeInterval = value;
                this.Notify( nameof( AutoRangeInterval ) );
            }
        }

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

        public void AddArea( ChartArea area )
        {
            this.GuiAsync( ( ) => _chartAreas.Add( area ) );
        }

        public void RemoveArea( ChartArea area )
        {
            this.GuiAsync( ( ) => _chartAreas.Remove( area ) );
        }

        public void ClearAreas( )
        {
            this.GuiAsync( ( ) => _chartAreas.Clear( ) );
        }

        public IEnumerable<IChartElement> Elements
        {
            get
            {
                return _uiList.Cache;
            }
        }

        public void AddElement( ChartArea area, IChartElement element )
        {
            if ( area == null )
            {
                throw new ArgumentNullException( nameof( area ) );
            }

            if ( element == null )
            {
                throw new ArgumentNullException( nameof( element ) );
            }

            this.GuiAsync( ( ) => area.Elements.Add( element ) );
        }

        public void AddElement( ChartArea area, CandlestickUI element, CandleSeries candleSeries )
        {
            if ( area == null )
            {
                throw new ArgumentNullException( nameof( area ) );
            }

            if ( element == null )
            {
                throw new ArgumentNullException( nameof( element ) );
            }

            if ( candleSeries == null )
            {
                throw new ArgumentNullException( nameof( candleSeries ) );
            }

            _uiDatasource.Add( element, candleSeries );

            if ( element.Title.IsEmpty( ) )
            {
                element.Title = candleSeries.Title( );
            }

            AddElement( area, element );
        }

        public void AddElement( ChartArea area, IndicatorUI indicatorUI, CandleSeries candleSeries, IIndicator indicator )
        {
            if ( area == null )
            {
                throw new ArgumentNullException( nameof( area ) );
            }

            if ( indicatorUI == null )
            {
                throw new ArgumentNullException( nameof( indicatorUI ) );
            }

            if ( candleSeries == null )
            {
                throw new ArgumentNullException( nameof( candleSeries ) );
            }

            if ( indicator == null )
            {
                throw new ArgumentNullException( nameof( indicator ) );
            }

            _uiDatasource.Add( indicatorUI, candleSeries );

            _indicators.Add( indicatorUI, indicator );

            if ( !DisableIndicatorReset )
            {
                indicator.Reseted += ( ) => OnIndicatorReset( indicatorUI, indicator );
            }

            if ( StringHelper.IsEmpty( indicatorUI.FullTitle ) )
            {
                indicatorUI.FullTitle = indicator.ToString( );
            }

            indicatorUI.CreateIndicatorPainter( IndicatorTypes, indicator );
            AddElement( area, indicatorUI );
        }

        public void AddElement( ChartArea area, OrdersUI element, Security security )
        {
            if ( area == null )
            {
                throw new ArgumentNullException( nameof( area ) );
            }

            if ( element == null )
            {
                throw new ArgumentNullException( nameof( element ) );
            }

            if ( security == null )
            {
                throw new ArgumentNullException( nameof( security ) );
            }

            _uiDatasource.Add( element, security );

            if ( element.FullTitle.IsEmpty( ) )
            {
                element.FullTitle = "{0} ({1})".Put( security.Code, element.GetType( ).GetDisplayName( null ).ToLower( ) );
            }

            AddElement( area, element );
        }

        public void AddElement( ChartArea area, TradesUI element, Security security )
        {
            if ( area == null )
            {
                throw new ArgumentNullException( nameof( area ) );
            }

            if ( element == null )
            {
                throw new ArgumentNullException( nameof( element ) );
            }

            if ( security == null )
            {
                throw new ArgumentNullException( nameof( security ) );
            }

            _uiDatasource.Add( element, security );

            if ( element.FullTitle.IsEmpty( ) )
            {
                element.FullTitle = "{0} ({1})".Put( security.Code, element.GetType( ).GetDisplayName( null ).ToLower( ) );
            }

            AddElement( area, element );
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

            IndicatorUI indicator = element as IndicatorUI;
            if ( indicator != null )
            {
                _indicators.Remove( indicator );
            }

            this.GuiAsync( ( ) => area.Elements.Remove( element ) );

            _uiDatasource.Remove( element );
        }

        public IIndicator GetIndicator( IndicatorUI element )
        {
            return Ecng.Collections.CollectionHelper.TryGetValue( _indicators, element );
            
        }

        public object GetSource( IChartElement element )
        {
            return Ecng.Collections.CollectionHelper.TryGetValue( _uiDatasource, element );
            
        }

        private T GetSeries<T>( IChartElement element ) where T : class
        {
            return ( T )GetSource( element );
        }

        public void SetSource( IChartElement element, object source )
        {
            _uiDatasource[ element ] = source;
        }

        public bool AutoRangeByAnnotations
        {
            get
            {
                return _autoRangeByAnnotations;
            }
            set
            {
                _autoRangeByAnnotations = value;
                this.Notify( nameof( AutoRangeByAnnotations ) );
            }
        }

        public int MinimumRange
        {
            get
            {
                return ViewModel.MinimumRange;
            }
            set
            {
                ViewModel.MinimumRange = value;
            }
        }

        public string ChartTheme
        {
            get
            {
                return ViewModel.SelectedTheme;
            }
            set
            {
                ViewModel.SelectedTheme = value;
            }
        }

        public bool ShowLegend
        {
            get
            {
                return ViewModel.ShowLegend;
            }
            set
            {
                ViewModel.ShowLegend = value;
            }
        }

        public bool ShowOverview
        {
            get
            {
                return ViewModel.ShowOverview;
            }
            set
            {
                ViewModel.ShowOverview = value;
            }
        }

        public bool ShowPerfStats
        {
            get
            {
                return _showPerfStats;
            }
            set
            {
                _showPerfStats = value;
                this.Notify( nameof( ShowPerfStats ) );
            }
        }

        public bool IsInteracted
        {
            get
            {
                return ViewModel.IsInteracted;
            }
            set
            {
                ViewModel.IsInteracted = value;
            }
        }

        public bool CrossHair
        {
            get
            {
                return _crossHair;
            }
            set
            {
                _crossHair = value;
                this.Notify( nameof( CrossHair ) );
            }
        }

        public bool CrossHairTooltip
        {
            get
            {
                return _crossHairTooltip;
            }
            set
            {
                _crossHairTooltip = value;
                this.Notify( nameof( CrossHairTooltip ) );
            }
        }

        public bool CrossHairAxisLabels
        {
            get
            {
                return _crossHairAxisLabels;
            }
            set
            {
                _crossHairAxisLabels = value;
                this.Notify( nameof( CrossHairAxisLabels ) );
            }
        }

        public ChartAnnotationTypes AnnotationType
        {
            get
            {
                return _annotationTypes;
            }
            set
            {
                _annotationTypes = value;
                this.Notify( nameof( AnnotationType ) );
            }
        }

        public bool OrderCreationMode
        {
            get
            {
                return _orderCreationMode;
            }
            set
            {
                _orderCreationMode = value;
                this.Notify( nameof( OrderCreationMode ) );
            }
        }

        public TimeZoneInfo TimeZone
        {
            get
            {
                return _timeZoneInfo;
            }
            set
            {
                TimeZoneInfo timeZoneInfo = value;
                if ( timeZoneInfo == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }

                _timeZoneInfo = timeZoneInfo;
                this.Notify( nameof( TimeZone ) );
            }
        }

        public IList<IndicatorType> IndicatorTypes
        {
            get
            {
                return ViewModel.IndicatorTypes;
            }
        }

        public event Action AreaAdded
        {
            add
            {
                ViewModel.AreaAddedEvent += value;
            }
            remove
            {
                ViewModel.AreaAddedEvent -= value;
            }
        }

        public event Action<ChartArea> AddCandles
        {
            add
            {
                ViewModel.AddCandlesEvent += value;
            }
            remove
            {
                ViewModel.AddCandlesEvent -= value;
            }
        }

        public event EventHandler<AddCandlesEventArgs> CodingAddCandles
        {
            add
            {
                ViewModel.CodingAddCandlesEvent += value;
            }
            remove
            {
                ViewModel.CodingAddCandlesEvent -= value;
            }
        }

        public event Action<ChartArea> AddIndicator
        {
            add
            {
                ViewModel.AddIndicatorEvent += value;
            }
            remove
            {
                ViewModel.AddIndicatorEvent -= value;
            }
        }

        public event Action<ChartArea> AddOrders
        {
            add
            {
                ViewModel.AddOrdersEvent += value;
            }
            remove
            {
                ViewModel.AddOrdersEvent -= value;
            }
        }

        public event Action<ChartArea> AddTrades
        {
            add
            {
                ViewModel.AddTradesEvent += value;
            }
            remove
            {
                ViewModel.AddTradesEvent -= value;
            }
        }

        public event Action<IChartElement> RemoveElement
        {
            add
            {
                ViewModel.RemoveElementEvent += value;
            }
            remove
            {
                ViewModel.RemoveElementEvent -= value;
            }
        }

        public event Action<ChartArea, Order> CreateOrder;

        public event Action<Order, Decimal> MoveOrder;

        public event Action<Order> CancelOrder;

        public event Action<AnnotationUI> AnnotationCreated;

        public event Action<AnnotationUI, ChartDrawDataEx.sAnnotation> AnnotationModified;

        public event Action<AnnotationUI> AnnotationDeleted;

        public event Action<AnnotationUI, ChartDrawDataEx.sAnnotation> AnnotationSelected;

        public event Action<CandlestickUI, CandleSeries> SubscribeCandleElement;

        public event Action<IndicatorUI, CandleSeries, IIndicator> SubscribeIndicatorElement;

        public event Action<OrdersUI, Security> SubscribeOrderElement;

        public event Action<TradesUI, Security> SubscribeTradeElement;

        public event Action<IChartElement> UnSubscribeElement;

        public void Reset( IEnumerable<IChartElement> elements )
        {
            _chartAreas.ResetChartAreas( elements.ToArray( ) );
        }

        public void Draw( ChartDrawDataEx data )
        {
            /* -------------------------------------------------------------------------------------------------------------------------------------------
            * 
            *  Step 7----------> 8 The Single Candle is passed to DrawChartAreas
            *                           
            * ------------------------------------------------------------------------------------------------------------------------------------------- 
            */
            _chartAreas.DrawChartAreas( data );
        }

        internal void InvokeCreateOrderEvent( ChartArea area, Order order )
        {
            CreateOrder?.Invoke( area, order );
        }

        internal void InvokeMoveOrderEvent( Order order, Decimal valueOne )
        {
            MoveOrder?.Invoke( order, valueOne );
        }

        internal void InvokeCancelOrderEvent( Order order )
        {
            CancelOrder?.Invoke( order );
        }

        

        public TimeZoneInfo GetTimeZone( )
        {
            return ChartAreas.Select( a => a.XAxises.FirstOrDefault( i => i.TimeZone != null ) ).LastOrDefault( ax => ax != null )?.TimeZone;
        }

        public void Load( SettingsStorage storage )
        {
            IsAutoScroll           = storage.GetValue( "IsAutoScroll", IsAutoScroll );
            IsAutoRange            = storage.GetValue( "IsAutoRange", IsAutoRange );
            XAxisType              = storage.GetValue( "XAxisType", ChartAxisType.CategoryDateTime );
            AutoRangeByAnnotations = storage.GetValue( "AutoRangeByAnnotations", AutoRangeByAnnotations );
            ShowOverview           = storage.GetValue( "ShowOverview", ShowOverview );
            ShowLegend             = storage.GetValue( "ShowLegend", ShowLegend );
            CrossHair              = storage.GetValue( "CrossHair", CrossHair );
            CrossHairTooltip       = storage.GetValue( "CrossHairTooltip", CrossHairTooltip );
            CrossHairAxisLabels    = storage.GetValue( "CrossHairAxisLabels", CrossHairAxisLabels );
            OrderCreationMode      = storage.GetValue( "OrderCreationMode", OrderCreationMode );
            TimeZone               = MayBe.With(storage.GetValue<string>("TimeZone", null), new Func<string, TimeZoneInfo>( TimeZoneInfo.FindSystemTimeZoneById ) ) ?? TimeZoneInfo.Local;

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
            storage.SetValue( "IsAutoScroll"          , IsAutoScroll );
            storage.SetValue( "IsAutoRange"           , IsAutoRange );
            storage.SetValue( "XAxisType"             , XAxisType );
            storage.SetValue( "AutoRangeByAnnotations", AutoRangeByAnnotations );
            storage.SetValue( "ShowOverview"          , ShowOverview );
            storage.SetValue( "ShowLegend"            , ShowLegend );
            storage.SetValue( "CrossHair"             , CrossHair );
            storage.SetValue( "CrossHairTooltip"      , CrossHairTooltip );
            storage.SetValue( "CrossHairAxisLabels"   , CrossHairAxisLabels );
            storage.SetValue( "OrderCreationMode"     , OrderCreationMode );
            storage.SetValue( "TimeZone"              , TimeZone?.Id );

            if ( !IsInteracted )
            {
                return;
            }

            storage.SetValue( "Sources"               , SaveCandleSeries( ) );
            storage.SetValue( "Indicators"            , SaveIndicators( ) );
            storage.SetValue( "Areas"                 , _chartAreas.Save( ) );
        }

        public void ReSubscribeElements( )
        {
            if ( !IsInteracted )
            {
                return;
            }

            foreach ( IChartElement chartElement in Elements )
            {
                RemoveAndRaiseUnsubscribeElementEvent( chartElement );
                AddElement( chartElement );
            }
        }

        private void OnCandleSeriesPropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            if ( !IsInteracted )
            {
                return;
            }

            var candleSeries = ( CandleSeries )sender;

            foreach ( IChartElement element in Elements.Where( x => GetSource( x ) == candleSeries ).ToArray( ) )
            {
                var CandlestickUI = element as CandlestickUI;

                if ( CandlestickUI != null )
                {
                    CandlestickUI.Title = candleSeries.Title( );
                }

                ResetElement( element, true );
            }
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

        private SettingsStorage SaveIndicators( )
        {
            SettingsStorage settingsStorage = new SettingsStorage( );

            foreach ( var pair in _indicators.SyncGet( d => d.ToArray( ) ) )
            {
                settingsStorage.SetValue( pair.Key.Id.ToString( ), pair.Value.SaveEntire( false ) );
            }

            return settingsStorage;
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

        private void AddElement( IChartElement element )
        {
            if ( GetSource( element ) == null )
            {
                return;
            }

            _uiList.Add( element );
            RaiseChartElementSubscribedEvent( element );
        }

        private void RaiseChartElementSubscribedEvent( IChartElement chartElement )
        {
            switch ( chartElement )
            {
                case CandlestickUI CandlestickUI:
                {
                    /* -------------------------------------------------------------------------------------------------------------------------------------------
                    *  When we clicked on the Add Candle Command from the context menu, the following function get executed.
                    *  
                    *  Step A ----------> 4 After Candle get added to the UI PooledList, we raise Chart Element Subscribed Event.
                    * 
                    * ------------------------------------------------------------------------------------------------------------------------------------------- 
                    */
                    SubscribeCandleElement?.Invoke( CandlestickUI, GetSeries<CandleSeries>( CandlestickUI ) );
                }                
                break;

                case IndicatorUI element:
                {
                    SubscribeIndicatorElement?.Invoke( element, GetSeries<CandleSeries>( element ), GetIndicator( element ) );
                }                
                break;

                case OrdersUI chartOrderElement:
                {
                    SubscribeOrderElement?.Invoke( chartOrderElement, GetSeries<Security>( chartOrderElement ) );
                }                
                break;

                case TradesUI chartTradeElement:
                {
                    SubscribeTradeElement?.Invoke( chartTradeElement, GetSeries<Security>( chartTradeElement ) );
                }                
                break;
            }
        }

        private void RemoveAndRaiseUnsubscribeElementEvent( IChartElement element )
        {
            if ( GetSource( element ) == null )
            {
                return;
            }

            _uiList.Remove( element );

            UnSubscribeElement?.Invoke( element );
        }

        private void OnNewAreaAddedToChartArea( ChartArea area )
        {
            area.Elements.Added   += new Action<IChartElement>( OnNewUIAddedToArea );
            area.Elements.Removed += new Action<IChartElement>( OnUIRemovedFromArea );
            
            area.Chart = ( this );
            
            ViewModel.ScichartSurfaceViewModels.Add( ( ScichartSurfaceMVVM ) area.ChartSurfaceViewModel );

            Ecng.Collections.CollectionHelper.ForEach( area.Elements, new Action<IChartElement>( OnNewUIAddedToArea ) );
            
        }

        private void OnAreaRemovedFromChartArea( ChartArea area )
        {
            area.Elements.Added   -= new Action<IChartElement>( OnNewUIAddedToArea );
            area.Elements.Removed -= new Action<IChartElement>( OnUIRemovedFromArea );
            ViewModel.ScichartSurfaceViewModels.Remove( ( ScichartSurfaceMVVM ) area.ChartSurfaceViewModel );

            Ecng.Collections.CollectionHelper.ForEach( area.Elements, new Action<IChartElement>( OnUIRemovedFromArea ) );

            
            area.Chart= null;
            area.Dispose( );
        }

        private bool OnClearChartArea( )
        {
            foreach ( ChartArea area in ChartAreas )
            {
                OnAreaRemovedFromChartArea( area );
            }

            ViewModel.InitRangeDepProperty( );
            return true;
        }

        /* -------------------------------------------------------------------------------------------------------------------------------------------
        *  When we clicked on the Add Candle Command from the context menu, the following function get executed.
        *  
        *  Step A ----------> 3 Now that NotifyList OnAdded Event has been invoked.
        * 
        * ------------------------------------------------------------------------------------------------------------------------------------------- 
        */
        private void OnNewUIAddedToArea( IChartElement element )
        {
            if ( !IsInteracted )
            {
                return;
            }

            AddElement( element );
        }

        private void OnUIRemovedFromArea( IChartElement element )
        {
            ResetElement( element, false );
        }

        private void ResetElement( IChartElement element, bool needAddElement )
        {
            IChartElement[ ] elementArray;
            if ( element is CandlestickUI )
            {
                elementArray = Elements.Where( e => GetSource( e ) == GetSeries<CandleSeries>( element ) ).ToArray( );
            }
            else
            {
                elementArray = new IChartElement[ 1 ]
                {
                    element
                };
            }

            if ( needAddElement )
            {
                if ( IsInteracted )
                {
                    Ecng.Collections.CollectionHelper.ForEach( elementArray, new Action<IChartElement>( RaiseUnsubscribeElementEvent ) );                    
                }

                Reset( elementArray );

                if ( !IsInteracted )
                {
                    return;
                }


                Ecng.Collections.CollectionHelper.ForEach( elementArray, new Action<IChartElement>( RaiseChartElementSubscribedEvent ) );
                
            }
            else
            {
                if ( !IsInteracted )
                {
                    return;
                }

                Ecng.Collections.CollectionHelper.ForEach( elementArray, new Action<IChartElement>( RemoveAndRaiseUnsubscribeElementEvent ) );

                
            }
        }

        internal static void SetupScichartSurface( SciChartSurface chartSurface )
        {
            chartSurface.DebugWhyDoesntSciChartRender = true;

            if ( chartSurface.DataContext != null )
            {
                ( ( ScichartSurfaceMVVM )chartSurface.DataContext ).SetScichartSurface( chartSurface );
            }
            else
            {
                chartSurface.DataContextChanged += ( s, e ) =>
                {
                    ( ( ScichartSurfaceMVVM )chartSurface.DataContext ).SetScichartSurface( chartSurface );
                };
            }
        }

        private void OnInitialized( object sender, EventArgs e )
        {
            SetupScichartSurface( ( SciChartSurface )sender );
        }

        private void OnIndicatorReset( IndicatorUI indicatorElement, IIndicator indicator )
        {
            indicatorElement.FullTitle = indicator.ToString( );
            ResetElement( indicatorElement, true );
        }


        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                _propertyChangedEventHandler += value;
            }
            remove
            {
                _propertyChangedEventHandler -= value;
            }
        }


        void INotifyPropertyChangedEx.NotifyPropertyChanged( string string_0 )
        {
            PropertyChangedEventHandler changedEventHandler0 = _propertyChangedEventHandler;
            if ( changedEventHandler0 == null )
            {
                return;
            }

            changedEventHandler0.Invoke( this, string_0 );
        }


        private void OnAreaAdded( )
        {
            ChartArea area             = new ChartArea( ) { Title = LocalizedStrings.Panel + " " +   ( ChartAreas .Count + 1 ), XAxisType = XAxisType };
            var viewModel              = new ScichartSurfaceMVVM( area );
            area.ChartSurfaceViewModel = viewModel;

            var timeZoneInfo = GetTimeZone( );

            foreach ( ChartAxis xAxis in area.XAxises )
            {
                xAxis.TimeZone = timeZoneInfo;
            }

            AddArea( area );
        }

        
        private void OnAddIndicatorArea( ChartArea area )
        {
            IndicatorPickerWindow indicatorPicker = new IndicatorPickerWindow( )
            {
                AutoSelectCandles = true,
                IndicatorTypes = IndicatorTypes
            };

            if ( !indicatorPicker.ShowModal( this ) )
            {
                return;
            }

            CandlestickUI[ ] array = Elements.OfType<CandlestickUI>( ).ToArray( );
            CandlestickUI CandlestickUI = area.Elements.OfType<CandlestickUI>( ).Concat( array ).FirstOrDefault( );
            
            if ( CandlestickUI == null )
            {
                new MessageBoxBuilder( ).Error( ).Text( LocalizedStrings.CandleStick ).Owner( this ).Show( );
            }
            else
            {
                if ( !indicatorPicker.AutoSelectCandles )
                {
                    CandlestickUIPicker wnd2 = new CandlestickUIPicker( )
                    {
                        Elements = array,
                        SelectedElement = CandlestickUI
                    };

                    if ( !wnd2.ShowModal( this ) )
                    {
                        return;
                    }

                    CandlestickUI = wnd2.SelectedElement;
                }

                /* ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 * Tony Indicator Step 1: The above code will show the indicator Window and let us select the wanted indicator and the following will create the Indicator and the 
                 *                  corresponding indicator painter.
                 *                  
                 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------                 
                 */
                var indicatorUI              = new IndicatorUI( );
                
                var indicatorPainter                  = indicatorPicker.SelectedIndicatorType.CreatePainter();                               

                indicatorUI.IndicatorPainter = (fx.Charting.IChartIndicatorPainter) indicatorPainter;

                var tonyCandleSeries         = GetSeries< CandleSeries >( CandlestickUI );


                AddElement( area, indicatorUI, tonyCandleSeries, indicatorPicker.Indicator );
            }
        }

        private void OnAddOrdersArea( ChartArea area )
        {
            SecurityPickerWindow securityPickerWindow = new SecurityPickerWindow( );
            securityPickerWindow.SelectionMode = ( MultiSelectMode.Row );
            SecurityPickerWindow wnd = securityPickerWindow;
            if ( SecurityProvider != null )
            {
                wnd.SecurityProvider = ( SecurityProvider );
            }

            if ( !wnd.ShowModal( this ) )
            {
                return;
            }

            OrdersUI element = new OrdersUI( );
            AddElement( area, element, wnd.SelectedSecurity );
        }

        private void OnAddTradesArea( ChartArea area )
        {
            SecurityPickerWindow securityPickerWindow = new SecurityPickerWindow( );
            securityPickerWindow.SelectionMode = ( MultiSelectMode.Row );
            SecurityPickerWindow wnd = securityPickerWindow;
            if ( SecurityProvider != null )
            {
                wnd.SecurityProvider = ( SecurityProvider );
            }

            if ( !wnd.ShowModal( this ) )
            {
                return;
            }

            TradesUI element = new TradesUI( );
            AddElement( area, element, wnd.SelectedSecurity );
        }

        private void OnRemoveElement( IChartElement element )
        {
            if ( element is IndicatorUI indicator && indicator.ParentElement != null )
            {
                element = indicator.ParentElement;
            }

            ( ( IChart )this ).RemoveElement( element.ChartArea, element );
        }



        private void RaiseUnsubscribeElementEvent( IChartElement element )
        {
            UnSubscribeElement?.Invoke( element );
        }

        void IChart.InvokeAnnotationCreatedEvent( AnnotationUI annotation )
        {
            AnnotationCreated?.Invoke( annotation );
        }

        void IChart.InvokeAnnotationModifiedEvent( AnnotationUI a, ChartDrawDataEx.sAnnotation d )
        {
            AnnotationModified?.Invoke( a, d );
        }

        void IChart.InvokeAnnotationSelectedEvent( AnnotationUI a, ChartDrawDataEx.sAnnotation d )
        {
            AnnotationSelected?.Invoke( a, d );
        }

        void IChart.InvokeAnnotationDeletedEvent( AnnotationUI a )
        {
            AnnotationDeleted?.Invoke( a );
        }

        private sealed class ChartAreasList : BaseList<ChartArea>, IPersistable
        {
            private readonly Chart _chart;

            public ChartAreasList( Chart chart_1 )
            {
                Chart chart = chart_1;
                if ( chart == null )
                {
                    throw new ArgumentNullException( "chart" );
                }

                _chart = chart;
            }

            protected override bool OnAdding( ChartArea newArea )
            {
                XamlHelper.EnsureUIThread( _chart );

                if ( newArea.Chart!= null )
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
                    newArea.XAxisType = _chart.XAxisType;
                }
                else if ( newArea.XAxisType != _chart.XAxisType )
                {
                    throw new InvalidOperationException( LocalizedStrings.InvalidAxisType );
                }

                return base.OnAdding( newArea );
            }

            protected override void OnAdded( ChartArea area )
            {
                foreach ( ChartAxis xAxis in area.XAxises )
                {
                    xAxis.AutoRange = _chart.IsAutoRange;
                }

                area.PropertyChanged += new PropertyChangedEventHandler( ChartAreaHeightPropertyChanged );
                base.OnAdded( area );
            }

            protected override void OnRemoved( ChartArea area )
            {
                XamlHelper.EnsureUIThread( _chart );
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

                chartArea.ChartSurfaceViewModel.Height = chartArea.Height;
            }

            public void DrawChartAreas( ChartDrawDataEx drawData )
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
                    chartArea.ChartSurfaceViewModel.Draw( drawData );
                }
            }

            public void ResetChartAreas( IChartElement[ ] element )
            {
                foreach ( ChartArea chartArea in this )
                {
                    chartArea.ChartSurfaceViewModel.Reset( element );
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
                    ChartArea chartArea             = new ChartArea( );
                    var viewModel                   = new ScichartSurfaceMVVM( chartArea );
                    chartArea.ChartSurfaceViewModel = viewModel;

                    chartArea.Load( storage );

                    foreach ( IChartElement element in chartArea.Elements )
                    {
                        object candle = Ecng.Collections.CollectionHelper.TryGetValue( candleSeries, element.Id );
                        
                        if ( candle != null )
                        {
                            _chart._uiDatasource.Add( element, candle );
                        }

                        if ( element is IndicatorUI key )
                        {

                            IIndicator iindicator = Ecng.Collections.CollectionHelper.TryGetValue( indicatorSeries, key.Id );
                            
                            if ( candle != null )
                            {
                                _chart._indicators.Add( key, iindicator );
                            }
                        }
                    }
                    Add( chartArea );
                    chartArea.ChartSurfaceViewModel.Height = storage.GetValue( "Height", double.NaN );
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
                    s.SetValue( "Height", a.ChartSurfaceViewModel.Height );
                    return s;
                } ).ToArray( ) );
            }

            
        }
    }
}
