// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.PolarLinesPathContextFactory
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace fx.Xaml.Charting
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
