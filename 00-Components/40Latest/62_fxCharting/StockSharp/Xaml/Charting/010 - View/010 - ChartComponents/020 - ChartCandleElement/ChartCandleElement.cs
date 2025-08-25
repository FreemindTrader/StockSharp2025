using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>
/// This class contains the code for the CandleStick UI.
/// </summary>
[Display(ResourceType = typeof(LocalizedStrings), Name = "CandleSettings")]
public class ChartCandleElement : ChartComponentView<ChartCandleElement>,
                                  IChartElement,
                                  IChartPart<IChartElement>,
                                  INotifyPropertyChanged,
                                  IChartCandleElement,
                                  INotifyPropertyChanging,
                                  IPersistable,
                                  IChartComponent,
                                  IDrawableChartElement
{

    private ChartCandleDrawStyles _drawStyle;

    private System.Windows.Media.Color _downFillColor;

    private System.Windows.Media.Color _upFillColor;

    private System.Windows.Media.Color _downBorderColor;

    private System.Windows.Media.Color _upBorderColor;

    private int _strokeThickness;

    private bool _antiAliasing;

    private System.Windows.Media.Color? _lineColor;

    private System.Windows.Media.Color? _areaColor;

    private bool _showAxisMarker = true;

    private Decimal? _priceStep;

    private Func<DateTimeOffset, bool, bool, System.Windows.Media.Color?> _colorer;

    private int? _timeframe2Multiplier;

    private int? _timeframe3Multiplier = new int?(15);

    private System.Windows.Media.Color? _fontColor;

    private System.Windows.Media.Color? _timeframe2Color;

    private System.Windows.Media.Color? _timeframe2FrameColor;

    private System.Windows.Media.Color? _timeframe3Color;

    private System.Windows.Media.Color? _maxVolumeColor;

    private System.Windows.Media.Color? _maxVolumeBackground;

    private System.Windows.Media.Color? _clusterLineColor;

    private System.Windows.Media.Color? _clusterSeparatorLineColor;

    private System.Windows.Media.Color? _clusterTextColor;

    private System.Windows.Media.Color? _clusterColor;

    private System.Windows.Media.Color? _clusterMaxColor;

    private bool _showHorizontalVolumes = true;

    private bool _localHorizontalVolumes = true;

    private double _horizontalVolumeWidthFraction = 0.15;

    private System.Windows.Media.Color? _horizontalVolumeColor;

    private System.Windows.Media.Color? _horizontalVolumeFontColor;

    private string _fontFamily = "Segoe UI";

    private Decimal _fontSize = 14M;

    private FontWeight _fontWeight = FontWeights.Bold;

    private bool _drawSeparateVolumes = true;

    private System.Windows.Media.Color? _buyColor;

    private System.Windows.Media.Color? _sellColor;

    private System.Windows.Media.Color? _upColor;

    private System.Windows.Media.Color? _downColor;

    private Func<DateTimeOffset, bool, bool, System.Drawing.Color?> _drawingColor;

    private DrawableChartComponentBaseViewModel _baseViewModel;

    public ChartCandleElement()
    {
        DownFillColor = DownBorderColor = Colors.Red;
        UpFillColor = UpBorderColor = Colors.Green;
        StrokeThickness = 1;
        DrawStyle = ChartCandleDrawStyles.CandleStick;
    }

    System.Windows.Media.Color IDrawableChartElement.Color
    {
        get
        {
            return Colors.Transparent;
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Style", Description = "StyleCandlesRender", GroupName = "Style", Order = 25)]    

    public ChartCandleDrawStyles DrawStyle
    {
        get => _drawStyle;
        set
        {
            if ( _drawStyle == value )
                return;
            RaisePropertyValueChanging(nameof(DrawStyle), (object)value);
            if ( _drawStyle == ChartCandleDrawStyles.PnF )
                AntiAliasing = false;
            switch ( value )
            {
                case ChartCandleDrawStyles.CandleStick:
                case ChartCandleDrawStyles.LineOpen:
                case ChartCandleDrawStyles.LineHigh:
                case ChartCandleDrawStyles.LineLow:
                case ChartCandleDrawStyles.LineClose:
                case ChartCandleDrawStyles.Area:
                    if ( _drawStyle == ChartCandleDrawStyles.Ohlc )
                    {
                        StrokeThickness = 1;
                        break;
                    }
                    break;
                case ChartCandleDrawStyles.Ohlc:
                    StrokeThickness = 8;
                    break;
                case ChartCandleDrawStyles.BoxVolume:
                case ChartCandleDrawStyles.ClusterProfile:
                    StrokeThickness = 1;
                    break;
                case ChartCandleDrawStyles.PnF:
                    StrokeThickness = 1;
                    AntiAliasing = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), (object)value, LocalizedStrings.InvalidValue);
            }
            _drawStyle = value;
            RaisePropertyChanged(nameof(DrawStyle));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Decrease", Description = "ColorOfDecreaseCandle", GroupName = "Style", Order = 30)]
    public System.Windows.Media.Color DownFillColor
    {
        get => _downFillColor;
        set
        {
            if ( _downFillColor == value )
                return;
            _downFillColor = value;
            RaisePropertyChanged(nameof(DownFillColor));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Increase", Description = "ColorOfIncreaseCandle", GroupName = "Style", Order = 32 /*0x20*/)]
    public System.Windows.Media.Color UpFillColor
    {
        get => _upFillColor;
        set
        {
            if ( _upFillColor == value )
                return;
            _upFillColor = value;
            RaisePropertyChanged(nameof(UpFillColor));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "DecreaseBorder", Description = "DecreaseBorderDesc", GroupName = "Style", Order = 31 /*0x1F*/)]
    public System.Windows.Media.Color DownBorderColor
    {
        get => _downBorderColor;
        set
        {
            if ( _downBorderColor == value )
                return;
            _downBorderColor = value;
            RaisePropertyChanged(nameof(DownBorderColor));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "IncreaseBorder", Description = "IncreaseBorderDesc", GroupName = "Style", Order = 33)]
    public System.Windows.Media.Color UpBorderColor
    {
        get => _upBorderColor;
        set
        {
            if ( _upBorderColor == value )
                return;
            _upBorderColor = value;
            RaisePropertyChanged(nameof(UpBorderColor));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "LineWidth", Description = "LineWidthDesc", GroupName = "Style", Order = 40)]
    public int StrokeThickness
    {
        get => _strokeThickness;
        set
        {
            if ( _strokeThickness == value )
                return;
            _strokeThickness = value;
            if ( _strokeThickness < 1 || _strokeThickness > 10 )
                throw new ArgumentOutOfRangeException(nameof(value), (object)value, LocalizedStrings.InvalidValue);
            RaisePropertyChanged(nameof(StrokeThickness));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "AntiAliasing", Description = "CandlesRenderAntiAliasing", GroupName = "Style", Order = 40)]
    public bool AntiAliasing
    {
        get => _antiAliasing;
        set
        {
            if ( _antiAliasing == value )
                return;
            _antiAliasing = value;
            RaisePropertyChanged(nameof(AntiAliasing));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "LineColor", Description = "LineColorDot", GroupName = "Style", Order = 46)]
    public System.Windows.Media.Color? LineColor
    {
        get => _lineColor;
        set
        {
            System.Windows.Media.Color? zgRuR77srSeQq = _lineColor;
            System.Windows.Media.Color? nullable = value;
            if ( ( zgRuR77srSeQq.HasValue == nullable.HasValue ? ( zgRuR77srSeQq.HasValue ? ( zgRuR77srSeQq.GetValueOrDefault() == nullable.GetValueOrDefault() ? 1 : 0 ) : 1 ) : 0 ) != 0 )
                return;
            _lineColor = value;
            RaisePropertyChanged(nameof(LineColor));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "AreaColor", Description = "AreaColorDot", GroupName = "Style", Order = 47)]
    public System.Windows.Media.Color? AreaColor
    {
        get => _areaColor;
        set
        {
            System.Windows.Media.Color? z1qvt9yuVxTg7 = _areaColor;
            System.Windows.Media.Color? nullable = value;
            if ( ( z1qvt9yuVxTg7.HasValue == nullable.HasValue ? ( z1qvt9yuVxTg7.HasValue ? ( z1qvt9yuVxTg7.GetValueOrDefault() == nullable.GetValueOrDefault() ? 1 : 0 ) : 1 ) : 0 ) != 0 )
                return;
            _areaColor = value;
            RaisePropertyChanged(nameof(AreaColor));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Marker", Description = "ShowAxisMarker", GroupName = "Style", Order = 50)]
    public bool ShowAxisMarker
    {
        get => _showAxisMarker;
        set
        {
            if ( _showAxisMarker == value )
                return;
            _showAxisMarker = value;
            RaisePropertyChanged(nameof(ShowAxisMarker));
        }
    }

    [DataMember]
    [Display(ResourceType = typeof(LocalizedStrings), Name = "PriceStep", Description = "MinPriceStep", GroupName = "Common", Order = 1004)]
    public Decimal? PriceStep
    {
        get => _priceStep;
        set
        {
            Decimal? kpsOhjyC4ZcEekGzw = _priceStep;
            Decimal? nullable1 = value;
            if ( kpsOhjyC4ZcEekGzw.GetValueOrDefault() == nullable1.GetValueOrDefault() & kpsOhjyC4ZcEekGzw.HasValue == nullable1.HasValue )
                return;
            Decimal? nullable2 = value;
            Decimal num = 0M;
            if ( nullable2.GetValueOrDefault() <= num & nullable2.HasValue )
                throw new ArgumentOutOfRangeException(nameof(value), (object)value, LocalizedStrings.InvalidValue);
            _priceStep = value;
            RaisePropertyChanged(nameof(PriceStep));
        }
    }

    [Browsable(false)]
    public Func<DateTimeOffset, bool, bool, System.Windows.Media.Color?> Colorer
    {
        get => _colorer;
        set
        {
            _colorer = value;
            RaisePropertyChanged(nameof(Colorer));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Timeframe2Multiplier", Description = "TimeframeMultiplierDescr", GroupName = "VolumeSettings", Order = 3)]
    public int? Timeframe2Multiplier
    {
        get => _timeframe2Multiplier;
        set
        {            
            if ( _timeframe2Multiplier.GetValueOrDefault() == value.GetValueOrDefault() & _timeframe2Multiplier.HasValue == value.HasValue )
                return;
            
            if ( value.GetValueOrDefault() < 1 & value.HasValue )
                throw new ArgumentOutOfRangeException(nameof(Timeframe2Multiplier));

            SetField<int?>(ref _timeframe2Multiplier, value, nameof(Timeframe2Multiplier));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Timeframe3Multiplier", Description = "TimeframeMultiplierDescr", GroupName = "VolumeSettings", Order = 4)]
    public int? Timeframe3Multiplier
    {
        get => _timeframe3Multiplier;
        set
        {            
            if ( _timeframe3Multiplier.GetValueOrDefault() == value.GetValueOrDefault() & _timeframe3Multiplier.HasValue == value.HasValue )
                return;
            
            if ( value.GetValueOrDefault() < 1 & value.HasValue )
                throw new ArgumentOutOfRangeException(nameof(Timeframe3Multiplier));

            SetField<int?>(ref _timeframe3Multiplier, value, nameof(Timeframe3Multiplier));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "FontColor", Description = "FontColorDot", GroupName = "VolumeSettings", Order = 5)]
    public System.Windows.Media.Color? FontColor
    {
        get => _fontColor;
        set => SetField<System.Windows.Media.Color?>(ref _fontColor, value, nameof(FontColor));
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Timeframe2GridColor", Description = "Timeframe2GridColorDot", GroupName = "VolumeSettings", Order = 6)]
    public System.Windows.Media.Color? Timeframe2Color
    {
        get => _timeframe2Color;
        set
        {
            SetField<System.Windows.Media.Color?>(ref _timeframe2Color, value, nameof(Timeframe2Color));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Timeframe2FrameColor", Description = "Timeframe2FrameColorDot", GroupName = "VolumeSettings", Order = 7)]
    public System.Windows.Media.Color? Timeframe2FrameColor
    {
        get => _timeframe2FrameColor;
        set
        {
            SetField<System.Windows.Media.Color?>(ref _timeframe2FrameColor, value, nameof(Timeframe2FrameColor));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Timeframe3GridColor", Description = "Timeframe3GridColorDot", GroupName = "VolumeSettings", Order = 8)]
    public System.Windows.Media.Color? Timeframe3Color
    {
        get => _timeframe3Color;
        set
        {
            SetField<System.Windows.Media.Color?>(ref _timeframe3Color, value, nameof(Timeframe3Color));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "MaxVolumeColor", Description = "MaxVolumeColorDot", GroupName = "VolumeSettings", Order = 9)]
    public System.Windows.Media.Color? MaxVolumeColor
    {
        get => _maxVolumeColor;
        set
        {
            SetField<System.Windows.Media.Color?>(ref _maxVolumeColor, value, nameof(MaxVolumeColor));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "MaxVolumeBackground", Description = "MaxVolumeBackground", GroupName = "VolumeSettings", Order = 9)]
    public System.Windows.Media.Color? MaxVolumeBackground
    {
        get => _maxVolumeBackground;
        set
        {
            SetField<System.Windows.Media.Color?>(ref _maxVolumeBackground, value, nameof(MaxVolumeBackground));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "ClusterLineColor", Description = "ClusterLineColorDot", GroupName = "VolumeSettings", Order = 10)]
    public System.Windows.Media.Color? ClusterLineColor
    {
        get => _clusterLineColor;
        set
        {
            SetField<System.Windows.Media.Color?>(ref _clusterLineColor, value, nameof(ClusterLineColor));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "ClusterSeparatorLineColor", Description = "ClusterSeparatorLineColorDot", GroupName = "VolumeSettings", Order = 11)]
    public System.Windows.Media.Color? ClusterSeparatorLineColor
    {
        get => _clusterSeparatorLineColor;
        set
        {
            SetField<System.Windows.Media.Color?>(ref _clusterSeparatorLineColor, value, nameof(ClusterSeparatorLineColor));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "ClusterTextColor", Description = "ClusterTextColorDot", GroupName = "VolumeSettings", Order = 12)]
    public System.Windows.Media.Color? ClusterTextColor
    {
        get => _clusterTextColor;
        set
        {
            SetField<System.Windows.Media.Color?>(ref _clusterTextColor, value, nameof(ClusterTextColor));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "ClusterColor", Description = "ClusterColorDot", GroupName = "VolumeSettings", Order = 13)]
    public System.Windows.Media.Color? ClusterColor
    {
        get => _clusterColor;
        set
        {
            SetField<System.Windows.Media.Color?>(ref _clusterColor, value, nameof(ClusterColor));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "ClusterMaxVolumeColor", Description = "ClusterMaxVolumeColorDot", GroupName = "VolumeSettings", Order = 14)]
    public System.Windows.Media.Color? ClusterMaxColor
    {
        get => _clusterMaxColor;
        set
        {
            SetField<System.Windows.Media.Color?>(ref _clusterMaxColor, value, nameof(ClusterMaxColor));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "ShowHorizontalVolumes", Description = "ShowHorizontalVolumesDot", GroupName = "VolumeSettings", Order = 15)]
    public bool ShowHorizontalVolumes
    {
        get => _showHorizontalVolumes;
        set
        {
            SetField<bool>(ref _showHorizontalVolumes, value, nameof(ShowHorizontalVolumes));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "LocalHorizontalVolumes", Description = "LocalHorizontalVolumesDot", GroupName = "VolumeSettings", Order = 16 /*0x10*/)]
    public bool LocalHorizontalVolumes
    {
        get => _localHorizontalVolumes;
        set
        {
            SetField<bool>(ref _localHorizontalVolumes, value, nameof(LocalHorizontalVolumes));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "HorizontalVolumeWidthFraction", Description = "HorizontalVolumeWidthFractionDot", GroupName = "VolumeSettings", Order = 17)]
    public double HorizontalVolumeWidthFraction
    {
        get => _horizontalVolumeWidthFraction;
        set
        {
            if ( _horizontalVolumeWidthFraction == value )
                return;
            if ( value < 0.0 || value > 1.0 )
                throw new ArgumentOutOfRangeException(nameof(HorizontalVolumeWidthFraction));
            SetField<double>(ref _horizontalVolumeWidthFraction, value, nameof(HorizontalVolumeWidthFraction));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "HorizontalVolumeColor", Description = "HorizontalVolumeColorDot", GroupName = "VolumeSettings", Order = 18)]
    public System.Windows.Media.Color? HorizontalVolumeColor
    {
        get => _horizontalVolumeColor;
        set
        {
            SetField<System.Windows.Media.Color?>(ref _horizontalVolumeColor, value, nameof(HorizontalVolumeColor));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "HorizontalVolumeFontColor", Description = "HorizontalVolumeFontColorDot", GroupName = "VolumeSettings", Order = 19)]
    public System.Windows.Media.Color? HorizontalVolumeFontColor
    {
        get => _horizontalVolumeFontColor;
        set
        {
            SetField<System.Windows.Media.Color?>(ref _horizontalVolumeFontColor, value, nameof(HorizontalVolumeFontColor));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "FontFamily", Description = "FontFamily", GroupName = "VolumeSettings", Order = 20)]
    [ItemsSource(typeof(FontFamilyNamesItemsSource))]
    public string FontFamily
    {
        get => _fontFamily;
        set
        {
            ref string local = ref _fontFamily;
            string str = !StringHelper.IsEmpty(value) ? value : throw new ArgumentNullException(nameof(value));
            SetField<string>(ref local, str, nameof(FontFamily));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "FontSize", Description = "FontSize", GroupName = "VolumeSettings", Order = 21)]
    public Decimal FontSize
    {
        get => _fontSize;
        set
        {
            if ( _fontSize == value )
                return;
            if ( value < 7M )
                throw new ArgumentOutOfRangeException(nameof(value), (object)value, LocalizedStrings.InvalidValue);
            SetField<Decimal>(ref _fontSize, value, nameof(FontSize));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "FontWeight", Description = "FontWeight", GroupName = "VolumeSettings", Order = 22)]
    public FontWeight FontWeight
    {
        get => _fontWeight;
        set
        {
            SetField<FontWeight>(ref _fontWeight, value, nameof(FontWeight));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Separate", Description = "DrawSeparateVolumes", GroupName = "VolumeSettings", Order = 23)]
    public bool DrawSeparateVolumes
    {
        get => _drawSeparateVolumes;
        set
        {
            SetField<bool>(ref _drawSeparateVolumes, value, nameof(DrawSeparateVolumes));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Buy", Description = "BuyColor", GroupName = "VolumeSettings", Order = 24)]
    public System.Windows.Media.Color? BuyColor
    {
        get => _buyColor;
        set => SetField<System.Windows.Media.Color?>(ref _buyColor, value, nameof(BuyColor));
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Sell", Description = "SellColor", GroupName = "VolumeSettings", Order = 25)]
    public System.Windows.Media.Color? SellColor
    {
        get => _sellColor;
        set => SetField<System.Windows.Media.Color?>(ref _sellColor, value, nameof(SellColor));
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Up", Description = "UpTrend", GroupName = "VolumeSettings", Order = 26)]
    public System.Windows.Media.Color? UpColor
    {
        get => _upColor;
        set => SetField<System.Windows.Media.Color?>(ref _upColor, value, nameof(UpColor));
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Down", Description = "DownTrend", GroupName = "VolumeSettings", Order = 27)]
    public System.Windows.Media.Color? DownColor
    {
        get => _downColor;
        set => SetField<System.Windows.Media.Color?>(ref _downColor, value, nameof(DownColor));
    }

    System.Drawing.Color IChartCandleElement.DownFillColor
    {
        get => DownFillColor.FromWpf();
        set => DownFillColor = value.ToWpf();
    }

    System.Drawing.Color IChartCandleElement.UpFillColor
    {
        get => UpFillColor.FromWpf();
        set => UpFillColor = value.ToWpf();
    }

    System.Drawing.Color IChartCandleElement.DownBorderColor
    {
        get => DownBorderColor.FromWpf();
        set => DownBorderColor = value.ToWpf();
    }

    System.Drawing.Color IChartCandleElement.UpBorderColor
    {
        get => UpBorderColor.FromWpf();
        set => UpBorderColor = value.ToWpf();
    }

    System.Drawing.Color? IChartCandleElement.LineColor
    {
        get
        {
            System.Windows.Media.Color? lineColor = LineColor;
            ref System.Windows.Media.Color? local = ref lineColor;
            return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
        }
        set
        {
            LineColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
        }
    }

    System.Drawing.Color? IChartCandleElement.AreaColor
    {
        get
        {
            System.Windows.Media.Color? areaColor = AreaColor;
            ref System.Windows.Media.Color? local = ref areaColor;
            return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
        }
        set
        {
            AreaColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
        }
    }

    System.Drawing.Color? IChartCandleElement.FontColor
    {
        get
        {
            System.Windows.Media.Color? fontColor = FontColor;
            ref System.Windows.Media.Color? local = ref fontColor;
            return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
        }
        set
        {
            FontColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
        }
    }

    System.Drawing.Color? IChartCandleElement.Timeframe2Color
    {
        get
        {
            System.Windows.Media.Color? timeframe2Color = Timeframe2Color;
            ref System.Windows.Media.Color? local = ref timeframe2Color;
            return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
        }
        set
        {
            Timeframe2Color = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
        }
    }

    System.Drawing.Color? IChartCandleElement.Timeframe2FrameColor
    {
        get
        {
            System.Windows.Media.Color? timeframe2FrameColor = Timeframe2FrameColor;
            ref System.Windows.Media.Color? local = ref timeframe2FrameColor;
            return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
        }
        set
        {
            Timeframe2FrameColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
        }
    }

    System.Drawing.Color? IChartCandleElement.Timeframe3Color
    {
        get
        {
            System.Windows.Media.Color? timeframe3Color = Timeframe3Color;
            ref System.Windows.Media.Color? local = ref timeframe3Color;
            return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
        }
        set
        {
            Timeframe3Color = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
        }
    }

    System.Drawing.Color? IChartCandleElement.MaxVolumeColor
    {
        get
        {
            System.Windows.Media.Color? maxVolumeColor = MaxVolumeColor;
            ref System.Windows.Media.Color? local = ref maxVolumeColor;
            return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
        }
        set
        {
            MaxVolumeColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
        }
    }

    System.Drawing.Color? IChartCandleElement.ClusterLineColor
    {
        get
        {
            System.Windows.Media.Color? clusterLineColor = ClusterLineColor;
            ref System.Windows.Media.Color? local = ref clusterLineColor;
            return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
        }
        set
        {
            ClusterLineColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
        }
    }

    System.Drawing.Color? IChartCandleElement.ClusterSeparatorLineColor
    {
        get
        {
            System.Windows.Media.Color? separatorLineColor = ClusterSeparatorLineColor;
            ref System.Windows.Media.Color? local = ref separatorLineColor;
            return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
        }
        set
        {
            ClusterSeparatorLineColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
        }
    }

    System.Drawing.Color? IChartCandleElement.ClusterTextColor
    {
        get
        {
            System.Windows.Media.Color? clusterTextColor = ClusterTextColor;
            ref System.Windows.Media.Color? local = ref clusterTextColor;
            return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
        }
        set
        {
            ClusterTextColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
        }
    }

    System.Drawing.Color? IChartCandleElement.ClusterColor
    {
        get
        {
            System.Windows.Media.Color? clusterColor = ClusterColor;
            ref System.Windows.Media.Color? local = ref clusterColor;
            return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
        }
        set
        {
            ClusterColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
        }
    }

    System.Drawing.Color? IChartCandleElement.ClusterMaxColor
    {
        get
        {
            System.Windows.Media.Color? clusterMaxColor = ClusterMaxColor;
            ref System.Windows.Media.Color? local = ref clusterMaxColor;
            return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
        }
        set
        {
            ClusterMaxColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
        }
    }

    System.Drawing.Color? IChartCandleElement.HorizontalVolumeColor
    {
        get
        {
            System.Windows.Media.Color? horizontalVolumeColor = HorizontalVolumeColor;
            ref System.Windows.Media.Color? local = ref horizontalVolumeColor;
            return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
        }
        set
        {
            HorizontalVolumeColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
        }
    }

    System.Drawing.Color? IChartCandleElement.HorizontalVolumeFontColor
    {
        get
        {
            System.Windows.Media.Color? horizontalVolumeFontColor = HorizontalVolumeFontColor;
            ref System.Windows.Media.Color? local = ref horizontalVolumeFontColor;
            return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
        }
        set
        {
            HorizontalVolumeFontColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
        }
    }

    System.Drawing.Color? IChartCandleElement.BuyColor
    {
        get
        {
            System.Windows.Media.Color? buyColor = BuyColor;
            ref System.Windows.Media.Color? local = ref buyColor;
            return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
        }
        set
        {
            BuyColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
        }
    }

    System.Drawing.Color? IChartCandleElement.SellColor
    {
        get
        {
            System.Windows.Media.Color? sellColor = SellColor;
            ref System.Windows.Media.Color? local = ref sellColor;
            return !local.HasValue ? new System.Drawing.Color?() : new System.Drawing.Color?(local.GetValueOrDefault().FromWpf());
        }
        set
        {
            SellColor = value.HasValue ? new System.Windows.Media.Color?(value.GetValueOrDefault().ToWpf()) : new System.Windows.Media.Color?();
        }
    }

    Func<DateTimeOffset, bool, bool, System.Drawing.Color?> IChartCandleElement.Colorer
    {
        get => _drawingColor;
        set
        {
            ChartCandleElement.InternalSealClassQMSS v89qiXjFj7EloQmsS0 = new ChartCandleElement.InternalSealClassQMSS();
            v89qiXjFj7EloQmsS0._colorFunctionGz2 = value;
            _drawingColor = v89qiXjFj7EloQmsS0._colorFunctionGz2;
            if ( _drawingColor == null )
                Colorer = (Func<DateTimeOffset, bool, bool, System.Windows.Media.Color?>)null;
            else
                Colorer = new Func<DateTimeOffset, bool, bool, System.Windows.Media.Color?>(v89qiXjFj7EloQmsS0.Method0123);
        }
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);

        UpFillColor          = storage.GetValue<int>("UpFillColor", UpFillColor.ToInt()).ToColor();
        UpBorderColor        = storage.GetValue<int>("UpBorderColor", UpBorderColor.ToInt()).ToColor();
        DownFillColor        = storage.GetValue<int>("DownFillColor", DownFillColor.ToInt()).ToColor();
        DownBorderColor      = storage.GetValue<int>("DownBorderColor", DownBorderColor.ToInt()).ToColor();        
        var lineColorSetting = storage.GetValue<int?>("LineColor", null );        
        LineColor            = lineColorSetting.HasValue ? new System.Windows.Media.Color?(lineColorSetting.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();               
        var AreaColorSetting = storage.GetValue<int?>("AreaColor", null );        
        AreaColor            = AreaColorSetting.HasValue ? new System.Windows.Media.Color?(AreaColorSetting.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
        DrawStyle            = storage.GetValue<ChartCandleDrawStyles>("DrawStyle", DrawStyle);
        StrokeThickness      = storage.GetValue<int>("StrokeThickness", StrokeThickness);
        AntiAliasing         = storage.GetValue<bool>("AntiAliasing", AntiAliasing);
        ShowAxisMarker       = storage.GetValue<bool>("ShowAxisMarker", ShowAxisMarker);
        var ClusterSetting = storage.GetValue<SettingsStorage>("Cluster", (SettingsStorage)null);
        if ( ClusterSetting == null )
            return;
        LoadCluster(ClusterSetting);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<int>("UpFillColor", UpFillColor.ToInt());
        storage.SetValue<int>("UpBorderColor", UpBorderColor.ToInt());
        storage.SetValue<int>("DownFillColor", DownFillColor.ToInt());
        storage.SetValue<int>("DownBorderColor", DownBorderColor.ToInt());
        
        int? lineColorSetting = LineColor.HasValue ? new int?(LineColor.GetValueOrDefault().ToInt()) : new int?();
        storage.SetValue<int?>("LineColor", lineColorSetting);
                
        int? AreaColorSetting = AreaColor.HasValue ? new int?(AreaColor.GetValueOrDefault().ToInt()) : new int?();
        storage.SetValue<int?>("AreaColor", AreaColorSetting);
        storage.SetValue<string>("DrawStyle", Converter.To<string>((object)DrawStyle));
        storage.SetValue<int>("StrokeThickness", StrokeThickness);
        storage.SetValue<bool>("AntiAliasing", AntiAliasing);
        storage.SetValue<bool>("ShowAxisMarker", ShowAxisMarker);
        storage.SetValue<SettingsStorage>("Cluster", SaveCluster());
    }

    private void LoadCluster(SettingsStorage settings)
    {
        Timeframe2Multiplier = settings != null ? settings.GetValue<int?>("Timeframe2Multiplier", Timeframe2Multiplier) : throw new ArgumentNullException("storage");
        Timeframe3Multiplier = settings.GetValue<int?>("Timeframe3Multiplier", Timeframe3Multiplier);
        
        var fontColor = settings.GetValue<int?>("FontColor", new int?());
        FontColor = fontColor.HasValue ? new System.Windows.Media.Color?(fontColor.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
                
        var timeframe2ColorSetting = settings.GetValue<int?>("Timeframe2Color", null );        
        Timeframe2Color = timeframe2ColorSetting.HasValue ? new System.Windows.Media.Color?(timeframe2ColorSetting.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
                
        var Timeframe2FrameColorSetting = settings.GetValue<int?>("Timeframe2FrameColor", null );        
        Timeframe2FrameColor = Timeframe2FrameColorSetting.HasValue ? new System.Windows.Media.Color?(Timeframe2FrameColorSetting.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
        
        var Timeframe3Colorsetting = settings.GetValue<int?>("Timeframe3Color", null );        
        Timeframe3Color = Timeframe3Colorsetting.HasValue ? new System.Windows.Media.Color?(Timeframe3Colorsetting.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
        
        var MaxVolumeColorSettings = settings.GetValue<int?>("MaxVolumeColor", null);        
        MaxVolumeColor = MaxVolumeColorSettings.HasValue ? new System.Windows.Media.Color?(MaxVolumeColorSettings.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
        
        var MaxVolumeBackgroundsetting = settings.GetValue<int?>("MaxVolumeBackground", null );        
        MaxVolumeBackground = MaxVolumeBackgroundsetting.HasValue ? new System.Windows.Media.Color?(MaxVolumeBackgroundsetting.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
        
        var clusterSeparatorLineColorsetting = settings.GetValue<int?>("ClusterSeparatorLineColor", null );        
        ClusterSeparatorLineColor = clusterSeparatorLineColorsetting.HasValue ? new System.Windows.Media.Color?(clusterSeparatorLineColorsetting.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
        
        var ClusterLineColorsetting = settings.GetValue<int?>("ClusterLineColor", null );        
        ClusterLineColor = ClusterLineColorsetting.HasValue ? new System.Windows.Media.Color?(ClusterLineColorsetting.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
        
        var ClusterTextColorsetting = settings.GetValue<int?>("ClusterTextColor", null );        
        ClusterTextColor = ClusterTextColorsetting.HasValue ? new System.Windows.Media.Color?(ClusterTextColorsetting.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();

        
        var ClusterColorsetting = settings.GetValue<int?>("ClusterColor", null );        
        ClusterColor = ClusterColorsetting.HasValue ? new System.Windows.Media.Color?(ClusterColorsetting.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
        
        
        var ClusterMaxColorSetting = settings.GetValue<int?>("ClusterMaxColor", null );        
        ClusterMaxColor = ClusterMaxColorSetting.HasValue ? new System.Windows.Media.Color?(ClusterMaxColorSetting.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
        ShowHorizontalVolumes = settings.GetValue<bool>("ShowHorizontalVolumes", false);
        LocalHorizontalVolumes = settings.GetValue<bool>("LocalHorizontalVolumes", false);
        HorizontalVolumeWidthFraction = settings.GetValue<double>("HorizontalVolumeWidthFraction", 0.0);

        
        var HorizontalVolumeColorSetting = settings.GetValue<int?>("HorizontalVolumeColor", null );        
        HorizontalVolumeColor = HorizontalVolumeColorSetting.HasValue ? new System.Windows.Media.Color?(HorizontalVolumeColorSetting.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();

        
        var HorizontalVolumeFontColorSetting = settings.GetValue<int?>("HorizontalVolumeFontColor", null );        
        HorizontalVolumeFontColor = HorizontalVolumeFontColorSetting.HasValue ? new System.Windows.Media.Color?(HorizontalVolumeFontColorSetting.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();

        PriceStep = settings.GetValue<Decimal?>("PriceStep", new Decimal?());
        FontFamily = settings.GetValue<string>("FontFamily", FontFamily);
        FontSize = settings.GetValue<Decimal>("FontSize", FontSize);
        FontWeight = FontWeight.FromOpenTypeWeight(settings.GetValue<int>("FontWeight", FontWeight.ToOpenTypeWeight()));
        DrawSeparateVolumes = settings.GetValue<bool>("DrawSeparateVolumes", DrawSeparateVolumes);
        SettingsStorage settingsStorage13 = settings;
        
        var BuyColorsetting = settings.GetValue<int?>("BuyColor", null );        
        BuyColor = BuyColorsetting.HasValue ? new System.Windows.Media.Color?(BuyColorsetting.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
        
        var SellColorsetting = settings.GetValue<int?>("SellColor", null );        
        SellColor = SellColorsetting.HasValue ? new System.Windows.Media.Color?(SellColorsetting.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
        
        var UpColorsetting = settings.GetValue<int?>("UpColor", null);
        
        UpColor = UpColorsetting.HasValue ? new System.Windows.Media.Color?(UpColorsetting.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
        
        var DownColorsetting = settings.GetValue<int?>("DownColor", null );
        
        DownColor = DownColorsetting.HasValue ? new System.Windows.Media.Color?(DownColorsetting.GetValueOrDefault().ToColor()) : new System.Windows.Media.Color?();
    }

    private SettingsStorage SaveCluster()
    {
        SettingsStorage settingsStorage = new SettingsStorage();
        settingsStorage.SetValue<int?>("Timeframe2Multiplier", Timeframe2Multiplier);
        settingsStorage.SetValue<int?>("Timeframe3Multiplier", Timeframe3Multiplier);
        System.Windows.Media.Color? nullable = FontColor;
        ref System.Windows.Media.Color? local1 = ref nullable;
        settingsStorage.SetValue<int?>("FontColor", local1.HasValue ? new int?(local1.GetValueOrDefault().ToInt()) : new int?());
        nullable = Timeframe2Color;
        ref System.Windows.Media.Color? local2 = ref nullable;
        settingsStorage.SetValue<int?>("Timeframe2Color", local2.HasValue ? new int?(local2.GetValueOrDefault().ToInt()) : new int?());
        nullable = Timeframe2FrameColor;
        ref System.Windows.Media.Color? local3 = ref nullable;
        settingsStorage.SetValue<int?>("Timeframe2FrameColor", local3.HasValue ? new int?(local3.GetValueOrDefault().ToInt()) : new int?());
        nullable = Timeframe3Color;
        ref System.Windows.Media.Color? local4 = ref nullable;
        settingsStorage.SetValue<int?>("Timeframe3Color", local4.HasValue ? new int?(local4.GetValueOrDefault().ToInt()) : new int?());
        nullable = MaxVolumeColor;
        ref System.Windows.Media.Color? local5 = ref nullable;
        settingsStorage.SetValue<int?>("MaxVolumeColor", local5.HasValue ? new int?(local5.GetValueOrDefault().ToInt()) : new int?());
        nullable = MaxVolumeBackground;
        ref System.Windows.Media.Color? local6 = ref nullable;
        settingsStorage.SetValue<int?>("MaxVolumeBackground", local6.HasValue ? new int?(local6.GetValueOrDefault().ToInt()) : new int?());
        nullable = ClusterLineColor;
        ref System.Windows.Media.Color? local7 = ref nullable;
        settingsStorage.SetValue<int?>("ClusterLineColor", local7.HasValue ? new int?(local7.GetValueOrDefault().ToInt()) : new int?());
        nullable = ClusterSeparatorLineColor;
        ref System.Windows.Media.Color? local8 = ref nullable;
        settingsStorage.SetValue<int?>("ClusterSeparatorLineColor", local8.HasValue ? new int?(local8.GetValueOrDefault().ToInt()) : new int?());
        nullable = ClusterTextColor;
        ref System.Windows.Media.Color? local9 = ref nullable;
        settingsStorage.SetValue<int?>("ClusterTextColor", local9.HasValue ? new int?(local9.GetValueOrDefault().ToInt()) : new int?());
        nullable = ClusterColor;
        ref System.Windows.Media.Color? local10 = ref nullable;
        settingsStorage.SetValue<int?>("ClusterColor", local10.HasValue ? new int?(local10.GetValueOrDefault().ToInt()) : new int?());
        nullable = ClusterMaxColor;
        ref System.Windows.Media.Color? local11 = ref nullable;
        settingsStorage.SetValue<int?>("ClusterMaxColor", local11.HasValue ? new int?(local11.GetValueOrDefault().ToInt()) : new int?());
        settingsStorage.SetValue<bool>("ShowHorizontalVolumes", ShowHorizontalVolumes);
        settingsStorage.SetValue<bool>("LocalHorizontalVolumes", LocalHorizontalVolumes);
        settingsStorage.SetValue<double>("HorizontalVolumeWidthFraction", HorizontalVolumeWidthFraction);
        nullable = HorizontalVolumeColor;
        ref System.Windows.Media.Color? local12 = ref nullable;
        settingsStorage.SetValue<int?>("HorizontalVolumeColor", local12.HasValue ? new int?(local12.GetValueOrDefault().ToInt()) : new int?());
        nullable = HorizontalVolumeFontColor;
        ref System.Windows.Media.Color? local13 = ref nullable;
        settingsStorage.SetValue<int?>("HorizontalVolumeFontColor", local13.HasValue ? new int?(local13.GetValueOrDefault().ToInt()) : new int?());
        settingsStorage.SetValue<Decimal?>("PriceStep", PriceStep);
        settingsStorage.SetValue<string>("FontFamily", FontFamily);
        settingsStorage.SetValue<Decimal>("FontSize", FontSize);
        settingsStorage.SetValue<int>("FontWeight", FontWeight.ToOpenTypeWeight());
        settingsStorage.SetValue<bool>("DrawSeparateVolumes", DrawSeparateVolumes);
        nullable = BuyColor;
        ref System.Windows.Media.Color? local14 = ref nullable;
        settingsStorage.SetValue<int?>("BuyColor", local14.HasValue ? new int?(local14.GetValueOrDefault().ToInt()) : new int?());
        nullable = SellColor;
        ref System.Windows.Media.Color? local15 = ref nullable;
        settingsStorage.SetValue<int?>("SellColor", local15.HasValue ? new int?(local15.GetValueOrDefault().ToInt()) : new int?());
        nullable = UpColor;
        ref System.Windows.Media.Color? local16 = ref nullable;
        settingsStorage.SetValue<int?>("UpColor", local16.HasValue ? new int?(local16.GetValueOrDefault().ToInt()) : new int?());
        nullable = DownColor;
        ref System.Windows.Media.Color? local17 = ref nullable;
        settingsStorage.SetValue<int?>("DownColor", local17.HasValue ? new int?(local17.GetValueOrDefault().ToInt()) : new int?());
        return settingsStorage;
    }

    internal override ChartCandleElement Clone(ChartCandleElement _param1)
    {
        _param1 = base.Clone(_param1);
        _param1.DownFillColor = DownFillColor;
        _param1.UpFillColor = UpFillColor;
        _param1.DownBorderColor = DownBorderColor;
        _param1.UpBorderColor = UpBorderColor;
        _param1.AntiAliasing = AntiAliasing;
        _param1.StrokeThickness = StrokeThickness;
        _param1.LineColor = LineColor;
        _param1.AreaColor = AreaColor;
        _param1.DrawStyle = DrawStyle;
        _param1.ShowAxisMarker = ShowAxisMarker;
        _param1.FontColor = FontColor;
        _param1.Timeframe2Color = Timeframe2Color;
        _param1.Timeframe2FrameColor = Timeframe2FrameColor;
        _param1.Timeframe3Color = Timeframe3Color;
        _param1.MaxVolumeColor = MaxVolumeColor;
        _param1.MaxVolumeBackground = MaxVolumeBackground;
        _param1.Timeframe2Multiplier = Timeframe2Multiplier;
        _param1.Timeframe3Multiplier = Timeframe3Multiplier;
        _param1.ClusterLineColor = ClusterLineColor;
        _param1.ClusterSeparatorLineColor = ClusterSeparatorLineColor;
        _param1.ClusterTextColor = ClusterTextColor;
        _param1.ClusterColor = ClusterColor;
        _param1.ClusterMaxColor = ClusterMaxColor;
        _param1.ShowHorizontalVolumes = ShowHorizontalVolumes;
        _param1.LocalHorizontalVolumes = LocalHorizontalVolumes;
        _param1.HorizontalVolumeWidthFraction = HorizontalVolumeWidthFraction;
        _param1.HorizontalVolumeColor = HorizontalVolumeColor;
        _param1.HorizontalVolumeFontColor = HorizontalVolumeFontColor;
        _param1.FontFamily = FontFamily;
        _param1.FontSize = FontSize;
        _param1.FontWeight = FontWeight;
        _param1.DrawSeparateVolumes = DrawSeparateVolumes;
        _param1.BuyColor = BuyColor;
        _param1.SellColor = SellColor;
        _param1.UpColor = UpColor;
        _param1.DownColor = DownColor;
        return _param1;
    }

    //DrawableChartComponentBaseViewModel IDrawableChartElement.CreateViewModel(
    //  IDrawingSurfaceVM _param1)
    public DrawableChartComponentBaseViewModel CreateViewModel(IDrawingSurfaceVM viewModel)
    {
        throw new NotImplementedException();

        //return _baseViewModel = (DrawableChartComponentBaseViewModel)new ChartCandleElement(this);
    }

    bool IDrawableChartElement.StartDrawing(
      IEnumerableEx<ChartDrawData.IDrawValue> _param1)
    {
        return _baseViewModel.Draw(_param1);
    }

    void IDrawableChartElement.StartDrawing()
    {
        _baseViewModel.Draw(CollectionHelper.ToEx<ChartDrawData.IDrawValue>(Enumerable.Empty<ChartDrawData.IDrawValue>(), 0));
    }

    protected override bool OnDraw(ChartDrawData data)
    {
        List<ChartDrawData.sCandle> source1 = data.GetCandleRelatedData((IChartCandleElement)this);
        List<ChartDrawData.sCandleColor> source2 = data.GetCandleColor((IChartCandleElement)this);
        bool flag1 = source1 != null && !CollectionHelper.IsEmpty<ChartDrawData.sCandle>((ICollection<ChartDrawData.sCandle>)source1);
        bool flag2 = source2 != null && !CollectionHelper.IsEmpty<ChartDrawData.sCandleColor>((ICollection<ChartDrawData.sCandleColor>)source2);
        if ( flag1 || flag2 )
            return ( ( !flag1 ? 0 : ( ( (IDrawableChartElement)this ).StartDrawing(CollectionHelper.ToEx<ChartDrawData.IDrawValue>(source1.Cast<ChartDrawData.IDrawValue>(), source1.Count)) ? 1 : 0 ) ) | ( !flag2 ? ( false ? 1 : 0 ) : ( ( (IDrawableChartElement)this ).StartDrawing(CollectionHelper.ToEx<ChartDrawData.IDrawValue>(source2.Cast<ChartDrawData.IDrawValue>(), source2.Count)) ? 1 : 0 ) ) ) != 0;
        ( (IDrawableChartElement)this ).StartDrawing();
        return false;
    }

    protected override string GetGeneratedTitle()
    {
        Subscription subscription = ChartHelper2025.TryGetSubscription( this );
        return subscription == null ? (string)null : ChartHelper2025.ChartSeriesTitle(subscription);
    }

    protected override int Priority => 0;

    

    private sealed class InternalSealClassQMSS
    {
        public Func<DateTimeOffset, bool, bool, System.Drawing.Color?> _colorFunctionGz2;

        public System.Windows.Media.Color? Method0123(
          DateTimeOffset _param1,
          bool _param2,
          bool _param3)
        {
            System.Drawing.Color? nullable = _colorFunctionGz2(_param1, _param2, _param3);
            ref System.Drawing.Color? local = ref nullable;
            return !local.HasValue ? new System.Windows.Media.Color?() : new System.Windows.Media.Color?(local.GetValueOrDefault().ToWpf());
        }
    }
}



//using Ecng.Collections;
//using Ecng.Common;
//using Ecng.Serialization;
//using Ecng.Xaml;
//using StockSharp.Localization;
//using System;
//using System.Collections.Generic;
//using fx.Collections;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Windows.Media;
//using StockSharp.Charting;

//#pragma warning disable CA1416

//namespace StockSharp.Xaml.Charting
//{

//    public partial class ChartCandleElement : ChartComponentView<ChartCandleElement>, ICloneable, INotifyPropertyChanging, INotifyPropertyChanged, IChartComponent, IDrawableChartElement, IChartElement
//    {
//        private Func<DateTimeOffset, bool, bool, Color?> _colorer;
//        private CandlestickVM _viewModel;

//        private bool _showAxisMarker = true;
//        private int _timeframe2Multiplier = 5;
//        private int _timeframe3Multiplier = 15;
//        private Color _fontColor = Color.FromRgb(51, 51, 51);
//        private Color _timeframe2Color = Color.FromRgb(221, 221, 221);
//        private Color _timeframe2FrameColor = Color.FromRgb(byte.MaxValue, 85, 85);
//        private Color _timeframe3Color = Color.FromRgb(170, 170, 170);
//        private Color _maxVolumeColor = Colors.Red;
//        private Color _clusterLineColor = Color.FromRgb(170, 170, 170);



//        private Color _clusterTextColor = Color.FromRgb(51, 51, 51);
//        private Color _clusterColor = Color.FromRgb(136, 136, 136);
//        private Color _clusterMaxColor = Colors.Red;
//        private bool _showHorizontalVolumes = true;
//        private bool _localHorizontalVolumes = true;
//        private double _horizontalVolumeWidthFraction = 0.15;
//        private Color _horizontalVolumeColor = XamlHelper.ToTransparent(Colors.DarkGreen, 128);
//        private Color _horizontalVolumeFontColor = Colors.DarkGreen;
//        private string _title;
//        private ChartCandleDrawStyles _drawStyle;
//        private Color _downFillColor;
//        private Color _upFillColor;
//        private Color _downBorderColor;
//        private Color _upBorderColor;
//        private int _strokeThickness;
//        private bool _antiAliasing;
//        private Color _lineColor;
//        private Color _areaColor;
//        private bool _showCandlePattern;
//        private bool _showIndicatorResult;
//        private bool _showExtremes;
//        private bool _showMacdExtremes;
//        private bool _showTradingTime;
//        private bool _showElliottWave;

//        private bool _showMonotWave;
//        private bool _showDivergence;
//        private bool _showPriceTimeSignal;
//        private bool _highQualityWaveText = true;
//        private int _signalMargin;

//        private Color _fxFallingBarFill = Color.FromRgb(212, 212, 212);
//        private Color _fxFallingBarPen = Colors.DarkGray;
//        private Color _fxRisingBarFill = Colors.White;
//        private Color _fxRisingBarPen = Colors.DarkGray;

//        private int _waveScenarioNo;
//        private bool _isSimulation;

//        public ChartCandleElement()
//        {
//            DownFillColor   = _fxFallingBarFill;
//            DownBorderColor = _fxFallingBarPen;
//            UpFillColor     = _fxRisingBarFill;
//            UpBorderColor   = _fxRisingBarPen;
//            LineColor       = Colors.DarkBlue;
//            AreaColor       = Colors.DeepSkyBlue;
//            StrokeThickness = 1;
//            DrawStyle       = ChartCandleDrawStyles.CandleStick;
//        }

//        public ChartCandleElement(int fifoCapcity)
//        {
//            DownFillColor   = _fxFallingBarFill;
//            DownBorderColor = _fxFallingBarPen;
//            UpFillColor     = _fxRisingBarFill;
//            UpBorderColor   = _fxRisingBarPen;
//            LineColor       = Colors.DarkBlue;
//            AreaColor       = Colors.DeepSkyBlue;
//            StrokeThickness = 1;
//            DrawStyle       = ChartCandleDrawStyles.CandleStick;
//        }

//        Color IDrawableChartElement.Color
//        {
//            get
//            {
//                return Colors.Transparent;
//            }
//        }



//        [Display(Description = "Str1945", GroupName = "Str1946", Name = "Str215", Order = 20, ResourceType = typeof(LocalizedStrings))]
//        public string Title
//        {
//            get
//            {
//                return _title;
//            }
//            set
//            {
//                if ( _title == value )
//                {
//                    return;
//                }

//                _title = value;
//                FullTitle = value;
//                RaisePropertyChanged(nameof(Title));
//            }
//        }

//        [Display(Description = "Str1947", GroupName = "Str1946", Name = "Str1946", Order = 25, ResourceType = typeof(LocalizedStrings))]
//        public ChartCandleDrawStyles DrawStyle
//        {
//            get
//            {
//                return _drawStyle;
//            }
//            set
//            {
//                if ( _drawStyle == value )
//                {
//                    return;
//                }

//                RaisePropertyValueChanging(nameof(DrawStyle), value);
//                switch ( value )
//                {
//                    case ChartCandleDrawStyles.CandleStick:
//                    case ChartCandleDrawStyles.LineOpen:
//                    case ChartCandleDrawStyles.LineHigh:
//                    case ChartCandleDrawStyles.LineLow:
//                    case ChartCandleDrawStyles.LineClose:
//                    case ChartCandleDrawStyles.Area:
//                        if ( _drawStyle == ChartCandleDrawStyles.Ohlc )
//                        {
//                            StrokeThickness = 1;
//                        }

//                        _drawStyle = value;
//                        RaisePropertyChanged(nameof(DrawStyle));
//                        break;

//                    case ChartCandleDrawStyles.Ohlc:
//                        StrokeThickness = 8;
//                        _drawStyle = value;
//                        RaisePropertyChanged(nameof(DrawStyle));
//                        break;

//                    case ChartCandleDrawStyles.BoxVolume:
//                    case ChartCandleDrawStyles.ClusterProfile:
//                        _drawStyle = value;
//                        RaisePropertyChanged(nameof(DrawStyle));
//                        break;

//                    case ChartCandleDrawStyles.PnF:
//                        StrokeThickness = 1;
//                        AntiAliasing = true;
//                        _drawStyle = value;
//                        RaisePropertyChanged(nameof(DrawStyle));
//                        break;

//                    default:
//                        throw new ArgumentOutOfRangeException();
//                }
//            }
//        }



//        [Display(Description = "Str1949", GroupName = "Str1946", Name = "Str1948", Order = 30, ResourceType = typeof(LocalizedStrings))]
//        public Color DownFillColor
//        {
//            get
//            {
//                return _downFillColor;
//            }
//            set
//            {
//                _downFillColor = value;
//                RaisePropertyChanged(nameof(DownFillColor));
//            }
//        }

//        [Display(Description = "Str1951", GroupName = "Str1946", Name = "Str1950", Order = 32, ResourceType = typeof(LocalizedStrings))]
//        public Color UpFillColor
//        {
//            get
//            {
//                return _upFillColor;
//            }
//            set
//            {
//                _upFillColor = value;
//                RaisePropertyChanged(nameof(UpFillColor));
//            }
//        }




//        [Display(Description = "Str1953", GroupName = "Str1946", Name = "Str1952", Order = 31, ResourceType = typeof(LocalizedStrings))]
//        public Color DownBorderColor
//        {
//            get
//            {
//                return _downBorderColor;
//            }
//            set
//            {
//                _downBorderColor = value;
//                RaisePropertyChanged(nameof(DownBorderColor));
//            }
//        }

//        [Display(Description = "Str1955", GroupName = "Str1946", Name = "Str1954", Order = 33, ResourceType = typeof(LocalizedStrings))]
//        public Color UpBorderColor
//        {
//            get
//            {
//                return _upBorderColor;
//            }
//            set
//            {
//                _upBorderColor = value;
//                RaisePropertyChanged(nameof(UpBorderColor));
//            }
//        }

//        [Display(Description = "Str1957", GroupName = "Str1946", Name = "Str1956", Order = 40, ResourceType = typeof(LocalizedStrings))]
//        public int StrokeThickness
//        {
//            get
//            {
//                return _strokeThickness;
//            }
//            set
//            {
//                _strokeThickness = value;
//                if ( _strokeThickness < 1 || _strokeThickness > 10 )
//                {
//                    //throw new ArgumentOutOfRangeException( nameof( value ), LocalizedStrings.Str1958 );
//                }

//                RaisePropertyChanged(nameof(StrokeThickness));
//            }
//        }

//        [Display(Description = "Str1960", GroupName = "Str1946", Name = "Str1959", Order = 40, ResourceType = typeof(LocalizedStrings))]
//        public bool AntiAliasing
//        {
//            get
//            {
//                return _antiAliasing;
//            }
//            set
//            {
//                _antiAliasing = value;
//                RaisePropertyChanged(nameof(AntiAliasing));
//            }
//        }

//        [Display(Description = "LineColorDot", GroupName = "Str1946", Name = "LineColor", Order = 46, ResourceType = typeof(LocalizedStrings))]
//        public Color LineColor
//        {
//            get
//            {
//                return _lineColor;
//            }
//            set
//            {
//                _lineColor = value;
//                RaisePropertyChanged(nameof(LineColor));
//            }
//        }

//        [Display(Description = "AreaColorDot", GroupName = "Str1946", Name = "AreaColor", Order = 47, ResourceType = typeof(LocalizedStrings))]
//        public Color AreaColor
//        {
//            get
//            {
//                return _areaColor;
//            }
//            set
//            {
//                _areaColor = value;
//                RaisePropertyChanged(nameof(AreaColor));
//            }
//        }

//        [Display(Description = "Str1962", GroupName = "Str1946", Name = "Str1961", Order = 50, ResourceType = typeof(LocalizedStrings))]
//        public bool ShowAxisMarker
//        {
//            get
//            {
//                return _showAxisMarker;
//            }
//            set
//            {
//                _showAxisMarker = value;
//                RaisePropertyChanged(nameof(ShowAxisMarker));
//            }
//        }

//        [Browsable(false)]
//        public new Func<DateTimeOffset, bool, bool, Color?> Colorer
//        {
//            get
//            {
//                return _colorer;
//            }
//            set
//            {
//                _colorer = value;
//                RaisePropertyChanged(nameof(Colorer));
//            }
//        }

//        [Display(Description = "TimeframeMultiplierDescr", GroupName = "VolumeSettings", Name = "Timeframe2Multiplier", Order = 3, ResourceType = typeof(LocalizedStrings))]
//        public int Timeframe2Multiplier
//        {
//            get
//            {
//                return _timeframe2Multiplier;
//            }
//            set
//            {
//                if ( value < 1 )
//                {
//                    throw new ArgumentOutOfRangeException(nameof(Timeframe2Multiplier));
//                }

//                SetField(ref _timeframe2Multiplier, value, nameof(Timeframe2Multiplier));
//            }
//        }

//        [Display(Description = "TimeframeMultiplierDescr", GroupName = "VolumeSettings", Name = "Timeframe3Multiplier", Order = 4, ResourceType = typeof(LocalizedStrings))]
//        public int Timeframe3Multiplier
//        {
//            get
//            {
//                return _timeframe3Multiplier;
//            }
//            set
//            {
//                if ( value < 1 )
//                {
//                    throw new ArgumentOutOfRangeException(nameof(Timeframe3Multiplier));
//                }

//                SetField(ref _timeframe3Multiplier, value, nameof(Timeframe3Multiplier));
//            }
//        }

//        [Display(Description = "FontColorDot", GroupName = "VolumeSettings", Name = "FontColor", Order = 5, ResourceType = typeof(LocalizedStrings))]
//        public Color FontColor
//        {
//            get
//            {
//                return _fontColor;
//            }
//            set
//            {
//                SetField(ref _fontColor, value, nameof(FontColor));
//            }
//        }

//        [Display(Description = "Timeframe2GridColorDot", GroupName = "VolumeSettings", Name = "Timeframe2GridColor", Order = 6, ResourceType = typeof(LocalizedStrings))]
//        public Color Timeframe2Color
//        {
//            get
//            {
//                return _timeframe2Color;
//            }
//            set
//            {
//                SetField(ref _timeframe2Color, value, nameof(Timeframe2Color));
//            }
//        }

//        [Display(Description = "Timeframe2FrameColorDot", GroupName = "VolumeSettings", Name = "Timeframe2FrameColor", Order = 7, ResourceType = typeof(LocalizedStrings))]
//        public Color Timeframe2FrameColor
//        {
//            get
//            {
//                return _timeframe2FrameColor;
//            }
//            set
//            {
//                SetField(ref _timeframe2FrameColor, value, nameof(Timeframe2FrameColor));
//            }
//        }

//        [Display(Description = "Timeframe3GridColorDot", GroupName = "VolumeSettings", Name = "Timeframe3GridColor", Order = 8, ResourceType = typeof(LocalizedStrings))]
//        public Color Timeframe3Color
//        {
//            get
//            {
//                return _timeframe3Color;
//            }
//            set
//            {
//                SetField(ref _timeframe3Color, value, nameof(Timeframe3Color));
//            }
//        }

//        [Display(Description = "MaxVolumeColorDot", GroupName = "VolumeSettings", Name = "MaxVolumeColor", Order = 9, ResourceType = typeof(LocalizedStrings))]
//        public Color MaxVolumeColor
//        {
//            get
//            {
//                return _maxVolumeColor;
//            }
//            set
//            {
//                SetField(ref _maxVolumeColor, value, nameof(MaxVolumeColor));
//            }
//        }

//        [Display(Description = "ClusterLineColorDot", GroupName = "VolumeSettings", Name = "ClusterLineColor", Order = 10, ResourceType = typeof(LocalizedStrings))]
//        public Color ClusterLineColor
//        {
//            get
//            {
//                return _clusterLineColor;
//            }
//            set
//            {
//                SetField(ref _clusterLineColor, value, nameof(ClusterLineColor));
//            }
//        }

//        [Display(Description = "ClusterTextColorDot", GroupName = "VolumeSettings", Name = "ClusterTextColor", Order = 11, ResourceType = typeof(LocalizedStrings))]
//        public Color ClusterTextColor
//        {
//            get
//            {
//                return _clusterTextColor;
//            }
//            set
//            {
//                SetField(ref _clusterTextColor, value, nameof(ClusterTextColor));
//            }
//        }

//        [Display(Description = "ClusterColorDot", GroupName = "VolumeSettings", Name = "ClusterColor", Order = 12, ResourceType = typeof(LocalizedStrings))]
//        public Color ClusterColor
//        {
//            get
//            {
//                return _clusterColor;
//            }
//            set
//            {
//                SetField(ref _clusterColor, value, nameof(ClusterColor));
//            }
//        }

//        [Display(Description = "ClusterMaxVolumeColorDot", GroupName = "VolumeSettings", Name = "ClusterMaxVolumeColor", Order = 13, ResourceType = typeof(LocalizedStrings))]
//        public Color ClusterMaxColor
//        {
//            get
//            {
//                return _clusterMaxColor;
//            }
//            set
//            {
//                SetField(ref _clusterMaxColor, value, nameof(ClusterMaxColor));
//            }
//        }

//        [Display(Description = "ShowHorizontalVolumesDot", GroupName = "VolumeSettings", Name = "ShowHorizontalVolumes", Order = 14, ResourceType = typeof(LocalizedStrings))]
//        public bool ShowHorizontalVolumes
//        {
//            get
//            {
//                return _showHorizontalVolumes;
//            }
//            set
//            {
//                SetField(ref _showHorizontalVolumes, value, nameof(ShowHorizontalVolumes));
//            }
//        }

//        [Display(Description = "LocalHorizontalVolumesDot", GroupName = "VolumeSettings", Name = "LocalHorizontalVolumes", Order = 15, ResourceType = typeof(LocalizedStrings))]
//        public bool LocalHorizontalVolumes
//        {
//            get
//            {
//                return _localHorizontalVolumes;
//            }
//            set
//            {
//                SetField(ref _localHorizontalVolumes, value, nameof(LocalHorizontalVolumes));
//            }
//        }

//        [Display(Description = "HorizontalVolumeWidthFractionDot", GroupName = "VolumeSettings", Name = "HorizontalVolumeWidthFraction", Order = 16, ResourceType = typeof(LocalizedStrings))]
//        public double HorizontalVolumeWidthFraction
//        {
//            get
//            {
//                return _horizontalVolumeWidthFraction;
//            }
//            set
//            {
//                if ( value < 0.0 || value > 1.0 )
//                {
//                    throw new ArgumentOutOfRangeException(nameof(HorizontalVolumeWidthFraction));
//                }

//                SetField(ref _horizontalVolumeWidthFraction, value, nameof(HorizontalVolumeWidthFraction));
//            }
//        }

//        [Display(Description = "HorizontalVolumeColorDot", GroupName = "VolumeSettings", Name = "HorizontalVolumeColor", Order = 17, ResourceType = typeof(LocalizedStrings))]
//        public Color HorizontalVolumeColor
//        {
//            get
//            {
//                return _horizontalVolumeColor;
//            }
//            set
//            {
//                SetField(ref _horizontalVolumeColor, value, nameof(HorizontalVolumeColor));
//            }
//        }

//        [Display(Description = "HorizontalVolumeFontColorDot", GroupName = "VolumeSettings", Name = "HorizontalVolumeFontColor", Order = 18, ResourceType = typeof(LocalizedStrings))]
//        public Color HorizontalVolumeFontColor
//        {
//            get
//            {
//                return _horizontalVolumeFontColor;
//            }
//            set
//            {
//                SetField(ref _horizontalVolumeFontColor, value, nameof(HorizontalVolumeFontColor));
//            }
//        }

//        public override void Load(SettingsStorage storage)
//        {
//            base.Load(storage);
//            UpFillColor              = storage.GetValue<object>("UpFillColor", null).ToColor();
//            UpBorderColor            = storage.GetValue<object>("UpBorderColor", null).ToColor();
//            DownFillColor            = storage.GetValue<object>("DownFillColor", null).ToColor();
//            DownBorderColor          = storage.GetValue<object>("DownBorderColor", null).ToColor();
//            var lineColor = storage.GetValue<object>("LineColor", null);
//            LineColor                = lineColor != null ? lineColor.ToColor() : LineColor;
//            var areaColor = storage.GetValue<SettingsStorage>("AreaColor", null);
//            AreaColor                = areaColor != null ? areaColor.ToColor() : AreaColor;
//            DrawStyle                = storage.GetValue<ChartCandleDrawStyles>("DrawStyle", 0);
//            StrokeThickness          = storage.GetValue("StrokeThickness", 0);
//            AntiAliasing             = storage.GetValue("AntiAliasing", false);
//            Title                    = storage.GetValue<string>("Title", null);
//            ShowAxisMarker           = storage.GetValue("ShowAxisMarker", true);
//            SettingsStorage settings = (SettingsStorage)CollectionHelper.TryGetValue(storage, "Cluster");
//            if ( settings == null )
//            {
//                return;
//            }

//            LoadClusterSettings(settings);
//        }

//        private void LoadClusterSettings(SettingsStorage settings)
//        {
//            Timeframe2Multiplier          = settings.GetValue("Timeframe2Multiplier", Timeframe2Multiplier);
//            Timeframe3Multiplier          = settings.GetValue("Timeframe3Multiplier", Timeframe3Multiplier);
//            FontColor                     = settings.GetValue<object>("FontColor", null).ToColor();
//            Timeframe2Color               = settings.GetValue<object>("Timeframe2Color", null).ToColor();
//            Timeframe2FrameColor          = settings.GetValue<object>("Timeframe2FrameColor", null).ToColor();
//            Timeframe3Color               = settings.GetValue<object>("Timeframe3Color", null).ToColor();
//            MaxVolumeColor                = settings.GetValue<object>("MaxVolumeColor", null).ToColor();
//            ClusterLineColor              = settings.GetValue<object>("ClusterLineColor", null).ToColor();
//            ClusterTextColor              = settings.GetValue<object>("ClusterTextColor", null).ToColor();
//            ClusterColor                  = settings.GetValue<object>("ClusterColor", null).ToColor();
//            ClusterMaxColor               = settings.GetValue<object>("ClusterMaxColor", null).ToColor();
//            ShowHorizontalVolumes         = settings.GetValue("ShowHorizontalVolumes", false);
//            LocalHorizontalVolumes        = settings.GetValue("LocalHorizontalVolumes", false);
//            HorizontalVolumeWidthFraction = settings.GetValue("HorizontalVolumeWidthFraction", 0.0);
//            HorizontalVolumeColor         = settings.GetValue<object>("HorizontalVolumeColor", null).ToColor();
//            HorizontalVolumeFontColor     = settings.GetValue<object>("HorizontalVolumeFontColor", null).ToColor();
//        }

//        public override void Save(SettingsStorage storage)
//        {
//            base.Save(storage);
//            storage.SetValue("UpFillColor", UpFillColor.ToInt());
//            storage.SetValue("UpBorderColor", UpBorderColor.ToInt());
//            storage.SetValue("DownFillColor", ( DownFillColor.ToInt() ));
//            storage.SetValue("DownBorderColor", ( DownBorderColor.ToInt() ));
//            storage.SetValue("LineColor", ( LineColor.ToInt() ));
//            storage.SetValue("AreaColor", ( AreaColor.ToInt() ));
//            storage.SetValue("DrawStyle", Converter.To<string>(DrawStyle));
//            storage.SetValue("StrokeThickness", StrokeThickness);
//            storage.SetValue("AntiAliasing", ( AntiAliasing ));
//            storage.SetValue("Title", Title);
//            storage.SetValue("ShowAxisMarker", ( ShowAxisMarker ));
//            storage.SetValue("Cluster", SaveClusterSettings());
//        }

//        private SettingsStorage SaveClusterSettings()
//        {
//            SettingsStorage settingsStorage = new SettingsStorage();
//            settingsStorage.SetValue("Timeframe2Multiplier", Timeframe2Multiplier);
//            settingsStorage.SetValue("Timeframe3Multiplier", Timeframe3Multiplier);
//            settingsStorage.SetValue("FontColor", FontColor.ToInt());
//            settingsStorage.SetValue("Timeframe2Color", ( Timeframe2Color.ToInt() ));
//            settingsStorage.SetValue("Timeframe2FrameColor", ( Timeframe2FrameColor.ToInt() ));
//            settingsStorage.SetValue("Timeframe3Color", Timeframe3Color.ToInt());
//            settingsStorage.SetValue("MaxVolumeColor", ( MaxVolumeColor.ToInt() ));
//            settingsStorage.SetValue("ClusterLineColor", ( ClusterLineColor.ToInt() ));
//            settingsStorage.SetValue("ClusterTextColor", ( ClusterTextColor.ToInt() ));
//            settingsStorage.SetValue("ClusterColor", ( ClusterColor.ToInt() ));
//            settingsStorage.SetValue("ClusterMaxColor", ( ClusterMaxColor.ToInt() ));
//            settingsStorage.SetValue("ShowHorizontalVolumes", ( ShowHorizontalVolumes ));
//            settingsStorage.SetValue("LocalHorizontalVolumes", ( LocalHorizontalVolumes ));
//            settingsStorage.SetValue("HorizontalVolumeWidthFraction", HorizontalVolumeWidthFraction);
//            settingsStorage.SetValue("HorizontalVolumeColor", ( HorizontalVolumeColor.ToInt() ));
//            settingsStorage.SetValue("HorizontalVolumeFontColor", ( HorizontalVolumeFontColor.ToInt() ));
//            return settingsStorage;
//        }

//        internal override ChartCandleElement Clone(ChartCandleElement other)
//        {
//            other                               = base.Clone(other);
//            other.DownFillColor                 = DownFillColor;
//            other.UpFillColor                   = UpFillColor;
//            other.DownBorderColor               = DownBorderColor;
//            other.UpBorderColor                 = UpBorderColor;
//            other.AntiAliasing                  = AntiAliasing;
//            other.StrokeThickness               = StrokeThickness;
//            other.LineColor                     = LineColor;
//            other.AreaColor                     = AreaColor;
//            other.DrawStyle                     = DrawStyle;
//            other.Title                         = Title;
//            other.ShowAxisMarker                = ShowAxisMarker;
//            other.FontColor                     = FontColor;
//            other.Timeframe2Color               = Timeframe2Color;
//            other.Timeframe2FrameColor          = Timeframe2FrameColor;
//            other.Timeframe3Color               = Timeframe3Color;
//            other.MaxVolumeColor                = MaxVolumeColor;
//            other.Timeframe2Multiplier          = Timeframe2Multiplier;
//            other.Timeframe3Multiplier          = Timeframe3Multiplier;
//            other.ClusterLineColor              = ClusterLineColor;
//            other.ClusterTextColor              = ClusterTextColor;
//            other.ClusterColor                  = ClusterColor;
//            other.ClusterMaxColor               = ClusterMaxColor;
//            other.ShowHorizontalVolumes         = ShowHorizontalVolumes;
//            other.LocalHorizontalVolumes        = LocalHorizontalVolumes;
//            other.HorizontalVolumeWidthFraction = HorizontalVolumeWidthFraction;
//            other.HorizontalVolumeColor         = HorizontalVolumeColor;
//            other.HorizontalVolumeFontColor     = HorizontalVolumeFontColor;
//            return other;
//        }

//        public void CheckAndShowFibonacci()
//        {
//            _viewModel.CheckAndShowFibonacci();
//        }

//        DrawableChartComponentBaseViewModel IDrawableChartElement.CreateViewModel(IDrawingSurfaceVM viewModel)
//        {
//            return _viewModel = new CandlestickVM(this);
//        }



//        public void CenterViewOnTime(DateTime selectedBarTime)
//        {
//            _viewModel.CenterViewOnTime(selectedBarTime);

//        }


//        protected override bool OnDraw(ChartDrawData data)
//        {
//            /* -------------------------------------------------------------------------------------------------------------------------------------------
//            * 
//            *  Step 7----------> 10 ChartCandleElementViewModel get the Candle from the data and start Drawing from there.
//            *                           
//            * ------------------------------------------------------------------------------------------------------------------------------------------- 
//            */
//            if ( data.HasCandleData )
//            {
//                var canlesData = data.GetCandle((IChartCandleElement)this);

//                if ( canlesData.IsSet )
//                {
//                    return StartDrawing(canlesData);
//                }
//            }

//            ( (IDrawableChartElement)this ).StartDrawing();
//            return false;
//        }

//        bool StartDrawing(ChartDrawData.sCandleEx drawValues)
//        {
//            return _viewModel.TonyDrawSeries(drawValues);
//        }

//        bool IDrawableChartElement.StartDrawing(IEnumerableEx<ChartDrawData.IDrawValue> drawValues)
//        {
//            return _viewModel.Draw(drawValues);
//        }

//        void IDrawableChartElement.StartDrawing()
//        {
//            _viewModel.Draw(Enumerable.Empty<ChartDrawData.IDrawValue>().ToEx(0));
//        }


//    }
//}


