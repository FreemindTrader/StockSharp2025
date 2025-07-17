// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.dda_line_interpolator
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal sealed class dda_line_interpolator
    {
        private int m_y;
        private int m_inc;
        private int m_dy;
        private int m_FractionShift;

        public dda_line_interpolator( int FractionShift )
        {
            this.m_FractionShift = FractionShift;
        }

        public dda_line_interpolator( int y1, int y2, int count, int FractionShift )
        {
            this.m_FractionShift = FractionShift;
            this.m_y = y1;
            this.m_inc = ( y2 - y1 << this.m_FractionShift ) / count;
            this.m_dy = 0;
        }

        public void Next()
        {
            this.m_dy += this.m_inc;
        }

        public void Prev()
        {
            this.m_dy -= this.m_inc;
        }

        public void Next( int n )
        {
            this.m_dy += this.m_inc * n;
        }

        public void Prev( int n )
        {
            this.m_dy -= this.m_inc * n;
        }

        public int y()
        {
            return this.m_y + ( this.m_dy >> this.m_FractionShift );
        }

        public int dy()
        {
            return this.m_dy;
        }
    }
}
