// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Image.ImageProxyFloat
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using MatterHackers.VectorMath;

namespace MatterHackers.Agg.Image
{
    internal abstract class ImageProxyFloat : IImageFloat, IImage
    {
        protected IImageFloat linkedImage;

        public ImageProxyFloat( IImageFloat linkedImage )
        {
            this.linkedImage = linkedImage;
        }

        public virtual void LinkToImage( IImageFloat linkedImage )
        {
            this.linkedImage = linkedImage;
        }

        public virtual Vector2 OriginOffset
        {
            get
            {
                return this.linkedImage.OriginOffset;
            }
            set
            {
                this.linkedImage.OriginOffset = value;
            }
        }

        public virtual int Width
        {
            get
            {
                return this.linkedImage.Width;
            }
        }

        public virtual int Height
        {
            get
            {
                return this.linkedImage.Height;
            }
        }

        public virtual int StrideInFloats()
        {
            return this.linkedImage.StrideInFloats();
        }

        public virtual int StrideInFloatsAbs()
        {
            return this.linkedImage.StrideInFloatsAbs();
        }

        public virtual RectangleInt GetBounds()
        {
            return this.linkedImage.GetBounds();
        }

        public Graphics2D NewGraphics2D()
        {
            return this.linkedImage.NewGraphics2D();
        }

        public IBlenderFloat GetBlender()
        {
            return this.linkedImage.GetBlender();
        }

        public void SetBlender( IBlenderFloat value )
        {
            this.linkedImage.SetBlender( value );
        }

        public virtual RGBA_Floats GetPixel( int x, int y )
        {
            return this.linkedImage.GetPixel( y, x );
        }

        public virtual void copy_pixel( int x, int y, float[ ] c, int FloatOffset )
        {
            this.linkedImage.copy_pixel( x, y, c, FloatOffset );
        }

        public virtual void CopyFrom( IImageFloat sourceRaster )
        {
            this.linkedImage.CopyFrom( sourceRaster );
        }

        public virtual void CopyFrom( IImageFloat sourceImage, RectangleInt sourceImageRect, int destXOffset, int destYOffset )
        {
            this.linkedImage.CopyFrom( sourceImage, sourceImageRect, destXOffset, destYOffset );
        }

        public virtual void SetPixel( int x, int y, RGBA_Floats color )
        {
            this.linkedImage.SetPixel( x, y, color );
        }

        public virtual void BlendPixel( int x, int y, RGBA_Floats sourceColor, byte cover )
        {
            this.linkedImage.BlendPixel( x, y, sourceColor, cover );
        }

        public virtual void copy_hline( int x, int y, int len, RGBA_Floats sourceColor )
        {
            this.linkedImage.copy_hline( x, y, len, sourceColor );
        }

        public virtual void copy_vline( int x, int y, int len, RGBA_Floats sourceColor )
        {
            this.linkedImage.copy_vline( x, y, len, sourceColor );
        }

        public virtual void blend_hline( int x1, int y, int x2, RGBA_Floats sourceColor, byte cover )
        {
            this.linkedImage.blend_hline( x1, y, x2, sourceColor, cover );
        }

        public virtual void blend_vline( int x, int y1, int y2, RGBA_Floats sourceColor, byte cover )
        {
            this.linkedImage.blend_vline( x, y1, y2, sourceColor, cover );
        }

        public virtual void blend_solid_hspan( int x, int y, int len, RGBA_Floats c, byte[ ] covers, int coversIndex )
        {
            this.linkedImage.blend_solid_hspan( x, y, len, c, covers, coversIndex );
        }

        public virtual void copy_color_hspan( int x, int y, int len, RGBA_Floats[ ] colors, int colorIndex )
        {
            this.linkedImage.copy_color_hspan( x, y, len, colors, colorIndex );
        }

        public virtual void copy_color_vspan( int x, int y, int len, RGBA_Floats[ ] colors, int colorIndex )
        {
            this.linkedImage.copy_color_vspan( x, y, len, colors, colorIndex );
        }

        public virtual void blend_solid_vspan( int x, int y, int len, RGBA_Floats c, byte[ ] covers, int coversIndex )
        {
            this.linkedImage.blend_solid_vspan( x, y, len, c, covers, coversIndex );
        }

        public virtual void blend_color_hspan( int x, int y, int len, RGBA_Floats[ ] colors, int colorsIndex, byte[ ] covers, int coversIndex, bool firstCoverForAll )
        {
            this.linkedImage.blend_color_hspan( x, y, len, colors, colorsIndex, covers, coversIndex, firstCoverForAll );
        }

        public virtual void blend_color_vspan( int x, int y, int len, RGBA_Floats[ ] colors, int colorsIndex, byte[ ] covers, int coversIndex, bool firstCoverForAll )
        {
            this.linkedImage.blend_color_vspan( x, y, len, colors, colorsIndex, covers, coversIndex, firstCoverForAll );
        }

        public float[ ] GetBuffer()
        {
            return this.linkedImage.GetBuffer();
        }

        public int GetBufferOffsetY( int y )
        {
            return this.linkedImage.GetBufferOffsetY( y );
        }

        public int GetBufferOffsetXY( int x, int y )
        {
            return this.linkedImage.GetBufferOffsetXY( x, y );
        }

        public virtual int GetFloatsBetweenPixelsInclusive()
        {
            return this.linkedImage.GetFloatsBetweenPixelsInclusive();
        }

        public virtual int BitDepth
        {
            get
            {
                return this.linkedImage.BitDepth;
            }
        }

        public void MarkImageChanged()
        {
            this.linkedImage.MarkImageChanged();
        }
    }
}
