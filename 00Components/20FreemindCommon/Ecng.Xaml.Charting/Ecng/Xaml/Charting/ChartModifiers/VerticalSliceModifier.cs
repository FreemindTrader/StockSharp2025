// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartModifiers.VerticalSliceModifier
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Common.Helpers;
using StockSharp.Xaml.Charting.Visuals;
using StockSharp.Xaml.Charting.Visuals.Annotations;
using StockSharp.Xaml.Charting.Visuals.RenderableSeries;

namespace StockSharp.Xaml.Charting.ChartModifiers
{
    public class VerticalSliceModifier : VerticalSliceModifierBase
    {
        public static readonly DependencyProperty IncludeSeriesProperty = DependencyProperty.RegisterAttached("IncludeSeries", typeof (bool), typeof (VerticalSliceModifier), new PropertyMetadata((object) true));
        public static readonly DependencyProperty VerticalLinesProperty = DependencyProperty.Register(nameof (VerticalLines), typeof (VerticalLineAnnotationCollection), typeof (VerticalSliceModifier), new PropertyMetadata((object) null, new PropertyChangedCallback(VerticalSliceModifier.OnVerticalLinesDependencyPropertyChanged)));
        private Dictionary<BaseRenderableSeries, ObjectPool<TemplatableControl>> rolloverMarkersDictionary = new Dictionary<BaseRenderableSeries, ObjectPool<TemplatableControl>>();

        public static bool GetIncludeSeries( DependencyObject obj )
        {
            return ( bool ) obj.GetValue( VerticalSliceModifier.IncludeSeriesProperty );
        }

        public static void SetIncludeSeries( DependencyObject obj, bool value )
        {
            obj.SetValue( VerticalSliceModifier.IncludeSeriesProperty, ( object ) value );
        }

        public VerticalSliceModifier()
        {
            this.DefaultStyleKey = ( object ) typeof( VerticalSliceModifier );
            this.SetCurrentValue( InspectSeriesModifierBase.SeriesDataProperty, ( object ) new ChartDataObject() );
            this.SetCurrentValue( VerticalSliceModifier.VerticalLinesProperty, ( object ) new VerticalLineAnnotationCollection() );
        }

        public VerticalLineAnnotationCollection VerticalLines
        {
            get
            {
                return ( VerticalLineAnnotationCollection ) this.GetValue( VerticalSliceModifier.VerticalLinesProperty );
            }
            set
            {
                this.SetValue( VerticalSliceModifier.VerticalLinesProperty, ( object ) value );
            }
        }

        protected override IEnumerable<SeriesInfo> GetSeriesInfoAt( Point point )
        {
            foreach ( VerticalLineAnnotation verticalLineAnnotation in this.VerticalLines.Where<VerticalLineAnnotation>( ( Func<VerticalLineAnnotation, bool> ) ( x =>
            {
                if ( !x.IsHidden && x.IsAttached && x.XAxis != null )
                    return x.XAxis.IsHorizontalAxis;
                return false;
            } ) ) )
            {
                if ( verticalLineAnnotation.X1 != null )
                {
                    foreach ( SeriesInfo seriesInfo in base.GetSeriesInfoAt( new Point( verticalLineAnnotation.XAxis.GetCoordinate( verticalLineAnnotation.X1 ), 0.0 ) ) )
                        yield return seriesInfo;
                }
            }
        }

        protected override FrameworkElement GetRolloverMarkerFrom( SeriesInfo seriesInfo )
        {
            BaseRenderableSeries renderableSeries = seriesInfo.RenderableSeries as BaseRenderableSeries;
            if ( renderableSeries == null )
                return ( FrameworkElement ) null;
            ObjectPool<TemplatableControl> objectPool;
            if ( !this.rolloverMarkersDictionary.TryGetValue( renderableSeries, out objectPool ) )
            {
                objectPool = new ObjectPool<TemplatableControl>();
                this.rolloverMarkersDictionary.Add( renderableSeries, objectPool );
            }
            return ( FrameworkElement ) objectPool.Get( ( Func<TemplatableControl> ) ( () => ( TemplatableControl ) PointMarker.CreateFromTemplate( renderableSeries.RolloverMarkerTemplate, ( object ) seriesInfo ) ) );
        }

