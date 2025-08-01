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

using StockSharp.Xaml.Charting.Definitions;
using StockSharp.Xaml;
using DevExpress.Mvvm.Native;
using StockSharp.Charting;

#pragma warning disable CA1416

namespace StockSharp.Xaml.Charting
{
    public partial class Chart : UserControl, INotifyPropertyChanged, IComponentConnector, IPersistable, INotifyPropertyChangedEx, IChartEx, IThemeableChart
    {
        private readonly SynchronizedDictionary<ChartIndicatorElement, IIndicator> _indicators = new SynchronizedDictionary<ChartIndicatorElement, IIndicator>();
        private readonly SynchronizedDictionary<IChartElement, object> _uiDatasource = new SynchronizedDictionary<IChartElement, object>();
        private readonly CachedSynchronizedList<IChartElement> _uiList = new CachedSynchronizedList<IChartElement>();

        private static int staticChartCount;

        private readonly int _instanceCount = ++staticChartCount;
        private bool _isAutoScroll = true;
        private bool _crossHairAxisLabels = true;
        private bool _crossHair = true;
        private ChartAxisType _xAxisType = ChartAxisType.CategoryDateTime;
        private TimeSpan _autoRangeInterval = TimeSpan.FromMilliseconds(200.0);
        private TimeZoneInfo _timeZoneInfo = TimeZoneInfo.Local;

        private CandleSeries _candleSeries;
        private readonly ChartViewModel _chartViewModel;
        private readonly ChartAreasList _chartAreas;
        private ChartAnnotationTypes _annotationTypes;
        private bool _crossHairTooltip;
        private bool _orderCreationMode;
        private bool _isAutoRange;
        private bool _autoRangeByAnnotations;
        private bool _showPerfStats;
        private ISecurityProvider _securityProvider;
        private bool _disableIndicatorReset;
        private PropertyChangedEventHandler _propertyChangedEventHandler;

        static Chart()
        {
            LicenseManager.CreateInstance();
        }

        public Chart()
        {
            InitializeComponent();

            _chartAreas          = new ChartAreasList(this);
            DataContext          = _chartViewModel = new ChartViewModel();

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
        private void OnAddCandlesArea(ChartArea candleArea)
        {
            ChartCandleElement candleUI = new ChartCandleElement();

            if ( _candleSeries == null )
            {
                var candleSeries = new CandleSeries();
                candleSeries.CandleType = ( typeof(TimeFrameCandle) );
                candleSeries.Arg = TimeSpan.FromMinutes(5.0);
                _candleSeries = candleSeries;
            }

            CandleSettingsWindow wnd = new CandleSettingsWindow()
            {
                Series = _candleSeries
            };

            if ( SecurityProvider != null )
            {
                wnd.SecurityProvider = SecurityProvider;
            }

            if ( !wnd.ShowModal(this) )
            {
                return;
            }

            CandleSeries userSelectedSeries = wnd.Series;
            _candleSeries = userSelectedSeries.Clone();
            _candleSeries.Security = null;

            AddElement(candleArea, candleUI, userSelectedSeries);
            userSelectedSeries.PropertyChanged += new PropertyChangedEventHandler(OnCandleSeriesPropertyChanged);
        }

        private void OnCodingAddCandles(object sender, AddCandlesEventArgs e)
        {
            ChartCandleElement candleUI = null;
            if ( e.UseFifo )
            {
                candleUI = new ChartCandleElement() { FifoCapacity = e.FifoCapcity };
            }
            else
            {
                candleUI = new ChartCandleElement();
            }


            _candleSeries          = e.CandleSerie.Clone();
            _candleSeries.Security = null;

            AddElement(e.ChartArea, candleUI, e.CandleSerie);
            e.CandleSerie.PropertyChanged += new PropertyChangedEventHandler(OnCandleSeriesPropertyChanged);
        }

        #endregion


        private bool Areas_Clearing()
        {
            throw new NotImplementedException();
        }

        internal int InstanceCount()
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
                this.Notify(nameof(IsAutoScroll));
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
                    Ecng.Collections.CollectionHelper.ForEach(area.XAxises, ax => ax.AutoRange = value);

                }

                this.Notify(nameof(IsAutoRange));
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

                if ( ChartAreas.SelectMany(a => a.Elements).Any() )
                {
                    throw new InvalidOperationException(LocalizedStrings.AxisTypeCantBeSet);
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
                    throw new ArgumentOutOfRangeException(nameof(AutoRangeInterval));
                }

                _autoRangeInterval = value;
                this.Notify(nameof(AutoRangeInterval));
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

        public void AddArea(ChartArea area)
        {
            this.GuiAsync(() => _chartAreas.Add(area));
        }

        public void RemoveArea(ChartArea area)
        {
            this.GuiAsync(() => _chartAreas.Remove(area));
        }

        public void ClearAreas()
        {
            this.GuiAsync(() => _chartAreas.Clear());
        }

        public IEnumerable<IChartElement> Elements
        {
            get
            {
                return _uiList.Cache;
            }
        }

        public void AddElement(ChartArea area, IChartElement element)
        {
            if ( area == null )
            {
                throw new ArgumentNullException(nameof(area));
            }

            if ( element == null )
            {
                throw new ArgumentNullException(nameof(element));
            }

            this.GuiAsync(() => area.Elements.Add(element));
        }

        public void AddElement(ChartArea area, ChartCandleElement element, CandleSeries candleSeries)
        {
            if ( area == null )
            {
                throw new ArgumentNullException(nameof(area));
            }

            if ( element == null )
            {
                throw new ArgumentNullException(nameof(element));
            }

            if ( candleSeries == null )
            {
                throw new ArgumentNullException(nameof(candleSeries));
            }

            _uiDatasource.Add(element, candleSeries);

            if ( element.Title.IsEmpty() )
            {
                element.Title = candleSeries.Title();
            }

            AddElement(area, element);
        }

        public void AddElement(ChartArea area, ChartIndicatorElement indicatorUI, CandleSeries candleSeries, IIndicator indicator)
        {
            if ( area == null )
            {
                throw new ArgumentNullException(nameof(area));
            }

            if ( indicatorUI == null )
            {
                throw new ArgumentNullException(nameof(indicatorUI));
            }

            if ( candleSeries == null )
            {
                throw new ArgumentNullException(nameof(candleSeries));
            }

            if ( indicator == null )
            {
                throw new ArgumentNullException(nameof(indicator));
            }

            _uiDatasource.Add(indicatorUI, candleSeries);

            _indicators.Add(indicatorUI, indicator);

            if ( !DisableIndicatorReset )
            {
                indicator.Reseted += () => OnIndicatorReset(indicatorUI, indicator);
            }

            if ( StringHelper.IsEmpty(indicatorUI.FullTitle) )
            {
                indicatorUI.FullTitle = indicator.ToString();
            }

            indicatorUI.CreateIndicatorPainter(IndicatorTypes, indicator);
            AddElement(area, indicatorUI);
        }

        public void AddElement(ChartArea area, OrdersUI element, Security security)
        {
            if ( area == null )
            {
                throw new ArgumentNullException(nameof(area));
            }

            if ( element == null )
            {
                throw new ArgumentNullException(nameof(element));
            }

            if ( security == null )
            {
                throw new ArgumentNullException(nameof(security));
            }

            _uiDatasource.Add(element, security);

            if ( element.FullTitle.IsEmpty() )
            {
                element.FullTitle = "{0} ({1})".Put(security.Code, element.GetType().GetDisplayName(null).ToLower());
            }

            AddElement(area, element);
        }

        public void AddElement(ChartArea area, TradesUI element, Security security)
        {
            if ( area == null )
            {
                throw new ArgumentNullException(nameof(area));
            }

            if ( element == null )
            {
                throw new ArgumentNullException(nameof(element));
            }

            if ( security == null )
            {
                throw new ArgumentNullException(nameof(security));
            }

            _uiDatasource.Add(element, security);

            if ( element.FullTitle.IsEmpty() )
            {
                element.FullTitle = "{0} ({1})".Put(security.Code, element.GetType().GetDisplayName(null).ToLower());
            }

            AddElement(area, element);
        }

        void IChartEx.RemoveElement(ChartArea area, IChartElement element)
        {
            if ( area == null )
            {
                throw new ArgumentNullException(nameof(area));
            }

            if ( element == null )
            {
                throw new ArgumentNullException(nameof(element));
            }

            ChartIndicatorElement indicator = element as ChartIndicatorElement;
            if ( indicator != null )
            {
                _indicators.Remove(indicator);
            }

            this.GuiAsync(() => area.Elements.Remove(element));

            _uiDatasource.Remove(element);
        }

        public IIndicator GetIndicator(ChartIndicatorElement element)
        {
            return Ecng.Collections.CollectionHelper.TryGetValue(_indicators, element);

        }

        public object GetSource(IChartElement element)
        {
            return Ecng.Collections.CollectionHelper.TryGetValue(_uiDatasource, element);

        }

        private T GetSeries<T>(IChartElement element) where T : class
        {
            return (T)GetSource(element);
        }

        public void SetSource(IChartElement element, object source)
        {
            _uiDatasource[element] = source;
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
                this.Notify(nameof(AutoRangeByAnnotations));
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
                this.Notify(nameof(ShowPerfStats));
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
                this.Notify(nameof(CrossHair));
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
                this.Notify(nameof(CrossHairTooltip));
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
                this.Notify(nameof(CrossHairAxisLabels));
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
                this.Notify(nameof(AnnotationType));
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
                this.Notify(nameof(OrderCreationMode));
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
                    throw new ArgumentNullException(nameof(value));
                }

                _timeZoneInfo = timeZoneInfo;
                this.Notify(nameof(TimeZone));
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
                throw new NotImplementedException();
                //ViewModel.AreaAddedEvent += value;
            }
            remove
            {
                throw new NotImplementedException();
                //ViewModel.AreaAddedEvent -= value;
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

        public event Action<ChartAnnotation> AnnotationCreated;

        public event Action<ChartAnnotation, ChartDrawData.AnnotationData> AnnotationModified;

        public event Action<ChartAnnotation> AnnotationDeleted;

        public event Action<ChartAnnotation, ChartDrawData.AnnotationData> AnnotationSelected;

        public event Action<ChartCandleElement, CandleSeries> SubscribeCandleElement;

        public event Action<ChartIndicatorElement, CandleSeries, IIndicator> SubscribeIndicatorElement;

        public event Action<OrdersUI, Security> SubscribeOrderElement;

        public event Action<TradesUI, Security> SubscribeTradeElement;

        public event Action<IChartElement> UnSubscribeElement;

        public void Reset(IEnumerable<IChartElement> elements)
        {
            _chartAreas.ResetChartAreas(elements.ToArray());
        }

        public void Draw(ChartDrawData data)
        {
            /* -------------------------------------------------------------------------------------------------------------------------------------------
            * 
            *  Step 7----------> 8 The Single Candle is passed to DrawChartAreas
            *                           
            * ------------------------------------------------------------------------------------------------------------------------------------------- 
            */
            _chartAreas.DrawChartAreas(data);
        }

        internal void InvokeCreateOrderEvent(ChartArea area, Order order)
        {
            CreateOrder?.Invoke(area, order);
        }

        internal void InvokeMoveOrderEvent(Order order, Decimal valueOne)
        {
            MoveOrder?.Invoke(order, valueOne);
        }

        internal void InvokeCancelOrderEvent(Order order)
        {
            CancelOrder?.Invoke(order);
        }



        public TimeZoneInfo GetTimeZone()
        {
            return ChartAreas.Select(a => a.XAxises.FirstOrDefault(i => i.TimeZone != null)).LastOrDefault(ax => ax != null)?.TimeZone;
        }

        public void Load(SettingsStorage storage)
        {
            IsAutoScroll           = storage.GetValue("IsAutoScroll", IsAutoScroll);
            IsAutoRange            = storage.GetValue("IsAutoRange", IsAutoRange);
            XAxisType              = storage.GetValue("XAxisType", ChartAxisType.CategoryDateTime);
            AutoRangeByAnnotations = storage.GetValue("AutoRangeByAnnotations", AutoRangeByAnnotations);
            ShowOverview           = storage.GetValue("ShowOverview", ShowOverview);
            ShowLegend             = storage.GetValue("ShowLegend", ShowLegend);
            CrossHair              = storage.GetValue("CrossHair", CrossHair);
            CrossHairTooltip       = storage.GetValue("CrossHairTooltip", CrossHairTooltip);
            CrossHairAxisLabels    = storage.GetValue("CrossHairAxisLabels", CrossHairAxisLabels);
            OrderCreationMode      = storage.GetValue("OrderCreationMode", OrderCreationMode);
            TimeZone               = MayBe.With(storage.GetValue<string>("TimeZone", null), new Func<string, TimeZoneInfo>(TimeZoneInfo.FindSystemTimeZoneById)) ?? TimeZoneInfo.Local;

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
            var candleSeries = GetCandleSeries(storage.GetValue<SettingsStorage>("Sources", null));
            var indicatorSeries = GetIndicators(storage.GetValue<SettingsStorage>("Indicators", null));
            _chartAreas.GetAreas(storage.GetValue<SettingsStorage>("Areas", null), candleSeries, indicatorSeries);
        }

        public void Save(SettingsStorage storage)
        {
            storage.SetValue("IsAutoScroll", IsAutoScroll);
            storage.SetValue("IsAutoRange", IsAutoRange);
            storage.SetValue("XAxisType", XAxisType);
            storage.SetValue("AutoRangeByAnnotations", AutoRangeByAnnotations);
            storage.SetValue("ShowOverview", ShowOverview);
            storage.SetValue("ShowLegend", ShowLegend);
            storage.SetValue("CrossHair", CrossHair);
            storage.SetValue("CrossHairTooltip", CrossHairTooltip);
            storage.SetValue("CrossHairAxisLabels", CrossHairAxisLabels);
            storage.SetValue("OrderCreationMode", OrderCreationMode);
            storage.SetValue("TimeZone", TimeZone?.Id);

            if ( !IsInteracted )
            {
                return;
            }

            storage.SetValue("Sources", SaveCandleSeries());
            storage.SetValue("Indicators", SaveIndicators());
            storage.SetValue("Areas", _chartAreas.Save());
        }

        public void ReSubscribeElements()
        {
            if ( !IsInteracted )
            {
                return;
            }

            foreach ( IChartElement chartElement in Elements )
            {
                RemoveAndRaiseUnsubscribeElementEvent(chartElement);
                AddElement(chartElement);
            }
        }

        private void OnCandleSeriesPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if ( !IsInteracted )
            {
                return;
            }

            var candleSeries = (CandleSeries)sender;

            foreach ( IChartElement element in Elements.Where(x => GetSource(x) == candleSeries).ToArray() )
            {
                var ChartCandleElement = element as ChartCandleElement;

                if ( ChartCandleElement != null )
                {
                    ChartCandleElement.Title = candleSeries.Title();
                }

                ResetElement(element, true);
            }
        }

