// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Model.DataSeries.BoxPointSeries
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\StockSharp.Xaml.Charting.dll

namespace StockSharp.Xaml.Charting.Model.DataSeries
{
    public class BoxPointSeries : GenericPointSeriesBase<BoxSeriesPoint>
    {
        private readonly IPointSeries _yPoints;
        private readonly IPointSeries _minPoints;
        private readonly IPointSeries _lowerPoints;
        private readonly IPointSeries _upperPoints;
        private readonly IPointSeries _maxPoints;

        public BoxPointSeries( IPointSeries yPoints, IPointSeries minPoints, IPointSeries lowerPoints, IPointSeries upperPoints, IPointSeries maxPoints )
          : base( yPoints )
        {
            this._yPoints = yPoints;
            this._minPoints = minPoints;
            this._lowerPoints = lowerPoints;
            this._upperPoints = upperPoints;
            this._maxPoints = maxPoints;
        }

        public override int Count
        {
            get
            {
                return this._yPoints.Count;
            }
        }

        public override IPoint this[ int index ]
        {
            get
            {
                return ( IPoint ) new GenericPoint2D<BoxSeriesPoint>( this._yPoints[ index ].X, new BoxSeriesPoint( this._yPoints[ index ].Y, this._minPoints[ index ].Y, this._lowerPoints[ index ].Y, this._upperPoints[ index ].Y, this._maxPoints[ index ].Y ) );
            }
        }

        public override DoubleRange GetYRange()
        {
            int count = this.Count;
            double min = double.MaxValue;
            double max = double.MinValue;
            for ( int index = 0 ; index < count ; ++index )
            {
                double y1 = this._maxPoints[index].Y;
                double y2 = this._minPoints[index].Y;
                if ( !double.IsNaN( y1 ) && !double.IsNaN( y2 ) )
                {
                    min = min < y2 ? min : y2;
                    max = max > y1 ? max : y1;
                    count = this._minPoints.Count;
                }
            }
            return new DoubleRange( min, max );
        }
    }
}
