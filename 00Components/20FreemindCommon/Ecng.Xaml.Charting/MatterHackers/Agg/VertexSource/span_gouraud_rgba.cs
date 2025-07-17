// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VertexSource.span_gouraud_rgba
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg.VertexSource
{
    internal sealed class span_gouraud_rgba : span_gouraud, ISpanGenerator
    {
        private bool m_swap;
        private int m_y2;
        private span_gouraud_rgba.rgba_calc m_rgba1;
        private span_gouraud_rgba.rgba_calc m_rgba2;
        private span_gouraud_rgba.rgba_calc m_rgba3;

        public span_gouraud_rgba()
        {
        }

        public span_gouraud_rgba( RGBA_Bytes c1, RGBA_Bytes c2, RGBA_Bytes c3, double x1, double y1, double x2, double y2, double x3, double y3 )
          : this( c1, c2, c3, x1, y1, x2, y2, x3, y3, 0.0 )
        {
        }

        public span_gouraud_rgba( RGBA_Bytes c1, RGBA_Bytes c2, RGBA_Bytes c3, double x1, double y1, double x2, double y2, double x3, double y3, double d )
          : base( c1, c2, c3, x1, y1, x2, y2, x3, y3, d )
        {
        }

        public void prepare()
        {
            span_gouraud.coord_type[] coord = new span_gouraud.coord_type[3];
            this.arrange_vertices( coord );
            this.m_y2 = ( int ) coord[ 1 ].y;
            this.m_swap = agg_math.cross_product( coord[ 0 ].x, coord[ 0 ].y, coord[ 2 ].x, coord[ 2 ].y, coord[ 1 ].x, coord[ 1 ].y ) < 0.0;
            this.m_rgba1.init( coord[ 0 ], coord[ 2 ] );
            this.m_rgba2.init( coord[ 0 ], coord[ 1 ] );
            this.m_rgba3.init( coord[ 1 ], coord[ 2 ] );
        }

        public void generate( RGBA_Bytes[ ] span, int spanIndex, int x, int y, int len )
        {
            this.m_rgba1.calc( ( double ) y );
            span_gouraud_rgba.rgba_calc rgbaCalc1 = this.m_rgba1;
            span_gouraud_rgba.rgba_calc rgbaCalc2 = this.m_rgba2;
            if ( y <= this.m_y2 )
            {
                this.m_rgba2.calc( ( double ) y + this.m_rgba2.m_1dy );
            }
            else
            {
                this.m_rgba3.calc( ( double ) y - this.m_rgba3.m_1dy );
                rgbaCalc2 = this.m_rgba3;
            }
            if ( this.m_swap )
            {
                span_gouraud_rgba.rgba_calc rgbaCalc3 = rgbaCalc2;
                rgbaCalc2 = rgbaCalc1;
                rgbaCalc1 = rgbaCalc3;
            }
            int count = Math.Abs(rgbaCalc2.m_x - rgbaCalc1.m_x);
            if ( count <= 0 )
                count = 1;
            dda_line_interpolator lineInterpolator1 = new dda_line_interpolator(rgbaCalc1.m_r, rgbaCalc2.m_r, count, 14);
            dda_line_interpolator lineInterpolator2 = new dda_line_interpolator(rgbaCalc1.m_g, rgbaCalc2.m_g, count, 14);
            dda_line_interpolator lineInterpolator3 = new dda_line_interpolator(rgbaCalc1.m_b, rgbaCalc2.m_b, count, 14);
            dda_line_interpolator lineInterpolator4 = new dda_line_interpolator(rgbaCalc1.m_a, rgbaCalc2.m_a, count, 14);
            int n = rgbaCalc1.m_x - (x << 4);
            lineInterpolator1.Prev( n );
            lineInterpolator2.Prev( n );
            lineInterpolator3.Prev( n );
            lineInterpolator4.Prev( n );
            int num1 = count + n;
            uint maxValue = (uint) byte.MaxValue;
            for ( ; len != 0 && n > 0 ; --len )
            {
                int num2 = lineInterpolator1.y();
                int num3 = lineInterpolator2.y();
                int num4 = lineInterpolator3.y();
                int num5 = lineInterpolator4.y();
                if ( num2 < 0 )
                    num2 = 0;
                if ( ( long ) num2 > ( long ) maxValue )
                    num2 = ( int ) maxValue;
                if ( num3 < 0 )
                    num3 = 0;
                if ( ( long ) num3 > ( long ) maxValue )
                    num3 = ( int ) maxValue;
                if ( num4 < 0 )
                    num4 = 0;
                if ( ( long ) num4 > ( long ) maxValue )
                    num4 = ( int ) maxValue;
                if ( num5 < 0 )
                    num5 = 0;
                if ( ( long ) num5 > ( long ) maxValue )
                    num5 = ( int ) maxValue;
                span[ spanIndex ].red = ( byte ) num2;
                span[ spanIndex ].green = ( byte ) num3;
                span[ spanIndex ].blue = ( byte ) num4;
                span[ spanIndex ].alpha = ( byte ) num5;
                lineInterpolator1.Next( 16 );
                lineInterpolator2.Next( 16 );
                lineInterpolator3.Next( 16 );
                lineInterpolator4.Next( 16 );
                num1 -= 16;
                n -= 16;
                ++spanIndex;
            }
            for ( ; len != 0 && num1 > 0 ; --len )
            {
                span[ spanIndex ].red = ( byte ) lineInterpolator1.y();
                span[ spanIndex ].green = ( byte ) lineInterpolator2.y();
                span[ spanIndex ].blue = ( byte ) lineInterpolator3.y();
                span[ spanIndex ].alpha = ( byte ) lineInterpolator4.y();
                lineInterpolator1.Next( 16 );
                lineInterpolator2.Next( 16 );
                lineInterpolator3.Next( 16 );
                lineInterpolator4.Next( 16 );
                num1 -= 16;
                ++spanIndex;
            }
            for ( ; len != 0 ; --len )
            {
                int num2 = lineInterpolator1.y();
                int num3 = lineInterpolator2.y();
                int num4 = lineInterpolator3.y();
                int num5 = lineInterpolator4.y();
                if ( num2 < 0 )
                    num2 = 0;
                if ( ( long ) num2 > ( long ) maxValue )
                    num2 = ( int ) maxValue;
                if ( num3 < 0 )
                    num3 = 0;
                if ( ( long ) num3 > ( long ) maxValue )
                    num3 = ( int ) maxValue;
                if ( num4 < 0 )
                    num4 = 0;
                if ( ( long ) num4 > ( long ) maxValue )
                    num4 = ( int ) maxValue;
                if ( num5 < 0 )
                    num5 = 0;
                if ( ( long ) num5 > ( long ) maxValue )
                    num5 = ( int ) maxValue;
                span[ spanIndex ].red = ( byte ) num2;
                span[ spanIndex ].green = ( byte ) num3;
                span[ spanIndex ].blue = ( byte ) num4;
                span[ spanIndex ].alpha = ( byte ) num5;
                lineInterpolator1.Next( 16 );
                lineInterpolator2.Next( 16 );
                lineInterpolator3.Next( 16 );
                lineInterpolator4.Next( 16 );
                ++spanIndex;
            }
        }

        public enum subpixel_scale_e
        {
            subpixel_shift = 4,
            subpixel_scale = 16, // 0x00000010
        }

        internal struct rgba_calc
        {
            public double m_x1;
            public double m_y1;
            public double m_dx;
            public double m_1dy;
            public int m_r1;
            public int m_g1;
            public int m_b1;
            public int m_a1;
            public int m_dr;
            public int m_dg;
            public int m_db;
            public int m_da;
            public int m_r;
            public int m_g;
            public int m_b;
            public int m_a;
            public int m_x;

            public void init( span_gouraud.coord_type c1, span_gouraud.coord_type c2 )
            {
                this.m_x1 = c1.x - 0.5;
                this.m_y1 = c1.y - 0.5;
                this.m_dx = c2.x - c1.x;
                double num = c2.y - c1.y;
                this.m_1dy = num < 1E-05 ? 100000.0 : 1.0 / num;
                this.m_r1 = ( int ) c1.color.red;
                this.m_g1 = ( int ) c1.color.green;
                this.m_b1 = ( int ) c1.color.blue;
                this.m_a1 = ( int ) c1.color.alpha;
                this.m_dr = ( int ) c2.color.red - this.m_r1;
                this.m_dg = ( int ) c2.color.green - this.m_g1;
                this.m_db = ( int ) c2.color.blue - this.m_b1;
                this.m_da = ( int ) c2.color.alpha - this.m_a1;
            }

            public void calc( double y )
            {
                double num = (y - this.m_y1) * this.m_1dy;
                if ( num < 0.0 )
                    num = 0.0;
                if ( num > 1.0 )
                    num = 1.0;
                this.m_r = this.m_r1 + agg_basics.iround( ( double ) this.m_dr * num );
                this.m_g = this.m_g1 + agg_basics.iround( ( double ) this.m_dg * num );
                this.m_b = this.m_b1 + agg_basics.iround( ( double ) this.m_db * num );
                this.m_a = this.m_a1 + agg_basics.iround( ( double ) this.m_da * num );
                this.m_x = agg_basics.iround( ( this.m_x1 + this.m_dx * num ) * 16.0 );
            }
        }
    }
}
