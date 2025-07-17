// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Rendering.HighQualityRasterizer.HqTextureBrush
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Rendering.Common;

namespace StockSharp.Xaml.Charting.Rendering.HighQualityRasterizer
{
    internal class HqTextureBrush : IBrush2D, IPathColor, IDisposable
    {
        private Size _cachedViewportSize = Size.Empty;
        private readonly Brush _brush;
        private byte[] _cachedTexture;

        public HqTextureBrush( Brush brush )
        {
            this.IsTransparent = brush.Opacity == 0.0 || brush is SolidColorBrush && ( ( SolidColorBrush ) brush ).Color.A == ( byte ) 0;
            this._brush = brush;
        }

        public Color Color
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int ColorCode
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool AlphaBlend
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsTransparent
        {
            get; private set;
        }

        public void Dispose()
        {
        }

        public unsafe byte[ ] GetTexture( Size viewportSize )
        {
            if ( this._cachedViewportSize != viewportSize || this._cachedTexture == null )
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Width = viewportSize.Width;
                rectangle.Height = viewportSize.Height;
                Rectangle element = rectangle;
                element.Fill = this._brush;
                element.MeasureArrange();
                WriteableBitmap bitmap = element.RenderToBitmap((int) viewportSize.Width, (int) viewportSize.Height);
                this._cachedTexture = new byte[ bitmap.PixelWidth * bitmap.PixelHeight * 4 ];
                byte[] numArray;
                using ( BitmapContext bitmapContext = bitmap.GetBitmapContext( ReadWriteMode.ReadOnly ) )
                {
                    try
                    {
                        numArray = this._cachedTexture;

                        fixed ( byte* tonyPointer = &numArray[ 0 ] )
                        {
                            byte* dstPtr = numArray == null || numArray.Length == 0 ? (byte*) null : tonyPointer;
                            NativeMethods.CopyUnmanagedMemory( ( byte* ) bitmapContext.Pixels, 0, dstPtr, 0, bitmap.PixelWidth * bitmap.PixelHeight * 4 );
                        }


                    }
                    finally
                    {
                        numArray = ( byte[ ] ) null;
                    }
                }
                this._cachedViewportSize = viewportSize;
            }
            return this._cachedTexture;
        }
    }
}
