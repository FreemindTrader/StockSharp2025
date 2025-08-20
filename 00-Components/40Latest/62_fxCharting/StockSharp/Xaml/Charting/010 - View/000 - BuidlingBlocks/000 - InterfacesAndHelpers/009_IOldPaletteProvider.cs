using SciChart.Charting.Visuals.RenderableSeries;
using System.Windows.Media;

namespace StockSharp.Xaml.Charting;

public interface IOldPaletteProvider
{
    Color? GetColor(IRenderableSeries series, double xValue, double yValue);

    Color? OverrideColor(IRenderableSeries series, double xValue, double openValue, double highValue, double lowValue, double closeValue);

    Color? OverrideColor(IRenderableSeries series, double xValue, double yValue, double zValue);
}