        private SettingsStorage SaveCandleSeries()
        {
            SettingsStorage storage = new SettingsStorage();
            PooledDictionary<CandleSeries, Guid> dictionary = new PooledDictionary<CandleSeries, Guid>();

            foreach ( var pair in _uiDatasource.SyncGet(d => d.ToArray()) )
            {
                string elementGuid = pair.Key.Id.ToString();

                if ( pair.Value is CandleSeries key )
                {
                    Guid guid = dictionary.SafeAdd(key, ( s => Guid.NewGuid() ));
                    storage.SetValue(elementGuid, guid);
                }
                else
                {
                    storage.SetValue(elementGuid, ( (Security)pair.Value ).Id);
                }
            }

            if ( dictionary.Count > 0 )
            {
                SettingsStorage candleSeriesStorage = new SettingsStorage();

                foreach ( KeyValuePair<CandleSeries, Guid> candleSeries in dictionary )
                {
                    // This setting is the reverse of the above setting, it saves the CandleSeries map to Guid 
                    candleSeriesStorage.Add(candleSeries.Value.ToString(), candleSeries.Key.Save());
                }
                storage.SetValue("CandleSeries", candleSeriesStorage);
            }
            return storage;
        }

        private IDictionary<Guid, object> GetCandleSeries(SettingsStorage settings)
        {
            PooledDictionary<Guid, object> output = new PooledDictionary<Guid, object>();
            if ( settings == null )
            {
                return output;
            }

            var candleSeriesMap = new PooledDictionary<Guid, CandleSeries>();
            var candleStorage = settings.GetValue<SettingsStorage>("CandleSeries", null);

            if ( candleStorage != null )
            {
                foreach ( KeyValuePair<string, object> keyValuePair in candleStorage )
                {
                    Guid key = keyValuePair.Key.To<Guid>();
                    CandleSeries candleSeries = ( (SettingsStorage)keyValuePair.Value ).Load<CandleSeries>();
                    candleSeries.PropertyChanged += new PropertyChangedEventHandler(OnCandleSeriesPropertyChanged);
                    candleSeriesMap.Add(key, candleSeries);
                }
            }

            foreach ( KeyValuePair<string, object> keyValuePair in settings )
            {
                if ( !( keyValuePair.Key == "CandleSeries" ) )
                {
                    object obj;
                    if ( keyValuePair.Value is SettingsStorage storage )
                    {
                        CandleSeries candleSeries = storage.Load<CandleSeries>();
                        candleSeries.PropertyChanged += new PropertyChangedEventHandler(OnCandleSeriesPropertyChanged);
                        obj = candleSeries;
                    }
                    else
                    {
                        obj = !( keyValuePair.Value is Guid index )
                            ? ( SecurityProvider ?? ServicesRegistry.SecurityProvider ).LookupById((string)keyValuePair.Value)
                            : (object)candleSeriesMap[index];
                    }

                    output.Add(keyValuePair.Key.To<Guid>(), obj);
                }
            }
            return output;
        }

        private SettingsStorage SaveIndicators()
        {
            SettingsStorage settingsStorage = new SettingsStorage();

            foreach ( var pair in _indicators.SyncGet(d => d.ToArray()) )
            {
                settingsStorage.SetValue(pair.Key.Id.ToString(), pair.Value.SaveEntire(false));
            }

            return settingsStorage;
        }

        private static IDictionary<Guid, IIndicator> GetIndicators(SettingsStorage settings)
        {
            PooledDictionary<Guid, IIndicator> dictionary = new PooledDictionary<Guid, IIndicator>();
            if ( settings == null )
            {
                return dictionary;
            }

            foreach ( KeyValuePair<string, object> keyValuePair in settings )
            {
                dictionary.Add(keyValuePair.Key.To<Guid>(), ( (SettingsStorage)keyValuePair.Value ).LoadEntire<IIndicator>());
            }

            return dictionary;
        }

        private void AddElement(IChartElement element)
        {
            if ( GetSource(element) == null )
            {
                return;
            }

            _uiList.Add(element);
            RaiseChartElementSubscribedEvent(element);
        }

        private void RaiseChartElementSubscribedEvent(IChartElement chartElement)
        {
            switch ( chartElement )
            {
                case ChartCandleElement ChartCandleElement:
                    {
                        /* -------------------------------------------------------------------------------------------------------------------------------------------
                        *  When we clicked on the Add Candle Command from the context menu, the following function get executed.
                        *  
                        *  Step A ----------> 4 After Candle get added to the UI PooledList, we raise Chart Element Subscribed Event.
                        * 
                        * ------------------------------------------------------------------------------------------------------------------------------------------- 
                        */
                        SubscribeCandleElement?.Invoke(ChartCandleElement, GetSeries<CandleSeries>(ChartCandleElement));
                    }
                    break;

                case ChartIndicatorElement element:
                    {
                        SubscribeIndicatorElement?.Invoke(element, GetSeries<CandleSeries>(element), GetIndicator(element));
                    }
                    break;

                case OrdersUI chartOrderElement:
                    {
                        SubscribeOrderElement?.Invoke(chartOrderElement, GetSeries<Security>(chartOrderElement));
                    }
                    break;

                case TradesUI chartTradeElement:
                    {
                        SubscribeTradeElement?.Invoke(chartTradeElement, GetSeries<Security>(chartTradeElement));
                    }
                    break;
            }
        }

        private void RemoveAndRaiseUnsubscribeElementEvent(IChartElement element)
        {
            if ( GetSource(element) == null )
            {
                return;
            }

            _uiList.Remove(element);

            UnSubscribeElement?.Invoke(element);
        }

        private void OnNewAreaAddedToChartArea(ChartArea area)
        {
            area.Elements.Added   += new Action<IChartElement>(OnNewUIAddedToArea);
            area.Elements.Removed += new Action<IChartElement>(OnUIRemovedFromArea);

            throw new NotImplementedException();
            //area.Chart = ( this );

            //ViewModel.ScichartSurfaceViewModels.Add( ( ScichartSurfaceMVVM ) area.ViewModel );

            //Ecng.Collections.CollectionHelper.ForEach( area.Elements, new Action<IChartElement>( OnNewUIAddedToArea ) );

        }

        private void OnAreaRemovedFromChartArea(ChartArea area)
        {
            area.Elements.Added   -= new Action<IChartElement>(OnNewUIAddedToArea);
            area.Elements.Removed -= new Action<IChartElement>(OnUIRemovedFromArea);
            //ViewModel.ScichartSurfaceViewModels.Remove( ( ScichartSurfaceMVVM ) area.ViewModel );

            Ecng.Collections.CollectionHelper.ForEach(area.Elements, new Action<IChartElement>(OnUIRemovedFromArea));


            area.Chart= null;
            area.Dispose();
        }

        private bool OnClearChartArea()
        {
            foreach ( ChartArea area in ChartAreas )
            {
                OnAreaRemovedFromChartArea(area);
            }

            ViewModel.InitRangeDepProperty();
            return true;
        }

        /* -------------------------------------------------------------------------------------------------------------------------------------------
        *  When we clicked on the Add Candle Command from the context menu, the following function get executed.
        *  
        *  Step A ----------> 3 Now that NotifyList OnAdded Event has been invoked.
        * 
        * ------------------------------------------------------------------------------------------------------------------------------------------- 
        */
        private void OnNewUIAddedToArea(IChartElement element)
        {
            if ( !IsInteracted )
            {
                return;
            }

            AddElement(element);
        }

        private void OnUIRemovedFromArea(IChartElement element)
        {
            ResetElement(element, false);
        }

        private void ResetElement(IChartElement element, bool needAddElement)
        {
            IChartElement[] elementArray;
            if ( element is ChartCandleElement )
            {
                elementArray = Elements.Where(e => GetSource(e) == GetSeries<CandleSeries>(element)).ToArray();
            }
            else
            {
                elementArray = new IChartElement[1]
                {
                    element
                };
            }

            if ( needAddElement )
            {
                if ( IsInteracted )
                {
                    Ecng.Collections.CollectionHelper.ForEach(elementArray, new Action<IChartElement>(RaiseUnsubscribeElementEvent));
                }

                Reset(elementArray);

                if ( !IsInteracted )
                {
                    return;
                }


                Ecng.Collections.CollectionHelper.ForEach(elementArray, new Action<IChartElement>(RaiseChartElementSubscribedEvent));

            }
            else
            {
                if ( !IsInteracted )
                {
                    return;
                }

                Ecng.Collections.CollectionHelper.ForEach(elementArray, new Action<IChartElement>(RemoveAndRaiseUnsubscribeElementEvent));


            }
        }

        internal static void SetupScichartSurface(SciChartSurface chartSurface)
        {
            chartSurface.DebugWhyDoesntSciChartRender = true;

            if ( chartSurface.DataContext != null )
            {
                ( (ScichartSurfaceMVVM)chartSurface.DataContext ).SetScichartSurface(chartSurface);
            }
            else
            {
                chartSurface.DataContextChanged += (s, e) =>
                {
                    ( (ScichartSurfaceMVVM)chartSurface.DataContext ).SetScichartSurface(chartSurface);
                };
            }
        }

        private void OnInitialized(object sender, EventArgs e)
        {
            SetupScichartSurface((SciChartSurface)sender);
        }

