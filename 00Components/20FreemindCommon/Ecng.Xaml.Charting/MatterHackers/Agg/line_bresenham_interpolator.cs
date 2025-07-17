// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.line_bresenham_interpolator
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg
{
    internal sealed class line_bresenham_interpolator
    {
        private int m_x1_lr;
        private int m_y1_lr;
        private int m_x2_lr;
        private int m_y2_lr;
        private bool m_ver;
        private int m_len;
        private int m_inc;
        private dda2_line_interpolator m_interpolator;

        public static int line_lr( int v )
        {
            return v >> 8;
        }

        public line_bresenham_interpolator( int x1, int y1, int x2, int y2 )
        {
            this.m_x1_lr = line_bresenham_interpolator.line_lr( x1 );
            this.m_y1_lr = line_bresenham_interpolator.line_lr( y1 );
            this.m_x2_lr = line_bresenham_interpolator.line_lr( x2 );
            this.m_y2_lr = line_bresenham_interpolator.line_lr( y2 );
            this.m_ver = Math.Abs( this.m_x2_lr - this.m_x1_lr ) < Math.Abs( this.m_y2_lr - this.m_y1_lr );
            this.m_len = !this.m_ver ? Math.Abs( this.m_x2_lr - this.m_x1_lr ) : Math.Abs( this.m_y2_lr - this.m_y1_lr );
            this.m_inc = this.m_ver ? ( y2 > y1 ? 1 : -1 ) : ( x2 > x1 ? 1 : -1 );
            this.m_interpolator = new dda2_line_interpolator( this.m_ver ? x1 : y1, this.m_ver ? x2 : y2, this.m_len );
        }

        public bool is_ver()
        {
            return this.m_ver;
        }

        public int len()
        {
            return this.m_len;
        }

        public int inc()
        {
            return this.m_inc;
        }

        public void hstep()
        {
            this.m_interpolator.Next();
            this.m_x1_lr += this.m_inc;
        }

        public void vstep()
        {
            this.m_interpolator.Next();
            this.m_y1_lr += this.m_inc;
        }

        public int x1()
        {
            return this.m_x1_lr;
        }

        public int y1()
        {
            return this.m_y1_lr;
        }

        public int x2()
        {
            return line_bresenham_interpolator.line_lr( this.m_interpolator.y() );
        }

        public int y2()
        {
            return line_bresenham_interpolator.line_lr( this.m_interpolator.y() );
        }

        public int x2_hr()
        {
            return this.m_interpolator.y();
        }

        public int y2_hr()
        {
            return this.m_interpolator.y();
        }

        public enum subpixel_scale_e
        {
            subpixel_shift = 8,
            subpixel_mask = 255, // 0x000000FF
            subpixel_scale = 256, // 0x00000100
        }
    }
}
