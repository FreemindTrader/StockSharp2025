// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.PointMarkers.PolarPointDrawingDecoratorFactory
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Windows;
using StockSharp.Xaml.Charting.Rendering.Common;
using StockSharp.Xaml.Charting.StrategyManager;

namespace StockSharp.Xaml.Charting.Visuals.PointMarkers
{
    internal class PolarPointDrawingDecoratorFactory : IPathContextFactory
    {
        private readonly IPathContextFactory _factory;
        private readonly ITransformationStrategy _transformationStrategy;
        private readonly Size _viewportSize;

        public PolarPointDrawingDecoratorFactory( IPathContextFactory factory, ITransformationStrategy transformationStrategy, Size viewportSize )
        {
            this._factory = factory;
            this._transformationStrategy = transformationStrategy;
            this._viewportSize = viewportSize;
        }

        public IPathDrawingContext Begin( IPathColor color, double startX, double startY )
        {
            return new PolarPointDrawingDecorator( this._factory, this._transformationStrategy, this._viewportSize ).Begin( color, startX, startY );
        }
    }
}
