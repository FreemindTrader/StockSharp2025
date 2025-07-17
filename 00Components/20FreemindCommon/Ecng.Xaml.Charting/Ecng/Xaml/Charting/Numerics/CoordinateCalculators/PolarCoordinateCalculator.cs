// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.CoordinateCalculators.PolarCoordinateCalculator
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace Ecng.Xaml.Charting
{
    internal class PolarCoordinateCalculator : CoordinateCalculatorBase
    {
        private const double MaxDegree = 360.0;
        private readonly double _min;
        private readonly double _max;
        private readonly double _dataPerDegree;

        public PolarCoordinateCalculator( double rangeMin, double rangeMax, bool isXAxis, bool isHorizontal, bool flipCoordinates )
        {
            this._min = rangeMin;
            this._max = rangeMax;
            this._dataPerDegree = ( this._max - this._min ) / 360.0;
            this.IsXAxisCalculator = isXAxis;
            this.IsHorizontalAxisCalculator = isHorizontal;
            this.HasFlippedCoordinates = flipCoordinates;
        }

        public override double GetCoordinate( DateTime dataValue )
        {
            return this.GetCoordinate( ( double ) dataValue.Ticks );
        }

        public override double GetCoordinate( double dataValue )
        {
            dataValue = ( dataValue - this._min ) / this._dataPerDegree;
            return CoordinateCalculatorBase.Flip( this.HasFlippedCoordinates, dataValue, 361.0 );
        }

        public override double GetDataValue( double pixelCoordinate )
        {
            pixelCoordinate = CoordinateCalculatorBase.Flip( this.HasFlippedCoordinates, pixelCoordinate, 361.0 );
            return this._min + pixelCoordinate * this._dataPerDegree;
        }
    }
}
