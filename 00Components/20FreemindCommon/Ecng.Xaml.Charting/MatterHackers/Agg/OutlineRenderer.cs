// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.OutlineRenderer
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using MatterHackers.Agg.Image;

namespace MatterHackers.Agg
{
    internal class OutlineRenderer : LineRenderer
    {
        private IImageByte destImageSurface;
        private LineProfileAnitAlias lineProfile;
        private RectangleInt clippingRectangle;
        private bool doClipping;
        protected const int max_half_width = 64;

        public OutlineRenderer( IImageByte destImage, LineProfileAnitAlias profile )
        {
            this.destImageSurface = destImage;
            this.lineProfile = profile;
            this.clippingRectangle = new RectangleInt( 0, 0, 0, 0 );
            this.doClipping = false;
        }

        public void attach( IImageByte ren )
        {
            this.destImageSurface = ren;
        }

        public void profile( LineProfileAnitAlias prof )
        {
            this.lineProfile = prof;
        }

        public LineProfileAnitAlias profile()
        {
            return this.lineProfile;
        }

        public int subpixel_width()
        {
            return this.lineProfile.subpixel_width();
        }

        public void reset_clipping()
        {
            this.doClipping = false;
        }

        public void clip_box( double x1, double y1, double x2, double y2 )
        {
            this.clippingRectangle.Left = line_coord_sat.conv( x1 );
            this.clippingRectangle.Bottom = line_coord_sat.conv( y1 );
            this.clippingRectangle.Right = line_coord_sat.conv( x2 );
            this.clippingRectangle.Top = line_coord_sat.conv( y2 );
            this.doClipping = true;
        }

        public int cover( int d )
        {
            return ( int ) this.lineProfile.value( d );
        }

        public void blend_solid_hspan( int x, int y, int len, byte[ ] covers, int coversOffset )
        {
            this.destImageSurface.blend_solid_hspan( x, y, len, this.color(), covers, coversOffset );
        }

        public void blend_solid_vspan( int x, int y, int len, byte[ ] covers, int coversOffset )
        {
            this.destImageSurface.blend_solid_vspan( x, y, len, this.color(), covers, coversOffset );
        }

        public static bool accurate_join_only()
        {
            return false;
        }

        public override void semidot_hline( LineRenderer.CompareFunction cmp, int xc1, int yc1, int xc2, int yc2, int x1, int y1, int x2 )
        {
            byte[] covers = new byte[132];
            int coversIndex = 0;
            int index = 0;
            int x3 = x1 << 8;
            int y = y1 << 8;
            int num1 = this.subpixel_width();
            distance_interpolator0 distanceInterpolator0 = new distance_interpolator0(xc1, yc1, xc2, yc2, x3, y);
            int num2 = x3 + 128;
            int num3 = y + 128;
            int x4 = x1;
            int num4 = num2 - xc1;
            int num5 = num3 - yc1;
            do
            {
                int d = agg_math.fast_sqrt(num4 * num4 + num5 * num5);
                covers[ index ] = ( byte ) 0;
                if ( cmp( distanceInterpolator0.dist() ) && d <= num1 )
                {
                    covers[ index ] = ( byte ) this.cover( d );
                }

                ++index;
                num4 += 256;
                distanceInterpolator0.inc_x();
            }
            while ( ++x1 <= x2 );
            this.destImageSurface.blend_solid_hspan( x4, y1, index - coversIndex, this.color(), covers, coversIndex );
        }

        public override void semidot( LineRenderer.CompareFunction cmp, int xc1, int yc1, int xc2, int yc2 )
        {
            if ( this.doClipping && ClipLiangBarsky.clipping_flags( xc1, yc1, this.clippingRectangle ) != 0 )
            {
                return;
            }

            int num1 = this.subpixel_width() + (int) byte.MaxValue >> 8;
            if ( num1 < 1 )
            {
                num1 = 1;
            }

            ellipse_bresenham_interpolator bresenhamInterpolator = new ellipse_bresenham_interpolator(num1, num1);
            int num2 = 0;
            int num3 = -num1;
            int num4 = num3;
            int num5 = num2;
            int num6 = xc1 >> 8;
            int num7 = yc1 >> 8;
            do
            {
                num2 += bresenhamInterpolator.dx();
                num3 += bresenhamInterpolator.dy();
                if ( num3 != num4 )
                {
                    this.semidot_hline( cmp, xc1, yc1, xc2, yc2, num6 - num5, num7 + num4, num6 + num5 );
                    this.semidot_hline( cmp, xc1, yc1, xc2, yc2, num6 - num5, num7 - num4, num6 + num5 );
                }
                num5 = num2;
                num4 = num3;
                bresenhamInterpolator.Next();
            }
            while ( num3 < 0 );
            this.semidot_hline( cmp, xc1, yc1, xc2, yc2, num6 - num5, num7 + num4, num6 + num5 );
        }

