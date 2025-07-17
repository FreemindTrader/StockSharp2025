// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Model.DataSeries.XyzPointSeries
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\StockSharp.Xaml.Charting.dll

namespace StockSharp.Xaml.Charting.Model.DataSeries
{
    public class XyzPointSeries : GenericPointSeriesBase<XyySeriesPoint>
    {
        private readonly IPointSeries _yPoints;
        private readonly IPointSeries _zPoints;

        public XyzPointSeries( IPointSeries yPoints, IPointSeries zPoints )
          : base( yPoints )
        {
            _yPoints = yPoints;
            _zPoints = zPoints;
        }

        public override int Count
        {
            get
            {
                return _yPoints.Count;
            }
        }

        public new IPointSeries YPoints
        {
            get
            {
                return _yPoints;
            }
        }

        public IPointSeries ZPoints
        {
            get
            {
                return _zPoints;
            }
        }

        public override IPoint this[ int index ]
        {
            get
            {
                return ( IPoint ) new GenericPoint2D<XyzSeriesPoint>( _yPoints[ index ].X, new XyzSeriesPoint( _yPoints != null ? _yPoints[ index ].Y : 0.0, _zPoints[ index ].Y ) );
            }
        }

        public override DoubleRange GetYRange()
        {
            int count = Count;
            double min = double.MaxValue;
            double max = double.MinValue;
            for ( int index = 0 ; index < count ; ++index )
            {
                double y = _yPoints[index].Y;
                if ( !double.IsNaN( y ) )
                {
                    min = min < y ? min : y;
                    max = max > y ? max : y;
                }
            }
            return new DoubleRange( min, max );
        }
    }
}
