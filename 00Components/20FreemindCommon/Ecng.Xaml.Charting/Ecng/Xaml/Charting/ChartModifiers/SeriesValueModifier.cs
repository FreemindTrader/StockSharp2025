// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartModifiers.SeriesValueModifier
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Visuals;
using StockSharp.Xaml.Charting.Visuals.Annotations;
using StockSharp.Xaml.Charting.Visuals.Axes;
using StockSharp.Xaml.Charting.Visuals.RenderableSeries;

namespace StockSharp.Xaml.Charting.ChartModifiers
{
    public class SeriesValueModifier : ChartModifierBase
    {
        public static readonly DependencyProperty AxisMarkerStyleProperty = DependencyProperty.Register(nameof (AxisMarkerStyle), typeof (Style), typeof (SeriesValueModifier), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty YAxisIdProperty = DependencyProperty.Register(nameof (YAxisId), typeof (string), typeof (SeriesValueModifier), new PropertyMetadata((object) "DefaultAxisId", new PropertyChangedCallback(SeriesValueModifier.OnAxisIdDependencyPropertyChanged)));
        public static readonly DependencyProperty IsSeriesValueModifierEnabledProperty = DependencyProperty.RegisterAttached("IsSeriesValueModifierEnabled", typeof (bool), typeof (SeriesValueModifier), new PropertyMetadata((object) true));
        public static readonly DependencyProperty IsRenderableSeriesInViewportProperty = DependencyProperty.RegisterAttached("IsRenderableSeriesInViewport", typeof (bool), typeof (SeriesValueModifier), new PropertyMetadata((object) false));
        public static readonly DependencyProperty IsLastPointInViewportProperty = DependencyProperty.RegisterAttached("IsLastPointInViewport", typeof (bool), typeof (SeriesValueModifier), new PropertyMetadata((object) false));
        public static readonly DependencyProperty SeriesMarkerColorProperty = DependencyProperty.RegisterAttached("SeriesMarkerColor", typeof (Color), typeof (SeriesValueModifier), new PropertyMetadata((object) new Color()));
        private readonly IDictionary<IRenderableSeries, IAnnotation> _annotationsBySeries = (IDictionary<IRenderableSeries, IAnnotation>) new Dictionary<IRenderableSeries, IAnnotation>();
        private ObservableCollection<IRenderableSeries> _renderSeriesCollection;
        private Ecng.Xaml.PropertyChangeNotifier _renderSeriesNotifier;

        public static void SetIsSeriesValueModifierEnabled( UIElement element, bool value )
        {
            element.SetValue( SeriesValueModifier.IsSeriesValueModifierEnabledProperty, ( object ) value );
        }

        public static bool GetIsSeriesValueModifierEnabled( UIElement element )
        {
            return ( bool ) element.GetValue( SeriesValueModifier.IsSeriesValueModifierEnabledProperty );
        }

        public static bool GetIsRenderableSeriesInViewport( DependencyObject obj )
        {
            return ( bool ) obj.GetValue( SeriesValueModifier.IsRenderableSeriesInViewportProperty );
        }

        public static void SetIsRenderableSeriesInViewport( DependencyObject obj, bool value )
        {
            obj.SetValue( SeriesValueModifier.IsRenderableSeriesInViewportProperty, ( object ) value );
        }

        public static bool GetIsLastPointInViewport( DependencyObject obj )
        {
            return ( bool ) obj.GetValue( SeriesValueModifier.IsLastPointInViewportProperty );
        }

        public static void SetIsLastPointInViewport( DependencyObject obj, bool value )
        {
            obj.SetValue( SeriesValueModifier.IsLastPointInViewportProperty, ( object ) value );
        }

        public static Color GetSeriesMarkerColor( DependencyObject obj )
        {
            return ( Color ) obj.GetValue( SeriesValueModifier.SeriesMarkerColorProperty );
        }

        public static void SetSeriesMarkerColor( DependencyObject obj, Color value )
        {
            obj.SetValue( SeriesValueModifier.SeriesMarkerColorProperty, ( object ) value );
        }

        public SeriesValueModifier()
        {
            this.IsPolarChartSupported = false;
        }

        protected IDictionary<IRenderableSeries, IAnnotation> AnnotationsBySeries
        {
            get
            {
                return this._annotationsBySeries;
            }
        }

        public Style AxisMarkerStyle
        {
            get
            {
                return ( Style ) this.GetValue( SeriesValueModifier.AxisMarkerStyleProperty );
            }
            set
            {
                this.SetValue( SeriesValueModifier.AxisMarkerStyleProperty, ( object ) value );
            }
        }

        public string YAxisId
        {
            get
            {
                return ( string ) this.GetValue( SeriesValueModifier.YAxisIdProperty );
            }
            set
            {
                this.SetValue( SeriesValueModifier.YAxisIdProperty, ( object ) value );
            }
        }

        public override void OnAttached()
        {
            base.OnAttached();
            UltrachartSurface parentSurface = this.ParentSurface as UltrachartSurface;
            if ( parentSurface != null )
            {
                this._renderSeriesNotifier = new Ecng.Xaml.PropertyChangeNotifier( ( DependencyObject ) parentSurface, UltrachartSurface.RenderableSeriesProperty );
                this._renderSeriesNotifier.ValueChanged += new Action( this.OnRenderableSeriesDrasticallyChanged );
            }
            this.OnRenderableSeriesDrasticallyChanged();
        }

        private void OnRenderableSeriesDrasticallyChanged()
        {
            this.ResetAllMarkers();
            if ( this._renderSeriesCollection != null )
                this._renderSeriesCollection.CollectionChanged -= new NotifyCollectionChangedEventHandler( this.OnRenderableSeriesCollectionChanged );
            this._renderSeriesCollection = this.ParentSurface.RenderableSeries;
            if ( this._renderSeriesCollection == null )
                return;
            this._renderSeriesCollection.CollectionChanged += new NotifyCollectionChangedEventHandler( this.OnRenderableSeriesCollectionChanged );
        }

        private void OnRenderableSeriesCollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            if ( e.Action == NotifyCollectionChangedAction.Reset )
            {
                this.ResetAllMarkers();
            }
            else
            {
                if ( this.ParentSurface == null || this.ParentSurface.Annotations == null )
                    return;
                e.OldItems.ForEachDo<IRenderableSeries>( new Action<IRenderableSeries>( this.DetachMarkerFor ) );
                e.NewItems.ForEachDo<IRenderableSeries>( new Action<IRenderableSeries>( this.AttachMarkerFor ) );
            }
        }

