// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.scanline_hit_test
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg
{
    internal class scanline_hit_test : IScanlineCache
    {
        private int m_x;
        private bool m_hit;

        public scanline_hit_test( int x )
        {
            this.m_x = x;
            this.m_hit = false;
        }

        public void ResetSpans()
        {
        }

        public void finalize( int nothing )
        {
        }

        public void add_cell( int x, int nothing )
        {
            if ( this.m_x != x )
                return;
            this.m_hit = true;
        }

        public void add_span( int x, int len, int nothing )
        {
            if ( this.m_x < x || this.m_x >= x + len )
                return;
            this.m_hit = true;
        }

        public int num_spans()
        {
            return 1;
        }

        public bool hit()
        {
            return this.m_hit;
        }

        public void reset( int min_x, int max_x )
        {
            throw new NotImplementedException();
        }

        public ScanlineSpan begin()
        {
            throw new NotImplementedException();
        }

        public ScanlineSpan GetNextScanlineSpan()
        {
            throw new NotImplementedException();
        }

        public int y()
        {
            throw new NotImplementedException();
        }

        public byte[ ] GetCovers()
        {
            throw new NotImplementedException();
        }
    }
}
