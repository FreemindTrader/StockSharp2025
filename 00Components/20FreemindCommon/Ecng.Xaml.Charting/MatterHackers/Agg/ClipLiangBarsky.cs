// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.ClipLiangBarsky
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal static class ClipLiangBarsky
    {
        public static int clipping_flags( int x, int y, RectangleInt clip_box )
        {
            return ( x > clip_box.Right ? 1 : 0 ) | ( y > clip_box.Top ? 2 : 0 ) | ( x < clip_box.Left ? 4 : 0 ) | ( y < clip_box.Bottom ? 8 : 0 );
        }

        public static int clipping_flags_x( int x, RectangleInt clip_box )
        {
            return ( x > clip_box.Right ? 1 : 0 ) | ( x < clip_box.Left ? 1 : 0 ) << 2;
        }

        public static int clipping_flags_y( int y, RectangleInt clip_box )
        {
            return ( y > clip_box.Top ? 1 : 0 ) << 1 | ( y < clip_box.Bottom ? 1 : 0 ) << 3;
        }

        public static int clip_liang_barsky( int x1, int y1, int x2, int y2, RectangleInt clip_box, int[ ] x, int[ ] y )
        {
            int num1 = 0;
            int num2 = 0;
            double num3 = 1E-30;
            double num4 = (double) (x2 - x1);
            double num5 = (double) (y2 - y1);
            int num6 = 0;
            if ( num4 == 0.0 )
                num4 = x1 > clip_box.Left ? -num3 : num3;
            if ( num5 == 0.0 )
                num5 = y1 > clip_box.Bottom ? -num3 : num3;
            double num7;
            double num8;
            if ( num4 > 0.0 )
            {
                num7 = ( double ) clip_box.Left;
                num8 = ( double ) clip_box.Right;
            }
            else
            {
                num7 = ( double ) clip_box.Right;
                num8 = ( double ) clip_box.Left;
            }
            double num9;
            double num10;
            if ( num5 > 0.0 )
            {
                num9 = ( double ) clip_box.Bottom;
                num10 = ( double ) clip_box.Top;
            }
            else
            {
                num9 = ( double ) clip_box.Top;
                num10 = ( double ) clip_box.Bottom;
            }
            double num11 = (num7 - (double) x1) / num4;
            double num12 = (num9 - (double) y1) / num5;
            double num13;
            double num14;
            if ( num11 < num12 )
            {
                num13 = num11;
                num14 = num12;
            }
            else
            {
                num13 = num12;
                num14 = num11;
            }
            if ( num13 <= 1.0 )
            {
                if ( 0.0 < num13 )
                {
                    x[ num1++ ] = ( int ) num7;
                    y[ num2++ ] = ( int ) num9;
                    ++num6;
                }
                if ( num14 <= 1.0 )
                {
                    double num15 = (num8 - (double) x1) / num4;
                    double num16 = (num10 - (double) y1) / num5;
                    double num17 = num15 < num16 ? num15 : num16;
                    int num18;
                    int num19;
                    if ( num14 > 0.0 || num17 > 0.0 )
                    {
                        if ( num14 <= num17 )
                        {
                            if ( num14 > 0.0 )
                            {
                                if ( num11 > num12 )
                                {
                                    x[ num1++ ] = ( int ) num7;
                                    y[ num2++ ] = ( int ) ( ( double ) y1 + num11 * num5 );
                                }
                                else
                                {
                                    x[ num1++ ] = ( int ) ( ( double ) x1 + num12 * num4 );
                                    y[ num2++ ] = ( int ) num9;
                                }
                                ++num6;
                            }
                            if ( num17 < 1.0 )
                            {
                                if ( num15 < num16 )
                                {
                                    int[] numArray1 = x;
                                    int index1 = num1;
                                    num18 = index1 + 1;
                                    int num20 = (int) num8;
                                    numArray1[ index1 ] = num20;
                                    int[] numArray2 = y;
                                    int index2 = num2;
                                    num19 = index2 + 1;
                                    int num21 = (int) ((double) y1 + num15 * num5);
                                    numArray2[ index2 ] = num21;
                                }
                                else
                                {
                                    int[] numArray1 = x;
                                    int index1 = num1;
                                    num18 = index1 + 1;
                                    int num20 = (int) ((double) x1 + num16 * num4);
                                    numArray1[ index1 ] = num20;
                                    int[] numArray2 = y;
                                    int index2 = num2;
                                    num19 = index2 + 1;
                                    int num21 = (int) num10;
                                    numArray2[ index2 ] = num21;
                                }
                            }
                            else
                            {
                                int[] numArray1 = x;
                                int index1 = num1;
                                num18 = index1 + 1;
                                int num20 = x2;
                                numArray1[ index1 ] = num20;
                                int[] numArray2 = y;
                                int index2 = num2;
                                num19 = index2 + 1;
                                int num21 = y2;
                                numArray2[ index2 ] = num21;
                            }
                            ++num6;
                        }
                        else
                        {
                            if ( num11 > num12 )
                            {
                                int[] numArray1 = x;
                                int index1 = num1;
                                num18 = index1 + 1;
                                int num20 = (int) num7;
                                numArray1[ index1 ] = num20;
                                int[] numArray2 = y;
                                int index2 = num2;
                                num19 = index2 + 1;
                                int num21 = (int) num10;
                                numArray2[ index2 ] = num21;
                            }
                            else
                            {
                                int[] numArray1 = x;
                                int index1 = num1;
                                num18 = index1 + 1;
                                int num20 = (int) num8;
                                numArray1[ index1 ] = num20;
                                int[] numArray2 = y;
                                int index2 = num2;
                                num19 = index2 + 1;
                                int num21 = (int) num9;
                                numArray2[ index2 ] = num21;
                            }
                            ++num6;
                        }
                    }
                }
            }
            return num6;
        }

        public static bool clip_move_point( int x1, int y1, int x2, int y2, RectangleInt clip_box, ref int x, ref int y, int flags )
        {
            if ( ( flags & 5 ) != 0 )
            {
                if ( x1 == x2 )
                    return false;
                int num = (flags & 4) != 0 ? clip_box.Left : clip_box.Right;
                y = ( int ) ( ( double ) ( num - x1 ) * ( double ) ( y2 - y1 ) / ( double ) ( x2 - x1 ) + ( double ) y1 );
                x = num;
            }
            flags = ClipLiangBarsky.clipping_flags_y( y, clip_box );
            if ( ( flags & 10 ) != 0 )
            {
                if ( y1 == y2 )
                    return false;
                int num = (flags & 4) != 0 ? clip_box.Bottom : clip_box.Top;
                x = ( int ) ( ( double ) ( num - y1 ) * ( double ) ( x2 - x1 ) / ( double ) ( y2 - y1 ) + ( double ) x1 );
                y = num;
            }
            return true;
        }

        public static int clip_line_segment( ref int x1, ref int y1, ref int x2, ref int y2, RectangleInt clip_box )
        {
            int flags1 = ClipLiangBarsky.clipping_flags(x1, y1, clip_box);
            int flags2 = ClipLiangBarsky.clipping_flags(x2, y2, clip_box);
            int num = 0;
            if ( ( flags2 | flags1 ) == 0 )
                return 0;
            if ( ( flags1 & 5 ) != 0 && ( flags1 & 5 ) == ( flags2 & 5 ) || ( flags1 & 10 ) != 0 && ( flags1 & 10 ) == ( flags2 & 10 ) )
                return 4;
            int x1_1 = x1;
            int y1_1 = y1;
            int x2_1 = x2;
            int y2_1 = y2;
            if ( flags1 != 0 )
            {
                if ( !ClipLiangBarsky.clip_move_point( x1_1, y1_1, x2_1, y2_1, clip_box, ref x1, ref y1, flags1 ) || x1 == x2 && y1 == y2 )
                    return 4;
                num |= 1;
            }
            if ( flags2 != 0 )
            {
                if ( !ClipLiangBarsky.clip_move_point( x1_1, y1_1, x2_1, y2_1, clip_box, ref x2, ref y2, flags2 ) || x1 == x2 && y1 == y2 )
                    return 4;
                num |= 2;
            }
            return num;
        }

        private enum clipping_flags_e
        {
            clipping_flags_x2_clipped = 1,
            clipping_flags_y2_clipped = 2,
            clipping_flags_x1_clipped = 4,
            clipping_flags_x_clipped = 5,
            clipping_flags_y1_clipped = 8,
            clipping_flags_y_clipped = 10, // 0x0000000A
        }
    }
}
