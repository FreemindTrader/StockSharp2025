// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Themes.IThemeProvider
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Windows;
using System.Windows.Media;

namespace fx.Xaml.Charting
{
    public interface IThemeProvider
    {
        Brush GridBorderBrush
        {
            get; set;
        }

        Brush GridBackgroundBrush
        {
            get; set;
        }

        Brush UltrachartBackground
        {
            get; set;
        }

        Brush TickTextBrush
        {
            get; set;
        }

        Brush MajorGridLinesBrush
        {
            get; set;
        }

        Brush MinorGridLinesBrush
        {
            get; set;
        }

        Brush RolloverLineStroke
        {
            get; set;
        }

        Brush RolloverLabelBorderBrush
        {
            get; set;
        }

        Brush RolloverLabelBackgroundBrush
        {
            get; set;
        }

        Color DefaultCandleUpWickColor
        {
            get; set;
        }

        Color DefaultCandleDownWickColor
        {
            get; set;
        }

        Brush DefaultCandleUpBodyBrush
        {
            get; set;
        }

        Brush DefaultCandleDownBodyBrush
        {
            get; set;
        }

        Color DefaultColumnOutlineColor
        {
            get; set;
        }

        Brush DefaultColumnFillBrush
        {
            get; set;
        }

        Color DefaultLineSeriesColor
        {
            get; set;
        }

        Color DefaultMountainLineColor
        {
            get; set;
        }

        Color DefaultDownBandFillColor
        {
            get; set;
        }

        Color DefaultUpBandFillColor
        {
            get; set;
        }

        Color DefaultUpBandLineColor
        {
            get; set;
        }

        Color DefaultDownBandLineColor
        {
            get; set;
        }

        Brush DefaultMountainAreaBrush
        {
            get; set;
        }

        Brush DefaultColorMapBrush
        {
            get; set;
        }

        Brush CursorLabelForeground
        {
            get; set;
        }

        Brush CursorLabelBackgroundBrush
        {
            get; set;
        }

        Brush CursorLabelBorderBrush
        {
            get; set;
        }

        Brush RubberBandFillBrush
        {
            get; set;
        }

        Brush RubberBandStrokeBrush
        {
            get; set;
        }

        Brush CursorLineBrush
        {
            get; set;
        }

        Brush OverviewFillBrush
        {
            get; set;
        }

        Brush ScrollbarFillBrush
        {
            get; set;
        }

        Brush LegendBackgroundBrush
        {
            get; set;
        }

        Brush DefaultTextAnnotationBackground
        {
            get; set;
        }

        Brush DefaultTextAnnotationForeground
        {
            get; set;
        }

        Brush DefaultAxisMarkerAnnotationBackground
        {
            get; set;
        }

        Brush DefaultAxisMarkerAnnotationForeground
        {
            get; set;
        }

        Color AxisBandsFill
        {
            get; set;
        }

        Color BoxVolumeTimeframe2Color
        {
            get; set;
        }

        Color BoxVolumeTimeframe2FrameColor
        {
            get; set;
        }

        Color BoxVolumeTimeframe3Color
        {
            get; set;
        }

        Color BoxVolumeCellFontColor
        {
            get; set;
        }

        Color BoxVolumeHighVolColor
        {
            get; set;
        }

        Color ClusterProfileLineColor
        {
            get; set;
        }

        Color ClusterProfileTextColor
        {
            get; set;
        }

        Color ClusterProfileClusterColor
        {
            get; set;
        }

        Color ClusterProfileClusterMaxColor
        {
            get; set;
        }

        void ApplyTheme( IThemeProvider newTheme );

        void ApplyTheme( ResourceDictionary dictionary );
    }
}
