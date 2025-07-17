// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.span_image_filter_rgb_bilinear
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using MatterHackers.Agg.Image;

namespace MatterHackers.Agg
{
    internal class span_image_filter_rgb_bilinear : span_image_filter
    {
        private const int base_shift = 8;
        private const int base_scale = 256;
        private const int base_mask = 255;

        public span_image_filter_rgb_bilinear( IImageBufferAccessor src, ISpanInterpolator inter )
          : base( src, inter, ( ImageFilterLookUpTable ) null )
        {
            if ( src.SourceImage.GetBytesBetweenPixelsInclusive() != 3 )
                throw new NotSupportedException( "span_image_filter_rgb must have a 24 bit DestImage" );
        }

        public override void generate( RGBA_Bytes[ ] span, int spanIndex, int x, int y, int len )
        {
            this.interpolator().begin( ( double ) x + this.filter_dx_dbl(), ( double ) y + this.filter_dy_dbl(), len );
            ImageBuffer sourceImage = (ImageBuffer) this.GetImageBufferAccessor().SourceImage;
            ISpanInterpolator spanInterpolator = this.interpolator();
            int bufferOffset;
            byte[] buffer = sourceImage.GetBuffer(out bufferOffset);
            do
            {
                int x1;
                int y1;
                spanInterpolator.coordinates( out x1, out y1 );
                x1 -= this.filter_dx_int();
                y1 -= this.filter_dy_int();
                int x2 = x1 >> 8;
                int y2 = y1 >> 8;
                int num1;
                int num2 = num1 = 32768;
                int num3 = num1;
                int num4 = num1;
                x1 &= ( int ) byte.MaxValue;
                y1 &= ( int ) byte.MaxValue;
                int bufferOffsetXy1 = sourceImage.GetBufferOffsetXY(x2, y2);
                int num5 = (256 - x1) * (256 - y1);
                int num6 = num4 + num5 * (int) buffer[bufferOffsetXy1 + 2];
                int num7 = num3 + num5 * (int) buffer[bufferOffsetXy1 + 1];
                int num8 = num2 + num5 * (int) buffer[bufferOffsetXy1];
                int index1 = bufferOffsetXy1 + 3;
                int num9 = x1 * (256 - y1);
                int num10 = num6 + num9 * (int) buffer[index1 + 2];
                int num11 = num7 + num9 * (int) buffer[index1 + 1];
                int num12 = num8 + num9 * (int) buffer[index1];
                int y3 = y2 + 1;
                int bufferOffsetXy2 = sourceImage.GetBufferOffsetXY(x2, y3);
                int num13 = (256 - x1) * y1;
                int num14 = num10 + num13 * (int) buffer[bufferOffsetXy2 + 2];
                int num15 = num11 + num13 * (int) buffer[bufferOffsetXy2 + 1];
                int num16 = num12 + num13 * (int) buffer[bufferOffsetXy2];
                int index2 = bufferOffsetXy2 + 3;
                int num17 = x1 * y1;
                int num18 = num14 + num17 * (int) buffer[index2 + 2];
                int num19 = num15 + num17 * (int) buffer[index2 + 1];
                int num20 = num16 + num17 * (int) buffer[index2];
                int num21 = num18 >> 16;
                int num22 = num19 >> 16;
                int num23 = num20 >> 16;
                RGBA_Bytes rgbaBytes;
                rgbaBytes.red = ( byte ) num21;
                rgbaBytes.green = ( byte ) num22;
                rgbaBytes.blue = ( byte ) num23;
                rgbaBytes.alpha = byte.MaxValue;
                span[ spanIndex ] = rgbaBytes;
                ++spanIndex;
                spanInterpolator.Next();
            }
            while ( --len != 0 );
        }

        private void BlendInFilterPixel( int[ ] fg, ref int src_alpha, int back_r, int back_g, int back_b, int back_a, ImageBuffer SourceRenderingBuffer, int maxx, int maxy, int x_lr, int y_lr, int weight )
        {
            throw new NotImplementedException();
        }
    }
}
