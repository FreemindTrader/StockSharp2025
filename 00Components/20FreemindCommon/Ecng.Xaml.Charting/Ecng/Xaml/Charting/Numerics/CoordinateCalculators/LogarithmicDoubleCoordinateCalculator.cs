// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.CoordinateCalculators.LogarithmicDoubleCoordinateCalculator
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using Ecng.Xaml.Charting.Common.Extensions;

namespace Ecng.Xaml.Charting.Numerics.CoordinateCalculators
{
    internal sealed class LogarithmicDoubleCoordinateCalculator : CoordinateCalculatorBase, ILogarithmicCoordinateCalculator, ICoordinateCalculator<double>
    {
        private readonly double _dimensionOverMaxMinusMin;
        private readonly double _logMax;
        private readonly double _logMin;
        private readonly double _mult;
        private readonly double _oneOverViewportDimension;
        private readonly double _viewportDimension;
        private readonly double _logBase;

        public LogarithmicDoubleCoordinateCalculator( double viewportDimension, double min, double max, XyDirection xyDirection, bool flipCoordinates )
          : this( viewportDimension, min, max, 10.0, xyDirection == XyDirection.XDirection, true, flipCoordinates )
        {
        }

        public LogarithmicDoubleCoordinateCalculator( double viewportDimension, double min, double max, double logBase, bool isXAxis, bool isHorizontal, bool flipCoordinates )
        {
            IsXAxisCalculator = isXAxis;
            IsHorizontalAxisCalculator = isHorizontal;
            HasFlippedCoordinates = flipCoordinates;
            IsLogarithmicAxisCalculator = true;
            _logBase = logBase;
            _viewportDimension = viewportDimension;
            _mult = min < 0.0 ? -1.0 : 1.0;
            _logMax = Math.Log( _mult * max, _logBase );
            _logMin = Math.Log( _mult * min, _logBase );
            _dimensionOverMaxMinusMin = ( viewportDimension - 1.0 ) / ( _logMax - _logMin );
            _oneOverViewportDimension = 1.0 / ( _viewportDimension - 1.0 );
        }

        public double LogarithmicBase
        {
            get
            {
                return _logBase;
            }
        }

        public override sealed double GetCoordinate( DateTime dataValue )
        {
            return GetCoordinate( ( double ) dataValue.Ticks );
        }

        public override sealed double GetCoordinate( double dataValue )
        {
            return CoordinateCalculatorBase.Flip( IsXAxisCalculator ^ HasFlippedCoordinates, ( _logMax - Math.Log( _mult * dataValue, _logBase ) ) * _dimensionOverMaxMinusMin, _viewportDimension ) + CoordinatesOffset;
        }

        public override sealed double GetDataValue( double coordinate )
        {
            coordinate = CoordinateCalculatorBase.Flip( IsXAxisCalculator == HasFlippedCoordinates, coordinate - CoordinatesOffset, _viewportDimension );
            return Math.Pow( _logBase, ( _logMax - _logMin ) * coordinate * _oneOverViewportDimension + _logMin ) * _mult;
        }

        public override DoubleRange TranslateBy( double minFraction, double maxFraction, IRange inputRange )
        {
            return ( ( IRange ) inputRange.Clone() ).GrowBy( minFraction, maxFraction, true, _logBase ).AsDoubleRange();
        }

        public override DoubleRange TranslateBy( double pixels, DoubleRange inputRange )
        {
            double dataValue = GetDataValue(0.0);
            double num = Math.Log(GetDataValue(pixels), _logBase) - Math.Log(dataValue, _logBase);
            if ( IsXAxisCalculator )
                num = -num;
            return new DoubleRange( Math.Pow( _logBase, _logMin + num ), Math.Pow( _logBase, _logMax + num ) );
        }
    }
}
