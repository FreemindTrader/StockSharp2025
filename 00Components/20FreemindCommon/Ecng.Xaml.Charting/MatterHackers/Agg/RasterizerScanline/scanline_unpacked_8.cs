// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.RasterizerScanline.scanline_unpacked_8
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace MatterHackers.Agg.RasterizerScanline
{
    internal sealed class scanline_unpacked_8 : IScanlineCache
    {
        private int m_min_x;
        private int m_last_x;
        private int m_y;
        private ArrayPOD<byte> m_covers;
        private ArrayPOD<ScanlineSpan> m_spans;
        private int m_span_index;
        private int m_interator_index;

        public ScanlineSpan GetNextScanlineSpan()
        {
            ++this.m_interator_index;
            return this.m_spans.Array[ this.m_interator_index - 1 ];
        }

        public scanline_unpacked_8()
        {
            this.m_last_x = 2147483632;
            this.m_covers = new ArrayPOD<byte>( 1000 );
            this.m_spans = new ArrayPOD<ScanlineSpan>( 1000 );
        }

        public void reset( int min_x, int max_x )
        {
            int size = max_x - min_x + 2;
            if ( size > this.m_spans.Size() )
            {
                this.m_spans.Resize( size );
                this.m_covers.Resize( size );
            }
            this.m_last_x = 2147483632;
            this.m_min_x = min_x;
            this.m_span_index = 0;
        }

        public void add_cell( int x, int cover )
        {
            x -= this.m_min_x;
            this.m_covers.Array[ x ] = ( byte ) cover;
            if ( x == this.m_last_x + 1 )
            {
                ++this.m_spans.Array[ this.m_span_index ].len;
            }
            else
            {
                ++this.m_span_index;
                this.m_spans.Array[ this.m_span_index ].x = x + this.m_min_x;
                this.m_spans.Array[ this.m_span_index ].len = 1;
                this.m_spans.Array[ this.m_span_index ].cover_index = x;
            }
            this.m_last_x = x;
        }

        public void add_span( int x, int len, int cover )
        {
            x -= this.m_min_x;
            for ( int index = 0 ; index < len ; ++index )
                this.m_covers.Array[ x + index ] = ( byte ) cover;
            if ( x == this.m_last_x + 1 )
            {
                this.m_spans.Array[ this.m_span_index ].len += len;
            }
            else
            {
                ++this.m_span_index;
                this.m_spans.Array[ this.m_span_index ].x = x + this.m_min_x;
                this.m_spans.Array[ this.m_span_index ].len = len;
                this.m_spans.Array[ this.m_span_index ].cover_index = x;
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
            return this.m_covers.Array;
        }
    }
}
