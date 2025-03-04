
using Ecng.Collections;
using System;
using System.Collections.Generic; 
using System.Text;

namespace StockSharp.FxConnectFXCM.Freemind
{    
    public interface IOffer
    {
        /// <summary>
        /// Gets the instrument name
        /// </summary>
        string Instrument { get; }

        /// <summary>
        /// Gets the date/time (in UTC time zone) when the offer was updated the last time
        /// </summary>
        DateTime LastUpdate { get; }

        /// <summary>
        /// Gets the latest offer bid price
        /// </summary>
        double Bid { get; }

        /// <summary>
        /// Gets the latest offer ask price
        /// </summary>
        double Ask { get; }

        /// <summary>
        /// Gets the offer accumulated last minute volume
        /// </summary>
        int MinuteVolume { get; }

        /// <summary>
        /// Gets the number of significant digits after decimal point
        /// </summary>
        int Digits { get; }

        /// <summary>
        /// Makes a copy of the offer
        /// </summary>
        /// <returns></returns>
        IOffer Clone( );

    }

    public struct FxBidAsk
    {
        public double Ask;
        public double Bid;

        public FxBidAsk( double bid, double ask )
        {
            Ask = ask;
            Bid = bid;
        }

        public double Price
        {
            get
            {
                return ( Ask + Bid ) / 2;
            }
        }

        public double Spread
        {
            get
            {
                return Math.Abs( Ask - Bid );
            }
        }
    }
    public enum OverLeverageStatus
    {
        INVALID = 0,
        PROCESSING = 1,
        PROCESSED = 2,
        WARNING = 80,
        DANGEROUS = 70,
        CRITICAL = 60,
        IMMEDIATE = 50
    }

    public enum ChangeOrderType
    {
        INVALID,
        NEW,
        MODIFIED,
        CANCELLED,
        CLOSED
    }

    public enum PriceLevelsType
    {
        NO_CHANGE = 0,
        TAKE_PROFIT = 1,
        STOP_LOSS = 2,
        SAFETY_NET = 3

    }

    public enum FxOrderSide
    {
        INVALID,
        Buy,
        Sell,
        Both,
        LongSafetyNet,
        ShortSafetyNet
    }

    
    public static class MyTimeUnit
    {
        // NOTE: currently not supported
        //public const string Nanoseconds = "n";

        // NOTE: currently not supported
        //public const string Microseconds = "u";

        public const string Milliseconds = "ms";

        public const string Seconds = "s";

        public const string Minutes = "m";

        public const string Hours = "h";
    }
    public static class TonyHelper
    {
        private static DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static long ToLinuxTime( this DateTime date, string precision = MyTimeUnit.Milliseconds )
        {
            var span = date - _epoch;
            double fractionalSpan;

            switch ( precision )
            {
                case MyTimeUnit.Milliseconds:
                    fractionalSpan = span.TotalMilliseconds;
                    break;
                case MyTimeUnit.Seconds:
                    fractionalSpan = span.TotalSeconds;
                    break;
                case MyTimeUnit.Minutes:
                    fractionalSpan = span.TotalMinutes;
                    break;
                case MyTimeUnit.Hours:
                    fractionalSpan = span.TotalHours;
                    break;
                default:
                    fractionalSpan = span.TotalMilliseconds;
                    break;
            }

            return Convert.ToInt64( fractionalSpan );
        }
        public static DateTime FromLinuxTime( this long unixTime, string precision = MyTimeUnit.Milliseconds )
        {
            switch ( precision )
            {
                case MyTimeUnit.Milliseconds:
                    return _epoch.AddMilliseconds( unixTime );
                case MyTimeUnit.Seconds:
                    return _epoch.AddSeconds( unixTime );
                case MyTimeUnit.Minutes:
                    return _epoch.AddMinutes( unixTime );
                case MyTimeUnit.Hours:
                    return _epoch.AddHours( unixTime );
                default:
                    return _epoch.AddMilliseconds( unixTime );
            }
        }

