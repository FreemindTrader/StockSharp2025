// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Image.ImageClippingProxy
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace MatterHackers.Agg.Image
{
    internal class ImageClippingProxy : ImageProxy
    {
        private RectangleInt m_ClippingRect;
        public const byte cover_full = 255;

        public ImageClippingProxy( IImageByte ren )
          : base( ren )
        {
            this.m_ClippingRect = new RectangleInt( 0, 0, ren.Width - 1, ren.Height - 1 );
        }

        public override void LinkToImage( IImageByte ren )
        {
            base.LinkToImage( ren );
            this.m_ClippingRect = new RectangleInt( 0, 0, ren.Width - 1, ren.Height - 1 );
        }

        public bool SetClippingBox( int x1, int y1, int x2, int y2 )
        {
            RectangleInt rectangleInt = new RectangleInt(x1, y1, x2, y2);
            rectangleInt.normalize();
            if ( rectangleInt.clip( new RectangleInt( 0, 0, this.Width - 1, this.Height - 1 ) ) )
            {
                this.m_ClippingRect = rectangleInt;
                return true;
            }
            this.m_ClippingRect.Left = 1;
            this.m_ClippingRect.Bottom = 1;
            this.m_ClippingRect.Right = 0;
            this.m_ClippingRect.Top = 0;
            return false;
        }

        public void reset_clipping( bool visibility )
        {
            if ( visibility )
            {
                this.m_ClippingRect.Left = 0;
                this.m_ClippingRect.Bottom = 0;
                this.m_ClippingRect.Right = this.Width - 1;
                this.m_ClippingRect.Top = this.Height - 1;
            }
            else
            {
                this.m_ClippingRect.Left = 1;
                this.m_ClippingRect.Bottom = 1;
                this.m_ClippingRect.Right = 0;
                this.m_ClippingRect.Top = 0;
            }
        }

        public void clip_box_naked( int x1, int y1, int x2, int y2 )
        {
            this.m_ClippingRect.Left = x1;
            this.m_ClippingRect.Bottom = y1;
            this.m_ClippingRect.Right = x2;
            this.m_ClippingRect.Top = y2;
        }

        public bool inbox( int x, int y )
        {
            if ( x >= this.m_ClippingRect.Left && y >= this.m_ClippingRect.Bottom && x <= this.m_ClippingRect.Right )
                return y <= this.m_ClippingRect.Top;
            return false;
        }

        public RectangleInt clip_box()
        {
            return this.m_ClippingRect;
        }

        private int xmin()
        {
            return this.m_ClippingRect.Left;
        }

        private int ymin()
        {
            return this.m_ClippingRect.Bottom;
        }

        private int xmax()
        {
            return this.m_ClippingRect.Right;
        }

        private int ymax()
        {
            return this.m_ClippingRect.Top;
        }

        public RectangleInt bounding_clip_box()
        {
            return this.m_ClippingRect;
        }

        public int bounding_xmin()
        {
            return this.m_ClippingRect.Left;
        }

        public int bounding_ymin()
        {
            return this.m_ClippingRect.Bottom;
        }

        public int bounding_xmax()
        {
            return this.m_ClippingRect.Right;
        }

        public int bounding_ymax()
        {
            return this.m_ClippingRect.Top;
        }

        public void clear( IColorType in_c )
        {
            RGBA_Bytes sourceColor = new RGBA_Bytes(in_c.Red0To255, in_c.Green0To255, in_c.Blue0To255, in_c.Alpha0To255);
            if ( this.Width == 0 )
                return;
            for ( int y = 0 ; y < this.Height ; ++y )
                base.copy_hline( 0, y, this.Width, sourceColor );
        }

        public override void copy_pixel( int x, int y, byte[ ] c, int ByteOffset )
        {
            if ( !this.inbox( x, y ) )
                return;
            base.copy_pixel( x, y, c, ByteOffset );
        }

        public override RGBA_Bytes GetPixel( int x, int y )
        {
            if ( !this.inbox( x, y ) )
                return new RGBA_Bytes();
            return base.GetPixel( x, y );
        }

        public override void copy_hline( int x1, int y, int x2, RGBA_Bytes c )
        {
            if ( x1 > x2 )
            {
                int num = x2;
                x2 = x1;
                x1 = num;
            }
            if ( y > this.ymax() || y < this.ymin() || ( x1 > this.xmax() || x2 < this.xmin() ) )
                return;
            if ( x1 < this.xmin() )
                x1 = this.xmin();
            if ( x2 > this.xmax() )
                x2 = this.xmax();
            base.copy_hline( x1, y, x2 - x1 + 1, c );
        }

        public override void copy_vline( int x, int y1, int y2, RGBA_Bytes c )
        {
            if ( y1 > y2 )
            {
                int num = y2;
                y2 = y1;
                y1 = num;
            }
            if ( x > this.xmax() || x < this.xmin() || ( y1 > this.ymax() || y2 < this.ymin() ) )
                return;
            if ( y1 < this.ymin() )
                y1 = this.ymin();
            if ( y2 > this.ymax() )
                y2 = this.ymax();
            base.copy_vline( x, y1, y2 - y1 + 1, c );
        }

        public override void blend_hline( int x1, int y, int x2, RGBA_Bytes c, byte cover )
        {
            if ( x1 > x2 )
            {
                int num = x2;
                x2 = x1;
                x1 = num;
            }
            if ( y > this.ymax() || y < this.ymin() || ( x1 > this.xmax() || x2 < this.xmin() ) )
                return;
            if ( x1 < this.xmin() )
                x1 = this.xmin();
            if ( x2 > this.xmax() )
                x2 = this.xmax();
            base.blend_hline( x1, y, x2, c, cover );
        }

        public override void blend_vline( int x, int y1, int y2, RGBA_Bytes c, byte cover )
        {
            if ( y1 > y2 )
            {
                int num = y2;
                y2 = y1;
                y1 = num;
            }
            if ( x > this.xmax() || x < this.xmin() || ( y1 > this.ymax() || y2 < this.ymin() ) )
                return;
            if ( y1 < this.ymin() )
                y1 = this.ymin();
            if ( y2 > this.ymax() )
                y2 = this.ymax();
            base.blend_vline( x, y1, y2, c, cover );
        }

        public override void blend_solid_hspan( int x, int y, int len, RGBA_Bytes c, byte[ ] covers, int coversIndex )
        {
            if ( y > this.ymax() || y < this.ymin() )
                return;
            if ( x < this.xmin() )
            {
                len -= this.xmin() - x;
                if ( len <= 0 )
                    return;
                coversIndex += this.xmin() - x;
                x = this.xmin();
            }
            if ( x + len > this.xmax() )
            {
                len = this.xmax() - x + 1;
                if ( len <= 0 )
                    return;
            }
            base.blend_solid_hspan( x, y, len, c, covers, coversIndex );
        }

        public override void blend_solid_vspan( int x, int y, int len, RGBA_Bytes c, byte[ ] covers, int coversIndex )
        {
            if ( x > this.xmax() || x < this.xmin() )
                return;
            if ( y < this.ymin() )
            {
                len -= this.ymin() - y;
                if ( len <= 0 )
                    return;
                coversIndex += this.ymin() - y;
                y = this.ymin();
            }
            if ( y + len > this.ymax() )
            {
                len = this.ymax() - y + 1;
                if ( len <= 0 )
                    return;
            }
            base.blend_solid_vspan( x, y, len, c, covers, coversIndex );
        }

        public override void copy_color_hspan( int x, int y, int len, RGBA_Bytes[ ] colors, int colorsIndex )
        {
            if ( y > this.ymax() || y < this.ymin() )
                return;
            if ( x < this.xmin() )
            {
                int num = this.xmin() - x;
                len -= num;
                if ( len <= 0 )
                    return;
                colorsIndex += num;
                x = this.xmin();
            }
            if ( x + len > this.xmax() )
            {
                len = this.xmax() - x + 1;
                if ( len <= 0 )
                    return;
            }
            base.copy_color_hspan( x, y, len, colors, colorsIndex );
        }

        public override void copy_color_vspan( int x, int y, int len, RGBA_Bytes[ ] colors, int colorsIndex )
        {
            if ( x > this.xmax() || x < this.xmin() )
                return;
            if ( y < this.ymin() )
            {
                int num = this.ymin() - y;
                len -= num;
                if ( len <= 0 )
                    return;
                colorsIndex += num;
                y = this.ymin();
            }
            if ( y + len > this.ymax() )
            {
                len = this.ymax() - y + 1;
                if ( len <= 0 )
                    return;
            }
            base.copy_color_vspan( x, y, len, colors, colorsIndex );
        }

        public override void blend_color_hspan( int x, int y, int in_len, RGBA_Bytes[ ] colors, int colorsIndex, byte[ ] covers, int coversIndex, bool firstCoverForAll )
        {
            int len = in_len;
            if ( y > this.ymax() || y < this.ymin() )
                return;
            if ( x < this.xmin() )
            {
                int num = this.xmin() - x;
                len -= num;
                if ( len <= 0 )
                    return;
                if ( covers != null )
                    coversIndex += num;
                colorsIndex += num;
                x = this.xmin();
            }
            if ( x + len - 1 > this.xmax() )
            {
                len = this.xmax() - x + 1;
                if ( len <= 0 )
                    return;
            }
            base.blend_color_hspan( x, y, len, colors, colorsIndex, covers, coversIndex, firstCoverForAll );
        }

        public void copy_from( IImageByte src )
        {
            this.CopyFrom( src, new RectangleInt( 0, 0, src.Width, src.Height ), 0, 0 );
        }

        public override void SetPixel( int x, int y, RGBA_Bytes color )
        {
            if ( ( long ) ( uint ) x >= ( long ) this.Width || ( long ) ( uint ) y >= ( long ) this.Height )
                return;
            base.SetPixel( x, y, color );
        }

        public override void CopyFrom( IImageByte sourceImage, RectangleInt sourceImageRect, int destXOffset, int destYOffset )
        {
            RectangleInt rectToCopy = sourceImageRect;
            rectToCopy.Offset( destXOffset, destYOffset );
            RectangleInt sourceImageRect1 = new RectangleInt();
            if ( !sourceImageRect1.IntersectRectangles( rectToCopy, this.m_ClippingRect ) )
                return;
            sourceImageRect1.Offset( -destXOffset, -destYOffset );
            base.CopyFrom( sourceImage, sourceImageRect1, destXOffset, destYOffset );
        }

        public RectangleInt clip_rect_area( ref RectangleInt destRect, ref RectangleInt sourceRect, int sourceWidth, int sourceHeight )
        {
            RectangleInt rectangleInt1 = new RectangleInt(0, 0, 0, 0);
            RectangleInt rectangleInt2 = this.clip_box();
            ++rectangleInt2.Right;
            ++rectangleInt2.Top;
            if ( sourceRect.Left < 0 )
            {
                destRect.Left -= sourceRect.Left;
                sourceRect.Left = 0;
            }
            if ( sourceRect.Bottom < 0 )
            {
                destRect.Bottom -= sourceRect.Bottom;
                sourceRect.Bottom = 0;
            }
            if ( sourceRect.Right > sourceWidth )
                sourceRect.Right = sourceWidth;
            if ( sourceRect.Top > sourceHeight )
                sourceRect.Top = sourceHeight;
            if ( destRect.Left < rectangleInt2.Left )
            {
                sourceRect.Left += rectangleInt2.Left - destRect.Left;
                destRect.Left = rectangleInt2.Left;
            }
            if ( destRect.Bottom < rectangleInt2.Bottom )
            {
                sourceRect.Bottom += rectangleInt2.Bottom - destRect.Bottom;
                destRect.Bottom = rectangleInt2.Bottom;
            }
            if ( destRect.Right > rectangleInt2.Right )
                destRect.Right = rectangleInt2.Right;
            if ( destRect.Top > rectangleInt2.Top )
                destRect.Top = rectangleInt2.Top;
            rectangleInt1.Right = destRect.Right - destRect.Left;
            rectangleInt1.Top = destRect.Top - destRect.Bottom;
            if ( rectangleInt1.Right > sourceRect.Right - sourceRect.Left )
                rectangleInt1.Right = sourceRect.Right - sourceRect.Left;
            if ( rectangleInt1.Top > sourceRect.Top - sourceRect.Bottom )
                rectangleInt1.Top = sourceRect.Top - sourceRect.Bottom;
            return rectangleInt1;
        }

        public override void blend_color_vspan( int x, int y, int len, RGBA_Bytes[ ] colors, int colorsIndex, byte[ ] covers, int coversIndex, bool firstCoverForAll )
        {
            if ( x > this.xmax() || x < this.xmin() )
                return;
            if ( y < this.ymin() )
            {
                int num = this.ymin() - y;
                len -= num;
                if ( len <= 0 )
                    return;
                if ( covers != null )
                    coversIndex += num;
                colorsIndex += num;
                y = this.ymin();
            }
            if ( y + len > this.ymax() )
            {
                len = this.ymax() - y + 1;
                if ( len <= 0 )
                    return;
            }
            base.blend_color_vspan( x, y, len, colors, colorsIndex, covers, coversIndex, firstCoverForAll );
        }
    }
}
