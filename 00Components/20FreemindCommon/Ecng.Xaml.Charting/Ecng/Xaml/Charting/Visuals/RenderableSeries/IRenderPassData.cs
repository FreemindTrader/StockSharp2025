// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.IRenderPassData
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace fx.Xaml.Charting
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
