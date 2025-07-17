// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.IAlphaMask
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal interface IAlphaMask
    {
        byte pixel( int x, int y );

        byte combine_pixel( int x, int y, byte val );

        void fill_hspan( int x, int y, byte[ ] dst, int dstIndex, int num_pix );

        void fill_vspan( int x, int y, byte[ ] dst, int dstIndex, int num_pix );

        void combine_hspanFullCover( int x, int y, byte[ ] dst, int dstIndex, int num_pix );

        void combine_hspan( int x, int y, byte[ ] dst, int dstIndex, int num_pix );

        void combine_vspan( int x, int y, byte[ ] dst, int dstIndex, int num_pix );
    }
}
