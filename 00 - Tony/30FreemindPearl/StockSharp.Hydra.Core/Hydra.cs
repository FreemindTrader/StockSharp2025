// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Core.Hydra
// Assembly: StockSharp.Hydra.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BF4FBD4E-7629-47D5-B0AC-6D48C0A60551
// Assembly location: T:\00-StockSharp\Data\StockSharp.Hydra.Core.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.Configuration;
using Ecng.Security;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.Algo.Candles.Compression;
using StockSharp.Algo.Storages;
using StockSharp.Algo.Storages.Csv;
using StockSharp.Algo.Testing;
using StockSharp.BusinessEntities;
using StockSharp.Configuration;
using StockSharp.Hydra.Core.Server;

using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Server.Core;
using StockSharp.Server.Fix;
using StockSharp.Studio.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;

namespace StockSharp.Hydra.Core
{
    /// <summary>Hydra logic.</summary>
    public class Hydra : BaseLogReceiver
    {
        private readonly SynchronizedDictionary<IHydraTask, HydraTaskSecurity> _taskAllSecurities = new SynchronizedDictionary<IHydraTask, HydraTaskSecurity>();
        private readonly SynchronizedDictionary<IHydraTask, SynchronizedDictionary<StockSharp.BusinessEntities.Security, HydraTaskSecurity>> _taskSecurityCache = new SynchronizedDictionary<IHydraTask, SynchronizedDictionary<StockSharp.BusinessEntities.Security, HydraTaskSecurity>>();
        private readonly StockSharp.Hydra.Core.Hydra.HydraAuthorization _authorization;

        /// <summary>Server mode mapping security id storage.</summary>
        public ISecurityMappingStorage ServerMapping
        {
            get
            {
                return ConfigManager.GetService<ISecurityMappingStorage>( "server_mapping" );
            }
        }

        /// <summary>Hydra server.</summary>
        public HydraServer Server { get; }

        /// <summary>Authorization module.</summary>
        public IAuthorization Authorization
        {
            get
            {
                return ( IAuthorization )this._authorization;
            }
        }

        /// <summary>Hydra server settings.</summary>
        public HydraServerSettings ServerSettings { get; }

        /// <summary>Count of loaded ticks.</summary>
        public long LoadedTrades { get; private set; }

        /// <summary>Count of loaded order books.</summary>
        public long LoadedDepths { get; private set; }

        /// <summary>Count of loaded order logs.</summary>
        public long LoadedOrderLog { get; private set; }

        /// <summary>Count of loaded level1.</summary>
        public long LoadedLevel1 { get; private set; }

        /// <summary>Count of loaded candles.</summary>
        public long LoadedCandles { get; private set; }

        /// <summary>Count of loaded news.</summary>
        public long LoadedNews { get; private set; }

        /// <summary>Count of loaded transactions.</summary>
        public long LoadedTransactions { get; private set; }

        /// <summary>Settings of exchange emulator.</summary>
        public MarketEmulatorSettings EmulatorSettings { get; }

        /// <summary>
        /// <see cref="T:Ecng.Security.IAuthorization" /> provider.
        /// </summary>
        public AuthorizationProvider AuthProvider { get; }

