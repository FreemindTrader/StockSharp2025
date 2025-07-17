// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.ImageBufferAccessorCommon
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using MatterHackers.Agg.Image;

namespace MatterHackers.Agg
{
    internal class ImageBufferAccessorCommon : IImageBufferAccessor
    {
        protected int m_CurrentBufferOffset = -1;
        protected IImageByte m_SourceImage;
        protected int m_x;
        protected int m_x0;
        protected int m_y;
        protected int m_DistanceBetweenPixelsInclusive;
        protected byte[] m_Buffer;
        private int m_Width;

        public ImageBufferAccessorCommon( IImageByte pixf )
        {
            this.attach( pixf );
        }

        private void attach( IImageByte pixf )
        {
            this.m_SourceImage = pixf;
            this.m_Buffer = this.m_SourceImage.GetBuffer();
            this.m_Width = this.m_SourceImage.Width;
            this.m_DistanceBetweenPixelsInclusive = this.m_SourceImage.GetBytesBetweenPixelsInclusive();
        }

        public IImageByte SourceImage
        {
            get
            {
                return this.m_SourceImage;
            }
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

        public byte[ ] span( int x, int y, int len, out int bufferOffset )
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

        public byte[ ] next_x( out int bufferOffset )
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

        public byte[ ] next_y( out int bufferOffset )
        {
            ++this.m_y;
            this.m_x = this.m_x0;
            if ( this.m_CurrentBufferOffset != -1 && ( uint ) this.m_y < ( uint ) this.m_SourceImage.Height )
            {
                this.m_CurrentBufferOffset = this.m_SourceImage.GetBufferOffsetXY( this.m_x, this.m_y );
                this.m_SourceImage.GetBuffer();
                bufferOffset = this.m_CurrentBufferOffset;
                return this.m_Buffer;
            }
            this.m_CurrentBufferOffset = -1;
            return this.pixel( out bufferOffset );
        }
    }
}
