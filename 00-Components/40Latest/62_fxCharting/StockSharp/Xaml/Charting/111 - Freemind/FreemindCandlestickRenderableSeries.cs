using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals.PaletteProviders;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Data.Model;
using SciChart.Drawing.Common;
using fx.Definitions;
using StockSharp.Xaml.Charting.ATony;
using StockSharp.Xaml.Charting.Definitions;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using fx.Algorithm;
using SciChart.Charting.Visuals.PointMarkers;
using SciChart.Core.Extensions;
using System.Windows.Controls;
using StockSharp.BusinessEntities;
using StockSharp.Xaml.Charting.HewFibonacci;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Charting.Numerics.CoordinateCalculators;
using fx.Indicators;
using fx.Common;
using StockSharp.Xaml.Charting.CustomAnnotations;
using Vector = fx.Algorithm.Vector;
using fx.DefinitionsWnd;
using fx.Bars;


#pragma warning disable 414

namespace StockSharp.Xaml.Charting
{
    public partial class FreemindCandlestickRenderableSeries : FastCandlestickRenderableSeries
    {
        public FreemindCandlestickRenderableSeries()
        {
            DefaultStyleKey = typeof( FreemindCandlestickRenderableSeries );
            SetCurrentValue( FastOhlcRenderableSeries.DataPointWidthProperty, 1.0 );

            SetDrawingFucntion();
        }

        public void ClearConfluenceVariables( )
        {
            _confluencelevels.Clear( );

            _selectedAnnotation.Clear( );
            _lockedFibonacciAnnotations.Clear( );
            _lockedPPandFibObjects.Clear( );
            _lockedAllIfxImportantLines.Clear( );

            _lockedImptLinesDirection = TrendDirection.NoTrend;
            _earliestLockedLevel      = DateTime.MaxValue;
            _extremumBarIndex         = default;
            _extremumValue            = 0;
        }


        public bool CanUpdate
        {
            get { return _canUpdate; }
            set
            {
                if ( _canUpdate == value )
                {
                    return;
                }

                _canUpdate = value;

                if ( _canUpdate == false )
                {
                    HiddenAllFibonacciTargets( );
                }
                else
                {
                    ShowAllFibonacciTargets( );
                }

                if ( _canUpdate )
                {
                    OnInvalidateParentSurface( );
                }
            }
        }


        

        protected override void InternalDraw( IRenderContext2D renderContext, IRenderPassData renderPassData )
        {
            AssertPointSeriesType<OhlcPointSeries>( "OhlcDataSeries" );
            using ( PenManager penManager = new PenManager( renderContext, AntiAliasing, StrokeThickness, Opacity, null ) )
            {
                using ( _upWickPen = penManager.GetPen( StrokeUp ) )
                {
                    using ( _downWickPen = penManager.GetPen( StrokeDown ) )
                    {
                        _upBodyBrush   = CreateBrush( renderContext, FillUp );
                        _downBodyBrush = CreateBrush( renderContext, FillDown );

                        renderContext.DisposeResourceAfterDraw( _upBodyBrush );
                        renderContext.DisposeResourceAfterDraw( _downBodyBrush );

                        renderContext.SetPrimitivesCachingEnabled( true );

                        _candleWidth = GetDatapointWidth( renderPassData.XCoordinateCalculator, renderPassData.PointSeries, DataPointWidth );
                        _candleWidth = _candleWidth <= 1 || _candleWidth % 2 != 0 ? _candleWidth - 1 : _candleWidth;

                        if ( _needToDeleteWaves )
                        {
                            RemoveAllEWaves();
                        }

                        if ( _candleWidth < StrokeThickness )
                        {
                            _reducedViewFunction?.Invoke( renderContext, renderPassData, PaletteProvider as IStrokePaletteProvider, penManager );
                        }
                        else
                        {
                            _vanillaViewFunction?.Invoke( renderContext, renderPassData, PaletteProvider as IStrokePaletteProvider, PaletteProvider as IFillPaletteProvider, penManager );
                        }

                        //renderContext.SetPrimitivesCachingEnabled( false );
                    }
                }
            }
        }

        private IBrush2D CreateBrush( IRenderContext2D renderContext, Brush brush )
        {
            SolidColorBrush solidColorBrush = brush as SolidColorBrush;
            return solidColorBrush != null ? renderContext.CreateBrush( solidColorBrush.Color, Opacity, new bool?( ) ) : renderContext.CreateBrush( brush, Opacity, TextureMappingMode.PerPrimitive );
        }

        private IPen2D GetPenColor( bool rising, ref SBar realBar, ref PeriodXTaManager taManager, IStrokePaletteProvider strokePaletteProvider, IPenManager penManager, int index )
        {
            var pointPen = rising ? _upWickPen : _downWickPen;

            if ( ShowHewDetection && realBar != SBar.EmptySBar )
            {
                if ( taManager == null )
                {
                    var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _symbol );
                    
                    if ( aa != null )
                    {
                        taManager = (PeriodXTaManager ) aa.GetPeriodXTa( realBar.BarPeriod );
                    }

                    if( taManager == null )
                        return null;
                }

                RangeEx<DateTime> timeBlock = null;

                if ( timeBlock == null )
                {
                    if ( taManager.IsImpulsiveWave( realBar.BarTime, out timeBlock ) )
                    {
                        pointPen = penManager.GetPen( Color.FromRgb( 0, 255, 0 ) );
                    }
                }
                else
                {
                    if ( !timeBlock.Contains( realBar.BarTime ) )
                    {
                        timeBlock = null;
                    }
                    else
                    {
                        pointPen = penManager.GetPen( Color.FromRgb( 0, 255, 0 ) );
                    }
                }
            }
            else
            {
                if ( strokePaletteProvider != null )
                {
                    Color? stroke = null;

                    if ( realBar != SBar.EmptySBar && realBar.HasCandleStickPattern && ShowCandlePattern )
                    {
                        stroke = realBar.IsSelected ? strokePaletteProvider.OverrideStrokeColor( this, index, realBar ) : realBar.GetCandleStickPatternColor( true );
                    }
                    else
                    {
                        stroke = strokePaletteProvider.OverrideStrokeColor( this, index, realBar );
                    }

                    pointPen = stroke.HasValue ? penManager.GetPen( new Color?( stroke.Value ) ) : pointPen;
                }
            }

            return pointPen;
        }

        private IPen2D GetPenColorBareBone( bool rising, ref SBar realBar, ref PeriodXTaManager taManager, IStrokePaletteProvider strokePaletteProvider, IPenManager penManager, int index )
        {
            var pointPen = rising ? _upWickPen : _downWickPen;

            if ( ShowHewDetection && realBar != SBar.EmptySBar )
            {
                if ( taManager == null )
                {
                    var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _symbol );

                    if ( aa != null )
                    {
                        taManager = ( PeriodXTaManager ) aa.GetPeriodXTa( realBar.BarPeriod );
                    }

                    if ( taManager == null )
                        return null;
                }

                RangeEx<DateTime> timeBlock = null;

                if ( timeBlock == null )
                {
                    if ( taManager.IsImpulsiveWave( realBar.BarTime, out timeBlock ) )
                    {
                        pointPen = penManager.GetPen( Color.FromRgb( 0, 255, 0 ) );
                    }
                }
                else
                {
                    if ( !timeBlock.Contains( realBar.BarTime ) )
                    {
                        timeBlock = null;
                    }
                    else
                    {
                        pointPen = penManager.GetPen( Color.FromRgb( 0, 255, 0 ) );
                    }
                }
            }
            else
            {
                if ( strokePaletteProvider != null )
                {
                    Color? stroke = null;

                    if ( realBar != SBar.EmptySBar && realBar.HasCandleStickPattern && ShowCandlePattern )
                    {
                        stroke = realBar.IsSelected ? strokePaletteProvider.OverrideStrokeColor( this, index, realBar ) : realBar.GetCandleStickPatternColor( true );
                    }
                    else
                    {
                        stroke = strokePaletteProvider.OverrideStrokeColor( this, index, realBar );
                    }

                    pointPen = stroke.HasValue ? penManager.GetPen( new Color?( stroke.Value ) ) : pointPen;
                }
            }

