using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Localization;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Media;

#pragma warning disable CA1416

namespace fx.Charting
{
    
    public class ActiveOrdersUI : ChartElement< ActiveOrdersUI >, ICloneable< IChartElement >, INotifyPropertyChanged, IChartComponent, IDrawableChartElement, ICloneable, INotifyPropertyChanging, IChartElement
    {
        private INotifyList< ChartActiveOrderInfo > _activeOrderInfoList =   new CachedSynchronizedSet< ChartActiveOrderInfo >( );
        private Color _buyPendingColor;
        private Color _buyColor;
        private Color _buyBlinkColor;
        private Color _sellPendingColor;
        private Color _sellColor;
        private Color _sellBlinkColor;
        private Color _cancelButtonColor;
        private Color _cancelButtonBackground;
        private Color _foregroundColor;
        private bool _isAnimationEnabled;
        private UIChartBaseViewModel _viewModel;

        public ActiveOrdersUI( )
        {
            SellColor              = Colors.DarkRed;
            SellBlinkColor         = Color.FromRgb( byte.MaxValue, 151, 50 );
            SellPendingColor       = BuyPendingColor = Colors.Gray;
            BuyColor               = Colors.DarkGreen;
            BuyBlinkColor          = Color.FromRgb( 162, 204, 45 );
            ForegroundColor        = Colors.White;
            CancelButtonColor      = Colors.Black;
            CancelButtonBackground = Colors.DarkGray;
            IsAnimationEnabled     = true;
            Orders.Added          += new Action< ChartActiveOrderInfo >( OnOrdersAdded );
        }

        [Display( Description = "BuyPendingColorDot", Name = "BuyPendingColor", Order = 1, ResourceType = typeof( LocalizedStrings ) )]
        public Color BuyPendingColor
        {
            get
            {
                return _buyPendingColor;
            }
            set
            {
                SetField( ref _buyPendingColor, value, nameof( BuyPendingColor ) );
            }
        }

        [Display( Description = "BuyColorDot", Name = "BuyColor", Order = 2, ResourceType = typeof( LocalizedStrings ) )]
        public Color BuyColor
        {
            get
            {
                return _buyColor;
            }
            set
            {
                SetField( ref _buyColor, value, nameof( BuyColor ) );
            }
        }

        [Display( Description = "BuyBlinkColorDot", Name = "BuyBlinkColor", Order = 3, ResourceType = typeof( LocalizedStrings ) )]
        public Color BuyBlinkColor
        {
            get
            {
                return _buyBlinkColor;
            }
            set
            {
                SetField( ref _buyBlinkColor, value, nameof( BuyBlinkColor ) );
            }
        }

        [Display( Description = "SellPendingColorDot", Name = "SellPendingColor", Order = 4, ResourceType = typeof( LocalizedStrings ) )]
        public Color SellPendingColor
        {
            get
            {
                return _sellPendingColor;
            }
            set
            {
                SetField( ref _sellPendingColor, value, nameof( SellPendingColor ) );
            }
        }

        [Display( Description = "SellColorDot", Name = "SellColor", Order = 5, ResourceType = typeof( LocalizedStrings ) )]
        public Color SellColor
        {
            get
            {
                return _sellColor;
            }
            set
            {
                SetField( ref _sellColor, value, nameof( SellColor ) );
            }
        }

        [Display( Description = "SellBlinkColorDot", Name = "SellBlinkColor", Order = 6, ResourceType = typeof( LocalizedStrings ) )]
        public Color SellBlinkColor
        {
            get
            {
                return _sellBlinkColor;
            }
            set
            {
                SetField( ref _sellBlinkColor, value, nameof( SellBlinkColor ) );
            }
        }

        [Display( Description = "CancelButtonColorDot", Name = "CancelButtonColor", Order = 7, ResourceType = typeof( LocalizedStrings ) )]
        public Color CancelButtonColor
        {
            get
            {
                return _cancelButtonColor;
            }
            set
            {
                SetField( ref _cancelButtonColor, value, nameof( CancelButtonColor ) );
            }
        }

        [Display( Description = "CancelButtonBgColorDot", Name = "CancelButtonBgColor", Order = 8, ResourceType = typeof( LocalizedStrings ) )]
        public Color CancelButtonBackground
        {
            get
            {
                return _cancelButtonBackground;
            }
            set
            {
                SetField( ref _cancelButtonBackground, value, nameof( CancelButtonBackground ) );
            }
        }

        [Display( Description = "FontColorDot", Name = "FontColor", Order = 9, ResourceType = typeof( LocalizedStrings ) )]
        public Color ForegroundColor
        {
            get
            {
                return _foregroundColor;
            }
            set
            {
                SetField( ref _foregroundColor, value, nameof( ForegroundColor ) );
            }
        }

