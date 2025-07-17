// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Model.DataSeries.I2DArraySegment
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Collections.Generic;

namespace StockSharp.Xaml.Charting.Model.DataSeries
{
    internal interface I2DArraySegment : IPoint
    {
        double XValueAtLeft
        {
            get;
        }

        double XValueAtRight
        {
            get;
        }

        double YValueAtBottom
        {
            get;
        }

        double YValueAtTop
        {
            get;
        }

        IList<int> GetVerticalPixelsArgb( DoubleToColorMappingSettings mappingSettings );

        IList<double> GetVerticalPixelValues();
    }
}
