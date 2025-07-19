// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartBuilder
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using StockSharp.Charting;

#nullable disable
namespace StockSharp.Xaml.Charting;

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
        throw new NotImplementedException();
        //return ( IChartIndicatorElement ) new ChartIndicatorElement();
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
