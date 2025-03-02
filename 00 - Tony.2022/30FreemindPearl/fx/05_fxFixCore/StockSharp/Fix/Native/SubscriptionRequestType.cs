namespace StockSharp.Fix.Native
{
    /// <summary>
    /// </summary>
    public static class SubscriptionRequestType
    {
        /// <summary>
        /// </summary>
        public const char Snapshot = '0';
        /// <summary>
        /// </summary>
        public const char SnapshotPlusUpdates = '1';
        /// <summary>
        /// </summary>
        public const char DisablePreviousSnapshotPlusUpdateRequest = '2';
        /// <summary>
        /// </summary>
        public const char DisablePrevious = '2';
    }
}