        public static SymbolGroup FindGroupBySymbol( string symbolName )
        {
            SymbolGroup symbolGroup = SymbolGroup.NA;

            symbolName = symbolName.ToUpper( );

            switch ( symbolName )
            {
                case "EUR/USD":
                case "CHF/JPY":
                case "GBP/CHF":
                case "EUR/AUD":
                case "EUR/CAD":
                case "AUD/CAD":
                case "CAD/JPY":
                case "NZD/JPY":
                case "GBP/CAD":
                case "AUD/NZD":
                case "USD/SEK":
                case "USD/DDK":
                case "EUR/SEK":
                case "EUR/NOK":
                case "USD/NOK":
                case "USD/MXN":
                case "AUD/CHF":
                case "EUR/NZD":
                case "EUR/PLN":
                case "USD/PLN":
                case "EUR/CZK":
                case "USD/CZK":
                case "USD/ZAR":
                case "USD/SGD":
                case "USD/HKD":
                case "EUR/DKK":
                case "GBP/SEK":
                case "NOK/JPY":
                case "SEK/JPY":
                case "SGD/JPY":
                case "HKD/JPY":
                case "ZAR/JPY":
                case "USD/TRY":
                case "EUR/TRY":
                case "NZD/CHF":
                case "CAD/CHF":
                case "NZD/CAD":
                case "CHF/SEK":
                case "CHF/NOK":
                case "EUR/HUF":
                case "USD/HUF":
                case "TRY/JPY":
                case "GBP/USD":
                case "USD/CNH":
                case "EUR/JPY":
                case "USD/JPY":
                case "GBP/JPY":
                case "AUD/JPY":
                case "USD/CHF":
                case "AUD/USD":
                case "EUR/CHF":
                case "EUR/GBP":
                case "NZD/USD":
                case "USD/CAD":
                case "GBP/AUD":
                    symbolGroup = SymbolGroup.Forex;
                    break;

                case "XAG/USD":
                case "XAU/USD":
                case "XPD/USD":
                case "XPT/USD":
                    symbolGroup = SymbolGroup.Bullion;
                    break;

                case "UK100":
                case "GER30":
                case "FRA40":
                case "AUS200":
                case "ESP35":
                case "HKG33":
                case "ITA40":
                case "JPN225":
                case "NAS100":
                case "SPX500":
                case "SUI20":
                case "COPPER":
                case "EUSTX50":
                case "US30":
                case "CHN50":
                    symbolGroup = SymbolGroup.Indices;
                    break;

                case "USDOLLAR":
                    symbolGroup = SymbolGroup.FXIndex;
                    break;

                case "USOIL":
                case "UKOIL":
                case "NGAS":
                case "SOYF":
                case "WHEATF":
                case "CORNF":
                    symbolGroup = SymbolGroup.Commodity;
                    break;

                case "BUND":
                    symbolGroup = SymbolGroup.Treasury;
                    break;

                case "BTC/USD":
                case "ETH/USD":
                case "LTC/USD":
                    symbolGroup = SymbolGroup.Crypto;
                    break;

                default:
                    symbolGroup = SymbolGroup.NA;
                    break;
            }

            return symbolGroup;
        }
    }


    public class FxThreadLocalData
    {
        public double Total { get; set; }

        public string Symbol { get; set; }
    }
    public interface IFxcm
    {
        long Id { get; set; }
        long StartDate { get; set; }

    }
    public enum SymbolGroup
    {
        NA        = 0,
        Forex     = 1,
        Indices   = 2,
        Commodity = 3,
        Treasury  = 4,
        Bullion   = 5,
        Shares    = 6,
        FXIndex   = 7,
        Crypto = 8
    }
    public interface IOpenPositionAndOrders
    {
        string AccountID { get; set; }
        string AccountName { get; set; }
        string MainLoginName { get; set; }
        int Amount { get; set; }
        string BuySell { get; set; }
        string Comment { get; set; }
        double? ClosePrice { get; set; }
        double Commission { get; set; }
        double GrossProfit { get; set; }
        double? LimitPrice { get; set; }
        double OpenPrice { get; set; }
        DateTime OpenTime { get; set; }
        double PipProfit { get; set; }
        double RolloverInterest { get; set; }
        int? StopMove { get; set; }
        double? StopPrice { get; set; }
        string Symbol { get; set; }
        string Ticket { get; set; }
        double UsedMargin { get; set; }
        bool IsBuy { get; }

        SymbolGroup SymbolGroup { get; }

        IDetailedOrder ThisOrder { get; set; }
        IDetailedOrder StopOrder { get; set; }
        IDetailedOrder LimitOrder { get; set; }        

        IPositionOrderCalculatedValue CalculatedValue { get; set; }

        string GetPositionType( );
        IOpenPositionAndOrders Clone( );

        bool Equals( IDetailedPosition other );
    }


    public interface IPositionOrderCalculatedValue
    {
        double? ClosePrice { get; set; }  //The current profit/loss of all short (sell) positions. It is expressed in the account currency. It does not include commissions and interests. If no short positions exist for the instrument, the value of this field is 0.0.
        double GrossProfit { get; set; }  // The current profit/loss of all positions (both long and short). It is expressed in the account currency. It does not include commissions and interests. 
        double PipProfit { get; set; }  // The current profit/loss of all positions (both long and short). It is expressed in the account currency. It includes commissions and interests.     
        void SetParent( IOpenPositionAndOrders parent );

    }

