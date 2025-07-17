// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.ImageBufferAccessorCommonFloat
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using MatterHackers.Agg.Image;

namespace MatterHackers.Agg
{
    internal class ImageBufferAccessorCommonFloat : IImageBufferAccessorFloat
    {
        protected int m_CurrentBufferOffset = -1;
        protected IImageFloat m_SourceImage;
        protected int m_x;
        protected int m_x0;
        protected int m_y;
        protected int m_DistanceBetweenPixelsInclusive;
        protected float[] m_Buffer;
        private int m_Width;

        public ImageBufferAccessorCommonFloat( IImageFloat pixf )
        {
            this.attach( pixf );
        }

        private void attach( IImageFloat pixf )
        {
            this.m_SourceImage = pixf;
            this.m_Buffer = this.m_SourceImage.GetBuffer();
            this.m_Width = this.m_SourceImage.Width;
            this.m_DistanceBetweenPixelsInclusive = this.m_SourceImage.GetFloatsBetweenPixelsInclusive();
        }

        public IImageFloat SourceImage
        {
            get
            {
                return this.m_SourceImage;
            }
        }

        private float[ ] pixel( out int bufferFloatOffset )
        {
            int x = this.m_x;
            int y = this.m_y;
            if ( ( uint ) x >= ( uint ) this.m_SourceImage.Width )
                x = x >= 0 ? this.m_SourceImage.Width - 1 : 0;
            if ( ( uint ) y >= ( uint ) this.m_SourceImage.Height )
                y = y >= 0 ? this.m_SourceImage.Height - 1 : 0;
            bufferFloatOffset = this.m_SourceImage.GetBufferOffsetXY( x, y );
            return this.m_SourceImage.GetBuffer();
        }

        public float[ ] span( int x, int y, int len, out int bufferOffset )
        {
            this.m_x = this.m_x0 = x;
            this.m_y = y;
            if ( ( uint ) y < ( uint ) this.m_SourceImage.Height && x >= 0 && x + len <= this.m_SourceImage.Width )
            {
                bufferOffset = this.m_SourceImage.GetBufferOffsetXY( x, y );
                this.m_Buffer = this.m_SourceImage.GetBuffer();
                this.m_CurrentBufferOffset = bufferOffset;
                return this.m_Buffer;
            }
            this.m_CurrentBufferOffset = -1;
            return this.pixel( out bufferOffset );
        }

        public float[ ] next_x( out int bufferOffset )
        {
            if ( this.m_CurrentBufferOffset != -1 )
            {
                this.m_CurrentBufferOffset += this.m_DistanceBetweenPixelsInclusive;
                bufferOffset = this.m_CurrentBufferOffset;
                return this.m_Buffer;
            }
            ++this.m_x;
            return this.pixel( out bufferOffset );
        }

        public float[ ] next_y( out int bufferOffset )
        {
            ++this.m_y;
            this.m_x = this.m_x0;
            if ( this.m_CurrentBufferOffset != -1 && ( uint ) this.m_y < ( uint ) this.m_SourceImage.Height )
            {
                this.m_CurrentBufferOffset = this.m_SourceImage.GetBufferOffsetXY( this.m_x, this.m_y );
                bufferOffset = this.m_CurrentBufferOffset;
                return this.m_Buffer;
            }
            this.m_CurrentBufferOffset = -1;
            return this.pixel( out bufferOffset );
        }
    }
}
