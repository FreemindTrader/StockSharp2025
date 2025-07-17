// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Image.ImageBufferFloat
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using MatterHackers.Agg.RasterizerScanline;
using MatterHackers.VectorMath;

namespace MatterHackers.Agg.Image
{
    internal class ImageBufferFloat : IImageFloat, IImage
    {
        private Vector2 m_OriginOffset = new Vector2(0.0, 0.0);
        public const int OrderB = 0;
        public const int OrderG = 1;
        public const int OrderR = 2;
        public const int OrderA = 3;
        protected int[] m_yTable;
        protected int[] m_xTable;
        private float[] m_FloatBuffer;
        private int m_BufferOffset;
        private int m_BufferFirstPixel;
        private int m_Width;
        private int m_Height;
        private int m_StrideInFloats;
        private int m_DistanceInFloatsBetweenPixelsInclusive;
        private int m_BitDepth;
        private IBlenderFloat m_Blender;
        private int changedCount;

        public int ChangedCount
        {
            get
            {
                return this.changedCount;
            }
        }

        public void MarkImageChanged()
        {
            ++this.changedCount;
        }

        public ImageBufferFloat()
        {
        }

        public ImageBufferFloat( IBlenderFloat blender )
        {
            this.SetBlender( blender );
        }

        public ImageBufferFloat( IImageFloat sourceImage, IBlenderFloat blender )
        {
            this.SetDimmensionAndFormat( sourceImage.Width, sourceImage.Height, sourceImage.StrideInFloats(), sourceImage.BitDepth, sourceImage.GetFloatsBetweenPixelsInclusive() );
            int bufferOffsetXy = sourceImage.GetBufferOffsetXY(0, 0);
            float[] buffer = sourceImage.GetBuffer();
            float[] numArray = new float[buffer.Length];
            agg_basics.memcpy( numArray, bufferOffsetXy, buffer, bufferOffsetXy, buffer.Length - bufferOffsetXy );
            this.SetBuffer( numArray, bufferOffsetXy );
            this.SetBlender( blender );
        }

        public ImageBufferFloat( int width, int height, int bitsPerPixel, IBlenderFloat blender )
        {
            this.Allocate( width, height, width * ( bitsPerPixel / 32 ), bitsPerPixel );
            this.SetBlender( blender );
        }

        public ImageBufferFloat( IImageFloat sourceImageToCopy, IBlenderFloat blender, int distanceBetweenPixelsInclusive, int bufferOffset, int bitsPerPixel )
        {
            this.SetDimmensionAndFormat( sourceImageToCopy.Width, sourceImageToCopy.Height, sourceImageToCopy.StrideInFloats(), bitsPerPixel, distanceBetweenPixelsInclusive );
            sourceImageToCopy.GetBufferOffsetXY( 0, 0 );
            float[] numArray = new float[sourceImageToCopy.GetBuffer().Length];
            throw new NotImplementedException();
        }

        public void AttachBuffer( float[ ] buffer, int bufferOffset, int width, int height, int strideInBytes, int bitDepth, int distanceInBytesBetweenPixelsInclusive )
        {
            this.m_FloatBuffer = ( float[ ] ) null;
            this.SetDimmensionAndFormat( width, height, strideInBytes, bitDepth, distanceInBytesBetweenPixelsInclusive );
            this.SetBuffer( buffer, bufferOffset );
        }

        public void Attach( IImageFloat sourceImage, IBlenderFloat blender, int distanceBetweenPixelsInclusive, int bufferOffset, int bitsPerPixel )
        {
            this.SetDimmensionAndFormat( sourceImage.Width, sourceImage.Height, sourceImage.StrideInFloats(), bitsPerPixel, distanceBetweenPixelsInclusive );
            int bufferOffsetXy = sourceImage.GetBufferOffsetXY(0, 0);
            this.SetBuffer( sourceImage.GetBuffer(), bufferOffsetXy + bufferOffset );
            this.SetBlender( blender );
        }

        public void Attach( IImageFloat sourceImage, IBlenderFloat blender )
        {
            this.Attach( sourceImage, blender, sourceImage.GetFloatsBetweenPixelsInclusive(), 0, sourceImage.BitDepth );
        }

