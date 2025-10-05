using DevExpress.Xpf.Core;
using Ecng.Collections;
using Ecng.Xaml;
using StockSharp.Charting;
using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Common;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Charting.Visuals.Axes;
using SciChart.Charting.Visuals.RenderableSeries;
using Ecng.Xaml.Converters;
using MoreLinq;

using StockSharp.Localization;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using fx.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Visuals.TradeChart;
using SciChart.Charting.Model;
using SciChart.Charting.Common.Helpers;
using SciChart.Drawing.Common;
using SciChart.Core.Utility.Mouse;
using StockSharp.Xaml.Charting.Definitions;
using StockSharp.Xaml;
using StockSharp.BusinessEntities;
using SciChart.Charting.DrawingTools.TradingModifiers;
using System.Drawing;
using System.Windows.Media;
using SciChart.Charting.Themes;
using Ecng.Common;
using SciChart.Charting;
using ThemeManager = DevExpress.Xpf.Core.ThemeManager;
using StockSharp.Xaml.Charting.Xaml;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;


namespace StockSharp.Xaml.Charting;

/// <summary>
/// 
/// </summary>
public sealed class ScichartSurfaceMVVM : ChartPropertiesViewModel, IDisposable, IDrawingSurfaceVM
{
    /// <summary>
    /// Chart Components Cache 
    /// </summary>
    private sealed class ChartComponentsCache : CachedSynchronizedDictionary<IChartComponent, ChartComponentUiDomain>
    {

        private ChartComponentUiDomain[ ]? _componentViewModels =  null;

        public ChartComponentUiDomain[ ] InitCache()
        {
            lock ( ( ( SynchronizedDictionary<IChartComponent, ChartComponentUiDomain> ) this ).SyncRoot )
            {
                if ( _componentViewModels == null )
                {
                    var holders = new List<ChartComponentUiDomain>();
                    holders.AddRange( ( ( IEnumerable<KeyValuePair<IChartComponent, ChartComponentUiDomain>> ) this ).OrderBy( p => p.Key.Priority ).Select( x => x.Value ) );
                    _componentViewModels = holders.ToArray();
                }

                return _componentViewModels;
            }
        }

        protected override void OnResetCache( bool reset )
        {
            base.OnResetCache( reset );
            _componentViewModels = null;
        }
    }

    private readonly ChartComponentsCache _componentsCache = new ChartComponentsCache();

    private readonly Dictionary<IChartComponent, List<IChartSeriesViewModel>> _chartUIRSeries = new Dictionary<IChartComponent, List<IChartSeriesViewModel>>();

    private readonly Dictionary<IChartComponent, Dictionary<object, IAnnotation>> _topChartElmentAnnotationMap = new Dictionary<IChartComponent, Dictionary<object, IAnnotation>>();

    private SciChartSurface _sciChartSurface;

    private bool _doneInitialization;

    private readonly List<ChartModifierBase> _chartModifiers = new List<ChartModifierBase>();

    private readonly RubberBandXyZoomModifier _rubberBandXyZoomModifier;

    private readonly DispatcherTimer _dispatcherTimer;

    private readonly SynchronizedSet<ChartComponentUiDomain> _parentChartViewModelCache = new SynchronizedSet<ChartComponentUiDomain>();

    private readonly Queue<double> _queue = new Queue<double>();

    private double _fpsTotal;

    private readonly ObservableCollection<ChartComponentUiDomain> _legendElements = new ObservableCollection<ChartComponentUiDomain>();

    public event Action<IChartElement> RemoveElementEvent;

    private string _paneGroupSuffix;

    private readonly ChartArea _chartArea;

    private LegendModifierVM _legendViewModel;

    private AnnotationModifier _annotationModifier;

    private readonly ObservableCollection<IChartSeriesViewModel> _advanceChartRenderableSeries = new ObservableCollection<IChartSeriesViewModel>();

    private readonly AxisCollection _xAxises = new AxisCollection();

    private readonly AxisCollection _yAxises = new AxisCollection();

    private bool _paneHasCandles;

    private ChartComponentUiDomain _candleCompositeElement;

    private readonly AnnotationCollection _annotationCollection = new AnnotationCollection();

    private ModifierGroup _modifierGroup;

    private readonly ViewportManager _viewPortManager = new ViewportManager();
    private string _title;

    private string _perfStats;

    private double _height;

    private bool _showPerfStats;

    private TimeSpan _autoRangeIntervalNoGroup = TimeSpan.FromMilliseconds(200);

    private bool _showLegendNoParent;

    private bool _showOverviewNoParent;

    private int _minimumRange;

    private string _selectThemeNoParent;

    private double _dataPointWidth;

    private ICommand _resetAxisTimeZoneCommand;

    private ICommand _closePaneCommand;

    private readonly ICommand _showHiddenAxesCommand;

    private fxDataPointSelectionModifier _dataPointSelector;

    public ScichartSurfaceMVVM( ChartArea area ) : base()
    {
        _chartArea                = area ?? throw new ArgumentNullException( "area" );
        _dispatcherTimer          = new DispatcherTimer( ( DispatcherPriority ) 7, Application.Current.Dispatcher );
        _dispatcherTimer.Tick    += new EventHandler( OnTimer );
        _showHiddenAxesCommand    = ( ICommand ) new DelegateCommand( new Action( OnShowHiddenAxes ) );
        ResetAxisTimeZoneCommand  = new ActionCommand<ChartAxis>( a => a.TimeZone = null );
        Height                    = area.Height;
        area.Elements.Added      += new Action<IChartElement>( OnChartAreaElementsAdded );
        area.Elements.Removing   += new Func<IChartElement, bool>( OnChartAreaElementsRemoving );
        area.Elements.RemovingAt += ( i => OnChartAreaElementsRemoving(  area.Elements[ i ] ));

        area.Elements.Clearing += () =>
        {
            CollectionHelper.ForEach<IChartElement>( area.Elements, ( i => OnChartAreaElementsRemoving( i ) ) );
            return true;
        };

        area.XAxises.Added      += ( x => AddAxis( x, XAxises ) );
        area.XAxises.Removing   += ( a => RemoveAxis( a, XAxises ) );
        area.XAxises.RemovingAt += ( i => RemoveAxis( area.XAxises[ i ], XAxises ) );
        area.YAxises.Added      += ( y => AddAxis( y, YAxises ) );
        area.YAxises.Removing   += ( y => RemoveAxis( y, YAxises ) );
        area.YAxises.RemovingAt += ( i => RemoveAxis( area.YAxises[ i ], YAxises ) );

        
        ThemeManager.ApplicationThemeChanged += (d,e) => ChangeApplicationTheme();
        ChangeApplicationTheme();

        CollectionHelper.ForEach( Area.Elements, OnChartAreaElementsAdded );
        Area.PropertyChanged += new PropertyChangedEventHandler( OnAreaPropertyChanged );

        var rb = new RubberBandXyZoomModifier();
        rb.IsXAxisOnly = true;
        rb.ExecuteOn = ExecuteOn.MouseRightButton;
        rb.ReceiveHandledEvents = true;
        _rubberBandXyZoomModifier = rb;
    }

    