        private void OnIndicatorReset(ChartIndicatorElement indicatorElement, IIndicator indicator)
        {
            indicatorElement.FullTitle = indicator.ToString();
            ResetElement(indicatorElement, true);
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


        void INotifyPropertyChangedEx.NotifyPropertyChanged(string string_0)
        {
            PropertyChangedEventHandler changedEventHandler0 = _propertyChangedEventHandler;
            if ( changedEventHandler0 == null )
            {
                return;
            }

            changedEventHandler0.Invoke(this, string_0);
        }


        private void OnAreaAdded()
        {
            ChartArea area = new ChartArea() { Title = LocalizedStrings.Panel + " " +   ( ChartAreas.Count + 1 ), XAxisType = XAxisType };
            var viewModel = new ScichartSurfaceMVVM(area);
            area.ViewModel = viewModel;

            var timeZoneInfo = GetTimeZone();

            foreach ( ChartAxis xAxis in area.XAxises )
            {
                xAxis.TimeZone = timeZoneInfo;
            }

            AddArea(area);
        }


        private void OnAddIndicatorArea(ChartArea area)
        {
            IndicatorPickerWindow indicatorPicker = new IndicatorPickerWindow()
            {
                AutoSelectCandles = true,
                IndicatorTypes = IndicatorTypes
            };

            if ( !indicatorPicker.ShowModal(this) )
            {
                return;
            }

            ChartCandleElement[] array = Elements.OfType<ChartCandleElement>().ToArray();
            ChartCandleElement ChartCandleElement = area.Elements.OfType<ChartCandleElement>().Concat(array).FirstOrDefault();

            if ( ChartCandleElement == null )
            {
                new MessageBoxBuilder().Error().Text(LocalizedStrings.CandleStick).Owner(this).Show();
            }
            else
            {
                if ( !indicatorPicker.AutoSelectCandles )
                {
                    ChartCandleElementViewModelPicker wnd2 = new ChartCandleElementViewModelPicker()
                    {
                        Elements = array,
                        SelectedElement = ChartCandleElement
                    };

                    if ( !wnd2.ShowModal(this) )
                    {
                        return;
                    }

                    ChartCandleElement = wnd2.SelectedElement;
                }

                /* ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
                 * 
                 * Tony Indicator Step 1: The above code will show the indicator Window and let us select the wanted indicator and the following will create the Indicator and the 
                 *                  corresponding indicator painter.
                 *                  
                 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------                 
                 */
                var indicatorUI = new ChartIndicatorElement();

                var indicatorPainter = indicatorPicker.SelectedIndicatorType.CreatePainter();

                indicatorUI.IndicatorPainter =  indicatorPainter;

                var tonyCandleSeries = GetSeries<CandleSeries>(ChartCandleElement);


                AddElement(area, indicatorUI, tonyCandleSeries, indicatorPicker.Indicator);
            }
        }

        private void OnAddOrdersArea(ChartArea area)
        {
            SecurityPickerWindow securityPickerWindow = new SecurityPickerWindow();
            securityPickerWindow.SelectionMode = ( MultiSelectMode.Row );
            SecurityPickerWindow wnd = securityPickerWindow;
            if ( SecurityProvider != null )
            {
                wnd.SecurityProvider = ( SecurityProvider );
            }

            if ( !wnd.ShowModal(this) )
            {
                return;
            }

            OrdersUI element = new OrdersUI();
            AddElement(area, element, wnd.SelectedSecurity);
        }

        private void OnAddTradesArea(ChartArea area)
        {
            SecurityPickerWindow securityPickerWindow = new SecurityPickerWindow();
            securityPickerWindow.SelectionMode = ( MultiSelectMode.Row );
            SecurityPickerWindow wnd = securityPickerWindow;
            if ( SecurityProvider != null )
            {
                wnd.SecurityProvider = ( SecurityProvider );
            }

            if ( !wnd.ShowModal(this) )
            {
                return;
            }

            TradesUI element = new TradesUI();
            AddElement(area, element, wnd.SelectedSecurity);
        }

        private void OnRemoveElement(IChartElement element)
        {
            if ( element is ChartIndicatorElement indicator && indicator.ParentElement != null )
            {
                element = indicator.ParentElement;
            }

            ( (IChartEx)this ).RemoveElement((ChartArea)element.ChartArea, element);
        }



        private void RaiseUnsubscribeElementEvent(IChartElement element)
        {
            UnSubscribeElement?.Invoke(element);
        }

        void IChartEx.InvokeAnnotationCreatedEvent(ChartAnnotation annotation)
        {
            AnnotationCreated?.Invoke(annotation);
        }

        void IChartEx.InvokeAnnotationModifiedEvent(ChartAnnotation a, ChartDrawData.AnnotationData d)
        {
            AnnotationModified?.Invoke(a, d);
        }

        void IChartEx.InvokeAnnotationSelectedEvent(ChartAnnotation a, ChartDrawData.AnnotationData d)
        {
            AnnotationSelected?.Invoke(a, d);
        }

        void IChartEx.InvokeAnnotationDeletedEvent(ChartAnnotation a)
        {
            AnnotationDeleted?.Invoke(a);
        }

        private sealed class ChartAreasList : BaseList<ChartArea>, IPersistable
        {
            private readonly Chart _chart;

            public ChartAreasList(Chart chart_1)
            {
                Chart chart = chart_1;
                if ( chart == null )
                {
                    throw new ArgumentNullException("chart");
                }

                _chart = chart;
            }

            protected override bool OnAdding(ChartArea newArea)
            {
                XamlHelper.EnsureUIThread(_chart);

                if ( newArea.Chart!= null )
                {
                    //lock ( newArea.GetStackTrace( ).SyncRoot )
                    //{
                    //    ;
                    //}

                    throw new ArgumentException("area");
                }
                if ( newArea == null || Contains(newArea) )
                {
                    throw new ArgumentException("area2");
                }

                if ( newArea.Elements.IsEmpty() )
                {
                    newArea.XAxisType = _chart.XAxisType;
                }
                else if ( newArea.XAxisType != _chart.XAxisType )
                {
                    throw new InvalidOperationException(LocalizedStrings.InvalidAxisType);
                }

                return base.OnAdding(newArea);
            }

            protected override void OnAdded(ChartArea area)
            {
                foreach ( ChartAxis xAxis in area.XAxises )
                {
                    xAxis.AutoRange = _chart.IsAutoRange;
                }

                area.PropertyChanged += new PropertyChangedEventHandler(ChartAreaHeightPropertyChanged);
                base.OnAdded(area);
            }

            protected override void OnRemoved(ChartArea area)
            {
                XamlHelper.EnsureUIThread(_chart);
                area.PropertyChanged -= new PropertyChangedEventHandler(ChartAreaHeightPropertyChanged);
                base.OnRemoved(area);
            }

            private void ChartAreaHeightPropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                ChartArea chartArea = (ChartArea)sender;
                if ( !( e.PropertyName == "Height" ) )
                {
                    return;
                }

                var vm = (ScichartSurfaceMVVM)chartArea.ViewModel;

                vm.Height = chartArea.Height;
            }

            public void DrawChartAreas(ChartDrawData drawData)
            {
                if ( drawData == null )
                {
                    throw new ArgumentNullException("data");
                }

                /* -------------------------------------------------------------------------------------------------------------------------------------------
                * 
                *  Step 7----------> 9 All the Scichart Area will use the candle to draw
                *                           
                * ------------------------------------------------------------------------------------------------------------------------------------------- 
                */
                foreach ( ChartArea chartArea in this )
                {
                    var vm = (ScichartSurfaceMVVM)chartArea.ViewModel;
                    vm.Draw(drawData);
                }
            }

            public void ResetChartAreas(IChartElement[] element)
            {
                foreach ( ChartArea chartArea in this )
                {
                    var vm = (ScichartSurfaceMVVM)chartArea.ViewModel;
                    vm.Reset(element);
                }
            }

            /// <summary>
            /// Tony: The following function will try to DeSerialize and reStore the ChartAreas with Candles and Indicators
            /// </summary>
            /// <param name="settings"></param>
            /// <param name="candleSeries"></param>
            /// <param name="indicatorSeries"></param>
            public void GetAreas(SettingsStorage settings,
                                  IDictionary<Guid, object> candleSeries,
                                  IDictionary<Guid, IIndicator> indicatorSeries)
            {
                Clear();

                foreach ( SettingsStorage storage in settings.GetValue<IEnumerable<SettingsStorage>>("Areas", null) )
                {
                    ChartArea chartArea = new ChartArea();
                    var viewModel = new ScichartSurfaceMVVM(chartArea);
                    chartArea.ViewModel = viewModel;

                    chartArea.Load(storage);

                    foreach ( IChartElement element in chartArea.Elements )
                    {
                        object candle = Ecng.Collections.CollectionHelper.TryGetValue(candleSeries, element.Id);

                        if ( candle != null )
                        {
                            _chart._uiDatasource.Add(element, candle);
                        }

                        if ( element is ChartIndicatorElement key )
                        {

                            IIndicator iindicator = Ecng.Collections.CollectionHelper.TryGetValue(indicatorSeries, key.Id);

                            if ( candle != null )
                            {
                                _chart._indicators.Add(key, iindicator);
                            }
                        }
                    }
                    Add(chartArea);

                    var vm = (ScichartSurfaceMVVM)chartArea.ViewModel;

                    vm.Height = storage.GetValue("Height", double.NaN);
                }
            }

            void IPersistable.Load(SettingsStorage settings)
            {
                throw new NotSupportedException();
            }

            public void Save(SettingsStorage settings)
            {
                settings.SetValue("Areas", this.Select(a =>
                {
                    var s = a.Save();

                    var vm = (ScichartSurfaceMVVM)a.ViewModel;

                    s.SetValue("Height", vm.Height);
                    return s;
                }).ToArray());
            }


        }
    }
}


//using DevExpress.Xpf.Grid;
//using Ecng.Collections;
//using Ecng.Common;
//using Ecng.ComponentModel;
//using Ecng.Configuration;
//using Ecng.Serialization;
//using Ecng.Xaml;
//using SciChart.Data.Model;
//using StockSharp.Algo;
//using StockSharp.Algo.Indicators;
//using StockSharp.BusinessEntities;
//using StockSharp.Charting;
//using StockSharp.Localization;
//using StockSharp.Messages;
//using System;
//using System.CodeDom.Compiler;
//using System.Collections;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Markup;
//using System.Windows.Threading;
//using System.Xml.Linq;

//#nullable enable
//namespace StockSharp.Xaml.Charting;

///// <summary>The graphic component of the candles charts display.</summary>
///// <summary>Chart</summary>
//public class Chart : UserControl,
//                      INotifyPropertyChanged,
//                      IChart,
//                      IPersistable,  
//                      IChartBuilder,
//                      IThemeableChart,
//                      INotifyPropertyChangedEx,
//                      IWpfChart
//{
//    private sealed class SomeInternalSealedClass98364
//    {
//        public string[] _someStringArray03843;
//        public Chart _variableSome3535;

//        public void OnAreaYAxisesRemoving()
//        {
//            CollectionHelper.ForEach<string>((IEnumerable<string>)this._someStringArray03843, new Action<string>(this._variableSome3535.SomeInternalFunction34382084));
//        }
//    }

//    private sealed class SomeInternalSealedClass897634 : Disposable
//    {

//        private readonly Chart _chart;

//        private readonly IChartIndicatorElement _baseViewModel;

//        private readonly IIndicator _indicator934;

//        public SomeInternalSealedClass897634(
//          Chart _param1,
//          IChartIndicatorElement _param2,
//          IIndicator _param3)
//        {
//            this._chart = _param1 ?? throw new ArgumentNullException("parent");
//            this._baseViewModel = _param2 ?? throw new ArgumentNullException("element");
//            this._indicator934 = _param3 ?? throw new ArgumentNullException("indicator");
//            this.Indicator.Reseted += new Action(this.OnResetCallback);
//        }

//        public IIndicator Indicator => this._indicator934;

//        protected override void DisposeManaged()
//        {
//            base.DisposeManaged();
//            this.Indicator.Reseted -= new Action(this.OnResetCallback);
//        }

//        private void OnResetCallback()
//        {
//            if ( this._chart.DisableIndicatorReset )
//                return;
//            this._chart.SomeMethod03342(this._baseViewModel, this.Indicator);
//        }
//    }


//    private static int staticChartCount;

//    private readonly int _instanceCount = ++Chart.staticChartCount;

//    private readonly ChartBuilder _chartBuilder = new ChartBuilder();

//    private Subscription _subscription;

//    private ChartCandleDrawStyles _chartCandleDrawStyles;

//    private MarketDataMessage _defaultCandlesSettings = new MarketDataMessage()
//    {
//        DataType2 = Extensions.TimeFrame(TimeSpan.FromMinutes(5.0))
//    };

//    private readonly ChartViewModel _chartSurfaceVM;

//    private readonly SynchronizedDictionary<IChartIndicatorElement, Chart.SomeInternalSealedClass897634> _indicatorElementMap = new SynchronizedDictionary<IChartIndicatorElement, Chart.SomeInternalSealedClass897634>();

//    private readonly SynchronizedDictionary<IChartElement, Subscription> _subscrptionMap = new SynchronizedDictionary<IChartElement, Subscription>();

//    private ChartAnnotationTypes _annotationType;

//    private bool _isAutoScroll = true;

//    private bool _crossHairAxisLabels = true;

//    private bool _crossHairTooltip;

//    private bool _crossHair = true;

//    private bool _orderCreationMode;

//    private bool _isAutoRange;

//    private bool _autoRangeByAnnotations;

//    private bool _showPerfStats;

//    private readonly Dictionary<(IChartArea, IndicatorMeasures), string> _area2IndicatorMeasuresMap = new Dictionary<(IChartArea, IndicatorMeasures), string>();

//    private TimeSpan _autoRangeIntervalNoGroup = TimeSpan.FromMilliseconds(200.0);

//    private readonly List<IChartArea> _iChartAreaList = new List<IChartArea>();

//    private ISecurityProvider _securityProvider;

//    private bool _disableIndicatorReset;

//    private TimeZoneInfo _timeZone = TimeZoneInfo.Local;

