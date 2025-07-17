//// Decompiled with JetBrains decompiler
//// Type: System.Windows.Media.Imaging.WriteableBitmapExtensions
//// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
//// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
//// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

//using System.Collections.Generic;
//using System.IO;
//using System.Reflection;

//namespace System.Windows.Media.Imaging
//{
//    internal static class WriteableBitmapExtensions
//    {
//        private static readonly int[] leftEdgeX = new int[8192];
//        private static readonly int[] rightEdgeX = new int[8192];
//        internal static int[,] KernelGaussianBlur5x5 = new int[5, 5]
//    {
//      {
//        1,
//        4,
//        7,
//        4,
//        1
//      },
//      {
//        4,
//        16,
//        26,
//        16,
//        4
//      },
//      {
//        7,
//        26,
//        41,
//        26,
//        7
//      },
//      {
//        4,
//        16,
//        26,
//        16,
//        4
//      },
//      {
//        1,
//        4,
//        7,
//        4,
//        1
//      }
//    };
//        internal static int[,] KernelGaussianBlur3x3 = new int[3, 3]
//    {
//      {
//        16,
//        26,
//        16
//      },
//      {
//        26,
//        41,
//        26
//      },
//      {
//        16,
//        26,
//        16
//      }
//    };
//        internal static int[,] KernelSharpen3x3 = new int[3, 3]
//    {
//      {
//        0,
//        -2,
//        0
//      },
//      {
//        -2,
//        11,
//        -2
//      },
//      {
//        0,
//        -2,
//        0
//      }
//    };
//        internal const int SizeOfArgb = 4;
//        private const int WhiteR = 255;
//        private const int WhiteG = 255;
//        private const int WhiteB = 255;
//        private const byte INSIDE = 0;
//        private const byte LEFT = 1;
//        private const byte RIGHT = 2;
//        private const byte BOTTOM = 4;
//        private const byte TOP = 8;
//        private const float StepFactor = 2f;

//        private static void swap<T>( ref T a, ref T b )
//        {
//            T obj = a;
//            a = b;
//            b = obj;
//        }

//        private static unsafe void AALineQ1( int width, int height, BitmapContext context, int x1, int y1, int x2, int y2, int color, bool minEdge, bool leftEdge )
//        {
//            byte num1 = 0;
//            if ( minEdge )
//                num1 = byte.MaxValue;
//            if ( x1 == x2 || y1 == y2 )
//                return;
//            int* pixels = context.Pixels;
//            if ( y1 > y2 )
//            {
//                WriteableBitmapExtensions.swap<int>( ref x1, ref x2 );
//                WriteableBitmapExtensions.swap<int>( ref y1, ref y2 );
//            }
//            int num2 = x2 - x1;
//            int num3 = y2 - y1;
//            if ( x1 > x2 )
//                num2 = x1 - x2;
//            int num4 = x1;
//            int index = y1;
//            ushort num5 = num2 <= num3 ? (ushort) ((num2 << 16) / num3) : (ushort) ((num3 << 16) / num2);
//            byte num6 = (byte) (((long) color & 4278190080L) >> 24);
//            byte num7 = (byte) ((color & 16711680) >> 16);
//            byte num8 = (byte) ((color & 65280) >> 8);
//            byte num9 = (byte) (color & (int) byte.MaxValue);
//            ushort num10 = 0;
//            if ( num2 >= num3 )
//            {
//                while ( num2-- != 0 )
//                {
//                    if ( ( int ) ( ushort ) ( ( uint ) num10 + ( uint ) num5 ) <= ( int ) num10 )
//                        ++index;
//                    num10 += num5;
//                    if ( x1 < x2 )
//                        ++num4;
//                    else
//                        --num4;
//                    if ( index >= 0 && index < height )
//                    {
//                        if ( leftEdge )
//                            WriteableBitmapExtensions.leftEdgeX[ index ] = Math.Max( num4 + 1, WriteableBitmapExtensions.leftEdgeX[ index ] );
//                        else
//                            WriteableBitmapExtensions.rightEdgeX[ index ] = Math.Min( num4 - 1, WriteableBitmapExtensions.rightEdgeX[ index ] );
//                        if ( num4 >= 0 && num4 < width )
//                        {
//                            byte num11 = (byte) ((int) num6 * (int) (ushort) ((uint) (ushort) ((uint) num10 >> 8) ^ (uint) num1) >> 8);
//                            byte num12 = num7;
//                            byte num13 = num8;
//                            byte num14 = num9;
//                            int num15 = pixels[index * width + num4];
//                            byte num16 = (byte) ((num15 & 16711680) >> 16);
//                            byte num17 = (byte) ((num15 & 65280) >> 8);
//                            byte num18 = (byte) (num15 & (int) byte.MaxValue);
//                            byte num19 = (byte) ((int) num12 * (int) num11 + (int) num16 * ((int) byte.MaxValue - (int) num11) >> 8);
//                            byte num20 = (byte) ((int) num13 * (int) num11 + (int) num17 * ((int) byte.MaxValue - (int) num11) >> 8);
//                            byte num21 = (byte) ((int) num14 * (int) num11 + (int) num18 * ((int) byte.MaxValue - (int) num11) >> 8);
//                            pixels[ index * width + num4 ] = -16777216 | ( int ) num19 << 16 | ( int ) num20 << 8 | ( int ) num21;
//                        }
//                    }
//                }
//            }
//            else
//            {
//                byte num11 = (byte) ((uint) num1 ^ (uint) byte.MaxValue);
//                while ( --num3 != 0 )
//                {
//                    if ( ( int ) ( ushort ) ( ( uint ) num10 + ( uint ) num5 ) <= ( int ) num10 )
//                    {
//                        if ( x1 < x2 )
//                            ++num4;
//                        else
//                            --num4;
//                    }
//                    num10 += num5;
//                    ++index;
//                    if ( num4 >= 0 && num4 < width && ( index >= 0 && index < height ) )
//                    {
//                        byte num12 = (byte) ((int) num6 * (int) (ushort) ((uint) (ushort) ((uint) num10 >> 8) ^ (uint) num11) >> 8);
//                        byte num13 = num7;
//                        byte num14 = num8;
//                        byte num15 = num9;
//                        int num16 = pixels[index * width + num4];
//                        byte num17 = (byte) ((num16 & 16711680) >> 16);
//                        byte num18 = (byte) ((num16 & 65280) >> 8);
//                        byte num19 = (byte) (num16 & (int) byte.MaxValue);
//                        byte num20 = (byte) ((int) num13 * (int) num12 + (int) num17 * ((int) byte.MaxValue - (int) num12) >> 8);
//                        byte num21 = (byte) ((int) num14 * (int) num12 + (int) num18 * ((int) byte.MaxValue - (int) num12) >> 8);
//                        byte num22 = (byte) ((int) num15 * (int) num12 + (int) num19 * ((int) byte.MaxValue - (int) num12) >> 8);
//                        pixels[ index * width + num4 ] = -16777216 | ( int ) num20 << 16 | ( int ) num21 << 8 | ( int ) num22;
//                        if ( leftEdge )
//                            WriteableBitmapExtensions.leftEdgeX[ index ] = num4 + 1;
//                        else
//                            WriteableBitmapExtensions.rightEdgeX[ index ] = num4 - 1;
//                    }
//                }
//            }
//        }

//        private static unsafe void AAWidthLine( int width, int height, BitmapContext context, float x1, float y1, float x2, float y2, float lineWidth, int color )
//        {
//            if ( ( double ) lineWidth <= 0.0 )
//                return;
//            int* pixels = context.Pixels;
//            if ( ( double ) y1 > ( double ) y2 )
//            {
//                WriteableBitmapExtensions.swap<float>( ref x1, ref x2 );
//                WriteableBitmapExtensions.swap<float>( ref y1, ref y2 );
//            }
//            if ( ( double ) x1 == ( double ) x2 )
//            {
//                x1 -= ( float ) ( ( int ) lineWidth / 2 );
//                x2 += ( float ) ( ( int ) lineWidth / 2 );
//                if ( ( double ) x1 < 0.0 )
//                    x1 = 0.0f;
//                if ( ( double ) x2 < 0.0 || ( double ) x1 >= ( double ) width )
//                    return;
//                if ( ( double ) x2 >= ( double ) width )
//                    x2 = ( float ) ( width - 1 );
//                if ( ( double ) y1 >= ( double ) height || ( double ) y2 < 0.0 )
//                    return;
//                if ( ( double ) y1 < 0.0 )
//                    y1 = 0.0f;
//                if ( ( double ) y2 >= ( double ) height )
//                    y2 = ( float ) ( height - 1 );
//                for ( int index1 = ( int ) x1 ; ( double ) index1 <= ( double ) x2 ; ++index1 )
//                {
//                    for ( int index2 = ( int ) y1 ; ( double ) index2 <= ( double ) y2 ; ++index2 )
//                    {
//                        byte num1 = (byte) (((long) color & 4278190080L) >> 24);
//                        byte num2 = (byte) ((color & 16711680) >> 16);
//                        byte num3 = (byte) ((color & 65280) >> 8);
//                        byte num4 = (byte) (color & (int) byte.MaxValue);
//                        byte num5 = num2;
//                        byte num6 = num3;
//                        byte num7 = num4;
//                        int num8 = pixels[index2 * width + index1];
//                        byte num9 = (byte) ((num8 & 16711680) >> 16);
//                        byte num10 = (byte) ((num8 & 65280) >> 8);
//                        byte num11 = (byte) (num8 & (int) byte.MaxValue);
//                        byte num12 = (byte) ((int) num5 * (int) num1 + (int) num9 * ((int) byte.MaxValue - (int) num1) >> 8);
//                        byte num13 = (byte) ((int) num6 * (int) num1 + (int) num10 * ((int) byte.MaxValue - (int) num1) >> 8);
//                        byte num14 = (byte) ((int) num7 * (int) num1 + (int) num11 * ((int) byte.MaxValue - (int) num1) >> 8);
//                        pixels[ index2 * width + index1 ] = -16777216 | ( int ) num12 << 16 | ( int ) num13 << 8 | ( int ) num14;
//                    }
//                }
//            }
//            else if ( ( double ) y1 == ( double ) y2 )
//            {
//                if ( ( double ) x1 > ( double ) x2 )
//                    WriteableBitmapExtensions.swap<float>( ref x1, ref x2 );
//                y1 -= ( float ) ( ( int ) lineWidth / 2 );
//                y2 += ( float ) ( ( int ) lineWidth / 2 );
//                if ( ( double ) y1 < 0.0 )
//                    y1 = 0.0f;
//                if ( ( double ) y2 < 0.0 || ( double ) y1 >= ( double ) height )
//                    return;
//                if ( ( double ) y2 >= ( double ) height )
//                    x2 = ( float ) ( height - 1 );
//                if ( ( double ) x1 >= ( double ) width || ( double ) y2 < 0.0 )
//                    return;
//                if ( ( double ) x1 < 0.0 )
//                    x1 = 0.0f;
//                if ( ( double ) x2 >= ( double ) width )
//                    x2 = ( float ) ( width - 1 );
//                for ( int index1 = ( int ) x1 ; ( double ) index1 <= ( double ) x2 ; ++index1 )
//                {
//                    for ( int index2 = ( int ) y1 ; ( double ) index2 <= ( double ) y2 ; ++index2 )
//                    {
//                        byte num1 = (byte) (((long) color & 4278190080L) >> 24);
//                        byte num2 = (byte) ((color & 16711680) >> 16);
//                        byte num3 = (byte) ((color & 65280) >> 8);
//                        byte num4 = (byte) (color & (int) byte.MaxValue);
//                        byte num5 = num2;
//                        byte num6 = num3;
//                        byte num7 = num4;
//                        int num8 = pixels[index2 * width + index1];
//                        byte num9 = (byte) ((num8 & 16711680) >> 16);
//                        byte num10 = (byte) ((num8 & 65280) >> 8);
//                        byte num11 = (byte) (num8 & (int) byte.MaxValue);
//                        byte num12 = (byte) ((int) num5 * (int) num1 + (int) num9 * ((int) byte.MaxValue - (int) num1) >> 8);
//                        byte num13 = (byte) ((int) num6 * (int) num1 + (int) num10 * ((int) byte.MaxValue - (int) num1) >> 8);
//                        byte num14 = (byte) ((int) num7 * (int) num1 + (int) num11 * ((int) byte.MaxValue - (int) num1) >> 8);
//                        pixels[ index2 * width + index1 ] = -16777216 | ( int ) num12 << 16 | ( int ) num13 << 8 | ( int ) num14;
//                    }
//                }
//            }
//            else
//            {
//                ++y1;
//                ++y2;
//                float num1 = (float) (((double) y2 - (double) y1) / ((double) x2 - (double) x1));
//                float num2 = (float) (((double) x2 - (double) x1) / ((double) y2 - (double) y1));
//                float num3 = lineWidth;
//                float num4 = x2 - x1;
//                float num5 = y2 - y1;
//                float num6 = (float) ((double) num3 * (double) num5 / Math.Sqrt((double) num4 * (double) num4 + (double) num5 * (double) num5));
//                float num7 = (float) ((double) num3 * (double) num4 / Math.Sqrt((double) num4 * (double) num4 + (double) num5 * (double) num5));
//                float num8 = (float) ((double) num4 * (double) num5 / ((double) num4 * (double) num4 + (double) num5 * (double) num5));
//                x1 += num6 / 2f;
//                y1 -= num7 / 2f;
//                x2 += num6 / 2f;
//                y2 -= num7 / 2f;
//                float num9 = -num6;
//                float num10 = num7;
//                int num11 = (int) x1;
//                int index1 = (int) y1;
//                int num12 = (int) x2;
//                int index2 = (int) y2;
//                int num13 = (int) ((double) x1 + (double) num9);
//                int index3 = (int) ((double) y1 + (double) num10);
//                int num14 = (int) ((double) x2 + (double) num9);
//                int index4 = (int) ((double) y2 + (double) num10);
//                if ( ( double ) lineWidth == 2.0 )
//                {
//                    if ( ( double ) Math.Abs( num5 ) < ( double ) Math.Abs( num4 ) )
//                    {
//                        if ( ( double ) x1 < ( double ) x2 )
//                        {
//                            index3 = index1 + 2;
//                            index4 = index2 + 2;
//                        }
//                        else
//                        {
//                            index1 = index3 + 2;
//                            index2 = index4 + 2;
//                        }
//                    }
//                    else
//                    {
//                        num11 = num13 + 2;
//                        num12 = num14 + 2;
//                    }
//                }
//                int num15 = Math.Min(Math.Min(index1, index2), Math.Min(index3, index4));
//                int num16 = Math.Max(Math.Max(index1, index2), Math.Max(index3, index4));
//                if ( num15 < 0 )
//                    num15 = -1;
//                if ( num16 >= height )
//                    num16 = height + 1;
//                for ( int index5 = num15 + 1 ; index5 < num16 - 1 ; ++index5 )
//                {
//                    WriteableBitmapExtensions.leftEdgeX[ index5 ] = -65536;
//                    WriteableBitmapExtensions.rightEdgeX[ index5 ] = 32768;
//                }
//                WriteableBitmapExtensions.AALineQ1( width, height, context, num11, index1, num12, index2, color, ( double ) num10 > 0.0, false );
//                WriteableBitmapExtensions.AALineQ1( width, height, context, num13, index3, num14, index4, color, ( double ) num10 < 0.0, true );
//                if ( ( double ) lineWidth > 1.0 )
//                {
//                    WriteableBitmapExtensions.AALineQ1( width, height, context, num11, index1, num13, index3, color, true, ( double ) num10 > 0.0 );
//                    WriteableBitmapExtensions.AALineQ1( width, height, context, num12, index2, num14, index4, color, false, ( double ) num10 < 0.0 );
//                }
//                if ( ( double ) x1 < ( double ) x2 )
//                {
//                    if ( index2 >= 0 && index2 < height )
//                        WriteableBitmapExtensions.rightEdgeX[ index2 ] = Math.Min( num12, WriteableBitmapExtensions.rightEdgeX[ index2 ] );
//                    if ( index3 >= 0 && index3 < height )
//                        WriteableBitmapExtensions.leftEdgeX[ index3 ] = Math.Max( num13, WriteableBitmapExtensions.leftEdgeX[ index3 ] );
//                }
//                else
//                {
//                    if ( index1 >= 0 && index1 < height )
//                        WriteableBitmapExtensions.rightEdgeX[ index1 ] = Math.Min( num11, WriteableBitmapExtensions.rightEdgeX[ index1 ] );
//                    if ( index4 >= 0 && index4 < height )
//                        WriteableBitmapExtensions.leftEdgeX[ index4 ] = Math.Max( num14, WriteableBitmapExtensions.leftEdgeX[ index4 ] );
//                }
//                for ( int index5 = num15 + 1 ; index5 < num16 - 1 ; ++index5 )
//                {
//                    WriteableBitmapExtensions.leftEdgeX[ index5 ] = Math.Max( WriteableBitmapExtensions.leftEdgeX[ index5 ], 0 );
//                    WriteableBitmapExtensions.rightEdgeX[ index5 ] = Math.Min( WriteableBitmapExtensions.rightEdgeX[ index5 ], width - 1 );
//                    for ( int index6 = WriteableBitmapExtensions.leftEdgeX[ index5 ] ; index6 <= WriteableBitmapExtensions.rightEdgeX[ index5 ] ; ++index6 )
//                    {
//                        byte num17 = (byte) (((long) color & 4278190080L) >> 24);
//                        byte num18 = (byte) ((color & 16711680) >> 16);
//                        byte num19 = (byte) ((color & 65280) >> 8);
//                        byte num20 = (byte) (color & (int) byte.MaxValue);
//                        byte num21 = num18;
//                        byte num22 = num19;
//                        byte num23 = num20;
//                        int num24 = pixels[index5 * width + index6];
//                        byte num25 = (byte) ((num24 & 16711680) >> 16);
//                        byte num26 = (byte) ((num24 & 65280) >> 8);
//                        byte num27 = (byte) (num24 & (int) byte.MaxValue);
//                        byte num28 = (byte) ((int) num21 * (int) num17 + (int) num25 * ((int) byte.MaxValue - (int) num17) >> 8);
//                        byte num29 = (byte) ((int) num22 * (int) num17 + (int) num26 * ((int) byte.MaxValue - (int) num17) >> 8);
//                        byte num30 = (byte) ((int) num23 * (int) num17 + (int) num27 * ((int) byte.MaxValue - (int) num17) >> 8);
//                        pixels[ index5 * width + index6 ] = -16777216 | ( int ) num28 << 16 | ( int ) num29 << 8 | ( int ) num30;
//                    }
//                }
//            }
//        }

//        internal static void DrawLineAA( BitmapContext context, int pixelWidth, int pixelHeight, int x1, int y1, int x2, int y2, int color, int strokeThickness )
//        {
//            WriteableBitmapExtensions.AAWidthLine( pixelWidth, pixelHeight, context, ( float ) x1, ( float ) y1, ( float ) x2, ( float ) y2, ( float ) strokeThickness, color );
//        }

//        internal static void DrawLineAA( BitmapContext context, int pixelWidth, int pixelHeight, int x1, int y1, int x2, int y2, Color color, int strokeThickness )
//        {
//            int num = (int) color.A + 1;
//            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
//            WriteableBitmapExtensions.AAWidthLine( pixelWidth, pixelHeight, context, ( float ) x1, ( float ) y1, ( float ) x2, ( float ) y2, ( float ) strokeThickness, color1 );
//        }

//        internal static void DrawLineAA( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Color color, int strokeThickness )
//        {
//            int num = (int) color.A + 1;
//            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//                WriteableBitmapExtensions.AAWidthLine( bmp.PixelWidth, bmp.PixelHeight, bitmapContext, ( float ) x1, ( float ) y1, ( float ) x2, ( float ) y2, ( float ) strokeThickness, color1 );
//        }

