using DevExpress.Xpf.Core;
using Ecng.Collections;
using Ecng.Xaml;
using SciChart.Charting;
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
using fx.Charting;
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
using fx.Charting.Definitions;
using StockSharp.Xaml;
using StockSharp.BusinessEntities;
using SciChart.Charting.DrawingTools.TradingModifiers;
using System.Drawing;
using System.Windows.Media;
using SciChart.Charting.Themes;
using Ecng.Common;

public class ScichartSurfaceMVVM : ChartBaseViewModel, IChildPane, IScichartSurfaceVM, IDisposable
{
    private readonly CachedSynchronizedDictionary<IChartComponent, ParentVM>      _vmChartUIs = new CachedSynchronizedDictionary<IChartComponent, ParentVM>( );
    private readonly PooledDictionary<IChartComponent, PooledList<IRenderableSeries>>         _chartUIRSeries               = new PooledDictionary<IChartComponent, PooledList<IRenderableSeries>>( );

    private readonly PooledDictionary<IChartComponent, PooledDictionary<object, IAnnotation>> _topChartElmentAnnotationMap  = new PooledDictionary<IChartComponent, PooledDictionary<object, IAnnotation>>( );
    private readonly PooledDictionary<string, string>                                      _axisIdToGroup                = new PooledDictionary<string, string>( );
    private readonly ObservableCollection<ParentVM>                                  _legendElements               = new ObservableCollection<ParentVM>( );
    private readonly SynchronizedSet<ParentVM>                                       _parentChartViewModelCache    = new SynchronizedSet<ParentVM>( );

    private readonly PooledList<ChartModifierBase>                                         _chartModifiers        = new PooledList<ChartModifierBase>( );
    private ObservableCollection<IRenderableSeries>                                  _advanceChartRenderableSeries = new ObservableCollection<IRenderableSeries>( );
    //private fxTradingAnnotationCreationModifier _tradingAPI;

    private Action<IChartElement> RemoveElementEvent = null;

    private SciChartSurface                   _sciChartSurface;
    private bool                              _doneInitialization;

    private readonly DispatcherTimer          _dispatcherTimer;

    private readonly Queue<double>            _queue                    = new Queue<double>( );
    private double                            _fpsTotal;

    private IFastQuotes _quotesVM = null;
    private INullBar _candleStickVM = null;

    private readonly ChartArea                _chartArea;
    private LegendModifierVM                  _legendViewModel;
    private FxAnnotationModifier      _annotationModifier;
    
    private readonly AxisCollection           _xAxises                  = new AxisCollection( );
    private readonly AxisCollection           _yAxises                  = new AxisCollection( );
    private AnnotationCollection              _annotationCollection     = new AnnotationCollection( );
    private ModifierGroup                     _modifierGroup;
    private readonly ViewportManager          _viewPortManager          = new ViewportManager( );
    private string                            _title;
    private string                            _perfStats;
    private double                            _height                   = double.NaN;
    private bool                              _showPerfStats;
    private TimeSpan                          _autoRangeIntervalNoGroup = TimeSpan.FromMilliseconds( 200 );
    private bool                              _showLegendNoParent;
    private bool                              _showOverviewNoParent;
    private int                               _minimumRangeNoParent;
    private string                            _selectThemeNoParent;
    private ICommand                          _resetAxisTimeZoneCommand;
    private ICommand                          _closePaneCommand;
    private readonly ICommand                 _showHiddenAxesCommand;
    private fxDataPointSelectionModifier      _dataPointSelector;

    public ScichartSurfaceMVVM( ChartArea area, bool isViewModel = false ) : base( )
    {
        if ( area == null )
        {
            throw new ArgumentNullException( "area" );
        }
        
        _chartArea                          = area;
        _dispatcherTimer                    = new DispatcherTimer( DispatcherPriority.Render, Application.Current.Dispatcher );
        _dispatcherTimer.Tick              += new EventHandler( OnTimer );
        _showHiddenAxesCommand              = new ActionCommand( new Action( OnShowHiddenAxes ) );
        ResetAxisTimeZoneCommand            = new ActionCommand<ChartAxis>( a => a.TimeZone = null );

        Title                               = area.Title;
        Height                              = area.Height;


        area.Elements.Added      += OnChartAreaElementsAdded;
        area.Elements.Removing   += OnChartAreaElementsRemoving;
        area.Elements.RemovingAt += ( i => OnChartAreaElementsRemoving( ( area.Elements )[ i ] ) );
        area.Elements.Clearing   += ( ( ) => { MoreEnumerable.ForEach( area.Elements, ( i => OnChartAreaElementsRemoving( i ) ) ); return true; } );
        area.XAxises.Added                 += ( x => AddAxis( x, XAxises ) );
        area.XAxises.Removing              += ( a => RemoveAxis( a, area.XAxises, XAxises ) );
        area.XAxises.RemovingAt            += ( i => RemoveAxis( area.XAxises[ i ], area.XAxises, XAxises ) );
        area.YAxises.Added                 += ( y => AddAxis( y, YAxises ) );
        area.YAxises.Removing              += ( y => RemoveAxis( y, area.YAxises, YAxises ) );
        area.YAxises.RemovingAt            += ( i => RemoveAxis( area.YAxises[ i ], area.YAxises, YAxises ) );

        DevExpress.Xpf.Core.ThemeManager.ApplicationThemeChanged += ( d, e ) => SetSelectedTheme( );
        SetSelectedTheme( );

        foreach ( IChartElement chartElement in Area.Elements )
        {
            OnChartAreaElementsAdded( chartElement );
        }
    }

    int _waveScenarioNo = 1;

