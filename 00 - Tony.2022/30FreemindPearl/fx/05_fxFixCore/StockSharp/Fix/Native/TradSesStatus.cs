namespace StockSharp.Fix.Native
{
    /// <summary>Session states.</summary>
    public enum TradSesStatus
    {
        /// <summary>Unknown.</summary>
        Unknown,
        /// <summary>Suspended.</summary>
        Halted,
        /// <summary>Opened.</summary>
        Open,
        /// <summary>Closed.</summary>
        Closed,
        /// <summary>Open pending.</summary>
        PreOpen,
        /// <summary>Close pending.</summary>
        PreClose,
        /// <summary>Session state request error.</summary>
        RequestRejected,
    }
}
