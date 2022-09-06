namespace StockSharp.Fix.Native
{
    /// <summary>
    /// </summary>
    public static class ExecType
    {
        /// <summary>
        /// </summary>
        public const char New = '0';
        /// <summary>
        /// </summary>
        public const char PartialFill = '1';
        /// <summary>
        /// </summary>
        public const char Fill = '2';
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
        public const char Restated = 'D';
        /// <summary>
        /// </summary>
        public const char PendingReplace = 'E';
        /// <summary>
        /// </summary>
        public const char Trade = 'F';
        /// <summary>
        /// </summary>
        public const char TradeCorrect = 'G';
        /// <summary>
        /// </summary>
        public const char TradeCancel = 'H';
        /// <summary>
        /// </summary>
        public const char OrderStatus = 'I';
        /// <summary>
        /// </summary>
        public const char TradeInAClearingHold = 'J';
        /// <summary>
        /// </summary>
        public const char TradeHasBeenReleasedToClearing = 'K';
        /// <summary>
        /// </summary>
        public const char TriggeredOrActivatedBySystem = 'L';
    }
}
