using DevExpress.Mvvm;
using fx.Algorithm;
using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Common.Extensions;
using SciChart.Charting.Model.ChartData;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals.Axes;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Core.Extensions;
using SciChart.Core.Utility.Mouse;
using SciChart.Data.Model;
using fx.Definitions;
using StockSharp.Xaml.Charting.HewFibonacci;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using fx.Bars;
using System.Windows.Threading;

namespace StockSharp.Xaml.Charting
{
    public class fxDataPointSelectionModifier : DataPointSelectionModifier
    {
        private DispatcherTimer _timer = new DispatcherTimer();
        bool _pressAndHold = false;

        public fxDataPointSelectionModifier( ) : base()
        {
            _dataPointInfoCollection = new ObservableCollection<DataPointInfo>( );

            _timer.Interval = TimeSpan.FromMilliseconds( 1000 );

            _timer.Tick += delegate
            {
                _timer.Stop();
                _pressAndHold = true;
            };
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
            }
        }

        bool _whenSelectionChange;
        private readonly PooledDictionary<string, IRange> _selectionRanges = new PooledDictionary<string, IRange>( );
        private ObservableCollection<DataPointInfo> _dataPointInfoCollection;
        private PooledList< long > _highlightedBarTime = new PooledList< long >( );

        bool _contrlPressed = false;

        fxHistoricBarsRepo _bars = null;
        
        HewManager _hewManager = null;

        public bool HasMultipleBarsHighlighted
        {
            get
            {
                return _dataPointInfoCollection.Count > 1;
            }
        }

        
        public bool SendMessageWhenSelectionChange
        {
            get { return _whenSelectionChange; }
            set
            {
                if ( _whenSelectionChange == value )
                    return;
                _whenSelectionChange = value;
                OnPropertyChanged( nameof( SendMessageWhenSelectionChange ) );
            }
        }
        

        public PooledList<long> HighlightedBarLinuxTime
        {
            get { return _highlightedBarTime; }
        }

        //
        // Summary:
        //     When overriden in derived classes, is used to override the default selection
        //     behavior.
        //
        // Parameters:
        //   modifierKey:
        //     The modifier key which has been pressed.
        //
        //   isAreaSelection:
        //     True when selection was performed by dragging a reticule, othetwise False.
        protected override SelectionMode GetSelectionMode( MouseModifier modifierKey, bool isAreaSelection )
        {

            return SelectionMode.Replace;
        }
        //
        // Summary:
        //     Enumerates RenderableSeries on the parent SciChart.Charting.ChartModifiers.ChartModifierBase.ParentSurface
        //     and gets SciChart.Charting.Model.ChartData.SeriesInfo objects in given point.
        //
        // Parameters:
        //   point:        
        //protected override IEnumerable<SeriesInfo> GetSeriesInfoAt( Point point )
        //{

        //}
        //
        // Summary:
        //     When overridden in derived classes, indicates whether mouse point is valid for
        //     current modifier
        //
        // Parameters:
        //   point:
        //protected virtual bool IsEnabledAt( Point point );
        //
        // Summary:
        //     When overridden in derived classes, indicates whether SciChart.Charting.Visuals.RenderableSeries.HitTestInfo
        //     result of hit-test should be returned from the SciChart.Charting.ChartModifiers.DataPointSelectionModifier.GetSeriesInfoAt(System.Windows.Point)
        //     method.
        //protected virtual bool IsHitPointValid( HitTestInfo hitTestInfo );
        //
        // Summary:
        //     When overridden in derived classes, indicates whether the series should be inspected
        //     in order to get SciChart.Charting.Model.ChartData.SeriesInfo inside the SciChart.Charting.ChartModifiers.DataPointSelectionModifier.GetSeriesInfoAt(System.Windows.Point)
        //     method.
        //
        // Parameters:
        //   series:
        //protected virtual bool IsSeriesValid( IRenderableSeries series );
        //
        // Summary:
        //     When overriden in derived classes, called to raise the SciChart.Charting.ChartModifiers.DataPointSelectionModifier.SelectionChanged
        //     event.
        //protected void OnSelectionChanged( );
        //
        // Summary:
        //     When overriden in derived classes, called to perform selection of all the PointMarkers
        //     located inside the rectangle, formed by startPoint and endPoint.
        //
        // Parameters:
        //   endPoint:
        //     he mouse point where selection ended, translated relative to SciChart.Charting.ChartModifiers.ApiElementBase.ModifierSurface.
        //
        //   startPoint:
        //     The mouse point where selection started, translated relative to SciChart.Charting.ChartModifiers.ApiElementBase.ModifierSurface.
        //
        //   selectionMode:
        //     Determines selection behavior.

