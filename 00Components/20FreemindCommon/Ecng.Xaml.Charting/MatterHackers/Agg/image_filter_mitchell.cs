// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.image_filter_mitchell
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class image_filter_mitchell : IImageFilterFunction
    {
        private double p0;
        private double p2;
        private double p3;
        private double q0;
        private double q1;
        private double q2;
        private double q3;

        public image_filter_mitchell()
          : this( 1.0 / 3.0, 1.0 / 3.0 )
        {
        }

        public image_filter_mitchell( double b, double c )
        {
            this.p0 = ( 6.0 - 2.0 * b ) / 6.0;
            this.p2 = ( 12.0 * b - 18.0 + 6.0 * c ) / 6.0;
            this.p3 = ( 12.0 - 9.0 * b - 6.0 * c ) / 6.0;
            this.q0 = ( 8.0 * b + 24.0 * c ) / 6.0;
            this.q1 = ( -12.0 * b - 48.0 * c ) / 6.0;
            this.q2 = ( 6.0 * b + 30.0 * c ) / 6.0;
            this.q3 = ( -b - 6.0 * c ) / 6.0;
        }

        public double radius()
        {
            return 2.0;
        }

        public double calc_weight( double x )
        {
            if ( x < 1.0 )
                return this.p0 + x * x * ( this.p2 + x * this.p3 );
            if ( x < 2.0 )
                return this.q0 + x * ( this.q1 + x * ( this.q2 + x * this.q3 ) );
            return 0.0;
        }
    }
}
