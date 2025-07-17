//// Decompiled with JetBrains decompiler
//// Type: fx.Xaml.Charting.Annotations.AxisMarkerAnnotation
//// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
//// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
//// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

//using fx.Xaml.Charting;//using fx.Xaml.Charting;//using fx.Xaml.Charting;//using fx.Xaml.Charting;//using fx.Xaml.Charting;//using fx.Xaml.Charting;//using fx.Xaml.Charting;//using System;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Input;

//namespace fx.Xaml.Charting//{
//  public class AxisMarkerAnnotation : AnchorPointAnnotation
//  {
//    public static readonly DependencyProperty FormattedValueProperty = DependencyProperty.Register(nameof (FormattedValue), typeof (string), typeof (AxisMarkerAnnotation), new PropertyMetadata((object) null));
//    public static readonly DependencyProperty MarkerPointWidthProperty = DependencyProperty.Register(nameof (MarkerPointWidth), typeof (double), typeof (AxisMarkerAnnotation), new PropertyMetadata((object) 8.0));
//    public static readonly DependencyProperty LabelTemplateProperty = DependencyProperty.Register(nameof (LabelTemplate), typeof (DataTemplate), typeof (AxisMarkerAnnotation), new PropertyMetadata((object) null));
//    public static readonly DependencyProperty PointerTemplateProperty = DependencyProperty.Register(nameof (PointerTemplate), typeof (DataTemplate), typeof (AxisMarkerAnnotation), new PropertyMetadata((object) null));
//    protected internal static readonly DependencyProperty AxisInfoProperty = DependencyProperty.Register(nameof (AxisInfo), typeof (AxisInfo), typeof (AxisMarkerAnnotation), new PropertyMetadata((object) null));

//    public AxisMarkerAnnotation()
//    {
//      DefaultStyleKey = (object) typeof (AxisMarkerAnnotation);
//      AnnotationCanvas = AnnotationCanvas.YAxis;
//    }

//    public string FormattedValue
//    {
//      get
//      {
//        return (string) GetValue(AxisMarkerAnnotation.FormattedValueProperty);
//      }
//      set
//      {
//        SetValue(AxisMarkerAnnotation.FormattedValueProperty, (object) value);
//      }
//    }

//    public double MarkerPointWidth
//    {
//      get
//      {
//        return (double) GetValue(AxisMarkerAnnotation.MarkerPointWidthProperty);
//      }
//      set
//      {
//        SetValue(AxisMarkerAnnotation.MarkerPointWidthProperty, (object) value);
//      }
//    }

//    public DataTemplate LabelTemplate
//    {
//      get
//      {
//        return (DataTemplate) GetValue(AxisMarkerAnnotation.LabelTemplateProperty);
//      }
//      set
//      {
//        SetValue(AxisMarkerAnnotation.LabelTemplateProperty, (object) value);
//      }
//    }

//    public DataTemplate PointerTemplate
//    {
//      get
//      {
//        return (DataTemplate) GetValue(AxisMarkerAnnotation.PointerTemplateProperty);
//      }
//      set
//      {
//        SetValue(AxisMarkerAnnotation.PointerTemplateProperty, (object) value);
//      }
//    }

//    public IAxis Axis
//    {
//      get
//      {
//        if (AnnotationCanvas != AnnotationCanvas.YAxis)
//          return XAxis;
//        return YAxis;
//      }
//    }

//    public AxisInfo AxisInfo
//    {
//      get
//      {
//        return (AxisInfo) GetValue(AxisMarkerAnnotation.AxisInfoProperty);
//      }
//      private set
//      {
//        SetValue(AxisMarkerAnnotation.AxisInfoProperty, (object) value);
//      }
//    }

//    public override void OnApplyTemplate()
//    {
//      base.OnApplyTemplate();
//      AnnotationRoot = GetAndAssertTemplateChild<FrameworkElement>("PART_AxisMarkerAnnotationRoot");
//    }

//    protected override void OnAxisAlignmentChanged(IAxis axis, AxisAlignment oldAlignment)
//    {
//      base.OnAxisAlignmentChanged(axis, oldAlignment);
//      Cursor selectedCursor = GetSelectedCursor();
//      SetCurrentValue(FrameworkElement.CursorProperty, (object) selectedCursor);
//    }

