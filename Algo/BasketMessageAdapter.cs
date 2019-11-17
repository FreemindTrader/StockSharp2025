#region S# License
/******************************************************************************************
NOTICE!!!  This program and source code is owned and licensed by
StockSharp, LLC, www.stocksharp.com
Viewing or use of this code requires your acceptance of the license
agreement found at https://github.com/StockSharp/StockSharp/blob/master/LICENSE
Removal of this comment is a violation of the license agreement.

Project: StockSharp.Algo.Algo
File: BasketMessageAdapter.cs
Created: 2015, 11, 11, 2:32 PM

Copyright 2010 by StockSharp, LLC
*******************************************************************************************/
#endregion S# License
namespace StockSharp.Algo
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Ecng.Collections;
	using Ecng.Common;
	using Ecng.Serialization;

	using MoreLinq;

	using StockSharp.Algo.Candles;
	using StockSharp.Algo.Candles.Compression;
	using StockSharp.Algo.Commissions;
	using StockSharp.Algo.Latency;
	using StockSharp.Algo.PnL;
	using StockSharp.Algo.Slippage;
	using StockSharp.Algo.Storages;
	using StockSharp.Logging;
	using StockSharp.Messages;
	using StockSharp.Localization;

	/// <summary>
	/// The interface describing the list of adapters to trading systems with which the aggregator operates.
	/// </summary>
	public interface IInnerAdapterList : ISynchronizedCollection<IMessageAdapter>, INotifyList<IMessageAdapter>
	{
		/// <summary>
		/// Internal adapters sorted by operation speed.
		/// </summary>
		IEnumerable<IMessageAdapter> SortedAdapters { get; }

		/// <summary>
		/// The indexer through which speed priorities (the smaller the value, then adapter is faster) for internal adapters are set.
		/// </summary>
		/// <param name="adapter">The internal adapter.</param>
		/// <returns>The adapter priority. If the -1 value is set the adapter is considered to be off.</returns>
		int this[IMessageAdapter adapter] { get; set; }
	}

	/// <summary>
	/// Adapter-aggregator that allows simultaneously to operate multiple adapters connected to different trading systems.
	/// </summary>
	public class BasketMessageAdapter : MessageAdapter
	{
		private sealed class InnerAdapterList : CachedSynchronizedList<IMessageAdapter>, IInnerAdapterList
		{
			private readonly BasketMessageAdapter _parent;
			private readonly Dictionary<IMessageAdapter, int> _enables = new Dictionary<IMessageAdapter, int>();

			public InnerAdapterList(BasketMessageAdapter parent)
			{
				_parent = parent ?? throw new ArgumentNullException(nameof(parent));
			}

			public IEnumerable<IMessageAdapter> SortedAdapters => Cache.Where(t => this[t] != -1).OrderBy(t => this[t]);

			protected override bool OnAdding(IMessageAdapter item)
			{
				_enables.Add(item, 0);
				return base.OnAdding(item);
			}

			protected override bool OnInserting(int index, IMessageAdapter item)
			{
				_enables.Add(item, 0);
				return base.OnInserting(index, item);
			}

			protected override bool OnRemoving(IMessageAdapter item)
			{
				_enables.Remove(item);
				_parent._activeAdapters.Remove(item);
				return base.OnRemoving(item);
			}

			protected override bool OnClearing()
			{
				_enables.Clear();
				_parent._activeAdapters.Clear();
				return base.OnClearing();
			}

			public int this[IMessageAdapter adapter]
			{
				get
				{
					lock (SyncRoot)
						return _enables.TryGetValue2(adapter) ?? -1;
				}
				set
				{
					if (value < -1)
						throw new ArgumentOutOfRangeException();

					lock (SyncRoot)
					{
						if (!Contains(adapter))
							Add(adapter);

						_enables[adapter] = value;
						//_portfolioTraders.Clear();
					}
				}
			}
		}

		private readonly SynchronizedDictionary<long, MarketDataMessage> _subscriptionMessages = new SynchronizedDictionary<long, MarketDataMessage>();
		private readonly SynchronizedDictionary<long, Tuple<IMessageAdapter, MarketDataMessage>> _subscriptionsById = new SynchronizedDictionary<long, Tuple<IMessageAdapter, MarketDataMessage>>();
		private readonly Dictionary<long, HashSet<IMessageAdapter>> _subscriptionNonSupportedAdapters = new Dictionary<long, HashSet<IMessageAdapter>>();
		private readonly SynchronizedDictionary<Helper.SubscriptionKey, long> _keysToTransId = new SynchronizedDictionary<Helper.SubscriptionKey, long>();
		private readonly SynchronizedDictionary<IMessageAdapter, IMessageAdapter> _activeAdapters = new SynchronizedDictionary<IMessageAdapter, IMessageAdapter>();
		private readonly SyncObject _connectedResponseLock = new SyncObject();
		private readonly Dictionary<MessageTypes, CachedSynchronizedSet<IMessageAdapter>> _messageTypeAdapters = new Dictionary<MessageTypes, CachedSynchronizedSet<IMessageAdapter>>();
		private readonly HashSet<IMessageAdapter> _pendingConnectAdapters = new HashSet<IMessageAdapter>();
		private readonly Queue<Message> _pendingMessages = new Queue<Message>();
		private readonly HashSet<IMessageAdapter> _connectedAdapters = new HashSet<IMessageAdapter>();
		private readonly InnerAdapterList _innerAdapters;
		private readonly SynchronizedDictionary<long, RefTriple<long, bool?, IMessageAdapter>> _newsBoardSubscriptions = new SynchronizedDictionary<long, RefTriple<long, bool?, IMessageAdapter>>();
		private readonly SynchronizedDictionary<Tuple<MessageTypes, long>, HashSet<IMessageAdapter>> _lookups = new SynchronizedDictionary<Tuple<MessageTypes, long>, HashSet<IMessageAdapter>>();

		private readonly SynchronizedDictionary<string, IMessageAdapter> _portfolioAdapters = new SynchronizedDictionary<string, IMessageAdapter>(StringComparer.InvariantCultureIgnoreCase);
		private readonly SynchronizedDictionary<Tuple<SecurityId, MarketDataTypes?>, IMessageAdapter> _securityAdapters = new SynchronizedDictionary<Tuple<SecurityId, MarketDataTypes?>, IMessageAdapter>();
		private readonly SynchronizedSet<long> _subscriptionListRequests = new SynchronizedSet<long>();
		private readonly SynchronizedDictionary<IMessageAdapter, HashSet<string>> _subscribedPortfolios = new SynchronizedDictionary<IMessageAdapter, HashSet<string>>();

		/// <summary>
		/// Adapters with which the aggregator operates.
		/// </summary>
		public IInnerAdapterList InnerAdapters => _innerAdapters;

		private INativeIdStorage _nativeIdStorage = new InMemoryNativeIdStorage();

		/// <summary>
		/// Security native identifier storage.
		/// </summary>
		public INativeIdStorage NativeIdStorage
		{
			get => _nativeIdStorage;
			set => _nativeIdStorage = value ?? throw new ArgumentNullException(nameof(value));
		}

		private ISecurityMappingStorage _securityMappingStorage;

		/// <summary>
		/// Security identifier mappings storage.
		/// </summary>
		public ISecurityMappingStorage SecurityMappingStorage
		{
			get => _securityMappingStorage;
			set => _securityMappingStorage = value ?? throw new ArgumentNullException(nameof(value));
		}

		/// <summary>
		/// Extended info <see cref="Message.ExtensionInfo"/> storage.
		/// </summary>
		public IExtendedInfoStorage ExtendedInfoStorage { get; set; }

		/// <summary>
		/// Orders registration delay calculation manager.
		/// </summary>
		public ILatencyManager LatencyManager { get; set; }

		/// <summary>
		/// The profit-loss manager.
		/// </summary>
		public IPnLManager PnLManager { get; set; }

		/// <summary>
		/// The commission calculating manager.
		/// </summary>
		public ICommissionManager CommissionManager { get; set; }

		/// <summary>
		/// Slippage manager.
		/// </summary>
		public ISlippageManager SlippageManager { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="BasketMessageAdapter"/>.
		/// </summary>
		/// <param name="transactionIdGenerator">Transaction id generator.</param>
		/// <param name="candleBuilderProvider">Candle builders provider.</param>
		public BasketMessageAdapter(IdGenerator transactionIdGenerator, CandleBuilderProvider candleBuilderProvider)
			: this(transactionIdGenerator, new InMemorySecurityMessageAdapterProvider(), new InMemoryPortfolioMessageAdapterProvider(), candleBuilderProvider, null, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BasketMessageAdapter"/>.
		/// </summary>
		/// <param name="transactionIdGenerator">Transaction id generator.</param>
		/// <param name="securityAdapterProvider">The security based message adapter's provider.</param>
		/// <param name="portfolioAdapterProvider">The portfolio based message adapter's provider.</param>
		/// <param name="candleBuilderProvider">Candle builders provider.</param>
		/// <param name="storageRegistry">The storage of market data.</param>
		/// <param name="snapshotRegistry">Snapshot storage registry.</param>
		public BasketMessageAdapter(IdGenerator transactionIdGenerator,
			ISecurityMessageAdapterProvider securityAdapterProvider,
			IPortfolioMessageAdapterProvider portfolioAdapterProvider,
			CandleBuilderProvider candleBuilderProvider,
			IStorageRegistry storageRegistry,
			SnapshotRegistry snapshotRegistry)
			: base(transactionIdGenerator)
		{
			_innerAdapters = new InnerAdapterList(this);
			SecurityAdapterProvider = securityAdapterProvider ?? throw new ArgumentNullException(nameof(securityAdapterProvider));
			PortfolioAdapterProvider = portfolioAdapterProvider ?? throw new ArgumentNullException(nameof(portfolioAdapterProvider));
			CandleBuilderProvider = candleBuilderProvider ?? throw new ArgumentNullException(nameof(portfolioAdapterProvider));

			StorageRegistry = storageRegistry;// ?? throw new ArgumentNullException(nameof(storageRegistry));
			SnapshotRegistry = snapshotRegistry;// ?? throw new ArgumentNullException(nameof(snapshotRegistry));

			LatencyManager = new LatencyManager();
			CommissionManager = new CommissionManager();
			//PnLManager = new PnLManager();
			SlippageManager = new SlippageManager();

			SecurityAdapterProvider.Changed += SecurityAdapterProviderOnChanged;
			PortfolioAdapterProvider.Changed += PortfolioAdapterProviderOnChanged;
		}

		/// <summary>
		/// The portfolio based message adapter's provider.
		/// </summary>
		public IPortfolioMessageAdapterProvider PortfolioAdapterProvider { get; }

		/// <summary>
		/// The security based message adapter's provider.
		/// </summary>
		public ISecurityMessageAdapterProvider SecurityAdapterProvider { get; }

		/// <summary>
		/// Candle builders provider.
		/// </summary>
		public CandleBuilderProvider CandleBuilderProvider { get; }

		/// <inheritdoc />
		public override IEnumerable<MessageTypes> SupportedMessages => GetSortedAdapters().SelectMany(a => a.SupportedMessages).Distinct();

		/// <inheritdoc />
		public override IEnumerable<MessageTypes> SupportedOutMessages => GetSortedAdapters().SelectMany(a => a.SupportedOutMessages).Distinct();

		/// <inheritdoc />
		public override IEnumerable<MarketDataTypes> SupportedMarketDataTypes => GetSortedAdapters().SelectMany(a => a.SupportedMarketDataTypes).Distinct();

		/// <inheritdoc />
		public override bool PortfolioLookupRequired => GetSortedAdapters().Any(a => a.PortfolioLookupRequired);

		/// <inheritdoc />
		public override bool OrderStatusRequired => GetSortedAdapters().Any(a => a.OrderStatusRequired);

		/// <inheritdoc />
		public override bool SecurityLookupRequired => GetSortedAdapters().Any(a => a.SecurityLookupRequired);

		/// <inheritdoc />
		public override bool IsSupportSecuritiesLookupAll => GetSortedAdapters().Any(a => a.IsSupportSecuritiesLookupAll);

		/// <inheritdoc />
		public override MessageAdapterCategories Categories => GetSortedAdapters().Select(a => a.Categories).JoinMask();

		/// <summary>
		/// Restore subscription on reconnect.
		/// </summary>
		/// <remarks>
		/// Error case like connection lost etc.
		/// </remarks>
		public bool IsRestoreSubscriptionOnErrorReconnect { get; set; } = true;

		/// <summary>
		/// Restore subscription on reconnect.
		/// </summary>
		/// <remarks>
		/// Normal case connect/disconnect.
		/// </remarks>
		public bool IsRestoreSubscriptionOnNormalReconnect { get; set; } = true;

		/// <summary>
		/// Suppress reconnecting errors.
		/// </summary>
		public bool SuppressReconnectingErrors { get; set; } = true;

		/// <summary>
		/// Use <see cref="CandleBuilderMessageAdapter"/>.
		/// </summary>
		public bool SupportCandlesCompression { get; set; } = true;

		/// <summary>
		/// Use <see cref="OrderLogMessageAdapter"/>.
		/// </summary>
		public bool SupportBuildingFromOrderLog { get; set; } = true;

		/// <summary>
		/// Use <see cref="OrderBookTruncateMessageAdapter"/>.
		/// </summary>
		public bool SupportOrderBookTruncate { get; set; } = true;

		/// <summary>
		/// Use <see cref="OfflineMessageAdapter"/>.
		/// </summary>
		public bool SupportOffline { get; set; }

		/// <summary>
		/// Do not add extra adapters.
		/// </summary>
		public bool IgnoreExtraAdapters { get; set; }

		/// <summary>
		/// Send lookup messages on connect. By default is <see langword="true"/>.
		/// </summary>
		public bool LookupMessagesOnConnect { get; set; } = true;

		/// <summary>
		/// The storage of market data.
		/// </summary>
		public IStorageRegistry StorageRegistry { get; }

		/// <summary>
		/// Snapshot storage registry.
		/// </summary>
		public SnapshotRegistry SnapshotRegistry { get; }

		/// <summary>
		/// Save data only for subscriptions.
		/// </summary>
		public bool StorageFilterSubscription { get; set; }

		/// <summary>
		/// The storage (database, file etc.).
		/// </summary>
		public IMarketDataDrive StorageDrive { get; set; }

		/// <summary>
		/// Storage mode. By default is <see cref="StorageModes.Incremental"/>.
		/// </summary>
		public StorageModes StorageMode { get; set; } = StorageModes.Incremental;

		/// <summary>
		/// Format.
		/// </summary>
		public StorageFormats StorageFormat { get; set; } = StorageFormats.Binary;

		private TimeSpan _storageDaysLoad;

		/// <summary>
		/// Max days to load stored data.
		/// </summary>
		public TimeSpan StorageDaysLoad
		{
			get => _storageDaysLoad;
			set
			{
				if (value < TimeSpan.Zero)
					throw new ArgumentOutOfRangeException(nameof(value), value, LocalizedStrings.Str1219);

				_storageDaysLoad = value;
			}
		}

		/// <inheritdoc />
		public override IEnumerable<object> GetCandleArgs(Type candleType, SecurityId securityId, DateTimeOffset? from, DateTimeOffset? to)
			=> GetSortedAdapters().SelectMany(a => a.GetCandleArgs(candleType, securityId, from, to)).Distinct().OrderBy();

		/// <inheritdoc />
		public override bool IsSecurityNewsOnly => GetSortedAdapters().All(a => a.IsSecurityNewsOnly);

		/// <summary>
		/// To get adapters <see cref="IInnerAdapterList.SortedAdapters"/> sorted by the specified priority. By default, there is no sorting.
		/// </summary>
		/// <returns>Sorted adapters.</returns>
		protected IEnumerable<IMessageAdapter> GetSortedAdapters() => _innerAdapters.SortedAdapters;

		private void ProcessReset(ResetMessage message)
		{
			_activeAdapters.Values.ForEach(a =>
			{
				a.SendInMessage(message);
				a.Dispose();
			});

			lock (_connectedResponseLock)
			{
				_connectedAdapters.Clear();
				_messageTypeAdapters.Clear();
				_pendingConnectAdapters.Clear();
				_pendingMessages.Clear();
				_subscriptionNonSupportedAdapters.Clear();
			}

			_activeAdapters.Clear();
			_subscriptionsById.Clear();
			_keysToTransId.Clear();
			_subscriptionMessages.Clear();
			_newsBoardSubscriptions.Clear();
			_lookups.Clear();
			_subscriptionListRequests.Clear();
			_subscribedPortfolios.Clear();
		}

		private IMessageAdapter CreateWrappers(IMessageAdapter adapter)
		{
			if (LatencyManager != null)
			{
				adapter = new LatencyMessageAdapter(adapter) { LatencyManager = LatencyManager.Clone(), OwnInnerAdapter = true };
			}

			if (SlippageManager != null)
			{
				adapter = new SlippageMessageAdapter(adapter) { SlippageManager = SlippageManager.Clone(), OwnInnerAdapter = true };
			}

			if (adapter.IsNativeIdentifiers)
			{
				adapter = new SecurityNativeIdMessageAdapter(adapter, NativeIdStorage) { OwnInnerAdapter = true };
			}

			if (SecurityMappingStorage != null)
			{
				adapter = new SecurityMappingMessageAdapter(adapter, SecurityMappingStorage) { OwnInnerAdapter = true };
			}

			if (PnLManager != null && !adapter.IsSupportExecutionsPnL)
			{
				adapter = new PnLMessageAdapter(adapter) { PnLManager = PnLManager.Clone(), OwnInnerAdapter = true };
			}

			if (CommissionManager != null)
			{
				adapter = new CommissionMessageAdapter(adapter) { CommissionManager = CommissionManager.Clone(), OwnInnerAdapter = true };
			}

			if (adapter.IsSupportSubscriptions)
			{
				adapter = new SubscriptionIdMessageAdapter(adapter) { OwnInnerAdapter = true };
			}

			adapter = new PartialDownloadMessageAdapter(adapter) { OwnInnerAdapter = true };

			if (adapter.IsFullCandlesOnly)
			{
				adapter = new CandleHolderMessageAdapter(adapter) { OwnInnerAdapter = true };
			}

			adapter = new LookupTrackingMessageAdapter(adapter) { OwnInnerAdapter = true };

			if (StorageRegistry != null)
			{
				adapter = new StorageMessageAdapter(adapter, StorageRegistry, SnapshotRegistry, CandleBuilderProvider)
				{
					OwnInnerAdapter = true,

					FilterSubscription = StorageFilterSubscription,
					Drive = StorageDrive,
					DaysLoad = StorageDaysLoad,
					Format = StorageFormat,
					Mode = StorageMode,
				};
			}

			if (SupportBuildingFromOrderLog)
			{
				adapter = new OrderLogMessageAdapter(adapter) { OwnInnerAdapter = true };
			}

			if (adapter.IsSupportOrderBookIncrements)
			{
				adapter = new OrderBookInrementMessageAdapter(adapter) { OwnInnerAdapter = true };
			}

			if (SupportOrderBookTruncate)
			{
				adapter = new OrderBookTruncateMessageAdapter(adapter) { OwnInnerAdapter = true };
			}

			if (SupportCandlesCompression)
			{
				adapter = new CandleBuilderMessageAdapter(adapter, CandleBuilderProvider) { OwnInnerAdapter = true };
			}

			if (ExtendedInfoStorage != null && !adapter.SecurityExtendedFields.IsEmpty())
			{
				adapter = new ExtendedInfoStorageMessageAdapter(adapter, ExtendedInfoStorage) { OwnInnerAdapter = true };
			}

			return adapter;
		}

		private readonly Dictionary<IMessageAdapter, bool> _hearbeatFlags = new Dictionary<IMessageAdapter, bool>();

		private bool IsHeartbeatOn(IMessageAdapter adapter)
		{
			return _hearbeatFlags.TryGetValue2(adapter) ?? true;
		}

		/// <summary>
		/// Apply on/off heartbeat mode for the specified adapter.
		/// </summary>
		/// <param name="adapter">Adapter.</param>
		/// <param name="on">Is active.</param>
		public void ApplyHeartbeat(IMessageAdapter adapter, bool on)
		{
			_hearbeatFlags[adapter] = on;
		}

		/// <inheritdoc />
		protected override void OnSendInMessage(Message message)
		{
			if (message.IsBack)
			{
				var adapter = message.Adapter;

				if (adapter == null)
					throw new InvalidOperationException();

				if (adapter == this)
				{
					message.Adapter = null;
					message.IsBack = false;
				}
				else
				{
					adapter.SendInMessage(message);
					return;	
				}
			}

			switch (message.Type)
			{
				case MessageTypes.Reset:
					ProcessReset((ResetMessage)message);
					break;

				case MessageTypes.Connect:
				{
					ProcessReset(new ResetMessage());

					_activeAdapters.AddRange(GetSortedAdapters().ToDictionary(a => a, a =>
					{
						lock (_connectedResponseLock)
							_pendingConnectAdapters.Add(a);

						var wrapper = IgnoreExtraAdapters ? a : CreateWrappers(a);

						var adapter = wrapper;

						if (IsHeartbeatOn(a))
						{
							adapter = new HeartbeatMessageAdapter(adapter)
							{
								SuppressReconnectingErrors = SuppressReconnectingErrors,
								Parent = this,
								OwnInnerAdapter = adapter != a
							};
						}

						if (SupportOffline)
							adapter = new OfflineMessageAdapter(adapter) { OwnInnerAdapter = adapter != a };

						adapter.NewOutMessage += m => OnInnerAdapterNewOutMessage(wrapper, m);
						
						return adapter;
					}));
					
					if (_activeAdapters.Count == 0)
						throw new InvalidOperationException(LocalizedStrings.Str3650);

					_activeAdapters.Values.ForEach(a =>
					{
						var u = a.GetUnderlyingAdapter();
						this.AddInfoLog("Connecting '{0}'.", u);

						a.SendInMessage(message);
					});
					break;
				}

				case MessageTypes.Disconnect:
				{
					lock (_connectedResponseLock)
					{
						_connectedAdapters.ToArray().ForEach(a =>
						{
							var u = a.GetUnderlyingAdapter();
							this.AddInfoLog("Disconnecting '{0}'.", u);

							a.SendInMessage(message);
						});
					}

					break;
				}

				case MessageTypes.Portfolio:
				//case MessageTypes.PortfolioChange:
				{
					var pfMsg = (PortfolioMessage)message;
					ProcessPortfolioMessage(pfMsg.PortfolioName, message);
					break;
				}

				case MessageTypes.OrderRegister:
				case MessageTypes.OrderReplace:
				case MessageTypes.OrderCancel:
				case MessageTypes.OrderGroupCancel:
				{
					var ordMsg = (OrderMessage)message;
					ProcessAdapterMessage(ordMsg.PortfolioName, ordMsg);
					break;
				}

				case MessageTypes.OrderPairReplace:
				{
					var ordMsg = (OrderPairReplaceMessage)message;
					ProcessAdapterMessage(ordMsg.Message1.PortfolioName, ordMsg);
					break;
				}

				case MessageTypes.MarketData:
				{
					ProcessMarketDataRequest((MarketDataMessage)message);
					break;
				}

				case MessageTypes.ChangePassword:
				{
					var adapter = GetSortedAdapters().FirstOrDefault(a => a.SupportedMessages.Contains(MessageTypes.ChangePassword));

					if (adapter == null)
						throw new InvalidOperationException(LocalizedStrings.Str629Params.Put(message.Type));

					adapter.SendInMessage(message);
					break;
				}

				default:
				{
					ProcessOtherMessage(message);
					break;
				}
			}
		}

		private void ProcessOtherMessage(Message message)
		{
			if (message.Adapter != null)
			{
				message.Adapter.SendInMessage(message);
				return;
			}

			var adapters = GetAdapters(message, out var isPended, out _);

			if (isPended)
				return;

			if (adapters.Length == 0)
			{
				if (message.Type.IsLookup())
					SendOutMessage(message.Type.ToResultType().CreateLookupResult(((ITransactionIdMessage)message).TransactionId));
			}
			else
			{
				var key = message.Type.IsLookup()
					? Tuple.Create(message.Type.ToResultType(), ((ITransactionIdMessage)message).TransactionId)
					: null;

				if (key != null && key.Item2 != 0)
				{
					lock (_lookups.SyncRoot)
					{
						if (!_lookups.ContainsKey(key))
							_lookups.Add(key, new HashSet<IMessageAdapter>(adapters.Select(Helper.GetUnderlyingAdapter)));
					}
				}

				if (message is SubscriptionListRequestMessage listRequest)
					_subscriptionListRequests.Add(listRequest.TransactionId);

				adapters.ForEach(a => a.SendInMessage(message));
			}
		}

		private IMessageAdapter[] GetAdapters(Message message, out bool isPended, out bool skipSupportedMessages)
		{
			isPended = false;
			skipSupportedMessages = false;

			IMessageAdapter[] adapters = null;

			var adapter = message.Adapter?.GetUnderlyingAdapter();

			if (adapter == null && message is MarketDataMessage mdMsg && mdMsg.DataType.IsSecurityRequired())
				adapter = _securityAdapters.TryGetValue(Tuple.Create(mdMsg.SecurityId, (MarketDataTypes?)mdMsg.DataType)) ?? _securityAdapters.TryGetValue(Tuple.Create(mdMsg.SecurityId, (MarketDataTypes?)null));

			if (adapter != null)
			{
				adapter = _activeAdapters.TryGetValue(adapter);

				if (adapter != null)
				{
					adapters = new[] { adapter };
					skipSupportedMessages = true;
				}
			}

			lock (_connectedResponseLock)
			{
				if (adapters == null)
					adapters = _messageTypeAdapters.TryGetValue(message.Type)?.Cache;

				if (adapters != null)
				{
					if (message.Type == MessageTypes.MarketData)
					{
						var mdMsg1 = (MarketDataMessage)message;
						var set = _subscriptionNonSupportedAdapters.TryGetValue(mdMsg1.TransactionId);

						if (set != null)
						{
							adapters = adapters.Where(a => !set.Contains(a.GetUnderlyingAdapter())).ToArray();
						}
						else if (mdMsg1.DataType == MarketDataTypes.News && mdMsg1.SecurityId == default)
						{
							adapters = adapters.Where(a => !a.IsSecurityNewsOnly).ToArray();
						}

						if (adapters.Length == 0)
							adapters = null;
					}
					else if (message.Type == MessageTypes.SecurityLookup)
					{
						var isAll = ((SecurityLookupMessage)message).IsLookupAll();

						if (isAll)
							adapters = adapters.Where(a => a.IsSupportSecuritiesLookupAll).ToArray();
					}
				}

				if (adapters == null)
				{
					if (_pendingConnectAdapters.Count > 0)
					{
						isPended = true;
						_pendingMessages.Enqueue(message.Clone());
						return ArrayHelper.Empty<IMessageAdapter>();
					}
				}
			}

			if (adapters == null)
			{
				adapters = ArrayHelper.Empty<IMessageAdapter>();
			}

			if (adapters.Length == 0)
			{
				this.AddInfoLog(LocalizedStrings.Str629Params.Put(message));
				//throw new InvalidOperationException(LocalizedStrings.Str629Params.Put(message));
			}

			return adapters;
		}

		private IMessageAdapter[] GetSubscriptionAdapters(MarketDataMessage mdMsg)
		{
			var adapters = GetAdapters(mdMsg, out _, out var skipSupportedMessages).Where(a =>
			{
				if (skipSupportedMessages)
					return true;

				if (mdMsg.DataType != MarketDataTypes.CandleTimeFrame)
				{
					var isCandles = mdMsg.DataType.IsCandleDataType();

					if (a.IsMarketDataTypeSupported(mdMsg.DataType) && (!isCandles || a.IsCandlesSupported(mdMsg)))
						return true;
					else
					{
						switch (mdMsg.DataType)
						{
							case MarketDataTypes.Level1:
							case MarketDataTypes.OrderLog:
							case MarketDataTypes.News:
							case MarketDataTypes.Board:
								return false;
							case MarketDataTypes.MarketDepth:
							{
								if (mdMsg.BuildMode != MarketDataBuildModes.Load)
								{
									switch (mdMsg.BuildFrom)
									{
										case MarketDataTypes.Level1:
											return a.IsMarketDataTypeSupported(MarketDataTypes.Level1);
										case MarketDataTypes.OrderLog:
											return a.IsMarketDataTypeSupported(MarketDataTypes.OrderLog);
									}
								}

								return false;
							}
							case MarketDataTypes.Trades:
								return a.IsMarketDataTypeSupported(MarketDataTypes.OrderLog);
							default:
							{
								if (isCandles && a.TryGetCandlesBuildFrom(mdMsg, CandleBuilderProvider) != null)
									return true;

								return false;
								//throw new ArgumentOutOfRangeException(mdMsg.DataType.ToString());
							}
						}
					}
				}

				var original = mdMsg.GetTimeFrame();
				var timeFrames = a.GetTimeFrames(mdMsg.SecurityId, mdMsg.From, mdMsg.To).ToArray();

				if (timeFrames.Contains(original) || a.CheckTimeFrameByRequest)
					return true;

				if (mdMsg.AllowBuildFromSmallerTimeFrame)
				{
					var smaller = timeFrames
					              .FilterSmallerTimeFrames(original)
					              .OrderByDescending()
					              .FirstOr();

					if (smaller != null)
						return true;
				}

				return a.TryGetCandlesBuildFrom(mdMsg, CandleBuilderProvider) != null;
			}).ToArray();

			//if (!isPended && adapters.Length == 0)
			//	throw new InvalidOperationException(LocalizedStrings.Str629Params.Put(mdMsg));

			return adapters;
		}

		private void ProcessMarketDataRequest(MarketDataMessage mdMsg)
		{
			if (mdMsg.TransactionId == 0)
				throw new InvalidOperationException("TransId == 0");

			switch (mdMsg.DataType)
			{
				case MarketDataTypes.News:
				case MarketDataTypes.Board:
				{
					var dict = new Dictionary<IMessageAdapter, long>();

					if (mdMsg.IsSubscribe)
					{
						var adapters = GetSubscriptionAdapters(mdMsg);

						if (adapters.Length == 0)
						{
							SendOutMarketDataNotSupported(mdMsg.TransactionId);
							break;
						}

						lock (_newsBoardSubscriptions.SyncRoot)
						{
							foreach (var adapter in adapters)
							{
								var transId = TransactionIdGenerator.GetNextId();

								dict.Add(adapter, transId);

								_newsBoardSubscriptions.Add(transId, RefTuple.Create(mdMsg.TransactionId, (bool?)null, adapter));
							}
						}
					}
					else
					{
						lock (_newsBoardSubscriptions.SyncRoot)
						{
							var adapters = _newsBoardSubscriptions.Select(p => p.Value.Third).Where(a => a != null).ToArray();

							foreach (var adapter in adapters)
							{
								var transId = TransactionIdGenerator.GetNextId();

								dict.Add(adapter, transId);

								_newsBoardSubscriptions.Add(transId, RefTuple.Create(mdMsg.TransactionId, (bool?)null, adapter));
							}
						}
					}

					// sending to inner adapters unique subscriptions
					foreach (var pair in dict)
					{
						var clone = (MarketDataMessage)mdMsg.Clone();
						clone.TransactionId = pair.Value;

						_subscriptionMessages.Add(pair.Value, clone);
						pair.Key.SendInMessage(clone);
					}

					break;
				}

				default:
				{
					IMessageAdapter adapter;

					if (mdMsg.IsSubscribe)
						adapter = GetSubscriptionAdapters(mdMsg).FirstOrDefault();
					else
					{
						var originTransId = mdMsg.OriginalTransactionId;

						if (originTransId == 0)
							originTransId = _keysToTransId.TryGetValue(mdMsg.CreateKey());

						var tuple = _subscriptionsById.TryGetValue(originTransId);

						if (tuple == null)
							adapter = null;
						else
						{
							adapter = tuple.Item1;

							// copy full subscription's details into unsubscribe request
							var transId = mdMsg.TransactionId;
							tuple.Item2?.CopyTo(mdMsg);
							mdMsg.TransactionId = transId;
							mdMsg.OriginalTransactionId = originTransId;
						}
					}

					if (adapter != null)
					{
						// if the message was looped back via IsBack=true
						_subscriptionMessages.TryAdd(mdMsg.TransactionId, (MarketDataMessage)mdMsg.Clone());
						adapter.SendInMessage(mdMsg);
					}
					else
					{
						if (mdMsg.IsSubscribe)
							SendOutMarketDataNotSupported(mdMsg.TransactionId);
					}

					break;
				}
			}
		}

		private void ProcessAdapterMessage(string portfolioName, Message message)
		{
			var adapter = message.Adapter;

			if (adapter == null)
				ProcessPortfolioMessage(portfolioName, message);
			else
				adapter.SendInMessage(message);
		}

		private void ProcessPortfolioMessage(string portfolioName, Message message)
		{
			var adapter = portfolioName.IsEmpty() ? null : _portfolioAdapters.TryGetValue(portfolioName);

			if (adapter == null)
			{
				adapter = GetAdapters(message, out _, out _).FirstOrDefault();

				if (adapter == null)
					return;
			}
			else
			{
				var a = _activeAdapters.TryGetValue(adapter);

				adapter = a ?? throw new InvalidOperationException(LocalizedStrings.Str1838Params.Put(adapter.GetType()));
			}

			adapter.SendInMessage(message);
		}

		/// <summary>
		/// The embedded adapter event <see cref="IMessageChannel.NewOutMessage"/> handler.
		/// </summary>
		/// <param name="innerAdapter">The embedded adapter.</param>
		/// <param name="message">Message.</param>
		protected virtual void OnInnerAdapterNewOutMessage(IMessageAdapter innerAdapter, Message message)
		{
			Message extraOutMsg = null;

			if (!message.IsBack)
			{
				if (message.Adapter == null)
					message.Adapter = innerAdapter;

				switch (message.Type)
				{
					case MessageTypes.Connect:
						ProcessConnectMessage(innerAdapter, (ConnectMessage)message);
						return;

					case MessageTypes.Disconnect:
						ProcessDisconnectMessage(innerAdapter, (DisconnectMessage)message);
						return;

					case MessageTypes.MarketData:
						ProcessMarketDataResponse(innerAdapter, (MarketDataMessage)message);
						return;

					case MessageTypes.Portfolio:
					case MessageTypes.PortfolioChange:
					case MessageTypes.PositionChange:
					{
						var pfMsg = (IPortfolioNameMessage)message;
						PortfolioAdapterProvider.SetAdapter(pfMsg.PortfolioName, innerAdapter.GetUnderlyingAdapter().Id);
						
						if (LookupMessagesOnConnect && innerAdapter.IsSupportSubscriptionByPortfolio)
							extraOutMsg = CreatePortfolioSubscription(innerAdapter, pfMsg.PortfolioName);

						break;
					}

					case MessageTypes.Security:
						var secMsg = (SecurityMessage)message;
						SecurityAdapterProvider.SetAdapter(secMsg.SecurityId, null, innerAdapter.GetUnderlyingAdapter().Id);
						break;

					default:
						if (message.Type.IsLookupResult() && !CanProcessLookupResult(innerAdapter.GetUnderlyingAdapter(), message))
							return;

						break;
				}
			}

			SendOutMessage(message);

			if (extraOutMsg != null)
			{
				extraOutMsg.IsBack = true;
				SendOutMessage(extraOutMsg);
			}
		}

		private bool CanProcessLookupResult(IMessageAdapter innerAdapter, Message message)
		{
			var transId = ((IOriginalTransactionIdMessage)message).OriginalTransactionId;

			if (transId == 0)
				return true;

			lock (_lookups.SyncRoot)
			{
				var adapters = _lookups.TryGetValue(Tuple.Create(message.Type, transId));

				if (adapters == null)
					return true;

				if (!adapters.Remove(innerAdapter))
					return true;

				return adapters.Count == 0;	
			}
		}

		private static TMessage FillIdAndAdapter<TMessage>(IMessageAdapter adapter, TMessage m)
			where TMessage : Message, ITransactionIdMessage
		{
			m.TransactionId = adapter.TransactionIdGenerator.GetNextId();
			m.Adapter = adapter;

			return m;
		}

		private PortfolioMessage CreatePortfolioSubscription(IMessageAdapter adapter, string portfolioName)
		{
			if (portfolioName.IsEmpty())
				throw new ArgumentNullException(nameof(portfolioName));

			var underlyingAdapter = adapter.GetUnderlyingAdapter();

			if (!_subscribedPortfolios.SafeAdd(underlyingAdapter, key => new HashSet<string>(StringComparer.InvariantCultureIgnoreCase)).Add(portfolioName))
				return null;

			return FillIdAndAdapter(adapter, new PortfolioMessage
			{
				PortfolioName = portfolioName,
				IsSubscribe = true,
			});
		}

		private void ProcessConnectMessage(IMessageAdapter innerAdapter, ConnectMessage message)
		{
			var underlyingAdapter = innerAdapter.GetUnderlyingAdapter();
			var wrapper = _activeAdapters[underlyingAdapter];

			var isError = message.Error != null;

			var messages = new List<Message>();

			if (isError)
				this.AddErrorLog(LocalizedStrings.Str625Params, underlyingAdapter, message.Error);
			else
				this.AddInfoLog("Connected to '{0}'.", underlyingAdapter);

			lock (_connectedResponseLock)
			{
				_pendingConnectAdapters.Remove(underlyingAdapter);

				if (isError)
				{
					_connectedAdapters.Remove(wrapper);

					if (_pendingConnectAdapters.Count == 0)
					{
						messages.AddRange(_pendingMessages);
						_pendingMessages.Clear();
					}
				}
				else
				{
					foreach (var supportedMessage in innerAdapter.SupportedMessages)
					{
						_messageTypeAdapters.SafeAdd(supportedMessage).Add(wrapper);
					}

					_connectedAdapters.Add(wrapper);

					messages.AddRange(_pendingMessages);
					_pendingMessages.Clear();
				}
			}

			message.Adapter = underlyingAdapter;
			SendOutMessage(message);

			if (!isError && LookupMessagesOnConnect)
			{
				if (innerAdapter.PortfolioLookupRequired)
					messages.Add(FillIdAndAdapter(innerAdapter, new PortfolioLookupMessage { IsSubscribe = true }));

				if (innerAdapter.OrderStatusRequired)
					messages.Add(FillIdAndAdapter(innerAdapter, new OrderStatusMessage { IsSubscribe = true }));

				if (innerAdapter.SecurityLookupRequired && innerAdapter.IsSupportSecuritiesLookupAll)
					messages.Add(FillIdAndAdapter(innerAdapter, new SecurityLookupMessage()));

				if (innerAdapter.IsSupportSubscriptionByPortfolio)
				{
					var portfolioNames = PortfolioAdapterProvider
						.Adapters
						.Where(p => p.Value == innerAdapter.Id)
						.Select(p => p.Key);

					foreach (var portfolioName in portfolioNames)
					{
						var msg = CreatePortfolioSubscription(innerAdapter, portfolioName);

						if (msg != null)
							messages.Add(msg);
					}
				}
			}

			foreach (var backMsg in messages)
			{
				if (isError)
					SendOutError(LocalizedStrings.Str629Params.Put(backMsg.Type));
				else
				{
					if (backMsg.Adapter == null)
						backMsg.Adapter = this;

					backMsg.IsBack = true;
					SendOutMessage(backMsg);
				}
			}
		}

		private void ProcessDisconnectMessage(IMessageAdapter innerAdapter, DisconnectMessage message)
		{
			var underlyingAdapter = innerAdapter.GetUnderlyingAdapter();
			var wrapper = _activeAdapters[underlyingAdapter];

			if (message.Error == null)
				this.AddInfoLog("Disconnected from '{0}'.", underlyingAdapter);
			else
				this.AddErrorLog(LocalizedStrings.Str627Params, underlyingAdapter, message.Error);

			lock (_connectedResponseLock)
			{
				foreach (var supportedMessage in innerAdapter.SupportedMessages)
				{
					var list = _messageTypeAdapters.TryGetValue(supportedMessage);

					if (list == null)
						continue;

					list.Remove(wrapper);

					if (list.Count == 0)
						_messageTypeAdapters.Remove(supportedMessage);
				}

				_connectedAdapters.Remove(wrapper);
			}

			message.Adapter = underlyingAdapter;
			SendOutMessage(message);
		}

		private void ProcessMarketDataResponse(IMessageAdapter adapter, MarketDataMessage message)
		{
			var originalTransactionId = message.OriginalTransactionId;
			var originMsg = _subscriptionMessages.TryGetValue(originalTransactionId);

			if (originMsg == null)
			{
				if (_subscriptionListRequests.Contains(originalTransactionId))
					_subscriptionsById.TryAdd(message.TransactionId, Tuple.Create(adapter, (MarketDataMessage)null));

				SendOutMessage(message);
				return;
			}

			var isSubscribe = originMsg.IsSubscribe;

			if (!originMsg.DataType.IsSecurityRequired())
			{
				long? transId;
				var allError = true;

				lock (_newsBoardSubscriptions.SyncRoot)
				{
					var tuple = _newsBoardSubscriptions.TryGetValue(originalTransactionId);

					transId = tuple.First;
					tuple.Second = message.IsOk();

					foreach (var pair in _newsBoardSubscriptions)
					{
						var t = pair.Value;

						if (t.First == tuple.First)
						{
							// one of adapter still not yet response.
							if (t.Second == null)
							{
								transId = null;
								break;
							}
							else if (t.Second == true)
								allError = false;
						}
					}
				}

				if (transId != null)
				{
					SendOutMessage(new MarketDataMessage
					{
						OriginalTransactionId = transId.Value,
						Adapter = adapter,
						IsSubscribe = isSubscribe,
						Error = allError ? new InvalidOperationException(LocalizedStrings.Str629Params.Put(originMsg)) : null,
					});
				}

				return;
			}

			var key = originMsg.CreateKey();

			if (message.IsNotSupported)
			{
				lock (_connectedResponseLock)
				{
					// try loopback only subscribe messages
					if (originMsg.IsSubscribe)
					{
						var set = _subscriptionNonSupportedAdapters.SafeAdd(originalTransactionId, k => new HashSet<IMessageAdapter>());
						set.Add(adapter.GetUnderlyingAdapter());

						originMsg.Adapter = this;
						originMsg.IsBack = true;
					}
					
					SendOutMessage(originMsg);
				}

				return;
			}
			
			if (message.Error == null && isSubscribe && originMsg.To == null)
			{
				// we can initiate multiple subscriptions with unique request id and same params
				_keysToTransId.TryAdd(key, originalTransactionId);

				// TODO
				_subscriptionsById.TryAdd(originalTransactionId, Tuple.Create(adapter, originMsg));
			}

			RaiseMarketDataMessage(adapter, originalTransactionId, message.Error, isSubscribe);
		}

		private void RaiseMarketDataMessage(IMessageAdapter adapter, long originalTransactionId, Exception error, bool isSubscribe)
		{
			SendOutMessage(new MarketDataMessage
			{
				OriginalTransactionId = originalTransactionId,
				Error = error,
				Adapter = adapter,
				IsSubscribe = isSubscribe,
			});
		}

		private void SecurityAdapterProviderOnChanged(Tuple<SecurityId, MarketDataTypes?> key, Guid adapterId, bool changeType)
		{
			if (changeType)
			{
				var adapter = InnerAdapters.SyncGet(c => c.FindById(adapterId));

				if (adapter == null)
					_securityAdapters.Remove(key);
				else
					_securityAdapters[key] = adapter;
			}
			else
				_securityAdapters.Remove(key);
		}

		private void PortfolioAdapterProviderOnChanged(string key, Guid adapterId, bool changeType)
		{
			if (changeType)
			{
				var adapter = InnerAdapters.SyncGet(c => c.FindById(adapterId));

				if (adapter == null)
					_portfolioAdapters.Remove(key);
				else
					_portfolioAdapters[key] = adapter;
			}
			else
				_portfolioAdapters.Remove(key);
		}

		/// <inheritdoc />
		public override void Save(SettingsStorage storage)
		{
			lock (InnerAdapters.SyncRoot)
			{
				storage.SetValue(nameof(InnerAdapters), InnerAdapters.Select(a =>
				{
					var s = new SettingsStorage();

					s.SetValue("AdapterType", a.GetType().GetTypeName(false));
					s.SetValue("AdapterSettings", a.Save());
					s.SetValue("Priority", InnerAdapters[a]);

					return s;
				}).ToArray());
			}

			if (LatencyManager != null)
				storage.SetValue(nameof(LatencyManager), LatencyManager.SaveEntire(false));

			if (CommissionManager != null)
				storage.SetValue(nameof(CommissionManager), CommissionManager.SaveEntire(false));

			if (PnLManager != null)
				storage.SetValue(nameof(PnLManager), PnLManager.SaveEntire(false));

			if (SlippageManager != null)
				storage.SetValue(nameof(SlippageManager), SlippageManager.SaveEntire(false));

			base.Save(storage);
		}

		/// <inheritdoc />
		public override void Load(SettingsStorage storage)
		{
			lock (InnerAdapters.SyncRoot)
			{
				InnerAdapters.Clear();

				var adapters = new Dictionary<Guid, IMessageAdapter>();

				foreach (var s in storage.GetValue<IEnumerable<SettingsStorage>>(nameof(InnerAdapters)))
				{
					try
					{
						var adapter = s.GetValue<Type>("AdapterType").CreateAdapter(TransactionIdGenerator);
						adapter.Load(s.GetValue<SettingsStorage>("AdapterSettings"));
						InnerAdapters[adapter] = s.GetValue<int>("Priority");

						adapters.Add(adapter.Id, adapter);
					}
					catch (Exception e)
					{
						e.LogError();
					}
				}

				_securityAdapters.Clear();

				foreach (var pair in SecurityAdapterProvider.Adapters)
				{
					if (!adapters.TryGetValue(pair.Value, out var adapter))
						continue;

					_securityAdapters.Add(pair.Key, adapter);
				}

				_portfolioAdapters.Clear();

				foreach (var pair in PortfolioAdapterProvider.Adapters)
				{
					if (!adapters.TryGetValue(pair.Value, out var adapter))
						continue;

					_portfolioAdapters.Add(pair.Key, adapter);
				}
			}

			if (storage.ContainsKey(nameof(LatencyManager)))
				LatencyManager = storage.GetValue<SettingsStorage>(nameof(LatencyManager)).LoadEntire<ILatencyManager>();

			if (storage.ContainsKey(nameof(CommissionManager)))
				CommissionManager = storage.GetValue<SettingsStorage>(nameof(CommissionManager)).LoadEntire<ICommissionManager>();

			if (storage.ContainsKey(nameof(PnLManager)))
				PnLManager = storage.GetValue<SettingsStorage>(nameof(PnLManager)).LoadEntire<IPnLManager>();

			if (storage.ContainsKey(nameof(SlippageManager)))
				SlippageManager = storage.GetValue<SettingsStorage>(nameof(SlippageManager)).LoadEntire<ISlippageManager>();

			base.Load(storage);
		}

		/// <summary>
		/// To release allocated resources.
		/// </summary>
		protected override void DisposeManaged()
		{
			SecurityAdapterProvider.Changed -= SecurityAdapterProviderOnChanged;
			PortfolioAdapterProvider.Changed -= PortfolioAdapterProviderOnChanged;

			_activeAdapters.Values.ForEach(a => a.Parent = null);

			base.DisposeManaged();
		}

		/// <summary>
		/// Create a copy of <see cref="BasketMessageAdapter"/>.
		/// </summary>
		/// <returns>Copy.</returns>
		public override IMessageChannel Clone()
		{
			var clone = new BasketMessageAdapter(TransactionIdGenerator, SecurityAdapterProvider, PortfolioAdapterProvider, CandleBuilderProvider, StorageRegistry, SnapshotRegistry)
			{
				ExtendedInfoStorage = ExtendedInfoStorage,
				SupportCandlesCompression = SupportCandlesCompression,
				SuppressReconnectingErrors = SuppressReconnectingErrors,
				IsRestoreSubscriptionOnErrorReconnect = IsRestoreSubscriptionOnErrorReconnect,
				IsRestoreSubscriptionOnNormalReconnect = IsRestoreSubscriptionOnNormalReconnect,
				SupportBuildingFromOrderLog = SupportBuildingFromOrderLog,
				SupportOrderBookTruncate = SupportOrderBookTruncate,
				SupportOffline = SupportOffline,
				IgnoreExtraAdapters = IgnoreExtraAdapters,
				LookupMessagesOnConnect = LookupMessagesOnConnect,
				NativeIdStorage = NativeIdStorage,
				StorageDaysLoad = StorageDaysLoad,
				StorageMode = StorageMode,
				StorageFormat = StorageFormat,
				StorageDrive = StorageDrive,
				StorageFilterSubscription = StorageFilterSubscription,
			};

			clone.Load(this.Save());

			return clone;
		}
	}
}