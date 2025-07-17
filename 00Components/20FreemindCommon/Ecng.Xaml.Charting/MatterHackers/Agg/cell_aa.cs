// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.cell_aa
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal struct cell_aa
    {
        public int x;
        public int y;
        public int cover;
        public int area;
        public int left;
        public int right;

        public void initial()
        {
            this.x = int.MaxValue;
            this.y = int.MaxValue;
            this.cover = 0;
            this.area = 0;
            this.left = -1;
            this.right = -1;
        }

        public void Set( cell_aa cellB )
        {
            this.x = cellB.x;
            this.y = cellB.y;
            this.cover = cellB.cover;
            this.area = cellB.area;
            this.left = cellB.left;
            this.right = cellB.right;
        }

        public void style( cell_aa cellB )
        {
            this.left = cellB.left;
            this.right = cellB.right;
        }

        public bool not_equal( int ex, int ey, cell_aa cell )
        {
            return ( uint ) ( ex - this.x | ey - this.y | this.left - cell.left | this.right - cell.right ) > 0U;
        }
    }
}
