// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.AlphaMaskByteClipped
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using MatterHackers.Agg.Image;

namespace MatterHackers.Agg
{
    internal sealed class AlphaMaskByteClipped : IAlphaMask
    {
        public static int cover_shift = 8;
        public static int cover_none = 0;
        public static int cover_full = (int) byte.MaxValue;
        private IImageByte m_rbuf;
        private uint m_Step;
        private uint m_Offset;

        public AlphaMaskByteClipped( IImageByte rbuf, uint Step, uint Offset )
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
            if ( ( uint ) x < ( uint ) this.m_rbuf.Width && ( uint ) y < ( uint ) this.m_rbuf.Height )
                return this.m_rbuf.GetBuffer()[ this.m_rbuf.GetBufferOffsetXY( x, y ) ];
            return 0;
        }

        public byte combine_pixel( int x, int y, byte val )
        {
            if ( ( uint ) x >= ( uint ) this.m_rbuf.Width || ( uint ) y >= ( uint ) this.m_rbuf.Height )
                return 0;
            int bufferOffsetXy = this.m_rbuf.GetBufferOffsetXY(x, y);
            byte[] buffer = this.m_rbuf.GetBuffer();
            return ( byte ) ( ( int ) val * ( int ) buffer[ bufferOffsetXy ] + ( int ) byte.MaxValue >> 8 );
        }

        public void fill_hspan( int x, int y, byte[ ] dst, int dstIndex, int num_pix )
        {
            throw new NotImplementedException();
        }

        public void combine_hspanFullCover( int x, int y, byte[ ] covers, int coversIndex, int num_pix )
        {
            int num1 = this.m_rbuf.Width - 1;
            int num2 = this.m_rbuf.Height - 1;
            int num3 = num_pix;
            if ( y < 0 || y > num2 )
            {
                agg_basics.MemClear( covers, coversIndex, num_pix );
            }
            else
            {
                if ( x < 0 )
                {
                    num3 += x;
                    if ( num3 <= 0 )
                    {
                        agg_basics.MemClear( covers, coversIndex, num_pix );
                        return;
                    }
                    agg_basics.MemClear( covers, coversIndex, -x );
                    coversIndex -= x;
                    x = 0;
                }
                if ( x + num3 > num1 )
                {
                    int Count = x + num3 - num1 - 1;
                    num3 -= Count;
                    if ( num3 <= 0 )
                    {
                        agg_basics.MemClear( covers, coversIndex, num_pix );
                        return;
                    }
                    agg_basics.MemClear( covers, coversIndex + num3, Count );
                }
                int bufferOffsetXy = this.m_rbuf.GetBufferOffsetXY(x, y);
                byte[] buffer = this.m_rbuf.GetBuffer();
                do
                {
                    covers[ coversIndex++ ] = buffer[ bufferOffsetXy++ ];
                }
                while ( --num3 != 0 );
            }
        }

        public void combine_hspan( int x, int y, byte[ ] buffer, int bufferIndex, int num_pix )
        {
            int num1 = this.m_rbuf.Width - 1;
            int num2 = this.m_rbuf.Height - 1;
            int num3 = num_pix;
            byte[] dest = buffer;
            int destIndex = bufferIndex;
            if ( y < 0 || y > num2 )
            {
                agg_basics.MemClear( buffer, bufferIndex, num_pix );
            }
            else
            {
                if ( x < 0 )
                {
                    num3 += x;
                    if ( num3 <= 0 )
                    {
                        agg_basics.MemClear( buffer, bufferIndex, num_pix );
                        return;
                    }
                    agg_basics.MemClear( dest, destIndex, -x );
                    destIndex -= x;
                    x = 0;
                }
                if ( x + num3 > num1 )
                {
                    int Count = x + num3 - num1 - 1;
                    num3 -= Count;
                    if ( num3 <= 0 )
                    {
                        agg_basics.MemClear( buffer, bufferIndex, num_pix );
                        return;
                    }
                    agg_basics.MemClear( dest, destIndex + num3, Count );
                }
                int bufferOffsetXy = this.m_rbuf.GetBufferOffsetXY(x, y);
                byte[] buffer1 = this.m_rbuf.GetBuffer();
                do
                {
                    dest[ destIndex ] = ( byte ) ( ( int ) dest[ destIndex ] * ( int ) buffer1[ bufferOffsetXy ] + ( int ) byte.MaxValue >> 8 );
                    ++destIndex;
                    ++bufferOffsetXy;
                }
                while ( --num3 != 0 );
            }
        }

        public void fill_vspan( int x, int y, byte[ ] buffer, int bufferIndex, int num_pix )
        {
            throw new NotImplementedException();
        }

        public void combine_vspan( int x, int y, byte[ ] buffer, int bufferIndex, int num_pix )
        {
            throw new NotImplementedException();
        }
    }
}
