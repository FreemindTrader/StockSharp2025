using StockSharp.Algo;
using StockSharp.Messages;
using System;
using System.Diagnostics;

namespace StockSharp.Server.Core
{
    /// <summary>Server subscription.</summary>
    public class ServerSubscription : Subscription
    {
        private readonly IMessageListenerSession _session;
        private long _messagesCount;
        private DateTimeOffset? _lastMessageTime;
        private int _errorCount;
        private long _bytesReceived;
        private long _bytesSent;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Server.Core.ServerSubscription" />.
        /// </summary>
        /// <param name="session">Session.</param>
        /// <param name="subscriptionMessage">Subscription message.</param>
        /// <param name="security">Security.</param>
        public ServerSubscription( IMessageListenerSession session, ISubscriptionMessage subscriptionMessage, SecurityMessage security = null )
          : base( subscriptionMessage, security )
        {
            _session = session ?? throw new ArgumentNullException( nameof( session ) );
        }

        /// <summary>Session.</summary>
        public IMessageListenerSession Session => _session;

        /// <summary>Total messages count.</summary>
        public long MessagesCount
        {
            get => _messagesCount;
            set => _messagesCount = value;
        }

        /// <summary>Last message time.</summary>
        public DateTimeOffset? LastMessageTime
        {
            get => _lastMessageTime;
            set => _lastMessageTime = value;
        }

        /// <summary>The number of errors.</summary>
        public int ErrorCount
        {
            get => _errorCount;
            set => _errorCount = value;
        }

        /// <summary>The number of bytes received.</summary>
        public long BytesReceived
        {
            get => _bytesReceived;
            set => _bytesReceived = value;
        }

        /// <summary>The number of bytes sent.</summary>
        public long BytesSent
        {
            get => _bytesSent;
            set => _bytesSent = value;
        }
    }
}
