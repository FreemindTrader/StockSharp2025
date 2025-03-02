using Ecng.Collections;
using Ecng.Common;
using MoreLinq;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using StockSharp.Algo.Testing;
using StockSharp.Algo.Strategies;
using StockSharp.Algo;

namespace StockSharp.Algo.Strategies.Quoting
{
    /// <summary>Base quoting strategy class.</summary>
    public abstract class QuotingStrategy : Strategy
    {
        private IEnumerable<IMarketRule>? _notificationRules;

        private readonly StrategyParam<bool> _isSupportAtomicReRegister;
        private readonly StrategyParam<bool> _useLastTradePrice;
        private readonly StrategyParam<Sides> _quotingDirection;
        private readonly StrategyParam<decimal> _quotingVolume;
        private readonly StrategyParam<TimeSpan> _timeOut;

        private Order? _order;
        private Order? _orderCloned;
        private Order? _reregisteringOrder;

        private bool _isCancelingProcess;
        private bool _isReregisteringProcess;
        private bool _waitingForOrderTobeFilled;

        private QuoteChangeMessage? _quoteChangeMsg;

        private Trade? _lastTrade;


        /// <summary>
        /// Initialize <see cref="T:StockSharp.Algo.Strategies.Quoting.QuotingStrategy" />.
        /// </summary>
        /// <param name="quotingDirection">Quoting direction.</param>
        /// <param name="quotingVolume">Total quoting volume.</param>
        protected QuotingStrategy( Sides quotingDirection, decimal quotingVolume )
        {
            CheckVolume( quotingVolume );

            _quotingDirection = this.Param( nameof( QuotingDirection ), quotingDirection );
            _quotingVolume = this.Param( nameof( QuotingVolume ), quotingVolume );
            _timeOut = this.Param( nameof( TimeOut ), new TimeSpan() );
            _useLastTradePrice = this.Param( nameof( UseLastTradePrice ), true );
            _isSupportAtomicReRegister = this.Param( nameof( IsSupportAtomicReRegister ), true );

            DisposeOnStop = true;
        }

        /// <summary>
        /// Initialize <see cref="T:StockSharp.Algo.Strategies.Quoting.QuotingStrategy" />.
        /// </summary>
        /// <param name="order">Quoting order.</param>
        protected QuotingStrategy( Order order ) : this( order.Direction, order.TransactionId == 0L ? order.Volume : order.Balance )
        {
            if ( order == null )
                throw new ArgumentNullException( nameof( order ) );
            if ( order.TransactionId == 0L )
                return;
            _order = order;
        }

        /// <summary>
        /// Gets a value indicating whether the re-registration orders via the method <see cref="M:StockSharp.BusinessEntities.ITransactionProvider.ReRegisterOrder(StockSharp.BusinessEntities.Order,StockSharp.BusinessEntities.Order)" /> as a single transaction. The default is enabled.
        /// </summary>
        public bool IsSupportAtomicReRegister
        {
            get
            {
                return _isSupportAtomicReRegister.Value;
            }
            set
            {
                _isSupportAtomicReRegister.Value = value;
            }
        }

        /// <summary>
        /// To use the last trade price, if the information in the order book is missed. The default is enabled.
        /// </summary>
        public bool UseLastTradePrice
        {
            get
            {
                return _useLastTradePrice.Value;
            }
            set
            {
                _useLastTradePrice.Value = value;
            }
        }

        /// <summary>Quoting direction.</summary>
        public Sides QuotingDirection
        {
            get
            {
                return _quotingDirection.Value;
            }
            set
            {
                if ( value == QuotingDirection )
                    return;

                if ( Position != decimal.Zero )
                    throw new InvalidOperationException( LocalizedStrings.Str1288 );

                _quotingDirection.Value = value;
            }
        }

        /// <summary>Total quoting volume.</summary>
        public virtual decimal QuotingVolume
        {
            get
            {
                return _quotingVolume.Value;
            }
            set
            {
                if ( value == QuotingVolume )
                    return;

                this.AddInfoLog( LocalizedStrings.Str1289Params, new object[2] { QuotingVolume, value } );

                CheckVolume( value );

                if ( ProcessState == ProcessStates.Started )
                    ProcessQuoting();

                _quotingVolume.Value = value;
            }
        }

