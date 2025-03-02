using StockSharp.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace StockSharp.Fix
{
    /// <summary>
    /// The stop orders types that are specific for <see cref="N:StockSharp.Fix" />.
    /// </summary>
    [DataContract]
    [Serializable]
    public enum FixStopOrderTypes
    {
        /// <summary>Stop-loss.</summary>
        [Display(Name = "StopLoss", ResourceType = typeof(LocalizedStrings)), EnumMember] StopLoss,
        /// <summary>Trailing stop-loss.</summary>
        [EnumMember, Display(Name = "TrailingStopLoss", ResourceType = typeof(LocalizedStrings))] StopLossTrailing,
        /// <summary>Take.</summary>
        [EnumMember, Display(Name = "TakeProfit", ResourceType = typeof(LocalizedStrings))] TakeProfit,
        /// <summary>Trailing take-profit.</summary>
        [Display(Name = "TrailingTakeProfit", ResourceType = typeof(LocalizedStrings)), EnumMember] TakeProfitTrailing,
        /// <summary>Market on close (pit).</summary>
        [Display(Name = "MarketOnPitClose", ResourceType = typeof(LocalizedStrings)), EnumMember] MarketOnClose,
        /// <summary>
        /// With the market price when the condition is fulfilled.
        /// </summary>
        [Display(Name = "MarketOnTouch", ResourceType = typeof(LocalizedStrings)), EnumMember] MarketIfTouched,
    }
}
