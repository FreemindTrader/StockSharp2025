using System;
using StockSharp.BusinessEntities;
using System.Collections.Generic;

#nullable disable
namespace StockSharp.Studio.Core.Commands;

public class ReRegisterOrderCommand(Order oldOrder, Order newOrder) : BaseStudioCommand
{
    public Order OldOrder { get; } = oldOrder ?? throw new ArgumentNullException(nameof(oldOrder));

    public Order NewOrder { get; } = newOrder ?? throw new ArgumentNullException(nameof(newOrder));
}
