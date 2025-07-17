// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.gamma_power
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg
{
    internal class gamma_power : IGammaFunction
    {
        private double m_gamma;

        public gamma_power()
        {
            this.m_gamma = 1.0;
        }

        public gamma_power( double g )
        {
            this.m_gamma = g;
        }

        public void gamma( double g )
        {
            this.m_gamma = g;
        }

        public double gamma()
        {
            return this.m_gamma;
        }

        public double GetGamma( double x )
        {
            return Math.Pow( x, this.m_gamma );
        }
    }
}