        public bool Attach( IImageFloat sourceImage, int x1, int y1, int x2, int y2 )
        {
            this.m_FloatBuffer = ( float[ ] ) null;
            this.DettachBuffer();
            if ( x1 > x2 || y1 > y2 )
                throw new Exception( "You need to have your x1 and y1 be the lower left corner of your sub image." );
            RectangleInt rectangleInt = new RectangleInt(x1, y1, x2, y2);
            if ( !rectangleInt.clip( new RectangleInt( 0, 0, sourceImage.Width - 1, sourceImage.Height - 1 ) ) )
                return false;
            this.SetDimmensionAndFormat( rectangleInt.Width, rectangleInt.Height, sourceImage.StrideInFloats(), sourceImage.BitDepth, sourceImage.GetFloatsBetweenPixelsInclusive() );
            int bufferOffsetXy = sourceImage.GetBufferOffsetXY(rectangleInt.Left, rectangleInt.Bottom);
            this.SetBuffer( sourceImage.GetBuffer(), bufferOffsetXy );
            return true;
        }

        public void SetAlpha( byte value )
        {
            if ( this.BitDepth != 32 )
                throw new Exception( "You don't have alpha channel to set.  Your image has a bit depth of " + this.BitDepth.ToString() + "." );
            int num = this.Width * this.Height;
            int bufferOffset;
            float[] buffer = this.GetBuffer(out bufferOffset);
            for ( int index = 0 ; index < num ; ++index )
                buffer[ bufferOffset + index * 4 + 3 ] = ( float ) value;
        }

        private void Deallocate()
        {
            this.m_FloatBuffer = ( float[ ] ) null;
            this.SetDimmensionAndFormat( 0, 0, 0, 32, 4 );
        }

        public void Allocate( int inWidth, int inHeight, int inScanWidthInFloats, int bitsPerPixel )
        {
            if ( bitsPerPixel != 128 && bitsPerPixel != 96 && bitsPerPixel != 32 )
                throw new Exception( "Unsupported bits per pixel." );
            if ( inScanWidthInFloats < inWidth * ( bitsPerPixel / 32 ) )
                throw new Exception( "Your scan width is not big enough to hold your width and height." );
            this.SetDimmensionAndFormat( inWidth, inHeight, inScanWidthInFloats, bitsPerPixel, bitsPerPixel / 32 );
            this.m_FloatBuffer = new float[ this.m_StrideInFloats * this.m_Height ];
            this.SetUpLookupTables();
        }

        public Graphics2D NewGraphics2D()
        {
            ImageBufferFloat.InternalImageGraphics2D internalImageGraphics2D = new ImageBufferFloat.InternalImageGraphics2D(this);
            internalImageGraphics2D.Rasterizer.SetVectorClipBox( 0.0, 0.0, ( double ) this.Width, ( double ) this.Height );
            return ( Graphics2D ) internalImageGraphics2D;
        }

        public void CopyFrom( IImageFloat sourceImage )
        {
            this.CopyFrom( sourceImage, sourceImage.GetBounds(), 0, 0 );
        }

