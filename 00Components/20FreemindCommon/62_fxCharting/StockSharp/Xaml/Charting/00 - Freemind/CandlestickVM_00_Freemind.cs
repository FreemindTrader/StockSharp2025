using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using SciChart.Charting;
using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Common;
using SciChart.Charting.Model.ChartData;
using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Charting.Utility;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.PaletteProviders;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Data.Model;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml.Charting;
using StockSharp.Xaml.Charting.ATony;
using StockSharp.Xaml.Charting.Definitions;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using fx.Bars;

namespace StockSharp.Xaml.Charting
{
    internal partial class CandlestickVM : UIHigherVM<ChartCandleElement>, IPaletteProvider, IStrokePaletteProvider, IFillPaletteProvider, INullBar
    {
        public long SelectedCandleBarTime
        {
            get
            {
                if ( _candlestickSeries is FreemindCandlestickRenderableSeries )
                {
                    var fm = ( FreemindCandlestickRenderableSeries )_candlestickSeries;

                    return fm.SelectedCandleBarTime;
                }

                return -1;
            }

            set
            {
                if ( _candlestickSeries is FreemindCandlestickRenderableSeries )
                {
                    var fm = ( FreemindCandlestickRenderableSeries )_candlestickSeries;

                    fm.SelectedCandleBarTime = value;
                }
            }
        }

        public void LockFibLevelsObject( )
        {
            if ( _candlestickSeries is FreemindCandlestickRenderableSeries )
            {
                var fm = ( FreemindCandlestickRenderableSeries )_candlestickSeries;

                fm.LockFibLevelsObject( );
            }
        }


        public void RemoveAllEWaves()
        {
            if ( _candlestickSeries is FreemindCandlestickRenderableSeries )
            {
                var fm = ( FreemindCandlestickRenderableSeries )_candlestickSeries;

                fm.RemoveAllEWaves();
            }
        }

        

        public void DeleteAllLockFibLevels( )
        {
            if ( _candlestickSeries is FreemindCandlestickRenderableSeries )
            {
                var fm = ( FreemindCandlestickRenderableSeries )_candlestickSeries;

                fm.DeleteAllLockFibLevels( );
            }
        }

        public void ResetUI()
        {
            if ( _candlestickSeries is FreemindCandlestickRenderableSeries )
            {
                var fm = ( FreemindCandlestickRenderableSeries )_candlestickSeries;

                fm.ResetUI( );
            }
        }

