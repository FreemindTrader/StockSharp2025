namespace StockSharp.Fix.Native
{
    /// <summary>
    /// </summary>
    public enum SecurityRequestResult
    {
        /// <summary>
        /// </summary>
        ValidRequest,
        /// <summary>
        /// </summary>
        InvalidOrUnsupportedRequest,
        /// <summary>
        /// </summary>
        NoInstrumentsFoundThatMatchSelectionCriteria,
        /// <summary>
        /// </summary>
        NotAuthorizedToRetrieveInstrumentData,
        /// <summary>
        /// </summary>
        InstrumentDataTemporarilyUnavailable,
        /// <summary>
        /// </summary>
        RequestForInstrumentDataNotSupported,
    }
}
