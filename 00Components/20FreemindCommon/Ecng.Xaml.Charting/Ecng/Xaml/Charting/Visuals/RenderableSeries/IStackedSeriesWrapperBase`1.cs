// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.RenderableSeries.IStackedSeriesWrapperBase`1
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Windows;
using StockSharp.Xaml.Charting.Rendering.Common;

namespace StockSharp.Xaml.Charting.Visuals.RenderableSeries
{
    public interface IStackedSeriesWrapperBase<T> where T : IStackedRenderableSeries
    {
        bool IsOneHundredPercentGroup( string groupId );

        Tuple<double, double> AccumulateYValueAtX( T series, int index, bool isResampledSeries = false );

        DoubleRange CalculateYRange( T series, IndexRange xIndexRange );

        void DrawStackedSeries( IRenderContext2D renderContext );

        HitTestInfo ShiftHitTestInfo( Point rawPoint, HitTestInfo nearestHitResult, double hitTestRadius, T series );

        void AddSeries( T series );

        void RemoveSeries( T series );

        void MoveSeriesToAnotherGroup( T rSeries, string oldGroupId, string newGroupId );

        IList<T> GetStackedSeriesFromSameGroup( string groupId );

        int GetStackedSeriesCount();
    }
}
