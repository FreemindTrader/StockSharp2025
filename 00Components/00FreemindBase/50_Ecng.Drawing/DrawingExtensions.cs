// Decompiled with JetBrains decompiler
// Type: Ecng.Drawing.DrawingExtensions
// Assembly: Ecng.Drawing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A7C4F4C-CC98-4F26-9962-1C4A5F640FF6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\Ecng.Drawing.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\Ecng.Drawing.xml

using System.Drawing;

#nullable disable
namespace Ecng.Drawing;

/// <summary>
/// Provides extension methods for converting between integer, HTML color string representations and <see cref="T:System.Drawing.Color" />.
/// </summary>
public static class DrawingExtensions
{
    /// <summary>Creates a Color from the specified ARGB integer.</summary>
    /// <param name="argb">An integer representing the ARGB value.</param>
    /// <returns>A <see cref="T:System.Drawing.Color" /> corresponding to the ARGB value specified.</returns>
    public static Color ToColor( this int argb ) => Color.FromArgb( argb );

    /// <summary>
    /// Converts an HTML color representation to a <see cref="T:System.Drawing.Color" />.
    /// </summary>
    /// <param name="htmlColor">
    /// A string representing the HTML color. Accepts formats such as "#RRGGBB", "#RGB" or the special case "LightGrey".
    /// </param>
    /// <returns>A <see cref="T:System.Drawing.Color" /> corresponding to the HTML color provided.</returns>
    public static Color ToColor( this string htmlColor ) => ColorTranslator.FromHtml( htmlColor );

    /// <summary>
    /// Converts a <see cref="T:System.Drawing.Color" /> to its HTML representation.
    /// </summary>
    /// <param name="color">The <see cref="T:System.Drawing.Color" /> to convert.</param>
    /// <returns>A string representing the HTML color value. Returns an empty string if the color is empty.</returns>
    public static string ToHtml( this Color color ) => ColorTranslator.ToHtml( color );
}
