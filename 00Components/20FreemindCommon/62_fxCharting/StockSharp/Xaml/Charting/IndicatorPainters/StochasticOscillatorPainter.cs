using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace fx.Charting.IndicatorPainters
{
    [Indicator( typeof( StochasticOscillator ) )]
    public class StochasticOscillatorPainter : BaseChartIndicatorPainter
    {
        private readonly LineUI _kline;
        private readonly LineUI _dline;

        public StochasticOscillatorPainter( )
        {
            _kline = new LineUI( )
            {
                Color = Colors.Red
            };

            _dline = new LineUI( )
            {
                Color = Colors.Blue
            };

            AddChildElement( KLine );
            AddChildElement( DLine );
        }

        [Display( Description = "SOK", Name = "SOK", ResourceType = typeof( LocalizedStrings ) )]
        public LineUI KLine
        {
            get
            {
                return _kline;
            }
        }

        [Display( Description = "SOD", Name = "SOD", ResourceType = typeof( LocalizedStrings ) )]
        public LineUI DLine
        {
            get
            {
                return _dline;
            }
        }

        protected override bool OnDraw( )
        {
            StochasticOscillator indicator = ( StochasticOscillator )Indicator;
            return ( 0 | ( DrawValues( indicator.K, KLine ) ? 1 : 0 ) | ( DrawValues( indicator.D, DLine ) ? 1 : 0 ) ) != 0;
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            KLine.Load( storage.GetValue< SettingsStorage >( "K", null ) );
            DLine.Load( storage.GetValue< SettingsStorage >( "D", null ) );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "K", KLine.Save( ) );
            storage.SetValue( "D", DLine.Save( ) );
        }
    }
}
