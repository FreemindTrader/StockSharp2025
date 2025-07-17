// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.LineAABasics
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg
{
    internal static class LineAABasics
    {
        public const int line_subpixel_shift = 8;
        public const int line_subpixel_scale = 256;
        public const int line_subpixel_mask = 255;
        public const int line_max_coord = 268435455;
        public const int line_max_length = 262144;
        public const int line_mr_subpixel_shift = 4;
        public const int line_mr_subpixel_scale = 16;
        public const int line_mr_subpixel_mask = 15;

        public static int line_mr( int x )
        {
            return x >> 4;
        }

        public static int line_hr( int x )
        {
            return x << 4;
        }

        public static int line_dbl_hr( int x )
        {
            return x << 8;
        }

        public static void bisectrix( line_parameters l1, line_parameters l2, out int x, out int y )
        {
            double num1 = (double) l2.len / (double) l1.len;
            double v1 = (double) l2.x2 - (double) (l2.x1 - l1.x1) * num1;
            double v2 = (double) l2.y2 - (double) (l2.y1 - l1.y1) * num1;
            if ( ( double ) ( l2.x2 - l2.x1 ) * ( double ) ( l2.y1 - l1.y1 ) < ( double ) ( l2.y2 - l2.y1 ) * ( double ) ( l2.x1 - l1.x1 ) + 100.0 )
            {
                v1 -= ( v1 - ( double ) l2.x1 ) * 2.0;
                v2 -= ( v2 - ( double ) l2.y1 ) * 2.0;
            }
            double num2 = v1 - (double) l2.x1;
            double num3 = v2 - (double) l2.y1;
            if ( ( int ) Math.Sqrt( num2 * num2 + num3 * num3 ) < 256 )
            {
                x = l2.x1 + l2.x1 + ( l2.y1 - l1.y1 ) + ( l2.y2 - l2.y1 ) >> 1;
                y = l2.y1 + l2.y1 - ( l2.x1 - l1.x1 ) - ( l2.x2 - l2.x1 ) >> 1;
            }
            else
            {
                x = agg_basics.iround( v1 );
                y = agg_basics.iround( v2 );
            }
        }

        public static void fix_degenerate_bisectrix_start( line_parameters lp, ref int x, ref int y )
        {
            if ( agg_basics.iround( ( ( double ) ( x - lp.x2 ) * ( double ) ( lp.y2 - lp.y1 ) - ( double ) ( y - lp.y2 ) * ( double ) ( lp.x2 - lp.x1 ) ) / ( double ) lp.len ) >= 128 )
                return;
            x = lp.x1 + ( lp.y2 - lp.y1 );
            y = lp.y1 - ( lp.x2 - lp.x1 );
        }

        public static void fix_degenerate_bisectrix_end( line_parameters lp, ref int x, ref int y )
        {
            if ( agg_basics.iround( ( ( double ) ( x - lp.x2 ) * ( double ) ( lp.y2 - lp.y1 ) - ( double ) ( y - lp.y2 ) * ( double ) ( lp.x2 - lp.x1 ) ) / ( double ) lp.len ) >= 128 )
                return;
            x = lp.x2 + ( lp.y2 - lp.y1 );
            y = lp.y2 - ( lp.x2 - lp.x1 );
        }
    }
}
