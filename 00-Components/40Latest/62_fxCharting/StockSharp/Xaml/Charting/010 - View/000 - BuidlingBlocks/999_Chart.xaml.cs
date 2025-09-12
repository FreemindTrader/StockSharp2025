using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Serialization;
using Ecng.Xaml;
using SciChart.Charting.Visuals.TradeChart;
using SciChart.Charting.Visuals;
using SciChart.Data.Model;
using StockSharp.Algo;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Xml.Linq;
using StockSharp.Xaml.Charting.IndicatorPainters;
using DevExpress.Charts.Model;

#nullable enable
namespace StockSharp.Xaml.Charting;

/// <summary>The graphic component of the candles charts display.</summary>
/// <summary>Chart</summary>
public partial class Chart : UserControl,
                              INotifyPropertyChanged,
                              IChart,
                              IPersistable,
                              IChartBuilder,
                              IThemeableChart,
                              INotifyPropertyChangedEx,
                              IWpfChart
{
    private sealed class ChartIndicatorAndUI : Disposable
    {

        private readonly Chart _chart;

        private readonly IChartIndicatorElement _indicatorUI;

        private readonly IIndicator _indicator;

        public ChartIndicatorAndUI( Chart chart, IChartIndicatorElement indicatorUI, IIndicator indicator )
        {
            _chart       = chart ?? throw new ArgumentNullException( "parent" );
            _indicatorUI = indicatorUI ?? throw new ArgumentNullException( "element" );
            _indicator   = indicator ?? throw new ArgumentNullException( "indicator" );

            Indicator.Reseted += new Action( OnResetCallback );
        }

        public IIndicator Indicator => _indicator;

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            Indicator.Reseted -= OnResetCallback;
        }

        private void OnResetCallback()
        {
            if ( _chart.DisableIndicatorReset )
                return;
            _chart.ResetIndicator( _indicatorUI, Indicator );
        }
    }


    private static int staticChartCount;

    private readonly int _instanceCount = ++Chart.staticChartCount;

    private readonly ChartBuilder _chartBuilder = new ChartBuilder();

    private Subscription? _subscription;

    private ChartCandleDrawStyles _chartCandleDrawStyles;

    private MarketDataMessage _defaultCandlesSettings = new MarketDataMessage()
    {
        DataType2 = StockSharp.Messages.Extensions.TimeFrame(TimeSpan.FromMinutes(5.0))
    };

    private readonly ChartViewModel _chartSurfaceVM;

    private readonly SynchronizedDictionary<IChartIndicatorElement, ChartIndicatorAndUI> _indicatorUITupleMap = new SynchronizedDictionary<IChartIndicatorElement, ChartIndicatorAndUI>();

    private readonly SynchronizedDictionary<IChartElement, Subscription> _chartElement2SubsMap = new SynchronizedDictionary<IChartElement, Subscription>();

    private ChartAnnotationTypes _annotationType;

    private bool _isAutoScroll = true;

    private bool _crossHairAxisLabels = true;

    private bool _crossHairTooltip;

    private bool _crossHair = true;

    private bool _orderCreationMode;

    private bool _isAutoRange;

    private bool _autoRangeByAnnotations;

    private bool _showPerfStats;

    private readonly Dictionary<(IChartArea, IndicatorMeasures), string> _area2IndicatorMeasuresMap = new Dictionary<(IChartArea, IndicatorMeasures), string>();

    private TimeSpan _autoRangeIntervalNoGroup = TimeSpan.FromMilliseconds(200.0);

    private readonly List<IChartArea> _iChartAreaList = new List<IChartArea>();

    private ISecurityProvider? _securityProvider;

    private bool _disableIndicatorReset;

    private TimeZoneInfo _timeZone = TimeZoneInfo.Local;

    private bool _showNonFormedIndicators;

    private Security? _security;

    private Subscription? _candleSubscription;

    private Action<IChartArea>? UngroupEvent;

    private Action? ResetSubscriptionEvent;

    private PropertyChangedEventHandler? PropertyChangedEvent;

    public SciChartGroup? _scichartGroup;

    public event Action<IChartArea>? AreaAdded;

    public event Action<IChartArea>? AreaRemoved;
    /// <summary>The new order creation event.</summary>
    public event Action < ChartArea, Order > ? CreateOrder;

    /// <summary>Move order event.</summary>
    public event Action < Order, Decimal > ? MoveOrder;

    /// <summary>Cancel order event.</summary>
    public event Action < Order > ? CancelOrder;

    /// <summary>Annotation created event.</summary>
    public event Action < IChartAnnotationElement > ? AnnotationCreated;

    /// <summary>Annotation Modified event.</summary>
    public event Action<IChartAnnotationElement, ChartDrawData.AnnotationData>? AnnotationModified;

    /// <summary>Annotation deleted event.</summary>
    public event Action < IChartAnnotationElement > ? AnnotationDeleted;

    /// <summary>Annotation selection event.</summary>
    public event Action < IChartAnnotationElement, ChartDrawData.AnnotationData > ? AnnotationSelected;

    /// <summary>
    /// The event of the subscription to getting data for the element.
    /// </summary>
    public event Action < IChartCandleElement, Subscription > ? SubscribeCandleElement;

    /// <summary>
    /// The event of the subscription to getting data for the Indicator Chart element.
    /// </summary>
    public event Action < IChartIndicatorElement, Subscription, IIndicator > ? SubscribeIndicatorElement;

    /// <summary>
    /// The event of the subscription to getting data for the Order Chart Element UI.
    /// </summary>
    public event Action < IChartOrderElement, Subscription > ? SubscribeOrderElement;

    /// <summary>
    /// The event of the subscription to getting data for the Trade Chart Element UI.
    /// </summary>
    public event Action < IChartTradeElement, Subscription > ? SubscribeTradeElement;

    /// <summary>
    /// The event of the unsubscribe from getting data for the element.
    /// </summary>
    public event Action < IChartElement > ? UnSubscribeElement;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.Chart" />.
    /// </summary>
    public Chart()
    {
        InitializeComponent();

        DataContext = _chartSurfaceVM = new ChartViewModel();

        ViewModel.CancelActiveOrderEvent += InvokeCancelOrderEvent;
        ViewModel.UngroupEvent           += OnUngroupEvent;
        AreaAdding                       += OnAreaAdding;
        AddCandles                       += OnAddCandles;
        RebuildCandles                   += OnRebuildCandles;
        AddIndicator                     += OnAddIndicator;
        AddOrders                        += OnAddOrders;
        AddTrades                        += OnAddTrades;
        RemoveElement                    += OnRemoveElement;

        var mySettings = new string[7]
        {
              nameof (IsInteracted),
              nameof (AllowAddArea),
              nameof (AllowAddAxis),
              nameof (AllowAddCandles),
              nameof (AllowAddIndicators),
              nameof (AllowAddOrders),
              nameof (AllowAddOwnTrades)
        };

        ChartViewModel.InteractedEvent += () =>
        {
            CollectionHelper.ForEach<string>( mySettings, p => ( ( INotifyPropertyChangedEx ) this ).NotifyPropertyChanged( p ) );
        };

        if ( IChartExtensions.TryIndicatorProvider == null )
        {
            IndicatorProvider indicatorProvider = new IndicatorProvider();
            indicatorProvider.Init();
            ConfigManager.RegisterService( ( IIndicatorProvider ) indicatorProvider );
        }

        if ( IChartExtensions.TryIndicatorPainterProvider != null )
            return;
        
        var provider = new IndicatorColorProvider();
        
        ( ( IChartIndicatorPainterProvider ) provider ).Init();
        ConfigManager.RegisterService( ( IChartIndicatorPainterProvider ) provider );
    }

    public int GetInstanceCount() => _instanceCount;

    /// <summary>
    /// The style of drawing candles. The default is <see cref="F:StockSharp.Charting.ChartCandleDrawStyles.Stick" />.
    /// </summary>
    public ChartCandleDrawStyles CandleDrawStyles
    {
        get
        {
            return _chartCandleDrawStyles;
        }

        set
        {
            _chartCandleDrawStyles = value;
        }
    }


    /// <summary>
    /// The default settings of candles, which are used when adding new candles element without specifying the settings explicitly.
    /// The default is 5 minutes bars
    /// </summary>
    public MarketDataMessage DefaultCandlesSettings
    {
        get => _defaultCandlesSettings;
        set
        {
            _defaultCandlesSettings = value ?? throw new ArgumentNullException( nameof( value ) );
        }
    }

    public ChartViewModel ViewModel
    {
        get
        {
            return _chartSurfaceVM;
        }
    }

    public IEnumerable<IChartArea> Areas => ( IEnumerable<IChartArea> ) _iChartAreaList;

    public bool IsAutoScroll
    {
        get => _isAutoScroll;
        set
        {
            _isAutoScroll = value;
            NotifyPropertyChangedExHelper.Notify<Chart>( this, nameof( IsAutoScroll ) );
        }
    }

    public bool IsAutoRange
    {
        get => _isAutoRange;
        set
        {
            _isAutoRange = value;
            foreach ( IChartArea area in Areas )
                area.SetAutoRange( value );
            NotifyPropertyChangedExHelper.Notify<Chart>( this, nameof( IsAutoRange ) );
        }
    }

    /// <summary>
    /// The time interval for updating the visible range of the X-axis when <see cref="P:StockSharp.Charting.IChart.IsAutoRange" /> is enabled.
    /// 
    /// Chart range/scroll interval. Default is 200ms.
    /// </summary>
    public TimeSpan AutoRangeInterval
    {
        get => _autoRangeIntervalNoGroup;
        set
        {
            _autoRangeIntervalNoGroup = !( value <= TimeSpan.Zero ) ? value : throw new ArgumentOutOfRangeException( nameof( value ), value, LocalizedStrings.InvalidValue );
            NotifyPropertyChangedExHelper.Notify<Chart>( this, nameof( AutoRangeInterval ) );
        }
    }


    /// <summary>
    /// The provider of information about instruments.
    /// </summary>
    public ISecurityProvider? SecurityProvider
    {
        get => _securityProvider;
        set => _securityProvider = value;
    }

    /// <summary>
    /// Disable indicator reset on settings change. The default is <see langword="false" />.
    /// Disable tracking indicator reset.
    /// </summary>
    public bool DisableIndicatorReset
    {
        get => _disableIndicatorReset;
        set => _disableIndicatorReset = value;
    }

    /// <summary>
    /// To add a new chart area. The area is added at the bottom of the chart.
    /// 
    /// This area is normally used to display technical indicators.
    /// 
    /// </summary>
    /// <param name="area"></param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public void AddArea( IChartArea area )
    {
        ( ( DispatcherObject ) this ).GuiSync( 
            () =>
            {
                if ( area.Chart != null )
                    throw new ArgumentException( "area.Chart != null", "area" );

                if ( area == null || _iChartAreaList.Contains( area ) )
                    throw new ArgumentException( "area2" );

                // The first area is normally the candlestick display area.
                ChartAxisType? xaxisType = _iChartAreaList.FirstOrDefault<IChartArea>()?.XAxisType;

                if ( xaxisType.HasValue )
                {
                    // This area shouldn't have indicators or candlesticks at this time.
                    if ( CollectionHelper.IsEmpty<IChartElement>( area.Elements ) )
                    {
                        area.XAxisType = xaxisType.Value;
                    }
                    else if ( area.XAxisType != xaxisType.Value )
                    {
                        throw new InvalidOperationException( LocalizedStrings.InvalidAxisType );
                    }

                }

                // for this particular area, set the auto range property
                CollectionHelper.ForEach<IChartAxis>( area.XAxises, p => p.AutoRange = IsAutoRange );

                // And the corresponding event handlers for
                //      i) height change
                //      ii) add some new indicators.
                //      iii) remove some indicators.
                area.PropertyChanged  += OnHeightPropertyChanged;
                area.Elements.Added   += OnChartAreaElementAdded;
                area.Elements.Removed += OnChartAreaElementRemoved;

                _iChartAreaList.Add( area );

                area.Chart = ( IChart ) this;
                ViewModel.ChartPaneViewModels.Add( ( ( ChartArea ) area ).ViewModel );

                CollectionHelper.ForEach<IChartElement>( ( IEnumerable<IChartElement> ) area.Elements, new Action<IChartElement>( OnChartAreaElementAdded ) );

                AreaAdded?.Invoke( area );
            } );
    }


    /// <summary>
    /// To remove the chart area. Undo all the action on <see cref="M:StockSharp.Xaml.Charting.Chart.AddArea(StockSharp.Charting.IChartArea)" />.
    /// </summary>
    /// <param name="area"></param>
    public void RemoveArea( IChartArea area )
    {
        ( ( DispatcherObject ) this ).GuiSync( () =>
                                            {
                                                if ( !_iChartAreaList.Remove( area ) )
                                                    return;

                                                area.PropertyChanged  -= OnHeightPropertyChanged;
                                                area.Elements.Added   -= OnChartAreaElementAdded;
                                                area.Elements.Removed -= OnChartAreaElementRemoved;
                                                ViewModel.ChartPaneViewModels.Remove( ( ( ChartArea ) area ).ViewModel );
                                                CollectionHelper.ForEach<IChartElement>( area.Elements, new Action<IChartElement>( OnChartAreaElementRemoved ) );

                                                area.Chart = null;
                                                TypeHelper.DoDispose<IChartArea>( area );

                                                AreaRemoved?.Invoke( area );

                                                if ( !CollectionHelper.IsEmpty<IChartArea>( _iChartAreaList ) )
                                                    return;

                                                ViewModel.InitRangeDepProperty();
                                            }
        );
    }

    private void OnHeightPropertyChanged( object area, PropertyChangedEventArgs e )
    {
        ChartArea chartArea = (ChartArea)area;
        if ( e.PropertyName != "Height" )
            return;

        chartArea.ViewModel.Height = chartArea.Height;
    }


    /// <summary>
    /// The core of AddElement. To add an element to the chart. we have to do it on the GUI thread.
    /// </summary>
    /// <param name="area"></param>
    /// <param name="element"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void AddElement( IChartArea area, IChartElement element )
    {
        if ( area == null )
        {
            throw new ArgumentNullException( nameof( area ) );
        }

        if ( element == null )
        {
            throw new ArgumentNullException( nameof( element ) );
        }

        ( ( DispatcherObject ) this ).GuiSync( () =>
                                            {                                                
                                                if ( 
                                                       element is IChartIndicatorElement myIndicator &&                         // If it is indicator
                                                       myIndicator.AutoAssignYAxis &&                                           // and the YAxis is auto-assign
                                                       _indicatorUITupleMap.TryGetValue( myIndicator, out var imap ) && 
                                                       imap.Indicator.Measure != IndicatorMeasures.Price                        // and the indicator is not price measure (price measure always use the main Y axis)
                                                   )
                                                {
                                                    (IChartArea, IndicatorMeasures) key = (area, imap.Indicator.Measure);
                                                    
                                                    if ( !_area2IndicatorMeasuresMap.TryGetValue( key, out var measure ) )
                                                    {
                                                        measure       = $"{"Y"}({Guid.NewGuid()})";                             // Since it is not price measure, 

                                                        var axis      = CreateAxis();                                           // We create another y-axis based on Percent, MinusOnePlusOne, Volume
                                                        axis.Id       = measure;
                                                        axis.AxisType = ChartAxisType.Numeric;

                                                        area.YAxises.Add( axis );                                               // we add another Y-axis to the chart

                                                        _area2IndicatorMeasuresMap.Add( key, axis.Id );
                                                    }
                                                    element.YAxisId = measure;
                                                }

                                                area.Elements.Add( element );
                                            }
                                        );
    }


    /// <summary>To add an element to the chart.</summary>
    /// <param name="area">Chart area.</param>
    /// <param name="element">The chart element.</param>
    /// <param name="subscription">Subscription.</param>
    public void AddElement( IChartArea area, IChartCandleElement element, Subscription subscription )
    {
        if ( area == null )
            throw new ArgumentNullException( nameof( area ) );
        if ( element == null )
            throw new ArgumentNullException( nameof( element ) );
        if ( subscription == null )
            throw new ArgumentNullException( nameof( subscription ) );
        _chartElement2SubsMap.Add( ( IChartElement ) element, subscription );
        AddElement( area, ( IChartElement ) element );
    }


    /// <summary>To add an Indicator element to the chart.</summary>
    /// <param name="area">Chart area.</param>
    /// <param name="indicatorUI">The chart element.</param>
    /// <param name="subscription">Subscription.</param>
    public void AddElement( IChartArea area, IChartIndicatorElement indicatorUI, Subscription subscription, IIndicator indicator )
    {
        if ( area == null )
            throw new ArgumentNullException( nameof( area ) );

        if ( indicatorUI == null )
            throw new ArgumentNullException( nameof( indicatorUI ) );

        if ( indicator == null )
            throw new ArgumentNullException( nameof( indicator ) );

        if ( subscription != null )
            _chartElement2SubsMap.Add( indicatorUI, subscription );

        _indicatorUITupleMap.Add( indicatorUI, new ChartIndicatorAndUI( this, indicatorUI, indicator ) );

        // Create an Indicator Painter from the indicator UI
        ( ( ChartIndicatorElement ) indicatorUI ).CreateIndicatorPainter( IndicatorTypes, indicator );

        AddElement( area, indicatorUI );
    }

    /// <summary>
    /// Add an Order UI to the chart.
    /// </summary>
    /// <param name="area"></param>
    /// <param name="element"></param>
    /// <param name="subscription"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void AddElement( IChartArea area, IChartOrderElement element, Subscription subscription )
    {
        if ( area == null )
            throw new ArgumentNullException( nameof( area ) );
        if ( element == null )
            throw new ArgumentNullException( nameof( element ) );
        if ( subscription == null )
            throw new ArgumentNullException( nameof( subscription ) );

        _chartElement2SubsMap.Add( element, subscription );
        
        AddElement( area, element );
    }

    /// <summary>
    /// Add a Trade UI to the chart
    /// </summary>
    /// <param name="area"></param>
    /// <param name="element"></param>
    /// <param name="subscription"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void AddElement( IChartArea area, IChartTradeElement element, Subscription subscription )
    {
        if ( area == null )
            throw new ArgumentNullException( nameof( area ) );
        if ( element == null )
            throw new ArgumentNullException( nameof( element ) );
        if ( subscription == null )
            throw new ArgumentNullException( nameof( subscription ) );
        _chartElement2SubsMap.Add( element, subscription );
        AddElement( area, element );
    }

    /// <summary>
    /// Remove an element from a certain chart area.
    /// </summary>
    /// <param name="area"></param>
    /// <param name="element"></param>
    /// <exception cref="ArgumentNullException"></exception>
    void IChart.RemoveElement( IChartArea area, IChartElement element )
    {
        if ( area == null )
            throw new ArgumentNullException( nameof( area ) );

        if ( element == null )
            throw new ArgumentNullException( nameof( element ) );
        
        if ( element is IChartIndicatorElement indicatorUI && _indicatorUITupleMap.TryGetValue( indicatorUI, out var chartIndicatorAndUI ) )
        {
            chartIndicatorAndUI.Dispose();
            _indicatorUITupleMap.Remove( indicatorUI );
        }

        ( ( DispatcherObject ) this ).GuiSync<bool>(
                                                    () =>
                                                        {
                                                            return ( ( ICollection<IChartElement> ) area.Elements ).Remove( element );
                                                        }
                                                    );

        _chartElement2SubsMap.Remove( element );
    }


    /// <summary>
    /// To get an indicator which is associated with <see cref="T:StockSharp.Charting.IChartIndicatorElement" />.
    /// </summary>
    /// <param name="indicatorUI">The chart element.</param>
    /// <returns>Indicator.</returns>
    public IIndicator GetIndicatorElement( IChartIndicatorElement indicatorUI )
    {
        return CollectionHelper.TryGetValue( _indicatorUITupleMap, indicatorUI ).Indicator;
    }

    public Subscription TryGetSubscription( IChartElement element )
    {
        return CollectionHelper.TryGetValue( _chartElement2SubsMap, element );
    }

    /// <summary>
    /// The most current subscrption and security should be retrieved from the areas and elements.
    /// </summary>
    /// <returns></returns>
    private (IChartCandleElement, Subscription) GetCandleAndSubscriptionFromAreas()
    {
        foreach ( IChartElement chartElement in Areas.SelectMany( p => p.Elements ) )
        {
            if ( chartElement is IChartCandleElement candle )
            {
                Subscription subscription = TryGetSubscription((IChartElement)candle);

                if ( subscription != null && subscription.SecurityId.HasValue )
                {
                    return (candle, subscription);
                }

            }
        }

        return default;
    }

    /// <summary>
    /// Get the current candle subscription from the chart areas.
    /// </summary>
    private void UpdateSecurityAndSubscriptionFromAreas()
    {
        // Get the most current candle and subscription from the areas.
        var newCandleSub = GetCandleAndSubscriptionFromAreas().Item2;

        var oldCandleSub = GetCandleSubscription();
        var oldSecurity  = GetSecurity();

        if ( oldCandleSub == newCandleSub && oldSecurity == ( newCandleSub != null ? newCandleSub.TryGetSecurity() : null ) )
            return;

        SetCandleSubscription( newCandleSub );

        Subscription candleSubscrption = GetCandleSubscription();
        SetSecurity( candleSubscrption != null ? candleSubscrption.TryGetSecurity() : null );

        ResetSubscriptionEvent?.Invoke();
    }


    /// <summary>
    /// Set the subscription for the element. This method is used when the subscription is changed.
    /// </summary>
    /// <param name="chartUI"></param>
    /// <param name="subscription"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void SetSubscription( IChartElement chartUI, Subscription subscription )
    {        
        _chartElement2SubsMap[chartUI] = subscription ?? throw new ArgumentNullException( nameof( subscription ) );
        
        ( ( IChartComponent ) chartUI ).ResetUI();
    }


    /// <summary>
    /// Cancel orders that were added to this instance of <see cref="T:StockSharp.Xaml.Charting.Chart" />.
    /// This method does not cancel orders by itself. It just notifies subscribers with <see cref="E:StockSharp.Xaml.Charting.Chart.CancelOrder" /> event.
    /// </summary>
    public void CancelOrders( Func<Order, bool> predicate = null )
    {
        ( ( DispatcherObject ) this ).GuiSync( () =>
                                                {
                                                    ViewModel.InternalExecuteCancelActiveOrders( predicate );
                                                }

                                           );
    }


    /// <summary>
    /// To use annotations to define the visible range for the Y-axis.
    /// </summary>
    public bool AutoRangeByAnnotations
    {
        get => _autoRangeByAnnotations;
        set
        {
            if ( _autoRangeByAnnotations == value )
                return;
            _autoRangeByAnnotations = value;
            NotifyPropertyChangedExHelper.Notify<Chart>( this, nameof( AutoRangeByAnnotations ) );
        }
    }


    /// <summary>The minimum number of visible candles.</summary>
    public int MinimumRange
    {
        get => ViewModel.MinimumRange;
        set => ViewModel.MinimumRange = value;
    }

    public string ChartTheme
    {
        get => ViewModel.SelectedTheme;
        set => ViewModel.SelectedTheme = value;
    }

    public bool ShowLegend
    {
        get => ViewModel.ShowLegend;
        set => ViewModel.ShowLegend = value;
    }

    public bool ShowOverview
    {
        get => ViewModel.ShowOverview;
        set => ViewModel.ShowOverview = value;
    }

    public bool ShowPerfStats
    {
        get => _showPerfStats;
        set
        {
            if ( _showPerfStats == value )
                return;
            _showPerfStats = value;
            NotifyPropertyChangedExHelper.Notify<Chart>( this, nameof( ShowPerfStats ) );
        }
    }

    public bool IsInteracted
    {
        get => ViewModel.IsInteracted;
        set => ViewModel.IsInteracted = value;
    }

    /// <summary>Allow user to add new chart area.</summary>
    public bool AllowAddArea
    {
        get => ViewModel.AllowAddArea;
        set => ViewModel.AllowAddArea = value;
    }


    /// <summary>Allow user to add new chart axis.</summary>
    public bool AllowAddAxis
    {
        get => ViewModel.AllowAddAxis;
        set => ViewModel.AllowAddAxis = value;
    }

    /// <summary>Allow user to add new candles elements.</summary>
    public bool AllowAddCandles
    {
        get => ViewModel.AllowAddCandles;
        set => ViewModel.AllowAddCandles = value;
    }

    /// <summary>Allow user to add new indicator elements.</summary>
    public bool AllowAddIndicators
    {
        get => ViewModel.AllowAddIndicators;
        set => ViewModel.AllowAddIndicators = value;
    }

    /// <summary>Allow user to add new orders elements.</summary>
    public bool AllowAddOrders
    {
        get => ViewModel.AllowAddOrders;
        set => ViewModel.AllowAddOrders = value;
    }

    /// <summary>Allow user to add new own trades elements.</summary>
    public bool AllowAddOwnTrades
    {
        get => ViewModel.AllowAddOwnTrades;
        set => ViewModel.AllowAddOwnTrades = value;
    }

    public bool CrossHair
    {
        get => _crossHair;
        set
        {
            if ( _crossHair == value )
                return;
            _crossHair = value;
            NotifyPropertyChangedExHelper.Notify<Chart>( this, nameof( CrossHair ) );
        }
    }

    public bool CrossHairTooltip
    {
        get => _crossHairTooltip;
        set
        {
            if ( _crossHairTooltip == value )
                return;
            _crossHairTooltip = value;
            NotifyPropertyChangedExHelper.Notify<Chart>( this, nameof( CrossHairTooltip ) );
        }
    }

    public bool CrossHairAxisLabels
    {
        get => _crossHairAxisLabels;
        set
        {
            if ( _crossHairAxisLabels == value )
                return;
            _crossHairAxisLabels = value;
            NotifyPropertyChangedExHelper.Notify<Chart>( this, nameof( CrossHairAxisLabels ) );
        }
    }

    /// <summary>The prompt message type for drawing on the chart.</summary>
    public ChartAnnotationTypes AnnotationType
    {
        get => _annotationType;
        set
        {
            if ( _annotationType == value )
                return;
            _annotationType = value;
            NotifyPropertyChangedExHelper.Notify<Chart>( this, nameof( AnnotationType ) );
        }
    }

    public bool OrderCreationMode
    {
        get => _orderCreationMode;
        set
        {
            if ( _orderCreationMode == value )
                return;
            _orderCreationMode = value;
            NotifyPropertyChangedExHelper.Notify<Chart>( this, nameof( OrderCreationMode ) );
        }
    }

    public TimeZoneInfo TimeZone
    {
        get => _timeZone;
        set
        {
            if ( _timeZone == value )
                return;
            _timeZone = value ?? throw new ArgumentNullException( nameof( value ) );
            NotifyPropertyChangedExHelper.Notify<Chart>( this, nameof( TimeZone ) );
        }
    }

    public bool ShowNonFormedIndicators
    {
        get => _showNonFormedIndicators;
        set => _showNonFormedIndicators = value;
    }

    public IList<IndicatorType> IndicatorTypes
    {
        get => ( IList<IndicatorType> ) ViewModel.IndicatorTypes;
    }

    public Security? GetSecurity()
    {
        return _security;
    }

    private void SetSecurity( Security sec )
    {
        _security = sec;
    }

    public Subscription? GetCandleSubscription()
    {
        return _candleSubscription;
    }

    private void SetCandleSubscription( Subscription _param1 )
    {
        _candleSubscription = _param1;
    }

    public IEnumerable<Subscription> Subscriptions
    {
        get => _chartElement2SubsMap.Values.Distinct<Subscription>();
    }

    /// <summary>The chart area creation event.</summary>
    public event Action AreaAdding
    {
        add
        {
            ViewModel.AreaAddingEvent += value;
        }
        remove
        {
            ViewModel.AreaAddingEvent -= value;
        }
    }

    /// <summary>The Candle UI creation event.</summary>
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

    /// <summary>The Indicator UI creation event.</summary>
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

    /// <summary>The Orders UI creation event.</summary>
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

    /// <summary>The Trade UI creation event.</summary>
    public event Action<ChartArea> AddTrades
    {
        add => ViewModel.AddTradesEvent += value;
        remove
        {
            ViewModel.AddTradesEvent -= value;
        }
    }

    /// <summary>The chart element removal event.</summary>
    public event Action<IChartElement> RemoveElement
    {
        add => ViewModel.RemoveElementEvent+= value;
        remove => ViewModel.RemoveElementEvent -= value;
    }

    /// <summary>Rebuild candles event.</summary>
    public event Action<IChartElement, Subscription> RebuildCandles
    {
        add => ViewModel.RebuildCandlesEvent += value;
        remove => ViewModel.RebuildCandlesEvent -=value;
    }


    public event Action<IChartArea> Ungroup
    {
        add => UngroupEvent += value;
        remove => UngroupEvent -=value;
    }

    public event Action? ResetSubscription
    {
        add => ResetSubscriptionEvent += value;
        remove => ResetSubscriptionEvent -=value;
    }

    
    /// <summary>
    /// Reset all the indicators in all the areas with the new set of elements.
    /// </summary>
    /// <param name="elements"></param>
    public void Reset( IEnumerable<IChartElement> elements )
    {
        var chartElements = new List<IChartElement>();
        chartElements.AddRange( elements );

        ( ( DispatcherObject ) this ).GuiSync( () =>
        {
            foreach ( ChartArea area in _iChartAreaList )
            {
                area.ViewModel.Reset( chartElements );
            }                
        } );
    }

    public IChartDrawData CreateData() => ( IChartDrawData ) new ChartDrawData();

    public IChartArea CreateArea()
    {
        return ( this ).GuiSync( _chartBuilder.CreateArea );
    }

    public IChartAxis CreateAxis()
    {
        return ( this ).GuiSync( _chartBuilder.CreateAxis );        
    }

    public IChartCandleElement CreateCandleElement()
    {
        return ( this ).GuiSync( _chartBuilder.CreateCandleElement );        
    }

    public IChartIndicatorElement CreateIndicatorElement()
    {
        return ( this ).GuiSync( _chartBuilder.CreateIndicatorElement );        
    }

    public IChartActiveOrdersElement CreateActiveOrdersElement()
    {
        return ( this ).GuiSync( _chartBuilder.CreateActiveOrdersElement );        
    }

    public IChartAnnotationElement CreateAnnotation()
    {
        return ( this ).GuiSync( _chartBuilder.CreateAnnotation );        
    }

    public IChartBandElement CreateBandElement()
    {
        return ( this ).GuiSync( _chartBuilder.CreateBandElement );        
    }

    public IChartLineElement CreateLineElement()
    {
        return ( this ).GuiSync( _chartBuilder.CreateLineElement );        
    }

    public IChartLineElement CreateBubbleElement()
    {
        return ( this ).GuiSync( _chartBuilder.CreateBubbleElement );        
    }

    public IChartOrderElement CreateOrderElement()
    {
        return ( this ).GuiSync( _chartBuilder.CreateOrderElement );        
    }

    public IChartTradeElement CreateTradeElement()
    {
        return ( this ).GuiSync( _chartBuilder.CreateTradeElement );        
    }

    public void Draw( IChartDrawData data )
    {
        ChartDrawData drawData = data != null ? (ChartDrawData)data : throw new ArgumentNullException(nameof(data));
        
        foreach ( ChartArea chartArea in _iChartAreaList )
            chartArea.ViewModel.Draw( drawData );
    }

    public void InvokeCreateOrderEvent( ChartArea area, Order myOrder )
    {
        CreateOrder?.Invoke( area, myOrder );
    }

    public void InvokeMoveOrderEvent( Order myOrder, Decimal price )
    {
        MoveOrder?.Invoke( myOrder, price );
    }

    public void InvokeCancelOrderEvent( Order myOrder )
    {
        CancelOrder?.Invoke( myOrder );
    }

    public void InvokeAnnotationCreatedEvent( ChartAnnotation annotation )
    {
        AnnotationCreated?.Invoke( ( IChartAnnotationElement ) annotation );
    }

    public void InvokeAnnotationModifiedEvent( ChartAnnotation annotation, ChartDrawData.AnnotationData data )
    {
        AnnotationModified?.Invoke( ( IChartAnnotationElement ) annotation, data );
    }

    public void InvokeAnnotationDeletedEvent( ChartAnnotation annotation )
    {
        AnnotationDeleted?.Invoke( ( IChartAnnotationElement ) annotation );
    }

    public void InvokeAnnotationSelectedEvent( ChartAnnotation annotation, ChartDrawData.AnnotationData data )
    {
        AnnotationSelected?.Invoke( ( IChartAnnotationElement ) annotation, data );
    }

    public TimeZoneInfo GetTimeZoneInfo()
    {
        return Areas.Select( p => p.XAxises.FirstOrDefault<IChartAxis>( x => x.TimeZone != null ) ).LastOrDefault<IChartAxis>( a => a != null )?.TimeZone;
    }

    public void ChangeCandleTimeFrame( TimeSpan tf )
    {
        (IChartCandleElement candle, Subscription subscription) = GetCandleAndSubscriptionFromAreas();
        if ( candle == null )
            return;

        object period = subscription.DataType.Arg;

        if ( period != null && period.Equals( tf ) )
        {
            return;
        }

        OnRebuildCandles( candle, new Subscription( StockSharp.Messages.Extensions.TimeFrame( tf ), ( SecurityMessage ) subscription.MarketData ) );
    }

    /// <summary>
    /// The following will rebuild candles by removing all the candleUI and indicator UI
    /// and then re-add the new candleUI and indicator UI with the new subscription.
    /// </summary>
    /// <param name="candle"></param>
    /// <param name="subscription"></param>
    private void OnRebuildCandles( IChartElement candle, Subscription subscription )
    {
        var candleUi = candle as IChartCandleElement;

        if ( candleUi == null )
        {
            return;
        }

        var chartArea  = candleUi.ChartArea;
        var candleSub  = TryGetSubscription( candleUi );
        var indicators = _chartElement2SubsMap.Where( p => p.Value == candleSub && p.Key != candleUi ).Select( i => (IChartIndicatorElement)i.Key ).ToDictionary(p => p, i => Tuple.Create<IIndicator, IChartArea>(GetIndicatorElement(i), i.ChartArea));

        // The following remove all the CandleUI, indicatorUIs from the chart, but not remove the subscriptions.
        OnRemoveElement( candleUi );
        CollectionHelper.ForEach( indicators.Keys, OnRemoveElement );
        ( ( IChartComponent ) candleUi ).ResetUI();

        // Add a new CandleUI and subscription to the chart area
        AddElement( chartArea, candleUi, subscription );

        foreach ( var indicator in indicators )
        {
            // Add a new indicator to the chart area
            AddElement( indicator.Value.Item2, indicator.Key, subscription, indicator.Value.Item1 );
        }

        UpdateSecurityAndSubscriptionFromAreas();
    }

    private void OnRemoveElement( IChartElement element )
    {
        if ( element is ChartIndicatorElement indicator && indicator.ParentElement != null )
            element = indicator.ParentElement;

        ( ( IChart ) this ).RemoveElement( element.ChartArea, element );

        UpdateSecurityAndSubscriptionFromAreas();
    }

    private void InvokeUnscribeElement( IChartElement element )
    {
        if ( TryGetSubscription( element ) == null )
            return;

        UnSubscribeElement?.Invoke( element );
    }

    /// <summary>Load settings.</summary>
    /// <param name="storage">Settings storage.</param>
    public void Load( SettingsStorage storage )
    {
        IsAutoScroll           = storage.GetValue<bool>( "IsAutoScroll", IsAutoScroll );
        IsAutoRange            = storage.GetValue<bool>( "IsAutoRange", IsAutoRange );
        AutoRangeByAnnotations = storage.GetValue<bool>( "AutoRangeByAnnotations", AutoRangeByAnnotations );
        ShowOverview           = storage.GetValue<bool>( "ShowOverview", ShowOverview );
        ShowLegend             = storage.GetValue<bool>( "ShowLegend", ShowLegend );
        CrossHair              = storage.GetValue<bool>( "CrossHair", CrossHair );
        CrossHairTooltip       = storage.GetValue<bool>( "CrossHairTooltip", CrossHairTooltip );
        CrossHairAxisLabels    = storage.GetValue<bool>( "CrossHairAxisLabels", CrossHairAxisLabels );
        OrderCreationMode      = storage.GetValue<bool>( "OrderCreationMode", OrderCreationMode );
        TimeZone               = Converter.To<TimeZoneInfo>( storage.GetValue<string>( "TimeZone", null ) ) ?? TimeZone;
        ShowPerfStats          = storage.GetValue<bool>( "ShowPerfStats", ShowPerfStats );
        
        if ( !IsInteracted )
            return;
        _chartElement2SubsMap.Clear();
        CollectionHelper.ForEach( _indicatorUITupleMap.Values, i => i.Dispose() );
        _indicatorUITupleMap.Clear();

        var myArea = storage.GetValue<object>("Areas");

        if ( myArea == null )
            return;

        if ( myArea is SettingsStorage areaStorage )
        {
            myArea = areaStorage.GetValue<IEnumerable<SettingsStorage>>( "Areas" );
        }

        LoadAreasSettings( ( ( IEnumerable ) myArea ).Cast<SettingsStorage>() );
    }

    /// <summary>Save settings.</summary>
    /// <param name="storage">Settings storage.</param>
    public void Save( SettingsStorage storage )
    {
        storage.SetValue<bool>( "IsAutoScroll", IsAutoScroll );
        storage.SetValue<bool>( "IsAutoRange", IsAutoRange );
        storage.SetValue<bool>( "AutoRangeByAnnotations", AutoRangeByAnnotations );
        storage.SetValue<bool>( "ShowOverview", ShowOverview );
        storage.SetValue<bool>( "ShowLegend", ShowLegend );
        storage.SetValue<bool>( "CrossHair", CrossHair );
        storage.SetValue<bool>( "CrossHairTooltip", CrossHairTooltip );
        storage.SetValue<bool>( "CrossHairAxisLabels", CrossHairAxisLabels );
        storage.SetValue<bool>( "OrderCreationMode", OrderCreationMode );
        storage.SetValue<string>( "TimeZone", TimeZone?.Id );
        storage.SetValue<bool>( "ShowPerfStats", ShowPerfStats );
        if ( !IsInteracted )
            return;
        storage.SetValue<SettingsStorage[ ]>( "Areas", _iChartAreaList.Select( a =>
        {
            var set = PersistableHelper.Save((IPersistable)a);
            set.SetValue<double>( "Height", ( ( ChartArea ) a ).ViewModel.Height );
            return set;
        } ).ToArray<SettingsStorage>() );
    }

    /// <summary>To re-subscribe to getting data for all elements.</summary>
    public void ReSubscribeElements()
    {
        if ( !IsInteracted )
            return;

        foreach ( IChartElement element in IChartExtensions.GetElements( ( IChart ) this ) )
        {
            InvokeUnscribeElement( element );
            ProcessSubscription( element );
        }
    }

    private void LoadAreasSettings( IEnumerable<SettingsStorage> areaSettings )
    {
        _iChartAreaList.Clear();

        foreach ( SettingsStorage storage in areaSettings )
        {
            ChartArea area = new ChartArea();
            area.Load( storage );
            AddArea( ( IChartArea ) area );
            area.ViewModel.Height = storage.GetValue<double>( "Height", double.NaN );
        }
    }

    private void ProcessSubscription( IChartElement element )
    {
        Subscription subscription = TryGetSubscription(element);
        if ( subscription == null )
            return;
        RaiseChartElementSubscribedEvent( element, subscription );
    }

    private void RaiseChartElementSubscribedEvent( IChartElement element, Subscription subscription )
    {
        switch ( element )
        {
            case IChartCandleElement myCandle:
                SubscribeCandleElement?.Invoke( myCandle, subscription );
                break;

            case IChartIndicatorElement myIndicator:
                SubscribeIndicatorElement?.Invoke( myIndicator, subscription, GetIndicatorElement( myIndicator ) );
                break;

            case IChartOrderElement myOrder:
                SubscribeOrderElement?.Invoke( myOrder, subscription );
                break;

            case IChartTradeElement myTrade:
                SubscribeTradeElement?.Invoke( myTrade, subscription );
                break;
        }
    }



    private void OnChartAreaElementAdded( IChartElement element )
    {
        UpdateSecurityAndSubscriptionFromAreas();
        if ( !IsInteracted )
            return;
        ProcessSubscription( element );
    }

    private void OnChartAreaElementRemoved( IChartElement element )
    {
        UpdateSecurityAndSubscriptionFromAreas();
        InternalUnReSubscribeEvents( element, false );
    }

    /// <summary>
    /// The following function will unsubscribe the element first, then reset the element if resubscribe is true,
    /// </summary>
    /// <param name="element"></param>
    /// <param name="resubscribe"></param>
    private void InternalUnReSubscribeEvents( IChartElement element, bool resubscribe )
    {
        IChartElement[] chartElements;

        if ( element is IChartCandleElement )
        {
            var mySubscription = TryGetSubscription(element);
            var myCandles      = new List<IChartElement>();
            var myList         = new List<IChartElement>();
            myList.Add( element );

            myCandles.AddRange( IChartExtensions.GetElements( ( IChart ) this ).Where( e => TryGetSubscription( e ) == mySubscription ).Concat<IChartElement>( myList ).Distinct<IChartElement>() );
            chartElements      = myCandles.ToArray();
        }
        else
        {
            chartElements      = new IChartElement[1] { element };
        }

        if ( resubscribe )
        {
            if ( IsInteracted )
            {
                CollectionHelper.ForEach( chartElements, InvokeUnscribeElementNoChecking );
            }
                
            Reset( chartElements );

            if ( IsInteracted )
            {
                CollectionHelper.ForEach( chartElements, InvokeSubscriptionEvent );
            }
                
        }
        else
        {
            if ( IsInteracted )
            {
                CollectionHelper.ForEach( chartElements, InvokeUnscribeElement );
            }                
        }
    }

    public static void SetupScichartSurface( SciChartSurface s )
    {
        if ( s.DataContext != null )
        {
            ( ( ScichartSurfaceMVVM ) s.DataContext ).SetScichartSurface( s );
        }            
        else
        {
            s.DataContextChanged += ( o, e ) => ( ( ScichartSurfaceMVVM ) s.DataContext ).SetScichartSurface( s );
        }            
    }

    private void OnInitialized( object o, EventArgs e )
    {
        SetupScichartSurface( ( SciChartSurface ) o );
    }

    private void ResetIndicator( IChartIndicatorElement ui, IIndicator i )
    {
        ( ( IChartComponent ) ui ).ResetUI();
        InternalUnReSubscribeEvents( ( IChartElement ) ui, true );
    }

    event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
    {
        add => PropertyChangedEvent += value;
        remove => PropertyChangedEvent -= value;
    }

    void INotifyPropertyChangedEx.NotifyPropertyChanged( string propertyName )
    {
        PropertyChangedEventHandler handler = PropertyChangedEvent;
        if ( handler == null )
            return;
        DelegateHelper.Invoke( handler, this, propertyName );
    }


    /// <summary>
    /// Get a security from the chart area. If there are multiple candle elements, user will be asked to select one.
    /// </summary>
    /// <param name="area"></param>
    /// <returns></returns>
    private Security? GetSelectedSecurity( ChartArea area )
    {
        var candles   = area.Elements.OfType<IChartCandleElement>().ToArray();
        var candle1st = TemplateTypeHelper.GetFirstElement<IChartCandleElement>(candles);

        if ( candles.Length > 1 )
        {
            var wnd = new ChartCandleElementPicker()
            {
                Elements = candles,
                SelectedElement = candle1st
            };

            if ( !wnd.ShowModal( this ) )
                return null;

            candle1st = wnd.SelectedElement;
        }

        Security? selectedSecurity;

        if ( candle1st != null )
        {
            var subscription = TryGetSubscription(candle1st);
            selectedSecurity = subscription != null ? subscription.TryGetSecurity() : null;
        }
        else
            selectedSecurity = null;


        if ( selectedSecurity == null )
        {
            SecurityPickerWindow wnd = new SecurityPickerWindow()
            {
                SelectionMode = MultiSelectMode.Row
            };

            if ( SecurityProvider != null )
                wnd.SecurityProvider = SecurityProvider;

            if ( !wnd.ShowModal( this ) )
                return null;

            selectedSecurity = wnd.SelectedSecurity;
        }

        return selectedSecurity;
    }

    private void OnUngroupEvent( ChartArea area )
    {
        if ( area == null )
            return;

        area.GroupId = !StringHelper.IsEmpty( area.GroupId ) ? string.Empty : Guid.NewGuid().ToString();
        
        UngroupEvent?.Invoke( ( IChartArea ) area );
    }

    private void OnAreaAdding()
    {        
        ChartArea area = new ChartArea()
        {
            Title = $"{LocalizedStrings.Panel} {( _iChartAreaList.Count + 1 ).ToString()}"
        };
        
        CollectionHelper.ForEach( area.XAxises, p => p.TimeZone = GetTimeZoneInfo() );
        
        AddArea(  area );
    }

    private void OnAddCandles( ChartArea area )
    {
        if ( _subscription == null )
        {
            _subscription = new Subscription( ( ISubscriptionMessage ) DefaultCandlesSettings, ( SecurityMessage ) null );
        }
            
        CandleSettingsWindow wnd = new CandleSettingsWindow()
        {
            Subscription = _subscription.Clone()
        };

        if ( SecurityProvider != null )
            wnd.SecurityProvider = SecurityProvider;

        if ( !wnd.ShowModal( this ) )
            return;
        
        _subscription = wnd.Subscription;

        ChartCandleElement element = new ChartCandleElement()
        {
            PriceStep = ( (SecurityMessage)_subscription?.MarketData )?.PriceStep,
            DrawStyle = CandleDrawStyles
        };
        AddElement( ( IChartArea ) area, ( IChartCandleElement ) element, _subscription );
    }

    private void OnAddIndicator( ChartArea area )
    {
        IndicatorPickerWindow pickerWnd = new IndicatorPickerWindow()
        {
            AutoSelectCandles = true,
            IndicatorTypes    = IndicatorTypes
        };

        if ( !pickerWnd.ShowModal( this ) )
            return;

        IChartCandleElement[] candles = IChartExtensions.GetElements<IChartCandleElement>((IChart) this).ToArray<IChartCandleElement>();
        IChartCandleElement candle1st = area.Elements.OfType<IChartCandleElement>().Concat<IChartCandleElement>((IEnumerable<IChartCandleElement>)candles).FirstOrDefault<IChartCandleElement>();
        
        if ( candle1st == null )
        {
            new MessageBoxBuilder().Error().Text(LocalizedStrings.NoData2).Owner((DependencyObject)this).Show();
        }
        else
        {
            if ( !pickerWnd.AutoSelectCandles )
            {
                ChartCandleElementPicker candleWnd = new ChartCandleElementPicker()
                {
                    Elements        = candles,
                    SelectedElement = candle1st
                };

                if ( !candleWnd.ShowModal( ( DependencyObject ) this ) )
                    return;

                candle1st = candleWnd.SelectedElement;
            }

            ChartIndicatorElement indicatorUI = new ChartIndicatorElement()
            {
                IndicatorPainter = pickerWnd.SelectedIndicatorType.CreatePainter(),
                AutoAssignYAxis = true
            };
            AddElement( ( IChartArea ) area, ( IChartIndicatorElement ) indicatorUI, TryGetSubscription( ( IChartElement ) candle1st ), pickerWnd.Indicator );
        }
    }

    private void OnAddOrders( ChartArea _param1 )
    {
        Security security = GetSelectedSecurity(_param1);
        if ( security == null )
            return;

        //AddElement((IChartArea)_param1, (IChartOrderElement)new ChartOrderElement(), new Subscription(DataType.Transactions, security));
    }

    private void OnAddTrades( ChartArea _param1 )
    {
        Security security = GetSelectedSecurity(_param1);
        if ( security == null )
            return;

        //AddElement((IChartArea)_param1, (IChartTradeElement)new ChartTradeElement(), new Subscription(DataType.Transactions, security));
    }

    
    private void InvokeUnscribeElementNoChecking( IChartElement _param1 )
    {        
        UnSubscribeElement?.Invoke( _param1 );
    }

    private void InvokeSubscriptionEvent( IChartElement _param1 )
    {
        RaiseChartElementSubscribedEvent( _param1, TryGetSubscription( _param1 ) );
    }    
}



