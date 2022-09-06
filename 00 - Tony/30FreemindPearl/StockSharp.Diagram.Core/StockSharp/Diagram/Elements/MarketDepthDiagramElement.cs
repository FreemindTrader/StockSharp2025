// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.MarketDepthDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Diagnostics;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Security market depth changes receiving element.</summary>
  [CategoryLoc("MarketDepths")]
  [DisplayNameLoc("MarketDepth")]
  [Doc("topics/Designer_Depth.html")]
  [DescriptionLoc("Str3117", false)]
  public class MarketDepthDiagramElement : SubscriptionDiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260196866).To<Guid>();
    
    private readonly string _iconName = nameof(-1260196907);

    /// <inheritdoc />
    public MarketDepthDiagramElement()
    {
      this.AddOutput(StaticSocketIds.Output, LocalizedStrings.MarketDepth, DiagramSocketType.MarketDepth, int.MaxValue, int.MaxValue, new bool?());
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
      Subscription subscription = DataType.MarketDepth.ToSubscription();
      subscription.WhenMarketDepthReceived((ISubscriptionProvider) this.Strategy).Do(new Action<MarketDepth>(this.\u0023\u003Dz02IxsKRs\u00247AI44ToGtn_tN0\u003D)).Apply<Subscription, MarketDepth>((IMarketRuleContainer) this.Strategy);
      return subscription;
    }

    private void \u0023\u003Dz02IxsKRs\u00247AI44ToGtn_tN0\u003D(MarketDepth _param1)
    {
      this.RaiseProcessOutput(_param1.LastChangeTime, (object) _param1, (DiagramSocketValue) null, (Subscription) null);
      this.Strategy.Flush();
    }
  }
}