    public interface IDetailedOrder : IFxcm
    {

        string AccountID { get; set; }
        string AccountKind { get; set; }
        string MainLoginName { get; set; }
        string AccountName { get; set; }
        int Amount { get; set; }
        double AtMarket { get; set; }
        string BuySell { get; set; }
        int ContingencyType { get; set; }
        string ContingentOrderID { get; set; }
        double ExecutionRate { get; set; }
        long ExpireDate { get; set; }
        DateTime ExpireDateDT { get; set; }
        int FilledAmount { get; set; }
        bool NetQuantity { get; set; }
        string OfferID { get; set; }
        string OrderID { get; set; }
        int OriginAmount { get; set; }
        string Parties { get; set; }
        double PegOffset { get; set; }
        string PegType { get; set; }
        string PrimaryID { get; set; }
        double Rate { get; set; }
        double RateMax { get; set; }
        double RateMin { get; set; }
        string RequestID { get; set; }
        string RequestTXT { get; set; }
        string Stage { get; set; }
        string Status { get; set; }
        DateTime StatusTimeDT { get; set; }
        long StatusTime { get; set; }
        string TimeInForce { get; set; }
        string TradeID { get; set; }
        double TrailRate { get; set; }
        int TrailStep { get; set; }
        string Type { get; set; }
        string ValueDate { get; set; }
        bool WorkingIndicator { get; set; }

        void CopyFrom( IDetailedOrder other );

        bool isMarketOrder( );

        IDetailedOrder Clone( );
    }

    public interface IDetailedAccount
    {
        string AccountID { get; set; }

        string AccountKind { get; set; }
        string AccountName { get; set; }

        string AccountType { get; set; }

        int AmountLimit { get; set; }

        double Balance { get; set; }

        int BaseUnitSize { get; set; }

        DateTime LastMarginCallDate { get; set; }

        string LeverageProfileID { get; set; }

        int MarginLeverage { get; set; }

        double M2MEquity { get; set; }

        bool MaintenanceFlag { get; set; }
        string MaintenanceType { get; set; }

        string ManagerAccountID { get; set; }

        string MarginCallFlag { get; set; }

        double NonTradeEquity { get; set; }

        double UsedMargin { get; set; }

        double UsedMargin3 { get; set; }

        int SubAccountType { get; set; }

        string AccountCurrency { get; set; }

        void CopyFrom( IDetailedAccount other );
        IDetailedAccount Clone( );
    }

    public class EnumeratorHelper<T, I> : IEnumerator<I> where T : I
    {
        private IEnumerator< T > mEnumerator;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="enumerator">Enumerator of class instances</param>
        public EnumeratorHelper( IEnumerator<T> enumerator )
        {
            mEnumerator = enumerator;
        }

        /// <summary>
        /// Gets the current element of enumeration (typped).
        /// </summary>
        public I Current
        {
            get { return mEnumerator.Current; }
        }

        /// <summary>
        /// Gets the current element of the enumeration (as an object).
        /// </summary>
        object System.Collections.IEnumerator.Current
        {
            get { return mEnumerator.Current; }
        }

        /// <summary>
        /// Disposes the enumerator.
        /// </summary>
        public void Dispose( )
        {
            mEnumerator.Dispose( );
        }

        /// <summary>
        /// Moves to the next element.
        /// </summary>
        /// <returns>false if there is no more elements</returns>
        public bool MoveNext( )
        {
            return mEnumerator.MoveNext( );
        }

        /// <summary>
        /// Resets the enumerator into its initial state.
        /// </summary>
        public void Reset( )
        {
            mEnumerator.Reset( );
        }
    }

    public interface IDetailedAccountsCollection : IEnumerable<IDetailedAccount>
    {
        int Count { get; }        // Get number of the offers in the collection        
        //IDetailedAccount this[ int index ] { get; }        // Gets the offer by its index
        IDetailedAccount FindAccountById( string accountId );
    }

    public interface ISubaccountTradeDataRepo
    {
        void AddDetailedAccountsCollection( FxDetailedAccountsCollection accountInfo );
        void AddPosition( IDetailedPosition fxPosition );
        void AddOrder( DetailedOrderDB fxOrder );

        void TryUpdatePosition( IDetailedPosition fxPosition );
        void TryUpdateOrder( IDetailedOrder fxOrder );


