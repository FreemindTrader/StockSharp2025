// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.gamma_multiply
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class gamma_multiply : IGammaFunction
    {
        private double m_mul;

        public gamma_multiply()
        {
            this.m_mul = 1.0;
        }

        public gamma_multiply( double v )
        {
            this.m_mul = v;
        }

        public void value( double v )
        {
            this.m_mul = v;
        }

        public double value()
        {
            return this.m_mul;
        }

        public double GetGamma( double x )
        {
            double num = x * this.m_mul;
            if ( num > 1.0 )
                num = 1.0;
            return num;
        }
    }
}