//        internal static int ConvertColor( double opacity, Color color )
//        {
//            if ( opacity < 0.0 || opacity > 1.0 )
//                throw new ArgumentOutOfRangeException( nameof( opacity ), "Opacity must be between 0.0 and 1.0" );
//            color.A = ( byte ) ( ( double ) color.A * opacity );
//            return WriteableBitmapExtensions.ConvertColor( color );
//        }

//        internal static int ConvertColor( Color color )
//        {
//            if ( color.A == ( byte ) 0 )
//                return 0;
//            int num = (int) color.A + 1;
//            return ( int ) color.A << 24 | ( int ) ( byte ) ( ( int ) color.R * num >> 8 ) << 16 | ( int ) ( byte ) ( ( int ) color.G * num >> 8 ) << 8 | ( int ) ( byte ) ( ( int ) color.B * num >> 8 );
//        }

//        internal static unsafe void Clear( this WriteableBitmap bmp, Color color )
//        {
//            int num1 = (int) color.A + 1;
//            int num2 = (int) color.A << 24 | (int) (byte) ((int) color.R * num1 >> 8) << 16 | (int) (byte) ((int) color.G * num1 >> 8) << 8 | (int) (byte) ((int) color.B * num1 >> 8);
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                int* pixels = bitmapContext.Pixels;
//                int pixelWidth = bmp.PixelWidth;
//                int pixelHeight = bmp.PixelHeight;
//                int num3 = pixelWidth * 4;
//                for ( int index = 0 ; index < pixelWidth ; ++index )
//                    pixels[ index ] = num2;
//                int num4 = 1;
//                int num5 = 1;
//                while ( num5 < pixelHeight )
//                {
//                    BitmapContext.BlockCopy( bitmapContext, 0, bitmapContext, num5 * num3, num4 * num3 );
//                    num5 += num4;
//                    num4 = Math.Min( 2 * num4, pixelHeight - num5 );
//                }
//            }
//        }

//        internal static void Clear( this WriteableBitmap bmp )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//                bitmapContext.Clear();
//        }

//        internal static WriteableBitmap Clone( this WriteableBitmap bmp )
//        {
//            WriteableBitmap bmp1 = BitmapFactory.New(bmp.PixelWidth, bmp.PixelHeight);
//            using ( BitmapContext bitmapContext1 = bmp.GetBitmapContext( ReadWriteMode.ReadOnly ) )
//            {
//                using ( BitmapContext bitmapContext2 = bmp1.GetBitmapContext() )
//                    BitmapContext.BlockCopy( bitmapContext1, 0, bitmapContext2, 0, bitmapContext1.Length * 4 );
//            }
//            return bmp1;
//        }

//        internal static unsafe void ForEach( this WriteableBitmap bmp, Func<int, int, Color> func )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                int* pixels = bitmapContext.Pixels;
//                int pixelWidth = bmp.PixelWidth;
//                int pixelHeight = bmp.PixelHeight;
//                int num1 = 0;
//                for ( int index1 = 0 ; index1 < pixelHeight ; ++index1 )
//                {
//                    for ( int index2 = 0 ; index2 < pixelWidth ; ++index2 )
//                    {
//                        Color color = func(index2, index1);
//                        int num2 = (int) color.A + 1;
//                        pixels[ num1++ ] = ( int ) color.A << 24 | ( int ) ( byte ) ( ( int ) color.R * num2 >> 8 ) << 16 | ( int ) ( byte ) ( ( int ) color.G * num2 >> 8 ) << 8 | ( int ) ( byte ) ( ( int ) color.B * num2 >> 8 );
//                    }
//                }
//            }
//        }

//        internal static unsafe void ForEach( this WriteableBitmap bmp, Func<int, int, Color, Color> func )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                int* pixels = bitmapContext.Pixels;
//                int pixelWidth = bmp.PixelWidth;
//                int pixelHeight = bmp.PixelHeight;
//                int index1 = 0;
//                for ( int index2 = 0 ; index2 < pixelHeight ; ++index2 )
//                {
//                    for ( int index3 = 0 ; index3 < pixelWidth ; ++index3 )
//                    {
//                        int num1 = pixels[index1];
//                        Color color = func(index3, index2, Color.FromArgb((byte) (num1 >> 24), (byte) (num1 >> 16), (byte) (num1 >> 8), (byte) num1));
//                        int num2 = (int) color.A + 1;
//                        pixels[ index1++ ] = ( int ) color.A << 24 | ( int ) ( byte ) ( ( int ) color.R * num2 >> 8 ) << 16 | ( int ) ( byte ) ( ( int ) color.G * num2 >> 8 ) << 8 | ( int ) ( byte ) ( ( int ) color.B * num2 >> 8 );
//                    }
//                }
//            }
//        }

//        internal static unsafe int GetPixeli( this WriteableBitmap bmp, int x, int y )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//                return bitmapContext.Pixels[ y * bmp.PixelWidth + x ];
//        }

//        internal static unsafe Color GetPixel( this WriteableBitmap bmp, int x, int y )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                int num1 = bitmapContext.Pixels[y * bmp.PixelWidth + x];
//                byte a = (byte) (num1 >> 24);
//                int num2 = (int) a;
//                if ( num2 == 0 )
//                    num2 = 1;
//                int num3 = 65280 / num2;
//                return Color.FromArgb( a, ( byte ) ( ( num1 >> 16 & ( int ) byte.MaxValue ) * num3 >> 8 ), ( byte ) ( ( num1 >> 8 & ( int ) byte.MaxValue ) * num3 >> 8 ), ( byte ) ( ( num1 & ( int ) byte.MaxValue ) * num3 >> 8 ) );
//            }
//        }

//        internal static unsafe byte GetBrightness( this WriteableBitmap bmp, int x, int y )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext( ReadWriteMode.ReadOnly ) )
//            {
//                int num = bitmapContext.Pixels[y * bmp.PixelWidth + x];
//                return ( byte ) ( ( int ) ( byte ) ( num >> 16 ) * 6966 + ( int ) ( byte ) ( num >> 8 ) * 23436 + ( int ) ( byte ) num * 2366 >> 15 );
//            }
//        }

//        internal static unsafe void SetPixeli( this WriteableBitmap bmp, int index, byte r, byte g, byte b )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//                bitmapContext.Pixels[ index ] = -16777216 | ( int ) r << 16 | ( int ) g << 8 | ( int ) b;
//        }

//        internal static unsafe void SetPixel( this WriteableBitmap bmp, int x, int y, byte r, byte g, byte b )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//                bitmapContext.Pixels[ y * bmp.PixelWidth + x ] = -16777216 | ( int ) r << 16 | ( int ) g << 8 | ( int ) b;
//        }

//        internal static unsafe void SetPixeli( this WriteableBitmap bmp, int index, byte a, byte r, byte g, byte b )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//                bitmapContext.Pixels[ index ] = ( int ) a << 24 | ( int ) r << 16 | ( int ) g << 8 | ( int ) b;
//        }

//        internal static unsafe void SetPixel( this WriteableBitmap bmp, int x, int y, byte a, byte r, byte g, byte b )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//                bitmapContext.Pixels[ y * bmp.PixelWidth + x ] = ( int ) a << 24 | ( int ) r << 16 | ( int ) g << 8 | ( int ) b;
//        }

//        internal static unsafe void SetPixeli( this WriteableBitmap bmp, int index, Color color )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                int num = (int) color.A + 1;
//                bitmapContext.Pixels[ index ] = ( int ) color.A << 24 | ( int ) ( byte ) ( ( int ) color.R * num >> 8 ) << 16 | ( int ) ( byte ) ( ( int ) color.G * num >> 8 ) << 8 | ( int ) ( byte ) ( ( int ) color.B * num >> 8 );
//            }
//        }

//        internal static unsafe void SetPixel( this WriteableBitmap bmp, int x, int y, Color color )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                int num = (int) color.A + 1;
//                bitmapContext.Pixels[ y * bmp.PixelWidth + x ] = ( int ) color.A << 24 | ( int ) ( byte ) ( ( int ) color.R * num >> 8 ) << 16 | ( int ) ( byte ) ( ( int ) color.G * num >> 8 ) << 8 | ( int ) ( byte ) ( ( int ) color.B * num >> 8 );
//            }
//        }

//        internal static unsafe void SetPixeli( this WriteableBitmap bmp, int index, byte a, Color color )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                int num = (int) a + 1;
//                bitmapContext.Pixels[ index ] = ( int ) a << 24 | ( int ) ( byte ) ( ( int ) color.R * num >> 8 ) << 16 | ( int ) ( byte ) ( ( int ) color.G * num >> 8 ) << 8 | ( int ) ( byte ) ( ( int ) color.B * num >> 8 );
//            }
//        }

//        internal static unsafe void SetPixel( this WriteableBitmap bmp, int x, int y, byte a, Color color )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                int num = (int) a + 1;
//                bitmapContext.Pixels[ y * bmp.PixelWidth + x ] = ( int ) a << 24 | ( int ) ( byte ) ( ( int ) color.R * num >> 8 ) << 16 | ( int ) ( byte ) ( ( int ) color.G * num >> 8 ) << 8 | ( int ) ( byte ) ( ( int ) color.B * num >> 8 );
//            }
//        }

//        internal static unsafe void SetPixeli( this WriteableBitmap bmp, int index, int color )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//                bitmapContext.Pixels[ index ] = color;
//        }

//        internal static unsafe void SetPixel( this WriteableBitmap bmp, int x, int y, int color )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//                bitmapContext.Pixels[ y * bmp.PixelWidth + x ] = color;
//        }

//        internal static unsafe void DrawPixelsVertically( this WriteableBitmap bmp, int x, int yStartBottom, int yEndTop, IList<int> pixelColorsArgb, double opacity, bool yAxisIsFlipped )
//        {
//            int num1 = Math.Max(yStartBottom, yEndTop);
//            yEndTop = Math.Min( yStartBottom, yEndTop );
//            yStartBottom = num1;
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            if ( yStartBottom == yEndTop )
//                return;
//            int num2 = (int) (opacity * 256.0);
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                int* pixels = bitmapContext.Pixels;
//                int num3 = Math.Min(yStartBottom, pixelHeight);
//                int index1 = x + num3 * pixelWidth;
//                int num4 = num3;
//                while ( num4 >= yEndTop && num4 >= 0 )
//                {
//                    if ( num4 >= 0 && num4 < pixelHeight )
//                    {
//                        int index2 = (yStartBottom - num4) * pixelColorsArgb.Count / (yStartBottom - yEndTop);
//                        if ( yAxisIsFlipped )
//                            index2 = pixelColorsArgb.Count - 1 - index2;
//                        if ( index2 >= 0 && index2 < pixelColorsArgb.Count )
//                        {
//                            int num5 = pixelColorsArgb[index2];
//                            int num6 = (int) ((double) (num5 >> 24 & (int) byte.MaxValue) * opacity);
//                            if ( num6 == ( int ) byte.MaxValue )
//                                pixels[ index1 ] = num5;
//                            else if ( num6 > 0 )
//                            {
//                                int num7 = bitmapContext.Pixels[index1];
//                                int num8 = num7 >> 24 & (int) byte.MaxValue;
//                                int num9 = num7 >> 16 & (int) byte.MaxValue;
//                                int num10 = num7 >> 8 & (int) byte.MaxValue;
//                                int num11 = num7 & (int) byte.MaxValue;
//                                int num12 = num5 >> 16 & (int) byte.MaxValue;
//                                int num13 = num5 >> 8 & (int) byte.MaxValue;
//                                int num14 = num5 & (int) byte.MaxValue;
//                                int num15 = num12 * num6 / (int) byte.MaxValue + num9 * num8 * ((int) byte.MaxValue - num6) / 65025;
//                                int num16 = num13 * num6 / (int) byte.MaxValue + num10 * num8 * ((int) byte.MaxValue - num6) / 65025;
//                                int num17 = num14 * num6 / (int) byte.MaxValue + num11 * num8 * ((int) byte.MaxValue - num6) / 65025;
//                                int num18 = num6 + num8 * ((int) byte.MaxValue - num6) / (int) byte.MaxValue;
//                                bitmapContext.Pixels[ index1 ] = ( num18 << 24 ) + ( num15 << 16 ) + ( num16 << 8 ) + num17;
//                            }
//                        }
//                    }
//                    --num4;
//                    index1 -= pixelWidth;
//                }
//            }
//        }

//        internal static void Blit( this WriteableBitmap bmp, Rect destRect, WriteableBitmap source, Rect sourceRect, WriteableBitmapExtensions.BlendMode BlendMode )
//        {
//            bmp.Blit( destRect, source, sourceRect, Colors.White, BlendMode );
//        }

//        internal static void Blit( this WriteableBitmap bmp, Rect destRect, WriteableBitmap source, Rect sourceRect )
//        {
//            bmp.Blit( destRect, source, sourceRect, Colors.White, WriteableBitmapExtensions.BlendMode.Alpha );
//        }

//        internal static void Blit( this WriteableBitmap bmp, Point destPosition, WriteableBitmap source, Rect sourceRect, Color color, WriteableBitmapExtensions.BlendMode BlendMode )
//        {
//            Rect destRect = new Rect(destPosition, new Size(sourceRect.Width, sourceRect.Height));
//            bmp.Blit( destRect, source, sourceRect, color, BlendMode );
//        }

//        internal static unsafe void Blit( this WriteableBitmap bmp, Rect destRect, WriteableBitmap source, Rect sourceRect, Color color, WriteableBitmapExtensions.BlendMode BlendMode )
//        {
//            if ( color.A == ( byte ) 0 )
//                return;
//            int width1 = (int) destRect.Width;
//            int height = (int) destRect.Height;
//            int pixelWidth1 = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            Rect rect = new Rect(0.0, 0.0, (double) pixelWidth1, (double) pixelHeight);
//            rect.Intersect( destRect );
//            if ( rect.IsEmpty )
//                return;
//            int pixelWidth2 = source.PixelWidth;
//            using ( BitmapContext bitmapContext1 = source.GetBitmapContext() )
//            {
//                using ( BitmapContext bitmapContext2 = bmp.GetBitmapContext() )
//                {
//                    int* pixels1 = bitmapContext1.Pixels;
//                    int* pixels2 = bitmapContext2.Pixels;
//                    int length1 = bitmapContext1.Length;
//                    int length2 = bitmapContext2.Length;
//                    int x1 = (int) destRect.X;
//                    int y1 = (int) destRect.Y;
//                    int num1 = x1 + width1;
//                    int num2 = y1 + height;
//                    int num3 = 0;
//                    int num4 = 0;
//                    int num5 = 0;
//                    int num6 = 0;
//                    int a = (int) color.A;
//                    int r = (int) color.R;
//                    int g = (int) color.G;
//                    int b = (int) color.B;
//                    bool flag = color != Colors.White;
//                    int width2 = (int) sourceRect.Width;
//                    double num7 = sourceRect.Width / destRect.Width;
//                    double num8 = sourceRect.Height / destRect.Height;
//                    int x2 = (int) sourceRect.X;
//                    int y2 = (int) sourceRect.Y;
//                    int num9 = -1;
//                    int num10 = -1;
//                    double num11 = (double) y2;
//                    int num12 = y1;
//                    for ( int index1 = 0 ; index1 < height ; ++index1 )
//                    {
//                        if ( num12 >= 0 && num12 < pixelHeight )
//                        {
//                            double num13 = (double) x2;
//                            int index2 = x1 + num12 * pixelWidth1;
//                            int num14 = x1;
//                            int num15 = *pixels1;
//                            if ( BlendMode == WriteableBitmapExtensions.BlendMode.None && !flag )
//                            {
//                                int num16 = (int) num13 + (int) num11 * pixelWidth2;
//                                int num17 = num14 < 0 ? -num14 : 0;
//                                int num18 = num14 + num17;
//                                int num19 = pixelWidth2 - num17;
//                                int num20 = num18 + num19 < pixelWidth1 ? num19 : pixelWidth1 - num18;
//                                if ( num20 > width2 )
//                                    num20 = width2;
//                                if ( num20 > width1 )
//                                    num20 = width1;
//                                BitmapContext.BlockCopy( bitmapContext1, ( num16 + num17 ) * 4, bitmapContext2, ( index2 + num17 ) * 4, num20 * 4 );
//                            }
//                            else
//                            {
//                                for ( int index3 = 0 ; index3 < width1 ; ++index3 )
//                                {
//                                    if ( num14 >= 0 && num14 < pixelWidth1 )
//                                    {
//                                        if ( ( int ) num13 != num9 || ( int ) num11 != num10 )
//                                        {
//                                            int index4 = (int) num13 + (int) num11 * pixelWidth2;
//                                            if ( index4 >= 0 && index4 < length1 )
//                                            {
//                                                num15 = pixels1[ index4 ];
//                                                num6 = num15 >> 24 & ( int ) byte.MaxValue;
//                                                num3 = num15 >> 16 & ( int ) byte.MaxValue;
//                                                num4 = num15 >> 8 & ( int ) byte.MaxValue;
//                                                num5 = num15 & ( int ) byte.MaxValue;
//                                                if ( flag && num6 != 0 )
//                                                {
//                                                    num6 = num6 * a * 32897 >> 23;
//                                                    num3 = ( num3 * r * 32897 >> 23 ) * a * 32897 >> 23;
//                                                    num4 = ( num4 * g * 32897 >> 23 ) * a * 32897 >> 23;
//                                                    num5 = ( num5 * b * 32897 >> 23 ) * a * 32897 >> 23;
//                                                    num15 = num6 << 24 | num3 << 16 | num4 << 8 | num5;
//                                                }
//                                            }
//                                            else
//                                                num6 = 0;
//                                        }
//                                        switch ( BlendMode )
//                                        {
//                                            case WriteableBitmapExtensions.BlendMode.Mask:
//                                                int num16 = pixels2[index2];
//                                                int num17 = num16 >> 24 & (int) byte.MaxValue;
//                                                int num18 = num16 >> 16 & (int) byte.MaxValue;
//                                                int num19 = num16 >> 8 & (int) byte.MaxValue;
//                                                int num20 = num16 & (int) byte.MaxValue;
//                                                int num21 = num17 * num6 * 32897 >> 23 << 24 | num18 * num6 * 32897 >> 23 << 16 | num19 * num6 * 32897 >> 23 << 8 | num20 * num6 * 32897 >> 23;
//                                                pixels2[ index2 ] = num21;
//                                                break;
//                                            case WriteableBitmapExtensions.BlendMode.ColorKeying:
//                                                num3 = num15 >> 16 & ( int ) byte.MaxValue;
//                                                num4 = num15 >> 8 & ( int ) byte.MaxValue;
//                                                num5 = num15 & ( int ) byte.MaxValue;
//                                                if ( num3 != ( int ) color.R || num4 != ( int ) color.G || num5 != ( int ) color.B )
//                                                {
//                                                    pixels2[ index2 ] = num15;
//                                                    break;
//                                                }
//                                                break;
//                                            case WriteableBitmapExtensions.BlendMode.None:
//                                                pixels2[ index2 ] = num15;
//                                                break;
//                                            default:
//                                                if ( num6 > 0 )
//                                                {
//                                                    int num22 = pixels2[index2];
//                                                    int num23 = num22 >> 24 & (int) byte.MaxValue;
//                                                    if ( ( num6 == ( int ) byte.MaxValue || num23 == 0 ) && ( BlendMode != WriteableBitmapExtensions.BlendMode.Additive && BlendMode != WriteableBitmapExtensions.BlendMode.Subtractive ) && BlendMode != WriteableBitmapExtensions.BlendMode.Multiply )
//                                                    {
//                                                        pixels2[ index2 ] = num15;
//                                                        break;
//                                                    }
//                                                    int num24 = num22 >> 16 & (int) byte.MaxValue;
//                                                    int num25 = num22 >> 8 & (int) byte.MaxValue;
//                                                    int num26 = num22 & (int) byte.MaxValue;
//                                                    switch ( BlendMode )
//                                                    {
//                                                        case WriteableBitmapExtensions.BlendMode.Alpha:
//                                                            num22 = num6 + ( num23 * ( ( int ) byte.MaxValue - num6 ) * 32897 >> 23 ) << 24 | num3 + ( num24 * ( ( int ) byte.MaxValue - num6 ) * 32897 >> 23 ) << 16 | num4 + ( num25 * ( ( int ) byte.MaxValue - num6 ) * 32897 >> 23 ) << 8 | num5 + ( num26 * ( ( int ) byte.MaxValue - num6 ) * 32897 >> 23 );
//                                                            break;
//                                                        case WriteableBitmapExtensions.BlendMode.Additive:
//                                                            int num27 = (int) byte.MaxValue <= num6 + num23 ? (int) byte.MaxValue : num6 + num23;
//                                                            num22 = num27 << 24 | ( num27 <= num3 + num24 ? num27 : num3 + num24 ) << 16 | ( num27 <= num4 + num25 ? num27 : num4 + num25 ) << 8 | ( num27 <= num5 + num26 ? num27 : num5 + num26 );
//                                                            break;
//                                                        case WriteableBitmapExtensions.BlendMode.Subtractive:
//                                                            num22 = num23 << 24 | ( num3 >= num24 ? 0 : num3 - num24 ) << 16 | ( num4 >= num25 ? 0 : num4 - num25 ) << 8 | ( num5 >= num26 ? 0 : num5 - num26 );
//                                                            break;
//                                                        case WriteableBitmapExtensions.BlendMode.Multiply:
//                                                            int num28 = num6 * num23 + 128;
//                                                            int num29 = num3 * num24 + 128;
//                                                            int num30 = num4 * num25 + 128;
//                                                            int num31 = num5 * num26 + 128;
//                                                            int num32 = (num28 >> 8) + num28 >> 8;
//                                                            int num33 = (num29 >> 8) + num29 >> 8;
//                                                            int num34 = (num30 >> 8) + num30 >> 8;
//                                                            int num35 = (num31 >> 8) + num31 >> 8;
//                                                            num22 = num32 << 24 | ( num32 <= num33 ? num32 : num33 ) << 16 | ( num32 <= num34 ? num32 : num34 ) << 8 | ( num32 <= num35 ? num32 : num35 );
//                                                            break;
//                                                    }
//                                                    pixels2[ index2 ] = num22;
//                                                    break;
//                                                }
//                                                break;
//                                        }
//                                    }
//                                    ++num14;
//                                    ++index2;
//                                    num13 += num7;
//                                }
//                            }
//                        }
//                        num11 += num8;
//                        ++num12;
//                    }
//                }
//            }
//        }

