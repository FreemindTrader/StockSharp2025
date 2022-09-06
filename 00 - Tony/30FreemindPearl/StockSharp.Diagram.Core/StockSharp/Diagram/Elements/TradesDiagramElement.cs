// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.TradesDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Diagnostics;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Security new trades receiving element.</summary>
  [DescriptionLoc("Str1047", false)]
  [CategoryLoc("Sources")]
  [DisplayNameLoc("Str985")]
  public class TradesDiagramElement : SubscriptionDiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260192359).To<Guid>();
    
    private readonly string _iconName = nameof(-1260195722);

    /// <inheritdoc />
    public TradesDiagramElement()
    {
      this.AddOutput(StaticSocketIds.Output, LocalizedStrings.Str985, DiagramSocketType.Trade, int.MaxValue, int.MaxValue, new bool?());
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

    /// <inheritdoc />
    protected override Subscription OnCreateSubscription()
    {
      TradesDiagramElement.\u0023\u003DzCMrwQL7kk9lxifvRLQ\u003D\u003D cmrwQl7kk9lxifvRlq = new TradesDiagramElement.\u0023\u003DzCMrwQL7kk9lxifvRLQ\u003D\u003D();
      cmrwQl7kk9lxifvRlq._diagramElement = this;
      cmrwQl7kk9lxifvRlq.\u0023\u003Dzv10ueLU\u003D = DataType.Ticks.ToSubscription();
      cmrwQl7kk9lxifvRlq.\u0023\u003Dzv10ueLU\u003D.WhenTickTradeReceived((ISubscriptionProvider) this.Strategy).Do(new Action<Trade>(cmrwQl7kk9lxifvRlq.\u0023\u003Dzf3cAGPy66XOs\u0024\u00243fUw\u003D\u003D)).Apply<Subscription, Trade>((IMarketRuleContainer) this.Strategy);
      return cmrwQl7kk9lxifvRlq.\u0023\u003Dzv10ueLU\u003D;
    }

    private sealed class \u0023\u003DzCMrwQL7kk9lxifvRLQ\u003D\u003D
    {
      public TradesDiagramElement _diagramElement;
      public Subscription \u0023\u003Dzv10ueLU\u003D;

      internal void \u0023\u003Dzf3cAGPy66XOs\u0024\u00243fUw\u003D\u003D(Trade _param1)
      {
        this._diagramElement.RaiseProcessOutput(_param1.Time, (object) _param1, (DiagramSocketValue) null, this.\u0023\u003Dzv10ueLU\u003D);
        this._diagramElement.Strategy.Flush();
      }
    }
  }
}
