// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.PolarSeriesDrawingHelper
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
namespace fx.Xaml.Charting
{
    internal class PolarSeriesDrawingHelper : ISeriesDrawingHelper
    {
        private readonly IRenderContext2D _renderContext;
        private readonly ITransformationStrategy _transformationStrategy;
        private readonly IPathContextFactory _polygonsFactory;
        private readonly IPathContextFactory _linesFactory;
        private readonly Size _viewportSize;

        public PolarSeriesDrawingHelper( IRenderContext2D renderContext, ITransformationStrategy transformationStrategy )
        {
            this._renderContext = renderContext;
            this._transformationStrategy = transformationStrategy;
            this._viewportSize = PolarUtil.CalculatePolarViewportSize( renderContext.ViewportSize );
            this._linesFactory = SeriesDrawingHelpersFactory.NewPolarLinesFactory( renderContext, transformationStrategy );
            this._polygonsFactory = SeriesDrawingHelpersFactory.NewPolarPolygonsFactory( renderContext, transformationStrategy );
        }

        public void DrawQuad( IPen2D pen, Point pt1, Point pt2 )
        {
        }

        public void DrawBox( Point leftTop, Point rightBottom, IBrush2D pointFill, IPen2D pointPen, double gradientRotationAngle )
        {
            using ( IPathDrawingContext pathDrawingContext = this._polygonsFactory.Begin( ( IPathColor ) pointFill, leftTop.X, leftTop.Y ) )
            {
                pathDrawingContext.MoveTo( rightBottom.X, leftTop.Y );
                pathDrawingContext.MoveTo( rightBottom.X, rightBottom.Y );
                pathDrawingContext.MoveTo( leftTop.X, rightBottom.Y );
                pathDrawingContext.MoveTo( leftTop.X, leftTop.Y );
            }
            if ( ( double ) pointPen.StrokeThickness <= 0.0 || pointPen.Color.A == ( byte ) 0 )
                return;
            using ( IPathDrawingContext pathDrawingContext = this._linesFactory.Begin( ( IPathColor ) pointPen, leftTop.X, leftTop.Y ) )
            {
                pathDrawingContext.MoveTo( rightBottom.X, leftTop.Y );
                pathDrawingContext.MoveTo( rightBottom.X, rightBottom.Y );
                pathDrawingContext.MoveTo( leftTop.X, rightBottom.Y );
                pathDrawingContext.MoveTo( leftTop.X, leftTop.Y );
            }
        }

        public void DrawLine( Point pt1, Point pt2, IPen2D pointPen )
        {
            if ( ( double ) pointPen.StrokeThickness <= 0.0 || pointPen.Color.A == ( byte ) 0 )
                return;
            using ( IPathDrawingContext pathDrawingContext = this._linesFactory.Begin( ( IPathColor ) pointPen, pt1.X, pt1.Y ) )
                pathDrawingContext.MoveTo( pt2.X, pt2.Y );
        }

        public void FillPolygon( IBrush2D fillBrush, Point[ ] points )
        {
            Point point1 = ((IEnumerable<Point>) points).First<Point>();
            using ( IPathDrawingContext pathDrawingContext = this._polygonsFactory.Begin( ( IPathColor ) fillBrush, point1.X, point1.Y ) )
            {
                for ( int index = 1 ; index < points.Length ; ++index )
                {
                    Point point2 = points[index];
                    pathDrawingContext.MoveTo( point2.X, point2.Y );
                }
            }
        }

        public void DrawPoint( IPointMarker pt, Point point, IBrush2D brush, IPen2D pen )
        {
            if ( !point.IsInBounds( this._viewportSize ) )
                return;
            point = this.TransformPoint( point );
            pt.Draw( this._renderContext, point.X, point.Y, pen, brush );
        }

        public void DrawPoints( IPointMarker pt, IEnumerable<Point> points )
        {
            IEnumerable<Point> centers = points.Where<Point>((Func<Point, bool>) (p => p.IsInBounds(this._viewportSize))).Select<Point, Point>(new Func<Point, Point>(this.TransformPoint));
            pt.Draw( this._renderContext, centers );
        }

        private Point TransformPoint( Point point )
        {
            return this._transformationStrategy.ReverseTransform( point );
        }
    }
}