//        internal static unsafe void Blit( BitmapContext destContext, int dpw, int dph, Rect destRect, BitmapContext srcContext, Rect sourceRect, int sourceWidth )
//        {
//            WriteableBitmapExtensions.BlendMode blendMode = WriteableBitmapExtensions.BlendMode.Alpha;
//            int width1 = (int) destRect.Width;
//            int height = (int) destRect.Height;
//            Rect rect = new Rect(0.0, 0.0, (double) dpw, (double) dph);
//            rect.Intersect( destRect );
//            if ( rect.IsEmpty )
//                return;
//            int* pixels1 = srcContext.Pixels;
//            int* pixels2 = destContext.Pixels;
//            int length = srcContext.Length;
//            int x1 = (int) destRect.X;
//            int y1 = (int) destRect.Y;
//            int num1 = 0;
//            int num2 = 0;
//            int num3 = 0;
//            int num4 = 0;
//            int width2 = (int) sourceRect.Width;
//            double num5 = sourceRect.Width / destRect.Width;
//            double num6 = sourceRect.Height / destRect.Height;
//            int x2 = (int) sourceRect.X;
//            int y2 = (int) sourceRect.Y;
//            int num7 = -1;
//            int num8 = -1;
//            double num9 = (double) y2;
//            int num10 = y1;
//            for ( int index1 = 0 ; index1 < height ; ++index1 )
//            {
//                if ( num10 >= 0 && num10 < dph )
//                {
//                    double num11 = (double) x2;
//                    int index2 = x1 + num10 * dpw;
//                    int num12 = x1;
//                    int num13 = *pixels1;
//                    if ( blendMode == WriteableBitmapExtensions.BlendMode.None )
//                    {
//                        int num14 = (int) num11 + (int) num9 * sourceWidth;
//                        int num15 = num12 < 0 ? -num12 : 0;
//                        int num16 = num12 + num15;
//                        int num17 = sourceWidth - num15;
//                        int num18 = num16 + num17 < dpw ? num17 : dpw - num16;
//                        if ( num18 > width2 )
//                            num18 = width2;
//                        if ( num18 > width1 )
//                            num18 = width1;
//                        BitmapContext.BlockCopy( srcContext, ( num14 + num15 ) * 4, destContext, ( index2 + num15 ) * 4, num18 * 4 );
//                    }
//                    else
//                    {
//                        for ( int index3 = 0 ; index3 < width1 ; ++index3 )
//                        {
//                            if ( num12 >= 0 && num12 < dpw )
//                            {
//                                if ( ( int ) num11 != num7 || ( int ) num9 != num8 )
//                                {
//                                    int index4 = (int) num11 + (int) num9 * sourceWidth;
//                                    if ( index4 >= 0 && index4 < length )
//                                    {
//                                        num13 = pixels1[ index4 ];
//                                        num4 = num13 >> 24 & ( int ) byte.MaxValue;
//                                        num1 = num13 >> 16 & ( int ) byte.MaxValue;
//                                        num2 = num13 >> 8 & ( int ) byte.MaxValue;
//                                        num3 = num13 & ( int ) byte.MaxValue;
//                                    }
//                                    else
//                                        num4 = 0;
//                                }
//                                switch ( blendMode )
//                                {
//                                    case WriteableBitmapExtensions.BlendMode.Mask:
//                                        int num14 = pixels2[index2];
//                                        int num15 = num14 >> 24 & (int) byte.MaxValue;
//                                        int num16 = num14 >> 16 & (int) byte.MaxValue;
//                                        int num17 = num14 >> 8 & (int) byte.MaxValue;
//                                        int num18 = num14 & (int) byte.MaxValue;
//                                        int num19 = num15 * num4 * 32897 >> 23 << 24 | num16 * num4 * 32897 >> 23 << 16 | num17 * num4 * 32897 >> 23 << 8 | num18 * num4 * 32897 >> 23;
//                                        pixels2[ index2 ] = num19;
//                                        break;
//                                    case WriteableBitmapExtensions.BlendMode.ColorKeying:
//                                        num1 = num13 >> 16 & ( int ) byte.MaxValue;
//                                        num2 = num13 >> 8 & ( int ) byte.MaxValue;
//                                        num3 = num13 & ( int ) byte.MaxValue;
//                                        if ( num1 != ( int ) byte.MaxValue || num2 != ( int ) byte.MaxValue || num3 != ( int ) byte.MaxValue )
//                                        {
//                                            pixels2[ index2 ] = num13;
//                                            break;
//                                        }
//                                        break;
//                                    case WriteableBitmapExtensions.BlendMode.None:
//                                        pixels2[ index2 ] = num13;
//                                        break;
//                                    default:
//                                        if ( num4 > 0 )
//                                        {
//                                            int num20 = pixels2[index2];
//                                            int num21 = num20 >> 24 & (int) byte.MaxValue;
//                                            if ( ( num4 == ( int ) byte.MaxValue || num21 == 0 ) && ( blendMode != WriteableBitmapExtensions.BlendMode.Additive && blendMode != WriteableBitmapExtensions.BlendMode.Subtractive ) && blendMode != WriteableBitmapExtensions.BlendMode.Multiply )
//                                            {
//                                                pixels2[ index2 ] = num13;
//                                                break;
//                                            }
//                                            int num22 = num20 >> 16 & (int) byte.MaxValue;
//                                            int num23 = num20 >> 8 & (int) byte.MaxValue;
//                                            int num24 = num20 & (int) byte.MaxValue;
//                                            switch ( blendMode )
//                                            {
//                                                case WriteableBitmapExtensions.BlendMode.Alpha:
//                                                    num20 = num4 + ( num21 * ( ( int ) byte.MaxValue - num4 ) * 32897 >> 23 ) << 24 | num1 + ( num22 * ( ( int ) byte.MaxValue - num4 ) * 32897 >> 23 ) << 16 | num2 + ( num23 * ( ( int ) byte.MaxValue - num4 ) * 32897 >> 23 ) << 8 | num3 + ( num24 * ( ( int ) byte.MaxValue - num4 ) * 32897 >> 23 );
//                                                    break;
//                                                case WriteableBitmapExtensions.BlendMode.Additive:
//                                                    int num25 = (int) byte.MaxValue <= num4 + num21 ? (int) byte.MaxValue : num4 + num21;
//                                                    num20 = num25 << 24 | ( num25 <= num1 + num22 ? num25 : num1 + num22 ) << 16 | ( num25 <= num2 + num23 ? num25 : num2 + num23 ) << 8 | ( num25 <= num3 + num24 ? num25 : num3 + num24 );
//                                                    break;
//                                                case WriteableBitmapExtensions.BlendMode.Subtractive:
//                                                    num20 = num21 << 24 | ( num1 >= num22 ? 0 : num1 - num22 ) << 16 | ( num2 >= num23 ? 0 : num2 - num23 ) << 8 | ( num3 >= num24 ? 0 : num3 - num24 );
//                                                    break;
//                                                case WriteableBitmapExtensions.BlendMode.Multiply:
//                                                    int num26 = num4 * num21 + 128;
//                                                    int num27 = num1 * num22 + 128;
//                                                    int num28 = num2 * num23 + 128;
//                                                    int num29 = num3 * num24 + 128;
//                                                    int num30 = (num26 >> 8) + num26 >> 8;
//                                                    int num31 = (num27 >> 8) + num27 >> 8;
//                                                    int num32 = (num28 >> 8) + num28 >> 8;
//                                                    int num33 = (num29 >> 8) + num29 >> 8;
//                                                    num20 = num30 << 24 | ( num30 <= num31 ? num30 : num31 ) << 16 | ( num30 <= num32 ? num30 : num32 ) << 8 | ( num30 <= num33 ? num30 : num33 );
//                                                    break;
//                                            }
//                                            pixels2[ index2 ] = num20;
//                                            break;
//                                        }
//                                        break;
//                                }
//                            }
//                            ++num12;
//                            ++index2;
//                            num11 += num5;
//                        }
//                    }
//                }
//                num9 += num6;
//                ++num10;
//            }
//        }

//        internal static byte[ ] ToByteArray( this WriteableBitmap bmp, int offset, int count )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                if ( count == -1 )
//                    count = bitmapContext.Length;
//                int count1 = count * 4;
//                byte[] dest = new byte[count1];
//                BitmapContext.BlockCopy( bitmapContext, offset, dest, 0, count1 );
//                return dest;
//            }
//        }

//        internal static byte[ ] ToByteArray( this WriteableBitmap bmp, int count )
//        {
//            return bmp.ToByteArray( 0, count );
//        }

//        internal static byte[ ] ToByteArray( this WriteableBitmap bmp )
//        {
//            return bmp.ToByteArray( 0, -1 );
//        }

//        internal static WriteableBitmap FromByteArray( this WriteableBitmap bmp, byte[ ] buffer, int offset, int count )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                BitmapContext.BlockCopy( buffer, offset, bitmapContext, 0, count );
//                return bmp;
//            }
//        }

//        internal static WriteableBitmap FromByteArray( this WriteableBitmap bmp, byte[ ] buffer, int count )
//        {
//            return bmp.FromByteArray( buffer, 0, count );
//        }

//        internal static WriteableBitmap FromByteArray( this WriteableBitmap bmp, byte[ ] buffer )
//        {
//            return bmp.FromByteArray( buffer, 0, buffer.Length );
//        }

//        internal static unsafe void WriteTga( this WriteableBitmap bmp, Stream destination )
//        {
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                int* pixels = bitmapContext.Pixels;
//                byte[] buffer1 = new byte[bitmapContext.Length * 4];
//                int index1 = 0;
//                int num1 = pixelWidth << 2;
//                int num2 = pixelWidth << 3;
//                int index2 = (pixelHeight - 1) * num1;
//                for ( int index3 = 0 ; index3 < pixelHeight ; ++index3 )
//                {
//                    for ( int index4 = 0 ; index4 < pixelWidth ; ++index4 )
//                    {
//                        int num3 = pixels[index1];
//                        buffer1[ index2 ] = ( byte ) ( num3 & ( int ) byte.MaxValue );
//                        buffer1[ index2 + 1 ] = ( byte ) ( num3 >> 8 & ( int ) byte.MaxValue );
//                        buffer1[ index2 + 2 ] = ( byte ) ( num3 >> 16 & ( int ) byte.MaxValue );
//                        buffer1[ index2 + 3 ] = ( byte ) ( num3 >> 24 );
//                        ++index1;
//                        index2 += 4;
//                    }
//                    index2 -= num2;
//                }
//                byte[] numArray = new byte[18];
//                numArray[ 2 ] = ( byte ) 2;
//                numArray[ 12 ] = ( byte ) ( pixelWidth & ( int ) byte.MaxValue );
//                numArray[ 13 ] = ( byte ) ( ( pixelWidth & 65280 ) >> 8 );
//                numArray[ 14 ] = ( byte ) ( pixelHeight & ( int ) byte.MaxValue );
//                numArray[ 15 ] = ( byte ) ( ( pixelHeight & 65280 ) >> 8 );
//                numArray[ 16 ] = ( byte ) 32;
//                byte[] buffer2 = numArray;
//                using ( BinaryWriter binaryWriter = new BinaryWriter( destination ) )
//                {
//                    binaryWriter.Write( buffer2 );
//                    binaryWriter.Write( buffer1 );
//                }
//            }
//        }

//        internal static WriteableBitmap FromResource( this WriteableBitmap bmp, string relativePath )
//        {
//            string name = new AssemblyName(Assembly.GetCallingAssembly().FullName).Name;
//            return bmp.FromContent( name + ";component/" + relativePath );
//        }

//        internal static WriteableBitmap FromContent( this WriteableBitmap bmp, string relativePath )
//        {
//            using ( Stream stream = Application.GetResourceStream( new Uri( relativePath, UriKind.Relative ) ).Stream )
//            {
//                BitmapImage bitmapImage = new BitmapImage();
//                bitmapImage.StreamSource = stream;
//                bmp = new WriteableBitmap( ( BitmapSource ) bitmapImage );
//                bitmapImage.UriSource = ( Uri ) null;
//                return bmp;
//            }
//        }

//        internal static void FillRectangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Color color )
//        {
//            int num = (int) color.A + 1;
//            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
//            bmp.FillRectangle( x1, y1, x2, y2, color1, WriteableBitmapExtensions.BlendMode.Alpha );
//        }

//        internal static unsafe void FillRectangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int color, WriteableBitmapExtensions.BlendMode blendMode = WriteableBitmapExtensions.BlendMode.Alpha )
//        {
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            int sa = color >> 24 & (int) byte.MaxValue;
//            int sr = color >> 16 & (int) byte.MaxValue;
//            int sg = color >> 8 & (int) byte.MaxValue;
//            int sb = color & (int) byte.MaxValue;
//            bool flag = blendMode == WriteableBitmapExtensions.BlendMode.None || sa == (int) byte.MaxValue;
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                int* pixels = bitmapContext.Pixels;
//                if ( x1 < 0 && x2 < 0 || y1 < 0 && y2 < 0 || ( x1 >= pixelWidth && x2 >= pixelWidth || y1 >= pixelHeight && y2 >= pixelHeight ) )
//                    return;
//                if ( x1 < 0 )
//                    x1 = 0;
//                if ( x1 >= pixelWidth )
//                    x1 = pixelWidth - 1;
//                if ( y1 < 0 )
//                    y1 = 0;
//                if ( y1 >= pixelHeight )
//                    y1 = pixelHeight - 1;
//                if ( x2 < 0 )
//                    x2 = 0;
//                if ( x2 >= pixelWidth )
//                    x2 = pixelWidth - 1;
//                if ( y2 < 0 )
//                    y2 = 0;
//                if ( y2 >= pixelHeight )
//                    y2 = pixelHeight - 1;
//                if ( y1 > y2 )
//                {
//                    y2 -= y1;
//                    y1 += y2;
//                    y2 = y1 - y2;
//                }
//                int num1 = y1 * pixelWidth;
//                int num2 = num1 + x1;
//                int num3 = num1 + x2;
//                for ( int index = num2 ; index <= num3 ; ++index )
//                    pixels[ index ] = flag ? color : WriteableBitmapExtensions.AlphaBlendColors( pixels[ index ], sa, sr, sg, sb );
//                int num4 = x2 - x1 + 1;
//                int srcOffset = num2 * 4;
//                int num5 = y2 * pixelWidth + x1;
//                int num6 = num2 + pixelWidth;
//                while ( num6 <= num5 )
//                {
//                    if ( flag )
//                    {
//                        BitmapContext.BlockCopy( bitmapContext, srcOffset, bitmapContext, num6 * 4, num4 * 4 );
//                    }
//                    else
//                    {
//                        for ( int index1 = 0 ; index1 < num4 ; ++index1 )
//                        {
//                            int index2 = num6 + index1;
//                            pixels[ index2 ] = WriteableBitmapExtensions.AlphaBlendColors( pixels[ index2 ], sa, sr, sg, sb );
//                        }
//                    }
//                    num6 += pixelWidth;
//                }
//            }
//        }

//        private static int AlphaBlendColors( int pixel, int sa, int sr, int sg, int sb )
//        {
//            int num1 = pixel;
//            int num2 = num1 >> 24 & (int) byte.MaxValue;
//            int num3 = num1 >> 16 & (int) byte.MaxValue;
//            int num4 = num1 >> 8 & (int) byte.MaxValue;
//            int num5 = num1 & (int) byte.MaxValue;
//            return sa + ( num2 * ( ( int ) byte.MaxValue - sa ) * 32897 >> 23 ) << 24 | sr + ( num3 * ( ( int ) byte.MaxValue - sa ) * 32897 >> 23 ) << 16 | sg + ( num4 * ( ( int ) byte.MaxValue - sa ) * 32897 >> 23 ) << 8 | sb + ( num5 * ( ( int ) byte.MaxValue - sa ) * 32897 >> 23 );
//        }

//        internal static unsafe void FillRectangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Func<int, int, int> colorCb, WriteableBitmapExtensions.BlendMode blendMode = WriteableBitmapExtensions.BlendMode.Alpha )
//        {
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                int* pixels = bitmapContext.Pixels;
//                if ( x1 < 0 && x2 < 0 || y1 < 0 && y2 < 0 || ( x1 >= pixelWidth && x2 >= pixelWidth || y1 >= pixelHeight && y2 >= pixelHeight ) )
//                    return;
//                if ( x1 < 0 )
//                    x1 = 0;
//                if ( y1 < 0 )
//                    y1 = 0;
//                if ( x2 < 0 )
//                    x2 = 0;
//                if ( y2 < 0 )
//                    y2 = 0;
//                if ( x1 > pixelWidth )
//                    x1 = pixelWidth;
//                if ( y1 > pixelHeight )
//                    y1 = pixelHeight;
//                if ( x2 > pixelWidth )
//                    x2 = pixelWidth;
//                if ( y2 > pixelHeight )
//                    y2 = pixelHeight;
//                if ( y1 > y2 )
//                {
//                    y2 -= y1;
//                    y1 += y2;
//                    y2 = y1 - y2;
//                }
//                int num1 = x2 - x1 + 1;
//                int num2 = y1 * pixelWidth + x1;
//                int num3 = y2 * pixelWidth + x1;
//                int num4 = y1;
//                int num5 = num2;
//                while ( num5 < num3 )
//                {
//                    int num6 = x1;
//                    int num7 = 0;
//                    while ( num7 < num1 )
//                    {
//                        int index = num5 + num7;
//                        int num8 = colorCb(num6, num4);
//                        int sa = num8 >> 24 & (int) byte.MaxValue;
//                        int sr = num8 >> 16 & (int) byte.MaxValue;
//                        int sg = num8 >> 8 & (int) byte.MaxValue;
//                        int sb = num8 & (int) byte.MaxValue;
//                        bool flag = blendMode == WriteableBitmapExtensions.BlendMode.None || sa == (int) byte.MaxValue;
//                        pixels[ index ] = flag ? num8 : WriteableBitmapExtensions.AlphaBlendColors( pixels[ index ], sa, sr, sg, sb );
//                        ++num7;
//                        ++num6;
//                    }
//                    num5 += pixelWidth;
//                    ++num4;
//                }
//            }
//        }

