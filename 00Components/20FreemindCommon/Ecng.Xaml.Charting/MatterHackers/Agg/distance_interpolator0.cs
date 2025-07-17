// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.distance_interpolator0
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class distance_interpolator0
    {
        private int m_dx;
        private int m_dy;
        private int m_dist;

        public distance_interpolator0()
        {
        }

        public distance_interpolator0( int x1, int y1, int x2, int y2, int x, int y )
        {
            this.m_dx = LineAABasics.line_mr( x2 ) - LineAABasics.line_mr( x1 );
            this.m_dy = LineAABasics.line_mr( y2 ) - LineAABasics.line_mr( y1 );
            this.m_dist = ( LineAABasics.line_mr( x + 128 ) - LineAABasics.line_mr( x2 ) ) * this.m_dy - ( LineAABasics.line_mr( y + 128 ) - LineAABasics.line_mr( y2 ) ) * this.m_dx;
            this.m_dx <<= 4;
            this.m_dy <<= 4;
        }

        public void inc_x()
        {
            this.m_dist += this.m_dy;
        }

        public int dist()
        {
            return this.m_dist;
        }
    }
}
