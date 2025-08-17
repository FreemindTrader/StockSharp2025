// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.SubscriptionManager
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.Linq;
using Ecng.Collections;
using Ecng.Common;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using StockSharp.Studio.Core.Commands;

#nullable disable
namespace StockSharp.Studio.Controls;

public class SubscriptionManager : Disposable
{
    private readonly IStudioControl _control;
    private readonly SynchronizedDictionary<Subscription, Tuple<Security, DataType>> _subscriptions;

    public SubscriptionManager(IStudioControl control)
    {
        this._control = control ?? throw new ArgumentNullException(nameof(control));
        this._subscriptions = new SynchronizedDictionary<Subscription, Tuple<Security, DataType>>();

    }

    public Subscription CreateSubscription(DataType dataType, Action<Subscription> configure = null)
    {
        return this.CreateSubscription(EntitiesExtensions.AllSecurity, dataType, configure);
    }

    public Subscription CreateSubscription(
      Security security,
      DataType dataType,
      Action<Subscription> configure = null)
    {
        Subscription subscription = security != null ? new Subscription(dataType, security) : throw new ArgumentNullException(nameof(security));
        this._subscriptions.Add(subscription, Tuple.Create<Security, DataType>(security, dataType));
        if (configure != null)
            configure(subscription);
        new SubscribeCommand(subscription).Process((object)this._control);
        return subscription;
    }

    public void RemoveSubscription(Subscription subscription)
    {
        if (!this._subscriptions.Remove(subscription) || !SubscriptionExtensions.IsActive(subscription.State))
            return;
        new UnSubscribeCommand(subscription).Process((object)this._control);
    }

    public void RemoveSubscriptions(Security security)
    {
        this.RemoveSubscriptions(security, (DataType)null);
    }

    public void RemoveSubscriptions(Security security, DataType dataType)
    {
        foreach (Subscription subscription in _subscriptions.SyncGet(c => c.Where(p =>
        {
            if (!p.Key.State.IsActive() || p.Value.Item1 != security)
                return false;

            if (!(dataType == null))
                return p.Value.Item2 == dataType;
            return true;
        }).Select(p => p.Key).ToArray()))
            this.RemoveSubscription(subscription);
    }

    protected override void DisposeManaged()
    {
        base.DisposeManaged();
        foreach ((Subscription subscription, Tuple<Security, DataType> _) in _subscriptions.SyncGet((d => d.CopyAndClear())))
        {
            if (SubscriptionExtensions.IsActive(subscription.State))
                new UnSubscribeCommand(subscription).Process((object)this._control);
        }
    }
}
