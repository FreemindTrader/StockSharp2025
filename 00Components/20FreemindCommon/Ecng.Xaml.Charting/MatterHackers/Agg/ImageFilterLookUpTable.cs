// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.ImageFilterLookUpTable
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class ImageFilterLookUpTable
    {
        private double m_radius;
        private int m_diameter;
        private int m_start;
        private ArrayPOD<int> m_weight_array;

        public void calculate( IImageFilterFunction filter )
        {
            this.calculate( filter, true );
        }

        public void calculate( IImageFilterFunction filter, bool normalization )
        {
            this.realloc_lut( filter.radius() );
            int num1 = this.diameter() << 7;
            for ( int index = 0 ; index < num1 ; ++index )
            {
                double x = (double) index / 256.0;
                double num2 = filter.calc_weight(x);
                this.m_weight_array.Array[ num1 + index ] = this.m_weight_array.Array[ num1 - index ] = agg_basics.iround( num2 * 16384.0 );
            }
            this.m_weight_array.Array[ 0 ] = this.m_weight_array.Array[ ( this.diameter() << 8 ) - 1 ];
            if ( !normalization )
                return;
            this.normalize();
        }

        public ImageFilterLookUpTable()
        {
            this.m_weight_array = new ArrayPOD<int>( 256 );
            this.m_radius = 0.0;
            this.m_diameter = 0;
            this.m_start = 0;
        }

        public ImageFilterLookUpTable( IImageFilterFunction filter )
          : this( filter, true )
        {
        }

        public ImageFilterLookUpTable( IImageFilterFunction filter, bool normalization )
        {
            this.m_weight_array = new ArrayPOD<int>( 256 );
            this.calculate( filter, normalization );
        }

        public double radius()
        {
            return this.m_radius;
        }

        public int diameter()
        {
            return this.m_diameter;
        }

        public int start()
        {
            return this.m_start;
        }

        public int[ ] weight_array()
        {
            return this.m_weight_array.Array;
        }

        public void normalize()
        {
            int num1 = 1;
            for ( int index1 = 0 ; index1 < 256 ; ++index1 )
            {
            label_1:
                int num2 = 0;
                for ( int index2 = 0 ; index2 < this.m_diameter ; ++index2 )
                    num2 += this.m_weight_array.Array[ index2 * 256 + index1 ];
                if ( num2 != 16384 )
                {
                    double num3 = 16384.0 / (double) num2;
                    int num4 = 0;
                    for ( int index2 = 0 ; index2 < this.m_diameter ; ++index2 )
                        num4 += this.m_weight_array.Array[ index2 * 256 + index1 ] = agg_basics.iround( ( double ) this.m_weight_array.Array[ index2 * 256 + index1 ] * num3 );
                    int num5 = num4 - 16384;
                    int num6 = num5 > 0 ? -1 : 1;
                    int num7 = 0;
                    while ( true )
                    {
                        if ( num7 < this.m_diameter && num5 != 0 )
                        {
                            num1 ^= 1;
                            int num8 = num1 != 0 ? this.m_diameter / 2 + num7 / 2 : this.m_diameter / 2 - num7 / 2;
                            if ( this.m_weight_array.Array[ num8 * 256 + index1 ] < 16384 )
                            {
                                this.m_weight_array.Array[ num8 * 256 + index1 ] += num6;
                                num5 += num6;
                            }
                            ++num7;
                        }
                        else
                            goto label_1;
                    }
                }
            }
            int num9 = this.m_diameter << 7;
            for ( int index = 0 ; index < num9 ; ++index )
                this.m_weight_array.Array[ num9 + index ] = this.m_weight_array.Array[ num9 - index ];
            this.m_weight_array.Array[ 0 ] = this.m_weight_array.Array[ ( this.diameter() << 8 ) - 1 ];
        }

        private void realloc_lut( double radius )
        {
            this.m_radius = radius;
            this.m_diameter = agg_basics.uceil( radius ) * 2;
            this.m_start = -( this.m_diameter / 2 - 1 );
            int size = this.m_diameter << 8;
            if ( size <= this.m_weight_array.Size() )
                return;
            this.m_weight_array.Resize( size );
        }

        public enum image_filter_scale_e
        {
            image_filter_shift = 14, // 0x0000000E
            image_filter_mask = 16383, // 0x00003FFF
            image_filter_scale = 16384, // 0x00004000
        }

        public enum image_subpixel_scale_e
        {
            image_subpixel_shift = 8,
            image_subpixel_mask = 255, // 0x000000FF
            image_subpixel_scale = 256, // 0x00000100
        }
    }
}
