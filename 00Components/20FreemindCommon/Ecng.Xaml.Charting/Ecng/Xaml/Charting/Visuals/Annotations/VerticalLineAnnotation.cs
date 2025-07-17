// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Annotations.VerticalLineAnnotation
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
namespace Ecng.Xaml.Charting
{
    [ContentProperty( "AnnotationLabels" )]
    [TemplatePart( Name = "PART_LineAnnotationRoot", Type = typeof( Grid ) )]
    public class VerticalLineAnnotation : LineAnnotationWithLabelsBase
    {
        public new static readonly DependencyProperty VerticalAlignmentProperty = DependencyProperty.Register(nameof (VerticalAlignment), typeof (VerticalAlignment), typeof (VerticalLineAnnotation), new PropertyMetadata((object) VerticalAlignment.Stretch, new PropertyChangedCallback(VerticalLineAnnotation.OnVerticalAlignmentChanged)));
        public static readonly DependencyProperty LabelsOrientationProperty = DependencyProperty.Register(nameof (LabelsOrientation), typeof (Orientation), typeof (VerticalLineAnnotation), new PropertyMetadata((object) Orientation.Vertical, new PropertyChangedCallback(VerticalLineAnnotation.OnLabelsOrientationChanged)));

        public VerticalLineAnnotation()
        {
            this.DefaultStyleKey = ( object ) typeof( VerticalLineAnnotation );
        }

        public Orientation LabelsOrientation
        {
            get
            {
                return ( Orientation ) this.GetValue( VerticalLineAnnotation.LabelsOrientationProperty );
            }
            set
            {
                this.SetValue( VerticalLineAnnotation.LabelsOrientationProperty, ( object ) value );
            }
        }

        public new VerticalAlignment VerticalAlignment
        {
            get
            {
                return ( VerticalAlignment ) this.GetValue( VerticalLineAnnotation.VerticalAlignmentProperty );
            }
            set
            {
                this.SetValue( VerticalLineAnnotation.VerticalAlignmentProperty, ( object ) value );
            }
        }

        private void ApplyOrientation( AnnotationLabel label )
        {
            if ( this.LabelsOrientation == Orientation.Horizontal )
            {
                label.RotationAngle = 0.0;
            }
            else
            {
                LabelPlacement labelPlacement = label.ParentAnnotation.GetLabelPlacement(label);
                switch ( labelPlacement )
                {
                    case LabelPlacement.Bottom:
                    case LabelPlacement.Top:
                    case LabelPlacement.Axis:
                        label.RotationAngle = 0.0;
                        break;
                    default:
                        if ( labelPlacement.IsRight() )
                        {
                            label.RotationAngle = 90.0;
                            break;
                        }
                        if ( !labelPlacement.IsLeft() )
                            break;
                        label.RotationAngle = -90.0;
                        break;
                }
            }
        }

        protected override void Attach( AnnotationLabel label )
        {
            base.Attach( label );
            if ( this.IsHidden )
                return;
            this.ApplyOrientation( label );
        }

        public override IAxis GetUsedAxis()
        {
            IAxis axis = (IAxis) null;
            if ( this.XAxis != null )
                axis = this.XAxis.IsHorizontalAxis ? this.XAxis : this.YAxis;
            else if ( this.YAxis != null )
                axis = this.YAxis.IsHorizontalAxis ? this.YAxis : this.XAxis;
            return axis;
        }

        protected override void ApplyPlacement( AnnotationLabel label, LabelPlacement placement )
        {
            bool flag1 = placement.IsTop();
            bool flag2 = placement.IsBottom();
            bool flag3 = placement.IsLeft();
            if ( placement.IsRight() | flag3 )
            {
                label.SetValue( Grid.RowProperty, ( object ) 1 );
                label.SetValue( Grid.ColumnProperty, ( object ) ( flag3 ? 0 : 2 ) );
                label.VerticalAlignment = VerticalAlignment.Center;
                label.HorizontalAlignment = flag3 ? HorizontalAlignment.Right : HorizontalAlignment.Left;
                if ( flag2 )
                    label.VerticalAlignment = VerticalAlignment.Bottom;
                if ( !flag1 )
                    return;
                label.VerticalAlignment = VerticalAlignment.Top;
            }
            else
            {
                if ( flag2 )
                    label.SetValue( Grid.RowProperty, ( object ) 2 );
                if ( flag1 )
                    label.SetValue( Grid.RowProperty, ( object ) 0 );
                label.SetValue( Grid.ColumnProperty, ( object ) 1 );
                label.HorizontalAlignment = HorizontalAlignment.Center;
            }
        }