//    private bool _showNonFormedIndicators;

//    private Security _security;

//    private Subscription _subscription2;

//    private Action<IChartArea> _iChartAreaEvent;

//    private Action _someActionEvent;

//    private PropertyChangedEventHandler PropertyChangedEvent;

//    public SciChartGroup _scichartGroup;




//    /// <summary>
//    /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.Chart" />.
//    /// </summary>
//    public Chart()
//    {
//        Chart.SomeInternalSealedClass98364 _someInstance = new Chart.SomeInternalSealedClass98364();
//        _someInstance._variableSome3535 = this;
//        this.InitializeComponent();
//        this.DataContext = (object)( this._chartSurfaceVM = new ChartViewModel() );
//        this.ViewModel.CancelActiveOrderEvent += ( new Action<Order>(this.InvokeCancelOrderEvent) );
//        this.ViewModel.UngroupEvent += (new Action<ChartArea>(this.OnUngroupEvent));
//        this.AreaAdding += new Action(this.OnAreaAdding);
//        this.AddCandles += new Action<ChartArea>(this.OnAddCandles);
//        this.RebuildCandles += new Action<IChartElement, Subscription>(this.OnRebuildCandles);
//        this.AddIndicator += new Action<ChartArea>(this.OnAddIndicator);
//        this.AddOrders += new Action<ChartArea>(this.OnAddOrders);
//        this.AddTrades += new Action<ChartArea>(this.OnAddTrades);
//        this.RemoveElement += new Action<IChartElement>(this.OnRemoveElement);
//        _someInstance._someStringArray03843 = new string[7]
//        {
//              nameof (IsInteracted),
//              nameof (AllowAddArea),
//              nameof (AllowAddAxis),
//              nameof (AllowAddCandles),
//              nameof (AllowAddIndicators),
//              nameof (AllowAddOrders),
//              nameof (AllowAddOwnTrades)
//        };
//        ChartViewModel.InteractedEvent += (new Action(_someInstance.OnAreaYAxisesRemoving));
//        if ( IChartExtensions.TryIndicatorProvider == null )
//        {
//            IndicatorProvider indicatorProvider = new IndicatorProvider();
//            indicatorProvider.Init();
//            ConfigManager.RegisterService<IIndicatorProvider>((IIndicatorProvider)indicatorProvider);
//        }
//        if ( IChartExtensions.TryIndicatorPainterProvider != null )
//            return;
//        ChartIndicatorPainterProvider indicatorPainterProvider = new ChartIndicatorPainterProvider();
//        ( (IChartIndicatorPainterProvider)indicatorPainterProvider ).Init();
//        ConfigManager.RegisterService<IChartIndicatorPainterProvider>((IChartIndicatorPainterProvider)indicatorPainterProvider);
//    }

//    public int GetInstanceCount() => this._instanceCount;


//    public ChartCandleDrawStyles CandleDrawStyles
//    {
//        get
//        {
//            return this._chartCandleDrawStyles;
//        }

//        set
//        {
//            this._chartCandleDrawStyles = value;
//        }
//    }


//    public MarketDataMessage DefaultCandlesSettings
//    {
//        get => this._defaultCandlesSettings;
//        set
//        {
//            this._defaultCandlesSettings = value ?? throw new ArgumentNullException(nameof(value));
//        }
//    }

//    public ChartViewModel ViewModel
//    {
//        get
//        {
//            return this._chartSurfaceVM;
//        }
//    }

//    public IEnumerable<IChartArea> Areas => (IEnumerable<IChartArea>)this._iChartAreaList;

//    public bool IsAutoScroll
//    {
//        get => this._isAutoScroll;
//        set
//        {
//            this._isAutoScroll = value;
//            NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof(IsAutoScroll));
//        }
//    }

//    public bool IsAutoRange
//    {
//        get => this._isAutoRange;
//        set
//        {
//            this._isAutoRange = value;
//            foreach ( IChartArea area in this.Areas )
//                area.SetAutoRange(value);
//            NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof(IsAutoRange));
//        }
//    }

//    /// <summary>Chart range/scroll interval. Default is 200ms.</summary>
//    public TimeSpan AutoRangeInterval
//    {
//        get => this._autoRangeIntervalNoGroup;
//        set
//        {
//            this._autoRangeIntervalNoGroup = !( value <= TimeSpan.Zero ) ? value : throw new ArgumentOutOfRangeException(nameof(value), (object)value, LocalizedStrings.InvalidValue);
//            NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof(AutoRangeInterval));
//        }
//    }


//    /// <summary>The provider of information about instruments.</summary>
//    public ISecurityProvider SecurityProvider
//    {
//        get => this._securityProvider;
//        set => this._securityProvider = value;
//    }

//    /// <summary>Disable tracking indicator reset.</summary>
//    public bool DisableIndicatorReset
//    {
//        get => this._disableIndicatorReset;
//        set => this._disableIndicatorReset = value;
//    }

//    public void AddArea(IChartArea area)
//    {
//        ( (DispatcherObject)this ).GuiSync(new Action(new Chart.SomeInternalSealedClass08343()

//        {
//            _chartArea_093 = area,
//            _variableSome3535 = this
//        }.Method03));
//    }

//    public void RemoveArea(IChartArea area)
//    {
//        ( (DispatcherObject)this ).GuiSync(new Action(new Chart.SomeInternalClass03850835()


//        {
//            _variableSome3535 = this,
//            _chartArea_093 = area
//        }.Method0833421));
//    }

//    private void OnHeightPropertyChanged(object _param1, PropertyChangedEventArgs _param2)
//    {
//        ChartArea chartArea = (ChartArea)_param1;
//        if ( !( _param2.PropertyName == "Height" ) )
//            return;
//        chartArea.ViewModel.Height = chartArea.Height;
//    }

//    public event Action<IChartArea> AreaAdded;

//    public event Action<IChartArea> AreaRemoved;

//    public void AddElement(IChartArea area, IChartElement element)
//    {
//        if ( area == null )
//        {
//            throw new ArgumentNullException(nameof(area));
//        }

//        if ( element == null )
//        {
//            throw new ArgumentNullException(nameof(element));
//        }

//        Chart.SomeInternalSealedClass3578512 gncgo1okL0HgWiq5JrU = new Chart.SomeInternalSealedClass3578512();
//        gncgo1okL0HgWiq5JrU._someChartElement0343123 = element;
//        gncgo1okL0HgWiq5JrU._variableSome3535 = this;
//        gncgo1okL0HgWiq5JrU._chartArea_093 = area;

//        ( (DispatcherObject)this ).GuiSync(new Action(gncgo1okL0HgWiq5JrU.SomeMethod03843));
//    }

//    private sealed class SomeInternalSealedClass3578512
//    {
//        public IChartElement _someChartElement0343123;
//        public Chart _variableSome3535;
//        public IChartArea _chartArea_093;

//        public void SomeMethod03843()
//        {
//            Chart.SomeInternalSealedClass897634 zZq9Hpf12oRwg;
//            if ( this._someChartElement0343123 is IChartIndicatorElement zI6sZdg && zI6sZdg.AutoAssignYAxis && this._variableSome3535._indicatorElementMap.TryGetValue(zI6sZdg, ref zZq9Hpf12oRwg) && zZq9Hpf12oRwg.Indicator.Measure != IndicatorMeasures.Price )

//            {
//                (IChartArea, IndicatorMeasures) key = (this._chartArea_093, zZq9Hpf12oRwg.Indicator.Measure);
//                string str;
//                if ( !this._variableSome3535._area2IndicatorMeasuresMap.TryGetValue(key, out str) )
//                {
//                    str = $"{"Y"}({Guid.NewGuid()})";
//                    // ISSUE: explicit non-virtual call
//                    IChartAxis axis = ( this._variableSome3535.CreateAxis() );
//                    axis.Id = str;
//                    axis.AxisType = ChartAxisType.Numeric;
//                    ( (ICollection<IChartAxis>)this._chartArea_093.YAxises ).Add(axis);
//                    this._variableSome3535._area2IndicatorMeasuresMap.Add(key, axis.Id);
//                }
//                this._someChartElement0343123.YAxisId = str;
//            }

//          ( (ICollection<IChartElement>)this._chartArea_093.Elements ).Add(this._someChartElement0343123);
//        }
//    }


//    /// <summary>To add an element to the chart.</summary>
//    /// <param name="area">Chart area.</param>
//    /// <param name="element">The chart element.</param>
//    /// <param name="subscription">Subscription.</param>
//    public void AddElement(IChartArea area, IChartCandleElement element, Subscription subscription)
//    {
//        if ( area == null )
//            throw new ArgumentNullException(nameof(area));
//        if ( element == null )
//            throw new ArgumentNullException(nameof(element));
//        if ( subscription == null )
//            throw new ArgumentNullException(nameof(subscription));
//        this._subscrptionMap.Add((IChartElement)element, subscription);
//        this.AddElement(area, (IChartElement)element);
//    }


//    /// <summary>To add an element to the chart.</summary>
//    /// <param name="area">Chart area.</param>
//    /// <param name="element">The chart element.</param>
//    /// <param name="subscription">Subscription.</param>
//    public void AddElement(
//      IChartArea area,
//      IChartIndicatorElement element,
//      Subscription subscription,
//      IIndicator indicator)
//    {
//        if ( area == null )
//            throw new ArgumentNullException(nameof(area));
//        if ( element == null )
//            throw new ArgumentNullException(nameof(element));
//        if ( indicator == null )
//            throw new ArgumentNullException(nameof(indicator));
//        if ( subscription != null )
//            this._subscrptionMap.Add((IChartElement)element, subscription);
//        this._indicatorElementMap.Add(element, new Chart.SomeInternalSealedClass897634(this, element, indicator));
//        ( (ChartIndicatorElement)element ).CreateIndicatorPainter(this.IndicatorTypes, indicator);
//        this.AddElement(area, (IChartElement)element);
//    }

//    public void AddElement(IChartArea area, IChartOrderElement element, Subscription subscription)
//    {
//        if ( area == null )
//            throw new ArgumentNullException(nameof(area));
//        if ( element == null )
//            throw new ArgumentNullException(nameof(element));
//        if ( subscription == null )
//            throw new ArgumentNullException(nameof(subscription));
//        this._subscrptionMap.Add((IChartElement)element, subscription);
//        this.AddElement(area, (IChartElement)element);
//    }

//    public void AddElement(IChartArea area, IChartTradeElement element, Subscription subscription)
//    {
//        if ( area == null )
//            throw new ArgumentNullException(nameof(area));
//        if ( element == null )
//            throw new ArgumentNullException(nameof(element));
//        if ( subscription == null )
//            throw new ArgumentNullException(nameof(subscription));
//        this._subscrptionMap.Add((IChartElement)element, subscription);
//        this.AddElement(area, (IChartElement)element);
//    }

//    void IChart.RemoveElement(IChartArea area, IChartElement element)
//    {
//        Chart.SomeInternalClass033434 jyDziF55m7JqXuwJc = new Chart.SomeInternalClass033434();
//        jyDziF55m7JqXuwJc._chartArea_093 = area;
//        jyDziF55m7JqXuwJc._someChartElement0343123 = element;
//        if ( jyDziF55m7JqXuwJc._chartArea_093 == null )
//            throw new ArgumentNullException(nameof(area));
//        if ( jyDziF55m7JqXuwJc._someChartElement0343123 == null )
//            throw new ArgumentNullException(nameof(element));
//        Chart.SomeInternalSealedClass897634 zZq9Hpf12oRwg;
//        if ( jyDziF55m7JqXuwJc._someChartElement0343123 is IChartIndicatorElement zI6sZdg && this._indicatorElementMap.TryGetValue(zI6sZdg, ref zZq9Hpf12oRwg) )
//        {
//            zZq9Hpf12oRwg.Dispose();
//            this._indicatorElementMap.Remove(zI6sZdg);
//        }
//      ( (DispatcherObject)this ).GuiSync<bool>(new Func<bool>(jyDziF55m7JqXuwJc.Method0834));
//        this._subscrptionMap.Remove(jyDziF55m7JqXuwJc._someChartElement0343123);
//    }


//    /// <summary>
//    /// To get an indicator which is associated with <see cref="T:StockSharp.Charting.IChartIndicatorElement" />.
//    /// </summary>
//    /// <param name="element">The chart element.</param>
//    /// <returns>Indicator.</returns>
//    public IIndicator GetIndicatorElement(IChartIndicatorElement element)
//    {
//        return CollectionHelper.TryGetValue<IChartIndicatorElement, Chart.SomeInternalSealedClass897634>((IDictionary<IChartIndicatorElement, Chart.SomeInternalSealedClass897634>)this._indicatorElementMap, element)?.Indicator;
//    }

//    public Subscription TryGetSubscription(IChartElement element)
//    {
//        return CollectionHelper.TryGetValue<IChartElement, Subscription>((IDictionary<IChartElement, Subscription>)this._subscrptionMap, element);
//    }

