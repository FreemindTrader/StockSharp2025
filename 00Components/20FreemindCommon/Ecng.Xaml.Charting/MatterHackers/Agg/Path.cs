// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Path
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg
{
    internal static class Path
    {
        public static bool is_vertex( Path.FlagsAndCommand c )
        {
            if ( c >= Path.FlagsAndCommand.CommandMoveTo )
                return c < Path.FlagsAndCommand.CommandEndPoly;
            return false;
        }

        public static bool is_drawing( Path.FlagsAndCommand c )
        {
            if ( c >= Path.FlagsAndCommand.CommandLineTo )
                return c < Path.FlagsAndCommand.CommandEndPoly;
            return false;
        }

        public static bool is_stop( Path.FlagsAndCommand c )
        {
            return c == Path.FlagsAndCommand.CommandStop;
        }

        public static bool is_move_to( Path.FlagsAndCommand c )
        {
            return c == Path.FlagsAndCommand.CommandMoveTo;
        }

        public static bool is_line_to( Path.FlagsAndCommand c )
        {
            return c == Path.FlagsAndCommand.CommandLineTo;
        }

        public static bool is_curve( Path.FlagsAndCommand c )
        {
            if ( c != Path.FlagsAndCommand.CommandCurve3 )
                return c == Path.FlagsAndCommand.CommandCurve4;
            return true;
        }

        public static bool is_curve3( Path.FlagsAndCommand c )
        {
            return c == Path.FlagsAndCommand.CommandCurve3;
        }

        public static bool is_curve4( Path.FlagsAndCommand c )
        {
            return c == Path.FlagsAndCommand.CommandCurve4;
        }

        public static bool is_end_poly( Path.FlagsAndCommand c )
        {
            return ( c & Path.FlagsAndCommand.CommandEndPoly ) == Path.FlagsAndCommand.CommandEndPoly;
        }

        public static bool is_close( Path.FlagsAndCommand c )
        {
            return ( c & ~( Path.FlagsAndCommand.FlagCCW | Path.FlagsAndCommand.FlagCW ) ) == ( Path.FlagsAndCommand.CommandEndPoly | Path.FlagsAndCommand.FlagClose );
        }

        public static bool is_next_poly( Path.FlagsAndCommand c )
        {
            if ( !Path.is_stop( c ) && !Path.is_move_to( c ) )
                return Path.is_end_poly( c );
            return true;
        }

        public static bool is_cw( Path.FlagsAndCommand c )
        {
            return ( uint ) ( c & Path.FlagsAndCommand.FlagCW ) > 0U;
        }

        public static bool is_ccw( Path.FlagsAndCommand c )
        {
            return ( uint ) ( c & Path.FlagsAndCommand.FlagCCW ) > 0U;
        }

        public static bool is_oriented( Path.FlagsAndCommand c )
        {
            return ( uint ) ( c & ( Path.FlagsAndCommand.FlagCCW | Path.FlagsAndCommand.FlagCW ) ) > 0U;
        }

        public static bool is_closed( Path.FlagsAndCommand c )
        {
            return ( uint ) ( c & Path.FlagsAndCommand.FlagClose ) > 0U;
        }

        public static Path.FlagsAndCommand get_close_flag( Path.FlagsAndCommand c )
        {
            return c & Path.FlagsAndCommand.FlagClose;
        }

        public static Path.FlagsAndCommand clear_orientation( Path.FlagsAndCommand c )
        {
            return c & ~( Path.FlagsAndCommand.FlagCCW | Path.FlagsAndCommand.FlagCW );
        }

        public static Path.FlagsAndCommand get_orientation( Path.FlagsAndCommand c )
        {
            return c & ( Path.FlagsAndCommand.FlagCCW | Path.FlagsAndCommand.FlagCW );
        }

        public static void shorten_path( VertexSequence vs, double s )
        {
            Path.shorten_path( vs, s, 0 );
        }

        public static void shorten_path( VertexSequence vs, double s, int closed )
        {
            if ( s <= 0.0 || vs.size() <= 1 )
                return;
            for ( int index = vs.size() - 2 ; index != 0 ; --index )
            {
                double dist = vs[index].dist;
                if ( dist <= s )
                {
                    vs.RemoveLast();
                    s -= dist;
                }
                else
                    break;
            }
            if ( vs.size() < 2 )
            {
                vs.remove_all();
            }
            else
            {
                int index = vs.size() - 1;
                VertexDistance v1 = vs[index - 1];
                VertexDistance v2 = vs[index];
                double num1 = (v1.dist - s) / v1.dist;
                double num2 = v1.x + (v2.x - v1.x) * num1;
                double num3 = v1.y + (v2.y - v1.y) * num1;
                v2.x = num2;
                v2.y = num3;
                if ( !v1.IsEqual( v2 ) )
                    vs.RemoveLast();
                vs.close( ( uint ) closed > 0U );
            }
        }

        [Flags]
        public enum FlagsAndCommand
        {
            CommandStop = 0,
            CommandMoveTo = 1,
            CommandLineTo = 2,
            CommandCurve3 = CommandLineTo | CommandMoveTo, // 0x00000003
            CommandCurve4 = 4,
            CommandEndPoly = 15, // 0x0000000F
            CommandsMask = CommandEndPoly, // 0x0000000F
            FlagNone = 0,
            FlagCCW = 16, // 0x00000010
            FlagCW = 32, // 0x00000020
            FlagClose = 64, // 0x00000040
            FlagsMask = 240, // 0x000000F0
        }
    }
}
