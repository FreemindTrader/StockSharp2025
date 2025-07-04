using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Localization;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;

#pragma warning disable CA1416

namespace fx.Charting
{
    public sealed class QuotesUI : ChartElement<QuotesUI>, ICloneable<IChartElement>, INotifyPropertyChanged, IElementWithXYAxes, IDrawableChartElement, ICloneable, INotifyPropertyChanging, IChartElement
    {
        private ChartIndicatorDrawStyles _drawStyle = ChartIndicatorDrawStyles.Band;
        private LineUI                   _bidLine;
        private LineUI                   _askLine;
        private UIBaseVM                 _viewModel;

        public QuotesUI( )
        {
            BidLine = new LineUI( )
            {
                Color           = Colors.BlueViolet,
                AdditionalColor = Colors.BlueViolet.ToTransparent( 50 )
            };

            AskLine = new LineUI( )
            {
                Color           = Colors.BlueViolet,
                AdditionalColor = Colors.BlueViolet.ToTransparent( 50 )
            };

            BidLine.PropertyChanged += new PropertyChangedEventHandler( OnBidLinePropertyChanged );
            
            AddChildElement( BidLine, true );
            AddChildElement( AskLine, true );
        }

        Color IDrawableChartElement.Color
        {
            get
            {
                return BidLine.AdditionalColor;
            }
        }

        [Browsable( false )]
        public ChartIndicatorDrawStyles Style
        {
            get
            {
                return _drawStyle;
            }
            set
            {
                if ( _drawStyle == value )
                {
                    return;
                }

                if ( value != ChartIndicatorDrawStyles.Band && value != ChartIndicatorDrawStyles.BandOneValue )
                {
                    //throw new InvalidOperationException( LocalizedStrings.Str2063Params.Put( value ) );
                }

                RaisePropertyValueChanging( nameof( Style ), value );
                _drawStyle = value;
                RaisePropertyChanged( nameof( Style ) );
            }
        }

        public LineUI BidLine
        {
            get
            {
                return _bidLine;
            }
            private set
            {
                _bidLine = value;
            }
        }

        public LineUI AskLine
        {
            get
            {
                return _askLine;
            }
            private set
            {
                _askLine = value;
            }
        }

        public override bool CheckAxesCompatible( ChartAxisType? xType, ChartAxisType? yType )
        {
            if ( !yType.HasValue )
            {
                return true;
            }
            ChartAxisType? nullable = yType;
            return nullable.GetValueOrDefault( ) == ChartAxisType.Numeric & nullable.HasValue;
        }

        UIBaseVM IDrawableChartElement.CreateViewModel( IScichartSurfaceVM viewModel )
        {
            _viewModel = new QuotesVM( this );
            return _viewModel;
        }

        bool IDrawableChartElement.StartDrawing( IEnumerableEx<ChartDrawDataEx.IDrawValue> drawValues )
        {
            return _viewModel.Draw( drawValues );
        }

        void IDrawableChartElement.StartDrawing( )
        {
            _viewModel.Draw( Enumerable.Empty<ChartDrawDataEx.IDrawValue>( ).ToEx( 0 ) );
        }

        protected override bool OnDraw( ChartDrawDataEx data )
        {
            //IEnumerableEx<ChartDrawDataEx.IDrawValue> enumerableEx = data.GetBandDrawValues( this );
            //if ( enumerableEx != null && !enumerableEx.IsEmpty<ChartDrawDataEx.IDrawValue>( ) )
            //{
            //    return ( ( IDrawableChartElement )this ).StartDrawing( enumerableEx );
            //}
            return false;
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            if ( storage.ContainsKey( "BidLine" ) )
            {
                BidLine.Load( storage.GetValue<SettingsStorage>( "BidLine", null ) );
            }
            if ( !storage.ContainsKey( "AskLine" ) )
            {
                return;
            }
            AskLine.Load( storage.GetValue<SettingsStorage>( "AskLine", null ) );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "BidLine", BidLine.Save( ) );
            storage.SetValue( "AskLine", AskLine.Save( ) );
        }

        internal override QuotesUI Clone( QuotesUI ui )
        {
            ui = base.Clone( ui );
            BidLine.Clone( ui.BidLine );
            AskLine.Clone( ui.AskLine );
            return ui;
        }

        private void OnBidLinePropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            if ( !( e.PropertyName == "AdditionalColor" ) )
            {
                return;
            }
            RaisePropertyChanged( "Color" );
        }
    }
}
