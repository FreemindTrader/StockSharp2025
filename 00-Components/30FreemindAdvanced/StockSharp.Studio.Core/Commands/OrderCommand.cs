// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.OrderCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 79FA112F-39E9-4D2F-8DA4-EB9B4E826551
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Core.dll

#nullable disable
namespace StockSharp.Studio.Core.Commands;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;

public class OrderCommand : EntityCommand<Order>
{
    public OrderCommand(Subscription subscription, Order order) : base(subscription, order)
    {
        this.Time = order.ServerTime;
        this.Price = order.Price;
        this.Balance = order.Balance;
        this.State = order.State;
    }

    public DateTimeOffset Time { get; }

    public Decimal Price { get; }

    public Decimal Balance { get; }

    public OrderStates State { get; }
}
