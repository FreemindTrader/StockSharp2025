using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ecng.Data
{
    public class DatabaseConnectionCache : IPersistable
    {
        private readonly CachedSynchronizedSet<DatabaseConnectionPair> _connections = new CachedSynchronizedSet<DatabaseConnectionPair>();

        public IEnumerable<DatabaseConnectionPair> Connections
        {
            get
            {
                return _connections.Cache;
            }
        }

        public event Action<DatabaseConnectionPair> ConnectionCreated;

        public event Action<DatabaseConnectionPair> ConnectionDeleted;

        public DatabaseConnectionPair GetConnection(
          Type provider,
          string connectionString )
        {
            if ( ( object )provider == null )
                throw new ArgumentNullException( nameof( provider ) );
            if ( connectionString.IsEmpty() )
                throw new ArgumentNullException( nameof( connectionString ) );
            DatabaseConnectionPair connection = Connections.FirstOrDefault( p =>
            {
                if ( p.Provider == provider )
                    return p.ConnectionString.EqualsIgnoreCase( connectionString );
                return false;
            } );
            if ( connection == null )
            {
                connection = new DatabaseConnectionPair()
                {
                    Provider = provider,
                    ConnectionString = connectionString
                };
                AddConnection( connection );
            }
            return connection;
        }

        private void AddConnection( DatabaseConnectionPair connection )
        {
            if ( connection == null )
                throw new ArgumentNullException( nameof( connection ) );
            _connections.Add( connection );
            Action<DatabaseConnectionPair> connectionCreated = ConnectionCreated;
            if ( connectionCreated == null )
                return;
            connectionCreated( connection );
        }

        public bool DeleteConnection( DatabaseConnectionPair connection )
        {
            if ( connection == null )
                throw new ArgumentNullException( nameof( connection ) );
            if ( !_connections.Remove( connection ) )
                return false;
            Action<DatabaseConnectionPair> connectionDeleted = ConnectionDeleted;
            if ( connectionDeleted != null )
                connectionDeleted( connection );
            return true;
        }

        public void Load( SettingsStorage storage )
        {
            DatabaseConnectionPair[ ] array = storage.GetValue<IEnumerable<DatabaseConnectionPair>>( "Connections", null ).Where( p => p.Provider != null ).ToArray();
            lock ( _connections.SyncRoot )
                _connections.AddRange( array );
        }

        public void Save( SettingsStorage storage )
        {
            storage.SetValue( "Connections", Connections.Select( pair => pair.Save() ).ToArray() );
        }
    }
}
