// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.PointMarkers.PolarPointDrawingDecorator
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Windows;
namespace fx.Xaml.Charting
{
    internal class PolarPointDrawingDecorator : IPathDrawingContext, IDisposable
    {
        private readonly IPathContextFactory _factory;
        private readonly ITransformationStrategy _transformationStrategy;
        private readonly Size _viewportSize;
        private IPathDrawingContext _drawingContext;
        private IPathColor _lastColor;

        public PolarPointDrawingDecorator( IPathContextFactory factory, ITransformationStrategy transformationStrategy, Size viewportSize )
        {
            this._factory = factory;
            this._transformationStrategy = transformationStrategy;
            this._viewportSize = viewportSize;
        }

        public IPathDrawingContext Begin( IPathColor color, double x, double y )
        {
            this._lastColor = color;
            this.DrawPoint( x, y );
            return ( IPathDrawingContext ) this;
        }

        private void DrawPoint( double x, double y )
        {
            Point point1 = new Point(x, y);
            if ( !point1.IsInBounds( this._viewportSize ) )
                return;
            Point point2 = this._transformationStrategy.ReverseTransform(point1);
            if ( this._drawingContext != null )
                this._drawingContext.MoveTo( point2.X, point2.Y );
            else
                this._drawingContext = this._factory.Begin( this._lastColor, point2.X, point2.Y );
        }

        public IPathDrawingContext MoveTo( double x, double y )
        {
            this.DrawPoint( x, y );
            return ( IPathDrawingContext ) this;
        }

        public void End()
        {
            if ( this._drawingContext == null )
                return;
            this._drawingContext.End();
            this._drawingContext = ( IPathDrawingContext ) null;
        }

        public void Dispose()
        {
            this.End();
        }
    }
}
