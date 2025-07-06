using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Localization;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Media;

namespace StockSharp.Xaml.Charting
{
    public abstract class TransactionUI< T > : ChartElement< T >, ICloneable< IChartElement >, INotifyPropertyChanged, IChartComponent, IDrawableChartElement, ICloneable, INotifyPropertyChanging, IChartElement
        where T : TransactionUI< T >, new()
    {
        private string _title;
        private Color _buyColor;
        private Color _buyStrokeColor;
        private Color _sellColor;
        private Color _sellStrokeColor;
        private UIChartBaseViewModel _viewModel;

        protected TransactionUI( )
        {
            BuyColor = BuyStrokeColor = Colors.Lime;
            SellColor = SellStrokeColor = Colors.HotPink;
        }

        [Display( Description = "Str1945", Name = "Str215", Order = 20, ResourceType = typeof( LocalizedStrings ) )]
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if( _title == value )
                {
                    return;
                }
                _title = value;
                FullTitle = value;
                RaisePropertyChanged( nameof( Title ) );
            }
        }

        [Display( Description = "Str2007", Name = "Str2006", Order = 30, ResourceType = typeof( LocalizedStrings ) )]
        public Color BuyColor
        {
            get
            {
                return _buyColor;
            }
            set
            {
                if( _buyColor == value )
                {
                    return;
                }
                _buyColor = value;
                RaisePropertyChanged( nameof( BuyColor ) );
            }
        }

        [Display( Description = "Str2009", Name = "Str2008", Order = 40, ResourceType = typeof( LocalizedStrings ) )]
        public Color BuyStrokeColor
        {
            get
            {
                return _buyStrokeColor;
            }
            set
            {
                if( _buyStrokeColor == value )
                {
                    return;
                }
                _buyStrokeColor = value;
                RaisePropertyChanged( nameof( BuyStrokeColor ) );
            }
        }

        [Display( Description = "Str2011", Name = "Str2010", Order = 50, ResourceType = typeof( LocalizedStrings ) )]
        public Color SellColor
        {
            get
            {
                return _sellColor;
            }
            set
            {
                if( _sellColor == value )
                {
                    return;
                }
                _sellColor = value;
                RaisePropertyChanged( nameof( SellColor ) );
            }
        }

        [Display( Description = "Str2013", Name = "Str2012", Order = 60, ResourceType = typeof( LocalizedStrings ) )]
        public Color SellStrokeColor
        {
            get
            {
                return _sellStrokeColor;
            }
            set
            {
                if( _sellStrokeColor == value )
                {
                    return;
                }
                _sellStrokeColor = value;
                RaisePropertyChanged( nameof( SellStrokeColor ) );
            }
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            Title = storage.GetValue( "Title", Title );
            SettingsStorage settings1 = storage.GetValue( "BuyColor", ( SettingsStorage )null );
            SettingsStorage settings2 = storage.GetValue( "BuyStrokeColor", ( SettingsStorage )null );
            SettingsStorage settings3 = storage.GetValue( "SellColor", ( SettingsStorage )null );
            SettingsStorage settings4 = storage.GetValue( "SellStrokeColor", ( SettingsStorage )null );
            if( settings1 != null )
            {
                BuyColor = settings1.ToColor( );
            }
            if( settings2 != null )
            {
                BuyStrokeColor = settings2.ToColor( );
            }
            if( settings3 != null )
            {
                SellColor = settings3.ToColor( );
            }
            if( settings4 == null )
            {
                return;
            }
            SellStrokeColor = settings4.ToColor( );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "Title", Title );
            storage.SetValue( "BuyColor", BuyColor.ToInt( ) );
            storage.SetValue( "BuyStrokeColor", BuyStrokeColor.ToInt( ) );
            storage.SetValue( "SellColor", SellColor.ToInt( ) );
            storage.SetValue( "SellStrokeColor", SellStrokeColor.ToInt( ) );
        }

        internal override T Clone( T other )
        {
            other.BuyColor        = BuyColor;
            other.BuyStrokeColor  = BuyStrokeColor;
            other.SellColor       = SellColor;
            other.SellStrokeColor = SellStrokeColor;
            other.Title           = Title;
            return base.Clone( other );
        }

        Color IDrawableChartElement.Color
        {
            get
            {
                return Colors.Transparent;
            }
        }

        UIChartBaseViewModel IDrawableChartElement.CreateViewModel( IScichartSurfaceVM viewModel )
        {
            return _viewModel = new TransactionVM<T>( ( T ) this );
        }

        bool IDrawableChartElement.StartDrawing( IEnumerableEx< ChartDrawData.IDrawValue > ienumerableEx_0 )
        {
            return _viewModel.Draw( ienumerableEx_0 );
        }

        void IDrawableChartElement.StartDrawing( )
        {
            _viewModel.Draw( Enumerable.Empty< ChartDrawData.IDrawValue >( ).ToEx( 0 ) );
        }
    }
}
