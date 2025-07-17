// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Image.BlenderPreMultBGRA
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg.Image
{
    internal sealed class BlenderPreMultBGRA : BlenderBaseBGRA, IBlenderByte
    {
        private static int[] m_Saturate9BitToByte = new int[512];

        public BlenderPreMultBGRA()
        {
            if ( BlenderPreMultBGRA.m_Saturate9BitToByte[ 2 ] != 0 )
                return;
            for ( int val1 = 0 ; val1 < BlenderPreMultBGRA.m_Saturate9BitToByte.Length ; ++val1 )
                BlenderPreMultBGRA.m_Saturate9BitToByte[ val1 ] = Math.Min( val1, ( int ) byte.MaxValue );
        }

        public RGBA_Bytes PixelToColorRGBA_Bytes( byte[ ] buffer, int bufferOffset )
        {
            return new RGBA_Bytes( ( int ) buffer[ bufferOffset + 2 ], ( int ) buffer[ bufferOffset + 1 ], ( int ) buffer[ bufferOffset ], ( int ) buffer[ bufferOffset + 3 ] );
        }

        public void CopyPixels( byte[ ] buffer, int bufferOffset, RGBA_Bytes sourceColor, int count )
        {
            for ( int index = 0 ; index < count ; ++index )
            {
                buffer[ bufferOffset + 2 ] = sourceColor.red;
                buffer[ bufferOffset + 1 ] = sourceColor.green;
                buffer[ bufferOffset ] = sourceColor.blue;
                buffer[ bufferOffset + 3 ] = sourceColor.alpha;
                bufferOffset += 4;
            }
        }

        public void BlendPixel( byte[ ] pDestBuffer, int bufferOffset, RGBA_Bytes sourceColor )
        {
            int num1 = (int) byte.MaxValue - (int) sourceColor.alpha;
            int num2 = BlenderPreMultBGRA.m_Saturate9BitToByte[((int) pDestBuffer[bufferOffset + 2] * num1 + (int) byte.MaxValue >> 8) + (int) sourceColor.red];
            int num3 = BlenderPreMultBGRA.m_Saturate9BitToByte[((int) pDestBuffer[bufferOffset + 1] * num1 + (int) byte.MaxValue >> 8) + (int) sourceColor.green];
            int num4 = BlenderPreMultBGRA.m_Saturate9BitToByte[((int) pDestBuffer[bufferOffset] * num1 + (int) byte.MaxValue >> 8) + (int) sourceColor.blue];
            int num5 = (int) pDestBuffer[bufferOffset + 3];
            pDestBuffer[ bufferOffset + 2 ] = ( byte ) num2;
            pDestBuffer[ bufferOffset + 1 ] = ( byte ) num3;
            pDestBuffer[ bufferOffset ] = ( byte ) num4;
            pDestBuffer[ bufferOffset + 3 ] = ( byte ) ( ( int ) byte.MaxValue - BlenderPreMultBGRA.m_Saturate9BitToByte[ num1 * ( ( int ) byte.MaxValue - num5 ) + ( int ) byte.MaxValue >> 8 ] );
        }

        public void BlendPixels( byte[ ] pDestBuffer, int bufferOffset, RGBA_Bytes[ ] sourceColors, int sourceColorsOffset, byte[ ] sourceCovers, int sourceCoversOffset, bool firstCoverForAll, int count )
        {
            if ( firstCoverForAll )
            {
                if ( sourceCovers[ sourceCoversOffset ] == byte.MaxValue )
                {
                    for ( int index = 0 ; index < count ; ++index )
                    {
                        RGBA_Bytes sourceColor = sourceColors[sourceColorsOffset];
                        if ( sourceColor.alpha == byte.MaxValue )
                        {
                            pDestBuffer[ bufferOffset + 2 ] = sourceColor.red;
                            pDestBuffer[ bufferOffset + 1 ] = sourceColor.green;
                            pDestBuffer[ bufferOffset ] = sourceColor.blue;
                            pDestBuffer[ bufferOffset + 3 ] = byte.MaxValue;
                        }
                        else
                        {
                            int num1 = (int) byte.MaxValue - (int) sourceColor.alpha;
                            int num2 = BlenderPreMultBGRA.m_Saturate9BitToByte[((int) pDestBuffer[bufferOffset + 2] * num1 + (int) byte.MaxValue >> 8) + (int) sourceColor.red];
                            int num3 = BlenderPreMultBGRA.m_Saturate9BitToByte[((int) pDestBuffer[bufferOffset + 1] * num1 + (int) byte.MaxValue >> 8) + (int) sourceColor.green];
                            int num4 = BlenderPreMultBGRA.m_Saturate9BitToByte[((int) pDestBuffer[bufferOffset] * num1 + (int) byte.MaxValue >> 8) + (int) sourceColor.blue];
                            int num5 = (int) pDestBuffer[bufferOffset + 3];
                            pDestBuffer[ bufferOffset + 2 ] = ( byte ) num2;
                            pDestBuffer[ bufferOffset + 1 ] = ( byte ) num3;
                            pDestBuffer[ bufferOffset ] = ( byte ) num4;
                            pDestBuffer[ bufferOffset + 3 ] = ( byte ) ( ( int ) byte.MaxValue - BlenderPreMultBGRA.m_Saturate9BitToByte[ num1 * ( ( int ) byte.MaxValue - num5 ) + ( int ) byte.MaxValue >> 8 ] );
                        }
                        ++sourceColorsOffset;
                        bufferOffset += 4;
                    }
                }
                else
                {
                    for ( int index = 0 ; index < count ; ++index )
                    {
                        RGBA_Bytes sourceColor = sourceColors[sourceColorsOffset];
                        int num1 = ((int) sourceColor.alpha * (int) sourceCovers[sourceCoversOffset] + (int) byte.MaxValue) / 256;
                        switch ( num1 )
                        {
                            case 0:
                                continue;
                            case ( int ) byte.MaxValue:
                                pDestBuffer[ bufferOffset + 2 ] = sourceColor.red;
                                pDestBuffer[ bufferOffset + 1 ] = sourceColor.green;
                                pDestBuffer[ bufferOffset ] = sourceColor.blue;
                                pDestBuffer[ bufferOffset + 3 ] = ( byte ) num1;
                                break;
                            default:
                                int num2 = (int) byte.MaxValue - num1;
                                int num3 = BlenderPreMultBGRA.m_Saturate9BitToByte[((int) pDestBuffer[bufferOffset + 2] * num2 + (int) byte.MaxValue >> 8) + (int) sourceColor.red];
                                int num4 = BlenderPreMultBGRA.m_Saturate9BitToByte[((int) pDestBuffer[bufferOffset + 1] * num2 + (int) byte.MaxValue >> 8) + (int) sourceColor.green];
                                int num5 = BlenderPreMultBGRA.m_Saturate9BitToByte[((int) pDestBuffer[bufferOffset] * num2 + (int) byte.MaxValue >> 8) + (int) sourceColor.blue];
                                int num6 = (int) pDestBuffer[bufferOffset + 3];
                                pDestBuffer[ bufferOffset + 2 ] = ( byte ) num3;
                                pDestBuffer[ bufferOffset + 1 ] = ( byte ) num4;
                                pDestBuffer[ bufferOffset ] = ( byte ) num5;
                                pDestBuffer[ bufferOffset + 3 ] = ( byte ) ( ( int ) byte.MaxValue - BlenderPreMultBGRA.m_Saturate9BitToByte[ num2 * ( ( int ) byte.MaxValue - num6 ) + ( int ) byte.MaxValue >> 8 ] );
                                break;
                        }
                        ++sourceColorsOffset;
                        bufferOffset += 4;
                    }
                }
            }
            else
            {
                for ( int index = 0 ; index < count ; ++index )
                {
                    RGBA_Bytes sourceColor = sourceColors[sourceColorsOffset];
                    int num1 = ((int) sourceColor.alpha * (int) sourceCovers[sourceCoversOffset] + (int) byte.MaxValue) / 256;
                    if ( num1 == ( int ) byte.MaxValue )
                    {
                        pDestBuffer[ bufferOffset + 2 ] = sourceColor.red;
                        pDestBuffer[ bufferOffset + 1 ] = sourceColor.green;
                        pDestBuffer[ bufferOffset ] = sourceColor.blue;
                        pDestBuffer[ bufferOffset + 3 ] = ( byte ) num1;
                    }
                    else if ( num1 > 0 )
                    {
                        int num2 = (int) byte.MaxValue - num1;
                        int num3 = BlenderPreMultBGRA.m_Saturate9BitToByte[((int) pDestBuffer[bufferOffset + 2] * num2 + (int) byte.MaxValue >> 8) + (int) sourceColor.red];
                        int num4 = BlenderPreMultBGRA.m_Saturate9BitToByte[((int) pDestBuffer[bufferOffset + 1] * num2 + (int) byte.MaxValue >> 8) + (int) sourceColor.green];
                        int num5 = BlenderPreMultBGRA.m_Saturate9BitToByte[((int) pDestBuffer[bufferOffset] * num2 + (int) byte.MaxValue >> 8) + (int) sourceColor.blue];
                        int num6 = (int) pDestBuffer[bufferOffset + 3];
                        pDestBuffer[ bufferOffset + 2 ] = ( byte ) num3;
                        pDestBuffer[ bufferOffset + 1 ] = ( byte ) num4;
                        pDestBuffer[ bufferOffset ] = ( byte ) num5;
                        pDestBuffer[ bufferOffset + 3 ] = ( byte ) ( ( int ) byte.MaxValue - BlenderPreMultBGRA.m_Saturate9BitToByte[ num2 * ( ( int ) byte.MaxValue - num6 ) + ( int ) byte.MaxValue >> 8 ] );
                    }
                    ++sourceColorsOffset;
                    ++sourceCoversOffset;
                    bufferOffset += 4;
                }
            }
        }
    }
}
