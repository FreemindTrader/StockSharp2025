// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.span_image_filter_rgb
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg
{
    internal class span_image_filter_rgb : span_image_filter
    {
        private const int base_mask = 255;

        public span_image_filter_rgb( IImageBufferAccessor src, ISpanInterpolator inter, ImageFilterLookUpTable filter )
          : base( src, inter, filter )
        {
            if ( src.SourceImage.GetBytesBetweenPixelsInclusive() != 3 )
                throw new NotSupportedException( "span_image_filter_rgb must have a 24 bit DestImage" );
        }

        public override void generate( RGBA_Bytes[ ] span, int spanIndex, int x, int y, int len )
        {
            this.interpolator().begin( ( double ) x + this.filter_dx_dbl(), ( double ) y + this.filter_dy_dbl(), len );
            int len1 = this.m_filter.diameter();
            int num1 = this.m_filter.start();
            int[] numArray1 = this.m_filter.weight_array();
            ISpanInterpolator spanInterpolator = this.interpolator();
            do
            {
                spanInterpolator.coordinates( out x, out y );
                x -= this.filter_dx_int();
                y -= this.filter_dy_int();
                int num2 = x;
                int num3 = y;
                int num4 = num2 >> 8;
                int num5 = num3 >> 8;
                int num6;
                int num7 = num6 = 8192;
                int num8 = num6;
                int num9 = num6;
                int num10 = num2 & (int) byte.MaxValue;
                int num11 = len1;
                int index1 = (int) byte.MaxValue - (num3 & (int) byte.MaxValue);
                int index2;
                byte[] numArray2 = this.GetImageBufferAccessor().span(num4 + num1, num5 + num1, len1, out index2);
                while ( true )
                {
                    int num12 = len1;
                    int num13 = numArray1[index1];
                    int index3 = (int) byte.MaxValue - num10;
                    while ( true )
                    {
                        int num14 = num13 * numArray1[index3] + 8192 >> 14;
                        num9 += num14 * ( int ) numArray2[ index2 + 2 ];
                        num8 += num14 * ( int ) numArray2[ index2 + 1 ];
                        num7 += num14 * ( int ) numArray2[ index2 ];
                        if ( --num12 != 0 )
                        {
                            index3 += 256;
                            this.GetImageBufferAccessor().next_x( out index2 );
                        }
                        else
                            break;
                    }
                    if ( --num11 != 0 )
                    {
                        index1 += 256;
                        numArray2 = this.GetImageBufferAccessor().next_y( out index2 );
                    }
                    else
                        break;
                }
                int num15 = num9 >> 14;
                int num16 = num8 >> 14;
                int num17 = num7 >> 14;
                if ( ( uint ) num15 > ( uint ) byte.MaxValue )
                {
                    if ( num15 < 0 )
                        num15 = 0;
                    if ( num15 > ( int ) byte.MaxValue )
                        num15 = ( int ) byte.MaxValue;
                }
                if ( ( uint ) num16 > ( uint ) byte.MaxValue )
                {
                    if ( num16 < 0 )
                        num16 = 0;
                    if ( num16 > ( int ) byte.MaxValue )
                        num16 = ( int ) byte.MaxValue;
                }
                if ( ( uint ) num17 > ( uint ) byte.MaxValue )
                {
                    if ( num17 < 0 )
                        num17 = 0;
                    if ( num17 > ( int ) byte.MaxValue )
                        num17 = ( int ) byte.MaxValue;
                }
                span[ spanIndex ].alpha = byte.MaxValue;
                span[ spanIndex ].red = ( byte ) num15;
                span[ spanIndex ].green = ( byte ) num16;
                span[ spanIndex ].blue = ( byte ) num17;
                ++spanIndex;
                spanInterpolator.Next();
            }
            while ( --len != 0 );
        }
    }
}
