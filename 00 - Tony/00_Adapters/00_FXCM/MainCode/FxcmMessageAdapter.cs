namespace StockSharp.FxConnectFXCM
{
	

	using System;
	using System.Collections.Generic; 

	using Ecng.Common;

	using StockSharp.Messages;
	using StockSharp.Localization;
    using System.Threading;
    using Ecng.Collections;
    using fxcore2;
    using System.Security;
    using StockSharp.Logging;
    using System.Linq;
    using StockSharp.BusinessEntities;
    using System.ComponentModel;
    using StockSharp.FxConnectFXCM.MainManager;
    using Ecng.ComponentModel;
    using StockSharp.FxConnectFXCM.Freemind;
    using StockSharp.Algo;
    
    using StockSharp.Algo.Candles;

    /* ---------------------------------------------------------------------------------------------------------------------------
    *  Connector 
    *   --> OfflineMessageAdapter
    *       --> FilteredMarketDepthAdapter
    *           --> BasketMessageAdapter     
    *               --> HeartbeatMessageAdapter
    *                   --> SecurityMappingMessageAdapter
    *                       --> SubscriptionMessageAdapter
    *                           --> CandleHolderMessageAdapter
    *                               --> OrderLogMessageAdapter
    *                                   --> CommissionMessageAdapter
    *                                       --> SlippageMessageAdapter
    *                                           --> SecurityNativeIdMessageAdapter
    *                                               --> LatencyMessageAdapter
    *                                                   --> FxcmFxconnectMessageAdapter
    *                                                                  
    * ---------------------------------------------------------------------------------------------------------------------------*/

    [MediaIcon( "fxcm_logo.png" )]
	[OrderCondition( typeof( FxcmOrderCondition ) )]
	[MessageAdapterCategory( MessageAdapterCategories.FX |
							 MessageAdapterCategories.History |
							 MessageAdapterCategories.RealTime |
							 MessageAdapterCategories.Free |
							 MessageAdapterCategories.Candles |
							 MessageAdapterCategories.Level1 |
							 MessageAdapterCategories.Transactions )]
	[DisplayNameLoc( "FxConnectFXCM" )]	
	[DescriptionLoc( LocalizedStrings.Str1770Key, "FxConnectFXCM" )]
	[CategoryLoc( LocalizedStrings.ForexKey )]	
	public partial class FxConnectFxcmMsgAdapter : MessageAdapter, ILoginPasswordAdapter, IAddressAdapter<Uri>, IDemoAdapter, ITokenAdapter
	{
        #region Variables
        public enum StateMachine
		{
			IDLE = 0,
			LOOKUP_SECURITIES = 1,
			SECURITY_LOOKUP_DONE = 2,
			WAIT_FOR_LOOKUP_DONE = 3
		}

        private PortfolioLookupMessage _lookupMsg;

		private StateMachine _currentState = StateMachine.IDLE;

		private FxConnectionManager _connectionManager;
		private readonly SyncObject _timeSync = new SyncObject( );
		private Timer _minuteTimer;

		private bool _timerStarted = false;

		public static readonly Uri DefaultAddress = "http://www.fxcorporate.com/Hosts.jsp".To<Uri>( );

		private readonly SynchronizedDictionary<string, SynchronizedDictionary<string, RefPair<decimal, decimal?>>> _accountsTrades           = new SynchronizedDictionary<string, SynchronizedDictionary<string, RefPair<decimal, decimal?>>>( );
		private readonly SynchronizedDictionary<string, long>                                                       _requestIdToTransactionId = new SynchronizedDictionary<string, long>( );
		private SynchronizedDictionary<string, Tuple<ISubscriptionMessage, Action<ISubscriptionMessage, O2GResponse>>> _requestIdToHandlerDict;
		private readonly SynchronizedDictionary<long, string>                                                       _accountIdToName          = new SynchronizedDictionary<long, string>( );
		private SynchronizedDictionary< ( string, TimeSpan ), ( MarketDataMessage, DateTime ) > _candlesLiveDownload = new SynchronizedDictionary< ( string, TimeSpan ), ( MarketDataMessage, DateTime ) >( );

		private static readonly Dictionary<TimeSpan, string> fxcmPeriodString = new Dictionary<TimeSpan, string>( ) {
																														{ TimeSpan.FromMinutes(1.0)          , "m1"  },
																														{ TimeSpan.FromMinutes(5.0)          , "m5"  },
																														{ TimeSpan.FromMinutes(15.0)         , "m15" },
																														{ TimeSpan.FromMinutes(30.0)         , "m30" },
																														{ TimeSpan.FromHours(1.0)            , "H1"  },
																														{ TimeSpan.FromHours(2.0)            , "H2"  },
																														{ TimeSpan.FromHours(3.0)            , "H3"  },
																														{ TimeSpan.FromHours(4.0)            , "H4"  },
																														{ TimeSpan.FromHours(6.0)            , "H6"  },
																														{ TimeSpan.FromHours(8.0)            , "H8"  },
																														{ TimeSpan.FromDays(1.0)             , "D1"  },
																														{ TimeSpan.FromTicks(6048000000000L) , "W1"  },
																														{ TimeSpan.FromTicks(25920000000000L), "M1"  }
																													};
		//private static bool                  _isLoaded;
		//private DateTimeOffset? _lastTimeBalanceCheck;
		//      private bool _lookupPortfolio = false;

		//private long _lastMyTradeId;

		private O2GSession                    _fxSessionId;
		private Uri                           _address         = DefaultAddress;
		private bool                          _isDemo;
		private string                        _login;
		private SecureString                  _password;		
		private string                        _proxyIP         = "";
		private int                           _proxyPort       = 0;
		private SecureString                  _token;
		private PriceHistoryResponseListener  _pricelistener   = null;
		Func< bool, bool >                    _canProcess      = null;
  
		private readonly Dictionary<long, RefPair<long, decimal>> _orderInfo = new Dictionary<long, RefPair<long, decimal>>();

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="FxConnectFxcmMsgAdapter"/>.
        /// </summary>
        /// <param name="transactionIdGenerator">Transaction id generator.</param>
        public FxConnectFxcmMsgAdapter(IdGenerator transactionIdGenerator) : base(transactionIdGenerator)
		{
			this.AddMarketDataSupport( );
			this.AddTransactionalSupport( );
			this.RemoveSupportedMessage( MessageTypes.Portfolio );			
			this.AddSupportedMarketDataType( DataType.Level1 );
			this.AddSupportedMarketDataType( DataType.CandleTimeFrame );
			this.AddSupportedResultMessage( MessageTypes.SecurityLookup );
			this.AddSupportedResultMessage( MessageTypes.PortfolioLookup );
			this.AddSupportedResultMessage( MessageTypes.OrderStatus );
			this.AddSupportedResultMessage( MessageTypes.OrderGroupCancel );
			
			_connectionManager = null;			
		}

		static string TimeSpanToString( TimeSpan arg ) => arg.ToString().Replace( ':', '-' );
		static TimeSpan StringToTimeSpan( string str ) => str.Replace( '-', ':' ).To<TimeSpan>();

		//static FxConnectFxcmMsgAdapter()
  //      {
		//	MessageConverterHelper.RegisterCandle( () => new fxTimeFrameCandle(), () => new fxTimeFrameCandleMessage() );

		//	StockSharp.Messages.Extensions.RegisterCandleType( typeof( fxTimeFrameCandleMessage ), ( MessageTypes ) fxMessageTypes.fxCandleTimeFrame, ( MarketDataTypes ) fxMarketDataType.fxCandleTimeFrame, typeof( fxTimeFrameCandleMessage ).Name.Remove( nameof( Message ) ), StringToTimeSpan, TimeSpanToString );
		//}

		protected override bool OnSendInMessage( Message message )
		{
			switch ( message.Type )
			{
				case MessageTypes.OrderRegister:
				{
					this.AddInfoLog( "MessageTypes.OrderRegister Received" );
					ProcessOrderRegisterMessage( ( OrderRegisterMessage ) message );
				}
				break;

				case MessageTypes.OrderReplace:
				{
					this.AddInfoLog( "MessageTypes.OrderReplace Received" );
					ProcessOrderReplaceMessage( ( OrderReplaceMessage ) message );
				}
				break;

				case MessageTypes.OrderCancel:
				{
					this.AddInfoLog( "MessageTypes.OrderCancel Received" );
					ProcessOrderCancelMessage( ( OrderCancelMessage ) message );
				}
				break;

				case MessageTypes.OrderGroupCancel:
				{
					this.AddInfoLog( "MessageTypes.OrderGroupCancel Received" );
					ProcessOrderGroupCancel( ( OrderGroupCancelMessage ) message );
					break;
				}

				case MessageTypes.MarketData:
				{
					this.AddInfoLog( "MessageTypes.MarketDataMessage Received" );
					ProcessMarketDataMessage( ( MarketDataMessage ) message );
				}
				break;

				case MessageTypes.Connect:
				{
					this.AddInfoLog( "MessageTypes.Connect Received" );
					ProcessConnectMessage( );
				}
				break;

				case MessageTypes.Disconnect:
				{
					_currentState = StateMachine.IDLE;
					this.AddInfoLog( "MessageTypes.Disconnect Received" );
					Logout( );
				}
				break;

				case MessageTypes.SecurityLookup:
				{
					this.AddInfoLog( "MessageTypes.SecurityLookup Received" );

					if ( _currentState == StateMachine.IDLE || _currentState == StateMachine.SECURITY_LOOKUP_DONE )
                    {
						_currentState = StateMachine.LOOKUP_SECURITIES;
						ProcessSecurityLookupMessage( ( SecurityLookupMessage ) message );
					}
					else
                    {
						this.AddInfoLog( "MessageTypes.SecurityLookup Received Again, we are already in the middle of security Lookup" );
					}										
				}
				break;

				case MessageTypes.PortfolioLookup:
				{
					if ( _currentState == StateMachine.IDLE  )
                    {
						this.AddInfoLog( "MessageTypes.PortfolioLookup Received, StateMachine.IDLE" );
					}
					else if ( _currentState == StateMachine.SECURITY_LOOKUP_DONE )
					{
						this.AddInfoLog( "MessageTypes.PortfolioLookup Received, SECURITY_LOOKUP_DONE, process portfolio lookup" );
						ProcessPortfolioLookupMessage( ( PortfolioLookupMessage ) message );
					}
					else
					{
						this.AddInfoLog( "MessageTypes.PortfolioLookup Received, WAIT_FOR_LOOKUP_DONE, wait for SYMBOLS before portfolio lookup" );

						_currentState = StateMachine.WAIT_FOR_LOOKUP_DONE;
						_lookupMsg = ( PortfolioLookupMessage ) message;
					}


				}
				break;

				case MessageTypes.OrderStatus:
				{
					this.AddInfoLog( "MessageTypes.OrderStatus Received" );
					ProcessOrderStatusMessage( ( OrderStatusMessage ) message );
				}
				break;

				case MessageTypes.Reset:
				{
					this.AddInfoLog( "MessageTypes.Reset Received" );
					ProcessResetMessage( );
				}
				break;

				case MessageTypes.Time:
                {
					this.AddInfoLog( "MessageTypes.Time Received" );

					var timeMsg = (TimeMessage)message;

					var now = DateTime.UtcNow;
					var diff = now - _lastOfferUpdateTime;

					if ( diff.TotalMinutes > 5 )
                    {
						this.AddWarningLog( "No data for over 5 minutes, Resetting the connection" );
						Reset( );
                    }					
				}
				break;

				default:
                {
					this.AddInfoLog( "Unsupported message Type --> {0} will not be processed", message.Type.ToString() );
					return false;
				}
					
			}

			return true;
		}

		public override bool IsReplaceCommandEditCurrent
		{
			get
			{
				return true;
			}
		}

		public override bool IsAllDownloadingSupported( DataType dataType )
		{
			if ( !( dataType == DataType.Securities ) )
				return base.IsAllDownloadingSupported( dataType );
			return true;

			//if ( dataType != DataType.Securities )
   //         {
			//	return base.IsAllDownloadingSupported( dataType );
			//}
			//	
			//return true;
			//=> dataType == DataType.Securities || base.IsAllDownloadingSupported( dataType );
		}

		public new void SendOutMessage( Message msg )
		{
            base.SendOutMessage( msg );
		}

		private void StopTimer()
        {
			_minuteTimer.Dispose( );

			_minuteTimer = null;
        }

		private void StartTimer( )
		{
			if ( _minuteTimer != null )
			{
				return;
			}

			var time           = TimeHelper.Now;
			var lastUpdateTime = TimeHelper.Now;

			var sync           = new SyncObject( );
			var isProcessing   = false;

			_minuteTimer = ThreadingHelper.Timer( ( ) =>
														{
															lock ( sync )
															{
																if ( isProcessing )
																{
																	return;
																}
																isProcessing = true;
															}

															try
															{
																var now = TimeHelper.Now;
																var diff = now - time;

																if ( now - lastUpdateTime >= TimeSpan.FromSeconds( 30 ) )
																{
																	if ( ! _isReloading )
                                                                    {
																		if ( UpdateLatestCandle( DateTime.UtcNow ) )
																		{
																			lastUpdateTime = now;
																		}
																	}
																	
																}

																time = now;
															}
															catch ( Exception ex )
															{
																ex.LogError( );
															}
															finally
															{
																lock ( sync )
																{
																	isProcessing = false;
																}
															}
														}
												).Interval( TimeSpan.FromSeconds( 30 ) );
		}

		private bool UpdateLatestCandle( DateTime now )
		{
			List< ( string, TimeSpan ) > tickList = new List< ( string, TimeSpan ) >( );

			foreach ( var symbolTF in _candlesLiveDownload.Keys.ToList( ) )
			{
				var symbolPayload = _candlesLiveDownload[ symbolTF ];

				var period = symbolTF.Item2;

				DateTime lastDownload = DateTime.MinValue;

				if ( period == TimeSpan.FromTicks( 1 ) )
				{
					tickList.Add( symbolTF );
				}
				else
				{
					lastDownload = DownloadLastestFewBars( symbolTF, symbolPayload, now );

					if ( lastDownload == DateTime.MinValue )
					{
						return false;
					}

					if ( lastDownload > symbolPayload.Item2 )
					{
						_candlesLiveDownload[ symbolTF ] = ( symbolPayload.Item1,  lastDownload );
					}
				}
			}

			foreach ( var tickTF in tickList )
			{
				var tickPayload = _candlesLiveDownload[ tickTF ];

				var period   = tickTF.Item2;

				var lastTick = DateTime.MinValue;

				lastTick = DownloadLastestFewTicks( tickTF, tickPayload, now );

				if ( lastTick > tickPayload.Item2 )
				{
					if ( lastTick.Date < now.Date )
					{

					}

					_candlesLiveDownload[ tickTF ] = (tickPayload.Item1, lastTick); ;
				}
			}

			return true;
		}		

		public override bool IsNativeIdentifiers
		{
			get
			{
				return true;
			}
		}


		[Browsable( false )]
		public override bool IsSupportCandlesUpdates
		{
			get
			{
				return true;
			}
		}

		

		

		private void Logout( )
		{
			var session = GetSession( );

			if ( session != null )
			{
				session.logout( );
			}
			
		}

		private void ProcessPortfolioLookupMessage( PortfolioLookupMessage msg )
		{
			_subAccountsRepo = GFMgr.GetOrCreateSubAccountsRepo( Login );
			SetupTableResponseHandler( O2GTableType.Accounts, msg, null, new Action<ISubscriptionMessage, O2GResponse>( OnAccountsTableLoaded ) );
			
		}

		private void ProcessSecurityLookupMessage( SecurityLookupMessage msg )
		{
			SetupTableResponseHandler( O2GTableType.Offers, msg, null, new Action<ISubscriptionMessage, O2GResponse>( OnOfferTableLoaded ) );
		}

		

		public O2GSession GetSession( )
		{
			O2GSession o2Gsession0 = _fxSessionId;
			if ( o2Gsession0 != null )
			{
				return o2Gsession0;
			}
			else
            {
				Reset( );
				SendOutMessage( new DisconnectMessage( ) );
			}
			

			return null;

			throw new InvalidOperationException( LocalizedStrings.Str1856 );
		}

		public void Reset( )
		{			
			_currentState = StateMachine.IDLE;

			try
			{
				if ( _fxSessionId != null )
				{
					_fxSessionId.unsubscribeResponse( _pricelistener );

					_fxSessionId.Dispose( );
					_fxSessionId = null;
				}

				if ( _timerStarted )
				{
					StopTimer( );

					_timerStarted = false;
				}

				if ( _connectionManager != null )
				{
					_connectionManager.Reset( );

					_connectionManager = null;
				}
			}
			catch ( Exception ex )
			{
				SendOutError( ex );
			}

			
			_pricelistener = null;
			_requestIdToHandlerDict = null;

			_requestIdToTransactionId.Clear( );
			_accountsTrades.Clear( );

			
			_canProcess = null;

            _subAccountsRepo = null;			
		}

		private void SetupTableResponseHandler(
												  O2GTableType table,
												  ISubscriptionMessage msg,
												  string account,
												  Action<ISubscriptionMessage, O2GResponse> responseHandler
											  )
		{
			if ( responseHandler == null )
			{
				throw new ArgumentNullException( "processResponse" );
			}

			O2GSession o2Gsession = GetSession( );

			if( o2Gsession == null )
				return;
			

			O2GLoginRules loginRules = o2Gsession.getLoginRules( );

			//if ( loginRules.isTableLoadedByDefault( table ) )
			//{
			//	responseHandler( msg, loginRules.getTableRefreshResponse( table ) );
			//}
			//else
			{
				O2GRequestFactory requestFactory = o2Gsession.getRequestFactory( );
				if ( requestFactory == null )
				{
					throw new InvalidOperationException( );
				}

				O2GRequest o2Grequest = StringHelper.IsEmpty( account ) ? requestFactory.createRefreshTableRequest( table ) : requestFactory.createRefreshTableRequestByAccount( table, account );

				if ( o2Grequest == null )
				{
					throw new InvalidOperationException( requestFactory.getLastError( ) );
				}

				AddTableResponseHandler( o2Grequest.RequestID, msg, responseHandler );
				o2Gsession.sendRequest( o2Grequest );
			}
		}

		private void AddTableResponseHandler( string requestId, ISubscriptionMessage msg, Action<ISubscriptionMessage, O2GResponse> handler )
		{
			if ( handler == null )
			{
				throw new ArgumentNullException( "processResponse" );
			}


			if ( _requestIdToHandlerDict == null )
			{
                _requestIdToHandlerDict = new SynchronizedDictionary< string, Tuple< ISubscriptionMessage, Action< ISubscriptionMessage, O2GResponse > > >( );
				//throw new InvalidOperationException( LocalizedStrings.Str1856 );
			}

			_requestIdToHandlerDict.Add( requestId, Tuple.Create( msg, handler ) );
		}


		private void ProcessConnectMessage( )
		{
			if ( _fxSessionId != null )
			{
				throw new InvalidOperationException( LocalizedStrings.Str1619 );
			}

			_connectionManager = new FxConnectionManager( this );

			ServicesRegistry.LogManager.Sources.Add( _connectionManager );

			_fxSessionId = O2GTransport.createSession( );
					
			_requestIdToHandlerDict = new SynchronizedDictionary<string, Tuple<ISubscriptionMessage, Action<ISubscriptionMessage, O2GResponse>>>( StringComparer.InvariantCultureIgnoreCase );
						
			_fxSessionId.subscribeResponse( new FxcmResponseListener( this ) );
			_pricelistener               = new PriceHistoryResponseListener( _fxSessionId );

			_fxSessionId.subscribeResponse( _pricelistener );

			_connectionManager.Login( _fxSessionId, Login, Password.To<string>( ), Address.To<string>( ), IsDemo ? "Demo" : "Real", Proxy, ProxyPort );
		}


		public void ProcessConnectedToServer()
        {
			SendOutMessage( new ConnectMessage( ) );

			if ( _candlesLiveDownload.Count > 0 && _timerStarted == false )
            {
				StartTimer( );
            }
		}

		private void ProcessResetMessage( )
		{
			Reset( );

			SendOutMessage( new ResetMessage( ) );
		}

		

		/// <inheritdoc />
		public override TimeSpan GetHistoryStepSize(DataType dataType, out TimeSpan iterationInterval)
		{
			var step = base.GetHistoryStepSize(dataType, out iterationInterval);
			
			if (dataType == DataType.Ticks)
				step = TimeSpan.FromDays(1);

			return step;
		}

		//private void SubscribePusherClient()
		//{
		//	_pusherClient.Connected += SessionOnPusherConnected;
		//	_pusherClient.Disconnected += SessionOnPusherDisconnected;
		//	_pusherClient.Error += SessionOnPusherError;
		//	_pusherClient.NewOrderBook += SessionOnNewOrderBook;
		//	_pusherClient.NewOrderLog += SessionOnNewOrderLog;
		//	_pusherClient.NewTrade += SessionOnNewTrade;
		//}

		//private void UnsubscribePusherClient()
		//{
		//	_pusherClient.Connected -= SessionOnPusherConnected;
		//	_pusherClient.Disconnected -= SessionOnPusherDisconnected;
		//	_pusherClient.Error -= SessionOnPusherError;
		//	_pusherClient.NewOrderBook -= SessionOnNewOrderBook;
		//	_pusherClient.NewOrderLog -= SessionOnNewOrderLog;
		//	_pusherClient.NewTrade -= SessionOnNewTrade;
		//}

		/// <inheritdoc />
		public override string FeatureName => "";

		/// <inheritdoc />

		//private void SessionOnPusherConnected()
		//{
		//	SendOutMessage(new ConnectMessage());
		//}

		//private void SessionOnPusherError(Exception exception)
		//{
		//	SendOutError(exception);
		//}

		//private void SessionOnPusherDisconnected(bool expected)
		//{
		//	SendOutDisconnectMessage(expected);
		//}

		private void ProcessRequestCompleted( O2GResponse response )
		{
			var o2Gsession = GetSession( );
			if ( o2Gsession == null )
				return;

			O2GResponseReaderFactory factory = o2Gsession.getResponseReaderFactory( );

			if ( factory == null )
			{
				if ( _currentState != StateMachine.IDLE )
                {
					string error = "Response Reader Factory is empty";
					SendOutError( error );
				}
				
				return;
			}			
			
			if ( _requestIdToHandlerDict == null )
			{
				throw new InvalidOperationException( LocalizedStrings.Str1856 );
			}

			var handler = _requestIdToHandlerDict.TryGetAndRemove( response.RequestID );

			if ( handler == null )
			{
				return;
			}

			this.AddInfoLog( "RequestID = {0}", response.RequestID );

			handler.Item2( handler.Item1, response );
		}

		private void ProcessRequestFailed( string requestId, string error )
		{
			if ( error.ContainsIgnoreCase( "unsupported scope" ) )
			{
				return;
			}

			long? transId = _requestIdToTransactionId.TryGetAndRemove( requestId );

			if ( !transId.HasValue )
			{
				SendOutError( error );
			}
			else
			{
				var exeMsg = new ExecutionMessage( );

				exeMsg.DataTypeEx = DataType.Transactions;
				exeMsg.HasOrderInfo = true;
				exeMsg.OriginalTransactionId = transId.Value;
				exeMsg.OrderState = OrderStates.Failed;
				exeMsg.Error = new InvalidOperationException( error );

				SendOutMessage( exeMsg );
			}
		}

		private void ProcessTableUpdate( O2GResponse response )
		{
			var o2Gsession = GetSession( );
			if ( o2Gsession == null )
				return;

			O2GResponseReaderFactory factory = o2Gsession.getResponseReaderFactory( );

			if ( factory == null )
			{
				if ( _currentState != StateMachine.IDLE )
				{
					string error = "Response Reader Factory is empty";

					this.AddErrorLog( error );					

					SendOutMessage( new ResetMessage( ) );
				}
				
				return;
			}

			O2GTablesUpdatesReader reader = factory.createTablesUpdatesReader( response );

			for ( int index = 0; index < reader.Count; ++index )
			{
				var tableType = reader.getUpdateTable( index );
				var updateType = reader.getUpdateType( index );

				switch ( tableType )
				{					
					case O2GTableType.Offers:
                    {
						OnOfferTableUpdate( reader.getOfferRow( index ), updateType, 0L );
					}
					break;

					case O2GTableType.Accounts:
                    {
						OnAccountsTableUpdate( reader.getAccountRow( index ), updateType );
					}
					break;

					case O2GTableType.Orders:
					{												
						OnOrdersTableUpdate( reader.getOrderRow( index ), updateType );
					}
					break;

					case O2GTableType.Trades:
					{
						OnTradesTableUpdate( reader.getTradeRow( index ), updateType, reader.ServerTime );
					}
					break;

					case O2GTableType.ClosedTrades:
					{
						OnClosedTradesTableUpdate( reader.getClosedTradeRow( index ), updateType, reader.ServerTime );
					}
					break;

					case O2GTableType.Messages:
                    {
						OnMessagesTableUdate( reader.getMessageRow( index ), null );
					}
					break;

					default:
                    {

                    }
                    break;
				}
			}
		}

		

		

		



		private string GetAccountNameFromId( long accountId )
		{
			string str;
			if ( _accountIdToName.TryGetValue( accountId, out str ) )
			{
				return str;
			}

			return "0" + accountId;
		}

		



		//private sealed class FxcmSessionStatus : IO2GSessionStatus
		//{
		//	private readonly FxConnectFxcmMsgAdapter _msgAdapter;

		//	public FxcmSessionStatus( FxConnectFxcmMsgAdapter adapter )
		//	{
		//		if ( adapter == null )
		//		{
		//			throw new ArgumentNullException( "adapter" );
		//		}

		//		_msgAdapter = adapter;
		//	}

		//	

		//	void IO2GSessionStatus.onSessionStatusChanged( O2GSessionStatusCode code )
		//	{
		//		switch ( code )
		//		{
		//			case O2GSessionStatusCode.Disconnected:
		//			{
		//				_msgAdapter.Reset( );
		//				_msgAdapter.SendOutMessage( new DisconnectMessage( ) );
		//			}
		//			break;

		//			case O2GSessionStatusCode.Connecting:
		//				break;

		//			case O2GSessionStatusCode.TradingSessionRequested:
		//			{
		//				var o2Gsession = _msgAdapter.GetSession( );

		//				using ( var enumerator = ( ( IEnumerable<O2GSessionDescriptor> ) o2Gsession.getTradingSessionDescriptors( ) ).GetEnumerator( ) )
		//				{
		//					while ( enumerator.MoveNext( ) )
		//					{
		//						O2GSessionDescriptor current = enumerator.Current;
		//						if ( current.RequiresPin )
		//						{
		//							o2Gsession.setTradingSession( current.Id, _msgAdapter.Pin );
		//						}
		//					}
		//					break;
		//				}
		//			}


		//			case O2GSessionStatusCode.Connected:
		//			{
		//				_msgAdapter.SendOutMessage( new ConnectMessage( ) );
		//			}
		//			break;

		//			case O2GSessionStatusCode.Reconnecting:
		//				break;

		//			case O2GSessionStatusCode.Disconnecting:
		//				break;

		//			case O2GSessionStatusCode.SessionLost:
		//			{
		//				var msg = new ConnectMessage( );
		//				msg.Error = new InvalidOperationException( code.ToString( ) );
		//				_msgAdapter.SendOutMessage( msg );
		//			}
		//			break;

		//			case O2GSessionStatusCode.PriceSessionReconnecting:
		//				break;

		//			case O2GSessionStatusCode.Unknown:
		//			{
		//				var UMsg = new ConnectMessage( );
		//				UMsg.Error = new InvalidOperationException( code.ToString( ) );
		//				_msgAdapter.SendOutMessage( UMsg );
		//			}
		//			break;

		//			default:
		//				throw new ArgumentOutOfRangeException( "status", code, null );
		//		}
		//	}

		//	void IO2GSessionStatus.onLoginFailed( string failedString )
		//	{
		//		ConnectMessage msg = new ConnectMessage( );
		//		msg.Error = new InvalidOperationException( failedString );
		//		_msgAdapter.SendOutMessage( msg );
		//	}
		//}

		private sealed class FxcmResponseListener : IO2GResponseListener
		{
			private readonly FxConnectFxcmMsgAdapter _msgAdapter;

			public FxcmResponseListener( FxConnectFxcmMsgAdapter adapter )
			{
				FxConnectFxcmMsgAdapter FxConnectFxcmMsgAdapter = adapter;
				if ( FxConnectFxcmMsgAdapter == null )
				{
					throw new ArgumentNullException( "adapter" );
				}

				_msgAdapter = FxConnectFxcmMsgAdapter;
			}

			void IO2GResponseListener.onRequestCompleted( string requestId, O2GResponse response )
			{
				_msgAdapter.ProcessRequestCompleted( response );
			}

			void IO2GResponseListener.onRequestFailed( string requestId, string error )
			{
				_msgAdapter.ProcessRequestFailed( requestId, error );
			}

			void IO2GResponseListener.onTablesUpdates( O2GResponse o2GResponse_0 )
			{
				_msgAdapter.ProcessTableUpdate( o2GResponse_0 );
			}
		}

	}
}