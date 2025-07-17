// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Model.DataSeries.HlcPointSeries
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace Ecng.Xaml.Charting
{
    public class HlcPointSeries : GenericPointSeriesBase<HlcSeriesPoint>
    {
        private readonly IPointSeries _yErrorHighPoints;
        private readonly IPointSeries _yErrorLowPoints;

        public HlcPointSeries( IPointSeries yPoints, IPointSeries yErrorHighPoints, IPointSeries yErrorLowPoints )
          : base( yPoints )
        {
            this._yErrorHighPoints = yErrorHighPoints;
            this._yErrorLowPoints = yErrorLowPoints;
        }

        public override int Count
        {
            get
            {
                return this.YPoints.Count;
            }
        }

        public override IPoint this[ int index ]
        {
            get
            {
                return ( IPoint ) new GenericPoint2D<HlcSeriesPoint>( this.YPoints[ index ].X, new HlcSeriesPoint( this.YPoints[ index ].Y, this._yErrorHighPoints[ index ].Y, this._yErrorLowPoints[ index ].Y ) );
            }
        }

        public override DoubleRange GetYRange()
        {
            int count = this.Count;
            double min = double.MaxValue;
            double max = double.MinValue;
            for ( int index = 0 ; index < count ; ++index )
            {
                double y1 = this._yErrorHighPoints[index].Y;
                double y2 = this._yErrorLowPoints[index].Y;
                if ( !double.IsNaN( y1 ) && !double.IsNaN( y2 ) )
                {
                    min = min < y2 ? min : y2;
                    max = max > y1 ? max : y1;
                    count = this._yErrorHighPoints.Count;
                }
            }
            return new DoubleRange( min, max );
        }
    }
}
