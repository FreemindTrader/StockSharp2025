// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Rendering.HighQualityRasterizer.HqSprite2D
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows.Media.Imaging;
using MatterHackers.Agg.Image;
using Ecng.Xaml.Charting.Rendering.Common;

namespace Ecng.Xaml.Charting.Rendering.HighQualityRasterizer
{
    internal class HqSprite2D : ISprite2D, IDisposable
    {
        private readonly WriteableBitmap _bmp;
        private ImageBuffer _imageBuffer;

        public unsafe HqSprite2D( WriteableBitmap bmp )
        {
            this._bmp = bmp;
            this._imageBuffer = new ImageBuffer( bmp.PixelWidth, bmp.PixelHeight, 32, ( IBlenderByte ) new BlenderBGRA() );
            using ( BitmapContext bitmapContext = this._bmp.GetBitmapContext( ReadWriteMode.ReadOnly ) )
            {
                fixed ( byte* dstPtr = &this._imageBuffer.GetBuffer()[ 0 ] )
                    NativeMethods.CopyUnmanagedMemory( ( byte* ) bitmapContext.Pixels, 0, dstPtr, 0, bmp.PixelWidth * bmp.PixelHeight * 4 );
            }
            this.Width = ( float ) this._bmp.PixelWidth;
            this.Height = ( float ) this._bmp.PixelHeight;
        }

        public byte[ ] GetBuffer()
        {
            return this._imageBuffer.GetBuffer();
        }

        public int GetBufferOffsetXY( int i, int j )
        {
            return this._imageBuffer.GetBufferOffsetXY( i, j );
        }

        public void Dispose()
        {
        }

        public float Width
        {
            get; private set;
        }

        public float Height
        {
            get; private set;
        }
    }
}
