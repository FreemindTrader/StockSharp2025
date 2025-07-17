// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.PolarPathDrawingDecorator
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Windows;
namespace Ecng.Xaml.Charting
{
    internal class PolarPathDrawingDecorator : IPathDrawingContext, IDisposable
    {
        private IPathDrawingContext _drawingContext;
        private readonly IPathContextFactory _factory;
        private readonly ITransformationStrategy _transformationStrategy;
        private Point _lastPoint;
        private const double AdditionalSegmentLength = 4.0;

        public PolarPathDrawingDecorator( IPathContextFactory factory, ITransformationStrategy transformationStrategy )
        {
            _factory = factory;
            _transformationStrategy = transformationStrategy;
        }

        public IPathDrawingContext Begin( IPathColor color, double x, double y )
        {
            _lastPoint = new Point( x, y );
            Point point = TransformPoint(_lastPoint);
            _drawingContext = _factory.Begin( color, point.X, point.Y );
            return ( IPathDrawingContext ) this;
        }

        private Point TransformPoint( Point point )
        {
            return _transformationStrategy.ReverseTransform( point );
        }

        public IPathDrawingContext MoveTo( double x, double y )
        {
            DrawCurveTo( new Point( x, y ) );
            return ( IPathDrawingContext ) this;
        }

        private void DrawCurveTo( Point currentPoint )
        {
            DrawAdditionalPoints( _lastPoint, currentPoint );
            DrawPoint( currentPoint );
            _lastPoint = currentPoint;
        }

        private void DrawAdditionalPoints( Point startPoint, Point endPoint )
        {
            foreach ( Point additionalDrawingPoint in PolarPathDrawingDecorator.CalculateAdditionalDrawingPoints( startPoint, endPoint ) )
                DrawPoint( additionalDrawingPoint );
        }

        private static IEnumerable<Point> CalculateAdditionalDrawingPoints( Point pt1, Point pt2 )
        {
            int pointsAmount = PolarPathDrawingDecorator.CalculateAmountOfAdditionalDrawingPoints(pt1, pt2);
            if ( pointsAmount > 0 )
            {
                double x1 = pt1.X;
                double x = pt2.X;
                double y1 = pt1.Y;
                double y = pt2.Y;
                int num = pointsAmount + 1;
                double xDelta = (x - x1) / (double) num;
                double yDelta = (y - y1) / (double) num;
                for ( int i = 0 ; i < pointsAmount ; ++i )
                {
                    x1 += xDelta;
                    y1 += yDelta;
                    yield return new Point( x1, y1 );
                }
            }
        }

        private static int CalculateAmountOfAdditionalDrawingPoints( Point pt1, Point pt2 )
        {
            double num = Math.Abs(pt1.X - pt2.X);
            return ( int ) ( Math.PI * ( ( Math.Abs( pt1.Y ) + Math.Abs( pt2.Y ) ) / 2.0 ) * ( num / 180.0 ) / 4.0 );
        }

        private void DrawPoint( Point point )
        {
            Point point1 = _transformationStrategy.ReverseTransform(point);
            _drawingContext.MoveTo( point1.X, point1.Y );
        }

        public void End()
        {
            if ( _drawingContext == null )
                return;
            _drawingContext.End();
            _drawingContext = ( IPathDrawingContext ) null;
        }

        public void Dispose()
        {
            End();
        }
    }
}
