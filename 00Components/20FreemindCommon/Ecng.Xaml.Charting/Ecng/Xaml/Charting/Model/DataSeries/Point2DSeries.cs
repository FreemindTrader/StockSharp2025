// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Model.DataSeries.Point2DSeries
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace fx.Xaml.Charting
{
    public class Point2DSeries : IPoint2DListSeries, IPointSeries
    {
        private UltraList<double> xValues;
        private UltraList<double> yValues;
        private double[] _xItems;
        private double[] _yItems;
        private bool _isFrozen;

        public Point2DSeries( int capacity )
        {
            this.xValues = new UltraList<double>( capacity );
            this.yValues = new UltraList<double>( capacity );
        }

        public DoubleRange GetYRange()
        {
            double min;
            double max;
            ArrayOperations.MinMax<double>( this.yValues.ItemsArray, 0, this.yValues.Count, out min, out max );
            return new DoubleRange( min, max );
        }

        public int XBaseIndex
        {
            get
            {
                return 0;
            }
        }

        public void Add( Point2D value )
        {
            this.xValues.Add( value.X );
            this.yValues.Add( value.Y );
        }

        IPoint IPointSeries.this[ int index ]
        {
            get
            {
                return ( IPoint ) new Point2D( this._xItems[ index ], this._yItems[ index ] );
            }
        }

        public IUltraList<double> XValues
        {
            get
            {
                return ( IUltraList<double> ) this.xValues;
            }
        }

        public IUltraList<double> YValues
        {
            get
            {
                return ( IUltraList<double> ) this.yValues;
            }
        }

        UncheckedList<double> IPoint2DListSeries.YValues
        {
            get
            {
                return new UncheckedList<double>( this.yValues.ItemsArray );
            }
        }

        UncheckedList<double> IPoint2DListSeries.XValues
        {
            get
            {
                return new UncheckedList<double>( this.xValues.ItemsArray, 0, this.xValues.Count );
            }
        }

        public int Count
        {
            get
            {
                return this.yValues.Count;
            }
        }

        public void Freeze()
        {
            this._xItems = this.xValues.ToUncheckedList<double>();
            this._yItems = this.yValues.ToUncheckedList<double>();
        }
    }
}
