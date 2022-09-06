using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;

namespace StockSharp.Algo.Strategies.Quoting
{
    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Algo.Strategies.Quoting.LastTradeQuotingStrategy" />.
    /// </summary>
    public class LastTradeQuotingStrategy : BestByPriceQuotingStrategy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Algo.Strategies.Quoting.LastTradeQuotingStrategy" />.
        /// </summary>
        public LastTradeQuotingStrategy()
          : this( Sides.Buy, Decimal.One )
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Algo.Strategies.Quoting.LastTradeQuotingStrategy" />.
        /// </summary>
        /// <param name="quotingDirection">Quoting direction.</param>
        /// <param name="quotingVolume">Total quoting volume.</param>
        public LastTradeQuotingStrategy( Sides quotingDirection, Decimal quotingVolume )
          : base( quotingDirection, quotingVolume )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Algo.Strategies.Quoting.LastTradeQuotingStrategy" />.
        /// </summary>
        /// <param name="order">Quoting order.</param>
        /// <param name="bestPriceOffset">The shift from the best price, on which the quoted order can be changed.</param>
        /// <returns>Strategy.</returns>
        public LastTradeQuotingStrategy( Order order, Unit bestPriceOffset )
          : base( order, bestPriceOffset )
        {
        }

        /// <summary>
        /// To get the best price. If it is impossible to calculate the best price at the moment, then <see langword="null" /> will be returned.
        /// </summary>
        protected override Decimal? BestPrice
        {
            get
            {
                return this.LastTradePrice;
            }
        }
    }
}
