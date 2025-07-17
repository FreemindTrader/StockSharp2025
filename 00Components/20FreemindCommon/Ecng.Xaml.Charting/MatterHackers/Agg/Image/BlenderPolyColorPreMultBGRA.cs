// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Image.BlenderPolyColorPreMultBGRA
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg.Image
{
    internal sealed class BlenderPolyColorPreMultBGRA : BlenderBaseBGRA, IBlenderByte
    {
        private static int[] m_Saturate9BitToByte = new int[512];
        private RGBA_Bytes polyColor;

        public BlenderPolyColorPreMultBGRA( RGBA_Bytes polyColor )
        {
            this.polyColor = polyColor;
            if ( BlenderPolyColorPreMultBGRA.m_Saturate9BitToByte[ 2 ] != 0 )
                return;
            for ( int val1 = 0 ; val1 < BlenderPolyColorPreMultBGRA.m_Saturate9BitToByte.Length ; ++val1 )
                BlenderPolyColorPreMultBGRA.m_Saturate9BitToByte[ val1 ] = Math.Min( val1, ( int ) byte.MaxValue );
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
            int num1 = (int) byte.MaxValue - (int) (byte) BlenderPolyColorPreMultBGRA.m_Saturate9BitToByte[this.polyColor.Alpha0To255 * (int) sourceColor.alpha + (int) byte.MaxValue >> 8];
            int num2 = (int) (byte) BlenderPolyColorPreMultBGRA.m_Saturate9BitToByte[this.polyColor.Alpha0To255 * (int) sourceColor.red + (int) byte.MaxValue >> 8];
            int num3 = (int) (byte) BlenderPolyColorPreMultBGRA.m_Saturate9BitToByte[this.polyColor.Alpha0To255 * (int) sourceColor.green + (int) byte.MaxValue >> 8];
            int num4 = (int) (byte) BlenderPolyColorPreMultBGRA.m_Saturate9BitToByte[this.polyColor.Alpha0To255 * (int) sourceColor.blue + (int) byte.MaxValue >> 8];
            int num5 = BlenderPolyColorPreMultBGRA.m_Saturate9BitToByte[((int) pDestBuffer[bufferOffset + 2] * num1 + (int) byte.MaxValue >> 8) + num2];
            int num6 = BlenderPolyColorPreMultBGRA.m_Saturate9BitToByte[((int) pDestBuffer[bufferOffset + 1] * num1 + (int) byte.MaxValue >> 8) + num3];
            int num7 = BlenderPolyColorPreMultBGRA.m_Saturate9BitToByte[((int) pDestBuffer[bufferOffset] * num1 + (int) byte.MaxValue >> 8) + num4];
            pDestBuffer[ bufferOffset + 2 ] = ( byte ) num5;
            pDestBuffer[ bufferOffset + 1 ] = ( byte ) num6;
            pDestBuffer[ bufferOffset ] = ( byte ) num7;
        }

        public void BlendPixels( byte[ ] pDestBuffer, int bufferOffset, RGBA_Bytes[ ] sourceColors, int sourceColorsOffset, byte[ ] sourceCovers, int sourceCoversOffset, bool firstCoverForAll, int count )
        {
            if ( !firstCoverForAll )
                throw new NotImplementedException( "need to consider the polyColor" );
            if ( sourceCovers[ sourceCoversOffset ] != byte.MaxValue )
                throw new NotImplementedException( "need to consider the polyColor" );
            for ( int index = 0 ; index < count ; ++index )
            {
                this.BlendPixel( pDestBuffer, bufferOffset, sourceColors[ sourceColorsOffset ] );
                ++sourceColorsOffset;
                bufferOffset += 4;
            }
        }
    }
}
