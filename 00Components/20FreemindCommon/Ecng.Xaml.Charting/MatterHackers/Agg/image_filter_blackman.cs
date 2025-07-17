// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.image_filter_blackman
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg
{
    internal class image_filter_blackman : IImageFilterFunction
    {
        private double m_radius;

        public image_filter_blackman( double r )
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
            if ( x > this.m_radius )
                return 0.0;
            x *= Math.PI;
            double d = x / this.m_radius;
            return Math.Sin( x ) / x * ( 0.42 + 0.5 * Math.Cos( d ) + 0.08 * Math.Cos( 2.0 * d ) );
        }
    }
}
