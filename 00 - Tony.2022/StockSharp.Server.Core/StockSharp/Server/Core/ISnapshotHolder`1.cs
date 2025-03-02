
using StockSharp.Messages;

namespace StockSharp.Server.Core
{
    /// <summary>Interface, described snapshots holder.</summary>
    /// <typeparam name="TMessage">Message type.</typeparam>
    public interface ISnapshotHolder<TMessage> where TMessage : Message
    {
        /// <summary>Try get snapshot for the specified security.</summary>
        /// <param name="securityId">Security ID.</param>
        /// <returns>Snapshot.</returns>
        TMessage TryGetSnapshot( SecurityId securityId );

        /// <summary>
        /// Process <typeref name="TMessage" /> change.
        /// </summary>
        /// <param name="change">
        /// <typeref name="TMessage" /> change.</param>
        /// <param name="needResponse">Need response value.</param>
        /// <returns>
        /// <typeref name="TMessage" /> change.</returns>
        TMessage Process( TMessage change, bool needResponse );

        /// <summary>Reset snapshot for the specified security.</summary>
        /// <param name="securityId">Security ID.</param>
        void ResetSnapshot( SecurityId securityId );
    }
}
