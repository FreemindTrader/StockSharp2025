using SciChart.Charting.Visuals.PointMarkers;
using System;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Drawing.Common;
using System.Windows;
using System.Windows.Media;
using SciChart.Charting.Common;
using SciChart.Core.Extensions;
using System.Collections.Generic; using fx.Collections;

namespace fx.Charting
{
    public class FxSpritePointMarker : BasePointMarker, IPointMarker
    {
        private Color _strokeColor;
        private Color _strokeBrush;
        private SmartDisposable<ISprite2D> _cachedPointMarker;
        private ISprite2D _spirte2D;
        private Size _renderSize;
        private Rect _drawRect;
        private Type _typeOfRendererForCachedResources;

        double IPointMarker.Width
        {
            get
            {
                if ( _cachedPointMarker == null )
                {
                    return ActualWidth;
                }
                return _cachedPointMarker.Inner.Width;
            }
            set
            {
                Width = value;
            }
        }

        double IPointMarker.Height
        {
            get
            {
                if ( _cachedPointMarker == null )
                {
                    return ActualHeight;
                }
                return _cachedPointMarker.Inner.Height;
            }
            set
            {
                Height = value;
            }
        }

        protected new Size RenderSize
        {
            get
            {
                return _renderSize;
            }
        }

        private PointMarker _pointMarker;

        protected override void OnPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            Dispose( );
            base.OnPropertyChanged( d, e );
        }

        protected ISprite2D CreateSprite( IRenderContext2D context, Color strokeColor, Color strokeBrush )
        {
            if ( _pointMarker == null )
            {
                _pointMarker = PointMarker.CreateFromTemplate( PointMarkerTemplate, DataContext );
            }
            return context.CreateSprite( _pointMarker );
        }

        public override void BeginBatch( IRenderContext2D context, Color? strokeColor, Color? fillColor )
        {
            Init( context, strokeColor, fillColor );
            base.BeginBatch( context, strokeColor, fillColor );
        }

        public void BeginBatchNoInit( IRenderContext2D context, Color? strokeColor, Color? fillColor )
        {            
            base.BeginBatch( context, strokeColor, fillColor );
        }

        private void Init( IRenderContext2D context, Color? strokeClr, Color? strokeBrh )
        {
            Color strokeColor = strokeClr ?? Stroke;
            Color strokeBrush = strokeBrh ?? Fill;

            if ( ( _cachedPointMarker == null || _spirte2D == null || ( _strokeColor != strokeColor || _strokeBrush != strokeBrush ) ? 1 : ( CheckCachedSprite( context ) ? 1 : 0 ) ) == 0 )
            {
                return;
            }

            _cachedPointMarker.SafeDispose( );
            _spirte2D.SafeDispose( );
            _spirte2D          = CreateSprite( context, strokeColor, strokeBrush );
            _cachedPointMarker = new SmartDisposable<ISprite2D>( _spirte2D );
            float width        = _spirte2D.Width;
            float height       = _spirte2D.Height;
            _drawRect.Width    = width;
            _drawRect.Height   = height;
            _xOffset           = ( int )( width * 0.5 );
            _yOffset           = ( int )( height * 0.5 );
            _strokeColor       = strokeColor;
            _strokeBrush       = strokeBrush;
        }

        private bool CheckCachedSprite( IRenderContext2D context )
        {
            bool flag = false;
            if ( _typeOfRendererForCachedResources == null || context.GetType( ) != _typeOfRendererForCachedResources )
            {
                _typeOfRendererForCachedResources = context.GetType( );
                flag = true;
            }
            return flag;
        }
        

        protected virtual Size GetSpriteSize( )
        {
            _renderSize.Width = Width;
            _renderSize.Height = Height;
            return _renderSize;
        }

        public override void Draw( IRenderContext2D context, IEnumerable<Point> centers )
        {
            if ( _cachedPointMarker == null )
            {
                return;
            }

            // Tony: Make some changes to Sprites
            context.DrawSprites( _cachedPointMarker.Inner, centers );
        }
        

        public override void Dispose( )
        {
            base.Dispose( );
            if ( _cachedPointMarker == null )
            {
                return;
            }
            _cachedPointMarker.Dispose( );
            _cachedPointMarker = null;
        }

        public ISprite2D GetSprite( )
        {
            return _spirte2D;
        }

        internal SmartDisposable<ISprite2D> CachedPointMarker
        {
            get
            {
                return _cachedPointMarker;
            }
        }
    }
}

