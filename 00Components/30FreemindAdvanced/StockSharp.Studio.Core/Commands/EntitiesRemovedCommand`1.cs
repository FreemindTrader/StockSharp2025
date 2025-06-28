using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using System;
using StockSharp.BusinessEntities;
using System.Collections.Generic;

#nullable disable
namespace StockSharp.Studio.Core.Commands;

public class EntitiesRemovedCommand<TEntity>(IEnumerable<TEntity> entities) : BaseStudioCommand
{
    public IEnumerable<TEntity> Entities { get; } = entities ?? throw new ArgumentNullException(nameof(entities));
}
