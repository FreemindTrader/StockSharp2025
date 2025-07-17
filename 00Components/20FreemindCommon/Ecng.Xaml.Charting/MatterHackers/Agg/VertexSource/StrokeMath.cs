// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VertexSource.StrokeMath
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using MatterHackers.VectorMath;

namespace MatterHackers.Agg.VertexSource
{
    internal class StrokeMath
    {
        private double m_width;
        private double m_width_abs;
        private double m_width_eps;
        private int m_width_sign;
        private double m_miter_limit;
        private double m_inner_miter_limit;
        private double m_approx_scale;
        private LineCap m_line_cap;
        private LineJoin m_line_join;
        private InnerJoin m_inner_join;

        public StrokeMath()
        {
            this.m_width = 0.5;
            this.m_width_abs = 0.5;
            this.m_width_eps = 0.00048828125;
            this.m_width_sign = 1;
            this.m_miter_limit = 4.0;
            this.m_inner_miter_limit = 1.01;
            this.m_approx_scale = 1.0;
            this.m_line_cap = LineCap.Butt;
            this.m_line_join = LineJoin.Miter;
            this.m_inner_join = InnerJoin.Miter;
        }

        public void line_cap( LineCap lc )
        {
            this.m_line_cap = lc;
        }

        public void line_join( LineJoin lj )
        {
            this.m_line_join = lj;
        }

        public void inner_join( InnerJoin ij )
        {
            this.m_inner_join = ij;
        }

        public LineCap line_cap()
        {
            return this.m_line_cap;
        }

        public LineJoin line_join()
        {
            return this.m_line_join;
        }

        public InnerJoin inner_join()
        {
            return this.m_inner_join;
        }

        public void width( double w )
        {
            this.m_width = w * 0.5;
            if ( this.m_width < 0.0 )
            {
                this.m_width_abs = -this.m_width;
                this.m_width_sign = -1;
            }
            else
            {
                this.m_width_abs = this.m_width;
                this.m_width_sign = 1;
            }
            this.m_width_eps = this.m_width / 1024.0;
        }

        public void miter_limit( double ml )
        {
            this.m_miter_limit = ml;
        }

        public void miter_limit_theta( double t )
        {
            this.m_miter_limit = 1.0 / Math.Sin( t * 0.5 );
        }

        public void inner_miter_limit( double ml )
        {
            this.m_inner_miter_limit = ml;
        }

        public void approximation_scale( double aproxScale )
        {
            this.m_approx_scale = aproxScale;
        }

        public double width()
        {
            return this.m_width * 2.0;
        }

        public double miter_limit()
        {
            return this.m_miter_limit;
        }

        public double inner_miter_limit()
        {
            return this.m_inner_miter_limit;
        }

        public double approximation_scale()
        {
            return this.m_approx_scale;
        }

        public void calc_cap( IVertexDest vc, VertexDistance v0, VertexDistance v1, double len )
        {
            vc.remove_all();
            double num1 = (v1.y - v0.y) / len;
            double num2 = (v1.x - v0.x) / len;
            double num3 = 0.0;
            double num4 = 0.0;
            double x = num1 * this.m_width;
            double y = num2 * this.m_width;
            if ( this.m_line_cap != LineCap.Round )
            {
                if ( this.m_line_cap == LineCap.Square )
                {
                    num3 = y * ( double ) this.m_width_sign;
                    num4 = x * ( double ) this.m_width_sign;
                }
                this.add_vertex( vc, v0.x - x - num3, v0.y + y - num4 );
                this.add_vertex( vc, v0.x + x - num3, v0.y - y - num4 );
            }
            else
            {
                int num5 = (int) (Math.PI / (Math.Acos(this.m_width_abs / (this.m_width_abs + 0.125 / this.m_approx_scale)) * 2.0));
                double num6 = Math.PI / (double) (num5 + 1);
                this.add_vertex( vc, v0.x - x, v0.y + y );
                if ( this.m_width_sign > 0 )
                {
                    double num7 = Math.Atan2(y, -x) + num6;
                    for ( int index = 0 ; index < num5 ; ++index )
                    {
                        this.add_vertex( vc, v0.x + Math.Cos( num7 ) * this.m_width, v0.y + Math.Sin( num7 ) * this.m_width );
                        num7 += num6;
                    }
                }
                else
                {
                    double num7 = Math.Atan2(-y, x) - num6;
                    for ( int index = 0 ; index < num5 ; ++index )
                    {
                        this.add_vertex( vc, v0.x + Math.Cos( num7 ) * this.m_width, v0.y + Math.Sin( num7 ) * this.m_width );
                        num7 -= num6;
                    }
                }
                this.add_vertex( vc, v0.x + x, v0.y - y );
            }
        }

