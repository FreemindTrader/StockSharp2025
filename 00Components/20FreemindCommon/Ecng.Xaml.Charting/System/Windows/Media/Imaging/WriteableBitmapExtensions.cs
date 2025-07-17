//// System.Windows.Media.Imaging.WriteableBitmapExtensions
//using StockSharp.Xaml.Charting.Common.Extensions;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Reflection;
//using System.Windows;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;

//internal static class WriteableBitmapExtensions
//{
//    internal enum BlendMode
//    {
//        Alpha,
//        Additive,
//        Subtractive,
//        Mask,
//        Multiply,
//        ColorKeying,
//        None
//    }

//    internal enum Interpolation
//    {
//        NearestNeighbor,
//        Bilinear
//    }

//    internal enum FlipMode
//    {
//        Vertical,
//        Horizontal
//    }

//    private static readonly int[] leftEdgeX = new int[8192];

//    private static readonly int[] rightEdgeX = new int[8192];

//    internal const int SizeOfArgb = 4;

//    private const int WhiteR = 255;

//    private const int WhiteG = 255;

//    private const int WhiteB = 255;

//    internal static int[,] KernelGaussianBlur5x5 = new int[5, 5]
//    {
//        {
//            1,
//            4,
//            7,
//            4,
//            1
//        },
//        {
//            4,
//            16,
//            26,
//            16,
//            4
//        },
//        {
//            7,
//            26,
//            41,
//            26,
//            7
//        },
//        {
//            4,
//            16,
//            26,
//            16,
//            4
//        },
//        {
//            1,
//            4,
//            7,
//            4,
//            1
//        }
//    };

//    internal static int[,] KernelGaussianBlur3x3 = new int[3, 3]
//    {
//        {
//            16,
//            26,
//            16
//        },
//        {
//            26,
//            41,
//            26
//        },
//        {
//            16,
//            26,
//            16
//        }
//    };

//    internal static int[,] KernelSharpen3x3 = new int[3, 3]
//    {
//        {
//            0,
//            -2,
//            0
//        },
//        {
//            -2,
//            11,
//            -2
//        },
//        {
//            0,
//            -2,
//            0
//        }
//    };

//    private const byte INSIDE = 0;

//    private const byte LEFT = 1;

//    private const byte RIGHT = 2;

//    private const byte BOTTOM = 4;

//    private const byte TOP = 8;

//    private const float StepFactor = 2f;

//    private static void swap<T>( ref T a, ref T b )
//    {
//        T val = a;
//        a = b;
//        b = val;
//    }

//    private unsafe static void AALineQ1( int width, int height, BitmapContext context, int x1, int y1, int x2, int y2, int color, bool minEdge, bool leftEdge )
//    {
//        byte b = 0;
//        if ( minEdge )
//        {
//            b = byte.MaxValue;
//        }
//        if ( x1 != x2 && y1 != y2 )
//        {
//            int* pixels = context.Pixels;
//            if ( y1 > y2 )
//            {
//                swap( ref x1, ref x2 );
//                swap( ref y1, ref y2 );
//            }
//            int num = x2 - x1;
//            int num2 = y2 - y1;
//            if ( x1 > x2 )
//            {
//                num = x1 - x2;
//            }
//            int num3 = x1;
//            int num4 = y1;
//            ushort num5 = 0;
//            num5 = ( ( num <= num2 ) ? ( ( ushort ) ( ( num << 16 ) / num2 ) ) : ( ( ushort ) ( ( num2 << 16 ) / num ) ) );
//            ushort num6 = 0;
//            byte b2 = (byte)((color & 4278190080u) >> 24);
//            byte b3 = (byte)((color & 0xFF0000) >> 16);
//            byte b4 = (byte)((color & 0xFF00) >> 8);
//            byte b5 = (byte)(color & 0xFF);
//            byte b6 = b2;
//            num6 = 0;
//            if ( num >= num2 )
//            {
//                while ( num-- != 0 )
//                {
//                    if ( ( ushort ) ( num6 + num5 ) <= num6 )
//                    {
//                        num4++;
//                    }
//                    num6 = ( ushort ) ( num6 + num5 );
//                    num3 = ( ( x1 >= x2 ) ? ( num3 - 1 ) : ( num3 + 1 ) );
//                    if ( num4 >= 0 && num4 < height )
//                    {
//                        if ( leftEdge )
//                        {
//                            leftEdgeX[ num4 ] = Math.Max( num3 + 1, leftEdgeX[ num4 ] );
//                        }
//                        else
//                        {
//                            rightEdgeX[ num4 ] = Math.Min( num3 - 1, rightEdgeX[ num4 ] );
//                        }
//                        if ( num3 >= 0 && num3 < width )
//                        {
//                            b6 = ( byte ) ( b2 * ( ushort ) ( ( ushort ) ( num6 >> 8 ) ^ b ) >> 8 );
//                            byte b7 = b3;
//                            byte b8 = b4;
//                            byte b9 = b5;
//                            int num8 = pixels[num4 * width + num3];
//                            byte b10 = (byte)((num8 & 0xFF0000) >> 16);
//                            byte b11 = (byte)((num8 & 0xFF00) >> 8);
//                            byte b12 = (byte)(num8 & 0xFF);
//                            b10 = ( byte ) ( b7 * b6 + b10 * ( 255 - b6 ) >> 8 );
//                            b11 = ( byte ) ( b8 * b6 + b11 * ( 255 - b6 ) >> 8 );
//                            b12 = ( byte ) ( b9 * b6 + b12 * ( 255 - b6 ) >> 8 );
//                            pixels[ num4 * width + num3 ] = ( -16777216 | ( b10 << 16 ) | ( b11 << 8 ) | b12 );
//                        }
//                    }
//                }
//            }
//            else
//            {
//                b = ( byte ) ( b ^ 0xFF );
//                while ( --num2 != 0 )
//                {
//                    if ( ( ushort ) ( num6 + num5 ) <= num6 )
//                    {
//                        num3 = ( ( x1 >= x2 ) ? ( num3 - 1 ) : ( num3 + 1 ) );
//                    }
//                    num6 = ( ushort ) ( num6 + num5 );
//                    num4++;
//                    if ( num3 >= 0 && num3 < width && num4 >= 0 && num4 < height )
//                    {
//                        b6 = ( byte ) ( b2 * ( ushort ) ( ( ushort ) ( num6 >> 8 ) ^ b ) >> 8 );
//                        byte b7 = b3;
//                        byte b8 = b4;
//                        byte b9 = b5;
//                        int num8 = pixels[num4 * width + num3];
//                        byte b10 = (byte)((num8 & 0xFF0000) >> 16);
//                        byte b11 = (byte)((num8 & 0xFF00) >> 8);
//                        byte b12 = (byte)(num8 & 0xFF);
//                        b10 = ( byte ) ( b7 * b6 + b10 * ( 255 - b6 ) >> 8 );
//                        b11 = ( byte ) ( b8 * b6 + b11 * ( 255 - b6 ) >> 8 );
//                        b12 = ( byte ) ( b9 * b6 + b12 * ( 255 - b6 ) >> 8 );
//                        pixels[ num4 * width + num3 ] = ( -16777216 | ( b10 << 16 ) | ( b11 << 8 ) | b12 );
//                        if ( leftEdge )
//                        {
//                            leftEdgeX[ num4 ] = num3 + 1;
//                        }
//                        else
//                        {
//                            rightEdgeX[ num4 ] = num3 - 1;
//                        }
//                    }
//                }
//            }
//        }
//    }

//    private unsafe static void AAWidthLine( int width, int height, BitmapContext context, float x1, float y1, float x2, float y2, float lineWidth, int color )
//    {
//        if ( !( lineWidth <= 0f ) )
//        {
//            int* pixels = context.Pixels;
//            if ( y1 > y2 )
//            {
//                swap( ref x1, ref x2 );
//                swap( ref y1, ref y2 );
//            }
//            if ( x1 == x2 )
//            {
//                x1 -= ( float ) ( ( int ) lineWidth / 2 );
//                x2 += ( float ) ( ( int ) lineWidth / 2 );
//                if ( x1 < 0f )
//                {
//                    x1 = 0f;
//                }
//                if ( !( x2 < 0f ) && !( x1 >= ( float ) width ) )
//                {
//                    if ( x2 >= ( float ) width )
//                    {
//                        x2 = ( float ) ( width - 1 );
//                    }
//                    if ( !( y1 >= ( float ) height ) && !( y2 < 0f ) )
//                    {
//                        if ( y1 < 0f )
//                        {
//                            y1 = 0f;
//                        }
//                        if ( y2 >= ( float ) height )
//                        {
//                            y2 = ( float ) ( height - 1 );
//                        }
//                        for ( int i = ( int ) x1; ( float ) i <= x2; i++ )
//                        {
//                            for ( int j = ( int ) y1; ( float ) j <= y2; j++ )
//                            {
//                                byte b = (byte)((color & 4278190080u) >> 24);
//                                byte b2 = (byte)((color & 0xFF0000) >> 16);
//                                byte b3 = (byte)((color & 0xFF00) >> 8);
//                                byte b4 = (byte)(color & 0xFF);
//                                byte b5 = b2;
//                                byte b6 = b3;
//                                byte b7 = b4;
//                                int num = pixels[j * width + i];
//                                byte b8 = (byte)((num & 0xFF0000) >> 16);
//                                byte b9 = (byte)((num & 0xFF00) >> 8);
//                                byte b10 = (byte)(num & 0xFF);
//                                b8 = ( byte ) ( b5 * b + b8 * ( 255 - b ) >> 8 );
//                                b9 = ( byte ) ( b6 * b + b9 * ( 255 - b ) >> 8 );
//                                b10 = ( byte ) ( b7 * b + b10 * ( 255 - b ) >> 8 );
//                                pixels[ j * width + i ] = ( -16777216 | ( b8 << 16 ) | ( b9 << 8 ) | b10 );
//                            }
//                        }
//                    }
//                }
//            }
//            else if ( y1 == y2 )
//            {
//                if ( x1 > x2 )
//                {
//                    swap( ref x1, ref x2 );
//                }
//                y1 -= ( float ) ( ( int ) lineWidth / 2 );
//                y2 += ( float ) ( ( int ) lineWidth / 2 );
//                if ( y1 < 0f )
//                {
//                    y1 = 0f;
//                }
//                if ( !( y2 < 0f ) && !( y1 >= ( float ) height ) )
//                {
//                    if ( y2 >= ( float ) height )
//                    {
//                        x2 = ( float ) ( height - 1 );
//                    }
//                    if ( !( x1 >= ( float ) width ) && !( y2 < 0f ) )
//                    {
//                        if ( x1 < 0f )
//                        {
//                            x1 = 0f;
//                        }
//                        if ( x2 >= ( float ) width )
//                        {
//                            x2 = ( float ) ( width - 1 );
//                        }
//                        for ( int k = ( int ) x1; ( float ) k <= x2; k++ )
//                        {
//                            for ( int l = ( int ) y1; ( float ) l <= y2; l++ )
//                            {
//                                byte b11 = (byte)((color & 4278190080u) >> 24);
//                                byte b12 = (byte)((color & 0xFF0000) >> 16);
//                                byte b13 = (byte)((color & 0xFF00) >> 8);
//                                byte b14 = (byte)(color & 0xFF);
//                                byte b15 = b12;
//                                byte b16 = b13;
//                                byte b17 = b14;
//                                int num2 = pixels[l * width + k];
//                                byte b18 = (byte)((num2 & 0xFF0000) >> 16);
//                                byte b19 = (byte)((num2 & 0xFF00) >> 8);
//                                byte b20 = (byte)(num2 & 0xFF);
//                                b18 = ( byte ) ( b15 * b11 + b18 * ( 255 - b11 ) >> 8 );
//                                b19 = ( byte ) ( b16 * b11 + b19 * ( 255 - b11 ) >> 8 );
//                                b20 = ( byte ) ( b17 * b11 + b20 * ( 255 - b11 ) >> 8 );
//                                pixels[ l * width + k ] = ( -16777216 | ( b18 << 16 ) | ( b19 << 8 ) | b20 );
//                            }
//                        }
//                    }
//                }
//            }
//            else
//            {
//                y1 += 1f;
//                y2 += 1f;
//                float num3 = (y2 - y1) / (x2 - x1);
//                float num4 = (x2 - x1) / (y2 - y1);
//                float num5 = num3;
//                float num6 = x2 - x1;
//                float num7 = y2 - y1;
//                float num8 = (float)((double)(lineWidth * num7) / Math.Sqrt((double)(num6 * num6 + num7 * num7)));
//                float num9 = (float)((double)(lineWidth * num6) / Math.Sqrt((double)(num6 * num6 + num7 * num7)));
//                float num10 = num6 * num7 / (num6 * num6 + num7 * num7);
//                x1 += num8 / 2f;
//                y1 -= num9 / 2f;
//                x2 += num8 / 2f;
//                y2 -= num9 / 2f;
//                float num11 = 0f - num8;
//                float num12 = num9;
//                int num13 = (int)x1;
//                int num14 = (int)y1;
//                int num15 = (int)x2;
//                int num16 = (int)y2;
//                int num17 = (int)(x1 + num11);
//                int num18 = (int)(y1 + num12);
//                int num19 = (int)(x2 + num11);
//                int num20 = (int)(y2 + num12);
//                if ( lineWidth == 2f )
//                {
//                    if ( Math.Abs( num7 ) < Math.Abs( num6 ) )
//                    {
//                        if ( x1 < x2 )
//                        {
//                            num18 = num14 + 2;
//                            num20 = num16 + 2;
//                        }
//                        else
//                        {
//                            num14 = num18 + 2;
//                            num16 = num20 + 2;
//                        }
//                    }
//                    else
//                    {
//                        num13 = num17 + 2;
//                        num15 = num19 + 2;
//                    }
//                }
//                int num21 = Math.Min(Math.Min(num14, num16), Math.Min(num18, num20));
//                int num22 = Math.Max(Math.Max(num14, num16), Math.Max(num18, num20));
//                if ( num21 < 0 )
//                {
//                    num21 = -1;
//                }
//                if ( num22 >= height )
//                {
//                    num22 = height + 1;
//                }
//                for ( int m = num21 + 1; m < num22 - 1; m++ )
//                {
//                    leftEdgeX[ m ] = -65536;
//                    rightEdgeX[ m ] = 32768;
//                }
//                AALineQ1( width, height, context, num13, num14, num15, num16, color, num12 > 0f, false );
//                AALineQ1( width, height, context, num17, num18, num19, num20, color, num12 < 0f, true );
//                if ( lineWidth > 1f )
//                {
//                    AALineQ1( width, height, context, num13, num14, num17, num18, color, true, num12 > 0f );
//                    AALineQ1( width, height, context, num15, num16, num19, num20, color, false, num12 < 0f );
//                }
//                if ( x1 < x2 )
//                {
//                    if ( num16 >= 0 && num16 < height )
//                    {
//                        rightEdgeX[ num16 ] = Math.Min( num15, rightEdgeX[ num16 ] );
//                    }
//                    if ( num18 >= 0 && num18 < height )
//                    {
//                        leftEdgeX[ num18 ] = Math.Max( num17, leftEdgeX[ num18 ] );
//                    }
//                }
//                else
//                {
//                    if ( num14 >= 0 && num14 < height )
//                    {
//                        rightEdgeX[ num14 ] = Math.Min( num13, rightEdgeX[ num14 ] );
//                    }
//                    if ( num20 >= 0 && num20 < height )
//                    {
//                        leftEdgeX[ num20 ] = Math.Max( num19, leftEdgeX[ num20 ] );
//                    }
//                }
//                for ( int n = num21 + 1; n < num22 - 1; n++ )
//                {
//                    leftEdgeX[ n ] = Math.Max( leftEdgeX[ n ], 0 );
//                    rightEdgeX[ n ] = Math.Min( rightEdgeX[ n ], width - 1 );
//                    for ( int num23 = leftEdgeX[ n ]; num23 <= rightEdgeX[ n ]; num23++ )
//                    {
//                        byte b21 = (byte)((color & 4278190080u) >> 24);
//                        byte b22 = (byte)((color & 0xFF0000) >> 16);
//                        byte b23 = (byte)((color & 0xFF00) >> 8);
//                        byte b24 = (byte)(color & 0xFF);
//                        byte b25 = b22;
//                        byte b26 = b23;
//                        byte b27 = b24;
//                        int num24 = pixels[n * width + num23];
//                        byte b28 = (byte)((num24 & 0xFF0000) >> 16);
//                        byte b29 = (byte)((num24 & 0xFF00) >> 8);
//                        byte b30 = (byte)(num24 & 0xFF);
//                        b28 = ( byte ) ( b25 * b21 + b28 * ( 255 - b21 ) >> 8 );
//                        b29 = ( byte ) ( b26 * b21 + b29 * ( 255 - b21 ) >> 8 );
//                        b30 = ( byte ) ( b27 * b21 + b30 * ( 255 - b21 ) >> 8 );
//                        pixels[ n * width + num23 ] = ( -16777216 | ( b28 << 16 ) | ( b29 << 8 ) | b30 );
//                    }
//                }
//            }
//        }
//    }

//    internal static void DrawLineAA( BitmapContext context, int pixelWidth, int pixelHeight, int x1, int y1, int x2, int y2, int color, int strokeThickness )
//    {
//        AAWidthLine( pixelWidth, pixelHeight, context, ( float ) x1, ( float ) y1, ( float ) x2, ( float ) y2, ( float ) strokeThickness, color );
//    }

//    internal static void DrawLineAA( BitmapContext context, int pixelWidth, int pixelHeight, int x1, int y1, int x2, int y2, Color color, int strokeThickness )
//    {
//        int num = color.A + 1;
//        int color2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        AAWidthLine( pixelWidth, pixelHeight, context, ( float ) x1, ( float ) y1, ( float ) x2, ( float ) y2, ( float ) strokeThickness, color2 );
//    }

//    internal static void DrawLineAA( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Color color, int strokeThickness )
//    {
//        int num = color.A + 1;
//        int color2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        using ( BitmapContext context = bmp.GetBitmapContext() )
//        {
//            AAWidthLine( bmp.PixelWidth, bmp.PixelHeight, context, ( float ) x1, ( float ) y1, ( float ) x2, ( float ) y2, ( float ) strokeThickness, color2 );
//        }
//    }

//    internal static int ConvertColor( double opacity, Color color )
//    {
//        if ( opacity < 0.0 || opacity > 1.0 )
//        {
//            throw new ArgumentOutOfRangeException( "opacity", "Opacity must be between 0.0 and 1.0" );
//        }
//        color.A = ( byte ) ( ( double ) ( int ) color.A * opacity );
//        return ConvertColor( color );
//    }

//    internal static int ConvertColor( Color color )
//    {
//        if ( color.A == 0 )
//        {
//            return 0;
//        }
//        int num = color.A + 1;
//        return ( color.A << 24 ) | ( ( byte ) ( color.R * num >> 8 ) << 16 ) | ( ( byte ) ( color.G * num >> 8 ) << 8 ) | ( byte ) ( color.B * num >> 8 );
//    }

//    internal unsafe static void Clear( this WriteableBitmap bmp, Color color )
//    {
//        int num = color.A + 1;
//        int num2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            int* pixels = bitmapContext.Pixels;
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            int num3 = pixelWidth * 4;
//            for ( int i = 0; i < pixelWidth; i++ )
//            {
//                pixels[ i ] = num2;
//            }
//            int num4 = 1;
//            int num5 = 1;
//            while ( num5 < pixelHeight )
//            {
//                BitmapContext.BlockCopy( bitmapContext, 0, bitmapContext, num5 * num3, num4 * num3 );
//                num5 += num4;
//                num4 = Math.Min( 2 * num4, pixelHeight - num5 );
//            }
//        }
//    }

//    internal static void Clear( this WriteableBitmap bmp )
//    {
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            bitmapContext.Clear();
//        }
//    }

//    internal static WriteableBitmap Clone( this WriteableBitmap bmp )
//    {
//        WriteableBitmap writeableBitmap = BitmapFactory.New(bmp.PixelWidth, bmp.PixelHeight);
//        using ( BitmapContext src = bmp.GetBitmapContext( ReadWriteMode.ReadOnly ) )
//        {
//            using ( BitmapContext dest = writeableBitmap.GetBitmapContext() )
//            {
//                BitmapContext.BlockCopy( src, 0, dest, 0, src.Length * 4 );
//                return writeableBitmap;
//            }
//        }
//    }

//    internal unsafe static void ForEach( this WriteableBitmap bmp, Func<int, int, Color> func )
//    {
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            int* pixels = bitmapContext.Pixels;
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            int num = 0;
//            for ( int i = 0; i < pixelHeight; i++ )
//            {
//                for ( int j = 0; j < pixelWidth; j++ )
//                {
//                    Color color = func(j, i);
//                    int num2 = color.A + 1;
//                    pixels[ num++ ] = ( ( color.A << 24 ) | ( ( byte ) ( color.R * num2 >> 8 ) << 16 ) | ( ( byte ) ( color.G * num2 >> 8 ) << 8 ) | ( byte ) ( color.B * num2 >> 8 ) );
//                }
//            }
//        }
//    }

//    internal unsafe static void ForEach( this WriteableBitmap bmp, Func<int, int, Color, Color> func )
//    {
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            int* pixels = bitmapContext.Pixels;
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            int num = 0;
//            for ( int i = 0; i < pixelHeight; i++ )
//            {
//                for ( int j = 0; j < pixelWidth; j++ )
//                {
//                    int num2 = pixels[num];
//                    Color color = func(j, i, Color.FromArgb((byte)(num2 >> 24), (byte)(num2 >> 16), (byte)(num2 >> 8), (byte)num2));
//                    int num3 = color.A + 1;
//                    pixels[ num++ ] = ( ( color.A << 24 ) | ( ( byte ) ( color.R * num3 >> 8 ) << 16 ) | ( ( byte ) ( color.G * num3 >> 8 ) << 8 ) | ( byte ) ( color.B * num3 >> 8 ) );
//                }
//            }
//        }
//    }

//    internal unsafe static int GetPixeli( this WriteableBitmap bmp, int x, int y )
//    {
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            return bitmapContext.Pixels[ y * bmp.PixelWidth + x ];
//        }
//    }

//    internal unsafe static Color GetPixel( this WriteableBitmap bmp, int x, int y )
//    {
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            int num = bitmapContext.Pixels[y * bmp.PixelWidth + x];
//            byte b = (byte)(num >> 24);
//            int num2 = b;
//            if ( num2 == 0 )
//            {
//                num2 = 1;
//            }
//            num2 = 65280 / num2;
//            return Color.FromArgb( b, ( byte ) ( ( ( num >> 16 ) & 0xFF ) * num2 >> 8 ), ( byte ) ( ( ( num >> 8 ) & 0xFF ) * num2 >> 8 ), ( byte ) ( ( num & 0xFF ) * num2 >> 8 ) );
//        }
//    }

//    internal unsafe static byte GetBrightness( this WriteableBitmap bmp, int x, int y )
//    {
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext( ReadWriteMode.ReadOnly ) )
//        {
//            int num = bitmapContext.Pixels[y * bmp.PixelWidth + x];
//            byte b = (byte)(num >> 16);
//            byte b2 = (byte)(num >> 8);
//            byte b3 = (byte)num;
//            return ( byte ) ( b * 6966 + b2 * 23436 + b3 * 2366 >> 15 );
//        }
//    }

//    internal unsafe static void SetPixeli( this WriteableBitmap bmp, int index, byte r, byte g, byte b )
//    {
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            bitmapContext.Pixels[ index ] = ( -16777216 | ( r << 16 ) | ( g << 8 ) | b );
//        }
//    }

//    internal unsafe static void SetPixel( this WriteableBitmap bmp, int x, int y, byte r, byte g, byte b )
//    {
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            bitmapContext.Pixels[ y * bmp.PixelWidth + x ] = ( -16777216 | ( r << 16 ) | ( g << 8 ) | b );
//        }
//    }

//    internal unsafe static void SetPixeli( this WriteableBitmap bmp, int index, byte a, byte r, byte g, byte b )
//    {
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            bitmapContext.Pixels[ index ] = ( ( a << 24 ) | ( r << 16 ) | ( g << 8 ) | b );
//        }
//    }

//    internal unsafe static void SetPixel( this WriteableBitmap bmp, int x, int y, byte a, byte r, byte g, byte b )
//    {
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            bitmapContext.Pixels[ y * bmp.PixelWidth + x ] = ( ( a << 24 ) | ( r << 16 ) | ( g << 8 ) | b );
//        }
//    }

//    internal unsafe static void SetPixeli( this WriteableBitmap bmp, int index, Color color )
//    {
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            int num = color.A + 1;
//            bitmapContext.Pixels[ index ] = ( ( color.A << 24 ) | ( ( byte ) ( color.R * num >> 8 ) << 16 ) | ( ( byte ) ( color.G * num >> 8 ) << 8 ) | ( byte ) ( color.B * num >> 8 ) );
//        }
//    }

//    internal unsafe static void SetPixel( this WriteableBitmap bmp, int x, int y, Color color )
//    {
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            int num = color.A + 1;
//            bitmapContext.Pixels[ y * bmp.PixelWidth + x ] = ( ( color.A << 24 ) | ( ( byte ) ( color.R * num >> 8 ) << 16 ) | ( ( byte ) ( color.G * num >> 8 ) << 8 ) | ( byte ) ( color.B * num >> 8 ) );
//        }
//    }

//    internal unsafe static void SetPixeli( this WriteableBitmap bmp, int index, byte a, Color color )
//    {
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            int num = a + 1;
//            bitmapContext.Pixels[ index ] = ( ( a << 24 ) | ( ( byte ) ( color.R * num >> 8 ) << 16 ) | ( ( byte ) ( color.G * num >> 8 ) << 8 ) | ( byte ) ( color.B * num >> 8 ) );
//        }
//    }

//    internal unsafe static void SetPixel( this WriteableBitmap bmp, int x, int y, byte a, Color color )
//    {
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            int num = a + 1;
//            bitmapContext.Pixels[ y * bmp.PixelWidth + x ] = ( ( a << 24 ) | ( ( byte ) ( color.R * num >> 8 ) << 16 ) | ( ( byte ) ( color.G * num >> 8 ) << 8 ) | ( byte ) ( color.B * num >> 8 ) );
//        }
//    }

//    internal unsafe static void SetPixeli( this WriteableBitmap bmp, int index, int color )
//    {
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            bitmapContext.Pixels[ index ] = color;
//        }
//    }

//    internal unsafe static void SetPixel( this WriteableBitmap bmp, int x, int y, int color )
//    {
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            bitmapContext.Pixels[ y * bmp.PixelWidth + x ] = color;
//        }
//    }

//    internal unsafe static void DrawPixelsVertically( this WriteableBitmap bmp, int x, int yStartBottom, int yEndTop, IList<int> pixelColorsArgb, double opacity, bool yAxisIsFlipped )
//    {
//        int num = Math.Max(yStartBottom, yEndTop);
//        yEndTop = Math.Min( yStartBottom, yEndTop );
//        yStartBottom = num;
//        int pixelWidth = bmp.PixelWidth;
//        int pixelHeight = bmp.PixelHeight;
//        if ( yStartBottom != yEndTop )
//        {
//            int num2 = (int)(opacity * 256.0);
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                int* pixels = bitmapContext.Pixels;
//                int num3 = Math.Min(yStartBottom, pixelHeight);
//                int num4 = x + num3 * pixelWidth;
//                int num5 = num3;
//                while ( num5 >= yEndTop && num5 >= 0 )
//                {
//                    if ( num5 >= 0 && num5 < pixelHeight )
//                    {
//                        int num6 = (yStartBottom - num5) * pixelColorsArgb.Count / (yStartBottom - yEndTop);
//                        if ( yAxisIsFlipped )
//                        {
//                            num6 = pixelColorsArgb.Count - 1 - num6;
//                        }
//                        if ( num6 >= 0 && num6 < pixelColorsArgb.Count )
//                        {
//                            int num7 = pixelColorsArgb[num6];
//                            int num8 = (int)((double)((num7 >> 24) & 0xFF) * opacity);
//                            if ( num8 == 255 )
//                            {
//                                pixels[ num4 ] = num7;
//                            }
//                            else if ( num8 > 0 )
//                            {
//                                int num9 = bitmapContext.Pixels[num4];
//                                int num10 = (num9 >> 24) & 0xFF;
//                                int num11 = (num9 >> 16) & 0xFF;
//                                int num12 = (num9 >> 8) & 0xFF;
//                                int num13 = num9 & 0xFF;
//                                int num14 = (num7 >> 16) & 0xFF;
//                                int num15 = (num7 >> 8) & 0xFF;
//                                int num16 = num7 & 0xFF;
//                                int num17 = num14 * num8 / 255 + num11 * num10 * (255 - num8) / 65025;
//                                int num18 = num15 * num8 / 255 + num12 * num10 * (255 - num8) / 65025;
//                                int num19 = num16 * num8 / 255 + num13 * num10 * (255 - num8) / 65025;
//                                int num20 = num8 + num10 * (255 - num8) / 255;
//                                bitmapContext.Pixels[ num4 ] = ( num20 << 24 ) + ( num17 << 16 ) + ( num18 << 8 ) + num19;
//                            }
//                        }
//                    }
//                    num5--;
//                    num4 -= pixelWidth;
//                }
//            }
//        }
//    }

//    internal static void Blit( this WriteableBitmap bmp, Rect destRect, WriteableBitmap source, Rect sourceRect, BlendMode BlendMode )
//    {
//        bmp.Blit( destRect, source, sourceRect, Colors.White, BlendMode );
//    }

//    internal static void Blit( this WriteableBitmap bmp, Rect destRect, WriteableBitmap source, Rect sourceRect )
//    {
//        bmp.Blit( destRect, source, sourceRect, Colors.White, BlendMode.Alpha );
//    }

//    internal static void Blit( this WriteableBitmap bmp, Point destPosition, WriteableBitmap source, Rect sourceRect, Color color, BlendMode BlendMode )
//    {
//        Rect destRect = new Rect(destPosition, new Size(sourceRect.Width, sourceRect.Height));
//        bmp.Blit( destRect, source, sourceRect, color, BlendMode );
//    }

//    internal unsafe static void Blit( this WriteableBitmap bmp, Rect destRect, WriteableBitmap source, Rect sourceRect, Color color, BlendMode BlendMode )
//    {
//        if ( color.A != 0 )
//        {
//            int num = (int)destRect.Width;
//            int num2 = (int)destRect.Height;
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            Rect rect = new Rect(0.0, 0.0, (double)pixelWidth, (double)pixelHeight);
//            rect.Intersect( destRect );
//            if ( !rect.IsEmpty )
//            {
//                int pixelWidth2 = source.PixelWidth;
//                using ( BitmapContext src = source.GetBitmapContext() )
//                {
//                    using ( BitmapContext dest = bmp.GetBitmapContext() )
//                    {
//                        int* pixels = src.Pixels;
//                        int* pixels2 = dest.Pixels;
//                        int length = src.Length;
//                        int length2 = dest.Length;
//                        int num3 = -1;
//                        int num4 = (int)destRect.X;
//                        int num5 = (int)destRect.Y;
//                        int num6 = num4 + num;
//                        int num7 = num5 + num2;
//                        int num8 = 0;
//                        int num9 = 0;
//                        int num10 = 0;
//                        int num11 = 0;
//                        int a = color.A;
//                        int r = color.R;
//                        int g = color.G;
//                        int b = color.B;
//                        bool flag = color != Colors.White;
//                        int num12 = (int)sourceRect.Width;
//                        double num13 = sourceRect.Width / destRect.Width;
//                        double num14 = sourceRect.Height / destRect.Height;
//                        int num15 = (int)sourceRect.X;
//                        int num16 = (int)sourceRect.Y;
//                        int num17 = -1;
//                        int num18 = -1;
//                        double num19 = (double)num16;
//                        int num20 = num5;
//                        for ( int i = 0; i < num2; i++ )
//                        {
//                            if ( num20 >= 0 && num20 < pixelHeight )
//                            {
//                                double num21 = (double)num15;
//                                int num22 = num4 + num20 * pixelWidth;
//                                int num23 = num4;
//                                int num24 = *pixels;
//                                if ( BlendMode == BlendMode.None && !flag )
//                                {
//                                    num3 = ( int ) num21 + ( int ) num19 * pixelWidth2;
//                                    int num25 = (num23 < 0) ? (-num23) : 0;
//                                    int num26 = num23 + num25;
//                                    int num27 = pixelWidth2 - num25;
//                                    int num28 = (num26 + num27 < pixelWidth) ? num27 : (pixelWidth - num26);
//                                    if ( num28 > num12 )
//                                    {
//                                        num28 = num12;
//                                    }
//                                    if ( num28 > num )
//                                    {
//                                        num28 = num;
//                                    }
//                                    BitmapContext.BlockCopy( src, ( num3 + num25 ) * 4, dest, ( num22 + num25 ) * 4, num28 * 4 );
//                                }
//                                else
//                                {
//                                    for ( int j = 0; j < num; j++ )
//                                    {
//                                        if ( num23 >= 0 && num23 < pixelWidth )
//                                        {
//                                            if ( ( int ) num21 != num17 || ( int ) num19 != num18 )
//                                            {
//                                                num3 = ( int ) num21 + ( int ) num19 * pixelWidth2;
//                                                if ( num3 >= 0 && num3 < length )
//                                                {
//                                                    num24 = pixels[ num3 ];
//                                                    num11 = ( ( num24 >> 24 ) & 0xFF );
//                                                    num8 = ( ( num24 >> 16 ) & 0xFF );
//                                                    num9 = ( ( num24 >> 8 ) & 0xFF );
//                                                    num10 = ( num24 & 0xFF );
//                                                    if ( flag && num11 != 0 )
//                                                    {
//                                                        num11 = num11 * a * 32897 >> 23;
//                                                        num8 = ( num8 * r * 32897 >> 23 ) * a * 32897 >> 23;
//                                                        num9 = ( num9 * g * 32897 >> 23 ) * a * 32897 >> 23;
//                                                        num10 = ( num10 * b * 32897 >> 23 ) * a * 32897 >> 23;
//                                                        num24 = ( ( num11 << 24 ) | ( num8 << 16 ) | ( num9 << 8 ) | num10 );
//                                                    }
//                                                }
//                                                else
//                                                {
//                                                    num11 = 0;
//                                                }
//                                            }
//                                            switch ( BlendMode )
//                                            {
//                                                case BlendMode.None:
//                                                    pixels2[ num22 ] = num24;
//                                                    break;
//                                                case BlendMode.ColorKeying:
//                                                    num8 = ( ( num24 >> 16 ) & 0xFF );
//                                                    num9 = ( ( num24 >> 8 ) & 0xFF );
//                                                    num10 = ( num24 & 0xFF );
//                                                    if ( num8 != color.R || num9 != color.G || num10 != color.B )
//                                                    {
//                                                        pixels2[ num22 ] = num24;
//                                                    }
//                                                    break;
//                                                case BlendMode.Mask:
//                                                    {
//                                                        int num44 = pixels2[num22];
//                                                        int num30 = (num44 >> 24) & 0xFF;
//                                                        int num31 = (num44 >> 16) & 0xFF;
//                                                        int num32 = (num44 >> 8) & 0xFF;
//                                                        int num33 = num44 & 0xFF;
//                                                        num44 = ( pixels2[ num22 ] = ( ( num30 * num11 * 32897 >> 23 << 24 ) | ( num31 * num11 * 32897 >> 23 << 16 ) | ( num32 * num11 * 32897 >> 23 << 8 ) | ( num33 * num11 * 32897 >> 23 ) ) );
//                                                        break;
//                                                    }
//                                                default:
//                                                    if ( num11 > 0 )
//                                                    {
//                                                        int num29 = pixels2[num22];
//                                                        int num30 = (num29 >> 24) & 0xFF;
//                                                        if ( ( num11 == 255 || num30 == 0 ) && BlendMode != BlendMode.Additive && BlendMode != BlendMode.Subtractive && BlendMode != BlendMode.Multiply )
//                                                        {
//                                                            pixels2[ num22 ] = num24;
//                                                        }
//                                                        else
//                                                        {
//                                                            int num31 = (num29 >> 16) & 0xFF;
//                                                            int num32 = (num29 >> 8) & 0xFF;
//                                                            int num33 = num29 & 0xFF;
//                                                            switch ( BlendMode )
//                                                            {
//                                                                case BlendMode.Alpha:
//                                                                    num29 = ( ( num11 + ( num30 * ( 255 - num11 ) * 32897 >> 23 ) << 24 ) | ( num8 + ( num31 * ( 255 - num11 ) * 32897 >> 23 ) << 16 ) | ( num9 + ( num32 * ( 255 - num11 ) * 32897 >> 23 ) << 8 ) | ( num10 + ( num33 * ( 255 - num11 ) * 32897 >> 23 ) ) );
//                                                                    break;
//                                                                case BlendMode.Additive:
//                                                                    {
//                                                                        int num43 = (255 <= num11 + num30) ? 255 : (num11 + num30);
//                                                                        num29 = ( ( num43 << 24 ) | ( ( ( num43 <= num8 + num31 ) ? num43 : ( num8 + num31 ) ) << 16 ) | ( ( ( num43 <= num9 + num32 ) ? num43 : ( num9 + num32 ) ) << 8 ) | ( ( num43 <= num10 + num33 ) ? num43 : ( num10 + num33 ) ) );
//                                                                        break;
//                                                                    }
//                                                                case BlendMode.Subtractive:
//                                                                    {
//                                                                        int num42 = num30;
//                                                                        num29 = ( ( num42 << 24 ) | ( ( ( num8 < num31 ) ? ( num8 - num31 ) : 0 ) << 16 ) | ( ( ( num9 < num32 ) ? ( num9 - num32 ) : 0 ) << 8 ) | ( ( num10 < num33 ) ? ( num10 - num33 ) : 0 ) );
//                                                                        break;
//                                                                    }
//                                                                case BlendMode.Multiply:
//                                                                    {
//                                                                        int num34 = num11 * num30 + 128;
//                                                                        int num35 = num8 * num31 + 128;
//                                                                        int num36 = num9 * num32 + 128;
//                                                                        int num37 = num10 * num33 + 128;
//                                                                        int num38 = (num34 >> 8) + num34 >> 8;
//                                                                        int num39 = (num35 >> 8) + num35 >> 8;
//                                                                        int num40 = (num36 >> 8) + num36 >> 8;
//                                                                        int num41 = (num37 >> 8) + num37 >> 8;
//                                                                        num29 = ( ( num38 << 24 ) | ( ( ( num38 <= num39 ) ? num38 : num39 ) << 16 ) | ( ( ( num38 <= num40 ) ? num38 : num40 ) << 8 ) | ( ( num38 <= num41 ) ? num38 : num41 ) );
//                                                                        break;
//                                                                    }
//                                                            }
//                                                            pixels2[ num22 ] = num29;
//                                                        }
//                                                    }
//                                                    break;
//                                            }
//                                        }
//                                        num23++;
//                                        num22++;
//                                        num21 += num13;
//                                    }
//                                }
//                            }
//                            num19 += num14;
//                            num20++;
//                        }
//                    }
//                }
//            }
//        }
//    }

