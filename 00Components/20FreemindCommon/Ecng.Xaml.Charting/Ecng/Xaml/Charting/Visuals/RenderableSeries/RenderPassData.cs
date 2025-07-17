// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.RenderPassData
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace Ecng.Xaml.Charting
{
    public class RenderPassData : IRenderPassData
    {
        private readonly IndexRange _pointRange;
        private readonly ICoordinateCalculator<double> _xCoordinateCalculator;
        private readonly ICoordinateCalculator<double> _yCoordinateCalculator;
        private readonly IPointSeries _pointSeries;
        private readonly ITransformationStrategy _transformationStrategy;

        public RenderPassData( IndexRange pointRange, ICoordinateCalculator<double> xCoordinateCalculator, ICoordinateCalculator<double> yCoordinateCalculator, IPointSeries pointSeries, ITransformationStrategy transformationStrategy )
        {
            this._pointRange = pointRange;
            this._xCoordinateCalculator = xCoordinateCalculator;
            this._yCoordinateCalculator = yCoordinateCalculator;
            this._pointSeries = pointSeries;
            this._transformationStrategy = transformationStrategy;
        }

        public bool IsVerticalChart
        {
            get
            {
                return !this.XCoordinateCalculator.IsHorizontalAxisCalculator;
            }
        }

        public ICoordinateCalculator<double> YCoordinateCalculator
        {
            get
            {
                return this._yCoordinateCalculator;
            }
        }

        public ICoordinateCalculator<double> XCoordinateCalculator
        {
            get
            {
                return this._xCoordinateCalculator;
            }
        }

        public IPointSeries PointSeries
        {
            get
            {
                return this._pointSeries;
            }
        }

        public IndexRange PointRange
        {
            get
            {
                return this._pointRange;
            }
        }

        public ITransformationStrategy TransformationStrategy
        {
            get
            {
                return this._transformationStrategy;
            }
        }
    }
}