//        internal static void FillEllipse( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Color color )
//        {
//            int num = (int) color.A + 1;
//            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
//            bmp.FillEllipse( x1, y1, x2, y2, color1 );
//        }

//        internal static void FillEllipse( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int color )
//        {
//            int xr = x2 - x1 >> 1;
//            int yr = y2 - y1 >> 1;
//            int xc = x1 + xr;
//            int yc = y1 + yr;
//            bmp.FillEllipseCentered( xc, yc, xr, yr, color );
//        }

//        internal static void FillEllipseCentered( this WriteableBitmap bmp, int xc, int yc, int xr, int yr, Color color )
//        {
//            int num = (int) color.A + 1;
//            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
//            bmp.FillEllipseCentered( xc, yc, xr, yr, color1 );
//        }

//        internal static void FillEllipseCentered( this WriteableBitmap bmp, int xc, int yc, int xr, int yr, int color )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//                WriteableBitmapExtensions.FillEllipseCentered( bitmapContext, xc, yc, xr, yr, color, WriteableBitmapExtensions.BlendMode.Alpha );
//        }

//        internal static unsafe void FillEllipseCentered( BitmapContext context, int xc, int yc, int xr, int yr, int color, WriteableBitmapExtensions.BlendMode blendMode = WriteableBitmapExtensions.BlendMode.Alpha )
//        {
//            int* pixels = context.Pixels;
//            int pixelWidth = context.PixelWidth;
//            int pixelHeight = context.PixelHeight;
//            if ( xr < 1 || yr < 1 )
//                return;
//            int num1 = xr;
//            int num2 = 0;
//            int num3 = xr * xr << 1;
//            int num4 = yr * yr << 1;
//            int num5 = yr * yr * (1 - (xr << 1));
//            int num6 = xr * xr;
//            int num7 = 0;
//            int num8 = num4 * xr;
//            int num9 = 0;
//            int sa = color >> 24 & (int) byte.MaxValue;
//            int sr = color >> 16 & (int) byte.MaxValue;
//            int sg = color >> 8 & (int) byte.MaxValue;
//            int sb = color & (int) byte.MaxValue;
//            bool flag = blendMode == WriteableBitmapExtensions.BlendMode.None || sa == (int) byte.MaxValue;
//            while ( num8 >= num9 )
//            {
//                int num10 = yc + num2;
//                int num11 = yc - num2;
//                if ( num10 < 0 )
//                    num10 = 0;
//                if ( num10 >= pixelHeight )
//                    num10 = pixelHeight - 1;
//                if ( num11 < 0 )
//                    num11 = 0;
//                if ( num11 >= pixelHeight )
//                    num11 = pixelHeight - 1;
//                int num12 = num10 * pixelWidth;
//                int num13 = num11 * pixelWidth;
//                int num14 = xc + num1;
//                int num15 = xc - num1;
//                if ( num14 < 0 )
//                    num14 = 0;
//                if ( num14 >= pixelWidth )
//                    num14 = pixelWidth - 1;
//                if ( num15 < 0 )
//                    num15 = 0;
//                if ( num15 >= pixelWidth )
//                    num15 = pixelWidth - 1;
//                for ( int index = num15 ; index <= num14 ; ++index )
//                {
//                    pixels[ index + num12 ] = flag ? color : WriteableBitmapExtensions.AlphaBlendColors( pixels[ index + num12 ], sa, sr, sg, sb );
//                    pixels[ index + num13 ] = flag ? color : WriteableBitmapExtensions.AlphaBlendColors( pixels[ index + num13 ], sa, sr, sg, sb );
//                }
//                ++num2;
//                num9 += num3;
//                num7 += num6;
//                num6 += num3;
//                if ( num5 + ( num7 << 1 ) > 0 )
//                {
//                    --num1;
//                    num8 -= num4;
//                    num7 += num5;
//                    num5 += num4;
//                }
//            }
//            int num16 = 0;
//            int num17 = yr;
//            int num18 = yc + num17;
//            int num19 = yc - num17;
//            if ( num18 < 0 )
//                num18 = 0;
//            if ( num18 >= pixelHeight )
//                num18 = pixelHeight - 1;
//            if ( num19 < 0 )
//                num19 = 0;
//            if ( num19 >= pixelHeight )
//                num19 = pixelHeight - 1;
//            int num20 = num18 * pixelWidth;
//            int num21 = num19 * pixelWidth;
//            int num22 = yr * yr;
//            int num23 = xr * xr * (1 - (yr << 1));
//            int num24 = 0;
//            int num25 = 0;
//            int num26 = num3 * yr;
//            while ( num25 <= num26 )
//            {
//                int num10 = xc + num16;
//                int num11 = xc - num16;
//                if ( num10 < 0 )
//                    num10 = 0;
//                if ( num10 >= pixelWidth )
//                    num10 = pixelWidth - 1;
//                if ( num11 < 0 )
//                    num11 = 0;
//                if ( num11 >= pixelWidth )
//                    num11 = pixelWidth - 1;
//                for ( int index = num11 ; index <= num10 ; ++index )
//                {
//                    pixels[ index + num20 ] = flag ? color : WriteableBitmapExtensions.AlphaBlendColors( pixels[ index + num20 ], sa, sr, sg, sb );
//                    pixels[ index + num21 ] = flag ? color : WriteableBitmapExtensions.AlphaBlendColors( pixels[ index + num21 ], sa, sr, sg, sb );
//                }
//                ++num16;
//                num25 += num4;
//                num24 += num22;
//                num22 += num4;
//                if ( num23 + ( num24 << 1 ) > 0 )
//                {
//                    --num17;
//                    int num12 = yc + num17;
//                    int num13 = yc - num17;
//                    if ( num12 < 0 )
//                        num12 = 0;
//                    if ( num12 >= pixelHeight )
//                        num12 = pixelHeight - 1;
//                    if ( num13 < 0 )
//                        num13 = 0;
//                    if ( num13 >= pixelHeight )
//                        num13 = pixelHeight - 1;
//                    num20 = num12 * pixelWidth;
//                    num21 = num13 * pixelWidth;
//                    num26 -= num3;
//                    num24 += num23;
//                    num23 += num3;
//                }
//            }
//        }

//        internal static void FillPolygon( this WriteableBitmap bmp, int[ ] points, Color color )
//        {
//            int num = (int) color.A + 1;
//            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
//            bmp.FillPolygon( points, color1, WriteableBitmapExtensions.BlendMode.Alpha );
//        }

//        internal static unsafe void FillPolygon( this WriteableBitmap bmp, int[ ] points, int color, WriteableBitmapExtensions.BlendMode blendMode = WriteableBitmapExtensions.BlendMode.Alpha )
//        {
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            int sa = color >> 24 & (int) byte.MaxValue;
//            int sr = color >> 16 & (int) byte.MaxValue;
//            int sg = color >> 8 & (int) byte.MaxValue;
//            int sb = color & (int) byte.MaxValue;
//            bool flag = blendMode == WriteableBitmapExtensions.BlendMode.None || sa == (int) byte.MaxValue;
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                int* pixels = bitmapContext.Pixels;
//                int length = points.Length;
//                int[] numArray = new int[points.Length >> 1];
//                int num1 = pixelHeight;
//                int num2 = 0;
//                int index1 = 1;
//                while ( index1 < length )
//                {
//                    int point = points[index1];
//                    if ( point < num1 )
//                        num1 = point;
//                    if ( point > num2 )
//                        num2 = point;
//                    index1 += 2;
//                }
//                if ( num1 < 0 )
//                    num1 = 0;
//                if ( num2 >= pixelHeight )
//                    num2 = pixelHeight - 1;
//                for ( int index2 = num1 ; index2 <= num2 ; ++index2 )
//                {
//                    float num3 = (float) points[0];
//                    float num4 = (float) points[1];
//                    int num5 = 0;
//                    int index3 = 2;
//                    while ( index3 < length )
//                    {
//                        float point1 = (float) points[index3];
//                        float point2 = (float) points[index3 + 1];
//                        if ( ( double ) num4 < ( double ) index2 && ( double ) point2 >= ( double ) index2 || ( double ) point2 < ( double ) index2 && ( double ) num4 >= ( double ) index2 )
//                            numArray[ num5++ ] = ( int ) ( ( double ) num3 + ( ( double ) index2 - ( double ) num4 ) / ( ( double ) point2 - ( double ) num4 ) * ( ( double ) point1 - ( double ) num3 ) );
//                        num3 = point1;
//                        num4 = point2;
//                        index3 += 2;
//                    }
//                    for ( int index4 = 1 ; index4 < num5 ; ++index4 )
//                    {
//                        int num6 = numArray[index4];
//                        int index5;
//                        for ( index5 = index4 ; index5 > 0 && numArray[ index5 - 1 ] > num6 ; --index5 )
//                            numArray[ index5 ] = numArray[ index5 - 1 ];
//                        numArray[ index5 ] = num6;
//                    }
//                    int index6 = 0;
//                    while ( index6 < num5 - 1 )
//                    {
//                        int num6 = numArray[index6];
//                        int num7 = numArray[index6 + 1];
//                        if ( num7 > 0 && num6 < pixelWidth )
//                        {
//                            if ( num6 < 0 )
//                                num6 = 0;
//                            if ( num7 >= pixelWidth )
//                                num7 = pixelWidth - 1;
//                            for ( int index4 = num6 ; index4 <= num7 ; ++index4 )
//                            {
//                                int index5 = index2 * pixelWidth + index4;
//                                pixels[ index5 ] = flag ? color : WriteableBitmapExtensions.AlphaBlendColors( pixels[ index5 ], sa, sr, sg, sb );
//                            }
//                        }
//                        index6 += 2;
//                    }
//                }
//            }
//        }

//        internal static unsafe void FillPolygon( this WriteableBitmap bmp, int[ ] points, Func<int, int, int> colorCb, WriteableBitmapExtensions.BlendMode blendMode = WriteableBitmapExtensions.BlendMode.Alpha )
//        {
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                int* pixels = bitmapContext.Pixels;
//                int length = points.Length;
//                int[] numArray = new int[points.Length >> 1];
//                int num1 = pixelHeight;
//                int num2 = 0;
//                int index1 = 1;
//                while ( index1 < length )
//                {
//                    int point = points[index1];
//                    if ( point < num1 )
//                        num1 = point;
//                    if ( point > num2 )
//                        num2 = point;
//                    index1 += 2;
//                }
//                if ( num1 < 0 )
//                    num1 = 0;
//                if ( num2 >= pixelHeight )
//                    num2 = pixelHeight - 1;
//                for ( int index2 = num1 ; index2 <= num2 ; ++index2 )
//                {
//                    float num3 = (float) points[0];
//                    float num4 = (float) points[1];
//                    int num5 = 0;
//                    int index3 = 2;
//                    while ( index3 < length )
//                    {
//                        float point1 = (float) points[index3];
//                        float point2 = (float) points[index3 + 1];
//                        if ( ( double ) num4 < ( double ) index2 && ( double ) point2 >= ( double ) index2 || ( double ) point2 < ( double ) index2 && ( double ) num4 >= ( double ) index2 )
//                            numArray[ num5++ ] = ( int ) ( ( double ) num3 + ( ( double ) index2 - ( double ) num4 ) / ( ( double ) point2 - ( double ) num4 ) * ( ( double ) point1 - ( double ) num3 ) );
//                        num3 = point1;
//                        num4 = point2;
//                        index3 += 2;
//                    }
//                    for ( int index4 = 1 ; index4 < num5 ; ++index4 )
//                    {
//                        int num6 = numArray[index4];
//                        int index5;
//                        for ( index5 = index4 ; index5 > 0 && numArray[ index5 - 1 ] > num6 ; --index5 )
//                            numArray[ index5 ] = numArray[ index5 - 1 ];
//                        numArray[ index5 ] = num6;
//                    }
//                    int index6 = 0;
//                    while ( index6 < num5 - 1 )
//                    {
//                        int num6 = numArray[index6];
//                        int num7 = numArray[index6 + 1];
//                        if ( num7 > 0 && num6 < pixelWidth )
//                        {
//                            if ( num6 < 0 )
//                                num6 = 0;
//                            if ( num7 >= pixelWidth )
//                                num7 = pixelWidth - 1;
//                            for ( int index4 = num6 ; index4 <= num7 ; ++index4 )
//                            {
//                                int index5 = index2 * pixelWidth + index4;
//                                int num8 = colorCb(index4, index2);
//                                int sa = num8 >> 24 & (int) byte.MaxValue;
//                                int sr = num8 >> 16 & (int) byte.MaxValue;
//                                int sg = num8 >> 8 & (int) byte.MaxValue;
//                                int sb = num8 & (int) byte.MaxValue;
//                                bool flag = blendMode == WriteableBitmapExtensions.BlendMode.None || sa == (int) byte.MaxValue;
//                                pixels[ index5 ] = flag ? num8 : WriteableBitmapExtensions.AlphaBlendColors( pixels[ index5 ], sa, sr, sg, sb );
//                            }
//                        }
//                        index6 += 2;
//                    }
//                }
//            }
//        }

//        internal static void FillQuad( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, Color color )
//        {
//            int num = (int) color.A + 1;
//            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
//            bmp.FillQuad( x1, y1, x2, y2, x3, y3, x4, y4, color1 );
//        }

//        internal static void FillQuad( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, int color )
//        {
//            bmp.FillPolygon( new int[ 10 ]
//            {
//        x1,
//        y1,
//        x2,
//        y2,
//        x3,
//        y3,
//        x4,
//        y4,
//        x1,
//        y1
//            }, color, WriteableBitmapExtensions.BlendMode.Alpha );
//        }

//        internal static void FillTriangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int x3, int y3, Color color )
//        {
//            int num = (int) color.A + 1;
//            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
//            bmp.FillTriangle( x1, y1, x2, y2, x3, y3, color1 );
//        }

//        internal static void FillTriangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int x3, int y3, int color )
//        {
//            bmp.FillPolygon( new int[ 8 ]
//            {
//        x1,
//        y1,
//        x2,
//        y2,
//        x3,
//        y3,
//        x1,
//        y1
//            }, color, WriteableBitmapExtensions.BlendMode.Alpha );
//        }

//        private static unsafe List<int> ComputeBezierPoints( int x1, int y1, int cx1, int cy1, int cx2, int cy2, int x2, int y2, int color, BitmapContext context, int w, int h )
//        {
//            int* pixels = context.Pixels;
//            int num1 = Math.Min(x1, Math.Min(cx1, Math.Min(cx2, x2)));
//            int num2 = Math.Min(y1, Math.Min(cy1, Math.Min(cy2, y2)));
//            int num3 = Math.Max(x1, Math.Max(cx1, Math.Max(cx2, x2)));
//            int num4 = Math.Max(y1, Math.Max(cy1, Math.Max(cy2, y2)));
//            int num5 = num3 - num1;
//            int num6 = num4 - num2;
//            if ( num5 > num6 )
//                num6 = num5;
//            List<int> intList = new List<int>();
//            if ( num6 != 0 )
//            {
//                float num7 = 2f / (float) num6;
//                float num8 = 0.0f;
//                while ( ( double ) num8 <= 1.0 )
//                {
//                    float num9 = num8 * num8;
//                    float num10 = 1f - num8;
//                    float num11 = num10 * num10;
//                    int num12 = (int) ((double) num10 * (double) num11 * (double) x1 + 3.0 * (double) num8 * (double) num11 * (double) cx1 + 3.0 * (double) num10 * (double) num9 * (double) cx2 + (double) num8 * (double) num9 * (double) x2);
//                    int num13 = (int) ((double) num10 * (double) num11 * (double) y1 + 3.0 * (double) num8 * (double) num11 * (double) cy1 + 3.0 * (double) num10 * (double) num9 * (double) cy2 + (double) num8 * (double) num9 * (double) y2);
//                    intList.Add( num12 );
//                    intList.Add( num13 );
//                    num8 += num7;
//                }
//                intList.Add( x2 );
//                intList.Add( y2 );
//            }
//            return intList;
//        }

//        internal static void FillBeziers( this WriteableBitmap bmp, int[ ] points, Color color )
//        {
//            int num = (int) color.A + 1;
//            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
//            bmp.FillBeziers( points, color1 );
//        }

//        internal static void FillBeziers( this WriteableBitmap bmp, int[ ] points, int color )
//        {
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                int x1 = points[0];
//                int y1 = points[1];
//                List<int> intList = new List<int>();
//                int index = 2;
//                while ( index + 5 < points.Length )
//                {
//                    int point1 = points[index + 4];
//                    int point2 = points[index + 5];
//                    intList.AddRange( ( IEnumerable<int> ) WriteableBitmapExtensions.ComputeBezierPoints( x1, y1, points[ index ], points[ index + 1 ], points[ index + 2 ], points[ index + 3 ], point1, point2, color, bitmapContext, pixelWidth, pixelHeight ) );
//                    x1 = point1;
//                    y1 = point2;
//                    index += 6;
//                }
//                bmp.FillPolygon( intList.ToArray(), color, WriteableBitmapExtensions.BlendMode.Alpha );
//            }
//        }

//        private static unsafe List<int> ComputeSegmentPoints( int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, float tension, int color, BitmapContext context, int w, int h )
//        {
//            int* pixels = context.Pixels;
//            int num1 = Math.Min(x1, Math.Min(x2, Math.Min(x3, x4)));
//            int num2 = Math.Min(y1, Math.Min(y2, Math.Min(y3, y4)));
//            int num3 = Math.Max(x1, Math.Max(x2, Math.Max(x3, x4)));
//            int num4 = Math.Max(y1, Math.Max(y2, Math.Max(y3, y4)));
//            int num5 = num3 - num1;
//            int num6 = num4 - num2;
//            if ( num5 > num6 )
//                num6 = num5;
//            List<int> intList = new List<int>();
//            if ( num6 != 0 )
//            {
//                float num7 = 2f / (float) num6;
//                float num8 = tension * (float) (x3 - x1);
//                float num9 = tension * (float) (y3 - y1);
//                float num10 = tension * (float) (x4 - x2);
//                float num11 = tension * (float) (y4 - y2);
//                float num12 = num8 + num10 + (float) (2 * x2) - (float) (2 * x3);
//                float num13 = num9 + num11 + (float) (2 * y2) - (float) (2 * y3);
//                float num14 = -2f * num8 - num10 - (float) (3 * x2) + (float) (3 * x3);
//                float num15 = -2f * num9 - num11 - (float) (3 * y2) + (float) (3 * y3);
//                float num16 = 0.0f;
//                while ( ( double ) num16 <= 1.0 )
//                {
//                    float num17 = num16 * num16;
//                    int num18 = (int) ((double) num12 * (double) num17 * (double) num16 + (double) num14 * (double) num17 + (double) num8 * (double) num16 + (double) x2);
//                    int num19 = (int) ((double) num13 * (double) num17 * (double) num16 + (double) num15 * (double) num17 + (double) num9 * (double) num16 + (double) y2);
//                    intList.Add( num18 );
//                    intList.Add( num19 );
//                    num16 += num7;
//                }
//                intList.Add( x3 );
//                intList.Add( y3 );
//            }
//            return intList;
//        }

