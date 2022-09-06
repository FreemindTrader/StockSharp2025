using Ecng.ComponentModel;
using StockSharp.Algo.Derivatives;
using StockSharp.Algo.Strategies.Quoting;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Logging;
using StockSharp.Messages;
using System;

namespace StockSharp.Algo.Strategies.Derivatives
{
    /// <summary>Option volatility quoting.</summary>
    public class VolatilityQuotingStrategy : BestByPriceQuotingStrategy
    {
        private readonly IExchangeInfoProvider _exchangeProvider;
        private BlackScholes? _blackScholes;
        private readonly StrategyParam<Range<decimal>> _ivRange;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Algo.Strategies.Derivatives.VolatilityQuotingStrategy" />.
        /// </summary>
        /// <param name="quotingDirection">Quoting direction.</param>
        /// <param name="quotingVolume">Total quoting volume.</param>
        /// <param name="ivRange">Volatility range.</param>
        /// <param name="exchangeInfoProvider">Exchanges and trading boards provider.</param>
        public VolatilityQuotingStrategy( Sides quotingDirection, decimal quotingVolume, Range<decimal> ivRange, IExchangeInfoProvider exchangeInfoProvider )
          : base( quotingDirection, quotingVolume )
        {
            _exchangeProvider = exchangeInfoProvider;
            _ivRange = this.Param<Range<Decimal>>( nameof( IVRange ), ivRange );
        }

        /// <summary>Volatility range.</summary>
        public Range<decimal> IVRange
        {
            get
            {
                return _ivRange.Value;
            }
            set
            {
                _ivRange.Value = ( value );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override Security Security
        {
            set
            {
                _blackScholes = new BlackScholes( value, this, this, _exchangeProvider );
                base.Security = ( value );
            }
        }


        /// <summary>Should the order be quoted.</summary>
        /// <param name="currentPrice">The current price. If the value is equal to <see langword="null" /> then the order is not registered yet.</param>
        /// <param name="currentVolume">The current volume. If the value is equal to <see langword="null" /> then the order is not registered yet.</param>
        /// <param name="newVolume">New volume.</param>
        /// <returns>The price at which the order will be registered. If the value is equal to <see langword="null" /> then the quoting is not required.</returns>
        protected override decimal? NeedQuoting( decimal? currentPrice, decimal? currentVolume, decimal newVolume )
        {
            if ( _blackScholes == null )
                return null;

            decimal? lowestPremium = _blackScholes.Premium( CurrentTime, new decimal?( IVRange.Min / new decimal( 100 ) ), new decimal?() );
            if ( !lowestPremium.HasValue )
            {
                return null;
            }

            decimal? highestPremium = _blackScholes.Premium( CurrentTime, new decimal?( IVRange.Max / new decimal( 100 ) ), new decimal?() );
            if ( !highestPremium.HasValue )
            {
                return null;
            }

            if ( currentPrice.HasValue )
            {
                if ( !( currentPrice.GetValueOrDefault() < lowestPremium.GetValueOrDefault() & ( currentPrice.HasValue & lowestPremium.HasValue ) ) )
                {
                    if ( !( currentPrice.GetValueOrDefault() > highestPremium.GetValueOrDefault() & ( currentPrice.HasValue & highestPremium.HasValue ) ) )
                    {
                        if ( !( currentVolume.GetValueOrDefault() == newVolume & currentVolume.HasValue ) )
                        {
                            return currentPrice;
                        }

                        return null;
                    }
                }
            }

            var total = lowestPremium.HasValue & highestPremium.HasValue ? new decimal?( lowestPremium.GetValueOrDefault() + highestPremium.GetValueOrDefault() ) : new decimal?();

            if ( total.HasValue )
            {
                return new decimal?( total.GetValueOrDefault() / 2 );
            }

            return null;
        }
    }
}
