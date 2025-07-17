// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Visuals.RenderableSeries.FastLinesHelper
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using Ecng.Xaml.Charting.Model.DataSeries;
using Ecng.Xaml.Charting.Numerics.CoordinateCalculators;
using Ecng.Xaml.Charting.Rendering.Common;

namespace Ecng.Xaml.Charting.Visuals.RenderableSeries
{
    internal static class FastLinesHelper
    {
        internal static void IterateLines( IPathContextFactory lineContextFactory, IPointSeries pointSeries, ICoordinateCalculator<double> xCalc, ICoordinateCalculator<double> yCalc, bool isDigitalLine )
        {
            FastLinesHelper.IterateLines( lineContextFactory, ( Func<double, double, IPathColor> ) ( ( x, y ) => ( IPathColor ) null ), pointSeries.XValues.ItemsArray, pointSeries.YValues.ItemsArray, pointSeries.Count, xCalc, yCalc, isDigitalLine, true );
        }

        internal static void IterateLines( IPathContextFactory lineContextFactory, IPen2D pen, IPointSeries pointSeries, ICoordinateCalculator<double> xCalc, ICoordinateCalculator<double> yCalc, bool isDigitalLine, bool closeGaps )
        {
            if ( ( double ) pen.StrokeThickness <= 0.0 || pen.Color.A == ( byte ) 0 )
                return;
            FastLinesHelper.IterateLines( lineContextFactory, ( Func<double, double, IPathColor> ) ( ( x, y ) => ( IPathColor ) pen ), pointSeries.XValues.ItemsArray, pointSeries.YValues.ItemsArray, pointSeries.Count, xCalc, yCalc, isDigitalLine, closeGaps );
        }

        internal static void IterateLines( IPathContextFactory lineContextFactory, IBrush2D brush, IPointSeries pointSeries, ICoordinateCalculator<double> xCalc, ICoordinateCalculator<double> yCalc, bool isDigitalLine, bool closeGaps )
        {
            FastLinesHelper.IterateLines( lineContextFactory, ( Func<double, double, IPathColor> ) ( ( x, y ) => ( IPathColor ) brush ), pointSeries.XValues.ItemsArray, pointSeries.YValues.ItemsArray, pointSeries.Count, xCalc, yCalc, isDigitalLine, closeGaps );
        }

        internal static void IterateLines( IPathContextFactory lineContextFactory, Func<double, double, IPathColor> createPenFunc, IPointSeries pointSeries, ICoordinateCalculator<double> xCalc, ICoordinateCalculator<double> yCalc, bool isDigitalLine, bool closeGaps )
        {
            FastLinesHelper.IterateLines( lineContextFactory, createPenFunc, pointSeries.XValues.ItemsArray, pointSeries.YValues.ItemsArray, pointSeries.Count, xCalc, yCalc, isDigitalLine, closeGaps );
        }

        private static void IterateLines( IPathContextFactory pathContextFactory, Func<double, double, IPathColor> createPenFunc, double[ ] xValues, double[ ] yValues, int count, ICoordinateCalculator<double> xCalc, ICoordinateCalculator<double> yCalc, bool isDigitalLine, bool closeGaps )
        {
            bool flag = !xCalc.IsHorizontalAxisCalculator;
            IPathDrawingContext pathDrawingContext = (IPathDrawingContext) null;
            double xValue1 = xValues[0];
            double yValue1 = yValues[0];
            IPathColor color = createPenFunc(xValue1, yValue1);
            double coordinate1 = xCalc.GetCoordinate(xValue1);
            double coordinate2 = yCalc.GetCoordinate(yValue1);
            double num1 = flag ? coordinate2 : coordinate1;
            double startY1 = flag ? coordinate1 : coordinate2;
            double startX1 = num1;
            double dataValue1 = xValue1;
            double dataValue2 = yValue1;
            double y1 = startY1;
            double x1 = startX1;
            if ( yValue1 == yValue1 )
                pathDrawingContext = pathContextFactory.Begin( color, startX1, startY1 );
            for ( int index = 1 ; index < count ; ++index )
            {
                double xValue2 = xValues[index];
                double yValue2 = yValues[index];
                if ( yValue2 != yValue2 )
                {
                    if ( pathDrawingContext != null )
                    {
                        pathDrawingContext.End();
                        pathDrawingContext = ( IPathDrawingContext ) null;
                    }
                    dataValue1 = closeGaps ? dataValue1 : double.NaN;
                    dataValue2 = closeGaps ? dataValue2 : double.NaN;
                }
                else
                {
                    IPathColor pathColor = createPenFunc(xValue2, yValue2);
                    if ( pathColor != color )
                    {
                        color.Dispose();
                        pathDrawingContext?.End();
                        pathDrawingContext = ( IPathDrawingContext ) null;
                        color = pathColor;
                    }
                    if ( pathDrawingContext == null )
                    {
                        double coordinate3 = xCalc.GetCoordinate(dataValue1);
                        double coordinate4 = yCalc.GetCoordinate(dataValue2);
                        double num2 = flag ? coordinate4 : coordinate3;
                        double startY2 = flag ? coordinate3 : coordinate4;
                        double startX2 = num2;
                        y1 = startY2;
                        x1 = startX2;
                        if ( dataValue2 != dataValue2 )
                        {
                            dataValue1 = xValue2;
                            dataValue2 = yValue2;
                            continue;
                        }
                        pathDrawingContext = pathContextFactory.Begin( color, startX2, startY2 );
                    }
                    double coordinate5 = xCalc.GetCoordinate(xValue2);
                    double coordinate6 = yCalc.GetCoordinate(yValue2);
                    double num3 = flag ? coordinate6 : coordinate5;
                    double y2 = flag ? coordinate5 : coordinate6;
                    double x2 = num3;
                    if ( isDigitalLine )
                    {
                        if ( flag )
                        {
                            pathDrawingContext.MoveTo( x1, y2 );
                            pathDrawingContext.MoveTo( x2, y2 );
                        }
                        else
                        {
                            pathDrawingContext.MoveTo( x2, y1 );
                            pathDrawingContext.MoveTo( x2, y2 );
                        }
                    }
                    else
                        pathDrawingContext.MoveTo( x2, y2 );
                    dataValue1 = xValue2;
                    dataValue2 = yValue2;
                    y1 = y2;
                    x1 = x2;
                }
            }
            pathDrawingContext?.End();
        }
    }
}
