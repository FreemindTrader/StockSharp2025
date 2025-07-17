// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Rendering.Common.DrawEventArgs
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using Ecng.Xaml.Charting.Utility;

namespace Ecng.Xaml.Charting.Rendering.Common
{
    public class DrawEventArgs : EventArgs
    {
        public DrawEventArgs( IRenderSurface2D renderSurface )
        {
            Guard.NotNull( ( object ) renderSurface, nameof( renderSurface ) );
            this.RenderSurface2D = renderSurface;
        }

        public IRenderSurface2D RenderSurface2D
        {
            get; private set;
        }
    }
}
