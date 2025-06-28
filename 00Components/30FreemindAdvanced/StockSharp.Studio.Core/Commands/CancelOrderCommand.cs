using StockSharp.BusinessEntities;
using System;

#nullable disable
namespace StockSharp.Studio.Core.Commands;

public class CancelOrderCommand(Order order) : BaseStudioCommand
{
    public Order Order { get; } = order ?? throw new ArgumentNullException(nameof(order));
}