//        internal static void FillCurve( this WriteableBitmap bmp, int[ ] points, float tension, Color color )
//        {
//            int num = (int) color.A + 1;
//            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
//            bmp.FillCurve( points, tension, color1 );
//        }

//        internal static void FillCurve( this WriteableBitmap bmp, int[ ] points, float tension, int color )
//        {
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                List<int> segmentPoints = WriteableBitmapExtensions.ComputeSegmentPoints(points[0], points[1], points[0], points[1], points[2], points[3], points[4], points[5], tension, color, bitmapContext, pixelWidth, pixelHeight);
//                int index = 2;
//                while ( index < points.Length - 4 )
//                {
//                    segmentPoints.AddRange( ( IEnumerable<int> ) WriteableBitmapExtensions.ComputeSegmentPoints( points[ index - 2 ], points[ index - 1 ], points[ index ], points[ index + 1 ], points[ index + 2 ], points[ index + 3 ], points[ index + 4 ], points[ index + 5 ], tension, color, bitmapContext, pixelWidth, pixelHeight ) );
//                    index += 2;
//                }
//                segmentPoints.AddRange( ( IEnumerable<int> ) WriteableBitmapExtensions.ComputeSegmentPoints( points[ index - 2 ], points[ index - 1 ], points[ index ], points[ index + 1 ], points[ index + 2 ], points[ index + 3 ], points[ index + 2 ], points[ index + 3 ], tension, color, bitmapContext, pixelWidth, pixelHeight ) );
//                bmp.FillPolygon( segmentPoints.ToArray(), color, WriteableBitmapExtensions.BlendMode.Alpha );
//            }
//        }

//        internal static void FillCurveClosed( this WriteableBitmap bmp, int[ ] points, float tension, Color color )
//        {
//            int num = (int) color.A + 1;
//            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
//            bmp.FillCurveClosed( points, tension, color1 );
//        }

//        internal static void FillCurveClosed( this WriteableBitmap bmp, int[ ] points, float tension, int color )
//        {
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                int length = points.Length;
//                List<int> segmentPoints = WriteableBitmapExtensions.ComputeSegmentPoints(points[length - 2], points[length - 1], points[0], points[1], points[2], points[3], points[4], points[5], tension, color, bitmapContext, pixelWidth, pixelHeight);
//                int index = 2;
//                while ( index < length - 4 )
//                {
//                    segmentPoints.AddRange( ( IEnumerable<int> ) WriteableBitmapExtensions.ComputeSegmentPoints( points[ index - 2 ], points[ index - 1 ], points[ index ], points[ index + 1 ], points[ index + 2 ], points[ index + 3 ], points[ index + 4 ], points[ index + 5 ], tension, color, bitmapContext, pixelWidth, pixelHeight ) );
//                    index += 2;
//                }
//                segmentPoints.AddRange( ( IEnumerable<int> ) WriteableBitmapExtensions.ComputeSegmentPoints( points[ index - 2 ], points[ index - 1 ], points[ index ], points[ index + 1 ], points[ index + 2 ], points[ index + 3 ], points[ 0 ], points[ 1 ], tension, color, bitmapContext, pixelWidth, pixelHeight ) );
//                segmentPoints.AddRange( ( IEnumerable<int> ) WriteableBitmapExtensions.ComputeSegmentPoints( points[ index ], points[ index + 1 ], points[ index + 2 ], points[ index + 3 ], points[ 0 ], points[ 1 ], points[ 2 ], points[ 3 ], tension, color, bitmapContext, pixelWidth, pixelHeight ) );
//                bmp.FillPolygon( segmentPoints.ToArray(), color, WriteableBitmapExtensions.BlendMode.Alpha );
//            }
//        }

//        internal static WriteableBitmap Convolute( this WriteableBitmap bmp, int[ , ] kernel )
//        {
//            int kernelFactorSum = 0;
//            int[,] numArray = kernel;
//            int upperBound1 = numArray.GetUpperBound(0);
//            int upperBound2 = numArray.GetUpperBound(1);
//            for ( int lowerBound1 = numArray.GetLowerBound( 0 ) ; lowerBound1 <= upperBound1 ; ++lowerBound1 )
//            {
//                for ( int lowerBound2 = numArray.GetLowerBound( 1 ) ; lowerBound2 <= upperBound2 ; ++lowerBound2 )
//                {
//                    int num = numArray[lowerBound1, lowerBound2];
//                    kernelFactorSum += num;
//                }
//            }
//            return bmp.Convolute( kernel, kernelFactorSum, 0 );
//        }

//        internal static unsafe WriteableBitmap Convolute( this WriteableBitmap bmp, int[ , ] kernel, int kernelFactorSum, int kernelOffsetSum )
//        {
//            int num1 = kernel.GetUpperBound(0) + 1;
//            int num2 = kernel.GetUpperBound(1) + 1;
//            if ( ( num2 & 1 ) == 0 )
//                throw new InvalidOperationException( "Kernel width must be odd!" );
//            if ( ( num1 & 1 ) == 0 )
//                throw new InvalidOperationException( "Kernel height must be odd!" );
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            WriteableBitmap bmp1 = BitmapFactory.New(pixelWidth, pixelHeight);
//            using ( BitmapContext bitmapContext1 = bmp.GetBitmapContext( ReadWriteMode.ReadOnly ) )
//            {
//                using ( BitmapContext bitmapContext2 = bmp1.GetBitmapContext() )
//                {
//                    int* pixels1 = bitmapContext1.Pixels;
//                    int* pixels2 = bitmapContext2.Pixels;
//                    int num3 = 0;
//                    int num4 = num2 >> 1;
//                    int num5 = num1 >> 1;
//                    for ( int index1 = 0 ; index1 < pixelHeight ; ++index1 )
//                    {
//                        for ( int index2 = 0 ; index2 < pixelWidth ; ++index2 )
//                        {
//                            int num6 = 0;
//                            int num7 = 0;
//                            int num8 = 0;
//                            int num9 = 0;
//                            for ( int index3 = -num4 ; index3 <= num4 ; ++index3 )
//                            {
//                                int num10 = index3 + index2;
//                                if ( num10 < 0 )
//                                    num10 = 0;
//                                else if ( num10 >= pixelWidth )
//                                    num10 = pixelWidth - 1;
//                                for ( int index4 = -num5 ; index4 <= num5 ; ++index4 )
//                                {
//                                    int num11 = index4 + index1;
//                                    if ( num11 < 0 )
//                                        num11 = 0;
//                                    else if ( num11 >= pixelHeight )
//                                        num11 = pixelHeight - 1;
//                                    int num12 = pixels1[num11 * pixelWidth + num10];
//                                    int num13 = kernel[index4 + num4, index3 + num5];
//                                    num6 += ( num12 >> 24 & ( int ) byte.MaxValue ) * num13;
//                                    num7 += ( num12 >> 16 & ( int ) byte.MaxValue ) * num13;
//                                    num8 += ( num12 >> 8 & ( int ) byte.MaxValue ) * num13;
//                                    num9 += ( num12 & ( int ) byte.MaxValue ) * num13;
//                                }
//                            }
//                            int num14 = num6 / kernelFactorSum + kernelOffsetSum;
//                            int num15 = num7 / kernelFactorSum + kernelOffsetSum;
//                            int num16 = num8 / kernelFactorSum + kernelOffsetSum;
//                            int num17 = num9 / kernelFactorSum + kernelOffsetSum;
//                            byte num18 = num14 > (int) byte.MaxValue ? byte.MaxValue : (num14 < 0 ? (byte) 0 : (byte) num14);
//                            byte num19 = num15 > (int) byte.MaxValue ? byte.MaxValue : (num15 < 0 ? (byte) 0 : (byte) num15);
//                            byte num20 = num16 > (int) byte.MaxValue ? byte.MaxValue : (num16 < 0 ? (byte) 0 : (byte) num16);
//                            byte num21 = num17 > (int) byte.MaxValue ? byte.MaxValue : (num17 < 0 ? (byte) 0 : (byte) num17);
//                            pixels2[ num3++ ] = ( int ) num18 << 24 | ( int ) num19 << 16 | ( int ) num20 << 8 | ( int ) num21;
//                        }
//                    }
//                    return bmp1;
//                }
//            }
//        }

//        internal static unsafe WriteableBitmap Invert( this WriteableBitmap bmp )
//        {
//            WriteableBitmap bmp1 = BitmapFactory.New(bmp.PixelWidth, bmp.PixelHeight);
//            using ( BitmapContext bitmapContext1 = bmp1.GetBitmapContext() )
//            {
//                using ( BitmapContext bitmapContext2 = bmp.GetBitmapContext() )
//                {
//                    int* pixels1 = bitmapContext1.Pixels;
//                    int* pixels2 = bitmapContext2.Pixels;
//                    int length = bitmapContext2.Length;
//                    for ( int index = 0 ; index < length ; ++index )
//                    {
//                        int num1 = pixels2[index];
//                        int num2 = num1 >> 24 & (int) byte.MaxValue;
//                        int num3 = num1 >> 16 & (int) byte.MaxValue;
//                        int num4 = num1 >> 8 & (int) byte.MaxValue;
//                        int num5 = num1 & (int) byte.MaxValue;
//                        int num6 = (int) byte.MaxValue - num3;
//                        int num7 = (int) byte.MaxValue - num4;
//                        int num8 = (int) byte.MaxValue - num5;
//                        pixels1[ index ] = num2 << 24 | num6 << 16 | num7 << 8 | num8;
//                    }
//                    return bmp1;
//                }
//            }
//        }

//        internal static void DrawPennedLine( BitmapContext context, int w, int h, int x1, int y1, int x2, int y2, BitmapContext pen )
//        {
//            if ( y1 < 0 && y2 < 0 || y1 > h && y2 > h || x1 == x2 && y1 == y2 )
//                return;
//            int pixelWidth = pen.WriteableBitmap.PixelWidth;
//            int num1 = pixelWidth / 2;
//            Rect sourceRect = new Rect(0.0, 0.0, (double) pixelWidth, (double) pixelWidth);
//            int num2 = x2 - x1;
//            int num3 = y2 - y1;
//            int num4 = 0;
//            if ( num2 < 0 )
//            {
//                num2 = -num2;
//                num4 = -1;
//            }
//            else if ( num2 > 0 )
//                num4 = 1;
//            int num5 = 0;
//            if ( num3 < 0 )
//            {
//                num3 = -num3;
//                num5 = -1;
//            }
//            else if ( num3 > 0 )
//                num5 = 1;
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
//            Rect destRect = new Rect((double) (num12 - num1), (double) (num13 - num1), (double) pixelWidth, (double) pixelWidth);
//            if ( num13 < h && num13 >= 0 && ( num12 < w && num12 >= 0 ) )
//                WriteableBitmapExtensions.Blit( context, w, h, destRect, pen, sourceRect, pixelWidth );
//            for ( int index = 0 ; index < num11 ; ++index )
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
//                if ( num13 < h && num13 >= 0 && ( num12 < w && num12 >= 0 ) )
//                {
//                    destRect.X = ( double ) ( num12 - num1 );
//                    destRect.Y = ( double ) ( num13 - num1 );
//                    WriteableBitmapExtensions.Blit( context, w, h, destRect, pen, sourceRect, pixelWidth );
//                }
//            }
//        }

//        private static byte ComputeOutCode( Rect extents, double x, double y )
//        {
//            byte num = 0;
//            if ( x < extents.Left )
//                num |= ( byte ) 1;
//            else if ( x > extents.Right )
//                num |= ( byte ) 2;
//            if ( y > extents.Bottom )
//                num |= ( byte ) 4;
//            else if ( y < extents.Top )
//                num |= ( byte ) 8;
//            return num;
//        }

//        internal static bool CohenSutherlandLineClipWithViewPortOffset( Rect viewPort, ref float xi0, ref float yi0, ref float xi1, ref float yi1, int offset )
//        {
//            return WriteableBitmapExtensions.CohenSutherlandLineClip( new Rect( viewPort.X - ( double ) offset, viewPort.Y - ( double ) offset, viewPort.Width + ( double ) ( 2 * offset ), viewPort.Height + ( double ) ( 2 * offset ) ), ref xi0, ref yi0, ref xi1, ref yi1 );
//        }

//        internal static bool CohenSutherlandLineClip( Rect extents, ref float xi0, ref float yi0, ref float xi1, ref float yi1 )
//        {
//            double x0 = (double) xi0.ClipToInt();
//            double y0 = (double) yi0.ClipToInt();
//            double x1 = (double) xi1.ClipToInt();
//            double y1 = (double) yi1.ClipToInt();
//            bool flag = WriteableBitmapExtensions.CohenSutherlandLineClip(extents, ref x0, ref y0, ref x1, ref y1);
//            xi0 = ( float ) x0;
//            yi0 = ( float ) y0;
//            xi1 = ( float ) x1;
//            yi1 = ( float ) y1;
//            return flag;
//        }

//        internal static bool CohenSutherlandLineClip( Rect extents, ref int xi0, ref int yi0, ref int xi1, ref int yi1 )
//        {
//            double x0 = (double) xi0;
//            double y0 = (double) yi0;
//            double x1 = (double) xi1;
//            double y1 = (double) yi1;
//            bool flag = WriteableBitmapExtensions.CohenSutherlandLineClip(extents, ref x0, ref y0, ref x1, ref y1);
//            xi0 = ( int ) x0;
//            yi0 = ( int ) y0;
//            xi1 = ( int ) x1;
//            yi1 = ( int ) y1;
//            return flag;
//        }

//        internal static bool CohenSutherlandLineClip( Rect extents, ref double x0, ref double y0, ref double x1, ref double y1 )
//        {
//            byte outCode1 = WriteableBitmapExtensions.ComputeOutCode(extents, x0, y0);
//            byte outCode2 = WriteableBitmapExtensions.ComputeOutCode(extents, x1, y1);
//            if ( outCode1 == ( byte ) 0 && outCode2 == ( byte ) 0 )
//                return true;
//            bool flag = false;
//            while ( ( ( int ) outCode1 | ( int ) outCode2 ) != 0 )
//            {
//                if ( ( ( int ) outCode1 & ( int ) outCode2 ) == 0 )
//                {
//                    double num1 = x1;
//                    double num2 = y1;
//                    byte num3 = outCode1 != (byte) 0 ? outCode1 : outCode2;
//                    if ( ( ( int ) num3 & 8 ) != 0 )
//                    {
//                        if ( !double.IsInfinity( y0 ) )
//                            num1 = x0 + ( x1 - x0 ) * ( extents.Top - y0 ) / ( y1 - y0 );
//                        num2 = extents.Top;
//                    }
//                    else if ( ( ( int ) num3 & 4 ) != 0 )
//                    {
//                        if ( !double.IsInfinity( y0 ) )
//                            num1 = x0 + ( x1 - x0 ) * ( extents.Bottom - y0 ) / ( y1 - y0 );
//                        num2 = extents.Bottom;
//                    }
//                    else if ( ( ( int ) num3 & 2 ) != 0 )
//                    {
//                        if ( !double.IsInfinity( x0 ) )
//                            num2 = y0 + ( y1 - y0 ) * ( extents.Right - x0 ) / ( x1 - x0 );
//                        num1 = extents.Right;
//                    }
//                    else if ( ( ( int ) num3 & 1 ) != 0 )
//                    {
//                        if ( !double.IsInfinity( x0 ) )
//                            num2 = y0 + ( y1 - y0 ) * ( extents.Left - x0 ) / ( x1 - x0 );
//                        num1 = extents.Left;
//                    }
//                    else
//                    {
//                        num1 = double.NaN;
//                        num2 = double.NaN;
//                    }
//                    if ( ( int ) num3 == ( int ) outCode1 )
//                    {
//                        x0 = num1;
//                        y0 = num2;
//                        outCode1 = WriteableBitmapExtensions.ComputeOutCode( extents, x0, y0 );
//                    }
//                    else
//                    {
//                        x1 = num1;
//                        y1 = num2;
//                        outCode2 = WriteableBitmapExtensions.ComputeOutCode( extents, x1, y1 );
//                    }
//                }
//                else
//                    goto label_26;
//            }
//            flag = true;
//        label_26:
//            return flag;
//        }

//        public static int AlphaBlend( int sa, int sr, int sg, int sb, int destPixel )
//        {
//            int num1 = destPixel >> 24 & (int) byte.MaxValue;
//            int num2 = destPixel >> 16 & (int) byte.MaxValue;
//            int num3 = destPixel >> 8 & (int) byte.MaxValue;
//            int num4 = destPixel & (int) byte.MaxValue;
//            destPixel = sa + ( num1 * ( ( int ) byte.MaxValue - sa ) * 32897 >> 23 ) << 24 | sr + ( num2 * ( ( int ) byte.MaxValue - sa ) * 32897 >> 23 ) << 16 | sg + ( num3 * ( ( int ) byte.MaxValue - sa ) * 32897 >> 23 ) << 8 | sb + ( num4 * ( ( int ) byte.MaxValue - sa ) * 32897 >> 23 );
//            return destPixel;
//        }

//        public static unsafe void DrawWuLine( BitmapContext context, int pixelWidth, int pixelHeight, short X0, short Y0, short X1, short Y1, int sa, int sr, int sg, int sb )
//        {
//            int* pixels = context.Pixels;
//            if ( ( int ) Y0 > ( int ) Y1 )
//            {
//                short num1 = Y0;
//                Y0 = Y1;
//                Y1 = num1;
//                short num2 = X0;
//                X0 = X1;
//                X1 = num2;
//            }
//            pixels[ ( int ) Y0 * pixelWidth + ( int ) X0 ] = WriteableBitmapExtensions.AlphaBlend( sa, sr, sg, sb, pixels[ ( int ) Y0 * pixelWidth + ( int ) X0 ] );
//            short num3 = (short) ((int) X1 - (int) X0);
//            short num4;
//            if ( num3 >= ( short ) 0 )
//            {
//                num4 = ( short ) 1;
//            }
//            else
//            {
//                num4 = ( short ) -1;
//                num3 = -num3;
//            }
//            short num5 = (short) ((int) Y1 - (int) Y0);
//            if ( num5 == ( short ) 0 )
//            {
//                while ( num3-- != ( short ) 0 )
//                {
//                    X0 += num4;
//                    pixels[ ( int ) Y0 * pixelWidth + ( int ) X0 ] = WriteableBitmapExtensions.AlphaBlend( sa, sr, sg, sb, pixels[ ( int ) Y0 * pixelWidth + ( int ) X0 ] );
//                }
//            }
//            else if ( num3 == ( short ) 0 )
//            {
//                do
//                {
//                    ++Y0;
//                    pixels[ ( int ) Y0 * pixelWidth + ( int ) X0 ] = WriteableBitmapExtensions.AlphaBlend( sa, sr, sg, sb, pixels[ ( int ) Y0 * pixelWidth + ( int ) X0 ] );
//                }
//                while ( --num5 != ( short ) 0 );
//            }
//            else if ( ( int ) num3 == ( int ) num5 )
//            {
//                do
//                {
//                    X0 += num4;
//                    ++Y0;
//                    pixels[ ( int ) Y0 * pixelWidth + ( int ) X0 ] = WriteableBitmapExtensions.AlphaBlend( sa, sr, sg, sb, pixels[ ( int ) Y0 * pixelWidth + ( int ) X0 ] );
//                }
//                while ( --num5 != ( short ) 0 );
//            }
//            else
//            {
//                ushort num1 = 0;
//                if ( ( int ) num5 > ( int ) num3 )
//                {
//                    ushort num2 = (ushort) (((ulong) num3 << 16) / (ulong) num5);
//                    while ( --num5 != ( short ) 0 )
//                    {
//                        ushort num6 = num1;
//                        num1 += num2;
//                        if ( ( int ) num1 <= ( int ) num6 )
//                            X0 += num4;
//                        ++Y0;
//                        ushort num7 = (ushort) ((uint) num1 >> 8);
//                        int num8 = (int) num7 ^ (int) byte.MaxValue;
//                        pixels[ ( int ) Y0 * pixelWidth + ( int ) X0 ] = WriteableBitmapExtensions.AlphaBlend( sa, sr * num8 >> 8, sg * num8 >> 8, sb * num8 >> 8, pixels[ ( int ) Y0 * pixelWidth + ( int ) X0 ] );
//                        int num9 = (int) num7;
//                        pixels[ ( int ) Y0 * pixelWidth + ( int ) X0 + ( int ) num4 ] = WriteableBitmapExtensions.AlphaBlend( sa, sr * num9 >> 8, sg * num9 >> 8, sb * num9 >> 8, pixels[ ( int ) Y0 * pixelWidth + ( int ) X0 + ( int ) num4 ] );
//                    }
//                    pixels[ ( int ) Y1 * pixelWidth + ( int ) X1 ] = WriteableBitmapExtensions.AlphaBlend( sa, sr, sg, sb, pixels[ ( int ) Y1 * pixelWidth + ( int ) X1 ] );
//                }
//                else
//                {
//                    ushort num2 = (ushort) (((ulong) num5 << 16) / (ulong) num3);
//                    while ( --num3 != ( short ) 0 )
//                    {
//                        ushort num6 = num1;
//                        num1 += num2;
//                        if ( ( int ) num1 <= ( int ) num6 )
//                            ++Y0;
//                        X0 += num4;
//                        ushort num7 = (ushort) ((uint) num1 >> 8);
//                        int num8 = (int) num7 ^ (int) byte.MaxValue;
//                        pixels[ ( int ) Y0 * pixelWidth + ( int ) X0 ] = WriteableBitmapExtensions.AlphaBlend( sa, sr * num8 >> 8, sg * num8 >> 8, sb * num8 >> 8, pixels[ ( int ) Y0 * pixelWidth + ( int ) X0 ] );
//                        int num9 = (int) num7;
//                        pixels[ ( ( int ) Y0 + 1 ) * pixelWidth + ( int ) X0 ] = WriteableBitmapExtensions.AlphaBlend( sa, sr * num9 >> 8, sg * num9 >> 8, sb * num9 >> 8, pixels[ ( ( int ) Y0 + 1 ) * pixelWidth + ( int ) X0 ] );
//                    }
//                    pixels[ ( int ) Y1 * pixelWidth + ( int ) X1 ] = WriteableBitmapExtensions.AlphaBlend( sa, sr, sg, sb, pixels[ ( int ) Y1 * pixelWidth + ( int ) X1 ] );
//                }
//            }
//        }

