// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Model.DataSeries.BoxSeriesPoint
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;

namespace fx.Xaml.Charting
{
    public struct BoxSeriesPoint : ISeriesPoint<double>, IComparable
    {
        public double Max
        {
            get; private set;
        }

        public double Min
        {
            get; private set;
        }

        public double Y
        {
            get; private set;
        }

        public double LowerQuartile
        {
            get; private set;
        }

        public double UpperQuartile
        {
            get; private set;
        }

        public BoxSeriesPoint( double y, double min, double lower, double upper, double max )
        {
            this = new BoxSeriesPoint();
            this.Y = y;
            this.Max = max;
            this.Min = min;
            this.LowerQuartile = lower;
            this.UpperQuartile = upper;
        }

        public int CompareTo( object obj )
        {
            BoxSeriesPoint boxSeriesPoint = (BoxSeriesPoint) obj;
            if ( this.Max > boxSeriesPoint.Max )
                return 1;
            return this.Min >= boxSeriesPoint.Min ? 0 : -1;
        }
    }
}
