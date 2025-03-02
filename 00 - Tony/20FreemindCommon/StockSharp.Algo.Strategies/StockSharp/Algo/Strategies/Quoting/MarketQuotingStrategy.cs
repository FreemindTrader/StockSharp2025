using Ecng.Collections;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StockSharp.Algo.Strategies.Quoting
{
    /// <summary>The quoting by the market price.</summary>
    public class MarketQuotingStrategy : BestByPriceQuotingStrategy
    {

        private readonly StrategyParam<MarketPriceTypes> _marketPriceType;

        private readonly StrategyParam<Unit> _unit;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Algo.Strategies.Quoting.MarketQuotingStrategy" />.
        /// </summary>
        public MarketQuotingStrategy() : this( Sides.Buy, decimal.One )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Algo.Strategies.Quoting.MarketQuotingStrategy" />.
        /// </summary>
        /// <param name="quotingDirection">Quoting direction.</param>
        /// <param name="quotingVolume">Total quoting volume.</param>
        public MarketQuotingStrategy( Sides quotingDirection, decimal quotingVolume ) : base( quotingDirection, quotingVolume )
        {
            _marketPriceType = this.Param( nameof( PriceType ), MarketPriceTypes.Following );
            _unit = this.Param( nameof( PriceOffset ), new Unit() );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Algo.Strategies.Quoting.MarketQuotingStrategy" />.
        /// </summary>
        /// <param name="order">Quoting order.</param>
        /// <param name="bestPriceOffset">The shift from the best price, on which the quoted order can be changed.</param>
        /// <param name="priceOffset">The price shift for the registering order. It determines the amount of shift from the best quote (for the buy it is added to the price, for the sell it is subtracted).</param>
        /// <returns>Strategy.</returns>
        public MarketQuotingStrategy( Order order, Unit bestPriceOffset, Unit priceOffset ) : base( order, bestPriceOffset )
        {
            _marketPriceType = this.Param( nameof( PriceType ), MarketPriceTypes.Following );
            _unit = this.Param( nameof( PriceOffset ), priceOffset );
            UseLastTradePrice = false;
        }

        /// <summary>
        /// The market price type. The default value is <see cref="F:StockSharp.Algo.MarketPriceTypes.Following" />.
        /// </summary>
        public MarketPriceTypes PriceType
        {
            get
            {
                return _marketPriceType.Value;
            }
            set
            {
                _marketPriceType.Value = value;
            }
        }

        /// <summary>
        /// The price shift for the registering order. It determines the amount of shift from the best quote (for the buy it is added to the price, for the sell it is subtracted).
        /// </summary>
        public Unit PriceOffset
        {
            get
            {
                return _unit.Value;
            }
            set
            {
                _unit.Value = value;
            }
        }

        /// <inheritdoc />
        protected override decimal? BestPrice
        {
            get
            {
                Unit? unit;
                Unit? offset;
                switch ( PriceType )
                {
                    case MarketPriceTypes.Opposite:
                        {
                            QuoteChange[ ]? filteredQuotes1 = GetFilteredQuotes( QuotingDirection.Invert() );
                            QuoteChange? bestQuote = filteredQuotes1 != null ? ( ( IEnumerable<QuoteChange> )filteredQuotes1 ).FirstOr() : new QuoteChange?();
                            decimal? bestQuoteVal = bestQuote.HasValue ? new decimal?( bestQuote.GetValueOrDefault().Price ) : LastTradePrice;
                            unit = bestQuoteVal.HasValue ? bestQuoteVal.GetValueOrDefault() : null;
                            offset = -PriceOffset;
                        }
                        break;

                    case MarketPriceTypes.Following:
                        {
                            decimal? bestPrice = base.BestPrice;
                            unit = bestPrice.HasValue ? bestPrice.GetValueOrDefault() : null;
                            offset = PriceOffset;
                        }
                        break;

                    case MarketPriceTypes.Middle:
                        {
                            QuoteChange[ ]? bestBuy = GetFilteredQuotes( Sides.Buy );
                            QuoteChange? bestBuyVal = bestBuy != null ? ( ( IEnumerable<QuoteChange> )bestBuy ).FirstOr() : new QuoteChange?();
                            QuoteChange[ ]? bestSell = GetFilteredQuotes( Sides.Sell );
                            QuoteChange? bestSellVal = bestSell != null ? ( ( IEnumerable<QuoteChange> )bestSell ).FirstOr() : new QuoteChange?();
                            unit = ( !bestBuyVal.HasValue || !bestSellVal.HasValue ) ? null : ( Unit )( bestBuyVal.Value.Price + ( bestSellVal.Value.Price - bestBuyVal.Value.Price ) / new decimal( 2 ) );
                            offset = null;
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                if ( unit == null ) return null;

                unit.SetSecurity( Security );

                if ( offset != null )
                    unit = unit.ApplyOffset( QuotingDirection, offset, Security );

                return new decimal?( ( decimal )unit );
            }
        }
    }
}
