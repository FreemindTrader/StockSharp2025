using SciChart.Charting.Visuals.PointMarkers;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Drawing.Common;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StockSharp.Xaml.Charting;

public interface ISeriesDrawingHelperEx : ISeriesDrawingHelper
{
    void DrawPoint(IPointMarker pt, Point point, IBrush2D brush, IPen2D pen);    

    void DrawQuad(IPen2D pen, Point pt1, Point pt2);
}

