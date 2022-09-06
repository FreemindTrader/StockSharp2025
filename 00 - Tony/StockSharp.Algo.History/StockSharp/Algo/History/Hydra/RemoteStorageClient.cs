//using Ecng.Collections;
//using Ecng.Common;
//using Ecng.ComponentModel;
//using MoreLinq;
//using StockSharp.Algo.Storages;
//using StockSharp.BusinessEntities;
//using StockSharp.Community;
//using StockSharp.Messages;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Security;
//using System.ServiceModel;
//using System.ServiceModel.Channels;
//using System.ServiceModel.Description;

//#pragma warning disable 649

//namespace StockSharp.Algo.History.Hydra
//{
//    public class RemoteStorageClient : BaseCommunityClient< IRemoteStorage >, ISecurityDownloader
//    {
//        private readonly SynchronizedDictionary< Tuple< SecurityId, System.Type, object, StorageFormats >, RemoteStorageClient.Class92 > synchronizedDictionary_0 = new SynchronizedDictionary< Tuple< SecurityId, System.Type, object, StorageFormats >, RemoteStorageClient.Class92 >( );
//        private readonly SynchronizedDictionary< string, RemoteStorageClient.Class85 > synchronizedDictionary_1 = new SynchronizedDictionary< string, RemoteStorageClient.Class85 >( ( IEqualityComparer< string > )StringComparer.InvariantCultureIgnoreCase );
//        private readonly bool bool_0;
//        private RemoteMarketDataDrive remoteMarketDataDrive_0;
//        private readonly IExchangeInfoProvider iexchangeInfoProvider_0;
//        private readonly ServerCredentials serverCredentials_0;
//        private Guid guid_0;
//        private int int_0;

//        public RemoteStorageClient( IExchangeInfoProvider exchangeInfoProvider ) : this( exchangeInfoProvider, "net.tcp://localhost:8000".To< Uri >( ), true )
//        {
//        }

//        public RemoteStorageClient( IExchangeInfoProvider exchangeInfoProvider, Uri address, bool streaming = true ) : base( address, "hydra", false )
//        {
//            if( address == ( Uri )null )
//            {
//                throw new ArgumentNullException( nameof( address ) );
//            }

//            IExchangeInfoProvider exchangeInfoProvider1 = exchangeInfoProvider;
//            if( exchangeInfoProvider1 == null )
//            {
//                throw new ArgumentNullException( nameof( exchangeInfoProvider ) );
//            }

//            this.iexchangeInfoProvider_0 = exchangeInfoProvider1;
//            this.bool_0 = streaming;
//            this.serverCredentials_0 = new ServerCredentials( );
//            this.SecurityBatchSize = 1000;
//        }

//        internal RemoteMarketDataDrive method_0( )
//        {
//            return this.remoteMarketDataDrive_0;
//        }

//        internal void method_1( RemoteMarketDataDrive remoteMarketDataDrive_1 )
//        {
//            this.remoteMarketDataDrive_0 = remoteMarketDataDrive_1;
//        }

//        public IExchangeInfoProvider ExchangeInfoProvider
//        {
//            get
//            {
//                return this.iexchangeInfoProvider_0;
//            }
//        }

//        public ServerCredentials Credentials
//        {
//            get
//            {
//                return this.serverCredentials_0;
//            }
//        }

//        protected override Guid SessionId
//        {
//            get
//            {
//                return this.guid_0;
//            }
//        }

//        public int SecurityBatchSize
//        {
//            get
//            {
//                return this.int_0;
//            }
//            set
//            {
//                if( value <= 0 )
//                {
//                    throw new ArgumentOutOfRangeException( );
//                }

//                this.int_0 = value;
//            }
//        }

//        public IEnumerable< SecurityId > AvailableSecurities
//        {
//            get
//            {
//                return ( IEnumerable< SecurityId > )( ( IEnumerable< string > )this.Invoke< string[ ] >( new Func< IRemoteStorage, string[ ] >( this.method_2 ) ) ).Select< string, SecurityId >( RemoteStorageClient.Class99.func_0 ?? ( RemoteStorageClient.Class99.func_0 = new Func< string, SecurityId >( RemoteStorageClient.Class99.class99_0.method_0 ) ) ).ToArray< SecurityId >( );
//            }
//        }

//        protected override ChannelFactory< IRemoteStorage > CreateChannel( )
//        {
//            NetTcpBinding netTcpBinding = new NetTcpBinding( SecurityMode.None );
//            netTcpBinding.TransferMode = this.bool_0 ? TransferMode.StreamedResponse : TransferMode.Buffered;
//            netTcpBinding.OpenTimeout = TimeSpan.FromMinutes( 5.0 );
//            netTcpBinding.SendTimeout = TimeSpan.FromMinutes( 40.0 );
//            netTcpBinding.ReceiveTimeout = TimeSpan.FromMinutes( 40.0 );
//            netTcpBinding.MaxReceivedMessageSize = ( long )int.MaxValue;
//            netTcpBinding.ReaderQuotas.MaxArrayLength = int.MaxValue;
//            netTcpBinding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
//            netTcpBinding.MaxBufferSize = int.MaxValue;
//            netTcpBinding.MaxBufferPoolSize = ( long )int.MaxValue;
//            ChannelFactory< IRemoteStorage > channelFactory = new ChannelFactory< IRemoteStorage >( ( Binding )netTcpBinding, new EndpointAddress( this.Address, new AddressHeader[ 0 ] ) );
//            foreach( OperationDescription operation in ( Collection< OperationDescription > )channelFactory.Endpoint.Contract.Operations )
//            {
//                if( operation.Behaviors[ typeof( DataContractSerializerOperationBehavior ) ] is DataContractSerializerOperationBehavior behavior )
//                {
//                    behavior.MaxItemsInObjectGraph = int.MaxValue;
//                }
//            }
//            return channelFactory;
//        }

//        public void Refresh(
//                                ISecurityStorage storage,
//                                StockSharp.BusinessEntities.Security security,
//                                Action< StockSharp.BusinessEntities.Security > securityHandler,
//                                Func< bool > isCancelled )
//        {
//            if( storage == null )
//            {
//                throw new ArgumentNullException( nameof( storage ) );
//            }

//            if( security == null )
//            {
//                throw new ArgumentNullException( nameof( security ) );
//            }

//            if( securityHandler == null )
//            {
//                throw new ArgumentNullException( nameof( securityHandler ) );
//            }

//            if( isCancelled == null )
//            {
//                throw new ArgumentNullException( nameof( isCancelled ) );
//            }