        internal override LabelPlacement ResolveAutoPlacement()
        {
            LabelPlacement labelPlacement = LabelPlacement.Axis;
            if ( this.VerticalAlignment == VerticalAlignment.Top )
                labelPlacement = LabelPlacement.Bottom;
            if ( this.VerticalAlignment == VerticalAlignment.Center )
                labelPlacement = LabelPlacement.Right;
            if ( this.VerticalAlignment == VerticalAlignment.Stretch )
                labelPlacement = LabelPlacement.Axis;
            return labelPlacement;
        }

        protected override Cursor GetSelectedCursor()
        {
            return Cursors.SizeWE;
        }

        protected override void MoveAnnotationTo( AnnotationCoordinates coordinates, double horizOffset, double vertOffset )
        {
            IAnnotationCanvas canvas = this.GetCanvas(this.AnnotationCanvas);
            double coord = coordinates.X1Coord + horizOffset;
            if ( !this.IsCoordinateValid( coord, canvas.ActualWidth ) )
            {
                if ( coord < 0.0 )
                    horizOffset -= coord - 1.0;
                if ( coord > canvas.ActualWidth )
                    horizOffset -= coord - ( canvas.ActualWidth - 1.0 );
                coord = coordinates.X1Coord + horizOffset;
            }
            base.SetBasePoint( new Point()
            {
                X = coord,
                Y = coordinates.Y1Coord
            }, 0, this.XAxis, this.YAxis );
            this.OnAnnotationDragging( new AnnotationDragDeltaEventArgs( horizOffset, 0.0 ) );
        }

        protected override void SetBasePoint( Point newPoint, int index, IAxis xAxis, IAxis yAxis )
        {
            IAnnotationCanvas canvas = this.GetCanvas(this.AnnotationCanvas);
            IComparable[] comparableArray = this.FromCoordinates(0.0, newPoint.Y);
            IComparable comparable1 = comparableArray[0];
            IComparable comparable2 = comparableArray[1];
            DependencyProperty x;
            DependencyProperty y;
            this.GetPropertiesFromIndex( index, out x, out y );
            bool flag = !this.XAxis.IsHorizontalAxis;
            if ( !this.IsCoordinateValid( newPoint.Y, canvas.ActualHeight ) )
                return;
            if ( flag )
                this.SetCurrentValue( x, ( object ) comparable1 );
            else
                this.SetCurrentValue( y, ( object ) comparable2 );
        }

        protected override void GetPropertiesFromIndex( int index, out DependencyProperty x, out DependencyProperty y )
        {
            x = AnnotationBase.X1Property;
            y = AnnotationBase.Y1Property;
            switch ( this.VerticalAlignment )
            {
                case VerticalAlignment.Top:
                    y = AnnotationBase.Y2Property;
                    break;
                case VerticalAlignment.Center:
                    y = index == 0 ? AnnotationBase.Y1Property : AnnotationBase.Y2Property;
                    break;
                case VerticalAlignment.Bottom:
                    y = AnnotationBase.Y1Property;
                    break;
            }
        }

        private static void OnVerticalAlignmentChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( d as VerticalLineAnnotation )?.Refresh();
        }

