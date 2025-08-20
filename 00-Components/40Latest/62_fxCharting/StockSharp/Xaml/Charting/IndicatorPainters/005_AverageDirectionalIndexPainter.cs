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
/// Chart painter for <see cref="T:StockSharp.Algo.Indicators.AverageDirectionalIndex" /> indicator.
/// </summary>
[Indicator( typeof( AverageDirectionalIndex ) )]
public class AverageDirectionalIndexPainter : BaseChartIndicatorPainter<AverageDirectionalIndex>
{

    private readonly IChartLineElement _diPlusLine;

    private readonly IChartLineElement _diMinusLine;

    private readonly IChartLineElement _adxLine;

    /// <summary>Create instance.</summary>
    public AverageDirectionalIndexPainter()
    {
        IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<AverageDirectionalIndex>.GetColorProvider();
        _diPlusLine = ( IChartLineElement ) new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        _diMinusLine = ( IChartLineElement ) new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        _adxLine = ( IChartLineElement ) new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        AddChildElement( ( IChartElement ) DiPlus );
        AddChildElement( ( IChartElement ) DiMinus );
        AddChildElement( ( IChartElement ) Adx );
    }

    /// <summary>
    /// <see cref="P:StockSharp.Xaml.Charting.IndicatorPainters.AverageDirectionalIndexPainter.DiPlus" />.
    ///     </summary>
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "DiPlus", Description = "DiPlusLine" )]
    public IChartLineElement DiPlus => _diPlusLine;


    /// <summary>
    /// <see cref="P:StockSharp.Xaml.Charting.IndicatorPainters.AverageDirectionalIndexPainter.DiMinus" />.
    ///     </summary>
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "DiMinus", Description = "DiMinusLine" )]
    public IChartLineElement DiMinus => _diMinusLine;

    /// <summary>
    /// <see cref="T:StockSharp.Algo.Indicators.AverageDirectionalIndex" />.
    ///     </summary>
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Adx", Description = "AdxLine" )]
    public IChartLineElement Adx => _adxLine;

    protected override bool OnDraw(
      AverageDirectionalIndex indicator,
      IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data )
    {
        bool flag = false | DrawValues(data[(IIndicator) indicator.Dx.Plus], (IChartElement) DiPlus) | DrawValues(data[(IIndicator) indicator.Dx.Minus], (IChartElement) DiMinus);
        IList<ChartDrawData.IndicatorData> vals;
        if ( data.TryGetValue( ( IIndicator ) indicator.MovingAverage, out vals ) )
            flag |= DrawValues( vals, ( IChartElement ) Adx );
        return flag;
    }

    public override void Load( SettingsStorage storage )
    {
        base.Load( storage );
        PersistableHelper.Load( ( IPersistable ) DiPlus, storage, "DiPlus" );
        PersistableHelper.Load( ( IPersistable ) DiMinus, storage, "DiMinus" );
        PersistableHelper.Load( ( IPersistable ) Adx, storage, "Adx" );
    }

    public override void Save( SettingsStorage storage )
    {
        base.Save( storage );
        storage.SetValue<SettingsStorage>( "DiPlus", PersistableHelper.Save( ( IPersistable ) DiPlus ) );
        storage.SetValue<SettingsStorage>( "DiMinus", PersistableHelper.Save( ( IPersistable ) DiMinus ) );
        storage.SetValue<SettingsStorage>( "Adx", PersistableHelper.Save( ( IPersistable ) Adx ) );
    }
}



//using Ecng.Serialization;
//using StockSharp.Algo.Indicators;
//using System.ComponentModel;
//using System.Windows.Media;

//namespace StockSharp.Xaml.Charting.IndicatorPainters
//{
//    [Indicator( typeof( AverageDirectionalIndex ) )]
//    public class AverageDirectionalIndexPainter : BaseChartIndicatorPainter
//    {
//        private readonly ChartLineElement chartLineElement_0;
//        private readonly ChartLineElement chartLineElement_1;
//        private readonly ChartLineElement chartLineElement_2;

//        public AverageDirectionalIndexPainter( )
//        {
//            chartLineElement_0 = new ChartLineElement( )
//            {
//                Color = Colors.Green
//            };
//            chartLineElement_1 = new ChartLineElement( )
//            {
//                Color = Colors.Red
//            };
//            chartLineElement_2 = new ChartLineElement( )
//            {
//                Color = Colors.Blue
//            };
//            AddChildElement( DiPlus );
//            AddChildElement( DiMinus );
//            AddChildElement( Adx );
//        }

//        [DisplayName( "DI+" )]
//        public ChartLineElement DiPlus
//        {
//            get
//            {
//                return chartLineElement_0;
//            }
//        }

//        [DisplayName( "DI-" )]
//        public ChartLineElement DiMinus
//        {
//            get
//            {
//                return chartLineElement_1;
//            }
//        }

//        [DisplayName( "ADX" )]
//        public ChartLineElement Adx
//        {
//            get
//            {
//                return chartLineElement_2;
//            }
//        }

//        protected override bool OnDraw( )
//        {
//            AverageDirectionalIndex indicator = ( AverageDirectionalIndex )Indicator;
//            return ( 0 |
//                    ( DrawValues( indicator.Dx.Plus, DiPlus ) ? 1 : 0 ) |
//                    ( DrawValues( indicator.Dx.Minus, DiMinus ) ? 1 : 0 ) |
//                    ( DrawValues( indicator.MovingAverage, Adx ) ? 1 : 0 ) ) !=
//                0;
//        }

//        public override void Load( SettingsStorage storage )
//        {
//            base.Load( storage );
//            DiPlus.Load( storage.GetValue( "DiPlus", ( SettingsStorage )null ) );
//            DiMinus.Load( storage.GetValue( "DiMinus", ( SettingsStorage )null ) );
//            Adx.Load( storage.GetValue( "Adx", ( SettingsStorage )null ) );
//        }

//        public override void Save( SettingsStorage storage )
//        {
//            base.Save( storage );
//            storage.SetValue( "DiPlus", DiPlus.Save( ) );
//            storage.SetValue( "DiMinus", DiMinus.Save( ) );
//            storage.SetValue( "Adx", Adx.Save( ) );
//        }
//    }
//}
