// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.RenderableSeries.PolarLinesPathContextFactory
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using StockSharp.Xaml.Charting.Rendering.Common;
using StockSharp.Xaml.Charting.StrategyManager;

namespace StockSharp.Xaml.Charting.Visuals.RenderableSeries
{
    internal class PolarLinesPathContextFactory : IPathContextFactory
    {
        private readonly IPathContextFactory _factory;

        public PolarLinesPathContextFactory( IRenderContext2D renderContext, ITransformationStrategy transformationStrategy )
        {
            this._factory = SeriesDrawingHelpersFactory.NewPolarLinesFactory( renderContext, transformationStrategy );
        }

        public IPathDrawingContext Begin( IPathColor pen, double x, double y )
        {
            return this._factory.Begin( pen, x, y );
        }
    }
}