        private void ResetAllMarkers()
        {
            if ( this.ParentSurface == null || this.ParentSurface.Annotations == null )
                return;
            this.ClearAllMarkers();
            if ( !this.IsEnabled )
                return;
            this.ParentSurface.RenderableSeries.ForEachDo<IRenderableSeries>( new Action<IRenderableSeries>( this.AttachMarkerFor ) );
        }

        private void ClearAllMarkers()
        {
            if ( this.ParentSurface != null && this.ParentSurface.Annotations != null )
                this._annotationsBySeries.ForEachDo<KeyValuePair<IRenderableSeries, IAnnotation>>( ( Action<KeyValuePair<IRenderableSeries, IAnnotation>> ) ( kvp => this.ParentSurface.Annotations.Remove( kvp.Value ) ) );
            this._annotationsBySeries.Clear();
        }

        private void AttachMarkerFor( IRenderableSeries renderableSeries )
        {
            if ( !( renderableSeries.YAxisId == this.YAxisId ) || this._annotationsBySeries.ContainsKey( renderableSeries ) )
                return;
            SeriesValueAxisMarkerAnnotation markerAnnotation1 = new SeriesValueAxisMarkerAnnotation();
            markerAnnotation1.Style = this.AxisMarkerStyle;
            markerAnnotation1.DataContext = ( object ) renderableSeries;
            markerAnnotation1.Y1 = renderableSeries.DataSeries != null ? renderableSeries.DataSeries.LatestYValue : ( IComparable ) null;
            markerAnnotation1.XAxisId = renderableSeries.XAxisId;
            markerAnnotation1.YAxisId = renderableSeries.YAxisId;
            SeriesValueAxisMarkerAnnotation markerAnnotation2 = markerAnnotation1;
            this.ParentSurface.Annotations.Add( ( IAnnotation ) markerAnnotation2 );
            this._annotationsBySeries.Add( renderableSeries, ( IAnnotation ) markerAnnotation2 );
        }

        private void DetachMarkerFor( IRenderableSeries renderableSeries )
        {
            IAnnotation annotation;
            if ( !this._annotationsBySeries.TryGetValue( renderableSeries, out annotation ) )
                return;
            this.ParentSurface.Annotations.Remove( annotation );
            this._annotationsBySeries.Remove( renderableSeries );
        }

