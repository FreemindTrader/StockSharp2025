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
/// The chart element for <see cref="T:StockSharp.Algo.Indicators.Envelope" />.
/// </summary>
[Indicator( typeof( Envelope ) )]
public class EnvelopePainter : BaseChartIndicatorPainter<Envelope>
{

    private readonly IChartBandElement _band;

    private readonly IChartLineElement _movingAverage;

    /// <summary>Create instance.</summary>
    public EnvelopePainter()
    {
        IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<Envelope>.GetColorProvider();
        Color nextColor1 = indicatorColorProvider.GetNextColor();
        Color nextColor2 = indicatorColorProvider.GetNextColor();
        ChartBandElement chartBandElement = new ChartBandElement();
        chartBandElement.Line1.AdditionalColor = chartBandElement.Line2.AdditionalColor = nextColor1.ToTransparent( ( byte ) 30 );
        chartBandElement.Line1.Color = chartBandElement.Line2.Color = nextColor1;
        ChartLineElement chartLineElement = new ChartLineElement()
        {
            Color = nextColor2
        };
        this.AddChildElement( ( IChartElement ) ( this._band = ( IChartBandElement ) chartBandElement ) );
        this.AddChildElement( ( IChartElement ) ( this._movingAverage = ( IChartLineElement ) chartLineElement ) );
        chartBandElement.AddName( ( IChartElement ) chartBandElement.Line1, LocalizedStrings.UpperLine );
        chartBandElement.AddName( ( IChartElement ) chartBandElement.Line2, LocalizedStrings.LowerLine );
        chartBandElement.Line2.AddExtraName( "AdditionalColor" );
    }

    /// <summary>
    /// <see cref="P:StockSharp.Xaml.Charting.IndicatorPainters.EnvelopePainter.Band" />.
    ///     </summary>
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Band", Description = "Band" )]
    public IChartBandElement Band => this._band;

    /// <summary>
    /// <see cref="P:StockSharp.Xaml.Charting.IndicatorPainters.EnvelopePainter.MovingAverage" />.
    ///     </summary>
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "MovingAverage", Description = "MovingAverage" )]
    public IChartLineElement MovingAverage => this._movingAverage;

    protected override bool OnDraw(
      Envelope indicator,
      IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data )
    {
        return ( 0 | ( this.DrawValues( data[ ( IIndicator ) indicator.Upper ], data[ ( IIndicator ) indicator.Lower ], ( IChartElement ) this.Band ) ? 1 : 0 ) | ( this.DrawValues( data[ ( IIndicator ) indicator.Middle ], ( IChartElement ) this.MovingAverage ) ? 1 : 0 ) ) != 0;
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
//    [Indicator( typeof( Envelope ) )]
//    public class EnvelopePainter : BaseChartIndicatorPainter
//    {
//        private readonly ChartBandElement BandsUI_0;
//        private readonly ChartLineElement chartLineElement_0;

//        public EnvelopePainter( )
//        {
//            BandsUI_0 = new ChartBandElement( );
//            Band.Line1.AdditionalColor = Band.Line2.AdditionalColor = Colors.DodgerBlue.ToTransparent( 50 );
//            Band.Line1.Color = Band.Line2.Color = Colors.Blue;
//            chartLineElement_0 = new ChartLineElement( )
//            {
//                Color = Colors.Red
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
//            Envelope indicator = ( Envelope )Indicator;
//            return ( 0 |
//                    ( DrawValues( indicator.Upper, indicator.Lower, Band ) ? 1 : 0 ) |
//                    ( DrawValues( indicator.Middle, MovingAverage ) ? 1 : 0 ) ) !=
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
