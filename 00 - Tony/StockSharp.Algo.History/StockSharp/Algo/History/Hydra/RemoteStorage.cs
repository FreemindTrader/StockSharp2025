//using Ecng.Collections;
//using Ecng.Common;
//using Ecng.Net;
//using MoreLinq;
//using StockSharp.Algo.Storages;
//using StockSharp.BusinessEntities;
//using StockSharp.Community;
//using StockSharp.Localization;
//using StockSharp.Logging;
//using StockSharp.Messages;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Security;

//#pragma warning disable 649

//namespace StockSharp.Algo.History.Hydra
//{
//    [ErrorLogging]
//    public abstract class RemoteStorage : BaseLogReceiver, IAuthenticationService, IRemoteStorage
//    {
//        private readonly SynchronizedDictionary< Guid, SynchronizedDictionary< UserPermissions, SynchronizedDictionary< Tuple< string, string, string, DateTime? >, bool > > > _sessionIds = new SynchronizedDictionary< Guid, SynchronizedDictionary< UserPermissions, SynchronizedDictionary< Tuple< string, string, string, DateTime? >, bool > > >( );

//        private readonly SynchronizedDictionary< string, Type > _nameToTypeDict = new SynchronizedDictionary< string, Type >( StringComparer.InvariantCultureIgnoreCase );
//        private IRemoteAuthorization                          _authorization = new AnonymousRemoteAuthorization( );
//        private int                                           _maxSecurityCount = 200;
//        private readonly IStorageRegistry                     _storageRegistry;
//        private readonly ISecurityStorage                     _securityStorage;
//        private readonly IExtendedInfoStorage                 _extendedInfoStorage;
//        private readonly IExchangeInfoProvider                _infoProvider;
//        public const int DefaultMaxSecurityCount = 200;

//        protected RemoteStorage(
//                                    IStorageRegistry storageRegistry,
//                                    ISecurityStorage securityStorage,
//                                    IExtendedInfoStorage extendedInfoStorage,
//                                    IExchangeInfoProvider exchangeInfoProvider )
//        {
//            if( storageRegistry == null )
//            {
//                throw new ArgumentNullException( nameof( storageRegistry ) );
//            }

//            if( securityStorage == null )
//            {
//                throw new ArgumentNullException( nameof( securityStorage ) );
//            }

//            if( extendedInfoStorage == null )
//            {
//                throw new ArgumentNullException( nameof( extendedInfoStorage ) );
//            }

//            if( exchangeInfoProvider == null )
//            {
//                throw new ArgumentNullException( nameof( exchangeInfoProvider ) );
//            }

//            _storageRegistry = storageRegistry;
//            _securityStorage = securityStorage;
//            _extendedInfoStorage = extendedInfoStorage;
//            _infoProvider = exchangeInfoProvider;

//            AddDataType( typeof( ExecutionMessage ) );
//            AddDataType( typeof( Level1ChangeMessage ) );
//            AddDataType( typeof( QuoteChangeMessage ) );
//            AddDataType( typeof( NewsMessage ) );
//            AddDataType( typeof( TimeFrameCandleMessage ) );
//            AddDataType( typeof( RangeCandleMessage ) );
//            AddDataType( typeof( RenkoCandleMessage ) );
//            AddDataType( typeof( PnFCandleMessage ) );
//            AddDataType( typeof( TickCandleMessage ) );
//            AddDataType( typeof( VolumeCandleMessage ) );
//            AddDataType( typeof( PositionChangeMessage ) );
//        }

//        public IStorageRegistry StorageRegistry
//        {
//            get
//            {
//                return _storageRegistry;
//            }
//        }

//        public ISecurityStorage SecurityStorage
//        {
//            get
//            {
//                return _securityStorage;
//            }
//        }

//        public IExtendedInfoStorage ExtendedInfoStorage
//        {
//            get
//            {
//                return _extendedInfoStorage;
//            }
//        }

//        public IExchangeInfoProvider ExchangeInfoProvider
//        {
//            get
//            {
//                return _infoProvider;
//            }
//        }

//        public IRemoteAuthorization Authorization
//        {
//            get
//            {
//                return _authorization;
//            }
//            set
//            {
//                if( value == null )
//                {
//                    throw new ArgumentNullException( nameof( value ) );
//                }

//                _authorization = value;
//            }
//        }

//        public int MaxSecurityCount
//        {
//            get
//            {
//                return _maxSecurityCount;
//            }
//            set
//            {
//                if( value < 1 )
//                {
//                    throw new ArgumentOutOfRangeException( );
//                }

//                _maxSecurityCount = value;
//            }
//        }

//        public event Action Restarting;

//        public event Func< bool > StartDownloading;

//        public event Action StopDownloading;

//        protected void AddDataType( Type dataType )
//        {
//            if( dataType == null )
//            {
//                throw new ArgumentNullException( nameof( dataType ) );
//            }

//            _nameToTypeDict.Add( dataType.Name, dataType );
//        }

//        protected abstract IEnumerable< IMarketDataDrive > GetDrives( );

//        private void ValidatePermission(
//                                          Guid sessionId,
//                                          UserPermissions permission,
//                                          string securityId,
//                                          string dataType,
//                                          string arg,
//                                          DateTime? date )
//        {
//            var myDict = _sessionIds.TryGetValue( sessionId );

//            if( myDict == null )
//            {
//                throw new UnauthorizedAccessException( LocalizedStrings.Str2080Params.Put( sessionId ) );
//            }

