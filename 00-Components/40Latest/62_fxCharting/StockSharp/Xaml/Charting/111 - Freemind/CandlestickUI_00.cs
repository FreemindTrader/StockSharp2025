using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Localization;
using System;
using System.Collections.Generic; 
using fx.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Media;
using StockSharp.Charting;

#pragma warning disable CA1416

namespace StockSharp.Xaml.Charting
{
    
    public partial class ChartCandleElementEx : ChartComponentViewModel<ChartCandleElementEx>, ICloneable, INotifyPropertyChanging, INotifyPropertyChanged, IChartComponent, IChartElementUiDomain, IChartElement
    {
        private Func<DateTimeOffset, bool, bool, Color?> _colorer;
        private CandlestickVM _viewModel;

        private bool                  _showAxisMarker                = true;
        private int                   _timeframe2Multiplier          = 5;
        private int                   _timeframe3Multiplier          = 15;
        private Color                 _fontColor                     = Color.FromRgb( 51, 51, 51 );
        private Color                 _timeframe2Color               = Color.FromRgb( 221, 221, 221 );
        private Color                 _timeframe2FrameColor          = Color.FromRgb(byte.MaxValue, 85, 85 );
        private Color                 _timeframe3Color               = Color.FromRgb( 170, 170, 170 );
        private Color                 _maxVolumeColor                = Colors.Red;
        private Color                 _clusterLineColor              = Color.FromRgb( 170, 170, 170 );

        

        private Color                 _clusterTextColor              = Color.FromRgb( 51, 51, 51 );
        private Color                 _clusterColor                  = Color.FromRgb( 136, 136, 136 );
        private Color                 _clusterMaxColor               = Colors.Red;
        private bool                  _showHorizontalVolumes         = true;
        private bool                  _localHorizontalVolumes        = true;
        private double                _horizontalVolumeWidthFraction = 0.15;
        private Color                 _horizontalVolumeColor         = XamlHelper.ToTransparent(Colors.DarkGreen, 128 );
        private Color                 _horizontalVolumeFontColor     = Colors.DarkGreen;
        private string                _title;
        private ChartCandleDrawStyles _drawStyle;
        private Color                 _downFillColor;
        private Color                 _upFillColor;
        private Color                 _downBorderColor;
        private Color                 _upBorderColor;
        private int                   _strokeThickness;
        private bool                  _antiAliasing;
        private Color                 _lineColor;
        private Color                 _areaColor;
        private bool                  _showCandlePattern;
        private bool                  _showIndicatorResult;
        private bool                  _showExtremes;
        private bool                  _showMacdExtremes;
        private bool                  _showTradingTime;
        private bool                  _showElliottWave;
        
        private bool                  _showMonotWave;
        private bool                  _showDivergence;        
        private bool                  _showPriceTimeSignal;
        private bool                  _highQualityWaveText           = true;
        private int                   _signalMargin;        

        private Color                 _fxFallingBarFill              = Color.FromRgb( 212, 212, 212 );
        private Color                 _fxFallingBarPen               = Colors.DarkGray;
        private Color                 _fxRisingBarFill               = Colors.White;
        private Color                 _fxRisingBarPen                = Colors.DarkGray;

        private int                  _waveScenarioNo;
        private bool _isSimulation;

        public ChartCandleElementEx( )
        {
            DownFillColor   = _fxFallingBarFill;
            DownBorderColor = _fxFallingBarPen;
            UpFillColor     = _fxRisingBarFill;
            UpBorderColor   = _fxRisingBarPen;
            LineColor       = Colors.DarkBlue;
            AreaColor       = Colors.DeepSkyBlue;
            StrokeThickness = 1;
            DrawStyle       = ChartCandleDrawStyles.CandleStick;
        }

        public ChartCandleElementEx( int fifoCapcity )
        {
            DownFillColor   = _fxFallingBarFill;
            DownBorderColor = _fxFallingBarPen;
            UpFillColor     = _fxRisingBarFill;
            UpBorderColor   = _fxRisingBarPen;
            LineColor       = Colors.DarkBlue;
            AreaColor       = Colors.DeepSkyBlue;
            StrokeThickness = 1;
            DrawStyle       = ChartCandleDrawStyles.CandleStick;
        }

        Color IChartElementUiDomain.Color
        {
            get
            {
                return Colors.Transparent;
            }
        }

        

