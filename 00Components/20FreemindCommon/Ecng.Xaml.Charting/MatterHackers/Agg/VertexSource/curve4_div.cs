// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VertexSource.curve4_div
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using MatterHackers.VectorMath;

namespace MatterHackers.Agg.VertexSource
{
    internal sealed class curve4_div
    {
        private double m_approximation_scale;
        private double m_distance_tolerance_square;
        private double m_angle_tolerance;
        private double m_cusp_limit;
        private int m_count;
        private VectorPOD<Vector2> m_points;

        public curve4_div()
        {
            this.m_points = new VectorPOD<Vector2>();
            this.m_approximation_scale = 1.0;
            this.m_angle_tolerance = 0.0;
            this.m_cusp_limit = 0.0;
            this.m_count = 0;
        }

        public curve4_div( double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4 )
        {
            this.m_approximation_scale = 1.0;
            this.m_angle_tolerance = 0.0;
            this.m_cusp_limit = 0.0;
            this.m_count = 0;
            this.init( x1, y1, x2, y2, x3, y3, x4, y4 );
        }

        public curve4_div( curve4_points cp )
        {
            this.m_approximation_scale = 1.0;
            this.m_angle_tolerance = 0.0;
            this.m_count = 0;
            this.init( cp[ 0 ], cp[ 1 ], cp[ 2 ], cp[ 3 ], cp[ 4 ], cp[ 5 ], cp[ 6 ], cp[ 7 ] );
        }

        public void reset()
        {
            this.m_points.remove_all();
            this.m_count = 0;
        }

        public void init( double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4 )
        {
            this.m_points.remove_all();
            this.m_distance_tolerance_square = 0.5 / this.m_approximation_scale;
            this.m_distance_tolerance_square *= this.m_distance_tolerance_square;
            this.bezier( x1, y1, x2, y2, x3, y3, x4, y4 );
            this.m_count = 0;
        }

