// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.PointMarkers.CrossPointMarker
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Collections.Generic;
using System.Windows;
using StockSharp.Xaml.Charting.Rendering.Common;

namespace StockSharp.Xaml.Charting.Visuals.PointMarkers
{
    public class CrossPointMarker : BasePointMarker
    {
        private float _halfWidth;
        private float _halfHeight;

        protected override void DrawInternal( IRenderContext2D context, IEnumerable<Point> centers, IPen2D pen, IBrush2D brush )
        {
            foreach ( Point center in centers )
                this.DrawInternal( context, center.X, center.Y, pen, brush );
        }

        protected override void DrawInternal( IRenderContext2D context, double x, double y, IPen2D pen, IBrush2D brush )
        {
            context.DrawLine( pen, new Point( x - ( double ) this._halfWidth, y ), new Point( x + ( double ) this._halfWidth, y ) );
            context.DrawLine( pen, new Point( x, y - ( double ) this._halfHeight ), new Point( x, y + ( double ) this._halfHeight ) );
        }

        protected override void OnPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            this._halfWidth = ( float ) ( this.Width * 0.5 );
            this._halfHeight = ( float ) ( this.Height * 0.5 );
            base.OnPropertyChanged( d, e );
        }
    }
}
