// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Numerics.CoordinateCalculators.ICoordinateCalculator`1
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace StockSharp.Xaml.Charting.Numerics.CoordinateCalculators
{
    public interface ICoordinateCalculator<T> where T : IComparable
    {
        bool IsCategoryAxisCalculator
        {
            get;
        }

        bool IsLogarithmicAxisCalculator
        {
            get;
        }

        bool IsHorizontalAxisCalculator
        {
            get;
        }

        bool IsXAxisCalculator
        {
            get;
        }

        bool HasFlippedCoordinates
        {
            get;
        }

        bool IsPolarAxisCalculator
        {
            get;
        }

        double CoordinatesOffset
        {
            get;
        }

        double GetCoordinate( DateTime dataValue );

        double GetCoordinate( T dataValue );

        T GetDataValue( double pixelCoordinate );

        DoubleRange TranslateBy( double pixels, DoubleRange inputRange );

        DoubleRange TranslateBy( double minFraction, double maxFraction, IRange inputRange );
    }
}
