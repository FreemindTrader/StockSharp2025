// Decompiled with JetBrains decompiler
// Type: Ecng.Drawing.Thickness
// Assembly: Ecng.Drawing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A7C4F4C-CC98-4F26-9962-1C4A5F640FF6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\Ecng.Drawing.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\Ecng.Drawing.xml

#nullable disable
namespace Ecng.Drawing;

/// <summary>Thickness.</summary>
public struct Thickness
{
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Ecng.Drawing.Thickness" />.
    /// </summary>
    /// <param name="left"><see cref="P:Ecng.Drawing.Thickness.Left" />.</param>
    /// <param name="top"><see cref="P:Ecng.Drawing.Thickness.Top" />.</param>
    /// <param name="right"><see cref="P:Ecng.Drawing.Thickness.Right" />.</param>
    /// <param name="bottom"><see cref="P:Ecng.Drawing.Thickness.Bottom" />.</param>
    public Thickness( double left, double top, double right, double bottom )
      : this()
    {
        this.Left = left;
        this.Top = top;
        this.Right = right;
        this.Bottom = bottom;
    }

    /// <summary>
    /// Gets or sets the width, in pixels, of the lower side of the bounding rectangle.
    /// </summary>
    public double Bottom
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets the width, in pixels, of the left side of the bounding rectangle.
    /// </summary>
    public double Left
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets the width, in pixels, of the right side of the bounding rectangle.
    /// </summary>
    public double Right
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets the width, in pixels, of the upper side of the bounding rectangle.
    /// </summary>
    public double Top
    {
        get; set;
    }
}