        protected void CopyFromNoClipping( IImageFloat sourceImage, RectangleInt clippedSourceImageRect, int destXOffset, int destYOffset )
        {
            if ( this.GetFloatsBetweenPixelsInclusive() != this.BitDepth / 32 || sourceImage.GetFloatsBetweenPixelsInclusive() != sourceImage.BitDepth / 32 )
                throw new Exception( "WIP we only support packed pixel formats at this time." );
            if ( this.BitDepth == sourceImage.BitDepth )
            {
                int Count = clippedSourceImageRect.Width * this.GetFloatsBetweenPixelsInclusive();
                int bufferOffsetXy = sourceImage.GetBufferOffsetXY(clippedSourceImageRect.Left, clippedSourceImageRect.Bottom);
                float[] buffer = sourceImage.GetBuffer();
                int bufferOffset;
                float[] pixelPointerXy = this.GetPixelPointerXY(clippedSourceImageRect.Left + destXOffset, clippedSourceImageRect.Bottom + destYOffset, out bufferOffset);
                for ( int index = 0 ; index < clippedSourceImageRect.Height ; ++index )
                {
                    agg_basics.memmove( pixelPointerXy, bufferOffset, buffer, bufferOffsetXy, Count );
                    bufferOffsetXy += sourceImage.StrideInFloats();
                    bufferOffset += this.StrideInFloats();
                }
            }
            else
            {
                bool flag = true;
                if ( sourceImage.BitDepth == 24 )
                {
                    if ( this.BitDepth == 32 )
                    {
                        int width = clippedSourceImageRect.Width;
                        for ( int bottom = clippedSourceImageRect.Bottom ; bottom < clippedSourceImageRect.Top ; ++bottom )
                        {
                            int num1 = sourceImage.GetBufferOffsetXY(clippedSourceImageRect.Left, clippedSourceImageRect.Bottom + bottom);
                            float[] buffer = sourceImage.GetBuffer();
                            int bufferOffset;
                            float[] pixelPointerXy = this.GetPixelPointerXY(clippedSourceImageRect.Left + destXOffset, clippedSourceImageRect.Bottom + bottom + destYOffset, out bufferOffset);
                            for ( int index1 = 0 ; index1 < width ; ++index1 )
                            {
                                float[] numArray1 = pixelPointerXy;
                                int index2 = bufferOffset;
                                int num2 = index2 + 1;
                                float[] numArray2 = buffer;
                                int index3 = num1;
                                int num3 = index3 + 1;
                                double num4 = (double) numArray2[index3];
                                numArray1[ index2 ] = ( float ) num4;
                                float[] numArray3 = pixelPointerXy;
                                int index4 = num2;
                                int num5 = index4 + 1;
                                float[] numArray4 = buffer;
                                int index5 = num3;
                                int num6 = index5 + 1;
                                double num7 = (double) numArray4[index5];
                                numArray3[ index4 ] = ( float ) num7;
                                float[] numArray5 = pixelPointerXy;
                                int index6 = num5;
                                int num8 = index6 + 1;
                                float[] numArray6 = buffer;
                                int index7 = num6;
                                num1 = index7 + 1;
                                double num9 = (double) numArray6[index7];
                                numArray5[ index6 ] = ( float ) num9;
                                float[] numArray7 = pixelPointerXy;
                                int index8 = num8;
                                bufferOffset = index8 + 1;
                                double maxValue = (double) byte.MaxValue;
                                numArray7[ index8 ] = ( float ) maxValue;
                            }
                        }
                    }
                    else
                        flag = false;
                }
                else
                    flag = false;
                if ( !flag )
                    throw new NotImplementedException( "You need to write the " + sourceImage.BitDepth.ToString() + " to " + this.BitDepth.ToString() + " conversion" );
            }
        }

        public void CopyFrom( IImageFloat sourceImage, RectangleInt sourceImageRect, int destXOffset, int destYOffset )
        {
            RectangleInt bounds1 = sourceImage.GetBounds();
            RectangleInt rectangleInt1 = new RectangleInt();
            if ( !rectangleInt1.IntersectRectangles( sourceImageRect, bounds1 ) )
                return;
            RectangleInt rectToCopy = rectangleInt1;
            rectToCopy.Offset( destXOffset, destYOffset );
            RectangleInt bounds2 = this.GetBounds();
            RectangleInt rectangleInt2 = new RectangleInt();
            if ( !rectangleInt2.IntersectRectangles( rectToCopy, bounds2 ) )
                return;
            RectangleInt clippedSourceImageRect = rectangleInt2;
            clippedSourceImageRect.Offset( -destXOffset, -destYOffset );
            this.CopyFromNoClipping( sourceImage, clippedSourceImageRect, destXOffset, destYOffset );
        }

        public Vector2 OriginOffset
        {
            get
            {
                return this.m_OriginOffset;
            }
            set
            {
                this.m_OriginOffset = value;
            }
        }

        public int Width
        {
            get
            {
                return this.m_Width;
            }
        }

        public int Height
        {
            get
            {
                return this.m_Height;
            }
        }

        public int StrideInFloats()
        {
            return this.m_StrideInFloats;
        }

        public int StrideInFloatsAbs()
        {
            return Math.Abs( this.m_StrideInFloats );
        }

