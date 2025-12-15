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
/// I am using this metadata class to provide additional information when user clicks on a TransactionRenderableSeries
/// 
/// The one piece of specific info is ChartDrawData.sTrade.
/// 
/// </summary>
public class TransactionMetadata : IPointMetadata
{
    public event PropertyChangedEventHandler PropertyChanged;

    public TransactionMetadata( ChartDrawData.sTrade trade )
    {
        Transaction = trade;
    }

    public bool IsSelected { get; set; }    

    public ChartDrawData.sTrade Transaction { get; set; }
}
