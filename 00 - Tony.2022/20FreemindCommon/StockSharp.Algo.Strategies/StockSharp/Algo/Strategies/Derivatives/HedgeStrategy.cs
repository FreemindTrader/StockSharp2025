using Ecng.Collections;
using Ecng.Common;
using StockSharp.Algo.Derivatives;
using StockSharp.Algo.Testing;
using StockSharp.BusinessEntities;
//using StockSharp.Licensing;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using StockSharp.Algo.Storages;
using System.Linq;
using System.Reflection;
using System.Threading;
using fx.Collections;
using StockSharp.Algo.Strategies;
using StockSharp.Algo;
using StockSharp.Algo.Strategies.Quoting;

namespace StockSharp.Algo.Strategies.Derivatives
{
    /// <summary>The base strategy of hedging.</summary>
    public abstract class HedgeStrategy : Strategy
    {
        private readonly SynchronizedDictionary<Security, Strategy> _securityStratgies = new SynchronizedDictionary<Security, Strategy>();
        private Strategy? _strategy;
        private readonly PooledSet<Order> _orders = new PooledSet<Order>();
        private readonly SyncObject _lock = new SyncObject();
        private readonly BasketBlackScholes _blackScholes;
        private readonly StrategyParam<bool> _useQuoting;
        private readonly StrategyParam<Unit> _priceOffset;

        /// <summary>
        /// Initialize <see cref="T:StockSharp.Algo.Strategies.Derivatives.HedgeStrategy" />.
        /// </summary>
        /// <param name="exchangeInfoProvider">Exchanges and trading boards provider.</param>
        protected HedgeStrategy( IExchangeInfoProvider exchangeInfoProvider )
        {
            _blackScholes = new BasketBlackScholes( this, this, exchangeInfoProvider, this );
            _useQuoting = new StrategyParam<bool>( this, nameof( UseQuoting ) );
            _priceOffset = new StrategyParam<Unit>( this, nameof( PriceOffset ), new Unit() );
        }

        /// <summary>
        /// Portfolio model for calculating the values of Greeks by the Black-Scholes formula.
        /// </summary>
        protected BasketBlackScholes BlackScholes
        {
            get
            {
                return _blackScholes;
            }
        }

        /// <summary>
        /// Whether to quote the registered order by the market price. The default mode is disabled.
        /// </summary>
        [Display( Description = "Str1265", GroupName = "Str1244", Name = "Str1264", Order = 0, ResourceType = typeof( LocalizedStrings ) )]
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
        /// The price shift for the registering order. It determines the amount of shift from the best quote (for the buy it is added to the price, for the sell it is subtracted).
        /// </summary>
        [Display( Description = "Str1267", GroupName = "Str1244", Name = "Str1266", Order = 1, ResourceType = typeof( LocalizedStrings ) )]
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

        /// <summary>
        /// To get a list of rules on which the rehedging will respond.
        /// </summary>
        /// <returns>Rule list.</returns>
        protected virtual IEnumerable<IMarketRule> GetNotificationRules()
        {
            yield return this.Security.WhenNewTrade( this.SafeGetConnector() );
            //return new Class190( -2 ) { hedgeStrategy_0 = this };
        }

