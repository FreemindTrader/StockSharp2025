// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.span_image_resample
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal abstract class span_image_resample : span_image_filter
    {
        private int m_scale_limit;
        private int m_blur_x;
        private int m_blur_y;

        public span_image_resample( IImageBufferAccessor src, ISpanInterpolator inter, ImageFilterLookUpTable filter )
          : base( src, inter, filter )
        {
            this.m_scale_limit = 20;
            this.m_blur_x = 256;
            this.m_blur_y = 256;
        }

        private int scale_limit()
        {
            return this.m_scale_limit;
        }

        private void scale_limit( int v )
        {
            this.m_scale_limit = v;
        }

        private double blur_x()
        {
            return ( double ) this.m_blur_x / 256.0;
        }

        private double blur_y()
        {
            return ( double ) this.m_blur_y / 256.0;
        }

        private void blur_x( double v )
        {
            this.m_blur_x = agg_basics.uround( v * 256.0 );
        }

        private void blur_y( double v )
        {
            this.m_blur_y = agg_basics.uround( v * 256.0 );
        }

        public void blur( double v )
        {
            this.m_blur_x = this.m_blur_y = agg_basics.uround( v * 256.0 );
        }

        protected void adjust_scale( ref int rx, ref int ry )
        {
            if ( rx < 256 )
                rx = 256;
            if ( ry < 256 )
                ry = 256;
            if ( rx > 256 * this.m_scale_limit )
                rx = 256 * this.m_scale_limit;
            if ( ry > 256 * this.m_scale_limit )
                ry = 256 * this.m_scale_limit;
            rx = rx * this.m_blur_x >> 8;
            ry = ry * this.m_blur_y >> 8;
            if ( rx < 256 )
                rx = 256;
            if ( ry >= 256 )
                return;
            ry = 256;
        }
    }
}
