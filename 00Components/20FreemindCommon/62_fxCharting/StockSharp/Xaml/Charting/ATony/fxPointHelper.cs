using SciChart.Drawing.Common;
using System.Collections.Generic; using fx.Collections;
using System.Windows;

namespace StockSharp.Xaml.Charting
{
    internal static class fxPointHelper
    {
        public static Point smethod_0( float float_0, float float_1, bool bool_0 )
        {
            if ( bool_0 )
                return new Point( float_1, float_0 );
            return new Point( float_0, float_1 );
        }

        public static Point Swap( Point pt, bool swap )
        {
            if ( swap )
            {
                double x = pt.X;
                pt.X = pt.Y;
                pt.Y = x;
            }
            return pt;
        }

        public static Point smethod_2( double double_0, double double_1, bool bool_0 )
        {
            if ( bool_0 )
                return new Point( double_1, double_0 );
            return new Point( double_0, double_1 );
        }

        public static void smethod_3( IEnumerable<Point> points, IPathContextFactory factory, IPathColor pathColor )
        {
            IEnumerator<Point> enumerator = points.GetEnumerator( );
            if ( !enumerator.MoveNext( ) )
                return;
            Point current1 = enumerator.Current;
            using ( IPathDrawingContext pathDrawingContext1 = factory.Begin( pathColor, current1.X, current1.Y ) )
            {
                while ( enumerator.MoveNext( ) )
                {
                    var pathDrawingContext2 = pathDrawingContext1;
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
