// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.ReRegisterOrderCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.BusinessEntities;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class ReRegisterOrderCommand : BaseStudioCommand
    {
        public ReRegisterOrderCommand( Order oldOrder, Order newOrder )
        {
            Order order1 = oldOrder;
            if ( order1 == null )
                throw new ArgumentNullException( nameof( oldOrder ) );
            this.OldOrder = order1;
            Order order2 = newOrder;
            if ( order2 == null )
                throw new ArgumentNullException( nameof( newOrder ) );
            this.NewOrder = order2;
        }

        public Order OldOrder { get; }

        public Order NewOrder { get; }
    }
}
