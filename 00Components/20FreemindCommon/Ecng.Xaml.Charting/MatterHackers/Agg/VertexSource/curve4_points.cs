// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VertexSource.curve4_points
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace MatterHackers.Agg.VertexSource
{
    internal sealed class curve4_points
    {
        private double[] cp = new double[8];

        public curve4_points()
        {
        }

        public curve4_points( double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4 )
        {
            this.cp[ 0 ] = x1;
            this.cp[ 1 ] = y1;
            this.cp[ 2 ] = x2;
            this.cp[ 3 ] = y2;
            this.cp[ 4 ] = x3;
            this.cp[ 5 ] = y3;
            this.cp[ 6 ] = x4;
            this.cp[ 7 ] = y4;
        }

        public void init( double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4 )
        {
            this.cp[ 0 ] = x1;
            this.cp[ 1 ] = y1;
            this.cp[ 2 ] = x2;
            this.cp[ 3 ] = y2;
            this.cp[ 4 ] = x3;
            this.cp[ 5 ] = y3;
            this.cp[ 6 ] = x4;
            this.cp[ 7 ] = y4;
        }

        public double this[ int i ]
        {
            get
            {
                return this.cp[ i ];
            }
        }
    }
}