//            RemoteStorageClient.Class82 class82 = new RemoteStorageClient.Class82( );
//            class82._this = this;
//            class82._Security = security;
//            string[ ] strArray = this.Invoke< string[ ] >( s => s.LookupSecurityIds( this.SessionId, security ) );

//            var tony = storage.LookupAll( ).Select( s => s.Id );

//            class82.hashSet_0 = MoreEnumerable.ToHashSet< string >( tony, ( IEqualityComparer< string > )StringComparer.InvariantCultureIgnoreCase );
//            Func< string, bool > predicate = new Func< string, bool >( class82.method_1 );
//            foreach( IEnumerable< string > source in ( ( IEnumerable< string > )( ( IEnumerable< string > )strArray ).Where< string >( predicate ).ToArray< string >( ) ).Batch< string >( 200 ) )
//            {
//                RemoteStorageClient.Class102 class102 = new RemoteStorageClient.Class102( );
//                class102.class82_0 = class82;
//                if( isCancelled( ) )
//                {
//                    break;
//                }

//                class102.string_0 = source.ToArray< string >( );

//                foreach( StockSharp.BusinessEntities.Security sec in this.Invoke< StockSharp.BusinessEntities.Security[ ] >( class102.func_0 ?? ( class102.func_0 = new Func< IRemoteStorage, StockSharp.BusinessEntities.Security[ ] >( class102.method_0 ) ) ) )
//                {
//                    if( ( Equatable< ExchangeBoard > )sec.Board == ( ExchangeBoard )null )
//                    {
//                        SecurityId securityId = sec.Id.ToSecurityId( ( SecurityIdGenerator )null );
//                        sec.Board = this.ExchangeInfoProvider.GetOrCreateBoard( securityId.BoardCode, ( Func< string, ExchangeBoard > )null );
//                    }
//                    else
//                    {
//                        ExchangeBoard exchangeBoard = this.ExchangeInfoProvider.GetExchangeBoard( sec.Board.Code );
//                        if( ( Equatable< ExchangeBoard > )exchangeBoard != ( ExchangeBoard )null )
//                        {
//                            sec.Board = exchangeBoard;
//                        }
//                        else
//                        {
//                            ExchangeBoard board = sec.Board;
//                            if( ( Equatable< Exchange > )board.Exchange == ( Exchange )null )
//                            {
//                                board.Exchange = new Exchange( )
//                                {
//                                    Name = board.Code
//                                };
//                                this.ExchangeInfoProvider.Save( board.Exchange );
//                            }
//                            this.ExchangeInfoProvider.Save( board );
//                        }
//                        if( sec.Board.TimeZone == null )
//                        {
//                            sec.Board.TimeZone = TimeZoneInfo.Utc;
//                            this.ExchangeInfoProvider.Save( sec.Board );
//                        }
//                    }
//                    storage.Save( sec, false );
//                    securityHandler( sec );
//                }
//            }
//        }

//        public Exchange[ ] LoadExchanges( Exchange criteria )
//        {
//            RemoteStorageClient.Class97 class97 = new RemoteStorageClient.Class97( );
//            class97.remoteStorageClient_0 = this;
//            class97.exchange_0 = criteria;
//            if( ( Equatable< Exchange > )class97.exchange_0 == ( Exchange )null )
//            {
//                throw new ArgumentNullException( nameof( criteria ) );
//            }

//            class97.string_0 = this.Invoke< string[ ] >( new Func< IRemoteStorage, string[ ] >( class97.method_0 ) );
//            return this.Invoke< Exchange[ ] >( new Func< IRemoteStorage, Exchange[ ] >( class97.method_1 ) );
//        }

//        public ExchangeBoard[ ] LoadExchangeBoards( ExchangeBoard criteria )
//        {
//            RemoteStorageClient.Class98 class98 = new RemoteStorageClient.Class98( );
//            class98.remoteStorageClient_0 = this;
//            class98.exchangeBoard_0 = criteria;
//            if( ( Equatable< ExchangeBoard > )class98.exchangeBoard_0 == ( ExchangeBoard )null )
//            {
//                throw new ArgumentNullException( nameof( criteria ) );
//            }

//            class98.string_0 = this.Invoke< string[ ] >( new Func< IRemoteStorage, string[ ] >( class98.method_0 ) );
//            return this.Invoke< ExchangeBoard[ ] >( new Func< IRemoteStorage, ExchangeBoard[ ] >( class98.method_1 ) );
//        }

//        public void SaveSecurities( StockSharp.BusinessEntities.Security[ ] securities )
//        {
//            RemoteStorageClient.Class77 class77 = new RemoteStorageClient.Class77( );
//            class77.remoteStorageClient_0 = this;
//            class77.security_0 = securities;
//            if( class77.security_0 == null )
//            {
//                throw new ArgumentNullException( nameof( securities ) );
//            }

//            if( class77.security_0.IsEmpty< StockSharp.BusinessEntities.Security >( ) )
//            {
//                throw new ArgumentOutOfRangeException( nameof( securities ) );
//            }

//            this.Invoke( new Action< IRemoteStorage >( class77.method_0 ) );
//        }

//        public void DeleteSecurities( StockSharp.BusinessEntities.Security[ ] securities )
//        {
//            RemoteStorageClient.Class79 class79 = new RemoteStorageClient.Class79( );
//            class79.remoteStorageClient_0 = this;
//            class79.security_0 = securities;
//            if( class79.security_0 == null )
//            {
//                throw new ArgumentNullException( nameof( securities ) );
//            }

//            if( class79.security_0.IsEmpty< StockSharp.BusinessEntities.Security >( ) )
//            {
//                throw new ArgumentOutOfRangeException( nameof( securities ) );
//            }

//            this.Invoke( new Action< IRemoteStorage >( class79.method_0 ) );
//        }

//        public void SaveExchanges( Exchange[ ] exchanges )
//        {
//            RemoteStorageClient.Class103 class103 = new RemoteStorageClient.Class103( );
//            class103.remoteStorageClient_0 = this;
//            class103.exchange_0 = exchanges;
//            if( class103.exchange_0 == null )
//            {
//                throw new ArgumentNullException( nameof( exchanges ) );
//            }

//            if( class103.exchange_0.IsEmpty< Exchange >( ) )
//            {
//                throw new ArgumentOutOfRangeException( nameof( exchanges ) );
//            }

//            this.Invoke( new Action< IRemoteStorage >( class103.method_0 ) );
//        }