//    internal unsafe static void Blit( BitmapContext destContext, int dpw, int dph, Rect destRect, BitmapContext srcContext, Rect sourceRect, int sourceWidth )
//    {
//        BlendMode blendMode = BlendMode.Alpha;
//        int num = (int)destRect.Width;
//        int num2 = (int)destRect.Height;
//        Rect rect = new Rect(0.0, 0.0, (double)dpw, (double)dph);
//        rect.Intersect( destRect );
//        if ( !rect.IsEmpty )
//        {
//            int* pixels = srcContext.Pixels;
//            int* pixels2 = destContext.Pixels;
//            int length = srcContext.Length;
//            int num3 = -1;
//            int num4 = (int)destRect.X;
//            int num5 = (int)destRect.Y;
//            int num6 = 0;
//            int num7 = 0;
//            int num8 = 0;
//            int num9 = 0;
//            int num10 = (int)sourceRect.Width;
//            double num11 = sourceRect.Width / destRect.Width;
//            double num12 = sourceRect.Height / destRect.Height;
//            int num13 = (int)sourceRect.X;
//            int num14 = (int)sourceRect.Y;
//            int num15 = -1;
//            int num16 = -1;
//            double num17 = (double)num14;
//            int num18 = num5;
//            for ( int i = 0; i < num2; i++ )
//            {
//                if ( num18 >= 0 && num18 < dph )
//                {
//                    double num19 = (double)num13;
//                    int num20 = num4 + num18 * dpw;
//                    int num21 = num4;
//                    int num22 = *pixels;
//                    if ( blendMode == BlendMode.None )
//                    {
//                        num3 = ( int ) num19 + ( int ) num17 * sourceWidth;
//                        int num23 = (num21 < 0) ? (-num21) : 0;
//                        int num24 = num21 + num23;
//                        int num25 = sourceWidth - num23;
//                        int num26 = (num24 + num25 < dpw) ? num25 : (dpw - num24);
//                        if ( num26 > num10 )
//                        {
//                            num26 = num10;
//                        }
//                        if ( num26 > num )
//                        {
//                            num26 = num;
//                        }
//                        BitmapContext.BlockCopy( srcContext, ( num3 + num23 ) * 4, destContext, ( num20 + num23 ) * 4, num26 * 4 );
//                    }
//                    else
//                    {
//                        for ( int j = 0; j < num; j++ )
//                        {
//                            if ( num21 >= 0 && num21 < dpw )
//                            {
//                                if ( ( int ) num19 != num15 || ( int ) num17 != num16 )
//                                {
//                                    num3 = ( int ) num19 + ( int ) num17 * sourceWidth;
//                                    if ( num3 >= 0 && num3 < length )
//                                    {
//                                        num22 = pixels[ num3 ];
//                                        num9 = ( ( num22 >> 24 ) & 0xFF );
//                                        num6 = ( ( num22 >> 16 ) & 0xFF );
//                                        num7 = ( ( num22 >> 8 ) & 0xFF );
//                                        num8 = ( num22 & 0xFF );
//                                    }
//                                    else
//                                    {
//                                        num9 = 0;
//                                    }
//                                }
//                                switch ( blendMode )
//                                {
//                                    case BlendMode.None:
//                                        pixels2[ num20 ] = num22;
//                                        break;
//                                    case BlendMode.ColorKeying:
//                                        num6 = ( ( num22 >> 16 ) & 0xFF );
//                                        num7 = ( ( num22 >> 8 ) & 0xFF );
//                                        num8 = ( num22 & 0xFF );
//                                        if ( num6 != 255 || num7 != 255 || num8 != 255 )
//                                        {
//                                            pixels2[ num20 ] = num22;
//                                        }
//                                        break;
//                                    case BlendMode.Mask:
//                                        {
//                                            int num42 = pixels2[num20];
//                                            int num28 = (num42 >> 24) & 0xFF;
//                                            int num29 = (num42 >> 16) & 0xFF;
//                                            int num30 = (num42 >> 8) & 0xFF;
//                                            int num31 = num42 & 0xFF;
//                                            num42 = ( pixels2[ num20 ] = ( ( num28 * num9 * 32897 >> 23 << 24 ) | ( num29 * num9 * 32897 >> 23 << 16 ) | ( num30 * num9 * 32897 >> 23 << 8 ) | ( num31 * num9 * 32897 >> 23 ) ) );
//                                            break;
//                                        }
//                                    default:
//                                        if ( num9 > 0 )
//                                        {
//                                            int num27 = pixels2[num20];
//                                            int num28 = (num27 >> 24) & 0xFF;
//                                            if ( ( num9 == 255 || num28 == 0 ) && blendMode != BlendMode.Additive && blendMode != BlendMode.Subtractive && blendMode != BlendMode.Multiply )
//                                            {
//                                                pixels2[ num20 ] = num22;
//                                            }
//                                            else
//                                            {
//                                                int num29 = (num27 >> 16) & 0xFF;
//                                                int num30 = (num27 >> 8) & 0xFF;
//                                                int num31 = num27 & 0xFF;
//                                                switch ( blendMode )
//                                                {
//                                                    case BlendMode.Alpha:
//                                                        num27 = ( ( num9 + ( num28 * ( 255 - num9 ) * 32897 >> 23 ) << 24 ) | ( num6 + ( num29 * ( 255 - num9 ) * 32897 >> 23 ) << 16 ) | ( num7 + ( num30 * ( 255 - num9 ) * 32897 >> 23 ) << 8 ) | ( num8 + ( num31 * ( 255 - num9 ) * 32897 >> 23 ) ) );
//                                                        break;
//                                                    case BlendMode.Additive:
//                                                        {
//                                                            int num41 = (255 <= num9 + num28) ? 255 : (num9 + num28);
//                                                            num27 = ( ( num41 << 24 ) | ( ( ( num41 <= num6 + num29 ) ? num41 : ( num6 + num29 ) ) << 16 ) | ( ( ( num41 <= num7 + num30 ) ? num41 : ( num7 + num30 ) ) << 8 ) | ( ( num41 <= num8 + num31 ) ? num41 : ( num8 + num31 ) ) );
//                                                            break;
//                                                        }
//                                                    case BlendMode.Subtractive:
//                                                        {
//                                                            int num40 = num28;
//                                                            num27 = ( ( num40 << 24 ) | ( ( ( num6 < num29 ) ? ( num6 - num29 ) : 0 ) << 16 ) | ( ( ( num7 < num30 ) ? ( num7 - num30 ) : 0 ) << 8 ) | ( ( num8 < num31 ) ? ( num8 - num31 ) : 0 ) );
//                                                            break;
//                                                        }
//                                                    case BlendMode.Multiply:
//                                                        {
//                                                            int num32 = num9 * num28 + 128;
//                                                            int num33 = num6 * num29 + 128;
//                                                            int num34 = num7 * num30 + 128;
//                                                            int num35 = num8 * num31 + 128;
//                                                            int num36 = (num32 >> 8) + num32 >> 8;
//                                                            int num37 = (num33 >> 8) + num33 >> 8;
//                                                            int num38 = (num34 >> 8) + num34 >> 8;
//                                                            int num39 = (num35 >> 8) + num35 >> 8;
//                                                            num27 = ( ( num36 << 24 ) | ( ( ( num36 <= num37 ) ? num36 : num37 ) << 16 ) | ( ( ( num36 <= num38 ) ? num36 : num38 ) << 8 ) | ( ( num36 <= num39 ) ? num36 : num39 ) );
//                                                            break;
//                                                        }
//                                                }
//                                                pixels2[ num20 ] = num27;
//                                            }
//                                        }
//                                        break;
//                                }
//                            }
//                            num21++;
//                            num20++;
//                            num19 += num11;
//                        }
//                    }
//                }
//                num17 += num12;
//                num18++;
//            }
//        }
//    }

//    internal static byte[ ] ToByteArray( this WriteableBitmap bmp, int offset, int count )
//    {
//        using ( BitmapContext src = bmp.GetBitmapContext() )
//        {
//            if ( count == -1 )
//            {
//                count = src.Length;
//            }
//            int num = count * 4;
//            byte[] array = new byte[num];
//            BitmapContext.BlockCopy( src, offset, array, 0, num );
//            return array;
//        }
//    }

//    internal static byte[ ] ToByteArray( this WriteableBitmap bmp, int count )
//    {
//        return bmp.ToByteArray( 0, count );
//    }

//    internal static byte[ ] ToByteArray( this WriteableBitmap bmp )
//    {
//        return bmp.ToByteArray( 0, -1 );
//    }

//    internal static WriteableBitmap FromByteArray( this WriteableBitmap bmp, byte[ ] buffer, int offset, int count )
//    {
//        using ( BitmapContext dest = bmp.GetBitmapContext() )
//        {
//            BitmapContext.BlockCopy( buffer, offset, dest, 0, count );
//            return bmp;
//        }
//    }

//    internal static WriteableBitmap FromByteArray( this WriteableBitmap bmp, byte[ ] buffer, int count )
//    {
//        return bmp.FromByteArray( buffer, 0, count );
//    }

//    internal static WriteableBitmap FromByteArray( this WriteableBitmap bmp, byte[ ] buffer )
//    {
//        return bmp.FromByteArray( buffer, 0, buffer.Length );
//    }

//    internal unsafe static void WriteTga( this WriteableBitmap bmp, Stream destination )
//    {
//        int pixelWidth = bmp.PixelWidth;
//        int pixelHeight = bmp.PixelHeight;
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            int* pixels = bitmapContext.Pixels;
//            byte[] array = new byte[bitmapContext.Length * 4];
//            int num = 0;
//            int num2 = pixelWidth << 2;
//            int num3 = pixelWidth << 3;
//            int num4 = (pixelHeight - 1) * num2;
//            for ( int i = 0; i < pixelHeight; i++ )
//            {
//                for ( int j = 0; j < pixelWidth; j++ )
//                {
//                    int num5 = pixels[num];
//                    array[ num4 ] = ( byte ) ( num5 & 0xFF );
//                    array[ num4 + 1 ] = ( byte ) ( ( num5 >> 8 ) & 0xFF );
//                    array[ num4 + 2 ] = ( byte ) ( ( num5 >> 16 ) & 0xFF );
//                    array[ num4 + 3 ] = ( byte ) ( num5 >> 24 );
//                    num++;
//                    num4 += 4;
//                }
//                num4 -= num3;
//            }
//            byte[] buffer = new byte[18]
//            {
//                0,
//                0,
//                2,
//                0,
//                0,
//                0,
//                0,
//                0,
//                0,
//                0,
//                0,
//                0,
//                (byte)(pixelWidth & 0xFF),
//                (byte)((pixelWidth & 0xFF00) >> 8),
//                (byte)(pixelHeight & 0xFF),
//                (byte)((pixelHeight & 0xFF00) >> 8),
//                32,
//                0
//            };
//            using ( BinaryWriter binaryWriter = new BinaryWriter( destination ) )
//            {
//                binaryWriter.Write( buffer );
//                binaryWriter.Write( array );
//            }
//        }
//    }

//    internal static WriteableBitmap FromResource( this WriteableBitmap bmp, string relativePath )
//    {
//        string fullName = Assembly.GetCallingAssembly().FullName;
//        string name = new AssemblyName(fullName).Name;
//        return bmp.FromContent( name + ";component/" + relativePath );
//    }

//    internal static WriteableBitmap FromContent( this WriteableBitmap bmp, string relativePath )
//    {
//        using ( Stream streamSource = Application.GetResourceStream( new Uri( relativePath, UriKind.Relative ) ).Stream )
//        {
//            BitmapImage bitmapImage = new BitmapImage();
//            bitmapImage.StreamSource = streamSource;
//            bmp = new WriteableBitmap( bitmapImage );
//            bitmapImage.UriSource = null;
//            return bmp;
//        }
//    }

//    internal static void FillRectangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Color color )
//    {
//        int num = color.A + 1;
//        int color2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        bmp.FillRectangle( x1, y1, x2, y2, color2, BlendMode.Alpha );
//    }

//    internal unsafe static void FillRectangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int color, BlendMode blendMode = BlendMode.Alpha )
//    {
//        int pixelWidth = bmp.PixelWidth;
//        int pixelHeight = bmp.PixelHeight;
//        int num = (color >> 24) & 0xFF;
//        int sr = (color >> 16) & 0xFF;
//        int sg = (color >> 8) & 0xFF;
//        int sb = color & 0xFF;
//        bool flag = blendMode == BlendMode.None || num == 255;
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            int* pixels = bitmapContext.Pixels;
//            if ( ( x1 >= 0 || x2 >= 0 ) && ( y1 >= 0 || y2 >= 0 ) && ( x1 < pixelWidth || x2 < pixelWidth ) && ( y1 < pixelHeight || y2 < pixelHeight ) )
//            {
//                if ( x1 < 0 )
//                {
//                    x1 = 0;
//                }
//                if ( x1 >= pixelWidth )
//                {
//                    x1 = pixelWidth - 1;
//                }
//                if ( y1 < 0 )
//                {
//                    y1 = 0;
//                }
//                if ( y1 >= pixelHeight )
//                {
//                    y1 = pixelHeight - 1;
//                }
//                if ( x2 < 0 )
//                {
//                    x2 = 0;
//                }
//                if ( x2 >= pixelWidth )
//                {
//                    x2 = pixelWidth - 1;
//                }
//                if ( y2 < 0 )
//                {
//                    y2 = 0;
//                }
//                if ( y2 >= pixelHeight )
//                {
//                    y2 = pixelHeight - 1;
//                }
//                if ( y1 > y2 )
//                {
//                    y2 -= y1;
//                    y1 += y2;
//                    y2 = y1 - y2;
//                }
//                int num2 = y1 * pixelWidth;
//                int num3 = num2 + x1;
//                int num4 = num2 + x2;
//                for ( int i = num3; i <= num4; i++ )
//                {
//                    pixels[ i ] = ( flag ? color : AlphaBlendColors( pixels[ i ], num, sr, sg, sb ) );
//                }
//                int num5 = x2 - x1 + 1;
//                int srcOffset = num3 * 4;
//                int num6 = y2 * pixelWidth + x1;
//                for ( int j = num3 + pixelWidth; j <= num6; j += pixelWidth )
//                {
//                    if ( flag )
//                    {
//                        BitmapContext.BlockCopy( bitmapContext, srcOffset, bitmapContext, j * 4, num5 * 4 );
//                    }
//                    else
//                    {
//                        for ( int k = 0; k < num5; k++ )
//                        {
//                            int num7 = j + k;
//                            pixels[ num7 ] = AlphaBlendColors( pixels[ num7 ], num, sr, sg, sb );
//                        }
//                    }
//                }
//            }
//        }
//    }

//    private static int AlphaBlendColors( int pixel, int sa, int sr, int sg, int sb )
//    {
//        int num = (pixel >> 24) & 0xFF;
//        int num2 = (pixel >> 16) & 0xFF;
//        int num3 = (pixel >> 8) & 0xFF;
//        int num4 = pixel & 0xFF;
//        return ( sa + ( num * ( 255 - sa ) * 32897 >> 23 ) << 24 ) | ( sr + ( num2 * ( 255 - sa ) * 32897 >> 23 ) << 16 ) | ( sg + ( num3 * ( 255 - sa ) * 32897 >> 23 ) << 8 ) | ( sb + ( num4 * ( 255 - sa ) * 32897 >> 23 ) );
//    }

//    internal unsafe static void FillRectangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Func<int, int, int> colorCb, BlendMode blendMode = BlendMode.Alpha )
//    {
//        int pixelWidth = bmp.PixelWidth;
//        int pixelHeight = bmp.PixelHeight;
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            int* pixels = bitmapContext.Pixels;
//            if ( ( x1 >= 0 || x2 >= 0 ) && ( y1 >= 0 || y2 >= 0 ) && ( x1 < pixelWidth || x2 < pixelWidth ) && ( y1 < pixelHeight || y2 < pixelHeight ) )
//            {
//                if ( x1 < 0 )
//                {
//                    x1 = 0;
//                }
//                if ( y1 < 0 )
//                {
//                    y1 = 0;
//                }
//                if ( x2 < 0 )
//                {
//                    x2 = 0;
//                }
//                if ( y2 < 0 )
//                {
//                    y2 = 0;
//                }
//                if ( x1 > pixelWidth )
//                {
//                    x1 = pixelWidth;
//                }
//                if ( y1 > pixelHeight )
//                {
//                    y1 = pixelHeight;
//                }
//                if ( x2 > pixelWidth )
//                {
//                    x2 = pixelWidth;
//                }
//                if ( y2 > pixelHeight )
//                {
//                    y2 = pixelHeight;
//                }
//                if ( y1 > y2 )
//                {
//                    y2 -= y1;
//                    y1 += y2;
//                    y2 = y1 - y2;
//                }
//                int num = x2 - x1 + 1;
//                int num2 = y1 * pixelWidth;
//                int num3 = num2 + x1;
//                int num4 = y2 * pixelWidth + x1;
//                int num5 = y1;
//                int num6 = num3;
//                while ( num6 < num4 )
//                {
//                    int num7 = x1;
//                    int num8 = 0;
//                    while ( num8 < num )
//                    {
//                        int num9 = num6 + num8;
//                        int num10 = colorCb(num7, num5);
//                        int num11 = (num10 >> 24) & 0xFF;
//                        int sr = (num10 >> 16) & 0xFF;
//                        int sg = (num10 >> 8) & 0xFF;
//                        int sb = num10 & 0xFF;
//                        bool flag = blendMode == BlendMode.None || num11 == 255;
//                        pixels[ num9 ] = ( flag ? num10 : AlphaBlendColors( pixels[ num9 ], num11, sr, sg, sb ) );
//                        num8++;
//                        num7++;
//                    }
//                    num6 += pixelWidth;
//                    num5++;
//                }
//            }
//        }
//    }

//    internal static void FillEllipse( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Color color )
//    {
//        int num = color.A + 1;
//        int color2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        bmp.FillEllipse( x1, y1, x2, y2, color2 );
//    }

//    internal static void FillEllipse( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int color )
//    {
//        int num = x2 - x1 >> 1;
//        int num2 = y2 - y1 >> 1;
//        int xc = x1 + num;
//        int yc = y1 + num2;
//        bmp.FillEllipseCentered( xc, yc, num, num2, color );
//    }

//    internal static void FillEllipseCentered( this WriteableBitmap bmp, int xc, int yc, int xr, int yr, Color color )
//    {
//        int num = color.A + 1;
//        int color2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        bmp.FillEllipseCentered( xc, yc, xr, yr, color2 );
//    }

//    internal static void FillEllipseCentered( this WriteableBitmap bmp, int xc, int yc, int xr, int yr, int color )
//    {
//        using ( BitmapContext context = bmp.GetBitmapContext() )
//        {
//            FillEllipseCentered( context, xc, yc, xr, yr, color, BlendMode.Alpha );
//        }
//    }

//    internal unsafe static void FillEllipseCentered( BitmapContext context, int xc, int yc, int xr, int yr, int color, BlendMode blendMode = BlendMode.Alpha )
//    {
//        int* pixels = context.Pixels;
//        int pixelWidth = context.PixelWidth;
//        int pixelHeight = context.PixelHeight;
//        if ( xr >= 1 && yr >= 1 )
//        {
//            int num = xr;
//            int num2 = 0;
//            int num3 = xr * xr << 1;
//            int num4 = yr * yr << 1;
//            int num5 = yr * yr * (1 - (xr << 1));
//            int num6 = xr * xr;
//            int num7 = 0;
//            int num8 = num4 * xr;
//            int num9 = 0;
//            int num10 = (color >> 24) & 0xFF;
//            int sr = (color >> 16) & 0xFF;
//            int sg = (color >> 8) & 0xFF;
//            int sb = color & 0xFF;
//            bool flag = blendMode == BlendMode.None || num10 == 255;
//            int num13;
//            int num14;
//            int num11;
//            int num12;
//            while ( num8 >= num9 )
//            {
//                num11 = yc + num2;
//                num12 = yc - num2;
//                if ( num11 < 0 )
//                {
//                    num11 = 0;
//                }
//                if ( num11 >= pixelHeight )
//                {
//                    num11 = pixelHeight - 1;
//                }
//                if ( num12 < 0 )
//                {
//                    num12 = 0;
//                }
//                if ( num12 >= pixelHeight )
//                {
//                    num12 = pixelHeight - 1;
//                }
//                num13 = num11 * pixelWidth;
//                num14 = num12 * pixelWidth;
//                int num15 = xc + num;
//                int num16 = xc - num;
//                if ( num15 < 0 )
//                {
//                    num15 = 0;
//                }
//                if ( num15 >= pixelWidth )
//                {
//                    num15 = pixelWidth - 1;
//                }
//                if ( num16 < 0 )
//                {
//                    num16 = 0;
//                }
//                if ( num16 >= pixelWidth )
//                {
//                    num16 = pixelWidth - 1;
//                }
//                for ( int i = num16; i <= num15; i++ )
//                {
//                    pixels[ i + num13 ] = ( flag ? color : AlphaBlendColors( pixels[ i + num13 ], num10, sr, sg, sb ) );
//                    pixels[ i + num14 ] = ( flag ? color : AlphaBlendColors( pixels[ i + num14 ], num10, sr, sg, sb ) );
//                }
//                num2++;
//                num9 += num3;
//                num7 += num6;
//                num6 += num3;
//                if ( num5 + ( num7 << 1 ) > 0 )
//                {
//                    num--;
//                    num8 -= num4;
//                    num7 += num5;
//                    num5 += num4;
//                }
//            }
//            num = 0;
//            num2 = yr;
//            num11 = yc + num2;
//            num12 = yc - num2;
//            if ( num11 < 0 )
//            {
//                num11 = 0;
//            }
//            if ( num11 >= pixelHeight )
//            {
//                num11 = pixelHeight - 1;
//            }
//            if ( num12 < 0 )
//            {
//                num12 = 0;
//            }
//            if ( num12 >= pixelHeight )
//            {
//                num12 = pixelHeight - 1;
//            }
//            num13 = num11 * pixelWidth;
//            num14 = num12 * pixelWidth;
//            num5 = yr * yr;
//            num6 = xr * xr * ( 1 - ( yr << 1 ) );
//            num7 = 0;
//            num8 = 0;
//            num9 = num3 * yr;
//            while ( num8 <= num9 )
//            {
//                int num15 = xc + num;
//                int num16 = xc - num;
//                if ( num15 < 0 )
//                {
//                    num15 = 0;
//                }
//                if ( num15 >= pixelWidth )
//                {
//                    num15 = pixelWidth - 1;
//                }
//                if ( num16 < 0 )
//                {
//                    num16 = 0;
//                }
//                if ( num16 >= pixelWidth )
//                {
//                    num16 = pixelWidth - 1;
//                }
//                for ( int j = num16; j <= num15; j++ )
//                {
//                    pixels[ j + num13 ] = ( flag ? color : AlphaBlendColors( pixels[ j + num13 ], num10, sr, sg, sb ) );
//                    pixels[ j + num14 ] = ( flag ? color : AlphaBlendColors( pixels[ j + num14 ], num10, sr, sg, sb ) );
//                }
//                num++;
//                num8 += num4;
//                num7 += num5;
//                num5 += num4;
//                if ( num6 + ( num7 << 1 ) > 0 )
//                {
//                    num2--;
//                    num11 = yc + num2;
//                    num12 = yc - num2;
//                    if ( num11 < 0 )
//                    {
//                        num11 = 0;
//                    }
//                    if ( num11 >= pixelHeight )
//                    {
//                        num11 = pixelHeight - 1;
//                    }
//                    if ( num12 < 0 )
//                    {
//                        num12 = 0;
//                    }
//                    if ( num12 >= pixelHeight )
//                    {
//                        num12 = pixelHeight - 1;
//                    }
//                    num13 = num11 * pixelWidth;
//                    num14 = num12 * pixelWidth;
//                    num9 -= num3;
//                    num7 += num6;
//                    num6 += num3;
//                }
//            }
//        }
//    }

//    internal static void FillPolygon( this WriteableBitmap bmp, int[ ] points, Color color )
//    {
//        int num = color.A + 1;
//        int color2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        bmp.FillPolygon( points, color2, BlendMode.Alpha );
//    }

//    internal unsafe static void FillPolygon( this WriteableBitmap bmp, int[ ] points, int color, BlendMode blendMode = BlendMode.Alpha )
//    {
//        int pixelWidth = bmp.PixelWidth;
//        int pixelHeight = bmp.PixelHeight;
//        int num = (color >> 24) & 0xFF;
//        int sr = (color >> 16) & 0xFF;
//        int sg = (color >> 8) & 0xFF;
//        int sb = color & 0xFF;
//        bool flag = blendMode == BlendMode.None || num == 255;
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            int* pixels = bitmapContext.Pixels;
//            int num2 = points.Length;
//            int num3 = points.Length >> 1;
//            int[] array = new int[num3];
//            int num4 = pixelHeight;
//            int num5 = 0;
//            for ( int i = 1; i < num2; i += 2 )
//            {
//                int num6 = points[i];
//                if ( num6 < num4 )
//                {
//                    num4 = num6;
//                }
//                if ( num6 > num5 )
//                {
//                    num5 = num6;
//                }
//            }
//            if ( num4 < 0 )
//            {
//                num4 = 0;
//            }
//            if ( num5 >= pixelHeight )
//            {
//                num5 = pixelHeight - 1;
//            }
//            for ( int j = num4; j <= num5; j++ )
//            {
//                float num7 = (float)points[0];
//                float num8 = (float)points[1];
//                int num9 = 0;
//                for ( int k = 2; k < num2; k += 2 )
//                {
//                    float num10 = (float)points[k];
//                    float num11 = (float)points[k + 1];
//                    if ( ( num8 < ( float ) j && num11 >= ( float ) j ) || ( num11 < ( float ) j && num8 >= ( float ) j ) )
//                    {
//                        array[ num9++ ] = ( int ) ( num7 + ( ( float ) j - num8 ) / ( num11 - num8 ) * ( num10 - num7 ) );
//                    }
//                    num7 = num10;
//                    num8 = num11;
//                }
//                for ( int l = 1; l < num9; l++ )
//                {
//                    int num13 = array[l];
//                    int num14 = l;
//                    while ( num14 > 0 && array[ num14 - 1 ] > num13 )
//                    {
//                        array[ num14 ] = array[ num14 - 1 ];
//                        num14--;
//                    }
//                    array[ num14 ] = num13;
//                }
//                for ( int m = 0; m < num9 - 1; m += 2 )
//                {
//                    int num15 = array[m];
//                    int num16 = array[m + 1];
//                    if ( num16 > 0 && num15 < pixelWidth )
//                    {
//                        if ( num15 < 0 )
//                        {
//                            num15 = 0;
//                        }
//                        if ( num16 >= pixelWidth )
//                        {
//                            num16 = pixelWidth - 1;
//                        }
//                        for ( int n = num15; n <= num16; n++ )
//                        {
//                            int num17 = j * pixelWidth + n;
//                            pixels[ num17 ] = ( flag ? color : AlphaBlendColors( pixels[ num17 ], num, sr, sg, sb ) );
//                        }
//                    }
//                }
//            }
//        }
//    }

//    internal unsafe static void FillPolygon( this WriteableBitmap bmp, int[ ] points, Func<int, int, int> colorCb, BlendMode blendMode = BlendMode.Alpha )
//    {
//        int pixelWidth = bmp.PixelWidth;
//        int pixelHeight = bmp.PixelHeight;
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            int* pixels = bitmapContext.Pixels;
//            int num = points.Length;
//            int num2 = points.Length >> 1;
//            int[] array = new int[num2];
//            int num3 = pixelHeight;
//            int num4 = 0;
//            for ( int i = 1; i < num; i += 2 )
//            {
//                int num5 = points[i];
//                if ( num5 < num3 )
//                {
//                    num3 = num5;
//                }
//                if ( num5 > num4 )
//                {
//                    num4 = num5;
//                }
//            }
//            if ( num3 < 0 )
//            {
//                num3 = 0;
//            }
//            if ( num4 >= pixelHeight )
//            {
//                num4 = pixelHeight - 1;
//            }
//            for ( int j = num3; j <= num4; j++ )
//            {
//                float num6 = (float)points[0];
//                float num7 = (float)points[1];
//                int num8 = 0;
//                for ( int k = 2; k < num; k += 2 )
//                {
//                    float num9 = (float)points[k];
//                    float num10 = (float)points[k + 1];
//                    if ( ( num7 < ( float ) j && num10 >= ( float ) j ) || ( num10 < ( float ) j && num7 >= ( float ) j ) )
//                    {
//                        array[ num8++ ] = ( int ) ( num6 + ( ( float ) j - num7 ) / ( num10 - num7 ) * ( num9 - num6 ) );
//                    }
//                    num6 = num9;
//                    num7 = num10;
//                }
//                for ( int l = 1; l < num8; l++ )
//                {
//                    int num12 = array[l];
//                    int num13 = l;
//                    while ( num13 > 0 && array[ num13 - 1 ] > num12 )
//                    {
//                        array[ num13 ] = array[ num13 - 1 ];
//                        num13--;
//                    }
//                    array[ num13 ] = num12;
//                }
//                for ( int m = 0; m < num8 - 1; m += 2 )
//                {
//                    int num14 = array[m];
//                    int num15 = array[m + 1];
//                    if ( num15 > 0 && num14 < pixelWidth )
//                    {
//                        if ( num14 < 0 )
//                        {
//                            num14 = 0;
//                        }
//                        if ( num15 >= pixelWidth )
//                        {
//                            num15 = pixelWidth - 1;
//                        }
//                        for ( int n = num14; n <= num15; n++ )
//                        {
//                            int num16 = j * pixelWidth + n;
//                            int num17 = colorCb(n, j);
//                            int num18 = (num17 >> 24) & 0xFF;
//                            int sr = (num17 >> 16) & 0xFF;
//                            int sg = (num17 >> 8) & 0xFF;
//                            int sb = num17 & 0xFF;
//                            bool flag = blendMode == BlendMode.None || num18 == 255;
//                            pixels[ num16 ] = ( flag ? num17 : AlphaBlendColors( pixels[ num16 ], num18, sr, sg, sb ) );
//                        }
//                    }
//                }
//            }
//        }
//    }

//    internal static void FillQuad( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, Color color )
//    {
//        int num = color.A + 1;
//        int color2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        bmp.FillQuad( x1, y1, x2, y2, x3, y3, x4, y4, color2 );
//    }

//    internal static void FillQuad( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, int color )
//    {
//        bmp.FillPolygon( new int[ 10 ]
//        {
//            x1,
//            y1,
//            x2,
//            y2,
//            x3,
//            y3,
//            x4,
//            y4,
//            x1,
//            y1
//        }, color, BlendMode.Alpha );
//    }

//    internal static void FillTriangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int x3, int y3, Color color )
//    {
//        int num = color.A + 1;
//        int color2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        bmp.FillTriangle( x1, y1, x2, y2, x3, y3, color2 );
//    }

//    internal static void FillTriangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int x3, int y3, int color )
//    {
//        bmp.FillPolygon( new int[ 8 ]
//        {
//            x1,
//            y1,
//            x2,
//            y2,
//            x3,
//            y3,
//            x1,
//            y1
//        }, color, BlendMode.Alpha );
//    }

//    private unsafe static List<int> ComputeBezierPoints( int x1, int y1, int cx1, int cy1, int cx2, int cy2, int x2, int y2, int color, BitmapContext context, int w, int h )
//    {
//        int* pixels = context.Pixels;
//        int num = Math.Min(x1, Math.Min(cx1, Math.Min(cx2, x2)));
//        int num2 = Math.Min(y1, Math.Min(cy1, Math.Min(cy2, y2)));
//        int num3 = Math.Max(x1, Math.Max(cx1, Math.Max(cx2, x2)));
//        int num4 = Math.Max(y1, Math.Max(cy1, Math.Max(cy2, y2)));
//        int num5 = num3 - num;
//        int num6 = num4 - num2;
//        if ( num5 > num6 )
//        {
//            num6 = num5;
//        }
//        List<int> list = new List<int>();
//        if ( num6 != 0 )
//        {
//            float num7 = 2f / (float)num6;
//            for ( float num8 = 0f; num8 <= 1f; num8 += num7 )
//            {
//                float num9 = num8 * num8;
//                float num10 = 1f - num8;
//                float num11 = num10 * num10;
//                int item = (int)(num10 * num11 * (float)x1 + 3f * num8 * num11 * (float)cx1 + 3f * num10 * num9 * (float)cx2 + num8 * num9 * (float)x2);
//                int item2 = (int)(num10 * num11 * (float)y1 + 3f * num8 * num11 * (float)cy1 + 3f * num10 * num9 * (float)cy2 + num8 * num9 * (float)y2);
//                list.Add( item );
//                list.Add( item2 );
//            }
//            list.Add( x2 );
//            list.Add( y2 );
//        }
//        return list;
//    }

//    internal static void FillBeziers( this WriteableBitmap bmp, int[ ] points, Color color )
//    {
//        int num = color.A + 1;
//        int color2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        bmp.FillBeziers( points, color2 );
//    }

//    internal static void FillBeziers( this WriteableBitmap bmp, int[ ] points, int color )
//    {
//        int pixelWidth = bmp.PixelWidth;
//        int pixelHeight = bmp.PixelHeight;
//        using ( BitmapContext context = bmp.GetBitmapContext() )
//        {
//            int x = points[0];
//            int y = points[1];
//            List<int> list = new List<int>();
//            for ( int i = 2; i + 5 < points.Length; i += 6 )
//            {
//                int num = points[i + 4];
//                int num2 = points[i + 5];
//                list.AddRange( ComputeBezierPoints( x, y, points[ i ], points[ i + 1 ], points[ i + 2 ], points[ i + 3 ], num, num2, color, context, pixelWidth, pixelHeight ) );
//                x = num;
//                y = num2;
//            }
//            bmp.FillPolygon( list.ToArray(), color, BlendMode.Alpha );
//        }
//    }

//    private unsafe static List<int> ComputeSegmentPoints( int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, float tension, int color, BitmapContext context, int w, int h )
//    {
//        int* pixels = context.Pixels;
//        int num = Math.Min(x1, Math.Min(x2, Math.Min(x3, x4)));
//        int num2 = Math.Min(y1, Math.Min(y2, Math.Min(y3, y4)));
//        int num3 = Math.Max(x1, Math.Max(x2, Math.Max(x3, x4)));
//        int num4 = Math.Max(y1, Math.Max(y2, Math.Max(y3, y4)));
//        int num5 = num3 - num;
//        int num6 = num4 - num2;
//        if ( num5 > num6 )
//        {
//            num6 = num5;
//        }
//        List<int> list = new List<int>();
//        if ( num6 != 0 )
//        {
//            float num7 = 2f / (float)num6;
//            float num8 = tension * (float)(x3 - x1);
//            float num9 = tension * (float)(y3 - y1);
//            float num10 = tension * (float)(x4 - x2);
//            float num11 = tension * (float)(y4 - y2);
//            float num12 = num8 + num10 + (float)(2 * x2) - (float)(2 * x3);
//            float num13 = num9 + num11 + (float)(2 * y2) - (float)(2 * y3);
//            float num14 = -2f * num8 - num10 - (float)(3 * x2) + (float)(3 * x3);
//            float num15 = -2f * num9 - num11 - (float)(3 * y2) + (float)(3 * y3);
//            for ( float num16 = 0f; num16 <= 1f; num16 += num7 )
//            {
//                float num17 = num16 * num16;
//                int item = (int)(num12 * num17 * num16 + num14 * num17 + num8 * num16 + (float)x2);
//                int item2 = (int)(num13 * num17 * num16 + num15 * num17 + num9 * num16 + (float)y2);
//                list.Add( item );
//                list.Add( item2 );
//            }
//            list.Add( x3 );
//            list.Add( y3 );
//        }
//        return list;
//    }

//    internal static void FillCurve( this WriteableBitmap bmp, int[ ] points, float tension, Color color )
//    {
//        int num = color.A + 1;
//        int color2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        bmp.FillCurve( points, tension, color2 );
//    }

