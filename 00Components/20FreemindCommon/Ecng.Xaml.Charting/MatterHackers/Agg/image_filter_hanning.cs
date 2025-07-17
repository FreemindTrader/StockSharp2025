// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.image_filter_hanning
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Runtime.InteropServices;

namespace MatterHackers.Agg
{
    [StructLayout( LayoutKind.Sequential, Size = 1 )]
    internal struct image_filter_hanning : IImageFilterFunction
    {
        public double radius()
        {
            return 1.0;
        }

        public double calc_weight( double x )
        {
            return 0.5 + 0.5 * Math.Cos( Math.PI * x );
        }
    }
}
