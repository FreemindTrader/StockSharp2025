using DevExpress.Xpf.Core;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using Ecng.Xaml.Converters;
using StockSharp.Charting;
using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Common.Helpers;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.Annotations;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using DevExpress.Charts.Model;
using StockSharp.Xaml.Charting.Visuals;

namespace StockSharp.Xaml.Charting;

#nullable enable
public sealed class ScichartSurfaceMVVM : ChartBaseViewModel,
                                              IDisposable,
                                              IScichartSurfaceVM
{
    private sealed class ChartComponentsCache : CachedSynchronizedDictionary<IChartComponent, ChartComponentViewModel>
    {

        private ChartComponentViewModel[ ]? _componentViewModels =  null;

        public ChartComponentViewModel[ ] InitCache()
        {
            lock ( ( ( SynchronizedDictionary<IChartComponent, ChartComponentViewModel> ) this ).SyncRoot )
            {
                if ( _componentViewModels == null )
                {
                    var holders = new List<ChartComponentViewModel>();
                    holders.AddRange( ( ( IEnumerable<KeyValuePair<IChartComponent, ChartComponentViewModel>> ) this ).OrderBy( p => p.Key.Priority ).Select( x => x.Value ) );
                    _componentViewModels = holders.ToArray();
                }

                return _componentViewModels;
            }
        }

        protected override void OnResetCache( bool reset )
        {
            base.OnResetCache( reset );
            this._componentViewModels = null;
        }
    }

    private readonly ScichartSurfaceMVVM.ChartComponentsCache _componentsCache;

    private readonly Dictionary<IChartComponent, List<IRenderableSeries>> _chartUIRSeries = new Dictionary<IChartComponent, List<IRenderableSeries>>();

    private readonly Dictionary<IChartComponent, Dictionary<object, IAnnotation>> _topChartElmentAnnotationMap = new Dictionary<IChartComponent, Dictionary<object, IAnnotation>>();

    private SciChartSurface _sciChartSurface;

    private bool _doneInitialization;

    private readonly List<ChartModifierBase> _chartModifiers = new List<ChartModifierBase>();

    private readonly RubberBandXyZoomModifier _rubberBandXyZoomModifier;

    private readonly DispatcherTimer _dispatcherTimer;

    private readonly SynchronizedSet<ChartComponentViewModel> _parentChartViewModelCache = new SynchronizedSet<ChartComponentViewModel>();

    private readonly Queue<double> _queue = new Queue<double>();

    private double _fpsTotal;

    private readonly ObservableCollection<ChartComponentViewModel> _legendElements = new ObservableCollection<ChartComponentViewModel>();

    public event Action<IChartElement> RemoveElementEvent;

    private string _paneGroupSuffix;

    private readonly ChartArea _chartArea;

    private LegendModifierVM _legendViewModel;

    private FxAnnotationModifier _annotationModifier;

    private readonly ObservableCollection<IRenderableSeries> _advanceChartRenderableSeries = new ObservableCollection<IRenderableSeries>();

    private readonly AxisCollection _xAxises = new AxisCollection();

    private readonly AxisCollection _yAxises = new AxisCollection();

    private bool _paneHasCandles;

    private ChartComponentViewModel _candleCompositeElement;

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

    public ScichartSurfaceMVVM( ChartArea area ) : base()
    {
        ScichartSurfaceMVVM.SomeClass6409 _someInstance08383 = new ScichartSurfaceMVVM.SomeClass6409();


        _someInstance08383._variableSome3535 = this;
        this._chartArea = area ?? throw new ArgumentNullException( "area" );
        this._dispatcherTimer = new DispatcherTimer( ( DispatcherPriority ) 7, Application.Current.Dispatcher );
        this._dispatcherTimer.Tick += new EventHandler( this.OnTimer );
        this._showHiddenAxesCommand = ( ICommand ) new DelegateCommand( new Action( this.OnShowHiddenAxes ) );
        ResetAxisTimeZoneCommand = new ActionCommand<ChartAxis>( a => a.TimeZone = null );
        this.Height = area.Height;
        area.Elements.Added += new Action<IChartElement>( this.OnChartAreaElementsAdded );
        area.Elements.Removing += new Func<IChartElement, bool>( this.OnChartAreaElementsRemoving );
        area.Elements.RemovingAt += new Func<int, bool>( _someInstance08383.OnChartAreaElementsRemovingAt );
        area.Elements.Clearing += new Func<bool>( _someInstance08383.OnChartAreaElementsClearing );
        area.XAxises.Added += new Action<IChartAxis>( _someInstance08383.OnAreaXAxisesAdded );
        area.XAxises.Removing += new Func<IChartAxis, bool>( _someInstance08383.OnAreaXAxisesRemoving );
        area.XAxises.RemovingAt += new Func<int, bool>( _someInstance08383.OnArea.XAxisesRemovingAt );
        area.YAxises.Added += new Action<IChartAxis>( _someInstance08383.OnAreaYAxisesAdded );
        area.YAxises.Removing += new Func<IChartAxis, bool>( _someInstance08383.OnAreaYAxisesRemoving );
        area.YAxises.RemovingAt += new Func<int, bool>( _someInstance08383.OnAreaYAxisesRemovingAt );
        ThemeManager.ApplicationThemeChanged += new ThemeChangedRoutedEventHandler( _someInstance08383.OnApplicationThemeChanged );
        this.ChangeApplicationTheme();
        CollectionHelper.ForEach<IChartElement>( ( IEnumerable<IChartElement> ) this.Area.Elements, new Action<IChartElement>( this.OnChartAreaElementsAdded ) );
        this.Area.PropertyChanged += new PropertyChangedEventHandler( this.OnAreaPropertyChanged );
        RubberBandXyZoomModifier rb = new RubberBandXyZoomModifier();
        rb.IsXAxisOnly = true;
        rb.ExecuteOn = ExecuteOn.MouseRightButton;
        rb.ReceiveHandledEvents = true;
        this._rubberBandXyZoomModifier = rb;
    }

    private object GetRootElement() => ( object ) this.ParentViewModel ?? ( object ) this;

    public ObservableCollection<ChartComponentViewModel> LegendElements
    {
        get => this._legendElements;
    }



    private void OnAreaPropertyChanged( object obj, PropertyChangedEventArgs e )
    {
        if ( e.PropertyName == "Height" )
        {
            this.Height = this.Area.Height;
        }

        if ( e.PropertyName != "Title" )
        {
            return;
        }

        this.NotifyChanged( "Title" );
    }

    private void OnChartOrderMouseEvent( ModifierMouseArgs _param1 )
    {
        IChart chart = this.Chart;
        if ( chart == null )
            return;
        chart.IsAutoRange = false;
    }

    private void OnChartOrderMouseMove(
    ModifierMouseArgs _param1 )
    {
        IChart chart = this.Chart;
        if ( chart == null )
            return;
        chart.IsAutoRange = false;
    }

    public IChart Chart => this.Area.Chart;

    public ChartArea Area => this._chartArea;

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
                this.RestartTimer();
                this.NotifyChanged( e.PropertyName );
                break;
            case "ShowPerfStats":
                this.NotifyChanged( e.PropertyName );
                break;
        }
    }

    public IEnumerable<Order> GetActiveOrders( Func<Order, bool> _param1 )
    {
        return ( ( IEnumerable<ChartComponentViewModel> ) this._componentsCache.CachedValues ).Where<ChartComponentViewModel>( ScichartSurfaceMVVM.SomeClass34343383.m_public_static_Func_ChartComponentViewModel_bool_ ?? ( ScichartSurfaceMVVM.SomeClass34343383.m_public_static_Func_ChartComponentViewModel_bool_ = new Func<ChartComponentViewModel, bool>( ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.SomeMEthod03852 ) ) ).SelectMany<ChartComponentViewModel, Order>( new Func<ChartComponentViewModel, IEnumerable<Order>>( new ScichartSurfaceMVVM.PrivateSealedClass0835()
        {
            m_public_Func_Order_bool_ = _param1
        }.public_IEnumerable_Order_098 ) );
    }

    public void InitPropertiesEventHandlers()
    {
        ( ( INotifyPropertyChanged ) this.Chart ).PropertyChanged += new PropertyChangedEventHandler( this.OnChartPropertyChanged );
        if ( !this._doneInitialization )
        {
            this._doneInitialization = true;
            this.ChartModifier.ChildModifiers.Add( ( IChartModifier ) this.LegendModifier );
            this.ChartModifier.ChildModifiers.Add( ( IChartModifier ) this.AnnotationModifier );
            IChartCandleElement[] array = ((SynchronizedDictionary<IChartComponent, ChartComponentViewModel>) this._componentsCache).Keys.OfType<IChartCandleElement>().ToArray<IChartCandleElement>();
            if ( array.Length != 0 )
                CollectionHelper.ForEach<IChartCandleElement>( ( IEnumerable<IChartCandleElement> ) array, new Action<IChartCandleElement>( this.SomeWhTekjerek023 ) );
        }
        this.AssignOrphanedChildElementsWithParent();

        if ( this.ParentViewModel == null )
        {
            this.ClosePaneCommand = ( ICommand ) null;
        }
        else
        {
            this.ClosePaneCommand = ( ICommand ) this.ParentViewModel.ClosePaneCommand;
            this.ParentViewModel.AddPropertyListener( ChartViewModel.ShowLegendProperty, new Action<DependencyPropertyChangedEventArgs>( this.OnShowLegendProperty ) );
            this.ParentViewModel.AddPropertyListener( ChartViewModel.ShowOverviewProperty, new Action<DependencyPropertyChangedEventArgs>( this.OnShowOverview ) );
            this.ParentViewModel.AddPropertyListener( ChartViewModel.MinimumRangeProperty, new Action<DependencyPropertyChangedEventArgs>( this.OnMinimumRangeProperty ) );
            this.ParentViewModel.AddPropertyListener( ChartViewModel.SelectedThemeProperty, new Action<DependencyPropertyChangedEventArgs>( this.OnSelectedThemeProperty ) );
        }
        StockSharp.Xaml.Charting.Chart groupChart = this.GroupChart;
        if ( groupChart != null )
        {
            this.SetupModifiers();
            CollectionHelper.AddRange<IChartModifier>( ( ICollection<IChartModifier> ) this.ChartModifier.ChildModifiers, ( IEnumerable<IChartModifier> ) this._chartModifiers );
            this.AnnotationModifier.SetBindings( FxAnnotationModifier.UserAnnotationTypeProperty, ( object ) groupChart, "AnnotationType" );
            UltrachartCursormodifier yx2796KwcrF36XmmEjd = this.ChartModifier.ChildModifiers.OfType<UltrachartCursormodifier>().Single<UltrachartCursormodifier>();
            yx2796KwcrF36XmmEjd.SetBindings( ChartModifierBase._isEnabled, ( object ) this.Chart, "CrossHair" );
            yx2796KwcrF36XmmEjd.SetBindings( TooltipModifierBase.ShowAxisLabelsProperty, ( object ) this.Chart, "CrossHairAxisLabels" );
            yx2796KwcrF36XmmEjd.SetBindings( UltrachartCursormodifier.InPlaceTooltipProperty, ( object ) this.Chart, "CrossHairTooltip" );
            ChartOrderModifier orderLines = this.ChartModifier.ChildModifiers.OfType <ChartOrderModifier> ().Single <ChartOrderModifier> ();
            ChartOrderModifier orderLines = orderLines;
            DependencyProperty zSlZmDsF5TsAu = ChartModifierBase._isEnabled;
            BoolAllConverter conv = new BoolAllConverter();
            conv.Value = true;
            Binding[] bindingArray = new Binding[2]
      {
        new Binding("OrderCreationMode")
        {
          Source = (object) this.Chart
        },
        new Binding("PaneHasCandles") { Source = (object) this }
      };
            orderLines.SetMultiBinding( zSlZmDsF5TsAu, ( IMultiValueConverter ) conv, bindingArray );
            orderLines.SetBindings( ChartOrderModifier.ShowHorizontalLineProperty, ( object ) this.Chart, "CrossHair", BindingMode.OneWay, ( IValueConverter ) new InverseBooleanConverter() );
            this.ChartModifier.ChildModifiers.OfType<fxZoomPanModifier>().Single<fxZoomPanModifier>().SetBindings( ChartModifierBase._isEnabled, ( object ) this.Chart, "AnnotationType", BindingMode.OneWay, ( IValueConverter ) new EnumBooleanConverter(), ( object ) ChartAnnotationTypes.None.ToString() );
            MouseManager.SetMouseEventGroup( ( DependencyObject ) this.ChartModifier, this.PaneGroup );
        }
        this.RestartTimer();
        CollectionHelper.ForEach<IChartAxis>( ( IEnumerable<IChartAxis> ) this.Area.XAxises, new Action<IChartAxis>( this.OnAddXAxises ) );
        CollectionHelper.ForEach<IChartAxis>( ( IEnumerable<IChartAxis> ) this.Area.YAxises, new Action<IChartAxis>( this.OnAddYAxises ) );
    }

    public void Release()
    {
        ( ( INotifyPropertyChanged ) this.Chart ).PropertyChanged -= new PropertyChangedEventHandler( this.OnChartPropertyChanged );
        if ( this.GroupChart != null )
            this._chartModifiers.ForEach( new Action<ChartModifierBase>( this.OnReleaseModifiersD ) );
        CollectionHelper.ForEach<IChartAxis>( ( IEnumerable<IChartAxis> ) this.Area.XAxises, new Action<IChartAxis>( this.OnRemoveXAxis ) );
        CollectionHelper.ForEach<IChartAxis>( ( IEnumerable<IChartAxis> ) this.Area.YAxises, new Action<IChartAxis>( this.OnRemoveYAxises ) );
        this._dispatcherTimer.Stop();
    }

    private void RestartTimer()
    {
        this._dispatcherTimer.Stop();
        this._dispatcherTimer.Interval = this.AutoRangeInterval;
        this._dispatcherTimer.Start();
    }

    private void AddAxis( IChartAxis _param1, ICollection<IAxis> _param2 )
    {
        ScichartSurfaceMVVM.PrivateSealedClass_3Ins_1Met mpa4F4mcyQlZrFjfRi = new ScichartSurfaceMVVM.PrivateSealedClass_3Ins_1Met();
        mpa4F4mcyQlZrFjfRi._variableSome3535 = this;
        mpa4F4mcyQlZrFjfRi._IChartAxis_098 = _param1;
        mpa4F4mcyQlZrFjfRi._ICollection_IAxis_098 = _param2;
        FrameworkElement chart = (FrameworkElement) this.Chart;
        if ( chart == null )
            return;
        ( ( DispatcherObject ) chart ).GuiAsync( new Action( mpa4F4mcyQlZrFjfRi.OnGuiAsyncDoStuff ) );
    }

    private void SetupAxisBindings()
    {
        foreach ( AxisBase axe in this.XAxises.OfType<AxisBase>() )
        {
            if ( axF9ZgQ7NbH9KsEjd1.Tag is IChartAxis tag )
            {
                VisibleRangeDpo rangeDep = VisibleRangeDpo.AddRangeProperty( this.GetRootElement(), tag.Group, this.PaneGroupSuffix, tag.AxisType );
                AxisBase axF9ZgQ7NbH9KsEjd2 = axF9ZgQ7NbH9KsEjd1;
                DependencyProperty zWl3LbWhL1z0D = AxisBase.VisibleRangeProperty;
                VisibleRangeDpo dataObject = rangeDep;
                string path;
                if ( rangeDep.GetAxisType() != ChartAxisType.CategoryDateTime )
                {
                    if ( rangeDep.GetAxisType() != ChartAxisType.Numeric )
                    {
                        if ( rangeDep.GetAxisType() != ChartAxisType.DateTime )
                            throw new NotSupportedException( "unsupported range type" );
                        path = "DateTimeRange";
                    }
                    else
                        path = "NumericRange";
                }
                else
                    path = "CategoryDateTimeRange";
                axF9ZgQ7NbH9KsEjd2.SetBindings( zWl3LbWhL1z0D, ( object ) dataObject, path );
            }
        }
    }

    private bool RemoveAxis(
      IChartAxis _param1,
      ICollection<IAxis> _param2 )
    {
        ScichartSurfaceMVVM.SomeSealClass083523 jy0mx0yCuWlqEsh0ZdY = new ScichartSurfaceMVVM.SomeSealClass083523();
        jy0mx0yCuWlqEsh0ZdY._ICollection_IAxis_098 = _param2;
        jy0mx0yCuWlqEsh0ZdY._IChartAxis_098 = _param1;
        jy0mx0yCuWlqEsh0ZdY._variableSome3535 = this;
        FrameworkElement chart = (FrameworkElement) this.Chart;
        if ( chart != null )
            ( ( DispatcherObject ) chart ).GuiAsync( new Action( jy0mx0yCuWlqEsh0ZdY._SomeSealClass083523_Metho03 ) );
        return true;
    }

    private void OnTargetPropertyChanged308( object _param1, PropertyChangedEventArgs _param2 )
    {
        if ( !( _param1 is CategoryDateTimeAxis nu9622VfydaypdeqEjd ) || _param2.PropertyName != "CurrentDatapointPixelSize" )
            return;
        IChartCandleElement chartCandleElement = ((SynchronizedDictionary<IChartComponent, ChartComponentViewModel>) this._componentsCache).Keys.OfType<IChartCandleElement>().FirstOrDefault<IChartCandleElement>();
        if ( chartCandleElement != null && chartCandleElement.XAxisId != nu9622VfydaypdeqEjd.Id )
            return;
        this.DataPointWidth = Math.Round( MathHelper.IsNaN( nu9622VfydaypdeqEjd.CurrentDatapointPixelSize ) ? 0.0 : nu9622VfydaypdeqEjd.CurrentDatapointPixelSize, 1, MidpointRounding.AwayFromZero );
    }

    public void SetScichartSurface(
      SciChartSurface _param1 )
    {
        if ( this._sciChartSurface == _param1 )
            return;
        this._sciChartSurface = this._sciChartSurface == null || _param1 == null ? _param1 : throw new InvalidOperationException( "got unexpected chart surface" );
        _param1?.RenderSurface.Rendered( new EventHandler<RenderedEventArgs>( this.OnRenderSurfaceRendered ) );
    }

    public void Draw( ChartDrawData _param1 )
    {
        if ( this._sciChartSurface != null )
        {
            using ( this._sciChartSurface.SuspendUpdates() )
                this.Draw( _param1 );
        }
        else
            this.Draw( _param1 );
    }

    private void OnRenderSurfaceRendered(
      object _param1,
      RenderedEventArgs _param2 )
    {
        if ( this._sciChartSurface == null )
            return;
        CollectionHelper.ForEach<CategoryDateTimeAxis>( this.XAxises.OfType<CategoryDateTimeAxis>(), ScichartSurfaceMVVM.SomeClass34343383.public_static_Action_CategoryDateTimeAxis_009 ?? ( ScichartSurfaceMVVM.SomeClass34343383.public_static_Action_CategoryDateTimeAxis_009 = new Action<CategoryDateTimeAxis>( ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.SomeMEthod03853 ) ) );
        CollectionHelper.ForEach<ChartComponentViewModel>( ( IEnumerable<ChartComponentViewModel> ) ( ( SynchronizedDictionary<IChartComponent, ChartComponentViewModel> ) this._componentsCache ).Values, ScichartSurfaceMVVM.SomeClass34343383.public_static_Action_ChartComponentViewModel_008 ?? ( ScichartSurfaceMVVM.SomeClass34343383.public_static_Action_ChartComponentViewModel_008 = new Action<ChartComponentViewModel>( ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.SomeMEthod03854 ) ) );
        if ( !this.ShowPerfStats || _param2.\u0023\u003DzguiAuOeZYTXy() <= 0.0)
      return;
        double num = 1000.0 / _param2.\u0023\u003DzguiAuOeZYTXy();
        while ( this._queue.Count >= 10 )
            this._fpsTotal -= this._queue.Dequeue();
        this._queue.Enqueue( num );
        this._fpsTotal += num;
        this.PerfStats = $"FPS: {this._fpsTotal / ( double ) this._queue.Count:0}   Count: {this._sciChartSurface.RenderableSeries.Where<IRenderableSeries>( ScichartSurfaceMVVM.SomeClass34343383._public_static_Func_IRenderableSeries_bool_003 ?? ( ScichartSurfaceMVVM.SomeClass34343383._public_static_Func_IRenderableSeries_bool_003 = new Func<IRenderableSeries, bool>( ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.SomeMEthod03855 ) ) ).Sum<IRenderableSeries>( ScichartSurfaceMVVM.SomeClass34343383.m_public_static_Func_IRenderableSeries__nt_ ?? ( ScichartSurfaceMVVM.SomeClass34343383.m_public_static_Func_IRenderableSeries__nt_ = new Func<IRenderableSeries, int>( ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.public_int_Method_IRenderableSeries_ ) ) ):n0}";
    }

    private void Draw( ChartDrawData _param1 )
    {
        foreach ( ChartComponentViewModel a4VgOpCeDiqsTdzB in this._componentsCache.GetComponents() )
        {
            if ( a4VgOpCeDiqsTdzB.Draw( _param1 ) )
                ( ( BaseCollection<ChartComponentViewModel, ISet<ChartComponentViewModel>> ) this._parentChartViewModelCache ).Add( a4VgOpCeDiqsTdzB );
        }
    }

    private void OnTimer( object _param1, EventArgs _param2 )
    {
        ChartComponentViewModel[] a4VgOpCeDiqsTdzBArray;
        lock ( ( ( SynchronizedCollection<ChartComponentViewModel, ISet<ChartComponentViewModel>> ) this._parentChartViewModelCache ).SyncRoot )
            a4VgOpCeDiqsTdzBArray = CollectionHelper.CopyAndClear<ChartComponentViewModel>( ( ICollection<ChartComponentViewModel> ) this._parentChartViewModelCache );
        foreach ( ChartComponentViewModel a4VgOpCeDiqsTdzB in a4VgOpCeDiqsTdzBArray )
            a4VgOpCeDiqsTdzB.PerformPeriodicalAction();
    }

    public void Reset( IEnumerable<IChartElement> _param1 )
    {
        foreach ( IChartComponent ddznyiGmdRlAevOq in _param1 )
        {
            ChartComponentViewModel a4VgOpCeDiqsTdzB;
            if ( this.GetViewModelFromCache( ddznyiGmdRlAevOq, out a4VgOpCeDiqsTdzB ))
      {
                ( ( BaseCollection<ChartComponentViewModel, ISet<ChartComponentViewModel>> ) this._parentChartViewModelCache ).Add( a4VgOpCeDiqsTdzB );
                a4VgOpCeDiqsTdzB.Reset();
            }
        }
    }

    private void AssignOrphanedChildElementsWithParent()
    {
        foreach ( IChartComponent ddznyiGmdRlAevOq in ( ( IEnumerable<KeyValuePair<IChartComponent, ChartComponentViewModel>> ) this._componentsCache ).Where<KeyValuePair<IChartComponent, ChartComponentViewModel>>( ScichartSurfaceMVVM.SomeClass34343383.public_static_Func_KeyValuePair_IChartComponent_ChartComponentViewModel_bool_ ?? ( ScichartSurfaceMVVM.SomeClass34343383.public_static_Func_KeyValuePair_IChartComponent_ChartComponentViewModel_bool_ = new Func<KeyValuePair<IChartComponent, ChartComponentViewModel>, bool>( ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.public_bool_Method_KeyValuePair_IChartComponent_ChartComponentViewModel ) ) ).Select<KeyValuePair<IChartComponent, ChartComponentViewModel>, IChartComponent>( ScichartSurfaceMVVM.SomeClass34343383.public_static_Func_KeyValuePair_IChartComponent_ChartComponentViewModel_IChartComponent_ ?? ( ScichartSurfaceMVVM.SomeClass34343383.public_static_Func_KeyValuePair_IChartComponent_ChartComponentViewModel_IChartComponent_ = new Func<KeyValuePair<IChartComponent, ChartComponentViewModel>, IChartComponent>( ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.public_bool_Method_KeyValuePair_IChartComponent_ChartComponentViewModel_033 ) ) ).ToArray<IChartComponent>() )
        {
            ChartComponentViewModel a4VgOpCeDiqsTdzB = new ChartComponentViewModel(this, ddznyiGmdRlAevOq);
            a4VgOpCeDiqsTdzB.InitializeChildElements( CollectionHelper.Append2<IChartElement>( ddznyiGmdRlAevOq.ChildElements, ( IChartElement ) ddznyiGmdRlAevOq ).OfType<IDrawableChartElement>().Where<IDrawableChartElement>( ScichartSurfaceMVVM.SomeClass34343383.Func_IDrawableChartElement_bool_098 ?? ( ScichartSurfaceMVVM.SomeClass34343383.Func_IDrawableChartElement_bool_098 = new Func<IDrawableChartElement, bool>( ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.public_bool_Method_KeyValuePair_IChartComponent_ChartComponentViewModel_0352 ) ) ).Select<IDrawableChartElement, DrawableChartElementBaseViewModel>( new Func<IDrawableChartElement, DrawableChartElementBaseViewModel>( this.CreateDrawableChartElementBaseViewModel) ).Where<DrawableChartElementBaseViewModel>( ScichartSurfaceMVVM.SomeClass34343383.__Func_DrawableChartElementBaseViewModel_bool_003 ?? ( ScichartSurfaceMVVM.SomeClass34343383.__Func_DrawableChartElementBaseViewModel_bool_003 = new Func<DrawableChartElementBaseViewModel, bool>( ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.public_bool_Method_KeyValuePair_IChartComponent_ChartComponentViewModel_4353 ) ) ) );
            ( ( SynchronizedDictionary<IChartComponent, ChartComponentViewModel> ) this._componentsCache )[ ddznyiGmdRlAevOq ] = a4VgOpCeDiqsTdzB;
            if ( ddznyiGmdRlAevOq.IsLegend )
                this.LegendElements.Add( a4VgOpCeDiqsTdzB );
        }
        this.UpdateSomeUI();
    }

    private void UpdateSomeUI()
    {
        this.CandlesCompositeElement = ( ( IEnumerable<KeyValuePair<IChartComponent, ChartComponentViewModel>> ) this._componentsCache ).FirstOrDefault<KeyValuePair<IChartComponent, ChartComponentViewModel>>( ScichartSurfaceMVVM.SomeClass34343383._Func_KeyValuePair_IChartComponent_ChartComponentViewModel__bool_ ?? ( ScichartSurfaceMVVM.SomeClass34343383._Func_KeyValuePair_IChartComponent_ChartComponentViewModel__bool_ = new Func<KeyValuePair<IChartComponent, ChartComponentViewModel>, bool>( ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.public_bool_Method_0983 ) ) ).Value;
        this.PaneHasCandles = this.CandlesCompositeElement != null;
    }

    public bool GetViewModelFromCache(
      IChartComponent _param1,
      out ChartComponentViewModel _param2 )
    {
        return ( ( SynchronizedDictionary<IChartComponent, ChartComponentViewModel> ) this._componentsCache ).TryGetValue( _param1, ref _param2 );
    }

    public void OnChartAreaElementsAdded( IChartElement _param1 )
    {
        IChart chart = this.Chart;
        if ( chart != null )
            chart.EnsureUIThread();
        IChartComponent ddznyiGmdRlAevOq = (IChartComponent) _param1;
        ddznyiGmdRlAevOq.AddAxisesAndEventHandler( this.Area );
        if ( ( ( SynchronizedDictionary<IChartComponent, ChartComponentViewModel> ) this._componentsCache ).ContainsKey( ddznyiGmdRlAevOq ) )
            throw new ArgumentException( "duplicate chart element", "element" );
        if ( this.Chart != null )
        {
            ChartComponentViewModel a4VgOpCeDiqsTdzB = new ChartComponentViewModel(this, ddznyiGmdRlAevOq);
            a4VgOpCeDiqsTdzB.InitializeChildElements( CollectionHelper.Append2<IChartElement>( ddznyiGmdRlAevOq.ChildElements, ( IChartElement ) ddznyiGmdRlAevOq ).OfType<IDrawableChartElement>().Where<IDrawableChartElement>( ScichartSurfaceMVVM.SomeClass34343383.__Func_IDrawableChartElement__bool__903 ?? ( ScichartSurfaceMVVM.SomeClass34343383.__Func_IDrawableChartElement__bool__903 = new Func<IDrawableChartElement, bool>( ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.public_bool_Method_0983333 ) ) ).Select<IDrawableChartElement, DrawableChartElementBaseViewModel>( new Func<IDrawableChartElement, DrawableChartElementBaseViewModel>( this.NewDrawableChartElementBaseViewModel) ).Where<DrawableChartElementBaseViewModel>( ScichartSurfaceMVVM.SomeClass34343383.__Func_DrawableChartElementBaseViewModel__bool__003 ?? ( ScichartSurfaceMVVM.SomeClass34343383.__Func_DrawableChartElementBaseViewModel__bool__003 = new Func<DrawableChartElementBaseViewModel, bool>( ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.public_bool_Method_5498751 ) ) ) );
            ( ( SynchronizedDictionary<IChartComponent, ChartComponentViewModel> ) this._componentsCache )[ ddznyiGmdRlAevOq ] = a4VgOpCeDiqsTdzB;
            if ( ddznyiGmdRlAevOq.IsLegend )
                this.LegendElements.Add( a4VgOpCeDiqsTdzB );
        }
        else
            ( ( SynchronizedDictionary<IChartComponent, ChartComponentViewModel> ) this._componentsCache )[ ddznyiGmdRlAevOq ] = ( ChartComponentViewModel ) null;
        if ( this._modifierGroup != null && ddznyiGmdRlAevOq is IChartCandleElement chartCandleElement )
        {
            this.OnDrawStylePropertyChanged( chartCandleElement );
            chartCandleElement.PropertyChanged += new PropertyChangedEventHandler( this.Candle_PropertyChanged );
        }
        this.UpdateSomeUI();
        ddznyiGmdRlAevOq.PropertyChanged += new PropertyChangedEventHandler( this.OnChartComponentPropertiesChanged );
    }

    public bool OnChartAreaElementsRemoving( IChartElement _param1 )
    {
        IChart chart = this.Chart;
        if ( chart != null )
            chart.EnsureUIThread();
        IChartComponent ddznyiGmdRlAevOq = (IChartComponent) _param1;
        ChartComponentViewModel a4VgOpCeDiqsTdzB;
        if ( !this.GetViewModelFromCache( ddznyiGmdRlAevOq, out a4VgOpCeDiqsTdzB ))
      return false;
        ddznyiGmdRlAevOq.RemoveAxisesEventHandler();
        if ( ddznyiGmdRlAevOq is IChartCandleElement chartCandleElement )
            chartCandleElement.PropertyChanged -= new PropertyChangedEventHandler( this.Candle_PropertyChanged );
        ddznyiGmdRlAevOq.PropertyChanged -= new PropertyChangedEventHandler( this.OnChartComponentPropertiesChanged );
        ( ( SynchronizedDictionary<IChartComponent, ChartComponentViewModel> ) this._componentsCache ).Remove( ddznyiGmdRlAevOq );
        if ( a4VgOpCeDiqsTdzB != null )
        {
            a4VgOpCeDiqsTdzB.GuiUpdateAndClear();
            a4VgOpCeDiqsTdzB.Dispose();
            this.LegendElements.Remove( a4VgOpCeDiqsTdzB );
        }
        this.UpdateSomeUI();
        return true;
    }

    private void OnChartComponentPropertiesChanged( object _param1, PropertyChangedEventArgs _param2 )
    {
        IChartComponent ddznyiGmdRlAevOq = (IChartComponent) _param1;
        if ( _param2.PropertyName != "XAxisId" && _param2.PropertyName != "YAxisId" )
            return;
        IChart chart = this.Chart;
        if ( chart != null )
            chart.EnsureUIThread();
        List<IRenderableSeries> koh9jO5RuUcFiAqLcList;
        if ( this._chartUIRSeries.TryGetValue( ddznyiGmdRlAevOq, out koh9jO5RuUcFiAqLcList ) )
        {
            if ( ddznyiGmdRlAevOq.TryGetXAxis() != null && ddznyiGmdRlAevOq.TryGetYAxis() != null )
            {
                foreach ( IRenderableSeries koh9jO5RuUcFiAqLc in koh9jO5RuUcFiAqLcList )
                {
                    if ( !this._advanceChartRenderableSeries.Contains( koh9jO5RuUcFiAqLc ) )
                        this._advanceChartRenderableSeries.Add( koh9jO5RuUcFiAqLc );
                }
            }
            else
            {
                foreach ( IRenderableSeries koh9jO5RuUcFiAqLc in koh9jO5RuUcFiAqLcList )
                    this._advanceChartRenderableSeries.Remove( koh9jO5RuUcFiAqLc );
            }
        }
        Dictionary<object, IAnnotation> dictionary;
        if ( !this._topChartElmentAnnotationMap.TryGetValue( ddznyiGmdRlAevOq, out dictionary ) )
            return;
        if ( ddznyiGmdRlAevOq.TryGetXAxis() != null && ddznyiGmdRlAevOq.TryGetYAxis() != null )
        {
            foreach ( KeyValuePair<object, IAnnotation> keyValuePair in dictionary )
            {
                if ( !this.Annotations.Contains( keyValuePair.Value ) )
                    this.Annotations.Add( keyValuePair.Value );
            }
        }
        else
        {
            foreach ( KeyValuePair<object, IAnnotation> keyValuePair in dictionary )
                this.Annotations.Remove( keyValuePair.Value );
        }
    }

    private void Candle_PropertyChanged(
      object _param1,
      PropertyChangedEventArgs _param2 )
    {
        if ( !( _param2.PropertyName == "DrawStyle" ) )
            return;
        this.OnDrawStylePropertyChanged( ( IChartCandleElement ) _param1 );
    }

    private void OnDrawStylePropertyChanged( IChartCandleElement _param1 )
    {
        this._rubberBandXyZoomModifier.IsXAxisOnly = !_param1.DrawStyle.IsVolumeProfileChart();
    }

    public VisibleRangeDpo GetVisibleRangeDpo(
      string _param1 )
    {
        IChartAxis chartAxis = ((IEnumerable<IChartAxis>) this.Area.XAxises).FirstOrDefault<IChartAxis>(new Func<IChartAxis, bool>(new ScichartSurfaceMVVM.SomeSealClass0833352()
        {
            _someString0382 = _param1
        }.bool_Method02_IChartAxis_));
        return chartAxis == null ? ( VisibleRangeDpo ) null : VisibleRangeDpo.AddRangeProperty( this.GetRootElement(), chartAxis.Group, this.PaneGroupSuffix, chartAxis.AxisType );
    }

    private void OnShowHiddenAxes()
    {
        CollectionHelper.ForEach<IChartAxis>( ( IEnumerable<IChartAxis> ) this.Area.XAxises, ScichartSurfaceMVVM.SomeClass34343383._Action_IChartAxis_0932 ?? ( ScichartSurfaceMVVM.SomeClass34343383._Action_IChartAxis_0932 = new Action<IChartAxis>( ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.public_bool_Method_303403 ) ) );
        CollectionHelper.ForEach<IChartAxis>( ( IEnumerable<IChartAxis> ) this.Area.YAxises, ScichartSurfaceMVVM.SomeClass34343383._Action_IChartAxis_0932323 ?? ( ScichartSurfaceMVVM.SomeClass34343383._Action_IChartAxis_0932323 = new Action<IChartAxis>( ScichartSurfaceMVVM.SomeClass34343383.SomeMethond0343.public_bool_Method_938745 ) ) );
    }

    public string PaneGroupSuffix
    {
        get => this._paneGroupSuffix;
        set
        {
            value = value?.Trim() ?? string.Empty;
            if ( !this.SetField<string>( ref this._paneGroupSuffix, value, nameof( PaneGroupSuffix ) ) )
                return;
            this.NotifyChanged( "PaneGroup" );
            if ( this.GroupChart == null )
                return;
            this.SetupAxisBindings();
            MouseManager.SetMouseEventGroup( ( DependencyObject ) this.ChartModifier, this.PaneGroup );
        }
    }

    public string PaneGroup
    {
        get
        {
            object[] objArray = new object[2];
            objArray[ 0 ] = ( object ) ( this.GroupChart ?? throw new NotSupportedException() ).GetInstanceCount();
            objArray[ 1 ] = ( object ) this._paneGroupSuffix;
            return StringHelper.Put( "ssharpultrachart{0}_{1}", objArray );
        }
    }



    public StockSharp.Xaml.Charting.Chart GroupChart => this.Chart as StockSharp.Xaml.Charting.Chart;



    public ChartViewModel ParentViewModel
    {
        get => this.GroupChart?.ViewModel;
    }

    public LegendModifierVM LegendViewModel
    {
        get
        {
            if ( this._legendViewModel != null )
                return this._legendViewModel;
            this._legendViewModel = new LegendModifierVM( this )
            {
                LegendModifier = new LegendModifier()
            };
            this._legendViewModel.RemoveElementEvent( new Action<IChartElement>( this.OnRemoveElementEvent ) );
            return this._legendViewModel;
        }
    }

    public FxAnnotationModifier AnnotationModifier
    {
        get
        {
            FxAnnotationModifier zIfS1UpijEycx = this._annotationModifier;
            if ( zIfS1UpijEycx != null )
                return zIfS1UpijEycx;
            FxAnnotationModifier g7AaupM52GhwgqEjd = new FxAnnotationModifier(this.Area, this.Annotations);
            g7AaupM52GhwgqEjd.IsEnabled = false;
            FxAnnotationModifier annotationModifier = g7AaupM52GhwgqEjd;
            this._annotationModifier = g7AaupM52GhwgqEjd;
            return annotationModifier;
        }
    }

    public IEnumerable<IRenderableSeries> ChartSeriesViewModels
    {
        get
        {
            return ( IEnumerable<IRenderableSeries> ) this._advanceChartRenderableSeries;
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
        if ( !this._topChartElmentAnnotationMap.TryGetValue( _param1, out dictionary ) )
            this._topChartElmentAnnotationMap[ _param1 ] = dictionary = new Dictionary<object, IAnnotation>();
        dictionary.Add( _param3, _param2 );
        if ( _param1.TryGetXAxis() == null || _param1.TryGetYAxis() == null )
            return;
        this.Annotations.Add( _param2 );
    }

    public IAnnotation GetAxisMakerAnnotation(
      IChartComponent _param1,
      object _param2 )
    {
        Dictionary<object, IAnnotation> dictionary;
        return !this._topChartElmentAnnotationMap.TryGetValue( _param1, out dictionary ) ? ( IAnnotation ) null : CollectionHelper.TryGetValue<object, IAnnotation>( ( IDictionary<object, IAnnotation> ) dictionary, _param2 );
    }

    public void RemoveAnnotation(
      IChartComponent _param1,
      object _param2 )
    {
        Dictionary<object, IAnnotation> dictionary;
        if ( !this._topChartElmentAnnotationMap.TryGetValue( _param1, out dictionary ) )
            return;
        if ( _param2 == null )
        {
            foreach ( KeyValuePair<object, IAnnotation> keyValuePair in dictionary )
                this.Annotations.Remove( keyValuePair.Value );
            this._topChartElmentAnnotationMap.Remove( _param1 );
        }
        else
        {
            IAnnotation hhh93Q0DqkV5Sv90k;
            if ( !dictionary.TryGetValue( _param2, out hhh93Q0DqkV5Sv90k ) )
                return;
            this.Annotations.Remove( hhh93Q0DqkV5Sv90k );
            dictionary.Remove( _param2 );
        }
    }

    public void AddSeriesViewModelsToRoot(
      IChartComponent _param1,
      IRenderableSeries _param2 )
    {
        List<IRenderableSeries> koh9jO5RuUcFiAqLcList;
        if ( !this._chartUIRSeries.TryGetValue( _param1, out koh9jO5RuUcFiAqLcList ) )
            this._chartUIRSeries[ _param1 ] = koh9jO5RuUcFiAqLcList = new List<IRenderableSeries>();
        if ( !koh9jO5RuUcFiAqLcList.Contains( _param2 ) )
            koh9jO5RuUcFiAqLcList.Add( _param2 );
        this.OnChartComponentPropertiesChanged( ( object ) _param1, new PropertyChangedEventArgs( "XAxisId" ) );
    }

    public void RemoveChartComponent(
      IChartComponent _param1 )
    {
        List<IRenderableSeries> koh9jO5RuUcFiAqLcList;
        if ( !this._chartUIRSeries.TryGetValue( _param1, out koh9jO5RuUcFiAqLcList ) )
            return;
        foreach ( IRenderableSeries koh9jO5RuUcFiAqLc in koh9jO5RuUcFiAqLcList )
            this._advanceChartRenderableSeries.Remove( koh9jO5RuUcFiAqLc );
        this._chartUIRSeries.Remove( _param1 );
    }

    public void Refresh()
    {
        this._sciChartSurface?.InvalidateElement();
    }

    public AxisCollection XAxises
    {
        get => this._xAxisNotifyList;
    }

    public AxisCollection YAxises
    {
        get => this._yAxisNotifyList;
    }

    public ChartComponentViewModel CandlesCompositeElement
    {
        get => this._candleCompositeElement;
        private set => this._candleCompositeElement = value;
    }

    public bool PaneHasCandles
    {
        get => this._paneHasCandles;
        set
        {
            this.SetField<bool>( ref this._paneHasCandles, value, nameof( PaneHasCandles ) );
        }
    }

    public AnnotationCollection Annotations
    {
        get => this._annotationCollection;
    }

    public ModifierGroup ChartModifier
    {
        get
        {
            return this._modifierGroup ?? ( this._modifierGroup = new ModifierGroup() );
        }
        set
        {
            this.SetField<ModifierGroup>( ref this._modifierGroup, value, nameof( ChartModifier ) );
        }
    }

    public ViewportManager ViewportManager
    {
        get => this._viewPortManager;
    }

    public LegendModifier LegendModifier
    {
        get => this.LegendViewModel.LegendModifier;
    }

    public string Title
    {
        get => this.Area.Title;
        set => this.Area.Title = value;
    }

    public string PerfStats
    {
        get => this._perfStats;
        set
        {
            this.SetField<string>( ref this._perfStats, value, nameof( PerfStats ) );
        }
    }

    public double Height
    {
        get => this._height;
        set
        {
            this.SetField<double>( ref this._height, value, nameof( Height ) );
            this.Area.Height = value;
        }
    }

    public bool ShowPerfStats
    {
        get
        {
            StockSharp.Xaml.Charting.Chart groupChart = this.GroupChart;
            return groupChart == null ? this._showPerfStats : __nonvirtual( groupChart.ShowPerfStats );
        }
        set
        {
            if ( this.Chart is StockSharp.Xaml.Charting.Chart chart )
            {
                chart.ShowPerfStats = value;
                this.NotifyChanged( nameof( ShowPerfStats ) );
            }
            else
                this.SetField<bool>( ref this._showPerfStats, value, nameof( ShowPerfStats ) );
        }
    }

    public TimeSpan AutoRangeInterval
    {
        get
        {
            StockSharp.Xaml.Charting.Chart groupChart = this.GroupChart;
            return groupChart == null ? this._autoRangeIntervalNoGroup : groupChart.AutoRangeInterval;
        }
        set
        {
            if ( this.Chart is StockSharp.Xaml.Charting.Chart chart )
            {
                chart.AutoRangeInterval = value;
                this.NotifyChanged( nameof( AutoRangeInterval ) );
            }
            else
                this.SetField<TimeSpan>( ref this._autoRangeIntervalNoGroup, value, nameof( AutoRangeInterval ) );
        }
    }

    public bool ShowLegend
    {
        get
        {
            ChartViewModel parentViewModel = this.ParentViewModel;
            return parentViewModel == null ? this._showLegendNoParent : parentViewModel.ShowLegend;
        }
        set
        {
            if ( this.ParentViewModel != null )
            {
                this.ParentViewModel.ShowLegend = value;
                this.NotifyChanged( nameof( ShowLegend ) );
            }
            else
                this.SetField<bool>( ref this._showLegendNoParent, value, nameof( ShowLegend ) );
        }
    }

    public bool ShowOverview
    {
        get
        {
            ChartViewModel parentViewModel = this.ParentViewModel;
            return parentViewModel == null ? this._showOverviewNoParent : parentViewModel.ShowOverview;
        }
        set
        {
            if ( this.ParentViewModel != null )
            {
                this.ParentViewModel.ShowOverview = value;
                this.NotifyChanged( nameof( ShowOverview ) );
            }
            else
                this.SetField<bool>( ref this._showOverviewNoParent, value, nameof( ShowOverview ) );
        }
    }

    public int MinimumRange
    {
        get
        {
            ChartViewModel parentViewModel = this.ParentViewModel;
            return parentViewModel == null ? this._minimumRange : parentViewModel.MinimumRange;
        }
        set
        {
            if ( this.ParentViewModel != null )
            {
                this.ParentViewModel.MinimumRange = value;
                this.NotifyChanged( nameof( MinimumRange ) );
            }
            else
                this.SetField<int>( ref this._minimumRange, value, nameof( MinimumRange ) );
        }
    }

    public string SelectedTheme
    {
        get => this.ParentViewModel?.SelectedTheme ?? this._selectThemeNoParent;
        set
        {
            if ( this.ParentViewModel != null )
            {
                this.ParentViewModel.SelectedTheme = value;
                this.NotifyChanged( nameof( SelectedTheme ) );
            }
            else
                this.SetField<string>( ref this._selectThemeNoParent, value, nameof( SelectedTheme ) );
        }
    }

    public double DataPointWidth
    {
        get => this._dataPointWidth;
        set
        {
            this.SetField<double>( ref this._dataPointWidth, value, nameof( DataPointWidth ) );
        }
    }

    public ICommand ResetAxisTimeZoneCommand
    {
        get => this._resetAxisTimeZoneCommand;
        set
        {
            this.SetField<ICommand>( ref this._resetAxisTimeZoneCommand, value, nameof( ResetAxisTimeZoneCommand ) );
        }
    }

    public ICommand ClosePaneCommand
    {
        get => this._closePaneCommand;
        set => this._closePaneCommand = value;
    }

    public ICommand ShowHiddenAxesCommand => this._showHiddenAxesCommand;

    void IDrawingSurfaceVM.ZoomExtents()
    {
        throw new NotSupportedException();
    }

    private void ChangeApplicationTheme() => this.SelectedTheme = ChartHelper.CurrChartTheme();

    public void Dispose()
    {
        this._sciChartSurface?.Dispose();
        this._sciChartSurface = ( SciChartSurface ) null;
    }

    public bool AllowElementToBeRemoved( ChartComponentViewModel _param1 )
    {
        if ( this.ParentViewModel == null || !this.ParentViewModel.IsInteracted )
            return false;
        bool flag;
        switch ( _param1.ChartComponent )
        {
            case IChartCandleElement _:
                flag = this.ParentViewModel.AllowAddCandles;
                break;
            case IChartIndicatorElement _:
                flag = this.ParentViewModel.AllowAddIndicators;
                break;
            case IChartOrderElement _:
                flag = this.ParentViewModel.AllowAddOrders;
                break;
            case IChartTradeElement _:
                flag = this.ParentViewModel.AllowAddOwnTrades;
                break;
            default:
                flag = false;
                break;
        }
        return flag;
    }





    private void SomeWhTekjerek023( IChartCandleElement _param1 )
    {
        this.OnDrawStylePropertyChanged( _param1 );
        _param1.PropertyChanged += new PropertyChangedEventHandler( this.Candle_PropertyChanged );
    }

    private void OnShowLegendProperty(
      DependencyPropertyChangedEventArgs _param1 )
    {
        this.NotifyChanged( "ShowLegend" );
    }

    private void OnShowOverview( DependencyPropertyChangedEventArgs _param1 )
    {
        this.NotifyChanged( "ShowOverview" );
    }

    private void OnMinimumRangeProperty( DependencyPropertyChangedEventArgs _param1 )
    {
        this.NotifyChanged( "MinimumRange" );
    }

    private void OnSelectedThemeProperty(
      DependencyPropertyChangedEventArgs _param1 )
    {
        this.NotifyChanged( "SelectedTheme" );
    }

    private void OnAddXAxises( IChartAxis _param1 )
    {
        this.AddAxis( _param1, ( ICollection<IAxis> ) this.XAxises );
    }

    private void OnAddYAxises( IChartAxis _param1 )
    {
        this.AddAxis( _param1, ( ICollection<IAxis> ) this.YAxises );
    }

    private void OnReleaseModifiersD(
      ChartModifierBase _param1 )
    {
        this.ChartModifier.ChildModifiers.Remove( ( IChartModifier ) _param1 );
    }

    private void OnRemoveXAxis( IChartAxis _param1 )
    {
        this.RemoveAxis( _param1, ( ICollection<IAxis> ) this.XAxises );
    }

    private void OnRemoveYAxises( IChartAxis _param1 )
    {
        this.RemoveAxis( _param1, ( ICollection<IAxis> ) this.YAxises );
    }

    private DrawableChartElementBaseViewModel CreateDrawableChartElementBaseViewModel(
      IDrawableChartElement _param1)
  {
    return _param1.CreateViewModel(this);
    }

    private DrawableChartElementBaseViewModel NewDrawableChartElementBaseViewModel(
      IDrawableChartElement _param1)
  {
    return _param1.CreateViewModel(this);
    }

    private void OnRemoveElementEvent( IChartElement _param1 )
    {
        this.ParentViewModel?.\u0023\u003DzzXq5ccDMuPZc( _param1 );
        Action<IChartElement> zeBeQvx4 = this.RemoveElementEvent;
        if ( zeBeQvx4 == null )
            return;
        zeBeQvx4( _param1 );
    }

    private sealed class PrivateSealedClass0835
    {
        public Func<Order, bool> m_public_Func_Order_bool_;
        public Func<ChartActiveOrdersElementVM, IEnumerable<Order>> Func_ChartActiveOrdersElementVM_0938;

        public IEnumerable<Order> public_IEnumerable_Order_098( ChartComponentViewModel _param1 )
        {
            return _param1.Elements.OfType<ChartActiveOrdersElementVM>().SelectMany<ChartActiveOrdersElementVM, Order>( this.Func_ChartActiveOrdersElementVM_0938 ?? ( this.Func_ChartActiveOrdersElementVM_0938 = new Func<ChartActiveOrdersElementVM, IEnumerable<Order>>( this.public_IEnumerable_Order__ ) ) );
        }

        public IEnumerable<Order> public_IEnumerable_Order__( ChartActiveOrdersElementVM _param1 )
        {
            return _param1.GetActiveOrders( this.m_public_Func_Order_bool_ );
        }
    }

    private sealed class PrivateSealedClass_3Ins_1Met
    {
        public ScichartSurfaceMVVM _variableSome3535;
        public IChartAxis _IChartAxis_098;
        public ICollection<IAxis> _ICollection_IAxis_098;

        public void OnGuiAsyncDoStuff()
        {
            if ( this._variableSome3535.Chart == null )
                return;
            AxisBase axF9ZgQ7NbH9KsEjd = this._IChartAxis_098.InitAndSetBinding(this._variableSome3535.ParentViewModel?.RemoveAxisCommand, this._variableSome3535.ResetAxisTimeZoneCommand, this._variableSome3535.Chart);
            axF9ZgQ7NbH9KsEjd.PropertyChanged += new PropertyChangedEventHandler( this._variableSome3535.OnTargetPropertyChanged308 );
            this._ICollection_IAxis_098.Add( ( IAxis ) axF9ZgQ7NbH9KsEjd );
            if ( this._ICollection_IAxis_098 != this._variableSome3535.XAxises )
                return;
            this._variableSome3535.SetupAxisBindings();
        }
    }

    [Serializable]
    private new sealed class SomeClass34343383
    {
        public static readonly ScichartSurfaceMVVM.SomeClass34343383 SomeMethond0343 = new ScichartSurfaceMVVM.SomeClass34343383();
        public static Action<ChartAxis> pubStatic_Action_ChartAxis_;
        public static Func<ChartComponentViewModel, bool> m_public_static_Func_ChartComponentViewModel_bool_;
        public static Action<CategoryDateTimeAxis> public_static_Action_CategoryDateTimeAxis_009;
        public static Action<ChartComponentViewModel> public_static_Action_ChartComponentViewModel_008;
        public static Func<IRenderableSeries, bool> _public_static_Func_IRenderableSeries_bool_003;
        public static Func<IRenderableSeries, int> m_public_static_Func_IRenderableSeries__nt_;
        public static Func<KeyValuePair<IChartComponent, ChartComponentViewModel>, bool> public_static_Func_KeyValuePair_IChartComponent_ChartComponentViewModel_bool_;
        public static Func<KeyValuePair<IChartComponent, ChartComponentViewModel>, IChartComponent> public_static_Func_KeyValuePair_IChartComponent_ChartComponentViewModel_IChartComponent_;
        public static Func<IDrawableChartElement, bool> Func_IDrawableChartElement_bool_098;
        public static Func<DrawableChartElementBaseViewModel, bool> __Func_DrawableChartElementBaseViewModel_bool_003;
        public static Func<KeyValuePair<IChartComponent, ChartComponentViewModel>, bool> _Func_KeyValuePair_IChartComponent_ChartComponentViewModel__bool_;
        public static Func<IDrawableChartElement, bool> __Func_IDrawableChartElement__bool__903;
        public static Func<DrawableChartElementBaseViewModel, bool> __Func_DrawableChartElementBaseViewModel__bool__003;
        public static Action<IChartAxis> _Action_IChartAxis_0932;
        public static Action<IChartAxis> _Action_IChartAxis_0932323;



        public bool SomeMEthod03852(
          ChartComponentViewModel _param1 )
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
          ChartComponentViewModel _param1 )
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

        public bool public_bool_Method_KeyValuePair_IChartComponent_ChartComponentViewModel(
          KeyValuePair<IChartComponent, ChartComponentViewModel> _param1 )
        {
            return _param1.Value == null;
        }

        public IChartComponent public_bool_Method_KeyValuePair_IChartComponent_ChartComponentViewModel_033(
          KeyValuePair<IChartComponent, ChartComponentViewModel> _param1 )
        {
            return _param1.Key;
        }

        public bool public_bool_Method_KeyValuePair_IChartComponent_ChartComponentViewModel_0352(
          IDrawableChartElement _param1 )
        {
            return !_param1.DontDraw;
        }

        public bool public_bool_Method_KeyValuePair_IChartComponent_ChartComponentViewModel_4353(
          DrawableChartElementBaseViewModel _param1 )
        {
            return _param1 != null;
        }

        public bool public_bool_Method_0983(
          KeyValuePair<IChartComponent, ChartComponentViewModel> _param1 )
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
            return _param1.Id == this._someString0382;
        }
    }



    private sealed class SomeSealClass083523
    {
        public ICollection<IAxis> _ICollection_IAxis_098;
        public IChartAxis _IChartAxis_098;
        public ScichartSurfaceMVVM _variableSome3535;
        public Func<IAxis, bool> _Func_IAxis_bool_0835;

        public void _SomeSealClass083523_Metho03()
        {
            AxisBase target = (AxisBase) this._ICollection_IAxis_098.FirstOrDefault<IAxis>(this._Func_IAxis_bool_0835 ?? (this._Func_IAxis_bool_0835 = new Func<IAxis, bool>(this._function_IAxis__R__void__001)));
            if ( target == null )
                return;
            target.PropertyChanged -= new PropertyChangedEventHandler( this._variableSome3535.OnTargetPropertyChanged308 );
            BindingOperations.ClearAllBindings( ( DependencyObject ) target );
            this._ICollection_IAxis_098.Remove( ( IAxis ) target );
        }

        public bool _function_IAxis__R__void__001(
          IAxis _param1 )
        {
            return _param1.Id == this._IChartAxis_098.Id;
        }
    }

    private sealed class SomeClass6409
    {
        public ScichartSurfaceMVVM _variableSome3535;
        public ChartArea _chartArea_093;
        public Action<IChartElement> _action_IChartElement_023;

        public bool OnChartAreaElementsRemovingAt( int _param1 )
        {
            return this._variableSome3535.OnChartAreaElementsRemoving( ( ( IList<IChartElement> ) this._chartArea_093.Elements )[ _param1 ] );
        }

        public bool OnChartAreaElementsClearing()
        {
            CollectionHelper.ForEach<IChartElement>( ( IEnumerable<IChartElement> ) this._chartArea_093.Elements, this._action_IChartElement_023 ?? ( this._action_IChartElement_023 = new Action<IChartElement>( this._function1_IChartElement____void__001 ) ) );
            return true;
        }

        public void _function1_IChartElement____void__001( IChartElement _param1 )
        {
            this._variableSome3535.OnChartAreaElementsRemoving( _param1 );
        }

        public void OnAreaXAxisesAdded( IChartAxis _param1 )
        {
            this._variableSome3535.AddAxis( _param1, ( ICollection<IAxis> ) this._variableSome3535.XAxises );
        }

        public bool OnAreaXAxisesRemoving( IChartAxis _param1 )
        {
            return this._variableSome3535.RemoveAxis( _param1, ( ICollection<IAxis> ) this._variableSome3535.XAxises );
        }

        public bool OnArea.XAxisesRemovingAt( int _param1 )
        {
            return this._variableSome3535.RemoveAxis( ( ( IList<IChartAxis> ) this._chartArea_093.XAxises )[ _param1 ], ( ICollection<IAxis> ) this._variableSome3535.XAxises );
        }

        public void OnAreaYAxisesAdded( IChartAxis _param1 )
        {
            this._variableSome3535.AddAxis( _param1, ( ICollection<IAxis> ) this._variableSome3535.YAxises );
        }

        public bool OnAreaYAxisesRemoving( IChartAxis _param1 )
        {
            return this._variableSome3535.RemoveAxis( _param1, ( ICollection<IAxis> ) this._variableSome3535.YAxises );
        }

        public bool OnAreaYAxisesRemovingAt( int _param1 )
        {
            return this._variableSome3535.RemoveAxis( ( ( IList<IChartAxis> ) this._chartArea_093.YAxises )[ _param1 ], ( ICollection<IAxis> ) this._variableSome3535.YAxises );
        }

        public void OnApplicationThemeChanged(
          DependencyObject _param1,
          ThemeChangedRoutedEventArgs _param2 )
        {
            this._variableSome3535.ChangeApplicationTheme();
        }
    }
}