//        public void DeleteExchanges( Exchange[ ] exchanges )
//        {
//            RemoteStorageClient.Class78 class78 = new RemoteStorageClient.Class78( );
//            class78.remoteStorageClient_0 = this;
//            class78.exchange_0 = exchanges;
//            if( class78.exchange_0 == null )
//            {
//                throw new ArgumentNullException( nameof( exchanges ) );
//            }

//            if( class78.exchange_0.IsEmpty< Exchange >( ) )
//            {
//                throw new ArgumentOutOfRangeException( nameof( exchanges ) );
//            }

//            this.Invoke( new Action< IRemoteStorage >( class78.method_0 ) );
//        }

//        public void SaveExchangeBoards( ExchangeBoard[ ] boards )
//        {
//            RemoteStorageClient.Class100 class100 = new RemoteStorageClient.Class100( );
//            class100.remoteStorageClient_0 = this;
//            class100.exchangeBoard_0 = boards;
//            if( class100.exchangeBoard_0 == null )
//            {
//                throw new ArgumentNullException( nameof( boards ) );
//            }

//            if( class100.exchangeBoard_0.IsEmpty< ExchangeBoard >( ) )
//            {
//                throw new ArgumentOutOfRangeException( nameof( boards ) );
//            }

//            this.Invoke( new Action< IRemoteStorage >( class100.method_0 ) );
//        }

//        public void DeleteExchangeBoards( ExchangeBoard[ ] boards )
//        {
//            RemoteStorageClient.Class80 class80 = new RemoteStorageClient.Class80( );
//            class80.remoteStorageClient_0 = this;
//            class80.exchangeBoard_0 = boards;
//            if( class80.exchangeBoard_0 == null )
//            {
//                throw new ArgumentNullException( nameof( boards ) );
//            }

//            if( class80.exchangeBoard_0.IsEmpty< ExchangeBoard >( ) )
//            {
//                throw new ArgumentOutOfRangeException( nameof( boards ) );
//            }

//            this.Invoke( new Action< IRemoteStorage >( class80.method_0 ) );
//        }

//        public string[ ] GetSecurityExtendedStorages( )
//        {
//            return this.Invoke< string[ ] >( new Func< IRemoteStorage, string[ ] >( this.method_3 ) );
//        }

//        public IRemoteExtendedStorage GetExtendedStorage( string storageName )
//        {
//            RemoteStorageClient.Class84 class84 = new RemoteStorageClient.Class84( );
//            class84.remoteStorageClient_0 = this;
//            class84.string_0 = storageName;
//            return ( IRemoteExtendedStorage )this.synchronizedDictionary_1.SafeAdd< string, RemoteStorageClient.Class85 >( class84.string_0, new Func< string, RemoteStorageClient.Class85 >( class84.method_0 ) );
//        }

//        public Tuple< string, IPAddress[ ], UserPermissions >[ ] GetUsers( )
//        {
//            return ( ( IEnumerable< Tuple< string, string[ ], UserPermissions > > )this.Invoke< Tuple< string, string[ ], UserPermissions >[ ] >( new Func< IRemoteStorage, Tuple< string, string[ ], UserPermissions >[ ] >( this.method_4 ) ) ).Select< Tuple< string, string[ ], UserPermissions >, Tuple< string, IPAddress[ ], UserPermissions > >( RemoteStorageClient.Class99.func_6 ?? ( RemoteStorageClient.Class99.func_6 = new Func< Tuple< string, string[ ], UserPermissions >, Tuple< string, IPAddress[ ], UserPermissions > >( RemoteStorageClient.Class99.class99_0.method_5 ) ) ).ToArray< Tuple< string, IPAddress[ ], UserPermissions > >( );
//        }

//        public void SaveUser(
//          string login,
//          SecureString password,
//          IPAddress[ ] ipAddresses,
//          UserPermissions permissions )
//        {
//            this.Invoke( new Action< IRemoteStorage >( new RemoteStorageClient.Class104( )
//            {
//                remoteStorageClient_0 = this,
//                string_0 = login,
//                secureString_0 = password,
//                ipaddress_0 = ipAddresses,
//                userPermissions_0 = permissions
//            }.method_0 ) );
//        }

//        public void DeleteUser( string login )
//        {
//            this.Invoke( new Action< IRemoteStorage >( new RemoteStorageClient.Class81( )
//            {
//                remoteStorageClient_0 = this,
//                string_0 = login
//            }.method_0 ) );
//        }

//        public void Restart( )
//        {
//            this.Invoke( new Action< IRemoteStorage >( this.method_5 ) );
//        }

//        public bool StartDownloading( )
//        {
//            return this.Invoke< bool >( new Func< IRemoteStorage, bool >( this.method_6 ) );
//        }

//        public void StopDownloading( )
//        {
//            this.Invoke( new Action< IRemoteStorage >( this.method_7 ) );
//        }

//        public IMarketDataStorageDrive GetRemoteStorage(
//          SecurityId securityId,
//          System.Type dataType,
//          object arg,
//          StorageFormats format )
//        {
//            RemoteStorageClient.Class101 class101 = new RemoteStorageClient.Class101( );
//            class101.remoteStorageClient_0 = this;
//            class101.securityId_0 = securityId;
//            class101.type_0 = dataType;
//            class101.object_0 = arg;
//            class101.storageFormats_0 = format;
//            if( class101.securityId_0.IsDefault< SecurityId >( ) )
//            {
//                throw new ArgumentNullException( nameof( securityId ) );
//            }

//            if( class101.type_0 == ( System.Type )null )
//            {
//                throw new ArgumentNullException( nameof( dataType ) );
//            }

//            return ( IMarketDataStorageDrive )this.synchronizedDictionary_0.SafeAdd< Tuple< SecurityId, System.Type, object, StorageFormats >, RemoteStorageClient.Class92 >( Tuple.Create< SecurityId, System.Type, object, StorageFormats >( class101.securityId_0, class101.type_0, class101.object_0, class101.storageFormats_0 ), new Func< Tuple< SecurityId, System.Type, object, StorageFormats >, RemoteStorageClient.Class92 >( class101.method_0 ) );
//        }

//        public IEnumerable< DataType > GetAvailableDataTypes(
//          SecurityId securityId,
//          StorageFormats format )
//        {
//            RemoteStorageClient.Class83 class83 = new RemoteStorageClient.Class83( );
//            class83.remoteStorageClient_0 = this;
//            class83.securityId_0 = securityId;
//            class83.storageFormats_0 = format;
//            if( class83.securityId_0.IsDefault< SecurityId >( ) )
//            {
//                throw new ArgumentNullException( nameof( securityId ) );
//            }

