namespace StockSharp.Fix.Native
{
    /// <summary>Quotes cancel types.</summary>
    public enum QuoteCancelType
    {
        /// <summary>
        /// </summary>
        CancelForOneOrMoreSecurities = 1,
        /// <summary>
        /// </summary>
        CancelForSecurityType = 2,
        /// <summary>
        /// </summary>
        CancelForUnderlyingSecurity = 3,
        /// <summary>
        /// </summary>
        CancelAllQuotes = 4,
        /// <summary>
        /// </summary>
        CancelQuoteSpecifiedInQuoteid = 5,
        /// <summary>
        /// </summary>
        CancelByQuotetype = 6,
        /// <summary>
        /// </summary>
        CancelForSecurityIssuer = 7,
        /// <summary>
        /// </summary>
        CancelForIssuerOfUnderlyingSecurity = 8,
    }
}
