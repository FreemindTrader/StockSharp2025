using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;

#nullable disable
namespace StockSharp.Studio.Core.Commands;

public class CandleCommand(Subscription subscription, ICandleMessage candle) : BaseStudioCommand
{
    public Subscription Subscription { get; } = subscription ?? throw new ArgumentNullException(nameof(subscription));

    public ICandleMessage Candle { get; } = candle ?? throw new ArgumentNullException(nameof(candle));
}
