// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.GammaLookUpTable
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg
{
    internal class GammaLookUpTable
    {
        private double m_gamma;
        private byte[] m_dir_gamma;
        private byte[] m_inv_gamma;

        public GammaLookUpTable()
        {
            this.m_gamma = 1.0;
            this.m_dir_gamma = new byte[ 256 ];
            this.m_inv_gamma = new byte[ 256 ];
        }

        public GammaLookUpTable( double gamma )
        {
            this.m_gamma = gamma;
            this.m_dir_gamma = new byte[ 256 ];
            this.m_inv_gamma = new byte[ 256 ];
            this.SetGamma( this.m_gamma );
        }

        public void SetGamma( double g )
        {
            this.m_gamma = g;
            for ( uint index = 0 ; index < 256U ; ++index )
                this.m_dir_gamma[ ( int ) index ] = ( byte ) agg_basics.uround( Math.Pow( ( double ) index / ( double ) byte.MaxValue, this.m_gamma ) * ( double ) byte.MaxValue );
            double y = 1.0 / g;
            for ( uint index = 0 ; index < 256U ; ++index )
                this.m_inv_gamma[ ( int ) index ] = ( byte ) agg_basics.uround( Math.Pow( ( double ) index / ( double ) byte.MaxValue, y ) * ( double ) byte.MaxValue );
        }

        public double GetGamma()
        {
            return this.m_gamma;
        }

        public byte dir( int v )
        {
            return this.m_dir_gamma[ v ];
        }

        public byte inv( int v )
        {
            return this.m_inv_gamma[ v ];
        }

        private enum gamma_scale_e
        {
            gamma_shift = 8,
            gamma_mask = 255, // 0x000000FF
            gamma_size = 256, // 0x00000100
        }
    }
}
