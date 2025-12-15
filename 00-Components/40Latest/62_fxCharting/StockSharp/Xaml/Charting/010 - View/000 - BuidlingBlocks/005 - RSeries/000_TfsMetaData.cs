using SciChart.Charting.Model.DataSeries;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockSharp.Xaml.Charting;

/// <summary>
/// I am using this metadata class to provide additional information when user clicks on a TimeframeSegmentRenderableSeries
/// 
/// The one piece of specific info is CandlePriceLevel.
/// </summary>
public class TfsMetadata : IPointMetadata
{
    public event PropertyChangedEventHandler PropertyChanged;

    public TfsMetadata( CandlePriceLevel level )
    {
        CandlePriceLevel = level;
    }

    public bool IsSelected { get; set; }
    public CandlePriceLevel CandlePriceLevel { get; set; }

    //public ChartDrawData.sTrade Transaction { get; set; }
}

