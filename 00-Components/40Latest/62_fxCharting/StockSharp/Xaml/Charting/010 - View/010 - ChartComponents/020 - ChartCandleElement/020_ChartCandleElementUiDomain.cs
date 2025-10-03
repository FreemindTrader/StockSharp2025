using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.UI;

using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml.Converters;
using SciChart.Charting;
using SciChart.Charting.Model.ChartData;
using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Themes;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.PaletteProviders;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Data.Model;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml.Charting;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using Color = System.Windows.Media.Color;

namespace StockSharp.Xaml.Charting;

/// <summary>
/// The UI business logic to draw the Candlestick UI on the chart
/// </summary>
/// <param name="candle"></param>
public sealed class ChartCandleElementUiDomain( ChartCandleElement candle ) : ChartCompentWpfUiDomain<ChartCandleElement>( candle ), IPaletteProvider, IPaletteProviderSS
{
    private readonly OhlcDataSeriesTF<DateTime, double>                 _ohlcDataSeries          = new OhlcDataSeriesTF<DateTime, double>();
    private readonly XyDataSeries<DateTime, double>                     _xyDataSeries            = new XyDataSeries<DateTime, double>();
    private readonly SynchronizedList<CandlePatternElementUiDomain>    _chartPatternsList       = new SynchronizedList<CandlePatternElementUiDomain>();
    private readonly SynchronizedDictionary<DateTime, Color>            _dateTime2ColorMap       = new SynchronizedDictionary<DateTime, Color>();
    private readonly NotifiableCandlestickUI                            _notifiableCandlestickUI = new NotifiableCandlestickUI(candle);
    private Func<DateTimeOffset, bool, bool, Color?>                    _colorerFunction;

    //
    // Summary:
    //     A viewmodel to a single data-render series pair, used in the new SciChart.Charting.Visuals.SciChartSurface
    //     Mvvm API. For usage, see the SeriesSource property and the Mvvm examples in the
    //     examples suite. You may bind SeriesSource to a collection of SciChart.Charting.Model.ChartSeries.IChartSeriesViewModel
    //     and SciChart will automatically associated the SciChart.Charting.Visuals.RenderableSeries.BaseRenderableSeries
    //     and SciChart.Charting.Model.DataSeries.IDataSeries instances
    //
    // Remarks:
    //     DataSeries are assigned to the RenderableSeries via the SciChart.Charting.Visuals.RenderableSeries.IRenderableSeries.DataSeries
    //     property. Any time a DataSeries is appended to, the parent SciChart.Charting.Visuals.SciChartSurface
    //     will be redrawn
    private ChartSeriesViewModel       _chartSeriesViewModel;
    private TimeframeSegmentDataSeries _tfsDataSeries;              // This is used to draw the Volume Profile
    private DateTime                   _lastCandleTime;             // The time of the most recent candle
    private bool?                      _notAutoScroll;
    private IndexRange                 _visibleRange;               // Current Visible Range of the viewport on the X axis
    private ChartElementViewModel      _openViewModel;              // Candlestick Open
    private ChartElementViewModel      _highViewModel;              // Candlestick High
    private ChartElementViewModel      _lowViewModel;               // Candlestick Low
    private ChartElementViewModel      _closeViewModel;             // Candlestick Close
    private ChartElementViewModel      _lineViewModel;              // When we are drawing lines instead of candlesticks
    private ChartElementViewModel      _volumeViewModel;            // Volume for this particular candle
    private Decimal?                   _pnfBoxSize;                 // Point and figure chart size for the X and O


    private bool _hasDrawnOnce;                                     // One time setting of xAxisVisibleRange.CategoryDateTimeRange = visibleRange;

    public OhlcDataSeriesTF<DateTime, double> OhlcSeries
    {
        get => _ohlcDataSeries;
    }

    public TimeSpan? CandlesTimeframe => OhlcSeries.Timeframe;


    /// <summary>
    /// The list of patterns associated with this candle element
    /// </summary>
    /// <param name="pattern"></param>
    public void AddPattern( CandlePatternElementUiDomain pattern )
    {
        if ( _chartPatternsList.Contains( pattern ) )
            return;

        _chartPatternsList.Add( pattern );
    }

    /// <summary>
    /// Remove a pattern from the _chartPatternsList
    /// </summary>
    /// <param name="pattern"></param>
    public void RemovePattern( CandlePatternElementUiDomain pattern )
    {
        _chartPatternsList.Remove( pattern );
    }

