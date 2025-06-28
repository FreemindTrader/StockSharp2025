using System;
using StockSharp.BusinessEntities;
using System.Collections.Generic;

#nullable disable
namespace StockSharp.Studio.Core.Commands;

public class RemoveEntitiesCommand<TEntity>(IEnumerable<TEntity> entities) : BaseStudioCommand
{
    public override StudioCommandDirections PossibleDirection => StudioCommandDirections.Top;

    public IEnumerable<TEntity> Entities { get; } = entities ?? throw new ArgumentNullException(nameof(entities));
}
