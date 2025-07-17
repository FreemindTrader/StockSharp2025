// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Model.DataSeries.XyzSeriesPoint
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace Ecng.Xaml.Charting
{
    public struct XyzSeriesPoint : ISeriesPoint<double>, IComparable
    {
        private readonly double _y;
        private readonly double _z;

        public XyzSeriesPoint( double y, double z )
        {
            this = new XyzSeriesPoint();
            this._y = y;
            this._z = z;
        }

        public int CompareTo( object obj )
        {
            XyzSeriesPoint xyzSeriesPoint = (XyzSeriesPoint) obj;
            if ( this.Max > xyzSeriesPoint.Max )
                return 1;
            return this.Min < xyzSeriesPoint.Min ? -1 : 0;
        }

        public double Y
        {
            get
            {
                return this._y;
            }
        }

        public double Z
        {
            get
            {
                return this._z;
            }
        }

        public double Max
        {
            get
            {
                return this._y;
            }
        }

        public double Min
        {
            get
            {
                return this._y;
            }
        }
    }
}
