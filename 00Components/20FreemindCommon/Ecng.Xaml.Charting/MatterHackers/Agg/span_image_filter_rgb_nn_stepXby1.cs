// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.span_image_filter_rgb_nn_stepXby1
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using MatterHackers.Agg.Image;

namespace MatterHackers.Agg
{
    internal class span_image_filter_rgb_nn_stepXby1 : span_image_filter
    {
        private const int base_shift = 8;
        private const int base_scale = 256;
        private const int base_mask = 255;

        public span_image_filter_rgb_nn_stepXby1( IImageBufferAccessor sourceAccessor, ISpanInterpolator spanInterpolator )
          : base( sourceAccessor, spanInterpolator, ( ImageFilterLookUpTable ) null )
        {
        }

        public override void generate( RGBA_Bytes[ ] span, int spanIndex, int x, int y, int len )
        {
            ImageBuffer sourceImage = (ImageBuffer) this.GetImageBufferAccessor().SourceImage;
            if ( sourceImage.BitDepth != 24 )
                throw new NotSupportedException( "The source is expected to be 32 bit." );
            ISpanInterpolator spanInterpolator = this.interpolator();
            spanInterpolator.begin( ( double ) x + this.filter_dx_dbl(), ( double ) y + this.filter_dy_dbl(), len );
            int x1;
            int y1;
            spanInterpolator.coordinates( out x1, out y1 );
            int x2 = x1 >> 8;
            int y2 = y1 >> 8;
            int num1 = sourceImage.GetBufferOffsetXY(x2, y2);
            byte[] buffer = sourceImage.GetBuffer();
            RGBA_Bytes white = RGBA_Bytes.White;
            do
            {
                ref RGBA_Bytes local1 = ref white;
                byte[] numArray1 = buffer;
                int index1 = num1;
                int num2 = index1 + 1;
                int num3 = (int) numArray1[index1];
                local1.blue = ( byte ) num3;
                ref RGBA_Bytes local2 = ref white;
                byte[] numArray2 = buffer;
                int index2 = num2;
                int num4 = index2 + 1;
                int num5 = (int) numArray2[index2];
                local2.green = ( byte ) num5;
                ref RGBA_Bytes local3 = ref white;
                byte[] numArray3 = buffer;
                int index3 = num4;
                num1 = index3 + 1;
                int num6 = (int) numArray3[index3];
                local3.red = ( byte ) num6;
                span[ spanIndex++ ] = white;
            }
            while ( --len != 0 );
        }
    }
}
