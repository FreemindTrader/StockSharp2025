// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.span_image_filter_rgba_nn
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using MatterHackers.Agg.Image;

namespace MatterHackers.Agg
{
    internal class span_image_filter_rgba_nn : span_image_filter
    {
        private const int baseShift = 8;
        private const int baseScale = 256;
        private const int baseMask = 255;

        public span_image_filter_rgba_nn( IImageBufferAccessor sourceAccessor, ISpanInterpolator spanInterpolator )
          : base( sourceAccessor, spanInterpolator, ( ImageFilterLookUpTable ) null )
        {
        }

        public override void generate( RGBA_Bytes[ ] span, int spanIndex, int x, int y, int len )
        {
            ImageBuffer sourceImage = (ImageBuffer) this.GetImageBufferAccessor().SourceImage;
            if ( sourceImage.BitDepth != 32 )
            {
                throw new NotSupportedException( "The source is expected to be 32 bit." );
            }

            ISpanInterpolator spanInterpolator = this.interpolator();
            spanInterpolator.begin( ( double ) x + this.filter_dx_dbl(), ( double ) y + this.filter_dy_dbl(), len );
            byte[] buffer = sourceImage.GetBuffer();
            do
            {
                int x1;
                int y1;
                spanInterpolator.coordinates( out x1, out y1 );
                int x2 = x1 >> 8;
                int y2 = y1 >> 8;
                int bufferOffsetXy = sourceImage.GetBufferOffsetXY(x2, y2);
                RGBA_Bytes rgbaBytes;

                //ref RGBA_Bytes local1 = ref rgbaBytes;
                byte[] numArray1 = buffer;
                int index1 = bufferOffsetXy;
                int num1 = index1 + 1;
                int num2 = (int) numArray1[index1];
                rgbaBytes.blue = ( byte ) num2;

                //ref RGBA_Bytes local2 = ref rgbaBytes;
                byte[] numArray2 = buffer;
                int index2 = num1;
                int num3 = index2 + 1;
                int num4 = (int) numArray2[index2];
                rgbaBytes.green = ( byte ) num4;

                // ref RGBA_Bytes local3 = ref rgbaBytes;
                byte[] numArray3 = buffer;
                int index3 = num3;
                int num5 = index3 + 1;
                int num6 = (int) numArray3[index3];
                rgbaBytes.red = ( byte ) num6;

                //ref RGBA_Bytes local4 = ref rgbaBytes;
                byte[] numArray4 = buffer;
                int index4 = num5;
                int num7 = index4 + 1;
                int num8 = (int) numArray4[index4];
                rgbaBytes.alpha = ( byte ) num8;
                span[ spanIndex ] = rgbaBytes;
                ++spanIndex;
                spanInterpolator.Next();
            }
            while ( --len != 0 );
        }
    }
}
