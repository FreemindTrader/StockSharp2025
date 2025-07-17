// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.line_interpolator_aa1
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class line_interpolator_aa1 : line_interpolator_aa_base
    {
        private distance_interpolator2 m_di;

        public line_interpolator_aa1( OutlineRenderer ren, line_parameters lp, int sx, int sy )
          : base( ren, lp )
        {
            this.m_di = new distance_interpolator2( lp.x1, lp.y1, lp.x2, lp.y2, sx, sy, lp.x1 & -256, lp.y1 & -256 );
            int num1 = 1;
            if ( lp.vertical )
            {
                do
                {
                    this.m_li.Prev();
                    this.m_y -= lp.inc;
                    this.m_x = this.m_lp.x1 + this.m_li.y() >> 8;
                    if ( lp.inc > 0 )
                        this.m_di.dec_y( this.m_x - this.m_old_x );
                    else
                        this.m_di.inc_y( this.m_x - this.m_old_x );
                    this.m_old_x = this.m_x;
                    int num2;
                    int num3 = num2 = this.m_di.dist_start();
                    int index = 0;
                    if ( num3 < 0 )
                        ++num1;
                    do
                    {
                        num3 += this.m_di.dy_start();
                        num2 -= this.m_di.dy_start();
                        if ( num3 < 0 )
                            ++num1;
                        if ( num2 < 0 )
                            ++num1;
                        ++index;
                    }
                    while ( this.m_dist[ index ] <= this.m_width );
                    --this.m_step;
                    if ( num1 != 0 )
                        num1 = 0;
                    else
                        break;
                }
                while ( this.m_step >= -this.m_max_extent );
            }
            else
            {
                do
                {
                    this.m_li.Prev();
                    this.m_x -= lp.inc;
                    this.m_y = this.m_lp.y1 + this.m_li.y() >> 8;
                    if ( lp.inc > 0 )
                        this.m_di.dec_x( this.m_y - this.m_old_y );
                    else
                        this.m_di.inc_x( this.m_y - this.m_old_y );
                    this.m_old_y = this.m_y;
                    int num2;
                    int num3 = num2 = this.m_di.dist_start();
                    int index = 0;
                    if ( num3 < 0 )
                        ++num1;
                    do
                    {
                        num3 -= this.m_di.dx_start();
                        num2 += this.m_di.dx_start();
                        if ( num3 < 0 )
                            ++num1;
                        if ( num2 < 0 )
                            ++num1;
                        ++index;
                    }
                    while ( this.m_dist[ index ] <= this.m_width );
                    --this.m_step;
                    if ( num1 != 0 )
                        num1 = 0;
                    else
                        break;
                }
                while ( this.m_step >= -this.m_max_extent );
            }
            this.m_li.adjust_forward();
        }

        public bool step_hor()
        {
            int d1 = this.step_hor_base(this.m_di);
            int num1 = this.m_di.dist_start();
            int coversOffset = 66;
            int index1 = coversOffset;
            this.m_covers[ index1 ] = ( byte ) 0;
            if ( num1 <= 0 )
                this.m_covers[ index1 ] = ( byte ) this.m_ren.cover( d1 );
            int index2 = index1 + 1;
            int d2;
            for ( int index3 = 1 ; ( d2 = this.m_dist[ index3 ] - d1 ) <= this.m_width ; ++index3 )
            {
                num1 -= this.m_di.dx_start();
                this.m_covers[ index2 ] = ( byte ) 0;
                if ( num1 <= 0 )
                    this.m_covers[ index2 ] = ( byte ) this.m_ren.cover( d2 );
                ++index2;
            }
            int index4 = 1;
            int num2 = this.m_di.dist_start();
            int d3;
            for ( ; ( d3 = this.m_dist[ index4 ] + d1 ) <= this.m_width ; ++index4 )
            {
                num2 += this.m_di.dx_start();
                this.m_covers[ --coversOffset ] = ( byte ) 0;
                if ( num2 <= 0 )
                    this.m_covers[ coversOffset ] = ( byte ) this.m_ren.cover( d3 );
            }
            int len = index2 - coversOffset;
            this.m_ren.blend_solid_vspan( this.m_x, this.m_y - index4 + 1, len, this.m_covers, coversOffset );
            return ++this.m_step < this.m_count;
        }

        public bool step_ver()
        {
            int d1 = this.step_ver_base(this.m_di);
            int coversOffset = 66;
            int index1 = coversOffset;
            int num1 = this.m_di.dist_start();
            this.m_covers[ index1 ] = ( byte ) 0;
            if ( num1 <= 0 )
                this.m_covers[ index1 ] = ( byte ) this.m_ren.cover( d1 );
            int index2 = index1 + 1;
            int d2;
            for ( int index3 = 1 ; ( d2 = this.m_dist[ index3 ] - d1 ) <= this.m_width ; ++index3 )
            {
                num1 += this.m_di.dy_start();
                this.m_covers[ index2 ] = ( byte ) 0;
                if ( num1 <= 0 )
                    this.m_covers[ index2 ] = ( byte ) this.m_ren.cover( d2 );
                ++index2;
            }
            int index4 = 1;
            int num2 = this.m_di.dist_start();
            int d3;
            for ( ; ( d3 = this.m_dist[ index4 ] + d1 ) <= this.m_width ; ++index4 )
            {
                num2 -= this.m_di.dy_start();
                this.m_covers[ --coversOffset ] = ( byte ) 0;
                if ( num2 <= 0 )
                    this.m_covers[ coversOffset ] = ( byte ) this.m_ren.cover( d3 );
            }
            this.m_ren.blend_solid_hspan( this.m_x - index4 + 1, this.m_y, index2 - coversOffset, this.m_covers, coversOffset );
            return ++this.m_step < this.m_count;
        }
    }
}
