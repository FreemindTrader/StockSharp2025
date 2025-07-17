// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.TradeChartParams
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using Ecng.Xaml.Charting.Visuals.Axes;

namespace Ecng.Xaml.Charting.Numerics
{
    internal class TradeChartParams : IAxisParams
    {
        public IRange VisibleRange
        {
            get; set;
        }

        public IRange<double> GrowBy
        {
            get; set;
        }

        public IComparable MinorDelta
        {
            get; set;
        }

        public IComparable MajorDelta
        {
            get; set;
        }

        public IRange GetMaximumRange()
        {
            throw new NotImplementedException();
        }
    }
}
