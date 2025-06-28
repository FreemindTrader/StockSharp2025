using System;
using StockSharp.BusinessEntities;
using System.Collections.Generic;
#nullable disable
namespace StockSharp.Studio.Core.Commands;

public class RegisterOrderCommand(Order order) : BaseStudioCommand
{
    public Order Order { get; } = order ?? throw new ArgumentNullException(nameof(order));
}