//    internal static void FillCurve( this WriteableBitmap bmp, int[ ] points, float tension, int color )
//    {
//        int pixelWidth = bmp.PixelWidth;
//        int pixelHeight = bmp.PixelHeight;
//        using ( BitmapContext context = bmp.GetBitmapContext() )
//        {
//            List<int> list = ComputeSegmentPoints(points[0], points[1], points[0], points[1], points[2], points[3], points[4], points[5], tension, color, context, pixelWidth, pixelHeight);
//            int i;
//            for ( i = 2; i < points.Length - 4; i += 2 )
//            {
//                list.AddRange( ComputeSegmentPoints( points[ i - 2 ], points[ i - 1 ], points[ i ], points[ i + 1 ], points[ i + 2 ], points[ i + 3 ], points[ i + 4 ], points[ i + 5 ], tension, color, context, pixelWidth, pixelHeight ) );
//            }
//            list.AddRange( ComputeSegmentPoints( points[ i - 2 ], points[ i - 1 ], points[ i ], points[ i + 1 ], points[ i + 2 ], points[ i + 3 ], points[ i + 2 ], points[ i + 3 ], tension, color, context, pixelWidth, pixelHeight ) );
//            bmp.FillPolygon( list.ToArray(), color, BlendMode.Alpha );
//        }
//    }

//    internal static void FillCurveClosed( this WriteableBitmap bmp, int[ ] points, float tension, Color color )
//    {
//        int num = color.A + 1;
//        int color2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        bmp.FillCurveClosed( points, tension, color2 );
//    }

//    internal static void FillCurveClosed( this WriteableBitmap bmp, int[ ] points, float tension, int color )
//    {
//        int pixelWidth = bmp.PixelWidth;
//        int pixelHeight = bmp.PixelHeight;
//        using ( BitmapContext context = bmp.GetBitmapContext() )
//        {
//            int num = points.Length;
//            List<int> list = ComputeSegmentPoints(points[num - 2], points[num - 1], points[0], points[1], points[2], points[3], points[4], points[5], tension, color, context, pixelWidth, pixelHeight);
//            int i;
//            for ( i = 2; i < num - 4; i += 2 )
//            {
//                list.AddRange( ComputeSegmentPoints( points[ i - 2 ], points[ i - 1 ], points[ i ], points[ i + 1 ], points[ i + 2 ], points[ i + 3 ], points[ i + 4 ], points[ i + 5 ], tension, color, context, pixelWidth, pixelHeight ) );
//            }
//            list.AddRange( ComputeSegmentPoints( points[ i - 2 ], points[ i - 1 ], points[ i ], points[ i + 1 ], points[ i + 2 ], points[ i + 3 ], points[ 0 ], points[ 1 ], tension, color, context, pixelWidth, pixelHeight ) );
//            list.AddRange( ComputeSegmentPoints( points[ i ], points[ i + 1 ], points[ i + 2 ], points[ i + 3 ], points[ 0 ], points[ 1 ], points[ 2 ], points[ 3 ], tension, color, context, pixelWidth, pixelHeight ) );
//            bmp.FillPolygon( list.ToArray(), color, BlendMode.Alpha );
//        }
//    }

//    internal static WriteableBitmap Convolute( this WriteableBitmap bmp, int[ , ] kernel )
//    {
//        int num = 0;
//        foreach ( int num2 in kernel )
//        {
//            num += num2;
//        }
//        return bmp.Convolute( kernel, num, 0 );
//    }

//    internal unsafe static WriteableBitmap Convolute( this WriteableBitmap bmp, int[ , ] kernel, int kernelFactorSum, int kernelOffsetSum )
//    {
//        int num = kernel.GetUpperBound(0) + 1;
//        int num2 = kernel.GetUpperBound(1) + 1;
//        if ( ( num2 & 1 ) == 0 )
//        {
//            throw new InvalidOperationException( "Kernel width must be odd!" );
//        }
//        if ( ( num & 1 ) == 0 )
//        {
//            throw new InvalidOperationException( "Kernel height must be odd!" );
//        }
//        int pixelWidth = bmp.PixelWidth;
//        int pixelHeight = bmp.PixelHeight;
//        WriteableBitmap writeableBitmap = BitmapFactory.New(pixelWidth, pixelHeight);
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext( ReadWriteMode.ReadOnly ) )
//        {
//            using ( BitmapContext bitmapContext2 = writeableBitmap.GetBitmapContext() )
//            {
//                int* pixels = bitmapContext.Pixels;
//                int* pixels2 = bitmapContext2.Pixels;
//                int num3 = 0;
//                int num4 = num2 >> 1;
//                int num5 = num >> 1;
//                for ( int i = 0; i < pixelHeight; i++ )
//                {
//                    for ( int j = 0; j < pixelWidth; j++ )
//                    {
//                        int num6 = 0;
//                        int num7 = 0;
//                        int num8 = 0;
//                        int num9 = 0;
//                        for ( int k = -num4; k <= num4; k++ )
//                        {
//                            int num10 = k + j;
//                            if ( num10 < 0 )
//                            {
//                                num10 = 0;
//                            }
//                            else if ( num10 >= pixelWidth )
//                            {
//                                num10 = pixelWidth - 1;
//                            }
//                            for ( int l = -num5; l <= num5; l++ )
//                            {
//                                int num11 = l + i;
//                                if ( num11 < 0 )
//                                {
//                                    num11 = 0;
//                                }
//                                else if ( num11 >= pixelHeight )
//                                {
//                                    num11 = pixelHeight - 1;
//                                }
//                                int num12 = pixels[num11 * pixelWidth + num10];
//                                int num13 = kernel[l + num4, k + num5];
//                                num6 += ( ( num12 >> 24 ) & 0xFF ) * num13;
//                                num7 += ( ( num12 >> 16 ) & 0xFF ) * num13;
//                                num8 += ( ( num12 >> 8 ) & 0xFF ) * num13;
//                                num9 += ( num12 & 0xFF ) * num13;
//                            }
//                        }
//                        int num14 = num6 / kernelFactorSum + kernelOffsetSum;
//                        int num15 = num7 / kernelFactorSum + kernelOffsetSum;
//                        int num16 = num8 / kernelFactorSum + kernelOffsetSum;
//                        int num17 = num9 / kernelFactorSum + kernelOffsetSum;
//                        byte b = (byte)((num14 > 255) ? 255 : ((num14 >= 0) ? num14 : 0));
//                        byte b2 = (byte)((num15 > 255) ? 255 : ((num15 >= 0) ? num15 : 0));
//                        byte b3 = (byte)((num16 > 255) ? 255 : ((num16 >= 0) ? num16 : 0));
//                        byte b4 = (byte)((num17 > 255) ? 255 : ((num17 >= 0) ? num17 : 0));
//                        pixels2[ num3++ ] = ( ( b << 24 ) | ( b2 << 16 ) | ( b3 << 8 ) | b4 );
//                    }
//                }
//                return writeableBitmap;
//            }
//        }
//    }

//    internal unsafe static WriteableBitmap Invert( this WriteableBitmap bmp )
//    {
//        WriteableBitmap writeableBitmap = BitmapFactory.New(bmp.PixelWidth, bmp.PixelHeight);
//        using ( BitmapContext bitmapContext = writeableBitmap.GetBitmapContext() )
//        {
//            using ( BitmapContext bitmapContext2 = bmp.GetBitmapContext() )
//            {
//                int* pixels = bitmapContext.Pixels;
//                int* pixels2 = bitmapContext2.Pixels;
//                int length = bitmapContext2.Length;
//                for ( int i = 0; i < length; i++ )
//                {
//                    int num = pixels2[i];
//                    int num2 = (num >> 24) & 0xFF;
//                    int num3 = (num >> 16) & 0xFF;
//                    int num4 = (num >> 8) & 0xFF;
//                    int num5 = num & 0xFF;
//                    num3 = 255 - num3;
//                    num4 = 255 - num4;
//                    num5 = 255 - num5;
//                    pixels[ i ] = ( ( num2 << 24 ) | ( num3 << 16 ) | ( num4 << 8 ) | num5 );
//                }
//                return writeableBitmap;
//            }
//        }
//    }

//    internal static void DrawPennedLine( BitmapContext context, int w, int h, int x1, int y1, int x2, int y2, BitmapContext pen )
//    {
//        if ( ( y1 >= 0 || y2 >= 0 ) && ( y1 <= h || y2 <= h ) && ( x1 != x2 || y1 != y2 ) )
//        {
//            int pixelWidth = pen.WriteableBitmap.PixelWidth;
//            int num = pixelWidth / 2;
//            Rect sourceRect = new Rect(0.0, 0.0, (double)pixelWidth, (double)pixelWidth);
//            int num2 = x2 - x1;
//            int num3 = y2 - y1;
//            int num4 = 0;
//            if ( num2 < 0 )
//            {
//                num2 = -num2;
//                num4 = -1;
//            }
//            else if ( num2 > 0 )
//            {
//                num4 = 1;
//            }
//            int num5 = 0;
//            if ( num3 < 0 )
//            {
//                num3 = -num3;
//                num5 = -1;
//            }
//            else if ( num3 > 0 )
//            {
//                num5 = 1;
//            }
//            int num6;
//            int num7;
//            int num8;
//            int num9;
//            int num10;
//            int num11;
//            if ( num2 > num3 )
//            {
//                num6 = num4;
//                num7 = 0;
//                num8 = num4;
//                num9 = num5;
//                num10 = num3;
//                num11 = num2;
//            }
//            else
//            {
//                num6 = 0;
//                num7 = num5;
//                num8 = num4;
//                num9 = num5;
//                num10 = num2;
//                num11 = num3;
//            }
//            int num12 = x1;
//            int num13 = y1;
//            int num14 = num11 >> 1;
//            Rect destRect = new Rect((double)(num12 - num), (double)(num13 - num), (double)pixelWidth, (double)pixelWidth);
//            if ( num13 < h && num13 >= 0 && num12 < w && num12 >= 0 )
//            {
//                Blit( context, w, h, destRect, pen, sourceRect, pixelWidth );
//            }
//            for ( int i = 0; i < num11; i++ )
//            {
//                num14 -= num10;
//                if ( num14 < 0 )
//                {
//                    num14 += num11;
//                    num12 += num8;
//                    num13 += num9;
//                }
//                else
//                {
//                    num12 += num6;
//                    num13 += num7;
//                }
//                if ( num13 < h && num13 >= 0 && num12 < w && num12 >= 0 )
//                {
//                    destRect.X = ( double ) ( num12 - num );
//                    destRect.Y = ( double ) ( num13 - num );
//                    Blit( context, w, h, destRect, pen, sourceRect, pixelWidth );
//                }
//            }
//        }
//    }

//    private static byte ComputeOutCode( Rect extents, double x, double y )
//    {
//        byte b = 0;
//        if ( x < extents.Left )
//        {
//            b = ( byte ) ( b | 1 );
//        }
//        else if ( x > extents.Right )
//        {
//            b = ( byte ) ( b | 2 );
//        }
//        if ( y > extents.Bottom )
//        {
//            b = ( byte ) ( b | 4 );
//        }
//        else if ( y < extents.Top )
//        {
//            b = ( byte ) ( b | 8 );
//        }
//        return b;
//    }

//    internal static bool CohenSutherlandLineClipWithViewPortOffset( Rect viewPort, ref float xi0, ref float yi0, ref float xi1, ref float yi1, int offset )
//    {
//        Rect extents = new Rect(viewPort.X - (double)offset, viewPort.Y - (double)offset, viewPort.Width + (double)(2 * offset), viewPort.Height + (double)(2 * offset));
//        return CohenSutherlandLineClip( extents, ref xi0, ref yi0, ref xi1, ref yi1 );
//    }

//    internal static bool CohenSutherlandLineClip( Rect extents, ref float xi0, ref float yi0, ref float xi1, ref float yi1 )
//    {
//        double x = (double)xi0.ClipToInt();
//        double y = (double)yi0.ClipToInt();
//        double x2 = (double)xi1.ClipToInt();
//        double y2 = (double)yi1.ClipToInt();
//        bool result = CohenSutherlandLineClip(extents, ref x, ref y, ref x2, ref y2);
//        xi0 = ( float ) x;
//        yi0 = ( float ) y;
//        xi1 = ( float ) x2;
//        yi1 = ( float ) y2;
//        return result;
//    }

//    internal static bool CohenSutherlandLineClip( Rect extents, ref int xi0, ref int yi0, ref int xi1, ref int yi1 )
//    {
//        double x = (double)xi0;
//        double y = (double)yi0;
//        double x2 = (double)xi1;
//        double y2 = (double)yi1;
//        bool result = CohenSutherlandLineClip(extents, ref x, ref y, ref x2, ref y2);
//        xi0 = ( int ) x;
//        yi0 = ( int ) y;
//        xi1 = ( int ) x2;
//        yi1 = ( int ) y2;
//        return result;
//    }

//    internal static bool CohenSutherlandLineClip( Rect extents, ref double x0, ref double y0, ref double x1, ref double y1 )
//    {
//        byte b = ComputeOutCode(extents, x0, y0);
//        byte b2 = ComputeOutCode(extents, x1, y1);
//        if ( b == 0 && b2 == 0 )
//        {
//            return true;
//        }
//        bool result = false;
//        while ( true )
//        {
//            if ( ( b | b2 ) == 0 )
//            {
//                result = true;
//                break;
//            }
//            if ( ( b & b2 ) != 0 )
//            {
//                break;
//            }
//            double num = x1;
//            double num2 = y1;
//            byte b3 = (b != 0) ? b : b2;
//            if ( ( b3 & 8 ) != 0 )
//            {
//                if ( !double.IsInfinity( y0 ) )
//                {
//                    num = x0 + ( x1 - x0 ) * ( extents.Top - y0 ) / ( y1 - y0 );
//                }
//                num2 = extents.Top;
//            }
//            else if ( ( b3 & 4 ) != 0 )
//            {
//                if ( !double.IsInfinity( y0 ) )
//                {
//                    num = x0 + ( x1 - x0 ) * ( extents.Bottom - y0 ) / ( y1 - y0 );
//                }
//                num2 = extents.Bottom;
//            }
//            else if ( ( b3 & 2 ) != 0 )
//            {
//                if ( !double.IsInfinity( x0 ) )
//                {
//                    num2 = y0 + ( y1 - y0 ) * ( extents.Right - x0 ) / ( x1 - x0 );
//                }
//                num = extents.Right;
//            }
//            else if ( ( b3 & 1 ) != 0 )
//            {
//                if ( !double.IsInfinity( x0 ) )
//                {
//                    num2 = y0 + ( y1 - y0 ) * ( extents.Left - x0 ) / ( x1 - x0 );
//                }
//                num = extents.Left;
//            }
//            else
//            {
//                num = double.NaN;
//                num2 = double.NaN;
//            }
//            if ( b3 == b )
//            {
//                x0 = num;
//                y0 = num2;
//                b = ComputeOutCode( extents, x0, y0 );
//            }
//            else
//            {
//                x1 = num;
//                y1 = num2;
//                b2 = ComputeOutCode( extents, x1, y1 );
//            }
//        }
//        return result;
//    }

//    public static int AlphaBlend( int sa, int sr, int sg, int sb, int destPixel )
//    {
//        int num = (destPixel >> 24) & 0xFF;
//        int num2 = (destPixel >> 16) & 0xFF;
//        int num3 = (destPixel >> 8) & 0xFF;
//        int num4 = destPixel & 0xFF;
//        destPixel = ( ( sa + ( num * ( 255 - sa ) * 32897 >> 23 ) << 24 ) | ( sr + ( num2 * ( 255 - sa ) * 32897 >> 23 ) << 16 ) | ( sg + ( num3 * ( 255 - sa ) * 32897 >> 23 ) << 8 ) | ( sb + ( num4 * ( 255 - sa ) * 32897 >> 23 ) ) );
//        return destPixel;
//    }



//    internal static void DrawLineBresenham( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Color color )
//    {
//        int num = color.A + 1;
//        int color2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        bmp.DrawLineBresenham( x1, y1, x2, y2, color2 );
//    }

//    internal static void DrawLineBresenham( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int color )
//    {
//        using ( BitmapContext context = bmp.GetBitmapContext() )
//        {
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            DrawLineBresenham( context, pixelWidth, pixelHeight, x1, y1, x2, y2, color );
//        }
//    }

//    internal unsafe static void DrawLineBresenham( BitmapContext context, int w, int h, int x1, int y1, int x2, int y2, int color )
//    {
//        if ( ( y1 >= 0 || y2 >= 0 ) && ( y1 <= h || y2 <= h ) )
//        {
//            if ( x1 == x2 && y1 == y2 )
//            {
//                DrawPixel( context, w, h, x1, y1, color );
//            }
//            else
//            {
//                int* pixels = context.Pixels;
//                int num = x2 - x1;
//                int num2 = y2 - y1;
//                int num3 = 0;
//                if ( num < 0 )
//                {
//                    num = -num;
//                    num3 = -1;
//                }
//                else if ( num > 0 )
//                {
//                    num3 = 1;
//                }
//                int num4 = 0;
//                if ( num2 < 0 )
//                {
//                    num2 = -num2;
//                    num4 = -1;
//                }
//                else if ( num2 > 0 )
//                {
//                    num4 = 1;
//                }
//                int num5;
//                int num6;
//                int num7;
//                int num8;
//                int num9;
//                int num10;
//                if ( num > num2 )
//                {
//                    num5 = num3;
//                    num6 = 0;
//                    num7 = num3;
//                    num8 = num4;
//                    num9 = num2;
//                    num10 = num;
//                }
//                else
//                {
//                    num5 = 0;
//                    num6 = num4;
//                    num7 = num3;
//                    num8 = num4;
//                    num9 = num;
//                    num10 = num2;
//                }
//                int num11 = x1;
//                int num12 = y1;
//                int num13 = num10 >> 1;
//                if ( num12 < h && num12 >= 0 && num11 < w && num11 >= 0 )
//                {
//                    pixels[ num12 * w + num11 ] = color;
//                }
//                for ( int i = 0; i < num10; i++ )
//                {
//                    num13 -= num9;
//                    if ( num13 < 0 )
//                    {
//                        num13 += num10;
//                        num11 += num7;
//                        num12 += num8;
//                    }
//                    else
//                    {
//                        num11 += num5;
//                        num12 += num6;
//                    }
//                    if ( num12 < h && num12 >= 0 && num11 < w && num11 >= 0 )
//                    {
//                        pixels[ num12 * w + num11 ] = color;
//                    }
//                }
//            }
//        }
//    }

//    internal unsafe static void DrawPixel( BitmapContext context, int w, int h, int x1, int y1, int color )
//    {
//        if ( y1 < h && y1 >= 0 && x1 < w && x1 >= 0 )
//        {
//            context.Pixels[ y1 * w + x1 ] = color;
//        }
//    }

//    internal static void DrawLineDDA( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Color color )
//    {
//        int num = color.A + 1;
//        int color2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        bmp.DrawLineDDA( x1, y1, x2, y2, color2 );
//    }

//    internal unsafe static void DrawLineDDA( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int color )
//    {
//        int pixelWidth = bmp.PixelWidth;
//        int pixelHeight = bmp.PixelHeight;
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            int* pixels = bitmapContext.Pixels;
//            int num = x2 - x1;
//            int num2 = y2 - y1;
//            int num3 = (num2 >= 0) ? num2 : (-num2);
//            int num4 = (num >= 0) ? num : (-num);
//            if ( num4 > num3 )
//            {
//                num3 = num4;
//            }
//            if ( num3 != 0 )
//            {
//                float num5 = (float)num / (float)num3;
//                float num6 = (float)num2 / (float)num3;
//                float num7 = (float)x1;
//                float num8 = (float)y1;
//                for ( int i = 0; i < num3; i++ )
//                {
//                    if ( num8 < ( float ) pixelHeight && num8 >= 0f && num7 < ( float ) pixelWidth && num7 >= 0f )
//                    {
//                        pixels[ ( int ) num8 * pixelWidth + ( int ) num7 ] = color;
//                    }
//                    num7 += num5;
//                    num8 += num6;
//                }
//            }
//        }
//    }

//    internal static void DrawLine( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Color color )
//    {
//        int num = color.A + 1;
//        int color2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        bmp.DrawLine( x1, y1, x2, y2, color2 );
//    }

//    internal static void DrawLine( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int color )
//    {
//        using ( BitmapContext context = bmp.GetBitmapContext() )
//        {
//            DrawLine( context, bmp.PixelWidth, bmp.PixelHeight, x1, y1, x2, y2, color );
//        }
//    }

//    internal unsafe static void DrawLine( BitmapContext context, int pixelWidth, int pixelHeight, int x1, int y1, int x2, int y2, int color )
//    {
//        int* pixels = context.Pixels;
//        int num = x2 - x1;
//        int num2 = y2 - y1;
//        int num3 = (num2 < 0) ? (-num2) : num2;
//        int num4 = (num < 0) ? (-num) : num;
//        if ( num4 > num3 )
//        {
//            if ( num < 0 )
//            {
//                int num5 = x1;
//                x1 = x2;
//                x2 = num5;
//                num5 = y1;
//                y1 = y2;
//                y2 = num5;
//            }
//            int num6 = (num2 << 8) / num;
//            int num7 = y1 << 8;
//            int num8 = y2 << 8;
//            int num9 = pixelHeight << 8;
//            if ( y1 < y2 )
//            {
//                if ( y1 >= pixelHeight || y2 < 0 )
//                {
//                    return;
//                }
//                if ( num7 < 0 )
//                {
//                    if ( num6 == 0 )
//                    {
//                        return;
//                    }
//                    int num10 = num7;
//                    num7 = num6 - 1 + ( num7 + 1 ) % num6;
//                    x1 += ( num7 - num10 ) / num6;
//                }
//                if ( num8 >= num9 && num6 != 0 )
//                {
//                    num8 = num9 - 1 - ( num9 - 1 - num7 ) % num6;
//                    x2 = x1 + ( num8 - num7 ) / num6;
//                }
//            }
//            else
//            {
//                if ( y2 >= pixelHeight || y1 < 0 )
//                {
//                    return;
//                }
//                if ( num7 >= num9 )
//                {
//                    if ( num6 == 0 )
//                    {
//                        return;
//                    }
//                    int num11 = num7;
//                    num7 = num9 - 1 + ( num6 - ( num9 - 1 - num11 ) % num6 );
//                    x1 += ( num7 - num11 ) / num6;
//                }
//                if ( num8 < 0 && num6 != 0 )
//                {
//                    num8 = num7 % num6;
//                    x2 = x1 + ( num8 - num7 ) / num6;
//                }
//            }
//            if ( x1 < 0 )
//            {
//                num7 -= num6 * x1;
//                x1 = 0;
//            }
//            if ( x2 >= pixelWidth )
//            {
//                x2 = pixelWidth - 1;
//            }
//            int num12 = num7;
//            int num13 = num12 >> 8;
//            int num14 = num13;
//            int num15 = x1 + num13 * pixelWidth;
//            int num16 = (num6 < 0) ? (1 - pixelWidth) : (1 + pixelWidth);
//            for ( int i = x1; i <= x2; i++ )
//            {
//                pixels[ num15 ] = color;
//                num12 += num6;
//                num13 = num12 >> 8;
//                if ( num13 != num14 )
//                {
//                    num14 = num13;
//                    num15 += num16;
//                }
//                else
//                {
//                    num15++;
//                }
//            }
//        }
//        else if ( num3 != 0 )
//        {
//            if ( num2 < 0 )
//            {
//                int num17 = x1;
//                x1 = x2;
//                x2 = num17;
//                num17 = y1;
//                y1 = y2;
//                y2 = num17;
//            }
//            int num18 = x1 << 8;
//            int num19 = x2 << 8;
//            int num20 = pixelWidth << 8;
//            int num21 = (num << 8) / num2;
//            if ( x1 < x2 )
//            {
//                if ( x1 >= pixelWidth || x2 < 0 )
//                {
//                    return;
//                }
//                if ( num18 < 0 )
//                {
//                    if ( num21 == 0 )
//                    {
//                        return;
//                    }
//                    int num22 = num18;
//                    num18 = num21 - 1 + ( num18 + 1 ) % num21;
//                    y1 += ( num18 - num22 ) / num21;
//                }
//                if ( num19 >= num20 && num21 != 0 )
//                {
//                    num19 = num20 - 1 - ( num20 - 1 - num18 ) % num21;
//                    y2 = y1 + ( num19 - num18 ) / num21;
//                }
//            }
//            else
//            {
//                if ( x2 >= pixelWidth || x1 < 0 )
//                {
//                    return;
//                }
//                if ( num18 >= num20 )
//                {
//                    if ( num21 == 0 )
//                    {
//                        return;
//                    }
//                    int num23 = num18;
//                    num18 = num20 - 1 + ( num21 - ( num20 - 1 - num23 ) % num21 );
//                    y1 += ( num18 - num23 ) / num21;
//                }
//                if ( num19 < 0 && num21 != 0 )
//                {
//                    num19 = num18 % num21;
//                    y2 = y1 + ( num19 - num18 ) / num21;
//                }
//            }
//            if ( y1 < 0 )
//            {
//                num18 -= num21 * y1;
//                y1 = 0;
//            }
//            if ( y2 >= pixelHeight )
//            {
//                y2 = pixelHeight - 1;
//            }
//            int num24 = num18 + (y1 * pixelWidth << 8);
//            int num25 = (pixelWidth << 8) + num21;
//            for ( int j = y1; j <= y2; j++ )
//            {
//                pixels[ num24 >> 8 ] = color;
//                num24 += num25;
//            }
//        }
//    }

//    internal static void DrawLineAa( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Color color )
//    {
//        int num = color.A + 1;
//        int color2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        bmp.DrawLineAa( x1, y1, x2, y2, color2 );
//    }

//    internal static void DrawLineAa( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int color )
//    {
//        using ( BitmapContext context = bmp.GetBitmapContext() )
//        {
//            DrawLineAa( context, bmp.PixelWidth, bmp.PixelHeight, x1, y1, x2, y2, color, false );
//        }
//    }

//    internal unsafe static void DrawLineAa( BitmapContext context, int pixelWidth, int pixelHeight, int x1, int y1, int x2, int y2, int color, bool skipFirstPixel = false )
//    {
//        if ( CohenSutherlandLineClip( new Rect( 0.0, 0.0, ( double ) pixelWidth, ( double ) pixelHeight ), ref x1, ref y1, ref x2, ref y2 ) )
//        {
//            int num = pixelWidth - 1;
//            int num2 = pixelHeight - 1;
//            if ( x1 < 0 )
//            {
//                x1 = 0;
//            }
//            else if ( x1 > num )
//            {
//                x1 = num;
//            }
//            if ( y1 < 0 )
//            {
//                y1 = 0;
//            }
//            else if ( y1 > num2 )
//            {
//                y1 = num2;
//            }
//            if ( x2 < 0 )
//            {
//                x2 = 0;
//            }
//            if ( x2 > num )
//            {
//                x2 = num;
//            }
//            if ( y2 < 0 )
//            {
//                y2 = 0;
//            }
//            if ( y2 > num2 )
//            {
//                y2 = num2;
//            }
//            int num3 = pixelWidth * pixelHeight;
//            int num4 = y1 * pixelWidth + x1;
//            int num5 = x2 - x1;
//            int num6 = y2 - y1;
//            int num7 = (color >> 24) & 0xFF;
//            uint srb = (uint)(color & 0xFF00FF);
//            uint sg = (uint)((color >> 8) & 0xFF);
//            int num8 = num5;
//            int num9 = num6;
//            if ( num5 < 0 )
//            {
//                num8 = -num5;
//            }
//            if ( num6 < 0 )
//            {
//                num9 = -num6;
//            }
//            int num10;
//            int num11;
//            int num12;
//            int num13;
//            int num14;
//            int num16;
//            int num15;
//            if ( num8 > num9 )
//            {
//                num10 = num8;
//                num11 = num9;
//                num12 = x2;
//                num13 = y2;
//                num14 = 1;
//                num16 = ( num15 = pixelWidth );
//                if ( num5 < 0 )
//                {
//                    num14 = -num14;
//                }
//                if ( num6 < 0 )
//                {
//                    num16 = -num16;
//                }
//            }
//            else
//            {
//                num10 = num9;
//                num11 = num8;
//                num12 = y2;
//                num13 = x2;
//                num14 = ( num15 = pixelWidth );
//                num16 = 1;
//                if ( num6 < 0 )
//                {
//                    num14 = -num14;
//                }
//                if ( num5 < 0 )
//                {
//                    num16 = -num16;
//                }
//            }
//            int num17 = num12 + num10;
//            int num18 = (num11 << 1) - num10;
//            int num19 = num11 << 1;
//            int num20 = num11 - num10 << 1;
//            double num21 = 1.0 / (4.0 * Math.Sqrt((double)(num10 * num10 + num11 * num11)));
//            double num22 = 0.75 - 2.0 * ((double)num10 * num21);
//            int num23 = (int)(num21 * 1024.0);
//            int num24 = (int)(num22 * 1024.0 * (double)num7);
//            int num25 = (int)(768.0 * (double)num7);
//            int num26 = num23 * num7;
//            int num27 = num10 * num26;
//            int num28 = num18 * num26;
//            int num29 = 0;
//            int num30 = num19 * num26;
//            int num31 = num20 * num26;
//            int* pixels = context.Pixels;
//            bool flag = true;
//            do
//            {
//                if ( !flag || !skipFirstPixel )
//                {
//                    AlphaBlendNormalOnPremultiplied( pixels, num4, num25 - num29 >> 10, srb, sg );
//                    int num32 = num4 + num16;
//                    if ( num32 < num3 && ( ( flag && num15 == num16 ) || num32 % num15 > 0 ) )
//                    {
//                        AlphaBlendNormalOnPremultiplied( pixels, num32, num24 + num29 >> 10, srb, sg );
//                    }
//                    num32 = num4 - num16;
//                    if ( num32 >= 0 && num32 < num3 && ( ( flag && num15 == num16 ) || num4 % num15 > 0 ) )
//                    {
//                        AlphaBlendNormalOnPremultiplied( pixels, num32, num24 - num29 >> 10, srb, sg );
//                    }
//                }
//                if ( num18 < 0 )
//                {
//                    num29 = num28 + num27;
//                    num18 += num19;
//                    num28 += num30;
//                }
//                else
//                {
//                    num29 = num28 - num27;
//                    num18 += num20;
//                    num28 += num31;
//                    num13++;
//                    num4 += num16;
//                }
//                num12++;
//                num4 += num14;
//                flag = false;
//            }
//            while ( num12 <= num17 );
//        }
//    }

//    private unsafe static void AlphaBlendNormalOnPremultiplied( int* pixels, int index, int sa, uint srb, uint sg )
//    {
//        uint num = (uint)pixels[index];
//        uint num2 = num >> 24;
//        uint num3 = (num >> 8) & 0xFF;
//        uint num4 = num & 0xFF00FF;
//        pixels[ index ] = ( int ) ( ( sa + ( num2 * ( 255 - sa ) * 32897 >> 23 ) << 24 ) | ( ( ( sg - num3 ) * sa + ( num3 << 8 ) ) & 4294967040u ) | ( ( ( ( srb - num4 ) * sa >> 8 ) + num4 ) & 0xFF00FF ) );
//    }

//    internal static void DrawPolyline( this WriteableBitmap bmp, int[ ] points, Color color )
//    {
//        int num = color.A + 1;
//        int color2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        bmp.DrawPolyline( points, color2 );
//    }

//    internal static void DrawPolyline( this WriteableBitmap bmp, int[ ] points, int color )
//    {
//        int pixelWidth = bmp.PixelWidth;
//        int pixelHeight = bmp.PixelHeight;
//        using ( BitmapContext context = bmp.GetBitmapContext() )
//        {
//            int num = points[0];
//            int num2 = points[1];
//            if ( num < 0 )
//            {
//                num = 0;
//            }
//            if ( num2 < 0 )
//            {
//                num2 = 0;
//            }
//            if ( num > pixelWidth )
//            {
//                num = pixelWidth;
//            }
//            if ( num2 > pixelHeight )
//            {
//                num2 = pixelHeight;
//            }
//            for ( int i = 2; i < points.Length; i += 2 )
//            {
//                int num3 = points[i];
//                int num4 = points[i + 1];
//                if ( num3 < 0 )
//                {
//                    num3 = 0;
//                }
//                if ( num4 < 0 )
//                {
//                    num4 = 0;
//                }
//                if ( num3 > pixelWidth )
//                {
//                    num3 = pixelWidth;
//                }
//                if ( num4 > pixelHeight )
//                {
//                    num4 = pixelHeight;
//                }
//                DrawLine( context, pixelWidth, pixelHeight, num, num2, num3, num4, color );
//                num = num3;
//                num2 = num4;
//            }
//        }
//    }

//    internal static void DrawTriangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int x3, int y3, Color color )
//    {
//        int num = color.A + 1;
//        int color2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        bmp.DrawTriangle( x1, y1, x2, y2, x3, y3, color2 );
//    }

//    internal static void DrawTriangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int x3, int y3, int color )
//    {
//        int pixelWidth = bmp.PixelWidth;
//        int pixelHeight = bmp.PixelHeight;
//        using ( BitmapContext context = bmp.GetBitmapContext() )
//        {
//            DrawLine( context, pixelWidth, pixelHeight, x1, y1, x2, y2, color );
//            DrawLine( context, pixelWidth, pixelHeight, x2, y2, x3, y3, color );
//            DrawLine( context, pixelWidth, pixelHeight, x3, y3, x1, y1, color );
//        }
//    }

//    internal static void DrawQuad( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, Color color )
//    {
//        int num = color.A + 1;
//        int color2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        bmp.DrawQuad( x1, y1, x2, y2, x3, y3, x4, y4, color2 );
//    }

//    internal static void DrawQuad( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, int color )
//    {
//        int pixelWidth = bmp.PixelWidth;
//        int pixelHeight = bmp.PixelHeight;
//        using ( BitmapContext context = bmp.GetBitmapContext() )
//        {
//            DrawLine( context, pixelWidth, pixelHeight, x1, y1, x2, y2, color );
//            DrawLine( context, pixelWidth, pixelHeight, x2, y2, x3, y3, color );
//            DrawLine( context, pixelWidth, pixelHeight, x3, y3, x4, y4, color );
//            DrawLine( context, pixelWidth, pixelHeight, x4, y4, x1, y1, color );
//        }
//    }

//    internal static void DrawRectangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Color color )
//    {
//        int num = color.A + 1;
//        int color2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        bmp.DrawRectangle( x1, y1, x2, y2, color2 );
//    }

//    internal unsafe static void DrawRectangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int color )
//    {
//        int pixelWidth = bmp.PixelWidth;
//        int pixelHeight = bmp.PixelHeight;
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            int* pixels = bitmapContext.Pixels;
//            if ( ( x1 >= 0 || x2 >= 0 ) && ( y1 >= 0 || y2 >= 0 ) && ( x1 < pixelWidth || x2 < pixelWidth ) && ( y1 < pixelHeight || y2 < pixelHeight ) )
//            {
//                if ( x1 < 0 )
//                {
//                    x1 = 0;
//                }
//                if ( y1 < 0 )
//                {
//                    y1 = 0;
//                }
//                if ( x2 < 0 )
//                {
//                    x2 = 0;
//                }
//                if ( y2 < 0 )
//                {
//                    y2 = 0;
//                }
//                if ( x1 > pixelWidth )
//                {
//                    x1 = pixelWidth;
//                }
//                if ( y1 > pixelHeight )
//                {
//                    y1 = pixelHeight;
//                }
//                if ( x2 > pixelWidth )
//                {
//                    x2 = pixelWidth;
//                }
//                if ( y2 > pixelHeight )
//                {
//                    y2 = pixelHeight;
//                }
//                int num = y1 * pixelWidth;
//                int num2 = y2 * pixelWidth;
//                int num3 = num2 - pixelWidth + x1;
//                int num4 = num + x2;
//                int num5 = num + x1;
//                for ( int i = num5; i < num4; i++ )
//                {
//                    pixels[ i ] = color;
//                    pixels[ num3 ] = color;
//                    num3++;
//                }
//                num4 = num5 + pixelWidth;
//                num3 -= pixelWidth;
//                for ( int j = num + x2 - 1 + pixelWidth; j < num3; j += pixelWidth )
//                {
//                    pixels[ j ] = color;
//                    pixels[ num4 ] = color;
//                    num4 += pixelWidth;
//                }
//            }
//        }
//    }

//    internal static void DrawEllipse( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int color, int thickness )
//    {
//        int num = x2 - x1 >> 1;
//        int num2 = y2 - y1 >> 1;
//        int xc = x1 + num;
//        int yc = y1 + num2;
//        bmp.DrawEllipseCentered( xc, yc, num, num2, color, thickness );
//    }

//    internal static void DrawEllipseCentered( this WriteableBitmap bmp, int xc, int yc, int xr, int yr, Color color )
//    {
//        int num = color.A + 1;
//        int color2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        bmp.DrawEllipseCentered( xc, yc, xr, yr, color2 );
//    }

