// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Annotations.LineAnnotation
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
namespace fx.Xaml.Charting
{
    public class LineAnnotation : LineAnnotationBase
    {
        private Line _line;
        private Line _ghostLine;

        public LineAnnotation()
        {
            DefaultStyleKey = ( object ) typeof( LineAnnotation );
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            AnnotationRoot = ( FrameworkElement ) GetAndAssertTemplateChild<Grid>( "PART_LineAnnotationRoot" );
            _line = GetAndAssertTemplateChild<Line>( "PART_Line" );
            _ghostLine = GetAndAssertTemplateChild<Line>( "PART_GhostLine" );
        }

        protected override IAnnotationPlacementStrategy GetCurrentPlacementStrategy()
        {
            if ( XAxis != null && XAxis.IsPolarAxis )
            {
                return ( IAnnotationPlacementStrategy ) new LineAnnotation.PolarAnnotationPlacementStrategy( this );
            }

            return ( IAnnotationPlacementStrategy ) new LineAnnotation.CartesianAnnotationPlacementStrategy( this );
        }

        private static void PlaceLineAnnotation( AnnotationCoordinates coordinates, LineAnnotation annotation )
        {
            annotation._line.X1 = coordinates.X1Coord;
            annotation._line.X2 = coordinates.X2Coord;
            annotation._line.Y1 = coordinates.Y1Coord;
            annotation._line.Y2 = coordinates.Y2Coord;
            annotation._ghostLine.X1 = coordinates.X1Coord;
            annotation._ghostLine.X2 = coordinates.X2Coord;
            annotation._ghostLine.Y1 = coordinates.Y1Coord;
            annotation._ghostLine.Y2 = coordinates.Y2Coord;
        }

        private static Point[ ] CalculateBasePoints( AnnotationCoordinates coordinates )
        {
            return new Point[ 2 ] { new Point( coordinates.X1Coord, coordinates.Y1Coord ), new Point( coordinates.X2Coord, coordinates.Y2Coord ) };
        }

        internal Line Line
        {
            get
            {
                return _line;
            }
        }

        internal Line GhostLine
        {
            get
            {
                return _ghostLine;
            }
        }

        internal class CartesianAnnotationPlacementStrategy : AnnotationBase.CartesianAnnotationPlacementStrategyBase<LineAnnotation>
        {
            public CartesianAnnotationPlacementStrategy( LineAnnotation annotation )
              : base( annotation )
            {
            }

            public override void PlaceAnnotation( AnnotationCoordinates coordinates )
            {
                LineAnnotation.PlaceLineAnnotation( coordinates, Annotation );
            }

            public override Point[ ] GetBasePoints( AnnotationCoordinates coordinates )
            {
                return LineAnnotation.CalculateBasePoints( coordinates );
            }
        }

        internal class PolarAnnotationPlacementStrategy : AnnotationBase.PolarAnnotationPlacementStrategyBase<LineAnnotation>
        {
            public PolarAnnotationPlacementStrategy( LineAnnotation annotation )
              : base( annotation )
            {
            }

            public override void PlaceAnnotation( AnnotationCoordinates coordinates )
            {
                LineAnnotation.PlaceLineAnnotation( GetCartesianAnnotationCoordinates( coordinates ), Annotation );
            }

            public override Point[ ] GetBasePoints( AnnotationCoordinates coordinates )
            {
                return LineAnnotation.CalculateBasePoints( GetCartesianAnnotationCoordinates( coordinates ) );
            }
        }
    }
}
