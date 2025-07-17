// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.scanline_bin
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal sealed class scanline_bin : IScanlineCache
    {
        private int m_last_x;
        private int m_y;
        private ArrayPOD<ScanlineSpan> m_spans;
        private int m_span_index;
        private int m_interator_index;

        public ScanlineSpan GetNextScanlineSpan()
        {
            ++this.m_interator_index;
            return this.m_spans.Array[ this.m_interator_index - 1 ];
        }

        public scanline_bin()
        {
            this.m_last_x = 2147483632;
            this.m_spans = new ArrayPOD<ScanlineSpan>( 1000 );
            this.m_span_index = 0;
        }

        public void reset( int min_x, int max_x )
        {
            int size = max_x - min_x + 3;
            if ( size > this.m_spans.Size() )
                this.m_spans.Resize( size );
            this.m_last_x = 2147483632;
            this.m_span_index = 0;
        }

        public void add_cell( int x, int cover )
        {
            if ( x == this.m_last_x + 1 )
            {
                ++this.m_spans.Array[ this.m_span_index ].len;
            }
            else
            {
                ++this.m_span_index;
                this.m_spans.Array[ this.m_span_index ].x = x;
                this.m_spans.Array[ this.m_span_index ].len = 1;
            }
            this.m_last_x = x;
        }

        public void add_span( int x, int len, int cover )
        {
            if ( x == this.m_last_x + 1 )
            {
                this.m_spans.Array[ this.m_span_index ].len += len;
            }
            else
            {
                ++this.m_span_index;
                this.m_spans.Array[ this.m_span_index ].x = x;
                this.m_spans.Array[ this.m_span_index ].len = len;
            }
            this.m_last_x = x + len - 1;
        }

        public void finalize( int y )
        {
            this.m_y = y;
        }

        public void ResetSpans()
        {
            this.m_last_x = 2147483632;
            this.m_span_index = 0;
        }

        public int y()
        {
            return this.m_y;
        }

        public int num_spans()
        {
            return this.m_span_index;
        }

        public ScanlineSpan begin()
        {
            this.m_interator_index = 1;
            return this.GetNextScanlineSpan();
        }

        public byte[ ] GetCovers()
        {
            return ( byte[ ] ) null;
        }
    }
}
