// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Core.ConnectorHydraTask`1
// Assembly: StockSharp.Hydra.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BF4FBD4E-7629-47D5-B0AC-6D48C0A60551
// Assembly location: T:\00-StockSharp\Data\StockSharp.Hydra.Core.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StockSharp.Hydra.Core
{
    /// <summary>
    /// Базовый источник, работающий через <see cref="T:StockSharp.Messages.IMessageAdapter" />.
    /// </summary>
    /// <typeparam name="TMessageAdapter">Тип подключения.</typeparam>
    public class ConnectorHydraTask<TMessageAdapter> : BaseHydraTask, IMessageChannel, IDisposable, ICloneable<IMessageChannel>, ICloneable, IConnectorHydraTask
      where TMessageAdapter : class, IMessageAdapter
    {
        private readonly CachedSynchronizedDictionary<Security, HydraTaskSecurity> _securityMap = new CachedSynchronizedDictionary<Security, HydraTaskSecurity>();
        private readonly SynchronizedDictionary<string, HydraTaskSecurity> _associatedSecurityCodes = new SynchronizedDictionary<string, HydraTaskSecurity>( StringComparer.InvariantCultureIgnoreCase );
        private readonly SyncObject _refreshSync = new SyncObject();
        private HydraTaskSecurity _allSecurity;
        private bool _refreshOnly;
        private Action<Exception> _connectionChanged;
        private Action<Security> _newSecurity;
        private Func<bool> _isRefreshCancelled;
        private bool _isRefreshed;
        private readonly Connector _connector;
        private Action<Message> _newOutMessage;

        private StorageBuffer Buffer
        {
            get
            {
                return _connector.Buffer;
            }
        }

        public ConnectorHydraTask()
        {
            Connector connector = new Connector( EntityRegistry.Securities, new InMemoryPositionStorage(), ServicesRegistry.ExchangeInfoProvider, buffer: new StorageBuffer()
            {
                DisableStorageTimer = true
            } );

            /*
             * 
             *   85: Here I don't want the connector to do whole bunch of Partial Download.
             *  
             *           Since I am downloading the word candle, I don't need Candles to be build.
             * 
             * */
            connector.Adapter.IgnoreExtraAdapters = true;
            //connector.Adapter.SupportPartialDownload    = false;
            //connector.Adapter.SupportCandlesCompression = false;
            //connector.Adapter.SupportLookupTracking     = false;
            //connector.Adapter.IsSupportTransactionLog   = false;
            //connector.Adapter.IsSupportOrderBookSort = false;
            //

            connector.Parent = this;
            connector.Adapter.NativeIdStorage = ServicesRegistry.NativeIdStorage;
            connector.Adapter.ExtendedInfoStorage = ServicesRegistry.ExtendedInfoStorage;
            connector.Adapter.SecurityMappingStorage = ServicesRegistry.MappingStorage;
            _connector = connector;

            Adapter = typeof( TMessageAdapter ).CreateInstance<TMessageAdapter>( ( object )_connector.TransactionIdGenerator );
            Adapter.GenerateOrderBookFromLevel1 = false;

            if ( Adapter is INotifyPropertyChanged adapter )
                adapter.PropertyChanged += new PropertyChangedEventHandler( OnAdapterPropertyChanged );

            Buffer.IgnoreGeneratedMarketData = false;
            Buffer.IgnoreGeneratedTransactional = false;

            _connector.Connected += new Action( OnConnected );
            _connector.ConnectionError += new Action<Exception>( OnConnectionError );
            _connector.Disconnected += new Action( OnDisconnected );
            _connector.SecurityReceived += new Action<Subscription, Security>( OnSecurityReceived );
            _connector.LookupSecuritiesResult += new Action<SecurityLookupMessage, IEnumerable<Security>, Exception>( OnLookupSecuritiesResult );
            _connector.NewMessage += new Action<Message>( ConnectorOnNewMessage );
        }

        /// <summary>
        /// Создать <see cref="T:StockSharp.Hydra.Core.ConnectorHydraTask`1" />.
        /// </summary>
        //public ConnectorHydraTask()
        //{
        //    Connector connector = new Connector( EntityRegistry.Securities, new InMemoryPositionStorage(), ServicesRegistry.ExchangeInfoProvider, null, null, new StorageBuffer() { DisableStorageTimer = true }, true, true );
        //    connector.Parent = this;
        //    connector.Adapter.NativeIdStorage = ServicesRegistry.NativeIdStorage;
        //    connector.Adapter.ExtendedInfoStorage = ServicesRegistry.ExtendedInfoStorage;
        //    connector.Adapter.SecurityMappingStorage = ServicesRegistry.MappingStorage;
        //    _connector = connector;
        //    Adapter = typeof( TMessageAdapter ).CreateInstance<TMessageAdapter>( _connector.TransactionIdGenerator );
        //    Adapter.GenerateOrderBookFromLevel1 = false;
        //    INotifyPropertyChanged adapter = ( object )Adapter as INotifyPropertyChanged;
        //    if ( adapter != null )
        //        adapter.PropertyChanged += new PropertyChangedEventHandler( OnAdapterPropertyChanged );
        //    Buffer.IgnoreGeneratedMarketData = false;
        //    Buffer.IgnoreGeneratedTransactional = false;
        //    _connector.Connected += new Action( OnConnected );
        //    _connector.ConnectionError += new Action<Exception>( OnConnectionError );
        //    _connector.Disconnected += new Action( OnDisconnected );
        //    _connector.SecurityReceived += new Action<Subscription, Security>( OnSecurityReceived );
        //    _connector.LookupSecuritiesResult += new Action<SecurityLookupMessage, IEnumerable<Security>, Exception>( OnLookupSecuritiesResult );
        //    _connector.NewMessage += new Action<Message>( ConnectorOnNewMessage );
        //}

        private void OnAdapterPropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            if ( !( e.PropertyName == "SupportedMarketDataTypes" ) )
                return;
            NotifyPropertyChanged( e.PropertyName );
        }

        IMessageAdapter IConnectorHydraTask.Adapter
        {
            get
            {
                return Adapter;
            }
        }

        /// <summary>Адаптер сообщений.</summary>
        [Display( Description = "ConnectionSettings", GroupName = "General", Name = "Str174", Order = 0, ResourceType = typeof( LocalizedStrings ) )]
        [TypeConverter( typeof( ExpandableObjectConverter ) )]
        public TMessageAdapter Adapter { get; }

        /// <summary>Скачивать новости.</summary>
        [Display( Description = "Str2251", GroupName = "General", Name = "News", Order = 10, ResourceType = typeof( LocalizedStrings ) )]
        public bool IsDownloadNews { get; set; }

        /// <inheritdoc />
        [Display( Description = "Str2283", GroupName = "General", Name = "StartDateCandles", Order = 21, ResourceType = typeof( LocalizedStrings ) )]
        public DateTime? CandlesFromDate { get; set; }

        /// <summary>Начальная дата загрузки тиков из подключения.</summary>
        [Display( Description = "Str2283", GroupName = "General", Name = "StartDateTicks", Order = 22, ResourceType = typeof( LocalizedStrings ) )]
        public DateTime? TicksFromDate { get; set; }

        /// <summary>Resume download.</summary>
        [Display( Description = "ResumeFromLastDateInStorage", GroupName = "General", Name = "XamlStr584", Order = 23, ResourceType = typeof( LocalizedStrings ) )]
        public bool ResumeDownload { get; set; } = true;

        /// <summary>Обновлять инструменты при подключении.</summary>
        [Display( Description = "UpdateSecuritiesOnConnect", GroupName = "General", Name = "UpdateSecurities", Order = 11, ResourceType = typeof( LocalizedStrings ) )]
        public bool UpdateSecurities { get; set; }

        /// <inheritdoc />
        public override IEnumerable<StockSharp.Messages.DataType> SupportedDataTypes
        {
            get
            {
                return Adapter.GetSupportedDataTypes();
            }
        }

        /// <inheritdoc />
        public override bool CanTestConnect
        {
            get
            {
                return true;
            }
        }

        public override void TestConnect( Action<Exception> connectionChanged )
        {
            Action<Exception> action = connectionChanged;
            if ( action == null )
                throw new ArgumentNullException( nameof( connectionChanged ) );
            _connectionChanged = action;
            _refreshOnly = true;
            Connect( false, false );
        }

        /// <inheritdoc />
        public override SecurityLookupSupportTypes SecurityLookupSupportType
        {
            get
            {
                if ( !Adapter.IsMessageSupported( MessageTypes.SecurityLookup ) )
                    return SecurityLookupSupportTypes.NotSupported;
                return !Adapter.IsSupportSecuritiesLookupAll() ? SecurityLookupSupportTypes.CodeRequired : SecurityLookupSupportTypes.SupportAll;
            }
        }

        /// <inheritdoc />
        public override IEnumerable<Level1Fields> CandlesBuildFrom
        {
            get
            {
                return Adapter.CandlesBuildFrom;
            }
        }

        /// <inheritdoc />
        public override IEnumerable<int> SupportedDepths
        {
            get
            {
                return Adapter.SupportedOrderBookDepths;
            }
        }

        /// <inheritdoc />
        public override bool IsAllDownloadingSupported( StockSharp.Messages.DataType dataType )
        {
            return Adapter.IsAllDownloadingSupported( dataType );
        }

        /// <summary>Освободить занятые ресурсы.</summary>
        protected override void DisposeManaged()
        {
            _connector.Connected -= new Action( OnConnected );
            _connector.ConnectionError -= new Action<Exception>( OnConnectionError );
            _connector.Disconnected -= new Action( OnDisconnected );
            _connector.SecurityReceived -= new Action<Subscription, Security>( OnSecurityReceived );
            _connector.LookupSecuritiesResult -= new Action<SecurityLookupMessage, IEnumerable<Security>, Exception>( OnLookupSecuritiesResult );
            _connector.NewMessage -= new Action<Message>( ConnectorOnNewMessage );
            base.DisposeManaged();
        }

        private void OnSecurityReceived( Subscription subscription, Security security )
        {
            if ( !_refreshOnly && ( State == TaskStates.Stopping || State == TaskStates.Stopped ) )
                return;
            Func<bool> refreshCancelled = _isRefreshCancelled;
            if ( ( refreshCancelled != null ? ( refreshCancelled() ? 1 : 0 ) : 0 ) != 0 )
                return;
            SaveSecurity( security );
            if ( _refreshOnly || _allSecurity == null || !Adapter.IsSecurityRequired( StockSharp.Messages.DataType.Level1 ) )
                return;
            SubscribeSecurity( security );
        }

        private void OnDisconnected()
        {
            RaiseRefreshComplete();
            if ( _refreshOnly )
                return;
            RaiseStopped();
        }

        private void OnConnectionError( Exception error )
        {
            RaiseRefreshComplete();
            if ( _refreshOnly )
            {
                Action<Exception> connectionChanged = _connectionChanged;
                if ( connectionChanged != null )
                    connectionChanged( error );
                _connectionChanged = null;
            }
            else
                Stop();
        }

        private void OnConnected()
        {
            if ( _refreshOnly )
            {
                if ( _connectionChanged == null )
                    return;
                _connectionChanged( null );
                _connectionChanged = null;
                _connector.Disconnect();
            }
            else
            {
                bool flag = IsDownloadNews;
                if ( _allSecurity == null )
                {
                    foreach ( KeyValuePair<Security, HydraTaskSecurity> cachedPair in _securityMap.CachedPairs )
                    {
                        SubscribeSecurity( cachedPair.Key );
                        if ( !flag && cachedPair.Value.Enabled( StockSharp.Messages.DataType.News ) )
                            flag = true;
                    }
                }
                else
                {
                    if ( !Adapter.IsSecurityRequired( StockSharp.Messages.DataType.Level1 ) )
                        SubscribeSecurity( _allSecurity.Security );
                    if ( !flag && _allSecurity.Enabled( StockSharp.Messages.DataType.News ) )
                        flag = true;
                }
                RaiseStarted();
                if ( !flag )
                    return;
                _connector.SubscribeNews( null, new DateTimeOffset?(), new DateTimeOffset?(), new long?(), null, new long?() );
            }
        }

        private void OnLookupSecuritiesResult(
          SecurityLookupMessage message,
          IEnumerable<Security> securities,
          Exception error )
        {
            if ( _newSecurity == null )
                return;
            foreach ( Security security in securities )
            {
                if ( !_isRefreshCancelled() )
                    _newSecurity( security );
                else
                    break;
            }
            if ( _connector.ConnectionState == ConnectionStates.Connected )
                _connector.Disconnect();
            RaiseRefreshComplete();
        }

        /// <inheritdoc />
        protected override void OnStarting()
        {
            _refreshOnly = false;
            _newSecurity = null;
            _isRefreshCancelled = null;
            _connector.UpdateSecurityByDefinition = UpdateSecurities;
            _connector.UpdateSecurityByLevel1 = UpdateSecurities;
            _connector.UpdateSecurityLastQuotes = false;
            Connect( true, true );
        }

        private void Connect( bool supportLookup, bool needReconnect )
        {
            TMessageAdapter messageAdapter = Adapter.TypedClone<TMessageAdapter>();
            if ( _refreshOnly || Securities.All<HydraTaskSecurity>( s => s.GetDataTypes().All<StockSharp.Messages.DataType>( t => t.IsMarketData ) ) )
                messageAdapter.SupportedInMessages = messageAdapter.SupportedInMessages.Except<MessageTypes>( StockSharp.Messages.Extensions.TransactionalMessageTypes );
            if ( !needReconnect )
            {
                messageAdapter.ReConnectionSettings.AttemptCount = 0;
                messageAdapter.ReConnectionSettings.ReAttemptCount = 0;
            }
            if ( !supportLookup )
                _connector.LookupMessagesOnConnect.Clear();
            _connector.IsAutoPortfoliosSubscribe = supportLookup;
            _connector.Adapter.InnerAdapters.Clear();
            _connector.Adapter.InnerAdapters.Add( messageAdapter );
            _allSecurity = null;
            _securityMap.Clear();
            _associatedSecurityCodes.Clear();
            if ( !_refreshOnly )
            {
                _allSecurity = this.GetAllSecurity();
                if ( _allSecurity == null )
                {
                    Buffer.FilterSubscription = true;
                    foreach ( HydraTaskSecurity security in Securities )
                        _securityMap.Add( security.Security, security );
                    _associatedSecurityCodes.AddRange<KeyValuePair<string, HydraTaskSecurity>>( Securities.Where<HydraTaskSecurity>( p => p.Security.Board == ExchangeBoard.Associated ).DistinctBy<HydraTaskSecurity, string>( sec => sec.Security.Code ).ToDictionary<HydraTaskSecurity, string, HydraTaskSecurity>( s => s.Security.Code, s => s ) );
                }
                else
                    Buffer.FilterSubscription = false;
            }
            _connector.ClearCache();
            _connector.Connect();
        }

        /// <inheritdoc />
        protected override void FinalizeTask()
        {
            try
            {
                OnStopped();
            }
            catch ( Exception ex )
            {
                this.AddErrorLog( ex );
            }
        }

        /// <inheritdoc />
        protected override void OnStopped()
        {
            RaiseRefreshComplete();
            if ( _connector.ConnectionState == ConnectionStates.Failed )
            {
                ProcessNewData();
                RaiseStopped();
            }
            else
            {
                _connector.Disconnect();
                ProcessNewData();
            }
            base.OnStopped();
        }

        /// <summary>
        /// Подписаться на получение реалтайм данных для инструмента.
        /// </summary>
        /// <param name="security">Инструмент.</param>
        private void SubscribeSecurity( Security security )
        {
            if ( _allSecurity == null && !_securityMap.ContainsKey( security ) )
                return;
            HydraTaskSecurity taskSecurity;
            foreach ( StockSharp.Messages.DataType dataType in GetDataTypes( security, out taskSecurity ).ToArray<StockSharp.Messages.DataType>() )
            {
                if ( dataType == StockSharp.Messages.DataType.MarketDepth )
                {
                    int? maxDepth = taskSecurity != null ? taskSecurity.GetMaxDepth( dataType ) : new int?();
                    ProcessDataTypeSubscription( ( Action<Security, DateTimeOffset?, DateTimeOffset?> )( ( sec, from, to ) =>
                    {
                        long? count = new long?();                        
                        TimeSpan? refreshSpeed = new TimeSpan?();
                        long? skip = new long?();

                        _connector.SubscribeMarketDepth( sec, from, to, count, MarketDataBuildModes.LoadAndBuild, null, maxDepth, refreshSpeed, null, true, null, skip );
                    } ), security, taskSecurity, dataType, TicksFromDate );
                }
                else if ( dataType == StockSharp.Messages.DataType.Level1 )
                    ProcessDataTypeSubscription( ( sec, from, to ) => _connector.SubscribeLevel1( sec, from, to, new long?(), MarketDataBuildModes.LoadAndBuild, null, null, new long?() ), security, taskSecurity, dataType, TicksFromDate );
                else if ( dataType == StockSharp.Messages.DataType.Ticks )
                    ProcessDataTypeSubscription( ( sec, from, to ) => _connector.SubscribeTrades( sec, from, to, new long?(), MarketDataBuildModes.LoadAndBuild, null, null, new long?() ), security, taskSecurity, dataType, TicksFromDate );
                else if ( dataType == StockSharp.Messages.DataType.OrderLog )
                    ProcessDataTypeSubscription( ( sec, from, to ) => _connector.SubscribeOrderLog( sec, from, to, new long?(), null, new long?() ), security, taskSecurity, dataType, TicksFromDate );
                else if ( !( dataType == StockSharp.Messages.DataType.Transactions ) && !( dataType == StockSharp.Messages.DataType.PositionChanges ) && dataType.IsCandles )
                {
                    CandleSeries series = dataType.ToCandleSeries( security );
                    bool? nullable1;
                    bool? nullable2;
                    if ( taskSecurity == null )
                    {
                        nullable1 = new bool?();
                        nullable2 = nullable1;
                    }
                    else
                        nullable2 = taskSecurity.GetVolumeProfile( dataType );
                    bool? nullable3 = nullable2;
                    series.BuildCandlesField = taskSecurity != null ? taskSecurity.GetCandlesBuildFrom( dataType ) : new Level1Fields?();
                    CandleSeries candleSeries = series;
                    nullable1 = nullable3;
                    bool flag = true;
                    int num = nullable1.GetValueOrDefault() == flag & nullable1.HasValue ? 1 : 0;
                    candleSeries.IsCalcVolumeProfile = num != 0;
                    ProcessDataTypeSubscription( ( sec, from, to ) => _connector.SubscribeCandles( series, from, to, new long?(), new long?(), null, new long?() ), security, taskSecurity, dataType, CandlesFromDate );
                }
            }
        }

        private void ProcessDataTypeSubscription(
          Action<Security, DateTimeOffset?, DateTimeOffset?> subscribe,
          Security security,
          HydraTaskSecurity map,
          StockSharp.Messages.DataType dataType,
          DateTime? taskSettingsStartDate )
        {
            if ( subscribe == null )
                throw new ArgumentNullException( nameof( subscribe ) );
            if ( map == null || security.IsAllSecurity() )
            {
                this.AddVerboseLog( string.Format( "{0}: ignoring map isnull={1}", security.Id, map == null ) );
                subscribe( security, new DateTimeOffset?(), new DateTimeOffset?() );
            }
            else
            {
                bool resumeDownload = ResumeDownload;
                DateTime? beginDate = map.GetBeginDate( dataType );
                DateTime? nullable1 = beginDate;
                DateTime? nullable2 = nullable1.HasValue ? nullable1 : taskSettingsStartDate;
                DateTimeOffset? nullable3 = nullable2.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable2.GetValueOrDefault() ) : new DateTimeOffset?();
                DateTimeOffset? nullable4;
                DateTimeOffset? nullable5;
                if ( resumeDownload )
                {
                    DateTimeOffset? max = StorageRegistry.GetStorage( security, dataType, Drive, StorageFormat ).GetRange( new DateTimeOffset?(), new DateTimeOffset?() )?.Max;
                    this.AddVerboseLog( string.Format( "{0}: storageLastDate={1}", security.Id, max ) );
                    if ( nullable3.HasValue )
                    {
                        nullable4 = max;
                        nullable5 = nullable3;
                        if ( ( nullable4.HasValue & nullable5.HasValue ? ( nullable4.GetValueOrDefault() > nullable5.GetValueOrDefault() ? 1 : 0 ) : 0 ) == 0 )
                            goto label_8;
                    }
                    nullable3 = max;
                }
            label_8:
                this.AddVerboseLog( string.Format( "{0}: resume={1}, taskSecStartDate={2}, taskSettingsStartDate={3}, fromDate={4}", security.Id, resumeDownload, beginDate, taskSettingsStartDate, nullable3 ) );
                DateTime? nullable6 = map.GetEndDate( dataType );
                if ( nullable6.HasValue )
                {
                    nullable2 = nullable6;
                    TimeSpan timeSpan = TimeSpan.FromDays( 1.0 );
                    DateTime? nullable7;
                    if ( !nullable2.HasValue )
                    {
                        nullable1 = new DateTime?();
                        nullable7 = nullable1;
                    }
                    else
                        nullable7 = new DateTime?( nullable2.GetValueOrDefault() + timeSpan );
                    nullable6 = nullable7;
                    if ( nullable3.HasValue )
                    {
                        nullable5 = nullable3;
                        nullable2 = nullable6;
                        nullable4 = nullable2.HasValue ? new DateTimeOffset?( ( DateTimeOffset )nullable2.GetValueOrDefault() ) : new DateTimeOffset?();
                        if ( ( nullable5.HasValue & nullable4.HasValue ? ( nullable5.GetValueOrDefault() > nullable4.GetValueOrDefault() ? 1 : 0 ) : 0 ) != 0 )
                        {
                            nullable2 = nullable6;
                            DateTimeOffset? nullable8;
                            if ( !nullable2.HasValue )
                            {
                                nullable4 = new DateTimeOffset?();
                                nullable8 = nullable4;
                            }
                            else
                                nullable8 = new DateTimeOffset?( ( DateTimeOffset )nullable2.GetValueOrDefault() );
                            nullable3 = nullable8;
                        }
                    }
                }
                this.AddVerboseLog( string.Format( "{0}: type={1}, from={2}, to={3}", security.Id, dataType, nullable3, nullable6 ) );
                Action<Security, DateTimeOffset?, DateTimeOffset?> action = subscribe;
                Security security1 = security;
                DateTimeOffset? nullable9 = nullable3;
                nullable2 = nullable6;
                DateTimeOffset? nullable10;
                if ( !nullable2.HasValue )
                {
                    nullable4 = new DateTimeOffset?();
                    nullable10 = nullable4;
                }
                else
                    nullable10 = new DateTimeOffset?( ( DateTimeOffset )nullable2.GetValueOrDefault() );
                action( security1, nullable9, nullable10 );
            }
        }

        private void ConnectorOnNewMessage( Message message )
        {
            try
            {
                Action<Message> newOutMessage = _newOutMessage;
                if ( newOutMessage == null )
                    return;
                newOutMessage( message );
            }
            catch ( Exception ex )
            {
                this.AddErrorLog( ex );
            }
        }

        private void RaiseRefreshComplete()
        {
            lock ( _refreshSync )
            {
                _isRefreshed = true;
                _refreshSync.Pulse();
            }
        }

        private IEnumerable<StockSharp.Messages.DataType> GetDataTypes(
          Security security,
          out HydraTaskSecurity taskSecurity )
        {
            taskSecurity = null;
            if ( _allSecurity != null )
            {
                taskSecurity = _allSecurity;
                return _allSecurity.GetDataTypes();
            }
            if ( security.Board == ExchangeBoard.Associated )
                return Enumerable.Empty<StockSharp.Messages.DataType>();
            taskSecurity = _securityMap.TryGetValue<Security, HydraTaskSecurity>( security );
            if ( taskSecurity != null )
                return taskSecurity.GetDataTypes();
            return _associatedSecurityCodes.TryGetValue<string, HydraTaskSecurity>( security.Code )?.GetDataTypes() ?? Enumerable.Empty<StockSharp.Messages.DataType>();
        }

        public override void Refresh(
          ISecurityStorage securityStorage,
          SecurityLookupMessage criteria,
          Action<Security> newSecurity,
          Func<bool> isCancelled )
        {
            if ( securityStorage == null )
                throw new ArgumentNullException( nameof( securityStorage ) );
            if ( criteria == null )
                throw new ArgumentNullException( nameof( criteria ) );
            Action<Security> action = newSecurity;
            if ( action == null )
                throw new ArgumentNullException( nameof( newSecurity ) );
            _newSecurity = action;
            Func<bool> func = isCancelled;
            if ( func == null )
                throw new ArgumentNullException( nameof( isCancelled ) );
            _isRefreshCancelled = func;
            _refreshOnly = false;
            _isRefreshed = false;
            if ( State == TaskStates.Stopped )
            {
                _refreshOnly = true;
                Connect( false, true );
            }
            _connector.LookupSecurities( criteria );
            bool flag = false;
            DateTime now = TimeHelper.Now;
            lock ( _refreshSync )
            {
                while ( !_isRefreshed )
                {
                    if ( ( TimeHelper.Now - now ).TotalMinutes > 20.0 )
                        throw new TimeoutException();
                    if ( !flag && _isRefreshCancelled() )
                    {
                        flag = true;
                        if ( _connector.ConnectionState == ConnectionStates.Connected || _connector.ConnectionState == ConnectionStates.Connecting )
                            _connector.Disconnect();
                    }
                    _refreshSync.Wait( new TimeSpan?( TimeSpan.FromSeconds( 3.0 ) ) );
                }
            }
        }

        /// <inheritdoc />
        protected override TimeSpan OnProcess()
        {
            ProcessNewData();
            return base.OnProcess();
        }

        private void SaveValues<T>(
          IDictionary<SecurityId, IEnumerable<T>> newValues,
          Action<Security, IEnumerable<T>> saveValues )
        {
            if ( newValues == null )
                throw new ArgumentNullException( nameof( newValues ) );
            foreach ( KeyValuePair<SecurityId, IEnumerable<T>> newValue in ( IEnumerable<KeyValuePair<SecurityId, IEnumerable<T>>> )newValues )
            {
                if ( !CanProcess() )
                    break;
                saveValues( GetSecurity( newValue.Key ), newValue.Value );
            }
        }

        /* -------------------------------------------------------------------------------------------------------------------------------------------
		* 
		*   19: Here we save all the ticks, Candles and the rest.
		* 
		* ------------------------------------------------------------------------------------------------------------------------------------------- */
        private void ProcessNewData()
        {
            StorageBuffer buffer = Buffer;
            SaveValues( buffer.GetTicks(), SaveTicks );
            SaveValues( buffer.GetOrderBooks(), SaveDepths );
            SaveValues( buffer.GetOrderLog(), SaveOrderLog );
            SaveValues( buffer.GetLevel1(), SaveLevel1Changes );
            SaveValues( buffer.GetTransactions(), SaveTransactions );
            SaveValues( buffer.GetPositionChanges(), SavePositionChanges );

            foreach ( var candle in buffer.GetCandles() )
            {
                SaveCandles( GetSecurity( candle.Key.Item1 ), candle.Value );
            }

            SaveNews( buffer.GetNews() );
            SaveBoardStates( buffer.GetBoardStates() );
        }

        /// <inheritdoc />
        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.Add( "Adapter", Adapter.Save() );
            storage.Add( "IsDownloadNews", IsDownloadNews );
            storage.Add( "CandlesFromDate", CandlesFromDate );
            storage.Add( "TicksFromDate", TicksFromDate );
            storage.Add( "UpdateSecurities", UpdateSecurities );
            storage.Add( "ResumeDownload", ResumeDownload );
        }

        /// <inheritdoc />
        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            if ( storage.Contains( "Adapter" ) )
                Adapter.Load( storage.GetValue<SettingsStorage>( "Adapter", null ) );
            IsDownloadNews = storage.GetValue<bool>( "IsDownloadNews", IsDownloadNews );
            CandlesFromDate = storage.GetValue<DateTime?>( "CandlesFromDate", CandlesFromDate );
            TicksFromDate = storage.GetValue<DateTime?>( "TicksFromDate", TicksFromDate );
            UpdateSecurities = storage.GetValue<bool>( "UpdateSecurities", UpdateSecurities );
            ResumeDownload = storage.GetValue<bool>( "ResumeDownload", true );
        }

        IMessageChannel ICloneable<IMessageChannel>.Clone()
        {
            return ( IMessageChannel )Clone();
        }

        ChannelStates IMessageChannel.State
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        void IMessageChannel.Open()
        {
            throw new NotSupportedException();
        }

        void IMessageChannel.Close()
        {
            throw new NotSupportedException();
        }

        void IMessageChannel.Suspend()
        {
            throw new NotSupportedException();
        }

        void IMessageChannel.Resume()
        {
            throw new NotSupportedException();
        }

        void IMessageChannel.Clear()
        {
            throw new NotSupportedException();
        }

        bool IMessageChannel.SendInMessage( Message message )
        {
            throw new NotSupportedException();
        }

        event Action IMessageChannel.StateChanged
        {
            add
            {
                throw new NotSupportedException();
            }
            remove
            {
                throw new NotSupportedException();
            }
        }

        event Action<Message> IMessageChannel.NewOutMessage
        {
            add
            {
                _newOutMessage += value;
            }
            remove
            {
                _newOutMessage -= value;
            }
        }
    }
}
