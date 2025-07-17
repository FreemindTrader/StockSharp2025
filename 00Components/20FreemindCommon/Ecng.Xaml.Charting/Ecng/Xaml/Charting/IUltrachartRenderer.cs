// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.IUltrachartRenderer
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using Ecng.Xaml.Charting.Rendering.Common;
using Ecng.Xaml.Charting.Services;

namespace Ecng.Xaml.Charting
{
    public interface IUltrachartRenderer
    {
        RendererErrorCode RenderLoop( IRenderContext2D renderContext );
    }
}