    private object GetRootElement() => ( object ) ParentViewModel ?? ( object ) this;

    public ObservableCollection<ChartComponentUiDomain> LegendElements
    {
        get => _legendElements;
    }



    private void OnAreaPropertyChanged( object obj, PropertyChangedEventArgs e )
    {
        if ( e.PropertyName == "Height" )
        {
            Height = Area.Height;
        }

        if ( e.PropertyName != "Title" )
        {
            return;
        }

        NotifyChanged( "Title" );
    }

    private void OnChartOrderMouseEvent( ModifierMouseArgs _param1 )
    {
        var chart = Chart;
        if ( chart == null )
            return;
        chart.IsAutoRange = false;
    }

    private void OnChartOrderMouseMove(
    ModifierMouseArgs _param1 )
    {
        var chart = Chart;
        if ( chart == null )
            return;
        chart.IsAutoRange = false;
    }

    public StockSharp.Charting.IChart Chart => Area.Chart;

    public ChartArea Area => _chartArea;


    /// <summary>
    /// Scichart relies on Chart Modifiers to provide functionalities for drawing labels (annotation), fib expansion, fib retracement on the drawing surface
    /// </summary>
    private void SetupModifiers()
    {
        if ( _chartModifiers.Count > 0 )
        {
            return;
        }

        var cursor                              = new UltrachartCursormodifier();
        cursor.ShowTooltip                      = true;
        cursor.ReceiveHandledEvents             = true;
        //cursor.ShowTooltipOn                  = ShowTooltipOptions.MouseHover;
        //cursor.HoverDelay                     = 1000;

        var orderLines                          = new ChartOrderModifier(Area);
        orderLines.IsEnabled                    = true;
        //orderLines.CanCreateOrders            = true;

        _dataPointSelector                      = new fxDataPointSelectionModifier();
        _dataPointSelector.ExecuteOn            = ExecuteOn.MouseMiddleButton;
        _dataPointSelector.ReceiveHandledEvents = true;
        _dataPointSelector.XAxisId              = "X";
        _dataPointSelector.IsEnabled            = true;
        _dataPointSelector.AllowsMultiSelection = true;


        var zoomPan                             = new fxZoomPanModifier();
        zoomPan.ExecuteOn                       = ExecuteOn.MouseLeftButton;
        zoomPan.ReceiveHandledEvents            = true;
        zoomPan.ClipModeX                       = ClipMode.None;
        //zoomPan.XyDirection                   = XyDirection.XYDirection;


        var xaxisDragModifier                   = new XAxisDragModifier();
        xaxisDragModifier.AxisId                = "X";
        xaxisDragModifier.ClipModeX             = ClipMode.None;

        var yaxisDragModifier                   = new YAxisDragModifier();
        yaxisDragModifier.AxisId                = "Y";

        var bandXyZoomModifier                  = new RubberBandXyZoomModifier();
        bandXyZoomModifier.IsXAxisOnly          = true;
        bandXyZoomModifier.ExecuteOn            = ExecuteOn.MouseRightButton;
        bandXyZoomModifier.ReceiveHandledEvents = true;

        var zoomExtentsModifier                 = new ZoomExtentsModifier();
        zoomExtentsModifier.ExecuteOn           = ExecuteOn.MouseDoubleClick;
        zoomExtentsModifier.XyDirection         = XyDirection.YDirection;

        var seriesValueModifer                  = new SeriesValueModifier();

        //_tradingAPI                             = new fxTradingAnnotationCreationModifier( );
        //_tradingAPI.XAxisId                     = "X";
        //_tradingAPI.YAxisId                     = "Y";
        //_tradingAPI.ExecuteOn                   = ExecuteOn.MouseLeftButton;
        //_tradingAPI.ReceiveHandledEvents        = true;

        //_annotationModifier.TradingAPI          = _tradingAPI;


        var chartModifierBaseArray = new ChartModifierBase[ 10 ];
        chartModifierBaseArray[ 0 ] = orderLines;
        chartModifierBaseArray[ 1 ] = cursor;
        chartModifierBaseArray[ 2 ] = xaxisDragModifier;
        chartModifierBaseArray[ 3 ] = yaxisDragModifier;
        chartModifierBaseArray[ 4 ] = new MouseWheelZoomModifier();
        chartModifierBaseArray[ 5 ] = bandXyZoomModifier;
        chartModifierBaseArray[ 6 ] = zoomExtentsModifier;
        chartModifierBaseArray[ 7 ] = zoomPan;
        chartModifierBaseArray[ 8 ] = _dataPointSelector;
        chartModifierBaseArray[ 9 ] = seriesValueModifer;


        _chartModifiers.AddRange( chartModifierBaseArray );
    }

    private void OnChartPropertyChanged( object _param1, PropertyChangedEventArgs e )
    {
        switch ( e.PropertyName )
        {
            case "AutoRangeInterval":
                RestartTimer();
                NotifyChanged( e.PropertyName );
                break;
            case "ShowPerfStats":
                NotifyChanged( e.PropertyName );
                break;
        }
    }

    public IEnumerable<Order> GetActiveOrders( Func<Order, bool> condition )
    {
        return _componentsCache.CachedValues.Where( p => p != null ).SelectMany(
                x =>
                {
                    return x.Elements.OfType<ChartActiveOrdersElementUiDomain>().SelectMany( ao => ao.GetActiveOrders( condition ) );
                } ); ;
    }