        public override void OnModifierTouchUp( ModifierTouchArgs e )
        {
            base.OnModifierTouchUp( e );

            _timer.Stop();

            if ( _pressAndHold )
            {
                var curPt = e.TouchPoints.First().Position;
                var src   = Enumerable.Empty<SeriesInfo>( );

                src       = GetSeriesInfoAt( curPt );

                fxDeselectAllPointMarkers();

                int lastIndex = -1;
                IRenderableSeries lastRSerie = null;

                foreach ( SeriesInfo seriesInfo in src )
                {
                    lastIndex = seriesInfo.DataSeriesIndex;
                    lastRSerie = seriesInfo.RenderableSeries;
                    SetSelected( lastIndex, lastRSerie );
                }

                SendMessageForSelectionChange( lastIndex, lastRSerie );

                ParentSurface.InvalidateElement();
                OnPropertyChanged( "SelectedPointMarkers" );
                OnSelectionChanged();
            }
        }

        protected override void SelectSinglePoint( Point mousePoint, SelectionMode selectionMode )
        {
            IEnumerable<SeriesInfo> source = Enumerable.Empty<SeriesInfo>( );
            bool isEnabledAtMoustPt;
            bool isInverseOrUnion = ( isEnabledAtMoustPt = IsEnabledAt( mousePoint ) ) && selectionMode != SelectionMode.Replace;

            if ( isEnabledAtMoustPt )
            {
                source = GetSeriesInfoAt( mousePoint );
                isInverseOrUnion = isInverseOrUnion && source.Any( );
            }

            if ( !isInverseOrUnion || selectionMode == SelectionMode.Union )
            {
                source = source.Where( s =>
                {
                    if ( s.PointMetadata != null )
                    {
                        return !s.PointMetadata.IsSelected;
                    }
                    return false;
                } );
            }

            if ( !isInverseOrUnion )
            {
                fxDeselectAllPointMarkers( );
            }

            int lastIndex = -1;
            IRenderableSeries lastRSerie = null;

            foreach ( SeriesInfo seriesInfo in source )
            {
                lastIndex = seriesInfo.DataSeriesIndex;
                lastRSerie = seriesInfo.RenderableSeries;
                SetSelected( lastIndex, lastRSerie );
            }

            SendMessageForSelectionChange( lastIndex, lastRSerie );

            ParentSurface.InvalidateElement( );            
            OnPropertyChanged( "SelectedPointMarkers" );
            OnSelectionChanged( );
        }

        

        protected override void SelectManyPoints( Point startPoint, Point endPoint, SelectionMode selectionMode )
        {
            if ( selectionMode == SelectionMode.Replace )
            {
                fxDeselectAllPointMarkers( );
            }            

            var xaxis       = GetXAxis( XAxisId );
            var source      = ParentSurface.RenderableSeries.Where( new Func<IRenderableSeries, bool>( IsSeriesValid ) );
            bool isVertical = source.Any( ) && source.FirstOrDefault( ).CurrentRenderPassData.IsVerticalChart;

            startPoint      = fxPointHelper.Swap( startPoint, isVertical );
            endPoint        = fxPointHelper.Swap( endPoint, isVertical );
            var selectionX  = GetSelectionRange( startPoint, endPoint, xaxis.ToEnumerable( ) ).Single( ).Value;
            var selectionY  = GetSelectionRange( startPoint, endPoint, YAxes );

            

            foreach ( IRenderableSeries rSeries in source )
            {
                if ( _contrlPressed )
                {
                    SelectBars( selectionMode, selectionX, selectionY, rSeries );
                }
                else
                {
                    SelectWaves( selectionMode, selectionX, selectionY, rSeries );
                }                
            }

            var annotations = ParentSurface.Annotations;            

            foreach (var anno in annotations)
            {
                if ( anno is IfxImportantLevel )
                {
                    var fibAnno = ( IfxImportantLevel )anno;
                    fibAnno.HighlightedSelected( _contrlPressed, selectionX, selectionY );
                }                
            }



            ParentSurface.InvalidateElement( );
            //_hasSelections = true;
            OnSelectionChanged( );
            OnPropertyChanged( "SelectedPointMarkers" );

            
        }

