// Decompiled with JetBrains decompiler
// Type: TinyMessenger.TinyMessengerHub
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Ecng.Xaml.Charting;
namespace TinyMessenger
{
    public sealed class TinyMessengerHub : IEventAggregator, ITinyMessengerHub
    {
        private readonly object _SubscriptionsPadlock = new object();
        private readonly Dictionary<Type, List<TinyMessengerHub.SubscriptionItem>> _Subscriptions = new Dictionary<Type, List<TinyMessengerHub.SubscriptionItem>>();

        public TinyMessageSubscriptionToken Subscribe<TMessage>( Action<TMessage> deliveryAction ) where TMessage : class, ITinyMessage
        {
            return AddSubscriptionInternal<TMessage>( deliveryAction, ( Func<TMessage, bool> ) ( m => true ), true, ( ITinyMessageProxy ) DefaultTinyMessageProxy.Instance );
        }

        public TinyMessageSubscriptionToken Subscribe<TMessage>( Action<TMessage> deliveryAction, ITinyMessageProxy proxy ) where TMessage : class, ITinyMessage
        {
            return AddSubscriptionInternal<TMessage>( deliveryAction, ( Func<TMessage, bool> ) ( m => true ), true, proxy );
        }

        public TinyMessageSubscriptionToken Subscribe<TMessage>( Action<TMessage> deliveryAction, bool useStrongReferences ) where TMessage : class, ITinyMessage
        {
            return AddSubscriptionInternal<TMessage>( deliveryAction, ( Func<TMessage, bool> ) ( m => true ), useStrongReferences, ( ITinyMessageProxy ) DefaultTinyMessageProxy.Instance );
        }

        public TinyMessageSubscriptionToken Subscribe<TMessage>( Action<TMessage> deliveryAction, bool useStrongReferences, ITinyMessageProxy proxy ) where TMessage : class, ITinyMessage
        {
            return AddSubscriptionInternal<TMessage>( deliveryAction, ( Func<TMessage, bool> ) ( m => true ), useStrongReferences, proxy );
        }

        public TinyMessageSubscriptionToken Subscribe<TMessage>( Action<TMessage> deliveryAction, Func<TMessage, bool> messageFilter ) where TMessage : class, ITinyMessage
        {
            return AddSubscriptionInternal<TMessage>( deliveryAction, messageFilter, true, ( ITinyMessageProxy ) DefaultTinyMessageProxy.Instance );
        }

        public TinyMessageSubscriptionToken Subscribe<TMessage>( Action<TMessage> deliveryAction, Func<TMessage, bool> messageFilter, ITinyMessageProxy proxy ) where TMessage : class, ITinyMessage
        {
            return AddSubscriptionInternal<TMessage>( deliveryAction, messageFilter, true, proxy );
        }

        public TinyMessageSubscriptionToken Subscribe<TMessage>( Action<TMessage> deliveryAction, Func<TMessage, bool> messageFilter, bool useStrongReferences ) where TMessage : class, ITinyMessage
        {
            return AddSubscriptionInternal<TMessage>( deliveryAction, messageFilter, useStrongReferences, ( ITinyMessageProxy ) DefaultTinyMessageProxy.Instance );
        }

        public TinyMessageSubscriptionToken Subscribe<TMessage>( Action<TMessage> deliveryAction, Func<TMessage, bool> messageFilter, bool useStrongReferences, ITinyMessageProxy proxy ) where TMessage : class, ITinyMessage
        {
            return AddSubscriptionInternal<TMessage>( deliveryAction, messageFilter, useStrongReferences, proxy );
        }

        public void Unsubscribe<TMessage>( TinyMessageSubscriptionToken subscriptionToken ) where TMessage : class, ITinyMessage
        {
            RemoveSubscriptionInternal<TMessage>( subscriptionToken );
        }

        public void Publish<TMessage>( TMessage message ) where TMessage : class, ITinyMessage
        {
            PublishInternal<TMessage>( message );
        }

