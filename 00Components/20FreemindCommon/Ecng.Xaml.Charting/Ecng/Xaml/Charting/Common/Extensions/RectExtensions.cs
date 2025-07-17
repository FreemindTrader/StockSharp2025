// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Extensions.RectExtensions
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Windows;

namespace Ecng.Xaml.Charting.Common.Extensions
{
    internal static class RectExtensions
    {
        internal static Point ClipToBounds( this Rect rect, Point point )
        {
            double right = rect.Right;
            double left = rect.Left;
            double top = rect.Top;
            double bottom = rect.Bottom;
            point.X = point.X > right ? right : point.X;
            point.X = point.X < left ? left : point.X;
            point.Y = point.Y > bottom ? bottom : point.Y;
            point.Y = point.Y < top ? top : point.Y;
            return point;
        }

        internal static Rect Expand( this Rect rect, double offset )
        {
            return new Rect( rect.X - offset, rect.Y - offset, rect.Width + 2.0 * offset, rect.Height + 2.0 * offset );
        }
    }
}
