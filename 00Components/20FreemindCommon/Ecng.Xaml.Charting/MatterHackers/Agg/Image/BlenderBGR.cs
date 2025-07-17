// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Image.BlenderBGR
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace MatterHackers.Agg.Image
{
    internal sealed class BlenderBGR : BlenderBaseBGR, IBlenderByte
    {
        public RGBA_Bytes PixelToColorRGBA_Bytes( byte[ ] buffer, int bufferOffset )
        {
            return new RGBA_Bytes( ( int ) buffer[ bufferOffset + 2 ], ( int ) buffer[ bufferOffset + 1 ], ( int ) buffer[ bufferOffset ], ( int ) byte.MaxValue );
        }

        public void CopyPixels( byte[ ] buffer, int bufferOffset, RGBA_Bytes sourceColor, int count )
        {
            do
            {
                buffer[ bufferOffset + 2 ] = sourceColor.red;
                buffer[ bufferOffset + 1 ] = sourceColor.green;
                buffer[ bufferOffset ] = sourceColor.blue;
                bufferOffset += 3;
            }
            while ( --count != 0 );
        }

        public void BlendPixel( byte[ ] buffer, int bufferOffset, RGBA_Bytes sourceColor )
        {
            int num1 = (int) buffer[bufferOffset + 2];
            int num2 = (int) buffer[bufferOffset + 1];
            int num3 = (int) buffer[bufferOffset];
            buffer[ bufferOffset + 2 ] = ( byte ) ( ( ( int ) sourceColor.red - num1 ) * ( int ) sourceColor.alpha + ( num1 << 8 ) >> 8 );
            buffer[ bufferOffset + 1 ] = ( byte ) ( ( ( int ) sourceColor.green - num2 ) * ( int ) sourceColor.alpha + ( num2 << 8 ) >> 8 );
            buffer[ bufferOffset ] = ( byte ) ( ( ( int ) sourceColor.blue - num3 ) * ( int ) sourceColor.alpha + ( num3 << 8 ) >> 8 );
        }

        public void BlendPixels( byte[ ] destBuffer, int bufferOffset, RGBA_Bytes[ ] sourceColors, int sourceColorsOffset, byte[ ] covers, int coversIndex, bool firstCoverForAll, int count )
        {
            if ( firstCoverForAll )
            {
                int cover = (int) covers[coversIndex];
                if ( cover == ( int ) byte.MaxValue )
                {
                    do
                    {
                        this.BlendPixel( destBuffer, bufferOffset, sourceColors[ sourceColorsOffset++ ] );
                        bufferOffset += 3;
                    }
                    while ( --count != 0 );
                }
                else
                {
                    do
                    {
                        sourceColors[ sourceColorsOffset ].alpha = ( byte ) ( ( int ) sourceColors[ sourceColorsOffset ].alpha * cover + ( int ) byte.MaxValue >> 8 );
                        this.BlendPixel( destBuffer, bufferOffset, sourceColors[ sourceColorsOffset ] );
                        bufferOffset += 3;
                        ++sourceColorsOffset;
                    }
                    while ( --count != 0 );
                }
            }
            else
            {
                do
                {
                    int cover = (int) covers[coversIndex++];
                    if ( cover == ( int ) byte.MaxValue )
                    {
                        this.BlendPixel( destBuffer, bufferOffset, sourceColors[ sourceColorsOffset ] );
                    }
                    else
                    {
                        RGBA_Bytes sourceColor = sourceColors[sourceColorsOffset];
                        sourceColor.alpha = ( byte ) ( ( int ) sourceColor.alpha * cover + ( int ) byte.MaxValue >> 8 );
                        this.BlendPixel( destBuffer, bufferOffset, sourceColor );
                    }
                    bufferOffset += 3;
                    ++sourceColorsOffset;
                }
                while ( --count != 0 );
            }
        }
    }
}
