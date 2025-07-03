using System.Windows;

namespace SciChart.Core.Framework;

//
// Summary:
//     Defines the base interface for a type which can be hit-tested
public interface IHitTestable
{
    //
    // Summary:
    //     Gets the width of the SciChart.Core.Framework.IHitTestable
    double ActualWidth
    {
        get;
    }

    //
    // Summary:
    //     Gets the height of the SciChart.Core.Framework.IHitTestable
    double ActualHeight
    {
        get;
    }

    //
    // Summary:
    //     Translates the point relative to the other SciChart.Core.Framework.IHitTestable
    //     element
    //
    // Parameters:
    //   point:
    //     The input point relative to this SciChart.Core.Framework.IHitTestable
    //
    //   relativeTo:
    //     The other SciChart.Core.Framework.IHitTestable to use when transforming the point
    //
    //
    // Returns:
    //     The transformed Point
    Point TranslatePoint( Point point, IHitTestable relativeTo );

    //
    // Summary:
    //     Returns true if the Point is within the bounds of the current SciChart.Core.Framework.IHitTestable
    //     element
    //
    // Parameters:
    //   point:
    //     The point to test
    //
    // Returns:
    //     true if the Point is within the bounds
    bool IsPointWithinBounds( Point point );

    //
    // Summary:
    //     Gets the bounds of the current SciChart.Core.Framework.IHitTestable element relative
    //     to another SciChart.Core.Framework.IHitTestable element
    //
    // Parameters:
    //   relativeTo:
    Rect GetBoundsRelativeTo( IHitTestable relativeTo );
}