//            if( !myDict.SafeAdd( permission ).SafeAdd( Tuple.Create( securityId, dataType, arg, date ), new Func< Tuple< string, string, string, DateTime? >, bool >( x => this._authorization.HasPermissions( sessionId, permission, securityId, dataType, arg, date ) ) ) )
//            {
//                throw new UnauthorizedAccessException( LocalizedStrings.Str2081Params.Put( sessionId, permission, securityId, dataType, arg, date ) );
//            }
//        }

//        private Security GetSecurityById( string securityName, bool throwException )
//        {
//            Security security = SecurityStorage.LookupById( securityName );

//            if( throwException && security == null )
//            {
//                throw new InvalidOperationException( LocalizedStrings.Str704Params.Put( securityName ) );
//            }

//            return security;
//        }

//        private IMarketDataStorageDrive method_2(
//          string string_0,
//          string string_1,
//          string string_2,
//          StorageFormats storageFormats_0 )
//        {
//            return method_4( GetSecurityById( string_0, true ), string_1, string_2, GetDrives( ).First( ), storageFormats_0 ).Drive;
//        }

//        private IMarketDataStorage method_3(
//          string string_0,
//          string string_1,
//          string string_2,
//          DateTime dateTime_0,
//          StorageFormats storageFormats_0 )
//        {
//            Class67 class67 = new Class67( );
//            class67.remoteStorage_0 = this;
//            class67.string_0 = string_1;
//            class67.string_1 = string_2;
//            class67.storageFormats_0 = storageFormats_0;
//            class67.dateTime_0 = dateTime_0;
//            class67.security_0 = GetSecurityById( string_0, true );
//            return GetDrives( ).Select( new Func< IMarketDataDrive, IMarketDataStorage >( class67.method_0 ) ).FirstOrDefault( new Func< IMarketDataStorage, bool >( class67.method_1 ) );
//        }

//        private IMarketDataStorage method_4(
//          Security sec,
//          string string_0,
//          string string_1,
//          IMarketDataDrive imarketDataDrive_0,
//          StorageFormats storageFormats_0 )
//        {
//            Type type = _nameToTypeDict.TryGetValue( string_0 );
//            if( type == null )
//            {
//                throw new InvalidOperationException( LocalizedStrings.Str2082Params.Put( string_0 ) );
//            }

//            object obj = string_1;
//            if( type == typeof( ExecutionMessage ) )
//            {
//                obj = string_1.To< ExecutionTypes >( );
//            }
//            else if( type.IsCandleMessage( ) )
//            {
//                obj = type.ToCandleArg( string_1 );
//            }

//            return StorageRegistry.GetStorage( sec, type, obj, imarketDataDrive_0, storageFormats_0 );
//        }

//        Guid IAuthenticationService.Login( string email, string password )
//        {
//            return ( ( IAuthenticationService )this ).Login2( Products.Hydra, email, password ).Item1;
//        }

//        Tuple< Guid, long > IAuthenticationService.Login2(
//                                                              Products product,
//                                                              string email,
//                                                              string password )
//        {
//            return ( ( IAuthenticationService )this ).Login3( product, null, email, password );
//        }

//        Tuple< Guid, long > IAuthenticationService.Login3(
//                                                              Products product,
//                                                              string args,
//                                                              string login,
//                                                              string password )
//        {
//            Guid key = Authorization.ValidateCredentials( login, password.To< SecureString >( ), NetworkHelper.UserAddress );
//            _sessionIds.Add( key, new SynchronizedDictionary< UserPermissions, SynchronizedDictionary< Tuple< string, string, string, DateTime? >, bool > >( ) );
//            this.AddInfoLog( LocalizedStrings.Str2084Params, key, login, product, args );
//            return Tuple.Create( key, -1L );
//        }

//        void IAuthenticationService.Ping( Guid guid_0 )
//        {
//        }

//        void IAuthenticationService.Logout( Guid sessionId )
//        {
//            if( _sessionIds.TryGetValue( sessionId ) == null )
//            {
//                return;
//            }

//            _sessionIds.Remove( sessionId );
//            this.AddInfoLog( LocalizedStrings.Str2085Params, sessionId );
//        }

//        long IAuthenticationService.GetId( Guid guid_0 )
//        {
//            this.AddInfoLog( "GetId {0}.", guid_0 );
//            throw new NotSupportedException( );
//        }

//        private static bool CheckIfSecurityHasError( Security sec )
//        {
//            if( sec == null )
//            {
//                throw new ArgumentNullException( "security" );
//            }

//            if( sec.Id.IsEmpty( ) &&
//                    sec.Code.IsEmpty( ) &&
//                    ( sec.Class.IsEmpty( ) && sec.Name.IsEmpty( ) ) &&
//                      sec.ShortName.IsEmpty( ) && !sec.Type.HasValue &&
//                      ( !sec.OptionType.HasValue && sec.BinaryOptionType.IsEmpty( ) && !sec.Strike.HasValue ) && ( !sec.ExpiryDate.HasValue && ( !sec.Currency.HasValue && sec.UnderlyingSecurityId.IsEmpty( ) && sec.Board == null ) && ( !sec.SettlementDate.HasValue && !sec.PriceStep.HasValue ) ) && ( !sec.VolumeStep.HasValue && !sec.Decimals.HasValue && ( !sec.Multiplier.HasValue && sec.ExternalId.Bloomberg.IsEmpty( ) && ( sec.ExternalId.Cusip.IsEmpty( ) && sec.ExternalId.IQFeed.IsEmpty( ) ) && ( !sec.ExternalId.InteractiveBrokers.HasValue && sec.ExternalId.Isin.IsEmpty( ) && ( sec.ExternalId.Plaza.IsEmpty( ) && sec.ExternalId.Ric.IsEmpty( ) ) ) ) ) )
//            {
//                return sec.ExternalId.Sedol.IsEmpty( );
//            }

