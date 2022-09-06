namespace StockSharp.Fix.Native
{
    /// <summary>
    /// </summary>
    public enum SecurityResponseType
    {
        /// <summary>
        /// </summary>
        AcceptSecurityProposalAsIs = 1,
        /// <summary>
        /// </summary>
        AcceptSecurityProposalWithRevisionsAsIndicatedInTheMessage = 2,
        /// <summary>
        /// </summary>
        ListOfSecurityTypesReturnedPerRequest = 3,
        /// <summary>
        /// </summary>
        ListOfSecuritiesReturnedPerRequest = 4,
        /// <summary>
        /// </summary>
        RejectSecurityProposal = 5,
        /// <summary>
        /// </summary>
        CannotMatchSelectionCriteria = 6,
    }
}