//            return ( IEnumerable< DataType > )( ( IEnumerable< Tuple< string, object > > )this.Invoke< Tuple< string, object >[ ] >( new Func< IRemoteStorage, Tuple< string, object >[ ] >( class83.method_0 ) ) ).Select< Tuple< string, object >, DataType >( RemoteStorageClient.Class99.func_8 ?? ( RemoteStorageClient.Class99.func_8 = new Func< Tuple< string, object >, DataType >( RemoteStorageClient.Class99.class99_0.method_8 ) ) ).ToArray< DataType >( );
//        }

//        public void Login( )
//        {
//            this.guid_0 = base.Invoke< Guid >( new Func< IRemoteStorage, Guid >( this.method_8 ) );
//        }

//        protected override TResult Invoke< TResult >( Func< IRemoteStorage, TResult > handler )
//        {
//            if( this.guid_0 == new Guid( ) )
//            {
//                this.Login( );
//            }

//            try
//            {
//                return base.Invoke< TResult >( handler );
//            }
//            catch( FaultException< ExceptionDetail > ex )
//            {
//                if( ex.Detail.Type != typeof( UnauthorizedAccessException ).FullName )
//                {
//                    throw;
//                }
//                else
//                {
//                    this.Login( );
//                    return base.Invoke< TResult >( handler );
//                }
//            }
//        }

//        protected override void DisposeManaged( )
//        {
//            if( this.guid_0 != new Guid( ) )
//            {
//                this.Invoke( new Action< IRemoteStorage >( this.method_9 ) );
//                this.guid_0 = new Guid( );
//            }
//            base.DisposeManaged( );
//        }

//        private string[ ] method_2( IRemoteStorage iremoteStorage_0 )
//        {
//            return iremoteStorage_0.GetAvailableSecurities( this.SessionId );
//        }

//        private string[ ] method_3( IRemoteStorage iremoteStorage_0 )
//        {
//            return iremoteStorage_0.GetSecurityExtendedStorages( this.SessionId );
//        }

//        private Tuple< string, string[ ], UserPermissions >[ ] method_4(
//          IRemoteStorage iremoteStorage_0 )
//        {
//            return iremoteStorage_0.GetUsers( this.SessionId );
//        }

//        private void method_5( IRemoteStorage iremoteStorage_0 )
//        {
//            iremoteStorage_0.Restart( this.SessionId );
//        }

//        private bool method_6( IRemoteStorage iremoteStorage_0 )
//        {
//            return iremoteStorage_0.StartDownloading( this.SessionId );
//        }

//        private void method_7( IRemoteStorage iremoteStorage_0 )
//        {
//            iremoteStorage_0.StopDownloading( this.SessionId );
//        }

//        private Guid method_8( IRemoteStorage iremoteStorage_0 )
//        {
//            return iremoteStorage_0.Login( this.Credentials.Email, this.Credentials.Password.To< string >( ) );
//        }

//        private void method_9( IRemoteStorage iremoteStorage_0 )
//        {
//            iremoteStorage_0.Logout( this.guid_0 );
//        }

//        private sealed class Class77
//        {
//            public RemoteStorageClient remoteStorageClient_0;
//            public StockSharp.BusinessEntities.Security[ ] security_0;

//            internal void method_0( IRemoteStorage iremoteStorage_0 )
//            {
//                iremoteStorage_0.SaveSecurities( this.remoteStorageClient_0.SessionId, this.security_0 );
//            }
//        }

//        private sealed class Class78
//        {
//            public RemoteStorageClient remoteStorageClient_0;
//            public Exchange[ ] exchange_0;

//            internal void method_0( IRemoteStorage iremoteStorage_0 )
//            {
//                iremoteStorage_0.DeleteExchanges( this.remoteStorageClient_0.SessionId, ( ( IEnumerable< Exchange > )this.exchange_0 ).Select< Exchange, string >( RemoteStorageClient.Class99.func_3 ?? ( RemoteStorageClient.Class99.func_3 = new Func< Exchange, string >( RemoteStorageClient.Class99.class99_0.method_3 ) ) ).ToArray< string >( ) );
//            }
//        }

//        private sealed class Class79
//        {
//            public RemoteStorageClient remoteStorageClient_0;
//            public StockSharp.BusinessEntities.Security[ ] security_0;

//            internal void method_0( IRemoteStorage iremoteStorage_0 )
//            {
//                iremoteStorage_0.DeleteSecurities( this.remoteStorageClient_0.SessionId, ( ( IEnumerable< StockSharp.BusinessEntities.Security > )this.security_0 ).Select< StockSharp.BusinessEntities.Security, string >( RemoteStorageClient.Class99.func_2 ?? ( RemoteStorageClient.Class99.func_2 = new Func< StockSharp.BusinessEntities.Security, string >( RemoteStorageClient.Class99.class99_0.method_2 ) ) ).ToArray< string >( ) );
//            }
//        }

//        private sealed class Class80
//        {
//            public RemoteStorageClient remoteStorageClient_0;
//            public ExchangeBoard[ ] exchangeBoard_0;

//            internal void method_0( IRemoteStorage iremoteStorage_0 )
//            {
//                iremoteStorage_0.DeleteExchangeBoards( this.remoteStorageClient_0.SessionId, ( ( IEnumerable< ExchangeBoard > )this.exchangeBoard_0 ).Select< ExchangeBoard, string >( RemoteStorageClient.Class99.func_4 ?? ( RemoteStorageClient.Class99.func_4 = new Func< ExchangeBoard, string >( RemoteStorageClient.Class99.class99_0.method_4 ) ) ).ToArray< string >( ) );
//            }
//        }

//        private sealed class Class81
//        {
//            public RemoteStorageClient remoteStorageClient_0;
//            public string string_0;

//            internal void method_0( IRemoteStorage iremoteStorage_0 )
//            {
//                iremoteStorage_0.DeleteUser( this.remoteStorageClient_0.SessionId, this.string_0 );
//            }
//        }

//        private sealed class Class82
//        {
//            public RemoteStorageClient _this;
//            public StockSharp.BusinessEntities.Security _Security;
//            public HashSet< string > hashSet_0;

//            internal string[ ] method_0( IRemoteStorage s )
//            {
//                return s.LookupSecurityIds( this._this.SessionId, this._Security );
//            }

