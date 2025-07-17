// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.distance_interpolator1
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class distance_interpolator1
    {
        private int m_dx;
        private int m_dy;
        private int m_dist;

        public distance_interpolator1()
        {
        }

        public distance_interpolator1( int x1, int y1, int x2, int y2, int x, int y )
        {
            this.m_dx = x2 - x1;
            this.m_dy = y2 - y1;
            this.m_dist = agg_basics.iround( ( double ) ( x + 128 - x2 ) * ( double ) this.m_dy - ( double ) ( y + 128 - y2 ) * ( double ) this.m_dx );
            this.m_dx <<= 8;
            this.m_dy <<= 8;
        }

        public void inc_x()
        {
            this.m_dist += this.m_dy;
        }

        public void dec_x()
        {
            this.m_dist -= this.m_dy;
        }

        public void inc_y()
        {
            this.m_dist -= this.m_dx;
        }

        public void dec_y()
        {
            this.m_dist += this.m_dx;
        }

        public void inc_x( int dy )
        {
            this.m_dist += this.m_dy;
            if ( dy > 0 )
                this.m_dist -= this.m_dx;
            if ( dy >= 0 )
                return;
            this.m_dist += this.m_dx;
        }

        public void dec_x( int dy )
        {
            this.m_dist -= this.m_dy;
            if ( dy > 0 )
                this.m_dist -= this.m_dx;
            if ( dy >= 0 )
                return;
            this.m_dist += this.m_dx;
        }

        public void inc_y( int dx )
        {
            this.m_dist -= this.m_dx;
            if ( dx > 0 )
                this.m_dist += this.m_dy;
            if ( dx >= 0 )
                return;
            this.m_dist -= this.m_dy;
        }

        public void dec_y( int dx )
        {
            this.m_dist += this.m_dx;
            if ( dx > 0 )
                this.m_dist += this.m_dy;
            if ( dx >= 0 )
                return;
            this.m_dist -= this.m_dy;
        }

        public int dist()
        {
            return this.m_dist;
        }

        public int dx()
        {
            return this.m_dx;
        }

        public int dy()
        {
            return this.m_dy;
        }
    }
}