        [Display( Description = "Str1945", GroupName = "Str1946", Name = "Str215", Order = 20, ResourceType = typeof( LocalizedStrings ) )]
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if ( _title == value )
                {
                    return;
                }

                _title = value;
                FullTitle = value;
                RaisePropertyChanged( nameof( Title ) );
            }
        }

        [Display( Description = "Str1947", GroupName = "Str1946", Name = "Str1946", Order = 25, ResourceType = typeof( LocalizedStrings ) )]
        public ChartCandleDrawStyles DrawStyle
        {
            get
            {
                return _drawStyle;
            }
            set
            {
                if ( _drawStyle == value )
                {
                    return;
                }

                RaisePropertyValueChanging( nameof( DrawStyle ), value );
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
                        }
                        
                        _drawStyle = value;
                        RaisePropertyChanged( nameof( DrawStyle ) );
                        break;

                    case ChartCandleDrawStyles.Ohlc:
                        StrokeThickness = 8;
                        _drawStyle = value;
                        RaisePropertyChanged( nameof( DrawStyle ) );
                        break;

                    case ChartCandleDrawStyles.BoxVolume:
                    case ChartCandleDrawStyles.ClusterProfile:
                        _drawStyle = value;
                        RaisePropertyChanged( nameof( DrawStyle ) );
                        break;

                    case ChartCandleDrawStyles.PnF:
                        StrokeThickness = 1;
                        AntiAliasing = true;
                        _drawStyle = value;
                        RaisePropertyChanged( nameof( DrawStyle ) );
                        break;

                    default:
                        throw new ArgumentOutOfRangeException( );
                }
            }
        }

        

        [Display( Description = "Str1949", GroupName = "Str1946", Name = "Str1948", Order = 30, ResourceType = typeof( LocalizedStrings ) )]
        public Color DownFillColor
        {
            get
            {
                return _downFillColor;
            }
            set
            {
                _downFillColor = value;
                RaisePropertyChanged( nameof( DownFillColor ) );
            }
        }

        [Display( Description = "Str1951", GroupName = "Str1946", Name = "Str1950", Order = 32, ResourceType = typeof( LocalizedStrings ) )]
        public Color UpFillColor
        {
            get
            {
                return _upFillColor;
            }
            set
            {
                _upFillColor = value;
                RaisePropertyChanged( nameof( UpFillColor ) );
            }
        }

        


        [ Display( Description = "Str1953", GroupName = "Str1946", Name = "Str1952", Order = 31, ResourceType = typeof( LocalizedStrings ) )]
        public Color DownBorderColor
        {
            get
            {
                return _downBorderColor;
            }
            set
            {
                _downBorderColor = value;
                RaisePropertyChanged( nameof( DownBorderColor ) );
            }
        }

        [Display( Description = "Str1955", GroupName = "Str1946", Name = "Str1954", Order = 33, ResourceType = typeof( LocalizedStrings ) )]
        public Color UpBorderColor
        {
            get
            {
                return _upBorderColor;
            }
            set
            {
                _upBorderColor = value;
                RaisePropertyChanged( nameof( UpBorderColor ) );
            }
        }

        [Display( Description = "Str1957", GroupName = "Str1946", Name = "Str1956", Order = 40, ResourceType = typeof( LocalizedStrings ) )]
        public int StrokeThickness
        {
            get
            {
                return _strokeThickness;
            }
            set
            {
                _strokeThickness = value;
                if ( _strokeThickness < 1 || _strokeThickness > 10 )
                {
                    //throw new ArgumentOutOfRangeException( nameof( value ), LocalizedStrings.Str1958 );
                }

                RaisePropertyChanged( nameof( StrokeThickness ) );
            }
        }

        [Display( Description = "Str1960", GroupName = "Str1946", Name = "Str1959", Order = 40, ResourceType = typeof( LocalizedStrings ) )]
        public bool AntiAliasing
        {
            get
            {
                return _antiAliasing;
            }
            set
            {
                _antiAliasing = value;
                RaisePropertyChanged( nameof( AntiAliasing ) );
            }
        }

        [Display( Description = "LineColorDot", GroupName = "Str1946", Name = "LineColor", Order = 46, ResourceType = typeof( LocalizedStrings ) )]
        public Color LineColor
        {
            get
            {
                return _lineColor;
            }
            set
            {
                _lineColor = value;
                RaisePropertyChanged( nameof( LineColor ) );
            }
        }

        [Display( Description = "AreaColorDot", GroupName = "Str1946", Name = "AreaColor", Order = 47, ResourceType = typeof( LocalizedStrings ) )]
        public Color AreaColor
        {
            get
            {
                return _areaColor;
            }
            set
            {
                _areaColor = value;
                RaisePropertyChanged( nameof( AreaColor ) );
            }
        }

        [Display( Description = "Str1962", GroupName = "Str1946", Name = "Str1961", Order = 50, ResourceType = typeof( LocalizedStrings ) )]
        public bool ShowAxisMarker
        {
            get
            {
                return _showAxisMarker;
            }
            set
            {
                _showAxisMarker = value;
                RaisePropertyChanged( nameof( ShowAxisMarker ) );
            }
        }

        [Browsable( false )]
        public new Func<DateTimeOffset, bool, bool, Color?> Colorer
        {
            get
            {
                return _colorer;
            }
            set
            {
                _colorer = value;
                RaisePropertyChanged( nameof( Colorer ) );
            }
        }

        [Display( Description = "TimeframeMultiplierDescr", GroupName = "VolumeSettings", Name = "Timeframe2Multiplier", Order = 3, ResourceType = typeof( LocalizedStrings ) )]
        public int Timeframe2Multiplier
        {
            get
            {
                return _timeframe2Multiplier;
            }
            set
            {
                if ( value < 1 )
                {
                    throw new ArgumentOutOfRangeException( nameof( Timeframe2Multiplier ) );
                }

                SetField( ref _timeframe2Multiplier, value, nameof( Timeframe2Multiplier ) );
            }
        }

        [Display( Description = "TimeframeMultiplierDescr", GroupName = "VolumeSettings", Name = "Timeframe3Multiplier", Order = 4, ResourceType = typeof( LocalizedStrings ) )]
        public int Timeframe3Multiplier
        {
            get
            {
                return _timeframe3Multiplier;
            }
            set
            {
                if ( value < 1 )
                {
                    throw new ArgumentOutOfRangeException( nameof( Timeframe3Multiplier ) );
                }

                SetField( ref _timeframe3Multiplier, value, nameof( Timeframe3Multiplier ) );
            }
        }

        [Display( Description = "FontColorDot", GroupName = "VolumeSettings", Name = "FontColor", Order = 5, ResourceType = typeof( LocalizedStrings ) )]
        public Color FontColor
        {
            get
            {
                return _fontColor;
            }
            set
            {
                SetField( ref _fontColor, value, nameof( FontColor ) );
            }
        }

        [Display( Description = "Timeframe2GridColorDot", GroupName = "VolumeSettings", Name = "Timeframe2GridColor", Order = 6, ResourceType = typeof( LocalizedStrings ) )]
        public Color Timeframe2Color
        {
            get
            {
                return _timeframe2Color;
            }
            set
            {
                SetField( ref _timeframe2Color, value, nameof( Timeframe2Color ) );
            }
        }

        [Display( Description = "Timeframe2FrameColorDot", GroupName = "VolumeSettings", Name = "Timeframe2FrameColor", Order = 7, ResourceType = typeof( LocalizedStrings ) )]
        public Color Timeframe2FrameColor
        {
            get
            {
                return _timeframe2FrameColor;
            }
            set
            {
                SetField( ref _timeframe2FrameColor, value, nameof( Timeframe2FrameColor ) );
            }
        }

        [Display( Description = "Timeframe3GridColorDot", GroupName = "VolumeSettings", Name = "Timeframe3GridColor", Order = 8, ResourceType = typeof( LocalizedStrings ) )]
        public Color Timeframe3Color
        {
            get
            {
                return _timeframe3Color;
            }
            set
            {
                SetField( ref _timeframe3Color, value, nameof( Timeframe3Color ) );
            }
        }

        [Display( Description = "MaxVolumeColorDot", GroupName = "VolumeSettings", Name = "MaxVolumeColor", Order = 9, ResourceType = typeof( LocalizedStrings ) )]
        public Color MaxVolumeColor
        {
            get
            {
                return _maxVolumeColor;
            }
            set
            {
                SetField( ref _maxVolumeColor, value, nameof( MaxVolumeColor ) );
            }
        }

        [Display( Description = "ClusterLineColorDot", GroupName = "VolumeSettings", Name = "ClusterLineColor", Order = 10, ResourceType = typeof( LocalizedStrings ) )]
        public Color ClusterLineColor
        {
            get
            {
                return _clusterLineColor;
            }
            set
            {
                SetField( ref _clusterLineColor, value, nameof( ClusterLineColor ) );
            }
        }

        [Display( Description = "ClusterTextColorDot", GroupName = "VolumeSettings", Name = "ClusterTextColor", Order = 11, ResourceType = typeof( LocalizedStrings ) )]
        public Color ClusterTextColor
        {
            get
            {
                return _clusterTextColor;
            }
            set
            {
                SetField( ref _clusterTextColor, value, nameof( ClusterTextColor ) );
            }
        }

        [Display( Description = "ClusterColorDot", GroupName = "VolumeSettings", Name = "ClusterColor", Order = 12, ResourceType = typeof( LocalizedStrings ) )]
        public Color ClusterColor
        {
            get
            {
                return _clusterColor;
            }
            set
            {
                SetField( ref _clusterColor, value, nameof( ClusterColor ) );
            }
        }

        [Display( Description = "ClusterMaxVolumeColorDot", GroupName = "VolumeSettings", Name = "ClusterMaxVolumeColor", Order = 13, ResourceType = typeof( LocalizedStrings ) )]
        public Color ClusterMaxColor
        {
            get
            {
                return _clusterMaxColor;
            }
            set
            {
                SetField( ref _clusterMaxColor, value, nameof( ClusterMaxColor ) );
            }
        }

        [Display( Description = "ShowHorizontalVolumesDot", GroupName = "VolumeSettings", Name = "ShowHorizontalVolumes", Order = 14, ResourceType = typeof( LocalizedStrings ) )]
        public bool ShowHorizontalVolumes
        {
            get
            {
                return _showHorizontalVolumes;
            }
            set
            {
                SetField( ref _showHorizontalVolumes, value, nameof( ShowHorizontalVolumes ) );
            }
        }

        [Display( Description = "LocalHorizontalVolumesDot", GroupName = "VolumeSettings", Name = "LocalHorizontalVolumes", Order = 15, ResourceType = typeof( LocalizedStrings ) )]
        public bool LocalHorizontalVolumes
        {
            get
            {
                return _localHorizontalVolumes;
            }
            set
            {
                SetField( ref _localHorizontalVolumes, value, nameof( LocalHorizontalVolumes ) );
            }
        }

        [Display( Description = "HorizontalVolumeWidthFractionDot", GroupName = "VolumeSettings", Name = "HorizontalVolumeWidthFraction", Order = 16, ResourceType = typeof( LocalizedStrings ) )]
        public double HorizontalVolumeWidthFraction
        {
            get
            {
                return _horizontalVolumeWidthFraction;
            }
            set
            {
                if ( value < 0.0 || value > 1.0 )
                {
                    throw new ArgumentOutOfRangeException( nameof( HorizontalVolumeWidthFraction ) );
                }

                SetField( ref _horizontalVolumeWidthFraction, value, nameof( HorizontalVolumeWidthFraction ) );
            }
        }

        [Display( Description = "HorizontalVolumeColorDot", GroupName = "VolumeSettings", Name = "HorizontalVolumeColor", Order = 17, ResourceType = typeof( LocalizedStrings ) )]
        public Color HorizontalVolumeColor
        {
            get
            {
                return _horizontalVolumeColor;
            }
            set
            {
                SetField( ref _horizontalVolumeColor, value, nameof( HorizontalVolumeColor ) );
            }
        }

        [Display( Description = "HorizontalVolumeFontColorDot", GroupName = "VolumeSettings", Name = "HorizontalVolumeFontColor", Order = 18, ResourceType = typeof( LocalizedStrings ) )]
        public Color HorizontalVolumeFontColor
        {
            get
            {
                return _horizontalVolumeFontColor;
            }
            set
            {
                SetField( ref _horizontalVolumeFontColor, value, nameof( HorizontalVolumeFontColor ) );
            }
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            UpFillColor              = storage.GetValue<object>( "UpFillColor", null ).ToColor(); 
            UpBorderColor            = storage.GetValue<object>( "UpBorderColor", null ).ToColor(); 
            DownFillColor            = storage.GetValue<object>( "DownFillColor", null ).ToColor(); 
            DownBorderColor          = storage.GetValue<object>( "DownBorderColor", null ).ToColor(); 
            var lineColor            = storage.GetValue< object >("LineColor",  null);
            LineColor                = lineColor != null ? lineColor.ToColor( ) : LineColor;
            var areaColor            = storage.GetValue<SettingsStorage>("AreaColor",  null);
            AreaColor                = areaColor != null ? areaColor.ToColor( ) : AreaColor;
            DrawStyle                = storage.GetValue<ChartCandleDrawStyles>( "DrawStyle", 0 );
            StrokeThickness          = storage.GetValue( "StrokeThickness", 0 );
            AntiAliasing             = storage.GetValue( "AntiAliasing", false );
            Title                    = storage.GetValue<string>( "Title", null );
            ShowAxisMarker           = storage.GetValue( "ShowAxisMarker", true );
            SettingsStorage settings = (SettingsStorage) CollectionHelper.TryGetValue( storage,  "Cluster");
            if ( settings == null )
            {
                return;
            }

            LoadClusterSettings( settings );
        }

        private void LoadClusterSettings( SettingsStorage settings )
        {
            Timeframe2Multiplier          = settings.GetValue( "Timeframe2Multiplier", Timeframe2Multiplier );
            Timeframe3Multiplier          = settings.GetValue( "Timeframe3Multiplier", Timeframe3Multiplier );
            FontColor                     = settings.GetValue<object>( "FontColor", null ).ToColor();
            Timeframe2Color               = settings.GetValue<object>( "Timeframe2Color", null ).ToColor();
            Timeframe2FrameColor          = settings.GetValue<object>( "Timeframe2FrameColor", null ).ToColor();
            Timeframe3Color               = settings.GetValue<object>( "Timeframe3Color", null ).ToColor(); 
            MaxVolumeColor                = settings.GetValue<object>( "MaxVolumeColor", null ).ToColor(); 
            ClusterLineColor              = settings.GetValue<object>( "ClusterLineColor", null ).ToColor(); 
            ClusterTextColor              = settings.GetValue<object>( "ClusterTextColor", null ).ToColor(); 
            ClusterColor                  = settings.GetValue<object>( "ClusterColor", null ).ToColor(); 
            ClusterMaxColor               = settings.GetValue<object>( "ClusterMaxColor", null ).ToColor(); 
            ShowHorizontalVolumes         = settings.GetValue( "ShowHorizontalVolumes", false );
            LocalHorizontalVolumes        = settings.GetValue( "LocalHorizontalVolumes", false );
            HorizontalVolumeWidthFraction = settings.GetValue( "HorizontalVolumeWidthFraction", 0.0 );
            HorizontalVolumeColor         = settings.GetValue<object>( "HorizontalVolumeColor", null ).ToColor();
            HorizontalVolumeFontColor     = settings.GetValue<object>( "HorizontalVolumeFontColor", null ).ToColor(); 
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "UpFillColor", UpFillColor.ToInt() );
            storage.SetValue( "UpBorderColor", UpBorderColor.ToInt());
            storage.SetValue( "DownFillColor",  ( DownFillColor.ToInt() ) );
            storage.SetValue( "DownBorderColor",  ( DownBorderColor.ToInt() ) );
            storage.SetValue( "LineColor",  ( LineColor.ToInt() ) );
            storage.SetValue( "AreaColor",  ( AreaColor.ToInt() ) );
            storage.SetValue( "DrawStyle", Converter.To<string>( DrawStyle ) );
            storage.SetValue( "StrokeThickness",  StrokeThickness );
            storage.SetValue( "AntiAliasing",  ( AntiAliasing  ) );
            storage.SetValue( "Title",  Title );
            storage.SetValue( "ShowAxisMarker",  ( ShowAxisMarker  ) );
            storage.SetValue( "Cluster",  SaveClusterSettings( ) );
        }

        private SettingsStorage SaveClusterSettings( )
        {
            SettingsStorage settingsStorage = new SettingsStorage();
            settingsStorage.SetValue( "Timeframe2Multiplier",  Timeframe2Multiplier );
            settingsStorage.SetValue( "Timeframe3Multiplier",  Timeframe3Multiplier );
            settingsStorage.SetValue( "FontColor", FontColor.ToInt() );
            settingsStorage.SetValue( "Timeframe2Color",  ( Timeframe2Color.ToInt() ) );
            settingsStorage.SetValue( "Timeframe2FrameColor",  ( Timeframe2FrameColor.ToInt() ) );
            settingsStorage.SetValue( "Timeframe3Color", Timeframe3Color.ToInt() );
            settingsStorage.SetValue( "MaxVolumeColor",  ( MaxVolumeColor.ToInt() ) );
            settingsStorage.SetValue( "ClusterLineColor",  ( ClusterLineColor.ToInt() ) );
            settingsStorage.SetValue( "ClusterTextColor",  ( ClusterTextColor.ToInt() ) );
            settingsStorage.SetValue( "ClusterColor",  ( ClusterColor.ToInt() ) );
            settingsStorage.SetValue( "ClusterMaxColor",  ( ClusterMaxColor.ToInt() ) );
            settingsStorage.SetValue( "ShowHorizontalVolumes",  ( ShowHorizontalVolumes  ) );
            settingsStorage.SetValue( "LocalHorizontalVolumes",  ( LocalHorizontalVolumes  ) );
            settingsStorage.SetValue( "HorizontalVolumeWidthFraction",  HorizontalVolumeWidthFraction );
            settingsStorage.SetValue( "HorizontalVolumeColor",  ( HorizontalVolumeColor.ToInt() ) );
            settingsStorage.SetValue( "HorizontalVolumeFontColor",  ( HorizontalVolumeFontColor.ToInt() ) );
            return settingsStorage;
        }

        internal override ChartCandleElementEx Clone( ChartCandleElementEx other )
        {
            other                               = base.Clone( other );
            other.DownFillColor                 = DownFillColor;
            other.UpFillColor                   = UpFillColor;
            other.DownBorderColor               = DownBorderColor;
            other.UpBorderColor                 = UpBorderColor;
            other.AntiAliasing                  = AntiAliasing;
            other.StrokeThickness               = StrokeThickness;
            other.LineColor                     = LineColor;
            other.AreaColor                     = AreaColor;
            other.DrawStyle                     = DrawStyle;
            other.Title                         = Title;
            other.ShowAxisMarker                = ShowAxisMarker;
            other.FontColor                     = FontColor;
            other.Timeframe2Color               = Timeframe2Color;
            other.Timeframe2FrameColor          = Timeframe2FrameColor;
            other.Timeframe3Color               = Timeframe3Color;
            other.MaxVolumeColor                = MaxVolumeColor;
            other.Timeframe2Multiplier          = Timeframe2Multiplier;
            other.Timeframe3Multiplier          = Timeframe3Multiplier;
            other.ClusterLineColor              = ClusterLineColor;
            other.ClusterTextColor              = ClusterTextColor;
            other.ClusterColor                  = ClusterColor;
            other.ClusterMaxColor               = ClusterMaxColor;
            other.ShowHorizontalVolumes         = ShowHorizontalVolumes;
            other.LocalHorizontalVolumes        = LocalHorizontalVolumes;
            other.HorizontalVolumeWidthFraction = HorizontalVolumeWidthFraction;
            other.HorizontalVolumeColor         = HorizontalVolumeColor;
            other.HorizontalVolumeFontColor     = HorizontalVolumeFontColor;
            return other;
        }

        public void CheckAndShowFibonacci( )
        {
            _viewModel.CheckAndShowFibonacci( );
        }

        ChartElementUiDomain IChartElementUiDomain.CreateViewModel( IDrawingSurfaceVM viewModel )
        {
            return _viewModel = new CandlestickVM( this );
        }

        

        public void CenterViewOnTime( DateTime selectedBarTime )
        {
            _viewModel.CenterViewOnTime( selectedBarTime );
            
        }

        
        protected override bool OnDraw( ChartDrawData data )
        {
            /* -------------------------------------------------------------------------------------------------------------------------------------------
            * 
            *  Step 7----------> 10 ChartCandleElementViewModel get the Candle from the data and start Drawing from there.
            *                           
            * ------------------------------------------------------------------------------------------------------------------------------------------- 
            */
            if ( data.HasCandleData )
            {
                var canlesData = data.GetCandle( (IChartCandleElement) this );

                if ( canlesData.IsSet )
                {
                    return StartDrawing( canlesData );
                }
            }
            
            ( ( IChartElementUiDomain ) this ).StartDrawing();
            return false;
        }

        bool StartDrawing( ChartDrawData.sCandleEx drawValues )
        {
            return _viewModel.TonyDrawSeries( drawValues );
        }

        bool IChartElementUiDomain.StartDrawing( IEnumerableEx<ChartDrawData.IDrawValue> drawValues )
        {
            return _viewModel.Draw( drawValues );
        }

        void IChartElementUiDomain.StartDrawing()
        {
            _viewModel.Draw( Enumerable.Empty<ChartDrawData.IDrawValue>().ToEx( 0 ) );
        }


    }
}
