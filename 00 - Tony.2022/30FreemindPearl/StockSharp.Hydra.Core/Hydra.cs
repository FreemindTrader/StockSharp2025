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
        private readonly SynchronizedDictionary<IHydraTask, SynchronizedDictionary<Security, HydraTaskSecurity>> _taskSecurityCache = new SynchronizedDictionary<IHydraTask, SynchronizedDictionary<Security, HydraTaskSecurity>>();
        private readonly HydraAuthorization _authorization;

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
                return _authorization;
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
            ServerSettings = hydraServerSettings;
            MarketEmulatorSettings emulatorSettings1 = emulatorSettings;
            if ( emulatorSettings1 == null )
                throw new ArgumentNullException( nameof( emulatorSettings ) );
            EmulatorSettings = emulatorSettings1;
            AuthorizationProvider authorizationProvider = authProvider;
            if ( authorizationProvider == null )
                throw new ArgumentNullException( nameof( authProvider ) );
            AuthProvider = authorizationProvider;
            ConfigManager.RegisterService<IStorageRegistry>( new StorageRegistry()
            {
                DefaultDrive = new LocalMarketDataDrive( Path.Combine( Directory.GetCurrentDirectory(), "Storage" ) )
            } );
            CsvEntityRegistry csvEntityRegistry = new CsvEntityRegistry( Paths.AppDataPath );
            ConfigManager.RegisterService<IEntityRegistry>( csvEntityRegistry );
            INativeIdStorage service1 = new CsvNativeIdStorage( Paths.SecurityNativeIdDir ) { DelayAction = csvEntityRegistry.DelayAction };
            ISecurityMappingStorage service2 = new CsvSecurityMappingStorage( Paths.SecurityMappingDir ) { DelayAction = csvEntityRegistry.DelayAction };
            IExtendedInfoStorage service3 = new CsvExtendedInfoStorage( Paths.SecurityExtendedInfo ) { DelayAction = csvEntityRegistry.DelayAction };
            ISecurityMappingStorage service4 = new CsvSecurityMappingStorage( Path.Combine( Paths.AppDataPath, "Server mapping" ) ) { DelayAction = csvEntityRegistry.DelayAction };
            HydraStorage service5 = new HydraStorage( Path.Combine( Paths.AppDataPath, "Tasks" ) );
            service5.DelayAction = csvEntityRegistry.DelayAction;
            ConfigManager.RegisterService( service1 );
            ConfigManager.RegisterService( service2 );
            ConfigManager.RegisterService( service3 );
            ConfigManager.RegisterService( service5 );
            ConfigManager.RegisterService( "server_mapping", service4 );
            StorageExchangeInfoProvider exchangeInfoProvider = new StorageExchangeInfoProvider( csvEntityRegistry, true );
            ConfigManager.RegisterService<IExchangeInfoProvider>( exchangeInfoProvider );
            ConfigManager.RegisterService( new CandleBuilderProvider( exchangeInfoProvider ) );
            ConfigManager.RegisterService<ISecurityProvider>( new FilterableSecurityProvider( csvEntityRegistry.Securities ) );
            ConfigManager.RegisterService<ISecurityStorage>( csvEntityRegistry.Securities );
            ConfigManager.RegisterService( csvEntityRegistry.PositionStorage );
            ConfigManager.TryRegisterService<IMessageAdapterProvider>( new InMemoryMessageAdapterProvider( Enumerable.Empty<IMessageAdapter>() ) );
            HydraTaskManager service6 = new HydraTaskManager();
            service6.Parent = this;
            ConfigManager.RegisterService( service6 );
            _authorization = new HydraAuthorization( this );
            HydraServer hydraServer = new HydraServer( this, getPermissions );
            hydraServer.Parent = this;
            Server = hydraServer;
            Server.EmulatorSettings.Apply( EmulatorSettings );
        }

        /// <inheritdoc />
        protected override void DisposeManaged()
        {
            Server.Dispose();
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
            LoggingHelper.DoWithLog( new Func<IDictionary<string, Exception>>( ServicesRegistry.NativeIdStorage.Init ) );
            LoggingHelper.DoWithLog( new Func<IDictionary<string, Exception>>( ServicesRegistry.MappingStorage.Init ) );
            LoggingHelper.DoWithLog( new Func<IDictionary<IExtendedInfoStorageItem, Exception>>( ServicesRegistry.ExtendedInfoStorage.Init ) );
            LoggingHelper.DoWithLog( new Func<IDictionary<object, Exception>>( ServicesRegistry.EntityRegistry.Init ) );
            LoggingHelper.DoWithLog( new Func<IDictionary<string, Exception>>( ServerMapping.Init ) );
            new Action( ConfigManager.GetService<HydraStorage>().Init ).DoWithLog();
            IEntityRegistry entityRegistry = ServicesRegistry.EntityRegistry;
            if ( entityRegistry.Securities.GetAllSecurity() == null )
            {
                entityRegistry.Securities.Add( TraderHelper.AllSecurity );
                entityRegistry.WaitSecuritiesFlush();
            }
            HydraTaskManager instance = HydraTaskManager.Instance;
            instance.TaskAdded += new Action<IHydraTask>( OnTaskAdded );
            instance.TaskRemoved += new Action<IHydraTask>( OnTaskRemoved );
            instance.Init( ServicesRegistry.AdapterProvider.PossibleAdapters.Select( a => a.GetType() ), new Func<string, Type>( ConvertConnectorTask ) );
        }

        private Type ConvertConnectorTask( string type )
        {
            try
            {
                type = type.Substring( 0, type.IndexOf( ',' ) );
                type = type.Substring( type.LastIndexOf( '.' ) + 1 ).Remove( "Task", false );
                Type type1 = Type.GetType( "StockSharp." + type + "." + type + "MessageAdapter, StockSharp." + type, false, true );
                if ( type1 == null )
                    return null;
                return typeof( ConnectorHydraTask<> ).Make( type1 );
            }
            catch ( Exception ex )
            {
                ex.LogError( null );
                return null;
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
            if ( Server.State != ChannelStates.Started )
                return;
            Server.TrySendByFix( TryRemoveSubscriptionId( message ) );
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
                subscriptionIdMessage.SubscriptionIds = null;
            }
            return message;
        }

        private void Task_OnStopped( IHydraTask task )
        {
            _taskAllSecurities.Remove( task );
            _taskSecurityCache.Remove( task );
        }

        private void OnTaskAdded( IHydraTask task )
        {
            IMessageChannel msgChannel = task as IMessageChannel;
            if ( msgChannel != null )
                msgChannel.NewOutMessage += new Action<Message>( TaskNewOutMessage );
            task.Stopped += new Action<IHydraTask>( Task_OnStopped );
            task.DataLoaded += ( security, dataType, time, count, messages ) =>
            {
                if ( security == null )
                    security = TraderHelper.AllSecurity;
                if ( msgChannel == null && Server.State == ChannelStates.Started )
                {
                    foreach ( Message message in messages )
                        Server.TrySendByFix( TryRemoveSubscriptionId( message ) );
                }
                HydraTaskSecurity security1 = _taskAllSecurities.SafeAdd( task, _ => task.GetAllSecurity() );
                HydraTaskSecurity security2;
                HydraTaskSecurity.TypeInfo typeInfo1;
                HydraTaskSecurity.TypeInfo typeInfo2;
                if ( dataType == DataType.News )
                {
                    security2 = null;
                    typeInfo1 = null;
                    typeInfo2 = security1 != null ? security1.InfoDict.SafeAdd( dataType ) : ( HydraTaskSecurity.TypeInfo )null;
                    LoadedNews += count;
                }
                else
                {
                    security2 = _taskSecurityCache.SafeAdd( task ).SafeAdd( security, key => task.Securities.FirstOrDefault( s => s.Security == key ) );
                    if ( dataType == DataType.Ticks )
                        LoadedTrades += count;
                    else if ( dataType == DataType.OrderLog )
                        LoadedOrderLog += count;
                    else if ( dataType == DataType.Transactions )
                        LoadedTransactions += count;
                    else if ( dataType == DataType.MarketDepth )
                        LoadedDepths += count;
                    else if ( dataType == DataType.Level1 )
                        LoadedLevel1 += count;
                    else if ( dataType == DataType.PositionChanges )
                    {
                        LoadedLevel1 += count;
                    }
                    else
                    {
                        if ( dataType == DataType.BoardState )
                            return;
                        if ( !dataType.IsCandles )
                            throw new ArgumentOutOfRangeException( nameof( dataType ), dataType, LocalizedStrings.Str1018 );
                        LoadedCandles += count;
                    }
                    typeInfo1 = security2 != null ? security2.InfoDict.SafeAdd( dataType ) : ( HydraTaskSecurity.TypeInfo )null;
                    typeInfo2 = security1 != null ? security1.InfoDict.SafeAdd( dataType ) : ( HydraTaskSecurity.TypeInfo )null;
                }
                if ( typeInfo2 != null )
                {
                    UpdateInfo( typeInfo2 );
                    if ( dataType.IsCandles )
                        UpdateInfo( security1.InfoDict.SafeAdd( dataType ) );
                    task.UpdateSecurity( security1 );
                }
                if ( typeInfo1 == null )
                    return;
                UpdateInfo( typeInfo1 );
                if ( dataType.IsCandles )
                    UpdateInfo( security2.InfoDict.SafeAdd( dataType ) );
                task.UpdateSecurity( security2 );

                void UpdateInfo( HydraTaskSecurity.TypeInfo typeInfo )
                {
                    typeInfo.Count += count;
                    if ( !time.HasValue )
                        return;
                    typeInfo.LastTime = new DateTime?( time.Value.LocalDateTime );
                }
            };
        }

        private void OnTaskRemoved( IHydraTask task )
        {
            task.Stopped -= new Action<IHydraTask>( Task_OnStopped );
            IMessageChannel messageChannel = task as IMessageChannel;
            if ( messageChannel == null )
                return;
            messageChannel.NewOutMessage -= new Action<Message>( TaskNewOutMessage );
        }

        public bool ApplyServerSettings( bool needSave, Func<bool> canStart )
        {
            if ( canStart == null )
                throw new ArgumentNullException( nameof( canStart ) );
            if ( Server.State == ChannelStates.Started )
                Server.Close();
            Server.TransportSettings.Apply( ServerSettings.ServerSettings );
            _authorization.Underlying = AuthProvider[ServerSettings.Authorization];
            if ( ServerSettings.IsFixServer )
            {
                if ( canStart() )
                {
                    Server.Open();
                }
                else
                {
                    ServerSettings.IsFixServer = false;
                    needSave = true;
                }
            }
            return needSave;
        }

        private class HydraAuthorization : IAuthorization
        {
            private IAuthorization _underlying = new DisabledAuthorization();
            private readonly ILogReceiver _logReceiver;

            public HydraAuthorization( ILogReceiver logReceiver )
            {
                ILogReceiver logReceiver1 = logReceiver;
                if ( logReceiver1 == null )
                    throw new ArgumentNullException( nameof( logReceiver ) );
                _logReceiver = logReceiver1;
            }

            public IAuthorization Underlying
            {
                get
                {
                    return _underlying;
                }
                set
                {
                    IAuthorization authorization = value;
                    if ( authorization == null )
                        throw new ArgumentNullException( nameof( value ) );
                    _underlying = authorization;
                }
            }

            string IAuthorization.ValidateCredentials(
              string login,
              SecureString password,
              IPAddress clientAddress )
            {
                string validate = Validate;
                if ( !validate.IsEmpty() )
                {
                    _logReceiver.AddErrorLog( validate );
                    throw new UnauthorizedAccessException( LocalizedStrings.Str2083 );
                }
                return _underlying.ValidateCredentials( login, password, clientAddress );
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
