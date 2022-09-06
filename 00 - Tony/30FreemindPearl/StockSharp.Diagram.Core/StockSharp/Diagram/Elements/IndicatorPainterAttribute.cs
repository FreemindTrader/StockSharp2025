// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.IndicatorPainterAttribute
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using System;
using System.Diagnostics;

namespace StockSharp.Diagram.Elements
{
  /// <summary>
  /// Attribute, applied to class, to create renderer type for indicator extended drawing.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class)]
  public class IndicatorPainterAttribute : Attribute
  {
    
    private readonly Type \u0023\u003DzXSYSKy_4RTSuHmf\u0024h5oISjE\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.IndicatorPainterAttribute" />.
    /// </summary>
    /// <param name="painter">The renderer type for indicator extended drawing.</param>
    public IndicatorPainterAttribute(Type painter)
    {
      Type type = painter;
      if ((object) type == null)
        throw new ArgumentNullException(nameof(-1260197645));
      this.\u0023\u003DzXSYSKy_4RTSuHmf\u0024h5oISjE\u003D = type;
    }

    /// <summary>The renderer type for indicator extended drawing.</summary>
    public Type Painter
    {
      get
      {
        return this.\u0023\u003DzXSYSKy_4RTSuHmf\u0024h5oISjE\u003D;
      }
    }
  }
}