    public int WaveScenarioNo
    {
        get
        {
            return _waveScenarioNo;
        }
        set
        {
            _waveScenarioNo = value;

            _dataPointSelector.WaveScenarioNo = value;
        }
    }

    private object GetViewModel( )
    {
        return ChartExViewModel ?? ( object )this;
    }

    public ObservableCollection<ParentVM> LegendElements
    {
        get
        {
            return _legendElements;
        }
    }



    private void SetupModifiers( )
    {
        if ( _chartModifiers.Count > 0 )
        {
            return;
        }

        var cursor                              = new UltrachartCursormodifier( );
        cursor.ShowTooltip                      = true;
        cursor.ReceiveHandledEvents             = true;
        cursor.ShowTooltipOn                    = ShowTooltipOptions.MouseHover;
        cursor.HoverDelay = 1000;

        var orderLines                          = new ChartOrderModifier( Area );
        orderLines.IsEnabled                    = true;
        orderLines.CanCreateOrders              = true;

        _dataPointSelector                      = new fxDataPointSelectionModifier( );
        _dataPointSelector.ExecuteOn            = ExecuteOn.MouseMiddleButton;
        _dataPointSelector.ReceiveHandledEvents = true;
        _dataPointSelector.XAxisId              = "X";
        _dataPointSelector.IsEnabled            = true;
        _dataPointSelector.AllowsMultiSelection = true;
        

        var zoomPan                             = new fxZoomPanModifier( );
        zoomPan.ExecuteOn                       = ExecuteOn.MouseLeftButton;
        zoomPan.ReceiveHandledEvents            = true;
        
        zoomPan.ClipModeX                       = ClipMode.None;
        zoomPan.XyDirection                     = XyDirection.XYDirection;
        

        var xaxisDragModifier                   = new XAxisDragModifier( );
        xaxisDragModifier.AxisId                = "X";
        xaxisDragModifier.ClipModeX             = ClipMode.None;

        var yaxisDragModifier                   = new YAxisDragModifier( );
        yaxisDragModifier.AxisId                = "Y";

        var bandXyZoomModifier                  = new RubberBandXyZoomModifier( );
        bandXyZoomModifier.IsXAxisOnly          = true;
        bandXyZoomModifier.ExecuteOn            = ExecuteOn.MouseRightButton;
        bandXyZoomModifier.ReceiveHandledEvents = true;

        var zoomExtentsModifier                 = new ZoomExtentsModifier( );
        zoomExtentsModifier.ExecuteOn           = ExecuteOn.MouseDoubleClick;
        zoomExtentsModifier.XyDirection         = XyDirection.YDirection;

        var seriesValueModifer                  = new SeriesValueModifier( );

        //_tradingAPI                             = new fxTradingAnnotationCreationModifier( );
        //_tradingAPI.XAxisId                     = "X";
        //_tradingAPI.YAxisId                     = "Y";
        //_tradingAPI.ExecuteOn                   = ExecuteOn.MouseLeftButton;
        //_tradingAPI.ReceiveHandledEvents        = true;

        //_annotationModifier.TradingAPI          = _tradingAPI;

        
        var chartModifierBaseArray              = new ChartModifierBase[ 10 ];
        chartModifierBaseArray[ 0 ]             = orderLines;
        chartModifierBaseArray[ 1 ]             = cursor;
        chartModifierBaseArray[ 2 ]             = xaxisDragModifier;
        chartModifierBaseArray[ 3 ]             = yaxisDragModifier;
        chartModifierBaseArray[ 4 ]             = new MouseWheelZoomModifier( );
        chartModifierBaseArray[ 5 ]             = bandXyZoomModifier;
        chartModifierBaseArray[ 6 ]             = zoomExtentsModifier;
        chartModifierBaseArray[ 7 ]             = zoomPan;
        chartModifierBaseArray[ 8 ]             = _dataPointSelector;
        chartModifierBaseArray[ 9 ]             = seriesValueModifer;
        



        _chartModifiers.AddRange( chartModifierBaseArray );
    }

    private void OnChartPropertyChanged( object sender, PropertyChangedEventArgs e )
    {
        string propertyName = e.PropertyName;

        if ( !( propertyName == "AutoRangeInterval" ) )
        {
            if ( !( propertyName == "ShowPerfStats" ) )
            {
                return;
            }

            NotifyChanged( e.PropertyName );
        }
        else
        {
            RestartTimer( );
            NotifyChanged( e.PropertyName );
        }
    }

    public void InitPropertiesEventHandlers( )
    {
        ( ( INotifyPropertyChanged )Chart ).PropertyChanged += new PropertyChangedEventHandler( OnChartPropertyChanged );

        if ( !_doneInitialization )
        {
            _doneInitialization = true;

            ChartModifier.ChildModifiers.Add( LegendModifier );
            ChartModifier.ChildModifiers.Add( AnnotationModifier );

            CandlestickUI[ ] candles = _vmChartUIs.Keys.OfType<CandlestickUI>( ).ToArray( );

            if ( candles.Length != 0 )
            {
                var order = ChartModifier.ChildModifiers.OfType<ChartOrderModifier>( ).FirstOrDefault( );

                if ( order != null )
                {
                    order.IsEnabled = true;
                }

                foreach ( CandlestickUI candle in candles )
                {
                    OnDrawStylePropertyChanged( candle );
                    candle.PropertyChanged += new PropertyChangedEventHandler( Candle_PropertyChanged );
                }
            }
        }

        AssignOrphanedChildElementsWithParent( );

        if ( ChartExViewModel == null )
        {
            ClosePaneCommand = null;
        }
        else
        {
            ClosePaneCommand = ChartExViewModel.ClosePaneCommand;
            //ChartExViewModel.AddPropertyListener( ChartViewModel.ShowLegendProperty, e => NotifyChanged( "ShowLegend" ) );
            //ChartExViewModel.AddPropertyListener( ChartViewModel.ShowOverviewProperty, e => NotifyChanged( "ShowOverview" ) );
            //ChartExViewModel.AddPropertyListener( ChartViewModel.MinimumRangeProperty, e => NotifyChanged( "MinimumRange" ) );
            //ChartExViewModel.AddPropertyListener( ChartViewModel.SelectedThemeProperty, e => NotifyChanged( "SelectedTheme" ) );
        }

        GroupChartEx.Do( new Action<ChartExViewModel>( SetupModifiersAndBindingMVVM ) );

        RestartTimer( );

        foreach ( ChartAxis xAxis in Area.XAxises )
        {
            AddAxis( xAxis, XAxises );
        }

        foreach ( ChartAxis yAxis in Area.YAxises )
        {
            AddAxis( yAxis, YAxises );
        }
    }