//        internal static void DrawLineBresenham( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Color color )
//        {
//            int num = (int) color.A + 1;
//            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
//            bmp.DrawLineBresenham( x1, y1, x2, y2, color1 );
//        }

//        internal static void DrawLineBresenham( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int color )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                int pixelWidth = bmp.PixelWidth;
//                int pixelHeight = bmp.PixelHeight;
//                WriteableBitmapExtensions.DrawLineBresenham( bitmapContext, pixelWidth, pixelHeight, x1, y1, x2, y2, color );
//            }
//        }

//        internal static unsafe void DrawLineBresenham( BitmapContext context, int w, int h, int x1, int y1, int x2, int y2, int color )
//        {
//            if ( y1 < 0 && y2 < 0 || y1 > h && y2 > h )
//                return;
//            if ( x1 == x2 && y1 == y2 )
//            {
//                WriteableBitmapExtensions.DrawPixel( context, w, h, x1, y1, color );
//            }
//            else
//            {
//                int* pixels = context.Pixels;
//                int num1 = x2 - x1;
//                int num2 = y2 - y1;
//                int num3 = 0;
//                if ( num1 < 0 )
//                {
//                    num1 = -num1;
//                    num3 = -1;
//                }
//                else if ( num1 > 0 )
//                    num3 = 1;
//                int num4 = 0;
//                if ( num2 < 0 )
//                {
//                    num2 = -num2;
//                    num4 = -1;
//                }
//                else if ( num2 > 0 )
//                    num4 = 1;
//                int num5;
//                int num6;
//                int num7;
//                int num8;
//                int num9;
//                int num10;
//                if ( num1 > num2 )
//                {
//                    num5 = num3;
//                    num6 = 0;
//                    num7 = num3;
//                    num8 = num4;
//                    num9 = num2;
//                    num10 = num1;
//                }
//                else
//                {
//                    num5 = 0;
//                    num6 = num4;
//                    num7 = num3;
//                    num8 = num4;
//                    num9 = num1;
//                    num10 = num2;
//                }
//                int num11 = x1;
//                int num12 = y1;
//                int num13 = num10 >> 1;
//                if ( num12 < h && num12 >= 0 && ( num11 < w && num11 >= 0 ) )
//                    pixels[ num12 * w + num11 ] = color;
//                for ( int index = 0 ; index < num10 ; ++index )
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
//                    if ( num12 < h && num12 >= 0 && ( num11 < w && num11 >= 0 ) )
//                        pixels[ num12 * w + num11 ] = color;
//                }
//            }
//        }

//        internal static unsafe void DrawPixel( BitmapContext context, int w, int h, int x1, int y1, int color )
//        {
//            if ( y1 >= h || y1 < 0 || ( x1 >= w || x1 < 0 ) )
//                return;
//            context.Pixels[ y1 * w + x1 ] = color;
//        }

//        internal static void DrawLineDDA( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Color color )
//        {
//            int num = (int) color.A + 1;
//            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
//            bmp.DrawLineDDA( x1, y1, x2, y2, color1 );
//        }

//        internal static unsafe void DrawLineDDA( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int color )
//        {
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                int* pixels = bitmapContext.Pixels;
//                int num1 = x2 - x1;
//                int num2 = y2 - y1;
//                int num3 = num2 >= 0 ? num2 : -num2;
//                int num4 = num1 >= 0 ? num1 : -num1;
//                if ( num4 > num3 )
//                    num3 = num4;
//                if ( num3 == 0 )
//                    return;
//                float num5 = (float) num1 / (float) num3;
//                float num6 = (float) num2 / (float) num3;
//                float num7 = (float) x1;
//                float num8 = (float) y1;
//                for ( int index = 0 ; index < num3 ; ++index )
//                {
//                    if ( ( double ) num8 < ( double ) pixelHeight && ( double ) num8 >= 0.0 && ( ( double ) num7 < ( double ) pixelWidth && ( double ) num7 >= 0.0 ) )
//                        pixels[ ( int ) num8 * pixelWidth + ( int ) num7 ] = color;
//                    num7 += num5;
//                    num8 += num6;
//                }
//            }
//        }

//        internal static void DrawLine( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Color color )
//        {
//            int num = (int) color.A + 1;
//            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
//            bmp.DrawLine( x1, y1, x2, y2, color1 );
//        }

//        internal static void DrawLine( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int color )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//                WriteableBitmapExtensions.DrawLine( bitmapContext, bmp.PixelWidth, bmp.PixelHeight, x1, y1, x2, y2, color );
//        }

//        internal static unsafe void DrawLine( BitmapContext context, int pixelWidth, int pixelHeight, int x1, int y1, int x2, int y2, int color )
//        {
//            int* pixels = context.Pixels;
//            int num1 = x2 - x1;
//            int num2 = y2 - y1;
//            int num3 = num2 < 0 ? -num2 : num2;
//            if ( ( num1 < 0 ? -num1 : num1 ) > num3 )
//            {
//                if ( num1 < 0 )
//                {
//                    int num4 = x1;
//                    x1 = x2;
//                    x2 = num4;
//                    int num5 = y1;
//                    y1 = y2;
//                    y2 = num5;
//                }
//                int num6 = (num2 << 8) / num1;
//                int num7 = y1 << 8;
//                int num8 = y2 << 8;
//                int num9 = pixelHeight << 8;
//                if ( y1 < y2 )
//                {
//                    if ( y1 >= pixelHeight || y2 < 0 )
//                        return;
//                    if ( num7 < 0 )
//                    {
//                        if ( num6 == 0 )
//                            return;
//                        int num4 = num7;
//                        num7 = num6 - 1 + ( num7 + 1 ) % num6;
//                        x1 += ( num7 - num4 ) / num6;
//                    }
//                    if ( num8 >= num9 && num6 != 0 )
//                    {
//                        int num4 = num9 - 1 - (num9 - 1 - num7) % num6;
//                        x2 = x1 + ( num4 - num7 ) / num6;
//                    }
//                }
//                else
//                {
//                    if ( y2 >= pixelHeight || y1 < 0 )
//                        return;
//                    if ( num7 >= num9 )
//                    {
//                        if ( num6 == 0 )
//                            return;
//                        int num4 = num7;
//                        num7 = num9 - 1 + ( num6 - ( num9 - 1 - num4 ) % num6 );
//                        x1 += ( num7 - num4 ) / num6;
//                    }
//                    if ( num8 < 0 && num6 != 0 )
//                    {
//                        int num4 = num7 % num6;
//                        x2 = x1 + ( num4 - num7 ) / num6;
//                    }
//                }
//                if ( x1 < 0 )
//                {
//                    num7 -= num6 * x1;
//                    x1 = 0;
//                }
//                if ( x2 >= pixelWidth )
//                    x2 = pixelWidth - 1;
//                int num10 = num7;
//                int num11 = num10 >> 8;
//                int num12 = num11;
//                int index1 = x1 + num11 * pixelWidth;
//                int num13 = num6 < 0 ? 1 - pixelWidth : 1 + pixelWidth;
//                for ( int index2 = x1 ; index2 <= x2 ; ++index2 )
//                {
//                    pixels[ index1 ] = color;
//                    num10 += num6;
//                    int num4 = num10 >> 8;
//                    if ( num4 != num12 )
//                    {
//                        num12 = num4;
//                        index1 += num13;
//                    }
//                    else
//                        ++index1;
//                }
//            }
//            else
//            {
//                if ( num3 == 0 )
//                    return;
//                if ( num2 < 0 )
//                {
//                    int num4 = x1;
//                    x1 = x2;
//                    x2 = num4;
//                    int num5 = y1;
//                    y1 = y2;
//                    y2 = num5;
//                }
//                int num6 = x1 << 8;
//                int num7 = x2 << 8;
//                int num8 = pixelWidth << 8;
//                int num9 = (num1 << 8) / num2;
//                if ( x1 < x2 )
//                {
//                    if ( x1 >= pixelWidth || x2 < 0 )
//                        return;
//                    if ( num6 < 0 )
//                    {
//                        if ( num9 == 0 )
//                            return;
//                        int num4 = num6;
//                        num6 = num9 - 1 + ( num6 + 1 ) % num9;
//                        y1 += ( num6 - num4 ) / num9;
//                    }
//                    if ( num7 >= num8 && num9 != 0 )
//                    {
//                        int num4 = num8 - 1 - (num8 - 1 - num6) % num9;
//                        y2 = y1 + ( num4 - num6 ) / num9;
//                    }
//                }
//                else
//                {
//                    if ( x2 >= pixelWidth || x1 < 0 )
//                        return;
//                    if ( num6 >= num8 )
//                    {
//                        if ( num9 == 0 )
//                            return;
//                        int num4 = num6;
//                        num6 = num8 - 1 + ( num9 - ( num8 - 1 - num4 ) % num9 );
//                        y1 += ( num6 - num4 ) / num9;
//                    }
//                    if ( num7 < 0 && num9 != 0 )
//                    {
//                        int num4 = num6 % num9;
//                        y2 = y1 + ( num4 - num6 ) / num9;
//                    }
//                }
//                if ( y1 < 0 )
//                {
//                    num6 -= num9 * y1;
//                    y1 = 0;
//                }
//                if ( y2 >= pixelHeight )
//                    y2 = pixelHeight - 1;
//                int num10 = num6 + (y1 * pixelWidth << 8);
//                int num11 = (pixelWidth << 8) + num9;
//                for ( int index = y1 ; index <= y2 ; ++index )
//                {
//                    pixels[ num10 >> 8 ] = color;
//                    num10 += num11;
//                }
//            }
//        }

//        internal static void DrawLineAa( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Color color )
//        {
//            int num = (int) color.A + 1;
//            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
//            bmp.DrawLineAa( x1, y1, x2, y2, color1 );
//        }

//        internal static void DrawLineAa( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int color )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//                WriteableBitmapExtensions.DrawLineAa( bitmapContext, bmp.PixelWidth, bmp.PixelHeight, x1, y1, x2, y2, color, false );
//        }

//        internal static unsafe void DrawLineAa( BitmapContext context, int pixelWidth, int pixelHeight, int x1, int y1, int x2, int y2, int color, bool skipFirstPixel = false )
//        {
//            if ( !WriteableBitmapExtensions.CohenSutherlandLineClip( new Rect( 0.0, 0.0, ( double ) pixelWidth, ( double ) pixelHeight ), ref x1, ref y1, ref x2, ref y2 ) )
//                return;
//            int num1 = pixelWidth - 1;
//            int num2 = pixelHeight - 1;
//            if ( x1 < 0 )
//                x1 = 0;
//            else if ( x1 > num1 )
//                x1 = num1;
//            if ( y1 < 0 )
//                y1 = 0;
//            else if ( y1 > num2 )
//                y1 = num2;
//            if ( x2 < 0 )
//                x2 = 0;
//            if ( x2 > num1 )
//                x2 = num1;
//            if ( y2 < 0 )
//                y2 = 0;
//            if ( y2 > num2 )
//                y2 = num2;
//            int num3 = pixelWidth * pixelHeight;
//            int index1 = y1 * pixelWidth + x1;
//            int num4 = x2 - x1;
//            int num5 = y2 - y1;
//            int num6 = color >> 24 & (int) byte.MaxValue;
//            uint srb = (uint) (color & 16711935);
//            uint sg = (uint) (color >> 8 & (int) byte.MaxValue);
//            int num7 = num4;
//            int num8 = num5;
//            if ( num4 < 0 )
//                num7 = -num4;
//            if ( num5 < 0 )
//                num8 = -num5;
//            int num9;
//            int num10;
//            int num11;
//            int num12;
//            int num13;
//            int num14;
//            int num15;
//            if ( num7 > num8 )
//            {
//                num9 = num7;
//                num10 = num8;
//                num11 = x2;
//                num12 = y2;
//                num13 = 1;
//                num15 = num14 = pixelWidth;
//                if ( num4 < 0 )
//                    num13 = -num13;
//                if ( num5 < 0 )
//                    num15 = -num15;
//            }
//            else
//            {
//                num9 = num8;
//                num10 = num7;
//                num11 = y2;
//                num12 = x2;
//                num13 = num14 = pixelWidth;
//                num15 = 1;
//                if ( num5 < 0 )
//                    num13 = -num13;
//                if ( num4 < 0 )
//                    num15 = -num15;
//            }
//            int num16 = num11 + num9;
//            int num17 = (num10 << 1) - num9;
//            int num18 = num10 << 1;
//            int num19 = num10 - num9 << 1;
//            double num20 = 1.0 / (4.0 * Math.Sqrt((double) (num9 * num9 + num10 * num10)));
//            double num21 = 0.75 - 2.0 * ((double) num9 * num20);
//            int num22 = (int) (num20 * 1024.0);
//            int num23 = (int) (num21 * 1024.0 * (double) num6);
//            int num24 = (int) (768.0 * (double) num6);
//            int num25 = num22 * num6;
//            int num26 = num9 * num25;
//            int num27 = num17 * num25;
//            int num28 = 0;
//            int num29 = num18 * num25;
//            int num30 = num19 * num25;
//            int* pixels = context.Pixels;
//            bool flag = true;
//            do
//            {
//                if ( !flag || !skipFirstPixel )
//                {
//                    WriteableBitmapExtensions.AlphaBlendNormalOnPremultiplied( pixels, index1, num24 - num28 >> 10, srb, sg );
//                    int index2 = index1 + num15;
//                    if ( index2 < num3 && ( flag && num14 == num15 || index2 % num14 > 0 ) )
//                        WriteableBitmapExtensions.AlphaBlendNormalOnPremultiplied( pixels, index2, num23 + num28 >> 10, srb, sg );
//                    int index3 = index1 - num15;
//                    if ( index3 >= 0 && index3 < num3 && ( flag && num14 == num15 || index1 % num14 > 0 ) )
//                        WriteableBitmapExtensions.AlphaBlendNormalOnPremultiplied( pixels, index3, num23 - num28 >> 10, srb, sg );
//                }
//                if ( num17 < 0 )
//                {
//                    num28 = num27 + num26;
//                    num17 += num18;
//                    num27 += num29;
//                }
//                else
//                {
//                    num28 = num27 - num26;
//                    num17 += num19;
//                    num27 += num30;
//                    ++num12;
//                    index1 += num15;
//                }
//                ++num11;
//                index1 += num13;
//                flag = false;
//            }
//            while ( num11 <= num16 );
//        }

//        private static unsafe void AlphaBlendNormalOnPremultiplied( int* pixels, int index, int sa, uint srb, uint sg )
//        {
//            uint num1 = (uint) pixels[index];
//            uint num2 = num1 >> 24;
//            uint num3 = num1 >> 8 & (uint) byte.MaxValue;
//            uint num4 = num1 & 16711935U;
//            pixels[ index ] = ( int ) ( ( long ) sa + ( ( long ) num2 * ( long ) ( ( int ) byte.MaxValue - sa ) * 32897L >> 23 ) << 24 | ( long ) ( sg - num3 ) * ( long ) sa + ( long ) ( num3 << 8 ) & 4294967040L | ( ( long ) ( srb - num4 ) * ( long ) sa >> 8 ) + ( long ) num4 & 16711935L );
//        }

//        internal static void DrawPolyline( this WriteableBitmap bmp, int[ ] points, Color color )
//        {
//            int num = (int) color.A + 1;
//            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
//            bmp.DrawPolyline( points, color1 );
//        }

//        internal static void DrawPolyline( this WriteableBitmap bmp, int[ ] points, int color )
//        {
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                int x1 = points[0];
//                int y1 = points[1];
//                if ( x1 < 0 )
//                    x1 = 0;
//                if ( y1 < 0 )
//                    y1 = 0;
//                if ( x1 > pixelWidth )
//                    x1 = pixelWidth;
//                if ( y1 > pixelHeight )
//                    y1 = pixelHeight;
//                int index = 2;
//                while ( index < points.Length )
//                {
//                    int x2 = points[index];
//                    int y2 = points[index + 1];
//                    if ( x2 < 0 )
//                        x2 = 0;
//                    if ( y2 < 0 )
//                        y2 = 0;
//                    if ( x2 > pixelWidth )
//                        x2 = pixelWidth;
//                    if ( y2 > pixelHeight )
//                        y2 = pixelHeight;
//                    WriteableBitmapExtensions.DrawLine( bitmapContext, pixelWidth, pixelHeight, x1, y1, x2, y2, color );
//                    x1 = x2;
//                    y1 = y2;
//                    index += 2;
//                }
//            }
//        }

//        internal static void DrawTriangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int x3, int y3, Color color )
//        {
//            int num = (int) color.A + 1;
//            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
//            bmp.DrawTriangle( x1, y1, x2, y2, x3, y3, color1 );
//        }

//        internal static void DrawTriangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int x3, int y3, int color )
//        {
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                WriteableBitmapExtensions.DrawLine( bitmapContext, pixelWidth, pixelHeight, x1, y1, x2, y2, color );
//                WriteableBitmapExtensions.DrawLine( bitmapContext, pixelWidth, pixelHeight, x2, y2, x3, y3, color );
//                WriteableBitmapExtensions.DrawLine( bitmapContext, pixelWidth, pixelHeight, x3, y3, x1, y1, color );
//            }
//        }

//        internal static void DrawQuad( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, Color color )
//        {
//            int num = (int) color.A + 1;
//            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
//            bmp.DrawQuad( x1, y1, x2, y2, x3, y3, x4, y4, color1 );
//        }

