namespace StockSharp.Fix.Native
{
    /// <summary>
    /// Indicates the type of product the security is associated with.
    /// </summary>
    public enum Product
    {
        /// <summary>
        /// </summary>
        Agency = 1,
        /// <summary>
        /// </summary>
        Commodity = 2,
        /// <summary>
        /// </summary>
        Corporate = 3,
        /// <summary>
        /// </summary>
        Currency = 4,
        /// <summary>
        /// </summary>
        Equity = 5,
        /// <summary>
        /// </summary>
        Government = 6,
        /// <summary>
        /// </summary>
        Index = 7,
        /// <summary>
        /// </summary>
        Loan = 8,
        /// <summary>
        /// </summary>
        MoneyMarket = 9,
        /// <summary>
        /// </summary>
        Mortgage = 10, // 0x0000000A
        /// <summary>
        /// </summary>
        Municipal = 11, // 0x0000000B
        /// <summary>
        /// </summary>
        Other = 12, // 0x0000000C
        /// <summary>
        /// </summary>
        Financing = 13, // 0x0000000D
    }
}