//    private (IChartCandleElement, Subscription) GetChartCandleElementToSubscription()
//    {
//        foreach ( IChartElement chartElement in this.Areas.SelectMany<IChartArea, IChartElement>(Chart.SomeClass34343383.SomeShit77Mn ?? ( Chart.SomeClass34343383.SomeShit77Mn = new Func<IChartArea, IEnumerable<IChartElement>>(Chart.SomeClass34343383.SomeMethond0343.SomeMethodPJeLPTCg) )) )
//        {
//            if ( chartElement is IChartCandleElement element )
//            {
//                Subscription subscription = this.TryGetSubscription((IChartElement)element);
//                if ( ( subscription != null ? ( !subscription.SecurityId.HasValue ? 1 : 0 ) : 1 ) == 0 )
//                    return (element, subscription);
//            }
//        }
//        return ();
//    }

//    private void ResetSecurityAndSubscription()
//    {
//        Subscription subscription1 = this.GetChartCandleElementToSubscription().Item2;
//        if ( this.GetSubscription() == subscription1 && this.GetSecurity() == ( subscription1 != null ? subscription1.TryGetSecurity() : (Security)null ) )
//            return;
//        this.SetSubscription(subscription1);
//        Subscription subscription2 = this.GetSubscription();
//        this.SetSecurity(subscription2 != null ? subscription2.TryGetSecurity() : (Security)null);
//        Action zKgKj0Lc = this._someActionEvent;
//        if ( zKgKj0Lc == null )
//            return;
//        zKgKj0Lc();
//    }

//    public void SetSubscription(IChartElement element, Subscription subscription)
//    {
//        SynchronizedDictionary<IChartElement, Subscription> zvWhSaOs = this._subscrptionMap;
//        IChartElement chartElement = element;
//        zvWhSaOs[chartElement] = subscription ?? throw new ArgumentNullException(nameof(subscription));
//        ( (IChartComponent)element ).ResetUI();
//    }


//    /// <summary>
//    /// Cancel orders that were added to this instance of <see cref="T:StockSharp.Xaml.Charting.Chart" />.
//    /// This method does not cancel orders by itself. It just notifies subscribers with <see cref="E:StockSharp.Xaml.Charting.Chart.CancelOrder" /> event.
//    /// </summary>
//    public void CancelOrders(Func<Order, bool> predicate = null)
//    {
//        ( (DispatcherObject)this ).GuiSync(new Action(new Chart.SomeInternalSealedClass082232()
//        {
//            _variableSome3535 = this,
//            m_public_Func_Order_bool_ = predicate
//        }.SomeCancelOrdersMethod03343));
//    }


//    /// <summary>
//    /// To use annotations to define the visible range for the Y-axis.
//    /// </summary>
//    public bool AutoRangeByAnnotations
//    {
//        get => this._autoRangeByAnnotations;
//        set
//        {
//            if ( this._autoRangeByAnnotations == value )
//                return;
//            this._autoRangeByAnnotations = value;
//            NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof(AutoRangeByAnnotations));
//        }
//    }


//    /// <summary>The minimum number of visible candles.</summary>
//    public int MinimumRange
//    {
//        get => this.ViewModel.MinimumRange;
//        set => this.ViewModel.MinimumRange = value;
//    }

//    public string ChartTheme
//    {
//        get => this.ViewModel.SelectedTheme;
//        set => this.ViewModel.SelectedTheme = value;
//    }

//    public bool ShowLegend
//    {
//        get => this.ViewModel.ShowLegend;
//        set => this.ViewModel.ShowLegend = value;
//    }

//    public bool ShowOverview
//    {
//        get => this.ViewModel.ShowOverview;
//        set => this.ViewModel.ShowOverview = value;
//    }

//    public bool ShowPerfStats
//    {
//        get => this._showPerfStats;
//        set
//        {
//            if ( this._showPerfStats == value )
//                return;
//            this._showPerfStats = value;
//            NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof(ShowPerfStats));
//        }
//    }

//    public bool IsInteracted
//    {
//        get => this.ViewModel.IsInteracted;
//        set => this.ViewModel.IsInteracted = value;
//    }

//    /// <summary>Allow user to add new chart area.</summary>
//    public bool AllowAddArea
//    {
//        get => this.ViewModel.AllowAddArea;
//        set => this.ViewModel.AllowAddArea = value;
//    }


//    /// <summary>Allow user to add new chart axis.</summary>
//    public bool AllowAddAxis
//    {
//        get => this.ViewModel.AllowAddAxis;
//        set => this.ViewModel.AllowAddAxis = value;
//    }

//    /// <summary>Allow user to add new candles elements.</summary>
//    public bool AllowAddCandles
//    {
//        get => this.ViewModel.AllowAddCandles;
//        set => this.ViewModel.AllowAddCandles = value;
//    }

//    /// <summary>Allow user to add new indicator elements.</summary>
//    public bool AllowAddIndicators
//    {
//        get => this.ViewModel.AllowAddIndicators;
//        set => this.ViewModel.AllowAddIndicators = value;
//    }

//    /// <summary>Allow user to add new orders elements.</summary>
//    public bool AllowAddOrders
//    {
//        get => this.ViewModel.AllowAddOrders;
//        set => this.ViewModel.AllowAddOrders = value;
//    }

//    /// <summary>Allow user to add new own trades elements.</summary>
//    public bool AllowAddOwnTrades
//    {
//        get => this.ViewModel.AllowAddOwnTrades;
//        set => this.ViewModel.AllowAddOwnTrades = value;
//    }

//    public bool CrossHair
//    {
//        get => this._crossHair;
//        set
//        {
//            if ( this._crossHair == value )
//                return;
//            this._crossHair = value;
//            NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof(CrossHair));
//        }
//    }

//    public bool CrossHairTooltip
//    {
//        get => this._crossHairTooltip;
//        set
//        {
//            if ( this._crossHairTooltip == value )
//                return;
//            this._crossHairTooltip = value;
//            NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof(CrossHairTooltip));
//        }
//    }

//    public bool CrossHairAxisLabels
//    {
//        get => this._crossHairAxisLabels;
//        set
//        {
//            if ( this._crossHairAxisLabels == value )
//                return;
//            this._crossHairAxisLabels = value;
//            NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof(CrossHairAxisLabels));
//        }
//    }

//    /// <summary>The prompt message type for drawing on the chart.</summary>
//    public ChartAnnotationTypes AnnotationType
//    {
//        get => this._annotationType;
//        set
//        {
//            if ( this._annotationType == value )
//                return;
//            this._annotationType = value;
//            NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof(AnnotationType));
//        }
//    }

//    public bool OrderCreationMode
//    {
//        get => this._orderCreationMode;
//        set
//        {
//            if ( this._orderCreationMode == value )
//                return;
//            this._orderCreationMode = value;
//            NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof(OrderCreationMode));
//        }
//    }

//    public TimeZoneInfo TimeZone
//    {
//        get => this._timeZone;
//        set
//        {
//            if ( this._timeZone == value )
//                return;
//            this._timeZone = value ?? throw new ArgumentNullException(nameof(value));
//            NotifyPropertyChangedExHelper.Notify<Chart>(this, nameof(TimeZone));
//        }
//    }

//    public bool ShowNonFormedIndicators
//    {
//        get => this._showNonFormedIndicators;
//        set => this._showNonFormedIndicators = value;
//    }

//    public IList<IndicatorType> IndicatorTypes
//    {
//        get => (IList<IndicatorType>)this.ViewModel.IndicatorTypes;
//    }

//    public Security GetSecurity()
//    {
//        return this._security;
//    }

//    private void SetSecurity(Security _param1)
//    {
//        this._security = _param1;
//    }

//    public Subscription GetSubscription()
//    {
//        return this._subscription2;
//    }

//    private void SetSubscription(Subscription _param1)
//    {
//        this._subscription2 = _param1;
//    }

//    public IEnumerable<Subscription> Subscriptions
//    {
//        get => this._subscrptionMap.Values.Distinct<Subscription>();
//    }

//    /// <summary>The chart area creation event.</summary>
//    public event Action AreaAdding
//    {
//        add
//        {
//            ViewModel.AreaAddingEvent += value;
//        }
//        remove
//        {
//            ViewModel.AreaAddingEvent -= value;
//        }
//    }

//    /// <summary>The chart element creation event.</summary>
//    public event Action<ChartArea> AddCandles
//    {
//        add
//        {
//            ViewModel.AddCandlesEvent += value;
//        }
//        remove
//        {
//            ViewModel.AddCandlesEvent -= value;
//        }
//    }

//    /// <summary>The chart element creation event.</summary>
//    public event Action<ChartArea> AddIndicator
//    {
//        add
//        {
//            ViewModel.AddIndicatorEvent += value;
//        }
//        remove
//        {
//            ViewModel.AddIndicatorEvent -= value;
//        }
//    }

//    /// <summary>The chart element creation event.</summary>
//    public event Action<ChartArea> AddOrders
//    {
//        add
//        {
//            ViewModel.AddOrdersEvent += value;
//        }
//        remove
//        {
//            ViewModel.AddOrdersEvent -= value;
//        }
//    }

//    /// <summary>The chart element creation event.</summary>
//    public event Action<ChartArea> AddTrades
//    {
//        add => this.ViewModel.AddTradesEvent += value;
//        remove
//        {
//            this.ViewModel.AddTradesEvent -= value;
//        }
//    }

//    /// <summary>The chart element removal event.</summary>
//    public event Action<IChartElement> RemoveElement
//    {
//        add => this.ViewModel.RemoveElementEvent+= ( value );
//        remove => this.ViewModel.RemoveElementEvent -= ( value );
//    }

//    /// <summary>Rebuild candles event.</summary>
//    public event Action<IChartElement, Subscription> RebuildCandles
//    {
//        add => this.ViewModel.RebuildCandlesEvent += ( value );
//        remove => this.ViewModel.RebuildCandlesEvent -=( value );
//    }

//    /// <summary>The new order creation event.</summary>
//    public event Action<ChartArea, Order> CreateOrder;

//    /// <summary>Move order event.</summary>
//    public event Action<Order, Decimal> MoveOrder;

//    /// <summary>Cancel order event.</summary>
//    public event Action<Order> CancelOrder;

//    /// <summary>Annotation created event.</summary>
//    public event Action<IChartAnnotationElement> AnnotationCreated;

//    /// <summary>Annotation Modified event.</summary>
//    public event Action<IChartAnnotationElement, ChartDrawData.AnnotationData> AnnotationModified;

//    /// <summary>Annotation deleted event.</summary>
//    public event Action<IChartAnnotationElement> AnnotationDeleted;

//    /// <summary>Annotation selection event.</summary>
//    public event Action<IChartAnnotationElement, ChartDrawData.AnnotationData> AnnotationSelected;

//    /// <summary>
//    /// The event of the subscription to getting data for the element.
//    /// </summary>
//    public event Action<IChartCandleElement, Subscription> SubscribeCandleElement;

//    /// <summary>
//    /// The event of the subscription to getting data for the Indicator Chart element.
//    /// </summary>
//    public event Action<IChartIndicatorElement, Subscription, IIndicator> SubscribeIndicatorElement;

//    /// <summary>
//    /// The event of the subscription to getting data for the Order Chart Element UI.
//    /// </summary>
//    public event Action<IChartOrderElement, Subscription> SubscribeOrderElement;

//    /// <summary>
//    /// The event of the subscription to getting data for the Trade Chart Element UI.
//    /// </summary>
//    public event Action<IChartTradeElement, Subscription> SubscribeTradeElement;

//    /// <summary>
//    /// The event of the unsubscribe from getting data for the element.
//    /// </summary>
//    public event Action<IChartElement> UnSubscribeElement;

//    public void AddiChartAreaEvent(Action<IChartArea> _param1)
//    {
//        Action<IChartArea> action = this._iChartAreaEvent;
//        Action<IChartArea> comparand;
//        do
//        {
//            comparand = action;
//            action = Interlocked.CompareExchange<Action<IChartArea>>(ref this._iChartAreaEvent, comparand + _param1, comparand);
//        }
//        while ( action != comparand );
//    }

//    public void RemoveiChartAreaEvent(Action<IChartArea> _param1)
//    {
//        Action<IChartArea> action = this._iChartAreaEvent;
//        Action<IChartArea> comparand;
//        do
//        {
//            comparand = action;
//            action = Interlocked.CompareExchange<Action<IChartArea>>(ref this._iChartAreaEvent, comparand - _param1, comparand);
//        }
//        while ( action != comparand );
//    }

//    public void AddsomeActionEvent(Action _param1)
//    {
//        Action action = this._someActionEvent;
//        Action comparand;
//        do
//        {
//            comparand = action;
//            action = Interlocked.CompareExchange<Action>(ref this._someActionEvent, comparand + _param1, comparand);
//        }
//        while ( action != comparand );
//    }

