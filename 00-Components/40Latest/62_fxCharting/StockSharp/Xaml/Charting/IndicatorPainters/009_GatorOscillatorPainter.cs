using Ecng.Drawing;
using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using StockSharp.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;


/// <summary>
/// The chart element for <see cref="T:StockSharp.Algo.Indicators.GatorOscillator" />.
/// </summary>
[Indicator(typeof(GatorOscillator))]
public class GatorOscillatorPainter : BaseChartIndicatorPainter<GatorOscillator>
{

    private readonly IChartLineElement _histogram1;

    private readonly IChartLineElement _histogram2;

    /// <summary>Create instance.</summary>
    public GatorOscillatorPainter()
    {
        IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<GatorOscillator>.GetColorProvider();
        _histogram1 = (IChartLineElement)new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        _histogram2 = (IChartLineElement)new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        Histogram1.Style = Histogram2.Style = DrawStyles.Histogram;
        Histogram1.StrokeThickness = Histogram2.StrokeThickness = 4;
        AddChildElement((IChartElement)Histogram1);
        AddChildElement((IChartElement)Histogram2);
    }

    /// <summary>
    ///     <see cref="P:StockSharp.Algo.Indicators.GatorOscillator.Histogram1" /> line color.
    /// </summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Up", Description = "TopHistogram")]
    public IChartLineElement Histogram1 => _histogram1;


    /// <summary>
    ///     <see cref="P:StockSharp.Algo.Indicators.GatorOscillator.Histogram2" /> line color.
    /// </summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Down", Description = "LowHistogram")]
    public IChartLineElement Histogram2 => _histogram2;

    protected override bool OnDraw(
      GatorOscillator indicator,
      IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
    {
        return ( 0 | ( DrawValues(data[(IIndicator)indicator.Histogram1], (IChartElement)Histogram1) ? 1 : 0 ) 
                   | ( DrawValues(data[(IIndicator)indicator.Histogram2], (IChartElement)Histogram2) ? 1 : 0 ) ) != 0;
    }

    //        protected override bool OnDraw( )
    //        {
    //            GatorOscillator indicator = ( GatorOscillator )Indicator;
    //            return ( 0 |
    //                    ( DrawValues( indicator.Histogram1, Histogram1 ) ? 1 : 0 ) |
    //                    ( DrawValues( indicator.Histogram2, Histogram2 ) ? 1 : 0 ) ) !=
    //                0;
    //        }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        PersistableHelper.Load((IPersistable)Histogram1, storage, "Histogram1");
        PersistableHelper.Load((IPersistable)Histogram2, storage, "Histogram2");
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("Histogram1", PersistableHelper.Save((IPersistable)Histogram1));
        storage.SetValue<SettingsStorage>("Histogram2", PersistableHelper.Save((IPersistable)Histogram2));
    }
}


//using Ecng.Serialization;
//using StockSharp.Algo.Indicators;
//using StockSharp.Localization;
//using System.ComponentModel.DataAnnotations;
//using System.Windows.Media;
//using Ecng.Drawing;

//namespace StockSharp.Xaml.Charting.IndicatorPainters
//{
//    [Indicator( typeof( GatorOscillator ) )]
//    public class GatorOscillatorPainter : BaseChartIndicatorPainter
//    {
//        private readonly ChartLineElement chartLineElement_0;
//        private readonly ChartLineElement chartLineElement_1;

//        public GatorOscillatorPainter( )
//        {
//            chartLineElement_0 = new ChartLineElement( )
//            {
//                Color = Colors.Green
//            };
//            chartLineElement_1 = new ChartLineElement( )
//            {
//                Color = Colors.Red
//            };
//            ChartLineElement histogram1_1 = Histogram1;
//            Histogram2.Style = DrawStyles.Histogram;
//            histogram1_1.Style = DrawStyles.Histogram;
//            ChartLineElement histogram1_2 = Histogram1;
//            Histogram2.StrokeThickness = 4;
//            histogram1_2.StrokeThickness = 4;
//            AddChildElement( Histogram1 );
//            AddChildElement( Histogram2 );
//        }

//        [Display( Description = "Str851", Name = "Str3564", ResourceType = typeof( LocalizedStrings ) )]
//        public ChartLineElement Histogram1
//        {
//            get
//            {
//                return chartLineElement_0;
//            }
//        }

//        [Display( Description = "Str852", Name = "Str3565", ResourceType = typeof( LocalizedStrings ) )]
//        public ChartLineElement Histogram2
//        {
//            get
//            {
//                return chartLineElement_1;
//            }
//        }

//        protected override bool OnDraw( )
//        {
//            GatorOscillator indicator = ( GatorOscillator )Indicator;
//            return ( 0 |
//                    ( DrawValues( indicator.Histogram1, Histogram1 ) ? 1 : 0 ) |
//                    ( DrawValues( indicator.Histogram2, Histogram2 ) ? 1 : 0 ) ) !=
//                0;
//        }

//        public override void Load( SettingsStorage storage )
//        {
//            base.Load( storage );
//            Histogram1.Load( storage.GetValue( "Histogram1", ( SettingsStorage )null ) );
//            Histogram2.Load( storage.GetValue( "Histogram2", ( SettingsStorage )null ) );
//        }

//        public override void Save( SettingsStorage storage )
//        {
//            base.Save( storage );
//            storage.SetValue( "Histogram1", Histogram1.Save( ) );
//            storage.SetValue( "Histogram2", Histogram2.Save( ) );
//        }
//    }
//}