        public int GetFloatsBetweenPixelsInclusive()
        {
            return this.m_DistanceInFloatsBetweenPixelsInclusive;
        }

        public int BitDepth
        {
            get
            {
                return this.m_BitDepth;
            }
        }

        public virtual RectangleInt GetBounds()
        {
            return new RectangleInt( -( int ) this.m_OriginOffset.x, -( int ) this.m_OriginOffset.y, this.Width - ( int ) this.m_OriginOffset.x, this.Height - ( int ) this.m_OriginOffset.y );
        }

        public IBlenderFloat GetBlender()
        {
            return this.m_Blender;
        }

        public void SetBlender( IBlenderFloat value )
        {
            if ( value != null && value.NumPixelBits != this.BitDepth )
                throw new NotSupportedException( "The blender has to support the bit depth of this image." );
            this.m_Blender = value;
        }

        private void SetUpLookupTables()
        {
            this.m_yTable = new int[ this.m_Height ];
            for ( int index = 0 ; index < this.m_Height ; ++index )
                this.m_yTable[ index ] = index * this.m_StrideInFloats;
            this.m_xTable = new int[ this.m_Width ];
            for ( int index = 0 ; index < this.m_Width ; ++index )
                this.m_xTable[ index ] = index * this.m_DistanceInFloatsBetweenPixelsInclusive;
        }

        public void FlipY()
        {
            this.m_StrideInFloats *= -1;
            this.m_BufferFirstPixel = this.m_BufferOffset;
            if ( this.m_StrideInFloats < 0 )
                this.m_BufferFirstPixel = -( ( this.m_Height - 1 ) * this.m_StrideInFloats ) + this.m_BufferOffset;
            this.SetUpLookupTables();
        }

        public void SetBuffer( float[ ] floatBuffer, int bufferOffset )
        {
            if ( floatBuffer.Length < this.m_Height * this.m_StrideInFloats )
                throw new Exception( "Your buffer does not have enough room it it for your height and strideInBytes." );
            this.m_FloatBuffer = floatBuffer;
            this.m_BufferOffset = this.m_BufferFirstPixel = bufferOffset;
            if ( this.m_StrideInFloats < 0 )
                this.m_BufferFirstPixel = -( ( this.m_Height - 1 ) * this.m_StrideInFloats ) + this.m_BufferOffset;
            this.SetUpLookupTables();
        }

        private void SetDimmensionAndFormat( int width, int height, int strideInFloats, int bitDepth, int distanceInFloatsBetweenPixelsInclusive )
        {
            if ( this.m_FloatBuffer != null )
                throw new Exception( "You already have a buffer set. You need to set dimmensoins before the buffer.  You may need to clear the buffer first." );
            this.m_Width = width;
            this.m_Height = height;
            this.m_StrideInFloats = strideInFloats;
            this.m_BitDepth = bitDepth;
            if ( distanceInFloatsBetweenPixelsInclusive > 4 )
                throw new Exception( "It looks like you are passing bits per pixel rather than distance in Floats." );
            if ( distanceInFloatsBetweenPixelsInclusive < bitDepth / 32 )
                throw new Exception( "You do not have enough room between pixels to support your bit depth." );
            this.m_DistanceInFloatsBetweenPixelsInclusive = distanceInFloatsBetweenPixelsInclusive;
            if ( strideInFloats < distanceInFloatsBetweenPixelsInclusive * width )
                throw new Exception( "You do not have enough strideInFloats to hold the width and pixel distance you have described." );
        }

        public void DettachBuffer()
        {
            this.m_FloatBuffer = ( float[ ] ) null;
            this.m_Width = this.m_Height = this.m_StrideInFloats = this.m_DistanceInFloatsBetweenPixelsInclusive = 0;
        }

        public float[ ] GetBuffer()
        {
            return this.m_FloatBuffer;
        }

        public float[ ] GetBuffer( out int bufferOffset )
        {
            bufferOffset = this.m_BufferOffset;
            return this.m_FloatBuffer;
        }

        public float[ ] GetPixelPointerY( int y, out int bufferOffset )
        {
            bufferOffset = this.m_BufferFirstPixel + this.m_yTable[ y ];
            return this.m_FloatBuffer;
        }

