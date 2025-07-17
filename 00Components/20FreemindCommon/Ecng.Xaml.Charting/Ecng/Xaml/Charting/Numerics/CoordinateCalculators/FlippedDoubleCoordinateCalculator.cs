// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Numerics.CoordinateCalculators.FlippedDoubleCoordinateCalculator
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\StockSharp.Xaml.Charting.dll

using System;

namespace StockSharp.Xaml.Charting.Numerics.CoordinateCalculators
{
    internal sealed class FlippedDoubleCoordinateCalculator : CoordinateCalculatorBase
    {
        private readonly double _min;
        private readonly double _max;
        private double _dimensionOverMaxMinusMin;
        private readonly double _oneOverViewportDimension;
        private readonly double _viewportDimensionMinusOne;

        public FlippedDoubleCoordinateCalculator( double viewportDimension, double min, double max, XyDirection xyDirection, bool flipCoordinates )
          : this( viewportDimension, min, max, xyDirection == XyDirection.XDirection, xyDirection == XyDirection.XDirection, flipCoordinates )
        {
        }

        public FlippedDoubleCoordinateCalculator( double viewportDimension, double min, double max, bool isXAxis, bool isHorizontal, bool flipCoordinates )
        {
            IsXAxisCalculator = isXAxis;
            IsHorizontalAxisCalculator = isHorizontal;
            HasFlippedCoordinates = flipCoordinates;
            _min = min;
            _max = max;
            _viewportDimensionMinusOne = viewportDimension - 1.0;
            _dimensionOverMaxMinusMin = _viewportDimensionMinusOne / ( _max - _min );
            _oneOverViewportDimension = 1.0 / _viewportDimensionMinusOne;
        }

        public override sealed double GetCoordinate( DateTime dataValue )
        {
            return GetCoordinate( ( double ) dataValue.Ticks );
        }

        public override sealed double GetCoordinate( double dataValue )
        {
            return _viewportDimensionMinusOne - ( _max - dataValue ) * _dimensionOverMaxMinusMin + CoordinatesOffset;
        }

        public override sealed double GetDataValue( double coordinate )
        {
            return ( _max - _min ) * ( coordinate - CoordinatesOffset ) * _oneOverViewportDimension + _min;
        }

        internal double CoordinateConstant
        {
            get
            {
                return _dimensionOverMaxMinusMin;
            }
            set
            {
                _dimensionOverMaxMinusMin = value;
            }
        }
    }
}
