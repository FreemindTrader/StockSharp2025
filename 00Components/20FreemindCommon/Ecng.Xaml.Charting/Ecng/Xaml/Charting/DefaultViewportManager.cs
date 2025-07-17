// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.DefaultViewportManager
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace fx.Xaml.Charting
{
    public class DefaultViewportManager : ViewportManagerBase
    {
        public override void OnVisibleRangeChanged( IAxis axis )
        {
        }

        public override void OnParentSurfaceRendered( ISciChartSurface ultraChartSurface )
        {
        }

        protected override IRange OnCalculateNewXRange( IAxis xAxis )
        {
            IRange range = xAxis.VisibleRange;
            if ( xAxis.AutoRange == AutoRange.Always )
                range = this.CalculateAutoRange( xAxis );
            return range;
        }

        protected override IRange OnCalculateNewYRange( IAxis yAxis, RenderPassInfo renderPassInfo )
        {
            IRange range = yAxis.VisibleRange;
            if ( yAxis.AutoRange == AutoRange.Always && renderPassInfo.PointSeries != null && renderPassInfo.RenderableSeries != null )
                range = yAxis.CalculateYRange( renderPassInfo );
            return range;
        }
    }
}
