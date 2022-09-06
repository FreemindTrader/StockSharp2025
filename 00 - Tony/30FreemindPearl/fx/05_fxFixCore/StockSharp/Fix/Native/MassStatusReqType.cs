namespace StockSharp.Fix.Native
{
    /// <summary>
    /// </summary>
    public enum MassStatusReqType
    {
        /// <summary>
        /// </summary>
        StatusForOrdersForASecurity = 1,
        /// <summary>
        /// </summary>
        StatusForOrdersForAnUnderlyingSecurity = 2,
        /// <summary>
        /// </summary>
        StatusForOrdersForAProduct = 3,
        /// <summary>
        /// </summary>
        StatusForOrdersForACfiCode = 4,
        /// <summary>
        /// </summary>
        StatusForOrdersForASecurityType = 5,
        /// <summary>
        /// </summary>
        StatusForOrdersForATradingSession = 6,
        /// <summary>
        /// </summary>
        StatusForAllOrders = 7,
        /// <summary>
        /// </summary>
        StatusForOrdersForAPartyid = 8,
        /// <summary>
        /// </summary>
        StatusForSecurityIssuer = 9,
        /// <summary>
        /// </summary>
        StatusForIssuerOfUnderlyingSecurity = 10, // 0x0000000A
    }
}
