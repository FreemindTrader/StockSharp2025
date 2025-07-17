// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Common.Extensions.FloatExtensions
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace StockSharp.Xaml.Charting.Common.Extensions
{
    internal static class FloatExtensions
    {
        internal static float ClipToInt( this float d )
        {
            if ( ( double ) d > 2147483648.0 )
                return ( float ) int.MaxValue;
            if ( ( double ) d < ( double ) int.MinValue )
                return ( float ) int.MinValue;
            return d;
        }
    }
}
