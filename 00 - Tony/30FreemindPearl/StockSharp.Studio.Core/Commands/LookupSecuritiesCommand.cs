// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.LookupSecuritiesCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.BusinessEntities;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class LookupSecuritiesCommand : BaseStudioCommand
    {
        public LookupSecuritiesCommand( Security criteria )
        {
            Security security = criteria;
            if ( security == null )
                throw new ArgumentNullException( nameof( criteria ) );
            Criteria = security;
        }

        public Security Criteria { get; }
    }
}
