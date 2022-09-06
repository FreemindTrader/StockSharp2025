namespace StockSharp.Fix.Native
{
    /// <summary>
    /// </summary>
    public static class FixTimeInForce
    {
        /// <summary>
        /// </summary>
        public const char Day = '0';
        /// <summary>
        /// </summary>
        public const char GoodTillCancel = '1';
        /// <summary>
        /// </summary>
        public const char AtTheOpening = '2';
        /// <summary>
        /// </summary>
        public const char ImmediateOrCancel = '3';
        /// <summary>
        /// </summary>
        public const char FillOrKill = '4';
        /// <summary>
        /// </summary>
        public const char GoodTillCrossing = '5';
        /// <summary>
        /// </summary>
        public const char GoodTillDate = '6';
        /// <summary>
        /// </summary>
        public const char AtTheClose = '7';
        /// <summary>
        /// </summary>
        public const char GoodThroughCrossing = '8';
        /// <summary>
        /// </summary>
        public const char AtCrossing = '9';
    }
}
