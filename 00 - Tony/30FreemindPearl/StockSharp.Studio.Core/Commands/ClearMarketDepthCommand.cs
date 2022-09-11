// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.ClearMarketDepthCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.BusinessEntities;

namespace StockSharp.Studio.Core.Commands
{
    public class ClearMarketDepthCommand : BaseStudioCommand
    {
        public Security Security { get; }

        public ClearMarketDepthCommand( Security security )
        {
            Security = security;
        }
    }
}