//            return false;
//        }

//        private static string[ ] smethod_1( IEnumerable< Security > ienumerable_0 )
//        {
//            return ienumerable_0.Select( RemoteStorage.Class73.func_0 ?? ( RemoteStorage.Class73.func_0 = new Func< Security, string >( RemoteStorage.Class73.class73_0.method_0 ) ) ).ToArray( );
//        }

//        string[ ] IRemoteStorage.LookupSecurityIds( Guid sessionId, Security security )
//        {
//            ValidatePermission( sessionId, UserPermissions.SecurityLookup, null, null, null, new DateTime?( ) );
//            this.AddInfoLog( LocalizedStrings.Str2086Params, sessionId );

//            if( security == null )
//            {
//                throw new ArgumentNullException( nameof( security ) );
//            }

//            if( RemoteStorage.CheckIfSecurityHasError( security ) )
//            {
//                return RemoteStorage.smethod_1( SecurityStorage.LookupAll( ) );
//            }

//            this.AddInfoLog( LocalizedStrings.Str2087Params, sessionId );
//            return RemoteStorage.smethod_1( SecurityStorage.Lookup( security ).Where( RemoteStorage.Class73.func_1 ?? ( RemoteStorage.Class73.func_1 = new Func< Security, bool >( RemoteStorage.Class73.class73_0.method_1 ) ) ) );
//        }

//        Security[ ] IRemoteStorage.GetSecurities(
//          Guid sessionId,
//          string[ ] securityIds )
//        {
//            ValidatePermission( sessionId, UserPermissions.SecurityLookup, null, null, null, new DateTime?( ) );
//            this.AddInfoLog( LocalizedStrings.Str2088Params, sessionId );
//            if( securityIds == null )
//            {
//                throw new ArgumentNullException( nameof( securityIds ) );
//            }

//            if( securityIds.Length > MaxSecurityCount )
//            {
//                throw new ArgumentOutOfRangeException( nameof( securityIds ) );
//            }

//            return ( ( IEnumerable< string > )securityIds ).Select( new Func< string, Security >( method_5 ) ).Where( RemoteStorage.Class73.func_2 ?? ( RemoteStorage.Class73.func_2 = new Func< Security, bool >( RemoteStorage.Class73.class73_0.method_2 ) ) ).ToArray( );
//        }

//        void IRemoteStorage.SaveSecurities( Guid sessionId, Security[ ] securities )
//        {
//            ValidatePermission( sessionId, UserPermissions.EditSecurities, null, null, null, new DateTime?( ) );
//            this.AddInfoLog( LocalizedStrings.Str2088Params, sessionId );
//            if( securities == null )
//            {
//                throw new ArgumentNullException( nameof( securities ) );
//            }

//            if( securities.IsEmpty( ) )
//            {
//                throw new ArgumentOutOfRangeException( nameof( securities ) );
//            }

//            if( securities.Length > MaxSecurityCount )
//            {
//                throw new ArgumentOutOfRangeException( nameof( securities ) );
//            }

//            foreach( Security security in securities )
//            {
//                SecurityStorage.Save( security, false );
//            }
//        }

//        void IRemoteStorage.DeleteSecurities( Guid sessionId, string[ ] securityIds )
//        {
//            ValidatePermission( sessionId, UserPermissions.DeleteSecurities, null, null, null, new DateTime?( ) );
//            if( securityIds == null )
//            {
//                throw new ArgumentNullException( nameof( securityIds ) );
//            }

//            if( securityIds.IsEmpty( ) )
//            {
//                throw new ArgumentOutOfRangeException( nameof( securityIds ) );
//            }

//            foreach( string securityId in securityIds )
//            {
//                SecurityStorage.DeleteById( securityId );
//            }
//        }

//        DateTime[ ] IRemoteStorage.GetDates(
//          Guid sessionId,
//          string securityId,
//          string dataType,
//          string arg,
//          StorageFormats format )
//        {
//            Class66 class66 = new Class66( );
//            class66.remoteStorage_0 = this;
//            class66.string_0 = dataType;
//            class66.string_1 = arg;
//            class66.storageFormats_0 = format;
//            ValidatePermission( sessionId, UserPermissions.Load, null, null, null, new DateTime?( ) );
//            this.AddInfoLog( LocalizedStrings.Str2089Params, sessionId, securityId, class66.string_0, class66.string_1 );
//            class66.security_0 = GetSecurityById( securityId, false );
//            if( class66.security_0 != null )
//            {
//                return GetDrives( ).SelectMany( new Func< IMarketDataDrive, IEnumerable< DateTime > >( class66.method_0 ) ).Distinct( ).OrderBy( ).ToArray( );
//            }

//            return ArrayHelper.Empty< DateTime >( );
//        }

//        string[ ] IRemoteStorage.GetAvailableSecurities( Guid sessionId )
//        {
//            Class74 class74 = new Class74( );
//            ValidatePermission( sessionId, UserPermissions.SecurityLookup, null, null, null, new DateTime?( ) );
//            this.AddInfoLog( LocalizedStrings.Str2088Params, sessionId );
//            class74.securityIdGenerator_0 = new SecurityIdGenerator( );
//            return GetDrives( ).SelectMany( RemoteStorage.Class73.func_3 ?? ( RemoteStorage.Class73.func_3 = new Func< IMarketDataDrive, IEnumerable< SecurityId > >( RemoteStorage.Class73.class73_0.method_3 ) ) ).Distinct( ).Select( new Func< SecurityId, string >( class74.method_0 ) ).ToArray( );
//        }