    protected override void Init()
    {
        base.Init();
        string[] strArray = new string[2]
        {
          "UpFillColor",
          "DownFillColor"
        };

        ChartComponentUiDomain.AddChild( _openViewModel   = new ChartElementViewModel( "O", ChartComponentView, new Func<SeriesInfo, Color>( GetSereisInfoColor ), ( s => ( s as OhlcSeriesInfo )?.FormattedOpenValue ), strArray ) );
        ChartComponentUiDomain.AddChild( _highViewModel   = new ChartElementViewModel( "H", ChartComponentView, new Func<SeriesInfo, Color>( GetSereisInfoColor ), ( s => ( s as OhlcSeriesInfo )?.FormattedHighValue ), strArray ) );
        ChartComponentUiDomain.AddChild( _lowViewModel    = new ChartElementViewModel( "L", ChartComponentView, new Func<SeriesInfo, Color>( GetSereisInfoColor ), ( s => ( s as OhlcSeriesInfo )?.FormattedLowValue ), strArray ) );
        ChartComponentUiDomain.AddChild( _closeViewModel  = new ChartElementViewModel( "C", ChartComponentView, new Func<SeriesInfo, Color>( GetSereisInfoColor ), ( s => ( s as OhlcSeriesInfo )?.FormattedCloseValue ), strArray ) );
        ChartComponentUiDomain.AddChild( _lineViewModel   = new ChartElementViewModel( "Line", ChartComponentView, new Func<SeriesInfo, Color>( GetSereisInfoColor ), new Func<SeriesInfo, string>( SetLineViewModelName ), strArray ) );
        ChartComponentUiDomain.AddChild( _volumeViewModel = new ChartElementViewModel( "Vol", ChartComponentView, new Func<SeriesInfo, Color>( GetSereisInfoColor ), ( s =>
                                                                                                                                                                    {
                                                                                                                                                                        var plc = s as OhlCPLSeriesInfo;
                                                                                                                                                                        if ( plc != null )
                                                                                                                                                                            return string.Empty;

                                                                                                                                                                        string str = string.Empty;

                                                                                                                                                                        if ( plc.XValue != null )
                                                                                                                                                                        {
                                                                                                                                                                            str = $"O:{plc.FormattedOpenValue} H:{plc.FormattedHighValue} L:{plc.FormattedLowValue} C:{plc.FormattedCloseValue}";
                                                                                                                                                                        }

                                                                                                                                                                        return $"{str}   {plc.Level}";

                                                                                                                                                                    } ), strArray ) );


        GuiInitSeries();

        // The following code will add the DrawStyle property to the Candlestick UI
        // And whenever the DrawStyle property is changed, the event handler will be called to redraw the candle
        ValidateDrawStylePropertyChanging<ChartCandleDrawStyles>( ChartComponentView, "DrawStyle", new ChartCandleDrawStyles[10]
        {
              ChartCandleDrawStyles.CandleStick,
              ChartCandleDrawStyles.Ohlc,
              ChartCandleDrawStyles.PnF,
              ChartCandleDrawStyles.LineOpen,
              ChartCandleDrawStyles.LineHigh,
              ChartCandleDrawStyles.LineLow,
              ChartCandleDrawStyles.LineClose,
              ChartCandleDrawStyles.BoxVolume,
              ChartCandleDrawStyles.ClusterProfile,
              ChartCandleDrawStyles.Area
        } );
    }

    /// <summary>
    /// If we have specific color for certain bar, we will return that color
    /// 
    /// If not, 
    ///     In light mode, we will return Green for rising bar and Red for falling bar
    ///     In dark mode,  we will return LineGreen for rising bar and OrangeRead for falling bar
    /// </summary>
    /// <param name="series"></param>
    /// <returns></returns>
    private Color GetSereisInfoColor( SeriesInfo series )
    {
        Color color;
        if ( _dateTime2ColorMap.TryGetValue( OhlcSeries.XValues[series.DataSeriesIndex], out color ) )
            return color;

        return IsRising( series ) ? ( !_notifiableCandlestickUI.IsDark ? Colors.Green : Colors.LimeGreen ) : ( !_notifiableCandlestickUI.IsDark ? Colors.Red : Colors.OrangeRed );
    }

    private string SetLineViewModelName( SeriesInfo s )
    {
        if ( !( s is XySeriesInfo xySeriesInfo ) )
            return null;

        switch ( ChartComponentView.DrawStyle )
        {
            case ChartCandleDrawStyles.LineOpen:
                _lineViewModel.Name = "O";
                return xySeriesInfo.FormattedYValue;

            case ChartCandleDrawStyles.LineHigh:
                _lineViewModel.Name = "H";
                return xySeriesInfo.FormattedYValue;

            case ChartCandleDrawStyles.LineLow:
                _lineViewModel.Name = "L";
                return xySeriesInfo.FormattedYValue;

            case ChartCandleDrawStyles.LineClose:
                _lineViewModel.Name = "C";
                return xySeriesInfo.FormattedYValue;

            case ChartCandleDrawStyles.Area:
                _lineViewModel.Name = "C";
                return xySeriesInfo.FormattedYValue;

            default:
                _lineViewModel.Value = null;
                return null;
        }
    }

    public static bool IsRising( SeriesInfo s )
    {
        return s is OhlcSeriesInfo ohlc && ohlc.CloseValue > ohlc.OpenValue;
    }

    private void InitTimeframeSegmentDataSeries()
    {
        if ( _tfsDataSeries != null )
        {
            TimeSpan? timeframe = _tfsDataSeries.Timeframe;

            if ( ( timeframe.HasValue == CandlesTimeframe.HasValue ? ( timeframe.HasValue ? ( timeframe.GetValueOrDefault() == CandlesTimeframe.GetValueOrDefault() ? 1 : 0 ) : 1 ) : 0 ) != 0 )
            {
                return;
            }
        }

        _tfsDataSeries = new TimeframeSegmentDataSeries( CandlesTimeframe, ChartComponentView );
    }

    private void GuiInitSeries()
    {
        if ( !IsUiThread() )
        {
            PerformUiAction( new Action( GuiInitSeries ), true );
        }
        else
        {
            // Setup TimeframeSegmentDataSeries for cluster profile
            InitTimeframeSegmentDataSeries( );

            if ( GetDataSeriesByDrawStyle() == null )
            {
                RemoveChartSeries();
            }
            else
            {
                if ( _chartSeriesViewModel != null )
                {
                    Type drawStyleRS = GetRenderingSeriesByDrawStyle();
                    Type realDrawRS = _chartSeriesViewModel.RenderSeries.GetType();

                    // If nothing changed, no need to do anything
                    if ( _chartSeriesViewModel.DataSeries == GetDataSeriesByDrawStyle() && drawStyleRS == realDrawRS )
                        return;

                    // Now that user has changed the DrawStyle, we need to remove the old chart series
                    BindingOperations.ClearAllBindings( ( DependencyObject ) _chartSeriesViewModel.RenderSeries );
                }

                if ( _chartSeriesViewModel == null || _chartSeriesViewModel.DataSeries != GetDataSeriesByDrawStyle() )
                {
                    RemoveChartSeries();
                    NewChartSeries();
                }
                else
                {
                    _chartSeriesViewModel.RenderSeries = ( IRenderableSeries ) CreateRenderableSeriesAndBinding();
                    ClearAll();
                    SetupAxisMarkerAndBinding( _chartSeriesViewModel.RenderSeries, ( IChartComponent ) ChartComponentView, "ShowAxisMarker", ( string ) null );
                }
            }
        }
    }