        private BaseRenderableSeries CreateRenderableSeries( )
        {
            BaseRenderableSeries series;
            switch ( ChartElement.DrawStyle )
            {
                case ChartCandleDrawStyles.CandleStick:
                {
                    series = CreateRenderableSeries<FreemindCandlestickRenderableSeries>( new ChartElementViewModel[ 4 ] { _openViewModel, _highViewModel, _lowViewModel, _closeViewModel } );
                        

                    series.SetBindings( FastCandlestickRenderableSeries.FillUpProperty,              ChartElement, "UpFillColor",         BindingMode.TwoWay, new ColorToBrushConverter( ), null );
                    series.SetBindings( FastCandlestickRenderableSeries.FillDownProperty,            ChartElement, "DownFillColor",       BindingMode.TwoWay, new ColorToBrushConverter( ), null );
                    series.SetBindings( FastCandlestickRenderableSeries.StrokeUpProperty,            ChartElement, "UpBorderColor",       BindingMode.TwoWay, null, null );
                    series.SetBindings( FastCandlestickRenderableSeries.StrokeDownProperty,          ChartElement, "DownBorderColor",     BindingMode.TwoWay, null, null );
                    series.SetBindings( FreemindCandlestickRenderableSeries.ShowCandlePatternProperty,   ChartElement, "ShowCandlePattern",   BindingMode.TwoWay, null, null );
                    series.SetBindings( FreemindCandlestickRenderableSeries.ShowIndicatorResultProperty, ChartElement, "ShowIndicatorResult", BindingMode.TwoWay, null, null );
                    series.SetBindings( FreemindCandlestickRenderableSeries.ShowExtremesProperty,        ChartElement, "ShowExtremes",        BindingMode.TwoWay, null, null );
                    series.SetBindings( FreemindCandlestickRenderableSeries.ShowMacdExtremesProperty,    ChartElement, "ShowMacdExtremes",    BindingMode.TwoWay, null, null );
                    series.SetBindings( FreemindCandlestickRenderableSeries.ShowTradingTimeProperty,     ChartElement, "ShowTradingTime",     BindingMode.TwoWay, null, null );                    
                    series.SetBindings( FreemindCandlestickRenderableSeries.ShowElliottWaveProperty,     ChartElement, "ShowElliottWave",     BindingMode.TwoWay, null, null );
                    series.SetBindings( FreemindCandlestickRenderableSeries.IsSimulationProperty,        ChartElement, "IsSimulation",        BindingMode.TwoWay, null, null);
                    series.SetBindings( FreemindCandlestickRenderableSeries.ShowMonoWaveProperty,        ChartElement, "ShowMonoWave",        BindingMode.TwoWay, null, null );
                    series.SetBindings( FreemindCandlestickRenderableSeries.ShowHewDetectionProperty,    ChartElement, "ShowHewDetection",    BindingMode.TwoWay, null, null );
                    series.SetBindings( FreemindCandlestickRenderableSeries.ShowPriceTimeSignalProperty, ChartElement, "ShowPriceTimeSignal", BindingMode.TwoWay, null, null );
                    series.SetBindings( FreemindCandlestickRenderableSeries.ShowDivergenceProperty,      ChartElement, "ShowDivergence",      BindingMode.TwoWay, null, null );
                    series.SetBindings( FreemindCandlestickRenderableSeries.SignalMarginProperty,        ChartElement, "SignalMargin",        BindingMode.TwoWay, null, null );
                    series.SetBindings( FreemindCandlestickRenderableSeries.FifoCapacityProperty,        ChartElement, "FifoCapacity",        BindingMode.TwoWay, null, null );
                    series.SetBindings( FreemindCandlestickRenderableSeries.WaveScenarioNoProperty,      ChartElement, "WaveScenarioNo",      BindingMode.TwoWay, null, null );
                    series.SetBindings( FreemindCandlestickRenderableSeries.WaveImportanceProperty,      ChartElement, "WaveImportance",      BindingMode.TwoWay, null, null );
                    series.SetBindings( FreemindCandlestickRenderableSeries.WaveCycleProperty,           ChartElement, "WaveCycle",           BindingMode.TwoWay, null, null );

                    series.SetBindings( FreemindCandlestickRenderableSeries.HighQualityWaveTextProperty, ChartElement, "HighQualityWaveText", BindingMode.TwoWay, null, null );

                    series.SetBindings( BaseRenderableSeries.StrokeThicknessProperty, ChartElement, "StrokeThickness",                        BindingMode.TwoWay, null, null );
                    series.PaletteProvider = ( this );
                }
                break;

                case ChartCandleDrawStyles.Ohlc:
                {
                    series = CreateRenderableSeries<FastOhlcRenderableSeries>( new ChartElementViewModel[ 4 ] { _openViewModel, _highViewModel, _lowViewModel, _closeViewModel } );

                    series.SetBindings( FastOhlcRenderableSeries.StrokeUpProperty, ChartElement, "UpBorderColor", BindingMode.TwoWay, null, null );
                    series.SetBindings( FastOhlcRenderableSeries.StrokeDownProperty, ChartElement, "DownBorderColor", BindingMode.TwoWay, null, null );
                    series.SetBindings( FastOhlcRenderableSeries.DataPointWidthProperty, ChartElement, "StrokeThickness", BindingMode.TwoWay, new StrokeThickToPointWidthConverter( ), null );

                    BindingOperations.ClearBinding( series, BaseRenderableSeries.StrokeThicknessProperty );
                    series.PaletteProvider = ( this );
                }
                break;

                case ChartCandleDrawStyles.LineOpen:
                case ChartCandleDrawStyles.LineHigh:
                case ChartCandleDrawStyles.LineLow:
                case ChartCandleDrawStyles.LineClose:
                {
                    series = CreateRenderableSeries<FastLineRenderableSeries>( new ChartElementViewModel[ 1 ] { _lineViewModel } );

                    series.SetBindings( BaseRenderableSeries.StrokeProperty, ChartElement, "LineColor", BindingMode.TwoWay, null, null );
                    series.SetBindings( BaseRenderableSeries.StrokeThicknessProperty, ChartElement, "StrokeThickness", BindingMode.TwoWay, null, null );
                    //series.SetBindings( FastLineRenderableSeries.OhlcDrawModeProperty   , ChartElement, "DrawStyle"      , BindingMode.TwoWay, new DrawStylesToModeConverter( ), null );
                }
                break;

                ////case ChartCandleDrawStyles.BoxVolume:
                ////{
                ////    series = CreateRenderableSeries< BoxVolumeRenderableSeries >( new ChildrenChartViewModel[ 1 ] { _volumeViewModel } );

                ////    series.FontFamily = new FontFamily( "Tahoma" );
                ////    series.FontSize = 28.0;
                ////    series.FontWeight = FontWeights.SemiBold;

                ////    series.SetBindings( BoxVolumeRenderableSeries.Timeframe2ColorProperty     , ChartElement, "Timeframe2Color"     , BindingMode.TwoWay, null, null );
                ////    series.SetBindings( BoxVolumeRenderableSeries.Timeframe2FrameColorProperty, ChartElement, "Timeframe2FrameColor", BindingMode.TwoWay, null, null );
                ////    series.SetBindings( BoxVolumeRenderableSeries.Timeframe3ColorProperty     , ChartElement, "Timeframe3Color"     , BindingMode.TwoWay, null, null );
                ////    series.SetBindings( BoxVolumeRenderableSeries.CellFontColorProperty       , ChartElement, "FontColor"           , BindingMode.TwoWay, null, null );
                ////    series.SetBindings( BoxVolumeRenderableSeries.HighVolColorProperty        , ChartElement, "MaxVolumeColor"      , BindingMode.TwoWay, null, null );
                ////    series.SetBindings( BoxVolumeRenderableSeries.Timeframe2Property          , ChartElement, "Timeframe2Multiplier", BindingMode.OneWay, new CandleViewModel.TimeframeMultiplierConverter( _totalMinutes ), null );
                ////    series.SetBindings( BoxVolumeRenderableSeries.Timeframe3Property          , ChartElement, "Timeframe3Multiplier", BindingMode.OneWay, new CandleViewModel.TimeframeMultiplierConverter( _totalMinutes ), null );
                ////}

                //    break;

                //case ChartCandleDrawStyles.ClusterProfile:
                //{
                //    series = CreateRenderableSeries< ClusterProfileRenderableSeries >( new ChildrenChartViewModel[ 1 ] { _volumeViewModel } );

                //    series.FontFamily = new FontFamily( "Tahoma" );
                //    series.FontSize = 28.0;
                //    series.FontWeight = FontWeights.SemiBold;
                //    series.SetBindings( ClusterProfileRenderableSeries.LineColorProperty      , ChartElement, "ClusterLineColor", BindingMode.TwoWay, null, null );
                //    series.SetBindings( ClusterProfileRenderableSeries.TextColorProperty      , ChartElement, "ClusterTextColor", BindingMode.TwoWay, null, null );
                //    series.SetBindings( ClusterProfileRenderableSeries.ClusterColorProperty   , ChartElement, "ClusterColor"    , BindingMode.TwoWay, null, null );
                //    series.SetBindings( ClusterProfileRenderableSeries.ClusterMaxColorProperty, ChartElement, "ClusterMaxColor" , BindingMode.TwoWay, null, null );
                //}

                //    break;

                case ChartCandleDrawStyles.Area:
                {
                    series = CreateRenderableSeries<FastMountainRenderableSeries>( new ChartElementViewModel[ 1 ] { _lineViewModel } );
                    series.Stroke = Colors.Transparent;
                    series.SetBindings( BaseRenderableSeries.StrokeProperty, ChartElement, "LineColor", BindingMode.TwoWay, null, null );
                    series.SetBindings( BaseRenderableSeries.StrokeThicknessProperty, ChartElement, "StrokeThickness", BindingMode.TwoWay, null, null );
                    series.SetBindings( BaseMountainRenderableSeries.FillProperty, ChartElement, "AreaColor", BindingMode.TwoWay, new ColorToBrushConverter( ), null );
                }
                break;

                case ChartCandleDrawStyles.PnF:
                {
                    series = CreateRenderableSeries<FastXORenderableSeries>( new ChartElementViewModel[ 0 ] );
                    series.SetBindings( FastOhlcRenderableSeries.StrokeUpProperty, ChartElement, "UpBorderColor", BindingMode.TwoWay, null, null );
                    series.SetBindings( FastOhlcRenderableSeries.StrokeDownProperty, ChartElement, "DownBorderColor", BindingMode.TwoWay, null, null );
                    series.SetBindings( BaseRenderableSeries.StrokeThicknessProperty, ChartElement, "StrokeThickness", BindingMode.TwoWay, null, null );

                    if ( !_pnfBoxSize.DoubleEquals( 0.0 ) )
                    {
                        ( ( FastXORenderableSeries ) series ).XOBoxSize = _pnfBoxSize;
                    }
                    series.PaletteProvider = ( this );
                }
                break;

                default:
                    throw new InvalidOperationException( "LocalizedStrings.Str2063Params.Put( ChartElement.DrawStyle )" );
            }

            //if( ChartElement.DrawStyle.IsVolumeProfileChart( ) )
            //{
            //    series.SetBindings( TimeframeSegmentRenderableSeries.ShowHorizontalVolumesProperty        , ChartElement, "ShowHorizontalVolumes", BindingMode.TwoWay, null, null );
            //    series.SetBindings( TimeframeSegmentRenderableSeries.LocalHorizontalVolumesProperty       , ChartElement, "LocalHorizontalVolumes", BindingMode.TwoWay, null, null );
            //    series.SetBindings( TimeframeSegmentRenderableSeries.HorizontalVolumeWidthFractionProperty, ChartElement, "HorizontalVolumeWidthFraction", BindingMode.TwoWay, null, null );
            //    series.SetBindings( TimeframeSegmentRenderableSeries.VolumeBarsBrushProperty              , ChartElement, "HorizontalVolumeColor", BindingMode.OneWay, new ColorToBrushConverter( ), null );
            //    series.SetBindings( TimeframeSegmentRenderableSeries.VolBarsFontColorProperty             , ChartElement, "HorizontalVolumeFontColor", BindingMode.TwoWay, null, null );
            //}
            return series;
        }

