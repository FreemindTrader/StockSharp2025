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
/// The chart element for <see cref="T:StockSharp.Algo.Indicators.Fractals" />.
/// </summary>
[Indicator(typeof(Fractals))]
public class FractalsPainter : BaseChartIndicatorPainter<Fractals>
{

    private readonly IChartLineElement _upUI;

    private readonly IChartLineElement _downUI;

    /// <summary>Create instance.</summary>
    public FractalsPainter()
    {
        IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<Fractals>.GetColorProvider();
        _upUI = (IChartLineElement)new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        _downUI = (IChartLineElement)new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        Up.Style = Down.Style = DrawStyles.Dot;
        Up.StrokeThickness = Down.StrokeThickness = 8;
        AddChildElement((IChartElement)Up);
        AddChildElement((IChartElement)Down);
    }

    /// <summary>
    ///     <see cref="P:StockSharp.Algo.Indicators.Fractals.Up" /> dots color.
    /// </summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "UpColor", Description = "UpLineColor")]
    public IChartLineElement Up => _upUI;

    /// <summary>
    ///     <see cref="P:StockSharp.Algo.Indicators.Fractals.Down" /> dots color.
    /// </summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "DownColor", Description = "DownLineColor")]
    public IChartLineElement Down => _downUI;

    protected override bool OnDraw(
      Fractals indicator,
      IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
    {
        return ( 0 | ( DrawValues(data[(IIndicator)indicator.Down], (IChartElement)Down) ? 1 : 0 ) | ( DrawValues(data[(IIndicator)indicator.Up], (IChartElement)Up) ? 1 : 0 ) ) != 0;
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        PersistableHelper.Load((IPersistable)Up, storage, "Up");
        PersistableHelper.Load((IPersistable)Down, storage, "Down");
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("Up", PersistableHelper.Save((IPersistable)Up));
        storage.SetValue<SettingsStorage>("Down", PersistableHelper.Save((IPersistable)Down));
    }
}

//using Ecng.Drawing;
//using Ecng.Serialization;
//using StockSharp.Algo.Indicators;
//using StockSharp.Localization;
//using System.ComponentModel.DataAnnotations;
//using System.Windows.Media;

//namespace StockSharp.Xaml.Charting.IndicatorPainters
//{
//    [Indicator( typeof( Fractals ) )]
//    public class FractalsPainter : BaseChartIndicatorPainter
//    {
//        private readonly ChartLineElement _upUI;
//        private readonly ChartLineElement _downUI;

//        public FractalsPainter( )
//        {
//            _upUI           = new ChartLineElement( ) { Color = Colors.Green };
//            _downUI         = new ChartLineElement( ) { Color = Colors.Red };

//            Down.Style      = DrawStyles.Dot;
//            Down.SignalType = TASignalSymbol.PositiveDivergence;
//            Up.Style        = DrawStyles.Dot;
//            Up.SignalType   = TASignalSymbol.NegativeDivergence;

//            Down.StrokeThickness = 4;
//            Up.StrokeThickness   = 4;

//            AddChildElement( Up );
//            AddChildElement( Down );
//        }

//        [Display( Description = "Str2036", Name = "Str2035", ResourceType = typeof( LocalizedStrings ) )]
//        public ChartLineElement Up
//        {
//            get
//            {
//                return _upUI;
//            }
//        }

//        [Display( Description = "Str2038", Name = "Str2037", ResourceType = typeof( LocalizedStrings ) )]
//        public ChartLineElement Down
//        {
//            get
//            {
//                return _downUI;
//            }
//        }

//        protected override bool OnDraw( )
//        {
//            Fractals indicator = ( Fractals )Indicator;
//            return ( 0 | ( DrawValues( indicator.Down, Down ) ? 1 : 0 ) | ( DrawValues( indicator.Up, Up ) ? 1 : 0 ) ) != 0;
//        }

//        public override void Load( SettingsStorage storage )
//        {
//            base.Load( storage );
//            Up.Load( storage.GetValue< SettingsStorage >( "Up", null ) );
//            Down.Load( storage.GetValue< SettingsStorage >( "Down", null ) );
//        }

//        public override void Save( SettingsStorage storage )
//        {
//            base.Save( storage );
//            storage.SetValue( "Up", Up.Save( ) );
//            storage.SetValue( "Down", Down.Save( ) );
//        }
//    }
//}
