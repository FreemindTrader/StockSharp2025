// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.PointMarkers.SpritePointMarker
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
namespace fx.Xaml.Charting
{
    public class SpritePointMarker : BasePointMarker, IPointMarker
    {
        private SmartDisposable<ISprite2D> _cachedPointMarker;
        private Type _typeOfRendererForCachedResources;
        private Rect _pmRect;

        double IPointMarker.Width
        {
            get
            {
                if ( this._cachedPointMarker == null )
                    return this.ActualWidth;
                return ( double ) this._cachedPointMarker.Inner.Width;
            }
            set
            {
                this.Width = value;
            }
        }

        double IPointMarker.Height
        {
            get
            {
                if ( this._cachedPointMarker == null )
                    return this.ActualHeight;
                return ( double ) this._cachedPointMarker.Inner.Height;
            }
            set
            {
                this.Height = value;
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            if ( this._cachedPointMarker == null )
                return;
            this._cachedPointMarker.Dispose();
            this._cachedPointMarker = ( SmartDisposable<ISprite2D> ) null;
        }

        protected override void DrawInternal( IRenderContext2D context, IEnumerable<Point> centers, IPen2D pen, IBrush2D brush )
        {
            if ( this.PointMarkerTemplate == null )
                return;
            this.CheckCachedSprite( context );
            context.DrawSprites( this._cachedPointMarker.Inner, this._pmRect, centers.Select<Point, Point>( ( Func<Point, Point> ) ( center => new Point( center.X - ( double ) this._cachedPointMarker.Inner.Width / 2.0, center.Y - ( double ) this._cachedPointMarker.Inner.Height / 2.0 ) ) ) );
        }

        protected override void DrawInternal( IRenderContext2D context, double x, double y, IPen2D pen, IBrush2D brush )
        {
            if ( this.PointMarkerTemplate == null )
                return;
            this.CheckCachedSprite( context );
            context.DrawSprite( this._cachedPointMarker.Inner, this._pmRect, new Point( x - ( double ) this._cachedPointMarker.Inner.Width * 0.5, y - ( double ) this._cachedPointMarker.Inner.Height * 0.5 ) );
        }

        private void CheckCachedSprite( IRenderContext2D context )
        {
            Type type = context.GetType();
            if ( this._cachedPointMarker != null && !( type != this._typeOfRendererForCachedResources ) )
                return;
            PointMarker fromTemplate = PointMarker.CreateFromTemplate(this.PointMarkerTemplate, (object) this);
            this._cachedPointMarker = new SmartDisposable<ISprite2D>( context.CreateSprite( ( FrameworkElement ) fromTemplate ) );
            this._pmRect = new Rect( 0.0, 0.0, ( double ) this._cachedPointMarker.Inner.Width, ( double ) this._cachedPointMarker.Inner.Height );
            this._typeOfRendererForCachedResources = type;
        }
    }
}
