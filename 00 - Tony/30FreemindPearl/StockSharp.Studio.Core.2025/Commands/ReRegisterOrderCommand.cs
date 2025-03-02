// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.ReRegisterOrderCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

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
