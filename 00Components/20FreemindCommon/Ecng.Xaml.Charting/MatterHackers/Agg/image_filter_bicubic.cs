// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.image_filter_bicubic
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class image_filter_bicubic : IImageFilterFunction
    {
        private static double pow3( double x )
        {
            if ( x > 0.0 )
                return x * x * x;
            return 0.0;
        }

        public double radius()
        {
            return 2.0;
        }

        public double calc_weight( double x )
        {
            return 1.0 / 6.0 * ( image_filter_bicubic.pow3( x + 2.0 ) - 4.0 * image_filter_bicubic.pow3( x + 1.0 ) + 6.0 * image_filter_bicubic.pow3( x ) - 4.0 * image_filter_bicubic.pow3( x - 1.0 ) );
        }
    }
}
