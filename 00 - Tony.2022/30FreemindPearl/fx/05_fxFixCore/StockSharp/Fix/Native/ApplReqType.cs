namespace StockSharp.Fix.Native
{
    /// <summary>
    /// </summary>
    public enum ApplReqType
    {
        /// <summary>
        /// </summary>
        RetransmissionOfApplicationMessagesForTheSpecifiedApplications,
        /// <summary>
        /// </summary>
        SubscriptionToTheSpecifiedApplications,
        /// <summary>
        /// </summary>
        RequestForTheLastAppllastseqnumPublishedForTheSpecifiedApplications,
        /// <summary>
        /// </summary>
        RequestValidSetOfApplications,
        /// <summary>
        /// </summary>
        UnsubscribeToTheSpecifiedApplications,
        /// <summary>
        /// </summary>
        CancelRetransmission,
        /// <summary>
        /// </summary>
        CancelRetransmissionAndUnsubscribeToTheSpecifiedApplications,
    }
}