//            internal bool method_1( string string_0 )
//            {
//                return !this.hashSet_0.Contains( string_0 );
//            }
//        }

//        private sealed class Class83
//        {
//            public RemoteStorageClient remoteStorageClient_0;
//            public SecurityId securityId_0;
//            public StorageFormats storageFormats_0;

//            internal Tuple< string, object >[ ] method_0( IRemoteStorage iremoteStorage_0 )
//            {
//                return iremoteStorage_0.GetAvailableDataTypes( this.remoteStorageClient_0.SessionId, this.securityId_0.ToStringId( ( SecurityIdGenerator )null ), this.storageFormats_0 );
//            }
//        }

//        private sealed class Class84
//        {
//            public RemoteStorageClient remoteStorageClient_0;
//            public string string_0;

//            internal RemoteStorageClient.Class85 method_0( string string_1 )
//            {
//                return new RemoteStorageClient.Class85( this.remoteStorageClient_0, this.string_0 );
//            }
//        }

//        private sealed class Class85 : IRemoteExtendedStorage
//        {
//            private readonly SynchronizedDictionary< SecurityId, ISecurityRemoteExtendedStorage > synchronizedDictionary_0 = new SynchronizedDictionary< SecurityId, ISecurityRemoteExtendedStorage >( );
//            private readonly RemoteStorageClient remoteStorageClient_0;
//            private readonly string string_0;
//            private Tuple< string, System.Type >[ ] tuple_0;

//            public Class85( RemoteStorageClient remoteStorageClient_1, string string_1 )
//            {
//                if( string_1.IsEmpty( ) )
//                {
//                    throw new ArgumentNullException( "storageName" );
//                }

//                RemoteStorageClient remoteStorageClient = remoteStorageClient_1;
//                if( remoteStorageClient == null )
//                {
//                    throw new ArgumentNullException( "client" );
//                }

//                this.remoteStorageClient_0 = remoteStorageClient;
//                this.string_0 = string_1;
//            }

//            public Tuple< string, System.Type >[ ] Fields
//            {
//                get
//                {
//                    return this.tuple_0 ?? ( this.tuple_0 = ( ( IEnumerable< Tuple< string, string > > )this.remoteStorageClient_0.Invoke< Tuple< string, string >[ ] >( new Func< IRemoteStorage, Tuple< string, string >[ ] >( this.method_0 ) ) ).Select< Tuple< string, string >, Tuple< string, System.Type > >( RemoteStorageClient.Class85.Class90.func_0 ?? ( RemoteStorageClient.Class85.Class90.func_0 = new Func< Tuple< string, string >, Tuple< string, System.Type > >( RemoteStorageClient.Class85.Class90.class90_0.method_0 ) ) ).ToArray< Tuple< string, System.Type > >( ) );
//                }
//            }

//            void IRemoteExtendedStorage.CreateSecurityExtendedFields(
//              Tuple< string, System.Type >[ ] fields )
//            {
//                RemoteStorageClient.Class85.Class91 class91 = new RemoteStorageClient.Class85.Class91( );
//                class91.class85_0 = this;
//                class91.tuple_0 = fields;
//                if( class91.tuple_0 == null )
//                {
//                    throw new ArgumentNullException( nameof( fields ) );
//                }

//                this.remoteStorageClient_0.Invoke( new Action< IRemoteStorage >( class91.method_0 ) );
//            }

//            string IRemoteExtendedStorage.StorageName
//            {
//                get
//                {
//                    return this.string_0;
//                }
//            }

//            //string IRemoteExtendedStorage.get_StorageName()
//            //{
//            //  return this.string_0;
//            //}

//            IEnumerable< SecurityId > IRemoteExtendedStorage.Securities
//            {
//                get
//                {
//                    return ( ( IEnumerable< string > )this.remoteStorageClient_0.Invoke< string[ ] >( new Func< IRemoteStorage, string[ ] >( this.method_1 ) ) ).Select< string, SecurityId >( RemoteStorageClient.Class85.Class90.func_2 ?? ( RemoteStorageClient.Class85.Class90.func_2 = new Func< string, SecurityId >( RemoteStorageClient.Class85.Class90.class90_0.method_2 ) ) );
//                }
//            }

//            //IEnumerable<SecurityId> IRemoteExtendedStorage.get_Securities()
//            //{
//            //  return ((IEnumerable<string>) this.remoteStorageClient_0.Invoke<string[]>(new Func<IRemoteStorage, string[]>(this.method_1))).Select<string, SecurityId>(RemoteStorageClient.Class85.Class90.func_2 ?? (RemoteStorageClient.Class85.Class90.func_2 = new Func<string, SecurityId>(RemoteStorageClient.Class85.Class90.class90_0.method_2)));
//            //}

//            ISecurityRemoteExtendedStorage IRemoteExtendedStorage.GetSecurityStorage(
//              SecurityId securityId )
//            {
//                if( securityId.IsDefault< SecurityId >( ) )
//                {
//                    throw new ArgumentNullException( nameof( securityId ) );
//                }

//                return this.synchronizedDictionary_0.SafeAdd< SecurityId, ISecurityRemoteExtendedStorage >( securityId, new Func< SecurityId, ISecurityRemoteExtendedStorage >( this.method_2 ) );
//            }

//            Tuple< SecurityId, object[ ] >[ ] IRemoteExtendedStorage.GetAllExtendedInfo( )
//            {
//                RemoteStorageClient.Class85.Class89 class89 = new RemoteStorageClient.Class85.Class89( );
//                class89.class85_0 = this;
//                class89.tuple_0 = this.Fields;
//                if( class89.tuple_0 == null )
//                {
//                    return ( Tuple< SecurityId, object[ ] >[ ] )null;
//                }

//                return ( ( IEnumerable< Tuple< string, string[ ] > > )this.remoteStorageClient_0.Invoke< Tuple< string, string[ ] >[ ] >( new Func< IRemoteStorage, Tuple< string, string[ ] >[ ] >( class89.method_0 ) ) ).Select< Tuple< string, string[ ] >, Tuple< SecurityId, object[ ] > >( new Func< Tuple< string, string[ ] >, Tuple< SecurityId, object[ ] > >( class89.method_1 ) ).ToArray< Tuple< SecurityId, object[ ] > >( );
//            }

//            private Tuple< string, string >[ ] method_0( IRemoteStorage iremoteStorage_0 )
//            {
//                return iremoteStorage_0.GetSecurityExtendedFields( this.remoteStorageClient_0.SessionId, this.string_0 );
//            }

