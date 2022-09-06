namespace StockSharp.Fix.Native
{
    /// <summary>Session state request error message.</summary>
    public enum TradSesStatusRejReason
    {
        /// <summary>Unknown session.</summary>
        InvalidSession = 1,
        /// <summary>Other.</summary>
        Other = 99, // 0x00000063
    }
}
