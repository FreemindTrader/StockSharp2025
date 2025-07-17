// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.LineRenderer
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal abstract class LineRenderer
    {
        private RGBA_Bytes m_color;

        public RGBA_Bytes color()
        {
            return this.m_color;
        }

        public void color( IColorType c )
        {
            this.m_color = c.GetAsRGBA_Bytes();
        }

        public abstract void semidot( LineRenderer.CompareFunction cmp, int xc1, int yc1, int xc2, int yc2 );

        public abstract void semidot_hline( LineRenderer.CompareFunction cmp, int xc1, int yc1, int xc2, int yc2, int x1, int y1, int x2 );

        public abstract void pie( int xc, int yc, int x1, int y1, int x2, int y2 );

        public abstract void line0( line_parameters lp );

        public abstract void line1( line_parameters lp, int sx, int sy );

        public abstract void line2( line_parameters lp, int ex, int ey );

        public abstract void line3( line_parameters lp, int sx, int sy, int ex, int ey );

        public delegate bool CompareFunction( int value );
    }
}