        public void pie_hline( int xc, int yc, int xp1, int yp1, int xp2, int yp2, int xh1, int yh1, int xh2 )
        {
            if ( this.doClipping && ClipLiangBarsky.clipping_flags( xc, yc, this.clippingRectangle ) != 0 )
            {
                return;
            }

            byte[] covers = new byte[132];
            int coversIndex = 0;
            int index = 0;
            int x1 = xh1 << 8;
            int y = yh1 << 8;
            int num1 = this.subpixel_width();
            distance_interpolator00 distanceInterpolator00 = new distance_interpolator00(xc, yc, xp1, yp1, xp2, yp2, x1, y);
            int num2 = x1 + 128;
            int num3 = y + 128;
            int x2 = xh1;
            int num4 = num2 - xc;
            int num5 = num3 - yc;
            do
            {
                int d = agg_math.fast_sqrt(num4 * num4 + num5 * num5);
                covers[ index ] = ( byte ) 0;
                if ( distanceInterpolator00.dist1() <= 0 && distanceInterpolator00.dist2() > 0 && d <= num1 )
                {
                    covers[ index ] = ( byte ) this.cover( d );
                }

                ++index;
                num4 += 256;
                distanceInterpolator00.inc_x();
            }
            while ( ++xh1 <= xh2 );
            this.destImageSurface.blend_solid_hspan( x2, yh1, index - coversIndex, this.color(), covers, coversIndex );
        }

        public override void pie( int xc, int yc, int x1, int y1, int x2, int y2 )
        {
            int num1 = this.subpixel_width() + (int) byte.MaxValue >> 8;
            if ( num1 < 1 )
            {
                num1 = 1;
            }

            ellipse_bresenham_interpolator bresenhamInterpolator = new ellipse_bresenham_interpolator(num1, num1);
            int num2 = 0;
            int num3 = -num1;
            int num4 = num3;
            int num5 = num2;
            int num6 = xc >> 8;
            int num7 = yc >> 8;
            do
            {
                num2 += bresenhamInterpolator.dx();
                num3 += bresenhamInterpolator.dy();
                if ( num3 != num4 )
                {
                    this.pie_hline( xc, yc, x1, y1, x2, y2, num6 - num5, num7 + num4, num6 + num5 );
                    this.pie_hline( xc, yc, x1, y1, x2, y2, num6 - num5, num7 - num4, num6 + num5 );
                }
                num5 = num2;
                num4 = num3;
                bresenhamInterpolator.Next();
            }
            while ( num3 < 0 );
            this.pie_hline( xc, yc, x1, y1, x2, y2, num6 - num5, num7 + num4, num6 + num5 );
        }

        public void line0_no_clip( line_parameters lp )
        {
            if ( lp.len > 262144 )
            {
                line_parameters lp1;
                line_parameters lp2;
                lp.divide( out lp1, out lp2 );
                this.line0_no_clip( lp1 );
                this.line0_no_clip( lp2 );
            }
            else
            {
                line_interpolator_aa0 lineInterpolatorAa0 = new line_interpolator_aa0(this, lp);
                if ( lineInterpolatorAa0.count() == 0 )
                {
                    return;
                }

                if ( lineInterpolatorAa0.vertical() )
                {
                    do
                    {
                        ;
                    }
                    while ( lineInterpolatorAa0.step_ver() );
                }
                else
                {
                    do
                    {
                        ;
                    }
                    while ( lineInterpolatorAa0.step_hor() );
                }
            }
        }

