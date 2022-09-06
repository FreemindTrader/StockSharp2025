using StockSharp.Messages;

namespace StockSharp.Fix
{
    /// <summary>FIX server extended message for user based action.</summary>
    public class FixUserRequestMessage : ChangePasswordMessage
    {
        private FixUserRequestTypes _requestType;

        /// <summary>
        /// Initialize <see cref="T:StockSharp.Fix.FixUserRequestMessage" />.
        /// </summary>
        public FixUserRequestMessage() : base((MessageTypes)(-5002))
        {
        }

        /// <summary>Request type.</summary>
        public FixUserRequestTypes RequestType
        {
            get => _requestType;
            set => _requestType = value;
        }

        /// <summary>
        /// Create a copy of <see cref="T:StockSharp.Fix.FixUserRequestMessage" />.
        /// </summary>
        /// <returns>Copy.</returns>
        public override Message Clone()
        {
            FixUserRequestMessage userRequestMessage = new FixUserRequestMessage()
            {
                RequestType = RequestType
            };
            CopyTo(userRequestMessage);
            return userRequestMessage;
        }
    }
}
