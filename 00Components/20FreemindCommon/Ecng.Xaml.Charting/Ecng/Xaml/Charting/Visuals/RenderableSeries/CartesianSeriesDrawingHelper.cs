// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Visuals.RenderableSeries.CartesianSeriesDrawingHelper
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Collections.Generic;
using System.Windows;
using Ecng.Xaml.Charting.Rendering.Common;
using Ecng.Xaml.Charting.Visuals.PointMarkers;

namespace Ecng.Xaml.Charting.Visuals.RenderableSeries
{
    internal class CartesianSeriesDrawingHelper : ISeriesDrawingHelper
    {
        private readonly IRenderContext2D _renderContext;

        public CartesianSeriesDrawingHelper( IRenderContext2D renderContext )
        {
            this._renderContext = renderContext;
        }

        public void DrawQuad( IPen2D pen, Point pt1, Point pt2 )
        {
            this._renderContext.DrawQuad( pen, pt1, pt2 );
        }

        public void DrawBox( Point pt1, Point pt2, IBrush2D pointFill, IPen2D pointPen, double gradientRotationAngle )
        {
            this._renderContext.FillRectangle( pointFill, pt1, pt2, gradientRotationAngle );
            if ( ( double ) pointPen.StrokeThickness <= 0.0 || pointPen.Color.A == ( byte ) 0 )
                return;
            this._renderContext.DrawQuad( pointPen, pt1, pt2 );
        }

        public void DrawLine( Point pt1, Point pt2, IPen2D pointPen )
        {
            if ( ( double ) pointPen.StrokeThickness <= 0.0 || pointPen.Color.A == ( byte ) 0 )
                return;
            this._renderContext.DrawLine( pointPen, pt1, pt2 );
        }

        public void FillPolygon( IBrush2D fillBrush, Point[ ] points )
        {
            this._renderContext.FillPolygon( fillBrush, ( IEnumerable<Point> ) points );
        }

        public void DrawPoint( IPointMarker pt, Point point, IBrush2D brush, IPen2D pen )
        {
            pt.Draw( this._renderContext, point.X, point.Y, pen, brush );
        }

        public void DrawPoints( IPointMarker pt, IEnumerable<Point> points )
        {
            pt.Draw( this._renderContext, points );
        }
    }
}
