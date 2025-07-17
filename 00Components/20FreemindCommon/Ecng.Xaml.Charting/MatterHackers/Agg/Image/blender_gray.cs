// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Image.blender_gray
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg.Image
{
    internal class blender_gray : IBlenderByte
    {
        private static int[] m_Saturate9BitToByte = new int[512];
        public const byte base_mask = 255;
        private const int base_shift = 8;
        private int bytesBetweenPixelsInclusive;

        public int NumPixelBits
        {
            get
            {
                return 8;
            }
        }

        public blender_gray( int bytesBetweenPixelsInclusive )
        {
            this.bytesBetweenPixelsInclusive = bytesBetweenPixelsInclusive;
            if ( blender_gray.m_Saturate9BitToByte[ 2 ] != 0 )
                return;
            for ( int val1 = 0 ; val1 < blender_gray.m_Saturate9BitToByte.Length ; ++val1 )
                blender_gray.m_Saturate9BitToByte[ val1 ] = Math.Min( val1, ( int ) byte.MaxValue );
        }

        public RGBA_Bytes PixelToColorRGBA_Bytes( byte[ ] buffer, int bufferOffset )
        {
            int num = (int) buffer[bufferOffset];
            return new RGBA_Bytes( num, num, num, ( int ) byte.MaxValue );
        }

        public void CopyPixels( byte[ ] pDestBuffer, int bufferOffset, RGBA_Bytes sourceColor, int count )
        {
            do
            {
                int num = (int) sourceColor.red * 77 + (int) sourceColor.green * 151 + (int) sourceColor.blue * 28 >> 8;
                pDestBuffer[ bufferOffset ] = ( byte ) num;
                bufferOffset += this.bytesBetweenPixelsInclusive;
            }
            while ( --count != 0 );
        }

        public void BlendPixel( byte[ ] pDestBuffer, int bufferOffset, RGBA_Bytes sourceColor )
        {
            int num = (int) (byte) ((((int) sourceColor.red * 77 + (int) sourceColor.green * 151 + (int) sourceColor.blue * 28 >> 8) - (int) pDestBuffer[bufferOffset]) * (int) sourceColor.alpha + ((int) pDestBuffer[bufferOffset] << 8) >> 8);
            pDestBuffer[ bufferOffset ] = ( byte ) num;
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
                        bufferOffset += this.bytesBetweenPixelsInclusive;
                    }
                    while ( --count != 0 );
                }
                else
                {
                    do
                    {
                        sourceColors[ sourceColorsOffset ].alpha = ( byte ) ( ( int ) sourceColors[ sourceColorsOffset ].alpha * cover + ( int ) byte.MaxValue >> 8 );
                        this.BlendPixel( destBuffer, bufferOffset, sourceColors[ sourceColorsOffset ] );
                        bufferOffset += this.bytesBetweenPixelsInclusive;
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
                    bufferOffset += this.bytesBetweenPixelsInclusive;
                    ++sourceColorsOffset;
                }
                while ( --count != 0 );
            }
        }
    }
}
