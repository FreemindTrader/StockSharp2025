// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.OrderReplaceElement
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
using System.Collections.Generic;
using System.Diagnostics;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Order replacing element.</summary>
  [DescriptionLoc("OrderReplacing", true)]
  [CategoryLoc("Str3599")]
  [Doc("topics/Designer_Position_opening.html")]
  [DisplayNameLoc("OrderReplacing")]
  public class OrderReplaceElement : OrderBaseDiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260195387).To<Guid>();
    
    private readonly string _iconName = nameof(-1260195400);
    
    private readonly DiagramSocket \u0023\u003Dzggwgbe2iNBUa;
    
    private readonly DiagramSocket \u0023\u003Dz9ojoEnwdJk5zHrHMyQ\u003D\u003D;
    
    private readonly DiagramSocket \u0023\u003DzhjU5VWjQ3F7xi\u00246XxQ\u003D\u003D;
    
    private readonly DiagramSocket \u0023\u003DzwBn94cBRgh3Y;
    
    private readonly DiagramSocket \u0023\u003DzQjJfFIdH_T1\u0024;
    
    private readonly DiagramElementParam<bool> \u0023\u003DzAL\u0024F6aY_XVsWOIaXrIjIe0I\u003D;
    
    private readonly DiagramElementParam<bool> \u0023\u003Dz9994xh7a1Rh8VMJETA\u003D\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.OrderReplaceElement" />.
    /// </summary>
    public OrderReplaceElement()
    {
      this.\u0023\u003Dzggwgbe2iNBUa = this.AddInput(StaticSocketIds.Order, LocalizedStrings.Str504, DiagramSocketType.Order, (Action<DiagramSocketValue>) null, 1, int.MaxValue, false, new bool?());
      this.\u0023\u003Dz9ojoEnwdJk5zHrHMyQ\u003D\u003D = this.AddInput(StaticSocketIds.Price, LocalizedStrings.Price, DiagramSocketType.Unit, (Action<DiagramSocketValue>) null, 1, int.MaxValue, true, new bool?());
      this.\u0023\u003DzhjU5VWjQ3F7xi\u00246XxQ\u003D\u003D = this.AddInput(StaticSocketIds.Volume, LocalizedStrings.Volume, DiagramSocketType.Unit, (Action<DiagramSocketValue>) null, 1, int.MaxValue, true, new bool?());
      this.\u0023\u003DzwBn94cBRgh3Y = this.AddOutput(StaticSocketIds.Output, LocalizedStrings.Str504, DiagramSocketType.Order, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003DzQjJfFIdH_T1\u0024 = this.AddOutput(StaticSocketIds.OrderFail, LocalizedStrings.XamlStr182, DiagramSocketType.OrderFail, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003DzAL\u0024F6aY_XVsWOIaXrIjIe0I\u003D = this.AddParam<bool>(nameof(-1260195467), true).SetDisplay<DiagramElementParam<bool>>(LocalizedStrings.Str504, LocalizedStrings.ShrinkPrice, LocalizedStrings.ShrinkPrice, 90);
      this.\u0023\u003Dz9994xh7a1Rh8VMJETA\u003D\u003D = this.AddParam<bool>(nameof(-1260194142), false).SetDisplay<DiagramElementParam<bool>>(LocalizedStrings.Str504, LocalizedStrings.ZeroPrice, LocalizedStrings.ZeroAsMarket, 91);
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

    /// <summary>Shrink order price.</summary>
    public bool ShrinkPrice
    {
      get
      {
        return this.\u0023\u003DzAL\u0024F6aY_XVsWOIaXrIjIe0I\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzAL\u0024F6aY_XVsWOIaXrIjIe0I\u003D.Value = value;
      }
    }

    /// <summary>Zero price makes market order.</summary>
    public bool ZeroAsMarket
    {
      get
      {
        return this.\u0023\u003Dz9994xh7a1Rh8VMJETA\u003D\u003D.Value;
      }
      set
      {
        this.\u0023\u003Dz9994xh7a1Rh8VMJETA\u003D\u003D.Value = value;
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
      DiagramSocketValue diagramSocketValue1;
      if (!values.TryGetValue(this.\u0023\u003Dzggwgbe2iNBUa, out diagramSocketValue1))
        throw new InvalidOperationException(LocalizedStrings.OrderNotPassed);
      Order order1 = diagramSocketValue1.GetValue<Order>();
      DiagramSocketValue diagramSocketValue2;
      if (!values.TryGetValue(this.\u0023\u003Dz9ojoEnwdJk5zHrHMyQ\u003D\u003D, out diagramSocketValue2))
        throw new InvalidOperationException(LocalizedStrings.OrderPriceNotSpecified);
      Decimal price = diagramSocketValue2.GetValue<Decimal>();
      DiagramSocketValue diagramSocketValue3;
      if (!values.TryGetValue(this.\u0023\u003DzhjU5VWjQ3F7xi\u00246XxQ\u003D\u003D, out diagramSocketValue3))
        throw new InvalidOperationException(LocalizedStrings.OrderVolumeNotSpecified);
      Decimal num = diagramSocketValue3.GetValue<Decimal>();
      if (price != Decimal.Zero && this.ShrinkPrice)
        price = order1.Security.ShrinkPrice(price, ShrinkRules.Auto);
      Order order2 = order1.ReRegisterClone(new Decimal?(price), new Decimal?(num));
      if (price == Decimal.Zero && this.ZeroAsMarket)
        order2.Type = new OrderTypes?(OrderTypes.Market);
      order2.WhenRegistered((ITransactionProvider) this.Strategy).Or<Order, Order>(new MarketRule<Order, Order>[1]
      {
        order1.WhenMatched((ITransactionProvider) this.Strategy)
      }).Do(new Action<Order>(this.\u0023\u003Dz9VeIPkHNPMzuyOlW2A\u003D\u003D)).Apply<Order, Order>((IMarketRuleContainer) this.Strategy);
      order2.WhenRegisterFailed((ITransactionProvider) this.Strategy).Do(new Action<OrderFail>(this.\u0023\u003DzfTQPPtHwnRhZxZtYTw\u003D\u003D)).Apply<Order, OrderFail>((IMarketRuleContainer) this.Strategy);
      this.Strategy.ReRegisterOrder(order1, order2);
    }

    private void \u0023\u003Dz9VeIPkHNPMzuyOlW2A\u003D\u003D(Order _param1)
    {
      this.RaiseProcessOutput(this.\u0023\u003DzwBn94cBRgh3Y, _param1.LastChangeTime, (object) _param1, (DiagramSocketValue) null, (Subscription) null);
      this.Strategy.Flush();
    }

    private void \u0023\u003DzfTQPPtHwnRhZxZtYTw\u003D\u003D(OrderFail _param1)
    {
      this.RaiseProcessOutput(this.\u0023\u003DzQjJfFIdH_T1\u0024, _param1.ServerTime, (object) _param1, (DiagramSocketValue) null, (Subscription) null);
      this.Strategy.Flush();
    }
  }
}
