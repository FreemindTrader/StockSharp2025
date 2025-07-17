using System.Windows;
using System.Windows.Input;
namespace Ecng.Xaml.Charting
{
    public class YAxisDragModifier : AxisDragModifierBase
    {
        public YAxisDragModifier()
        {
        }

        protected override IRange CalculateScaledRange( Point currentPoint, Point lastPoint, bool isSecondHalf, IAxis axis )
        {
            IAxisInteractivityHelper currentInteractivityHelper = axis.GetCurrentInteractivityHelper();
            double x = currentPoint.X - lastPoint.X;
            double y = lastPoint.Y - currentPoint.Y;
            double num = (axis.IsHorizontalAxis ? -x : y);
            IRange range = (isSecondHalf ? currentInteractivityHelper.ScrollInMaxDirection(axis.VisibleRange, num) : currentInteractivityHelper.ScrollInMinDirection(axis.VisibleRange, num));
            if ( axis.VisibleRangeLimit != null )
            {
                range.ClipTo( axis.VisibleRangeLimit, axis.VisibleRangeLimitMode );
            }
            return range;
        }

        protected override IAxis GetCurrentAxis()
        {
            return base.GetYAxis( base.AxisId );
        }

        protected override bool GetIsSecondHalf( Point point, Rect axisBounds, bool isHorizontalAxis )
        {
            bool isPolarAxis;
            IAxis xAxis = this.XAxis;
            if ( xAxis != null )
            {
                isPolarAxis = xAxis.IsPolarAxis;
            }
            else
            {
                isPolarAxis = false;
            }
            if ( !isPolarAxis )
            {
                return !base.GetIsSecondHalf( point, axisBounds, isHorizontalAxis );
            }
            axisBounds.Height = axisBounds.Height / 2;
            return !axisBounds.Contains( point );
        }

        protected override System.Windows.Input.Cursor GetUsedCursor( IAxis axis )
        {
            if ( axis.IsPolarAxis )
            {
                return Cursors.None;
            }
            return base.GetUsedCursor( axis );
        }

        public override void OnModifierMouseDown( ModifierMouseArgs e )
        {
            if ( e.IsMaster )
            {
                base.OnModifierMouseDown( e );
            }
        }

        public override void OnModifierMouseMove( ModifierMouseArgs e )
        {
            if ( e.IsMaster )
            {
                base.OnModifierMouseMove( e );
            }
        }

        public override void OnModifierMouseUp( ModifierMouseArgs e )
        {
            if ( e.IsMaster )
            {
                base.OnModifierMouseUp( e );
            }
        }

        protected override void PerformPan( Point currentPoint, Point lastPoint )
        {
            IAxis currentAxis = this.GetCurrentAxis();
            double x = currentPoint.X - lastPoint.X;
            double y = lastPoint.Y - currentPoint.Y;
            currentAxis.Scroll( ( currentAxis.IsHorizontalAxis ? -x : y ), ClipMode.None );
        }
    }
}
