using StockSharp.Messages;

namespace StockSharp.Fix
{
    /// <summary>
    /// FIX server extended message for user based action result.
    /// </summary>
    public class FixUserResponseMessage : BaseResultMessage<FixUserResponseMessage>
    {
        private string _userName;
        private FixUserResponseTypes _responseType;

        /// <summary>
        /// Initialize <see cref="T:StockSharp.Fix.FixUserResponseMessage" />.
        /// </summary>
        public FixUserResponseMessage() : base((MessageTypes)(-5003))
        {
        }

        /// <summary>User name.</summary>
        public string UserName
        {
            get => _userName;
            set => _userName = value;
        }

        /// <summary>Response type.</summary>
        public FixUserResponseTypes ResponseType
        {
            get => _responseType;
            set => _responseType = value;
        }

        /// <inheritdoc />
        protected override void CopyTo(FixUserResponseMessage destination)
        {
            base.CopyTo(destination);
            destination.UserName = UserName;
            destination.ResponseType = ResponseType;
        }
    }
}
