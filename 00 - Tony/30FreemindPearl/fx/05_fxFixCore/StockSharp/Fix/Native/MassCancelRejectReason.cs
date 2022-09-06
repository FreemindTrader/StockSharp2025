namespace StockSharp.Fix.Native
{
    /// <summary>
    /// </summary>
    public enum MassCancelRejectReason
    {
        /// <summary>
        /// </summary>
        MassCancelNotSupported = 0,
        /// <summary>
        /// </summary>
        InvalidOrUnknownSecurity = 1,
        /// <summary>
        /// </summary>
        InvalidOrUnknownUnderlying = 2,
        /// <summary>
        /// </summary>
        InvalidOrUnkownUnderlyingSecurity = 2,
        /// <summary>
        /// </summary>
        InvalidOrUnknownProduct = 3,
        /// <summary>
        /// </summary>
        InvalidOrUnknownCficode = 4,
        /// <summary>
        /// </summary>
        InvalidOrUnknownSecurityType = 5,
        /// <summary>
        /// </summary>
        InvalidOrUnknownTradingSession = 6,
        /// <summary>
        /// </summary>
        InvalidOrUnknownMarket = 7,
        /// <summary>
        /// </summary>
        InvalidOrUnkownMarketSegment = 8,
        /// <summary>
        /// </summary>
        InvalidOrUnknownSecurityGroup = 9,
        /// <summary>
        /// </summary>
        InvalidOrUnknownSecurityIssuer = 10, // 0x0000000A
        /// <summary>
        /// </summary>
        InvalidOrUnknownIssuerOfUnderlyingSecurity = 11, // 0x0000000B
        /// <summary>
        /// </summary>
        Other = 99, // 0x00000063
    }
}
