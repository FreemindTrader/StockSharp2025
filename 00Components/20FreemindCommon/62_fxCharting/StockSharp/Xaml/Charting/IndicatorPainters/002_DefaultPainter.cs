using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using StockSharp.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

/// <summary>Indicator painter which is used by default.</summary>
public class DefaultPainter : BaseChartIndicatorPainter<IIndicator>
{

    private readonly IChartLineElement _line;

    /// <summary>Create instance.</summary>
    public DefaultPainter()
    {
        IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<IIndicator>.GetColorProvider();

        _line = ( IChartLineElement ) new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };

        AddChildElement( ( IChartElement ) Line );
    }

    /// <summary>Default indicator line element.</summary>
    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Line2", Description = "Line2" )]
    public IChartLineElement Line => _line;

    protected override bool OnDraw( IIndicator indicator, IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data )
    {
        if ( !( indicator is IComplexIndicator complexIndicator ) )
            return DrawValues( data[ indicator ], ( IChartElement ) Line );

        IReadOnlyList<IIndicator> innerIndicators = complexIndicator.InnerIndicators;
        
        int diff = innerIndicators.Count - InnerElements.Count;
        
        if ( diff > 0 )
        {
            IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<IIndicator>.GetColorProvider();
            for ( int i = 0 ; i < diff ; ++i )
            {
                IIndicator myIndicator = innerIndicators[InnerElements.Count + i];
                ChartLineElement element = new ChartLineElement();
                element.Color = myIndicator.Color.HasValue ? myIndicator.Color.GetValueOrDefault().ToWpf() : indicatorColorProvider.GetNextColor();
                element.Style = myIndicator.Style;
                
                AddChildElement( element );
            }
        }
        else if ( diff < 0 )
        {
            int positiveDiff = -diff;

            for ( int i = 0 ; i < positiveDiff ; ++i )
            {
                RemoveChildElement( InnerElements[ InnerElements.Count - 1 ] );
            }
                
        }
        bool drawResult = false;
        int index = 0;
        foreach ( IIndicator key in ( IEnumerable<IIndicator> ) innerIndicators )
        {
            drawResult |= DrawValues( data[ key ], InnerElements[ index++ ] );
        }
            

        return drawResult;
    }

    public override void Load( SettingsStorage storage )
    {
        base.Load( storage );
        PersistableHelper.Load( ( IPersistable ) Line, storage, "Line" );
    }

    public override void Save( SettingsStorage storage )
    {
        base.Save( storage );
        storage.SetValue<SettingsStorage>( "Line", PersistableHelper.Save( ( IPersistable ) Line ) );
    }
}


//using Ecng.Serialization;
//using StockSharp.Localization;
//using System.ComponentModel.DataAnnotations;
//using System.Windows.Media;

//namespace StockSharp.Xaml.Charting.IndicatorPainters
//{
//    public class DefaultPainter : BaseChartIndicatorPainter
//    {
//        private readonly ChartLineElement _line;

//        public DefaultPainter( )
//        {
//            _line = new ChartLineElement( )
//            {
//                Color = Colors.Blue
//            };

//            AddChildElement( Line );
//        }

//        public DefaultPainter( int fifoCapacity )
//        {
//            _line = new ChartLineElement()
//            {
//                FifoCapacity = fifoCapacity,
//                Color        = Colors.Blue
//            };

//            AddChildElement( Line );
//        }

//        [Display( Description = "Str1898", Name = "Str1898", ResourceType = typeof( LocalizedStrings ) )]
//        public ChartLineElement Line
//        {
//            get
//            {
//                return _line;
//            }
//        }

//        protected override bool OnDraw( )
//        {
//            return DrawValues( Indicator, Line );
//        }

//        public override void Load( SettingsStorage storage )
//        {
//            base.Load( storage );
//            Line.Load( storage.GetValue( "Line", ( SettingsStorage )null ) );
//        }

//        public override void Save( SettingsStorage storage )
//        {
//            base.Save( storage );
//            storage.SetValue( "Line", Line.Save( ) );
//        }
//    }
//}
