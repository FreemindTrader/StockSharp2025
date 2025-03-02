// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.OrderCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class OrderCommand : EntityCommand<Order>
    {
        public OrderCommand( Subscription subscription, Order order ) : base( subscription, order )
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
}