//using DevExpress.Xpf.Grid;
//using Ecng.Collections;
//using Ecng.Common;
//using Ecng.ComponentModel;
//using Ecng.Serialization;
//using Ecng.Xaml;
//using SciChart.Charting.Common;
//using SciChart.Charting.Visuals;
//using MoreLinq;
//using StockSharp.Algo;
//using StockSharp.Algo.Candles;
//using StockSharp.Algo.Indicators;
//using StockSharp.BusinessEntities;
//using StockSharp.Localization;
//using System;
//using System.Collections.Generic; 
//using fx.Collections;
//using System.ComponentModel;
//using System.Linq;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Markup;

//using StockSharp.Xaml.Charting.Definitions;
//using StockSharp.Xaml;
//using DevExpress.Mvvm.Native;
//using StockSharp.Charting;

//#pragma warning disable CA1416

//namespace StockSharp.Xaml.Charting
//{
//    public partial class Chart : UserControl, INotifyPropertyChanged, IComponentConnector, IPersistable, INotifyPropertyChangedEx, IChartEx, IThemeableChart
//    {
//        private readonly SynchronizedDictionary<ChartIndicatorElement, IIndicator> _indicators   = new SynchronizedDictionary<ChartIndicatorElement, IIndicator>( );
//        private readonly SynchronizedDictionary<IChartElement, object>   _uiDatasource = new SynchronizedDictionary<IChartElement, object>( );
//        private readonly CachedSynchronizedList<IChartElement>           _uiList       = new CachedSynchronizedList<IChartElement>( );