        protected override void DetachRolloverMarker( FrameworkElement rolloverMarker )
        {
            base.DetachRolloverMarker( rolloverMarker );
            SeriesInfo dataContext = rolloverMarker.DataContext as SeriesInfo;
            if ( dataContext == null )
                return;
            this.rolloverMarkersDictionary[ ( BaseRenderableSeries ) dataContext.RenderableSeries ].Put( ( TemplatableControl ) rolloverMarker );
        }

        protected override void OnParentSurfaceMouseLeave()
        {
        }

        protected override bool IsEnabledAt( Point point )
        {
            return true;
        }

        protected override void FillWithIncludedSeries( IEnumerable<SeriesInfo> infos, ObservableCollection<SeriesInfo> seriesInfos )
        {
            infos.ForEachDo<SeriesInfo>( ( Action<SeriesInfo> ) ( info =>
            {
                if ( !info.RenderableSeries.GetIncludeSeries( Modifier.VerticalSlice ) )
                    return;
                seriesInfos.Add( info );
            } ) );
        }

        protected override void OnIsEnabledChanged()
        {
            base.OnIsEnabledChanged();
            if ( this.IsEnabled )
                this.OnAttached();
            else
                this.OnDetached();
        }

        public override void OnAttached()
        {
            base.OnAttached();
            this.SubscribeAnnotationsChangedNotification();
        }

        private void SubscribeAnnotationsChangedNotification()
        {
            ISciChartSurface parentSurface = this.ParentSurface;
            if ( parentSurface == null )
                return;
            parentSurface.AnnotationsCollectionNewCollectionAssigned -= new EventHandler( this.OnAnnotationsDrasticallyChanged );
            parentSurface.AnnotationsCollectionNewCollectionAssigned += new EventHandler( this.OnAnnotationsDrasticallyChanged );
            this.VerticalLines.CollectionChanged -= new NotifyCollectionChangedEventHandler( this.OnVerticalLinesCollectionChanged );
            this.VerticalLines.CollectionChanged += new NotifyCollectionChangedEventHandler( this.OnVerticalLinesCollectionChanged );
            foreach ( IAnnotation verticalLine in ( Collection<VerticalLineAnnotation> ) this.VerticalLines )
                this.ParentSurface.Annotations.Add( verticalLine );
            this.OnAnnotationsDrasticallyChanged( ( object ) null, EventArgs.Empty );
        }

        public override void OnDetached()
        {
            base.OnDetached();
            this.UnsubscribeAnnotationsChangedNotification();
        }

        private void UnsubscribeAnnotationsChangedNotification()
        {
            if ( this.VerticalLines == null )
                return;
            this.VerticalLines.CollectionChanged -= new NotifyCollectionChangedEventHandler( this.OnVerticalLinesCollectionChanged );
            ISciChartSurface parentSurface = this.ParentSurface;
            if ( parentSurface != null )
            {
                parentSurface.AnnotationsCollectionNewCollectionAssigned -= new EventHandler( this.OnAnnotationsDrasticallyChanged );
                if ( parentSurface.Annotations != null )
                    parentSurface.Annotations.CollectionChanged -= new NotifyCollectionChangedEventHandler( this.OnAnnotationsCollectionChanged );
            }
            foreach ( VerticalLineAnnotation annotation in this.VerticalLines.ToArray<VerticalLineAnnotation>() )
                this.DetachVerticalLine( annotation );
        }

        private void OnVerticalLinesCollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            IList newItems = e.NewItems;
            IList oldItems = e.OldItems;
            if ( oldItems != null )
                oldItems.OfType<VerticalLineAnnotation>().ForEachDo<VerticalLineAnnotation>( new Action<VerticalLineAnnotation>( this.DetachVerticalLine ) );
            if ( newItems != null )
                newItems.OfType<VerticalLineAnnotation>().ForEachDo<VerticalLineAnnotation>( new Action<VerticalLineAnnotation>( this.AttachVerticalLine ) );
            if ( e.Action == NotifyCollectionChangedAction.Reset )
                this.VerticalLines.OldItems.ForEachDo<VerticalLineAnnotation>( new Action<VerticalLineAnnotation>( this.DetachVerticalLine ) );
            this.HandleMasterMouseEvent( this.CurrentPoint );
        }

