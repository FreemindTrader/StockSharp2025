// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Model.DataSeries.OhlcPointSeries
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace StockSharp.Xaml.Charting.Model.DataSeries
{
    public class OhlcPointSeries : GenericPointSeriesBase<OhlcSeriesPoint>
    {
        private readonly IPointSeries _openPoints;
        private readonly IPointSeries _highPoints;
        private readonly IPointSeries _lowPoints;
        private readonly IPointSeries _closePoints;
        private bool _useXIndices;
        private int _baseIndex;
        private readonly UncheckedList<double> _xValues;
        private readonly UncheckedList<double> _openValues;
        private readonly UncheckedList<double> _highValues;
        private readonly UncheckedList<double> _lowValues;
        private readonly UncheckedList<double> _closeValues;

        public OhlcPointSeries( IPointSeries openPoints, IPointSeries highPoints, IPointSeries lowPoints, IPointSeries closePoints )
          : base( closePoints )
        {
            this._openPoints = openPoints;
            this._highPoints = highPoints;
            this._lowPoints = lowPoints;
            this._closePoints = closePoints;
            this._openValues = ( openPoints as IPoint2DListSeries )?.YValues;
            this._highValues = ( highPoints as IPoint2DListSeries )?.YValues;
            this._lowValues = ( lowPoints as IPoint2DListSeries )?.YValues;
            IPoint2DListSeries point2DlistSeries = closePoints as IPoint2DListSeries;
            if ( point2DlistSeries == null )
                return;
            this._xValues = point2DlistSeries.XValues;
            this._useXIndices = this._xValues == null;
            this._baseIndex = point2DlistSeries.XBaseIndex;
            this._closeValues = point2DlistSeries.YValues;
        }

        public override int Count
        {
            get
            {
                return this._closePoints.Count;
            }
        }

        public override IPoint this[ int index ]
        {
            get
            {
                return ( IPoint ) new GenericPoint2D<OhlcSeriesPoint>( this._useXIndices ? ( double ) ( index + this._baseIndex ) : ( this._xValues != null ? this._xValues[ index ] : this._closePoints[ index ].X ), new OhlcSeriesPoint( this._openValues != null ? this._openValues[ index ] : this._openPoints[ index ].Y, this._highValues != null ? this._highValues[ index ] : this._highPoints[ index ].Y, this._lowValues != null ? this._lowValues[ index ] : this._lowPoints[ index ].Y, this._closeValues != null ? this._closeValues[ index ] : this._closePoints[ index ].Y ) );
            }
        }

        public override DoubleRange GetYRange()
        {
            int count = this.Count;
            double min = double.MaxValue;
            double max = double.MinValue;
            for ( int index = 0 ; index < count ; ++index )
            {
                double d1 = this._highValues != null ? this._highValues[index] : this._highPoints[index].Y;
                double d2 = this._lowValues != null ? this._lowValues[index] : this._lowPoints[index].Y;
                if ( !double.IsNaN( d1 ) && !double.IsNaN( d2 ) )
                {
                    min = min < d2 ? min : d2;
                    max = max > d1 ? max : d1;
                    count = this._highPoints.Count;
                }
            }
            return new DoubleRange( min, max );
        }
    }
}
