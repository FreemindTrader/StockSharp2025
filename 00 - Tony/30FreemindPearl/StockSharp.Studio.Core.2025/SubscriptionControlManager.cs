// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.SubscriptionControlManager
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using Ecng.Collections;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Studio.Core
{
    public class SubscriptionControlManager
    {
        private readonly SynchronizedDictionary<Subscription, SubscriptionControlManager.SubInfo> _subscriptions = new SynchronizedDictionary<Subscription, SubscriptionControlManager.SubInfo>();
        private readonly ISubscriptionProvider _provider;

        public SubscriptionControlManager( ISubscriptionProvider provider )
        {
            this._provider = provider;
            provider.SubscriptionStopped += ( Action<Subscription, Exception> ) ( ( s, _ ) => this.CheckUnsubscribe( s, false ) );
            provider.SubscriptionStarted += ( Action<Subscription> ) ( s => this.CheckUnsubscribe( s, false ) );
            provider.SubscriptionFailed += ( Action<Subscription, Exception, bool> ) ( ( s, _1, _2 ) => this.CheckUnsubscribe( s, false ) );
            provider.SubscriptionOnline += ( Action<Subscription> ) ( s => this.CheckUnsubscribe( s, false ) );
            provider.SubscriptionReceived += ( Action<Subscription, object> ) ( ( s, _ ) => this.CheckUnsubscribe( s, false ) );
        }

        private void CheckUnsubscribe( Subscription sub, bool unsub = false )
        {
            SubscriptionControlManager.SubInfo subInfo = (SubscriptionControlManager.SubInfo) CollectionHelper.TryGetValue<Subscription, SubscriptionControlManager.SubInfo>( this._subscriptions,  sub);
            if ( subInfo == null )
                return;
            if ( unsub )
                subInfo.IsSubscribed = false;
            if ( subInfo.IsSubscribed || !sub.State.IsActive() )
                return;
            this._subscriptions.Remove( sub );
            this._provider.UnSubscribe( sub );
        }

        public void Subscribe( object handler, Subscription subscription )
        {
            ( ( BaseCollection<object, ISet<object>> ) ( ( SubscriptionControlManager.SubInfo ) CollectionHelper.SafeAdd<Subscription, SubscriptionControlManager.SubInfo>(  this._subscriptions,  subscription ) ).Listeners ).Add( handler );
            this._provider.Subscribe( subscription );
        }

        public object [ ] Get( Subscription subscription )
        {
            var m1 = CollectionHelper.TryGetValue<Subscription, SubscriptionControlManager.SubInfo>( this._subscriptions,  subscription);
            return ( m1 != null ? ( ( IEnumerable<object> ) ( ( SubscriptionControlManager.SubInfo ) m1 ).Listeners ).ToArray<object>() : ( object [ ] ) null ) ?? Array.Empty<object>();
        }

        public void Unsubscribe( Subscription subscription )
        {
            this.CheckUnsubscribe( subscription, true );
        }

        private class SubInfo
        {
            public bool IsSubscribed { get; set; } = true;

            public CachedSynchronizedSet<object> Listeners { get; } = new CachedSynchronizedSet<object>();
        }
    }
}
