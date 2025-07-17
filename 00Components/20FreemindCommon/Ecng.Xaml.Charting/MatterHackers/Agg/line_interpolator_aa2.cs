// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.line_interpolator_aa2
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class line_interpolator_aa2 : line_interpolator_aa_base
    {
        private distance_interpolator2 m_di;

        public line_interpolator_aa2( OutlineRenderer ren, line_parameters lp, int ex, int ey )
          : base( ren, lp )
        {
            this.m_di = new distance_interpolator2( lp.x1, lp.y1, lp.x2, lp.y2, ex, ey, lp.x1 & -256, lp.y1 & -256, 0 );
            this.m_li.adjust_forward();
            this.m_step -= this.m_max_extent;
        }

        public bool step_hor()
        {
            int d1 = this.step_hor_base(this.m_di);
            int coversOffset = 66;
            int index1 = coversOffset;
            int num1 = this.m_di.dist_end();
            int num2 = 0;
            this.m_covers[ index1 ] = ( byte ) 0;
            if ( num1 > 0 )
            {
                this.m_covers[ index1 ] = ( byte ) this.m_ren.cover( d1 );
                ++num2;
            }
            int index2 = index1 + 1;
            int d2;
            for ( int index3 = 1 ; ( d2 = this.m_dist[ index3 ] - d1 ) <= this.m_width ; ++index3 )
            {
                num1 -= this.m_di.dx_end();
                this.m_covers[ index2 ] = ( byte ) 0;
                if ( num1 > 0 )
                {
                    this.m_covers[ index2 ] = ( byte ) this.m_ren.cover( d2 );
                    ++num2;
                }
                ++index2;
            }
            int index4 = 1;
            int num3 = this.m_di.dist_end();
            int d3;
            for ( ; ( d3 = this.m_dist[ index4 ] + d1 ) <= this.m_width ; ++index4 )
            {
                num3 += this.m_di.dx_end();
                this.m_covers[ --coversOffset ] = ( byte ) 0;
                if ( num3 > 0 )
                {
                    this.m_covers[ coversOffset ] = ( byte ) this.m_ren.cover( d3 );
                    ++num2;
                }
            }
            this.m_ren.blend_solid_vspan( this.m_x, this.m_y - index4 + 1, index2 - coversOffset, this.m_covers, coversOffset );
            if ( num2 == 0 )
                return false;
            return ++this.m_step < this.m_count;
        }

        public bool step_ver()
        {
            int d1 = this.step_ver_base(this.m_di);
            int coversOffset = 66;
            int index1 = coversOffset;
            int num1 = this.m_di.dist_end();
            int num2 = 0;
            this.m_covers[ index1 ] = ( byte ) 0;
            if ( num1 > 0 )
            {
                this.m_covers[ index1 ] = ( byte ) this.m_ren.cover( d1 );
                ++num2;
            }
            int index2 = index1 + 1;
            int d2;
            for ( int index3 = 1 ; ( d2 = this.m_dist[ index3 ] - d1 ) <= this.m_width ; ++index3 )
            {
                num1 += this.m_di.dy_end();
                this.m_covers[ index2 ] = ( byte ) 0;
                if ( num1 > 0 )
                {
                    this.m_covers[ index2 ] = ( byte ) this.m_ren.cover( d2 );
                    ++num2;
                }
                ++index2;
            }
            int index4 = 1;
            int num3 = this.m_di.dist_end();
            int d3;
            for ( ; ( d3 = this.m_dist[ index4 ] + d1 ) <= this.m_width ; ++index4 )
            {
                num3 -= this.m_di.dy_end();
                this.m_covers[ --coversOffset ] = ( byte ) 0;
                if ( num3 > 0 )
                {
                    this.m_covers[ coversOffset ] = ( byte ) this.m_ren.cover( d3 );
                    ++num2;
                }
            }
            this.m_ren.blend_solid_hspan( this.m_x - index4 + 1, this.m_y, index2 - coversOffset, this.m_covers, coversOffset );
            if ( num2 == 0 )
                return false;
            return ++this.m_step < this.m_count;
        }
    }
}
