using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using StockSharp.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

[Indicator( typeof( BollingerBands ) )]
public class BollingerBandsPainter : BaseChartIndicatorPainter<BollingerBands>
{

    private readonly IChartBandElement _bollingBand;

    private readonly IChartLineElement _bollingMean;

    public BollingerBandsPainter()
    {
        IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<BollingerBands>.GetColorProvider();
        Color nextColor1 = indicatorColorProvider.GetNextColor();
        Color nextColor2 = indicatorColorProvider.GetNextColor();
        ChartBandElement chartBandElement = new ChartBandElement();
        chartBandElement.Line1.AdditionalColor = chartBandElement.Line2.AdditionalColor = nextColor1.ToTransparent( ( byte ) 30 );
        chartBandElement.Line1.Color = chartBandElement.Line2.Color = nextColor1;
        ChartLineElement chartLineElement = new ChartLineElement()
        {
            Color = nextColor2,
            AdditionalColor = nextColor2.ToTransparent((byte) 30)
        };
        this.AddChildElement( ( IChartElement ) ( this._bollingBand = ( IChartBandElement ) chartBandElement ) );
        this.AddChildElement( ( IChartElement ) ( this._bollingMean = ( IChartLineElement ) chartLineElement ) );
        chartBandElement.SetName( ( IChartElement ) chartBandElement.Line1, LocalizedStrings.UpperLine );
        chartBandElement.SetName( ( IChartElement ) chartBandElement.Line2, LocalizedStrings.LowerLine );
        chartBandElement.Line2.AddExtraName( "AdditionalColor" );
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Band", Description = "Band" )]
    public IChartBandElement Band => this._bollingBand;

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "MovingAverage", Description = "MovingAverage" )]
    public IChartLineElement MovingAverage => this._bollingMean;

    protected override bool OnDraw(
      BollingerBands indicator,
      IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data )
    {
        return ( 0 | ( this.DrawValues( data[ ( IIndicator ) indicator.UpBand ], data[ ( IIndicator ) indicator.LowBand ], ( IChartElement ) this.Band ) ? 1 : 0 ) | ( this.DrawValues( data[ ( IIndicator ) indicator.MovingAverage ], ( IChartElement ) this.MovingAverage ) ? 1 : 0 ) ) != 0;
    }

    public override void Load( SettingsStorage storage )
    {
        base.Load( storage );
        PersistableHelper.Load( ( IPersistable ) this.Band, storage, "Band" );
        PersistableHelper.Load( ( IPersistable ) this.MovingAverage, storage, "MovingAverage" );
    }

    public override void Save( SettingsStorage storage )
    {
        base.Save( storage );
        storage.SetValue<SettingsStorage>( "Band", PersistableHelper.Save( ( IPersistable ) this.Band ) );
        storage.SetValue<SettingsStorage>( "MovingAverage", PersistableHelper.Save( ( IPersistable ) this.MovingAverage ) );
    }
}
