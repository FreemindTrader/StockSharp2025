using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using StockSharp.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;

internal sealed class MarketDepthQuote : NotifiableObject
{
    private readonly MarketDepthControl _depthControl;
    private Quote _quote;
    private Sides _sides;
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

    public MarketDepthQuote( MarketDepthControl control, Sides side )
    {                
        if ( control == null )
        {
            throw new ArgumentNullException( "depthControl" );
        }

        _depthControl = control;
        Side = side;
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
            return _sides;
        }
        private set
        {
            if ( value == Side )
            {
                return;
            }

            _sides = value;
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

    public void Init( Quote quote = null, OrderRegistry.OrderContainer orders = null, OrderRegistry.OrderContainer stopOrders = null, Quote trades = null, Quote myTrade = null )
    {
        Quote = quote;
        if ( quote == null && trades == null )
        {
            BuyValue    = 0;
            SellValue   = 0;
            VolumeValue = 0;
            Volume      = Buy = Sell = OwnBuy = OwnSell = string.Empty;
            Price       = Board = string.Empty;
            IsBest      = false;
        }
        else
        {
            if ( quote != null )
            {
                VolumeValue          = quote.Volume;
                Volume               = VolumeValue.ToString( DepthControl.VolumeTextFormat );
                BuyValue             = Quote.OrderDirection == Sides.Buy  ? VolumeValue : 0;
                SellValue            = Quote.OrderDirection == Sides.Sell ? VolumeValue : 0;
                Buy                  = Quote.OrderDirection == Sides.Buy  ? Volume      : string.Empty;
                Sell                 = Quote.OrderDirection == Sides.Sell ? Volume      : string.Empty;
                Price                = quote.Price.ToString( DepthControl.PriceTextFormat );

                var buyPosition      = orders     != null ? orders.TotalBuyBalance      : 0;
                var stopBuyPosition  = stopOrders != null ? stopOrders.TotalBuyBalance  : 0;
                var sellPosition     = orders     != null ? orders.TotalSellBalance     : 0;
                var stopSellPosition = stopOrders != null ? stopOrders.TotalSellBalance : 0;

                var ownBuy           = buyPosition + stopBuyPosition;                
                var ownSell          = sellPosition + stopSellPosition;

                OwnBuy               = ownBuy  == 0 ? string.Empty : ( string ) ownBuy.To<string>();
                OwnSell              = ownSell == 0 ? string.Empty : ( string ) ownSell.To<string>( );
                Board                = quote.Security?.Board?.Code;
            }
            if ( trades != null )
            {
                if ( trades.OrderDirection == Sides.Buy )
                {
                    Buy = "[" + ( object ) trades.Volume + "]" + Buy;
                }
                else
                {
                    Sell = "[" + ( object ) trades.Volume + "]" + Sell;
                }
            }
            if ( myTrade == null )
            {
                return;
            }

            if ( myTrade.OrderDirection == Sides.Buy )
            {
                OwnBuy = "[" + ( object ) myTrade.Volume + "]" + OwnBuy;
            }
            else
            {
                OwnSell = "[" + ( object ) myTrade.Volume + "]" + OwnSell;
            }
        }
    }

    public sealed class OrderRegistry
    {
        private readonly SynchronizedDictionary<Decimal, OrderContainer> _orders = new SynchronizedDictionary<Decimal, OrderContainer>();

        public OrderContainer Add( Decimal price )
        {
            return _orders.SafeAdd( price );
        }

        public OrderContainer GetContainer( Decimal price )
        {
            return _orders.TryGetValue( price );
        }


        /// <summary>
        /// The container of orders grouped by price <see cref="Order.Price"/>.
        /// </summary>
        public sealed class OrderContainer
        {
            private readonly SyncObject _lock = new SyncObject();
            private readonly HashSet<Order> _orderSet = new HashSet<Order>();
            private readonly Dictionary<Order, Decimal> _orders = new Dictionary<Order, Decimal>();
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

                private set { ; }
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
                private set {;}
            }

            
            public void RefreshTotals( Order order, Decimal input, OrderStates orderState )
            {
                if ( order == null )
                {
                    throw new ArgumentNullException( "order" );
                }

                lock ( _lock )
                {
                    Decimal num = new Decimal();

                    if ( orderState == OrderStates.Active )
                    {
                        num = input;

                        if ( !_orderSet.Add( order ) )
                        {
                            num -= _orders[ order ];
                        }

                        _orders[ order ] = input;
                    }
                    else if ( orderState == OrderStates.Done )
                    {
                        if ( !_orderSet.Remove( order ) )
                        {
                            return;
                        }

                        num = 0 - ( Decimal ) _orders.GetAndRemove( order );
                    }

                    if ( num == 0 )
                    {
                        return;
                    }

                    if ( order.Direction == Sides.Buy )
                    {
                        _totalBuyBalance += num;
                    }
                    else
                    {
                        _totalSellBalance += num;
                    }
                }
            }

            public IEnumerable<Order> GetOrderBySide( Sides? side )
            {                
                lock ( _lock )
                {
                    var output =  _orderSet.Where( o =>
                                                        {
                                                            if ( ! side.HasValue )
                                                            {
                                                                return true;
                                                            }

                                                            return o.Direction == side.Value;
                                                        } 
                                                 ).ToArray< Order >( );

                    return output;
                }                
            }            
        }
    }
}
