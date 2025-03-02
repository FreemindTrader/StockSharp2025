// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.CancelOrderCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using StockSharp.BusinessEntities;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class CancelOrderCommand : BaseStudioCommand
    {
        public CancelOrderCommand( Order order )
        {
            Order order1 = order;
            if ( order1 == null )
                throw new ArgumentNullException( nameof( order ) );
            this.Order = order1;           
        }

        public Order Order { get; }
    }
}
