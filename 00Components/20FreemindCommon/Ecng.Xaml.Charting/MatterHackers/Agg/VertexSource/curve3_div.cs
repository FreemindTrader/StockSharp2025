// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VertexSource.curve3_div
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using MatterHackers.VectorMath;

namespace MatterHackers.Agg.VertexSource
{
    internal sealed class curve3_div
    {
        private double m_approximation_scale;
        private double m_distance_tolerance_square;
        private double m_angle_tolerance;
        private int m_count;
        private VectorPOD<Vector2> m_points;

        public curve3_div()
        {
            this.m_points = new VectorPOD<Vector2>();
            this.m_approximation_scale = 1.0;
            this.m_angle_tolerance = 0.0;
            this.m_count = 0;
        }

        public curve3_div( double x1, double y1, double x2, double y2, double x3, double y3 )
        {
            this.m_approximation_scale = 1.0;
            this.m_angle_tolerance = 0.0;
            this.m_count = 0;
            this.init( x1, y1, x2, y2, x3, y3 );
        }

        public void reset()
        {
            this.m_points.remove_all();
            this.m_count = 0;
        }

        public void init( double x1, double y1, double x2, double y2, double x3, double y3 )
        {
            this.m_points.remove_all();
            this.m_distance_tolerance_square = 0.5 / this.m_approximation_scale;
            this.m_distance_tolerance_square *= this.m_distance_tolerance_square;
            this.bezier( x1, y1, x2, y2, x3, y3 );
            this.m_count = 0;
        }

        public void approximation_method( Curves.CurveApproximationMethod method )
        {
        }

        public Curves.CurveApproximationMethod approximation_method()
        {
            return Curves.CurveApproximationMethod.curve_div;
        }

        public void approximation_scale( double s )
        {
            this.m_approximation_scale = s;
        }

        public double approximation_scale()
        {
            return this.m_approximation_scale;
        }

        public void angle_tolerance( double a )
        {
            this.m_angle_tolerance = a;
        }

        public double angle_tolerance()
        {
            return this.m_angle_tolerance;
        }

        public void cusp_limit( double limit )
        {
        }

        public double cusp_limit()
        {
            return 0.0;
        }

        public void rewind( int idx )
        {
            this.m_count = 0;
        }

        public Path.FlagsAndCommand vertex( out double x, out double y )
        {
            if ( this.m_count >= this.m_points.size() )
            {
                x = 0.0;
                y = 0.0;
                return Path.FlagsAndCommand.CommandStop;
            }
            Vector2 point = this.m_points[this.m_count++];
            x = point.x;
            y = point.y;
            return this.m_count != 1 ? Path.FlagsAndCommand.CommandLineTo : Path.FlagsAndCommand.CommandMoveTo;
        }

        private void bezier( double x1, double y1, double x2, double y2, double x3, double y3 )
        {
            this.m_points.add( new Vector2( x1, y1 ) );
            this.recursive_bezier( x1, y1, x2, y2, x3, y3, 0 );
            this.m_points.add( new Vector2( x3, y3 ) );
        }

        private void recursive_bezier( double x1, double y1, double x2, double y2, double x3, double y3, int level )
        {
            if ( level > 32 )
                return;
            double x2_1 = (x1 + x2) / 2.0;
            double y2_1 = (y1 + y2) / 2.0;
            double x2_2 = (x2 + x3) / 2.0;
            double y2_2 = (y2 + y3) / 2.0;
            double num1 = (x2_1 + x2_2) / 2.0;
            double num2 = (y2_1 + y2_2) / 2.0;
            double num3 = x3 - x1;
            double num4 = y3 - y1;
            double num5 = Math.Abs((x2 - x3) * num4 - (y2 - y3) * num3);
            if ( num5 > Curves.curve_collinearity_epsilon )
            {
                if ( num5 * num5 <= this.m_distance_tolerance_square * ( num3 * num3 + num4 * num4 ) )
                {
                    if ( this.m_angle_tolerance < Curves.curve_angle_tolerance_epsilon )
                    {
                        this.m_points.add( new Vector2( num1, num2 ) );
                        return;
                    }
                    double num6 = Math.Abs(Math.Atan2(y3 - y2, x3 - x2) - Math.Atan2(y2 - y1, x2 - x1));
                    if ( num6 >= Math.PI )
                        num6 = 2.0 * Math.PI - num6;
                    if ( num6 < this.m_angle_tolerance )
                    {
                        this.m_points.add( new Vector2( num1, num2 ) );
                        return;
                    }
                }
            }
            else
            {
                double num6 = num3 * num3 + num4 * num4;
                double num7;
                if ( num6 == 0.0 )
                {
                    num7 = agg_math.calc_sq_distance( x1, y1, x2, y2 );
                }
                else
                {
                    double num8 = ((x2 - x1) * num3 + (y2 - y1) * num4) / num6;
                    if ( num8 > 0.0 && num8 < 1.0 )
                        return;
                    num7 = num8 > 0.0 ? ( num8 < 1.0 ? agg_math.calc_sq_distance( x2, y2, x1 + num8 * num3, y1 + num8 * num4 ) : agg_math.calc_sq_distance( x2, y2, x3, y3 ) ) : agg_math.calc_sq_distance( x2, y2, x1, y1 );
                }
                if ( num7 < this.m_distance_tolerance_square )
                {
                    this.m_points.add( new Vector2( x2, y2 ) );
                    return;
                }
            }
            this.recursive_bezier( x1, y1, x2_1, y2_1, num1, num2, level + 1 );
            this.recursive_bezier( num1, num2, x2_2, y2_2, x3, y3, level + 1 );
        }
    }
}
