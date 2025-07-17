//// Decompiled with JetBrains decompiler
//// Type: Ecng.Xaml.Charting.Visuals.Annotations.BoxAnnotation
//// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
//// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
//// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

//using Ecng.Xaml.Charting.StrategyManager;
//using Ecng.Xaml.Charting.Utility;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Input;

//namespace Ecng.Xaml.Charting.Visuals.Annotations
//{
//  public class BoxAnnotation : AnnotationBase
//  {
//    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof (CornerRadius), typeof (CornerRadius), typeof (BoxAnnotation), new PropertyMetadata((object) new CornerRadius()));

//    public BoxAnnotation()
//    {
//      DefaultStyleKey = (object) typeof (BoxAnnotation);
//    }

//    public CornerRadius CornerRadius
//    {
//      get
//      {
//        return (CornerRadius) GetValue(BoxAnnotation.CornerRadiusProperty);
//      }
//      set
//      {
//        SetValue(BoxAnnotation.CornerRadiusProperty, (object) value);
//      }
//    }

//    public override void OnApplyTemplate()
//    {
//      base.OnApplyTemplate();
//      AnnotationRoot = (FrameworkElement) GetAndAssertTemplateChild<Border>("PART_BoxAnnotationRoot");
//    }

//    protected override Cursor GetSelectedCursor()
//    {
//      return Cursors.SizeAll;
//    }

//    public override bool IsPointWithinBounds(Point point)
//    {
//      AnnotationCoordinates coordinates = GetCoordinates(GetCanvas(AnnotationCanvas), XAxis.GetCurrentCoordinateCalculator(), YAxis.GetCurrentCoordinateCalculator());
//      return new Rect(new Point(coordinates.X1Coord, coordinates.Y1Coord), new Point(coordinates.X2Coord, coordinates.Y2Coord)).Contains(point);
//    }

//    protected override IAnnotationPlacementStrategy GetCurrentPlacementStrategy()
//    {
//      if (XAxis != null && XAxis.IsPolarAxis)
//        return (IAnnotationPlacementStrategy) new BoxAnnotation.PolarAnnotationPlacementStrategy(this);
//      return (IAnnotationPlacementStrategy) new BoxAnnotation.CartesianAnnotationPlacementStrategy(this);
//    }

//    private static void PlaceBoxAnnotation(BoxAnnotation annotation, AnnotationCoordinates coordinates)
//    {
//      double x1Coord = coordinates.X1Coord;
//      double x2Coord = coordinates.X2Coord;
//      double y1Coord = coordinates.Y1Coord;
//      double y2Coord = coordinates.Y2Coord;
//      if (x2Coord < x1Coord)
//        NumberUtil.Swap(ref x1Coord, ref x2Coord);
//      if (y2Coord < y1Coord)
//        NumberUtil.Swap(ref y1Coord, ref y2Coord);
//      annotation.Width = x2Coord - x1Coord + 1.0;
//      annotation.Height = y2Coord - y1Coord + 1.0;
//      Canvas.SetLeft((UIElement) annotation, x1Coord);
//      Canvas.SetTop((UIElement) annotation, y1Coord);
//    }

//    internal class CartesianAnnotationPlacementStrategy : AnnotationBase.CartesianAnnotationPlacementStrategyBase<BoxAnnotation>
//    {
//      public CartesianAnnotationPlacementStrategy(BoxAnnotation annotation)
//        : base(annotation)
//      {
//      }

//      public override Point[] GetBasePoints(AnnotationCoordinates coordinates)
//      {
//        return new Point[4]{ new Point(coordinates.X1Coord, coordinates.Y1Coord), new Point(coordinates.X2Coord, coordinates.Y1Coord), new Point(coordinates.X2Coord, coordinates.Y2Coord), new Point(coordinates.X1Coord, coordinates.Y2Coord) };
//      }

//      public override void PlaceAnnotation(AnnotationCoordinates coordinates)
//      {
//        BoxAnnotation.PlaceBoxAnnotation(Annotation, coordinates);
//      }
//    }

//    internal class PolarAnnotationPlacementStrategy : AnnotationBase.PolarAnnotationPlacementStrategyBase<BoxAnnotation>
//    {
//      public PolarAnnotationPlacementStrategy(BoxAnnotation annotation)
//        : base(annotation)
//      {
//      }

//      public override void PlaceAnnotation(AnnotationCoordinates coordinates)
//      {
//        coordinates = GetCartesianAnnotationCoordinates(coordinates);
//        BoxAnnotation.PlaceBoxAnnotation(Annotation, coordinates);
//      }

//      public override void SetBasePoint(Point newPoint, int index)
//      {
//        if (index == 1)
//          index = 2;
//        base.SetBasePoint(newPoint, index);
//      }

//      public override Point[] GetBasePoints(AnnotationCoordinates coordinates)
//      {
//        AnnotationCoordinates annotationCoordinates = GetCartesianAnnotationCoordinates(coordinates);
//        return new Point[2]{ new Point(annotationCoordinates.X1Coord, annotationCoordinates.Y1Coord), new Point(annotationCoordinates.X2Coord, annotationCoordinates.Y2Coord) };
//      }
//    }
//  }
//}


// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Visuals.Annotations.BoxAnnotation
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ecng.Xaml.Charting.Numerics.CoordinateCalculators;
using Ecng.Xaml.Charting.StrategyManager;
using Ecng.Xaml.Charting.Utility;

namespace Ecng.Xaml.Charting.Visuals.Annotations
{
    public class BoxAnnotation : AnnotationBase
    {
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof (CornerRadius), typeof (CornerRadius), typeof (BoxAnnotation), new PropertyMetadata((object) new CornerRadius()));

        public BoxAnnotation()
        {
            DefaultStyleKey = ( object ) typeof( BoxAnnotation );
        }

        public CornerRadius CornerRadius
        {
            get
            {
                return ( CornerRadius ) GetValue( BoxAnnotation.CornerRadiusProperty );
            }
            set
            {
                SetValue( BoxAnnotation.CornerRadiusProperty, ( object ) value );
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            AnnotationRoot = ( FrameworkElement ) GetAndAssertTemplateChild<Border>( "PART_BoxAnnotationRoot" );
        }

        protected override Cursor GetSelectedCursor()
        {
            return Cursors.SizeAll;
        }

        public override bool IsPointWithinBounds( Point point )
        {
            IAnnotationCanvas canvas = GetCanvas(AnnotationCanvas);
            if ( XAxis == null || YAxis == null )
                return false;
            ICoordinateCalculator<double> coordinateCalculator1 = XAxis.GetCurrentCoordinateCalculator();
            ICoordinateCalculator<double> coordinateCalculator2 = YAxis.GetCurrentCoordinateCalculator();
            AnnotationCoordinates coordinates = GetCoordinates(canvas, coordinateCalculator1, coordinateCalculator2);
            return new Rect( new Point( coordinates.X1Coord, coordinates.Y1Coord ), new Point( coordinates.X2Coord, coordinates.Y2Coord ) ).Contains( point );
        }

        protected override IAnnotationPlacementStrategy GetCurrentPlacementStrategy()
        {
            if ( XAxis != null && XAxis.IsPolarAxis )
                return ( IAnnotationPlacementStrategy ) new BoxAnnotation.PolarAnnotationPlacementStrategy( this );
            return ( IAnnotationPlacementStrategy ) new BoxAnnotation.CartesianAnnotationPlacementStrategy( this );
        }

        private static void PlaceBoxAnnotation( BoxAnnotation annotation, AnnotationCoordinates coordinates )
        {
            double x1Coord = coordinates.X1Coord;
            double x2Coord = coordinates.X2Coord;
            double y1Coord = coordinates.Y1Coord;
            double y2Coord = coordinates.Y2Coord;
            if ( x2Coord < x1Coord )
                NumberUtil.Swap( ref x1Coord, ref x2Coord );
            if ( y2Coord < y1Coord )
                NumberUtil.Swap( ref y1Coord, ref y2Coord );
            annotation.Width = x2Coord - x1Coord + 1.0;
            annotation.Height = y2Coord - y1Coord + 1.0;
            Canvas.SetLeft( ( UIElement ) annotation, x1Coord );
            Canvas.SetTop( ( UIElement ) annotation, y1Coord );
        }

        internal class CartesianAnnotationPlacementStrategy : AnnotationBase.CartesianAnnotationPlacementStrategyBase<BoxAnnotation>
        {
            public CartesianAnnotationPlacementStrategy( BoxAnnotation annotation )
              : base( annotation )
            {
            }

            public override Point[ ] GetBasePoints( AnnotationCoordinates coordinates )
            {
                return new Point[ 4 ]
                {
          new Point(coordinates.X1Coord, coordinates.Y1Coord),
          new Point(coordinates.X2Coord, coordinates.Y1Coord),
          new Point(coordinates.X2Coord, coordinates.Y2Coord),
          new Point(coordinates.X1Coord, coordinates.Y2Coord)
                };
            }

            public override void PlaceAnnotation( AnnotationCoordinates coordinates )
            {
                BoxAnnotation.PlaceBoxAnnotation( Annotation, coordinates );
            }
        }

        internal class PolarAnnotationPlacementStrategy : AnnotationBase.PolarAnnotationPlacementStrategyBase<BoxAnnotation>
        {
            public PolarAnnotationPlacementStrategy( BoxAnnotation annotation )
              : base( annotation )
            {
            }

            public override void PlaceAnnotation( AnnotationCoordinates coordinates )
            {
                coordinates = GetCartesianAnnotationCoordinates( coordinates );
                BoxAnnotation.PlaceBoxAnnotation( Annotation, coordinates );
            }

            public override void SetBasePoint( Point newPoint, int index )
            {
                if ( index == 1 )
                    index = 2;
                base.SetBasePoint( newPoint, index );
            }

            public override Point[ ] GetBasePoints( AnnotationCoordinates coordinates )
            {
                AnnotationCoordinates annotationCoordinates = GetCartesianAnnotationCoordinates(coordinates);
                return new Point[ 2 ]
                {
          new Point(annotationCoordinates.X1Coord, annotationCoordinates.Y1Coord),
          new Point(annotationCoordinates.X2Coord, annotationCoordinates.Y2Coord)
                };
            }
        }
    }
}
