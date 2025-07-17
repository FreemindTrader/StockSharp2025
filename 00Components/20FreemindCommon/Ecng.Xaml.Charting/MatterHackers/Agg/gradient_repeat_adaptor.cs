// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.gradient_repeat_adaptor
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class gradient_repeat_adaptor : IGradient
    {
        private IGradient m_gradient;

        public gradient_repeat_adaptor( IGradient gradient )
        {
            this.m_gradient = gradient;
        }

        public int calculate( int x, int y, int d )
        {
            int num = this.m_gradient.calculate(x, y, d) % d;
            if ( num < 0 )
                num += d;
            return num;
        }
    }
}
