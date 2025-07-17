// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.distance_interpolator00
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class distance_interpolator00
    {
        private int m_dx1;
        private int m_dy1;
        private int m_dx2;
        private int m_dy2;
        private int m_dist1;
        private int m_dist2;

        public distance_interpolator00()
        {
        }

        public distance_interpolator00( int xc, int yc, int x1, int y1, int x2, int y2, int x, int y )
        {
            this.m_dx1 = LineAABasics.line_mr( x1 ) - LineAABasics.line_mr( xc );
            this.m_dy1 = LineAABasics.line_mr( y1 ) - LineAABasics.line_mr( yc );
            this.m_dx2 = LineAABasics.line_mr( x2 ) - LineAABasics.line_mr( xc );
            this.m_dy2 = LineAABasics.line_mr( y2 ) - LineAABasics.line_mr( yc );
            this.m_dist1 = ( LineAABasics.line_mr( x + 128 ) - LineAABasics.line_mr( x1 ) ) * this.m_dy1 - ( LineAABasics.line_mr( y + 128 ) - LineAABasics.line_mr( y1 ) ) * this.m_dx1;
            this.m_dist2 = ( LineAABasics.line_mr( x + 128 ) - LineAABasics.line_mr( x2 ) ) * this.m_dy2 - ( LineAABasics.line_mr( y + 128 ) - LineAABasics.line_mr( y2 ) ) * this.m_dx2;
            this.m_dx1 <<= 4;
            this.m_dy1 <<= 4;
            this.m_dx2 <<= 4;
            this.m_dy2 <<= 4;
        }

        public void inc_x()
        {
            this.m_dist1 += this.m_dy1;
            this.m_dist2 += this.m_dy2;
        }

        public int dist1()
        {
            return this.m_dist1;
        }

        public int dist2()
        {
            return this.m_dist2;
        }
    }
}
