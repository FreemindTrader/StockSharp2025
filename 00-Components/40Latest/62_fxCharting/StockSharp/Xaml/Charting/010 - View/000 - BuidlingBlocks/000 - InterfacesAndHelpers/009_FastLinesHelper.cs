using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Data.Model;
using SciChart.Drawing.Common;
using System;

namespace StockSharp.Xaml.Charting;

internal static class FastLinesHelper
{
    internal static void IterateLines(IPathContextFactory lineContextFactory, IPointSeries pointSeries, ICoordinateCalculator<double> xCalc, ICoordinateCalculator<double> yCalc, bool isDigitalLine)
    {
        FastLinesHelper.IterateLines(lineContextFactory, (Func<double, double, IPathColor>)( (x, y) => (IPathColor)null ), pointSeries.XValues, pointSeries.YValues, pointSeries.Count, xCalc, yCalc, isDigitalLine, true);
    }

    internal static void IterateLines(IPathContextFactory lineContextFactory, IPen2D pen, IPointSeries pointSeries, ICoordinateCalculator<double> xCalc, ICoordinateCalculator<double> yCalc, bool isDigitalLine, bool closeGaps)
    {
        if((double) pen.StrokeThickness <= 0.0 || pen.Color.A == (byte) 0)
            return;
        FastLinesHelper.IterateLines(lineContextFactory, (Func<double, double, IPathColor>)( (x, y) => (IPathColor)pen ), pointSeries.XValues, pointSeries.YValues, pointSeries.Count, xCalc, yCalc, isDigitalLine, closeGaps);
    }

    internal static void IterateLines(IPathContextFactory lineContextFactory, IBrush2D brush, IPointSeries pointSeries, ICoordinateCalculator<double> xCalc, ICoordinateCalculator<double> yCalc, bool isDigitalLine, bool closeGaps)
    {
        FastLinesHelper.IterateLines(lineContextFactory, (Func<double, double, IPathColor>)( (x, y) => (IPathColor)brush ), pointSeries.XValues, pointSeries.YValues, pointSeries.Count, xCalc, yCalc, isDigitalLine, closeGaps);
    }

    internal static void IterateLines(IPathContextFactory lineContextFactory, Func<double, double, IPathColor> createPenFunc, IPointSeries pointSeries, ICoordinateCalculator<double> xCalc, ICoordinateCalculator<double> yCalc, bool isDigitalLine, bool closeGaps)
    {
        FastLinesHelper.IterateLines(lineContextFactory, createPenFunc, pointSeries.XValues, pointSeries.YValues, pointSeries.Count, xCalc, yCalc, isDigitalLine, closeGaps);
    }

    private static void IterateLines(IPathContextFactory pathContextFactory, Func<double, double, IPathColor> createPenFunc, Values<double> xValues, Values<double> yValues, int count, ICoordinateCalculator<double> xCalc, ICoordinateCalculator<double> yCalc, bool isDigitalLine, bool closeGaps)
    {
        bool isHorizontal = !xCalc.IsHorizontalAxisCalculator;
        IPathDrawingContext dc = null;
        double x0_datavalue = xValues[0];
        double y0_datavalue = yValues[0];

        IPathColor x0y0Color = createPenFunc(x0_datavalue, y0_datavalue);

        double X_pixel = xCalc.GetCoordinate(x0_datavalue);
        double Y_pixel = yCalc.GetCoordinate(y0_datavalue);                

        double XStart_datavalue = x0_datavalue;
        double YStart_datavalue = y0_datavalue;

        double XStart_logicalPixel = isHorizontal ? X_pixel : Y_pixel;
        double YStart_logicalPixel = isHorizontal ? Y_pixel : X_pixel;


        // This is our first point
        dc = pathContextFactory.Begin(x0y0Color, XStart_logicalPixel, YStart_logicalPixel);
        
        for(int index = 1; index < count; ++index)
        {
            double XNew_datavalue = xValues[index];
            double YNew_datavalue = yValues[index];

            // The second point is not at the horizaontal level to the first point
            // So in a way, there is a gap between the price.
            if(YNew_datavalue != YStart_datavalue )
            {
                if(dc != null)
                {
                    dc.End();
                    dc = null;
                }
                XStart_datavalue = closeGaps ? XStart_datavalue : double.NaN;
                YStart_datavalue = closeGaps ? YStart_datavalue : double.NaN;
            } 
            else
            {
                // For all the continous bar, the close price should be the same as the opening price of a new bar.
                // So in a way, this point is the beginning point of a new candlestick bar.
                IPathColor pathColor = createPenFunc(XNew_datavalue, YNew_datavalue);
                
                if(pathColor != x0y0Color)
                {
                    x0y0Color.Dispose();
                    dc?.End();
                    dc = null;
                    x0y0Color = pathColor;
                }

                // Since dc == null, that means what we have here at index = i is the endinging point of the line 
                //      XStart_datavalue = X Value of beginning point 
                //      YStart_datavalue = Y Value of beginning point
                if ( dc == null)
                {
                    double tmp_X1 = xCalc.GetCoordinate(XStart_datavalue);
                    double tmp_Y1 = yCalc.GetCoordinate(YStart_datavalue);                    
                    double real_Y1 = isHorizontal ? tmp_X1 : tmp_Y1;
                    double real_X1 = isHorizontal ? tmp_Y1 : tmp_X1; 

                    YStart_logicalPixel = real_Y1;
                    XStart_logicalPixel = real_X1;

                    if( YNew_datavalue != YStart_datavalue)
                    {
                        XStart_datavalue = XNew_datavalue;
                        YStart_datavalue = YNew_datavalue;
                        continue;
                    }
                    dc = pathContextFactory.Begin(x0y0Color, real_X1, real_Y1);
                }
                double Xnew_pixel = xCalc.GetCoordinate(XNew_datavalue);
                double Ynew_pixel = yCalc.GetCoordinate(YNew_datavalue);
                
                double YNew_logicalPixel = isHorizontal ? Xnew_pixel : Ynew_pixel;
                double XNew_logicalPixel = isHorizontal ? Ynew_pixel : Xnew_pixel; 
                
                if(isDigitalLine)
                {
                    if(isHorizontal)
                    {
                        dc.MoveTo(XStart_logicalPixel, YNew_logicalPixel);
                        dc.MoveTo(XNew_logicalPixel, YNew_logicalPixel);
                    } 
                    else
                    {
                        dc.MoveTo(XNew_logicalPixel, YStart_logicalPixel);
                        dc.MoveTo(XNew_logicalPixel, YNew_logicalPixel);
                    }
                } 
                else
                {
                    dc.MoveTo(XNew_logicalPixel, YNew_logicalPixel);
                }
                    
                XStart_datavalue = XNew_datavalue;
                XStart_logicalPixel = XNew_logicalPixel;

                YStart_datavalue = YNew_datavalue;
                YStart_logicalPixel = YNew_logicalPixel;
                
            }
        }
        dc?.End();
    }
}
