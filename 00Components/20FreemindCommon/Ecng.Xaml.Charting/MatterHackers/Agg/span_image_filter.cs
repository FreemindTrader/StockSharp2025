// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.span_image_filter
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal abstract class span_image_filter : ISpanGenerator
    {
        private IImageBufferAccessor imageBufferAccessor;
        protected ISpanInterpolator m_interpolator;
        protected ImageFilterLookUpTable m_filter;
        private double m_dx_dbl;
        private double m_dy_dbl;
        private int m_dx_int;
        private int m_dy_int;

        public span_image_filter()
        {
        }

        public span_image_filter( IImageBufferAccessor src, ISpanInterpolator interpolator )
          : this( src, interpolator, ( ImageFilterLookUpTable ) null )
        {
        }

        public span_image_filter( IImageBufferAccessor src, ISpanInterpolator interpolator, ImageFilterLookUpTable filter )
        {
            this.imageBufferAccessor = src;
            this.m_interpolator = interpolator;
            this.m_filter = filter;
            this.m_dx_dbl = 0.5;
            this.m_dy_dbl = 0.5;
            this.m_dx_int = 128;
            this.m_dy_int = 128;
        }

        public void attach( IImageBufferAccessor v )
        {
            this.imageBufferAccessor = v;
        }

        public abstract void generate( RGBA_Bytes[ ] span, int spanIndex, int x, int y, int len );

        public IImageBufferAccessor GetImageBufferAccessor()
        {
            return this.imageBufferAccessor;
        }

        public ImageFilterLookUpTable filter()
        {
            return this.m_filter;
        }

        public int filter_dx_int()
        {
            return this.m_dx_int;
        }

        public int filter_dy_int()
        {
            return this.m_dy_int;
        }

        public double filter_dx_dbl()
        {
            return this.m_dx_dbl;
        }

        public double filter_dy_dbl()
        {
            return this.m_dy_dbl;
        }

        public void interpolator( ISpanInterpolator v )
        {
            this.m_interpolator = v;
        }

        public void filter( ImageFilterLookUpTable v )
        {
            this.m_filter = v;
        }

        public void filter_offset( double dx, double dy )
        {
            this.m_dx_dbl = dx;
            this.m_dy_dbl = dy;
            this.m_dx_int = agg_basics.iround( dx * 256.0 );
            this.m_dy_int = agg_basics.iround( dy * 256.0 );
        }

        public void filter_offset( double d )
        {
            this.filter_offset( d, d );
        }

        public ISpanInterpolator interpolator()
        {
            return this.m_interpolator;
        }

        public void prepare()
        {
        }
    }
}
