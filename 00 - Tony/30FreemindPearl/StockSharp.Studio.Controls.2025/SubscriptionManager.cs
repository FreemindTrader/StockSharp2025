// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.SubscriptionManager
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using Ecng.Collections;
using Ecng.Common;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using StockSharp.Studio.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Studio.Controls
{
    public class SubscriptionManager : Disposable
    {
        private readonly IStudioControl _control;
        private readonly SynchronizedDictionary<Subscription, Tuple<Security, DataType>> _subscriptions;

        public SubscriptionManager( IStudioControl control )
        {
            IStudioControl studioControl = control;
            if ( studioControl == null )
                throw new ArgumentNullException( nameof( control ) );
            this._control = studioControl;
            this._subscriptions = new SynchronizedDictionary<Subscription, Tuple<Security, DataType>>();
            
        }

        public Subscription CreateSubscription(
          DataType dataType,
          Action<Subscription> configure = null )
        {
            return this.CreateSubscription( EntitiesExtensions.AllSecurity, dataType, configure );
        }

        public Subscription CreateSubscription(
          Security security,
          DataType dataType,
          Action<Subscription> configure = null )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            Subscription subscription = new Subscription(dataType, security);
            this._subscriptions.Add( subscription, Tuple.Create<Security, DataType>( security, dataType ) );
            if ( configure != null )
                configure( subscription );
            new SubscribeCommand( subscription ).Process(  this._control, false );
            return subscription;
        }

        public void RemoveSubscription( Subscription subscription )
        {
            if ( !this._subscriptions.Remove( subscription ) || !subscription.State.IsActive() )
                return;
            new UnSubscribeCommand( subscription ).Process(  this._control, false );
        }

        public void RemoveSubscriptions( Security security )
        {
            this.RemoveSubscriptions( security, ( DataType ) null );
        }

        public void RemoveSubscriptions( Security security, DataType dataType )
        {
            foreach ( Subscription subscription in _subscriptions.SyncGet( c =>  c.Where(  p =>
            {
                if ( !p.Key.State.IsActive() || p.Value.Item1 != security )
                    return false;

                if ( !( dataType == null ) )
                    return p.Value.Item2 == dataType;
                return true;
            } ).Select( p => p.Key ).ToArray() ) )
                this.RemoveSubscription( subscription );
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            foreach ( var keyValuePair in _subscriptions.SyncGet( d => d.CopyAndClear() ) )
            {
                Subscription key;
                Tuple<Security, DataType> tuple;
                keyValuePair.Deconstruct( out key, out tuple );
                Subscription subscription = key;
                if ( subscription.State.IsActive() )
                    new UnSubscribeCommand( subscription ).Process(  this._control, false );
            }
        }
    }
}
