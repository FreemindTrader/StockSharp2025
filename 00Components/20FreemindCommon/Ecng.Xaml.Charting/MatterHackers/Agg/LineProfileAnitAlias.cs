// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.LineProfileAnitAlias
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class LineProfileAnitAlias
    {
        private ArrayPOD<byte> m_profile = new ArrayPOD<byte>();
        private byte[] m_gamma = new byte[256];
        private const int subpixel_shift = 8;
        private const int subpixel_scale = 256;
        private const int subpixel_mask = 255;
        private const int aa_shift = 8;
        private const int aa_scale = 256;
        private const int aa_mask = 255;
        private int m_subpixel_width;
        private double m_min_width;
        private double m_smoother_width;

        public LineProfileAnitAlias()
        {
            this.m_subpixel_width = 0;
            this.m_min_width = 1.0;
            this.m_smoother_width = 1.0;
            for ( int index = 0 ; index < 256 ; ++index )
                this.m_gamma[ index ] = ( byte ) index;
        }

        public LineProfileAnitAlias( double w, IGammaFunction gamma_function )
        {
            this.m_subpixel_width = 0;
            this.m_min_width = 1.0;
            this.m_smoother_width = 1.0;
            this.gamma( gamma_function );
            this.width( w );
        }

        public void min_width( double w )
        {
            this.m_min_width = w;
        }

        public void smoother_width( double w )
        {
            this.m_smoother_width = w;
        }

        public void gamma( IGammaFunction gamma_function )
        {
            for ( int index = 0 ; index < 256 ; ++index )
                this.m_gamma[ index ] = ( byte ) agg_basics.uround( gamma_function.GetGamma( ( double ) index / ( double ) byte.MaxValue ) * ( double ) byte.MaxValue );
        }

        public void width( double w )
        {
            if ( w < 0.0 )
                w = 0.0;
            if ( w < this.m_smoother_width )
                w += w;
            else
                w += this.m_smoother_width;
            w *= 0.5;
            w -= this.m_smoother_width;
            double smootherWidth = this.m_smoother_width;
            if ( w < 0.0 )
            {
                smootherWidth += w;
                w = 0.0;
            }
            this.set( w, smootherWidth );
        }

        public int profile_size()
        {
            return this.m_profile.Size();
        }

        public int subpixel_width()
        {
            return this.m_subpixel_width;
        }

        public double min_width()
        {
            return this.m_min_width;
        }

        public double smoother_width()
        {
            return this.m_smoother_width;
        }

        public byte value( int dist )
        {
            return this.m_profile.Array[ dist + 512 ];
        }

        private byte[ ] profile( double w )
        {
            this.m_subpixel_width = agg_basics.uround( w * 256.0 );
            int size = this.m_subpixel_width + 1536;
            if ( size > this.m_profile.Size() )
                this.m_profile.Resize( size );
            return this.m_profile.Array;
        }

        private void set( double center_width, double smoother_width )
        {
            double num1 = 1.0;
            if ( center_width == 0.0 )
                center_width = 1.0 / 256.0;
            if ( smoother_width == 0.0 )
                smoother_width = 1.0 / 256.0;
            double num2 = center_width + smoother_width;
            if ( num2 < this.m_min_width )
            {
                double num3 = num2 / this.m_min_width;
                num1 *= num3;
                center_width /= num3;
                smoother_width /= num3;
            }
            byte[] numArray = this.profile(center_width + smoother_width);
            int num4 = (int) (center_width * 256.0);
            int num5 = (int) (smoother_width * 256.0);
            int num6 = 512;
            int num7 = num6 + num4;
            int num8 = (int) this.m_gamma[(int) (num1 * (double) byte.MaxValue)];
            int num9 = num6;
            for ( int index = 0 ; index < num4 ; ++index )
                numArray[ num9++ ] = ( byte ) num8;
            for ( int index = 0 ; index < num5 ; ++index )
                numArray[ num7++ ] = this.m_gamma[ ( int ) ( ( num1 - num1 * ( ( double ) index / ( double ) num5 ) ) * ( double ) byte.MaxValue ) ];
            int num10 = numArray.Length - num5 - num4 - 512;
            int num11 = (int) this.m_gamma[0];
            for ( int index = 0 ; index < num10 ; ++index )
                numArray[ num7++ ] = ( byte ) num11;
            int num12 = num6;
            for ( int index = 0 ; index < 512 ; ++index )
                numArray[ --num12 ] = numArray[ num6++ ];
            for ( int index = 0 ; index < numArray.Length ; ++index )
                this.m_profile.Array[ index ] = numArray[ index ];
        }
    }
}
