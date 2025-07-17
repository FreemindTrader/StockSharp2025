// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.line_coord
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Runtime.InteropServices;

namespace MatterHackers.Agg
{
    [StructLayout( LayoutKind.Sequential, Size = 1 )]
    internal struct line_coord
    {
        public static int conv( double x )
        {
            return ( int ) Math.Round( x * 256.0 );
        }
    }
}
