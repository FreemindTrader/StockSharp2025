// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.RenderPassInfo
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Collections.Generic;
using System.Windows;
using Ecng.Xaml.Charting.Model.DataSeries;
using Ecng.Xaml.Charting.Numerics.CoordinateCalculators;
using Ecng.Xaml.Charting.StrategyManager;
using Ecng.Xaml.Charting.Visuals.RenderableSeries;

namespace Ecng.Xaml.Charting
{
    public struct RenderPassInfo
    {
        public Size ViewportSize;
        public IRenderableSeries[] RenderableSeries;
        public IPointSeries[] PointSeries;
        public IDataSeries[] DataSeries;
        public IndexRange[] IndicesRanges;
        public IDictionary<string, ICoordinateCalculator<double>> XCoordinateCalculators;
        public IDictionary<string, ICoordinateCalculator<double>> YCoordinateCalculators;
        public ITransformationStrategy TransformationStrategy;

        public List<string> Warnings
        {
            get; set;
        }
    }
}