        public float[ ] GetPixelPointerXY( int x, int y, out int bufferOffset )
        {
            bufferOffset = this.GetBufferOffsetXY( x, y );
            return this.m_FloatBuffer;
        }

        public RGBA_Floats GetPixel( int x, int y )
        {
            return this.m_Blender.PixelToColorRGBA_Floats( this.m_FloatBuffer, this.GetBufferOffsetXY( x, y ) );
        }

        public virtual void SetPixel( int x, int y, RGBA_Floats color )
        {
            x -= ( int ) this.m_OriginOffset.x;
            y -= ( int ) this.m_OriginOffset.y;
            this.m_Blender.CopyPixels( this.GetBuffer(), this.GetBufferOffsetXY( x, y ), color, 1 );
        }

        public int GetBufferOffsetY( int y )
        {
            return this.m_BufferFirstPixel + this.m_yTable[ y ];
        }

        public int GetBufferOffsetXY( int x, int y )
        {
            return this.m_BufferFirstPixel + this.m_yTable[ y ] + this.m_xTable[ x ];
        }

        public void copy_pixel( int x, int y, float[ ] c, int ByteOffset )
        {
            throw new NotImplementedException();
        }

        public void BlendPixel( int x, int y, RGBA_Floats c, byte cover )
        {
            throw new NotImplementedException();
        }

        public void SetPixelFromColor( float[ ] destPixel, IColorType c )
        {
            throw new NotImplementedException();
        }

        public void copy_hline( int x, int y, int len, RGBA_Floats sourceColor )
        {
            int bufferOffset;
            this.m_Blender.CopyPixels( this.GetPixelPointerXY( x, y, out bufferOffset ), bufferOffset, sourceColor, len );
        }

        public void copy_vline( int x, int y, int len, RGBA_Floats sourceColor )
        {
            throw new NotImplementedException();
        }

        public void blend_hline( int x1, int y, int x2, RGBA_Floats sourceColor, byte cover )
        {
            if ( ( double ) sourceColor.alpha == 0.0 )
                return;
            int count = x2 - x1 + 1;
            int bufferOffset;
            float[] pixelPointerXy = this.GetPixelPointerXY(x1, y, out bufferOffset);
            float a_ = sourceColor.alpha * ((float) cover * 0.003921569f);
            if ( ( double ) a_ == 1.0 )
            {
                this.m_Blender.CopyPixels( pixelPointerXy, bufferOffset, sourceColor, count );
            }
            else
            {
                do
                {
                    this.m_Blender.BlendPixel( pixelPointerXy, bufferOffset, new RGBA_Floats( sourceColor.red, sourceColor.green, sourceColor.blue, a_ ) );
                    bufferOffset += this.m_DistanceInFloatsBetweenPixelsInclusive;
                }
                while ( --count != 0 );
            }
        }

        public void blend_vline( int x, int y1, int y2, RGBA_Floats sourceColor, byte cover )
        {
            throw new NotImplementedException();
        }

        public void blend_solid_hspan( int x, int y, int len, RGBA_Floats sourceColor, byte[ ] covers, int coversIndex )
        {
            float alpha = sourceColor.alpha;
            if ( ( double ) alpha == 0.0 )
                return;
            int bufferOffset;
            float[] pixelPointerXy = this.GetPixelPointerXY(x, y, out bufferOffset);
            do
            {
                float a_ = alpha * ((float) covers[coversIndex] * 0.003921569f);
                if ( ( double ) a_ == 1.0 )
                    this.m_Blender.CopyPixels( pixelPointerXy, bufferOffset, sourceColor, 1 );
                else
                    this.m_Blender.BlendPixel( pixelPointerXy, bufferOffset, new RGBA_Floats( sourceColor.red, sourceColor.green, sourceColor.blue, a_ ) );
                bufferOffset += this.m_DistanceInFloatsBetweenPixelsInclusive;
                ++coversIndex;
            }
            while ( --len != 0 );
        }

        public void blend_solid_vspan( int x, int y, int len, RGBA_Floats c, byte[ ] covers, int coversIndex )
        {
            throw new NotImplementedException();
        }

