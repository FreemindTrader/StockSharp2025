// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.bounding_rect
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using MatterHackers.Agg.VertexSource;

namespace MatterHackers.Agg
{
    internal static class bounding_rect
    {
        public static bool get_bounding_rect( PathStorage vs, int[ ] gi, int start, int num, out double x1, out double y1, out double x2, out double y2 )
        {
            double x = 0.0;
            double y = 0.0;
            bool flag = true;
            x1 = 1.0;
            y1 = 1.0;
            x2 = 0.0;
            y2 = 0.0;
            for ( int index = 0 ; index < num ; ++index )
            {
                vs.rewind( gi[ start + index ] );
                Path.FlagsAndCommand c;
                while ( !Path.is_stop( c = vs.vertex( out x, out y ) ) )
                {
                    if ( Path.is_vertex( c ) )
                    {
                        if ( flag )
                        {
                            x1 = x;
                            y1 = y;
                            x2 = x;
                            y2 = y;
                            flag = false;
                        }
                        else
                        {
                            if ( x < x1 )
                                x1 = x;
                            if ( y < y1 )
                                y1 = y;
                            if ( x > x2 )
                                x2 = x;
                            if ( y > y2 )
                                y2 = y;
                        }
                    }
                }
            }
            if ( x1 <= x2 )
                return y1 <= y2;
            return false;
        }

        public static bool get_bounding_rect( PathStorage vs, int[ ] gi, int start, int num, out RectangleDouble boundingRect )
        {
            return bounding_rect.get_bounding_rect( vs, gi, start, num, out boundingRect.Left, out boundingRect.Bottom, out boundingRect.Right, out boundingRect.Top );
        }

        public static bool bounding_rect_single( IVertexSource vs, int path_id, ref RectangleDouble rect )
        {
            double x1;
            double y1;
            double x2;
            double y2;
            bool flag = bounding_rect.bounding_rect_single(vs, path_id, out x1, out y1, out x2, out y2);
            rect.Left = x1;
            rect.Bottom = y1;
            rect.Right = x2;
            rect.Top = y2;
            return flag;
        }

        public static bool bounding_rect_single( IVertexSource vs, int path_id, out double x1, out double y1, out double x2, out double y2 )
        {
            double x = 0.0;
            double y = 0.0;
            bool flag = true;
            x1 = 1.0;
            y1 = 1.0;
            x2 = 0.0;
            y2 = 0.0;
            vs.rewind( path_id );
            Path.FlagsAndCommand c;
            while ( !Path.is_stop( c = vs.vertex( out x, out y ) ) )
            {
                if ( Path.is_vertex( c ) )
                {
                    if ( flag )
                    {
                        x1 = x;
                        y1 = y;
                        x2 = x;
                        y2 = y;
                        flag = false;
                    }
                    else
                    {
                        if ( x < x1 )
                            x1 = x;
                        if ( y < y1 )
                            y1 = y;
                        if ( x > x2 )
                            x2 = x;
                        if ( y > y2 )
                            y2 = y;
                    }
                }
            }
            if ( x1 <= x2 )
                return y1 <= y2;
            return false;
        }
    }
}