    public void Release( )
    {
        ( ( INotifyPropertyChanged )Chart ).PropertyChanged -= OnChartPropertyChanged;


        if ( GroupChartEx != null )
        {
            foreach ( ChartModifierBase moidifier in _chartModifiers )
            {
                ChartModifier.ChildModifiers.Remove( moidifier );
            }
        }

        foreach ( ChartAxis xAxis in Area.XAxises )
        {
            RemoveAxis( xAxis, Area.XAxises, XAxises );
        }

        foreach ( ChartAxis yAxis in Area.YAxises )
        {
            RemoveAxis( yAxis, Area.YAxises, YAxises );
        }

        _dispatcherTimer.Stop( );
    }

    private void RestartTimer( )
    {
        _dispatcherTimer.Stop( );
        _dispatcherTimer.Interval = AutoRangeInterval;
        _dispatcherTimer.Start( );
    }

    private void AddAxis( ChartAxis axis, ICollection<IAxis> axises )
    {
        if ( Chart == null )
        {
            return;
        }

        VisbleRangeDp rangeProp = VisbleRangeDp.AddRangeProperty( GetViewModel( ), axis.Group, axis.AxisType );
        _axisIdToGroup[ axis.Id ] = axis.Group;

        axis.PropertyChanged += ( s, e ) =>
        {
            if ( !( e.PropertyName == "Group" ) )
                return;

            _axisIdToGroup[ axis.Id ] = axis.Group;
        };
        axises.Add( axis.InitAndSetBinding( rangeProp, ChartExViewModel?.RemoveAxisCommand, ResetAxisTimeZoneCommand, Chart ) );
    }

    private bool RemoveAxis( ChartAxis axis, INotifyList<ChartAxis> axisesNotifyList, ICollection<IAxis> axisesCollection )
    {
        if ( !axisesNotifyList.Contains( axis ) )
        {
            return false;
        }

        if ( Chart == null )
        {
            return true;
        }

        var axisBase = ( AxisBase )axisesCollection.FirstOrDefault( a => a.Id == axis.Id );
        if ( axisBase == null )
        {
            return true;
        }

        _axisIdToGroup.Remove( axis.Id );
        BindingOperations.ClearAllBindings( axisBase );
        axisesCollection.Remove( axisBase );
        return true;
    }

    public void SetScichartSurface( SciChartSurface scichart )
    {
        if ( _sciChartSurface == scichart )
        {
            return;
        }

        if ( _sciChartSurface != null && scichart != null )
        {
            throw new InvalidOperationException( "got unexpected chart surface" );
        }

        _sciChartSurface = scichart;

        if ( _sciChartSurface != null )
        {
            _sciChartSurface.RenderSurface.Rendered += OnRenderSurfaceRendered;
        }

        UpdateBackgroundColor( );
    }

    public void Refresh()
    {
        _sciChartSurface?.InvalidateElement( );
    }

    public void Draw( ChartDrawDataEx data )
    {
        if ( _sciChartSurface != null )
        {
            using ( _sciChartSurface.SuspendUpdates( ) )
            {
                StartRenderingChartUIs( data );
            }
        }
        else
        {
            StartRenderingChartUIs( data );
        }
    }

    private bool _isActive;
    public bool IsActive
    {
        get
        {
            return _isActive;
        }

        set
        {
            _isActive = value;
        }
    }

    public void UpdateQuote( DateTime barTime, double bid, double ask )
    {
        if ( ( _quotesVM == null ) || ( _candleStickVM == null ) )
        {
            return;
        }
            

        if ( _quotesVM.CanUpdateQuotes )
        {
            if ( _sciChartSurface != null )
            {
                using ( _sciChartSurface.SuspendUpdates( ) )
                {
                    _quotesVM?.UpdateQuotes( bid, ask );

                    if ( _candleStickVM.CanUpdateNullBar)
                    {
                        _candleStickVM?.UpdateNullBar( barTime, bid, ask );
                    }
                    
                }
            }
            else
            {
                _quotesVM.UpdateQuotes( bid, ask );

                if ( _candleStickVM.CanUpdateNullBar )
                {
                    _candleStickVM?.UpdateNullBar( barTime, bid, ask );
                }
                
            }
        }                 
    }

