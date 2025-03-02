using Ecng.Common;
using StockSharp.Algo.Derivatives;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using StockSharp.Algo.Storages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


using System.Linq;

namespace StockSharp.Algo.Strategies.Derivatives
{
    /// <summary>The options delta hedging strategy.</summary>
    public class DeltaHedgeStrategy : HedgeStrategy
    {
        private readonly StrategyParam<Decimal> _positionOffset;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Algo.Strategies.Derivatives.DeltaHedgeStrategy" />.
        /// </summary>
        public DeltaHedgeStrategy( IExchangeInfoProvider exchangeInfoProvider ) : base( exchangeInfoProvider )
        {
            _positionOffset = new StrategyParam<Decimal>( this, nameof( PositionOffset ) );
        }

        /// <summary>
        /// Shift in position for underlying asset, allowing not to hedge part of the options position.
        /// </summary>
        [Display( Description = "Str1246", GroupName = "Str1244", Name = "Str1245", Order = 0, ResourceType = typeof( LocalizedStrings ) )]
        public Decimal PositionOffset
        {
            get
            {
                return _positionOffset.Value;
            }
            set
            {
                _positionOffset.Value = ( value );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<Order> GetReHedgeOrders()
        {
            Decimal? deltaTotal = BlackScholes.Delta( ( this ).CurrentTime, new Decimal?(), new Decimal?() );

            if ( !deltaTotal.HasValue )
            {
                return Enumerable.Empty<Order>();
            }

            Decimal num = MathHelper.Round( deltaTotal.Value ) + PositionOffset;
            LoggingHelper.AddInfoLog( this, LocalizedStrings.Str1247Params, new object[4]
                                                                                                    {
                                                                                                         deltaTotal,
                                                                                                          BlackScholes.UnderlyingAsset,
                                                                                                         PositionOffset,
                                                                                                         num
                                                                                                    } );
            if ( num == Decimal.Zero )
            {
                return Enumerable.Empty<Order>();
            }

            Sides sides = num > Decimal.Zero ? Sides.Sell : Sides.Buy;

            Unit currentPrice = TraderHelper.GetCurrentPrice( Security, this, new Sides?( sides ), ( MarketPriceTypes )1, null );

            if ( ( currentPrice == null ) )
            {
                return Enumerable.Empty<Order>();
            }

            Order[ ] orderArray = new Order[1];

            int index = 0;

            Order order = new Order();

            order.Direction = ( sides );
            order.Volume = ( MathHelper.Abs( num ) );
            order.Security = ( BlackScholes.UnderlyingAsset );
            order.Portfolio = ( Portfolio );
            order.Price = ( TraderHelper.ApplyOffset( currentPrice, sides, PriceOffset, Security ) );
            orderArray[index] = order;

            return orderArray;
        }
    }
}
