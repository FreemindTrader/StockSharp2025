// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.line_interpolator_aa0
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class line_interpolator_aa0 : line_interpolator_aa_base
    {
        private distance_interpolator1 m_di;

        public line_interpolator_aa0( OutlineRenderer ren, line_parameters lp )
          : base( ren, lp )
        {
            this.m_di = new distance_interpolator1( lp.x1, lp.y1, lp.x2, lp.y2, lp.x1 & -256, lp.y1 & -256 );
            this.m_li.adjust_forward();
        }

        public bool step_hor()
        {
            int d1 = this.step_hor_base(this.m_di);
            int coversOffset = 66;
            int num1 = coversOffset;
            byte[] covers = this.m_covers;
            int index1 = num1;
            int num2 = index1 + 1;
            int num3 = (int) (byte) this.m_ren.cover(d1);
            covers[ index1 ] = ( byte ) num3;
            int d2;
            for ( int index2 = 1 ; ( d2 = this.m_dist[ index2 ] - d1 ) <= this.m_width ; ++index2 )
                this.m_covers[ num2++ ] = ( byte ) this.m_ren.cover( d2 );
            int index3;
            int d3;
            for ( index3 = 1 ; ( d3 = this.m_dist[ index3 ] + d1 ) <= this.m_width ; ++index3 )
                this.m_covers[ --coversOffset ] = ( byte ) this.m_ren.cover( d3 );
            this.m_ren.blend_solid_vspan( this.m_x, this.m_y - index3 + 1, num2 - coversOffset, this.m_covers, coversOffset );
            return ++this.m_step < this.m_count;
        }

        public bool step_ver()
        {
            int d1 = this.step_ver_base(this.m_di);
            int coversOffset = 66;
            int num1 = coversOffset;
            byte[] covers = this.m_covers;
            int index1 = num1;
            int num2 = index1 + 1;
            int num3 = (int) (byte) this.m_ren.cover(d1);
            covers[ index1 ] = ( byte ) num3;
            int d2;
            for ( int index2 = 1 ; ( d2 = this.m_dist[ index2 ] - d1 ) <= this.m_width ; ++index2 )
                this.m_covers[ num2++ ] = ( byte ) this.m_ren.cover( d2 );
            int index3;
            int d3;
            for ( index3 = 1 ; ( d3 = this.m_dist[ index3 ] + d1 ) <= this.m_width ; ++index3 )
                this.m_covers[ --coversOffset ] = ( byte ) this.m_ren.cover( d3 );
            this.m_ren.blend_solid_hspan( this.m_x - index3 + 1, this.m_y, num2 - coversOffset, this.m_covers, coversOffset );
            return ++this.m_step < this.m_count;
        }
    }
}
