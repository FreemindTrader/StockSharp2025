// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.OrderRegisterDiagramElement
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Order registering element.</summary>
  [Doc("topics/Designer_Position_opening.html")]
  [CategoryLoc("Str3599")]
  [DescriptionLoc("Str3124", false)]
  [DisplayNameLoc("Str3123")]
  public class OrderRegisterDiagramElement : OrderBaseDiagramElement
  {
    
    private readonly Guid _typeId = nameof(-1260194302).To<Guid>();
    
    private readonly string _iconName = nameof(-1260194055);
    
    private readonly DiagramElementParam<OrderConditionSettings> \u0023\u003DziBjjeCZGAfV\u0024;
    
    private readonly DiagramSocket \u0023\u003DzTal4U24QVng9;
    
    private readonly DiagramSocket \u0023\u003DzhjU5VWjQ3F7xi\u00246XxQ\u003D\u003D;
    
    private readonly DiagramSocket \u0023\u003DzROizxQxrHZ2G_FvBE_Raa8E\u003D;
    
    private readonly DiagramSocket \u0023\u003Dz06d0Ylny\u0024ZEk;
    
    private readonly DiagramSocket \u0023\u003DzQjJfFIdH_T1\u0024;
    
    private DiagramSocket \u0023\u003Dzercw6GekSf8Eu7IDpA\u003D\u003D;
    
    private readonly DiagramElementParam<Sides?> \u0023\u003DzX5moHbk\u003D;
    
    private readonly DiagramElementParam<bool> \u0023\u003DzANFdMA0JzhYFdBd6Bw\u003D\u003D;
    
    private readonly DiagramElementParam<bool> \u0023\u003Dz9994xh7a1Rh8VMJETA\u003D\u003D;
    
    private readonly DiagramElementParam<string> \u0023\u003Dz3xzK\u0024Hm4P3WW;
    
    private readonly DiagramElementParam<string> \u0023\u003Dz4lbSnSqLA5\u0024L;
    
    private readonly DiagramElementParam<bool> \u0023\u003DzAL\u0024F6aY_XVsWOIaXrIjIe0I\u003D;
    
    private readonly DiagramElementParam<bool?> \u0023\u003DzDXqhrqfD1Tgn;
    
    private readonly DiagramElementParam<bool?> \u0023\u003DzwqUvUK7KRG4\u0024;
    
    private readonly DiagramElementParam<bool?> \u0023\u003Dz1EscS2UTQQwP2Jp8g2RbCKw\u003D;
    
    private readonly DiagramElementParam<StockSharp.Messages.TimeInForce?> \u0023\u003DzZXORnjVLytSA;
    
    private readonly DiagramElementParam<DateTimeOffset?> \u0023\u003DzoTtF4YQZCiUjzSYQjQ\u003D\u003D;
    
    private readonly DiagramElementParam<Decimal?> \u0023\u003DzQ8e00SAl0YqAPUplQA\u003D\u003D;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.Elements.OrderRegisterDiagramElement" />.
    /// </summary>
    public OrderRegisterDiagramElement()
    {
      this.\u0023\u003DzTal4U24QVng9 = this.AddInput(StaticSocketIds.Security, LocalizedStrings.Security, DiagramSocketType.Security, (Action<DiagramSocketValue>) null, 1, 0, false, new bool?());
      this.\u0023\u003DzsL\u0024xsXGKagUvF8oiOA\u003D\u003D(true);
      this.\u0023\u003DzhjU5VWjQ3F7xi\u00246XxQ\u003D\u003D = this.AddInput(StaticSocketIds.Volume, LocalizedStrings.Volume, DiagramSocketType.Unit, (Action<DiagramSocketValue>) null, 1, 3, false, new bool?());
      this.\u0023\u003DzROizxQxrHZ2G_FvBE_Raa8E\u003D = this.AddInput(StaticSocketIds.Portfolio, LocalizedStrings.Portfolio, DiagramSocketType.Portfolio, (Action<DiagramSocketValue>) null, 1, 4, false, new bool?());
      this.\u0023\u003Dz06d0Ylny\u0024ZEk = this.AddOutput(StaticSocketIds.Order, LocalizedStrings.Str504, DiagramSocketType.Order, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003DzQjJfFIdH_T1\u0024 = this.AddOutput(StaticSocketIds.OrderFail, LocalizedStrings.XamlStr182, DiagramSocketType.OrderFail, int.MaxValue, int.MaxValue, new bool?());
      this.\u0023\u003DzX5moHbk\u003D = this.AddParam<Sides?>(nameof(-1260194109), new Sides?()).SetDisplay<DiagramElementParam<Sides?>>(LocalizedStrings.Str504, LocalizedStrings.Str128, LocalizedStrings.Str3126, 30).SetOnValueChangedHandler<Sides?>(new Action<Sides?>(this.\u0023\u003Dz2YBcTJQkK0EK1kuuiiulWvM\u003D));
      this.\u0023\u003DzANFdMA0JzhYFdBd6Bw\u003D\u003D = this.AddParam<bool>(nameof(-1260194093), false).SetDisplay<DiagramElementParam<bool>>(LocalizedStrings.Str504, LocalizedStrings.Str241, LocalizedStrings.Str3127, 50).SetOnValueChangedHandler<bool>(new Action<bool>(this.\u0023\u003DzMHcMd\u0024Kw9J0WNeWSMQwFkbo\u003D)).SetIsParam<DiagramElementParam<bool>>();
      this.\u0023\u003Dz9994xh7a1Rh8VMJETA\u003D\u003D = this.AddParam<bool>(nameof(-1260194142), false).SetDisplay<DiagramElementParam<bool>>(LocalizedStrings.Str504, LocalizedStrings.ZeroPrice, LocalizedStrings.ZeroAsMarket, 50).SetIsParam<DiagramElementParam<bool>>();
      this.\u0023\u003Dz3xzK\u0024Hm4P3WW = this.AddParam<string>(nameof(-1260194127), (string) null).SetDisplay<DiagramElementParam<string>>(LocalizedStrings.Str504, LocalizedStrings.ClientCode, LocalizedStrings.ClientCodeDesc, 60).SetIsParam<DiagramElementParam<string>>();
      this.\u0023\u003Dz4lbSnSqLA5\u0024L = this.AddParam<string>(nameof(-1260194146), (string) null).SetDisplay<DiagramElementParam<string>>(LocalizedStrings.Str504, LocalizedStrings.Broker, LocalizedStrings.Str2619, 70).SetIsParam<DiagramElementParam<string>>();
      this.\u0023\u003DziBjjeCZGAfV\u0024 = this.AddParam<OrderConditionSettings>(nameof(-1260195473), (OrderConditionSettings) null).SetDisplay<DiagramElementParam<OrderConditionSettings>>(LocalizedStrings.Str504, LocalizedStrings.Str156, LocalizedStrings.Str156, 80);
      this.\u0023\u003DzAL\u0024F6aY_XVsWOIaXrIjIe0I\u003D = this.AddParam<bool>(nameof(-1260195467), true).SetDisplay<DiagramElementParam<bool>>(LocalizedStrings.Str504, LocalizedStrings.ShrinkPrice, LocalizedStrings.ShrinkPrice, 90);
      this.\u0023\u003DzwqUvUK7KRG4\u0024 = this.AddParam<bool?>(nameof(-1260195517), new bool?()).SetDisplay<DiagramElementParam<bool?>>(LocalizedStrings.Str504, LocalizedStrings.Margin, LocalizedStrings.IsMargin, 100);
      this.\u0023\u003Dz1EscS2UTQQwP2Jp8g2RbCKw\u003D = this.AddParam<bool?>(nameof(-1260195502), new bool?()).SetDisplay<DiagramElementParam<bool?>>(LocalizedStrings.Str504, LocalizedStrings.MarketMaker, LocalizedStrings.MarketMakerOrder, 110);
      this.\u0023\u003DzDXqhrqfD1Tgn = this.AddParam<bool?>(nameof(-1260195522), new bool?()).SetDisplay<DiagramElementParam<bool?>>(LocalizedStrings.Str504, LocalizedStrings.Manual, LocalizedStrings.IsOrderManual, 120);
      this.\u0023\u003DzZXORnjVLytSA = this.AddParam<StockSharp.Messages.TimeInForce?>(nameof(-1260195535), new StockSharp.Messages.TimeInForce?()).SetDisplay<DiagramElementParam<StockSharp.Messages.TimeInForce?>>(LocalizedStrings.Str504, LocalizedStrings.TimeInForce, LocalizedStrings.Str232, 130);
      this.\u0023\u003DzoTtF4YQZCiUjzSYQjQ\u003D\u003D = this.AddParam<DateTimeOffset?>(nameof(-1260195553), new DateTimeOffset?()).SetDisplay<DiagramElementParam<DateTimeOffset?>>(LocalizedStrings.Str504, LocalizedStrings.Str141, LocalizedStrings.Str142, 140);
      this.\u0023\u003DzQ8e00SAl0YqAPUplQA\u003D\u003D = this.AddParam<Decimal?>(nameof(-1260195553), new Decimal?()).SetDisplay<DiagramElementParam<Decimal?>>(LocalizedStrings.Str504, LocalizedStrings.Str163, LocalizedStrings.Str164, 150);
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

    /// <summary>Direction.</summary>
    public Sides? Direction
    {
      get
      {
        return this.\u0023\u003DzX5moHbk\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzX5moHbk\u003D.Value = value;
      }
    }

    /// <summary>Market order type.</summary>
    public bool IsMarket
    {
      get
      {
        return this.\u0023\u003DzANFdMA0JzhYFdBd6Bw\u003D\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzANFdMA0JzhYFdBd6Bw\u003D\u003D.Value = value;
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

    /// <summary>Client code assigned by the broker.</summary>
    public string ClientCode
    {
      get
      {
        return this.\u0023\u003Dz3xzK\u0024Hm4P3WW.Value;
      }
      set
      {
        this.\u0023\u003Dz3xzK\u0024Hm4P3WW.Value = value;
      }
    }

    /// <summary>Broker firm code.</summary>
    public string BrokerCode
    {
      get
      {
        return this.\u0023\u003Dz4lbSnSqLA5\u0024L.Value;
      }
      set
      {
        this.\u0023\u003Dz4lbSnSqLA5\u0024L.Value = value;
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

    /// <summary>Is order manual.</summary>
    public bool? IsManual
    {
      get
      {
        return this.\u0023\u003DzDXqhrqfD1Tgn.Value;
      }
      set
      {
        this.\u0023\u003DzDXqhrqfD1Tgn.Value = value;
      }
    }

    /// <summary>Is margin enabled.</summary>
    public bool? IsMargin
    {
      get
      {
        return this.\u0023\u003DzwqUvUK7KRG4\u0024.Value;
      }
      set
      {
        this.\u0023\u003DzwqUvUK7KRG4\u0024.Value = value;
      }
    }

    /// <summary>Is the order of market-maker.</summary>
    public bool? IsMarketMaker
    {
      get
      {
        return this.\u0023\u003Dz1EscS2UTQQwP2Jp8g2RbCKw\u003D.Value;
      }
      set
      {
        this.\u0023\u003Dz1EscS2UTQQwP2Jp8g2RbCKw\u003D.Value = value;
      }
    }

    /// <summary>Limit order time in force.</summary>
    public StockSharp.Messages.TimeInForce? TimeInForce
    {
      get
      {
        return this.\u0023\u003DzZXORnjVLytSA.Value;
      }
      set
      {
        this.\u0023\u003DzZXORnjVLytSA.Value = value;
      }
    }

    /// <summary>
    /// Order expiry time. The default is <see langword="null" />, which mean (GTC).
    /// </summary>
    public DateTimeOffset? ExpiryDate
    {
      get
      {
        return this.\u0023\u003DzoTtF4YQZCiUjzSYQjQ\u003D\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzoTtF4YQZCiUjzSYQjQ\u003D\u003D.Value = value;
      }
    }

    /// <summary>Slippage in trade price.</summary>
    public Decimal? Slippage
    {
      get
      {
        return this.\u0023\u003DzQ8e00SAl0YqAPUplQA\u003D\u003D.Value;
      }
      set
      {
        this.\u0023\u003DzQ8e00SAl0YqAPUplQA\u003D\u003D.Value = value;
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
      Security security = this.Strategy.Security;
      DiagramSocketValue diagramSocketValue1;
      if (values.TryGetValue(this.\u0023\u003DzTal4U24QVng9, out diagramSocketValue1))
        security = diagramSocketValue1.GetValue<Security>();
      if (security == null)
        throw new InvalidOperationException(LocalizedStrings.Str3128);
      Portfolio portfolio = this.Strategy.Portfolio;
      DiagramSocketValue diagramSocketValue2;
      if (values.TryGetValue(this.\u0023\u003DzROizxQxrHZ2G_FvBE_Raa8E\u003D, out diagramSocketValue2))
        portfolio = diagramSocketValue2.GetValue<Portfolio>();
      if (portfolio == null)
        throw new InvalidOperationException(LocalizedStrings.Str3253);
      Sides? direction = this.Direction;
      if (!direction.HasValue)
        throw new InvalidOperationException(LocalizedStrings.OrderSideNotSpecified);
      DiagramSocketValue diagramSocketValue3 = (DiagramSocketValue) null;
      if (!this.IsMarket && !values.TryGetValue(this.\u0023\u003Dzercw6GekSf8Eu7IDpA\u003D\u003D, out diagramSocketValue3))
        throw new InvalidOperationException(LocalizedStrings.OrderPriceNotSpecified);
      Decimal? nullable = diagramSocketValue3?.GetValue<Decimal>();
      DiagramSocketValue diagramSocketValue4;
      if (!values.TryGetValue(this.\u0023\u003DzhjU5VWjQ3F7xi\u00246XxQ\u003D\u003D, out diagramSocketValue4))
        throw new InvalidOperationException(LocalizedStrings.OrderVolumeNotSpecified);
      Decimal num = diagramSocketValue4.GetValue<Decimal>();
      Order order = new Order() { Portfolio = portfolio, Security = security, Direction = direction.Value, Type = new OrderTypes?(this.IsMarket || this.ZeroAsMarket && nullable.GetValueOrDefault() == Decimal.Zero ? OrderTypes.Market : OrderTypes.Limit), Volume = num, ClientCode = this.ClientCode, BrokerCode = this.BrokerCode, IsManual = this.IsManual, IsMargin = this.IsMargin, IsMarketMaker = this.IsMarketMaker, TimeInForce = this.TimeInForce, ExpiryDate = this.ExpiryDate, Slippage = this.Slippage };
      if (this.\u0023\u003DziBjjeCZGAfV\u0024.Value?.AdapterType != (Type) null)
      {
        IMessageAdapter adapter = ServicesRegistry.AdapterProvider.PossibleAdapters.FirstOrDefault<IMessageAdapter>(new Func<IMessageAdapter, bool>(this.\u0023\u003DzSanyAoA8VS4PViRU\u0024A\u003D\u003D));
        if (adapter == null)
          throw new InvalidOperationException(string.Format(nameof(-1260195348), (object) this.\u0023\u003DziBjjeCZGAfV\u0024.Value.AdapterType));
        order.Type = new OrderTypes?(OrderTypes.Conditional);
        order.Condition = adapter.CreateOrderCondition();
        foreach (KeyValuePair<string, object> parameter in (IEnumerable<KeyValuePair<string, object>>) this.\u0023\u003DziBjjeCZGAfV\u0024.Value.Parameters)
          order.Condition.Parameters[parameter.Key] = parameter.Value;
      }
      OrderTypes? type = order.Type;
      OrderTypes orderTypes = OrderTypes.Market;
      if (!(type.GetValueOrDefault() == orderTypes & type.HasValue))
      {
        if (!nullable.HasValue)
          nullable = new Decimal?(new Decimal());
        if (this.ShrinkPrice)
          nullable = new Decimal?(security.ShrinkPrice(nullable.Value, ShrinkRules.Auto));
        order.Price = nullable.Value;
      }
      order.WhenRegistered((ITransactionProvider) this.Strategy).Do(new Action<Order>(this.\u0023\u003Dzp6upjqpY\u0024WYBrOISBw\u003D\u003D)).Apply<Order, Order>((IMarketRuleContainer) this.Strategy);
      order.WhenRegisterFailed((ITransactionProvider) this.Strategy).Do(new Action<OrderFail>(this.\u0023\u003DzBrGnNLQ3\u00240P498fgMw\u003D\u003D)).Apply<Order, OrderFail>((IMarketRuleContainer) this.Strategy);
      this.Strategy.RegisterOrder(order);
    }

    /// <inheritdoc />
    public override void Load(SettingsStorage storage)
    {
      base.Load(storage);
      this.\u0023\u003DzsL\u0024xsXGKagUvF8oiOA\u003D\u003D(false);
    }

    private void \u0023\u003DzsL\u0024xsXGKagUvF8oiOA\u003D\u003D(bool _param1)
    {
      if (this.\u0023\u003Dzercw6GekSf8Eu7IDpA\u003D\u003D != null)
      {
        this.RemoveSocket(this.\u0023\u003Dzercw6GekSf8Eu7IDpA\u003D\u003D);
        this.\u0023\u003Dzercw6GekSf8Eu7IDpA\u003D\u003D = (DiagramSocket) null;
      }
      if (!_param1 && this.\u0023\u003DzANFdMA0JzhYFdBd6Bw\u003D\u003D.Value)
        return;
      this.\u0023\u003Dzercw6GekSf8Eu7IDpA\u003D\u003D = this.AddInput(DiagramElement.GenerateSocketId(nameof(-1260195343)), LocalizedStrings.Price, DiagramSocketType.Unit, (Action<DiagramSocketValue>) null, 1, 1, true, new bool?());
    }

    private void \u0023\u003Dz2YBcTJQkK0EK1kuuiiulWvM\u003D(Sides? _param1)
    {
      this.SetElementName(_param1.HasValue ? _param1.GetValueOrDefault().GetDisplayName() : (string) null);
    }

    private void \u0023\u003DzMHcMd\u0024Kw9J0WNeWSMQwFkbo\u003D(bool _param1)
    {
      this.\u0023\u003DzsL\u0024xsXGKagUvF8oiOA\u003D\u003D(false);
    }

    private bool \u0023\u003DzSanyAoA8VS4PViRU\u0024A\u003D\u003D(IMessageAdapter _param1)
    {
      return ((object) _param1).GetType() == this.\u0023\u003DziBjjeCZGAfV\u0024.Value.AdapterType;
    }

    private void \u0023\u003Dzp6upjqpY\u0024WYBrOISBw\u003D\u003D(Order _param1)
    {
      this.RaiseProcessOutput(this.\u0023\u003Dz06d0Ylny\u0024ZEk, _param1.LastChangeTime, (object) _param1, (DiagramSocketValue) null, (Subscription) null);
      this.Strategy.Flush();
    }

    private void \u0023\u003DzBrGnNLQ3\u00240P498fgMw\u003D\u003D(OrderFail _param1)
    {
      this.RaiseProcessOutput(this.\u0023\u003DzQjJfFIdH_T1\u0024, _param1.ServerTime, (object) _param1, (DiagramSocketValue) null, (Subscription) null);
      this.Strategy.Flush();
    }
  }
}
