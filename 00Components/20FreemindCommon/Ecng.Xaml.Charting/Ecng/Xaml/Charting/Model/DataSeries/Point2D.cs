// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Model.DataSeries.Point2D
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace StockSharp.Xaml.Charting.Model.DataSeries
{
    public struct Point2D : IPoint
    {
        private readonly double _x;
        private readonly double _y;

        public Point2D( double x, double y )
        {
            this._x = x;
            this._y = y;
        }

        public override string ToString()
        {
            return string.Format( "X={0};Y={1}", ( object ) this._x, ( object ) this._y );
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
                return this._y;
            }
        }
    }
}
