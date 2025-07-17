// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Annotations.AnnotationResizeAdorner
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using StockSharp.Xaml.Charting.Common.Extensions;

namespace StockSharp.Xaml.Charting.Visuals.Annotations
{
    internal class AnnotationResizeAdorner : AdornerBase, IAnnotationResizeAdorner, IAnnotationAdorner
    {
        private readonly List<Thumb> _adornerMarkers = new List<Thumb>();
        private Point[] _points;
        private double _horizontalChange;
        private double _verticalChange;
        private bool _inUpdate;

        public AnnotationResizeAdorner( IAnnotation adornedElement )
          : base( adornedElement )
        {
        }

        public override void Initialize()
        {
            this.Clear();
            this.UpdatePositions();
        }

        private void AttachMarkerAt( Point point )
        {
            Thumb thumb = new Thumb();
            AnnotationBase adornedAnnotation = this.AdornedAnnotation as AnnotationBase;
            if ( adornedAnnotation != null && adornedAnnotation.ResizingGripsStyle != null )
                thumb.Style = adornedAnnotation.ResizingGripsStyle;
            this.ParentCanvas.Children.Add( ( UIElement ) thumb );
            thumb.DragStarted += new DragStartedEventHandler( this.OnDragMarkerStarted );
            thumb.DragDelta += new DragDeltaEventHandler( this.OnDragMarker );
            thumb.DragCompleted += new DragCompletedEventHandler( this.OnDragMarkerCompleted );
            this._adornerMarkers.Add( thumb );
        }

        private void OnDragMarkerCompleted( object sender, DragCompletedEventArgs e )
        {
            ( sender as Thumb ).ReleaseMouseCapture();
            IAnnotation adornedAnnotation = this.AdornedAnnotation;
            if ( !adornedAnnotation.IsEditable )
                return;
            adornedAnnotation.OnDragEnded();
        }

        private void OnDragMarkerStarted( object sender, DragStartedEventArgs e )
        {
            ( sender as Thumb ).CaptureMouse();
            IAnnotation adornedAnnotation = this.AdornedAnnotation;
            if ( !adornedAnnotation.IsEditable )
                return;
            adornedAnnotation.OnDragStarted();
        }

        public override void Clear()
        {
            this._adornerMarkers.ForEachDo<Thumb>( new Action<Thumb>( this.DetachMarker ) );
            this._adornerMarkers.Clear();
        }

        private void DetachMarker( Thumb marker )
        {
            marker.DragDelta -= new DragDeltaEventHandler( this.OnDragMarker );
            this.ParentCanvas.Children.Remove( ( UIElement ) marker );
        }

        private void UpdatePositions( Point[ ] newPositions )
        {
            foreach ( Thumb adornerMarker in this._adornerMarkers )
            {
                Point newPosition = newPositions[this._adornerMarkers.IndexOf(adornerMarker)];
                newPosition.X -= adornerMarker.Width / 2.0;
                newPosition.Y -= adornerMarker.Height / 2.0;
                Canvas.SetLeft( ( UIElement ) adornerMarker, newPosition.X );
                Canvas.SetTop( ( UIElement ) adornerMarker, newPosition.Y );
            }
        }

        public override void UpdatePositions()
        {
            if ( this._inUpdate )
                return;
            try
            {
                this._inUpdate = true;
                this._points = this.AdornedAnnotation.GetBasePoints();
                if ( this._points != null && this._points.Length != 0 && this._adornerMarkers.Count == 0 )
                    ( ( IEnumerable<Point> ) this._points ).ForEachDo<Point>( new Action<Point>( this.AttachMarkerAt ) );
                this.UpdatePositions( this._points );
                this._adornerMarkers.ForEach( ( Action<Thumb> ) ( m => m.ContextMenu = ( this.AdornedAnnotation as AnnotationBase )?.ContextMenu ) );
            }
            finally
            {
                this._inUpdate = false;
            }
        }

        private void OnDragMarker( object sender, DragDeltaEventArgs e )
        {
            IAnnotation adornedAnnotation = this.AdornedAnnotation;
            if ( !adornedAnnotation.IsEditable )
                return;
            int index = this._adornerMarkers.IndexOf(sender as Thumb);
            this._horizontalChange = e.HorizontalChange;
            this._verticalChange = e.VerticalChange;
            double offsetX = adornedAnnotation.ResizeDirections == XyDirection.YDirection ? 0.0 : this._horizontalChange;
            double offsetY = adornedAnnotation.ResizeDirections == XyDirection.XDirection ? 0.0 : this._verticalChange;
            Point point = this._points[index];
            point.X += offsetX;
            point.Y += offsetY;
            if ( adornedAnnotation.IsResizable )
                adornedAnnotation.SetBasePoint( point, index );
            else
                adornedAnnotation.MoveAnnotation( offsetX, offsetY );
            adornedAnnotation.OnDragDelta();
        }

        public IEnumerable<Thumb> AdornerMarkers
        {
            get
            {
                return ( IEnumerable<Thumb> ) this._adornerMarkers;
            }
        }
    }
}