    private void RemoveChartSeries()
    {
        if ( _chartSeriesViewModel == null )
            return;
        DrawingSurface.RemoveChartComponent( RootElem );
        _chartSeriesViewModel = null;
    }

    private void InternalGuiInitSeries()
    {
        if ( _tfsDataSeries != null )
            return;
        GuiInitSeries();
    }

    private IDataSeries GetDataSeriesByDrawStyle()
    {
        if ( ChartComponentView.DrawStyle.IsVolumeProfileChart() )
        {
            return ( IDataSeries ) _tfsDataSeries;
        }

        return ChartComponentView.DrawStyle != ChartCandleDrawStyles.Area ? ( IDataSeries ) OhlcSeries : ( IDataSeries ) _xyDataSeries;
    }

    private void SetPnfBoxSize( object _param1 )
    {
        if ( _pnfBoxSize.HasValue || !( _param1 is PnFArg pnFarg ) )
            return;
        _pnfBoxSize = ( decimal ) ( pnFarg.BoxSize.Type == UnitTypes.Percent ? pnFarg.BoxSize.Value : ( pnFarg.BoxSize ) );
        if ( !( _chartSeriesViewModel.RenderSeries is FastXORenderableSeries renderSeries ) )
            return;
        renderSeries.XOBoxSize = ( double ) _pnfBoxSize.Value;
    }


    /// <summary>
    /// For different DrawStyle, we need to return different RenderableSeries type
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private Type GetRenderingSeriesByDrawStyle()
    {
        switch ( ChartComponentView.DrawStyle )
        {
            case ChartCandleDrawStyles.CandleStick:
                return typeof( FastCandlestickRenderableSeries );

            case ChartCandleDrawStyles.Ohlc:
                return typeof( FastOhlcRenderableSeries );

            case ChartCandleDrawStyles.LineOpen:
            case ChartCandleDrawStyles.LineHigh:
            case ChartCandleDrawStyles.LineLow:
            case ChartCandleDrawStyles.LineClose:
                return typeof( FastLineRenderableSeries );

            case ChartCandleDrawStyles.BoxVolume:
                return typeof( BoxVolumeRenderableSeries );

            case ChartCandleDrawStyles.ClusterProfile:
                return typeof( ClusterProfileRenderableSeries );

            case ChartCandleDrawStyles.Area:
                return typeof( FastMountainRenderableSeries );

            case ChartCandleDrawStyles.PnF:
                return typeof( FastXORenderableSeries );

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected override void RootElementPropertyChanged( IChartComponent component, string propertyName )
    {
        base.RootElementPropertyChanged( component, propertyName );
        if ( !( propertyName == "DrawStyle" ) )
            return;
        GuiInitSeries();
    }

    /// <summary>
    /// Create the RenderableSeries according to the DrawStyle and setup the binding to 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    private BaseRenderableSeries CreateRenderableSeriesAndBinding()
    {
        BaseRenderableSeries rSeries;

        switch ( ChartComponentView.DrawStyle )
        {
            case ChartCandleDrawStyles.CandleStick:
                
                rSeries = CreateRenderableSeries<FastCandlestickRenderableSeries>( new ChartElementViewModel[4] { _openViewModel, _highViewModel, _lowViewModel, _closeViewModel } );

                Ecng.Xaml.XamlHelper.SetBindings( rSeries, FastCandlestickRenderableSeries.FillUpProperty,            ChartComponentView, "UpFillColor",     converter: ( IValueConverter ) new Ecng.Xaml.Converters.ColorToBrushConverter()                                   );
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, FastCandlestickRenderableSeries.FillDownProperty,          ChartComponentView, "DownFillColor",   converter: ( IValueConverter ) new Ecng.Xaml.Converters.ColorToBrushConverter()                                   );
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, FastCandlestickRenderableSeries.StrokeUpProperty,          ChartComponentView, "UpBorderColor"                                                                                                                      );
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, FastCandlestickRenderableSeries.StrokeDownProperty,        ChartComponentView, "DownBorderColor"                                                                                                                    );
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, BaseRenderableSeries.StrokeThicknessProperty,              ChartComponentView, "StrokeThickness"                                                                                                                    );
                rSeries.PaletteProvider = this;
                
                break;

            case ChartCandleDrawStyles.Ohlc:
                
                rSeries = CreateRenderableSeries<FastOhlcRenderableSeries>( new ChartElementViewModel[4] { _openViewModel, _highViewModel, _lowViewModel, _closeViewModel } );

