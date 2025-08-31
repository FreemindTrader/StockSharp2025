
using System;

namespace StockSharp.Xaml.Charting;

/// <summary>
/// A timeframe interface.
/// 
/// This interface is implemented in <see cref="T:StockSharp.Xaml.Charting.TimeframeSegmentDataSeries"/> class.
/// 
/// </summary>
#nullable disable
public interface ITimeframe
{
    TimeSpan? Timeframe
    {
        get;
        set;
    }
}
