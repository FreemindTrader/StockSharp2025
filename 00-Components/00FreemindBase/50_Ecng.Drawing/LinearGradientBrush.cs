// Decompiled with JetBrains decompiler
// Type: Ecng.Drawing.LinearGradientBrush
// Assembly: Ecng.Drawing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A7C4F4C-CC98-4F26-9962-1C4A5F640FF6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\Ecng.Drawing.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\Ecng.Drawing.xml

using System;
using System.Drawing;
using Ecng.Common;

#nullable disable
namespace Ecng.Drawing;

/// <summary>
/// Represents a brush that paints a gradient between multiple colors.
/// </summary>
/// <param name="linearColors">An array of colors defining the gradient stops.</param>
/// <param name="rectangle">The rectangle that defines the bounds of the gradient.</param>
/// <summary>
/// Represents a brush that paints a gradient between multiple colors.
/// </summary>
/// <param name="linearColors">An array of colors defining the gradient stops.</param>
/// <param name="rectangle">The rectangle that defines the bounds of the gradient.</param>
public class LinearGradientBrush( Color[ ] linearColors, Rectangle rectangle ) : Brush
{
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ecng.Drawing.LinearGradientBrush" /> class using two points and two colors.
    /// </summary>
    /// <param name="stop0">The first point of the gradient.</param>
    /// <param name="stop1">The second point of the gradient.</param>
    /// <param name="color0">The color at the first point.</param>
    /// <param name="color1">The color at the second point.</param>
    public LinearGradientBrush( Point stop0, Point stop1, Color color0, Color color1 )
      : this( new Color[ 2 ] { color0, color1 }, new Rectangle( stop0, new Size( MathHelper.Abs( stop1.X - stop0.X ), MathHelper.Abs( stop1.Y - stop0.Y ) ) ) )
    {
    }

    /// <summary>Gets the array of colors defining the gradient stops.</summary>
    public Color[ ] LinearColors { get; } = linearColors ?? throw new ArgumentNullException( nameof( linearColors ) );

    /// <summary>
    /// Gets the rectangle that defines the bounds of the gradient.
    /// </summary>
    public Rectangle Rectangle { get; } = rectangle;
}
