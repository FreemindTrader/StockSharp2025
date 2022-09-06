using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace StockSharp.Algo.Strategies.Quoting
{
    /// <summary>The quoting by specified level in the order book.</summary>
    public class LevelQuotingStrategy : QuotingStrategy
    {
        private readonly StrategyParam<Range<int>> _level;
        private readonly StrategyParam<bool> _ownLevel;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Algo.Strategies.Quoting.LevelQuotingStrategy" />.
        /// </summary>
        public LevelQuotingStrategy() : this( Sides.Buy, decimal.One )
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Algo.Strategies.Quoting.LevelQuotingStrategy" />.
        /// </summary>
        /// <param name="quotingDirection">Quoting direction.</param>
        /// <param name="quotingVolume">Total quoting volume.</param>
        public LevelQuotingStrategy( Sides quotingDirection, decimal quotingVolume ) : base( quotingDirection, quotingVolume )
        {
            _level = this.Param( nameof( Level ), new Range<int>() );
            _ownLevel = this.Param( nameof( OwnLevel ), false );
        }

        /// <summary>
        /// The level in the order book. It specifies the number of quotes to the deep from the best one. By default, it is equal to {0:0} which means quoting by the best quote.
        /// </summary>
        public Range<int> Level
        {
            get
            {
                return _level.Value;
            }
            set
            {
                if ( value == null )
                {
                    throw new ArgumentNullException();
                }

                if ( value.Contains( -1 ) )
                {
                    throw new ArgumentOutOfRangeException();
                }

                if ( value == Level )
                {
                    return;
                }

                _level.Value = value;
            }
        }

        /// <summary>
        /// To create your own price level in the order book, if there is no quote with necessary price yet. The default is disabled.
        /// </summary>
        public bool OwnLevel
        {
            get
            {
                return _ownLevel.Value;
            }
            set
            {
                _ownLevel.Value = value;
            }
        }

        /// <summary>Should the order be quoted.</summary>
        /// <param name="currentPrice">The current price. If the value is equal to <see langword="null" /> then the order is not registered yet.</param>
        /// <param name="currentVolume">The current volume. If the value is equal to <see langword="null" /> then the order is not registered yet.</param>
        /// <param name="newVolume">New volume.</param>
        /// <returns>The price at which the order will be registered. If the value is equal to <see langword="null" /> then the quoting is not required.</returns>
        protected override decimal? NeedQuoting( decimal? currentPrice, decimal? currentVolume, decimal newVolume )
        {
            var filteredQuotes = GetFilteredQuotes( QuotingDirection );
            var minQuote = filteredQuotes != null ? filteredQuotes.ElementAtOr( Level.Min ) : null;

            if ( !minQuote.HasValue )
            {
                return null;
            }

            var maxQuote = filteredQuotes.ElementAtOr( Level.Max );
            var worstQuote = ( OwnLevel ? ( decimal )( minQuote.Value.Price + ( QuotingDirection == Sides.Sell ? 1 : -1 ) * Level.Length.Pips( Security ) ) : filteredQuotes!.Last().Price );
            var reallybadQuote = maxQuote.HasValue ? maxQuote.Value.Price : worstQuote;

            if ( QuotingDirection == Sides.Sell )
            {
                decimal minPrice = minQuote.Value.Price;

                if ( !( minPrice > currentPrice.GetValueOrDefault() & currentPrice.HasValue ) )
                {
                    if ( !( currentPrice.GetValueOrDefault() > reallybadQuote & currentPrice.HasValue ) )
                        goto label_9;
                }

                return ( ( minPrice + reallybadQuote ) / 2 );
            }
            else
            {
                if ( reallybadQuote > currentPrice.GetValueOrDefault() & currentPrice.HasValue )
                {
                    if ( currentPrice.GetValueOrDefault() > minQuote.Value.Price & currentPrice.HasValue )
                        return new decimal?( ( reallybadQuote + minQuote.Value.Price ) / 2 );
                }
            }


        label_9:
            if ( currentPrice.HasValue )
            {
                if ( !( currentVolume.GetValueOrDefault() == newVolume & currentVolume.HasValue ) )
                    return currentPrice;
            }

            return null;
        }
    }
}
