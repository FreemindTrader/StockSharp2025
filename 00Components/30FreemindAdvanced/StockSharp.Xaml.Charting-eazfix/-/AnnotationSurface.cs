using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using StockSharp.Charting.Visuals;

#nullable disable
namespace StockSharp.Charting;

public class AnnotationSurface : Canvas, StockSharp.Charting.Visuals.IAnnotationCanvas, IHitTestable
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
    UIElementCollection StockSharp.Charting.Visuals.IAnnotationCanvas.Children
    {
        get
        {
            return this.Children;
        }
    }
}