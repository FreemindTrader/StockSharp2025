using StockSharp.Messages;

namespace StockSharp.Fix
{
    /// <summary>
    /// The resend request is sent by the receiving application to initiate the retransmission of messages.
    /// This function is utilized if a sequence number gap is detected, if the receiving application lost a message, or as a function of the initialization process.
    /// </summary>
    public class FixResendRequestMessage : Message
    {
        private long _beginSeqNo;
        private long _endSeqNo;

        /// <summary>
        /// Initialize <see cref="T:StockSharp.Fix.FixResendRequestMessage" />.
        /// </summary>
        public FixResendRequestMessage() : base((MessageTypes)(-5001))
        {
        }

        /// <summary>
        /// Message sequence number of first message in range to be resent.
        /// </summary>
        public long BeginSeqNo
        {
            get => _beginSeqNo;
            set => _beginSeqNo = value;
        }

        /// <summary>
        /// Message sequence number of last message in range to be resent. If request is for a single message <see cref="P:StockSharp.Fix.FixResendRequestMessage.BeginSeqNo" /> = <see cref="P:StockSharp.Fix.FixResendRequestMessage.EndSeqNo" />. If request is for all messages subsequent to a particular message, <see cref="P:StockSharp.Fix.FixResendRequestMessage.EndSeqNo" /> = "0" (representing infinity).
        /// </summary>
        public long EndSeqNo
        {
            get => _endSeqNo;
            set => _endSeqNo = value;
        }

        /// <summary>
        /// Create a copy of <see cref="T:StockSharp.Fix.FixResendRequestMessage" />.
        /// </summary>
        /// <returns>Copy.</returns>
        public override Message Clone() => (Message)new FixResendRequestMessage()
        {
            BeginSeqNo = BeginSeqNo,
            EndSeqNo = EndSeqNo
        };
    }
}
