// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.span_image_filter_gray_nn_stepXby1
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using MatterHackers.Agg.Image;

namespace MatterHackers.Agg
{
    internal class span_image_filter_gray_nn_stepXby1 : span_image_filter
    {
        private const int base_shift = 8;
        private const int base_scale = 256;
        private const int base_mask = 255;

        public span_image_filter_gray_nn_stepXby1( IImageBufferAccessor sourceAccessor, ISpanInterpolator spanInterpolator )
          : base( sourceAccessor, spanInterpolator, ( ImageFilterLookUpTable ) null )
        {
        }

        public override unsafe void generate( RGBA_Bytes[ ] span, int spanIndex, int x, int y, int len )
        {
            ImageBuffer imageBuffer = (ImageBuffer)GetImageBufferAccessor().SourceImage;
            int bytesBetweenPixelsInclusive = imageBuffer.GetBytesBetweenPixelsInclusive();
            if ( imageBuffer.BitDepth != 8 )
            {
                throw new NotSupportedException( "The source is expected to be 32 bit." );
            }
            ISpanInterpolator spanInterpolator = interpolator();
            spanInterpolator.begin( ( double ) x + filter_dx_dbl(), ( double ) y + filter_dy_dbl(), len );
            spanInterpolator.coordinates( out int x2, out int y2 );
            int x3 = x2 >> 8;
            int y3 = y2 >> 8;
            int num = imageBuffer.GetBufferOffsetXY(x3, y3);
            byte[] buffer = imageBuffer.GetBuffer();
            fixed ( byte* ptr = buffer )
            {
                do
                {
                    span[ spanIndex ].red = ptr[ num ];
                    span[ spanIndex ].green = ptr[ num ];
                    span[ spanIndex ].blue = ptr[ num ];
                    span[ spanIndex ].alpha = byte.MaxValue;
                    spanIndex++;
                    num += bytesBetweenPixelsInclusive;
                }
                while ( --len != 0 );
            }
        }
    }
}
