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
                return ( IEnumerable<DatabaseConnectionPair> )this._connections.Cache;
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
            DatabaseConnectionPair connection = this.Connections.FirstOrDefault<DatabaseConnectionPair>( ( Func<DatabaseConnectionPair, bool> )( p =>
            {
                if ( p.Provider == provider )
                    return p.ConnectionString.EqualsIgnoreCase( connectionString );
                return false;
            } ) );
            if ( connection == null )
            {
                connection = new DatabaseConnectionPair()
                {
                    Provider = provider,
                    ConnectionString = connectionString
                };
                this.AddConnection( connection );
            }
            return connection;
        }

        private void AddConnection( DatabaseConnectionPair connection )
        {
            if ( connection == null )
                throw new ArgumentNullException( nameof( connection ) );
            this._connections.Add( connection );
            Action<DatabaseConnectionPair> connectionCreated = this.ConnectionCreated;
            if ( connectionCreated == null )
                return;
            connectionCreated( connection );
        }

        public bool DeleteConnection( DatabaseConnectionPair connection )
        {
            if ( connection == null )
                throw new ArgumentNullException( nameof( connection ) );
            if ( !this._connections.Remove( connection ) )
                return false;
            Action<DatabaseConnectionPair> connectionDeleted = this.ConnectionDeleted;
            if ( connectionDeleted != null )
                connectionDeleted( connection );
            return true;
        }

        public void Load( SettingsStorage storage )
        {
            DatabaseConnectionPair[ ] array = storage.GetValue<IEnumerable<DatabaseConnectionPair>>( "Connections", ( IEnumerable<DatabaseConnectionPair> )null ).Where<DatabaseConnectionPair>( ( Func<DatabaseConnectionPair, bool> )( p => p.Provider != ( Type )null ) ).ToArray<DatabaseConnectionPair>();
            lock ( this._connections.SyncRoot )
                this._connections.AddRange( ( IEnumerable<DatabaseConnectionPair> )array );
        }

        public void Save( SettingsStorage storage )
        {
            storage.SetValue<SettingsStorage[ ]>( "Connections", this.Connections.Select<DatabaseConnectionPair, SettingsStorage>( ( Func<DatabaseConnectionPair, SettingsStorage> )( pair => pair.Save() ) ).ToArray<SettingsStorage>() );
        }
    }
}
