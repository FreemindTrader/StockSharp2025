// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.ellipse_bresenham_interpolator
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class ellipse_bresenham_interpolator
    {
        private int m_rx2;
        private int m_ry2;
        private int m_two_rx2;
        private int m_two_ry2;
        private int m_dx;
        private int m_dy;
        private int m_inc_x;
        private int m_inc_y;
        private int m_cur_f;

        public ellipse_bresenham_interpolator( int rx, int ry )
        {
            this.m_rx2 = rx * rx;
            this.m_ry2 = ry * ry;
            this.m_two_rx2 = this.m_rx2 << 1;
            this.m_two_ry2 = this.m_ry2 << 1;
            this.m_dx = 0;
            this.m_dy = 0;
            this.m_inc_x = 0;
            this.m_inc_y = -ry * this.m_two_rx2;
            this.m_cur_f = 0;
        }

        public int dx()
        {
            return this.m_dx;
        }

        public int dy()
        {
            return this.m_dy;
        }

        public void Next()
        {
            int num1;
            int num2 = num1 = this.m_cur_f + this.m_inc_x + this.m_ry2;
            if ( num2 < 0 )
                num2 = -num2;
            int num3;
            int num4 = num3 = this.m_cur_f + this.m_inc_y + this.m_rx2;
            if ( num4 < 0 )
                num4 = -num4;
            int num5;
            int num6 = num5 = this.m_cur_f + this.m_inc_x + this.m_ry2 + this.m_inc_y + this.m_rx2;
            if ( num6 < 0 )
                num6 = -num6;
            int num7 = num2;
            bool flag = true;
            if ( num7 > num4 )
            {
                num7 = num4;
                flag = false;
            }
            this.m_dx = this.m_dy = 0;
            if ( num7 > num6 )
            {
                this.m_inc_x += this.m_two_ry2;
                this.m_inc_y += this.m_two_rx2;
                this.m_cur_f = num5;
                this.m_dx = 1;
                this.m_dy = 1;
            }
            else if ( flag )
            {
                this.m_inc_x += this.m_two_ry2;
                this.m_cur_f = num1;
                this.m_dx = 1;
            }
            else
            {
                this.m_inc_y += this.m_two_rx2;
                this.m_cur_f = num3;
                this.m_dy = 1;
            }
        }
    }
}
