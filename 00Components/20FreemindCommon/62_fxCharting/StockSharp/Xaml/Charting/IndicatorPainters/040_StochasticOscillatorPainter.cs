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
/// The chart element for <see cref="T:StockSharp.Algo.Indicators.StochasticOscillator" />.
/// </summary>
[Indicator(typeof(StochasticOscillator))]
public class StochasticOscillatorPainter : BaseChartIndicatorPainter<StochasticOscillator>
{

    private readonly IChartLineElement _lineK;

    private readonly IChartLineElement _lineD;

    /// <summary>Create instance.</summary>
    public StochasticOscillatorPainter()
    {
        IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<StochasticOscillator>.GetColorProvider();
        this._lineK = (IChartLineElement)new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        this._lineD = (IChartLineElement)new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        this.AddChildElement((IChartElement)this.K);
        this.AddChildElement((IChartElement)this.D);
    }

    /// <summary>
    ///     <see cref="P:StockSharp.Algo.Indicators.StochasticOscillator.K" /> line color.
    /// </summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "SOK", Description = "SOK")]
    public IChartLineElement K => this._lineK;

    /// <summary>
    /// <see cref="P:StockSharp.Algo.Indicators.StochasticOscillator.D" /> line color.
    ///     </summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "SOD", Description = "SOD")]
    public IChartLineElement D => this._lineD;

    protected override bool OnDraw(
      StochasticOscillator indicator,
      IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
    {
        bool flag = false | this.DrawValues(data[(IIndicator)indicator.K], (IChartElement)this.K);
        IList<ChartDrawData.IndicatorData> vals;
        if ( data.TryGetValue((IIndicator)indicator.D, out vals) )
            flag |= this.DrawValues(vals, (IChartElement)this.D);
        return flag;
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        PersistableHelper.Load((IPersistable)this.K, storage, "K");
        PersistableHelper.Load((IPersistable)this.D, storage, "D");
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("K", PersistableHelper.Save((IPersistable)this.K));
        storage.SetValue<SettingsStorage>("D", PersistableHelper.Save((IPersistable)this.D));
    }
}



//using Ecng.Serialization;
//using StockSharp.Algo.Indicators;
//using StockSharp.Localization;
//using System.ComponentModel.DataAnnotations;
//using System.Windows.Media;

//namespace StockSharp.Xaml.Charting.IndicatorPainters
//{
//    [Indicator( typeof( StochasticOscillator ) )]
//    public class StochasticOscillatorPainter : BaseChartIndicatorPainter
//    {
//        private readonly ChartLineElement _kline;
//        private readonly ChartLineElement _dline;

//        public StochasticOscillatorPainter( )
//        {
//            _kline = new ChartLineElement( )
//            {
//                Color = Colors.Red
//            };

//            _dline = new ChartLineElement( )
//            {
//                Color = Colors.Blue
//            };

//            AddChildElement( KLine );
//            AddChildElement( DLine );
//        }

//        [Display( Description = "SOK", Name = "SOK", ResourceType = typeof( LocalizedStrings ) )]
//        public ChartLineElement KLine
//        {
//            get
//            {
//                return _kline;
//            }
//        }

//        [Display( Description = "SOD", Name = "SOD", ResourceType = typeof( LocalizedStrings ) )]
//        public ChartLineElement DLine
//        {
//            get
//            {
//                return _dline;
//            }
//        }

//        protected override bool OnDraw( )
//        {
//            StochasticOscillator indicator = ( StochasticOscillator )Indicator;
//            return ( 0 | ( DrawValues( indicator.K, KLine ) ? 1 : 0 ) | ( DrawValues( indicator.D, DLine ) ? 1 : 0 ) ) != 0;
//        }

//        public override void Load( SettingsStorage storage )
//        {
//            base.Load( storage );
//            KLine.Load( storage.GetValue< SettingsStorage >( "K", null ) );
//            DLine.Load( storage.GetValue< SettingsStorage >( "D", null ) );
//        }

//        public override void Save( SettingsStorage storage )
//        {
//            base.Save( storage );
//            storage.SetValue( "K", KLine.Save( ) );
//            storage.SetValue( "D", DLine.Save( ) );
//        }
//    }
//}
