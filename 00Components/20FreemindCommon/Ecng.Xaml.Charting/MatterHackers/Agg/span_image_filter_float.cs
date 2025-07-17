// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.span_image_filter_float
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal abstract class span_image_filter_float : ISpanGeneratorFloat
    {
        private IImageBufferAccessorFloat m_ImageBufferAccessor;
        protected ISpanInterpolatorFloat m_interpolator;
        protected IImageFilterFunction m_filterFunction;
        private float m_dx_dbl;
        private float m_dy_dbl;

        public span_image_filter_float()
        {
        }

        public span_image_filter_float( IImageBufferAccessorFloat src, ISpanInterpolatorFloat interpolator )
          : this( src, interpolator, ( IImageFilterFunction ) null )
        {
        }

        public span_image_filter_float( IImageBufferAccessorFloat src, ISpanInterpolatorFloat interpolator, IImageFilterFunction filterFunction )
        {
            this.m_ImageBufferAccessor = src;
            this.m_interpolator = interpolator;
            this.m_filterFunction = filterFunction;
            this.m_dx_dbl = 0.5f;
            this.m_dy_dbl = 0.5f;
        }

        public void attach( IImageBufferAccessorFloat v )
        {
            this.m_ImageBufferAccessor = v;
        }

        public abstract void generate( RGBA_Floats[ ] span, int spanIndex, int x, int y, int len );

        public IImageBufferAccessorFloat source()
        {
            return this.m_ImageBufferAccessor;
        }

        public IImageFilterFunction filterFunction()
        {
            return this.m_filterFunction;
        }

        public float filter_dx_dbl()
        {
            return this.m_dx_dbl;
        }

        public float filter_dy_dbl()
        {
            return this.m_dy_dbl;
        }

        public void interpolator( ISpanInterpolatorFloat v )
        {
            this.m_interpolator = v;
        }

        public void filterFunction( IImageFilterFunction v )
        {
            this.m_filterFunction = v;
        }

        public void filter_offset( float dx, float dy )
        {
            this.m_dx_dbl = dx;
            this.m_dy_dbl = dy;
        }

        public void filter_offset( float d )
        {
            this.filter_offset( d, d );
        }

        public ISpanInterpolatorFloat interpolator()
        {
            return this.m_interpolator;
        }

        public void prepare()
        {
        }
    }
}
