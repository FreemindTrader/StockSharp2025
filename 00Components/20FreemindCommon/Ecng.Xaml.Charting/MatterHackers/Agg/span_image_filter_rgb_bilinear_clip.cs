// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.span_image_filter_rgb_bilinear_clip
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using MatterHackers.Agg.Image;

namespace MatterHackers.Agg
{
    internal class span_image_filter_rgb_bilinear_clip : span_image_filter
    {
        private RGBA_Bytes m_OutsideSourceColor;
        private const int base_shift = 8;
        private const int base_scale = 256;
        private const int base_mask = 255;

        public span_image_filter_rgb_bilinear_clip( IImageBufferAccessor src, IColorType back_color, ISpanInterpolator inter )
          : base( src, inter, ( ImageFilterLookUpTable ) null )
        {
            this.m_OutsideSourceColor = back_color.GetAsRGBA_Bytes();
        }

        public IColorType background_color()
        {
            return ( IColorType ) this.m_OutsideSourceColor;
        }

        public void background_color( IColorType v )
        {
            this.m_OutsideSourceColor = v.GetAsRGBA_Bytes();
        }

        public override void generate( RGBA_Bytes[ ] span, int spanIndex, int x, int y, int len )
        {
            this.interpolator().begin( ( double ) x + this.filter_dx_dbl(), ( double ) y + this.filter_dy_dbl(), len );
            int[] accumulatedColor = new int[3];
            int red = (int) this.m_OutsideSourceColor.red;
            int green = (int) this.m_OutsideSourceColor.green;
            int blue = (int) this.m_OutsideSourceColor.blue;
            int alpha = (int) this.m_OutsideSourceColor.alpha;
            ImageBuffer sourceImage = (ImageBuffer) this.GetImageBufferAccessor().SourceImage;
            int maxx = sourceImage.Width - 1;
            int maxy = sourceImage.Height - 1;
            ISpanInterpolator spanInterpolator = this.interpolator();
            do
            {
                int x1;
                int y1;
                spanInterpolator.coordinates( out x1, out y1 );
                x1 -= this.filter_dx_int();
                y1 -= this.filter_dy_int();
                int num1 = x1 >> 8;
                int num2 = y1 >> 8;
                int num3;
                if ( num1 >= 0 && num2 >= 0 && ( num1 < maxx && num2 < maxy ) )
                {
                    accumulatedColor[ 0 ] = accumulatedColor[ 1 ] = accumulatedColor[ 2 ] = 32768;
                    x1 &= ( int ) byte.MaxValue;
                    y1 &= ( int ) byte.MaxValue;
                    int bufferOffset1;
                    byte[] pixelPointerXy1 = sourceImage.GetPixelPointerXY(num1, num2, out bufferOffset1);
                    int num4 = (256 - x1) * (256 - y1);
                    accumulatedColor[ 0 ] += num4 * ( int ) pixelPointerXy1[ bufferOffset1 + 2 ];
                    accumulatedColor[ 1 ] += num4 * ( int ) pixelPointerXy1[ bufferOffset1 + 1 ];
                    accumulatedColor[ 2 ] += num4 * ( int ) pixelPointerXy1[ bufferOffset1 ];
                    int bufferOffset2 = bufferOffset1 + 3;
                    int num5 = x1 * (256 - y1);
                    accumulatedColor[ 0 ] += num5 * ( int ) pixelPointerXy1[ bufferOffset2 + 2 ];
                    accumulatedColor[ 1 ] += num5 * ( int ) pixelPointerXy1[ bufferOffset2 + 1 ];
                    accumulatedColor[ 2 ] += num5 * ( int ) pixelPointerXy1[ bufferOffset2 ];
                    int y2 = num2 + 1;
                    byte[] pixelPointerXy2 = sourceImage.GetPixelPointerXY(num1, y2, out bufferOffset2);
                    int num6 = (256 - x1) * y1;
                    accumulatedColor[ 0 ] += num6 * ( int ) pixelPointerXy2[ bufferOffset2 + 2 ];
                    accumulatedColor[ 1 ] += num6 * ( int ) pixelPointerXy2[ bufferOffset2 + 1 ];
                    accumulatedColor[ 2 ] += num6 * ( int ) pixelPointerXy2[ bufferOffset2 ];
                    bufferOffset1 = bufferOffset2 + 3;
                    int num7 = x1 * y1;
                    accumulatedColor[ 0 ] += num7 * ( int ) pixelPointerXy2[ bufferOffset1 + 2 ];
                    accumulatedColor[ 1 ] += num7 * ( int ) pixelPointerXy2[ bufferOffset1 + 1 ];
                    accumulatedColor[ 2 ] += num7 * ( int ) pixelPointerXy2[ bufferOffset1 ];
                    accumulatedColor[ 0 ] >>= 16;
                    accumulatedColor[ 1 ] >>= 16;
                    accumulatedColor[ 2 ] >>= 16;
                    num3 = ( int ) byte.MaxValue;
                }
                else if ( num1 < -1 || num2 < -1 || ( num1 > maxx || num2 > maxy ) )
                {
                    accumulatedColor[ 0 ] = red;
                    accumulatedColor[ 1 ] = green;
                    accumulatedColor[ 2 ] = blue;
                    num3 = alpha;
                }
                else
                {
                    accumulatedColor[ 0 ] = accumulatedColor[ 1 ] = accumulatedColor[ 2 ] = 32768;
                    int sourceAlpha = 32768;
                    x1 &= ( int ) byte.MaxValue;
                    y1 &= ( int ) byte.MaxValue;
                    int weight1 = (256 - x1) * (256 - y1);
                    this.BlendInFilterPixel( accumulatedColor, ref sourceAlpha, red, green, blue, alpha, sourceImage, maxx, maxy, num1, num2, weight1 );
                    int x_lr1 = num1 + 1;
                    int weight2 = x1 * (256 - y1);
                    this.BlendInFilterPixel( accumulatedColor, ref sourceAlpha, red, green, blue, alpha, sourceImage, maxx, maxy, x_lr1, num2, weight2 );
                    int x_lr2 = x_lr1 - 1;
                    int y_lr = num2 + 1;
                    int weight3 = (256 - x1) * y1;
                    this.BlendInFilterPixel( accumulatedColor, ref sourceAlpha, red, green, blue, alpha, sourceImage, maxx, maxy, x_lr2, y_lr, weight3 );
                    int x_lr3 = x_lr2 + 1;
                    int weight4 = x1 * y1;
                    this.BlendInFilterPixel( accumulatedColor, ref sourceAlpha, red, green, blue, alpha, sourceImage, maxx, maxy, x_lr3, y_lr, weight4 );
                    accumulatedColor[ 0 ] >>= 16;
                    accumulatedColor[ 1 ] >>= 16;
                    accumulatedColor[ 2 ] >>= 16;
                    num3 = sourceAlpha >> 16;
                }
                span[ spanIndex ].red = ( byte ) accumulatedColor[ 0 ];
                span[ spanIndex ].green = ( byte ) accumulatedColor[ 1 ];
                span[ spanIndex ].blue = ( byte ) accumulatedColor[ 2 ];
                span[ spanIndex ].alpha = ( byte ) num3;
                ++spanIndex;
                spanInterpolator.Next();
            }
            while ( --len != 0 );
        }

        private void BlendInFilterPixel( int[ ] accumulatedColor, ref int sourceAlpha, int back_r, int back_g, int back_b, int back_a, ImageBuffer SourceRenderingBuffer, int maxx, int maxy, int x_lr, int y_lr, int weight )
        {
            if ( ( uint ) x_lr <= ( uint ) maxx && ( uint ) y_lr <= ( uint ) maxy )
            {
                int bufferOffset;
                byte[] pixelPointerXy = SourceRenderingBuffer.GetPixelPointerXY(x_lr, y_lr, out bufferOffset);
                accumulatedColor[ 0 ] += weight * ( int ) pixelPointerXy[ bufferOffset + 2 ];
                accumulatedColor[ 1 ] += weight * ( int ) pixelPointerXy[ bufferOffset + 1 ];
                accumulatedColor[ 2 ] += weight * ( int ) pixelPointerXy[ bufferOffset ];
                sourceAlpha += weight * ( int ) byte.MaxValue;
            }
            else
            {
                accumulatedColor[ 0 ] += back_r * weight;
                accumulatedColor[ 1 ] += back_g * weight;
                accumulatedColor[ 2 ] += back_b * weight;
                sourceAlpha += back_a * weight;
            }
        }
    }
}
