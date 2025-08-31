using SciChart.Charting.Themes;
using System.Windows;
using System.Windows.Media;

namespace StockSharp.Xaml.Charting;

public interface IThemeProviderEx : IThemeProvider
{
    /// <summary>
    /// Gets or Sets the brush used for Scroll bar Fill color
    /// </summary>
    Brush ScrollbarFillBrush
    {
        get;
        set;
    }

    /// <summary>
    /// 
    /// The following are custom colors used in Point and Figure Charts
    /// 
    /// </summary>
    Color BoxVolumeTimeframe2Color
    {
        get;
        set;
    }

    /// <summary>
    ///
    /// </summary>
    Color BoxVolumeTimeframe2FrameColor
    {
        get;
        set;
    }

    /// <summary>
    ///
    /// </summary>
    Color BoxVolumeTimeframe3Color
    {
        get;
        set;
    }

    /// <summary>
    ///
    /// </summary>
    Color BoxVolumeCellFontColor
    {
        get;
        set;
    }

    /// <summary>
    ///
    /// </summary>
    Color BoxVolumeHighVolColor
    {
        get;
        set;
    }

    /// <summary>
    /// 
    /// The following are custom colors used in Cluster Profile Charts
    /// 
    /// </summary>
    Color ClusterProfileLineColor
    {
        get;
        set;
    }

    /// <summary>
    ///
    /// </summary>
    Color ClusterProfileTextColor
    {
        get;
        set;
    }

    /// <summary>
    ///
    /// </summary>
    Color ClusterProfileClusterColor
    {
        get;
        set;
    }

    /// <summary>
    ///
    /// </summary>
    Color ClusterProfileClusterMaxColor
    {
        get;
        set;
    }

    Color ClusterProfileSeparatorLineColor
    {
        get;
        set;
    }
}
