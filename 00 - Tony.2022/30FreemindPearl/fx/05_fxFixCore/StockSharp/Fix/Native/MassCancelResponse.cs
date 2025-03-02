namespace StockSharp.Fix.Native
{
    /// <summary>
    /// </summary>
    public static class MassCancelResponse
    {
        /// <summary>
        /// </summary>
        public const char CancelRequestRejected = '0';
        /// <summary>
        /// </summary>
        public const char CancelOrdersForASecurity = '1';
        /// <summary>
        /// </summary>
        public const char CancelOrdersForAnUnderlyingSecurity = '2';
        /// <summary>
        /// </summary>
        public const char CancelOrdersForAProduct = '3';
        /// <summary>
        /// </summary>
        public const char CancelOrdersForACficode = '4';
        /// <summary>
        /// </summary>
        public const char CancelOrdersForASecuritytype = '5';
        /// <summary>
        /// </summary>
        public const char CancelOrdersForATradingSession = '6';
        /// <summary>
        /// </summary>
        public const char CancelAllOrders = '7';
        /// <summary>
        /// </summary>
        public const char CancelOrdersForAMarket = '8';
        /// <summary>
        /// </summary>
        public const char CancelOrdersForAMarketSegment = '9';
        /// <summary>
        /// </summary>
        public const char CancelOrdersForASecurityGroup = 'A';
    }
}
