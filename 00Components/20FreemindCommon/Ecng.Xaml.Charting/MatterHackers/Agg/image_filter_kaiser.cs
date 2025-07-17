// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.image_filter_kaiser
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg
{
    internal class image_filter_kaiser : IImageFilterFunction
    {
        private double a;
        private double i0a;
        private double epsilon;

        public image_filter_kaiser()
          : this( 6.33 )
        {
        }

        public image_filter_kaiser( double b )
        {
            this.a = b;
            this.epsilon = 1E-12;
            this.i0a = 1.0 / this.bessel_i0( b );
        }

        public double radius()
        {
            return 1.0;
        }

        public double calc_weight( double x )
        {
            return this.bessel_i0( this.a * Math.Sqrt( 1.0 - x * x ) ) * this.i0a;
        }

        private double bessel_i0( double x )
        {
            double num1 = 1.0;
            double num2 = x * x / 4.0;
            double num3 = num2;
            int num4 = 2;
            while ( num3 > this.epsilon )
            {
                num1 += num3;
                num3 *= num2 / ( double ) ( num4 * num4 );
                ++num4;
            }
            return num1;
        }
    }
}