        private static void OnLabelsOrientationChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            VerticalLineAnnotation verticalLineAnnotation = d as VerticalLineAnnotation;
            if ( verticalLineAnnotation == null )
                return;
            foreach ( AnnotationLabel annotationLabel in ( Collection<AnnotationLabel> ) verticalLineAnnotation.AnnotationLabels )
                verticalLineAnnotation.ApplyOrientation( annotationLabel );
            verticalLineAnnotation.Refresh();
        }

        protected override IAnnotationPlacementStrategy GetCurrentPlacementStrategy()
        {
            if ( this.XAxis != null && this.XAxis.IsPolarAxis )
                return ( IAnnotationPlacementStrategy ) new VerticalLineAnnotation.PolarAnnotationPlacementStrategy( this );
            return ( IAnnotationPlacementStrategy ) new VerticalLineAnnotation.CartesianAnnotationPlacementStrategy( this );
        }

        internal new class CartesianAnnotationPlacementStrategy : AnnotationBase.CartesianAnnotationPlacementStrategyBase<VerticalLineAnnotation>
        {
            public CartesianAnnotationPlacementStrategy( VerticalLineAnnotation annotation )
              : base( annotation )
            {
            }

            public override void PlaceAnnotation( AnnotationCoordinates coordinates )
            {
                IAnnotationCanvas canvas = this.Annotation.GetCanvas(this.Annotation.AnnotationCanvas);
                double y = 0.0;
                double x1Coord = coordinates.X1Coord;
                if ( !x1Coord.IsRealNumber() || canvas == null )
                    return;
                double val1 = canvas.ActualHeight;
                switch ( this.Annotation.VerticalAlignment )
                {
                    case VerticalAlignment.Top:
                        val1 = coordinates.Y1Coord.IsDefined() ? coordinates.Y1Coord : coordinates.Y2Coord;
                        break;
                    case VerticalAlignment.Center:
                        double num1 = Math.Min(coordinates.Y1Coord, coordinates.Y2Coord);
                        double num2 = Math.Max(coordinates.Y1Coord, coordinates.Y2Coord);
                        y = num1;
                        double num3 = num1;
                        val1 = num2 - num3;
                        break;
                    case VerticalAlignment.Bottom:
                        y = coordinates.Y1Coord.IsDefined() ? coordinates.Y1Coord : coordinates.Y2Coord;
                        val1 -= y;
                        break;
                }
                this.PlaceAnnotation( x1Coord, y, Math.Max( val1, 0.0 ), coordinates.XOffset );
            }

            private void PlaceAnnotation( double x, double y, double height, double axisOffset )
            {
                Grid annotationRoot = this.Annotation.AnnotationRoot as Grid;
                this.Annotation.Height = height;
                double number = annotationRoot.ColumnDefinitions[0].ActualWidth + (double) (int) (annotationRoot.ColumnDefinitions[1].ActualWidth / 2.0);
                double num = x - (number.IsRealNumber() ? number : 0.0);
                this.Annotation.SetValue( Canvas.LeftProperty, ( object ) num );
                this.Annotation.SetValue( Canvas.TopProperty, ( object ) y );
                this.Annotation.TryPlaceAxisLabels( new Point( x - axisOffset, y ) );
            }

            public override Point[ ] GetBasePoints( AnnotationCoordinates coordinates )
            {
                Point[] pointArray = (Point[]) null;
                switch ( this.Annotation.VerticalAlignment )
                {
                    case VerticalAlignment.Top:
                        pointArray = new Point[ 1 ]
                        {
              new Point(coordinates.X1Coord, coordinates.Y2Coord)
                        };
                        break;
                    case VerticalAlignment.Center:
                        pointArray = new Point[ 2 ]
                        {
              new Point(coordinates.X1Coord, coordinates.Y1Coord),
              new Point(coordinates.X1Coord, coordinates.Y2Coord)
                        };
                        break;
                    case VerticalAlignment.Bottom:
                        pointArray = new Point[ 1 ]
                        {
              new Point(coordinates.X1Coord, coordinates.Y1Coord)
                        };
                        break;
                }
                return pointArray;
            }

            public override bool IsInBounds( AnnotationCoordinates coordinates, IAnnotationCanvas canvas )
            {
                bool flag = false;
                if ( coordinates.X1Coord < 0.0 || coordinates.X1Coord > canvas.ActualWidth )
                {
                    flag = true;
                }
                else
                {
                    switch ( this.Annotation.VerticalAlignment )
                    {
                        case VerticalAlignment.Top:
                            flag = coordinates.Y2Coord < 0.0;
                            break;
                        case VerticalAlignment.Center:
                            flag = coordinates.Y1Coord < 0.0 && coordinates.Y2Coord < 0.0 || coordinates.Y1Coord > canvas.ActualHeight && coordinates.Y2Coord > canvas.ActualHeight;
                            break;
                        case VerticalAlignment.Bottom:
                            flag = coordinates.Y1Coord > canvas.ActualHeight;
                            break;
                    }
                }
                return !flag;
            }
        }

        internal new class PolarAnnotationPlacementStrategy : AnnotationBase.PolarAnnotationPlacementStrategyBase<VerticalLineAnnotation>
        {
            public PolarAnnotationPlacementStrategy( VerticalLineAnnotation annotation )
              : base( annotation )
            {
                throw new InvalidOperationException( string.Format( "Unable to place {0} on polar chart.", ( object ) annotation.GetType().Name ) );
            }
        }
    }
}
