// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.OrderCancelElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Order cancelling element.</summary>
  [Doc("topics/Designer_Cancellations.html")]
  [DescriptionLoc("OrderCancelling", true)]
  [CategoryLoc("Str3599")]
  [DisplayNameLoc("OrderCancelling")]
  public class OrderCancelElement : OrderBaseDiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260193901).To<Guid>();
    
    private readonly string _iconName = nameof(-1260194234);
    
    private readonly DiagramSocket \u0023\u003Dzggwgbe2iNBUa;
    
    private readonly DiagramSocket \u0023\u003DzwBn94cBRgh3Y;
    
    private readonly DiagramSocket \u0023\u003DzQjJfFIdH_T1\u0024;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.OrderCancelElement" />.
    /// </summary>
    public OrderCancelElement()
    {
      this.\u0023\u003Dzggwgbe2iNBUa = this.AddInput(StaticSocketIds.Order, LocalizedStrings.Str504, DiagramSocketType.Order, (Action<DiagramSocketValue>) null, 1, int.MaxValue, false, new bool?());
      this.\u0023\u003DzwBn94cBRgh3Y = this.AddOutput(StaticSocketIds.Output, LocalizedStrings.Str504, DiagramSocketType.Order, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003DzQjJfFIdH_T1\u0024 = this.AddOutput(StaticSocketIds.OrderFail, LocalizedStrings.XamlStr182, DiagramSocketType.OrderFail, int.MaxValue, int.MaxValue, new bool?());
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
    protected override void OnProcess(
      DateTimeOffset time,
      IDictionary<DiagramSocket, DiagramSocketValue> values,
      DiagramSocketValue source)
    {
      if (!this.CanProcess(values))
        return;
      DiagramSocketValue diagramSocketValue;
      if (!values.TryGetValue(this.\u0023\u003Dzggwgbe2iNBUa, out diagramSocketValue))
        throw new InvalidOperationException(LocalizedStrings.OrderNotPassed);
      Order order = diagramSocketValue.GetValue<Order>();
      if (order == null)
        throw new InvalidOperationException(LocalizedStrings.OrderNotPassed);
      order.WhenCanceled((ITransactionProvider) this.Strategy).Do(new Action<Order>(this.\u0023\u003DzrmyI6xuhd4AknWwQTw\u003D\u003D)).Apply<Order, Order>((IMarketRuleContainer) this.Strategy);
      order.WhenCancelFailed((ITransactionProvider) this.Strategy).Do(new Action<OrderFail>(this.\u0023\u003DzsodJpUeFvu2PBPEgQA\u003D\u003D)).Apply<Order, OrderFail>((IMarketRuleContainer) this.Strategy);
      this.Strategy.CancelOrder(order);
    }

    private void \u0023\u003DzrmyI6xuhd4AknWwQTw\u003D\u003D(Order _param1)
    {
      this.RaiseProcessOutput(this.\u0023\u003DzwBn94cBRgh3Y, _param1.LastChangeTime, (object) _param1, (DiagramSocketValue) null, (Subscription) null);
      this.Strategy.Flush();
    }

    private void \u0023\u003DzsodJpUeFvu2PBPEgQA\u003D\u003D(OrderFail _param1)
    {
      this.RaiseProcessOutput(this.\u0023\u003DzQjJfFIdH_T1\u0024, _param1.ServerTime, (object) _param1, (DiagramSocketValue) null, (Subscription) null);
      this.Strategy.Flush();
    }
  }
}
