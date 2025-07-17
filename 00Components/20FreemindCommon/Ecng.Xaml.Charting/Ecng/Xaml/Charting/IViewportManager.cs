// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.IViewportManager
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using Ecng.Xaml.Charting.Model.DataSeries;
using Ecng.Xaml.Charting.Visuals;
using Ecng.Xaml.Charting.Visuals.Axes;

namespace Ecng.Xaml.Charting
{
    public interface IViewportManager : IUltrachartController, ISuspendable, IInvalidatableElement
    {
        IServiceContainer Services
        {
            get; set;
        }

        bool IsAttached
        {
            get;
        }

        void OnVisibleRangeChanged( IAxis axis );

        IRange CalculateNewYAxisRange( IAxis yAxis, RenderPassInfo renderPassInfo );

        IRange CalculateNewXAxisRange( IAxis xAxis );

        IRange CalculateAutoRange( IAxis axis );

        void OnParentSurfaceRendered( ISciChartSurface ultraChartSurface );

        void InvalidateParentSurface( RangeMode rangeMode );

        void AttachUltrachartSurface( ISciChartSurface scs );

        void DetachUltrachartSurface();
    }
}
