using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSharp.Xaml.Charting;

public class OhlcDataSeriesTF<TX, TY> : SciChart.Charting.Model.DataSeries.OhlcDataSeries<TX, TY> where TX : IComparable
    where TY : IComparable
{
    private TimeSpan? _timeframe;

    public TimeSpan? Timeframe
    {
        get => _timeframe;
        set => _timeframe = value;
    }
}
