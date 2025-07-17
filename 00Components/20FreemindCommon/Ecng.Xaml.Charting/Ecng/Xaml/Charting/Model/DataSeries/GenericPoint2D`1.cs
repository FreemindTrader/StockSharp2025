// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Model.DataSeries.GenericPoint2D`1
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace fx.Xaml.Charting
{
    public class GenericPoint2D<TY> : IPoint where TY : ISeriesPoint<double>
    {
        private double _x;
        private TY _y;

        public GenericPoint2D( double x, TY y )
        {
            this._x = x;
            this._y = y;
        }

        public double X
        {
            get
            {
                return this._x;
            }
        }

        public double Y
        {
            get
            {
                return this._y.Y;
            }
        }

        public TY YValues
        {
            get
            {
                return this._y;
            }
        }
    }
}