//            private string[ ] method_1( IRemoteStorage iremoteStorage_0 )
//            {
//                return iremoteStorage_0.GetExtendedInfoSecurities( this.remoteStorageClient_0.SessionId, this.string_0 );
//            }

//            private ISecurityRemoteExtendedStorage method_2(
//              SecurityId securityId_0 )
//            {
//                return ( ISecurityRemoteExtendedStorage )new RemoteStorageClient.Class85.Class86( this, securityId_0 );
//            }

//            private sealed class Class86 : ISecurityRemoteExtendedStorage
//            {
//                private readonly RemoteStorageClient.Class85 class85_0;
//                private readonly SecurityId securityId_0;
//                private readonly string string_0;

//                public Class86( RemoteStorageClient.Class85 class85_1, SecurityId securityId_1 )
//                {
//                    RemoteStorageClient.Class85 class85 = class85_1;
//                    if( class85 == null )
//                    {
//                        throw new ArgumentNullException( "parent" );
//                    }

//                    this.class85_0 = class85;
//                    this.securityId_0 = securityId_1;
//                    this.string_0 = securityId_1.ToStringId( ( SecurityIdGenerator )null );
//                }

//                void ISecurityRemoteExtendedStorage.AddSecurityExtendedInfo(
//                  object[ ] fieldValues )
//                {
//                    this.class85_0.remoteStorageClient_0.Invoke( new Action< IRemoteStorage >( new RemoteStorageClient.Class85.Class86.Class88( )
//                    {
//                        class86_0 = this,
//                        object_0 = fieldValues
//                    }.method_0 ) );
//                }

//                void ISecurityRemoteExtendedStorage.DeleteSecurityExtendedInfo( )
//                {
//                    this.class85_0.remoteStorageClient_0.Invoke( new Action< IRemoteStorage >( this.method_0 ) );
//                }

//                SecurityId ISecurityRemoteExtendedStorage.SecurityId
//                {
//                    get
//                    {
//                        return this.securityId_0;
//                    }
//                }

//                //SecurityId ISecurityRemoteExtendedStorage.get_SecurityId()
//                //{
//                //  return this.securityId_0;
//                //}

//                private void method_0( IRemoteStorage iremoteStorage_0 )
//                {
//                    iremoteStorage_0.DeleteSecurityExtendedInfo( this.class85_0.remoteStorageClient_0.SessionId, this.class85_0.string_0, this.string_0 );
//                }

//                [Serializable]
//                private sealed class Class87
//                {
//                    public static readonly RemoteStorageClient.Class85.Class86.Class87 class87_0 = new RemoteStorageClient.Class85.Class86.Class87( );
//                    public static Func< object, string > func_0;

//                    internal string method_0( object object_0 )
//                    {
//                        return object_0.To< string >( );
//                    }
//                }

//                private sealed class Class88
//                {
//                    public RemoteStorageClient.Class85.Class86 class86_0;
//                    public object[ ] object_0;

//                    internal void method_0( IRemoteStorage iremoteStorage_0 )
//                    {
//                        iremoteStorage_0.AddSecurityExtendedInfo( this.class86_0.class85_0.remoteStorageClient_0.SessionId, this.class86_0.class85_0.string_0, this.class86_0.string_0, ( ( IEnumerable< object > )this.object_0 ).Select< object, string >( RemoteStorageClient.Class85.Class86.Class87.func_0 ?? ( RemoteStorageClient.Class85.Class86.Class87.func_0 = new Func< object, string >( RemoteStorageClient.Class85.Class86.Class87.class87_0.method_0 ) ) ).ToArray< string >( ) );
//                    }
//                }
//            }

//            private sealed class Class89
//            {
//                public RemoteStorageClient.Class85 class85_0;
//                public Tuple< string, System.Type >[ ] tuple_0;
//                public Func< string, int, object > func_0;

//                internal Tuple< string, string[ ] >[ ] method_0( IRemoteStorage iremoteStorage_0 )
//                {
//                    return iremoteStorage_0.GetAllExtendedInfo( this.class85_0.remoteStorageClient_0.SessionId, this.class85_0.string_0 );
//                }

//                internal Tuple< SecurityId, object[ ] > method_1( Tuple< string, string[ ] > tuple_1 )
//                {
//                    return Tuple.Create< SecurityId, object[ ] >( tuple_1.Item1.ToSecurityId( ( SecurityIdGenerator )null ), ( ( IEnumerable< string > )tuple_1.Item2 ).Select< string, object >( this.func_0 ?? ( this.func_0 = new Func< string, int, object >( this.method_2 ) ) ).ToArray< object >( ) );
//                }

//                internal object method_2( string string_0, int int_0 )
//                {
//                    return string_0.To( this.tuple_0[ int_0 ].Item2 );
//                }
//            }

//            [Serializable]
//            private sealed class Class90
//            {
//                public static readonly RemoteStorageClient.Class85.Class90 class90_0 = new RemoteStorageClient.Class85.Class90( );
//                public static Func< Tuple< string, string >, Tuple< string, System.Type > > func_0;
//                public static Func< Tuple< string, System.Type >, Tuple< string, string > > func_1;
//                public static Func< string, SecurityId > func_2;

//                internal Tuple< string, System.Type > method_0( Tuple< string, string > tuple_0 )
//                {
//                    return Tuple.Create< string, System.Type >( tuple_0.Item1, tuple_0.Item2.To< System.Type >( ) );
//                }

//                internal Tuple< string, string > method_1( Tuple< string, System.Type > tuple_0 )
//                {
//                    return Tuple.Create< string, string >( tuple_0.Item1, Converter.GetAlias( tuple_0.Item2 ) ?? tuple_0.Item2.GetTypeName( false ) );
//                }

//                internal SecurityId method_2( string string_0 )
//                {
//                    return string_0.ToSecurityId( ( SecurityIdGenerator )null );
//                }
//            }

//            private sealed class Class91
//            {
//                public RemoteStorageClient.Class85 class85_0;
//                public Tuple< string, System.Type >[ ] tuple_0;

//                internal void method_0( IRemoteStorage iremoteStorage_0 )
//                {
//                    iremoteStorage_0.CreateSecurityExtendedFields( this.class85_0.remoteStorageClient_0.SessionId, this.class85_0.string_0, ( ( IEnumerable< Tuple< string, System.Type > > )this.tuple_0 ).Select< Tuple< string, System.Type >, Tuple< string, string > >( RemoteStorageClient.Class85.Class90.func_1 ?? ( RemoteStorageClient.Class85.Class90.func_1 = new Func< Tuple< string, System.Type >, Tuple< string, string > >( RemoteStorageClient.Class85.Class90.class90_0.method_1 ) ) ).ToArray< Tuple< string, string > >( ) );
//                }
//            }
//        }

