// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.ImageBufferAccessorClipFloat
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using MatterHackers.Agg.Image;

namespace MatterHackers.Agg
{
    internal sealed class ImageBufferAccessorClipFloat : ImageBufferAccessorCommonFloat
    {
        private float[] m_OutsideBufferColor;

        public ImageBufferAccessorClipFloat( IImageFloat sourceImage, RGBA_Floats bk )
          : base( sourceImage )
        {
            this.m_OutsideBufferColor = new float[ 4 ];
            this.m_OutsideBufferColor[ 0 ] = bk.red;
            this.m_OutsideBufferColor[ 1 ] = bk.green;
            this.m_OutsideBufferColor[ 2 ] = bk.blue;
            this.m_OutsideBufferColor[ 3 ] = bk.alpha;
        }

        private float[ ] pixel( out int bufferFloatOffset )
        {
            if ( ( uint ) this.m_x < ( uint ) this.m_SourceImage.Width && ( uint ) this.m_y < ( uint ) this.m_SourceImage.Height )
            {
                bufferFloatOffset = this.m_SourceImage.GetBufferOffsetXY( this.m_x, this.m_y );
                return this.m_SourceImage.GetBuffer();
            }
            bufferFloatOffset = 0;
            return this.m_OutsideBufferColor;
        }
    }
}
