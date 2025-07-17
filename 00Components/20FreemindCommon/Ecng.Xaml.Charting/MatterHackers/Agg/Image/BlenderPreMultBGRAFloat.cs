// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Image.BlenderPreMultBGRAFloat
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg.Image
{
    internal sealed class BlenderPreMultBGRAFloat : BlenderBaseBGRAFloat, IBlenderFloat
    {
        public RGBA_Floats PixelToColorRGBA_Floats( float[ ] buffer, int bufferOffset )
        {
            throw new NotImplementedException();
        }

        public void SetPixels( float[ ] buffer, int bufferOffset, RGBA_Floats sourceColor, int count )
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

        public void CopyPixels( float[ ] buffer, int bufferOffset, RGBA_Floats[ ] sourceColors, int sourceColorsOffset, int count )
        {
            throw new NotImplementedException();
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

        public void BlendPixels( float[ ] pDestBuffer, int bufferOffset, RGBA_Floats[ ] sourceColors, int sourceColorsOffset, int count )
        {
        }

        public void BlendPixels( float[ ] pDestBuffer, int bufferOffset, RGBA_Floats[ ] sourceColors, int sourceColorsOffset, byte sourceCovers, int count )
        {
        }

        public void BlendPixels( float[ ] pDestBuffer, int bufferOffset, RGBA_Floats[ ] sourceColors, int sourceColorsOffset, byte[ ] sourceCovers, int sourceCoversOffset, int count )
        {
        }

        public void BlendPixels( float[ ] pDestBuffer, int bufferOffset, RGBA_Floats[ ] sourceColors, int sourceColorsOffset, byte[ ] sourceCovers, int sourceCoversOffset, bool firstCoverForAll, int count )
        {
            if ( firstCoverForAll )
            {
                if ( sourceCovers[ sourceCoversOffset ] == byte.MaxValue )
                {
                    for ( int index = 0 ; index < count ; ++index )
                    {
                        this.BlendPixel( pDestBuffer, bufferOffset, sourceColors[ sourceColorsOffset ] );
                        ++sourceColorsOffset;
                        bufferOffset += 4;
                    }
                }
                else
                {
                    for ( int index = 0 ; index < count ; ++index )
                    {
                        RGBA_Floats sourceColor = sourceColors[sourceColorsOffset];
                        float num1 = (float) (((double) sourceColor.alpha * (double) sourceCovers[sourceCoversOffset] + (double) byte.MaxValue) / 256.0);
                        if ( ( double ) num1 != 0.0 )
                        {
                            if ( ( double ) num1 == ( double ) byte.MaxValue )
                            {
                                pDestBuffer[ bufferOffset + 2 ] = ( float ) ( byte ) sourceColor.red;
                                pDestBuffer[ bufferOffset + 1 ] = ( float ) ( byte ) sourceColor.green;
                                pDestBuffer[ bufferOffset ] = ( float ) ( byte ) sourceColor.blue;
                                pDestBuffer[ bufferOffset + 3 ] = ( float ) ( byte ) num1;
                            }
                            else
                            {
                                float num2 = (float) byte.MaxValue - num1;
                                float num3 = pDestBuffer[bufferOffset + 2] * num2 + sourceColor.red;
                                float num4 = pDestBuffer[bufferOffset + 1] * num2 + sourceColor.green;
                                float num5 = pDestBuffer[bufferOffset] * num2 + sourceColor.blue;
                                float num6 = pDestBuffer[bufferOffset + 3];
                                pDestBuffer[ bufferOffset + 2 ] = num3;
                                pDestBuffer[ bufferOffset + 1 ] = num4;
                                pDestBuffer[ bufferOffset ] = num5;
                                pDestBuffer[ bufferOffset + 3 ] = ( float ) ( 1.0 - ( double ) num2 * ( 1.0 - ( double ) num6 ) );
                            }
                            ++sourceColorsOffset;
                            bufferOffset += 4;
                        }
                    }
                }
            }
            else
            {
                for ( int index = 0 ; index < count ; ++index )
                {
                    RGBA_Floats sourceColor = sourceColors[sourceColorsOffset];
                    if ( ( double ) sourceColor.alpha == 1.0 && sourceCovers[ sourceCoversOffset ] == byte.MaxValue )
                    {
                        pDestBuffer[ bufferOffset + 2 ] = sourceColor.red;
                        pDestBuffer[ bufferOffset + 1 ] = sourceColor.green;
                        pDestBuffer[ bufferOffset ] = sourceColor.blue;
                        pDestBuffer[ bufferOffset + 3 ] = 1f;
                    }
                    else
                    {
                        float num1 = (float) sourceCovers[sourceCoversOffset] * 0.003921569f;
                        float num2 = sourceColor.alpha * num1;
                        if ( ( double ) num1 > 0.0 && ( double ) num2 > 0.0 )
                        {
                            float num3 = 1f - num2;
                            float num4 = (float) ((double) pDestBuffer[bufferOffset + 2] * (double) num3 + (double) sourceColor.red * (double) num1);
                            float num5 = (float) ((double) pDestBuffer[bufferOffset + 1] * (double) num3 + (double) sourceColor.green * (double) num1);
                            float num6 = (float) ((double) pDestBuffer[bufferOffset] * (double) num3 + (double) sourceColor.blue * (double) num1);
                            float num7 = pDestBuffer[bufferOffset + 3];
                            float num8 = num7 + (1f - num7) * sourceColor.alpha * num1;
                            pDestBuffer[ bufferOffset + 2 ] = num4;
                            pDestBuffer[ bufferOffset + 1 ] = num5;
                            pDestBuffer[ bufferOffset ] = num6;
                            pDestBuffer[ bufferOffset + 3 ] = num8;
                        }
                    }
                    ++sourceColorsOffset;
                    ++sourceCoversOffset;
                    bufferOffset += 4;
                }
            }
        }
    }
}