        public Hydra(
          HydraServerSettings serverSettings,
          MarketEmulatorSettings emulatorSettings,
          AuthorizationProvider authProvider,
          Func<string, PermissionCredentials> getPermissions )
        {
            HydraServerSettings hydraServerSettings = serverSettings;
            if ( hydraServerSettings == null )
                throw new ArgumentNullException( nameof( serverSettings ) );
            this.ServerSettings = hydraServerSettings;
            MarketEmulatorSettings emulatorSettings1 = emulatorSettings;
            if ( emulatorSettings1 == null )
                throw new ArgumentNullException( nameof( emulatorSettings ) );
            this.EmulatorSettings = emulatorSettings1;
            AuthorizationProvider authorizationProvider = authProvider;
            if ( authorizationProvider == null )
                throw new ArgumentNullException( nameof( authProvider ) );
            this.AuthProvider = authorizationProvider;
            ConfigManager.RegisterService<IStorageRegistry>( ( IStorageRegistry )new StorageRegistry()
            {
                DefaultDrive = ( IMarketDataDrive )new LocalMarketDataDrive( Path.Combine( Directory.GetCurrentDirectory(), "Storage" ) )
            } );
            CsvEntityRegistry csvEntityRegistry = new CsvEntityRegistry( Paths.AppDataPath );
            ConfigManager.RegisterService<IEntityRegistry>( ( IEntityRegistry )csvEntityRegistry );
            INativeIdStorage service1 = ( INativeIdStorage )new CsvNativeIdStorage( Paths.SecurityNativeIdDir ) { DelayAction = csvEntityRegistry.DelayAction };
            ISecurityMappingStorage service2 = ( ISecurityMappingStorage )new CsvSecurityMappingStorage( Paths.SecurityMappingDir ) { DelayAction = csvEntityRegistry.DelayAction };
            IExtendedInfoStorage service3 = ( IExtendedInfoStorage )new CsvExtendedInfoStorage( Paths.SecurityExtendedInfo ) { DelayAction = csvEntityRegistry.DelayAction };
            ISecurityMappingStorage service4 = ( ISecurityMappingStorage )new CsvSecurityMappingStorage( Path.Combine( Paths.AppDataPath, "Server mapping" ) ) { DelayAction = csvEntityRegistry.DelayAction };
            HydraStorage service5 = new HydraStorage( Path.Combine( Paths.AppDataPath, "Tasks" ) );
            service5.DelayAction = csvEntityRegistry.DelayAction;
            ConfigManager.RegisterService<INativeIdStorage>( service1 );
            ConfigManager.RegisterService<ISecurityMappingStorage>( service2 );
            ConfigManager.RegisterService<IExtendedInfoStorage>( service3 );
            ConfigManager.RegisterService<HydraStorage>( service5 );
            ConfigManager.RegisterService<ISecurityMappingStorage>( "server_mapping", service4 );
            StorageExchangeInfoProvider exchangeInfoProvider = new StorageExchangeInfoProvider( ( IEntityRegistry )csvEntityRegistry, true );
            ConfigManager.RegisterService<IExchangeInfoProvider>( ( IExchangeInfoProvider )exchangeInfoProvider );
            ConfigManager.RegisterService<CandleBuilderProvider>( new CandleBuilderProvider( ( IExchangeInfoProvider )exchangeInfoProvider ) );
            ConfigManager.RegisterService<ISecurityProvider>( ( ISecurityProvider )new FilterableSecurityProvider( ( ISecurityProvider )csvEntityRegistry.Securities ) );
            ConfigManager.RegisterService<ISecurityStorage>( ( ISecurityStorage )csvEntityRegistry.Securities );
            ConfigManager.RegisterService<IPositionStorage>( csvEntityRegistry.PositionStorage );
            ConfigManager.TryRegisterService<IMessageAdapterProvider>( ( IMessageAdapterProvider )new InMemoryMessageAdapterProvider( Enumerable.Empty<IMessageAdapter>() ) );
            HydraTaskManager service6 = new HydraTaskManager();
            service6.Parent = ( ILogSource )this;
            ConfigManager.RegisterService<HydraTaskManager>( service6 );
            this._authorization = new StockSharp.Hydra.Core.Hydra.HydraAuthorization( ( ILogReceiver )this );
            HydraServer hydraServer = new HydraServer( this, getPermissions );
            hydraServer.Parent = ( ILogSource )this;
            this.Server = hydraServer;
            this.Server.EmulatorSettings.Apply<MarketEmulatorSettings>( this.EmulatorSettings );
        }

        /// <inheritdoc />
        protected override void DisposeManaged()
        {
            this.Server.Dispose();
            base.DisposeManaged();
        }

        /// <summary>
        /// </summary>
        public static string Validate
        {
            get
            {
                return "HydraServer";//.ValidateLicense( ( string )null, ( Type )null );
            }
        }