        public override void line0( line_parameters lp )
        {
            if ( this.doClipping )
            {
                int x1 = lp.x1;
                int y1 = lp.y1;
                int x2 = lp.x2;
                int y2 = lp.y2;
                int num = ClipLiangBarsky.clip_line_segment(ref x1, ref y1, ref x2, ref y2, this.clippingRectangle);
                if ( ( num & 4 ) != 0 )
                {
                    return;
                }

                if ( num != 0 )
                {
                    this.line0_no_clip( new line_parameters( x1, y1, x2, y2, agg_basics.uround( agg_math.calc_distance( ( double ) x1, ( double ) y1, ( double ) x2, ( double ) y2 ) ) ) );
                }
                else
                {
                    this.line0_no_clip( lp );
                }
            }
            else
            {
                this.line0_no_clip( lp );
            }
        }

        public void line1_no_clip( line_parameters lp, int sx, int sy )
        {
            if ( lp.len > 262144 )
            {
                line_parameters lp1;
                line_parameters lp2;
                lp.divide( out lp1, out lp2 );
                this.line1_no_clip( lp1, lp.x1 + sx >> 1, lp.y1 + sy >> 1 );
                this.line1_no_clip( lp2, lp1.x2 + ( lp1.y2 - lp1.y1 ), lp1.y2 - ( lp1.x2 - lp1.x1 ) );
            }
            else
            {
                LineAABasics.fix_degenerate_bisectrix_start( lp, ref sx, ref sy );
                line_interpolator_aa1 lineInterpolatorAa1 = new line_interpolator_aa1(this, lp, sx, sy);
                if ( lineInterpolatorAa1.vertical() )
                {
                    do
                    {
                        ;
                    }
                    while ( lineInterpolatorAa1.step_ver() );
                }
                else
                {
                    do
                    {
                        ;
                    }
                    while ( lineInterpolatorAa1.step_hor() );
                }
            }
        }

        public override void line1( line_parameters lp, int sx, int sy )
        {
            if ( this.doClipping )
            {
                int x1 = lp.x1;
                int y1 = lp.y1;
                int x2 = lp.x2;
                int y2 = lp.y2;
                int num = ClipLiangBarsky.clip_line_segment(ref x1, ref y1, ref x2, ref y2, this.clippingRectangle);
                if ( ( num & 4 ) != 0 )
                {
                    return;
                }

                if ( num != 0 )
                {
                    line_parameters lp1 = new line_parameters(x1, y1, x2, y2, agg_basics.uround(agg_math.calc_distance((double) x1, (double) y1, (double) x2, (double) y2)));
                    if ( ( num & 1 ) != 0 )
                    {
                        sx = x1 + ( y2 - y1 );
                        sy = y1 - ( x2 - x1 );
                    }
                    else
                    {
                        for ( ; Math.Abs( sx - lp.x1 ) + Math.Abs( sy - lp.y1 ) > lp1.len ; sy = lp.y1 + sy >> 1 )
                        {
                            sx = lp.x1 + sx >> 1;
                        }
                    }
                    this.line1_no_clip( lp1, sx, sy );
                }
                else
                {
                    this.line1_no_clip( lp, sx, sy );
                }
            }
            else
            {
                this.line1_no_clip( lp, sx, sy );
            }
        }

        public void line2_no_clip( line_parameters lp, int ex, int ey )
        {
            if ( lp.len > 262144 )
            {
                line_parameters lp1;
                line_parameters lp2;
                lp.divide( out lp1, out lp2 );
                this.line2_no_clip( lp1, lp1.x2 + ( lp1.y2 - lp1.y1 ), lp1.y2 - ( lp1.x2 - lp1.x1 ) );
                this.line2_no_clip( lp2, lp.x2 + ex >> 1, lp.y2 + ey >> 1 );
            }
            else
            {
                LineAABasics.fix_degenerate_bisectrix_end( lp, ref ex, ref ey );
                line_interpolator_aa2 lineInterpolatorAa2 = new line_interpolator_aa2(this, lp, ex, ey);
                if ( lineInterpolatorAa2.vertical() )
                {
                    do
                    {
                        ;
                    }
                    while ( lineInterpolatorAa2.step_ver() );
                }
                else
                {
                    do
                    {
                        ;
                    }
                    while ( lineInterpolatorAa2.step_hor() );
                }
            }
        }

