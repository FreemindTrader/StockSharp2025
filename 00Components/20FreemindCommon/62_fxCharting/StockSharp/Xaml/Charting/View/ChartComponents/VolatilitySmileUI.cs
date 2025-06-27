using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace fx.Charting
{
    public sealed class VolatilitySmileUI : ChartComponent< VolatilitySmileUI >
    {
        private LineUI chartLineElement_0;
        private LineUI chartLineElement_1;

        public VolatilitySmileUI( )
        {
            Values = new LineUI( )
            {
                Color = Colors.DarkGreen,
                AdditionalColor = Colors.DarkGreen.ToTransparent( 50 )
            };
            Smile = new LineUI( )
            {
                Color = Colors.DarkGreen,
                AdditionalColor = Colors.DarkGreen.ToTransparent( 50 )
            };
            AddChildElement( Values, false );
            AddChildElement( Smile, false );
        }

        [Display( Description = "SourceValues", Name = "SourceValues", ResourceType = typeof( LocalizedStrings ) )]
        public LineUI Values
        {
            get
            {
                return chartLineElement_0;
            }
            private set
            {
                chartLineElement_0 = value;
            }
        }

        [Display( Description = "VolatilitySmile", Name = "VolatilitySmile", ResourceType = typeof( LocalizedStrings ) )]
        public LineUI Smile
        {
            get
            {
                return chartLineElement_1;
            }
            private set
            {
                chartLineElement_1 = value;
            }
        }

        public override bool CheckAxesCompatible( ChartAxisType? xType, ChartAxisType? yType )
        {
            if( !yType.HasValue )
            {
                return true;
            }
            ChartAxisType? nullable = yType;
            return nullable.GetValueOrDefault( ) == ChartAxisType.Numeric & nullable.HasValue;
        }

        protected override bool OnDraw( ChartDrawDataEx data )
        {
            return ( 0 | ( ( ( IElementWithXYAxes )Values ).Draw( data ) ? 1 : 0 ) | ( ( ( IElementWithXYAxes )Smile ).Draw( data ) ? 1 : 0 ) ) != 0;
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            if( storage.ContainsKey( "Values" ) )
            {
                Values.Load( storage.GetValue( "Values", ( SettingsStorage )null ) );
            }
            if( !storage.ContainsKey( "Smile" ) )
            {
                return;
            }
            Smile.Load( storage.GetValue( "Smile", ( SettingsStorage )null ) );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "Values", Values.Save( ) );
            storage.SetValue( "Smile", Smile.Save( ) );
        }

        internal override VolatilitySmileUI Clone( VolatilitySmileUI chartVolatilitySmileElement_0 )
        {
            chartVolatilitySmileElement_0 = base.Clone( chartVolatilitySmileElement_0 );
            Values.Clone( chartVolatilitySmileElement_0.Values );
            Smile.Clone( chartVolatilitySmileElement_0.Smile );
            return chartVolatilitySmileElement_0;
        }
    }
}