//        private static int staticChartCount;

//        private readonly int                _instanceCount = ++staticChartCount;       
//        private bool                        _isAutoScroll = true;
//        private bool                        _crossHairAxisLabels = true;
//        private bool                        _crossHair = true;
//        private ChartAxisType               _xAxisType = ChartAxisType.CategoryDateTime;
//        private TimeSpan                    _autoRangeInterval = TimeSpan.FromMilliseconds( 200.0 );
//        private TimeZoneInfo                _timeZoneInfo = TimeZoneInfo.Local;

//        private CandleSeries                _candleSeries;
//        private readonly ChartViewModel     _chartViewModel;
//        private readonly ChartAreasList     _chartAreas;
//        private ChartAnnotationTypes        _annotationTypes;
//        private bool                        _crossHairTooltip;
//        private bool                        _orderCreationMode;
//        private bool                        _isAutoRange;
//        private bool                        _autoRangeByAnnotations;
//        private bool                        _showPerfStats;
//        private ISecurityProvider           _securityProvider;
//        private bool                        _disableIndicatorReset;
//        private PropertyChangedEventHandler _propertyChangedEventHandler;

//        static Chart( )
//        {
//            LicenseManager.CreateInstance( );
//        }

//        public Chart( )
//        {
//            InitializeComponent( );

