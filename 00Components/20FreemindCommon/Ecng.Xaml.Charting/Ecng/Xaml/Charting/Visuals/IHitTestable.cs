// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Visuals.IHitTestable
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Windows;

namespace Ecng.Xaml.Charting.Visuals
{
    public interface IHitTestable
    {
        double ActualWidth
        {
            get;
        }

        double ActualHeight
        {
            get;
        }

        Point TranslatePoint( Point point, IHitTestable relativeTo );

        bool IsPointWithinBounds( Point point );

        Rect GetBoundsRelativeTo( IHitTestable relativeTo );
    }
}