        /// <summary>
        /// Called when the Strategy is started
        /// </summary>
        protected override void OnStarted()
        {
            base.OnStarted();
            if ( !( Connector is BaseEmulationConnector ) )
            {
                //string str = LicenseHelper.ValidateLicense("Hedge", this.Portfolio.Name, (Assembly) null);
                //if ( !StringHelper.IsEmpty( str ) )
                //{
                //    LoggingHelper.AddErrorLog( ( ILogReceiver ) this, str, new object[ 0 ] );
                //    return;
                //}
            }

            _orders.Clear();
            _securityStratgies.Clear();

            if ( _strategy == null )
            {
                _strategy = ( ( IEnumerable<Strategy> )ChildStrategies ).FirstOrDefault( s => s.Security == Security );

                if ( _strategy == null )
                {
                    _strategy = new SecurityStrategy( Security );
                    ChildStrategies.Add( _strategy );
                    LoggingHelper.AddInfoLog( this, LocalizedStrings.Str1268, new object[0] );
                }
                else
                {
                    LoggingHelper.AddInfoLog( this, StringHelper.Put( LocalizedStrings.Str1269Params, new object[1] { _strategy } ), new object[0] );
                }
            }

            _securityStratgies.Add( Security, _strategy );

            if ( BlackScholes.UnderlyingAsset == null )
            {
                BlackScholes.UnderlyingAsset = ( _strategy.Security );
                LoggingHelper.AddInfoLog( this, LocalizedStrings.Str1270, new object[0] );
            }

            BlackScholes.InnerModels.Clear();

            foreach ( Strategy childStrategy in ChildStrategies )
            {
                SecurityTypes? type = childStrategy.Security.Type;

                if ( type.GetValueOrDefault() == SecurityTypes.Option & type.HasValue && DerivativesHelper.GetAsset( childStrategy.Security, this ) == Security )
                {
                    BlackScholes.InnerModels.Add( new BlackScholes( childStrategy.Security, this, this, this.BlackScholes.ExchangeInfoProvider ) );
                    _securityStratgies.Add( childStrategy.Security, childStrategy );
                    LoggingHelper.AddInfoLog( this, StringHelper.Put( LocalizedStrings.Str1271Params, new object[1] { childStrategy } ), new object[0] );
                }
            }

            MarketRuleHelper.SuspendRules( this, new Action( mySuspendRulesAction ) );

            if ( IsRulesSuspended )
            {
                return;
            }

            lock ( _lock )
            {
                RuleSuspensionAction();
            }
        }

        /// <summary>
        /// To get a list of orders rehedging the option position.
        /// </summary>
        /// <returns>Rehedging orders.</returns>
        protected abstract IEnumerable<Order> GetReHedgeOrders();


        /// <summary>To add the rehedging strategy.</summary>
        /// <param name="parentStrategy">The parent strategy (by the strike or the underlying asset).</param>
        /// <param name="order">The rehedging order.</param>
        protected virtual void AddReHedgeQuoting( Strategy parentStrategy, Order order )
        {
            //Class189 class189 = new Class189( );
            //class189.hedgeStrategy_0 = this;
            //class189.order_0 = order;

            if ( parentStrategy == null )
            {
                throw new ArgumentNullException( nameof( parentStrategy ) );
            }

            QuotingStrategy quoting = CreateQuoting( order );

            quoting.Name = ( parentStrategy.Name + "_" + quoting.Name );

            quoting.WhenStopped().Do( ( r, s ) => RemoveOrder( order ) ).Once().Apply( parentStrategy );

            parentStrategy.ChildStrategies.Add( quoting );
        }


        /// <summary>To add the rehedging order.</summary>
        /// <param name="parentStrategy">The parent strategy (by the strike or the underlying asset).</param>
        /// <param name="order">The rehedging order.</param>
        protected virtual void AddReHedgeOrder( Strategy parentStrategy, Order order )
        {
            Action<MarketRule<Order, Order>, Order> removeOrderAction = ( r, o ) =>
            {
                LoggingHelper.AddInfoLog( parentStrategy, LocalizedStrings.Str1272Params, new object[3]
                {
                    o.TransactionId,
                    TraderHelper.IsMatched( o ) ? LocalizedStrings.Str1328 : LocalizedStrings.Str1329,
                    o.LastChangeTime
                } );

                Rules.RemoveRulesByToken( o, r );
                RemoveOrder( order );
            };


            Action<Order> logOrderAction = o => LoggingHelper.AddInfoLog( parentStrategy, LocalizedStrings.Str1275Params, o.TransactionId, o.Id, o.Time );


            Action<MarketRule<Order, OrderFail>, OrderFail> removeOrderSuspendRuleAction = ( r, o ) =>
            {
                LoggingHelper.AddErrorLog( parentStrategy, LocalizedStrings.Str1276Params, new object[2] { o.Order.TransactionId, o.Error } );

                RemoveOrder( order );
                RuleSuspensionAction();
            };

            var orderCancelRule = order.WhenMatched( this ).Or( order.WhenCanceled( this ) ).Do( removeOrderAction ).Once().Apply( parentStrategy );
            var orderSuccessfulRule = order.WhenRegistered( this ).Do( logOrderAction ).Once().Apply( parentStrategy );
            var orderFailRule = order.WhenRegisterFailed( this ).Do( removeOrderSuspendRuleAction ).Once().Apply( parentStrategy );

            orderCancelRule.Exclusive( orderFailRule );
            orderSuccessfulRule.Exclusive( orderFailRule );

            parentStrategy.RegisterOrder( order );
        }


