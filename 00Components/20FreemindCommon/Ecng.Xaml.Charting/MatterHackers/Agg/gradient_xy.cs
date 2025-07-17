// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.gradient_xy
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg
{
    internal class gradient_xy : IGradient
    {
        public int calculate( int x, int y, int d )
        {
            return Math.Abs( x ) * Math.Abs( y ) / d;
        }
    }
}
