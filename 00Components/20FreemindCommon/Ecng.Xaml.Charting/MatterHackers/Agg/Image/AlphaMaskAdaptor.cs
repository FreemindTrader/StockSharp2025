// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Image.AlphaMaskAdaptor
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg.Image
{
    internal sealed class AlphaMaskAdaptor : ImageProxy
    {
        private static byte cover_full = byte.MaxValue;
        private IAlphaMask m_mask;
        private ArrayPOD<byte> m_span;

        private void realloc_span( int len )
        {
            if ( len <= this.m_span.Size() )
                return;
            this.m_span.Resize( len + 256 );
        }

        private void init_span( int len )
        {
            this.init_span( len, AlphaMaskAdaptor.cover_full );
        }

        private void init_span( int len, byte cover )
        {
            this.realloc_span( len );
            agg_basics.memset( this.m_span.Array, 0, cover, len );
        }

        private void init_span( int len, byte[ ] covers, int coversIndex )
        {
            this.realloc_span( len );
            byte[] array = this.m_span.Array;
            for ( int index = 0 ; index < len ; ++index )
                array[ index ] = covers[ coversIndex + index ];
        }

        public AlphaMaskAdaptor( IImageByte image, IAlphaMask mask )
          : base( image )
        {
            this._linkedImage = image;
            this.m_mask = mask;
            this.m_span = new ArrayPOD<byte>( ( int ) byte.MaxValue );
        }

        public void AttachImage( IImageByte image )
        {
            this._linkedImage = image;
        }

        public void attach_alpha_mask( IAlphaMask mask )
        {
            this.m_mask = mask;
        }

        public void copy_pixel( int x, int y, RGBA_Bytes c )
        {
            this._linkedImage.BlendPixel( x, y, c, this.m_mask.pixel( x, y ) );
        }

        public override void copy_hline( int x, int y, int len, RGBA_Bytes c )
        {
            throw new NotImplementedException();
        }

        public override void blend_hline( int x1, int y, int x2, RGBA_Bytes c, byte cover )
        {
            int num = x2 - x1 + 1;
            if ( ( int ) cover == ( int ) AlphaMaskAdaptor.cover_full )
            {
                this.realloc_span( num );
                this.m_mask.combine_hspanFullCover( x1, y, this.m_span.Array, 0, num );
                this._linkedImage.blend_solid_hspan( x1, y, num, c, this.m_span.Array, 0 );
            }
            else
            {
                this.init_span( num, cover );
                this.m_mask.combine_hspan( x1, y, this.m_span.Array, 0, num );
                this._linkedImage.blend_solid_hspan( x1, y, num, c, this.m_span.Array, 0 );
            }
        }

        public override void copy_vline( int x, int y, int len, RGBA_Bytes c )
        {
            throw new NotImplementedException();
        }

        public override void blend_vline( int x, int y1, int y2, RGBA_Bytes c, byte cover )
        {
            throw new NotImplementedException();
        }

        public override void blend_solid_hspan( int x, int y, int len, RGBA_Bytes color, byte[ ] covers, int coversIndex )
        {
            byte[] array = this.m_span.Array;
            this.m_mask.combine_hspan( x, y, covers, coversIndex, len );
            this._linkedImage.blend_solid_hspan( x, y, len, color, covers, coversIndex );
        }

        public override void blend_solid_vspan( int x, int y, int len, RGBA_Bytes c, byte[ ] covers, int coversIndex )
        {
            throw new NotImplementedException();
        }

        public override void copy_color_hspan( int x, int y, int len, RGBA_Bytes[ ] colors, int colorsIndex )
        {
            throw new NotImplementedException();
        }

        public override void copy_color_vspan( int x, int y, int len, RGBA_Bytes[ ] colors, int colorsIndex )
        {
            throw new NotImplementedException();
        }

        public override void blend_color_hspan( int x, int y, int len, RGBA_Bytes[ ] colors, int colorsIndex, byte[ ] covers, int coversIndex, bool firstCoverForAll )
        {
            throw new NotImplementedException();
        }

        public override void blend_color_vspan( int x, int y, int len, RGBA_Bytes[ ] colors, int colorsIndex, byte[ ] covers, int coversIndex, bool firstCoverForAll )
        {
            throw new NotImplementedException();
        }

        private enum span_extra_tail_e
        {
            span_extra_tail = 256, // 0x00000100
        }
    }
}
