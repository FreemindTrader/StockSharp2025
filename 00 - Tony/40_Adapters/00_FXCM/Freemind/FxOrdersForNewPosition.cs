using System;
using System.Collections.Generic; 
using System.Text;

namespace StockSharp.FxConnectFXCM.Freemind
{
    public enum FxOrderType
    {
        Unknown,

        OpenMarket,             // An open market order opens a position at any currently available market rate.
        Open,                   // An open order opens a position at the specified market rate in case such rate is available on the market.
        OpenLimit,              // An open order opens a position at the specified market rate or at a more favorable rate in case such rate is available on the market.
        OpenRange,              // An open range order opens a position at the available market rate in case this rate is in the range specified in the command.

        CloseMarket,            //A close market order closes a position at any currently available market rate. Please note that close orders must be permitted for the account in order to be used.
        Close,                  // A close order closes a position at the specified market rate in case such rate is available on the market.
        CloseLimit,             // A close order closes a position at the specified market rate or at a more favorable price in case such rate is available on the market.
        CloseRange,             // A close range order closes a position at the available market rate in case this rate is in the range specified in the command.

        EntryStop,              // A regular entry order opens a position when the specified market condition is met.
                                // A netting entry order closes all positions for the specified instrument and account which are in the direction (buy or sell) opposite to the direction of the entry order.
                                // A stop entry order with a sell direction is filled when the market is below the rate specified in the order.
                                // A stop entry order with a buy direction is filled when the market is above the rate specified in the order.

        EntryLimit,             // A regular entry order opens a position when the specified market condition is met.
                                // A netting entry order closes all positions for the specified instrument and account which are in the direction (buy or sell) opposite to the direction of the entry order.
                                // A limit entry order with a sell direction is filled when the market is above the rate specified in the order.
                                // A limit entry order with a buy direction is filled when the market is below the rate specified in the order.

        Entry,
        //A regular entry order opens a position when the specified market condition is met.
        //Please note that if hedging is disabled for the account, the order, first, closes existing opposite positions for the same account and instrument and only then opens a new position for the remaining amount.
        //A netting entry order closes all positions for the specified instrument and account which are in the direction (buy or sell) opposite to the direction of the entry order.
        //There are two types of Entry orders : Limit Entry and Stop Entry. This command allows you to create an entry order without specifying order type. The system will determine order type automatically, based on three parameters:
        //      Order direction (Buy or Sell).
        //      Desired order rate.
        //      Current market price of a trading instrument.

        //The system will create a Limit Entry order if:
        //      Rate for a buy order is below current market price.
        //      Rate for a sell order is above current market price.

        //The system will create a Stop Entry order if:
        //      Rate for a buy order is above current market price.
        //      Rate for a sell order is below current market price.
        //

        Limit,                  //  A limit order is used for locking in profit of the existing position when the market condition is met.
                                //  Limit orders can be created for existing trades as well as for existing entry orders. Limit orders created for entry orders remain inactive until the trade is created by the entry order.
                                //  Only one limit order can be attached to a position or an entry order.
                                //  Please note that close orders must be permitted for the account in order to be used.
                                //  Please note that stop and limit orders cannot be attached to netting entry orders.


        Stop,                   //  A stop order is used for limiting losses of the existing position when the market condition is met.
                                //  Stop orders can be created for existing trades as well as for existing entry orders. Stop orders created for entry orders remain inactive until the trade is created by the entry order.
                                //  Only one stop order can be attached to a position or an the entry order.
                                //  Please note that close orders must be permitted for the account in order to be used.
                                //  Please note that stop and limit orders cannot be attached to netting entry orders.

        TrailingStop,
        TrailingEntryStop,
        TrailingEntryLimit,
        MarginCall,
        SafetyNet,
        SpecificOrder,
        All
    }

    public enum SymbolsEnum
    {
        NA,
        EURUSD,
        CHFJPY,
        GBPCHF,
        EURAUD,
        EURCAD,
        AUDCAD,
        CADJPY,
        NZDJPY,
        GBPCAD,
        AUDNZD,
        USDSEK,
        USDDDK,
        EURSEK,
        EURNOK,
        USDNOK,
        USDMXN,
        AUDCHF,
        EURNZD,
        EURPLN,
        USDPLN,
        EURCZK,
        USDCZK,
        USDZAR,
        USDSGD,
        USDHKD,
        EURDKK,
        GBPSEK,
        NOKJPY,
        SEKJPY,
        SGDJPY,
        HKDJPY,
        ZARJPY,
        USDTRY,
        EURTRY,
        NZDCHF,
        CADCHF,
        NZDCAD,
        CHFSEK,
        CHFNOK,
        EURHUF,
        USDHUF,
        TRYJPY,
        GBPUSD,
        USDCNH,
        EURJPY,
        USDJPY,
        GBPJPY,
        AUDJPY,
        USDCHF,
        AUDUSD,
        EURCHF,
        EURGBP,
        NZDUSD,
        USDCAD,
        GBPAUD,
        XAGUSD,
        XAUUSD,
        UK100,
        USDOLLAR,
        GER30,
        FRA40,
        AUS200,
        ESP35,
        HKG33,
        ITA40,
        JPN225,
        NAS100,
        SPX500,
        SUI20,
        COPPER,
        EUSTX50,
        US30,
        USOIL,
        UKOIL,
        NGAS,
        XPDUSD,
        XPTUSD,
        BUND,
        USDILS,
        CHN50,
        SOYF,
        US2000,
        WHEATF,
        CORNF,
        BTCUSD,
        ETHUSD,
        LTCUSD
    }

