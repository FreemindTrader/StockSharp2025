// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.span_image_resample_rgb
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg
{
    internal class span_image_resample_rgb : span_image_resample
    {
        private const int base_mask = 255;
        private const int downscale_shift = 14;

        public span_image_resample_rgb( IImageBufferAccessor src, ISpanInterpolator inter, ImageFilterLookUpTable filter )
          : base( src, inter, filter )
        {
            if ( src.SourceImage.GetBlender().NumPixelBits != 24 )
                throw new FormatException( "You have to use a rgb blender with span_image_resample_rgb" );
        }

        public override void generate( RGBA_Bytes[ ] span, int spanIndex, int x, int y, int len )
        {
            ISpanInterpolator spanInterpolator = this.interpolator();
            spanInterpolator.begin( ( double ) x + this.filter_dx_dbl(), ( double ) y + this.filter_dy_dbl(), len );
            int[] numArray1 = new int[3];
            int[] numArray2 = this.filter().weight_array();
            int num1 = this.filter().diameter();
            int num2 = num1 << 8;
            int[] numArray3 = numArray2;
            do
            {
                spanInterpolator.coordinates( out x, out y );
                int x1;
                int y1;
                spanInterpolator.local_scale( out x1, out y1 );
                this.adjust_scale( ref x1, ref y1 );
                int num3 = 65536 / x1;
                int num4 = 65536 / y1;
                int num5 = num1 * x1 >> 1;
                int num6 = num1 * y1 >> 1;
                int len1 = num1 * x1 + (int) byte.MaxValue >> 8;
                x += this.filter_dx_int() - num5;
                y += this.filter_dy_int() - num6;
                numArray1[ 0 ] = numArray1[ 1 ] = numArray1[ 2 ] = 8192;
                int y2 = y >> 8;
                int index1 = ((int) byte.MaxValue - (y & (int) byte.MaxValue)) * num4 >> 8;
                int num7 = 0;
                int x2 = x >> 8;
                int num8 = ((int) byte.MaxValue - (x & (int) byte.MaxValue)) * num3 >> 8;
                int index2;
                byte[] numArray4 = this.GetImageBufferAccessor().span(x2, y2, len1, out index2);
                while ( true )
                {
                    int num9 = numArray3[index1];
                    int index3 = num8;
                    while ( true )
                    {
                        int num10 = num9 * numArray3[index3] + 8192 >> 14;
                        numArray1[ 0 ] += ( int ) numArray4[ index2 + 2 ] * num10;
                        numArray1[ 1 ] += ( int ) numArray4[ index2 + 1 ] * num10;
                        numArray1[ 2 ] += ( int ) numArray4[ index2 ] * num10;
                        num7 += num10;
                        index3 += num3;
                        if ( index3 < num2 )
                            numArray4 = this.GetImageBufferAccessor().next_x( out index2 );
                        else
                            break;
                    }
                    index1 += num4;
                    if ( index1 < num2 )
                        numArray4 = this.GetImageBufferAccessor().next_y( out index2 );
                    else
                        break;
                }
                numArray1[ 0 ] /= num7;
                numArray1[ 1 ] /= num7;
                numArray1[ 2 ] /= num7;
                if ( numArray1[ 0 ] < 0 )
                    numArray1[ 0 ] = 0;
                if ( numArray1[ 1 ] < 0 )
                    numArray1[ 1 ] = 0;
                if ( numArray1[ 2 ] < 0 )
                    numArray1[ 2 ] = 0;
                if ( numArray1[ 0 ] > ( int ) byte.MaxValue )
                    numArray1[ 0 ] = ( int ) byte.MaxValue;
                if ( numArray1[ 1 ] > ( int ) byte.MaxValue )
                    numArray1[ 1 ] = ( int ) byte.MaxValue;
                if ( numArray1[ 2 ] > ( int ) byte.MaxValue )
                    numArray1[ 2 ] = ( int ) byte.MaxValue;
                span[ spanIndex ].alpha = byte.MaxValue;
                span[ spanIndex ].red = ( byte ) numArray1[ 0 ];
                span[ spanIndex ].green = ( byte ) numArray1[ 1 ];
                span[ spanIndex ].blue = ( byte ) numArray1[ 2 ];
                ++spanIndex;
                this.interpolator().Next();
            }
            while ( --len != 0 );
        }
    }
}
