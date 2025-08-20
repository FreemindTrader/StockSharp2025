using SciChart.Charting.Themes;
using SciChart.Data.Model;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace StockSharp.Xaml.Charting;
#nullable disable
public class ThemeColorProviderEx : ThemeColorProvider,
                                    IThemeProviderEx
{
    protected bool OnSetPropertyChanged<T>(ref T objectOne, T objectTwo, string propertyName)
    {
        if ( EqualityComparer<T>.Default.Equals(objectOne, objectTwo) )
            return false;

        objectOne = objectTwo;

        OnPropertyChanged(propertyName);

        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    public Brush ScrollbarFillBrush
    {
        get => _scrollbarFillBrush;
        set
        {
            if ( _scrollbarFillBrush == value )
                return;
            _scrollbarFillBrush = value;
            OnPropertyChanged(nameof(ScrollbarFillBrush));
        }
    }

    /// <summary>
    /// Color for the second timeframe in the box volume indicator.
    /// </summary>
    public Color BoxVolumeTimeframe2Color
    {
        get => _boxVolumeTimeframe2Color;
        set
        {
            OnSetPropertyChanged<Color>(ref _boxVolumeTimeframe2Color, value, nameof(BoxVolumeTimeframe2Color));
        }
    }

    public Color BoxVolumeTimeframe2FrameColor
    {
        get => _boxVolumeTimeframe2FrameColor;
        set
        {
            OnSetPropertyChanged<Color>(ref _boxVolumeTimeframe2FrameColor, value, nameof(BoxVolumeTimeframe2FrameColor));
        }
    }

    public Color BoxVolumeTimeframe3Color
    {
        get => _boxVolumeTimeframe3Color;
        set
        {
            OnSetPropertyChanged<Color>(ref _boxVolumeTimeframe3Color, value, nameof(BoxVolumeTimeframe3Color));
        }
    }

    public Color BoxVolumeHighVolColor
    {
        get => _boxVolumeHighVolColor;
        set
        {
            OnSetPropertyChanged<Color>(ref _boxVolumeHighVolColor, value, nameof(BoxVolumeHighVolColor));
        }
    }

    public Color ClusterProfileLineColor
    {
        get => _clusterProfileLineColor;
        set
        {
            OnSetPropertyChanged<Color>(ref _clusterProfileLineColor, value, nameof(ClusterProfileLineColor));
        }
    }

    public Color ClusterProfileTextColor
    {
        get => _clusterProfileTextColor;
        set
        {
            OnSetPropertyChanged<Color>(ref _clusterProfileTextColor, value, nameof(ClusterProfileTextColor));
        }
    }

    public Color ClusterProfileClusterColor
    {
        get => _clusterProfileClusterColor;
        set
        {
            OnSetPropertyChanged<Color>(ref _clusterProfileClusterColor, value, nameof(ClusterProfileClusterColor));
        }
    }

    public Color ClusterProfileClusterMaxColor
    {
        get => _clusterProfileClusterMaxColor;
        set
        {
            OnSetPropertyChanged<Color>(ref _clusterProfileClusterMaxColor, value, nameof(ClusterProfileClusterMaxColor));
        }
    }

    public Color BoxVolumeCellFontColor
    {
        get => _boxVolumeCellFontColor;
        set
        {
            OnSetPropertyChanged<Color>(ref _boxVolumeCellFontColor, value, nameof(BoxVolumeCellFontColor));
        }
    }



    public Color ClusterProfileSeparatorLineColor
    {
        get => _clusterProfileSeparatorLineColor;
        set
        {
            OnSetPropertyChanged<Color>(ref _clusterProfileSeparatorLineColor, value, nameof(ClusterProfileSeparatorLineColor));
        }
    }

    private Brush _defaultAxisMarkerAnnotationBackground;

    private Brush _defaultAxisMarkerAnnotationForeground;

    private Brush _cursorLabelBackgroundBrush;

    private Brush _cursorLabelBorderBrush;

    private Brush _cursorLabelForeground;

    private Brush _cursorLineBrush;

    private Brush _defaultColumnFillBrush;

    private Color _defaultColumnOutlineColor;

    private Color _defaultLineSeriesColor;

    private Color _defaultDownBandFillColor;

    private Color _defaultDownBandLineColor;

    private Brush _defaultCandleDownBodyBrush;

    private Color _defaultCandleDownWickColor;

    private Brush _gridBackgroundBrush;

    private Brush _gridBorderBrush;

    private Brush _legendBackgroundBrush;

    private Brush _majorGridLinesBrush;

    private Brush _minorGridLinesBrush;

    private Brush _defaultMountainAreaBrush;

    private Color _defaultMountainLineColor;

    private Brush _defaultColorMapBrush;

    private Brush _overviewFillBrush;

    private Brush _rolloverLabelBackgroundBrush;

    private Brush _rolloverLabelBorderBrush;

    private Brush _rubberBandFillBrush;

    private Brush _rubberBandStrokeBrush;

    private Brush _sciChartBackground;

    private Brush _defaultTextAnnotationBackground;

    private Brush _defaultTextAnnotationForeground;

    private Brush _tickTextBrush;

    private Color _defaultUpBandFillColor;

    private Color _defaultUpBandLineColor;

    private Brush _defaultCandleUpBodyBrush;

    private Color _defaultCandleUpWickColor;

    private Color _axisBandsFill;

    private Brush _rolloverLineStroke;

    private Brush _scrollbarFillBrush;

    private Color _boxVolumeTimeframe2Color;

    private Color _boxVolumeTimeframe2FrameColor;

    private Color _boxVolumeTimeframe3Color;

    private Color _boxVolumeCellFontColor;

    private Color _boxVolumeHighVolColor;

    private Color _clusterProfileLineColor;

    private Color _clusterProfileSeparatorLineColor;

    private Color _clusterProfileTextColor;

    private Color _clusterProfileClusterColor;

    private Color _clusterProfileClusterMaxColor;

    public new void ApplyTheme(IThemeProvider _param1)
    {
        InterfaceHelpers2025.CopyInterfaceProperties<IThemeProvider>(_param1, (IThemeProvider)this);
    }

    public new void ApplyTheme(ResourceDictionary rc)
    {
        foreach ( DictionaryEntry dictionaryEntry in rc )
        {
            if ( dictionaryEntry.Value is DependencyObject dependencyObject && !dependencyObject.IsSealed )
            {
                FreezeHelper2025.SetFreeze(dependencyObject, true);
            }

        }
        GridBorderBrush = (Brush)rc["GridBorderBrush"];
        GridBackgroundBrush = (Brush)rc["GridBackgroundBrush"];
        SciChartBackground = (Brush)rc["SciChartBackground"];
        TickTextBrush = (Brush)rc["TickTextBrush"];
        MajorGridLinesBrush = (Brush)rc["MajorGridLineBrush"];
        MinorGridLinesBrush = (Brush)rc["MinorGridLineBrush"];
        RolloverLineStroke = (Brush)rc["RolloverLineBrush"];
        RolloverLabelBorderBrush = (Brush)rc["LabelBorderBrush"];
        RolloverLabelBackgroundBrush = (Brush)rc["LabelBackgroundBrush"];
        DefaultCandleUpWickColor = (Color)rc["UpWickColor"];
        DefaultCandleDownWickColor = (Color)rc["DownWickColor"];
        DefaultCandleUpBodyBrush = (Brush)rc["UpBodyBrush"];
        DefaultCandleDownBodyBrush = (Brush)rc["DownBodyBrush"];
        DefaultColumnOutlineColor = (Color)rc["ColumnLineColor"];
        DefaultColumnFillBrush = (Brush)rc["ColumnFillBrush"];
        DefaultLineSeriesColor = (Color)rc["LineSeriesColor"];
        DefaultMountainAreaBrush = (Brush)rc["MountainAreaBrush"];
        DefaultMountainLineColor = (Color)rc["MountainLineColor"];
        DefaultColorMapBrush = (Brush)rc["DefaultColorMapBrush"];
        DefaultDownBandFillColor = (Color)rc["DownBandSeriesLineColor"];
        DefaultUpBandFillColor = (Color)rc["UpBandSeriesLineColor"];
        DefaultUpBandLineColor = (Color)rc["UpBandSeriesFillColor"];
        DefaultDownBandLineColor = (Color)rc["DownBandSeriesFillColor"];
        RubberBandFillBrush = (Brush)rc["RubberBandFillBrush"];
        RubberBandStrokeBrush = (Brush)rc["RubberBandStrokeBrush"];
        CursorLabelForeground = (Brush)rc["LabelForegroundBrush"];
        CursorLabelBackgroundBrush = (Brush)rc["LabelBackgroundBrush"];
        CursorLabelBorderBrush = (Brush)rc["LabelBorderBrush"];
        CursorLineBrush = (Brush)rc["CursorLineBrush"];
        OverviewFillBrush = (Brush)rc["OverviewFillBrush"];
        ScrollbarFillBrush = (Brush)rc["ScrollbarFillBrush"];
        LegendBackgroundBrush = (Brush)rc["LegendBackgroundBrush"];
        DefaultTextAnnotationBackground = (Brush)rc["TextAnnotationBackground"];
        DefaultTextAnnotationForeground = (Brush)rc["TextAnnotationForeground"];
        DefaultAxisMarkerAnnotationBackground = (Brush)rc["TextAnnotationBackground"];
        DefaultAxisMarkerAnnotationForeground = (Brush)rc["TextAnnotationForeground"];
        AxisBandsFill = (Color)rc["AxisBandsFill"];
        BoxVolumeTimeframe2Color = (Color)rc["BoxVolumeTimeframe2Color"];
        BoxVolumeTimeframe2FrameColor = (Color)rc["BoxVolumeTimeframe2FrameColor"];
        BoxVolumeTimeframe3Color = (Color)rc["BoxVolumeTimeframe3Color"];
        BoxVolumeCellFontColor = (Color)rc["BoxVolumeCellFontColor"];
        BoxVolumeHighVolColor = (Color)rc["BoxVolumeHighVolColor"];
        ClusterProfileSeparatorLineColor = (Color)rc["ClusterProfileSeparatorLineColor"];
        ClusterProfileLineColor = (Color)rc["ClusterProfileLineColor"];
        ClusterProfileTextColor = (Color)rc["ClusterProfileTextColor"];
        ClusterProfileClusterColor = (Color)rc["ClusterProfileClusterColor"];
        ClusterProfileClusterMaxColor = (Color)rc["ClusterProfileClusterMaxColor"];
    }

    //public Brush GridBorderBrush
    //{
    //    get => _gridBorderBrush;
    //    set
    //    {
    //        if ( _gridBorderBrush == value )
    //            return;
    //        _gridBorderBrush = value;
    //        OnPropertyChanged(nameof(GridBorderBrush));
    //    }
    //}

    //public Brush GridBackgroundBrush
    //{
    //    get => _gridBackgroundBrush;
    //    set
    //    {
    //        if ( _gridBackgroundBrush == value )
    //            return;
    //        _gridBackgroundBrush = value;
    //        OnPropertyChanged(nameof(GridBackgroundBrush));
    //    }
    //}

    //public Brush SciChartBackground
    //{
    //    get => _sciChartBackground;
    //    set
    //    {
    //        if ( _sciChartBackground == value )
    //            return;
    //        _sciChartBackground = value;
    //        OnPropertyChanged(nameof(SciChartBackground));
    //    }
    //}

    //public Brush TickTextBrush
    //{
    //    get => _tickTextBrush;
    //    set
    //    {
    //        if ( _tickTextBrush == value )
    //            return;
    //        _tickTextBrush = value;
    //        OnPropertyChanged(nameof(TickTextBrush));
    //    }
    //}

    //public Brush MajorGridLinesBrush
    //{
    //    get => _majorGridLinesBrush;
    //    set
    //    {
    //        if ( _majorGridLinesBrush == value )
    //            return;
    //        _majorGridLinesBrush = value;
    //        OnPropertyChanged(nameof(MajorGridLinesBrush));
    //    }
    //}

    //public Brush MinorGridLinesBrush
    //{
    //    get => _minorGridLinesBrush;
    //    set
    //    {
    //        if ( _minorGridLinesBrush == value )
    //            return;
    //        _minorGridLinesBrush = value;
    //        OnPropertyChanged(nameof(MinorGridLinesBrush));
    //    }
    //}

    //public Brush RolloverLineStroke
    //{
    //    get => _rolloverLineStroke;
    //    set
    //    {
    //        if ( _rolloverLineStroke == value )
    //            return;
    //        _rolloverLineStroke = value;
    //        OnPropertyChanged(nameof(RolloverLineStroke));
    //    }
    //}

    //public Brush RolloverLabelBorderBrush
    //{
    //    get => _rolloverLabelBorderBrush;
    //    set
    //    {
    //        if ( _rolloverLabelBorderBrush == value )
    //            return;
    //        _rolloverLabelBorderBrush = value;
    //        OnPropertyChanged(nameof(RolloverLabelBorderBrush));
    //    }
    //}

    //public Brush RolloverLabelBackgroundBrush
    //{
    //    get => _rolloverLabelBackgroundBrush;
    //    set
    //    {
    //        if ( _rolloverLabelBackgroundBrush == value )
    //            return;
    //        _rolloverLabelBackgroundBrush = value;
    //        OnPropertyChanged(nameof(RolloverLabelBackgroundBrush));
    //    }
    //}

    //public Color DefaultCandleUpWickColor
    //{
    //    get => _defaultCandleUpWickColor;
    //    set
    //    {
    //        if ( !( _defaultCandleUpWickColor != value ) )
    //            return;
    //        _defaultCandleUpWickColor = value;
    //        OnPropertyChanged(nameof(DefaultCandleUpWickColor));
    //    }
    //}

    //public Color DefaultCandleDownWickColor
    //{
    //    get => _defaultCandleDownWickColor;
    //    set
    //    {
    //        if ( !( _defaultCandleDownWickColor != value ) )
    //            return;
    //        _defaultCandleDownWickColor = value;
    //        OnPropertyChanged(nameof(DefaultCandleDownWickColor));
    //    }
    //}

    //public Brush DefaultCandleUpBodyBrush
    //{
    //    get => _defaultCandleUpBodyBrush;
    //    set
    //    {
    //        if ( _defaultCandleUpBodyBrush == value )
    //            return;
    //        _defaultCandleUpBodyBrush = value;
    //        OnPropertyChanged(nameof(DefaultCandleUpBodyBrush));
    //    }
    //}

    //public Brush DefaultCandleDownBodyBrush
    //{
    //    get => _defaultCandleDownBodyBrush;
    //    set
    //    {
    //        if ( _defaultCandleDownBodyBrush == value )
    //            return;
    //        _defaultCandleDownBodyBrush = value;
    //        OnPropertyChanged(nameof(DefaultCandleDownBodyBrush));
    //    }
    //}

    //public Color DefaultColumnOutlineColor
    //{
    //    get => _defaultColumnOutlineColor;
    //    set
    //    {
    //        if ( !( _defaultColumnOutlineColor != value ) )
    //            return;
    //        _defaultColumnOutlineColor = value;
    //        OnPropertyChanged(nameof(DefaultColumnOutlineColor));
    //    }
    //}

    //public Brush DefaultColumnFillBrush
    //{
    //    get => _defaultColumnFillBrush;
    //    set
    //    {
    //        if ( _defaultColumnFillBrush == value )
    //            return;
    //        _defaultColumnFillBrush = value;
    //        OnPropertyChanged(nameof(DefaultColumnFillBrush));
    //    }
    //}

    //public Color DefaultLineSeriesColor
    //{
    //    get => _defaultLineSeriesColor;
    //    set
    //    {
    //        if ( !( _defaultLineSeriesColor != value ) )
    //            return;
    //        _defaultLineSeriesColor = value;
    //        OnPropertyChanged(nameof(DefaultLineSeriesColor));
    //    }
    //}

    //public Color DefaultMountainLineColor
    //{
    //    get => _defaultMountainLineColor;
    //    set
    //    {
    //        if ( !( _defaultMountainLineColor != value ) )
    //            return;
    //        _defaultMountainLineColor = value;
    //        OnPropertyChanged(nameof(DefaultMountainLineColor));
    //    }
    //}

    //public Brush DefaultMountainAreaBrush
    //{
    //    get => _defaultMountainAreaBrush;
    //    set
    //    {
    //        if ( _defaultMountainAreaBrush == value )
    //            return;
    //        _defaultMountainAreaBrush = value;
    //        OnPropertyChanged(nameof(DefaultMountainAreaBrush));
    //    }
    //}

    //public Brush DefaultColorMapBrush
    //{
    //    get => _defaultColorMapBrush;
    //    set
    //    {
    //        if ( _defaultColorMapBrush == value )
    //            return;
    //        _defaultColorMapBrush = value;
    //        OnPropertyChanged(nameof(DefaultColorMapBrush));
    //    }
    //}

    //public Color DefaultDownBandFillColor
    //{
    //    get => _defaultDownBandFillColor;
    //    set
    //    {
    //        if ( !( _defaultDownBandFillColor != value ) )
    //            return;
    //        _defaultDownBandFillColor = value;
    //        OnPropertyChanged(nameof(DefaultDownBandFillColor));
    //    }
    //}

    //public Color DefaultUpBandFillColor
    //{
    //    get => _defaultUpBandFillColor;
    //    set
    //    {
    //        if ( !( _defaultUpBandFillColor != value ) )
    //            return;
    //        _defaultUpBandFillColor = value;
    //        OnPropertyChanged(nameof(DefaultUpBandFillColor));
    //    }
    //}

    //public Color DefaultUpBandLineColor
    //{
    //    get => _defaultUpBandLineColor;
    //    set
    //    {
    //        if ( !( _defaultUpBandLineColor != value ) )
    //            return;
    //        _defaultUpBandLineColor = value;
    //        OnPropertyChanged(nameof(DefaultUpBandLineColor));
    //    }
    //}

    //public Color DefaultDownBandLineColor
    //{
    //    get => _defaultDownBandLineColor;
    //    set
    //    {
    //        if ( !( _defaultDownBandLineColor != value ) )
    //            return;
    //        _defaultDownBandLineColor = value;
    //        OnPropertyChanged(nameof(DefaultDownBandLineColor));
    //    }
    //}

    //public Brush CursorLabelForeground
    //{
    //    get => _cursorLabelForeground;
    //    set
    //    {
    //        if ( _cursorLabelForeground == value )
    //            return;
    //        _cursorLabelForeground = value;
    //        OnPropertyChanged(nameof(CursorLabelForeground));
    //    }
    //}

    //public Brush CursorLabelBackgroundBrush
    //{
    //    get => _cursorLabelBackgroundBrush;
    //    set
    //    {
    //        if ( _cursorLabelBackgroundBrush == value )
    //            return;
    //        _cursorLabelBackgroundBrush = value;
    //        OnPropertyChanged(nameof(CursorLabelBackgroundBrush));
    //    }
    //}

    //public Brush CursorLabelBorderBrush
    //{
    //    get => _cursorLabelBorderBrush;
    //    set
    //    {
    //        if ( _cursorLabelBorderBrush == value )
    //            return;
    //        _cursorLabelBorderBrush = value;
    //        OnPropertyChanged(nameof(CursorLabelBorderBrush));
    //    }
    //}

    //public Brush RubberBandFillBrush
    //{
    //    get => _rubberBandFillBrush;
    //    set
    //    {
    //        if ( _rubberBandFillBrush == value )
    //            return;
    //        _rubberBandFillBrush = value;
    //        OnPropertyChanged(nameof(RubberBandFillBrush));
    //    }
    //}

    //public Brush RubberBandStrokeBrush
    //{
    //    get => _rubberBandStrokeBrush;
    //    set
    //    {
    //        if ( _rubberBandStrokeBrush == value )
    //            return;
    //        _rubberBandStrokeBrush = value;
    //        OnPropertyChanged(nameof(RubberBandStrokeBrush));
    //    }
    //}

    //public Brush CursorLineBrush
    //{
    //    get => _cursorLineBrush;
    //    set
    //    {
    //        if ( _cursorLineBrush == value )
    //            return;
    //        _cursorLineBrush = value;
    //        OnPropertyChanged(nameof(CursorLineBrush));
    //    }
    //}

    //public Brush OverviewFillBrush
    //{
    //    get => _overviewFillBrush;
    //    set
    //    {
    //        if ( _overviewFillBrush == value )
    //            return;
    //        _overviewFillBrush = value;
    //        OnPropertyChanged(nameof(OverviewFillBrush));
    //    }
    //}



    //public Brush LegendBackgroundBrush
    //{
    //    get => _legendBackgroundBrush;
    //    set
    //    {
    //        if ( _legendBackgroundBrush == value )
    //            return;
    //        _legendBackgroundBrush = value;
    //        OnPropertyChanged(nameof(LegendBackgroundBrush));
    //    }
    //}

    //public Brush DefaultTextAnnotationBackground
    //{
    //    get => _defaultTextAnnotationBackground;
    //    set
    //    {
    //        if ( _defaultTextAnnotationBackground == value )
    //            return;
    //        _defaultTextAnnotationBackground = value;
    //        OnPropertyChanged(nameof(DefaultTextAnnotationBackground));
    //    }
    //}

    //public Brush DefaultTextAnnotationForeground
    //{
    //    get => _defaultTextAnnotationForeground;
    //    set
    //    {
    //        if ( _defaultTextAnnotationForeground == value )
    //            return;
    //        _defaultTextAnnotationForeground = value;
    //        OnPropertyChanged(nameof(DefaultTextAnnotationForeground));
    //    }
    //}

    //public Brush DefaultAxisMarkerAnnotationBackground
    //{
    //    get => _defaultAxisMarkerAnnotationBackground;
    //    set
    //    {
    //        if ( _defaultAxisMarkerAnnotationBackground == value )
    //            return;
    //        _defaultAxisMarkerAnnotationBackground = value;
    //        OnPropertyChanged(nameof(DefaultAxisMarkerAnnotationBackground));
    //    }
    //}

    //public Brush DefaultAxisMarkerAnnotationForeground
    //{
    //    get => _defaultAxisMarkerAnnotationForeground;
    //    set
    //    {
    //        if ( _defaultAxisMarkerAnnotationForeground == value )
    //            return;
    //        _defaultAxisMarkerAnnotationForeground = value;
    //        OnPropertyChanged(nameof(DefaultAxisMarkerAnnotationForeground));
    //    }
    //}

    //public Color AxisBandsFill
    //{
    //    get => _axisBandsFill;
    //    set
    //    {
    //        if ( !( _axisBandsFill != value ) )
    //            return;
    //        _axisBandsFill = value;
    //        OnPropertyChanged(nameof(AxisBandsFill));
    //    }
    //}







    

    
}