        public void PublishAsync<TMessage>( TMessage message ) where TMessage : class, ITinyMessage
        {
            PublishAsyncInternal<TMessage>( message, ( AsyncCallback ) null );
        }

        public void PublishAsync<TMessage>( TMessage message, AsyncCallback callback ) where TMessage : class, ITinyMessage
        {
            PublishAsyncInternal<TMessage>( message, callback );
        }

        private TinyMessageSubscriptionToken AddSubscriptionInternal<TMessage>( Action<TMessage> deliveryAction, Func<TMessage, bool> messageFilter, bool strongReference, ITinyMessageProxy proxy ) where TMessage : class, ITinyMessage
        {
            if ( deliveryAction == null )
                throw new ArgumentNullException( nameof( deliveryAction ) );
            if ( messageFilter == null )
                throw new ArgumentNullException( nameof( messageFilter ) );
            if ( proxy == null )
                throw new ArgumentNullException( nameof( proxy ) );
            lock ( _SubscriptionsPadlock )
            {
                List<TinyMessengerHub.SubscriptionItem> subscriptionItemList;
                if ( !_Subscriptions.TryGetValue( typeof( TMessage ), out subscriptionItemList ) )
                {
                    subscriptionItemList = new List<TinyMessengerHub.SubscriptionItem>();
                    _Subscriptions[ typeof( TMessage ) ] = subscriptionItemList;
                }
                TinyMessageSubscriptionToken subscriptionToken = new TinyMessageSubscriptionToken((ITinyMessengerHub) this, typeof (TMessage));
                ITinyMessageSubscription subscription = !strongReference ? (ITinyMessageSubscription) new TinyMessengerHub.WeakTinyMessageSubscription<TMessage>(subscriptionToken, deliveryAction, messageFilter) : (ITinyMessageSubscription) new TinyMessengerHub.StrongTinyMessageSubscription<TMessage>(subscriptionToken, deliveryAction, messageFilter);
                subscriptionItemList.Add( new TinyMessengerHub.SubscriptionItem( proxy, subscription ) );
                return subscriptionToken;
            }
        }

        private void RemoveSubscriptionInternal<TMessage>( TinyMessageSubscriptionToken subscriptionToken ) where TMessage : class, ITinyMessage
        {
            if ( subscriptionToken == null )
                throw new ArgumentNullException( nameof( subscriptionToken ) );
            lock ( _SubscriptionsPadlock )
            {
                List<TinyMessengerHub.SubscriptionItem> currentSubscriptions;
                if ( !_Subscriptions.TryGetValue( typeof( TMessage ), out currentSubscriptions ) )
                    return;
                currentSubscriptions.Where<TinyMessengerHub.SubscriptionItem>( ( Func<TinyMessengerHub.SubscriptionItem, bool> ) ( sub => sub.Subscription.SubscriptionToken == subscriptionToken ) ).ToList<TinyMessengerHub.SubscriptionItem>().ForEach( ( Action<TinyMessengerHub.SubscriptionItem> ) ( sub => currentSubscriptions.Remove( sub ) ) );
            }
        }

        private void PublishInternal<TMessage>( TMessage message ) where TMessage : class, ITinyMessage
        {
            if ( ( object ) message == null )
                throw new ArgumentNullException( nameof( message ) );
            List<TinyMessengerHub.SubscriptionItem> list;
            lock ( _SubscriptionsPadlock )
            {
                List<TinyMessengerHub.SubscriptionItem> source;
                if ( !_Subscriptions.TryGetValue( typeof( TMessage ), out source ) )
                    return;
                list = source.Where<TinyMessengerHub.SubscriptionItem>( ( Func<TinyMessengerHub.SubscriptionItem, bool> ) ( sub => sub.Subscription.ShouldAttemptDelivery( ( ITinyMessage ) message ) ) ).ToList<TinyMessengerHub.SubscriptionItem>();
            }
            list.ForEach( ( Action<TinyMessengerHub.SubscriptionItem> ) ( sub =>
            {
                try
                {
                    sub.Proxy.Deliver( ( ITinyMessage ) message, sub.Subscription );
                }
                catch ( Exception ex )
                {
                }
            } ) );
        }