    private void OnRenderSurfaceRendered( object sender, RenderedEventArgs e )
    {
        if ( _sciChartSurface == null )
        {
            return;
        }

        var dateTimeAxises = XAxises.OfType<CategoryDateTimeAxis>( );

        foreach ( var axis in dateTimeAxises )
        {
            var tag = axis.Tag as ChartAxis;

            if ( tag == null )
            {
                return;
            }

            //Tony FIx
            //tag.DataPointWidth = axis.CurrentDatapointPixelSize;
        }

        var parentVms = _vmChartUIs.Values;

        foreach ( var vm in parentVms )
        {
            vm.UpdateChildElementYAxisMarker( );
        }

        if ( !ShowPerfStats || e.Duration <= 0.0 )
        {
            return;
        }

        double num = 1000.0 / e.Duration;
        while ( _queue.Count >= 10 )
        {
            _fpsTotal -= _queue.Dequeue( );
        }

        _queue.Enqueue( num );
        _fpsTotal += num;

        var someValue = _sciChartSurface.RenderableSeries
                        .Where( s => s.IsVisible )
                        .Sum( y => y.DataSeries.Return( x => x.Count, 0 ) );

        PerfStats = string.Format( "FPS: {0:0}   Count: {1:n0}", _fpsTotal / _queue.Count, someValue );
    }

    private void StartRenderingChartUIs( ChartDrawDataEx drawData )
    {
        foreach ( var vmChartUI in _vmChartUIs.CachedValues )
        {
            if ( vmChartUI.DrawChartData( drawData ) )
            {
                _parentChartViewModelCache.Add( vmChartUI );
            }
        }
    }

    private void OnTimer( object sender, EventArgs e )
    {
        ParentVM[ ] vms;

        lock ( _parentChartViewModelCache.SyncRoot )
        {
            vms = _parentChartViewModelCache.CopyAndClear( );
        }

        foreach ( ParentVM ParentChartViewModel in vms )
        {
            ParentChartViewModel.ChildElementPeriodicalAction( );
        }
    }

    public void Reset( IEnumerable<IChartElement> chartElements )
    {
        foreach ( IChartComponent chartElement in chartElements )
        {
            var ParentChartViewModel = _vmChartUIs.TryGetValue( chartElement );

            if ( ParentChartViewModel != null )
            {
                _parentChartViewModelCache.Add( ParentChartViewModel );
                ParentChartViewModel.UpdateChildElements( );
            }
        }
    }

    private void AssignOrphanedChildElementsWithParent( )
    {
        var noParents = _vmChartUIs.Where( p => p.Value == null ).Select( p => p.Key ).ToArray( );

        foreach ( IChartComponent element in noParents )
        {
            var combinedElements = MoreEnumerable.Append( element.ChildElements, element );

            var childElements = combinedElements
                                                .OfType<IDrawableChartElement>( )
                                                .Where( e => !e.DontDraw )
                                                .Select( e => e.CreateViewModel( this ) )
                                                .Where( e => e != null );

            ParentVM parentVm = new ParentVM( this, element, childElements );

            _vmChartUIs[ element ] = parentVm;

            if ( element.IsLegend )
            {
                LegendElements.Add( parentVm );
            }
        }
    }

    public void OnChartAreaElementsAdded( IChartElement anyChartUI )
    {
        if ( Chart != null )
        {
            Chart.EnsureUIThread( );
        }

        IChartComponent anyChartUiXY = ( IChartComponent )anyChartUI;
        anyChartUiXY.AddAxisesAndEventHandler( Area );

        if ( _vmChartUIs.ContainsKey( anyChartUiXY ) )
        {
            throw new ArgumentException( "duplicate chart element", "elem" );
        }

        bool foundFastQuotes = false;
        bool foundCandle = false;

        if ( Chart != null )
        {
            var combinedElements = MoreEnumerable.Append( anyChartUiXY.ChildElements, anyChartUiXY );
            var childElements = combinedElements.OfType<IDrawableChartElement>( ).Where( e => !e.DontDraw ).Select( e => e.CreateViewModel( this ) ).Where( e => e != null );

            foreach ( var child in childElements )
            {
                if ( child is IFastQuotes )
                {
                    foundFastQuotes = true;                    
                }

                if ( child is INullBar )
                {
                    foundCandle = true;
                }
            }

            ParentVM ParentChartViewModel = new ParentVM( this, anyChartUiXY, childElements );

            if ( foundFastQuotes )
            {
                _quotesVM = ( IFastQuotes )  ParentChartViewModel.Elements.ElementAt( 0 );
                _quotesVM.CanUpdateQuotes = true;
            }

            if ( foundCandle )
            {
                _candleStickVM = ( INullBar )ParentChartViewModel.Elements.ElementAt( 0 );
                _candleStickVM.CanUpdateNullBar = true;
            }

            _vmChartUIs[ anyChartUiXY ] = ParentChartViewModel;

            if ( anyChartUiXY.IsLegend )
            {
                LegendElements.Add( ParentChartViewModel );
            }
        }
        else
        {
            _vmChartUIs[ anyChartUiXY ] = null;
        }

        if ( _modifierGroup != null && anyChartUiXY is CandlestickUI candle )
        {
            var orderModifier = ChartModifier.ChildModifiers.OfType<ChartOrderModifier>( ).FirstOrDefault( );

            if ( orderModifier != null )
            {
                orderModifier.IsEnabled = true;
            }

            OnDrawStylePropertyChanged( candle );

            candle.PropertyChanged += new PropertyChangedEventHandler( Candle_PropertyChanged );
        }

        anyChartUiXY.PropertyChanged += new PropertyChangedEventHandler( OnXYAxisPropertyChanged );
    }