//        Tuple< string, object >[ ] IRemoteStorage.GetAvailableDataTypes(
//          Guid sessionId,
//          string securityId,
//          StorageFormats format )
//        {
//            Class75 class75 = new Class75( );
//            class75.storageFormats_0 = format;
//            ValidatePermission( sessionId, UserPermissions.Load, null, null, null, new DateTime?( ) );
//            this.AddInfoLog( LocalizedStrings.Str2090Params, sessionId, securityId );
//            class75.security_0 = GetSecurityById( securityId, false );
//            if( class75.security_0 == null )
//            {
//                return ArrayHelper.Empty< Tuple< string, object > >( );
//            }

//            return GetDrives( ).SelectMany( new Func< IMarketDataDrive, IEnumerable< DataType > >( class75.method_0 ) ).Select( RemoteStorage.Class73.func_4 ?? ( RemoteStorage.Class73.func_4 = new Func< DataType, Tuple< string, object > >( RemoteStorage.Class73.class73_0.method_4 ) ) ).Distinct( ).ToArray( );
//        }

//        void IRemoteStorage.Save(
//          Guid sessionId,
//          string securityId,
//          string dataType,
//          string arg,
//          DateTime date,
//          StorageFormats format,
//          byte[ ] data )
//        {
//            ValidatePermission( sessionId, UserPermissions.Save, securityId, dataType, arg, new DateTime?( date ) );
//            this.AddInfoLog( LocalizedStrings.Str2091Params, sessionId, securityId, dataType, arg, date );
//            method_2( securityId, dataType, arg, format ).SaveStream( date, data.To< Stream >( ) );
//        }

//        void IRemoteStorage.Delete(
//          Guid sessionId,
//          string securityId,
//          string dataType,
//          string arg,
//          DateTime date,
//          StorageFormats format )
//        {
//            ValidatePermission( sessionId, UserPermissions.Delete, securityId, dataType, arg, new DateTime?( date ) );
//            this.AddInfoLog( LocalizedStrings.Str2092Params, sessionId, securityId, dataType, arg, date );
//            method_3( securityId, dataType, arg, date, format ).Delete( date );
//        }

//        Stream IRemoteStorage.LoadStream(
//          Guid sessionId,
//          string securityId,
//          string dataType,
//          string arg,
//          DateTime date,
//          StorageFormats format )
//        {
//            ValidatePermission( sessionId, UserPermissions.Load, securityId, dataType, arg, new DateTime?( date ) );
//            this.AddInfoLog( LocalizedStrings.Str2093Params, sessionId, securityId, dataType, arg, date );
//            IMarketDataStorage marketDataStorage = method_3( securityId, dataType, arg, date, format );
//            if( marketDataStorage != null )
//            {
//                return marketDataStorage.Drive.LoadStream( date );
//            }

//            return Stream.Null;
//        }

//        string[ ] IRemoteStorage.LookupExchanges( Guid sessionId, Exchange criteria )
//        {
//            Class70 class70 = new Class70( );
//            ValidatePermission( sessionId, UserPermissions.ExchangeLookup, null, null, null, new DateTime?( ) );
//            this.AddInfoLog( LocalizedStrings.RemoteStorageLookupExchanges, sessionId, criteria );
//            if( criteria == null )
//            {
//                throw new ArgumentNullException( nameof( criteria ) );
//            }

//            class70.string_0 = criteria.Name;
//            IEnumerable< Exchange > source = ExchangeInfoProvider.Exchanges;
//            if( !class70.string_0.IsEmpty( ) )
//            {
//                source = source.Where( new Func< Exchange, bool >( class70.method_0 ) );
//            }

//            return source.Select( RemoteStorage.Class73.func_5 ?? ( RemoteStorage.Class73.func_5 = new Func< Exchange, string >( RemoteStorage.Class73.class73_0.method_5 ) ) ).ToArray( );
//        }

//        string[ ] IRemoteStorage.LookupExchangeBoards(
//          Guid sessionId,
//          ExchangeBoard criteria )
//        {
//            ValidatePermission( sessionId, UserPermissions.ExchangeBoardLookup, null, null, null, new DateTime?( ) );
//            this.AddInfoLog( LocalizedStrings.RemoteStorageLookupExchangeBoards, sessionId, criteria );
//            if( criteria == null )
//            {
//                throw new ArgumentNullException( nameof( criteria ) );
//            }

//            return ExchangeInfoProvider.LookupBoards( criteria.Code ).Select( RemoteStorage.Class73.func_6 ?? ( RemoteStorage.Class73.func_6 = new Func< ExchangeBoard, string >( RemoteStorage.Class73.class73_0.method_6 ) ) ).ToArray( );
//        }

//        Exchange[ ] IRemoteStorage.GetExchanges( Guid sessionId, string[ ] codes )
//        {
//            ValidatePermission( sessionId, UserPermissions.ExchangeLookup, null, null, null, new DateTime?( ) );
//            if( codes == null )
//            {
//                throw new ArgumentNullException( nameof( codes ) );
//            }

//            this.AddInfoLog( LocalizedStrings.RemoteStorageGetExchanges, sessionId, ( ( IEnumerable< string > )codes ).Join( "," ) );
//            return ( ( IEnumerable< string > )codes ).Select( new Func< string, Exchange >( ExchangeInfoProvider.GetExchange ) ).Where( RemoteStorage.Class73.func_7 ?? ( RemoteStorage.Class73.func_7 = new Func< Exchange, bool >( RemoteStorage.Class73.class73_0.method_7 ) ) ).ToArray( );
//        }

