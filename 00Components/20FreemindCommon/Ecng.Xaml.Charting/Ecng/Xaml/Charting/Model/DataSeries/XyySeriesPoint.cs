// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Model.DataSeries.XyySeriesPoint
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace StockSharp.Xaml.Charting.Model.DataSeries
{
    public struct XyySeriesPoint : ISeriesPoint<double>, IComparable
    {
        private readonly double _y0;
        private readonly double _y1;

        public XyySeriesPoint( double y0, double y1 )
        {
            this = new XyySeriesPoint();
            this._y0 = y0;
            this._y1 = y1;
        }

        public int CompareTo( object obj )
        {
            XyySeriesPoint xyySeriesPoint = (XyySeriesPoint) obj;
            if ( this.Y0 > xyySeriesPoint.Max || this.Y1 > xyySeriesPoint.Max )
                return 1;
            return this.Y0 < xyySeriesPoint.Min || this.Y1 < xyySeriesPoint.Min ? -1 : 0;
        }

        public double Y0
        {
            get
            {
                return this._y0;
            }
        }

        public double Y1
        {
            get
            {
                return this._y1;
            }
        }

        public double Max
        {
            get
            {
                return Math.Max( this._y0, this._y1 );
            }
        }

        public double Min
        {
            get
            {
                return Math.Min( this._y0, this._y1 );
            }
        }

        public double Y
        {
            get
            {
                return this.Y0;
            }
        }
    }
}
