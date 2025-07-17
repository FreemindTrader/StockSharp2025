// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Model.DataSeries.XyyPointSeries
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;

namespace Ecng.Xaml.Charting
{
    public class XyyPointSeries : GenericPointSeriesBase<XyySeriesPoint>
    {
        private readonly IPointSeries _yPoints;
        private readonly IPointSeries _y1Points;

        public XyyPointSeries( IPointSeries yPoints, IPointSeries y1Points )
          : base( yPoints )
        {
            _yPoints = yPoints;
            _y1Points = y1Points;
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

        public IPointSeries Y1Points
        {
            get
            {
                return _y1Points;
            }
        }

        public override IPoint this[ int index ]
        {
            get
            {
                return ( IPoint ) new GenericPoint2D<XyySeriesPoint>( _yPoints[ index ].X, new XyySeriesPoint( _yPoints != null ? _yPoints[ index ].Y : 0.0, _y1Points[ index ].Y ) );
            }
        }

        public override DoubleRange GetYRange()
        {
            int count  = Count;
            double min = double.MaxValue;
            double max = double.MinValue;

            for ( int index = 0 ; index < count ; ++index )
            {
                double y1 = _y1Points[index].Y;
                double y2 = _yPoints[index].Y;
                if ( !double.IsNaN( y2 ) && !double.IsNaN( y1 ) )
                {
                    double num1 = Math.Min(y1, y2);
                    double num2 = Math.Max(y1, y2);
                    min = min < num1 ? min : num1;
                    max = max > num2 ? max : num2;
                }
            }
            return new DoubleRange( min, max );
        }
    }
}