//    public override void Update(ICoordinateCalculator<double> xCoordinateCalculator, ICoordinateCalculator<double> yCoordinateCalculator)
//    {
//      base.Update(xCoordinateCalculator, yCoordinateCalculator);
//      AxisBase axis = Axis as AxisBase;
//      if (axis == null)
//        return;
//      AxisInfo = axis.HitTest((IComparable) GetValue(GetBaseProperty()));
//    }

//    private DependencyProperty GetBaseProperty()
//    {
//      if (AnnotationCanvas != AnnotationCanvas.XAxis)
//        return AnnotationBase.Y1Property;
//      return AnnotationBase.X1Property;
//    }

//    protected override Cursor GetSelectedCursor()
//    {
//      if (Axis == null || !Axis.IsHorizontalAxis)
//        return Cursors.SizeNS;
//      return Cursors.SizeWE;
//    }

//    protected override double ToCoordinate(IComparable dataValue, double canvasMeasurement, ICoordinateCalculator<double> coordCalc, XyDirection direction)
//    {
//      return base.ToCoordinate(dataValue, canvasMeasurement, coordCalc, direction) - coordCalc.CoordinatesOffset;
//    }

//    public override bool IsPointWithinBounds(Point point)
//    {
//      Grid annotationRoot = AnnotationRoot as Grid;
//      point = ModifierSurface.TranslatePoint(point, (IHitTestable) this);
//      return annotationRoot.IsPointWithinBounds(point);
//    }

//    protected override IAnnotationPlacementStrategy GetCurrentPlacementStrategy()
//    {
//      if (XAxis != null && XAxis.IsPolarAxis)
//        return (IAnnotationPlacementStrategy) new AxisMarkerAnnotation.PolarAnnotationPlacementStrategy(this);
//      return (IAnnotationPlacementStrategy) new AxisMarkerAnnotation.CartesianAnnotationPlacementStrategy(this);
//    }

//    private static void PlaceMarker(IAxis axis, AxisMarkerAnnotation axisMarker, AnnotationCoordinates coordinates)
//    {
//      coordinates = axisMarker.GetAnchorAnnotationCoordinates(coordinates);
//      Point point = new Point(coordinates.X1Coord, coordinates.Y1Coord);
//      AxisMarkerAnnotation.ClearAxisMarkerPlacement((FrameworkElement) axisMarker);
//      if (axis.IsHorizontalAxis)
//      {
//        DependencyProperty dp = axis.AxisAlignment == AxisAlignment.Bottom ? AxisCanvas.TopProperty : AxisCanvas.BottomProperty;
//        axisMarker.SetValue(dp, (object) 0.0);
//        AxisCanvas.SetLeft((UIElement) axisMarker, point.X);
//      }
//      else
//      {
//        DependencyProperty dp = axis.AxisAlignment == AxisAlignment.Right ? AxisCanvas.LeftProperty : AxisCanvas.RightProperty;
//        axisMarker.SetValue(dp, (object) 0.0);
//        AxisCanvas.SetTop((UIElement) axisMarker, point.Y);
//      }
//    }

//    private static void ClearAxisMarkerPlacement(FrameworkElement axisLabel)
//    {
//      axisLabel.SetValue(AxisCanvas.LeftProperty, (object) double.NaN);
//      axisLabel.SetValue(AxisCanvas.RightProperty, (object) double.NaN);
//      axisLabel.SetValue(AxisCanvas.CenterLeftProperty, (object) double.NaN);
//      axisLabel.SetValue(AxisCanvas.TopProperty, (object) double.NaN);
//      axisLabel.SetValue(AxisCanvas.BottomProperty, (object) double.NaN);
//    }

//    private static double CalculateNewPosition(AxisMarkerAnnotation annotation, double currentPosition, double offset, double canvasSize)
//    {
//      double coord = currentPosition + offset;
//      if (!annotation.IsCoordinateValid(coord, canvasSize))
//      {
//        if (coord < 0.0)
//          coord = 0.0;
//        if (coord > canvasSize)
//          coord = canvasSize - 1.0;
//      }
//      return coord;
//    }

//    internal class CartesianAnnotationPlacementStrategy : AnnotationBase.CartesianAnnotationPlacementStrategyBase<AxisMarkerAnnotation>
//    {
//      public CartesianAnnotationPlacementStrategy(AxisMarkerAnnotation annotation)
//        : base(annotation)
//      {
//      }

//      public override Point[] GetBasePoints(AnnotationCoordinates coordinates)
//      {
//        return AxisMarkerAnnotation.CartesianAnnotationPlacementStrategy.CalculateBasePoints(coordinates, Annotation);
//      }