        public override void OnDetached()
        {
            this.ClearAllMarkers();
            if ( this._renderSeriesCollection != null )
            {
                this._renderSeriesCollection.CollectionChanged -= new NotifyCollectionChangedEventHandler( this.OnRenderableSeriesCollectionChanged );
                this._renderSeriesCollection = ( ObservableCollection<IRenderableSeries> ) null;
            }
            if ( this._renderSeriesNotifier == null )
                return;
            this._renderSeriesNotifier.ValueChanged -= new Action( this.OnRenderableSeriesDrasticallyChanged );
            this._renderSeriesNotifier = ( Ecng.Xaml.PropertyChangeNotifier ) null;
        }

        protected override void OnIsEnabledChanged()
        {
            base.OnIsEnabledChanged();
            this.ResetAllMarkers();
        }

        protected override void OnAnnotationCollectionChanged( object sender, NotifyCollectionChangedEventArgs args )
        {
            base.OnAnnotationCollectionChanged( sender, args );
            if ( args.Action != NotifyCollectionChangedAction.Reset )
                return;
            this.ResetAllMarkers();
        }

        public override void OnParentSurfaceRendered( UltrachartRenderedMessage e )
        {
            base.OnParentSurfaceRendered( e );
            if ( !this.IsEnabled )
                return;
            if ( this.ParentSurface.RenderableSeries.Count<IRenderableSeries>( ( Func<IRenderableSeries, bool> ) ( s => s.YAxisId == this.YAxisId ) ) != this._annotationsBySeries.Count )
                this.ResetAllMarkers();
            foreach ( IRenderableSeries index in this.ParentSurface.RenderableSeries.Where<IRenderableSeries>( new Func<IRenderableSeries, bool>( this.CanUpdateAxisMarkerFor ) ) )
            {
                AxisMarkerAnnotation markerAnnotation = (AxisMarkerAnnotation) this._annotationsBySeries[index];
                IRange visibleRange = index.XAxis.VisibleRange;
                IndexRange indicesRange = index.DataSeries.GetIndicesRange(visibleRange);
                HitTestInfo hitTestInfo = HitTestInfo.Empty;
                IComparable latestPointInViewport = index.DataSeries.LatestYValue;
                bool isDefined = indicesRange.IsDefined;
                if ( isDefined )
                {
                    IComparable xvalue = (IComparable) index.DataSeries.XValues[indicesRange.Max];
                    if ( !visibleRange.AsDoubleRange().IsValueWithinRange( ( IComparable ) xvalue.ToDouble() ) )
                    {
                        double num = this.ModifierSurface.ActualWidth - 1.0;
                        Point rawPoint = this.XAxis.IsHorizontalAxis ? new Point(num, 0.0) : new Point(0.0, num);
                        hitTestInfo = index.VerticalSliceHitTest( rawPoint, true );
                        latestPointInViewport = hitTestInfo.YValue;
                    }
                }
                IComparable latestYvalue = index.DataSeries.LatestYValue;
                bool flag = latestYvalue != null && latestPointInViewport != null && latestYvalue.CompareTo((object) latestPointInViewport) == 0;
                BaseRenderableSeries renderableSeries = index as BaseRenderableSeries;
                if ( renderableSeries != null )
                {
                    renderableSeries.SetValue( SeriesValueModifier.IsRenderableSeriesInViewportProperty, ( object ) isDefined );
                    renderableSeries.SetValue( SeriesValueModifier.IsLastPointInViewportProperty, ( object ) flag );
                    Color color = !hitTestInfo.IsEmpty() ? index.GetSeriesColorAtPoint(hitTestInfo) : renderableSeries.SeriesColor;
                    renderableSeries.SetValue( SeriesValueModifier.SeriesMarkerColorProperty, ( object ) color );
                }
                markerAnnotation.Y1 = latestPointInViewport;
                if ( index.YAxis != null )
                    markerAnnotation.FormattedValue = this.FormatAxisMarker( index, latestPointInViewport );
            }
        }

        protected virtual string FormatAxisMarker( IRenderableSeries renderableSeries, IComparable latestPointInViewport )
        {
            return ( ( AxisBase ) renderableSeries.YAxis ).FormatCursorText( latestPointInViewport );
        }

        private bool CanUpdateAxisMarkerFor( IRenderableSeries renderSeries )
        {
            if ( renderSeries.DataSeries != null && renderSeries.XAxis != null )
                return renderSeries.YAxisId == this.YAxisId;
            return false;
        }

        private static void OnAxisIdDependencyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            SeriesValueModifier seriesValueModifier = d as SeriesValueModifier;
            if ( seriesValueModifier == null || seriesValueModifier.ParentSurface == null )
                return;
            seriesValueModifier.ResetAllMarkers();
        }
    }
}
