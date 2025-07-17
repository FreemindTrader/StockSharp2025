// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Visuals.Annotations.HorizontalLineAnnotation
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.StrategyManager;
using Ecng.Xaml.Charting.Visuals.Axes;

namespace Ecng.Xaml.Charting.Visuals.Annotations
{
    [ContentProperty( "AnnotationLabels" )]
    [TemplatePart( Name = "PART_LineAnnotationRoot", Type = typeof( Grid ) )]
    public class HorizontalLineAnnotation : LineAnnotationWithLabelsBase
    {
        public new static readonly DependencyProperty HorizontalAlignmentProperty = DependencyProperty.Register(nameof (HorizontalAlignment), typeof (HorizontalAlignment), typeof (HorizontalLineAnnotation), new PropertyMetadata((object) HorizontalAlignment.Left, new PropertyChangedCallback(HorizontalLineAnnotation.OnHorizontalAlignmentChanged)));
        public static readonly DependencyProperty YDragStepProperty = DependencyProperty.Register(nameof (YDragStep), typeof (double), typeof (HorizontalLineAnnotation), new PropertyMetadata((object) 0.0));

        public HorizontalLineAnnotation()
        {
            this.DefaultStyleKey = ( object ) typeof( HorizontalLineAnnotation );
        }

        public new HorizontalAlignment HorizontalAlignment
        {
            get
            {
                return ( HorizontalAlignment ) this.GetValue( HorizontalLineAnnotation.HorizontalAlignmentProperty );
            }
            set
            {
                this.SetValue( HorizontalLineAnnotation.HorizontalAlignmentProperty, ( object ) value );
            }
        }

        public double YDragStep
        {
            get
            {
                return ( double ) this.GetValue( HorizontalLineAnnotation.YDragStepProperty );
            }
            set
            {
                this.SetValue( HorizontalLineAnnotation.YDragStepProperty, ( object ) value );
            }
        }

        public override IAxis GetUsedAxis()
        {
            IAxis axis = (IAxis) null;
            if ( this.XAxis != null )
            {
                axis = this.XAxis.IsHorizontalAxis ? this.YAxis : this.XAxis;
            }
            else if ( this.YAxis != null )
            {
                axis = this.YAxis.IsHorizontalAxis ? this.XAxis : this.YAxis;
            }

            return axis;
        }

        internal override LabelPlacement ResolveAutoPlacement()
        {
            LabelPlacement labelPlacement = LabelPlacement.Top;
            if ( this.HorizontalAlignment == HorizontalAlignment.Right )
            {
                labelPlacement = LabelPlacement.TopRight;
            }

            if ( this.HorizontalAlignment == HorizontalAlignment.Left )
            {
                labelPlacement = LabelPlacement.TopLeft;
            }

            if ( this.HorizontalAlignment == HorizontalAlignment.Center )
            {
                labelPlacement = LabelPlacement.Top;
            }

            if ( this.HorizontalAlignment == HorizontalAlignment.Stretch )
            {
                labelPlacement = LabelPlacement.Axis;
            }

            return labelPlacement;
        }

        protected override void MoveAnnotationTo( AnnotationCoordinates coordinates, double horizOffset, double vertOffset )
        {
            IAxis usedAxis = this.GetUsedAxis();
            IAnnotationCanvas canvas = this.GetCanvas(this.AnnotationCanvas);
            double coord = coordinates.Y1Coord + vertOffset;
            if ( !this.IsCoordinateValid( coord, canvas.ActualHeight ) )
            {
                if ( coord < 0.0 )
                {
                    vertOffset -= coord - 1.0;
                }

                if ( coord > canvas.ActualHeight )
                {
                    vertOffset -= coord - ( canvas.ActualHeight - 1.0 );
                }

                coord = coordinates.Y1Coord + vertOffset;
            }
            if ( this.YDragStep > 0.0 && !usedAxis.IsHorizontalAxis && !usedAxis.IsXAxis )
            {
                IComparable c1 = this.FromCoordinate(coordinates.Y1Coord, usedAxis);
                IComparable c2 = this.FromCoordinate(coord, usedAxis);
                int num1 = (int) (Math.Abs(c1.ToDouble() - c2.ToDouble()) / this.YDragStep);
                int num2 = !usedAxis.FlipCoordinates ? -Math.Sign(vertOffset) : Math.Sign(vertOffset);
                coord = this.ToCoordinate( ( IComparable ) ( c1.ToDouble() + ( double ) ( num2 * num1 ) * this.YDragStep ), usedAxis );
                vertOffset = coord - coordinates.Y1Coord;
            }
            if ( !this.IsCoordinateValid( coord, canvas.ActualHeight ) )
            {
                return;
            }

            base.SetBasePoint( new Point()
            {
                X = coordinates.X1Coord,
                Y = coord
            }, 0, this.XAxis, this.YAxis );
            this.OnAnnotationDragging( new AnnotationDragDeltaEventArgs( 0.0, vertOffset ) );
        }

        protected override void GetPropertiesFromIndex( int index, out DependencyProperty X, out DependencyProperty Y )
        {
            X = AnnotationBase.X1Property;
            Y = AnnotationBase.Y1Property;
            switch ( this.HorizontalAlignment )
            {
                case HorizontalAlignment.Left:
                    X = AnnotationBase.X2Property;
                    break;
                case HorizontalAlignment.Center:
                    X = index == 0 ? AnnotationBase.X1Property : AnnotationBase.X2Property;
                    break;
                case HorizontalAlignment.Right:
                    X = AnnotationBase.X1Property;
                    break;
            }
        }

