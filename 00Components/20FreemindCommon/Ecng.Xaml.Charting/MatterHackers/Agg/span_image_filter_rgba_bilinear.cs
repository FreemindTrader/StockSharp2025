// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.span_image_filter_rgba_bilinear
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using MatterHackers.Agg.Image;

namespace MatterHackers.Agg
{
    internal class span_image_filter_rgba_bilinear : span_image_filter
    {
        private const int base_shift = 8;
        private const int base_scale = 256;
        private const int base_mask = 255;

        public span_image_filter_rgba_bilinear( IImageBufferAccessor src, ISpanInterpolator inter )
          : base( src, inter, ( ImageFilterLookUpTable ) null )
        {
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
                int x2              = x1 >> 8;
                int y2              = y1 >> 8;
                int num1;
                int num2            = num1 = 32768;
                int num3            = num1;
                int num4            = num1;
                x1 &= ( int ) byte.MaxValue;
                y1 &= ( int ) byte.MaxValue;
                int bufferOffsetXy1 = sourceImage.GetBufferOffsetXY(x2, y2);
                int num5            = (256 - x1) * (256 - y1);
                int num6            = num4 + num5 * (int) buffer[bufferOffsetXy1 + 2];
                int num7            = num3 + num5 * (int) buffer[bufferOffsetXy1 + 1];
                int num8            = num2 + num5 * (int) buffer[bufferOffsetXy1];
                int num9            = num1 + num5 * (int) buffer[bufferOffsetXy1 + 3];
                int index1          = bufferOffsetXy1 + 4;
                int num10           = x1 * (256 - y1);
                int num11           = num6 + num10 * (int) buffer[index1 + 2];
                int num12           = num7 + num10 * (int) buffer[index1 + 1];
                int num13           = num8 + num10 * (int) buffer[index1];
                int num14           = num10 * (int) buffer[index1 + 3];
                int num15           = num9 + num14;
                int y3              = y2 + 1;
                int bufferOffsetXy2 = sourceImage.GetBufferOffsetXY(x2, y3);
                int num16           = (256 - x1) * y1;
                int num17           = num11 + num16 * (int) buffer[bufferOffsetXy2 + 2];
                int num18           = num12 + num16 * (int) buffer[bufferOffsetXy2 + 1];
                int num19           = num13 + num16 * (int) buffer[bufferOffsetXy2];
                int num20           = num16 * (int) buffer[bufferOffsetXy2 + 3];
                int num21           = num15 + num20;
                int index2          = bufferOffsetXy2 + 4;
                int num22           = x1 * y1;
                int num23           = num17 + num22 * (int) buffer[index2 + 2];
                int num24           = num18 + num22 * (int) buffer[index2 + 1];
                int num25           = num19 + num22 * (int) buffer[index2];
                int num26           = num22 * (int) buffer[index2 + 3];
                int num27           = num21 + num26;
                int num28           = num23 >> 16;
                int num29           = num24 >> 16;
                int num30           = num25 >> 16;

                RGBA_Bytes rgbaBytes;

                rgbaBytes.red = ( byte ) num28;
                rgbaBytes.green = ( byte ) num29;
                rgbaBytes.blue = ( byte ) num30;
                rgbaBytes.alpha = byte.MaxValue;
                span[ spanIndex ] = rgbaBytes;

                ++spanIndex;
                spanInterpolator.Next();
            }
            while ( --len != 0 );
        }
    }
}
