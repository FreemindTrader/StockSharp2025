// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.gradient_linear_color
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal struct gradient_linear_color : IColorFunction
    {
        private RGBA_Bytes m_c1;
        private RGBA_Bytes m_c2;
        private int m_size;

        public gradient_linear_color( RGBA_Bytes c1, RGBA_Bytes c2 )
        {
            this = new gradient_linear_color( c1, c2, 256 );
        }

        public gradient_linear_color( RGBA_Bytes c1, RGBA_Bytes c2, int size )
        {
            this.m_c1 = c1;
            this.m_c2 = c2;
            this.m_size = size;
        }

        public int size()
        {
            return this.m_size;
        }

        public RGBA_Bytes this[ int v ]
        {
            get
            {
                return this.m_c1.gradient( this.m_c2, ( double ) v / ( double ) ( this.m_size - 1 ) );
            }
        }

        public void colors( RGBA_Bytes c1, RGBA_Bytes c2 )
        {
            this.colors( c1, c2, 256 );
        }

        public void colors( RGBA_Bytes c1, RGBA_Bytes c2, int size )
        {
            this.m_c1 = c1;
            this.m_c2 = c2;
            this.m_size = size;
        }
    }
}
