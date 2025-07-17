// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.span_subdiv_adaptor
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using MatterHackers.Agg.Transform;

namespace MatterHackers.Agg
{
    internal class span_subdiv_adaptor : ISpanInterpolator
    {
        private int m_subdiv_shift;
        private int m_subdiv_size;
        private int m_subdiv_mask;
        private ISpanInterpolator m_interpolator;
        private int m_src_x;
        private double m_src_y;
        private int m_pos;
        private int m_len;
        private const int subpixel_shift = 8;
        private const int subpixel_scale = 256;

        public span_subdiv_adaptor( ISpanInterpolator interpolator )
          : this( interpolator, 4 )
        {
        }

        public span_subdiv_adaptor( ISpanInterpolator interpolator, int subdiv_shift )
        {
            this.m_subdiv_shift = subdiv_shift;
            this.m_subdiv_size = 1 << this.m_subdiv_shift;
            this.m_subdiv_mask = this.m_subdiv_size - 1;
            this.m_interpolator = interpolator;
        }

        public span_subdiv_adaptor( ISpanInterpolator interpolator, double x, double y, int len, int subdiv_shift )
          : this( interpolator, subdiv_shift )
        {
            this.begin( x, y, len );
        }

        public void resynchronize( double xe, double ye, int len )
        {
            throw new NotImplementedException();
        }

        public ISpanInterpolator interpolator()
        {
            return this.m_interpolator;
        }

        public void interpolator( ISpanInterpolator intr )
        {
            this.m_interpolator = intr;
        }

        public ITransform transformer()
        {
            return this.m_interpolator.transformer();
        }

        public void transformer( ITransform trans )
        {
            this.m_interpolator.transformer( trans );
        }

        public int subdiv_shift()
        {
            return this.m_subdiv_shift;
        }

        public void subdiv_shift( int shift )
        {
            this.m_subdiv_shift = shift;
            this.m_subdiv_size = 1 << this.m_subdiv_shift;
            this.m_subdiv_mask = this.m_subdiv_size - 1;
        }

        public void begin( double x, double y, int len )
        {
            this.m_pos = 1;
            this.m_src_x = agg_basics.iround( x * 256.0 ) + 256;
            this.m_src_y = y;
            this.m_len = len;
            if ( len > this.m_subdiv_size )
                len = this.m_subdiv_size;
            this.m_interpolator.begin( x, y, len );
        }

        public void Next()
        {
            this.m_interpolator.Next();
            if ( this.m_pos >= this.m_subdiv_size )
            {
                int len = this.m_len;
                if ( len > this.m_subdiv_size )
                    len = this.m_subdiv_size;
                this.m_interpolator.resynchronize( ( double ) this.m_src_x / 256.0 + ( double ) len, this.m_src_y, len );
                this.m_pos = 0;
            }
            this.m_src_x += 256;
            ++this.m_pos;
            --this.m_len;
        }

        public void coordinates( out int x, out int y )
        {
            this.m_interpolator.coordinates( out x, out y );
        }

        public void local_scale( out int x, out int y )
        {
            this.m_interpolator.local_scale( out x, out y );
        }
    }
}