//            _chartAreas          = new ChartAreasList( this );
//            DataContext          = _chartViewModel = new ChartViewModel( );

//            ChartAreas.Added    += OnNewAreaAddedToChartArea;
//            ChartAreas.Removed  += OnAreaRemovedFromChartArea;
//            ChartAreas.Clearing += OnClearChartArea;
//            AreaAdded           += OnAreaAdded;
//            AddCandles          += OnAddCandlesArea;            
//            AddIndicator        += OnAddIndicatorArea;
//            AddOrders           += OnAddOrdersArea;
//            AddTrades           += OnAddTradesArea;
//            RemoveElement       += OnRemoveElement;

//            CodingAddCandles    += OnCodingAddCandles;
//        }

//        #region Tony
//        /* -------------------------------------------------------------------------------------------------------------------------------------------
//         *  When we clicked on the Add Candle Command from the context menu, the following function get executed.
//         *  
//         *  Step A ----------> 1
//         * 
//         * ------------------------------------------------------------------------------------------------------------------------------------------- 
//         */
//        private void OnAddCandlesArea( ChartArea candleArea )
//        {
//            ChartCandleElement candleUI = new ChartCandleElement( );

//            if ( _candleSeries == null )
//            {
//                var candleSeries = new CandleSeries( );
//                candleSeries.CandleType = ( typeof( TimeFrameCandle ) );
//                candleSeries.Arg = TimeSpan.FromMinutes( 5.0 );
//                _candleSeries = candleSeries;
//            }