        void AddPosition(
                                    string symbolName,
                                    string mainLoginName,
                                    string accountId,
                                    string accountKind,
                                    string accountName,
                                    int amount,
                                    string buySell,
                                    double commission,
                                    string offerId,
                                    string openOrderId,
                                    string openOrderParties,
                                    string openOrderReqId,
                                    string openOrderRequestTxt,
                                    string openQuoteId,
                                    double openRate,
                                    DateTime openTime,
                                    double rolloverInterest,
                                    string tradeId,
                                    string tradeIdOrigin,
                                    string valueDate,
                                    double dividend
                        );

        void AddOrder( string accountId, string accountKind, string mainLoginName, string accountName, int amount, double atMarket, string buySell, int contingencyType, string contingentOrderId, double executionRate, long expireDate, int filledAmount, bool netQuantity, string offerId, string orderId, int originAmount, string parties, double pegOffset, string pegType, string primaryId, double rate, double rateMax, double rateMin, string requestId, string requestTxt, string stage, string status, long statusTime, string timeInForce, string tradeId, double trailRate, int trailStep, string type, string valueDate, bool workingIndicator );




        void TryUpdatePosition(
                                    string symbolName,
                                    string mainLoginName,
                                    string accountId,
                                    string accountKind,
                                    string accountName,
                                    int amount,
                                    string buySell,
                                    double commission,
                                    string offerId,
                                    string openOrderId,
                                    string openOrderParties,
                                    string openOrderReqId,
                                    string openOrderRequestTxt,
                                    string openQuoteId,
                                    double openRate,
                                    DateTime openTime,
                                    double rolloverInterest,
                                    string tradeId,
                                    string tradeIdOrigin,
                                    string valueDate,
                                    double dividend
                       );

        void TryUpdateOrder( string accountId,
                                    string accountKind,
                                    string mainLoginName,
                                    string accountName,
                                    int amount,
                                    double atMarket,
                                    string buySell,
                                    int contingencyType,
                                    string contingentOrderId,
                                    double executionRate,
                                    long expireDate,
                                    int filledAmount,
                                    bool netQuantity,
                                    string offerId,
                                    string orderId,
                                    int originAmount,
                                    string parties,
                                    double pegOffset,
                                    string pegType,
                                    string primaryId,
                                    double rate,
                                    double rateMax,
                                    double rateMin,
                                    string requestId,
                                    string requestTxt,
                                    string stage,
                                    string status,
                                    long statusTime,
                                    string timeInForce,
                                    string tradeId,
                                    double trailRate,
                                    int trailStep,
                                    string type,
                                    string valueDate,
                                    bool workingIndicator );

        FxPositionsSummary RemovePosition( string accountName, string accountId, string instrument, string tradeId );        
        void RemoveOrder( string instrument, string orderid );

        void DetailedOrdersToExistingPositionsOrderOrNewPositionOrders( );

        int OrdersCount( );
        bool PositionsHaveSymbol( string instrument );
        List<IDetailedOrder> LimitedOrdersList { get; }
    }

    public interface ITrade
    {
        string MainLoginName { get; }
        string AccountID { get; }
        string AccountKind { get; }
        string AccountName { get; }
        int Amount { get; }
        string BuySell { get; }

        bool IsBuy { get; }

        double Commission { get; }

        string OfferID { get; }

        string OpenOrderID { get; }

        string OpenOrderParties { get; }

        string OpenOrderReqID { get; }

        string OpenOrderRequestTXT { get; }

        string OpenQuoteID { get; }

        double OpenRate { get; }
        DateTime OpenTime { get; }

        double RolloverInterest { get; }

        string TradeID { get; }

        string TradeIDOrigin { get; }
    }

    public interface IDetailedPosition : ITrade
    {
        string ValueDate { get; }
        double Dividends { get; }
        IDetailedPosition Clone( );
    }

    public class ItemEventArgs<T> : EventArgs
    {
        public ItemEventArgs( T item )
        {
            Item = item;
        }

        public T Item { get; protected set; }

        public static implicit operator ItemEventArgs<T>( T item )
        {
            return new ItemEventArgs<T>( item );
        }
    }

    public delegate void ItemEventHandler<T>( object sender, ItemEventArgs<T> e );

    public class OpenPositionsPl
    {
        public string AccountName { get; set; }
        public double ProfitLoss { get; set; }

        private SynchronizedDictionary<string, double> _symbolToNetBuyProfit = new SynchronizedDictionary< string, double >();
        private SynchronizedDictionary<string, double> _symbolToNetSellProfit = new SynchronizedDictionary< string, double >();

