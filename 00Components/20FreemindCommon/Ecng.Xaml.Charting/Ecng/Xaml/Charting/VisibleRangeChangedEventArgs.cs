// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.VisibleRangeChangedEventArgs
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace StockSharp.Xaml.Charting
{
    public class VisibleRangeChangedEventArgs : EventArgs
    {
        public IRange OldVisibleRange
        {
            get; private set;
        }

        public IRange NewVisibleRange
        {
            get; private set;
        }

        public bool IsAnimating
        {
            get; private set;
        }

        public VisibleRangeChangedEventArgs( IRange oldRange, IRange newRange, bool isAnimationChange )
        {
            this.OldVisibleRange = oldRange;
            this.NewVisibleRange = newRange;
            this.IsAnimating = isAnimationChange;
        }
    }
}
