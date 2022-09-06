using Ecng.Collections;
using Ecng.Common;
using StockSharp.Algo;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace StockSharp.Server.Core
{
    /// <summary>Subscription holder.</summary>
    /// <typeparam name="TSubcription">Subscription type.</typeparam>
    /// <typeparam name="TSession">Session type.</typeparam>
    /// <typeparam name="TRequestId">Request identifier type.</typeparam>
    public class SubscriptionHolder<TSubcription, TSession, TRequestId> where TSubcription : SubscriptionInfo<TSession, TRequestId> where TSession : class, IMessageListenerSession
    {

        private readonly SynchronizedDictionary<DataType, CachedSynchronizedSet<TSubcription>> _dataTypeSubscription = new SynchronizedDictionary<DataType, CachedSynchronizedSet<TSubcription>>();

        private readonly SynchronizedDictionary<Tuple<DataType, SecurityId>, CachedSynchronizedSet<TSubcription>> _dataTypeSecIDSubscriptions = new SynchronizedDictionary<Tuple<DataType, SecurityId>, CachedSynchronizedSet<TSubcription>>();

        private readonly SynchronizedDictionary<long, TSubcription> _subscriptionIdentifiers = new SynchronizedDictionary<long, TSubcription>();

        private readonly SynchronizedDictionary<MessageTypes, CachedSynchronizedSet<TSubcription>> _messageTypesSubscriptions = new SynchronizedDictionary<MessageTypes, CachedSynchronizedSet<TSubcription>>();

        private readonly SynchronizedDictionary<long, long> _unsubscribeRequests = new SynchronizedDictionary<long, long>();

        private readonly SynchronizedSet<long> _subscriptionById = new SynchronizedSet<long>();

        private readonly IMessageListener _messageListener;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Server.Core.SubscriptionHolder`3" />.
        /// </summary>
        /// <param name="server">Server.</param>
        public SubscriptionHolder( IMessageListener server ) => _messageListener = server ?? throw new ArgumentNullException( nameof( server ) );

        /// <summary>Get subscription for the specified session.</summary>
        /// <param name="session">Session.</param>
        /// <returns>Subscriptions.</returns>
        public IEnumerable<TSubcription> GetSubscriptions( IMessageListenerSession session )
        {
            if ( session == null )
            {
                throw new ArgumentNullException( nameof( session ) );
            }

            bool lockTaken = false;
            try
            {
                Monitor.Enter( _subscriptionIdentifiers.SyncRoot, ref lockTaken );
                return ( _subscriptionIdentifiers.Values.Where( p => p.Session == session ).ToArray() );
            }
            finally
            {
                if ( lockTaken )
                    Monitor.Exit( _subscriptionIdentifiers.SyncRoot );
            }
        }

        /// <summary>Subscription changed event.</summary>
        public event Action<TSubcription> SubscriptionChanged;

        /// <summary>Add new subscription.</summary>
        /// <param name="info">Subscription.</param>
        public void Add( TSubcription info )
        {
            SecurityId securityId = info != null ? info.SecurityId : throw new ArgumentNullException( nameof( info ) );

            DataType dataType = info.DataType;

            if ( dataType != null )
            {
                if ( securityId == new SecurityId() )
                    _dataTypeSubscription.SafeAdd( dataType ).Add( info );
                else
                    _dataTypeSecIDSubscriptions.SafeAdd( Tuple.Create( dataType, securityId ) ).Add( info );
            }

            foreach ( MessageTypes response in info.Responses )
            {
                _messageTypesSubscriptions.SafeAdd( response ).Add( info );
            }

            if ( info.Id != 0L )
                _subscriptionIdentifiers.Add( info.Id, info );

            var changedEvent = this.SubscriptionChanged;
            if ( changedEvent == null )
                return;
            changedEvent( info );
        }

        /// <summary>Add unsubscribe request identifier.</summary>
        /// <param name="transactionId">Request identifier.</param>
        /// <param name="originalTransactionId">ID of the original message <see cref="P:StockSharp.Messages.ITransactionIdMessage.TransactionId" /> for which this message is a response.</param>
        public void AddUnsubscribeRequest( long transactionId, long originalTransactionId ) => _unsubscribeRequests.Add( transactionId, originalTransactionId );

        /// <summary>Remove session.</summary>
        /// <param name="session">Session.</param>
        /// <returns>Subscriptions.</returns>
        public IEnumerable<TSubcription> Remove( TSession session )
        {
            if ( session == null )
            {
                throw new ArgumentNullException( nameof( session ) );
            }

            var subscriptions = new HashSet<TSubcription>();


            SyncObject syncRoot = _dataTypeSubscription.SyncRoot;
            bool lockTaken = false;
            try
            {
                Monitor.Enter( syncRoot, ref lockTaken );
                foreach ( var pair in _dataTypeSubscription.ToArray() )
                {
                    var tsubscritpions = pair.Value;
                    subscriptions.AddRange( tsubscritpions.RemoveWhere( p => p.Session == session ) );
                    if ( tsubscritpions.Count == 0 )
                        _dataTypeSubscription.Remove( pair.Key );
                }
            }
            finally
            {
                if ( lockTaken )
                    Monitor.Exit( syncRoot );
            }

            syncRoot = _dataTypeSecIDSubscriptions.SyncRoot;
            lockTaken = false;
            try
            {
                Monitor.Enter( syncRoot, ref lockTaken );
                foreach ( var pair in _dataTypeSecIDSubscriptions.ToArray() )
                {
                    var tsubscritpions = pair.Value;
                    subscriptions.AddRange( tsubscritpions.RemoveWhere( p => p.Session == session ) );
                    if ( tsubscritpions.Count == 0 )
                        _dataTypeSecIDSubscriptions.Remove( pair.Key );
                }
            }
            finally
            {
                if ( lockTaken )
                    Monitor.Exit( syncRoot );
            }

            syncRoot = _messageTypesSubscriptions.SyncRoot;
            lockTaken = false;
            try
            {
                Monitor.Enter( syncRoot, ref lockTaken );
                foreach ( var pair in _messageTypesSubscriptions.ToArray() )
                {
                    var tsubscritpions = pair.Value;
                    subscriptions.AddRange( tsubscritpions.RemoveWhere( p => p.Session == session ) );
                    if ( tsubscritpions.Count == 0 )
                        _messageTypesSubscriptions.Remove( pair.Key );
                }
            }
            finally
            {
                if ( lockTaken )
                    Monitor.Exit( syncRoot );
            }

            syncRoot = _subscriptionIdentifiers.SyncRoot;

            lockTaken = false;
            try
            {
                Monitor.Enter( syncRoot, ref lockTaken );

                var subscriptionIds = _subscriptionIdentifiers.RemoveWhere( p => p.Value.Session == session );

                subscriptions.AddRange( subscriptionIds.Select( p => p.Value ) );
            }
            finally
            {
                if ( lockTaken )
                    Monitor.Exit( syncRoot );
            }

            foreach ( TSubcription subcription in subscriptions )
            {
                subcription.State = subcription.State.ChangeSubscriptionState( SubscriptionStates.Stopped, subcription.Id, _messageListener );
                var changedEvent = this.SubscriptionChanged;
                if ( changedEvent != null )
                    changedEvent( subcription );
            }
            return subscriptions;
        }

        /// <summary>Remove subscription.</summary>
        /// <param name="info">Subscription.</param>
        public void Remove( TSubcription info )
        {
            SyncObject syncRoot = _dataTypeSubscription.SyncRoot;
            bool lockTaken = false;

            try
            {
                Monitor.Enter( syncRoot, ref lockTaken );
                foreach ( var keyValuePair in _dataTypeSubscription.ToArray() )
                {
                    var subscriptions = keyValuePair.Value;
                    if ( subscriptions.Remove( info ) && subscriptions.Count == 0 )
                        _dataTypeSubscription.Remove( keyValuePair.Key );
                }
            }
            finally
            {
                if ( lockTaken )
                    Monitor.Exit( syncRoot );
            }


            syncRoot = _dataTypeSecIDSubscriptions.SyncRoot;
            lockTaken = false;

            try
            {
                Monitor.Enter( syncRoot, ref lockTaken );
                foreach ( var keyValuePair in _dataTypeSecIDSubscriptions.ToArray() )
                {
                    var subscriptions = keyValuePair.Value;
                    if ( subscriptions.Remove( info ) && subscriptions.Count == 0 )
                        _dataTypeSecIDSubscriptions.Remove( keyValuePair.Key );
                }
            }
            finally
            {
                if ( lockTaken )
                    Monitor.Exit( syncRoot );
            }

            syncRoot = _messageTypesSubscriptions.SyncRoot;
            lockTaken = false;

            try
            {
                Monitor.Enter( syncRoot, ref lockTaken );
                foreach ( var keyValuePair in _messageTypesSubscriptions.ToArray() )
                {
                    var subscriptions = keyValuePair.Value;
                    if ( subscriptions.Remove( info ) && subscriptions.Count == 0 )
                        _messageTypesSubscriptions.Remove( keyValuePair.Key );
                }
            }
            finally
            {
                if ( lockTaken )
                    Monitor.Exit( syncRoot );
            }

            _subscriptionIdentifiers.Remove( info.Id );
        }

        /// <summary>Try get subscription by the specified identifier.</summary>
        /// <param name="id">Identifier.</param>
        /// <returns>Subscription.</returns>
        public TSubcription TryGetById( long id ) => _subscriptionIdentifiers.TryGetValue( id );

        /// <summary>Clear state.</summary>
        public void Clear()
        {
            _messageTypesSubscriptions.Clear();
            _subscriptionIdentifiers.Clear();
            _dataTypeSecIDSubscriptions.Clear();
            _dataTypeSubscription.Clear();
            _unsubscribeRequests.Clear();
            _subscriptionById.Clear();
        }

        /// <summary>
        /// Determines has subscription for the specified data type and security.
        /// </summary>
        /// <param name="dataType">Data type info.</param>
        /// <param name="securityId">Security ID.</param>
        /// <returns>Check result.</returns>
        public bool HasSubscriptions( DataType dataType, SecurityId securityId )
        {
            var subscriptions = _dataTypeSubscription.TryGetValue( dataType ) ?? _dataTypeSecIDSubscriptions.TryGetValue( Tuple.Create( dataType, securityId ) );
            return subscriptions != null && subscriptions.Count > 0;
        }

        /// <summary>
        /// Try get and stop subscription by the specified identifier.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <returns>Subscription.</returns>
        public TSubcription TryGetSubscriptionAndStop( long id ) => TryGetSubscription( id, new SubscriptionStates?( SubscriptionStates.Stopped ) );

        /// <summary>
        /// Try get subscription by the specified identifier and swith into new state.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="state">State.</param>
        /// <returns>Subscription.</returns>
        public TSubcription TryGetSubscription( long id, SubscriptionStates? state )
        {
            if ( id == 0L )
                return default( TSubcription );
            TSubcription byId = TryGetById( id );
            if ( byId == null )
            {
                if ( _subscriptionById.TryAdd( id ) )
                    _messageListener.AddWarningLog( LocalizedStrings.SubscriptionNonExist, ( object )id );
                return default( TSubcription );
            }
            if ( state.HasValue )
            {
                byId.State = byId.State.ChangeSubscriptionState( state.Value, id, _messageListener );
                Action<TSubcription> changedEvents = this.SubscriptionChanged;
                if ( changedEvents != null )
                    changedEvents( byId );
            }
            if ( ( state.HasValue ? ( !state.GetValueOrDefault().IsActive() ? 1 : 0 ) : 0 ) != 0 )
                Remove( byId );
            return byId;
        }

        private IEnumerable<TSubcription> GetSubscriptions( MessageTypes type, long transId )
        {
            if ( transId != 0L )
            {
                SubscriptionStates? state;
                switch ( type )
                {
                    case MessageTypes.Error:
                    case MessageTypes.ChangePassword:
                    case MessageTypes.SubscriptionFinished:
                        state = new SubscriptionStates?( SubscriptionStates.Finished );
                        break;
                    case MessageTypes.SubscriptionOnline:
                        state = new SubscriptionStates?( SubscriptionStates.Online );
                        break;
                    default:
                        state = new SubscriptionStates?();
                        break;
                }
                return CreateNewSubscription( TryGetSubscription( transId, state ), true );
            }

            var subscriptions = _messageTypesSubscriptions.TryGetValue( type );

            return ( subscriptions != null ? subscriptions.Cache.Where( p => !p.Suspend ) : null ?? Enumerable.Empty<TSubcription>() );
        }

        private IEnumerable<TSubcription> CreateNewSubscription( TSubcription subscription, bool isSubspended )
        {
            if ( subscription == null || isSubspended && subscription.Suspend )
                return Enumerable.Empty<TSubcription>();

            return new TSubcription[1] { subscription };
        }

        private IEnumerable<TSubcription> GetSubscriptions( DataType dataType, SecurityId secId, long transId )
        {
            if ( transId != 0L )
                return CreateNewSubscription( TryGetSubscription( transId, new SubscriptionStates?() ), true );

            var cache = ( _dataTypeSubscription.TryGetValue( dataType )?.Cache ) ?? Enumerable.Empty<TSubcription>();

            if ( _dataTypeSecIDSubscriptions.TryGetValue( Tuple.Create( dataType, secId ), out var subscription ) )
                cache = cache.Concat( subscription.Cache );

            return cache.Where<TSubcription>( p => p.State == SubscriptionStates.Online && !p.Suspend ).Distinct<TSubcription>();
        }

        /// <summary>Get subscription for the specified message.</summary>
        /// <param name="message">Message.</param>
        /// <returns>Subscriptions.</returns>
        public IEnumerable<TSubcription> GetSubscriptions( Message message )
        {
            switch ( message.Type )
            {
                case MessageTypes.Level1Change:
                    {
                        return GetSubscriptions( DataType.Level1, ( ( Level1ChangeMessage )message ).SecurityId, ( ( BaseSubscriptionIdMessage<Level1ChangeMessage> )message ).OriginalTransactionId );
                    }

                case MessageTypes.QuoteChange:
                    {
                        return GetSubscriptions( DataType.MarketDepth, ( ( QuoteChangeMessage )message ).SecurityId, ( ( BaseSubscriptionIdMessage<QuoteChangeMessage> )message ).OriginalTransactionId );
                    }

                case MessageTypes.Execution:
                    {
                        ExecutionMessage executionMessage = ( ExecutionMessage )message;
                        long originalTransactionId1 = executionMessage.OriginalTransactionId;
                        ExecutionTypes? executionType = executionMessage.ExecutionType;
                        if ( executionType.HasValue )
                        {
                            switch ( executionType.GetValueOrDefault() )
                            {
                                case ExecutionTypes.Tick:
                                    return GetSubscriptions( DataType.Ticks, executionMessage.SecurityId, originalTransactionId1 );
                                case ExecutionTypes.Transaction:
                                    TSubcription byId = TryGetById( originalTransactionId1 );
                                    if ( byId == null )
                                        return Enumerable.Empty<TSubcription>();
                                    if ( executionMessage.TransactionId != 0L )
                                        _subscriptionIdentifiers[executionMessage.TransactionId] = byId;
                                    return CreateNewSubscription( byId, true );
                                case ExecutionTypes.OrderLog:
                                    return GetSubscriptions( DataType.OrderLog, executionMessage.SecurityId, originalTransactionId1 );
                            }
                        }
                        throw new ArgumentOutOfRangeException();
                    }

                case MessageTypes.SubscriptionResponse:
                    {
                        SubscriptionResponseMessage responseMsg = ( SubscriptionResponseMessage )message;
                        long originalTransactionId2 = responseMsg.OriginalTransactionId;
                        long id;
                        return CreateNewSubscription( !_unsubscribeRequests.TryGetAndRemove( originalTransactionId2, out id ) ? TryGetSubscription( originalTransactionId2, new SubscriptionStates?( responseMsg.Error == null ? SubscriptionStates.Active : SubscriptionStates.Error ) ) : TryGetSubscription( id, new SubscriptionStates?( SubscriptionStates.Stopped ) ), false );
                    }

                default:
                    {
                        if ( message is IOriginalTransactionIdMessage transactionIdMessage2 )
                        {
                            if ( transactionIdMessage2.OriginalTransactionId != 0L )
                                return GetSubscriptions( message.Type, transactionIdMessage2.OriginalTransactionId );
                            if ( message is ISubscriptionIdMessage subscriptionIdMessage3 && message is ISecurityIdMessage securityIdMessage3 )
                                return GetSubscriptions( subscriptionIdMessage3.DataType, securityIdMessage3.SecurityId, 0L );
                        }
                        return Enumerable.Empty<TSubcription>();
                    }
            }
        }

    }
}
