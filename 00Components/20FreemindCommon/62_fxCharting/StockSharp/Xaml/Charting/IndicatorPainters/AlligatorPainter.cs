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
        private readonly LineUI _lips;
        private readonly LineUI _teeth;
        private readonly LineUI _jaw;

        public AlligatorPainter( )
        {
            _lips = new LineUI( )
            {
                Color = Colors.Green
            };
            _teeth = new LineUI( )
            {
                Color = Colors.Red
            };
            _jaw = new LineUI( )
            {
                Color = Colors.Blue
            };
            AddChildElement( Lips );
            AddChildElement( Teeth );
            AddChildElement( Jaw );
        }

        [Display( Description = "Str840", Name = "Str840", ResourceType = typeof( LocalizedStrings ) )]
        public LineUI Lips
        {
            get
            {
                return _lips;
            }
        }

        [Display( Description = "Str839", Name = "Str839", ResourceType = typeof( LocalizedStrings ) )]
        public LineUI Teeth
        {
            get
            {
                return _teeth;
            }
        }

        [Display( Description = "Str838", Name = "Str838", ResourceType = typeof( LocalizedStrings ) )]
        public LineUI Jaw
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