//        internal static void DrawQuad( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, int color )
//        {
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                WriteableBitmapExtensions.DrawLine( bitmapContext, pixelWidth, pixelHeight, x1, y1, x2, y2, color );
//                WriteableBitmapExtensions.DrawLine( bitmapContext, pixelWidth, pixelHeight, x2, y2, x3, y3, color );
//                WriteableBitmapExtensions.DrawLine( bitmapContext, pixelWidth, pixelHeight, x3, y3, x4, y4, color );
//                WriteableBitmapExtensions.DrawLine( bitmapContext, pixelWidth, pixelHeight, x4, y4, x1, y1, color );
//            }
//        }

//        internal static void DrawRectangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, Color color )
//        {
//            int num = (int) color.A + 1;
//            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
//            bmp.DrawRectangle( x1, y1, x2, y2, color1 );
//        }

//        internal static unsafe void DrawRectangle( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int color )
//        {
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                int* pixels = bitmapContext.Pixels;
//                if ( x1 < 0 && x2 < 0 || y1 < 0 && y2 < 0 || ( x1 >= pixelWidth && x2 >= pixelWidth || y1 >= pixelHeight && y2 >= pixelHeight ) )
//                    return;
//                if ( x1 < 0 )
//                    x1 = 0;
//                if ( y1 < 0 )
//                    y1 = 0;
//                if ( x2 < 0 )
//                    x2 = 0;
//                if ( y2 < 0 )
//                    y2 = 0;
//                if ( x1 > pixelWidth )
//                    x1 = pixelWidth;
//                if ( y1 > pixelHeight )
//                    y1 = pixelHeight;
//                if ( x2 > pixelWidth )
//                    x2 = pixelWidth;
//                if ( y2 > pixelHeight )
//                    y2 = pixelHeight;
//                int num1 = y1 * pixelWidth;
//                int index1 = y2 * pixelWidth - pixelWidth + x1;
//                int num2 = num1 + x2;
//                int num3 = num1 + x1;
//                for ( int index2 = num3 ; index2 < num2 ; ++index2 )
//                {
//                    pixels[ index2 ] = color;
//                    pixels[ index1 ] = color;
//                    ++index1;
//                }
//                int index3 = num3 + pixelWidth;
//                int num4 = index1 - pixelWidth;
//                int index4 = num1 + x2 - 1 + pixelWidth;
//                while ( index4 < num4 )
//                {
//                    pixels[ index4 ] = color;
//                    pixels[ index3 ] = color;
//                    index3 += pixelWidth;
//                    index4 += pixelWidth;
//                }
//            }
//        }

//        internal static void DrawEllipse( this WriteableBitmap bmp, int x1, int y1, int x2, int y2, int color, int thickness )
//        {
//            int xr = x2 - x1 >> 1;
//            int yr = y2 - y1 >> 1;
//            int xc = x1 + xr;
//            int yc = y1 + yr;
//            bmp.DrawEllipseCentered( xc, yc, xr, yr, color, thickness );
//        }

//        internal static void DrawEllipseCentered( this WriteableBitmap bmp, int xc, int yc, int xr, int yr, Color color )
//        {
//            int num = (int) color.A + 1;
//            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
//            bmp.DrawEllipseCentered( xc, yc, xr, yr, color1 );
//        }

//        internal static unsafe void DrawEllipseCentered( this WriteableBitmap bmp, int xc, int yc, int xr, int yr, int color )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                int* pixels = bitmapContext.Pixels;
//                int pixelWidth = bmp.PixelWidth;
//                int pixelHeight = bmp.PixelHeight;
//                if ( xr < 1 || yr < 1 )
//                    return;
//                int num1 = xr;
//                int num2 = 0;
//                int num3 = xr * xr << 1;
//                int num4 = yr * yr << 1;
//                int num5 = yr * yr * (1 - (xr << 1));
//                int num6 = xr * xr;
//                int num7 = 0;
//                int num8 = num4 * xr;
//                int num9 = 0;
//                while ( num8 >= num9 )
//                {
//                    int num10 = yc + num2;
//                    int num11 = yc - num2;
//                    if ( num10 < 0 )
//                        num10 = 0;
//                    if ( num10 >= pixelHeight )
//                        num10 = pixelHeight - 1;
//                    if ( num11 < 0 )
//                        num11 = 0;
//                    if ( num11 >= pixelHeight )
//                        num11 = pixelHeight - 1;
//                    int num12 = num10 * pixelWidth;
//                    int num13 = num11 * pixelWidth;
//                    int num14 = xc + num1;
//                    int num15 = xc - num1;
//                    if ( num14 < 0 )
//                        num14 = 0;
//                    if ( num14 >= pixelWidth )
//                        num14 = pixelWidth - 1;
//                    if ( num15 < 0 )
//                        num15 = 0;
//                    if ( num15 >= pixelWidth )
//                        num15 = pixelWidth - 1;
//                    pixels[ num14 + num12 ] = color;
//                    pixels[ num15 + num12 ] = color;
//                    pixels[ num15 + num13 ] = color;
//                    pixels[ num14 + num13 ] = color;
//                    ++num2;
//                    num9 += num3;
//                    num7 += num6;
//                    num6 += num3;
//                    if ( num5 + ( num7 << 1 ) > 0 )
//                    {
//                        --num1;
//                        num8 -= num4;
//                        num7 += num5;
//                        num5 += num4;
//                    }
//                }
//                int num16 = 0;
//                int num17 = yr;
//                int num18 = yc + num17;
//                int num19 = yc - num17;
//                if ( num18 < 0 )
//                    num18 = 0;
//                if ( num18 >= pixelHeight )
//                    num18 = pixelHeight - 1;
//                if ( num19 < 0 )
//                    num19 = 0;
//                if ( num19 >= pixelHeight )
//                    num19 = pixelHeight - 1;
//                int num20 = num18 * pixelWidth;
//                int num21 = num19 * pixelWidth;
//                int num22 = yr * yr;
//                int num23 = xr * xr * (1 - (yr << 1));
//                int num24 = 0;
//                int num25 = 0;
//                int num26 = num3 * yr;
//                while ( num25 <= num26 )
//                {
//                    int num10 = xc + num16;
//                    int num11 = xc - num16;
//                    if ( num10 < 0 )
//                        num10 = 0;
//                    if ( num10 >= pixelWidth )
//                        num10 = pixelWidth - 1;
//                    if ( num11 < 0 )
//                        num11 = 0;
//                    if ( num11 >= pixelWidth )
//                        num11 = pixelWidth - 1;
//                    pixels[ num10 + num20 ] = color;
//                    pixels[ num11 + num20 ] = color;
//                    pixels[ num11 + num21 ] = color;
//                    pixels[ num10 + num21 ] = color;
//                    ++num16;
//                    num25 += num4;
//                    num24 += num22;
//                    num22 += num4;
//                    if ( num23 + ( num24 << 1 ) > 0 )
//                    {
//                        --num17;
//                        int num12 = yc + num17;
//                        int num13 = yc - num17;
//                        if ( num12 < 0 )
//                            num12 = 0;
//                        if ( num12 >= pixelHeight )
//                            num12 = pixelHeight - 1;
//                        if ( num13 < 0 )
//                            num13 = 0;
//                        if ( num13 >= pixelHeight )
//                            num13 = pixelHeight - 1;
//                        num20 = num12 * pixelWidth;
//                        num21 = num13 * pixelWidth;
//                        num26 -= num3;
//                        num24 += num23;
//                        num23 += num3;
//                    }
//                }
//            }
//        }

//        internal static void DrawEllipseCentered( this WriteableBitmap bmp, int xc, int yc, int xr, int yr, int color, int thickness )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//                WriteableBitmapExtensions.DrawEllipseCentered( bitmapContext, xc, yc, xr, yr, color, thickness );
//        }

//        internal static unsafe void DrawEllipseCentered( BitmapContext context, int xc, int yc, int xr, int yr, int color, int thickness )
//        {
//            int num1 = thickness >> 1;
//            int num2 = thickness - num1 - 1;
//            int* pixels = context.Pixels;
//            int pixelWidth = context.PixelWidth;
//            int pixelHeight = context.PixelHeight;
//            if ( xr < 1 || yr < 1 )
//                return;
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
//            while ( num10 >= num11 )
//            {
//                int num13 = yc + num4;
//                int num14 = yc - num4;
//                if ( num13 < 0 )
//                    num13 = 0;
//                if ( num13 >= pixelHeight )
//                    num13 = pixelHeight - 1;
//                if ( num14 < 0 )
//                    num14 = 0;
//                if ( num14 >= pixelHeight )
//                    num14 = pixelHeight - 1;
//                int num15 = num13 * pixelWidth;
//                int num16 = num14 * pixelWidth;
//                int num17 = xc + num3;
//                int num18 = xc - num3;
//                if ( num17 < 0 )
//                    num17 = 0;
//                if ( num17 >= pixelWidth )
//                    num17 = pixelWidth - 1;
//                if ( num18 < 0 )
//                    num18 = 0;
//                if ( num18 >= pixelWidth )
//                    num18 = pixelWidth - 1;
//                pixels[ num17 + num15 ] = color;
//                pixels[ num18 + num15 ] = color;
//                pixels[ num18 + num16 ] = color;
//                pixels[ num17 + num16 ] = color;
//                for ( int index = 1 ; index <= num1 ; ++index )
//                {
//                    if ( num17 + index < pixelWidth )
//                    {
//                        pixels[ num17 + index + num15 ] = color;
//                        pixels[ num17 + index + num16 ] = color;
//                    }
//                    if ( num18 - index >= 0 )
//                    {
//                        pixels[ num18 - index + num15 ] = color;
//                        pixels[ num18 - index + num16 ] = color;
//                    }
//                }
//                for ( int index = 1 ; index <= num2 ; ++index )
//                {
//                    if ( num17 - index < pixelWidth )
//                    {
//                        pixels[ num17 - index + num15 ] = color;
//                        pixels[ num17 - index + num16 ] = color;
//                    }
//                    if ( num18 + index >= 0 )
//                    {
//                        pixels[ num18 + index + num15 ] = color;
//                        pixels[ num18 + index + num16 ] = color;
//                    }
//                }
//                num12 = num17 - xc;
//                ++num4;
//                num11 += num5;
//                num9 += num8;
//                num8 += num5;
//                if ( num7 + ( num9 << 1 ) > 0 )
//                {
//                    --num3;
//                    num10 -= num6;
//                    num9 += num7;
//                    num7 += num6;
//                }
//            }
//            int num19 = 0;
//            int num20 = yr;
//            int num21 = yc + num20;
//            int num22 = yc - num20;
//            if ( num21 < 0 )
//                num21 = 0;
//            if ( num21 >= pixelHeight )
//                num21 = pixelHeight - 1;
//            if ( num22 < 0 )
//                num22 = 0;
//            if ( num22 >= pixelHeight )
//                num22 = pixelHeight - 1;
//            int num23 = num21 * pixelWidth;
//            int num24 = num22 * pixelWidth;
//            int num25 = yr * yr;
//            int num26 = xr * xr * (1 - (yr << 1));
//            int num27 = 0;
//            int num28 = 0;
//            int num29 = num5 * yr;
//            while ( num28 <= num29 )
//            {
//                int num13 = xc + num19;
//                int num14 = xc - num19;
//                if ( num13 < 0 )
//                    num13 = 0;
//                if ( num13 >= pixelWidth )
//                    num13 = pixelWidth - 1;
//                if ( num14 < 0 )
//                    num14 = 0;
//                if ( num14 >= pixelWidth )
//                    num14 = pixelWidth - 1;
//                pixels[ num13 + num23 ] = color;
//                pixels[ num14 + num23 ] = color;
//                pixels[ num14 + num24 ] = color;
//                pixels[ num13 + num24 ] = color;
//                for ( int index = 1 ; index <= num1 ; ++index )
//                {
//                    if ( num21 + index < pixelHeight )
//                    {
//                        pixels[ num13 + num23 + index * pixelWidth ] = color;
//                        pixels[ num14 + num23 + index * pixelWidth ] = color;
//                    }
//                    if ( num22 - index >= 0 )
//                    {
//                        pixels[ num14 + num24 - index * pixelWidth ] = color;
//                        pixels[ num13 + num24 - index * pixelWidth ] = color;
//                    }
//                }
//                for ( int index = 1 ; index <= num2 ; ++index )
//                {
//                    if ( num21 - index >= 0 )
//                    {
//                        pixels[ num13 + num23 - index * pixelWidth ] = color;
//                        pixels[ num14 + num23 - index * pixelWidth ] = color;
//                    }
//                    if ( num22 + index < pixelHeight )
//                    {
//                        pixels[ num14 + num24 + index * pixelWidth ] = color;
//                        pixels[ num13 + num24 + index * pixelWidth ] = color;
//                    }
//                }
//                ++num19;
//                num28 += num6;
//                num27 += num25;
//                num25 += num6;
//                if ( num26 + ( num27 << 1 ) > 0 )
//                {
//                    --num20;
//                    num21 = yc + num20;
//                    num22 = yc - num20;
//                    if ( num21 < 0 )
//                        num21 = 0;
//                    if ( num21 >= pixelHeight )
//                        num21 = pixelHeight - 1;
//                    if ( num22 < 0 )
//                        num22 = 0;
//                    if ( num22 >= pixelHeight )
//                        num22 = pixelHeight - 1;
//                    num23 = num21 * pixelWidth;
//                    num24 = num22 * pixelWidth;
//                    num29 -= num5;
//                    num27 += num26;
//                    num26 += num5;
//                }
//            }
//            for ( int index1 = 1 ; index1 <= num1 ; ++index1 )
//            {
//                for ( int index2 = 1 ; index2 <= num1 - index1 ; ++index2 )
//                {
//                    int num13 = index1 + xc + num12;
//                    int num14 = yc - index2 - num12;
//                    if ( num13 >= 0 && num13 < pixelWidth && ( num14 >= 0 && num14 < pixelHeight ) )
//                        pixels[ num13 + num14 * pixelWidth ] = color;
//                    int num15 = -index1 + xc - num12;
//                    int num16 = num14;
//                    if ( num15 >= 0 && num15 < pixelWidth && ( num16 >= 0 && num16 < pixelHeight ) )
//                        pixels[ num15 + num16 * pixelWidth ] = color;
//                    int num17 = num15;
//                    int num18 = yc + index2 + num12;
//                    if ( num17 >= 0 && num17 < pixelWidth && ( num18 >= 0 && num18 < pixelHeight ) )
//                        pixels[ num17 + num18 * pixelWidth ] = color;
//                    int num30 = num13;
//                    int num31 = num18;
//                    if ( num30 >= 0 && num30 < pixelWidth && ( num31 >= 0 && num31 < pixelHeight ) )
//                        pixels[ num30 + num31 * pixelWidth ] = color;
//                }
//            }
//        }

//        internal static void DrawBezier( this WriteableBitmap bmp, int x1, int y1, int cx1, int cy1, int cx2, int cy2, int x2, int y2, Color color )
//        {
//            int num = (int) color.A + 1;
//            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
//            bmp.DrawBezier( x1, y1, cx1, cy1, cx2, cy2, x2, y2, color1 );
//        }

//        internal static void DrawBezier( this WriteableBitmap bmp, int x1, int y1, int cx1, int cy1, int cx2, int cy2, int x2, int y2, int color )
//        {
//            int num1 = Math.Min(x1, Math.Min(cx1, Math.Min(cx2, x2)));
//            int num2 = Math.Min(y1, Math.Min(cy1, Math.Min(cy2, y2)));
//            int num3 = Math.Max(x1, Math.Max(cx1, Math.Max(cx2, x2)));
//            int num4 = Math.Max(y1, Math.Max(cy1, Math.Max(cy2, y2)));
//            int num5 = num3 - num1;
//            int num6 = num4 - num2;
//            if ( num5 > num6 )
//                num6 = num5;
//            if ( num6 == 0 )
//                return;
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                float num7 = 2f / (float) num6;
//                int x1_1 = x1;
//                int y1_1 = y1;
//                float num8 = num7;
//                while ( ( double ) num8 <= 1.0 )
//                {
//                    float num9 = num8 * num8;
//                    float num10 = 1f - num8;
//                    float num11 = num10 * num10;
//                    int x2_1 = (int) ((double) num10 * (double) num11 * (double) x1 + 3.0 * (double) num8 * (double) num11 * (double) cx1 + 3.0 * (double) num10 * (double) num9 * (double) cx2 + (double) num8 * (double) num9 * (double) x2);
//                    int y2_1 = (int) ((double) num10 * (double) num11 * (double) y1 + 3.0 * (double) num8 * (double) num11 * (double) cy1 + 3.0 * (double) num10 * (double) num9 * (double) cy2 + (double) num8 * (double) num9 * (double) y2);
//                    WriteableBitmapExtensions.DrawLine( bitmapContext, pixelWidth, pixelHeight, x1_1, y1_1, x2_1, y2_1, color );
//                    x1_1 = x2_1;
//                    y1_1 = y2_1;
//                    num8 += num7;
//                }
//                WriteableBitmapExtensions.DrawLine( bitmapContext, pixelWidth, pixelHeight, x1_1, y1_1, x2, y2, color );
//            }
//        }

//        internal static void DrawBeziers( this WriteableBitmap bmp, int[ ] points, Color color )
//        {
//            int num = (int) color.A + 1;
//            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
//            bmp.DrawBeziers( points, color1 );
//        }

//        internal static void DrawBeziers( this WriteableBitmap bmp, int[ ] points, int color )
//        {
//            int x1 = points[0];
//            int y1 = points[1];
//            int index = 2;
//            while ( index + 5 < points.Length )
//            {
//                int point1 = points[index + 4];
//                int point2 = points[index + 5];
//                bmp.DrawBezier( x1, y1, points[ index ], points[ index + 1 ], points[ index + 2 ], points[ index + 3 ], point1, point2, color );
//                x1 = point1;
//                y1 = point2;
//                index += 6;
//            }
//        }

//        private static void DrawCurveSegment( int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, float tension, int color, BitmapContext context, int w, int h )
//        {
//            int num1 = Math.Min(x1, Math.Min(x2, Math.Min(x3, x4)));
//            int num2 = Math.Min(y1, Math.Min(y2, Math.Min(y3, y4)));
//            int num3 = Math.Max(x1, Math.Max(x2, Math.Max(x3, x4)));
//            int num4 = Math.Max(y1, Math.Max(y2, Math.Max(y3, y4)));
//            int num5 = num3 - num1;
//            int num6 = num4 - num2;
//            if ( num5 > num6 )
//                num6 = num5;
//            if ( num6 == 0 )
//                return;
//            float num7 = 2f / (float) num6;
//            int x1_1 = x2;
//            int y1_1 = y2;
//            float num8 = tension * (float) (x3 - x1);
//            float num9 = tension * (float) (y3 - y1);
//            float num10 = tension * (float) (x4 - x2);
//            float num11 = tension * (float) (y4 - y2);
//            float num12 = num8 + num10 + (float) (2 * x2) - (float) (2 * x3);
//            float num13 = num9 + num11 + (float) (2 * y2) - (float) (2 * y3);
//            float num14 = -2f * num8 - num10 - (float) (3 * x2) + (float) (3 * x3);
//            float num15 = -2f * num9 - num11 - (float) (3 * y2) + (float) (3 * y3);
//            float num16 = num7;
//            while ( ( double ) num16 <= 1.0 )
//            {
//                float num17 = num16 * num16;
//                int x2_1 = (int) ((double) num12 * (double) num17 * (double) num16 + (double) num14 * (double) num17 + (double) num8 * (double) num16 + (double) x2);
//                int y2_1 = (int) ((double) num13 * (double) num17 * (double) num16 + (double) num15 * (double) num17 + (double) num9 * (double) num16 + (double) y2);
//                WriteableBitmapExtensions.DrawLine( context, w, h, x1_1, y1_1, x2_1, y2_1, color );
//                x1_1 = x2_1;
//                y1_1 = y2_1;
//                num16 += num7;
//            }
//            WriteableBitmapExtensions.DrawLine( context, w, h, x1_1, y1_1, x3, y3, color );
//        }