        public void copy_color_hspan( int x, int y, int len, RGBA_Floats[ ] colors, int colorsIndex )
        {
            int bufferOffsetXy = this.GetBufferOffsetXY(x, y);
            do
            {
                this.m_Blender.CopyPixels( this.m_FloatBuffer, bufferOffsetXy, colors[ colorsIndex ], 1 );
                ++colorsIndex;
                bufferOffsetXy += this.m_DistanceInFloatsBetweenPixelsInclusive;
            }
            while ( --len != 0 );
        }

        public void copy_color_vspan( int x, int y, int len, RGBA_Floats[ ] colors, int colorsIndex )
        {
            int bufferOffsetXy = this.GetBufferOffsetXY(x, y);
            do
            {
                this.m_Blender.CopyPixels( this.m_FloatBuffer, bufferOffsetXy, colors[ colorsIndex ], 1 );
                ++colorsIndex;
                bufferOffsetXy += this.m_StrideInFloats;
            }
            while ( --len != 0 );
        }

        public void blend_color_hspan( int x, int y, int len, RGBA_Floats[ ] colors, int colorsIndex, byte[ ] covers, int coversIndex, bool firstCoverForAll )
        {
            this.m_Blender.BlendPixels( this.m_FloatBuffer, this.GetBufferOffsetXY( x, y ), colors, colorsIndex, covers, coversIndex, firstCoverForAll, len );
        }

        public void blend_color_vspan( int x, int y, int len, RGBA_Floats[ ] colors, int colorsIndex, byte[ ] covers, int coversIndex, bool firstCoverForAll )
        {
            int bufferOffsetXy = this.GetBufferOffsetXY(x, y);
            int num = this.StrideInFloatsAbs();
            if ( !firstCoverForAll )
            {
                do
                {
                    DoCopyOrBlendFloat.BasedOnAlphaAndCover( this.m_Blender, this.m_FloatBuffer, bufferOffsetXy, colors[ colorsIndex ], ( int ) covers[ coversIndex++ ] );
                    bufferOffsetXy += num;
                    ++colorsIndex;
                }
                while ( --len != 0 );
            }
            else if ( covers[ coversIndex ] == ( byte ) 1 )
            {
                do
                {
                    DoCopyOrBlendFloat.BasedOnAlpha( this.m_Blender, this.m_FloatBuffer, bufferOffsetXy, colors[ colorsIndex ] );
                    bufferOffsetXy += num;
                    ++colorsIndex;
                }
                while ( --len != 0 );
            }
            else
            {
                do
                {
                    DoCopyOrBlendFloat.BasedOnAlphaAndCover( this.m_Blender, this.m_FloatBuffer, bufferOffsetXy, colors[ colorsIndex ], ( int ) covers[ coversIndex ] );
                    bufferOffsetXy += num;
                    ++colorsIndex;
                }
                while ( --len != 0 );
            }
        }

        public void apply_gamma_inv( GammaLookUpTable g )
        {
            throw new NotImplementedException();
        }

        private bool IsPixelVisible( int x, int y )
        {
            RGBA_Floats colorRgbaFloats = this.GetBlender().PixelToColorRGBA_Floats(this.m_FloatBuffer, this.GetBufferOffsetXY(x, y));
            if ( colorRgbaFloats.Alpha0To255 == 0 && colorRgbaFloats.Red0To255 == 0 && colorRgbaFloats.Green0To255 == 0 )
                return ( uint ) colorRgbaFloats.Blue0To255 > 0U;
            return true;
        }

