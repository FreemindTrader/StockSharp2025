using Ecng.Serialization;
using fx.Indicators;
using StockSharp.Algo.Indicators;
using StockSharp.Localization;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace fx.Charting.IndicatorPainters
{
    [Indicator( typeof( HewRsiComplex ) )]
    public class HewRsiPainter : BaseChartIndicatorPainter
    {
        private readonly LineUI _overBought;
        private readonly LineUI _hewRsi;
        private readonly LineUI _overSold;

        public HewRsiPainter()
        {
            _overBought = new LineUI()
            {
                Color = Colors.Green
            };

            _hewRsi = new LineUI()
            {
                Color = Colors.Blue
            };

            _overSold = new LineUI()
            {
                Color = Colors.Red
            };

            AddChildElement( OverBought );
            AddChildElement( HewRsi );
            AddChildElement( OverSold );
        }

        [DisplayName( "OverBought" )]
        public LineUI OverBought
        {
            get
            {
                return _overBought;
            }
        }

        [Display( Description = "Str805", Name = "Str804", ResourceType = typeof( LocalizedStrings ) )]
        public LineUI HewRsi
        {
            get
            {
                return _hewRsi;
            }
        }

        [DisplayName( "OverSold" )]
        public LineUI OverSold
        {
            get
            {
                return _overSold;
            }
        }

        protected override bool OnDraw()
        {
            HewRsiComplex indicator = ( HewRsiComplex )Indicator;

            return ( DrawValues( indicator.OverBought, OverBought ) | DrawValues( indicator.Rsi, HewRsi ) | DrawValues( indicator.OverSold, OverSold ) );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            OverBought.Load( storage.GetValue<SettingsStorage>( "OverBought", null ) );
            HewRsi.Load( storage.GetValue<SettingsStorage>( "HewRsi", null ) );
            OverSold.Load( storage.GetValue<SettingsStorage>( "OverSold", null ) );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "OverBought", OverBought.Save() );
            storage.SetValue( "HewRsi", HewRsi.Save() );
            storage.SetValue( "OverSold", OverSold.Save() );
        }
    }
}
