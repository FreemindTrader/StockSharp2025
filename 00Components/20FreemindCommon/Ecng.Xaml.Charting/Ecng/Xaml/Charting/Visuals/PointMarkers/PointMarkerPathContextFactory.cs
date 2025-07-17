// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Visuals.PointMarkers.PointMarkerPathContextFactory
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using Ecng.Xaml.Charting.Rendering.Common;

namespace Ecng.Xaml.Charting.Visuals.PointMarkers
{
    internal class PointMarkerPathContextFactory : IPathContextFactory, IPathDrawingContext, IDisposable
    {
        private readonly IRenderContext2D _renderContext;
        private readonly IPointMarker _pointMarker;
        private IPen2D _defaultPen;
        private IBrush2D _defaultBrush;

        public PointMarkerPathContextFactory( IRenderContext2D renderContext, IPointMarker pointMarker )
        {
            this._renderContext = renderContext;
            this._pointMarker = pointMarker;
        }

        public IPathDrawingContext Begin( IPathColor pen, double startX, double startY )
        {
            if ( pen != null )
            {
                this._defaultPen = ( IPen2D ) pen;
                this._defaultBrush = this._renderContext.CreateBrush( pen.Color, 1.0, new bool?() );
            }
            else
            {
                this._defaultPen = ( IPen2D ) null;
                this._defaultBrush = ( IBrush2D ) null;
            }
            this._pointMarker.Begin( this._renderContext, this._defaultPen, this._defaultBrush );
            this._pointMarker.Draw( this._renderContext, startX, startY, this._defaultPen, this._defaultBrush );
            return ( IPathDrawingContext ) this;
        }

        public IPathDrawingContext MoveTo( double x, double y )
        {
            this._pointMarker.Draw( this._renderContext, x, y, this._defaultPen, this._defaultBrush );
            return ( IPathDrawingContext ) this;
        }

        public void End()
        {
            this._pointMarker.End( this._renderContext );
            this._defaultBrush = ( IBrush2D ) null;
            this._defaultPen = ( IPen2D ) null;
        }

        void IDisposable.Dispose()
        {
            this.End();
        }
    }
}
