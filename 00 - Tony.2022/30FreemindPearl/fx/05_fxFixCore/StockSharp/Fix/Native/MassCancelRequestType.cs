namespace StockSharp.Fix.Native
{
    /// <summary>
    /// </summary>
    public static class MassCancelRequestType
    {
        /// <summary>
        /// </summary>
        public const char CancelOrdersForSecurity = '1';
        /// <summary>
        /// </summary>
        public const char CancelOrdersForUnderlyingSecurity = '2';
        /// <summary>
        /// </summary>
        public const char CancelOrdersForProduct = '3';
        /// <summary>
        /// </summary>
        public const char CancelOrdersForCfiCode = '4';
        /// <summary>
        /// </summary>
        public const char CancelOrdersForSecurityType = '5';
        /// <summary>
        /// </summary>
        public const char CancelOrdersForTradingSession = '6';
        /// <summary>
        /// </summary>
        public const char CancelAllOrders = '7';
        /// <summary>
        /// </summary>
        public const char CancelOrdersForMarket = '8';
        /// <summary>
        /// </summary>
        public const char CancelOrdersForMarketSegment = '9';
        /// <summary>
        /// </summary>
        public const char CancelOrdersForSecurityGroup = 'A';
        /// <summary>
        /// </summary>
        public const char CancelForSecurityIssuer = 'B';
        /// <summary>
        /// </summary>
        public const char CancelForIssuerOfUnderlyingSecurity = 'C';
    }
}
