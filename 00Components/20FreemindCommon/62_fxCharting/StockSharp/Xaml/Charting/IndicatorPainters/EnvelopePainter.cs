using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Indicators;
using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace StockSharp.Xaml.Charting.IndicatorPainters
{
    [Indicator( typeof( Envelope ) )]
    public class EnvelopePainter : BaseChartIndicatorPainter
    {
        private readonly BandsUI BandsUI_0;
        private readonly LineUI chartLineElement_0;

        public EnvelopePainter( )
        {
            BandsUI_0 = new BandsUI( );
            Band.Line1.AdditionalColor = Band.Line2.AdditionalColor = Colors.DodgerBlue.ToTransparent( 50 );
            Band.Line1.Color = Band.Line2.Color = Colors.Blue;
            chartLineElement_0 = new LineUI( )
            {
                Color = Colors.Red
            };
            AddChildElement( Band );
            AddChildElement( MovingAverage );
            Band.AddName( Band.Line1, LocalizedStrings.UpperLine );
            Band.AddName( Band.Line2, LocalizedStrings.LowerLine );
            Band.Line2.AddExtraName( "AdditionalColor" );
        }

        [Display( Description = "Str1974", Name = "Str1974", ResourceType = typeof( LocalizedStrings ) )]
        public BandsUI Band
        {
            get
            {
                return BandsUI_0;
            }
        }

        [Display( Description = "Str731", Name = "Str731", ResourceType = typeof( LocalizedStrings ) )]
        public LineUI MovingAverage
        {
            get
            {
                return chartLineElement_0;
            }
        }

        protected override bool OnDraw( )
        {
            Envelope indicator = ( Envelope )Indicator;
            return ( 0 |
                    ( DrawValues( indicator.Upper, indicator.Lower, Band ) ? 1 : 0 ) |
                    ( DrawValues( indicator.Middle, MovingAverage ) ? 1 : 0 ) ) !=
                0;
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            Band.Load( storage.GetValue( "Band", ( SettingsStorage )null ) );
            MovingAverage.Load( storage.GetValue( "MovingAverage", ( SettingsStorage )null ) );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "Band", Band.Save( ) );
            storage.SetValue( "MovingAverage", MovingAverage.Save( ) );
        }
    }
}
