// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.RegisterOrderCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.BusinessEntities;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class RegisterOrderCommand : BaseStudioCommand
    {
        public RegisterOrderCommand( Order order )
        {
            Order order1 = order;
            if ( order1 == null )
                throw new ArgumentNullException( nameof( order ) );
            Order = order1;
        }

        public Order Order { get; }
    }
}
