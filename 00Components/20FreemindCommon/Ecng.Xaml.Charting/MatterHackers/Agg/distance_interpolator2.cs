// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.distance_interpolator2
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class distance_interpolator2
    {
        private int m_dx;
        private int m_dy;
        private int m_dx_start;
        private int m_dy_start;
        private int m_dist;
        private int m_dist_start;

        public distance_interpolator2()
        {
        }

        public distance_interpolator2( int x1, int y1, int x2, int y2, int sx, int sy, int x, int y )
        {
            this.m_dx = x2 - x1;
            this.m_dy = y2 - y1;
            this.m_dx_start = LineAABasics.line_mr( sx ) - LineAABasics.line_mr( x1 );
            this.m_dy_start = LineAABasics.line_mr( sy ) - LineAABasics.line_mr( y1 );
            this.m_dist = agg_basics.iround( ( double ) ( x + 128 - x2 ) * ( double ) this.m_dy - ( double ) ( y + 128 - y2 ) * ( double ) this.m_dx );
            this.m_dist_start = ( LineAABasics.line_mr( x + 128 ) - LineAABasics.line_mr( sx ) ) * this.m_dy_start - ( LineAABasics.line_mr( y + 128 ) - LineAABasics.line_mr( sy ) ) * this.m_dx_start;
            this.m_dx <<= 8;
            this.m_dy <<= 8;
            this.m_dx_start <<= 4;
            this.m_dy_start <<= 4;
        }

        public distance_interpolator2( int x1, int y1, int x2, int y2, int ex, int ey, int x, int y, int none )
        {
            this.m_dx = x2 - x1;
            this.m_dy = y2 - y1;
            this.m_dx_start = LineAABasics.line_mr( ex ) - LineAABasics.line_mr( x2 );
            this.m_dy_start = LineAABasics.line_mr( ey ) - LineAABasics.line_mr( y2 );
            this.m_dist = agg_basics.iround( ( double ) ( x + 128 - x2 ) * ( double ) this.m_dy - ( double ) ( y + 128 - y2 ) * ( double ) this.m_dx );
            this.m_dist_start = ( LineAABasics.line_mr( x + 128 ) - LineAABasics.line_mr( ex ) ) * this.m_dy_start - ( LineAABasics.line_mr( y + 128 ) - LineAABasics.line_mr( ey ) ) * this.m_dx_start;
            this.m_dx <<= 8;
            this.m_dy <<= 8;
            this.m_dx_start <<= 4;
            this.m_dy_start <<= 4;
        }

        public void inc_x()
        {
            this.m_dist += this.m_dy;
            this.m_dist_start += this.m_dy_start;
        }

        public void dec_x()
        {
            this.m_dist -= this.m_dy;
            this.m_dist_start -= this.m_dy_start;
        }

        public void inc_y()
        {
            this.m_dist -= this.m_dx;
            this.m_dist_start -= this.m_dx_start;
        }

        public void dec_y()
        {
            this.m_dist += this.m_dx;
            this.m_dist_start += this.m_dx_start;
        }

        public void inc_x( int dy )
        {
            this.m_dist += this.m_dy;
            this.m_dist_start += this.m_dy_start;
            if ( dy > 0 )
            {
                this.m_dist -= this.m_dx;
                this.m_dist_start -= this.m_dx_start;
            }
            if ( dy >= 0 )
                return;
            this.m_dist += this.m_dx;
            this.m_dist_start += this.m_dx_start;
        }

        public void dec_x( int dy )
        {
            this.m_dist -= this.m_dy;
            this.m_dist_start -= this.m_dy_start;
            if ( dy > 0 )
            {
                this.m_dist -= this.m_dx;
                this.m_dist_start -= this.m_dx_start;
            }
            if ( dy >= 0 )
                return;
            this.m_dist += this.m_dx;
            this.m_dist_start += this.m_dx_start;
        }

        public void inc_y( int dx )
        {
            this.m_dist -= this.m_dx;
            this.m_dist_start -= this.m_dx_start;
            if ( dx > 0 )
            {
                this.m_dist += this.m_dy;
                this.m_dist_start += this.m_dy_start;
            }
            if ( dx >= 0 )
                return;
            this.m_dist -= this.m_dy;
            this.m_dist_start -= this.m_dy_start;
        }

        public void dec_y( int dx )
        {
            this.m_dist += this.m_dx;
            this.m_dist_start += this.m_dx_start;
            if ( dx > 0 )
            {
                this.m_dist += this.m_dy;
                this.m_dist_start += this.m_dy_start;
            }
            if ( dx >= 0 )
                return;
            this.m_dist -= this.m_dy;
            this.m_dist_start -= this.m_dy_start;
        }

        public int dist()
        {
            return this.m_dist;
        }

        public int dist_start()
        {
            return this.m_dist_start;
        }

        public int dist_end()
        {
            return this.m_dist_start;
        }

        public int dx()
        {
            return this.m_dx;
        }

        public int dy()
        {
            return this.m_dy;
        }

        public int dx_start()
        {
            return this.m_dx_start;
        }

        public int dy_start()
        {
            return this.m_dy_start;
        }

        public int dx_end()
        {
            return this.m_dx_start;
        }

        public int dy_end()
        {
            return this.m_dy_start;
        }
    }
}
