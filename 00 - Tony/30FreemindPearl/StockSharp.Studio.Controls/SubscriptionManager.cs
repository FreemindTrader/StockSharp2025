
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
        private static readonly int? _defaultMaxDepth = ConfigManager.TryGet<int?>( "maxDepth", new int?() );
        private readonly IStudioControl _control;

        public SubscriptionManager( IStudioControl control )
        {
            IStudioControl studioControl = control;
            if ( studioControl == null )
                throw new ArgumentNullException( nameof( control ) );
            this._control = studioControl;
            this.MaxDepth = SubscriptionManager._defaultMaxDepth;
        }

        public int? MaxDepth { get; set; }

        public Subscription CreateSubscription(
          DataType dataType,
          Action<Subscription> configure = null )
        {
            return this.CreateSubscription( TraderHelper.AllSecurity, dataType, configure );
        }

        public Subscription CreateSubscription(
          Security security,
          DataType dataType,
          Action<Subscription> configure = null )
        {
            if ( security == null )
                throw new ArgumentNullException( nameof( security ) );
            Subscription subscription = new Subscription( dataType, security );
            this._subscriptions.Add( subscription, Tuple.Create<Security, DataType>( security, dataType ) );
            if ( configure != null )
                configure( subscription );
            if ( ( Equatable<DataType> )dataType == DataType.MarketDepth )
            {
                MarketDataMessage subscriptionMessage = subscription.SubscriptionMessage as MarketDataMessage;
                if ( subscriptionMessage != null && !subscriptionMessage.MaxDepth.HasValue )
                    subscriptionMessage.MaxDepth = this.MaxDepth;
            }
            new SubscribeCommand( subscription ).Process( ( object )this._control, false );
            return subscription;
        }

        public void RemoveSubscription( Subscription subscription )
        {
            if ( !this._subscriptions.Remove( subscription ) )
                return;
            new UnSubscribeCommand( subscription ).Process( ( object )this._control, false );
        }

        public void RemoveSubscriptions( Security security )
        {
            this.RemoveSubscriptions( security, ( DataType )null );
        }

        public void RemoveSubscriptions( Security security, DataType dataType )
        {
            foreach ( Subscription subscription in this._subscriptions.SyncGet<SynchronizedDictionary<Subscription, Tuple<Security, DataType>>, Subscription[ ]>( ( Func<SynchronizedDictionary<Subscription, Tuple<Security, DataType>>, Subscription[ ]> )( c => c.Where<KeyValuePair<Subscription, Tuple<Security, DataType>>>( ( Func<KeyValuePair<Subscription, Tuple<Security, DataType>>, bool> )( p =>
                     {
                         if ( p.Value.Item1 != security )
                             return false;
                         if ( !( ( Equatable<DataType> )dataType == ( DataType )null ) )
                             return ( Equatable<DataType> )p.Value.Item2 == dataType;
                         return true;
                     } ) ).Select<KeyValuePair<Subscription, Tuple<Security, DataType>>, Subscription>( ( Func<KeyValuePair<Subscription, Tuple<Security, DataType>>, Subscription> )( p => p.Key ) ).ToArray<Subscription>() ) ) )
                this.RemoveSubscription( subscription );
        }

        public void Dispose()
        {
            foreach ( KeyValuePair<Subscription, Tuple<Security, DataType>> keyValuePair in this._subscriptions.SyncGet<SynchronizedDictionary<Subscription, Tuple<Security, DataType>>, KeyValuePair<Subscription, Tuple<Security, DataType>>[ ]>( ( Func<SynchronizedDictionary<Subscription, Tuple<Security, DataType>>, KeyValuePair<Subscription, Tuple<Security, DataType>>[ ]> )( d => d.CopyAndClear<KeyValuePair<Subscription, Tuple<Security, DataType>>>() ) ) )
                new UnSubscribeCommand( keyValuePair.Key ).Process( ( object )this._control, false );
        }
    }
}
