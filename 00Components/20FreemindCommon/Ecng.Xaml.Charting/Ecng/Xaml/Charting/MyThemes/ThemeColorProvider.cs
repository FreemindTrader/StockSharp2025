// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Themes.ThemeColorProvider
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Collections;
using System.Windows;
using System.Windows.Media;
namespace fx.Xaml.Charting
{
    public class ThemeColorProvider : BindableObject, IThemeProvider
    {
        private Brush _axisMarkerAnnotationBackground;
        private Brush _axisMarkerAnnotationForeground;
        private Brush _cursorLabelBackground;
        private Brush _cursorLabelBorder;
        private Brush _cursorLabelForeground;
        private Brush _cursorLineBrush;
        private Brush _defaultColumnFillBrush;
        private Color _defaultColumnOutlineColor;
        private Color _defaultLineSeriesColor;
        private Color _downBandFillColor;
        private Color _downBandLineColor;
        private Brush _downBodyBrush;
        private Color _downWickColor;
        private Brush _gridBackgroundBrush;
        private Brush _gridBorderBrush;
        private Brush _legendBackgroundBrush;
        private Brush _majorGridLinesBrush;
        private Brush _minorGridLinesBrush;
        private Brush _mountainAreaBrush;
        private Color _mountainLineColor;
        private Brush _defaultColorMapBrush;
        private Brush _overviewFillBrush;
        private Brush _rolloverLabelBackgroundBrush;
        private Brush _rolloverLabelBorderBrush;
        private Brush _rubberBandFill;
        private Brush _rubberBandStroke;
        private Brush _ultraChartBackground;
        private Brush _textAnnotationBackground;
        private Brush _textAnnotationForeground;
        private Brush _tickTextBrush;
        private Color _upBandFillColor;
        private Color _upBandLineColor;
        private Brush _upBodyBrush;
        private Color _upWickColor;
        private Color _axisBandsFill;
        private Brush _rolloverLineStroke;
        private Brush _scrollbarFillBrush;
        private Color _boxVolumeTimeframe2Color;
        private Color _boxVolumeTimeframe2FrameColor;
        private Color _boxVolumeTimeframe3Color;
        private Color _boxVolumeCellFontColor;
        private Color _boxVolumeHighVolColor;
        private Color _clusterProfileLineColor;
        private Color _clusterProfileTextColor;
        private Color _clusterProfileClusterColor;
        private Color _clusterProfileClusterMaxColor;

        public void ApplyTheme( IThemeProvider newTheme )
        {
            InterfaceHelpers.CopyInterfaceProperties<IThemeProvider>( newTheme, ( IThemeProvider ) this );
        }

