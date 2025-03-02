using StockSharp.Messages;
using System;

namespace StockSharp.Algo.Strategies.Quoting
{
    /// <summary>
    /// The strategy realizing volume quoting algorithm by the limited price.
    /// </summary>
    public class LimitQuotingStrategy : QuotingStrategy
    {
        private readonly StrategyParam<decimal> _limitPrice;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Algo.Strategies.Quoting.LimitQuotingStrategy" />.
        /// </summary>
        public LimitQuotingStrategy() : this( Sides.Buy, decimal.One, decimal.Zero )
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Algo.Strategies.Quoting.LimitQuotingStrategy" />.
        /// </summary>
        /// <param name="quotingDirection">Quoting direction.</param>
        /// <param name="quotingVolume">Total quoting volume.</param>
        /// <param name="limitPrice">The limited price for quoted orders.</param>
        public LimitQuotingStrategy( Sides quotingDirection, decimal quotingVolume, decimal limitPrice ) : base( quotingDirection, quotingVolume )
        {
            _limitPrice = this.Param( nameof( LimitPrice ), limitPrice );
        }

        /// <summary>The limited price for quoted orders.</summary>
        public decimal LimitPrice
        {
            get
            {
                return _limitPrice.Value;
            }
            set
            {
                _limitPrice.Value = ( value );
            }
        }

        /// <summary>
        /// To get the best price. If it is impossible to calculate the best price at the moment, then <see langword="null" /> will be returned.
        /// </summary>
        protected override decimal? BestPrice
        {
            get
            {
                return new decimal?( LimitPrice );
            }
        }
    }
}