//            CandleSettingsWindow wnd = new CandleSettingsWindow( )
//            {
//                Series = _candleSeries
//            };

//            if ( SecurityProvider != null )
//            {
//                wnd.SecurityProvider = SecurityProvider;
//            }

//            if ( !wnd.ShowModal( this ) )
//            {
//                return;
//            }

//            CandleSeries userSelectedSeries = wnd.Series;
//            _candleSeries = userSelectedSeries.Clone( );
//            _candleSeries.Security = null;

//            AddElement( candleArea, candleUI, userSelectedSeries );
//            userSelectedSeries.PropertyChanged += new PropertyChangedEventHandler( OnCandleSeriesPropertyChanged );
//        }

//        private void OnCodingAddCandles( object sender, AddCandlesEventArgs e )
//        {
//            ChartCandleElement candleUI = null;
//            if ( e.UseFifo )
//            {
//                candleUI = new ChartCandleElement() { FifoCapacity = e.FifoCapcity };
//            }
//            else
//            {
//                candleUI = new ChartCandleElement();
//            }


//            _candleSeries          = e.CandleSerie.Clone( );
//            _candleSeries.Security = null;

//            AddElement( e.ChartArea, candleUI, e.CandleSerie );
//            e.CandleSerie.PropertyChanged += new PropertyChangedEventHandler( OnCandleSeriesPropertyChanged );
//        }

//        #endregion


//        private bool Areas_Clearing( )
//        {
//            throw new NotImplementedException( );
//        }

//        internal int InstanceCount( )
//        {
//            return _instanceCount;
//        }

//        public ChartViewModel ViewModel
//        {
//            get
//            {
//                return _chartViewModel;
//            }
//        }

//        public INotifyList<ChartArea> ChartAreas
//        {
//            get
//            {
//                return _chartAreas;
//            }
//        }

//        public bool IsAutoScroll
//        {
//            get
//            {
//                return _isAutoScroll;
//            }
//            set
//            {
//                _isAutoScroll = value;
//                Notify( nameof( IsAutoScroll ) );
//            }
//        }

//        public bool IsAutoRange
//        {
//            get
//            {
//                return _isAutoRange;
//            }

//            set
//            {
//                _isAutoRange = value;

//                foreach ( ChartArea area in ChartAreas )
//                {
//                    Ecng.Collections.CollectionHelper.ForEach( area.XAxises, ax => ax.AutoRange = value  );

//                }

//                Notify( nameof( IsAutoRange ) );
//            }
//        }

//        public ChartAxisType XAxisType
//        {
//            get
//            {
//                return _xAxisType;
//            }
//            set
//            {
//                if ( _xAxisType == value )
//                {
//                    return;
//                }

//                if ( ChartAreas.SelectMany( a => a.Elements ).Any( ) )
//                {
//                    throw new InvalidOperationException( LocalizedStrings.AxisTypeCantBeSet );
//                }

//                _xAxisType = value;

//                foreach ( ChartArea chartArea in ChartAreas )
//                {
//                    chartArea.XAxisType = _xAxisType;
//                }
//            }
//        }

//        public TimeSpan AutoRangeInterval
//        {
//            get
//            {
//                return _autoRangeInterval;
//            }
//            set
//            {
//                if ( value <= TimeSpan.Zero )
//                {
//                    throw new ArgumentOutOfRangeException( nameof( AutoRangeInterval ) );
//                }

//                _autoRangeInterval = value;
//                Notify( nameof( AutoRangeInterval ) );
//            }
//        }

//        public ISecurityProvider SecurityProvider
//        {
//            get
//            {
//                return _securityProvider;
//            }
//            set
//            {
//                _securityProvider = value;
//            }
//        }

//        public bool DisableIndicatorReset
//        {
//            get
//            {
//                return _disableIndicatorReset;
//            }
//            set
//            {
//                _disableIndicatorReset = value;
//            }
//        }

//        public void AddArea( ChartArea area )
//        {
//            GuiAsync( ( ) => _chartAreas.Add( area ) );
//        }

//        public void RemoveArea( ChartArea area )
//        {
//            GuiAsync( ( ) => _chartAreas.Remove( area ) );
//        }

//        public void ClearAreas( )
//        {
//            GuiAsync( ( ) => _chartAreas.Clear( ) );
//        }

//        public IEnumerable<IChartElement> Elements
//        {
//            get
//            {
//                return _uiList.Cache;
//            }
//        }

//        public void AddElement( ChartArea area, IChartElement element )
//        {
//            if ( area == null )
//            {
//                throw new ArgumentNullException( nameof( area ) );
//            }

//            if ( element == null )
//            {
//                throw new ArgumentNullException( nameof( element ) );
//            }

//            GuiAsync( ( ) => area.Elements.Add( element ) );
//        }

//        public void AddElement( ChartArea area, ChartCandleElement element, CandleSeries candleSeries )
//        {
//            if ( area == null )
//            {
//                throw new ArgumentNullException( nameof( area ) );
//            }

//            if ( element == null )
//            {
//                throw new ArgumentNullException( nameof( element ) );
//            }

//            if ( candleSeries == null )
//            {
//                throw new ArgumentNullException( nameof( candleSeries ) );
//            }

//            _uiDatasource.Add( element, candleSeries );

//            if ( element.Title.IsEmpty( ) )
//            {
//                element.Title = candleSeries.Title( );
//            }

//            AddElement( area, element );
//        }

//        public void AddElement( ChartArea area, ChartIndicatorElement indicatorUI, CandleSeries candleSeries, IIndicator indicator )
//        {
//            if ( area == null )
//            {
//                throw new ArgumentNullException( nameof( area ) );
//            }

//            if ( indicatorUI == null )
//            {
//                throw new ArgumentNullException( nameof( indicatorUI ) );
//            }