//        public class Class92 : IMarketDataStorageDrive
//        {
//            public DateTime GetFileCreationTime( DateTime date )
//            {
//                return DateTime.MinValue;
//            }

//            private readonly RemoteStorageClient _parent;
//            private readonly string _securityID;
//            private readonly string string_1;
//            private readonly string string_2;
//            private readonly StorageFormats storageFormats_0;
//            private readonly IMarketDataDrive _drive;

//            public IMarketDataDrive Drive
//            {
//                get
//                {
//                    return this._drive;
//                }
//            }

//            public IEnumerable< DateTime > Dates
//            {
//                get
//                {
//                    return ( IEnumerable< DateTime > )( ( IEnumerable< DateTime > )this._parent.Invoke< DateTime[ ] >( new Func< IRemoteStorage, DateTime[ ] >( this.method_0 ) ) ).Select< DateTime, DateTime >( RemoteStorageClient.Class92.Class95.func_0 ?? ( RemoteStorageClient.Class92.Class95.func_0 = new Func< DateTime, DateTime >( RemoteStorageClient.Class92.Class95.class95_0.method_0 ) ) ).ToArray< DateTime >( );
//                }
//            }

//            public Class92( RemoteStorageClient parent, string securityId, System.Type dataType, object object_0, StorageFormats storageFormats_1, IMarketDataDrive imarketDataDrive_1 )
//            {
//                if( securityId.IsDefault< string >( ) )
//                {
//                    throw new ArgumentNullException( "securityId" );
//                }

//                if( dataType == ( System.Type )null )
//                {
//                    throw new ArgumentNullException( "dataType" );
//                }

//                RemoteStorageClient remoteStorageClient = parent;
//                if( remoteStorageClient == null )
//                {
//                    throw new ArgumentNullException( "parent" );
//                }

//                this._parent = remoteStorageClient;
//                this._securityID = securityId;
//                this.string_1 = dataType.Name;
//                this.string_2 = object_0.To< string >( );
//                this.storageFormats_0 = storageFormats_1;
//                this._drive = imarketDataDrive_1;
//            }

//            void IMarketDataStorageDrive.ClearDatesCache( )
//            {
//            }

//            void IMarketDataStorageDrive.Delete( DateTime dateTime_0 )
//            {
//                this._parent.Invoke( new Action< IRemoteStorage >( new RemoteStorageClient.Class92.Class96( )
//                {
//                    class92_0 = this,
//                    dateTime_0 = dateTime_0
//                }.method_0 ) );
//            }

//            void IMarketDataStorageDrive.SaveStream(
//              DateTime dateTime_0,
//              Stream stream_0 )
//            {
//                this._parent.Invoke( new Action< IRemoteStorage >( new RemoteStorageClient.Class92.Class94( )
//                {
//                    class92_0 = this,
//                    dateTime_0 = dateTime_0,
//                    byte_0 = stream_0.To< byte[ ] >( )
//                }.method_0 ) );
//            }

//            Stream IMarketDataStorageDrive.LoadStream( DateTime dateTime_0 )
//            {
//                Stream stream = this._parent.Invoke< Stream >( new Func< IRemoteStorage, Stream >( new RemoteStorageClient.Class92.Class93( )
//                {
//                    class92_0 = this,
//                    dateTime_0 = dateTime_0
//                }.method_0 ) );
//                MemoryStream memoryStream1 = new MemoryStream( );
//                MemoryStream memoryStream2 = memoryStream1;
//                stream.CopyTo( ( Stream )memoryStream2 );
//                memoryStream1.Position = 0L;
//                if( memoryStream1.Length != 0L )
//                {
//                    return ( Stream )memoryStream1;
//                }

//                return Stream.Null;
//            }

//            private DateTime[ ] method_0( IRemoteStorage iremoteStorage_0 )
//            {
//                return iremoteStorage_0.GetDates( this._parent.SessionId, this._securityID, this.string_1, this.string_2, this.storageFormats_0 );
//            }

//            

//            private sealed class Class93
//            {
//                public RemoteStorageClient.Class92 class92_0;
//                public DateTime dateTime_0;

//                internal Stream method_0( IRemoteStorage iremoteStorage_0 )
//                {
//                    return iremoteStorage_0.LoadStream( this.class92_0._parent.SessionId, this.class92_0._securityID, this.class92_0.string_1, this.class92_0.string_2, this.dateTime_0.ChangeKind( DateTimeKind.Unspecified ), this.class92_0.storageFormats_0 );
//                }
//            }

//            private sealed class Class94
//            {
//                public RemoteStorageClient.Class92 class92_0;
//                public DateTime dateTime_0;
//                public byte[ ] byte_0;

//                internal void method_0( IRemoteStorage iremoteStorage_0 )
//                {
//                    iremoteStorage_0.Save( this.class92_0._parent.SessionId, this.class92_0._securityID, this.class92_0.string_1, this.class92_0.string_2, this.dateTime_0.ChangeKind( DateTimeKind.Unspecified ), this.class92_0.storageFormats_0, this.byte_0 );
//                }
//            }

//            [Serializable]
//            private sealed class Class95
//            {
//                public static readonly RemoteStorageClient.Class92.Class95 class95_0 = new RemoteStorageClient.Class92.Class95( );
//                public static Func< DateTime, DateTime > func_0;

//                internal DateTime method_0( DateTime dateTime_0 )
//                {
//                    return dateTime_0.ChangeKind( DateTimeKind.Utc );
//                }
//            }

//            private sealed class Class96
//            {
//                public RemoteStorageClient.Class92 class92_0;
//                public DateTime dateTime_0;

//                internal void method_0( IRemoteStorage iremoteStorage_0 )
//                {
//                    iremoteStorage_0.Delete( this.class92_0._parent.SessionId, this.class92_0._securityID, this.class92_0.string_1, this.class92_0.string_2, this.dateTime_0.ChangeKind( DateTimeKind.Unspecified ), this.class92_0.storageFormats_0 );
//                }
//            }
//        }

//        private sealed class Class97
//        {
//            public RemoteStorageClient remoteStorageClient_0;
//            public Exchange exchange_0;
//            public string[ ] string_0;

