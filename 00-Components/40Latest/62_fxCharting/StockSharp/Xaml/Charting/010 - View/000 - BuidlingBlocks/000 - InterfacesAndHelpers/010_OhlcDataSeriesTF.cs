using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSharp.Xaml.Charting;

/// <summary>
/// An OHLC data series with an associated timeframe.
/// 
/// It makes more sense to store the TimeSpan in the DateSeries. So in order to have this effect, I derived OhlcDataSeriesTF from the SciChart OhlcDataSeries class.
/// 
/// </summary>
/// <typeparam name="TX"></typeparam>
/// <typeparam name="TY"></typeparam>
public class OhlcDataSeriesTF<TX, TY> : SciChart.Charting.Model.DataSeries.OhlcDataSeries<TX, TY> where TX : IComparable where TY : IComparable
{
    private TimeSpan? _timeframe;

    public TimeSpan? Timeframe
    {
        get => _timeframe;
        set => _timeframe = value;
    }
}