        public void calc_join( IVertexDest vc, VertexDistance v0, VertexDistance v1, VertexDistance v2, double len1, double len2 )
        {
            double num1 = this.m_width * (v1.y - v0.y) / len1;
            double dy1 = this.m_width * (v1.x - v0.x) / len1;
            double num2 = this.m_width * (v2.y - v1.y) / len2;
            double dy2 = this.m_width * (v2.x - v1.x) / len2;
            vc.remove_all();
            double num3 = agg_math.cross_product(v0.x, v0.y, v1.x, v1.y, v2.x, v2.y);
            if ( num3 != 0.0 && num3 > 0.0 == this.m_width > 0.0 )
            {
                double mlimit = (len1 < len2 ? len1 : len2) / this.m_width_abs;
                if ( mlimit < this.m_inner_miter_limit )
                    mlimit = this.m_inner_miter_limit;
                switch ( this.m_inner_join )
                {
                    case InnerJoin.Miter:
                        this.calc_miter( vc, v0, v1, v2, num1, dy1, num2, dy2, LineJoin.MiterRevert, mlimit, 0.0 );
                        break;
                    case InnerJoin.Jag:
                    case InnerJoin.Round:
                        double num4 = (num1 - num2) * (num1 - num2) + (dy1 - dy2) * (dy1 - dy2);
                        if ( num4 < len1 * len1 && num4 < len2 * len2 )
                        {
                            this.calc_miter( vc, v0, v1, v2, num1, dy1, num2, dy2, LineJoin.MiterRevert, mlimit, 0.0 );
                            break;
                        }
                        if ( this.m_inner_join == InnerJoin.Jag )
                        {
                            this.add_vertex( vc, v1.x + num1, v1.y - dy1 );
                            this.add_vertex( vc, v1.x, v1.y );
                            this.add_vertex( vc, v1.x + num2, v1.y - dy2 );
                            break;
                        }
                        this.add_vertex( vc, v1.x + num1, v1.y - dy1 );
                        this.add_vertex( vc, v1.x, v1.y );
                        this.calc_arc( vc, v1.x, v1.y, num2, -dy2, num1, -dy1 );
                        this.add_vertex( vc, v1.x, v1.y );
                        this.add_vertex( vc, v1.x + num2, v1.y - dy2 );
                        break;
                    default:
                        this.add_vertex( vc, v1.x + num1, v1.y - dy1 );
                        this.add_vertex( vc, v1.x + num2, v1.y - dy2 );
                        break;
                }
            }
            else
            {
                double x = (num1 + num2) / 2.0;
                double y = (dy1 + dy2) / 2.0;
                double dbevel = Math.Sqrt(x * x + y * y);
                if ( ( this.m_line_join == LineJoin.Round || this.m_line_join == LineJoin.Bevel ) && this.m_approx_scale * ( this.m_width_abs - dbevel ) < this.m_width_eps )
                {
                    if ( agg_math.calc_intersection( v0.x + num1, v0.y - dy1, v1.x + num1, v1.y - dy1, v1.x + num2, v1.y - dy2, v2.x + num2, v2.y - dy2, out x, out y ) )
                        this.add_vertex( vc, x, y );
                    else
                        this.add_vertex( vc, v1.x + num1, v1.y - dy1 );
                }
                else
                {
                    switch ( this.m_line_join )
                    {
                        case LineJoin.Miter:
                        case LineJoin.MiterRevert:
                        case LineJoin.MiterRound:
                            this.calc_miter( vc, v0, v1, v2, num1, dy1, num2, dy2, this.m_line_join, this.m_miter_limit, dbevel );
                            break;
                        case LineJoin.Round:
                            this.calc_arc( vc, v1.x, v1.y, num1, -dy1, num2, -dy2 );
                            break;
                        default:
                            this.add_vertex( vc, v1.x + num1, v1.y - dy1 );
                            this.add_vertex( vc, v1.x + num2, v1.y - dy2 );
                            break;
                    }
                }
            }
        }

        private void add_vertex( IVertexDest vc, double x, double y )
        {
            vc.add( new Vector2( x, y ) );
        }

