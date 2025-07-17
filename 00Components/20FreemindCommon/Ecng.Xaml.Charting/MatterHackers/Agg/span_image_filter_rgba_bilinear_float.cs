// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.span_image_filter_rgba_bilinear_float
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using MatterHackers.Agg.Image;

namespace MatterHackers.Agg
{
    internal class span_image_filter_rgba_bilinear_float : span_image_filter_float
    {
        public span_image_filter_rgba_bilinear_float( IImageBufferAccessorFloat src, ISpanInterpolatorFloat inter )
          : base( src, inter, ( IImageFilterFunction ) null )
        {
        }

        public override void generate( RGBA_Floats[ ] span, int spanIndex, int x, int y, int len )
        {
            this.interpolator().begin( ( double ) x + ( double ) this.filter_dx_dbl(), ( double ) y + ( double ) this.filter_dy_dbl(), len );
            ImageBufferFloat sourceImage = (ImageBufferFloat) this.source().SourceImage;
            ISpanInterpolatorFloat interpolatorFloat = this.interpolator();
            int bufferOffset;
            float[] buffer = sourceImage.GetBuffer(out bufferOffset);
            do
            {
                float x1;
                float y1;
                interpolatorFloat.coordinates( out x1, out y1 );
                x1 -= this.filter_dx_dbl();
                y1 -= this.filter_dy_dbl();
                int x2 = (int) x1;
                int y2 = (int) y1;
                double num1;
                float num2 = (float) (num1 = 0.0);
                float num3 = (float) num1;
                float num4 = (float) num1;
                float num5 = (float) num1;
                x1 -= ( float ) x2;
                y1 -= ( float ) y2;
                int bufferOffsetXy1 = sourceImage.GetBufferOffsetXY(x2, y2);
                float num6 = (float) ((1.0 - (double) x1) * (1.0 - (double) y1));
                float num7 = num5 + num6 * buffer[bufferOffsetXy1 + 2];
                float num8 = num4 + num6 * buffer[bufferOffsetXy1 + 1];
                float num9 = num3 + num6 * buffer[bufferOffsetXy1];
                float num10 = num2 + num6 * buffer[bufferOffsetXy1 + 3];
                int index1 = bufferOffsetXy1 + 4;
                float num11 = x1 * (1f - y1);
                float num12 = num7 + num11 * buffer[index1 + 2];
                float num13 = num8 + num11 * buffer[index1 + 1];
                float num14 = num9 + num11 * buffer[index1];
                float num15 = num10 + num11 * buffer[index1 + 3];
                int y3 = y2 + 1;
                int bufferOffsetXy2 = sourceImage.GetBufferOffsetXY(x2, y3);
                float num16 = (1f - x1) * y1;
                float num17 = num12 + num16 * buffer[bufferOffsetXy2 + 2];
                float num18 = num13 + num16 * buffer[bufferOffsetXy2 + 1];
                float num19 = num14 + num16 * buffer[bufferOffsetXy2];
                float num20 = num15 + num16 * buffer[bufferOffsetXy2 + 3];
                int index2 = bufferOffsetXy2 + 4;
                float num21 = x1 * y1;
                float num22 = num17 + num21 * buffer[index2 + 2];
                float num23 = num18 + num21 * buffer[index2 + 1];
                float num24 = num19 + num21 * buffer[index2];
                float num25 = num20 + num21 * buffer[index2 + 3];
                RGBA_Floats rgbaFloats;
                rgbaFloats.red = num22;
                rgbaFloats.green = num23;
                rgbaFloats.blue = num24;
                rgbaFloats.alpha = num25;
                span[ spanIndex ] = rgbaFloats;
                ++spanIndex;
                interpolatorFloat.Next();
            }
            while ( --len != 0 );
        }
    }
}
