using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using System;
using StockSharp.BusinessEntities;

#nullable disable
namespace StockSharp.Studio.Core.Commands;

public class ChartAddElementCommand : BaseStudioCommand
{
    public ChartAddElementCommand(IChartElement element, Subscription subscription)
    {
        this.Element = element ?? throw new ArgumentNullException(nameof(element));
        this.Subscription = subscription ?? throw new ArgumentNullException(nameof(subscription));
    }

    public ChartAddElementCommand(
      IChartElement element,
      Subscription subscription,
      IIndicator indicator)
      : this(element, subscription)
    {
        this.Indicator = indicator ?? throw new ArgumentNullException(nameof(indicator));
    }

    public IChartElement Element { get; }

    public Subscription Subscription { get; }

    public IIndicator Indicator { get; }
}
