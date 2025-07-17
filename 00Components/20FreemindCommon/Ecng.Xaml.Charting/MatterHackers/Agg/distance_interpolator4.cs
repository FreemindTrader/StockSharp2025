// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.distance_interpolator4
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class distance_interpolator4
    {
        private int m_dx;
        private int m_dy;
        private int m_dx_start;
        private int m_dy_start;
        private int m_dx_pict;
        private int m_dy_pict;
        private int m_dx_end;
        private int m_dy_end;
        private int m_dist;
        private int m_dist_start;
        private int m_dist_pict;
        private int m_dist_end;
        private int m_len;

        public distance_interpolator4()
        {
        }

        public distance_interpolator4( int x1, int y1, int x2, int y2, int sx, int sy, int ex, int ey, int len, double scale, int x, int y )
        {
            this.m_dx = x2 - x1;
            this.m_dy = y2 - y1;
            this.m_dx_start = LineAABasics.line_mr( sx ) - LineAABasics.line_mr( x1 );
            this.m_dy_start = LineAABasics.line_mr( sy ) - LineAABasics.line_mr( y1 );
            this.m_dx_end = LineAABasics.line_mr( ex ) - LineAABasics.line_mr( x2 );
            this.m_dy_end = LineAABasics.line_mr( ey ) - LineAABasics.line_mr( y2 );
            this.m_dist = agg_basics.iround( ( double ) ( x + 128 - x2 ) * ( double ) this.m_dy - ( double ) ( y + 128 - y2 ) * ( double ) this.m_dx );
            this.m_dist_start = ( LineAABasics.line_mr( x + 128 ) - LineAABasics.line_mr( sx ) ) * this.m_dy_start - ( LineAABasics.line_mr( y + 128 ) - LineAABasics.line_mr( sy ) ) * this.m_dx_start;
            this.m_dist_end = ( LineAABasics.line_mr( x + 128 ) - LineAABasics.line_mr( ex ) ) * this.m_dy_end - ( LineAABasics.line_mr( y + 128 ) - LineAABasics.line_mr( ey ) ) * this.m_dx_end;
            this.m_len = agg_basics.uround( ( double ) len / scale );
            double num1 = (double) len * scale;
            int num2 = agg_basics.iround((double) (x2 - x1 << 8) / num1);
            int num3 = agg_basics.iround((double) (y2 - y1 << 8) / num1);
            this.m_dx_pict = -num3;
            this.m_dy_pict = num2;
            this.m_dist_pict = ( x + 128 - ( x1 - num3 ) ) * this.m_dy_pict - ( y + 128 - ( y1 + num2 ) ) * this.m_dx_pict >> 8;
            this.m_dx <<= 8;
            this.m_dy <<= 8;
            this.m_dx_start <<= 4;
            this.m_dy_start <<= 4;
            this.m_dx_end <<= 4;
            this.m_dy_end <<= 4;
        }

        public void inc_x()
        {
            this.m_dist += this.m_dy;
            this.m_dist_start += this.m_dy_start;
            this.m_dist_pict += this.m_dy_pict;
            this.m_dist_end += this.m_dy_end;
        }

        public void dec_x()
        {
            this.m_dist -= this.m_dy;
            this.m_dist_start -= this.m_dy_start;
            this.m_dist_pict -= this.m_dy_pict;
            this.m_dist_end -= this.m_dy_end;
        }

        public void inc_y()
        {
            this.m_dist -= this.m_dx;
            this.m_dist_start -= this.m_dx_start;
            this.m_dist_pict -= this.m_dx_pict;
            this.m_dist_end -= this.m_dx_end;
        }

        public void dec_y()
        {
            this.m_dist += this.m_dx;
            this.m_dist_start += this.m_dx_start;
            this.m_dist_pict += this.m_dx_pict;
            this.m_dist_end += this.m_dx_end;
        }

        public void inc_x( int dy )
        {
            this.m_dist += this.m_dy;
            this.m_dist_start += this.m_dy_start;
            this.m_dist_pict += this.m_dy_pict;
            this.m_dist_end += this.m_dy_end;
            if ( dy > 0 )
            {
                this.m_dist -= this.m_dx;
                this.m_dist_start -= this.m_dx_start;
                this.m_dist_pict -= this.m_dx_pict;
                this.m_dist_end -= this.m_dx_end;
            }
            if ( dy >= 0 )
                return;
            this.m_dist += this.m_dx;
            this.m_dist_start += this.m_dx_start;
            this.m_dist_pict += this.m_dx_pict;
            this.m_dist_end += this.m_dx_end;
        }

        public void dec_x( int dy )
        {
            this.m_dist -= this.m_dy;
            this.m_dist_start -= this.m_dy_start;
            this.m_dist_pict -= this.m_dy_pict;
            this.m_dist_end -= this.m_dy_end;
            if ( dy > 0 )
            {
                this.m_dist -= this.m_dx;
                this.m_dist_start -= this.m_dx_start;
                this.m_dist_pict -= this.m_dx_pict;
                this.m_dist_end -= this.m_dx_end;
            }
            if ( dy >= 0 )
                return;
            this.m_dist += this.m_dx;
            this.m_dist_start += this.m_dx_start;
            this.m_dist_pict += this.m_dx_pict;
            this.m_dist_end += this.m_dx_end;
        }

        public void inc_y( int dx )
        {
            this.m_dist -= this.m_dx;
            this.m_dist_start -= this.m_dx_start;
            this.m_dist_pict -= this.m_dx_pict;
            this.m_dist_end -= this.m_dx_end;
            if ( dx > 0 )
            {
                this.m_dist += this.m_dy;
                this.m_dist_start += this.m_dy_start;
                this.m_dist_pict += this.m_dy_pict;
                this.m_dist_end += this.m_dy_end;
            }
            if ( dx >= 0 )
                return;
            this.m_dist -= this.m_dy;
            this.m_dist_start -= this.m_dy_start;
            this.m_dist_pict -= this.m_dy_pict;
            this.m_dist_end -= this.m_dy_end;
        }

        public void dec_y( int dx )
        {
            this.m_dist += this.m_dx;
            this.m_dist_start += this.m_dx_start;
            this.m_dist_pict += this.m_dx_pict;
            this.m_dist_end += this.m_dx_end;
            if ( dx > 0 )
            {
                this.m_dist += this.m_dy;
                this.m_dist_start += this.m_dy_start;
                this.m_dist_pict += this.m_dy_pict;
                this.m_dist_end += this.m_dy_end;
            }
            if ( dx >= 0 )
                return;
            this.m_dist -= this.m_dy;
            this.m_dist_start -= this.m_dy_start;
            this.m_dist_pict -= this.m_dy_pict;
            this.m_dist_end -= this.m_dy_end;
        }

        public int dist()
        {
            return this.m_dist;
        }

        public int dist_start()
        {
            return this.m_dist_start;
        }

        public int dist_pict()
        {
            return this.m_dist_pict;
        }

        public int dist_end()
        {
            return this.m_dist_end;
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

        public int dx_pict()
        {
            return this.m_dx_pict;
        }

        public int dy_pict()
        {
            return this.m_dy_pict;
        }

        public int dx_end()
        {
            return this.m_dx_end;
        }

        public int dy_end()
        {
            return this.m_dy_end;
        }

        public int len()
        {
            return this.m_len;
        }
    }
}
