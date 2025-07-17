// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Image.ImageBuffer
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using MatterHackers.Agg.RasterizerScanline;
using MatterHackers.VectorMath;

namespace MatterHackers.Agg.Image
{
    internal class ImageBuffer : IImageByte, IImage
    {
        private Vector2 m_OriginOffset = new Vector2(0.0, 0.0);
        public const int OrderB = 0;
        public const int OrderG = 1;
        public const int OrderR = 2;
        public const int OrderA = 3;
        protected int[] yTableArray;
        protected int[] xTableArray;
        private byte[] m_ByteBuffer;
        private int m_BufferOffset;
        private int m_BufferFirstPixel;
        private int m_Width;
        private int m_Height;
        private int m_StrideInBytes;
        private int m_DistanceInBytesBetweenPixelsInclusive;
        private int m_BitDepth;
        private IBlenderByte m_Blender;
        private const int base_mask = 255;
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

        public ImageBuffer()
        {
        }

        public ImageBuffer( IBlenderByte blender )
        {
            this.SetBlender( blender );
        }

        public ImageBuffer( IImageByte sourceImage, IBlenderByte blender )
        {
            this.SetDimmensionAndFormat( sourceImage.Width, sourceImage.Height, sourceImage.StrideInBytes(), sourceImage.BitDepth, sourceImage.GetBytesBetweenPixelsInclusive(), true );
            int bufferOffsetXy = sourceImage.GetBufferOffsetXY(0, 0);
            byte[] buffer = sourceImage.GetBuffer();
            byte[] numArray = new byte[buffer.Length];
            agg_basics.memcpy( numArray, bufferOffsetXy, buffer, bufferOffsetXy, buffer.Length - bufferOffsetXy );
            this.SetBuffer( numArray, bufferOffsetXy );
            this.SetBlender( blender );
        }

        public ImageBuffer( ImageBuffer sourceImage )
        {
            this.SetDimmensionAndFormat( sourceImage.Width, sourceImage.Height, sourceImage.StrideInBytes(), sourceImage.BitDepth, sourceImage.GetBytesBetweenPixelsInclusive(), true );
            int bufferOffsetXy = sourceImage.GetBufferOffsetXY(0, 0);
            byte[] buffer = sourceImage.GetBuffer();
            byte[] numArray = new byte[buffer.Length];
            agg_basics.memcpy( numArray, bufferOffsetXy, buffer, bufferOffsetXy, buffer.Length - bufferOffsetXy );
            this.SetBuffer( numArray, bufferOffsetXy );
            this.SetBlender( sourceImage.GetBlender() );
        }

        public ImageBuffer( int width, int height, int bitsPerPixel, IBlenderByte blender )
        {
            this.Allocate( width, height, width * ( bitsPerPixel / 8 ), bitsPerPixel );
            this.SetBlender( blender );
        }

        public void Allocate( int width, int height, int bitsPerPixel, IBlenderByte blender )
        {
            this.Allocate( width, height, width * ( bitsPerPixel / 8 ), bitsPerPixel );
            this.SetBlender( blender );
        }

        public ImageBuffer( IImageByte sourceImageToCopy, IBlenderByte blender, int distanceBetweenPixelsInclusive, int bufferOffset, int bitsPerPixel )
        {
            this.SetDimmensionAndFormat( sourceImageToCopy.Width, sourceImageToCopy.Height, sourceImageToCopy.StrideInBytes(), bitsPerPixel, distanceBetweenPixelsInclusive, true );
            int bufferOffsetXy = sourceImageToCopy.GetBufferOffsetXY(0, 0);
            byte[] buffer = sourceImageToCopy.GetBuffer();
            byte[] numArray = new byte[buffer.Length];
            agg_basics.memcpy( numArray, bufferOffsetXy, buffer, bufferOffsetXy, buffer.Length - bufferOffsetXy );
            this.SetBuffer( numArray, bufferOffsetXy + bufferOffset );
            this.SetBlender( blender );
        }

