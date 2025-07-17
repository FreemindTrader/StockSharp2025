// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VectorClipper
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class VectorClipper
    {
        public RectangleInt clipBox;
        private int m_x1;
        private int m_y1;
        private int m_f1;
        private bool m_clipping;

        private int mul_div( double a, double b, double c )
        {
            return agg_basics.iround( a * b / c );
        }

        private int xi( int v )
        {
            return v;
        }

        private int yi( int v )
        {
            return v;
        }

        public int upscale( double v )
        {
            return agg_basics.iround( v * 256.0 );
        }

        public int downscale( int v )
        {
            return v / 256;
        }

        public VectorClipper()
        {
            this.clipBox = new RectangleInt( 0, 0, 0, 0 );
            this.m_x1 = 0;
            this.m_y1 = 0;
            this.m_f1 = 0;
            this.m_clipping = false;
        }

        public void reset_clipping()
        {
            this.m_clipping = false;
        }

        public void clip_box( int x1, int y1, int x2, int y2 )
        {
            this.clipBox = new RectangleInt( x1, y1, x2, y2 );
            this.clipBox.normalize();
            this.m_clipping = true;
        }

        public void move_to( int x1, int y1 )
        {
            this.m_x1 = x1;
            this.m_y1 = y1;
            if ( !this.m_clipping )
                return;
            this.m_f1 = ClipLiangBarsky.clipping_flags( x1, y1, this.clipBox );
        }

        private void line_clip_y( rasterizer_cells_aa ras, int x1, int y1, int x2, int y2, int f1, int f2 )
        {
            f1 &= 10;
            f2 &= 10;
            if ( ( f1 | f2 ) == 0 )
            {
                ras.line( x1, y1, x2, y2 );
            }
            else
            {
                if ( f1 == f2 )
                    return;
                int x1_1 = x1;
                int y1_1 = y1;
                int x2_1 = x2;
                int y2_1 = y2;
                if ( ( f1 & 8 ) != 0 )
                {
                    x1_1 = x1 + this.mul_div( ( double ) ( this.clipBox.Bottom - y1 ), ( double ) ( x2 - x1 ), ( double ) ( y2 - y1 ) );
                    y1_1 = this.clipBox.Bottom;
                }
                if ( ( f1 & 2 ) != 0 )
                {
                    x1_1 = x1 + this.mul_div( ( double ) ( this.clipBox.Top - y1 ), ( double ) ( x2 - x1 ), ( double ) ( y2 - y1 ) );
                    y1_1 = this.clipBox.Top;
                }
                if ( ( f2 & 8 ) != 0 )
                {
                    x2_1 = x1 + this.mul_div( ( double ) ( this.clipBox.Bottom - y1 ), ( double ) ( x2 - x1 ), ( double ) ( y2 - y1 ) );
                    y2_1 = this.clipBox.Bottom;
                }
                if ( ( f2 & 2 ) != 0 )
                {
                    x2_1 = x1 + this.mul_div( ( double ) ( this.clipBox.Top - y1 ), ( double ) ( x2 - x1 ), ( double ) ( y2 - y1 ) );
                    y2_1 = this.clipBox.Top;
                }
                ras.line( x1_1, y1_1, x2_1, y2_1 );
            }
        }

        public void line_to( rasterizer_cells_aa ras, int x2, int y2 )
        {
            if ( this.m_clipping )
            {
                int f2 = ClipLiangBarsky.clipping_flags(x2, y2, this.clipBox);
                if ( ( this.m_f1 & 10 ) == ( f2 & 10 ) && ( this.m_f1 & 10 ) != 0 )
                {
                    this.m_x1 = x2;
                    this.m_y1 = y2;
                    this.m_f1 = f2;
                    return;
                }
                int x1 = this.m_x1;
                int y1 = this.m_y1;
                int f1 = this.m_f1;
                switch ( ( f1 & 5 ) << 1 | f2 & 5 )
                {
                    case 0:
                        this.line_clip_y( ras, x1, y1, x2, y2, f1, f2 );
                        break;
                    case 1:
                        int num1 = y1 + this.mul_div((double) (this.clipBox.Right - x1), (double) (y2 - y1), (double) (x2 - x1));
                        int num2 = ClipLiangBarsky.clipping_flags_y(num1, this.clipBox);
                        this.line_clip_y( ras, x1, y1, this.clipBox.Right, num1, f1, num2 );
                        this.line_clip_y( ras, this.clipBox.Right, num1, this.clipBox.Right, y2, num2, f2 );
                        break;
                    case 2:
                        int num3 = y1 + this.mul_div((double) (this.clipBox.Right - x1), (double) (y2 - y1), (double) (x2 - x1));
                        int num4 = ClipLiangBarsky.clipping_flags_y(num3, this.clipBox);
                        this.line_clip_y( ras, this.clipBox.Right, y1, this.clipBox.Right, num3, f1, num4 );
                        this.line_clip_y( ras, this.clipBox.Right, num3, x2, y2, num4, f2 );
                        break;
                    case 3:
                        this.line_clip_y( ras, this.clipBox.Right, y1, this.clipBox.Right, y2, f1, f2 );
                        break;
                    case 4:
                        int num5 = y1 + this.mul_div((double) (this.clipBox.Left - x1), (double) (y2 - y1), (double) (x2 - x1));
                        int num6 = ClipLiangBarsky.clipping_flags_y(num5, this.clipBox);
                        this.line_clip_y( ras, x1, y1, this.clipBox.Left, num5, f1, num6 );
                        this.line_clip_y( ras, this.clipBox.Left, num5, this.clipBox.Left, y2, num6, f2 );
                        break;
                    case 6:
                        int num7 = y1 + this.mul_div((double) (this.clipBox.Right - x1), (double) (y2 - y1), (double) (x2 - x1));
                        int num8 = y1 + this.mul_div((double) (this.clipBox.Left - x1), (double) (y2 - y1), (double) (x2 - x1));
                        int num9 = ClipLiangBarsky.clipping_flags_y(num7, this.clipBox);
                        int num10 = ClipLiangBarsky.clipping_flags_y(num8, this.clipBox);
                        this.line_clip_y( ras, this.clipBox.Right, y1, this.clipBox.Right, num7, f1, num9 );
                        this.line_clip_y( ras, this.clipBox.Right, num7, this.clipBox.Left, num8, num9, num10 );
                        this.line_clip_y( ras, this.clipBox.Left, num8, this.clipBox.Left, y2, num10, f2 );
                        break;
                    case 8:
                        int num11 = y1 + this.mul_div((double) (this.clipBox.Left - x1), (double) (y2 - y1), (double) (x2 - x1));
                        int num12 = ClipLiangBarsky.clipping_flags_y(num11, this.clipBox);
                        this.line_clip_y( ras, this.clipBox.Left, y1, this.clipBox.Left, num11, f1, num12 );
                        this.line_clip_y( ras, this.clipBox.Left, num11, x2, y2, num12, f2 );
                        break;
                    case 9:
                        int num13 = y1 + this.mul_div((double) (this.clipBox.Left - x1), (double) (y2 - y1), (double) (x2 - x1));
                        int num14 = y1 + this.mul_div((double) (this.clipBox.Right - x1), (double) (y2 - y1), (double) (x2 - x1));
                        int num15 = ClipLiangBarsky.clipping_flags_y(num13, this.clipBox);
                        int num16 = ClipLiangBarsky.clipping_flags_y(num14, this.clipBox);
                        this.line_clip_y( ras, this.clipBox.Left, y1, this.clipBox.Left, num13, f1, num15 );
                        this.line_clip_y( ras, this.clipBox.Left, num13, this.clipBox.Right, num14, num15, num16 );
                        this.line_clip_y( ras, this.clipBox.Right, num14, this.clipBox.Right, y2, num16, f2 );
                        break;
                    case 12:
                        this.line_clip_y( ras, this.clipBox.Left, y1, this.clipBox.Left, y2, f1, f2 );
                        break;
                }
                this.m_f1 = f2;
            }
            else
                ras.line( this.m_x1, this.m_y1, x2, y2 );
            this.m_x1 = x2;
            this.m_y1 = y2;
        }
    }
}