        private void PublishAsyncInternal<TMessage>( TMessage message, AsyncCallback callback ) where TMessage : class, ITinyMessage
        {
            ( ( Action ) ( () => PublishInternal<TMessage>( message ) ) ).BeginInvoke( callback, ( object ) null );
        }

        private class WeakTinyMessageSubscription<TMessage> : ITinyMessageSubscription where TMessage : class, ITinyMessage
        {
            protected TinyMessageSubscriptionToken _SubscriptionToken;
            protected WeakReference _DeliveryAction;
            protected WeakReference _MessageFilter;

            public TinyMessageSubscriptionToken SubscriptionToken
            {
                get
                {
                    return _SubscriptionToken;
                }
            }

            public bool ShouldAttemptDelivery( ITinyMessage message )
            {
                if ( !( message is TMessage ) || !_DeliveryAction.IsAlive || !_MessageFilter.IsAlive )
                    return false;
                return ( ( Func<TMessage, bool> ) _MessageFilter.Target )( message as TMessage );
            }

            public void Deliver( ITinyMessage message )
            {
                if ( !( message is TMessage ) )
                    throw new ArgumentException( "Message is not the correct type" );
                if ( !_DeliveryAction.IsAlive )
                    return;
                ( ( Action<TMessage> ) _DeliveryAction.Target )( message as TMessage );
            }

            public WeakTinyMessageSubscription( TinyMessageSubscriptionToken subscriptionToken, Action<TMessage> deliveryAction, Func<TMessage, bool> messageFilter )
            {
                if ( subscriptionToken == null )
                    throw new ArgumentNullException( nameof( subscriptionToken ) );
                if ( deliveryAction == null )
                    throw new ArgumentNullException( nameof( deliveryAction ) );
                if ( messageFilter == null )
                    throw new ArgumentNullException( nameof( messageFilter ) );
                _SubscriptionToken = subscriptionToken;
                _DeliveryAction = new WeakReference( ( object ) deliveryAction );
                _MessageFilter = new WeakReference( ( object ) messageFilter );
            }
        }

        private class StrongTinyMessageSubscription<TMessage> : ITinyMessageSubscription where TMessage : class, ITinyMessage
        {
            protected TinyMessageSubscriptionToken _SubscriptionToken;
            protected Action<TMessage> _DeliveryAction;
            protected Func<TMessage, bool> _MessageFilter;

            public TinyMessageSubscriptionToken SubscriptionToken
            {
                get
                {
                    return _SubscriptionToken;
                }
            }

            public bool ShouldAttemptDelivery( ITinyMessage message )
            {
                if ( !( message is TMessage ) )
                    return false;
                return _MessageFilter( message as TMessage );
            }

            public void Deliver( ITinyMessage message )
            {
                if ( !( message is TMessage ) )
                    throw new ArgumentException( "Message is not the correct type" );
                _DeliveryAction( message as TMessage );
            }

            public StrongTinyMessageSubscription( TinyMessageSubscriptionToken subscriptionToken, Action<TMessage> deliveryAction, Func<TMessage, bool> messageFilter )
            {
                if ( subscriptionToken == null )
                    throw new ArgumentNullException( nameof( subscriptionToken ) );
                if ( deliveryAction == null )
                    throw new ArgumentNullException( nameof( deliveryAction ) );
                if ( messageFilter == null )
                    throw new ArgumentNullException( nameof( messageFilter ) );
                _SubscriptionToken = subscriptionToken;
                _DeliveryAction = deliveryAction;
                _MessageFilter = messageFilter;
            }
        }

        private class SubscriptionItem
        {
            public ITinyMessageProxy Proxy
            {
                get; private set;
            }

            public ITinyMessageSubscription Subscription
            {
                get; private set;
            }

            public SubscriptionItem( ITinyMessageProxy proxy, ITinyMessageSubscription subscription )
            {
                Proxy = proxy;
                Subscription = subscription;
            }
        }
    }
}