        public void ApplyTheme( ResourceDictionary dictionary )
        {
            foreach ( DictionaryEntry dictionaryEntry in dictionary )
            {
                DependencyObject element = dictionaryEntry.Value as DependencyObject;
                if ( element != null && !element.IsSealed )
                {
                    FreezeHelper.SetFreeze( element, true );
                }
            }

            GridBorderBrush = ( Brush ) dictionary[ ( object ) "GridBorderBrush" ];
            GridBackgroundBrush = ( Brush ) dictionary[ ( object ) "GridBackgroundBrush" ];
            UltrachartBackground = ( Brush ) dictionary[ ( object ) "UltrachartBackground" ];
            TickTextBrush = ( Brush ) dictionary[ ( object ) "TickTextBrush" ];
            MajorGridLinesBrush = ( Brush ) dictionary[ ( object ) "MajorGridLineBrush" ];
            MinorGridLinesBrush = ( Brush ) dictionary[ ( object ) "MinorGridLineBrush" ];
            RolloverLineStroke = ( Brush ) dictionary[ ( object ) "RolloverLineBrush" ];
            RolloverLabelBorderBrush = ( Brush ) dictionary[ ( object ) "LabelBorderBrush" ];
            RolloverLabelBackgroundBrush = ( Brush ) dictionary[ ( object ) "LabelBackgroundBrush" ];
            DefaultCandleUpWickColor = ( Color ) dictionary[ ( object ) "UpWickColor" ];
            DefaultCandleDownWickColor = ( Color ) dictionary[ ( object ) "DownWickColor" ];
            DefaultCandleUpBodyBrush = ( Brush ) dictionary[ ( object ) "UpBodyBrush" ];
            DefaultCandleDownBodyBrush = ( Brush ) dictionary[ ( object ) "DownBodyBrush" ];
            DefaultColumnOutlineColor = ( Color ) dictionary[ ( object ) "ColumnLineColor" ];
            DefaultColumnFillBrush = ( Brush ) dictionary[ ( object ) "ColumnFillBrush" ];
            DefaultLineSeriesColor = ( Color ) dictionary[ ( object ) "LineSeriesColor" ];
            DefaultMountainAreaBrush = ( Brush ) dictionary[ ( object ) "MountainAreaBrush" ];
            DefaultMountainLineColor = ( Color ) dictionary[ ( object ) "MountainLineColor" ];
            DefaultColorMapBrush = ( Brush ) dictionary[ ( object ) "DefaultColorMapBrush" ];
            DefaultDownBandFillColor = ( Color ) dictionary[ ( object ) "DownBandSeriesLineColor" ];
            DefaultUpBandFillColor = ( Color ) dictionary[ ( object ) "UpBandSeriesLineColor" ];
            DefaultUpBandLineColor = ( Color ) dictionary[ ( object ) "UpBandSeriesFillColor" ];
            DefaultDownBandLineColor = ( Color ) dictionary[ ( object ) "DownBandSeriesFillColor" ];
            RubberBandFillBrush = ( Brush ) dictionary[ ( object ) "RubberBandFillBrush" ];
            RubberBandStrokeBrush = ( Brush ) dictionary[ ( object ) "RubberBandStrokeBrush" ];
            CursorLabelForeground = ( Brush ) dictionary[ ( object ) "LabelForegroundBrush" ];
            CursorLabelBackgroundBrush = ( Brush ) dictionary[ ( object ) "LabelBackgroundBrush" ];
            CursorLabelBorderBrush = ( Brush ) dictionary[ ( object ) "LabelBorderBrush" ];
            CursorLineBrush = ( Brush ) dictionary[ ( object ) "CursorLineBrush" ];
            OverviewFillBrush = ( Brush ) dictionary[ ( object ) "OverviewFillBrush" ];
            ScrollbarFillBrush = ( Brush ) dictionary[ ( object ) "ScrollbarFillBrush" ];
            LegendBackgroundBrush = ( Brush ) dictionary[ ( object ) "LegendBackgroundBrush" ];
            DefaultTextAnnotationBackground = ( Brush ) dictionary[ ( object ) "TextAnnotationBackground" ];
            DefaultTextAnnotationForeground = ( Brush ) dictionary[ ( object ) "TextAnnotationForeground" ];
            DefaultAxisMarkerAnnotationBackground = ( Brush ) dictionary[ ( object ) "TextAnnotationBackground" ];
            DefaultAxisMarkerAnnotationForeground = ( Brush ) dictionary[ ( object ) "TextAnnotationForeground" ];
            AxisBandsFill = ( Color ) dictionary[ ( object ) "AxisBandsFill" ];
            BoxVolumeTimeframe2Color = ( Color ) dictionary[ ( object ) "BoxVolumeTimeframe2Color" ];
            BoxVolumeTimeframe2FrameColor = ( Color ) dictionary[ ( object ) "BoxVolumeTimeframe2FrameColor" ];
            BoxVolumeTimeframe3Color = ( Color ) dictionary[ ( object ) "BoxVolumeTimeframe3Color" ];
            BoxVolumeCellFontColor = ( Color ) dictionary[ ( object ) "BoxVolumeCellFontColor" ];
            BoxVolumeHighVolColor = ( Color ) dictionary[ ( object ) "BoxVolumeHighVolColor" ];
            ClusterProfileLineColor = ( Color ) dictionary[ ( object ) "ClusterProfileLineColor" ];
            ClusterProfileTextColor = ( Color ) dictionary[ ( object ) "ClusterProfileTextColor" ];
            ClusterProfileClusterColor = ( Color ) dictionary[ ( object ) "ClusterProfileClusterColor" ];
            ClusterProfileClusterMaxColor = ( Color ) dictionary[ ( object ) "ClusterProfileClusterMaxColor" ];
        }

        public Brush GridBorderBrush
        {
            get
            {
                return _gridBorderBrush;
            }
            set
            {
                if ( _gridBorderBrush == value )
                {
                    return;
                }

                _gridBorderBrush = value;
                OnPropertyChanged( nameof( GridBorderBrush ) );
            }
        }

        public Brush GridBackgroundBrush
        {
            get
            {
                return _gridBackgroundBrush;
            }
            set
            {
                if ( _gridBackgroundBrush == value )
                {
                    return;
                }

                _gridBackgroundBrush = value;
                OnPropertyChanged( nameof( GridBackgroundBrush ) );
            }
        }

        public Brush UltrachartBackground
        {
            get
            {
                return _ultraChartBackground;
            }
            set
            {
                if ( _ultraChartBackground == value )
                {
                    return;
                }

                _ultraChartBackground = value;
                OnPropertyChanged( nameof( UltrachartBackground ) );
            }
        }

