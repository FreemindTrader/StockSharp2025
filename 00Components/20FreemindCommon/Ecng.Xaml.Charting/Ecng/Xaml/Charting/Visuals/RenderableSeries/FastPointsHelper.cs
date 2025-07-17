// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Visuals.RenderableSeries.FastPointsHelper
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using Ecng.Xaml.Charting.Model.DataSeries;
using Ecng.Xaml.Charting.Numerics.CoordinateCalculators;
using Ecng.Xaml.Charting.Rendering.Common;

namespace Ecng.Xaml.Charting.Visuals.RenderableSeries
{
    internal static class FastPointsHelper
    {
        internal static void IteratePoints( IPathContextFactory lineContextFactory, Func<double, double, IPathColor> createPenFunc, IPointSeries pointSeries, ICoordinateCalculator<double> xCalc, ICoordinateCalculator<double> yCalc )
        {
            FastPointsHelper.IteratePoints( lineContextFactory, createPenFunc, pointSeries.XValues.ItemsArray, pointSeries.YValues.ItemsArray, pointSeries.Count, xCalc, yCalc );
        }

        public static void IteratePoints( IPathContextFactory pathFactory, IPointSeries pointSeries, ICoordinateCalculator<double> xCalc, ICoordinateCalculator<double> yCalc )
        {
            FastPointsHelper.IteratePoints( pathFactory, ( Func<double, double, IPathColor> ) ( ( x, y ) => ( IPathColor ) null ), pointSeries.XValues.ItemsArray, pointSeries.YValues.ItemsArray, pointSeries.Count, xCalc, yCalc );
        }

        private static void IteratePoints( IPathContextFactory pathContextFactory, Func<double, double, IPathColor> createPenFunc, double[ ] xValues, double[ ] yValues, int count, ICoordinateCalculator<double> xCalc, ICoordinateCalculator<double> yCalc )
        {
            bool flag = !xCalc.IsHorizontalAxisCalculator;
            IPathDrawingContext pathDrawingContext = (IPathDrawingContext) null;
            double xValue1 = xValues[0];
            double yValue1 = yValues[0];
            IPathColor color = createPenFunc(xValue1, yValue1);
            double coordinate1 = xCalc.GetCoordinate(xValue1);
            double coordinate2 = yCalc.GetCoordinate(yValue1);
            double num1 = flag ? coordinate2 : coordinate1;
            double startY = flag ? coordinate1 : coordinate2;
            double startX = num1;
            if ( !double.IsNaN( yValue1 ) )
                pathDrawingContext = pathContextFactory.Begin( color, startX, startY );
            for ( int index = 1 ; index < count ; ++index )
            {
                double xValue2 = xValues[index];
                double yValue2 = yValues[index];
                if ( !double.IsNaN( yValue2 ) )
                {
                    IPathColor pathColor = createPenFunc(xValue2, yValue2);
                    if ( pathColor != color )
                    {
                        color.Dispose();
                        pathDrawingContext?.End();
                        pathDrawingContext = ( IPathDrawingContext ) null;
                        color = pathColor;
                    }
                    double coordinate3 = xCalc.GetCoordinate(xValue2);
                    double coordinate4 = yCalc.GetCoordinate(yValue2);
                    double num2 = flag ? coordinate4 : coordinate3;
                    double num3 = flag ? coordinate3 : coordinate4;
                    double num4 = num2;
                    if ( pathDrawingContext == null )
                        pathDrawingContext = pathContextFactory.Begin( color, num4, num3 );
                    else
                        pathDrawingContext.MoveTo( num4, num3 );
                }
            }
            pathDrawingContext?.End();
        }
    }
}
