// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Annotations.LineArrowAnnotation
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using StockSharp.Xaml.Charting.Numerics.CoordinateCalculators;
using StockSharp.Xaml.Charting.StrategyManager;
using StockSharp.Xaml.Charting.Utility;

namespace StockSharp.Xaml.Charting.Visuals.Annotations
{
    public class LineArrowAnnotation : LineAnnotationBase
    {
        public static readonly DependencyProperty HeadLengthProperty = DependencyProperty.Register(nameof (HeadLength), typeof (double), typeof (LineArrowAnnotation), new PropertyMetadata((object) 4.0, new PropertyChangedCallback(AnnotationBase.OnRenderablePropertyChanged)));
        public static readonly DependencyProperty HeadWidthProperty = DependencyProperty.Register(nameof (HeadWidth), typeof (double), typeof (LineArrowAnnotation), new PropertyMetadata((object) 8.0, new PropertyChangedCallback(AnnotationBase.OnRenderablePropertyChanged)));
        private System.Windows.Shapes.Line _line;
        private System.Windows.Shapes.Line _ghostLine;
        private Polygon _arrowHead;

        public LineArrowAnnotation()
        {
            this.DefaultStyleKey = ( object ) typeof( LineArrowAnnotation );
        }

        public double HeadLength
        {
            get
            {
                return ( double ) this.GetValue( LineArrowAnnotation.HeadLengthProperty );
            }
            set
            {
                this.SetValue( LineArrowAnnotation.HeadLengthProperty, ( object ) value );
            }
        }

        public double HeadWidth
        {
            get
            {
                return ( double ) this.GetValue( LineArrowAnnotation.HeadWidthProperty );
            }
            set
            {
                this.SetValue( LineArrowAnnotation.HeadWidthProperty, ( object ) value );
            }
        }

        public override void OnApplyTemplate()
        {
            this.AnnotationRoot = ( FrameworkElement ) this.GetAndAssertTemplateChild<Grid>( "PART_LineArrowAnnotationRoot" );
            this._line = this.GetAndAssertTemplateChild<System.Windows.Shapes.Line>( "PART_Line" );
            this._ghostLine = this.GetAndAssertTemplateChild<System.Windows.Shapes.Line>( "PART_GhostLine" );
            this._arrowHead = this.GetAndAssertTemplateChild<Polygon>( "PART_ArrowHead" );
        }

        public override bool IsPointWithinBounds( Point point )
        {
            IAnnotationCanvas canvas = base.GetCanvas(base.AnnotationCanvas);
            if ( this.XAxis == null || this.YAxis == null )
            {
                return false;
            }
            ICoordinateCalculator<double> currentCoordinateCalculator = this.XAxis.GetCurrentCoordinateCalculator();
            ICoordinateCalculator<double> coordinateCalculator = this.YAxis.GetCurrentCoordinateCalculator();
            AnnotationCoordinates coordinates = base.GetCoordinates(canvas, currentCoordinateCalculator, coordinateCalculator);
            Point point1 = new Point(coordinates.X1Coord, coordinates.Y1Coord);
            Point point2 = new Point(coordinates.X2Coord, coordinates.Y2Coord);
            Point[] headPoints = this.GetHeadPoints(point1, point2, this.HeadLength, this.HeadWidth);
            if ( base.IsPointWithinBounds( point ) )
            {
                return true;
            }
            return PointUtil.IsPointInTriangle( point, headPoints[ 0 ], headPoints[ 1 ], point2 );
        }


        internal Point[ ] GetHeadPoints( Point pt1, Point pt2, double headLength, double headWidth )
        {
            double num1 = Math.Atan2(pt1.Y - pt2.Y, pt1.X - pt2.X);
            double num2 = Math.Sin(num1);
            double num3 = Math.Cos(num1);
            Point point1 = new Point(pt2.X + (this.HeadWidth * num3 - this.HeadLength * num2), pt2.Y + (this.HeadWidth * num2 + this.HeadLength * num3));
            Point point2 = new Point(pt2.X + (this.HeadWidth * num3 + this.HeadLength * num2), pt2.Y + (this.HeadWidth * num2 - this.HeadLength * num3));
            return new Point[ 3 ] { pt2, point1, point2 };
        }

