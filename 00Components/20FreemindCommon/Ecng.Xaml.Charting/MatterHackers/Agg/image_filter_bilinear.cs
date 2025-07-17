// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.image_filter_bilinear
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Runtime.InteropServices;

namespace MatterHackers.Agg
{
    [StructLayout( LayoutKind.Sequential, Size = 1 )]
    internal struct image_filter_bilinear : IImageFilterFunction
    {
        public double radius()
        {
            return 1.0;
        }

        public double calc_weight( double x )
        {
            if ( Math.Abs( x ) >= 1.0 )
                return 0.0;
            if ( x < 0.0 )
                return 1.0 + x;
            return 1.0 - x;
        }
    }
}
