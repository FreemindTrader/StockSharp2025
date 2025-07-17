// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Common.Extensions.DoubleCollectionExtensions
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Linq;
using System.Windows.Media;

namespace fx.Xaml.Charting
{
    internal static class DoubleCollectionExtensions
    {
        internal static float[ ] ToFloatArray( this DoubleCollection doubleCollection )
        {
            if ( doubleCollection == null )
                return ( float[ ] ) null;
            return doubleCollection.Select<double, float>( ( Func<double, float> ) ( c => ( float ) c ) ).ToArray<float>();
        }
    }
}