        /// <summary>Initialize.</summary>
        public void Initialize()
        {
            StudioBaseHelper.InitializeDriveCache();
            LoggingHelper.DoWithLog<string>( new Func<IDictionary<string, Exception>>( ServicesRegistry.NativeIdStorage.Init ) );
            LoggingHelper.DoWithLog<string>( new Func<IDictionary<string, Exception>>( ServicesRegistry.MappingStorage.Init ) );
            LoggingHelper.DoWithLog<IExtendedInfoStorageItem>( new Func<IDictionary<IExtendedInfoStorageItem, Exception>>( ServicesRegistry.ExtendedInfoStorage.Init ) );
            LoggingHelper.DoWithLog<object>( new Func<IDictionary<object, Exception>>( ServicesRegistry.EntityRegistry.Init ) );
            LoggingHelper.DoWithLog<string>( new Func<IDictionary<string, Exception>>( this.ServerMapping.Init ) );
            new Action( ConfigManager.GetService<HydraStorage>().Init ).DoWithLog();
            IEntityRegistry entityRegistry = ServicesRegistry.EntityRegistry;
            if ( entityRegistry.Securities.GetAllSecurity() == null )
            {
                entityRegistry.Securities.Add( TraderHelper.AllSecurity );
                entityRegistry.WaitSecuritiesFlush();
            }
            HydraTaskManager instance = HydraTaskManager.Instance;
            instance.TaskAdded += new Action<IHydraTask>( this.OnTaskAdded );
            instance.TaskRemoved += new Action<IHydraTask>( this.OnTaskRemoved );
            instance.Init( ServicesRegistry.AdapterProvider.PossibleAdapters.Select<IMessageAdapter, Type>( ( Func<IMessageAdapter, Type> )( a => a.GetType() ) ), new Func<string, Type>( this.ConvertConnectorTask ) );
        }

        private Type ConvertConnectorTask( string type )
        {
            try
            {
                type = type.Substring( 0, type.IndexOf( ',' ) );
                type = type.Substring( type.LastIndexOf( '.' ) + 1 ).Remove( "Task", false );
                Type type1 = Type.GetType( "StockSharp." + type + "." + type + "MessageAdapter, StockSharp." + type, false, true );
                if ( type1 == ( Type )null )
                    return ( Type )null;
                return typeof( ConnectorHydraTask<> ).Make( type1 );
            }
            catch ( Exception ex )
            {
                ex.LogError( ( string )null );
                return ( Type )null;
            }
        }

        private void TaskNewOutMessage( Message message )
        {
            if ( message.BackMode != MessageBackModes.None )
                return;
            MessageTypes type = message.Type;
            if ( type <= MessageTypes.Execution )
            {
                if ( type != MessageTypes.Level1Change )
                {
                    if ( type == MessageTypes.Time )
                        return;
                    if ( ( uint )( type - 13 ) > 1U )
                        goto label_13;
                }
                long originalTransactionId = ( ( IOriginalTransactionIdMessage )message ).OriginalTransactionId;
            }
            else if ( type <= MessageTypes.Error )
            {
                if ( ( uint )( type - 19 ) <= 1U || type == MessageTypes.Error )
                    return;
            }
            else if ( ( uint )( type - 36 ) <= 2U || ( uint )( type - 65 ) <= 1U )
                return;
            label_13:
            if ( this.Server.State != ChannelStates.Started )
                return;
            this.Server.TrySendByFix( this.TryRemoveSubscriptionId( message ) );
        }

        private Message TryRemoveSubscriptionId( Message message )
        {
            message = message.Clone();
            IOriginalTransactionIdMessage transactionIdMessage = message as IOriginalTransactionIdMessage;
            if ( transactionIdMessage != null )
                transactionIdMessage.OriginalTransactionId = 0L;
            ISubscriptionIdMessage subscriptionIdMessage = message as ISubscriptionIdMessage;
            if ( subscriptionIdMessage != null )
            {
                subscriptionIdMessage.SubscriptionId = 0L;
                subscriptionIdMessage.SubscriptionIds = ( long[ ] )null;
            }
            return message;
        }

