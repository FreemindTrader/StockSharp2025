using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
namespace fx.Xaml.Charting
{
    internal class CartesianRubberBandOverlayPlacementStrategy : IRubberBandOverlayPlacementStrategy
    {
        private readonly IChartModifier _modifier;

        private Shape _rectangle;

        public CartesianRubberBandOverlayPlacementStrategy( IChartModifier modifier )
        {
            _modifier = modifier;
        }

        public double CalculateDraggedDistance( Point start, Point end )
        {
            return PointUtil.Distance( start, end );
        }

        public Shape CreateShape( Brush rubberBandFill, Brush rubberBandStroke, DoubleCollection rubberBandStrokeDashArray )
        {
            _rectangle = new Rectangle()
            {
                Fill = rubberBandFill,
                Stroke = rubberBandStroke,
                StrokeDashArray = rubberBandStrokeDashArray
            };
            return _rectangle;
        }

        public void SetupShape( bool isXAxisOnly, Point start, Point end )
        {
            UpdateShape( isXAxisOnly, start, end );
        }

        public Point UpdateShape( bool isXAxisOnly, Point start, Point end )
        {
            if ( isXAxisOnly )
            {
                if ( !_modifier.XAxis.IsHorizontalAxis )
                {
                    start.X = 0;
                    end.X = _modifier.ModifierSurface.ActualWidth;
                }
                else
                {
                    start.Y = 0;
                    end.Y = _modifier.ModifierSurface.ActualHeight;
                }
            }
            end = ( new Rect( 0, 0, _modifier.ModifierSurface.ActualWidth, _modifier.ModifierSurface.ActualHeight ) ).ClipToBounds( end );
            Rect rect = new Rect(start, end);
            Canvas.SetLeft( _rectangle, rect.X );
            Canvas.SetTop( _rectangle, rect.Y );
            _rectangle.Width = rect.Width;
            _rectangle.Height = rect.Height;

            return end;
        }
    }
}
