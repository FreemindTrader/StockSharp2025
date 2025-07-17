// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Rendering.Common.IRenderSurface2D
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using StockSharp.Xaml.Charting.Visuals;
using StockSharp.Xaml.Charting.Visuals.RenderableSeries;

namespace StockSharp.Xaml.Charting.Rendering.Common
{
    public interface IRenderSurface2D : IRenderSurface, IDisposable, IHitTestable, IInvalidatableElement
    {
        ReadOnlyCollection<IRenderableSeries> ChildSeries
        {
            get;
        }

        IRenderContext2D GetRenderContext();

        bool ContainsSeries( IRenderableSeries renderableSeries );

        void AddSeries( IRenderableSeries renderableSeries );

        void AddSeries( IEnumerable<IRenderableSeries> renderableSeries );

        void RemoveSeries( IRenderableSeries renderableSeries );
    }
}
