// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.image_filter_sinc
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg
{
    internal class image_filter_sinc : IImageFilterFunction
    {
        private double m_radius;

        public image_filter_sinc( double r )
        {
            this.m_radius = r < 2.0 ? 2.0 : r;
        }

        public double radius()
        {
            return this.m_radius;
        }

        public double calc_weight( double x )
        {
            if ( x == 0.0 )
                return 1.0;
            x *= Math.PI;
            return Math.Sin( x ) / x;
        }
    }
}
