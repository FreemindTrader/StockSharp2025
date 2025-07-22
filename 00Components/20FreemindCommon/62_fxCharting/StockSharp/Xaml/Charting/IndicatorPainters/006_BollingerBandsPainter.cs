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

/// <summary>
/// The chart element for <see cref="T:StockSharp.Algo.Indicators.BollingerBands" />.
/// </summary>
[Indicator( typeof( BollingerBands ) )]
public class BollingerBandsPainter : BaseChartIndicatorPainter<BollingerBands>
{

    private readonly IChartBandElement _bollingBand;

    private readonly IChartLineElement _bollingMean;

    /// <summary>Create instance.</summary>
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
        chartBandElement.AddName( ( IChartElement ) chartBandElement.Line1, LocalizedStrings.UpperLine );
        chartBandElement.AddName( ( IChartElement ) chartBandElement.Line2, LocalizedStrings.LowerLine );
        chartBandElement.Line2.AddExtraName( "AdditionalColor" );
    }

    /// <summary>
    ///     <see cref="P:StockSharp.Xaml.Charting.IndicatorPainters.BollingerBandsPainter.Band" />.
    /// </summary>
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Band", Description = "Band" )]
    public IChartBandElement Band => this._bollingBand;

    /// <summary>
    ///     <see cref="P:StockSharp.Xaml.Charting.IndicatorPainters.BollingerBandsPainter.MovingAverage" />.
    /// </summary>
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



//using Ecng.Serialization;
//using Ecng.Xaml;
//using StockSharp.Algo.Indicators;
//using StockSharp.Localization;
//using System.ComponentModel.DataAnnotations;
//using System.Windows.Media;

//namespace StockSharp.Xaml.Charting.IndicatorPainters
//{
//    [Indicator( typeof( BollingerBands ) )]
//    public class BollingerBandsPainter : BaseChartIndicatorPainter
//    {
//        private readonly ChartBandElement BandsUI_0;
//        private readonly ChartLineElement chartLineElement_0;

//        public BollingerBandsPainter( )
//        {
//            BandsUI_0 = new ChartBandElement( );
//            Band.Line1.AdditionalColor = Band.Line2.AdditionalColor = Colors.Blue.ToTransparent( 50 );
//            Band.Line1.Color = Band.Line2.Color = Colors.Blue;
//            chartLineElement_0 = new ChartLineElement( )
//            {
//                Color = Colors.Red,
//                AdditionalColor = Colors.Red.ToTransparent( 50 )
//            };
//            AddChildElement( Band );
//            AddChildElement( MovingAverage );
//            Band.AddName( Band.Line1, LocalizedStrings.UpperLine );
//            Band.AddName( Band.Line2, LocalizedStrings.LowerLine );
//            Band.Line2.AddExtraName( "AdditionalColor" );
//        }

//        [Display( Description = "Str1974", Name = "Str1974", ResourceType = typeof( LocalizedStrings ) )]
//        public ChartBandElement Band
//        {
//            get
//            {
//                return BandsUI_0;
//            }
//        }

//        [Display( Description = "Str731", Name = "Str731", ResourceType = typeof( LocalizedStrings ) )]
//        public ChartLineElement MovingAverage
//        {
//            get
//            {
//                return chartLineElement_0;
//            }
//        }

//        protected override bool OnDraw( )
//        {
//            BollingerBands indicator = ( BollingerBands )Indicator;
//            return ( 0 |
//                    ( DrawValues( indicator.UpBand, indicator.LowBand, Band ) ? 1 : 0 ) |
//                    ( DrawValues( indicator.MovingAverage, MovingAverage ) ? 1 : 0 ) ) !=
//                0;
//        }

//        public override void Load( SettingsStorage storage )
//        {
//            base.Load( storage );
//            Band.Load( storage.GetValue( "Band", ( SettingsStorage )null ) );
//            MovingAverage.Load( storage.GetValue( "MovingAverage", ( SettingsStorage )null ) );
//        }

//        public override void Save( SettingsStorage storage )
//        {
//            base.Save( storage );
//            storage.SetValue( "Band", Band.Save( ) );
//            storage.SetValue( "MovingAverage", MovingAverage.Save( ) );
//        }
//    }
//}
