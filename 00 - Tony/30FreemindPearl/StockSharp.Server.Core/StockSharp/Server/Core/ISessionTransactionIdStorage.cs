
namespace StockSharp.Server.Core
{
    /// <summary>
    /// The interface describing the session based transaction and request identifiers storage.
    /// </summary>
    public interface ISessionTransactionIdStorage
    {
        /// <summary>Find request id by the specified transaction id.</summary>
        /// <param name="transactionId">Transaction ID.</param>
        /// <returns>The request identifier. <see langword="null" /> if the specified id doesn't exist.</returns>
        string TryGetRequestId( long transactionId );

        /// <summary>Find transaction id by the specified request id.</summary>
        /// <param name="requestId">The request identifier.</param>
        /// <returns>Transaction ID. <see langword="null" /> if the specified request doesn't exist.</returns>
        long? TryGetTransactionId( string requestId );

        /// <summary>Create request identifier.</summary>
        /// <returns>The request identifier.</returns>
        string CreateRequestId();

        /// <summary>Create association.</summary>
        /// <param name="requestId">The request identifier.</param>
        /// <returns>Transaction ID.</returns>
        long CreateTransactionId( string requestId );

        /// <summary>Delete association.</summary>
        /// <param name="requestId">The request identifier.</param>
        /// <returns>
        /// <see langword="true" /> if association was removed successfully, otherwise, returns <see langword="false" />.</returns>
        bool RemoveRequestId( string requestId );

        /// <summary>Delete association.</summary>
        /// <param name="transactionId">Transaction ID.</param>
        /// <returns>
        /// <see langword="true" /> if association was removed successfully, otherwise, returns <see langword="false" />.</returns>
        bool RemoveTransactionId( long transactionId );
    }
}
