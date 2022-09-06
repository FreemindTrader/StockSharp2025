// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.OrderCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class OrderCommand : EntityCommand<Order>
    {
        public OrderCommand( Subscription subscription, Order order )
          : base( subscription, order )
        {
            this.Time = order.LastChangeTime;
            this.Balance = order.Balance;
            this.State = order.State;
            this.Price = order.Price;
        }

        public DateTimeOffset Time { get; }

        public Decimal Price { get; }

        public Decimal Balance { get; }

        public OrderStates State { get; }
    }
}
