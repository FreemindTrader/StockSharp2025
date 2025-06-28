using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using System;
using StockSharp.BusinessEntities;

#nullable disable
namespace StockSharp.Studio.Core.Commands;

public class ChartRemoveElementCommand(IChartElement element, Subscription subscription) :
  BaseStudioCommand
{
    public Subscription Subscription { get; } = subscription ?? throw new ArgumentNullException(nameof(subscription));

    public IChartElement Element { get; } = element ?? throw new ArgumentNullException(nameof(element));
}
