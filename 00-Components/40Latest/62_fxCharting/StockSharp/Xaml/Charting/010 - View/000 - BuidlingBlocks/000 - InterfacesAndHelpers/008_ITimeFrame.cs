
using System;

namespace StockSharp.Xaml.Charting;

#nullable disable
public interface ITimeframe
{
    TimeSpan? Timeframe
    {
        get;
        set;
    }
}
