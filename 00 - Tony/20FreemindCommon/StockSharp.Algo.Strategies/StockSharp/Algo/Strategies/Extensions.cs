using Ecng.Common;
using StockSharp.Algo.Strategies.Protective;
using StockSharp.Algo.Strategies.Quoting;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;

namespace StockSharp.Algo.Strategies
{
    /// <summary>
    /// Extension class for <see cref="T:StockSharp.Algo.Strategies.Strategy" />.
    /// </summary>
    public static class Extensions
    {
        /// <summary>To open the position via quoting.</summary>
        /// <param name="strategy">Strategy.</param>
        /// <param name="finishPosition">The position value that should be reached. A negative value means the short position.</param>
        public static void OpenPositionByQuoting( this Strategy strategy, decimal finishPosition )
        {
            if ( strategy == null )
            {
                throw new ArgumentNullException( nameof( strategy ) );
            }

            decimal position = strategy.Position;
            if ( finishPosition == position )
            {
                return;
            }

            decimal quotingVolume = MathHelper.Abs( finishPosition - position );
            var marketQuotingStrategy = new MarketQuotingStrategy( finishPosition < position ? Sides.Sell : Sides.Buy, quotingVolume );

            strategy.ChildStrategies.Add( marketQuotingStrategy );
        }


        /// <summary>To close the open position via quoting.</summary>
        /// <param name="strategy">Strategy.</param>
        public static void ClosePositionByQuoting( this Strategy strategy )
        {
            if ( strategy == null )
            {
                throw new ArgumentNullException( nameof( strategy ) );
            }

            decimal position = strategy.Position;
            if ( position == decimal.Zero )
            {
                return;
            }

            MarketQuotingStrategy marketQuotingStrategy = new MarketQuotingStrategy( position > decimal.Zero ? Sides.Sell : Sides.Buy, MathHelper.Abs( position ) );
            ( ( ICollection<Strategy> )strategy.ChildStrategies ).Add( marketQuotingStrategy );
        }


        /// <summary>
        /// To create the action protecting orders by strategies <see cref="T:StockSharp.Algo.Strategies.Protective.TakeProfitStrategy" /> 
        /// and <see cref="T:StockSharp.Algo.Strategies.Protective.StopLossStrategy" />.
        /// </summary>
        /// <param name="rule">The rule for new orders.</param>
        /// <param name="takePriceDelta">The delta from the price of the protected order, by which the protective take profit order is to be registered.</param>
        /// <param name="stopPriceDelta">The delta from the price of the protected order, by which the protective stop loss order is to be registered.</param>
        /// <returns>Rule.</returns>
        public static MarketRule<Order, MyTrade> Protect( this MarketRule<Order, MyTrade> rule, Unit takePriceDelta, Unit stopPriceDelta )
        {
            return rule.Protect( tp => new TakeProfitStrategy( tp, takePriceDelta ), sl => new StopLossStrategy( sl, stopPriceDelta ) );
        }


        /// <summary>
        /// To create the action protecting orders by strategies <see cref="T:StockSharp.Algo.Strategies.Protective.TakeProfitStrategy" /> and <see cref="T:StockSharp.Algo.Strategies.Protective.StopLossStrategy" />.
        /// </summary>
        /// <param name="rule">The rule for new orders.</param>
        /// <param name="takeProfit">The function that creates the strategy <see cref="T:StockSharp.Algo.Strategies.Protective.TakeProfitStrategy" /> by the order.</param>
        /// <param name="stopLoss">The function that creates the strategy <see cref="T:StockSharp.Algo.Strategies.Protective.StopLossStrategy" /> by the order.</param>
        /// <returns>Rule.</returns>
        public static MarketRule<Order, MyTrade> Protect( this MarketRule<Order, MyTrade> rule, Func<MyTrade, TakeProfitStrategy> takeProfit, Func<MyTrade, StopLossStrategy> stopLoss )
        {
            if ( rule == null )
            {
                throw new ArgumentNullException( nameof( rule ) );
            }

            if ( takeProfit == null && stopLoss == null )
            {
                throw new ArgumentException( LocalizedStrings.Str1248 );
            }

            Func<MyTrade, Strategy> getStrategy = tr =>
            {
                if ( takeProfit != null && stopLoss != null )
                {
                    return new TakeProfitStopLossStrategy( takeProfit( tr ), stopLoss( tr ) );
                }

                if ( takeProfit != null )
                {
                    return takeProfit( tr );
                }

                if ( stopLoss != null )
                {
                    return stopLoss( tr );
                }

                throw new ArgumentNullException( "Both Stop Loss and Take Profit strategies are null" );
            };

            Action<MyTrade> takeProtection = ( tr => ( ( ICollection<Strategy> )GetStrategyFromRule( rule ).ChildStrategies ).Add( getStrategy( tr ) ) );


            rule.Do( takeProtection );
            return rule;
        }


        private static Strategy GetStrategyFromRule( IMarketRule rule )
        {
            if ( rule == null )
            {
                throw new ArgumentNullException( "rule" );
            }

            Strategy? container = rule.Container as Strategy;

            if ( container == null )
            {
                throw new ArgumentException( StringHelper.Put( LocalizedStrings.Str1263Params, rule.Name ), "rule" );
            }

            return container;
        }


        /// <summary>
        /// To create the rule for the event <see cref="E:StockSharp.Algo.Strategies.Protective.ProtectiveStrategy.Activated" />.
        /// </summary>
        /// <param name="strategy">The strategy, by which the event will be monitored.</param>
        /// <returns>Rule.</returns>
        public static IMarketRule WhenActivated( this ProtectiveStrategy strategy )
        {
            return new ProtectiveStrategyActivationRule( strategy );
        }


        private sealed class ProtectiveStrategyActivationRule : MarketRule<ProtectiveStrategy, ProtectiveStrategy>
        {
            private readonly ProtectiveStrategy _strategy;

            public ProtectiveStrategyActivationRule( ProtectiveStrategy newStrategy ) : base( newStrategy )
            {
                Name = LocalizedStrings.Str1082;
                _strategy = newStrategy;
                _strategy.Activated += new Action( Activate );
            }

            protected override void DisposeManaged()
            {
                _strategy.Activated -= new Action( Activate );
                base.DisposeManaged();
            }
        }
    }
}
