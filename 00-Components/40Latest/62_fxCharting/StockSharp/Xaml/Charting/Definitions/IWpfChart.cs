using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using System.Collections.Generic;

#nullable disable
namespace StockSharp.Xaml.Charting;

public interface IWpfChart : IChart, IPersistable, IChartBuilder, IThemeableChart
{
    IEnumerable<Subscription> Subscriptions
    {
        get;
    }

    Subscription TryGetSubscription( IChartElement element );

    void SetSubscription( IChartElement element, Subscription subscription );

    void AddElement( IChartArea area, IChartCandleElement element, Subscription subscription );

    void AddElement( IChartArea area, IChartIndicatorElement element, Subscription subscription, IIndicator indicator );
}
