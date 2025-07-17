// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.span_image_filter_rgba_bilinear_clip
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using MatterHackers.Agg.Image;
using MatterHackers.Agg.Transform;

namespace MatterHackers.Agg
{
    internal class span_image_filter_rgba_bilinear_clip : span_image_filter
    {
        private RGBA_Bytes m_OutsideSourceColor;
        private const int base_shift = 8;
        private const int base_scale = 256;
        private const int base_mask = 255;

        public span_image_filter_rgba_bilinear_clip( IImageBufferAccessor src, IColorType back_color, ISpanInterpolator inter )
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
            ImageBuffer sourceImage = (ImageBuffer) this.GetImageBufferAccessor().SourceImage;
            if ( this.m_interpolator.GetType() == typeof( span_interpolator_linear ) && ( ( span_interpolator_linear ) this.m_interpolator ).transformer().GetType() == typeof( Affine ) && ( ( Affine ) ( ( span_interpolator_linear ) this.m_interpolator ).transformer() ).is_identity() )
            {
                int bufferOffset;
                byte[] pixelPointerXy = sourceImage.GetPixelPointerXY(x, y, out bufferOffset);
                do
                {
                    ref RGBA_Bytes local1 = ref span[spanIndex];
                    byte[] numArray1 = pixelPointerXy;
                    int index1 = bufferOffset;
                    int num1 = index1 + 1;
                    int num2 = (int) numArray1[index1];
                    local1.blue = ( byte ) num2;
                    ref RGBA_Bytes local2 = ref span[spanIndex];
                    byte[] numArray2 = pixelPointerXy;
                    int index2 = num1;
                    int num3 = index2 + 1;
                    int num4 = (int) numArray2[index2];
                    local2.green = ( byte ) num4;
                    ref RGBA_Bytes local3 = ref span[spanIndex];
                    byte[] numArray3 = pixelPointerXy;
                    int index3 = num3;
                    int num5 = index3 + 1;
                    int num6 = (int) numArray3[index3];
                    local3.red = ( byte ) num6;
                    ref RGBA_Bytes local4 = ref span[spanIndex];
                    byte[] numArray4 = pixelPointerXy;
                    int index4 = num5;
                    bufferOffset = index4 + 1;
                    int num7 = (int) numArray4[index4];
                    local4.alpha = ( byte ) num7;
                    ++spanIndex;
                }
                while ( --len != 0 );
            }
            else
            {
                this.interpolator().begin( ( double ) x + this.filter_dx_dbl(), ( double ) y + this.filter_dy_dbl(), len );
                int[] accumulatedColor = new int[4];
                int red = (int) this.m_OutsideSourceColor.red;
                int green = (int) this.m_OutsideSourceColor.green;
                int blue = (int) this.m_OutsideSourceColor.blue;
                int alpha = (int) this.m_OutsideSourceColor.alpha;
                int betweenPixelsInclusive = this.GetImageBufferAccessor().SourceImage.GetBytesBetweenPixelsInclusive();
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
                    if ( num1 >= 0 && num2 >= 0 && ( num1 < maxx && num2 < maxy ) )
                    {
                        accumulatedColor[ 0 ] = accumulatedColor[ 1 ] = accumulatedColor[ 2 ] = accumulatedColor[ 3 ] = 32768;
                        x1 &= ( int ) byte.MaxValue;
                        y1 &= ( int ) byte.MaxValue;
                        int bufferOffset;
                        byte[] pixelPointerXy = sourceImage.GetPixelPointerXY(num1, num2, out bufferOffset);
                        int num3 = (256 - x1) * (256 - y1);
                        if ( num3 > ( int ) byte.MaxValue )
                        {
                            accumulatedColor[ 0 ] += num3 * ( int ) pixelPointerXy[ bufferOffset + 2 ];
                            accumulatedColor[ 1 ] += num3 * ( int ) pixelPointerXy[ bufferOffset + 1 ];
                            accumulatedColor[ 2 ] += num3 * ( int ) pixelPointerXy[ bufferOffset ];
                            accumulatedColor[ 3 ] += num3 * ( int ) pixelPointerXy[ bufferOffset + 3 ];
                        }
                        int num4 = x1 * (256 - y1);
                        if ( num4 > ( int ) byte.MaxValue )
                        {
                            bufferOffset += betweenPixelsInclusive;
                            accumulatedColor[ 0 ] += num4 * ( int ) pixelPointerXy[ bufferOffset + 2 ];
                            accumulatedColor[ 1 ] += num4 * ( int ) pixelPointerXy[ bufferOffset + 1 ];
                            accumulatedColor[ 2 ] += num4 * ( int ) pixelPointerXy[ bufferOffset ];
                            accumulatedColor[ 3 ] += num4 * ( int ) pixelPointerXy[ bufferOffset + 3 ];
                        }
                        int num5 = (256 - x1) * y1;
                        if ( num5 > ( int ) byte.MaxValue )
                        {
                            int y2 = num2 + 1;
                            pixelPointerXy = sourceImage.GetPixelPointerXY( num1, y2, out bufferOffset );
                            accumulatedColor[ 0 ] += num5 * ( int ) pixelPointerXy[ bufferOffset + 2 ];
                            accumulatedColor[ 1 ] += num5 * ( int ) pixelPointerXy[ bufferOffset + 1 ];
                            accumulatedColor[ 2 ] += num5 * ( int ) pixelPointerXy[ bufferOffset ];
                            accumulatedColor[ 3 ] += num5 * ( int ) pixelPointerXy[ bufferOffset + 3 ];
                        }
                        int num6 = x1 * y1;
                        if ( num6 > ( int ) byte.MaxValue )
                        {
                            bufferOffset += betweenPixelsInclusive;
                            accumulatedColor[ 0 ] += num6 * ( int ) pixelPointerXy[ bufferOffset + 2 ];
                            accumulatedColor[ 1 ] += num6 * ( int ) pixelPointerXy[ bufferOffset + 1 ];
                            accumulatedColor[ 2 ] += num6 * ( int ) pixelPointerXy[ bufferOffset ];
                            accumulatedColor[ 3 ] += num6 * ( int ) pixelPointerXy[ bufferOffset + 3 ];
                        }
                        accumulatedColor[ 0 ] >>= 16;
                        accumulatedColor[ 1 ] >>= 16;
                        accumulatedColor[ 2 ] >>= 16;
                        accumulatedColor[ 3 ] >>= 16;
                    }
                    else if ( num1 < -1 || num2 < -1 || ( num1 > maxx || num2 > maxy ) )
                    {
                        accumulatedColor[ 0 ] = red;
                        accumulatedColor[ 1 ] = green;
                        accumulatedColor[ 2 ] = blue;
                        accumulatedColor[ 3 ] = alpha;
                    }
                    else
                    {
                        accumulatedColor[ 0 ] = accumulatedColor[ 1 ] = accumulatedColor[ 2 ] = accumulatedColor[ 3 ] = 32768;
                        x1 &= ( int ) byte.MaxValue;
                        y1 &= ( int ) byte.MaxValue;
                        int weight1 = (256 - x1) * (256 - y1);
                        if ( weight1 > ( int ) byte.MaxValue )
                            this.BlendInFilterPixel( accumulatedColor, red, green, blue, alpha, ( IImageByte ) sourceImage, maxx, maxy, num1, num2, weight1 );
                        int x_lr1 = num1 + 1;
                        int weight2 = x1 * (256 - y1);
                        if ( weight2 > ( int ) byte.MaxValue )
                            this.BlendInFilterPixel( accumulatedColor, red, green, blue, alpha, ( IImageByte ) sourceImage, maxx, maxy, x_lr1, num2, weight2 );
                        int x_lr2 = x_lr1 - 1;
                        int y_lr = num2 + 1;
                        int weight3 = (256 - x1) * y1;
                        if ( weight3 > ( int ) byte.MaxValue )
                            this.BlendInFilterPixel( accumulatedColor, red, green, blue, alpha, ( IImageByte ) sourceImage, maxx, maxy, x_lr2, y_lr, weight3 );
                        int x_lr3 = x_lr2 + 1;
                        int weight4 = x1 * y1;
                        if ( weight4 > ( int ) byte.MaxValue )
                            this.BlendInFilterPixel( accumulatedColor, red, green, blue, alpha, ( IImageByte ) sourceImage, maxx, maxy, x_lr3, y_lr, weight4 );
                        accumulatedColor[ 0 ] >>= 16;
                        accumulatedColor[ 1 ] >>= 16;
                        accumulatedColor[ 2 ] >>= 16;
                        accumulatedColor[ 3 ] >>= 16;
                    }
                    span[ spanIndex ].red = ( byte ) accumulatedColor[ 0 ];
                    span[ spanIndex ].green = ( byte ) accumulatedColor[ 1 ];
                    span[ spanIndex ].blue = ( byte ) accumulatedColor[ 2 ];
                    span[ spanIndex ].alpha = ( byte ) accumulatedColor[ 3 ];
                    ++spanIndex;
                    spanInterpolator.Next();
                }
                while ( --len != 0 );
            }
        }

        private void BlendInFilterPixel( int[ ] accumulatedColor, int back_r, int back_g, int back_b, int back_a, IImageByte SourceRenderingBuffer, int maxx, int maxy, int x_lr, int y_lr, int weight )
        {
            if ( ( uint ) x_lr <= ( uint ) maxx && ( uint ) y_lr <= ( uint ) maxy )
            {
                int bufferOffsetXy = SourceRenderingBuffer.GetBufferOffsetXY(x_lr, y_lr);
                byte[] buffer = SourceRenderingBuffer.GetBuffer();
                accumulatedColor[ 0 ] += weight * ( int ) buffer[ bufferOffsetXy + 2 ];
                accumulatedColor[ 1 ] += weight * ( int ) buffer[ bufferOffsetXy + 1 ];
                accumulatedColor[ 2 ] += weight * ( int ) buffer[ bufferOffsetXy ];
                accumulatedColor[ 3 ] += weight * ( int ) buffer[ bufferOffsetXy + 3 ];
            }
            else
            {
                accumulatedColor[ 0 ] += back_r * weight;
                accumulatedColor[ 1 ] += back_g * weight;
                accumulatedColor[ 2 ] += back_b * weight;
                accumulatedColor[ 3 ] += back_a * weight;
            }
        }
    }
}