//    internal unsafe static void DrawEllipseCentered( this WriteableBitmap bmp, int xc, int yc, int xr, int yr, int color )
//    {
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            int* pixels = bitmapContext.Pixels;
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            if ( xr >= 1 && yr >= 1 )
//            {
//                int num = xr;
//                int num2 = 0;
//                int num3 = xr * xr << 1;
//                int num4 = yr * yr << 1;
//                int num5 = yr * yr * (1 - (xr << 1));
//                int num6 = xr * xr;
//                int num7 = 0;
//                int num8 = num4 * xr;
//                int num9 = 0;
//                int num12;
//                int num13;
//                int num10;
//                int num11;
//                while ( num8 >= num9 )
//                {
//                    num10 = yc + num2;
//                    num11 = yc - num2;
//                    if ( num10 < 0 )
//                    {
//                        num10 = 0;
//                    }
//                    if ( num10 >= pixelHeight )
//                    {
//                        num10 = pixelHeight - 1;
//                    }
//                    if ( num11 < 0 )
//                    {
//                        num11 = 0;
//                    }
//                    if ( num11 >= pixelHeight )
//                    {
//                        num11 = pixelHeight - 1;
//                    }
//                    num12 = num10 * pixelWidth;
//                    num13 = num11 * pixelWidth;
//                    int num14 = xc + num;
//                    int num15 = xc - num;
//                    if ( num14 < 0 )
//                    {
//                        num14 = 0;
//                    }
//                    if ( num14 >= pixelWidth )
//                    {
//                        num14 = pixelWidth - 1;
//                    }
//                    if ( num15 < 0 )
//                    {
//                        num15 = 0;
//                    }
//                    if ( num15 >= pixelWidth )
//                    {
//                        num15 = pixelWidth - 1;
//                    }
//                    pixels[ num14 + num12 ] = color;
//                    pixels[ num15 + num12 ] = color;
//                    pixels[ num15 + num13 ] = color;
//                    pixels[ num14 + num13 ] = color;
//                    num2++;
//                    num9 += num3;
//                    num7 += num6;
//                    num6 += num3;
//                    if ( num5 + ( num7 << 1 ) > 0 )
//                    {
//                        num--;
//                        num8 -= num4;
//                        num7 += num5;
//                        num5 += num4;
//                    }
//                }
//                num = 0;
//                num2 = yr;
//                num10 = yc + num2;
//                num11 = yc - num2;
//                if ( num10 < 0 )
//                {
//                    num10 = 0;
//                }
//                if ( num10 >= pixelHeight )
//                {
//                    num10 = pixelHeight - 1;
//                }
//                if ( num11 < 0 )
//                {
//                    num11 = 0;
//                }
//                if ( num11 >= pixelHeight )
//                {
//                    num11 = pixelHeight - 1;
//                }
//                num12 = num10 * pixelWidth;
//                num13 = num11 * pixelWidth;
//                num5 = yr * yr;
//                num6 = xr * xr * ( 1 - ( yr << 1 ) );
//                num7 = 0;
//                num8 = 0;
//                num9 = num3 * yr;
//                while ( num8 <= num9 )
//                {
//                    int num14 = xc + num;
//                    int num15 = xc - num;
//                    if ( num14 < 0 )
//                    {
//                        num14 = 0;
//                    }
//                    if ( num14 >= pixelWidth )
//                    {
//                        num14 = pixelWidth - 1;
//                    }
//                    if ( num15 < 0 )
//                    {
//                        num15 = 0;
//                    }
//                    if ( num15 >= pixelWidth )
//                    {
//                        num15 = pixelWidth - 1;
//                    }
//                    pixels[ num14 + num12 ] = color;
//                    pixels[ num15 + num12 ] = color;
//                    pixels[ num15 + num13 ] = color;
//                    pixels[ num14 + num13 ] = color;
//                    num++;
//                    num8 += num4;
//                    num7 += num5;
//                    num5 += num4;
//                    if ( num6 + ( num7 << 1 ) > 0 )
//                    {
//                        num2--;
//                        num10 = yc + num2;
//                        num11 = yc - num2;
//                        if ( num10 < 0 )
//                        {
//                            num10 = 0;
//                        }
//                        if ( num10 >= pixelHeight )
//                        {
//                            num10 = pixelHeight - 1;
//                        }
//                        if ( num11 < 0 )
//                        {
//                            num11 = 0;
//                        }
//                        if ( num11 >= pixelHeight )
//                        {
//                            num11 = pixelHeight - 1;
//                        }
//                        num12 = num10 * pixelWidth;
//                        num13 = num11 * pixelWidth;
//                        num9 -= num3;
//                        num7 += num6;
//                        num6 += num3;
//                    }
//                }
//            }
//        }
//    }

//    internal static void DrawEllipseCentered( this WriteableBitmap bmp, int xc, int yc, int xr, int yr, int color, int thickness )
//    {
//        using ( BitmapContext context = bmp.GetBitmapContext() )
//        {
//            DrawEllipseCentered( context, xc, yc, xr, yr, color, thickness );
//        }
//    }

//    internal unsafe static void DrawEllipseCentered( BitmapContext context, int xc, int yc, int xr, int yr, int color, int thickness )
//    {
//        int num = thickness >> 1;
//        int num2 = thickness - num - 1;
//        int* pixels = context.Pixels;
//        int pixelWidth = context.PixelWidth;
//        int pixelHeight = context.PixelHeight;
//        if ( xr >= 1 && yr >= 1 )
//        {
//            int num3 = xr;
//            int num4 = 0;
//            int num5 = xr * xr << 1;
//            int num6 = yr * yr << 1;
//            int num7 = yr * yr * (1 - (xr << 1));
//            int num8 = xr * xr;
//            int num9 = 0;
//            int num10 = num6 * xr;
//            int num11 = 0;
//            int num12 = 0;
//            int num13;
//            int num14;
//            int num15;
//            int num16;
//            while ( num10 >= num11 )
//            {
//                num13 = yc + num4;
//                num14 = yc - num4;
//                if ( num13 < 0 )
//                {
//                    num13 = 0;
//                }
//                if ( num13 >= pixelHeight )
//                {
//                    num13 = pixelHeight - 1;
//                }
//                if ( num14 < 0 )
//                {
//                    num14 = 0;
//                }
//                if ( num14 >= pixelHeight )
//                {
//                    num14 = pixelHeight - 1;
//                }
//                num15 = num13 * pixelWidth;
//                num16 = num14 * pixelWidth;
//                int num17 = xc + num3;
//                int num18 = xc - num3;
//                if ( num17 < 0 )
//                {
//                    num17 = 0;
//                }
//                if ( num17 >= pixelWidth )
//                {
//                    num17 = pixelWidth - 1;
//                }
//                if ( num18 < 0 )
//                {
//                    num18 = 0;
//                }
//                if ( num18 >= pixelWidth )
//                {
//                    num18 = pixelWidth - 1;
//                }
//                pixels[ num17 + num15 ] = color;
//                pixels[ num18 + num15 ] = color;
//                pixels[ num18 + num16 ] = color;
//                pixels[ num17 + num16 ] = color;
//                for ( int i = 1; i <= num; i++ )
//                {
//                    if ( num17 + i < pixelWidth )
//                    {
//                        pixels[ num17 + i + num15 ] = color;
//                        pixels[ num17 + i + num16 ] = color;
//                    }
//                    if ( num18 - i >= 0 )
//                    {
//                        pixels[ num18 - i + num15 ] = color;
//                        pixels[ num18 - i + num16 ] = color;
//                    }
//                }
//                for ( int j = 1; j <= num2; j++ )
//                {
//                    if ( num17 - j < pixelWidth )
//                    {
//                        pixels[ num17 - j + num15 ] = color;
//                        pixels[ num17 - j + num16 ] = color;
//                    }
//                    if ( num18 + j >= 0 )
//                    {
//                        pixels[ num18 + j + num15 ] = color;
//                        pixels[ num18 + j + num16 ] = color;
//                    }
//                }
//                num12 = num17 - xc;
//                num4++;
//                num11 += num5;
//                num9 += num8;
//                num8 += num5;
//                if ( num7 + ( num9 << 1 ) > 0 )
//                {
//                    num3--;
//                    num10 -= num6;
//                    num9 += num7;
//                    num7 += num6;
//                }
//            }
//            num3 = 0;
//            num4 = yr;
//            num13 = yc + num4;
//            num14 = yc - num4;
//            if ( num13 < 0 )
//            {
//                num13 = 0;
//            }
//            if ( num13 >= pixelHeight )
//            {
//                num13 = pixelHeight - 1;
//            }
//            if ( num14 < 0 )
//            {
//                num14 = 0;
//            }
//            if ( num14 >= pixelHeight )
//            {
//                num14 = pixelHeight - 1;
//            }
//            num15 = num13 * pixelWidth;
//            num16 = num14 * pixelWidth;
//            num7 = yr * yr;
//            num8 = xr * xr * ( 1 - ( yr << 1 ) );
//            num9 = 0;
//            num10 = 0;
//            num11 = num5 * yr;
//            while ( num10 <= num11 )
//            {
//                int num17 = xc + num3;
//                int num18 = xc - num3;
//                if ( num17 < 0 )
//                {
//                    num17 = 0;
//                }
//                if ( num17 >= pixelWidth )
//                {
//                    num17 = pixelWidth - 1;
//                }
//                if ( num18 < 0 )
//                {
//                    num18 = 0;
//                }
//                if ( num18 >= pixelWidth )
//                {
//                    num18 = pixelWidth - 1;
//                }
//                pixels[ num17 + num15 ] = color;
//                pixels[ num18 + num15 ] = color;
//                pixels[ num18 + num16 ] = color;
//                pixels[ num17 + num16 ] = color;
//                for ( int k = 1; k <= num; k++ )
//                {
//                    if ( num13 + k < pixelHeight )
//                    {
//                        pixels[ num17 + num15 + k * pixelWidth ] = color;
//                        pixels[ num18 + num15 + k * pixelWidth ] = color;
//                    }
//                    if ( num14 - k >= 0 )
//                    {
//                        pixels[ num18 + num16 - k * pixelWidth ] = color;
//                        pixels[ num17 + num16 - k * pixelWidth ] = color;
//                    }
//                }
//                for ( int l = 1; l <= num2; l++ )
//                {
//                    if ( num13 - l >= 0 )
//                    {
//                        pixels[ num17 + num15 - l * pixelWidth ] = color;
//                        pixels[ num18 + num15 - l * pixelWidth ] = color;
//                    }
//                    if ( num14 + l < pixelHeight )
//                    {
//                        pixels[ num18 + num16 + l * pixelWidth ] = color;
//                        pixels[ num17 + num16 + l * pixelWidth ] = color;
//                    }
//                }
//                num3++;
//                num10 += num6;
//                num9 += num7;
//                num7 += num6;
//                if ( num8 + ( num9 << 1 ) > 0 )
//                {
//                    num4--;
//                    num13 = yc + num4;
//                    num14 = yc - num4;
//                    if ( num13 < 0 )
//                    {
//                        num13 = 0;
//                    }
//                    if ( num13 >= pixelHeight )
//                    {
//                        num13 = pixelHeight - 1;
//                    }
//                    if ( num14 < 0 )
//                    {
//                        num14 = 0;
//                    }
//                    if ( num14 >= pixelHeight )
//                    {
//                        num14 = pixelHeight - 1;
//                    }
//                    num15 = num13 * pixelWidth;
//                    num16 = num14 * pixelWidth;
//                    num11 -= num5;
//                    num9 += num8;
//                    num8 += num5;
//                }
//            }
//            for ( int m = 1; m <= num; m++ )
//            {
//                for ( int n = 1; n <= num - m; n++ )
//                {
//                    int num19 = m + xc + num12;
//                    int num20 = yc - n - num12;
//                    if ( num19 >= 0 && num19 < pixelWidth && num20 >= 0 && num20 < pixelHeight )
//                    {
//                        pixels[ num19 + num20 * pixelWidth ] = color;
//                    }
//                    int num21 = -m + xc - num12;
//                    int num22 = num20;
//                    if ( num21 >= 0 && num21 < pixelWidth && num22 >= 0 && num22 < pixelHeight )
//                    {
//                        pixels[ num21 + num22 * pixelWidth ] = color;
//                    }
//                    int num23 = num21;
//                    int num24 = yc + n + num12;
//                    if ( num23 >= 0 && num23 < pixelWidth && num24 >= 0 && num24 < pixelHeight )
//                    {
//                        pixels[ num23 + num24 * pixelWidth ] = color;
//                    }
//                    int num25 = num19;
//                    int num26 = num24;
//                    if ( num25 >= 0 && num25 < pixelWidth && num26 >= 0 && num26 < pixelHeight )
//                    {
//                        pixels[ num25 + num26 * pixelWidth ] = color;
//                    }
//                }
//            }
//        }
//    }

//    internal static void DrawBezier( this WriteableBitmap bmp, int x1, int y1, int cx1, int cy1, int cx2, int cy2, int x2, int y2, Color color )
//    {
//        int num = color.A + 1;
//        int color2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        bmp.DrawBezier( x1, y1, cx1, cy1, cx2, cy2, x2, y2, color2 );
//    }

//    internal static void DrawBezier( this WriteableBitmap bmp, int x1, int y1, int cx1, int cy1, int cx2, int cy2, int x2, int y2, int color )
//    {
//        int num = Math.Min(x1, Math.Min(cx1, Math.Min(cx2, x2)));
//        int num2 = Math.Min(y1, Math.Min(cy1, Math.Min(cy2, y2)));
//        int num3 = Math.Max(x1, Math.Max(cx1, Math.Max(cx2, x2)));
//        int num4 = Math.Max(y1, Math.Max(cy1, Math.Max(cy2, y2)));
//        int num5 = num3 - num;
//        int num6 = num4 - num2;
//        if ( num5 > num6 )
//        {
//            num6 = num5;
//        }
//        if ( num6 != 0 )
//        {
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            using ( BitmapContext context = bmp.GetBitmapContext() )
//            {
//                float num7 = 2f / (float)num6;
//                int x3 = x1;
//                int y3 = y1;
//                for ( float num8 = num7; num8 <= 1f; num8 += num7 )
//                {
//                    float num9 = num8 * num8;
//                    float num10 = 1f - num8;
//                    float num11 = num10 * num10;
//                    int num12 = (int)(num10 * num11 * (float)x1 + 3f * num8 * num11 * (float)cx1 + 3f * num10 * num9 * (float)cx2 + num8 * num9 * (float)x2);
//                    int num13 = (int)(num10 * num11 * (float)y1 + 3f * num8 * num11 * (float)cy1 + 3f * num10 * num9 * (float)cy2 + num8 * num9 * (float)y2);
//                    DrawLine( context, pixelWidth, pixelHeight, x3, y3, num12, num13, color );
//                    x3 = num12;
//                    y3 = num13;
//                }
//                DrawLine( context, pixelWidth, pixelHeight, x3, y3, x2, y2, color );
//            }
//        }
//    }

//    internal static void DrawBeziers( this WriteableBitmap bmp, int[ ] points, Color color )
//    {
//        int num = color.A + 1;
//        int color2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        bmp.DrawBeziers( points, color2 );
//    }

//    internal static void DrawBeziers( this WriteableBitmap bmp, int[ ] points, int color )
//    {
//        int x = points[0];
//        int y = points[1];
//        for ( int i = 2; i + 5 < points.Length; i += 6 )
//        {
//            int num = points[i + 4];
//            int num2 = points[i + 5];
//            bmp.DrawBezier( x, y, points[ i ], points[ i + 1 ], points[ i + 2 ], points[ i + 3 ], num, num2, color );
//            x = num;
//            y = num2;
//        }
//    }

//    private static void DrawCurveSegment( int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, float tension, int color, BitmapContext context, int w, int h )
//    {
//        int num = Math.Min(x1, Math.Min(x2, Math.Min(x3, x4)));
//        int num2 = Math.Min(y1, Math.Min(y2, Math.Min(y3, y4)));
//        int num3 = Math.Max(x1, Math.Max(x2, Math.Max(x3, x4)));
//        int num4 = Math.Max(y1, Math.Max(y2, Math.Max(y3, y4)));
//        int num5 = num3 - num;
//        int num6 = num4 - num2;
//        if ( num5 > num6 )
//        {
//            num6 = num5;
//        }
//        if ( num6 != 0 )
//        {
//            float num7 = 2f / (float)num6;
//            int x5 = x2;
//            int y5 = y2;
//            float num8 = tension * (float)(x3 - x1);
//            float num9 = tension * (float)(y3 - y1);
//            float num10 = tension * (float)(x4 - x2);
//            float num11 = tension * (float)(y4 - y2);
//            float num12 = num8 + num10 + (float)(2 * x2) - (float)(2 * x3);
//            float num13 = num9 + num11 + (float)(2 * y2) - (float)(2 * y3);
//            float num14 = -2f * num8 - num10 - (float)(3 * x2) + (float)(3 * x3);
//            float num15 = -2f * num9 - num11 - (float)(3 * y2) + (float)(3 * y3);
//            for ( float num16 = num7; num16 <= 1f; num16 += num7 )
//            {
//                float num17 = num16 * num16;
//                int num18 = (int)(num12 * num17 * num16 + num14 * num17 + num8 * num16 + (float)x2);
//                int num19 = (int)(num13 * num17 * num16 + num15 * num17 + num9 * num16 + (float)y2);
//                DrawLine( context, w, h, x5, y5, num18, num19, color );
//                x5 = num18;
//                y5 = num19;
//            }
//            DrawLine( context, w, h, x5, y5, x3, y3, color );
//        }
//    }

//    internal static void DrawCurve( this WriteableBitmap bmp, int[ ] points, float tension, Color color )
//    {
//        int num = color.A + 1;
//        int color2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        bmp.DrawCurve( points, tension, color2 );
//    }

//    internal static void DrawCurve( this WriteableBitmap bmp, int[ ] points, float tension, int color )
//    {
//        int pixelWidth = bmp.PixelWidth;
//        int pixelHeight = bmp.PixelHeight;
//        using ( BitmapContext context = bmp.GetBitmapContext() )
//        {
//            DrawCurveSegment( points[ 0 ], points[ 1 ], points[ 0 ], points[ 1 ], points[ 2 ], points[ 3 ], points[ 4 ], points[ 5 ], tension, color, context, pixelWidth, pixelHeight );
//            int i;
//            for ( i = 2; i < points.Length - 4; i += 2 )
//            {
//                DrawCurveSegment( points[ i - 2 ], points[ i - 1 ], points[ i ], points[ i + 1 ], points[ i + 2 ], points[ i + 3 ], points[ i + 4 ], points[ i + 5 ], tension, color, context, pixelWidth, pixelHeight );
//            }
//            DrawCurveSegment( points[ i - 2 ], points[ i - 1 ], points[ i ], points[ i + 1 ], points[ i + 2 ], points[ i + 3 ], points[ i + 2 ], points[ i + 3 ], tension, color, context, pixelWidth, pixelHeight );
//        }
//    }

//    internal static void DrawCurveClosed( this WriteableBitmap bmp, int[ ] points, float tension, Color color )
//    {
//        int num = color.A + 1;
//        int color2 = (color.A << 24) | ((byte)(color.R * num >> 8) << 16) | ((byte)(color.G * num >> 8) << 8) | (byte)(color.B * num >> 8);
//        bmp.DrawCurveClosed( points, tension, color2 );
//    }

//    internal static void DrawCurveClosed( this WriteableBitmap bmp, int[ ] points, float tension, int color )
//    {
//        int pixelWidth = bmp.PixelWidth;
//        int pixelHeight = bmp.PixelHeight;
//        using ( BitmapContext context = bmp.GetBitmapContext() )
//        {
//            int num = points.Length;
//            DrawCurveSegment( points[ num - 2 ], points[ num - 1 ], points[ 0 ], points[ 1 ], points[ 2 ], points[ 3 ], points[ 4 ], points[ 5 ], tension, color, context, pixelWidth, pixelHeight );
//            int i;
//            for ( i = 2; i < num - 4; i += 2 )
//            {
//                DrawCurveSegment( points[ i - 2 ], points[ i - 1 ], points[ i ], points[ i + 1 ], points[ i + 2 ], points[ i + 3 ], points[ i + 4 ], points[ i + 5 ], tension, color, context, pixelWidth, pixelHeight );
//            }
//            DrawCurveSegment( points[ i - 2 ], points[ i - 1 ], points[ i ], points[ i + 1 ], points[ i + 2 ], points[ i + 3 ], points[ 0 ], points[ 1 ], tension, color, context, pixelWidth, pixelHeight );
//            DrawCurveSegment( points[ i ], points[ i + 1 ], points[ i + 2 ], points[ i + 3 ], points[ 0 ], points[ 1 ], points[ 2 ], points[ 3 ], tension, color, context, pixelWidth, pixelHeight );
//        }
//    }

//    internal static WriteableBitmap Resize( this WriteableBitmap bmp, int width, int height, Interpolation interpolation )
//    {
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            int[] array = Resize(bitmapContext, bmp.PixelWidth, bmp.PixelHeight, width, height, interpolation);
//            WriteableBitmap result = BitmapFactory.New(width, height);
//            BitmapContext.BlockCopy( array, 0, bitmapContext, 0, 4 * array.Length );
//            return result;
//        }
//    }

//    internal unsafe static int[ ] Resize( BitmapContext srcContext, int widthSource, int heightSource, int width, int height, Interpolation interpolation )
//    {
//        int* pixels = srcContext.Pixels;
//        int[] array = new int[width * height];
//        float num = (float)widthSource / (float)width;
//        float num2 = (float)heightSource / (float)height;
//        switch ( interpolation )
//        {
//            case Interpolation.NearestNeighbor:
//                {
//                    int num21 = 0;
//                    for ( int k = 0; k < height; k++ )
//                    {
//                        for ( int l = 0; l < width; l++ )
//                        {
//                            float num4 = (float)l * num;
//                            float num5 = (float)k * num2;
//                            int num6 = (int)num4;
//                            int num7 = (int)num5;
//                            array[ num21++ ] = pixels[ num7 * widthSource + num6 ];
//                        }
//                    }
//                    break;
//                }
//            case Interpolation.Bilinear:
//                {
//                    int num3 = 0;
//                    for ( int i = 0; i < height; i++ )
//                    {
//                        for ( int j = 0; j < width; j++ )
//                        {
//                            float num4 = (float)j * num;
//                            float num5 = (float)i * num2;
//                            int num6 = (int)num4;
//                            int num7 = (int)num5;
//                            float num8 = num4 - (float)num6;
//                            float num9 = num5 - (float)num7;
//                            float num10 = 1f - num8;
//                            float num11 = 1f - num9;
//                            int num12 = num6 + 1;
//                            if ( num12 >= widthSource )
//                            {
//                                num12 = num6;
//                            }
//                            int num13 = num7 + 1;
//                            if ( num13 >= heightSource )
//                            {
//                                num13 = num7;
//                            }
//                            int num14 = pixels[num7 * widthSource + num6];
//                            byte b = (byte)(num14 >> 24);
//                            byte b2 = (byte)(num14 >> 16);
//                            byte b3 = (byte)(num14 >> 8);
//                            byte b4 = (byte)num14;
//                            num14 = pixels[ num7 * widthSource + num12 ];
//                            byte b5 = (byte)(num14 >> 24);
//                            byte b6 = (byte)(num14 >> 16);
//                            byte b7 = (byte)(num14 >> 8);
//                            byte b8 = (byte)num14;
//                            num14 = pixels[ num13 * widthSource + num6 ];
//                            byte b9 = (byte)(num14 >> 24);
//                            byte b10 = (byte)(num14 >> 16);
//                            byte b11 = (byte)(num14 >> 8);
//                            byte b12 = (byte)num14;
//                            num14 = pixels[ num13 * widthSource + num12 ];
//                            byte b13 = (byte)(num14 >> 24);
//                            byte b14 = (byte)(num14 >> 16);
//                            byte b15 = (byte)(num14 >> 8);
//                            byte b16 = (byte)num14;
//                            float num15 = num10 * (float)(int)b + num8 * (float)(int)b5;
//                            float num16 = num10 * (float)(int)b9 + num8 * (float)(int)b13;
//                            byte b17 = (byte)(num11 * num15 + num9 * num16);
//                            num15 = num10 * ( float ) ( int ) b2 * ( float ) ( int ) b + num8 * ( float ) ( int ) b6 * ( float ) ( int ) b5;
//                            num16 = num10 * ( float ) ( int ) b10 * ( float ) ( int ) b9 + num8 * ( float ) ( int ) b14 * ( float ) ( int ) b13;
//                            float num17 = num11 * num15 + num9 * num16;
//                            num15 = num10 * ( float ) ( int ) b3 * ( float ) ( int ) b + num8 * ( float ) ( int ) b7 * ( float ) ( int ) b5;
//                            num16 = num10 * ( float ) ( int ) b11 * ( float ) ( int ) b9 + num8 * ( float ) ( int ) b15 * ( float ) ( int ) b13;
//                            float num18 = num11 * num15 + num9 * num16;
//                            num15 = num10 * ( float ) ( int ) b4 * ( float ) ( int ) b + num8 * ( float ) ( int ) b8 * ( float ) ( int ) b5;
//                            num16 = num10 * ( float ) ( int ) b12 * ( float ) ( int ) b9 + num8 * ( float ) ( int ) b16 * ( float ) ( int ) b13;
//                            float num19 = num11 * num15 + num9 * num16;
//                            if ( b17 > 0 )
//                            {
//                                num17 /= ( float ) ( int ) b17;
//                                num18 /= ( float ) ( int ) b17;
//                                num19 /= ( float ) ( int ) b17;
//                            }
//                            byte b18 = (byte)num17;
//                            byte b19 = (byte)num18;
//                            byte b20 = (byte)num19;
//                            array[ num3++ ] = ( ( b17 << 24 ) | ( b18 << 16 ) | ( b19 << 8 ) | b20 );
//                        }
//                    }
//                    break;
//                }
//        }
//        return array;
//    }

//    internal unsafe static WriteableBitmap Rotate( this WriteableBitmap bmp, int angle )
//    {
//        int pixelWidth = bmp.PixelWidth;
//        int pixelHeight = bmp.PixelHeight;
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            int* pixels = bitmapContext.Pixels;
//            int num = 0;
//            WriteableBitmap writeableBitmap = null;
//            angle %= 360;
//            if ( angle > 0 && angle <= 90 )
//            {
//                writeableBitmap = BitmapFactory.New( pixelHeight, pixelWidth );
//                using ( BitmapContext bitmapContext2 = writeableBitmap.GetBitmapContext() )
//                {
//                    int* pixels2 = bitmapContext2.Pixels;
//                    for ( int i = 0; i < pixelWidth; i++ )
//                    {
//                        for ( int num2 = pixelHeight - 1; num2 >= 0; num2-- )
//                        {
//                            int num3 = num2 * pixelWidth + i;
//                            pixels2[ num ] = pixels[ num3 ];
//                            num++;
//                        }
//                    }
//                }
//            }
//            else if ( angle > 90 && angle <= 180 )
//            {
//                writeableBitmap = BitmapFactory.New( pixelWidth, pixelHeight );
//                using ( BitmapContext bitmapContext3 = writeableBitmap.GetBitmapContext() )
//                {
//                    int* pixels3 = bitmapContext3.Pixels;
//                    for ( int num4 = pixelHeight - 1; num4 >= 0; num4-- )
//                    {
//                        for ( int num5 = pixelWidth - 1; num5 >= 0; num5-- )
//                        {
//                            int num6 = num4 * pixelWidth + num5;
//                            pixels3[ num ] = pixels[ num6 ];
//                            num++;
//                        }
//                    }
//                }
//            }
//            else if ( angle > 180 && angle <= 270 )
//            {
//                writeableBitmap = BitmapFactory.New( pixelHeight, pixelWidth );
//                using ( BitmapContext bitmapContext4 = writeableBitmap.GetBitmapContext() )
//                {
//                    int* pixels4 = bitmapContext4.Pixels;
//                    for ( int num7 = pixelWidth - 1; num7 >= 0; num7-- )
//                    {
//                        for ( int j = 0; j < pixelHeight; j++ )
//                        {
//                            int num8 = j * pixelWidth + num7;
//                            pixels4[ num ] = pixels[ num8 ];
//                            num++;
//                        }
//                    }
//                }
//            }
//            else
//            {
//                writeableBitmap = bmp.Clone();
//            }
//            return writeableBitmap;
//        }
//    }

//    internal unsafe static WriteableBitmap RotateFree( this WriteableBitmap bmp, double angle, bool crop = true )
//    {
//        double num = -0.017453292519943295 * angle;
//        int pixelWidth = bmp.PixelWidth;
//        int pixelHeight = bmp.PixelHeight;
//        int num2;
//        int num3;
//        if ( crop )
//        {
//            num2 = pixelWidth;
//            num3 = pixelHeight;
//        }
//        else
//        {
//            double num4 = angle / 57.295779513082323;
//            num2 = ( int ) Math.Ceiling( Math.Abs( Math.Sin( num4 ) * ( double ) pixelHeight ) + Math.Abs( Math.Cos( num4 ) * ( double ) pixelWidth ) );
//            num3 = ( int ) Math.Ceiling( Math.Abs( Math.Sin( num4 ) * ( double ) pixelWidth ) + Math.Abs( Math.Cos( num4 ) * ( double ) pixelHeight ) );
//        }
//        int num5 = pixelWidth / 2;
//        int num6 = pixelHeight / 2;
//        int num7 = num2 / 2;
//        int num8 = num3 / 2;
//        WriteableBitmap writeableBitmap = BitmapFactory.New(num2, num3);
//        using ( BitmapContext bitmapContext = writeableBitmap.GetBitmapContext() )
//        {
//            using ( BitmapContext bitmapContext2 = bmp.GetBitmapContext() )
//            {
//                int* pixels = bitmapContext.Pixels;
//                int* pixels2 = bitmapContext2.Pixels;
//                int pixelWidth2 = bmp.PixelWidth;
//                for ( int i = 0; i < num3; i++ )
//                {
//                    for ( int j = 0; j < num2; j++ )
//                    {
//                        int num9 = j - num7;
//                        int num10 = num8 - i;
//                        double num11 = Math.Sqrt((double)(num9 * num9 + num10 * num10));
//                        double num12;
//                        if ( num9 == 0 )
//                        {
//                            if ( num10 == 0 )
//                            {
//                                pixels[ i * num2 + j ] = pixels2[ num6 * pixelWidth2 + num5 ];
//                                continue;
//                            }
//                            num12 = ( ( num10 >= 0 ) ? 1.5707963267948966 : 4.71238898038469 );
//                        }
//                        else
//                        {
//                            num12 = Math.Atan2( ( double ) num10, ( double ) num9 );
//                        }
//                        num12 -= num;
//                        double num13 = num11 * Math.Cos(num12);
//                        double num14 = num11 * Math.Sin(num12);
//                        num13 += ( double ) num5;
//                        num14 = ( double ) num6 - num14;
//                        int num15 = (int)Math.Floor(num13);
//                        int num16 = (int)Math.Floor(num14);
//                        int num17 = (int)Math.Ceiling(num13);
//                        int num18 = (int)Math.Ceiling(num14);
//                        if ( num15 >= 0 && num17 >= 0 && num15 < pixelWidth && num17 < pixelWidth && num16 >= 0 && num18 >= 0 && num16 < pixelHeight && num18 < pixelHeight )
//                        {
//                            double num19 = num13 - (double)num15;
//                            double num20 = num14 - (double)num16;
//                            Color pixel = bmp.GetPixel(num15, num16);
//                            Color pixel2 = bmp.GetPixel(num17, num16);
//                            Color pixel3 = bmp.GetPixel(num15, num18);
//                            Color pixel4 = bmp.GetPixel(num17, num18);
//                            double num21 = (1.0 - num19) * (double)(int)pixel.R + num19 * (double)(int)pixel2.R;
//                            double num22 = (1.0 - num19) * (double)(int)pixel.G + num19 * (double)(int)pixel2.G;
//                            double num23 = (1.0 - num19) * (double)(int)pixel.B + num19 * (double)(int)pixel2.B;
//                            double num24 = (1.0 - num19) * (double)(int)pixel.A + num19 * (double)(int)pixel2.A;
//                            double num25 = (1.0 - num19) * (double)(int)pixel3.R + num19 * (double)(int)pixel4.R;
//                            double num26 = (1.0 - num19) * (double)(int)pixel3.G + num19 * (double)(int)pixel4.G;
//                            double num27 = (1.0 - num19) * (double)(int)pixel3.B + num19 * (double)(int)pixel4.B;
//                            double num28 = (1.0 - num19) * (double)(int)pixel3.A + num19 * (double)(int)pixel4.A;
//                            int num29 = (int)Math.Round((1.0 - num20) * num21 + num20 * num25);
//                            int num30 = (int)Math.Round((1.0 - num20) * num22 + num20 * num26);
//                            int num31 = (int)Math.Round((1.0 - num20) * num23 + num20 * num27);
//                            int num32 = (int)Math.Round((1.0 - num20) * num24 + num20 * num28);
//                            if ( num29 < 0 )
//                            {
//                                num29 = 0;
//                            }
//                            if ( num29 > 255 )
//                            {
//                                num29 = 255;
//                            }
//                            if ( num30 < 0 )
//                            {
//                                num30 = 0;
//                            }
//                            if ( num30 > 255 )
//                            {
//                                num30 = 255;
//                            }
//                            if ( num31 < 0 )
//                            {
//                                num31 = 0;
//                            }
//                            if ( num31 > 255 )
//                            {
//                                num31 = 255;
//                            }
//                            if ( num32 < 0 )
//                            {
//                                num32 = 0;
//                            }
//                            if ( num32 > 255 )
//                            {
//                                num32 = 255;
//                            }
//                            int num33 = num32 + 1;
//                            pixels[ i * num2 + j ] = ( ( num32 << 24 ) | ( ( byte ) ( num29 * num33 >> 8 ) << 16 ) | ( ( byte ) ( num30 * num33 >> 8 ) << 8 ) | ( byte ) ( num31 * num33 >> 8 ) );
//                        }
//                    }
//                }
//                return writeableBitmap;
//            }
//        }
//    }

//    internal unsafe static WriteableBitmap Flip( this WriteableBitmap bmp, FlipMode flipMode )
//    {
//        int pixelWidth = bmp.PixelWidth;
//        int pixelHeight = bmp.PixelHeight;
//        using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//        {
//            int* pixels = bitmapContext.Pixels;
//            int num = 0;
//            WriteableBitmap writeableBitmap = null;
//            switch ( flipMode )
//            {
//                case FlipMode.Horizontal:
//                    writeableBitmap = BitmapFactory.New( pixelWidth, pixelHeight );
//                    using ( BitmapContext bitmapContext3 = writeableBitmap.GetBitmapContext() )
//                    {
//                        int* pixels3 = bitmapContext3.Pixels;
//                        for ( int num4 = pixelHeight - 1; num4 >= 0; num4-- )
//                        {
//                            for ( int j = 0; j < pixelWidth; j++ )
//                            {
//                                int num5 = num4 * pixelWidth + j;
//                                pixels3[ num ] = pixels[ num5 ];
//                                num++;
//                            }
//                        }
//                    }
//                    break;
//                case FlipMode.Vertical:
//                    writeableBitmap = BitmapFactory.New( pixelWidth, pixelHeight );
//                    using ( BitmapContext bitmapContext2 = writeableBitmap.GetBitmapContext() )
//                    {
//                        int* pixels2 = bitmapContext2.Pixels;
//                        for ( int i = 0; i < pixelHeight; i++ )
//                        {
//                            for ( int num2 = pixelWidth - 1; num2 >= 0; num2-- )
//                            {
//                                int num3 = i * pixelWidth + num2;
//                                pixels2[ num ] = pixels[ num3 ];
//                                num++;
//                            }
//                        }
//                    }
//                    break;
//            }
//            return writeableBitmap;
//        }
//    }
//}


// Decompiled with JetBrains decompiler
// Type: System.Windows.Media.Imaging.WriteableBitmapExtensions
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\StockSharp.Xaml.Charting.dll

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using StockSharp.Xaml.Charting.Common.Extensions;

namespace System.Windows.Media.Imaging
{
    internal static class WriteableBitmapExtensions
    {
        private static readonly int[] leftEdgeX = new int[8192];
        private static readonly int[] rightEdgeX = new int[8192];
        internal static int[,] KernelGaussianBlur5x5 = new int[5, 5]
    {
      {
        1,
        4,
        7,
        4,
        1
      },
      {
        4,
        16,
        26,
        16,
        4
      },
      {
        7,
        26,
        41,
        26,
        7
      },
      {
        4,
        16,
        26,
        16,
        4
      },
      {
        1,
        4,
        7,
        4,
        1
      }
    };
        internal static int[,] KernelGaussianBlur3x3 = new int[3, 3]
    {
      {
        16,
        26,
        16
      },
      {
        26,
        41,
        26
      },
      {
        16,
        26,
        16
      }
    };
        internal static int[,] KernelSharpen3x3 = new int[3, 3]
    {
      {
        0,
        -2,
        0
      },
      {
        -2,
        11,
        -2
      },
      {
        0,
        -2,
        0
      }
    };
        internal const int SizeOfArgb = 4;
        private const int WhiteR = 255;
        private const int WhiteG = 255;
        private const int WhiteB = 255;
        private const byte INSIDE = 0;
        private const byte LEFT = 1;
        private const byte RIGHT = 2;
        private const byte BOTTOM = 4;
        private const byte TOP = 8;
        private const float StepFactor = 2f;

        private static void swap<T>( ref T a, ref T b )
        {
            T obj = a;
            a = b;
            b = obj;
        }

