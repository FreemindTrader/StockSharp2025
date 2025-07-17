// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Extensions.DoubleCollectionExtensions
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Linq;
using System.Windows.Media;

namespace Ecng.Xaml.Charting.Common.Extensions
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
