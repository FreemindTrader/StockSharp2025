// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.RenderableSeries.IRenderPassData
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using StockSharp.Xaml.Charting.Model.DataSeries;
using StockSharp.Xaml.Charting.Numerics.CoordinateCalculators;
using StockSharp.Xaml.Charting.StrategyManager;

namespace StockSharp.Xaml.Charting.Visuals.RenderableSeries
{
    public interface IRenderPassData
    {
        IndexRange PointRange
        {
            get;
        }

        IPointSeries PointSeries
        {
            get;
        }

        bool IsVerticalChart
        {
            get;
        }

        ICoordinateCalculator<double> YCoordinateCalculator
        {
            get;
        }

        ICoordinateCalculator<double> XCoordinateCalculator
        {
            get;
        }

        ITransformationStrategy TransformationStrategy
        {
            get;
        }
    }
}
