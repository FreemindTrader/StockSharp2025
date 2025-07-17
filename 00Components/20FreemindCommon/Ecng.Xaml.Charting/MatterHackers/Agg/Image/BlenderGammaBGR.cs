// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Image.BlenderGammaBGR
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg.Image
{
    internal sealed class BlenderGammaBGR : BlenderBaseBGR, IBlenderByte
    {
        private GammaLookUpTable m_gamma;

        public BlenderGammaBGR()
        {
            this.m_gamma = new GammaLookUpTable();
        }

        public BlenderGammaBGR( GammaLookUpTable g )
        {
            this.m_gamma = g;
        }

        public void gamma( GammaLookUpTable g )
        {
            this.m_gamma = g;
        }

        public RGBA_Bytes PixelToColorRGBA_Bytes( byte[ ] buffer, int bufferOffset )
        {
            return new RGBA_Bytes( ( int ) buffer[ bufferOffset + 2 ], ( int ) buffer[ bufferOffset + 1 ], ( int ) buffer[ bufferOffset ], ( int ) byte.MaxValue );
        }

        public void CopyPixels( byte[ ] buffer, int bufferOffset, RGBA_Bytes sourceColor, int count )
        {
            buffer[ bufferOffset + 2 ] = this.m_gamma.inv( ( int ) sourceColor.red );
            buffer[ bufferOffset + 1 ] = this.m_gamma.inv( ( int ) sourceColor.green );
            buffer[ bufferOffset ] = this.m_gamma.inv( ( int ) sourceColor.blue );
        }

        public void BlendPixel( byte[ ] buffer, int bufferOffset, RGBA_Bytes sourceColor )
        {
            int num1 = (int) buffer[bufferOffset + 2];
            int num2 = (int) buffer[bufferOffset + 1];
            int num3 = (int) buffer[bufferOffset];
            buffer[ bufferOffset + 2 ] = this.m_gamma.inv( ( int ) ( byte ) ( ( ( int ) sourceColor.red - num1 ) * ( int ) sourceColor.alpha + ( num1 << 8 ) >> 8 ) );
            buffer[ bufferOffset + 1 ] = this.m_gamma.inv( ( int ) ( byte ) ( ( ( int ) sourceColor.green - num2 ) * ( int ) sourceColor.alpha + ( num2 << 8 ) >> 8 ) );
            buffer[ bufferOffset ] = this.m_gamma.inv( ( int ) ( byte ) ( ( ( int ) sourceColor.blue - num3 ) * ( int ) sourceColor.alpha + ( num3 << 8 ) >> 8 ) );
        }

        public void BlendPixels( byte[ ] buffer, int bufferOffset, RGBA_Bytes[ ] sourceColors, int sourceColorsOffset, byte[ ] sourceCovers, int sourceCoversOffset, bool firstCoverForAll, int count )
        {
            throw new NotImplementedException();
        }
    }
}
