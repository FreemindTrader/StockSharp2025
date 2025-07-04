using Ecng.Common;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;
using StockSharp.Charting;

namespace fx.Charting;

/// <summary>
/// The interface that describes the chart element (indicator, candle, etc.).
/// </summary>
public interface IChartElement : IChartPart<IChartElement>
{
    Guid Id
    {
        get;
    }

    IChartElement ParentElement
    {
        get;
    }

    IEnumerable<IChartElement> ChildElements
    {
        get;
    }

    bool IsVisible
    {
        get;
        set;
    }

    bool IsLegend
    {
        get;
        set;
    }

    string XAxisId
    {
        get;
        set;
    }

    string YAxisId
    {
        get;
        set;
    }

    IChart Chart
    {
        get;
    }

    ChartArea ChartArea
    {
        get;
    }

    ChartArea PersistantChartArea
    {
        get;
    }
}
