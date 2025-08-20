// Decompiled with JetBrains decompiler
// Type: Ecng.Drawing.HorizontalAlignment
// Assembly: Ecng.Drawing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A7C4F4C-CC98-4F26-9962-1C4A5F640FF6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\Ecng.Drawing.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\Ecng.Drawing.xml

#nullable disable
namespace Ecng.Drawing;

/// <summary>
/// Indicates where an element should be displayed on the horizontal axis relative to the allocated layout slot of the parent element.
/// </summary>
public enum HorizontalAlignment
{
    /// <summary>
    /// An element aligned to the left of the layout slot for the parent element.
    /// </summary>
    Left,
    /// <summary>
    /// An element aligned to the center of the layout slot for the parent element.
    /// </summary>
    Center,
    /// <summary>
    /// An element aligned to the right of the layout slot for the parent element.
    /// </summary>
    Right,
    /// <summary>
    /// An element stretched to fill the entire layout slot of the parent element.
    /// </summary>
    Stretch,
}
