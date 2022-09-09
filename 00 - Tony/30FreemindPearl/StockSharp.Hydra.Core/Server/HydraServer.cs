// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Core.Server.HydraServer
// Assembly: StockSharp.Hydra.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BF4FBD4E-7629-47D5-B0AC-6D48C0A60551
// Assembly location: T:\00-StockSharp\Data\StockSharp.Hydra.Core.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.Configuration;
using StockSharp.Algo;
using StockSharp.Algo.Candles.Compression;
using StockSharp.Algo.Matching;
using StockSharp.Algo.Storages;
using StockSharp.Algo.Storages.Remote.Messages;
using StockSharp.Algo.Testing;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Server.Core;
using StockSharp.Server.Fix;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StockSharp.Hydra.Core.Server
{
    /// <summary>Hydra server.</summary>
    public class HydraServer : BaseLogReceiver
    {
        private readonly BlockingQueue<Tuple<IMessageListenerSession, Message>> _fixRequests = new BlockingQueue<Tuple<IMessageListenerSession, Message>>();
        private readonly Dictionary<SecurityId, IOrderMatcher> _matchers = new Dictionary<SecurityId, IOrderMatcher>();
        private readonly IdGenerator _orderIdGenerator = ( IdGenerator )new TickIncrementalIdGenerator();
        private readonly IdGenerator _tradeIdGenerator = ( IdGenerator )new TickIncrementalIdGenerator();
        private readonly HashSet<MessageTypes> _unknownTypes = new HashSet<MessageTypes>();
        private readonly StockSharp.Hydra.Core.Hydra _hydra;
        private readonly Func<string, PermissionCredentials> _getPermissions;
        private readonly FixServerChannel _fixServer;
        private readonly OrderBookSnapshotHolder _bookSnapshotsHolder;
        private readonly Level1SnapshotHolder _level1SnapshotsHolder;
        private IMarketEmulator _emulator;
        private readonly IPositionControllerProvider _positionControllerProvider;
        private const string _serverStorageName = "HydraServer";

        private HydraServerSettings ServerSettings
        {
            get
            {
                return this._hydra.ServerSettings;
            }
        }

        private ISecurityProvider SecurityProvider
        {
            get
            {
                return ServicesRegistry.SecurityProvider;
            }
        }

        private IExchangeInfoProvider ExchangeInfoProvider
        {
            get
            {
                return ServicesRegistry.ExchangeInfoProvider;
            }
        }

        private IStorageRegistry StorageRegistry
        {
            get
            {
                return ServicesRegistry.StorageRegistry;
            }
        }

        private CandleBuilderProvider CandleBuilderProvider
        {
            get
            {
                return ConfigManager.GetService<CandleBuilderProvider>();
            }
        }

        private ISecurityMappingStorage ServerMapping
        {
            get
            {
                return this._hydra.ServerMapping;
            }
        }

        public HydraServer( StockSharp.Hydra.Core.Hydra parent, Func<string, PermissionCredentials> getPermissions )
        {
            StockSharp.Hydra.Core.Hydra hydra = parent;
            if ( hydra == null )
                throw new ArgumentNullException( nameof( parent ) );
            this._hydra = hydra;
            Func<string, PermissionCredentials> func = getPermissions;
            if ( func == null )
                throw new ArgumentNullException( nameof( getPermissions ) );
            this._getPermissions = func;
            FixServer fixServer = new FixServer( this._hydra.Authorization, ( ITransactionIdStorage )new InMemoryTransactionIdStorage( ( IdGenerator )new MillisecondIncrementalIdGenerator() ) );
            fixServer.Parent = ( ILogSource )this;
            FixServer server = fixServer;
            this._fixServer = new FixServerChannel( server, ( IMessageChannel )new PassThroughMessageChannel(), ( IMessageChannel )null );
            this._fixServer.StateChanged += new Action( this.FixServerOnStateChanged );
            this._fixServer.NewOutMessage += new Action<IMessageListenerSession, Message>( this.FixServerOnNewOutMessage );
            OrderBookSnapshotHolder bookSnapshotHolder = new OrderBookSnapshotHolder();
            bookSnapshotHolder.Parent = ( ILogSource )server;
            this._bookSnapshotsHolder = bookSnapshotHolder;
            Level1SnapshotHolder level1SnapshotHolder = new Level1SnapshotHolder();
            level1SnapshotHolder.Parent = ( ILogSource )server;
            this._level1SnapshotsHolder = level1SnapshotHolder;
            ConfigManager.RegisterService<IMessageListener>( ( IMessageListener )this._fixServer.Server );
            this._fixRequests.Close();
            this._positionControllerProvider = ( IPositionControllerProvider )new PositionControllerProvider( ( Func<SecurityId, SecurityMessage> )( secId =>
            {
                Security security = this.SecurityProvider.LookupById( secId );
                if ( security == null )
                    return ( SecurityMessage )null;
                return security.ToMessage( new SecurityId?(), 0L, false );
            } ), ( Func<SecurityId, Sides, Decimal> )( ( secId, side ) => Decimal.Zero ) )
            {
                CheckMoney = true,
                CheckShortable = true
            };
            this.Name = "Server";
        }

        /// <summary>
        /// </summary>
        public MarketEmulatorSettings EmulatorSettings
        {
            get
            {
                return this._hydra.EmulatorSettings;
            }
        }

        /// <summary>
        /// </summary>
        public FixServerSettings TransportSettings
        {
            get
            {
                return this._fixServer.Server.Settings;
            }
        }

        /// <summary>
        /// </summary>
        public void Open()
        {
            this._emulator = ( IMarketEmulator )new MarketEmulator( this.SecurityProvider, ( IPortfolioProvider )new CollectionPortfolioProvider( ( IEnumerable<Portfolio> )new Portfolio[1]
            {
        Portfolio.CreateSimulator()
            } ), this.ExchangeInfoProvider, ( IdGenerator )new TickIncrementalIdGenerator() );
            this._emulator.NewOutMessage += new Action<Message>( this.TrySendByFix );
            this._fixServer.Open();
        }

        /// <summary>
        /// </summary>
        public void Close()
        {
            this._fixServer.Close();
            if ( this._emulator == null )
                return;
            this._emulator.NewOutMessage -= new Action<Message>( this.TrySendByFix );
            this._emulator.Dispose();
        }

        /// <summary>
        /// </summary>
        public ChannelStates State
        {
            get
            {
                return this._fixServer.State;
            }
        }

        /// <summary>
        /// </summary>
        public event Action StateChanged;

        /// <summary>
        /// </summary>
        public event Action<CommandTypes> ProcessCommand;

        /// <inheritdoc />
        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            this._fixServer.Close();
            this._fixServer.StateChanged -= new Action( this.FixServerOnStateChanged );
            this._fixServer.NewOutMessage -= new Action<IMessageListenerSession, Message>( this.FixServerOnNewOutMessage );
            this._fixServer.Server.Dispose();
        }

        private IOrderMatcher GetMatcher( SecurityId secId )
        {
            return this._matchers.SafeAdd<SecurityId, IOrderMatcher>( secId, ( Func<SecurityId, IOrderMatcher> )( key =>
            {
                return ( IOrderMatcher )new OrderMatcher( key, this._orderIdGenerator, this._tradeIdGenerator ) { Parent = ( ILogSource )this._fixServer.Server };
            } ) );
        }

        private void LoadAndSend(
          Func<IMarketDataDrive, StorageFormats, DateTimeOffset, DateTimeOffset?, Action<Message>, DateTimeOffset?> loadMessages,
          ISubscriptionMessage subscrMsg,
          int maxDays )
        {
            if ( loadMessages == null )
                throw new ArgumentNullException( nameof( loadMessages ) );
            if ( maxDays == 0 )
                return;
            DateTimeOffset? nullable1 = subscrMsg.From;
            DateTimeOffset? to = subscrMsg.To;
            if ( !nullable1.HasValue && !to.HasValue )
                return;
            if ( !nullable1.HasValue )
                nullable1 = new DateTimeOffset?( to.Value.AddDays( ( double )-maxDays ) );
            foreach ( StorageFormats storageFormats in Ecng.Common.Enumerator.GetValues<StorageFormats>() )
            {
                foreach ( IMarketDataDrive drive in ServicesRegistry.DriveCache.Drives )
                {
                    if ( to.HasValue )
                    {
                        DateTimeOffset? nullable2 = nullable1;
                        DateTimeOffset? nullable3 = to;
                        if ( ( nullable2.HasValue & nullable3.HasValue ? ( nullable2.GetValueOrDefault() >= nullable3.GetValueOrDefault() ? 1 : 0 ) : 0 ) != 0 )
                            return;
                    }
                    DateTimeOffset? nullable4 = loadMessages( drive, storageFormats, nullable1.Value, to, new Action<Message>( this.TrySendByFix ) );
                    if ( nullable4.HasValue )
                    {
                        if ( to.HasValue && nullable4.Value >= to.Value )
                            return;
                        nullable1 = new DateTimeOffset?( nullable4.Value );
                    }
                }
            }
        }

        /// <summary>
        /// </summary>
        public void UpdateSnapshot( Level1ChangeMessage message )
        {
            this._fixRequests.Enqueue( Tuple.Create<IMessageListenerSession, Message>( ( IMessageListenerSession )null, message.Clone() ), false );
        }

        /// <summary>
        /// </summary>
        public void UpdateSnapshot( QuoteChangeMessage message )
        {
            this._fixRequests.Enqueue( Tuple.Create<IMessageListenerSession, Message>( ( IMessageListenerSession )null, message.Clone() ), false );
        }

        private void ProcessFixRequests()
        {
            Tuple<IMessageListenerSession, Message> tuple;
            while ( !this._fixRequests.IsClosed && this._fixRequests.TryDequeue( out tuple, true, true ) )
            {
                IMessageListenerSession session = tuple.Item1;
                Message message1 = tuple.Item2;
                FixServer logs = this._fixServer.Server;
                try
                {
                    switch ( message1.Type )
                    {
                        case ~MessageTypes.OrderStatus:
                            AvailableDataRequestMessage dataRequestMessage = ( AvailableDataRequestMessage )message1;
                            Exception exception = CheckPermissions( UserPermissions.Load );
                            if ( exception != null )
                            {
                                this.TrySendByFix( ( Message )new SubscriptionResponseMessage()
                                {
                                    OriginalTransactionId = dataRequestMessage.TransactionId,
                                    Error = exception
                                } );
                                continue;
                            }
                            StorageFormats? format2 = dataRequestMessage.Format;
                            IEnumerable<StorageFormats> storageFormatses1;
                            if ( format2.HasValue )
                            {
                                StorageFormats[ ] storageFormatsArray = new StorageFormats[1];
                                format2 = dataRequestMessage.Format;
                                storageFormatsArray[0] = format2.Value;
                                storageFormatses1 = ( IEnumerable<StorageFormats> )storageFormatsArray;
                            }
                            else
                                storageFormatses1 = Ecng.Common.Enumerator.GetValues<StorageFormats>();
                            IEnumerable<StorageFormats> storageFormatses2 = storageFormatses1;
                            HashSet<Tuple<StorageFormats, SecurityId, DataType, DateTime>> tupleSet = new HashSet<Tuple<StorageFormats, SecurityId, DataType, DateTime>>();
                            foreach ( IMarketDataDrive drive in ServicesRegistry.DriveCache.Drives )
                            {
                                foreach ( StorageFormats format3 in storageFormatses2 )
                                {
                                    IEnumerable<SecurityId> securityIds;
                                    if ( !( dataRequestMessage.SecurityId == new SecurityId() ) )
                                        securityIds = ( IEnumerable<SecurityId> )new SecurityId[1]
                                        {
                      this.ServerMapping.TryGetAdapterId(nameof (HydraServer), dataRequestMessage.SecurityId) ?? dataRequestMessage.SecurityId
                                        };
                                    else
                                        securityIds = drive.AvailableSecurities;
                                    foreach ( SecurityId securityId in securityIds )
                                    {
                                        IEnumerable<DataType> dataTypes;
                                        if ( !( ( Equatable<DataType> )dataRequestMessage.RequestDataType == ( DataType )null ) )
                                            dataTypes = ( IEnumerable<DataType> )new DataType[1]
                                            {
                        dataRequestMessage.RequestDataType
                                            };
                                        else
                                            dataTypes = drive.GetAvailableDataTypes( securityId, format3 );
                                        foreach ( DataType dataType1 in dataTypes )
                                        {
                                            foreach ( DateTime date in this.StorageRegistry.GetStorage( securityId, dataType1, drive, format3 ).Dates )
                                            {
                                                if ( tupleSet.Add( Tuple.Create<StorageFormats, SecurityId, DataType, DateTime>( format3, securityId, dataType1, date ) ) )
                                                {
                                                    AvailableDataInfoMessage availableDataInfoMessage = new AvailableDataInfoMessage();
                                                    availableDataInfoMessage.OriginalTransactionId = dataRequestMessage.TransactionId;
                                                    availableDataInfoMessage.SecurityId = securityId;
                                                    availableDataInfoMessage.FileDataType = dataType1;
                                                    availableDataInfoMessage.Format = format3;
                                                    availableDataInfoMessage.Date = ( DateTimeOffset )date;
                                                    this.TrySendByFix( ( Message )availableDataInfoMessage );
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            SubscriptionFinishedMessage subscriptionFinishedMessage1 = new SubscriptionFinishedMessage();
                            subscriptionFinishedMessage1.OriginalTransactionId = dataRequestMessage.TransactionId;
                            this.TrySendByFix( ( Message )subscriptionFinishedMessage1 );
                            continue;
                        case ~MessageTypes.PortfolioLookup:
                            RemoteFileCommandMessage message2 = ( RemoteFileCommandMessage )message1;
                            SecurityId secId = message2.SecurityId;
                            DataType dataType = message2.FileDataType;
                            DateTimeOffset dateTimeOffset1 = message2.StartDate;
                            DateTime utcDateTime1 = dateTimeOffset1.UtcDateTime;
                            dateTimeOffset1 = message2.EndDate;
                            DateTime utcDateTime2 = dateTimeOffset1.UtcDateTime;
                            StorageFormats format1 = message2.Format;
                            byte[ ] body = message2.Body;
                            if ( secId != new SecurityId() )
                                secId = this.ServerMapping.TryGetAdapterId( nameof( HydraServer ), secId ) ?? secId;
                            switch ( message2.Command )
                            {
                                case CommandTypes.Update:
                                    Exception error1 = CheckPermissions( UserPermissions.Save );
                                    if ( error1 != null )
                                    {
                                        this.TrySendByFix( ( Message )message2.CreateResponse( error1 ) );
                                        continue;
                                    }
                                    GetStorage().Drive.SaveStream( utcDateTime1, body.To<Stream>() );
                                    this.TrySendByFix( ( Message )message2.CreateResponse( ( Exception )null ) );
                                    continue;
                                case CommandTypes.Add:
                                    Exception error2 = CheckPermissions( UserPermissions.Save );
                                    if ( error2 != null )
                                    {
                                        this.TrySendByFix( ( Message )message2.CreateResponse( error2 ) );
                                        continue;
                                    }
                                    GetStorage().Drive.SaveStream( utcDateTime1, body.To<Stream>() );
                                    this.TrySendByFix( ( Message )message2.CreateResponse( ( Exception )null ) );
                                    continue;
                                case CommandTypes.Remove:
                                    Exception error3 = CheckPermissions( UserPermissions.Save );
                                    if ( error3 != null )
                                    {
                                        this.TrySendByFix( ( Message )message2.CreateResponse( error3 ) );
                                        continue;
                                    }
                                    GetStorage().Delete( new DateTimeOffset?( ( DateTimeOffset )utcDateTime1 ), new DateTimeOffset?( ( DateTimeOffset )utcDateTime2 ) );
                                    this.TrySendByFix( ( Message )message2.CreateResponse( ( Exception )null ) );
                                    continue;
                                case CommandTypes.Get:
                                    Exception error4 = CheckPermissions( UserPermissions.Load );
                                    if ( error4 != null )
                                    {
                                        this.TrySendByFix( ( Message )message2.CreateResponse( error4 ) );
                                        continue;
                                    }
                                    this.TrySendByFix( ( Message )message2.CreateResponse( ( Exception )null ) );
                                    IMarketDataStorage storage = GetStorage();
                                    foreach ( DateTime date in storage.GetDates( new DateTime?( utcDateTime1 ), new DateTime?( utcDateTime2 ) ) )
                                    {
                                        using ( Stream stream = storage.Drive.LoadStream( date ) )
                                        {
                                            if ( stream != Stream.Null )
                                            {
                                                RemoteFileMessage remoteFileMessage = new RemoteFileMessage();
                                                remoteFileMessage.OriginalTransactionId = message2.TransactionId;
                                                remoteFileMessage.Date = ( DateTimeOffset )date;
                                                remoteFileMessage.FileDataType = dataType;
                                                remoteFileMessage.Format = format1;
                                                remoteFileMessage.SecurityId = secId;
                                                remoteFileMessage.Body = stream.To<byte[ ]>();
                                                this.TrySendByFix( ( Message )remoteFileMessage );
                                            }
                                        }
                                    }
                                    SubscriptionFinishedMessage subscriptionFinishedMessage2 = new SubscriptionFinishedMessage();
                                    subscriptionFinishedMessage2.OriginalTransactionId = message2.TransactionId;
                                    this.TrySendByFix( ( Message )subscriptionFinishedMessage2 );
                                    continue;
                                default:
                                    ArgumentOutOfRangeException ofRangeException1 = new ArgumentOutOfRangeException( "message", ( object )message2.Command, LocalizedStrings.Str1219 );
                                    logs.AddErrorLog( ( Exception )ofRangeException1 );
                                    this.TrySendByFix( ( Message )message2.CreateResponse( ( Exception )ofRangeException1 ) );
                                    continue;
                            }

                            IMarketDataStorage GetStorage()
                            {
                                return this.StorageRegistry.GetStorage( secId, dataType.MessageType, dataType.Arg, ServicesRegistry.DriveCache.DefaultDrive, format1 );
                            }
                        case MessageTypes.Security:
                            SecurityMessage message3 = ( SecurityMessage )message1;
                            Exception error5 = CheckPermissions( UserPermissions.EditSecurities );
                            if ( error5 != null )
                            {
                                this.TrySendByFix( ( Message )error5.ToErrorMessage( message3.OriginalTransactionId ) );
                                continue;
                            }
                            Security security1 = this.SecurityProvider.LookupById( message3.SecurityId );
                            if ( security1 == null )
                                security1 = message3.ToSecurity( this.ExchangeInfoProvider );
                            else
                                security1.ApplyChanges( message3, this.ExchangeInfoProvider, true );
                            ServicesRegistry.SecurityStorage.Save( security1, true );
                            continue;
                        case MessageTypes.Level1Change:
                            Level1ChangeMessage level1ChangeMessage = this._level1SnapshotsHolder.Process( ( Level1ChangeMessage )message1, true );
                            if ( this._emulator != null )
                            {
                                this._emulator.SendInMessage( ( Message )level1ChangeMessage );
                                continue;
                            }
                            this.TrySendByFix( ( Message )level1ChangeMessage );
                            continue;
                        case MessageTypes.OrderRegister:
                        case MessageTypes.OrderReplace:
                        case MessageTypes.OrderPairReplace:
                        case MessageTypes.OrderCancel:
                        case MessageTypes.OrderGroupCancel:
                            OrderMessage message4 = message1 as OrderMessage ?? ( OrderMessage )( ( OrderPairReplaceMessage )message1 ).Message1;
                            Exception error6 = CheckPermissions( UserPermissions.Trading );
                            if ( error6 != null )
                            {
                                this.TrySendByFix( ( Message )message4.CreateReply( error6 ) );
                                continue;
                            }
                            if ( !this.ServerSettings.SimulatorEnabled )
                            {
                                this.TrySendByFix( ( Message )message4.CreateReply( ( Exception )new InvalidOperationException( LocalizedStrings.TradingDisabled ) ) );
                                continue;
                            }
                            SecurityId? adapterId1 = this.ServerMapping.TryGetAdapterId( nameof( HydraServer ), message4.SecurityId );
                            if ( adapterId1.HasValue )
                                message4.SecurityId = adapterId1.Value;
                            OrderPairReplaceMessage pairReplaceMessage = message1 as OrderPairReplaceMessage;
                            if ( pairReplaceMessage != null && adapterId1.HasValue )
                                pairReplaceMessage.Message2.SecurityId = adapterId1.Value;
                            IOrderMatcher matcher = this.GetMatcher( message4.SecurityId );
                            ExecutionMessage executionMessage1;
                            switch ( message1.Type )
                            {
                                case MessageTypes.OrderRegister:
                                    matcher.RegisterOrder( ( OrderRegisterMessage )message1, new Action<Message>( this.TrySendByFix ), new Action<Message>( this.TrySendByFix ) );
                                    continue;
                                case MessageTypes.OrderReplace:
                                    matcher.ReplaceOrder( ( OrderReplaceMessage )message1, new Action<Message>( this.TrySendByFix ), new Action<Message>( this.TrySendByFix ), out executionMessage1 );
                                    continue;
                                case MessageTypes.OrderCancel:
                                    matcher.CancelOrder( ( OrderCancelMessage )message1, new Action<Message>( this.TrySendByFix ), new Action<Message>( this.TrySendByFix ), out executionMessage1 );
                                    continue;
                                case MessageTypes.OrderGroupCancel:
                                    OrderGroupCancelMessage message5 = ( OrderGroupCancelMessage )message1;
                                    message5.PortfolioName = session.SessionId;
                                    matcher.CancelOrders( message5, new Action<Message>( this.TrySendByFix ) );
                                    continue;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        case MessageTypes.QuoteChange:
                            QuoteChangeMessage quoteChangeMessage = this._bookSnapshotsHolder.Process( ( QuoteChangeMessage )message1, true );
                            if ( this._emulator != null )
                            {
                                this._emulator.SendInMessage( ( Message )quoteChangeMessage );
                                continue;
                            }
                            this.TrySendByFix( ( Message )quoteChangeMessage );
                            continue;
                        case MessageTypes.Execution:
                            ExecutionMessage execMsg = ( ExecutionMessage )message1;
                            if ( this._emulator != null )
                            {
                                if ( execMsg.IsMarketData() )
                                {
                                    this._emulator.SendInMessage( ( Message )execMsg );
                                    continue;
                                }
                                continue;
                            }
                            continue;
                        case MessageTypes.MarketData:
                            MarketDataMessage mdMsg = ( MarketDataMessage )message1;
                            Exception error7 = CheckPermissions( UserPermissions.Load );
                            if ( error7 != null )
                            {
                                this.TrySendByFix( ( Message )mdMsg.CreateResponse( error7 ) );
                                continue;
                            }
                            this.TrySendByFix( ( Message )mdMsg.CreateResponse( ( Exception )null ) );
                            if ( mdMsg.IsSubscribe )
                            {
                                HydraServerSettings serverSettings = this.ServerSettings;
                                DataType dataType2 = mdMsg.DataType2;
                                SecurityId securityId = mdMsg.SecurityId;
                                if ( securityId != new SecurityId() )
                                {
                                    SecurityId? adapterId2 = this.ServerMapping.TryGetAdapterId( nameof( HydraServer ), securityId );
                                    if ( adapterId2.HasValue )
                                    {
                                        mdMsg = mdMsg.TypedClone<MarketDataMessage>();
                                        mdMsg.SecurityId = adapterId2.Value;
                                    }
                                    else if ( this.ServerSettings.OnlyMappedSecurities )
                                    {
                                        this.TrySendByFix( ( Message )mdMsg.CreateResponse( ( Exception )new InvalidOperationException( LocalizedStrings.Str704Params.Put( ( object )securityId ) ) ) );
                                        continue;
                                    }
                                }
                                int maxDays = 0;
                                if ( dataType2.IsCandles )
                                    maxDays = serverSettings.CandleHistoryMaxDays;
                                else if ( ( Equatable<DataType> )dataType2 == DataType.Ticks || ( Equatable<DataType> )dataType2 == DataType.Level1 )
                                    maxDays = serverSettings.TickHistoryMaxDays;
                                else if ( ( Equatable<DataType> )dataType2 == DataType.MarketDepth )
                                    maxDays = serverSettings.OrderBookHistoryMaxDays;
                                else if ( ( Equatable<DataType> )dataType2 == DataType.OrderLog )
                                    maxDays = serverSettings.OrderLogHistoryMaxDays;
                                if ( maxDays == 0 && mdMsg.From.HasValue )
                                    logs.AddInfoLog( LocalizedStrings.HistoryDisabled );
                                this.LoadAndSend( ( Func<IMarketDataDrive, StorageFormats, DateTimeOffset, DateTimeOffset?, Action<Message>, DateTimeOffset?> )( ( drive, format, from, to, sendMsg ) => new StorageCoreSettings()
                                {
                                    StorageRegistry = this.StorageRegistry,
                                    Drive = drive,
                                    Format = format,
                                    Mode = StorageModes.Incremental
                                }.LoadMessages( this.CandleBuilderProvider, mdMsg, sendMsg ) ), ( ISubscriptionMessage )mdMsg, maxDays );
                                if ( !mdMsg.To.HasValue )
                                {
                                    if ( ( Equatable<DataType> )mdMsg.DataType2 == DataType.MarketDepth )
                                        SendSnapshot<QuoteChangeMessage>( ( ISnapshotHolder<QuoteChangeMessage> )this._bookSnapshotsHolder );
                                    else if ( ( Equatable<DataType> )mdMsg.DataType2 == DataType.Level1 )
                                        SendSnapshot<Level1ChangeMessage>( ( ISnapshotHolder<Level1ChangeMessage> )this._level1SnapshotsHolder );
                                }
                                this.TrySendByFix( mdMsg.CreateResult() );
                                continue;
                            }
                            continue;

                            void SendSnapshot<TMessage>( ISnapshotHolder<TMessage> holder ) where TMessage : Message, IOriginalTransactionIdMessage
                            {
                                TMessage snapshot = holder.TryGetSnapshot( mdMsg.SecurityId );
                                if ( ( object )snapshot == null )
                                    return;
                                snapshot.OriginalTransactionId = mdMsg.TransactionId;
                                this.TrySendByFix( ( Message )snapshot );
                            }
                        case MessageTypes.SecurityLookup:
                            SecurityLookupMessage securityLookupMessage = ( SecurityLookupMessage )message1;
                            Exception error8 = CheckPermissions( UserPermissions.SecurityLookup );
                            if ( error8 != null )
                            {
                                this.TrySendByFix( ( Message )securityLookupMessage.CreateResponse( error8 ) );
                                continue;
                            }
                            if ( !securityLookupMessage.OnlySecurityId )
                            {
                                int maxSecurityCount = this.ServerSettings.MaxSecurityCount;
                                long? count = securityLookupMessage.Count;
                                if ( !count.HasValue && securityLookupMessage.SecurityIds.Length == 0 )
                                {
                                    securityLookupMessage.Count = new long?( ( long )maxSecurityCount );
                                }
                                else
                                {
                                    count = securityLookupMessage.Count;
                                    long num = ( long )maxSecurityCount;
                                    if ( count.GetValueOrDefault() > num & count.HasValue || securityLookupMessage.SecurityIds.Length > maxSecurityCount )
                                    {
                                        this.TrySendByFix( ( Message )securityLookupMessage.CreateResponse( ( Exception )new InvalidOperationException( LocalizedStrings.MaxAllowedItems.Put( ( object )maxSecurityCount ) ) ) );
                                        continue;
                                    }
                                }
                            }
                            if ( this.ServerSettings.OnlyMappedSecurities )
                            {
                                IEnumerable<SecurityIdMapping> source1 = this.ServerMapping.Get( nameof( HydraServer ) );
                                long? skip = securityLookupMessage.Skip;
                                int valueOrDefault1 = ( skip.HasValue ? new int?( ( int )skip.GetValueOrDefault() ) : new int?() ).GetValueOrDefault();
                                IEnumerable<SecurityIdMapping> source2 = source1.Skip<SecurityIdMapping>( valueOrDefault1 );
                                long? count = securityLookupMessage.Count;
                                int valueOrDefault2 = ( count.HasValue ? new int?( ( int )count.GetValueOrDefault() ) : new int?() ).GetValueOrDefault();
                                foreach ( SecurityIdMapping securityIdMapping in source2.Take<SecurityIdMapping>( valueOrDefault2 ) )
                                {
                                    Security security2 = this.SecurityProvider.LookupById( securityIdMapping.AdapterId );
                                    if ( security2 != null )
                                    {
                                        Security security3 = security2;
                                        long transactionId = securityLookupMessage.TransactionId;
                                        SecurityId? securityId = new SecurityId?();
                                        long originalTransactionId = transactionId;
                                        SecurityMessage message6 = security3.ToMessage( securityId, originalTransactionId, false );
                                        if ( StockSharp.Messages.Extensions.IsMatch( message6, securityLookupMessage ) )
                                            this.TrySendByFix( ( Message )message6 );
                                    }
                                }
                            }
                            else
                            {
                                foreach ( Security security2 in this.SecurityProvider.Lookup( securityLookupMessage ) )
                                {
                                    if ( !security2.IsAllSecurity() )
                                    {
                                        SecurityMessage securityMessage;
                                        if ( !securityLookupMessage.OnlySecurityId )
                                        {
                                            Security security3 = security2;
                                            long transactionId = securityLookupMessage.TransactionId;
                                            SecurityId? securityId = new SecurityId?();
                                            long originalTransactionId = transactionId;
                                            securityMessage = security3.ToMessage( securityId, originalTransactionId, false );
                                        }
                                        else
                                        {
                                            securityMessage = new SecurityMessage();
                                            securityMessage.SecurityId = security2.ToSecurityId( ( SecurityIdGenerator )null, true, false );
                                            securityMessage.OriginalTransactionId = securityLookupMessage.TransactionId;
                                        }
                                        this.TrySendByFix( ( Message )securityMessage );
                                    }
                                }
                            }
                            this.TrySendByFix( securityLookupMessage.CreateResult() );
                            continue;
                        case MessageTypes.PortfolioLookup:
                            PortfolioLookupMessage lookupMsg = ( PortfolioLookupMessage )message1;
                            Exception error9 = CheckPermissions( UserPermissions.Trading );
                            if ( error9 != null )
                            {
                                this.TrySendByFix( ( Message )lookupMsg.CreateResponse( error9 ) );
                                continue;
                            }
                            if ( !lookupMsg.IsSubscribe )
                            {
                                this.TrySendByFix( ( Message )lookupMsg.CreateResponse( ( Exception )null ) );
                                continue;
                            }
                            if ( this.ServerSettings.SimulatorEnabled )
                            {
                                PortfolioMessage portfolioMessage = new PortfolioMessage();
                                portfolioMessage.PortfolioName = session.SessionId;
                                portfolioMessage.OriginalTransactionId = lookupMsg.TransactionId;
                                this.TrySendByFix( ( Message )portfolioMessage );
                                this._positionControllerProvider.GetController( session.SessionId ).RequestState( lookupMsg, new Action<Message>( this.TrySendByFix ) );
                                this.TrySendByFix( lookupMsg.CreateResult() );
                                continue;
                            }
                            SecurityId? securityId1 = lookupMsg.SecurityId;
                            if ( !securityId1.HasValue || securityId1.Value == new SecurityId() )
                            {
                                logs.AddWarningLog( LocalizedStrings.Str3252 );
                            }
                            else
                            {
                                SecurityId? adapterId2 = this.ServerMapping.TryGetAdapterId( nameof( HydraServer ), securityId1.Value );
                                if ( adapterId2.HasValue )
                                {
                                    lookupMsg = lookupMsg.TypedClone<PortfolioLookupMessage>();
                                    lookupMsg.SecurityId = new SecurityId?( adapterId2.Value );
                                }
                                else if ( this.ServerSettings.OnlyMappedSecurities )
                                {
                                    this.TrySendByFix( ( Message )lookupMsg.CreateResponse( ( Exception )new InvalidOperationException( LocalizedStrings.Str704Params.Put( ( object )securityId1 ) ) ) );
                                    continue;
                                }
                                int transactionsHistoryMaxDays = this.ServerSettings.TransactionsHistoryMaxDays;
                                if ( transactionsHistoryMaxDays == 0 && lookupMsg.From.HasValue )
                                    logs.AddInfoLog( LocalizedStrings.HistoryDisabled );
                                this.LoadAndSend( ( Func<IMarketDataDrive, StorageFormats, DateTimeOffset, DateTimeOffset?, Action<Message>, DateTimeOffset?> )( ( drive, format, from, to, sendMsg ) =>
                                {
                                    IMarketDataStorage<PositionChangeMessage> positionMessageStorage = this.StorageRegistry.GetPositionMessageStorage( lookupMsg.SecurityId.Value, drive, format );
                                    DateTimeOffset dateTimeOffset = from;
                                    DateTimeOffset? from1 = new DateTimeOffset?( from );
                                    DateTimeOffset? to1 = to;
                                    foreach ( PositionChangeMessage positionChangeMessage in positionMessageStorage.Load<PositionChangeMessage>( from1, to1 ) )
                                    {
                                        positionChangeMessage.OriginalTransactionId = lookupMsg.TransactionId;
                                        this.TrySendByFix( ( Message )positionChangeMessage );
                                        dateTimeOffset = positionChangeMessage.ServerTime;
                                    }
                                    return new DateTimeOffset?( dateTimeOffset );
                                } ), ( ISubscriptionMessage )lookupMsg, transactionsHistoryMaxDays );
                            }
                            this.TrySendByFix( lookupMsg.CreateResult() );
                            continue;
                        case MessageTypes.OrderStatus:
                            OrderStatusMessage statusMsg = ( OrderStatusMessage )message1;
                            Exception error10 = CheckPermissions( UserPermissions.Trading );
                            if ( error10 != null )
                            {
                                this.TrySendByFix( ( Message )statusMsg.CreateResponse( error10 ) );
                                continue;
                            }
                            if ( !statusMsg.IsSubscribe )
                            {
                                this.TrySendByFix( ( Message )statusMsg.CreateResponse( ( Exception )null ) );
                                continue;
                            }
                            if ( this.ServerSettings.SimulatorEnabled )
                            {
                                statusMsg.PortfolioName = session.SessionId;
                                foreach ( IOrderMatcher orderMatcher in this._matchers.Values )
                                    orderMatcher.RequestOrders( statusMsg, new Action<Message>( this.TrySendByFix ) );
                                this.TrySendByFix( statusMsg.CreateResult() );
                                continue;
                            }
                            SecurityId securityId2 = statusMsg.SecurityId;
                            if ( securityId2 == new SecurityId() )
                            {
                                logs.AddWarningLog( LocalizedStrings.Str3252 );
                            }
                            else
                            {
                                SecurityId? adapterId2 = this.ServerMapping.TryGetAdapterId( nameof( HydraServer ), securityId2 );
                                if ( adapterId2.HasValue )
                                {
                                    statusMsg = statusMsg.TypedClone<OrderStatusMessage>();
                                    statusMsg.SecurityId = adapterId2.Value;
                                }
                                else if ( this.ServerSettings.OnlyMappedSecurities )
                                {
                                    this.TrySendByFix( ( Message )statusMsg.CreateResponse( ( Exception )new InvalidOperationException( LocalizedStrings.Str704Params.Put( ( object )securityId2 ) ) ) );
                                    continue;
                                }
                                int transactionsHistoryMaxDays = this.ServerSettings.TransactionsHistoryMaxDays;
                                if ( transactionsHistoryMaxDays == 0 && statusMsg.From.HasValue )
                                    logs.AddInfoLog( LocalizedStrings.HistoryDisabled );
                                this.LoadAndSend( ( Func<IMarketDataDrive, StorageFormats, DateTimeOffset, DateTimeOffset?, Action<Message>, DateTimeOffset?> )( ( drive, format, from, to, sendMsg ) =>
                                {
                                    IMarketDataStorage<ExecutionMessage> executionMessageStorage = this.StorageRegistry.GetExecutionMessageStorage( statusMsg.SecurityId, ExecutionTypes.Transaction, drive, format );
                                    DateTimeOffset dateTimeOffset = from;
                                    DateTimeOffset? from1 = new DateTimeOffset?( from );
                                    DateTimeOffset? to1 = to;
                                    foreach ( ExecutionMessage executionMessage in executionMessageStorage.Load<ExecutionMessage>( from1, to1 ) )
                                    {
                                        executionMessage.OriginalTransactionId = statusMsg.TransactionId;
                                        this.TrySendByFix( ( Message )executionMessage );
                                        dateTimeOffset = executionMessage.ServerTime;
                                    }
                                    return new DateTimeOffset?( dateTimeOffset );
                                } ), ( ISubscriptionMessage )statusMsg, transactionsHistoryMaxDays );
                            }
                            this.TrySendByFix( statusMsg.CreateResult() );
                            continue;
                        case MessageTypes.Board:
                            BoardMessage message7 = ( BoardMessage )message1;
                            Exception error11 = CheckPermissions( UserPermissions.EditBoards );
                            if ( error11 != null )
                            {
                                this.TrySendByFix( ( Message )error11.ToErrorMessage( message7.OriginalTransactionId ) );
                                continue;
                            }
                            ExchangeBoard exchangeBoard1 = this.ExchangeInfoProvider.GetExchangeBoard( message7.Code );
                            this.ExchangeInfoProvider.Save( ( Equatable<ExchangeBoard> )exchangeBoard1 == ( ExchangeBoard )null ? message7.ToBoard() : exchangeBoard1.ApplyChanges( message7 ) );
                            continue;
                        case MessageTypes.Remove:
                            RemoveMessage removeMessage = ( RemoveMessage )message1;
                            switch ( removeMessage.RemoveType )
                            {
                                case RemoveTypes.Security:
                                    Exception error12 = CheckPermissions( UserPermissions.DeleteSecurities );
                                    if ( error12 != null )
                                    {
                                        this.TrySendByFix( ( Message )error12.ToErrorMessage( removeMessage.TransactionId ) );
                                        continue;
                                    }
                                    Security security4 = this.SecurityProvider.LookupById( removeMessage.RemoveId );
                                    if ( security4 != null )
                                    {
                                        HydraTaskManager.Instance.Delete( security4 );
                                        continue;
                                    }
                                    continue;
                                case RemoveTypes.Portfolio:
                                    continue;
                                case RemoveTypes.Exchange:
                                    Exchange exchange = this.ExchangeInfoProvider.GetExchange( removeMessage.RemoveId );
                                    if ( ( Equatable<Exchange> )exchange != ( Exchange )null )
                                    {
                                        this.ExchangeInfoProvider.Delete( exchange );
                                        continue;
                                    }
                                    continue;
                                case RemoveTypes.Board:
                                    Exception error13 = CheckPermissions( UserPermissions.DeleteBoards );
                                    if ( error13 != null )
                                    {
                                        this.TrySendByFix( ( Message )error13.ToErrorMessage( removeMessage.TransactionId ) );
                                        continue;
                                    }
                                    ExchangeBoard exchangeBoard2 = this.ExchangeInfoProvider.GetExchangeBoard( removeMessage.RemoveId );
                                    if ( ( Equatable<ExchangeBoard> )exchangeBoard2 != ( ExchangeBoard )null )
                                    {
                                        this.ExchangeInfoProvider.Delete( exchangeBoard2 );
                                        continue;
                                    }
                                    continue;
                                case RemoveTypes.User:
                                    Exception error14 = CheckPermissions( UserPermissions.DeleteUsers );
                                    if ( error14 != null )
                                    {
                                        this.TrySendByFix( ( Message )error14.ToErrorMessage( removeMessage.TransactionId ) );
                                        continue;
                                    }
                                    continue;
                                default:
                                    ArgumentOutOfRangeException error15 = new ArgumentOutOfRangeException( "removeMsg", ( object )removeMessage.RemoveType, LocalizedStrings.Str1219 );
                                    logs.AddErrorLog( ( Exception )error15 );
                                    this.TrySendByFix( ( Message )error15.ToErrorMessage( removeMessage.TransactionId ) );
                                    continue;
                            }
                        case MessageTypes.UserInfo:
                            UserInfoMessage userInfoMessage = ( UserInfoMessage )message1;
                            Exception error16 = CheckPermissions( UserPermissions.EditUsers );
                            if ( error16 != null )
                            {
                                this.TrySendByFix( ( Message )error16.ToErrorMessage( userInfoMessage.TransactionId ) );
                                continue;
                            }
                            continue;
                        case MessageTypes.UserLookup:
                            UserLookupMessage message8 = ( UserLookupMessage )message1;
                            Exception error17 = CheckPermissions( UserPermissions.GetUsers );
                            if ( error17 != null )
                            {
                                this.TrySendByFix( ( Message )message8.CreateResponse( error17 ) );
                                continue;
                            }
                            if ( !message8.IsSubscribe )
                            {
                                this.TrySendByFix( ( Message )message8.CreateResponse( ( Exception )null ) );
                                continue;
                            }
                            this.TrySendByFix( message8.CreateResult() );
                            continue;
                        case MessageTypes.BoardLookup:
                            BoardLookupMessage boardLookupMessage = ( BoardLookupMessage )message1;
                            Exception error18 = CheckPermissions( UserPermissions.ExchangeBoardLookup );
                            if ( error18 != null )
                            {
                                this.TrySendByFix( ( Message )boardLookupMessage.CreateResponse( error18 ) );
                                continue;
                            }
                            if ( !boardLookupMessage.IsSubscribe )
                            {
                                this.TrySendByFix( ( Message )boardLookupMessage.CreateResponse( ( Exception )null ) );
                                continue;
                            }
                            foreach ( Message message6 in this.ExchangeInfoProvider.LookupBoards2( boardLookupMessage ) )
                                this.TrySendByFix( message6 );
                            this.TrySendByFix( boardLookupMessage.CreateResult() );
                            continue;
                        case MessageTypes.TimeFrameLookup:
                            TimeFrameLookupMessage message9 = ( TimeFrameLookupMessage )message1;
                            Exception error19 = CheckPermissions( UserPermissions.Load );
                            if ( error19 != null )
                            {
                                this.TrySendByFix( ( Message )message9.CreateResponse( error19 ) );
                                continue;
                            }
                            if ( !message9.IsSubscribe )
                            {
                                this.TrySendByFix( ( Message )message9.CreateResponse( ( Exception )null ) );
                                continue;
                            }
                            TimeSpan[ ] array = HydraTaskManager.Instance.Tasks.SelectMany<IHydraTask, TimeSpan>( ( Func<IHydraTask, IEnumerable<TimeSpan>> )( task => task.SupportedDataTypes.FilterTimeFrames() ) ).Distinct<TimeSpan>().OrderBy<TimeSpan>().ToArray<TimeSpan>();
                            TimeFrameInfoMessage frameInfoMessage = new TimeFrameInfoMessage();
                            frameInfoMessage.OriginalTransactionId = message9.TransactionId;
                            frameInfoMessage.TimeFrames = array;
                            this.TrySendByFix( ( Message )frameInfoMessage );
                            this.TrySendByFix( message9.CreateResult() );
                            continue;
                        case MessageTypes.SecurityMappingRequest:
                            SecurityMappingRequestMessage message10 = ( SecurityMappingRequestMessage )message1;
                            Exception error20 = CheckPermissions( UserPermissions.SecurityLookup );
                            if ( error20 != null )
                            {
                                this.TrySendByFix( ( Message )message10.CreateResponse( error20 ) );
                                continue;
                            }
                            if ( !message10.IsSubscribe )
                            {
                                this.TrySendByFix( ( Message )message10.CreateResponse( ( Exception )null ) );
                                continue;
                            }
                            ISecurityMappingStorage mappingStorage1 = ServicesRegistry.MappingStorage;
                            SecurityMappingInfoMessage mappingInfoMessage = new SecurityMappingInfoMessage();
                            mappingInfoMessage.Mapping = ( IDictionary<string, IEnumerable<SecurityIdMapping>> )mappingStorage1.GetStorageNames().ToDictionary<string, string, IEnumerable<SecurityIdMapping>>( ( Func<string, string> )( n => n ), new Func<string, IEnumerable<SecurityIdMapping>>( mappingStorage1.Get ) );
                            mappingInfoMessage.OriginalTransactionId = message10.TransactionId;
                            this.TrySendByFix( ( Message )mappingInfoMessage );
                            this.TrySendByFix( message10.CreateResult() );
                            continue;
                        case MessageTypes.SecurityLegsRequest:
                            SecurityLegsRequestMessage message11 = ( SecurityLegsRequestMessage )message1;
                            Exception error21 = CheckPermissions( UserPermissions.SecurityLookup );
                            if ( error21 != null )
                            {
                                this.TrySendByFix( ( Message )message11.CreateResponse( error21 ) );
                                continue;
                            }
                            if ( !message11.IsSubscribe )
                            {
                                this.TrySendByFix( ( Message )message11.CreateResponse( ( Exception )null ) );
                                continue;
                            }
                            this.TrySendByFix( message11.CreateResult() );
                            continue;
                        case MessageTypes.Command:
                            CommandMessage message12 = ( CommandMessage )message1;
                            Exception error22 = CheckPermissions( UserPermissions.ServerManage );
                            if ( error22 != null )
                            {
                                this.TrySendByFix( ( Message )error22.ToErrorMessage( message12.TransactionId ) );
                                continue;
                            }
                            switch ( message12.Command )
                            {
                                case CommandTypes.Start:
                                case CommandTypes.Stop:
                                case CommandTypes.Restart:
                                    Action<CommandTypes> processCommand = this.ProcessCommand;
                                    if ( processCommand != null )
                                        processCommand( message12.Command );
                                    this.TrySendByFix( ( Message )message12.CreateResponse( ( Exception )null ) );
                                    continue;
                                default:
                                    ArgumentOutOfRangeException ofRangeException2 = new ArgumentOutOfRangeException( "message", ( object )message12.Command, LocalizedStrings.Str1219 );
                                    logs.AddErrorLog( ( Exception )ofRangeException2 );
                                    this.TrySendByFix( ( Message )message12.CreateResponse( ( Exception )ofRangeException2 ) );
                                    continue;
                            }
                        case MessageTypes.SecurityRouteListRequest:
                            SecurityRouteListRequestMessage message13 = ( SecurityRouteListRequestMessage )message1;
                            Exception error23 = CheckPermissions( UserPermissions.SecurityLookup );
                            if ( error23 != null )
                            {
                                this.TrySendByFix( ( Message )message13.CreateResponse( error23 ) );
                                continue;
                            }
                            if ( !message13.IsSubscribe )
                            {
                                this.TrySendByFix( ( Message )message13.CreateResponse( ( Exception )null ) );
                                continue;
                            }
                            foreach ( KeyValuePair<Tuple<SecurityId, DataType>, Guid> adapter in ServicesRegistry.SecurityAdapterProvider.Adapters )
                            {
                                SecurityRouteMessage securityRouteMessage = new SecurityRouteMessage();
                                securityRouteMessage.SecurityId = adapter.Key.Item1;
                                securityRouteMessage.SecurityDataType = adapter.Key.Item2;
                                securityRouteMessage.AdapterId = adapter.Value;
                                securityRouteMessage.OriginalTransactionId = message13.TransactionId;
                                this.TrySendByFix( ( Message )securityRouteMessage );
                            }
                            this.TrySendByFix( message13.CreateResult() );
                            continue;
                        case MessageTypes.PortfolioRouteListRequest:
                            PortfolioRouteListRequestMessage message14 = ( PortfolioRouteListRequestMessage )message1;
                            if ( !message14.IsSubscribe )
                            {
                                this.TrySendByFix( ( Message )message14.CreateResponse( ( Exception )null ) );
                                continue;
                            }
                            foreach ( KeyValuePair<string, Guid> adapter in ServicesRegistry.PortfolioAdapterProvider.Adapters )
                            {
                                PortfolioRouteMessage portfolioRouteMessage = new PortfolioRouteMessage();
                                portfolioRouteMessage.PortfolioName = adapter.Key;
                                portfolioRouteMessage.AdapterId = adapter.Value;
                                portfolioRouteMessage.OriginalTransactionId = message14.TransactionId;
                                this.TrySendByFix( ( Message )portfolioRouteMessage );
                            }
                            this.TrySendByFix( message14.CreateResult() );
                            continue;
                        case MessageTypes.SecurityMapping:
                            SecurityMappingMessage message15 = ( SecurityMappingMessage )message1;
                            Exception error24 = CheckPermissions( UserPermissions.EditSecurities );
                            if ( error24 != null )
                            {
                                this.TrySendByFix( ( Message )message15.CreateResponse( error24 ) );
                                continue;
                            }
                            ISecurityMappingStorage mappingStorage2 = ServicesRegistry.MappingStorage;
                            if ( message15.IsDelete )
                                mappingStorage2.Remove( message15.StorageName, message15.Mapping.StockSharpId );
                            else
                                mappingStorage2.Save( message15.StorageName, message15.Mapping );
                            this.TrySendByFix( message15.CreateResult() );
                            continue;
                        default:
                            continue;
                    }
                }
                catch ( Exception ex1 )
                {
                    ex1.LogError( ( string )null );
                    try
                    {
                        ITransactionIdMessage transactionIdMessage = message1 as ITransactionIdMessage;
                        if ( transactionIdMessage != null )
                            this.TrySendByFix( ( Message )ex1.ToErrorMessage( transactionIdMessage.TransactionId ) );
                    }
                    catch ( Exception ex2 )
                    {
                        ex2.LogError( ( string )null );
                    }
                }

                Exception CheckPermissions( UserPermissions permission )
                {
                    if ( this.ServerSettings.Authorization == AuthorizationModes.Anonymous )
                    {
                        if ( permission == UserPermissions.Load || permission == UserPermissions.SecurityLookup || permission == UserPermissions.ExchangeBoardLookup )
                            return ( Exception )null;
                        UnauthorizedAccessException unauthorizedAccessException = new UnauthorizedAccessException( LocalizedStrings.AnonymousCannotAction.Put( ( object )session.Id, ( object )permission ) );
                        logs.AddErrorLog( ( Exception )unauthorizedAccessException );
                        return ( Exception )unauthorizedAccessException;
                    }
                    PermissionCredentials permissionCredentials = this._getPermissions( session.SessionId );
                    if ( permissionCredentials == null )
                    {
                        UnauthorizedAccessException unauthorizedAccessException = new UnauthorizedAccessException( LocalizedStrings.SessionNoPermissionStorage.Put( ( object )session.Id ) );
                        logs.AddErrorLog( ( Exception )unauthorizedAccessException );
                        return ( Exception )unauthorizedAccessException;
                    }
                    
                    IDictionary<(string, string, string, DateTime?), bool>  temp = new SynchronizedDictionary< (string, string, string, DateTime?), bool> ();
                    
                    if ( permissionCredentials.Permissions.TryGetValue( permission, out temp ) )
                        return ( Exception )null;
                    UnauthorizedAccessException unauthorizedAccessException1 = new UnauthorizedAccessException( LocalizedStrings.SessionNoPermission.Put( ( object )session.Id, ( object )permission ) );
                    logs.AddErrorLog( ( Exception )unauthorizedAccessException1 );
                    return ( Exception )unauthorizedAccessException1;
                }
            }
        }

        private void FixServerOnNewOutMessage( IMessageListenerSession session, Message message )
        {
            switch ( message.Type )
            {
                case ~MessageTypes.OrderStatus:
                case ~MessageTypes.PortfolioLookup:
                case MessageTypes.Security:
                case MessageTypes.OrderRegister:
                case MessageTypes.OrderReplace:
                case MessageTypes.OrderPairReplace:
                case MessageTypes.OrderCancel:
                case MessageTypes.OrderGroupCancel:
                case MessageTypes.MarketData:
                case MessageTypes.SecurityLookup:
                case MessageTypes.PortfolioLookup:
                case MessageTypes.OrderStatus:
                case MessageTypes.Board:
                case MessageTypes.Remove:
                case MessageTypes.UserInfo:
                case MessageTypes.UserLookup:
                case MessageTypes.BoardLookup:
                case MessageTypes.TimeFrameLookup:
                case MessageTypes.SecurityMappingRequest:
                case MessageTypes.SecurityLegsRequest:
                case MessageTypes.Command:
                case MessageTypes.SecurityRouteListRequest:
                case MessageTypes.PortfolioRouteListRequest:
                case MessageTypes.SecurityMapping:
                    this._fixRequests.Enqueue( Tuple.Create<IMessageListenerSession, Message>( session, message.Clone() ), false );
                    break;
            }
        }

        /// <summary>
        /// </summary>
        public void TrySendByFix( Message message )
        {
            if ( message == null )
                throw new ArgumentNullException( nameof( message ) );
            switch ( message.Type )
            {
                case ~MessageTypes.Board:
                case ~MessageTypes.SecurityLookup:
                case MessageTypes.Security:
                case MessageTypes.Level1Change:
                case MessageTypes.News:
                case MessageTypes.Portfolio:
                case MessageTypes.CandleTimeFrame:
                case MessageTypes.QuoteChange:
                case MessageTypes.Execution:
                case MessageTypes.PositionChange:
                case MessageTypes.Error:
                case MessageTypes.BoardState:
                case MessageTypes.Board:
                case MessageTypes.CandleTick:
                case MessageTypes.CandleVolume:
                case MessageTypes.CandleRange:
                case MessageTypes.CandlePnF:
                case MessageTypes.CandleRenko:
                case MessageTypes.SubscriptionFinished:
                case MessageTypes.UserInfo:
                case MessageTypes.TimeFrameInfo:
                case MessageTypes.SecurityMappingInfo:
                case MessageTypes.SecurityLegsInfo:
                case MessageTypes.Command:
                case MessageTypes.AdapterResponse:
                case MessageTypes.SecurityRoute:
                case MessageTypes.PortfolioRoute:
                case MessageTypes.SecurityMapping:
                case MessageTypes.SubscriptionOnline:
                case MessageTypes.SubscriptionResponse:
                case MessageTypes.CandleHeikinAshi:
                    try
                    {
                        ISecurityIdMessage securityIdMessage = message as ISecurityIdMessage;
                        if ( securityIdMessage != null && securityIdMessage.SecurityId != new SecurityId() )
                        {
                            SecurityId? stockSharpId = ( SecurityId? )this.ServerMapping?.TryGetStockSharpId( nameof( HydraServer ), securityIdMessage.SecurityId );
                            if ( stockSharpId.HasValue )
                                securityIdMessage.SecurityId = stockSharpId.Value;
                            else if ( this.ServerSettings.OnlyMappedSecurities )
                                break;
                        }
                        this._fixServer.SendInMessage( message );
                        break;
                    }
                    catch ( Exception ex )
                    {
                        this.AddErrorLog( ex );
                        break;
                    }
                default:
                    if ( !this._unknownTypes.Add( message.Type ) )
                        break;
                    this._fixServer.Server.AddErrorLog( LocalizedStrings.Str3312Params, ( object )message.Type );
                    break;
            }
        }

        private void FixServerOnStateChanged()
        {
            switch ( this._fixServer.State )
            {
                case ChannelStates.Stopped:
                    this._fixRequests.Close();
                    break;
                case ChannelStates.Started:
                    this._fixRequests.Open();
                    new Action( this.ProcessFixRequests ).Thread().Launch();
                    break;
            }
            Action stateChanged = this.StateChanged;
            if ( stateChanged == null )
                return;
            stateChanged();
        }
    }
}
