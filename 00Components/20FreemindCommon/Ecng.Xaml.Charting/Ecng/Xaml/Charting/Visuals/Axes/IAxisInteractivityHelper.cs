// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Axes.IAxisInteractivityHelper
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace StockSharp.Xaml.Charting.Visuals.Axes
{
    public interface IAxisInteractivityHelper
    {
        IRange Zoom( IRange initialRange, double fromCoord, double toCoord );

        IRange ZoomBy( IRange initialRange, double minFraction, double maxFraction );

        IRange ScrollInMinDirection( IRange rangeToScroll, double pixels );

        IRange ScrollInMaxDirection( IRange rangeToScroll, double pixels );

        IRange Scroll( IRange rangeToScroll, double pixels );

        [Obsolete( "The ScrollBy method is Obsolete as it is only really possible to implement on Category Axis. For this axis type just update the IndexRange (visibleRange) by N to scroll the axis", true )]
        IRange ScrollBy( IRange rangeToScroll, int pointAmount );

        IRange ClipRange( IRange rangeToClip, IRange maximumRange, ClipMode clipMode );
    }
}