        private static void CheckVolume( decimal volume )
        {
            if ( volume <= decimal.Zero )
            {
                throw new ArgumentOutOfRangeException( "quotingVolume", volume, LocalizedStrings.Str1290 );
            }
        }

        /// <summary>
        /// The order with which the quoting strategy is currently operating.
        /// </summary>        
        public virtual Order? Order
        {
            get
            {
                return _order;
            }
        }

        /// <summary>
        /// The volume which is left to fulfill before the quoting end.
        /// </summary>        
        public decimal LeftVolume
        {
            get
            {
                return QuotingVolume - Position.Abs();
            }
        }

        /// <summary>
        /// The time limit during which the quoting should be fulfilled. If the total volume of <see cref="P:StockSharp.Algo.Strategies.Quoting.QuotingStrategy.QuotingVolume" /> will not be fulfilled by this time, the strategy will stop operating.
        /// </summary>
        /// <remarks>
        /// By default, the limit is disabled and it is equal to <see cref="F:System.TimeSpan.Zero" />.
        /// </remarks>
        public TimeSpan TimeOut
        {
            get
            {
                return _timeOut.Value;
            }
            set
            {
                if ( value < TimeSpan.Zero )
                {
                    throw new ArgumentOutOfRangeException( nameof( value ), value, LocalizedStrings.Str1227 );
                }

                _timeOut.Value = value;
            }
        }

        /// <summary>
        /// Is the <see cref="P:StockSharp.Algo.Strategies.Quoting.QuotingStrategy.TimeOut" /> occurred.
        /// </summary>
        protected bool IsTimeOut
        {
            get
            {
                if ( TimeOut != TimeSpan.Zero )
                    return CurrentTime - StartedTime >= TimeOut;
                return false;
            }
        }

        /// <summary>Whether the quoting can be stopped.</summary>
        /// <returns><see langword="true" /> it is possible, otherwise, <see langword="false" />.</returns>
        /// <remarks>
        /// By default, the quoting is stopped when all contracts are fulfilled and <see cref="P:StockSharp.Algo.Strategies.Quoting.QuotingStrategy.LeftVolume" /> is equal to 0.
        /// </remarks>
        protected virtual bool NeedFinish()
        {
            return LeftVolume <= decimal.Zero;
        }

        /// <summary>Should the order be quoted.</summary>
        /// <param name="currentPrice">The current price. If the value is equal to <see langword="null" /> then the order is not registered yet.</param>
        /// <param name="currentVolume">The current volume. If the value is equal to <see langword="null" /> then the order is not registered yet.</param>
        /// <param name="newVolume">New volume.</param>
        /// <returns>The price at which the order will be registered. If the value is equal to <see langword="null" /> then the quoting is not required.</returns>
        protected virtual decimal? NeedQuoting( decimal? currentPrice, decimal? currentVolume, decimal newVolume )
        {
            if ( !BestPrice.HasValue )
                return null;

            if ( !currentPrice.HasValue )
                return null;

            if ( BestPrice.Value == currentPrice.Value )
            {
                if ( currentVolume.HasValue && currentVolume.Value == newVolume )
                {
                    return null;
                }
            }
            return BestPrice;
        }

        /// <summary>
        /// To get the best price. If it is impossible to calculate the best price at the moment, then <see langword="null" /> will be returned.
        /// </summary>
        protected virtual decimal? BestPrice
        {
            get
            {
                QuoteChange[ ]? filteredQuotes = GetFilteredQuotes( QuotingDirection );
                QuoteChange? bestQuote = filteredQuotes != null ? filteredQuotes.FirstOr() : new QuoteChange?();

                if ( bestQuote.HasValue )
                {
                    return bestQuote.Value.Price;
                }

                if ( UseLastTradePrice )
                {
                    return LastTradePrice;
                }

                return null;
            }
        }

        /// <summary>Last trade price.</summary>
        protected decimal? LastTradePrice
        {
            get
            {
                return _lastTrade?.Price;
            }
        }

        /// <summary>To get a new volume for an order.</summary>
        /// <returns>The new volume for an order.</returns>
        protected virtual decimal GetNewVolume()
        {
            if ( Volume <= decimal.Zero )
                return LeftVolume;

            decimal newVolume = Volume;

            if ( LeftVolume > decimal.Zero )
                newVolume = newVolume.Min( LeftVolume );

            return newVolume;
        }

