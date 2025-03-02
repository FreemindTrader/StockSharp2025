using Ecng.Collections;
using Ecng.Common;
using StockSharp.Localization;
using System;
using System.Diagnostics;
using System.Threading;

namespace StockSharp.Server.Core
{
    /// <summary>
    /// In memory implementation of <see cref="T:StockSharp.Server.Core.ITransactionIdStorage" />.
    /// </summary>
    public class InMemoryTransactionIdStorage : ITransactionIdStorage
    {
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private readonly IdGenerator _idGenerator;
        [DebuggerBrowsable( DebuggerBrowsableState.Never )]
        private readonly SynchronizedDictionary<string, ISessionTransactionIdStorage> _sessionIdStorage = new SynchronizedDictionary<string, ISessionTransactionIdStorage>();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Server.Core.InMemoryTransactionIdStorage" />.
        /// </summary>
        /// <param name="idGenerator">Transaction id generator.</param>
        public InMemoryTransactionIdStorage( IdGenerator idGenerator ) => _idGenerator = idGenerator ?? throw new ArgumentNullException( nameof( idGenerator ) );

        ISessionTransactionIdStorage ITransactionIdStorage.Get( string sessionId, bool persistable )
        {
            return !persistable ? new SessionTransactionIdStorage( _idGenerator ) : _sessionIdStorage.SafeAdd( sessionId, p => new SessionTransactionIdStorage( _idGenerator ) );
        }



        private sealed class SessionTransactionIdStorage : ISessionTransactionIdStorage
        {
            private readonly IdGenerator _idGenerator;
            private readonly SynchronizedPairSet<long, string> _longStringPairSet = new SynchronizedPairSet<long, string>();

            public SessionTransactionIdStorage( IdGenerator generator ) => _idGenerator = generator ?? throw new ArgumentNullException( nameof( generator ) );

            string ISessionTransactionIdStorage.TryGetRequestId(
              long _param1 )
            {
                return _longStringPairSet.TryGetValue( _param1 );
            }

            long? ISessionTransactionIdStorage.TryGetTransactionId( string _param1 )
            {
                long key;
                return _longStringPairSet.TryGetKey( _param1, out key ) ? new long?( key ) : new long?();
            }

            string ISessionTransactionIdStorage.CreateRequestId()
            {
                long nextId = _idGenerator.GetNextId();
                string str = nextId.To<string>();
                GetNextId( str, nextId );
                return str;
            }

            long ISessionTransactionIdStorage.CreateTransactionId( string _param1 )
            {
                if ( _param1.IsEmpty() )
                    throw new ArgumentNullException( nameof( _param1 ) );
                long nextId = _idGenerator.GetNextId();
                GetNextId( _param1, nextId );
                return nextId;
            }

            private void GetNextId( string _param1, long _param2 )
            {
                SyncObject syncRoot = _longStringPairSet.SyncRoot;
                bool flag = false;
                try
                {
                    Monitor.Enter( syncRoot, ref flag );
                    if ( _longStringPairSet.ContainsKey( _param2 ) )
                        throw new ArgumentException( LocalizedStrings.Str415Params.Put( _param2 ) );
                    if ( _longStringPairSet.ContainsValue( _param1 ) )
                        throw new ArgumentException( LocalizedStrings.Str415Params.Put( _param1 ) );
                    _longStringPairSet.Add( _param2, _param1 );
                }
                finally
                {
                    if ( flag )
                        Monitor.Exit( syncRoot );
                }
            }

            bool ISessionTransactionIdStorage.RemoveRequestId( string _param1 )
            {
                return _longStringPairSet.RemoveByValue( _param1 );
            }

            bool ISessionTransactionIdStorage.RemoveTransactionId( long _param1 )
            {
                return _longStringPairSet.Remove( _param1 );
            }
        }
    }
}
