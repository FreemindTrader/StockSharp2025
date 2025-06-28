// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.OrderFailCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 79FA112F-39E9-4D2F-8DA4-EB9B4E826551
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Core.dll

#nullable disable
namespace StockSharp.Studio.Core.Commands;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
public class OrderFailCommand : EntityCommand<OrderFail>
{
    public OrderFailCommand(Subscription subscription, OrderFail fail, OrderFailTypes type) : base(subscription, fail)
    {
        this.State = fail.Order.State;
        this.Type = type;
    }

    public OrderStates State { get; }

    public OrderFailTypes Type { get; }
}