        /// <summary>
        /// To get the order book filtered with <see cref="M:StockSharp.BusinessEntities.IMarketDataProvider.GetFilteredMarketDepth(StockSharp.BusinessEntities.Security)" />.
        /// </summary>
        /// <param name="side">The order book side (bids or offers).</param>
        /// <returns>The filtered order book.</returns>
        protected QuoteChange[ ]? GetFilteredQuotes( Sides side )
        {
            if ( _quoteChangeMsg == null )
                return null;

            return side == Sides.Buy ? _quoteChangeMsg.Bids : _quoteChangeMsg.Asks;
        }

        /// <summary>
        /// To get a list of rules on which the quoting will respond.
        /// </summary>
        /// <returns>Rule list.</returns>
        protected virtual IEnumerable<IMarketRule> GetNotificationRules()
        {
            yield return this.SubscribeTrades( Security ).WhenTickTradeReceived( this );
            // yield return this.SubscribeFilteredMarketDepth( Security ).WhenOrderBookReceived( this );            
        }

        /// <inheritdoc />
        protected override void OnStarted()
        {
            base.OnStarted();

            _orderCloned = null;
            _isCancelingProcess = false;
            _isReregisteringProcess = false;
            _reregisteringOrder = null;
            _waitingForOrderTobeFilled = false;
            _order = null;
            _quoteChangeMsg = null;
            _lastTrade = null;

            this.AddInfoLog( LocalizedStrings.Str1293Params, QuotingDirection, QuotingVolume );
            this.SuspendRules( new Action( LiveUpdateAction ) );

            if ( IsRulesSuspended )
                return;

            ProcessQuoting();
        }

        /// <summary>
        /// The <see cref="P:StockSharp.Algo.Strategies.Quoting.QuotingStrategy.TimeOut" /> occurrence event handler.
        /// </summary>
        protected virtual void ProcessTimeOut()
        {
            Stop();
        }

        /// <inheritdoc />
        protected override void OnStopping()
        {
            if ( _notificationRules != null )
            {
                foreach ( IMarketRule marketRule in _notificationRules )
                {
                    marketRule.Suspend( true );
                }
            }

            base.OnStopping();
        }

        /// <summary>To register the quoted order.</summary>
        /// <param name="order">The quoted order.</param>
        protected virtual void RegisterQuotingOrder( Order order )
        {
            SetupOrderCloneRules( order );
            _isReregisteringProcess = true;
            RegisterOrder( order );
        }

        private void CheckIfReRegistering( Order myOrder )
        {
            if ( myOrder == _order )
            {
                _isReregisteringProcess = false;
                this.AddInfoLog( LocalizedStrings.Str1299Params, myOrder.TransactionId );
            }
            else if ( myOrder == _reregisteringOrder )
            {
                if ( _order != null )
                {
                    this.AddInfoLog( LocalizedStrings.Str1300Params, _order.TransactionId, _reregisteringOrder.TransactionId );

                    Rules.RemoveRulesByToken( _order, null );
                    _order = _reregisteringOrder;
                    _reregisteringOrder = null;
                }
            }
            else
                this.AddWarningLog( LocalizedStrings.Str1301Params, new object[1] { myOrder.TransactionId } );

            ProcessQuoting();
        }

        private void SetupOrderCloneRules( Order orderClone )
        {
            var orderOkayRule = orderClone.WhenRegistered( this ).Do( CheckIfReRegistering ).Once().Apply( this );
            var orderFailRule = orderClone.WhenRegisterFailed( this ).Do( ProcessFailedOrder ).Once().Apply( this );

            orderOkayRule.Exclusive( orderFailRule );

            var allFilledRule = orderClone.WhenMatched( this ).Do( OrderCompletelyFilled ).Once().Apply( this );
            orderFailRule.Exclusive( allFilledRule );
        }

