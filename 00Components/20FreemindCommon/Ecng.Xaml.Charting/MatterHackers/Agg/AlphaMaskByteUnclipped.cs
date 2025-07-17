// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.AlphaMaskByteUnclipped
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using MatterHackers.Agg.Image;

namespace MatterHackers.Agg
{
    internal sealed class AlphaMaskByteUnclipped : IAlphaMask
    {
        public static int cover_shift = 8;
        public static int cover_none = 0;
        public static int cover_full = (int) byte.MaxValue;
        private IImageByte m_rbuf;
        private uint m_Step;
        private uint m_Offset;

        public AlphaMaskByteUnclipped( IImageByte rbuf, uint Step, uint Offset )
        {
            this.m_Step = Step;
            this.m_Offset = Offset;
            this.m_rbuf = rbuf;
        }

        public void attach( IImageByte rbuf )
        {
            this.m_rbuf = rbuf;
        }

        public byte pixel( int x, int y )
        {
            return this.m_rbuf.GetBuffer()[ this.m_rbuf.GetBufferOffsetXY( x, y ) ];
        }

        public byte combine_pixel( int x, int y, byte val )
        {
            int bufferOffsetXy = this.m_rbuf.GetBufferOffsetXY(x, y);
            byte[] buffer = this.m_rbuf.GetBuffer();
            return ( byte ) ( ( int ) byte.MaxValue + ( int ) val * ( int ) buffer[ bufferOffsetXy ] >> 8 );
        }

        public void fill_hspan( int x, int y, byte[ ] dst, int dstIndex, int num_pix )
        {
            throw new NotImplementedException();
        }

        public void combine_hspanFullCover( int x, int y, byte[ ] covers, int coversIndex, int count )
        {
            int bufferOffsetXy = this.m_rbuf.GetBufferOffsetXY(x, y);
            byte[] buffer = this.m_rbuf.GetBuffer();
            do
            {
                covers[ coversIndex++ ] = buffer[ bufferOffsetXy++ ];
            }
            while ( --count != 0 );
        }

        public void combine_hspan( int x, int y, byte[ ] covers, int coversIndex, int count )
        {
            int bufferOffsetXy = this.m_rbuf.GetBufferOffsetXY(x, y);
            byte[] buffer = this.m_rbuf.GetBuffer();
            do
            {
                covers[ coversIndex ] = ( byte ) ( ( int ) byte.MaxValue + ( int ) covers[ coversIndex ] * ( int ) buffer[ bufferOffsetXy ] >> 8 );
                ++coversIndex;
                ++bufferOffsetXy;
            }
            while ( --count != 0 );
        }

        public void fill_vspan( int x, int y, byte[ ] buffer, int bufferIndex, int num_pix )
        {
            throw new NotImplementedException();
        }

        public void combine_vspan( int x, int y, byte[ ] dst, int dstIndex, int num_pix )
        {
            throw new NotImplementedException();
        }
    }
}
