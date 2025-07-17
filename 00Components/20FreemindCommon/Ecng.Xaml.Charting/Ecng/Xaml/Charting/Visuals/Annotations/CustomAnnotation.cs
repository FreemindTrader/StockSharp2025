// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Annotations.CustomAnnotation
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Controls;
namespace Ecng.Xaml.Charting
{
    public class CustomAnnotation : AnchorPointAnnotation
    {
        public CustomAnnotation()
        {
            this.DefaultStyleKey = ( object ) typeof( CustomAnnotation );
            this.AnnotationRoot = ( FrameworkElement ) this;
        }

        protected override void OnContentChanged( object oldContent, object newContent )
        {
            base.OnContentChanged( oldContent, newContent );
            this.AnnotationRoot = newContent as FrameworkElement ?? ( FrameworkElement ) this;
            this.Refresh();
        }

        protected override void OnContentTemplateChanged( DataTemplate oldContentTemplate, DataTemplate newContentTemplate )
        {
            base.OnContentTemplateChanged( oldContentTemplate, newContentTemplate );
            this.AnnotationRoot = ( FrameworkElement ) this.Content ?? ( FrameworkElement ) this;
            this.Refresh();
        }

        public override bool IsPointWithinBounds( Point point )
        {
            FrameworkElement annotationRoot = this.AnnotationRoot;
            if ( annotationRoot != null )
            {
                return annotationRoot.IsPointWithinBounds( point );
            }

            return false;
        }

        protected override IAnnotationPlacementStrategy GetCurrentPlacementStrategy()
        {
            if ( this.XAxis != null && this.XAxis.IsPolarAxis )
            {
                return ( IAnnotationPlacementStrategy ) new CustomAnnotation.PolarAnnotationPlacementStrategy( this );
            }

            return ( IAnnotationPlacementStrategy ) new CustomAnnotation.CartesianAnnotationPlacementStrategy( this );
        }

        private static void PlaceCustomAnnotation( AnnotationCoordinates coordinates, CustomAnnotation annotation )
        {
            coordinates = annotation.GetAnchorAnnotationCoordinates( coordinates );
            double x1Coord = coordinates.X1Coord;
            double y1Coord = coordinates.Y1Coord;
            Canvas.SetLeft( ( UIElement ) annotation, x1Coord );
            Canvas.SetTop( ( UIElement ) annotation, y1Coord );
        }

        private static Point[ ] CalculateBasePoints( AnnotationCoordinates coordinates, CustomAnnotation annotation )
        {
            coordinates = annotation.GetAnchorAnnotationCoordinates( coordinates );
            if ( annotation.AnnotationRoot == null )
            {
                return new Point[ 0 ];
            }

            return new Point[ 1 ] { new Point( coordinates.X1Coord, coordinates.Y1Coord ) };
        }

        internal class CartesianAnnotationPlacementStrategy : AnnotationBase.CartesianAnnotationPlacementStrategyBase<CustomAnnotation>
        {
            public CartesianAnnotationPlacementStrategy( CustomAnnotation annotation )
              : base( annotation )
            {
            }

            public override void PlaceAnnotation( AnnotationCoordinates coordinates )
            {
                CustomAnnotation.PlaceCustomAnnotation( coordinates, this.Annotation );
            }

            public override Point[ ] GetBasePoints( AnnotationCoordinates coordinates )
            {
                return CustomAnnotation.CalculateBasePoints( coordinates, this.Annotation );
            }

            public override bool IsInBounds( AnnotationCoordinates coordinates, IAnnotationCanvas canvas )
            {
                return coordinates.X1Coord >= 0.0 && coordinates.X1Coord <= canvas.ActualWidth && coordinates.Y1Coord >= 0.0 && coordinates.Y1Coord <= canvas.ActualHeight;
            }

