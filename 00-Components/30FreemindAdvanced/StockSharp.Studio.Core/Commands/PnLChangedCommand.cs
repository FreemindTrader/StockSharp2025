using System;
using StockSharp.BusinessEntities;
using System.Collections.Generic;
#nullable disable
namespace StockSharp.Studio.Core.Commands;

public class PnLChangedCommand(
  Subscription subscription,
  Portfolio portfolio,
  DateTimeOffset time,
  Decimal? realizedPnL,
  Decimal? unrealizedPnL,
  Decimal? commission) : EntityCommand<Portfolio>(subscription, portfolio)
{
    public DateTimeOffset Time { get; } = time;

    public Decimal? RealizedPnL { get; } = realizedPnL;

    public Decimal? UnrealizedPnL { get; } = unrealizedPnL;

    public Decimal? Commission { get; set; } = commission;
}
