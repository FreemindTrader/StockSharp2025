using Ecng.Collections;
using Ecng.Common;
using Ecng.Drawing;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Charting;
using StockSharp.Localization;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;

#pragma warning disable CA1416

namespace StockSharp.Xaml.Charting
{
    public sealed class QuotesUI : ChartComponentViewModel<QuotesUI>,  INotifyPropertyChanged, IChartComponent, IChartElementUiDomain, ICloneable, INotifyPropertyChanging, IChartElement
    {
        private DrawStyles _drawStyle = DrawStyles.Band;
        private ChartLineElement                   _bidLine;
        private ChartLineElement                   _askLine;
        private ChartElementUiDomain                 _viewModel;

        public QuotesUI( )
        {
            BidLine = new ChartLineElement( )
            {
                Color           = Colors.BlueViolet,
                AdditionalColor = Colors.BlueViolet.ToTransparent( 50 )
            };

            AskLine = new ChartLineElement( )
            {
                Color           = Colors.BlueViolet,
                AdditionalColor = Colors.BlueViolet.ToTransparent( 50 )
            };

            BidLine.PropertyChanged += new PropertyChangedEventHandler( OnBidLinePropertyChanged );
            
            AddChildElement( BidLine, true );
            AddChildElement( AskLine, true );
        }

        Color IChartElementUiDomain.Color
        {
            get
            {
                return BidLine.AdditionalColor;
            }
        }

        [Browsable( false )]
        public DrawStyles Style
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

                if ( value != DrawStyles.Band && value != DrawStyles.BandOneValue )
                {
                    //throw new InvalidOperationException( LocalizedStrings.Str2063Params.Put( value ) );
                }

                RaisePropertyValueChanging( nameof( Style ), value );
                _drawStyle = value;
                RaisePropertyChanged( nameof( Style ) );
            }
        }

        public ChartLineElement BidLine
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

        public ChartLineElement AskLine
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

        ChartElementUiDomain IChartElementUiDomain.CreateViewModel( IDrawingSurfaceVM viewModel )
        {
            _viewModel = new QuotesVM( this );
            return _viewModel;
        }

        bool IChartElementUiDomain.StartDrawing( IEnumerableEx<ChartDrawData.IDrawValue> drawValues )
        {
            return _viewModel.Draw( drawValues );
        }

        void IChartElementUiDomain.StartDrawing( )
        {
            _viewModel.Draw( Enumerable.Empty<ChartDrawData.IDrawValue>( ).ToEx( 0 ) );
        }

        protected override bool OnDraw( ChartDrawData data )
        {
            //IEnumerableEx<ChartDrawData.IDrawValue> enumerableEx = data.GetBandDrawValues( this );
            //if ( enumerableEx != null && !enumerableEx.IsEmpty<ChartDrawData.IDrawValue>( ) )
            //{
            //    return ( ( IChartElementUiDomain )this ).StartDrawing( enumerableEx );
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
