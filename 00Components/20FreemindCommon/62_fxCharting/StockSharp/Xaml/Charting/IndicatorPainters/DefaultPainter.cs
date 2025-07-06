using Ecng.Serialization;
using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace StockSharp.Xaml.Charting.IndicatorPainters
{
    public class DefaultPainter : BaseChartIndicatorPainter
    {
        private readonly ChartLineElement _line;

        public DefaultPainter( )
        {
            _line = new ChartLineElement( )
            {
                Color = Colors.Blue
            };

            AddChildElement( Line );
        }

        public DefaultPainter( int fifoCapacity )
        {
            _line = new ChartLineElement()
            {
                FifoCapacity = fifoCapacity,
                Color        = Colors.Blue
            };
            
            AddChildElement( Line );
        }

        [Display( Description = "Str1898", Name = "Str1898", ResourceType = typeof( LocalizedStrings ) )]
        public ChartLineElement Line
        {
            get
            {
                return _line;
            }
        }

        protected override bool OnDraw( )
        {
            return DrawValues( Indicator, Line );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            Line.Load( storage.GetValue( "Line", ( SettingsStorage )null ) );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "Line", Line.Save( ) );
        }
    }
}