        private void SelectBars( SelectionMode selectionMode, IRange selectionX, PooledDictionary<string, IRange> selectionY, IRenderableSeries rSeries )
        {
            IRange yRange = selectionY[ rSeries.YAxisId ];
            var selectionYMax = yRange.Max.ToDouble( );
            var selectionYMin = yRange.Min.ToDouble( );
            var selectionXMin = selectionX.Min.ToDouble( );
            var selectionXMax = selectionX.Max.ToDouble( );

            IDataSeries dataSeries = rSeries.DataSeries;
            IndexRange indicesRange = dataSeries.GetIndicesRange( selectionX, SearchMode.RoundDown, SearchMode.RoundUp );
            
            if ( indicesRange.IsDefined )
            {
                for ( int i = indicesRange.Min; i <= indicesRange.Max; ++i )
                {
                    IPointMetadata pointMetadata = dataSeries.Metadata[ i ];

                    bool isSelected = pointMetadata.IsSelected;
                    bool withinRange = false;

                    double yVal         = ( ( IComparable )dataSeries.YValues[ i ] ).ToDouble( );
                    double xVal         = ( ( IComparable )dataSeries.XValues[ i ] ).ToDouble( );

                    withinRange = ( ( yVal > selectionYMin ) && ( yVal <= selectionYMax ) &&
                                    ( xVal > selectionXMin ) && ( xVal <= selectionXMax ) );

                    if ( withinRange && ( selectionMode == SelectionMode.Union && !isSelected || ( selectionMode == SelectionMode.Inverse || selectionMode == SelectionMode.Replace ) ) )
                    {
                        SetSelected( i, rSeries );
                    }
                }
            }
        }

        private void SelectWaves( SelectionMode selectionMode, IRange selectionX, PooledDictionary<string, IRange> selectionY, IRenderableSeries rSeries )
        {
            IRange yRange = selectionY[ rSeries.YAxisId ];
            var selectionYMax = yRange.Max.ToDouble( );
            var selectionYMin = yRange.Min.ToDouble( );
            var selectionXMin = selectionX.Min.ToDouble( );
            var selectionXMax = selectionX.Max.ToDouble( );

            IDataSeries dataSeries = rSeries.DataSeries;
            IndexRange indicesRange = dataSeries.GetIndicesRange( selectionX, SearchMode.RoundDown, SearchMode.RoundUp );

            //if ( !indicesRange.IsDefined && rSeries is StackedColumnRenderableSeries )
            //{
            //    Point point1 = fxPointHelper.Swap( startPoint, isVertical );
            //    Point point2 = fxPointHelper.Swap( endPoint, isVertical );
            //    double x = ( point1.X + point2.X ) / 2.0;
            //    double y = ( point1.Y + point2.Y ) / 2.0;

            //    var hitTestProvider = rSeries.HitTestProvider;

            //    HitTestInfo hitTestInfo = hitTestProvider.HitTest( new Point( x, y ), false );
            //    indicesRange.Min = indicesRange.Max = hitTestInfo.DataSeriesIndex;
            //}

            if ( indicesRange.IsDefined )
            {
                bool foundWave = false;

                for ( int i = indicesRange.Min; i <= indicesRange.Max; ++i )
                {
                    IPointMetadata pointMetadata = dataSeries.Metadata[ i ];

                    SBar tmpBar;

                    if ( pointMetadata != null )
                    {
                        if ( _bars == null )
                        {
                            tmpBar = ( SBar ) pointMetadata;

                            _bars = SymbolsMgr.Instance.GetDatabarRepo( tmpBar.SymbolEx, tmpBar.SymbolEx.Period );
                            var aa      = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( tmpBar.SymbolEx );
                            _hewManager = ( HewManager ) aa.HewManager;
                        }
                        else
                        {
                            tmpBar = ( SBar ) pointMetadata;
                        }

                        ref SBar bar = ref _bars.GetBarByIndex( tmpBar.BarIndex );

                        if ( !bar.HasElliottWave )
                            continue;

                        ref var hew = ref bar.GetWaveFromScenario( _waveScenarioNo );

                        if ( hew == AdvBarInfo.EmptyHew )
                        {
                            continue;
                        }

                        foundWave = true;

                        bool isSelected = pointMetadata.IsSelected;
                        bool withinRange = false;

                        double yVal         = ( ( IComparable )dataSeries.YValues[ i ] ).ToDouble( );
                        double xVal         = ( ( IComparable )dataSeries.XValues[ i ] ).ToDouble( );
                        bool notFastImpulse = !( rSeries is FastImpulseRenderableSeries );
                        bool isFastBubble   = rSeries is FastBubbleRenderableSeries;

                        withinRange = ( ( yVal > selectionYMin ) && ( yVal <= selectionYMax ) &&
                                        ( xVal > selectionXMin ) && ( xVal <= selectionXMax ) );

                        if ( withinRange && ( selectionMode == SelectionMode.Union && !isSelected || ( selectionMode == SelectionMode.Inverse || selectionMode == SelectionMode.Replace ) ) )
                        {
                            SetSelected( i, rSeries );
                        }
                    }
                }

                int largestWaveImpt  = -1;
                int largestWaveIndex = -1;

                if ( !foundWave )
                {
                    SBar tmpBar;

                    for ( int i = indicesRange.Min; i <= indicesRange.Max; ++i )
                    {
                        var pointMetadata = dataSeries.Metadata[ i ];
                        if ( pointMetadata != null )
                        {
                            if ( _bars == null )
                            {
                                tmpBar = ( SBar ) pointMetadata;
                                _bars = SymbolsMgr.Instance.GetDatabarRepo( tmpBar.SymbolEx, tmpBar.SymbolEx.Period );
                                var aa      = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( tmpBar.SymbolEx );
                                _hewManager = ( HewManager ) aa.HewManager;
                            }
                            else
                            {
                                tmpBar = ( SBar ) pointMetadata;
                            }

                            ref SBar bar = ref _bars.GetBarByIndex( tmpBar.BarIndex );

                            if ( bar.High < selectionYMax && bar.Low > selectionYMin )
                            {
                                var waveImpt = _hewManager.GetWaveImportance( bar.BarPeriod, bar.LinuxTime );

                                if ( waveImpt > largestWaveImpt )
                                {
                                    largestWaveImpt = waveImpt;
                                    largestWaveIndex = i;
                                }
                            }
                        }
                    }

                    if ( largestWaveIndex > -1 )
                    {
                        SetSelected( largestWaveIndex, rSeries );
                    }
                }
            }
        }



