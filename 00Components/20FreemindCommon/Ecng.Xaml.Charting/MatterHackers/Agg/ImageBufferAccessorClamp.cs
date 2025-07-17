// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.ImageBufferAccessorClamp
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using MatterHackers.Agg.Image;

namespace MatterHackers.Agg
{
    internal sealed class ImageBufferAccessorClamp : ImageBufferAccessorCommon
    {
        public ImageBufferAccessorClamp( IImageByte pixf )
          : base( pixf )
        {
        }

        private byte[ ] pixel( out int bufferByteOffset )
        {
            int x = this.m_x;
            int y = this.m_y;
            if ( ( uint ) x >= ( uint ) this.m_SourceImage.Width )
                x = x >= 0 ? this.m_SourceImage.Width - 1 : 0;
            if ( ( uint ) y >= ( uint ) this.m_SourceImage.Height )
                y = y >= 0 ? this.m_SourceImage.Height - 1 : 0;
            bufferByteOffset = this.m_SourceImage.GetBufferOffsetXY( x, y );
            return this.m_SourceImage.GetBuffer();
        }
    }
}
