// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.PointMarkers.BasePointMarker
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace Ecng.Xaml.Charting
{
    public abstract class BasePointMarker : ContentControl, IPointMarker
    {
        public static readonly DependencyProperty PointMarkerTemplateProperty = DependencyProperty.Register(nameof (PointMarkerTemplate), typeof (ControlTemplate), typeof (BasePointMarker), new PropertyMetadata((object) null, (PropertyChangedCallback) ((s, e) => ((BasePointMarker) s).OnPropertyChanged(s, e))));
        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(nameof (Stroke), typeof (Color), typeof (BasePointMarker), new PropertyMetadata((object) new Color(), (PropertyChangedCallback) ((s, e) => ((BasePointMarker) s).OnPropertyChanged(s, e))));
        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(nameof (StrokeThickness), typeof (double), typeof (BasePointMarker), new PropertyMetadata((object) 1.0, (PropertyChangedCallback) ((s, e) => ((BasePointMarker) s).OnPropertyChanged(s, e))));
        public static readonly DependencyProperty FillProperty = DependencyProperty.Register(nameof (Fill), typeof (Color), typeof (BasePointMarker), new PropertyMetadata((object) new Color(), (PropertyChangedCallback) ((s, e) => ((BasePointMarker) s).OnPropertyChanged(s, e))));
        public static readonly DependencyProperty AntiAliasingProperty = DependencyProperty.Register(nameof (AntiAliasing), typeof (bool), typeof (BasePointMarker), new PropertyMetadata((object) true, (PropertyChangedCallback) ((s, e) => ((BasePointMarker) s).OnPropertyChanged(s, e))));
        private SmartDisposable<IPen2D> _cachedPen;
        private SmartDisposable<IBrush2D> _cachedBrush;
        private Type _typeOfRendererForCachedResources;
        private Ecng.Xaml.PropertyChangeNotifier _widthNotifier;
        private Ecng.Xaml.PropertyChangeNotifier _heightNotifier;

        protected BasePointMarker()
        {
            this.DefaultStyleKey = ( object ) this.GetType();
            this._widthNotifier = new Ecng.Xaml.PropertyChangeNotifier( ( DependencyObject ) this, FrameworkElement.WidthProperty );
            this._heightNotifier = new Ecng.Xaml.PropertyChangeNotifier( ( DependencyObject ) this, FrameworkElement.HeightProperty );
            this._widthNotifier.ValueChanged += ( Action ) ( () => this.OnPropertyChanged( ( DependencyObject ) this, new DependencyPropertyChangedEventArgs() ) );
            this._heightNotifier.ValueChanged += ( Action ) ( () => this.OnPropertyChanged( ( DependencyObject ) this, new DependencyPropertyChangedEventArgs() ) );
        }

        public static bool EnableMultithreadedDrawing
        {
            get; set;
        }

        public ControlTemplate PointMarkerTemplate
        {
            get
            {
                return ( ControlTemplate ) this.GetValue( BasePointMarker.PointMarkerTemplateProperty );
            }
            set
            {
                this.SetValue( BasePointMarker.PointMarkerTemplateProperty, ( object ) value );
            }
        }

        public Color Stroke
        {
            get
            {
                return ( Color ) this.GetValue( BasePointMarker.StrokeProperty );
            }
            set
            {
                this.SetValue( BasePointMarker.StrokeProperty, ( object ) value );
            }
        }

        public Color Fill
        {
            get
            {
                return ( Color ) this.GetValue( BasePointMarker.FillProperty );
            }
            set
            {
                this.SetValue( BasePointMarker.FillProperty, ( object ) value );
            }
        }

        public double StrokeThickness
        {
            get
            {
                return ( double ) this.GetValue( BasePointMarker.StrokeThicknessProperty );
            }
            set
            {
                this.SetValue( BasePointMarker.StrokeThicknessProperty, ( object ) value );
            }
        }

        public bool AntiAliasing
        {
            get
            {
                return ( bool ) this.GetValue( BasePointMarker.AntiAliasingProperty );
            }
            set
            {
                this.SetValue( BasePointMarker.AntiAliasingProperty, ( object ) value );
            }
        }

        public virtual void Draw( IRenderContext2D context, IEnumerable<Point> centers )
        {
            this.DrawInternal( context, centers, this._cachedPen.Inner, this._cachedBrush.Inner );
        }

        public virtual void Draw( IRenderContext2D context, double x, double y, IPen2D defaultPen, IBrush2D defaultBrush )
        {
            this.DrawInternal( context, x, y, this.GetPen( defaultPen ), this.GetBrush( defaultBrush ) );
        }

        private IPen2D GetPen( IPen2D pen )
        {
            return pen ?? this._cachedPen.Inner;
        }

        private IBrush2D GetBrush( IBrush2D brush )
        {
            return brush ?? this._cachedBrush.Inner;
        }

        public virtual void Dispose()
        {
            this._typeOfRendererForCachedResources = ( Type ) null;
            if ( this._cachedBrush != null )
            {
                this._cachedBrush.Dispose();
                this._cachedBrush = ( SmartDisposable<IBrush2D> ) null;
            }
            if ( this._cachedPen == null )
                return;
            this._cachedPen.Dispose();
            this._cachedPen = ( SmartDisposable<IPen2D> ) null;
        }

        protected abstract void DrawInternal( IRenderContext2D context, IEnumerable<Point> centers, IPen2D pen, IBrush2D brush );

        protected abstract void DrawInternal( IRenderContext2D context, double x, double y, IPen2D pen, IBrush2D brush );

        protected virtual void OnPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( BasePointMarker ) d ).Dispose();
            ( ( BasePointMarker ) d ).InvalidateParentSurface();
        }

        private void InvalidateParentSurface()
        {
            ( this.DataContext as BaseRenderableSeries )?.OnInvalidateParentSurface();
        }

        private void CheckCachedResources( IRenderContext2D context )
        {
            if ( !( context.GetType() != this._typeOfRendererForCachedResources ) && this._cachedPen != null && this._cachedBrush != null )
                return;
            this.Dispose();
            this._cachedBrush = new SmartDisposable<IBrush2D>( context.CreateBrush( this.Fill, 1.0, new bool?() ) );
            this._cachedPen = new SmartDisposable<IPen2D>( context.CreatePen( this.Stroke, this.AntiAliasing, ( float ) this.StrokeThickness, 1.0, ( double[ ] ) null, PenLineCap.Round ) );
            this._typeOfRendererForCachedResources = context.GetType();
        }

        public virtual void Begin( IRenderContext2D context, IPen2D defaultPen, IBrush2D defaultBrush )
        {
            context.SetPrimitvesCachingEnabled( true );
            this.CheckCachedResources( context );
        }

        public virtual void End( IRenderContext2D context )
        {
            context.SetPrimitvesCachingEnabled( false );
        }
    }
}