        /// <summary>To start rehedging.</summary>
        /// <param name="orders">Rehedging orders.</param>
        protected virtual void ReHedge( IEnumerable<Order> orders )
        {
            if ( orders == null )
            {
                throw new ArgumentNullException( nameof( orders ) );
            }

            foreach ( Order order in orders )
            {
                this.AddInfoLog( LocalizedStrings.Str1277Params, ( object[ ] )new object[4] { order.Security, order.Direction, order.Volume, order.Price } );

                var parentStrategy = this._securityStratgies.TryGetValue<Security, Strategy>( order.Security );

                if ( parentStrategy == null )
                    throw new InvalidOperationException( LocalizedStrings.Str1278Params.Put( ( object[ ] )new object[1] { order.Security.Id } ) );

                if ( this.UseQuoting )
                    this.AddReHedgeQuoting( parentStrategy, order );
                else
                    this.AddReHedgeOrder( parentStrategy, order );
            }
        }


        /// <summary>Whether the rehedging is paused.</summary>
        /// <returns><see langword="true" /> if paused, otherwise, <see langword="false" />.</returns>
        protected virtual bool IsSuspended()
        {
            return !CollectionHelper.IsEmpty( _orders );
        }

        private void RuleSuspensionAction()
        {
            if ( IsSuspended() )
            {
                return;
            }

            _orders.Clear();
            var reHedgeOrders = GetReHedgeOrders();

            _orders.AddRange( reHedgeOrders );

            if ( _orders.IsEmpty() )
            {
                return;
            }

            LoggingHelper.AddInfoLog( this, LocalizedStrings.Str1279Params, new object[1] { _orders.Count } );
            ReHedge( reHedgeOrders );
        }

        private void RemoveOrder( Order order )
        {
            if ( !_orders.Remove( order ) )
            {
                return;
            }

            if ( CollectionHelper.IsEmpty( _orders ) )
            {
                LoggingHelper.AddInfoLog( this, LocalizedStrings.Str1280, new object[0] );
            }
            else
            {
                LoggingHelper.AddInfoLog( this, LocalizedStrings.Str1281Params, new object[1] { _orders.Count } );
            }
        }


        /// <summary>To create a quoting strategy to change the position.</summary>
        /// <param name="order">Quoting order.</param>
        /// <returns>The strategy of quoting.</returns>
        protected virtual QuotingStrategy CreateQuoting( Order order )
        {
            MarketQuotingStrategy marketQuotingStrategy = new MarketQuotingStrategy( order, new Unit(), new Unit() );
            marketQuotingStrategy.Volume = ( Volume );
            return marketQuotingStrategy;
        }

        private bool method_2( Strategy s )
        {
            return s.Security == Security;
        }

        private void mySuspendRulesAction()
        {
            GetNotificationRules().Or().Do( new Action( RuleSuspensionAction ) ).Apply( this );
        }


        private sealed class SecurityStrategy : Strategy
        {
            public SecurityStrategy( Security sec )
            {
                if ( sec == null )
                {
                    throw new ArgumentNullException( "asset" );
                }

                Name = sec.Id;
            }
        }
    }
}
