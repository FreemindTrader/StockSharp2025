namespace StockSharp.Fix.Native
{
    /// <summary>
    /// </summary>
    public enum SecurityRequestType
    {
        /// <summary>
        /// </summary>
        RequestSecurityIdentityAndSpecifications,
        /// <summary>
        /// </summary>
        RequestSecurityIdentityForTheSpecificationsProvided,
        /// <summary>
        /// </summary>
        RequestListSecurityTypes,
        /// <summary>
        /// </summary>
        RequestListSecurities,
        /// <summary>
        /// </summary>
        Symbol,
        /// <summary>
        /// </summary>
        SecurityTypeAndOrCfiCode,
        /// <summary>
        /// </summary>
        Product,
        /// <summary>
        /// </summary>
        TradingSessionId,
        /// <summary>
        /// </summary>
        AllSecurities,
        /// <summary>
        /// </summary>
        MarketId,
    }
}
