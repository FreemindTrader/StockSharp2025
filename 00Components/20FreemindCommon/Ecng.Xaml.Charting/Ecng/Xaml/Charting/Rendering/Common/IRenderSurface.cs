// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Rendering.Common.IRenderSurface
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows;
using Ecng.Xaml.Charting.Visuals;

namespace Ecng.Xaml.Charting.Rendering.Common
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
