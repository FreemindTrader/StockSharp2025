using SciChart.Charting.Visuals.PointMarkers;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Drawing.Common;
using System.Windows;

namespace StockSharp.Xaml.Charting;

/// <summary>
/// Some additional methods for <see cref="ISeriesDrawingHelper"/>.
/// </summary>
public interface ISeriesDrawingHelperSS : ISeriesDrawingHelper
{
    void DrawPoint( IPointMarker pt, Point point, IBrush2D brush, IPen2D pen );

    void DrawQuad( IPen2D pen, Point pt1, Point pt2 );
}

