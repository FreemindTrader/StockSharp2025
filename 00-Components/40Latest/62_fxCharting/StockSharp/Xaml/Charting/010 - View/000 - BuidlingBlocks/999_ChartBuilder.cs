
using System;
using StockSharp.Charting;

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>
/// Default implementation of <see cref="T:StockSharp.Charting.IChartBuilder" />.
/// </summary>
public class ChartBuilder : IChartBuilder
{
    public IChartArea CreateArea() => ( IChartArea ) new ChartArea();

    public IChartAxis CreateAxis() => ( IChartAxis ) new ChartAxis();

    public IChartCandleElement CreateCandleElement()
    {
        return ( IChartCandleElement ) new ChartCandleElement();
    }

    public IChartIndicatorElement CreateIndicatorElement()
    {        
        return ( IChartIndicatorElement ) new ChartIndicatorElement();
    }

    public IChartActiveOrdersElement CreateActiveOrdersElement()
    {
        return ( IChartActiveOrdersElement ) new ChartActiveOrdersElement();
    }

    public IChartAnnotationElement CreateAnnotation()
    {
        return ( IChartAnnotationElement ) new ChartAnnotation();
    }

    public IChartBandElement CreateBandElement() => ( IChartBandElement ) new ChartBandElement();

    public IChartLineElement CreateLineElement() => ( IChartLineElement ) new ChartLineElement();

    public IChartLineElement CreateBubbleElement()
    {
        throw new NotImplementedException();
        //( IChartLineElement ) new ChartBubbleElement();        
    }

    public IChartOrderElement CreateOrderElement()
    {
        throw new NotImplementedException();
        //( IChartOrderElement ) new ChartOrderElement();        
    }

    public IChartTradeElement CreateTradeElement()
    {
        throw new NotImplementedException();
        //( IChartTradeElement ) new ChartTradeElement();        
    }
}
