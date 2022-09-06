// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.OrderBaseDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Order base element.</summary>
  public abstract class OrderBaseDiagramElement : DiagramElement
  {
    
    private readonly DiagramSocket _diagramSocket;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.OrderBaseDiagramElement" />.
    /// </summary>
    protected OrderBaseDiagramElement()
    {
      this._diagramSocket = this.AddInput(StaticSocketIds.Trigger, LocalizedStrings.Trigger, DiagramSocketType.Any, (Action<DiagramSocketValue>) null, int.MaxValue, int.MaxValue, false, new bool?());
    }

    /// <summary>Can process order action.</summary>
    /// <param name="values">Values.</param>
    /// <returns>Check result.</returns>
    protected bool CanProcess(
      IDictionary<DiagramSocket, DiagramSocketValue> values)
    {
      DiagramSocketValue diagramSocketValue;
      if (!values.TryGetValue(this._diagramSocket, out diagramSocketValue))
        throw new InvalidOperationException();
      object obj = diagramSocketValue.Value;
      return (!(obj is bool) || (bool) obj) && this.ProcessingLevel <= 1;
    }
  }
}
