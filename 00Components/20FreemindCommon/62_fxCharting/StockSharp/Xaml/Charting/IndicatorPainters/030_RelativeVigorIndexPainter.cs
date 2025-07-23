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
/// The chart element for <see cref="T:StockSharp.Algo.Indicators.RelativeVigorIndex" />.
/// </summary>
[Display(ResourceType = typeof(LocalizedStrings), Name = "RVI")]
[Indicator(typeof(RelativeVigorIndex))]
public class RelativeVigorIndexPainter : BaseChartIndicatorPainter<RelativeVigorIndex>
{

    private readonly IChartLineElement _signal;

    private readonly IChartLineElement _average;

    /// <summary>Create instance.</summary>
    public RelativeVigorIndexPainter()
    {
        IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<RelativeVigorIndex>.GetColorProvider();
        this._signal = (IChartLineElement)new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        this._average = (IChartLineElement)new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        this.AddChildElement((IChartElement)this.Signal);
        this.AddChildElement((IChartElement)this.Average);
    }

    /// <summary>
    ///     <see cref="P:StockSharp.Algo.Indicators.RelativeVigorIndex.Signal" />.
    /// </summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Signal", Description = "SignalPart")]
    public IChartLineElement Signal => this._signal;

    /// <summary>
    ///     <see cref="P:StockSharp.Algo.Indicators.RelativeVigorIndex.Average" />.
    /// </summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Average", Description = "AveragePart")]
    public IChartLineElement Average => this._average;

    protected override bool OnDraw(
      RelativeVigorIndex indicator,
      IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
    {
        bool flag = false | this.DrawValues(data[(IIndicator)indicator.Average], (IChartElement)this.Average);
        IList<ChartDrawData.IndicatorData> vals;
        if ( data.TryGetValue((IIndicator)indicator.Signal, out vals) )
            flag |= this.DrawValues(vals, (IChartElement)this.Signal);
        return flag;
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        PersistableHelper.Load((IPersistable)this.Signal, storage, "Signal");
        PersistableHelper.Load((IPersistable)this.Average, storage, "Average");
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("Signal", PersistableHelper.Save((IPersistable)this.Signal));
        storage.SetValue<SettingsStorage>("Average", PersistableHelper.Save((IPersistable)this.Average));
    }
}

//using Ecng.Serialization;
//using StockSharp.Algo.Indicators;
//using StockSharp.Localization;
//using System.ComponentModel.DataAnnotations;
//using System.Windows.Media;

//namespace StockSharp.Xaml.Charting.IndicatorPainters
//{
//    [Indicator( typeof( RelativeVigorIndex ) )]

//    public class RelativeVigorIndexPainter : BaseChartIndicatorPainter
//    {
//        private readonly ChartLineElement chartLineElement_0;
//        private readonly ChartLineElement chartLineElement_1;

//        public RelativeVigorIndexPainter( )
//        {
//            chartLineElement_0 = new ChartLineElement( )
//            {
//                Color = Colors.Red
//            };
//            chartLineElement_1 = new ChartLineElement( )
//            {
//                Color = Colors.Green
//            };
//            AddChildElement( Signal );
//            AddChildElement( Average );
//        }

//        [Display( Description = "Str773", Name = "Signal", ResourceType = typeof( LocalizedStrings ) )]
//        public ChartLineElement Signal
//        {
//            get
//            {
//                return chartLineElement_0;
//            }
//        }

//        [Display( Description = "Str772", Name = "Average", ResourceType = typeof( LocalizedStrings ) )]
//        public ChartLineElement Average
//        {
//            get
//            {
//                return chartLineElement_1;
//            }
//        }

//        protected override bool OnDraw( )
//        {
//            RelativeVigorIndex indicator = ( RelativeVigorIndex )Indicator;
//            return ( 0 | ( DrawValues( indicator.Signal, Signal ) ? 1 : 0 ) | ( DrawValues( indicator.Average, Average ) ? 1 : 0 ) ) != 0;
//        }

//        public override void Load( SettingsStorage storage )
//        {
//            base.Load( storage );
//            Signal.Load( storage.GetValue( "Signal", ( SettingsStorage )null ) );
//            Average.Load( storage.GetValue( "Average", ( SettingsStorage )null ) );
//        }

//        public override void Save( SettingsStorage storage )
//        {
//            base.Save( storage );
//            storage.SetValue( "Signal", Signal.Save( ) );
//            storage.SetValue( "Average", Average.Save( ) );
//        }
//    }
//}