//        ExchangeBoard[ ] IRemoteStorage.GetExchangeBoards(
//          Guid sessionId,
//          string[ ] codes )
//        {
//            ValidatePermission( sessionId, UserPermissions.ExchangeBoardLookup, null, null, null, new DateTime?( ) );
//            if( codes == null )
//            {
//                throw new ArgumentNullException( nameof( codes ) );
//            }

//            this.AddInfoLog( LocalizedStrings.RemoteStorageGetExchangeBoards, sessionId, ( ( IEnumerable< string > )codes ).Join( "," ) );
//            return ( ( IEnumerable< string > )codes ).Select( new Func< string, ExchangeBoard >( ExchangeInfoProvider.GetExchangeBoard ) ).Where( RemoteStorage.Class73.func_8 ?? ( RemoteStorage.Class73.func_8 = new Func< ExchangeBoard, bool >( RemoteStorage.Class73.class73_0.method_8 ) ) ).ToArray( );
//        }

//        void IRemoteStorage.SaveExchanges( Guid sessionId, Exchange[ ] exchanges )
//        {
//            ValidatePermission( sessionId, UserPermissions.EditExchanges, null, null, null, new DateTime?( ) );
//            this.AddInfoLog( LocalizedStrings.RemoteStorageSaveExchanges, sessionId );
//            ( ( IEnumerable< Exchange > )exchanges ).ForEach( new Action< Exchange >( ExchangeInfoProvider.Save ) );
//        }

//        void IRemoteStorage.SaveExchangeBoards( Guid sessionId, ExchangeBoard[ ] boards )
//        {
//            ValidatePermission( sessionId, UserPermissions.EditBoards, null, null, null, new DateTime?( ) );
//            this.AddInfoLog( LocalizedStrings.RemoteStorageSaveExchangeBoards, sessionId );
//            ( ( IEnumerable< ExchangeBoard > )boards ).ForEach( new Action< ExchangeBoard >( ExchangeInfoProvider.Save ) );
//        }

//        void IRemoteStorage.DeleteExchanges( Guid sessionId, string[ ] codes )
//        {
//            ValidatePermission( sessionId, UserPermissions.DeleteExchanges, null, null, null, new DateTime?( ) );
//            if( codes == null )
//            {
//                throw new ArgumentNullException( nameof( codes ) );
//            }

//            this.AddInfoLog( LocalizedStrings.RemoteStorageDeleteExchanges, sessionId, ( ( IEnumerable< string > )codes ).Join( "," ) );
//            foreach( string code in codes )
//            {
//                Exchange exchange = ExchangeInfoProvider.GetExchange( code );
//                if( !( exchange == null ) )
//                {
//                    ExchangeInfoProvider.Delete( exchange );
//                }
//            }
//        }

//        void IRemoteStorage.DeleteExchangeBoards( Guid sessionId, string[ ] codes )
//        {
//            ValidatePermission( sessionId, UserPermissions.DeleteBoards, null, null, null, new DateTime?( ) );
//            if( codes == null )
//            {
//                throw new ArgumentNullException( nameof( codes ) );
//            }

//            this.AddInfoLog( LocalizedStrings.RemoteStorageDeleteExchangeBoards, sessionId, ( ( IEnumerable< string > )codes ).Join( "," ) );
//            foreach( string code in codes )
//            {
//                ExchangeBoard exchangeBoard = ExchangeInfoProvider.GetExchangeBoard( code );
//                if( !( exchangeBoard == null ) )
//                {
//                    ExchangeInfoProvider.Delete( exchangeBoard );
//                }
//            }
//        }

//        private static string smethod_2( Type type_0 )
//        {
//            return Converter.GetAlias( type_0 ) ?? type_0.GetTypeName( false );
//        }

//        Tuple< string, string >[ ] IRemoteStorage.GetSecurityExtendedFields(
//          Guid sessionId,
//          string storageName )
//        {
//            ValidatePermission( sessionId, UserPermissions.SecurityLookup, null, null, null, new DateTime?( ) );
//            this.AddInfoLog( LocalizedStrings.RemoteStorageGetSecurityExtendedFields, sessionId, storageName );
//            IExtendedInfoStorageItem extendedInfoStorageItem = ExtendedInfoStorage.Get( storageName );
//            if( extendedInfoStorageItem == null )
//            {
//                return null;
//            }

//            return extendedInfoStorageItem.Fields.Select( RemoteStorage.Class73.func_9 ?? ( RemoteStorage.Class73.func_9 = new Func< Tuple< string, Type >, Tuple< string, string > >( RemoteStorage.Class73.class73_0.method_9 ) ) ).ToArray( );
//        }

//        void IRemoteStorage.CreateSecurityExtendedFields(
//          Guid sessionId,
//          string storageName,
//          Tuple< string, string >[ ] fields )
//        {
//            ValidatePermission( sessionId, UserPermissions.EditSecurities, null, null, null, new DateTime?( ) );
//            if( fields == null )
//            {
//                throw new ArgumentNullException( nameof( fields ) );
//            }

//            this.AddInfoLog( LocalizedStrings.RemoteStorageCreateSecurityExtendedFields, sessionId, storageName, ( ( IEnumerable< Tuple< string, string > > )fields ).Select( RemoteStorage.Class73.func_10 ?? ( RemoteStorage.Class73.func_10 = new Func< Tuple< string, string >, string >( RemoteStorage.Class73.class73_0.method_10 ) ) ).Join( "," ) );
//            ExtendedInfoStorage.Create( storageName, ( ( IEnumerable< Tuple< string, string > > )fields ).Select( RemoteStorage.Class73.func_11 ?? ( RemoteStorage.Class73.func_11 = new Func< Tuple< string, string >, Tuple< string, Type > >( RemoteStorage.Class73.class73_0.method_11 ) ) ).ToArray( ) );
//        }

