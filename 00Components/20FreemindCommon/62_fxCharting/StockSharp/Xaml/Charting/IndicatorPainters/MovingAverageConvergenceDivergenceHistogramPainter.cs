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
        private readonly ChartLineElement _macd;
        private readonly ChartLineElement _signal;
        private readonly ChartLineElement _histogram;

        public MovingAverageConvergenceDivergenceHistogramPainter( )
        {
            _macd = new ChartLineElement( )
            {
                Color = Colors.Green
            };

            _signal = new ChartLineElement( )
            {
                Color = Colors.Red
            };

            _histogram = new ChartLineElement( )
            {
                Style = DrawStyles.Histogram,
                Color = Colors.LightGray
            };

            AddChildElement( Macd );
            AddChildElement( Signal );
            AddChildElement( Histogram );
        }

        [DisplayName( "MACD" )]
        public ChartLineElement Macd
        {
            get
            {
                return _macd;
            }
        }

        [Display( Description = "Str805", Name = "Str804", ResourceType = typeof( LocalizedStrings ) )]
        public ChartLineElement Signal
        {
            get
            {
                return _signal;
            }
        }

        [DisplayName( "MACD (Hist)" )]
        public ChartLineElement Histogram
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