        public void GetVisibleBounds( out RectangleInt visibleBounds )
        {
            visibleBounds = new RectangleInt( 0, 0, this.Width, this.Height );
            bool flag = false;
            for ( int y = 0 ; y < this.m_Height ; ++y )
            {
                for ( int x = 0 ; x < this.m_Width ; ++x )
                {
                    if ( this.IsPixelVisible( x, y ) )
                    {
                        visibleBounds.Bottom = y;
                        y = this.m_Height;
                        x = this.m_Width;
                        flag = true;
                    }
                }
            }
            if ( !flag )
            {
                visibleBounds.SetRect( 0, 0, 0, 0 );
            }
            else
            {
                for ( int y = this.m_Height - 1 ; y >= 0 ; --y )
                {
                    for ( int x = 0 ; x < this.m_Width ; ++x )
                    {
                        if ( this.IsPixelVisible( x, y ) )
                        {
                            visibleBounds.Top = y + 1;
                            y = -1;
                            x = this.m_Width;
                        }
                    }
                }
                for ( int x = 0 ; x < this.m_Width ; ++x )
                {
                    for ( int y = 0 ; y < this.m_Height ; ++y )
                    {
                        if ( this.IsPixelVisible( x, y ) )
                        {
                            visibleBounds.Left = x;
                            y = this.m_Height;
                            x = this.m_Width;
                        }
                    }
                }
                for ( int x = this.m_Width - 1 ; x >= 0 ; --x )
                {
                    for ( int y = 0 ; y < this.m_Height ; ++y )
                    {
                        if ( this.IsPixelVisible( x, y ) )
                        {
                            visibleBounds.Right = x + 1;
                            y = this.m_Height;
                            x = -1;
                        }
                    }
                }
            }
        }

        public void CropToVisible()
        {
            Vector2 originOffset = this.OriginOffset;
            this.OriginOffset = new Vector2( 0.0, 0.0 );
            RectangleInt visibleBounds;
            this.GetVisibleBounds( out visibleBounds );
            if ( visibleBounds.Width == this.Width && visibleBounds.Height == this.Height )
                this.OriginOffset = originOffset;
            else if ( visibleBounds.Width > 0 )
            {
                ImageBufferFloat sourceImage = new ImageBufferFloat();
                sourceImage.Initialize( this, visibleBounds );
                this.Initialize( sourceImage );
                this.OriginOffset = new Vector2( ( double ) -visibleBounds.Left + originOffset.x, ( double ) -visibleBounds.Bottom + originOffset.y );
            }
            else
                this.Deallocate();
        }

        public RectangleInt GetBoundingRect()
        {
            RectangleInt rectangleInt = new RectangleInt(0, 0, this.Width, this.Height);
            rectangleInt.Offset( ( int ) this.OriginOffset.x, ( int ) this.OriginOffset.y );
            return rectangleInt;
        }

        private void Initialize( ImageBufferFloat sourceImage )
        {
            RectangleInt boundingRect = sourceImage.GetBoundingRect();
            this.Initialize( sourceImage, boundingRect );
            this.OriginOffset = sourceImage.OriginOffset;
        }

        private void Initialize( ImageBufferFloat sourceImage, RectangleInt boundsToCopyFrom )
        {
            if ( sourceImage == this )
                throw new Exception( "We do not create a temp buffer for this to work.  You must have a source distinct from the dest." );
            this.Deallocate();
            this.Allocate( boundsToCopyFrom.Width, boundsToCopyFrom.Height, boundsToCopyFrom.Width * sourceImage.BitDepth / 8, sourceImage.BitDepth );
            this.SetBlender( sourceImage.GetBlender() );
            if ( this.m_Width == 0 || this.m_Height == 0 )
                return;
            RectangleInt rectangleInt = new RectangleInt(0, 0, boundsToCopyFrom.Width, boundsToCopyFrom.Height);
            Graphics2D graphics2D = this.NewGraphics2D();
            graphics2D.Clear( ( IColorType ) new RGBA_Floats( 0.0f, 0.0f, 0.0f, 0.0f ) );
            int num1 = -boundsToCopyFrom.Left - (int) sourceImage.OriginOffset.x;
            int num2 = -boundsToCopyFrom.Bottom - (int) sourceImage.OriginOffset.y;
            graphics2D.Render( ( IImageFloat ) sourceImage, ( double ) num1, ( double ) num2, 0.0, 1.0, 1.0 );
        }

        internal class InternalImageGraphics2D : ImageGraphics2D
        {
            private ImageBufferFloat m_Owner;

            internal InternalImageGraphics2D( ImageBufferFloat owner )
            {
                this.m_Owner = owner;
                ScanlineRasterizer rasterizer = new ScanlineRasterizer();
                this.Initialize( ( IImageFloat ) new ImageClippingProxyFloat( ( IImageFloat ) owner ), rasterizer );
                this.ScanlineCache = ( IScanlineCache ) new ScanlineCachePacked8();
            }
        }
    }
}
