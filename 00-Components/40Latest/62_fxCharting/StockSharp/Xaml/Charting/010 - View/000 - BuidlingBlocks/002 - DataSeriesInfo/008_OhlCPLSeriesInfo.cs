using SciChart.Charting.Model.ChartData;
using SciChart.Charting.Visuals.RenderableSeries;
using StockSharp.Messages;
using System.Diagnostics;
namespace StockSharp.Xaml.Charting;
#nullable disable
public sealed class OhlCPLSeriesInfo : OhlcSeriesInfo
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private CandlePriceLevel _candlePriceLevel;

    public OhlCPLSeriesInfo( IRenderableSeries rs, HitTestInfo ht ) : base( rs, ht )
    {
        TfsMetadata myMeta = ht.Metadata as TfsMetadata;
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
