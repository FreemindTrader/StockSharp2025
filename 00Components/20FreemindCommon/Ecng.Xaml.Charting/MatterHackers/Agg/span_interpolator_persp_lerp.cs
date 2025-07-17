// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.span_interpolator_persp_lerp
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using MatterHackers.Agg.Transform;

namespace MatterHackers.Agg
{
    internal class span_interpolator_persp_lerp : ISpanInterpolator
    {
        private Perspective m_trans_dir;
        private Perspective m_trans_inv;
        private dda2_line_interpolator m_coord_x;
        private dda2_line_interpolator m_coord_y;
        private dda2_line_interpolator m_scale_x;
        private dda2_line_interpolator m_scale_y;
        private const int subpixel_shift = 8;
        private const int subpixel_scale = 256;

        public span_interpolator_persp_lerp()
        {
            this.m_trans_dir = new Perspective();
            this.m_trans_inv = new Perspective();
        }

        public span_interpolator_persp_lerp( double[ ] src, double[ ] dst )
          : this()
        {
            this.quad_to_quad( src, dst );
        }

        public span_interpolator_persp_lerp( double x1, double y1, double x2, double y2, double[ ] quad )
          : this()
        {
            this.rect_to_quad( x1, y1, x2, y2, quad );
        }

        public span_interpolator_persp_lerp( double[ ] quad, double x1, double y1, double x2, double y2 )
          : this()
        {
            this.quad_to_rect( quad, x1, y1, x2, y2 );
        }

        public void quad_to_quad( double[ ] src, double[ ] dst )
        {
            this.m_trans_dir.quad_to_quad( src, dst );
            this.m_trans_inv.quad_to_quad( dst, src );
        }

        public void rect_to_quad( double x1, double y1, double x2, double y2, double[ ] quad )
        {
            double[] src = new double[8];
            src[ 0 ] = src[ 6 ] = x1;
            src[ 2 ] = src[ 4 ] = x2;
            src[ 1 ] = src[ 3 ] = y1;
            src[ 5 ] = src[ 7 ] = y2;
            this.quad_to_quad( src, quad );
        }

        public void quad_to_rect( double[ ] quad, double x1, double y1, double x2, double y2 )
        {
            double[] dst = new double[8];
            dst[ 0 ] = dst[ 6 ] = x1;
            dst[ 2 ] = dst[ 4 ] = x2;
            dst[ 1 ] = dst[ 3 ] = y1;
            dst[ 5 ] = dst[ 7 ] = y2;
            this.quad_to_quad( quad, dst );
        }

        public bool is_valid()
        {
            return this.m_trans_dir.is_valid();
        }

        public void begin( double x, double y, int len )
        {
            double px1 = x;
            double py1 = y;
            this.m_trans_dir.transform( ref px1, ref py1 );
            int y1_1 = agg_basics.iround(px1 * 256.0);
            int y1_2 = agg_basics.iround(py1 * 256.0);
            double num1 = 1.0 / 256.0;
            double px2 = px1 + num1;
            double py2 = py1;
            this.m_trans_inv.transform( ref px2, ref py2 );
            double num2 = px2 - x;
            double num3 = py2 - y;
            int y1_3 = agg_basics.uround(256.0 / Math.Sqrt(num2 * num2 + num3 * num3)) >> 8;
            double px3 = px1;
            double py3 = py1 + num1;
            this.m_trans_inv.transform( ref px3, ref py3 );
            double num4 = px3 - x;
            double num5 = py3 - y;
            int y1_4 = agg_basics.uround(256.0 / Math.Sqrt(num4 * num4 + num5 * num5)) >> 8;
            x += ( double ) len;
            double px4 = x;
            double py4 = y;
            this.m_trans_dir.transform( ref px4, ref py4 );
            int y2_1 = agg_basics.iround(px4 * 256.0);
            int y2_2 = agg_basics.iround(py4 * 256.0);
            double px5 = px4 + num1;
            double py5 = py4;
            this.m_trans_inv.transform( ref px5, ref py5 );
            double num6 = px5 - x;
            double num7 = py5 - y;
            int y2_3 = agg_basics.uround(256.0 / Math.Sqrt(num6 * num6 + num7 * num7)) >> 8;
            double px6 = px4;
            double py6 = py4 + num1;
            this.m_trans_inv.transform( ref px6, ref py6 );
            double num8 = px6 - x;
            double num9 = py6 - y;
            int y2_4 = agg_basics.uround(256.0 / Math.Sqrt(num8 * num8 + num9 * num9)) >> 8;
            this.m_coord_x = new dda2_line_interpolator( y1_1, y2_1, len );
            this.m_coord_y = new dda2_line_interpolator( y1_2, y2_2, len );
            this.m_scale_x = new dda2_line_interpolator( y1_3, y2_3, len );
            this.m_scale_y = new dda2_line_interpolator( y1_4, y2_4, len );
        }

        public void resynchronize( double xe, double ye, int len )
        {
            int y1_1 = this.m_coord_x.y();
            int y1_2 = this.m_coord_y.y();
            int y1_3 = this.m_scale_x.y();
            int y1_4 = this.m_scale_y.y();
            double px1 = xe;
            double py1 = ye;
            this.m_trans_dir.transform( ref px1, ref py1 );
            int y2_1 = agg_basics.iround(px1 * 256.0);
            int y2_2 = agg_basics.iround(py1 * 256.0);
            double num1 = 1.0 / 256.0;
            double px2 = px1 + num1;
            double py2 = py1;
            this.m_trans_inv.transform( ref px2, ref py2 );
            double num2 = px2 - xe;
            double num3 = py2 - ye;
            int y2_3 = agg_basics.uround(256.0 / Math.Sqrt(num2 * num2 + num3 * num3)) >> 8;
            double px3 = px1;
            double py3 = py1 + num1;
            this.m_trans_inv.transform( ref px3, ref py3 );
            double num4 = px3 - xe;
            double num5 = py3 - ye;
            int y2_4 = agg_basics.uround(256.0 / Math.Sqrt(num4 * num4 + num5 * num5)) >> 8;
            this.m_coord_x = new dda2_line_interpolator( y1_1, y2_1, len );
            this.m_coord_y = new dda2_line_interpolator( y1_2, y2_2, len );
            this.m_scale_x = new dda2_line_interpolator( y1_3, y2_3, len );
            this.m_scale_y = new dda2_line_interpolator( y1_4, y2_4, len );
        }

        public ITransform transformer()
        {
            throw new NotImplementedException();
        }

        public void transformer( ITransform trans )
        {
            throw new NotImplementedException();
        }

        public void Next()
        {
            this.m_coord_x.Next();
            this.m_coord_y.Next();
            this.m_scale_x.Next();
            this.m_scale_y.Next();
        }

        public void coordinates( out int x, out int y )
        {
            x = this.m_coord_x.y();
            y = this.m_coord_y.y();
        }

        public void local_scale( out int x, out int y )
        {
            x = this.m_scale_x.y();
            y = this.m_scale_y.y();
        }

        public void transform( ref double x, ref double y )
        {
            this.m_trans_dir.transform( ref x, ref y );
        }
    }
}
