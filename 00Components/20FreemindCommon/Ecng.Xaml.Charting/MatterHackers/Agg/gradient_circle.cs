// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.gradient_circle
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class gradient_circle : IGradient
    {
        public int calculate( int x, int y, int d )
        {
            return agg_math.fast_sqrt( x * x + y * y );
        }
    }
}
