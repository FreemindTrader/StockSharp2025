using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals.PaletteProviders;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Data.Model;
using SciChart.Drawing.Common;
using fx.Definitions;
using fx.Charting.ATony;
using fx.Charting.Definitions;
using System;
using System.Collections.Generic;
using fx.Collections;
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
using fx.Charting.HewFibonacci;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Charting.Numerics.CoordinateCalculators;
using fx.Indicators;
using fx.Common;
using fx.Charting.CustomAnnotations;
using fx.Bars;

namespace fx.Charting
{
    public partial class FreemindCandlestickRenderableSeries : FastCandlestickRenderableSeries
    {
        private void DrawReduced( IRenderContext2D renderContext, IRenderPassData renderPassData, IStrokePaletteProvider strokePaletteProvider, IPenManager penManager )
        {
            bool isVerticalChart        = renderPassData.IsVerticalChart;
            var pointSeries             = CurrentRenderPassData.PointSeries as OhlcPointSeries;
            var indexes                 = pointSeries.Indexes;
            var xvalues                 = pointSeries.XValues;
            var highValues              = pointSeries.HighValues;
            var lowValues               = pointSeries.LowValues;
            var openValues              = pointSeries.OpenValues;
            var closeValues             = pointSeries.CloseValues;
            int count                   = pointSeries.Count;

            var seriesDrawingHelper     = SeriesDrawingHelpersFactory.GetSeriesDrawingHelper( renderContext, CurrentRenderPassData );

            if ( ShowTradingTime )
            {
                DrawTradingTime( renderContext, renderPassData, indexes, xvalues, count );
            }

            bool selectedBarChangedFib  = false;
            bool selectedBarChangedMono = false;
            var highestCycle            = ElliottWaveCycle.UNKNOWN;
            int selectedBarCount        = 0;
            int realBarIndex            = -1;


            for ( int i = 0; i < count; ++i )
            {
                var xValue        = xvalues[ i ];
                var open          = openValues[ i ];
                var high          = highValues[ i ];
                var low           = lowValues[ i ];
                var close         = closeValues[ i ];
                var rising        = close >= open;

                var xCoor         = renderPassData.XCoordinateCalculator.GetCoordinate( xValue ).ClipToIntValue( );
                var highY         = renderPassData.YCoordinateCalculator.GetCoordinate( high ).ClipToIntValue( );
                var lowY          = renderPassData.YCoordinateCalculator.GetCoordinate( low ).ClipToIntValue( );

                var currIdx       = indexes[ i ];

                var pointPen      = rising ? _upWickPen : _downWickPen;

                var metadata      = DataSeries.HasMetadata ? DataSeries.Metadata[ currIdx ] : null;

                var tmpBar        = ( SBar ) metadata;
                CheckBarsRepo( ref tmpBar );
                ref SBar realBar  = ref _barList[ tmpBar.Index ];
                realBarIndex      = realBar.Index;

                if ( realBar.IsSelected )
                {
                    pointPen = penManager.GetPen( Color.FromRgb( 255, 0, 0 ) );
                }

                seriesDrawingHelper.DrawLine( new Point( xCoor, highY ), new Point( xCoor, lowY ), pointPen );

                if ( realBar != SBar.EmptySBar )
                {
                    if ( realBar.IsSelected )
                    {
                        selectedBarCount++;

                        if ( realBar.Index != _lastReducedSelectedBarIndex )
                        {
                            _lastReducedSelectedBarIndex = realBar.Index;
                            _lastSelectedBarTime = realBar.LinuxTime;

                            if ( selectedBarCount == 1 )
                            {
                                highestCycle = ElliottWaveCycle.UNKNOWN;
                            }
                        }

                        if ( realBar.SelectionChanged )
                        {
                            selectedBarChangedFib = true;
                            selectedBarChangedMono = true;
                            realBar.SelectionChanged = false;
                        }
                    }

                    if ( _canUpdate )
                    {
                        if ( ShowMonoWave )
                        {
                            if ( selectedBarChangedMono )
                            {
                                _drawMonoWave = true;
                                selectedBarChangedMono = false;
                            }
                        }
                        else
                        {
                            if ( _hasMonolines ) RemoveAllMonowavesLines();
                            if ( _monoWaveAnnotationAdded.Count > 0 ) RemoveAllStructuralLabels();
                        }

                        _lastBottomPosition = 0;
                        _lastTopPosition = 0;

                        var hew = realBar.GetWaveFromScenario( WaveScenarioNo );

                        if ( hew.HasElliottWave )
                        {
                            bool higherDegree = false;

                            if ( ShowElliottWave )
                            {
                                DrawElliottWaveLabelsOnCandleNoTA( renderContext, ref realBar, penManager, xCoor, highY, lowY, WaveScenarioNo, true );

                                var degree = hew.GetLastHighestWaveDegree( ).Value.WaveCycle;

                                if ( degree > highestCycle )
                                {
                                    highestCycle = degree;

                                    higherDegree = true;
                                }

                                if ( selectedBarChangedFib )
                                {
                                    if ( higherDegree )
                                    {
                                        _highestSelectedBarIndex = realBar.Index;
                                        _drawFibonacci = true;
                                    }

                                    selectedBarChangedFib = false;
                                }
                            }
                            else
                            {
                                foreach ( var iBarEx in _waveAnnotationAdded )
                                {
                                    var barList = iBarEx.Value;

                                    foreach ( var bar in barList )
                                    {
                                        bar.IsHidden = true;
                                    }
                                }
                                if ( _elliottWaveFibCount > 0 )
                                {
                                    RemoveAllFibonacciTargets();
                                }
                            }
                        }
                        else if ( realBar.HasSignal && ( WaveScenarioNo == 1 ) )
                        {
                            DrawReducedEWaveDatabarSignals( renderContext, ref realBar, penManager, xCoor, highY, lowY );
                        }

                        if ( realBar.HasStructureLabel )
                        {
                            if ( ShowMonoWave )
                            {
                                var structLabel = _taManager.GetStructureLabel( ref realBar );

                                if ( structLabel!= StructureLabelEnum.NONE )
                                {
                                    if ( realBar.IsPeak() )
                                    {
                                        DrawStructureLabelTop( structLabel, renderContext, xCoor, highY, lowY, ref realBar );
                                    }
                                    else if ( realBar.IsTrough() )
                                    {
                                        DrawStructureLabelBottom( structLabel, renderContext, xCoor, highY, lowY, ref realBar );
                                    }
                                }
                            }
                            else
                            {
                                if ( _hasMonolines ) RemoveAllMonowavesLines();
                                if ( _monoWaveAnnotationAdded.Count > 0 ) RemoveAllStructuralLabels();
                            }
                        }
                    }


                    if ( selectedBarChangedFib && !_drawFibonacci )
                    {
                        if ( _elliottWaveFibCount > 0 )
                        {
                            RemoveAllFibonacciTargets();
                        }
                    }

                    selectedBarChangedFib = false;


                    if ( realBar.WaveDirty != WaveDirtyEnum.NONE && HighQualityWaveText )
                    {
                        RemoveWavesFromChart( ref realBar );
                        realBar.WaveDirty = WaveDirtyEnum.NONE;
                    }
                }
            }

            if ( WaveScenarioNo == 1 )
            {
                foreach ( var pair in _drawPointMarkers )
                {
                    var pointMarker = pair.Value;

                    pointMarker.EndBatch( renderContext );
                }

                _drawPointMarkers.Clear();


            }

            if ( _lastBarIndex != realBarIndex )
            {
                _lastBarIndex = realBarIndex;
                ref SBar bar = ref _barList[ realBarIndex ];
                UpdateFibonnaciLastX( bar.High, bar.Low );
            }

            if ( _highestSelectedBarIndex != -1 && _canUpdate && _drawFibonacci )
            {
                ref SBar bar = ref _barList[ _highestSelectedBarIndex ];
                ToggleShowFibonacciTargets( true, ref bar, true );

                _drawFibonacci = false;
            }

            if ( _drawMonoWave )
            {
                ref SBar bar = ref _barList[ _lastReducedSelectedBarIndex ];
                DrawMonoWaveLines( true, ref bar, true );
                _drawMonoWave = false;
            }

            if ( _needToRedrawElliottWave )
            {
                _needToRedrawElliottWave = false;
            }
        }