        private void fxDeselectAllPointMarkers( )
        {
            foreach ( DataPointInfo pt in _dataPointInfoCollection )
            {
                if ( pt.RenderableSeries != null && pt.DataPointIndex < pt.RenderableSeries.DataSeries.Count && pt.DataPointMetadata != null )
                {
                    var tmpbar = ( SBar ) pt.RenderableSeries.DataSeries.Metadata[ pt.DataPointIndex ];

                    ref SBar bar = ref _bars.GetBarByIndex( tmpbar.BarIndex );
                    bar.IsSelected = false;
                    pt.RenderableSeries.DataSeries.Metadata[ pt.DataPointIndex ] = bar;
                }
            }

            _dataPointInfoCollection.Clear( );

            foreach ( DataPointInfo pt in SelectedPointMarkers )
            {
                if ( pt.RenderableSeries != null && pt.DataPointIndex < pt.RenderableSeries.DataSeries.Count && pt.DataPointMetadata != null )
                {
                    var tmpbar = ( SBar ) pt.RenderableSeries.DataSeries.Metadata[ pt.DataPointIndex ];
                    ref SBar bar = ref _bars.GetBarByIndex( tmpbar.BarIndex );
                    bar.IsSelected = false;
                    pt.RenderableSeries.DataSeries.Metadata[ pt.DataPointIndex ] = bar;
                }
            }
            SelectedPointMarkers.Clear( );
            _highlightedBarTime.Clear( );
        }

        private void SetSelected( int index, IRenderableSeries rSeries )
        {
            IPointMetadata pointMetadata = rSeries.DataSeries.Metadata[ index ];
            if ( pointMetadata == null )
            {
                return;
            }

            SBar tmpBar;

            if ( _bars == null )
            {
                tmpBar      = ( SBar ) pointMetadata;

                _bars    = SymbolsMgr.Instance.GetDatabarRepo( tmpBar.SymbolEx, tmpBar.SymbolEx.Period );
                var aa      = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( tmpBar.SymbolEx );
                _hewManager = ( HewManager ) aa.HewManager;
            }
            else
            {
                tmpBar = ( SBar ) pointMetadata;
            }

            ref SBar bar = ref _bars.GetBarByIndex( tmpBar.BarIndex );

            bar.SelectionChanged = true;
            _highlightedBarTime.Add( bar.LinuxTime );

            bar.IsSelected = true;            

            _dataPointInfoCollection.Add( new DataPointInfo( index, rSeries ) );
        }