        /// <summary>To initiate the quoting.</summary>
        protected virtual void ProcessQuoting()
        {
            if ( ProcessState != ProcessStates.Started )
            {
                this.AddWarningLog( LocalizedStrings.Str1304Params, new object[1] { ProcessState } );
            }
            else
            {
                if ( _order != null )
                {
                    if ( _isCancelingProcess )
                    {
                        this.AddDebugLog( LocalizedStrings.Str1305Params, _order.TransactionId );
                        return;
                    }

                    if ( _isReregisteringProcess )
                    {
                        this.AddDebugLog( LocalizedStrings.Str1306Params, _order.TransactionId );
                        return;
                    }

                    if ( _orderCloned != null )
                    {
                        this.AddDebugLog( LocalizedStrings.Str1307Params, _order.TransactionId, _orderCloned.TransactionId );
                        return;
                    }

                    if ( _reregisteringOrder != null )
                    {
                        this.AddDebugLog( LocalizedStrings.Str1307Params, _order.TransactionId, _reregisteringOrder.TransactionId );
                        return;
                    }

                    if ( _waitingForOrderTobeFilled )
                    {
                        this.AddDebugLog( LocalizedStrings.Str1308Params, _order.TransactionId );
                        return;
                    }

                    if ( ProcessState != ProcessStates.Started )
                        return;
                }

                decimal newVolume = GetNewVolume();
                decimal? newPrice = NeedQuoting( _order?.Price, _order?.Balance, newVolume );

                if ( !newPrice.HasValue )
                    return;

                newPrice = Security.ShrinkPrice( newPrice.Value, ShrinkRules.Auto );

                var orderPrice = _order?.Price;
                var priceStr = orderPrice.HasValue ? ( object )orderPrice.GetValueOrDefault() : "order price is null";

                this.AddInfoLog( LocalizedStrings.Str1310Params, priceStr, newPrice );

                decimal? bestAskPrice = null;
                decimal? bestBidPrice = null;

                if ( _quoteChangeMsg != null )
                {
                    QuoteChange[ ] bids = _quoteChangeMsg.Bids;
                    if ( bids != null )
                    {
                        var bestBid = bids.FirstOr();

                        bestBidPrice = bestBid.HasValue ? bestBid.Value.Price : null;

                        bestBidPrice = bestBidPrice ?? this.GetSecurityValue<decimal?>( Level1Fields.BestBidPrice );
                    }

                    QuoteChange[ ] asks = _quoteChangeMsg.Asks;
                    if ( asks != null )
                    {
                        var bestAsk = asks.FirstOr();

                        bestAskPrice = bestAsk.HasValue ? bestAsk.Value.Price : null;

                        bestAskPrice = bestAskPrice ?? this.GetSecurityValue<decimal?>( Level1Fields.BestAskPrice );
                    }
                }

                object bestAskString = bestAskPrice.HasValue ? bestAskPrice.GetValueOrDefault() : "NA";
                object bestBidString = bestBidPrice.HasValue ? bestBidPrice.GetValueOrDefault() : "NA";
                this.AddInfoLog( LocalizedStrings.Str1311Params, bestBidString, bestAskString );

                if ( _order == null )
                {
                    if ( !AllowTrading )
                        return;

                    _order = this.CreateOrder( QuotingDirection, newPrice.Value, newVolume );
                    RegisterQuotingOrder( _order );
                }
                else
                {
                    this.AddInfoLog( LocalizedStrings.Str1312Params, _order.TransactionId, _order.Direction, _order.Price, _order.Volume );

                    if ( IsSupportAtomicReRegister )
                    {
                        if ( IsOrderReplaceable( _order ).GetValueOrDefault() == true & IsOrderReplaceable( _order ).HasValue )
                        {
                            if ( orderPrice.GetValueOrDefault() == 0 & orderPrice.HasValue )
                            {
                                this.AddWarningLog( LocalizedStrings.Str1313 );
                                return;
                            }

                            Order orderCloned = _order.ReRegisterClone( newPrice, _order.Balance );

                            if ( IsOrderEditable( _order ).GetValueOrDefault() == true & IsOrderEditable( _order ).HasValue )
                            {
                                _orderCloned = orderCloned;

                                var orderEditOkay = _order.WhenEdited( this ).Do( ProcessOrderWhenEdited ).Once().Apply( this );
                                var orderEditFail = _order.WhenEditFailed( this ).Do( ProcessOrderWhenEditedFailed ).Once().Apply( this );

                                orderEditOkay.Exclusive( orderEditFail );

                                EditOrder( _order, orderCloned );
                            }
                            else
                            {
                                SetupOrderCloneRules( orderCloned );
                                _reregisteringOrder = orderCloned;
                                _waitingForOrderTobeFilled = false;

                                ReRegisterOrder( _order, orderCloned );
                            }
                            this.AddInfoLog( LocalizedStrings.Str1314Params, _order.TransactionId, orderCloned.Price, orderCloned.Volume );
                            return;
                        }
                    }
                    this.AddInfoLog( LocalizedStrings.Str1315Params, _order.TransactionId );

                    _order.WhenCanceled( this ).Do( ProcessOrderWhenCanceled ).Once().Apply( this );
                    _order.WhenCancelFailed( this ).Do( ProcessOrderWhenCancelFailed ).Once().Apply( this );
                    _isCancelingProcess = true;

                    CancelOrder( _order );
                }
            }
        }

