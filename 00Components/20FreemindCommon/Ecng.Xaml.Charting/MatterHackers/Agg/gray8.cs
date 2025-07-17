// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.gray8
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal struct gray8
    {
        private const int base_shift = 8;
        private const uint base_scale = 256;
        private const uint base_mask = 255;
        private byte v;
        private byte a;

        public gray8( uint v_ )
        {
            this = new gray8( v_, ( uint ) byte.MaxValue );
        }

        public gray8( uint v_, uint a_ )
        {
            this.v = ( byte ) v_;
            this.a = ( byte ) a_;
        }

        private gray8( gray8 c, uint a_ )
        {
            this.v = c.v;
            this.a = ( byte ) a_;
        }

        public gray8( RGBA_Floats c )
        {
            this.v = ( byte ) agg_basics.uround( ( 0.299 * ( double ) c.Red0To255 + 0.587 * ( double ) c.Green0To255 + 0.114 * ( double ) c.Blue0To255 ) * ( double ) byte.MaxValue );
            this.a = ( byte ) agg_basics.uround( ( double ) c.Alpha0To255 * ( double ) byte.MaxValue );
        }

        public gray8( RGBA_Floats c, double a_ )
        {
            this.v = ( byte ) agg_basics.uround( ( 0.299 * ( double ) c.Red0To255 + 0.587 * ( double ) c.Green0To255 + 0.114 * ( double ) c.Blue0To255 ) * ( double ) byte.MaxValue );
            this.a = ( byte ) agg_basics.uround( a_ * ( double ) byte.MaxValue );
        }

        public gray8( RGBA_Bytes c )
        {
            this.v = ( byte ) ( c.Red0To255 * 77 + c.Green0To255 * 150 + c.Blue0To255 * 29 >> 8 );
            this.a = ( byte ) c.Alpha0To255;
        }

        public gray8( RGBA_Bytes c, int a_ )
        {
            this.v = ( byte ) ( c.Red0To255 * 77 + c.Green0To255 * 150 + c.Blue0To255 * 29 >> 8 );
            this.a = ( byte ) a_;
        }

        public void clear()
        {
            this.v = this.a = ( byte ) 0;
        }

        public gray8 transparent()
        {
            this.a = ( byte ) 0;
            return this;
        }

        public void opacity( double a_ )
        {
            if ( a_ < 0.0 )
                a_ = 0.0;
            if ( a_ > 1.0 )
                a_ = 1.0;
            this.a = ( byte ) agg_basics.uround( a_ * ( double ) byte.MaxValue );
        }

        public double opacity()
        {
            return ( double ) this.a / ( double ) byte.MaxValue;
        }

        public gray8 premultiply()
        {
            if ( this.a == byte.MaxValue )
                return this;
            if ( this.a == ( byte ) 0 )
            {
                this.v = ( byte ) 0;
                return this;
            }
            this.v = ( byte ) ( ( int ) this.v * ( int ) this.a >> 8 );
            return this;
        }

        public gray8 premultiply( int a_ )
        {
            if ( this.a == byte.MaxValue && a_ >= ( int ) byte.MaxValue )
                return this;
            if ( this.a == ( byte ) 0 || a_ == 0 )
            {
                this.v = this.a = ( byte ) 0;
                return this;
            }
            int num = (int) this.v * a_ / (int) this.a;
            this.v = num > a_ ? ( byte ) a_ : ( byte ) num;
            this.a = ( byte ) a_;
            return this;
        }

        public gray8 demultiply()
        {
            if ( this.a == byte.MaxValue )
                return this;
            if ( this.a == ( byte ) 0 )
            {
                this.v = ( byte ) 0;
                return this;
            }
            int num = (int) this.v * (int) byte.MaxValue / (int) this.a;
            this.v = num > ( int ) byte.MaxValue ? byte.MaxValue : ( byte ) num;
            return this;
        }

        public gray8 gradient( gray8 c, double k )
        {
            int num = agg_basics.uround(k * 256.0);
            gray8 gray8;
            gray8.v = ( byte ) ( ( uint ) this.v + ( uint ) ( ( ( int ) c.v - ( int ) this.v ) * num >> 8 ) );
            gray8.a = ( byte ) ( ( uint ) this.a + ( uint ) ( ( ( int ) c.a - ( int ) this.a ) * num >> 8 ) );
            return gray8;
        }
    }
}