//      private static Point[] CalculateBasePoints(AnnotationCoordinates coordinates, AxisMarkerAnnotation annotation)
//      {
//        coordinates = annotation.GetAnchorAnnotationCoordinates(coordinates);
//        annotation.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
//        IAxis axis = annotation.Axis;
//        IChartModifierSurface modifierSurface = annotation.ModifierSurface;
//        if (axis.IsHorizontalAxis)
//        {
//          double width = annotation.DesiredSize.Width;
//          double x1Coord = coordinates.X1Coord;
//          return new Point[4]{ axis.TranslatePoint(new Point(x1Coord, 0.0), (IHitTestable) modifierSurface), axis.TranslatePoint(new Point(x1Coord, axis.ActualHeight), (IHitTestable) modifierSurface), axis.TranslatePoint(new Point(x1Coord + width, axis.ActualHeight), (IHitTestable) modifierSurface), axis.TranslatePoint(new Point(x1Coord + width, 0.0), (IHitTestable) modifierSurface) };
//        }
//        double height = annotation.DesiredSize.Height;
//        double y1Coord = coordinates.Y1Coord;
//        return new Point[4]{ axis.TranslatePoint(new Point(0.0, y1Coord), (IHitTestable) modifierSurface), axis.TranslatePoint(new Point(axis.ActualWidth, y1Coord), (IHitTestable) modifierSurface), axis.TranslatePoint(new Point(axis.ActualWidth, y1Coord + height), (IHitTestable) modifierSurface), axis.TranslatePoint(new Point(0.0, y1Coord + height), (IHitTestable) modifierSurface) };
//      }

//      public override void PlaceAnnotation(AnnotationCoordinates coordinates)
//      {
//        AxisMarkerAnnotation.PlaceMarker(Annotation.Axis, Annotation, coordinates);
//      }

//      public override bool IsInBounds(AnnotationCoordinates coordinates, IAnnotationCanvas canvas)
//      {
//        bool flag;
//        if (Annotation.Axis.IsHorizontalAxis)
//        {
//          double x1Coord = coordinates.X1Coord;
//          flag = x1Coord >= 0.0 && x1Coord <= canvas.ActualWidth;
//        }
//        else
//        {
//          double y1Coord = coordinates.Y1Coord;
//          flag = y1Coord >= 0.0 && y1Coord <= canvas.ActualHeight;
//        }
//        return flag;
//      }

//      protected override void InternalMoveAnntotationTo(AnnotationCoordinates coordinates, ref double horizontalOffset, ref double verticalOffset, IAnnotationCanvas canvas)
//      {
//        IComparable[] comparableArray = !Annotation.Axis.IsHorizontalAxis ? FromCoordinates(0.0, AxisMarkerAnnotation.CalculateNewPosition(Annotation, coordinates.Y1Coord, verticalOffset, canvas.ActualHeight)) : FromCoordinates(AxisMarkerAnnotation.CalculateNewPosition(Annotation, coordinates.X1Coord, horizontalOffset, canvas.ActualWidth), 0.0);
//        Annotation.SetCurrentValue(Annotation.GetBaseProperty(), Annotation.Axis.IsXAxis ? (object) comparableArray[0] : (object) comparableArray[1]);
//      }
//    }

//    internal class PolarAnnotationPlacementStrategy : AnnotationBase.PolarAnnotationPlacementStrategyBase<AxisMarkerAnnotation>
//    {
//      private readonly double _radius;

//      public PolarAnnotationPlacementStrategy(AxisMarkerAnnotation annotation)
//        : base(annotation)
//      {
//        _radius = PolarUtil.CalculateViewportRadius(TransformationStrategy.ViewportSize);
//      }

//      public override void PlaceAnnotation(AnnotationCoordinates coordinates)
//      {
//        if (Annotation.Axis.IsXAxis)
//        {
//          AxisMarkerAnnotation.ClearAxisMarkerPlacement((FrameworkElement) Annotation);
//          AxisCanvas.SetCenterLeft((UIElement) Annotation, coordinates.X1Coord);
//          AxisCanvas.SetBottom((UIElement) Annotation, 0.0);
//        }
//        else
//        {
//          coordinates.X1Coord = coordinates.Y1Coord;
//          AxisMarkerAnnotation.PlaceMarker(Annotation.Axis, Annotation, coordinates);
//        }
//      }

