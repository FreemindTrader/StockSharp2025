// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Image.BlenderBGRAFloat
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg.Image
{
    internal sealed class BlenderBGRAFloat : BlenderBaseBGRAFloat, IBlenderFloat
    {
        public RGBA_Floats PixelToColorRGBA_Floats( float[ ] buffer, int bufferOffset )
        {
            return new RGBA_Floats( buffer[ bufferOffset + 2 ], buffer[ bufferOffset + 1 ], buffer[ bufferOffset ], buffer[ bufferOffset + 3 ] );
        }

        public void CopyPixels( float[ ] buffer, int bufferOffset, RGBA_Floats sourceColor, int count )
        {
            do
            {
                buffer[ bufferOffset + 2 ] = sourceColor.red;
                buffer[ bufferOffset + 1 ] = sourceColor.green;
                buffer[ bufferOffset ] = sourceColor.blue;
                buffer[ bufferOffset + 3 ] = sourceColor.alpha;
                bufferOffset += 4;
            }
            while ( --count != 0 );
        }

        public void BlendPixel( float[ ] buffer, int bufferOffset, RGBA_Floats sourceColor )
        {
            if ( ( double ) sourceColor.alpha == 1.0 )
            {
                buffer[ bufferOffset + 2 ] = ( float ) ( byte ) sourceColor.red;
                buffer[ bufferOffset + 1 ] = ( float ) ( byte ) sourceColor.green;
                buffer[ bufferOffset ] = ( float ) ( byte ) sourceColor.blue;
                buffer[ bufferOffset + 3 ] = ( float ) ( byte ) sourceColor.alpha;
            }
            else
            {
                float num1 = buffer[bufferOffset + 2];
                float num2 = buffer[bufferOffset + 1];
                float num3 = buffer[bufferOffset];
                float num4 = buffer[bufferOffset + 3];
                buffer[ bufferOffset + 2 ] = ( sourceColor.red - num1 ) * sourceColor.alpha + num1;
                buffer[ bufferOffset + 1 ] = ( sourceColor.green - num2 ) * sourceColor.alpha + num2;
                buffer[ bufferOffset ] = ( sourceColor.blue - num3 ) * sourceColor.alpha + num3;
                buffer[ bufferOffset + 3 ] = ( float ) ( ( double ) sourceColor.alpha + ( double ) num4 - ( double ) sourceColor.alpha * ( double ) num4 );
            }
        }

        public void BlendPixels( float[ ] destBuffer, int bufferOffset, RGBA_Floats[ ] sourceColors, int sourceColorsOffset, byte[ ] covers, int coversIndex, bool firstCoverForAll, int count )
        {
            throw new NotImplementedException();
        }
    }
}