//            internal string[ ] method_0( IRemoteStorage iremoteStorage_0 )
//            {
//                return iremoteStorage_0.LookupExchanges( this.remoteStorageClient_0.SessionId, this.exchange_0 );
//            }

//            internal Exchange[ ] method_1( IRemoteStorage iremoteStorage_0 )
//            {
//                return iremoteStorage_0.GetExchanges( this.remoteStorageClient_0.SessionId, this.string_0 );
//            }
//        }

//        private sealed class Class98
//        {
//            public RemoteStorageClient remoteStorageClient_0;
//            public ExchangeBoard exchangeBoard_0;
//            public string[ ] string_0;

//            internal string[ ] method_0( IRemoteStorage iremoteStorage_0 )
//            {
//                return iremoteStorage_0.LookupExchangeBoards( this.remoteStorageClient_0.SessionId, this.exchangeBoard_0 );
//            }

//            internal ExchangeBoard[ ] method_1( IRemoteStorage iremoteStorage_0 )
//            {
//                return iremoteStorage_0.GetExchangeBoards( this.remoteStorageClient_0.SessionId, this.string_0 );
//            }
//        }

//        [Serializable]
//        private sealed class Class99
//        {
//            public static readonly RemoteStorageClient.Class99 class99_0 = new RemoteStorageClient.Class99( );
//            public static Func< string, SecurityId > func_0;
//            public static Func< StockSharp.BusinessEntities.Security, string > func_1;
//            public static Func< StockSharp.BusinessEntities.Security, string > func_2;
//            public static Func< Exchange, string > func_3;
//            public static Func< ExchangeBoard, string > func_4;
//            public static Func< string, IPAddress > func_5;
//            public static Func< Tuple< string, string[ ], UserPermissions >, Tuple< string, IPAddress[ ], UserPermissions > > func_6;
//            public static Func< IPAddress, string > func_7;
//            public static Func< Tuple< string, object >, DataType > func_8;

//            internal SecurityId method_0( string string_0 )
//            {
//                return string_0.ToSecurityId( ( SecurityIdGenerator )null );
//            }

//            internal string method_1( StockSharp.BusinessEntities.Security s )
//            {
//                return s.Id;
//            }

//            internal string method_2( StockSharp.BusinessEntities.Security security_0 )
//            {
//                return security_0.Id;
//            }

//            internal string method_3( Exchange exchange_0 )
//            {
//                return exchange_0.Name;
//            }

//            internal string method_4( ExchangeBoard exchangeBoard_0 )
//            {
//                return exchangeBoard_0.Code;
//            }

//            internal Tuple< string, IPAddress[ ], UserPermissions > method_5(
//              Tuple< string, string[ ], UserPermissions > tuple_0 )
//            {
//                return Tuple.Create< string, IPAddress[ ], UserPermissions >( tuple_0.Item1, ( ( IEnumerable< string > )tuple_0.Item2 ).Select< string, IPAddress >( RemoteStorageClient.Class99.func_5 ?? ( RemoteStorageClient.Class99.func_5 = new Func< string, IPAddress >( RemoteStorageClient.Class99.class99_0.method_6 ) ) ).ToArray< IPAddress >( ), tuple_0.Item3 );
//            }

//            internal IPAddress method_6( string string_0 )
//            {
//                return string_0.To< IPAddress >( );
//            }

//            internal string method_7( IPAddress ipaddress_0 )
//            {
//                return ipaddress_0.To< string >( );
//            }

//            internal DataType method_8( Tuple< string, object > tuple_0 )
//            {
//                return DataType.Create( typeof( CandleMessage ).To< string >( ).Replace( typeof( CandleMessage ).Name, tuple_0.Item1 ).To< System.Type >( ), tuple_0.Item2 );
//            }
//        }

//        private sealed class Class100
//        {
//            public RemoteStorageClient remoteStorageClient_0;
//            public ExchangeBoard[ ] exchangeBoard_0;

//            internal void method_0( IRemoteStorage iremoteStorage_0 )
//            {
//                iremoteStorage_0.SaveExchangeBoards( this.remoteStorageClient_0.SessionId, this.exchangeBoard_0 );
//            }
//        }

//        private sealed class Class101
//        {
//            public RemoteStorageClient remoteStorageClient_0;
//            public SecurityId securityId_0;
//            public System.Type type_0;
//            public object object_0;
//            public StorageFormats storageFormats_0;

//            internal RemoteStorageClient.Class92 method_0( Tuple< SecurityId, System.Type, object, StorageFormats > tuple_0 )
//            {
//                return new RemoteStorageClient.Class92( this.remoteStorageClient_0, this.securityId_0.ToStringId( ( SecurityIdGenerator )null ), this.type_0, this.object_0, this.storageFormats_0, ( IMarketDataDrive )this.remoteStorageClient_0.method_0( ) );
//            }
//        }

//        private sealed class Class102
//        {
//            public string[ ] string_0;
//            public RemoteStorageClient.Class82 class82_0;
//            public Func< IRemoteStorage, StockSharp.BusinessEntities.Security[ ] > func_0;

//            internal StockSharp.BusinessEntities.Security[ ] method_0(
//              IRemoteStorage iremoteStorage_0 )
//            {
//                return iremoteStorage_0.GetSecurities( this.class82_0._this.SessionId, this.string_0 );
//            }
//        }

//        private sealed class Class103
//        {
//            public RemoteStorageClient remoteStorageClient_0;
//            public Exchange[ ] exchange_0;

//            internal void method_0( IRemoteStorage iremoteStorage_0 )
//            {
//                iremoteStorage_0.SaveExchanges( this.remoteStorageClient_0.SessionId, this.exchange_0 );
//            }
//        }

//        private sealed class Class104
//        {
//            public RemoteStorageClient remoteStorageClient_0;
//            public string string_0;
//            public SecureString secureString_0;
//            public IPAddress[ ] ipaddress_0;
//            public UserPermissions userPermissions_0;

//            internal void method_0( IRemoteStorage iremoteStorage_0 )
//            {
//                iremoteStorage_0.SaveUser( this.remoteStorageClient_0.SessionId, this.string_0, this.secureString_0.To< string >( ), ( ( IEnumerable< IPAddress > )this.ipaddress_0 ).Select< IPAddress, string >( RemoteStorageClient.Class99.func_7 ?? ( RemoteStorageClient.Class99.func_7 = new Func< IPAddress, string >( RemoteStorageClient.Class99.class99_0.method_7 ) ) ).ToArray< string >( ), this.userPermissions_0 );
//            }
//        }
//    }
//}
