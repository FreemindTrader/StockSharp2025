// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.PointMarkers.TrianglePointMarker
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Collections.Generic;
using System.Windows;
namespace Ecng.Xaml.Charting
{
    public class TrianglePointMarker : BasePointMarker
    {
        private float _width2;
        private float _height2;

        protected override void DrawInternal( IRenderContext2D context, IEnumerable<Point> centers, IPen2D pen, IBrush2D brush )
        {
            foreach ( Point center in centers )
                this.DrawInternal( context, center.X, center.Y, pen, brush );
        }

        protected override void DrawInternal( IRenderContext2D context, double x, double y, IPen2D pen, IBrush2D brush )
        {
            Point[] pointArray = new Point[4]{ new Point(x - (double) this._width2, y - (double) this._height2), new Point(x + (double) this._width2, y - (double) this._height2), new Point(x, y + (double) this._height2), new Point(x - (double) this._width2, y - (double) this._height2) };
            context.FillPolygon( brush, ( IEnumerable<Point> ) pointArray );
            context.DrawLines( pen, ( IEnumerable<Point> ) pointArray );
        }

        protected override void OnPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            this._width2 = ( float ) ( this.Width * 0.5 );
            this._height2 = ( float ) ( this.Height * 0.5 );
            base.OnPropertyChanged( d, e );
        }
    }
}