//        internal static void DrawCurve( this WriteableBitmap bmp, int[ ] points, float tension, Color color )
//        {
//            int num = (int) color.A + 1;
//            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
//            bmp.DrawCurve( points, tension, color1 );
//        }

//        internal static void DrawCurve( this WriteableBitmap bmp, int[ ] points, float tension, int color )
//        {
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                WriteableBitmapExtensions.DrawCurveSegment( points[ 0 ], points[ 1 ], points[ 0 ], points[ 1 ], points[ 2 ], points[ 3 ], points[ 4 ], points[ 5 ], tension, color, bitmapContext, pixelWidth, pixelHeight );
//                int index = 2;
//                while ( index < points.Length - 4 )
//                {
//                    WriteableBitmapExtensions.DrawCurveSegment( points[ index - 2 ], points[ index - 1 ], points[ index ], points[ index + 1 ], points[ index + 2 ], points[ index + 3 ], points[ index + 4 ], points[ index + 5 ], tension, color, bitmapContext, pixelWidth, pixelHeight );
//                    index += 2;
//                }
//                WriteableBitmapExtensions.DrawCurveSegment( points[ index - 2 ], points[ index - 1 ], points[ index ], points[ index + 1 ], points[ index + 2 ], points[ index + 3 ], points[ index + 2 ], points[ index + 3 ], tension, color, bitmapContext, pixelWidth, pixelHeight );
//            }
//        }

//        internal static void DrawCurveClosed( this WriteableBitmap bmp, int[ ] points, float tension, Color color )
//        {
//            int num = (int) color.A + 1;
//            int color1 = (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
//            bmp.DrawCurveClosed( points, tension, color1 );
//        }

//        internal static void DrawCurveClosed( this WriteableBitmap bmp, int[ ] points, float tension, int color )
//        {
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                int length = points.Length;
//                WriteableBitmapExtensions.DrawCurveSegment( points[ length - 2 ], points[ length - 1 ], points[ 0 ], points[ 1 ], points[ 2 ], points[ 3 ], points[ 4 ], points[ 5 ], tension, color, bitmapContext, pixelWidth, pixelHeight );
//                int index = 2;
//                while ( index < length - 4 )
//                {
//                    WriteableBitmapExtensions.DrawCurveSegment( points[ index - 2 ], points[ index - 1 ], points[ index ], points[ index + 1 ], points[ index + 2 ], points[ index + 3 ], points[ index + 4 ], points[ index + 5 ], tension, color, bitmapContext, pixelWidth, pixelHeight );
//                    index += 2;
//                }
//                WriteableBitmapExtensions.DrawCurveSegment( points[ index - 2 ], points[ index - 1 ], points[ index ], points[ index + 1 ], points[ index + 2 ], points[ index + 3 ], points[ 0 ], points[ 1 ], tension, color, bitmapContext, pixelWidth, pixelHeight );
//                WriteableBitmapExtensions.DrawCurveSegment( points[ index ], points[ index + 1 ], points[ index + 2 ], points[ index + 3 ], points[ 0 ], points[ 1 ], points[ 2 ], points[ 3 ], tension, color, bitmapContext, pixelWidth, pixelHeight );
//            }
//        }

//        internal static WriteableBitmap Resize( this WriteableBitmap bmp, int width, int height, WriteableBitmapExtensions.Interpolation interpolation )
//        {
//            using ( BitmapContext bitmapContext = bmp.GetBitmapContext() )
//            {
//                int[] src = WriteableBitmapExtensions.Resize(bitmapContext, bmp.PixelWidth, bmp.PixelHeight, width, height, interpolation);
//                WriteableBitmap writeableBitmap = BitmapFactory.New(width, height);
//                BitmapContext.BlockCopy( src, 0, bitmapContext, 0, 4 * src.Length );
//                return writeableBitmap;
//            }
//        }

//        internal static unsafe int[ ] Resize( BitmapContext srcContext, int widthSource, int heightSource, int width, int height, WriteableBitmapExtensions.Interpolation interpolation )
//        {
//            int* pixels = srcContext.Pixels;
//            int[] numArray = new int[width * height];
//            float num1 = (float) widthSource / (float) width;
//            float num2 = (float) heightSource / (float) height;
//            switch ( interpolation )
//            {
//                case WriteableBitmapExtensions.Interpolation.NearestNeighbor:
//                    int num3 = 0;
//                    for ( int index1 = 0 ; index1 < height ; ++index1 )
//                    {
//                        for ( int index2 = 0 ; index2 < width ; ++index2 )
//                        {
//                            float num4 = (float) index2 * num1;
//                            float num5 = (float) index1 * num2;
//                            int num6 = (int) num4;
//                            int num7 = (int) num5;
//                            numArray[ num3++ ] = pixels[ num7 * widthSource + num6 ];
//                        }
//                    }
//                    break;
//                case WriteableBitmapExtensions.Interpolation.Bilinear:
//                    int num8 = 0;
//                    for ( int index1 = 0 ; index1 < height ; ++index1 )
//                    {
//                        for ( int index2 = 0 ; index2 < width ; ++index2 )
//                        {
//                            float num4 = (float) index2 * num1;
//                            float num5 = (float) index1 * num2;
//                            int num6 = (int) num4;
//                            int num7 = (int) num5;
//                            float num9 = num4 - (float) num6;
//                            float num10 = num5 - (float) num7;
//                            float num11 = 1f - num9;
//                            float num12 = 1f - num10;
//                            int num13 = num6 + 1;
//                            if ( num13 >= widthSource )
//                                num13 = num6;
//                            int num14 = num7 + 1;
//                            if ( num14 >= heightSource )
//                                num14 = num7;
//                            int num15 = pixels[num7 * widthSource + num6];
//                            byte num16 = (byte) (num15 >> 24);
//                            byte num17 = (byte) (num15 >> 16);
//                            byte num18 = (byte) (num15 >> 8);
//                            byte num19 = (byte) num15;
//                            int num20 = pixels[num7 * widthSource + num13];
//                            byte num21 = (byte) (num20 >> 24);
//                            byte num22 = (byte) (num20 >> 16);
//                            byte num23 = (byte) (num20 >> 8);
//                            byte num24 = (byte) num20;
//                            int num25 = pixels[num14 * widthSource + num6];
//                            byte num26 = (byte) (num25 >> 24);
//                            byte num27 = (byte) (num25 >> 16);
//                            byte num28 = (byte) (num25 >> 8);
//                            byte num29 = (byte) num25;
//                            int num30 = pixels[num14 * widthSource + num13];
//                            byte num31 = (byte) (num30 >> 24);
//                            byte num32 = (byte) (num30 >> 16);
//                            byte num33 = (byte) (num30 >> 8);
//                            byte num34 = (byte) num30;
//                            float num35 = (float) ((double) num11 * (double) num16 + (double) num9 * (double) num21);
//                            float num36 = (float) ((double) num11 * (double) num26 + (double) num9 * (double) num31);
//                            byte num37 = (byte) ((double) num12 * (double) num35 + (double) num10 * (double) num36);
//                            float num38 = (float) ((double) num11 * (double) num17 * (double) num16 + (double) num9 * (double) num22 * (double) num21);
//                            float num39 = (float) ((double) num11 * (double) num27 * (double) num26 + (double) num9 * (double) num32 * (double) num31);
//                            float num40 = (float) ((double) num12 * (double) num38 + (double) num10 * (double) num39);
//                            float num41 = (float) ((double) num11 * (double) num18 * (double) num16 + (double) num9 * (double) num23 * (double) num21);
//                            float num42 = (float) ((double) num11 * (double) num28 * (double) num26 + (double) num9 * (double) num33 * (double) num31);
//                            float num43 = (float) ((double) num12 * (double) num41 + (double) num10 * (double) num42);
//                            float num44 = (float) ((double) num11 * (double) num19 * (double) num16 + (double) num9 * (double) num24 * (double) num21);
//                            float num45 = (float) ((double) num11 * (double) num29 * (double) num26 + (double) num9 * (double) num34 * (double) num31);
//                            float num46 = (float) ((double) num12 * (double) num44 + (double) num10 * (double) num45);
//                            if ( num37 > ( byte ) 0 )
//                            {
//                                num40 /= ( float ) num37;
//                                num43 /= ( float ) num37;
//                                num46 /= ( float ) num37;
//                            }
//                            byte num47 = (byte) num40;
//                            byte num48 = (byte) num43;
//                            byte num49 = (byte) num46;
//                            numArray[ num8++ ] = ( int ) num37 << 24 | ( int ) num47 << 16 | ( int ) num48 << 8 | ( int ) num49;
//                        }
//                    }
//                    break;
//            }
//            return numArray;
//        }

//        internal static unsafe WriteableBitmap Rotate( this WriteableBitmap bmp, int angle )
//        {
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            using ( BitmapContext bitmapContext1 = bmp.GetBitmapContext() )
//            {
//                int* pixels1 = bitmapContext1.Pixels;
//                int index1 = 0;
//                angle %= 360;
//                WriteableBitmap bmp1;
//                if ( angle > 0 && angle <= 90 )
//                {
//                    bmp1 = BitmapFactory.New( pixelHeight, pixelWidth );
//                    using ( BitmapContext bitmapContext2 = bmp1.GetBitmapContext() )
//                    {
//                        int* pixels2 = bitmapContext2.Pixels;
//                        for ( int index2 = 0 ; index2 < pixelWidth ; ++index2 )
//                        {
//                            for ( int index3 = pixelHeight - 1 ; index3 >= 0 ; --index3 )
//                            {
//                                int index4 = index3 * pixelWidth + index2;
//                                pixels2[ index1 ] = pixels1[ index4 ];
//                                ++index1;
//                            }
//                        }
//                    }
//                }
//                else if ( angle > 90 && angle <= 180 )
//                {
//                    bmp1 = BitmapFactory.New( pixelWidth, pixelHeight );
//                    using ( BitmapContext bitmapContext2 = bmp1.GetBitmapContext() )
//                    {
//                        int* pixels2 = bitmapContext2.Pixels;
//                        for ( int index2 = pixelHeight - 1 ; index2 >= 0 ; --index2 )
//                        {
//                            for ( int index3 = pixelWidth - 1 ; index3 >= 0 ; --index3 )
//                            {
//                                int index4 = index2 * pixelWidth + index3;
//                                pixels2[ index1 ] = pixels1[ index4 ];
//                                ++index1;
//                            }
//                        }
//                    }
//                }
//                else if ( angle > 180 && angle <= 270 )
//                {
//                    bmp1 = BitmapFactory.New( pixelHeight, pixelWidth );
//                    using ( BitmapContext bitmapContext2 = bmp1.GetBitmapContext() )
//                    {
//                        int* pixels2 = bitmapContext2.Pixels;
//                        for ( int index2 = pixelWidth - 1 ; index2 >= 0 ; --index2 )
//                        {
//                            for ( int index3 = 0 ; index3 < pixelHeight ; ++index3 )
//                            {
//                                int index4 = index3 * pixelWidth + index2;
//                                pixels2[ index1 ] = pixels1[ index4 ];
//                                ++index1;
//                            }
//                        }
//                    }
//                }
//                else
//                    bmp1 = bmp.Clone();
//                return bmp1;
//            }
//        }

//        internal static unsafe WriteableBitmap RotateFree( this WriteableBitmap bmp, double angle, bool crop = true )
//        {
//            double num1 = -1.0 * Math.PI / 180.0 * angle;
//            int pixelWidth1 = bmp.PixelWidth;
//            int pixelHeight1 = bmp.PixelHeight;
//            int pixelWidth2;
//            int pixelHeight2;
//            if ( crop )
//            {
//                pixelWidth2 = pixelWidth1;
//                pixelHeight2 = pixelHeight1;
//            }
//            else
//            {
//                double num2 = angle / (180.0 / Math.PI);
//                pixelWidth2 = ( int ) Math.Ceiling( Math.Abs( Math.Sin( num2 ) * ( double ) pixelHeight1 ) + Math.Abs( Math.Cos( num2 ) * ( double ) pixelWidth1 ) );
//                pixelHeight2 = ( int ) Math.Ceiling( Math.Abs( Math.Sin( num2 ) * ( double ) pixelWidth1 ) + Math.Abs( Math.Cos( num2 ) * ( double ) pixelHeight1 ) );
//            }
//            int num3 = pixelWidth1 / 2;
//            int num4 = pixelHeight1 / 2;
//            int num5 = pixelWidth2 / 2;
//            int num6 = pixelHeight2 / 2;
//            WriteableBitmap bmp1 = BitmapFactory.New(pixelWidth2, pixelHeight2);
//            using ( BitmapContext bitmapContext1 = bmp1.GetBitmapContext() )
//            {
//                using ( BitmapContext bitmapContext2 = bmp.GetBitmapContext() )
//                {
//                    int* pixels1 = bitmapContext1.Pixels;
//                    int* pixels2 = bitmapContext2.Pixels;
//                    int pixelWidth3 = bmp.PixelWidth;
//                    for ( int index1 = 0 ; index1 < pixelHeight2 ; ++index1 )
//                    {
//                        for ( int index2 = 0 ; index2 < pixelWidth2 ; ++index2 )
//                        {
//                            int num2 = index2 - num5;
//                            int num7 = num6 - index1;
//                            double num8 = Math.Sqrt((double) (num2 * num2 + num7 * num7));
//                            double num9;
//                            if ( num2 == 0 )
//                            {
//                                if ( num7 == 0 )
//                                {
//                                    pixels1[ index1 * pixelWidth2 + index2 ] = pixels2[ num4 * pixelWidth3 + num3 ];
//                                    continue;
//                                }
//                                num9 = num7 >= 0 ? Math.PI / 2.0 : 3.0 * Math.PI / 2.0;
//                            }
//                            else
//                                num9 = Math.Atan2( ( double ) num7, ( double ) num2 );
//                            double num10 = num9 - num1;
//                            double num11 = num8 * Math.Cos(num10);
//                            double num12 = num8 * Math.Sin(num10);
//                            double num13 = num11 + (double) num3;
//                            double num14 = (double) num4 - num12;
//                            int x1 = (int) Math.Floor(num13);
//                            int y1 = (int) Math.Floor(num14);
//                            int x2 = (int) Math.Ceiling(num13);
//                            int y2 = (int) Math.Ceiling(num14);
//                            if ( x1 >= 0 && x2 >= 0 && ( x1 < pixelWidth1 && x2 < pixelWidth1 ) && ( y1 >= 0 && y2 >= 0 && ( y1 < pixelHeight1 && y2 < pixelHeight1 ) ) )
//                            {
//                                double num15 = num13 - (double) x1;
//                                double num16 = num14 - (double) y1;
//                                Color pixel1 = bmp.GetPixel(x1, y1);
//                                Color pixel2 = bmp.GetPixel(x2, y1);
//                                Color pixel3 = bmp.GetPixel(x1, y2);
//                                Color pixel4 = bmp.GetPixel(x2, y2);
//                                double num17 = (1.0 - num15) * (double) pixel1.R + num15 * (double) pixel2.R;
//                                double num18 = (1.0 - num15) * (double) pixel1.G + num15 * (double) pixel2.G;
//                                double num19 = (1.0 - num15) * (double) pixel1.B + num15 * (double) pixel2.B;
//                                double num20 = (1.0 - num15) * (double) pixel1.A + num15 * (double) pixel2.A;
//                                double num21 = (1.0 - num15) * (double) pixel3.R + num15 * (double) pixel4.R;
//                                double num22 = (1.0 - num15) * (double) pixel3.G + num15 * (double) pixel4.G;
//                                double num23 = (1.0 - num15) * (double) pixel3.B + num15 * (double) pixel4.B;
//                                double num24 = (1.0 - num15) * (double) pixel3.A + num15 * (double) pixel4.A;
//                                int num25 = (int) Math.Round((1.0 - num16) * num17 + num16 * num21);
//                                int num26 = (int) Math.Round((1.0 - num16) * num18 + num16 * num22);
//                                int num27 = (int) Math.Round((1.0 - num16) * num19 + num16 * num23);
//                                int num28 = (int) Math.Round((1.0 - num16) * num20 + num16 * num24);
//                                if ( num25 < 0 )
//                                    num25 = 0;
//                                if ( num25 > ( int ) byte.MaxValue )
//                                    num25 = ( int ) byte.MaxValue;
//                                if ( num26 < 0 )
//                                    num26 = 0;
//                                if ( num26 > ( int ) byte.MaxValue )
//                                    num26 = ( int ) byte.MaxValue;
//                                if ( num27 < 0 )
//                                    num27 = 0;
//                                if ( num27 > ( int ) byte.MaxValue )
//                                    num27 = ( int ) byte.MaxValue;
//                                if ( num28 < 0 )
//                                    num28 = 0;
//                                if ( num28 > ( int ) byte.MaxValue )
//                                    num28 = ( int ) byte.MaxValue;
//                                int num29 = num28 + 1;
//                                pixels1[ index1 * pixelWidth2 + index2 ] = num28 << 24 | ( int ) ( byte ) ( num25 * num29 >> 8 ) << 16 | ( int ) ( byte ) ( num26 * num29 >> 8 ) << 8 | ( int ) ( byte ) ( num27 * num29 >> 8 );
//                            }
//                        }
//                    }
//                    return bmp1;
//                }
//            }
//        }

//        internal static unsafe WriteableBitmap Flip( this WriteableBitmap bmp, WriteableBitmapExtensions.FlipMode flipMode )
//        {
//            int pixelWidth = bmp.PixelWidth;
//            int pixelHeight = bmp.PixelHeight;
//            using ( BitmapContext bitmapContext1 = bmp.GetBitmapContext() )
//            {
//                int* pixels1 = bitmapContext1.Pixels;
//                int index1 = 0;
//                WriteableBitmap bmp1 = (WriteableBitmap) null;
//                switch ( flipMode )
//                {
//                    case WriteableBitmapExtensions.FlipMode.Vertical:
//                        bmp1 = BitmapFactory.New( pixelWidth, pixelHeight );
//                        using ( BitmapContext bitmapContext2 = bmp1.GetBitmapContext() )
//                        {
//                            int* pixels2 = bitmapContext2.Pixels;
//                            for ( int index2 = 0 ; index2 < pixelHeight ; ++index2 )
//                            {
//                                for ( int index3 = pixelWidth - 1 ; index3 >= 0 ; --index3 )
//                                {
//                                    int index4 = index2 * pixelWidth + index3;
//                                    pixels2[ index1 ] = pixels1[ index4 ];
//                                    ++index1;
//                                }
//                            }
//                            break;
//                        }
//                    case WriteableBitmapExtensions.FlipMode.Horizontal:
//                        bmp1 = BitmapFactory.New( pixelWidth, pixelHeight );
//                        using ( BitmapContext bitmapContext2 = bmp1.GetBitmapContext() )
//                        {
//                            int* pixels2 = bitmapContext2.Pixels;
//                            for ( int index2 = pixelHeight - 1 ; index2 >= 0 ; --index2 )
//                            {
//                                for ( int index3 = 0 ; index3 < pixelWidth ; ++index3 )
//                                {
//                                    int index4 = index2 * pixelWidth + index3;
//                                    pixels2[ index1 ] = pixels1[ index4 ];
//                                    ++index1;
//                                }
//                            }
//                            break;
//                        }
//                }
//                return bmp1;
//            }
//        }

//        internal enum BlendMode
//        {
//            Alpha,
//            Additive,
//            Subtractive,
//            Mask,
//            Multiply,
//            ColorKeying,
//            None,
//        }

//        internal enum Interpolation
//        {
//            NearestNeighbor,
//            Bilinear,
//        }

//        internal enum FlipMode
//        {
//            Vertical,
//            Horizontal,
//        }
//    }
//}
