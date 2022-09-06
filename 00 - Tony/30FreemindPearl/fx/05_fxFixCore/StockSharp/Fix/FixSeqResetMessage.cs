using StockSharp.Messages;

namespace StockSharp.Fix
{
    /// <summary>Sequence reset message.</summary>
    public class FixSeqResetMessage : Message
    {
        private bool? _gapFill;
        private long _newSeqNo;
        private long _seqNumber;
        private bool? _possMissingApplMsg;

        /// <summary>
        /// Initialize <see cref="T:StockSharp.Fix.FixSeqResetMessage" />.
        /// </summary>
        public FixSeqResetMessage() : base((MessageTypes)(-5000))
        {
        }

        /// <summary>Gap fill.</summary>
        public bool? GapFill
        {
            get => _gapFill;
            set => _gapFill = value;
        }

        /// <summary>New sequence number.</summary>
        public long NewSeqNo
        {
            get => _newSeqNo;
            set => _newSeqNo = value;
        }

        /// <summary>Sequence number.</summary>
        public long SeqNum
        {
            get => _seqNumber;
            set => _seqNumber = value;
        }

        /// <summary>
        /// Indicates that the range of messages retransmitted after a <see cref="T:StockSharp.Fix.FixResendRequestMessage" /> may not include all the application messages contained in the original range requested.
        /// </summary>
        public bool? PossMissingApplMsg
        {
            get => _possMissingApplMsg;
            set => _possMissingApplMsg = value;
        }

        /// <summary>
        /// Create a copy of <see cref="T:StockSharp.Fix.FixSeqResetMessage" />.
        /// </summary>
        /// <returns>Copy.</returns>
        public override Message Clone() => (Message)new FixSeqResetMessage()
        {
            GapFill = GapFill,
            NewSeqNo = NewSeqNo,
            SeqNum = SeqNum,
            PossMissingApplMsg = PossMissingApplMsg
        };
    }
}
