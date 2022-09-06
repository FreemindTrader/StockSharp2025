
namespace StockSharp.Server.Core
{
    /// <summary>
    /// The interface describing the transaction and request identifiers storage.
    /// </summary>
    public interface ITransactionIdStorage
    {
        /// <summary>
        /// Get session based transaction and request identifiers storage.
        /// </summary>
        /// <param name="sessionId">
        /// <see cref="P:StockSharp.Server.Core.IMessageListenerSession.SessionId" /> value.</param>
        /// <returns>Session based transaction and request identifiers storage.</returns>
        /// <param name="persistable">Reuse session identifier.</param>
        ISessionTransactionIdStorage Get( string sessionId, bool persistable );
    }
}
