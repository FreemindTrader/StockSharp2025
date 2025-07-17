// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Numerics.CoordinateCalculators.DoubleCoordinateCalculator
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;

namespace fx.Xaml.Charting
{
    internal sealed class DoubleCoordinateCalculator : CoordinateCalculatorBase
    {
        private readonly double _viewportDimension;
        private readonly double _min;
        private readonly double _max;
        private double _dimensionOverMaxMinusMin;
        private readonly double _oneOverViewportDimension;

        public DoubleCoordinateCalculator( double viewportDimension, double min, double max, XyDirection xyDirection, bool flipCoordinates )
          : this( viewportDimension, min, max, xyDirection == XyDirection.XDirection, xyDirection == XyDirection.XDirection, flipCoordinates )
        {
        }

        public DoubleCoordinateCalculator( double viewportDimension, double min, double max, bool isXAxis, bool isHorizontal, bool flipCoordinates )
        {
            IsXAxisCalculator = isXAxis;
            IsHorizontalAxisCalculator = isHorizontal;
            HasFlippedCoordinates = flipCoordinates;
            _viewportDimension = viewportDimension;
            _min = min;
            _max = max;
            _dimensionOverMaxMinusMin = ( _viewportDimension - 1.0 ) / ( _max - _min );
            _oneOverViewportDimension = 1.0 / ( _viewportDimension - 1.0 );
        }

        public override sealed double GetCoordinate( DateTime dataValue )
        {
            return GetCoordinate( ( double ) dataValue.Ticks );
        }

        public override sealed double GetCoordinate( double dataValue )
        {
            return ( _max - dataValue ) * _dimensionOverMaxMinusMin + CoordinatesOffset;
        }

        public override sealed double GetDataValue( double coordinate )
        {
            coordinate = _viewportDimension - ( coordinate - CoordinatesOffset ) - 1.0;
            return ( _max - _min ) * coordinate * _oneOverViewportDimension + _min;
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