        public void init( curve4_points cp )
        {
            this.init( cp[ 0 ], cp[ 1 ], cp[ 2 ], cp[ 3 ], cp[ 4 ], cp[ 5 ], cp[ 6 ], cp[ 7 ] );
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

        public void cusp_limit( double v )
        {
            this.m_cusp_limit = v == 0.0 ? 0.0 : Math.PI - v;
        }

        public double cusp_limit()
        {
            if ( this.m_cusp_limit != 0.0 )
                return Math.PI - this.m_cusp_limit;
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

        private void bezier( double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4 )
        {
            this.m_points.add( new Vector2( x1, y1 ) );
            this.recursive_bezier( x1, y1, x2, y2, x3, y3, x4, y4, 0 );
            this.m_points.add( new Vector2( x4, y4 ) );
        }

        private void recursive_bezier( double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4, int level )
        {
            if ( level > 32 )
                return;
            double x2_1 = (x1 + x2) / 2.0;
            double y2_1 = (y1 + y2) / 2.0;
            double x = (x2 + x3) / 2.0;
            double y = (y2 + y3) / 2.0;
            double x3_1 = (x3 + x4) / 2.0;
            double y3_1 = (y3 + y4) / 2.0;
            double x3_2 = (x2_1 + x) / 2.0;
            double y3_2 = (y2_1 + y) / 2.0;
            double x2_2 = (x + x3_1) / 2.0;
            double y2_2 = (y + y3_1) / 2.0;
            double num1 = (x3_2 + x2_2) / 2.0;
            double num2 = (y3_2 + y2_2) / 2.0;
            double num3 = x4 - x1;
            double num4 = y4 - y1;
            double num5 = Math.Abs((x2 - x4) * num4 - (y2 - y4) * num3);
            double num6 = Math.Abs((x3 - x4) * num4 - (y3 - y4) * num3);
            int num7 = 0;
            if ( num5 > Curves.curve_collinearity_epsilon )
                num7 = 2;
            if ( num6 > Curves.curve_collinearity_epsilon )
                ++num7;
            switch ( num7 )
            {
                case 0:
                    double num8 = num3 * num3 + num4 * num4;
                    double num9;
                    double num10;
                    if ( num8 == 0.0 )
                    {
                        num9 = agg_math.calc_sq_distance( x1, y1, x2, y2 );
                        num10 = agg_math.calc_sq_distance( x4, y4, x3, y3 );
                    }
                    else
                    {
                        double num11 = 1.0 / num8;
                        double num12 = x2 - x1;
                        double num13 = y2 - y1;
                        double num14 = num11 * (num12 * num3 + num13 * num4);
                        double num15 = x3 - x1;
                        double num16 = y3 - y1;
                        double num17 = num11 * (num15 * num3 + num16 * num4);
                        if ( num14 > 0.0 && num14 < 1.0 && ( num17 > 0.0 && num17 < 1.0 ) )
                            return;
                        num9 = num14 > 0.0 ? ( num14 < 1.0 ? agg_math.calc_sq_distance( x2, y2, x1 + num14 * num3, y1 + num14 * num4 ) : agg_math.calc_sq_distance( x2, y2, x4, y4 ) ) : agg_math.calc_sq_distance( x2, y2, x1, y1 );
                        num10 = num17 > 0.0 ? ( num17 < 1.0 ? agg_math.calc_sq_distance( x3, y3, x1 + num17 * num3, y1 + num17 * num4 ) : agg_math.calc_sq_distance( x3, y3, x4, y4 ) ) : agg_math.calc_sq_distance( x3, y3, x1, y1 );
                    }
                    if ( num9 > num10 )
                    {
                        if ( num9 < this.m_distance_tolerance_square )
                        {
                            this.m_points.add( new Vector2( x2, y2 ) );
                            return;
                        }
                        break;
                    }
                    if ( num10 < this.m_distance_tolerance_square )
                    {
                        this.m_points.add( new Vector2( x3, y3 ) );
                        return;
                    }
                    break;
                case 1:
                    if ( num6 * num6 <= this.m_distance_tolerance_square * ( num3 * num3 + num4 * num4 ) )
                    {
                        if ( this.m_angle_tolerance < Curves.curve_angle_tolerance_epsilon )
                        {
                            this.m_points.add( new Vector2( x, y ) );
                            return;
                        }
                        double num11 = Math.Abs(Math.Atan2(y4 - y3, x4 - x3) - Math.Atan2(y3 - y2, x3 - x2));
                        if ( num11 >= Math.PI )
                            num11 = 2.0 * Math.PI - num11;
                        if ( num11 < this.m_angle_tolerance )
                        {
                            this.m_points.add( new Vector2( x2, y2 ) );
                            this.m_points.add( new Vector2( x3, y3 ) );
                            return;
                        }
                        if ( this.m_cusp_limit != 0.0 && num11 > this.m_cusp_limit )
                        {
                            this.m_points.add( new Vector2( x3, y3 ) );
                            return;
                        }
                        break;
                    }
                    break;
                case 2:
                    if ( num5 * num5 <= this.m_distance_tolerance_square * ( num3 * num3 + num4 * num4 ) )
                    {
                        if ( this.m_angle_tolerance < Curves.curve_angle_tolerance_epsilon )
                        {
                            this.m_points.add( new Vector2( x, y ) );
                            return;
                        }
                        double num11 = Math.Abs(Math.Atan2(y3 - y2, x3 - x2) - Math.Atan2(y2 - y1, x2 - x1));
                        if ( num11 >= Math.PI )
                            num11 = 2.0 * Math.PI - num11;
                        if ( num11 < this.m_angle_tolerance )
                        {
                            this.m_points.add( new Vector2( x2, y2 ) );
                            this.m_points.add( new Vector2( x3, y3 ) );
                            return;
                        }
                        if ( this.m_cusp_limit != 0.0 && num11 > this.m_cusp_limit )
                        {
                            this.m_points.add( new Vector2( x2, y2 ) );
                            return;
                        }
                        break;
                    }
                    break;
                case 3:
                    if ( ( num5 + num6 ) * ( num5 + num6 ) <= this.m_distance_tolerance_square * ( num3 * num3 + num4 * num4 ) )
                    {
                        if ( this.m_angle_tolerance < Curves.curve_angle_tolerance_epsilon )
                        {
                            this.m_points.add( new Vector2( x, y ) );
                            return;
                        }
                        double num11 = Math.Atan2(y3 - y2, x3 - x2);
                        double num12 = Math.Abs(num11 - Math.Atan2(y2 - y1, x2 - x1));
                        double num13 = Math.Abs(Math.Atan2(y4 - y3, x4 - x3) - num11);
                        if ( num12 >= Math.PI )
                            num12 = 2.0 * Math.PI - num12;
                        if ( num13 >= Math.PI )
                            num13 = 2.0 * Math.PI - num13;
                        if ( num12 + num13 < this.m_angle_tolerance )
                        {
                            this.m_points.add( new Vector2( x, y ) );
                            return;
                        }
                        if ( this.m_cusp_limit != 0.0 )
                        {
                            if ( num12 > this.m_cusp_limit )
                            {
                                this.m_points.add( new Vector2( x2, y2 ) );
                                return;
                            }
                            if ( num13 > this.m_cusp_limit )
                            {
                                this.m_points.add( new Vector2( x3, y3 ) );
                                return;
                            }
                            break;
                        }
                        break;
                    }
                    break;
            }
            this.recursive_bezier( x1, y1, x2_1, y2_1, x3_2, y3_2, num1, num2, level + 1 );
            this.recursive_bezier( num1, num2, x2_2, y2_2, x3_1, y3_1, x4, y4, level + 1 );
        }
    }
}