        private static unsafe void AALineQ1( int width, int height, BitmapContext context, int x1, int y1, int x2, int y2, int color, bool minEdge, bool leftEdge )
        {
            byte num1 = 0;
            if ( minEdge )
                num1 = byte.MaxValue;
            if ( x1 == x2 || y1 == y2 )
                return;
            int* pixels = context.Pixels;
            if ( y1 > y2 )
            {
                WriteableBitmapExtensions.swap<int>( ref x1, ref x2 );
                WriteableBitmapExtensions.swap<int>( ref y1, ref y2 );
            }
            int num2 = x2 - x1;
            int num3 = y2 - y1;
            if ( x1 > x2 )
                num2 = x1 - x2;
            int num4 = x1;
            int index = y1;
            ushort num5 = num2 <= num3 ? (ushort) ((num2 << 16) / num3) : (ushort) ((num3 << 16) / num2);
            byte num6 = (byte) (((long) color & 4278190080L) >> 24);
            byte num7 = (byte) ((color & 16711680) >> 16);
            byte num8 = (byte) ((color & 65280) >> 8);
            byte num9 = (byte) (color & (int) byte.MaxValue);
            ushort num10 = 0;
            if ( num2 >= num3 )
            {
                while ( num2-- != 0 )
                {
                    if ( ( int ) ( ushort ) ( ( uint ) num10 + ( uint ) num5 ) <= ( int ) num10 )
                        ++index;
                    num10 += num5;
                    if ( x1 < x2 )
                        ++num4;
                    else
                        --num4;
                    if ( index >= 0 && index < height )
                    {
                        if ( leftEdge )
                            WriteableBitmapExtensions.leftEdgeX[ index ] = Math.Max( num4 + 1, WriteableBitmapExtensions.leftEdgeX[ index ] );
                        else
                            WriteableBitmapExtensions.rightEdgeX[ index ] = Math.Min( num4 - 1, WriteableBitmapExtensions.rightEdgeX[ index ] );
                        if ( num4 >= 0 && num4 < width )
                        {
                            byte num11 = (byte) ((int) num6 * (int) (ushort) ((uint) (ushort) ((uint) num10 >> 8) ^ (uint) num1) >> 8);
                            int num12 = (int) num7;
                            byte num13 = num8;
                            byte num14 = num9;
                            int num15 = pixels[index * width + num4];
                            byte num16 = (byte) ((num15 & 16711680) >> 16);
                            byte num17 = (byte) ((num15 & 65280) >> 8);
                            byte num18 = (byte) (num15 & (int) byte.MaxValue);
                            int num19 = (int) num11;
                            byte num20 = (byte) (num12 * num19 + (int) num16 * ((int) byte.MaxValue - (int) num11) >> 8);
                            byte num21 = (byte) ((int) num13 * (int) num11 + (int) num17 * ((int) byte.MaxValue - (int) num11) >> 8);
                            byte num22 = (byte) ((int) num14 * (int) num11 + (int) num18 * ((int) byte.MaxValue - (int) num11) >> 8);
                            pixels[ index * width + num4 ] = -16777216 | ( int ) num20 << 16 | ( int ) num21 << 8 | ( int ) num22;
                        }
                    }
                }
            }
            else
            {
                byte num11 = (byte) ((uint) num1 ^ (uint) byte.MaxValue);
                while ( --num3 != 0 )
                {
                    if ( ( int ) ( ushort ) ( ( uint ) num10 + ( uint ) num5 ) <= ( int ) num10 )
                    {
                        if ( x1 < x2 )
                            ++num4;
                        else
                            --num4;
                    }
                    num10 += num5;
                    ++index;
                    if ( num4 >= 0 && num4 < width && ( index >= 0 && index < height ) )
                    {
                        byte num12 = (byte) ((int) num6 * (int) (ushort) ((uint) (ushort) ((uint) num10 >> 8) ^ (uint) num11) >> 8);
                        int num13 = (int) num7;
                        byte num14 = num8;
                        byte num15 = num9;
                        int num16 = pixels[index * width + num4];
                        byte num17 = (byte) ((num16 & 16711680) >> 16);
                        byte num18 = (byte) ((num16 & 65280) >> 8);
                        byte num19 = (byte) (num16 & (int) byte.MaxValue);
                        int num20 = (int) num12;
                        byte num21 = (byte) (num13 * num20 + (int) num17 * ((int) byte.MaxValue - (int) num12) >> 8);
                        byte num22 = (byte) ((int) num14 * (int) num12 + (int) num18 * ((int) byte.MaxValue - (int) num12) >> 8);
                        byte num23 = (byte) ((int) num15 * (int) num12 + (int) num19 * ((int) byte.MaxValue - (int) num12) >> 8);
                        pixels[ index * width + num4 ] = -16777216 | ( int ) num21 << 16 | ( int ) num22 << 8 | ( int ) num23;
                        if ( leftEdge )
                            WriteableBitmapExtensions.leftEdgeX[ index ] = num4 + 1;
                        else
                            WriteableBitmapExtensions.rightEdgeX[ index ] = num4 - 1;
                    }
                }
            }
        }

        private static unsafe void AAWidthLine( int width, int height, BitmapContext context, float x1, float y1, float x2, float y2, float lineWidth, int color )
        {
            if ( ( double ) lineWidth <= 0.0 )
                return;
            int* pixels = context.Pixels;
            if ( ( double ) y1 > ( double ) y2 )
            {
                WriteableBitmapExtensions.swap<float>( ref x1, ref x2 );
                WriteableBitmapExtensions.swap<float>( ref y1, ref y2 );
            }
            if ( ( double ) x1 == ( double ) x2 )
            {
                x1 -= ( float ) ( ( int ) lineWidth / 2 );
                x2 += ( float ) ( ( int ) lineWidth / 2 );
                if ( ( double ) x1 < 0.0 )
                    x1 = 0.0f;
                if ( ( double ) x2 < 0.0 || ( double ) x1 >= ( double ) width )
                    return;
                if ( ( double ) x2 >= ( double ) width )
                    x2 = ( float ) ( width - 1 );
                if ( ( double ) y1 >= ( double ) height || ( double ) y2 < 0.0 )
                    return;
                if ( ( double ) y1 < 0.0 )
                    y1 = 0.0f;
                if ( ( double ) y2 >= ( double ) height )
                    y2 = ( float ) ( height - 1 );
                for ( int index1 = ( int ) x1 ; ( double ) index1 <= ( double ) x2 ; ++index1 )
                {
                    for ( int index2 = ( int ) y1 ; ( double ) index2 <= ( double ) y2 ; ++index2 )
                    {
                        byte num1 = (byte) (((long) color & 4278190080L) >> 24);
                        byte num2 = (byte) ((color & 16711680) >> 16);
                        byte num3 = (byte) ((color & 65280) >> 8);
                        int num4 = (int) (byte) (color & (int) byte.MaxValue);
                        byte num5 = num2;
                        byte num6 = num3;
                        int num7 = pixels[index2 * width + index1];
                        byte num8 = (byte) ((num7 & 16711680) >> 16);
                        byte num9 = (byte) ((num7 & 65280) >> 8);
                        byte num10 = (byte) (num7 & (int) byte.MaxValue);
                        byte num11 = (byte) ((int) num5 * (int) num1 + (int) num8 * ((int) byte.MaxValue - (int) num1) >> 8);
                        byte num12 = (byte) ((int) num6 * (int) num1 + (int) num9 * ((int) byte.MaxValue - (int) num1) >> 8);
                        int num13 = (int) num1;
                        byte num14 = (byte) (num4 * num13 + (int) num10 * ((int) byte.MaxValue - (int) num1) >> 8);
                        pixels[ index2 * width + index1 ] = -16777216 | ( int ) num11 << 16 | ( int ) num12 << 8 | ( int ) num14;
                    }
                }
            }
            else if ( ( double ) y1 == ( double ) y2 )
            {
                if ( ( double ) x1 > ( double ) x2 )
                    WriteableBitmapExtensions.swap<float>( ref x1, ref x2 );
                y1 -= ( float ) ( ( int ) lineWidth / 2 );
                y2 += ( float ) ( ( int ) lineWidth / 2 );
                if ( ( double ) y1 < 0.0 )
                    y1 = 0.0f;
                if ( ( double ) y2 < 0.0 || ( double ) y1 >= ( double ) height )
                    return;
                if ( ( double ) y2 >= ( double ) height )
                    x2 = ( float ) ( height - 1 );
                if ( ( double ) x1 >= ( double ) width || ( double ) y2 < 0.0 )
                    return;
                if ( ( double ) x1 < 0.0 )
                    x1 = 0.0f;
                if ( ( double ) x2 >= ( double ) width )
                    x2 = ( float ) ( width - 1 );
                for ( int index1 = ( int ) x1 ; ( double ) index1 <= ( double ) x2 ; ++index1 )
                {
                    for ( int index2 = ( int ) y1 ; ( double ) index2 <= ( double ) y2 ; ++index2 )
                    {
                        byte num1 = (byte) (((long) color & 4278190080L) >> 24);
                        byte num2 = (byte) ((color & 16711680) >> 16);
                        byte num3 = (byte) ((color & 65280) >> 8);
                        int num4 = (int) (byte) (color & (int) byte.MaxValue);
                        byte num5 = num2;
                        byte num6 = num3;
                        int num7 = pixels[index2 * width + index1];
                        byte num8 = (byte) ((num7 & 16711680) >> 16);
                        byte num9 = (byte) ((num7 & 65280) >> 8);
                        byte num10 = (byte) (num7 & (int) byte.MaxValue);
                        byte num11 = (byte) ((int) num5 * (int) num1 + (int) num8 * ((int) byte.MaxValue - (int) num1) >> 8);
                        byte num12 = (byte) ((int) num6 * (int) num1 + (int) num9 * ((int) byte.MaxValue - (int) num1) >> 8);
                        int num13 = (int) num1;
                        byte num14 = (byte) (num4 * num13 + (int) num10 * ((int) byte.MaxValue - (int) num1) >> 8);
                        pixels[ index2 * width + index1 ] = -16777216 | ( int ) num11 << 16 | ( int ) num12 << 8 | ( int ) num14;
                    }
                }
            }
            else
            {
                ++y1;
                ++y2;
                double num1 = ((double) y2 - (double) y1) / ((double) x2 - (double) x1);
                double num2 = ((double) x2 - (double) x1) / ((double) y2 - (double) y1);
                double num3 = (double) lineWidth;
                float num4 = x2 - x1;
                float num5 = y2 - y1;
                float num6 = (float) (num3 * (double) num5 / Math.Sqrt((double) num4 * (double) num4 + (double) num5 * (double) num5));
                float num7 = (float) (num3 * (double) num4 / Math.Sqrt((double) num4 * (double) num4 + (double) num5 * (double) num5));
                double num8 = (double) num4 * (double) num5 / ((double) num4 * (double) num4 + (double) num5 * (double) num5);
                x1 += num6 / 2f;
                y1 -= num7 / 2f;
                x2 += num6 / 2f;
                y2 -= num7 / 2f;
                float num9 = -num6;
                float num10 = num7;
                int num11 = (int) x1;
                int index1 = (int) y1;
                int num12 = (int) x2;
                int index2 = (int) y2;
                int num13 = (int) ((double) x1 + (double) num9);
                int index3 = (int) ((double) y1 + (double) num10);
                int num14 = (int) ((double) x2 + (double) num9);
                int index4 = (int) ((double) y2 + (double) num10);
                if ( ( double ) lineWidth == 2.0 )
                {
                    if ( ( double ) Math.Abs( num5 ) < ( double ) Math.Abs( num4 ) )
                    {
                        if ( ( double ) x1 < ( double ) x2 )
                        {
                            index3 = index1 + 2;
                            index4 = index2 + 2;
                        }
                        else
                        {
                            index1 = index3 + 2;
                            index2 = index4 + 2;
                        }
                    }
                    else
                    {
                        num11 = num13 + 2;
                        num12 = num14 + 2;
                    }
                }
                int num15 = Math.Min(Math.Min(index1, index2), Math.Min(index3, index4));
                int num16 = Math.Max(Math.Max(index1, index2), Math.Max(index3, index4));
                if ( num15 < 0 )
                    num15 = -1;
                if ( num16 >= height )
                    num16 = height + 1;
                for ( int index5 = num15 + 1 ; index5 < num16 - 1 ; ++index5 )
                {
                    WriteableBitmapExtensions.leftEdgeX[ index5 ] = -65536;
                    WriteableBitmapExtensions.rightEdgeX[ index5 ] = 32768;
                }
                WriteableBitmapExtensions.AALineQ1( width, height, context, num11, index1, num12, index2, color, ( double ) num10 > 0.0, false );
                WriteableBitmapExtensions.AALineQ1( width, height, context, num13, index3, num14, index4, color, ( double ) num10 < 0.0, true );
                if ( ( double ) lineWidth > 1.0 )
                {
                    WriteableBitmapExtensions.AALineQ1( width, height, context, num11, index1, num13, index3, color, true, ( double ) num10 > 0.0 );
                    WriteableBitmapExtensions.AALineQ1( width, height, context, num12, index2, num14, index4, color, false, ( double ) num10 < 0.0 );
                }
                if ( ( double ) x1 < ( double ) x2 )
                {
                    if ( index2 >= 0 && index2 < height )
                        WriteableBitmapExtensions.rightEdgeX[ index2 ] = Math.Min( num12, WriteableBitmapExtensions.rightEdgeX[ index2 ] );
                    if ( index3 >= 0 && index3 < height )
                        WriteableBitmapExtensions.leftEdgeX[ index3 ] = Math.Max( num13, WriteableBitmapExtensions.leftEdgeX[ index3 ] );
                }
                else
                {
                    if ( index1 >= 0 && index1 < height )
                        WriteableBitmapExtensions.rightEdgeX[ index1 ] = Math.Min( num11, WriteableBitmapExtensions.rightEdgeX[ index1 ] );
                    if ( index4 >= 0 && index4 < height )
                        WriteableBitmapExtensions.leftEdgeX[ index4 ] = Math.Max( num14, WriteableBitmapExtensions.leftEdgeX[ index4 ] );
                }
                for ( int index5 = num15 + 1 ; index5 < num16 - 1 ; ++index5 )
                {
                    WriteableBitmapExtensions.leftEdgeX[ index5 ] = Math.Max( WriteableBitmapExtensions.leftEdgeX[ index5 ], 0 );
                    WriteableBitmapExtensions.rightEdgeX[ index5 ] = Math.Min( WriteableBitmapExtensions.rightEdgeX[ index5 ], width - 1 );
                    for ( int index6 = WriteableBitmapExtensions.leftEdgeX[ index5 ] ; index6 <= WriteableBitmapExtensions.rightEdgeX[ index5 ] ; ++index6 )
                    {
                        byte num17 = (byte) (((long) color & 4278190080L) >> 24);
                        byte num18 = (byte) ((color & 16711680) >> 16);
                        byte num19 = (byte) ((color & 65280) >> 8);
                        int num20 = (int) (byte) (color & (int) byte.MaxValue);
                        byte num21 = num18;
                        byte num22 = num19;
                        int num23 = pixels[index5 * width + index6];
                        byte num24 = (byte) ((num23 & 16711680) >> 16);
                        byte num25 = (byte) ((num23 & 65280) >> 8);
                        byte num26 = (byte) (num23 & (int) byte.MaxValue);
                        byte num27 = (byte) ((int) num21 * (int) num17 + (int) num24 * ((int) byte.MaxValue - (int) num17) >> 8);
                        byte num28 = (byte) ((int) num22 * (int) num17 + (int) num25 * ((int) byte.MaxValue - (int) num17) >> 8);
                        int num29 = (int) num17;
                        byte num30 = (byte) (num20 * num29 + (int) num26 * ((int) byte.MaxValue - (int) num17) >> 8);
                        pixels[ index5 * width + index6 ] = -16777216 | ( int ) num27 << 16 | ( int ) num28 << 8 | ( int ) num30;
                    }
                }
            }
        }

        internal static void DrawLineAA( BitmapContext context, int pixelWidth, int pixelHeight, int x1, int y1, int x2, int y2, int color, int strokeThickness )
        {
            WriteableBitmapExtensions.AAWidthLine( pixelWidth, pixelHeight, context, ( float ) x1, ( float ) y1, ( float ) x2, ( float ) y2, ( float ) strokeThickness, color );
        }

        internal static void DrawLineAA( BitmapContext context, int pixelWidth, int pixelHeight, int x1, int y1, int x2, int y2, Color color, int strokeThickness )
        {
            int num = (int) color.A + 1;
            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
            WriteableBitmapExtensions.AAWidthLine( pixelWidth, pixelHeight, context, ( float ) x1, ( float ) y1, ( float ) x2, ( float ) y2, ( float ) strokeThickness, color1 );
        }

        internal static void DrawLineAA( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Color color, int strokeThickness )
        {
            int num = (int) color.A + 1;
            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
                WriteableBitmapExtensions.AAWidthLine( bmp.PixelWidth, bmp.PixelHeight, bitmapContext, ( float ) x1, ( float ) y1, ( float ) x2, ( float ) y2, ( float ) strokeThickness, color1 );
        }

        internal static int ConvertColor( double opacity, Color color )
        {
            if ( opacity < 0.0 || opacity > 1.0 )
                throw new ArgumentOutOfRangeException( nameof( opacity ), "Opacity must be between 0.0 and 1.0" );
            color.A = ( byte ) ( ( double ) color.A * opacity );
            return WriteableBitmapExtensions.ConvertColor( color );
        }

        internal static int ConvertColor( Color color )
        {
            if ( color.A == ( byte ) 0 )
                return 0;
            int num = (int) color.A + 1;
            return ( int ) color.A << 24 | ( int ) ( byte ) ( ( int ) color.R * num >> 8 ) << 16 | ( int ) ( byte ) ( ( int ) color.G * num >> 8 ) << 8 | ( int ) ( byte ) ( ( int ) color.B * num >> 8 );
        }

        internal static unsafe void Clear( this WriteableBitmap bmp, Color color )
        {
            int num1 = (int) color.A + 1;
            int num2 = (int) color.A << 24 | (int) (byte) ((int) color.R * num1 >> 8) << 16 | (int) (byte) ((int) color.G * num1 >> 8) << 8 | (int) (byte) ((int) color.B * num1 >> 8);
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                int* pixels = bitmapContext.Pixels;
                int pixelWidth = bmp.PixelWidth;
                int pixelHeight = bmp.PixelHeight;
                int num3 = pixelWidth * 4;
                for ( int index = 0 ; index < pixelWidth ; ++index )
                    pixels[ index ] = num2;
                int num4 = 1;
                int num5 = 1;
                while ( num5 < pixelHeight )
                {
                    BitmapContext.BlockCopy( bitmapContext, 0, bitmapContext, num5 * num3, num4 * num3 );
                    num5 += num4;
                    num4 = Math.Min( 2 * num4, pixelHeight - num5 );
                }
            }
        }