            protected override void InternalMoveAnntotationTo( AnnotationCoordinates coordinates, ref double horizontalOffset, ref double verticalOffset, IAnnotationCanvas canvas )
            {
                double num1 = coordinates.X1Coord + horizontalOffset;
                double num2 = coordinates.Y1Coord + verticalOffset;
                if ( !this.IsCoordinateValid( num1, canvas.ActualWidth ) || !this.IsCoordinateValid( num2, canvas.ActualHeight ) )
                {
                    double num3 = double.IsNaN(num1) ? 0.0 : num1;
                    double num4 = double.IsNaN(num2) ? 0.0 : num2;
                    if ( num3 < 0.0 )
                    {
                        horizontalOffset -= num3;
                    }

                    if ( num3 > canvas.ActualWidth )
                    {
                        horizontalOffset -= num3 - ( canvas.ActualWidth - 1.0 );
                    }

                    if ( num4 < 0.0 )
                    {
                        verticalOffset -= num4;
                    }

                    if ( num4 > canvas.ActualHeight )
                    {
                        verticalOffset -= num4 - ( canvas.ActualHeight - 1.0 );
                    }
                }
                coordinates.X1Coord += horizontalOffset;
                coordinates.Y1Coord += verticalOffset;
                IComparable[] comparableArray = this.FromCoordinates(coordinates.X1Coord, coordinates.Y1Coord);
                this.Annotation.SetCurrentValue( AnnotationBase.X1Property, ( object ) comparableArray[ 0 ] );
                this.Annotation.SetCurrentValue( AnnotationBase.Y1Property, ( object ) comparableArray[ 1 ] );
            }
        }

        internal class PolarAnnotationPlacementStrategy : AnnotationBase.PolarAnnotationPlacementStrategyBase<CustomAnnotation>
        {
            public PolarAnnotationPlacementStrategy( CustomAnnotation annotation )
              : base( annotation )
            {
            }

            protected override bool IsInBoundsInternal( AnnotationCoordinates coordinates, Size canvasSize )
            {
                return coordinates.X1Coord >= 0.0 && coordinates.X1Coord <= canvasSize.Width && coordinates.Y1Coord >= 0.0 && coordinates.Y1Coord <= canvasSize.Height;
            }

            public override void PlaceAnnotation( AnnotationCoordinates coordinates )
            {
                CustomAnnotation.PlaceCustomAnnotation( this.GetCartesianAnnotationCoordinates( coordinates ), this.Annotation );
            }

            public override Point[ ] GetBasePoints( AnnotationCoordinates coordinates )
            {
                return CustomAnnotation.CalculateBasePoints( this.GetCartesianAnnotationCoordinates( coordinates ), this.Annotation );
            }

            protected override void InternalMoveAnntotationTo( AnnotationCoordinates coordinates, Point x1y1Offset, Point x2y2Offset, Size canvasSize )
            {
                double x = x1y1Offset.X;
                double y = x1y1Offset.Y;
                double num1 = coordinates.X1Coord + x;
                double num2 = coordinates.Y1Coord + y;
                if ( !this.IsCoordinateValid( num1, canvasSize.Width ) || !this.IsCoordinateValid( num2, canvasSize.Height ) )
                {
                    double num3 = double.IsNaN(num1) ? 0.0 : num1;
                    double num4 = double.IsNaN(num2) ? 0.0 : num2;
                    if ( num3 < 0.0 )
                    {
                        x -= num3;
                    }

                    if ( num3 > canvasSize.Width )
                    {
                        x -= num3 - ( canvasSize.Width - 1.0 );
                    }

                    if ( num4 < 0.0 )
                    {
                        y -= num4;
                    }

                    if ( num4 > canvasSize.Height )
                    {
                        y -= num4 - ( canvasSize.Height - 1.0 );
                    }
                }
                coordinates.X1Coord += x;
                coordinates.Y1Coord += y;
                IComparable[] comparableArray = this.FromCoordinates(coordinates.X1Coord, coordinates.Y1Coord);
                this.Annotation.SetCurrentValue( AnnotationBase.X1Property, ( object ) comparableArray[ 0 ] );
                this.Annotation.SetCurrentValue( AnnotationBase.Y1Property, ( object ) comparableArray[ 1 ] );
            }
        }
    }
}