    /// <summary>
    /// The following mainly takes care of setting up the basic functionalities of WPF drawing MVVM mechanism. All the visuals effect of WPF is just some binding between Dependency Object and some properties of the ViewModel.
    ///     1) Add all the required modifiers.
    ///     2) if we have candles, we need to setup the draw sytle changes and properties change handlers.
    /// </summary>
    public void InitPropertiesEventHandlers()
    {
        ( ( INotifyPropertyChanged ) Chart ).PropertyChanged += new PropertyChangedEventHandler( OnChartPropertyChanged );
        if ( !_doneInitialization )
        {
            _doneInitialization = true;
            ChartModifier.ChildModifiers.Add( LegendModifier );
            ChartModifier.ChildModifiers.Add( AnnotationModifier );
            var candles = _componentsCache.Keys.OfType<IChartCandleElement>().ToArray<IChartCandleElement>();
            
            if ( candles.Length != 0 )
            {
                foreach ( IChartCandleElement candle in candles )
                {
                    OnDrawStylePropertyChanged( candle );
                    candle.PropertyChanged += new PropertyChangedEventHandler( Candle_PropertyChanged );
                }                
            }
                
        }

        AssignOrphanedChildElementsWithParent();

        if ( ParentViewModel == null )
        {
            ClosePaneCommand = ( ICommand ) null;
        }
        else
        {
            ClosePaneCommand = ( ICommand ) ParentViewModel.ClosePaneCommand;
            ParentViewModel.AddPropertyListener( ChartViewModel.ShowLegendProperty, e=> NotifyChanged( "ShowLegend" ) );
            ParentViewModel.AddPropertyListener( ChartViewModel.ShowOverviewProperty, e => NotifyChanged( "ShowOverview" ) );
            ParentViewModel.AddPropertyListener( ChartViewModel.MinimumRangeProperty, e => NotifyChanged( "MinimumRange" ) );
            ParentViewModel.AddPropertyListener( ChartViewModel.SelectedThemeProperty, e => NotifyChanged( "SelectedTheme" ) );
        }
        
        if ( GroupChart != null )
        {
            SetupModifiers();
            CollectionHelper.AddRange( ChartModifier.ChildModifiers, _chartModifiers );
            AnnotationModifier.SetBindings( AnnotationModifier.UserAnnotationTypeProperty, GroupChart, "AnnotationType" );

            var cursor = ChartModifier.ChildModifiers.OfType<UltrachartCursormodifier>().Single();
            cursor.SetBindings( ChartModifierBase.IsEnabledProperty, Chart, "CrossHair" );
            cursor.SetBindings( TooltipModifierBase.ShowAxisLabelsProperty, Chart, "CrossHairAxisLabels" );
            cursor.SetBindings( UltrachartCursormodifier.InPlaceTooltipProperty, Chart, "CrossHairTooltip" );

            var order = ChartModifier.ChildModifiers.OfType<ChartOrderModifier>().Single();            
            
            BoolAllConverter conv = new BoolAllConverter();
            conv.Value = true;

            var bindingArray = new Binding[2]
                                                      {
                                                        new Binding("OrderCreationMode")
                                                        {
                                                          Source = (object) Chart
                                                        },
                                                        new Binding("PaneHasCandles") { Source = (object) this }
                                                      };

            order.SetMultiBinding( ChartModifierBase.IsEnabledProperty, ( IMultiValueConverter ) conv, bindingArray );
            order.SetBindings( ChartOrderModifier.ShowHorizontalLineProperty, ( object ) Chart, "CrossHair", BindingMode.OneWay, ( IValueConverter ) new InverseBooleanConverter() );

            ChartModifier.ChildModifiers.OfType<fxZoomPanModifier>().Single<fxZoomPanModifier>().SetBindings( ChartModifierBase.IsEnabledProperty, Chart, "AnnotationType", BindingMode.OneWay, ( IValueConverter ) new EnumBooleanConverter(), ( object ) ChartAnnotationTypes.None.ToString() );

            MouseManager.SetMouseEventGroup( ( DependencyObject ) ChartModifier, PaneGroup );
        }
        RestartTimer();
        CollectionHelper.ForEach<IChartAxis>( ( IEnumerable<IChartAxis> ) Area.XAxises, new Action<IChartAxis>( OnAddXAxises ) );
        CollectionHelper.ForEach<IChartAxis>( ( IEnumerable<IChartAxis> ) Area.YAxises, new Action<IChartAxis>( OnAddYAxises ) );
    }

    public void Release()
    {
        ( ( INotifyPropertyChanged ) Chart ).PropertyChanged -= new PropertyChangedEventHandler( OnChartPropertyChanged );
        if ( GroupChart != null )
            _chartModifiers.ForEach( new Action<ChartModifierBase>( OnReleaseModifiersD ) );
        CollectionHelper.ForEach<IChartAxis>( ( IEnumerable<IChartAxis> ) Area.XAxises, new Action<IChartAxis>( OnRemoveXAxis ) );
        CollectionHelper.ForEach<IChartAxis>( ( IEnumerable<IChartAxis> ) Area.YAxises, new Action<IChartAxis>( OnRemoveYAxises ) );
        _dispatcherTimer.Stop();
    }

