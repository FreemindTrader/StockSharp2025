// Decompiled with JetBrains decompiler
// Type: StockSharp.Fix.Dialects.BaseFixDialect
// Assembly: StockSharp.Fix.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9148E39-A5BB-4657-14B1-EA8DED27B1C2
// Assembly location: A:\StockSharpBin\Terminal\StockSharp.Fix.Core.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.Fix.Native;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace StockSharp.Fix.Dialects
{
    /// <summary>
    /// Base class describing the dialect of the FIX protocol.
    /// </summary>
    public abstract class BaseFixDialect : BaseLogReceiver, ICloneable<IMessageChannel>, IFixDialect, IMessageAdapter, IMessageChannel, IDisposable, ICloneable, IPersistable, ILogReceiver, ILogSource
    {

        private sealed class QuoteChangeProcessor
        {
            private readonly BaseFixDialect _fixDialect;
            private readonly Dictionary<SecurityId, Dictionary<string, QuoteChange>> _secIdToQuoteChangeMap = new Dictionary<SecurityId, Dictionary<string, QuoteChange>>();

            public QuoteChangeProcessor(BaseFixDialect _param1) => _fixDialect = _param1 ?? throw new ArgumentNullException(nameof(_param1));

            public bool? ProcessQuoteChanges(IFixReader fixReader, Action<Message> handler)
            {
                var reader = new FixTagsReader();
                reader._iFixReader = fixReader;
                reader._qcProcessor = this;
                reader._symbol = null;
                reader._securityExchange = null;
                reader._exDestination = null;
                reader._tradingSessionID = null;
                reader._quoteID = null;
                reader._quoteType = new QuoteType?();
                reader._side = new char?();
                reader._bidPx = new decimal?();
                reader._bidSize = new decimal?();
                reader._offerPx = new decimal?();
                reader._offerSize = new decimal?();
                reader._sendingTime = new DateTimeOffset();

                if (!reader._iFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                    return new bool?();

                SecurityId key = new SecurityId()
                {
                    SecurityCode = reader._symbol,
                    BoardCode = _fixDialect.GetBoardCode(reader._exDestination, reader._securityExchange, reader._tradingSessionID)
                };

                var quoteChangeMap = _secIdToQuoteChangeMap.SafeAdd(key, p => new Dictionary<string, QuoteChange>());
                QuoteType? type = reader._quoteType;

                if (type.GetValueOrDefault() == QuoteType.Tradeable & type.HasValue)
                {
                    if (reader._side.HasValue)
                    {
                        QuoteChange quoteChange;
                        switch (reader._side.GetValueOrDefault())
                        {
                            case '1':
                                quoteChange = new QuoteChange(reader._bidPx.Value, reader._bidSize.Value);
                                break;
                            case '2':
                                quoteChange = new QuoteChange(reader._offerPx.Value, reader._offerSize.Value);
                                break;
                            default:
                                goto label_7;
                        }
                        quoteChangeMap[reader._quoteID] = quoteChange;
                        goto label_12;
                    }
                label_7:
                    throw new InvalidOperationException();
                }
                if (!quoteChangeMap.TryGetValue(reader._quoteID, out QuoteChange _))
                    return new bool?(true);
                quoteChangeMap.Remove(reader._quoteID);
            label_12:
                List<QuoteChange> bids = new List<QuoteChange>();
                List<QuoteChange> asks = new List<QuoteChange>();
                foreach (QuoteChange quoteChange in quoteChangeMap.Values)
                {
                    char? zkJdNu7o = reader._side;
                    int? nullable = zkJdNu7o.HasValue ? new int?(zkJdNu7o.GetValueOrDefault()) : new int?();
                    (nullable.GetValueOrDefault() == 49 & nullable.HasValue ? bids : asks).Add(quoteChange);
                }

                handler(
                            new QuoteChangeMessage()
                            {
                                SecurityId = key,
                                ServerTime = reader._sendingTime,
                                Bids = bids.ToArray(),
                                Asks = asks.ToArray()
                            }
                       );

                return new bool?(true);
            }

            public QuoteChangeMessage GetQuoteChangeMessage(SecurityId secId, DateTimeOffset serverTime, IEnumerable<MDEntry> mdEntries, bool qChangeStates, long? origTransId, DataType buildFrom)
            {
                List<QuoteChange> bids = new List<QuoteChange>();
                List<QuoteChange> asks = new List<QuoteChange>();

                bool hasPositions = false;

                foreach (MDEntry mdEntry in mdEntries)
                {
                    var mdPrice = mdEntry.Price.GetValueOrDefault();
                    var mdSize = mdEntry.Size.GetValueOrDefault();
                    var mdOrders = mdEntry.NumberOfOrders;

                    int? ordersCount;

                    if (!mdOrders.HasValue)
                    {
                        ordersCount = null;
                    }
                    else
                    {
                        ordersCount = new int?((int)mdOrders.GetValueOrDefault());
                    }


                    int quoteCondition = (int)mdEntry.QuoteCondition.ToQuoteCondition();
                    QuoteChange qChange = new QuoteChange(mdPrice, mdSize, ordersCount, (QuoteConditions)quoteCondition);
                    qChange.Action = mdEntry.Action.HasValue ? new QuoteChangeActions?(mdEntry.Action.GetValueOrDefault().ToQuoteAction()) : new QuoteChangeActions?();
                    qChange.StartPosition = mdEntry.Position;
                    qChange.EndPosition = mdEntry.PositionExtra;

                    if (!qChange.StartPosition.HasValue && !qChange.EndPosition.HasValue)
                    {
                        (mdEntry.Type == '0' ? bids : asks).Add(qChange);
                    }
                    else
                    {
                        hasPositions = true;
                    }
                }
                var msg = new QuoteChangeMessage();
                msg.SecurityId = secId;
                msg.ServerTime = serverTime;
                msg.State = new QuoteChangeStates?(qChangeStates ? QuoteChangeStates.SnapshotComplete : QuoteChangeStates.Increment);
                msg.Bids = bids.ToArray();
                msg.Asks = asks.ToArray();
                msg.HasPositions = hasPositions;
                msg.OriginalTransactionId = origTransId.GetValueOrDefault();
                msg.BuildFrom = buildFrom;
                return msg;
            }

            public void Reset() => _secIdToQuoteChangeMap.Clear();

            public QuoteChangeMessage NewQuoteChangeMessage(SecurityId _param1, DateTimeOffset _param2)
            {
                _secIdToQuoteChangeMap.Clear();
                return new QuoteChangeMessage()
                {
                    SecurityId = _param1,
                    ServerTime = _param2,
                    State = new QuoteChangeStates?(QuoteChangeStates.SnapshotComplete)
                };
            }

            private sealed class FixTagsReader
            {
                public string _symbol;
                public IFixReader _iFixReader;
                public string _securityExchange;
                public string _exDestination;
                public string _tradingSessionID;
                public string _quoteID;
                public QuoteType? _quoteType;
                public char? _side;
                public decimal? _bidPx;
                public decimal? _bidSize;
                public decimal? _offerPx;
                public decimal? _offerSize;
                public DateTimeOffset _sendingTime;
                public QuoteChangeProcessor _qcProcessor;

                internal bool ProcessFixTags(FixTags tag)
                {
                    switch (tag)
                    {
                        case FixTags.SendingTime:
                            _sendingTime = _iFixReader.ReadUtc(_qcProcessor._fixDialect.TimeStampParser);
                            return true;
                        case FixTags.Side:
                            _side = new char?(_iFixReader.ReadChar());
                            return true;
                        case FixTags.Symbol:
                            _symbol = _iFixReader.ReadString();
                            return true;
                        case FixTags.ExDestination:
                            _exDestination = _iFixReader.ReadString();
                            return true;
                        case FixTags.QuoteID:
                            _quoteID = _iFixReader.ReadString();
                            return true;
                        case FixTags.BidPx:
                            _bidPx = new decimal?(_iFixReader.ReadDecimal());
                            return true;
                        case FixTags.OfferPx:
                            _offerPx = new decimal?(_iFixReader.ReadDecimal());
                            return true;
                        case FixTags.BidSize:
                            _bidSize = new decimal?(_iFixReader.ReadDecimal());
                            return true;
                        case FixTags.OfferSize:
                            _offerSize = new decimal?(_iFixReader.ReadDecimal());
                            return true;
                        case FixTags.SecurityExchange:
                            _securityExchange = _iFixReader.ReadString();
                            return true;
                        case FixTags.TradingSessionID:
                            _tradingSessionID = _iFixReader.ReadString();
                            return true;
                        case FixTags.QuoteType:
                            _quoteType = new QuoteType?((QuoteType)_iFixReader.ReadInt());
                            return true;
                        default:
                            return false;
                    }
                }
            }
        }


        private readonly IdGenerator _idGenerator;
        private IFixWriter _bodyWriter;
        private IFixWriter _fixWriter2;
        private long _originalTransId;
        private bool _isDisconnecting;
        private readonly string _version;
        private Encoding _encoding;
        private string _login;
        private SecureString _password;
        private string _senderCompId;
        private string _targetCompId;

        private bool _isResetCounter = true;
        private TimeSpan _heartbeatInterval = TimeSpan.FromSeconds(60.0);
        private readonly ReConnectionSettings _reconnectionSettings = new ReConnectionSettings();
        private TimeZoneInfo _timeZone = TimeZoneInfo.Utc;

        private string _exchangeBoard;
        private string _clientCode;
        private bool _doNotSendAccount;
        private string _clientVersion;
        private string _accounts;

        private readonly FixFormats _fixFormats;
        private IFixWriter _fixMainWriter;
        private IFixReader _fixMainReader;

        private bool _tickAsLevel1;
        private bool _quotesAsLevel1;
        private bool _overrideExecIdByNative;

        private readonly MessageAdapterCategories _msgAdapterCategories;

        private readonly SynchronizedDictionary<long, SecurityId> _transId2SecIdMap = new SynchronizedDictionary<long, SecurityId>();
        private readonly SynchronizedDictionary<long, long> _nextId2TransIdMap = new SynchronizedDictionary<long, long>();
        private readonly SynchronizedPairSet<long, string> _transId2ClOrdIdMap = new SynchronizedPairSet<long, string>();

        private FastDateTimeParser _timeStampParser = new FastDateTimeParser("yyyyMMdd-HH:mm:ss.fff");
        private FastTimeSpanParser _timeParser = new FastTimeSpanParser(@"hh\:mm\:ss\.fff");
        private FastDateTimeParser _dateParser = new FastDateTimeParser("yyyyMMdd");
        private FastDateTimeParser _yearMonthParser = new FastDateTimeParser("yyyyMM");

        private readonly IEnumerable<Tuple<string, Type>> _securityExtendedFields = Enumerable.Empty<Tuple<string, Type>>();

        private bool _supportUnknownExecutions;

        private IEnumerable<DataType> _supportedDataTypes = new DataType[5]
                                                                            {
                                                                                DataType.Level1,
                                                                                DataType.News,
                                                                                DataType.MarketDepth,
                                                                                DataType.CandleTimeFrame,
                                                                                DataType.Ticks
                                                                            };

        private readonly IEnumerable<MessageTypeInfo> _supportedMessages = new MessageTypeInfo[12]
                                                                            {
                                                                                MessageTypes.MarketData.ToInfo(),
                                                                                MessageTypes.SecurityLookup.ToInfo(),
                                                                                MessageTypes.Portfolio.ToInfo(),
                                                                                MessageTypes.PortfolioLookup.ToInfo(),
                                                                                MessageTypes.OrderRegister.ToInfo(),
                                                                                MessageTypes.OrderReplace.ToInfo(),
                                                                                MessageTypes.OrderCancel.ToInfo(),
                                                                                MessageTypes.OrderGroupCancel.ToInfo(),
                                                                                MessageTypes.OrderStatus.ToInfo(),
                                                                                MessageTypes.ChangePassword.ToInfo(),
                                                                                ((MessageTypes) (-5000)).ToInfo( ),
                                                                                ((MessageTypes) (-5001)).ToInfo( )
                                                                            };

        private IEnumerable<MessageTypes> _supportedInMessages = Enumerable.Empty<MessageTypes>();

        private IEnumerable<MessageTypes> _supportedOutMessages = Enumerable.Empty<MessageTypes>();

        private IEnumerable<MessageTypes> _supportedResultMessages = new MessageTypes[2]
                                                                            {
                                                                                MessageTypes.MarketData,
                                                                                MessageTypes.Portfolio
                                                                            };

        private bool _cancelOnDisconnect;

        private readonly IDictionary<string, RefPair<SecurityTypes, string>> _securityClassInfo = new Dictionary<string, RefPair<SecurityTypes, string>>();

        private bool _enqueueSubscriptions;

        private bool _generateOrderBookFromLevel1 = true;

        private readonly IncrementalIdGenerator _incrementalIdGenerator = new IncrementalIdGenerator();

        private EndPoint _endPointAddress;

        private readonly Regex _regex = new Regex(@"MsgSeqNum too (low|high), expecting (?<expected>(\d+))");

        /// <summary>
        /// 
        /// </summary>
        public event Action<Message> NewOutMessage;

        private readonly QuoteChangeProcessor _innerClass;

        private readonly SynchronizedDictionary<long, RefPair<int, int>> _longRefPairMap = new SynchronizedDictionary<long, RefPair<int, int>>();

        /// <summary>
        /// Initialize <see cref="T:StockSharp.Fix.Dialects.BaseFixDialect" />.
        /// </summary>
        /// <param name="transactionIdGenerator">Transaction id generator.</param>
        /// <param name="encoding">Encoding.</param>
        /// <param name="version">FIX version.</param>
        /// <param name="format">FIX protocol format.</param>
        protected BaseFixDialect(
                                      IdGenerator transactionIdGenerator,
                                      Encoding encoding,
                                      string version = "FIX.4.4",
                                      FixFormats format = FixFormats.Text
                                )
        {
            if (version.IsEmpty())
            {
                throw new ArgumentNullException(nameof(version));
            }

            Encoding = encoding;
            _idGenerator = transactionIdGenerator ?? throw new ArgumentNullException(nameof(transactionIdGenerator));
            _version = version;
            _fixFormats = format;
            _innerClass = new QuoteChangeProcessor(this);
            var attribute = GetType().GetAttribute<MessageAdapterCategoryAttribute>();

            if (attribute == null)
                return;
            _msgAdapterCategories = attribute.Categories;
        }

        /// <inheritdoc />
        public override string ToString() => string.Concat(new string[5]
        {
            base.ToString(),
            ": Sender ",
            SenderCompId,
            " Target ",
            TargetCompId
        });

        /// <inheritdoc />
        public string Version => _version;

        /// <inheritdoc />
        public Encoding Encoding
        {
            get => _encoding;
            set => _encoding = value ?? throw new ArgumentNullException(nameof(_encoding));
        }

        /// <inheritdoc />
        public string Login
        {
            get => _login;
            set => _login = value;
        }

        /// <inheritdoc />
        public SecureString Password
        {
            get => _password;
            set => _password = value;
        }

        /// <inheritdoc />
        public string SenderCompId
        {
            get => _senderCompId;
            set => _senderCompId = value;
        }

        /// <inheritdoc />
        public string TargetCompId
        {
            get => _targetCompId;
            set => _targetCompId = value;
        }

        /// <inheritdoc />
        public FastDateTimeParser TimeStampParser
        {
            get => _timeStampParser;
            set => _timeStampParser = value ?? throw new ArgumentNullException(nameof(_timeStampParser));
        }

        /// <inheritdoc />
        public FastTimeSpanParser TimeParser
        {
            get => _timeParser;
            set => _timeParser = value ?? throw new ArgumentNullException(nameof(_timeParser));
        }

        /// <inheritdoc />
        public FastDateTimeParser DateParser
        {
            get => _dateParser;
            set => _dateParser = value ?? throw new ArgumentNullException(nameof(_dateParser));
        }

        /// <inheritdoc />
        public FastDateTimeParser YearMonthParser
        {
            get => _yearMonthParser;
            set => _yearMonthParser = value ?? throw new ArgumentNullException(nameof(_yearMonthParser));
        }

        /// <inheritdoc />
        public bool IsResetCounter
        {
            get => _isResetCounter;
            set => _isResetCounter = value;
        }

        /// <inheritdoc />
        public ReConnectionSettings ReConnectionSettings => _reconnectionSettings;

        /// <inheritdoc />
        public TimeSpan HeartbeatInterval
        {
            get => _heartbeatInterval;
            set => _heartbeatInterval = (int)value.TotalSeconds > 0 ? value : throw new ArgumentOutOfRangeException(nameof(_heartbeatInterval), value, LocalizedStrings.Str1219);
        }

        /// <inheritdoc />
        public TimeZoneInfo TimeZone
        {
            get => _timeZone;
            set => _timeZone = value ?? throw new ArgumentNullException(nameof(_timeZone));
        }

        /// <inheritdoc />
        public string ExchangeBoard
        {
            get => _exchangeBoard;
            set => _exchangeBoard = value;
        }

        /// <inheritdoc />
        public string ClientCode
        {
            get => _clientCode;
            set => _clientCode = value;
        }

        /// <inheritdoc />
        public bool DoNotSendAccount
        {
            get => _doNotSendAccount;
            set => _doNotSendAccount = value;
        }

        /// <inheritdoc />
        public string ClientVersion
        {
            get => _clientVersion;
            set => _clientVersion = value;
        }

        /// <inheritdoc />
        public string Accounts
        {
            get => _accounts;
            set => _accounts = value;
        }

        /// <inheritdoc />
        public FixFormats Format => _fixFormats;

        IFixWriter IFixDialect.Writer => _fixMainWriter;

        IFixReader IFixDialect.Reader => _fixMainReader;

        /// <summary>
        /// Translate tick data as <see cref="T:StockSharp.Messages.Level1ChangeMessage" /> or <see cref="T:StockSharp.Messages.ExecutionMessage" />.
        /// </summary>
        public bool TickAsLevel1
        {
            get => _tickAsLevel1;
            set => _tickAsLevel1 = value;
        }

        /// <summary>
        /// Translate quote data as <see cref="T:StockSharp.Messages.Level1ChangeMessage" /> or <see cref="T:StockSharp.Messages.QuoteChangeMessage" />.
        /// </summary>
        public bool QuotesAsLevel1
        {
            get => _quotesAsLevel1;
            set => _quotesAsLevel1 = value;
        }

        /// <summary>
        /// Use <see cref="P:StockSharp.Fix.Dialects.BaseFixDialect.Login" /> as portfolio name.
        /// </summary>
        protected virtual bool LoginAsPortfolioName => false;

        /// <inheritdoc />
        public bool OverrideExecIdByNative
        {
            get => _overrideExecIdByNative;
            set => _overrideExecIdByNative = value;
        }

        /// <inheritdoc />
        public virtual MessageAdapterCategories Categories => _msgAdapterCategories;

        /// <inheritdoc />
        public virtual string StorageName => Name;

        /// <inheritdoc />
        public virtual bool IsNativeIdentifiers => false;

        /// <summary>Support market-data response.</summary>
        protected virtual bool IsSupportMarketDataResponse => false;

        /// <inheritdoc />
        public virtual IEnumerable<Tuple<string, Type>> SecurityExtendedFields => _securityExtendedFields;

        /// <inheritdoc />
        public virtual bool IsNativeIdentifiersPersistable => true;

        /// <inheritdoc />
        public virtual bool SupportUnknownExecutions
        {
            get => _supportUnknownExecutions;
            set => _supportUnknownExecutions = value;
        }

        /// <summary>Possible time-frames.</summary>
        protected virtual IEnumerable<TimeSpan> TimeFrames => Enumerable.Empty<TimeSpan>();

        /// <inheritdoc />
        public virtual IOrderLogMarketDepthBuilder CreateOrderLogMarketDepthBuilder(
          SecurityId securityId)
        {
            return new OrderLogMarketDepthBuilder(securityId);
        }

        /// <inheritdoc />
        public IEnumerable<object> GetCandleArgs(Type candleType, SecurityId securityId, DateTimeOffset? from, DateTimeOffset? to)
        {
            return !(candleType == typeof(TimeFrameCandleMessage)) ? Enumerable.Empty<object>() : TimeFrames.Cast<object>();
        }

        /// <inheritdoc />
        public virtual TimeSpan GetHistoryStepSize(DataType dataType, out TimeSpan iterationInterval)
        {
            return GetHistoryStepSize(dataType, out iterationInterval);
        }

        /// <inheritdoc />
        public virtual int? GetMaxCount(DataType dataType) => dataType.GetDefaultMaxCount();

        /// <inheritdoc />
        public virtual IEnumerable<DataType> SupportedMarketDataTypes
        {
            get => _supportedDataTypes;
            set => _supportedDataTypes = value;
        }

        /// <inheritdoc />
        public virtual bool IsSupportCandlesUpdates => false;

        /// <inheritdoc />
        public virtual bool IsSupportCandlesPriceLevels => false;

        /// <summary>
        /// Reply errors for messages of type <see cref="F:StockSharp.Fix.Native.FixMessages.NewOrderSingle" /> transfers via <see cref="F:StockSharp.Fix.Native.FixMessages.Reject" />.
        /// </summary>
        protected virtual bool NewOrderSingleErrorsAsReject => true;

        /// <inheritdoc />
        public virtual bool CheckTimeFrameByRequest => false;

        /// <inheritdoc />
        public virtual IEnumerable<int> SupportedOrderBookDepths => Enumerable.Empty<int>();

        /// <inheritdoc />
        public virtual bool IsSupportOrderBookIncrements => true;

        /// <inheritdoc />
        public virtual bool IsSupportExecutionsPnL => false;

        /// <inheritdoc />
        public virtual bool IsSecurityNewsOnly => false;

        /// <inheritdoc />
        public virtual Type OrderConditionType => typeof(FixOrderCondition);

        /// <inheritdoc />
        public virtual bool HeartbeatBeforConnect => throw new NotSupportedException();

        /// <inheritdoc />
        public virtual IdGenerator TransactionIdGenerator => _idGenerator;

        /// <inheritdoc />
        public virtual IEnumerable<MessageTypeInfo> PossibleSupportedMessages => _supportedMessages;

        /// <inheritdoc />
        public virtual IEnumerable<MessageTypes> SupportedInMessages
        {
            get => _supportedInMessages;
            set => _supportedInMessages = value;
        }

        /// <inheritdoc />
        public virtual IEnumerable<MessageTypes> SupportedOutMessages
        {
            get => _supportedOutMessages;
            set => _supportedOutMessages = value;
        }

        /// <inheritdoc />
        public virtual IEnumerable<MessageTypes> SupportedResultMessages
        {
            get => _supportedResultMessages;
            set => _supportedResultMessages = value;
        }

        /// <inheritdoc />
        public virtual bool CancelOnDisconnect
        {
            get => _cancelOnDisconnect;
            set => _cancelOnDisconnect = value;
        }

        /// <inheritdoc />
        public virtual Uri Icon => GetType().GetIconUrl();

        /// <inheritdoc />
        public virtual bool IsAutoReplyOnTransactonalUnsubscription => true;

        /// <inheritdoc />
        public virtual bool IsFullCandlesOnly => true;

        /// <inheritdoc />
        public virtual bool IsSupportSubscriptions => true;

        /// <inheritdoc />
        public virtual IDictionary<string, RefPair<SecurityTypes, string>> SecurityClassInfo => _securityClassInfo;

        /// <inheritdoc />
        public virtual IEnumerable<Level1Fields> CandlesBuildFrom => Enumerable.Empty<Level1Fields>();

        /// <inheritdoc />
        public virtual bool EnqueueSubscriptions
        {
            get => _enqueueSubscriptions;
            set => _enqueueSubscriptions = value;
        }

        /// <inheritdoc />
        public virtual bool IsSupportTransactionLog => !IsResetCounter;

        /// <inheritdoc />
        public virtual bool UseChannels => false;

        /// <inheritdoc />
        public virtual TimeSpan IterationInterval => new TimeSpan();

        /// <inheritdoc />
        public virtual TimeSpan? LookupTimeout => new TimeSpan?();

        /// <inheritdoc />
        public virtual string FeatureName => GetFeaturesAttributes();

        /// <inheritdoc />
        public virtual bool? IsPositionsEmulationRequired => new bool?();

        /// <inheritdoc />
        public virtual bool IsReplaceCommandEditCurrent => false;

        /// <inheritdoc />
        public virtual bool GenerateOrderBookFromLevel1
        {
            get => _generateOrderBookFromLevel1;
            set => _generateOrderBookFromLevel1 = value;
        }

        /// <inheritdoc />
        public virtual long CurrentCounter
        {
            get => _incrementalIdGenerator.Current;
            set => _incrementalIdGenerator.Current = value;
        }

        /// <summary>Server address.</summary>
        protected EndPoint Address
        {
            get
            {
                return _endPointAddress;
            }

            set
            {
                _endPointAddress = value;
            }
        }


        void IFixDialect.Init(IFixWriter writer, IFixReader reader, EndPoint address)
        {
            _fixMainWriter = writer ?? throw new ArgumentNullException(nameof(writer));
            _fixMainReader = reader ?? throw new ArgumentNullException(nameof(reader));
            Address = address ?? throw new ArgumentNullException(nameof(address));

            switch (Format)
            {
                case FixFormats.Text:
                    _bodyWriter = new TextFixWriter(new MemoryStream(), Encoding);
                    _fixWriter2 = new TextFixWriter(new MemoryStream(), Encoding);
                    break;
                case FixFormats.Binary:
                    _bodyWriter = new BinaryFixWriter(new MemoryStream(), Encoding);
                    _fixWriter2 = new BinaryFixWriter(new MemoryStream(), Encoding);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <inheritdoc />
        public virtual long? TryParseNextMsqSeqNum(string errorMessage)
        {
            Match match = _regex.Match(errorMessage);
            return !match.Success ? new long?() : new long?(match.Groups["expected"].Value.To<long>());
        }

        /// <summary>Reset state.</summary>
        protected virtual void OnReset()
        {
            _innerClass.Reset();
            _longRefPairMap.Clear();
            _nextId2TransIdMap.Clear();
            _transId2ClOrdIdMap.Clear();
            _transId2SecIdMap.Clear();
            _originalTransId = 0L;
            _isDisconnecting = false;
        }


        /// <summary>
        /// Raise <see cref="E:StockSharp.Messages.IMessageChannel.NewOutMessage" /></summary>
        /// <param name="message">Message.</param>
        protected void RaiseNewOutMessage(Message message)
        {
            NewOutMessage?.Invoke(message);
        }


        /// <summary>Check state before connect.</summary>
        protected virtual void CheckState()
        {
            if (SenderCompId.IsEmpty())
                throw new InvalidOperationException(LocalizedStrings.SenderIdNotSet);
            if (TargetCompId.IsEmpty())
                throw new InvalidOperationException(LocalizedStrings.TargetIdNotSet);
        }

        private string GetFeaturesAttributes()
        {
            //LicenseFeatureAttribute attribute = ( this).GetType().GetAttribute<LicenseFeatureAttribute>();
            return /*attribute != null ? string.Concat( nameof( -1557770254 ), ( ( string ) attribute.FeatureName ).ToUpperInvariant( ) ) :*/ string.Empty;
        }

        /// <inheritdoc />
        public virtual bool SendInMessage(Message message)
        {
            switch (message.Type)
            {
                case MessageTypes.Connect:
                case MessageTypes.ChangePassword:
                    CheckState();
                    break;
                case MessageTypes.Disconnect:
                    _isDisconnecting = true;
                    break;
                case MessageTypes.Reset:
                    OnReset();
                    return true;
            }
            switch (message.Type)
            {
                case MessageTypes.PortfolioLookup:
                    PortfolioLookupMessage message1 = (PortfolioLookupMessage)message;
                    if (message1.IsSubscribe)
                    {
                        if (LoginAsPortfolioName)
                        {
                            PortfolioMessage portfolioMessage = new PortfolioMessage();
                            portfolioMessage.PortfolioName = GetSyntheticPortfolioName();
                            portfolioMessage.OriginalTransactionId = message1.TransactionId;
                            RaiseNewOutMessage(portfolioMessage);
                            RaiseNewOutMessage(message1.CreateResult());
                            return true;
                        }
                        break;
                    }
                    if (IsAutoReplyOnTransactonalUnsubscription)
                    {
                        RaiseNewOutMessage(message1.CreateResult());
                        return true;
                    }
                    break;
                case MessageTypes.OrderStatus:
                    OrderStatusMessage message2 = (OrderStatusMessage)message;
                    if (message2.IsSubscribe)
                    {
                        if (!IsResetCounter && !PossibleSupportedMessages.Any(p => p.Type == MessageTypes.OrderStatus))
                        {
                            _originalTransId = message2.TransactionId;
                            FixResendRequestMessage resendRequestMessage = new FixResendRequestMessage();
                            long? skip = message2.Skip;
                            resendRequestMessage.BeginSeqNo = skip ?? 1L;
                            skip = message2.Skip;
                            long? count = message2.Count;
                            resendRequestMessage.EndSeqNo = (skip.HasValue & count.HasValue ? new long?(skip.GetValueOrDefault() + count.GetValueOrDefault()) : new long?()).GetValueOrDefault();
                            message = resendRequestMessage;
                            break;
                        }
                        break;
                    }
                    if (IsAutoReplyOnTransactonalUnsubscription)
                    {
                        RaiseNewOutMessage(message2.CreateResult());
                        return true;
                    }
                    break;
            }
            try
            {
                _fixMainWriter.FlushDump();
                _fixMainWriter.ClearState();
                string msgType = OnWrite(_fixWriter2, message);
                if (msgType == null)
                {
                    _fixWriter2.Stream.Position = 0L;
                    return false;
                }
                long nextId = _incrementalIdGenerator.GetNextId();

                if (NewOrderSingleErrorsAsReject && message is OrderRegisterMessage orderRegisterMessage)
                {
                    _nextId2TransIdMap.Add(nextId, orderRegisterMessage.TransactionId);
                }

                MarketDataMessage marketDataMsg = null;

                if (message is MarketDataMessage)
                {
                    marketDataMsg = (MarketDataMessage)message;

                    if (!marketDataMsg.SecurityId.SecurityCode.IsEmpty())
                    {
                        _transId2SecIdMap.Add(marketDataMsg.TransactionId, marketDataMsg.SecurityId);
                    }
                }

                _fixMainWriter.WriteFixMessage(_bodyWriter, Version, msgType, SenderCompId, TargetCompId, TimeStampParser, nextId, x => x.WriteStream(_fixWriter2));

                if (marketDataMsg != null && !IsSupportMarketDataResponse)
                    RaiseNewOutMessage(marketDataMsg.CreateResponse());

                return true;
            }
            finally
            {
                if (_fixMainWriter.IsDump)
                    this.AddDebugLog(nameof(_fixMainWriter), _fixMainWriter.FlushDump());
            }
        }


        bool? IFixDialect.Read(Action<Message> messageHandler)
        {
            if (messageHandler == null)
                throw new ArgumentNullException(nameof(messageHandler));

            string msgType = _fixMainReader.ReadHeader(false, Version);
            if (msgType == null)
                return new bool?();
            bool fullRead = false;
            try
            {
                bool? nullable1 = OnRead(_fixMainReader, msgType, messageHandler);

                if (!nullable1.HasValue)
                    return new bool?();

                bool? nullable2 = nullable1;

                if (!nullable2.GetValueOrDefault() & nullable2.HasValue)
                {
                    this.AddWarningLog(LocalizedStrings.Str2142Params.Put(msgType));
                    _fixMainReader.SkipMessage();
                    return new bool?(false);
                }

                _fixMainReader.ReadTrailer(out fullRead);

                return new bool?(true);
            }
            catch (Exception ex)
            {
                if (!fullRead)
                    _fixMainReader.SkipMessage();
                throw;
            }
        }

        /// <inheritdoc />
        public virtual bool IsAllDownloadingSupported(DataType dataType) => dataType == DataType.Securities;

        /// <inheritdoc />
        public virtual bool IsSecurityRequired(DataType dataType) => dataType.IsSecurityRequired;

        /// <summary>Read next message from FIX protocol.</summary>
        /// <param name="reader">The reader of data recorded in the FIX protocol format.</param>
        /// <param name="msgType">Message type.</param>
        /// <param name="messageHandler">Message handler.</param>
        /// <returns>
        /// <see langword="true" />, if the messages was read successfully, otherwise, <see langword="false" />.</returns>
        protected virtual bool? OnRead(IFixReader reader, string msgType, Action<Message> messageHandler)
        {
            if (msgType == FixMessages.UserResponse)
                return ProcessUserResponse(reader, messageHandler);

            if (msgType == FixMessages.RequestForPositionsAck)
                return ProcessRequestForPositionsAck(reader, messageHandler);

            if (msgType == FixMessages.QuoteStatusReport)
                return ProcessQuoteStatusReport(reader, messageHandler);

            if (msgType == FixMessages.QuoteRequestReject)
                return ProcessQuoteRequestReject(reader, messageHandler);

            if (msgType == FixMessages.Logout)
                return ProcessLogout(reader, messageHandler);

            if (msgType == FixMessages.SequenceReset)
                return ProcessSequenceReset(reader, messageHandler);

            if (msgType == FixMessages.TestRequest)
                return ProcessTestRequest(reader, messageHandler);

            if (msgType == FixMessages.Heartbeat)
                return ProcessHeartbeat(reader);

            if (msgType == FixMessages.Reject)
                return ProcessReject(reader, messageHandler);

            if (msgType == FixMessages.ResendRequest)
                return ProcessResendRequest(reader, messageHandler);

            if (msgType == FixMessages.PositionReport)
                return ProcessPositionReport(reader, messageHandler);

            if (msgType == FixMessages.OrderCancelReject)
                return ProcessOrderCancelReject(reader, messageHandler);

            if (msgType == FixMessages.ExecutionReport)
                return ProcessExecutionReport(reader, messageHandler);

            if (msgType == FixMessages.Logon)
                return ProcessLogon(reader, messageHandler);

            if (msgType == FixMessages.News)
                return ProcessNews(reader, messageHandler);

            if (msgType == FixMessages.MarketDataSnapshotFullRefresh)
                return ProcessMarketDataSnapshotFullRefresh(reader, messageHandler, true);

            if (msgType == FixMessages.Quote)
                return _innerClass.ProcessQuoteChanges(reader, messageHandler);

            if (msgType == FixMessages.MarketDataRequestReject)
                return ProcessMarketDataRequestReject(reader, messageHandler);

            if (msgType == FixMessages.MarketDataIncrementalRefresh)
                return ProcessMarketDataSnapshotFullRefresh(reader, messageHandler, false);

            if (msgType == FixMessages.SecurityStatus)
                return ProcessSecurityStatus(reader, messageHandler);

            if (msgType == FixMessages.TradingSessionStatus)
                return ProcessTradingSessionStatus(reader, messageHandler);

            if (msgType == FixMessages.BusinessMessageReject)
                return ProcessBusinessMessageReject(reader, messageHandler);

            if (msgType == FixMessages.OrderMassCancelReport)
                return ProcessOrderMassCancelReport(reader, messageHandler);

            if (msgType == FixMessages.SecurityDefinition)
            {
            }

            if (msgType == FixMessages.SecurityList)
            {
            }

            return DefaultHandler(reader, messageHandler);
        }


        private sealed class LogonReader
        {
            public bool? _resetSeqNumFlag;
            public IFixReader IFixReader;
            public int? _nextExpectedMsgSeqNum;
            public string _tradingSessionID;
            public string _language;

            internal bool ProcessFixTags(FixTags _param1)
            {
                switch (_param1)
                {
                    case FixTags.ResetSeqNumFlag:
                        _resetSeqNumFlag = new bool?(IFixReader.ReadBool());
                        return true;
                    case FixTags.TradingSessionID:
                        _tradingSessionID = IFixReader.ReadString();
                        return true;
                    case FixTags.NextExpectedMsgSeqNum:
                        _nextExpectedMsgSeqNum = new int?(IFixReader.ReadInt());
                        return true;
                    case FixTags.Language:
                        _language = IFixReader.ReadString();
                        return true;
                    default:
                        return false;
                }
            }
        }

        private bool? ProcessLogon(IFixReader _param1, Action<Message> _param2)
        {
            LogonReader reader = new LogonReader();
            reader.IFixReader = _param1;
            reader._resetSeqNumFlag = new bool?();
            reader._nextExpectedMsgSeqNum = new int?();
            reader._tradingSessionID = null;
            reader._language = null;
            if (!reader.IFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            if ((!reader._resetSeqNumFlag.HasValue || !reader._resetSeqNumFlag.Value) && reader._nextExpectedMsgSeqNum.HasValue)
                CurrentCounter = reader._nextExpectedMsgSeqNum.Value;
            _param2(new ConnectMessage()
            {
                SessionId = reader._tradingSessionID,
                Language = reader._language
            });
            return new bool?(true);
        }


        private sealed class HeartbeatReader
        {
            public bool? _heartBeat;
            public IFixReader IFixReader;

            internal bool ProcessFixTags(FixTags _param1)
            {
                if (_param1 != FixTags.PossDupFlag)
                    return false;
                _heartBeat = new bool?(IFixReader.ReadBool());
                return true;
            }
        }


        private static bool? ProcessHeartbeat(IFixReader _param0)
        {
            HeartbeatReader reader = new HeartbeatReader();
            reader.IFixReader = _param0;
            reader._heartBeat = new bool?();
            if (!reader.IFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            reader._heartBeat.GetValueOrDefault();
            return new bool?(true);
        }

        private sealed class TestRequestReader
        {
            public string _userRequestID;
            public IFixReader IFixReader;
            public bool? _heartBeat;

            internal bool ProcessFixTags(FixTags _param1)
            {
                if (_param1 != FixTags.PossDupFlag)
                {
                    if (_param1 != FixTags.TestReqID)
                        return false;
                    _userRequestID = IFixReader.ReadString();
                    return true;
                }
                _heartBeat = new bool?(IFixReader.ReadBool());
                return true;
            }
        }

        private bool? ProcessTestRequest(IFixReader _param1, Action<Message> _param2)
        {
            TestRequestReader reader = new TestRequestReader();
            reader.IFixReader = _param1;
            reader._userRequestID = null;
            reader._heartBeat = new bool?();
            if (!reader.IFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            bool? jluJmNmApvhLyPaZiA = reader._heartBeat;
            if (jluJmNmApvhLyPaZiA.GetValueOrDefault() & jluJmNmApvhLyPaZiA.HasValue)
                return new bool?(true);
            Action<Message> action = _param2;
            TimeMessage timeMessage = new TimeMessage();
            timeMessage.TransactionId = _idGenerator.GetNextId();
            timeMessage.OriginalTransactionId = reader._userRequestID;
            timeMessage.BackMode = MessageBackModes.Direct;
            action(timeMessage);
            return new bool?(true);
        }

        private sealed class BusinessMessageRejectReader
        {
            public DateTimeOffset? _sendingTime;
            public IFixReader IFixReader;
            public BaseFixDialect _innerClass;
            public long? _refSeqNum;
            public string _refMsgType;
            public int? _businessRejectReason;
            public string _text;
            public int? _refTagID;
            public long? _mdResponseID;

            internal bool ProcessFixTags(FixTags _param1)
            {
                switch (_param1)
                {
                    case FixTags.RefSeqNum:
                        _refSeqNum = new long?(IFixReader.ReadLong());
                        return true;
                    case FixTags.SendingTime:
                        // ISSUE: explicit non-virtual call
                        _sendingTime = new DateTimeOffset?(IFixReader.ReadUtc(_innerClass.TimeStampParser));
                        return true;
                    case FixTags.Text:
                        _text = IFixReader.ReadString();
                        return true;
                    case FixTags.RefTagID:
                        _refTagID = new int?(IFixReader.ReadInt());
                        return true;
                    case FixTags.RefMsgType:
                        _refMsgType = IFixReader.ReadString();
                        return true;
                    case FixTags.BusinessRejectReason:
                        _businessRejectReason = new int?(IFixReader.ReadInt());
                        return true;
                    case FixTags.MDResponseID:
                        _mdResponseID = new long?(IFixReader.ReadLong());
                        return true;
                    default:
                        return false;
                }
            }
        }

        private bool? ProcessBusinessMessageReject(IFixReader _param1, Action<Message> _param2)
        {
            BusinessMessageRejectReader reader = new BusinessMessageRejectReader();
            reader.IFixReader = _param1;
            reader._innerClass = this;
            reader._refSeqNum = new long?();
            reader._refMsgType = null;
            reader._businessRejectReason = new int?();
            reader._text = null;
            reader._refTagID = new int?();
            reader._sendingTime = new DateTimeOffset?();
            reader._mdResponseID = new long?();
            if (!reader.IFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            string str = LocalizedStrings.Str1627Params.Put(reader._refSeqNum, reader._refMsgType, reader._businessRejectReason, reader._text, reader._refTagID);
            long num;

            if (NewOrderSingleErrorsAsReject && reader._refSeqNum.HasValue && _nextId2TransIdMap.TryGetValue(reader._refSeqNum.Value, out num))
            {
                Action<Message> action = _param2;
                ExecutionMessage executionMessage = new ExecutionMessage();
                executionMessage.ExecutionType = new ExecutionTypes?(ExecutionTypes.Transaction);
                executionMessage.HasOrderInfo = true;
                executionMessage.ServerTime = reader._sendingTime ?? DateTimeOffset.Now;
                executionMessage.OriginalTransactionId = num;
                executionMessage.Error = new InvalidOperationException(str);
                executionMessage.OrderState = new OrderStates?(OrderStates.Failed);
                action(executionMessage);
            }
            else
            {
                ErrorMessage errorMessage = str.ToErrorMessage();
                errorMessage.OriginalTransactionId = reader._mdResponseID.GetValueOrDefault();
                _param2(errorMessage);
            }
            return new bool?(true);
        }

        private sealed class RejectReader
        {
            public DateTimeOffset? _sendingTime;
            public IFixReader IFixReader;
            public BaseFixDialect _innerClass;
            public long? _refSeqNum;
            public string _refMsgType;
            public int? _businessRejectReason;
            public string _text;
            public int? _refTagID;
            public long? _mdResponseID;

            internal bool ProcessFixTags(FixTags _param1)
            {
                switch (_param1)
                {
                    case FixTags.RefSeqNum:
                        _refSeqNum = new long?(IFixReader.ReadLong());
                        return true;
                    case FixTags.SendingTime:
                        // ISSUE: explicit non-virtual call
                        _sendingTime = new DateTimeOffset?(IFixReader.ReadUtc(_innerClass.TimeStampParser));
                        return true;
                    case FixTags.Text:
                        _text = IFixReader.ReadString();
                        return true;
                    case FixTags.RefTagID:
                        _refTagID = new int?(IFixReader.ReadInt());
                        return true;
                    case FixTags.RefMsgType:
                        _refMsgType = IFixReader.ReadString();
                        return true;
                    case FixTags.SessionRejectReason:
                        _businessRejectReason = new int?(IFixReader.ReadInt());
                        return true;
                    case FixTags.MDResponseID:
                        _mdResponseID = new long?(IFixReader.ReadLong());
                        return true;
                    default:
                        return false;
                }
            }
        }

        private bool? ProcessReject(IFixReader _param1, Action<Message> _param2)
        {
            RejectReader reader = new RejectReader();
            reader.IFixReader = _param1;
            reader._innerClass = this;
            reader._refSeqNum = new long?();
            reader._refMsgType = null;
            reader._businessRejectReason = new int?();
            reader._text = null;
            reader._refTagID = new int?();
            reader._sendingTime = new DateTimeOffset?();
            reader._mdResponseID = new long?();
            if (!reader.IFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            string str = LocalizedStrings.Str1627Params.Put(reader._refSeqNum, reader._refMsgType, reader._businessRejectReason, reader._text, reader._refTagID);
            long num;
            if (NewOrderSingleErrorsAsReject && reader._refSeqNum.HasValue && _nextId2TransIdMap.TryGetValue(reader._refSeqNum.Value, out num))
            {
                Action<Message> action = _param2;
                ExecutionMessage msg = new ExecutionMessage();
                msg.ExecutionType = new ExecutionTypes?(ExecutionTypes.Transaction);
                msg.HasOrderInfo = true;
                msg.ServerTime = reader._sendingTime ?? DateTimeOffset.Now;
                msg.OriginalTransactionId = num;
                msg.Error = new InvalidOperationException(str);
                msg.OrderState = new OrderStates?(OrderStates.Failed);
                action(msg);
            }
            else
            {
                ErrorMessage errorMessage = str.ToErrorMessage();
                errorMessage.OriginalTransactionId = reader._mdResponseID.GetValueOrDefault();
                _param2(errorMessage);
            }
            return new bool?(true);
        }

        private sealed class UserResponseReader
        {
            public string _userRequestID;
            public IFixReader IFixReader;
            public string _userName;
            public UserStatus? _userStatus;
            public string _userStatusText;

            internal bool ProcessFixTags(FixTags _param1)
            {
                switch (_param1)
                {
                    case FixTags.Username:
                        _userName = IFixReader.ReadString();
                        return true;
                    case FixTags.UserRequestID:
                        _userRequestID = IFixReader.ReadString();
                        return true;
                    case FixTags.UserStatus:
                        _userStatus = new UserStatus?((UserStatus)IFixReader.ReadInt());
                        return true;
                    case FixTags.UserStatusText:
                        _userStatusText = IFixReader.ReadString();
                        return true;
                    default:
                        return false;
                }
            }
        }

        private static bool? ProcessUserResponse(IFixReader _param0, Action<Message> _param1)
        {
            UserResponseReader reader = new UserResponseReader();
            reader.IFixReader = _param0;
            reader._userRequestID = null;
            reader._userName = null;
            reader._userStatus = new UserStatus?();
            reader._userStatusText = null;
            if (!reader.IFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            long valueOrDefault = reader._userRequestID.To<long?>().GetValueOrDefault();
            if (reader._userStatus.HasValue)
            {
                switch (reader._userStatus.GetValueOrDefault())
                {
                    case 0:
                        Action<Message> action1 = _param1;
                        UserInfoMessage userInfoMessage = new UserInfoMessage();
                        userInfoMessage.Login = reader._userName;
                        userInfoMessage.OriginalTransactionId = valueOrDefault;
                        action1(userInfoMessage);
                        break;
                    case UserStatus.PasswordIncorrect:
                        Action<Message> action2 = _param1;
                        ChangePasswordMessage changePasswordMessage1 = new ChangePasswordMessage();
                        changePasswordMessage1.OriginalTransactionId = valueOrDefault;
                        changePasswordMessage1.Error = new InvalidOperationException(reader._userStatusText ?? LocalizedStrings.Str1628);
                        action2(changePasswordMessage1);
                        break;
                    case UserStatus.PasswordChanged:
                        Action<Message> action3 = _param1;
                        ChangePasswordMessage changePasswordMessage2 = new ChangePasswordMessage();
                        changePasswordMessage2.OriginalTransactionId = valueOrDefault;
                        action3(changePasswordMessage2);
                        break;
                }
            }
            return new bool?(true);
        }


        private sealed class ResendRequestReader
        {
            public long? _beginSeqNo;
            public IFixReader IFixReader;
            public long? _endSeqNo;
            public bool? _heartBeat;

            internal bool ProcessFixTags(FixTags _param1)
            {
                switch (_param1)
                {
                    case FixTags.BeginSeqNo:
                        _beginSeqNo = new long?(IFixReader.ReadLong());
                        return true;
                    case FixTags.EndSeqNo:
                        _endSeqNo = new long?(IFixReader.ReadLong());
                        return true;
                    case FixTags.PossDupFlag:
                        _heartBeat = new bool?(IFixReader.ReadBool());
                        return true;
                    default:
                        return false;
                }
            }
        }

        private bool? ProcessResendRequest(IFixReader _param1, Action<Message> _param2)
        {
            ResendRequestReader reader = new ResendRequestReader();
            reader.IFixReader = _param1;
            reader._beginSeqNo = new long?();
            reader._endSeqNo = new long?();
            reader._heartBeat = new bool?();
            if (!reader.IFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();

            if (reader._heartBeat.GetValueOrDefault() & reader._heartBeat.HasValue)
                return new bool?(true);

            _param2(new FixResendRequestMessage()
            {
                BeginSeqNo = reader._beginSeqNo.GetValueOrDefault(),
                EndSeqNo = reader._endSeqNo.GetValueOrDefault()
            });
            return new bool?(true);
        }

        /// <summary>
        /// Process extra tags for <see cref="T:StockSharp.Fix.FixSeqResetMessage" />.
        /// </summary>
        /// <param name="tag">Tag.</param>
        /// <param name="reader">The reader of data recorded in the FIX protocol format.</param>
        /// <param name="message">Sequence reset message.</param>
        /// <returns>Result.</returns>
        protected virtual bool ProcessSequenceResetExtraTag(
          FixTags tag,
          IFixReader reader,
          FixSeqResetMessage message)
        {
            return false;
        }

        private sealed class SequenceResetReader
        {
            public FixSeqResetMessage _fixResetMsg;
            public IFixReader IFixReader;
            public BaseFixDialect _innerClass;

            internal bool ProcessFixTags(FixTags _param1)
            {
                switch (_param1)
                {
                    case FixTags.MsgSeqNum:
                        _fixResetMsg.SeqNum = IFixReader.ReadLong();
                        return true;
                    case FixTags.NewSeqNo:
                        _fixResetMsg.NewSeqNo = IFixReader.ReadLong();
                        return true;
                    case FixTags.GapFillFlag:
                        _fixResetMsg.GapFill = new bool?(IFixReader.ReadBool());
                        return true;
                    default:
                        return _innerClass.ProcessSequenceResetExtraTag(_param1, IFixReader, _fixResetMsg);
                }
            }
        }

        private bool? ProcessSequenceReset(IFixReader _param1, Action<Message> _param2)
        {
            SequenceResetReader reader = new SequenceResetReader();
            reader.IFixReader = _param1;
            reader._innerClass = this;
            reader._fixResetMsg = new FixSeqResetMessage();
            if (!reader.IFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            _param2(reader._fixResetMsg);
            return new bool?(true);
        }

        /// <summary>
        /// Check <see cref="F:StockSharp.Fix.Native.FixMessages.Logout" /> contains error message.
        /// </summary>
        /// <param name="text">Text message.</param>
        /// <returns>
        /// <see langword="true" /> if the specified text contains error message, otherwise, <see langword="false" />.</returns>
        protected virtual bool IsLogoutError(string text) => !text.IsEmpty();

        private sealed class LogoutMsgReader
        {
            public string _text;
            public IFixReader IFixReader;

            internal bool ProcessFixTags(FixTags _param1)
            {
                if (_param1 != FixTags.Text)
                    return false;
                _text = IFixReader.ReadString();
                return true;
            }
        }

        private bool? ProcessLogout(IFixReader _param1, Action<Message> _param2)
        {
            LogoutMsgReader reader = new LogoutMsgReader();
            reader.IFixReader = _param1;
            reader._text = null;
            if (!reader.IFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            if (IsLogoutError(reader._text))
            {
                BaseConnectionMessage connectionMessage = _isDisconnecting ? new DisconnectMessage() : (BaseConnectionMessage)new ConnectMessage();
                connectionMessage.Error = new InvalidOperationException(reader._text);
                _param2(connectionMessage);
            }
            return new bool?(true);
        }

        /// <summary>Write the specified message into FIX protocol.</summary>
        /// <param name="writer">The recorder of data in the FIX protocol format.</param>
        /// <param name="message">The message.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Fix.Native.FixMessages" /> value.</returns>
        protected virtual string OnWrite(IFixWriter writer, Message message)
        {
            switch (message.Type)
            {
                case (MessageTypes)(-5001):
                    FixResendRequestMessage resendRequestMessage = (FixResendRequestMessage)message;
                    return WriteResendRequest(writer, resendRequestMessage.BeginSeqNo, resendRequestMessage.EndSeqNo);
                case (MessageTypes)(-5000):
                    FixSeqResetMessage fixSeqResetMessage = (FixSeqResetMessage)message;
                    return WriteSequenceReset(writer, fixSeqResetMessage.GapFill.GetValueOrDefault(), fixSeqResetMessage.NewSeqNo);
                case MessageTypes.Time:
                    return WriteTimeMessage(writer, (TimeMessage)message);
                case MessageTypes.Connect:
                    return WriteLogonRequest(writer, (ConnectMessage)message);
                case MessageTypes.Disconnect:
                    return WriteLogoutRequest(writer);
                case MessageTypes.ChangePassword:
                    return WriteUserRequestChangePassword(writer, (ChangePasswordMessage)message);
                default:
                    return null;
            }
        }

        /// <summary>
        /// To record the <see cref="F:StockSharp.Fix.Native.FixMessages.Logon" /> message (request).
        /// </summary>
        /// <param name="writer">The recorder of data in the FIX protocol format.</param>
        /// <param name="message">
        /// <see cref="T:StockSharp.Messages.ConnectMessage" />.</param>
        /// <param name="extra">Write extra parameters for <see cref="F:StockSharp.Fix.Native.FixMessages.Logon" /> message.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Fix.Native.FixMessages" /> value.</returns>
        protected string WriteLogonRequest(IFixWriter writer, ConnectMessage message, Action<IFixWriter> extra = null)
        {
            writer.Write(FixTags.EncryptMethod);
            writer.Write(0);
            writer.Write(FixTags.HeartBtInt);
            writer.Write((int)HeartbeatInterval.TotalSeconds);
            writer.Write(FixTags.ResetSeqNumFlag);
            writer.Write(IsResetCounter);

            if (!Login.IsEmpty())
            {
                writer.Write(FixTags.Username);
                writer.Write(Login);
            }

            if (!Password.IsEmpty())
            {
                writer.Write(FixTags.Password);
                writer.Write(Password.UnSecure());
            }

            if (message != null && !message.ClientVersion.IsEmpty())
            {
                writer.Write(FixTags.DefaultApplVerID);
                writer.Write(message.ClientVersion);
            }

            if (!message.Language.IsEmpty())
            {
                writer.Write(FixTags.Language);
                writer.Write(message.Language);
            }

            if (extra != null)
                extra(writer);

            return "A";
        }

        /// <summary>
        /// To record the <see cref="F:StockSharp.Fix.Native.FixMessages.Logout" /> message (request).
        /// </summary>
        /// <param name="writer">The recorder of data in the FIX protocol format.</param>
        /// <param name="text">The text of reason.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Fix.Native.FixMessages" /> value.</returns>
        protected static string WriteLogoutRequest(IFixWriter writer, string text = null)
        {
            if (!text.IsEmpty())
            {
                writer.Write(FixTags.Text);
                writer.Write(text);
            }
            return "5";
        }

        /// <summary>
        /// To record the <see cref="F:StockSharp.Fix.Native.FixMessages.SequenceReset" /> message (request).
        /// </summary>
        /// <param name="writer">The recorder of data in the FIX protocol format.</param>
        /// <param name="gapFill">Gap fill.</param>
        /// <param name="newSeqNo">New sequence number.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Fix.Native.FixMessages" /> value.</returns>
        protected string WriteSequenceReset(IFixWriter writer, bool gapFill, long newSeqNo)
        {
            writer.Write(FixTags.GapFillFlag);
            writer.Write(gapFill);
            writer.Write(FixTags.NewSeqNo);
            writer.Write(newSeqNo);

            return "4";
        }

        /// <summary>
        /// To record the <see cref="F:StockSharp.Fix.Native.FixMessages.ResendRequest" /> message.
        /// </summary>
        /// <param name="writer">The recorder of data in the FIX protocol format.</param>
        /// <param name="beginSeqNo">The original message identifier.</param>
        /// <param name="endSeqNo">The last message identifier.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Fix.Native.FixMessages" /> value.</returns>
        protected static string WriteResendRequest(IFixWriter writer, long beginSeqNo, long endSeqNo)
        {
            writer.Write(FixTags.BeginSeqNo);
            writer.Write(beginSeqNo);
            writer.Write(FixTags.EndSeqNo);
            writer.Write(endSeqNo);

            return "2";
        }

        /// <summary>
        /// To record the <see cref="F:StockSharp.Fix.Native.FixMessages.UserRequest" /> message.
        /// </summary>
        /// <param name="writer">The recorder of data in the FIX protocol format.</param>
        /// <param name="message">Password change message.</param>
        /// <param name="userName">Current user name.</param>
        /// <param name="password">Current password.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Fix.Native.FixMessages" /> value.</returns>
        protected string WriteUserRequestChangePassword(
          IFixWriter writer,
          ChangePasswordMessage message,
          string userName = null,
          string password = null)
        {
            writer.Write(FixTags.UserRequestID);
            writer.Write(message.TransactionId);
            writer.Write(FixTags.UserRequestType);
            writer.Write(3);
            writer.Write(FixTags.NewPassword);
            writer.Write(message.NewPassword.UnSecure());
            if (userName != null)
            {
                writer.Write(FixTags.Username);
                writer.Write(userName);
            }
            if (password != null)
            {
                writer.Write(FixTags.Password);
                writer.Write(password);
            }
            return "BE";
        }

        /// <summary>
        /// To record the <see cref="F:StockSharp.Fix.Native.FixMessages.Heartbeat" /> or <see cref="F:StockSharp.Fix.Native.FixMessages.TestRequest" /> message.
        /// </summary>
        /// <param name="writer">The recorder of data in the FIX protocol format.</param>
        /// <param name="timeMsg">Heartbeat message.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Fix.Native.FixMessages" /> value.</returns>
        protected static string WriteTimeMessage(IFixWriter writer, TimeMessage timeMsg) => timeMsg.OriginalTransactionId.IsEmpty() ? WriteTransId(writer, timeMsg.TransactionId) : WriteOriginalTransId(writer, timeMsg.OriginalTransactionId);

        private static string WriteOriginalTransId(IFixWriter _param0, string _param1)
        {
            _param0.Write(FixTags.TestReqID);
            _param0.Write(_param1);

            return "0";
        }

        private static string WriteTransId(IFixWriter _param0, long _param1)
        {
            _param0.Write(FixTags.TestReqID);
            _param0.Write(_param1);

            return "1";
        }

        /// <summary>Get board code.</summary>
        /// <param name="destination">
        /// <see cref="F:StockSharp.Fix.Native.FixTags.ExDestination" />.</param>
        /// <param name="exchange">
        /// <see cref="F:StockSharp.Fix.Native.FixTags.SecurityGroup" />.</param>
        /// <param name="tradingSession">
        /// <see cref="F:StockSharp.Fix.Native.FixTags.TradingSessionID" />.</param>
        /// <returns>Board code.</returns>
        protected virtual string GetBoardCode(
          string destination,
          string exchange,
          string tradingSession)
        {
            if (!destination.IsEmpty())
                return destination;
            if (!exchange.IsEmpty())
                return exchange;
            return !tradingSession.IsEmpty() ? tradingSession : ExchangeBoard;
        }

        /// <summary>
        /// Write <see cref="F:StockSharp.Fix.Native.FixTags.ClOrdID" />.
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="transactionId">Transaction ID.</param>
        protected void WriteClOrdId(IFixWriter writer, long transactionId)
        {
            string str;
            if (SupportUnknownExecutions && _transId2ClOrdIdMap.TryGetValue(transactionId, out str))
                writer.Write(str);
            else
                writer.Write(transactionId);
        }

        /// <inheritdoc />
        public virtual IMessageChannel Clone() => this.Clone<BaseFixDialect>();

        object ICloneable.Clone() => Clone();


        /// <inheritdoc />
        public virtual ChannelStates State => throw new NotSupportedException();

        event Action IMessageChannel.StateChanged
        {
            add { }
            remove { }
        }

        /// <inheritdoc />
        public virtual void Open() => throw new NotSupportedException();

        /// <inheritdoc />
        public virtual void Close() => throw new NotSupportedException();

        void IMessageChannel.Suspend()
        {
        }

        void IMessageChannel.Resume()
        {
        }

        void IMessageChannel.Clear()
        {
        }

        private sealed class MarketDataSnapshotFullRefreshReader
        {
            public DateTimeOffset? _date;
            public TimeSpan? _period;
            public DateTimeOffset _sendingTime;
            public BaseFixDialect _innerClass;
            public char? _myChar;
            public string _buildFromType;
            public char? _mdEntryType;
            public SecurityId? _securityId;
            public long? _originalTransactionId;
            public decimal? _price;
            public decimal? _size;
            public char? _tickDirection;
            public char? _mdEntryOriginator;
            public List<Level1ChangeMessage> _lvl1ChangeMessages;
            public string _symbol;
            public string _securityExchange;
            public string _tradeId;
            public CurrencyTypes? _currency;
            public decimal? _numberOfOrders;
            public decimal? _yield;
            public string _mdEntryBuyer;
            public string _mdEntrySeller;
            public Action<Message> _msgHandler;
            public List<MDEntry> _mdEntriesList;
            public int? _position;
            public int? _positionExtra;
            public char? _action;
            public string _quoteCondition;
            public DataType _dataType;
            public decimal? _openPrice;
            public decimal? _highPrice;
            public decimal? _lowPrice;
            public decimal? _closePrice;
            public int? _candleState;
            public decimal? _lowVolume;
            public decimal? _highVolume;
            public int? _upTicks;
            public int? _downTicks;
            public int? _totalTicks;
            public CandlePriceLevel[] _candlePriceLevels;
            public List<CandleMessage> _candleMsgList;
            public string _mdOtherValue;
            public int? _securityTradingStatus;
            public decimal? _levelPrice;
            public int _count;
            public Sides? _sides;
            public IFixReader IFixReader;
            public int _legIssueSize;

            internal DateTimeOffset GetServerTime()
            {
                if (!_date.HasValue && _period.HasValue)
                {
                    // ISSUE: explicit non-virtual call
                    return ((_sendingTime.IsDefault() ? DateTime.Today : _sendingTime.Date) + _period.Value).ApplyTimeZone(_innerClass.TimeZone);
                }
                DateTimeOffset? zk389mc9hkuj2 = _date;
                TimeSpan? zhBf3Bv7Ogkz8 = _period;
                return (zk389mc9hkuj2.HasValue & zhBf3Bv7Ogkz8.HasValue ? new DateTimeOffset?(zk389mc9hkuj2.GetValueOrDefault() + zhBf3Bv7Ogkz8.GetValueOrDefault()) : new DateTimeOffset?()) ?? _sendingTime;
            }

            internal DataType BuildFromDataType()
            {
                ref char? local = ref _myChar;
                return !local.HasValue ? null : local.GetValueOrDefault().ToDataType(_buildFromType);
            }

            internal void Process()
            {
                if (_mdEntryType.HasValue)
                {
                    switch (_mdEntryType.GetValueOrDefault())
                    {
                        case '0':
                        case '1':
                            if (_innerClass.QuotesAsLevel1)
                            {
                                Level1ChangeMessage level1ChangeMessage = new Level1ChangeMessage();
                                level1ChangeMessage.ServerTime = GetServerTime();
                                level1ChangeMessage.OriginalTransactionId = _originalTransactionId.GetValueOrDefault();
                                level1ChangeMessage.BuildFrom = BuildFromDataType();
                                Level1ChangeMessage message = level1ChangeMessage;
                                char? zXvJwKlefG2Cl = _mdEntryType;
                                int? nullable = zXvJwKlefG2Cl.HasValue ? new int?(zXvJwKlefG2Cl.GetValueOrDefault()) : new int?();
                                if (nullable.GetValueOrDefault() == 48 & nullable.HasValue)
                                    message.TryAdd(Level1Fields.BestBidPrice, _price).TryAdd(Level1Fields.BestBidVolume, _size);
                                else
                                    message.TryAdd(Level1Fields.BestAskPrice, _price).TryAdd(Level1Fields.BestAskVolume, _size);
                                _lvl1ChangeMessages.Add(message);
                                goto label_35;
                            }
                            else
                            {
                                _mdEntriesList.Add(new MDEntry()
                                {
                                    Type = _mdEntryType.Value,
                                    Price = _price,
                                    Size = _size,
                                    Position = _position,
                                    PositionExtra = _positionExtra,
                                    Action = _action,
                                    Currency = _currency,
                                    NumberOfOrders = _numberOfOrders,
                                    QuoteCondition = _quoteCondition,
                                    Yield = _yield
                                });
                                _dataType = BuildFromDataType();
                                goto label_35;
                            }
                        case '2':
                            if (!_securityId.HasValue)
                            {
                                _innerClass.AddErrorLog(LocalizedStrings.Str1615Params.Put(new object[1]
                                {
                   _originalTransactionId
                                }));
                                return;
                            }
                            if (_innerClass.TickAsLevel1)
                            {
                                Level1ChangeMessage message1 = new Level1ChangeMessage();
                                message1.ServerTime = GetServerTime();
                                message1.OriginalTransactionId = _originalTransactionId.GetValueOrDefault();
                                message1.BuildFrom = BuildFromDataType();
                                Level1ChangeMessage message2 = message1.Add(Level1Fields.LastTradePrice, _price).Add(Level1Fields.LastTradeVolume, _size);
                                ref char? local1 = ref _tickDirection;
                                bool? nullable1 = local1.HasValue ? new bool?(local1.GetValueOrDefault().FromTickDir()) : new bool?();
                                Level1ChangeMessage message3 = message2.TryAdd(Level1Fields.LastTradeUpDown, nullable1);
                                ref char? local2 = ref _mdEntryOriginator;
                                Sides? nullable2 = local2.HasValue ? new Sides?(local2.GetValueOrDefault().FromFixSide()) : new Sides?();
                                _lvl1ChangeMessages.Add(message3.TryAdd(Level1Fields.LastTradeOrigin, nullable2));
                                goto label_35;
                            }
                            else
                            {
                                if (_securityId.Value == new SecurityId())
                                    _securityId = new SecurityId?(new SecurityId()
                                    {
                                        SecurityCode = _symbol,
                                        BoardCode = _securityExchange
                                    });
                                ExecutionMessage executionMessage1 = new ExecutionMessage();
                                executionMessage1.SecurityId = _securityId.Value;
                                executionMessage1.TradeId = _tradeId.To<long?>();
                                executionMessage1.TradePrice = _price;
                                executionMessage1.TradeVolume = _size;
                                executionMessage1.ServerTime = GetServerTime();
                                executionMessage1.ExecutionType = new ExecutionTypes?(ExecutionTypes.Tick);
                                executionMessage1.Currency = _currency;
                                executionMessage1.OpenInterest = _numberOfOrders;
                                ref char? local1 = ref _mdEntryOriginator;
                                executionMessage1.OriginSide = local1.HasValue ? new Sides?(local1.GetValueOrDefault().FromFixSide()) : new Sides?();
                                ref char? local2 = ref _tickDirection;
                                executionMessage1.IsUpTick = local2.HasValue ? new bool?(local2.GetValueOrDefault().FromTickDir()) : new bool?();
                                executionMessage1.OriginalTransactionId = _originalTransactionId.GetValueOrDefault();
                                executionMessage1.BuildFrom = BuildFromDataType();
                                executionMessage1.Yield = _yield;
                                ExecutionMessage executionMessage2 = executionMessage1;
                                long result1;
                                if (_mdEntryBuyer != null && long.TryParse(_mdEntryBuyer, out result1))
                                    executionMessage2.OrderBuyId = new long?(result1);
                                long result2;
                                if (_mdEntrySeller != null && long.TryParse(_mdEntrySeller, out result2))
                                    executionMessage2.OrderSellId = new long?(result2);
                                _msgHandler(executionMessage2);
                                goto label_35;
                            }
                        case 'J':
                            if (_securityId.Value == new SecurityId())
                                _securityId = new SecurityId?(new SecurityId()
                                {
                                    SecurityCode = _symbol,
                                    BoardCode = _securityExchange
                                });
                            _msgHandler(_innerClass._innerClass.NewQuoteChangeMessage(_securityId.Value, _sendingTime));
                            goto label_35;
                    }
                }
                if (_mdEntryType.Value.IsCandleEntry())
                {
                    CandleMessage candleMessage1 = _mdEntryType.Value.ToCandleMessage();
                    candleMessage1.OpenPrice = _openPrice.GetValueOrDefault();
                    candleMessage1.HighPrice = _highPrice.GetValueOrDefault();
                    candleMessage1.LowPrice = _lowPrice.GetValueOrDefault();
                    candleMessage1.ClosePrice = _closePrice.GetValueOrDefault();
                    candleMessage1.TotalVolume = _size.GetValueOrDefault();
                    candleMessage1.OpenInterest = _numberOfOrders;
                    candleMessage1.OpenTime = GetServerTime();
                    CandleMessage candleMessage2 = candleMessage1;
                    int? zEiR7ED8NthT = _candleState;
                    int valueOrDefault = (int)(zEiR7ED8NthT.HasValue ? new CandleStates?((CandleStates)zEiR7ED8NthT.GetValueOrDefault()) : new CandleStates?()).GetValueOrDefault();
                    candleMessage2.State = (CandleStates)valueOrDefault;
                    candleMessage1.LowVolume = _lowVolume;
                    candleMessage1.HighVolume = _highVolume;
                    candleMessage1.OriginalTransactionId = _originalTransactionId.GetValueOrDefault();
                    candleMessage1.UpTicks = _upTicks;
                    candleMessage1.DownTicks = _downTicks;
                    candleMessage1.TotalTicks = _totalTicks;
                    candleMessage1.BuildFrom = BuildFromDataType();
                    if (_candlePriceLevels != null)
                    {
                        for (int index = 0; index < _candlePriceLevels.Length; ++index)
                        {
                            CandlePriceLevel candlePriceLevel = _candlePriceLevels[index];
                            if (candlePriceLevel.BuyVolumes != null)
                            {
                                candlePriceLevel.BuyCount = candlePriceLevel.BuyVolumes.Count();
                                candlePriceLevel.BuyVolume = candlePriceLevel.BuyVolumes.Sum();
                            }
                            if (candlePriceLevel.SellVolumes != null)
                            {
                                candlePriceLevel.SellCount = candlePriceLevel.SellVolumes.Count();
                                candlePriceLevel.SellVolume = candlePriceLevel.SellVolumes.Sum();
                            }
                            candlePriceLevel.TotalVolume = candlePriceLevel.BuyVolume + candlePriceLevel.SellVolume;
                            _candlePriceLevels[index] = candlePriceLevel;
                        }
                    }
                    candleMessage1.PriceLevels = _candlePriceLevels;
                    _candleMsgList.Add(candleMessage1);
                }
                else
                {
                    Level1ChangeMessage level1ChangeMessage = new Level1ChangeMessage();
                    level1ChangeMessage.ServerTime = GetServerTime();
                    level1ChangeMessage.OriginalTransactionId = _originalTransactionId.GetValueOrDefault();
                    level1ChangeMessage.BuildFrom = BuildFromDataType();
                    Level1ChangeMessage message = level1ChangeMessage;
                    // ISSUE: explicit non-virtual call
                    message.FillLevel1(_mdEntryType.Value, _price, _size, _mdOtherValue, _innerClass.TimeStampParser);
                    if (_securityTradingStatus.HasValue)
                        message.TryAdd(Level1Fields.State, _innerClass.FromSecurityTradingStatus(_securityTradingStatus));
                    _lvl1ChangeMessages.Add(message);
                }
            label_35:
                _mdEntryType = new char?();
                _date = new DateTimeOffset?();
                _period = new TimeSpan?();
                _price = new decimal?();
                _size = new decimal?();
                _position = new int?();
                _positionExtra = new int?();
                _action = new char?();
                _openPrice = new decimal?();
                _highPrice = new decimal?();
                _lowPrice = new decimal?();
                _closePrice = new decimal?();
                _tradeId = null;
                _tickDirection = new char?();
                _mdOtherValue = null;
                _currency = new CurrencyTypes?();
                _numberOfOrders = new decimal?();
                _candleState = new int?();
                _securityTradingStatus = new int?();
                _quoteCondition = null;
                _yield = new decimal?();
                _mdEntryOriginator = new char?();
                _lowVolume = new decimal?();
                _highVolume = new decimal?();
                _upTicks = new int?();
                _downTicks = new int?();
                _totalTicks = new int?();
                _candlePriceLevels = null;
                _levelPrice = new decimal?();
                _count = -1;
                _sides = new Sides?();
                _myChar = new char?();
                _buildFromType = null;
                _mdEntryBuyer = null;
                _mdEntrySeller = null;
            }

            internal bool ProcessFixTags(FixTags _param1)
            {
                switch (_param1)
                {
                    case FixTags.Currency:
                        if (_currency.HasValue)
                            Process();
                        _currency = IFixReader.ReadString().FromMicexCurrencyName(new Action<Exception>(_innerClass.AddErrorLog));
                        return true;
                    case FixTags.LastPx:
                        if (_closePrice.HasValue)
                            Process();
                        _closePrice = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.SendingTime:
                        // ISSUE: explicit non-virtual call
                        _sendingTime = IFixReader.ReadUtc(_innerClass.TimeStampParser);
                        return true;
                    case FixTags.Symbol:
                        _symbol = IFixReader.ReadString();
                        return true;
                    case FixTags.MinQty:
                        if (_lowVolume.HasValue)
                            Process();
                        _lowVolume = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.PrevClosePx:
                        if (_openPrice.HasValue)
                            Process();
                        _openPrice = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.SecurityExchange:
                        _securityExchange = IFixReader.ReadString();
                        return true;
                    case FixTags.Yield:
                        if (_yield.HasValue)
                            Process();
                        _yield = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.MDReqID:
                        _originalTransactionId = new long?(IFixReader.ReadLong());
                        _securityId = _innerClass._transId2SecIdMap.TryGetValue2(_originalTransactionId.Value);
                        return true;
                    case FixTags.MDEntryType:
                        if (_mdEntryType.HasValue)
                            Process();
                        _mdEntryType = new char?(IFixReader.ReadChar());
                        return true;
                    case FixTags.MDEntryPx:
                        if (_price.HasValue)
                            Process();
                        _price = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.MDEntrySize:
                        if (_size.HasValue)
                            Process();
                        _size = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.MDEntryDate:
                        if (_date.HasValue)
                            Process();
                        // ISSUE: explicit non-virtual call
                        _date = new DateTimeOffset?(IFixReader.ReadUtc(_innerClass.DateParser));
                        return true;
                    case FixTags.MDEntryTime:
                        if (_period.HasValue)
                            Process();
                        // ISSUE: explicit non-virtual call
                        _period = new TimeSpan?(IFixReader.ReadTimeSpan(_innerClass.TimeParser));
                        return true;
                    case FixTags.TickDirection:
                        if (_tickDirection.HasValue)
                            Process();
                        _tickDirection = new char?(IFixReader.ReadChar());
                        return true;
                    case FixTags.QuoteCondition:
                        if (_quoteCondition != null)
                            Process();
                        _quoteCondition = IFixReader.ReadString();
                        return true;
                    case FixTags.MDUpdateAction:
                        if (_action.HasValue)
                            Process();
                        _action = new char?(IFixReader.ReadChar());
                        return true;
                    case FixTags.MDEntryOriginator:
                        if (_mdEntryOriginator.HasValue)
                            Process();
                        _mdEntryOriginator = new char?(IFixReader.ReadChar());
                        return true;
                    case FixTags.MDEntryBuyer:
                        if (_mdEntryBuyer != null)
                            Process();
                        _mdEntryBuyer = IFixReader.ReadString();
                        return true;
                    case FixTags.MDEntrySeller:
                        if (_mdEntrySeller != null)
                            Process();
                        _mdEntrySeller = IFixReader.ReadString();
                        return true;
                    case FixTags.MDEntryPositionNo:
                        if (_position.HasValue)
                            Process();
                        _position = new int?(IFixReader.ReadInt());
                        return true;
                    case FixTags.NoQuoteEntries:
                        if (_candlePriceLevels != null)
                            Process();
                        _candlePriceLevels = new CandlePriceLevel[IFixReader.ReadInt()];
                        for (int index = 0; index < _candlePriceLevels.Length; ++index)
                            _candlePriceLevels[index] = new CandlePriceLevel();
                        _count = 0;
                        _levelPrice = new decimal?();
                        _sides = new Sides?();
                        return true;
                    case FixTags.QuoteEntryID:
                        if (_tradeId != null)
                            Process();
                        _tradeId = IFixReader.ReadString();
                        return true;
                    case FixTags.SecurityTradingStatus:
                        if (_securityTradingStatus.HasValue)
                            Process();
                        _securityTradingStatus = new int?(IFixReader.ReadInt());
                        return true;
                    case FixTags.HighPx:
                        if (_highPrice.HasValue)
                            Process();
                        _highPrice = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.LowPx:
                        if (_lowPrice.HasValue)
                            Process();
                        _lowPrice = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.NumberOfOrders:
                        if (_numberOfOrders.HasValue)
                            Process();
                        _numberOfOrders = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.NoLegs:
                        int length = IFixReader.ReadInt();
                        CandlePriceLevel candlePriceLevel1 = _candlePriceLevels[_count];
                        Sides? zLbgd0w6xqPgH1 = _sides;
                        Sides sides1 = Sides.Buy;
                        if (zLbgd0w6xqPgH1.GetValueOrDefault() == sides1 & zLbgd0w6xqPgH1.HasValue)
                            candlePriceLevel1.BuyVolumes = (new decimal[length]);
                        else
                            candlePriceLevel1.SellVolumes = (new decimal[length]);
                        _legIssueSize = 0;
                        return true;
                    case FixTags.MaxTradeVol:
                        if (_highVolume.HasValue)
                            Process();
                        _highVolume = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.MDOtherValue:
                        if (_mdOtherValue != null)
                            Process();
                        _mdOtherValue = IFixReader.ReadString();
                        return true;
                    case FixTags.CandleState:
                        if (_candleState.HasValue)
                            Process();
                        _candleState = new int?(IFixReader.ReadInt());
                        return true;
                    case FixTags.LegIssueSize:
                        CandlePriceLevel candlePriceLevel2 = _candlePriceLevels[_count];
                        Sides? zLbgd0w6xqPgH2 = _sides;
                        Sides sides2 = Sides.Buy;
                        (zLbgd0w6xqPgH2.GetValueOrDefault() == sides2 & zLbgd0w6xqPgH2.HasValue ? (decimal[])candlePriceLevel2.BuyVolumes : (decimal[])candlePriceLevel2.SellVolumes)[_legIssueSize++] = IFixReader.ReadDecimal();
                        return true;
                    case FixTags.UpTicks:
                        if (_upTicks.HasValue)
                            Process();
                        _upTicks = new int?(IFixReader.ReadInt());
                        return true;
                    case FixTags.DownTicks:
                        if (_downTicks.HasValue)
                            Process();
                        _downTicks = new int?(IFixReader.ReadInt());
                        return true;
                    case FixTags.TotalTicks:
                        if (_totalTicks.HasValue)
                            Process();
                        _totalTicks = new int?(IFixReader.ReadInt());
                        return true;
                    case FixTags.LevelPrice:
                        if (_levelPrice.HasValue)
                        {
                            ++_count;
                            _sides = new Sides?();
                        }
                        _levelPrice = new decimal?(_candlePriceLevels[_count].Price = IFixReader.ReadDecimal());
                        return true;
                    case FixTags.LevelType:
                        _sides = new Sides?(IFixReader.ReadChar().FromFixSide(true));
                        return true;
                    case FixTags.MDEntryPositionExtra:
                        if (_positionExtra.HasValue)
                            Process();
                        _positionExtra = new int?(IFixReader.ReadInt());
                        return true;
                    case FixTags.LevelPriceBuyCount:
                        _candlePriceLevels[_count].BuyCount = IFixReader.ReadInt();
                        return true;
                    case FixTags.LevelPriceSellCount:
                        _candlePriceLevels[_count].SellCount = IFixReader.ReadInt();
                        return true;
                    case FixTags.LevelPriceBuyVolume:
                        _candlePriceLevels[_count].BuyVolume = IFixReader.ReadDecimal();
                        return true;
                    case FixTags.LevelPriceSellVolume:
                        _candlePriceLevels[_count].SellVolume = IFixReader.ReadDecimal();
                        return true;
                    case FixTags.BuildFromType:
                        if (_myChar.HasValue)
                            Process();
                        _myChar = new char?(IFixReader.ReadChar());
                        return true;
                    case FixTags.BuildFromArg:
                        if (_buildFromType != null)
                            Process();
                        _buildFromType = IFixReader.ReadString();
                        return true;
                    default:
                        return false;
                }
            }
        }

        private bool? ProcessMarketDataSnapshotFullRefresh(
          IFixReader _param1,
          Action<Message> _param2,
          bool _param3)
        {
            MarketDataSnapshotFullRefreshReader ddDjxlwAhRbT20564_1 = new MarketDataSnapshotFullRefreshReader();
            ddDjxlwAhRbT20564_1._innerClass = this;
            ddDjxlwAhRbT20564_1._msgHandler = _param2;
            ddDjxlwAhRbT20564_1.IFixReader = _param1;
            ddDjxlwAhRbT20564_1._mdEntriesList = new List<MDEntry>();
            ddDjxlwAhRbT20564_1._lvl1ChangeMessages = new List<Level1ChangeMessage>();
            ddDjxlwAhRbT20564_1._candleMsgList = new List<CandleMessage>();
            ddDjxlwAhRbT20564_1._securityId = new SecurityId?();
            ddDjxlwAhRbT20564_1._originalTransactionId = new long?();
            ddDjxlwAhRbT20564_1._symbol = null;
            ddDjxlwAhRbT20564_1._securityExchange = null;
            ddDjxlwAhRbT20564_1._sendingTime = new DateTimeOffset();
            ddDjxlwAhRbT20564_1._date = new DateTimeOffset?();
            ddDjxlwAhRbT20564_1._period = new TimeSpan?();
            ddDjxlwAhRbT20564_1._mdEntryType = new char?();
            ddDjxlwAhRbT20564_1._price = new decimal?();
            ddDjxlwAhRbT20564_1._size = new decimal?();
            ddDjxlwAhRbT20564_1._openPrice = new decimal?();
            ddDjxlwAhRbT20564_1._highPrice = new decimal?();
            ddDjxlwAhRbT20564_1._lowPrice = new decimal?();
            ddDjxlwAhRbT20564_1._closePrice = new decimal?();
            ddDjxlwAhRbT20564_1._tradeId = null;
            ddDjxlwAhRbT20564_1._tickDirection = new char?();
            ddDjxlwAhRbT20564_1._position = new int?();
            ddDjxlwAhRbT20564_1._positionExtra = new int?();
            ddDjxlwAhRbT20564_1._action = new char?();
            ddDjxlwAhRbT20564_1._mdOtherValue = null;
            ddDjxlwAhRbT20564_1._currency = new CurrencyTypes?();
            ddDjxlwAhRbT20564_1._numberOfOrders = new decimal?();
            ddDjxlwAhRbT20564_1._candleState = new int?();
            ddDjxlwAhRbT20564_1._securityTradingStatus = new int?();
            ddDjxlwAhRbT20564_1._quoteCondition = null;
            ddDjxlwAhRbT20564_1._yield = new decimal?();
            ddDjxlwAhRbT20564_1._mdEntryOriginator = new char?();
            ddDjxlwAhRbT20564_1._lowVolume = new decimal?();
            ddDjxlwAhRbT20564_1._highVolume = new decimal?();
            ddDjxlwAhRbT20564_1._upTicks = new int?();
            ddDjxlwAhRbT20564_1._downTicks = new int?();
            ddDjxlwAhRbT20564_1._totalTicks = new int?();
            ddDjxlwAhRbT20564_1._candlePriceLevels = null;
            ddDjxlwAhRbT20564_1._levelPrice = new decimal?();
            ddDjxlwAhRbT20564_1._count = -1;
            ddDjxlwAhRbT20564_1._sides = new Sides?();
            ddDjxlwAhRbT20564_1._legIssueSize = -1;
            ddDjxlwAhRbT20564_1._myChar = new char?();
            ddDjxlwAhRbT20564_1._buildFromType = null;
            ddDjxlwAhRbT20564_1._mdEntryBuyer = null;
            ddDjxlwAhRbT20564_1._mdEntrySeller = null;
            ddDjxlwAhRbT20564_1._dataType = null;
            if (!ddDjxlwAhRbT20564_1.IFixReader.ReadMessage(new Func<FixTags, bool>(ddDjxlwAhRbT20564_1.ProcessFixTags)))
                return new bool?();
            if (ddDjxlwAhRbT20564_1._mdEntryType.HasValue)
                ddDjxlwAhRbT20564_1.Process();
            SecurityId securityId1;
            if (!ddDjxlwAhRbT20564_1._securityId.HasValue)
            {
                if (ddDjxlwAhRbT20564_1._symbol.IsEmpty() || ExchangeBoard.IsEmpty())
                {
                    this.AddErrorLog(LocalizedStrings.Str1615Params.Put(new object[1]
                    {
             ddDjxlwAhRbT20564_1._originalTransactionId
                    }));
                    return new bool?(true);
                }
                MarketDataSnapshotFullRefreshReader ddDjxlwAhRbT20564_2 = ddDjxlwAhRbT20564_1;
                securityId1 = new SecurityId();
                securityId1.SecurityCode = ddDjxlwAhRbT20564_1._symbol;
                securityId1.BoardCode = ExchangeBoard;
                SecurityId? nullable = new SecurityId?(securityId1);
                ddDjxlwAhRbT20564_2._securityId = nullable;
            }
            SecurityId securityId2 = ddDjxlwAhRbT20564_1._securityId.Value;
            securityId1 = new SecurityId();
            SecurityId securityId3 = securityId1;
            if (securityId2 == securityId3)
            {
                MarketDataSnapshotFullRefreshReader ddDjxlwAhRbT20564_2 = ddDjxlwAhRbT20564_1;
                securityId1 = new SecurityId();
                securityId1.SecurityCode = ddDjxlwAhRbT20564_1._symbol;
                securityId1.BoardCode = ddDjxlwAhRbT20564_1._securityExchange;
                SecurityId? nullable = new SecurityId?(securityId1);
                ddDjxlwAhRbT20564_2._securityId = nullable;
            }
            if (ddDjxlwAhRbT20564_1._mdEntriesList.Count > 0)
                ddDjxlwAhRbT20564_1._msgHandler(_innerClass.GetQuoteChangeMessage(ddDjxlwAhRbT20564_1._securityId.Value, ddDjxlwAhRbT20564_1._sendingTime, ddDjxlwAhRbT20564_1._mdEntriesList, _param3, ddDjxlwAhRbT20564_1._originalTransactionId, ddDjxlwAhRbT20564_1._dataType));
            Level1ChangeMessage message = null;
            foreach (Level1ChangeMessage level1ChangeMessage1 in ddDjxlwAhRbT20564_1._lvl1ChangeMessages)
            {
                if (message == null)
                {
                    Level1ChangeMessage level1ChangeMessage2 = new Level1ChangeMessage();
                    level1ChangeMessage2.SecurityId = ddDjxlwAhRbT20564_1._securityId.Value;
                    level1ChangeMessage2.ServerTime = level1ChangeMessage1.ServerTime;
                    level1ChangeMessage2.OriginalTransactionId = ddDjxlwAhRbT20564_1._originalTransactionId.GetValueOrDefault();
                    level1ChangeMessage2.BuildFrom = level1ChangeMessage1.BuildFrom;
                    message = level1ChangeMessage2;
                }
                foreach (KeyValuePair<Level1Fields, object> change in level1ChangeMessage1.Changes)
                    message.Changes[change.Key] = change.Value;
            }
            if (message != null)
            {
                if (ddDjxlwAhRbT20564_1._securityTradingStatus.HasValue)
                    message.TryAdd(Level1Fields.State, FromSecurityTradingStatus(ddDjxlwAhRbT20564_1._securityTradingStatus));
                ddDjxlwAhRbT20564_1._msgHandler(message);
            }
            foreach (CandleMessage candleMessage in ddDjxlwAhRbT20564_1._candleMsgList)
            {
                candleMessage.SecurityId = ddDjxlwAhRbT20564_1._securityId.Value;
                ddDjxlwAhRbT20564_1._msgHandler(candleMessage);
            }
            return new bool?(true);
        }

        private sealed class MarketDataRequestRejectReader
        {
            public string _originalTransactionId;
            public IFixReader IFixReader;
            public char? _mdReqRejReason;
            public string _text;

            internal bool ProcessFixTags(FixTags _param1)
            {
                switch (_param1)
                {
                    case FixTags.Text:
                        _text = IFixReader.ReadString();
                        return true;
                    case FixTags.MDReqID:
                        _originalTransactionId = IFixReader.ReadString();
                        return true;
                    case FixTags.MDReqRejReason:
                        _mdReqRejReason = new char?(IFixReader.ReadChar());
                        return true;
                    default:
                        return false;
                }
            }
        }

        private static bool? ProcessMarketDataRequestReject(
          IFixReader _param0,
          Action<Message> _param1)
        {
            MarketDataRequestRejectReader reader = new MarketDataRequestRejectReader();
            reader.IFixReader = _param0;
            reader._originalTransactionId = null;
            reader._mdReqRejReason = new char?();
            reader._text = null;
            if (!reader.IFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            char? zxkcq6PcsPfwVeVbYlg = reader._mdReqRejReason;
            int? nullable = zxkcq6PcsPfwVeVbYlg.HasValue ? new int?(zxkcq6PcsPfwVeVbYlg.GetValueOrDefault()) : new int?();
            bool flag = nullable.GetValueOrDefault() == 56 & nullable.HasValue;
            Action<Message> action = _param1;
            SubscriptionResponseMessage subscriptionResponseMessage1 = new SubscriptionResponseMessage();
            subscriptionResponseMessage1.OriginalTransactionId = reader._originalTransactionId.To<long>();
            SubscriptionResponseMessage subscriptionResponseMessage2 = subscriptionResponseMessage1;
            SystemException systemException;
            if (!flag)
                systemException = new InvalidOperationException(LocalizedStrings.Str1616Params.Put(new object[2]
                {
           reader._mdReqRejReason,
           reader._text
                }));
            else
                systemException = SubscriptionResponseMessage.NotSupported;
            subscriptionResponseMessage2.Error = systemException;
            SubscriptionResponseMessage subscriptionResponseMessage3 = subscriptionResponseMessage1;
            action(subscriptionResponseMessage3);
            return new bool?(true);
        }

        private bool? ProcessNews(IFixReader _param1, Action<Message> _param2) => _param1.ReadNews(_param2, TimeStampParser);

        private bool? DefaultHandler(IFixReader _param1, Action<Message> _param2)
        {
            bool? lastFragment2;
            long? securityReqId2;
            string reason;
            string text2;
            bool? nullable1 = _param1.ReadSecurityMessage(DateParser, YearMonthParser, _longRefPairMap, new Action<SecurityMessage, string, string, string, string>(InitSecId), new Action<Exception>(this.AddErrorLog), new Func<FixTags, IFixReader, SecurityMessage, bool>(ProcessSecurityDefinition), _param2, new Func<string, SecurityTypes?>(GetSecurityType), out lastFragment2, out securityReqId2, out reason, out text2);
            if (!nullable1.HasValue)
                return new bool?();
            bool? nullable2 = nullable1;
            bool flag1 = false;
            if (!(nullable2.GetValueOrDefault() == flag1 & nullable2.HasValue))
            {
                nullable2 = lastFragment2;
                bool flag2 = true;
                if (!(nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue))
                    goto label_7;
            }
            long valueOrDefault = securityReqId2.GetValueOrDefault();
            nullable2 = nullable1;
            bool flag3 = false;
            if (nullable2.GetValueOrDefault() == flag3 & nullable2.HasValue)
            {
                _param2(valueOrDefault.CreateSubscriptionResponse(new InvalidOperationException(string.Concat(new string[5]
                {
                    "Error lookup code ",
                    reason,
                    ". Text '",
                    text2,
                    "'."
                }))));
            }
            else
            {
                Action<Message> action = _param2;
                SubscriptionFinishedMessage subscriptionFinishedMessage = new SubscriptionFinishedMessage();
                subscriptionFinishedMessage.OriginalTransactionId = valueOrDefault;
                action(subscriptionFinishedMessage);
            }
        label_7:
            return new bool?(true);
        }

        /// <summary>
        /// Convert <see cref="T:System.String" /> to <see cref="T:StockSharp.Messages.SecurityTypes" /> value.
        /// </summary>
        /// <param name="type">
        /// <see cref="T:System.String" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Messages.SecurityTypes" /> value.</returns>
        protected virtual SecurityTypes? GetSecurityType(string type) => type.FromFixType();

        /// <summary>
        /// Process <see cref="F:StockSharp.Fix.Native.FixMessages.SecurityDefinition" /> message.
        /// </summary>
        /// <param name="tag">Tag.</param>
        /// <param name="reader">The reader of data recorded in the FIX protocol format.</param>
        /// <param name="message">A message containing info about the security.</param>
        /// <returns>Processing result.</returns>
        protected virtual bool ProcessSecurityDefinition(FixTags tag, IFixReader reader, SecurityMessage message)
        {
            return false;
        }

        /// <summary>Init security id information.</summary>
        /// <param name="message">A message containing info about the security.</param>
        /// <param name="symbol">Symbol.</param>
        /// <param name="securityExchange">Security exchange.</param>
        /// <param name="idSource">Id source.</param>
        /// <param name="idValue">Id value.</param>
        protected virtual void InitSecId(SecurityMessage message, string symbol, string securityExchange, string idSource, string idValue)
        {
            message.InitSecId(symbol, securityExchange, idSource, idValue);
        }

        private sealed class QuoteRequestRejectReader
        {
            public string _quoteReqID;
            public IFixReader IFixReader;
            public int? _quoteRequestRejectReason;
            public List<string> _symbolsList;

            internal bool ProcessFixTags(FixTags tag)
            {
                switch (tag)
                {
                    case FixTags.Symbol:
                        _symbolsList.Add(IFixReader.ReadString());
                        return true;
                    case FixTags.QuoteReqID:
                        _quoteReqID = IFixReader.ReadString();
                        return true;
                    case FixTags.NoRelatedSym:
                        IFixReader.ReadInt();
                        return true;
                    case FixTags.QuoteRequestRejectReason:
                        _quoteRequestRejectReason = new int?(IFixReader.ReadInt());
                        return true;
                    default:
                        return false;
                }
            }
        }

        private static bool? ProcessQuoteRequestReject(IFixReader _param0, Action<Message> _param1)
        {
            QuoteRequestRejectReader reader = new QuoteRequestRejectReader();
            reader.IFixReader = _param0;
            reader._quoteRequestRejectReason = new int?();
            reader._quoteReqID = null;
            reader._symbolsList = new List<string>();
            if (!reader.IFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            _param1(LocalizedStrings.Str1614Params.Put(reader._quoteReqID, reader._symbolsList.JoinComma(), reader._quoteRequestRejectReason).ToErrorMessage());
            return new bool?(true);
        }


        private sealed class QuoteStatusReportReader
        {
            public string _quoteStatusReqID;
            public IFixReader IFixReader;
            public string _quoteReqID;
            public string _quoteID;
            public int? _quoteType;
            public string _symbol;
            public string _securityExchange;
            public DateTimeOffset? _sendingTime;
            public BaseFixDialect _innerClass;

            internal bool ProcessFixTags(FixTags _param1)
            {
                switch (_param1)
                {
                    case FixTags.SendingTime:
                        // ISSUE: explicit non-virtual call
                        _sendingTime = new DateTimeOffset?(IFixReader.ReadUtc(_innerClass.TimeStampParser));
                        return true;
                    case FixTags.Symbol:
                        _symbol = IFixReader.ReadString();
                        return true;
                    case FixTags.QuoteID:
                        _quoteID = IFixReader.ReadString();
                        return true;
                    case FixTags.QuoteReqID:
                        _quoteReqID = IFixReader.ReadString();
                        return true;
                    case FixTags.SecurityExchange:
                        _securityExchange = IFixReader.ReadString();
                        return true;
                    case FixTags.QuoteType:
                        _quoteType = new int?(IFixReader.ReadInt());
                        return true;
                    case FixTags.QuoteStatusReqID:
                        _quoteStatusReqID = IFixReader.ReadString();
                        return true;
                    default:
                        return false;
                }
            }
        }

        private bool? ProcessQuoteStatusReport(IFixReader _param1, Action<Message> _param2)
        {
            QuoteStatusReportReader reader = new QuoteStatusReportReader();
            reader.IFixReader = _param1;
            reader._innerClass = this;
            reader._quoteStatusReqID = null;
            reader._quoteReqID = null;
            reader._quoteID = null;
            reader._symbol = null;
            reader._securityExchange = null;
            reader._quoteType = new int?();
            reader._sendingTime = new DateTimeOffset?();
            if (!reader.IFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            if (!reader._sendingTime.HasValue)
                throw new InvalidOperationException();
            Action<Message> action = _param2;
            Level1ChangeMessage message = new Level1ChangeMessage();
            message.ServerTime = reader._sendingTime.Value;
            message.SecurityId = new SecurityId()
            {
                SecurityCode = reader._symbol,
                BoardCode = reader._securityExchange ?? ExchangeBoard ?? "ALL"
            };
            Level1ChangeMessage level1ChangeMessage = message.TryAdd(Level1Fields.State, reader._quoteType.FromQuoteType());
            action(level1ChangeMessage);
            return new bool?(true);
        }


        private sealed class SecurityStatusReader
        {
            public string _securityStatusReqID;
            public IFixReader IFixReader;
            public string _symbol;
            public string _securityExchange;
            public int? _securityTradingStatus;
            public decimal? _highPrice;
            public decimal? _lowPrice;
            public decimal? _closePrice;
            public DateTimeOffset? _sendingTime;
            public BaseFixDialect _innerClass;

            internal bool ProcessFixTags(FixTags _param1)
            {
                switch (_param1)
                {
                    case FixTags.LastPx:
                        _closePrice = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.SendingTime:
                        // ISSUE: explicit non-virtual call
                        _sendingTime = new DateTimeOffset?(IFixReader.ReadUtc(_innerClass.TimeStampParser));
                        return true;
                    case FixTags.Symbol:
                        _symbol = IFixReader.ReadString();
                        return true;
                    case FixTags.SecurityExchange:
                        _securityExchange = IFixReader.ReadString();
                        return true;
                    case FixTags.SecurityStatusReqID:
                        _securityStatusReqID = IFixReader.ReadString();
                        return true;
                    case FixTags.SecurityTradingStatus:
                        _securityTradingStatus = new int?(IFixReader.ReadInt());
                        return true;
                    case FixTags.HighPx:
                        _highPrice = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.LowPx:
                        _lowPrice = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    default:
                        return false;
                }
            }
        }

        private bool? ProcessSecurityStatus(IFixReader _param1, Action<Message> _param2)
        {
            SecurityStatusReader reader = new SecurityStatusReader();
            reader.IFixReader = _param1;
            reader._innerClass = this;
            reader._securityStatusReqID = null;
            reader._symbol = null;
            reader._securityExchange = null;
            reader._sendingTime = new DateTimeOffset?();
            reader._securityTradingStatus = new int?();
            reader._highPrice = new decimal?();
            reader._lowPrice = new decimal?();
            reader._closePrice = new decimal?();
            if (!reader.IFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            if (!reader._sendingTime.HasValue)
                throw new InvalidOperationException();
            Action<Message> action = _param2;
            Level1ChangeMessage message = new Level1ChangeMessage();
            message.ServerTime = reader._sendingTime.Value;
            message.SecurityId = new SecurityId()
            {
                SecurityCode = reader._symbol,
                BoardCode = reader._securityExchange ?? ExchangeBoard ?? "ALL"
            };
            Level1ChangeMessage level1ChangeMessage = message.TryAdd(Level1Fields.HighPrice, reader._highPrice).TryAdd(Level1Fields.LowPrice, reader._lowPrice).TryAdd(Level1Fields.ClosePrice, reader._closePrice).TryAdd(Level1Fields.State, FromSecurityTradingStatus(reader._securityTradingStatus));
            action(level1ChangeMessage);
            return new bool?(true);
        }

        /// <summary>
        /// Convert <see cref="F:StockSharp.Fix.Native.FixTags.SecurityTradingStatus" /> to <see cref="T:StockSharp.Messages.SecurityStates" /> value.
        /// </summary>
        /// <param name="status">
        /// <see cref="F:StockSharp.Fix.Native.FixTags.SecurityTradingStatus" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Messages.SecurityStates" /> value.</returns>
        protected virtual SecurityStates? FromSecurityTradingStatus(int? status)
        {
            if (!status.HasValue)
                return new SecurityStates?();
            return status.GetValueOrDefault() == 7 ? new SecurityStates?(SecurityStates.Trading) : new SecurityStates?(SecurityStates.Stoped);
        }

        private sealed class TradingSessionStatusReader
        {
            public string _tradingSessionID;
            public IFixReader IFixReader;
            public TradSesStatus? _tradSesStatus;
            public TradSesStatusRejReason? _tradSesStatusRejReason;
            public string _tradSesReqID;
            public DateTimeOffset? _period;
            public BaseFixDialect _innerClass;
            public DateTimeOffset? _sendingTime;

            internal bool ProcessFixTags(FixTags _param1)
            {
                switch (_param1)
                {
                    case FixTags.SendingTime:
                        // ISSUE: explicit non-virtual call
                        _sendingTime = new DateTimeOffset?(IFixReader.ReadUtc(_innerClass.TimeStampParser));
                        return true;
                    case FixTags.MDEntryTime:
                        // ISSUE: explicit non-virtual call
                        _period = new DateTimeOffset?(IFixReader.ReadUtc(_innerClass.TimeStampParser));
                        return true;
                    case FixTags.TradSesReqID:
                        _tradSesReqID = IFixReader.ReadString();
                        return true;
                    case FixTags.TradingSessionID:
                        _tradingSessionID = IFixReader.ReadString();
                        return true;
                    case FixTags.TradSesStatus:
                        _tradSesStatus = new TradSesStatus?((TradSesStatus)IFixReader.ReadInt());
                        return true;
                    case FixTags.TradSesStatusRejReason:
                        _tradSesStatusRejReason = new TradSesStatusRejReason?((TradSesStatusRejReason)IFixReader.ReadInt());
                        return true;
                    default:
                        return false;
                }
            }
        }









        private bool? ProcessTradingSessionStatus(IFixReader _param1, Action<Message> _param2)
        {
            TradingSessionStatusReader reader = new TradingSessionStatusReader();
            reader.IFixReader = _param1;
            reader._innerClass = this;
            reader._tradingSessionID = null;
            reader._tradSesStatus = new TradSesStatus?();
            reader._tradSesStatusRejReason = new TradSesStatusRejReason?();
            reader._tradSesReqID = null;
            reader._period = new DateTimeOffset?();
            reader._sendingTime = new DateTimeOffset?();
            if (!reader.IFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            TradSesStatus? fgwe8iTreMaVsOgA = reader._tradSesStatus;
            if (fgwe8iTreMaVsOgA.GetValueOrDefault() == TradSesStatus.RequestRejected & fgwe8iTreMaVsOgA.HasValue)
            {
                string str1624Params = LocalizedStrings.Str1624Params;
                object[] objArray = new object[2]
        {
           reader._tradingSessionID,
          null
        };
                TradSesStatusRejReason? bdrtfT6VgUtP7Aavc = reader._tradSesStatusRejReason;
                objArray[1] = (bdrtfT6VgUtP7Aavc.GetValueOrDefault() == TradSesStatusRejReason.InvalidSession & bdrtfT6VgUtP7Aavc.HasValue ? LocalizedStrings.Str1625 : LocalizedStrings.Str1626);
                string description = str1624Params.Put(objArray);
                _param2(description.ToErrorMessage());
            }
            else
            {
                Action<Message> action = _param2;
                BoardStateMessage boardStateMessage = new BoardStateMessage();
                boardStateMessage.ServerTime = reader._period ?? reader._sendingTime.Value;
                boardStateMessage.BoardCode = ExchangeBoard ?? reader._tradingSessionID;
                ref TradSesStatus? local = ref reader._tradSesStatus;
                boardStateMessage.State = local.HasValue ? local.GetValueOrDefault().FromFixStatus() : SessionStates.Active;
                action(boardStateMessage);
            }
            return new bool?(true);
        }

        /// <summary>
        /// Write <see cref="F:StockSharp.Fix.Native.FixTags.Account" /> value.
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="message">Message.</param>
        protected void WriteAccount(IFixWriter writer, IPortfolioNameMessage message)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));
            string str = message != null ? message.PortfolioName : throw new ArgumentNullException(nameof(message));
            if (DoNotSendAccount || str.IsEmpty() || IsSyntheticPortfolioName(str))
                return;
            writer.Write(FixTags.Account);
            writer.Write(message.PortfolioName);
        }

        /// <summary>
        /// Get synthetic portfolio name for <see cref="P:StockSharp.Fix.Dialects.BaseFixDialect.LoginAsPortfolioName" /> mode.
        /// </summary>
        /// <returns>Portfolio code name.</returns>
        protected string GetSyntheticPortfolioName()
        {
            string str = Login;
            if (str.IsEmpty())
                str = SenderCompId;
            return str;
        }

        /// <summary>
        /// Is the specified portfolio name generated by <see cref="M:StockSharp.Fix.Dialects.BaseFixDialect.GetSyntheticPortfolioName" />.
        /// </summary>
        /// <param name="portfolioName">Portfolio code name.</param>
        /// <returns>Check result.</returns>
        protected bool IsSyntheticPortfolioName(string portfolioName) => LoginAsPortfolioName && portfolioName.CompareIgnoreCase(GetSyntheticPortfolioName());

        private string GetSyntheticPortfolioName(string _param1) => !LoginAsPortfolioName ? _param1 : GetSyntheticPortfolioName();

        private sealed class PositionReportReader
        {
            public string _account;
            public IFixReader IFixReader;
            public int? _posReqResult;
            public DateTimeOffset _sendingTime;
            public BaseFixDialect _innerClass;
            public CurrencyTypes? _currency;
            public string _symbol;
            public string _securityExchange;
            public string _exDestination;
            public string _tradingSessionID;
            public decimal? _contractMultiplier;
            public TPlusLimits? _acctIDSource;
            public string _clientID;
            public string _strategyTypeId;
            public char? _side;
            public long? _posReqID;
            public char? _buildFromSource;
            public string _buildFromType;
            public RefPair<string, decimal?>[] _refPair;
            public RefTriple<string, decimal?, decimal?>[] _postionsTriple;
            public int _positionCount;
            public int _count;
            public string _execBroker;
            public string _text;

            internal bool ProcessFixTags(FixTags _param1)
            {
                switch (_param1)
                {
                    case FixTags.Account:
                        _account = IFixReader.ReadString();
                        return true;
                    case FixTags.Currency:
                        _currency = IFixReader.ReadString().FromMicexCurrencyName(new Action<Exception>(_innerClass.AddErrorLog));
                        return true;
                    case FixTags.SendingTime:
                        // ISSUE: explicit non-virtual call
                        _sendingTime = IFixReader.ReadUtc(_innerClass.TimeStampParser);
                        return true;
                    case FixTags.Side:
                        _side = new char?(IFixReader.ReadChar());
                        return true;
                    case FixTags.Symbol:
                        _symbol = IFixReader.ReadString();
                        return true;
                    case FixTags.Text:
                        _text = IFixReader.ReadString();
                        return true;
                    case FixTags.ExecBroker:
                        _execBroker = IFixReader.ReadString();
                        return true;
                    case FixTags.ExDestination:
                        _exDestination = IFixReader.ReadString();
                        return true;
                    case FixTags.ClientID:
                        _clientID = IFixReader.ReadString();
                        return true;
                    case FixTags.SecurityType:
                        IFixReader.ReadString().FromFixType();
                        return true;
                    case FixTags.SecurityExchange:
                        _securityExchange = IFixReader.ReadString();
                        return true;
                    case FixTags.ContractMultiplier:
                        _contractMultiplier = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.TradingSessionID:
                        _tradingSessionID = IFixReader.ReadString();
                        return true;
                    case FixTags.AcctIDSource:
                        _acctIDSource = new TPlusLimits?(IFixReader.ReadInt().To<TPlusLimits>());
                        return true;
                    case FixTags.NoPositions:
                        _postionsTriple = new RefTriple<string, decimal?, decimal?>[IFixReader.ReadInt()];
                        for (int index = 0; index < _postionsTriple.Length; ++index)
                            _postionsTriple[index] = new RefTriple<string, decimal?, decimal?>();
                        return true;
                    case FixTags.PosType:
                        if (_postionsTriple[_count].First != null)
                            ++_count;
                        _postionsTriple[_count].First = IFixReader.ReadString();
                        return true;
                    case FixTags.LongQty:
                        if (_postionsTriple[_count].Second.HasValue)
                            ++_count;
                        _postionsTriple[_count].Second = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.ShortQty:
                        if (_postionsTriple[_count].Third.HasValue)
                            ++_count;
                        _postionsTriple[_count].Third = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.PosAmtType:
                        if (_refPair[_positionCount].First != null)
                            ++_positionCount;
                        _refPair[_positionCount].First = IFixReader.ReadString();
                        return true;
                    case FixTags.PosAmt:
                        if (_refPair[_positionCount].Second.HasValue)
                            ++_positionCount;
                        _refPair[_positionCount].Second = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.PosReqID:
                        _posReqID = new long?(IFixReader.ReadString().To<long>());
                        return true;
                    case FixTags.PosReqResult:
                        _posReqResult = new int?(IFixReader.ReadInt());
                        return true;
                    case FixTags.NoPosAmt:
                        _refPair = new RefPair<string, decimal?>[IFixReader.ReadInt()];
                        for (int index = 0; index < _refPair.Length; ++index)
                            _refPair[index] = new RefPair<string, decimal?>();
                        return true;
                    case FixTags.StrategyTypeId:
                        _strategyTypeId = IFixReader.ReadString();
                        return true;
                    case FixTags.BuildFromType:
                        _buildFromSource = new char?(IFixReader.ReadChar());
                        return true;
                    case FixTags.BuildFromArg:
                        _buildFromType = IFixReader.ReadString();
                        return true;
                    default:
                        return false;
                }
            }
        }

        private bool? ProcessPositionReport(IFixReader fixReader, Action<Message> handler)
        {
            PositionReportReader reader = new PositionReportReader();
            reader.IFixReader = fixReader;
            reader._innerClass = this;
            reader._account = null;
            reader._posReqResult = new int?();
            reader._sendingTime = new DateTimeOffset();
            reader._currency = new CurrencyTypes?();
            reader._symbol = null;
            reader._securityExchange = null;
            reader._exDestination = null;
            reader._tradingSessionID = null;
            reader._contractMultiplier = new decimal?();
            reader._acctIDSource = new TPlusLimits?();
            reader._clientID = null;
            reader._strategyTypeId = null;
            reader._side = new char?();
            reader._posReqID = new long?();
            reader._refPair = ArrayHelper.Empty<RefPair<string, decimal?>>();
            reader._postionsTriple = ArrayHelper.Empty<RefTriple<string, decimal?, decimal?>>();
            reader._count = 0;
            reader._positionCount = 0;
            reader._buildFromSource = new char?();
            reader._buildFromType = null;
            reader._execBroker = null;
            reader._text = null;

            if (!reader.IFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();

            if (reader._posReqResult.HasValue)
            {
                int? posReq = reader._posReqResult;
                int num = 0;

                if (!(posReq.GetValueOrDefault() == num & posReq.HasValue))
                {
                    handler(LocalizedStrings.Str1699Params.Put(reader._account, reader._posReqResult).ToErrorMessage());

                    return new bool?(true);
                }
            }
            reader._account = GetSyntheticPortfolioName(reader._account);
            PositionChangeMessage msg = new PositionChangeMessage();
            msg.PortfolioName = reader._account;
            msg.ServerTime = reader._sendingTime;
            msg.ClientCode = reader._clientID;
            msg.StrategyId = reader._strategyTypeId;
            ref char? mySide = ref reader._side;
            msg.Side = mySide.HasValue ? new Sides?(mySide.GetValueOrDefault().FromFixSide(true)) : new Sides?();
            ref char? buildfrom = ref reader._buildFromSource;
            msg.BuildFrom = buildfrom.HasValue ? buildfrom.GetValueOrDefault().ToDataType(reader._buildFromType) : null;
            msg.DepoName = reader._execBroker;
            msg.Description = reader._text;
            PositionChangeMessage message = msg;
            if (reader._symbol == SecurityId.Money.SecurityCode)
            {
                message.SecurityId = SecurityId.Money;
                if (reader._currency.HasValue)
                    message.Add(PositionChangeTypes.Currency, reader._currency.Value);

                message.BoardCode = reader._securityExchange;

                foreach (var refPair in reader._refPair)
                {
                    string first = refPair.First;

                    if (first == "COMM")
                    {
                        message.Add(PositionChangeTypes.Commission, refPair.Second);
                        break;
                    }

                    if (first == "UPNL")
                    {
                        message.Add(PositionChangeTypes.UnrealizedPnL, refPair.Second);
                        break;
                    }

                    if (first == "RPNL")
                    {
                        message.Add(PositionChangeTypes.RealizedPnL, refPair.Second);
                        break;
                    }

                    if (first == "LVRG")
                    {
                        message.Add(PositionChangeTypes.Leverage, refPair.Second);
                        break;
                    }

                    if (first == "VADJ")
                    {
                        message.Add(PositionChangeTypes.BlockedValue, refPair.Second);
                        break;
                    }

                    if (first == "SMTM")
                    {
                        message.Add(PositionChangeTypes.BeginValue, refPair.Second);
                        break;
                    }

                    if (first == "CASH")
                    {
                        message.Add(PositionChangeTypes.AveragePrice, refPair.Second);
                        break;
                    }

                    if (first == "CRES")
                    {
                        message.Add(PositionChangeTypes.CurrentValue, refPair.Second);
                        break;
                    }

                    if (first == "COLAT")
                    {
                        message.Add(PositionChangeTypes.CurrentPrice, refPair.Second);
                        break;
                    }

                    if (first == "TVAR")
                    {
                        message.Add(PositionChangeTypes.VariationMargin, refPair.Second);
                        break;
                    }
                }
                handler(message);
                return new bool?(true);
            }
            decimal mulitiplier = reader._contractMultiplier ?? 1M;
            message.SecurityId = new SecurityId()
            {
                SecurityCode = reader._symbol,
                BoardCode = GetBoardCode(reader._exDestination, reader._securityExchange, reader._tradingSessionID)
            };
            message.LimitType = reader._acctIDSource;
            message.OriginalTransactionId = reader._posReqID.GetValueOrDefault();
            foreach (RefTriple<string, decimal?, decimal?> refTriple in reader._postionsTriple)
            {
                decimal? longQty = refTriple.Second;
                decimal? shortQty = refTriple.Third;

                if (shortQty.HasValue)
                {
                    if (longQty.HasValue)
                    {
                        shortQty = longQty;
                        if (!(shortQty.GetValueOrDefault() == 0M & shortQty.HasValue))
                            goto label_39;
                    }

                    longQty = refTriple.Third;
                    shortQty = longQty;

                    if (shortQty.GetValueOrDefault() > 0M & shortQty.HasValue)
                    {
                        shortQty = longQty;
                        decimal? oldLongQty = longQty;
                        longQty = shortQty.HasValue & oldLongQty.HasValue ? new decimal?(shortQty.GetValueOrDefault() - oldLongQty.GetValueOrDefault()) : new decimal?();
                    }
                }
            label_39:
                if (longQty.HasValue)
                {
                    string posType = refTriple.First;

                    if (posType == PosType.StartOfDayQty)
                    {
                        message.Add(PositionChangeTypes.BeginValue, longQty.Value / mulitiplier);
                        continue;
                    }

                    if (posType == PosType.ExchangeForPhysicalQty)
                    {
                        message.Add(PositionChangeTypes.CurrentValue, longQty.Value / mulitiplier);
                        continue;
                    }

                    if (posType == PosType.TotalTransactionQty)
                    {
                        message.Add(PositionChangeTypes.CurrentPrice, longQty.Value);
                        continue;
                    }

                    if (posType == PosType.DeliveryQty)
                    {
                        message.Add(PositionChangeTypes.AveragePrice, longQty.Value);
                        continue;
                    }

                    if (posType == PosType.AdjustmentQty)
                    {
                        message.Add(PositionChangeTypes.BlockedValue, longQty.Value / mulitiplier);
                        continue;
                    }

                    if (posType == Native.Extensions.Commission)
                    {
                        message.Add(PositionChangeTypes.Commission, longQty.Value);
                        continue;
                    }

                    if (posType == Native.Extensions.UnrealizedPnL)
                    {
                        message.Add(PositionChangeTypes.UnrealizedPnL, longQty.Value);
                        continue;
                    }

                    if (posType == Native.Extensions.RealizedPnL)
                    {
                        message.Add(PositionChangeTypes.RealizedPnL, longQty.Value);
                        continue;
                    }

                    if (posType == Native.Extensions.Leverage)
                    {
                        message.Add(PositionChangeTypes.Leverage, longQty.Value);
                        continue;
                    }
                }
            }
            if (message.Changes.Count > 0)
                handler(message);
            return new bool?(true);
        }

        private sealed class OrderMassCancelReportReader
        {
            public char? _massCancelResponse;
            public IFixReader IFixReader;
            public int? _massCancelRejectReason;
            public string _text;

            internal bool ProcessFixTags(FixTags _param1)
            {
                switch (_param1)
                {
                    case FixTags.Text:
                        _text = IFixReader.ReadString();
                        return true;
                    case FixTags.MassCancelResponse:
                        _massCancelResponse = new char?(IFixReader.ReadChar());
                        return true;
                    case FixTags.MassCancelRejectReason:
                        _massCancelRejectReason = new int?(IFixReader.ReadInt());
                        return true;
                    default:
                        return false;
                }
            }
        }

        private static bool? ProcessOrderMassCancelReport(IFixReader _param0, Action<Message> _param1)
        {
            OrderMassCancelReportReader reader = new OrderMassCancelReportReader();
            reader.IFixReader = _param0;
            reader._massCancelResponse = new char?();
            reader._massCancelRejectReason = new int?();
            reader._text = null;
            if (!reader.IFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            char? z0TnPo1CkbCkDeqw = reader._massCancelResponse;
            int? nullable = z0TnPo1CkbCkDeqw.HasValue ? new int?(z0TnPo1CkbCkDeqw.GetValueOrDefault()) : new int?();
            if (nullable.GetValueOrDefault() == 48 & nullable.HasValue)
                _param1(LocalizedStrings.Str1697Params.Put(new object[2]
                {
           reader._text,
           reader._massCancelRejectReason
        }).ToErrorMessage());
            return new bool?(true);
        }


        private sealed class ReportReader
        {
            public ExecutionReport _report;
            public IFixReader IFixReader;
            public BaseFixDialect _innerClass;
            public FastDateTimeParser _parser;
            public int _count;
            public Func<FixTags, IFixReader, ExecutionReport, bool> _handler;

            internal bool ProcessFixTags(FixTags _param1)
            {
                switch (_param1)
                {
                    case FixTags.Account:
                        _report.Account = IFixReader.ReadString();
                        return true;
                    case FixTags.AvgPx:
                        _report.AvgPx = IFixReader.ReadDecimal().DefaultAsNull();
                        return true;
                    case FixTags.ClOrdID:
                        _report.ClOrdId = IFixReader.ReadString();
                        return true;
                    case FixTags.Commission:
                        _report.Commission = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.CumQty:
                        _report.CumQty = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.ExecID:
                        _report.ExecId = IFixReader.ReadString();
                        return true;
                    case FixTags.IDSource:
                        _report.SecurityIdSource = IFixReader.ReadString();
                        return true;
                    case FixTags.LastCapacity:
                        _report.LastCapacity = new char?(IFixReader.ReadChar());
                        return true;
                    case FixTags.LastPx:
                        _report.LastPx = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.LastQty:
                        _report.LastQty = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.MsgSeqNum:
                        _report.MsgSeqNum = IFixReader.ReadLong();
                        return true;
                    case FixTags.OrderID:
                        _report.OrderId = IFixReader.ReadString();
                        return true;
                    case FixTags.OrderQty:
                        _report.OrderQty = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.OrdStatus:
                        _report.OrdStatus = new char?(IFixReader.ReadChar());
                        return true;
                    case FixTags.OrdType:
                        _report.OrdType = new char?(IFixReader.ReadChar());
                        return true;
                    case FixTags.OrigClOrdID:
                        _report.OrigClOrdId = IFixReader.ReadString();
                        return true;
                    case FixTags.PossDupFlag:
                        _report.PossDupFlag = new bool?(IFixReader.ReadBool());
                        return true;
                    case FixTags.Price:
                        _report.Price = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.SecurityID:
                        _report.SecurityId = IFixReader.ReadString();
                        return true;
                    case FixTags.SendingTime:
                        // ISSUE: explicit non-virtual call
                        _report.SendingTime = IFixReader.ReadUtc(_innerClass.TimeStampParser).UtcDateTime;
                        return true;
                    case FixTags.Side:
                        _report.Side = new char?(IFixReader.ReadChar());
                        return true;
                    case FixTags.Symbol:
                        _report.Symbol = IFixReader.ReadString();
                        return true;
                    case FixTags.Text:
                        _report.Text = IFixReader.ReadString();
                        return true;
                    case FixTags.TimeInForce:
                        _report.TimeInForce = new char?(IFixReader.ReadChar());
                        return true;
                    case FixTags.TransactTime:
                        _report.TransactTime = new DateTime?(IFixReader.ReadUtc(_parser).UtcDateTime);
                        return true;
                    case FixTags.TradeDate:
                        // ISSUE: explicit non-virtual call
                        // ISSUE: explicit non-virtual call
                        _report.TradeDate = new DateTime?(IFixReader.ReadDateTime(_innerClass.DateParser).ApplyTimeZone(_innerClass.TimeZone).UtcDateTime);
                        return true;
                    case FixTags.ExecBroker:
                        _report.ExecBroker = IFixReader.ReadString();
                        return true;
                    case FixTags.PositionEffect:
                        _report.PositionEffect = new char?(IFixReader.ReadChar());
                        return true;
                    case FixTags.StopPx:
                        _report.StopPx = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.ExDestination:
                        _report.ExDestination = IFixReader.ReadString();
                        return true;
                    case FixTags.ClientID:
                        _report.ClientId = IFixReader.ReadString();
                        return true;
                    case FixTags.MinQty:
                        _report.MinQty = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.MaxFloor:
                        _report.MaxFloor = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.OrigSendingTime:
                        _report.OrigSendingTime = new DateTime?(IFixReader.ReadUtc(_parser).UtcDateTime);
                        return true;
                    case FixTags.ExpireTime:
                        // ISSUE: explicit non-virtual call
                        _report.ExpireTime = new DateTime?(IFixReader.ReadUtc(_innerClass.TimeStampParser).UtcDateTime);
                        return true;
                    case FixTags.ExecType:
                        _report.ExecType = new char?(IFixReader.ReadChar());
                        return true;
                    case FixTags.LeavesQty:
                        _report.LeavesQty = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.SecondaryOrderID:
                        _report.SecondaryOrderId = IFixReader.ReadString();
                        return true;
                    case FixTags.SecurityExchange:
                        _report.SecurityExchange = IFixReader.ReadString();
                        return true;
                    case FixTags.Yield:
                        _report.Yield = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.TradingSessionID:
                        _report.TradingSessionId = IFixReader.ReadString();
                        return true;
                    case FixTags.ExpireDate:
                        // ISSUE: explicit non-virtual call
                        // ISSUE: explicit non-virtual call
                        _report.ExpireDate = new DateTime?(IFixReader.ReadDateTime(_innerClass.DateParser).ApplyTimeZone(_innerClass.TimeZone).UtcDateTime);
                        return true;
                    case FixTags.PartyID:
                        switch (_report.Parties[_count]?.PartyId)
                        {
                            case null:
                                (_report.Parties[_count] ?? (_report.Parties[_count] = new Party())).PartyId = IFixReader.ReadString();
                                return true;
                            default:
                                ++_count;
                                goto case null;
                        }
                    case FixTags.PartyRole:
                        Party party = _report.Parties[_count];
                        if ((party != null ? (party.PartyRole.HasValue ? 1 : 0) : 0) != 0)
                            ++_count;
                        (_report.Parties[_count] ?? (_report.Parties[_count] = new Party())).PartyRole = new PartyRole?((PartyRole)IFixReader.ReadInt());
                        return true;
                    case FixTags.NoPartyIDs:
                        _report.Parties = new Party[IFixReader.ReadInt()];
                        return true;
                    case FixTags.OrderCapacity:
                        _report.OrderCapacity = new char?(IFixReader.ReadChar());
                        return true;
                    case FixTags.OrderRestrictions:
                        _report.OrderRestrictions = IFixReader.ReadString();
                        return true;
                    case FixTags.CashMargin:
                        _report.CashMargin = new char?(IFixReader.ReadChar());
                        return true;
                    case FixTags.MassStatusReqID:
                        _report.MassStatusReqId = IFixReader.ReadString();
                        return true;
                    case FixTags.NoTrdRegTimestamps:
                        _report.TimeInForce = new char?(IFixReader.ReadChar());
                        return true;
                    case FixTags.TrdRegTimestamp:
                        _report.TrdRegTimestamp = new DateTime?(IFixReader.ReadUtc(_parser).UtcDateTime);
                        return true;
                    case FixTags.TrdRegTimestampType:
                        _report.TrdRegTimestampType = new TrdRegTimestampType?((TrdRegTimestampType)IFixReader.ReadInt());
                        return true;
                    case FixTags.OrdStatusReqID:
                        _report.OrdStatusReqId = IFixReader.ReadString();
                        return true;
                    case FixTags.LastLiquidityInd:
                        _report.LastLiquidityInd = new int?(IFixReader.ReadInt());
                        return true;
                    case FixTags.ManualOrderIndicator:
                        _report.ManualOrderIndicator = new bool?(IFixReader.ReadBool());
                        return true;
                    case FixTags.AggressorIndicator:
                        _report.AggressorIndicator = new bool?(IFixReader.ReadBool());
                        return true;
                    case FixTags.DisplayQty:
                        _report.DisplayQty = new decimal?(IFixReader.ReadDecimal());
                        return true;
                    case FixTags.ExtendedOrderStatus:
                        _report.ExtendedOrderStatus = new long?(IFixReader.ReadLong());
                        return true;
                    case FixTags.ExtendedTradeStatus:
                        _report.ExtendedTradeStatus = new int?(IFixReader.ReadInt());
                        return true;
                    case FixTags.StrategyTypeId:
                        _report.StrategyTypeId = IFixReader.ReadString();
                        return true;
                    case FixTags.Leverage:
                        _report.Leverage = new int?(IFixReader.ReadInt());
                        return true;
                    default:
                        return _handler(_param1, IFixReader, _report);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="report"></param>
        /// <param name="transactTimeParser"></param>
        /// <param name="extraTagProcess"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        protected bool? ReadExecutionReport(
          IFixReader reader,
          ExecutionReport report,
          FastDateTimeParser transactTimeParser,
          Func<FixTags, IFixReader, ExecutionReport, bool> extraTagProcess)
        {
            ReportReader reportReader = new ReportReader();
            reportReader._report = report;
            reportReader.IFixReader = reader;
            reportReader._innerClass = this;
            reportReader._parser = transactTimeParser;
            reportReader._handler = extraTagProcess;
            if (reportReader.IFixReader == null)
                throw new ArgumentNullException(nameof(IFixReader));
            if (reportReader._report == null)
                throw new ArgumentNullException(nameof(report));
            if (reportReader._parser == null)
                throw new ArgumentNullException(nameof(transactTimeParser));
            if (reportReader._handler == null)
                throw new ArgumentNullException(nameof(extraTagProcess));
            reportReader._count = 0;
            return !reportReader.IFixReader.ReadMessage(new Func<FixTags, bool>(reportReader.ProcessFixTags)) ? new bool?() : new bool?(true);
        }

        private bool? ProcessExecutionReport(IFixReader _param1, Action<Message> _param2)
        {
            ExecutionReport report = new ExecutionReport();
            bool? nullable1 = ReadExecutionReport(_param1, report, TimeStampParser, new Func<FixTags, IFixReader, ExecutionReport, bool>(ProcessExecutionReportExtraTag));
            if (!nullable1.HasValue)
                return new bool?();
            bool? nullable2 = nullable1;
            if (!nullable2.GetValueOrDefault() & nullable2.HasValue)
                return new bool?(false);
            ProcessExecutionReport(report, _param2, new Action<ExecutionReport, Action<Message>, ExecutionMessage>(ProcessExecutionReport));
            return new bool?(true);
        }

        /// <summary>
        /// Process extra tags for <see cref="T:StockSharp.Fix.Native.ExecutionReport" />.
        /// </summary>
        /// <param name="tag">Tag.</param>
        /// <param name="reader">The reader of data recorded in the FIX protocol format.</param>
        /// <param name="report">Execution report.</param>
        /// <returns>Result.</returns>
        protected virtual bool ProcessExecutionReportExtraTag(
          FixTags tag,
          IFixReader reader,
          ExecutionReport report)
        {
            return false;
        }

        /// <summary>
        /// Process <see cref="T:StockSharp.Fix.Native.ExecutionReport" /> instance.
        /// </summary>
        /// <param name="report">
        /// <see cref="T:StockSharp.Fix.Native.ExecutionReport" /> instance.</param>
        /// <param name="messageHandler">Message handler.</param>
        /// <param name="message">The message contains information about the execution.</param>
        protected virtual void ProcessExecutionReport(
          ExecutionReport report,
          Action<Message> messageHandler,
          ExecutionMessage message)
        {
        }

        /// <summary>Get order type.</summary>
        /// <param name="report">Execution report.</param>
        /// <param name="condition">Base order condition (for example, for stop order algo orders).</param>
        /// <returns>Order type.</returns>
        protected virtual OrderTypes GetOrderType(ExecutionReport report, out OrderCondition condition)
        {
            if (report == null)
                throw new ArgumentNullException(nameof(report));
            condition = null;
            char? ordType = report.OrdType;
            if (ordType.HasValue)
            {
                switch (ordType.GetValueOrDefault())
                {
                    case '1':
                        return OrderTypes.Market;
                    case '2':
                        return OrderTypes.Limit;
                }
            }
            condition = new FixOrderCondition()
            {
                StopPrice = report.StopPx
            };
            return OrderTypes.Conditional;
        }

        /// <summary>
        /// Process <see cref="F:StockSharp.Fix.Native.ExecutionReport.Parties" />.
        /// </summary>
        /// <param name="report">Execution report.</param>
        protected virtual void ProcessParties(ExecutionReport report)
        {
            if (report.ClientId == null)
            {
                report.ClientId = report.Parties.FirstOrDefault(p => p.PartyRole.GetValueOrDefault() == PartyRole.ClientId & p.PartyRole.HasValue)?.PartyId;
            }

            if (report.ExecBroker == null)
            {
                report.ExecBroker = report.Parties.FirstOrDefault(p => p.PartyRole.GetValueOrDefault() == PartyRole.ExecutingFirm & p.PartyRole.HasValue)?.PartyId;
            }
        }

        /// <summary>
        /// Get <see cref="P:StockSharp.Messages.ExecutionMessage.OrderStringId" />.
        /// </summary>
        /// <param name="orderId">
        /// <see cref="P:StockSharp.Messages.ExecutionMessage.OrderStringId" /> value.</param>
        /// <returns>
        /// <see cref="P:StockSharp.Messages.ExecutionMessage.OrderStringId" /> value.</returns>
        protected virtual string GetOrderStringId(string orderId) => orderId.IsEmpty() || !orderId.CompareIgnoreCase("none") ? orderId : null;


        [StructLayout(LayoutKind.Auto)]
        private struct ExecutionReportStruct
        {
            public ExecutionReport _report;
            public BaseFixDialect _innerClass;
            public long? _anotherId;
            public DateTimeOffset _reportTime;
            public bool _hasId;
            public long? _id;
            public Action<ExecutionReport, Action<Message>, ExecutionMessage> _handler;
            public Action<Message> _msgHandler;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        /// <param name="messageHandler"></param>
        /// <param name="processExecMsg"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        protected virtual void ProcessExecutionReport(ExecutionReport report, Action<Message> messageHandler, Action<ExecutionReport, Action<Message>, ExecutionMessage> processExecMsg)
        {
            ExecutionReportStruct s;

            s._report = report;
            s._innerClass = this;
            s._handler = processExecMsg;
            s._msgHandler = messageHandler;

            if (s._report == null)
                throw new ArgumentNullException(nameof(report));
            if (s._msgHandler == null)
                throw new ArgumentNullException(nameof(messageHandler));
            if (s._handler == null)
                throw new ArgumentNullException(nameof(processExecMsg));

            s._id = new long?();

            if (!s._report.ClOrdId.IsEmpty())
            {
                long num;
                if (long.TryParse(s._report.ClOrdId, out num))
                    s._id = new long?(num);
                else if (SupportUnknownExecutions)
                {
                    if (_transId2ClOrdIdMap.TryGetKey(s._report.ClOrdId, out num))
                    {
                        s._id = new long?(num);
                    }
                    else
                    {
                        s._id = new long?(_idGenerator.GetNextId());
                        _transId2ClOrdIdMap[s._id.Value] = s._report.ClOrdId;
                    }
                }
            }

            s._hasId = false;
            s._anotherId = new long?();
            long id;
            char? execType1;

            if (long.TryParse(s._report.OrdStatusReqId, out id))
            {
                s._anotherId = new long?(id);
                s._hasId = true;
            }
            else if (long.TryParse(s._report.MassStatusReqId, out id))
            {
                s._anotherId = new long?(id);
                s._hasId = true;
            }
            else
            {
                bool? possDupFlag = s._report.PossDupFlag;
                bool flag = true;
                if (possDupFlag.GetValueOrDefault() == flag & possDupFlag.HasValue)
                {
                    if (long.TryParse(s._report.OrigClOrdId, out id))
                    {
                        s._anotherId = new long?(id);
                        execType1 = s._report.ExecType;
                        int? nullable = execType1.HasValue ? new int?(execType1.GetValueOrDefault()) : new int?();
                        int num2 = 52;
                        if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
                        {
                            s._id = s._anotherId;
                            s._anotherId = new long?(_originalTransId);
                            s._hasId = true;
                        }
                    }
                    else
                    {
                        char? execType2 = s._report.ExecType;
                        int? nullable = execType2.HasValue ? new int?(execType2.GetValueOrDefault()) : new int?();
                        int num2 = 70;
                        if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
                        {
                            if (long.TryParse(s._report.ClOrdId, out id))
                                s._anotherId = new long?(id);
                            else if (long.TryParse(s._report.OrigClOrdId, out id))
                                s._anotherId = new long?(id);
                        }
                        else
                        {
                            s._anotherId = new long?(_originalTransId);
                            s._hasId = true;
                        }
                    }
                }
                else if (s._id.HasValue)
                    s._anotherId = new long?(s._id.Value);
                else if (long.TryParse(s._report.OrigClOrdId, out id))
                    s._anotherId = new long?(id);
            }
            if (s._report.Parties != null)
                ProcessParties(s._report);
            if (s._report.ClientId.IsEmpty())
                s._report.ClientId = ClientCode;
            s._reportTime = (s._report.TransactTime ?? s._report.TrdRegTimestamp ?? s._report.TradeDate ?? s._report.OrigSendingTime ?? s._report.SendingTime).ToDateTimeOffset(TimeZone);
            execType1 = s._report.ExecType;
            if (execType1.HasValue)
            {
                switch (execType1.GetValueOrDefault())
                {
                    case '0':
                    case '1':
                    case '2':
                    case '4':
                    case '5':
                    case 'A':
                    case 'B':
                    case 'E':
                    case 'I':
                        char? execType3 = s._report.ExecType;
                        int? nullable1 = execType3.HasValue ? new int?(execType3.GetValueOrDefault()) : new int?();
                        int num3 = 52;
                        if (nullable1.GetValueOrDefault() == num3 & nullable1.HasValue)
                        {
                            decimal? leavesQty = s._report.LeavesQty;
                            decimal num2 = 0M;
                            if (leavesQty.GetValueOrDefault() == num2 & leavesQty.HasValue)
                                s._report.LeavesQty = new decimal?();
                        }
                        ExecutionMessage executionMessage1 = GetExecutionMsg(ref s);
                        char? execType4 = s._report.ExecType;
                        int? nullable2 = execType4.HasValue ? new int?(execType4.GetValueOrDefault()) : new int?();
                        int num4 = 50;
                        if (!(nullable2.GetValueOrDefault() == num4 & nullable2.HasValue))
                        {
                            execType4 = s._report.ExecType;
                            int? nullable3 = execType4.HasValue ? new int?(execType4.GetValueOrDefault()) : new int?();
                            int num2 = 49;
                            if (!(nullable3.GetValueOrDefault() == num2 & nullable3.HasValue))
                                goto label_49;
                        }
                        executionMessage1.HasTradeInfo = true;
                        if (!executionMessage1.OriginSide.HasValue)
                            executionMessage1.OriginSide = GetOrderSide(executionMessage1.Side, s._report.LastLiquidityInd);
                        if (!executionMessage1.TradeId.HasValue && executionMessage1.TradeStringId.IsEmpty() && s._report.ExecId != null)
                        {
                            long result;
                            if (long.TryParse(s._report.ExecId, out result))
                                executionMessage1.TradeId = new long?(result);
                            else
                                executionMessage1.TradeStringId = s._report.ExecId;
                        }
                    label_49:
                        s._msgHandler(executionMessage1);
                        return;

                    case '6':
                        return;

                    case '8':
                        if (!s._anotherId.HasValue && SupportUnknownExecutions && _transId2ClOrdIdMap.TryGetKey(s._report.OrigClOrdId, out id))
                            s._anotherId = new long?(id);

                        ExecutionMessage executionMessage2 = new ExecutionMessage()
                        {
                            ServerTime = s._report.SendingTime,
                            ExecutionType = new ExecutionTypes?(ExecutionTypes.Transaction),
                            OrderState = new OrderStates?(OrderStates.Failed),
                            Error = new InvalidOperationException(s._report.Text),
                            Commission = s._report.Commission,
                            CommissionCurrency = s._report.CommissionCurrency,
                            ClientCode = s._report.ClientId,
                            HasOrderInfo = true,
                            SeqNum = s._report.MsgSeqNum,
                            UserOrderId = s._report.SecondaryOrderId,
                            StrategyId = s._report.StrategyTypeId,
                            Leverage = s._report.Leverage
                        };
                        bool? possDupFlag1 = s._report.PossDupFlag;
                        bool flag1 = true;
                        if (possDupFlag1.GetValueOrDefault() == flag1 & possDupFlag1.HasValue)
                            executionMessage2.TransactionId = s._report.ClOrdId.TryToLong().GetValueOrDefault();
                        executionMessage2.OriginalTransactionId = s._anotherId.GetValueOrDefault();
                        long result1;
                        if (long.TryParse(s._report.OrderId, out result1))
                            executionMessage2.OrderId = new long?(result1);
                        else
                            executionMessage2.OrderStringId = GetOrderStringId(s._report.OrderId);
                        s._handler(s._report, s._msgHandler, executionMessage2);
                        s._msgHandler(executionMessage2);
                        return;

                    case 'F':
                        ExecutionMessage executionMessage3 = GetExecutionMsg(ref s);
                        executionMessage3.HasTradeInfo = true;
                        if (!executionMessage3.OriginSide.HasValue)
                            executionMessage3.OriginSide = GetOrderSide(executionMessage3.Side, s._report.LastLiquidityInd);
                        if (!executionMessage3.TradeId.HasValue && executionMessage3.TradeStringId.IsEmpty() && s._report.ExecId != null)
                        {
                            long result2;
                            if (long.TryParse(s._report.ExecId, out result2))
                                executionMessage3.TradeId = new long?(result2);
                            else
                                executionMessage3.TradeStringId = s._report.ExecId;
                        }
                        s._msgHandler(executionMessage3);
                        return;
                }
            }
            throw new InvalidOperationException(LocalizedStrings.Str1695Params.Put(s._report.ExecType));
        }

        /// <summary>
        /// Convert <see cref="F:StockSharp.Fix.Native.ExecutionReport.OrdStatus" /> to <see cref="T:StockSharp.Messages.OrderStates" /> value.
        /// </summary>
        /// <param name="report">
        /// <see cref="T:StockSharp.Fix.Native.ExecutionReport" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Messages.OrderStates" /> value.</returns>
        protected virtual OrderStates? GetOrderState(ExecutionReport report)
        {
            ref char? local = ref report.OrdStatus;
            return !local.HasValue ? new OrderStates?() : new OrderStates?(local.GetValueOrDefault().FromFixStatus());
        }


        private sealed class OrderCancelRejectReader
        {
            public string _id;
            public IFixReader IFixReader;
            public string _orderID;
            public DateTimeOffset _sendingTime;
            public BaseFixDialect _innerClass;
            public DateTimeOffset? _time;
            public string _text;

            internal bool ProcessFixTags(FixTags _param1)
            {
                switch (_param1)
                {
                    case FixTags.ClOrdID:
                        _id = IFixReader.ReadString();
                        return true;

                    case FixTags.OrderID:
                        this._orderID = IFixReader.ReadString();
                        return true;

                    case FixTags.SendingTime:
                        _sendingTime = IFixReader.ReadUtc(_innerClass.TimeStampParser);
                        return true;

                    case FixTags.Text:
                        _text = IFixReader.ReadString();
                        return true;

                    case FixTags.TransactTime:
                        _time = new DateTimeOffset?(IFixReader.ReadUtc(_innerClass.TimeStampParser));
                        return true;

                    default:
                        return false;
                }
            }
        }

        private bool? ProcessOrderCancelReject(IFixReader _param1, Action<Message> _param2)
        {
            OrderCancelRejectReader reader = new OrderCancelRejectReader();
            reader.IFixReader = _param1;
            reader._innerClass = this;
            reader._id = null;
            reader._orderID = null;
            reader._sendingTime = new DateTimeOffset();
            reader._time = new DateTimeOffset?();
            reader._text = null;
            if (!reader.IFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            ExecutionMessage executionMessage1 = new ExecutionMessage();
            executionMessage1.OriginalTransactionId = reader._id.To<long>();
            executionMessage1.ServerTime = reader._time ?? reader._sendingTime;
            executionMessage1.ExecutionType = new ExecutionTypes?(ExecutionTypes.Transaction);
            executionMessage1.OrderState = new OrderStates?(OrderStates.Failed);
            executionMessage1.Error = new InvalidOperationException(LocalizedStrings.Str1696Params.Put(new object[2]
            {
         reader._orderID,
         reader._text
            }));
            executionMessage1.HasOrderInfo = true;
            ExecutionMessage executionMessage2 = executionMessage1;
            if (reader._orderID != null)
            {
                long result;
                if (long.TryParse(reader._orderID, out result))
                    executionMessage2.OrderId = new long?(result);
                else
                    executionMessage2.OrderStringId = GetOrderStringId(reader._orderID);
            }
            _param2(executionMessage2);
            return new bool?(true);
        }

        private static bool? ProcessRequestForPositionsAck(IFixReader _param0, Action<Message> _param1)
        {
            var reader = new RequestForPositionsAckReader();
            reader.IFixReader = _param0;
            reader._account = null;
            reader._posReqResult = new PosReqResult?();
            reader._posReqStatus = new PosReqStatus?();
            reader._text = null;
            if (!reader.IFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();

            if (reader._posReqResult.HasValue)
            {
                PosReqResult? zNaWwRn18Hrac1 = reader._posReqResult;
                PosReqResult posReqResult1 = PosReqResult.ValidRequest;
                if (!(zNaWwRn18Hrac1.GetValueOrDefault() == posReqResult1 & zNaWwRn18Hrac1.HasValue))
                {
                    PosReqStatus? zUmV8jiO0EtZe = reader._posReqStatus;
                    if (!(zUmV8jiO0EtZe.GetValueOrDefault() == PosReqStatus.Rejected & zUmV8jiO0EtZe.HasValue))
                    {
                        if (!reader._posReqStatus.HasValue)
                        {
                            PosReqResult? zNaWwRn18Hrac2 = reader._posReqResult;
                            PosReqResult posReqResult2 = PosReqResult.ValidRequest;
                            if (zNaWwRn18Hrac2.GetValueOrDefault() == posReqResult2 & zNaWwRn18Hrac2.HasValue)
                                goto label_8;
                        }
                        else
                            goto label_8;
                    }
                    _param1(LocalizedStrings.Str1698Params.Put(new object[3]
                    {
             reader._account,
             reader._posReqResult,
             reader._text
                    }).ToErrorMessage());
                }
            }
        label_8:
            return new bool?(true);
        }



        private ExecutionMessage GetExecutionMsg(ref ExecutionReportStruct r)
        {
            if (r._report.OrderQty.GetValueOrDefault() == 0M & r._report.OrderQty.HasValue)
            {
                r._report.OrderQty = new decimal?();
            }

            char? status = r._report.OrdStatus;
            int? ordStatus = status.HasValue ? new int?(status.GetValueOrDefault()) : new int?();

            if (ordStatus.GetValueOrDefault() == 52 & ordStatus.HasValue)
            {
                decimal? leavesQty = r._report.LeavesQty;

                if (leavesQty.GetValueOrDefault() == 0M & leavesQty.HasValue)
                {
                    r._report.LeavesQty = new decimal?();
                }
            }

            var msg = new ExecutionMessage();

            msg.SecurityId = new SecurityId()
            {
                SecurityCode = r._report.Symbol,
                BoardCode = GetBoardCode(r._report.ExDestination, r._report.SecurityExchange, r._report.TradingSessionId)
            };

            msg.OriginalTransactionId = r._anotherId.GetValueOrDefault();
            msg.OrderPrice = r._report.Price.GetValueOrDefault();
            msg.OrderVolume = r._report.OrderQty;
            msg.PortfolioName = GetSyntheticPortfolioName(r._report.Account);
            msg.Side = r._report.Side.HasValue ? r._report.Side.GetValueOrDefault().FromFixSide() : Sides.Buy;
            msg.ServerTime = r._reportTime;

            decimal? balance;

            if (!r._report.OrderQty.HasValue || !r._report.CumQty.HasValue)
            {
                balance = r._report.LeavesQty;
            }
            else
            {
                balance = r._report.OrderQty.HasValue & r._report.CumQty.HasValue ? new decimal?(r._report.OrderQty.GetValueOrDefault() - r._report.CumQty.GetValueOrDefault()) : new decimal?();
            }

            msg.Balance = balance;
            msg.OrderState = GetOrderState(r._report);
            msg.ExecutionType = new ExecutionTypes?(ExecutionTypes.Transaction);
            msg.Commission = r._report.Commission;
            msg.CommissionCurrency = r._report.CommissionCurrency;
            msg.ClientCode = r._report.ClientId;
            msg.BrokerCode = r._report.ExecBroker;
            msg.TradePrice = r._report.LastPx;
            msg.TradeVolume = r._report.LastQty;
            msg.VisibleVolume = r._report.MaxFloor.HasValue ? r._report.MaxFloor : r._report.DisplayQty;
            msg.OrderStatus = r._report.ExtendedOrderStatus;
            msg.TradeStatus = r._report.ExtendedTradeStatus;
            status = r._report.CashMargin;

            bool? isMargin;
            if (r._report.CashMargin.HasValue)
            {
                var cashMargin = r._report.CashMargin.HasValue ? new int?(r._report.CashMargin.GetValueOrDefault()) : new int?();

                int marginValue;

                if (!(cashMargin.GetValueOrDefault() == 2 & cashMargin.HasValue))
                {
                    cashMargin = r._report.CashMargin.HasValue ? new int?(r._report.CashMargin.GetValueOrDefault()) : new int?();
                    marginValue = cashMargin.GetValueOrDefault() == 3 & cashMargin.HasValue ? 1 : 0;
                }
                else
                {
                    marginValue = 1;
                }

                isMargin = new bool?(marginValue != 0);
            }
            else
                isMargin = new bool?();

            msg.IsMargin = isMargin;
            msg.AveragePrice = r._report.AvgPx;
            msg.Yield = r._report.Yield;
            msg.MinVolume = r._report.MinQty;
            msg.PositionEffect = r._report.PositionEffect.ToPositionEffect();
            msg.Initiator = r._report.AggressorIndicator;
            msg.IsManual = r._report.ManualOrderIndicator;
            msg.SeqNum = r._report.MsgSeqNum;
            msg.UserOrderId = r._report.SecondaryOrderId;
            msg.StrategyId = r._report.StrategyTypeId;
            msg.Leverage = r._report.Leverage;

            OrderStates? orderState;

            if (!msg.Balance.HasValue)
            {
                if (!msg.OrderState.HasValue)
                {
                    if (!msg.OrderVolume.HasValue && !r._report.Price.HasValue)
                        goto label_18;
                }
            }
            msg.HasOrderInfo = true;

        label_18:
            if (r._report.OrdType.HasValue)
            {
                OrderCondition condition;
                msg.OrderType = new OrderTypes?(GetOrderType(r._report, out condition));
                msg.Condition = condition;
                msg.HasOrderInfo = true;
            }
            if (r._report.OrderId != null)
            {
                long result;
                if (long.TryParse(r._report.OrderId, out result))
                    msg.OrderId = new long?(result);
                else
                    msg.OrderStringId = GetOrderStringId(r._report.OrderId);
            }

            if (Native.Extensions.IsMarketMaker(r._report.OrderCapacity, r._report.OrderRestrictions))
            {
                msg.IsMarketMaker = new bool?(true);
            }

            if (r._hasId)
            {
                msg.TransactionId = r._id.GetValueOrDefault();
            }

            r._handler(r._report, r._msgHandler, msg);
            orderState = msg.OrderState;
            OrderStates orderStates = OrderStates.Failed;

            if (orderState.GetValueOrDefault() == orderStates & orderState.HasValue)
            {
                msg.Error = new InvalidOperationException(r._report.Text);
                msg.HasOrderInfo = true;
            }
            else
            {
                msg.Comment = r._report.Text;
            }


            if (r._report.TimeInForce.HasValue)
            {
                msg.HasOrderInfo = true;
                status = r._report.TimeInForce;
                if (status.HasValue)
                {
                    switch (status.GetValueOrDefault())
                    {
                        case '0':
                            msg.TimeInForce = new StockSharp.Messages.TimeInForce?(Messages.TimeInForce.PutInQueue);
                            msg.ExpiryDate = new DateTimeOffset?(Messages.Extensions.Today);
                            goto label_40;
                        case '1':
                            msg.TimeInForce = new StockSharp.Messages.TimeInForce?(Messages.TimeInForce.PutInQueue);
                            goto label_40;
                        case '3':
                            msg.TimeInForce = new StockSharp.Messages.TimeInForce?(Messages.TimeInForce.CancelBalance);
                            goto label_40;
                        case '4':
                            msg.TimeInForce = new StockSharp.Messages.TimeInForce?(Messages.TimeInForce.MatchOrCancel);
                            goto label_40;
                        case '6':
                            msg.TimeInForce = new StockSharp.Messages.TimeInForce?(Messages.TimeInForce.PutInQueue);
                            ExecutionMessage executionMessage3 = msg;
                            DateTime? expireTime = r._report.ExpireTime;
                            DateTime? nullable6 = expireTime.HasValue ? expireTime : r._report.ExpireDate;
                            ref DateTime? local2 = ref nullable6;
                            DateTimeOffset? nullable7 = local2.HasValue ? new DateTimeOffset?(local2.GetValueOrDefault().ToDateTimeOffset(TimeZone)) : new DateTimeOffset?();
                            executionMessage3.ExpiryDate = nullable7;
                            goto label_40;
                    }
                }
                throw new InvalidOperationException(LocalizedStrings.Str1599);
            }
        label_40:
            return msg;
        }

        internal static Sides? GetOrderSide(Sides orderSide, int? orderValue)
        {
            if (!orderValue.HasValue)
                return new Sides?();
            switch (orderValue.GetValueOrDefault())
            {
                case 1:
                    return new Sides?(orderSide.Invert());
                case 2:
                case 3:
                    return new Sides?(orderSide);
                default:
                    throw new ArgumentOutOfRangeException(nameof(orderSide), orderValue, LocalizedStrings.Str1219);
            }
        }













        private sealed class RequestForPositionsAckReader
        {
            public string _account;
            public IFixReader IFixReader;
            public PosReqResult? _posReqResult;
            public PosReqStatus? _posReqStatus;
            public string _text;

            internal bool ProcessFixTags(FixTags _param1)
            {
                switch (_param1)
                {
                    case FixTags.Account:
                        _account = IFixReader.ReadString();
                        return true;
                    case FixTags.Text:
                        _text = IFixReader.ReadString();
                        return true;
                    case FixTags.PosReqResult:
                        _posReqResult = new PosReqResult?((PosReqResult)IFixReader.ReadInt());
                        return true;
                    case FixTags.PosReqStatus:
                        _posReqStatus = new PosReqStatus?((PosReqStatus)IFixReader.ReadInt());
                        return true;
                    default:
                        return false;
                }
            }
        }
    }
}
