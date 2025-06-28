using System;
using StockSharp.BusinessEntities;
using System.Collections.Generic;

#nullable disable
namespace StockSharp.Studio.Core.Commands;

public class FirstInitSecuritiesCommand : BaseStudioCommand
{
    public FirstInitSecuritiesCommand(IEnumerable<Security> securities)
    {
        this.Securities = securities ?? throw new ArgumentNullException(nameof(securities));
        if (Ecng.Collections.CollectionHelper.IsEmpty<Security>(this.Securities))
            throw new ArgumentException(nameof(securities));
    }

    public IEnumerable<Security> Securities { get; }
}
