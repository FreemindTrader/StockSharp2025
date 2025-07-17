// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.span_gradient
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class span_gradient : ISpanGenerator
    {
        public const int gradient_subpixel_shift = 4;
        public const int gradient_subpixel_scale = 16;
        public const int gradient_subpixel_mask = 15;
        public const int subpixelShift = 8;
        public const int downscale_shift = 4;
        private ISpanInterpolator m_interpolator;
        private IGradient m_gradient_function;
        private IColorFunction m_color_function;
        private int m_d1;
        private int m_d2;

        public span_gradient()
        {
        }

        public span_gradient( ISpanInterpolator inter, IGradient gradient_function, IColorFunction color_function, double d1, double d2 )
        {
            this.m_interpolator = inter;
            this.m_gradient_function = gradient_function;
            this.m_color_function = color_function;
            this.m_d1 = agg_basics.iround( d1 * 16.0 );
            this.m_d2 = agg_basics.iround( d2 * 16.0 );
        }

        public ISpanInterpolator interpolator()
        {
            return this.m_interpolator;
        }

        public IGradient gradient_function()
        {
            return this.m_gradient_function;
        }

        public IColorFunction color_function()
        {
            return this.m_color_function;
        }

        public double d1()
        {
            return ( double ) this.m_d1 / 16.0;
        }

        public double d2()
        {
            return ( double ) this.m_d2 / 16.0;
        }

        public void interpolator( ISpanInterpolator i )
        {
            this.m_interpolator = i;
        }

        public void gradient_function( IGradient gf )
        {
            this.m_gradient_function = gf;
        }

        public void color_function( IColorFunction cf )
        {
            this.m_color_function = cf;
        }

        public void d1( double v )
        {
            this.m_d1 = agg_basics.iround( v * 16.0 );
        }

        public void d2( double v )
        {
            this.m_d2 = agg_basics.iround( v * 16.0 );
        }

        public void prepare()
        {
        }

        public void generate( RGBA_Bytes[ ] span, int spanIndex, int x, int y, int len )
        {
            int num = this.m_d2 - this.m_d1;
            if ( num < 1 )
                num = 1;
            this.m_interpolator.begin( ( double ) x + 0.5, ( double ) y + 0.5, len );
            do
            {
                this.m_interpolator.coordinates( out x, out y );
                int index = (this.m_gradient_function.calculate(x >> 4, y >> 4, this.m_d2) - this.m_d1) * this.m_color_function.size() / num;
                if ( index < 0 )
                    index = 0;
                if ( index >= this.m_color_function.size() )
                    index = this.m_color_function.size() - 1;
                span[ spanIndex++ ] = this.m_color_function[ index ];
                this.m_interpolator.Next();
            }
            while ( --len != 0 );
        }
    }
}
