﻿using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using System.ComponentModel;
using System.Windows.Media;

namespace fx.Charting.IndicatorPainters
{
    [Indicator( typeof( AverageDirectionalIndex ) )]
    public class AverageDirectionalIndexPainter : BaseChartIndicatorPainter
    {
        private readonly LineUI chartLineElement_0;
        private readonly LineUI chartLineElement_1;
        private readonly LineUI chartLineElement_2;

        public AverageDirectionalIndexPainter( )
        {
            chartLineElement_0 = new LineUI( )
            {
                Color = Colors.Green
            };
            chartLineElement_1 = new LineUI( )
            {
                Color = Colors.Red
            };
            chartLineElement_2 = new LineUI( )
            {
                Color = Colors.Blue
            };
            AddChildElement( DiPlus );
            AddChildElement( DiMinus );
            AddChildElement( Adx );
        }

        [DisplayName( "DI+" )]
        public LineUI DiPlus
        {
            get
            {
                return chartLineElement_0;
            }
        }

        [DisplayName( "DI-" )]
        public LineUI DiMinus
        {
            get
            {
                return chartLineElement_1;
            }
        }

        [DisplayName( "ADX" )]
        public LineUI Adx
        {
            get
            {
                return chartLineElement_2;
            }
        }

        protected override bool OnDraw( )
        {
            AverageDirectionalIndex indicator = ( AverageDirectionalIndex )Indicator;
            return ( 0 |
                    ( DrawValues( indicator.Dx.Plus, DiPlus ) ? 1 : 0 ) |
                    ( DrawValues( indicator.Dx.Minus, DiMinus ) ? 1 : 0 ) |
                    ( DrawValues( indicator.MovingAverage, Adx ) ? 1 : 0 ) ) !=
                0;
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            DiPlus.Load( storage.GetValue( "DiPlus", ( SettingsStorage )null ) );
            DiMinus.Load( storage.GetValue( "DiMinus", ( SettingsStorage )null ) );
            Adx.Load( storage.GetValue( "Adx", ( SettingsStorage )null ) );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "DiPlus", DiPlus.Save( ) );
            storage.SetValue( "DiMinus", DiMinus.Save( ) );
            storage.SetValue( "Adx", Adx.Save( ) );
        }
    }
}