        private void Task_OnStopped( IHydraTask task )
        {
            this._taskAllSecurities.Remove( task );
            this._taskSecurityCache.Remove( task );
        }

        private void OnTaskAdded( IHydraTask task )
        {
            IMessageChannel msgChannel = task as IMessageChannel;
            if ( msgChannel != null )
                msgChannel.NewOutMessage += new Action<Message>( this.TaskNewOutMessage );
            task.Stopped += new Action<IHydraTask>( this.Task_OnStopped );
            task.DataLoaded += ( Action<StockSharp.BusinessEntities.Security, DataType, DateTimeOffset?, int, IEnumerable<Message>> )( ( security, dataType, time, count, messages ) =>
            {
                if ( security == null )
                    security = TraderHelper.AllSecurity;
                if ( msgChannel == null && this.Server.State == ChannelStates.Started )
                {
                    foreach ( Message message in messages )
                        this.Server.TrySendByFix( this.TryRemoveSubscriptionId( message ) );
                }
                HydraTaskSecurity security1 = this._taskAllSecurities.SafeAdd<IHydraTask, HydraTaskSecurity>( task, ( Func<IHydraTask, HydraTaskSecurity> )( _ => task.GetAllSecurity() ) );
                HydraTaskSecurity security2;
                HydraTaskSecurity.TypeInfo typeInfo1;
                HydraTaskSecurity.TypeInfo typeInfo2;
                if ( ( Equatable<DataType> )dataType == DataType.News )
                {
                    security2 = ( HydraTaskSecurity )null;
                    typeInfo1 = ( HydraTaskSecurity.TypeInfo )null;
                    typeInfo2 = security1 != null ? ( HydraTaskSecurity.TypeInfo )security1.InfoDict.SafeAdd<DataType, HydraTaskSecurity.DateTypeInfo>( dataType ) : ( HydraTaskSecurity.TypeInfo )null;
                    this.LoadedNews += ( long )count;
                }
                else
                {
                    security2 = this._taskSecurityCache.SafeAdd<IHydraTask, SynchronizedDictionary<StockSharp.BusinessEntities.Security, HydraTaskSecurity>>( task ).SafeAdd<StockSharp.BusinessEntities.Security, HydraTaskSecurity>( security, ( Func<StockSharp.BusinessEntities.Security, HydraTaskSecurity> )( key => task.Securities.FirstOrDefault<HydraTaskSecurity>( ( Func<HydraTaskSecurity, bool> )( s => s.Security == key ) ) ) );
                    if ( ( Equatable<DataType> )dataType == DataType.Ticks )
                        this.LoadedTrades += ( long )count;
                    else if ( ( Equatable<DataType> )dataType == DataType.OrderLog )
                        this.LoadedOrderLog += ( long )count;
                    else if ( ( Equatable<DataType> )dataType == DataType.Transactions )
                        this.LoadedTransactions += ( long )count;
                    else if ( ( Equatable<DataType> )dataType == DataType.MarketDepth )
                        this.LoadedDepths += ( long )count;
                    else if ( ( Equatable<DataType> )dataType == DataType.Level1 )
                        this.LoadedLevel1 += ( long )count;
                    else if ( ( Equatable<DataType> )dataType == DataType.PositionChanges )
                    {
                        this.LoadedLevel1 += ( long )count;
                    }
                    else
                    {
                        if ( ( Equatable<DataType> )dataType == DataType.BoardState )
                            return;
                        if ( !dataType.IsCandles )
                            throw new ArgumentOutOfRangeException( nameof( dataType ), ( object )dataType, LocalizedStrings.Str1018 );
                        this.LoadedCandles += ( long )count;
                    }
                    typeInfo1 = security2 != null ? ( HydraTaskSecurity.TypeInfo )security2.InfoDict.SafeAdd<DataType, HydraTaskSecurity.DateTypeInfo>( dataType ) : ( HydraTaskSecurity.TypeInfo )null;
                    typeInfo2 = security1 != null ? ( HydraTaskSecurity.TypeInfo )security1.InfoDict.SafeAdd<DataType, HydraTaskSecurity.DateTypeInfo>( dataType ) : ( HydraTaskSecurity.TypeInfo )null;
                }
                if ( typeInfo2 != null )
                {
                    UpdateInfo( typeInfo2 );
                    if ( dataType.IsCandles )
                        UpdateInfo( ( HydraTaskSecurity.TypeInfo )security1.InfoDict.SafeAdd<DataType, HydraTaskSecurity.DateTypeInfo>( dataType ) );
                    task.UpdateSecurity( security1 );
                }
                if ( typeInfo1 == null )
                    return;
                UpdateInfo( typeInfo1 );
                if ( dataType.IsCandles )
                    UpdateInfo( ( HydraTaskSecurity.TypeInfo )security2.InfoDict.SafeAdd<DataType, HydraTaskSecurity.DateTypeInfo>( dataType ) );
                task.UpdateSecurity( security2 );

                void UpdateInfo( HydraTaskSecurity.TypeInfo typeInfo )
                {
                    typeInfo.Count += ( long )count;
                    if ( !time.HasValue )
                        return;
                    typeInfo.LastTime = new DateTime?( time.Value.LocalDateTime );
                }
            } );
        }

