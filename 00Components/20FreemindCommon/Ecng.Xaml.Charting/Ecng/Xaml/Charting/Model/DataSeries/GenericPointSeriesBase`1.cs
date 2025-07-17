// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Model.DataSeries.GenericPointSeriesBase`1
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace StockSharp.Xaml.Charting.Model.DataSeries
{
    public abstract class GenericPointSeriesBase<TY> : IGenericPointSeries<TY>, IPointSeries where TY : IComparable
    {
        private readonly IPointSeries _yPoints;

        protected GenericPointSeriesBase( IPointSeries yPoints )
        {
            this._yPoints = yPoints;
        }

        public IPointSeries YPoints
        {
            get
            {
                return this._yPoints;
            }
        }

        public IUltraList<double> XValues
        {
            get
            {
                return this._yPoints.XValues;
            }
        }

        public IUltraList<double> YValues
        {
            get
            {
                return this._yPoints.YValues;
            }
        }

        public abstract int Count
        {
            get;
        }

        public abstract IPoint this[ int index ] { get; }

        public abstract DoubleRange GetYRange();
    }
}
