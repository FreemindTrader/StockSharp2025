// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.BaseSubscriptionStudioControl
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using Ecng.Collections;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using StockSharp.Studio.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Studio.Controls
{
    public abstract class BaseSubscriptionStudioControl : BaseStudioControl
    {
        private readonly CachedSynchronizedSet<string> _securityIds = new CachedSynchronizedSet<string>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
        private readonly SubscriptionManager _subscriptionManager;
        private Subscription _subscription;

        protected abstract DataType DataType { get; }

        protected BaseSubscriptionStudioControl()
        {
            this._subscriptionManager = new SubscriptionManager( ( IStudioControl ) this );
            this.Register<EntitiesRemovedCommand<Security>>(  this, false, ( Action<EntitiesRemovedCommand<Security>> ) ( cmd =>
            {
                bool flag = false;
                foreach ( Security entity in cmd.Entities )
                {
                    if ( ( ( BaseCollection<string, ISet<string>> ) this._securityIds ).Remove( entity.Id ) )
                    {
                        this._subscriptionManager.RemoveSubscriptions( entity );
                        flag = true;
                    }
                }
                if ( !flag )
                    return;
                this.RaiseChangedCommand();
            } ), ( Func<EntitiesRemovedCommand<Security>, bool> ) null );
        }

        protected Security [ ] Securities
        {
            get
            {
                return ( this._securityIds.Cache ).Select<string, Security>( new Func<string, Security>( ( BaseStudioControl.SecurityProvider ).LookupById ) ).Where<Security>( ( Func<Security, bool> ) ( s => s != null ) ).ToArray<Security>();
            }
        }

        protected void LoadSubscriptions( SettingsStorage storage )
        {
            CollectionHelper.SyncDo<CachedSynchronizedSet<string>>(  this._securityIds,  ( list =>
            {
                ( ( BaseCollection<string, ISet<string>> ) list ).Clear();
                ( ( SynchronizedSet<string> ) list ).AddRange( ( IEnumerable<string> ) storage.GetValue<string [ ]>( "Securities",  Array.Empty<string>() ) );
            } ) );
            if ( CollectionHelper.IsEmpty<string>(  this._securityIds ) )
                return;
            foreach ( Security security in this.Securities )
                this._subscriptionManager.CreateSubscription( security, this.DataType, ( Action<Subscription> ) null );
        }

        protected void SaveSubscriptions( SettingsStorage storage )
        {
            storage.SetValue<string [ ]>( "Securities",  this._securityIds.Cache );
        }

        protected void FilterConfig()
        {
            Security[] securities = this.Securities;
            SecuritiesWindowEx wnd = new SecuritiesWindowEx()
            {
                SecurityProvider = BaseStudioControl.SecurityProvider,
                SelectedSecurities = (IEnumerable<Security>) securities
            };
            if ( !wnd.ShowModal(  this ) )
                return;
            this.AddSecurities( wnd.SelectedSecurities, ( IEnumerable<Security> ) securities );
        }

        protected void AddSecurities(
          IEnumerable<Security> newSecurities,
          IEnumerable<Security> existing )
        {
            Security[] array1 = existing.Except<Security>(newSecurities).ToArray<Security>();
            Security[] array2 = newSecurities.Except<Security>(existing).ToArray<Security>();
            if ( array1.Length == 0 && array2.Length == 0 )
                return;
            if ( CollectionHelper.IsEmpty<string>(  this._securityIds ) && this._subscription != null )
            {
                this._subscriptionManager.RemoveSubscription( this._subscription );
                this._subscription = ( Subscription ) null;
            }
            CollectionHelper.SyncDo<CachedSynchronizedSet<string>>(  this._securityIds,  ( list =>
            {
                ( ( BaseCollection<string, ISet<string>> ) list ).Clear();
                ( ( SynchronizedSet<string> ) list ).AddRange( newSecurities.Select<Security, string>( ( Func<Security, string> ) ( s => s.Id ) ) );
            } ) );
            CollectionHelper.ForEach<Security>(  array1,  ( s => this._subscriptionManager.RemoveSubscriptions( s ) ) );
            CollectionHelper.ForEach<Security>(  array2,  ( s => this._subscriptionManager.CreateSubscription( s, this.DataType, ( Action<Subscription> ) null ) ) );
            this.RaiseChangedCommand();
        }

        public override void Dispose( CloseReason reason )
        {
            this._subscriptionManager.Dispose();
            base.Dispose( reason );
        }
    }
}
