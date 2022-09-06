using StockSharp.Messages;
using System;

namespace StockSharp.Algo.Strategies.Protective
{
    /// <summary>Protective strategy base interface.</summary>
    public interface IProtectiveStrategy
    {
        /// <summary>Protected volume.</summary>
        Decimal ProtectiveVolume { get; set; }

        /// <summary>Protected position price.</summary>
        Decimal ProtectivePrice { get; }

        /// <summary>Protected position side.</summary>
        Sides ProtectiveSide { get; }

        /// <summary>The protected volume change event.</summary>
        event Action ProtectiveVolumeChanged;
    }
}
