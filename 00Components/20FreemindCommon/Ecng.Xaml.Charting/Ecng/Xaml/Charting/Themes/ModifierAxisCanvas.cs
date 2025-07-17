using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Ecng.Xaml.Charting.Visuals;
using Ecng.Xaml.Charting.Visuals.Annotations;
using Ecng.Xaml.Charting.Visuals.Axes;

namespace Ecng.Xaml.Charting.Themes
{
    public class ModifierAxisCanvas : AxisCanvas, IAnnotationCanvas, IHitTestable
    {
        internal AxisBase ParentAxis
        {
            get; set;
        }

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
        UIElementCollection IAnnotationCanvas.Children
        {
            get
            {
                return this.Children;
            }

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
    }
}