        public Brush TickTextBrush
        {
            get
            {
                return _tickTextBrush;
            }
            set
            {
                if ( _tickTextBrush == value )
                {
                    return;
                }

                _tickTextBrush = value;
                OnPropertyChanged( nameof( TickTextBrush ) );
            }
        }

        public Brush MajorGridLinesBrush
        {
            get
            {
                return _majorGridLinesBrush;
            }
            set
            {
                if ( _majorGridLinesBrush == value )
                {
                    return;
                }

                _majorGridLinesBrush = value;
                OnPropertyChanged( nameof( MajorGridLinesBrush ) );
            }
        }

        public Brush MinorGridLinesBrush
        {
            get
            {
                return _minorGridLinesBrush;
            }
            set
            {
                if ( _minorGridLinesBrush == value )
                {
                    return;
                }

                _minorGridLinesBrush = value;
                OnPropertyChanged( nameof( MinorGridLinesBrush ) );
            }
        }

        public Brush RolloverLineStroke
        {
            get
            {
                return _rolloverLineStroke;
            }
            set
            {
                if ( _rolloverLineStroke == value )
                {
                    return;
                }

                _rolloverLineStroke = value;
                OnPropertyChanged( nameof( RolloverLineStroke ) );
            }
        }

        public Brush RolloverLabelBorderBrush
        {
            get
            {
                return _rolloverLabelBorderBrush;
            }
            set
            {
                if ( _rolloverLabelBorderBrush == value )
                {
                    return;
                }

                _rolloverLabelBorderBrush = value;
                OnPropertyChanged( nameof( RolloverLabelBorderBrush ) );
            }
        }

        public Brush RolloverLabelBackgroundBrush
        {
            get
            {
                return _rolloverLabelBackgroundBrush;
            }
            set
            {
                if ( _rolloverLabelBackgroundBrush == value )
                {
                    return;
                }

                _rolloverLabelBackgroundBrush = value;
                OnPropertyChanged( nameof( RolloverLabelBackgroundBrush ) );
            }
        }

        public Color DefaultCandleUpWickColor
        {
            get
            {
                return _upWickColor;
            }
            set
            {
                if ( !( _upWickColor != value ) )
                {
                    return;
                }

                _upWickColor = value;
                OnPropertyChanged( nameof( DefaultCandleUpWickColor ) );
            }
        }

        public Color DefaultCandleDownWickColor
        {
            get
            {
                return _downWickColor;
            }
            set
            {
                if ( !( _downWickColor != value ) )
                {
                    return;
                }

                _downWickColor = value;
                OnPropertyChanged( nameof( DefaultCandleDownWickColor ) );
            }
        }

        public Brush DefaultCandleUpBodyBrush
        {
            get
            {
                return _upBodyBrush;
            }
            set
            {
                if ( _upBodyBrush == value )
                {
                    return;
                }

                _upBodyBrush = value;
                OnPropertyChanged( nameof( DefaultCandleUpBodyBrush ) );
            }
        }

        public Brush DefaultCandleDownBodyBrush
        {
            get
            {
                return _downBodyBrush;
            }
            set
            {
                if ( _downBodyBrush == value )
                {
                    return;
                }

                _downBodyBrush = value;
                OnPropertyChanged( nameof( DefaultCandleDownBodyBrush ) );
            }
        }

        public Color DefaultColumnOutlineColor
        {
            get
            {
                return _defaultColumnOutlineColor;
            }
            set
            {
                if ( !( _defaultColumnOutlineColor != value ) )
                {
                    return;
                }

                _defaultColumnOutlineColor = value;
                OnPropertyChanged( nameof( DefaultColumnOutlineColor ) );
            }
        }

        public Brush DefaultColumnFillBrush
        {
            get
            {
                return _defaultColumnFillBrush;
            }
            set
            {
                if ( _defaultColumnFillBrush == value )
                {
                    return;
                }

                _defaultColumnFillBrush = value;
                OnPropertyChanged( nameof( DefaultColumnFillBrush ) );
            }
        }

        public Color DefaultLineSeriesColor
        {
            get
            {
                return _defaultLineSeriesColor;
            }
            set
            {
                if ( !( _defaultLineSeriesColor != value ) )
                {
                    return;
                }

                _defaultLineSeriesColor = value;
                OnPropertyChanged( nameof( DefaultLineSeriesColor ) );
            }
        }

        public Color DefaultMountainLineColor
        {
            get
            {
                return _mountainLineColor;
            }
            set
            {
                if ( !( _mountainLineColor != value ) )
                {
                    return;
                }

                _mountainLineColor = value;
                OnPropertyChanged( nameof( DefaultMountainLineColor ) );
            }
        }

