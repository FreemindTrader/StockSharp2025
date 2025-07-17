// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.image_filter_spline36
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Runtime.InteropServices;

namespace MatterHackers.Agg
{
    [StructLayout( LayoutKind.Sequential, Size = 1 )]
    internal struct image_filter_spline36 : IImageFilterFunction
    {
        public double radius()
        {
            return 3.0;
        }

        public double calc_weight( double x )
        {
            if ( x < 1.0 )
                return ( ( 13.0 / 11.0 * x - 453.0 / 209.0 ) * x - 3.0 / 209.0 ) * x + 1.0;
            if ( x < 2.0 )
                return ( ( -6.0 / 11.0 * ( x - 1.0 ) + 270.0 / 209.0 ) * ( x - 1.0 ) - 156.0 / 209.0 ) * ( x - 1.0 );
            return ( ( 1.0 / 11.0 * ( x - 2.0 ) - 45.0 / 209.0 ) * ( x - 2.0 ) + 26.0 / 209.0 ) * ( x - 2.0 );
        }
    }
}
