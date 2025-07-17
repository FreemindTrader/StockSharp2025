// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.span_image_filter_rgb_nn
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using MatterHackers.Agg.Image;

namespace MatterHackers.Agg
{
    internal class span_image_filter_rgb_nn : span_image_filter
    {
        private const int base_shift = 8;
        private const int base_scale = 256;
        private const int base_mask = 255;

        public span_image_filter_rgb_nn( IImageBufferAccessor src, ISpanInterpolator inter )
          : base( src, inter, ( ImageFilterLookUpTable ) null )
        {
        }

        public override void generate( RGBA_Bytes[ ] span, int spanIndex, int x, int y, int len )
        {
            ImageBuffer imageBuffer = (ImageBuffer)GetImageBufferAccessor().SourceImage;
            if ( imageBuffer.BitDepth != 24 )
            {
                throw new NotSupportedException( "The source is expected to be 32 bit." );
            }
            ISpanInterpolator spanInterpolator = interpolator();
            spanInterpolator.begin( ( double ) x + filter_dx_dbl(), ( double ) y + filter_dy_dbl(), len );
            int bufferOffset;
            byte[] buffer = imageBuffer.GetBuffer(out bufferOffset);
            do
            {
                spanInterpolator.coordinates( out int x2, out int y2 );
                int x3 = x2 >> 8;
                int y3 = y2 >> 8;
                int num = imageBuffer.GetBufferOffsetXY(x3, y3);
                RGBA_Bytes rGBA_Bytes = default(RGBA_Bytes);
                rGBA_Bytes.blue = buffer[ num++ ];
                rGBA_Bytes.green = buffer[ num++ ];
                rGBA_Bytes.red = buffer[ num++ ];
                rGBA_Bytes.alpha = byte.MaxValue;
                span[ spanIndex ] = rGBA_Bytes;
                spanIndex++;
                spanInterpolator.Next();
            }
            while ( --len != 0 );
        }
    }
}