        private void LiveUpdateAction()
        {
            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  Subscribe to MarketDept cause BufferMessageAdapter to Create a SnapShortStorage for it.
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            this.SubscribeFilteredMarketDepth( Security ).WhenOrderBookReceived( this ).Do( m => _quoteChangeMsg = m ).Apply( this );
            this.SubscribeTrades( Security ).WhenTickTradeReceived( this ).Do( t =>
            {
                _lastTrade = t;
            }
                                                                             ).Apply( this );
            this.WhenStopping().Do( CheckUnfilledVolume ).Once().Apply( this );

            _notificationRules = GetNotificationRules().ToArray();

            if ( !_notificationRules.IsEmpty() )
            {
                _notificationRules.Or().Do( ProcessQuoting ).Apply( this );
            }

            this.WhenPositionChanged().Do( new Action( ProcessPositionChanged ) ).Apply( this );

            if ( TimeOut <= TimeSpan.Zero )
                return;

            SafeGetConnector().WhenIntervalElapsed( TimeOut ).Do( ProcessTimeOut ).Once().Apply( this );
        }

        private void CheckUnfilledVolume()
        {
            if ( LeftVolume == decimal.Zero )
                return;

            this.AddWarningLog( LocalizedStrings.Str1294Params, LeftVolume );
        }

        private void ProcessPositionChanged()
        {
            this.AddInfoLog( LocalizedStrings.Str1295Params, Position, LeftVolume );

            if ( !NeedFinish() )
                return;

            this.AddInfoLog( LocalizedStrings.Str1296 );
            Stop();
        }

        private void ProcessFailedOrder( OrderFail failedOrder )
        {
            Order order = failedOrder.Order;
            this.AddErrorLog( LocalizedStrings.Str1302Params, order.TransactionId, failedOrder.Error.Message );
            bool neeQuoting = false;

            if ( order == _order )
            {
                _order = null;
                _isReregisteringProcess = false;
                neeQuoting = true;
            }
            else if ( order == _reregisteringOrder )
            {
                _reregisteringOrder = null;
                _waitingForOrderTobeFilled = true;
            }
            else
            {
                this.AddWarningLog( LocalizedStrings.Str1301Params, order.TransactionId );
            }

            if ( !neeQuoting )
                return;
            ProcessQuoting();
        }

        private void OrderCompletelyFilled( MarketRule<Order, Order> orderRule, Order myOrder )
        {
            this.AddInfoLog( LocalizedStrings.Str1303Params, myOrder.TransactionId, LeftVolume );

            Rules.RemoveRulesByToken( myOrder, orderRule );

            if ( myOrder == _reregisteringOrder )
            {
                CheckIfReRegistering( myOrder );
            }

            if ( NeedFinish() )
            {
                this.AddInfoLog( LocalizedStrings.Str1296 );
                Stop();
            }
            else if ( _order == myOrder )
            {
                _isCancelingProcess = false;
                _order = null;
                _reregisteringOrder = null;
                _waitingForOrderTobeFilled = false;
                _orderCloned = null;
                ProcessQuoting();
            }
            else
            {
                this.AddWarningLog( LocalizedStrings.Str1301Params, myOrder.TransactionId );
            }

        }

