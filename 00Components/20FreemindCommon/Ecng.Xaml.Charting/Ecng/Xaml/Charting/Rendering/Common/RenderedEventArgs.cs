// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Rendering.Common.RenderedEventArgs
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;

namespace fx.Xaml.Charting
{
    public class RenderedEventArgs : EventArgs
    {
        private readonly double _duration;

        public RenderedEventArgs( double duration )
        {
            this._duration = duration;
        }

        public double Duration
        {
            get
            {
                return this._duration;
            }
        }
    }
}
