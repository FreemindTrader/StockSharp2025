using System;
using StockSharp.BusinessEntities;
using System.Collections.Generic;
#nullable disable
namespace StockSharp.Studio.Core.Commands;

public class SubscribeCommand(Subscription subscription) : BaseSubscriptionCommand(subscription)
{
}
