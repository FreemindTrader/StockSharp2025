using SciChart.Charting.Visuals.RenderableSeries;
using System.Windows.Media;

namespace StockSharp.Xaml.Charting;
#nullable disable

/// <summary>
/// This interface doesn't exist in Scichart library. It is only used in StockSharp custom code.
/// </summary>
public interface ISSPaletteProvider
{
    Color? GetColor(IRenderableSeries rSeries, double _param2, double _param3);

    Color? OverrideColor(IRenderableSeries rSeries, double candleIndex, double openPrice, double highPrice, double lowPrice, double closePrice);
    

    Color? OverrideColor(IRenderableSeries rSeries, double _param2, double _param3, double _param4);
}
