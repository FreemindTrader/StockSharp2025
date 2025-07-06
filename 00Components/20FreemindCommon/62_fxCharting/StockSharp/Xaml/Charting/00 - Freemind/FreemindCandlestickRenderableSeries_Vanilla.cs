using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals.PaletteProviders;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Data.Model;
using SciChart.Drawing.Common;
using fx.Definitions;
using StockSharp.Xaml.Charting.ATony;
using StockSharp.Xaml.Charting.Definitions;
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
using StockSharp.Xaml.Charting.HewFibonacci;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Charting.Numerics.CoordinateCalculators;
using fx.Indicators;
using fx.Common;
using StockSharp.Xaml.Charting.CustomAnnotations;
using fx.Bars;
using System.Runtime.CompilerServices;

namespace StockSharp.Xaml.Charting
{
    public partial class FreemindCandlestickRenderableSeries : FastCandlestickRenderableSeries
    {
        private void DrawVanilla( IRenderContext2D renderContext, IRenderPassData renderPassData, IStrokePaletteProvider strokePaletteProvider, IFillPaletteProvider fillPaletteProvider, IPenManager penManager )
        {
            bool isVerticalChart       = renderPassData.IsVerticalChart;
            double chartRotationAngle  = GetChartRotationAngle( renderPassData );

            var pointSeries            = CurrentRenderPassData.PointSeries as OhlcPointSeries;
            var indexes                = pointSeries.Indexes;
            var xvalues                = pointSeries.XValues;
            var highValues             = pointSeries.HighValues;
            var lowValues              = pointSeries.LowValues;
            var openValues             = pointSeries.OpenValues;
            var closeValues            = pointSeries.CloseValues;
            int count                  = pointSeries.Count;

            //renderContext.SetPrimitivesCachingEnabled( false );
            var seriesDrawingHelper = SeriesDrawingHelpersFactory.GetSeriesDrawingHelper( renderContext, CurrentRenderPassData );

            _lastReducedSelectedBarIndex = -1;

            if ( ShowTradingTime )
            {
                DrawTradingTime( renderContext, renderPassData, indexes, xvalues, count );
            }

            bool selectedBarHasChanged  = false;
            ElliottWaveCycle highestCycle = ElliottWaveCycle.UNKNOWN;
            int realBarIndex = -1;

            int selectedBarCount = 0;

            for ( int i = 0; i < count; ++i )
            {
                var xValue        = xvalues[ i ];
                var open          = openValues[ i ];
                var high          = highValues[ i ];
                var low           = lowValues[ i ];
                var close         = closeValues[ i ];
                var rising        = close >= open;

                var xCoor         = renderPassData.XCoordinateCalculator.GetCoordinate( xValue ).ClipToIntValue( );
                var leftMargin    = ( xCoor - _candleWidth * 0.5 ).ClipToIntValue( );
                var rightMargin   = ( xCoor + _candleWidth * 0.5 ).ClipToIntValue( );

                var closeY        = renderPassData.YCoordinateCalculator.GetCoordinate( rising ? close : open ).ClipToIntValue( );
                var highY         = renderPassData.YCoordinateCalculator.GetCoordinate( high                  ).ClipToIntValue( );
                var lowY          = renderPassData.YCoordinateCalculator.GetCoordinate( low                   ).ClipToIntValue( );
                var openY         = renderPassData.YCoordinateCalculator.GetCoordinate( rising ? open : close ).ClipToIntValue( );

                var pointPen      = rising ? _upWickPen   : _downWickPen;
                var pointFill     = rising ? _upBodyBrush : _downBodyBrush;
                var currIdx       = indexes[ i ];

                var metadata      = DataSeries.HasMetadata ? DataSeries.Metadata[ currIdx ] : null;

                var tmpBar        = ( SBar ) metadata;
                CheckBarsRepo( ref tmpBar );
                ref SBar realBar  = ref _barList[ tmpBar.Index ];
                realBarIndex      = realBar.Index;

                if ( realBar.IsSelected )
                {
                    pointPen = penManager.GetPen( Color.FromRgb( 255, 0, 0 ) );
                    pointFill = renderContext.CreateBrush( new SolidColorBrush( Colors.Red ), Opacity, TextureMappingMode.PerPrimitive );
                }


                seriesDrawingHelper.DrawLine( new Point( xCoor, highY ), new Point( xCoor, closeY ), pointPen );
                seriesDrawingHelper.DrawLine( new Point( xCoor, openY ), new Point( xCoor, lowY ), pointPen );

                if ( rising )
                {
                    NumberUtil.Swap( ref closeY, ref openY );
                }

                seriesDrawingHelper.DrawBox( new Point( leftMargin, closeY ), new Point( rightMargin, openY ), pointFill, pointPen, chartRotationAngle );


                

                if ( realBar != SBar.EmptySBar )
                {
                    if ( realBar.IsSelected )
                    {
                        selectedBarCount++;

                        if ( realBar.Index != _lastSelectedBarIndex )
                        {
                            _lastSelectedBarIndex = realBar.Index;
                            _lastSelectedBarTime = realBar.LinuxTime;

                            if ( selectedBarCount == 1 )
                            {
                                highestCycle = ElliottWaveCycle.UNKNOWN;
                            }
                        }

                        if ( realBar.SelectionChanged )
                        {
                            selectedBarHasChanged = true;
                            realBar.SelectionChanged = false;
                        }
                    }

                    if ( _canUpdate )
                    {
                        _lastBottomPosition = 0;
                        _lastTopPosition = 0;

                        var hew = realBar.GetWaveFromScenario( WaveScenarioNo );

                        if ( hew.HasElliottWave )
                        {
                            bool higherDegree = false;

                            if ( ShowElliottWave )
                            {
                                DrawElliottWaveLabelsOnCandle( renderContext, ref realBar, penManager, xCoor, highY, lowY, WaveScenarioNo, true );

                                var degree = hew.GetLastHighestWaveDegree( ).Value.WaveCycle;

                                if ( degree > highestCycle )
                                {
                                    highestCycle = degree;

                                    higherDegree = true;
                                }


                                if ( selectedBarHasChanged )
                                {
                                    if ( higherDegree )
                                    {
                                        _highestSelectedBarIndex = realBar.Index;
                                        _drawFibonacci = true;
                                    }


                                    selectedBarHasChanged = false;
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

                                if ( realBar.HasSignal )
                                {
                                    DrawDatabarSignals( renderContext, ref realBar, penManager, xCoor, highY, lowY );
                                }
                            }
                        }
                        else if ( realBar.HasSignal )
                        {
                            DrawDatabarSignals( renderContext, ref realBar, penManager, xCoor, highY, lowY );
                        }

                        if ( realBar.HasStructureLabel )
                        {
                            if ( ShowMonoWave )
                            {
                                var structLabel = _taManager.GetStructureLabel( ref realBar );

                                if ( structLabel != StructureLabelEnum.NONE )
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
                                RemoveAllMonowavesLines();
                                RemoveAllStructuralLabels();
                            }
                        }

                        if ( selectedBarHasChanged && !_drawFibonacci )
                        {
                            if ( _elliottWaveFibCount > 0 )
                            {
                                RemoveAllFibonacciTargets();
                            }
                        }

                        selectedBarHasChanged = false;


                        
                    }

                    if ( realBar.WaveDirty != WaveDirtyEnum.NONE && HighQualityWaveText )
                    {
                        RemoveWavesFromChart( ref realBar );

                        realBar.WaveDirty = WaveDirtyEnum.NONE;
                    }
                }
            } // End For Loop

            foreach ( var pair in _drawPointMarkers )
            {
                var pointMarker = pair.Value;

                pointMarker.EndBatch( renderContext );
            }

            _drawPointMarkers.Clear();

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

        private void DrawVanillaBareBone( IRenderContext2D renderContext, IRenderPassData renderPassData, IStrokePaletteProvider strokePaletteProvider, IFillPaletteProvider fillPaletteProvider, IPenManager penManager )
        {
            bool isVerticalChart       = renderPassData.IsVerticalChart;
            double chartRotationAngle  = GetChartRotationAngle( renderPassData );

            var pointSeries            = CurrentRenderPassData.PointSeries as OhlcPointSeries;
            var indexes                = pointSeries.Indexes;
            var xvalues                = pointSeries.XValues;
            var highValues             = pointSeries.HighValues;
            var lowValues              = pointSeries.LowValues;
            var openValues             = pointSeries.OpenValues;
            var closeValues            = pointSeries.CloseValues;
            int count                  = pointSeries.Count;

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
                var leftMargin    = ( xCoor - _candleWidth * 0.5 ).ClipToIntValue( );
                var rightMargin   = ( xCoor + _candleWidth * 0.5 ).ClipToIntValue( );

                var closeY        = renderPassData.YCoordinateCalculator.GetCoordinate( rising ? close : open ).ClipToIntValue( );
                var highY         = renderPassData.YCoordinateCalculator.GetCoordinate( high                  ).ClipToIntValue( );
                var lowY          = renderPassData.YCoordinateCalculator.GetCoordinate( low                   ).ClipToIntValue( );
                var openY         = renderPassData.YCoordinateCalculator.GetCoordinate( rising ? open : close ).ClipToIntValue( );

                var pointPen      = rising ? _upWickPen   : _downWickPen;
                var pointFill     = rising ? _upBodyBrush : _downBodyBrush;
                var currIdx       = indexes[ i ];

                var metadata      = DataSeries.HasMetadata ? DataSeries.Metadata[ currIdx ] : null;

                var tmpBar        = ( SBar ) metadata;
                CheckBarsRepo( ref tmpBar );
                ref SBar realBar  = ref _barList[ tmpBar.Index ];
                
                if ( realBar.IsSelected )
                {
                    pointPen = penManager.GetPen( Color.FromRgb( 255, 0, 0 ) );
                    pointFill = renderContext.CreateBrush( new SolidColorBrush( Colors.Red ), Opacity, TextureMappingMode.PerPrimitive );
                }

                seriesDrawingHelper.DrawLine( new Point( xCoor, highY ), new Point( xCoor, closeY ), pointPen );
                seriesDrawingHelper.DrawLine( new Point( xCoor, openY ), new Point( xCoor, lowY ), pointPen );

                if ( rising )
                {
                    NumberUtil.Swap( ref closeY, ref openY );
                }

                seriesDrawingHelper.DrawBox( new Point( leftMargin, closeY ), new Point( rightMargin, openY ), pointFill, pointPen, chartRotationAngle );
            }
        }

        private void DrawVanillaMonoWave( IRenderContext2D renderContext, IRenderPassData renderPassData, IStrokePaletteProvider strokePaletteProvider, IFillPaletteProvider fillPaletteProvider, IPenManager penManager )
        {
            bool isVerticalChart       = renderPassData.IsVerticalChart;
            double chartRotationAngle  = GetChartRotationAngle( renderPassData );

            var pointSeries            = CurrentRenderPassData.PointSeries as OhlcPointSeries;
            var indexes                = pointSeries.Indexes;
            var xvalues                = pointSeries.XValues;
            var highValues             = pointSeries.HighValues;
            var lowValues              = pointSeries.LowValues;
            var openValues             = pointSeries.OpenValues;
            var closeValues            = pointSeries.CloseValues;
            int count                  = pointSeries.Count;

            //renderContext.SetPrimitivesCachingEnabled( false );
            var seriesDrawingHelper = SeriesDrawingHelpersFactory.GetSeriesDrawingHelper( renderContext, CurrentRenderPassData );

            _lastReducedSelectedBarIndex = -1;

            if ( ShowTradingTime )
            {
                DrawTradingTime( renderContext, renderPassData, indexes, xvalues, count );
            }

            bool selectedBarHasChanged  = false;
            ElliottWaveCycle highestCycle = ElliottWaveCycle.UNKNOWN;
            int realBarIndex = -1;

            int selectedBarCount = 0;

            for ( int i = 0; i < count; ++i )
            {
                var xValue        = xvalues[ i ];
                var open          = openValues[ i ];
                var high          = highValues[ i ];
                var low           = lowValues[ i ];
                var close         = closeValues[ i ];
                var rising        = close >= open;

                var xCoor         = renderPassData.XCoordinateCalculator.GetCoordinate( xValue ).ClipToIntValue( );
                var leftMargin    = ( xCoor - _candleWidth * 0.5 ).ClipToIntValue( );
                var rightMargin   = ( xCoor + _candleWidth * 0.5 ).ClipToIntValue( );

                var closeY        = renderPassData.YCoordinateCalculator.GetCoordinate( rising ? close : open ).ClipToIntValue( );
                var highY         = renderPassData.YCoordinateCalculator.GetCoordinate( high                  ).ClipToIntValue( );
                var lowY          = renderPassData.YCoordinateCalculator.GetCoordinate( low                   ).ClipToIntValue( );
                var openY         = renderPassData.YCoordinateCalculator.GetCoordinate( rising ? open : close ).ClipToIntValue( );

                var pointPen      = rising ? _upWickPen   : _downWickPen;
                var pointFill     = rising ? _upBodyBrush : _downBodyBrush;
                var currIdx       = indexes[ i ];

                var metadata      = DataSeries.HasMetadata ? DataSeries.Metadata[ currIdx ] : null;

                var tmpBar        = ( SBar ) metadata;
                CheckBarsRepo( ref tmpBar );
                ref SBar realBar  = ref _barList[ tmpBar.Index ];
                realBarIndex = realBar.Index;

                if ( realBar.IsSelected )
                {
                    pointPen = penManager.GetPen( Color.FromRgb( 255, 0, 0 ) );
                    pointFill = renderContext.CreateBrush( new SolidColorBrush( Colors.Red ), Opacity, TextureMappingMode.PerPrimitive );
                }


                seriesDrawingHelper.DrawLine( new Point( xCoor, highY ), new Point( xCoor, closeY ), pointPen );
                seriesDrawingHelper.DrawLine( new Point( xCoor, openY ), new Point( xCoor, lowY ), pointPen );

                if ( rising )
                {
                    NumberUtil.Swap( ref closeY, ref openY );
                }

                seriesDrawingHelper.DrawBox( new Point( leftMargin, closeY ), new Point( rightMargin, openY ), pointFill, pointPen, chartRotationAngle );




                if ( realBar != SBar.EmptySBar )
                {
                    if ( realBar.IsSelected )
                    {
                        selectedBarCount++;

                        if ( realBar.Index != _lastSelectedBarIndex )
                        {
                            _lastSelectedBarIndex = realBar.Index;
                            _lastSelectedBarTime = realBar.LinuxTime;

                            if ( selectedBarCount == 1 )
                            {
                                highestCycle = ElliottWaveCycle.UNKNOWN;
                            }
                        }

                        if ( realBar.SelectionChanged )
                        {
                            selectedBarHasChanged = true;
                            realBar.SelectionChanged = false;
                        }
                    }

                    if ( _canUpdate )
                    {
                        _lastBottomPosition = 0;
                        _lastTopPosition = 0;

                        var hew = realBar.GetWaveFromScenario( WaveScenarioNo );

                        if ( hew.HasElliottWave )
                        {
                            bool higherDegree = false;

                            if ( ShowElliottWave )
                            {
                                DrawElliottWaveLabelsOnCandle( renderContext, ref realBar, penManager, xCoor, highY, lowY, WaveScenarioNo, true );

                                var degree = hew.GetLastHighestWaveDegree( ).Value.WaveCycle;

                                if ( degree > highestCycle )
                                {
                                    highestCycle = degree;

                                    higherDegree = true;
                                }


                                if ( selectedBarHasChanged )
                                {
                                    if ( higherDegree )
                                    {
                                        _highestSelectedBarIndex = realBar.Index;
                                        _drawFibonacci = true;
                                    }


                                    selectedBarHasChanged = false;
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

                                if ( realBar.HasSignal )
                                {
                                    DrawDatabarSignals( renderContext, ref realBar, penManager, xCoor, highY, lowY );
                                }
                            }
                        }
                        else if ( realBar.HasSignal )
                        {
                            DrawDatabarSignals( renderContext, ref realBar, penManager, xCoor, highY, lowY );
                        }

                        if ( realBar.HasStructureLabel )
                        {
                            if ( ShowMonoWave )
                            {
                                var structLabel = _taManager.GetStructureLabel( ref realBar );

                                if ( realBar.IsPeak() )
                                {
                                    DrawStructureLabelTop( structLabel, renderContext, xCoor, highY, lowY, ref realBar );
                                }
                                else if ( realBar.IsTrough() )
                                {
                                    DrawStructureLabelBottom( structLabel, renderContext, xCoor, highY, lowY, ref realBar );
                                }
                            }
                            else
                            {
                                RemoveAllMonowavesLines();
                                RemoveAllStructuralLabels();
                            }
                        }

                        if ( selectedBarHasChanged && !_drawFibonacci )
                        {
                            if ( _elliottWaveFibCount > 0 )
                            {
                                RemoveAllFibonacciTargets();
                            }
                        }

                        selectedBarHasChanged = false;
                    }

                    if ( realBar.WaveDirty != WaveDirtyEnum.NONE && HighQualityWaveText )
                    {
                        RemoveWavesFromChart( ref realBar );

                        realBar.WaveDirty = WaveDirtyEnum.NONE;
                    }
                }
            } // End For Loop

            foreach ( var pair in _drawPointMarkers )
            {
                var pointMarker = pair.Value;

                pointMarker.EndBatch( renderContext );
            }

            _drawPointMarkers.Clear();

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


        private void DrawVanillaEWave( IRenderContext2D renderContext, IRenderPassData renderPassData, IStrokePaletteProvider strokePaletteProvider, IFillPaletteProvider fillPaletteProvider, IPenManager penManager )
        {
            bool isVerticalChart          = renderPassData.IsVerticalChart;
            double chartRotationAngle     = GetChartRotationAngle( renderPassData );

            var pointSeries               = CurrentRenderPassData.PointSeries as OhlcPointSeries;
            var indexes                   = pointSeries.Indexes;
            var xvalues                   = pointSeries.XValues;
            var highValues                = pointSeries.HighValues;
            var lowValues                 = pointSeries.LowValues;
            var openValues                = pointSeries.OpenValues;
            var closeValues               = pointSeries.CloseValues;
            int count                     = pointSeries.Count;

            var seriesDrawingHelper       = SeriesDrawingHelpersFactory.GetSeriesDrawingHelper( renderContext, CurrentRenderPassData );

            _lastReducedSelectedBarIndex  = -1;
          

            bool selectedBarHasChanged    = false;
            ElliottWaveCycle highestCycle = ElliottWaveCycle.UNKNOWN;
            int realBarIndex              = -1;

            int selectedBarCount          = 0;

            for ( int i = 0; i < count; ++i )
            {
                var xValue      = xvalues[ i ];
                var open        = openValues[ i ];
                var high        = highValues[ i ];
                var low         = lowValues[ i ];
                var close       = closeValues[ i ];
                var rising      = close >= open;

                var xCoor         = renderPassData.XCoordinateCalculator.GetCoordinate( xValue ).ClipToIntValue( );
                var leftMargin    = ( xCoor - _candleWidth * 0.5 ).ClipToIntValue( );
                var rightMargin   = ( xCoor + _candleWidth * 0.5 ).ClipToIntValue( );

                var closeY        = renderPassData.YCoordinateCalculator.GetCoordinate( rising ? close : open ).ClipToIntValue( );
                var highY         = renderPassData.YCoordinateCalculator.GetCoordinate( high                  ).ClipToIntValue( );
                var lowY          = renderPassData.YCoordinateCalculator.GetCoordinate( low                   ).ClipToIntValue( );
                var openY         = renderPassData.YCoordinateCalculator.GetCoordinate( rising ? open : close ).ClipToIntValue( );

                var pointPen      = rising ? _upWickPen   : _downWickPen;
                var pointFill     = rising ? _upBodyBrush : _downBodyBrush;
                var currIdx       = indexes[ i ];

                var metadata      = DataSeries.HasMetadata ? DataSeries.Metadata[ currIdx ] : null;

                var tmpBar        = ( SBar ) metadata;
                CheckBarsRepo( ref tmpBar );
                ref SBar realBar  = ref _barList[ tmpBar.Index ];
                realBarIndex      = realBar.Index;

                if ( realBar.IsSelected )
                {
                    pointPen = penManager.GetPen( Color.FromRgb( 255, 0, 0 ) );
                    pointFill = renderContext.CreateBrush( new SolidColorBrush( Colors.Red ), Opacity, TextureMappingMode.PerPrimitive );
                }


                seriesDrawingHelper.DrawLine( new Point( xCoor, highY ), new Point( xCoor, closeY ), pointPen );
                seriesDrawingHelper.DrawLine( new Point( xCoor, openY ), new Point( xCoor, lowY ), pointPen );

                if ( rising )
                {
                    NumberUtil.Swap( ref closeY, ref openY );
                }

                seriesDrawingHelper.DrawBox( new Point( leftMargin, closeY ), new Point( rightMargin, openY ), pointFill, pointPen, chartRotationAngle );

                


                if ( realBar != SBar.EmptySBar )
                {
                    if ( realBar.IsSelected )
                    {
                        selectedBarCount++;

                        if ( realBar.Index != _lastSelectedBarIndex )
                        {
                            _lastSelectedBarIndex = realBar.Index;
                            _lastSelectedBarTime = realBar.LinuxTime;

                            if ( selectedBarCount == 1 )
                            {
                                highestCycle = ElliottWaveCycle.UNKNOWN;
                            }
                        }

                        if ( realBar.SelectionChanged )
                        {
                            selectedBarHasChanged = true;
                            realBar.SelectionChanged = false;
                        }
                    }

                    if ( _canUpdate )
                    {
                        _lastBottomPosition = 0;
                        _lastTopPosition = 0;

                        var hew = realBar.GetWaveFromScenario( WaveScenarioNo );

                        if ( hew.HasElliottWave )
                        {
                            bool higherDegree = false;

                            DrawElliottWaveLabelsOnCandle( renderContext, ref realBar, penManager, xCoor, highY, lowY, WaveScenarioNo, true );

                            var degree = hew.GetLastHighestWaveDegree( ).Value.WaveCycle;

                            if ( degree > highestCycle )
                            {
                                highestCycle = degree;

                                higherDegree = true;
                            }


                            if ( selectedBarHasChanged )
                            {
                                if ( higherDegree )
                                {
                                    _highestSelectedBarIndex = realBar.Index;
                                    _drawFibonacci = true;
                                }


                                selectedBarHasChanged = false;
                            }
                        }
                        else if ( realBar.HasSignal )
                        {
                            DrawEWaveDatabarSignals( renderContext, ref realBar, penManager, xCoor, highY, lowY );   
                        }

                        if ( selectedBarHasChanged && !_drawFibonacci )
                        {
                            if ( _elliottWaveFibCount > 0 )
                            {
                                RemoveAllFibonacciTargets();
                            }
                        }

                        selectedBarHasChanged = false;


                        
                    }

                    if ( realBar.WaveDirty != WaveDirtyEnum.NONE && HighQualityWaveText )
                    {
                        RemoveWavesFromChart( ref realBar );

                        realBar.WaveDirty = WaveDirtyEnum.NONE;
                    }
                }
            } // End For Loop


            foreach ( var pair in _drawPointMarkers )
            {
                var pointMarker = pair.Value;

                pointMarker.EndBatch( renderContext );
            }

            _drawPointMarkers.Clear();

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

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        private void CheckBarsRepo( ref SBar tmpBar )
        {
            if ( _barList == null )
            {
                _barList = tmpBar.Parent;

                var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _barList.SymbolEx );

                _symbol = _barList.SymbolEx;
                _period = _barList.SymbolEx.Period;

                if ( aa != null )
                {
                    _hews = ( HewManager ) aa.HewManager;
                    _taManager = ( PeriodXTaManager ) aa.GetPeriodXTa( _period );
                }
            }
        }
    }
}
