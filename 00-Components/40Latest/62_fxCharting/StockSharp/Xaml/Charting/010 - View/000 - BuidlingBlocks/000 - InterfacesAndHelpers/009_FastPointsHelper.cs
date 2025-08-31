
using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Data.Model;
using SciChart.Drawing.Common;
using System;

namespace StockSharp.Xaml.Charting;

/// <summary>
/// A helper class to handle the drawing of points.
/// </summary>
internal static class FastPointsHelper
{
    internal static void DrawPoints(IPathContextFactory lineContextFactory, Func<double, double, IPathColor> createPenFunc, IPointSeries pointSeries, ICoordinateCalculator<double> xCalc, ICoordinateCalculator<double> yCalc)
    {
        DrawPoints(lineContextFactory, createPenFunc, pointSeries.XValues, pointSeries.YValues, pointSeries.Count, xCalc, yCalc);
    }

    public static void DrawPoints(IPointMarkerPathContextFactory pathFactory, IPointSeries pointSeries, ICoordinateCalculator<double> xCalc, ICoordinateCalculator<double> yCalc)
    {
        DrawPoints(pathFactory, (Func<double, double, IPathColor>)( (x, y) => (IPathColor)null ), pointSeries.XValues, pointSeries.YValues, pointSeries.Count, xCalc, yCalc);
    }

    private static void DrawPoints(IPathContextFactory pathContextFactory, Func<double, double, IPathColor> createPenFunc, Values<double> xValues, Values<double> yValues, int count, ICoordinateCalculator<double> xCalc, ICoordinateCalculator<double> yCalc)
    {
        var isVertical         = !xCalc.IsHorizontalAxisCalculator;
        var pathDrawingContext = (IPathDrawingContext) null;
        var x0Value            = xValues[0];
        var y0Value            = yValues[0];
        var color              = createPenFunc(x0Value, y0Value);

        var x0                 = xCalc.GetCoordinate(x0Value);
        var y0                 = yCalc.GetCoordinate(y0Value);
                
        var startY             = isVertical ? x0 : y0;
        var startX             = isVertical ? y0 : x0;

        if (!double.IsNaN(y0Value))
            pathDrawingContext = pathContextFactory.Begin(color, startX, startY);
        
        for(int index = 1; index < count; ++index)
        {
            double xiValue = xValues[index];
            double yiValue = yValues[index];

            if(!double.IsNaN(yiValue))
            {
                var pathColor = createPenFunc(xiValue, yiValue);
                
                if(pathColor != color)
                {
                    color.Dispose();
                    pathDrawingContext?.End();
                    pathDrawingContext = (IPathDrawingContext) null;
                    color = pathColor;
                }
                double xi = xCalc.GetCoordinate(xiValue);
                double yi = yCalc.GetCoordinate(yiValue);

                double endX = isVertical ? yi : xi;
                double endY = isVertical ? xi : yi;
                
                if(pathDrawingContext == null)
                    pathDrawingContext = pathContextFactory.Begin(color, endX, endY);
                else
                    pathDrawingContext.MoveTo(endX, endY);
            }
        }
        pathDrawingContext?.End();
    }

    private static void DrawPoints(IPointMarkerPathContextFactory pathContextFactory, Func<double, double, IPathColor> createPenFunc, Values<double> xValues, Values<double> yValues, int count, ICoordinateCalculator<double> xCalc, ICoordinateCalculator<double> yCalc)
    {
        var isVertical         = !xCalc.IsHorizontalAxisCalculator;
        var pathDrawingContext = (IPointMarkerPathDrawingContext)null;
        var x0Value            = xValues[0];
        var y0Value            = yValues[0];
        var color              = createPenFunc(x0Value, y0Value);

        var x0                 = xCalc.GetCoordinate(x0Value);
        var y0                 = yCalc.GetCoordinate(y0Value);

        var startY             = isVertical ? x0 : y0;
        var startX             = isVertical ? y0 : x0;

        if ( !double.IsNaN(y0Value) )
            pathDrawingContext = pathContextFactory.Begin(color, startX, startY, 0);

        for ( int index = 1; index < count; ++index )
        {
            double xiValue = xValues[index];
            double yiValue = yValues[index];

            if ( !double.IsNaN(yiValue) )
            {
                var pathColor = createPenFunc(xiValue, yiValue);

                if ( pathColor != color )
                {
                    color.Dispose();
                    pathDrawingContext?.End();
                    pathDrawingContext = (IPointMarkerPathDrawingContext)null;
                    color = pathColor;
                }

                var xi   = xCalc.GetCoordinate(xiValue);
                var yi   = yCalc.GetCoordinate(yiValue);

                var endX = isVertical ? yi : xi;
                var endY = isVertical ? xi : yi;

                if ( pathDrawingContext == null )
                    pathDrawingContext = pathContextFactory.Begin(color, endX, endY, index);
                else
                    pathDrawingContext.MoveTo(endX, endY, index);
            }
        }
        pathDrawingContext?.End();
    }

}