        private void SendMessageForSelectionChange( int index, IRenderableSeries rSeries )
        {
            if ( SendMessageWhenSelectionChange )
            {
                IPointMetadata pointMetadata = rSeries.DataSeries.Metadata[ index ];
                if ( pointMetadata == null )
                {
                    return;
                }

                SBar tmpBar;

                if ( _bars == null )
                {
                    tmpBar = ( SBar ) pointMetadata;

                    _bars = SymbolsMgr.Instance.GetDatabarRepo( tmpBar.SymbolEx, tmpBar.SymbolEx.Period );
                }
                else
                {
                    tmpBar = ( SBar ) pointMetadata;
                }

                ref SBar bar = ref _bars.GetBarByIndex( tmpBar.BarIndex );

                Messenger.Default.Send( new BackResearchUpdateMessage( ref bar ) );
            }
        }

        private PooledDictionary<string, IRange> GetSelectionRange( Point start, Point end, IEnumerable<IAxis> axes )
        {
            _selectionRanges.Clear( );

            foreach ( IAxis axis in axes )
            {
                bool isXaxis = axis.IsXAxis;
                IComparable min = axis.GetDataValue( isXaxis ? start.X : start.Y );
                IComparable max = axis.GetDataValue( isXaxis ? end.X : end.Y );
                if ( min.CompareTo( max ) > 0 )
                {
                    IComparable comparable = min;
                    min = max;
                    max = comparable;
                }
                if ( isXaxis )
                {
                    IRange range = NewDateTimeRange( axis.VisibleRange, min, max );
                    _selectionRanges.Add( axis.Id, range );
                }
                else
                {
                    IRange range = NewDoubleRange( axis.VisibleRange, min, max );
                    _selectionRanges.Add( axis.Id, range );
                }
                
            }
            return _selectionRanges;
        }

        public static IRange NewDoubleRange( IRange originalRange, IComparable min, IComparable max )
        {            
            IRange range = ( IRange )originalRange.Clone( );
            range.SetMinMax( min.ToDouble( ), max.ToDouble( ) );

            return range;
        }

        public static IRange NewDateTimeRange( IRange originalRange, IComparable min, IComparable max )
        {
            IRange range = RangeFactory.NewRange( min, max );            

            return range;
        }

        public override void OnModifierKeyDown( ModifierKeyArgs e )
        {            
            if ( e.Key == Key.LeftCtrl )
            {
                _contrlPressed = true;
            }
            base.OnModifierKeyDown( e );
        }

        public override void OnModifierKeyUp( ModifierKeyArgs e )
        {
            if ( e.Key == Key.LeftCtrl  )
            {
                _contrlPressed = false;
            }
            base.OnModifierKeyUp( e );
        }

        private Point? _startPoint;

        public override void OnModifierTouchManipulationStarted( ModifierManipulationStartedArgs e )
        {
            base.OnModifierTouchManipulationStarted( e );
            _startPoint = e.ManipulationOrigin;
        }


        public override void OnModifierTouchDown( ModifierTouchArgs e )
        {
            base.OnModifierTouchDown( e );

            _pressAndHold = false;
            _timer.Start();
        }

        public override void OnModifierTouchManipulationDelta( ModifierManipulationDeltaArgs e )
        {
            var fingerCount = e.Manipulators.Count( );

            if ( fingerCount >= 2 )
            {
            }
            else
            {
                var curPt = e.Manipulators.First().GetPosition( ParentSurface.ModifierSurface as IInputElement );

                var xDiff = Math.Abs( curPt.X - _startPoint.Value.X );
                var yDiff = Math.Abs( curPt.Y - _startPoint.Value.Y );

                if ( xDiff > 3 || yDiff > 3 )
                {
                    _pressAndHold = false;
                    _timer.Stop();
                }
            }
        }

        public override void OnModifierTouchManipulationCompleted( ModifierManipulationCompletedArgs e )
        {
            base.OnModifierTouchManipulationCompleted( e );
        }

        
    }
}
