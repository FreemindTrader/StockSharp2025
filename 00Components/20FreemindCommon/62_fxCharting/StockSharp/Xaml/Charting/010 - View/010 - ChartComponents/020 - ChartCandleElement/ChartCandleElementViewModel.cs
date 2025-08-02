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
using System.Windows.Media;
using ChartCandleDrawStyles = StockSharp.Charting.ChartCandleDrawStyles;
using Color = System.Windows.Media.Color;

#nullable disable
public sealed class ChartCandleElementViewModel(ChartCandleElement candle) : ChartCompentWpfBaseViewModel<ChartCandleElement>(candle),
                                                                             IPaletteProvider
{
    public void OnBeginSeriesDraw(IRenderableSeries rSeries)
    {
        throw new NotImplementedException();
    }


    private sealed class ChartCandleElementHelper : NotifiableObject, IDisposable
    {

        private readonly ChartCandleElement _candle;

        private IThemeProviderEx _themeProvider;

        private bool _isDarkTheme;

        public ChartCandleElementHelper(ChartCandleElement _param1)
        {
            _candle = _param1 ?? throw new ArgumentNullException("elem");
            _candle.PropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged);
            InitThemes();
            DevExpress.Xpf.Core.ThemeManager.ApplicationThemeChanged += new DevExpress.Xpf.Core.ThemeChangedRoutedEventHandler(OnThemeChanged);
        }

        public void Dispose()
        {
            _candle.PropertyChanged -= new PropertyChangedEventHandler(OnPropertyChanged);
            DevExpress.Xpf.Core.ThemeManager.ApplicationThemeChanged -= new DevExpress.Xpf.Core.ThemeChangedRoutedEventHandler(OnThemeChanged);
            GC.SuppressFinalize((object)this);
        }

        private void OnPropertyChanged(object d, PropertyChangedEventArgs e)
        {
            NotifyChanged(e.PropertyName);
        }

        private void OnThemeChanged(DependencyObject d, DevExpress.Xpf.Core.ThemeChangedRoutedEventArgs e)
        {
            InitThemes();
            NotifyChanged("LineColor");
            NotifyChanged("AreaColor");
            NotifyChanged("FontColor");
            NotifyChanged("Timeframe2Color");
            NotifyChanged("Timeframe2FrameColor");
            NotifyChanged("Timeframe3Color");
            NotifyChanged("MaxVolumeColor");
            NotifyChanged("MaxVolumeBackground");
            NotifyChanged("ClusterLineColor");
            NotifyChanged("ClusterSeparatorLineColor");
            NotifyChanged("ClusterTextColor");
            NotifyChanged("ClusterColor");
            NotifyChanged("ClusterMaxColor");
            NotifyChanged("HorizontalVolumeColor");
            NotifyChanged("HorizontalVolumeFontColor");
            NotifyChanged("BuyColor");
            NotifyChanged("SellColor");
            NotifyChanged("UpColor");
            NotifyChanged("DownColor");
        }

        private void InitThemes()
        {
            IsDark = ChartHelper2025.CurrChartTheme() == "ExpressionDark";
            //_themeProvider = ThemeManager.GetThemeProvider(ChartHelper2025.CurrChartTheme());

            _themeProvider = new ThemeColorProviderEx
            {
                BoxVolumeTimeframe2Color = Colors.LightGray,
                BoxVolumeTimeframe2FrameColor = Colors.Gray,
                BoxVolumeTimeframe3Color = Colors.DarkGray,
                BoxVolumeHighVolColor = Colors.Red,
                ClusterProfileLineColor = Colors.Black,
                ClusterProfileSeparatorLineColor = Colors.Gray,
                ClusterProfileTextColor = Colors.White,
                ClusterProfileClusterColor = Colors.LightBlue,
                ClusterProfileClusterMaxColor = Colors.DarkBlue,
                BoxVolumeCellFontColor = Colors.Black
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
                Color? lineColor = _candle.LineColor;
                if ( lineColor.HasValue )
                    return lineColor.GetValueOrDefault();
                return !IsDark ? Colors.Blue : Colors.Orange;
            }
            set => _candle.LineColor = new Color?(value);
        }

        public Color AreaColor
        {
            get
            {
                Color? areaColor = _candle.AreaColor;
                if ( areaColor.HasValue )
                    return areaColor.GetValueOrDefault();
                return !IsDark ? Colors.Blue : Colors.Orange;
            }
            set => _candle.AreaColor = new Color?(value);
        }

        public Color FontColor
        {
            get
            {
                Color? fontColor = _candle.FontColor;
                if ( fontColor.HasValue )
                    return fontColor.GetValueOrDefault();
                return !IsDark ? Colors.Black : Colors.White;
            }
            set => _candle.FontColor = new Color?(value);
        }

        public Color Timeframe2Color
        {
            get
            {
                return _candle.Timeframe2Color ?? _themeProvider.BoxVolumeTimeframe2Color;
            }
            set => _candle.Timeframe2Color = new Color?(value);
        }

        public Color Timeframe2FrameColor
        {
            get
            {
                return _candle.Timeframe2FrameColor ?? _themeProvider.BoxVolumeTimeframe2FrameColor;
            }
            set => _candle.Timeframe2FrameColor = new Color?(value);
        }

        public Color Timeframe3Color
        {
            get
            {
                return _candle.Timeframe3Color ?? _themeProvider.BoxVolumeTimeframe3Color;
            }
            set => _candle.Timeframe3Color = new Color?(value);
        }

        public Color MaxVolumeColor
        {
            get
            {
                return _candle.MaxVolumeColor ?? _themeProvider.BoxVolumeHighVolColor;
            }
            set => _candle.MaxVolumeColor = new Color?(value);
        }

        public Color MaxVolumeBackground
        {
            get => _candle.MaxVolumeBackground ?? Colors.LightBlue;
            set => _candle.MaxVolumeBackground = new Color?(value);
        }

        public Color ClusterLineColor
        {
            get
            {
                return _candle.ClusterLineColor ?? _themeProvider.ClusterProfileLineColor;
            }
            set => _candle.ClusterLineColor = new Color?(value);
        }

        public Color ClusterSeparatorLineColor
        {
            get
            {
                return _candle.ClusterSeparatorLineColor ?? _themeProvider.ClusterProfileSeparatorLineColor;
            }
            set => _candle.ClusterSeparatorLineColor = new Color?(value);
        }

        public Color ClusterTextColor
        {
            get
            {
                return _candle.ClusterTextColor ?? _themeProvider.ClusterProfileTextColor;
            }
            set => _candle.ClusterTextColor = new Color?(value);
        }

        public Color ClusterColor
        {
            get
            {
                return _candle.ClusterColor ?? _themeProvider.ClusterProfileClusterColor;
            }
            set => _candle.ClusterColor = new Color?(value);
        }

        public Color ClusterMaxColor
        {
            get
            {
                return _candle.ClusterMaxColor ?? _themeProvider.ClusterProfileClusterMaxColor;
            }
            set => _candle.ClusterMaxColor = new Color?(value);
        }

        public Color HorizontalVolumeColor
        {
            get => _candle.HorizontalVolumeColor ?? Colors.Green;
            set => _candle.HorizontalVolumeColor = new Color?(value);
        }

        public Color HorizontalVolumeFontColor
        {
            get
            {
                return _candle.HorizontalVolumeFontColor ?? _themeProvider.BoxVolumeCellFontColor;
            }
            set => _candle.HorizontalVolumeFontColor = new Color?(value);
        }

        public Color BuyColor
        {
            get => _candle.BuyColor ?? Colors.Green;
            set => _candle.BuyColor = new Color?(value);
        }

        public Color SellColor
        {
            get => _candle.SellColor ?? Colors.Red;
            set => _candle.SellColor = new Color?(value);
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
            set => _candle.UpColor = new Color?(value);
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
            set => _candle.DownColor = new Color?(value);
        }
    }

    private readonly OhlcDataSeriesTF<DateTime, double> _ohlcDataSeries;

    private readonly XyDataSeries<DateTime, double> _xyDataSeries;

    private readonly SynchronizedList<CandlePatternElementViewModel> _chartPatternList;

    private ChartSeriesViewModel _chartSeriesViewModel;

    //private TimeframeSegmentDataSeries _timeframeSegmentDataSeries;

    private DateTime _dateTimeUtc;

    private bool? _boolean01;

    private IndexRange _indexRange;

    private ChartElementViewModel _openViewModel;

    private ChartElementViewModel _highViewModel;

    private ChartElementViewModel _lowViewModel;

    private ChartElementViewModel _closeViewModel;

    private ChartElementViewModel _lineViewModel;

    private ChartElementViewModel _volumeViewModel;

    private Decimal? _pnfBoxSize;

    private Func<DateTimeOffset, bool, bool, Color?> _colorerFunction;

    private readonly SynchronizedDictionary<DateTime, Color> _dateTime2ColorMap;

    private readonly ChartCandleElementViewModel.ChartCandleElementHelper _candleHelper = new ChartCandleElementViewModel.ChartCandleElementHelper(candle);

    private bool _boolean02;

    public OhlcDataSeriesTF<DateTime, double> OhlcSeries
    {
        get => _ohlcDataSeries;
    }

    public TimeSpan? CandlesTimeframe => OhlcSeries.Timeframe;

    

    public void AddPattern(CandlePatternElementViewModel _param1)
    {
        if ( ( (BaseCollection<CandlePatternElementViewModel, List<CandlePatternElementViewModel>>)_chartPatternList ).Contains(_param1) )
            return;
        ( (BaseCollection<CandlePatternElementViewModel, List<CandlePatternElementViewModel>>)_chartPatternList ).Add(_param1);
    }

    public void RemovePattern(CandlePatternElementViewModel _param1)
    {
        ( (BaseCollection<CandlePatternElementViewModel, List<CandlePatternElementViewModel>>)_chartPatternList ).Remove(_param1);
    }

    protected override void Init()
    {
        base.Init();
        string[] strArray = new string[2]
        {
          "UpFillColor",
          "DownFillColor"
        };
        ChartViewModel.AddChild(_openViewModel = new ChartElementViewModel("O", ChartComponentView, new Func<SeriesInfo, Color>(GetSereisInfoColor), ( s => ( s as OhlcSeriesInfo )?.FormattedOpenValue ), strArray));
        ChartViewModel.AddChild(_highViewModel = new ChartElementViewModel("H", ChartComponentView, new Func<SeriesInfo, Color>(GetSereisInfoColor), ( s => ( s as OhlcSeriesInfo )?.FormattedHighValue ), strArray));
        ChartViewModel.AddChild(_lowViewModel = new ChartElementViewModel("L", ChartComponentView, new Func<SeriesInfo, Color>(GetSereisInfoColor), ( s => ( s as OhlcSeriesInfo )?.FormattedLowValue ), strArray));
        ChartViewModel.AddChild(_closeViewModel = new ChartElementViewModel("C", ChartComponentView, new Func<SeriesInfo, Color>(GetSereisInfoColor), ( s => ( s as OhlcSeriesInfo )?.FormattedCloseValue ), strArray));
        ChartViewModel.AddChild(_lineViewModel = new ChartElementViewModel("Line", ChartComponentView, new Func<SeriesInfo, Color>(GetSereisInfoColor), new Func<SeriesInfo, string>(SetLineViewModelName), strArray));
        //ChartViewModel.AddChild( _volumeViewModel = new ChildrenChartViewModel( "Vol", ChartComponent, new Func< SeriesInfo, Color >( GetCandleColor ), ( s => ( s as TimeframeSegmentSeriesInfo )?.Volume.ToString( ) ), strArray ) );
        GuiInitSeries();
        AddStylePropertyChanging<StockSharp.Charting.ChartCandleDrawStyles>((IChartComponent)ChartComponentView, "DrawStyle", new StockSharp.Charting.ChartCandleDrawStyles[10]
        {
              StockSharp.Charting.ChartCandleDrawStyles.CandleStick,
              StockSharp.Charting.ChartCandleDrawStyles.Ohlc,
              StockSharp.Charting.ChartCandleDrawStyles.PnF,
              StockSharp.Charting.ChartCandleDrawStyles.LineOpen,
              StockSharp.Charting.ChartCandleDrawStyles.LineHigh,
              StockSharp.Charting.ChartCandleDrawStyles.LineLow,
              StockSharp.Charting.ChartCandleDrawStyles.LineClose,
              StockSharp.Charting.ChartCandleDrawStyles.BoxVolume,
              StockSharp.Charting.ChartCandleDrawStyles.ClusterProfile,
              StockSharp.Charting.ChartCandleDrawStyles.Area
        });
    }

    private Color GetSereisInfoColor(SeriesInfo series)
    {
        Color color;
        if ( _dateTime2ColorMap.TryGetValue(OhlcSeries.XValues[series.DataSeriesIndex], out color) )
            return color;
        return ChartCandleElementViewModel.IsRising(series) ? ( !_candleHelper.IsDark ? Colors.Green : Colors.LimeGreen ) : ( !_candleHelper.IsDark ? Colors.Red : Colors.OrangeRed );
    }

    private string SetLineViewModelName(SeriesInfo s)
    {
        if ( !( s is XySeriesInfo xySeriesInfo ) )
            return (string)null;
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
                _lineViewModel.Value = (string)null;
                return (string)null;
        }
    }

    public static bool IsRising(SeriesInfo s)
    {
        return s is OhlcSeriesInfo ohlc && ohlc.CloseValue > ohlc.OpenValue;
    }

    private void InitTimeframeSegmentDataSeries()
    {
        //    TimeSpan? candlesTimeframe = CandlesTimeframe;
        //    TimeframeSegmentDataSeries zQ73Ei9NuGdXx = _timeframeSegmentDataSeries;
        //    TimeframeSegmentDataSeries segmentDataSeries;
        //    if (zQ73Ei9NuGdXx != null)
        //    {
        //      TimeSpan? timeframe = zQ73Ei9NuGdXx.Timeframe;
        //    TimeSpan? nullable = candlesTimeframe;
        //      if ((timeframe.HasValue == nullable.HasValue? (timeframe.HasValue? (timeframe.GetValueOrDefault() == nullable.GetValueOrDefault()? 1 : 0) : 1) : 0) != 0)
        //      {
        //        segmentDataSeries = zQ73Ei9NuGdXx;
        //        goto label_4;
        //      }
        //    }
        //    segmentDataSeries = new TimeframeSegmentDataSeries(candlesTimeframe, ChartComponentView);
        //label_4:
        //_timeframeSegmentDataSeries = segmentDataSeries;
    }

    private void GuiInitSeries()
    {
        if ( !DrawableChartComponentBaseViewModel.IsUiThread() )
        {
            PerformUiAction(new Action(GuiInitSeries), true);
        }
        else
        {
            InitTimeframeSegmentDataSeries();
            if ( GetDataSeriesByDrawStyle() == null )
            {
                RemoveChartSeries();
            }
            else
            {
                if ( _chartSeriesViewModel != null )
                {
                    Type type1 = GetRenderingSeriesByType();
                    Type type2 = _chartSeriesViewModel.RenderSeries.GetType();
                    if ( _chartSeriesViewModel.DataSeries == GetDataSeriesByDrawStyle() && type1 == type2 )
                        return;
                    BindingOperations.ClearAllBindings((DependencyObject)_chartSeriesViewModel.RenderSeries);
                }
                if ( _chartSeriesViewModel == null || _chartSeriesViewModel.DataSeries != GetDataSeriesByDrawStyle() )
                {
                    RemoveChartSeries();
                    NewChartSeries();
                }
                else
                {
                    _chartSeriesViewModel.RenderSeries = (IRenderableSeries)CreateRenderableSeries();
                    ClearAll();
                    SetupAxisMarkerAndBinding(_chartSeriesViewModel.RenderSeries, (IChartComponent)ChartComponentView, "ShowAxisMarker", (string)null);
                }
            }
        }
    }

    private void RemoveChartSeries()
    {
        if ( _chartSeriesViewModel == null )
            return;
        DrawingSurface.RemoveChartComponent(RootElem);
        _chartSeriesViewModel = null;
    }

    private void InternalGuiInitSeries()
    {
        if ( _timeframeSegmentDataSeries != null )
            return;
        GuiInitSeries();
    }

    private IDataSeries GetDataSeriesByDrawStyle()
    {       
        if ( ChartComponentView.DrawStyle.IsVolumeProfileChart() )
        {
            //return (IDataSeries)_timeframeSegmentDataSeries;
            return null;
        }
            
        return ChartComponentView.DrawStyle != ChartCandleDrawStyles.Area ? (IDataSeries)OhlcSeries : (IDataSeries)_xyDataSeries;
    }

    private void SetPnfBoxSize(object _param1)
    {
        if ( _pnfBoxSize.HasValue || !( _param1 is PnFArg pnFarg ) )
            return;
        _pnfBoxSize = (decimal)( pnFarg.BoxSize.Type == UnitTypes.Percent ? pnFarg.BoxSize.Value : ( pnFarg.BoxSize ) );
        if ( !( _chartSeriesViewModel.RenderSeries is FastXORenderableSeries renderSeries ) )
            return;
        renderSeries.XOBoxSize = (double)_pnfBoxSize.Value;
    }



    private Type GetRenderingSeriesByType()
    {
        switch ( ChartComponentView.DrawStyle )
        {
            case ChartCandleDrawStyles.CandleStick:
                return typeof(FastCandlestickRenderableSeries);

            case ChartCandleDrawStyles.Ohlc:
                return typeof(FastOhlcRenderableSeries);

            case ChartCandleDrawStyles.LineOpen:
            case ChartCandleDrawStyles.LineHigh:
            case ChartCandleDrawStyles.LineLow:
            case ChartCandleDrawStyles.LineClose:
                return typeof(FastLineRenderableSeries);

            //case ChartCandleDrawStyles.BoxVolume:
            //    return typeof(BoxVolumeRenderableSeries);

            //case ChartCandleDrawStyles.ClusterProfile:
            //    return typeof(ClusterProfileRenderableSeries);

            case ChartCandleDrawStyles.Area:
                return typeof(FastMountainRenderableSeries);

            case ChartCandleDrawStyles.PnF:
                return typeof(FastXORenderableSeries);

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected override void RootElementPropertyChanged(IChartComponent _param1, string _param2)
    {
        base.RootElementPropertyChanged(_param1, _param2);
        if ( !( _param2 == "DrawStyle" ) )
            return;
        GuiInitSeries();
    }

    private BaseRenderableSeries CreateRenderableSeries()
    {
        BaseRenderableSeries target;
        switch ( ChartComponentView.DrawStyle )
        {
            case ChartCandleDrawStyles.CandleStick:
                {
                    target = (BaseRenderableSeries)CreateRenderableSeries<FastCandlestickRenderableSeries>(new ChartElementViewModel[4]
                             {
                                      _openViewModel,
                                      _highViewModel,
                                      _lowViewModel,
                                      _closeViewModel
                             });

                    Ecng.Xaml.XamlHelper.SetBindings(target, FastCandlestickRenderableSeries.FillUpProperty, (object)ChartComponentView, "UpFillColor", converter: (IValueConverter)new Ecng.Xaml.Converters.ColorToBrushConverter());
                    Ecng.Xaml.XamlHelper.SetBindings(target, FastCandlestickRenderableSeries.FillDownProperty, (object)ChartComponentView, "DownFillColor", converter: (IValueConverter)new Ecng.Xaml.Converters.ColorToBrushConverter());
                    Ecng.Xaml.XamlHelper.SetBindings(target, FastCandlestickRenderableSeries.StrokeUpProperty, (object)ChartComponentView, "UpBorderColor");
                    Ecng.Xaml.XamlHelper.SetBindings(target, FastCandlestickRenderableSeries.StrokeDownProperty, (object)ChartComponentView, "DownBorderColor");
                    Ecng.Xaml.XamlHelper.SetBindings(target, BaseRenderableSeries.StrokeThicknessProperty, (object)ChartComponentView, "StrokeThickness");
                    target.PaletteProvider = this;
                }             
                break;

            case ChartCandleDrawStyles.Ohlc:
                {
                    target = (BaseRenderableSeries)CreateRenderableSeries<FastOhlcRenderableSeries>(new ChartElementViewModel[4]
                            {
                                  _openViewModel,
                                  _highViewModel,
                                  _lowViewModel,
                                  _closeViewModel
                            });
                    Ecng.Xaml.XamlHelper.SetBindings(target, FastOhlcRenderableSeries.StrokeUpProperty, (object)ChartComponentView, "UpBorderColor");
                    Ecng.Xaml.XamlHelper.SetBindings(target, FastOhlcRenderableSeries.StrokeDownProperty, (object)ChartComponentView, "DownBorderColor");
                    Ecng.Xaml.XamlHelper.SetBindings(target, FastOhlcRenderableSeries.DataPointWidthProperty, (object)ChartComponentView, "StrokeThickness", converter: (IValueConverter)new StrokeThickToPointWidthConverter());
                    BindingOperations.ClearBinding((DependencyObject)target, BaseRenderableSeries.StrokeThicknessProperty);
                    target.PaletteProvider = this;
                }                
                break;
            case ChartCandleDrawStyles.LineOpen:
            case ChartCandleDrawStyles.LineHigh:
            case ChartCandleDrawStyles.LineLow:
            case ChartCandleDrawStyles.LineClose:
                target = (BaseRenderableSeries)CreateRenderableSeries<FastLineRenderableSeries>(new ChartElementViewModel[1]
                {
                    _lineViewModel
                });

                Ecng.Xaml.XamlHelper.SetBindings(target, BaseRenderableSeries.StrokeProperty, (object)_candleHelper, "LineColor");
                Ecng.Xaml.XamlHelper.SetBindings(target, BaseRenderableSeries.StrokeThicknessProperty, (object)ChartComponentView, "StrokeThickness");
                Ecng.Xaml.XamlHelper.SetBindings(target, FastLineRenderableSeries.OhlcDrawModeProperty, (object)ChartComponentView, "DrawStyle", converter: (IValueConverter)new \u0023\u003DzdkTsoRIhz16dAJ0Ha_QZUm_WprFxHE3GMkMYTmM5Gv6xPE1lfu86yUSurnkA());
                break;

            case ChartCandleDrawStyles.BoxVolume:
                target = (BaseRenderableSeries)CreateRenderableSeries<BoxVolumeRenderableSeries>(new ChartElementViewModel[1]
                {
                    _volumeViewModel
                });
                Ecng.Xaml.XamlHelper.SetBindings(target, BoxVolumeRenderableSeries.Timeframe2ColorProperty, (object)_candleHelper, "Timeframe2Color");
                Ecng.Xaml.XamlHelper.SetBindings(target, BoxVolumeRenderableSeries.Timeframe2FrameColorProperty, (object)_candleHelper, "Timeframe2FrameColor");
                Ecng.Xaml.XamlHelper.SetBindings(target, BoxVolumeRenderableSeries.Timeframe3ColorProperty, (object)_candleHelper, "Timeframe3Color");
                Ecng.Xaml.XamlHelper.SetBindings(target, BoxVolumeRenderableSeries.CellFontColorProperty, (object)_candleHelper, "FontColor");
                Ecng.Xaml.XamlHelper.SetBindings(target, BoxVolumeRenderableSeries.HighVolColorProperty, (object)_candleHelper, "MaxVolumeColor");
                Ecng.Xaml.XamlHelper.SetBindings(target, BoxVolumeRenderableSeries.\u0023\u003DzeOkHtL891mol_E40\u0024w\u003D\u003D, (object)_candleHelper, "MaxVolumeBackground");
                Ecng.Xaml.XamlHelper.SetBindings(target, BoxVolumeRenderableSeries.\u0023\u003DzY\u0024hEpvHX6SG5MENm\u0024w\u003D\u003D, (object)ChartComponentView, "Timeframe2Multiplier", BindingMode.OneWay, (IValueConverter)new ChartCandleElementViewModel.\u0023\u003DzBaQAMfOA0Xrd(CandlesTimeframe));
                Ecng.Xaml.XamlHelper.SetBindings(target, BoxVolumeRenderableSeries.\u0023\u003DzsXfDaxv8JwjzCERgkg\u003D\u003D, (object)ChartComponentView, "Timeframe3Multiplier", BindingMode.OneWay, (IValueConverter)new ChartCandleElementViewModel.\u0023\u003DzBaQAMfOA0Xrd(CandlesTimeframe));
                break;
            case ChartCandleDrawStyles.ClusterProfile:
                target = (BaseRenderableSeries)CreateRenderableSeries<ClusterProfileRenderableSeries>(new ChartElementViewModel[1]
                {
                    _volumeViewModel
                });
                Ecng.Xaml.XamlHelper.SetBindings(target, ClusterProfileRenderableSeries.SeparatorLineColorProperty, (object)_candleHelper, "ClusterSeparatorLineColor");
                Ecng.Xaml.XamlHelper.SetBindings(target, ClusterProfileRenderableSeries.LineColorProperty, (object)_candleHelper, "ClusterLineColor");
                Ecng.Xaml.XamlHelper.SetBindings(target, ClusterProfileRenderableSeries.TextColorProperty, (object)_candleHelper, "ClusterTextColor");
                Ecng.Xaml.XamlHelper.SetBindings(target, ClusterProfileRenderableSeries.ClusterColorProperty, (object)_candleHelper, "ClusterColor");
                Ecng.Xaml.XamlHelper.SetBindings(target, ClusterProfileRenderableSeries.ClusterMaxColorProperty, (object)_candleHelper, "ClusterMaxColor");
                break;
            case ChartCandleDrawStyles.Area:
                target = (BaseRenderableSeries)CreateRenderableSeries<FastMountainRenderableSeries>(new ChartElementViewModel[1]
                {
                    _lineViewModel
                });
                target.SeriesColor = Colors.Transparent;
                Ecng.Xaml.XamlHelper.SetBindings(target, BaseRenderableSeries.StrokeProperty, (object)_candleHelper, "LineColor");
                Ecng.Xaml.XamlHelper.SetBindings(target, BaseRenderableSeries.StrokeThicknessProperty, (object)ChartComponentView, "StrokeThickness");
                Ecng.Xaml.XamlHelper.SetBindings(target, BaseMountainRenderableSeries.\u0023\u003DzXc9apgJiH9mm, (object)_candleHelper, "AreaColor", converter: (IValueConverter)new Ecng.Xaml.Converters.ColorToBrushConverter());
                break;
            case ChartCandleDrawStyles.PnF:
                target = (BaseRenderableSeries)CreateRenderableSeries<FastXORenderableSeries>(new ChartElementViewModel[4]
                {
          _openViewModel,
          _highViewModel,
          _lowViewModel,
          _closeViewModel
                });
                Ecng.Xaml.XamlHelper.SetBindings(target, FastOhlcRenderableSeries.StrokeUpProperty, (object)ChartComponentView, "UpBorderColor");
                Ecng.Xaml.XamlHelper.SetBindings(target, FastOhlcRenderableSeries.StrokeDownProperty, (object)ChartComponentView, "DownBorderColor");
                Ecng.Xaml.XamlHelper.SetBindings(target, BaseRenderableSeries.StrokeThicknessProperty, (object)ChartComponentView, "StrokeThickness");
                Decimal? zfIlpmP5Jfeem = _pnfBoxSize;
                if ( zfIlpmP5Jfeem.HasValue )
                {
                    Decimal valueOrDefault = zfIlpmP5Jfeem.GetValueOrDefault();
                    ( (FastXORenderableSeries)target ).XOBoxSize = (double)valueOrDefault;
                }
                target.PaletteProvider = this;
                break;
            default:
                throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.UnsupportedType, new object[1]
                {
          (object) ChartComponentView.DrawStyle
                }));
        }
        if ( ChartComponentView.DrawStyle.IsVolumeProfileChart() )
        {
            Ecng.Xaml.XamlHelper.SetBindings(target, Control.FontFamilyProperty, (object)ChartComponentView, "FontFamily", converter: (IValueConverter)new FontFamilyValueConverter());
            Ecng.Xaml.XamlHelper.SetBindings(target, Control.FontSizeProperty, (object)ChartComponentView, "FontSize", converter: (IValueConverter)new TypeCastConverter());
            Ecng.Xaml.XamlHelper.SetBindings(target, Control.FontWeightProperty, (object)ChartComponentView, "FontWeight");
            Ecng.Xaml.XamlHelper.SetBindings(target, TimeframeSegmentRenderableSeries.PriceStepProperty, (object)ChartComponentView, "PriceStep");
            Ecng.Xaml.XamlHelper.SetBindings(target, TimeframeSegmentRenderableSeries.ShowHorizontalVolumesProperty, (object)ChartComponentView, "ShowHorizontalVolumes");
            Ecng.Xaml.XamlHelper.SetBindings(target, TimeframeSegmentRenderableSeries.LocalHorizontalVolumesProperty, (object)ChartComponentView, "LocalHorizontalVolumes");
            Ecng.Xaml.XamlHelper.SetBindings(target, TimeframeSegmentRenderableSeries.HorizontalVolumeWidthFractionProperty, (object)ChartComponentView, "HorizontalVolumeWidthFraction");
            Ecng.Xaml.XamlHelper.SetBindings(target, TimeframeSegmentRenderableSeries.VolumeBarsBrushProperty, (object)_candleHelper, "HorizontalVolumeColor", BindingMode.OneWay, (IValueConverter)new Ecng.Xaml.Converters.ColorToBrushConverter());
            Ecng.Xaml.XamlHelper.SetBindings(target, TimeframeSegmentRenderableSeries.VolBarsFontColorProperty, (object)_candleHelper, "HorizontalVolumeFontColor");
            Ecng.Xaml.XamlHelper.SetBindings(target, TimeframeSegmentRenderableSeries.DrawSeparateVolumesProperty, (object)ChartComponentView, "DrawSeparateVolumes");
            Ecng.Xaml.XamlHelper.SetBindings(target, TimeframeSegmentRenderableSeries.BuyColorProperty, (object)_candleHelper, "BuyColor");
            Ecng.Xaml.XamlHelper.SetBindings(target, TimeframeSegmentRenderableSeries.SellColorProperty, (object)_candleHelper, "SellColor");
            Ecng.Xaml.XamlHelper.SetBindings(target, TimeframeSegmentRenderableSeries.UpColorProperty, (object)_candleHelper, "UpColor");
            Ecng.Xaml.XamlHelper.SetBindings(target, TimeframeSegmentRenderableSeries.DownColorProperty, (object)_candleHelper, "DownColor");
        }
        return target;
    }

    protected override void Clear()
    {
        RemoveChartSeries();
        _candleHelper.Dispose();
    }

    protected override void UpdateUi()
    {
        OhlcSeries.Clear();
        OhlcSeries.Timeframe = new TimeSpan?();
        _xyDataSeries.Clear();
        _timeframeSegmentDataSeries = (TimeframeSegmentDataSeries)null;
        _dateTimeUtc = new DateTime();
        _pnfBoxSize = new Decimal?();
        _dateTime2ColorMap.Clear();
        PerformUiAction(new Action(\u0023\u003Dzp5J6G301TJ9o31R2wgChW2E\u003D), true);
    }

    private void NewChartSeries()
    {
        _chartSeriesViewModel = new ChartSeriesViewModel(GetDataSeriesByDrawStyle(), (IRenderableSeries)CreateRenderableSeries());
        ScichartSurfaceMVVM.AddSeriesViewModelsToRoot(RootElem, (IRenderableSeries)_chartSeriesViewModel);
        ClearAll();
        SetupAxisMarkerAndBinding(_chartSeriesViewModel.RenderSeries, (IChartComponent)ChartComponentView, "ShowAxisMarker", (string)null);
    }



    public override void PerformPeriodicalAction()
    {
        base.PerformPeriodicalAction();
        VisibleRangeDpo xAxisVisibleRange = ScichartSurfaceMVVM.GetVisibleRangeDpo(ChartComponentView.XAxisId);
        if ( xAxisVisibleRange == null )
            return;
        IndexRange categoryDateTimeRange = xAxisVisibleRange.CategoryDateTimeRange;
        int count = OhlcSeries.Count;
        bool flag1 = count > 0 && categoryDateTimeRange.IsDefined && !ScichartSurfaceMVVM.Chart.IsAutoRange;
        bool flag2 = flag1 && ScichartSurfaceMVVM.Chart.IsAutoScroll;
        if ( !_boolean02 & flag1 )
        {
            _boolean02 = true;
            if ( !flag2 )
            {
                IndexRange g8Oq2rGx6KyfAreq = new IndexRange(0, categoryDateTimeRange.Max - categoryDateTimeRange.Min);
                xAxisVisibleRange.CategoryDateTimeRange = g8Oq2rGx6KyfAreq;
            }
        }
        if ( !flag2 )
        {
            _boolean01 = new bool?();
            _indexRange = (IndexRange)null;
        }
        else
        {
            int num1;
            if ( _boolean01.HasValue && ( !_boolean01.GetValueOrDefault() || _indexRange != categoryDateTimeRange ) )
            {
                bool? zuuCmXeHwuMy = _boolean01;
                num1 = !( !zuuCmXeHwuMy.GetValueOrDefault() & zuuCmXeHwuMy.HasValue ) ? 0 : ( categoryDateTimeRange.Max >= count ? 1 : 0 );
            }
            else
                num1 = 1;
            bool flag3 = num1 != 0;
            int num2 = count;
            if ( flag3 && ( categoryDateTimeRange.Max < num2 || !_boolean01.HasValue ) )
            {
                int num3 = categoryDateTimeRange.Max - categoryDateTimeRange.Min + 1;
                IndexRange g8Oq2rGx6KyfAreq = new IndexRange(num2 - num3 + 1, num2);
                xAxisVisibleRange.CategoryDateTimeRange = g8Oq2rGx6KyfAreq;
                _indexRange = g8Oq2rGx6KyfAreq;
            }
            else
                _indexRange = categoryDateTimeRange;
            _boolean01 = new bool?(flag3);
        }
    }

    public override bool Draw(IEnumerableEx<ChartDrawData.IDrawValue> _param1)
    {
        switch ( _param1 != null ? ( (IEnumerable<ChartDrawData.IDrawValue>)_param1 ).FirstOrDefault<ChartDrawData.IDrawValue>() : (ChartDrawData.IDrawValue)null )
        {
            case null:
                return false;
            case ChartDrawData.sCandle _:
                return Draw(CollectionHelper.ToEx<ChartDrawData.sCandle>(( (IEnumerable)_param1 ).Cast<ChartDrawData.sCandle>(), ( (IEnumerableEx)_param1 ).Count));
            case ChartDrawData.sCandleColor _:
                return Draw(CollectionHelper.ToEx<ChartDrawData.sCandleColor>(( (IEnumerable)_param1 ).Cast<ChartDrawData.sCandleColor>(), ( (IEnumerableEx)_param1 ).Count));
            default:
                throw new ArgumentOutOfRangeException("values");
        }
    }

    private bool Draw(IEnumerableEx<ChartDrawData.sCandleColor> candleColors)
    {
        if ( CollectionHelper.IsEmpty<ChartDrawData.sCandleColor>((IEnumerable<ChartDrawData.sCandleColor>)candleColors) )
            return false;
        foreach ( ChartDrawData.sCandleColor candleColor in (IEnumerable<ChartDrawData.sCandleColor>)candleColors )
        {
            Color? color1 = candleColor.Color;
            if ( color1.HasValue )
            {
                SynchronizedDictionary<DateTime, Color> zK1tfXeY7PpNb = _dateTime2ColorMap;
                DateTime dateTime = candleColor.Time;
                color1 = candleColor.Color;
                Color color2 = color1.Value;
                zK1tfXeY7PpNb[dateTime] = color2;
            }
            else
                _dateTime2ColorMap.Remove(candleColor.Time);
        }
        _chartSeriesViewModel?.RenderSeries.Services?.GetService<ISciChartSurface>()?.InvalidateElement();
        return true;
    }

    private bool Draw(IEnumerableEx<ChartDrawData.sCandle> candles)
    {
        if ( _colorerFunction != ChartComponentView.Colorer )
        {
            _colorerFunction = ChartComponentView.Colorer;
            _chartSeriesViewModel?.RenderSeries.Services?.GetService<ISciChartSurface>()?.InvalidateElement();
        }
        if ( candles == null || CollectionHelper.IsEmpty<ChartDrawData.sCandle>(candles) )
            return false;

        int count = ( (IEnumerableEx)candles ).Count;
        DateTime dateTime = _dateTimeUtc;
        int index = -1;
        bool flag = false;
        DateTime[] timeArray = new DateTime[count];
        double[] openArray = new double[count];
        double[] highArray = new double[count];
        double[] lowArray = new double[count];
        double[] closeArray = new double[count];
        foreach ( ChartDrawData.sCandle bar in (IEnumerable<ChartDrawData.sCandle>)candles )
        {
            object tf = bar.CandleArg.Arg;
            if ( tf is TimeSpan timeSpan )
                OhlcSeries.Timeframe = new TimeSpan?(timeSpan);
            SetPnfBoxSize(tf);

            InternalGuiInitSeries();
            switch ( bar.Time.CompareTo(dateTime) )
            {
                case -1:
                    throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.CannotChangeCandleValue, new object[2]
                    {
            (object) bar.Time,
            (object) dateTime
                    }));
                case 0:
                    flag = true;
                    OhlcSeries.UpdateOrderAdornerLayer(bar.Time, bar.\u0023\u003DzGze4a8XU7KvB(), bar.\u0023\u003DzolXXlhDBER_c(), bar.\u0023\u003DzchuwVU\u00245sIH8(), bar.Close);
                    _xyDataSeries.UpdateOrderAdornerLayer(bar.Time, bar.Close);
                    if ( bar.\u0023\u003Dzeu8tE1P9bfD8() != null && _timeframeSegmentDataSeries != null)
          {
                        foreach ( CandlePriceLevel level in bar.\u0023\u003Dzeu8tE1P9bfD8())
              _timeframeSegmentDataSeries.Update(bar.Time, (double) ((CandlePriceLevel) ref level).Price, level);
          }
          --count;
          break;
        default:
          ++index;
          timeArray[index] = bar.Time;
          openArray[index] = bar.\u0023\u003DzGze4a8XU7KvB();
          highArray[index] = bar.\u0023\u003DzolXXlhDBER_c();
          lowArray [index] = bar.\u0023\u003DzchuwVU\u00245sIH8();
          closeArray[index] = bar.Close;
          if (bar.\u0023\u003Dzeu8tE1P9bfD8() != null && _timeframeSegmentDataSeries != null)
          {
            foreach (CandlePriceLevel level in bar.\u0023\u003Dzeu8tE1P9bfD8())
              _timeframeSegmentDataSeries.Append(bar.Time, (double) ((CandlePriceLevel) ref level).Price, level);
            break;
          }
          break;
      }
      dateTime = bar.Time;
    }
    if (count == 0)
      return flag;
    Array.Resize<DateTime>(ref timeArray, count);
    Array.Resize<double>(ref openArray, count);
    Array.Resize<double>(ref highArray, count);
    Array.Resize<double>(ref lowArray , count);
    Array.Resize<double>(ref closeArray, count);
    OhlcSeries.\u0023\u003Dznc8esWY\u003D((IEnumerable<DateTime>) timeArray, (IEnumerable<double>) openArray, (IEnumerable<double>) highArray, (IEnumerable<double>) lowArray , (IEnumerable<double>) closeArray);
    _xyDataSeries.\u0023\u003Dznc8esWY\u003D((IEnumerable<DateTime>) timeArray, (IEnumerable<double>) closeArray);
    _dateTimeUtc = dateTime;
    return true;
  }

  Color? IXxxPaletteProvider.\u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC3yzxavQxmzggPmZ8V62OI0Kr0hq2Km30eq101sCk(
    IRenderableSeries _param1,
    double _param2,
    double _param3)
  {
    return new Color?();
  }

  Color? IXxxPaletteProvider.\u0023\u003DzaP7vgnwtOd1ghQwnj\u00248jG1HXnj3oIOVYNqLIS92dT0MqcEWOD7IYv5ohC5gQ(
    IRenderableSeries _param1,
    double _param2,
    double _param3,
    double _param4)
  {
    return new Color?();
  }

  Color? IXxxPaletteProvider.\u0023\u003DzaP7vgnwtOd1ghQwnj\u00248jG1HXnj3oIOVYNqLIS92dT0MqcEWOD7IYv5ohC5gQ(
    IRenderableSeries _param1,
    double _param2,
    double _param3,
    double _param4,
    double _param5,
    double _param6)
  {
    int index = (int) _param2;
    DateTime xvalue = OhlcSeries.XValues[index];
    foreach (CandlePatternElementViewModel rri1f09FsCgNu6tg in (BaseCollection<CandlePatternElementViewModel, List<CandlePatternElementViewModel>>) _chartPatternList)
    {
      Color? nullable = rri1f09FsCgNu6tg.GetCandleColor(xvalue, _param6 > _param3);
      if (nullable.HasValue)
        return nullable;
    }
    Color color;
    if (_dateTime2ColorMap.TryGetValue(xvalue, ref color))
      return new Color?(color);
    Func<DateTimeOffset, bool, bool, Color?> qjskTkReJlfF5mAk = _colorerFunction;
    return qjskTkReJlfF5mAk == null ? new Color?() : qjskTkReJlfF5mAk(TimeHelper.ToDateTimeOffset(xvalue, TimeZoneInfo.Utc), _param6 >= _param3, index == OhlcSeries.Count - 1);
  }

  
  

  

  private void \u0023\u003Dzp5J6G301TJ9o31R2wgChW2E\u003D()
  {
    ClearAll();
    RemoveChartSeries();
    NewChartSeries();
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly ChartCandleElementViewModel.SomeClass34343383 SomeMethond0343 = new ChartCandleElementViewModel.SomeClass34343383();
    public static Func<SeriesInfo, string> \u0023\u003DzU9srAoETJDIIA3EbGw\u003D\u003D;
    public static Func<SeriesInfo, string> \u0023\u003Dzv7x3aq5xjFNkgWHCPg\u003D\u003D;
    public static Func<SeriesInfo, string> \u0023\u003Dz3QgfRT\u0024GTbbdeFCZLg\u003D\u003D;
    public static Func<SeriesInfo, string> \u0023\u003Dz3XWQ58Tacl3uxVgGiw\u003D\u003D;
    public static Func<SeriesInfo, string> \u0023\u003DztKkeF19DCI_S9dyd\u0024A\u003D\u003D;

    public string Method01(
      SeriesInfo _param1)
    {
      return !(_param1 is OhlcSeriesInfo vo1e0c8c41pWqbDkntdB13Yg) ? (string) null : vo1e0c8c41pWqbDkntdB13Yg.FormattedOpenValue;
    }

    public string \u0023\u003DzyboOJPrkbYbHrtK_jYxCKz0\u003D(
      SeriesInfo _param1)
    {
      return !(_param1 is OhlcSeriesInfo vo1e0c8c41pWqbDkntdB13Yg) ? (string) null : vo1e0c8c41pWqbDkntdB13Yg.FormattedHighValue;
    }

    public string \u0023\u003Dz_F9udS\u0024bvXmu7p7kNM2l1tM\u003D(
      SeriesInfo _param1)
    {
      return !(_param1 is OhlcSeriesInfo vo1e0c8c41pWqbDkntdB13Yg) ? (string) null : vo1e0c8c41pWqbDkntdB13Yg.FormattedLowValue;
    }

    public string \u0023\u003Dz_2ZdaKSEm\u0024dowilMRXDN0r4\u003D(
      SeriesInfo _param1)
    {
      return !(_param1 is OhlcSeriesInfo vo1e0c8c41pWqbDkntdB13Yg) ? (string) null : vo1e0c8c41pWqbDkntdB13Yg.FormattedCloseValue;
    }

    public string \u0023\u003Dzu0cWRPhAEXPa2F76HfbtuFw\u003D(
      SeriesInfo _param1)
    {
      if (!(_param1 is \u0023\u003DzGULZ_B3lGVEDiq9xPbVQjsPdCs3fSNYVEdhm_bS76Lhc cs3fSnyvEdhmBS76Lhc))
        return string.Empty;
      string str = string.Empty;
      if (cs3fSnyvEdhmBS76Lhc.XValue != null)
        str = $"O:{cs3fSnyvEdhmBS76Lhc.FormattedOpenValue} H:{cs3fSnyvEdhmBS76Lhc.FormattedHighValue} L:{cs3fSnyvEdhmBS76Lhc.FormattedLowValue} C:{cs3fSnyvEdhmBS76Lhc.FormattedCloseValue}";
      return $"{str}   {cs3fSnyvEdhmBS76Lhc.Level}";
    }
  }

  private sealed class \u0023\u003DzBaQAMfOA0Xrd(TimeSpan? _param1) : IValueConverter
  {
    
    private readonly TimeSpan? \u0023\u003DzIijA5WEGKapy9Yb82g\u003D\u003D = _param1;

    object IValueConverter.Convert(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      return (object) (!\u0023\u003DzIijA5WEGKapy9Yb82g\u003D\u003D.HasValue || !(_param1 is int num) ? new TimeSpan?() : new TimeSpan?(TimeSpan.FromTicks((long) num * \u0023\u003DzIijA5WEGKapy9Yb82g\u003D\u003D.Value.Ticks)));
    }

    object IValueConverter.ConvertBack(
      object _param1,
      Type _param2,
      object _param3,
      CultureInfo _param4)
    {
      throw new NotSupportedException();
    }
  }

  
}