//            if ( candleSeries == null )
//            {
//                throw new ArgumentNullException( nameof( candleSeries ) );
//            }

//            if ( indicator == null )
//            {
//                throw new ArgumentNullException( nameof( indicator ) );
//            }

//            _uiDatasource.Add( indicatorUI, candleSeries );

//            _indicators.Add( indicatorUI, indicator );

//            if ( !DisableIndicatorReset )
//            {
//                indicator.Reseted += ( ) => OnIndicatorReset( indicatorUI, indicator );
//            }

//            if ( StringHelper.IsEmpty( indicatorUI.FullTitle ) )
//            {
//                indicatorUI.FullTitle = indicator.ToString( );
//            }

//            indicatorUI.CreateIndicatorPainter( IndicatorTypes, indicator );
//            AddElement( area, indicatorUI );
//        }

//        public void AddElement( ChartArea area, OrdersUI element, Security security )
//        {
//            if ( area == null )
//            {
//                throw new ArgumentNullException( nameof( area ) );
//            }

//            if ( element == null )
//            {
//                throw new ArgumentNullException( nameof( element ) );
//            }

//            if ( security == null )
//            {
//                throw new ArgumentNullException( nameof( security ) );
//            }

//            _uiDatasource.Add( element, security );

//            if ( element.FullTitle.IsEmpty( ) )
//            {
//                element.FullTitle = "{0} ({1})".Put( security.Code, element.GetType( ).GetDisplayName( null ).ToLower( ) );
//            }

//            AddElement( area, element );
//        }

//        public void AddElement( ChartArea area, TradesUI element, Security security )
//        {
//            if ( area == null )
//            {
//                throw new ArgumentNullException( nameof( area ) );
//            }

//            if ( element == null )
//            {
//                throw new ArgumentNullException( nameof( element ) );
//            }

//            if ( security == null )
//            {
//                throw new ArgumentNullException( nameof( security ) );
//            }

//            _uiDatasource.Add( element, security );

//            if ( element.FullTitle.IsEmpty( ) )
//            {
//                element.FullTitle = "{0} ({1})".Put( security.Code, element.GetType( ).GetDisplayName( null ).ToLower( ) );
//            }

//            AddElement( area, element );
//        }

//        void IChartEx.RemoveElement( ChartArea area, IChartElement element )
//        {
//            if ( area == null )
//            {
//                throw new ArgumentNullException( nameof( area ) );
//            }

//            if ( element == null )
//            {
//                throw new ArgumentNullException( nameof( element ) );
//            }

//            ChartIndicatorElement indicator = element as ChartIndicatorElement;
//            if ( indicator != null )
//            {
//                _indicators.Remove( indicator );
//            }

//            GuiAsync( ( ) => area.Elements.Remove( element ) );

//            _uiDatasource.Remove( element );
//        }

//        public IIndicator GetIndicator( ChartIndicatorElement element )
//        {
//            return Ecng.Collections.CollectionHelper.TryGetValue( _indicators, element );

//        }

//        public object GetSource( IChartElement element )
//        {
//            return Ecng.Collections.CollectionHelper.TryGetValue( _uiDatasource, element );

//        }

//        private T GetSeries<T>( IChartElement element ) where T : class
//        {
//            return ( T )GetSource( element );
//        }

//        public void SetSource( IChartElement element, object source )
//        {
//            _uiDatasource[ element ] = source;
//        }

//        public bool AutoRangeByAnnotations
//        {
//            get
//            {
//                return _autoRangeByAnnotations;
//            }
//            set
//            {
//                _autoRangeByAnnotations = value;
//                Notify( nameof( AutoRangeByAnnotations ) );
//            }
//        }

//        public int MinimumRange
//        {
//            get
//            {
//                return ViewModel.MinimumRange;
//            }
//            set
//            {
//                ViewModel.MinimumRange = value;
//            }
//        }

//        public string ChartTheme
//        {
//            get
//            {
//                return ViewModel.SelectedTheme;
//            }
//            set
//            {
//                ViewModel.SelectedTheme = value;
//            }
//        }

//        public bool ShowLegend
//        {
//            get
//            {
//                return ViewModel.ShowLegend;
//            }
//            set
//            {
//                ViewModel.ShowLegend = value;
//            }
//        }

//        public bool ShowOverview
//        {
//            get
//            {
//                return ViewModel.ShowOverview;
//            }
//            set
//            {
//                ViewModel.ShowOverview = value;
//            }
//        }

//        public bool ShowPerfStats
//        {
//            get
//            {
//                return _showPerfStats;
//            }
//            set
//            {
//                _showPerfStats = value;
//                Notify( nameof( ShowPerfStats ) );
//            }
//        }

//        public bool IsInteracted
//        {
//            get
//            {
//                return ViewModel.IsInteracted;
//            }
//            set
//            {
//                ViewModel.IsInteracted = value;
//            }
//        }

//        public bool CrossHair
//        {
//            get
//            {
//                return _crossHair;
//            }
//            set
//            {
//                _crossHair = value;
//                Notify( nameof( CrossHair ) );
//            }
//        }

//        public bool CrossHairTooltip
//        {
//            get
//            {
//                return _crossHairTooltip;
//            }
//            set
//            {
//                _crossHairTooltip = value;
//                Notify( nameof( CrossHairTooltip ) );
//            }
//        }

//        public bool CrossHairAxisLabels
//        {
//            get
//            {
//                return _crossHairAxisLabels;
//            }
//            set
//            {
//                _crossHairAxisLabels = value;
//                Notify( nameof( CrossHairAxisLabels ) );
//            }
//        }

//        public ChartAnnotationTypes AnnotationType
//        {
//            get
//            {
//                return _annotationTypes;
//            }
//            set
//            {
//                _annotationTypes = value;
//                Notify( nameof( AnnotationType ) );
//            }
//        }

//        public bool OrderCreationMode
//        {
//            get
//            {
//                return _orderCreationMode;
//            }
//            set
//            {
//                _orderCreationMode = value;
//                Notify( nameof( OrderCreationMode ) );
//            }
//        }

//        public TimeZoneInfo TimeZone
//        {
//            get
//            {
//                return _timeZoneInfo;
//            }
//            set
//            {
//                TimeZoneInfo timeZoneInfo = value;
//                if ( timeZoneInfo == null )
//                {
//                    throw new ArgumentNullException( nameof( value ) );
//                }

//                _timeZoneInfo = timeZoneInfo;
//                Notify( nameof( TimeZone ) );
//            }
//        }

//        public IList<IndicatorType> IndicatorTypes
//        {
//            get
//            {
//                return ViewModel.IndicatorTypes;
//            }
//        }

//        public event Action AreaAdded
//        {
//            add
//            {
//                throw new NotImplementedException();
//                //ViewModel.AreaAddedEvent += value;
//            }
//            remove
//            {
//                throw new NotImplementedException();
//                //ViewModel.AreaAddedEvent -= value;
//            }
//        }

//        public event Action<ChartArea> AddCandles
//        {
//            add
//            {
//                ViewModel.AddCandlesEvent += value;
//            }
//            remove
//            {
//                ViewModel.AddCandlesEvent -= value;
//            }
//        }

//        public event EventHandler<AddCandlesEventArgs> CodingAddCandles
//        {
//            add
//            {
//                ViewModel.CodingAddCandlesEvent += value;
//            }
//            remove
//            {
//                ViewModel.CodingAddCandlesEvent -= value;
//            }
//        }

//        public event Action<ChartArea> AddIndicator
//        {
//            add
//            {
//                ViewModel.AddIndicatorEvent += value;
//            }
//            remove
//            {
//                ViewModel.AddIndicatorEvent -= value;
//            }
//        }

//        public event Action<ChartArea> AddOrders
//        {
//            add
//            {
//                ViewModel.AddOrdersEvent += value;
//            }
//            remove
//            {
//                ViewModel.AddOrdersEvent -= value;
//            }
//        }

//        public event Action<ChartArea> AddTrades
//        {
//            add
//            {
//                ViewModel.AddTradesEvent += value;
//            }
//            remove
//            {
//                ViewModel.AddTradesEvent -= value;
//            }
//        }

//        public event Action<IChartElement> RemoveElement
//        {
//            add
//            {
//                ViewModel.RemoveElementEvent += value;
//            }
//            remove
//            {
//                ViewModel.RemoveElementEvent -= value;
//            }
//        }

//        public event Action<ChartArea, Order> CreateOrder;

//        public event Action<Order, Decimal> MoveOrder;

//        public event Action<Order> CancelOrder;

//        public event Action<ChartAnnotation> AnnotationCreated;

//        public event Action<ChartAnnotation, ChartDrawData.AnnotationData> AnnotationModified;

//        public event Action<ChartAnnotation> AnnotationDeleted;

//        public event Action<ChartAnnotation, ChartDrawData.AnnotationData> AnnotationSelected;

//        public event Action<ChartCandleElement, CandleSeries> SubscribeCandleElement;

//        public event Action<ChartIndicatorElement, CandleSeries, IIndicator> SubscribeIndicatorElement;

//        public event Action<OrdersUI, Security> SubscribeOrderElement;

//        public event Action<TradesUI, Security> SubscribeTradeElement;

//        public event Action<IChartElement> UnSubscribeElement;

//        public void Reset( IEnumerable<IChartElement> elements )
//        {
//            _chartAreas.ResetChartAreas( elements.ToArray( ) );
//        }

//        public void Draw( ChartDrawData data )
//        {
//            /* -------------------------------------------------------------------------------------------------------------------------------------------
//            * 
//            *  Step 7----------> 8 The Single Candle is passed to DrawChartAreas
//            *                           
//            * ------------------------------------------------------------------------------------------------------------------------------------------- 
//            */
//            _chartAreas.DrawChartAreas( data );
//        }

//        internal void InvokeCreateOrderEvent( ChartArea area, Order order )
//        {
//            CreateOrder?.Invoke( area, order );
//        }

//        internal void InvokeMoveOrderEvent( Order order, Decimal valueOne )
//        {
//            MoveOrder?.Invoke( order, valueOne );
//        }

//        internal void InvokeCancelOrderEvent( Order order )
//        {
//            CancelOrder?.Invoke( order );
//        }



//        public TimeZoneInfo GetTimeZone( )
//        {
//            return ChartAreas.Select( a => a.XAxises.FirstOrDefault( i => i.TimeZone != null ) ).LastOrDefault( ax => ax != null )?.TimeZone;
//        }

//        public void Load( SettingsStorage storage )
//        {
//            IsAutoScroll           = storage.GetValue( "IsAutoScroll", IsAutoScroll );
//            IsAutoRange            = storage.GetValue( "IsAutoRange", IsAutoRange );
//            XAxisType              = storage.GetValue( "XAxisType", ChartAxisType.CategoryDateTime );
//            AutoRangeByAnnotations = storage.GetValue( "AutoRangeByAnnotations", AutoRangeByAnnotations );
//            ShowOverview           = storage.GetValue( "ShowOverview", ShowOverview );
//            ShowLegend             = storage.GetValue( "ShowLegend", ShowLegend );
//            CrossHair              = storage.GetValue( "CrossHair", CrossHair );
//            CrossHairTooltip       = storage.GetValue( "CrossHairTooltip", CrossHairTooltip );
//            CrossHairAxisLabels    = storage.GetValue( "CrossHairAxisLabels", CrossHairAxisLabels );
//            OrderCreationMode      = storage.GetValue( "OrderCreationMode", OrderCreationMode );
//            TimeZone               = MayBe.With(storage.GetValue<string>("TimeZone", null), new Func<string, TimeZoneInfo>( TimeZoneInfo.FindSystemTimeZoneById ) ) ?? TimeZoneInfo.Local;

//            if ( !IsInteracted )
//            {
//                return;
//            }

