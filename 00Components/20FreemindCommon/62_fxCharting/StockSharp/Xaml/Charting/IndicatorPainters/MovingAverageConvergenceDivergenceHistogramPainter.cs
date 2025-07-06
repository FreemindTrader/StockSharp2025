using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.Localization;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace StockSharp.Xaml.Charting.IndicatorPainters
{
    [Indicator( typeof( MovingAverageConvergenceDivergenceHistogram ) )]
    public class MovingAverageConvergenceDivergenceHistogramPainter : BaseChartIndicatorPainter
    {
        private readonly LineUI _macd;
        private readonly LineUI _signal;
        private readonly LineUI _histogram;

        public MovingAverageConvergenceDivergenceHistogramPainter( )
        {
            _macd = new LineUI( )
            {
                Color = Colors.Green
            };

            _signal = new LineUI( )
            {
                Color = Colors.Red
            };

            _histogram = new LineUI( )
            {
                Style = DrawStyles.Histogram,
                Color = Colors.LightGray
            };

            AddChildElement( Macd );
            AddChildElement( Signal );
            AddChildElement( Histogram );
        }

        [DisplayName( "MACD" )]
        public LineUI Macd
        {
            get
            {
                return _macd;
            }
        }

        [Display( Description = "Str805", Name = "Str804", ResourceType = typeof( LocalizedStrings ) )]
        public LineUI Signal
        {
            get
            {
                return _signal;
            }
        }

        [DisplayName( "MACD (Hist)" )]
        public LineUI Histogram
        {
            get
            {
                return _histogram;
            }
        }

        protected override bool OnDraw( )
        {
            MovingAverageConvergenceDivergenceHistogram indicator = ( MovingAverageConvergenceDivergenceHistogram )Indicator;

            return ( ( DrawValues( indicator.Macd, indicator.SignalMa, Histogram, ( d1, d2 ) => d1 - d2 ) ) | DrawValues( indicator.Macd, Macd ) | DrawValues( indicator.SignalMa, Signal ) );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            Macd.Load( storage.GetValue< SettingsStorage >( "Macd", null ) );
            Signal.Load( storage.GetValue< SettingsStorage >( "Signal", null ) );
            Histogram.Load( storage.GetValue< SettingsStorage >( "Histogram", null ) );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "Macd", Macd.Save( ) );
            storage.SetValue( "Signal", Signal.Save( ) );
            storage.SetValue( "Histogram", Histogram.Save( ) );
        }        
    }
}
