// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.BaseSubscriptionStudioControl
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Ecng.Collections;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using StockSharp.Studio.Core.Commands;

#nullable disable
namespace StockSharp.Studio.Controls;

public abstract class BaseSubscriptionStudioControl : BaseStudioControl
{
    private readonly SubscriptionManager _subscriptionManager;
    private Subscription _subscription;
    private readonly CachedSynchronizedSet<string> _securityIds = new CachedSynchronizedSet<string>((IEqualityComparer<string>)StringComparer.InvariantCultureIgnoreCase);

    protected abstract DataType DataType { get; }

    protected BaseSubscriptionStudioControl()
    {
        this._subscriptionManager = new SubscriptionManager((IStudioControl)this);
        this.Register<EntitiesRemovedCommand<Security>>((object)this, false, (Action<EntitiesRemovedCommand<Security>>)(cmd =>
        {
            bool flag = false;
            foreach (Security entity in cmd.Entities)
            {
                if (((BaseCollection<string, ISet<string>>)this._securityIds).Remove(entity.Id))
                {
                    this._subscriptionManager.RemoveSubscriptions(entity);
                    flag = true;
                }
            }
            if (!flag)
                return;
            this.RaiseChangedCommand();
        }));
    }

    protected Security[] Securities
    {
        get
        {
            return CollectionHelper.WhereNotNull<Security>(((IEnumerable<string>)this._securityIds.Cache).Select<string, Security>(new Func<string, Security>((BaseStudioControl.SecurityProvider).LookupById))).ToArray<Security>();
        }
    }

    protected void LoadSubscriptions(SettingsStorage storage)
    {
        CollectionHelper.SyncDo<CachedSynchronizedSet<string>>(this._securityIds, (Action<CachedSynchronizedSet<string>>)(list =>
        {
            ((BaseCollection<string, ISet<string>>)list).Clear();
            ((SynchronizedSet<string>)list).AddRange((IEnumerable<string>)storage.GetValue<string[]>("Securities", Array.Empty<string>()));
        }));
        if (CollectionHelper.IsEmpty<string>((ICollection<string>)this._securityIds))
            return;
        foreach (Security security in this.Securities)
            this._subscriptionManager.CreateSubscription(security, this.DataType);
    }

    protected void SaveSubscriptions(SettingsStorage storage)
    {
        storage.SetValue<string[]>("Securities", this._securityIds.Cache);
    }

    protected void FilterConfig()
    {
        Security[] securities = this.Securities;
        SecuritiesWindowEx wnd = new SecuritiesWindowEx()
        {
            SecurityProvider = BaseStudioControl.SecurityProvider,
            SelectedSecurities = (IEnumerable<Security>)securities
        };
        if (!wnd.ShowModal((DependencyObject)this))
            return;
        this.AddSecurities(wnd.SelectedSecurities, (IEnumerable<Security>)securities);
    }

    protected void AddSecurities(IEnumerable<Security> newSecurities, IEnumerable<Security> existing)
    {
        Security[] array1 = existing.Except<Security>(newSecurities).ToArray<Security>();
        Security[] array2 = newSecurities.Except<Security>(existing).ToArray<Security>();
        if (array1.Length == 0 && array2.Length == 0)
            return;
        if (CollectionHelper.IsEmpty<string>((ICollection<string>)this._securityIds) && this._subscription != null)
        {
            this._subscriptionManager.RemoveSubscription(this._subscription);
            this._subscription = (Subscription)null;
        }
        CollectionHelper.SyncDo<CachedSynchronizedSet<string>>(this._securityIds, (Action<CachedSynchronizedSet<string>>)(list =>
        {
            ((BaseCollection<string, ISet<string>>)list).Clear();
            ((SynchronizedSet<string>)list).AddRange(newSecurities.Select<Security, string>((Func<Security, string>)(s => s.Id)));
        }));
        CollectionHelper.ForEach<Security>((IEnumerable<Security>)array1, (Action<Security>)(s => this._subscriptionManager.RemoveSubscriptions(s)));
        CollectionHelper.ForEach<Security>((IEnumerable<Security>)array2, (Action<Security>)(s => this._subscriptionManager.CreateSubscription(s, this.DataType)));
        this.RaiseChangedCommand();
    }

    public override void Dispose(CloseReason reason)
    {
        this._subscriptionManager.Dispose();
        base.Dispose(reason);
    }
}
