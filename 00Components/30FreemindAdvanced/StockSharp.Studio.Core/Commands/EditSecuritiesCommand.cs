using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using System;
using StockSharp.BusinessEntities;
using System.Collections.Generic;
#nullable disable
namespace StockSharp.Studio.Core.Commands;

public class EditSecuritiesCommand : BaseStudioCommand
{
    public EditSecuritiesCommand(Security security)
    {
        this.Securities = security != null ? (IEnumerable<Security>)new Security[1]
        {
      security
        } : throw new ArgumentNullException(nameof(security));
    }

    public EditSecuritiesCommand(IEnumerable<Security> securities)
    {
        this.Securities = securities ?? throw new ArgumentNullException(nameof(securities));
    }

    public IEnumerable<Security> Securities { get; }
}
