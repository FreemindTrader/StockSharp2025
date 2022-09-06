using Ecng.Collections;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace StockSharp.Fix
{
    /// <summary>
    /// <see cref="N:StockSharp.Fix" /> order condition.
    ///     </summary>
    [DisplayNameLoc("Str2264", "FIX")]
    [DataContract]
    [Serializable]
    public class FixOrderCondition : OrderCondition, IStopLossOrderCondition, ITakeProfitOrderCondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Fix.FixOrderCondition" />.
        /// </summary>
        public FixOrderCondition() => Type = new FixStopOrderTypes?(FixStopOrderTypes.StopLoss);

        /// <summary>Stop-order type.</summary>
        [DataMember]
        [Display(Description = "Str1691", GroupName = "Str225", Name = "Type", Order = 0, ResourceType = typeof(LocalizedStrings))]
        public FixStopOrderTypes? Type
        {
            get => (FixStopOrderTypes?)Parameters.TryGetValue(nameof(Type));
            set => Parameters[nameof(Type)] = value;
        }

        /// <summary>Stop-price.</summary>
        [Display(Description = "Str1693", GroupName = "Str225", Name = "StopPrice", Order = 1, ResourceType = typeof(LocalizedStrings))]
        [DataMember]
        public decimal? StopPrice
        {
            get => (decimal?)Parameters.TryGetValue(nameof(StopPrice));
            set => Parameters[nameof(StopPrice)] = value;
        }

        /// <summary>Offset.</summary>
        [DataMember]
        public decimal? Offset
        {
            get => (decimal?)Parameters.TryGetValue(nameof(Offset));
            set => Parameters[nameof(Offset)] = value;
        }

        decimal? IStopLossOrderCondition.ActivationPrice
        {
            get => StopPrice;
            set => StopPrice = value;
        }

        decimal? IStopLossOrderCondition.ClosePositionPrice
        {
            get => Offset;
            set => Offset = value;
        }

        decimal? ITakeProfitOrderCondition.ActivationPrice
        {
            get => StopPrice;
            set => StopPrice = value;
        }

        decimal? ITakeProfitOrderCondition.ClosePositionPrice
        {
            get => Offset;
            set => Offset = value;
        }

        bool ITakeProfitOrderCondition.IsTrailing
        {
            get
            {
                return Type.GetValueOrDefault() == FixStopOrderTypes.TakeProfitTrailing & Type.HasValue;
            }

            set
            {
                Type = new FixStopOrderTypes?(value ? FixStopOrderTypes.TakeProfitTrailing : FixStopOrderTypes.TakeProfit);
            }
        }



        bool IStopLossOrderCondition.IsTrailing
        {
            get
            {
                return Type.GetValueOrDefault() == FixStopOrderTypes.StopLossTrailing & Type.HasValue;
            }

            set
            {
                Type = new FixStopOrderTypes?(value ? FixStopOrderTypes.StopLossTrailing : FixStopOrderTypes.StopLoss);
            }
        }
    }
}
