using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace fx.Charting.IndicatorPainters
{
    [Indicator( typeof( Fractals ) )]
    public class FractalsPainter : BaseChartIndicatorPainter
    {
        private readonly LineUI _upUI;
        private readonly LineUI _downUI;

        public FractalsPainter( )
        {
            _upUI           = new LineUI( ) { Color = Colors.Green };
            _downUI         = new LineUI( ) { Color = Colors.Red };
            
            Down.Style      = ChartIndicatorDrawStyles.Dot;
            Down.SignalType = TASignalSymbol.PositiveDivergence;
            Up.Style        = ChartIndicatorDrawStyles.Dot;
            Up.SignalType   = TASignalSymbol.NegativeDivergence;

            Down.StrokeThickness = 4;
            Up.StrokeThickness   = 4;

            AddChildElement( Up );
            AddChildElement( Down );
        }

        [Display( Description = "Str2036", Name = "Str2035", ResourceType = typeof( LocalizedStrings ) )]
        public LineUI Up
        {
            get
            {
                return _upUI;
            }
        }

        [Display( Description = "Str2038", Name = "Str2037", ResourceType = typeof( LocalizedStrings ) )]
        public LineUI Down
        {
            get
            {
                return _downUI;
            }
        }

        protected override bool OnDraw( )
        {
            Fractals indicator = ( Fractals )Indicator;
            return ( 0 | ( DrawValues( indicator.Down, Down ) ? 1 : 0 ) | ( DrawValues( indicator.Up, Up ) ? 1 : 0 ) ) != 0;
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            Up.Load( storage.GetValue< SettingsStorage >( "Up", null ) );
            Down.Load( storage.GetValue< SettingsStorage >( "Down", null ) );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "Up", Up.Save( ) );
            storage.SetValue( "Down", Down.Save( ) );
        }
    }
}
