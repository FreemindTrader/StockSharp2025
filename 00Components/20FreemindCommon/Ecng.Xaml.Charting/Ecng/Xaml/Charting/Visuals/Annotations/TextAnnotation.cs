// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Annotations.TextAnnotation
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
namespace fx.Xaml.Charting
{
    [TemplatePart( Name = "PART_InputTextArea", Type = typeof( TextBox ) )]
    public class TextAnnotation : AnchorPointAnnotation
    {
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof (CornerRadius), typeof (CornerRadius), typeof (TextAnnotation), new PropertyMetadata((object) new CornerRadius()));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof (Text), typeof (string), typeof (TextAnnotation), new PropertyMetadata((object) string.Empty));
        public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register(nameof (TextAlignment), typeof (TextAlignment), typeof (TextAnnotation), new PropertyMetadata((object) TextAlignment.Left));
        public static readonly DependencyProperty TextStretchProperty = DependencyProperty.Register(nameof (TextStretch), typeof (Stretch), typeof (TextAnnotation), new PropertyMetadata((object) Stretch.None));
        private TextBox _inputTextArea;

        public TextAnnotation()
        {
            this.DefaultStyleKey = ( object ) typeof( TextAnnotation );
            DependencyProperty contextMenuProperty = FrameworkElement.ContextMenuProperty;
            ContextMenu contextMenu = new ContextMenu();
            contextMenu.Visibility = Visibility.Collapsed;
            this.SetCurrentValue( contextMenuProperty, ( object ) contextMenu );
        }

        public CornerRadius CornerRadius
        {
            get
            {
                return ( CornerRadius ) this.GetValue( TextAnnotation.CornerRadiusProperty );
            }
            set
            {
                this.SetValue( TextAnnotation.CornerRadiusProperty, ( object ) value );
            }
        }

        public TextAlignment TextAlignment
        {
            get
            {
                return ( TextAlignment ) this.GetValue( TextAnnotation.TextAlignmentProperty );
            }
            set
            {
                this.SetValue( TextAnnotation.TextAlignmentProperty, ( object ) value );
            }
        }

        public string Text
        {
            get
            {
                return ( string ) this.GetValue( TextAnnotation.TextProperty );
            }
            set
            {
                this.SetValue( TextAnnotation.TextProperty, ( object ) value );
            }
        }

        private Stretch TextStretch
        {
            get
            {
                return ( Stretch ) this.GetValue( TextAnnotation.TextStretchProperty );
            }
            set
            {
                this.SetValue( TextAnnotation.TextStretchProperty, ( object ) value );
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.AnnotationRoot = ( FrameworkElement ) this.GetAndAssertTemplateChild<Border>( "PART_TextAnnotationRoot" );
            this._inputTextArea = this.GetAndAssertTemplateChild<TextBox>( "PART_InputTextArea" );
            Binding binding = new Binding("ContextMenu")
            {
                RelativeSource = RelativeSource.TemplatedParent
            };
            this._inputTextArea.SetBinding( FrameworkElement.ContextMenuProperty, ( BindingBase ) binding );
            this.PerformFocusOnInputTextArea();
        }

        protected override void FocusInputTextArea()
        {
            if ( this._inputTextArea == null )
                return;
            this._inputTextArea.IsEnabled = true;
            this._inputTextArea.Focus();
        }

        public void RemoveFocusFromInputTextArea()
        {
            this.RemoveFocusInputTextArea();
        }

        protected override void RemoveFocusInputTextArea()
        {
            if ( this._inputTextArea == null )
                return;
            this._inputTextArea.IsEnabled = false;
        }

        private AnnotationCoordinates CoerceValues()
        {
            AnnotationCoordinates coordinates = this.GetCoordinates(this.GetCanvas(this.AnnotationCanvas), this.XAxis.GetCurrentCoordinateCalculator(), this.YAxis.GetCurrentCoordinateCalculator());
            double x2Coord = coordinates.X2Coord;
            double y2Coord = coordinates.Y2Coord;
            Point point = new Point(coordinates.X1Coord, coordinates.Y1Coord);
            if ( double.IsNaN( x2Coord ) || double.IsNaN( y2Coord ) )
            {
                double xCoord = point.X + this.ActualWidth;
                double yCoord = point.Y + this.ActualHeight;
                IComparable[] comparableArray = this.FromCoordinates(xCoord, yCoord);
                this.SetCurrentValue( AnnotationBase.X2Property, ( object ) comparableArray[ 0 ] );
                this.SetCurrentValue( AnnotationBase.Y2Property, ( object ) comparableArray[ 1 ] );
                coordinates.X2Coord = xCoord;
                coordinates.Y2Coord = yCoord;
            }
            return coordinates;
        }

        public override bool IsPointWithinBounds( Point point )
        {
            AnnotationCoordinates annotationCoordinates = this.CoerceValues();
            return new Rect( new Point( annotationCoordinates.X1Coord, annotationCoordinates.Y1Coord ), new Point( annotationCoordinates.X2Coord, annotationCoordinates.Y2Coord ) ).Contains( point );
        }

        protected override IAnnotationPlacementStrategy GetCurrentPlacementStrategy()
        {
            if ( this.XAxis != null && this.XAxis.IsPolarAxis )
                return ( IAnnotationPlacementStrategy ) new TextAnnotation.PolarAnnotationPlacementStrategy( this );
            return ( IAnnotationPlacementStrategy ) new TextAnnotation.CartesianAnnotationPlacementStrategy( this );
        }

        internal class CartesianAnnotationPlacementStrategy : AnnotationBase.CartesianAnnotationPlacementStrategyBase<TextAnnotation>
        {
            public CartesianAnnotationPlacementStrategy( TextAnnotation annotation )
              : base( annotation )
            {
            }

            public override void PlaceAnnotation( AnnotationCoordinates coordinates )
            {
                coordinates = this.Annotation.GetAnchorAnnotationCoordinates( coordinates );
                double x1Coord = coordinates.X1Coord;
                double x2Coord = coordinates.X2Coord;
                double y1Coord = coordinates.Y1Coord;
                double y2Coord = coordinates.Y2Coord;
                if ( x2Coord < x1Coord )
                    NumberUtil.Swap( ref x1Coord, ref x2Coord );
                if ( y2Coord < y1Coord )
                    NumberUtil.Swap( ref y1Coord, ref y2Coord );
                double c1 = x2Coord - x1Coord + 1.0;
                double c2 = y2Coord - y1Coord + 1.0;
                if ( !x1Coord.IsDefined() || !y1Coord.IsDefined() )
                    return;
                if ( c1.IsDefined() )
                    this.Annotation.MinWidth = c1;
                if ( c2.IsDefined() )
                    this.Annotation.MinHeight = c2;
                Canvas.SetLeft( ( UIElement ) this.Annotation, x1Coord );
                Canvas.SetTop( ( UIElement ) this.Annotation, y1Coord );
            }

            public override Point[ ] GetBasePoints( AnnotationCoordinates coordinates )
            {
                coordinates = this.Annotation.GetAnchorAnnotationCoordinates( coordinates );
                if ( double.IsNaN( coordinates.X2Coord ) || double.IsNaN( coordinates.Y2Coord ) )
                    coordinates = this.Annotation.CoerceValues();
                return new Point[ 1 ]
                {
          new Point(coordinates.X1Coord, coordinates.Y1Coord)
                };
            }

            protected override void InternalMoveAnntotationTo( AnnotationCoordinates coordinates, ref double horizontalOffset, ref double verticalOffset, IAnnotationCanvas canvas )
            {
                double num1 = coordinates.X1Coord + horizontalOffset;
                double num2 = coordinates.X2Coord + horizontalOffset;
                double num3 = coordinates.Y1Coord + verticalOffset;
                double num4 = coordinates.Y2Coord + verticalOffset;
                if ( !this.IsCoordinateValid( num1, canvas.ActualWidth ) || !this.IsCoordinateValid( num3, canvas.ActualHeight ) || ( !this.IsCoordinateValid( num2, canvas.ActualWidth ) || !this.IsCoordinateValid( num4, canvas.ActualHeight ) ) )
                {
                    double val1_1 = double.IsNaN(num1) ? 0.0 : num1;
                    double val2_1 = double.IsNaN(num2) ? val1_1 : num2;
                    double val1_2 = double.IsNaN(num3) ? 0.0 : num3;
                    double val2_2 = double.IsNaN(num4) ? val1_2 : num4;
                    if ( Math.Max( val1_1, val2_1 ) < 0.0 )
                        horizontalOffset -= Math.Max( val1_1, val2_1 );
                    if ( Math.Min( val1_1, val2_1 ) > canvas.ActualWidth )
                        horizontalOffset -= Math.Min( val1_1, val2_1 ) - ( canvas.ActualWidth - 1.0 );
                    if ( Math.Max( val1_2, val2_2 ) < 0.0 )
                        verticalOffset -= Math.Max( val1_2, val2_2 );
                    if ( Math.Min( val1_2, val2_2 ) > canvas.ActualHeight )
                        verticalOffset -= Math.Min( val1_2, val2_2 ) - ( canvas.ActualHeight - 1.0 );
                }
                coordinates.X1Coord += horizontalOffset;
                coordinates.X2Coord += horizontalOffset;
                coordinates.Y1Coord += verticalOffset;
                coordinates.Y2Coord += verticalOffset;
                IComparable[] comparableArray1 = this.FromCoordinates(coordinates.X1Coord, coordinates.Y1Coord);
                this.Annotation.SetCurrentValue( AnnotationBase.X1Property, ( object ) comparableArray1[ 0 ] );
                this.Annotation.SetCurrentValue( AnnotationBase.Y1Property, ( object ) comparableArray1[ 1 ] );
                IComparable[] comparableArray2 = this.FromCoordinates(coordinates.X2Coord, coordinates.Y2Coord);
                this.Annotation.SetCurrentValue( AnnotationBase.X2Property, ( object ) comparableArray2[ 0 ] );
                this.Annotation.SetCurrentValue( AnnotationBase.Y2Property, ( object ) comparableArray2[ 1 ] );
            }
        }

        internal class PolarAnnotationPlacementStrategy : AnnotationBase.PolarAnnotationPlacementStrategyBase<TextAnnotation>
        {
            public PolarAnnotationPlacementStrategy( TextAnnotation annotation )
              : base( annotation )
            {
            }

            public override void PlaceAnnotation( AnnotationCoordinates coordinates )
            {
                AnnotationCoordinates annotationCoordinates = this.GetCartesianAnnotationCoordinates(coordinates);
                Canvas.SetLeft( ( UIElement ) this.Annotation, annotationCoordinates.X1Coord - this.Annotation.HorizontalOffset );
                Canvas.SetTop( ( UIElement ) this.Annotation, annotationCoordinates.Y1Coord - this.Annotation.VerticalOffset );
            }

            public override Point[ ] GetBasePoints( AnnotationCoordinates coordinates )
            {
                AnnotationCoordinates annotationCoordinates = this.GetCartesianAnnotationCoordinates(coordinates);
                return new Point[ 1 ]
                {
          new Point(annotationCoordinates.X1Coord - this.Annotation.HorizontalOffset, annotationCoordinates.Y1Coord - this.Annotation.VerticalOffset)
                };
            }

            protected override bool IsInBoundsInternal( AnnotationCoordinates coordinates, Size canvasSize )
            {
                return ( coordinates.X1Coord < 0.0 || coordinates.X1Coord > canvasSize.Width || coordinates.Y1Coord < 0.0 ? 1 : ( coordinates.Y1Coord > canvasSize.Height ? 1 : 0 ) ) == 0;
            }

            protected override void InternalMoveAnntotationTo( AnnotationCoordinates coordinates, Point x1y1Offset, Point x2y2Offset, Size canvasSize )
            {
                double num1 = coordinates.X1Coord + x1y1Offset.X;
                double num2 = coordinates.X2Coord + x2y2Offset.X;
                double num3 = coordinates.Y1Coord + x1y1Offset.Y;
                double num4 = coordinates.Y2Coord + x2y2Offset.Y;
                if ( !this.IsCoordinateValid( num1, canvasSize.Width ) || !this.IsCoordinateValid( num3, canvasSize.Height ) || ( !this.IsCoordinateValid( num2, canvasSize.Width ) || !this.IsCoordinateValid( num4, canvasSize.Height ) ) )
                {
                    double val1_1 = double.IsNaN(num1) ? 0.0 : num1;
                    double val2_1 = double.IsNaN(num2) ? val1_1 : num2;
                    double val1_2 = double.IsNaN(num3) ? 0.0 : num3;
                    double val2_2 = double.IsNaN(num4) ? val1_2 : num4;
                    if ( Math.Max( val1_1, val2_1 ) < 0.0 )
                    {
                        double num5 = Math.Max(val1_1, val2_1);
                        x1y1Offset.X -= num5;
                        x2y2Offset.X -= num5;
                    }
                    if ( Math.Min( val1_1, val2_1 ) > canvasSize.Width )
                    {
                        double num5 = Math.Min(val1_1, val2_1) - (canvasSize.Width - 1.0);
                        x1y1Offset.X -= num5;
                        x2y2Offset.X -= num5;
                    }
                    if ( Math.Max( val1_2, val2_2 ) < 0.0 )
                    {
                        double num5 = Math.Max(val1_2, val2_2);
                        x1y1Offset.Y -= num5;
                        x2y2Offset.Y -= num5;
                    }
                    if ( Math.Min( val1_2, val2_2 ) > canvasSize.Height )
                    {
                        double num5 = Math.Min(val1_2, val2_2) - (canvasSize.Height - 1.0);
                        x1y1Offset.Y -= num5;
                        x2y2Offset.Y -= num5;
                    }
                }
                coordinates.X1Coord += x1y1Offset.X;
                coordinates.X2Coord += x2y2Offset.X;
                coordinates.Y1Coord += x1y1Offset.Y;
                coordinates.Y2Coord += x2y2Offset.Y;
                IComparable[] comparableArray1 = this.FromCoordinates(coordinates.X1Coord, coordinates.Y1Coord);
                this.Annotation.SetCurrentValue( AnnotationBase.X1Property, ( object ) comparableArray1[ 0 ] );
                this.Annotation.SetCurrentValue( AnnotationBase.Y1Property, ( object ) comparableArray1[ 1 ] );
                IComparable[] comparableArray2 = this.FromCoordinates(coordinates.X2Coord, coordinates.Y2Coord);
                this.Annotation.SetCurrentValue( AnnotationBase.X2Property, ( object ) comparableArray2[ 0 ] );
                this.Annotation.SetCurrentValue( AnnotationBase.Y2Property, ( object ) comparableArray2[ 1 ] );
            }
        }
    }
}
