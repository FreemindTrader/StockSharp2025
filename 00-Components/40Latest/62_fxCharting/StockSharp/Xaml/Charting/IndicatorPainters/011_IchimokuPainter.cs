using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using StockSharp.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;


/// <summary>
/// The chart element for <see cref="T:StockSharp.Algo.Indicators.Ichimoku" />.
/// </summary>
[Indicator(typeof(Ichimoku))]
public class IchimokuPainter : BaseChartIndicatorPainter<Ichimoku>
{

    private readonly IChartLineElement _tenKan;

    private readonly IChartLineElement _kijun;

    private readonly IChartLineElement _chinkou;

    private readonly IChartBandElement _senkou;

    /// <summary>Create instance.</summary>
    public IchimokuPainter()
    {
        IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<Ichimoku>.GetColorProvider();
        ChartBandElement chartBandElement = new ChartBandElement();
        ChartLineElement chartLineElement1 = new ChartLineElement();
        ChartLineElement chartLineElement2 = new ChartLineElement();
        ChartLineElement chartLineElement3 = new ChartLineElement();
        chartBandElement.Line1.Color = indicatorColorProvider.GetNextColor();
        chartBandElement.Line2.Color = indicatorColorProvider.GetNextColor();
        chartBandElement.Line1.AdditionalColor = chartBandElement.Line2.AdditionalColor = chartBandElement.Line2.Color.ToTransparent((byte)30);
        chartLineElement1.Color = indicatorColorProvider.GetNextColor();
        chartLineElement1.AdditionalColor = chartLineElement1.Color.ToTransparent((byte)30);
        chartLineElement2.Color = indicatorColorProvider.GetNextColor();
        chartLineElement2.AdditionalColor = chartLineElement2.Color.ToTransparent((byte)30);
        chartLineElement3.Color = indicatorColorProvider.GetNextColor();
        chartLineElement3.AdditionalColor = chartLineElement3.Color.ToTransparent((byte)30);
        AddChildElement((IChartElement)( _tenKan = (IChartLineElement)chartLineElement1 ));
        AddChildElement((IChartElement)( _kijun = (IChartLineElement)chartLineElement2 ));
        AddChildElement((IChartElement)( _chinkou = (IChartLineElement)chartLineElement3 ));
        AddChildElement((IChartElement)( _senkou = (IChartBandElement)chartBandElement ));
        chartBandElement.AddName((IChartElement)Senkou.Line1, "SenkouA");
        chartBandElement.AddName((IChartElement)Senkou.Line2, "SenkouB");
    }

    /// <summary>
    /// <see cref="P:StockSharp.Algo.Indicators.Ichimoku.Tenkan" /> line.
    ///     </summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "TenkanLine", Description = "TenkanLine")]
    public IChartLineElement Tenkan => _tenKan;

    /// <summary>
    /// <see cref="P:StockSharp.Algo.Indicators.Ichimoku.Kijun" /> line.
    ///     </summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "KijunLine", Description = "KijunLine")]
    public IChartLineElement Kijun => _kijun;

    /// <summary>
    /// <see cref="P:StockSharp.Algo.Indicators.Ichimoku.Chinkou" /> line.
    ///     </summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "ChinkouLine", Description = "ChinkouLine")]
    public IChartLineElement Chinkou => _chinkou;

    /// <summary>Senkou range.</summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "SenkouRange", Description = "SenkouRange")]
    public IChartBandElement Senkou => _senkou;

