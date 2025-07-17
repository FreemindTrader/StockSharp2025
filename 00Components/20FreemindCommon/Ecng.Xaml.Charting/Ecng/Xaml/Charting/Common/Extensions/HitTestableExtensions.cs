// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Common.Extensions.HitTestableExtensions
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Windows;
using StockSharp.Xaml.Charting.Visuals;

namespace StockSharp.Xaml.Charting.Common.Extensions
{
    internal static class HitTestableExtensions
    {
        internal static bool IsPointWithinBounds( this IHitTestable hitTestable, Point point )
        {
            return hitTestable.GetBoundsRelativeTo( hitTestable ).Contains( point );
        }
    }
}