        public Brush DefaultMountainAreaBrush
        {
            get
            {
                return _mountainAreaBrush;
            }
            set
            {
                if ( _mountainAreaBrush == value )
                {
                    return;
                }

                _mountainAreaBrush = value;
                OnPropertyChanged( nameof( DefaultMountainAreaBrush ) );
            }
        }

        public Brush DefaultColorMapBrush
        {
            get
            {
                return _defaultColorMapBrush;
            }
            set
            {
                if ( _defaultColorMapBrush == value )
                {
                    return;
                }

                _defaultColorMapBrush = value;
                OnPropertyChanged( nameof( DefaultColorMapBrush ) );
            }
        }

        public Color DefaultDownBandFillColor
        {
            get
            {
                return _downBandFillColor;
            }
            set
            {
                if ( !( _downBandFillColor != value ) )
                {
                    return;
                }

                _downBandFillColor = value;
                OnPropertyChanged( nameof( DefaultDownBandFillColor ) );
            }
        }

        public Color DefaultUpBandFillColor
        {
            get
            {
                return _upBandFillColor;
            }
            set
            {
                if ( !( _upBandFillColor != value ) )
                {
                    return;
                }

                _upBandFillColor = value;
                OnPropertyChanged( nameof( DefaultUpBandFillColor ) );
            }
        }

        public Color DefaultUpBandLineColor
        {
            get
            {
                return _upBandLineColor;
            }
            set
            {
                if ( !( _upBandLineColor != value ) )
                {
                    return;
                }

                _upBandLineColor = value;
                OnPropertyChanged( nameof( DefaultUpBandLineColor ) );
            }
        }

        public Color DefaultDownBandLineColor
        {
            get
            {
                return _downBandLineColor;
            }
            set
            {
                if ( !( _downBandLineColor != value ) )
                {
                    return;
                }

                _downBandLineColor = value;
                OnPropertyChanged( nameof( DefaultDownBandLineColor ) );
            }
        }

        public Brush CursorLabelForeground
        {
            get
            {
                return _cursorLabelForeground;
            }
            set
            {
                if ( _cursorLabelForeground == value )
                {
                    return;
                }

                _cursorLabelForeground = value;
                OnPropertyChanged( nameof( CursorLabelForeground ) );
            }
        }

        public Brush CursorLabelBackgroundBrush
        {
            get
            {
                return _cursorLabelBackground;
            }
            set
            {
                if ( _cursorLabelBackground == value )
                {
                    return;
                }

                _cursorLabelBackground = value;
                OnPropertyChanged( nameof( CursorLabelBackgroundBrush ) );
            }
        }

        public Brush CursorLabelBorderBrush
        {
            get
            {
                return _cursorLabelBorder;
            }
            set
            {
                if ( _cursorLabelBorder == value )
                {
                    return;
                }

                _cursorLabelBorder = value;
                OnPropertyChanged( nameof( CursorLabelBorderBrush ) );
            }
        }

        public Brush RubberBandFillBrush
        {
            get
            {
                return _rubberBandFill;
            }
            set
            {
                if ( _rubberBandFill == value )
                {
                    return;
                }

                _rubberBandFill = value;
                OnPropertyChanged( nameof( RubberBandFillBrush ) );
            }
        }

        public Brush RubberBandStrokeBrush
        {
            get
            {
                return _rubberBandStroke;
            }
            set
            {
                if ( _rubberBandStroke == value )
                {
                    return;
                }

                _rubberBandStroke = value;
                OnPropertyChanged( nameof( RubberBandStrokeBrush ) );
            }
        }

        public Brush CursorLineBrush
        {
            get
            {
                return _cursorLineBrush;
            }
            set
            {
                if ( _cursorLineBrush == value )
                {
                    return;
                }

                _cursorLineBrush = value;
                OnPropertyChanged( nameof( CursorLineBrush ) );
            }
        }

        public Brush OverviewFillBrush
        {
            get
            {
                return _overviewFillBrush;
            }
            set
            {
                if ( _overviewFillBrush == value )
                {
                    return;
                }

                _overviewFillBrush = value;
                OnPropertyChanged( nameof( OverviewFillBrush ) );
            }
        }

        public Brush ScrollbarFillBrush
        {
            get
            {
                return _scrollbarFillBrush;
            }
            set
            {
                if ( _scrollbarFillBrush == value )
                {
                    return;
                }

                _scrollbarFillBrush = value;
                OnPropertyChanged( nameof( ScrollbarFillBrush ) );
            }
        }

