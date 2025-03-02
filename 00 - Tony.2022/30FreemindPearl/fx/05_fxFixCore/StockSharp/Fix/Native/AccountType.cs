namespace StockSharp.Fix.Native
{
    /// <summary>
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// </summary>
        AccountIsCarriedOnCustomerSideBooks = 1,
        /// <summary>
        /// </summary>
        AccountIsCarriedOnNonCustomerSideBooks = 2,
        /// <summary>
        /// </summary>
        HouseTrader = 3,
        /// <summary>
        /// </summary>
        FloorTrader = 4,
        /// <summary>
        /// </summary>
        AccountIsCarriedOnNonCustomerSideBooksAndIsCrossMargined = 6,
        /// <summary>
        /// </summary>
        AccountIsHouseTraderAndIsCrossMargined = 7,
        /// <summary>
        /// </summary>
        JointBackOfficeAccount = 8,
    }
}
