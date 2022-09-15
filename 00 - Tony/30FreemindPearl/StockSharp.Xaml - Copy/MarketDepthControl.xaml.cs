using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using MoreLinq;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    internal sealed class MarketDepthInfo : NotifiableObject
    {
        private readonly MarketDepthControl _depthControl;
        private Quote _quote;
        private Sides _side;
        private bool _isBest;
        private string _buy;
        private Decimal _buyValue;
        private string _sell;
        private Decimal _sellValue;
        private string _price;
        private string _volume;
        private Decimal _volumeValue;
        private string _ownBuy;
        private string _ownSell;
        private string _board;

        public MarketDepthInfo( MarketDepthControl control, Sides mySide )
        {
            MarketDepthControl marketDepthControl = control;
            if ( marketDepthControl == null )
            {
                throw new ArgumentNullException( "depthControl" );
            }

            _depthControl = marketDepthControl;
            Side = mySide;
        }

        public MarketDepthControl DepthControl
        {
            get
            {
                return _depthControl;
            }
        }

        public Quote Quote
        {
            get
            {
                return _quote;
            }
            private set
            {
                _quote = value;
            }
        }

        public Sides Side
        {
            get
            {
                return _side;
            }
            private set
            {
                if ( value == Side )
                {
                    return;
                }

                _side = value;
                NotifyChanged( nameof( Side ) );
            }
        }

        public bool IsBest
        {
            get
            {
                return _isBest;
            }
            set
            {
                if ( value == IsBest )
                {
                    return;
                }

                _isBest = value;
                NotifyChanged( nameof( IsBest ) );
            }
        }

        public string Buy
        {
            get
            {
                return _buy;
            }
            private set
            {
                if ( value == Buy )
                {
                    return;
                }

                _buy = value;
                NotifyChanged( nameof( Buy ) );
            }
        }

        public Decimal BuyValue
        {
            get
            {
                return _buyValue;
            }
            private set
            {
                if ( value == BuyValue )
                {
                    return;
                }

                _buyValue = value;
                NotifyChanged( nameof( BuyValue ) );
            }
        }

        public string Sell
        {
            get
            {
                return _sell;
            }
            private set
            {
                if ( value == Sell )
                {
                    return;
                }

                _sell = value;
                NotifyChanged( nameof( Sell ) );
            }
        }

        public Decimal SellValue
        {
            get
            {
                return _sellValue;
            }
            private set
            {
                if ( value == SellValue )
                {
                    return;
                }

                _sellValue = value;
                NotifyChanged( nameof( SellValue ) );
            }
        }

        public string Price
        {
            get
            {
                return _price;
            }
            private set
            {
                if ( value == Price )
                {
                    return;
                }

                _price = value;
                NotifyChanged( nameof( Price ) );
            }
        }

        public string Volume
        {
            get
            {
                return _volume;
            }
            private set
            {
                if ( value == Volume )
                {
                    return;
                }

                _volume = value;
                NotifyChanged( nameof( Volume ) );
            }
        }

        public Decimal VolumeValue
        {
            get
            {
                return _volumeValue;
            }
            private set
            {
                if ( value == VolumeValue )
                {
                    return;
                }

                _volumeValue = value;
                NotifyChanged( nameof( VolumeValue ) );
            }
        }

        public string OwnBuy
        {
            get
            {
                return _ownBuy;
            }
            private set
            {
                if ( value == OwnBuy )
                {
                    return;
                }

                _ownBuy = value;
                NotifyChanged( nameof( OwnBuy ) );
            }
        }

        public string OwnSell
        {
            get
            {
                return _ownSell;
            }
            private set
            {
                if ( value == OwnSell )
                {
                    return;
                }

                _ownSell = value;
                NotifyChanged( nameof( OwnSell ) );
            }
        }

        public string Board
        {
            get
            {
                return _board;
            }
            private set
            {
                if ( _board == value )
                {
                    return;
                }

                _board = value;
                NotifyChanged( nameof( Board ) );
            }
        }

        public void Init(
                              Quote quote,
                              OrderRegistry.OrderContainer orders,
                              OrderRegistry.OrderContainer stopOrders,
                              Quote trades,
                              Quote myTrade
                        )
        {
            Quote = quote;

            if ( quote == null && trades == null )
            {
                BuyValue    = Decimal.Zero;
                SellValue   = Decimal.Zero;
                VolumeValue = Decimal.Zero;
                Volume      = Buy = Sell = OwnBuy = OwnSell = null;
                Price       = Board = null;
                IsBest      = false;
            }
            else
            {
                if ( quote != null )
                {
                    VolumeValue = quote.Volume;
                    BuyValue    = quote.OrderDirection == Sides.Buy ? VolumeValue : Decimal.Zero;
                    SellValue   = quote.OrderDirection == Sides.Sell ? VolumeValue : Decimal.Zero;

                    GetBuySellVolume( );
                    GetPriceString( );

                    Decimal OrdersBuyBalance  = orders != null ? orders.TotalBuyBalance : Decimal.Zero;
                    Decimal StopBuyBalance    = stopOrders != null ? stopOrders.TotalBuyBalance : Decimal.Zero;
                    Decimal OrdersSellBalance = orders != null ? orders.TotalSellBalance : Decimal.Zero;
                    Decimal StopSellBalance   = stopOrders != null ? stopOrders.TotalSellBalance : Decimal.Zero;
                    Decimal totalBalance      = OrdersBuyBalance + StopBuyBalance;
                    Decimal stopBalance       = StopSellBalance;
                    Decimal myOwnSell         = OrdersSellBalance + stopBalance;

                    OwnBuy  = totalBalance == Decimal.Zero ? string.Empty : totalBalance.To<string>( );
                    OwnSell = myOwnSell    == Decimal.Zero ? string.Empty : myOwnSell.To<string>( );

                    Board = quote.Security?.Board?.Code;
                }

                if ( trades != null )
                {
                    if ( trades.OrderDirection == Sides.Buy )
                    {
                        Buy = "[" + trades.Volume + "]" + Buy;
                    }
                    else
                    {
                        Sell = "[" + trades.Volume + "]" + Sell;
                    }
                }
                if ( myTrade == null )
                {
                    return;
                }

                if ( myTrade.OrderDirection == Sides.Buy )
                {
                    OwnBuy = "[" + myTrade.Volume + "]" + OwnBuy;
                }
                else
                {
                    OwnSell = "[" + myTrade.Volume + "]" + OwnSell;
                }
            }
        }

        public void GetPriceString( )
        {
            Price = Quote.Price.ToString( DepthControl.PriceTextFormat );
        }

        public void GetBuySellVolume( )
        {
            Volume = VolumeValue.ToString( DepthControl.VolumeTextFormat );
            Buy    = Quote.OrderDirection == Sides.Buy ? Volume : string.Empty;
            Sell   = Quote.OrderDirection == Sides.Sell ? Volume : string.Empty;
        }

        public sealed class OrderRegistry
        {
            private readonly SynchronizedDictionary<Decimal, OrderContainer> _orders = new SynchronizedDictionary<Decimal, OrderContainer>( );

            public OrderContainer Add( Decimal price )
            {
                return _orders.SafeAdd( price );
            }

            public OrderContainer GetContainer( Decimal price )
            {
                return _orders.TryGetValue( price );
            }

            public sealed class OrderContainer
            {
                private readonly Dictionary<Order, Decimal> _orders = new Dictionary<Order, Decimal>( );
                private readonly SyncObject _lock = new SyncObject( );
                private readonly HashSet<Order> _orderSet = new HashSet<Order>( );

                private Decimal _totalBuyBalance;
                private Decimal _totalSellBalance;

                public Decimal TotalBuyBalance
                {
                    get
                    {
                        lock ( _lock )
                        {
                            return _totalBuyBalance;
                        }
                    }

                    private set {; }
                }

                public decimal TotalSellBalance
                {
                    get
                    {
                        lock ( _lock )
                        {
                            return _totalSellBalance;
                        }
                    }
                    private set {; }
                }

                public void RefreshTotals( Order order, Decimal input, OrderStates orderState )
                {
                    if ( order == null )
                    {
                        throw new ArgumentNullException( "order" );
                    }

                    lock ( _lock )
                    {
                        Decimal total = new Decimal( );

                        switch ( orderState )
                        {
                            case OrderStates.Active:
                            {
                                total = input;

                                if ( !_orderSet.Add( order ) )
                                {
                                    total -= _orders[ order ];
                                }

                                _orders[ order ] = input;
                            }                                
                            break;

                            case OrderStates.Done:
                            {
                                if ( !_orderSet.Remove( order ) )
                                {
                                    return;
                                }

                                total = Decimal.Zero - _orders.GetAndRemove( order );
                            }                                
                            break;

                        }

                        if ( total == Decimal.Zero )
                        {
                            return;
                        }

                        if ( order.Direction == Sides.Buy )
                        {
                            _totalBuyBalance += total;
                        }
                        else
                        {
                            _totalSellBalance += total;
                        }
                    }
                }

                public IEnumerable<Order> GetOrderBySide( Sides? side )
                {
                    lock ( _lock )
                    {
                        var output = _orderSet.Where( o =>
                        {
                            if ( !side.HasValue )
                            {
                                return true;
                            }

                            return o.Direction == side.Value;
                        }
                                                     ).ToArray( );

                        return output;
                    }
                }
            }
        }
    }

    public partial class MarketDepthControl : BaseGridControl, IComponentConnector
    {
        public Action<MarketDepthColumns, Quote, MouseButtonEventArgs> CellMouseLeftButtonUp;
        public Action<MarketDepthColumns, Quote, MouseButtonEventArgs> CellMouseRightButtonUp;
        public Action<MarketDepthColumns, Quote, ItemDoubleClickEventArgs> CellMouseLeftDoubleClick;

        public static readonly DependencyProperty MaxDepthProperty               = DependencyProperty.Register( nameof( MaxDepth ),               typeof( int )     , typeof( MarketDepthControl ),                 new PropertyMetadata( 20, ( d,e ) => ( ( MarketDepthControl )d ).UpdateDepth( ( int )e.NewValue ) ), new ValidateValueCallback( o => CheckDepth( ( int )o ) ) );
        public static readonly DependencyProperty ShowOwnVolumeColumnsProperty   = DependencyProperty.Register( nameof( ShowOwnVolumeColumns ),   typeof( bool )    , typeof( MarketDepthControl ),                 new PropertyMetadata( true ) );
        public static readonly DependencyProperty ShowBoardColumnProperty        = DependencyProperty.Register( nameof( ShowBoardColumn ),        typeof( bool )    , typeof( MarketDepthControl ),                 new PropertyMetadata( false ) );
        public static readonly DependencyProperty ShowSingleVolumeColumnProperty = DependencyProperty.Register( nameof( ShowSingleVolumeColumn )                    , typeof( bool ), typeof( MarketDepthControl ), new PropertyMetadata( false ) );
        public static readonly DependencyProperty IsBidsOnTopProperty            = DependencyProperty.Register( nameof( IsBidsOnTop ),            typeof( bool )    , typeof( MarketDepthControl ),                 new PropertyMetadata( false, new PropertyChangedCallback( (d,e) => {
                                                                                                                                                                                                                                                                                            
                                                                                                                                                                                                                                                                                            var dControl = ( MarketDepthControl )d;
                                                                                                                                                                                                                                                                                            dControl._isBidsOnTop = ( bool )e.NewValue;                                                                                                                                                                                                                                                                                                

                                                                                                                                                                                                                                                                                            var array = dControl._itemSource.Reverse( ).ToArray( );
                                                                                                                                                                                                                                                                                            dControl._itemSource.Clear( );
                                                                                                                                                                                                                                                                                            dControl._itemSource.AddRange( array );
                                                                                                                                                                                                                                                                                            
                                                                                                                                                                                                                                                                                       } 
                                                                                                                                                                                                                                                                             ) ) );

        public static readonly DependencyProperty PriceTextFormatProperty        = DependencyProperty.Register( nameof( PriceTextFormat ),        typeof( string )  , typeof( MarketDepthControl ),                 new PropertyMetadata( "0.00", new PropertyChangedCallback( (d,e) =>
                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                            var dControl = ( MarketDepthControl ) d;

                                                                                                                                                                                                                                                                                            dControl._priceTextFormat = ( string )e.NewValue;

                                                                                                                                                                                                                                                                                            if ( dControl._priceTextFormat.IsEmpty( ) )
                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                dControl._priceTextFormat = "0.00";
                                                                                                                                                                                                                                                                                            }

                                                                                                                                                                                                                                                                                            foreach ( MarketDepthInfo dInfo in dControl._itemSource )
                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                if ( dInfo.Quote != null )
                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                    dInfo.GetPriceString( );
                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                        } ) ) );

        private static MarketDepthControl dControl( DependencyObject d )
        {
            return ( MarketDepthControl )d;
        }

        public static readonly DependencyProperty VolumeTextFormatProperty       = DependencyProperty.Register( nameof( VolumeTextFormat ),       typeof( string )  , typeof( MarketDepthControl ),                 new PropertyMetadata( "0", new PropertyChangedCallback( ( d, e ) => {                                                                                                                                                                                                                                                                                            
                                                                                                                                                                                                                                                                                            var dControl = ( MarketDepthControl )d;

                                                                                                                                                                                                                                                                                            dControl._volumeTextFormat = ( string )e.NewValue;

                                                                                                                                                                                                                                                                                            if ( dControl._volumeTextFormat.IsEmpty( ) )
                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                dControl._volumeTextFormat = "0";
                                                                                                                                                                                                                                                                                            }

                                                                                                                                                                                                                                                                                            foreach ( MarketDepthInfo class370 in dControl._itemSource )
                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                if ( class370.Quote != null )
                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                    class370.GetBuySellVolume( );
                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                            
                                                                                                                                                                                                                                                                                        } ) ) );

        public static readonly DependencyProperty MaxVolumeProperty              = DependencyProperty.Register( nameof( MaxVolume ),              typeof( Decimal ) , typeof( MarketDepthControl ),                 new PropertyMetadata( new PropertyChangedCallback( ( d, e ) => ( ( MarketDepthControl )d )._maxVolume     = ( Decimal )e.NewValue ) ) );
        public static readonly DependencyProperty MaxBuyVolumeProperty           = DependencyProperty.Register( nameof( MaxBuyVolume ),           typeof( Decimal ) , typeof( MarketDepthControl ),                 new PropertyMetadata( new PropertyChangedCallback( ( d, e ) => ( ( MarketDepthControl )d )._maxBuyVolume  = ( Decimal )e.NewValue ) ) );
        public static readonly DependencyProperty MaxSellVolumeProperty          = DependencyProperty.Register( nameof( MaxSellVolume ),          typeof( Decimal ) , typeof( MarketDepthControl ),                 new PropertyMetadata( new PropertyChangedCallback( ( d, e ) => ( ( MarketDepthControl )d )._maxSellVolume = ( Decimal )e.NewValue ) ) );

        private readonly MarketDepthInfo.OrderRegistry           _ordersRegistry     = new MarketDepthInfo.OrderRegistry( );
        private readonly MarketDepthInfo.OrderRegistry           _stopOrdersRegistry = new MarketDepthInfo.OrderRegistry( );
        private readonly ObservableCollection<MarketDepthInfo>   _itemSource         = new ObservableCollection<MarketDepthInfo>( );
        private readonly PairSet<MarketDepthColumns, GridColumn> _columnIndecies     = new PairSet<MarketDepthColumns, GridColumn>( );
        private readonly SyncObject                              _lock               = new SyncObject( );

        private int                                              _defaultDepth       = 20;
        private string                                           _priceTextFormat    = "0.00";
        private string                                           _volumeTextFormat   = "0";
        private bool                                             _autoUpdateFormat   = true;
        private MarketDepth                                      _lastDepth;
        private QuoteChangeMessage                               _lastQuoteMsg;
        private bool                                             _needToClear;
        private bool                                             _isDirty;
        private Security                                         _prevSecurity;               
        private bool                                             _isBidsOnTop;        
        private Decimal                                          _maxVolume;
        private Decimal                                          _maxBuyVolume;
        private Decimal                                          _maxSellVolume;


        public const int DefaultDepth = 20;
        public const string DefaultPriceTextFormat = "0.00";
        public const string DefaultVolumeTextFormat = "0";

        public MarketDepthControl( )
        {
            InitializeComponent( );

            _columnIndecies.Add( MarketDepthColumns.OwnBuy , OwnBuyColumn  );
            _columnIndecies.Add( MarketDepthColumns.Buy    , BuyColumn     );
            _columnIndecies.Add( MarketDepthColumns.Price  , PriceColumn   );
            _columnIndecies.Add( MarketDepthColumns.Volume , VolumeColumn  );
            _columnIndecies.Add( MarketDepthColumns.Sell   , SellColumn    );
            _columnIndecies.Add( MarketDepthColumns.OwnSell, OwnSellColumn );
            _columnIndecies.Add( MarketDepthColumns.Board  , BoardColumn   );

            UpdateQuotes( MaxDepth );

            BoardColumn.SetBindings(  BaseColumn.VisibleProperty, this, nameof( ShowBoardColumn )       , BindingMode.TwoWay, null, null );
            VolumeColumn.SetBindings( BaseColumn.VisibleProperty, this, nameof( ShowSingleVolumeColumn ), BindingMode.TwoWay, null, null );
            BuyColumn.SetBindings(    BaseColumn.VisibleProperty, this, nameof( ShowSingleVolumeColumn ), BindingMode.TwoWay, new Ecng.Xaml.Converters.InverseBooleanConverter( ), null );
            SellColumn.SetBindings(   BaseColumn.VisibleProperty, this, nameof( ShowSingleVolumeColumn ), BindingMode.TwoWay, new Ecng.Xaml.Converters.InverseBooleanConverter( ), null );
            OwnBuyColumn.SetBindings( BaseColumn.VisibleProperty, this, nameof( ShowOwnVolumeColumns )  , BindingMode.TwoWay, null, null );
            SellColumn.SetBindings(   BaseColumn.VisibleProperty, this, nameof( ShowOwnVolumeColumns )  , BindingMode.TwoWay, null, null );

            ItemsSource = _itemSource;
            //GuiDispatcher.GlobalDispatcher.AddPeriodicalAction( new Action( UpdateIfDepthDirty ) );
            InitQuotes( 0 );

            MaxBuyVolume  = Decimal.One;
            MaxSellVolume = Decimal.One;
            MaxVolume     = Decimal.One;
        }

        private static bool CheckDepth( int depth )
        {
            if ( depth > 0 && depth <= 100 )
            {
                return true;
            }

            return false;
        }

        private void UpdateDepth( int newDepth )
        {
            if ( !CheckDepth( newDepth ) )
            {
                throw new ArgumentOutOfRangeException( "newDepth", newDepth, LocalizedStrings.Str1219 );
            }

            int diff = newDepth - MaxDepth;
            if ( diff == 0 )
            {
                return;
            }

            UpdateQuotes( diff );
            _defaultDepth = newDepth;
        }

        private void UpdateQuotes( int delta )
        {
            if ( delta > 0 )
            {
                Sides sides = IsBidsOnTop ? Sides.Buy : Sides.Sell;

                for ( int index = 0; index < delta; ++index )
                {
                    _itemSource.Insert( 0, new MarketDepthInfo( this, sides ) );
                }

                Sides invertedSides = sides.Invert( );

                for ( int index = 0; index < delta; ++index )
                {
                    _itemSource.Add( new MarketDepthInfo( this, invertedSides ) );
                }
            }
            else
            {
                delta = delta.Abs( );
                for ( int index = 0; index < delta; ++index )
                {
                    _itemSource.RemoveAt( _itemSource.Count - 1 );
                }

                for ( int index = 0; index < delta; ++index )
                {
                    _itemSource.RemoveAt( 0 );
                }
            }
        }

        public int MaxDepth
        {
            get
            {
                return _defaultDepth;
            }
            set
            {
                SetValue( MaxDepthProperty, value );
            }
        }

        public bool ShowOwnVolumeColumns
        {
            get
            {
                return ( bool )GetValue( ShowOwnVolumeColumnsProperty );
            }
            set
            {
                SetValue( ShowOwnVolumeColumnsProperty, value );
            }
        }

        public bool ShowBoardColumn
        {
            get
            {
                return ( bool )GetValue( ShowBoardColumnProperty );
            }
            set
            {
                SetValue( ShowBoardColumnProperty, value );
            }
        }

        public bool ShowSingleVolumeColumn
        {
            get
            {
                return ( bool )GetValue( ShowSingleVolumeColumnProperty );
            }
            set
            {
                SetValue( ShowSingleVolumeColumnProperty, value );
            }
        }

        public bool IsBidsOnTop
        {
            get
            {
                return _isBidsOnTop;
            }
            set
            {
                SetValue( IsBidsOnTopProperty, value );
            }
        }

        public Quote SelectedQuote
        {
            get
            {
                return ( ( MarketDepthInfo )GetRow( View.FocusedRowHandle ) )?.Quote;
            }
        }

        public string PriceTextFormat
        {
            get
            {
                return _priceTextFormat;
            }
            set
            {
                SetValue( PriceTextFormatProperty, value );
            }
        }

        public string VolumeTextFormat
        {
            get
            {
                return _volumeTextFormat;
            }
            set
            {
                SetValue( VolumeTextFormatProperty, value );
            }
        }

        public Decimal MaxVolume
        {
            get
            {
                return _maxVolume;
            }
            set
            {
                SetValue( MaxVolumeProperty, value );
            }
        }

        public Decimal MaxBuyVolume
        {
            get
            {
                return _maxBuyVolume;
            }
            set
            {
                SetValue( MaxBuyVolumeProperty, value );
            }
        }

        public Decimal MaxSellVolume
        {
            get
            {
                return _maxSellVolume;
            }
            set
            {
                SetValue( MaxSellVolumeProperty, value );
            }
        }

        public bool AutoUpdateFormat
        {
            get
            {
                return _autoUpdateFormat;
            }
            set
            {
                _autoUpdateFormat = value;
            }
        }

        public void UpdateFormat( Security security )
        {
            if ( security == null )
            {
                throw new ArgumentNullException( nameof( security ) );
            }

            this.GuiAsync( ( ) =>
                                {
                                    if ( security.PriceStep.HasValue || security.Decimals.HasValue )
                                    {
                                        PriceTextFormat = security.GetPriceTextFormat( );
                                    }

                                    if ( !security.VolumeStep.HasValue )
                                    {
                                        return;
                                    }

                                    VolumeTextFormat = security.GetVolumeTextFormat( );
                                }
                         );

            lock ( _lock )
            {
                _prevSecurity = security;
            }
        }


        private MarketDepthInfo.OrderRegistry GetOrderRegistry( OrderTypes? nullable_0 )
        {
            OrderTypes? nullable = nullable_0;
            if ( !( nullable.GetValueOrDefault( ) == OrderTypes.Conditional & nullable.HasValue ) )
            {
                return _ordersRegistry;
            }

            return _stopOrdersRegistry;
        }

        public IEnumerable<Order> GetOrders( Decimal price, Sides? side = null, OrderTypes? type = null )
        {
            if ( price == 0 )
            {
                return Enumerable.Empty<Order>( );
            }

            MarketDepthInfo.OrderRegistry[ ] OrderRegistryArray;

            if ( type.HasValue )
            {
                OrderRegistryArray = new MarketDepthInfo.OrderRegistry[ ] { GetOrderRegistry( type ) };
            }
            else
            {
                OrderRegistryArray = new MarketDepthInfo.OrderRegistry[ ]
                                                                            {
                                                                                _stopOrdersRegistry,
                                                                                _ordersRegistry
                                                                            };
            }

            var output = OrderRegistryArray.SelectMany( r =>
                                                            {
                                                                var OrderContainer = r.GetContainer( price );
                                                                IEnumerable<Order> orders;
                                                                if ( OrderContainer == null )
                                                                {
                                                                    orders = null;
                                                                }
                                                                else
                                                                {
                                                                    orders = OrderContainer.GetOrderBySide( side );
                                                                    if ( orders != null )
                                                                    {
                                                                        return orders;
                                                                    }
                                                                }
                                                                return Enumerable.Empty<Order>( );
                                                            }
                                                        ).ToArray<Order>( );

            return output;
        }

        public void ProcessOrder( Order order, Decimal balance, OrderStates state )
        {
            if ( order == null )
            {
                throw new ArgumentNullException( nameof( order ) );
            }

            if ( order.Price == Decimal.Zero )
            {
                return;
            }

            if ( ( uint )( order.State - 1 ) <= 1U )
            {
                GetOrderRegistry( order.Type ).Add( order.Price ).RefreshTotals( order, balance, state );
            }

            lock ( _lock )
            {
                _isDirty = true;
            }
        }

        public void UpdateDepth( MarketDepth depth )
        {
            if ( depth == null )
            {
                throw new ArgumentNullException( nameof( depth ) );
            }

            lock ( _lock )
            {
                _lastDepth   = depth.Clone( );
                _needToClear = false;
                _isDirty     = false;

                if ( _prevSecurity == depth.Security )
                {
                    return;
                }

                _prevSecurity = depth.Security;
            }

            if ( !AutoUpdateFormat )
            {
                return;
            }

            UpdateFormat( depth.Security );
        }

        public void UpdateDepth( QuoteChangeMessage message )
        {
            if ( message == null )
            {
                throw new ArgumentNullException( nameof( message ) );
            }

            lock ( _lock )
            {
                _lastQuoteMsg = ( QuoteChangeMessage )message.Clone( );
                _needToClear  = false;
                _isDirty      = false;
            }
        }

        public void Clear( )
        {
            lock ( _lock )
            {
                _needToClear  = true;
                _isDirty      = false;
                _prevSecurity = null;
                _lastDepth    = null;
                _lastQuoteMsg = null;
            }
        }

        private int GetMarketDepthLevel( Sides direction, int depthLevel )
        {
            bool isBidsOnTop = IsBidsOnTop;

            if ( direction == Sides.Buy )
            {
                if ( !isBidsOnTop )
                {
                    return MaxDepth + depthLevel;
                }

                return MaxDepth - 1 - depthLevel;
            }

            if ( !isBidsOnTop )
            {
                return MaxDepth - 1 - depthLevel;
            }

            return MaxDepth + depthLevel;
        }

        private void UpdateIfDepthDirty( )
        {
            IEnumerable<MarketDepthPair> marketDepthPairs = null;
            bool needInit = false;

            lock ( _lock )
            {
                if ( _needToClear )
                {
                    _needToClear     = false;
                    marketDepthPairs = Enumerable.Empty<MarketDepthPair>( );
                }
                else if ( _lastDepth != null )
                {
                    marketDepthPairs = _lastDepth.GetTopPairs( MaxDepth );
                    _lastDepth       = null;
                }
                else if ( _lastQuoteMsg != null )
                {
                    if ( _prevSecurity != null )
                    {
                        marketDepthPairs = _lastQuoteMsg.ToMarketDepth( _prevSecurity, null ).GetTopPairs( MaxDepth );
                    }

                    _lastQuoteMsg = null;
                }
                else if ( _isDirty )
                {
                    needInit = true;
                    _isDirty = false;
                }
            }

            if ( needInit )
            {
                foreach ( MarketDepthInfo info in _itemSource )
                {
                    Quote quote = info.Quote;

                    if ( quote != null )
                    {
                        info.Init( quote, _ordersRegistry.GetContainer( quote.Price ), _stopOrdersRegistry.GetContainer( quote.Price ), null, null );
                    }
                }
            }
            else
            {
                if ( marketDepthPairs == null )
                {
                    return;
                }
                
                var bidVolume = new Decimal( );
                var askVolume = new Decimal( );
                int index     = 0;

                foreach ( MarketDepthPair pair in marketDepthPairs )
                {
                    MarketDepthInfo bid = _itemSource[ GetMarketDepthLevel( Sides.Buy, index ) ];

                    if ( pair.Bid != null )
                    {
                        bid.Init( pair.Bid, _ordersRegistry.GetContainer( pair.Bid.Price ), _stopOrdersRegistry.GetContainer( pair.Bid.Price ), null, null );
                        bid.IsBest = index == 0;

                        bidVolume = pair.Bid.Volume.Max( bidVolume );
                    }
                    else
                    {
                        bid.Init( null, null, null, null, null );
                    }

                    MarketDepthInfo ask = _itemSource[ GetMarketDepthLevel( Sides.Sell, index ) ];

                    if ( pair.Ask != null )
                    {
                        ask.Init( pair.Ask, _ordersRegistry.GetContainer( pair.Ask.Price ), _stopOrdersRegistry.GetContainer( pair.Ask.Price ), null, null );
                        ask.IsBest = index == 0;
                        askVolume = pair.Ask.Volume.Max( askVolume );
                    }
                    else
                    {
                        ask.Init( null, null, null, null, null );
                    }

                    ++index;
                }

                InitQuotes( index );

                MaxBuyVolume  = bidVolume > Decimal.Zero ? bidVolume : Decimal.One;
                MaxSellVolume = askVolume > Decimal.Zero ? askVolume : Decimal.One;
                MaxVolume     = MaxBuyVolume.Max( MaxSellVolume );
            }
        }

        private void InitQuotes( int int_1 )
        {
            for ( int index = 0; index < MaxDepth - int_1; ++index )
            {
                _itemSource[ GetMarketDepthLevel( Sides.Buy, int_1 + index ) ].Init( null, null, null, null, null );
                _itemSource[ GetMarketDepthLevel( Sides.Sell, int_1 + index ) ].Init( null, null, null, null, null );
            }
        }

        public MarketDepthColumns GetColumnIndex( GridColumn column )
        {
            if ( column == null )
            {
                throw new ArgumentNullException( nameof( column ) );
            }

            return _columnIndecies[ column ];
        }

        protected override void OnPreviewMouseLeftButtonUp( MouseButtonEventArgs mouseEvt )
        {            
            int rowHnd = View.GetRowHandleByMouseEventArgs( mouseEvt );

            if ( rowHnd != int.MinValue )
            {
                GridColumn col = ( GridColumn ) View.GetColumnByMouseEventArgs( mouseEvt );

                if ( col != null )
                {
                    var columnIndex = GetColumnIndex( col );
                    var row         = ( MarketDepthInfo ) GetRow( rowHnd );

                    CellMouseLeftButtonUp?.Invoke( columnIndex, row.Quote, mouseEvt );
                }
            }

            base.OnPreviewMouseLeftButtonUp( mouseEvt );
        }

        protected override void OnPreviewMouseRightButtonUp( MouseButtonEventArgs mouseEvt )
        {            
            int rowHnd = View.GetRowHandleByMouseEventArgs( mouseEvt );

            if ( rowHnd != int.MinValue )
            {
                GridColumn col = ( GridColumn ) View.GetColumnByMouseEventArgs( mouseEvt );

                if ( col != null )
                {
                    var columnIndex = GetColumnIndex( col );
                    var row         = ( MarketDepthInfo ) GetRow( rowHnd );

                    CellMouseRightButtonUp?.Invoke( columnIndex, row.Quote, mouseEvt );
                }
            }
            base.OnPreviewMouseRightButtonUp( mouseEvt );
        }

        private void OnItemDoubleClicked( object sender, ItemDoubleClickEventArgs e )
        {
            var index = GetColumnIndex( e.Column );
            var row   = ( MarketDepthInfo ) e.Row;                       

            CellMouseLeftDoubleClick?.Invoke( index, row.Quote, e );
        }
    }
}
