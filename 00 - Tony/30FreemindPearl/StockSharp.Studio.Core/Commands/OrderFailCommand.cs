// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.OrderFailCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Messages;

namespace StockSharp.Studio.Core.Commands
{
    public class OrderFailCommand : EntityCommand<OrderFail>
    {
        public OrderFailCommand( Subscription subscription, OrderFail fail, OrderFailTypes type )
          : base( subscription, fail )
        {
            this.State = fail.Order.State;
            this.Type = type;
        }

        public OrderStates State { get; }

        public OrderFailTypes Type { get; }
    }
}
