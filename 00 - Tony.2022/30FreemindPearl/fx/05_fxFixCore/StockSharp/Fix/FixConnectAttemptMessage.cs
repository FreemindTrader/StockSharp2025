using StockSharp.Messages;

namespace StockSharp.Fix
{
    /// <summary>
    /// </summary>
    public class FixConnectAttemptMessage : Message
    {

        private long _expectedSeqNum;

        /// <summary>
        /// Initialize <see cref="T:StockSharp.Fix.FixConnectAttemptMessage" />.
        /// </summary>
        public FixConnectAttemptMessage() : base((MessageTypes)(-5006))
        {
        }

        /// <summary>
        /// </summary>
        public long ExpectedSeqNum
        {
            get => this._expectedSeqNum;
            set => this._expectedSeqNum = value;
        }

        /// <summary>
        /// Create a copy of <see cref="T:StockSharp.Fix.FixConnectAttemptMessage" />.
        /// </summary>
        /// <returns>Copy.</returns>
        public override Message Clone() => (Message)new FixConnectAttemptMessage()
        {
            ExpectedSeqNum = this.ExpectedSeqNum
        };
    }
}
