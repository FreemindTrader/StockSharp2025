namespace StockSharp.Fix.Native
{
    /// <summary>
    /// Code to identify reason for an <see cref="T:StockSharp.Fix.Native.ExecutionReport" /> message sent with <see cref="T:StockSharp.Fix.Native.ExecType" />=<see cref="F:StockSharp.Fix.Native.ExecType.Restated" /> or used when communicating an unsolicited cancel.
    /// </summary>
    public enum ExecRestatementReason
    {
        /// <summary>GT corporate action.</summary>
        CorporateAction = 0,
        /// <summary>GT renewal / restatement (no corporate action).</summary>
        RenewalRestatement = 1,
        /// <summary>Verbal change.</summary>
        VerbalChange = 2,
        /// <summary>Repricing of order.</summary>
        RepricingOrder = 3,
        /// <summary>Broker option.</summary>
        BrokerOption = 4,
        /// <summary>
        /// Partial decline of <see cref="F:StockSharp.Fix.Native.FixTags.OrderQty" /> (e.g. exchange initiated partial cancel).
        /// </summary>
        PartialDecline = 5,
        /// <summary>Cancel on Trading Halt.</summary>
        CancelTradingHalt = 6,
        /// <summary>Cancel on System Failure.</summary>
        CancelSystemFailure = 7,
        /// <summary>Market (Exchange) option.</summary>
        MarketOption = 8,
        /// <summary>Canceled, not best.</summary>
        CanceledNotBest = 9,
        /// <summary>Warehouse Recap.</summary>
        WarehouseRecap = 10, // 0x0000000A
        /// <summary>Peg Refresh.</summary>
        PegRefresh = 11, // 0x0000000B
        /// <summary>Other.</summary>
        Other = 99, // 0x00000063
    }
}
