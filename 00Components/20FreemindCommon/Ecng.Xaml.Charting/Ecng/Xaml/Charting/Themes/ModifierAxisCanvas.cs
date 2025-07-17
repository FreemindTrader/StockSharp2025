using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using StockSharp.Xaml.Charting.Visuals;
using StockSharp.Xaml.Charting.Visuals.Annotations;
using StockSharp.Xaml.Charting.Visuals.Axes;

namespace StockSharp.Xaml.Charting.Themes
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
