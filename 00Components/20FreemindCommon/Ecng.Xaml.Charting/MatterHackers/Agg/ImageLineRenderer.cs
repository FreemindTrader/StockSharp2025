// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.ImageLineRenderer
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using MatterHackers.Agg.Image;

namespace MatterHackers.Agg
{
    internal class ImageLineRenderer : LineRenderer
    {
        private IImageByte m_ren;
        private line_image_pattern m_pattern;
        private int m_start;
        private double m_scale_x;
        private RectangleInt m_clip_box;

        public ImageLineRenderer( IImageByte ren, line_image_pattern patt )
        {
            this.m_ren = ren;
            this.m_pattern = patt;
            this.m_start = 0;
            this.m_scale_x = 1.0;
            this.m_clip_box = new RectangleInt( 0, 0, 0, 0 );
        }

        public void attach( IImageByte ren )
        {
            this.m_ren = ren;
        }

        public void pattern( line_image_pattern p )
        {
            this.m_pattern = p;
        }

        public line_image_pattern pattern()
        {
            return this.m_pattern;
        }

        public void reset_clipping()
        {
        }

        public void clip_box( double x1, double y1, double x2, double y2 )
        {
            this.m_clip_box.Left = line_coord_sat.conv( x1 );
            this.m_clip_box.Bottom = line_coord_sat.conv( y1 );
            this.m_clip_box.Right = line_coord_sat.conv( x2 );
            this.m_clip_box.Top = line_coord_sat.conv( y2 );
        }

        public void scale_x( double s )
        {
            this.m_scale_x = s;
        }

        public double scale_x()
        {
            return this.m_scale_x;
        }

        public void start_x( double s )
        {
            this.m_start = agg_basics.iround( s * 256.0 );
        }

        public double start_x()
        {
            return ( double ) this.m_start / 256.0;
        }

        public int subpixel_width()
        {
            return this.m_pattern.line_width();
        }

        public int pattern_width()
        {
            return this.m_pattern.pattern_width();
        }

        public double width()
        {
            return ( double ) this.subpixel_width() / 256.0;
        }

        public void pixel( RGBA_Bytes[ ] p, int offset, int x, int y )
        {
            throw new NotImplementedException();
        }

        public void blend_color_hspan( int x, int y, uint len, RGBA_Bytes[ ] colors, int colorsOffset )
        {
            throw new NotImplementedException();
        }

        public void blend_color_vspan( int x, int y, uint len, RGBA_Bytes[ ] colors, int colorsOffset )
        {
            throw new NotImplementedException();
        }

        public static bool accurate_join_only()
        {
            return true;
        }

        public override void semidot( LineRenderer.CompareFunction cmp, int xc1, int yc1, int xc2, int yc2 )
        {
        }

        public override void semidot_hline( LineRenderer.CompareFunction cmp, int xc1, int yc1, int xc2, int yc2, int x1, int y1, int x2 )
        {
        }

        public override void pie( int xc, int yc, int x1, int y1, int x2, int y2 )
        {
        }

        public override void line0( line_parameters lp )
        {
        }

        public override void line1( line_parameters lp, int sx, int sy )
        {
        }

        public override void line2( line_parameters lp, int ex, int ey )
        {
        }

        public void line3_no_clip( line_parameters lp, int sx, int sy, int ex, int ey )
        {
            throw new NotImplementedException();
        }

        public override void line3( line_parameters lp, int sx, int sy, int ex, int ey )
        {
            throw new NotImplementedException();
        }
    }
}
