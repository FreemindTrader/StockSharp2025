// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.line_parameters
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg
{
    internal struct line_parameters
    {
        public static byte[] s_orthogonal_quadrant = new byte[8]{ (byte) 0, (byte) 0, (byte) 1, (byte) 1, (byte) 3, (byte) 3, (byte) 2, (byte) 2 };
        public static byte[] s_diagonal_quadrant = new byte[8]{ (byte) 0, (byte) 1, (byte) 2, (byte) 1, (byte) 0, (byte) 3, (byte) 2, (byte) 3 };
        public int x1;
        public int y1;
        public int x2;
        public int y2;
        public int dx;
        public int dy;
        public int sx;
        public int sy;
        public bool vertical;
        public int inc;
        public int len;
        public int octant;

        public line_parameters( int x1_, int y1_, int x2_, int y2_, int len_ )
        {
            this.x1 = x1_;
            this.y1 = y1_;
            this.x2 = x2_;
            this.y2 = y2_;
            this.dx = Math.Abs( x2_ - x1_ );
            this.dy = Math.Abs( y2_ - y1_ );
            this.sx = x2_ > x1_ ? 1 : -1;
            this.sy = y2_ > y1_ ? 1 : -1;
            this.vertical = this.dy >= this.dx;
            this.inc = this.vertical ? this.sy : this.sx;
            this.len = len_;
            this.octant = this.sy & 4 | this.sx & 2 | ( this.vertical ? 1 : 0 );
        }

        public uint orthogonal_quadrant()
        {
            return ( uint ) line_parameters.s_orthogonal_quadrant[ this.octant ];
        }

        public uint diagonal_quadrant()
        {
            return ( uint ) line_parameters.s_diagonal_quadrant[ this.octant ];
        }

        public bool same_orthogonal_quadrant( line_parameters lp )
        {
            return ( int ) line_parameters.s_orthogonal_quadrant[ this.octant ] == ( int ) line_parameters.s_orthogonal_quadrant[ lp.octant ];
        }

        public bool same_diagonal_quadrant( line_parameters lp )
        {
            return ( int ) line_parameters.s_diagonal_quadrant[ this.octant ] == ( int ) line_parameters.s_diagonal_quadrant[ lp.octant ];
        }

        public void divide( out line_parameters lp1, out line_parameters lp2 )
        {
            int num1 = this.x1 + this.x2 >> 1;
            int num2 = this.y1 + this.y2 >> 1;
            int num3 = this.len >> 1;
            lp1 = this;
            lp2 = this;
            lp1.x2 = num1;
            lp1.y2 = num2;
            lp1.len = num3;
            lp1.dx = Math.Abs( lp1.x2 - lp1.x1 );
            lp1.dy = Math.Abs( lp1.y2 - lp1.y1 );
            lp2.x1 = num1;
            lp2.y1 = num2;
            lp2.len = num3;
            lp2.dx = Math.Abs( lp2.x2 - lp2.x1 );
            lp2.dy = Math.Abs( lp2.y2 - lp2.y1 );
        }
    }
}
