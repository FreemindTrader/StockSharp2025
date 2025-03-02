
using Ecng.Collections;
using Ecng.Common;
using Ecng.Configuration;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Studio.Controls
{
    public class SubscriptionManager : IDisposable
    {
        private readonly SynchronizedDictionary<Subscription, Tuple<Security, DataType>> _subscriptions = new SynchronizedDictionary<Subscription, Tuple<Security, DataType>>();
        private static readonly int? _defaultMaxDepth = ConfigManager.TryGet( "maxDepth", new int?() );
        private readonly IStudioControl _control;

        public SubscriptionManager( IStudioControl control )
        {
            IStudioControl studioControl = control;
            if ( studioControl == null )
                throw new ArgumentNullException( nameof( control ) );
            _control = studioControl;
            MaxDepth = _defaultMaxDepth;
        }

        public int? MaxDepth { get; set; }

        public Subscription CreateSubscription(
          DataType dataType,
          Action<Subscription> configure = null )
        {
            return CreateSubscription( TraderHelper.AllSecurity, dataType, configure );
        }

        public Subscription CreateSubscription(
          Security security,
          DataType dataType,
          Action<Subscription> configure = null )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            Subscription subscription = new Subscription( dataType, security );
            _subscriptions.Add( subscription, Tuple.Create( security, dataType ) );
            if ( configure != null )
                configure( subscription );
            if ( dataType == DataType.MarketDepth )
            {
                MarketDataMessage subscriptionMessage = subscription.SubscriptionMessage as MarketDataMessage;
                if ( subscriptionMessage != null && !subscriptionMessage.MaxDepth.HasValue )
                    subscriptionMessage.MaxDepth = MaxDepth;
            }
            new SubscribeCommand( subscription ).Process( _control, false );
            return subscription;
        }

        public void RemoveSubscription( Subscription subscription )
        {
            if ( !_subscriptions.Remove( subscription ) )
                return;
            new UnSubscribeCommand( subscription ).Process( _control, false );
        }

        public void RemoveSubscriptions( Security security )
        {
            RemoveSubscriptions( security, null );
        }

        public void RemoveSubscriptions( Security security, DataType dataType )
        {
            foreach ( Subscription subscription in _subscriptions.SyncGet( c => c.Where( p =>
                     {
                         if ( p.Value.Item1 != security )
                             return false;
                         if ( !( dataType == null ) )
                             return p.Value.Item2 == dataType;
                         return true;
                     } ).Select( p => p.Key ).ToArray() ) )
                RemoveSubscription( subscription );
        }

        public void Dispose()
        {
            foreach ( KeyValuePair<Subscription, Tuple<Security, DataType>> keyValuePair in _subscriptions.SyncGet( d => d.CopyAndClear() ) )
                new UnSubscribeCommand( keyValuePair.Key ).Process( _control, false );
        }
    }
}
