// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.dda2_line_interpolator
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal sealed class dda2_line_interpolator
    {
        private int m_cnt;
        private int m_lft;
        private int m_rem;
        private int m_mod;
        private int m_y;

        public dda2_line_interpolator()
        {
        }

        public dda2_line_interpolator( int y1, int y2, int count )
        {
            this.m_cnt = count <= 0 ? 1 : count;
            this.m_lft = ( y2 - y1 ) / this.m_cnt;
            this.m_rem = ( y2 - y1 ) % this.m_cnt;
            this.m_mod = this.m_rem;
            this.m_y = y1;
            if ( this.m_mod <= 0 )
            {
                this.m_mod += count;
                this.m_rem += count;
                --this.m_lft;
            }
            this.m_mod -= count;
        }

        public dda2_line_interpolator( int y1, int y2, int count, int unused )
        {
            this.m_cnt = count <= 0 ? 1 : count;
            this.m_lft = ( y2 - y1 ) / this.m_cnt;
            this.m_rem = ( y2 - y1 ) % this.m_cnt;
            this.m_mod = this.m_rem;
            this.m_y = y1;
            if ( this.m_mod > 0 )
                return;
            this.m_mod += count;
            this.m_rem += count;
            --this.m_lft;
        }

        public dda2_line_interpolator( int y, int count )
        {
            this.m_cnt = count <= 0 ? 1 : count;
            this.m_lft = y / this.m_cnt;
            this.m_rem = y % this.m_cnt;
            this.m_mod = this.m_rem;
            this.m_y = 0;
            if ( this.m_mod > 0 )
                return;
            this.m_mod += count;
            this.m_rem += count;
            --this.m_lft;
        }

        public void Next()
        {
            this.m_mod += this.m_rem;
            this.m_y += this.m_lft;
            if ( this.m_mod <= 0 )
                return;
            this.m_mod -= this.m_cnt;
            ++this.m_y;
        }

        public void Prev()
        {
            if ( this.m_mod <= this.m_rem )
            {
                this.m_mod += this.m_cnt;
                --this.m_y;
            }
            this.m_mod -= this.m_rem;
            this.m_y -= this.m_lft;
        }

        public void adjust_forward()
        {
            this.m_mod -= this.m_cnt;
        }

        public void adjust_backward()
        {
            this.m_mod += this.m_cnt;
        }

        public int mod()
        {
            return this.m_mod;
        }

        public int rem()
        {
            return this.m_rem;
        }

        public int lft()
        {
            return this.m_lft;
        }

        public int y()
        {
            return this.m_y;
        }

        private enum save_size_e
        {
            save_size = 2,
        }
    }
}
