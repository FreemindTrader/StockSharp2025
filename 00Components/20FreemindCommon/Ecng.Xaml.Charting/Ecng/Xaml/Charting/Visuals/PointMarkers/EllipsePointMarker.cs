// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.PointMarkers.EllipsePointMarker
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Collections.Generic;
using System.Windows;
namespace fx.Xaml.Charting
{
    public class EllipsePointMarker : BasePointMarker
    {
        private float _width;
        private float _height;

        protected override void DrawInternal( IRenderContext2D context, IEnumerable<Point> centers, IPen2D pen, IBrush2D brush )
        {
            context.DrawEllipses( pen, brush, centers, ( double ) this._width, ( double ) this._height );
        }

        protected override void DrawInternal( IRenderContext2D context, double x, double y, IPen2D pen, IBrush2D brush )
        {
            context.DrawEllipse( pen, brush, new Point( x, y ), ( double ) this._width, ( double ) this._height );
        }

        protected override void OnPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            this._width = ( float ) this.Width;
            this._height = ( float ) this.Height;
            base.OnPropertyChanged( d, e );
        }
    }
}
