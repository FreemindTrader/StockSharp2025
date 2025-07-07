// Decompiled with JetBrains decompiler
// Type: Ecng.Drawing.SolidBrush
// Assembly: Ecng.Drawing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A7C4F4C-CC98-4F26-9962-1C4A5F640FF6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\Ecng.Drawing.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\Ecng.Drawing.xml

using System.Drawing;

#nullable disable
namespace Ecng.Drawing;

/// <summary>Represents a brush that paints a solid color.</summary>
/// <param name="color">The solid color to use for painting.</param>
/// <summary>Represents a brush that paints a solid color.</summary>
/// <param name="color">The solid color to use for painting.</param>
public class SolidBrush( Color color ) : Brush
{
    /// <summary>
    /// Gets the solid <see cref="P:Ecng.Drawing.SolidBrush.Color" /> used by the brush.
    /// </summary>
    public Color Color { get; } = color;
}