        protected override void SetBasePoint( Point newPoint, int index, IAxis xAxis, IAxis yAxis )
        {
            IAnnotationCanvas canvas = this.GetCanvas(this.AnnotationCanvas);
            IComparable[] comparableArray = this.FromCoordinates(newPoint);
            IComparable comparable1 = comparableArray[0];
            IComparable comparable2 = comparableArray[1];
            DependencyProperty x;
            DependencyProperty y;
            this.GetPropertiesFromIndex( index, out x, out y );
            bool flag = !this.XAxis.IsHorizontalAxis;
            if ( !this.IsCoordinateValid( newPoint.X, canvas.ActualWidth ) )
            {
                return;
            }

            if ( flag )
            {
                this.SetCurrentValue( y, ( object ) comparable2 );
            }
            else
            {
                this.SetCurrentValue( x, ( object ) comparable1 );
            }
        }

        private static void OnHorizontalAlignmentChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( d as HorizontalLineAnnotation )?.Refresh();
        }

        protected override IAnnotationPlacementStrategy GetCurrentPlacementStrategy()
        {
            if ( this.XAxis != null && this.XAxis.IsPolarAxis )
            {
                return ( IAnnotationPlacementStrategy ) new HorizontalLineAnnotation.PolarAnnotationPlacementStrategy( this );
            }

            return ( IAnnotationPlacementStrategy ) new HorizontalLineAnnotation.CartesianAnnotationPlacementStrategy( this );
        }

        internal class CartesianAnnotationPlacementStrategy : AnnotationBase.CartesianAnnotationPlacementStrategyBase<HorizontalLineAnnotation>
        {
            public CartesianAnnotationPlacementStrategy( HorizontalLineAnnotation annotation )
              : base( annotation )
            {
            }

            public override void PlaceAnnotation( AnnotationCoordinates coordinates )
            {
                IAnnotationCanvas canvas = this.Annotation.GetCanvas(this.Annotation.AnnotationCanvas);
                double x = 0.0;
                double y1Coord = coordinates.Y1Coord;
                if ( !y1Coord.IsRealNumber() || canvas == null )
                {
                    return;
                }

                double val1 = canvas.ActualWidth;
                switch ( this.Annotation.HorizontalAlignment )
                {
                    case HorizontalAlignment.Left:
                        val1 = coordinates.X1Coord.IsDefined() ? coordinates.X1Coord : coordinates.X2Coord;
                        break;
                    case HorizontalAlignment.Center:
                        double num1 = Math.Min(coordinates.X1Coord, coordinates.X2Coord);
                        double num2 = Math.Max(coordinates.X1Coord, coordinates.X2Coord);
                        x = num1;
                        val1 = num2 - num1;
                        break;
                    case HorizontalAlignment.Right:
                        x = coordinates.X1Coord.IsDefined() ? coordinates.X1Coord : coordinates.X2Coord;
                        val1 -= x;
                        break;
                }
                this.PlaceAnnotation( x, y1Coord, Math.Max( val1, 0.0 ), coordinates.YOffset );
            }

            private void PlaceAnnotation( double x, double y, double width, double axisOffset )
            {
                Grid annotationRoot = this.Annotation.AnnotationRoot as Grid;
                this.Annotation.Width = width;
                double num1 = annotationRoot.RowDefinitions[1].ActualHeight / 2.0;
                double number = annotationRoot.RowDefinitions[0].ActualHeight + num1;
                double num2 = y - (number.IsRealNumber() ? number : 0.0);
                this.Annotation.SetValue( Canvas.LeftProperty, ( object ) x );
                this.Annotation.SetValue( Canvas.TopProperty, ( object ) num2 );
                this.Annotation.TryPlaceAxisLabels( new Point( x, y - axisOffset ) );
            }

            public override Point[ ] GetBasePoints( AnnotationCoordinates coordinates )
            {
                Point[] pointArray = (Point[]) null;
                switch ( this.Annotation.HorizontalAlignment )
                {
                    case HorizontalAlignment.Left:
                        pointArray = new Point[ 1 ]
                        {
              new Point(coordinates.X2Coord, coordinates.Y1Coord)
                        };
                        break;
                    case HorizontalAlignment.Center:
                        pointArray = new Point[ 2 ]
                        {
              new Point(coordinates.X1Coord, coordinates.Y1Coord),
              new Point(coordinates.X2Coord, coordinates.Y1Coord)
                        };
                        break;
                    case HorizontalAlignment.Right:
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
                if ( coordinates.Y1Coord < 0.0 || coordinates.Y1Coord > canvas.ActualHeight )
                {
                    flag = true;
                }
                else
                {
                    switch ( this.Annotation.HorizontalAlignment )
                    {
                        case HorizontalAlignment.Left:
                            flag = coordinates.X2Coord < 0.0;
                            break;
                        case HorizontalAlignment.Center:
                            flag = coordinates.X1Coord < 0.0 && coordinates.X2Coord < 0.0 || coordinates.X1Coord > canvas.ActualWidth && coordinates.X2Coord > canvas.ActualWidth;
                            break;
                        case HorizontalAlignment.Right:
                            flag = coordinates.X1Coord > canvas.ActualWidth;
                            break;
                    }
                }
                return !flag;
            }
        }

        internal class PolarAnnotationPlacementStrategy : AnnotationBase.PolarAnnotationPlacementStrategyBase<HorizontalLineAnnotation>
        {
            public PolarAnnotationPlacementStrategy( HorizontalLineAnnotation annotation )
              : base( annotation )
            {
                throw new InvalidOperationException( string.Format( "Unable to place {0} on polar chart.", ( object ) annotation.GetType().Name ) );
            }
        }
    }
}
