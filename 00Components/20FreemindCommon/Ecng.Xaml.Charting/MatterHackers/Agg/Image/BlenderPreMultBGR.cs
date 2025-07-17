// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Image.BlenderPreMultBGR
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg.Image
{
    internal sealed class BlenderPreMultBGR : BlenderBaseBGR, IBlenderByte
    {
        private static int[] m_Saturate9BitToByte = new int[512];

        public BlenderPreMultBGR()
        {
            if ( BlenderPreMultBGR.m_Saturate9BitToByte[ 2 ] != 0 )
                return;
            for ( int val1 = 0 ; val1 < BlenderPreMultBGR.m_Saturate9BitToByte.Length ; ++val1 )
                BlenderPreMultBGR.m_Saturate9BitToByte[ val1 ] = Math.Min( val1, ( int ) byte.MaxValue );
        }

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

        public void BlendPixel( byte[ ] pDestBuffer, int bufferOffset, RGBA_Bytes sourceColor )
        {
            if ( sourceColor.alpha == byte.MaxValue )
            {
                pDestBuffer[ bufferOffset + 2 ] = sourceColor.red;
                pDestBuffer[ bufferOffset + 1 ] = sourceColor.green;
                pDestBuffer[ bufferOffset ] = sourceColor.blue;
            }
            else
            {
                int num1 = (int) byte.MaxValue - (int) sourceColor.alpha;
                int num2 = BlenderPreMultBGR.m_Saturate9BitToByte[((int) pDestBuffer[bufferOffset + 2] * num1 + (int) byte.MaxValue >> 8) + (int) sourceColor.red];
                int num3 = BlenderPreMultBGR.m_Saturate9BitToByte[((int) pDestBuffer[bufferOffset + 1] * num1 + (int) byte.MaxValue >> 8) + (int) sourceColor.green];
                int num4 = BlenderPreMultBGR.m_Saturate9BitToByte[((int) pDestBuffer[bufferOffset] * num1 + (int) byte.MaxValue >> 8) + (int) sourceColor.blue];
                pDestBuffer[ bufferOffset + 2 ] = ( byte ) num2;
                pDestBuffer[ bufferOffset + 1 ] = ( byte ) num3;
                pDestBuffer[ bufferOffset ] = ( byte ) num4;
            }
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
