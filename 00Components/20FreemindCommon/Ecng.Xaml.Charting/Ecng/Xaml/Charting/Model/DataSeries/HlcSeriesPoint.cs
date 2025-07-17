// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Model.DataSeries.HlcSeriesPoint
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace Ecng.Xaml.Charting
{
    public struct HlcSeriesPoint : ISeriesPoint<double>, IComparable
    {
        public double Max
        {
            get
            {
                return this.YErrorHigh;
            }
        }

        public double Min
        {
            get
            {
                return this.YErrorLow;
            }
        }

        public double Y
        {
            get; private set;
        }

        public double YErrorHigh
        {
            get; private set;
        }

        public double YErrorLow
        {
            get; private set;
        }

        public HlcSeriesPoint( double y, double yErrorHigh, double yErrorLow )
        {
            this = new HlcSeriesPoint();
            this.Y = y;
            this.YErrorHigh = yErrorHigh;
            this.YErrorLow = yErrorLow;
        }

        public int CompareTo( object obj )
        {
            HlcSeriesPoint hlcSeriesPoint = (HlcSeriesPoint) obj;
            if ( this.Max > hlcSeriesPoint.Max )
                return 1;
            return this.Min >= hlcSeriesPoint.Min ? 0 : -1;
        }
    }
}
