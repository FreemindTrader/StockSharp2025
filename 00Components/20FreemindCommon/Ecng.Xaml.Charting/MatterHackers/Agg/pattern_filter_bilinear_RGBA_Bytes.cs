// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.pattern_filter_bilinear_RGBA_Bytes
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Runtime.InteropServices;
using MatterHackers.Agg.Image;

namespace MatterHackers.Agg
{
    [StructLayout( LayoutKind.Sequential, Size = 1 )]
    internal struct pattern_filter_bilinear_RGBA_Bytes : IPatternFilter
    {
        public int dilation()
        {
            return 1;
        }

        public void pixel_low_res( RGBA_Bytes[ ][ ] buf, RGBA_Bytes[ ] p, int offset, int x, int y )
        {
            p[ offset ] = buf[ y ][ x ];
        }

        public void pixel_high_res( ImageBuffer sourceImage, RGBA_Bytes[ ] destBuffer, int destBufferOffset, int x, int y )
        {
            int num1;
            int num2 = num1 = 32768;
            int num3 = num1;
            int num4 = num1;
            int num5 = num1;
            int x1 = x >> 8;
            int y1 = y >> 8;
            x &= ( int ) byte.MaxValue;
            y &= ( int ) byte.MaxValue;
            int bufferOffset1;
            byte[] pixelPointerXy1 = sourceImage.GetPixelPointerXY(x1, y1, out bufferOffset1);
            int num6 = (256 - x) * (256 - y);
            int num7 = num5 + num6 * (int) pixelPointerXy1[bufferOffset1 + 2];
            int num8 = num4 + num6 * (int) pixelPointerXy1[bufferOffset1 + 1];
            int num9 = num3 + num6 * (int) pixelPointerXy1[bufferOffset1];
            int num10 = num2 + num6 * (int) pixelPointerXy1[bufferOffset1 + 3];
            int bufferOffset2 = bufferOffset1 + sourceImage.GetBytesBetweenPixelsInclusive();
            int num11 = x * (256 - y);
            int num12 = num7 + num11 * (int) pixelPointerXy1[bufferOffset2 + 2];
            int num13 = num8 + num11 * (int) pixelPointerXy1[bufferOffset2 + 1];
            int num14 = num9 + num11 * (int) pixelPointerXy1[bufferOffset2];
            int num15 = num10 + num11 * (int) pixelPointerXy1[bufferOffset2 + 3];
            byte[] pixelPointerXy2 = sourceImage.GetPixelPointerXY(x1, y1 + 1, out bufferOffset2);
            int num16 = (256 - x) * y;
            int num17 = num12 + num16 * (int) pixelPointerXy2[bufferOffset2 + 2];
            int num18 = num13 + num16 * (int) pixelPointerXy2[bufferOffset2 + 1];
            int num19 = num14 + num16 * (int) pixelPointerXy2[bufferOffset2];
            int num20 = num15 + num16 * (int) pixelPointerXy2[bufferOffset2 + 3];
            int index = bufferOffset2 + sourceImage.GetBytesBetweenPixelsInclusive();
            int num21 = x * y;
            int num22 = num17 + num21 * (int) pixelPointerXy2[index + 2];
            int num23 = num18 + num21 * (int) pixelPointerXy2[index + 1];
            int num24 = num19 + num21 * (int) pixelPointerXy2[index];
            int num25 = num20 + num21 * (int) pixelPointerXy2[index + 3];
            destBuffer[ destBufferOffset ].red = ( byte ) ( num22 >> 16 );
            destBuffer[ destBufferOffset ].green = ( byte ) ( num23 >> 16 );
            destBuffer[ destBufferOffset ].blue = ( byte ) ( num24 >> 16 );
            destBuffer[ destBufferOffset ].alpha = ( byte ) ( num25 >> 16 );
        }
    }
}
