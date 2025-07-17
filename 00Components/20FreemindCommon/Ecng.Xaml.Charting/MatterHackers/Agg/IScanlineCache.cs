// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.IScanlineCache
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal interface IScanlineCache
    {
        void finalize( int y );

        void reset( int min_x, int max_x );

        void ResetSpans();

        int num_spans();

        ScanlineSpan begin();

        ScanlineSpan GetNextScanlineSpan();

        int y();

        byte[ ] GetCovers();

        void add_cell( int x, int cover );

        void add_span( int x, int len, int cover );
    }
}