        public static ImageBuffer NewSubImageReference( IImageByte imageContainingSubImage, RectangleDouble subImageBounds )
        {
            ImageBuffer imageBuffer = new ImageBuffer();
            if ( subImageBounds.Left < 0.0 || subImageBounds.Bottom < 0.0 || ( subImageBounds.Right > ( double ) imageContainingSubImage.Width || subImageBounds.Top > ( double ) imageContainingSubImage.Height ) || ( subImageBounds.Left >= subImageBounds.Right || subImageBounds.Bottom >= subImageBounds.Top ) )
                throw new ArgumentException( "The subImageBounds must be on the image and valid." );
            int x = Math.Max(0, (int) Math.Floor(subImageBounds.Left));
            int y = Math.Max(0, (int) Math.Floor(subImageBounds.Bottom));
            int width = Math.Min(imageContainingSubImage.Width - x, (int) subImageBounds.Width);
            int height = Math.Min(imageContainingSubImage.Height - y, (int) subImageBounds.Height);
            int bufferOffsetXy = imageContainingSubImage.GetBufferOffsetXY(x, y);
            imageBuffer.AttachBuffer( imageContainingSubImage.GetBuffer(), bufferOffsetXy, width, height, imageContainingSubImage.StrideInBytes(), imageContainingSubImage.BitDepth, imageContainingSubImage.GetBytesBetweenPixelsInclusive() );
            imageBuffer.SetBlender( imageContainingSubImage.GetBlender() );
            return imageBuffer;
        }

        public void AttachBuffer( byte[ ] buffer, int bufferOffset, int width, int height, int strideInBytes, int bitDepth, int distanceInBytesBetweenPixelsInclusive )
        {
            this.m_ByteBuffer = ( byte[ ] ) null;
            this.SetDimmensionAndFormat( width, height, strideInBytes, bitDepth, distanceInBytesBetweenPixelsInclusive, false );
            this.SetBuffer( buffer, bufferOffset );
        }

        public void Attach( IImageByte sourceImage, IBlenderByte blender, int distanceBetweenPixelsInclusive, int bufferOffset, int bitsPerPixel )
        {
            this.SetDimmensionAndFormat( sourceImage.Width, sourceImage.Height, sourceImage.StrideInBytes(), bitsPerPixel, distanceBetweenPixelsInclusive, false );
            int bufferOffsetXy = sourceImage.GetBufferOffsetXY(0, 0);
            this.SetBuffer( sourceImage.GetBuffer(), bufferOffsetXy + bufferOffset );
            this.SetBlender( blender );
        }

        public void Attach( IImageByte sourceImage, IBlenderByte blender )
        {
            this.Attach( sourceImage, blender, sourceImage.GetBytesBetweenPixelsInclusive(), 0, sourceImage.BitDepth );
        }

        public bool Attach( IImageByte sourceImage, int x1, int y1, int x2, int y2 )
        {
            this.m_ByteBuffer = ( byte[ ] ) null;
            this.DettachBuffer();
            if ( x1 > x2 || y1 > y2 )
                throw new Exception( "You need to have your x1 and y1 be the lower left corner of your sub image." );
            RectangleInt rectangleInt = new RectangleInt(x1, y1, x2, y2);
            if ( !rectangleInt.clip( new RectangleInt( 0, 0, sourceImage.Width - 1, sourceImage.Height - 1 ) ) )
                return false;
            this.SetDimmensionAndFormat( rectangleInt.Width, rectangleInt.Height, sourceImage.StrideInBytes(), sourceImage.BitDepth, sourceImage.GetBytesBetweenPixelsInclusive(), false );
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
            byte[] buffer = this.GetBuffer(out bufferOffset);
            for ( int index = 0 ; index < num ; ++index )
                buffer[ bufferOffset + index * 4 + 3 ] = value;
        }

        private void Deallocate()
        {
            if ( this.m_ByteBuffer == null )
                return;
            this.m_ByteBuffer = ( byte[ ] ) null;
            this.SetDimmensionAndFormat( 0, 0, 0, 32, 4, true );
        }

        public void Allocate( int inWidth, int inHeight, int inScanWidthInBytes, int bitsPerPixel )
        {
            if ( inWidth < 1 || inHeight < 1 )
                throw new ArgumentOutOfRangeException( "You must have a width and height > than 0." );
            if ( bitsPerPixel != 32 && bitsPerPixel != 24 && bitsPerPixel != 8 )
                throw new Exception( "Unsupported bits per pixel." );
            if ( inScanWidthInBytes < inWidth * ( bitsPerPixel / 8 ) )
                throw new Exception( "Your scan width is not big enough to hold your width and height." );
            this.SetDimmensionAndFormat( inWidth, inHeight, inScanWidthInBytes, bitsPerPixel, bitsPerPixel / 8, true );
            if ( this.m_ByteBuffer == null || this.m_ByteBuffer.Length != this.m_StrideInBytes * this.m_Height )
            {
                this.m_ByteBuffer = new byte[ this.m_StrideInBytes * this.m_Height ];
                this.SetUpLookupTables();
            }
            if ( this.yTableArray.Length != inHeight || this.xTableArray.Length != inWidth )
                throw new Exception( "The yTable and xTable should be allocated correctly at this point. Figure out what happend." );
        }

