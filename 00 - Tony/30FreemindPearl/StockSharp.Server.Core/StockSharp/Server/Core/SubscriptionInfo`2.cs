using StockSharp.Algo;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StockSharp.Server.Core
{
    /// <summary>Subscription.</summary>
    /// <typeparam name="TSession">Session type.</typeparam>
    /// <typeparam name="TRequestId">Request identifier type.</typeparam>
    public class SubscriptionInfo<TSession, TRequestId> where TSession : class, IMessageListenerSession
    {
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private readonly TSession _session;
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private readonly TRequestId _requestId;
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private readonly ServerSubscription _subscription;
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private readonly MessageTypes _messageTypes;
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private object _arg;
        /// <summary>Responses.</summary>
        public readonly ISet<MessageTypes> Responses = new HashSet<MessageTypes>();
        /// <summary>Processed level1 securities.</summary>
        public readonly HashSet<SecurityId> ProcessedLevel1 = new HashSet<SecurityId>();
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private bool _isSuspend;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Server.Core.SubscriptionInfo`2" />.
        /// </summary>
        /// <param name="session">Session.</param>
        /// <param name="requestId">Request identifier.</param>
        /// <param name="subscription">Subscription.</param>
        /// <param name="type">Message type.</param>
        public SubscriptionInfo(
          TSession session,
          TRequestId requestId,
          ServerSubscription subscription,
          MessageTypes type )
        {
            _session = session ?? throw new ArgumentNullException( nameof( session ) );
            _requestId = requestId;
            _subscription = subscription ?? throw new ArgumentNullException( nameof( subscription ) );
            _messageTypes = type;
        }

        /// <summary>Session.</summary>
        public TSession Session => _session;

        /// <summary>Request identifier.</summary>
        public TRequestId RequestId => _requestId;

        /// <summary>Subscription id.</summary>
        public long Id => Subscription.TransactionId;

        /// <summary>Subscription.</summary>
        public ServerSubscription Subscription => _subscription;

        /// <summary>Message type.</summary>
        public MessageTypes Type => _messageTypes;

        /// <summary>Security ID.</summary>
        public SecurityId SecurityId => Subscription.SecurityId.GetValueOrDefault();

        /// <summary>Data type info.</summary>
        public DataType DataType => Subscription.DataType;

        /// <summary>Extra argument.</summary>
        public object Arg
        {
            get => _arg;
            set => _arg = value;
        }

        /// <summary>State.</summary>
        public SubscriptionStates State
        {
            get => Subscription.State;
            set => Subscription.State = value;
        }

        /// <summary>Suspended.</summary>
        public bool Suspend
        {
            get => _isSuspend;
            set => _isSuspend = value;
        }
    }
}
