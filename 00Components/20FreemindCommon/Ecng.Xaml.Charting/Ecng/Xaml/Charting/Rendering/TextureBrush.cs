// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Rendering.TextureBrush
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace fx.Xaml.Charting
{
    internal class TextureBrush : IBrush2D, IPathColor, IDisposable
    {
        private const int ByteOffset = 4;

        private Size                        _cachedTextureSize = Size.Empty;

        private readonly TextureCache       _textureCache;
        private readonly TextureMappingMode _mappingMode;
        private int[]                       _cachedIntTexture;
        private byte[]                      _cachedByteTexture;
        private readonly Brush              _brush;
        private Color                       _color;

        public TextureBrush( Brush brush, TextureMappingMode mappingMode, TextureCache textureCache )
        {
            if ( textureCache == null )
            {
                throw new ArgumentNullException();
            }

            IsTransparent = brush.IsTransparent();
            _textureCache = textureCache;
            _mappingMode = mappingMode;
            _brush = brush;
            _color = _brush.ExtractColor();
        }

        public Color Color
        {
            get
            {
                return _color;
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
                return true;
            }
        }

        public bool IsTransparent
        {
            get; private set;
        }

        public void Dispose()
        {
        }

        public unsafe int[ ] GetIntTexture( Size viewportSize )
        {
            if ( _cachedIntTexture == null || _mappingMode == TextureMappingMode.PerScreen && _cachedTextureSize != viewportSize )
            {
                _cachedIntTexture = _textureCache.GetIntTexture( viewportSize, _brush );
                if ( _cachedIntTexture == null )
                {
                    Rectangle rectangle    = new Rectangle();
                    rectangle.Width = viewportSize.Width;
                    rectangle.Height = viewportSize.Height;
                    rectangle.Fill = _brush;
                    Rectangle element      = rectangle;

                    element.MeasureArrange();

                    WriteableBitmap bitmap = element.RenderToBitmap((int) viewportSize.Width, (int) viewportSize.Height);
                    _cachedIntTexture = new int[ bitmap.PixelWidth * bitmap.PixelHeight ];

                    int[] numArray;
                    using ( BitmapContext bitmapContext = bitmap.GetBitmapContext( ReadWriteMode.ReadOnly ) )
                    {
                        try
                        {
                            numArray = _cachedIntTexture;

                            fixed ( int* tonyPointer = &numArray[ 0 ] )
                            {
                                int* dstPtr = numArray == null || numArray.Length == 0 ? (int*) null : tonyPointer;
                                NativeMethods.CopyUnmanagedMemory( bitmapContext.Pixels, dstPtr, bitmap.PixelWidth * bitmap.PixelHeight );
                            }
                        }
                        finally
                        {
                            numArray = ( int[ ] ) null;
                        }
                    }
                    _textureCache.AddTexture( viewportSize, _brush, _cachedIntTexture );
                }
                _cachedTextureSize = viewportSize;
            }
            return _cachedIntTexture;
        }

        public unsafe byte[ ] GetByteTexture( Size viewportSize )
        {
            if ( _cachedByteTexture == null || _mappingMode == TextureMappingMode.PerScreen && _cachedTextureSize != viewportSize )
            {
                _cachedByteTexture = _textureCache.GetByteTexture( viewportSize, _brush );
                if ( _cachedByteTexture == null )
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Width = viewportSize.Width;
                    rectangle.Height = viewportSize.Height;
                    rectangle.Fill = _brush;
                    Rectangle element = rectangle;
                    element.MeasureArrange();
                    WriteableBitmap bitmap = element.RenderToBitmap((int) viewportSize.Width, (int) viewportSize.Height);
                    _cachedByteTexture = new byte[ bitmap.PixelWidth * bitmap.PixelHeight * 4 ];
                    byte[] numArray;
                    using ( BitmapContext bitmapContext = bitmap.GetBitmapContext( ReadWriteMode.ReadOnly ) )
                    {
                        try
                        {
                            numArray = _cachedByteTexture;

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
                    int index = 0;
                    while ( index < _cachedByteTexture.Length )
                    {
                        int num = Math.Max((int) _cachedByteTexture[index + 3], 1);
                        _cachedByteTexture[ index + 2 ] = ( byte ) ( ( int ) _cachedByteTexture[ index + 2 ] * ( int ) byte.MaxValue / num );
                        _cachedByteTexture[ index + 1 ] = ( byte ) ( ( int ) _cachedByteTexture[ index + 1 ] * ( int ) byte.MaxValue / num );
                        _cachedByteTexture[ index ] = ( byte ) ( ( int ) _cachedByteTexture[ index ] * ( int ) byte.MaxValue / num );
                        index += 4;
                    }
                    _textureCache.AddTexture( viewportSize, _brush, _cachedByteTexture );
                }
                _cachedTextureSize = viewportSize;
            }
            return _cachedByteTexture;
        }

        public int GetByteOffsetConsideringMappingMode( int screenX, int screenY, Rect primitiveBoundingRect, double gradiendRotationAngle )
        {
            return GetIntOffsetConsideringMappingMode( screenX, screenY, primitiveBoundingRect, gradiendRotationAngle ) * 4;
        }

        public int GetByteOffsetConsideringMappingMode( int screenX, int screenY, double gradiendRotationAngle )
        {
            return GetIntOffsetConsideringMappingMode( screenX, screenY, gradiendRotationAngle ) * 4;
        }

        public int GetByteOffsetNotConsideringMappingMode( int screenX, int screenY, double gradiendRotationAngle )
        {
            return GetIntOffsetNotConsideringMappingMode( screenX, screenY, gradiendRotationAngle ) * 4;
        }

        public int GetIntOffsetConsideringMappingMode( int screenX, int screenY, Rect primitiveBoundingRect, double gradiendRotationAngle )
        {
            if ( _mappingMode == TextureMappingMode.PerScreen )
            {
                return GetIntOffsetNotConsideringMappingMode( screenX, screenY, gradiendRotationAngle );
            }

            int width = (int) _cachedTextureSize.Width;
            int height = (int) _cachedTextureSize.Height;
            int xOnTexture = (int) (((double) screenX - primitiveBoundingRect.Left) / primitiveBoundingRect.Width * (double) width);
            int yOnTexture = (int) (((double) screenY - primitiveBoundingRect.Top) / primitiveBoundingRect.Height * (double) height);
            RotateAroundCenterOfTexture( ref xOnTexture, ref yOnTexture, gradiendRotationAngle );
            return yOnTexture * width + xOnTexture;
        }

        public int GetIntOffsetNotConsideringMappingMode( int screenX, int screenY, double gradiendRotationAngle )
        {
            RotateAroundCenterOfTexture( ref screenX, ref screenY, gradiendRotationAngle );
            return screenY * ( int ) _cachedTextureSize.Width + screenX;
        }

        private void RotateAroundCenterOfTexture( ref int xOnTexture, ref int yOnTexture, double angle )
        {
            int width = (int) _cachedTextureSize.Width;
            int height = (int) _cachedTextureSize.Height;
            if ( angle != 0.0 )
            {
                int num1 = width / 2;
                int num2 = height / 2;
                double num3 = (double) (xOnTexture - num1) / (double) width;
                double num4 = (double) (yOnTexture - num2) / (double) height;
                double num5 = num3 * Math.Cos(angle) - num4 * Math.Sin(angle);
                double num6 = num3 * Math.Sin(angle) + num4 * Math.Cos(angle);
                xOnTexture = num1 + ( int ) ( num5 * ( double ) width );
                yOnTexture = num2 + ( int ) ( num6 * ( double ) width );
            }
            if ( xOnTexture >= width )
            {
                xOnTexture = width - 1;
            }
            else if ( xOnTexture < 0 )
            {
                xOnTexture = 0;
            }

            if ( yOnTexture >= height )
            {
                yOnTexture = height - 1;
            }
            else
            {
                if ( yOnTexture >= 0 )
                {
                    return;
                }

                yOnTexture = 0;
            }
        }

        public int GetIntOffsetConsideringMappingMode( int screenX, int screenY, double gradiendRotationAngle )
        {
            if ( _mappingMode == TextureMappingMode.PerScreen )
            {
                return GetIntOffsetNotConsideringMappingMode( screenX, screenY, gradiendRotationAngle );
            }

            Rect rect = new Rect(new Point(0.0, 0.0), _cachedTextureSize);
            int width = (int) _cachedTextureSize.Width;
            int height = (int) _cachedTextureSize.Height;
            int xOnTexture = (int) (((double) screenX - rect.Left) / rect.Width * (double) width);
            int yOnTexture = (int) (((double) screenY - rect.Top) / rect.Height * (double) height);
            RotateAroundCenterOfTexture( ref xOnTexture, ref yOnTexture, gradiendRotationAngle );
            return yOnTexture * width + xOnTexture;
        }
    }
}
