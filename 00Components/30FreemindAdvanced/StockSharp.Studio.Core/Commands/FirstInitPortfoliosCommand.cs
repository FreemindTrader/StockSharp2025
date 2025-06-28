using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using System;
using StockSharp.BusinessEntities;
using System.Collections.Generic;
  
#nullable disable
namespace StockSharp.Studio.Core.Commands;

public class FirstInitPortfoliosCommand : BaseStudioCommand
{
    public FirstInitPortfoliosCommand(IEnumerable<Portfolio> portfolios)
    {
        this.Portfolios = portfolios ?? throw new ArgumentNullException(nameof(portfolios));
        if (Ecng.Collections.CollectionHelper.IsEmpty<Portfolio>(this.Portfolios))
            throw new ArgumentException(nameof(portfolios));
    }

    public IEnumerable<Portfolio> Portfolios { get; }
}