//    public void RemoveSomeActionEvent(Action _param1)
//    {
//        Action action = this._someActionEvent;
//        Action comparand;
//        do
//        {
//            comparand = action;
//            action = Interlocked.CompareExchange<Action>(ref this._someActionEvent, comparand - _param1, comparand);
//        }
//        while ( action != comparand );
//    }

//    public void Reset(IEnumerable<IChartElement> elements)
//    {
//        Chart.SomeInternalClass033436 obj1 = new Chart.SomeInternalClass033436();
//        obj1._variableSome3535 = this;
//        obj1._someChartElements3432123 = elements;
//        Chart.SomeInternalClass033436 obj2 = obj1;
//        List<IChartElement> chartElementList = new List<IChartElement>();
//        chartElementList.AddRange(obj1._someChartElements3432123);
//        List<IChartElement> uqahKaP3EikL1yMva = new List<IChartElement>(chartElementList);
//        obj2._someChartElements3432123 = (IEnumerable<IChartElement>)uqahKaP3EikL1yMva;
//        ( (DispatcherObject)this ).GuiSync(new Action(obj1.Method01));
//    }

//    public IChartDrawData CreateData() => (IChartDrawData)new ChartDrawData();

//    public IChartArea CreateArea()
//    {
//        return ( (DispatcherObject)this ).GuiSync<IChartArea>(new Func<IChartArea>(this._chartBuilder.CreateArea));
//    }

//    public IChartAxis CreateAxis()
//    {
//        return ( (DispatcherObject)this ).GuiSync<IChartAxis>(new Func<IChartAxis>(this._chartBuilder.CreateAxis));
//    }

//    public IChartCandleElement CreateCandleElement()
//    {
//        return ( (DispatcherObject)this ).GuiSync<IChartCandleElement>(new Func<IChartCandleElement>(this._chartBuilder.CreateCandleElement));
//    }

//    public IChartIndicatorElement CreateIndicatorElement()
//    {
//        return ( (DispatcherObject)this ).GuiSync<IChartIndicatorElement>(new Func<IChartIndicatorElement>(this._chartBuilder.CreateIndicatorElement));
//    }

//    public IChartActiveOrdersElement CreateActiveOrdersElement()
//    {
//        return ( (DispatcherObject)this ).GuiSync<IChartActiveOrdersElement>(new Func<IChartActiveOrdersElement>(this._chartBuilder.CreateActiveOrdersElement));
//    }

//    public IChartAnnotationElement CreateAnnotation()
//    {
//        return ( (DispatcherObject)this ).GuiSync<IChartAnnotationElement>(new Func<IChartAnnotationElement>(this._chartBuilder.CreateAnnotation));
//    }

//    public IChartBandElement CreateBandElement()
//    {
//        return ( (DispatcherObject)this ).GuiSync<IChartBandElement>(new Func<IChartBandElement>(this._chartBuilder.CreateBandElement));
//    }

//    public IChartLineElement CreateLineElement()
//    {
//        return ( (DispatcherObject)this ).GuiSync<IChartLineElement>(new Func<IChartLineElement>(this._chartBuilder.CreateLineElement));
//    }

//    public IChartLineElement CreateBubbleElement()
//    {
//        return ( (DispatcherObject)this ).GuiSync<IChartLineElement>(new Func<IChartLineElement>(this._chartBuilder.CreateBubbleElement));
//    }

//    public IChartOrderElement CreateOrderElement()
//    {
//        return ( (DispatcherObject)this ).GuiSync<IChartOrderElement>(new Func<IChartOrderElement>(this._chartBuilder.CreateOrderElement));
//    }

//    public IChartTradeElement CreateTradeElement()
//    {
//        return ( (DispatcherObject)this ).GuiSync<IChartTradeElement>(new Func<IChartTradeElement>(this._chartBuilder.CreateTradeElement));
//    }

//    public void Draw(IChartDrawData data)
//    {
//        ChartDrawData chartDrawData = data != null ? (ChartDrawData)data : throw new ArgumentNullException(nameof(data));
//        foreach ( ChartArea chartArea in this._iChartAreaList )
//            chartArea.ViewModel.Draw(chartDrawData);
//    }

//    public void InvokeCreateOrderEvent(ChartArea _param1, Order _param2)
//    {
//        Action<ChartArea, Order> zlaBqx5E = this.CreateOrder;
//        if ( zlaBqx5E == null )
//            return;
//        zlaBqx5E(_param1, _param2);
//    }

//    public void InvokeMoveOrderEvent(Order _param1, Decimal _param2)
//    {
//        Action<Order, Decimal> zJiM5nvc = this.MoveOrder;
//        if ( zJiM5nvc == null )
//            return;
//        zJiM5nvc(_param1, _param2);
//    }

//    public void InvokeCancelOrderEvent(Order _param1)
//    {
//        Action<Order> zmMdfCucSnZwz = this.CancelOrder;
//        if ( zmMdfCucSnZwz == null )
//            return;
//        zmMdfCucSnZwz(_param1);
//    }

//    public void InvokeAnnotationCreatedEvent(ChartAnnotation _param1)
//    {
//        Action<IChartAnnotationElement> z6KsbJRt22Hb = this.AnnotationCreated;
//        if ( z6KsbJRt22Hb == null )
//            return;
//        z6KsbJRt22Hb((IChartAnnotationElement)_param1);
//    }

//    public void InvokeAnnotationModifiedEvent(
//      ChartAnnotation _param1,
//      ChartDrawData.AnnotationData _param2)
//    {
//        Action<IChartAnnotationElement, ChartDrawData.AnnotationData> zygdSp72uKvhL = this.AnnotationModified;
//        if ( zygdSp72uKvhL == null )
//            return;
//        zygdSp72uKvhL((IChartAnnotationElement)_param1, _param2);
//    }

//    public void InvokeAnnotationDeletedEvent(ChartAnnotation _param1)
//    {
//        Action<IChartAnnotationElement> z53l3VmDrGxpJ = this.AnnotationDeleted;
//        if ( z53l3VmDrGxpJ == null )
//            return;
//        z53l3VmDrGxpJ((IChartAnnotationElement)_param1);
//    }

//    public void InvokeAnnotationSelectedEvent(
//      ChartAnnotation _param1,
//      ChartDrawData.AnnotationData _param2)
//    {
//        Action<IChartAnnotationElement, ChartDrawData.AnnotationData> zxVqsLo94Ea68 = this.AnnotationSelected;
//        if ( zxVqsLo94Ea68 == null )
//            return;
//        zxVqsLo94Ea68((IChartAnnotationElement)_param1, _param2);
//    }

//    public TimeZoneInfo GetTimeZoneInfo()
//    {
//        return this.Areas.Select<IChartArea, IChartAxis>(Chart.SomeClass34343383.SomeShitMethodOrSomeWhate ?? ( Chart.SomeClass34343383.SomeShitMethodOrSomeWhate = new Func<IChartArea, IChartAxis>(Chart.SomeClass34343383.SomeMethond0343.Method01) )).LastOrDefault<IChartAxis>(Chart.SomeClass34343383._member02 ?? ( Chart.SomeClass34343383._member02 = new Func<IChartAxis, bool>(Chart.SomeClass34343383.SomeMethond0343.Method03) ))?.TimeZone;
//    }

//    public void ChangeCandleTimeFrame(TimeSpan _param1)
//    {
//        (IChartCandleElement chartCandleElement, Subscription subscription) = this.GetChartCandleElementToSubscription();
//        if ( chartCandleElement == null )
//            return;
//        object obj = subscription.DataType.Arg;
//        if ( ( obj != null ? ( obj.Equals((object)_param1) ? 1 : 0 ) : 0 ) != 0 )
//            return;
//        this.OnRebuildCandles((IChartElement)chartCandleElement, new Subscription(Extensions.TimeFrame(_param1), (SecurityMessage)subscription.MarketData));
//    }

//    private void OnRebuildCandles(
//      IChartElement _param1,
//      Subscription _param2)
//    {
//        Chart.SomeInternalClass033438 magwJg5Cu0tHrBa0 = new Chart.SomeInternalClass033438();
//        magwJg5Cu0tHrBa0._variableSome3535 = this;
//        magwJg5Cu0tHrBa0._someChartElement0343123 = _param1 as IChartCandleElement;
//        if ( magwJg5Cu0tHrBa0._someChartElement0343123 == null )
//            return;
//        IChartArea chartArea = magwJg5Cu0tHrBa0._someChartElement0343123.ChartArea;
//        magwJg5Cu0tHrBa0._subscription = this.TryGetSubscription((IChartElement)magwJg5Cu0tHrBa0._someChartElement0343123);
//        Dictionary<IChartIndicatorElement, Tuple<IIndicator, IChartArea>> dictionary = ( (IEnumerable<KeyValuePair<IChartElement, Subscription>>)this._subscrptionMap ).Where<KeyValuePair<IChartElement, Subscription>>(new Func<KeyValuePair<IChartElement, Subscription>, bool>(magwJg5Cu0tHrBa0.Method02341)).Select<KeyValuePair<IChartElement, Subscription>, IChartIndicatorElement>(Chart.SomeClass34343383._member03 ?? ( Chart.SomeClass34343383._member03 = new Func<KeyValuePair<IChartElement, Subscription>, IChartIndicatorElement>(Chart.SomeClass34343383.SomeMethond0343.Method04) )).ToDictionary<IChartIndicatorElement, IChartIndicatorElement, Tuple<IIndicator, IChartArea>>(Chart.SomeClass34343383._member04 ?? ( Chart.SomeClass34343383._member04 = new Func<IChartIndicatorElement, IChartIndicatorElement>(Chart.SomeClass34343383.SomeMethond0343.Method05) ), new Func<IChartIndicatorElement, Tuple<IIndicator, IChartArea>>(magwJg5Cu0tHrBa0.Method02342));
//        this.OnRemoveElement((IChartElement)magwJg5Cu0tHrBa0._someChartElement0343123);
//        CollectionHelper.ForEach<IChartIndicatorElement>((IEnumerable<IChartIndicatorElement>)dictionary.Keys, new Action<IChartIndicatorElement>(this.OnRemoveElement));
//        ( (IChartComponent)magwJg5Cu0tHrBa0._someChartElement0343123 ).ResetUI();
//        this.AddElement(chartArea, magwJg5Cu0tHrBa0._someChartElement0343123, _param2);
//        foreach ( KeyValuePair<IChartIndicatorElement, Tuple<IIndicator, IChartArea>> keyValuePair in dictionary )
//            this.AddElement(keyValuePair.Value.Item2, keyValuePair.Key, _param2, keyValuePair.Value.Item1);
//        this.ResetSecurityAndSubscription();
//    }

//    private void OnRemoveElement(IChartElement _param1)
//    {
//        if ( _param1 is ChartIndicatorElement indicatorElement && indicatorElement.ParentElement != null )
//            _param1 = indicatorElement.ParentElement;
//        ( (IChart)this ).RemoveElement(_param1.ChartArea, _param1);
//        this.ResetSecurityAndSubscription();
//    }

//    private void OnRemoveElement(IChartElement _param1)
//    {
//        if ( this.TryGetSubscription(_param1) == null )
//            return;
//        Action<IChartElement> z9PnYjM29SjfT = this.RemoveElement;
//        if ( z9PnYjM29SjfT == null )
//            return;
//        z9PnYjM29SjfT(_param1);
//    }

//    /// <summary>Load settings.</summary>
//    /// <param name="storage">Settings storage.</param>
//    public void Load(SettingsStorage storage)
//    {
//        this.IsAutoScroll = storage.GetValue<bool>("IsAutoScroll", this.IsAutoScroll);
//        this.IsAutoRange = storage.GetValue<bool>("IsAutoRange", this.IsAutoRange);
//        this.AutoRangeByAnnotations = storage.GetValue<bool>("AutoRangeByAnnotations", this.AutoRangeByAnnotations);
//        this.ShowOverview = storage.GetValue<bool>("ShowOverview", this.ShowOverview);
//        this.ShowLegend = storage.GetValue<bool>("ShowLegend", this.ShowLegend);
//        this.CrossHair = storage.GetValue<bool>("CrossHair", this.CrossHair);
//        this.CrossHairTooltip = storage.GetValue<bool>("CrossHairTooltip", this.CrossHairTooltip);
//        this.CrossHairAxisLabels = storage.GetValue<bool>("CrossHairAxisLabels", this.CrossHairAxisLabels);
//        this.OrderCreationMode = storage.GetValue<bool>("OrderCreationMode", this.OrderCreationMode);
//        this.TimeZone = Converter.To<TimeZoneInfo>((object)storage.GetValue<string>("TimeZone", (string)null)) ?? this.TimeZone;
//        this.ShowPerfStats = storage.GetValue<bool>("ShowPerfStats", this.ShowPerfStats);
//        if ( !this.IsInteracted )
//            return;
//        this._subscrptionMap.Clear();
//        CollectionHelper.ForEach<Chart.SomeInternalSealedClass897634>((IEnumerable<Chart.SomeInternalSealedClass897634>)this._indicatorElementMap.Values, Chart.SomeClass34343383._member05 ?? ( Chart.SomeClass34343383._member05 = new Action<Chart.SomeInternalSealedClass897634>(Chart.SomeClass34343383.SomeMethond0343.Method06) ));
//        this._indicatorElementMap.Clear();
//        object source = storage.GetValue<object>("Areas", (object)null);
//        if ( source == null )
//            return;
//        if ( source is SettingsStorage settingsStorage )
//            source = (object)settingsStorage.GetValue<IEnumerable<SettingsStorage>>("Areas", (IEnumerable<SettingsStorage>)null);
//        this.SomeOtherSettingStorageMethod(( (IEnumerable)source ).Cast<SettingsStorage>());
//    }

