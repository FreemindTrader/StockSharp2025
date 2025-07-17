// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.RasterizerScanline.ScanlineCachePacked8
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace MatterHackers.Agg.RasterizerScanline
{
    internal sealed class ScanlineCachePacked8 : IScanlineCache
    {
        private int m_last_x;
        private int m_y;
        private byte[] m_covers;
        private int m_cover_index;
        private ScanlineSpan[] m_spans;
        private int m_span_index;
        private int m_interator_index;

        public ScanlineSpan GetNextScanlineSpan()
        {
            ++this.m_interator_index;
            return this.m_spans[ this.m_interator_index - 1 ];
        }

        public ScanlineCachePacked8()
        {
            this.m_last_x = 2147483632;
            this.m_covers = new byte[ 1000 ];
            this.m_spans = new ScanlineSpan[ 1000 ];
        }

        public void reset( int min_x, int max_x )
        {
            int length = max_x - min_x + 3;
            if ( length > this.m_spans.Length )
            {
                this.m_spans = new ScanlineSpan[ length ];
                this.m_covers = new byte[ length ];
            }
            this.m_last_x = 2147483632;
            this.m_cover_index = 0;
            this.m_span_index = 0;
            this.m_spans[ this.m_span_index ].len = 0;
        }

        public void add_cell( int x, int cover )
        {
            this.m_covers[ this.m_cover_index ] = ( byte ) cover;
            if ( x == this.m_last_x + 1 && this.m_spans[ this.m_span_index ].len > 0 )
            {
                ++this.m_spans[ this.m_span_index ].len;
            }
            else
            {
                ++this.m_span_index;
                this.m_spans[ this.m_span_index ].cover_index = this.m_cover_index;
                this.m_spans[ this.m_span_index ].x = ( int ) ( short ) x;
                this.m_spans[ this.m_span_index ].len = 1;
            }
            this.m_last_x = x;
            ++this.m_cover_index;
        }

        public void add_cells( int x, int len, byte[ ] covers, int coversIndex )
        {
            for ( int index = 0 ; index < len ; ++index )
                this.m_covers[ this.m_cover_index + index ] = covers[ index ];
            if ( x == this.m_last_x + 1 && this.m_spans[ this.m_span_index ].len > 0 )
            {
                this.m_spans[ this.m_span_index ].len += ( int ) ( short ) len;
            }
            else
            {
                ++this.m_span_index;
                this.m_spans[ this.m_span_index ].cover_index = this.m_cover_index;
                this.m_spans[ this.m_span_index ].x = ( int ) ( short ) x;
                this.m_spans[ this.m_span_index ].len = ( int ) ( short ) len;
            }
            this.m_cover_index += len;
            this.m_last_x = x + len - 1;
        }

        public void add_span( int x, int len, int cover )
        {
            if ( x == this.m_last_x + 1 && this.m_spans[ this.m_span_index ].len < 0 && cover == this.m_spans[ this.m_span_index ].cover_index )
            {
                this.m_spans[ this.m_span_index ].len -= ( int ) ( short ) len;
            }
            else
            {
                this.m_covers[ this.m_cover_index ] = ( byte ) cover;
                ++this.m_span_index;
                this.m_spans[ this.m_span_index ].cover_index = this.m_cover_index++;
                this.m_spans[ this.m_span_index ].x = ( int ) ( short ) x;
                this.m_spans[ this.m_span_index ].len = ( int ) ( short ) -len;
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
            this.m_cover_index = 0;
            this.m_span_index = 0;
            this.m_spans[ this.m_span_index ].len = 0;
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
            return this.m_covers;
        }
    }
}
