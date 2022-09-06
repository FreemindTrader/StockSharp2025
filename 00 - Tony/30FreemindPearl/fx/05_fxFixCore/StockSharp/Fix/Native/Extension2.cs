using Ecng.Collections;
using Ecng.Common;
using StockSharp.Algo.Strategies.Messages;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;


namespace StockSharp.Fix.Native
{
    /// <summary>FIX/FAST extension methods.</summary>
    public static partial class Extensions
    {

        private sealed class SecurityMsgRader
        {
            public HashSet<FixTags> _fixTagsSet;
            public string _securityCode;
            public Action<SecurityMessage, string, string, string, string> InitSecId;
            public SecurityMessage _securityMsg;
            public string _boardCode;
            public string _exDestination;
            public string _idsourceTag;
            public string _securityIdTag;
            public long? _transactionId;
            public int _count;
            public Action<Message> MessageHandler;
            public int? _InstrAttribType;
            public Func<FixTags, IFixReader, SecurityMessage, bool> CustomTagHandler;
            public IFixReader _IFixReader;
            public bool? _LastFragment;
            public int? _TotNoRelatedSym;
            public int? _securityTypes;
            public SecurityResponseType? _SecurityResponseType;
            public string _text;
            public SecurityRequestResult? _SecurityRequestResult;
            public Func<string, SecurityTypes?> _securityTypeFunc;
            public FastDateTimeParser _SettlDate;
            public Action<Exception> _exceptionHandler;
            public FastDateTimeParser _fastDateTimeParser;

            internal bool ProcessTags(FixTags tag)
            {
                if (!_fixTagsSet.Add(tag))
                {
                    if (!_securityCode.IsEmpty())
                    {
                        InitSecId(_securityMsg, _securityCode, _boardCode ?? _exDestination, _idsourceTag, _securityIdTag);
                        _securityMsg.OriginalTransactionId = _transactionId.GetValueOrDefault();
                        ++_count;

                        MessageHandler(_securityMsg);

                        _securityCode = null;
                        _boardCode = null;
                        _exDestination = null;
                        _idsourceTag = null;
                        _securityIdTag = null;
                        _InstrAttribType = new int?();
                        _securityMsg = new SecurityMessage();
                    }

                    _fixTagsSet.Clear();
                    _fixTagsSet.Add(tag);
                }

                if (CustomTagHandler(tag, _IFixReader, _securityMsg))
                {
                    SecurityId securityId;
                    if (_securityCode.IsEmpty())
                    {
                        securityId = _securityMsg.SecurityId;
                        _securityCode = securityId.SecurityCode;
                    }
                    if (_boardCode.IsEmpty())
                    {
                        securityId = _securityMsg.SecurityId;
                        _boardCode = securityId.BoardCode;
                    }
                    return true;
                }

                switch (tag)
                {
                    case FixTags.Currency:
                        _securityMsg.Currency = _IFixReader.ReadString().FromMicexCurrencyName(_exceptionHandler);
                        return true;

                    case FixTags.IDSource:
                        _idsourceTag = _IFixReader.ReadString();
                        return true;

                    case FixTags.SecurityID:
                        _securityIdTag = _IFixReader.ReadString();
                        return true;

                    case FixTags.Symbol:
                        _securityCode = _IFixReader.ReadString();
                        return true;

                    case FixTags.Text:
                        _text = _IFixReader.ReadString();
                        return true;

                    case FixTags.SettlDate:
                        _securityMsg.SettlementDate = new DateTimeOffset?(_IFixReader.ReadUtc(_SettlDate));
                        return true;

                    case FixTags.SymbolSfx:
                        _securityMsg.UnderlyingSecurityCode = _IFixReader.ReadString();
                        return true;

                    case FixTags.ExDestination:
                        _exDestination = _IFixReader.ReadString();
                        return true;

                    case FixTags.SecurityDesc:
                        _securityMsg.Name = _IFixReader.ReadString();
                        return true;

                    case FixTags.MinQty:
                        _securityMsg.MinVolume = new decimal?(_IFixReader.ReadDecimal());
                        return true;

                    case FixTags.SettlCurrency:
                        string name = _IFixReader.ReadString();
                        if (!_securityMsg.Currency.HasValue)
                            _securityMsg.Currency = name.FromMicexCurrencyName(_exceptionHandler);
                        return true;

                    case FixTags.NoRelatedSym:
                        _securityTypes = new int?(_IFixReader.ReadInt());
                        return true;

                    case FixTags.SecurityType:
                        _securityMsg.SecurityType = _securityTypeFunc(_IFixReader.ReadString());
                        return true;

                    case FixTags.MaturityMonthYear:
                        _securityMsg.IssueDate = new DateTimeOffset?(_IFixReader.ReadUtc(_fastDateTimeParser));
                        return true;

                    case FixTags.PutOrCall:
                        _securityMsg.OptionType = new OptionTypes?(((PutOrCall)_IFixReader.ReadInt()).FromFixOptionType());
                        return true;

                    case FixTags.StrikePrice:
                        _securityMsg.Strike = new decimal?(_IFixReader.ReadDecimal());
                        return true;

                    case FixTags.SecurityExchange:
                        _boardCode = _IFixReader.ReadString();
                        return true;

                    case FixTags.IssueDate:
                        _securityMsg.IssueDate = new DateTimeOffset?(_IFixReader.ReadUtc(_SettlDate));
                        return true;

                    case FixTags.Factor:
                    case FixTags.ContractMultiplier:
                    case FixTags.RatioQty:
                        _securityMsg.Multiplier = new decimal?(_IFixReader.ReadDecimal());
                        return true;

                    case FixTags.UnderlyingSecurityID:
                        _securityMsg.UnderlyingSecurityCode = _IFixReader.ReadString();
                        return true;

                    case FixTags.UnderlyingSecurityType:
                        _securityMsg.UnderlyingSecurityType = _IFixReader.ReadString().FromFixType();
                        return true;

                    case FixTags.UnderlyingSymbol:
                        _securityMsg.UnderlyingSecurityCode = _IFixReader.ReadString();
                        return true;

                    case FixTags.SecurityReqID:
                        _transactionId = new long?(_IFixReader.ReadString().To<long>());
                        return true;
                    case FixTags.SecurityResponseType:
                        _SecurityResponseType = new SecurityResponseType?((SecurityResponseType)_IFixReader.ReadInt());
                        return true;

                    case FixTags.TotNoRelatedSym:
                        _TotNoRelatedSym = new int?(_IFixReader.ReadInt());
                        return true;

                    case FixTags.CFICode:
                        _securityMsg.CfiCode = _IFixReader.ReadString();
                        return true;

                    case FixTags.SecurityRequestResult:
                        _SecurityRequestResult = new SecurityRequestResult?((SecurityRequestResult)_IFixReader.ReadInt());
                        return true;

                    case FixTags.RoundLot:
                        _securityMsg.VolumeStep = new decimal?(_IFixReader.ReadDecimal());
                        return true;

                    case FixTags.MinTradeVol:
                    case FixTags.MinPriceIncrement:
                        _securityMsg.PriceStep = new decimal?(_IFixReader.ReadDecimal());
                        return true;

                    case FixTags.ContractSettlMonth:
                        _securityMsg.SettlementDate = new DateTimeOffset?(_IFixReader.ReadUtc(_SettlDate));
                        return true;

                    case FixTags.InstrAttribType:
                        _InstrAttribType = new int?(_IFixReader.ReadInt());
                        return true;

                    case FixTags.InstrAttribValue:
                        {
                            string str1 = _IFixReader.ReadString();

                            if (_InstrAttribType.HasValue && _InstrAttribType.GetValueOrDefault() == 27)
                            {
                                _securityMsg.Decimals = new int?(str1.To<int>());
                            }

                            _InstrAttribType = new int?();
                        }
                        return true;

                    case FixTags.LastFragment:
                        _LastFragment = new bool?(_IFixReader.ReadBool());
                        return true;

                    case FixTags.EndDate:
                        _securityMsg.ExpiryDate = new DateTimeOffset?(_IFixReader.ReadUtc(_SettlDate));
                        return true;

                    case FixTags.MaxTradeVol:
                        _securityMsg.MaxVolume = new decimal?(_IFixReader.ReadDecimal());
                        return true;

                    case FixTags.IssueSize:
                        _securityMsg.IssueSize = new decimal?(_IFixReader.ReadDecimal());
                        return true;

                    case FixTags.Formula:
                        {
                            string basket = _IFixReader.ReadString();
                            _securityMsg.BasketCode = basket.Substring(0, 2);
                            _securityMsg.BasketExpression = basket.Substring(3);
                        }
                        return true;

                    case FixTags.Shortable:
                        _securityMsg.Shortable = new bool?(_IFixReader.ReadBool());
                        return true;

                    case FixTags.FaceValue:
                        _securityMsg.FaceValue = new decimal?(_IFixReader.ReadDecimal());
                        return true;

                    case FixTags.PrimaryCode:
                        {
                            SecurityId tmpId = _securityMsg.PrimaryId;
                            tmpId.SecurityCode = _IFixReader.ReadString();
                            _securityMsg.PrimaryId = tmpId;
                        }
                        return true;

                    case FixTags.PrimaryBoard:
                        {
                            SecurityId tmpId = _securityMsg.PrimaryId;
                            tmpId.BoardCode = _IFixReader.ReadString();
                            _securityMsg.PrimaryId = tmpId;
                        }
                        return true;

                    default:
                        return false;
                }
            }

