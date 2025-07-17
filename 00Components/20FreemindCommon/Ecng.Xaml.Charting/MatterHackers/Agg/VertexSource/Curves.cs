// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VertexSource.Curves
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace MatterHackers.Agg.VertexSource
{
    internal static class Curves
    {
        public static double curve_distance_epsilon = 1E-30;
        public static double curve_collinearity_epsilon = 1E-30;
        public static double curve_angle_tolerance_epsilon = 0.01;

        public static curve4_points catrom_to_bezier( double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4 )
        {
            return new curve4_points( x2, y2, ( -x1 + 6.0 * x2 + x3 ) / 6.0, ( -y1 + 6.0 * y2 + y3 ) / 6.0, ( x2 + 6.0 * x3 - x4 ) / 6.0, ( y2 + 6.0 * y3 - y4 ) / 6.0, x3, y3 );
        }

        public static curve4_points catrom_to_bezier( curve4_points cp )
        {
            return Curves.catrom_to_bezier( cp[ 0 ], cp[ 1 ], cp[ 2 ], cp[ 3 ], cp[ 4 ], cp[ 5 ], cp[ 6 ], cp[ 7 ] );
        }

        public static curve4_points ubspline_to_bezier( double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4 )
        {
            return new curve4_points( ( x1 + 4.0 * x2 + x3 ) / 6.0, ( y1 + 4.0 * y2 + y3 ) / 6.0, ( 4.0 * x2 + 2.0 * x3 ) / 6.0, ( 4.0 * y2 + 2.0 * y3 ) / 6.0, ( 2.0 * x2 + 4.0 * x3 ) / 6.0, ( 2.0 * y2 + 4.0 * y3 ) / 6.0, ( x2 + 4.0 * x3 + x4 ) / 6.0, ( y2 + 4.0 * y3 + y4 ) / 6.0 );
        }

        public static curve4_points ubspline_to_bezier( curve4_points cp )
        {
            return Curves.ubspline_to_bezier( cp[ 0 ], cp[ 1 ], cp[ 2 ], cp[ 3 ], cp[ 4 ], cp[ 5 ], cp[ 6 ], cp[ 7 ] );
        }

        public static curve4_points hermite_to_bezier( double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4 )
        {
            return new curve4_points( x1, y1, ( 3.0 * x1 + x3 ) / 3.0, ( 3.0 * y1 + y3 ) / 3.0, ( 3.0 * x2 - x4 ) / 3.0, ( 3.0 * y2 - y4 ) / 3.0, x2, y2 );
        }

        public static curve4_points hermite_to_bezier( curve4_points cp )
        {
            return Curves.hermite_to_bezier( cp[ 0 ], cp[ 1 ], cp[ 2 ], cp[ 3 ], cp[ 4 ], cp[ 5 ], cp[ 6 ], cp[ 7 ] );
        }

        public enum CurveApproximationMethod
        {
            curve_inc,
            curve_div,
        }

        public enum curve_recursion_limit_e
        {
            curve_recursion_limit = 32, // 0x00000020
        }
    }
}
