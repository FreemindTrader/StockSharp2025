//using Ecng.Collections;
//using Ecng.Common;
//using Ecng.Data;
//using Ecng.Data.Providers;
//using Ecng.Serialization;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace fx.Database
//{
//    public class DatabaseConnectionCache : IPersistable
//    {
//        private readonly CachedSynchronizedSet<DatabaseConnectionPair> _connections = new CachedSynchronizedSet<DatabaseConnectionPair>();
//        private static readonly Lazy< DatabaseConnectionCache > _instance = new Lazy< DatabaseConnectionCache >( ( ) => new DatabaseConnectionCache( ) );


//        public IEnumerable<DatabaseConnectionPair> Connections => ( IEnumerable<DatabaseConnectionPair> ) this._connections.Cache;

//        private DatabaseConnectionCache( )
//        {
//        }

//        /// <summary>
//        /// Кэш.
//        /// </summary>
//        public static DatabaseConnectionCache Instance
//        {
//            get
//            {
//                return DatabaseConnectionCache._instance.Value;
//            }
//        }

//        /// <summary>
//        /// Список всех подключений.
//        /// </summary>
//        public IEnumerable<DatabaseConnectionPair> AllConnections
//        {
//            get
//            {
//                return _connections.Cache;
//            }
//        }

//        /// <summary>
//        /// Событие создания нового подключения.
//        /// </summary>
//        public event Action< DatabaseConnectionPair > NewConnectionCreated;

//        /// <summary>
//        /// Событие удаления подключения.
//        /// </summary>
//        public event Action< DatabaseConnectionPair > ConnectionDeleted;

//        /// <summary>
//        /// Получить подключение к базе данных.
//        /// </summary>
//        /// <param name="provider">Провайдер баз данных.</param>
//        /// <param name="connectionString">Строка подключения.</param>
//        /// <returns>Подключение к базе данных.</returns>
//        public DatabaseConnectionPair GetConnection( DatabaseProvider provider, string connectionString )
//        {
//            DatabaseConnectionPair connection = AllConnections.FirstOrDefault( p =>
//            {
//                if( p.Provider == provider )
//                {
//                    return p.ConnectionString.CompareIgnoreCase( connectionString );
//                }
//                return false;
//            } );
//            if ( connection == null )
//            {
//                connection = new DatabaseConnectionPair( )
//                {
//                    Provider = provider,
//                    ConnectionString = connectionString
//                };
//                AddConnection( connection );
//            }
//            return connection;
//        }

//        /// <summary>
//        /// Добавить новое подключение к базе данных.
//        /// </summary>
//        /// <param name="connection">Новое подключение.</param>
//        private void AddConnection( DatabaseConnectionPair connection )
//        {
//            if ( connection == null )
//            {
//                throw new ArgumentNullException( nameof( connection ) );
//            }
//            _connections.Add( connection );
//            // ISSUE: reference to a compiler-generated field
//            Action< DatabaseConnectionPair > connectionCreated = NewConnectionCreated;
//            if ( connectionCreated == null )
//            {
//                return;
//            }
//            connectionCreated( connection );
//        }

//        /// <summary>
//        /// Удалить подключение к базе данных.
//        /// </summary>
//        /// <param name="connection">Подключение.</param>
//        public void DeleteDrive( DatabaseConnectionPair connection )
//        {
//            if ( connection == null )
//            {
//                throw new ArgumentNullException( nameof( connection ) );
//            }
//            if ( !_connections.Remove( connection ) )
//            {
//                return;
//            }            // ISSUE: reference to a compiler-generated field
//            Action< DatabaseConnectionPair > connectionDeleted = ConnectionDeleted;
//            if ( connectionDeleted == null )
//            {
//                return;
//            }
//            connectionDeleted( connection );
//        }

//        /// <summary>
//        /// Загрузить настройки.
//        /// </summary>
//        /// <param name="storage">Хранилище настроек.</param>
//        public void Load( SettingsStorage storage )
//        {
//            DatabaseConnectionPair[ ] array = storage.GetValue< IEnumerable< SettingsStorage > >( "Connections", null ).Select( s =>
//            {
//                string providerName = s.GetValue< string >( "Provider", null );
//                DatabaseProvider databaseProvider = DatabaseProviderRegistry.Providers.FirstOrDefault( p => p.Name.CompareIgnoreCase( providerName ) );

//                if( databaseProvider == null )
//                {
//                    return null;
//                }
//                
//                return new DatabaseConnectionPair( )
//                {
//                    Provider = databaseProvider,
//                    ConnectionString = s.GetValue< string >( "ConnectionString", null )
//                };

//            } ).Where( p => p != null ).ToArray( );

//            lock ( _connections.SyncRoot )
//            {
//                _connections.AddRange( array );
//            }
//        }

//        /// <summary>
//        /// Сохранить настройки.
//        /// </summary>
//        /// <param name="storage">Хранилище настроек.</param>
//        public void Save( SettingsStorage storage )
//        {
//            storage.SetValue( "Connections", AllConnections.Select( pair =>
//            {
//                return new SettingsStorage( )
//                {
//                    [ "Provider" ] = pair.Provider.Name,
//                    [ "ConnectionString" ] = pair.ConnectionString
//                };
//            } ).ToArray( ) );
//        }
//    }
//}
