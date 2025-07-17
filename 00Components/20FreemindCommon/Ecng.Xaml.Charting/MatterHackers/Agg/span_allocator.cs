// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.span_allocator
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class span_allocator
    {
        private ArrayPOD<RGBA_Bytes> m_span;

        public span_allocator()
        {
            this.m_span = new ArrayPOD<RGBA_Bytes>( ( int ) byte.MaxValue );
        }

        public ArrayPOD<RGBA_Bytes> allocate( int span_len )
        {
            if ( span_len > this.m_span.Size() )
                this.m_span.Resize( span_len + ( int ) byte.MaxValue >> 8 << 8 );
            return this.m_span;
        }

        public ArrayPOD<RGBA_Bytes> span()
        {
            return this.m_span;
        }

        public int max_span_len()
        {
            return this.m_span.Size();
        }
    }
}