        private void DrawReducedBareBone( IRenderContext2D renderContext, IRenderPassData renderPassData, IStrokePaletteProvider strokePaletteProvider, IPenManager penManager )
        {
            bool isVerticalChart    = renderPassData.IsVerticalChart;
            var pointSeries         = CurrentRenderPassData.PointSeries as OhlcPointSeries;
            var indexes             = pointSeries.Indexes;
            var xvalues             = pointSeries.XValues;
            var highValues          = pointSeries.HighValues;
            var lowValues           = pointSeries.LowValues;
            var openValues          = pointSeries.OpenValues;
            var closeValues         = pointSeries.CloseValues;
            int count               = pointSeries.Count;

            var seriesDrawingHelper = SeriesDrawingHelpersFactory.GetSeriesDrawingHelper( renderContext, CurrentRenderPassData );

            for ( int i = 0; i < count; ++i )
            {
                var xValue        = xvalues[ i ];
                var open          = openValues[ i ];
                var high          = highValues[ i ];
                var low           = lowValues[ i ];
                var close         = closeValues[ i ];
                var rising        = close >= open;

                var xCoor         = renderPassData.XCoordinateCalculator.GetCoordinate( xValue ).ClipToIntValue( );
                var highY         = renderPassData.YCoordinateCalculator.GetCoordinate( high ).ClipToIntValue( );
                var lowY          = renderPassData.YCoordinateCalculator.GetCoordinate( low ).ClipToIntValue( );

                var currIdx       = indexes[ i ];
                var metadata      = DataSeries.HasMetadata ? DataSeries.Metadata[ currIdx ] : null;

                var pointPen      = rising ? _upWickPen : _downWickPen;

                var tmpBar        = ( SBar ) metadata;
                CheckBarsRepo( ref tmpBar );
                ref SBar realBar  = ref _barList[ tmpBar.Index ];
                var realBarIndex  = realBar.Index;

                if ( realBar.IsSelected )
                {
                    pointPen = penManager.GetPen( Color.FromRgb( 255, 0, 0 ) );
                }

                seriesDrawingHelper.DrawLine( new Point( xCoor, highY ), new Point( xCoor, lowY ), pointPen );
            }
        }

