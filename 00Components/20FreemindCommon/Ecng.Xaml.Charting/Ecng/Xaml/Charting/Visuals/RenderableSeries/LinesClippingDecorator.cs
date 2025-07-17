// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.LinesClippingDecorator
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Media.Imaging;
namespace Ecng.Xaml.Charting
{
    internal class LinesClippingDecorator : IPathDrawingContext, IDisposable
    {
        private bool _wasValid = true;
        private IPathColor _lastColor;
        private readonly IPathContextFactory _factory;
        private readonly Rect _viewportRect;
        private double _lastX;
        private double _lastY;
        private IPathDrawingContext _context;
        private Size _viewportSize;

        public LinesClippingDecorator( IPathContextFactory factory, Size viewportSize )
        {
            this._factory = factory;
            this._viewportRect = new Rect( new Point( 0.0, 0.0 ), viewportSize );
            this._viewportSize = viewportSize;
        }

        public IPathDrawingContext Begin( IPathColor color, double x, double y )
        {
            this._lastColor = color;
            this._lastX = x;
            this._lastY = y;
            this._context = ( IPathDrawingContext ) null;
            return ( IPathDrawingContext ) this;
        }

        public IPathDrawingContext MoveTo( double x, double y )
        {
            bool flag = LinesClippingDecorator.IsInBounds(x, y, this._viewportSize);
            if ( this._context != null )
            {
                if ( flag )
                {
                    this._context.MoveTo( x, y );
                }
                else
                {
                    double lastX = this._lastX;
                    double lastY = this._lastY;
                    double x1 = x;
                    double y1 = y;
                    WriteableBitmapExtensions.CohenSutherlandLineClip( this._viewportRect, ref lastX, ref lastY, ref x1, ref y1 );
                    this._context.MoveTo( x1, y1 );
                    this.End();
                }
            }
            else
            {
                double lastX = this._lastX;
                double lastY = this._lastY;
                double x1 = x;
                double y1 = y;
                if ( flag )
                {
                    WriteableBitmapExtensions.CohenSutherlandLineClip( this._viewportRect, ref lastX, ref lastY, ref x1, ref y1 );
                    this._context = this._factory.Begin( this._lastColor, lastX, lastY );
                    this._context.MoveTo( x1, y1 );
                }
                else if ( WriteableBitmapExtensions.CohenSutherlandLineClip( this._viewportRect, ref lastX, ref lastY, ref x1, ref y1 ) )
                {
                    this._context = this._factory.Begin( this._lastColor, lastX, lastY );
                    this._context.MoveTo( x1, y1 );
                    this.End();
                }
            }
            this._lastX = x;
            this._lastY = y;
            return ( IPathDrawingContext ) this;
        }

        public void End()
        {
            if ( this._context == null )
                return;
            this._context.End();
            this._context = ( IPathDrawingContext ) null;
        }

        public void Dispose()
        {
            this.End();
        }

        private static bool IsInBounds( double x, double y, Size viewportSize )
        {
            if ( x >= 0.0 && x < viewportSize.Width && y >= 0.0 )
                return y < viewportSize.Height;
            return false;
        }
    }
}
