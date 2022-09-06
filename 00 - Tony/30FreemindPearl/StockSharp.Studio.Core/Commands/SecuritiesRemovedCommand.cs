// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.SecuritiesRemovedCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;

namespace StockSharp.Studio.Core.Commands
{
    public class SecuritiesRemovedCommand : BaseStudioCommand
    {
        public SecuritiesRemovedCommand( IEnumerable<Security> securities )
        {
            IEnumerable<Security> securities1 = securities;
            if ( securities1 == null )
                throw new ArgumentNullException( nameof( securities ) );
            this.Securities = securities1;
        }

        public IEnumerable<Security> Securities { get; }
    }
}
