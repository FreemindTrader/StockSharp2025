// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.span_image_filter_rgba_float
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\StockSharp.Xaml.Charting.dll

using System;
using MatterHackers.VectorMath;

namespace MatterHackers.Agg
{
    internal class span_image_filter_rgba_float : span_image_filter_float
    {
        public span_image_filter_rgba_float( IImageBufferAccessorFloat src, ISpanInterpolatorFloat inter, IImageFilterFunction filterFunction )
          : base( src, inter, filterFunction )
        {
            if ( src.SourceImage.GetFloatsBetweenPixelsInclusive() != 4 )
                throw new NotSupportedException( "span_image_filter_rgba must have a 32 bit DestImage" );
        }

        public override void generate( RGBA_Floats[ ] span, int spanIndex, int xInt, int yInt, int len )
        {
            this.interpolator().begin( ( double ) xInt + ( double ) this.filter_dx_dbl(), ( double ) yInt + ( double ) this.filter_dy_dbl(), len );
            int len1 = (int) this.m_filterFunction.radius() * 2;
            int num1 = -(len1 / 2 - 1);
            ISpanInterpolatorFloat interpolatorFloat = this.interpolator();
            IImageBufferAccessorFloat bufferAccessorFloat = this.source();
            do
            {
                float x = (float) xInt;
                float y = (float) yInt;
                interpolatorFloat.coordinates( out x, out y );
                int num2 = (int) x;
                int num3 = (int) y;
                Vector2 vector2_1 = new Vector2((double) x, (double) y);
                Vector2 vector2_2 = new Vector2((double) (num2 + num1), (double) (num3 + num1));
                double num4;
                float num5 = (float) (num4 = 0.0);
                float num6 = (float) num4;
                float num7 = (float) num4;
                float num8 = (float) num4;
                int num9 = len1;
                int index;
                float[] numArray = bufferAccessorFloat.span(num2 + num1, num3 + num1, len1, out index);
                float num10 = 0.0f;
                while ( true )
                {
                    float num11 = (float) this.m_filterFunction.calc_weight(Math.Sqrt((vector2_2.y - vector2_1.y) * (vector2_2.y - vector2_1.y)));
                    int num12 = len1;
                    while ( true )
                    {
                        float num13 = (float) this.m_filterFunction.calc_weight(Math.Sqrt((vector2_2.x - vector2_1.x) * (vector2_2.x - vector2_1.x))) * num11;
                        num6 += num13 * numArray[ index + 2 ];
                        num7 += num13 * numArray[ index + 1 ];
                        num8 += num13 * numArray[ index ];
                        num5 += num13 * numArray[ index + 3 ];
                        num10 += num13;
                        ++vector2_2.x;
                        if ( --num12 != 0 )
                            bufferAccessorFloat.next_x( out index );
                        else
                            break;
                    }
                    vector2_2.x -= ( double ) len1;
                    if ( --num9 != 0 )
                    {
                        ++vector2_2.y;
                        numArray = bufferAccessorFloat.next_y( out index );
                    }
                    else
                        break;
                }
                if ( ( double ) num8 < 0.0 )
                    num8 = 0.0f;
                if ( ( double ) num8 > 1.0 )
                    num8 = 1f;
                if ( ( double ) num6 < 0.0 )
                    num6 = 0.0f;
                if ( ( double ) num6 > 1.0 )
                    num6 = 1f;
                if ( ( double ) num7 < 0.0 )
                    num7 = 0.0f;
                if ( ( double ) num7 > 1.0 )
                    num7 = 1f;
                span[ spanIndex ].red = num6;
                span[ spanIndex ].green = num7;
                span[ spanIndex ].blue = num8;
                span[ spanIndex ].alpha = 1f;
                ++spanIndex;
                interpolatorFloat.Next();
            }
            while ( --len != 0 );
        }
    }
}
