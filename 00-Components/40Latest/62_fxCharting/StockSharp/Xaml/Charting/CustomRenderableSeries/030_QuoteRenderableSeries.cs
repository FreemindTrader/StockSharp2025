using SciChart.Charting.Common.Databinding;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Data.Model;
using SciChart.Drawing.Common;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StockSharp.Xaml.Charting;

public class QuoteRenderableSeries : CustomRenderableSeries
{
    public static readonly DependencyProperty QuoteEnabledProperty = DependencyProperty.Register("QuoteEnabled", typeof(bool), typeof(QuoteRenderableSeries), new PropertyMetadata(default(bool)));    

    public bool IsQuoteEnabled
    {
        get
        {
            return (bool)GetValue(QuoteEnabledProperty);
        }
        set
        {
            SetValue(QuoteEnabledProperty, value);
        }
    }    

    private double _quotePrice;

    /// <summary>
    /// Draws the series using the <see cref="IRenderContext2D"/> and the <see cref="IRenderPassData"/> passed in
    /// </summary>
    /// <param name="renderContext">
    /// The render context. This is a graphics object which has methods to draw lines, quads and polygons to the screen
    /// </param>
    /// <param name="renderPassData">
    /// The render pass data. Contains a resampled  <see cref="IPointSeries"/>, the  <see cref="IndexRange"/> of points
    /// on the screen and the current YAxis and XAxis  <see cref="ICoordinateCalculator{T}"/> to convert data-points to
    /// screen points
    /// </param>
    protected override void Draw(IRenderContext2D renderContext, IRenderPassData renderPassData)
    {
        base.Draw(renderContext, renderPassData);

        // Get the data from RenderPassData. See CustomRenderableSeries article which describes PointSeries relationship to DataSeries
        if(renderPassData.PointSeries.Count == 0)
        {
            return;
        }

        var xRange  = renderPassData.PointRange;

        _quotePrice = renderPassData.PointSeries.YValues[0];

        // Get the coordinates of the first dataPoint
        var begin   = GetCoordinatesFor(xRange.Min, _quotePrice);
        var end     = GetCoordinatesFor(int.MaxValue, _quotePrice);

        // Create a pen to draw the spline line. Make sure you dispose it!             
        using(var linePen = renderContext.CreatePen(Stroke, AntiAliasing, StrokeThickness, Opacity, StrokeDashArray))
        {
            // Create a line drawing context. Make sure you dispose it!
            // NOTE: You can create mutliple line drawing contexts to draw segments if you want
            //       You can also call renderContext.DrawLine() and renderContext.DrawLines(), but the lineDrawingContext is higher performance
            using(var lineDrawingContext = renderContext.BeginLine(linePen, begin.X, begin.Y))
            {
                lineDrawingContext.MoveTo(end.X, end.Y);
            }
        }
    }

    private Point GetCoordinatesFor(double xValue, double yValue)
    {
        // Get the coordinateCalculators. See 'Converting Pixel Coordinates to Data Coordinates' documentation for coordinate transforms
        var xCoord = CurrentRenderPassData.XCoordinateCalculator.GetCoordinate(xValue);
        var yCoord = CurrentRenderPassData.YCoordinateCalculator.GetCoordinate(yValue);

        return new Point(xCoord, yCoord);
    }
}
