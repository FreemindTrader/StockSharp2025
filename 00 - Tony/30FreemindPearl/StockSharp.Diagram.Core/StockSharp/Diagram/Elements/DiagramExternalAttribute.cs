// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.DiagramExternalAttribute
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using System;

namespace StockSharp.Diagram.Elements
{
  /// <summary>
  /// Attribute, applied to methods or parameters, to create input socket.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Event)]
  public class DiagramExternalAttribute : Attribute
  {
  }
}
