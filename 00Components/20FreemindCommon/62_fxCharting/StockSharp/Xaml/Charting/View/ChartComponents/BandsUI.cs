using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Localization;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;

namespace StockSharp.Xaml.Charting
{
    public sealed class BandsUI : ChartElement< BandsUI >, ICloneable< IChartElement >, INotifyPropertyChanged, IChartComponent, IDrawableChartElement, ICloneable, INotifyPropertyChanging, IChartElement
    {
        private ChartIndicatorDrawStyles _drawStyle = ChartIndicatorDrawStyles.Band;
        private LineUI _lineOne;
        private LineUI _lineTwo;
        private UIChartBaseViewModel _viewModel;

        public BandsUI( )
        {
            Line1 = new LineUI( )
            {
                Color = Colors.DarkGreen,
                AdditionalColor = Colors.DarkGreen.ToTransparent( 50 )
            };
            Line2 = new LineUI( )
            {
                Color = Colors.DarkGreen,
                AdditionalColor = Colors.DarkGreen.ToTransparent( 50 )
            };
            Line1.PropertyChanged += new PropertyChangedEventHandler( OnLineOnePropertyChanged );
            AddChildElement( Line1, true );
            AddChildElement( Line2, true );
        }

        Color IDrawableChartElement.Color
        {
            get
            {
                return Line1.AdditionalColor;
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
                if( _drawStyle == value )
                {
                    return;
                }
                if( value != ChartIndicatorDrawStyles.Band && value != ChartIndicatorDrawStyles.BandOneValue )
                {
                    throw new InvalidOperationException( "LocalizedStrings.Str2063Params.Put( value )" );
                }
                RaisePropertyValueChanging( nameof( Style ), value );
                _drawStyle = value;
                RaisePropertyChanged( nameof( Style ) );
            }
        }

        public LineUI Line1
        {
            get
            {
                return _lineOne;
            }
            private set
            {
                _lineOne = value;
            }
        }

        public LineUI Line2
        {
            get
            {
                return _lineTwo;
            }
            private set
            {
                _lineTwo = value;
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

        UIChartBaseViewModel IDrawableChartElement.CreateViewModel( IScichartSurfaceVM viewModel )
        {
            _viewModel = viewModel.Area.XAxisType == ChartAxisType.Numeric ? new BandViewModel<double>( this ) : ( UIChartBaseViewModel )new BandViewModel< DateTime >( this );
            return _viewModel;
        }

        bool IDrawableChartElement.StartDrawing( IEnumerableEx< ChartDrawData.IDrawValue > drawValues )
        {
            return _viewModel.Draw( drawValues );
        }

        void IDrawableChartElement.StartDrawing( )
        {
            _viewModel.Draw( Enumerable.Empty< ChartDrawData.IDrawValue >( ).ToEx( 0 ) );
        }

        protected override bool OnDraw( ChartDrawData data )
        {
            IEnumerableEx< ChartDrawData.IDrawValue > enumerableEx = data.GetBandDrawValues( this );
            if( enumerableEx != null && !enumerableEx.IsEmpty( ) )
            {
                return ( ( IDrawableChartElement )this ).StartDrawing( enumerableEx );
            }
            return false;
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            if( storage.ContainsKey( "Line1" ) )
            {
                Line1.Load( storage.GetValue( "Line1", ( SettingsStorage )null ) );
            }
            if( !storage.ContainsKey( "Line2" ) )
            {
                return;
            }
            Line2.Load( storage.GetValue( "Line2", ( SettingsStorage )null ) );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "Line1", Line1.Save( ) );
            storage.SetValue( "Line2", Line2.Save( ) );
        }

        internal override BandsUI Clone( BandsUI BandsUI_0 )
        {
            BandsUI_0 = base.Clone( BandsUI_0 );
            Line1.Clone( BandsUI_0.Line1 );
            Line2.Clone( BandsUI_0.Line2 );
            return BandsUI_0;
        }

        private void OnLineOnePropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            if( !( e.PropertyName == "AdditionalColor" ) )
            {
                return;
            }
            RaisePropertyChanged( "Color" );
        }
    }
}
