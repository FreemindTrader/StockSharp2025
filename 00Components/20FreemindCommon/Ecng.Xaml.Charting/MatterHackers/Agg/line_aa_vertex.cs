// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.line_aa_vertex
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg
{
    internal struct line_aa_vertex
    {
        public int x;
        public int y;
        public int len;

        public line_aa_vertex( int x_, int y_ )
        {
            this.x = x_;
            this.y = y_;
            this.len = 0;
        }

        public bool Compare( line_aa_vertex val )
        {
            double num1 = (double) (val.x - this.x);
            double num2 = (double) (val.y - this.y);
            return ( this.len = agg_basics.uround( Math.Sqrt( num1 * num1 + num2 * num2 ) ) ) > 384;
        }
    }
}
