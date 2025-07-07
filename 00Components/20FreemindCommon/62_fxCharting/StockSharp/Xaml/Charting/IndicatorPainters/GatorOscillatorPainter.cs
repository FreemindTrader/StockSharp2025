using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using Ecng.Drawing;

namespace StockSharp.Xaml.Charting.IndicatorPainters
{
    [Indicator( typeof( GatorOscillator ) )]
    public class GatorOscillatorPainter : BaseChartIndicatorPainter
    {
        private readonly ChartLineElement chartLineElement_0;
        private readonly ChartLineElement chartLineElement_1;

        public GatorOscillatorPainter( )
        {
            chartLineElement_0 = new ChartLineElement( )
            {
                Color = Colors.Green
            };
            chartLineElement_1 = new ChartLineElement( )
            {
                Color = Colors.Red
            };
            ChartLineElement histogram1_1 = Histogram1;
            Histogram2.Style = DrawStyles.Histogram;
            histogram1_1.Style = DrawStyles.Histogram;
            ChartLineElement histogram1_2 = Histogram1;
            Histogram2.StrokeThickness = 4;
            histogram1_2.StrokeThickness = 4;
            AddChildElement( Histogram1 );
            AddChildElement( Histogram2 );
        }

        [Display( Description = "Str851", Name = "Str3564", ResourceType = typeof( LocalizedStrings ) )]
        public ChartLineElement Histogram1
        {
            get
            {
                return chartLineElement_0;
            }
        }

        [Display( Description = "Str852", Name = "Str3565", ResourceType = typeof( LocalizedStrings ) )]
        public ChartLineElement Histogram2
        {
            get
            {
                return chartLineElement_1;
            }
        }

        protected override bool OnDraw( )
        {
            GatorOscillator indicator = ( GatorOscillator )Indicator;
            return ( 0 |
                    ( DrawValues( indicator.Histogram1, Histogram1 ) ? 1 : 0 ) |
                    ( DrawValues( indicator.Histogram2, Histogram2 ) ? 1 : 0 ) ) !=
                0;
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            Histogram1.Load( storage.GetValue( "Histogram1", ( SettingsStorage )null ) );
            Histogram2.Load( storage.GetValue( "Histogram2", ( SettingsStorage )null ) );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "Histogram1", Histogram1.Save( ) );
            storage.SetValue( "Histogram2", Histogram2.Save( ) );
        }
    }
}
