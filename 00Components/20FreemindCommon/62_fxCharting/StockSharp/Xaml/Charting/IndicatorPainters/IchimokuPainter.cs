using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Indicators;
using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

#pragma warning disable CA1416

namespace fx.Charting.IndicatorPainters
{
    [Indicator( typeof( Ichimoku ) )]
    public class IchimokuPainter : BaseChartIndicatorPainter
    {
        private readonly LineUI _tenKan;
        private readonly LineUI _kijun;
        private readonly LineUI _chinkou;
        private readonly BandsUI _senkou;

        public IchimokuPainter( )
        {
            _senkou                      = new BandsUI( );
            _tenKan                      = new LineUI( );
            _kijun                       = new LineUI( );
            _chinkou                     = new LineUI( );
            Senkou.Line1.Color           = Colors.SandyBrown;
            Senkou.Line2.Color           = Colors.Thistle;
            Senkou.Line1.AdditionalColor = Senkou.Line2.AdditionalColor = Colors.Thistle.ToTransparent( 50 );
            Tenkan.Color                 = Colors.Red;
            Tenkan.AdditionalColor       = Tenkan.Color.ToTransparent( 50 );
            Kijun.Color                  = Colors.Blue;
            Kijun.AdditionalColor        = Kijun.Color.ToTransparent( 50 );
            Chinkou.Color                = Colors.Green;
            Chinkou.AdditionalColor      = Chinkou.Color.ToTransparent( 50 );

            AddChildElement( Tenkan );
            AddChildElement( Kijun );
            AddChildElement( Chinkou );
            AddChildElement( Senkou );
            Senkou.AddName( Senkou.Line1, "SenkouA" );
            Senkou.AddName( Senkou.Line2, "SenkouB" );
        }

        [Display( Description = "Str764", Name = "Str764", ResourceType = typeof( LocalizedStrings ) )]
        public LineUI Tenkan
        {
            get
            {
                return _tenKan;
            }
        }

        [Display( Description = "Str765", Name = "Str765", ResourceType = typeof( LocalizedStrings ) )]
        public LineUI Kijun
        {
            get
            {
                return _kijun;
            }
        }

        [Display( Description = "Str768", Name = "Str768", ResourceType = typeof( LocalizedStrings ) )]
        public LineUI Chinkou
        {
            get
            {
                return _chinkou;
            }
        }

        [Display( Description = "SenkouRange", Name = "SenkouRange", ResourceType = typeof( LocalizedStrings ) )]
        public BandsUI Senkou
        {
            get
            {
                return _senkou;
            }
        }

        protected override bool OnDraw( )
        {
            Ichimoku indicator = ( Ichimoku )Indicator;

            return ( ( DrawValues( indicator.Tenkan, Tenkan ) ) | ( DrawValues( indicator.Kijun, Kijun ) ) | ( DrawValues( indicator.Chinkou, Chinkou ) ) | ( DrawValues( indicator.SenkouA, indicator.SenkouB, Senkou ) ) );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            Tenkan.Load( storage.GetValue< SettingsStorage >( "Tenkan", null ) );
            Kijun.Load( storage.GetValue< SettingsStorage >( "Kijun", null ) );
            Chinkou.Load( storage.GetValue< SettingsStorage >( "Chinkou", null ) );
            Senkou.Load( storage.GetValue< SettingsStorage >( "Senkou", null ) );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "Tenkan", Tenkan.Save( ) );
            storage.SetValue( "Kijun", Kijun.Save( ) );
            storage.SetValue( "Chinkou", Chinkou.Save( ) );
            storage.SetValue( "Senkou", Senkou.Save( ) );
        }
    }
}