    public bool OnChartAreaElementsRemoving( IChartElement element )
    {
        if ( Chart != null )
        {
            Chart.EnsureUIThread( );
        }

        IChartComponent elementXY = ( IChartComponent )element;
        elementXY?.RemoveAxisesEventHandler( );

        ParentVM ParentChartViewModel;
        if ( !_vmChartUIs.TryGetValue( elementXY, out ParentChartViewModel ) )
        {
            return false;
        }

        if ( elementXY is CandlestickUI candle )
        {
            candle.PropertyChanged -= new PropertyChangedEventHandler( Candle_PropertyChanged );
        }

        elementXY.PropertyChanged -= new PropertyChangedEventHandler( OnXYAxisPropertyChanged );
        _vmChartUIs.Remove( elementXY );

        if ( ParentChartViewModel != null )
        {
            ParentChartViewModel.ChildElementUpdateAndClear( );
            ParentChartViewModel.Dispose( );
            LegendElements.Remove( ParentChartViewModel );
        }

        return true;
    }

    private void Candle_PropertyChanged( object sender, PropertyChangedEventArgs e )
    {
        if ( !( e.PropertyName == "DrawStyle" ) )
        {
            return;
        }

        OnDrawStylePropertyChanged( ( CandlestickUI )sender );
    }

    private void OnDrawStylePropertyChanged( CandlestickUI element )
    {
        var isVolumeProfileChart = element.DrawStyle.IsVolumeProfileChart( );

        //foreach ( var XAxis in XAxises )
        //{
        //    XAxis.DrawMinorGridLines = !isVolumeProfileChart;
        //    XAxis.DrawMajorGridLines = !isVolumeProfileChart;
        //}

        //foreach ( var YAxis in YAxises )
        //{
        //    YAxis.DrawMinorGridLines = !isVolumeProfileChart;
        //    YAxis.DrawMajorGridLines = !isVolumeProfileChart;
        //}


        var bandXyZoomModifier = ChartModifier.ChildModifiers.OfType<RubberBandXyZoomModifier>( ).FirstOrDefault( );

        if ( bandXyZoomModifier == null )
        {
            bandXyZoomModifier = new RubberBandXyZoomModifier( );
            ChartModifier.ChildModifiers.Add( bandXyZoomModifier );
        }

        bandXyZoomModifier.IsXAxisOnly          = !isVolumeProfileChart;
        bandXyZoomModifier.ExecuteOn            = ExecuteOn.MouseRightButton;
        bandXyZoomModifier.ReceiveHandledEvents = true;
    }

    public VisbleRangeDp GetVisibleRangeDp( string axisId )
    {
        ChartAxis chartAxis = Area.XAxises.FirstOrDefault( a => a.Id == axisId );

        if ( chartAxis == null )
        {
            return null;
        }

        string group =   _axisIdToGroup.TryGetValue( axisId );

        if ( group == null )
        {
            //return null;
            throw new InvalidOperationException( "Ecng.Common.StringHelper.Put( LocalizedStrings.Str2071Params, axisId )" );
        }

        return VisbleRangeDp.AddRangeProperty( GetViewModel( ), group, chartAxis.AxisType );
    }    

    private void OnShowHiddenAxes( )
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

    public string PaneGroup
    {
        get
        {            
            if ( GroupChartEx != null )
            {
                return Ecng.Common.StringHelper.Put( "ssharpultrachart{0}", GroupChartEx.InstanceCount( ) );
            }
            else
            {
                throw new NotImplementedException( );
            }            
        }
    }

    public bool IsAutoScroll
    {
        get
        {
            return GroupChartEx.IsAutoScroll;
        }
    }

    public bool IsAutoRange
    {
        get
        {
            return GroupChartEx.IsAutoRange;
        }
    }

    public IChart Chart
    {
        get
        {
            return Area.Chart;
        }
    }

    public ChartExViewModel GroupChartEx
    {
        get
        {
            return Chart as ChartExViewModel;
        }
    }

    //public Chart GroupChart
    //{
    //    get
    //    {
    //        return Chart as Chart;
    //    }
    //}

    public ChartArea Area
    {
        get
        {
            return _chartArea;
        }
    }

    // Tony: This name cannot change because it is in the decompile file
    public ChartViewModel ChartViewModel
    {
        get
        {
            return null;
        }
    }

    public ChartExViewModel ChartExViewModel
    {
        get
        {
            return Chart as ChartExViewModel;
        }
    }

    public LegendModifierVM LegendViewModel
    {
        get
        {
            if ( _legendViewModel != null )
            {
                return _legendViewModel;
            }

            _legendViewModel = new LegendModifierVM( this )
            {
                LegendModifier = new LegendModifierEx( )
            };

            LegendModifier.ViewModel = _legendViewModel;

            _legendViewModel.RemoveElmentEvent += OnRemoveElementEvent;
            return _legendViewModel;
        }
    }

    public FxAnnotationModifier AnnotationModifier
    {
        get
        {
            if ( _annotationModifier != null )
            {
                return _annotationModifier;
            }

            var annotation = new FxAnnotationModifier( Area, Annotations );
            annotation.IsEnabled = false;
            _annotationModifier = annotation;

            return annotation;
        }
    }

    public DataPointSelectionModifier PointSelectionModifier
    {
        get
        {
            if ( _dataPointSelector != null )
            {
                return _dataPointSelector;
            }

            var dpSelector             = new fxDataPointSelectionModifier( );
            dpSelector.IsEnabled       = true;
            dpSelector.SelectionFill   = new SolidColorBrush( System.Windows.Media.Color.FromArgb( 0xB1, 0xB5, 0xB2, 0xB2 ) );
            dpSelector.SelectionStroke = new SolidColorBrush( System.Windows.Media.Color.FromArgb( 0x00, 0x9E, 0x9C, 0x9C ) );
            _dataPointSelector         = dpSelector;

            return dpSelector;
        }
    }



    //public TradingAnnotationCreationModifier TradingModifer
    //{
    //    get
    //    {
    //        if ( _tradingModifier != null )
    //        {
    //            return _tradingModifier;
    //        }

