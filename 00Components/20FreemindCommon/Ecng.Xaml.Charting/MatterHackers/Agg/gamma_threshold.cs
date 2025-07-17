// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.gamma_threshold
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class gamma_threshold : IGammaFunction
    {
        private double m_threshold;

        public gamma_threshold()
        {
            this.m_threshold = 0.5;
        }

        public gamma_threshold( double t )
        {
            this.m_threshold = t;
        }

        public void threshold( double t )
        {
            this.m_threshold = t;
        }

        public double threshold()
        {
            return this.m_threshold;
        }

        public double GetGamma( double x )
        {
            return x >= this.m_threshold ? 1.0 : 0.0;
        }
    }
}