        private void OnTaskRemoved( IHydraTask task )
        {
            task.Stopped -= new Action<IHydraTask>( this.Task_OnStopped );
            IMessageChannel messageChannel = task as IMessageChannel;
            if ( messageChannel == null )
                return;
            messageChannel.NewOutMessage -= new Action<Message>( this.TaskNewOutMessage );
        }

        public bool ApplyServerSettings( bool needSave, Func<bool> canStart )
        {
            if ( canStart == null )
                throw new ArgumentNullException( nameof( canStart ) );
            if ( this.Server.State == ChannelStates.Started )
                this.Server.Close();
            this.Server.TransportSettings.Apply<FixServerSettings>( this.ServerSettings.ServerSettings );
            this._authorization.Underlying = this.AuthProvider[this.ServerSettings.Authorization];
            if ( this.ServerSettings.IsFixServer )
            {
                if ( canStart() )
                {
                    this.Server.Open();
                }
                else
                {
                    this.ServerSettings.IsFixServer = false;
                    needSave = true;
                }
            }
            return needSave;
        }

        private class HydraAuthorization : IAuthorization
        {
            private IAuthorization _underlying = ( IAuthorization )new StockSharp.Hydra.Core.Hydra.HydraAuthorization.DisabledAuthorization();
            private readonly ILogReceiver _logReceiver;

            public HydraAuthorization( ILogReceiver logReceiver )
            {
                ILogReceiver logReceiver1 = logReceiver;
                if ( logReceiver1 == null )
                    throw new ArgumentNullException( nameof( logReceiver ) );
                this._logReceiver = logReceiver1;
            }

            public IAuthorization Underlying
            {
                get
                {
                    return this._underlying;
                }
                set
                {
                    IAuthorization authorization = value;
                    if ( authorization == null )
                        throw new ArgumentNullException( nameof( value ) );
                    this._underlying = authorization;
                }
            }

            string IAuthorization.ValidateCredentials(
              string login,
              SecureString password,
              IPAddress clientAddress )
            {
                string validate = StockSharp.Hydra.Core.Hydra.Validate;
                if ( !validate.IsEmpty() )
                {
                    this._logReceiver.AddErrorLog( validate );
                    throw new UnauthorizedAccessException( LocalizedStrings.Str2083 );
                }
                return this._underlying.ValidateCredentials( login, password, clientAddress );
            }

            private class DisabledAuthorization : IAuthorization
            {
                string IAuthorization.ValidateCredentials(
                  string login,
                  SecureString password,
                  IPAddress clientAddress )
                {
                    throw new UnauthorizedAccessException();
                }
            }
        }
    }
}