//        void IRemoteStorage.DeleteSecurityExtendedFields(
//          Guid sessionId,
//          string storageName )
//        {
//            ValidatePermission( sessionId, UserPermissions.DeleteSecurities, null, null, null, new DateTime?( ) );
//            this.AddInfoLog( LocalizedStrings.RemoteStorageDeleteSecurityExtendedFields, sessionId, storageName );
//            IExtendedInfoStorageItem storage = ExtendedInfoStorage.Get( storageName );
//            if( storage == null )
//            {
//                return;
//            }

//            ExtendedInfoStorage.Delete( storage );
//        }

//        void IRemoteStorage.AddSecurityExtendedInfo(
//          Guid sessionId,
//          string storageName,
//          string securityId,
//          string[ ] fieldValues )
//        {
//            Class76 class76 = new Class76( );
//            class76.string_0 = fieldValues;
//            ValidatePermission( sessionId, UserPermissions.EditSecurities, null, null, null, new DateTime?( ) );
//            this.AddInfoLog( LocalizedStrings.RemoteStorageAddSecurityExtendedInfo, sessionId, storageName, securityId );
//            IExtendedInfoStorageItem extendedInfoStorageItem = ExtendedInfoStorage.Get( storageName );
//            if( extendedInfoStorageItem == null )
//            {
//                return;
//            }

//            IEnumerable< Tuple< string, Type > > fields = extendedInfoStorageItem.Fields;
//            extendedInfoStorageItem.Add( securityId.ToSecurityId( null ), fields.Select( new Func< Tuple< string, Type >, int, KeyValuePair< string, object > >( class76.method_0 ) ).ToDictionary( ) );
//        }

//        void IRemoteStorage.DeleteSecurityExtendedInfo(
//          Guid sessionId,
//          string storageName,
//          string securityId )
//        {
//            ValidatePermission( sessionId, UserPermissions.EditSecurities, null, null, null, new DateTime?( ) );
//            this.AddInfoLog( LocalizedStrings.RemoteStorageDeleteSecurityExtendedInfo, sessionId, storageName, securityId );
//            ExtendedInfoStorage.Get( storageName )?.Delete( securityId.ToSecurityId( null ) );
//        }

//        string[ ] IRemoteStorage.GetSecurityExtendedStorages( Guid sessionId )
//        {
//            ValidatePermission( sessionId, UserPermissions.SecurityLookup, null, null, null, new DateTime?( ) );
//            this.AddInfoLog( LocalizedStrings.RemoteStorageGetSecurityExtendedStorages, sessionId );
//            return ExtendedInfoStorage.Storages.Select( RemoteStorage.Class73.func_12 ?? ( RemoteStorage.Class73.func_12 = new Func< IExtendedInfoStorageItem, string >( RemoteStorage.Class73.class73_0.method_12 ) ) ).ToArray( );
//        }

//        string[ ] IRemoteStorage.GetExtendedInfoSecurities(
//          Guid sessionId,
//          string storageName )
//        {
//            ValidatePermission( sessionId, UserPermissions.SecurityLookup, null, null, null, new DateTime?( ) );
//            this.AddInfoLog( LocalizedStrings.RemoteStorageGetExtendedInfoSecurities, sessionId, storageName );
//            IExtendedInfoStorageItem extendedInfoStorageItem = ExtendedInfoStorage.Get( storageName );
//            if( extendedInfoStorageItem == null )
//            {
//                return null;
//            }

//            return extendedInfoStorageItem.Securities.Select( RemoteStorage.Class73.func_13 ?? ( RemoteStorage.Class73.func_13 = new Func< SecurityId, string >( RemoteStorage.Class73.class73_0.method_13 ) ) ).ToArray( );
//        }

//        string[ ] IRemoteStorage.GetSecurityExtendedInfo(
//          Guid sessionId,
//          string storageName,
//          string securityId )
//        {
//            Class72 class72 = new Class72( );
//            ValidatePermission( sessionId, UserPermissions.SecurityLookup, null, null, null, new DateTime?( ) );
//            this.AddInfoLog( LocalizedStrings.RemoteStorageGetSecurityExtendedInfo, sessionId, storageName, securityId );
//            IExtendedInfoStorageItem extendedInfoStorageItem = ExtendedInfoStorage.Get( storageName );
//            class72.idictionary_0 = extendedInfoStorageItem?.Load( securityId.ToSecurityId( null ) );
//            if( class72.idictionary_0 == null )
//            {
//                return null;
//            }

//            return extendedInfoStorageItem.Fields.Select( new Func< Tuple< string, Type >, string >( class72.method_0 ) ).ToArray( );
//        }

//        Tuple< string, string[ ] >[ ] IRemoteStorage.GetAllExtendedInfo(
//          Guid sessionId,
//          string storageName )
//        {
//            Class68 class68 = new Class68( );
//            ValidatePermission( sessionId, UserPermissions.SecurityLookup, null, null, null, new DateTime?( ) );
//            this.AddInfoLog( LocalizedStrings.RemoteStorageGetAllExtendedInfo, sessionId, storageName );
//            class68.iextendedInfoStorageItem_0 = ExtendedInfoStorage.Get( storageName );
//            if( class68.iextendedInfoStorageItem_0 == null )
//            {
//                return null;
//            }

//            return class68.iextendedInfoStorageItem_0.Load( ).Select( new Func< Tuple< SecurityId, IDictionary< string, object > >, Tuple< string, string[ ] > >( class68.method_0 ) ).ToArray( );
//        }

