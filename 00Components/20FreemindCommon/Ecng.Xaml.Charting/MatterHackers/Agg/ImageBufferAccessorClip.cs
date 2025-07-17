// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.ImageBufferAccessorClip
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using MatterHackers.Agg.Image;

namespace MatterHackers.Agg
{
    internal sealed class ImageBufferAccessorClip : ImageBufferAccessorCommon
    {
        private byte[] m_OutsideBufferColor;

        public ImageBufferAccessorClip( IImageByte sourceImage, RGBA_Bytes bk )
          : base( sourceImage )
        {
            this.m_OutsideBufferColor = new byte[ 4 ];
            this.m_OutsideBufferColor[ 0 ] = bk.red;
            this.m_OutsideBufferColor[ 1 ] = bk.green;
            this.m_OutsideBufferColor[ 2 ] = bk.blue;
            this.m_OutsideBufferColor[ 3 ] = bk.alpha;
        }

        private byte[ ] pixel( out int bufferByteOffset )
        {
            if ( ( uint ) this.m_x < ( uint ) this.m_SourceImage.Width && ( uint ) this.m_y < ( uint ) this.m_SourceImage.Height )
            {
                bufferByteOffset = this.m_SourceImage.GetBufferOffsetXY( this.m_x, this.m_y );
                return this.m_SourceImage.GetBuffer();
            }
            bufferByteOffset = 0;
            return this.m_OutsideBufferColor;
        }
    }
}
