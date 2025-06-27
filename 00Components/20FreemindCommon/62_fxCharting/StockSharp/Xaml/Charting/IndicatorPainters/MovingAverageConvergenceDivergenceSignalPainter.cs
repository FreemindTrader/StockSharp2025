using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.Localization;
using System.ComponentModel;
using System.Windows.Media;

namespace fx.Charting.IndicatorPainters
{
    [Indicator( typeof( MovingAverageConvergenceDivergenceSignal ) )]
    public class MovingAverageConvergenceDivergenceSignalPainter : BaseChartIndicatorPainter
    {
        private readonly LineUI chartLineElement_0;
        private readonly LineUI chartLineElement_1;

        public MovingAverageConvergenceDivergenceSignalPainter( )
        {
            chartLineElement_0 = new LineUI( )
            {
                Color = Colors.Green
            };
            chartLineElement_1 = new LineUI( )
            {
                Color = Colors.Red
            };
            AddChildElement( Macd );
            AddChildElement( SignalMa );
        }

        [DisplayName( "MACD" )]
        
        public LineUI Macd
        {
            get
            {
                return chartLineElement_0;
            }
        }

        public LineUI SignalMa
        {
            get
            {
                return chartLineElement_1;
            }
        }

        protected override bool OnDraw( )
        {
            MovingAverageConvergenceDivergenceSignal indicator = ( MovingAverageConvergenceDivergenceSignal )Indicator;
            return ( 0 | ( DrawValues( indicator.Macd, Macd ) ? 1 : 0 ) | ( DrawValues( indicator.SignalMa, SignalMa ) ? 1 : 0 ) ) != 0;
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            Macd.Load( storage.GetValue( "Macd", ( SettingsStorage )null ) );
            SignalMa.Load( storage.GetValue( "SignalMa", ( SettingsStorage )null ) );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "Macd", Macd.Save( ) );
            storage.SetValue( "SignalMa", SignalMa.Save( ) );
        }
    }
}
