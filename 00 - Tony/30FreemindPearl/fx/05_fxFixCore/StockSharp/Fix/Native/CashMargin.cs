namespace StockSharp.Fix.Native
{
    /// <summary>
    /// Identifies whether an order is a margin order or a non-margin order.
    /// </summary>
    public static class CashMargin
    {
        /// <summary>Cash.</summary>
        public const char Cash = '\x0001';
        /// <summary>Margin open.</summary>
        public const char MarginOpen = '\x0002';
        /// <summary>Margin close.</summary>
        public const char MarginClose = '\x0003';
    }
}