        public override void line2( line_parameters lp, int ex, int ey )
        {
            if ( this.doClipping )
            {
                int x1 = lp.x1;
                int y1 = lp.y1;
                int x2 = lp.x2;
                int y2 = lp.y2;
                int num = ClipLiangBarsky.clip_line_segment(ref x1, ref y1, ref x2, ref y2, this.clippingRectangle);
                if ( ( num & 4 ) != 0 )
                {
                    return;
                }

                if ( num != 0 )
                {
                    line_parameters lp1 = new line_parameters(x1, y1, x2, y2, agg_basics.uround(agg_math.calc_distance((double) x1, (double) y1, (double) x2, (double) y2)));
                    if ( ( num & 2 ) != 0 )
                    {
                        ex = x2 + ( y2 - y1 );
                        ey = y2 - ( x2 - x1 );
                    }
                    else
                    {
                        for ( ; Math.Abs( ex - lp.x2 ) + Math.Abs( ey - lp.y2 ) > lp1.len ; ey = lp.y2 + ey >> 1 )
                        {
                            ex = lp.x2 + ex >> 1;
                        }
                    }
                    this.line2_no_clip( lp1, ex, ey );
                }
                else
                {
                    this.line2_no_clip( lp, ex, ey );
                }
            }
            else
            {
                this.line2_no_clip( lp, ex, ey );
            }
        }

        public void line3_no_clip( line_parameters lp, int sx, int sy, int ex, int ey )
        {
            if ( lp.len > 262144 )
            {
                line_parameters lp1;
                line_parameters lp2;
                lp.divide( out lp1, out lp2 );
                int num1 = lp1.x2 + (lp1.y2 - lp1.y1);
                int num2 = lp1.y2 - (lp1.x2 - lp1.x1);
                this.line3_no_clip( lp1, lp.x1 + sx >> 1, lp.y1 + sy >> 1, num1, num2 );
                this.line3_no_clip( lp2, num1, num2, lp.x2 + ex >> 1, lp.y2 + ey >> 1 );
            }
            else
            {
                LineAABasics.fix_degenerate_bisectrix_start( lp, ref sx, ref sy );
                LineAABasics.fix_degenerate_bisectrix_end( lp, ref ex, ref ey );
                line_interpolator_aa3 lineInterpolatorAa3 = new line_interpolator_aa3(this, lp, sx, sy, ex, ey);
                if ( lineInterpolatorAa3.vertical() )
                {
                    do
                    {
                        ;
                    }
                    while ( lineInterpolatorAa3.step_ver() );
                }
                else
                {
                    do
                    {
                        ;
                    }
                    while ( lineInterpolatorAa3.step_hor() );
                }
            }
        }

        public override void line3( line_parameters lp, int sx, int sy, int ex, int ey )
        {
            if ( this.doClipping )
            {
                int x1 = lp.x1;
                int y1 = lp.y1;
                int x2 = lp.x2;
                int y2 = lp.y2;
                int num = ClipLiangBarsky.clip_line_segment(ref x1, ref y1, ref x2, ref y2, this.clippingRectangle);
                if ( ( num & 4 ) != 0 )
                {
                    return;
                }

                if ( num != 0 )
                {
                    line_parameters lp1 = new line_parameters(x1, y1, x2, y2, agg_basics.uround(agg_math.calc_distance((double) x1, (double) y1, (double) x2, (double) y2)));
                    if ( ( num & 1 ) != 0 )
                    {
                        sx = x1 + ( y2 - y1 );
                        sy = y1 - ( x2 - x1 );
                    }
                    else
                    {
                        for ( ; Math.Abs( sx - lp.x1 ) + Math.Abs( sy - lp.y1 ) > lp1.len ; sy = lp.y1 + sy >> 1 )
                        {
                            sx = lp.x1 + sx >> 1;
                        }
                    }
                    if ( ( num & 2 ) != 0 )
                    {
                        ex = x2 + ( y2 - y1 );
                        ey = y2 - ( x2 - x1 );
                    }
                    else
                    {
                        for ( ; Math.Abs( ex - lp.x2 ) + Math.Abs( ey - lp.y2 ) > lp1.len ; ey = lp.y2 + ey >> 1 )
                        {
                            ex = lp.x2 + ex >> 1;
                        }
                    }
                    this.line3_no_clip( lp1, sx, sy, ex, ey );
                }
                else
                {
                    this.line3_no_clip( lp, sx, sy, ex, ey );
                }
            }
            else
            {
                this.line3_no_clip( lp, sx, sy, ex, ey );
            }
        }
    }
}
