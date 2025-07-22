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
/// Chart painter for <see cref="T:StockSharp.Algo.Indicators.Alligator" /> indicator.
/// </summary>
[Display( ResourceType = typeof( LocalizedStrings ), Name = "Alligator" )]
[Indicator( typeof( Alligator ) )]
public class AlligatorPainter : BaseChartIndicatorPainter<Alligator>
{

    private readonly IChartLineElement _lips;

    private readonly IChartLineElement _teeth;

    private readonly IChartLineElement _jaw;


    /// <summary>Create instance.</summary>
    public AlligatorPainter()
    {
        IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<Alligator>.GetColorProvider();
        this._lips = ( IChartLineElement ) new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        this._teeth = ( IChartLineElement ) new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        this._jaw = ( IChartLineElement ) new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        this.AddChildElement( ( IChartElement ) this.Lips );
        this.AddChildElement( ( IChartElement ) this.Teeth );
        this.AddChildElement( ( IChartElement ) this.Jaw );
    }

    /// <summary>
    /// <see cref="P:StockSharp.Algo.Indicators.Alligator.Lips" />.
    ///     </summary>
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Lips", Description = "Lips" )]
    public IChartLineElement Lips => this._lips;

    /// <summary>
    /// <see cref="P:StockSharp.Algo.Indicators.Alligator.Teeth" />.
    ///     </summary>
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Teeth", Description = "Teeth" )]
    public IChartLineElement Teeth => this._teeth;


    /// <summary>
    /// <see cref="P:StockSharp.Algo.Indicators.Alligator.Jaw" />.
    ///     </summary>
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Jaw", Description = "Jaw" )]
    public IChartLineElement Jaw => this._jaw;

    protected override bool OnDraw(Alligator indicator, IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
    {
        return (DrawValues(data[(IIndicator)indicator.Lips], (IChartElement)this.Lips) | (DrawValues(data[(IIndicator)indicator.Teeth], (IChartElement)this.Teeth)) | (DrawValues(data[(IIndicator)indicator.Jaw], (IChartElement)this.Jaw)));
            
        //return ( 0 | ( this.DrawValues( data[ ( IIndicator ) indicator.Lips ], ( IChartElement ) this.Lips ) ? 1 : 0 ) | ( this.DrawValues( data[ ( IIndicator ) indicator.Teeth ], ( IChartElement ) this.Teeth ) ? 1 : 0 ) | ( this.DrawValues( data[ ( IIndicator ) indicator.Jaw ], ( IChartElement ) this.Jaw ) ? 1 : 0 ) ) != 0;
    }

    public override void Load( SettingsStorage storage )
    {
        base.Load( storage );
        PersistableHelper.Load( ( IPersistable ) this.Lips, storage, "Lips" );
        PersistableHelper.Load( ( IPersistable ) this.Teeth, storage, "Teeth" );
        PersistableHelper.Load( ( IPersistable ) this.Jaw, storage, "Jaw" );
    }

    public override void Save( SettingsStorage storage )
    {
        base.Save( storage );
        storage.SetValue<SettingsStorage>( "Lips", PersistableHelper.Save( ( IPersistable ) this.Lips ) );
        storage.SetValue<SettingsStorage>( "Teeth", PersistableHelper.Save( ( IPersistable ) this.Teeth ) );
        storage.SetValue<SettingsStorage>( "Jaw", PersistableHelper.Save( ( IPersistable ) this.Jaw ) );
    }
}




//using Ecng.Serialization;
//using StockSharp.Algo.Indicators;
//using StockSharp.Localization;
//using System.ComponentModel.DataAnnotations;
//using System.Windows.Media;

//namespace StockSharp.Xaml.Charting.IndicatorPainters
//{
//    [Indicator( typeof( Alligator ) )]

//    public class AlligatorPainter : BaseChartIndicatorPainter
//    {
//        private readonly ChartLineElement _lips;
//        private readonly ChartLineElement _teeth;
//        private readonly ChartLineElement _jaw;

//        public AlligatorPainter( )
//        {
//            _lips = new ChartLineElement( )
//            {
//                Color = Colors.Green
//            };
//            _teeth = new ChartLineElement( )
//            {
//                Color = Colors.Red
//            };
//            _jaw = new ChartLineElement( )
//            {
//                Color = Colors.Blue
//            };
//            AddChildElement( Lips );
//            AddChildElement( Teeth );
//            AddChildElement( Jaw );
//        }

//        [Display( Description = "Str840", Name = "Str840", ResourceType = typeof( LocalizedStrings ) )]
//        public ChartLineElement Lips
//        {
//            get
//            {
//                return _lips;
//            }
//        }

//        [Display( Description = "Str839", Name = "Str839", ResourceType = typeof( LocalizedStrings ) )]
//        public ChartLineElement Teeth
//        {
//            get
//            {
//                return _teeth;
//            }
//        }

//        [Display( Description = "Str838", Name = "Str838", ResourceType = typeof( LocalizedStrings ) )]
//        public ChartLineElement Jaw
//        {
//            get
//            {
//                return _jaw;
//            }
//        }

//        protected override bool OnDraw( )
//        {
//            Alligator indicator = ( Alligator )Indicator;
//            return ( 0 |
//                    ( DrawValues( indicator.Lips, Lips ) ? 1 : 0 ) |
//                    ( DrawValues( indicator.Teeth, Teeth ) ? 1 : 0 ) |
//                    ( DrawValues( indicator.Jaw, Jaw ) ? 1 : 0 ) ) !=
//                0;
//        }

//        public override void Load( SettingsStorage storage )
//        {
//            base.Load( storage );
//            Lips.Load( storage.GetValue( "Lips", ( SettingsStorage )null ) );
//            Teeth.Load( storage.GetValue( "Teeth", ( SettingsStorage )null ) );
//            Jaw.Load( storage.GetValue( "Jaw", ( SettingsStorage )null ) );
//        }

//        public override void Save( SettingsStorage storage )
//        {
//            base.Save( storage );
//            storage.SetValue( "Lips", Lips.Save( ) );
//            storage.SetValue( "Teeth", Teeth.Save( ) );
//            storage.SetValue( "Jaw", Jaw.Save( ) );
//        }
//    }
//}
