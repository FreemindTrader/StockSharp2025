using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Ecng.Xaml.Charting.Visuals.Annotations
{
    public class AnnotationSurface : Canvas, IAnnotationCanvas, IHitTestable
    {
        public Point TranslatePoint( Point point, IHitTestable relativeTo )
        {
            return this.TranslatePoint( point, relativeTo );
        }

        public bool IsPointWithinBounds( Point point )
        {
            return this.IsPointWithinBounds( point );
        }

        public Rect GetBoundsRelativeTo( IHitTestable relativeTo )
        {
            return this.GetBoundsRelativeTo( relativeTo );
        }

        [SpecialName]
        double IHitTestable.ActualWidth
        {
            get
            {
                return this.ActualWidth;
            }

        }

        [SpecialName]
        double IHitTestable.ActualHeight
        {
            get
            {
                return this.ActualHeight;
            }
        }

        [SpecialName]
        UIElementCollection IAnnotationCanvas.Children
        {
            get
            {
                return this.Children;
            }
        }
    }
}
