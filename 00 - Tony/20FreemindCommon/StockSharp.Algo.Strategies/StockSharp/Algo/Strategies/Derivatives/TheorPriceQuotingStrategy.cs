using Ecng.ComponentModel;
using StockSharp.Algo.Strategies.Quoting;
using StockSharp.Messages;
using System;

namespace StockSharp.Algo.Strategies.Derivatives
{
    /// <summary>Option theoretical price quoting.</summary>
    public class TheorPriceQuotingStrategy : BestByPriceQuotingStrategy
    {
        private readonly StrategyParam<Range<Unit>> strategyParam_9;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Algo.Strategies.Derivatives.TheorPriceQuotingStrategy" />.
        /// </summary>
        /// <param name="quotingDirection">Quoting direction.</param>
        /// <param name="quotingVolume">Total quoting volume.</param>
        /// <param name="theorPriceOffset">Theoretical price offset.</param>
        public TheorPriceQuotingStrategy( Sides quotingDirection, decimal quotingVolume, Range<Unit> theorPriceOffset )
          : base( quotingDirection, quotingVolume )
        {
            strategyParam_9 = this.Param<Range<Unit>>( nameof( TheorPriceOffset ), theorPriceOffset );
        }

        /// <summary>Theoretical price offset.</summary>
        public Range<Unit> TheorPriceOffset
        {
            get
            {
                return strategyParam_9.Value;
            }
            set
            {
                strategyParam_9.Value = ( value );
            }
        }


        /// <summary>Should the order be quoted.</summary>
        /// <param name="currentPrice">The current price. If the value is equal to <see langword="null" /> then the order is not registered yet.</param>
        /// <param name="currentVolume">The current volume. If the value is equal to <see langword="null" /> then the order is not registered yet.</param>
        /// <param name="newVolume">New volume.</param>
        /// <returns>The price at which the order will be registered. If the value is equal to <see langword="null" /> then the quoting is not required.</returns>
        protected override decimal? NeedQuoting( decimal? currentPrice, decimal? currentVolume, decimal newVolume )
        {
            decimal? securityValue = this.GetSecurityValue<decimal?>( Level1Fields.TheorPrice );
            if ( !securityValue.HasValue )
            {
                return new decimal?();
            }

            if ( currentPrice.HasValue )
            {
                Unit? currentPriceValue = currentPrice.HasValue ? currentPrice.GetValueOrDefault() : null;
                Unit? value2 = securityValue.Value + TheorPriceOffset.Min;

                if ( !( currentPriceValue < value2 ) )
                {
                    currentPriceValue = currentPrice.HasValue ? currentPrice.GetValueOrDefault() : null;
                    value2 = securityValue.Value + TheorPriceOffset.Max;

                    if ( !( currentPriceValue > value2 ) )
                    {
                        if ( !( currentVolume.GetValueOrDefault() == newVolume & currentVolume.HasValue ) )
                        {
                            return currentPrice;
                        }

                        return new decimal?();
                    }
                }
            }

            var output = securityValue.Value + TheorPriceOffset.Min + TheorPriceOffset.Length / 2;

            return ( ( decimal )output );
        }
    }
}