        private void AttachVerticalLine( VerticalLineAnnotation annotation )
        {
            this.DetachVerticalLine( annotation );
            annotation.PropertyChanged += new PropertyChangedEventHandler( this.OnPropertyChanged );
            if ( this.ParentSurface == null || this.ParentSurface.Annotations == null )
                return;
            this.ParentSurface.Annotations.Add( ( IAnnotation ) annotation );
        }

        private void DetachVerticalLine( VerticalLineAnnotation annotation )
        {
            annotation.IsHiddenChanged -= new EventHandler( this.OnAnnotationStateChanged );
            annotation.PropertyChanged -= new PropertyChangedEventHandler( this.OnPropertyChanged );
            if ( this.ParentSurface == null || this.ParentSurface.Annotations == null )
                return;
            this.ParentSurface.Annotations.Remove( ( IAnnotation ) annotation );
        }

        private void OnPropertyChanged( object sender, PropertyChangedEventArgs args )
        {
            if ( !( args.PropertyName == "PositionChanged" ) )
                return;
            this.HandleMasterMouseEvent( new Point() );
        }

        private void OnAnnotationStateChanged( object sender, EventArgs args )
        {
            this.HandleMasterMouseEvent( new Point() );
        }

        private void OnAnnotationsDrasticallyChanged( object sender, EventArgs e )
        {
            if ( this.ParentSurface == null || this.ParentSurface.Annotations == null )
                return;
            this.ParentSurface.Annotations.CollectionChanged -= new NotifyCollectionChangedEventHandler( this.OnAnnotationsCollectionChanged );
            this.ParentSurface.Annotations.CollectionChanged += new NotifyCollectionChangedEventHandler( this.OnAnnotationsCollectionChanged );
            this.OnAnnotationsCollectionChanged( ( object ) this, new NotifyCollectionChangedEventArgs( NotifyCollectionChangedAction.Reset ) );
            this.VerticalLines.OldItems.ForEachDo<VerticalLineAnnotation>( new Action<VerticalLineAnnotation>( ( ( Collection<VerticalLineAnnotation> ) this.VerticalLines ).Add ) );
        }

        private void OnAnnotationsCollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            if ( this.ParentSurface == null || this.ParentSurface.Annotations == null )
                return;
            VerticalLineAnnotationCollection annotationCollection = this.VerticalLines;
            IList oldItems = e.OldItems;
            if ( oldItems != null )
                oldItems.OfType<VerticalLineAnnotation>().ForEachDo<VerticalLineAnnotation>( ( Action<VerticalLineAnnotation> ) ( annotation => annotationCollection.Remove( annotation ) ) );
            if ( e.Action != NotifyCollectionChangedAction.Reset )
                return;
            annotationCollection.Clear();
            this.ParentSurface.InvalidateElement();
        }

        private static void OnVerticalLinesDependencyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            VerticalSliceModifier verticalSliceModifier = (VerticalSliceModifier) d;
            VerticalLineAnnotationCollection oldValue = e.OldValue as VerticalLineAnnotationCollection;
            VerticalLineAnnotationCollection newValue = e.NewValue as VerticalLineAnnotationCollection;
            if ( oldValue != null )
            {
                oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler( verticalSliceModifier.OnVerticalLinesCollectionChanged );
                oldValue.ForEachDo<VerticalLineAnnotation>( new Action<VerticalLineAnnotation>( verticalSliceModifier.DetachVerticalLine ) );
            }
            if ( newValue == null )
                return;
            newValue.CollectionChanged += new NotifyCollectionChangedEventHandler( verticalSliceModifier.OnVerticalLinesCollectionChanged );
            newValue.ForEachDo<VerticalLineAnnotation>( new Action<VerticalLineAnnotation>( verticalSliceModifier.AttachVerticalLine ) );
        }

        public override void OnModifierMouseMove( ModifierMouseArgs e )
        {
        }
    }
}
