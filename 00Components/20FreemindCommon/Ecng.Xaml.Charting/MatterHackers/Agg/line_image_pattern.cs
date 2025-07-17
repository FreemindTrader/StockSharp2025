// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.line_image_pattern
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using MatterHackers.Agg.Image;

namespace MatterHackers.Agg
{
    internal class line_image_pattern : ImageBuffer
    {
        private ImageBuffer m_buf = new ImageBuffer();
        private IPatternFilter m_filter;
        private int m_dilation;
        private int m_dilation_hr;
        private byte[] m_data;
        private int m_DataSizeInBytes;
        private int m_width;
        private int m_height;
        private int m_width_hr;
        private int m_half_height_hr;
        private int m_offset_y_hr;

        public line_image_pattern( IPatternFilter filter )
        {
            this.m_filter = filter;
            this.m_dilation = filter.dilation() + 1;
            this.m_dilation_hr = this.m_dilation << 8;
            this.m_width = 0;
            this.m_height = 0;
            this.m_width_hr = 0;
            this.m_half_height_hr = 0;
            this.m_offset_y_hr = 0;
        }

        ~line_image_pattern()
        {
            if ( this.m_DataSizeInBytes <= 0 )
                return;
            this.m_data = ( byte[ ] ) null;
        }

        public line_image_pattern( IPatternFilter filter, line_image_pattern src )
        {
            this.m_filter = filter;
            this.m_dilation = filter.dilation() + 1;
            this.m_dilation_hr = this.m_dilation << 8;
            this.m_width = 0;
            this.m_height = 0;
            this.m_width_hr = 0;
            this.m_half_height_hr = 0;
            this.m_offset_y_hr = 0;
            this.create( ( IImageByte ) src );
        }

        public void create( IImageByte src )
        {
            this.m_height = agg_basics.uceil( ( double ) src.Height );
            this.m_width = agg_basics.uceil( ( double ) src.Width );
            this.m_width_hr = agg_basics.uround( ( double ) ( src.Width * 256 ) );
            this.m_half_height_hr = agg_basics.uround( ( double ) ( src.Height * 256 / 2 ) );
            this.m_offset_y_hr = this.m_dilation_hr + this.m_half_height_hr - 128;
            this.m_half_height_hr += 128;
            int width = this.m_width + this.m_dilation * 2;
            int height = this.m_height + this.m_dilation * 2;
            int distanceInBytesBetweenPixelsInclusive = src.BitDepth / 8;
            int num = width * height * distanceInBytesBetweenPixelsInclusive;
            if ( this.m_DataSizeInBytes < num )
            {
                this.m_DataSizeInBytes = num;
                this.m_data = new byte[ this.m_DataSizeInBytes ];
            }
            this.m_buf.AttachBuffer( this.m_data, 0, width, height, width * distanceInBytesBetweenPixelsInclusive, src.BitDepth, distanceInBytesBetweenPixelsInclusive );
            byte[] buffer1 = this.m_buf.GetBuffer();
            byte[] buffer2 = src.GetBuffer();
            for ( int y = 0 ; y < this.m_height ; ++y )
            {
                for ( int x = 0 ; x < this.m_width ; ++x )
                {
                    int bufferOffsetXy1 = src.GetBufferOffsetXY(x, y);
                    int bufferOffsetXy2 = this.m_buf.GetBufferOffsetXY(this.m_dilation, y + this.m_dilation);
                    for ( int index = 0 ; index < distanceInBytesBetweenPixelsInclusive ; ++index )
                        buffer1[ bufferOffsetXy2++ ] = buffer2[ bufferOffsetXy1++ ];
                }
            }
            for ( int y = 0 ; y < this.m_height ; ++y )
            {
                int bufferOffsetXy1 = src.GetBufferOffsetXY(0, y);
                int bufferOffsetXy2 = this.m_buf.GetBufferOffsetXY(this.m_dilation + this.m_width, y);
                int bufferOffsetXy3 = src.GetBufferOffsetXY(this.m_width - this.m_dilation, y);
                int bufferOffsetXy4 = this.m_buf.GetBufferOffsetXY(0, y);
                for ( int index1 = 0 ; index1 < this.m_dilation ; ++index1 )
                {
                    for ( int index2 = 0 ; index2 < distanceInBytesBetweenPixelsInclusive ; ++index2 )
                    {
                        buffer1[ bufferOffsetXy2++ ] = buffer2[ bufferOffsetXy1++ ];
                        buffer1[ bufferOffsetXy4++ ] = buffer2[ bufferOffsetXy3++ ];
                    }
                }
            }
        }

        public int pattern_width()
        {
            return this.m_width_hr;
        }

        public int line_width()
        {
            return this.m_half_height_hr;
        }

        public double width()
        {
            return ( double ) this.m_height;
        }

        public void pixel( RGBA_Bytes[ ] destBuffer, int destBufferOffset, int x, int y )
        {
            this.m_filter.pixel_high_res( this.m_buf, destBuffer, destBufferOffset, x % this.m_width_hr + this.m_dilation_hr, y + this.m_offset_y_hr );
        }

        public IPatternFilter filter()
        {
            return this.m_filter;
        }
    }
}
