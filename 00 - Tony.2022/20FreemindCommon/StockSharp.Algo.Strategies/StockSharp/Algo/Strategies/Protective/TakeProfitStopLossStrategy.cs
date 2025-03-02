using MoreLinq;
using StockSharp.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Algo.Strategies.Protective
{
    /// <summary>
    /// The strategy protecting trades together by strategies <see cref="T:StockSharp.Algo.Strategies.Protective.TakeProfitStrategy" /> 
    /// and <see cref="T:StockSharp.Algo.Strategies.Protective.StopLossStrategy" />.
    /// </summary>
    public class TakeProfitStopLossStrategy : Strategy, IProtectiveStrategy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Algo.Strategies.Protective.TakeProfitStopLossStrategy" />.
        /// </summary>
        /// <param name="takeProfit">Profit protection strategy.</param>
        /// <param name="stopLoss">The loss protection strategy.</param>
        /// 
        public TakeProfitStopLossStrategy( TakeProfitStrategy takeProfit, StopLossStrategy stopLoss ) : base()
        {
            if ( takeProfit == null )
            {
                throw new ArgumentNullException( nameof( takeProfit ) );
            }

            if ( stopLoss == null )
            {
                throw new ArgumentNullException( nameof( stopLoss ) );
            }

            takeProfit.WhenActivated().Do( () => stopLoss.Stop() ).Apply( this );
            ChildStrategies.Add( takeProfit );

            stopLoss.WhenActivated().Do( () => takeProfit.Stop() ).Apply( this );
            ChildStrategies.Add( stopLoss );
        }

        /// <summary>Protected volume.</summary>
        public decimal ProtectiveVolume
        {
            get
            {
                var strategy = ( IProtectiveStrategy )( ( IEnumerable<Strategy> )ChildStrategies ).First();
                return strategy.ProtectiveVolume;
            }

            set
            {
                ChildStrategies.OfType<IProtectiveStrategy>().ForEach( s => s.ProtectiveVolume = value );

                if ( ProtectiveVolumeChanged == null )
                {
                    return;
                }

                ProtectiveVolumeChanged();
            }
        }


        /// <summary>Protected position price.</summary>
        public decimal ProtectivePrice
        {
            get
            {
                var strategy = ( IProtectiveStrategy )( ( IEnumerable<Strategy> )ChildStrategies ).First();
                return strategy.ProtectivePrice;
            }
        }

        /// <summary>Protected position side.</summary>
        public Sides ProtectiveSide
        {
            get
            {
                var strategy = ( IProtectiveStrategy )( ( IEnumerable<Strategy> )ChildStrategies ).First();
                return strategy.ProtectiveSide;
            }
        }

        /// <summary>The protected volume change event.</summary>
        public event Action? ProtectiveVolumeChanged;
    }
}
