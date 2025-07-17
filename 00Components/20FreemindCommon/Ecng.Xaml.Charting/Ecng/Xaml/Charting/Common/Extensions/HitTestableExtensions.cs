// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Common.Extensions.HitTestableExtensions
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Windows;
namespace fx.Xaml.Charting
{
    internal static class HitTestableExtensions
    {
        internal static bool IsPointWithinBounds( this IHitTestable hitTestable, Point point )
        {
            return hitTestable.GetBoundsRelativeTo( hitTestable ).Contains( point );
        }
    }
}
