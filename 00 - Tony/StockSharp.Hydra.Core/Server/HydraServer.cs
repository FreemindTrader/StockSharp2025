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
        private readonly IdGenerator _orderIdGenerator = new TickIncrementalIdGenerator();
        private readonly IdGenerator _tradeIdGenerator = new TickIncrementalIdGenerator();
        private readonly HashSet<MessageTypes> _unknownTypes = new HashSet<MessageTypes>();
        private readonly Hydra _hydra;
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
                return _hydra.ServerSettings;
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
                return _hydra.ServerMapping;
            }
        }

        public HydraServer( Hydra parent, Func<string, PermissionCredentials> getPermissions )
        {
            Hydra hydra = parent;
            if ( hydra == null )
                throw new ArgumentNullException( nameof( parent ) );
            _hydra = hydra;
            Func<string, PermissionCredentials> func = getPermissions;
            if ( func == null )
                throw new ArgumentNullException( nameof( getPermissions ) );
            _getPermissions = func;
            FixServer fixServer = new FixServer( _hydra.Authorization, new InMemoryTransactionIdStorage( new MillisecondIncrementalIdGenerator() ) );
            fixServer.Parent = this;
            FixServer server = fixServer;
            _fixServer = new FixServerChannel( server, new PassThroughMessageChannel(), null );
            _fixServer.StateChanged += new Action( FixServerOnStateChanged );
            _fixServer.NewOutMessage += new Action<IMessageListenerSession, Message>( FixServerOnNewOutMessage );
            OrderBookSnapshotHolder bookSnapshotHolder = new OrderBookSnapshotHolder();
            bookSnapshotHolder.Parent = server;
            _bookSnapshotsHolder = bookSnapshotHolder;
            Level1SnapshotHolder level1SnapshotHolder = new Level1SnapshotHolder();
            level1SnapshotHolder.Parent = server;
            _level1SnapshotsHolder = level1SnapshotHolder;
            ConfigManager.RegisterService<IMessageListener>( _fixServer.Server );
            _fixRequests.Close();
            _positionControllerProvider = new PositionControllerProvider( secId =>
            {
                Security security = SecurityProvider.LookupById( secId );
                if ( security == null )
                    return null;
                return security.ToMessage( new SecurityId?(), 0L, false );
            }, ( secId, side ) => Decimal.Zero )
            {
                CheckMoney = true,
                CheckShortable = true
            };
            Name = "Server";
        }

        /// <summary>
        /// </summary>
        public MarketEmulatorSettings EmulatorSettings
        {
            get
            {
                return _hydra.EmulatorSettings;
            }
        }

        /// <summary>
        /// </summary>
        public FixServerSettings TransportSettings
        {
            get
            {
                return _fixServer.Server.Settings;
            }
        }

        /// <summary>
        /// </summary>
        public void Open()
        {
            _emulator = new MarketEmulator( SecurityProvider, new CollectionPortfolioProvider( new Portfolio[1]
            {
        Portfolio.CreateSimulator()
            } ), ExchangeInfoProvider, new TickIncrementalIdGenerator() );
            _emulator.NewOutMessage += new Action<Message>( TrySendByFix );
            _fixServer.Open();
        }

        /// <summary>
        /// </summary>
        public void Close()
        {
            _fixServer.Close();
            if ( _emulator == null )
                return;
            _emulator.NewOutMessage -= new Action<Message>( TrySendByFix );
            _emulator.Dispose();
        }

        /// <summary>
        /// </summary>
        public ChannelStates State
        {
            get
            {
                return _fixServer.State;
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
            _fixServer.Close();
            _fixServer.StateChanged -= new Action( FixServerOnStateChanged );
            _fixServer.NewOutMessage -= new Action<IMessageListenerSession, Message>( FixServerOnNewOutMessage );
            _fixServer.Server.Dispose();
        }

        private IOrderMatcher GetMatcher( SecurityId secId )
        {
            return _matchers.SafeAdd( secId, key =>
            {
                return new OrderMatcher( key, _orderIdGenerator, _tradeIdGenerator ) { Parent = _fixServer.Server };
            } );
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
                nullable1 = new DateTimeOffset?( to.Value.AddDays( -maxDays ) );
            foreach ( StorageFormats storageFormats in Enumerator.GetValues<StorageFormats>() )
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
                    DateTimeOffset? nullable4 = loadMessages( drive, storageFormats, nullable1.Value, to, new Action<Message>( TrySendByFix ) );
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
            _fixRequests.Enqueue( Tuple.Create<IMessageListenerSession, Message>( null, message.Clone() ), false );
        }

        /// <summary>
        /// </summary>
        public void UpdateSnapshot( QuoteChangeMessage message )
        {
            _fixRequests.Enqueue( Tuple.Create<IMessageListenerSession, Message>( null, message.Clone() ), false );
        }

        private void ProcessFixRequests()
        {
            Tuple<IMessageListenerSession, Message> tuple;
            while ( !_fixRequests.IsClosed && _fixRequests.TryDequeue( out tuple, true, true ) )
            {
                IMessageListenerSession session = tuple.Item1;
                Message message1 = tuple.Item2;
                FixServer logs = _fixServer.Server;
                try
                {
                    switch ( message1.Type )
                    {
                        case ~MessageTypes.OrderStatus:
                            AvailableDataRequestMessage dataRequestMessage = ( AvailableDataRequestMessage )message1;
                            Exception exception = CheckPermissions( UserPermissions.Load );
                            if ( exception != null )
                            {
                                TrySendByFix( new SubscriptionResponseMessage()
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
                                storageFormatses1 = storageFormatsArray;
                            }
                            else
                                storageFormatses1 = Enumerator.GetValues<StorageFormats>();
                            IEnumerable<StorageFormats> storageFormatses2 = storageFormatses1;
                            HashSet<Tuple<StorageFormats, SecurityId, DataType, DateTime>> tupleSet = new HashSet<Tuple<StorageFormats, SecurityId, DataType, DateTime>>();
                            foreach ( IMarketDataDrive drive in ServicesRegistry.DriveCache.Drives )
                            {
                                foreach ( StorageFormats format3 in storageFormatses2 )
                                {
                                    IEnumerable<SecurityId> securityIds;
                                    if ( !( dataRequestMessage.SecurityId == new SecurityId() ) )
                                        securityIds = ( new SecurityId[1]
                                        {
                      ServerMapping.TryGetAdapterId(nameof (HydraServer), dataRequestMessage.SecurityId) ?? dataRequestMessage.SecurityId
                                        } );
                                    else
                                        securityIds = drive.AvailableSecurities;
                                    foreach ( SecurityId securityId in securityIds )
                                    {
                                        IEnumerable<DataType> dataTypes;
                                        if ( !( dataRequestMessage.RequestDataType == null ) )
                                            dataTypes = ( new DataType[1]
                                            {
                        dataRequestMessage.RequestDataType
                                            } );
                                        else
                                            dataTypes = drive.GetAvailableDataTypes( securityId, format3 );
                                        foreach ( DataType dataType1 in dataTypes )
                                        {
                                            foreach ( DateTime date in StorageRegistry.GetStorage( securityId, dataType1, drive, format3 ).Dates )
                                            {
                                                if ( tupleSet.Add( Tuple.Create( format3, securityId, dataType1, date ) ) )
                                                {
                                                    AvailableDataInfoMessage availableDataInfoMessage = new AvailableDataInfoMessage();
                                                    availableDataInfoMessage.OriginalTransactionId = dataRequestMessage.TransactionId;
                                                    availableDataInfoMessage.SecurityId = securityId;
                                                    availableDataInfoMessage.FileDataType = dataType1;
                                                    availableDataInfoMessage.Format = format3;
                                                    availableDataInfoMessage.Date = ( DateTimeOffset )date;
                                                    TrySendByFix( availableDataInfoMessage );
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            SubscriptionFinishedMessage subscriptionFinishedMessage1 = new SubscriptionFinishedMessage();
                            subscriptionFinishedMessage1.OriginalTransactionId = dataRequestMessage.TransactionId;
                            TrySendByFix( subscriptionFinishedMessage1 );
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
                                secId = ServerMapping.TryGetAdapterId( nameof( HydraServer ), secId ) ?? secId;
                            switch ( message2.Command )
                            {
                                case CommandTypes.Update:
                                    Exception error1 = CheckPermissions( UserPermissions.Save );
                                    if ( error1 != null )
                                    {
                                        TrySendByFix( message2.CreateResponse( error1 ) );
                                        continue;
                                    }
                                    GetStorage().Drive.SaveStream( utcDateTime1, body.To<Stream>() );
                                    TrySendByFix( message2.CreateResponse( null ) );
                                    continue;
                                case CommandTypes.Add:
                                    Exception error2 = CheckPermissions( UserPermissions.Save );
                                    if ( error2 != null )
                                    {
                                        TrySendByFix( message2.CreateResponse( error2 ) );
                                        continue;
                                    }
                                    GetStorage().Drive.SaveStream( utcDateTime1, body.To<Stream>() );
                                    TrySendByFix( message2.CreateResponse( null ) );
                                    continue;
                                case CommandTypes.Remove:
                                    Exception error3 = CheckPermissions( UserPermissions.Save );
                                    if ( error3 != null )
                                    {
                                        TrySendByFix( message2.CreateResponse( error3 ) );
                                        continue;
                                    }
                                    GetStorage().Delete( new DateTimeOffset?( ( DateTimeOffset )utcDateTime1 ), new DateTimeOffset?( ( DateTimeOffset )utcDateTime2 ) );
                                    TrySendByFix( message2.CreateResponse( null ) );
                                    continue;
                                case CommandTypes.Get:
                                    Exception error4 = CheckPermissions( UserPermissions.Load );
                                    if ( error4 != null )
                                    {
                                        TrySendByFix( message2.CreateResponse( error4 ) );
                                        continue;
                                    }
                                    TrySendByFix( message2.CreateResponse( null ) );
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
                                                TrySendByFix( remoteFileMessage );
                                            }
                                        }
                                    }
                                    SubscriptionFinishedMessage subscriptionFinishedMessage2 = new SubscriptionFinishedMessage();
                                    subscriptionFinishedMessage2.OriginalTransactionId = message2.TransactionId;
                                    TrySendByFix( subscriptionFinishedMessage2 );
                                    continue;
                                default:
                                    ArgumentOutOfRangeException ofRangeException1 = new ArgumentOutOfRangeException( "message", message2.Command, LocalizedStrings.Str1219 );
                                    logs.AddErrorLog( ofRangeException1 );
                                    TrySendByFix( message2.CreateResponse( ofRangeException1 ) );
                                    continue;
                            }

                            IMarketDataStorage GetStorage()
                            {
                                return StorageRegistry.GetStorage( secId, dataType.MessageType, dataType.Arg, ServicesRegistry.DriveCache.DefaultDrive, format1 );
                            }
                        case MessageTypes.Security:
                            SecurityMessage message3 = ( SecurityMessage )message1;
                            Exception error5 = CheckPermissions( UserPermissions.EditSecurities );
                            if ( error5 != null )
                            {
                                TrySendByFix( error5.ToErrorMessage( message3.OriginalTransactionId ) );
                                continue;
                            }
                            Security security1 = SecurityProvider.LookupById( message3.SecurityId );
                            if ( security1 == null )
                                security1 = message3.ToSecurity( ExchangeInfoProvider );
                            else
                                security1.ApplyChanges( message3, ExchangeInfoProvider, true );
                            ServicesRegistry.SecurityStorage.Save( security1, true );
                            continue;
                        case MessageTypes.Level1Change:
                            Level1ChangeMessage level1ChangeMessage = _level1SnapshotsHolder.Process( ( Level1ChangeMessage )message1, true );
                            if ( _emulator != null )
                            {
                                _emulator.SendInMessage( level1ChangeMessage );
                                continue;
                            }
                            TrySendByFix( level1ChangeMessage );
                            continue;
                        case MessageTypes.OrderRegister:
                        case MessageTypes.OrderReplace:
                        case MessageTypes.OrderPairReplace:
                        case MessageTypes.OrderCancel:
                        case MessageTypes.OrderGroupCancel:
                            OrderMessage message4 = message1 as OrderMessage ?? ( ( OrderPairReplaceMessage )message1 ).Message1;
                            Exception error6 = CheckPermissions( UserPermissions.Trading );
                            if ( error6 != null )
                            {
                                TrySendByFix( message4.CreateReply( error6 ) );
                                continue;
                            }
                            if ( !ServerSettings.SimulatorEnabled )
                            {
                                TrySendByFix( message4.CreateReply( new InvalidOperationException( LocalizedStrings.TradingDisabled ) ) );
                                continue;
                            }
                            SecurityId? adapterId1 = ServerMapping.TryGetAdapterId( nameof( HydraServer ), message4.SecurityId );
                            if ( adapterId1.HasValue )
                                message4.SecurityId = adapterId1.Value;
                            OrderPairReplaceMessage pairReplaceMessage = message1 as OrderPairReplaceMessage;
                            if ( pairReplaceMessage != null && adapterId1.HasValue )
                                pairReplaceMessage.Message2.SecurityId = adapterId1.Value;
                            IOrderMatcher matcher = GetMatcher( message4.SecurityId );
                            ExecutionMessage executionMessage1;
                            switch ( message1.Type )
                            {
                                case MessageTypes.OrderRegister:
                                    matcher.RegisterOrder( ( OrderRegisterMessage )message1, new Action<Message>( TrySendByFix ), new Action<Message>( TrySendByFix ) );
                                    continue;
                                case MessageTypes.OrderReplace:
                                    matcher.ReplaceOrder( ( OrderReplaceMessage )message1, new Action<Message>( TrySendByFix ), new Action<Message>( TrySendByFix ), out executionMessage1 );
                                    continue;
                                case MessageTypes.OrderCancel:
                                    matcher.CancelOrder( ( OrderCancelMessage )message1, new Action<Message>( TrySendByFix ), new Action<Message>( TrySendByFix ), out executionMessage1 );
                                    continue;
                                case MessageTypes.OrderGroupCancel:
                                    OrderGroupCancelMessage message5 = ( OrderGroupCancelMessage )message1;
                                    message5.PortfolioName = session.SessionId;
                                    matcher.CancelOrders( message5, new Action<Message>( TrySendByFix ) );
                                    continue;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        case MessageTypes.QuoteChange:
                            QuoteChangeMessage quoteChangeMessage = _bookSnapshotsHolder.Process( ( QuoteChangeMessage )message1, true );
                            if ( _emulator != null )
                            {
                                _emulator.SendInMessage( quoteChangeMessage );
                                continue;
                            }
                            TrySendByFix( quoteChangeMessage );
                            continue;
                        case MessageTypes.Execution:
                            ExecutionMessage execMsg = ( ExecutionMessage )message1;
                            if ( _emulator != null )
                            {
                                if ( execMsg.IsMarketData() )
                                {
                                    _emulator.SendInMessage( execMsg );
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
                                TrySendByFix( mdMsg.CreateResponse( error7 ) );
                                continue;
                            }
                            TrySendByFix( mdMsg.CreateResponse( null ) );
                            if ( mdMsg.IsSubscribe )
                            {
                                HydraServerSettings serverSettings = ServerSettings;
                                DataType dataType2 = mdMsg.DataType2;
                                SecurityId securityId = mdMsg.SecurityId;
                                if ( securityId != new SecurityId() )
                                {
                                    SecurityId? adapterId2 = ServerMapping.TryGetAdapterId( nameof( HydraServer ), securityId );
                                    if ( adapterId2.HasValue )
                                    {
                                        mdMsg = mdMsg.TypedClone();
                                        mdMsg.SecurityId = adapterId2.Value;
                                    }
                                    else if ( ServerSettings.OnlyMappedSecurities )
                                    {
                                        TrySendByFix( mdMsg.CreateResponse( new InvalidOperationException( LocalizedStrings.Str704Params.Put( securityId ) ) ) );
                                        continue;
                                    }
                                }
                                int maxDays = 0;
                                if ( dataType2.IsCandles )
                                    maxDays = serverSettings.CandleHistoryMaxDays;
                                else if ( dataType2 == DataType.Ticks || dataType2 == DataType.Level1 )
                                    maxDays = serverSettings.TickHistoryMaxDays;
                                else if ( dataType2 == DataType.MarketDepth )
                                    maxDays = serverSettings.OrderBookHistoryMaxDays;
                                else if ( dataType2 == DataType.OrderLog )
                                    maxDays = serverSettings.OrderLogHistoryMaxDays;
                                if ( maxDays == 0 && mdMsg.From.HasValue )
                                    logs.AddInfoLog( LocalizedStrings.HistoryDisabled );
                                LoadAndSend( ( drive, format, from, to, sendMsg ) => new StorageCoreSettings()
                                {
                                    StorageRegistry = StorageRegistry,
                                    Drive = drive,
                                    Format = format,
                                    Mode = StorageModes.Incremental
                                }.LoadMessages( CandleBuilderProvider, mdMsg, sendMsg ), mdMsg, maxDays );
                                if ( !mdMsg.To.HasValue )
                                {
                                    if ( mdMsg.DataType2 == DataType.MarketDepth )
                                        SendSnapshot( _bookSnapshotsHolder );
                                    else if ( mdMsg.DataType2 == DataType.Level1 )
                                        SendSnapshot( _level1SnapshotsHolder );
                                }
                                TrySendByFix( mdMsg.CreateResult() );
                                continue;
                            }
                            continue;

                            void SendSnapshot<TMessage>( ISnapshotHolder<TMessage> holder ) where TMessage : Message, IOriginalTransactionIdMessage
                            {
                                TMessage snapshot = holder.TryGetSnapshot( mdMsg.SecurityId );
                                if ( snapshot == null )
                                    return;
                                snapshot.OriginalTransactionId = mdMsg.TransactionId;
                                TrySendByFix( snapshot );
                            }
                        case MessageTypes.SecurityLookup:
                            SecurityLookupMessage securityLookupMessage = ( SecurityLookupMessage )message1;
                            Exception error8 = CheckPermissions( UserPermissions.SecurityLookup );
                            if ( error8 != null )
                            {
                                TrySendByFix( securityLookupMessage.CreateResponse( error8 ) );
                                continue;
                            }
                            if ( !securityLookupMessage.OnlySecurityId )
                            {
                                int maxSecurityCount = ServerSettings.MaxSecurityCount;
                                long? count = securityLookupMessage.Count;
                                if ( !count.HasValue && securityLookupMessage.SecurityIds.Length == 0 )
                                {
                                    securityLookupMessage.Count = new long?( maxSecurityCount );
                                }
                                else
                                {
                                    count = securityLookupMessage.Count;
                                    long num = maxSecurityCount;
                                    if ( count.GetValueOrDefault() > num & count.HasValue || securityLookupMessage.SecurityIds.Length > maxSecurityCount )
                                    {
                                        TrySendByFix( securityLookupMessage.CreateResponse( new InvalidOperationException( LocalizedStrings.MaxAllowedItems.Put( maxSecurityCount ) ) ) );
                                        continue;
                                    }
                                }
                            }
                            if ( ServerSettings.OnlyMappedSecurities )
                            {
                                IEnumerable<SecurityIdMapping> source1 = ServerMapping.Get( nameof( HydraServer ) );
                                long? skip = securityLookupMessage.Skip;
                                int valueOrDefault1 = ( skip.HasValue ? new int?( ( int )skip.GetValueOrDefault() ) : new int?() ).GetValueOrDefault();
                                IEnumerable<SecurityIdMapping> source2 = source1.Skip( valueOrDefault1 );
                                long? count = securityLookupMessage.Count;
                                int valueOrDefault2 = ( count.HasValue ? new int?( ( int )count.GetValueOrDefault() ) : new int?() ).GetValueOrDefault();
                                foreach ( SecurityIdMapping securityIdMapping in source2.Take( valueOrDefault2 ) )
                                {
                                    Security security2 = SecurityProvider.LookupById( securityIdMapping.AdapterId );
                                    if ( security2 != null )
                                    {
                                        Security security3 = security2;
                                        long transactionId = securityLookupMessage.TransactionId;
                                        SecurityId? securityId = new SecurityId?();
                                        long originalTransactionId = transactionId;
                                        SecurityMessage message6 = security3.ToMessage( securityId, originalTransactionId, false );
                                        if ( Messages.Extensions.IsMatch( message6, securityLookupMessage ) )
                                            TrySendByFix( message6 );
                                    }
                                }
                            }
                            else
                            {
                                foreach ( Security security2 in SecurityProvider.Lookup( securityLookupMessage ) )
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
                                            securityMessage.SecurityId = security2.ToSecurityId( null, true, false );
                                            securityMessage.OriginalTransactionId = securityLookupMessage.TransactionId;
                                        }
                                        TrySendByFix( securityMessage );
                                    }
                                }
                            }
                            TrySendByFix( securityLookupMessage.CreateResult() );
                            continue;
                        case MessageTypes.PortfolioLookup:
                            PortfolioLookupMessage lookupMsg = ( PortfolioLookupMessage )message1;
                            Exception error9 = CheckPermissions( UserPermissions.Trading );
                            if ( error9 != null )
                            {
                                TrySendByFix( lookupMsg.CreateResponse( error9 ) );
                                continue;
                            }
                            if ( !lookupMsg.IsSubscribe )
                            {
                                TrySendByFix( lookupMsg.CreateResponse( null ) );
                                continue;
                            }
                            if ( ServerSettings.SimulatorEnabled )
                            {
                                PortfolioMessage portfolioMessage = new PortfolioMessage();
                                portfolioMessage.PortfolioName = session.SessionId;
                                portfolioMessage.OriginalTransactionId = lookupMsg.TransactionId;
                                TrySendByFix( portfolioMessage );
                                _positionControllerProvider.GetController( session.SessionId ).RequestState( lookupMsg, new Action<Message>( TrySendByFix ) );
                                TrySendByFix( lookupMsg.CreateResult() );
                                continue;
                            }
                            SecurityId? securityId1 = lookupMsg.SecurityId;
                            if ( !securityId1.HasValue || securityId1.Value == new SecurityId() )
                            {
                                logs.AddWarningLog( LocalizedStrings.Str3252 );
                            }
                            else
                            {
                                SecurityId? adapterId2 = ServerMapping.TryGetAdapterId( nameof( HydraServer ), securityId1.Value );
                                if ( adapterId2.HasValue )
                                {
                                    lookupMsg = lookupMsg.TypedClone();
                                    lookupMsg.SecurityId = new SecurityId?( adapterId2.Value );
                                }
                                else if ( ServerSettings.OnlyMappedSecurities )
                                {
                                    TrySendByFix( lookupMsg.CreateResponse( new InvalidOperationException( LocalizedStrings.Str704Params.Put( securityId1 ) ) ) );
                                    continue;
                                }
                                int transactionsHistoryMaxDays = ServerSettings.TransactionsHistoryMaxDays;
                                if ( transactionsHistoryMaxDays == 0 && lookupMsg.From.HasValue )
                                    logs.AddInfoLog( LocalizedStrings.HistoryDisabled );
                                LoadAndSend( ( drive, format, from, to, sendMsg ) =>
                                {
                                    IMarketDataStorage<PositionChangeMessage> positionMessageStorage = StorageRegistry.GetPositionMessageStorage( lookupMsg.SecurityId.Value, drive, format );
                                    DateTimeOffset dateTimeOffset = from;
                                    DateTimeOffset? from1 = new DateTimeOffset?( from );
                                    DateTimeOffset? to1 = to;
                                    foreach ( PositionChangeMessage positionChangeMessage in positionMessageStorage.Load( from1, to1 ) )
                                    {
                                        positionChangeMessage.OriginalTransactionId = lookupMsg.TransactionId;
                                        TrySendByFix( positionChangeMessage );
                                        dateTimeOffset = positionChangeMessage.ServerTime;
                                    }
                                    return new DateTimeOffset?( dateTimeOffset );
                                }, lookupMsg, transactionsHistoryMaxDays );
                            }
                            TrySendByFix( lookupMsg.CreateResult() );
                            continue;
                        case MessageTypes.OrderStatus:
                            OrderStatusMessage statusMsg = ( OrderStatusMessage )message1;
                            Exception error10 = CheckPermissions( UserPermissions.Trading );
                            if ( error10 != null )
                            {
                                TrySendByFix( statusMsg.CreateResponse( error10 ) );
                                continue;
                            }
                            if ( !statusMsg.IsSubscribe )
                            {
                                TrySendByFix( statusMsg.CreateResponse( null ) );
                                continue;
                            }
                            if ( ServerSettings.SimulatorEnabled )
                            {
                                statusMsg.PortfolioName = session.SessionId;
                                foreach ( IOrderMatcher orderMatcher in _matchers.Values )
                                    orderMatcher.RequestOrders( statusMsg, new Action<Message>( TrySendByFix ) );
                                TrySendByFix( statusMsg.CreateResult() );
                                continue;
                            }
                            SecurityId securityId2 = statusMsg.SecurityId;
                            if ( securityId2 == new SecurityId() )
                            {
                                logs.AddWarningLog( LocalizedStrings.Str3252 );
                            }
                            else
                            {
                                SecurityId? adapterId2 = ServerMapping.TryGetAdapterId( nameof( HydraServer ), securityId2 );
                                if ( adapterId2.HasValue )
                                {
                                    statusMsg = statusMsg.TypedClone();
                                    statusMsg.SecurityId = adapterId2.Value;
                                }
                                else if ( ServerSettings.OnlyMappedSecurities )
                                {
                                    TrySendByFix( statusMsg.CreateResponse( new InvalidOperationException( LocalizedStrings.Str704Params.Put( securityId2 ) ) ) );
                                    continue;
                                }
                                int transactionsHistoryMaxDays = ServerSettings.TransactionsHistoryMaxDays;
                                if ( transactionsHistoryMaxDays == 0 && statusMsg.From.HasValue )
                                    logs.AddInfoLog( LocalizedStrings.HistoryDisabled );
                                LoadAndSend( ( drive, format, from, to, sendMsg ) =>
                                {
                                    IMarketDataStorage<ExecutionMessage> executionMessageStorage = StorageRegistry.GetExecutionMessageStorage( statusMsg.SecurityId, ExecutionTypes.Transaction, drive, format );
                                    DateTimeOffset dateTimeOffset = from;
                                    DateTimeOffset? from1 = new DateTimeOffset?( from );
                                    DateTimeOffset? to1 = to;
                                    foreach ( ExecutionMessage executionMessage in executionMessageStorage.Load( from1, to1 ) )
                                    {
                                        executionMessage.OriginalTransactionId = statusMsg.TransactionId;
                                        TrySendByFix( executionMessage );
                                        dateTimeOffset = executionMessage.ServerTime;
                                    }
                                    return new DateTimeOffset?( dateTimeOffset );
                                }, statusMsg, transactionsHistoryMaxDays );
                            }
                            TrySendByFix( statusMsg.CreateResult() );
                            continue;
                        case MessageTypes.Board:
                            BoardMessage message7 = ( BoardMessage )message1;
                            Exception error11 = CheckPermissions( UserPermissions.EditBoards );
                            if ( error11 != null )
                            {
                                TrySendByFix( error11.ToErrorMessage( message7.OriginalTransactionId ) );
                                continue;
                            }
                            ExchangeBoard exchangeBoard1 = ExchangeInfoProvider.GetExchangeBoard( message7.Code );
                            ExchangeInfoProvider.Save( exchangeBoard1 == null ? message7.ToBoard() : exchangeBoard1.ApplyChanges( message7 ) );
                            continue;
                        case MessageTypes.Remove:
                            RemoveMessage removeMessage = ( RemoveMessage )message1;
                            switch ( removeMessage.RemoveType )
                            {
                                case RemoveTypes.Security:
                                    Exception error12 = CheckPermissions( UserPermissions.DeleteSecurities );
                                    if ( error12 != null )
                                    {
                                        TrySendByFix( error12.ToErrorMessage( removeMessage.TransactionId ) );
                                        continue;
                                    }
                                    Security security4 = SecurityProvider.LookupById( removeMessage.RemoveId );
                                    if ( security4 != null )
                                    {
                                        HydraTaskManager.Instance.Delete( security4 );
                                        continue;
                                    }
                                    continue;
                                case RemoveTypes.Portfolio:
                                    continue;
                                case RemoveTypes.Exchange:
                                    Exchange exchange = ExchangeInfoProvider.GetExchange( removeMessage.RemoveId );
                                    if ( exchange != null )
                                    {
                                        ExchangeInfoProvider.Delete( exchange );
                                        continue;
                                    }
                                    continue;
                                case RemoveTypes.Board:
                                    Exception error13 = CheckPermissions( UserPermissions.DeleteBoards );
                                    if ( error13 != null )
                                    {
                                        TrySendByFix( error13.ToErrorMessage( removeMessage.TransactionId ) );
                                        continue;
                                    }
                                    ExchangeBoard exchangeBoard2 = ExchangeInfoProvider.GetExchangeBoard( removeMessage.RemoveId );
                                    if ( exchangeBoard2 != null )
                                    {
                                        ExchangeInfoProvider.Delete( exchangeBoard2 );
                                        continue;
                                    }
                                    continue;
                                case RemoveTypes.User:
                                    Exception error14 = CheckPermissions( UserPermissions.DeleteUsers );
                                    if ( error14 != null )
                                    {
                                        TrySendByFix( error14.ToErrorMessage( removeMessage.TransactionId ) );
                                        continue;
                                    }
                                    continue;
                                default:
                                    ArgumentOutOfRangeException error15 = new ArgumentOutOfRangeException( "removeMsg", removeMessage.RemoveType, LocalizedStrings.Str1219 );
                                    logs.AddErrorLog( error15 );
                                    TrySendByFix( error15.ToErrorMessage( removeMessage.TransactionId ) );
                                    continue;
                            }
                        case MessageTypes.UserInfo:
                            UserInfoMessage userInfoMessage = ( UserInfoMessage )message1;
                            Exception error16 = CheckPermissions( UserPermissions.EditUsers );
                            if ( error16 != null )
                            {
                                TrySendByFix( error16.ToErrorMessage( userInfoMessage.TransactionId ) );
                                continue;
                            }
                            continue;
                        case MessageTypes.UserLookup:
                            UserLookupMessage message8 = ( UserLookupMessage )message1;
                            Exception error17 = CheckPermissions( UserPermissions.GetUsers );
                            if ( error17 != null )
                            {
                                TrySendByFix( message8.CreateResponse( error17 ) );
                                continue;
                            }
                            if ( !message8.IsSubscribe )
                            {
                                TrySendByFix( message8.CreateResponse( null ) );
                                continue;
                            }
                            TrySendByFix( message8.CreateResult() );
                            continue;
                        case MessageTypes.BoardLookup:
                            BoardLookupMessage boardLookupMessage = ( BoardLookupMessage )message1;
                            Exception error18 = CheckPermissions( UserPermissions.ExchangeBoardLookup );
                            if ( error18 != null )
                            {
                                TrySendByFix( boardLookupMessage.CreateResponse( error18 ) );
                                continue;
                            }
                            if ( !boardLookupMessage.IsSubscribe )
                            {
                                TrySendByFix( boardLookupMessage.CreateResponse( null ) );
                                continue;
                            }
                            foreach ( Message message6 in ExchangeInfoProvider.LookupBoards2( boardLookupMessage ) )
                                TrySendByFix( message6 );
                            TrySendByFix( boardLookupMessage.CreateResult() );
                            continue;
                        case MessageTypes.TimeFrameLookup:
                            TimeFrameLookupMessage message9 = ( TimeFrameLookupMessage )message1;
                            Exception error19 = CheckPermissions( UserPermissions.Load );
                            if ( error19 != null )
                            {
                                TrySendByFix( message9.CreateResponse( error19 ) );
                                continue;
                            }
                            if ( !message9.IsSubscribe )
                            {
                                TrySendByFix( message9.CreateResponse( null ) );
                                continue;
                            }
                            TimeSpan[ ] array = HydraTaskManager.Instance.Tasks.SelectMany( task => task.SupportedDataTypes.FilterTimeFrames() ).Distinct().OrderBy().ToArray();
                            TimeFrameInfoMessage frameInfoMessage = new TimeFrameInfoMessage();
                            frameInfoMessage.OriginalTransactionId = message9.TransactionId;
                            frameInfoMessage.TimeFrames = array;
                            TrySendByFix( frameInfoMessage );
                            TrySendByFix( message9.CreateResult() );
                            continue;
                        case MessageTypes.SecurityMappingRequest:
                            SecurityMappingRequestMessage message10 = ( SecurityMappingRequestMessage )message1;
                            Exception error20 = CheckPermissions( UserPermissions.SecurityLookup );
                            if ( error20 != null )
                            {
                                TrySendByFix( message10.CreateResponse( error20 ) );
                                continue;
                            }
                            if ( !message10.IsSubscribe )
                            {
                                TrySendByFix( message10.CreateResponse( null ) );
                                continue;
                            }
                            ISecurityMappingStorage mappingStorage1 = ServicesRegistry.MappingStorage;
                            SecurityMappingInfoMessage mappingInfoMessage = new SecurityMappingInfoMessage();
                            mappingInfoMessage.Mapping = mappingStorage1.GetStorageNames().ToDictionary( n => n, new Func<string, IEnumerable<SecurityIdMapping>>( mappingStorage1.Get ) );
                            mappingInfoMessage.OriginalTransactionId = message10.TransactionId;
                            TrySendByFix( mappingInfoMessage );
                            TrySendByFix( message10.CreateResult() );
                            continue;
                        case MessageTypes.SecurityLegsRequest:
                            SecurityLegsRequestMessage message11 = ( SecurityLegsRequestMessage )message1;
                            Exception error21 = CheckPermissions( UserPermissions.SecurityLookup );
                            if ( error21 != null )
                            {
                                TrySendByFix( message11.CreateResponse( error21 ) );
                                continue;
                            }
                            if ( !message11.IsSubscribe )
                            {
                                TrySendByFix( message11.CreateResponse( null ) );
                                continue;
                            }
                            TrySendByFix( message11.CreateResult() );
                            continue;
                        case MessageTypes.Command:
                            CommandMessage message12 = ( CommandMessage )message1;
                            Exception error22 = CheckPermissions( UserPermissions.ServerManage );
                            if ( error22 != null )
                            {
                                TrySendByFix( error22.ToErrorMessage( message12.TransactionId ) );
                                continue;
                            }
                            switch ( message12.Command )
                            {
                                case CommandTypes.Start:
                                case CommandTypes.Stop:
                                case CommandTypes.Restart:
                                    Action<CommandTypes> processCommand = ProcessCommand;
                                    if ( processCommand != null )
                                        processCommand( message12.Command );
                                    TrySendByFix( message12.CreateResponse( null ) );
                                    continue;
                                default:
                                    ArgumentOutOfRangeException ofRangeException2 = new ArgumentOutOfRangeException( "message", message12.Command, LocalizedStrings.Str1219 );
                                    logs.AddErrorLog( ofRangeException2 );
                                    TrySendByFix( message12.CreateResponse( ofRangeException2 ) );
                                    continue;
                            }
                        case MessageTypes.SecurityRouteListRequest:
                            SecurityRouteListRequestMessage message13 = ( SecurityRouteListRequestMessage )message1;
                            Exception error23 = CheckPermissions( UserPermissions.SecurityLookup );
                            if ( error23 != null )
                            {
                                TrySendByFix( message13.CreateResponse( error23 ) );
                                continue;
                            }
                            if ( !message13.IsSubscribe )
                            {
                                TrySendByFix( message13.CreateResponse( null ) );
                                continue;
                            }
                            foreach ( KeyValuePair<Tuple<SecurityId, DataType>, Guid> adapter in ServicesRegistry.SecurityAdapterProvider.Adapters )
                            {
                                SecurityRouteMessage securityRouteMessage = new SecurityRouteMessage();
                                securityRouteMessage.SecurityId = adapter.Key.Item1;
                                securityRouteMessage.SecurityDataType = adapter.Key.Item2;
                                securityRouteMessage.AdapterId = adapter.Value;
                                securityRouteMessage.OriginalTransactionId = message13.TransactionId;
                                TrySendByFix( securityRouteMessage );
                            }
                            TrySendByFix( message13.CreateResult() );
                            continue;
                        case MessageTypes.PortfolioRouteListRequest:
                            PortfolioRouteListRequestMessage message14 = ( PortfolioRouteListRequestMessage )message1;
                            if ( !message14.IsSubscribe )
                            {
                                TrySendByFix( message14.CreateResponse( null ) );
                                continue;
                            }
                            foreach ( KeyValuePair<string, Guid> adapter in ServicesRegistry.PortfolioAdapterProvider.Adapters )
                            {
                                PortfolioRouteMessage portfolioRouteMessage = new PortfolioRouteMessage();
                                portfolioRouteMessage.PortfolioName = adapter.Key;
                                portfolioRouteMessage.AdapterId = adapter.Value;
                                portfolioRouteMessage.OriginalTransactionId = message14.TransactionId;
                                TrySendByFix( portfolioRouteMessage );
                            }
                            TrySendByFix( message14.CreateResult() );
                            continue;
                        case MessageTypes.SecurityMapping:
                            SecurityMappingMessage message15 = ( SecurityMappingMessage )message1;
                            Exception error24 = CheckPermissions( UserPermissions.EditSecurities );
                            if ( error24 != null )
                            {
                                TrySendByFix( message15.CreateResponse( error24 ) );
                                continue;
                            }
                            ISecurityMappingStorage mappingStorage2 = ServicesRegistry.MappingStorage;
                            if ( message15.IsDelete )
                                mappingStorage2.Remove( message15.StorageName, message15.Mapping.StockSharpId );
                            else
                                mappingStorage2.Save( message15.StorageName, message15.Mapping );
                            TrySendByFix( message15.CreateResult() );
                            continue;
                        default:
                            continue;
                    }
                }
                catch ( Exception ex1 )
                {
                    ex1.LogError( null );
                    try
                    {
                        ITransactionIdMessage transactionIdMessage = message1 as ITransactionIdMessage;
                        if ( transactionIdMessage != null )
                            TrySendByFix( ex1.ToErrorMessage( transactionIdMessage.TransactionId ) );
                    }
                    catch ( Exception ex2 )
                    {
                        ex2.LogError( null );
                    }
                }

                Exception CheckPermissions( UserPermissions permission )
                {
                    if ( ServerSettings.Authorization == AuthorizationModes.Anonymous )
                    {
                        if ( permission == UserPermissions.Load || permission == UserPermissions.SecurityLookup || permission == UserPermissions.ExchangeBoardLookup )
                            return null;
                        UnauthorizedAccessException unauthorizedAccessException = new UnauthorizedAccessException( LocalizedStrings.AnonymousCannotAction.Put( session.Id, permission ) );
                        logs.AddErrorLog( unauthorizedAccessException );
                        return unauthorizedAccessException;
                    }
                    PermissionCredentials permissionCredentials = _getPermissions( session.SessionId );
                    if ( permissionCredentials == null )
                    {
                        UnauthorizedAccessException unauthorizedAccessException = new UnauthorizedAccessException( LocalizedStrings.SessionNoPermissionStorage.Put( session.Id ) );
                        logs.AddErrorLog( unauthorizedAccessException );
                        return unauthorizedAccessException;
                    }

                    SynchronizedDictionary<Tuple<string, string, object, DateTime?>, bool> synchronizedDictionary;
                    if ( permissionCredentials.Permissions.TryGetValue( permission, out synchronizedDictionary ) )
                        return null;
                    UnauthorizedAccessException unauthorizedAccessException1 = new UnauthorizedAccessException( LocalizedStrings.SessionNoPermission.Put( session.Id, permission ) );
                    logs.AddErrorLog( unauthorizedAccessException1 );
                    return unauthorizedAccessException1;
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
                    _fixRequests.Enqueue( Tuple.Create( session, message.Clone() ), false );
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
                            SecurityId? stockSharpId = ServerMapping?.TryGetStockSharpId( nameof( HydraServer ), securityIdMessage.SecurityId );
                            if ( stockSharpId.HasValue )
                                securityIdMessage.SecurityId = stockSharpId.Value;
                            else if ( ServerSettings.OnlyMappedSecurities )
                                break;
                        }
                        _fixServer.SendInMessage( message );
                        break;
                    }
                    catch ( Exception ex )
                    {
                        this.AddErrorLog( ex );
                        break;
                    }
                default:
                    if ( !_unknownTypes.Add( message.Type ) )
                        break;
                    _fixServer.Server.AddErrorLog( LocalizedStrings.Str3312Params, message.Type );
                    break;
            }
        }

        private void FixServerOnStateChanged()
        {
            switch ( _fixServer.State )
            {
                case ChannelStates.Stopped:
                    _fixRequests.Close();
                    break;
                case ChannelStates.Started:
                    _fixRequests.Open();
                    new Action( ProcessFixRequests ).Thread().Launch();
                    break;
            }
            Action stateChanged = StateChanged;
            if ( stateChanged == null )
                return;
            stateChanged();
        }
    }
}
