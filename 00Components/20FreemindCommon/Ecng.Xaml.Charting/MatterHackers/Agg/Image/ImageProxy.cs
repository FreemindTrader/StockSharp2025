// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Image.ImageProxy
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using MatterHackers.VectorMath;

namespace MatterHackers.Agg.Image
{
    internal abstract class ImageProxy : IImageByte, IImage
    {
        protected IImageByte _linkedImage;

        public IImageByte LinkedImage
        {
            get
            {
                return this._linkedImage;
            }
            set
            {
                this._linkedImage = value;
            }
        }

        public ImageProxy( IImageByte linkedImage )
        {
            this._linkedImage = linkedImage;
        }

        public virtual void LinkToImage( IImageByte linkedImage )
        {
            this._linkedImage = linkedImage;
        }

        public virtual Vector2 OriginOffset
        {
            get
            {
                return this._linkedImage.OriginOffset;
            }
            set
            {
                this._linkedImage.OriginOffset = value;
            }
        }

        public virtual int Width
        {
            get
            {
                return this._linkedImage.Width;
            }
        }

        public virtual int Height
        {
            get
            {
                return this._linkedImage.Height;
            }
        }

        public virtual int StrideInBytes()
        {
            return this._linkedImage.StrideInBytes();
        }

        public virtual int StrideInBytesAbs()
        {
            return this._linkedImage.StrideInBytesAbs();
        }

        public virtual RectangleInt GetBounds()
        {
            return this._linkedImage.GetBounds();
        }

        public Graphics2D NewGraphics2D()
        {
            return this._linkedImage.NewGraphics2D();
        }

        public IBlenderByte GetBlender()
        {
            return this._linkedImage.GetBlender();
        }

        public void SetBlender( IBlenderByte value )
        {
            this._linkedImage.SetBlender( value );
        }

        public virtual RGBA_Bytes GetPixel( int x, int y )
        {
            return this._linkedImage.GetPixel( x, y );
        }

        public virtual void copy_pixel( int x, int y, byte[ ] c, int ByteOffset )
        {
            this._linkedImage.copy_pixel( x, y, c, ByteOffset );
        }

        public virtual void CopyFrom( IImageByte sourceRaster )
        {
            this._linkedImage.CopyFrom( sourceRaster );
        }

        public virtual void CopyFrom( IImageByte sourceImage, RectangleInt sourceImageRect, int destXOffset, int destYOffset )
        {
            this._linkedImage.CopyFrom( sourceImage, sourceImageRect, destXOffset, destYOffset );
        }

        public virtual void SetPixel( int x, int y, RGBA_Bytes color )
        {
            this._linkedImage.SetPixel( x, y, color );
        }

        public virtual void BlendPixel( int x, int y, RGBA_Bytes sourceColor, byte cover )
        {
            this._linkedImage.BlendPixel( x, y, sourceColor, cover );
        }

        public virtual void copy_hline( int x, int y, int len, RGBA_Bytes sourceColor )
        {
            this._linkedImage.copy_hline( x, y, len, sourceColor );
        }

        public virtual void copy_vline( int x, int y, int len, RGBA_Bytes sourceColor )
        {
            this._linkedImage.copy_vline( x, y, len, sourceColor );
        }

        public virtual void blend_hline( int x1, int y, int x2, RGBA_Bytes sourceColor, byte cover )
        {
            this._linkedImage.blend_hline( x1, y, x2, sourceColor, cover );
        }

        public virtual void blend_vline( int x, int y1, int y2, RGBA_Bytes sourceColor, byte cover )
        {
            this._linkedImage.blend_vline( x, y1, y2, sourceColor, cover );
        }

        public virtual void blend_solid_hspan( int x, int y, int len, RGBA_Bytes c, byte[ ] covers, int coversIndex )
        {
            this._linkedImage.blend_solid_hspan( x, y, len, c, covers, coversIndex );
        }

        public virtual void copy_color_hspan( int x, int y, int len, RGBA_Bytes[ ] colors, int colorIndex )
        {
            this._linkedImage.copy_color_hspan( x, y, len, colors, colorIndex );
        }

        public virtual void copy_color_vspan( int x, int y, int len, RGBA_Bytes[ ] colors, int colorIndex )
        {
            this._linkedImage.copy_color_vspan( x, y, len, colors, colorIndex );
        }

        public virtual void blend_solid_vspan( int x, int y, int len, RGBA_Bytes c, byte[ ] covers, int coversIndex )
        {
            this._linkedImage.blend_solid_vspan( x, y, len, c, covers, coversIndex );
        }

        public virtual void blend_color_hspan( int x, int y, int len, RGBA_Bytes[ ] colors, int colorsIndex, byte[ ] covers, int coversIndex, bool firstCoverForAll )
        {
            this._linkedImage.blend_color_hspan( x, y, len, colors, colorsIndex, covers, coversIndex, firstCoverForAll );
        }

        public virtual void blend_color_vspan( int x, int y, int len, RGBA_Bytes[ ] colors, int colorsIndex, byte[ ] covers, int coversIndex, bool firstCoverForAll )
        {
            this._linkedImage.blend_color_vspan( x, y, len, colors, colorsIndex, covers, coversIndex, firstCoverForAll );
        }

        public byte[ ] GetBuffer()
        {
            return this._linkedImage.GetBuffer();
        }

        public int GetBufferOffsetXY( int x, int y )
        {
            return this._linkedImage.GetBufferOffsetXY( x, y );
        }

        public int GetBufferOffsetY( int y )
        {
            return this._linkedImage.GetBufferOffsetY( y );
        }

        public virtual int GetBytesBetweenPixelsInclusive()
        {
            return this._linkedImage.GetBytesBetweenPixelsInclusive();
        }

        public virtual int BitDepth
        {
            get
            {
                return this._linkedImage.BitDepth;
            }
        }

        public void MarkImageChanged()
        {
            this._linkedImage.MarkImageChanged();
        }

        public void blend_hline( int x, int y, int x2, Func<int, int, RGBA_Bytes> sourceColorCb, byte cover )
        {
            this._linkedImage.blend_hline( x, y, x2, sourceColorCb, cover );
        }

        public void blend_solid_hspan( int x, int y, int len, Func<int, int, RGBA_Bytes> sourceColorCb, byte[ ] covers, int coversIndex )
        {
            this._linkedImage.blend_solid_hspan( x, y, len, sourceColorCb, covers, coversIndex );
        }
    }
}