        bool _needToCenterOnBar;

        DateTime _centerBarTime = DateTime.MinValue;
        

        public bool NeedToCenterOnBar
        {
            get { return _needToCenterOnBar; }
            set { _needToCenterOnBar = value; }
        }

        public void CenterViewOnTime( DateTime selectedBarTime )
        {
            NeedToCenterOnBar = true;
            _centerBarTime = selectedBarTime;            
        }

        public void CheckAndShowFibonacci( )
        {
            if ( _candlestickSeries is FreemindCandlestickRenderableSeries )
            {
                var fm = ( FreemindCandlestickRenderableSeries )_candlestickSeries;

                fm.ToggleShowFibonacciTargets( );
            }
        }

        public bool UpdateNullBar( DateTime barTime, double bid, double ask )
        {
            if ( _lastNullBar == SBar.EmptySBar )
            {
                return false;
            }
                

            var nextBarTime = _lastNullBar.BarTime +  _lastNullBar.BarPeriod ;

            if ( barTime >= _lastNullBar.BarTime && barTime < nextBarTime )
            {
                //float price = (float) ( ( bid + ask ) / 2 );

                /* -----------------------------------------------------------------------------------------------------------
                 * 
                 * Tony:    Fucking solve the bar non continous problem on UI, the problem is because I am using the avg price
                 *          Instead of avg price, I should be using the bid price so the bar will look continus.
                 * 
                 * -----------------------------------------------------------------------------------------------------------
                 */
                float price = ( float ) bid;

                if ( price == 0 )
                {
                    return false;
                }

                if ( price > _lastNullBar.High )
                {
                    _lastNullBar.High = price;
                }

                if ( price < _lastNullBar.Low )
                {
                    _lastNullBar.Low = price;
                }

                _lastNullBar.Close = price;

                _ohlcDataSeries.Update( _lastNullBar.Index, _lastNullBar.Open, _lastNullBar.High, _lastNullBar.Low, _lastNullBar.Close );
                _xyDataSeries.Update( _lastNullBar.Index, _lastNullBar.Close );
            }
            else if ( barTime > nextBarTime && barTime < nextBarTime + _lastNullBar.BarPeriod )
            {
                var newNullBar = _lastNullBar.NewNullBar();
                var latestIndx = _latestBarIndex.Value;

                if ( newNullBar.Index > latestIndx )
                {
                    _latestBarIndex.CompareAndSet( latestIndx, newNullBar.Index );

                    _lastNullBar = newNullBar;

                    _dateTimeUtc = _lastNullBar.BarTime;

                    if ( _lastNullBar.Close  == 0 )
                    {

                    }

                    _ohlcDataSeries.Append( _lastNullBar.BarTime, _lastNullBar.Open, _lastNullBar.High, _lastNullBar.Low, _lastNullBar.Close, _lastNullBar );
                    _xyDataSeries.Append( _lastNullBar.BarTime, _lastNullBar.Close );
                }
            }

            return false;
        }

        bool _updateNullBar = false;
        public bool CanUpdateNullBar
        {
            get
            {
                return _updateNullBar;
            }

            set
            {
                _updateNullBar = value;
            }
        }
    }

}