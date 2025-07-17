// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VertexDistance
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal struct VertexDistance
    {
        public double x;
        public double y;
        public double dist;

        public VertexDistance( double x_, double y_ )
        {
            this.x = x_;
            this.y = y_;
            this.dist = 0.0;
        }

        public bool IsEqual( VertexDistance val )
        {
            bool flag = (this.dist = agg_math.calc_distance(this.x, this.y, val.x, val.y)) > 1E-14;
            if ( !flag )
                this.dist = 100000000000000.0;
            return flag;
        }
    }
}
