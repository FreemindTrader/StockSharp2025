// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.IDrawable
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using StockSharp.Xaml.Charting.Rendering.Common;
using StockSharp.Xaml.Charting.Visuals.RenderableSeries;

namespace StockSharp.Xaml.Charting.Visuals
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
