// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.PnLChangedCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using StockSharp.BusinessEntities;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class PnLChangedCommand : EntityCommand<Portfolio>
    {
        public PnLChangedCommand( Subscription subscription, Portfolio portfolio, DateTimeOffset time, Decimal? realizedPnL, Decimal? unrealizedPnL, Decimal? commission ) : base( subscription, portfolio )
        {
            this.Time = time;
            this.RealizedPnL = realizedPnL;
            this.UnrealizedPnL = unrealizedPnL;            
            this.Commission = commission;
            
            
        }

        public DateTimeOffset Time { get; }

        public Decimal? RealizedPnL { get; }

        public Decimal? UnrealizedPnL { get; }

        public Decimal? Commission { get; set; }
    }
}