        protected override IAnnotationPlacementStrategy GetCurrentPlacementStrategy()
        {
            if ( this.XAxis != null && this.XAxis.IsPolarAxis )
            {
                return ( IAnnotationPlacementStrategy ) new LineArrowAnnotation.PolarAnnotationPlacementStrategy( this );
            }

            return ( IAnnotationPlacementStrategy ) new LineArrowAnnotation.CartesianAnnotationPlacementStrategy( this );
        }

        private static void PlaceLineArrowAnnotation( AnnotationCoordinates coordinates, LineArrowAnnotation annotation )
        {
            annotation._line.X1 = coordinates.X1Coord;
            annotation._line.X2 = coordinates.X2Coord;
            annotation._line.Y1 = coordinates.Y1Coord;
            annotation._line.Y2 = coordinates.Y2Coord;
            annotation._ghostLine.X1 = coordinates.X1Coord;
            annotation._ghostLine.X2 = coordinates.X2Coord;
            annotation._ghostLine.Y1 = coordinates.Y1Coord;
            annotation._ghostLine.Y2 = coordinates.Y2Coord;
            Point[] headPoints = annotation.GetHeadPoints(new Point(coordinates.X1Coord, coordinates.Y1Coord), new Point(coordinates.X2Coord, coordinates.Y2Coord), annotation.HeadLength, annotation.HeadWidth);
            annotation._arrowHead.Points = new PointCollection()
      {
        headPoints[0],
        headPoints[1],
        headPoints[2]
      };
        }

        private static Point[ ] CalculateBasePoints( AnnotationCoordinates coordinates )
        {
            return new Point[ 2 ] { new Point( coordinates.X1Coord, coordinates.Y1Coord ), new Point( coordinates.X2Coord, coordinates.Y2Coord ) };
        }

        internal System.Windows.Shapes.Line Line
        {
            get
            {
                return this._line;
            }
        }

        internal System.Windows.Shapes.Line GhostLine
        {
            get
            {
                return this._ghostLine;
            }
        }

        internal Polygon ArrowHead
        {
            get
            {
                return this._arrowHead;
            }
        }

        internal class CartesianAnnotationPlacementStrategy : AnnotationBase.CartesianAnnotationPlacementStrategyBase<LineArrowAnnotation>
        {
            public CartesianAnnotationPlacementStrategy( LineArrowAnnotation annotation )
              : base( annotation )
            {
            }

            public override void PlaceAnnotation( AnnotationCoordinates coordinates )
            {
                LineArrowAnnotation.PlaceLineArrowAnnotation( coordinates, this.Annotation );
            }

            public override Point[ ] GetBasePoints( AnnotationCoordinates coordinates )
            {
                return LineArrowAnnotation.CalculateBasePoints( coordinates );
            }
        }

        internal class PolarAnnotationPlacementStrategy : AnnotationBase.PolarAnnotationPlacementStrategyBase<LineArrowAnnotation>
        {
            public PolarAnnotationPlacementStrategy( LineArrowAnnotation annotation )
              : base( annotation )
            {
            }

            public override void PlaceAnnotation( AnnotationCoordinates coordinates )
            {
                LineArrowAnnotation.PlaceLineArrowAnnotation( this.GetCartesianAnnotationCoordinates( coordinates ), this.Annotation );
            }

            public override Point[ ] GetBasePoints( AnnotationCoordinates coordinates )
            {
                return LineArrowAnnotation.CalculateBasePoints( this.GetCartesianAnnotationCoordinates( coordinates ) );
            }
        }
    }
}
