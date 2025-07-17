// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Visuals.IDrawable
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using Ecng.Xaml.Charting.Rendering.Common;
using Ecng.Xaml.Charting.Visuals.RenderableSeries;

namespace Ecng.Xaml.Charting.Visuals
{
    public interface IDrawable
    {
        double Width
        {
            get; set;
        }

        double Height
        {
            get; set;
        }

        void OnDraw( IRenderContext2D renderContext, IRenderPassData renderPassData );
    }
}
