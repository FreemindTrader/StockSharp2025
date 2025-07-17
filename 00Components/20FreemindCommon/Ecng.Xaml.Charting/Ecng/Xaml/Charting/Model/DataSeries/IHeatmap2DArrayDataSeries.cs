// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Model.DataSeries.IHeatmap2DArrayDataSeries
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using StockSharp.Xaml.Charting.Visuals.RenderableSeries;

namespace StockSharp.Xaml.Charting.Model.DataSeries
{
    public interface IHeatmap2DArrayDataSeries
    {
        double[ , ] GetArray2D();

        int ArrayHeight
        {
            get;
        }

        int ArrayWidth
        {
            get;
        }

        HitTestInfo ToHitTestInfo( double xValue, double yValue, bool interpolate = true );
    }
}
