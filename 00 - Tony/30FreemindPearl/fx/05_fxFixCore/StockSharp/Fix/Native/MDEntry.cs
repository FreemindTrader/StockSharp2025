using Ecng.Common;
using StockSharp.Messages;
using System;

namespace StockSharp.Fix.Native
{
    /// <summary>The market data string.</summary>
    public struct MDEntry
    {

        private char _type;

        private DateTimeOffset _time;

        private decimal? _price;

        private decimal? _size;

        private char? _action;

        private int? _position;

        private int? _positionExtra;

        private string _entryId;

        private char? _tickDirection;

        private StockSharp.Messages.SecurityId? _securityId;

        private string _otherValue;

        private CurrencyTypes? _currency;

        private decimal? _numberOfOrders;

        private decimal? _openPrice;

        private decimal? _openSize;

        private decimal? _highPrice;

        private decimal? _highSize;

        private decimal? _lowPrice;

        private decimal? _lowSize;

        private decimal? _closePrice;

        private decimal? _closeSize;

        private CandleStates? _candleState;

        private string _quoteCondition;

        private char? _originator;

        private int? _upTicks;

        private int? _downTicks;

        private int? _totalTicks;

        private CandlePriceLevel[] _priceLevels;

        private decimal? _yield;

        private string _buyer;

        private string _seller;

        /// <summary>
        /// Data type. Available types <see cref="T:StockSharp.Fix.Native.MDEntryType" />.
        /// </summary>
        public char Type
        {
            readonly get => _type;
            set => _type = value;
        }

        /// <summary>Time.</summary>
        public DateTimeOffset Time
        {
            readonly get => _time;
            set => _time = value;
        }

        /// <summary>Price.</summary>
        public decimal? Price
        {
            readonly get => _price;
            set => _price = value;
        }

        /// <summary>Volume.</summary>
        public decimal? Size
        {
            readonly get => _size;
            set => _size = value;
        }

        /// <summary>
        /// Action. Available types <see cref="T:StockSharp.Fix.Native.MDUpdateAction" />.
        /// </summary>
        public char? Action
        {
            readonly get => _action;
            set => _action = value;
        }

        /// <summary>Position.</summary>
        public int? Position
        {
            readonly get => _position;
            set => _position = value;
        }

        /// <summary>Position (extra).</summary>
        public int? PositionExtra
        {
            readonly get => _positionExtra;
            set => _positionExtra = value;
        }

        /// <summary>Identifier.</summary>
        public string EntryId
        {
            readonly get => _entryId;
            set => _entryId = value;
        }

        /// <summary>The tick direction.</summary>
        public char? TickDirection
        {
            readonly get => _tickDirection;
            set => _tickDirection = value;
        }

        /// <summary>Security ID.</summary>
        public StockSharp.Messages.SecurityId? SecurityId
        {
            readonly get => _securityId;
            set => _securityId = value;
        }

        /// <summary>The additional string value.</summary>
        public string OtherValue
        {
            readonly get => _otherValue;
            set => _otherValue = value;
        }

        /// <summary>Currency.</summary>
        public CurrencyTypes? Currency
        {
            readonly get => _currency;
            set => _currency = value;
        }

        /// <summary>Number of orders.</summary>
        public decimal? NumberOfOrders
        {
            readonly get => _numberOfOrders;
            set => _numberOfOrders = value;
        }

        /// <summary>Opening price.</summary>
        public decimal? OpenPrice
        {
            readonly get => _openPrice;
            set => _openPrice = value;
        }

        /// <summary>Volume at open.</summary>
        public decimal? OpenSize
        {
            readonly get => _openSize;
            set => _openSize = value;
        }

        /// <summary>Highest price.</summary>
        public decimal? HighPrice
        {
            readonly get => _highPrice;
            set => _highPrice = value;
        }

        /// <summary>Volume at high.</summary>
        public decimal? HighSize
        {
            readonly get => _highSize;
            set => _highSize = value;
        }

        /// <summary>Lowest price.</summary>
        public decimal? LowPrice
        {
            readonly get => _lowPrice;
            set => _lowPrice = value;
        }

        /// <summary>Volume at low</summary>
        public decimal? LowSize
        {
            readonly get => _lowSize;
            set => _lowSize = value;
        }

        /// <summary>Closing price.</summary>
        public decimal? ClosePrice
        {
            readonly get => _closePrice;
            set => _closePrice = value;
        }

        /// <summary>Volume at close.</summary>
        public decimal? CloseSize
        {
            readonly get => _closeSize;
            set => _closeSize = value;
        }

        /// <summary>Candle state.</summary>
        public CandleStates? CandleState
        {
            readonly get => _candleState;
            set => _candleState = value;
        }

        /// <summary>
        /// Space-delimited list of conditions describing a quote.
        /// </summary>
        public string QuoteCondition
        {
            readonly get => _quoteCondition;
            set => _quoteCondition = value;
        }

        /// <summary>Originator.</summary>
        public char? Originator
        {
            readonly get => _originator;
            set => _originator = value;
        }

        /// <summary>Number of up trending ticks.</summary>
        public int? UpTicks
        {
            readonly get => _upTicks;
            set => _upTicks = value;
        }

        /// <summary>Number of down trending ticks.</summary>
        public int? DownTicks
        {
            readonly get => _downTicks;
            set => _downTicks = value;
        }

        /// <summary>Number of ticks.</summary>
        public int? TotalTicks
        {
            readonly get => _totalTicks;
            set => _totalTicks = value;
        }

        /// <summary>Price levels.</summary>
        public CandlePriceLevel[] PriceLevels
        {
            readonly get => _priceLevels;
            set => _priceLevels = value;
        }

        /// <summary>Yield.</summary>
        public decimal? Yield
        {
            readonly get => _yield;
            set => _yield = value;
        }

        /// <summary>Buying party in a trade.</summary>
        public string Buyer
        {
            readonly get => _buyer;
            set => _buyer = value;
        }

        /// <summary>Selling party in a trade.</summary>
        public string Seller
        {
            readonly get => _seller;
            set => _seller = value;
        }
    }
}
