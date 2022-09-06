using StockSharp.Messages;

namespace StockSharp.Fix
{
    /// <summary>FIX server extended messages.</summary>
    public static class FixMessageTypes
    {
        /// <summary>
        /// <see cref="T:StockSharp.Fix.FixSeqResetMessage" />.
        ///     </summary>
        public const MessageTypes SeqReset = (MessageTypes)(-5000);
        /// <summary>
        /// <see cref="T:StockSharp.Fix.FixResendRequestMessage" />.
        ///     </summary>
        public const MessageTypes ResendRequest = (MessageTypes)(-5001);
        /// <summary>
        /// <see cref="T:StockSharp.Fix.FixUserRequestMessage" />.
        ///     </summary>
        public const MessageTypes UserRequest = (MessageTypes)(-5002);
        /// <summary>
        /// <see cref="T:StockSharp.Fix.FixUserResponseMessage" />.
        ///     </summary>
        public const MessageTypes UserResponse = (MessageTypes)(-5003);
        /// <summary>
        /// <see cref="T:StockSharp.Fix.FixUserForceLogoffMessage" />.
        ///     </summary>
        public const MessageTypes UserForceLogoff = (MessageTypes)(-5004);
        /// <summary>
        /// <see cref="T:StockSharp.Fix.FixMakeGapMessage" />.
        ///     </summary>
        public const MessageTypes MakeGap = (MessageTypes)(-5005);
        /// <summary>
        /// <see cref="T:StockSharp.Fix.FixConnectAttemptMessage" />.
        ///     </summary>
        public const MessageTypes ConnectAttempt = (MessageTypes)(-5006);
    }
}
