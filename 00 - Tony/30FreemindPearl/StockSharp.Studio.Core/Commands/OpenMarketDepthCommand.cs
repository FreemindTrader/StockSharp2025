// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.OpenMarketDepthCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.BusinessEntities;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class OpenMarketDepthCommand : BaseStudioCommand
    {
        public Security Security { get; }

        public OpenMarketDepthCommand( Security security )
        {
            Security security1 = security;
            if ( security1 == null )
                throw new ArgumentNullException( nameof( security ) );
            this.Security = security1;
        }
    }
}
