﻿using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace fx.Charting.IndicatorPainters
{
    [Indicator( typeof( RelativeVigorIndex ) )]
    [DisplayNameLoc( "Str837" )]
    public class RelativeVigorIndexPainter : BaseChartIndicatorPainter
    {
        private readonly LineUI chartLineElement_0;
        private readonly LineUI chartLineElement_1;

        public RelativeVigorIndexPainter( )
        {
            chartLineElement_0 = new LineUI( )
            {
                Color = Colors.Red
            };
            chartLineElement_1 = new LineUI( )
            {
                Color = Colors.Green
            };
            AddChildElement( Signal );
            AddChildElement( Average );
        }

        [Display( Description = "Str773", Name = "Signal", ResourceType = typeof( LocalizedStrings ) )]
        public LineUI Signal
        {
            get
            {
                return chartLineElement_0;
            }
        }

        [Display( Description = "Str772", Name = "Average", ResourceType = typeof( LocalizedStrings ) )]
        public LineUI Average
        {
            get
            {
                return chartLineElement_1;
            }
        }

        protected override bool OnDraw( )
        {
            RelativeVigorIndex indicator = ( RelativeVigorIndex )Indicator;
            return ( 0 | ( DrawValues( indicator.Signal, Signal ) ? 1 : 0 ) | ( DrawValues( indicator.Average, Average ) ? 1 : 0 ) ) != 0;
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            Signal.Load( storage.GetValue( "Signal", ( SettingsStorage )null ) );
            Average.Load( storage.GetValue( "Average", ( SettingsStorage )null ) );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "Signal", Signal.Save( ) );
            storage.SetValue( "Average", Average.Save( ) );
        }
    }
}
