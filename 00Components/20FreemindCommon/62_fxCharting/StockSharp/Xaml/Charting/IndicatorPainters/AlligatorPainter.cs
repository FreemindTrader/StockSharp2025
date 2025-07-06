using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace StockSharp.Xaml.Charting.IndicatorPainters
{
    [Indicator( typeof( Alligator ) )]
    
    public class AlligatorPainter : BaseChartIndicatorPainter
    {
        private readonly ChartLineElement _lips;
        private readonly ChartLineElement _teeth;
        private readonly ChartLineElement _jaw;

        public AlligatorPainter( )
        {
            _lips = new ChartLineElement( )
            {
                Color = Colors.Green
            };
            _teeth = new ChartLineElement( )
            {
                Color = Colors.Red
            };
            _jaw = new ChartLineElement( )
            {
                Color = Colors.Blue
            };
            AddChildElement( Lips );
            AddChildElement( Teeth );
            AddChildElement( Jaw );
        }

        [Display( Description = "Str840", Name = "Str840", ResourceType = typeof( LocalizedStrings ) )]
        public ChartLineElement Lips
        {
            get
            {
                return _lips;
            }
        }

        [Display( Description = "Str839", Name = "Str839", ResourceType = typeof( LocalizedStrings ) )]
        public ChartLineElement Teeth
        {
            get
            {
                return _teeth;
            }
        }

        [Display( Description = "Str838", Name = "Str838", ResourceType = typeof( LocalizedStrings ) )]
        public ChartLineElement Jaw
        {
            get
            {
                return _jaw;
            }
        }

        protected override bool OnDraw( )
        {
            Alligator indicator = ( Alligator )Indicator;
            return ( 0 |
                    ( DrawValues( indicator.Lips, Lips ) ? 1 : 0 ) |
                    ( DrawValues( indicator.Teeth, Teeth ) ? 1 : 0 ) |
                    ( DrawValues( indicator.Jaw, Jaw ) ? 1 : 0 ) ) !=
                0;
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            Lips.Load( storage.GetValue( "Lips", ( SettingsStorage )null ) );
            Teeth.Load( storage.GetValue( "Teeth", ( SettingsStorage )null ) );
            Jaw.Load( storage.GetValue( "Jaw", ( SettingsStorage )null ) );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "Lips", Lips.Save( ) );
            storage.SetValue( "Teeth", Teeth.Save( ) );
            storage.SetValue( "Jaw", Jaw.Save( ) );
        }
    }
}