        public Brush LegendBackgroundBrush
        {
            get
            {
                return _legendBackgroundBrush;
            }
            set
            {
                if ( _legendBackgroundBrush == value )
                {
                    return;
                }

                _legendBackgroundBrush = value;
                OnPropertyChanged( nameof( LegendBackgroundBrush ) );
            }
        }

        public Brush DefaultTextAnnotationBackground
        {
            get
            {
                return _textAnnotationBackground;
            }
            set
            {
                if ( _textAnnotationBackground == value )
                {
                    return;
                }

                _textAnnotationBackground = value;
                OnPropertyChanged( nameof( DefaultTextAnnotationBackground ) );
            }
        }

        public Brush DefaultTextAnnotationForeground
        {
            get
            {
                return _textAnnotationForeground;
            }
            set
            {
                if ( _textAnnotationForeground == value )
                {
                    return;
                }

                _textAnnotationForeground = value;
                OnPropertyChanged( nameof( DefaultTextAnnotationForeground ) );
            }
        }

        public Brush DefaultAxisMarkerAnnotationBackground
        {
            get
            {
                return _axisMarkerAnnotationBackground;
            }
            set
            {
                if ( _axisMarkerAnnotationBackground == value )
                {
                    return;
                }

                _axisMarkerAnnotationBackground = value;
                OnPropertyChanged( nameof( DefaultAxisMarkerAnnotationBackground ) );
            }
        }

        public Brush DefaultAxisMarkerAnnotationForeground
        {
            get
            {
                return _axisMarkerAnnotationForeground;
            }
            set
            {
                if ( _axisMarkerAnnotationForeground == value )
                {
                    return;
                }

                _axisMarkerAnnotationForeground = value;
                OnPropertyChanged( nameof( DefaultAxisMarkerAnnotationForeground ) );
            }
        }

        public Color AxisBandsFill
        {
            get
            {
                return _axisBandsFill;
            }
            set
            {
                if ( !( _axisBandsFill != value ) )
                {
                    return;
                }

                _axisBandsFill = value;
                OnPropertyChanged( nameof( AxisBandsFill ) );
            }
        }

        public Color BoxVolumeTimeframe2Color
        {
            get
            {
                return _boxVolumeTimeframe2Color;
            }
            set
            {
                SetField<Color>( ref _boxVolumeTimeframe2Color, value, nameof( BoxVolumeTimeframe2Color ) );
            }
        }

        public Color BoxVolumeTimeframe2FrameColor
        {
            get
            {
                return _boxVolumeTimeframe2FrameColor;
            }
            set
            {
                SetField<Color>( ref _boxVolumeTimeframe2FrameColor, value, nameof( BoxVolumeTimeframe2FrameColor ) );
            }
        }

        public Color BoxVolumeTimeframe3Color
        {
            get
            {
                return _boxVolumeTimeframe3Color;
            }
            set
            {
                SetField<Color>( ref _boxVolumeTimeframe3Color, value, nameof( BoxVolumeTimeframe3Color ) );
            }
        }

        public Color BoxVolumeCellFontColor
        {
            get
            {
                return _boxVolumeCellFontColor;
            }
            set
            {
                SetField<Color>( ref _boxVolumeCellFontColor, value, nameof( BoxVolumeCellFontColor ) );
            }
        }

        public Color BoxVolumeHighVolColor
        {
            get
            {
                return _boxVolumeHighVolColor;
            }
            set
            {
                SetField<Color>( ref _boxVolumeHighVolColor, value, nameof( BoxVolumeHighVolColor ) );
            }
        }

        public Color ClusterProfileLineColor
        {
            get
            {
                return _clusterProfileLineColor;
            }
            set
            {
                SetField<Color>( ref _clusterProfileLineColor, value, nameof( ClusterProfileLineColor ) );
            }
        }

        public Color ClusterProfileTextColor
        {
            get
            {
                return _clusterProfileTextColor;
            }
            set
            {
                SetField<Color>( ref _clusterProfileTextColor, value, nameof( ClusterProfileTextColor ) );
            }
        }

        public Color ClusterProfileClusterColor
        {
            get
            {
                return _clusterProfileClusterColor;
            }
            set
            {
                SetField<Color>( ref _clusterProfileClusterColor, value, nameof( ClusterProfileClusterColor ) );
            }
        }

        public Color ClusterProfileClusterMaxColor
        {
            get
            {
                return _clusterProfileClusterMaxColor;
            }
            set
            {
                SetField<Color>( ref _clusterProfileClusterMaxColor, value, nameof( ClusterProfileClusterMaxColor ) );
            }
        }
    }
}
