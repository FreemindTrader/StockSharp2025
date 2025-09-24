using SciChart.Drawing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StockSharp.Xaml.Charting;

internal static class DrawingHelper2025
{
    public static Point NormalizePoint( float x, float y, bool isVerticalChart )
    {
        if ( isVerticalChart )
            return new Point( ( double ) y, ( double ) x );
        return new Point( ( double ) x, ( double ) y );
    }

    public static Point NormalizePoint( Point point, bool isVerticalChart )
    {
        if ( isVerticalChart )
        {
            double x = point.X;
            point.X = point.Y;
            point.Y = x;
        }
        return point;
    }

    public static Point NormalizePoint( double x, double y, bool isVerticalChart )
    {
        if ( isVerticalChart )
            return new Point( y, x );
        return new Point( x, y );
    }

    public static void DrawPoints( IEnumerable<Point> points, IPathContextFactory factory, IPathColor color )
    {
        IEnumerator<Point> enumerator = points.GetEnumerator();
        if ( !enumerator.MoveNext( ) )
            return;
        Point current1 = enumerator.Current;
        using ( IPathDrawingContext pathDrawingContext1 = factory.Begin( color, current1.X, current1.Y ) )
        {
            while ( enumerator.MoveNext( ) )
            {
                IPathDrawingContext pathDrawingContext2 = pathDrawingContext1;
                Point current2 = enumerator.Current;
                double x = current2.X;
                current2 = enumerator.Current;
                double y = current2.Y;
                pathDrawingContext2.MoveTo( x, y );
            }
        }
    }
}