        private void DrawReducedEWave( IRenderContext2D renderContext, IRenderPassData renderPassData, IStrokePaletteProvider strokePaletteProvider, IPenManager penManager )
        {
            bool isVerticalChart       = renderPassData.IsVerticalChart;
            var pointSeries            = CurrentRenderPassData.PointSeries as OhlcPointSeries;
            var indexes                = pointSeries.Indexes;
            var xvalues                = pointSeries.XValues;
            var highValues             = pointSeries.HighValues;
            var lowValues              = pointSeries.LowValues;
            var openValues             = pointSeries.OpenValues;
            var closeValues            = pointSeries.CloseValues;
            int count                  = pointSeries.Count;

            var seriesDrawingHelper    = SeriesDrawingHelpersFactory.GetSeriesDrawingHelper( renderContext, CurrentRenderPassData );

            bool selectedBarChangedFib = false;

            var highestCycle           = ElliottWaveCycle.UNKNOWN;

            int selectedBarCount       = 0;

            int realBarIndex           = -1;

            


            for ( int i = 0; i < count; ++i )
            {
                var xValue        = xvalues[ i ];
                var open          = openValues[ i ];
                var high          = highValues[ i ];
                var low           = lowValues[ i ];
                var close         = closeValues[ i ];
                var rising        = close >= open;

                var xCoor         = renderPassData.XCoordinateCalculator.GetCoordinate( xValue ).ClipToIntValue( );
                var highY         = renderPassData.YCoordinateCalculator.GetCoordinate( high ).ClipToIntValue( );
                var lowY          = renderPassData.YCoordinateCalculator.GetCoordinate( low ).ClipToIntValue( );

                var currIdx       = indexes[ i ];
                var metadata      = DataSeries.HasMetadata ? DataSeries.Metadata[ currIdx ] : null;

                var pointPen      = rising ? _upWickPen : _downWickPen;

                var tmpBar        = ( SBar ) metadata;
                CheckBarsRepo( ref tmpBar );
                ref SBar realBar  = ref _barList[ tmpBar.Index ];
                realBarIndex      = realBar.Index;

                if ( realBar.IsSelected )
                {
                    pointPen = penManager.GetPen( Color.FromRgb( 255, 0, 0 ) );
                }

                seriesDrawingHelper.DrawLine( new Point( xCoor, highY ), new Point( xCoor, lowY ), pointPen );

                if ( realBar != SBar.EmptySBar )
                {
                    if ( realBar.IsSelected )
                    {
                        selectedBarCount++;

                        if ( realBarIndex != _lastReducedSelectedBarIndex )
                        {
                            _lastReducedSelectedBarIndex = realBarIndex;
                            _lastSelectedBarTime         = realBar.LinuxTime;

                            if ( selectedBarCount == 1 )
                            {
                                highestCycle = ElliottWaveCycle.UNKNOWN;
                            }
                        }

                        if ( realBar.SelectionChanged )
                        {
                            selectedBarChangedFib    = true;
                            realBar.SelectionChanged = false;
                        }
                    }

                    if ( _canUpdate )
                    {
                        _lastBottomPosition = 0;
                        _lastTopPosition    = 0;

                        var hew = realBar.GetWaveFromScenario( WaveScenarioNo );

                        if ( hew.HasElliottWave )
                        {
                            bool higherDegree = false;

                            DrawElliottWaveLabelsOnCandleNoTA( renderContext, ref realBar, penManager, xCoor, highY, lowY, WaveScenarioNo, true );

                            var degree = hew.GetLastHighestWaveDegree( ).Value.WaveCycle;

                            if ( degree > highestCycle )
                            {
                                highestCycle = degree;
                                higherDegree = true;
                            }

                            if ( selectedBarChangedFib )
                            {
                                if ( higherDegree )
                                {
                                    _highestSelectedBarIndex = realBar.Index;
                                    _drawFibonacci           = true;
                                }

                                selectedBarChangedFib = false;
                            }
                        }
                        else if ( realBar.HasSignal && ( WaveScenarioNo == 1 ))
                        {
                            DrawReducedEWaveDatabarSignals( renderContext, ref realBar, penManager, xCoor, highY, lowY );
                        }
                    }

                    selectedBarChangedFib = false;


                    if ( realBar.WaveDirty != WaveDirtyEnum.NONE && HighQualityWaveText )
                    {
                        RemoveWavesFromChart( ref realBar );
                        realBar.WaveDirty = WaveDirtyEnum.NONE;
                    }
                }
            }
            if ( WaveScenarioNo == 1 )
            {
                foreach ( var pair in _drawPointMarkers )
                {
                    var pointMarker = pair.Value;

                    pointMarker.EndBatch( renderContext );
                }

                _drawPointMarkers.Clear();


            }


            if ( _lastBarIndex != realBarIndex )
            {
                _lastBarIndex = realBarIndex;
                ref SBar bar = ref _barList[ realBarIndex ];
                UpdateFibonnaciLastX( bar.High, bar.Low );
            }

            if ( _highestSelectedBarIndex != -1 && _canUpdate && _drawFibonacci )
            {
                ref SBar bar = ref _barList[ _highestSelectedBarIndex ];
                ToggleShowFibonacciTargets( true, ref bar, true );

                _drawFibonacci = false;
            }

            if ( _needToRedrawElliottWave )
            {
                _needToRedrawElliottWave = false;
            }
        }
    }
}
