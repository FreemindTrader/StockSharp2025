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
public sealed class DrawingSurfaceViewModel : ChartBaseViewModel,
                                          IDisposable,
                                          IScichartSurfaceVM
{
    /// <summary>
    /// Chart Components Cache 
    /// </summary>
    private sealed class ChartComponentsCache : CachedSynchronizedDictionary<IChartComponent, ChartCompentViewModel>
    {

        private ChartCompentViewModel[ ]? _componentViewModels =  null;

        public ChartCompentViewModel[ ] InitCache()
        {
            lock ( ( ( SynchronizedDictionary<IChartComponent, ChartCompentViewModel> ) this ).SyncRoot )
            {
                if ( _componentViewModels == null )
                {
                    var holders = new List<ChartCompentViewModel>();
                    holders.AddRange( ( ( IEnumerable<KeyValuePair<IChartComponent, ChartCompentViewModel>> ) this ).OrderBy( p => p.Key.Priority ).Select( x => x.Value ) );
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

    private readonly Dictionary<IChartComponent, List<IRenderableSeries>> _chartUIRSeries = new Dictionary<IChartComponent, List<IRenderableSeries>>();

    private readonly Dictionary<IChartComponent, Dictionary<object, IAnnotation>> _topChartElmentAnnotationMap = new Dictionary<IChartComponent, Dictionary<object, IAnnotation>>();

    private SciChartSurface _sciChartSurface;

    private bool _doneInitialization;

    private readonly List<ChartModifierBase> _chartModifiers = new List<ChartModifierBase>();

    private readonly RubberBandXyZoomModifier _rubberBandXyZoomModifier;

    private readonly DispatcherTimer _dispatcherTimer;

    private readonly SynchronizedSet<ChartCompentViewModel> _parentChartViewModelCache = new SynchronizedSet<ChartCompentViewModel>();

    private readonly Queue<double> _queue = new Queue<double>();

    private double _fpsTotal;

    private readonly ObservableCollection<ChartCompentViewModel> _legendElements = new ObservableCollection<ChartCompentViewModel>();

    public event Action<IChartElement> RemoveElementEvent;

    private string _paneGroupSuffix;

    private readonly ChartArea _chartArea;

    private LegendModifierVM _legendViewModel;

    private FxAnnotationModifier _annotationModifier;

    private readonly ObservableCollection<IRenderableSeries> _advanceChartRenderableSeries = new ObservableCollection<IRenderableSeries>();

    private readonly AxisCollection _xAxises = new AxisCollection();

    private readonly AxisCollection _yAxises = new AxisCollection();

    private bool _paneHasCandles;

    private ChartCompentViewModel _candleCompositeElement;

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

    public DrawingSurfaceViewModel( ChartArea area ) : base()
    {
        DrawingSurfaceViewModel.SomeClass6409 _someInstance08383 = new DrawingSurfaceViewModel.SomeClass6409();


        _someInstance08383._variableSome3535 = this;
        _chartArea = area ?? throw new ArgumentNullException( "area" );
        _dispatcherTimer = new DispatcherTimer( ( DispatcherPriority ) 7, Application.Current.Dispatcher );
        _dispatcherTimer.Tick += new EventHandler( OnTimer );
        _showHiddenAxesCommand = ( ICommand ) new DelegateCommand( new Action( OnShowHiddenAxes ) );
        ResetAxisTimeZoneCommand = new ActionCommand<ChartAxis>( a => a.TimeZone = null );
        Height = area.Height;
        area.Elements.Added += new Action<IChartElement>( OnChartAreaElementsAdded );
        area.Elements.Removing += new Func<IChartElement, bool>( OnChartAreaElementsRemoving );
        area.Elements.RemovingAt += new Func<int, bool>( _someInstance08383.OnChartAreaElementsRemovingAt );
        area.Elements.Clearing += new Func<bool>( _someInstance08383.OnChartAreaElementsClearing );
        area.XAxises.Added += new Action<IChartAxis>( _someInstance08383.OnAreaXAxisesAdded );
        area.XAxises.Removing += new Func<IChartAxis, bool>( _someInstance08383.OnAreaXAxisesRemoving );
        area.XAxises.RemovingAt += new Func<int, bool>( _someInstance08383.OnAreaXAxisesRemovingAt );
        area.YAxises.Added += new Action<IChartAxis>( _someInstance08383.OnAreaYAxisesAdded );
        area.YAxises.Removing += new Func<IChartAxis, bool>( _someInstance08383.OnAreaYAxisesRemoving );
        area.YAxises.RemovingAt += new Func<int, bool>( _someInstance08383.OnAreaYAxisesRemovingAt );
        ThemeManager.ApplicationThemeChanged += new ThemeChangedRoutedEventHandler( _someInstance08383.OnApplicationThemeChanged );
        ChangeApplicationTheme();
        CollectionHelper.ForEach<IChartElement>( ( IEnumerable<IChartElement> ) Area.Elements, new Action<IChartElement>( OnChartAreaElementsAdded ) );
        Area.PropertyChanged += new PropertyChangedEventHandler( OnAreaPropertyChanged );
        RubberBandXyZoomModifier rb = new RubberBandXyZoomModifier();
        rb.IsXAxisOnly = true;
        rb.ExecuteOn = ExecuteOn.MouseRightButton;
        rb.ReceiveHandledEvents = true;
        _rubberBandXyZoomModifier = rb;
    }

    private object GetRootElement() => ( object ) ParentViewModel ?? ( object ) this;

    public ObservableCollection<ChartCompentViewModel> LegendElements
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
        IChart chart = Chart;
        if ( chart == null )
            return;
        chart.IsAutoRange = false;
    }

    private void OnChartOrderMouseMove(
    ModifierMouseArgs _param1 )
    {
        IChart chart = Chart;
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

        var cursor = new UltrachartCursormodifier();
        cursor.ShowTooltip = true;
        cursor.ReceiveHandledEvents = true;
        //cursor.ShowTooltipOn = ShowTooltipOptions.MouseHover;
        //cursor.HoverDelay = 1000;

        var orderLines = new ChartOrderModifier(Area);
        orderLines.IsEnabled = true;
        //orderLines.CanCreateOrders = true;

        _dataPointSelector = new fxDataPointSelectionModifier();
        _dataPointSelector.ExecuteOn = ExecuteOn.MouseMiddleButton;
        _dataPointSelector.ReceiveHandledEvents = true;
        _dataPointSelector.XAxisId = "X";
        _dataPointSelector.IsEnabled = true;
        _dataPointSelector.AllowsMultiSelection = true;


        var zoomPan = new fxZoomPanModifier();
        zoomPan.ExecuteOn = ExecuteOn.MouseLeftButton;
        zoomPan.ReceiveHandledEvents = true;
        zoomPan.ClipModeX = ClipMode.None;
        //zoomPan.XyDirection = XyDirection.XYDirection;


        var xaxisDragModifier = new XAxisDragModifier();
        xaxisDragModifier.AxisId = "X";
        xaxisDragModifier.ClipModeX = ClipMode.None;

        var yaxisDragModifier = new YAxisDragModifier();
        yaxisDragModifier.AxisId = "Y";

        var bandXyZoomModifier = new RubberBandXyZoomModifier();
        bandXyZoomModifier.IsXAxisOnly = true;
        bandXyZoomModifier.ExecuteOn = ExecuteOn.MouseRightButton;
        bandXyZoomModifier.ReceiveHandledEvents = true;

        var zoomExtentsModifier = new ZoomExtentsModifier();
        zoomExtentsModifier.ExecuteOn = ExecuteOn.MouseDoubleClick;
        zoomExtentsModifier.XyDirection = XyDirection.YDirection;

        var seriesValueModifer = new SeriesValueModifier();

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

    public IEnumerable<Order> GetActiveOrders( Func<Order, bool> _param1 )
    {
        return _componentsCache.CachedValues.Where( p => p != null ).SelectMany(
                x =>
                {
                    return x.Elements.OfType<ChartActiveOrdersElementVM>().SelectMany( ao => ao.GetActiveOrders( _param1 ) );
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
            AnnotationModifier.SetBindings( FxAnnotationModifier.UserAnnotationTypeProperty, GroupChart, "AnnotationType" );

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

        AxisBase xBase = AxisBaseHelper.InitAndSetBinding(axis, this.ParentViewModel?.RemoveAxisCommand, this.ResetAxisTimeZoneCommand, this.Chart);
        xBase.PropertyChanged += OnAxisBasePropertyChanged;
        
        ( ( DispatcherObject ) Chart ).GuiAsync( () =>
        {
            if ( Chart == null )
                return;

            var ab = AxisBaseHelper.InitAndSetBinding(axis, this.ParentViewModel?.RemoveAxisCommand, this.ResetAxisTimeZoneCommand, this.Chart);
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

    private void OnTargetPropertyChanged308( object _param1, PropertyChangedEventArgs _param2 )
    {
        
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

    private bool RemoveAxis( IChartAxis _param1, ICollection<IAxis> _param2 )
    {
        DrawingSurfaceViewModel.SomeSealClass083523 jy0mx0yCuWlqEsh0ZdY = new DrawingSurfaceViewModel.SomeSealClass083523();
        jy0mx0yCuWlqEsh0ZdY._ICollection_IAxis_098 = _param2;
        jy0mx0yCuWlqEsh0ZdY._IChartAxis_098 = _param1;
        jy0mx0yCuWlqEsh0ZdY._variableSome3535 = this;
        FrameworkElement chart = (FrameworkElement) Chart;
        if ( chart != null )
            ( ( DispatcherObject ) chart ).GuiAsync( new Action( jy0mx0yCuWlqEsh0ZdY._SomeSealClass083523_Metho03 ) );
        return true;
    }

    private void OnTargetPropertyChanged308( object _param1, PropertyChangedEventArgs _param2 )
    {
        if ( !( _param1 is CategoryDateTimeAxis nu9622VfydaypdeqEjd ) || _param2.PropertyName != "CurrentDatapointPixelSize" )
            return;
        IChartCandleElement chartCandleElement = ((SynchronizedDictionary<IChartComponent, ChartCompentViewModel>) _componentsCache).Keys.OfType<IChartCandleElement>().FirstOrDefault<IChartCandleElement>();
        if ( chartCandleElement != null && chartCandleElement.XAxisId != nu9622VfydaypdeqEjd.Id )
            return;
        DataPointWidth = Math.Round( MathHelper.IsNaN( nu9622VfydaypdeqEjd.CurrentDatapointPixelSize ) ? 0.0 : nu9622VfydaypdeqEjd.CurrentDatapointPixelSize, 1, MidpointRounding.AwayFromZero );
    }

    public void SetScichartSurface( SciChartSurface _param1 )
    {
        if ( _sciChartSurface == _param1 )
            return;
        _sciChartSurface = _sciChartSurface == null || _param1 == null ? _param1 : throw new InvalidOperationException( "got unexpected chart surface" );
        _param1?.RenderSurface.Rendered( new EventHandler<RenderedEventArgs>( OnRenderSurfaceRendered ) );
    }

    public void Draw( ChartDrawData _param1 )
    {
        if ( _sciChartSurface != null )
        {
            using ( _sciChartSurface.SuspendUpdates() )
                Draw( _param1 );
        }
        else
            Draw( _param1 );
    }

    private void OnRenderSurfaceRendered(
      object _param1,
      RenderedEventArgs _param2 )
    {
        if ( _sciChartSurface == null )
            return;
        CollectionHelper.ForEach<CategoryDateTimeAxis>( XAxises.OfType<CategoryDateTimeAxis>(), DrawingSurfaceViewModel.SomeClass34343383.public_static_Action_CategoryDateTimeAxis_009 ?? ( DrawingSurfaceViewModel.SomeClass34343383.public_static_Action_CategoryDateTimeAxis_009 = new Action<CategoryDateTimeAxis>( DrawingSurfaceViewModel.SomeClass34343383.SomeMethond0343.SomeMEthod03853 ) ) );
        CollectionHelper.ForEach<ChartCompentViewModel>( ( IEnumerable<ChartCompentViewModel> ) ( ( SynchronizedDictionary<IChartComponent, ChartCompentViewModel> ) _componentsCache ).Values, DrawingSurfaceViewModel.SomeClass34343383.public_static_Action_ChartCompentViewModel_008 ?? ( DrawingSurfaceViewModel.SomeClass34343383.public_static_Action_ChartCompentViewModel_008 = new Action<ChartCompentViewModel>( DrawingSurfaceViewModel.SomeClass34343383.SomeMethond0343.SomeMEthod03854 ) ) );
        if ( !ShowPerfStats || _param2.\u0023\u003DzguiAuOeZYTXy() <= 0.0)
      return;
        double num = 1000.0 / _param2.\u0023\u003DzguiAuOeZYTXy();
        while ( _queue.Count >= 10 )
            _fpsTotal -= _queue.Dequeue();
        _queue.Enqueue( num );
        _fpsTotal += num;
        PerfStats = $"FPS: {_fpsTotal / ( double ) _queue.Count:0}   Count: {_sciChartSurface.RenderableSeries.Where<IRenderableSeries>( DrawingSurfaceViewModel.SomeClass34343383._public_static_Func_IRenderableSeries_bool_003 ?? ( DrawingSurfaceViewModel.SomeClass34343383._public_static_Func_IRenderableSeries_bool_003 = new Func<IRenderableSeries, bool>( DrawingSurfaceViewModel.SomeClass34343383.SomeMethond0343.SomeMEthod03855 ) ) ).Sum<IRenderableSeries>( DrawingSurfaceViewModel.SomeClass34343383.m_public_static_Func_IRenderableSeries__nt_ ?? ( DrawingSurfaceViewModel.SomeClass34343383.m_public_static_Func_IRenderableSeries__nt_ = new Func<IRenderableSeries, int>( DrawingSurfaceViewModel.SomeClass34343383.SomeMethond0343.public_int_Method_IRenderableSeries_ ) ) ):n0}";
    }

    private void Draw( ChartDrawData _param1 )
    {
        foreach ( ChartCompentViewModel a4VgOpCeDiqsTdzB in _componentsCache.GetComponents() )
        {
            if ( a4VgOpCeDiqsTdzB.Draw( _param1 ) )
                ( ( BaseCollection<ChartCompentViewModel, ISet<ChartCompentViewModel>> ) _parentChartViewModelCache ).Add( a4VgOpCeDiqsTdzB );
        }
    }

    private void OnTimer( object _param1, EventArgs _param2 )
    {
        ChartCompentViewModel[] a4VgOpCeDiqsTdzBArray;
        lock ( ( ( SynchronizedCollection<ChartCompentViewModel, ISet<ChartCompentViewModel>> ) _parentChartViewModelCache ).SyncRoot )
            a4VgOpCeDiqsTdzBArray = CollectionHelper.CopyAndClear<ChartCompentViewModel>( ( ICollection<ChartCompentViewModel> ) _parentChartViewModelCache );
        foreach ( ChartCompentViewModel a4VgOpCeDiqsTdzB in a4VgOpCeDiqsTdzBArray )
            a4VgOpCeDiqsTdzB.PerformPeriodicalAction();
    }

    public void Reset( IEnumerable<IChartElement> _param1 )
    {
        foreach ( IChartComponent ddznyiGmdRlAevOq in _param1 )
        {
            ChartCompentViewModel a4VgOpCeDiqsTdzB;
            if ( \u0023\u003DzKDbpj6zM462r( ddznyiGmdRlAevOq, out a4VgOpCeDiqsTdzB ))
      {
                ( ( BaseCollection<ChartCompentViewModel, ISet<ChartCompentViewModel>> ) _parentChartViewModelCache ).Add( a4VgOpCeDiqsTdzB );
                a4VgOpCeDiqsTdzB.Reset();
            }
        }
    }

    private void AssignOrphanedChildElementsWithParent()
    {
        foreach ( IChartComponent ddznyiGmdRlAevOq in ( ( IEnumerable<KeyValuePair<IChartComponent, ChartCompentViewModel>> ) _componentsCache ).Where<KeyValuePair<IChartComponent, ChartCompentViewModel>>( DrawingSurfaceViewModel.SomeClass34343383.public_static_Func_KeyValuePair_IChartComponent_ChartCompentViewModel_bool_ ?? ( DrawingSurfaceViewModel.SomeClass34343383.public_static_Func_KeyValuePair_IChartComponent_ChartCompentViewModel_bool_ = new Func<KeyValuePair<IChartComponent, ChartCompentViewModel>, bool>( DrawingSurfaceViewModel.SomeClass34343383.SomeMethond0343.public_bool_Method_KeyValuePair_IChartComponent_ChartCompentViewModel ) ) ).Select<KeyValuePair<IChartComponent, ChartCompentViewModel>, IChartComponent>( DrawingSurfaceViewModel.SomeClass34343383.public_static_Func_KeyValuePair_IChartComponent_ChartCompentViewModel_IChartComponent_ ?? ( DrawingSurfaceViewModel.SomeClass34343383.public_static_Func_KeyValuePair_IChartComponent_ChartCompentViewModel_IChartComponent_ = new Func<KeyValuePair<IChartComponent, ChartCompentViewModel>, IChartComponent>( DrawingSurfaceViewModel.SomeClass34343383.SomeMethond0343.public_bool_Method_KeyValuePair_IChartComponent_ChartCompentViewModel_033 ) ) ).ToArray<IChartComponent>() )
        {
            ChartCompentViewModel a4VgOpCeDiqsTdzB = new ChartCompentViewModel(this, ddznyiGmdRlAevOq);
            a4VgOpCeDiqsTdzB.InitializeChildElements( CollectionHelper.Append2<IChartElement>( ddznyiGmdRlAevOq.ChildElements, ( IChartElement ) ddznyiGmdRlAevOq ).OfType<IDrawableChartElement>().Where<IDrawableChartElement>( DrawingSurfaceViewModel.SomeClass34343383.Func_IDrawableChartElement_bool_098 ?? ( DrawingSurfaceViewModel.SomeClass34343383.Func_IDrawableChartElement_bool_098 = new Func<IDrawableChartElement, bool>( DrawingSurfaceViewModel.SomeClass34343383.SomeMethond0343.public_bool_Method_KeyValuePair_IChartComponent_ChartCompentViewModel_0352 ) ) ).Select<IDrawableChartElement, DrawableChartElementBaseViewModel>( new Func<IDrawableChartElement, DrawableChartElementBaseViewModel>( CreateDrawableChartElementBaseViewModel ) ).Where<DrawableChartElementBaseViewModel>( DrawingSurfaceViewModel.SomeClass34343383.__Func_DrawableChartElementBaseViewModel_bool_003 ?? ( DrawingSurfaceViewModel.SomeClass34343383.__Func_DrawableChartElementBaseViewModel_bool_003 = new Func<DrawableChartElementBaseViewModel, bool>( DrawingSurfaceViewModel.SomeClass34343383.SomeMethond0343.public_bool_Method_KeyValuePair_IChartComponent_ChartCompentViewModel_4353 ) ) ) );
            ( ( SynchronizedDictionary<IChartComponent, ChartCompentViewModel> ) _componentsCache )[ ddznyiGmdRlAevOq ] = a4VgOpCeDiqsTdzB;
            if ( ddznyiGmdRlAevOq.IsLegend )
                LegendElements.Add( a4VgOpCeDiqsTdzB );
        }
        UpdateSomeUI();
    }

    private void UpdateSomeUI()
    {
        CandlesCompositeElement = ( ( IEnumerable<KeyValuePair<IChartComponent, ChartCompentViewModel>> ) _componentsCache ).FirstOrDefault<KeyValuePair<IChartComponent, ChartCompentViewModel>>( DrawingSurfaceViewModel.SomeClass34343383._Func_KeyValuePair_IChartComponent_ChartCompentViewModel__bool_ ?? ( DrawingSurfaceViewModel.SomeClass34343383._Func_KeyValuePair_IChartComponent_ChartCompentViewModel__bool_ = new Func<KeyValuePair<IChartComponent, ChartCompentViewModel>, bool>( DrawingSurfaceViewModel.SomeClass34343383.SomeMethond0343.public_bool_Method_0983 ) ) ).Value;
        PaneHasCandles = CandlesCompositeElement != null;
    }

    public bool \u0023\u003DzKDbpj6zM462r(
      IChartComponent _param1,
      out ChartCompentViewModel _param2 )
    {
        return ( ( SynchronizedDictionary<IChartComponent, ChartCompentViewModel> ) _componentsCache ).TryGetValue( _param1, ref _param2 );
    }

    public void OnChartAreaElementsAdded( IChartElement _param1 )
    {
        IChart chart = Chart;
        if ( chart != null )
            chart.EnsureUIThread();
        IChartComponent ddznyiGmdRlAevOq = (IChartComponent) _param1;
        ddznyiGmdRlAevOq.AddAxisesAndEventHandler( Area );
        if ( ( ( SynchronizedDictionary<IChartComponent, ChartCompentViewModel> ) _componentsCache ).ContainsKey( ddznyiGmdRlAevOq ) )
            throw new ArgumentException( "duplicate chart element", "element" );
        if ( Chart != null )
        {
            ChartCompentViewModel a4VgOpCeDiqsTdzB = new ChartCompentViewModel(this, ddznyiGmdRlAevOq);
            a4VgOpCeDiqsTdzB.InitializeChildElements( CollectionHelper.Append2<IChartElement>( ddznyiGmdRlAevOq.ChildElements, ( IChartElement ) ddznyiGmdRlAevOq ).OfType<IDrawableChartElement>().Where<IDrawableChartElement>( DrawingSurfaceViewModel.SomeClass34343383.__Func_IDrawableChartElement__bool__903 ?? ( DrawingSurfaceViewModel.SomeClass34343383.__Func_IDrawableChartElement__bool__903 = new Func<IDrawableChartElement, bool>( DrawingSurfaceViewModel.SomeClass34343383.SomeMethond0343.public_bool_Method_0983333 ) ) ).Select<IDrawableChartElement, DrawableChartElementBaseViewModel>( new Func<IDrawableChartElement, DrawableChartElementBaseViewModel>( NewDrawableChartElementBaseViewModel ) ).Where<DrawableChartElementBaseViewModel>( DrawingSurfaceViewModel.SomeClass34343383.__Func_DrawableChartElementBaseViewModel__bool__003 ?? ( DrawingSurfaceViewModel.SomeClass34343383.__Func_DrawableChartElementBaseViewModel__bool__003 = new Func<DrawableChartElementBaseViewModel, bool>( DrawingSurfaceViewModel.SomeClass34343383.SomeMethond0343.public_bool_Method_5498751 ) ) ) );
            ( ( SynchronizedDictionary<IChartComponent, ChartCompentViewModel> ) _componentsCache )[ ddznyiGmdRlAevOq ] = a4VgOpCeDiqsTdzB;
            if ( ddznyiGmdRlAevOq.IsLegend )
                LegendElements.Add( a4VgOpCeDiqsTdzB );
        }
        else
            ( ( SynchronizedDictionary<IChartComponent, ChartCompentViewModel> ) _componentsCache )[ ddznyiGmdRlAevOq ] = ( ChartCompentViewModel ) null;
        if ( _modifierGroup != null && ddznyiGmdRlAevOq is IChartCandleElement chartCandleElement )
        {
            OnDrawStylePropertyChanged( chartCandleElement );
            chartCandleElement.PropertyChanged += new PropertyChangedEventHandler( Candle_PropertyChanged );
        }
        UpdateSomeUI();
        ddznyiGmdRlAevOq.PropertyChanged += new PropertyChangedEventHandler( OnXYAxisPropertyChanged );
    }

    public bool OnChartAreaElementsRemoving( IChartElement _param1 )
    {
        IChart chart = Chart;
        if ( chart != null )
            chart.EnsureUIThread();
        IChartComponent ddznyiGmdRlAevOq = (IChartComponent) _param1;
        ChartCompentViewModel a4VgOpCeDiqsTdzB;
        if ( !\u0023\u003DzKDbpj6zM462r( ddznyiGmdRlAevOq, out a4VgOpCeDiqsTdzB ))
      return false;
        ddznyiGmdRlAevOq.RemoveAxisesEventHandler();
        if ( ddznyiGmdRlAevOq is IChartCandleElement chartCandleElement )
            chartCandleElement.PropertyChanged -= new PropertyChangedEventHandler( Candle_PropertyChanged );
        ddznyiGmdRlAevOq.PropertyChanged -= new PropertyChangedEventHandler( OnXYAxisPropertyChanged );
        ( ( SynchronizedDictionary<IChartComponent, ChartCompentViewModel> ) _componentsCache ).Remove( ddznyiGmdRlAevOq );
        if ( a4VgOpCeDiqsTdzB != null )
        {
            a4VgOpCeDiqsTdzB.GuiUpdateAndClear();
            a4VgOpCeDiqsTdzB.Dispose();
            LegendElements.Remove( a4VgOpCeDiqsTdzB );
        }
        UpdateSomeUI();
        return true;
    }

    private void OnXYAxisPropertyChanged( object _param1, PropertyChangedEventArgs _param2 )
    {
        IChartComponent ddznyiGmdRlAevOq = (IChartComponent) _param1;
        if ( _param2.PropertyName != "XAxisId" && _param2.PropertyName != "YAxisId" )
            return;
        IChart chart = Chart;
        if ( chart != null )
            chart.EnsureUIThread();
        List<IRenderableSeries> koh9jO5RuUcFiAqLcList;
        if ( _chartUIRSeries.TryGetValue( ddznyiGmdRlAevOq, out koh9jO5RuUcFiAqLcList ) )
        {
            if ( ddznyiGmdRlAevOq.TryGetXAxis() != null && ddznyiGmdRlAevOq.TryGetYAxis() != null )
            {
                foreach ( IRenderableSeries koh9jO5RuUcFiAqLc in koh9jO5RuUcFiAqLcList )
                {
                    if ( !_advanceChartRenderableSeries.Contains( koh9jO5RuUcFiAqLc ) )
                        _advanceChartRenderableSeries.Add( koh9jO5RuUcFiAqLc );
                }
            }
            else
            {
                foreach ( IRenderableSeries koh9jO5RuUcFiAqLc in koh9jO5RuUcFiAqLcList )
                    _advanceChartRenderableSeries.Remove( koh9jO5RuUcFiAqLc );
            }
        }
        Dictionary<object, IAnnotation> dictionary;
        if ( !_topChartElmentAnnotationMap.TryGetValue( ddznyiGmdRlAevOq, out dictionary ) )
            return;
        if ( ddznyiGmdRlAevOq.TryGetXAxis() != null && ddznyiGmdRlAevOq.TryGetYAxis() != null )
        {
            foreach ( KeyValuePair<object, IAnnotation> keyValuePair in dictionary )
            {
                if ( !Annotations.Contains( keyValuePair.Value ) )
                    Annotations.Add( keyValuePair.Value );
            }
        }
        else
        {
            foreach ( KeyValuePair<object, IAnnotation> keyValuePair in dictionary )
                Annotations.Remove( keyValuePair.Value );
        }
    }

    private void Candle_PropertyChanged(
      object _param1,
      PropertyChangedEventArgs _param2 )
    {
        if ( !( _param2.PropertyName == "DrawStyle" ) )
            return;
        OnDrawStylePropertyChanged( ( IChartCandleElement ) _param1 );
    }

    private void OnDrawStylePropertyChanged( IChartCandleElement _param1 )
    {
        _rubberBandXyZoomModifier.IsXAxisOnly = !_param1.DrawStyle.IsVolumeProfileChart();
    }

    public VisibleRangeDpo GetVisibleRangeDp(
      string _param1 )
    {
        IChartAxis chartAxis = ((IEnumerable<IChartAxis>) Area.XAxises).FirstOrDefault<IChartAxis>(new Func<IChartAxis, bool>(new DrawingSurfaceViewModel.SomeSealClass0833352()
        {
            _someString0382 = _param1
        }.bool_Method02_IChartAxis_));
        return chartAxis == null ? ( VisibleRangeDpo ) null : VisibleRangeDpo.AddRangeProperty( GetRootElement(), chartAxis.Group, PaneGroupSuffix, chartAxis.AxisType );
    }

    private void OnShowHiddenAxes()
    {
        CollectionHelper.ForEach<IChartAxis>( ( IEnumerable<IChartAxis> ) Area.XAxises, DrawingSurfaceViewModel.SomeClass34343383._Action_IChartAxis_0932 ?? ( DrawingSurfaceViewModel.SomeClass34343383._Action_IChartAxis_0932 = new Action<IChartAxis>( DrawingSurfaceViewModel.SomeClass34343383.SomeMethond0343.public_bool_Method_303403 ) ) );
        CollectionHelper.ForEach<IChartAxis>( ( IEnumerable<IChartAxis> ) Area.YAxises, DrawingSurfaceViewModel.SomeClass34343383._Action_IChartAxis_0932323 ?? ( DrawingSurfaceViewModel.SomeClass34343383._Action_IChartAxis_0932323 = new Action<IChartAxis>( DrawingSurfaceViewModel.SomeClass34343383.SomeMethond0343.public_bool_Method_938745 ) ) );
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
            objArray[ 0 ] = ( object ) ( GroupChart ?? throw new NotSupportedException() ).GetInstanceCount();
            objArray[ 1 ] = ( object ) _paneGroupSuffix;
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
                LegendModifier = new LegendModifier()
            };
            _legendViewModel.RemoveElementEvent( new Action<IChartElement>( OnRemoveElementEvent ) );
            return _legendViewModel;
        }
    }

    public FxAnnotationModifier AnnotationModifier
    {
        get
        {
            FxAnnotationModifier zIfS1UpijEycx = _annotationModifier;
            if ( zIfS1UpijEycx != null )
                return zIfS1UpijEycx;
            FxAnnotationModifier g7AaupM52GhwgqEjd = new FxAnnotationModifier(Area, Annotations);
            g7AaupM52GhwgqEjd.IsEnabled = false;
            FxAnnotationModifier annotationModifier = g7AaupM52GhwgqEjd;
            _annotationModifier = g7AaupM52GhwgqEjd;
            return annotationModifier;
        }
    }

    public IEnumerable<IRenderableSeries> ChartSeriesViewModels
    {
        get
        {
            return ( IEnumerable<IRenderableSeries> ) _advanceChartRenderableSeries;
        }
    }

    public void AddAxisMakerAnnotation(
      IChartComponent _param1,
      IAnnotation _param2,
      object _param3 )
    {
        if ( _param3 == null )
            throw new ArgumentNullException( "key" );
        Dictionary<object, IAnnotation> dictionary;
        if ( !_topChartElmentAnnotationMap.TryGetValue( _param1, out dictionary ) )
            _topChartElmentAnnotationMap[ _param1 ] = dictionary = new Dictionary<object, IAnnotation>();
        dictionary.Add( _param3, _param2 );
        if ( _param1.TryGetXAxis() == null || _param1.TryGetYAxis() == null )
            return;
        Annotations.Add( _param2 );
    }

    public IAnnotation GetAxisMakerAnnotation(
      IChartComponent _param1,
      object _param2 )
    {
        Dictionary<object, IAnnotation> dictionary;
        return !_topChartElmentAnnotationMap.TryGetValue( _param1, out dictionary ) ? ( IAnnotation ) null : CollectionHelper.TryGetValue<object, IAnnotation>( ( IDictionary<object, IAnnotation> ) dictionary, _param2 );
    }

    public void RemoveAnnotation(
      IChartComponent _param1,
      object _param2 )
    {
        Dictionary<object, IAnnotation> dictionary;
        if ( !_topChartElmentAnnotationMap.TryGetValue( _param1, out dictionary ) )
            return;
        if ( _param2 == null )
        {
            foreach ( KeyValuePair<object, IAnnotation> keyValuePair in dictionary )
                Annotations.Remove( keyValuePair.Value );
            _topChartElmentAnnotationMap.Remove( _param1 );
        }
        else
        {
            IAnnotation hhh93Q0DqkV5Sv90k;
            if ( !dictionary.TryGetValue( _param2, out hhh93Q0DqkV5Sv90k ) )
                return;
            Annotations.Remove( hhh93Q0DqkV5Sv90k );
            dictionary.Remove( _param2 );
        }
    }

    public void AddSeriesViewModelsToRoot(
      IChartComponent _param1,
      IRenderableSeries _param2 )
    {
        List<IRenderableSeries> koh9jO5RuUcFiAqLcList;
        if ( !_chartUIRSeries.TryGetValue( _param1, out koh9jO5RuUcFiAqLcList ) )
            _chartUIRSeries[ _param1 ] = koh9jO5RuUcFiAqLcList = new List<IRenderableSeries>();
        if ( !koh9jO5RuUcFiAqLcList.Contains( _param2 ) )
            koh9jO5RuUcFiAqLcList.Add( _param2 );
        OnXYAxisPropertyChanged( ( object ) _param1, new PropertyChangedEventArgs( "XAxisId" ) );
    }

    public void RemoveChartComponent(
      IChartComponent _param1 )
    {
        List<IRenderableSeries> koh9jO5RuUcFiAqLcList;
        if ( !_chartUIRSeries.TryGetValue( _param1, out koh9jO5RuUcFiAqLcList ) )
            return;
        foreach ( IRenderableSeries koh9jO5RuUcFiAqLc in koh9jO5RuUcFiAqLcList )
            _advanceChartRenderableSeries.Remove( koh9jO5RuUcFiAqLc );
        _chartUIRSeries.Remove( _param1 );
    }

    public void Refresh()
    {
        _sciChartSurface?.InvalidateElement();
    }

    public AxisCollection XAxises
    {
        get => _xAxisNotifyList;
    }

    public AxisCollection YAxises
    {
        get => _yAxisNotifyList;
    }

    public ChartCompentViewModel CandlesCompositeElement
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
            Area.Height = value;
        }
    }

    public bool ShowPerfStats
    {
        get
        {
            StockSharp.Xaml.Charting.Chart groupChart = GroupChart;
            return groupChart == null ? _showPerfStats : __nonvirtual( groupChart.ShowPerfStats );
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

    void IDrawingSurfaceVM.ZoomExtents()
    {
        throw new NotSupportedException();
    }

    private void ChangeApplicationTheme() => SelectedTheme = ChartHelper.CurrChartTheme();

    public void Dispose()
    {
        _sciChartSurface?.Dispose();
        _sciChartSurface = ( SciChartSurface ) null;
    }

    public bool AllowElementToBeRemoved( ChartCompentViewModel _param1 )
    {
        if ( ParentViewModel == null || !ParentViewModel.IsInteracted )
            return false;
        bool flag;
        switch ( _param1.ChartComponent )
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

    private DrawableChartElementBaseViewModel CreateDrawableChartElementBaseViewModel(
      IDrawableChartElement _param1 )
    {
        return _param1.CreateViewModel( this );
    }

    private DrawableChartElementBaseViewModel NewDrawableChartElementBaseViewModel(
      IDrawableChartElement _param1 )
    {
        return _param1.CreateViewModel( this );
    }

    private void OnRemoveElementEvent( IChartElement _param1 )
    {
        ParentViewModel?.\u0023\u003DzzXq5ccDMuPZc( _param1 );
        Action<IChartElement> zeBeQvx4 = RemoveElementEvent;
        if ( zeBeQvx4 == null )
            return;
        zeBeQvx4( _param1 );
    }

    private sealed class PrivateSealedClass0835
    {
        public Func<Order, bool> m_public_Func_Order_bool_;
        public Func<ChartActiveOrdersElementVM, IEnumerable<Order>> Func_ChartActiveOrdersElementVM_0938;

        public IEnumerable<Order> public_IEnumerable_Order_098( ChartCompentViewModel x )
        {
            return x.Elements.OfType<ChartActiveOrdersElementVM>().SelectMany<ChartActiveOrdersElementVM, Order>( Func_ChartActiveOrdersElementVM_0938 ?? ( Func_ChartActiveOrdersElementVM_0938 = new Func<ChartActiveOrdersElementVM, IEnumerable<Order>>( public_IEnumerable_Order__ ) ) );
        }

        public IEnumerable<Order> public_IEnumerable_Order__( ChartActiveOrdersElementVM _param1 )
        {
            return _param1.GetActiveOrders( m_public_Func_Order_bool_ );
        }
    }

    private sealed class PrivateSealedClass_3Ins_1Met
    {
        public DrawingSurfaceViewModel _variableSome3535;
        public IChartAxis _IChartAxis_098;
        public ICollection<IAxis> _ICollection_IAxis_098;

        //public void OnGuiAsyncDoStuff()
        //{
        //    if ( _variableSome3535.Chart == null )
        //        return;
        //    AxisBase axF9ZgQ7NbH9KsEjd = _IChartAxis_098.InitAndSetBinding(_variableSome3535.ParentViewModel?.RemoveAxisCommand, _variableSome3535.ResetAxisTimeZoneCommand, _variableSome3535.Chart);
        //    axF9ZgQ7NbH9KsEjd.PropertyChanged += new PropertyChangedEventHandler( _variableSome3535.OnTargetPropertyChanged308 );
        //    _ICollection_IAxis_098.Add( ( IAxis ) axF9ZgQ7NbH9KsEjd );
        //    if ( _ICollection_IAxis_098 != _variableSome3535.XAxises )
        //        return;
        //    _variableSome3535.SetupAxisBindings();
        //}
    }

    [Serializable]
    private new sealed class SomeClass34343383
    {
        public static readonly DrawingSurfaceViewModel.SomeClass34343383 SomeMethond0343 = new DrawingSurfaceViewModel.SomeClass34343383();
        public static Action<ChartAxis> pubStatic_Action_ChartAxis_;
        public static Func<ChartCompentViewModel, bool> m_public_static_Func_ChartCompentViewModel_bool_;
        public static Action<CategoryDateTimeAxis> public_static_Action_CategoryDateTimeAxis_009;
        public static Action<ChartCompentViewModel> public_static_Action_ChartCompentViewModel_008;
        public static Func<IRenderableSeries, bool> _public_static_Func_IRenderableSeries_bool_003;
        public static Func<IRenderableSeries, int> m_public_static_Func_IRenderableSeries__nt_;
        public static Func<KeyValuePair<IChartComponent, ChartCompentViewModel>, bool> public_static_Func_KeyValuePair_IChartComponent_ChartCompentViewModel_bool_;
        public static Func<KeyValuePair<IChartComponent, ChartCompentViewModel>, IChartComponent> public_static_Func_KeyValuePair_IChartComponent_ChartCompentViewModel_IChartComponent_;
        public static Func<IDrawableChartElement, bool> Func_IDrawableChartElement_bool_098;
        public static Func<DrawableChartElementBaseViewModel, bool> __Func_DrawableChartElementBaseViewModel_bool_003;
        public static Func<KeyValuePair<IChartComponent, ChartCompentViewModel>, bool> _Func_KeyValuePair_IChartComponent_ChartCompentViewModel__bool_;
        public static Func<IDrawableChartElement, bool> __Func_IDrawableChartElement__bool__903;
        public static Func<DrawableChartElementBaseViewModel, bool> __Func_DrawableChartElementBaseViewModel__bool__003;
        public static Action<IChartAxis> _Action_IChartAxis_0932;
        public static Action<IChartAxis> _Action_IChartAxis_0932323;



        public bool SomeMEthod03852(
          ChartCompentViewModel _param1 )
        {
            return _param1 != null;
        }

        public void SomeMEthod03853(
          CategoryDateTimeAxis _param1 )
        {
            if ( !( _param1.Tag is ChartAxis tag ) )
                return;
            tag.DataPointWidth = _param1.CurrentDatapointPixelSize;
        }

        public void SomeMEthod03854(
          ChartCompentViewModel _param1 )
        {
            _param1.UpdateYAxisMarker();
        }

        public bool SomeMEthod03855(
          IRenderableSeries _param1 )
        {
            return _param1.IsVisible;
        }

        public int public_int_Method_IRenderableSeries_(
          IRenderableSeries _param1 )
        {
            var dataSeries = _param1.get_DataSeries();
            return dataSeries == null ? 0 : dataSeries.get_Count();
        }

        public bool public_bool_Method_KeyValuePair_IChartComponent_ChartCompentViewModel(
          KeyValuePair<IChartComponent, ChartCompentViewModel> _param1 )
        {
            return _param1.Value == null;
        }

        public IChartComponent public_bool_Method_KeyValuePair_IChartComponent_ChartCompentViewModel_033(
          KeyValuePair<IChartComponent, ChartCompentViewModel> _param1 )
        {
            return _param1.Key;
        }

        public bool public_bool_Method_KeyValuePair_IChartComponent_ChartCompentViewModel_0352(
          IDrawableChartElement _param1 )
        {
            return !_param1.DontDraw;
        }

        public bool public_bool_Method_KeyValuePair_IChartComponent_ChartCompentViewModel_4353(
          DrawableChartElementBaseViewModel _param1 )
        {
            return _param1 != null;
        }

        public bool public_bool_Method_0983(
          KeyValuePair<IChartComponent, ChartCompentViewModel> _param1 )
        {
            return _param1.Key is IChartCandleElement;
        }

        public bool public_bool_Method_0983333(
          IDrawableChartElement _param1 )
        {
            return !_param1.DontDraw;
        }

        public bool public_bool_Method_5498751(
          DrawableChartElementBaseViewModel _param1 )
        {
            return _param1 != null;
        }

        public void public_bool_Method_303403( IChartAxis _param1 )
        {
            _param1.IsVisible = true;
        }

        public void public_bool_Method_938745( IChartAxis _param1 )
        {
            _param1.IsVisible = true;
        }
    }

    private sealed class SomeSealClass0833352
    {
        public string _someString0382;

        public bool bool_Method02_IChartAxis_( IChartAxis _param1 )
        {
            return _param1.Id == _someString0382;
        }
    }



    private sealed class SomeSealClass083523
    {
        public ICollection<IAxis> _ICollection_IAxis_098;
        public IChartAxis _IChartAxis_098;
        public DrawingSurfaceViewModel _variableSome3535;
        public Func<IAxis, bool> _Func_IAxis_bool_0835;

        public void _SomeSealClass083523_Metho03()
        {
            AxisBase target = (AxisBase) _ICollection_IAxis_098.FirstOrDefault<IAxis>(_Func_IAxis_bool_0835 ?? (_Func_IAxis_bool_0835 = new Func<IAxis, bool>(_function_IAxis__R__void__001)));
            if ( target == null )
                return;
            target.PropertyChanged -= new PropertyChangedEventHandler( _variableSome3535.OnTargetPropertyChanged308 );
            BindingOperations.ClearAllBindings( ( DependencyObject ) target );
            _ICollection_IAxis_098.Remove( ( IAxis ) target );
        }

        public bool _function_IAxis__R__void__001(
          IAxis _param1 )
        {
            return _param1.Id == _IChartAxis_098.Id;
        }
    }

    private sealed class SomeClass6409
    {
        public DrawingSurfaceViewModel _variableSome3535;
        public ChartArea _chartArea_093;
        public Action<IChartElement> _action_IChartElement_023;

        public bool OnChartAreaElementsRemovingAt( int _param1 )
        {
            return _variableSome3535.OnChartAreaElementsRemoving( ( ( IList<IChartElement> ) _chartArea_093.Elements )[ _param1 ] );
        }

        public bool OnChartAreaElementsClearing()
        {
            CollectionHelper.ForEach<IChartElement>( ( IEnumerable<IChartElement> ) _chartArea_093.Elements, _action_IChartElement_023 ?? ( _action_IChartElement_023 = new Action<IChartElement>( _function1_IChartElement____void__001 ) ) );
            return true;
        }

        public void _function1_IChartElement____void__001( IChartElement _param1 )
        {
            _variableSome3535.OnChartAreaElementsRemoving( _param1 );
        }

        public void OnAreaXAxisesAdded( IChartAxis _param1 )
        {
            _variableSome3535.AddAxis( _param1, ( ICollection<IAxis> ) _variableSome3535.XAxises );
        }

        public bool OnAreaXAxisesRemoving( IChartAxis _param1 )
        {
            return _variableSome3535.RemoveAxis( _param1, ( ICollection<IAxis> ) _variableSome3535.XAxises );
        }

        public bool OnAreaXAxisesRemovingAt( int _param1 )
        {
            return _variableSome3535.RemoveAxis( ( ( IList<IChartAxis> ) _chartArea_093.XAxises )[ _param1 ], ( ICollection<IAxis> ) _variableSome3535.XAxises );
        }

        public void OnAreaYAxisesAdded( IChartAxis _param1 )
        {
            _variableSome3535.AddAxis( _param1, ( ICollection<IAxis> ) _variableSome3535.YAxises );
        }

        public bool OnAreaYAxisesRemoving( IChartAxis _param1 )
        {
            return _variableSome3535.RemoveAxis( _param1, ( ICollection<IAxis> ) _variableSome3535.YAxises );
        }

        public bool OnAreaYAxisesRemovingAt( int _param1 )
        {
            return _variableSome3535.RemoveAxis( ( ( IList<IChartAxis> ) _chartArea_093.YAxises )[ _param1 ], ( ICollection<IAxis> ) _variableSome3535.YAxises );
        }

        public void OnApplicationThemeChanged(
          DependencyObject _param1,
          ThemeChangedRoutedEventArgs _param2 )
        {
            _variableSome3535.ChangeApplicationTheme();
        }
    }
}
