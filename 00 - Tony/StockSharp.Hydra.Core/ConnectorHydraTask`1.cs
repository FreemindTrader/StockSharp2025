using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using fx.Collections;
using fx.Common;
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
using DataType = StockSharp.Messages.DataType;

namespace StockSharp.Hydra.Core
{
    /// <summary>
    /// Базовый источник, работающий через <see cref="T:StockSharp.Messages.IMessageAdapter" />.
    /// </summary>
    /// <typeparam name="TMessageAdapter">Тип подключения.</typeparam>
    public class ConnectorHydraTask<TMessageAdapter> : BaseHydraTask, IMessageChannel, IDisposable, ICloneable<IMessageChannel>, ICloneable, IConnectorHydraTask where TMessageAdapter : class, IMessageAdapter
    {
        private readonly CachedSynchronizedDictionary<Security, HydraTaskSecurity> _securityMap             = new CachedSynchronizedDictionary<Security, HydraTaskSecurity>();
        private readonly SynchronizedDictionary<string, HydraTaskSecurity>         _associatedSecurityCodes = new SynchronizedDictionary<string, HydraTaskSecurity>( StringComparer.InvariantCultureIgnoreCase );
        private readonly SynchronizedDictionary<Security, List<LongDownloadTaskInfo>> _longRunningCandlesTask = new SynchronizedDictionary<Security, List<LongDownloadTaskInfo>>();

        private readonly SyncObject _refreshSync = new SyncObject();
        private HydraTaskSecurity   _allSecurity;
        private bool                _refreshOnly;
        private Action<Exception>   _connectionChanged;
        private Action<Security>    _newSecurity;
        private Func<bool>          _isRefreshCancelled;
        private bool                _isRefreshed;
        private readonly Connector  _connector;
        private Action<Message>     _newOutMessage;

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

            connector.Parent                         = this;
            connector.Adapter.NativeIdStorage        = ServicesRegistry.NativeIdStorage;
            connector.Adapter.ExtendedInfoStorage    = ServicesRegistry.ExtendedInfoStorage;
            connector.Adapter.SecurityMappingStorage = ServicesRegistry.MappingStorage;
            _connector                               = connector;

            Adapter = typeof( TMessageAdapter ).CreateInstance<TMessageAdapter>( _connector.TransactionIdGenerator );
            Adapter.GenerateOrderBookFromLevel1 = false;

            if ( Adapter is INotifyPropertyChanged adapter )
                adapter.PropertyChanged += new PropertyChangedEventHandler( OnAdapterPropertyChanged );

            Buffer.IgnoreGeneratedMarketData = false;
            Buffer.IgnoreGeneratedTransactional = false;

            _connector.Connected              += OnConnected;
            _connector.ConnectionError        += OnConnectionError;
            _connector.Disconnected           += OnDisconnected;
            _connector.SecurityReceived       += OnSecurityReceived;
            _connector.LookupSecuritiesResult += OnLookupSecuritiesResult;
            _connector.NewMessage             += ConnectorOnNewMessage;
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
        public override IEnumerable<Messages.DataType> SupportedDataTypes
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
        public override bool IsAllDownloadingSupported( Messages.DataType dataType )
        {
            return Adapter.IsAllDownloadingSupported( dataType );
        }

        /// <summary>Освободить занятые ресурсы.</summary>
        protected override void DisposeManaged()
        {
            _connector.Connected              -= OnConnected;
            _connector.ConnectionError        -= OnConnectionError;
            _connector.Disconnected           -= OnDisconnected;
            _connector.SecurityReceived       -= OnSecurityReceived;
            _connector.LookupSecuritiesResult -= OnLookupSecuritiesResult;
            _connector.NewMessage             -= ConnectorOnNewMessage;

            base.DisposeManaged();
        }

        private void OnSecurityReceived( Subscription subscription, Security security )
        {
            if ( !_refreshOnly && ( State == TaskStates.Stopping || State == TaskStates.Stopped ) )
            {
                return;
            }

            if ( _isRefreshCancelled != null && _isRefreshCancelled() )
            {
                return;
            }

            SaveSecurity( security );

            if ( _refreshOnly || _allSecurity == null || !Adapter.IsSecurityRequired( Messages.DataType.Level1 ) )
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
                bool news = IsDownloadNews;
                if ( _allSecurity == null )
                {
                    /* ---------------------------------------------------------------------------------------------------------------------------------------
                     * 
                     * Tony: Here is where we subscribe to the download all the selected symbols on the UI
                     * 
                     *       Since I am going to run whole bunch of long running task to download everything since databars are available.
                     *       So I am going to running some background threads.
                     * 
                     *       1) background thread to check for missing bars.
                     *       2) Background thread to download all the way back to the earliest.
                     *--------------------------------------------------------------------------------------------------------------------------------------- 
                     */

                    try
                    {
                        foreach ( KeyValuePair<Security, HydraTaskSecurity> cachedPair in _securityMap.CachedPairs )
                        {
                            SubscribeSecurity( cachedPair.Key );
                            if ( !news && cachedPair.Value.Enabled( Messages.DataType.News ) )
                                news = true;
                        }
                    }
                    catch( Exception ex )
                    {
                        if ( _connector is ILogReceiver logs )
                            logs.AddErrorLog( ex );
                    }


                    ThreadingHelper.Launch( ThreadingHelper.Thread( ProcessLongRunningDownload ) );
                }
                else
                {
                    if ( !Adapter.IsSecurityRequired( Messages.DataType.Level1 ) )
                        SubscribeSecurity( _allSecurity.Security );
                    if ( !news && _allSecurity.Enabled( Messages.DataType.News ) )
                        news = true;
                }
                RaiseStarted();
                if ( !news )
                    return;
                _connector.SubscribeNews( null, new DateTimeOffset?(), new DateTimeOffset?(), new long?(), null, new long?() );
            }
        }

        private void ProcessLongRunningDownload()
        {
            if ( _longRunningCandlesTask.Count == 0 )
            {
                return;
            }

            foreach ( KeyValuePair<Security, List<LongDownloadTaskInfo>> pair in _longRunningCandlesTask )
            {
                var security = pair.Key;
                var taskList = pair.Value;

                var priorityTasks = taskList.OrderBy( x => x.Priority );

                foreach ( var runInfo in priorityTasks )
                {
                    if ( runInfo.IsTick )
                    {
                        long nextId = _connector.TransactionIdGenerator.GetNextId();
                        //_histTickRequests.Add( nextId, Tuple.Create( security, fromDay ) );

                        //Connector connector = Connector;
                        var msg           = new MarketDataMessage();
                        msg.SecurityId    = security.ToSecurityId();
                        msg.DataType      = MarketDataTypes.Level1;
                        msg.From          = new DateTimeOffset?( runInfo.From.Value );
                        msg.To            = new DateTimeOffset?( runInfo.To.Value );
                        msg.IsSubscribe   = true;
                        msg.TransactionId = nextId;

                        MarketDataMessage marketDataMessage = msg.ValidateBounds();
                        _connector.SendInMessage( marketDataMessage );
                    }
                    else
                    {
                        var count = new long?();
                        var transactionId = new long?();

                        var extendedInfo = new Dictionary<string, object>();

                        extendedInfo.Add( "DownloadBackward", true );

                        if ( _connector is null )
                            throw new ArgumentNullException( nameof( _connector ) );

                        if ( _connector is ILogReceiver logs )
                            logs.AddInfoLog( nameof( _connector ) );

                        var subscription = new Subscription( runInfo.CandleSeries );

                        var mdMsg = ( MarketDataMessage )subscription.SubscriptionMessage;

                        if ( runInfo.From != null )
                            mdMsg.From = runInfo.From.Value;

                        if ( runInfo.To != null )
                            mdMsg.To = runInfo.To.Value;

                        if ( count != null )
                            mdMsg.Count = count.Value;
                        

                        mdMsg.Adapter = null;

                        mdMsg.ExtensionInfo = extendedInfo;

                        if ( transactionId != null )
                            subscription.TransactionId = transactionId.Value;

                        _connector.Subscribe( subscription );

                        //_connector.SubscribeCandles( runInfo.CandleSeries, runInfo.From, runInfo.To, count, transactionId, extendedInfo );
                    }

                }
            }


        }


        private void OnLookupSecuritiesResult( SecurityLookupMessage message, IEnumerable<Security> securities, Exception error )
        {
            if ( _newSecurity == null )
            {
                return;
            }
                
            foreach ( Security security in securities )
            {
                if ( !_isRefreshCancelled() )
                {
                    _newSecurity( security );
                }                    
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
            TMessageAdapter messageAdapter = Adapter.TypedClone();
            if ( _refreshOnly || Securities.All( s => s.GetDataTypes().All( t => t.IsMarketData ) ) )
                messageAdapter.SupportedInMessages = messageAdapter.SupportedInMessages.Except( Messages.Extensions.TransactionalMessageTypes );
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
                    _associatedSecurityCodes.AddRange( Securities.Where( p => p.Security.Board == ExchangeBoard.Associated ).DistinctBy( sec => sec.Security.Code ).ToDictionary( s => s.Security.Code, s => s ) );
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
            {
                return;
            }

            
            HydraTaskSecurity taskSecurity;

            foreach ( Messages.DataType dataType in GetDataTypes( security, out taskSecurity ).ToArray() )
            {
                if ( dataType == Messages.DataType.MarketDepth )
                {
                    int? maxDepth = taskSecurity != null ? taskSecurity.GetMaxDepth( dataType ) : new int?();
                    
                    ProcessDataTypeSubscription( ( sec, from, to ) =>
                    {
                        long? count = new long?();
                        TimeSpan? refreshSpeed = new TimeSpan?();
                        long? skip = new long?();

                        _connector.SubscribeMarketDepth( sec, from, to, count, MarketDataBuildModes.LoadAndBuild, null, maxDepth, refreshSpeed, null, true, null, skip );
                    }, security, taskSecurity, dataType, TicksFromDate );
                }
                else if ( dataType == Messages.DataType.Level1 )
                {
                    ProcessLevel1Subscription( security );
                    //ProcessDataTypeSubscription( ( sec, from, to ) => _connector.SubscribeLevel1( sec, from, to, new long?(), MarketDataBuildModes.LoadAndBuild, null, null, new long?() ), security, taskSecurity, dataType, TicksFromDate );
                }                    
                else if ( dataType == Messages.DataType.Ticks )
                {
                    ProcessDataTypeSubscription( ( sec, from, to ) => _connector.SubscribeTrades( sec, from, to, new long?(), MarketDataBuildModes.LoadAndBuild, null, null, new long?() ), security, taskSecurity, dataType, TicksFromDate );
                }                    
                else if ( dataType == Messages.DataType.OrderLog )
                {
                    ProcessDataTypeSubscription( ( sec, from, to ) => _connector.SubscribeOrderLog( sec, from, to, new long?(), null, new long?() ), security, taskSecurity, dataType, TicksFromDate );
                }                    
                else if ( !( dataType == Messages.DataType.Transactions ) && !( dataType == Messages.DataType.PositionChanges ) && dataType.IsCandles )
                {
                    if ( dataType.IsCandles && dataType.MessageType == typeof( TimeFrameCandleMessage ) )
                    {
                        ProcessCandleSubscription( security, dataType );
                    }
                    else
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
        }

        private void ProcessCandleSubscription( Security security, DataType dataType )
        {
            HydraTaskSecurity taskSecurity;

            var period       = ( TimeSpan )( dataType.Arg );
            
            var secDataTypes = GetDataTypes( security, out taskSecurity ).ToArray<DataType>();

            var storage      = StorageRegistry.GetCandleMessageStorage( dataType.MessageType, security.ToSecurityId(), dataType.Arg, Drive, StorageFormat );
            var beginTime    = taskSecurity.GetBeginDate( dataType );
            var endTime      = taskSecurity.GetEndDate( dataType );

            if ( ! beginTime.HasValue )
            {
                beginTime = CandlesFromDate.Value;
            }
            else if ( beginTime.HasValue && CandlesFromDate.Value < beginTime.Value )
            {
                beginTime = CandlesFromDate.Value;
            }

            if ( !endTime.HasValue )
            {
                endTime = DateTime.UtcNow;
            }
            

            var result = fxCandleHelper.PrepareDownloadRange( storage, period, beginTime.Value, endTime );

            foreach ( var forward in result.Item1 )
            {
                var series        = new CandleSeries();
                series.Security   = security;
                series.CandleType = dataType.MessageType.ToCandleType();
                series.Arg        = dataType.Arg;
                

                var from          = new DateTimeOffset?( forward.BeginDate );
                var to            = new DateTimeOffset?( forward.EndDate );
                var count         = new long?();
                var transactionId = new long?();

                _connector.SubscribeCandles( series, from, to, count, transactionId, null );
            }


            foreach ( var backward in result.Item2 )
            {
                var series        = new CandleSeries();
                series.Security   = security;
                series.CandleType = dataType.MessageType.ToCandleType();
                series.Arg        = dataType.Arg;
                

                var from          = new DateTimeOffset?( backward.BeginDate );
                var to            = new DateTimeOffset?( backward.EndDate );

                _longRunningCandlesTask.SafeAdd( security ).Add( new LongDownloadTaskInfo( series, from, to, backward.Priority ) );
            }
        }

        private void ProcessLevel1Subscription( Security security )
        {            
            HydraTaskSecurity taskSecurity;
            var secDataTypes = GetDataTypes( security, out taskSecurity ).ToArray( );
            
            var missingDates = new PooledList<Ecng.ComponentModel.Range<DateTime>>();
            var downloadBackward = new List<Tuple<DateTime, DateTime, int>>();
            var downloadForward = new List<Tuple<DateTime, DateTime>>();
            var currentTime = DateTime.UtcNow;

            if ( taskSecurity != null )
            {
                var taskBegin     = taskSecurity.GetBeginDate( DataType.Level1 );
                var taskEnd       = taskSecurity.GetEndDate( DataType.Level1 );

                var lvl1Storage   = StorageRegistry.GetLevel1MessageStorage( security.ToSecurityId(), Drive, StorageFormat );
                var lvlOneDates   = lvl1Storage.Dates;

                var lvl1Store1st  = lvlOneDates.FirstOr();
                var lvl1StoreLast = lvlOneDates.LastOr<DateTime>();

                var loadFrom      = taskBegin ?? lvl1Store1st;
                var loadTo        = taskEnd ?? lvl1StoreLast;

                if ( loadFrom.HasValue && loadTo.HasValue )
                {
                    DatesHelper.GetDatesForMissingTicks( lvlOneDates, loadFrom, loadTo, ref missingDates );
                }


                if ( lvl1StoreLast == null && lvl1Store1st == null )
                {
                    // This case means we have nothing in the storage.
                    DateTime fromTime = taskBegin ?? CandlesFromDate.Value;

                    downloadBackward.Add( new Tuple<DateTime, DateTime, int>( fromTime, currentTime, 10 ) );
                }
                else if ( lvl1Store1st.HasValue && lvl1StoreLast.HasValue )
                {
                    /* ----------------------------------------------------------------------------------------------------------------------------------------------------
                    * This case means we have date in the storage.
                    * 
                    *  1) After testing, it seems like loading Ticks is taking too long. I am moving download of most recent ticks also to background thread.
                    * 
                    * ----------------------------------------------------------------------------------------------------------------------------------------------------
                    */

                    downloadBackward.Add( new Tuple<DateTime, DateTime, int>( lvl1StoreLast.Value, DateTime.UtcNow, 10 ) );

                    //var today = DateTime.UtcNow;

                    //downloadForward.Add( new Tuple<DateTime, DateTime>( today.Date, today ) );

                    //if ( today.Date.AddTicks( -1 )  > lvl1StoreLast.Value )
                    //{
                    //    downloadBackward.Add( new Tuple<DateTime, DateTime, int>( lvl1StoreLast.Value, today.Date.AddTicks( -1 ), 10 ) );
                    //}


                    /* --------------------------------------------------------------------------------------------------------------------------                             
                    * 
                    *  2) We want to check that whatever in our storage is of continous nature and refill those that are missing data. 
                    * 
                    * --------------------------------------------------------------------------------------------------------------------------
                    */

                    if ( missingDates != null )
                    {
                        var closestFirst = missingDates.OrderByDescending( x => x.Min );

                        int proirity = 15;

                        foreach ( var missingDate in closestFirst )
                        {
                            downloadBackward.Add( new Tuple<DateTime, DateTime, int>( missingDate.Min, missingDate.Max, proirity++ ) );
                        }
                    }


                    /* --------------------------------------------------------------------------------------------------------------------------                             
                    * 
                    *  3) We want to download from the first item in Storage all the way back to the earliest.
                    * 
                    * --------------------------------------------------------------------------------------------------------------------------
                    */
                    DateTime fromTime = taskBegin ?? CandlesFromDate.Value;

                    if ( CandlesFromDate.Value < fromTime )
                    {
                        fromTime = CandlesFromDate.Value;
                    }

                    DateTime toTime = lvl1Store1st.Value;

                    if ( fromTime < toTime )
                    {
                        downloadBackward.Add( new Tuple<DateTime, DateTime, int>( fromTime, toTime, 50 ) );
                    }
                }

                foreach ( Tuple<DateTime, DateTime> period in downloadForward )
                {
                    long nextId = _connector.TransactionIdGenerator.GetNextId();

                    var msg           = new MarketDataMessage();
                    msg.SecurityId    = security.ToSecurityId();
                    msg.DataType      = MarketDataTypes.Level1;
                    msg.From          = new DateTimeOffset?( period.Item1 );
                    msg.To            = new DateTimeOffset?( period.Item2 );
                    msg.IsSubscribe   = true;                    
                    msg.TransactionId = nextId;

                    MarketDataMessage marketDataMessage = msg.ValidateBounds();
                    _connector.SendInMessage( marketDataMessage );
                }


                foreach ( Tuple<DateTime, DateTime, int> period in downloadBackward )
                {
                    var from = new DateTimeOffset?( period.Item1 );
                    var to = new DateTimeOffset?( period.Item2 );
                    var priority = period.Item3;

                    _longRunningCandlesTask.SafeAdd( security ).Add( new LongDownloadTaskInfo( true, from, to, priority ) );
                }
            }
        }


        private void ProcessDataTypeSubscription( Action<Security, DateTimeOffset?, DateTimeOffset?> subscribe, Security security, HydraTaskSecurity map, Messages.DataType dataType, DateTime? taskSettingsStartDate )
        {
            if ( subscribe == null )
            {
                throw new ArgumentNullException( nameof( subscribe ) );
            }
                
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

        private IEnumerable<Messages.DataType> GetDataTypes(
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
                return Enumerable.Empty<Messages.DataType>();
            taskSecurity = _securityMap.TryGetValue( security );
            if ( taskSecurity != null )
                return taskSecurity.GetDataTypes();
            return _associatedSecurityCodes.TryGetValue( security.Code )?.GetDataTypes() ?? Enumerable.Empty<Messages.DataType>();
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
            IsDownloadNews = storage.GetValue( "IsDownloadNews", IsDownloadNews );
            CandlesFromDate = storage.GetValue( "CandlesFromDate", CandlesFromDate );
            TicksFromDate = storage.GetValue( "TicksFromDate", TicksFromDate );
            UpdateSecurities = storage.GetValue( "UpdateSecurities", UpdateSecurities );
            ResumeDownload = storage.GetValue( "ResumeDownload", true );
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
