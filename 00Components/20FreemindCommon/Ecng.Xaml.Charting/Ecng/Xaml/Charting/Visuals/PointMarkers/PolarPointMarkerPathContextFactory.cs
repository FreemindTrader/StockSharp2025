// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.PointMarkers.PolarPointMarkerPathContextFactory
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Windows;
namespace fx.Xaml.Charting
{
    internal class PolarPointMarkerPathContextFactory : IPathContextFactory
    {
        private readonly PolarPointDrawingDecoratorFactory _factory;

        public PolarPointMarkerPathContextFactory( IRenderContext2D renderContext, ITransformationStrategy transformationStrategy, IPointMarker pointMarker )
        {
            Size viewportSize = new Size(360.0, PolarUtil.CalculateViewportRadius(renderContext.ViewportSize));
            this._factory = new PolarPointDrawingDecoratorFactory( ( IPathContextFactory ) new PointMarkerPathContextFactory( renderContext, pointMarker ), transformationStrategy, viewportSize );
        }

        public IPathDrawingContext Begin( IPathColor color, double startX, double startY )
        {
            return this._factory.Begin( color, startX, startY );
        }
    }
}
