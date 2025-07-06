using SciChart.Drawing.Common;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Windows.Media;

namespace StockSharp.Xaml.Charting
{
    internal interface IPenManager : IDisposable
    {
        IPen2D GetPen( Color? nullable_0 );

        IBrush2D GetSolidBrush( Color? nullable_0 );

        IBrush2D GetTextureBrush( Brush brush_0 );
    }

    internal class PenManager : IPenManager, IDisposable
    {
        private readonly IRenderContext2D _renderContext;
        private readonly bool _antiAliasing;
        private readonly float _strokeThickness;
        private readonly double _opacity;
        private readonly double[ ] _strokeDashArray;
        private readonly PooledDictionary< Color, IPen2D > _2DColorPen;
        private readonly PooledDictionary< Color, IBrush2D > _2DColorBrush;
        private readonly PooledDictionary< Brush, IBrush2D > _2DTextureBrush;

        public PenManager( IRenderContext2D renderContext, bool antiAliasing, float strokeThickness, double opacity, double[ ] strokeDashArray = null )
        {
            _renderContext = renderContext;
            _antiAliasing = antiAliasing;
            _strokeThickness = strokeThickness;
            _opacity = opacity;
            _strokeDashArray = strokeDashArray;
            _2DColorPen = new PooledDictionary< Color, IPen2D >( );
            _2DColorBrush = new PooledDictionary< Color, IBrush2D >( );
            _2DTextureBrush = new PooledDictionary< Brush, IBrush2D >( );
        }

        public IPen2D GetPen( Color? nullable_0 )
        {
            if( !nullable_0.HasValue )
            {
                return null;
            }
            return GetPen( nullable_0.Value );
        }

        public IPen2D GetPen( Color myColor )
        {
            IPen2D pen2D = null;

            if( !_2DColorPen.TryGetValue( myColor, out pen2D ) )
            {
                pen2D = _renderContext.CreatePen( myColor, _antiAliasing, _strokeThickness, _opacity, _strokeDashArray, PenLineCap.Round );
                _2DColorPen.Add( myColor, pen2D );
            }
            return pen2D;
        }

        public IBrush2D GetSolidBrush( Color? myColor )
        {
            IBrush2D brush2D = null;
            if( myColor.HasValue && !_2DColorBrush.TryGetValue( myColor.Value, out brush2D ) )
            {
                brush2D = _renderContext.CreateBrush( myColor.Value, _opacity, new bool?( ) );
                _2DColorBrush.Add( myColor.Value, brush2D );
            }
            return brush2D;
        }

        public IBrush2D GetTextureBrush( Brush brush_0 )
        {
            IBrush2D brush2D = null;
            if( !_2DTextureBrush.TryGetValue( brush_0, out brush2D ) )
            {
                brush2D = _renderContext.CreateBrush( brush_0, _opacity, TextureMappingMode.PerPrimitive );
                _2DTextureBrush.Add( brush_0, brush2D );
            }
            return brush2D;
        }

        public void Dispose( )
        {
            foreach( IDisposable disposable in _2DColorPen.Values )
            {
                disposable.Dispose( );
            }
            foreach( IDisposable disposable in _2DTextureBrush.Values )
            {
                disposable.Dispose( );
            }
            foreach( IDisposable disposable in _2DColorBrush.Values )
            {
                disposable.Dispose( );
            }
            _2DColorPen.Clear( );
            _2DColorBrush.Clear( );
            _2DTextureBrush.Clear( );
        }

        internal PooledDictionary< Color, IPen2D > ColorPen
        {
            get
            {
                return _2DColorPen;
            }
        }

        internal PooledDictionary< Color, IBrush2D > ColorBrush
        {
            get
            {
                return _2DColorBrush;
            }
        }

        internal PooledDictionary< Brush, IBrush2D > TextureBrush
        {
            get
            {
                return _2DTextureBrush;
            }
        }
    }
}