    public interface ISymbol
    {
        string Source { get; set; }
        string SymbolString { get; set; }
        SymbolGroup SymbolGroup { get; set; }
        bool IsForexPair { get; set; }
        string FirstOfCross { get; set; }

        SymbolsEnum SymbolEnum { get; set; }
        string SecondOfCross { get; set; }

        void FixSymbol( );
    }

    public interface IOrdersForNewPosition
    {
        // Once a once is formed, the following will not change
        string AccountId { get; set; }
        string AccountName { get; set; }
        string OrderId { get; set; }
        FxOrderType OrderType { get; set; }
        ISymbol Symbol { get; set; }

        // The following properties will change according to the system or the 
        string Status { get; set; }
        int Amount { get; set; }
        double? SellPrice { get; set; }
        double? BuyPrice { get; set; }
        double? StopPrice { get; set; }
        double? LimitPrice { get; set; }
        DateTime OrderTime { get; set; }
        DateTime ExpireDate { get; set; }
        string Comment { get; set; }
        IDetailedOrder ThisOrder { get; set; }
        IDetailedOrder StopOrder { get; set; }
        IDetailedOrder LimitOrder { get; set; }

        string TradeId { get; set; }

        IOrdersForNewPosition Clone( );
    }



public class FxOrdersForNewPosition : IOrdersForNewPosition, IEquatable<IOrdersForNewPosition>
    {
        #region IOffers members

        private DetailedOrderDB _stopOrder;
        private DetailedOrderDB _limitOrder;
        private DetailedOrderDB _thisOrder;

        // Fields...
        private string         _comment;
        private DateTime       _expireDate;
        private DateTime       _orderTime;
        //private double?        _limitPrice;
        //private double?        _stopPrice;
        private double?        _buyPrice;
        private double?        _sellPrice;
        private int            _amount;
        private ISymbol        _symbol;
        private string         _status;
        private FxOrderType    _orderType;
        private string         _accountName;
        private string         _mainLoginName;
        private string         _accountId;
        private string         _orderId;

        
        public string TradeId { get; set; }

        
        public string OrderId
        {
            get { return _orderId; }
            set
            {
                if ( _orderId == value )
                    return;
                _orderId = value;

                

            }
        }


        public string AccountId
        {
            get { return _accountId; }
            set
            {
                if ( _accountId == value )
                    return;
                _accountId = value;

                
            }
        }


        public string AccountName
        {
            get { return _accountName; }
            set
            {
                if ( _accountName == value )
                    return;
                _accountName = value;

                
            }
        }

        public string MainLoginName
        {
            get { return _mainLoginName; }
            set
            {
                if ( _mainLoginName == value )
                    return;
                _mainLoginName = value;

                
            }
        }


        public FxOrderType OrderType
        {
            get { return _orderType; }
            set
            {
                if ( _orderType == value )
                    return;
                _orderType = value;

                
            }
        }


        public string Status
        {
            get { return _status; }
            set
            {
                if ( _status == value )
                    return;
                _status = value;

                

            }
        }


        public ISymbol Symbol
        {
            get { return _symbol; }
            set
            {
                if ( _symbol == value )
                    return;
                _symbol = value;

                
            }
        }


        public string SymbolString
        {
            get
            {
                return _symbol.SymbolString;
            }
        }

        public bool isBuy( )
        {
            return BuyPrice.HasValue && BuyPrice.Value > 0;
        }

        public bool isSell( )
        {
            return SellPrice.HasValue && SellPrice.Value > 0;
        }



        public int Amount
        {
            get { return _amount; }
            set
            {
                if ( _amount == value )
                    return;
                _amount = value;

                

            }
        }


        public double? SellPrice
        {
            get { return _sellPrice; }
            set
            {
                if ( _sellPrice == value )
                    return;
                _sellPrice = value;

                
            }
        }


