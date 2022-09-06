namespace StockSharp.Fix.Native
{
    /// <summary>
    /// </summary>
    public static class MDReqRejReason
    {
        /// <summary>
        /// </summary>
        public const char UnknownSymbol = '0';
        /// <summary>
        /// </summary>
        public const char DuplicateMdReqId = '1';
        /// <summary>
        /// </summary>
        public const char InsufficientBandwidth = '2';
        /// <summary>
        /// </summary>
        public const char InsufficientPermissions = '3';
        /// <summary>
        /// </summary>
        public const char UnsupportedSubscriptionRequestType = '4';
        /// <summary>
        /// </summary>
        public const char UnsupportedMarketDepth = '5';
        /// <summary>
        /// </summary>
        public const char UnsupportedMdUpdateType = '6';
        /// <summary>
        /// </summary>
        public const char UnsupportedAggregatedBook = '7';
        /// <summary>
        /// </summary>
        public const char UnsupportedMdEntryType = '8';
        /// <summary>
        /// </summary>
        public const char UnsupportedTradingSessionId = '9';
        /// <summary>
        /// </summary>
        public const char UnsupportedScope = 'A';
        /// <summary>
        /// </summary>
        public const char UnsupportedOpenCloseSettleFlag = 'B';
        /// <summary>
        /// </summary>
        public const char UnsupportedMdImplicitDelete = 'C';
        /// <summary>
        /// </summary>
        public const char InsufficientCredit = 'D';
    }
}
