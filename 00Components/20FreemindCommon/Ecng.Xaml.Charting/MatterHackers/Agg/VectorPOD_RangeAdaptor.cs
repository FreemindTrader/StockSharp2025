// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VectorPOD_RangeAdaptor
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class VectorPOD_RangeAdaptor
    {
        private VectorPOD<int> m_array;
        private int m_start;
        private int m_size;

        public VectorPOD_RangeAdaptor( VectorPOD<int> array, int start, int size )
        {
            this.m_array = array;
            this.m_start = start;
            this.m_size = size;
        }

        public int size()
        {
            return this.m_size;
        }

        public int this[ int i ]
        {
            get
            {
                return this.m_array.Array[ this.m_start + i ];
            }
            set
            {
                this.m_array.Array[ this.m_start + i ] = value;
            }
        }

        public int at( int i )
        {
            return this.m_array.Array[ this.m_start + i ];
        }

        public int value_at( int i )
        {
            return this.m_array.Array[ this.m_start + i ];
        }
    }
}
