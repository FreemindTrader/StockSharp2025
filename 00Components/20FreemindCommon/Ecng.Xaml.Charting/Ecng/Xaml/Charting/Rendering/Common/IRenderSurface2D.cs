// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Rendering.Common.IRenderSurface2D
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ecng.Xaml.Charting.Visuals;
using Ecng.Xaml.Charting.Visuals.RenderableSeries;

namespace Ecng.Xaml.Charting.Rendering.Common
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
