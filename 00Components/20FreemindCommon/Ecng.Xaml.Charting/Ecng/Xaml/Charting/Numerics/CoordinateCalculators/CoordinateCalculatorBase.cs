// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Numerics.CoordinateCalculators.CoordinateCalculatorBase
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;
namespace fx.Xaml.Charting
{
    internal abstract class CoordinateCalculatorBase : ICoordinateCalculator<double>
    {
        public bool IsCategoryAxisCalculator
        {
            get; internal set;
        }

        public bool IsLogarithmicAxisCalculator
        {
            get; internal set;
        }

        public bool IsHorizontalAxisCalculator
        {
            get; internal set;
        }

        public bool IsXAxisCalculator
        {
            get; internal set;
        }

        public double CoordinatesOffset
        {
            get; internal set;
        }

        public bool HasFlippedCoordinates
        {
            get; internal set;
        }

        public bool IsPolarAxisCalculator
        {
            get; internal set;
        }

        public abstract double GetCoordinate( DateTime dataValue );

        public abstract double GetCoordinate( double dataValue );

        public virtual DoubleRange TranslateBy( double pixels, DoubleRange inputRange )
        {
            double dataValue = this.GetDataValue(0.0);
            double num = this.GetDataValue(pixels) - dataValue;
            if ( this.IsXAxisCalculator )
                num = -num;
            return new DoubleRange( inputRange.Min.ToDouble() + num, inputRange.Max.ToDouble() + num );
        }

        public abstract double GetDataValue( double pixelCoordinate );

        public virtual DoubleRange TranslateBy( double minFraction, double maxFraction, IRange inputRange )
        {
            return ( ( IRange ) inputRange.Clone() ).GrowBy( minFraction, maxFraction, false, 0.0 ).AsDoubleRange();
        }

        protected static double Flip( bool flipCoords, double coord, double viewportDimension )
        {
            if ( !flipCoords )
                return coord;
            return viewportDimension - coord - 1.0;
        }
    }
}