//    /// <summary>Save settings.</summary>
//    /// <param name="storage">Settings storage.</param>
//    public void Save(SettingsStorage storage)
//    {
//        storage.SetValue<bool>("IsAutoScroll", this.IsAutoScroll);
//        storage.SetValue<bool>("IsAutoRange", this.IsAutoRange);
//        storage.SetValue<bool>("AutoRangeByAnnotations", this.AutoRangeByAnnotations);
//        storage.SetValue<bool>("ShowOverview", this.ShowOverview);
//        storage.SetValue<bool>("ShowLegend", this.ShowLegend);
//        storage.SetValue<bool>("CrossHair", this.CrossHair);
//        storage.SetValue<bool>("CrossHairTooltip", this.CrossHairTooltip);
//        storage.SetValue<bool>("CrossHairAxisLabels", this.CrossHairAxisLabels);
//        storage.SetValue<bool>("OrderCreationMode", this.OrderCreationMode);
//        storage.SetValue<string>("TimeZone", this.TimeZone?.Id);
//        storage.SetValue<bool>("ShowPerfStats", this.ShowPerfStats);
//        if ( !this.IsInteracted )
//            return;
//        storage.SetValue<SettingsStorage[]>("Areas", this._iChartAreaList.Select<IChartArea, SettingsStorage>(Chart.SomeClass34343383._member06 ?? ( Chart.SomeClass34343383._member06 = new Func<IChartArea, SettingsStorage>(Chart.SomeClass34343383.SomeMethond0343.SomeSettingsStorageHelpingFunction0342) )).ToArray<SettingsStorage>());
//    }

//    /// <summary>To re-subscribe to getting data for all elements.</summary>
//    public void ReSubscribeElements()
//    {
//        if ( !this.IsInteracted )
//            return;
//        foreach ( IChartElement element in this.GetElements() )
//        {
//            this.OnRemoveElement(element);
//            this.DoSomeSubscriptionStuff093(element);
//        }
//    }

//    private void SomeOtherSettingStorageMethod(IEnumerable<SettingsStorage> _param1)
//    {
//        this._iChartAreaList.Clear();
//        foreach ( SettingsStorage storage in _param1 )
//        {
//            ChartArea area = new ChartArea();
//            area.Load(storage);
//            this.AddArea((IChartArea)area);
//            area.ViewModel.Height = storage.GetValue<double>("Height", double.NaN);
//        }
//    }

//    private void DoSomeSubscriptionStuff093(IChartElement _param1)
//    {
//        Subscription subscription = this.TryGetSubscription(_param1);
//        if ( subscription == null )
//            return;
//        this.InternalDoSomeSubscriptionStuff093(_param1, subscription);
//    }

//    private void InternalDoSomeSubscriptionStuff093(IChartElement _param1, Subscription _param2)
//    {
//        switch ( _param1 )
//        {
//            case IChartCandleElement chartCandleElement:
//                Action<IChartCandleElement, Subscription> wdiUyNemvFqkVapq = this.SubscribeCandleElement;
//                if ( wdiUyNemvFqkVapq == null )
//                    break;
//                wdiUyNemvFqkVapq(chartCandleElement, _param2);
//                break;
//            case IChartIndicatorElement element:
//                Action<IChartIndicatorElement, Subscription, IIndicator> zLdfE1FxkiHdr = this.SubscribeIndicatorElement;
//                if ( zLdfE1FxkiHdr == null )
//                    break;
//                zLdfE1FxkiHdr(element, _param2, this.GetIndicatorElement(element));
//                break;
//            case IChartOrderElement chartOrderElement:
//                Action<IChartOrderElement, Subscription> zh7nXgYWoKl = this.SubscribeOrderElement;
//                if ( zh7nXgYWoKl == null )
//                    break;
//                zh7nXgYWoKl(chartOrderElement, _param2);
//                break;
//            case IChartTradeElement chartTradeElement:
//                Action<IChartTradeElement, Subscription> ssvKvae0LsR0LbUsEg = this.SubscribeTradeElement;
//                if ( ssvKvae0LsR0LbUsEg == null )
//                    break;
//                ssvKvae0LsR0LbUsEg(chartTradeElement, _param2);
//                break;
//        }
//    }



//    private void OnChartAreaElementAdded(IChartElement _param1)
//    {
//        this.ResetSecurityAndSubscription();
//        if ( !this.IsInteracted )
//            return;
//        this.DoSomeSubscriptionStuff093(_param1);
//    }

//    private void OnChartAreaElementRemoved(IChartElement _param1)
//    {
//        this.ResetSecurityAndSubscription();
//        this.SomeInternalHelperFunctions3234(_param1, false);
//    }

//    private void SomeInternalHelperFunctions3234(IChartElement _param1, bool _param2)
//    {
//        IChartElement[] elements;
//        if ( _param1 is IChartCandleElement )
//        {
//            Chart.SomeInternalClass033437 uyozsiG5aIcl2VmAkuos = new Chart.SomeInternalClass033437();
//            uyozsiG5aIcl2VmAkuos._variableSome3535 = this;
//            uyozsiG5aIcl2VmAkuos._subscription = this.TryGetSubscription(_param1);
//            List<IChartElement> chartElementList = new List<IChartElement>();
//            chartElementList.AddRange(this.GetElements().Where<IChartElement>(new Func<IChartElement, bool>(uyozsiG5aIcl2VmAkuos.Method023)).Concat<IChartElement>((IEnumerable<IChartElement>)new List<IChartElement>(_param1)).Distinct<IChartElement>());
//            elements = chartElementList.ToArray();
//        }
//        else
//            elements = new IChartElement[1] { _param1 };
//        if ( _param2 )
//        {
//            if ( this.IsInteracted )
//                CollectionHelper.ForEach<IChartElement>((IEnumerable<IChartElement>)elements, new Action<IChartElement>(this.OnRemoveElements));
//            this.Reset((IEnumerable<IChartElement>)elements);
//            if ( !this.IsInteracted )
//                return;
//            CollectionHelper.ForEach<IChartElement>((IEnumerable<IChartElement>)elements, new Action<IChartElement>(this.InternalDoSomeSubscriptionStuff31341));
//        }
//        else
//        {
//            if ( !this.IsInteracted )
//                return;
//            CollectionHelper.ForEach<IChartElement>((IEnumerable<IChartElement>)elements, new Action<IChartElement>(this.OnRemoveElement));
//        }
//    }

//    public static void SetupScichartSurface(
//      SciChartSurface _param0)
//    {
//        Chart.SomeInternalClass033435 qka25erb85NfEm3z4 = new Chart.SomeInternalClass033435();
//        qka25erb85NfEm3z4._drawingSurface = _param0;
//        if ( qka25erb85NfEm3z4._drawingSurface.DataContext != null )
//            qka25erb85NfEm3z4.SetDataContext();
//        else
//            qka25erb85NfEm3z4._drawingSurface.DataContextChanged += new DependencyPropertyChangedEventHandler(qka25erb85NfEm3z4.Method02);
//    }

//    private void OnInitialized(object _param1, EventArgs _param2)
//    {
//        Chart.SetupScichartSurface((SciChartSurface)_param1);
//    }

//    private void SomeMethod03342(
//      IChartIndicatorElement _param1,
//      IIndicator _param2)
//    {
//        ( (IChartComponent)_param1 ).ResetUI();
//        this.SomeInternalHelperFunctions3234((IChartElement)_param1, true);
//    }

//    event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
//    {
//        add => this.PropertyChangedEvent += value;
//        remove => this.PropertyChangedEvent -= value;
//    }

//    void INotifyPropertyChangedEx.NotifyPropertyChanged(string propertyName)
//    {
//        PropertyChangedEventHandler ziApqnpw = this.PropertyChangedEvent;
//        if ( ziApqnpw == null )
//            return;
//        DelegateHelper.Invoke(ziApqnpw, (object)this, propertyName);
//    }



//    private Security GetSelectedSecurity(ChartArea _param1)
//    {
//        IChartCandleElement[] array = ( (IEnumerable)_param1.Elements ).OfType<IChartCandleElement>().ToArray<IChartCandleElement>();
//        IChartCandleElement element = TemplateTypeHelper.GetFirstElement<IChartCandleElement>(array);
//        if ( array.Length > 1 )
//        {
//            ChartCandleElementPicker wnd = new ChartCandleElementPicker()
//            {
//                Elements = (IEnumerable<IChartCandleElement>)array,
//                SelectedElement = element
//            };
//            if ( !wnd.ShowModal((DependencyObject)this) )
//                return (Security)null;
//            element = wnd.SelectedElement;
//        }
//        Security security1;
//        if ( element != null )
//        {
//            Subscription subscription = this.TryGetSubscription((IChartElement)element);
//            security1 = subscription != null ? subscription.TryGetSecurity() : (Security)null;
//        }
//        else
//            security1 = (Security)null;
//        Security security2 = security1;
//        if ( security2 == null )
//        {
//            SecurityPickerWindow wnd = new SecurityPickerWindow()
//            {
//                SelectionMode = MultiSelectMode.Row
//            };
//            if ( this.SecurityProvider != null )
//                wnd.SecurityProvider = this.SecurityProvider;
//            if ( !wnd.ShowModal((DependencyObject)this) )
//                return (Security)null;
//            security2 = wnd.SelectedSecurity;
//        }
//        return security2;
//    }

//    private void OnUngroupEvent(ChartArea _param1)
//    {
//        if ( _param1 == null )
//            return;
//        _param1.GroupId = !StringHelper.IsEmpty(_param1.GroupId) ? string.Empty : Guid.NewGuid().ToString();
//        Action<IChartArea> zcHtgn6mNhxMm = this._iChartAreaEvent;
//        if ( zcHtgn6mNhxMm == null )
//            return;
//        zcHtgn6mNhxMm((IChartArea)_param1);
//    }

//    private void OnAreaAdding()
//    {
//        Chart.SomeInternalSealedClass00983 q4dXwYzLzYwDdBciE = new Chart.SomeInternalSealedClass00983();
//        ChartArea area = new ChartArea()
//        {
//            Title = $"{LocalizedStrings.Panel} {( this._iChartAreaList.Count + 1 ).ToString()}"
//        };
//        q4dXwYzLzYwDdBciE._timezoneInfo = this.GetTimeZoneInfo();
//        CollectionHelper.ForEach<IChartAxis>((IEnumerable<IChartAxis>)area.XAxises, new Action<IChartAxis>(q4dXwYzLzYwDdBciE.OnAreaYAxisesRemovingAt));
//        this.AddArea((IChartArea)area);
//    }

//    private void OnAddCandles(ChartArea _param1)
//    {
//        if ( this._subscription == null )
//            this._subscription = new Subscription((ISubscriptionMessage)this.DefaultCandlesSettings, (SecurityMessage)null);
//        CandleSettingsWindow wnd = new CandleSettingsWindow()
//        {
//            Subscription = ( (Cloneable<Subscription>)this._subscription ).Clone()
//        };
//        if ( this.SecurityProvider != null )
//            wnd.SecurityProvider = this.SecurityProvider;
//        if ( !wnd.ShowModal((DependencyObject)this) )
//            return;
//        Subscription subscription = wnd.Subscription;
//        this._subscription = subscription;
//        ChartCandleElement element = new ChartCandleElement()
//        {
//            PriceStep = (Decimal?)( (SecurityMessage)subscription?.MarketData )?.PriceStep,
//            DrawStyle = this.CandleDrawStyles()
//        };
//        this.AddElement((IChartArea)_param1, (IChartCandleElement)element, subscription);
//    }

