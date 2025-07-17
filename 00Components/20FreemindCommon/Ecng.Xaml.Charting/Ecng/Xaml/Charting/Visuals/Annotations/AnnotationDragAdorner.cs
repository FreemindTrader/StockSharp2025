// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Annotations.AnnotationDragAdorner
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Windows;
namespace fx.Xaml.Charting
{
    internal class AnnotationDragAdorner : AdornerBase
    {
        private bool _isDragging;
        private Point _startPoint;

        public AnnotationDragAdorner( IAnnotation adornedElement )
          : base( adornedElement )
        {
        }

        public override void Initialize()
        {
        }

        public override void UpdatePositions()
        {
        }

        public override void Clear()
        {
        }

        public void InitiateDrag()
        {
        }

        public override void OnModifierMouseMove( ModifierMouseArgs e )
        {
            base.OnModifierMouseMove( e );
            IAnnotation adornedAnnotation = this.AdornedAnnotation;
            if ( !this._isDragging )
                return;
            Point pointRelativeToRoot = this.GetPointRelativeToRoot(e.MousePoint);
            double num1 = pointRelativeToRoot.X < 0.0 || pointRelativeToRoot.X > this.ParentCanvas.ActualWidth ? 0.0 : pointRelativeToRoot.X - this._startPoint.X;
            double num2 = pointRelativeToRoot.Y < 0.0 || pointRelativeToRoot.Y > this.ParentCanvas.ActualHeight ? 0.0 : pointRelativeToRoot.Y - this._startPoint.Y;
            double offsetX = adornedAnnotation.DragDirections == XyDirection.YDirection ? 0.0 : num1;
            double offsetY = adornedAnnotation.DragDirections == XyDirection.XDirection ? 0.0 : num2;
            adornedAnnotation.MoveAnnotation( offsetX, offsetY );
            this._startPoint = pointRelativeToRoot;
            e.Handled = true;
            this.UpdatePositions();
        }

        public new void OnModifierMouseDown( ModifierMouseArgs e )
        {
            base.OnModifierMouseDown( e );
            IAnnotation adornedAnnotation = this.AdornedAnnotation;
            if ( !adornedAnnotation.IsEditable )
                return;
            this._isDragging = true;
            this._startPoint = this.GetPointRelativeToRoot( e.MousePoint );
            adornedAnnotation.CaptureMouse();
            e.Handled = true;
        }

        public override void OnModifierMouseUp( ModifierMouseArgs e )
        {
            base.OnModifierMouseUp( e );
            IAnnotation adornedAnnotation = this.AdornedAnnotation;
            if ( !this._isDragging )
                return;
            this._isDragging = false;
            adornedAnnotation.ReleaseMouseCapture();
        }
    }
}
