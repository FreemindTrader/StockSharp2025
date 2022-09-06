namespace StockSharp.Fix.Native
{
    /// <summary>
    /// </summary>
    public enum CxlRejReason
    {
        /// <summary>
        /// </summary>
        TooLateToCancel = 0,
        /// <summary>
        /// </summary>
        UnknownOrder = 1,
        /// <summary>
        /// </summary>
        Broker = 2,
        /// <summary>
        /// </summary>
        OrderAlreadyInPendingCancelOrPendingReplaceStatus = 3,
        /// <summary>
        /// </summary>
        UnableToProcessOrderMassCancelRequest = 4,
        /// <summary>
        /// </summary>
        OrigOrdModTime = 5,
        /// <summary>
        /// </summary>
        DuplicateClOrdId = 6,
        /// <summary>
        /// </summary>
        PriceExceedsCurrentPrice = 7,
        /// <summary>
        /// </summary>
        PriceExceedsCurrentPriceBand = 8,
        /// <summary>
        /// </summary>
        InvalidPriceIncrement = 18, // 0x00000012
        /// <summary>
        /// </summary>
        Other = 99, // 0x00000063
    }
}