//        Tuple< string, string[ ], UserPermissions >[ ] IRemoteStorage.GetUsers(
//          Guid sessionId )
//        {
//            ValidatePermission( sessionId, UserPermissions.GetUsers, null, null, null, new DateTime?( ) );
//            this.AddInfoLog( LocalizedStrings.RemoteStorageGetUsers, sessionId );
//            return _authorization.AllRemoteUsers.Select( RemoteStorage.Class73.func_15 ?? ( RemoteStorage.Class73.func_15 = new Func< Tuple< string, IEnumerable< IPAddress >, UserPermissions >, Tuple< string, string[ ], UserPermissions > >( RemoteStorage.Class73.class73_0.method_14 ) ) ).ToArray( );
//        }

//        void IRemoteStorage.SaveUser(
//          Guid sessionId,
//          string login,
//          string password,
//          string[ ] ipAddresses,
//          UserPermissions permissions )
//        {
//            ValidatePermission( sessionId, UserPermissions.EditUsers, null, null, null, new DateTime?( ) );
//            this.AddInfoLog( LocalizedStrings.RemoteStorageSaveUser, sessionId, login, ( ( IEnumerable< string > )ipAddresses ).Join( "," ), permissions );
//            _authorization.SaveRemoteUser( login, password.To< SecureString >( ), ( ( IEnumerable< string > )ipAddresses ).Select( RemoteStorage.Class73.func_16 ?? ( RemoteStorage.Class73.func_16 = new Func< string, IPAddress >( RemoteStorage.Class73.class73_0.method_16 ) ) ).ToArray( ), permissions );
//        }

//        void IRemoteStorage.DeleteUser( Guid sessionId, string login )
//        {
//            ValidatePermission( sessionId, UserPermissions.DeleteUsers, null, null, null, new DateTime?( ) );
//            this.AddInfoLog( LocalizedStrings.RemoteStorageDeleteUser, sessionId, login );
//            _authorization.DeleteRemoteUser( login );
//        }

//        void IRemoteStorage.Restart( Guid sessionId )
//        {
//            ValidatePermission( sessionId, UserPermissions.ServerManage, null, null, null, new DateTime?( ) );
//            this.AddInfoLog( LocalizedStrings.RemoteStorageRestart, sessionId );
//            Action action0 = Restarting;
//            if( action0 == null )
//            {
//                return;
//            }

//            action0( );
//        }

//        bool IRemoteStorage.StartDownloading( Guid sessionId )
//        {
//            ValidatePermission( sessionId, UserPermissions.ServerManage, null, null, null, new DateTime?( ) );
//            this.AddInfoLog( LocalizedStrings.RemoteStorageStartDownloading, sessionId );
//            Func< bool > func0 = StartDownloading;
//            if( func0 == null )
//            {
//                return false;
//            }

//            return func0( );
//        }

//        void IRemoteStorage.StopDownloading( Guid sessionId )
//        {
//            ValidatePermission( sessionId, UserPermissions.ServerManage, null, null, null, new DateTime?( ) );
//            this.AddInfoLog( LocalizedStrings.RemoteStorageStopDownloading, sessionId );
//            Action action1 = StopDownloading;
//            if( action1 == null )
//            {
//                return;
//            }

//            action1( );
//        }

//        private Security method_5( string string_0 )
//        {
//            return GetSecurityById( string_0, false );
//        }

//        private sealed class Class66
//        {
//            public RemoteStorage remoteStorage_0;
//            public Security security_0;
//            public string string_0;
//            public string string_1;
//            public StorageFormats storageFormats_0;

//            internal IEnumerable< DateTime > method_0( IMarketDataDrive imarketDataDrive_0 )
//            {
//                return remoteStorage_0.method_4( security_0, string_0, string_1, imarketDataDrive_0, storageFormats_0 ).Dates;
//            }
//        }

//        private sealed class Class67
//        {
//            public RemoteStorage remoteStorage_0;
//            public Security security_0;
//            public string string_0;
//            public string string_1;
//            public StorageFormats storageFormats_0;
//            public DateTime dateTime_0;

//            internal IMarketDataStorage method_0( IMarketDataDrive imarketDataDrive_0 )
//            {
//                return remoteStorage_0.method_4( security_0, string_0, string_1, imarketDataDrive_0, storageFormats_0 );
//            }

//            internal bool method_1( IMarketDataStorage imarketDataStorage_0 )
//            {
//                return imarketDataStorage_0.Dates.Contains( dateTime_0 );
//            }
//        }

//        private sealed class Class68
//        {
//            public IExtendedInfoStorageItem iextendedInfoStorageItem_0;

//            internal Tuple< string, string[ ] > method_0(
//              Tuple< SecurityId, IDictionary< string, object > > tuple_0 )
//            {
//                Class71 class71 = new Class71( );
//                class71.tuple_0 = tuple_0;
//                return Tuple.Create( class71.tuple_0.Item1.ToStringId( null ), iextendedInfoStorageItem_0.Fields.Select( new Func< Tuple< string, Type >, string >( class71.method_0 ) ).ToArray( ) );
//            }
//        }

//        private sealed class Class69
//        {
//            public RemoteStorage remoteStorage_0;
//            public Guid sessionId;
//            public UserPermissions userPermissions_0;
//            public string securityId;
//            public string dataType;
//            public string arg;
//            public DateTime? date;

//            internal bool method_0( Tuple< string, string, string, DateTime? > x )
//            {
//                return remoteStorage_0._authorization.HasPermissions( sessionId, userPermissions_0, securityId, dataType, arg, date );
//            }
//        }

//        private sealed class Class70
//        {
//            public string string_0;

//            internal bool method_0( Exchange exchange_0 )
//            {
//                return exchange_0.Name.ContainsIgnoreCase( string_0 );
//            }
//        }

