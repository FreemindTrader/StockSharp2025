// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.CoordinateCalculators.CoordinateCalculatorOptions
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace Ecng.Xaml.Charting.Numerics.CoordinateCalculators
{
    [Flags]
    internal enum CoordinateCalculatorOptions
    {
        XAxisCalculator = 1,
        YAxisCalculator = 2,
        CategoryAxisCalculator = 4,
        LogarithmicAxisCalculator = 8,
        HorizontalAxisCalculator = 16, // 0x00000010
        WithFlippedCoordinates = 32, // 0x00000020
    }
}