    protected override bool OnDraw(
      Ichimoku indicator,
      IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
    {
        return ( 0 | ( DrawValues(data[(IIndicator)indicator.Tenkan], (IChartElement)Tenkan) ? 1 : 0 ) | ( DrawValues(data[(IIndicator)indicator.Kijun], (IChartElement)Kijun) ? 1 : 0 ) | ( DrawValues(data[(IIndicator)indicator.Chinkou], (IChartElement)Chinkou) ? 1 : 0 ) | ( DrawValues(data[(IIndicator)indicator.SenkouA], data[(IIndicator)indicator.SenkouB], (IChartElement)Senkou) ? 1 : 0 ) ) != 0;
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        PersistableHelper.Load((IPersistable)Tenkan, storage, "Tenkan");
        PersistableHelper.Load((IPersistable)Kijun, storage, "Kijun");
        PersistableHelper.Load((IPersistable)Chinkou, storage, "Chinkou");
        PersistableHelper.Load((IPersistable)Senkou, storage, "Senkou");
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("Tenkan", PersistableHelper.Save((IPersistable)Tenkan));
        storage.SetValue<SettingsStorage>("Kijun", PersistableHelper.Save((IPersistable)Kijun));
        storage.SetValue<SettingsStorage>("Chinkou", PersistableHelper.Save((IPersistable)Chinkou));
        storage.SetValue<SettingsStorage>("Senkou", PersistableHelper.Save((IPersistable)Senkou));
    }
}


//using Ecng.Serialization;
//using Ecng.Xaml;
//using StockSharp.Algo.Indicators;
//using StockSharp.Localization;
//using System.ComponentModel.DataAnnotations;
//using System.Windows.Media;

//#pragma warning disable CA1416

//namespace StockSharp.Xaml.Charting.IndicatorPainters
//{
//    [Indicator( typeof( Ichimoku ) )]
//    public class IchimokuPainter : BaseChartIndicatorPainter
//    {
//        private readonly ChartLineElement _tenKan;
//        private readonly ChartLineElement _kijun;
//        private readonly ChartLineElement _chinkou;
//        private readonly ChartBandElement _senkou;

//        public IchimokuPainter( )
//        {
//            _senkou                      = new ChartBandElement( );
//            _tenKan                      = new ChartLineElement( );
//            _kijun                       = new ChartLineElement( );
//            _chinkou                     = new ChartLineElement( );
//            Senkou.Line1.Color           = Colors.SandyBrown;
//            Senkou.Line2.Color           = Colors.Thistle;
//            Senkou.Line1.AdditionalColor = Senkou.Line2.AdditionalColor = Colors.Thistle.ToTransparent( 50 );
//            Tenkan.Color                 = Colors.Red;
//            Tenkan.AdditionalColor       = Tenkan.Color.ToTransparent( 50 );
//            Kijun.Color                  = Colors.Blue;
//            Kijun.AdditionalColor        = Kijun.Color.ToTransparent( 50 );
//            Chinkou.Color                = Colors.Green;
//            Chinkou.AdditionalColor      = Chinkou.Color.ToTransparent( 50 );

//            AddChildElement( Tenkan );
//            AddChildElement( Kijun );
//            AddChildElement( Chinkou );
//            AddChildElement( Senkou );
//            Senkou.AddName( Senkou.Line1, "SenkouA" );
//            Senkou.AddName( Senkou.Line2, "SenkouB" );
//        }

//        [Display( Description = "Str764", Name = "Str764", ResourceType = typeof( LocalizedStrings ) )]
//        public ChartLineElement Tenkan
//        {
//            get
//            {
//                return _tenKan;
//            }
//        }

//        [Display( Description = "Str765", Name = "Str765", ResourceType = typeof( LocalizedStrings ) )]
//        public ChartLineElement Kijun
//        {
//            get
//            {
//                return _kijun;
//            }
//        }

//        [Display( Description = "Str768", Name = "Str768", ResourceType = typeof( LocalizedStrings ) )]
//        public ChartLineElement Chinkou
//        {
//            get
//            {
//                return _chinkou;
//            }
//        }

//        [Display( Description = "SenkouRange", Name = "SenkouRange", ResourceType = typeof( LocalizedStrings ) )]
//        public ChartBandElement Senkou
//        {
//            get
//            {
//                return _senkou;
//            }
//        }

//        protected override bool OnDraw( )
//        {
//            Ichimoku indicator = ( Ichimoku )Indicator;

//            return ( ( DrawValues( indicator.Tenkan, Tenkan ) ) | ( DrawValues( indicator.Kijun, Kijun ) ) | ( DrawValues( indicator.Chinkou, Chinkou ) ) | ( DrawValues( indicator.SenkouA, indicator.SenkouB, Senkou ) ) );
//        }

//        public override void Load( SettingsStorage storage )
//        {
//            base.Load( storage );
//            Tenkan.Load( storage.GetValue< SettingsStorage >( "Tenkan", null ) );
//            Kijun.Load( storage.GetValue< SettingsStorage >( "Kijun", null ) );
//            Chinkou.Load( storage.GetValue< SettingsStorage >( "Chinkou", null ) );
//            Senkou.Load( storage.GetValue< SettingsStorage >( "Senkou", null ) );
//        }

//        public override void Save( SettingsStorage storage )
//        {
//            base.Save( storage );
//            storage.SetValue( "Tenkan", Tenkan.Save( ) );
//            storage.SetValue( "Kijun", Kijun.Save( ) );
//            storage.SetValue( "Chinkou", Chinkou.Save( ) );
//            storage.SetValue( "Senkou", Senkou.Save( ) );
//        }
//    }
//}