//    private void OnAddIndicator(ChartArea _param1)
//    {
//        IndicatorPickerWindow wnd1 = new IndicatorPickerWindow()
//        {
//            AutoSelectCandles = true,
//            IndicatorTypes = (IEnumerable<IndicatorType>)this.IndicatorTypes
//        };
//        if ( !wnd1.ShowModal((DependencyObject)this) )
//            return;
//        IChartCandleElement[] array = this.GetElements<IChartCandleElement>().ToArray<IChartCandleElement>();
//        IChartCandleElement element1 = ( (IEnumerable)_param1.Elements ).OfType<IChartCandleElement>().Concat<IChartCandleElement>((IEnumerable<IChartCandleElement>)array).FirstOrDefault<IChartCandleElement>();
//        if ( element1 == null )
//        {
//            int num = (int)new MessageBoxBuilder().Error().Text(LocalizedStrings.NoData2).Owner((DependencyObject)this).Show();
//        }
//        else
//        {
//            if ( !wnd1.AutoSelectCandles )
//            {
//                ChartCandleElementPicker wnd2 = new ChartCandleElementPicker()
//                {
//                    Elements = (IEnumerable<IChartCandleElement>)array,
//                    SelectedElement = element1
//                };
//                if ( !wnd2.ShowModal((DependencyObject)this) )
//                    return;
//                element1 = wnd2.SelectedElement;
//            }
//            ChartIndicatorElement element2 = new ChartIndicatorElement()
//            {
//                IndicatorPainter = wnd1.SelectedIndicatorType.CreatePainter(),
//                AutoAssignYAxis = true
//            };
//            this.AddElement((IChartArea)_param1, (IChartIndicatorElement)element2, this.TryGetSubscription((IChartElement)element1), wnd1.Indicator);
//        }
//    }

//    private void OnAddOrders(ChartArea _param1)
//    {
//        Security security = this.GetSelectedSecurity(_param1);
//        if ( security == null )
//            return;
//        this.AddElement((IChartArea)_param1, (IChartOrderElement)new ChartOrderElement(), new Subscription(DataType.Transactions, security));
//    }

//    private void OnAddTrades(ChartArea _param1)
//    {
//        Security security = this.GetSelectedSecurity(_param1);
//        if ( security == null )
//            return;
//        this.AddElement((IChartArea)_param1, (IChartTradeElement)new ChartTradeElement(), new Subscription(DataType.Transactions, security));
//    }

//    private void SomeInternalFunction34382084(string _param1)
//    {
//        ( (INotifyPropertyChangedEx)this ).NotifyPropertyChanged(_param1);
//    }

//    private void OnRemoveElements(IChartElement _param1)
//    {
//        Action<IChartElement> z9PnYjM29SjfT = this.RemoveElement;
//        if ( z9PnYjM29SjfT == null )
//            return;
//        z9PnYjM29SjfT(_param1);
//    }

//    private void InternalDoSomeSubscriptionStuff31341(IChartElement _param1)
//    {
//        this.InternalDoSomeSubscriptionStuff093(_param1, this.TryGetSubscription(_param1));
//    }

//    [Serializable]
//    private sealed class SomeClass34343383
//    {
//        public static readonly Chart.SomeClass34343383 SomeMethond0343 = new Chart.SomeClass34343383();
//        public static Func<IChartArea,
//#nullable enable
//        IEnumerable<
//#nullable disable
//        IChartElement>> SomeShit77Mn;
//        public static Func<IChartAxis, bool> _member01;
//        public static Func<IChartArea, IChartAxis> SomeShitMethodOrSomeWhate;
//        public static Func<IChartAxis, bool> _member02;
//        public static Func<KeyValuePair<IChartElement, Subscription>, IChartIndicatorElement> _member03;
//        public static Func<IChartIndicatorElement,
//#nullable enable
//        IChartIndicatorElement> _member04;
//        public static
//#nullable disable
//        Action<Chart.SomeInternalSealedClass897634> _member05;
//        public static Func<IChartArea, SettingsStorage> _member06;

//        public
//#nullable enable
//        IEnumerable<
//#nullable disable
//        IChartElement> SomeMethodPJeLPTCg(IChartArea _param1)
//        {
//            return (IEnumerable<IChartElement>)_param1.Elements;
//        }

//        public IChartAxis Method01(IChartArea _param1)
//        {
//            return ( (IEnumerable<IChartAxis>)_param1.XAxises ).FirstOrDefault<IChartAxis>(Chart.SomeClass34343383._member01 ?? ( Chart.SomeClass34343383._member01 = new Func<IChartAxis, bool>(Chart.SomeClass34343383.SomeMethond0343.Method02) ));
//        }

//        public bool Method02(IChartAxis _param1)
//        {
//            return _param1.TimeZone != null;
//        }

//        public bool Method03(IChartAxis _param1) => _param1 != null;

//        public IChartIndicatorElement Method04(
//          KeyValuePair<IChartElement, Subscription> _param1)
//        {
//            return (IChartIndicatorElement)_param1.Key;
//        }

//        public
//#nullable enable
//        IChartIndicatorElement Method05(
//#nullable disable
//          IChartIndicatorElement _param1)
//        {
//            return _param1;
//        }

//        public void Method06(Chart.SomeInternalSealedClass897634 _param1)
//        {
//            _param1.Dispose();
//        }

//        public SettingsStorage SomeSettingsStorageHelpingFunction0342(IChartArea _param1)
//        {
//            SettingsStorage settingsStorage = PersistableHelper.Save((IPersistable)_param1);
//            settingsStorage.SetValue<double>("Height", ( (ChartArea)_param1 ).ViewModel.Height);
//            return settingsStorage;
//        }
//    }

//    private sealed class SomeInternalSealedClass00983
//    {
//        public TimeZoneInfo _timezoneInfo;

//        public void OnAreaYAxisesRemovingAt(IChartAxis _param1)
//        {
//            _param1.TimeZone = this._timezoneInfo;
//        }
//    }

//    private sealed class SomeInternalSealedClass082232
//    {
//        public Chart _variableSome3535;
//        public Func<Order, bool> m_public_Func_Order_bool_;

//        public void SomeCancelOrdersMethod03343()
//        {
//            this._variableSome3535.ViewModel.InternalExecuteCancelActiveOrders(this.m_public_Func_Order_bool_);
//        }
//    }



//    private sealed class SomeInternalSealedClass08343
//    {
//        public IChartArea _chartArea_093;
//        public Chart _variableSome3535;
//        public Action<IChartAxis> _Func_IAxis_bool_0835;

//        public void Method03()
//        {
//            if ( this._chartArea_093.Chart != null )
//                throw new ArgumentException("area.Chart != null", "area");
//            if ( this._chartArea_093 == null || this._variableSome3535._iChartAreaList.Contains(this._chartArea_093) )
//                throw new ArgumentException("area2");
//            ChartAxisType? xaxisType = this._variableSome3535._iChartAreaList.FirstOrDefault<IChartArea>()?.XAxisType;
//            if ( xaxisType.HasValue )
//            {
//                if ( CollectionHelper.IsEmpty<IChartElement>((ICollection<IChartElement>)this._chartArea_093.Elements) )
//                    this._chartArea_093.XAxisType = xaxisType.Value;
//                else if ( this._chartArea_093.XAxisType != xaxisType.Value )
//                    throw new InvalidOperationException(LocalizedStrings.InvalidAxisType);
//            }
//            CollectionHelper.ForEach<IChartAxis>((IEnumerable<IChartAxis>)this._chartArea_093.XAxises, this._Func_IAxis_bool_0835 ?? ( this._Func_IAxis_bool_0835 = new Action<IChartAxis>(this.SetSomeAutoRange) ));
//            this._chartArea_093.PropertyChanged += new PropertyChangedEventHandler(this._variableSome3535.OnHeightPropertyChanged);
//            ( (INotifyCollection<IChartElement>)this._chartArea_093.Elements ).Added += new Action<IChartElement>(this._variableSome3535.OnChartAreaElementAdded);
//            ( (INotifyCollection<IChartElement>)this._chartArea_093.Elements ).Removed += new Action<IChartElement>(this._variableSome3535.OnChartAreaElementRemoved);
//            this._variableSome3535._iChartAreaList.Add(this._chartArea_093);
//            this._chartArea_093.Chart = (IChart)this._variableSome3535;
//            this._variableSome3535.ViewModel.ChartPaneViewModels.Add(( (ChartArea)this._chartArea_093 ).ViewModel);
//            CollectionHelper.ForEach<IChartElement>((IEnumerable<IChartElement>)this._chartArea_093.Elements, new Action<IChartElement>(this._variableSome3535.OnChartAreaElementAdded));
//            Action<IChartArea> zk8cjLwfRrDki = this._variableSome3535.\u0023\u003Dzk8cjLWfRrDKI;
//            if ( zk8cjLwfRrDki == null )
//                return;
//            zk8cjLwfRrDki(this._chartArea_093);
//        }

//        public void SetSomeAutoRange(IChartAxis _param1)
//        {
//            // ISSUE: explicit non-virtual call
//            _param1.AutoRange = (this._variableSome3535.IsAutoRange);
//        }
//    }

//    private sealed class SomeInternalClass033438
//    {
//        public Subscription _subscription;
//        public IChartCandleElement _someChartElement0343123;
//        public Chart _variableSome3535;

//        public bool Method02341(
//          KeyValuePair<IChartElement, Subscription> _param1)
//        {
//            return _param1.Value == this._subscription && _param1.Key != this._someChartElement0343123;
//        }

//        public Tuple<IIndicator, IChartArea> Method02342(
//          IChartIndicatorElement _param1)
//        {
//            return Tuple.Create<IIndicator, IChartArea>(this._variableSome3535.GetIndicatorElement(_param1), _param1.ChartArea);
//        }
//    }



//    private sealed class SomeInternalClass033437
//    {
//        public Subscription _subscription;
//        public Chart _variableSome3535;

//        public bool Method023(IChartElement _param1)
//        {
//            // ISSUE: explicit non-virtual call
//            return (this._variableSome3535.TryGetSubscription(_param1)) == this._subscription;
//        }
//    }



//    private sealed class SomeInternalClass033436
//    {
//        public Chart _variableSome3535;
//        public IEnumerable<IChartElement> _someChartElements3432123;

//        public void Method01()
//        {
//            foreach ( ChartArea chartArea in this._variableSome3535._iChartAreaList )
//                chartArea.ViewModel.Reset(this._someChartElements3432123);
//        }
//    }

//    private sealed class SomeInternalClass033435
//    {
//        public SciChartSurface _drawingSurface;

//        public void SetDataContext()
//        {
//            ( (ScichartSurfaceMVVM)this._drawingSurface.DataContext ).SetScichartSurface(this._drawingSurface);
//        }

//        public void Method02(
//          object _param1,
//          DependencyPropertyChangedEventArgs _param2)
//        {
//            this.SetDataContext();
//        }
//    }

//    private sealed class SomeInternalClass033434
//    {
//        public IChartArea _chartArea_093;
//        public IChartElement _someChartElement0343123;

//        public bool Method0834()
//        {
//            return ( (ICollection<IChartElement>)this._chartArea_093.Elements ).Remove(this._someChartElement0343123);
//        }
//    }

//    private sealed class SomeInternalClass03850835
//    {
//        public Chart _variableSome3535;
//        public IChartArea _chartArea_093;

//        public void Method0833421()
//        {
//            if ( !this._variableSome3535._iChartAreaList.Remove(this._chartArea_093) )
//                return;
//            this._chartArea_093.PropertyChanged -= new PropertyChangedEventHandler(this._variableSome3535.OnHeightPropertyChanged);
//            ( (INotifyCollection<IChartElement>)this._chartArea_093.Elements ).Added -= new Action<IChartElement>(this._variableSome3535.OnChartAreaElementAdded);
//            ( (INotifyCollection<IChartElement>)this._chartArea_093.Elements ).Removed -= new Action<IChartElement>(this._variableSome3535.OnChartAreaElementRemoved);
//            this._variableSome3535.ViewModel.ChartPaneViewModels.Remove(( (ChartArea)this._chartArea_093 ).ViewModel);
//            CollectionHelper.ForEach<IChartElement>((IEnumerable<IChartElement>)this._chartArea_093.Elements, new Action<IChartElement>(this._variableSome3535.OnChartAreaElementRemoved));
//            this._chartArea_093.Chart = (IChart)null;
//            TypeHelper.DoDispose<IChartArea>(this._chartArea_093);
//            Action<IChartArea> z0aBkRs4Mkj0a = this._variableSome3535.\u0023\u003Dz0aBkRs4Mkj0a;
//            if ( z0aBkRs4Mkj0a != null )
//                z0aBkRs4Mkj0a(this._chartArea_093);
//            if ( !CollectionHelper.IsEmpty<IChartArea>((ICollection<IChartArea>)this._variableSome3535._iChartAreaList) )
//                return;
//            this._variableSome3535.ViewModel.InitRangeDepProperty();
//        }
//    }
//}
