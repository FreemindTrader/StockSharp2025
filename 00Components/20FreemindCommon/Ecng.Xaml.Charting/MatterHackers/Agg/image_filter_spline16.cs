// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.image_filter_spline16
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Runtime.InteropServices;

namespace MatterHackers.Agg
{
    [StructLayout( LayoutKind.Sequential, Size = 1 )]
    internal struct image_filter_spline16 : IImageFilterFunction
    {
        public double radius()
        {
            return 2.0;
        }

        public double calc_weight( double x )
        {
            if ( x < 1.0 )
                return ( ( x - 1.8 ) * x - 0.2 ) * x + 1.0;
            return ( ( -1.0 / 3.0 * ( x - 1.0 ) + 0.8 ) * ( x - 1.0 ) - 7.0 / 15.0 ) * ( x - 1.0 );
        }
    }
}
