// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.PolarMountainAreaPathContextFactory
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
namespace fx.Xaml.Charting
{
    internal class PolarMountainAreaPathContextFactory : IPathContextFactory, IPathDrawingContext, IDisposable
    {
        private readonly double _zeroCoord;
        private readonly List<double> _drawnXValues;
        private IPathDrawingContext _fillContext;
        private readonly IPathContextFactory _factory;

        public PolarMountainAreaPathContextFactory( IRenderContext2D renderContext, ITransformationStrategy transformationStrategy, double zeroCoord )
        {
            this._factory = SeriesDrawingHelpersFactory.NewPolarPolygonsFactory( renderContext, transformationStrategy );
            this._zeroCoord = zeroCoord;
            this._drawnXValues = new List<double>();
        }

        public IPathDrawingContext Begin( IPathColor brush, double startX, double startY )
        {
            this._fillContext = this._factory.Begin( brush, startX, startY );
            this._drawnXValues.Add( startX );
            return ( IPathDrawingContext ) this;
        }

        public IPathDrawingContext MoveTo( double x, double y )
        {
            this._fillContext.MoveTo( x, y );
            this._drawnXValues.Add( x );
            return ( IPathDrawingContext ) this;
        }

        public void End()
        {
            for ( int index = this._drawnXValues.Count - 1 ; index >= 0 ; --index )
                this._fillContext.MoveTo( this._drawnXValues[ index ], this._zeroCoord );
            this._fillContext.End();
            this._fillContext = ( IPathDrawingContext ) null;
            this._drawnXValues.Clear();
        }

        void IDisposable.Dispose()
        {
            this.End();
        }
    }
}
