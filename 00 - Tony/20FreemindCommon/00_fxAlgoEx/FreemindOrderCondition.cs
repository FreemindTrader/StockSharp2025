namespace StockSharp.Algo.Testing
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    using Ecng.Collections;    
    using StockSharp.Localization;
    using StockSharp.Messages;

    /// <summary>
    /// <see cref="IMarketEmulator"/> order condition.
    /// </summary>
    [Serializable]
    [DataContract]
    
    public class FreemindOrderCondition : OrderCondition, IStopLossOrderCondition, ITakeProfitOrderCondition
    {
        /// <summary>
        /// Is take profit.
        /// </summary>
        [DataMember]
        
        public bool IsTakeProfit
        {
            get => ( bool? ) Parameters.TryGetValue( nameof( IsTakeProfit ) ) == true;
            set => Parameters[ nameof( IsTakeProfit ) ] = value;
        }

        /// <summary>
        /// Stop-price.
        /// </summary>
        [DataMember]
        
        public decimal? StopPrice
        {
            get => ( decimal? ) Parameters.TryGetValue( nameof( StopPrice ) );
            set => Parameters[ nameof( StopPrice ) ] = value;
        }

        /// <summary>
        /// Number of pips to take profit
        /// </summary>
        [DataMember]
        
        public decimal? TakeProfitPips
        {
            get => ( decimal? ) Parameters.TryGetValue( nameof( TakeProfitPips ) );
            set => Parameters[ nameof( TakeProfitPips ) ] = value;
        }


        /// <summary>
        /// Number of Pips for Stop Loss
        /// </summary>
        [DataMember]
        
        public decimal? StopLossPips
        {
            get => ( decimal? ) Parameters.TryGetValue( nameof( StopLossPips ) );
            set => Parameters[ nameof( StopLossPips ) ] = value;
        }

        /// <summary>
        /// Number of Pips for Stop Loss
        /// </summary>
        [DataMember]
        
        public bool? WithEscape
        {
            get => ( bool? ) Parameters.TryGetValue( nameof( WithEscape ) );
            set => Parameters[ nameof( WithEscape ) ] = value;
        }

        decimal? IStopLossOrderCondition.ClosePositionPrice { get; set; }
        decimal? IStopLossOrderCondition.ActivationPrice { get; set; }
        bool IStopLossOrderCondition.IsTrailing { get; set; }

        decimal? ITakeProfitOrderCondition.ClosePositionPrice { get; set; }
        decimal? ITakeProfitOrderCondition.ActivationPrice { get; set; }
        
    }
}

