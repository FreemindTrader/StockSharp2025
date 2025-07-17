// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Rendering.Common.IRenderSurface
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Windows;
using StockSharp.Xaml.Charting.Visuals;

namespace StockSharp.Xaml.Charting.Rendering.Common
{
    public interface IRenderSurface : IDisposable, IHitTestable, IInvalidatableElement
    {
        event EventHandler<DrawEventArgs> Draw;

        event EventHandler<RenderedEventArgs> Rendered;

        bool NeedsResizing
        {
            get;
        }

        bool IsSizeValidForDrawing
        {
            get;
        }

        Style Style
        {
            get; set;
        }

        IServiceContainer Services
        {
            get; set;
        }

        void ClearSeries();

        void Clear();

        void RecreateSurface();
    }
}