        public double? BuyPrice
        {
            get { return _buyPrice; }
            set
            {
                if ( _buyPrice == value )
                    return;
                _buyPrice = value;

                

            }
        }


        public double? StopPrice
        {
            get
            {
                return StopOrder?.Rate;
            }

            set
            {
                if ( StopOrder != null )
                {
                    if ( value.HasValue )
                    {
                        StopOrder.Rate = value.Value;

                        


                    }
                }
            }


        }

        public void RaiseStopLimitChanged( )
        {
            
        }

        public double? LimitPrice
        {
            get
            {
                return LimitOrder?.Rate;
            }

            set
            {
                if ( LimitOrder != null )
                {
                    if ( value.HasValue )
                    {
                        LimitOrder.Rate = value.Value;

                        


                    }
                }
            }
        }


        public DateTime OrderTime
        {
            get { return _orderTime; }
            set
            {
                if ( _orderTime == value )
                    return;
                _orderTime = value;

                

            }
        }


        public DateTime ExpireDate
        {
            get { return _expireDate; }
            set
            {
                if ( _expireDate == value )
                    return;
                _expireDate = value;

                


            }
        }


        public string Comment
        {
            get { return _comment; }
            set
            {
                if ( _comment == value )
                    return;
                _comment = value;

                


            }
        }

        public IDetailedOrder ThisOrder
        {
            get { return _thisOrder; }
            set
            {
                _thisOrder = ( DetailedOrderDB ) value;

                

            }
        }


        public IDetailedOrder LimitOrder
        {
            get { return _limitOrder; }
            set
            {
                _limitOrder = ( DetailedOrderDB ) value;

                


            }
        }


        public IDetailedOrder StopOrder
        {
            get { return _stopOrder; }
            set
            {
                _stopOrder = ( DetailedOrderDB ) value;

                


            }
        }



        

        public FxOrdersForNewPosition( string orderId, string accountId, string mainLoginName, string accountName, FxOrderType orderType, string status, ISymbol symbol, int amount, double? sellPrice, double? buyPrice, double? stopPrice, double? limitPrice, DateTime orderTime, DateTime expireDate, string comment, string tradeid, IDetailedOrder stopOrder, IDetailedOrder limitOrder )
        {
            OrderId = orderId;
            AccountId = accountId;
            MainLoginName = mainLoginName;
            AccountName = accountName;
            OrderType = orderType;
            Status = status;
            Symbol = symbol;
            Amount = amount;
            SellPrice = sellPrice;
            BuyPrice = buyPrice;
            StopPrice = stopPrice;
            LimitPrice = limitPrice;
            OrderTime = orderTime;
            ExpireDate = expireDate;
            Comment = comment;
            TradeId = tradeid;
            StopOrder = stopOrder;
            LimitOrder = limitOrder;
        }

        public IOrdersForNewPosition Clone( )
        {
            return new FxOrdersForNewPosition( OrderId, AccountId, MainLoginName, AccountName, OrderType, Status, Symbol, Amount, SellPrice, BuyPrice, StopPrice.Value, LimitPrice.Value, OrderTime, ExpireDate, Comment, TradeId, StopOrder, LimitOrder );
        }


        public bool Equals( IOrdersForNewPosition other )
        {
            return string.Equals( OrderId, other.OrderId ) && string.Equals( AccountId, other.AccountId ) && string.Equals( AccountName, other.AccountName ) && OrderType == other.OrderType && string.Equals( Status, other.Status ) && Equals( Symbol, other.Symbol ) && Amount == other.Amount && SellPrice.Equals( other.SellPrice ) && BuyPrice.Equals( other.BuyPrice ) && StopPrice.Equals( other.StopPrice ) && LimitPrice.Equals( other.LimitPrice ) && OrderTime.Equals( other.OrderTime ) && ExpireDate.Equals( other.ExpireDate ) && string.Equals( Comment, other.Comment );
        }

        public override bool Equals( object obj )
        {
            if ( ReferenceEquals( null, obj ) ) return false;
            if ( ReferenceEquals( this, obj ) ) return true;
            if ( obj.GetType( ) != GetType( ) ) return false;
            return Equals( ( FxOrdersForNewPosition ) obj );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                var hashCode = ( OrderId != null ? OrderId.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ ( AccountId != null ? AccountId.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ ( AccountName != null ? AccountName.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ ( int ) OrderType;
                hashCode = ( hashCode * 397 ) ^ ( Status != null ? Status.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ ( Symbol != null ? Symbol.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ Amount;
                hashCode = ( hashCode * 397 ) ^ SellPrice.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ BuyPrice.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ StopPrice.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ LimitPrice.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ OrderTime.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ ExpireDate.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ ( Comment != null ? Comment.GetHashCode( ) : 0 );
                return hashCode;
            }
        }

        #endregion        
    }
}