//            /* -------------------------------------------------------------------------------------------------------------------------------------------
//             * 
//             *  Step 7A----------> 0 All the CandleSeries and IndicatorSeries are loaded from SettingStorage
//             * 
//             * ------------------------------------------------------------------------------------------------------------------------------------------- 
//             */
//            var candleSeries       = GetCandleSeries( storage.GetValue<SettingsStorage>( "Sources", null ) );
//            var indicatorSeries    = GetIndicators( storage.GetValue<SettingsStorage>( "Indicators", null ) );
//            _chartAreas.GetAreas( storage.GetValue<SettingsStorage>( "Areas", null ), candleSeries, indicatorSeries );
//        }

//        public void Save( SettingsStorage storage )
//        {
//            storage.SetValue( "IsAutoScroll"          , IsAutoScroll );
//            storage.SetValue( "IsAutoRange"           , IsAutoRange );
//            storage.SetValue( "XAxisType"             , XAxisType );
//            storage.SetValue( "AutoRangeByAnnotations", AutoRangeByAnnotations );
//            storage.SetValue( "ShowOverview"          , ShowOverview );
//            storage.SetValue( "ShowLegend"            , ShowLegend );
//            storage.SetValue( "CrossHair"             , CrossHair );
//            storage.SetValue( "CrossHairTooltip"      , CrossHairTooltip );
//            storage.SetValue( "CrossHairAxisLabels"   , CrossHairAxisLabels );
//            storage.SetValue( "OrderCreationMode"     , OrderCreationMode );
//            storage.SetValue( "TimeZone"              , TimeZone?.Id );

//            if ( !IsInteracted )
//            {
//                return;
//            }

//            storage.SetValue( "Sources"               , SaveCandleSeries( ) );
//            storage.SetValue( "Indicators"            , SaveIndicators( ) );
//            storage.SetValue( "Areas"                 , _chartAreas.Save( ) );
//        }

//        public void ReSubscribeElements( )
//        {
//            if ( !IsInteracted )
//            {
//                return;
//            }

//            foreach ( IChartElement chartElement in Elements )
//            {
//                RemoveAndRaiseUnsubscribeElementEvent( chartElement );
//                AddElement( chartElement );
//            }
//        }

//        private void OnCandleSeriesPropertyChanged( object sender, PropertyChangedEventArgs e )
//        {
//            if ( !IsInteracted )
//            {
//                return;
//            }

//            var candleSeries = ( CandleSeries )sender;

//            foreach ( IChartElement element in Elements.Where( x => GetSource( x ) == candleSeries ).ToArray( ) )
//            {
//                var ChartCandleElement = element as ChartCandleElement;

//                if ( ChartCandleElement != null )
//                {
//                    ChartCandleElement.Title = candleSeries.Title( );
//                }

//                ResetElement( element, true );
//            }
//        }

//        private SettingsStorage SaveCandleSeries( )
//        {
//            SettingsStorage storage = new SettingsStorage( );
//            PooledDictionary<CandleSeries, Guid> dictionary = new PooledDictionary<CandleSeries, Guid>( );

//            foreach ( var pair in _uiDatasource.SyncGet( d => d.ToArray( ) ) )
//            {
//                string elementGuid = pair.Key.Id.ToString( );

//                if ( pair.Value is CandleSeries key )
//                {
//                    Guid guid = dictionary.SafeAdd( key, ( s => Guid.NewGuid( ) ) );
//                    storage.SetValue( elementGuid, guid );
//                }
//                else
//                {
//                    storage.SetValue( elementGuid, ( ( Security )pair.Value ).Id );
//                }
//            }

//            if ( dictionary.Count > 0 )
//            {
//                SettingsStorage candleSeriesStorage = new SettingsStorage( );

//                foreach ( KeyValuePair<CandleSeries, Guid> candleSeries in dictionary )
//                {
//                    // This setting is the reverse of the above setting, it saves the CandleSeries map to Guid 
//                    candleSeriesStorage.Add( candleSeries.Value.ToString( ), candleSeries.Key.Save( ) );
//                }
//                storage.SetValue( "CandleSeries", candleSeriesStorage );
//            }
//            return storage;
//        }

//        private IDictionary<Guid, object> GetCandleSeries( SettingsStorage settings )
//        {
//            PooledDictionary<Guid, object> output = new PooledDictionary<Guid, object>( );
//            if ( settings == null )
//            {
//                return output;
//            }

//            var candleSeriesMap = new PooledDictionary<Guid, CandleSeries>( );
//            var candleStorage = settings.GetValue<SettingsStorage>( "CandleSeries", null );

//            if ( candleStorage != null )
//            {
//                foreach ( KeyValuePair<string, object> keyValuePair in candleStorage )
//                {
//                    Guid key = keyValuePair.Key.To<Guid>( );
//                    CandleSeries candleSeries = ( ( SettingsStorage )keyValuePair.Value ).Load<CandleSeries>( );
//                    candleSeries.PropertyChanged += new PropertyChangedEventHandler( OnCandleSeriesPropertyChanged );
//                    candleSeriesMap.Add( key, candleSeries );
//                }
//            }

//            foreach ( KeyValuePair<string, object> keyValuePair in settings )
//            {
//                if ( !( keyValuePair.Key == "CandleSeries" ) )
//                {
//                    object obj;
//                    if ( keyValuePair.Value is SettingsStorage storage )
//                    {
//                        CandleSeries candleSeries = storage.Load<CandleSeries>( );
//                        candleSeries.PropertyChanged += new PropertyChangedEventHandler( OnCandleSeriesPropertyChanged );
//                        obj = candleSeries;
//                    }
//                    else
//                    {
//                        obj = !( keyValuePair.Value is Guid index )
//                            ? ( SecurityProvider ?? ServicesRegistry.SecurityProvider ).LookupById( ( string )keyValuePair.Value )
//                            : ( object )candleSeriesMap[ index ];
//                    }

//                    output.Add( keyValuePair.Key.To<Guid>( ), obj );
//                }
//            }
//            return output;
//        }

//        private SettingsStorage SaveIndicators( )
//        {
//            SettingsStorage settingsStorage = new SettingsStorage( );

//            foreach ( var pair in _indicators.SyncGet( d => d.ToArray( ) ) )
//            {
//                settingsStorage.SetValue( pair.Key.Id.ToString( ), pair.Value.SaveEntire( false ) );
//            }

//            return settingsStorage;
//        }

//        private static IDictionary<Guid, IIndicator> GetIndicators( SettingsStorage settings )
//        {
//            PooledDictionary<Guid, IIndicator> dictionary = new PooledDictionary<Guid, IIndicator>( );
//            if ( settings == null )
//            {
//                return dictionary;
//            }

//            foreach ( KeyValuePair<string, object> keyValuePair in settings )
//            {
//                dictionary.Add( keyValuePair.Key.To<Guid>( ), ( ( SettingsStorage )keyValuePair.Value ).LoadEntire<IIndicator>( ) );
//            }

//            return dictionary;
//        }

//        private void AddElement( IChartElement element )
//        {
//            if ( GetSource( element ) == null )
//            {
//                return;
//            }

//            _uiList.Add( element );
//            RaiseChartElementSubscribedEvent( element );
//        }

//        private void RaiseChartElementSubscribedEvent( IChartElement chartElement )
//        {
//            switch ( chartElement )
//            {
//                case ChartCandleElement ChartCandleElement:
//                {
//                    /* -------------------------------------------------------------------------------------------------------------------------------------------
//                    *  When we clicked on the Add Candle Command from the context menu, the following function get executed.
//                    *  
//                    *  Step A ----------> 4 After Candle get added to the UI PooledList, we raise Chart Element Subscribed Event.
//                    * 
//                    * ------------------------------------------------------------------------------------------------------------------------------------------- 
//                    */
//                    SubscribeCandleElement?.Invoke( ChartCandleElement, GetSeries<CandleSeries>( ChartCandleElement ) );
//                }                
//                break;

//                case ChartIndicatorElement element:
//                {
//                    SubscribeIndicatorElement?.Invoke( element, GetSeries<CandleSeries>( element ), GetIndicator( element ) );
//                }                
//                break;

//                case OrdersUI chartOrderElement:
//                {
//                    SubscribeOrderElement?.Invoke( chartOrderElement, GetSeries<Security>( chartOrderElement ) );
//                }                
//                break;

//                case TradesUI chartTradeElement:
//                {
//                    SubscribeTradeElement?.Invoke( chartTradeElement, GetSeries<Security>( chartTradeElement ) );
//                }                
//                break;
//            }
//        }

//        private void RemoveAndRaiseUnsubscribeElementEvent( IChartElement element )
//        {
//            if ( GetSource( element ) == null )
//            {
//                return;
//            }

//            _uiList.Remove( element );

//            UnSubscribeElement?.Invoke( element );
//        }

//        private void OnNewAreaAddedToChartArea( ChartArea area )
//        {
//            area.Elements.Added   += new Action<IChartElement>( OnNewUIAddedToArea );
//            area.Elements.Removed += new Action<IChartElement>( OnUIRemovedFromArea );

//            throw new NotImplementedException();
//            //area.Chart = ( this );

//            //ViewModel.ScichartSurfaceViewModels.Add( ( ScichartSurfaceMVVM ) area.ViewModel );

//            //Ecng.Collections.CollectionHelper.ForEach( area.Elements, new Action<IChartElement>( OnNewUIAddedToArea ) );

//        }

//        private void OnAreaRemovedFromChartArea( ChartArea area )
//        {
//            area.Elements.Added   -= new Action<IChartElement>( OnNewUIAddedToArea );
//            area.Elements.Removed -= new Action<IChartElement>( OnUIRemovedFromArea );
//            //ViewModel.ScichartSurfaceViewModels.Remove( ( ScichartSurfaceMVVM ) area.ViewModel );

//            Ecng.Collections.CollectionHelper.ForEach( area.Elements, new Action<IChartElement>( OnUIRemovedFromArea ) );


//            area.Chart= null;
//            area.Dispose( );
//        }

//        private bool OnClearChartArea( )
//        {
//            foreach ( ChartArea area in ChartAreas )
//            {
//                OnAreaRemovedFromChartArea( area );
//            }

//            ViewModel.InitRangeDepProperty( );
//            return true;
//        }

//        /* -------------------------------------------------------------------------------------------------------------------------------------------
//        *  When we clicked on the Add Candle Command from the context menu, the following function get executed.
//        *  
//        *  Step A ----------> 3 Now that NotifyList OnAdded Event has been invoked.
//        * 
//        * ------------------------------------------------------------------------------------------------------------------------------------------- 
//        */
//        private void OnNewUIAddedToArea( IChartElement element )
//        {
//            if ( !IsInteracted )
//            {
//                return;
//            }

//            AddElement( element );
//        }

//        private void OnUIRemovedFromArea( IChartElement element )
//        {
//            ResetElement( element, false );
//        }

//        private void ResetElement( IChartElement element, bool needAddElement )
//        {
//            IChartElement[ ] elementArray;
//            if ( element is ChartCandleElement )
//            {
//                elementArray = Elements.Where( e => GetSource( e ) == GetSeries<CandleSeries>( element ) ).ToArray( );
//            }
//            else
//            {
//                elementArray = new IChartElement[ 1 ]
//                {
//                    element
//                };
//            }

//            if ( needAddElement )
//            {
//                if ( IsInteracted )
//                {
//                    Ecng.Collections.CollectionHelper.ForEach( elementArray, new Action<IChartElement>( RaiseUnsubscribeElementEvent ) );                    
//                }

//                Reset( elementArray );

//                if ( !IsInteracted )
//                {
//                    return;
//                }


//                Ecng.Collections.CollectionHelper.ForEach( elementArray, new Action<IChartElement>( RaiseChartElementSubscribedEvent ) );

//            }
//            else
//            {
//                if ( !IsInteracted )
//                {
//                    return;
//                }

