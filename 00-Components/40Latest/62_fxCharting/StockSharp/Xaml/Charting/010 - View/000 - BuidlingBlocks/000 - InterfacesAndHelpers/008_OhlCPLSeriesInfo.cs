using SciChart.Charting.Model.ChartData;
using SciChart.Charting.Visuals.RenderableSeries;
using StockSharp.Messages;
using System.Diagnostics;
using static TimeframeSegmentRenderableSeries;

#nullable disable
public sealed class OhlCPLSeriesInfo : OhlcSeriesInfo
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private CandlePriceLevel _candlePriceLevel;

    public OhlCPLSeriesInfo( IRenderableSeries rs, HitTestInfo ht ) : base( rs, ht )
    {
        MyMetadata myMeta = ht.Metadata as MyMetadata;
        this.Level = myMeta.CandlePriceLevel;
    }

    public CandlePriceLevel Level
    {
        get => this._candlePriceLevel;
        set
        {
            _candlePriceLevel = value;

            OnPropertyChanged( nameof( Level ) );            
        }
    }



    public void Clone( SeriesInfo other )
    {
        this.Clone( other );
        this.Level = ( ( OhlCPLSeriesInfo ) other ).Level;
    }
}
