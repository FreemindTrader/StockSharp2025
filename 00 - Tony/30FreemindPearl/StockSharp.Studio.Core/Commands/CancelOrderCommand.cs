// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.CancelOrderCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.BusinessEntities;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class CancelOrderCommand : BaseStudioCommand
    {
        public Order Order { get; }

        public CancelOrderCommand( Order order )
        {
            Order order1 = order;
            if ( order1 == null )
                throw new ArgumentNullException( nameof( order ) );
            this.Order = order1;
        }
    }
}