//      public override bool IsInBounds(AnnotationCoordinates coordinates, IAnnotationCanvas canvas)
//      {
//        if (Annotation.Axis.IsXAxis)
//          return true;
//        bool flag;
//        if (Annotation.Axis.IsHorizontalAxis)
//        {
//          double y1Coord = coordinates.Y1Coord;
//          flag = y1Coord >= 0.0 && y1Coord <= canvas.ActualWidth;
//        }
//        else
//        {
//          double y1Coord = coordinates.Y1Coord;
//          flag = y1Coord >= 0.0 && y1Coord <= canvas.ActualHeight;
//        }
//        return flag;
//      }

//      public override Point[] GetBasePoints(AnnotationCoordinates coordinates)
//      {
//        if (Annotation.Axis.IsXAxis)
//        {
//          AnnotationCoordinates annotationCoordinates = GetCartesianAnnotationCoordinates(coordinates);
//          return new Point[1]{ new Point(annotationCoordinates.X1Coord, annotationCoordinates.Y1Coord) };
//        }
//        coordinates.X1Coord = coordinates.Y1Coord;
//        coordinates = Annotation.GetAnchorAnnotationCoordinates(coordinates);
//        return AxisMarkerAnnotation.PolarAnnotationPlacementStrategy.CalculateBasePoints(coordinates, Annotation);
//      }

//      private static Point[] CalculateBasePoints(AnnotationCoordinates coordinates, AxisMarkerAnnotation annotation)
//      {
//        annotation.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
//        IAxis axis = annotation.Axis;
//        IChartModifierSurface modifierSurface = annotation.ModifierSurface;
//        if (axis.IsHorizontalAxis)
//        {
//          double x = coordinates.X1Coord + annotation.DesiredSize.Width / 2.0;
//          double y = axis.AxisAlignment == AxisAlignment.Top ? axis.ActualHeight : 0.0;
//          return new Point[1]{ axis.TranslatePoint(new Point(x, y), (IHitTestable) modifierSurface) };
//        }
//        double x1 = axis.AxisAlignment == AxisAlignment.Left ? axis.ActualWidth : 0.0;
//        double y1 = coordinates.Y1Coord + annotation.DesiredSize.Height / 2.0;
//        return new Point[1]{ axis.TranslatePoint(new Point(x1, y1), (IHitTestable) modifierSurface) };
//      }

//      protected override Tuple<Point, Point> CalculateAnnotationOffsets(AnnotationCoordinates coordinates, double horizontalOffset, double verticalOffset)
//      {
//        if (Annotation.Axis.IsXAxis)
//          return base.CalculateAnnotationOffsets(coordinates, horizontalOffset, verticalOffset);
//        Point point = new Point(horizontalOffset, verticalOffset);
//        return new Tuple<Point, Point>(point, point);
//      }

//      protected override AnnotationCoordinates GetCartesianAnnotationCoordinates(AnnotationCoordinates coordinates)
//      {
//        if (Annotation.Axis.IsXAxis)
//          coordinates.Y1Coord = coordinates.Y2Coord = _radius;
//        return base.GetCartesianAnnotationCoordinates(coordinates);
//      }

//      public override void MoveAnnotationTo(AnnotationCoordinates coordinates, double horizontalOffset, double verticalOffset, IAnnotationCanvas annotationCanvas)
//      {
//        IAxis axis = Annotation.Axis;
//        if (axis.IsXAxis)
//        {
//          double x = CalculateAnnotationOffsets(coordinates, horizontalOffset, verticalOffset).Item1.X;
//          IComparable[] comparableArray = FromCoordinates(AxisMarkerAnnotation.CalculateNewPosition(Annotation, coordinates.X1Coord, x, 360.0), 0.0);
//          Annotation.SetCurrentValue(AnnotationBase.X1Property, (object) comparableArray[0]);
//        }
//        else
//        {
//          IComparable comparable = Annotation.FromCoordinate(axis.IsHorizontalAxis ? AxisMarkerAnnotation.CalculateNewPosition(Annotation, coordinates.Y1Coord, horizontalOffset, annotationCanvas.ActualWidth) : AxisMarkerAnnotation.CalculateNewPosition(Annotation, coordinates.Y1Coord, verticalOffset, annotationCanvas.ActualHeight), axis);
//          Annotation.SetCurrentValue(AnnotationBase.Y1Property, (object) comparable);
//        }
//      }
//    }
//  }
//}

// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Annotations.AxisMarkerAnnotation
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace fx.Xaml.Charting
{
    public class AxisMarkerAnnotation : AnchorPointAnnotation
    {
        public static readonly DependencyProperty FormattedValueProperty = DependencyProperty.Register(nameof (FormattedValue), typeof (string), typeof (AxisMarkerAnnotation), new PropertyMetadata((object) null));
        public static readonly DependencyProperty MarkerPointWidthProperty = DependencyProperty.Register(nameof (MarkerPointWidth), typeof (double), typeof (AxisMarkerAnnotation), new PropertyMetadata((object) 8.0));
        public static readonly DependencyProperty LabelTemplateProperty = DependencyProperty.Register(nameof (LabelTemplate), typeof (DataTemplate), typeof (AxisMarkerAnnotation), new PropertyMetadata((object) null));
        public static readonly DependencyProperty PointerTemplateProperty = DependencyProperty.Register(nameof (PointerTemplate), typeof (DataTemplate), typeof (AxisMarkerAnnotation), new PropertyMetadata((object) null));
        protected internal static readonly DependencyProperty AxisInfoProperty = DependencyProperty.Register(nameof (AxisInfo), typeof (AxisInfo), typeof (AxisMarkerAnnotation), new PropertyMetadata((object) null));

        public AxisMarkerAnnotation()
        {
            DefaultStyleKey = ( object ) typeof( AxisMarkerAnnotation );
            AnnotationCanvas = AnnotationCanvas.YAxis;
        }

        public string FormattedValue
        {
            get
            {
                return ( string ) GetValue( AxisMarkerAnnotation.FormattedValueProperty );
            }
            set
            {
                SetValue( AxisMarkerAnnotation.FormattedValueProperty, ( object ) value );
            }
        }

        public double MarkerPointWidth
        {
            get
            {
                return ( double ) GetValue( AxisMarkerAnnotation.MarkerPointWidthProperty );
            }
            set
            {
                SetValue( AxisMarkerAnnotation.MarkerPointWidthProperty, ( object ) value );
            }
        }

        public DataTemplate LabelTemplate
        {
            get
            {
                return ( DataTemplate ) GetValue( AxisMarkerAnnotation.LabelTemplateProperty );
            }
            set
            {
                SetValue( AxisMarkerAnnotation.LabelTemplateProperty, ( object ) value );
            }
        }

        public DataTemplate PointerTemplate
        {
            get
            {
                return ( DataTemplate ) GetValue( AxisMarkerAnnotation.PointerTemplateProperty );
            }
            set
            {
                SetValue( AxisMarkerAnnotation.PointerTemplateProperty, ( object ) value );
            }
        }

        public IAxis Axis
        {
            get
            {
                if ( AnnotationCanvas != AnnotationCanvas.YAxis )
                {
                    return XAxis;
                }

                return YAxis;
            }
        }

        public AxisInfo AxisInfo
        {
            get
            {
                return ( AxisInfo ) GetValue( AxisMarkerAnnotation.AxisInfoProperty );
            }
            private set
            {
                SetValue( AxisMarkerAnnotation.AxisInfoProperty, ( object ) value );
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            AnnotationRoot = GetAndAssertTemplateChild<FrameworkElement>( "PART_AxisMarkerAnnotationRoot" );
        }

        protected override void OnAxisAlignmentChanged( IAxis axis, AxisAlignment oldAlignment )
        {
            base.OnAxisAlignmentChanged( axis, oldAlignment );
            Cursor selectedCursor = GetSelectedCursor();
            SetCurrentValue( FrameworkElement.CursorProperty, ( object ) selectedCursor );
        }

        public override void Update( ICoordinateCalculator<double> xCoordinateCalculator, ICoordinateCalculator<double> yCoordinateCalculator )
        {
            base.Update( xCoordinateCalculator, yCoordinateCalculator );
            AxisBase axis = Axis as AxisBase;
            if ( axis == null )
            {
                return;
            }

            AxisInfo = axis.HitTest( ( IComparable ) GetValue( GetBaseProperty() ) );
        }

        private DependencyProperty GetBaseProperty()
        {
            if ( AnnotationCanvas != AnnotationCanvas.XAxis )
            {
                return AnnotationBase.Y1Property;
            }

            return AnnotationBase.X1Property;
        }

        protected override Cursor GetSelectedCursor()
        {
            if ( Axis == null || !Axis.IsHorizontalAxis )
            {
                return Cursors.SizeNS;
            }

            return Cursors.SizeWE;
        }

        protected override double ToCoordinate( IComparable dataValue, double canvasMeasurement, ICoordinateCalculator<double> coordCalc, XyDirection direction )
        {
            return base.ToCoordinate( dataValue, canvasMeasurement, coordCalc, direction ) - coordCalc.CoordinatesOffset;
        }

        public override bool IsPointWithinBounds( Point point )
        {
            Grid annotationRoot = AnnotationRoot as Grid;
            point = ModifierSurface.TranslatePoint( point, ( IHitTestable ) this );
            Point point1 = point;
            return annotationRoot.IsPointWithinBounds( point1 );
        }

        protected override IAnnotationPlacementStrategy GetCurrentPlacementStrategy()
        {
            if ( XAxis != null && XAxis.IsPolarAxis )
            {
                return ( IAnnotationPlacementStrategy ) new AxisMarkerAnnotation.PolarAnnotationPlacementStrategy( this );
            }

            return ( IAnnotationPlacementStrategy ) new AxisMarkerAnnotation.CartesianAnnotationPlacementStrategy( this );
        }

        private static void PlaceMarker( IAxis axis, AxisMarkerAnnotation axisMarker, AnnotationCoordinates coordinates )
        {
            coordinates = axisMarker.GetAnchorAnnotationCoordinates( coordinates );
            Point point = new Point(coordinates.X1Coord, coordinates.Y1Coord);
            AxisMarkerAnnotation.ClearAxisMarkerPlacement( ( FrameworkElement ) axisMarker );
            if ( axis == null )
            {
                return;
            }

            if ( axis.IsHorizontalAxis )
            {
                DependencyProperty dp = axis.AxisAlignment == AxisAlignment.Bottom ? AxisCanvas.TopProperty : AxisCanvas.BottomProperty;
                axisMarker.SetValue( dp, ( object ) 0.0 );
                AxisCanvas.SetLeft( ( UIElement ) axisMarker, point.X );
            }
            else
            {
                DependencyProperty dp = axis.AxisAlignment == AxisAlignment.Right ? AxisCanvas.LeftProperty : AxisCanvas.RightProperty;
                axisMarker.SetValue( dp, ( object ) 0.0 );
                AxisCanvas.SetTop( ( UIElement ) axisMarker, point.Y );
            }
        }

        private static void ClearAxisMarkerPlacement( FrameworkElement axisLabel )
        {
            axisLabel.SetValue( AxisCanvas.LeftProperty, ( object ) double.NaN );
            axisLabel.SetValue( AxisCanvas.RightProperty, ( object ) double.NaN );
            axisLabel.SetValue( AxisCanvas.CenterLeftProperty, ( object ) double.NaN );
            axisLabel.SetValue( AxisCanvas.TopProperty, ( object ) double.NaN );
            axisLabel.SetValue( AxisCanvas.BottomProperty, ( object ) double.NaN );
        }

        private static double CalculateNewPosition( AxisMarkerAnnotation annotation, double currentPosition, double offset, double canvasSize )
        {
            double coord = currentPosition + offset;
            if ( !annotation.IsCoordinateValid( coord, canvasSize ) )
            {
                if ( coord < 0.0 )
                {
                    coord = 0.0;
                }

                if ( coord > canvasSize )
                {
                    coord = canvasSize - 1.0;
                }
            }
            return coord;
        }

        internal class CartesianAnnotationPlacementStrategy : AnnotationBase.CartesianAnnotationPlacementStrategyBase<AxisMarkerAnnotation>
        {
            public CartesianAnnotationPlacementStrategy( AxisMarkerAnnotation annotation )
              : base( annotation )
            {
            }

            public override Point[ ] GetBasePoints( AnnotationCoordinates coordinates )
            {
                return AxisMarkerAnnotation.CartesianAnnotationPlacementStrategy.CalculateBasePoints( coordinates, Annotation );
            }

            private static Point[ ] CalculateBasePoints( AnnotationCoordinates coordinates, AxisMarkerAnnotation annotation )
            {
                coordinates = annotation.GetAnchorAnnotationCoordinates( coordinates );
                annotation.Measure( new Size( double.PositiveInfinity, double.PositiveInfinity ) );
                IAxis axis = annotation.Axis;
                if ( axis == null )
                {
                    return new Point[ 0 ];
                }

                IChartModifierSurface modifierSurface = annotation.ModifierSurface;
                if ( axis.IsHorizontalAxis )
                {
                    double width = annotation.DesiredSize.Width;
                    double x1Coord = coordinates.X1Coord;
                    return new Point[ 4 ]
                    {
            axis.TranslatePoint(new Point(x1Coord, 0.0), (IHitTestable) modifierSurface),
            axis.TranslatePoint(new Point(x1Coord, axis.ActualHeight), (IHitTestable) modifierSurface),
            axis.TranslatePoint(new Point(x1Coord + width, axis.ActualHeight), (IHitTestable) modifierSurface),
            axis.TranslatePoint(new Point(x1Coord + width, 0.0), (IHitTestable) modifierSurface)
                    };
                }
                double height = annotation.DesiredSize.Height;
                double y1Coord = coordinates.Y1Coord;
                return new Point[ 4 ]
                {
          axis.TranslatePoint(new Point(0.0, y1Coord), (IHitTestable) modifierSurface),
          axis.TranslatePoint(new Point(axis.ActualWidth, y1Coord), (IHitTestable) modifierSurface),
          axis.TranslatePoint(new Point(axis.ActualWidth, y1Coord + height), (IHitTestable) modifierSurface),
          axis.TranslatePoint(new Point(0.0, y1Coord + height), (IHitTestable) modifierSurface)
                };
            }

            public override void PlaceAnnotation( AnnotationCoordinates coordinates )
            {
                AxisMarkerAnnotation.PlaceMarker( Annotation.Axis, Annotation, coordinates );
            }

            public override bool IsInBounds( AnnotationCoordinates coordinates, IAnnotationCanvas canvas )
            {
                if ( Annotation.Axis == null )
                {
                    return false;
                }

                bool flag;
                if ( Annotation.Axis.IsHorizontalAxis )
                {
                    double x1Coord = coordinates.X1Coord;
                    flag = x1Coord >= 0.0 && x1Coord <= canvas.ActualWidth;
                }
                else
                {
                    double y1Coord = coordinates.Y1Coord;
                    flag = y1Coord >= 0.0 && y1Coord <= canvas.ActualHeight;
                }
                return flag;
            }

            protected override void InternalMoveAnntotationTo( AnnotationCoordinates coordinates, ref double horizontalOffset, ref double verticalOffset, IAnnotationCanvas canvas )
            {
                if ( Annotation.Axis == null )
                {
                    return;
                }

                IComparable[] comparableArray = !Annotation.Axis.IsHorizontalAxis ? FromCoordinates(0.0, AxisMarkerAnnotation.CalculateNewPosition(Annotation, coordinates.Y1Coord, verticalOffset, canvas.ActualHeight)) : FromCoordinates(AxisMarkerAnnotation.CalculateNewPosition(Annotation, coordinates.X1Coord, horizontalOffset, canvas.ActualWidth), 0.0);
                Annotation.SetCurrentValue( Annotation.GetBaseProperty(), Annotation.Axis.IsXAxis ? ( object ) comparableArray[ 0 ] : ( object ) comparableArray[ 1 ] );
            }
        }

        internal class PolarAnnotationPlacementStrategy : AnnotationBase.PolarAnnotationPlacementStrategyBase<AxisMarkerAnnotation>
        {
            private readonly double _radius;

            public PolarAnnotationPlacementStrategy( AxisMarkerAnnotation annotation )
              : base( annotation )
            {
                _radius = PolarUtil.CalculateViewportRadius( TransformationStrategy.ViewportSize );
            }

            public override void PlaceAnnotation( AnnotationCoordinates coordinates )
            {
                if ( Annotation.Axis == null )
                {
                    return;
                }

                if ( Annotation.Axis.IsXAxis )
                {
                    AxisMarkerAnnotation.ClearAxisMarkerPlacement( ( FrameworkElement ) Annotation );
                    AxisCanvas.SetCenterLeft( ( UIElement ) Annotation, coordinates.X1Coord );
                    AxisCanvas.SetBottom( ( UIElement ) Annotation, 0.0 );
                }
                else
                {
                    coordinates.X1Coord = coordinates.Y1Coord;
                    AxisMarkerAnnotation.PlaceMarker( Annotation.Axis, Annotation, coordinates );
                }
            }

            public override bool IsInBounds( AnnotationCoordinates coordinates, IAnnotationCanvas canvas )
            {
                if ( Annotation.Axis == null )
                {
                    return false;
                }

                if ( Annotation.Axis.IsXAxis )
                {
                    return true;
                }

                bool flag;
                if ( Annotation.Axis.IsHorizontalAxis )
                {
                    double y1Coord = coordinates.Y1Coord;
                    flag = y1Coord >= 0.0 && y1Coord <= canvas.ActualWidth;
                }
                else
                {
                    double y1Coord = coordinates.Y1Coord;
                    flag = y1Coord >= 0.0 && y1Coord <= canvas.ActualHeight;
                }
                return flag;
            }

            public override Point[ ] GetBasePoints( AnnotationCoordinates coordinates )
            {
                if ( Annotation.Axis == null )
                {
                    return new Point[ 0 ];
                }

                if ( Annotation.Axis.IsXAxis )
                {
                    AnnotationCoordinates annotationCoordinates = GetCartesianAnnotationCoordinates(coordinates);
                    return new Point[ 1 ]
                    {
            new Point(annotationCoordinates.X1Coord, annotationCoordinates.Y1Coord)
                    };
                }
                coordinates.X1Coord = coordinates.Y1Coord;
                coordinates = Annotation.GetAnchorAnnotationCoordinates( coordinates );
                return AxisMarkerAnnotation.PolarAnnotationPlacementStrategy.CalculateBasePoints( coordinates, Annotation );
            }

            private static Point[ ] CalculateBasePoints( AnnotationCoordinates coordinates, AxisMarkerAnnotation annotation )
            {
                annotation.Measure( new Size( double.PositiveInfinity, double.PositiveInfinity ) );
                IAxis axis = annotation.Axis;
                IChartModifierSurface modifierSurface = annotation.ModifierSurface;
                if ( axis.IsHorizontalAxis )
                {
                    double x = coordinates.X1Coord + annotation.DesiredSize.Width / 2.0;
                    double y = axis.AxisAlignment == AxisAlignment.Top ? axis.ActualHeight : 0.0;
                    return new Point[ 1 ]
                    {
            axis.TranslatePoint(new Point(x, y), (IHitTestable) modifierSurface)
                    };
                }
                double x1 = axis.AxisAlignment == AxisAlignment.Left ? axis.ActualWidth : 0.0;
                double y1 = coordinates.Y1Coord + annotation.DesiredSize.Height / 2.0;
                return new Point[ 1 ]
                {
          axis.TranslatePoint(new Point(x1, y1), (IHitTestable) modifierSurface)
                };
            }

            protected override Tuple<Point, Point> CalculateAnnotationOffsets( AnnotationCoordinates coordinates, double horizontalOffset, double verticalOffset )
            {
                if ( Annotation.Axis.IsXAxis )
                {
                    return base.CalculateAnnotationOffsets( coordinates, horizontalOffset, verticalOffset );
                }

                Point point = new Point(horizontalOffset, verticalOffset);
                return new Tuple<Point, Point>( point, point );
            }

            protected override AnnotationCoordinates GetCartesianAnnotationCoordinates( AnnotationCoordinates coordinates )
            {
                if ( Annotation.Axis.IsXAxis )
                {
                    coordinates.Y1Coord = coordinates.Y2Coord = _radius;
                }

                return base.GetCartesianAnnotationCoordinates( coordinates );
            }

            public override void MoveAnnotationTo( AnnotationCoordinates coordinates, double horizontalOffset, double verticalOffset, IAnnotationCanvas annotationCanvas )
            {
                IAxis axis = Annotation.Axis;
                if ( axis.IsXAxis )
                {
                    double x = CalculateAnnotationOffsets(coordinates, horizontalOffset, verticalOffset).Item1.X;
                    IComparable[] comparableArray = FromCoordinates(AxisMarkerAnnotation.CalculateNewPosition(Annotation, coordinates.X1Coord, x, 360.0), 0.0);
                    Annotation.SetCurrentValue( AnnotationBase.X1Property, ( object ) comparableArray[ 0 ] );
                }
                else
                {
                    IComparable comparable = Annotation.FromCoordinate(axis.IsHorizontalAxis ? AxisMarkerAnnotation.CalculateNewPosition(Annotation, coordinates.Y1Coord, horizontalOffset, annotationCanvas.ActualWidth) : AxisMarkerAnnotation.CalculateNewPosition(Annotation, coordinates.Y1Coord, verticalOffset, annotationCanvas.ActualHeight), axis);
                    Annotation.SetCurrentValue( AnnotationBase.Y1Property, ( object ) comparable );
                }
            }
        }
    }
}

