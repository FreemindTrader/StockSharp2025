using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace StockSharp.Studio.Controls
{
    public abstract class BaseSubscriptionStudioControl : BaseStudioControl
    {
        private readonly CachedSynchronizedSet<string> _securityIds = new CachedSynchronizedSet<string>(
            StringComparer.InvariantCultureIgnoreCase );
        private Subscription _subscription;
        private readonly SubscriptionManager _subscriptionManager;

        protected BaseSubscriptionStudioControl()
        {
            _subscriptionManager = new SubscriptionManager( this );
            CommandService.Register<SecuritiesRemovedCommand>(
                this,
                false,
                cmd =>
                {
                    bool found = false;

                    foreach ( Security security in cmd.Securities )
                    {
                        if ( _securityIds.Remove( security.Id ) )
                        {
                            _subscriptionManager.RemoveSubscriptions( security );
                            found = true;
                        }
                    }

                    if ( !found )
                    {
                        return;
                    }

                    RaiseChangedCommand();
                },
                null );
        }

        protected void AddSecurities( IEnumerable<Security> newSecurities, IEnumerable<Security> existing )
        {
            Security[ ] tobeRemoved = existing.Except( newSecurities ).ToArray();
            Security[ ] tobeAdded = newSecurities.Except( existing ).ToArray();

            if ( tobeRemoved.Length == 0 && tobeAdded.Length == 0 )
            {
                return;
            }

            if ( _securityIds.IsEmpty() && _subscription != null )
            {
                _subscriptionManager.RemoveSubscription( _subscription );
                _subscription = null;
            }

            _securityIds.SyncDo(
                list =>
                {
                    list.Clear();
                    list.AddRange( newSecurities.Select( s => s.Id ) );
                } );

            tobeRemoved.ForEach( s => _subscriptionManager.RemoveSubscriptions( s ) );
            tobeAdded.ForEach( s => _subscriptionManager.CreateSubscription( s, DataType ) );

            RaiseChangedCommand();
        }

        protected void FilterConfig()
        {
            Security[ ] securities = Securities;
            var wnd = new SecuritiesWindowEx() { SecurityProvider = SecurityProvider, SelectedSecurities = securities };

            if ( !wnd.ShowModal( this ) )
            {
                return;
            }

            AddSecurities( wnd.SelectedSecurities, securities );
        }

        protected void LoadSubscriptions( SettingsStorage storage )
        {
            _securityIds.SyncDo(
                list =>
                {
                    list.Clear();
                    list.AddRange( storage.GetValue( nameof( Securities ), Array.Empty<string>() ) );
                } );

            if ( _securityIds.IsEmpty() )
            {
                return;
            }

            foreach ( Security security in Securities )
            {
                _subscriptionManager.CreateSubscription( security, DataType );
            }
        }

        protected void SaveSubscriptions( SettingsStorage storage )
        { storage.SetValue( nameof( Securities ), _securityIds.Cache ); }

        protected abstract DataType DataType { get; }

        protected Security[ ] Securities => _securityIds.Cache
            .Select( SecurityProvider.LookupById )
            .Where( s => s != null )
            .ToArray();

        public override void Dispose( CloseReason reason )
        {
            CommandService.UnRegister<SecuritiesRemovedCommand>( this );
            _subscriptionManager.Dispose();
            base.Dispose( reason );
        }
    }
}
