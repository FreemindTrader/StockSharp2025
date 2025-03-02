using StockSharp.Messages;

namespace StockSharp.Fix
{
    /// <summary>FIX server extended message for force logoff message.</summary>
    public class FixUserForceLogoffMessage : BaseResultMessage<FixUserForceLogoffMessage>
    {
        /// <summary>
        /// Initialize <see cref="T:StockSharp.Fix.FixUserForceLogoffMessage" />.
        /// </summary>
        public FixUserForceLogoffMessage() : base((MessageTypes)(-5004))
        {
        }
    }
}
