using Ecng.Collections;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Diagnostics;

namespace StockSharp.Algo.Strategies.Quoting
{
    /// <summary>
    /// The quoting according to the Best By Volume rule. For this quoting the volume delta <see cref="P:StockSharp.Algo.Strategies.Quoting.BestByVolumeQuotingStrategy.VolumeExchange" /> is specified, which can stand in front of the quoted order.
    /// </summary>
    public class BestByVolumeQuotingStrategy : QuotingStrategy
    {

        private readonly StrategyParam<Unit> _volumeExchange;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Algo.Strategies.Quoting.BestByVolumeQuotingStrategy" />.
        /// </summary>
        public BestByVolumeQuotingStrategy()
          : this( Sides.Buy, decimal.One )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Algo.Strategies.Quoting.BestByVolumeQuotingStrategy" />.
        /// </summary>
        /// <param name="quotingDirection">Quoting direction.</param>
        /// <param name="quotingVolume">Total quoting volume.</param>
        public BestByVolumeQuotingStrategy( Sides quotingDirection, decimal quotingVolume )
          : base( quotingDirection, quotingVolume )
        {
            _volumeExchange = new StrategyParam<Unit>( this, nameof( VolumeExchange ), new Unit() );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Algo.Strategies.Quoting.BestByVolumeQuotingStrategy" />.
        /// </summary>
        /// <param name="order">Quoting order.</param>
        /// <param name="volumeExchange">The volume delta that can stand in front of the quoted order.</param>
        /// <returns>Strategy.</returns>
        public BestByVolumeQuotingStrategy( Order order, Unit volumeExchange )
          : base( order )
        {
            _volumeExchange = new StrategyParam<Unit>( this, nameof( VolumeExchange ), volumeExchange );
        }

        /// <summary>
        /// The volume delta that can stand in front of the quoted order.
        /// </summary>
        public Unit VolumeExchange
        {
            get
            {
                return _volumeExchange.Value;
            }
            set
            {
                _volumeExchange.Value = value;
            }
        }

        /// <inheritdoc />
        protected override decimal? NeedQuoting( decimal? currentPrice, decimal? currentVolume, decimal newVolume )
        {
            QuoteChange[ ]? filteredQuotes = GetFilteredQuotes( QuotingDirection );
            if ( filteredQuotes == null || filteredQuotes.IsEmpty<QuoteChange>() && !currentPrice.HasValue )
            {
                this.AddWarningLog( LocalizedStrings.Str1241 );
                return new decimal?();
            }
            int sign = QuotingDirection == Sides.Buy ? 1 : -1;
            decimal totalVolume = new decimal();
            //Decimal? signedCurrent;

            foreach ( QuoteChange quoteChange in filteredQuotes )
            {
                decimal signedChangedPrice = quoteChange.Price * sign;

                var signedCurrent = currentPrice.HasValue ? new decimal?( currentPrice.Value * sign ) : new decimal?();

                if ( signedChangedPrice > signedCurrent & signedCurrent.HasValue )
                {
                    totalVolume += quoteChange.Volume;
                    if ( totalVolume > VolumeExchange )
                        return new decimal?( quoteChange.Price );
                }
                else
                    break;
            }

            if ( currentPrice.HasValue )
            {
                if ( !( currentVolume.GetValueOrDefault() == newVolume & currentVolume.HasValue ) )
                    return currentPrice;
            }

            return new decimal?();
        }
    }
}