    //        var tradingModifier = new TradingAnnotationCreationModifier( Area, Annotations );
    //        tradingModifier.IsEnabled = false;
    //        _annotationModifier = tradingModifier;

    //        return tradingModifier;

    //    }
    //}


    public ObservableCollection<IRenderableSeries> AdvanceChartRenderableSeries
    {
        get
        {
            return _advanceChartRenderableSeries;
        }

        set
        {
            _advanceChartRenderableSeries = value;

            NotifyChanged( "AdvanceChartRenderableSeries" );
        }
    }

    public void AddAxisMakerAnnotation( IChartComponent elementXY, IAnnotation axisMakerAnnotation, object axisMarker )
    {
        if ( axisMarker == null )
        {
            throw new ArgumentNullException( "key" );
        }

        PooledDictionary<object, IAnnotation> dictionary;
        if ( !_topChartElmentAnnotationMap.TryGetValue( elementXY, out dictionary ) )
        {
            _topChartElmentAnnotationMap[ elementXY ] = dictionary = new PooledDictionary<object, IAnnotation>( );
        }

        dictionary.Add( axisMarker, axisMakerAnnotation );
        OnXYAxisPropertyChanged( elementXY, new PropertyChangedEventArgs( "XAxis" ) );
    }

    public IAnnotation GetAxisMakerAnnotation( IChartComponent elementXY, object objAnnoPair )
    {
        PooledDictionary<object, IAnnotation> dict;
        if ( !_topChartElmentAnnotationMap.TryGetValue( elementXY, out dict ) )
        {
            return null;
        }

        return dict.TryGetValue( objAnnoPair );
    }

    public void RemoveAnnotation( IChartComponent elementXY, object objAnnoPair )
    {
        PooledDictionary<object, IAnnotation> rootAnnotation;

        if ( !_topChartElmentAnnotationMap.TryGetValue( elementXY, out rootAnnotation ) )
        {
            return;
        }

        if ( objAnnoPair == null )
        {
            foreach ( KeyValuePair<object, IAnnotation> keyValuePair in rootAnnotation )
            {
                _annotationCollection.Remove( keyValuePair.Value );
            }

            _topChartElmentAnnotationMap.Remove( elementXY );
        }
        else
        {
            IAnnotation annotation;
            if ( !rootAnnotation.TryGetValue( objAnnoPair, out annotation ) )
            {
                return;
            }

            _annotationCollection.Remove( annotation );
            rootAnnotation.Remove( objAnnoPair );
        }
    }

    //public void AddSeriesViewModelsToRoot( IElementWithXYAxes elementXY, IChartSeriesViewModel seriesViewModel )
    //{
    //    PooledList<IChartSeriesViewModel> chartSeriesViewModelList;

    //    if ( !_chartSeriesViewModelMap.TryGetValue( elementXY, out chartSeriesViewModelList ) )
    //    {
    //        _chartSeriesViewModelMap[ elementXY ] = chartSeriesViewModelList = new PooledList<IChartSeriesViewModel>( );
    //    }

    //    if ( !chartSeriesViewModelList.Contains( seriesViewModel ) )
    //    {
    //        chartSeriesViewModelList.Add( seriesViewModel );
    //    }

    //    OnXYAxisPropertyChanged( elementXY, new PropertyChangedEventArgs( "XAxis" ) );
    //}


    public AxisCollection XAxises
    {
        get
        {
            return _xAxises;
        }
    }

    public AxisCollection YAxises
    {
        get
        {
            return _yAxises;
        }
    }

    public AnnotationCollection Annotations
    {
        get
        {
            return _annotationCollection;
        }
        set
        {
            SetField( ref _annotationCollection, value, nameof( Annotations ) );
        }
    }

    public ModifierGroup ChartModifier
    {
        get
        {
            return _modifierGroup ?? ( _modifierGroup = new ModifierGroup( ) );
        }
        set
        {
            SetField( ref _modifierGroup, value, nameof( ChartModifier ) );
        }
    }

    public ViewportManager ViewportManager
    {
        get
        {
            return _viewPortManager;
        }
    }

    public LegendModifierEx LegendModifier
    {
        get
        {
            return LegendViewModel.LegendModifier;
        }
    }

    public string Title
    {
        get
        {
            return _title;
        }
        set
        {
            SetField( ref _title, value, nameof( Title ) );
        }
    }

    public string PerfStats
    {
        get
        {
            return _perfStats;
        }
        set
        {
            SetField( ref _perfStats, value, nameof( PerfStats ) );
        }
    }

    public double Height
    {
        get
        {
            return _height;
        }
        set
        {
            SetField( ref _height, value, nameof( Height ) );
        }
    }

    public bool ShowPerfStats
    {
        get
        {
            var groupChart = GroupChartEx;
            if ( groupChart == null )
            {
                return _showPerfStats;
            }

            return groupChart.ShowPerfStats;
        }
        set
        {
            if ( Chart is ChartExViewModel vm )
            {
                vm.ShowPerfStats = value;
                NotifyChanged( nameof( ShowPerfStats ) );
            }
            else
            {
                SetField( ref _showPerfStats, value, nameof( ShowPerfStats ) );
            }
        }
    }

    public TimeSpan AutoRangeInterval
    {
        get
        {
            TimeSpan output = TimeSpan.Zero;

            var vm = GroupChartEx;
            if ( vm == null )
            {
                // Tony: This is so fucked up. The UI is tied up big time because this value doesn't have default value and the timer keeps triggering without stopping.
                // and it took up all the CPU time as a result.
                // TRIPLE FUCK. Can't believe this.
                output = _autoRangeIntervalNoGroup;
            }
            else
            {
                output = vm.AutoRangeInterval;
            }

            if ( output == TimeSpan.Zero )
            {
                output = TimeSpan.FromSeconds( 1 );
            }

            return output;
        }
        set
        {
            if ( Chart is ChartExViewModel vm )
            {
                vm.AutoRangeInterval = value;
                NotifyChanged( nameof( AutoRangeInterval ) );
            }
            else
            {
                SetField( ref _autoRangeIntervalNoGroup, value, nameof( AutoRangeInterval ) );
            }
        }
    }

