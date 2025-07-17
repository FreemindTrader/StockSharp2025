// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.gamma_none
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Runtime.InteropServices;

namespace MatterHackers.Agg
{
    [StructLayout( LayoutKind.Sequential, Size = 1 )]
    internal struct gamma_none : IGammaFunction
    {
        public double GetGamma( double x )
        {
            return x;
        }
    }
}
