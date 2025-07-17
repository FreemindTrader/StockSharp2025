// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.DrawingHelper
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Collections.Generic;
using System.Windows;
namespace fx.Xaml.Charting
{
    internal static class DrawingHelper
    {
        public static Point TransformPoint( float x, float y, bool isVerticalChart )
        {
            if ( isVerticalChart )
                return new Point( ( double ) y, ( double ) x );
            return new Point( ( double ) x, ( double ) y );
        }

        public static Point TransformPoint( Point point, bool isVerticalChart )
        {
            if ( isVerticalChart )
            {
                double x = point.X;
                point.X = point.Y;
                point.Y = x;
            }
            return point;
        }

        public static Point TransformPoint( double x, double y, bool isVerticalChart )
        {
            if ( isVerticalChart )
                return new Point( y, x );
            return new Point( x, y );
        }

        public static void DrawPoints( IEnumerable<Point> points, IPathContextFactory factory, IPathColor color )
        {
            IEnumerator<Point> enumerator = points.GetEnumerator();
            if ( !enumerator.MoveNext() )
                return;
            Point current1 = enumerator.Current;
            using ( IPathDrawingContext pathDrawingContext1 = factory.Begin( color, current1.X, current1.Y ) )
            {
                while ( enumerator.MoveNext() )
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
}
