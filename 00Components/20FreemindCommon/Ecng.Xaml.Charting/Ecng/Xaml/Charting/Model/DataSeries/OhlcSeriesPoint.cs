// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Model.DataSeries.OhlcSeriesPoint
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace Ecng.Xaml.Charting.Model.DataSeries
{
    public struct OhlcSeriesPoint : ISeriesPoint<double>, IComparable
    {
        public OhlcSeriesPoint( double open, double high, double low, double close )
        {
            this = new OhlcSeriesPoint();
            this.Open = open;
            this.High = high;
            this.Low = low;
            this.Close = close;
        }

        public double Open
        {
            get; private set;
        }

        public double High
        {
            get; private set;
        }

        public double Low
        {
            get; private set;
        }

        public double Close
        {
            get; private set;
        }

        public double Max
        {
            get
            {
                return this.High;
            }
        }

        public double Min
        {
            get
            {
                return this.Low;
            }
        }

        public double Y
        {
            get
            {
                return this.Close;
            }
        }

        public int CompareTo( object obj )
        {
            OhlcSeriesPoint ohlcSeriesPoint = (OhlcSeriesPoint) obj;
            if ( this.Max > ohlcSeriesPoint.Max )
                return 1;
            return this.Min >= ohlcSeriesPoint.Min ? 0 : -1;
        }
    }
}
