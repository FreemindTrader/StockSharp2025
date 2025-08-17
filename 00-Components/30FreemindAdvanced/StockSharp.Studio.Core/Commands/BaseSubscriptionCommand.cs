using StockSharp.BusinessEntities;
using System;


#nullable disable
namespace StockSharp.Studio.Core.Commands;

public abstract class BaseSubscriptionCommand(Subscription subscription) : BaseStudioCommand
{
    public Subscription Subscription { get; } = subscription ?? throw new ArgumentNullException(nameof(subscription));

    public override bool IsPersistent => true;
}
