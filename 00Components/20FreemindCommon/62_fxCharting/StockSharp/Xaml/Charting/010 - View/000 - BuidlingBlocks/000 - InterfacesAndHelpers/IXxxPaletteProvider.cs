using SciChart.Charting.Visuals.RenderableSeries;
using System.Windows.Media;

namespace StockSharp.Xaml.Charting;
#nullable disable
public interface IXxxPaletteProvider
{
    Color? GetColor01(IRenderableSeries _param1, double _param2, double _param3);

    Color? GetColor02(IRenderableSeries _param1, double _param2, double _param3, double _param4, double _param5, double _param6);

    Color? GetColor02(IRenderableSeries _param1, double _param2, double _param3, double _param4);
}