        private void ProcessOrderWhenEdited()
        {
            if ( _order != null )
                this.AddDebugLog( "Order TransactionID = ", _order.TransactionId );

            _orderCloned = null;

            ProcessQuoting();
        }

        private void ProcessOrderWhenEditedFailed( OrderFail _param1 )
        {
            bool flag = false;
            this.AddErrorLog( "ProcessOrderWhenEditedFailed", _param1.Order.TransactionId, _param1.Error.Message );
            if ( _param1.Order == _order )
                flag = true;
            else
                this.AddWarningLog( LocalizedStrings.Str1301Params, _param1.Order.TransactionId );
            _orderCloned = null;
            if ( !flag )
                return;
            ProcessQuoting();
        }

        private void ProcessOrderWhenCanceled(
          MarketRule<Order, Order> _param1,
          Order _param2 )
        {
            this.AddInfoLog( LocalizedStrings.Str1316Params, new object[2]
            {
         _param2.TransactionId,
         _param2.LastChangeTime
            } );
            Rules.RemoveRulesByToken( _param2, _param1 );
            if ( _order == _param2 )
            {
                _order = null;
                _isCancelingProcess = false;
                ProcessQuoting();
            }
            else
                this.AddWarningLog( LocalizedStrings.Str1301Params, new object[1]
                {
           _param2.TransactionId
                } );
        }

        private void ProcessOrderWhenCancelFailed(
          MarketRule<Order, OrderFail> _param1,
          OrderFail _param2 )
        {
            this.AddInfoLog( LocalizedStrings.Str3373Params, new object[2]
            {
         _param2.Order.TransactionId,
         _param2.Error
            } );
            _isCancelingProcess = false;
        }

        //private sealed class MarketRulesEnumerator : IEnumerable<IMarketRule>, IEnumerable, IEnumerator<IMarketRule>, IEnumerator, IDisposable
        //{
        //    private int _state;
        //    private IMarketRule  _current;
        //    private int _initialThreadId;
        //    public QuotingStrategy _strategy;

        //    [DebuggerHidden]
        //    public MarketRulesEnumerator( int _param1 )
        //    {
        //        this._state = _param1;
        //        this._initialThreadId = Environment.CurrentManagedThreadId;
        //    }

        //    [DebuggerHidden]
        //    void IDisposable.Dispose( )
        //    {
        //    }

        //    bool IEnumerator.MoveNext( )
        //    {
        //        int state = this._state;
        //        QuotingStrategy quotingStrategy = this._strategy;
        //        if ( state != 0 )
        //        {
        //            if ( state != 1 )
        //                return false;
        //            this._state = -1;
        //            return false;
        //        }
        //        this._state = -1;
        //        this._current = ( IMarketRule ) quotingStrategy.SubscribeFilteredMarketDepth( quotingStrategy.Security ).WhenOrderBookReceived( ( ISubscriptionProvider ) quotingStrategy );
        //        this._state = 1;
        //        return true;
        //    }

        //    [DebuggerHidden]
        //    IMarketRule IEnumerator<IMarketRule>.Current( )
        //    {
        //        return this._current;
        //    }

        //    [DebuggerHidden]
        //    void IEnumerator.Reset( )
        //    {
        //        throw new NotSupportedException( );
        //    }

        //    [DebuggerHidden]
        //    object IEnumerator.Current( )
        //    {
        //        return this._current;
        //    }

        //    [DebuggerHidden]
        //    IEnumerator<IMarketRule> IEnumerable<IMarketRule>.GetEnumerator( )
        //    {
        //        QuotingStrategy.MarketRulesEnumerator myEnumerator;
        //        if ( this._state == -2 && this._initialThreadId == Environment.CurrentManagedThreadId )
        //        {
        //            this._state = 0;
        //            myEnumerator = this;
        //        }
        //        else
        //        {
        //            myEnumerator = new QuotingStrategy.MarketRulesEnumerator( 0 );
        //            myEnumerator._strategy = this._strategy;
        //        }
        //        return ( IEnumerator<IMarketRule> ) myEnumerator;
        //    }

        //    [DebuggerHidden]
        //    IEnumerator IEnumerable.GetEnumerator( )
        //    {
        //        return ( IEnumerator ) this.GetEnumerator( );
        //    }
        //}

    }
}

