using SciChart.Charting.Visuals.RenderableSeries;
using System.Windows.Media;

namespace StockSharp.Xaml.Charting;
#nullable disable
public interface IXxxPaletteProvider
{
    Color? GetColor(IRenderableSeries rSeries, double _param2, double _param3);

    Color? GetColor(
        IRenderableSeries rSeries,
        double candleIndex,
        double openPrice,
        double highPrice,
        double lowPrice,
        double closePrice);
    

    Color? GetColor(IRenderableSeries rSeries, double _param2, double _param3, double _param4);
}