        internal static void Clear( this WriteableBitmap bmp )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
                bitmapContext.Clear();
        }

        internal static WriteableBitmap Clone( this WriteableBitmap bmp )
        {
            WriteableBitmap bmp1 = BitmapFactory.New(bmp.PixelWidth, bmp.PixelHeight);
            using ( BitmapContext bitmapContext1 = bmp.GetBitmapContext( ReadWriteMode.ReadOnly ) )
            {
                using ( BitmapContext bitmapContext2 = bmp1.GetBitmapContext() )
                    BitmapContext.BlockCopy( bitmapContext1, 0, bitmapContext2, 0, bitmapContext1.Length * 4 );
            }
            return bmp1;
        }

        internal static unsafe void ForEach( this WriteableBitmap bmp, Func<int, int, Color> func )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                int* pixels = bitmapContext.Pixels;
                int pixelWidth = bmp.PixelWidth;
                int pixelHeight = bmp.PixelHeight;
                int num1 = 0;
                for ( int index1 = 0 ; index1 < pixelHeight ; ++index1 )
                {
                    for ( int index2 = 0 ; index2 < pixelWidth ; ++index2 )
                    {
                        Color color = func(index2, index1);
                        int num2 = (int) color.A + 1;
                        pixels[ num1++ ] = ( int ) color.A << 24 | ( int ) ( byte ) ( ( int ) color.R * num2 >> 8 ) << 16 | ( int ) ( byte ) ( ( int ) color.G * num2 >> 8 ) << 8 | ( int ) ( byte ) ( ( int ) color.B * num2 >> 8 );
                    }
                }
            }
        }

        internal static unsafe void ForEach( this WriteableBitmap bmp, Func<int, int, Color, Color> func )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                int* pixels = bitmapContext.Pixels;
                int pixelWidth = bmp.PixelWidth;
                int pixelHeight = bmp.PixelHeight;
                int index1 = 0;
                for ( int index2 = 0 ; index2 < pixelHeight ; ++index2 )
                {
                    for ( int index3 = 0 ; index3 < pixelWidth ; ++index3 )
                    {
                        int num1 = pixels[index1];
                        Color color = func(index3, index2, Color.FromArgb((byte) (num1 >> 24), (byte) (num1 >> 16), (byte) (num1 >> 8), (byte) num1));
                        int num2 = (int) color.A + 1;
                        pixels[ index1++ ] = ( int ) color.A << 24 | ( int ) ( byte ) ( ( int ) color.R * num2 >> 8 ) << 16 | ( int ) ( byte ) ( ( int ) color.G * num2 >> 8 ) << 8 | ( int ) ( byte ) ( ( int ) color.B * num2 >> 8 );
                    }
                }
            }
        }

        internal static unsafe int GetPixeli( this WriteableBitmap bmp, int x, int y )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
                return bitmapContext.Pixels[ y * bmp.PixelWidth + x ];
        }

        internal static unsafe Color GetPixel( this WriteableBitmap bmp, int x, int y )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                int num1 = bitmapContext.Pixels[y * bmp.PixelWidth + x];
                int num2;
                int num3 = num2 = (int) (byte) (num1 >> 24);
                if ( num3 == 0 )
                    num3 = 1;
                int num4 = 65280 / num3;
                int num5 = (int) (byte) ((num1 >> 16 & (int) byte.MaxValue) * num4 >> 8);
                int num6 = (int) (byte) ((num1 >> 8 & (int) byte.MaxValue) * num4 >> 8);
                int num7 = (int) (byte) ((num1 & (int) byte.MaxValue) * num4 >> 8);
                return Color.FromArgb( ( byte ) num2, ( byte ) num5, ( byte ) num6, ( byte ) num7 );
            }
        }

        internal static unsafe byte GetBrightness( this WriteableBitmap bmp, int x, int y )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext( ReadWriteMode.ReadOnly ) )
            {
                int num = bitmapContext.Pixels[y * bmp.PixelWidth + x];
                return ( byte ) ( ( int ) ( byte ) ( num >> 16 ) * 6966 + ( int ) ( byte ) ( num >> 8 ) * 23436 + ( int ) ( byte ) num * 2366 >> 15 );
            }
        }

        internal static unsafe void SetPixeli( this WriteableBitmap bmp, int index, byte r, byte g, byte b )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
                bitmapContext.Pixels[ index ] = -16777216 | ( int ) r << 16 | ( int ) g << 8 | ( int ) b;
        }

        internal static unsafe void SetPixel( this WriteableBitmap bmp, int x, int y, byte r, byte g, byte b )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
                bitmapContext.Pixels[ y * bmp.PixelWidth + x ] = -16777216 | ( int ) r << 16 | ( int ) g << 8 | ( int ) b;
        }

        internal static unsafe void SetPixeli( this WriteableBitmap bmp, int index, byte a, byte r, byte g, byte b )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
                bitmapContext.Pixels[ index ] = ( int ) a << 24 | ( int ) r << 16 | ( int ) g << 8 | ( int ) b;
        }

        internal static unsafe void SetPixel( this WriteableBitmap bmp, int x, int y, byte a, byte r, byte g, byte b )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
                bitmapContext.Pixels[ y * bmp.PixelWidth + x ] = ( int ) a << 24 | ( int ) r << 16 | ( int ) g << 8 | ( int ) b;
        }

        internal static unsafe void SetPixeli( this WriteableBitmap bmp, int index, Color color )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                int num = (int) color.A + 1;
                bitmapContext.Pixels[ index ] = ( int ) color.A << 24 | ( int ) ( byte ) ( ( int ) color.R * num >> 8 ) << 16 | ( int ) ( byte ) ( ( int ) color.G * num >> 8 ) << 8 | ( int ) ( byte ) ( ( int ) color.B * num >> 8 );
            }
        }

        internal static unsafe void SetPixel( this WriteableBitmap bmp, int x, int y, Color color )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                int num = (int) color.A + 1;
                bitmapContext.Pixels[ y * bmp.PixelWidth + x ] = ( int ) color.A << 24 | ( int ) ( byte ) ( ( int ) color.R * num >> 8 ) << 16 | ( int ) ( byte ) ( ( int ) color.G * num >> 8 ) << 8 | ( int ) ( byte ) ( ( int ) color.B * num >> 8 );
            }
        }

        internal static unsafe void SetPixeli( this WriteableBitmap bmp, int index, byte a, Color color )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                int num = (int) a + 1;
                bitmapContext.Pixels[ index ] = ( int ) a << 24 | ( int ) ( byte ) ( ( int ) color.R * num >> 8 ) << 16 | ( int ) ( byte ) ( ( int ) color.G * num >> 8 ) << 8 | ( int ) ( byte ) ( ( int ) color.B * num >> 8 );
            }
        }

        internal static unsafe void SetPixel( this WriteableBitmap bmp, int x, int y, byte a, Color color )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                int num = (int) a + 1;
                bitmapContext.Pixels[ y * bmp.PixelWidth + x ] = ( int ) a << 24 | ( int ) ( byte ) ( ( int ) color.R * num >> 8 ) << 16 | ( int ) ( byte ) ( ( int ) color.G * num >> 8 ) << 8 | ( int ) ( byte ) ( ( int ) color.B * num >> 8 );
            }
        }

        internal static unsafe void SetPixeli( this WriteableBitmap bmp, int index, int color )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
                bitmapContext.Pixels[ index ] = color;
        }

        internal static unsafe void SetPixel( this WriteableBitmap bmp, int x, int y, int color )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
                bitmapContext.Pixels[ y * bmp.PixelWidth + x ] = color;
        }

        internal static unsafe void DrawPixelsVertically( this WriteableBitmap bmp, int x, int yStartBottom, int yEndTop, IList<int> pixelColorsArgb, double opacity, bool yAxisIsFlipped )
        {
            int num1 = Math.Max(yStartBottom, yEndTop);
            yEndTop = Math.Min( yStartBottom, yEndTop );
            yStartBottom = num1;
            int pixelWidth = bmp.PixelWidth;
            int pixelHeight = bmp.PixelHeight;
            if ( yStartBottom == yEndTop )
                return;
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                int* pixels = bitmapContext.Pixels;
                int num2 = Math.Min(yStartBottom, pixelHeight);
                int index1 = x + num2 * pixelWidth;
                int num3 = num2;
                while ( num3 >= yEndTop && num3 >= 0 )
                {
                    if ( num3 >= 0 && num3 < pixelHeight )
                    {
                        int index2 = (yStartBottom - num3) * pixelColorsArgb.Count / (yStartBottom - yEndTop);
                        if ( yAxisIsFlipped )
                            index2 = pixelColorsArgb.Count - 1 - index2;
                        if ( index2 >= 0 && index2 < pixelColorsArgb.Count )
                        {
                            int num4 = pixelColorsArgb[index2];
                            int num5 = (int) ((double) (num4 >> 24 & (int) byte.MaxValue) * opacity);
                            if ( num5 == ( int ) byte.MaxValue )
                                pixels[ index1 ] = num4;
                            else if ( num5 > 0 )
                            {
                                int num6 = bitmapContext.Pixels[index1];
                                int num7 = num6 >> 24 & (int) byte.MaxValue;
                                int num8 = num6 >> 16 & (int) byte.MaxValue;
                                int num9 = num6 >> 8 & (int) byte.MaxValue;
                                int num10 = num6 & (int) byte.MaxValue;
                                int num11 = num4 >> 16 & (int) byte.MaxValue;
                                int num12 = num4 >> 8 & (int) byte.MaxValue;
                                int num13 = num4 & (int) byte.MaxValue;
                                int num14 = num5;
                                int num15 = num11 * num14 / (int) byte.MaxValue + num8 * num7 * ((int) byte.MaxValue - num5) / 65025;
                                int num16 = num12 * num5 / (int) byte.MaxValue + num9 * num7 * ((int) byte.MaxValue - num5) / 65025;
                                int num17 = num13 * num5 / (int) byte.MaxValue + num10 * num7 * ((int) byte.MaxValue - num5) / 65025;
                                int num18 = num5 + num7 * ((int) byte.MaxValue - num5) / (int) byte.MaxValue;
                                bitmapContext.Pixels[ index1 ] = ( num18 << 24 ) + ( num15 << 16 ) + ( num16 << 8 ) + num17;
                            }
                        }
                    }
                    --num3;
                    index1 -= pixelWidth;
                }
            }
        }

        internal static void Blit( this WriteableBitmap bmp, Rect destRect, WriteableBitmap source, Rect sourceRect, WriteableBitmapExtensions.BlendMode BlendMode )
        {
            bmp.Blit( destRect, source, sourceRect, Colors.White, BlendMode );
        }

        internal static void Blit( this WriteableBitmap bmp, Rect destRect, WriteableBitmap source, Rect sourceRect )
        {
            bmp.Blit( destRect, source, sourceRect, Colors.White, WriteableBitmapExtensions.BlendMode.Alpha );
        }

        internal static void Blit( this WriteableBitmap bmp, Point destPosition, WriteableBitmap source, Rect sourceRect, Color color, WriteableBitmapExtensions.BlendMode BlendMode )
        {
            Rect destRect = new Rect(destPosition, new Size(sourceRect.Width, sourceRect.Height));
            bmp.Blit( destRect, source, sourceRect, color, BlendMode );
        }

        internal static unsafe void Blit( this WriteableBitmap bmp, Rect destRect, WriteableBitmap source, Rect sourceRect, Color color, WriteableBitmapExtensions.BlendMode BlendMode )
        {
            if ( color.A == ( byte ) 0 )
                return;
            int width1 = (int) destRect.Width;
            int height = (int) destRect.Height;
            int pixelWidth1 = bmp.PixelWidth;
            int pixelHeight = bmp.PixelHeight;
            Rect rect = new Rect(0.0, 0.0, (double) pixelWidth1, (double) pixelHeight);
            rect.Intersect( destRect );
            if ( rect.IsEmpty )
                return;
            int pixelWidth2 = source.PixelWidth;
            using ( BitmapContext bitmapContext1 = source.GetBitmapContext() )
            {
                using ( BitmapContext bitmapContext2 = bmp.GetBitmapContext() )
                {
                    int* pixels1 = bitmapContext1.Pixels;
                    int* pixels2 = bitmapContext2.Pixels;
                    int length1 = bitmapContext1.Length;
                    int length2 = bitmapContext2.Length;
                    int x1 = (int) destRect.X;
                    int y1 = (int) destRect.Y;
                    int num1 = 0;
                    int num2 = 0;
                    int num3 = 0;
                    int num4 = 0;
                    int a = (int) color.A;
                    int r = (int) color.R;
                    int g = (int) color.G;
                    int b = (int) color.B;
                    bool flag = color != Colors.White;
                    int width2 = (int) sourceRect.Width;
                    double num5 = sourceRect.Width / destRect.Width;
                    double num6 = sourceRect.Height / destRect.Height;
                    int x2 = (int) sourceRect.X;
                    int y2 = (int) sourceRect.Y;
                    int num7 = -1;
                    int num8 = -1;
                    double num9 = (double) y2;
                    int num10 = y1;
                    for ( int index1 = 0 ; index1 < height ; ++index1 )
                    {
                        if ( num10 >= 0 && num10 < pixelHeight )
                        {
                            double num11 = (double) x2;
                            int index2 = x1 + num10 * pixelWidth1;
                            int num12 = x1;
                            int num13 = *pixels1;
                            if ( BlendMode == WriteableBitmapExtensions.BlendMode.None && !flag )
                            {
                                int num14 = (int) num11 + (int) num9 * pixelWidth2;
                                int num15 = num12 < 0 ? -num12 : 0;
                                int num16 = num12 + num15;
                                int num17 = pixelWidth2 - num15;
                                int num18 = num16 + num17 < pixelWidth1 ? num17 : pixelWidth1 - num16;
                                if ( num18 > width2 )
                                    num18 = width2;
                                if ( num18 > width1 )
                                    num18 = width1;
                                BitmapContext.BlockCopy( bitmapContext1, ( num14 + num15 ) * 4, bitmapContext2, ( index2 + num15 ) * 4, num18 * 4 );
                            }
                            else
                            {
                                for ( int index3 = 0 ; index3 < width1 ; ++index3 )
                                {
                                    if ( num12 >= 0 && num12 < pixelWidth1 )
                                    {
                                        if ( ( int ) num11 != num7 || ( int ) num9 != num8 )
                                        {
                                            int index4 = (int) num11 + (int) num9 * pixelWidth2;
                                            if ( index4 >= 0 && index4 < length1 )
                                            {
                                                num13 = pixels1[ index4 ];
                                                num4 = num13 >> 24 & ( int ) byte.MaxValue;
                                                num1 = num13 >> 16 & ( int ) byte.MaxValue;
                                                num2 = num13 >> 8 & ( int ) byte.MaxValue;
                                                num3 = num13 & ( int ) byte.MaxValue;
                                                if ( flag && num4 != 0 )
                                                {
                                                    num4 = num4 * a * 32897 >> 23;
                                                    num1 = ( num1 * r * 32897 >> 23 ) * a * 32897 >> 23;
                                                    num2 = ( num2 * g * 32897 >> 23 ) * a * 32897 >> 23;
                                                    num3 = ( num3 * b * 32897 >> 23 ) * a * 32897 >> 23;
                                                    num13 = num4 << 24 | num1 << 16 | num2 << 8 | num3;
                                                }
                                            }
                                            else
                                                num4 = 0;
                                        }
                                        switch ( BlendMode )
                                        {
                                            case WriteableBitmapExtensions.BlendMode.Mask:
                                                int num14 = pixels2[index2];
                                                int num15 = num14 >> 24 & (int) byte.MaxValue;
                                                int num16 = num14 >> 16 & (int) byte.MaxValue;
                                                int num17 = num14 >> 8 & (int) byte.MaxValue;
                                                int num18 = num14 & (int) byte.MaxValue;
                                                int num19 = num15 * num4 * 32897 >> 23 << 24 | num16 * num4 * 32897 >> 23 << 16 | num17 * num4 * 32897 >> 23 << 8 | num18 * num4 * 32897 >> 23;
                                                pixels2[ index2 ] = num19;
                                                break;
                                            case WriteableBitmapExtensions.BlendMode.ColorKeying:
                                                num1 = num13 >> 16 & ( int ) byte.MaxValue;
                                                num2 = num13 >> 8 & ( int ) byte.MaxValue;
                                                num3 = num13 & ( int ) byte.MaxValue;
                                                if ( num1 != ( int ) color.R || num2 != ( int ) color.G || num3 != ( int ) color.B )
                                                {
                                                    pixels2[ index2 ] = num13;
                                                    break;
                                                }
                                                break;
                                            case WriteableBitmapExtensions.BlendMode.None:
                                                pixels2[ index2 ] = num13;
                                                break;
                                            default:
                                                if ( num4 > 0 )
                                                {
                                                    int num20 = pixels2[index2];
                                                    int num21 = num20 >> 24 & (int) byte.MaxValue;
                                                    if ( ( num4 == ( int ) byte.MaxValue || num21 == 0 ) && ( BlendMode != WriteableBitmapExtensions.BlendMode.Additive && BlendMode != WriteableBitmapExtensions.BlendMode.Subtractive ) && BlendMode != WriteableBitmapExtensions.BlendMode.Multiply )
                                                    {
                                                        pixels2[ index2 ] = num13;
                                                        break;
                                                    }
                                                    int num22 = num20 >> 16 & (int) byte.MaxValue;
                                                    int num23 = num20 >> 8 & (int) byte.MaxValue;
                                                    int num24 = num20 & (int) byte.MaxValue;
                                                    switch ( BlendMode )
                                                    {
                                                        case WriteableBitmapExtensions.BlendMode.Alpha:
                                                            num20 = num4 + ( num21 * ( ( int ) byte.MaxValue - num4 ) * 32897 >> 23 ) << 24 | num1 + ( num22 * ( ( int ) byte.MaxValue - num4 ) * 32897 >> 23 ) << 16 | num2 + ( num23 * ( ( int ) byte.MaxValue - num4 ) * 32897 >> 23 ) << 8 | num3 + ( num24 * ( ( int ) byte.MaxValue - num4 ) * 32897 >> 23 );
                                                            break;
                                                        case WriteableBitmapExtensions.BlendMode.Additive:
                                                            int num25 = (int) byte.MaxValue <= num4 + num21 ? (int) byte.MaxValue : num4 + num21;
                                                            num20 = num25 << 24 | ( num25 <= num1 + num22 ? num25 : num1 + num22 ) << 16 | ( num25 <= num2 + num23 ? num25 : num2 + num23 ) << 8 | ( num25 <= num3 + num24 ? num25 : num3 + num24 );
                                                            break;
                                                        case WriteableBitmapExtensions.BlendMode.Subtractive:
                                                            num20 = num21 << 24 | ( num1 >= num22 ? 0 : num1 - num22 ) << 16 | ( num2 >= num23 ? 0 : num2 - num23 ) << 8 | ( num3 >= num24 ? 0 : num3 - num24 );
                                                            break;
                                                        case WriteableBitmapExtensions.BlendMode.Multiply:
                                                            int num26 = num4 * num21 + 128;
                                                            int num27 = num1 * num22 + 128;
                                                            int num28 = num2 * num23 + 128;
                                                            int num29 = num3 * num24 + 128;
                                                            int num30 = (num26 >> 8) + num26 >> 8;
                                                            int num31 = (num27 >> 8) + num27 >> 8;
                                                            int num32 = (num28 >> 8) + num28 >> 8;
                                                            int num33 = (num29 >> 8) + num29 >> 8;
                                                            num20 = num30 << 24 | ( num30 <= num31 ? num30 : num31 ) << 16 | ( num30 <= num32 ? num30 : num32 ) << 8 | ( num30 <= num33 ? num30 : num33 );
                                                            break;
                                                    }
                                                    pixels2[ index2 ] = num20;
                                                    break;
                                                }
                                                break;
                                        }
                                    }
                                    ++num12;
                                    ++index2;
                                    num11 += num5;
                                }
                            }
                        }
                        num9 += num6;
                        ++num10;
                    }
                }
            }
        }

        internal static unsafe void Blit( BitmapContext destContext, int dpw, int dph, Rect destRect, BitmapContext srcContext, Rect sourceRect, int sourceWidth )
        {
            WriteableBitmapExtensions.BlendMode blendMode = WriteableBitmapExtensions.BlendMode.Alpha;
            int width1 = (int) destRect.Width;
            int height = (int) destRect.Height;
            Rect rect = new Rect(0.0, 0.0, (double) dpw, (double) dph);
            rect.Intersect( destRect );
            if ( rect.IsEmpty )
                return;
            int* pixels1 = srcContext.Pixels;
            int* pixels2 = destContext.Pixels;
            int length = srcContext.Length;
            int x1 = (int) destRect.X;
            int y1 = (int) destRect.Y;
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int width2 = (int) sourceRect.Width;
            double num5 = sourceRect.Width / destRect.Width;
            double num6 = sourceRect.Height / destRect.Height;
            int x2 = (int) sourceRect.X;
            int y2 = (int) sourceRect.Y;
            int num7 = -1;
            int num8 = -1;
            double num9 = (double) y2;
            int num10 = y1;
            for ( int index1 = 0 ; index1 < height ; ++index1 )
            {
                if ( num10 >= 0 && num10 < dph )
                {
                    double num11 = (double) x2;
                    int index2 = x1 + num10 * dpw;
                    int num12 = x1;
                    int num13 = *pixels1;
                    if ( blendMode == WriteableBitmapExtensions.BlendMode.None )
                    {
                        int num14 = (int) num11 + (int) num9 * sourceWidth;
                        int num15 = num12 < 0 ? -num12 : 0;
                        int num16 = num12 + num15;
                        int num17 = sourceWidth - num15;
                        int num18 = num16 + num17 < dpw ? num17 : dpw - num16;
                        if ( num18 > width2 )
                            num18 = width2;
                        if ( num18 > width1 )
                            num18 = width1;
                        BitmapContext.BlockCopy( srcContext, ( num14 + num15 ) * 4, destContext, ( index2 + num15 ) * 4, num18 * 4 );
                    }
                    else
                    {
                        for ( int index3 = 0 ; index3 < width1 ; ++index3 )
                        {
                            if ( num12 >= 0 && num12 < dpw )
                            {
                                if ( ( int ) num11 != num7 || ( int ) num9 != num8 )
                                {
                                    int index4 = (int) num11 + (int) num9 * sourceWidth;
                                    if ( index4 >= 0 && index4 < length )
                                    {
                                        num13 = pixels1[ index4 ];
                                        num4 = num13 >> 24 & ( int ) byte.MaxValue;
                                        num1 = num13 >> 16 & ( int ) byte.MaxValue;
                                        num2 = num13 >> 8 & ( int ) byte.MaxValue;
                                        num3 = num13 & ( int ) byte.MaxValue;
                                    }
                                    else
                                        num4 = 0;
                                }
                                switch ( blendMode )
                                {
                                    case WriteableBitmapExtensions.BlendMode.Mask:
                                        int num14 = pixels2[index2];
                                        int num15 = num14 >> 24 & (int) byte.MaxValue;
                                        int num16 = num14 >> 16 & (int) byte.MaxValue;
                                        int num17 = num14 >> 8 & (int) byte.MaxValue;
                                        int num18 = num14 & (int) byte.MaxValue;
                                        int num19 = num15 * num4 * 32897 >> 23 << 24 | num16 * num4 * 32897 >> 23 << 16 | num17 * num4 * 32897 >> 23 << 8 | num18 * num4 * 32897 >> 23;
                                        pixels2[ index2 ] = num19;
                                        break;
                                    case WriteableBitmapExtensions.BlendMode.ColorKeying:
                                        num1 = num13 >> 16 & ( int ) byte.MaxValue;
                                        num2 = num13 >> 8 & ( int ) byte.MaxValue;
                                        num3 = num13 & ( int ) byte.MaxValue;
                                        if ( num1 != ( int ) byte.MaxValue || num2 != ( int ) byte.MaxValue || num3 != ( int ) byte.MaxValue )
                                        {
                                            pixels2[ index2 ] = num13;
                                            break;
                                        }
                                        break;
                                    case WriteableBitmapExtensions.BlendMode.None:
                                        pixels2[ index2 ] = num13;
                                        break;
                                    default:
                                        if ( num4 > 0 )
                                        {
                                            int num20 = pixels2[index2];
                                            int num21 = num20 >> 24 & (int) byte.MaxValue;
                                            if ( ( num4 == ( int ) byte.MaxValue || num21 == 0 ) && ( blendMode != WriteableBitmapExtensions.BlendMode.Additive && blendMode != WriteableBitmapExtensions.BlendMode.Subtractive ) && blendMode != WriteableBitmapExtensions.BlendMode.Multiply )
                                            {
                                                pixels2[ index2 ] = num13;
                                                break;
                                            }
                                            int num22 = num20 >> 16 & (int) byte.MaxValue;
                                            int num23 = num20 >> 8 & (int) byte.MaxValue;
                                            int num24 = num20 & (int) byte.MaxValue;
                                            switch ( blendMode )
                                            {
                                                case WriteableBitmapExtensions.BlendMode.Alpha:
                                                    num20 = num4 + ( num21 * ( ( int ) byte.MaxValue - num4 ) * 32897 >> 23 ) << 24 | num1 + ( num22 * ( ( int ) byte.MaxValue - num4 ) * 32897 >> 23 ) << 16 | num2 + ( num23 * ( ( int ) byte.MaxValue - num4 ) * 32897 >> 23 ) << 8 | num3 + ( num24 * ( ( int ) byte.MaxValue - num4 ) * 32897 >> 23 );
                                                    break;
                                                case WriteableBitmapExtensions.BlendMode.Additive:
                                                    int num25 = (int) byte.MaxValue <= num4 + num21 ? (int) byte.MaxValue : num4 + num21;
                                                    num20 = num25 << 24 | ( num25 <= num1 + num22 ? num25 : num1 + num22 ) << 16 | ( num25 <= num2 + num23 ? num25 : num2 + num23 ) << 8 | ( num25 <= num3 + num24 ? num25 : num3 + num24 );
                                                    break;
                                                case WriteableBitmapExtensions.BlendMode.Subtractive:
                                                    num20 = num21 << 24 | ( num1 >= num22 ? 0 : num1 - num22 ) << 16 | ( num2 >= num23 ? 0 : num2 - num23 ) << 8 | ( num3 >= num24 ? 0 : num3 - num24 );
                                                    break;
                                                case WriteableBitmapExtensions.BlendMode.Multiply:
                                                    int num26 = num4 * num21 + 128;
                                                    int num27 = num1 * num22 + 128;
                                                    int num28 = num2 * num23 + 128;
                                                    int num29 = num3 * num24 + 128;
                                                    int num30 = (num26 >> 8) + num26 >> 8;
                                                    int num31 = (num27 >> 8) + num27 >> 8;
                                                    int num32 = (num28 >> 8) + num28 >> 8;
                                                    int num33 = (num29 >> 8) + num29 >> 8;
                                                    num20 = num30 << 24 | ( num30 <= num31 ? num30 : num31 ) << 16 | ( num30 <= num32 ? num30 : num32 ) << 8 | ( num30 <= num33 ? num30 : num33 );
                                                    break;
                                            }
                                            pixels2[ index2 ] = num20;
                                            break;
                                        }
                                        break;
                                }
                            }
                            ++num12;
                            ++index2;
                            num11 += num5;
                        }
                    }
                }
                num9 += num6;
                ++num10;
            }
        }

        internal static byte[ ] ToByteArray( this WriteableBitmap bmp, int offset, int count )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                if ( count == -1 )
                    count = bitmapContext.Length;
                int count1 = count * 4;
                byte[] dest = new byte[count1];
                BitmapContext.BlockCopy( bitmapContext, offset, dest, 0, count1 );
                return dest;
            }
        }

        internal static byte[ ] ToByteArray( this WriteableBitmap bmp, int count )
        {
            return bmp.ToByteArray( 0, count );
        }

        internal static byte[ ] ToByteArray( this WriteableBitmap bmp )
        {
            return bmp.ToByteArray( 0, -1 );
        }

        internal static WriteableBitmap FromByteArray( this WriteableBitmap bmp, byte[ ] buffer, int offset, int count )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                BitmapContext.BlockCopy( buffer, offset, bitmapContext, 0, count );
                return bmp;
            }
        }

        internal static WriteableBitmap FromByteArray( this WriteableBitmap bmp, byte[ ] buffer, int count )
        {
            return bmp.FromByteArray( buffer, 0, count );
        }

        internal static WriteableBitmap FromByteArray( this WriteableBitmap bmp, byte[ ] buffer )
        {
            return bmp.FromByteArray( buffer, 0, buffer.Length );
        }

        internal static unsafe void WriteTga( this WriteableBitmap bmp, Stream destination )
        {
            int pixelWidth = bmp.PixelWidth;
            int pixelHeight = bmp.PixelHeight;
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                int* pixels = bitmapContext.Pixels;
                byte[] buffer1 = new byte[bitmapContext.Length * 4];
                int index1 = 0;
                int num1 = pixelWidth << 2;
                int num2 = pixelWidth << 3;
                int index2 = (pixelHeight - 1) * num1;
                for ( int index3 = 0 ; index3 < pixelHeight ; ++index3 )
                {
                    for ( int index4 = 0 ; index4 < pixelWidth ; ++index4 )
                    {
                        int num3 = pixels[index1];
                        buffer1[ index2 ] = ( byte ) ( num3 & ( int ) byte.MaxValue );
                        buffer1[ index2 + 1 ] = ( byte ) ( num3 >> 8 & ( int ) byte.MaxValue );
                        buffer1[ index2 + 2 ] = ( byte ) ( num3 >> 16 & ( int ) byte.MaxValue );
                        buffer1[ index2 + 3 ] = ( byte ) ( num3 >> 24 );
                        ++index1;
                        index2 += 4;
                    }
                    index2 -= num2;
                }
                byte[] numArray = new byte[18];
                numArray[ 2 ] = ( byte ) 2;
                numArray[ 12 ] = ( byte ) ( pixelWidth & ( int ) byte.MaxValue );
                numArray[ 13 ] = ( byte ) ( ( pixelWidth & 65280 ) >> 8 );
                numArray[ 14 ] = ( byte ) ( pixelHeight & ( int ) byte.MaxValue );
                numArray[ 15 ] = ( byte ) ( ( pixelHeight & 65280 ) >> 8 );
                numArray[ 16 ] = ( byte ) 32;
                byte[] buffer2 = numArray;
                using ( BinaryWriter binaryWriter = new BinaryWriter( destination ) )
                {
                    binaryWriter.Write( buffer2 );
                    binaryWriter.Write( buffer1 );
                }
            }
        }

        internal static WriteableBitmap FromResource( this WriteableBitmap bmp, string relativePath )
        {
            string name = new AssemblyName(Assembly.GetCallingAssembly().FullName).Name;
            return bmp.FromContent( name + ";component/" + relativePath );
        }

        internal static WriteableBitmap FromContent( this WriteableBitmap bmp, string relativePath )
        {
            using ( Stream stream = Application.GetResourceStream( new Uri( relativePath, UriKind.Relative ) ).Stream )
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.StreamSource = stream;
                bmp = new WriteableBitmap( ( BitmapSource ) bitmapImage );
                bitmapImage.UriSource = ( Uri ) null;
                return bmp;
            }
        }

        internal static void FillRectangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Color color )
        {
            int num = (int) color.A + 1;
            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
            bmp.FillRectangle( x1, y1, x2, y2, color1, WriteableBitmapExtensions.BlendMode.Alpha );
        }

        internal static unsafe void FillRectangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int color, WriteableBitmapExtensions.BlendMode blendMode = WriteableBitmapExtensions.BlendMode.Alpha )
        {
            int pixelWidth = bmp.PixelWidth;
            int pixelHeight = bmp.PixelHeight;
            int sa = color >> 24 & (int) byte.MaxValue;
            int sr = color >> 16 & (int) byte.MaxValue;
            int sg = color >> 8 & (int) byte.MaxValue;
            int sb = color & (int) byte.MaxValue;
            bool flag = blendMode == WriteableBitmapExtensions.BlendMode.None || sa == (int) byte.MaxValue;
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                int* pixels = bitmapContext.Pixels;
                if ( x1 < 0 && x2 < 0 || y1 < 0 && y2 < 0 || ( x1 >= pixelWidth && x2 >= pixelWidth || y1 >= pixelHeight && y2 >= pixelHeight ) )
                    return;
                if ( x1 < 0 )
                    x1 = 0;
                if ( x1 >= pixelWidth )
                    x1 = pixelWidth - 1;
                if ( y1 < 0 )
                    y1 = 0;
                if ( y1 >= pixelHeight )
                    y1 = pixelHeight - 1;
                if ( x2 < 0 )
                    x2 = 0;
                if ( x2 >= pixelWidth )
                    x2 = pixelWidth - 1;
                if ( y2 < 0 )
                    y2 = 0;
                if ( y2 >= pixelHeight )
                    y2 = pixelHeight - 1;
                if ( y1 > y2 )
                {
                    y2 -= y1;
                    y1 += y2;
                    y2 = y1 - y2;
                }
                int num1 = y1 * pixelWidth;
                int num2 = num1 + x1;
                int num3 = num1 + x2;
                for ( int index = num2 ; index <= num3 ; ++index )
                    pixels[ index ] = flag ? color : WriteableBitmapExtensions.AlphaBlendColors( pixels[ index ], sa, sr, sg, sb );
                int num4 = x2 - x1 + 1;
                int srcOffset = num2 * 4;
                int num5 = y2 * pixelWidth + x1;
                int num6 = num2 + pixelWidth;
                while ( num6 <= num5 )
                {
                    if ( flag )
                    {
                        BitmapContext.BlockCopy( bitmapContext, srcOffset, bitmapContext, num6 * 4, num4 * 4 );
                    }
                    else
                    {
                        for ( int index1 = 0 ; index1 < num4 ; ++index1 )
                        {
                            int index2 = num6 + index1;
                            pixels[ index2 ] = WriteableBitmapExtensions.AlphaBlendColors( pixels[ index2 ], sa, sr, sg, sb );
                        }
                    }
                    num6 += pixelWidth;
                }
            }
        }

        private static int AlphaBlendColors( int pixel, int sa, int sr, int sg, int sb )
        {
            int num1 = pixel;
            int num2 = num1 >> 24 & (int) byte.MaxValue;
            int num3 = num1 >> 16 & (int) byte.MaxValue;
            int num4 = num1 >> 8 & (int) byte.MaxValue;
            int num5 = num1 & (int) byte.MaxValue;
            return sa + ( num2 * ( ( int ) byte.MaxValue - sa ) * 32897 >> 23 ) << 24 | sr + ( num3 * ( ( int ) byte.MaxValue - sa ) * 32897 >> 23 ) << 16 | sg + ( num4 * ( ( int ) byte.MaxValue - sa ) * 32897 >> 23 ) << 8 | sb + ( num5 * ( ( int ) byte.MaxValue - sa ) * 32897 >> 23 );
        }

        internal static unsafe void FillRectangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Func<int, int, int> colorCb, WriteableBitmapExtensions.BlendMode blendMode = WriteableBitmapExtensions.BlendMode.Alpha )
        {
            int pixelWidth = bmp.PixelWidth;
            int pixelHeight = bmp.PixelHeight;
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                int* pixels = bitmapContext.Pixels;
                if ( x1 < 0 && x2 < 0 || y1 < 0 && y2 < 0 || ( x1 >= pixelWidth && x2 >= pixelWidth || y1 >= pixelHeight && y2 >= pixelHeight ) )
                    return;
                if ( x1 < 0 )
                    x1 = 0;
                if ( y1 < 0 )
                    y1 = 0;
                if ( x2 < 0 )
                    x2 = 0;
                if ( y2 < 0 )
                    y2 = 0;
                if ( x1 > pixelWidth )
                    x1 = pixelWidth;
                if ( y1 > pixelHeight )
                    y1 = pixelHeight;
                if ( x2 > pixelWidth )
                    x2 = pixelWidth;
                if ( y2 > pixelHeight )
                    y2 = pixelHeight;
                if ( y1 > y2 )
                {
                    y2 -= y1;
                    y1 += y2;
                    y2 = y1 - y2;
                }
                int num1 = x2 - x1 + 1;
                int num2 = y1 * pixelWidth + x1;
                int num3 = y2 * pixelWidth + x1;
                int num4 = y1;
                int num5 = num2;
                while ( num5 < num3 )
                {
                    int num6 = x1;
                    int num7 = 0;
                    while ( num7 < num1 )
                    {
                        int index = num5 + num7;
                        int num8 = colorCb(num6, num4);
                        int sa = num8 >> 24 & (int) byte.MaxValue;
                        int sr = num8 >> 16 & (int) byte.MaxValue;
                        int sg = num8 >> 8 & (int) byte.MaxValue;
                        int sb = num8 & (int) byte.MaxValue;
                        bool flag = blendMode == WriteableBitmapExtensions.BlendMode.None || sa == (int) byte.MaxValue;
                        pixels[ index ] = flag ? num8 : WriteableBitmapExtensions.AlphaBlendColors( pixels[ index ], sa, sr, sg, sb );
                        ++num7;
                        ++num6;
                    }
                    num5 += pixelWidth;
                    ++num4;
                }
            }
        }

        internal static void FillEllipse( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Color color )
        {
            int num = (int) color.A + 1;
            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
            bmp.FillEllipse( x1, y1, x2, y2, color1 );
        }

        internal static void FillEllipse( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int color )
        {
            int xr = x2 - x1 >> 1;
            int yr = y2 - y1 >> 1;
            int xc = x1 + xr;
            int yc = y1 + yr;
            bmp.FillEllipseCentered( xc, yc, xr, yr, color );
        }

        internal static void FillEllipseCentered( this WriteableBitmap bmp, int xc, int yc, int xr, int yr, Color color )
        {
            int num = (int) color.A + 1;
            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
            bmp.FillEllipseCentered( xc, yc, xr, yr, color1 );
        }

        internal static void FillEllipseCentered( this WriteableBitmap bmp, int xc, int yc, int xr, int yr, int color )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
                WriteableBitmapExtensions.FillEllipseCentered( bitmapContext, xc, yc, xr, yr, color, WriteableBitmapExtensions.BlendMode.Alpha );
        }

        internal static unsafe void FillEllipseCentered( BitmapContext context, int xc, int yc, int xr, int yr, int color, WriteableBitmapExtensions.BlendMode blendMode = WriteableBitmapExtensions.BlendMode.Alpha )
        {
            int* pixels = context.Pixels;
            int pixelWidth = context.PixelWidth;
            int pixelHeight = context.PixelHeight;
            if ( xr < 1 || yr < 1 )
                return;
            int num1 = xr;
            int num2 = 0;
            int num3 = xr * xr << 1;
            int num4 = yr * yr << 1;
            int num5 = yr * yr * (1 - (xr << 1));
            int num6 = xr * xr;
            int num7 = 0;
            int num8 = num4 * xr;
            int num9 = 0;
            int sa = color >> 24 & (int) byte.MaxValue;
            int sr = color >> 16 & (int) byte.MaxValue;
            int sg = color >> 8 & (int) byte.MaxValue;
            int sb = color & (int) byte.MaxValue;
            bool flag = blendMode == WriteableBitmapExtensions.BlendMode.None || sa == (int) byte.MaxValue;
            while ( num8 >= num9 )
            {
                int num10 = yc + num2;
                int num11 = yc - num2;
                if ( num10 < 0 )
                    num10 = 0;
                if ( num10 >= pixelHeight )
                    num10 = pixelHeight - 1;
                if ( num11 < 0 )
                    num11 = 0;
                if ( num11 >= pixelHeight )
                    num11 = pixelHeight - 1;
                int num12 = num10 * pixelWidth;
                int num13 = num11 * pixelWidth;
                int num14 = xc + num1;
                int num15 = xc - num1;
                if ( num14 < 0 )
                    num14 = 0;
                if ( num14 >= pixelWidth )
                    num14 = pixelWidth - 1;
                if ( num15 < 0 )
                    num15 = 0;
                if ( num15 >= pixelWidth )
                    num15 = pixelWidth - 1;
                for ( int index = num15 ; index <= num14 ; ++index )
                {
                    pixels[ index + num12 ] = flag ? color : WriteableBitmapExtensions.AlphaBlendColors( pixels[ index + num12 ], sa, sr, sg, sb );
                    pixels[ index + num13 ] = flag ? color : WriteableBitmapExtensions.AlphaBlendColors( pixels[ index + num13 ], sa, sr, sg, sb );
                }
                ++num2;
                num9 += num3;
                num7 += num6;
                num6 += num3;
                if ( num5 + ( num7 << 1 ) > 0 )
                {
                    --num1;
                    num8 -= num4;
                    num7 += num5;
                    num5 += num4;
                }
            }
            int num16 = 0;
            int num17 = yr;
            int num18 = yc + num17;
            int num19 = yc - num17;
            if ( num18 < 0 )
                num18 = 0;
            if ( num18 >= pixelHeight )
                num18 = pixelHeight - 1;
            if ( num19 < 0 )
                num19 = 0;
            if ( num19 >= pixelHeight )
                num19 = pixelHeight - 1;
            int num20 = num18 * pixelWidth;
            int num21 = num19 * pixelWidth;
            int num22 = yr * yr;
            int num23 = xr * xr * (1 - (yr << 1));
            int num24 = 0;
            int num25 = 0;
            int num26 = num3 * yr;
            while ( num25 <= num26 )
            {
                int num10 = xc + num16;
                int num11 = xc - num16;
                if ( num10 < 0 )
                    num10 = 0;
                if ( num10 >= pixelWidth )
                    num10 = pixelWidth - 1;
                if ( num11 < 0 )
                    num11 = 0;
                if ( num11 >= pixelWidth )
                    num11 = pixelWidth - 1;
                for ( int index = num11 ; index <= num10 ; ++index )
                {
                    pixels[ index + num20 ] = flag ? color : WriteableBitmapExtensions.AlphaBlendColors( pixels[ index + num20 ], sa, sr, sg, sb );
                    pixels[ index + num21 ] = flag ? color : WriteableBitmapExtensions.AlphaBlendColors( pixels[ index + num21 ], sa, sr, sg, sb );
                }
                ++num16;
                num25 += num4;
                num24 += num22;
                num22 += num4;
                if ( num23 + ( num24 << 1 ) > 0 )
                {
                    --num17;
                    int num12 = yc + num17;
                    int num13 = yc - num17;
                    if ( num12 < 0 )
                        num12 = 0;
                    if ( num12 >= pixelHeight )
                        num12 = pixelHeight - 1;
                    if ( num13 < 0 )
                        num13 = 0;
                    if ( num13 >= pixelHeight )
                        num13 = pixelHeight - 1;
                    num20 = num12 * pixelWidth;
                    num21 = num13 * pixelWidth;
                    num26 -= num3;
                    num24 += num23;
                    num23 += num3;
                }
            }
        }

        internal static void FillPolygon( this WriteableBitmap bmp, int[ ] points, Color color )
        {
            int num = (int) color.A + 1;
            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
            bmp.FillPolygon( points, color1, WriteableBitmapExtensions.BlendMode.Alpha );
        }

        internal static unsafe void FillPolygon( this WriteableBitmap bmp, int[ ] points, int color, WriteableBitmapExtensions.BlendMode blendMode = WriteableBitmapExtensions.BlendMode.Alpha )
        {
            int pixelWidth = bmp.PixelWidth;
            int pixelHeight = bmp.PixelHeight;
            int sa = color >> 24 & (int) byte.MaxValue;
            int sr = color >> 16 & (int) byte.MaxValue;
            int sg = color >> 8 & (int) byte.MaxValue;
            int sb = color & (int) byte.MaxValue;
            bool flag = blendMode == WriteableBitmapExtensions.BlendMode.None || sa == (int) byte.MaxValue;
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                int* pixels = bitmapContext.Pixels;
                int length = points.Length;
                int[] numArray = new int[points.Length >> 1];
                int num1 = pixelHeight;
                int num2 = 0;
                int index1 = 1;
                while ( index1 < length )
                {
                    int point = points[index1];
                    if ( point < num1 )
                        num1 = point;
                    if ( point > num2 )
                        num2 = point;
                    index1 += 2;
                }
                if ( num1 < 0 )
                    num1 = 0;
                if ( num2 >= pixelHeight )
                    num2 = pixelHeight - 1;
                for ( int index2 = num1 ; index2 <= num2 ; ++index2 )
                {
                    float num3 = (float) points[0];
                    float num4 = (float) points[1];
                    int num5 = 0;
                    int index3 = 2;
                    while ( index3 < length )
                    {
                        float point1 = (float) points[index3];
                        float point2 = (float) points[index3 + 1];
                        if ( ( double ) num4 < ( double ) index2 && ( double ) point2 >= ( double ) index2 || ( double ) point2 < ( double ) index2 && ( double ) num4 >= ( double ) index2 )
                            numArray[ num5++ ] = ( int ) ( ( double ) num3 + ( ( double ) index2 - ( double ) num4 ) / ( ( double ) point2 - ( double ) num4 ) * ( ( double ) point1 - ( double ) num3 ) );
                        num3 = point1;
                        num4 = point2;
                        index3 += 2;
                    }
                    for ( int index4 = 1 ; index4 < num5 ; ++index4 )
                    {
                        int num6 = numArray[index4];
                        int index5;
                        for ( index5 = index4 ; index5 > 0 && numArray[ index5 - 1 ] > num6 ; --index5 )
                            numArray[ index5 ] = numArray[ index5 - 1 ];
                        numArray[ index5 ] = num6;
                    }
                    int index6 = 0;
                    while ( index6 < num5 - 1 )
                    {
                        int num6 = numArray[index6];
                        int num7 = numArray[index6 + 1];
                        if ( num7 > 0 && num6 < pixelWidth )
                        {
                            if ( num6 < 0 )
                                num6 = 0;
                            if ( num7 >= pixelWidth )
                                num7 = pixelWidth - 1;
                            for ( int index4 = num6 ; index4 <= num7 ; ++index4 )
                            {
                                int index5 = index2 * pixelWidth + index4;
                                pixels[ index5 ] = flag ? color : WriteableBitmapExtensions.AlphaBlendColors( pixels[ index5 ], sa, sr, sg, sb );
                            }
                        }
                        index6 += 2;
                    }
                }
            }
        }

        internal static unsafe void FillPolygon( this WriteableBitmap bmp, int[ ] points, Func<int, int, int> colorCb, WriteableBitmapExtensions.BlendMode blendMode = WriteableBitmapExtensions.BlendMode.Alpha )
        {
            int pixelWidth = bmp.PixelWidth;
            int pixelHeight = bmp.PixelHeight;
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                int* pixels = bitmapContext.Pixels;
                int length = points.Length;
                int[] numArray = new int[points.Length >> 1];
                int num1 = pixelHeight;
                int num2 = 0;
                int index1 = 1;
                while ( index1 < length )
                {
                    int point = points[index1];
                    if ( point < num1 )
                        num1 = point;
                    if ( point > num2 )
                        num2 = point;
                    index1 += 2;
                }
                if ( num1 < 0 )
                    num1 = 0;
                if ( num2 >= pixelHeight )
                    num2 = pixelHeight - 1;
                for ( int index2 = num1 ; index2 <= num2 ; ++index2 )
                {
                    float num3 = (float) points[0];
                    float num4 = (float) points[1];
                    int num5 = 0;
                    int index3 = 2;
                    while ( index3 < length )
                    {
                        float point1 = (float) points[index3];
                        float point2 = (float) points[index3 + 1];
                        if ( ( double ) num4 < ( double ) index2 && ( double ) point2 >= ( double ) index2 || ( double ) point2 < ( double ) index2 && ( double ) num4 >= ( double ) index2 )
                            numArray[ num5++ ] = ( int ) ( ( double ) num3 + ( ( double ) index2 - ( double ) num4 ) / ( ( double ) point2 - ( double ) num4 ) * ( ( double ) point1 - ( double ) num3 ) );
                        num3 = point1;
                        num4 = point2;
                        index3 += 2;
                    }
                    for ( int index4 = 1 ; index4 < num5 ; ++index4 )
                    {
                        int num6 = numArray[index4];
                        int index5;
                        for ( index5 = index4 ; index5 > 0 && numArray[ index5 - 1 ] > num6 ; --index5 )
                            numArray[ index5 ] = numArray[ index5 - 1 ];
                        numArray[ index5 ] = num6;
                    }
                    int index6 = 0;
                    while ( index6 < num5 - 1 )
                    {
                        int num6 = numArray[index6];
                        int num7 = numArray[index6 + 1];
                        if ( num7 > 0 && num6 < pixelWidth )
                        {
                            if ( num6 < 0 )
                                num6 = 0;
                            if ( num7 >= pixelWidth )
                                num7 = pixelWidth - 1;
                            for ( int index4 = num6 ; index4 <= num7 ; ++index4 )
                            {
                                int index5 = index2 * pixelWidth + index4;
                                int num8 = colorCb(index4, index2);
                                int sa = num8 >> 24 & (int) byte.MaxValue;
                                int sr = num8 >> 16 & (int) byte.MaxValue;
                                int sg = num8 >> 8 & (int) byte.MaxValue;
                                int sb = num8 & (int) byte.MaxValue;
                                bool flag = blendMode == WriteableBitmapExtensions.BlendMode.None || sa == (int) byte.MaxValue;
                                pixels[ index5 ] = flag ? num8 : WriteableBitmapExtensions.AlphaBlendColors( pixels[ index5 ], sa, sr, sg, sb );
                            }
                        }
                        index6 += 2;
                    }
                }
            }
        }

        internal static void FillQuad( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, Color color )
        {
            int num = (int) color.A + 1;
            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
            bmp.FillQuad( x1, y1, x2, y2, x3, y3, x4, y4, color1 );
        }

        internal static void FillQuad( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, int color )
        {
            bmp.FillPolygon( new int[ 10 ]
            {
        x1,
        y1,
        x2,
        y2,
        x3,
        y3,
        x4,
        y4,
        x1,
        y1
            }, color, WriteableBitmapExtensions.BlendMode.Alpha );
        }

        internal static void FillTriangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int x3, int y3, Color color )
        {
            int num = (int) color.A + 1;
            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
            bmp.FillTriangle( x1, y1, x2, y2, x3, y3, color1 );
        }

        internal static void FillTriangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int x3, int y3, int color )
        {
            bmp.FillPolygon( new int[ 8 ]
            {
        x1,
        y1,
        x2,
        y2,
        x3,
        y3,
        x1,
        y1
            }, color, WriteableBitmapExtensions.BlendMode.Alpha );
        }

        private static unsafe List<int> ComputeBezierPoints( int x1, int y1, int cx1, int cy1, int cx2, int cy2, int x2, int y2, int color, BitmapContext context, int w, int h )
        {
            int* pixels = context.Pixels;
            int num1 = Math.Min(x1, Math.Min(cx1, Math.Min(cx2, x2)));
            int num2 = Math.Min(y1, Math.Min(cy1, Math.Min(cy2, y2)));
            int num3 = Math.Max(x1, Math.Max(cx1, Math.Max(cx2, x2)));
            int num4 = Math.Max(y1, Math.Max(cy1, Math.Max(cy2, y2)));
            int num5 = num3 - num1;
            int num6 = num2;
            int num7 = num4 - num6;
            if ( num5 > num7 )
                num7 = num5;
            List<int> intList = new List<int>();
            if ( num7 != 0 )
            {
                float num8 = 2f / (float) num7;
                float num9 = 0.0f;
                while ( ( double ) num9 <= 1.0 )
                {
                    float num10 = num9 * num9;
                    float num11 = 1f - num9;
                    float num12 = num11 * num11;
                    int num13 = (int) ((double) num11 * (double) num12 * (double) x1 + 3.0 * (double) num9 * (double) num12 * (double) cx1 + 3.0 * (double) num11 * (double) num10 * (double) cx2 + (double) num9 * (double) num10 * (double) x2);
                    int num14 = (int) ((double) num11 * (double) num12 * (double) y1 + 3.0 * (double) num9 * (double) num12 * (double) cy1 + 3.0 * (double) num11 * (double) num10 * (double) cy2 + (double) num9 * (double) num10 * (double) y2);
                    intList.Add( num13 );
                    intList.Add( num14 );
                    num9 += num8;
                }
                intList.Add( x2 );
                intList.Add( y2 );
            }
            return intList;
        }

        internal static void FillBeziers( this WriteableBitmap bmp, int[ ] points, Color color )
        {
            int num = (int) color.A + 1;
            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
            bmp.FillBeziers( points, color1 );
        }

        internal static void FillBeziers( this WriteableBitmap bmp, int[ ] points, int color )
        {
            int pixelWidth = bmp.PixelWidth;
            int pixelHeight = bmp.PixelHeight;
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                int x1 = points[0];
                int y1 = points[1];
                List<int> intList = new List<int>();
                int index = 2;
                while ( index + 5 < points.Length )
                {
                    int point1 = points[index + 4];
                    int point2 = points[index + 5];
                    intList.AddRange( ( IEnumerable<int> ) WriteableBitmapExtensions.ComputeBezierPoints( x1, y1, points[ index ], points[ index + 1 ], points[ index + 2 ], points[ index + 3 ], point1, point2, color, bitmapContext, pixelWidth, pixelHeight ) );
                    x1 = point1;
                    y1 = point2;
                    index += 6;
                }
                bmp.FillPolygon( intList.ToArray(), color, WriteableBitmapExtensions.BlendMode.Alpha );
            }
        }

        private static unsafe List<int> ComputeSegmentPoints( int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, float tension, int color, BitmapContext context, int w, int h )
        {
            int* pixels = context.Pixels;
            int num1 = Math.Min(x1, Math.Min(x2, Math.Min(x3, x4)));
            int num2 = Math.Min(y1, Math.Min(y2, Math.Min(y3, y4)));
            int num3 = Math.Max(x1, Math.Max(x2, Math.Max(x3, x4)));
            int num4 = Math.Max(y1, Math.Max(y2, Math.Max(y3, y4)));
            int num5 = num3 - num1;
            int num6 = num2;
            int num7 = num4 - num6;
            if ( num5 > num7 )
                num7 = num5;
            List<int> intList = new List<int>();
            if ( num7 != 0 )
            {
                float num8 = 2f / (float) num7;
                float num9 = tension * (float) (x3 - x1);
                float num10 = tension * (float) (y3 - y1);
                float num11 = tension * (float) (x4 - x2);
                float num12 = tension * (float) (y4 - y2);
                float num13 = num9 + num11 + (float) (2 * x2) - (float) (2 * x3);
                float num14 = num10 + num12 + (float) (2 * y2) - (float) (2 * y3);
                float num15 = -2f * num9 - num11 - (float) (3 * x2) + (float) (3 * x3);
                float num16 = -2f * num10 - num12 - (float) (3 * y2) + (float) (3 * y3);
                float num17 = 0.0f;
                while ( ( double ) num17 <= 1.0 )
                {
                    float num18 = num17 * num17;
                    int num19 = (int) ((double) num13 * (double) num18 * (double) num17 + (double) num15 * (double) num18 + (double) num9 * (double) num17 + (double) x2);
                    int num20 = (int) ((double) num14 * (double) num18 * (double) num17 + (double) num16 * (double) num18 + (double) num10 * (double) num17 + (double) y2);
                    intList.Add( num19 );
                    intList.Add( num20 );
                    num17 += num8;
                }
                intList.Add( x3 );
                intList.Add( y3 );
            }
            return intList;
        }

        internal static void FillCurve( this WriteableBitmap bmp, int[ ] points, float tension, Color color )
        {
            int num = (int) color.A + 1;
            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
            bmp.FillCurve( points, tension, color1 );
        }

        internal static void FillCurve( this WriteableBitmap bmp, int[ ] points, float tension, int color )
        {
            int pixelWidth = bmp.PixelWidth;
            int pixelHeight = bmp.PixelHeight;
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                List<int> segmentPoints = WriteableBitmapExtensions.ComputeSegmentPoints(points[0], points[1], points[0], points[1], points[2], points[3], points[4], points[5], tension, color, bitmapContext, pixelWidth, pixelHeight);
                int index = 2;
                while ( index < points.Length - 4 )
                {
                    segmentPoints.AddRange( ( IEnumerable<int> ) WriteableBitmapExtensions.ComputeSegmentPoints( points[ index - 2 ], points[ index - 1 ], points[ index ], points[ index + 1 ], points[ index + 2 ], points[ index + 3 ], points[ index + 4 ], points[ index + 5 ], tension, color, bitmapContext, pixelWidth, pixelHeight ) );
                    index += 2;
                }
                segmentPoints.AddRange( ( IEnumerable<int> ) WriteableBitmapExtensions.ComputeSegmentPoints( points[ index - 2 ], points[ index - 1 ], points[ index ], points[ index + 1 ], points[ index + 2 ], points[ index + 3 ], points[ index + 2 ], points[ index + 3 ], tension, color, bitmapContext, pixelWidth, pixelHeight ) );
                bmp.FillPolygon( segmentPoints.ToArray(), color, WriteableBitmapExtensions.BlendMode.Alpha );
            }
        }

        internal static void FillCurveClosed( this WriteableBitmap bmp, int[ ] points, float tension, Color color )
        {
            int num = (int) color.A + 1;
            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
            bmp.FillCurveClosed( points, tension, color1 );
        }

        internal static void FillCurveClosed( this WriteableBitmap bmp, int[ ] points, float tension, int color )
        {
            int pixelWidth = bmp.PixelWidth;
            int pixelHeight = bmp.PixelHeight;
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                int length = points.Length;
                List<int> segmentPoints = WriteableBitmapExtensions.ComputeSegmentPoints(points[length - 2], points[length - 1], points[0], points[1], points[2], points[3], points[4], points[5], tension, color, bitmapContext, pixelWidth, pixelHeight);
                int index = 2;
                while ( index < length - 4 )
                {
                    segmentPoints.AddRange( ( IEnumerable<int> ) WriteableBitmapExtensions.ComputeSegmentPoints( points[ index - 2 ], points[ index - 1 ], points[ index ], points[ index + 1 ], points[ index + 2 ], points[ index + 3 ], points[ index + 4 ], points[ index + 5 ], tension, color, bitmapContext, pixelWidth, pixelHeight ) );
                    index += 2;
                }
                segmentPoints.AddRange( ( IEnumerable<int> ) WriteableBitmapExtensions.ComputeSegmentPoints( points[ index - 2 ], points[ index - 1 ], points[ index ], points[ index + 1 ], points[ index + 2 ], points[ index + 3 ], points[ 0 ], points[ 1 ], tension, color, bitmapContext, pixelWidth, pixelHeight ) );
                segmentPoints.AddRange( ( IEnumerable<int> ) WriteableBitmapExtensions.ComputeSegmentPoints( points[ index ], points[ index + 1 ], points[ index + 2 ], points[ index + 3 ], points[ 0 ], points[ 1 ], points[ 2 ], points[ 3 ], tension, color, bitmapContext, pixelWidth, pixelHeight ) );
                bmp.FillPolygon( segmentPoints.ToArray(), color, WriteableBitmapExtensions.BlendMode.Alpha );
            }
        }

        internal static WriteableBitmap Convolute( this WriteableBitmap bmp, int[ , ] kernel )
        {
            int kernelFactorSum = 0;
            int[,] numArray = kernel;
            int upperBound1 = numArray.GetUpperBound(0);
            int upperBound2 = numArray.GetUpperBound(1);
            for ( int lowerBound1 = numArray.GetLowerBound( 0 ) ; lowerBound1 <= upperBound1 ; ++lowerBound1 )
            {
                for ( int lowerBound2 = numArray.GetLowerBound( 1 ) ; lowerBound2 <= upperBound2 ; ++lowerBound2 )
                {
                    int num = numArray[lowerBound1, lowerBound2];
                    kernelFactorSum += num;
                }
            }
            return bmp.Convolute( kernel, kernelFactorSum, 0 );
        }

        internal static unsafe WriteableBitmap Convolute( this WriteableBitmap bmp, int[ , ] kernel, int kernelFactorSum, int kernelOffsetSum )
        {
            int num1 = kernel.GetUpperBound(0) + 1;
            int num2 = kernel.GetUpperBound(1) + 1;
            if ( ( num2 & 1 ) == 0 )
                throw new InvalidOperationException( "Kernel width must be odd!" );
            if ( ( num1 & 1 ) == 0 )
                throw new InvalidOperationException( "Kernel height must be odd!" );
            int pixelWidth = bmp.PixelWidth;
            int pixelHeight = bmp.PixelHeight;
            WriteableBitmap bmp1 = BitmapFactory.New(pixelWidth, pixelHeight);
            using ( BitmapContext bitmapContext1 = bmp.GetBitmapContext( ReadWriteMode.ReadOnly ) )
            {
                using ( BitmapContext bitmapContext2 = bmp1.GetBitmapContext() )
                {
                    int* pixels1 = bitmapContext1.Pixels;
                    int* pixels2 = bitmapContext2.Pixels;
                    int num3 = 0;
                    int num4 = num2 >> 1;
                    int num5 = num1 >> 1;
                    for ( int index1 = 0 ; index1 < pixelHeight ; ++index1 )
                    {
                        for ( int index2 = 0 ; index2 < pixelWidth ; ++index2 )
                        {
                            int num6 = 0;
                            int num7 = 0;
                            int num8 = 0;
                            int num9 = 0;
                            for ( int index3 = -num4 ; index3 <= num4 ; ++index3 )
                            {
                                int num10 = index3 + index2;
                                if ( num10 < 0 )
                                    num10 = 0;
                                else if ( num10 >= pixelWidth )
                                    num10 = pixelWidth - 1;
                                for ( int index4 = -num5 ; index4 <= num5 ; ++index4 )
                                {
                                    int num11 = index4 + index1;
                                    if ( num11 < 0 )
                                        num11 = 0;
                                    else if ( num11 >= pixelHeight )
                                        num11 = pixelHeight - 1;
                                    int num12 = pixels1[num11 * pixelWidth + num10];
                                    int num13 = kernel[index4 + num4, index3 + num5];
                                    num6 += ( num12 >> 24 & ( int ) byte.MaxValue ) * num13;
                                    num7 += ( num12 >> 16 & ( int ) byte.MaxValue ) * num13;
                                    num8 += ( num12 >> 8 & ( int ) byte.MaxValue ) * num13;
                                    num9 += ( num12 & ( int ) byte.MaxValue ) * num13;
                                }
                            }
                            int num14 = num6 / kernelFactorSum + kernelOffsetSum;
                            int num15 = num7 / kernelFactorSum + kernelOffsetSum;
                            int num16 = num8 / kernelFactorSum + kernelOffsetSum;
                            int num17 = num9 / kernelFactorSum + kernelOffsetSum;
                            byte num18 = num14 > (int) byte.MaxValue ? byte.MaxValue : (num14 < 0 ? (byte) 0 : (byte) num14);
                            byte num19 = num15 > (int) byte.MaxValue ? byte.MaxValue : (num15 < 0 ? (byte) 0 : (byte) num15);
                            byte num20 = num16 > (int) byte.MaxValue ? byte.MaxValue : (num16 < 0 ? (byte) 0 : (byte) num16);
                            byte num21 = num17 > (int) byte.MaxValue ? byte.MaxValue : (num17 < 0 ? (byte) 0 : (byte) num17);
                            pixels2[ num3++ ] = ( int ) num18 << 24 | ( int ) num19 << 16 | ( int ) num20 << 8 | ( int ) num21;
                        }
                    }
                    return bmp1;
                }
            }
        }

        internal static unsafe WriteableBitmap Invert( this WriteableBitmap bmp )
        {
            WriteableBitmap bmp1 = BitmapFactory.New(bmp.PixelWidth, bmp.PixelHeight);
            using ( BitmapContext bitmapContext1 = bmp1.GetBitmapContext() )
            {
                using ( BitmapContext bitmapContext2 = bmp.GetBitmapContext() )
                {
                    int* pixels1 = bitmapContext1.Pixels;
                    int* pixels2 = bitmapContext2.Pixels;
                    int length = bitmapContext2.Length;
                    for ( int index = 0 ; index < length ; ++index )
                    {
                        int num1 = pixels2[index];
                        int num2 = num1 >> 24 & (int) byte.MaxValue;
                        int num3 = num1 >> 16 & (int) byte.MaxValue;
                        int num4 = num1 >> 8 & (int) byte.MaxValue;
                        int num5 = num1 & (int) byte.MaxValue;
                        int num6 = (int) byte.MaxValue - num3;
                        int num7 = (int) byte.MaxValue - num4;
                        int num8 = (int) byte.MaxValue - num5;
                        pixels1[ index ] = num2 << 24 | num6 << 16 | num7 << 8 | num8;
                    }
                    return bmp1;
                }
            }
        }

        internal static void DrawPennedLine( BitmapContext context, int w, int h, int x1, int y1, int x2, int y2, BitmapContext pen )
        {
            if ( y1 < 0 && y2 < 0 || y1 > h && y2 > h || x1 == x2 && y1 == y2 )
                return;
            int pixelWidth = pen.WriteableBitmap.PixelWidth;
            int num1 = pixelWidth / 2;
            Rect sourceRect = new Rect(0.0, 0.0, (double) pixelWidth, (double) pixelWidth);
            int num2 = x2 - x1;
            int num3 = y2 - y1;
            int num4 = 0;
            if ( num2 < 0 )
            {
                num2 = -num2;
                num4 = -1;
            }
            else if ( num2 > 0 )
                num4 = 1;
            int num5 = 0;
            if ( num3 < 0 )
            {
                num3 = -num3;
                num5 = -1;
            }
            else if ( num3 > 0 )
                num5 = 1;
            int num6;
            int num7;
            int num8;
            int num9;
            int num10;
            int num11;
            if ( num2 > num3 )
            {
                num6 = num4;
                num7 = 0;
                num8 = num4;
                num9 = num5;
                num10 = num3;
                num11 = num2;
            }
            else
            {
                num6 = 0;
                num7 = num5;
                num8 = num4;
                num9 = num5;
                num10 = num2;
                num11 = num3;
            }
            int num12 = x1;
            int num13 = y1;
            int num14 = num11 >> 1;
            Rect destRect = new Rect((double) (num12 - num1), (double) (num13 - num1), (double) pixelWidth, (double) pixelWidth);
            if ( num13 < h && num13 >= 0 && ( num12 < w && num12 >= 0 ) )
                WriteableBitmapExtensions.Blit( context, w, h, destRect, pen, sourceRect, pixelWidth );
            for ( int index = 0 ; index < num11 ; ++index )
            {
                num14 -= num10;
                if ( num14 < 0 )
                {
                    num14 += num11;
                    num12 += num8;
                    num13 += num9;
                }
                else
                {
                    num12 += num6;
                    num13 += num7;
                }
                if ( num13 < h && num13 >= 0 && ( num12 < w && num12 >= 0 ) )
                {
                    destRect.X = ( double ) ( num12 - num1 );
                    destRect.Y = ( double ) ( num13 - num1 );
                    WriteableBitmapExtensions.Blit( context, w, h, destRect, pen, sourceRect, pixelWidth );
                }
            }
        }

        private static byte ComputeOutCode( Rect extents, double x, double y )
        {
            byte num = 0;
            if ( x < extents.Left )
                num |= ( byte ) 1;
            else if ( x > extents.Right )
                num |= ( byte ) 2;
            if ( y > extents.Bottom )
                num |= ( byte ) 4;
            else if ( y < extents.Top )
                num |= ( byte ) 8;
            return num;
        }

        internal static bool CohenSutherlandLineClipWithViewPortOffset( Rect viewPort, ref float xi0, ref float yi0, ref float xi1, ref float yi1, int offset )
        {
            return WriteableBitmapExtensions.CohenSutherlandLineClip( new Rect( viewPort.X - ( double ) offset, viewPort.Y - ( double ) offset, viewPort.Width + ( double ) ( 2 * offset ), viewPort.Height + ( double ) ( 2 * offset ) ), ref xi0, ref yi0, ref xi1, ref yi1 );
        }

        internal static bool CohenSutherlandLineClip( Rect extents, ref float xi0, ref float yi0, ref float xi1, ref float yi1 )
        {
            double x0 = (double) xi0.ClipToInt();
            double y0 = (double) yi0.ClipToInt();
            double x1 = (double) xi1.ClipToInt();
            double y1 = (double) yi1.ClipToInt();
            int num = WriteableBitmapExtensions.CohenSutherlandLineClip(extents, ref x0, ref y0, ref x1, ref y1) ? 1 : 0;
            xi0 = ( float ) x0;
            yi0 = ( float ) y0;
            xi1 = ( float ) x1;
            yi1 = ( float ) y1;
            return num != 0;
        }

        internal static bool CohenSutherlandLineClip( Rect extents, ref int xi0, ref int yi0, ref int xi1, ref int yi1 )
        {
            double x0 = (double) xi0;
            double y0 = (double) yi0;
            double x1 = (double) xi1;
            double y1 = (double) yi1;
            int num = WriteableBitmapExtensions.CohenSutherlandLineClip(extents, ref x0, ref y0, ref x1, ref y1) ? 1 : 0;
            xi0 = ( int ) x0;
            yi0 = ( int ) y0;
            xi1 = ( int ) x1;
            yi1 = ( int ) y1;
            return num != 0;
        }

        internal static bool CohenSutherlandLineClip( Rect extents, ref double x0, ref double y0, ref double x1, ref double y1 )
        {
            byte outCode1 = WriteableBitmapExtensions.ComputeOutCode(extents, x0, y0);
            byte outCode2 = WriteableBitmapExtensions.ComputeOutCode(extents, x1, y1);
            if ( outCode1 == ( byte ) 0 && outCode2 == ( byte ) 0 )
                return true;
            bool flag = false;
            while ( ( ( int ) outCode1 | ( int ) outCode2 ) != 0 )
            {
                if ( ( ( int ) outCode1 & ( int ) outCode2 ) == 0 )
                {
                    double num1 = x1;
                    double num2 = y1;
                    byte num3 = outCode1 != (byte) 0 ? outCode1 : outCode2;
                    if ( ( ( int ) num3 & 8 ) != 0 )
                    {
                        if ( !double.IsInfinity( y0 ) )
                            num1 = x0 + ( x1 - x0 ) * ( extents.Top - y0 ) / ( y1 - y0 );
                        num2 = extents.Top;
                    }
                    else if ( ( ( int ) num3 & 4 ) != 0 )
                    {
                        if ( !double.IsInfinity( y0 ) )
                            num1 = x0 + ( x1 - x0 ) * ( extents.Bottom - y0 ) / ( y1 - y0 );
                        num2 = extents.Bottom;
                    }
                    else if ( ( ( int ) num3 & 2 ) != 0 )
                    {
                        if ( !double.IsInfinity( x0 ) )
                            num2 = y0 + ( y1 - y0 ) * ( extents.Right - x0 ) / ( x1 - x0 );
                        num1 = extents.Right;
                    }
                    else if ( ( ( int ) num3 & 1 ) != 0 )
                    {
                        if ( !double.IsInfinity( x0 ) )
                            num2 = y0 + ( y1 - y0 ) * ( extents.Left - x0 ) / ( x1 - x0 );
                        num1 = extents.Left;
                    }
                    else
                    {
                        num1 = double.NaN;
                        num2 = double.NaN;
                    }
                    if ( ( int ) num3 == ( int ) outCode1 )
                    {
                        x0 = num1;
                        y0 = num2;
                        outCode1 = WriteableBitmapExtensions.ComputeOutCode( extents, x0, y0 );
                    }
                    else
                    {
                        x1 = num1;
                        y1 = num2;
                        outCode2 = WriteableBitmapExtensions.ComputeOutCode( extents, x1, y1 );
                    }
                }
                else
                    goto label_26;
            }
            flag = true;
        label_26:
            return flag;
        }

        public static int AlphaBlend( int sa, int sr, int sg, int sb, int destPixel )
        {
            int num1 = destPixel >> 24 & (int) byte.MaxValue;
            int num2 = destPixel >> 16 & (int) byte.MaxValue;
            int num3 = destPixel >> 8 & (int) byte.MaxValue;
            int num4 = destPixel & (int) byte.MaxValue;
            destPixel = sa + ( num1 * ( ( int ) byte.MaxValue - sa ) * 32897 >> 23 ) << 24 | sr + ( num2 * ( ( int ) byte.MaxValue - sa ) * 32897 >> 23 ) << 16 | sg + ( num3 * ( ( int ) byte.MaxValue - sa ) * 32897 >> 23 ) << 8 | sb + ( num4 * ( ( int ) byte.MaxValue - sa ) * 32897 >> 23 );
            return destPixel;
        }

        public unsafe static void DrawWuLine( BitmapContext context, int pixelWidth, int pixelHeight, short X0, short Y0, short X1, short Y1, int sa, int sr, int sg, int sb )
        {
            int* pixels = context.Pixels;
            if ( Y0 > Y1 )
            {
                short num = Y0;
                Y0 = Y1;
                Y1 = num;
                num = X0;
                X0 = X1;
                X1 = num;
            }
            pixels[ Y0 * pixelWidth + X0 ] = AlphaBlend( sa, sr, sg, sb, pixels[ Y0 * pixelWidth + X0 ] );
            short num2 = (short)(X1 - X0);
            short num3;
            if ( num2 >= 0 )
            {
                num3 = 1;
            }
            else
            {
                num3 = -1;
                num2 = ( short ) ( -num2 );
            }
            short num4 = (short)(Y1 - Y0);
            if ( num4 == 0 )
            {
                while ( true )
                {
                    short num5 = num2;
                    num2 = ( short ) ( num5 - 1 );
                    if ( num5 == 0 )
                    {
                        break;
                    }
                    X0 = ( short ) ( X0 + num3 );
                    pixels[ Y0 * pixelWidth + X0 ] = AlphaBlend( sa, sr, sg, sb, pixels[ Y0 * pixelWidth + X0 ] );
                }
            }
            else if ( num2 == 0 )
            {
                do
                {
                    Y0 = ( short ) ( Y0 + 1 );
                    pixels[ Y0 * pixelWidth + X0 ] = AlphaBlend( sa, sr, sg, sb, pixels[ Y0 * pixelWidth + X0 ] );
                }
                while ( ( num4 = ( short ) ( num4 - 1 ) ) != 0 );
            }
            else if ( num2 == num4 )
            {
                do
                {
                    X0 = ( short ) ( X0 + num3 );
                    Y0 = ( short ) ( Y0 + 1 );
                    pixels[ Y0 * pixelWidth + X0 ] = AlphaBlend( sa, sr, sg, sb, pixels[ Y0 * pixelWidth + X0 ] );
                }
                while ( ( num4 = ( short ) ( num4 - 1 ) ) != 0 );
            }
            else
            {
                ushort num6 = 0;
                if ( num4 > num2 )
                {
                    ushort num7 = (ushort)((ulong)((long)num2 << 16) / (ulong)num4);
                    while ( ( num4 = ( short ) ( num4 - 1 ) ) != 0 )
                    {
                        ushort num8 = num6;
                        num6 = ( ushort ) ( num6 + num7 );
                        if ( num6 <= num8 )
                        {
                            X0 = ( short ) ( X0 + num3 );
                        }
                        Y0 = ( short ) ( Y0 + 1 );
                        ushort num9 = (ushort)(num6 >> 8);
                        int num10 = num9 ^ 0xFF;
                        pixels[ Y0 * pixelWidth + X0 ] = AlphaBlend( sa, sr * num10 >> 8, sg * num10 >> 8, sb * num10 >> 8, pixels[ Y0 * pixelWidth + X0 ] );
                        num10 = num9;
                        pixels[ Y0 * pixelWidth + X0 + num3 ] = AlphaBlend( sa, sr * num10 >> 8, sg * num10 >> 8, sb * num10 >> 8, pixels[ Y0 * pixelWidth + X0 + num3 ] );
                    }
                    pixels[ Y1 * pixelWidth + X1 ] = AlphaBlend( sa, sr, sg, sb, pixels[ Y1 * pixelWidth + X1 ] );
                }
                else
                {
                    ushort num7 = (ushort)((ulong)((long)num4 << 16) / (ulong)num2);
                    while ( ( num2 = ( short ) ( num2 - 1 ) ) != 0 )
                    {
                        ushort num8 = num6;
                        num6 = ( ushort ) ( num6 + num7 );
                        if ( num6 <= num8 )
                        {
                            Y0 = ( short ) ( Y0 + 1 );
                        }
                        X0 = ( short ) ( X0 + num3 );
                        ushort num9 = (ushort)(num6 >> 8);
                        int num11 = num9 ^ 0xFF;
                        pixels[ Y0 * pixelWidth + X0 ] = AlphaBlend( sa, sr * num11 >> 8, sg * num11 >> 8, sb * num11 >> 8, pixels[ Y0 * pixelWidth + X0 ] );
                        num11 = num9;
                        pixels[ ( Y0 + 1 ) * pixelWidth + X0 ] = AlphaBlend( sa, sr * num11 >> 8, sg * num11 >> 8, sb * num11 >> 8, pixels[ ( Y0 + 1 ) * pixelWidth + X0 ] );
                    }
                    pixels[ Y1 * pixelWidth + X1 ] = AlphaBlend( sa, sr, sg, sb, pixels[ Y1 * pixelWidth + X1 ] );
                }
            }
        }

        internal static void DrawLineBresenham( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Color color )
        {
            int num = (int) color.A + 1;
            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
            bmp.DrawLineBresenham( x1, y1, x2, y2, color1 );
        }

        internal static void DrawLineBresenham( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int color )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                int pixelWidth = bmp.PixelWidth;
                int pixelHeight = bmp.PixelHeight;
                WriteableBitmapExtensions.DrawLineBresenham( bitmapContext, pixelWidth, pixelHeight, x1, y1, x2, y2, color );
            }
        }

        internal static unsafe void DrawLineBresenham( BitmapContext context, int w, int h, int x1, int y1, int x2, int y2, int color )
        {
            if ( y1 < 0 && y2 < 0 || y1 > h && y2 > h )
                return;
            if ( x1 == x2 && y1 == y2 )
            {
                WriteableBitmapExtensions.DrawPixel( context, w, h, x1, y1, color );
            }
            else
            {
                int* pixels = context.Pixels;
                int num1 = x2 - x1;
                int num2 = y2 - y1;
                int num3 = 0;
                if ( num1 < 0 )
                {
                    num1 = -num1;
                    num3 = -1;
                }
                else if ( num1 > 0 )
                    num3 = 1;
                int num4 = 0;
                if ( num2 < 0 )
                {
                    num2 = -num2;
                    num4 = -1;
                }
                else if ( num2 > 0 )
                    num4 = 1;
                int num5;
                int num6;
                int num7;
                int num8;
                int num9;
                int num10;
                if ( num1 > num2 )
                {
                    num5 = num3;
                    num6 = 0;
                    num7 = num3;
                    num8 = num4;
                    num9 = num2;
                    num10 = num1;
                }
                else
                {
                    num5 = 0;
                    num6 = num4;
                    num7 = num3;
                    num8 = num4;
                    num9 = num1;
                    num10 = num2;
                }
                int num11 = x1;
                int num12 = y1;
                int num13 = num10 >> 1;
                if ( num12 < h && num12 >= 0 && ( num11 < w && num11 >= 0 ) )
                    pixels[ num12 * w + num11 ] = color;
                for ( int index = 0 ; index < num10 ; ++index )
                {
                    num13 -= num9;
                    if ( num13 < 0 )
                    {
                        num13 += num10;
                        num11 += num7;
                        num12 += num8;
                    }
                    else
                    {
                        num11 += num5;
                        num12 += num6;
                    }
                    if ( num12 < h && num12 >= 0 && ( num11 < w && num11 >= 0 ) )
                        pixels[ num12 * w + num11 ] = color;
                }
            }
        }

        internal static unsafe void DrawPixel( BitmapContext context, int w, int h, int x1, int y1, int color )
        {
            if ( y1 >= h || y1 < 0 || ( x1 >= w || x1 < 0 ) )
                return;
            context.Pixels[ y1 * w + x1 ] = color;
        }

        internal static void DrawLineDDA( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Color color )
        {
            int num = (int) color.A + 1;
            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
            bmp.DrawLineDDA( x1, y1, x2, y2, color1 );
        }

        internal static unsafe void DrawLineDDA( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int color )
        {
            int pixelWidth = bmp.PixelWidth;
            int pixelHeight = bmp.PixelHeight;
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                int* pixels = bitmapContext.Pixels;
                int num1 = x2 - x1;
                int num2 = y2 - y1;
                int num3 = num2 >= 0 ? num2 : -num2;
                int num4 = num1 >= 0 ? num1 : -num1;
                if ( num4 > num3 )
                    num3 = num4;
                if ( num3 == 0 )
                    return;
                float num5 = (float) num1 / (float) num3;
                float num6 = (float) num2 / (float) num3;
                float num7 = (float) x1;
                float num8 = (float) y1;
                for ( int index = 0 ; index < num3 ; ++index )
                {
                    if ( ( double ) num8 < ( double ) pixelHeight && ( double ) num8 >= 0.0 && ( ( double ) num7 < ( double ) pixelWidth && ( double ) num7 >= 0.0 ) )
                        pixels[ ( int ) num8 * pixelWidth + ( int ) num7 ] = color;
                    num7 += num5;
                    num8 += num6;
                }
            }
        }

        internal static void DrawLine( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Color color )
        {
            int num = (int) color.A + 1;
            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
            bmp.DrawLine( x1, y1, x2, y2, color1 );
        }

        internal static void DrawLine( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int color )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
                WriteableBitmapExtensions.DrawLine( bitmapContext, bmp.PixelWidth, bmp.PixelHeight, x1, y1, x2, y2, color );
        }

        internal static unsafe void DrawLine( BitmapContext context, int pixelWidth, int pixelHeight, int x1, int y1, int x2, int y2, int color )
        {
            int* pixels = context.Pixels;
            int num1 = x2 - x1;
            int num2 = y2 - y1;
            int num3 = num2 < 0 ? -num2 : num2;
            if ( ( num1 < 0 ? -num1 : num1 ) > num3 )
            {
                if ( num1 < 0 )
                {
                    int num4 = x1;
                    x1 = x2;
                    x2 = num4;
                    int num5 = y1;
                    y1 = y2;
                    y2 = num5;
                }
                int num6 = (num2 << 8) / num1;
                int num7 = y1 << 8;
                int num8 = y2 << 8;
                int num9 = pixelHeight << 8;
                if ( y1 < y2 )
                {
                    if ( y1 >= pixelHeight || y2 < 0 )
                        return;
                    if ( num7 < 0 )
                    {
                        if ( num6 == 0 )
                            return;
                        int num4 = num7;
                        num7 = num6 - 1 + ( num7 + 1 ) % num6;
                        x1 += ( num7 - num4 ) / num6;
                    }
                    if ( num8 >= num9 && num6 != 0 )
                    {
                        int num4 = num9 - 1 - (num9 - 1 - num7) % num6;
                        x2 = x1 + ( num4 - num7 ) / num6;
                    }
                }
                else
                {
                    if ( y2 >= pixelHeight || y1 < 0 )
                        return;
                    if ( num7 >= num9 )
                    {
                        if ( num6 == 0 )
                            return;
                        int num4 = num7;
                        num7 = num9 - 1 + ( num6 - ( num9 - 1 - num4 ) % num6 );
                        x1 += ( num7 - num4 ) / num6;
                    }
                    if ( num8 < 0 && num6 != 0 )
                    {
                        int num4 = num7 % num6;
                        x2 = x1 + ( num4 - num7 ) / num6;
                    }
                }
                if ( x1 < 0 )
                {
                    num7 -= num6 * x1;
                    x1 = 0;
                }
                if ( x2 >= pixelWidth )
                    x2 = pixelWidth - 1;
                int num10 = num7;
                int num11 = num10 >> 8;
                int num12 = num11;
                int index1 = x1 + num11 * pixelWidth;
                int num13 = num6 < 0 ? 1 - pixelWidth : 1 + pixelWidth;
                for ( int index2 = x1 ; index2 <= x2 ; ++index2 )
                {
                    pixels[ index1 ] = color;
                    num10 += num6;
                    int num4 = num10 >> 8;
                    if ( num4 != num12 )
                    {
                        num12 = num4;
                        index1 += num13;
                    }
                    else
                        ++index1;
                }
            }
            else
            {
                if ( num3 == 0 )
                    return;
                if ( num2 < 0 )
                {
                    int num4 = x1;
                    x1 = x2;
                    x2 = num4;
                    int num5 = y1;
                    y1 = y2;
                    y2 = num5;
                }
                int num6 = x1 << 8;
                int num7 = x2 << 8;
                int num8 = pixelWidth << 8;
                int num9 = (num1 << 8) / num2;
                if ( x1 < x2 )
                {
                    if ( x1 >= pixelWidth || x2 < 0 )
                        return;
                    if ( num6 < 0 )
                    {
                        if ( num9 == 0 )
                            return;
                        int num4 = num6;
                        num6 = num9 - 1 + ( num6 + 1 ) % num9;
                        y1 += ( num6 - num4 ) / num9;
                    }
                    if ( num7 >= num8 && num9 != 0 )
                    {
                        int num4 = num8 - 1 - (num8 - 1 - num6) % num9;
                        y2 = y1 + ( num4 - num6 ) / num9;
                    }
                }
                else
                {
                    if ( x2 >= pixelWidth || x1 < 0 )
                        return;
                    if ( num6 >= num8 )
                    {
                        if ( num9 == 0 )
                            return;
                        int num4 = num6;
                        num6 = num8 - 1 + ( num9 - ( num8 - 1 - num4 ) % num9 );
                        y1 += ( num6 - num4 ) / num9;
                    }
                    if ( num7 < 0 && num9 != 0 )
                    {
                        int num4 = num6 % num9;
                        y2 = y1 + ( num4 - num6 ) / num9;
                    }
                }
                if ( y1 < 0 )
                {
                    num6 -= num9 * y1;
                    y1 = 0;
                }
                if ( y2 >= pixelHeight )
                    y2 = pixelHeight - 1;
                int num10 = num6 + (y1 * pixelWidth << 8);
                int num11 = (pixelWidth << 8) + num9;
                for ( int index = y1 ; index <= y2 ; ++index )
                {
                    pixels[ num10 >> 8 ] = color;
                    num10 += num11;
                }
            }
        }

        internal static void DrawLineAa( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Color color )
        {
            int num = (int) color.A + 1;
            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
            bmp.DrawLineAa( x1, y1, x2, y2, color1 );
        }

        internal static void DrawLineAa( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int color )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
                WriteableBitmapExtensions.DrawLineAa( bitmapContext, bmp.PixelWidth, bmp.PixelHeight, x1, y1, x2, y2, color, false );
        }

        internal static unsafe void DrawLineAa( BitmapContext context, int pixelWidth, int pixelHeight, int x1, int y1, int x2, int y2, int color, bool skipFirstPixel = false )
        {
            if ( !WriteableBitmapExtensions.CohenSutherlandLineClip( new Rect( 0.0, 0.0, ( double ) pixelWidth, ( double ) pixelHeight ), ref x1, ref y1, ref x2, ref y2 ) )
                return;
            int num1 = pixelWidth - 1;
            int num2 = pixelHeight - 1;
            if ( x1 < 0 )
                x1 = 0;
            else if ( x1 > num1 )
                x1 = num1;
            if ( y1 < 0 )
                y1 = 0;
            else if ( y1 > num2 )
                y1 = num2;
            if ( x2 < 0 )
                x2 = 0;
            if ( x2 > num1 )
                x2 = num1;
            if ( y2 < 0 )
                y2 = 0;
            if ( y2 > num2 )
                y2 = num2;
            int num3 = pixelWidth * pixelHeight;
            int index1 = y1 * pixelWidth + x1;
            int num4 = x2 - x1;
            int num5 = y2 - y1;
            int num6 = color >> 24 & (int) byte.MaxValue;
            uint srb = (uint) (color & 16711935);
            uint sg = (uint) (color >> 8 & (int) byte.MaxValue);
            int num7 = num4;
            int num8 = num5;
            if ( num4 < 0 )
                num7 = -num4;
            if ( num5 < 0 )
                num8 = -num5;
            int num9;
            int num10;
            int num11;
            int num12;
            int num13;
            int num14;
            int num15;
            if ( num7 > num8 )
            {
                num9 = num7;
                num10 = num8;
                num11 = x2;
                num12 = y2;
                num13 = 1;
                num15 = num14 = pixelWidth;
                if ( num4 < 0 )
                    num13 = -num13;
                if ( num5 < 0 )
                    num15 = -num15;
            }
            else
            {
                num9 = num8;
                num10 = num7;
                num11 = y2;
                num12 = x2;
                num13 = num14 = pixelWidth;
                num15 = 1;
                if ( num5 < 0 )
                    num13 = -num13;
                if ( num4 < 0 )
                    num15 = -num15;
            }
            int num16 = num11 + num9;
            int num17 = (num10 << 1) - num9;
            int num18 = num10 << 1;
            int num19 = num10 - num9 << 1;
            double num20 = 1.0 / (4.0 * Math.Sqrt((double) (num9 * num9 + num10 * num10)));
            double num21 = 0.75 - 2.0 * ((double) num9 * num20);
            int num22 = (int) (num20 * 1024.0);
            double num23 = 1024.0;
            int num24 = (int) (num21 * num23 * (double) num6);
            int num25 = (int) (768.0 * (double) num6);
            int num26 = num22 * num6;
            int num27 = num9 * num26;
            int num28 = num17 * num26;
            int num29 = 0;
            int num30 = num18 * num26;
            int num31 = num19 * num26;
            int* pixels = context.Pixels;
            bool flag = true;
            do
            {
                if ( !flag || !skipFirstPixel )
                {
                    WriteableBitmapExtensions.AlphaBlendNormalOnPremultiplied( pixels, index1, num25 - num29 >> 10, srb, sg );
                    int index2 = index1 + num15;
                    if ( index2 < num3 && ( flag && num14 == num15 || index2 % num14 > 0 ) )
                        WriteableBitmapExtensions.AlphaBlendNormalOnPremultiplied( pixels, index2, num24 + num29 >> 10, srb, sg );
                    int index3 = index1 - num15;
                    if ( index3 >= 0 && index3 < num3 && ( flag && num14 == num15 || index1 % num14 > 0 ) )
                        WriteableBitmapExtensions.AlphaBlendNormalOnPremultiplied( pixels, index3, num24 - num29 >> 10, srb, sg );
                }
                if ( num17 < 0 )
                {
                    num29 = num28 + num27;
                    num17 += num18;
                    num28 += num30;
                }
                else
                {
                    num29 = num28 - num27;
                    num17 += num19;
                    num28 += num31;
                    ++num12;
                    index1 += num15;
                }
                ++num11;
                index1 += num13;
                flag = false;
            }
            while ( num11 <= num16 );
        }

        private static unsafe void AlphaBlendNormalOnPremultiplied( int* pixels, int index, int sa, uint srb, uint sg )
        {
            int num1 = pixels[index];
            uint num2 = (uint) num1 >> 24;
            uint num3 = (uint) num1 >> 8 & (uint) byte.MaxValue;
            uint num4 = (uint) (num1 & 16711935);
            pixels[ index ] = ( int ) ( ( long ) sa + ( ( long ) num2 * ( long ) ( ( int ) byte.MaxValue - sa ) * 32897L >> 23 ) << 24 | ( long ) ( sg - num3 ) * ( long ) sa + ( long ) ( num3 << 8 ) & 4294967040L | ( ( long ) ( srb - num4 ) * ( long ) sa >> 8 ) + ( long ) num4 & 16711935L );
        }

        internal static void DrawPolyline( this WriteableBitmap bmp, int[ ] points, Color color )
        {
            int num = (int) color.A + 1;
            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
            bmp.DrawPolyline( points, color1 );
        }

        internal static void DrawPolyline( this WriteableBitmap bmp, int[ ] points, int color )
        {
            int pixelWidth = bmp.PixelWidth;
            int pixelHeight = bmp.PixelHeight;
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                int x1 = points[0];
                int y1 = points[1];
                if ( x1 < 0 )
                    x1 = 0;
                if ( y1 < 0 )
                    y1 = 0;
                if ( x1 > pixelWidth )
                    x1 = pixelWidth;
                if ( y1 > pixelHeight )
                    y1 = pixelHeight;
                int index = 2;
                while ( index < points.Length )
                {
                    int x2 = points[index];
                    int y2 = points[index + 1];
                    if ( x2 < 0 )
                        x2 = 0;
                    if ( y2 < 0 )
                        y2 = 0;
                    if ( x2 > pixelWidth )
                        x2 = pixelWidth;
                    if ( y2 > pixelHeight )
                        y2 = pixelHeight;
                    WriteableBitmapExtensions.DrawLine( bitmapContext, pixelWidth, pixelHeight, x1, y1, x2, y2, color );
                    x1 = x2;
                    y1 = y2;
                    index += 2;
                }
            }
        }

        internal static void DrawTriangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int x3, int y3, Color color )
        {
            int num = (int) color.A + 1;
            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
            bmp.DrawTriangle( x1, y1, x2, y2, x3, y3, color1 );
        }

        internal static void DrawTriangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int x3, int y3, int color )
        {
            int pixelWidth = bmp.PixelWidth;
            int pixelHeight = bmp.PixelHeight;
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                WriteableBitmapExtensions.DrawLine( bitmapContext, pixelWidth, pixelHeight, x1, y1, x2, y2, color );
                WriteableBitmapExtensions.DrawLine( bitmapContext, pixelWidth, pixelHeight, x2, y2, x3, y3, color );
                WriteableBitmapExtensions.DrawLine( bitmapContext, pixelWidth, pixelHeight, x3, y3, x1, y1, color );
            }
        }

        internal static void DrawQuad( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, Color color )
        {
            int num = (int) color.A + 1;
            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
            bmp.DrawQuad( x1, y1, x2, y2, x3, y3, x4, y4, color1 );
        }

        internal static void DrawQuad( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, int color )
        {
            int pixelWidth = bmp.PixelWidth;
            int pixelHeight = bmp.PixelHeight;
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                WriteableBitmapExtensions.DrawLine( bitmapContext, pixelWidth, pixelHeight, x1, y1, x2, y2, color );
                WriteableBitmapExtensions.DrawLine( bitmapContext, pixelWidth, pixelHeight, x2, y2, x3, y3, color );
                WriteableBitmapExtensions.DrawLine( bitmapContext, pixelWidth, pixelHeight, x3, y3, x4, y4, color );
                WriteableBitmapExtensions.DrawLine( bitmapContext, pixelWidth, pixelHeight, x4, y4, x1, y1, color );
            }
        }

        internal static void DrawRectangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Color color )
        {
            int num = (int) color.A + 1;
            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
            bmp.DrawRectangle( x1, y1, x2, y2, color1 );
        }

        internal static unsafe void DrawRectangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int color )
        {
            int pixelWidth = bmp.PixelWidth;
            int pixelHeight = bmp.PixelHeight;
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                int* pixels = bitmapContext.Pixels;
                if ( x1 < 0 && x2 < 0 || y1 < 0 && y2 < 0 || ( x1 >= pixelWidth && x2 >= pixelWidth || y1 >= pixelHeight && y2 >= pixelHeight ) )
                    return;
                if ( x1 < 0 )
                    x1 = 0;
                if ( y1 < 0 )
                    y1 = 0;
                if ( x2 < 0 )
                    x2 = 0;
                if ( y2 < 0 )
                    y2 = 0;
                if ( x1 > pixelWidth )
                    x1 = pixelWidth;
                if ( y1 > pixelHeight )
                    y1 = pixelHeight;
                if ( x2 > pixelWidth )
                    x2 = pixelWidth;
                if ( y2 > pixelHeight )
                    y2 = pixelHeight;
                int num1 = y1 * pixelWidth;
                int index1 = y2 * pixelWidth - pixelWidth + x1;
                int num2 = num1 + x2;
                int num3 = num1 + x1;
                for ( int index2 = num3 ; index2 < num2 ; ++index2 )
                {
                    pixels[ index2 ] = color;
                    pixels[ index1 ] = color;
                    ++index1;
                }
                int index3 = num3 + pixelWidth;
                int num4 = index1 - pixelWidth;
                int index4 = num1 + x2 - 1 + pixelWidth;
                while ( index4 < num4 )
                {
                    pixels[ index4 ] = color;
                    pixels[ index3 ] = color;
                    index3 += pixelWidth;
                    index4 += pixelWidth;
                }
            }
        }

        internal static void DrawEllipse( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int color, int thickness )
        {
            int xr = x2 - x1 >> 1;
            int yr = y2 - y1 >> 1;
            int xc = x1 + xr;
            int yc = y1 + yr;
            bmp.DrawEllipseCentered( xc, yc, xr, yr, color, thickness );
        }

        internal static void DrawEllipseCentered( this WriteableBitmap bmp, int xc, int yc, int xr, int yr, Color color )
        {
            int num = (int) color.A + 1;
            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
            bmp.DrawEllipseCentered( xc, yc, xr, yr, color1 );
        }

        internal static unsafe void DrawEllipseCentered( this WriteableBitmap bmp, int xc, int yc, int xr, int yr, int color )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                int* pixels = bitmapContext.Pixels;
                int pixelWidth = bmp.PixelWidth;
                int pixelHeight = bmp.PixelHeight;
                if ( xr < 1 || yr < 1 )
                    return;
                int num1 = xr;
                int num2 = 0;
                int num3 = xr * xr << 1;
                int num4 = yr * yr << 1;
                int num5 = yr * yr * (1 - (xr << 1));
                int num6 = xr * xr;
                int num7 = 0;
                int num8 = num4 * xr;
                int num9 = 0;
                while ( num8 >= num9 )
                {
                    int num10 = yc + num2;
                    int num11 = yc - num2;
                    if ( num10 < 0 )
                        num10 = 0;
                    if ( num10 >= pixelHeight )
                        num10 = pixelHeight - 1;
                    if ( num11 < 0 )
                        num11 = 0;
                    if ( num11 >= pixelHeight )
                        num11 = pixelHeight - 1;
                    int num12 = num10 * pixelWidth;
                    int num13 = num11 * pixelWidth;
                    int num14 = xc + num1;
                    int num15 = xc - num1;
                    if ( num14 < 0 )
                        num14 = 0;
                    if ( num14 >= pixelWidth )
                        num14 = pixelWidth - 1;
                    if ( num15 < 0 )
                        num15 = 0;
                    if ( num15 >= pixelWidth )
                        num15 = pixelWidth - 1;
                    pixels[ num14 + num12 ] = color;
                    pixels[ num15 + num12 ] = color;
                    pixels[ num15 + num13 ] = color;
                    pixels[ num14 + num13 ] = color;
                    ++num2;
                    num9 += num3;
                    num7 += num6;
                    num6 += num3;
                    if ( num5 + ( num7 << 1 ) > 0 )
                    {
                        --num1;
                        num8 -= num4;
                        num7 += num5;
                        num5 += num4;
                    }
                }
                int num16 = 0;
                int num17 = yr;
                int num18 = yc + num17;
                int num19 = yc - num17;
                if ( num18 < 0 )
                    num18 = 0;
                if ( num18 >= pixelHeight )
                    num18 = pixelHeight - 1;
                if ( num19 < 0 )
                    num19 = 0;
                if ( num19 >= pixelHeight )
                    num19 = pixelHeight - 1;
                int num20 = num18 * pixelWidth;
                int num21 = num19 * pixelWidth;
                int num22 = yr * yr;
                int num23 = xr * xr * (1 - (yr << 1));
                int num24 = 0;
                int num25 = 0;
                int num26 = num3 * yr;
                while ( num25 <= num26 )
                {
                    int num10 = xc + num16;
                    int num11 = xc - num16;
                    if ( num10 < 0 )
                        num10 = 0;
                    if ( num10 >= pixelWidth )
                        num10 = pixelWidth - 1;
                    if ( num11 < 0 )
                        num11 = 0;
                    if ( num11 >= pixelWidth )
                        num11 = pixelWidth - 1;
                    pixels[ num10 + num20 ] = color;
                    pixels[ num11 + num20 ] = color;
                    pixels[ num11 + num21 ] = color;
                    pixels[ num10 + num21 ] = color;
                    ++num16;
                    num25 += num4;
                    num24 += num22;
                    num22 += num4;
                    if ( num23 + ( num24 << 1 ) > 0 )
                    {
                        --num17;
                        int num12 = yc + num17;
                        int num13 = yc - num17;
                        if ( num12 < 0 )
                            num12 = 0;
                        if ( num12 >= pixelHeight )
                            num12 = pixelHeight - 1;
                        if ( num13 < 0 )
                            num13 = 0;
                        if ( num13 >= pixelHeight )
                            num13 = pixelHeight - 1;
                        num20 = num12 * pixelWidth;
                        num21 = num13 * pixelWidth;
                        num26 -= num3;
                        num24 += num23;
                        num23 += num3;
                    }
                }
            }
        }

        internal static void DrawEllipseCentered( this WriteableBitmap bmp, int xc, int yc, int xr, int yr, int color, int thickness )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
                WriteableBitmapExtensions.DrawEllipseCentered( bitmapContext, xc, yc, xr, yr, color, thickness );
        }

        internal static unsafe void DrawEllipseCentered( BitmapContext context, int xc, int yc, int xr, int yr, int color, int thickness )
        {
            int num1 = thickness >> 1;
            int num2 = thickness - num1 - 1;
            int* pixels = context.Pixels;
            int pixelWidth = context.PixelWidth;
            int pixelHeight = context.PixelHeight;
            if ( xr < 1 || yr < 1 )
                return;
            int num3 = xr;
            int num4 = 0;
            int num5 = xr * xr << 1;
            int num6 = yr * yr << 1;
            int num7 = yr * yr * (1 - (xr << 1));
            int num8 = xr * xr;
            int num9 = 0;
            int num10 = num6 * xr;
            int num11 = 0;
            int num12 = 0;
            while ( num10 >= num11 )
            {
                int num13 = yc + num4;
                int num14 = yc - num4;
                if ( num13 < 0 )
                    num13 = 0;
                if ( num13 >= pixelHeight )
                    num13 = pixelHeight - 1;
                if ( num14 < 0 )
                    num14 = 0;
                if ( num14 >= pixelHeight )
                    num14 = pixelHeight - 1;
                int num15 = num13 * pixelWidth;
                int num16 = num14 * pixelWidth;
                int num17 = xc + num3;
                int num18 = xc - num3;
                if ( num17 < 0 )
                    num17 = 0;
                if ( num17 >= pixelWidth )
                    num17 = pixelWidth - 1;
                if ( num18 < 0 )
                    num18 = 0;
                if ( num18 >= pixelWidth )
                    num18 = pixelWidth - 1;
                pixels[ num17 + num15 ] = color;
                pixels[ num18 + num15 ] = color;
                pixels[ num18 + num16 ] = color;
                pixels[ num17 + num16 ] = color;
                for ( int index = 1 ; index <= num1 ; ++index )
                {
                    if ( num17 + index < pixelWidth )
                    {
                        pixels[ num17 + index + num15 ] = color;
                        pixels[ num17 + index + num16 ] = color;
                    }
                    if ( num18 - index >= 0 )
                    {
                        pixels[ num18 - index + num15 ] = color;
                        pixels[ num18 - index + num16 ] = color;
                    }
                }
                for ( int index = 1 ; index <= num2 ; ++index )
                {
                    if ( num17 - index < pixelWidth )
                    {
                        pixels[ num17 - index + num15 ] = color;
                        pixels[ num17 - index + num16 ] = color;
                    }
                    if ( num18 + index >= 0 )
                    {
                        pixels[ num18 + index + num15 ] = color;
                        pixels[ num18 + index + num16 ] = color;
                    }
                }
                num12 = num17 - xc;
                ++num4;
                num11 += num5;
                num9 += num8;
                num8 += num5;
                if ( num7 + ( num9 << 1 ) > 0 )
                {
                    --num3;
                    num10 -= num6;
                    num9 += num7;
                    num7 += num6;
                }
            }
            int num19 = 0;
            int num20 = yr;
            int num21 = yc + num20;
            int num22 = yc - num20;
            if ( num21 < 0 )
                num21 = 0;
            if ( num21 >= pixelHeight )
                num21 = pixelHeight - 1;
            if ( num22 < 0 )
                num22 = 0;
            if ( num22 >= pixelHeight )
                num22 = pixelHeight - 1;
            int num23 = num21 * pixelWidth;
            int num24 = num22 * pixelWidth;
            int num25 = yr * yr;
            int num26 = xr * xr * (1 - (yr << 1));
            int num27 = 0;
            int num28 = 0;
            int num29 = num5 * yr;
            while ( num28 <= num29 )
            {
                int num13 = xc + num19;
                int num14 = xc - num19;
                if ( num13 < 0 )
                    num13 = 0;
                if ( num13 >= pixelWidth )
                    num13 = pixelWidth - 1;
                if ( num14 < 0 )
                    num14 = 0;
                if ( num14 >= pixelWidth )
                    num14 = pixelWidth - 1;
                pixels[ num13 + num23 ] = color;
                pixels[ num14 + num23 ] = color;
                pixels[ num14 + num24 ] = color;
                pixels[ num13 + num24 ] = color;
                for ( int index = 1 ; index <= num1 ; ++index )
                {
                    if ( num21 + index < pixelHeight )
                    {
                        pixels[ num13 + num23 + index * pixelWidth ] = color;
                        pixels[ num14 + num23 + index * pixelWidth ] = color;
                    }
                    if ( num22 - index >= 0 )
                    {
                        pixels[ num14 + num24 - index * pixelWidth ] = color;
                        pixels[ num13 + num24 - index * pixelWidth ] = color;
                    }
                }
                for ( int index = 1 ; index <= num2 ; ++index )
                {
                    if ( num21 - index >= 0 )
                    {
                        pixels[ num13 + num23 - index * pixelWidth ] = color;
                        pixels[ num14 + num23 - index * pixelWidth ] = color;
                    }
                    if ( num22 + index < pixelHeight )
                    {
                        pixels[ num14 + num24 + index * pixelWidth ] = color;
                        pixels[ num13 + num24 + index * pixelWidth ] = color;
                    }
                }
                ++num19;
                num28 += num6;
                num27 += num25;
                num25 += num6;
                if ( num26 + ( num27 << 1 ) > 0 )
                {
                    --num20;
                    num21 = yc + num20;
                    num22 = yc - num20;
                    if ( num21 < 0 )
                        num21 = 0;
                    if ( num21 >= pixelHeight )
                        num21 = pixelHeight - 1;
                    if ( num22 < 0 )
                        num22 = 0;
                    if ( num22 >= pixelHeight )
                        num22 = pixelHeight - 1;
                    num23 = num21 * pixelWidth;
                    num24 = num22 * pixelWidth;
                    num29 -= num5;
                    num27 += num26;
                    num26 += num5;
                }
            }
            for ( int index1 = 1 ; index1 <= num1 ; ++index1 )
            {
                for ( int index2 = 1 ; index2 <= num1 - index1 ; ++index2 )
                {
                    int num13 = index1 + xc + num12;
                    int num14 = yc - index2 - num12;
                    if ( num13 >= 0 && num13 < pixelWidth && ( num14 >= 0 && num14 < pixelHeight ) )
                        pixels[ num13 + num14 * pixelWidth ] = color;
                    int num15 = -index1 + xc - num12;
                    int num16 = num14;
                    if ( num15 >= 0 && num15 < pixelWidth && ( num16 >= 0 && num16 < pixelHeight ) )
                        pixels[ num15 + num16 * pixelWidth ] = color;
                    int num17 = num15;
                    int num18 = yc + index2 + num12;
                    if ( num17 >= 0 && num17 < pixelWidth && ( num18 >= 0 && num18 < pixelHeight ) )
                        pixels[ num17 + num18 * pixelWidth ] = color;
                    int num30 = num13;
                    int num31 = num18;
                    if ( num30 >= 0 && num30 < pixelWidth && ( num31 >= 0 && num31 < pixelHeight ) )
                        pixels[ num30 + num31 * pixelWidth ] = color;
                }
            }
        }

        internal static void DrawBezier( this WriteableBitmap bmp, int x1, int y1, int cx1, int cy1, int cx2, int cy2, int x2, int y2, Color color )
        {
            int num = (int) color.A + 1;
            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
            bmp.DrawBezier( x1, y1, cx1, cy1, cx2, cy2, x2, y2, color1 );
        }

        internal static void DrawBezier( this WriteableBitmap bmp, int x1, int y1, int cx1, int cy1, int cx2, int cy2, int x2, int y2, int color )
        {
            int num1 = Math.Min(x1, Math.Min(cx1, Math.Min(cx2, x2)));
            int num2 = Math.Min(y1, Math.Min(cy1, Math.Min(cy2, y2)));
            int num3 = Math.Max(x1, Math.Max(cx1, Math.Max(cx2, x2)));
            int num4 = Math.Max(y1, Math.Max(cy1, Math.Max(cy2, y2)));
            int num5 = num3 - num1;
            int num6 = num2;
            int num7 = num4 - num6;
            if ( num5 > num7 )
                num7 = num5;
            if ( num7 == 0 )
                return;
            int pixelWidth = bmp.PixelWidth;
            int pixelHeight = bmp.PixelHeight;
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                float num8 = 2f / (float) num7;
                int x1_1 = x1;
                int y1_1 = y1;
                float num9 = num8;
                while ( ( double ) num9 <= 1.0 )
                {
                    float num10 = num9 * num9;
                    float num11 = 1f - num9;
                    float num12 = num11 * num11;
                    int x2_1 = (int) ((double) num11 * (double) num12 * (double) x1 + 3.0 * (double) num9 * (double) num12 * (double) cx1 + 3.0 * (double) num11 * (double) num10 * (double) cx2 + (double) num9 * (double) num10 * (double) x2);
                    int y2_1 = (int) ((double) num11 * (double) num12 * (double) y1 + 3.0 * (double) num9 * (double) num12 * (double) cy1 + 3.0 * (double) num11 * (double) num10 * (double) cy2 + (double) num9 * (double) num10 * (double) y2);
                    WriteableBitmapExtensions.DrawLine( bitmapContext, pixelWidth, pixelHeight, x1_1, y1_1, x2_1, y2_1, color );
                    x1_1 = x2_1;
                    y1_1 = y2_1;
                    num9 += num8;
                }
                WriteableBitmapExtensions.DrawLine( bitmapContext, pixelWidth, pixelHeight, x1_1, y1_1, x2, y2, color );
            }
        }

        internal static void DrawBeziers( this WriteableBitmap bmp, int[ ] points, Color color )
        {
            int num = (int) color.A + 1;
            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
            bmp.DrawBeziers( points, color1 );
        }

        internal static void DrawBeziers( this WriteableBitmap bmp, int[ ] points, int color )
        {
            int x1 = points[0];
            int y1 = points[1];
            int index = 2;
            while ( index + 5 < points.Length )
            {
                int point1 = points[index + 4];
                int point2 = points[index + 5];
                bmp.DrawBezier( x1, y1, points[ index ], points[ index + 1 ], points[ index + 2 ], points[ index + 3 ], point1, point2, color );
                x1 = point1;
                y1 = point2;
                index += 6;
            }
        }

        private static void DrawCurveSegment( int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, float tension, int color, BitmapContext context, int w, int h )
        {
            int num1 = Math.Min(x1, Math.Min(x2, Math.Min(x3, x4)));
            int num2 = Math.Min(y1, Math.Min(y2, Math.Min(y3, y4)));
            int num3 = Math.Max(x1, Math.Max(x2, Math.Max(x3, x4)));
            int num4 = Math.Max(y1, Math.Max(y2, Math.Max(y3, y4)));
            int num5 = num3 - num1;
            int num6 = num2;
            int num7 = num4 - num6;
            if ( num5 > num7 )
                num7 = num5;
            if ( num7 == 0 )
                return;
            float num8 = 2f / (float) num7;
            int x1_1 = x2;
            int y1_1 = y2;
            float num9 = tension * (float) (x3 - x1);
            float num10 = tension * (float) (y3 - y1);
            float num11 = tension * (float) (x4 - x2);
            float num12 = tension * (float) (y4 - y2);
            float num13 = num9 + num11 + (float) (2 * x2) - (float) (2 * x3);
            float num14 = num10 + num12 + (float) (2 * y2) - (float) (2 * y3);
            float num15 = -2f * num9 - num11 - (float) (3 * x2) + (float) (3 * x3);
            float num16 = -2f * num10 - num12 - (float) (3 * y2) + (float) (3 * y3);
            float num17 = num8;
            while ( ( double ) num17 <= 1.0 )
            {
                float num18 = num17 * num17;
                int x2_1 = (int) ((double) num13 * (double) num18 * (double) num17 + (double) num15 * (double) num18 + (double) num9 * (double) num17 + (double) x2);
                int y2_1 = (int) ((double) num14 * (double) num18 * (double) num17 + (double) num16 * (double) num18 + (double) num10 * (double) num17 + (double) y2);
                WriteableBitmapExtensions.DrawLine( context, w, h, x1_1, y1_1, x2_1, y2_1, color );
                x1_1 = x2_1;
                y1_1 = y2_1;
                num17 += num8;
            }
            WriteableBitmapExtensions.DrawLine( context, w, h, x1_1, y1_1, x3, y3, color );
        }

        internal static void DrawCurve( this WriteableBitmap bmp, int[ ] points, float tension, Color color )
        {
            int num = (int) color.A + 1;
            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
            bmp.DrawCurve( points, tension, color1 );
        }

        internal static void DrawCurve( this WriteableBitmap bmp, int[ ] points, float tension, int color )
        {
            int pixelWidth = bmp.PixelWidth;
            int pixelHeight = bmp.PixelHeight;
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                WriteableBitmapExtensions.DrawCurveSegment( points[ 0 ], points[ 1 ], points[ 0 ], points[ 1 ], points[ 2 ], points[ 3 ], points[ 4 ], points[ 5 ], tension, color, bitmapContext, pixelWidth, pixelHeight );
                int index = 2;
                while ( index < points.Length - 4 )
                {
                    WriteableBitmapExtensions.DrawCurveSegment( points[ index - 2 ], points[ index - 1 ], points[ index ], points[ index + 1 ], points[ index + 2 ], points[ index + 3 ], points[ index + 4 ], points[ index + 5 ], tension, color, bitmapContext, pixelWidth, pixelHeight );
                    index += 2;
                }
                WriteableBitmapExtensions.DrawCurveSegment( points[ index - 2 ], points[ index - 1 ], points[ index ], points[ index + 1 ], points[ index + 2 ], points[ index + 3 ], points[ index + 2 ], points[ index + 3 ], tension, color, bitmapContext, pixelWidth, pixelHeight );
            }
        }

        internal static void DrawCurveClosed( this WriteableBitmap bmp, int[ ] points, float tension, Color color )
        {
            int num = (int) color.A + 1;
            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
            bmp.DrawCurveClosed( points, tension, color1 );
        }

        internal static void DrawCurveClosed( this WriteableBitmap bmp, int[ ] points, float tension, int color )
        {
            int pixelWidth = bmp.PixelWidth;
            int pixelHeight = bmp.PixelHeight;
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                int length = points.Length;
                WriteableBitmapExtensions.DrawCurveSegment( points[ length - 2 ], points[ length - 1 ], points[ 0 ], points[ 1 ], points[ 2 ], points[ 3 ], points[ 4 ], points[ 5 ], tension, color, bitmapContext, pixelWidth, pixelHeight );
                int index = 2;
                while ( index < length - 4 )
                {
                    WriteableBitmapExtensions.DrawCurveSegment( points[ index - 2 ], points[ index - 1 ], points[ index ], points[ index + 1 ], points[ index + 2 ], points[ index + 3 ], points[ index + 4 ], points[ index + 5 ], tension, color, bitmapContext, pixelWidth, pixelHeight );
                    index += 2;
                }
                WriteableBitmapExtensions.DrawCurveSegment( points[ index - 2 ], points[ index - 1 ], points[ index ], points[ index + 1 ], points[ index + 2 ], points[ index + 3 ], points[ 0 ], points[ 1 ], tension, color, bitmapContext, pixelWidth, pixelHeight );
                WriteableBitmapExtensions.DrawCurveSegment( points[ index ], points[ index + 1 ], points[ index + 2 ], points[ index + 3 ], points[ 0 ], points[ 1 ], points[ 2 ], points[ 3 ], tension, color, bitmapContext, pixelWidth, pixelHeight );
            }
        }

        internal static WriteableBitmap Resize( this WriteableBitmap bmp, int width, int height, WriteableBitmapExtensions.Interpolation interpolation )
        {
            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
            {
                int[] src = WriteableBitmapExtensions.Resize(bitmapContext, bmp.PixelWidth, bmp.PixelHeight, width, height, interpolation);
                WriteableBitmap writeableBitmap = BitmapFactory.New(width, height);
                BitmapContext.BlockCopy( src, 0, bitmapContext, 0, 4 * src.Length );
                return writeableBitmap;
            }
        }

        internal static unsafe int[ ] Resize( BitmapContext srcContext, int widthSource, int heightSource, int width, int height, WriteableBitmapExtensions.Interpolation interpolation )
        {
            int* pixels = srcContext.Pixels;
            int[] numArray = new int[width * height];
            float num1 = (float) widthSource / (float) width;
            float num2 = (float) heightSource / (float) height;
            switch ( interpolation )
            {
                case WriteableBitmapExtensions.Interpolation.NearestNeighbor:
                    int num3 = 0;
                    for ( int index1 = 0 ; index1 < height ; ++index1 )
                    {
                        for ( int index2 = 0 ; index2 < width ; ++index2 )
                        {
                            float num4 = (float) index2 * num1;
                            double num5 = (double) index1 * (double) num2;
                            int num6 = (int) num4;
                            int num7 = (int) num5;
                            numArray[ num3++ ] = pixels[ num7 * widthSource + num6 ];
                        }
                    }
                    break;
                case WriteableBitmapExtensions.Interpolation.Bilinear:
                    int num8 = 0;
                    for ( int index1 = 0 ; index1 < height ; ++index1 )
                    {
                        for ( int index2 = 0 ; index2 < width ; ++index2 )
                        {
                            float num4 = (float) index2 * num1;
                            double num5 = (double) index1 * (double) num2;
                            int num6 = (int) num4;
                            int num7 = (int) num5;
                            float num9 = num4 - (float) num6;
                            float num10 = (float) num5 - (float) num7;
                            double num11 = 1.0 - (double) num9;
                            float num12 = 1f - num10;
                            int num13 = num6 + 1;
                            if ( num13 >= widthSource )
                                num13 = num6;
                            int num14 = num7 + 1;
                            if ( num14 >= heightSource )
                                num14 = num7;
                            int num15 = pixels[num7 * widthSource + num6];
                            byte num16 = (byte) (num15 >> 24);
                            byte num17 = (byte) (num15 >> 16);
                            byte num18 = (byte) (num15 >> 8);
                            byte num19 = (byte) num15;
                            int num20 = pixels[num7 * widthSource + num13];
                            byte num21 = (byte) (num20 >> 24);
                            byte num22 = (byte) (num20 >> 16);
                            byte num23 = (byte) (num20 >> 8);
                            byte num24 = (byte) num20;
                            int num25 = pixels[num14 * widthSource + num6];
                            byte num26 = (byte) (num25 >> 24);
                            byte num27 = (byte) (num25 >> 16);
                            byte num28 = (byte) (num25 >> 8);
                            byte num29 = (byte) num25;
                            int num30 = pixels[num14 * widthSource + num13];
                            byte num31 = (byte) (num30 >> 24);
                            byte num32 = (byte) (num30 >> 16);
                            byte num33 = (byte) (num30 >> 8);
                            byte num34 = (byte) num30;
                            float num35 = (float) (num11 * (double) num16 + (double) num9 * (double) num21);
                            float num36 = (float) (num11 * (double) num26 + (double) num9 * (double) num31);
                            byte num37 = (byte) ((double) num12 * (double) num35 + (double) num10 * (double) num36);
                            float num38 = (float) (num11 * (double) num17 * (double) num16 + (double) num9 * (double) num22 * (double) num21);
                            float num39 = (float) (num11 * (double) num27 * (double) num26 + (double) num9 * (double) num32 * (double) num31);
                            float num40 = (float) ((double) num12 * (double) num38 + (double) num10 * (double) num39);
                            float num41 = (float) (num11 * (double) num18 * (double) num16 + (double) num9 * (double) num23 * (double) num21);
                            float num42 = (float) (num11 * (double) num28 * (double) num26 + (double) num9 * (double) num33 * (double) num31);
                            float num43 = (float) ((double) num12 * (double) num41 + (double) num10 * (double) num42);
                            float num44 = (float) (num11 * (double) num19 * (double) num16 + (double) num9 * (double) num24 * (double) num21);
                            float num45 = (float) (num11 * (double) num29 * (double) num26 + (double) num9 * (double) num34 * (double) num31);
                            float num46 = (float) ((double) num12 * (double) num44 + (double) num10 * (double) num45);
                            if ( num37 > ( byte ) 0 )
                            {
                                num40 /= ( float ) num37;
                                num43 /= ( float ) num37;
                                num46 /= ( float ) num37;
                            }
                            byte num47 = (byte) num40;
                            byte num48 = (byte) num43;
                            byte num49 = (byte) num46;
                            numArray[ num8++ ] = ( int ) num37 << 24 | ( int ) num47 << 16 | ( int ) num48 << 8 | ( int ) num49;
                        }
                    }
                    break;
            }
            return numArray;
        }

        internal static unsafe WriteableBitmap Rotate( this WriteableBitmap bmp, int angle )
        {
            int pixelWidth = bmp.PixelWidth;
            int pixelHeight = bmp.PixelHeight;
            using ( BitmapContext bitmapContext1 = bmp.GetBitmapContext() )
            {
                int* pixels1 = bitmapContext1.Pixels;
                int index1 = 0;
                angle %= 360;
                WriteableBitmap bmp1;
                if ( angle > 0 && angle <= 90 )
                {
                    bmp1 = BitmapFactory.New( pixelHeight, pixelWidth );
                    using ( BitmapContext bitmapContext2 = bmp1.GetBitmapContext() )
                    {
                        int* pixels2 = bitmapContext2.Pixels;
                        for ( int index2 = 0 ; index2 < pixelWidth ; ++index2 )
                        {
                            for ( int index3 = pixelHeight - 1 ; index3 >= 0 ; --index3 )
                            {
                                int index4 = index3 * pixelWidth + index2;
                                pixels2[ index1 ] = pixels1[ index4 ];
                                ++index1;
                            }
                        }
                    }
                }
                else if ( angle > 90 && angle <= 180 )
                {
                    bmp1 = BitmapFactory.New( pixelWidth, pixelHeight );
                    using ( BitmapContext bitmapContext2 = bmp1.GetBitmapContext() )
                    {
                        int* pixels2 = bitmapContext2.Pixels;
                        for ( int index2 = pixelHeight - 1 ; index2 >= 0 ; --index2 )
                        {
                            for ( int index3 = pixelWidth - 1 ; index3 >= 0 ; --index3 )
                            {
                                int index4 = index2 * pixelWidth + index3;
                                pixels2[ index1 ] = pixels1[ index4 ];
                                ++index1;
                            }
                        }
                    }
                }
                else if ( angle > 180 && angle <= 270 )
                {
                    bmp1 = BitmapFactory.New( pixelHeight, pixelWidth );
                    using ( BitmapContext bitmapContext2 = bmp1.GetBitmapContext() )
                    {
                        int* pixels2 = bitmapContext2.Pixels;
                        for ( int index2 = pixelWidth - 1 ; index2 >= 0 ; --index2 )
                        {
                            for ( int index3 = 0 ; index3 < pixelHeight ; ++index3 )
                            {
                                int index4 = index3 * pixelWidth + index2;
                                pixels2[ index1 ] = pixels1[ index4 ];
                                ++index1;
                            }
                        }
                    }
                }
                else
                    bmp1 = bmp.Clone();
                return bmp1;
            }
        }

        internal static unsafe WriteableBitmap RotateFree( this WriteableBitmap bmp, double angle, bool crop = true )
        {
            double num1 = -1.0 * Math.PI / 180.0 * angle;
            int pixelWidth1 = bmp.PixelWidth;
            int pixelHeight1 = bmp.PixelHeight;
            int pixelWidth2;
            int pixelHeight2;
            if ( crop )
            {
                pixelWidth2 = pixelWidth1;
                pixelHeight2 = pixelHeight1;
            }
            else
            {
                double num2 = angle / (180.0 / Math.PI);
                pixelWidth2 = ( int ) Math.Ceiling( Math.Abs( Math.Sin( num2 ) * ( double ) pixelHeight1 ) + Math.Abs( Math.Cos( num2 ) * ( double ) pixelWidth1 ) );
                pixelHeight2 = ( int ) Math.Ceiling( Math.Abs( Math.Sin( num2 ) * ( double ) pixelWidth1 ) + Math.Abs( Math.Cos( num2 ) * ( double ) pixelHeight1 ) );
            }
            int num3 = pixelWidth1 / 2;
            int num4 = pixelHeight1 / 2;
            int num5 = pixelWidth2 / 2;
            int num6 = pixelHeight2 / 2;
            WriteableBitmap bmp1 = BitmapFactory.New(pixelWidth2, pixelHeight2);
            using ( BitmapContext bitmapContext1 = bmp1.GetBitmapContext() )
            {
                using ( BitmapContext bitmapContext2 = bmp.GetBitmapContext() )
                {
                    int* pixels1 = bitmapContext1.Pixels;
                    int* pixels2 = bitmapContext2.Pixels;
                    int pixelWidth3 = bmp.PixelWidth;
                    for ( int index1 = 0 ; index1 < pixelHeight2 ; ++index1 )
                    {
                        for ( int index2 = 0 ; index2 < pixelWidth2 ; ++index2 )
                        {
                            int num2 = index2 - num5;
                            int num7 = num6 - index1;
                            double num8 = Math.Sqrt((double) (num2 * num2 + num7 * num7));
                            double num9;
                            if ( num2 == 0 )
                            {
                                if ( num7 == 0 )
                                {
                                    pixels1[ index1 * pixelWidth2 + index2 ] = pixels2[ num4 * pixelWidth3 + num3 ];
                                    continue;
                                }
                                num9 = num7 >= 0 ? Math.PI / 2.0 : 3.0 * Math.PI / 2.0;
                            }
                            else
                                num9 = Math.Atan2( ( double ) num7, ( double ) num2 );
                            double num10 = num9 - num1;
                            double num11 = num8 * Math.Cos(num10);
                            double num12 = num8 * Math.Sin(num10);
                            double num13 = num11 + (double) num3;
                            double num14 = (double) num4 - num12;
                            int x1 = (int) Math.Floor(num13);
                            int y1 = (int) Math.Floor(num14);
                            int x2 = (int) Math.Ceiling(num13);
                            int y2 = (int) Math.Ceiling(num14);
                            if ( x1 >= 0 && x2 >= 0 && ( x1 < pixelWidth1 && x2 < pixelWidth1 ) && ( y1 >= 0 && y2 >= 0 && ( y1 < pixelHeight1 && y2 < pixelHeight1 ) ) )
                            {
                                double num15 = num13 - (double) x1;
                                double num16 = num14 - (double) y1;
                                Color pixel1 = bmp.GetPixel(x1, y1);
                                Color pixel2 = bmp.GetPixel(x2, y1);
                                Color pixel3 = bmp.GetPixel(x1, y2);
                                Color pixel4 = bmp.GetPixel(x2, y2);
                                double num17 = (1.0 - num15) * (double) pixel1.R + num15 * (double) pixel2.R;
                                double num18 = (1.0 - num15) * (double) pixel1.G + num15 * (double) pixel2.G;
                                double num19 = (1.0 - num15) * (double) pixel1.B + num15 * (double) pixel2.B;
                                double num20 = (1.0 - num15) * (double) pixel1.A + num15 * (double) pixel2.A;
                                double num21 = (1.0 - num15) * (double) pixel3.R + num15 * (double) pixel4.R;
                                double num22 = (1.0 - num15) * (double) pixel3.G + num15 * (double) pixel4.G;
                                double num23 = (1.0 - num15) * (double) pixel3.B + num15 * (double) pixel4.B;
                                double num24 = (1.0 - num15) * (double) pixel3.A + num15 * (double) pixel4.A;
                                int num25 = (int) Math.Round((1.0 - num16) * num17 + num16 * num21);
                                int num26 = (int) Math.Round((1.0 - num16) * num18 + num16 * num22);
                                int num27 = (int) Math.Round((1.0 - num16) * num19 + num16 * num23);
                                int num28 = (int) Math.Round((1.0 - num16) * num20 + num16 * num24);
                                if ( num25 < 0 )
                                    num25 = 0;
                                if ( num25 > ( int ) byte.MaxValue )
                                    num25 = ( int ) byte.MaxValue;
                                if ( num26 < 0 )
                                    num26 = 0;
                                if ( num26 > ( int ) byte.MaxValue )
                                    num26 = ( int ) byte.MaxValue;
                                if ( num27 < 0 )
                                    num27 = 0;
                                if ( num27 > ( int ) byte.MaxValue )
                                    num27 = ( int ) byte.MaxValue;
                                if ( num28 < 0 )
                                    num28 = 0;
                                if ( num28 > ( int ) byte.MaxValue )
                                    num28 = ( int ) byte.MaxValue;
                                int num29 = num28 + 1;
                                pixels1[ index1 * pixelWidth2 + index2 ] = num28 << 24 | ( int ) ( byte ) ( num25 * num29 >> 8 ) << 16 | ( int ) ( byte ) ( num26 * num29 >> 8 ) << 8 | ( int ) ( byte ) ( num27 * num29 >> 8 );
                            }
                        }
                    }
                    return bmp1;
                }
            }
        }

        internal static unsafe WriteableBitmap Flip( this WriteableBitmap bmp, WriteableBitmapExtensions.FlipMode flipMode )
        {
            int pixelWidth = bmp.PixelWidth;
            int pixelHeight = bmp.PixelHeight;
            using ( BitmapContext bitmapContext1 = bmp.GetBitmapContext() )
            {
                int* pixels1 = bitmapContext1.Pixels;
                int index1 = 0;
                WriteableBitmap bmp1 = (WriteableBitmap) null;
                switch ( flipMode )
                {
                    case WriteableBitmapExtensions.FlipMode.Vertical:
                        bmp1 = BitmapFactory.New( pixelWidth, pixelHeight );
                        using ( BitmapContext bitmapContext2 = bmp1.GetBitmapContext() )
                        {
                            int* pixels2 = bitmapContext2.Pixels;
                            for ( int index2 = 0 ; index2 < pixelHeight ; ++index2 )
                            {
                                for ( int index3 = pixelWidth - 1 ; index3 >= 0 ; --index3 )
                                {
                                    int index4 = index2 * pixelWidth + index3;
                                    pixels2[ index1 ] = pixels1[ index4 ];
                                    ++index1;
                                }
                            }
                            break;
                        }
                    case WriteableBitmapExtensions.FlipMode.Horizontal:
                        bmp1 = BitmapFactory.New( pixelWidth, pixelHeight );
                        using ( BitmapContext bitmapContext2 = bmp1.GetBitmapContext() )
                        {
                            int* pixels2 = bitmapContext2.Pixels;
                            for ( int index2 = pixelHeight - 1 ; index2 >= 0 ; --index2 )
                            {
                                for ( int index3 = 0 ; index3 < pixelWidth ; ++index3 )
                                {
                                    int index4 = index2 * pixelWidth + index3;
                                    pixels2[ index1 ] = pixels1[ index4 ];
                                    ++index1;
                                }
                            }
                            break;
                        }
                }
                return bmp1;
            }
        }

        internal enum BlendMode
        {
            Alpha,
            Additive,
            Subtractive,
            Mask,
            Multiply,
            ColorKeying,
            None,
        }

        internal enum Interpolation
        {
            NearestNeighbor,
            Bilinear,
        }

        internal enum FlipMode
        {
            Vertical,
            Horizontal,
        }
    }
}
