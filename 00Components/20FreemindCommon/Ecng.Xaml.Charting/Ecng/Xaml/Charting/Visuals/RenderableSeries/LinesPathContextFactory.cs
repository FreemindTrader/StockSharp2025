// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Visuals.RenderableSeries.LinesPathContextFactory
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using Ecng.Xaml.Charting.Rendering.Common;

namespace Ecng.Xaml.Charting.Visuals.RenderableSeries
{
    internal class LinesPathContextFactory : IPathContextFactory
    {
        private readonly IRenderContext2D _renderContext;

        public LinesPathContextFactory( IRenderContext2D renderContext )
        {
            this._renderContext = renderContext;
        }

        public IPathDrawingContext Begin( IPathColor pen, double startX, double startY )
        {
            return this._renderContext.BeginLine( ( IPen2D ) pen, startX, startY );
        }
    }
}