        public SynchronizedDictionary<string, double> SymbolToNetBuyProfit
        {
            get { return _symbolToNetBuyProfit; }

            set { _symbolToNetBuyProfit = value; }
        }

        public SynchronizedDictionary<string, double> SymbolToNetSellProfit
        {
            get { return _symbolToNetSellProfit; }

            set { _symbolToNetSellProfit = value; }
        }
    }

    public interface IPositionCalculatedValue
    {
        double? SellNetPl { get; set; }  //The current profit/loss of all short (sell) positions. It is expressed in the account currency. It does not include commissions and interests. If no short positions exist for the instrument, the value of this field is 0.0.
        double? SellClose { get; set; }  //The current market price, at which long (buy) positions can be closed. In the case of FX instruments, it is expressed in the instrument counter currency per one unit of base currency. In the case of CFD instruments, it is expressed in the instrument native currency per one contract. If no long positions exist for the instrument, the value of this field is 0.0.
        double? BuyNetPl { get; set; }  //The current profit/loss of all long (buy) positions. It is expressed in the account currency. It does not include commissions and interests. If no long positions exist for the instrument, the value of this field is 0.0.
        double? BuyClose { get; set; }  // The current market price, at which short (sell) positions can be closed. In the case of FX instruments, it is expressed in the instrument counter currency per one unit of base currency. In the case of CFD instruments, it is expressed in the instrument native currency per one contract. If no short positions exist for the instrument, the value of this field is 0.0.        
        double GrossPl { get; set; }  // The current profit/loss of all positions (both long and short). It is expressed in the account currency. It does not include commissions and interests. 
        double NetPl { get; set; }  // The current profit/loss of all positions (both long and short). It is expressed in the account currency. It includes commissions and interests.

        int? PendingSellVolume { get; set; }

        int? PendingBuyVolume { get; set; }

        double? Basis { get; set; }

        double? MarketValue { get; set; }

        void SetParent( IPositionsSummary parent );

    }

    public interface IPositionsSummary : IPositionCalculatedValue
    {
        string MainLoginName { get; set; }  // The unique identification number of the instrument traded.
        string AccountName { get; set; }  // The unique identification number of the instrument traded.
        string OfferId { get; set; }  // The unique identification number of the instrument traded.
        int DefaultSortOrder { get; set; }  // The sequence number of the instrument. It defines the instrument place in the dealing rates list of the FX Trading Station.
        string Symbol { get; set; }  //The symbol of the instrument traded. For example, EUR/USD, USD/JPY, GBP/USD.        
        double? SellAvgOpen { get; set; }  //The average open price of short (sell) positions. In the case of FX instruments, it is expressed in the instrument counter currency per one unit of base currency. In the case of CFD instruments, it is expressed in the instrument native currency per one contract. If no short positions exist for the instrument, the value of this field is 0.0.

        double? SellAmount { get; set; }  //The amount of short (sell) positions. In the case of FX instruments, it is expressed in the instrument base currency. In the case of CFD instruments, it is expressed in contracts. If short positions exist for the instrument traded, the value of this field is positive. Otherwise, the value of this field is 0.0.

        double? BuyAmount { get; set; }  //The amount of long (buy) positions. In the case of FX instruments, it is expressed in the instrument base currency. In the case of CFD instruments, it is expressed in contracts. If long positions exist for the instrument traded, the value of this field is positive. Otherwise, the value of this field is 0.0.         

        double? BuyAvgOpen { get; set; }  //The average open price of long (buy) positions. In the case of FX instruments, it is expressed in the instrument counter currency per one unit of base currency. In the case of CFD instruments, it is expressed in the instrument native currency per one contract. If no long positions exist for the instrument, the value of this field is 0.0.

        double Amount { get; set; }  //The amount of all positions (both long and short). In the case of FX instruments, it is expressed in the instrument base currency. In the case of CFD instruments, it is expressed in contracts. If the amount of long positions exceeds the amount of short positions for the instrument traded, the value of this field is positive. If the amount of short positions exceeds the amount of long positions for the instrument traded, the value of this field is negative.


        // -------------------- The following are calcuated values ----------------------------------------

        double? Result { get; set; }

        double? Commission { get; set; }

        IPositionCalculatedValue CalculatedValue { get; set; }      

        void CopyFrom( IPositionsSummary other );

        void AddDetailPositionInfos( List<IOpenPositionAndOrders> detailInfos );

        void AddDetailPositionInfos( IOpenPositionAndOrders detailPendingOrdersInfo );

        IList<IOpenPositionAndOrders> DetailPositionInfos { get; }
    }
}
