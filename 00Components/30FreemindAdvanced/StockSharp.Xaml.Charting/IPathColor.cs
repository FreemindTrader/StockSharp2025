using System;
using System.Windows.Media;

namespace SciChart.Drawing.Common;

//
// Summary:
//     A base interface for SciChart.Drawing.Common.IPen2D and SciChart.Drawing.Common.IBrush2D.
//     Used by the SciChart.Drawing.Common.IPathDrawingContext to draw fills and lines
public interface IPathColor : IDisposable, IEquatable<IPathColor>
{
    //
    // Summary:
    //     Gets the color of the pen. Supports transparency
    Color Color
    {
        get;
    }

    //
    // Summary:
    //     Used internally by the renderer, gets the integer color-code that represents
    //     the Pen color
    int ColorCode
    {
        get;
    }

    //
    // Summary:
    //     Gets a value indicating whether this pen is transparent.
    //
    // Value:
    //     true if this instance is transparent; otherwise, false.
    bool IsTransparent
    {
        get;
    }
}