    private void RestartTimer()
    {
        _dispatcherTimer.Stop();
        _dispatcherTimer.Interval = AutoRangeInterval;
        _dispatcherTimer.Start();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="axis"></param>
    /// <param name="axises"></param>
    private void AddAxis( IChartAxis axis, ICollection<IAxis> axises )
    {
        if ( Chart == null )
            return;

        AxisBase xBase = AxisBaseHelper2025.InitAndSetBinding(axis, ParentViewModel?.RemoveAxisCommand, ResetAxisTimeZoneCommand, Chart);
        xBase.PropertyChanged += OnAxisBasePropertyChanged;
        
        ( ( DispatcherObject ) Chart ).GuiAsync( () =>
        {
            if ( Chart == null )
                return;

            var ab = AxisBaseHelper2025.InitAndSetBinding(axis, ParentViewModel?.RemoveAxisCommand, ResetAxisTimeZoneCommand, Chart);
            ab.PropertyChanged += OnAxisBasePropertyChanged;
            axises.Add(ab);

            if ( axises != XAxises )
                return;

            SetupAxisBindings();
        } );
    }    

    private void OnAxisBasePropertyChanged( object sender, PropertyChangedEventArgs e )
    {
        if ( !( sender is CategoryDateTimeAxis timeAxis ) || e.PropertyName != "CurrentDatapointPixelSize" )
            return;

        var candle = _componentsCache.Keys.OfType<IChartCandleElement>().FirstOrDefault();
        if ( candle != null && candle.XAxisId != timeAxis.Id )
            return;

        DataPointWidth = Math.Round( MathHelper.IsNaN( timeAxis.CurrentDatapointWidth ) ? 0.0 : timeAxis.CurrentDatapointWidth, 1, MidpointRounding.AwayFromZero );
    }
        

    private void SetupAxisBindings()
    {
        foreach ( AxisBase axis in XAxises.OfType<AxisBase>() )
        {
            if ( axis.Tag is IChartAxis tag )
            {
                VisibleRangeDpo rangeDep = VisibleRangeDpo.AddRangeProperty( GetRootElement(), tag.Group, PaneGroupSuffix, tag.AxisType );
                
                string path;

                if ( rangeDep.GetAxisType() == ChartAxisType.CategoryDateTime )
                {
                    path = "CategoryDateTimeRange";
                }
                else if ( rangeDep.GetAxisType() == ChartAxisType.Numeric )
                {
                    path = "NumericRange";
                }
                else if ( rangeDep.GetAxisType() == ChartAxisType.DateTime )
                {
                    path = "DateTimeRange";
                }
                else
                {
                    throw new NotSupportedException( "unsupported range type" );
                }                                        

                axis.SetBindings( AxisBase.VisibleRangeProperty, rangeDep, path );
            }
        }
    }

    private bool RemoveAxis( IChartAxis axis, ICollection<IAxis> axisColl )
    {
        if ( Chart != null )
        {
            ( ( DispatcherObject ) Chart ).GuiAsync( () =>
            {
                var target = (AxisBase) axisColl.FirstOrDefault<IAxis>( x => x.Id == axis.Id);
                if ( target == null )
                    return;

                target.PropertyChanged -= OnTargetPropertyChanged;
                BindingOperations.ClearAllBindings( ( DependencyObject ) target );
                axisColl.Remove( ( IAxis ) target );
            } );
        }
            
        return true;
    }

    
    private void OnTargetPropertyChanged( object _param1, PropertyChangedEventArgs _param2 )
    {
        if ( !( _param1 is CategoryDateTimeAxis timeAxis ) || _param2.PropertyName != "CurrentDatapointPixelSize" )
            return;

        var candle = _componentsCache.Keys.OfType<IChartCandleElement>().FirstOrDefault();

        if ( candle != null && candle.XAxisId != timeAxis.Id )
            return;

        DataPointWidth = Math.Round( MathHelper.IsNaN( timeAxis.CurrentDatapointWidth ) ? 0.0 : timeAxis.CurrentDatapointWidth, 1, MidpointRounding.AwayFromZero );
    }

    public void SetScichartSurface( SciChartSurface s )
    {
        if ( _sciChartSurface == s )
            return;

        _sciChartSurface = _sciChartSurface == null || s == null ? s : throw new InvalidOperationException( "got unexpected chart surface" );

        if ( s?.RenderSurface != null )
        {            
            s.RenderSurface.Rendered += OnRenderSurfaceRendered;
        }
    }

    public void Draw( ChartDrawData d )
    {
        if ( _sciChartSurface != null )
        {
            using ( _sciChartSurface.SuspendUpdates() )
                StartRenderingChartUIs( d );
        }
        else
            StartRenderingChartUIs( d );
    }

    private void OnRenderSurfaceRendered( object sender, RenderedEventArgs e )
    {
        if ( _sciChartSurface == null )
            return;

        CollectionHelper.ForEach<CategoryDateTimeAxis>( XAxises.OfType<CategoryDateTimeAxis>(),
            axis =>
            {
                if ( axis.Tag is ChartAxis tag )
                {
                    tag.DataPointWidth = axis.CurrentDatapointWidth;
                }
            } );

        CollectionHelper.ForEach<ChartComponentUiDomain>( _componentsCache.CachedValues, component => component.UpdateYAxisMarker() );

        if ( ShowPerfStats && e.Duration > 0.0 )
        {
            double fps = 1000.0 / e.Duration;
            while ( _queue.Count >= 10 )
                _fpsTotal -= _queue.Dequeue();

            _queue.Enqueue( fps );
            _fpsTotal += fps;

            PerfStats = $"FPS: {_fpsTotal / _queue.Count:0}   Count: {_sciChartSurface.RenderableSeries.Count( series => series.IsVisible ):n0}";
        }
    }

    private void StartRenderingChartUIs( ChartDrawData d )
    {
        foreach ( var vm in _componentsCache.InitCache() )
        {
            if ( vm.Draw( d ) )
                 _parentChartViewModelCache.Add( vm );
        }
    }

    private void OnTimer( object _param1, EventArgs _param2 )
    {
        ChartComponentUiDomain[] vms;
        lock (  _parentChartViewModelCache.SyncRoot )
            vms = CollectionHelper.CopyAndClear<ChartComponentUiDomain>( _parentChartViewModelCache );

        foreach ( ChartComponentUiDomain vm in vms )
            vm.PerformPeriodicalAction();
    }

    public void Reset( IEnumerable<IChartElement> chartElement )
    {
        foreach ( IChartComponent comp in chartElement )
        {
            ChartComponentUiDomain vm;
            if ( GetViewModelFromCache( comp, out vm ))
      {
                _parentChartViewModelCache.Add( vm );
                vm.Reset();
            }
        }
    }

    private void AssignOrphanedChildElementsWithParent()
    {
        foreach ( var chartComp in ( _componentsCache ).Where(p => p.Value == null ).Select( p => p.Key ).ToArray() )
        {
            ChartComponentUiDomain vm = new ChartComponentUiDomain(this, chartComp);

            vm.InitializeChildElements( chartComp.ChildElements.Append2( chartComp )
                                        .OfType<IChartElementUiDomain>()
                                        .Where( e => !e.DontDraw )
                                        .Select( e => e.CreateViewModel( this ) )
                                        .Where( e => e != null ));

            
            ( ( SynchronizedDictionary<IChartComponent, ChartComponentUiDomain> ) _componentsCache )[ chartComp ] = vm;
            if ( chartComp.IsLegend )
                LegendElements.Add( vm );
        }
        CheckForCandles();
    }

    private void CheckForCandles()
    {
        CandlesCompositeElement = _componentsCache.FirstOrDefault( p => p.Key is IChartCandleElement ).Value;
        PaneHasCandles = CandlesCompositeElement != null;
    }

    public bool GetViewModelFromCache( IChartComponent comp, out ChartComponentUiDomain viewModel )
    {
        return  _componentsCache.TryGetValue( comp, out viewModel );
    }


    /// <summary>
    /// This is the event handler when we add a new chart element to the chart area. 
    /// 
    /// eg. If we are going to add a CandleStick Viusal UI to the chart, this is what is happening behind the scene.
    ///     1) First thing first, WPF UI drawing routine has to be done in the UI main thread. 
    ///         * Chart.EnsureUIThread();
    ///         
    ///     2) When we scroll the XAxis or YAxis, the candleStick need to scroll as well. This is accomplished by the following code.
    ///         * chartComponent.AddAxisesAndEventHandler( Area );
    ///         
    ///     3) Since a Candle is made up of multiple children UI elements (High, Low, Open, Close ), we need to create and initialize all the children UI.
    /// </summary>
    /// <param name="anyChartUI"></param>
    /// <exception cref="ArgumentException"></exception>
    public void OnChartAreaElementsAdded( IChartElement anyChartUI )
    {        
        if ( Chart != null )
        {
            Chart.EnsureUIThread();
        }
            
        IChartComponent chartComponent = (IChartComponent) anyChartUI;
        chartComponent.AddAxisesAndEventHandler( Area );
        
        if ( _componentsCache.ContainsKey( chartComponent ) )
        {
            throw new ArgumentException( "duplicate chart element", "element" );
        }

        if ( Chart != null )
        {
            var vm = new ChartComponentUiDomain(this, chartComponent);
            vm.InitializeChildElements( CollectionHelper.Append2( chartComponent.ChildElements, chartComponent ).OfType<IChartElementUiDomain>()
                                                        .Where( e => !e.DontDraw )
                                                        .Select( d => d.CreateViewModel( this ) ) );

            _componentsCache[ chartComponent ] = vm;

            if ( chartComponent.IsLegend )
            {
                LegendElements.Add( vm );
            }

        }
        else
        {
            _componentsCache[ chartComponent ] = null;
        }
            
        if ( _modifierGroup != null && chartComponent is IChartCandleElement candle )
        {
            OnDrawStylePropertyChanged( candle );
            candle.PropertyChanged += new PropertyChangedEventHandler( Candle_PropertyChanged );
        }
        CheckForCandles();
        chartComponent.PropertyChanged += new PropertyChangedEventHandler( XYAxisId_PropertyChanged );
    }

    public bool OnChartAreaElementsRemoving( IChartElement _param1 )
    {
        var chart = Chart;
        if ( chart != null )
            chart.EnsureUIThread();
        IChartComponent chartComp = (IChartComponent) _param1;
        ChartComponentUiDomain vm;
        if ( !GetViewModelFromCache( chartComp, out vm ))
      return false;
        chartComp.RemoveAxisesEventHandler();
        if ( chartComp is IChartCandleElement chartCandleElement )
            chartCandleElement.PropertyChanged -= new PropertyChangedEventHandler( Candle_PropertyChanged );
        chartComp.PropertyChanged -= new PropertyChangedEventHandler( XYAxisId_PropertyChanged );
        ( ( SynchronizedDictionary<IChartComponent, ChartComponentUiDomain> ) _componentsCache ).Remove( chartComp );
        if ( vm != null )
        {
            vm.GuiUpdateAndClear();
            vm.Dispose();
            LegendElements.Remove( vm );
        }
        CheckForCandles();
        return true;
    }

    private void XYAxisId_PropertyChanged( object chartComponent, PropertyChangedEventArgs e )
    {
        IChartComponent chartComp = (IChartComponent) chartComponent;
        if ( e.PropertyName != "XAxisId" && e.PropertyName != "YAxisId" )
            return;
        var chart = Chart;
        if ( chart != null )
            chart.EnsureUIThread();

        List<IChartSeriesViewModel> multipleSeries;

        if ( _chartUIRSeries.TryGetValue( chartComp, out multipleSeries ) )
        {
            if ( chartComp.TryGetXAxis() != null && chartComp.TryGetYAxis() != null )
            {
                foreach ( var rSerie in multipleSeries )
                {
                    if ( !_advanceChartRenderableSeries.Contains( rSerie ) )
                        _advanceChartRenderableSeries.Add( rSerie );
                }
            }
            else
            {
                foreach ( var oneSerie in multipleSeries )
                    _advanceChartRenderableSeries.Remove( oneSerie );
            }
        }
        Dictionary<object, IAnnotation> objectAnnotation;
        if ( !_topChartElmentAnnotationMap.TryGetValue( chartComp, out objectAnnotation ) )
            return;

        if ( chartComp.TryGetXAxis() != null && chartComp.TryGetYAxis() != null )
        {
            foreach ( KeyValuePair<object, IAnnotation> keyValuePair in objectAnnotation )
            {
                if ( !Annotations.Contains( keyValuePair.Value ) )
                    Annotations.Add( keyValuePair.Value );
            }
        }
        else
        {
            foreach ( KeyValuePair<object, IAnnotation> keyValuePair in objectAnnotation )
                Annotations.Remove( keyValuePair.Value );
        }
    }

    /// <summary>
    /// We only care about the DrawStyle property change of the candle, since that is the only property that can change the draw style of the candle.
    /// 
    /// This code is mainly used to take care of changing the drawing color of the candle.
    /// 
    /// In China, trader use red color for rising candle, and other color for falling candle.
    /// In USA, trader use green color for rishing candle and red color for falling candle.
    /// </summary>
    /// <param name="candle"></param>
    /// <param name="e"></param>
    private void Candle_PropertyChanged( object candle, PropertyChangedEventArgs e )
    {
        if ( ( e.PropertyName != "DrawStyle" ) )
            return;
        OnDrawStylePropertyChanged( ( IChartCandleElement ) candle );
    }

    private void OnDrawStylePropertyChanged( IChartCandleElement candle )
    {
        _rubberBandXyZoomModifier.IsXAxisOnly = !candle.DrawStyle.IsVolumeProfileChart();
    }

    public VisibleRangeDpo GetVisibleRangeDpo( string axisId )
    {
        IChartAxis chartAxis =  Area.XAxises.FirstOrDefault<IChartAxis>( p => p.Id == axisId );

        return chartAxis == null ? null : VisibleRangeDpo.AddRangeProperty( GetRootElement(), chartAxis.Group, PaneGroupSuffix, chartAxis.AxisType );
    }

    private void OnShowHiddenAxes()
    {
        foreach ( ChartAxis xAxis in Area.XAxises )
        {
            xAxis.IsVisible = true;
        }

        foreach ( ChartAxis yAxis in Area.YAxises )
        {
            yAxis.IsVisible = true;
        }
    }

    public string PaneGroupSuffix
    {
        get => _paneGroupSuffix;
        set
        {
            value = value?.Trim() ?? string.Empty;
            if ( !SetField<string>( ref _paneGroupSuffix, value, nameof( PaneGroupSuffix ) ) )
                return;
            NotifyChanged( "PaneGroup" );
            if ( GroupChart == null )
                return;
            SetupAxisBindings();
            MouseManager.SetMouseEventGroup( ( DependencyObject ) ChartModifier, PaneGroup );
        }
    }

    public string PaneGroup
    {
        get
        {
            object[] objArray = new object[2];

            if ( GroupChart != null )
            {
                throw new NotSupportedException();
            }
            
            objArray[0] = GroupChart.ViewModel.InstanceCount;
            objArray[1] = _paneGroupSuffix;
            return StringHelper.Put( "ssharpultrachart{0}_{1}", objArray );
        }
    }



    public StockSharp.Xaml.Charting.Chart GroupChart => Chart as StockSharp.Xaml.Charting.Chart;



    public ChartViewModel ParentViewModel
    {
        get => GroupChart?.ViewModel;
    }

    public LegendModifierVM LegendViewModel
    {
        get
        {
            if ( _legendViewModel != null )
                return _legendViewModel;
            _legendViewModel = new LegendModifierVM( this )
            {
                LegendModifier = new LegendModifierEx()
            };
            
            _legendViewModel.RemoveElementEvent +=  OnRemoveElementEvent;
            return _legendViewModel;
        }
    }

    public AnnotationModifier AnnotationModifier
    {
        get
        {
            if ( _annotationModifier != null )
            {
                return _annotationModifier;
            }

            var annotation = new AnnotationModifier(Area, Annotations);
            annotation.IsEnabled = false;
            _annotationModifier = annotation;

            return annotation;
        }
    }

    public IEnumerable<IChartSeriesViewModel> ChartSeriesViewModels
    {
        get
        {
            return ( IEnumerable<IChartSeriesViewModel> ) _advanceChartRenderableSeries;
        }
    }

    public void AddAxisMakerAnnotation( IChartComponent component, IAnnotation annotation, object axisMarker )
    {
        if ( axisMarker == null )
            throw new ArgumentNullException( "key" );
        Dictionary<object, IAnnotation> objectAnnotation;
        if ( !_topChartElmentAnnotationMap.TryGetValue( component, out objectAnnotation ) )
            _topChartElmentAnnotationMap[ component ] = objectAnnotation = new Dictionary<object, IAnnotation>();
        objectAnnotation.Add( axisMarker, annotation );
        if ( component.TryGetXAxis() == null || component.TryGetYAxis() == null )
            return;
        Annotations.Add( annotation );
    }

    public IAnnotation GetAxisMakerAnnotation( IChartComponent component, object annotationPair )
    {
        Dictionary<object, IAnnotation> objectAnnotation;
        return !_topChartElmentAnnotationMap.TryGetValue( component, out objectAnnotation ) ? ( IAnnotation ) null : CollectionHelper.TryGetValue<object, IAnnotation>( ( IDictionary<object, IAnnotation> ) objectAnnotation, annotationPair );
    }

    public void RemoveAnnotation( IChartComponent component, object objAnnoPair )
    {
        Dictionary<object, IAnnotation> objectAnnotation;
        if ( !_topChartElmentAnnotationMap.TryGetValue( component, out objectAnnotation ) )
            return;
        if ( objAnnoPair == null )
        {
            foreach ( KeyValuePair<object, IAnnotation> keyValuePair in objectAnnotation )
                Annotations.Remove( keyValuePair.Value );
            _topChartElmentAnnotationMap.Remove( component );
        }
        else
        {
            IAnnotation annotation;
            if ( !objectAnnotation.TryGetValue( objAnnoPair, out annotation ) )
                return;
            Annotations.Remove( annotation );
            objectAnnotation.Remove( objAnnoPair );
        }
    }

    public void AddSeriesViewModelsToRoot( IChartComponent component, IChartSeriesViewModel rSeries )
    {
        List<IChartSeriesViewModel> componentRSeries;
        if ( !_chartUIRSeries.TryGetValue( component, out componentRSeries ) )
            _chartUIRSeries[ component ] = componentRSeries = new List<IChartSeriesViewModel>();
        if ( !componentRSeries.Contains( rSeries ) )
            componentRSeries.Add( rSeries );
        XYAxisId_PropertyChanged( ( object ) component, new PropertyChangedEventArgs( "XAxisId" ) );
    }

    /// <summary>
    /// Remove the root and the underlying chartElement from the chart
    /// </summary>
    /// <param name="component"></param>
    public void RemoveChartComponent( IChartComponent component )
    {        
        if ( !_chartUIRSeries.TryGetValue( component, out var componentRSeries ) )
            return;

        foreach ( var serie in componentRSeries )
        {
            _advanceChartRenderableSeries.Remove( serie );
        }
            
        _chartUIRSeries.Remove( component );
    }

    //public void RemoveChartComponent2( IChartComponent elementXY )
    //{
    //    if ( !_chartUIRSeries.TryGetValue( elementXY, out var rSeriesList ) )
    //    {
    //        return;
    //    }

    //    foreach ( var rSerie in rSeriesList )
    //    {
    //        _advanceChartRenderableSeries.Remove( rSerie );
    //    }

    //    _chartUIRSeries.Remove( elementXY );
    //}

    public void Refresh()
    {
        _sciChartSurface?.InvalidateElement();
    }

    public AxisCollection XAxises
    {
        get => _xAxises;
    }

    public AxisCollection YAxises
    {
        get => _yAxises;
    }

    public ChartComponentUiDomain CandlesCompositeElement
    {
        get => _candleCompositeElement;
        private set => _candleCompositeElement = value;
    }

    public bool PaneHasCandles
    {
        get => _paneHasCandles;
        set
        {
            SetField<bool>( ref _paneHasCandles, value, nameof( PaneHasCandles ) );
        }
    }

    public AnnotationCollection Annotations
    {
        get => _annotationCollection;
    }

    public ModifierGroup ChartModifier
    {
        get
        {
            return _modifierGroup ?? ( _modifierGroup = new ModifierGroup() );
        }
        set
        {
            SetField<ModifierGroup>( ref _modifierGroup, value, nameof( ChartModifier ) );
        }
    }

    public ViewportManager ViewportManager
    {
        get => _viewPortManager;
    }

    public LegendModifier LegendModifier
    {
        get => LegendViewModel.LegendModifier;
    }

    public string Title
    {
        get => Area.Title;
        set => Area.Title = value;
    }

    public string PerfStats
    {
        get => _perfStats;
        set
        {
            SetField<string>( ref _perfStats, value, nameof( PerfStats ) );
        }
    }

    public double Height
    {
        get => _height;
        set
        {
            SetField<double>( ref _height, value, nameof( Height ) );
            Area.Height = (float) value;
        }
    }

    public bool ShowPerfStats
    {
        get
        {
            StockSharp.Xaml.Charting.Chart groupChart = GroupChart;
            return groupChart == null ? _showPerfStats :  groupChart.ShowPerfStats;
        }
        set
        {
            if ( Chart is StockSharp.Xaml.Charting.Chart chart )
            {
                chart.ShowPerfStats = value;
                NotifyChanged( nameof( ShowPerfStats ) );
            }
            else
                SetField<bool>( ref _showPerfStats, value, nameof( ShowPerfStats ) );
        }
    }

    public TimeSpan AutoRangeInterval
    {
        get
        {
            StockSharp.Xaml.Charting.Chart groupChart = GroupChart;
            return groupChart == null ? _autoRangeIntervalNoGroup : groupChart.AutoRangeInterval;
        }
        set
        {
            if ( Chart is StockSharp.Xaml.Charting.Chart chart )
            {
                chart.AutoRangeInterval = value;
                NotifyChanged( nameof( AutoRangeInterval ) );
            }
            else
                SetField<TimeSpan>( ref _autoRangeIntervalNoGroup, value, nameof( AutoRangeInterval ) );
        }
    }

    public bool ShowLegend
    {
        get
        {
            ChartViewModel parentViewModel = ParentViewModel;
            return parentViewModel == null ? _showLegendNoParent : parentViewModel.ShowLegend;
        }
        set
        {
            if ( ParentViewModel != null )
            {
                ParentViewModel.ShowLegend = value;
                NotifyChanged( nameof( ShowLegend ) );
            }
            else
                SetField<bool>( ref _showLegendNoParent, value, nameof( ShowLegend ) );
        }
    }

    public bool ShowOverview
    {
        get
        {
            ChartViewModel parentViewModel = ParentViewModel;
            return parentViewModel == null ? _showOverviewNoParent : parentViewModel.ShowOverview;
        }
        set
        {
            if ( ParentViewModel != null )
            {
                ParentViewModel.ShowOverview = value;
                NotifyChanged( nameof( ShowOverview ) );
            }
            else
                SetField<bool>( ref _showOverviewNoParent, value, nameof( ShowOverview ) );
        }
    }

    public int MinimumRange
    {
        get
        {
            ChartViewModel parentViewModel = ParentViewModel;
            return parentViewModel == null ? _minimumRange : parentViewModel.MinimumRange;
        }
        set
        {
            if ( ParentViewModel != null )
            {
                ParentViewModel.MinimumRange = value;
                NotifyChanged( nameof( MinimumRange ) );
            }
            else
                SetField<int>( ref _minimumRange, value, nameof( MinimumRange ) );
        }
    }

    public string SelectedTheme
    {
        get => ParentViewModel?.SelectedTheme ?? _selectThemeNoParent;
        set
        {
            if ( ParentViewModel != null )
            {
                ParentViewModel.SelectedTheme = value;
                NotifyChanged( nameof( SelectedTheme ) );
            }
            else
                SetField<string>( ref _selectThemeNoParent, value, nameof( SelectedTheme ) );
        }
    }

    public double DataPointWidth
    {
        get => _dataPointWidth;
        set
        {
            SetField<double>( ref _dataPointWidth, value, nameof( DataPointWidth ) );
        }
    }

    public ICommand ResetAxisTimeZoneCommand
    {
        get => _resetAxisTimeZoneCommand;
        set
        {
            SetField<ICommand>( ref _resetAxisTimeZoneCommand, value, nameof( ResetAxisTimeZoneCommand ) );
        }
    }

    public ICommand ClosePaneCommand
    {
        get => _closePaneCommand;
        set => _closePaneCommand = value;
    }

    public ICommand ShowHiddenAxesCommand => _showHiddenAxesCommand;    

    public void ZoomExtents()
    {
        throw new NotImplementedException();
    }

    private void ChangeApplicationTheme() => SelectedTheme = ChartHelper2025.CurrChartTheme();

    public void Dispose()
    {
        _sciChartSurface?.Dispose();
        _sciChartSurface = ( SciChartSurface ) null;
    }

    public bool AllowElementToBeRemoved( ChartComponentUiDomain vm )
    {
        if ( ParentViewModel == null || !ParentViewModel.IsInteracted )
            return false;
        
        bool flag;
        switch ( vm.RootChartComponent )
        {
            case IChartCandleElement _:
                flag = ParentViewModel.AllowAddCandles;
                break;
            case IChartIndicatorElement _:
                flag = ParentViewModel.AllowAddIndicators;
                break;
            case IChartOrderElement _:
                flag = ParentViewModel.AllowAddOrders;
                break;
            case IChartTradeElement _:
                flag = ParentViewModel.AllowAddOwnTrades;
                break;
            default:
                flag = false;
                break;
        }
        return flag;
    }





    private void SomeWhTekjerek023( IChartCandleElement _param1 )
    {
        OnDrawStylePropertyChanged( _param1 );
        _param1.PropertyChanged += new PropertyChangedEventHandler( Candle_PropertyChanged );
    }

    private void OnShowLegendProperty(
      DependencyPropertyChangedEventArgs _param1 )
    {
        NotifyChanged( "ShowLegend" );
    }

    private void OnShowOverview( DependencyPropertyChangedEventArgs _param1 )
    {
        NotifyChanged( "ShowOverview" );
    }

    private void OnMinimumRangeProperty( DependencyPropertyChangedEventArgs _param1 )
    {
        NotifyChanged( "MinimumRange" );
    }

    private void OnSelectedThemeProperty(
      DependencyPropertyChangedEventArgs _param1 )
    {
        NotifyChanged( "SelectedTheme" );
    }

    private void OnAddXAxises( IChartAxis _param1 )
    {
        AddAxis( _param1, ( ICollection<IAxis> ) XAxises );
    }

    private void OnAddYAxises( IChartAxis _param1 )
    {
        AddAxis( _param1, ( ICollection<IAxis> ) YAxises );
    }

    private void OnReleaseModifiersD(
      ChartModifierBase _param1 )
    {
        ChartModifier.ChildModifiers.Remove( ( IChartModifier ) _param1 );
    }

    private void OnRemoveXAxis( IChartAxis _param1 )
    {
        RemoveAxis( _param1, ( ICollection<IAxis> ) XAxises );
    }

    private void OnRemoveYAxises( IChartAxis _param1 )
    {
        RemoveAxis( _param1, ( ICollection<IAxis> ) YAxises );
    }
    
    private ChartElementUiDomain CreateDrawableChartElementBaseViewModel( IChartElementUiDomain _param1 )
    {
        return _param1.CreateViewModel( this );
    }

    private ChartElementUiDomain NewDrawableChartElementBaseViewModel( IChartElementUiDomain d )
    {
        return d.CreateViewModel( this );
    }

    private void OnRemoveElementEvent( IChartElement _param1 )
    {
        ParentViewModel?.InvokeRemoveElementEvent( _param1 );

        Action<IChartElement> myAction = RemoveElementEvent;
        if ( myAction == null )
            return;
        myAction( _param1 );
    }



    #region ----------------------------- Tony's new Implementation ----------------------------- 
    public void AddRenderableSeriesToChartSurface( IChartComponent elementXY, IChartSeriesViewModel rs )
    {        
        if ( !_chartUIRSeries.TryGetValue( elementXY, out var rSeriesList ) )
        {
            _chartUIRSeries[elementXY] = rSeriesList = new List<IChartSeriesViewModel>( );
        }

        if ( !rSeriesList.Contains( rs ) )
        {
            rSeriesList.Add( rs );
        }

        OnChartComponentPropertiesChanged( elementXY, new PropertyChangedEventArgs( "XAxis" ) );
    }

    


    /// <summary>
    /// Handles the PropertyChanged event of the XYAxis element.  Directly copy the code from
    /// StockSharp.Charting.IChartExtensions because I still haven't change my namespace from StockSharp.Xaml.Charting
    /// to Stocksharp.xaml.Charting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnChartComponentPropertiesChanged( object sender, PropertyChangedEventArgs e )
    {
        IChartComponent elementXY = (IChartComponent)sender;

        if ( e.PropertyName != "XAxis" && e.PropertyName != "YAxis" )
        {
            return;
        }

        IChart chart = Chart;

        if ( chart != null )
        {
            Ecng.Xaml.XamlHelper.EnsureUIThread( chart );
        }        

        ChartAxis xAxis = null;
        ChartAxis yAxis = null;

        if ( _chartUIRSeries.TryGetValue( elementXY, out var rSeriesList ) )
        {
            // Directly used the code from StockSharp.Charting.IChartExtensions.TryGetXAxis
            xAxis = ( ChartAxis ) elementXY.CheckOnNull( nameof( elementXY ) ).ChartArea?.XAxises.FirstOrDefault(
                xa => xa.Id == elementXY.XAxisId );

            // Directly used the code from StockSharp.Charting.IChartExtensions.TryGetXAxis
            yAxis = ( ChartAxis ) elementXY.CheckOnNull( nameof( elementXY ) ).ChartArea?.YAxises.FirstOrDefault(
                xa => xa.Id == elementXY.YAxisId );

            if ( xAxis != null || yAxis != null )
            {
                foreach ( var rSerie in rSeriesList )
                {
                    if ( !_advanceChartRenderableSeries.Contains( rSerie ) )
                    {
                        _advanceChartRenderableSeries.Add( rSerie );
                    }
                }
            }
            else
            {
                foreach ( var rSerie in rSeriesList )
                {
                    _advanceChartRenderableSeries.Remove( rSerie );
                }
            }
        }

        
        if ( !_topChartElmentAnnotationMap.TryGetValue( elementXY, out var dictionary ) )
        {
            return;
        }

        if ( xAxis != null || yAxis != null )
        {
            foreach ( KeyValuePair<object, IAnnotation> keyValuePair in dictionary )
            {
                if ( !_annotationCollection.Contains( keyValuePair.Value ) )
                {
                    _annotationCollection.Add( keyValuePair.Value );
                }
            }
        }
        else
        {
            foreach ( KeyValuePair<object, IAnnotation> keyValuePair in dictionary )
            {
                _annotationCollection.Remove( keyValuePair.Value );
            }
        }
    }

    public bool HasMultipleBarsHighlighted
    {
        get
        {
            return _dataPointSelector.HasMultipleBarsHighlighted;
        }
    }

    public PooledList<long> HighlightedBarLinxTime
    {
        get
        {
            return _dataPointSelector.HighlightedBarLinuxTime;
        }
    }
    #endregion
}

    