                Ecng.Xaml.XamlHelper.SetBindings( rSeries, FastOhlcRenderableSeries.StrokeUpProperty,                 ChartComponentView, "UpBorderColor"                                                                                                                      );
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, FastOhlcRenderableSeries.StrokeDownProperty,               ChartComponentView, "DownBorderColor"                                                                                                                    );
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, FastOhlcRenderableSeries.DataPointWidthProperty,           ChartComponentView, "StrokeThickness", converter: ( IValueConverter ) new StrokeThickToPointWidthConverter()                                             );
                BindingOperations.ClearBinding( rSeries, BaseRenderableSeries.StrokeThicknessProperty                                                                                                                                                                          );
                rSeries.PaletteProvider = this;
                
                break;

            case ChartCandleDrawStyles.LineOpen:
            case ChartCandleDrawStyles.LineHigh:
            case ChartCandleDrawStyles.LineLow:
            case ChartCandleDrawStyles.LineClose:
                
                rSeries = CreateRenderableSeries<FastLineRenderableSeries>( new ChartElementViewModel[1] { _lineViewModel } );

                Ecng.Xaml.XamlHelper.SetBindings( rSeries, BaseRenderableSeries.StrokeProperty,                       _notifiableCandlestickUI, "LineColor"                                                                                                                    );
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, BaseRenderableSeries.StrokeThicknessProperty,              ChartComponentView, "StrokeThickness"                                                                                                                    );
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, MyFastLineRenderableSeries.OhlcDrawModeProperty,           ChartComponentView, "DrawStyle",       converter: ( IValueConverter ) new DrawStylesToModeConverter()                                                    );
                
                break;

            case ChartCandleDrawStyles.BoxVolume:
                
                rSeries = CreateRenderableSeries<BoxVolumeRenderableSeries>( new ChartElementViewModel[1] { _volumeViewModel } );

                Ecng.Xaml.XamlHelper.SetBindings( rSeries, BoxVolumeRenderableSeries.Timeframe2ColorProperty,         _notifiableCandlestickUI, "Timeframe2Color"                                                                                                              );
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, BoxVolumeRenderableSeries.Timeframe2FrameColorProperty,    _notifiableCandlestickUI, "Timeframe2FrameColor"                                                                                                         );
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, BoxVolumeRenderableSeries.Timeframe3ColorProperty,         _notifiableCandlestickUI, "Timeframe3Color"                                                                                                              );
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, BoxVolumeRenderableSeries.CellFontColorProperty,           _notifiableCandlestickUI, "FontColor"                                                                                                                    );
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, BoxVolumeRenderableSeries.HighVolColorProperty,            _notifiableCandlestickUI, "MaxVolumeColor"                                                                                                               );
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, BoxVolumeRenderableSeries.HighVolBackgroundProperty,       _notifiableCandlestickUI, "MaxVolumeBackground"                                                                                                          );
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, BoxVolumeRenderableSeries.Timeframe2Property,              ChartComponentView, "Timeframe2Multiplier", BindingMode.OneWay, ( IValueConverter ) new TimeSpanConverter( CandlesTimeframe ) );
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, BoxVolumeRenderableSeries.Timeframe3Property,              ChartComponentView, "Timeframe3Multiplier", BindingMode.OneWay, ( IValueConverter ) new TimeSpanConverter( CandlesTimeframe ) );
                
                break;

            case ChartCandleDrawStyles.ClusterProfile:
                rSeries = ( BaseRenderableSeries ) CreateRenderableSeries<ClusterProfileRenderableSeries>( new ChartElementViewModel[1] { _volumeViewModel }                                                                                                                   );
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, ClusterProfileRenderableSeries.SeparatorLineColorProperty, _notifiableCandlestickUI, "ClusterSeparatorLineColor"                                                                                                    );
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, ClusterProfileRenderableSeries.LineColorProperty,          _notifiableCandlestickUI, "ClusterLineColor"                                                                                                             );
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, ClusterProfileRenderableSeries.TextColorProperty,          _notifiableCandlestickUI, "ClusterTextColor"                                                                                                             );
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, ClusterProfileRenderableSeries.ClusterColorProperty,       _notifiableCandlestickUI, "ClusterColor"                                                                                                                 );
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, ClusterProfileRenderableSeries.ClusterMaxColorProperty,    _notifiableCandlestickUI, "ClusterMaxColor"                                                                                                              );
                break;

            case ChartCandleDrawStyles.Area:
                rSeries        = CreateRenderableSeries<FastMountainRenderableSeries>( new ChartElementViewModel[1] { _lineViewModel } );
                rSeries.Stroke = Colors.Transparent;

                Ecng.Xaml.XamlHelper.SetBindings( rSeries, BaseRenderableSeries.StrokeProperty,                       _notifiableCandlestickUI, "LineColor"                                                                                                                    );
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, BaseRenderableSeries.StrokeThicknessProperty,              ChartComponentView, "StrokeThickness"                                                                                                                    );
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, BaseMountainRenderableSeries.FillProperty,                 _notifiableCandlestickUI, "AreaColor", converter: ( IValueConverter ) new Ecng.Xaml.Converters.ColorToBrushConverter()                                   );
                break;

            case ChartCandleDrawStyles.PnF:
                rSeries = CreateRenderableSeries<FastXORenderableSeries>( new ChartElementViewModel[4] { _openViewModel, _highViewModel, _lowViewModel, _closeViewModel });
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, FastOhlcRenderableSeries.StrokeUpProperty,                 ChartComponentView, "UpBorderColor"                                                                                                                      );
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, FastOhlcRenderableSeries.StrokeDownProperty,               ChartComponentView, "DownBorderColor"                                                                                                                    );
                Ecng.Xaml.XamlHelper.SetBindings( rSeries, BaseRenderableSeries.StrokeThicknessProperty,              ChartComponentView, "StrokeThickness"                                                                                                                    );
                
                if ( _pnfBoxSize.HasValue )
                {                    
                    ( ( FastXORenderableSeries ) rSeries ).XOBoxSize = ( double ) _pnfBoxSize.GetValueOrDefault( );
                }

                rSeries.PaletteProvider = this;

                break;
            default:
                throw new InvalidOperationException( StringHelper.Put( LocalizedStrings.UnsupportedType, new object[1]
                {
           ChartComponentView.DrawStyle
                } ) );
        }

        if ( ChartComponentView.DrawStyle.IsVolumeProfileChart() )
        {
            Ecng.Xaml.XamlHelper.SetBindings( rSeries, System.Windows.Controls.Control.FontFamilyProperty,                     ChartComponentView, "FontFamily", converter: ( IValueConverter ) new FontFamilyValueConverter() );
            Ecng.Xaml.XamlHelper.SetBindings( rSeries, System.Windows.Controls.Control.FontSizeProperty,                       ChartComponentView, "FontSize", converter: ( IValueConverter ) new TypeCastConverter() );
            Ecng.Xaml.XamlHelper.SetBindings( rSeries, System.Windows.Controls.Control.FontWeightProperty,                     ChartComponentView, "FontWeight" );
            Ecng.Xaml.XamlHelper.SetBindings( rSeries, TimeframeSegmentRenderableSeries.PriceStepProperty,                     ChartComponentView, "PriceStep" );
            Ecng.Xaml.XamlHelper.SetBindings( rSeries, TimeframeSegmentRenderableSeries.ShowHorizontalVolumesProperty,         ChartComponentView, "ShowHorizontalVolumes" );
            Ecng.Xaml.XamlHelper.SetBindings( rSeries, TimeframeSegmentRenderableSeries.LocalHorizontalVolumesProperty,        ChartComponentView, "LocalHorizontalVolumes" );
            Ecng.Xaml.XamlHelper.SetBindings( rSeries, TimeframeSegmentRenderableSeries.HorizontalVolumeWidthFractionProperty, ChartComponentView, "HorizontalVolumeWidthFraction" );
            Ecng.Xaml.XamlHelper.SetBindings( rSeries, TimeframeSegmentRenderableSeries.VolumeBarsBrushProperty,               _notifiableCandlestickUI, "HorizontalVolumeColor", BindingMode.OneWay, ( IValueConverter ) new Ecng.Xaml.Converters.ColorToBrushConverter() );
            Ecng.Xaml.XamlHelper.SetBindings( rSeries, TimeframeSegmentRenderableSeries.VolBarsFontColorProperty,              _notifiableCandlestickUI, "HorizontalVolumeFontColor" );
            Ecng.Xaml.XamlHelper.SetBindings( rSeries, TimeframeSegmentRenderableSeries.DrawSeparateVolumesProperty,           ChartComponentView, "DrawSeparateVolumes" );
            Ecng.Xaml.XamlHelper.SetBindings( rSeries, TimeframeSegmentRenderableSeries.BuyColorProperty,                      _notifiableCandlestickUI, "BuyColor" );
            Ecng.Xaml.XamlHelper.SetBindings( rSeries, TimeframeSegmentRenderableSeries.SellColorProperty,                     _notifiableCandlestickUI, "SellColor" );
            Ecng.Xaml.XamlHelper.SetBindings( rSeries, TimeframeSegmentRenderableSeries.UpColorProperty,                       _notifiableCandlestickUI, "UpColor" );
            Ecng.Xaml.XamlHelper.SetBindings( rSeries, TimeframeSegmentRenderableSeries.DownColorProperty,                     _notifiableCandlestickUI, "DownColor" );
        }
        return rSeries;
    }

    protected override void Clear()
    {
        RemoveChartSeries();
        _notifiableCandlestickUI.Dispose();
    }

    protected override void UpdateUi()
    {
        OhlcSeries.Clear();
        OhlcSeries.Timeframe = new TimeSpan?();
        _xyDataSeries.Clear();
        _tfsDataSeries       = ( TimeframeSegmentDataSeries ) null;
        _lastCandleTime         = new DateTime();
        _pnfBoxSize          = new Decimal?();
        _dateTime2ColorMap.Clear();
        PerformUiAction( new Action( Reset ), true );
    }

    private void NewChartSeries()
    {
        _chartSeriesViewModel = new ChartSeriesViewModel( GetDataSeriesByDrawStyle(), ( IRenderableSeries ) CreateRenderableSeriesAndBinding() );
        DrawingSurface.AddSeriesViewModelsToRoot( RootElem, ( IChartSeriesViewModel ) _chartSeriesViewModel );
        ClearAll();
        SetupAxisMarkerAndBinding( _chartSeriesViewModel.RenderSeries, ( IChartComponent ) ChartComponentView, "ShowAxisMarker", ( string ) null );
    }



    public override void PerformPeriodicalAction()
    {
        base.PerformPeriodicalAction();

        // get the current x-axis range for the view port
        VisibleRangeDpo xAxisVisibleRange = DrawingSurface.GetVisibleRangeDpo(ChartComponentView.XAxisId);
        if ( xAxisVisibleRange == null )
            return;

        IndexRange currentVisibleIndexRange = xAxisVisibleRange.CategoryDateTimeRange;
        
        var totalBarCount = OhlcSeries.Count;
        var notAutoRange  = totalBarCount > 0 && currentVisibleIndexRange.IsDefined && !DrawingSurface.Chart.IsAutoRange;
        var autoScroll    = notAutoRange && DrawingSurface.Chart.IsAutoScroll;

        if ( !_hasDrawnOnce & notAutoRange )
        {
            _hasDrawnOnce = true;

            if ( !autoScroll )
            {
                IndexRange visibleRange = new IndexRange(0, currentVisibleIndexRange.Max - currentVisibleIndexRange.Min);
                xAxisVisibleRange.CategoryDateTimeRange = visibleRange;
            }
        }

        if ( autoScroll )
        {
            bool shouldScrollNow;
            
            if ( _notAutoScroll.HasValue && ( !_notAutoScroll.GetValueOrDefault( ) || _visibleRange != currentVisibleIndexRange ) )
            {
                // Since the desired range is not in the current visible range, we should scroll the view port.
                shouldScrollNow = !( !_notAutoScroll.GetValueOrDefault( ) & _notAutoScroll.HasValue ) ? false : ( currentVisibleIndexRange.Max >= totalBarCount ? true : false );
            }
            else
            {
                shouldScrollNow = true;
            }
                            

            if ( shouldScrollNow && ( currentVisibleIndexRange.Max < totalBarCount || !_notAutoScroll.HasValue ) )
            {
                // scrolling conditions fulfilled, scroll to the end of the total bars
                var visibleBarCount = currentVisibleIndexRange.Max - currentVisibleIndexRange.Min + 1;
                var newRange        = new IndexRange(totalBarCount - visibleBarCount + 1, totalBarCount);
                _visibleRange         = newRange;

                xAxisVisibleRange.CategoryDateTimeRange = newRange;
                
            }
            else
            {
                _visibleRange = currentVisibleIndexRange;
            }
                
            _notAutoScroll = shouldScrollNow;            
        }
        else
        {
            _notAutoScroll = false;
            _visibleRange    = null;
        }
    }

    public override bool Draw( IEnumerableEx<ChartDrawData.IDrawValue> drawData )
    {
        switch ( drawData != null ? drawData.FirstOrDefault() :  null )
        {
            case null:
                return false;

            case ChartDrawData.sCandle _:
                return Draw( CollectionHelper.ToEx( drawData.Cast<ChartDrawData.sCandle>(), drawData.Count ) );

            case ChartDrawData.sCandleColor _:
                return Draw( CollectionHelper.ToEx( drawData.Cast<ChartDrawData.sCandleColor>(), drawData.Count ) );

            default:
                throw new ArgumentOutOfRangeException( "values" );
        }
    }

    public void OnBeginSeriesDraw( IRenderableSeries rSeries )
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// Given a list of candle colors, update the _dateTime2ColorMap dictionary
    /// 
    /// So that the candle that correspond to candleColor.Time will be updated to the candleColor.Color
    /// 
    /// </summary>
    /// <param name="candleColors"></param>
    /// <returns></returns>
    private bool Draw( IEnumerableEx<ChartDrawData.sCandleColor> candleColors )
    {
        if ( CollectionHelper.IsEmpty( candleColors ) )
            return false;

        foreach ( var candleColor in candleColors )
        {
            Color? drawColor = candleColor.Color;
            
            if ( drawColor.HasValue )
            {                                
                _dateTime2ColorMap[candleColor.Time] = candleColor.Color.Value;
            }
            else
            {
                _dateTime2ColorMap.Remove( candleColor.Time );
            }
                
        }
        _chartSeriesViewModel?.RenderSeries.Services?.GetService<ISciChartSurface>()?.InvalidateElement();
        return true;
    }

    /// <summary>
    /// Given a list of candles, update the OhlcSeries and XySeries. 
    /// Since OhlcSeries are scichart data series, which will be used in the RenderableSeries to draw the candles. Updating this series will update the chart.
    /// 
    /// </summary>
    /// <param name="candles"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    private bool Draw( IEnumerableEx<ChartDrawData.sCandle> candles )
    {
        if ( _colorerFunction != ChartComponentView.Colorer )
        {
            _colorerFunction = ChartComponentView.Colorer;
            _chartSeriesViewModel?.RenderSeries.Services?.GetService<ISciChartSurface>()?.InvalidateElement();
        }
        if ( candles == null || CollectionHelper.IsEmpty( candles ) )
            return false;

        int barCount = ( (IEnumerableEx)candles ).Count;
        DateTime lastCandleTime = _lastCandleTime;
        int index = -1;
        bool candleNotFinished = false;

        var timeArray  = new DateTime[barCount];
        var openArray  = new double[barCount];
        var highArray  = new double[barCount];
        var lowArray   = new double[barCount];
        var closeArray = new double[barCount];

        foreach ( var currentCandle in candles )
        {
            object tf = currentCandle.DataType.Arg;

            if ( tf is TimeSpan timeSpan )
            {
                _ohlcDataSeries.Timeframe = new TimeSpan?( timeSpan );
            }
                
            SetPnfBoxSize( tf );

            InternalGuiInitSeries();

            switch ( currentCandle.Time.CompareTo( lastCandleTime ) )
            {
                // The new candle is earlier than the last candle, this is not allowed
                case -1:
                    throw new InvalidOperationException( StringHelper.Put( LocalizedStrings.CannotChangeCandleValue, currentCandle.Time, lastCandleTime ) );

                // We are updating the same candle.
                case 0:
                    candleNotFinished = true;
                    _ohlcDataSeries.Update( currentCandle.Time, currentCandle.OpenPrice, currentCandle.HighPrice, currentCandle.LowPrice, currentCandle.ClosePrice );
                    _xyDataSeries.Update( currentCandle.Time, currentCandle.ClosePrice );

                    if ( currentCandle.CandlePriceLevel != null && _tfsDataSeries != null )
                    {
                        foreach ( CandlePriceLevel level in currentCandle.CandlePriceLevel )
                        {
                            _tfsDataSeries.Update( currentCandle.Time, ( double ) level.Price, level );
                        }                            
                    }
                    --barCount;
                    break;

                // We have a new candle
                default:
                    ++index;
                    timeArray[index]  = currentCandle.Time;
                    openArray[index]  = currentCandle.OpenPrice;
                    highArray[index]  = currentCandle.HighPrice;
                    lowArray[index]   = currentCandle.LowPrice;
                    closeArray[index] = currentCandle.ClosePrice;

                    if ( currentCandle.CandlePriceLevel != null && _tfsDataSeries != null )
                    {
                        foreach ( CandlePriceLevel level in currentCandle.CandlePriceLevel )
                        {
                            _tfsDataSeries.Append( currentCandle.Time, ( double ) level.Price, level );
                        }                           
                        break;
                    }
                    break;
            }
            lastCandleTime = currentCandle.Time;
        }

        if ( barCount == 0 )
        {
            return candleNotFinished;
        }
            
        Array.Resize<DateTime>( ref timeArray, barCount );
        Array.Resize<double>( ref openArray, barCount );
        Array.Resize<double>( ref highArray, barCount );
        Array.Resize<double>( ref lowArray, barCount );
        Array.Resize<double>( ref closeArray, barCount );

        _ohlcDataSeries.Append( timeArray, openArray, highArray, lowArray, closeArray );
        _xyDataSeries.Append( ( IEnumerable<DateTime> ) timeArray, ( IEnumerable<double> ) closeArray );
        _lastCandleTime = lastCandleTime;
        return true;
    }

    Color? IPaletteProviderSS.GetColor( IRenderableSeries rSeries, double _param2, double _param3 )
    {
        return new Color?();
    }

    Color? IPaletteProviderSS.OverrideColor( IRenderableSeries rSeries, double _param2, double _param3, double _param4 )
    {
        return new Color?();
    }

    Color? IPaletteProviderSS.OverrideColor( IRenderableSeries rSeries, double candleIndex, double openPrice, double highPrice, double lowPrice, double closePrice )
    {
        var barIndex = (int)candleIndex;
        var barTime  = OhlcSeries.XValues[barIndex];

        foreach ( var pattern in ( BaseCollection<CandlePatternElementUiDomain, List<CandlePatternElementUiDomain>> ) _chartPatternsList )
        {
            var candleColor = pattern.GetCandleColor(barTime, closePrice > openPrice);
            
            if ( candleColor.HasValue )
                return candleColor;
        }
        
        if ( _dateTime2ColorMap.TryGetValue( barTime, out var barColor ) )
            return barColor;
        
        return _colorerFunction == null ? null : _colorerFunction( TimeHelper.ToDateTimeOffset( barTime, TimeZoneInfo.Utc ), closePrice >= openPrice, barIndex == OhlcSeries.Count - 1 );
    }






    private void Reset()
    {
        ClearAll();
        RemoveChartSeries();
        NewChartSeries();
    }

    private sealed class TimeSpanConverter( TimeSpan? tf ) : IValueConverter
    {

        private readonly TimeSpan? _myTimeSpan = tf;

        object IValueConverter.Convert( object _param1, Type _param2, object _param3, CultureInfo _param4 )
        {
            return ( !_myTimeSpan.HasValue || !( _param1 is int num ) ? new TimeSpan?() : new TimeSpan?( TimeSpan.FromTicks( ( long ) num * _myTimeSpan.Value.Ticks ) ) );
        }

        object IValueConverter.ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
        {
            throw new NotSupportedException();
        }
    }

    /// <summary>
    /// An notifiable Object for CandleStick which will bind to the ChartCandleElement
    /// </summary>
    private sealed class NotifiableCandlestickUI : NotifiableObject, IDisposable
    {

        private readonly ChartCandleElement _candle;

        private IThemeProviderEx _themeProvider;

        private bool _isDarkTheme;

        public NotifiableCandlestickUI( ChartCandleElement candle )
        {
            _candle = candle ?? throw new ArgumentNullException( "elem" );
            _candle.PropertyChanged += new PropertyChangedEventHandler( OnPropertyChanged );
            InitThemes();
            DevExpress.Xpf.Core.ThemeManager.ApplicationThemeChanged += new DevExpress.Xpf.Core.ThemeChangedRoutedEventHandler( OnThemeChanged );
        }

        public void Dispose()
        {
            _candle.PropertyChanged -= new PropertyChangedEventHandler( OnPropertyChanged );
            DevExpress.Xpf.Core.ThemeManager.ApplicationThemeChanged -= new DevExpress.Xpf.Core.ThemeChangedRoutedEventHandler( OnThemeChanged );
            GC.SuppressFinalize( this );
        }

        private void OnPropertyChanged( object d, PropertyChangedEventArgs e )
        {
            NotifyChanged( e.PropertyName );
        }

        private void OnThemeChanged( DependencyObject d, DevExpress.Xpf.Core.ThemeChangedRoutedEventArgs e )
        {
            InitThemes();
            NotifyChanged( "LineColor"                 );
            NotifyChanged( "AreaColor"                 );
            NotifyChanged( "FontColor"                 );
            NotifyChanged( "Timeframe2Color"           );
            NotifyChanged( "Timeframe2FrameColor"      );
            NotifyChanged( "Timeframe3Color"           );
            NotifyChanged( "MaxVolumeColor"            );
            NotifyChanged( "MaxVolumeBackground"       );
            NotifyChanged( "ClusterLineColor"          );
            NotifyChanged( "ClusterSeparatorLineColor" );
            NotifyChanged( "ClusterTextColor"          );
            NotifyChanged( "ClusterColor"              );
            NotifyChanged( "ClusterMaxColor"           );
            NotifyChanged( "HorizontalVolumeColor"     );
            NotifyChanged( "HorizontalVolumeFontColor" );
            NotifyChanged( "BuyColor"                  );
            NotifyChanged( "SellColor"                 );
            NotifyChanged( "UpColor"                   );
            NotifyChanged( "DownColor"                 );
        }

        private void InitThemes()
        {
            IsDark = ChartHelper2025.CurrChartTheme() == "ExpressionDark";
            //_themeProvider = ThemeManager.GetThemeProvider(ChartHelper2025.CurrChartTheme());

            _themeProvider = new ThemeColorProviderEx
            {
                BoxVolumeTimeframe2Color         = Colors.LightGray,
                BoxVolumeTimeframe2FrameColor    = Colors.Gray,
                BoxVolumeTimeframe3Color         = Colors.DarkGray,
                BoxVolumeHighVolColor            = Colors.Red,
                ClusterProfileLineColor          = Colors.Black,
                ClusterProfileSeparatorLineColor = Colors.Gray,
                ClusterProfileTextColor          = Colors.White,
                ClusterProfileClusterColor       = Colors.LightBlue,
                ClusterProfileClusterMaxColor    = Colors.DarkBlue,
                BoxVolumeCellFontColor           = Colors.Black
            };
        }

        public bool IsDark
        {
            get => _isDarkTheme;
            private set => _isDarkTheme = value;
        }

        public Color LineColor
        {
            get
            {
                if ( _candle.LineColor.HasValue )
                    return _candle.LineColor.GetValueOrDefault();

                return !IsDark ? Colors.Blue : Colors.Orange;
            }
            set => _candle.LineColor = new Color?( value );
        }

        public Color AreaColor
        {
            get
            {
                if ( _candle.AreaColor.HasValue )
                    return _candle.AreaColor.GetValueOrDefault();

                return !IsDark ? Colors.Blue : Colors.Orange;
            }
            set => _candle.AreaColor = new Color?( value );
        }

        public Color FontColor
        {
            get
            {
                if ( _candle.FontColor.HasValue )
                    return _candle.FontColor.GetValueOrDefault();

                return !IsDark ? Colors.Black : Colors.White;
            }
            set => _candle.FontColor = new Color?( value );
        }

        /// <summary>
        /// This likely refers to how different timeframes (e.g., daily, hourly, 15-minute charts) might be visually represented or differentiated,
        /// possibly with different colors, within the StockSharp platform's charting tools or when designing trading strategies.
        /// </summary>
        public Color Timeframe2Color
        {
            get
            {
                return _candle.Timeframe2Color ?? _themeProvider.BoxVolumeTimeframe2Color;
            }
            set => _candle.Timeframe2Color = new Color?( value );
        }

        /// <summary>
        /// Similar to "Timeframe2Color," "Timeframe2FrameColor" is likely a concept within StockSharp related to customizing the visual representation of 
        /// different timeframes on charts, potentially by applying specific colors to elements associated with each timeframe, 
        /// such as the borders or backgrounds of frames within the charting interface. Although a direct definition wasn't found, 
        /// it's probable that this feature would allow users to visually distinguish between charts displaying data from different 
        /// timeframes for enhanced analysis or strategy development within the StockSharp platform.
        /// </summary>
        public Color Timeframe2FrameColor
        {
            get
            {
                return _candle.Timeframe2FrameColor ?? _themeProvider.BoxVolumeTimeframe2FrameColor;
            }
            set => _candle.Timeframe2FrameColor = new Color?( value );
        }

        /// <summary>
        /// "Timeframe3Color" likely refers to the ability to define distinct color schemes or styles that are applied to charts
        /// when using a specific timeframe, possibly the third in a sequence of defined timeframes or a specific custom setting. 
        /// To implement this, you would likely use the chart customization features within StockSharp to save different color schemes as chart styles tied to different timeframes.
        /// </summary>
        public Color Timeframe3Color
        {
            get
            {
                return _candle.Timeframe3Color ?? _themeProvider.BoxVolumeTimeframe3Color;
            }
            set => _candle.Timeframe3Color = new Color?( value );
        }

        public Color MaxVolumeColor
        {
            get
            {
                return _candle.MaxVolumeColor ?? _themeProvider.BoxVolumeHighVolColor;
            }
            set => _candle.MaxVolumeColor = new Color?( value );
        }

        public Color MaxVolumeBackground
        {
            get => _candle.MaxVolumeBackground ?? Colors.LightBlue;
            set => _candle.MaxVolumeBackground = new Color?( value );
        }

        public Color ClusterLineColor
        {
            get
            {
                return _candle.ClusterLineColor ?? _themeProvider.ClusterProfileLineColor;
            }
            set => _candle.ClusterLineColor = new Color?( value );
        }

        public Color ClusterSeparatorLineColor
        {
            get
            {
                return _candle.ClusterSeparatorLineColor ?? _themeProvider.ClusterProfileSeparatorLineColor;
            }
            set => _candle.ClusterSeparatorLineColor = new Color?( value );
        }

        public Color ClusterTextColor
        {
            get
            {
                return _candle.ClusterTextColor ?? _themeProvider.ClusterProfileTextColor;
            }
            set => _candle.ClusterTextColor = new Color?( value );
        }

        public Color ClusterColor
        {
            get
            {
                return _candle.ClusterColor ?? _themeProvider.ClusterProfileClusterColor;
            }
            set => _candle.ClusterColor = new Color?( value );
        }

        public Color ClusterMaxColor
        {
            get
            {
                return _candle.ClusterMaxColor ?? _themeProvider.ClusterProfileClusterMaxColor;
            }
            set => _candle.ClusterMaxColor = new Color?( value );
        }

        public Color HorizontalVolumeColor
        {
            get => _candle.HorizontalVolumeColor ?? Colors.Green;
            set => _candle.HorizontalVolumeColor = new Color?( value );
        }

        public Color HorizontalVolumeFontColor
        {
            get
            {
                return _candle.HorizontalVolumeFontColor ?? _themeProvider.BoxVolumeCellFontColor;
            }
            set => _candle.HorizontalVolumeFontColor = new Color?( value );
        }

        public Color BuyColor
        {
            get => _candle.BuyColor ?? Colors.Green;
            set => _candle.BuyColor = new Color?( value );
        }

        public Color SellColor
        {
            get => _candle.SellColor ?? Colors.Red;
            set => _candle.SellColor = new Color?( value );
        }

        public Color UpColor
        {
            get
            {
                Color? upColor = _candle.UpColor;
                if ( upColor.HasValue )
                    return upColor.GetValueOrDefault();
                return !IsDark ? Colors.DarkGreen : Colors.Yellow;
            }
            set => _candle.UpColor = new Color?( value );
        }

        public Color DownColor
        {
            get
            {
                Color? downColor = _candle.DownColor;
                if ( downColor.HasValue )
                    return downColor.GetValueOrDefault();
                return !IsDark ? Colors.Red : Colors.DeepPink;
            }
            set => _candle.DownColor = new Color?( value );
        }
    }

    
}


