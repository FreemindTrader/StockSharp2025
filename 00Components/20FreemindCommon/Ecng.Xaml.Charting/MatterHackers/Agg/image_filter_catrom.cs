// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.image_filter_catrom
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Runtime.InteropServices;

namespace MatterHackers.Agg
{
    [StructLayout( LayoutKind.Sequential, Size = 1 )]
    internal struct image_filter_catrom : IImageFilterFunction
    {
        public double radius()
        {
            return 2.0;
        }

        public double calc_weight( double x )
        {
            if ( x < 1.0 )
                return 0.5 * ( 2.0 + x * x * ( x * 3.0 - 5.0 ) );
            if ( x < 2.0 )
                return 0.5 * ( 4.0 + x * ( x * ( 5.0 - x ) - 8.0 ) );
            return 0.0;
        }
    }
}
