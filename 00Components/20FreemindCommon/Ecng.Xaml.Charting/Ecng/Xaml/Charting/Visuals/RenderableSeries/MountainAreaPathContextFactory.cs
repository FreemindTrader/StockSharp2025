// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.MountainAreaPathContextFactory
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
namespace Ecng.Xaml.Charting
{
    internal class MountainAreaPathContextFactory : IPathContextFactory, IPathDrawingContext, IDisposable
    {
        private readonly bool _drawToZeroLine = true;
        private readonly IRenderContext2D _renderContext;
        private IPathDrawingContext _fillContext;
        private readonly bool _isVerticalChart;
        private readonly double _gradientRotationAngle;
        private readonly double _zeroCoord;
        private double _firstX;
        private double _firstY;
        private double _lastX;
        private double _lastY;

        public MountainAreaPathContextFactory( IRenderContext2D renderContext, bool isVerticalChart, double zeroCoord, double gradientRotationAngle )
          : this( renderContext, isVerticalChart, true, zeroCoord, gradientRotationAngle )
        {
        }

        public MountainAreaPathContextFactory( IRenderContext2D renderContext, bool isVerticalChart, double gradientRotationAngle )
          : this( renderContext, isVerticalChart, false, 0.0, gradientRotationAngle )
        {
        }

        protected MountainAreaPathContextFactory( IRenderContext2D renderContext, bool isVerticalChart, bool drawToZeroline, double zeroCoord, double gradientRotationAngle )
        {
            this._renderContext = renderContext;
            this._isVerticalChart = isVerticalChart;
            this._drawToZeroLine = drawToZeroline;
            this._zeroCoord = zeroCoord;
            this._gradientRotationAngle = gradientRotationAngle;
        }

        public IPathDrawingContext Begin( IPathColor brush, double startX, double startY )
        {
            this._firstX = startX;
            this._firstY = startY;
            this._fillContext = this._renderContext.BeginPolygon( ( IBrush2D ) brush, startX, startY, this._gradientRotationAngle );
            return ( IPathDrawingContext ) this;
        }

        public void End()
        {
            if ( this._drawToZeroLine )
            {
                if ( this._isVerticalChart )
                {
                    this._fillContext.MoveTo( this._zeroCoord, this._lastY );
                    this._fillContext.MoveTo( this._zeroCoord, this._firstY );
                }
                else
                {
                    this._fillContext.MoveTo( this._lastX, this._zeroCoord );
                    this._fillContext.MoveTo( this._firstX, this._zeroCoord );
                }
            }
            this._fillContext.MoveTo( this._firstX, this._firstY );
            this._fillContext.End();
            this._fillContext = ( IPathDrawingContext ) null;
        }

        public IPathDrawingContext MoveTo( double x, double y )
        {
            this._lastX = x;
            this._lastY = y;
            this._fillContext.MoveTo( x, y );
            return ( IPathDrawingContext ) this;
        }

        void IDisposable.Dispose()
        {
            this.End();
        }
    }
}
