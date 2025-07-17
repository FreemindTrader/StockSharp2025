// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.line_interpolator_aa_base
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg
{
    internal class line_interpolator_aa_base
    {
        protected int[] m_dist = new int[65];
        protected byte[] m_covers = new byte[132];
        protected line_parameters m_lp;
        protected dda2_line_interpolator m_li;
        protected OutlineRenderer m_ren;
        private int m_len;
        protected int m_x;
        protected int m_y;
        protected int m_old_x;
        protected int m_old_y;
        protected int m_count;
        protected int m_width;
        protected int m_max_extent;
        protected int m_step;
        protected const int max_half_width = 64;

        public line_interpolator_aa_base( OutlineRenderer ren, line_parameters lp )
        {
            this.m_lp = lp;
            this.m_li = new dda2_line_interpolator( lp.vertical ? LineAABasics.line_dbl_hr( lp.x2 - lp.x1 ) : LineAABasics.line_dbl_hr( lp.y2 - lp.y1 ), lp.vertical ? Math.Abs( lp.y2 - lp.y1 ) : Math.Abs( lp.x2 - lp.x1 ) + 1 );
            this.m_ren = ren;
            this.m_len = lp.vertical == lp.inc > 0 ? -lp.len : lp.len;
            this.m_x = lp.x1 >> 8;
            this.m_y = lp.y1 >> 8;
            this.m_old_x = this.m_x;
            this.m_old_y = this.m_y;
            this.m_count = lp.vertical ? Math.Abs( ( lp.y2 >> 8 ) - this.m_y ) : Math.Abs( ( lp.x2 >> 8 ) - this.m_x );
            this.m_width = ren.subpixel_width();
            this.m_max_extent = this.m_width + ( int ) byte.MaxValue >> 8;
            this.m_step = 0;
            dda2_line_interpolator lineInterpolator = new dda2_line_interpolator(0, lp.vertical ? lp.dy << 8 : lp.dx << 8, lp.len);
            int num1 = this.m_width + 512;
            int index1;
            for ( index1 = 0 ; index1 < 64 ; ++index1 )
            {
                this.m_dist[ index1 ] = lineInterpolator.y();
                if ( this.m_dist[ index1 ] < num1 )
                    lineInterpolator.Next();
                else
                    break;
            }
            int[] dist = this.m_dist;
            int index2 = index1;
            int num2 = index2 + 1;
            int num3 = 2147418112;
            dist[ index2 ] = num3;
        }

        public int step_hor_base( distance_interpolator1 di )
        {
            this.m_li.Next();
            this.m_x += this.m_lp.inc;
            this.m_y = this.m_lp.y1 + this.m_li.y() >> 8;
            if ( this.m_lp.inc > 0 )
                di.inc_x( this.m_y - this.m_old_y );
            else
                di.dec_x( this.m_y - this.m_old_y );
            this.m_old_y = this.m_y;
            return di.dist() / this.m_len;
        }

        public int step_hor_base( distance_interpolator2 di )
        {
            this.m_li.Next();
            this.m_x += this.m_lp.inc;
            this.m_y = this.m_lp.y1 + this.m_li.y() >> 8;
            if ( this.m_lp.inc > 0 )
                di.inc_x( this.m_y - this.m_old_y );
            else
                di.dec_x( this.m_y - this.m_old_y );
            this.m_old_y = this.m_y;
            return di.dist() / this.m_len;
        }

        public int step_hor_base( distance_interpolator3 di )
        {
            this.m_li.Next();
            this.m_x += this.m_lp.inc;
            this.m_y = this.m_lp.y1 + this.m_li.y() >> 8;
            if ( this.m_lp.inc > 0 )
                di.inc_x( this.m_y - this.m_old_y );
            else
                di.dec_x( this.m_y - this.m_old_y );
            this.m_old_y = this.m_y;
            return di.dist() / this.m_len;
        }

        public int step_ver_base( distance_interpolator1 di )
        {
            this.m_li.Next();
            this.m_y += this.m_lp.inc;
            this.m_x = this.m_lp.x1 + this.m_li.y() >> 8;
            if ( this.m_lp.inc > 0 )
                di.inc_y( this.m_x - this.m_old_x );
            else
                di.dec_y( this.m_x - this.m_old_x );
            this.m_old_x = this.m_x;
            return di.dist() / this.m_len;
        }

        public int step_ver_base( distance_interpolator2 di )
        {
            this.m_li.Next();
            this.m_y += this.m_lp.inc;
            this.m_x = this.m_lp.x1 + this.m_li.y() >> 8;
            if ( this.m_lp.inc > 0 )
                di.inc_y( this.m_x - this.m_old_x );
            else
                di.dec_y( this.m_x - this.m_old_x );
            this.m_old_x = this.m_x;
            return di.dist() / this.m_len;
        }

        public int step_ver_base( distance_interpolator3 di )
        {
            this.m_li.Next();
            this.m_y += this.m_lp.inc;
            this.m_x = this.m_lp.x1 + this.m_li.y() >> 8;
            if ( this.m_lp.inc > 0 )
                di.inc_y( this.m_x - this.m_old_x );
            else
                di.dec_y( this.m_x - this.m_old_x );
            this.m_old_x = this.m_x;
            return di.dist() / this.m_len;
        }

        public bool vertical()
        {
            return this.m_lp.vertical;
        }

        public int width()
        {
            return this.m_width;
        }

        public int count()
        {
            return this.m_count;
        }
    }
}
