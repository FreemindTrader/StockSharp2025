using Ecng.Common;
using StockSharp.Algo.Strategies.Quoting;
using StockSharp.Algo.Testing;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace StockSharp.Algo.Strategies.Protective
{
    /// <summary>The base strategy of the position protection.</summary>
    public abstract class ProtectiveStrategy : QuotingStrategy, IProtectiveStrategy
    {
        private readonly bool _isUpTrend;
        private bool _isRegisteringProtectiveQuote;
        private decimal? _activationPrice;
        private bool _waitingForActivationPrice;
        private readonly decimal _protectivePrice;
        private readonly Sides _protectiveSide;
        private readonly StrategyParam<Unit> _protectiveLevel;
        private bool? _isTrailingLicense;
        private readonly StrategyParam<bool> _isTrailing;
        private bool _isActivated;
        private readonly StrategyParam<bool> _useQuoting;
        private readonly StrategyParam<bool> _useMarketOrders;
        private readonly StrategyParam<Unit> _bestPriceOffset;
        private readonly StrategyParam<Unit> _priceOffset;

        private Subscription? _tradeSubscription;

        /// <summary>
        /// Initialize <see cref="T:StockSharp.Algo.Strategies.Protective.ProtectiveStrategy" />.
        /// </summary>
        /// <param name="protectiveSide">Protected position side.</param>
        /// <param name="protectivePrice">Protected position price.</param>
        /// <param name="protectiveVolume">The protected position volume.</param>
        /// <param name="protectiveLevel">The protective level. If the <see cref="P:StockSharp.Messages.Unit.Type" /> type is equal to <see cref="F:StockSharp.Messages.UnitTypes.Limit" />, then the given price is specified. Otherwise, the shift value from <paramref name="protectivePrice" /> is specified.</param>
        /// <param name="isUpTrend">To track price increase or falling.</param>
        protected ProtectiveStrategy( Sides protectiveSide, decimal protectivePrice, decimal protectiveVolume, Unit protectiveLevel, bool isUpTrend )
          : base( protectiveSide.Invert(  ), protectiveVolume )
        {
            _protectiveLevel = this.Param<Unit>( nameof( ProtectiveLevel ), new Unit() );
            _useQuoting = this.Param<bool>( nameof( UseQuoting ), false );
            _useMarketOrders = this.Param<bool>( nameof( UseMarketOrders ), false );
            _bestPriceOffset = this.Param<Unit>( nameof( BestPriceOffset ), new Unit() );
            _priceOffset = this.Param<Unit>( nameof( PriceOffset ), new Unit() );
            _isTrailing = this.Param<bool>( nameof( IsTrailing ), false );
            _protectiveSide = protectiveSide;
            _protectivePrice = protectivePrice;
            ProtectiveLevel = protectiveLevel;
            _isUpTrend = isUpTrend;
        }

        /// <summary>Protected position price.</summary>
        public decimal ProtectivePrice
        {
            get
            {
                return _protectivePrice;
            }
        }

        /// <summary>Protected position side.</summary>
        public Sides ProtectiveSide
        {
            get
            {
                return _protectiveSide;
            }
        }

        /// <summary>Protected volume.</summary>
        public decimal ProtectiveVolume
        {
            get
            {
                return LeftVolume;
            }
            set
            {
                QuotingVolume = value - MathHelper.Abs( Position );

                if ( ProtectiveVolumeChanged == null )
                {
                    return;
                }

                ProtectiveVolumeChanged();
            }
        }

        /// <summary>The protected volume change event.</summary>
        public event Action? ProtectiveVolumeChanged;


        /// <summary>
        /// The protective level. If the <see cref="P:StockSharp.Messages.Unit.Type" /> type is equal to <see cref="F:StockSharp.Messages.UnitTypes.Limit" />, 
        /// then the given price is specified. 
        /// 
        /// Otherwise, the shift value from the protected trade <see cref="T:StockSharp.BusinessEntities.Trade" /> is specified.
        /// </summary>
        public Unit ProtectiveLevel
        {
            get
            {
                return _protectiveLevel.Value;
            }
            set
            {
                _protectiveLevel.Value = value;
                if ( ProcessState != ProcessStates.Started )
                {
                    return;
                }

                ProcessQuoting();
            }
        }


        /// <summary>
        /// Whether to use a trailing technique. For the <see cref="T:StockSharp.Algo.Strategies.Protective.TakeProfitStrategy" /> 
        /// with profit increasing the level of profit taking is automatically increased. 
        /// 
        /// For the <see cref="T:StockSharp.Algo.Strategies.Protective.StopLossStrategy" /> with profit increasing the level of loss protection 
        /// is automatically increased. 
        /// 
        /// The default is disabled.
        /// </summary>
        public bool IsTrailing
        {
            get
            {
                if ( !_isTrailing.Value )
                {
                    return false;
                }

                if ( _isTrailingLicense.HasValue )
                {
                    return _isTrailingLicense.Value;
                }

                if ( !( Connector is BaseEmulationConnector ) )
                {
                    //string str = LicenseHelper.ValidateLicense("Trailing", Portfolio.Name, (Assembly) null);
                    //if ( !StringHelper.IsEmpty( str ) )
                    //{
                    //    nullable_1 = new bool?( false );
                    //    LoggingHelper.AddErrorLog( ( ILogReceiver ) this, str, new object[ 0 ] );
                    //}
                    //else
                    //{
                    //    nullable_1 = new bool?( true );
                    //}

                    _isTrailingLicense = new bool?( true );
                }
                else
                {
                    _isTrailingLicense = new bool?( true );
                }

                return _isTrailingLicense.Value;
            }
            set
            {
                if ( value && ProtectiveLevel.Type == UnitTypes.Limit )
                {
                    throw new ArgumentException( LocalizedStrings.Str1282 );
                }

                _isTrailing.Value = ( value );
            }
        }


        /// <summary>
        /// The absolute value of the price when the one is reached the protective strategy is activated.
        /// </summary>
        /// <remarks>If the price is equal to <see langword="null" /> then the activation is not required.</remarks>
        public virtual decimal? ActivationPrice
        {
            get
            {
                if ( !_activationPrice.HasValue )
                {
                    _activationPrice = ProtectivePrice;
                }

                decimal? lastTradePrice = this.GetSecurityValue<decimal?>( Level1Fields.LastTradePrice );
                decimal? bestPrice = lastTradePrice.HasValue ? lastTradePrice : BestPrice;

                if ( !bestPrice.HasValue )
                {
                    LoggingHelper.AddDebugLog( this, "Best price is null." );
                    lastTradePrice = null;
                    return lastTradePrice;
                }

                if ( IsTimeOut )
                {
                    bool passActivationPrice;

                    if ( _isUpTrend )
                    {
                        passActivationPrice = bestPrice.GetValueOrDefault() > ProtectivePrice & bestPrice.HasValue;
                    }
                    else
                    {
                        passActivationPrice = bestPrice.GetValueOrDefault() < ProtectivePrice & bestPrice.HasValue;
                    }

                    if ( passActivationPrice )
                    {
                        this.AddDebugLog( "Timeout." );
                        return ClosePositionPrice;
                    }
                }

                this.AddDebugLog( "PrevBest={0} CurrBest={1}", _activationPrice, bestPrice );

                if ( IsTrailing )
                {
                    if ( _isUpTrend )
                    {
                        if ( _activationPrice.GetValueOrDefault() < bestPrice.GetValueOrDefault() & ( _activationPrice.HasValue & bestPrice.HasValue ) )
                        {
                            _activationPrice = bestPrice;
                        }
                        else
                        {
                            if ( _activationPrice.GetValueOrDefault() > bestPrice.GetValueOrDefault() & ( _activationPrice.HasValue & bestPrice.HasValue ) )
                            {
                                _waitingForActivationPrice = true;
                            }
                        }
                    }
                    else
                    {
                        if ( _activationPrice.GetValueOrDefault() > bestPrice.GetValueOrDefault() & ( _activationPrice.HasValue & bestPrice.HasValue ) )
                        {
                            _activationPrice = bestPrice;
                        }
                        else
                        {
                            if ( _activationPrice.GetValueOrDefault() < bestPrice.GetValueOrDefault() & ( _activationPrice.HasValue & bestPrice.HasValue ) )
                            {
                                _waitingForActivationPrice = true;
                            }
                        }
                    }

                    if ( !_waitingForActivationPrice )
                    {
                        return null;
                    }

                    decimal level = ProtectiveLevel.Type == UnitTypes.Limit ? MathHelper.Abs( _activationPrice.Value - ( decimal )( ProtectiveLevel ) ) : ( decimal )( ProtectiveLevel );
                    decimal trailingStop = _isUpTrend ? _activationPrice.Value - level : _activationPrice.Value + level;

                    LoggingHelper.AddDebugLog( this, "ActivationPrice={0} level={1}", new object[2] { trailingStop, level } );

                    if ( _isUpTrend )
                    {
                        if ( bestPrice.GetValueOrDefault() <= trailingStop & bestPrice.HasValue )
                        {
                            return ClosePositionPrice;
                        }
                    }
                    else
                    {

                        if ( bestPrice.GetValueOrDefault() >= trailingStop & bestPrice.HasValue )
                        {
                            return ClosePositionPrice;
                        }
                    }

                    return null;
                }
                decimal takeProfitOffSet = ProtectiveLevel.Type == UnitTypes.Limit ? MathHelper.Abs( ProtectivePrice - ( decimal )( ProtectiveLevel ) ) : ( decimal )( ProtectiveLevel );
                decimal takeProfitPrice = _isUpTrend ? _activationPrice.Value + takeProfitOffSet : _activationPrice.Value - takeProfitOffSet;
                LoggingHelper.AddDebugLog( this, "ActivationPrice={0} level={1}", new object[2] { takeProfitPrice, takeProfitOffSet } );

                if ( takeProfitPrice <= decimal.Zero )
                {
                    takeProfitPrice = Security.MinPrice ?? decimal.One;
                }

                if ( _isUpTrend )
                {
                    if ( bestPrice.GetValueOrDefault() >= takeProfitPrice & bestPrice.HasValue )
                    {
                        return ClosePositionPrice;
                    }
                }
                else
                {
                    if ( bestPrice.GetValueOrDefault() <= takeProfitPrice & bestPrice.HasValue )
                    {
                        return ClosePositionPrice;
                    }
                }

                return null;
            }
        }

        /// <summary>Whether the protective strategy is activated.</summary>
        public bool IsActivated
        {
            get
            {
                return _isActivated;
            }

            set
            {
                _isActivated = value;
            }
        }


        /// <summary>The protective strategy activation event.</summary>
        public event Action? Activated;

        /// <summary>
        /// Whether to quote the registered order by the market price. The default mode is disabled.
        /// </summary>
        public bool UseQuoting
        {
            get
            {
                return _useQuoting.Value;
            }
            set
            {
                _useQuoting.Value = ( value );
            }
        }

        /// <summary>
        /// Whether to use <see cref="F:StockSharp.Messages.OrderTypes.Market" /> for protection. 
        /// By default, orders <see cref="F:StockSharp.Messages.OrderTypes.Limit" /> are used.
        /// </summary>
        public bool UseMarketOrders
        {
            get
            {
                return _useMarketOrders.Value;
            }
            set
            {
                _useMarketOrders.Value = ( value );
            }
        }

        /// <summary>
        /// The shift from the best price, on which the quoted order can be changed.
        /// </summary>
        public Unit BestPriceOffset
        {
            get
            {
                return _bestPriceOffset.Value;
            }
            set
            {
                _bestPriceOffset.Value = ( value );
            }
        }


        /// <summary>
        /// The price shift for the registering order. It determines the amount of shift from the best quote 
        /// (for the buy it is added to the price, for the sell it is subtracted).
        /// </summary>
        public Unit PriceOffset
        {
            get
            {
                return _priceOffset.Value;
            }
            set
            {
                _priceOffset.Value = ( value );
            }
        }


        /// <summary>Total quoting volume.</summary>
        public override decimal QuotingVolume
        {
            set
            {
                if ( UseQuoting )
                {
                    foreach ( QuotingStrategy quotingStrategy in ( ( IEnumerable )ChildStrategies ).OfType<QuotingStrategy>() )
                    {
                        quotingStrategy.QuotingVolume = value;
                    }
                }
                else
                {
                    base.QuotingVolume = value;
                }
            }
        }


        /// <summary>
        /// To get a list of rules on which the quoting will respond.
        /// </summary>
        /// <returns>Rule list.</returns>
        protected override IEnumerable<IMarketRule> GetNotificationRules()
        {
            yield return this.Security.WhenNewTrade( this.SafeGetConnector() );
        }


        /// <summary>
        /// The method is called when the <see cref="M:StockSharp.Algo.Strategies.Strategy.Start" /> method has been called 
        /// and the <see cref="P:StockSharp.Algo.Strategies.Strategy.ProcessState" /> state has been taken 
        /// the <see cref="F:StockSharp.Algo.ProcessStates.Started" /> value.
        /// </summary>
        protected override void OnStarted()
        {
            LoggingHelper.AddInfoLog( this, LocalizedStrings.Str1283Params, ProtectiveSide, ProtectivePrice, ProtectiveVolume, ProtectiveLevel, IsTrailing, UseMarketOrders, UseQuoting, PriceOffset );

            _tradeSubscription = Connector.SubscribeTrades( Security );
            //Connector.RegisterTrades( Security, new DateTimeOffset?( ), new DateTimeOffset?( ), new long?( ), 0, null );
            base.OnStarted();
        }


        /// <summary>
        /// The method is called when the <see cref="P:StockSharp.Algo.Strategies.Strategy.ProcessState" /> process state has been taken the <see cref="F:StockSharp.Algo.ProcessStates.Stopping" /> value.
        /// </summary>
        protected override void OnStopping()
        {
            if ( _tradeSubscription != null )
            {
                Connector.UnSubscribe( _tradeSubscription );
            }

            base.OnStopping();
        }


        /// <summary>
        /// It is called from the <see cref="M:StockSharp.Algo.Strategies.Strategy.Reset" /> method.
        /// </summary>
        protected override void OnReseted()
        {
            _isRegisteringProtectiveQuote = false;
            _activationPrice = new decimal?();
            _waitingForActivationPrice = false;
            base.OnReseted();
        }


        /// <summary>
        /// The market price of position closing. If there is no information about the current price, then the <see langword="null" /> will be returned.
        /// </summary>
        protected decimal? ClosePositionPrice
        {
            get
            {
                if ( UseMarketOrders )
                {
                    return new decimal?( new decimal() );
                }

                bool flag = QuotingDirection == 0;
                decimal? securityValue = this.GetSecurityValue<decimal?>( flag ? ( Level1Fields )34 : ( Level1Fields )36 );
                if ( securityValue.HasValue )
                {
                    var value1 = securityValue.Value;
                    var value2 = flag ? PriceOffset : -( decimal )( PriceOffset );



                    return new decimal?( ( decimal )( value1 + value2 ) );
                }

                LoggingHelper.AddWarningLog( this, StringHelper.Put( LocalizedStrings.Str1284Params, Security, QuotingDirection ), new object[0] );

                return null;
            }
        }

        /// <summary>
        /// The <see cref="P:StockSharp.Algo.Strategies.Quoting.QuotingStrategy.TimeOut" /> occurrence event handler.
        /// </summary>
        protected override void ProcessTimeOut()
        {
            ProcessQuoting();
        }


        /// <summary>To register the quoted order.</summary>
        /// <param name="order">The quoted order.</param>
        protected override void RegisterQuotingOrder( Order order )
        {
            if ( UseMarketOrders )
            {
                order.Type = ( new OrderTypes?( ( OrderTypes )1 ) );
            }

            base.RegisterQuotingOrder( order );
        }


        /// <summary>Should the order be quoted.</summary>
        /// <param name="currentPrice">The current price. If the value is equal to <see langword="null" /> then the order is not registered yet.</param>
        /// <param name="currentVolume">The current volume. If the value is equal to <see langword="null" /> then the order is not registered yet.</param>
        /// <param name="newVolume">New volume.</param>
        /// <returns>The price at which the order will be registered. If the value is equal to <see langword="null" /> then the quoting is not required.</returns>
        protected override decimal? NeedQuoting( decimal? currentPrice, decimal? currentVolume, decimal newVolume )
        {
            if ( IsActivated )
            {
                return new decimal?();
            }

            decimal? activationPrice = ActivationPrice;
            if ( !activationPrice.HasValue )
            {
                return new decimal?();
            }

            OnPositionActiviated( activationPrice.Value );
            if ( !UseQuoting )
            {
                return activationPrice;
            }

            LoggingHelper.AddInfoLog( this, LocalizedStrings.Str1285 );

            _isRegisteringProtectiveQuote = true;
            var quoting = CreateQuoting();
            quoting.Name = ( this.Name + "_" + quoting.Name );

            quoting.WhenStopped().Do( this.Stop ).Once().Apply( this );

            ( ( ICollection<Strategy> )ChildStrategies ).Add( quoting );
            return new decimal?();
        }


        /// <summary>To initiate the quoting.</summary>
        protected override void ProcessQuoting()
        {
            if ( UseQuoting && _isRegisteringProtectiveQuote )
            {
                return;
            }

            base.ProcessQuoting();
        }


        /// <summary>
        /// To create a quoting strategy for the protective order (its fulfilment is guaranteed).
        /// </summary>
        /// <returns>The strategy of quoting.</returns>
        protected virtual QuotingStrategy CreateQuoting()
        {
            var quote = new MarketQuotingStrategy( QuotingDirection, ProtectiveVolume );
            quote.BestPriceOffset = BestPriceOffset;
            quote.PriceOffset = PriceOffset;
            return quote;
        }

        private void OnPositionActiviated( decimal price )
        {
            LoggingHelper.AddInfoLog( this, LocalizedStrings.Str1286Params, new object[1]
            {
                price == decimal.Zero ?  LocalizedStrings.Str1287 :   price.To<string>( )
            } );

            IsActivated = true;

            if ( Activated == null )
            {
                return;
            }

            Activated();
        }
    }
}