//        private sealed class Class71
//        {
//            public Tuple< SecurityId, IDictionary< string, object > > tuple_0;

//            internal string method_0( Tuple< string, Type > tuple_1 )
//            {
//                return tuple_0.Item2[ tuple_1.Item1 ].To< string >( );
//            }
//        }

//        private sealed class Class72
//        {
//            public IDictionary< string, object > idictionary_0;

//            internal string method_0( Tuple< string, Type > tuple_0 )
//            {
//                return idictionary_0[ tuple_0.Item1 ].To< string >( );
//            }
//        }

//        [Serializable]
//        private sealed class Class73
//        {
//            public static readonly Class73 class73_0 = new Class73( );
//            public static Func< Security, string > func_0;
//            public static Func< Security, bool > func_1;
//            public static Func< Security, bool > func_2;
//            public static Func< IMarketDataDrive, IEnumerable< SecurityId > > func_3;
//            public static Func< DataType, Tuple< string, object > > func_4;
//            public static Func< Exchange, string > func_5;
//            public static Func< ExchangeBoard, string > func_6;
//            public static Func< Exchange, bool > func_7;
//            public static Func< ExchangeBoard, bool > func_8;
//            public static Func< Tuple< string, Type >, Tuple< string, string > > func_9;
//            public static Func< Tuple< string, string >, string > func_10;
//            public static Func< Tuple< string, string >, Tuple< string, Type > > func_11;
//            public static Func< IExtendedInfoStorageItem, string > func_12;
//            public static Func< SecurityId, string > func_13;
//            public static Func< IPAddress, string > func_14;
//            public static Func< Tuple< string, IEnumerable< IPAddress >, UserPermissions >, Tuple< string, string[ ], UserPermissions > > func_15;
//            public static Func< string, IPAddress > func_16;

//            internal string method_0( Security security_0 )
//            {
//                return security_0.Id;
//            }

//            internal bool method_1( Security security_0 )
//            {
//                return security_0.Board != ExchangeBoard.Test;
//            }

//            internal bool method_2( Security security_0 )
//            {
//                return security_0 != null;
//            }

//            internal IEnumerable< SecurityId > method_3(
//              IMarketDataDrive imarketDataDrive_0 )
//            {
//                return imarketDataDrive_0.AvailableSecurities;
//            }

//            internal Tuple< string, object > method_4( DataType dataType_0 )
//            {
//                return Tuple.Create( dataType_0.MessageType.Name, dataType_0.Arg );
//            }

//            internal string method_5( Exchange exchange_0 )
//            {
//                return exchange_0.Name;
//            }

//            internal string method_6( ExchangeBoard exchangeBoard_0 )
//            {
//                return exchangeBoard_0.Code;
//            }

//            internal bool method_7( Exchange exchange_0 )
//            {
//                return exchange_0 != null;
//            }

//            internal bool method_8( ExchangeBoard exchangeBoard_0 )
//            {
//                return exchangeBoard_0 != null;
//            }

//            internal Tuple< string, string > method_9( Tuple< string, Type > tuple_0 )
//            {
//                return Tuple.Create( tuple_0.Item1, RemoteStorage.smethod_2( tuple_0.Item2 ) );
//            }

//            internal string method_10( Tuple< string, string > tuple_0 )
//            {
//                return tuple_0.Item1 + "=" + tuple_0.Item2;
//            }

//            internal Tuple< string, Type > method_11( Tuple< string, string > tuple_0 )
//            {
//                return Tuple.Create( tuple_0.Item1, tuple_0.Item2.To< Type >( ) );
//            }

//            internal string method_12(
//              IExtendedInfoStorageItem iextendedInfoStorageItem_0 )
//            {
//                return iextendedInfoStorageItem_0.StorageName;
//            }

//            internal string method_13( SecurityId securityId_0 )
//            {
//                return securityId_0.ToStringId( null );
//            }

//            internal Tuple< string, string[ ], UserPermissions > method_14(
//              Tuple< string, IEnumerable< IPAddress >, UserPermissions > tuple_0 )
//            {
//                return Tuple.Create( tuple_0.Item1, tuple_0.Item2.Select( RemoteStorage.Class73.func_14 ?? ( RemoteStorage.Class73.func_14 = new Func< IPAddress, string >( RemoteStorage.Class73.class73_0.method_15 ) ) ).ToArray( ), tuple_0.Item3 );
//            }

//            internal string method_15( IPAddress ipaddress_0 )
//            {
//                return ipaddress_0.To< string >( );
//            }

//            internal IPAddress method_16( string string_0 )
//            {
//                return string_0.To< IPAddress >( );
//            }
//        }

//        private sealed class Class74
//        {
//            public SecurityIdGenerator securityIdGenerator_0;

//            internal string method_0( SecurityId securityId_0 )
//            {
//                return securityIdGenerator_0.GenerateId( securityId_0.SecurityCode, securityId_0.BoardCode );
//            }
//        }

//        private sealed class Class75
//        {
//            public Security security_0;
//            public StorageFormats storageFormats_0;

//            internal IEnumerable< DataType > method_0( IMarketDataDrive imarketDataDrive_0 )
//            {
//                return imarketDataDrive_0.GetAvailableDataTypes( security_0.ToSecurityId( null ), storageFormats_0 );
//            }
//        }

//        private sealed class Class76
//        {
//            public string[ ] string_0;

//            internal KeyValuePair< string, object > method_0(
//              Tuple< string, Type > tuple_0,
//              int int_0 )
//            {
//                return new KeyValuePair< string, object >( tuple_0.Item1, string_0[ int_0 ].To( tuple_0.Item2 ) );
//            }
//        }
//    }
//}
