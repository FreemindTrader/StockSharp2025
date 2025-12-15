using SciChart.Charting.Model.ChartData;
using SciChart.Charting.Visuals.PointMarkers;
using SciChart.Charting.Visuals.RenderableSeries;
using StockSharp.Localization;
using StockSharp.Xaml.Charting;
using System;
using System.Diagnostics;

namespace StockSharp.Xaml.Charting;

internal sealed class TransactionSeriesInfo : SeriesInfo
{    
    private ChartDrawData.sTrade _sTrade;

    private readonly Lazy<TransactionSeriesInfo> _lazyTransaction;

    private string _header;

    private string _action;

    private string _time;

    private string _error;

    public string Header => _lazyTransaction.Value._header;

    public string Action => _lazyTransaction.Value._action;

    public string Time => _lazyTransaction.Value._time;

    public string Error => _lazyTransaction.Value._error;

    public TransactionSeriesInfo( IRenderableSeries rSeries, HitTestInfo ht )
      : base( rSeries, ht )
    {
        var myMeta = ht.Metadata as TransactionMetadata;
        _sTrade = myMeta.Transaction;
        _lazyTransaction = new Lazy<TransactionSeriesInfo>( new Func<TransactionSeriesInfo>( GetTransactionSeriesInfo ) );
    }



    public ChartDrawData.sTrade Transaction
    {
        get => _sTrade;
        set
        {
            _sTrade = value;
            OnPropertyChanged( nameof( Transaction ) );
        }
    }

    private TransactionSeriesInfo GetTransactionSeriesInfo( )
    {
        _header = _sTrade.IsOrderFilled( ) ? LocalizedStrings.Order : LocalizedStrings.Trade;
        _action = $"{( _sTrade.OrderSides( ) == StockSharp.Messages.Sides.Buy ? LocalizedStrings.Buy2 : LocalizedStrings.Sell2 )} {_sTrade.Volume}@{_sTrade.Price}";
        _time   = $"{LocalizedStrings.Time}: {_sTrade.Time:d MMM yyyy}, {_sTrade.Time:T}";

        if ( _sTrade.IsError )
            _error = $"{LocalizedStrings.Error}: {_sTrade.ErrorMessage}";
        return this;
    }

    public void Clone( SeriesInfo info )
    {
        this.Clone( info );
        Transaction = ( ( TransactionSeriesInfo ) info ).Transaction;
    }
}
