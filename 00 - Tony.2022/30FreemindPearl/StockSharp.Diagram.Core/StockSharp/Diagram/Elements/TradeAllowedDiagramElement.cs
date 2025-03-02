// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.TradeAllowedDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Algo;
using StockSharp.Algo.Strategies;
using StockSharp.Localization;
using System;
using System.Diagnostics;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Is trade allowed verification element.</summary>
  [DescriptionLoc("IsTradeAllowed", false)]
  [Doc("topics/Designer_TradeAllowedDiagramElement.html")]
  [CategoryLoc("Common")]
  [DisplayNameLoc("IsTradeAllowed")]
  public class TradeAllowedDiagramElement : DiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260192350).To<Guid>();
    
    private readonly string _iconName = nameof(-1260196131);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.TradeAllowedDiagramElement" />.
    /// </summary>
    public TradeAllowedDiagramElement()
    {
      this.AddInput(StaticSocketIds.Trigger, LocalizedStrings.Trigger, DiagramSocketType.Any, new Action<DiagramSocketValue>(this.\u0023\u003DzGjIGpXOf0Oqj), int.MaxValue, int.MaxValue, false, new bool?());
      this.AddOutput(StaticSocketIds.Output, LocalizedStrings.Output, DiagramSocketType.Bool, int.MaxValue, int.MaxValue, new bool?());
    }

    /// <inheritdoc />
    public override Guid TypeId
    {
      get
      {
        return this._typeId;
      }
    }

    /// <inheritdoc />
    public override string IconName
    {
      get
      {
        return this._iconName;
      }
    }

    private void \u0023\u003DzGjIGpXOf0Oqj(DiagramSocketValue _param1)
    {
      if (!this.Strategy.AllowTrading)
        this.RaiseProcessOutput(_param1.Time, (object) false, _param1, (Subscription) null);
      else if (!this.Strategy.GetIsEmulation() && _param1.Time < this.Strategy.StartedTime)
        this.RaiseProcessOutput(_param1.Time, (object) false, _param1, (Subscription) null);
      else
        this.RaiseProcessOutput(_param1.Time, (object) true, _param1, (Subscription) null);
    }
  }
}