            internal RefPair<int, int> CreateRefPair(long _param1)
            {
                return RefTuple.Create(_TotNoRelatedSym.Value, 0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dateParser"></param>
        /// <param name="yearMonthParser"></param>
        /// <param name="totalSecCountByRequestId"></param>
        /// <param name="initSecId"></param>
        /// <param name="errorHandler"></param>
        /// <param name="customTagHandler"></param>
        /// <param name="messageHandler"></param>
        /// <param name="getSecurityType"></param>
        /// <param name="lastFragment2"></param>
        /// <param name="securityReqId2"></param>
        /// <param name="reason"></param>
        /// <param name="text2"></param>
        /// <returns></returns>
        public static bool? ReadSecurityMessage(
                                                  this IFixReader reader,
                                                  FastDateTimeParser dateParser,
                                                  FastDateTimeParser yearMonthParser,
                                                  IDictionary<long, RefPair<int, int>> totalSecCountByRequestId,
                                                  Action<SecurityMessage, string, string, string, string> initSecId,
                                                  Action<Exception> errorHandler,
                                                  Func<FixTags, IFixReader, SecurityMessage, bool> customTagHandler,
                                                  Action<Message> messageHandler,
                                                  Func<string, SecurityTypes?> getSecurityType,
                                                  out bool? lastFragment2,
                                                  out long? securityReqId2,
                                                  out string reason,
                                                  out string text2)
        {
            var msgReader = new SecurityMsgRader();

            msgReader.InitSecId = initSecId;
            msgReader.MessageHandler = messageHandler;
            msgReader.CustomTagHandler = customTagHandler;
            msgReader._IFixReader = reader;
            msgReader._securityTypeFunc = getSecurityType;
            msgReader._SettlDate = dateParser;
            msgReader._exceptionHandler = errorHandler;
            msgReader._fastDateTimeParser = yearMonthParser;
            msgReader._securityMsg = new SecurityMessage();
            msgReader._transactionId = new long?();
            msgReader._SecurityResponseType = new SecurityResponseType?();
            msgReader._SecurityRequestResult = new SecurityRequestResult?();
            msgReader._InstrAttribType = new int?();
            msgReader._securityCode = null;
            msgReader._boardCode = null;
            msgReader._exDestination = null;
            msgReader._text = null;
            msgReader._securityIdTag = null;
            msgReader._idsourceTag = null;
            msgReader._LastFragment = new bool?();
            msgReader._TotNoRelatedSym = new int?();
            msgReader._securityTypes = new int?();
            msgReader._count = 0;
            msgReader._fixTagsSet = new HashSet<FixTags>();

            if (!msgReader._IFixReader.ReadMessage(new Func<FixTags, bool>(msgReader.ProcessTags)))
            {
                text2 = null;
                reason = null;
                lastFragment2 = new bool?();
                securityReqId2 = new long?();
                return false;
            }
            SecurityResponseType? responseType = msgReader._SecurityResponseType;
            int num;

            if (!(responseType.GetValueOrDefault() == SecurityResponseType.RejectSecurityProposal & responseType.HasValue))
            {
                if (msgReader._SecurityRequestResult.HasValue)
                {
                    SecurityRequestResult? requestResult = msgReader._SecurityRequestResult;

                    if (!(requestResult.GetValueOrDefault() == SecurityRequestResult.ValidRequest & requestResult.HasValue))
                    {
                        SecurityRequestResult securityRequestResult2 = SecurityRequestResult.NoInstrumentsFoundThatMatchSelectionCriteria;
                        num = !(requestResult.GetValueOrDefault() == securityRequestResult2 & requestResult.HasValue) ? 1 : 0;
                        goto label_8;
                    }
                }
                num = 0;
            }
            else
                num = 1;

            label_8:

            if (num == 0)
            {
                if (!msgReader._securityCode.IsEmpty())
                {
                    msgReader._securityMsg.OriginalTransactionId = msgReader._transactionId.GetValueOrDefault();
                    msgReader.InitSecId(msgReader._securityMsg, msgReader._securityCode, msgReader._boardCode ?? msgReader._exDestination, msgReader._idsourceTag, msgReader._securityIdTag);
                    msgReader._count++;
                    msgReader.MessageHandler(msgReader._securityMsg);
                }
                if (msgReader._transactionId.HasValue && totalSecCountByRequestId != null && (msgReader._TotNoRelatedSym.HasValue && !msgReader._LastFragment.HasValue))
                {
                    RefPair<int, int> refPair = totalSecCountByRequestId.SafeAdd(msgReader._transactionId.Value, new Func<long, RefPair<int, int>>(msgReader.CreateRefPair));
                    refPair.Second += msgReader._securityTypes ?? msgReader._count;
                    if (refPair.First == refPair.Second)
                    {
                        totalSecCountByRequestId.Remove(msgReader._transactionId.Value);
                        msgReader._LastFragment = new bool?(true);
                    }
                }
            }

            text2 = msgReader._text;
            reason = msgReader._SecurityRequestResult.To<string>() ?? msgReader._SecurityResponseType.To<string>();
            lastFragment2 = msgReader._LastFragment;
            securityReqId2 = msgReader._transactionId;

            return new bool?(num == 0);
        }

        private sealed class StrategyTypeRader
        {
            public string _name;
            public IFixReader _IFixReader;
            public string _StrategyTypeId;
            public string _MassStatusReqID;
            public int? _RawDataLength;
            public byte[] _rawData;

            internal bool ProcessTags(FixTags _param1)
            {
                switch (_param1)
                {
                    case FixTags.RawDataLength:
                        _RawDataLength = new int?(_IFixReader.ReadInt());
                        return true;

                    case FixTags.RawData:
                        _rawData = new byte[_RawDataLength.Value];
                        _IFixReader.ReadBytes(_rawData, 0, _rawData.Length);
                        return true;

                    case FixTags.MassStatusReqID:
                        _MassStatusReqID = _IFixReader.ReadString();
                        return true;

                    case FixTags.Name:
                        _name = _IFixReader.ReadString();
                        return true;
                    case FixTags.StrategyTypeId:
                        _StrategyTypeId = _IFixReader.ReadString();
                        return true;

                    default:
                        return false;
                }
            }
        }


        /// <summary>
        /// Read <see cref="T:StockSharp.Algo.Strategies.Messages.StrategyTypeMessage" />.
        /// </summary>
        /// <param name="reader">_IFixReader.</param>
        /// <param name="messageHandler">Message handler.</param>
        /// <returns>
        /// <see langword="true" /> message read ok, <see langword="false" /> message read with errors, <see langword="null" /> network problem.</returns>
        public static bool? ProcessStrategyType(this IFixReader reader, Action<Message> messageHandler)
        {
            var typeReader = new StrategyTypeRader();
            typeReader._IFixReader = reader;
            typeReader._name = null;
            typeReader._StrategyTypeId = null;
            typeReader._MassStatusReqID = null;
            typeReader._RawDataLength = new int?();
            typeReader._rawData = null;

            if (!typeReader._IFixReader.ReadMessage(new Func<FixTags, bool>(typeReader.ProcessTags)))
                return false;

            messageHandler(new StrategyTypeMessage()
            {
                StrategyName = typeReader._name,
                StrategyTypeId = typeReader._StrategyTypeId,
                OriginalTransactionId = typeReader._MassStatusReqID.To<long?>().GetValueOrDefault(),
                Assembly = typeReader._rawData
            });
            return true;
        }

        private sealed class StrategyInfoReader
        {
            public Dictionary<string, (string, string)> _noStrategyParameters;
            public IFixReader _IFixReader;

            public string _name;

            public string _clientId;
            public long _id;
            public DateTimeOffset _issueDate;
            public FastDateTimeParser _SettlDate;
            public string _MassStatusReqID;

            public string _strategyParameterName;
            public string _strategyParameterType;
            public string _strategyParameterValue;
            public long? _product;

            internal bool ProcessTags(FixTags _param1)
            {
                switch (_param1)
                {
                    case FixTags.ClientID:
                        _clientId = _IFixReader.ReadString();
                        return true;
                    case FixTags.IssueDate:
                        _issueDate = _IFixReader.ReadUtc(_SettlDate);
                        return true;
                    case FixTags.Product:
                        _product = new long?(_IFixReader.ReadLong());
                        return true;
                    case FixTags.MassStatusReqID:
                        _MassStatusReqID = _IFixReader.ReadString();
                        return true;
                    case FixTags.NoStrategyParameters:
                        _noStrategyParameters = new Dictionary<string, (string, string)>(_IFixReader.ReadInt());
                        return true;
                    case FixTags.StrategyParameterName:
                        _strategyParameterName = _IFixReader.ReadString();
                        return true;
                    case FixTags.StrategyParameterType:
                        _strategyParameterType = _IFixReader.ReadString();
                        return true;
                    case FixTags.StrategyParameterValue:
                        _strategyParameterValue = _IFixReader.ReadString();
                        _noStrategyParameters.Add(_strategyParameterName, (_strategyParameterType, _strategyParameterValue));
                        return true;
                    case FixTags.Name:
                        _name = _IFixReader.ReadString();
                        return true;
                    case FixTags.Id:
                        _id = _IFixReader.ReadLong();
                        return true;
                    default:
                        return false;
                }
            }
        }


        /// <summary>
        /// Read <see cref="T:StockSharp.Algo.Strategies.Messages.StrategyInfoMessage" />.
        /// </summary>
        /// <param name="reader">_IFixReader.</param>
        /// <param name="dateParser">Time parser.</param>
        /// <param name="messageHandler">Message handler.</param>
        /// <returns>
        /// <see langword="true" /> message read ok, <see langword="false" /> message read with errors, <see langword="null" /> network problem.</returns>
        public static bool? ProcessStrategyInfo(this IFixReader reader, FastDateTimeParser dateParser, Action<Message> messageHandler)
        {
            var msgReader = new StrategyInfoReader();

            msgReader._IFixReader = reader;
            msgReader._SettlDate = dateParser;
            msgReader._name = null;
            msgReader._id = 0L;
            msgReader._clientId = null;
            msgReader._issueDate = new DateTimeOffset();
            msgReader._MassStatusReqID = null;
            msgReader._noStrategyParameters = null;
            msgReader._strategyParameterName = null;
            msgReader._strategyParameterType = null;
            msgReader._strategyParameterValue = null;
            msgReader._product = new long?();

            if (!msgReader._IFixReader.ReadMessage(new Func<FixTags, bool>(msgReader.ProcessTags)))
                return false;

            var msg = new StrategyInfoMessage();
            msg.Name = msgReader._name;
            msg.StrategyId = msgReader._clientId.To<Guid>();
            msg.OriginalTransactionId = msgReader._MassStatusReqID.To<long?>().GetValueOrDefault();
            msg.Id = msgReader._id;
            msg.CreationDate = msgReader._issueDate;
            msg.ProductId = msgReader._product.GetValueOrDefault();

            msg.Parameters.AddRange(msgReader._noStrategyParameters);
            messageHandler(msg);

            return true;
        }


        private sealed class StrategyStateReader
        {
            public string _clientId;
            public IFixReader _IFixReader;
            public string _StrategyTypeId;
            public string _massStatusReqID;
            public string _MassStatusReqID;
            public Dictionary<string, (string, string)> _noStrategyParameters;
            public string _strategyParameterName;
            public string _strategyParameterType;
            public string _strategyParameterValue;

            internal bool ProcessFixTags(FixTags _param1)
            {
                switch (_param1)
                {
                    case FixTags.ClientID:
                        _clientId = _IFixReader.ReadString();
                        return true;
                    case FixTags.MDReqID:
                        _MassStatusReqID = _IFixReader.ReadString();
                        return true;
                    case FixTags.MassStatusReqID:
                        _massStatusReqID = _IFixReader.ReadString();
                        return true;
                    case FixTags.NoStrategyParameters:
                        _noStrategyParameters = new Dictionary<string, (string, string)>(_IFixReader.ReadInt());
                        return true;
                    case FixTags.StrategyParameterName:
                        _strategyParameterName = _IFixReader.ReadString();
                        return true;
                    case FixTags.StrategyParameterType:
                        _strategyParameterType = _IFixReader.ReadString();
                        return true;
                    case FixTags.StrategyParameterValue:
                        _strategyParameterValue = _IFixReader.ReadString();
                        _noStrategyParameters.Add(_strategyParameterName, (_strategyParameterType, _strategyParameterValue));
                        return true;
                    case FixTags.StrategyTypeId:
                        _StrategyTypeId = _IFixReader.ReadString();
                        return true;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// Read <see cref="T:StockSharp.Algo.Strategies.Messages.StrategyStateMessage" />.
        /// </summary>
        /// <param name="reader">_IFixReader.</param>
        /// <param name="messageHandler">Message handler.</param>
        /// <returns>
        /// <see langword="true" /> message read ok, <see langword="false" /> message read with errors, <see langword="null" /> network problem.</returns>
        public static bool? ProcessStrategyState(this IFixReader reader, Action<Message> messageHandler)
        {
            StrategyStateReader msgReader = new StrategyStateReader();
            msgReader._IFixReader = reader;
            msgReader._clientId = null;
            msgReader._StrategyTypeId = null;
            msgReader._MassStatusReqID = null;
            msgReader._massStatusReqID = null;
            msgReader._noStrategyParameters = null;
            msgReader._strategyParameterName = null;
            msgReader._strategyParameterType = null;
            msgReader._strategyParameterValue = null;

            if (!msgReader._IFixReader.ReadMessage(new Func<FixTags, bool>(msgReader.ProcessFixTags)))
                return false;

            StrategyStateMessage msg = new StrategyStateMessage();
            msg.StrategyId = msgReader._clientId.To<Guid?>() ?? Guid.Empty;
            msg.StrategyTypeId = msgReader._StrategyTypeId;
            msg.OriginalTransactionId = msgReader._massStatusReqID.To<long?>().GetValueOrDefault();
            msg.TransactionId = msgReader._MassStatusReqID.To<long?>().GetValueOrDefault();

            msg.Statistics.AddRange(msgReader._noStrategyParameters);
            messageHandler(msg);
            return true;
        }

        private sealed class MarketDataReader
        {
            public SecurityId[] _securityIds;
            public int _idCount;
            public string _securityCode;
            public string _securityExchange;
            public string _cfiCode;
            public string _mdReqId;
            public IFixReader _IFixReader;
            public string _mdResponseId;
            public char? _subscriptionRequestType;
            public int? _marketDepth;
            public char[] _noMDEntryTypes;
            public string[] _mdEntryArg;
            public string _startDate;
            public string _endDate;
            public int _mdEntryTypeCount;
            public int _mdEntryArgCount;
            public string[] _securityTypes;
            public int _securityTypeCount;
            public bool? _allowBuildFromSmallerTimeFrame;
            public bool? _calcVolumeProfile;
            public bool? _finishedCandles;
            public int? _marketDataBuildMode;
            public char? _marketDataBuildFrom;
            public char? _marketDataBuildField;
            public bool? _regularTradingHours;
            public long? _marketDataSkip;
            public long? _marketDataCount;
            public Func<char, int, DataType> _readerFunction;

            internal void NewSecurityId()
            {
                _securityIds[_idCount++] = new SecurityId()
                {
                    SecurityCode = _securityCode,
                    BoardCode = _securityExchange
                };
                _securityCode = null;
                _securityExchange = null;
                _cfiCode = null;
            }

            internal bool ProcessFixTags(FixTags tag)
            {
                switch (tag)
                {
                    case FixTags.Symbol:
                        if (_securityCode != null)
                            NewSecurityId();
                        _securityCode = _IFixReader.ReadString();
                        return true;

                    case FixTags.NoRelatedSym:
                        int length1 = _IFixReader.ReadInt();
                        _securityIds = new SecurityId[length1];
                        _securityTypes = (new string[length1]);
                        return true;

                    case FixTags.SecurityType:
                        _securityTypes[++_securityTypeCount] = _IFixReader.ReadString();
                        return true;

                    case FixTags.SecurityExchange:
                        if (_securityExchange != null)
                            NewSecurityId();
                        _securityExchange = _IFixReader.ReadString();
                        return true;

                    case FixTags.MDReqID:
                        _mdReqId = _IFixReader.ReadString();
                        return true;

                    case FixTags.SubscriptionRequestType:
                        _subscriptionRequestType = new char?(_IFixReader.ReadChar());
                        return true;

                    case FixTags.MarketDepth:
                        _marketDepth = new int?(_IFixReader.ReadInt());
                        if (_marketDepth.GetValueOrDefault() == 0 & _marketDepth.HasValue)
                            _marketDepth = new int?();
                        return true;

                    case FixTags.NoMDEntryTypes:
                        int typeLength = _IFixReader.ReadInt();
                        _noMDEntryTypes = new char[typeLength];
                        _mdEntryArg = new string[typeLength];
                        return true;

                    case FixTags.MDEntryType:
                        _noMDEntryTypes[_mdEntryTypeCount++] = _IFixReader.ReadChar();
                        return true;

                    case FixTags.CFICode:
                        if (_cfiCode != null)
                            NewSecurityId();
                        _cfiCode = _IFixReader.ReadString();
                        return true;

                    case FixTags.StartDate:
                        _startDate = _IFixReader.ReadString();
                        return true;

                    case FixTags.EndDate:
                        _endDate = _IFixReader.ReadString();
                        return true;

                    case FixTags.MDEntryArg:
                        _mdEntryArg[_mdEntryArgCount++] = _IFixReader.ReadString();
                        return true;

                    case FixTags.AllowBuildFromSmallerTimeFrame:
                        _allowBuildFromSmallerTimeFrame = new bool?(_IFixReader.ReadBool());
                        return true;

                    case FixTags.CalcVolumeProfile:
                        _calcVolumeProfile = new bool?(_IFixReader.ReadBool());
                        return true;

                    case FixTags.FinishedCandles:
                        _finishedCandles = new bool?(_IFixReader.ReadBool());
                        return true;

                    case FixTags.MarketDataBuildMode:
                        _marketDataBuildMode = new int?(_IFixReader.ReadInt());
                        return true;

                    case FixTags.MarketDataBuildFrom:
                        _marketDataBuildFrom = new char?(_IFixReader.ReadChar());
                        return true;

                    case FixTags.MarketDataBuildField:
                        _marketDataBuildField = new char?(_IFixReader.ReadChar());
                        return true;

                    case FixTags.RegularTradingHours:
                        _regularTradingHours = new bool?(_IFixReader.ReadBool());
                        return true;

                    case FixTags.MarketDataCount:
                        _marketDataCount = new long?(_IFixReader.ReadLong());
                        return true;

                    case FixTags.MDResponseID:
                        _mdResponseId = _IFixReader.ReadString();
                        return true;

                    case FixTags.MarketDataSkip:
                        _marketDataSkip = new long?(_IFixReader.ReadLong());
                        return true;

                    default:
                        return false;
                }
            }

            internal DataType GetDataType(char _param1, int _param2) => _param1.ToDataType(_mdEntryArg[_param2]);
        }

        /// <summary>
        /// Read <see cref="T:StockSharp.Messages.MarketDataMessage" />.
        /// </summary>
        /// <param name="reader">_IFixReader.</param>
        /// <param name="dataBoundDateParser">Time parser.</param>
        /// <param name="handler">Message handler.</param>
        /// <param name="mdReqId">
        /// <see cref="F:StockSharp.Fix.Native.FixTags.MDReqID" /> value.</param>
        /// <param name="mdResponseId">
        /// <see cref="F:StockSharp.Fix.Native.FixTags.MDResponseID" /> value.</param>
        /// <returns>
        /// <see langword="true" /> message read ok, <see langword="false" /> message read with errors, <see langword="null" /> network problem.</returns>
        public static bool? ReadMarketDataMessages(this IFixReader reader,
                                                        FastDateTimeParser dataBoundDateParser,
                                                        Action<MarketDataMessage> handler,
                                                        out string mdReqId,
                                                        out string mdResponseId)
        {
            MarketDataReader msgReader = new MarketDataReader();
            msgReader._IFixReader = reader;

            if (msgReader._IFixReader == null)
                throw new ArgumentNullException(nameof(reader));

            if (dataBoundDateParser == null)
                throw new ArgumentNullException(nameof(dataBoundDateParser));

            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            mdReqId = null;
            mdResponseId = null;
            msgReader._mdReqId = null;
            msgReader._mdResponseId = null;
            msgReader._subscriptionRequestType = new char?();
            msgReader._noMDEntryTypes = null;
            msgReader._mdEntryArg = null;
            msgReader._mdEntryTypeCount = 0;
            msgReader._mdEntryArgCount = 0;
            msgReader._securityCode = null;
            msgReader._securityExchange = null;
            msgReader._cfiCode = null;
            msgReader._marketDepth = new int?();
            msgReader._startDate = null;
            msgReader._endDate = null;
            msgReader._allowBuildFromSmallerTimeFrame = new bool?();
            msgReader._calcVolumeProfile = new bool?();
            msgReader._finishedCandles = new bool?();
            msgReader._regularTradingHours = new bool?();
            msgReader._marketDataBuildMode = new int?();
            msgReader._marketDataBuildFrom = new char?();
            msgReader._marketDataBuildField = new char?();
            msgReader._marketDataSkip = new long?();
            msgReader._marketDataCount = new long?();
            msgReader._securityIds = null;
            msgReader._idCount = 0;
            msgReader._securityTypes = null;
            msgReader._securityTypeCount = -1;
            int num = msgReader._IFixReader.ReadMessage(new Func<FixTags, bool>(msgReader.ProcessFixTags)) ? 1 : 0;
            mdReqId = msgReader._mdReqId;
            mdResponseId = msgReader._mdResponseId;

            if (num == 0)
                return false;

            if (msgReader._securityCode != null)
                msgReader.NewSecurityId();

            DateTime? startDate = msgReader._startDate == null ? new DateTime?() : new DateTime?(dataBoundDateParser.Parse(msgReader._startDate).UtcKind());
            DateTime? endDate = msgReader._endDate == null ? new DateTime?() : new DateTime?(dataBoundDateParser.Parse(msgReader._endDate).UtcKind());

            bool isSubscribe = msgReader._subscriptionRequestType.IsSubscribe();
            bool isMarketDepth = false;

            foreach (DataType dataType in ((IEnumerable<char>)msgReader._noMDEntryTypes).Select(msgReader._readerFunction ?? (msgReader._readerFunction = new Func<char, int, DataType>(msgReader.GetDataType))))
            {
                if (isMarketDepth)
                {
                    if (dataType == DataType.MarketDepth)
                        continue;
                }
                else
                    isMarketDepth = dataType == DataType.MarketDepth;

                DateTime? nullable3;

                if (dataType == DataType.News)
                {
                    MarketDataMessage message = new MarketDataMessage();
                    string[] z1yCxaqBxron = msgReader._securityTypes;
                    SecurityTypes? nullable4;
                    if (z1yCxaqBxron == null)
                    {
                        nullable4 = new SecurityTypes?();
                    }
                    else
                    {
                        string type = ((IEnumerable<string>)z1yCxaqBxron).First();
                        nullable4 = type != null ? type.FromFixType() : new SecurityTypes?();
                    }
                    message.SecurityType = nullable4;
                    message.DataType2 = dataType;
                    message.IsSubscribe = isSubscribe;
                    nullable3 = startDate;
                    message.From = nullable3.HasValue ? new DateTimeOffset?(nullable3.GetValueOrDefault()) : new DateTimeOffset?();
                    nullable3 = endDate;
                    message.To = nullable3.HasValue ? new DateTimeOffset?(nullable3.GetValueOrDefault()) : new DateTimeOffset?();
                    message.Skip = msgReader._marketDataSkip;
                    message.Count = msgReader._idCount;
                    MarketDataMessage marketDataMessage = message.ValidateBounds();
                    handler(marketDataMessage);
                }
                else
                {
                    int index = 0;

                    foreach (SecurityId securityId in msgReader._securityIds)
                    {
                        var msg = new MarketDataMessage();
                        msg.SecurityId = securityId;
                        string[] securityTypes = msgReader._securityTypes;
                        SecurityTypes? secType;

                        if (securityTypes == null)
                        {
                            secType = new SecurityTypes?();
                        }
                        else
                        {
                            string type = securityTypes[index];
                            secType = type != null ? type.FromFixType() : new SecurityTypes?();
                        }

                        msg.SecurityType = secType;
                        msg.DataType2 = dataType;
                        msg.IsSubscribe = isSubscribe;
                        msg.MaxDepth = msgReader._marketDepth;
                        msg.From = startDate.HasValue ? new DateTimeOffset?(startDate.GetValueOrDefault()) : new DateTimeOffset?();
                        msg.To = endDate.HasValue ? new DateTimeOffset?(endDate.GetValueOrDefault()) : new DateTimeOffset?();
                        msg.Skip = msgReader._marketDataSkip;
                        msg.Count = msgReader._idCount;

                        MarketDataMessage marketDataMessage = msg.ValidateBounds();

                        if (msgReader._allowBuildFromSmallerTimeFrame.HasValue)
                            marketDataMessage.AllowBuildFromSmallerTimeFrame = msgReader._allowBuildFromSmallerTimeFrame.Value;
                        if (msgReader._regularTradingHours.HasValue)
                            marketDataMessage.IsRegularTradingHours = msgReader._regularTradingHours.Value;
                        if (msgReader._calcVolumeProfile.HasValue)
                            marketDataMessage.IsCalcVolumeProfile = msgReader._calcVolumeProfile.Value;
                        if (msgReader._finishedCandles.HasValue)
                            marketDataMessage.IsFinishedOnly = msgReader._finishedCandles.Value;
                        if (msgReader._marketDataBuildMode.HasValue)
                            marketDataMessage.BuildMode = (MarketDataBuildModes)msgReader._marketDataBuildMode.Value;
                        if (msgReader._marketDataBuildFrom.HasValue)
                            marketDataMessage.BuildFrom = msgReader._marketDataBuildFrom.Value.ToDataType(null);
                        if (msgReader._marketDataBuildField.HasValue)
                            marketDataMessage.BuildField = new Level1Fields?(msgReader._marketDataBuildField.Value.ToLevel1());
                        handler(marketDataMessage);
                        ++index;
                    }
                }
            }
            return true;
        }

        private sealed class UserInfoReader
        {
            public UserInfoMessage _userInfoMsg;
            public IFixReader _IFixReader;
            public IPAddress[] _ipAddress;
            public int _ipRestrictionsCount;
            public FastDateTimeParser _SettlDate;
            public int _partyIdsCount;
            public Func<FixTags, bool> _handler;

            internal bool ProcessFixTags(FixTags _param1)
            {
                switch (_param1)
                {
                    case FixTags.MaxFloor:
                        _userInfoMsg.UploadLimit = _IFixReader.ReadLong();
                        return true;
                    case FixTags.IssueDate:
                        _userInfoMsg.CreationDate = new DateTimeOffset?(_IFixReader.ReadUtc(_SettlDate));
                        return true;
                    case FixTags.PartyID:
                        _userInfoMsg.Features[_partyIdsCount++] = _IFixReader.ReadString();
                        return true;
                    case FixTags.NoPartyIDs:
                        _userInfoMsg.Features = new string[_IFixReader.ReadInt()];
                        _partyIdsCount = 0;
                        return true;
                    case FixTags.Username:
                        _userInfoMsg.Login = _IFixReader.ReadString();
                        return true;
                    case FixTags.Password:
                        _userInfoMsg.Password = _IFixReader.ReadString().Secure();
                        return true;
                    case FixTags.PublishTrdIndicator:
                        _userInfoMsg.CanPublish = _IFixReader.ReadBool();
                        return true;
                    case FixTags.AgreementID:
                        _userInfoMsg.IsAgreementAccepted = new bool?(_IFixReader.ReadBool());
                        return true;
                    case FixTags.UserStatus:
                        _userInfoMsg.IsBlocked = _IFixReader.ReadInt() == 8;
                        return true;
                    case FixTags.TradeVolume:
                        _userInfoMsg.Balance = new decimal?(_IFixReader.ReadDecimal());
                        return true;
                    case FixTags.Id:
                        _userInfoMsg.Id = new long?(_IFixReader.ReadLong());
                        return true;
                    case FixTags.NoIpRestrictions:
                        _ipAddress = new IPAddress[_IFixReader.ReadInt()];
                        _ipRestrictionsCount = 0;
                        return true;
                    case FixTags.IpRestrictions:
                        _ipAddress[_ipRestrictionsCount++] = _IFixReader.ReadString().To<IPAddress>();
                        return true;
                    case FixTags.NoPermissions:
                        _IFixReader.ReadInt();
                        return true;
                    case FixTags.Permissions:
                        _userInfoMsg.Permissions.Add((UserPermissions)_IFixReader.ReadInt(), (IDictionary<Tuple<string, string, object, DateTime?>, bool>)new Dictionary<Tuple<string, string, object, DateTime?>, bool>());
                        return true;
                    case FixTags.Language:
                        _userInfoMsg.Language = _IFixReader.ReadString();
                        return true;
                    case FixTags.Picture:
                        _userInfoMsg.Avatar = new long?(_IFixReader.ReadLong());
                        return true;
                    case FixTags.DisplayName:
                        _userInfoMsg.DisplayName = _IFixReader.ReadString();
                        return true;
                    case FixTags.Phone:
                        _userInfoMsg.Phone = _IFixReader.ReadString();
                        return true;
                    case FixTags.Homepage:
                        _userInfoMsg.Homepage = _IFixReader.ReadString();
                        return true;
                    case FixTags.Skype:
                        _userInfoMsg.Skype = _IFixReader.ReadString();
                        return true;
                    case FixTags.City:
                        _userInfoMsg.City = _IFixReader.ReadString();
                        return true;
                    case FixTags.Gender:
                        _userInfoMsg.Gender = new bool?(_IFixReader.ReadBool());
                        return true;
                    case FixTags.Subscription:
                        _userInfoMsg.IsSubscription = new bool?(_IFixReader.ReadBool());
                        return true;
                    case FixTags.Token:
                        _userInfoMsg.AuthToken = _IFixReader.ReadString();
                        return true;
                    case FixTags.TrialAllow:
                        _userInfoMsg.IsTrialVerified = _IFixReader.ReadBool();
                        return true;
                    default:
                        return this._handler(_param1);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dateParser"></param>
        /// <param name="handler"></param>
        /// <param name="messageHandler"></param>
        /// <returns></returns>
        public static bool? ReadUserInfoMessage(this IFixReader reader, FastDateTimeParser dateParser, Func<FixTags, bool> handler, Action<Message> messageHandler)
        {
            var userInfo = new Extensions.UserInfoReader();
            userInfo._IFixReader = reader;
            userInfo._SettlDate = dateParser;
            userInfo._handler = handler;
            userInfo._userInfoMsg = new UserInfoMessage();
            userInfo._ipAddress = (IPAddress[])null;
            userInfo._ipRestrictionsCount = -1;
            userInfo._partyIdsCount = -1;

            if (!userInfo._IFixReader.ReadMessage(new Func<FixTags, bool>(userInfo.ProcessFixTags)))
                return false;

            userInfo._userInfoMsg.IpRestrictions = (IEnumerable<IPAddress>)userInfo._ipAddress ?? Enumerable.Empty<IPAddress>();

            if (userInfo._userInfoMsg.Features == null)
                userInfo._userInfoMsg.Features = ArrayHelper.Empty<string>();

            messageHandler((Message)userInfo._userInfoMsg);

            return true;
        }

        
        //public static bool? ReadProductFeedback( this IFixReader reader, Action<Message> messageHandler, FastDateTimeParser parser )
        //{
        //    var fbReader              = new Extensions.FeedBackReader();
        //    fbReader._IFixReader = reader;
        //    fbReader._dateTimeParser = parser;
        //    fbReader._product = new long?( );
        //    fbReader._id = new long?( );
        //    fbReader._MassStatusReqID = new long?( );
        //    fbReader._mdResponseID = new long?( );
        //    fbReader._issueDate = new DateTimeOffset?( );
        //    fbReader._rating = new int?( );
        //    fbReader._owner = new long?( );
        //    fbReader._text = ( string ) null;

        //    if ( !fbReader._IFixReader.ReadMessage( new Func<FixTags, bool>( fbReader.ProcessFixTags ) ) )
        //        return false;

        //    Action<Message> action    = messageHandler;
        //    var msg                   = new ProductFeedbackMessage();
        //    msg.TransactionId = fbReader._MassStatusReqID.GetValueOrDefault( );
        //    msg.OriginalTransactionId = fbReader._mdResponseID.GetValueOrDefault( );
        //    msg.ProductId = fbReader._product.GetValueOrDefault( );
        //    msg.Id = fbReader._id.GetValueOrDefault( );
        //    msg.Text = fbReader._text;
        //    msg.Rating = fbReader._rating.GetValueOrDefault( );
        //    msg.Author = fbReader._owner.GetValueOrDefault( );
        //    msg.CreationDate = fbReader._issueDate.GetValueOrDefault( );

        //    action( ( Message ) msg );
        //    return true;
        //}

        //private sealed class ProductPermissionReader
        //{
        //    public ProductPermissionMessage _userInfoMsg;
        //    public IFixReader _IFixReader;

        //    internal bool ProcessFixTags( FixTags _param1 )
        //    {
        //        switch ( _param1 )
        //        {
        //            case FixTags.Product:
        //                _userInfoMsg.ProductId = _IFixReader.ReadLong( );
        //                return true;
        //            case FixTags.Username:
        //                _userInfoMsg.UserId = _IFixReader.ReadLong( );
        //                return true;
        //            case FixTags.Command:
        //                _userInfoMsg.Command = new CommandTypes?( ( CommandTypes ) _IFixReader.ReadInt( ) );
        //                return true;
        //            case FixTags.MDResponseID:
        //                _userInfoMsg.OriginalTransactionId = _IFixReader.ReadLong( );
        //                return true;
        //            case FixTags.Owner:
        //                _userInfoMsg.IsManager = _IFixReader.ReadBool( );
        //                return true;
        //            default:
        //                return false;
        //        }
        //    }
        //}

        
        //public static bool? ReadProductPermission( this IFixReader reader, Action<Message> messageHandler )
        //{
        //    var premission = new Extensions.ProductPermissionReader();
        //    premission._IFixReader = reader;
        //    premission._userInfoMsg = new ProductPermissionMessage( );
        //    if ( !premission._IFixReader.ReadMessage( new Func<FixTags, bool>( premission.ProcessFixTags ) ) )
        //        return false;
        //    messageHandler( ( Message ) premission._userInfoMsg );
        //    return true;
        //}

        private sealed class NewsReader
        {
            public long? _MassStatusReqID;
            public IFixReader _IFixReader;
            public long? _mdResponseID;
            public string _id;
            public DateTimeOffset? _sendingTime;
            public FastDateTimeParser _dateTimeParser;
            public char? _urgency;
            public string _securityCode;
            public string _boardCode;
            public string _urlLink;
            public string _headline;
            public string _language;
            public string _text;
            public string _idSource;
            public DateTimeOffset? _expireTime;
            public DateTimeOffset? _origTime;
            public long[] _underlyingSymbols;
            public int _symbolsCount;

            internal bool ProcessFixTags(FixTags _param1)
            {
                switch (_param1)
                {
                    case FixTags.IDSource:
                        this._idSource = _IFixReader.ReadString();
                        return true;
                    case FixTags.OrigTime:
                        _origTime = new DateTimeOffset?(_IFixReader.ReadUtc(_dateTimeParser));
                        return true;
                    case FixTags.SendingTime:
                        _sendingTime = new DateTimeOffset?(_IFixReader.ReadUtc(_dateTimeParser));
                        return true;
                    case FixTags.Symbol:
                        _securityCode = _IFixReader.ReadString();
                        return true;
                    case FixTags.Text:
                        _text = _IFixReader.ReadString();
                        return true;
                    case FixTags.Urgency:
                        _urgency = new char?(_IFixReader.ReadChar());
                        return true;
                    case FixTags.ExpireTime:
                        _expireTime = new DateTimeOffset?(_IFixReader.ReadUtc(_dateTimeParser));
                        return true;
                    case FixTags.NoRelatedSym:
                        _IFixReader.ReadInt();
                        return true;
                    case FixTags.Headline:
                        _headline = _IFixReader.ReadString();
                        return true;
                    case FixTags.URLLink:
                        _urlLink = _IFixReader.ReadString();
                        return true;
                    case FixTags.SecurityExchange:
                        _boardCode = _IFixReader.ReadString();
                        return true;
                    case FixTags.MDReqID:
                        _MassStatusReqID = new long?(_IFixReader.ReadLong());
                        return true;
                    case FixTags.MDEntryId:
                        _id = _IFixReader.ReadString();
                        return true;
                    case FixTags.UnderlyingSymbol:
                        _underlyingSymbols[_symbolsCount++] = _IFixReader.ReadLong();
                        return true;
                    case FixTags.NoUnderlyings:
                        _underlyingSymbols = new long[_IFixReader.ReadInt()];
                        _symbolsCount = 0;
                        return true;
                    case FixTags.MDResponseID:
                        _mdResponseID = new long?(_IFixReader.ReadLong());
                        return true;
                    case FixTags.Language:
                        _language = _IFixReader.ReadString();
                        return true;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// Read <see cref="T:StockSharp.Messages.NewsMessage" />.
        /// </summary>
        /// <param name="reader">_IFixReader.</param>
        /// <param name="parser">Time parser.</param>
        /// <param name="messageHandler">Message handler.</param>
        /// <returns>
        /// <see langword="true" /> message read ok, <see langword="false" /> message read with errors, <see langword="null" /> network problem.</returns>
        public static bool? ReadNews(
          this IFixReader reader,
          Action<Message> messageHandler,
          FastDateTimeParser parser)
        {
            Extensions.NewsReader news = new Extensions.NewsReader();
            news._IFixReader = reader;
            news._dateTimeParser = parser;
            news._MassStatusReqID = new long?();
            news._mdResponseID = new long?();
            news._id = (string)null;
            news._securityCode = (string)null;
            news._boardCode = (string)null;
            news._urlLink = (string)null;
            news._headline = (string)null;
            news._sendingTime = new DateTimeOffset?();
            news._origTime = new DateTimeOffset?();
            news._expireTime = new DateTimeOffset?();
            news._urgency = new char?();
            news._language = (string)null;
            news._text = (string)null;
            news._idSource = (string)null;
            news._underlyingSymbols = (long[])null;
            news._symbolsCount = -1;

            if (!news._IFixReader.ReadMessage(new Func<FixTags, bool>(news.ProcessFixTags)))
                return false;
            SecurityId securityId = new SecurityId();
            if (news._securityCode != null)
                securityId = new SecurityId()
                {
                    SecurityCode = news._securityCode,
                    BoardCode = news._boardCode
                };
            Action<Message> action = messageHandler;
            NewsMessage msg = new NewsMessage();
            msg.TransactionId = news._MassStatusReqID.GetValueOrDefault();
            msg.OriginalTransactionId = news._mdResponseID.GetValueOrDefault();
            msg.Id = news._id;
            msg.Url = news._urlLink;
            msg.Headline = news._headline;
            msg.ServerTime = news._origTime ?? news._sendingTime ?? DateTimeOffset.Now;
            msg.SecurityId = new SecurityId?(securityId);
            msg.BoardCode = news._boardCode;
            msg.Priority = news._urgency.ToNewsPriority();
            msg.ExpiryDate = news._expireTime;
            msg.Language = news._language;
            msg.Source = news._idSource;
            msg.Story = news._text;
            msg.Attachments = news._underlyingSymbols ?? ArrayHelper.Empty<long>();
            action((Message)msg);
            return new bool?(true);
        }

        //private sealed class ProductCategoryReader
        //{
        //    public ProductCategoryMessage _userInfoMsg;
        //    public IFixReader _IFixReader;

        //    internal bool ProcessFixTags( FixTags _param1 )
        //    {
        //        switch ( _param1 )
        //        {
        //            case FixTags.Name:
        //                _userInfoMsg.Name = _IFixReader.ReadString( );
        //                return true;
        //            case FixTags.Id:
        //                _userInfoMsg.Id = _IFixReader.ReadLong( );
        //                return true;
        //            case FixTags.MDResponseID:
        //                _userInfoMsg.OriginalTransactionId = _IFixReader.ReadLong( );
        //                return true;
        //            default:
        //                return false;
        //        }
        //    }
        //}

        ///// <summary>
        ///// Read <see cref="T:StockSharp.Community.ProductCategoryMessage" />.
        ///// </summary>
        ///// <param name="reader">_IFixReader.</param>
        ///// <param name="messageHandler">Message handler.</param>
        ///// <returns>
        ///// <see langword="true" /> message read ok, <see langword="false" /> message read with errors, <see langword="null" /> network problem.</returns>
        //public static bool? ReadProductCategory( this IFixReader fixReader, Action<Message> messageHandler )
        //{
        //    Extensions.ProductCategoryReader reader = new Extensions.ProductCategoryReader();
        //    reader._IFixReader = fixReader;
        //    reader._userInfoMsg = new ProductCategoryMessage( );
        //    if ( !reader._IFixReader.ReadMessage( new Func<FixTags, bool>( reader.ProcessFixTags ) ) )
        //        return false;
        //    messageHandler( ( Message ) reader._userInfoMsg );
        //    return true; ;
        //}




        ///// <summary>
        ///// Read <see cref="T:StockSharp.Community.ProductInfoMessage" />.
        ///// </summary>
        ///// <param name="reader">_IFixReader.</param>
        ///// <param name="parser">Time parser.</param>
        ///// <param name="messageHandler">Message handler.</param>
        ///// <returns>
        ///// <see langword="true" /> message read ok, <see langword="false" /> message read with errors, <see langword="null" /> network problem.</returns>
        //public static bool? ProcessProductInfo( this IFixReader reader, Action<Message> messageHandler, FastDateTimeParser parser )
        //{
        //    Extensions.ProductInfoReader info = new Extensions.ProductInfoReader();

        //    info._IFixReader                    = reader;
        //    info._dateTimeParser                = parser;
        //    info._price                         = new decimal?( );
        //    info._currency                      = new CurrencyTypes?( );
        //    info._price2                        = new decimal?( );
        //    info._currency2                     = new CurrencyTypes?( );
        //    info._renewMonthlyPrice             = new decimal?( );
        //    info._renewMonthlyPriceCurrency     = new CurrencyTypes?( );
        //    info._renewAnnualPrice              = new decimal?( );
        //    info._renewAnnualPriceCurrency      = new CurrencyTypes?( );
        //    info._monthlyPrice                  = new decimal?( );
        //    info._monthlyPriceCurrency          = new CurrencyTypes?( );
        //    info._lifetimePrice                 = new decimal?( );
        //    info._lifetimePriceCurrency         = new CurrencyTypes?( );
        //    info._discountMonthlyPrice          = new decimal?( );
        //    info._discountMonthlyPriceCurrency  = new CurrencyTypes?( );
        //    info._discountAnnualPrice           = new decimal?( );
        //    info._discountAnnualPriceCurrency   = new CurrencyTypes?( );
        //    info._discountLifetimePrice         = new decimal?( );
        //    info._discountLifetimePriceCurrency = new CurrencyTypes?( );
        //    info._noStubCount                   = -1;
        //    info._realVersion                   = ( string ) null;
        //    info._stubVersion                   = ( string ) null;
        //    info._productCategories             = new List<long>( );
        //    info._productInfoMsg                = new ProductInfoMessage( );

        //    if ( !info._IFixReader.ReadMessage( new Func<FixTags, bool>( info.ProcessFixTags ) ) )
        //    {
        //        return false;
        //    }

        //    if ( info._productCategories.Any<long>( ) )
        //    {
        //        info._productInfoMsg.Categories = info._productCategories.ToArray( );
        //    }

        //    if ( info._realVersion != null || info._stubVersion != null )
        //    {
        //        info.CreateStubVersions( );
        //    }


        //    ProductInfoMessage productInfo = info._productInfoMsg;
        //    StockSharp.Messages.Currency annualPrice;

        //    if ( info._price.HasValue )
        //    {
        //        var curr   = new StockSharp.Messages.Currency();                
        //        curr.Type  = info._currency.HasValue ? info._currency.GetValueOrDefault( ) : CurrencyTypes.USD;
        //        curr.Value = info._price.Value;

        //        annualPrice = curr;
        //    }
        //    else
        //        annualPrice = null;

        //    productInfo.AnnualPrice = annualPrice;

        //    StockSharp.Messages.Currency renewPrice;

        //    if ( info._price2.HasValue )
        //    {
        //        var curr   = new StockSharp.Messages.Currency();

        //        curr.Type  = info._currency2.HasValue ? info._currency2.GetValueOrDefault( ) : CurrencyTypes.USD;
        //        curr.Value = info._price2.Value;
        //        renewPrice = curr;
        //    }
        //    else
        //        renewPrice = null;

        //    productInfo.RenewPrice = renewPrice;

        //    StockSharp.Messages.Currency renewMonthlyPrice;
        //    if ( info._renewMonthlyPrice.HasValue )
        //    {
        //        var curr          = new StockSharp.Messages.Currency();

        //        curr.Type         = info._renewMonthlyPriceCurrency.HasValue ? info._renewMonthlyPriceCurrency.GetValueOrDefault( ) : CurrencyTypes.USD;
        //        curr.Value        = info._renewMonthlyPrice.Value;
        //        renewMonthlyPrice = curr;
        //    }
        //    else
        //        renewMonthlyPrice = null;

        //    productInfo.RenewMonthlyPrice = renewMonthlyPrice;


        //    StockSharp.Messages.Currency renewAnnualPrice;
        //    if ( info._renewAnnualPrice.HasValue )
        //    {
        //        var curr = new StockSharp.Messages.Currency();

        //        curr.Type = info._renewAnnualPriceCurrency.HasValue ? info._renewAnnualPriceCurrency.GetValueOrDefault( ) : CurrencyTypes.USD;
        //        curr.Value = info._renewAnnualPrice.Value;
        //        renewAnnualPrice = curr;
        //    }
        //    else
        //        renewAnnualPrice = null;

        //    productInfo.RenewAnnualPrice = renewAnnualPrice;

        //    StockSharp.Messages.Currency MonthlyPrice;
        //    if ( info._monthlyPrice.HasValue )
        //    {
        //        var curr = new StockSharp.Messages.Currency();

        //        curr.Type = info._monthlyPriceCurrency.HasValue ? info._monthlyPriceCurrency.GetValueOrDefault( ) : CurrencyTypes.USD;
        //        curr.Value = info._monthlyPrice.Value;
        //        MonthlyPrice = curr;
        //    }
        //    else
        //        MonthlyPrice = ( StockSharp.Messages.Currency ) null;
        //    productInfo.MonthlyPrice = MonthlyPrice;                        

        //    StockSharp.Messages.Currency lifetimePrice;
        //    if ( info._lifetimePrice.HasValue )
        //    {
        //        var curr = new StockSharp.Messages.Currency();

        //        curr.Type = info._lifetimePriceCurrency.HasValue ? info._lifetimePriceCurrency.GetValueOrDefault( ) : CurrencyTypes.USD;
        //        curr.Value = info._lifetimePrice.Value;
        //        lifetimePrice = curr;
        //    }
        //    else
        //        lifetimePrice = null;
        //    productInfo.LifetimePrice = lifetimePrice;

        //    StockSharp.Messages.Currency discountMonthlyPrice;
        //    if ( info._discountMonthlyPrice.HasValue )
        //    {
        //        var curr = new StockSharp.Messages.Currency();

        //        curr.Type = info._discountMonthlyPriceCurrency.HasValue ? info._discountMonthlyPriceCurrency.GetValueOrDefault( ) : CurrencyTypes.USD;
        //        curr.Value = info._discountMonthlyPrice.Value;
        //        discountMonthlyPrice = curr;
        //    }
        //    else
        //        discountMonthlyPrice = null;

        //    productInfo.DiscountMonthlyPrice = discountMonthlyPrice;

        //    StockSharp.Messages.Currency discountAnnualPrice;

        //    if ( info._discountAnnualPrice.HasValue )
        //    {
        //        var curr = new StockSharp.Messages.Currency();                
        //        curr.Type = info._discountAnnualPriceCurrency.HasValue ? info._discountAnnualPriceCurrency.GetValueOrDefault( ) : CurrencyTypes.USD;
        //        curr.Value = info._discountAnnualPrice.Value;
        //        discountAnnualPrice = curr;
        //    }
        //    else
        //        discountAnnualPrice = null;

        //    productInfo.DiscountAnnualPrice = discountAnnualPrice;

        //    StockSharp.Messages.Currency discountLifetimePrice;
        //    if ( info._discountLifetimePrice.HasValue )
        //    {
        //        var curr = new StockSharp.Messages.Currency();                
        //        curr.Type = info._discountLifetimePriceCurrency.HasValue ? info._discountLifetimePriceCurrency.GetValueOrDefault( ) : CurrencyTypes.USD;
        //        curr.Value = info._discountLifetimePrice.Value;
        //        discountLifetimePrice = curr;
        //    }
        //    else
        //        discountLifetimePrice = null;

        //    productInfo.DiscountLifetimePrice = discountLifetimePrice;
        //    messageHandler( ( Message ) info._productInfoMsg );

        //    return new bool?( true );
        //}








        //private sealed class ProductInfoReader
        //{
        //    public ProductInfoMessage _productInfoMsg;
        //    public int                _noStubCount;
        //    public string             _realVersion;
        //    public string             _stubVersion;
        //    public IFixReader         _IFixReader;
        //    public decimal?           _price;
        //    public CurrencyTypes?     _currency;
        //    public decimal?           _price2;
        //    public CurrencyTypes?     _currency2;
        //    public decimal?           _renewMonthlyPrice;
        //    public CurrencyTypes?     _renewMonthlyPriceCurrency;
        //    public decimal?           _renewAnnualPrice;
        //    public CurrencyTypes?     _renewAnnualPriceCurrency;
        //    public decimal?           _monthlyPrice;
        //    public CurrencyTypes?     _monthlyPriceCurrency;
        //    public decimal?           _lifetimePrice;
        //    public CurrencyTypes?     _lifetimePriceCurrency;
        //    public decimal?           _discountMonthlyPrice;
        //    public CurrencyTypes?     _discountMonthlyPriceCurrency;
        //    public decimal?           _discountAnnualPrice;
        //    public CurrencyTypes?     _discountAnnualPriceCurrency;
        //    public decimal?           _discountLifetimePrice;
        //    public CurrencyTypes?     _discountLifetimePriceCurrency;
        //    public FastDateTimeParser _dateTimeParser;
        //    public List<long>         _productCategories;

        //    internal void CreateStubVersions( )
        //    {
        //        _productInfoMsg.StubVersions[ _noStubCount ] = Tuple.Create<string, string>( _realVersion, _stubVersion );
        //        ++_noStubCount;
        //        _realVersion = _stubVersion = ( string ) null;
        //    }

        //    internal bool ProcessFixTags( FixTags _param1 )
        //    {
        //        switch ( _param1 )
        //        {
        //            case FixTags.Currency:
        //                _currency = new CurrencyTypes?( ( CurrencyTypes ) _IFixReader.ReadInt( ) );
        //                return true;
        //            case FixTags.Price:
        //                _price = new decimal?( _IFixReader.ReadDecimal( ) );
        //                return true;
        //            case FixTags.Text:
        //                _productInfoMsg.Description = _IFixReader.ReadString( );
        //                return true;
        //            case FixTags.ExpireTime:
        //                _productInfoMsg.PurchasedTill = new DateTimeOffset?( _IFixReader.ReadUtc( _dateTimeParser ) );
        //                return true;
        //            case FixTags.URLLink:
        //                _productInfoMsg.PackageId = _IFixReader.ReadString( );
        //                return true;
        //            case FixTags.MDReqID:
        //                _productInfoMsg.OriginalTransactionId = _IFixReader.ReadLong( );
        //                return true;
        //            case FixTags.RefTagID:
        //                _productInfoMsg.Tags = _IFixReader.ReadString( );
        //                return true;
        //            case FixTags.Scope:
        //                _productInfoMsg.Scope = ( ProductScopes ) _IFixReader.ReadInt( );
        //                return true;
        //            case FixTags.Price2:
        //                _price2 = new decimal?( _IFixReader.ReadDecimal( ) );
        //                return true;
        //            case FixTags.OrderCategory:
        //                _productInfoMsg.Target = _IFixReader.ReadString( );
        //                return true;
        //            case FixTags.CstmApplVerID:
        //                _productInfoMsg.LatestVersion = _IFixReader.ReadString( );
        //                return true;
        //            case FixTags.ExtendedTradeStatus:
        //                _productInfoMsg.Flags = ( ProductInfoFlags ) _IFixReader.ReadInt( );
        //                return true;
        //            case FixTags.Name:
        //                _productInfoMsg.Name = _IFixReader.ReadString( );
        //                return true;
        //            case FixTags.Id:
        //                _productInfoMsg.Id = _IFixReader.ReadLong( );
        //                return true;
        //            case FixTags.ContentType:
        //                _productInfoMsg.ContentType = ( ProductContentTypes ) _IFixReader.ReadInt( );
        //                return true;
        //            case FixTags.Content:
        //                _productInfoMsg.Extra = _IFixReader.ReadString( );
        //                return true;
        //            case FixTags.Owner:
        //                _productInfoMsg.Author = _IFixReader.ReadLong( );
        //                return true;
        //            case FixTags.Picture:
        //                _productInfoMsg.Picture = _IFixReader.ReadLong( );
        //                return true;
        //            case FixTags.DownloadCount:
        //                _productInfoMsg.DownloadCount = _IFixReader.ReadInt( );
        //                return true;
        //            case FixTags.Rating:
        //                _productInfoMsg.Rating = new decimal?( _IFixReader.ReadDecimal( ) );
        //                return true;
        //            case FixTags.DocUrl:
        //                _productInfoMsg.DocUrl = _IFixReader.ReadString( );
        //                return true;
        //            case FixTags.SupportedPlugins:
        //                _productInfoMsg.SupportedPlugins = new long?( _IFixReader.ReadLong( ) );
        //                return true;
        //            case FixTags.Repository:
        //                _productInfoMsg.Repository = ( PackageRepositories ) _IFixReader.ReadInt( );
        //                return true;
        //            case FixTags.Currency2:
        //                _currency2 = new CurrencyTypes?( ( CurrencyTypes ) _IFixReader.ReadInt( ) );
        //                return true;
        //            case FixTags.MonthlyPrice:
        //                _monthlyPrice = new decimal?( _IFixReader.ReadDecimal( ) );
        //                return true;
        //            case FixTags.MonthlyPriceCurrency:
        //                _monthlyPriceCurrency = new CurrencyTypes?( ( CurrencyTypes ) _IFixReader.ReadInt( ) );
        //                return true;
        //            case FixTags.LifetimePrice:
        //                _lifetimePrice = new decimal?( _IFixReader.ReadDecimal( ) );
        //                return true;
        //            case FixTags.LifetimePriceCurrency:
        //                _lifetimePriceCurrency = new CurrencyTypes?( ( CurrencyTypes ) _IFixReader.ReadInt( ) );
        //                return true;
        //            case FixTags.NoStubCount:
        //                _productInfoMsg.StubVersions = new Tuple<string, string>[ _IFixReader.ReadInt( ) ];
        //                _noStubCount = 0;
        //                return true;
        //            case FixTags.RealVersion:
        //                if ( _realVersion != null )
        //                    CreateStubVersions( );
        //                _realVersion = _IFixReader.ReadString( );
        //                return true;
        //            case FixTags.StubVersion:
        //                if ( _stubVersion != null )
        //                    CreateStubVersions( );
        //                _stubVersion = _IFixReader.ReadString( );
        //                return true;
        //            case FixTags.DiscountMonthlyPrice:
        //                _discountMonthlyPrice = new decimal?( _IFixReader.ReadDecimal( ) );
        //                return true;
        //            case FixTags.DiscountMonthlyPriceCurrency:
        //                _discountMonthlyPriceCurrency = new CurrencyTypes?( ( CurrencyTypes ) _IFixReader.ReadInt( ) );
        //                return true;
        //            case FixTags.DiscountAnnualPrice:
        //                _discountAnnualPrice = new decimal?( _IFixReader.ReadDecimal( ) );
        //                return true;
        //            case FixTags.DiscountAnnualPriceCurrency:
        //                _discountAnnualPriceCurrency = new CurrencyTypes?( ( CurrencyTypes ) _IFixReader.ReadInt( ) );
        //                return true;
        //            case FixTags.DiscountLifetimePrice:
        //                _discountLifetimePrice = new decimal?( _IFixReader.ReadDecimal( ) );
        //                return true;
        //            case FixTags.DiscountLifetimePriceCurrency:
        //                _discountLifetimePriceCurrency = new CurrencyTypes?( ( CurrencyTypes ) _IFixReader.ReadInt( ) );
        //                return true;
        //            case FixTags.RenewMonthlyPrice:
        //                _renewMonthlyPrice = new decimal?( _IFixReader.ReadDecimal( ) );
        //                return true;
        //            case FixTags.RenewMonthlyPriceCurrency:
        //                _renewMonthlyPriceCurrency = new CurrencyTypes?( ( CurrencyTypes ) _IFixReader.ReadInt( ) );
        //                return true;
        //            case FixTags.ProductCategories:
        //                _productCategories.Add( _IFixReader.ReadLong( ) );
        //                return true;
        //            case FixTags.RenewAnnualPrice:
        //                _renewAnnualPrice = new decimal?( _IFixReader.ReadDecimal( ) );
        //                return true;
        //            case FixTags.RenewAnnualPriceCurrency:
        //                _renewAnnualPriceCurrency = new CurrencyTypes?( ( CurrencyTypes ) _IFixReader.ReadInt( ) );
        //                return true;
        //            default:
        //                return false;
        //        }
        //    }
        //}













    }
}
