// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.gamma_linear
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class gamma_linear : IGammaFunction
    {
        private double m_start;
        private double m_end;

        public gamma_linear()
        {
            this.m_start = 0.0;
            this.m_end = 1.0;
        }

        public gamma_linear( double s, double e )
        {
            this.m_start = s;
            this.m_end = e;
        }

        public void set( double s, double e )
        {
            this.m_start = s;
            this.m_end = e;
        }

        public void start( double s )
        {
            this.m_start = s;
        }

        public void end( double e )
        {
            this.m_end = e;
        }

        public double start()
        {
            return this.m_start;
        }

        public double end()
        {
            return this.m_end;
        }

        public double GetGamma( double x )
        {
            if ( x < this.m_start )
                return 0.0;
            if ( x > this.m_end )
                return 1.0;
            double num = this.m_end - this.m_start;
            if ( num != 0.0 )
                return ( x - this.m_start ) / num;
            return 0.0;
        }
    }
}
