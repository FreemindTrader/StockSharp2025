// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.PnLChangedCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.Algo;
using StockSharp.BusinessEntities;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class PnLChangedCommand : EntityCommand<Portfolio>
    {
        public PnLChangedCommand(
          Subscription subscription,
          Portfolio portfolio,
          DateTimeOffset time,
          Decimal? totalPnL,
          Decimal? unrealizedPnL,
          Decimal? commission )
          : base( subscription, portfolio )
        {
            this.Time = time;
            this.TotalPnL = totalPnL;
            this.UnrealizedPnL = unrealizedPnL;
            this.Commission = commission;
        }

        public DateTimeOffset Time { get; }

        public Decimal? TotalPnL { get; }

        public Decimal? UnrealizedPnL { get; }

        public Decimal? Commission { get; set; }
    }
}