            return pointPen;
        }

        

        

        
        private void UpdateFibonnaciLastX( float high, float low )
        {
            /* --------------------------------------------------------------------------------------------------
            * 
            * Since we have locked pivots and Fib levels, we want to see where Support and Resistance will be
            * 
            * --------------------------------------------------------------------------------------------------
            * */
            if ( _lockedPPandFibObjects.Count > 0 )
            {                
                (SBar bar, double value)  result = GetExtremumValues( _lockedImptLinesDirection );

                /* --------------------------------------------------------------------------------------------------
                * 
                * We have a new high or new low
                * 
                * --------------------------------------------------------------------------------------------------
                * */
                if ( result.bar != _extremumBarIndex )
                {
                    UpdateAllImptLinesBrokenStatus( _lockedImptLinesDirection );
                }

                foreach ( IfxImportantLevel lockedAnnotation in _lockedPPandFibObjects )
                {
                    lockedAnnotation.DimAllImportantLines( );
                }
                
                var closetLvl = HighlightedConfluenceClosestToExtremum( _lockedImptLinesDirection );

                if ( closetLvl != default )
                {
                    HighlightedConfluenceSecondClosestToExtremum( closetLvl, _lockedImptLinesDirection );
                }

                /* --------------------------------------------------------------------------------------------------
                * 
                * Here I want to show the confluent Cluster close to lastBar
                * 
                * --------------------------------------------------------------------------------------------------
                */

                HighlightedConfluenceClosestToLastBar( high, low, _lockedImptLinesDirection );
            }
            else
            {
                /* --------------------------------------------------------------------------------------------------
                * 
                * Here I would still like to when price is approaching Pivot Points.
                * 
                * --------------------------------------------------------------------------------------------------
                * */

            }


            GetParentSurface( ).InvalidateElement( );
        }

        private void RemoveWavesFromChart( ref SBar barData )
        {
            var waveTop    = new WaveAnnotationKey( barData.LinuxTime, WaveLabelPosition.TOP );
            var waveBottom = new WaveAnnotationKey( barData.LinuxTime, WaveLabelPosition.BOTTOM );

            var scichart   = GetParentSurface( );

            if ( _waveAnnotationAdded.ContainsKey( waveBottom ) )
            {
                var removedList = _waveAnnotationAdded.GetOrAddValueRef( waveBottom );

                if ( barData.WaveDirty == WaveDirtyEnum.DeleteAll || barData.WaveDirty == WaveDirtyEnum.Change || barData.WaveDirty == WaveDirtyEnum.DeleteSingle || _needToRedrawElliottWave )
                {
                    foreach ( var removed in removedList )
                    {
                        scichart.Annotations.Remove( removed );
                    }

                    _waveAnnotationAdded.Remove( waveBottom );
                    RemoveAllFibonacciTargets( );
                }                
            }

            if ( _waveAnnotationAdded.ContainsKey( waveTop ) )
            {
                var removedList = _waveAnnotationAdded.GetOrAddValueRef( waveTop );
                if ( barData.WaveDirty == WaveDirtyEnum.DeleteAll || barData.WaveDirty == WaveDirtyEnum.Change || barData.WaveDirty == WaveDirtyEnum.DeleteSingle || _needToRedrawElliottWave )
                {
                    foreach ( var removed in removedList )
                    {
                        scichart.Annotations.Remove( removed );
                    }

                    _waveAnnotationAdded.Remove( waveTop );
                    RemoveAllFibonacciTargets( );
                }                
            }
        }

        public long SelectedCandleBarTime
        {
            get
            {
                return _lastSelectedBarTime;                
            }

            set
            {
                _lastSelectedBarTime = value;
            }
        }

        public void ShowLessWaves()
        {
            _needToRedrawElliottWave = true;

            if ( _minimumToShow == ElliottWaveCycle.UNKNOWN )
            {
                _minimumToShow = _lowestWaveDeg + GlobalConstants.OneWaveCycle;
            }
            else
            {
                _minimumToShow += GlobalConstants.OneWaveCycle;                
            }

            if ( _minimumToShow > _higestWaveDeg )
            {
                _minimumToShow = _higestWaveDeg;
            }


        }

        public void ShowMoreWaves( )
        {
            _needToRedrawElliottWave = true;

            if ( _minimumToShow == ElliottWaveCycle.UNKNOWN )
            {
                _minimumToShow = _lowestWaveDeg;
            }
            else
            {
                _minimumToShow -= GlobalConstants.OneWaveCycle;
            }

            if ( _minimumToShow < ElliottWaveCycle.Miniscule )
            {
                _minimumToShow = ElliottWaveCycle.Miniscule;
            }

        }

        public void LockFibLevelsObject( )
        {
            if ( _selectedAnnotation.Count > 0 )
            {
                foreach ( var annotation in _selectedAnnotation )
                {
                    annotation.IsLocked = true;

                    if ( !_lockedFibonacciAnnotations.Contains( annotation ) )
                    {
                        _lockedFibonacciAnnotations.Add( annotation );
                    }
                }
            }
            else
            {
                var scichart = GetParentSurface( );

                var selected = SelectedCandleBarTime;

                foreach ( var a in scichart.Annotations )
                {
                    if ( a is IfxFibonacciAnnotation )
                    {
                        var fibLine = ( IfxFibonacciAnnotation ) a;

                        var fibTarget = fibLine.FibTarget;

                        if ( fibTarget != null )
                        {
                            var owningBarTime = fibTarget.OwingBarLinuxTime;

                            if ( owningBarTime > -1 && owningBarTime == selected )
                            {
                                if ( a is IfxImportantLevel )
                                {
                                    var imptLine = ( IfxImportantLevel ) a;

                                    imptLine.IsLocked = true;
                                }
                            }
                        }
                    }
                }
            }
                       

            FindConfluences4SRLevels( _symbol.PriceStep );
        }        

        public void DeleteAllLockFibLevels( )
        {
            var annotations = GetParentSurface( ).Annotations;

            PooledList<IfxFibonacciAnnotation> removed = new PooledList<IfxFibonacciAnnotation>( );

            foreach ( var annotation in annotations )
            {
                if ( annotation is IfxImportantLevel )
                {
                    var impt = ( IfxImportantLevel )annotation;
                    impt.DimAllImportantLines( );
                }

                if ( annotation is IfxFibonacciAnnotation )
                {
                    var a = ( IfxFibonacciAnnotation )annotation;

                    removed.Add( ( IfxFibonacciAnnotation )annotation );

                    _lockedPPandFibObjects.Remove( ( IfxImportantLevel) a );
                }
            }

            foreach ( IfxFibonacciAnnotation item in removed )
            {
                annotations.Remove( ( IAnnotation )item );
            }

            ClearConfluenceVariables( );

            _elliottWaveFibCount = annotations.Count;
        }

        public void ResetUI( )
        {
            var annotations = GetParentSurface( ).Annotations;

            PooledList< IAnnotation > removed = new PooledList<IAnnotation>( );

            foreach ( var annotation in annotations )
            {
                removed.Add( annotation );
            }

            foreach ( var item in removed )
            {
                annotations.Remove( item );
            }

            ClearConfluenceVariables( );

            _elliottWaveFibCount = annotations.Count;
        }

        public void ToggleShowFibonacciTargets()
        {
            ToggleShowFibonacciTargets( ShowElliottWave, _symbol, true );
        }

        public void ToggleShowFibonacciTargets( bool toShow, SymbolEx symbol, bool clearFib )
        {
            if ( toShow )
            {
                long mainChartSelectedCandleBarTime = SelectedCandleBarTime;

                if ( mainChartSelectedCandleBarTime > -1 )
                {
                    var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( symbol );

                    HewManager eWaveManager = null;
                    
                    if ( aa != null )
                    {
                        eWaveManager = (HewManager) aa.HewManager;
                    }

                    if( eWaveManager == null )
                        return;

                    if ( eWaveManager.IsSpecialBar( WaveScenarioNo, mainChartSelectedCandleBarTime ) )
                    {
                        var waves = eWaveManager.GetSpecialbarTopAndBottomHighestWaves(WaveScenarioNo, mainChartSelectedCandleBarTime );

                        if ( waves != default )
                        {
                            var firstWave = waves.Item1.WaveName;

                            var firstWaveDegree = waves.Item1.WaveCycle;

                            TonyDrawFibonacciProjectionAndRetracement( true, mainChartSelectedCandleBarTime, symbol, eWaveManager, firstWave, firstWaveDegree );


                            var secondWave = waves.Item2.WaveName;

                            var secondWaveDegree = waves.Item2.WaveCycle;

                            TonyDrawFibonacciProjectionAndRetracement( false, mainChartSelectedCandleBarTime, symbol, eWaveManager, secondWave, secondWaveDegree );
                        }
                    }
                    else
                    {
                        var allWaves = eWaveManager.GetWaveOfHigestDegree( WaveScenarioNo, mainChartSelectedCandleBarTime );

                        var currentWave = allWaves.HasValue ? allWaves.Value.WaveName : ElliottWaveEnum.NONE;

                        var currentWaveDegree = allWaves.HasValue ? allWaves.Value.WaveCycle : ElliottWaveCycle.UNKNOWN;

                        TonyDrawFibonacciProjectionAndRetracement( clearFib, mainChartSelectedCandleBarTime, symbol, eWaveManager, currentWave, currentWaveDegree );

                    }
                }
            }
            else
            {
                RemoveAllFibonacciTargets( );
            }
        }

        public void DrawMonoWaveLines( bool toShow, ref SBar bar, bool clearFib )
        {
            if ( toShow )
            {
                if( bar == SBar.EmptySBar)
                    return;
                RemoveAllMonowavesLines( );

                long mainChartSelectedCandleBarTime = bar.LinuxTime;

                if ( mainChartSelectedCandleBarTime > -1 )
                {
                    var scichart = GetParentSurface( );

                    var aa = (AdvancedAnalysisManager) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( bar.SymbolEx );

                    MonoWaveManager monoWaveMgr = null;

                    if ( aa != null )
                    {
                        monoWaveMgr = aa.CreateOrGetMonowaveManager( bar.BarPeriod );
                    }

                    if ( monoWaveMgr == null )
                        return;
                    

                    var monoLines = monoWaveMgr.GetMonoLines( mainChartSelectedCandleBarTime );

                    foreach ( var monoLine in monoLines )
                    {
                        var line                       = new LineAnnotation( );
                        line.XAxisId                   = "X";
                        line.YAxisId                   = "Y";
                        line.X1                        = monoLine.GetBeginDate();
                        line.Y1                        = monoLine.BeginY;
                        line.X2                        = monoLine.GetEndDate( );
                        line.Y2                        = monoLine.EndY;
                        //
                        line.StrokeThickness           = 1.0;
                        line.Stroke                    = monoLine.GetBrushColor( );
                        line.Opacity                   = 1.0;
                        Panel.SetZIndex( line, 1 );
                        line.IsEditable                = false;

                        if ( ! monoLine.RuleCalcWaves() )
                        {
                            line.StrokeDashArray = new DoubleCollection( ) { 2.0, 4.0 };
                        }


                        scichart.Annotations.Add( line );

                        var waveText                   = new TextAnnotation( );
                        waveText.FontFamily            = _fontFamily;
                        waveText.FontSize              = 14;
                        waveText.FontWeight            = _fontWeight;
                        waveText.FontStyle             = _fontStyle;
                        waveText.Text                  = monoLine.GetWaveNumberString();
                        waveText.Foreground            = new SolidColorBrush( Colors.Black );
                        waveText.Background            = monoLine.GetBackgroundBrushColor();
                        waveText.XAxisId               = "X";
                        waveText.YAxisId               = "Y";
                        waveText.VerticalAnchorPoint   = VerticalAnchorPoint.Center;
                        waveText.HorizontalAnchorPoint = HorizontalAnchorPoint.Center;
                        waveText.X1                    = monoLine.GetMidDate();
                        waveText.Y1                    = monoLine.MidY;  //barData.High;
                        waveText.Tag                   = "MonoWaveGroup";

                        scichart.Annotations.Add( waveText );

                        _hasMonolines = true;
                    }
                }
            }
            else
            {
                RemoveAllMonowavesLines( );
            }
        }

        public void ToggleShowFibonacciTargets( bool toShow, ref SBar bar, bool clearFib )
        {
            if ( toShow )
            {
                long mainChartSelectedCandleBarTime = bar.LinuxTime;

                if ( mainChartSelectedCandleBarTime > -1 )
                {
                    var aa = (AdvancedAnalysisManager)SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( bar.SymbolEx );

                    HewManager eWaveManager = null;

                    if ( aa != null )
                    {
                        eWaveManager = (HewManager) aa.HewManager;
                    }

                    if ( eWaveManager == null )
                        return;
                    

                    if ( eWaveManager.IsSpecialBar( WaveScenarioNo, mainChartSelectedCandleBarTime ) )
                    {
                        var waves = eWaveManager.GetSpecialbarTopAndBottomHighestWaves(WaveScenarioNo, mainChartSelectedCandleBarTime );

                        if ( waves != default )
                        {
                            var firstWave = waves.Item1.WaveName;

                            var firstWaveDegree = waves.Item1.WaveCycle;

                            TonyDrawFibonacciProjectionAndRetracement( true, mainChartSelectedCandleBarTime, bar.SymbolEx, eWaveManager, firstWave, firstWaveDegree );


                            var secondWave = waves.Item2.WaveName;

                            var secondWaveDegree = waves.Item2.WaveCycle;

                            TonyDrawFibonacciProjectionAndRetracement( false, mainChartSelectedCandleBarTime, bar.SymbolEx, eWaveManager, secondWave, secondWaveDegree );
                        }
                    }
                    else
                    {
                        var allWaves = eWaveManager.GetWaveOfHigestDegree( WaveScenarioNo, mainChartSelectedCandleBarTime );

                        var currentWave = allWaves.HasValue ? allWaves.Value.WaveName : ElliottWaveEnum.NONE;

                        var currentWaveDegree = allWaves.HasValue ? allWaves.Value.WaveCycle : ElliottWaveCycle.UNKNOWN;

                        TonyDrawFibonacciProjectionAndRetracement( clearFib, mainChartSelectedCandleBarTime, bar.SymbolEx, eWaveManager, currentWave, currentWaveDegree );

                    }
                }
            }
            else
            {
                RemoveAllFibonacciTargets( );
            }
        }



        private void TonyDrawFibonacciProjectionAndRetracement( bool clearFib, long barTime, SymbolEx symbol, HewManager eWaveManager, ElliottWaveEnum currentWave, ElliottWaveCycle currentWaveDegree )
        {            
            bool hasLockedFibs = false;

            if ( clearFib )
            {
                hasLockedFibs = RemoveAllFibonacciTargets( );
            }

            if ( hasLockedFibs )
            {
                FindConfluences4SRLevels( symbol.PriceStep );
            }

            var fib = eWaveManager.GetHewFibTargets( WaveScenarioNo, symbol, barTime, currentWave, currentWaveDegree );

            if ( fib != null )
            {
                AddFibonacciTarget( fib );
            }            
        }

        private void AddFibonacciTarget( HewFibGannTargets fib )
        {
            ref SBar realBar = ref _barList[ _lastBarIndex ];

            if ( fib.isRetracement )
            {
                _elliottWaveFibCount++;
                var fibRet              = new fxFibonacciRetracementAnnotation( fib, ref realBar, fib.TargetPoints );
                
                fibRet.XAxisId          = "X";
                fibRet.YAxisId          = "Y";
                fibRet.IsEnabled        = false;
                fibRet.IsEditable       = false;
                fibRet.AnnotationCanvas = AnnotationCanvas.BelowChart;

                var scichart            = GetParentSurface( );                               

                fibRet.Selected        += FibRet_Selected;
                fibRet.Unselected      += FibRet_Unselected;

                scichart.Annotations.Add( fibRet );
            }
            else if ( fib.isProjection )
            {
                _elliottWaveFibCount++;
                var fibProjection              = new fxFibonacciExtensionAnnotation( fib, ref realBar, fib.TargetPoints );
                fibProjection.XAxisId          = "X";
                fibProjection.YAxisId          = "Y";
                fibProjection.IsEnabled        = false;
                fibProjection.IsEditable       = false;                               
                fibProjection.Selected        += FibProjection_Selected;
                fibProjection.Unselected      += FibProjection_Unselected;
                fibProjection.AnnotationCanvas = AnnotationCanvas.BelowChart;

                GetParentSurface( ).Annotations.Add( fibProjection );                               
            }

            if ( fib.HasTonyRetracement )
            {
                _elliottWaveFibCount++;
                var fibRet              = new fxFibonacciRetracementAnnotation( fib, ref realBar, fib.TargetPoints, true );

                fibRet.XAxisId          = "X";
                fibRet.YAxisId          = "Y";
                fibRet.IsEnabled        = false;
                fibRet.IsEditable       = false;
                fibRet.AnnotationCanvas = AnnotationCanvas.BelowChart;

                var scichart            = GetParentSurface( );

                fibRet.Selected        += FibRet_Selected;
                fibRet.Unselected      += FibRet_Unselected;

                scichart.Annotations.Add( fibRet );

            }

            if ( fib.HasTonyExtensions && fib.ShowTonyExtensions )
            {
                _elliottWaveFibCount++;
                var fibRet              = new fxTonyProjectionAnnotation( fib, ref realBar, fib.TargetPoints );

                fibRet.XAxisId          = "X";
                fibRet.YAxisId          = "Y";
                fibRet.IsEnabled        = false;
                fibRet.IsEditable       = false;
                fibRet.AnnotationCanvas = AnnotationCanvas.BelowChart;

                fibRet.Selected        += FibRet_Selected;
                fibRet.Unselected      += FibRet_Unselected;

                GetParentSurface( ).Annotations.Add( fibRet );
            }

            if ( fib.HasTonyExtensions2 && fib.ShowTonyExtension2 )
            {
                _elliottWaveFibCount++;
                var fibRet              = new fxTonyProjectionAnnotation( fib, ref realBar, fib.TargetPoints, true );

                fibRet.XAxisId          = "X";
                fibRet.YAxisId          = "Y";
                fibRet.IsEnabled        = false;
                fibRet.IsEditable       = false;
                fibRet.AnnotationCanvas = AnnotationCanvas.BelowChart;

                fibRet.Selected        += FibRet_Selected;
                fibRet.Unselected      += FibRet_Unselected;

                GetParentSurface( ).Annotations.Add( fibRet );
            }

        }

        private void FibProjection_Unselected( object sender, EventArgs e )
        {
            if ( sender is IfxImportantLevel )
            {
                var a = ( IfxImportantLevel )sender;
                _selectedAnnotation.Remove( a );
            }
        }

        private void FibRet_Unselected( object sender, EventArgs e )
        {
            if ( sender is IfxImportantLevel )
            {
                var a = ( IfxImportantLevel )sender;
                _selectedAnnotation.Remove( a );
            }
        }

        private void FibRet_Selected( object sender, EventArgs e )
        {
            if ( sender is IfxImportantLevel )
            {
                var a = ( IfxImportantLevel )sender;
                _selectedAnnotation.Add( a );
            }
        }

        private void FibProjection_Selected( object sender, EventArgs e )
        {
            if ( sender is IfxImportantLevel )
            {
                var a = ( IfxImportantLevel )sender;
                _selectedAnnotation.Add( a );
            }
        }

        private void HiddenAllFibonacciTargets( )
        {
            var annotations = GetParentSurface( ).Annotations;            

            foreach ( var annotation in annotations )
            {
                annotation.IsHidden = true;
            }            
        }

        

        private void ShowAllFibonacciTargets( )
        {
            var annotations = GetParentSurface( ).Annotations;

            foreach ( var annotation in annotations )
            {
                annotation.IsHidden = false;
            }
        }




        private bool RemoveAllFibonacciTargets( )
        {
            bool hasLockedFibs = false;
            var surface = GetParentSurface( );

            if ( surface == null ) return false;

            var annotations = surface.Annotations;

            if ( annotations == null ) return false;

            PooledList< IfxImportantLevel > removed = new PooledList< IfxImportantLevel >( );

            foreach ( var annotation in annotations )
            {
                if ( annotation is IfxFibonacciAnnotation )
                {
                    var a = ( IfxImportantLevel )annotation;

                    if ( ! a.IsLocked )
                    {
                        removed.Add( ( IfxImportantLevel ) annotation );
                    }    
                    else
                    {
                        hasLockedFibs = true;
                    }
                }
            }

            foreach ( IfxImportantLevel item in removed )
            {
                annotations.Remove( ( IAnnotation) item );                
            }

            _elliottWaveFibCount = annotations.Count;

            return hasLockedFibs;
        }

        private void RemoveAllMonowavesLines( )
        {
            var surface = GetParentSurface( );

            if( surface == null )
                return;

            var annotations = surface.Annotations;

            var removed     = new PooledList< LineAnnotation >( );
            var txtRemoved  = new PooledList< TextAnnotation >( );
            foreach ( var annotation in annotations )
            {
                if ( annotation is LineAnnotation )
                {
                    removed.Add( ( LineAnnotation ) annotation );
                }

                if ( annotation is TextAnnotation )
                {
                    var txt = ( TextAnnotation ) annotation;
                    var tag = (string ) txt.Tag;

                    if ( tag == "MonoWaveGroup" )
                    {
                        txtRemoved.Add( ( TextAnnotation ) annotation );
                    }
                }
            }

                foreach ( LineAnnotation item in removed )
                {
                    annotations.Remove( item );
                }

                foreach ( TextAnnotation item in txtRemoved )
                {
                    annotations.Remove( item );
                }

                _hasMonolines = false;
            
            
        }

        private void RemoveAllStructuralLabels()
        {
            var surface = GetParentSurface( );

            if( surface == null )
                return;

            var annotations = surface.Annotations;

            var removed     = new PooledList< LineAnnotation >( );
            var txtRemoved  = new PooledList< TextAnnotation >( );

            foreach ( var annotation in annotations )
            {
                if ( annotation is TextAnnotation )
                {
                    var txt = ( TextAnnotation ) annotation;
                    var tag = (string ) txt.Tag;

                    if ( tag == "StructuralLabel" )
                    {
                        txtRemoved.Add( ( TextAnnotation ) annotation );
                    }
                }
            }

            foreach ( TextAnnotation item in txtRemoved )
            {
                annotations.Remove( item );
            }

            _monoWaveAnnotationAdded.Clear();
            
        }

        public (SBar index , double value  ) GetExtremumValues( TrendDirection dir )
        {
            ( SBar bar, double value  ) output = default;

            var bars = SymbolsMgr.Instance.GetDatabarRepo( _symbol, _period );

            if ( _earliestLockedLevel != DateTime.MaxValue )
            {
                if ( dir == TrendDirection.Uptrend )
                {
                    ref SBar highBar = ref bars.GetHighestBarOfTheRange( _earliestLockedLevel, bars.LastBarTime.Value );
                    
                    if ( highBar != SBar.EmptySBar ) 
                    {
                        output.bar = highBar;
                        output.value = output.bar.High;
                    }                    
                }
                else if ( dir == TrendDirection.DownTrend )
                {
                    ref SBar lowBar = ref bars.GetLowestBarOfTheRange( _earliestLockedLevel, bars.LastBarTime.Value );

                    if ( lowBar != SBar.EmptySBar )
                    {
                        output.bar = lowBar;
                        output.value = output.bar.Low;
                    }                                        
                }
            }
            
            return output;
        }

        public void UpdateAllImptLinesBrokenStatus( TrendDirection dir  )
        {
            (SBar bar, double value)  result = GetExtremumValues( dir );

            if (result != default )
            {
                _extremumBarIndex = result.bar;
                _extremumValue = result.value;
            }            

            for ( int i = 0; i < _lockedAllIfxImportantLines.Count; i++ )
            {
                (SRlevel level, IfxImportantLevel anno) current  = _lockedAllIfxImportantLines[ i ];

                if ( dir == TrendDirection.Uptrend )
                {
                    if ( current.level.SRvalue < _extremumValue )
                    {
                        current.level.IsBroken = true;
                    }
                    else
                    {
                        current.level.IsBroken = false;
                    }
                }
                else if ( dir == TrendDirection.DownTrend )
                {
                    if ( current.level.SRvalue < _extremumValue )
                    {
                        current.level.IsBroken = true;
                    }
                    else
                    {
                        current.level.IsBroken = false;
                    }
                }
            }            
        }


        private (SRlevel level, IfxImportantLevel line) HighlightedConfluenceClosestToExtremum( TrendDirection dir )
        {
            double different = double.MaxValue;
            (SRlevel level, IfxImportantLevel line ) closest = default;

            for ( int i = 0; i < _lockedAllIfxImportantLines.Count; i++ )
            {
                (SRlevel level, IfxImportantLevel anno) current  = _lockedAllIfxImportantLines[ i ];

                var diff = Math.Abs( current.level.SRvalue - _extremumValue );

                if ( diff < different )
                {
                    different = diff;
                    closest = current;
                }                
            }
            
            if ( closest != default )
            {
                var confluentCenter = closest.level.SRvalue;
                /* --------------------------------------------------------------------------------------------------
                * 
                * We highlight the closest line
                * 
                * Next we want to highlight the confluent lines.
                * 
                * --------------------------------------------------------------------------------------------------
                * */
                closest.line.HighlightConfluence( confluentCenter );

                var allowed = ( float ) _symbol.PriceStep * 3;

                for ( int i = 0; i < _lockedAllIfxImportantLines.Count; i++ )
                {
                    (SRlevel level, IfxImportantLevel line ) current  = _lockedAllIfxImportantLines[ i ];

                    var diff = Math.Abs( current.level.SRvalue - confluentCenter );

                    if ( diff <= allowed )
                    {
                        current.line.HighlightConfluence( current.level.SRvalue );
                    }                    
                }
            }

            return closest;
        }

        private (SRlevel level, IfxImportantLevel line) HighlightedConfluenceClosestToLastBar( float high, float low, TrendDirection dir )
        {
            double different = double.MaxValue;
            (SRlevel level, IfxImportantLevel line ) closest = default;

            var localExtremumValue = dir == TrendDirection.Uptrend ? high : low;

            for ( int i = 0; i < _lockedAllIfxImportantLines.Count; i++ )
            {
                (SRlevel level, IfxImportantLevel anno) current  = _lockedAllIfxImportantLines[ i ];

                var diff = Math.Abs( current.level.SRvalue - localExtremumValue );

                if ( diff < different )
                {
                    different = diff;
                    closest   = current;
                }
            }

            if ( closest != default )
            {
                var confluentCenter = closest.level.SRvalue;
                /* --------------------------------------------------------------------------------------------------
                * 
                * We highlight the closest line
                * 
                * Next we want to highlight the confluent lines.
                * 
                * --------------------------------------------------------------------------------------------------
                * */
                closest.line.HighlightConfluence( confluentCenter );

                var allowed = ( float ) _symbol.PriceStep * 3;

                for ( int i = 0; i < _lockedAllIfxImportantLines.Count; i++ )
                {
                    (SRlevel level, IfxImportantLevel line ) current  = _lockedAllIfxImportantLines[ i ];

                    var diff = Math.Abs( current.level.SRvalue - confluentCenter );

                    if ( diff <= allowed )
                    {
                        current.line.HighlightConfluence( current.level.SRvalue );
                    }
                }
            }

            return closest;
        }

        public ( TrendDirection, int ) CollectImportantLevels()
        {
            (TrendDirection Trend, int lockedFibCount) output = default;

            var annotations = GetParentSurface( ).Annotations;            

            /* --------------------------------------------------------------------------------------------------
            * 
            * First Find out the earliest date of the locked Fib
            * 
            * Store all the Fib levels and PP levels into _lockedAllIfxImportantLines
            * 
            * --------------------------------------------------------------------------------------------------
            * */
            foreach ( var annotation in annotations )
            {
                if ( annotation is IfxImportantLevel )
                {
                    var a = ( IfxImportantLevel )annotation;

                    if ( a.IsLocked )
                    {
                        if ( annotation is IfxFibonacciAnnotation )
                        {
                            var fib = ( IfxFibonacciAnnotation ) annotation;
                            output.lockedFibCount++;

                            var levelDate = fib.EndingTime;

                            if ( levelDate < _earliestLockedLevel )
                            {
                                _earliestLockedLevel = levelDate;
                            }

                            _lockedImptLinesDirection = fib.Trend;
                            output.Trend              = fib.Trend;

                            foreach ( SRlevel line in a.ImportantLines )
                            {
                                _lockedAllIfxImportantLines.Add( (line, a) );
                            }
                        }

                        if ( annotation is PivotPointLevelsAnnotation )
                        {
                            foreach ( SRlevel fib in a.ImportantLines )
                            {
                                _lockedPPLines.Add( (fib, a) );
                            }
                        }

                        if ( !_lockedPPandFibObjects.Contains( a ) )
                        {
                            _lockedPPandFibObjects.Add( a );
                        }
                    }
                }
            }

            if ( output.lockedFibCount  > 0 )
            {
                _lockedAllIfxImportantLines.AddRange( _lockedPPLines );

                _lockedAllIfxImportantLines.Sort( ( p, q ) => p.Item1.CompareTo( q.Item1 ) );
            }
            
            return output;
        }

        private void FindConfluences4SRLevels( float? priceStep )
        {
            (TrendDirection Trend, int lockedFibCount) lvls = CollectImportantLevels( );
                                    
            if ( lvls.lockedFibCount > 0 )
            {                
                foreach ( IfxImportantLevel lockedAnnotation in _lockedPPandFibObjects )
                {
                    lockedAnnotation.DimAllImportantLines( );
                }

                /* --------------------------------------------------------------------------------------------------
                * 
                * Next I want to see which lines have been broken and which will be the next lines to be broken.
                * 
                * --------------------------------------------------------------------------------------------------
                * */

                UpdateAllImptLinesBrokenStatus( lvls.Trend );

                /* --------------------------------------------------------------------------------------------------
                * 
                * Next I want to highlight the SRLevel closest to the highest
                * 
                * --------------------------------------------------------------------------------------------------
                * */

                var closetLvl = HighlightedConfluenceClosestToExtremum( lvls.Trend );

                if ( closetLvl != default )
                {
                    HighlightedConfluenceSecondClosestToExtremum( closetLvl, lvls.Trend );
                }
            }
        }

        private void HighlightedConfluenceSecondClosestToExtremum( (SRlevel level, IfxImportantLevel line) closetLvl, TrendDirection dir )
        {
            double different = double.MaxValue;
            (SRlevel level, IfxImportantLevel line ) closest2nd = default;

            var allowed = ( float ) _symbol.PriceStep * 3;

            var confluentCenter = closetLvl.level.SRvalue;

            for ( int i = 0; i < _lockedAllIfxImportantLines.Count; i++ )
            {
                (SRlevel level, IfxImportantLevel anno) current  = _lockedAllIfxImportantLines[ i ];

                if ( dir == TrendDirection.Uptrend )
                {
                    if ( current.level.SRvalue > confluentCenter )
                    {
                        var diff = current.level.SRvalue - confluentCenter;

                        if ( diff > allowed && diff < different )
                        {
                            different = diff;
                            closest2nd = current;
                        }
                    }
                }
                else if ( dir == TrendDirection.DownTrend )
                {
                    if ( current.level.SRvalue < confluentCenter )
                    {
                        var diff = confluentCenter - current.level.SRvalue;

                        if ( diff < different )
                        {
                            different = diff;
                            closest2nd = current;
                        }
                    }
                }
            }

            if ( closest2nd != default )
            {
                var closest2ndCenter = closest2nd.level.SRvalue;
                /* --------------------------------------------------------------------------------------------------
                * 
                * We highlight the closest line
                * 
                * Next we want to highlight the confluent lines.
                * 
                * --------------------------------------------------------------------------------------------------
                * */
                closest2nd.line.HighlightConfluence( closest2ndCenter );                

                for ( int i = 0; i < _lockedAllIfxImportantLines.Count; i++ )
                {
                    (SRlevel level, IfxImportantLevel line ) current  = _lockedAllIfxImportantLines[ i ];

                    var diff = Math.Abs( current.level.SRvalue - closest2ndCenter );

                    if ( diff <= allowed )
                    {
                        current.line.HighlightConfluence( current.level.SRvalue );
                    }
                }
            }            
        }

        private void DrawTradingTime( IRenderContext2D renderContext, IRenderPassData renderPassData, Values<int> indexes, Values<double> xvalues, int count )
        {
            for ( int i = 0; i < count; ++i )
            {
                int currIdx       = indexes[ i ];
                double xValue     = xvalues[ i ];
                var metadata      = DataSeries.HasMetadata ? DataSeries.Metadata[ currIdx ] : null;

                var tmpBar        = ( SBar ) metadata;
                CheckBarsRepo( ref tmpBar );
                ref SBar barData  = ref _barList[ tmpBar.Index ];
                
                if ( barData != SBar.EmptySBar )
                {
                    if ( barData.BarSession == SessionEnum.EuropeanSessionStart )
                    {
                        _sessionTime.Add( new Tuple<double, SessionEnum>( xValue, SessionEnum.EuropeanSessionStart ) );
                    }
                    else if ( barData.BarSession == SessionEnum.UsaSessionStart )
                    {
                        _sessionTime.Add( new Tuple<double, SessionEnum>( xValue, SessionEnum.UsaSessionStart ) );
                    }
                    else if ( barData.BarSession == SessionEnum.DailySessionEnd )
                    {
                        _sessionTime.Add( new Tuple<double, SessionEnum>( xValue, SessionEnum.DailySessionEnd ) );

                        //var newYorkTime = barData.NewYorkTime;

                        //_dailyTradingSessionEnd.TryAdd( newYorkTime.Date, i );
                    }
                }
            }

            int sessionCount = ( int )_sessionTime.Count;

            for ( int i = 0; i < sessionCount; i++ )
            {
                int xCoor = renderPassData.XCoordinateCalculator.GetCoordinate( _sessionTime.ElementAt( i ).Item1 ).ClipToIntValue( );

                int startX = ( xCoor - _candleWidth * 0.5 ).ClipToIntValue( );

                double endX = startX + ( float )renderContext.ViewportSize.Width;

                if ( ( i + 1 ) < sessionCount )
                {
                    endX = renderPassData.XCoordinateCalculator.GetCoordinate( _sessionTime.ElementAt( i + 1 ).Item1 ).ClipToIntValue( );
                }

                SolidColorBrush fill = null;

                if ( _sessionTime.ElementAt( i ).Item2 == SessionEnum.EuropeanSessionStart )
                {
                    if ( i == 0 )
                    {
                        Brush prefill = new SolidColorBrush( Color.FromRgb( 0xFD, 0xFD, 0xFD ) );
                        var prefillBrush = renderContext.CreateBrush( prefill, Opacity, TextureMappingMode.PerPrimitive );
                        var endPt = new Point( startX, renderContext.ViewportSize.Height );

                        renderContext.FillRectangle( prefillBrush, new Point( 0, 0 ), endPt );
                    }

                    fill = new SolidColorBrush( Color.FromRgb( 0xFF, 0xFF, 0xCC ) );
                }
                else if ( _sessionTime.ElementAt( i ).Item2 == SessionEnum.UsaSessionStart )
                {
                    if ( i == 0 )
                    {
                        Brush prefill = new SolidColorBrush( Color.FromRgb( 0xFD, 0xFD, 0xFD ) );
                        var prefillBrush = renderContext.CreateBrush( prefill, Opacity, TextureMappingMode.PerPrimitive );
                        var endPt = new Point( startX, renderContext.ViewportSize.Height );

                        renderContext.FillRectangle( prefillBrush, new Point( 0, 0 ), endPt );
                    }

                    fill = new SolidColorBrush( Color.FromRgb( 226, 255, 226 ) );
                }
                else if ( _sessionTime.ElementAt( i ).Item2 == SessionEnum.DailySessionEnd )
                {
                    if ( ( i + 1 ) < sessionCount )
                    {
                        endX = renderPassData.XCoordinateCalculator.GetCoordinate( _sessionTime.ElementAt( i + 1 ).Item1 ).ClipToIntValue( );
                    }

                    if ( i == 0 )
                    {
                        Brush prefill = new SolidColorBrush( Color.FromRgb( 226, 255, 226 ) );
                        var prefillBrush = renderContext.CreateBrush( prefill, Opacity, TextureMappingMode.PerPrimitive );
                        var endPt = new Point( startX, renderContext.ViewportSize.Height );

                        renderContext.FillRectangle( prefillBrush, new Point( 0, 0 ), endPt );
                    }

                    fill = new SolidColorBrush( Color.FromRgb( 253, 253, 253 ) );
                }

                var fillBrush = renderContext.CreateBrush( fill, Opacity, TextureMappingMode.PerPrimitive );

                renderContext.FillRectangle( fillBrush, new Point( startX, 0 ), new Point( endX, renderContext.ViewportSize.Height ) );
            }
        }

        public PooledDictionary<ElliottWaveCycle, string> GetFilteredCycleWaveString( IEnumerable<WaveInfo> mySortedList, WaveLabelPosition wavePosition )
        {
            PooledDictionary<ElliottWaveCycle, string> output = new PooledDictionary<ElliottWaveCycle, string>( );

            var waveCycle = ElliottWaveCycle.UNKNOWN;


            foreach ( var sortedItem in mySortedList )
            {
                if ( !output.ContainsKey( sortedItem.WaveCycle ) )
                {
                    waveCycle = sortedItem.WaveCycle;

                    var waveString = FinancialHelper.GetWaveString( waveCycle, sortedItem.WaveName );

                    output.Add( waveCycle, waveString );
                }
                else
                {
                    string lastWaveOfSameDegree = output[ sortedItem.WaveCycle ];

                    waveCycle = sortedItem.WaveCycle;

                    var waveString = FinancialHelper.GetWaveString( waveCycle, sortedItem.WaveName );

                    var newWaveName = string.Format( "{0}{2}{1}", lastWaveOfSameDegree, waveString, Environment.NewLine );

                    output[ sortedItem.WaveCycle ] = newWaveName;
                }

            }

            return output;
        }

        public bool DrawWithElliottWave( TASignal s )
        {
            if ( s == TASignal.WAVE_PEAK || s == TASignal.WAVE_TROUGH || s == TASignal.GANN_PEAK || s == TASignal.GANN_TROUGH )
            {
                return true;
            }

            if ( ( s == TASignal.HAS_BOTTOMING_SIGNAL || s == TASignal.HAS_TOPPING_SIGNAL ) && ShowIndicatorResult )
            {
                return true;
            }

            if ( s == TASignal.HAS_PIVOT_RELATION && ShowPriceTimeSignal )
            {
                return true;
            }

            if ( s == TASignal.HAS_TIME_ROTATION && ShowPriceTimeSignal )
            {
                return true;
            }

            if ( s == TASignal.HAS_GANN_SQUARE && ShowPriceTimeSignal )
            {
                return true;
            }

            if ( s == TASignal.HAS_DIVERGENCE && ShowDivergence )
            {
                return true;
            }


            return false;
        }



        public Color GetWaveColor( ElliottWaveCycle cycle, bool isWhiteTheme )
        {
            if ( isWhiteTheme )
            {
                switch ( cycle )
                {
                    case ElliottWaveCycle.Miniscule:
                    return Colors.Blue;

                    case ElliottWaveCycle.Submicro:
                    return Colors.Red;


                    case ElliottWaveCycle.Micro:
                    return Colors.Black;


                    case ElliottWaveCycle.Subminuette:
                    return Colors.Green;


                    case ElliottWaveCycle.Minuette:
                    return Colors.Blue;

                    case ElliottWaveCycle.SubMinute:
                    return Colors.Red;

                    case ElliottWaveCycle.Minute:
                    return Colors.Black;


                    case ElliottWaveCycle.SubMinor:
                    return Colors.Green;

                    case ElliottWaveCycle.Minor:
                    return Colors.Blue;

                    case ElliottWaveCycle.SubIntermediate:
                    return Colors.Red;

                    case ElliottWaveCycle.Intermediate:
                    return Colors.Black;


                    case ElliottWaveCycle.Primary:
                    return Colors.Green;

                    case ElliottWaveCycle.Cycle:
                    return Colors.Blue;

                    case ElliottWaveCycle.Supercycle:
                    return Colors.Red;

                    case ElliottWaveCycle.GrandSupercycle:
                    return Colors.Black;
                }
            }
            else
            {
                switch ( cycle )
                {
                    case ElliottWaveCycle.Miniscule:
                    return Colors.LightCoral;

                    case ElliottWaveCycle.Submicro:
                    return Colors.Red;


                    case ElliottWaveCycle.Micro:
                    return Colors.White;


                    case ElliottWaveCycle.Subminuette:
                    return Colors.LightGreen;


                    case ElliottWaveCycle.Minuette:
                    return Colors.LightCoral;

                    case ElliottWaveCycle.SubMinute:
                    return Colors.Red;

                    case ElliottWaveCycle.Minute:
                    return Colors.White;


                    case ElliottWaveCycle.SubMinor:
                    return Colors.LightGreen;

                    case ElliottWaveCycle.Minor:
                    return Colors.LightCoral;

                    case ElliottWaveCycle.SubIntermediate:
                    return Colors.Red;

                    case ElliottWaveCycle.Intermediate:
                    return Colors.White;


                    case ElliottWaveCycle.Primary:
                    return Colors.LightGreen;

                    case ElliottWaveCycle.Cycle:
                    return Colors.LightCoral;

                    case ElliottWaveCycle.Supercycle:
                    return Colors.Red;

                    case ElliottWaveCycle.GrandSupercycle:
                    return Colors.White;
                }
            }

            return Colors.Gray;
        }


        public float GetWaveFontSize( ElliottWaveCycle cycle )
        {
            switch ( cycle )
            {
                case ElliottWaveCycle.Miniscule:
                return 8F;

                case ElliottWaveCycle.Submicro:
                return 8F;

                case ElliottWaveCycle.Micro:
                return 8F;

                case ElliottWaveCycle.Subminuette:
                return 12F;

                case ElliottWaveCycle.Minuette:
                return 12F;

                case ElliottWaveCycle.SubMinute:
                return 12F;

                case ElliottWaveCycle.Minute:
                return 12F;

                case ElliottWaveCycle.SubMinor:
                return 16F;

                case ElliottWaveCycle.Minor:
                return 16F;

                case ElliottWaveCycle.SubIntermediate:
                return 16F;

                case ElliottWaveCycle.Intermediate:
                return 16F;

                case ElliottWaveCycle.Primary:
                return 20F;

                case ElliottWaveCycle.Cycle:
                return 20F;

                case ElliottWaveCycle.Supercycle:
                return 20F;

                case ElliottWaveCycle.GrandSupercycle:
                return 20F;
            }

            return 8F;
        }

        private bool DrawElliottWaveLabelsBoth( IRenderContext2D g, ref SBar barData, double xCoor, double highY, double lowY, int waveScnarioNo, bool isFilterWaves )
        {            
            var startingWave    = barData.GetFirstWave( waveScnarioNo );

            var topWaves    = _minimumToShow == ElliottWaveCycle.UNKNOWN ? barData.GetTopWaves( waveScnarioNo, isFilterWaves )    : barData.GetTopWaves( waveScnarioNo, isFilterWaves, _minimumToShow );
            var bottomWaves = _minimumToShow == ElliottWaveCycle.UNKNOWN ? barData.GetBottomWaves( waveScnarioNo, isFilterWaves ) : barData.GetBottomWaves( waveScnarioNo, isFilterWaves, _minimumToShow );
            
            if ( startingWave.Value.LabelPosition == WaveLabelPosition.TOP )
            {
                DrawElliottOnSpecialBarTop( g, ref barData, xCoor, highY, lowY, topWaves, bottomWaves );
            }
            else if ( startingWave.Value.LabelPosition == WaveLabelPosition.BOTTOM )
            {
                DrawElliottOnSpecialBarBottom( g, ref barData, xCoor, highY, lowY, topWaves, bottomWaves );
            }

            return false;

        }

        private void DrawElliottOnSpecialBarBottom( IRenderContext2D g, ref SBar barData, double xCoor, double highY, double lowY,  PooledList<WaveInfo> topWaves, PooledList<WaveInfo> bottomWaves )
        {
            var mySortedListString = GetFilteredCycleWaveString( topWaves, WaveLabelPosition.TOP );
            var lastWaveCycle  = ElliottWaveCycle.UNKNOWN;

            var toBeAddedTop = new PooledList<TextAnnotation>( );
            var toBeAddedBot = new PooledList<TextAnnotation>( );

            var waveKey        = new WaveAnnotationKey( barData.LinuxTime, WaveLabelPosition.TOP );

            if ( barData.WaveDirty == WaveDirtyEnum.Change || _needToRedrawElliottWave )
            {
                RemoveWavesFromChart( ref barData );
                barData.WaveDirty = WaveDirtyEnum.NONE;

            }

            foreach ( var elliottWave in topWaves )
            {
                var waveCycle = elliottWave.WaveCycle;

                if ( waveCycle != lastWaveCycle )
                {
                    lastWaveCycle = waveCycle;

                    if ( mySortedListString.ContainsKey( waveCycle ) )
                    {
                        var waveString   = mySortedListString[ waveCycle ];

                        var drawString   = waveString;

                        var fontSize     = GetWaveFontSize( waveCycle );
                        var textBlock    = new TextBlock( ) { FontFamily = _fontFamily, FontSize = fontSize, FontStyle = _fontStyle, FontWeight = _fontWeight };
                        var size         = drawString.Measure( textBlock );

                        var waveColor    = GetWaveColor( waveCycle, true );

                        var drawY        = _lastTopPosition > 0 ? _lastTopPosition : highY;

                        var drawPoint    = new Point( xCoor - size.Width / 2, drawY - 10 - size.Height );

                        _lastTopPosition = drawY - SignalMargin - size.Height;

                        var drawRect     = new Rect( drawPoint, size );
                        
                        if ( HighQualityWaveText )
                        {
                            if ( !_waveAnnotationAdded.ContainsKey( waveKey ) )
                            {
                                var scichart = GetParentSurface( );

                                IComparable x = 0;

                                ICategoryCoordinateCalculator<DateTime> coordinateCalculator = XAxis.GetCurrentCoordinateCalculator( ) as ICategoryCoordinateCalculator<DateTime>;
                                if ( coordinateCalculator != null )
                                {
                                    x = coordinateCalculator.TransformDataToIndex( barData.BarTime );
                                }

                                var waveText                   = new TextAnnotation( );
                                waveText.FontFamily            = _fontFamily;
                                waveText.FontSize              = fontSize;
                                waveText.FontWeight            = _fontWeight;
                                waveText.FontStyle             = _fontStyle;
                                waveText.Text                  = drawString;
                                waveText.Foreground            = new SolidColorBrush( waveColor );
                                waveText.XAxisId               = "X";
                                waveText.YAxisId               = "Y";
                                waveText.VerticalAnchorPoint   = VerticalAnchorPoint.Center;
                                waveText.HorizontalAnchorPoint = HorizontalAnchorPoint.Center;
                                waveText.X1                    = x;
                                waveText.Y1                    = YAxis.GetDataValue( _lastTopPosition );  //barData.High;

                                scichart.Annotations.Add( waveText );

                                toBeAddedTop.Add( waveText );                               
                            }
                            else
                            {
                                var waveList = _waveAnnotationAdded.GetOrAddValueRef( waveKey );

                                foreach ( var waveText in waveList )
                                {
                                    if ( waveText.Text == drawString )
                                    {
                                        waveText.FontFamily = _fontFamily;
                                        waveText.FontSize   = fontSize;
                                        waveText.FontWeight = _fontWeight;
                                        waveText.FontStyle  = _fontStyle;
                                        waveText.Text       = drawString;
                                        waveText.Foreground = new SolidColorBrush( waveColor );
                                        waveText.Y1         = YAxis.GetDataValue( _lastTopPosition );
                                        waveText.IsHidden   = false;
                                    }
                                }
                            }

                        }
                        else
                        {
                            g.DrawText( drawRect, waveColor, fontSize, drawString, _fontFamily, _fontWeight, _fontStyle );
                        }

                        mySortedListString.Remove( waveCycle );
                    }
                }
            }

            if ( toBeAddedTop.Count > 0 )
            {
                _waveAnnotationAdded.GetOrAddValueRef( waveKey ) = toBeAddedTop;                                
            }

            var mySortedList       = bottomWaves.OrderBy( l => l.WaveCycle ).ThenBy( l => l.WaveName );
            mySortedListString = GetFilteredCycleWaveString( mySortedList, WaveLabelPosition.BOTTOM );

            waveKey            = new WaveAnnotationKey( barData.LinuxTime, WaveLabelPosition.BOTTOM );

            foreach ( var elliottWave in mySortedList )
            {
                var waveCycle = elliottWave.WaveCycle;
                lastWaveCycle = waveCycle;

                if ( mySortedListString.ContainsKey( waveCycle ) )
                {
                    var waveString = mySortedListString[ waveCycle ];

                    var drawString = waveString;

                    var fontSize   = GetWaveFontSize( waveCycle );
                    var textBlock  = new TextBlock( ) { FontFamily = _fontFamily, FontSize = fontSize, FontStyle = _fontStyle, FontWeight = _fontWeight };
                    var size       = drawString.Measure( textBlock );

                    var waveColor  = GetWaveColor( waveCycle, true );

                    var drawY      = _lastBottomPosition > 0 ? _lastBottomPosition + SignalMargin : lowY;

                    var drawPoint  = new Point( xCoor - size.Width / 2, drawY );

                    var drawRect   = new Rect( drawPoint, size );

                    

                    if ( HighQualityWaveText )
                    {
                        if ( !_waveAnnotationAdded.ContainsKey( waveKey ) )
                        {
                            var scichart = GetParentSurface( );

                            IComparable x = 0;

                            ICategoryCoordinateCalculator<DateTime> coordinateCalculator = XAxis.GetCurrentCoordinateCalculator( ) as ICategoryCoordinateCalculator<DateTime>;
                            if ( coordinateCalculator != null )
                            {
                                x = coordinateCalculator.TransformDataToIndex( barData.BarTime );
                            }

                            var waveText                   = new TextAnnotation( );
                            waveText.FontFamily            = _fontFamily;
                            waveText.FontSize              = fontSize;
                            waveText.FontWeight            = _fontWeight;
                            waveText.FontStyle             = _fontStyle;
                            waveText.Text                  = drawString;
                            waveText.VerticalAnchorPoint   = VerticalAnchorPoint.Center;
                            waveText.HorizontalAnchorPoint = HorizontalAnchorPoint.Center;
                            waveText.Foreground            = new SolidColorBrush( waveColor );
                            waveText.XAxisId               = "X";
                            waveText.YAxisId               = "Y";
                            waveText.X1                    = x;
                            waveText.Y1                    = YAxis.GetDataValue( drawY );  //barData.High;

                            scichart.Annotations.Add( waveText );

                            toBeAddedBot.Add( waveText );
                        }
                        else
                        {
                            var waveList = _waveAnnotationAdded.GetOrAddValueRef( waveKey );

                            foreach ( var waveText in waveList )
                            {
                                if ( waveText.Text == drawString )
                                {
                                    waveText.FontFamily = _fontFamily;
                                    waveText.FontSize   = fontSize;
                                    waveText.FontWeight = _fontWeight;
                                    waveText.FontStyle  = _fontStyle;
                                    waveText.Text       = drawString;
                                    waveText.Foreground = new SolidColorBrush( waveColor );
                                    waveText.Y1         = YAxis.GetDataValue( drawY );
                                    waveText.IsHidden   = false;
                                }
                            }
                        }

                    }
                    else
                    {
                        g.DrawText( drawRect, waveColor, fontSize, drawString, _fontFamily, _fontWeight, _fontStyle );
                    }

                    //g.DrawText( new Rect( drawPoint, size ), waveColor, fontSize, drawString, _fontFamily, _fontWeight, _fontStyle );

                    _lastBottomPosition = drawY + size.Height;

                    mySortedListString.Remove( waveCycle );
                }

            }

            if ( toBeAddedBot.Count > 0 )
            {
                _waveAnnotationAdded.GetOrAddValueRef( waveKey ) = toBeAddedBot;                   
            }
        }

        private PooledDictionary<ElliottWaveCycle, string> DrawElliottOnSpecialBarTop( IRenderContext2D g, ref SBar barData, double xCoor, double highY, double lowY, PooledList<WaveInfo> topWaves, PooledList<WaveInfo> bottomWaves )
        {
            var mySortedListString = GetFilteredCycleWaveString( topWaves, WaveLabelPosition.TOP );
            var toBeAddedTop       = new PooledList<TextAnnotation>( );
            var toBeAddedBot       = new PooledList<TextAnnotation>( );
            var waveKey            = new WaveAnnotationKey( barData.LinuxTime, WaveLabelPosition.TOP );
            var mySortedList       = topWaves.OrderBy( l => l.WaveCycle ).ThenBy( l => l.WaveName );

            if ( barData.WaveDirty == WaveDirtyEnum.Change || _needToRedrawElliottWave )
            {
                RemoveWavesFromChart( ref barData );
                barData.WaveDirty = WaveDirtyEnum.NONE;

            }

            foreach ( var elliottWave in mySortedList )
            {
                var waveCycle = elliottWave.WaveCycle;
                var waveName = elliottWave.WaveName;

                if ( mySortedListString.ContainsKey( waveCycle ) )
                {
                    var waveString   = mySortedListString[ waveCycle ];

                    var drawString   = waveString;

                    var fontSize     = GetWaveFontSize( waveCycle );
                    var textBlock    = new TextBlock( ) { FontFamily = _fontFamily, FontSize = fontSize, FontStyle = _fontStyle, FontWeight = _fontWeight };
                    var size         = drawString.Measure( textBlock );

                    var waveColor    = GetWaveColor( waveCycle, true );

                    var drawY        = _lastTopPosition > 0 ? _lastTopPosition : highY;

                    var drawPoint    = new Point( xCoor - size.Width / 2, drawY - 10 - size.Height );

                    _lastTopPosition = drawY - SignalMargin - size.Height;

                    var drawRect     = new Rect( drawPoint, size );                    

                    if ( HighQualityWaveText )
                    {
                        if ( !_waveAnnotationAdded.ContainsKey( waveKey ) )
                        {
                            var scichart = GetParentSurface( );

                            IComparable x = 0;

                            ICategoryCoordinateCalculator<DateTime> coordinateCalculator = XAxis.GetCurrentCoordinateCalculator( ) as ICategoryCoordinateCalculator<DateTime>;
                            if ( coordinateCalculator != null )
                            {
                                x = coordinateCalculator.TransformDataToIndex( barData.BarTime );
                            }

                            var waveText                   = new TextAnnotation( );
                            waveText.FontFamily            = _fontFamily;
                            waveText.FontSize              = fontSize;
                            waveText.FontWeight            = _fontWeight;
                            waveText.FontStyle             = _fontStyle;
                            waveText.Text                  = drawString;
                            waveText.Foreground            = new SolidColorBrush( waveColor );
                            waveText.XAxisId               = "X";
                            waveText.YAxisId               = "Y";
                            waveText.VerticalAnchorPoint   = VerticalAnchorPoint.Center;
                            waveText.HorizontalAnchorPoint = HorizontalAnchorPoint.Center;
                            waveText.X1                    = x;
                            waveText.Y1                    = YAxis.GetDataValue( _lastTopPosition );  //barData.High;

                            scichart.Annotations.Add( waveText );

                            toBeAddedTop.Add( waveText );
                        }
                        else
                        {
                            var waveList = _waveAnnotationAdded.GetOrAddValueRef( waveKey );

                            foreach ( var waveText in waveList )
                            {
                                if ( waveText.Text == drawString )
                                {
                                    waveText.FontFamily = _fontFamily;
                                    waveText.FontSize   = fontSize;
                                    waveText.FontWeight = _fontWeight;
                                    waveText.FontStyle  = _fontStyle;
                                    waveText.Text       = drawString;
                                    waveText.Foreground = new SolidColorBrush( waveColor );
                                    waveText.Y1         = YAxis.GetDataValue( _lastTopPosition );
                                    waveText.IsHidden   = false;
                                }
                            }
                        }

                    }
                    else
                    {
                        g.DrawText( drawRect, waveColor, fontSize, drawString, _fontFamily, _fontWeight, _fontStyle );
                    }

                    mySortedListString.Remove( waveCycle );
                }
            }

            if ( toBeAddedTop.Count > 0 )
            {
                _waveAnnotationAdded.GetOrAddValueRef( waveKey ) = toBeAddedTop;                   
            }

            mySortedListString = GetFilteredCycleWaveString( bottomWaves, WaveLabelPosition.BOTTOM );
            var lastWaveCycle  = ElliottWaveCycle.UNKNOWN;

            waveKey            = new WaveAnnotationKey( barData.LinuxTime, WaveLabelPosition.BOTTOM );

            foreach ( var elliottWave in bottomWaves )
            {
                var waveCycle = elliottWave.WaveCycle;

                if ( waveCycle != lastWaveCycle )
                {
                    lastWaveCycle = waveCycle;

                    if ( mySortedListString.ContainsKey( waveCycle ) )
                    {
                        var waveString = mySortedListString[ waveCycle ];

                        var drawString = waveString;

                        var fontSize   = GetWaveFontSize( waveCycle );
                        var textBlock  = new TextBlock( ) { FontFamily = _fontFamily, FontSize = fontSize, FontStyle = _fontStyle, FontWeight = _fontWeight };
                        var size       = drawString.Measure( textBlock );

                        var waveColor  = GetWaveColor( waveCycle, true );

                        var drawY      = _lastBottomPosition > 0 ? _lastBottomPosition + SignalMargin : lowY;

                        var drawPoint  = new Point( xCoor - size.Width / 2, drawY );

                        var drawRect   = new Rect( drawPoint, size );
                        
                        if ( HighQualityWaveText )
                        {
                            if ( !_waveAnnotationAdded.ContainsKey( waveKey ) )
                            {
                                var scichart = GetParentSurface( );

                                IComparable x = 0;

                                ICategoryCoordinateCalculator<DateTime> coordinateCalculator = XAxis.GetCurrentCoordinateCalculator( ) as ICategoryCoordinateCalculator<DateTime>;
                                if ( coordinateCalculator != null )
                                {
                                    x = coordinateCalculator.TransformDataToIndex( barData.BarTime );
                                }

                                var waveText                   = new TextAnnotation( );
                                waveText.FontFamily            = _fontFamily;
                                waveText.FontSize              = fontSize;
                                waveText.FontWeight            = _fontWeight;
                                waveText.FontStyle             = _fontStyle;
                                waveText.Text                  = drawString;
                                waveText.VerticalAnchorPoint   = VerticalAnchorPoint.Center;
                                waveText.HorizontalAnchorPoint = HorizontalAnchorPoint.Center;
                                waveText.Foreground            = new SolidColorBrush( waveColor );
                                waveText.XAxisId               = "X";
                                waveText.YAxisId               = "Y";
                                waveText.X1                    = x;
                                waveText.Y1                    = YAxis.GetDataValue( drawY );  //barData.High;

                                scichart.Annotations.Add( waveText );

                                toBeAddedBot.Add( waveText );
                            }
                            else
                            {
                                var waveList = _waveAnnotationAdded.GetOrAddValueRef( waveKey );

                                foreach ( var waveText in waveList )
                                {
                                    if ( waveText.Text == drawString )
                                    {
                                        waveText.FontFamily = _fontFamily;
                                        waveText.FontSize   = fontSize;
                                        waveText.FontWeight = _fontWeight;
                                        waveText.FontStyle  = _fontStyle;
                                        waveText.Text       = drawString;
                                        waveText.Foreground = new SolidColorBrush( waveColor );
                                        waveText.Y1         = YAxis.GetDataValue( drawY );
                                        waveText.IsHidden   = false;
                                    }
                                }
                            }

                        }
                        else
                        {
                            g.DrawText( drawRect, waveColor, fontSize, drawString, _fontFamily, _fontWeight, _fontStyle );
                        }

                        _lastBottomPosition = drawY + size.Height;

                        mySortedListString.Remove( waveCycle );
                    }
                }
            }

            if ( toBeAddedBot.Count > 0 )
            {
                _waveAnnotationAdded.GetOrAddValueRef( waveKey ) = toBeAddedBot;                
            }

            return mySortedListString;
        }

        private bool DrawElliottWaveLabelsOnTop( IRenderContext2D g, ref SBar barData, double xCoor, double highY, double lowY, int waveScenarioNo, bool isFilterWaves )
        {            
            var totalWaves         = _minimumToShow == ElliottWaveCycle.UNKNOWN ? barData.GetAllWaves( waveScenarioNo, isFilterWaves ) : barData.GetAllWaves( waveScenarioNo, isFilterWaves, _minimumToShow );
            var retractStr         = barData.GetPriceTimeInfoString( waveScenarioNo );
            var toBeAddedTop       = new PooledList<TextAnnotation>( );            

            var mySortedList       = totalWaves.OrderBy( l => l.WaveCycle ).ThenBy( l => l.WaveName );
            var mySortedListString = GetFilteredCycleWaveString( mySortedList, WaveLabelPosition.TOP );
            
            var waveKey            = new WaveAnnotationKey( barData.LinuxTime, WaveLabelPosition.TOP );

            if ( barData.WaveDirty == WaveDirtyEnum.Change || _needToRedrawElliottWave )    
            {
                RemoveWavesFromChart( ref barData );
                barData.WaveDirty = WaveDirtyEnum.NONE;

            }

            foreach ( var elliottWave in mySortedList )
            {
                var waveCycle = elliottWave.WaveCycle;
                var waveName  = elliottWave.WaveName;

                if ( mySortedListString.ContainsKey( waveCycle ) )
                {
                    var waveString   = mySortedListString[ waveCycle ];

                    var drawString   = waveString;                    

                    var fontSize     = GetWaveFontSize( waveCycle );
                    var textBlock    = new TextBlock( ) { FontFamily = _fontFamily, FontSize = fontSize, FontStyle = _fontStyle, FontWeight = _fontWeight };
                    var size         = drawString.Measure( textBlock );

                    var waveColor    = GetWaveColor( waveCycle, true );

                    var drawY        = _lastTopPosition > 0 ? _lastTopPosition : highY;

                    

                    _lastTopPosition = drawY - SignalMargin - size.Height;

                    

                    if ( HighQualityWaveText )
                    {
                        if ( !_waveAnnotationAdded.ContainsKey( waveKey ) )
                        {
                            AddNewWavesToTop( ref barData, toBeAddedTop, drawString, fontSize, waveColor );
                        }
                        else
                        {
                            bool found = false;
                            var waveList = _waveAnnotationAdded.GetOrAddValueRef( waveKey );

                            foreach ( var waveText in waveList )
                            {
                                if ( waveText.Text == drawString )
                                {
                                    waveText.FontFamily = _fontFamily;
                                    waveText.FontSize   = fontSize;
                                    waveText.FontWeight = _fontWeight;
                                    waveText.FontStyle  = _fontStyle;
                                    waveText.Text       = drawString;
                                    waveText.Foreground = new SolidColorBrush( waveColor );                                    
                                    waveText.Y1         = YAxis.GetDataValue( _lastTopPosition );
                                    waveText.IsHidden   = false;
                                    found               = true;
                                }
                            }

                            if ( ! found )
                            {                                
                                AddNewWavesToTop( ref barData, waveList, drawString, fontSize, waveColor );

                                _waveAnnotationAdded.GetOrAddValueRef( waveKey ) = waveList;                                
                            }
                        }

                    }
                    else
                    {
                        var drawPoint = new Point(xCoor - size.Width / 2, drawY - 10 - size.Height);
                        var drawRect = new Rect(drawPoint, size);
                        g.DrawText( drawRect, waveColor, fontSize, drawString, _fontFamily, _fontWeight, _fontStyle );
                    }

                    //g.DrawText( new Rect( drawPoint, size ), waveColor, fontSize, drawString, _fontFamily, _fontWeight, _fontStyle );
                    

                    mySortedListString.Remove( waveCycle );
                }

            }

            if ( toBeAddedTop.Count > 0 )
            {
                _waveAnnotationAdded.GetOrAddValueRef( waveKey ) = toBeAddedTop;                   
            }
            return false;
        }

        private void AddNewWavesToTop( ref SBar barData, PooledList<TextAnnotation> annotationsList, string drawString, float fontSize, Color waveColor )
        {
            var scichart = GetParentSurface( );

            IComparable x = 0;

            ICategoryCoordinateCalculator<DateTime> coordinateCalculator = XAxis.GetCurrentCoordinateCalculator( ) as ICategoryCoordinateCalculator<DateTime>;
            if ( coordinateCalculator != null )
            {
                x = coordinateCalculator.TransformDataToIndex( barData.BarTime );
            }

            var waveText                   = new TextAnnotation( );
            waveText.FontFamily            = _fontFamily;
            waveText.FontSize              = fontSize;
            waveText.FontWeight            = _fontWeight;
            waveText.FontStyle             = _fontStyle;
            waveText.Text                  = drawString;
            waveText.Foreground            = new SolidColorBrush( waveColor );
            waveText.XAxisId               = "X";
            waveText.YAxisId               = "Y";
            waveText.VerticalAnchorPoint   = VerticalAnchorPoint.Center;
            waveText.HorizontalAnchorPoint = HorizontalAnchorPoint.Center;
            waveText.X1                    = x;
            waveText.Y1                    = YAxis.GetDataValue( _lastTopPosition );  //barData.High;

            scichart.Annotations.Add( waveText );

            annotationsList.Add( waveText );
        }

        private void AddStructureLabelToTop( ref SBar barData, PooledList<TextAnnotation> annotationsList, string drawString, float fontSize, Color waveColor )
        {
            var scichart = GetParentSurface( );

            IComparable x = 0;

            ICategoryCoordinateCalculator<DateTime> coordinateCalculator = XAxis.GetCurrentCoordinateCalculator( ) as ICategoryCoordinateCalculator<DateTime>;
            if ( coordinateCalculator != null )
            {
                x = coordinateCalculator.TransformDataToIndex( barData.BarTime );
            }

            var waveText                   = new TextAnnotation( );
            waveText.FontFamily            = _fontFamily;
            waveText.FontSize              = fontSize;
            waveText.FontWeight            = _fontWeight;
            waveText.FontStyle             = _fontStyle;
            waveText.Text                  = drawString;
            waveText.Foreground            = new SolidColorBrush( waveColor );
            waveText.XAxisId               = "X";
            waveText.YAxisId               = "Y";
            waveText.VerticalAnchorPoint   = VerticalAnchorPoint.Center;
            waveText.HorizontalAnchorPoint = HorizontalAnchorPoint.Center;
            waveText.X1                    = x;
            waveText.Y1                    = YAxis.GetDataValue( _lastTopPosition );  //barData.High;
            waveText.Tag = "StructuralLabel";

            scichart.Annotations.Add( waveText );

            annotationsList.Add( waveText );
        }

        private bool DrawElliottWaveLabelsOnBottom( IRenderContext2D g, ref SBar barData, double xCoor, double highY, double lowY, int waveScenarioNo, bool isFilterWaves )
        {
            var totalWaves         = _minimumToShow == ElliottWaveCycle.UNKNOWN ? barData.GetAllWaves( waveScenarioNo, isFilterWaves ) : barData.GetAllWaves( waveScenarioNo, isFilterWaves, _minimumToShow );
            var retractStr         = barData.GetPriceTimeInfoString( waveScenarioNo );
            
            var mySortedList       = totalWaves.OrderBy( l => l.WaveCycle ).ThenBy( l => l.WaveName );
            var mySortedListString = GetFilteredCycleWaveString( mySortedList, WaveLabelPosition.BOTTOM );
            
            var toBeAddedBot       = new PooledList<TextAnnotation>( );

            var waveKey            = new WaveAnnotationKey( barData.LinuxTime, WaveLabelPosition.BOTTOM );

            if ( barData.WaveDirty == WaveDirtyEnum.Change || _needToRedrawElliottWave )
            {
                RemoveWavesFromChart( ref barData );
                barData.WaveDirty = WaveDirtyEnum.NONE;

            }

            foreach ( var elliottWave in mySortedList )
            {
                var waveCycle = elliottWave.WaveCycle;
                var waveName = elliottWave.WaveName;

                if ( mySortedListString.ContainsKey( waveCycle ) )
                {
                    var waveString = mySortedListString[ waveCycle ];

                    var drawString = waveString;                    

                    var fontSize   = GetWaveFontSize( waveCycle );
                    var textBlock  = new TextBlock( ) { FontFamily = _fontFamily, FontSize = fontSize, FontStyle = _fontStyle, FontWeight = _fontWeight };
                    var size       = drawString.Measure( textBlock );

                    var waveColor  = GetWaveColor( waveCycle, true );

                    var drawY      = _lastBottomPosition > 0 ? _lastBottomPosition + SignalMargin  : lowY;

                    var drawPoint  = new Point( xCoor - size.Width / 2, drawY  );

                    var drawRect = new Rect( drawPoint, size );                    

                    if ( HighQualityWaveText )
                    {
                        if ( ! _waveAnnotationAdded.ContainsKey( waveKey ) )
                        {
                            AddNewWavesToBottom( ref barData, toBeAddedBot, drawString, fontSize, waveColor, drawY );
                        }
                        else
                        {
                            bool found = false;
                            var waveList = _waveAnnotationAdded.GetOrAddValueRef( waveKey );

                            foreach ( var waveText in waveList )
                            {
                                if ( waveText.Text == drawString )
                                {
                                    waveText.FontFamily = _fontFamily;
                                    waveText.FontSize   = fontSize;
                                    waveText.FontWeight = _fontWeight;
                                    waveText.FontStyle  = _fontStyle;
                                    waveText.Text       = drawString;
                                    waveText.Foreground = new SolidColorBrush( waveColor );
                                    waveText.Y1         = YAxis.GetDataValue( drawY );
                                    waveText.IsHidden   = false;
                                    found               = true;
                                }
                            }

                            if ( !found )
                            {
                                AddNewWavesToBottom( ref barData, waveList, drawString, fontSize, waveColor, drawY );

                                _waveAnnotationAdded.GetOrAddValueRef( waveKey ) = waveList;
                            }
                        }                        
                    }
                    else
                    {
                        g.DrawText( drawRect, waveColor, fontSize, drawString, _fontFamily, _fontWeight, _fontStyle );
                    }

                    //g.DrawText( new Rect( drawPoint, size ), waveColor, fontSize, drawString, _fontFamily, _fontWeight, _fontStyle );

                    _lastBottomPosition = drawY + size.Height;

                    mySortedListString.Remove( waveCycle );
                }

            }

            if ( toBeAddedBot.Count > 0 )
            {
                _waveAnnotationAdded.GetOrAddValueRef( waveKey ) = toBeAddedBot;                   
            }

            return false;
        }

        private void AddNewWavesToBottom( ref SBar barData, PooledList<TextAnnotation> toBeAddedBot, string drawString, float fontSize, Color waveColor, double drawY )
        {
            var scichart = GetParentSurface( );

            IComparable x = 0;

            ICategoryCoordinateCalculator<DateTime> coordinateCalculator = XAxis.GetCurrentCoordinateCalculator( ) as ICategoryCoordinateCalculator<DateTime>;
            if ( coordinateCalculator != null )
            {
                x = coordinateCalculator.TransformDataToIndex( barData.BarTime );
            }

            var waveText                   = new TextAnnotation( );
            waveText.FontFamily            = _fontFamily;
            waveText.FontSize              = fontSize;
            waveText.FontWeight            = _fontWeight;
            waveText.FontStyle             = _fontStyle;
            waveText.Text                  = drawString;
            waveText.VerticalAnchorPoint   = VerticalAnchorPoint.Center;
            waveText.HorizontalAnchorPoint = HorizontalAnchorPoint.Center;
            waveText.Foreground            = new SolidColorBrush( waveColor );
            waveText.XAxisId               = "X";
            waveText.YAxisId               = "Y";
            waveText.X1                    = x;
            waveText.Y1                    = YAxis.GetDataValue( drawY );  //barData.High;

            scichart.Annotations.Add( waveText );

            toBeAddedBot.Add( waveText );
        }

        private void AddStructureLabelToBottom( ref SBar barData, PooledList<TextAnnotation> toBeAddedBot, string drawString, float fontSize, Color waveColor, double drawY )
        {
            var scichart = GetParentSurface( );

            IComparable x = 0;

            ICategoryCoordinateCalculator<DateTime> coordinateCalculator = XAxis.GetCurrentCoordinateCalculator( ) as ICategoryCoordinateCalculator<DateTime>;
            if ( coordinateCalculator != null )
            {
                x = coordinateCalculator.TransformDataToIndex( barData.BarTime );
            }

            var waveText                   = new TextAnnotation( );
            waveText.FontFamily            = _fontFamily;
            waveText.FontSize              = fontSize;
            waveText.FontWeight            = _fontWeight;
            waveText.FontStyle             = _fontStyle;
            waveText.Text                  = drawString;
            waveText.VerticalAnchorPoint   = VerticalAnchorPoint.Center;
            waveText.HorizontalAnchorPoint = HorizontalAnchorPoint.Center;
            waveText.Foreground            = new SolidColorBrush( waveColor );
            waveText.XAxisId               = "X";
            waveText.YAxisId               = "Y";
            waveText.X1                    = x;
            waveText.Y1                    = YAxis.GetDataValue( drawY );  //barData.High;
            waveText.Tag = "StructuralLabel";

            scichart.Annotations.Add( waveText );

            toBeAddedBot.Add( waveText );
        }

        

        private bool DrawElliottWaveLabelsOnCandle( IRenderContext2D g, ref SBar barData, IPenManager penManager, double xCoor, double highY, double lowY, int waveScenarioNo, bool isFilterWaves )
        {
            var hew = barData.GetWaveFromScenario( WaveScenarioNo );

            if ( ! hew.HasElliottWave )                
                return false;
                        
            var totalWaves      = barData.GetAllWaves( waveScenarioNo, isFilterWaves );
            var labelPos        = barData.GetWaveLabelPosition( waveScenarioNo );           

            var tmpList         = new PooledList<TASignal>( );
            int totalSignal     = barData.TechnicalAnalysisSignalCount( ref tmpList );

            int iconIdx = 0;

            for ( int i = 0; i < totalSignal; i++ )
            {
                iconIdx += DrawTASignal( g, ref barData, penManager, xCoor, highY, lowY, iconIdx + 1, tmpList[ i ] );                
            }


            if ( totalWaves != null && totalWaves.Count > 0 )
            {
                foreach ( var wave in totalWaves )
                {
                    if ( wave.WaveCycle < _lowestWaveDeg )
                    {
                        _lowestWaveDeg = wave.WaveCycle;
                    }

                    if ( wave.WaveCycle > _higestWaveDeg )
                    {
                        _higestWaveDeg = wave.WaveCycle;
                    }
                }

                if ( labelPos == WaveLabelPosition.BOTH )
                {
                    DrawElliottWaveLabelsBoth( g, ref barData, xCoor, highY, lowY, waveScenarioNo, isFilterWaves );
                }
                else if ( labelPos == WaveLabelPosition.TOP )
                {
                    DrawElliottWaveLabelsOnTop( g, ref barData, xCoor, highY, lowY, waveScenarioNo, isFilterWaves );
                    
                }
                else if ( labelPos == WaveLabelPosition.BOTTOM )
                {
                    DrawElliottWaveLabelsOnBottom( g, ref barData, xCoor, highY, lowY, waveScenarioNo, isFilterWaves );                    
                }

                if ( !barData.HasFibRetExpInfo )
                {
                    return false;
                }

                var retractStr = barData.GetPriceTimeInfoString( waveScenarioNo );

                if ( string.IsNullOrEmpty( retractStr ) )
                    return false;

                var filteredWaves  = _minimumToShow == ElliottWaveCycle.UNKNOWN ? barData.GetAllWaves( waveScenarioNo, isFilterWaves ) : barData.GetAllWaves( waveScenarioNo, isFilterWaves, _minimumToShow );

                if( filteredWaves.Count <= 0 )
                    return true;

                var toBeAddedTop = new PooledList<TextAnnotation>( );

                if ( labelPos == WaveLabelPosition.TOP )
                {
                    var waveKey      = new WaveAnnotationKey( barData.LinuxTime, WaveLabelPosition.TOP );

                    var drawString   = retractStr;

                    var fontSize     = GetWaveFontSize( ElliottWaveCycle.Miniscule );
                    var textBlock    = new TextBlock( ) { FontFamily = _fontFamily, FontSize = fontSize, FontStyle = _fontStyle, FontWeight = _fontWeight };
                    var size         = drawString.Measure( textBlock );

                    var waveColor    = GetWaveColor( ElliottWaveCycle.Miniscule, true );

                    var drawY        = _lastTopPosition > 0 ? _lastTopPosition : highY;

                    var drawPoint    = new Point( xCoor - size.Width / 2, drawY - 10 - size.Height );

                    _lastTopPosition = drawY - SignalMargin - size.Height;

                    var drawRect     = new Rect( drawPoint, size );

                    if ( HighQualityWaveText )
                    {
                        if ( !_waveAnnotationAdded.ContainsKey( waveKey ) )
                        {
                            AddNewWavesToTop( ref barData, toBeAddedTop, drawString, fontSize, waveColor );
                        }
                        else
                        {
                            bool found = false;
                            var waveList = _waveAnnotationAdded.GetOrAddValueRef( waveKey );

                            foreach ( var waveText in waveList )
                            {
                                if ( waveText.Text == drawString )
                                {
                                    waveText.FontFamily = _fontFamily;
                                    waveText.FontSize   = fontSize;
                                    waveText.FontWeight = _fontWeight;
                                    waveText.FontStyle  = _fontStyle;
                                    waveText.Text       = drawString;
                                    waveText.Foreground = new SolidColorBrush( waveColor );
                                    waveText.Y1         = YAxis.GetDataValue( _lastTopPosition );
                                    waveText.IsHidden   = false;
                                    found               = true;
                                }
                            }

                            if ( !found )
                            {
                                AddNewWavesToTop( ref barData, waveList, drawString, fontSize, waveColor );

                                _waveAnnotationAdded.GetOrAddValueRef( waveKey ) = waveList;
                            }
                        }

                    }

                }
                else if ( labelPos == WaveLabelPosition.BOTTOM )
                {
                    var toBeAddedBot = new PooledList<TextAnnotation>( );

                    var waveKey = new WaveAnnotationKey( barData.LinuxTime, WaveLabelPosition.BOTTOM );

                    var drawString = retractStr;

                    var fontSize = GetWaveFontSize( ElliottWaveCycle.Miniscule );
                    var textBlock = new TextBlock( ) { FontFamily = _fontFamily, FontSize = fontSize, FontStyle = _fontStyle, FontWeight = _fontWeight };
                    var size = drawString.Measure( textBlock );

                    var waveColor = GetWaveColor( ElliottWaveCycle.Miniscule, true );

                    var drawY = _lastBottomPosition > 0 ? _lastBottomPosition + SignalMargin : lowY;

                    var drawPoint = new Point( xCoor - size.Width / 2, drawY );

                    var drawRect = new Rect( drawPoint, size );

                    if ( HighQualityWaveText )
                    {
                        if ( !_waveAnnotationAdded.ContainsKey( waveKey ) )
                        {
                            AddNewWavesToBottom( ref barData, toBeAddedBot, drawString, fontSize, waveColor, drawY );
                        }
                        else
                        {
                            bool found = false;
                            var waveList = _waveAnnotationAdded.GetOrAddValueRef( waveKey );

                            foreach ( var waveText in waveList )
                            {
                                if ( waveText.Text == drawString )
                                {
                                    waveText.FontFamily = _fontFamily;
                                    waveText.FontSize   = fontSize;
                                    waveText.FontWeight = _fontWeight;
                                    waveText.FontStyle  = _fontStyle;
                                    waveText.Text       = drawString;
                                    waveText.Foreground = new SolidColorBrush( waveColor );
                                    waveText.Y1         = YAxis.GetDataValue( drawY );
                                    waveText.IsHidden   = false;
                                    found               = true;
                                }
                            }

                            if ( !found )
                            {
                                AddNewWavesToBottom( ref barData, waveList, drawString, fontSize, waveColor, drawY );
                               
                                _waveAnnotationAdded.GetOrAddValueRef( waveKey ) = waveList;
                            }
                        }

                        _lastBottomPosition = drawY + size.Height;
                    }
                }
            }





            return true;
        }

        private bool DrawElliottWaveLabelsOnCandleNoTA( IRenderContext2D g, ref SBar barData, IPenManager penManager, double xCoor, double highY, double lowY, int waveScenarioNo, bool isFilterWaves )
        {
            var hew = barData.GetWaveFromScenario( WaveScenarioNo );

            if ( !hew.HasElliottWave )
                return false;

            var totalWaves      = barData.GetAllWaves( waveScenarioNo, isFilterWaves );
            var labelPos        = barData.GetWaveLabelPosition( waveScenarioNo );

            var tmpList         = new PooledList<TASignal>( );

            if ( WaveScenarioNo == 1 )
            {
                int totalSignal     = barData.TechnicalAnalysisSignalCount( ref tmpList );

                int iconIdx = 0;

                for ( int i = 0; i < totalSignal; i++ )
                {
                    iconIdx += DrawReducedEWave( g, ref barData, penManager, xCoor, highY, lowY, iconIdx + 1, tmpList[ i ] );
                }
            }
            

            if ( totalWaves != null && totalWaves.Count > 0 )
            {
                foreach ( var wave in totalWaves )
                {
                    if ( wave.WaveCycle < _lowestWaveDeg )
                    {
                        _lowestWaveDeg = wave.WaveCycle;
                    }

                    if ( wave.WaveCycle > _higestWaveDeg )
                    {
                        _higestWaveDeg = wave.WaveCycle;
                    }
                }

                if ( totalWaves.Count > 1 )
                {
                    _lastDrawWaveIndex = barData.Index;
                }

                //if ( totalWaves.Count == 1 )
                //{
                //    if ( totalWaves.First().WaveName == ElliottWaveEnum.WaveA || totalWaves.First().WaveName == ElliottWaveEnum.WaveB && totalWaves.First().WaveCycle < ElliottWaveCycle.Supercycle )
                //    {
                //        var currentIndex = (int)barData.Index;

                //        if ( Math.Abs ( currentIndex - _lastDrawWaveIndex ) < 20 )
                //        {
                //            if ( totalWaves.First( ).WaveCycle <= _lastWaveCycle )
                //            {
                //                barData.WaveDirty = WaveDirtyEnum.Change;
                //                RemoveWavesFromChart( ref barData );
                //                _lastDrawWaveIndex = ( int ) barData.Index;

                //                return false;
                //            }                            
                //        }
                //    }

                //    _lastDrawWaveIndex = (int)barData.Index;
                //    _lastWaveCycle     = totalWaves.First( ).WaveCycle;
                //}


                if ( labelPos == WaveLabelPosition.BOTH )
                {
                    DrawElliottWaveLabelsBoth( g, ref barData, xCoor, highY, lowY, waveScenarioNo, isFilterWaves );
                }
                else if ( labelPos == WaveLabelPosition.TOP )
                {
                    DrawElliottWaveLabelsOnTop( g, ref barData, xCoor, highY, lowY, waveScenarioNo, isFilterWaves );
                }
                else if ( labelPos == WaveLabelPosition.BOTTOM )
                {
                    DrawElliottWaveLabelsOnBottom( g, ref barData, xCoor, highY, lowY, waveScenarioNo, isFilterWaves );
                }                

                if ( !barData.HasFibRetExpInfo )
                {
                    return false;
                }

                var retractStr = barData.GetPriceTimeInfoString( waveScenarioNo );

                if ( string.IsNullOrEmpty( retractStr ) )
                    return false;

                var filteredWaves  = _minimumToShow == ElliottWaveCycle.UNKNOWN ? barData.GetAllWaves( waveScenarioNo, isFilterWaves ) : barData.GetAllWaves( waveScenarioNo, isFilterWaves, _minimumToShow );

                if ( filteredWaves.Count <= 0 )
                    return true;

                var toBeAddedTop = new PooledList<TextAnnotation>( );

                if ( labelPos == WaveLabelPosition.TOP )
                {
                    var waveKey      = new WaveAnnotationKey( barData.LinuxTime, WaveLabelPosition.TOP );

                    var drawString   = retractStr;

                    var fontSize     = GetWaveFontSize( ElliottWaveCycle.Miniscule );
                    var textBlock    = new TextBlock( ) { FontFamily = _fontFamily, FontSize = fontSize, FontStyle = _fontStyle, FontWeight = _fontWeight };
                    var size         = drawString.Measure( textBlock );

                    var waveColor    = GetWaveColor( ElliottWaveCycle.Miniscule, true );

                    var drawY        = _lastTopPosition > 0 ? _lastTopPosition : highY;

                    var drawPoint    = new Point( xCoor - size.Width / 2, drawY - 10 - size.Height );

                    _lastTopPosition = drawY - SignalMargin - size.Height;

                    var drawRect     = new Rect( drawPoint, size );

                    if ( HighQualityWaveText )
                    {
                        if ( !_waveAnnotationAdded.ContainsKey( waveKey ) )
                        {
                            AddNewWavesToTop( ref barData, toBeAddedTop, drawString, fontSize, waveColor );
                        }
                        else
                        {
                            bool found = false;
                            var waveList = _waveAnnotationAdded.GetOrAddValueRef( waveKey );

                            foreach ( var waveText in waveList )
                            {
                                if ( waveText.Text == drawString )
                                {
                                    waveText.FontFamily = _fontFamily;
                                    waveText.FontSize   = fontSize;
                                    waveText.FontWeight = _fontWeight;
                                    waveText.FontStyle  = _fontStyle;
                                    waveText.Text       = drawString;
                                    waveText.Foreground = new SolidColorBrush( waveColor );
                                    waveText.Y1         = YAxis.GetDataValue( _lastTopPosition );
                                    waveText.IsHidden   = false;
                                    found               = true;
                                }
                            }

                            if ( !found )
                            {
                                AddNewWavesToTop( ref barData, waveList, drawString, fontSize, waveColor );

                                _waveAnnotationAdded.GetOrAddValueRef( waveKey ) = waveList;
                            }
                        }

                    }

                }
                else if ( labelPos == WaveLabelPosition.BOTTOM )
                {
                    var toBeAddedBot = new PooledList<TextAnnotation>( );

                    var waveKey = new WaveAnnotationKey( barData.LinuxTime, WaveLabelPosition.BOTTOM );

                    var drawString = retractStr;

                    var fontSize = GetWaveFontSize( ElliottWaveCycle.Miniscule );
                    var textBlock = new TextBlock( ) { FontFamily = _fontFamily, FontSize = fontSize, FontStyle = _fontStyle, FontWeight = _fontWeight };
                    var size = drawString.Measure( textBlock );

                    var waveColor = GetWaveColor( ElliottWaveCycle.Miniscule, true );

                    var drawY = _lastBottomPosition > 0 ? _lastBottomPosition + SignalMargin : lowY;

                    var drawPoint = new Point( xCoor - size.Width / 2, drawY );

                    var drawRect = new Rect( drawPoint, size );

                    if ( HighQualityWaveText )
                    {
                        if ( !_waveAnnotationAdded.ContainsKey( waveKey ) )
                        {
                            AddNewWavesToBottom( ref barData, toBeAddedBot, drawString, fontSize, waveColor, drawY );
                        }
                        else
                        {
                            bool found = false;
                            var waveList = _waveAnnotationAdded.GetOrAddValueRef( waveKey );

                            foreach ( var waveText in waveList )
                            {
                                if ( waveText.Text == drawString )
                                {
                                    waveText.FontFamily = _fontFamily;
                                    waveText.FontSize   = fontSize;
                                    waveText.FontWeight = _fontWeight;
                                    waveText.FontStyle  = _fontStyle;
                                    waveText.Text       = drawString;
                                    waveText.Foreground = new SolidColorBrush( waveColor );
                                    waveText.Y1         = YAxis.GetDataValue( drawY );
                                    waveText.IsHidden   = false;
                                    found               = true;
                                }
                            }

                            if ( !found )
                            {
                                AddNewWavesToBottom( ref barData, waveList, drawString, fontSize, waveColor, drawY );

                                _waveAnnotationAdded.GetOrAddValueRef( waveKey ) = waveList;
                            }
                        }

                        _lastBottomPosition = drawY + size.Height;
                    }
                }
            }





            return true;
        }

        private int DrawReducedEWave( IRenderContext2D g, ref SBar barData, IPenManager penManager, double xCoor, double highY, double lowY, int i, TASignal dataBarSignals )
        {
            var symbol    = barData.SymbolEx;
            var period    = barData.BarPeriod;

            var aa = (AdvancedAnalysisManager) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( symbol );

            PeriodXTaManager taManager = null;

            if ( aa != null )
            {
                taManager = ( PeriodXTaManager ) aa.GetPeriodXTa( period );
            }

            if ( taManager == null )
                return -1;

            int count     = 0;

            switch ( dataBarSignals )
            {
                case TASignal.WAVE_PEAK:
                {
                    if ( ShowElliottWave )
                    {
                        var zigZag = taManager.ZigZagResult;

                        if ( zigZag.ContainsKey( barData.LinuxTime ) )
                        {
                            if ( _hews != null )
                            {
                                var waveImp = _hews.GetWaveImportanceExt( period, barData.LinuxTime );

                                if ( waveImp.WaveImportance >= WaveImportance )
                                {
                                    DrawSmallWavePeak( TASignalSymbol.WaveImportance, g, xCoor, highY, lowY, ref barData );
                                }
                            }
                            
                        }
                    }
                }
                break;

                case TASignal.WAVE_TROUGH:
                {
                    if ( ShowElliottWave )
                    {
                        var zigZag = taManager.ZigZagResult;

                        if ( zigZag.ContainsKey( barData.LinuxTime ) )
                        {
                            var waveImp = _hews.GetWaveImportanceExt( period, barData.LinuxTime );

                            if ( waveImp.WaveImportance >= WaveImportance )
                            {
                                DrawSmallWaveTrough( TASignalSymbol.WaveImportance, g, xCoor, highY, lowY, ref barData );
                            }
                            
                        }
                    }
                }
                break;

                
                case TASignal.HAS_DIVERGENCE:
                {
                    if ( barData.Index == taManager.LatestDivergenceIndex )
                    {
                        var divergences = taManager.GetDivergenceInfoForDrawing( ref barData );

                        if ( divergences != null )
                        {
                            foreach ( var d in divergences )
                            {
                                DrawDivergence( d.Divergence, g, xCoor, highY, lowY, ref barData, i + count );
                                count++;
                            }
                        }
                    }

                    return count;
                }


            }

            return count;
        }


        private int DrawEWaveAndLatestTASignal( IRenderContext2D g, ref SBar barData, IPenManager penManager, double xCoor, double highY, double lowY, int i, TASignal dataBarSignals )
        {
            var symbol    = barData.SymbolEx;
            var period    = barData.BarPeriod;

            var aa = (AdvancedAnalysisManager) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( symbol );

            PeriodXTaManager taManager = null;

            if ( aa != null )
            {
                taManager = ( PeriodXTaManager ) aa.GetPeriodXTa( period );
            }

            if ( taManager == null )
                return -1;

            int count     = 0;

            switch ( dataBarSignals )
            {
                case TASignal.WAVE_PEAK:
                {
                    if ( ShowElliottWave )
                    {
                        var zigZag = taManager.ZigZagResult;

                        if ( zigZag.ContainsKey( barData.LinuxTime ) )
                        {
                            DrawWavePeak( TASignalSymbol.WaveImportance, g, xCoor, highY, lowY, ref barData );
                        }
                    }
                }
                break;

                case TASignal.WAVE_TROUGH:
                {
                    if ( ShowElliottWave )
                    {
                        var zigZag = taManager.ZigZagResult;

                        if ( zigZag.ContainsKey( barData.LinuxTime ) )
                        {
                            DrawWaveTrough( TASignalSymbol.WaveImportance, g, xCoor, highY, lowY, ref barData );
                        }
                    }
                }
                break;

                case TASignal.GANN_PEAK:
                {
                    if ( ShowElliottWave )
                    {
                        DrawGannPeak( TASignalSymbol.GannImportance, g, xCoor, highY, lowY, ref barData );
                    }
                }
                break;

                case TASignal.GANN_TROUGH:
                {
                    if ( ShowElliottWave )
                    {
                        DrawGannTrough( TASignalSymbol.GannImportance, g, xCoor, highY, lowY, ref barData );
                    }
                }
                break;

                case TASignal.HAS_TOPPING_SIGNAL:
                {   
                    if ( barData.Index == taManager.LatestTopSignalIndex )
                    {
                        var toppingSignals = taManager.GetToppingSignalsInfo( ref barData );

                        if ( toppingSignals != null )
                        {
                            foreach ( TAToppingSignal signal in toppingSignals )
                            {
                                DrawToppingSignal( signal, g, xCoor, highY, lowY, ref barData, _candleWidth, i + count );
                                count++;
                            }
                        }
                    }
                   
                    return count;
                }

                case TASignal.HAS_BOTTOMING_SIGNAL:
                {
                    if ( barData.Index == taManager.LatestBottomSignalIndex )
                    {
                        var bottomSignals = taManager.GetBottomingSignalsInfo( ref barData );

                        if ( bottomSignals != null )
                        {
                            foreach ( var signal in bottomSignals )
                            {
                                DrawBottomingSignal( signal, g, xCoor, highY, lowY, ref barData, _candleWidth, i + count );
                                count++;
                            }
                        }
                    }
                    

                    return count;
                }

                case TASignal.HAS_DIVERGENCE:
                {
                    if ( barData.Index == taManager.LatestDivergenceIndex )
                    {
                        var divergences = taManager.GetDivergenceInfoForDrawing( ref barData );

                        if ( divergences != null )
                        {
                            foreach ( var d in divergences )
                            {
                                DrawDivergence( d.Divergence, g, xCoor, highY, lowY, ref barData, i + count );
                                count++;
                            }
                        }
                    }
                    
                    return count;
                }

                
                case TASignal.HAS_TIME_ROTATION:
                {
                    
                    if ( barData.Index == taManager.LatestWaveRotationIndex )
                    {
                        var waveImportantTimeInfo = taManager.GetWaveImportantTimeInfo( ref barData );

                        if ( waveImportantTimeInfo != null )
                        {
                            waveImportantTimeInfo.Sort();

                            foreach ( var wave in waveImportantTimeInfo )
                            {
                                DrawWaveImportantTimeInfo( wave, g, xCoor, highY, lowY, ref barData, i + count );
                                count++;
                            }
                        }
                    }
                    

                    return count;
                }
            }

            return count;
        }


        private int DrawTASignal( IRenderContext2D g, ref SBar barData, IPenManager penManager, double xCoor, double highY, double lowY, int i, TASignal dataBarSignals )
        {
            var symbol    = barData.SymbolEx;
            var period    = barData.BarPeriod;

            var aa = (AdvancedAnalysisManager) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( symbol );

            PeriodXTaManager taManager = null;

            if ( aa != null )
            {
                taManager = ( PeriodXTaManager ) aa.GetPeriodXTa( period );
            }

            if( taManager == null )
                return -1;            

            int count     = 0;

            switch ( dataBarSignals )
            {
                case TASignal.HAS_TOPPING_SIGNAL:
                {
                    if ( ShowIndicatorResult )
                    {
                        var toppingSignals = taManager.GetToppingSignalsInfo( ref barData );

                        if ( toppingSignals != null )
                        {
                            foreach ( TAToppingSignal signal in toppingSignals )
                            {
                                DrawToppingSignal( signal, g, xCoor, highY, lowY, ref barData, _candleWidth, i + count );
                                count++;
                            }
                        }                        
                    }
                    else
                    {
                        if ( barData.Index == taManager.LatestTopSignalIndex )
                        {
                            var toppingSignals = taManager.GetToppingSignalsInfo( ref barData );

                            if ( toppingSignals != null )
                            {
                                foreach ( TAToppingSignal signal in toppingSignals )
                                {
                                    DrawToppingSignal( signal, g, xCoor, highY, lowY, ref barData, _candleWidth, i + count );
                                    count++;
                                }                                
                            }
                        }
                    }

                    return count;
                }                

                case TASignal.HAS_BOTTOMING_SIGNAL:
                {
                    if ( ShowIndicatorResult )
                    {
                        var bottomSignals = taManager.GetBottomingSignalsInfo( ref barData );

                        if ( bottomSignals != null )
                        {
                            foreach ( var signal in bottomSignals )
                            {
                                DrawBottomingSignal( signal, g, xCoor, highY, lowY, ref barData, _candleWidth, i + count );
                                count++;
                            }
                        }                        
                    }
                    else
                    {
                        if ( barData.Index == taManager.LatestBottomSignalIndex )
                        {
                            var bottomSignals = taManager.GetBottomingSignalsInfo( ref barData );

                            if ( bottomSignals != null )
                            {
                                foreach ( var signal in bottomSignals )
                                {
                                    DrawBottomingSignal( signal, g, xCoor, highY, lowY, ref barData, _candleWidth, i + count );
                                    count++;
                                }
                            }
                        }
                    }

                    return count;
                }                

                case TASignal.HAS_DIVERGENCE:
                {
                    if ( ShowDivergence )
                    {
                        var divergences = taManager.GetDivergenceInfoForDrawing( ref barData );

                        if ( divergences != null )
                        {
                            foreach ( var d in divergences )
                            {
                                DrawDivergence( d.Divergence, g, xCoor, highY, lowY, ref barData, i + count );
                                count++;
                            }
                        }                        
                    }
                    else
                    {
                        if ( barData.Index == taManager.LatestDivergenceIndex )
                        {
                            var divergences = taManager.GetDivergenceInfoForDrawing( ref barData );

                            if ( divergences != null )
                            {
                                foreach ( var d in divergences )
                                {
                                    DrawDivergence( d.Divergence, g, xCoor, highY, lowY, ref barData, i + count );
                                    count++;
                                }
                            }
                        }
                    }

                    return count;
                }                

                case TASignal.WAVE_PEAK:
                {
                    if ( ShowElliottWave )
                    {
                        var zigZag = taManager.ZigZagResult;

                        if ( zigZag.ContainsKey( barData.LinuxTime ) )
                        {
                            DrawWavePeak( TASignalSymbol.WaveImportance, g, xCoor, highY, lowY, ref barData );
                        }                        
                    }
                    else
                    {

                    }
                }                
                break;

                case TASignal.WAVE_TROUGH:
                {
                    if ( ShowElliottWave )
                    {
                        var zigZag = taManager.ZigZagResult;

                        if ( zigZag.ContainsKey( barData.LinuxTime ) )
                        {
                            DrawWaveTrough( TASignalSymbol.WaveImportance, g, xCoor, highY, lowY, ref barData );
                        }                        
                    }   
                    else
                    {

                    }
                }
                break;

                //case TASignal.HAS_STRUCT_LABEL:
                //{
                //    if ( ShowMonoWave )
                //    {
                //        var structLabel = taManager.GetStructureLabel( barData );

                //        if ( barData.IsPeak() )
                //        {
                //            DrawStructureLabelTop( structLabel, g, xCoor, highY, lowY, barData );
                //        }
                //        else if ( barData.IsTrough() )
                //        {
                //            DrawStructureLabelBottom( structLabel, g, xCoor, highY, lowY, barData );
                //        }
                //    }
                //    else
                //    {

                //    }
                //}
                //break;

                case TASignal.GANN_PEAK:
                {
                    if ( ShowElliottWave )
                    {
                        DrawGannPeak( TASignalSymbol.GannImportance, g, xCoor, highY, lowY, ref barData );
                    }
                }
                break;

                case TASignal.GANN_TROUGH:
                {
                    if ( ShowElliottWave )
                    {
                        DrawGannTrough( TASignalSymbol.GannImportance, g, xCoor, highY, lowY, ref barData );
                    }
                }
                break;

                case TASignal.HAS_TIME_ROTATION:
                {
                    if ( ShowPriceTimeSignal )
                    {
                        var waveImportantTimeInfo = taManager.GetWaveImportantTimeInfo( ref barData );

                        if ( waveImportantTimeInfo != null )
                        {
                            waveImportantTimeInfo.Sort( );

                            foreach ( var wave in waveImportantTimeInfo )
                            {
                                DrawWaveImportantTimeInfo( wave, g, xCoor, highY, lowY, ref barData, i + count );
                                count++;
                            }
                        }                        
                    }
                    else
                    {
                        if ( barData.Index == taManager.LatestWaveRotationIndex )
                        {
                            var waveImportantTimeInfo = taManager.GetWaveImportantTimeInfo( ref barData );

                            if ( waveImportantTimeInfo != null )
                            {
                                waveImportantTimeInfo.Sort( );

                                foreach ( var wave in waveImportantTimeInfo )
                                {
                                    DrawWaveImportantTimeInfo( wave, g, xCoor, highY, lowY, ref barData, i + count );
                                    count++;
                                }
                            }
                        }
                    }

                    return count;
                }                
                

                case TASignal.HAS_GANN_SQUARE:
                {
                    if ( ShowPriceTimeSignal )
                    {
                        var gannInfo = taManager.GetGannPriceTimeInfo( ref barData );

                        if ( gannInfo != null )
                        {
                            gannInfo.Sort( );

                            foreach ( var wave in gannInfo )
                            {
                                DrawGannPriceTimeInfo( wave, g, xCoor, highY, lowY, ref barData, i + count );
                                count++;
                            }
                        }                        
                    }
                    else
                    {
                        if ( barData.Index == taManager.LastestGannPriceTimeIndex )
                        {
                            var gannInfo = taManager.GetGannPriceTimeInfo( ref barData );

                            if ( gannInfo != null )
                            {
                                gannInfo.Sort( );

                                foreach ( var wave in gannInfo )
                                {
                                    DrawGannPriceTimeInfo( wave, g, xCoor, highY, lowY, ref barData, i + count );
                                    count++;
                                }
                            }
                        }
                    }

                    return count;
                }                
                

                case TASignal.HAS_PIVOT_RELATION:
                {
                    if ( ShowPriceTimeSignal )
                    {
                        DrawPivotRelationship( g, xCoor, highY, lowY, ref barData, i + count );
                        count++;
                        return count;
                    }  
                    else
                    {

                    }
                }
                break;
            }

            return count;
        }

        private void DrawBottomingSignal( TABottomingSignal bottomSignal, IRenderContext2D g, double xCoor, double highY, double lowY, ref SBar barData, int candleWidth, int v )
        {
            (TASignalSymbol, int) signalKey = default;

            var ptSize = GetPointSizeByScale( _candleWidth );

            if ( bottomSignal == TABottomingSignal.MACD_CROSS_UP )
            {                
                signalKey = ( TASignalSymbol.MACD_CROSS_UP, ptSize );                
            }            
            else if ( bottomSignal == TABottomingSignal.ExitOverSold )
            {
                signalKey = ( TASignalSymbol.ExitOverSold, ptSize );
            }            
            

            if ( signalKey == default )
                return;

            FxSpritePointMarker pointMarker = null;

            if ( !_pointMarkerCache.TryGetValue( signalKey, out pointMarker ) )
            {
                pointMarker = FreemindTaXaml.GetTABottomingSignalPointMarker( bottomSignal, ptSize );

                if ( pointMarker == null )
                    return;

                _pointMarkerCache.GetOrAddValueRef( signalKey ) = pointMarker;
                _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;
                

                pointMarker.BeginBatch( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
            }
            else
            {
                if ( !_drawPointMarkers.ContainsKey( signalKey ) )
                {
                    _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                    pointMarker.BeginBatchNoInit( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                }
            }

            if ( pointMarker == null )
                return;

            double drawX = xCoor;
            double drawY = _lastBottomPosition > 0 ? _lastBottomPosition + SignalMargin + ptSize : lowY + SignalMargin + ptSize;

            _lastBottomPosition = drawY;


            pointMarker.MoveTo( g, drawX, drawY, barData.Index );
        }

        private void DrawPivotRelationship( IRenderContext2D g, double xCoor, double highY, double lowY, ref SBar barData, int v )
        {
            var ptSize = GetPointSizeByScale( _candleWidth );

            var signalKey = ( TASignalSymbol.PivotPoint, ptSize );

            FxSpritePointMarker pointMarker = null;

            if ( !_pointMarkerCache.TryGetValue( signalKey, out pointMarker ) )
            {
                int colorInt = 0x4DAE89;
                pointMarker = FreemindTaXaml.GetPivotPointMarker( ptSize, colorInt );

                _pointMarkerCache.GetOrAddValueRef( signalKey ) = pointMarker;                

                _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                pointMarker.BeginBatch( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
            }
            else
            {
                if ( !_drawPointMarkers.ContainsKey( signalKey ) )
                {
                    _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                    pointMarker.BeginBatchNoInit( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                }
            }

            if ( pointMarker == null )
                return;

            double drawX = xCoor;
            double drawY = 0;

            if ( barData.IsWaveTrough( ) )
            {
                drawY = _lastBottomPosition > 0 ? _lastBottomPosition + SignalMargin : lowY + SignalMargin;

                _lastBottomPosition = drawY + ptSize;
            }
            else
            {
                drawY = _lastTopPosition > 0 ? _lastTopPosition - SignalMargin - ptSize : highY - SignalMargin - ptSize;

                _lastTopPosition = drawY;
            }


            pointMarker.MoveTo( g, drawX, drawY, barData.Index );
        }

        private void DrawGannPriceTimeInfo( GannPriceTimeInfo info, IRenderContext2D g, double xCoor, double highY, double lowY, ref SBar barData, int v )
        {
            int waveImportance = -1;

            if ( _hews != null )
            {
                waveImportance = _hews.GetWaveImportance( barData.BarPeriod, barData.LinuxTime );
            }

            var ptSize = GetPointSizeByScaleAndWaveImportance( _candleWidth, waveImportance );
            FxSpritePointMarker pointMarker = null;

            var rot = info.GannPriceTimeType;

            if ( rot == TaGannPriceTimeType.CurrentPrice_TimeElapsed_Major || rot == TaGannPriceTimeType.CurrentPrice_TimeElapsed_RedDot || rot == TaGannPriceTimeType.CurrentPrice_TimeElapsed_Local )
            {
                var signalKey = ( TASignalSymbol.CurrentPrice_TimeElapsed, (int) rot );

                if ( !_pointMarkerCache.TryGetValue( signalKey, out pointMarker ) )
                {
                    int colorInt = 0xFF0000;
                    pointMarker = FreemindTaXaml.GetPriceTimePointMarker( rot, info.GannPriceTimeDegrees, ptSize, colorInt );

                    _pointMarkerCache.GetOrAddValueRef( signalKey ) = pointMarker;

                    _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                    pointMarker.BeginBatch( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                }
                else
                {
                    if ( !_drawPointMarkers.ContainsKey( signalKey ) )
                    {
                        _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                        pointMarker.BeginBatchNoInit( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                    }
                }
            }
            else if ( rot == TaGannPriceTimeType.PriorTrendTime_CurrentTrendRange_Major || rot == TaGannPriceTimeType.PriorTrendTime_CurrentTrendRange_RedDot || rot == TaGannPriceTimeType.PriorTrendTime_CurrentTrendRange_Local )
            {
                var signalKey = ( TASignalSymbol.PriorTrendTime_CurrentTrendRange, ( int )rot );

                if ( !_pointMarkerCache.TryGetValue( signalKey, out pointMarker ) )
                {
                    int colorInt = 0xFF5B00;
                    pointMarker = FreemindTaXaml.GetPriceTimePointMarker( rot, info.GannPriceTimeDegrees, ptSize, colorInt );

                    _pointMarkerCache.GetOrAddValueRef( signalKey ) = pointMarker;

                    _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                    pointMarker.BeginBatch( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                }
                else
                {
                    if ( !_drawPointMarkers.ContainsKey( signalKey ) )
                    {
                        _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                        pointMarker.BeginBatchNoInit( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                    }
                }
            }
            else if ( rot == TaGannPriceTimeType.PriorPriceRange_CurrentTrendTime_Major || rot == TaGannPriceTimeType.PriorPriceRange_CurrentTrendTime_RedDot || rot == TaGannPriceTimeType.PriorPriceRange_CurrentTrendTime_Local )
            {
                var signalKey = ( TASignalSymbol.PriorPriceRange_CurrentTrendTime, ptSize );

                if ( !_pointMarkerCache.TryGetValue( signalKey, out pointMarker ) )
                {                    
                    int colorInt = 0x4DAE89;
                    pointMarker = FreemindTaXaml.GetPriceTimePointMarker( rot, info.GannPriceTimeDegrees, ptSize, colorInt );

                    _pointMarkerCache.GetOrAddValueRef( signalKey ) = pointMarker;

                    _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                    pointMarker.BeginBatch( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                }
                else
                {
                    if ( !_drawPointMarkers.ContainsKey( signalKey ) )
                    {
                        _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                        pointMarker.BeginBatchNoInit( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                    }
                }
            }
            else if ( rot == TaGannPriceTimeType.PriorEndingPrice_CurrentTrendTime_Major || rot == TaGannPriceTimeType.PriorEndingPrice_CurrentTrendTime_RedDot || rot == TaGannPriceTimeType.PriorTrendEndPrice_CurrentTrendTime_Local )
            {
                var signalKey = ( TASignalSymbol.PriorEndingPrice_CurrentTrendTime, ptSize );

                if ( !_pointMarkerCache.TryGetValue( signalKey, out pointMarker ) )
                {
                    int colorInt = 0x377AB5;
                    pointMarker = FreemindTaXaml.GetPriceTimePointMarker( rot, info.GannPriceTimeDegrees, ptSize, colorInt );

                    _pointMarkerCache.GetOrAddValueRef( signalKey ) = pointMarker;

                    _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                    pointMarker.BeginBatch( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                }
                else
                {
                    if ( !_drawPointMarkers.ContainsKey( signalKey ) )
                    {
                        _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                        pointMarker.BeginBatchNoInit( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                    }
                }
            }
            else if ( rot == TaGannPriceTimeType.CurrentTrendRange_CurrentTrendTime_Major || rot == TaGannPriceTimeType.CurrentTrendRange_CurrentTrendTime_RedDot || rot == TaGannPriceTimeType.CurrentTrendRange_CurrentTrendTime_Local )
            {
                var signalKey = ( TASignalSymbol.CurrentTrendRange_CurrentTrendTime, ptSize );

                if ( !_pointMarkerCache.TryGetValue( signalKey, out pointMarker ) )
                {
                    int colorInt = 0xBA00FF;
                    pointMarker = FreemindTaXaml.GetPriceTimePointMarker( rot, info.GannPriceTimeDegrees, ptSize, colorInt );

                    _pointMarkerCache.GetOrAddValueRef( signalKey ) = pointMarker;

                    _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                    pointMarker.BeginBatch( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                }
                else
                {
                    if ( !_drawPointMarkers.ContainsKey( signalKey ) )
                    {
                        _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                        pointMarker.BeginBatchNoInit( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                    }
                }
            }

            if ( pointMarker == null )
                return;

            double drawX = xCoor;
            double drawY = 0;

            if ( barData.IsWaveTrough( ) )
            {
                drawY = _lastBottomPosition > 0 ? _lastBottomPosition + SignalMargin : lowY + SignalMargin;

                _lastBottomPosition = drawY + ptSize;
            }
            else
            {
                drawY = _lastTopPosition > 0 ? _lastTopPosition - SignalMargin - ptSize : highY - SignalMargin - ptSize;

                _lastTopPosition = drawY;
            }


            pointMarker.MoveTo( g, drawX, drawY, barData.Index );
        }

        private void DrawWaveImportantTimeInfo( WaveRotationInfo info, IRenderContext2D g, double xCoor, double highY, double lowY, ref SBar barData, int v )
        {
            int waveImportance = -1;

            if ( _hews != null )
            {
                waveImportance = _hews.GetWaveImportance( barData.BarPeriod, barData.LinuxTime );
            }

            var ptSize = GetPointSizeByScaleAndWaveImportance( _candleWidth, waveImportance );
            FxSpritePointMarker pointMarker = null;
            
            var rot = info.GannPriceTimeType;

            if ( rot == TaWaveRotation.IMPORTANT_HIGH_HIGH || rot == TaWaveRotation.IMPORTANT_LOW_LOW )
            {
                var signalKey = ( TASignalSymbol.WaveRotation_HHLL, ptSize );

                if ( !_pointMarkerCache.TryGetValue( signalKey, out pointMarker ) )
                {
                    pointMarker = FreemindTaXaml.CreateSpritePointMarker( TASignalSymbol.WaveRotation_HHLL, ptSize );

                    _pointMarkerCache.GetOrAddValueRef( signalKey ) = pointMarker;

                    _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                    pointMarker.BeginBatch( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                }
                else
                {
                    if ( ! _drawPointMarkers.ContainsKey( signalKey ) )
                    {
                        _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                        pointMarker.BeginBatchNoInit( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                    }
                }
            }
            else if ( rot == TaWaveRotation.IMPORTANT_LOW_HIGH || rot == TaWaveRotation.IMPORTANT_HIGH_LOW )
            {
                var signalKey = ( TASignalSymbol.WaveRotation_HLLH, ptSize );

                if ( !_pointMarkerCache.TryGetValue( signalKey, out pointMarker ) )
                {
                    pointMarker = FreemindTaXaml.CreateSpritePointMarker( TASignalSymbol.WaveRotation_HLLH, ptSize );

                    _pointMarkerCache.GetOrAddValueRef( signalKey ) = pointMarker;

                    _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                    pointMarker.BeginBatch( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                }
                else
                {
                    if ( !_drawPointMarkers.ContainsKey( signalKey ) )
                    {
                        _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                        pointMarker.BeginBatchNoInit( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                    }
                }
            }
            else if ( rot == TaWaveRotation.LOCAL_BEAR_CORRECTION || rot == TaWaveRotation.LOCAL_BULL_CORRECTION )
            {
                var signalKey = ( TASignalSymbol.WaveRotation_Correction, ptSize );

                if ( !_pointMarkerCache.TryGetValue( signalKey, out pointMarker ) )
                {
                    pointMarker = FreemindTaXaml.CreateSpritePointMarker( TASignalSymbol.WaveRotation_Correction, ptSize );

                    _pointMarkerCache.GetOrAddValueRef( signalKey ) = pointMarker;
                    _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                    pointMarker.BeginBatch( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                }
                else
                {
                    if ( !_drawPointMarkers.ContainsKey( signalKey ) )
                    {
                        _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                        pointMarker.BeginBatchNoInit( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                    }
                }
            }
            else if ( rot == TaWaveRotation.LOCAL_BEAR_TOTALBARS || rot == TaWaveRotation.LOCAL_BULL_TOTALBARS )
            {                
                var signalKey = ( TASignalSymbol.WaveRotation_BarCount, ptSize );

                if ( !_pointMarkerCache.TryGetValue( signalKey, out pointMarker ) )
                {
                    pointMarker = FreemindTaXaml.CreateSpritePointMarker( TASignalSymbol.WaveRotation_BarCount, ptSize );

                    _pointMarkerCache.GetOrAddValueRef( signalKey ) = pointMarker;

                    _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                    pointMarker.BeginBatch( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                }
                else
                {
                    if ( !_drawPointMarkers.ContainsKey( signalKey ) )
                    {
                        _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                        pointMarker.BeginBatchNoInit( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                    }
                }
            }

            if( pointMarker == null )
                return;                        

            double drawX = xCoor;
            double drawY = 0;

            if ( barData.IsWaveTrough( ) )
            {
                drawY = _lastBottomPosition > 0 ? _lastBottomPosition + SignalMargin : lowY + SignalMargin;                

                _lastBottomPosition = drawY + ptSize;               
            }
            else
            {
                drawY = _lastTopPosition > 0 ? _lastTopPosition - SignalMargin - ptSize : highY - SignalMargin - ptSize;

                _lastTopPosition = drawY;
            }
            

            pointMarker.MoveTo( g, drawX, drawY, barData.Index );

            
        }

        private void DrawDivergence( TADivergence div, IRenderContext2D g, double xCoor, double highY, double lowY, ref SBar barData, int i )
        {
            int waveImportance = -1;

            if ( _hews != null )
            {
                waveImportance = _hews.GetWaveImportance( barData.BarPeriod, barData.LinuxTime );
            }

            //var ptSize = GetPointSizeByScaleAndWaveImportance( _candleWidth, waveImportance );
            var ptSize = GetPointSizeByScale( _candleWidth );

            if ( div == TADivergence.POSITIVE_DIVERGENCE_DOUBLE_BOTTOM || div == TADivergence.POSITIVE_DIVERGENCE_LOWER_LOW )
            {
                DrawDivergenceBelow( g, TASignalSymbol.PositiveDivergence, i, xCoor, lowY, ptSize );
            }
            else if ( div == TADivergence.HIDDEN_POSITIVE_DIVERGENCE_HIGHER_HIGH || div == TADivergence.HIDDEN_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM )
            {
                DrawDivergenceBelow( g, TASignalSymbol.HiddenPosDiv, i, xCoor, lowY, ptSize );
            }
            else if ( div == TADivergence.IMPORTANT_HIDDEN_POSITIVE_DIVERGENCE_HIGHER_HIGH || div == TADivergence.IMPORTANT_HIDDEN_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM )
            {
                DrawDivergenceBelow( g, TASignalSymbol.ImportantHiddenPosDiv, i, xCoor, lowY, ptSize );
            }
            else if ( div == TADivergence.IMPORTANT_POSITIVE_DIVERGENCE_DOUBLE_BOTTOM || div == TADivergence.IMPORTANT_POSITIVE_DIVERGENCE_LOWER_LOW )
            {
                DrawDivergenceBelow( g, TASignalSymbol.ImportantPositive, i, xCoor, lowY, ptSize );
            }
            else if ( div == TADivergence.IMPORTANT_NEGATIVE_DIVERGENCE_DOUBLE_TOP || div == TADivergence.IMPORTANT_NEGATIVE_DIVERGENCE_HIGHER_HIGH )
            {
                DrawDivergenceAbove( g, TASignalSymbol.ImportantNegative, i, xCoor, highY, ptSize );
            }
            else if ( div == TADivergence.NEGATIVE_DIVERGENCE_DOUBLE_TOP || div == TADivergence.NEGATIVE_DIVERGENCE_HIGHER_HIGH )
            {
                DrawDivergenceAbove( g, TASignalSymbol.NegativeDivergence, i, xCoor, highY, ptSize );
            }
            else if ( div == TADivergence.IMPORTANT_HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW || div == TADivergence.IMPORTANT_HIDDEN_NEGATIVE_DIVERGENCE_DOUBLE_TOP )
            {
                DrawDivergenceAbove( g, TASignalSymbol.ImportantHiddenNegDiv, i, xCoor, highY, ptSize );
            }
            else if ( div == TADivergence.HIDDEN_NEGATIVE_DIVERGENCE_LOWER_LOW || div == TADivergence.HIDDEN_NEGATIVE_DIVERGENCE_DOUBLE_TOP )
            {
                DrawDivergenceAbove( g, TASignalSymbol.HiddenNegDiv, i, xCoor, highY, ptSize );
            }
        }


        public int GetWaveImportanceIconColor( int waveImportance )
        {
            Color output = Colors.White;

            if ( waveImportance == GlobalConstants.WEEKLYIMPT )
            {
                output = Colors.Red;
            }
            else if ( waveImportance == GlobalConstants.DAILYIMPT )
            {
                output = Colors.Red;
            }
            else if ( waveImportance == GlobalConstants.HRS08IMPT )
            {
                output =  Colors.Blue;
            }
            else if ( waveImportance == GlobalConstants.HRS04IMPT )
            {
                output =  Colors.Green;
            }
            else if ( waveImportance == GlobalConstants.HRS02IMPT )
            {
                output =  Colors.BlueViolet;
            }
            else if ( waveImportance == GlobalConstants.HRS01IMPT )
            {
                output =  Colors.Magenta;
            }
            else if ( waveImportance == GlobalConstants.MINS30IMPT )
            {
                output =  Colors.LightCoral;
            }
            else if ( waveImportance == GlobalConstants.MINS15IMPT )
            {
                output =  Colors.Yellow;
            }
            else if ( waveImportance == GlobalConstants.MINS05IMPT )
            {
                output =  Colors.Gray;
            }

            int colorCode = BitConverter.ToInt32( new byte[ ] { output.B, output.G, output.R, 0x00 }, 0 );

            return colorCode;
        }

        public int GetGannImportanceIconColor( int waveImportance )
        {
            Color output = Colors.White;
            if ( waveImportance == 5 )
            {
                output = Colors.Red;
            }            
            else if ( waveImportance == 4 )
            {
                output = Colors.Blue;
            }
            else if ( waveImportance == 3 )
            {
                output = Colors.Green;
            }            
            else if ( waveImportance == 2 )
            {
                output = Colors.Brown;
            }
            else if ( waveImportance == 1)
            {
                output = Colors.Gray;
            }

            int colorCode = BitConverter.ToInt32( new byte[ ] { output.B, output.G, output.R, 0x00 }, 0 );

            return colorCode;
        }


        private int GetSmallWaveImportanceIconSize( int waveImpt )
        { 
            if ( waveImpt == GlobalConstants.WEEKLYIMPT )
            {
                return 10;
            }
            if ( waveImpt == GlobalConstants.DAILYIMPT )
            {
                return 6;
            }

            return 5;
        }

            private int GetWaveImportanceIconSize( WavePointImportance waveImpt, TimeSpan period )
        {
            if( waveImpt == null )
                return 10;

            var highest = waveImpt.HighestTimeframe;

            if ( highest > period )
            {
                if ( highest == TimeSpan.FromDays( 1 ) )
                {
                    return 24;
                }
                else if ( highest == TimeSpan.FromHours( 8 ) )
                {
                    return 22;
                }
                else if ( highest == TimeSpan.FromHours( 6 ) )
                {
                    return 20;
                }
                else if ( highest == TimeSpan.FromHours( 4 ) )
                {
                    return 18;
                }
                else if ( highest == TimeSpan.FromHours( 3 ) )
                {
                    return 16;
                }
                else if ( highest == TimeSpan.FromHours( 2 ) )
                {
                    return 14;
                }
                else if ( highest == TimeSpan.FromHours( 1 ) )
                {
                    return 12;
                }
                else if ( highest == TimeSpan.FromMinutes( 30 ) )
                {
                    return 10;
                }
                else if ( highest == TimeSpan.FromMinutes( 15 ) )
                {
                    return 9;
                }
                else if ( highest == TimeSpan.FromMinutes( 5 ) )
                {
                    return 8;
                }
            }
            else
            {
                if ( waveImpt.WaveImportance == GlobalConstants.DAILYIMPT )
                {
                    return 12;
                }
                else if ( waveImpt.WaveImportance >= 21 )
                {
                    return 9;
                }
            }
           
            return 6;                       
        }

        private int GetGannImportanceIconSize( int gannImpt, TimeSpan period )
        {            
            return 4;
        }

        private void DrawWaveTrough( TASignalSymbol signal, IRenderContext2D g, double xCoor, double highY, double lowY, ref SBar barData )
        {
            WavePointImportance waveImportance = null;

            var period = barData.BarPeriod;

            if ( _hews != null )
            {
                waveImportance = _hews.GetWaveImportanceExt( period, barData.LinuxTime );
            }

            if ( waveImportance.WaveImportance < WaveImportance )
                return;


            int ptSize = 12;
            int ptColor = GetWaveImportanceIconColor( -1 );

            if ( waveImportance != null )
            {
                ptSize = GetWaveImportanceIconSize( waveImportance, period );
                ptColor = GetWaveImportanceIconColor( waveImportance.WaveImportance );
            }
            
            FxSpritePointMarker pointMarker = null;
            var signalKey = ( signal, ptColor + ptSize );

            if ( !_pointMarkerCache.TryGetValue( signalKey, out pointMarker ) )
            {
                pointMarker = FreemindTaXaml.CreateSpritePointMarker( signal, ptSize, ptColor );

                _pointMarkerCache.GetOrAddValueRef( signalKey ) = pointMarker;

                _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                pointMarker.BeginBatch( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
            }
            else
            {
                if ( !_drawPointMarkers.ContainsKey( signalKey ) )
                {
                    _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                    pointMarker.BeginBatchNoInit( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                }
            }

            double drawX = xCoor;
            var drawY = _lastBottomPosition > 0 ? _lastBottomPosition + SignalMargin : lowY + SignalMargin;

            pointMarker.MoveTo( g, drawX, drawY, barData.Index );

            _lastBottomPosition = drawY + ptSize;                        
        }

        private void DrawSmallWaveTrough( TASignalSymbol signal, IRenderContext2D g, double xCoor, double highY, double lowY, ref SBar barData )
        {
            WavePointImportance waveImportance = null;

            var period = barData.BarPeriod;

            if ( _hews != null )
            {
                waveImportance = _hews.GetWaveImportanceExt( period, barData.LinuxTime );
            }

            int ptSize = 5;
            int ptColor = GetWaveImportanceIconColor( -1 );

            if ( waveImportance != null )
            {
                ptSize = GetSmallWaveImportanceIconSize( waveImportance.WaveImportance );
                ptColor = GetWaveImportanceIconColor( waveImportance.WaveImportance );
            }

            FxSpritePointMarker pointMarker = null;
            var signalKey = ( signal, ptColor + ptSize );

            if ( !_pointMarkerCache.TryGetValue( signalKey, out pointMarker ) )
            {
                pointMarker = FreemindTaXaml.CreateSpritePointMarker( signal, ptSize, ptColor );

                _pointMarkerCache.GetOrAddValueRef( signalKey ) = pointMarker;

                _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                pointMarker.BeginBatch( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
            }
            else
            {
                if ( !_drawPointMarkers.ContainsKey( signalKey ) )
                {
                    _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                    pointMarker.BeginBatchNoInit( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                }
            }

            double drawX = xCoor;
            var drawY = _lastBottomPosition > 0 ? _lastBottomPosition + SignalMargin : lowY + SignalMargin;

            pointMarker.MoveTo( g, drawX, drawY, barData.Index );

            _lastBottomPosition = drawY + ptSize;
        }

        private void DrawStructureLabelTop( StructureLabelEnum structLabel, IRenderContext2D g, double xCoor, double highY, double lowY, ref SBar barData )
        {
            var waveKey      = new WaveAnnotationKey( barData.LinuxTime, WaveLabelPosition.TOP );
            var drawString   = MonoWaveGroup.GetStructureLabelString( structLabel );

            var toBeAddedTop = new PooledList<TextAnnotation>( );

            var textBlock    = new TextBlock( ) { FontFamily = _fontFamily, FontSize = 12, FontStyle = _fontStyle, FontWeight = _fontWeight };
            var size         = drawString.Measure( textBlock );

            var drawY        = _lastTopPosition > 0 ? _lastTopPosition : highY;

            _lastTopPosition = drawY - SignalMargin - size.Height;

            if ( !_monoWaveAnnotationAdded.ContainsKey( waveKey ) )
            {
                AddStructureLabelToTop( ref barData, toBeAddedTop, drawString, 12, Colors.Black );
            }
            else
            {
                bool found = false;
                var waveList = _monoWaveAnnotationAdded.GetOrAddValueRef( waveKey );

                foreach ( var waveText in waveList )
                {
                    if ( waveText.Text == drawString )
                    {
                        waveText.FontFamily = _fontFamily;
                        waveText.FontSize   = 12;
                        waveText.FontWeight = _fontWeight;
                        waveText.FontStyle  = _fontStyle;
                        waveText.Text       = drawString;
                        waveText.Foreground = new SolidColorBrush( Colors.Black );
                        waveText.Y1         = YAxis.GetDataValue( _lastTopPosition );
                        waveText.IsHidden   = false;
                        found               = true;
                    }
                }

                if ( !found )
                {
                    AddNewWavesToTop( ref barData, toBeAddedTop, drawString, 12, Colors.Black );

                    _monoWaveAnnotationAdded.GetOrAddValueRef( waveKey ) = waveList;
                }
            }

            if ( toBeAddedTop.Count > 0 )
            {
                _monoWaveAnnotationAdded.GetOrAddValueRef( waveKey ) = toBeAddedTop;                
            }

            //var drawPoint = new Point(xCoor - size.Width / 2, drawY - 10 - size.Height);
            //var drawRect = new Rect(drawPoint, size);

            //g.DrawText( drawRect, Colors.Black, 10, drawString, _fontFamily, _fontWeight, _fontStyle );
        }

        private void DrawStructureLabelBottom( StructureLabelEnum structLabel, IRenderContext2D g, double xCoor, double highY, double lowY, ref SBar barData )
        {
            var toBeAddedBot = new PooledList<TextAnnotation>( );
            var waveKey      = new WaveAnnotationKey( barData.LinuxTime, WaveLabelPosition.BOTTOM );

            var drawString   = MonoWaveGroup.GetStructureLabelString( structLabel );


            var textBlock    = new TextBlock( ) { FontFamily = _fontFamily, FontSize = 10, FontStyle = _fontStyle, FontWeight = _fontWeight };
            var size         = drawString.Measure( textBlock );

            var drawY        = _lastBottomPosition > 0 ? _lastBottomPosition + SignalMargin  : lowY;
            //var drawPoint    = new Point( xCoor - size.Width / 2, drawY  );

            //var drawRect     = new Rect( drawPoint, size );

            //g.DrawText( drawRect, Colors.Black, 10, drawString, _fontFamily, _fontWeight, _fontStyle );

            if ( !_monoWaveAnnotationAdded.ContainsKey( waveKey ) )
            {
                AddStructureLabelToBottom( ref barData, toBeAddedBot, drawString, 12, Colors.Black, drawY );
            }
            else
            {
                bool found = false;
                var waveList = _monoWaveAnnotationAdded.GetOrAddValueRef( waveKey );

                foreach ( var waveText in waveList )
                {
                    if ( waveText.Text == drawString )
                    {
                        waveText.FontFamily = _fontFamily;
                        waveText.FontSize   = 12;
                        waveText.FontWeight = _fontWeight;
                        waveText.FontStyle  = _fontStyle;
                        waveText.Text       = drawString;
                        waveText.Foreground = new SolidColorBrush( Colors.Black );
                        waveText.Y1         = YAxis.GetDataValue( drawY );
                        waveText.IsHidden   = false;
                        found               = true;
                    }
                }

                if ( !found )
                {
                    AddNewWavesToBottom( ref barData, toBeAddedBot, drawString, 12, Colors.Black, drawY );

                    _monoWaveAnnotationAdded.GetOrAddValueRef( waveKey ) = waveList;                    
                }
            }

            _lastBottomPosition = drawY + size.Height;

            if ( toBeAddedBot.Count > 0 )
            {
                _monoWaveAnnotationAdded.GetOrAddValueRef( waveKey ) = toBeAddedBot;                
            }
        }

        private void DrawGannTrough( TASignalSymbol signal, IRenderContext2D g, double xCoor, double highY, double lowY, ref SBar barData )
        {
            int gannImportance = -1;

            var period = barData.BarPeriod;

            if ( _hews != null )
            {
                gannImportance = _hews.GetGannImportance( period, barData.LinuxTime );
            }

            int ptSize = 12;
            int ptColor = GetGannImportanceIconColor( -1 );

            if ( gannImportance > 0  )
            {
                ptSize = GetGannImportanceIconSize( gannImportance, period );
                ptColor = GetGannImportanceIconColor( gannImportance );
            }

            FxSpritePointMarker pointMarker = null;
            var signalKey = ( signal, ptColor + ptSize );

            if ( !_pointMarkerCache.TryGetValue( signalKey, out pointMarker ) )
            {
                pointMarker = FreemindTaXaml.CreateSpritePointMarker( signal, ptSize, ptColor );

                _pointMarkerCache.GetOrAddValueRef( signalKey ) = pointMarker;

                _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                pointMarker.BeginBatch( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
            }
            else
            {
                if ( !_drawPointMarkers.ContainsKey( signalKey ) )
                {
                    _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                    pointMarker.BeginBatchNoInit( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                }
            }

            double drawX = xCoor;
            var drawY = _lastBottomPosition > 0 ? _lastBottomPosition + SignalMargin : lowY + SignalMargin;

            pointMarker.MoveTo( g, drawX, drawY, barData.Index );

            _lastBottomPosition = drawY + ptSize;
        }


        private void DrawWavePeak( TASignalSymbol signal, IRenderContext2D g, double xCoor, double highY, double lowY, ref SBar barData )
        {
            WavePointImportance waveImportance = null;

            var period = barData.BarPeriod;

            if ( _hews != null )
            {
                waveImportance = _hews.GetWaveImportanceExt( period, barData.LinuxTime );
            }

            if( waveImportance.WaveImportance < WaveImportance )
                return;

            int ptSize  = 6;
            int ptColor = GetWaveImportanceIconColor( -1 );

            if ( waveImportance != null )
            {
                ptSize  = GetWaveImportanceIconSize( waveImportance, period );
                ptColor = GetWaveImportanceIconColor( waveImportance.WaveImportance );
            }
            

            FxSpritePointMarker pointMarker = null;
            var signalKey = ( signal, ptColor + ptSize );

            if ( !_pointMarkerCache.TryGetValue( signalKey, out pointMarker ) )
            {                
                pointMarker = FreemindTaXaml.CreateSpritePointMarker( signal, ptSize, ptColor );

                _pointMarkerCache.GetOrAddValueRef( signalKey ) = pointMarker;

                _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                pointMarker.BeginBatch( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
            }
            else
            {
                if ( !_drawPointMarkers.ContainsKey( signalKey ) )
                {
                    _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                    pointMarker.BeginBatchNoInit( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                }
            }

            double drawX = xCoor;
            var drawY = _lastTopPosition > 0 ? _lastTopPosition - SignalMargin - ptSize : highY - SignalMargin - ptSize;
            _lastTopPosition = drawY;

            pointMarker.MoveTo( g, drawX, drawY, barData.Index );
        }


        private void DrawSmallWavePeak( TASignalSymbol signal, IRenderContext2D g, double xCoor, double highY, double lowY, ref SBar barData )
        {
            WavePointImportance waveImportance = null;

            var period = barData.BarPeriod;

            if ( _hews != null )
            {
                waveImportance = _hews.GetWaveImportanceExt( period, barData.LinuxTime );
            }

            int ptSize  = 5;
            int ptColor = GetWaveImportanceIconColor( -1 );

            if ( waveImportance != null )
            {
                ptSize = GetSmallWaveImportanceIconSize( waveImportance.WaveImportance );
                ptColor = GetWaveImportanceIconColor( waveImportance.WaveImportance );
            }


            FxSpritePointMarker pointMarker = null;
            var signalKey = ( signal, ptColor + ptSize );

            if ( !_pointMarkerCache.TryGetValue( signalKey, out pointMarker ) )
            {
                pointMarker = FreemindTaXaml.CreateSpritePointMarker( signal, ptSize, ptColor );

                _pointMarkerCache.GetOrAddValueRef( signalKey ) = pointMarker;

                _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                pointMarker.BeginBatch( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
            }
            else
            {
                if ( !_drawPointMarkers.ContainsKey( signalKey ) )
                {
                    _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                    pointMarker.BeginBatchNoInit( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                }
            }

            double drawX = xCoor;
            var drawY = _lastTopPosition > 0 ? _lastTopPosition - SignalMargin - ptSize : highY - SignalMargin - ptSize;
            _lastTopPosition = drawY;

            pointMarker.MoveTo( g, drawX, drawY, barData.Index );
        }

        private void DrawGannPeak( TASignalSymbol signal, IRenderContext2D g, double xCoor, double highY, double lowY, ref SBar barData )
        {
            int gannImportance = -1;

            var period = barData.BarPeriod;

            if ( _hews != null )
            {
                gannImportance = _hews.GetGannImportance( period, barData.LinuxTime );
            }

            int ptSize = 12;
            int ptColor = GetGannImportanceIconColor( -1 );

            if ( gannImportance > 0 )
            {
                ptSize = GetGannImportanceIconSize( gannImportance, period );
                ptColor = GetGannImportanceIconColor( gannImportance );
            }


            FxSpritePointMarker pointMarker = null;
            var signalKey = ( signal, ptColor + ptSize );

            if ( !_pointMarkerCache.TryGetValue( signalKey, out pointMarker ) )
            {
                pointMarker = FreemindTaXaml.CreateSpritePointMarker( signal, ptSize, ptColor );

                _pointMarkerCache.GetOrAddValueRef( signalKey ) = pointMarker;

                _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                pointMarker.BeginBatch( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
            }
            else
            {
                if ( !_drawPointMarkers.ContainsKey( signalKey ) )
                {
                    _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                    pointMarker.BeginBatchNoInit( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                }
            }

            double drawX = xCoor;
            var drawY = _lastTopPosition > 0 ? _lastTopPosition - SignalMargin - ptSize : highY - SignalMargin - ptSize;
            _lastTopPosition = drawY;

            pointMarker.MoveTo( g, drawX, drawY, barData.Index );
        }


        private void DrawDivergenceBelow( IRenderContext2D g, TASignalSymbol signal, int i, double xCoor, double lowY, int ptSize )
        {
            FxSpritePointMarker pointMarker = null;
            var signalKey = ( signal, ptSize );

            if ( !_pointMarkerCache.TryGetValue( signalKey, out pointMarker ) )
            {
                pointMarker = FreemindTaXaml.CreateSpritePointMarker( signal, ptSize );

                _pointMarkerCache.GetOrAddValueRef( signalKey ) = pointMarker;

                _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                pointMarker.BeginBatch( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
            }
            else
            {
                if ( !_drawPointMarkers.ContainsKey( signalKey ) )
                {
                    _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                    pointMarker.BeginBatchNoInit( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                }
            }

            double drawX = xCoor;
            var drawY = _lastBottomPosition > 0 ? _lastBottomPosition + SignalMargin : lowY + SignalMargin;

            pointMarker.MoveTo( g, drawX, drawY, 0 );            
            
            _lastBottomPosition = drawY + ptSize;
        }

        private void DrawDivergenceAbove( IRenderContext2D g, TASignalSymbol signal, int i, double xCoor, double highY, int ptSize )
        {
            FxSpritePointMarker pointMarker = null;
            var signalKey = ( signal, ptSize );

            if ( !_pointMarkerCache.TryGetValue( signalKey, out pointMarker ) )
            {
                pointMarker = FreemindTaXaml.CreateSpritePointMarker( signal, ptSize );

                _pointMarkerCache.GetOrAddValueRef( signalKey ) = pointMarker;

                _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                pointMarker.BeginBatch( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
            }
            else
            {
                if ( !_drawPointMarkers.ContainsKey( signalKey ) )
                {
                    _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                    pointMarker.BeginBatchNoInit( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                }
            }

            double drawX = xCoor;

            var drawY = _lastTopPosition > 0 ? _lastTopPosition - SignalMargin - ptSize : highY - SignalMargin - ptSize;

            pointMarker.MoveTo( g, drawX, drawY, 0 );

            _lastTopPosition = drawY;
        }

        private int GetPointSizeByScale( double candleWith )
        {
            int pointSize = 0;

            if ( candleWith >= 10.0 )
            {
                pointSize = 24;
            }
            else if ( candleWith >= 8 )
            {
                pointSize = 21;
            }
            else if ( candleWith >= 6 )
            {
                pointSize = 18;
            }
            else if ( candleWith >= 4 )
            {
                pointSize = 15;
            }
            else
            {
                pointSize = 12;
            }

            return pointSize;
        }

        private int GetPointSizeByScaleAndWaveImportance( double candleWith, int waveImportance )
        {
            int pointSize = 0;
            if ( candleWith >= 10.0 )
            {
                if ( waveImportance == GlobalConstants.MONTHLYIMPT )
                {
                    pointSize = 26;
                }
                else if ( waveImportance == GlobalConstants.WEEKLYIMPT )
                {
                    pointSize = 20;
                }
                else if ( waveImportance == GlobalConstants.DAILYIMPT )
                {
                    pointSize = 16;
                }
                else if ( waveImportance == GlobalConstants.HRS08IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance == GlobalConstants.HRS04IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance == GlobalConstants.HRS02IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance == GlobalConstants.HRS01IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance == GlobalConstants.MINS30IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance <= GlobalConstants.MINS15IMPT )
                {
                    pointSize = 10;
                }
            }
            else if ( candleWith >= 8 )
            {
                if ( waveImportance == GlobalConstants.MONTHLYIMPT )
                {
                    pointSize = 24;
                }
                else if ( waveImportance == GlobalConstants.WEEKLYIMPT )
                {
                    pointSize = 18;
                }
                else if ( waveImportance == GlobalConstants.DAILYIMPT )
                {
                    pointSize = 14;
                }
                else if ( waveImportance == GlobalConstants.HRS08IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance == GlobalConstants.HRS04IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance == GlobalConstants.HRS02IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance == GlobalConstants.HRS01IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance == GlobalConstants.MINS30IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance <= GlobalConstants.MINS15IMPT )
                {
                    pointSize = 10;
                }
            }
            else if ( candleWith >= 6 )
            {
                if ( waveImportance == GlobalConstants.MONTHLYIMPT )
                {
                    pointSize = 22;
                }
                else if ( waveImportance == GlobalConstants.WEEKLYIMPT )
                {
                    pointSize = 16;
                }
                else if ( waveImportance == GlobalConstants.DAILYIMPT )
                {
                    pointSize = 14;
                }
                else if ( waveImportance == GlobalConstants.HRS08IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance == GlobalConstants.HRS04IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance == GlobalConstants.HRS02IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance == GlobalConstants.HRS01IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance == GlobalConstants.MINS30IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance <= GlobalConstants.MINS15IMPT )
                {
                    pointSize = 10;
                }
            }
            else if ( candleWith >= 4 )
            {
                if ( waveImportance == GlobalConstants.MONTHLYIMPT )
                {
                    pointSize = 20;
                }
                else if ( waveImportance == GlobalConstants.WEEKLYIMPT )
                {
                    pointSize = 16;
                }
                else if ( waveImportance == GlobalConstants.DAILYIMPT )
                {
                    pointSize = 14;
                }
                else if ( waveImportance == GlobalConstants.HRS08IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance == GlobalConstants.HRS04IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance == GlobalConstants.HRS02IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance == GlobalConstants.HRS01IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance == GlobalConstants.MINS30IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance <= GlobalConstants.MINS15IMPT )
                {
                    pointSize = 10;
                }
            }
            else if ( candleWith >= 2 )
            {
                if ( waveImportance == GlobalConstants.MONTHLYIMPT )
                {
                    pointSize = 28;
                }
                else if ( waveImportance == GlobalConstants.WEEKLYIMPT )
                {
                    pointSize = 14;
                }
                else if ( waveImportance == GlobalConstants.DAILYIMPT )
                {
                    pointSize = 12;
                }
                else if ( waveImportance == GlobalConstants.HRS08IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance == GlobalConstants.HRS08IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance == GlobalConstants.HRS04IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance == GlobalConstants.HRS02IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance == GlobalConstants.HRS01IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance <= GlobalConstants.HRS02IMPT )
                {
                    pointSize = 10;
                }
            }
            else
            {
                if ( waveImportance == GlobalConstants.MONTHLYIMPT )
                {
                    pointSize = 20;
                }
                else if ( waveImportance == GlobalConstants.WEEKLYIMPT )
                {
                    pointSize = 14;
                }
                else if ( waveImportance == GlobalConstants.DAILYIMPT )
                {
                    pointSize = 12;
                }
                else if ( waveImportance == GlobalConstants.HRS08IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance == GlobalConstants.HRS04IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance == GlobalConstants.HRS02IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance == GlobalConstants.HRS01IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance == GlobalConstants.MINS30IMPT )
                {
                    pointSize = 10;
                }
                else if ( waveImportance <= GlobalConstants.MINS15IMPT )
                {
                    pointSize = 10;
                }
            }

            return pointSize;
        }



        private void DrawToppingSignal( TAToppingSignal topSignal, IRenderContext2D g, double xCoor, double highY, double lowY, ref SBar barData, int candleWidth, int v )
        {
            ( TASignalSymbol, int ) signalKey = default;
            var ptSize = GetPointSizeByScale( _candleWidth );

            if ( topSignal == TAToppingSignal.MACD_CROSS_DOWN )
            {                
                signalKey = ( TASignalSymbol.MACD_CROSS_DOWN, ptSize );                
            }
            else if ( topSignal == TAToppingSignal.ExitOverBought )
            {
                signalKey = ( TASignalSymbol.ExitOverBrought, ptSize );
            }
            else if ( topSignal == TAToppingSignal.OscillatorCrossDown )
            {

            }
            else if ( topSignal == TAToppingSignal.OscNegativeDivergence )
            {

            }            
            else if ( topSignal == TAToppingSignal.ComasTurnDown )
            {

            }
            else if ( topSignal == TAToppingSignal.ComasCrossDown )
            {

            }
            else if ( topSignal == TAToppingSignal.OscillatorSmoothDown )
            {

            }

            if( signalKey == default )
                return;

            FxSpritePointMarker pointMarker = null;

            if ( !_pointMarkerCache.TryGetValue( signalKey, out pointMarker ) )
            {                
                pointMarker = FreemindTaXaml.GetTAToppingSignalPointMarker( topSignal, ptSize );

                if( pointMarker == null )
                    return;

                _pointMarkerCache.GetOrAddValueRef( signalKey ) = pointMarker;

                _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                pointMarker.BeginBatch( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
            }
            else
            {
                if ( !_drawPointMarkers.ContainsKey( signalKey ) )
                {
                    _drawPointMarkers.GetOrAddValueRef( signalKey ) = pointMarker;

                    pointMarker.BeginBatchNoInit( g, new Color?( pointMarker.Stroke ), new Color?( pointMarker.Fill ) );
                }
            }
            
            double drawX = xCoor;
            double drawY = _lastTopPosition > 0 ? _lastTopPosition - SignalMargin - ptSize : highY - SignalMargin - ptSize;
            _lastTopPosition = drawY;

            pointMarker.MoveTo( g, drawX, drawY, barData.Index );
        }

        private float GetSignalSpacingForTimeFrame( TimeSpan timePeriod )
        {
            if ( timePeriod == TimeSpan.FromDays( 30 ) )
            {
                return 0.0180f;
            }
            else if ( timePeriod == TimeSpan.FromDays( 7 ) )
            {
                return 0.0080f;
            }
            else if ( timePeriod == TimeSpan.FromDays( 1 ) )
            {
                return 0.0020f;
            }
            else if ( ( timePeriod == TimeSpan.FromHours( 1 ) ) || ( timePeriod == TimeSpan.FromHours( 2 ) ) || ( timePeriod == TimeSpan.FromHours( 4 ) ) )
            {
                return 0.0010f;
            }
            else if ( ( timePeriod == TimeSpan.FromMinutes( 15 ) ) || ( timePeriod == TimeSpan.FromMinutes( 30 ) ) )
            {
                return 0.00030f;
            }
            else if ( timePeriod == TimeSpan.FromMinutes( 5 ) )
            {
                return 0.00015f;
            }
            else if ( timePeriod == TimeSpan.FromMinutes( 1 ) )
            {
                return 0.00008f;
            }

            return 0f;
        }

        private bool DrawDatabarSignals( IRenderContext2D g, ref SBar barData,  IPenManager penManager, double xCoor, double highY, double lowY )
        {
            if ( !barData.HasSignal )
                return false;

            var tmpList = new PooledList<TASignal>( );

            int totalSignal = barData.TechnicalAnalysisSignalCount( ref tmpList );

            //g.ResetImageLocationVariables( );

            int iconIdx = 0;

            for ( int i = 0; i < totalSignal; i++ )
            {
                iconIdx += DrawTASignal( g, ref barData, penManager, xCoor, highY, lowY, iconIdx + 1, tmpList[ i ] );
            }

            return true;
        }

        private bool DrawEWaveDatabarSignals( IRenderContext2D g, ref SBar barData, IPenManager penManager, double xCoor, double highY, double lowY )
        {
            if ( !barData.HasSignal )
                return false;

            var tmpList = new PooledList<TASignal>( );

            int totalSignal = barData.TechnicalAnalysisSignalCount( ref tmpList );

            int iconIdx = 0;

            for ( int i = 0; i < totalSignal; i++ )
            {
                iconIdx += DrawEWaveAndLatestTASignal( g, ref barData, penManager, xCoor, highY, lowY, iconIdx + 1, tmpList[ i ] );
            }

            return true;
        }

        private bool DrawReducedEWaveDatabarSignals( IRenderContext2D g, ref SBar barData, IPenManager penManager, double xCoor, double highY, double lowY )
        {
            if ( !barData.HasSignal )
                return false;

            var tmpList = new PooledList<TASignal>( );

            int totalSignal = barData.TechnicalAnalysisSignalCount( ref tmpList );

            int iconIdx = 0;

            for ( int i = 0; i < totalSignal; i++ )
            {
                iconIdx += DrawReducedEWave( g, ref barData, penManager, xCoor, highY, lowY, iconIdx + 1, tmpList[ i ] );
            }

            return true;
        }

        private (IPen2D, IBrush2D) GetPen( ref SBar realBar, int currIdx, IRenderContext2D renderContext, PeriodXTaManager taManager, IStrokePaletteProvider strokePaletteProvider, IFillPaletteProvider fillPaletteProvider, IPenManager penManager )
        {
            (IPen2D Pen, IBrush2D Brush ) output = default;
            if ( ShowHewDetection )
            {
                RangeEx< DateTime > timeBlock = null;

                if ( timeBlock == null )
                {
                    if ( taManager.IsImpulsiveWave( realBar.BarTime, out timeBlock ) )
                    {
                        output.Pen = penManager.GetPen( Color.FromRgb( 0, 255, 0 ) );
                    }
                }
                else
                {
                    if ( !timeBlock.Contains( realBar.BarTime ) )
                    {
                        timeBlock = null;
                    }
                    else
                    {
                        output.Pen = penManager.GetPen( Color.FromRgb( 0, 255, 0 ) );
                    }
                }
            }
            else
            {
                if ( strokePaletteProvider != null )
                {
                    Color? stroke = null;

                    if ( realBar != SBar.EmptySBar && realBar.HasCandleStickPattern && ShowCandlePattern )
                    {
                        stroke = realBar.IsSelected ? strokePaletteProvider.OverrideStrokeColor( this, currIdx, realBar ) : realBar.GetCandleStickPatternColor( true );
                    }
                    else
                    {
                        stroke = strokePaletteProvider.OverrideStrokeColor( this, currIdx, realBar );
                    }

                    if ( stroke.HasValue )
                    {
                        output.Pen = penManager.GetPen( new Color?( stroke.Value ) );
                    }
                }

                if ( fillPaletteProvider != null )
                {
                    Brush brush = null;

                    if ( realBar != SBar.EmptySBar && realBar.HasCandleStickPattern && ShowCandlePattern )
                    {
                        brush = realBar.IsSelected ? fillPaletteProvider.OverrideFillBrush( this, currIdx, realBar ) : new SolidColorBrush( realBar.GetCandleStickPatternColor( false ).Value );
                    }
                    else
                    {
                        brush = fillPaletteProvider.OverrideFillBrush( this, currIdx, realBar );
                    }

                    if ( brush != null )
                    {
                        output.Brush = renderContext.CreateBrush( brush, Opacity, TextureMappingMode.PerPrimitive );
                    }
                }
            }

            return output;
        }

    }
}