    public bool ShowLegend
    {
        get
        {
            var vm = ChartExViewModel;

            if ( vm == null )
            {
                return _showLegendNoParent;
            }

            return vm.ShowLegend;
        }
        set
        {
            if ( ChartExViewModel != null )
            {
                ChartExViewModel.ShowLegend = value;
                NotifyChanged( nameof( ShowLegend ) );
            }
            else
            {
                SetField( ref _showLegendNoParent, value, nameof( ShowLegend ) );
            }
        }
    }

    public bool ShowOverview
    {
        get
        {
            var vm = ChartExViewModel;
            if ( vm == null )
            {
                return _showOverviewNoParent;
            }

            return vm.ShowOverview;
        }

        set
        {
            if ( ChartExViewModel != null )
            {
                ChartExViewModel.ShowOverview = value;
                NotifyChanged( nameof( ShowOverview ) );
            }
            else
            {
                SetField( ref _showOverviewNoParent, value, nameof( ShowOverview ) );
            }
        }
    }

    public int MinimumRange
    {
        get
        {
            var parentViewModel = ChartExViewModel;
            if ( parentViewModel == null )
            {
                return _minimumRangeNoParent;
            }

            return parentViewModel.MinimumRange;
        }
        set
        {
            if ( ChartExViewModel != null )
            {
                ChartExViewModel.MinimumRange = value;
                NotifyChanged( nameof( MinimumRange ) );
            }
            else
            {
                SetField( ref _minimumRangeNoParent, value, nameof( MinimumRange ) );
            }
        }
    }

    private int _rightMarginBars;
    public int RightMarginBars
    {
        get
        {
            var parentViewModel = ChartExViewModel;
            if ( parentViewModel == null )
            {
                return _rightMarginBars;
            }

            return parentViewModel.RightMarginBars;
        }
        set
        {
            if ( ChartExViewModel != null )
            {
                ChartExViewModel.RightMarginBars = value;
                NotifyChanged( nameof( RightMarginBars ) );
            }
            else
            {
                SetField( ref _rightMarginBars, value, nameof( RightMarginBars ) );
            }
        }
    }

    public string SelectedTheme
    {
        get
        {
            string str;
            if ( ChartExViewModel == null )
            {
                str = null;
            }
            else
            {
                str = ChartExViewModel.SelectedTheme;

                if ( str != null )
                {
                    return str;
                }
            }
            return _selectThemeNoParent;
        }


        set
        {
            if ( ChartExViewModel != null )
            {
                ChartExViewModel.SelectedTheme = value;
                NotifyChanged( nameof( SelectedTheme ) );
            }
            else
            {
                SetField( ref _selectThemeNoParent, value, nameof( SelectedTheme ) );
            }
        }
    }

    public ICommand ResetAxisTimeZoneCommand
    {
        get
        {
            return _resetAxisTimeZoneCommand;
        }
        set
        {
            SetField( ref _resetAxisTimeZoneCommand, value, nameof( ResetAxisTimeZoneCommand ) );
        }
    }

    public ICommand ClosePaneCommand
    {
        get
        {
            return _closePaneCommand;
        }
        set
        {
            _closePaneCommand = value;
        }
    }

    public ICommand ShowHiddenAxesCommand
    {
        get
        {
            return _showHiddenAxesCommand;
        }
    }

    

    


    void IChildPane.ZoomExtents( )
    {
        throw new NotSupportedException( );
    }

    private void SetSelectedTheme( )
    {
        // Tony: Change the background to not have gradient color.       

        SelectedTheme = ApplicationThemeHelper.ApplicationThemeName.ToChartTheme( );
    }


    private void UpdateBackgroundColor( )
    {
        var sciChartName = SelectedTheme;

        if ( sciChartName == "Chrome" )
        {
            IThemeProvider tcp = SciChart.Charting.ThemeManager.GetThemeProvider( "Chrome" );

            System.Windows.Media.Brush brush = new SolidColorBrush( Colors.White );

            tcp.SciChartBackground = brush;

            if ( _sciChartSurface != null )
            {
                _sciChartSurface.Background = brush;
            }
        }
        else if ( sciChartName == "ExpressionDark" )
        {

            IThemeProvider tcp = SciChart.Charting.ThemeManager.GetThemeProvider( "ExpressionDark" );

            System.Windows.Media.Brush brush = new SolidColorBrush( System.Windows.Media.Color.FromRgb( 0x33, 0x33, 0x33 ) );

            tcp.SciChartBackground = brush;
            if ( _sciChartSurface != null )
            {
                _sciChartSurface.Background = brush;
            }
        }
    }

    public void Dispose( )
    {
        _sciChartSurface?.Dispose( );
        _sciChartSurface = null;
    }
    

