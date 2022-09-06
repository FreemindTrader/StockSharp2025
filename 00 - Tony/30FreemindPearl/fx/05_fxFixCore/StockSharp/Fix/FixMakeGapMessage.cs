using StockSharp.Messages;
using System;
using System.Diagnostics;

namespace StockSharp.Fix
{
    /// <summary>Make gap in incremental messages for test purpose.</summary>
    public class FixMakeGapMessage : Message, ICloneable, ITransactionIdMessage, IMessage
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int _gapSize;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private long _transactionID;

        /// <summary>
        /// Initialize <see cref="T:StockSharp.Fix.FixMakeGapMessage" />.
        /// </summary>
        public FixMakeGapMessage()
          : base((MessageTypes)(-5005))
        {
        }

        /// <summary>Gap size (in messages).</summary>
        public int GapSize
        {
            get => this._gapSize;
            set => this._gapSize = value;
        }

        /// <inheritdoc />
        public long TransactionId
        {
            get => this._transactionID;
            set => this._transactionID = value;
        }

        /// <summary>
        /// Create a copy of <see cref="T:StockSharp.Fix.FixMakeGapMessage" />.
        /// </summary>
        /// <returns>Copy.</returns>
        public override Message Clone()
        {
            FixMakeGapMessage destination = new FixMakeGapMessage();
            this.CopyTo(destination);
            return (Message)destination;
        }

        /// <summary>
        /// Copy the message into the <paramref name="destination" />.
        /// </summary>
        /// <param name="destination">The object, to which copied information.</param>
        protected virtual void CopyTo(FixMakeGapMessage destination)
        {
            this.CopyTo((Message)destination);
            destination.TransactionId = this.TransactionId;
            destination.GapSize = this.GapSize;
        }
    }
}
