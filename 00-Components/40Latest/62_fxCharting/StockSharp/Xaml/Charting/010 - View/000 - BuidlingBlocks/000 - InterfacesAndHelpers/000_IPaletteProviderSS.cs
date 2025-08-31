using SciChart.Charting.Visuals.RenderableSeries;
using System.Windows.Media;

namespace StockSharp.Xaml.Charting;
#nullable disable

/// <summary>
/// 
/// This interface doesn't exist in Scichart library. It is only used in StockSharp custom code.
/// 
/// The IPaletteProvider interface in SciChart v4.3 and earlier was used to apply custom colors to a series on a per-data-point basis. 
/// This allowed developers to create coloring rules for series based on values, indices, or custom metadata. 
/// 
/// How it worked
/// Instead of the entire series having a single color, implementing IPaletteProvider enabled dynamic coloring based on rules you defined. 
/// The palette provider would be attached to a specific series, and SciChart would call its methods during rendering to determine the color of each individual point or line segment.
/// 
/// For example, a developer could use it to:
/// Highlight a threshold: Color all points above a certain value in red and those below it in green.
/// Color based on index: Change the color of points based on their position in the dataset.
/// Incorporate metadata: Use a custom object associated with each data point (metadata) to control its color.
/// 
/// Variations for different series types
/// The IPaletteProvider interface was implemented through several derived interfaces to handle different parts of a series.For instance: 
/// IStrokePaletteProvider: Used to color the lines or outlines of a series.
/// IFillPaletteProvider: Used to color the fill of a series, like the area of a mountain chart or the interior of a column.
/// IPointMarkerPaletteProvider: Used to color the individual point markers of a series. 
/// 
/// Transition to newer versions
/// For users upgrading from older versions, it's important to note that the IPaletteProvider API has evolved significantly since v4.3. Newer versions, 
/// including the JavaScript library, provide more advanced and often simplified methods for per-point coloring. The core concept of a palette provider for custom coloring remains,
/// but the implementation details have changed to accommodate newer features and improvements
/// </summary>

public interface IPaletteProviderSS
{
    Color? GetColor(IRenderableSeries rSeries, double _param2, double _param3);

    Color? OverrideColor(IRenderableSeries rSeries, double candleIndex, double openPrice, double highPrice, double lowPrice, double closePrice);
    

    Color? OverrideColor(IRenderableSeries rSeries, double _param2, double _param3, double _param4);
}
