// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.SubscriptionControlManager
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using Ecng.Collections;
using StockSharp.Algo;
using StockSharp.Messages;
using System;

namespace StockSharp.Studio.Core
{
    public class SubscriptionControlManager
    {
        private readonly SynchronizedDictionary<Subscription, SubInfo> _subscriptions = new SynchronizedDictionary<Subscription, SubInfo>();
        private readonly ISubscriptionProvider _provider;

        public SubscriptionControlManager( ISubscriptionProvider provider )
        {
            _provider = provider;
            provider.SubscriptionStopped += ( s, _ ) => CheckUnsubscribe( s, false );
            provider.SubscriptionStarted += s => CheckUnsubscribe( s, false );
            provider.SubscriptionFailed += ( s, _1, _2 ) => CheckUnsubscribe( s, false );
            provider.SubscriptionOnline += s => CheckUnsubscribe( s, false );
            provider.SubscriptionReceived += ( s, _ ) => CheckUnsubscribe( s, false );
        }

        private void CheckUnsubscribe( Subscription sub, bool unsub = false )
        {
            SubInfo subInfo = _subscriptions.TryGetValue( sub );
            if ( subInfo == null )
                return;
            if ( unsub )
                subInfo.IsSubscribed = false;
            if ( subInfo.IsSubscribed || !sub.State.IsActive() )
                return;
            _subscriptions.Remove( sub );
            _provider.UnSubscribe( sub );
        }

        public void Subscribe( object handler, Subscription subscription )
        {
            _subscriptions.SafeAdd( subscription ).Listeners.Add( handler );
            _provider.Subscribe( subscription );
        }

        public object[ ] Get( Subscription subscription )
        {
            return _subscriptions.TryGetValue( subscription )?.Listeners.ToArray() ?? Array.Empty<object>();
        }

        public void Unsubscribe( Subscription subscription )
        {
            CheckUnsubscribe( subscription, true );
        }

        private class SubInfo
        {
            public bool IsSubscribed { get; set; } = true;

            public CachedSynchronizedSet<object> Listeners { get; } = new CachedSynchronizedSet<object>();
        }
    }
}
