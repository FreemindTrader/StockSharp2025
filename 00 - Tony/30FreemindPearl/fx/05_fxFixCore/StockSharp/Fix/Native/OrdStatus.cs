namespace StockSharp.Fix.Native
{
    /// <summary>
    /// </summary>
    public static class OrdStatus
    {
        /// <summary>
        /// </summary>
        public const char New = '0';
        /// <summary>
        /// </summary>
        public const char PartiallyFilled = '1';
        /// <summary>
        /// </summary>
        public const char Filled = '2';
        /// <summary>
        /// </summary>
        public const char DoneForDay = '3';
        /// <summary>
        /// </summary>
        public const char Canceled = '4';
        /// <summary>
        /// </summary>
        public const char Replaced = '5';
        /// <summary>
        /// </summary>
        public const char PendingCancel = '6';
        /// <summary>
        /// </summary>
        public const char Stopped = '7';
        /// <summary>
        /// </summary>
        public const char Rejected = '8';
        /// <summary>
        /// </summary>
        public const char Suspended = '9';
        /// <summary>
        /// </summary>
        public const char PendingNew = 'A';
        /// <summary>
        /// </summary>
        public const char Calculated = 'B';
        /// <summary>
        /// </summary>
        public const char Expired = 'C';
        /// <summary>
        /// </summary>
        public const char AcceptedForBidding = 'D';
        /// <summary>
        /// </summary>
        public const char PendingReplace = 'E';
    }
}
