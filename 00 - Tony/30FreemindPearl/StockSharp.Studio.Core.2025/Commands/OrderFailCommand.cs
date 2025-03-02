// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.OrderFailCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using StockSharp.BusinessEntities;
using StockSharp.Messages;

namespace StockSharp.Studio.Core.Commands
{
    public class OrderFailCommand : EntityCommand<OrderFail>
    {
        public OrderFailCommand( Subscription subscription, OrderFail fail, OrderFailTypes type ) : base( subscription, fail )
        {
            this.State = fail.Order.State;
            this.Type = type;            
        }

        public OrderStates State { get; }

        public OrderFailTypes Type { get; }
    }
}
