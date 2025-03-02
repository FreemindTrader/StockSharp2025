// Decompiled with JetBrains decompiler
// Type: StockSharp.Fix.Native.ExecutionReport
// Assembly: StockSharp.Fix.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9148E39-A5BB-4657-14B1-EA8DED27B1C2
// Assembly location: A:\StockSharpBin\Terminal\StockSharp.Fix.Core.dll

using System;
using System.Collections.Generic;

namespace StockSharp.Fix.Native
{
    /// <summary>Execution report.</summary>
    public class ExecutionReport
    {
        /// <summary>
        /// <see cref="F:StockSharp.Fix.Native.FixTags.PossDupFlag" />.
        ///     </summary>
        public bool? PossDupFlag;
        /// <summary>
        /// Describes the purpose of the execution report <see cref="T:StockSharp.Fix.Native.ExecType" />.
        /// </summary>
        public char? ExecType;
        /// <summary>
        /// Used to identify the previous order in cancel and cancel/replace requests.
        /// </summary>
        public string OrigClOrdId;
        /// <summary>
        /// Unique identifier for Order as assigned by sell-side (broker, exchange, ECN).
        /// </summary>
        public string OrderId;
        /// <summary>
        /// Unique identifier for Order as assigned by the buy-side (institution, broker, intermediary etc.).
        /// </summary>
        public string ClOrdId;
        /// <summary>
        /// Quantity ordered. This represents the number of shares for equities or par, face or nominal value for FI instruments.
        /// </summary>
        public decimal? OrderQty;
        /// <summary>Quantity open for further execution.</summary>
        public decimal? LeavesQty;

        private decimal? _cumQty;

        private DateTime? _trdRegTimestamp;

        private StockSharp.Fix.Native.TrdRegTimestampType? _trdRegTimestampType;

        private decimal? _maxFloor;

        private string _massStatusReqId;

        private char? _lastCapacity;

        private char? _orderCapacity;

        private string _orderRestrictions;

        private long? _extendedOrderStatus;

        private int? _extendedTradeStatus;

        private char? _cashMargin;

        private decimal? _avgPrice;
        /// <summary>Price per unit of quantity (e.g. per share).</summary>
        public decimal? Price;
        /// <summary>
        /// Ticker symbol. Common, "human understood" representation of the security.
        /// </summary>
        public string Symbol;
        /// <summary>Market used to help identify a security.</summary>
        public string SecurityExchange;
        /// <summary>Security identifier value.</summary>
        public string SecurityId;
        /// <summary>
        /// Identifies class or source of the <see cref="F:StockSharp.Fix.Native.ExecutionReport.SecurityId" /> value. Required if <see cref="F:StockSharp.Fix.Native.ExecutionReport.SecurityId" /> is specified.
        /// </summary>
        public string SecurityIdSource;
        /// <summary>Identifier for Trading Session.</summary>
        public string TradingSessionId;
        /// <summary>
        /// </summary>
        public string ExDestination;
        /// <summary>
        /// </summary>
        public DateTime SendingTime;
        /// <summary>
        /// Order type <see cref="T:StockSharp.Fix.Native.OrdType" />.
        /// </summary>
        public char? OrdType;
        /// <summary>
        /// </summary>
        public DateTime? TradeDate;
        /// <summary>
        /// </summary>
        public DateTime? TransactTime;
        /// <summary>
        /// </summary>
        public string Account;
        /// <summary>
        /// </summary>
        public string ClientId;
        /// <summary>
        /// </summary>
        public string ExecBroker;
        /// <summary>
        /// </summary>
        public Party[] Parties;
        /// <summary>
        /// </summary>
        public char? OrdStatus;
        /// <summary>
        /// Side of order <see cref="T:StockSharp.Fix.Native.Side" />.
        /// </summary>
        public char? Side;
        /// <summary>
        /// Specifies how long the order remains in effect. Absence of this field is interpreted as DAY. NOTE not applicable to CIV Orders.
        /// </summary>
        public char? TimeInForce;
        /// <summary>
        /// Date of order expiration (last day the order can trade), always expressed in terms of the local market date.
        /// </summary>
        public DateTime? ExpireDate;
        /// <summary>
        /// </summary>
        public DateTime? ExpireTime;
        /// <summary>Price of this (last) fill.</summary>
        public decimal? LastPx;
        /// <summary>
        /// Quantity (e.g. shares) bought/sold on this (last) fill.
        /// </summary>
        public decimal? LastQty;
        /// <summary>Commission.</summary>
        public decimal? Commission;
        /// <summary>
        /// Commission currency. Can be <see lnagword="null" />.
        /// </summary>
        public string CommissionCurrency;
        /// <summary>Free format text string.</summary>
        public string Text;
        /// <summary>
        /// Unique identifier of execution message as assigned by sell-side (broker, exchange, ECN).
        /// </summary>
        public string ExecId;
        /// <summary>
        /// Required if responding to and if provided on the <see cref="F:StockSharp.Fix.Native.FixMessages.OrderStatusRequest" /> message.
        /// </summary>
        public string OrdStatusReqId;
        /// <summary>
        /// Indicator to identify whether this fill was a result of a liquidity provider providing or liquidity taker taking the liquidity.
        /// Valid values:
        /// 1 = Added Liquidity
        /// 2 = Removed Liquidity
        /// 3 = Liquidity Routed Out
        /// </summary>
        public int? LastLiquidityInd;

        private decimal? _minQty;

        private decimal? _yield;

        private decimal? _stopPrice;

        private char? _positionEffect;

        private bool? _aggressorIndicator;

        private bool? _manualOrderIndicator;

        private DateTime? _origSendingTime;

        private long _msgSeqNum;

