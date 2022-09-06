namespace StockSharp.Fix.Native
{
    /// <summary>
    /// </summary>
    public enum PosReqResult
    {
        /// <summary>
        /// </summary>
        ValidRequest = 0,
        /// <summary>
        /// </summary>
        InvalidOrUnsupportedRequest = 1,
        /// <summary>
        /// </summary>
        NoPositionsFoundThatMatchCriteria = 2,
        /// <summary>
        /// </summary>
        NotAuthorizedToRequestPositions = 3,
        /// <summary>
        /// </summary>
        RequestForPositionNotSupported = 4,
        /// <summary>
        /// </summary>
        Other = 99, // 0x00000063
    }
}
