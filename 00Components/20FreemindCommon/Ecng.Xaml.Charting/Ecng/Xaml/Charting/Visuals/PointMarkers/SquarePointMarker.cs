// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.PointMarkers.SquarePointMarker
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Collections.Generic;
using System.Windows;
using StockSharp.Xaml.Charting.Rendering.Common;

namespace StockSharp.Xaml.Charting.Visuals.PointMarkers
{
    public class SquarePointMarker : BasePointMarker
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
            context.FillRectangle( brush, new Point( x - ( double ) this._width2, y - ( double ) this._height2 ), new Point( x + ( double ) this._width2, y + ( double ) this._height2 ), 0.0 );
            context.DrawQuad( pen, new Point( x - ( double ) this._width2, y - ( double ) this._height2 ), new Point( x + ( double ) this._width2, y + ( double ) this._height2 ) );
        }

        protected override void OnPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            this._width2 = ( float ) ( this.Width * 0.5 );
            this._height2 = ( float ) ( this.Height * 0.5 );
            base.OnPropertyChanged( d, e );
        }
    }
}
