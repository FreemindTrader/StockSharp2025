
using Ecng.Common;
using Ecng.Security;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Collections.Generic;

namespace StockSharp.Server.Core
{
    /// <summary>
    /// The interface describing a message listening component.
    /// </summary>
    public interface IMessageListener : IMessageChannel, IDisposable, ICloneable<IMessageChannel>, ICloneable, ILogReceiver, ILogSource
    {
        /// <summary>The customer authentication.</summary>
        IAuthorization Authorization { get; }

        /// <summary>Transaction and request identifiers storage.</summary>
        ITransactionIdStorage TransactionIdStorage { get; }

        /// <summary>Keep subscriptions on disconnect.</summary>
        bool KeepSubscriptionsOnDisconnect { get; set; }

        /// <summary>Active sessions.</summary>
        IEnumerable<IMessageListenerSession> Sessions { get; }

        /// <summary>New message event.</summary>
        new event Action<IMessageListenerSession, Message> NewOutMessage;

        /// <summary>Session connected event.</summary>
        event Action<IMessageListenerSession> SessionConnected;

        /// <summary>Session disconnected event.</summary>
        event Action<IMessageListenerSession> SessionDisconnected;

        /// <summary>Get subscription for the specified session.</summary>
        /// <param name="session">Session.</param>
        /// <returns>Subscriptions.</returns>
        IEnumerable<ServerSubscription> GetSubscriptions(
          IMessageListenerSession session );

        /// <summary>Client subscription changed event.</summary>
        event Action<ServerSubscription> SubscriptionChanged;

        /// <summary>Add subscription.</summary>
        /// <param name="subscription">Subscription.</param>
        void AddSubscription( ServerSubscription subscription );

        /// <summary>Remove subscription.</summary>
        /// <param name="subscription">Subscription.</param>
        /// <returns>
        /// <see langword="true" /> if subscription was found, otherwise <see langword="false" />.</returns>
        bool RemoveSubscription( ServerSubscription subscription );

        /// <summary>Suspend subscription.</summary>
        /// <param name="subscription">Subscription.</param>
        /// <returns>
        /// <see langword="true" /> if subscription was found, otherwise <see langword="false" />.</returns>
        bool Suspend( ServerSubscription subscription );

        /// <summary>Resume subscription.</summary>
        /// <param name="subscription">Subscription.</param>
        /// <returns>
        /// <see langword="true" /> if subscription was found, otherwise <see langword="false" />.</returns>
        bool Resume( ServerSubscription subscription );

        /// <summary>Suspend session.</summary>
        /// <param name="session">Session.</param>
        void Suspend( IMessageListenerSession session );

        /// <summary>Resume session.</summary>
        /// <param name="session">Session.</param>
        void Resume( IMessageListenerSession session );

        /// <summary>Disconnect session.</summary>
        /// <param name="session">Session.</param>
        void Disconnect( IMessageListenerSession session );

        /// <summary>Send message.</summary>
        /// <param name="session">Session.</param>
        /// <param name="subscriptionId">Subscription id.</param>
        /// <param name="message">Message.</param>
        /// <returns>
        /// <see langword="true" /> if the specified message was processed successfully, otherwise, <see langword="false" />.</returns>
        bool SendInMessage( IMessageListenerSession session, long? subscriptionId, Message message );
    }
}
