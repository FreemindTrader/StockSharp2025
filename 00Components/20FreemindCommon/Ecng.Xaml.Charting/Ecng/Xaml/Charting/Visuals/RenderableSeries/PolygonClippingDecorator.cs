// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.PolygonClippingDecorator
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Windows;
namespace fx.Xaml.Charting
{
    internal class PolygonClippingDecorator : IPathDrawingContext, IDisposable
    {
        private readonly List<Point> _points;
        private IPathColor _lastColor;
        private readonly IPathContextFactory _factory;
        private readonly Size _viewportSize;

        public PolygonClippingDecorator( IPathContextFactory factory, Size viewportSize )
        {
            _factory = factory;
            _viewportSize = viewportSize;
            _points = new List<Point>();
        }

        public IPathDrawingContext Begin( IPathColor color, double x, double y )
        {
            _points.Add( new Point( x, y ) );
            _lastColor = color;
            return ( IPathDrawingContext ) this;
        }

        public IPathDrawingContext MoveTo( double x, double y )
        {
            _points.Add( new Point( x, y ) );
            return ( IPathDrawingContext ) this;
        }

        public void End()
        {
            if ( _points.Count <= 0 )
                return;
            DrawingHelper.DrawPoints( PointUtil.ClipPolygon( ( IEnumerable<Point> ) _points, _viewportSize, 0, 0 ), _factory, _lastColor );
        }

        public void Dispose()
        {
            End();
        }
    }
}
