using Ecng.Collections;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace StockSharp.Studio.Core;

public class SubscriptionControlManager
{
    private readonly ISubscriptionProvider _provider;
    private readonly SynchronizedDictionary<Subscription, SubscriptionControlManager.SubInfo> _subscriptions = new SynchronizedDictionary<Subscription, SubscriptionControlManager.SubInfo>();

    public SubscriptionControlManager(ISubscriptionProvider provider)
    {
        this._provider = provider;
        provider.SubscriptionStopped += (Action<Subscription, Exception>)((s, _) => this.CheckUnsubscribe(s));
        provider.SubscriptionStarted += (Action<Subscription>)(s => this.CheckUnsubscribe(s));
        provider.SubscriptionFailed += (Action<Subscription, Exception, bool>)((s, _1, _2) => this.CheckUnsubscribe(s));
        provider.SubscriptionOnline += (Action<Subscription>)(s => this.CheckUnsubscribe(s));
        provider.SubscriptionReceived += (Action<Subscription, object>)((s, _) => this.CheckUnsubscribe(s));
    }

    private void CheckUnsubscribe(Subscription sub, bool unsub = false)
    {
        SubscriptionControlManager.SubInfo subInfo = CollectionHelper.TryGetValue<Subscription, SubscriptionControlManager.SubInfo>((IDictionary<Subscription, SubscriptionControlManager.SubInfo>)this._subscriptions, sub);
        if (subInfo == null)
            return;
        if (unsub)
            subInfo.IsSubscribed = false;
        if (subInfo.IsSubscribed || !SubscriptionExtensions.IsActive(sub.State))
            return;
        this._subscriptions.Remove(sub);
        this._provider.UnSubscribe(sub);
    }

    public void Subscribe(object handler, Subscription subscription)
    {
        ((BaseCollection<object, ISet<object>>)CollectionHelper.SafeAdd<Subscription, SubscriptionControlManager.SubInfo>((IDictionary<Subscription, SubscriptionControlManager.SubInfo>)this._subscriptions, subscription).Listeners).Add(handler);
        this._provider.Subscribe(subscription);
    }

    public object[] Get(Subscription subscription)
    {
        SubscriptionControlManager.SubInfo subInfo = CollectionHelper.TryGetValue<Subscription, SubscriptionControlManager.SubInfo>((IDictionary<Subscription, SubscriptionControlManager.SubInfo>)this._subscriptions, subscription);
        return (subInfo != null ? ((IEnumerable<object>)subInfo.Listeners).ToArray<object>() : (object[])null) ?? Array.Empty<object>();
    }

    public void Unsubscribe(Subscription subscription) => this.CheckUnsubscribe(subscription, true);

    private class SubInfo
    {
        public bool IsSubscribed { get; set; } = true;

        public CachedSynchronizedSet<object> Listeners { get; } = new CachedSynchronizedSet<object>();
    }
}