        private void calc_arc( IVertexDest vc, double x, double y, double dx1, double dy1, double dx2, double dy2 )
        {
            double num1 = Math.Atan2(dy1 * (double) this.m_width_sign, dx1 * (double) this.m_width_sign);
            double num2 = Math.Atan2(dy2 * (double) this.m_width_sign, dx2 * (double) this.m_width_sign);
            double num3 = num1 - num2;
            double num4 = Math.Acos(this.m_width_abs / (this.m_width_abs + 0.125 / this.m_approx_scale)) * 2.0;
            this.add_vertex( vc, x + dx1, y + dy1 );
            if ( this.m_width_sign > 0 )
            {
                if ( num1 > num2 )
                    num2 += 2.0 * Math.PI;
                int num5 = (int) ((num2 - num1) / num4);
                double num6 = (num2 - num1) / (double) (num5 + 1);
                double num7 = num1 + num6;
                for ( int index = 0 ; index < num5 ; ++index )
                {
                    this.add_vertex( vc, x + Math.Cos( num7 ) * this.m_width, y + Math.Sin( num7 ) * this.m_width );
                    num7 += num6;
                }
            }
            else
            {
                if ( num1 < num2 )
                    num2 -= 2.0 * Math.PI;
                int num5 = (int) ((num1 - num2) / num4);
                double num6 = (num1 - num2) / (double) (num5 + 1);
                double num7 = num1 - num6;
                for ( int index = 0 ; index < num5 ; ++index )
                {
                    this.add_vertex( vc, x + Math.Cos( num7 ) * this.m_width, y + Math.Sin( num7 ) * this.m_width );
                    num7 -= num6;
                }
            }
            this.add_vertex( vc, x + dx2, y + dy2 );
        }

        private void calc_miter( IVertexDest vc, VertexDistance v0, VertexDistance v1, VertexDistance v2, double dx1, double dy1, double dx2, double dy2, LineJoin lj, double mlimit, double dbevel )
        {
            double x1 = v1.x;
            double y1 = v1.y;
            double num1 = 1.0;
            double num2 = this.m_width_abs * mlimit;
            bool flag1 = true;
            bool flag2 = true;
            if ( agg_math.calc_intersection( v0.x + dx1, v0.y - dy1, v1.x + dx1, v1.y - dy1, v1.x + dx2, v1.y - dy2, v2.x + dx2, v2.y - dy2, out x1, out y1 ) )
            {
                num1 = agg_math.calc_distance( v1.x, v1.y, x1, y1 );
                if ( num1 <= num2 )
                {
                    this.add_vertex( vc, x1, y1 );
                    flag1 = false;
                }
                flag2 = false;
            }
            else
            {
                double x2 = v1.x + dx1;
                double y2 = v1.y - dy1;
                if ( agg_math.cross_product( v0.x, v0.y, v1.x, v1.y, x2, y2 ) < 0.0 == agg_math.cross_product( v1.x, v1.y, v2.x, v2.y, x2, y2 ) < 0.0 )
                {
                    this.add_vertex( vc, v1.x + dx1, v1.y - dy1 );
                    flag1 = false;
                }
            }
            if ( !flag1 )
                return;
            if ( lj != LineJoin.MiterRevert )
            {
                if ( lj == LineJoin.MiterRound )
                    this.calc_arc( vc, v1.x, v1.y, dx1, -dy1, dx2, -dy2 );
                else if ( flag2 )
                {
                    mlimit *= ( double ) this.m_width_sign;
                    this.add_vertex( vc, v1.x + dx1 + dy1 * mlimit, v1.y - dy1 + dx1 * mlimit );
                    this.add_vertex( vc, v1.x + dx2 - dy2 * mlimit, v1.y - dy2 - dx2 * mlimit );
                }
                else
                {
                    double num3 = v1.x + dx1;
                    double num4 = v1.y - dy1;
                    double num5 = v1.x + dx2;
                    double num6 = v1.y - dy2;
                    double num7 = (num2 - dbevel) / (num1 - dbevel);
                    this.add_vertex( vc, num3 + ( x1 - num3 ) * num7, num4 + ( y1 - num4 ) * num7 );
                    this.add_vertex( vc, num5 + ( x1 - num5 ) * num7, num6 + ( y1 - num6 ) * num7 );
                }
            }
            else
            {
                this.add_vertex( vc, v1.x + dx1, v1.y - dy1 );
                this.add_vertex( vc, v1.x + dx2, v1.y - dy2 );
            }
        }

        public enum status_e
        {
            initial,
            ready,
            cap1,
            cap2,
            outline1,
            close_first,
            outline2,
            out_vertices,
            end_poly1,
            end_poly2,
            stop,
        }
    }
}