    private void SetupModifiersAndBindingMVVM( ChartExViewModel chart )
    {
        SetupModifiers( );
        ChartModifier.ChildModifiers.AddRange( _chartModifiers );
        AnnotationModifier.SetBindings( FxAnnotationModifier.UserAnnotationTypeProperty, chart, "AnnotationType", BindingMode.TwoWay, null, null );

        var cursor = ChartModifier.ChildModifiers.OfType<UltrachartCursormodifier>( ).First( );
        var order  = ChartModifier.ChildModifiers.OfType<ChartOrderModifier>( ).First( );
        var zoom   = ChartModifier.ChildModifiers.OfType<fxZoomPanModifier>( ).First( );

        cursor.SetBindings( ChartModifierBase.IsEnabledProperty, Chart, "CrossHair", BindingMode.TwoWay, null, null );
        cursor.SetBindings( TooltipModifierBase.ShowAxisLabelsProperty, Chart, "CrossHairAxisLabels", BindingMode.TwoWay, null, null );
        cursor.SetBindings( UltrachartCursormodifier.InPlaceTooltipProperty, Chart, "CrossHairTooltip", BindingMode.TwoWay, null, null );

        order.SetBindings( ChartOrderModifier.CanCreateOrdersProperty, Chart, "OrderCreationMode", BindingMode.TwoWay, null, null );
        order.SetBindings( ChartOrderModifier.ShowHorizontalLineProperty, Chart, "CrossHair", BindingMode.OneWay, new InverseBooleanConverter( ), null );

        zoom.SetBindings( ChartModifierBase.IsEnabledProperty, Chart, "AnnotationType", BindingMode.OneWay, new EnumBooleanConverter( ), ChartAnnotationTypes.None.ToString( ) );

        MouseManager.SetMouseEventGroup( ChartModifier, PaneGroup );
    }



    private void OnRemoveElementEvent( IChartElement element )
    {
        ChartExViewModel?.InvokeRemoveElementEvent( element );
        RemoveElementEvent?.Invoke( element );
    }



    public void SetupAnnotation( IChartElement annotation, ChartDrawDataEx.sAnnotation data )
    {
        if ( _annotationModifier != null )
        {
            if ( annotation is AnnotationUI )
            {
                _annotationModifier.SetupAnnotation( ( AnnotationUI )annotation, data );
            }
        }
    }

    public void RemoveAnnotation( IChartElement annotation )
    {
        if ( _annotationModifier != null )
        {
            if ( annotation is AnnotationUI )
            {
                
                _annotationModifier.RemoveAnnotation( ( AnnotationUI )annotation );
            }
        }
    }

    public void InvokeMoveOrderEvent( Order order, Decimal newOrderPrice )
    {
        GroupChartEx?.InvokeMoveOrderEvent( order, newOrderPrice );

    }

    public void InvokeCancelOrderEvent( Order order )
    {
        GroupChartEx?.InvokeCancelOrderEvent( order );
    }

    public IEnumerable<Order> GetActiveOrders( Func<Order, bool> _param1 )
    {
        var parentVM = _vmChartUIs.CachedValues;

        var orders = parentVM.Where( p => p != null )
                             .SelectMany( x => 
                                             {
                                                return x.Elements.OfType<ChartActiveOrdersVM>( ).SelectMany( ao => ao.GetActiveOrders( _param1 ) );
                                             } 
                                        );

        return ( orders );
    }

    #region ----------------------------- Tony's new Implementation ----------------------------- 

    public void AddRenderableSeriesToChartSurface( IChartComponent elementXY, IRenderableSeries renderableSeries )
    {
        PooledList<IRenderableSeries> rSeriesList;

        if ( !_chartUIRSeries.TryGetValue( elementXY, out rSeriesList ) )
        {
            _chartUIRSeries[ elementXY ] = rSeriesList = new PooledList<IRenderableSeries>( );
        }

        if ( !rSeriesList.Contains( renderableSeries ) )
        {
            rSeriesList.Add( renderableSeries );
        }

        OnXYAxisPropertyChanged( elementXY, new PropertyChangedEventArgs( "XAxis" ) );
    }

    public void Remove( IChartComponent elementXY )
    {
        PooledList<IRenderableSeries> rSeriesList;

        if ( !_chartUIRSeries.TryGetValue( elementXY, out rSeriesList ) )
        {
            return;
        }

        foreach ( var rSerie in rSeriesList )
        {
            _advanceChartRenderableSeries.Remove( rSerie );
        }

        _chartUIRSeries.Remove( elementXY );
    }


    /// <summary>
    /// Handles the PropertyChanged event of the XYAxis element.
    ///     
    /// Directly copy the code from StockSharp.Charting.IChartExtensions because I still haven't change my namespace from fx.Charting to Stocksharp.xaml.Charting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnXYAxisPropertyChanged( object sender, PropertyChangedEventArgs e )
    {        
        IChartComponent elementXY = ( IChartComponent )sender;

        if ( e.PropertyName != "XAxis" && e.PropertyName != "YAxis" )
        {
            return;
        }

        IChart chart = Chart;

        if ( chart != null )
        {
            Ecng.Xaml.XamlHelper.EnsureUIThread( chart );
        }

        PooledList<IRenderableSeries> rSeriesList;

        ChartAxis xAxis = null;
        ChartAxis yAxis = null;

        if ( _chartUIRSeries.TryGetValue( elementXY, out rSeriesList ) )
        {
            // Directly used the code from StockSharp.Charting.IChartExtensions.TryGetXAxis
            xAxis = elementXY.CheckOnNull( nameof( elementXY ) ).ChartArea?.XAxises.FirstOrDefault( xa => xa.Id == elementXY.XAxisId );
            
            // Directly used the code from StockSharp.Charting.IChartExtensions.TryGetXAxis
            yAxis = elementXY.CheckOnNull( nameof( elementXY ) ).ChartArea?.YAxises.FirstOrDefault( xa => xa.Id == elementXY.YAxisId );

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

        PooledDictionary<object, IAnnotation> dictionary;

        if ( !_topChartElmentAnnotationMap.TryGetValue( elementXY, out dictionary ) )
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
        get { return _dataPointSelector.HighlightedBarLinuxTime; }
    }

    


    #endregion
}