//                Ecng.Collections.CollectionHelper.ForEach( elementArray, new Action<IChartElement>( RemoveAndRaiseUnsubscribeElementEvent ) );


//            }
//        }

//        internal static void SetupScichartSurface( SciChartSurface chartSurface )
//        {
//            chartSurface.DebugWhyDoesntSciChartRender = true;

//            if ( chartSurface.DataContext != null )
//            {
//                ( ( ScichartSurfaceMVVM )chartSurface.DataContext ).SetScichartSurface( chartSurface );
//            }
//            else
//            {
//                chartSurface.DataContextChanged += ( s, e ) =>
//                {
//                    ( ( ScichartSurfaceMVVM )chartSurface.DataContext ).SetScichartSurface( chartSurface );
//                };
//            }
//        }

//        private void OnInitialized( object sender, EventArgs e )
//        {
//            SetupScichartSurface( ( SciChartSurface )sender );
//        }

//        private void OnIndicatorReset( ChartIndicatorElement indicatorElement, IIndicator indicator )
//        {
//            indicatorElement.FullTitle = indicator.ToString( );
//            ResetElement( indicatorElement, true );
//        }


//        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
//        {
//            add
//            {
//                _propertyChangedEventHandler += value;
//            }
//            remove
//            {
//                _propertyChangedEventHandler -= value;
//            }
//        }


//        void INotifyPropertyChangedEx.NotifyPropertyChanged( string string_0 )
//        {
//            PropertyChangedEventHandler changedEventHandler0 = _propertyChangedEventHandler;
//            if ( changedEventHandler0 == null )
//            {
//                return;
//            }

//            changedEventHandler0.Invoke( this, string_0 );
//        }


//        private void OnAreaAdded( )
//        {
//            ChartArea area             = new ChartArea( ) { Title = LocalizedStrings.Panel + " " +   ( ChartAreas .Count + 1 ), XAxisType = XAxisType };
//            var viewModel              = new ScichartSurfaceMVVM( area );
//            area.ViewModel = viewModel;

//            var timeZoneInfo = GetTimeZone( );

//            foreach ( ChartAxis xAxis in area.XAxises )
//            {
//                xAxis.TimeZone = timeZoneInfo;
//            }

//            AddArea( area );
//        }


//        private void OnAddIndicatorArea( ChartArea area )
//        {
//            IndicatorPickerWindow indicatorPicker = new IndicatorPickerWindow( )
//            {
//                AutoSelectCandles = true,
//                IndicatorTypes = IndicatorTypes
//            };

//            if ( !indicatorPicker.ShowModal( this ) )
//            {
//                return;
//            }

//            ChartCandleElement[ ] array = Elements.OfType<ChartCandleElement>( ).ToArray( );
//            ChartCandleElement ChartCandleElement = area.Elements.OfType<ChartCandleElement>( ).Concat( array ).FirstOrDefault( );

//            if ( ChartCandleElement == null )
//            {
//                new MessageBoxBuilder( ).Error( ).Text( LocalizedStrings.CandleStick ).Owner( this ).Show( );
//            }
//            else
//            {
//                if ( !indicatorPicker.AutoSelectCandles )
//                {
//                    ChartCandleElementViewModelPicker wnd2 = new ChartCandleElementViewModelPicker( )
//                    {
//                        Elements = array,
//                        SelectedElement = ChartCandleElement
//                    };

//                    if ( !wnd2.ShowModal( this ) )
//                    {
//                        return;
//                    }

//                    ChartCandleElement = wnd2.SelectedElement;
//                }

//                /* ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
//                 * 
//                 * Tony Indicator Step 1: The above code will show the indicator Window and let us select the wanted indicator and the following will create the Indicator and the 
//                 *                  corresponding indicator painter.
//                 *                  
//                 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------                 
//                 */
//                var indicatorUI              = new ChartIndicatorElement( );

//                var indicatorPainter                  = indicatorPicker.SelectedIndicatorType.CreatePainter();                               

//                indicatorUI.IndicatorPainter =  indicatorPainter;

//                var tonyCandleSeries         = GetSeries< CandleSeries >( ChartCandleElement );


//                AddElement( area, indicatorUI, tonyCandleSeries, indicatorPicker.Indicator );
//            }
//        }

//        private void OnAddOrdersArea( ChartArea area )
//        {
//            SecurityPickerWindow securityPickerWindow = new SecurityPickerWindow( );
//            securityPickerWindow.SelectionMode = ( MultiSelectMode.Row );
//            SecurityPickerWindow wnd = securityPickerWindow;
//            if ( SecurityProvider != null )
//            {
//                wnd.SecurityProvider = ( SecurityProvider );
//            }

//            if ( !wnd.ShowModal( this ) )
//            {
//                return;
//            }

//            OrdersUI element = new OrdersUI( );
//            AddElement( area, element, wnd.SelectedSecurity );
//        }

//        private void OnAddTradesArea( ChartArea area )
//        {
//            SecurityPickerWindow securityPickerWindow = new SecurityPickerWindow( );
//            securityPickerWindow.SelectionMode = ( MultiSelectMode.Row );
//            SecurityPickerWindow wnd = securityPickerWindow;
//            if ( SecurityProvider != null )
//            {
//                wnd.SecurityProvider = ( SecurityProvider );
//            }

//            if ( !wnd.ShowModal( this ) )
//            {
//                return;
//            }

//            TradesUI element = new TradesUI( );
//            AddElement( area, element, wnd.SelectedSecurity );
//        }

//        private void OnRemoveElement( IChartElement element )
//        {
//            if ( element is ChartIndicatorElement indicator && indicator.ParentElement != null )
//            {
//                element = indicator.ParentElement;
//            }

//            ( ( IChartEx )this ).RemoveElement( (ChartArea) element.ChartArea, element );
//        }



//        private void RaiseUnsubscribeElementEvent( IChartElement element )
//        {
//            UnSubscribeElement?.Invoke( element );
//        }

//        void IChartEx.InvokeAnnotationCreatedEvent( ChartAnnotation annotation )
//        {
//            AnnotationCreated?.Invoke( annotation );
//        }

//        void IChartEx.InvokeAnnotationModifiedEvent( ChartAnnotation a, ChartDrawData.AnnotationData d )
//        {
//            AnnotationModified?.Invoke( a, d );
//        }

//        void IChartEx.InvokeAnnotationSelectedEvent( ChartAnnotation a, ChartDrawData.AnnotationData d )
//        {
//            AnnotationSelected?.Invoke( a, d );
//        }

//        void IChartEx.InvokeAnnotationDeletedEvent( ChartAnnotation a )
//        {
//            AnnotationDeleted?.Invoke( a );
//        }

//        private sealed class ChartAreasList : BaseList<ChartArea>, IPersistable
//        {
//            private readonly Chart _chart;

//            public ChartAreasList( Chart chart_1 )
//            {
//                Chart chart = chart_1;
//                if ( chart == null )
//                {
//                    throw new ArgumentNullException( "chart" );
//                }

//                _chart = chart;
//            }

//            protected override bool OnAdding( ChartArea newArea )
//            {
//                XamlHelper.EnsureUIThread( _chart );

//                if ( newArea.Chart!= null )
//                {
//                    //lock ( newArea.GetStackTrace( ).SyncRoot )
//                    //{
//                    //    ;
//                    //}

//                    throw new ArgumentException( "area" );
//                }
//                if ( newArea == null || Contains( newArea ) )
//                {
//                    throw new ArgumentException( "area2" );
//                }

//                if ( newArea.Elements.IsEmpty( ) )
//                {
//                    newArea.XAxisType = _chart.XAxisType;
//                }
//                else if ( newArea.XAxisType != _chart.XAxisType )
//                {
//                    throw new InvalidOperationException( LocalizedStrings.InvalidAxisType );
//                }

//                return base.OnAdding( newArea );
//            }

//            protected override void OnAdded( ChartArea area )
//            {
//                foreach ( ChartAxis xAxis in area.XAxises )
//                {
//                    xAxis.AutoRange = _chart.IsAutoRange;
//                }

//                area.PropertyChanged += new PropertyChangedEventHandler( ChartAreaHeightPropertyChanged );
//                base.OnAdded( area );
//            }

//            protected override void OnRemoved( ChartArea area )
//            {
//                XamlHelper.EnsureUIThread( _chart );
//                area.PropertyChanged -= new PropertyChangedEventHandler( ChartAreaHeightPropertyChanged );
//                base.OnRemoved( area );
//            }

//            private void ChartAreaHeightPropertyChanged( object sender, PropertyChangedEventArgs e )
//            {
//                ChartArea chartArea = ( ChartArea )sender;
//                if ( !( e.PropertyName == "Height" ) )
//                {
//                    return;
//                }

//                var vm = (ScichartSurfaceMVVM) chartArea.ViewModel;

//                vm.Height = chartArea.Height;
//            }

//            public void DrawChartAreas( ChartDrawData drawData )
//            {
//                if ( drawData == null )
//                {
//                    throw new ArgumentNullException( "data" );
//                }

//                /* -------------------------------------------------------------------------------------------------------------------------------------------
//                * 
//                *  Step 7----------> 9 All the Scichart Area will use the candle to draw
//                *                           
//                * ------------------------------------------------------------------------------------------------------------------------------------------- 
//                */
//                foreach ( ChartArea chartArea in this )
//                {
//                    var vm = (ScichartSurfaceMVVM) chartArea.ViewModel;
//                    vm.Draw( drawData );
//                }
//            }

//            public void ResetChartAreas( IChartElement[ ] element )
//            {
//                foreach ( ChartArea chartArea in this )
//                {
//                    var vm = (ScichartSurfaceMVVM) chartArea.ViewModel;
//                    vm.Reset( element );
//                }
//            }

//            /// <summary>
//            /// Tony: The following function will try to DeSerialize and reStore the ChartAreas with Candles and Indicators
//            /// </summary>
//            /// <param name="settings"></param>
//            /// <param name="candleSeries"></param>
//            /// <param name="indicatorSeries"></param>
//            public void GetAreas( SettingsStorage settings,
//                                  IDictionary<Guid, object> candleSeries,
//                                  IDictionary<Guid, IIndicator> indicatorSeries )
//            {
//                Clear( );

//                foreach ( SettingsStorage storage in settings.GetValue<IEnumerable<SettingsStorage>>( "Areas", null ) )
//                {
//                    ChartArea chartArea             = new ChartArea( );
//                    var viewModel                   = new ScichartSurfaceMVVM( chartArea );
//                    chartArea.ViewModel = viewModel;

//                    chartArea.Load( storage );

//                    foreach ( IChartElement element in chartArea.Elements )
//                    {
//                        object candle = Ecng.Collections.CollectionHelper.TryGetValue( candleSeries, element.Id );

//                        if ( candle != null )
//                        {
//                            _chart._uiDatasource.Add( element, candle );
//                        }

//                        if ( element is ChartIndicatorElement key )
//                        {

//                            IIndicator iindicator = Ecng.Collections.CollectionHelper.TryGetValue( indicatorSeries, key.Id );

//                            if ( candle != null )
//                            {
//                                _chart._indicators.Add( key, iindicator );
//                            }
//                        }
//                    }
//                    Add( chartArea );

//                    var vm = (ScichartSurfaceMVVM) chartArea.ViewModel;

//                    vm.Height = storage.GetValue( "Height", double.NaN );
//                }
//            }

//            void IPersistable.Load( SettingsStorage settings )
//            {
//                throw new NotSupportedException( );
//            }

//            public void Save( SettingsStorage settings )
//            {
//                settings.SetValue( "Areas", Select( a =>
//                {
//                    var s = a.Save( );

//                    var vm = (ScichartSurfaceMVVM) a.ViewModel;

//                    s.SetValue( "Height", vm.Height );
//                    return s;
//                } ).ToArray( ) );
//            }


//        }
//    }
//}