        public Graphics2D NewGraphics2D()
        {
            ImageBuffer.InternalImageGraphics2D internalImageGraphics2D = new ImageBuffer.InternalImageGraphics2D(this);
            internalImageGraphics2D.Rasterizer.SetVectorClipBox( 0.0, 0.0, ( double ) this.Width, ( double ) this.Height );
            return ( Graphics2D ) internalImageGraphics2D;
        }

        public void CopyFrom( IImageByte sourceImage )
        {
            this.CopyFrom( sourceImage, sourceImage.GetBounds(), 0, 0 );
        }

        protected void CopyFromNoClipping( IImageByte sourceImage, RectangleInt clippedSourceImageRect, int destXOffset, int destYOffset )
        {
            if ( this.GetBytesBetweenPixelsInclusive() != this.BitDepth / 8 || sourceImage.GetBytesBetweenPixelsInclusive() != sourceImage.BitDepth / 8 )
                throw new Exception( "WIP we only support packed pixel formats at this time." );
            if ( this.BitDepth == sourceImage.BitDepth )
            {
                int Count = clippedSourceImageRect.Width * this.GetBytesBetweenPixelsInclusive();
                int bufferOffsetXy = sourceImage.GetBufferOffsetXY(clippedSourceImageRect.Left, clippedSourceImageRect.Bottom);
                byte[] buffer = sourceImage.GetBuffer();
                int bufferOffset;
                byte[] pixelPointerXy = this.GetPixelPointerXY(clippedSourceImageRect.Left + destXOffset, clippedSourceImageRect.Bottom + destYOffset, out bufferOffset);
                for ( int index = 0 ; index < clippedSourceImageRect.Height ; ++index )
                {
                    agg_basics.memmove( pixelPointerXy, bufferOffset, buffer, bufferOffsetXy, Count );
                    bufferOffsetXy += sourceImage.StrideInBytes();
                    bufferOffset += this.StrideInBytes();
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
                            byte[] buffer = sourceImage.GetBuffer();
                            int bufferOffset;
                            byte[] pixelPointerXy = this.GetPixelPointerXY(clippedSourceImageRect.Left + destXOffset, clippedSourceImageRect.Bottom + bottom + destYOffset, out bufferOffset);
                            for ( int index1 = 0 ; index1 < width ; ++index1 )
                            {
                                byte[] numArray1 = pixelPointerXy;
                                int index2 = bufferOffset;
                                int num2 = index2 + 1;
                                byte[] numArray2 = buffer;
                                int index3 = num1;
                                int num3 = index3 + 1;
                                int num4 = (int) numArray2[index3];
                                numArray1[ index2 ] = ( byte ) num4;
                                byte[] numArray3 = pixelPointerXy;
                                int index4 = num2;
                                int num5 = index4 + 1;
                                byte[] numArray4 = buffer;
                                int index5 = num3;
                                int num6 = index5 + 1;
                                int num7 = (int) numArray4[index5];
                                numArray3[ index4 ] = ( byte ) num7;
                                byte[] numArray5 = pixelPointerXy;
                                int index6 = num5;
                                int num8 = index6 + 1;
                                byte[] numArray6 = buffer;
                                int index7 = num6;
                                num1 = index7 + 1;
                                int num9 = (int) numArray6[index7];
                                numArray5[ index6 ] = ( byte ) num9;
                                byte[] numArray7 = pixelPointerXy;
                                int index8 = num8;
                                bufferOffset = index8 + 1;
                                int maxValue = (int) byte.MaxValue;
                                numArray7[ index8 ] = ( byte ) maxValue;
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

        public void CopyFrom( IImageByte sourceImage, RectangleInt sourceImageRect, int destXOffset, int destYOffset )
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

        public int StrideInBytes()
        {
            return this.m_StrideInBytes;
        }

        public int StrideInBytesAbs()
        {
            return Math.Abs( this.m_StrideInBytes );
        }

        public int GetBytesBetweenPixelsInclusive()
        {
            return this.m_DistanceInBytesBetweenPixelsInclusive;
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

        public IBlenderByte GetBlender()
        {
            return this.m_Blender;
        }

        public void SetBlender( IBlenderByte value )
        {
            if ( this.BitDepth != 0 && value != null && value.NumPixelBits != this.BitDepth )
                throw new NotSupportedException( "The blender has to support the bit depth of this image." );
            this.m_Blender = value;
        }

        private void SetUpLookupTables()
        {
            this.yTableArray = new int[ this.m_Height ];
            for ( int index = 0 ; index < this.m_Height ; ++index )
                this.yTableArray[ index ] = index * this.m_StrideInBytes;
            this.xTableArray = new int[ this.m_Width ];
            for ( int index = 0 ; index < this.m_Width ; ++index )
                this.xTableArray[ index ] = index * this.m_DistanceInBytesBetweenPixelsInclusive;
        }

        public void FlipY()
        {
            this.m_StrideInBytes *= -1;
            this.m_BufferFirstPixel = this.m_BufferOffset;
            if ( this.m_StrideInBytes < 0 )
                this.m_BufferFirstPixel = -( ( this.m_Height - 1 ) * this.m_StrideInBytes ) + this.m_BufferOffset;
            this.SetUpLookupTables();
        }

        public void SetBuffer( byte[ ] byteBuffer, int bufferOffset )
        {
            if ( byteBuffer.Length < this.m_Height * this.m_StrideInBytes )
                throw new Exception( "Your buffer does not have enough room it it for your height and strideInBytes." );
            this.m_ByteBuffer = byteBuffer;
            this.m_BufferOffset = this.m_BufferFirstPixel = bufferOffset;
            if ( this.m_StrideInBytes < 0 )
                this.m_BufferFirstPixel = -( ( this.m_Height - 1 ) * this.m_StrideInBytes ) + this.m_BufferOffset;
            this.SetUpLookupTables();
        }

        private void DeallocateOrClearBuffer( int width, int height, int strideInBytes, int bitDepth, int distanceInBytesBetweenPixelsInclusive )
        {
            if ( this.m_Width == width && this.m_Height == height && ( this.m_StrideInBytes == strideInBytes && this.m_BitDepth == bitDepth ) && ( this.m_DistanceInBytesBetweenPixelsInclusive == distanceInBytesBetweenPixelsInclusive && this.m_ByteBuffer != null ) )
            {
                for ( int index = 0 ; index < this.m_ByteBuffer.Length ; ++index )
                    this.m_ByteBuffer[ index ] = ( byte ) 0;
            }
            else
                this.Deallocate();
        }

        private void SetDimmensionAndFormat( int width, int height, int strideInBytes, int bitDepth, int distanceInBytesBetweenPixelsInclusive, bool doDeallocateOrClearBuffer )
        {
            if ( doDeallocateOrClearBuffer )
                this.DeallocateOrClearBuffer( width, height, strideInBytes, bitDepth, distanceInBytesBetweenPixelsInclusive );
            this.m_Width = width;
            this.m_Height = height;
            this.m_StrideInBytes = strideInBytes;
            this.m_BitDepth = bitDepth;
            if ( distanceInBytesBetweenPixelsInclusive > 4 )
                throw new Exception( "It looks like you are passing bits per pixel rather than distance in bytes." );
            if ( distanceInBytesBetweenPixelsInclusive < bitDepth / 8 )
                throw new Exception( "You do not have enough room between pixels to support your bit depth." );
            this.m_DistanceInBytesBetweenPixelsInclusive = distanceInBytesBetweenPixelsInclusive;
            if ( strideInBytes < distanceInBytesBetweenPixelsInclusive * width )
                throw new Exception( "You do not have enough strideInBytes to hold the width and pixel distance you have described." );
        }

        public void DettachBuffer()
        {
            this.m_ByteBuffer = ( byte[ ] ) null;
            this.m_Width = this.m_Height = this.m_StrideInBytes = this.m_DistanceInBytesBetweenPixelsInclusive = 0;
        }

        public byte[ ] GetBuffer()
        {
            return this.m_ByteBuffer;
        }

        public byte[ ] GetBuffer( out int bufferOffset )
        {
            bufferOffset = this.m_BufferOffset;
            return this.m_ByteBuffer;
        }

        public byte[ ] GetPixelPointerY( int y, out int bufferOffset )
        {
            bufferOffset = this.m_BufferFirstPixel + this.yTableArray[ y ];
            return this.m_ByteBuffer;
        }

        public byte[ ] GetPixelPointerXY( int x, int y, out int bufferOffset )
        {
            bufferOffset = this.GetBufferOffsetXY( x, y );
            return this.m_ByteBuffer;
        }

        public RGBA_Bytes GetPixel( int x, int y )
        {
            return this.m_Blender.PixelToColorRGBA_Bytes( this.m_ByteBuffer, this.GetBufferOffsetXY( x, y ) );
        }

        public int GetBufferOffsetY( int y )
        {
            return this.m_BufferFirstPixel + this.yTableArray[ y ] + this.xTableArray[ 0 ];
        }

        public int GetBufferOffsetXY( int x, int y )
        {
            return this.m_BufferFirstPixel + this.yTableArray[ y ] + this.xTableArray[ x ];
        }

        public void copy_pixel( int x, int y, byte[ ] c, int ByteOffset )
        {
            throw new NotImplementedException();
        }

        public void BlendPixel( int x, int y, RGBA_Bytes c, byte cover )
        {
            throw new NotImplementedException();
        }

        public void SetPixel( int x, int y, RGBA_Bytes color )
        {
            x -= ( int ) this.m_OriginOffset.x;
            y -= ( int ) this.m_OriginOffset.y;
            this.m_Blender.CopyPixels( this.GetBuffer(), this.GetBufferOffsetXY( x, y ), color, 1 );
        }

        public void copy_hline( int x, int y, int len, RGBA_Bytes sourceColor )
        {
            int bufferOffset;
            this.m_Blender.CopyPixels( this.GetPixelPointerXY( x, y, out bufferOffset ), bufferOffset, sourceColor, len );
        }

        public void copy_vline( int x, int y, int len, RGBA_Bytes sourceColor )
        {
            throw new NotImplementedException();
        }

        public void blend_hline( int x1, int y, int x2, RGBA_Bytes sourceColor, byte cover )
        {
            if ( sourceColor.alpha == ( byte ) 0 )
                return;
            int count = x2 - x1 + 1;
            int bufferOffset;
            byte[] pixelPointerXy = this.GetPixelPointerXY(x1, y, out bufferOffset);
            int a_ = (int) sourceColor.alpha * ((int) cover + 1) >> 8;
            if ( a_ == ( int ) byte.MaxValue )
            {
                this.m_Blender.CopyPixels( pixelPointerXy, bufferOffset, sourceColor, count );
            }
            else
            {
                do
                {
                    this.m_Blender.BlendPixel( pixelPointerXy, bufferOffset, new RGBA_Bytes( ( int ) sourceColor.red, ( int ) sourceColor.green, ( int ) sourceColor.blue, a_ ) );
                    bufferOffset += this.m_DistanceInBytesBetweenPixelsInclusive;
                }
                while ( --count != 0 );
            }
        }

        public void blend_vline( int x, int y1, int y2, RGBA_Bytes sourceColor, byte cover )
        {
            throw new NotImplementedException();
        }

        public void blend_solid_hspan( int x, int y, int len, RGBA_Bytes sourceColor, byte[ ] covers, int coversIndex )
        {
            int alpha = (int) sourceColor.alpha;
            if ( alpha == 0 )
                return;
            int bufferOffset;
            byte[] pixelPointerXy = this.GetPixelPointerXY(x, y, out bufferOffset);
            do
            {
                int a_ = alpha * ((int) covers[coversIndex] + 1) >> 8;
                if ( a_ == ( int ) byte.MaxValue )
                    this.m_Blender.CopyPixels( pixelPointerXy, bufferOffset, sourceColor, 1 );
                else
                    this.m_Blender.BlendPixel( pixelPointerXy, bufferOffset, new RGBA_Bytes( ( int ) sourceColor.red, ( int ) sourceColor.green, ( int ) sourceColor.blue, a_ ) );
                bufferOffset += this.m_DistanceInBytesBetweenPixelsInclusive;
                ++coversIndex;
            }
            while ( --len != 0 );
        }

        public void blend_solid_vspan( int x, int y, int len, RGBA_Bytes sourceColor, byte[ ] covers, int coversIndex )
        {
            if ( sourceColor.alpha == ( byte ) 0 )
                return;
            int num = this.StrideInBytes();
            int bufferOffsetXy = this.GetBufferOffsetXY(x, y);
            do
            {
                byte alpha = sourceColor.alpha;
                sourceColor.alpha = ( byte ) ( ( int ) sourceColor.alpha * ( ( int ) covers[ coversIndex++ ] + 1 ) >> 8 );
                if ( sourceColor.alpha == byte.MaxValue )
                    this.m_Blender.CopyPixels( this.m_ByteBuffer, bufferOffsetXy, sourceColor, 1 );
                else
                    this.m_Blender.BlendPixel( this.m_ByteBuffer, bufferOffsetXy, sourceColor );
                bufferOffsetXy += num;
                sourceColor.alpha = alpha;
            }
            while ( --len != 0 );
        }

        public void copy_color_hspan( int x, int y, int len, RGBA_Bytes[ ] colors, int colorsIndex )
        {
            int bufferOffsetXy = this.GetBufferOffsetXY(x, y);
            do
            {
                this.m_Blender.CopyPixels( this.m_ByteBuffer, bufferOffsetXy, colors[ colorsIndex ], 1 );
                ++colorsIndex;
                bufferOffsetXy += this.m_DistanceInBytesBetweenPixelsInclusive;
            }
            while ( --len != 0 );
        }

        public void copy_color_vspan( int x, int y, int len, RGBA_Bytes[ ] colors, int colorsIndex )
        {
            int bufferOffsetXy = this.GetBufferOffsetXY(x, y);
            do
            {
                this.m_Blender.CopyPixels( this.m_ByteBuffer, bufferOffsetXy, colors[ colorsIndex ], 1 );
                ++colorsIndex;
                bufferOffsetXy += this.m_StrideInBytes;
            }
            while ( --len != 0 );
        }

        public void blend_color_hspan( int x, int y, int len, RGBA_Bytes[ ] colors, int colorsIndex, byte[ ] covers, int coversIndex, bool firstCoverForAll )
        {
            this.m_Blender.BlendPixels( this.m_ByteBuffer, this.GetBufferOffsetXY( x, y ), colors, colorsIndex, covers, coversIndex, firstCoverForAll, len );
        }

        public void blend_color_vspan( int x, int y, int len, RGBA_Bytes[ ] colors, int colorsIndex, byte[ ] covers, int coversIndex, bool firstCoverForAll )
        {
            int bufferOffsetXy = this.GetBufferOffsetXY(x, y);
            int num = this.StrideInBytesAbs();
            if ( !firstCoverForAll )
            {
                do
                {
                    DoCopyOrBlend.BasedOnAlphaAndCover( this.m_Blender, this.m_ByteBuffer, bufferOffsetXy, colors[ colorsIndex ], ( int ) covers[ coversIndex++ ] );
                    bufferOffsetXy += num;
                    ++colorsIndex;
                }
                while ( --len != 0 );
            }
            else if ( covers[ coversIndex ] == byte.MaxValue )
            {
                do
                {
                    DoCopyOrBlend.BasedOnAlpha( this.m_Blender, this.m_ByteBuffer, bufferOffsetXy, colors[ colorsIndex ] );
                    bufferOffsetXy += num;
                    ++colorsIndex;
                }
                while ( --len != 0 );
            }
            else
            {
                do
                {
                    DoCopyOrBlend.BasedOnAlphaAndCover( this.m_Blender, this.m_ByteBuffer, bufferOffsetXy, colors[ colorsIndex ], ( int ) covers[ coversIndex ] );
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
            RGBA_Bytes colorRgbaBytes = this.GetBlender().PixelToColorRGBA_Bytes(this.m_ByteBuffer, this.GetBufferOffsetXY(x, y));
            if ( colorRgbaBytes.Alpha0To255 == 0 && colorRgbaBytes.Red0To255 == 0 && colorRgbaBytes.Green0To255 == 0 )
                return ( uint ) colorRgbaBytes.Blue0To255 > 0U;
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
                ImageBuffer sourceImage = new ImageBuffer();
                sourceImage.Initialize( this, visibleBounds );
                this.Initialize( sourceImage );
                this.OriginOffset = new Vector2( ( double ) -visibleBounds.Left + originOffset.x, ( double ) -visibleBounds.Bottom + originOffset.y );
            }
            else
                this.Deallocate();
        }

        public static bool operator ==( ImageBuffer a, ImageBuffer b )
        {
            if ( ( object ) a != null && ( object ) b != null )
                return a.Equals( b, 0 );
            return ( object ) a == null && ( object ) b == null;
        }

        public static bool operator !=( ImageBuffer a, ImageBuffer b )
        {
            return !( a == b );
        }

        public override bool Equals( object obj )
        {
            if ( obj.GetType() == typeof( ImageBuffer ) )
                return this == ( ImageBuffer ) obj;
            return false;
        }

        public bool Equals( ImageBuffer b, int maxError = 0 )
        {
            if ( this.Width != b.Width || this.Height != b.Height || ( this.BitDepth != b.BitDepth || this.StrideInBytes() != b.StrideInBytes() ) || !( this.m_OriginOffset == b.m_OriginOffset ) )
                return false;
            int num1 = this.BitDepth / 8;
            int betweenPixelsInclusive1 = this.GetBytesBetweenPixelsInclusive();
            int betweenPixelsInclusive2 = b.GetBytesBetweenPixelsInclusive();
            byte[] buffer1 = this.GetBuffer();
            byte[] buffer2 = b.GetBuffer();
            for ( int y = 0 ; y < this.Height ; ++y )
            {
                int bufferOffsetY1 = this.GetBufferOffsetY(y);
                int bufferOffsetY2 = b.GetBufferOffsetY(y);
                for ( int index1 = 0 ; index1 < this.Width ; ++index1 )
                {
                    for ( int index2 = 0 ; index2 < num1 ; ++index2 )
                    {
                        byte num2 = buffer1[bufferOffsetY1 + index2];
                        byte num3 = buffer2[bufferOffsetY2 + index2];
                        if ( ( int ) num2 < ( int ) num3 - maxError || ( int ) num2 > ( int ) num3 + maxError )
                            return false;
                    }
                    bufferOffsetY1 += betweenPixelsInclusive1;
                    bufferOffsetY2 += betweenPixelsInclusive2;
                }
            }
            return true;
        }

        public bool Contains( ImageBuffer imageToFind, int maxError = 0 )
        {
            int matchX;
            int matchY;
            return this.Contains( imageToFind, out matchX, out matchY, maxError );
        }

        public bool Contains( ImageBuffer imageToFind, out int matchX, out int matchY, int maxError = 0 )
        {
            matchX = 0;
            matchY = 0;
            if ( this.Width >= imageToFind.Width && this.Height >= imageToFind.Height && this.BitDepth == imageToFind.BitDepth )
            {
                int num1 = this.BitDepth / 8;
                int betweenPixelsInclusive1 = this.GetBytesBetweenPixelsInclusive();
                int betweenPixelsInclusive2 = imageToFind.GetBytesBetweenPixelsInclusive();
                byte[] buffer1 = this.GetBuffer();
                byte[] buffer2 = imageToFind.GetBuffer();
                matchY = 0;
                while ( matchY <= this.Height - imageToFind.Height )
                {
                    matchX = 0;
                    while ( matchX <= this.Width - imageToFind.Width )
                    {
                        bool flag = false;
                        for ( int y = 0 ; y < imageToFind.Height ; ++y )
                        {
                            int bufferOffsetXy = this.GetBufferOffsetXY(matchX, matchY + y);
                            int bufferOffsetY = imageToFind.GetBufferOffsetY(y);
                            for ( int index1 = 0 ; index1 < imageToFind.Width ; ++index1 )
                            {
                                for ( int index2 = 0 ; index2 < num1 ; ++index2 )
                                {
                                    byte num2 = buffer1[bufferOffsetXy + index2];
                                    byte num3 = buffer2[bufferOffsetY + index2];
                                    if ( ( int ) num2 < ( int ) num3 - maxError || ( int ) num2 > ( int ) num3 + maxError )
                                    {
                                        flag = true;
                                        index2 = num1;
                                        index1 = imageToFind.Width;
                                        y = imageToFind.Height;
                                    }
                                }
                                bufferOffsetXy += betweenPixelsInclusive1;
                                bufferOffsetY += betweenPixelsInclusive2;
                            }
                        }
                        if ( !flag )
                            return true;
                        ++matchX;
                    }
                    ++matchY;
                }
            }
            return false;
        }

        public double FindLeastSquaresMatch( ImageBuffer imageToFind, out Vector2 bestPosition )
        {
            bestPosition = Vector2.Zero;
            double num1 = double.MaxValue;
            if ( this.Width >= imageToFind.Width && this.Height >= imageToFind.Height && this.BitDepth == imageToFind.BitDepth )
            {
                int num2 = this.BitDepth / 8;
                int betweenPixelsInclusive1 = this.GetBytesBetweenPixelsInclusive();
                int betweenPixelsInclusive2 = imageToFind.GetBytesBetweenPixelsInclusive();
                byte[] buffer1 = this.GetBuffer();
                byte[] buffer2 = imageToFind.GetBuffer();
                for ( int index1 = 0 ; index1 <= this.Height - imageToFind.Height ; ++index1 )
                {
                    for ( int x = 0 ; x <= this.Width - imageToFind.Width ; ++x )
                    {
                        double num3 = 0.0;
                        for ( int y = 0 ; y < imageToFind.Height ; ++y )
                        {
                            int bufferOffsetXy = this.GetBufferOffsetXY(x, index1 + y);
                            int bufferOffsetY = imageToFind.GetBufferOffsetY(y);
                            for ( int index2 = 0 ; index2 < imageToFind.Width ; ++index2 )
                            {
                                for ( int index3 = 0 ; index3 < num2 ; ++index3 )
                                {
                                    int num4 = (int) buffer1[bufferOffsetXy + index3] - (int) buffer2[bufferOffsetY + index3];
                                    num3 += ( double ) ( num4 * num4 );
                                }
                                bufferOffsetXy += betweenPixelsInclusive1;
                                bufferOffsetY += betweenPixelsInclusive2;
                            }
                            if ( num3 > num1 )
                                y = imageToFind.Height;
                        }
                        if ( num3 < num1 )
                        {
                            bestPosition = new Vector2( ( double ) x, ( double ) index1 );
                            num1 = num3;
                        }
                    }
                }
            }
            return num1;
        }

        public override int GetHashCode()
        {
            return this.m_ByteBuffer.GetHashCode() ^ this.m_BufferOffset.GetHashCode() ^ this.m_BufferFirstPixel.GetHashCode();
        }

        public RectangleInt GetBoundingRect()
        {
            RectangleInt rectangleInt = new RectangleInt(0, 0, this.Width, this.Height);
            rectangleInt.Offset( ( int ) this.OriginOffset.x, ( int ) this.OriginOffset.y );
            return rectangleInt;
        }

        private void Initialize( ImageBuffer sourceImage )
        {
            RectangleInt boundingRect = sourceImage.GetBoundingRect();
            this.Initialize( sourceImage, boundingRect );
            this.OriginOffset = sourceImage.OriginOffset;
        }

        private void Initialize( ImageBuffer sourceImage, RectangleInt boundsToCopyFrom )
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
            graphics2D.Clear( ( IColorType ) new RGBA_Bytes( 0, 0, 0, 0 ) );
            int num1 = -boundsToCopyFrom.Left - (int) sourceImage.OriginOffset.x;
            int num2 = -boundsToCopyFrom.Bottom - (int) sourceImage.OriginOffset.y;
            graphics2D.Render( ( IImageByte ) sourceImage, ( double ) num1, ( double ) num2, 0.0, 1.0, 1.0 );
        }

        public void SetPixel32( int p, int p_2, uint p_3 )
        {
            throw new NotImplementedException();
        }

        public uint GetPixel32( double p, double p_2 )
        {
            throw new NotImplementedException();
        }

        public void blend_hline( int x1, int y, int x2, Func<int, int, RGBA_Bytes> sourceColorCb, byte cover )
        {
            int num1 = x2 - x1 + 1;
            int bufferOffset;
            byte[] pixelPointerXy = this.GetPixelPointerXY(x1, y, out bufferOffset);
            int num2 = x1;
            do
            {
                RGBA_Bytes sourceColor = sourceColorCb(num2, y);
                this.m_Blender.BlendPixel( pixelPointerXy, bufferOffset, sourceColor );
                bufferOffset += this.m_DistanceInBytesBetweenPixelsInclusive;
                ++num2;
            }
            while ( --num1 != 0 );
        }

        public void blend_solid_hspan( int x, int y, int len, Func<int, int, RGBA_Bytes> sourceColorCb, byte[ ] covers, int coversIndex )
        {
            int bufferOffset;
            byte[] pixelPointerXy = this.GetPixelPointerXY(x, y, out bufferOffset);
            int num = x;
            do
            {
                RGBA_Bytes sourceColor = sourceColorCb(num, y);
                this.m_Blender.BlendPixel( pixelPointerXy, bufferOffset, sourceColor );
                bufferOffset += this.m_DistanceInBytesBetweenPixelsInclusive;
                ++coversIndex;
                ++num;
            }
            while ( --len != 0 );
        }

        internal class InternalImageGraphics2D : ImageGraphics2D
        {
            internal InternalImageGraphics2D( ImageBuffer owner )
            {
                ScanlineRasterizer rasterizer = new ScanlineRasterizer();
                this.Initialize( ( IImageByte ) new ImageClippingProxy( ( IImageByte ) owner ), rasterizer );
                this.ScanlineCache = ( IScanlineCache ) new ScanlineCachePacked8();
            }
        }
    }
}
