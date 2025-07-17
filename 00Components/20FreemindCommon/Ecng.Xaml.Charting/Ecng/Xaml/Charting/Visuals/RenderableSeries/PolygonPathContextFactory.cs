// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.RenderableSeries.PolygonPathContextFactory
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using StockSharp.Xaml.Charting.Rendering.Common;

namespace StockSharp.Xaml.Charting.Visuals.RenderableSeries
{
    internal class PolygonPathContextFactory : IPathContextFactory
    {
        private readonly IRenderContext2D _renderContext;

        public PolygonPathContextFactory( IRenderContext2D renderContext )
        {
            this._renderContext = renderContext;
        }

        public IPathDrawingContext Begin( IPathColor pen, double startX, double startY )
        {
            return this._renderContext.BeginPolygon( ( IBrush2D ) pen, startX, startY, 0.0 );
        }
    }
}
