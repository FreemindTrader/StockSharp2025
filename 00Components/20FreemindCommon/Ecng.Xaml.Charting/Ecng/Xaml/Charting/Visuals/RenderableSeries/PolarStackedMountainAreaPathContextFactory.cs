// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.PolarStackedMountainAreaPathContextFactory
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace Ecng.Xaml.Charting
{
    internal class PolarStackedMountainAreaPathContextFactory : IPathContextFactory
    {
        private readonly IPathContextFactory _factory;

        public PolarStackedMountainAreaPathContextFactory( IRenderContext2D renderContext, ITransformationStrategy transformationStrategy )
        {
            this._factory = SeriesDrawingHelpersFactory.NewPolarPolygonsFactory( renderContext, transformationStrategy );
        }

        public IPathDrawingContext Begin( IPathColor color, double startX, double startY )
        {
            return this._factory.Begin( color, startX, startY );
        }
    }
}
