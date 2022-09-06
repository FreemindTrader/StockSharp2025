using StockSharp.Algo.Strategies;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;

namespace StockSharp.Algo.Strategies.Quoting
{
    /// <summary>
    /// The quoting by the best price. For this quoting the shift from the best price <see cref="P:StockSharp.Algo.Strategies.Quoting.BestByPriceQuotingStrategy.BestPriceOffset" /> is specified, 
    /// on which quoted order can be changed.
    /// </summary>
    public class BestByPriceQuotingStrategy : QuotingStrategy
    {
        private readonly StrategyParam<Unit> _bestPriceOffset;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Algo.Strategies.Quoting.BestByPriceQuotingStrategy" />.
        /// </summary>
        public BestByPriceQuotingStrategy()
          : this( Sides.Buy, Decimal.One )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Algo.Strategies.Quoting.BestByPriceQuotingStrategy" />.
        /// </summary>
        /// <param name="quotingDirection">Quoting direction.</param>
        /// <param name="quotingVolume">Total quoting volume.</param>
        public BestByPriceQuotingStrategy( Sides quotingDirection, Decimal quotingVolume ) : base( quotingDirection, quotingVolume )
        {
            _bestPriceOffset = this.Param( nameof( BestPriceOffset ), new Unit() );
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Algo.Strategies.Quoting.BestByPriceQuotingStrategy" />.
        /// </summary>
        /// <param name="order">Quoting order.</param>
        /// <param name="bestPriceOffset">The shift from the best price, on which quoted order can be changed.</param>
        public BestByPriceQuotingStrategy( Order order, Unit bestPriceOffset ) : base( order )
        {
            _bestPriceOffset = this.Param( nameof( BestPriceOffset ), bestPriceOffset );
        }


        /// <summary>
        /// The shift from the best price, on which quoted order can be changed.
        /// </summary>
        public Unit BestPriceOffset
        {
            get
            {
                return _bestPriceOffset.Value;
            }

            set
            {
                _bestPriceOffset.Value = value;
            }
        }


        /// <summary>Should the order be quoted.</summary>
        /// <param name="currentPrice">The current price. If the value is equal to <see langword="null" /> then the order is not registered yet.</param>
        /// <param name="currentVolume">The current volume. If the value is equal to <see langword="null" /> then the order is not registered yet.</param>
        /// <param name="newVolume">New volume.</param>
        /// <returns>The price at which the order will be registered. If the value is equal to <see langword="null" /> then the quoting is not required.</returns>
        protected override Decimal? NeedQuoting( Decimal? currentPrice, Decimal? currentVolume, Decimal newVolume )
        {
            if ( !BestPrice.HasValue )
            {
                return null;
            }

            if ( currentPrice.HasValue )
            {
                Unit? bestPrice = BestPrice ?? null;
                Unit offset = BestPriceOffset;

                Decimal plusOffset = ( decimal )bestPrice + ( decimal )offset;

                if ( !( plusOffset < currentPrice.Value & currentPrice.HasValue ) )
                {
                    bestPrice = BestPrice.HasValue ? BestPrice.GetValueOrDefault() : null;
                    offset = BestPriceOffset;

                    Decimal minuOffset = ( decimal )bestPrice - ( decimal )offset;

                    if ( !( minuOffset > currentPrice.Value & currentPrice.HasValue ) )
                    {
                        if ( !( currentVolume.GetValueOrDefault() == newVolume & currentVolume.HasValue ) )
                        {
                            return currentPrice;
                        }

                        return null;
                    }
                }
            }

            Decimal offsetAvg = ( QuotingDirection == Sides.Sell ? 1 : -1 ) * ( decimal )BestPriceOffset / 2;

            if ( BestPrice.HasValue )
            {
                return new Decimal?( BestPrice.GetValueOrDefault() + offsetAvg );
            }

            return null;
        }
    }
}