        private string _secondaryOrderId;

        private string _strategyTypeId;

        private int? _leverage;

        private decimal? _displayQty;

        private readonly IDictionary<FixTags, object> _extraFields = (IDictionary<FixTags, object>)new Dictionary<FixTags, object>();

        /// <summary>Total quantity (e.g. number of shares) filled.</summary>
        public decimal? CumQty
        {
            get => this._cumQty;
            set => this._cumQty = value;
        }

        /// <summary>Traded / Regulatory timestamp value.</summary>
        public DateTime? TrdRegTimestamp
        {
            get => this._trdRegTimestamp;
            set => this._trdRegTimestamp = value;
        }

        /// <summary>Traded / Regulatory timestamp type.</summary>
        public StockSharp.Fix.Native.TrdRegTimestampType? TrdRegTimestampType
        {
            get => this._trdRegTimestampType;
            set => this._trdRegTimestampType = value;
        }

        /// <summary>
        /// Maximum quantity (e.g. number of shares) within an order to be shown on the exchange floor at any given time.
        /// </summary>
        public decimal? MaxFloor
        {
            get => this._maxFloor;
            set => this._maxFloor = value;
        }

        /// <summary>
        /// Required if responding to and if provided on the <see cref="F:StockSharp.Fix.Native.FixMessages.OrderMassStatusRequest" /> message.
        /// </summary>
        public string MassStatusReqId
        {
            get => this._massStatusReqId;
            set => this._massStatusReqId = value;
        }

        /// <summary>Broker capacity in order execution.</summary>
        public char? LastCapacity
        {
            get => this._lastCapacity;
            set => this._lastCapacity = value;
        }

        /// <summary>
        /// Designates the capacity of the firm placing the order.
        /// </summary>
        public char? OrderCapacity
        {
            get => this._orderCapacity;
            set => this._orderCapacity = value;
        }

        /// <summary>
        /// Restrictions associated with an order. If more than one restriction is applicable to an order, this field can contain multiple instructions separated by space.
        /// </summary>
        public string OrderRestrictions
        {
            get => this._orderRestrictions;
            set => this._orderRestrictions = value;
        }

        /// <summary>Extended order status.</summary>
        public long? ExtendedOrderStatus
        {
            get => this._extendedOrderStatus;
            set => this._extendedOrderStatus = value;
        }

        /// <summary>Extended trade status.</summary>
        public int? ExtendedTradeStatus
        {
            get => this._extendedTradeStatus;
            set => this._extendedTradeStatus = value;
        }

        /// <summary>
        /// Identifies whether an order is a margin order or a non-margin order.
        /// </summary>
        public char? CashMargin
        {
            get => this._cashMargin;
            set => this._cashMargin = value;
        }

        /// <summary>Calculated average price of all fills on this order.</summary>
        public decimal? AvgPx
        {
            get => this._avgPrice;
            set => this._avgPrice = value;
        }

        /// <summary>Minimum quantity of an order to be executed.</summary>
        public decimal? MinQty
        {
            get => this._minQty;
            set => this._minQty = value;
        }

        /// <summary>Yield.</summary>
        public decimal? Yield
        {
            get => this._yield;
            set => this._yield = value;
        }

        /// <summary>Stop price.</summary>
        public decimal? StopPx
        {
            get => this._stopPrice;
            set => this._stopPrice = value;
        }

        /// <summary>
        /// Indicates whether the resulting position after a trade should be an opening position or closing position.
        /// </summary>
        public char? PositionEffect
        {
            get => this._positionEffect;
            set => this._positionEffect = value;
        }

        /// <summary>
        /// Used to identify whether the order initiator is an aggressor or not in the trade.
        /// </summary>
        public bool? AggressorIndicator
        {
            get => this._aggressorIndicator;
            set => this._aggressorIndicator = value;
        }

        /// <summary>
        /// Indicates if the order was initially received manually (as opposed to electronically).
        /// </summary>
        public bool? ManualOrderIndicator
        {
            get => this._manualOrderIndicator;
            set => this._manualOrderIndicator = value;
        }

        /// <summary>
        /// <see cref="F:StockSharp.Fix.Native.FixTags.OrigSendingTime" />.
        ///     </summary>
        public DateTime? OrigSendingTime
        {
            get => this._origSendingTime;
            set => this._origSendingTime = value;
        }

        /// <summary>
        /// <see cref="F:StockSharp.Fix.Native.FixTags.MsgSeqNum" />.
        ///     </summary>
        public long MsgSeqNum
        {
            get => this._msgSeqNum;
            set => this._msgSeqNum = value;
        }

        /// <summary>
        /// <see cref="F:StockSharp.Fix.Native.FixTags.SecondaryOrderID" />.
        ///     </summary>
        public string SecondaryOrderId
        {
            get => this._secondaryOrderId;
            set => this._secondaryOrderId = value;
        }

        /// <summary>
        /// <see cref="F:StockSharp.Fix.Native.FixTags.StrategyTypeId" />.
        ///     </summary>
        public string StrategyTypeId
        {
            get => this._strategyTypeId;
            set => this._strategyTypeId = value;
        }

        /// <summary>
        /// <see cref="F:StockSharp.Fix.Native.FixTags.Leverage" />.
        ///     </summary>
        public int? Leverage
        {
            get => this._leverage;
            set => this._leverage = value;
        }

        /// <summary>
        /// <see cref="F:StockSharp.Fix.Native.FixTags.DisplayQty" />.
        ///     </summary>
        public decimal? DisplayQty
        {
            get => this._displayQty;
            set => this._displayQty = value;
        }

        /// <summary>Extra fields.</summary>
        public IDictionary<FixTags, object> ExtraFields => this._extraFields;
    }
}