        [Display( Description = "AnimationDot", Name = "Animation", Order = 10, ResourceType = typeof( LocalizedStrings ) )]
        public bool IsAnimationEnabled
        {
            get
            {
                return _isAnimationEnabled;
            }
            set
            {
                SetField( ref _isAnimationEnabled, value, nameof( IsAnimationEnabled ) );
            }
        }

        [Browsable( false )]
        public INotifyList< ChartActiveOrderInfo > Orders
        {
            get
            {
                return _activeOrderInfoList;
            }
            private set
            {
                _activeOrderInfoList = value;
            }
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
            return _viewModel = new ChartActiveOrdersVM( this );
        }

        bool IDrawableChartElement.StartDrawing( IEnumerableEx< ChartDrawDataEx.IDrawValue > drawValue )
        {
            return _viewModel.Draw( drawValue );
        }

        void IDrawableChartElement.StartDrawing( )
        {
            _viewModel.Draw( Enumerable.Empty<ChartDrawDataEx.IDrawValue>( ).ToEx( 0 ) );
        }

        protected override bool OnDraw( ChartDrawDataEx data )
        {
            PooledList < ChartDrawDataEx.sActiveOrder > source = data.GetActiveOrders( this );
            if ( source == null || source.IsEmpty( ) )
                return false;
            return ( ( IDrawableChartElement ) this ).StartDrawing( source.Cast<ChartDrawDataEx.IDrawValue>( ).ToEx( source.Count ) );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );

            BuyColor               = storage.GetValue( "BuyColor", ( SettingsStorage )null ).ToColor( );
            BuyBlinkColor          = storage.GetValue( "BuyBlinkColor", ( SettingsStorage )null ).ToColor( );
            BuyPendingColor        = storage.GetValue( "BuyPendingColor", ( SettingsStorage )null ).ToColor( );
            SellColor              = storage.GetValue( "SellColor", ( SettingsStorage )null ).ToColor( );
            SellBlinkColor         = storage.GetValue( "SellBlinkColor", ( SettingsStorage )null ).ToColor( );
            SellPendingColor       = storage.GetValue( "SellPendingColor", ( SettingsStorage )null ).ToColor( );
            ForegroundColor        = storage.GetValue( "ForegroundColor", ( SettingsStorage )null ).ToColor( );
            CancelButtonColor      = storage.GetValue( "CancelButtonColor", ( SettingsStorage )null ).ToColor( );
            CancelButtonBackground = storage.GetValue( "CancelButtonBackground", ( SettingsStorage )null ).ToColor( );
            IsAnimationEnabled     = storage.GetValue( "IsAnimationEnabled", false );

            IEnumerable< SettingsStorage > source = storage.GetValue( "Orders", ( IEnumerable< SettingsStorage > )null );

            Orders = new CachedSynchronizedSet<ChartActiveOrderInfo>( );
            if( source == null )
            {
                return;
            }

            Orders.AddRange( source.Select( s =>
            {
                var info = new ChartActiveOrderInfo( );
                info.Load( s );
                return info;
            } ) );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "BuyColor", BuyColor.ToInt( ) );
            storage.SetValue( "BuyBlinkColor", BuyBlinkColor.ToInt( ) );
            storage.SetValue( "BuyPendingColor", BuyPendingColor.ToInt( ) );
            storage.SetValue( "SellColor", SellColor.ToInt( ) );
            storage.SetValue( "SellBlinkColor", SellBlinkColor.ToInt( ) );
            storage.SetValue( "SellPendingColor", SellPendingColor.ToInt( ) );
            storage.SetValue( "CancelButtonColor", CancelButtonColor.ToInt( ) );
            storage.SetValue( "ForegroundColor", ForegroundColor.ToInt( ) );
            storage.SetValue( "CancelButtonBackground", CancelButtonBackground.ToInt( ) );
            storage.SetValue( "IsAnimationEnabled", IsAnimationEnabled );
            storage.SetValue( "Orders", Orders.Select( i => i.Save( ) ).ToArray( ) );
        }

        internal override ActiveOrdersUI Clone( ActiveOrdersUI other )
        {
            other                        = base.Clone( other );
            other.BuyColor               = BuyColor;
            other.SellColor              = SellColor;
            other.CancelButtonColor      = CancelButtonColor;
            other.ForegroundColor        = ForegroundColor;
            other.CancelButtonBackground = CancelButtonBackground;
            other.BuyPendingColor        = BuyPendingColor;
            other.SellPendingColor       = SellPendingColor;
            other.IsAnimationEnabled     = IsAnimationEnabled;

            other.Orders.AddRange( Orders.Select( i => i.Clone( ) ) );

            return other;
        }

        private void OnOrdersAdded( ChartActiveOrderInfo oderInfo )
        {
            oderInfo.Element = this;
        }
    }
}
