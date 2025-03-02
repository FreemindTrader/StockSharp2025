using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Algo.Storages;
using StockSharp.Algo.Storages.Remote.Messages;
using StockSharp.Algo.Strategies.Messages;
using StockSharp.Algo.Testing;

using StockSharp.Fix.Native;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockSharp.Fix.Dialects
{
    /// <summary>
    /// The default implementation of <see cref="T:StockSharp.Fix.Dialects.IFixDialect" />.
    /// </summary>
    [DisplayNameLoc("Default")]
    public class DefaultFixDialect : BaseFixDialect
    {

        private readonly IEnumerable<MessageTypeInfo> _supportedMsgs = new MessageTypeInfo[36]
                                                                        {
                                                                            MessageTypes.MarketData.ToInfo(),
                                                                            MessageTypes.SecurityLookup.ToInfo(),
                                                                            MessageTypes.BoardLookup.ToInfo(new bool?(true)),
                                                                            MessageTypes.Portfolio.ToInfo(),
                                                                            MessageTypes.PortfolioLookup.ToInfo(),
                                                                            MessageTypes.OrderRegister.ToInfo(),
                                                                            MessageTypes.OrderReplace.ToInfo(),
                                                                            MessageTypes.OrderCancel.ToInfo(),
                                                                            MessageTypes.OrderGroupCancel.ToInfo(),
                                                                            MessageTypes.OrderStatus.ToInfo(),
                                                                            MessageTypes.ChangePassword.ToInfo(),
                                                                            ((MessageTypes) (-5000) ).ToInfo(),
                                                                            ((MessageTypes) (-5001)).ToInfo(),
                                                                            new MessageTypeInfo(~MessageTypes.OrderPairReplace, new bool?(), "EmulationState",  null),
                                                                            new MessageTypeInfo(~MessageTypes.QuoteChange, new bool?(), "ChangeTimeInterval",  null),
                                                                            MessageTypes.UserInfo.ToInfo(),
                                                                            MessageTypes.UserLookup.ToInfo(),
                                                                            MessageTypes.UserRequest.ToInfo(),
                                                                            MessageTypes.TimeFrameLookup.ToInfo(new bool?(true)),
                                                                            new MessageTypeInfo(~MessageTypes.PortfolioChange, new bool?(), "StrategyInfo",  null),
                                                                            new MessageTypeInfo(~MessageTypes.NativeSecurityId, new bool?(), "StrategyState",  null),
                                                                            MessageTypes.SecurityMappingRequest.ToInfo(new bool?(true)),
                                                                            MessageTypes.SecurityLegsRequest.ToInfo(new bool?(true)),
                                                                            MessageTypes.AdapterListRequest.ToInfo(),
                                                                            MessageTypes.Command.ToInfo(),
                                                                            MessageTypes.SecurityRouteListRequest.ToInfo(new bool?(true)),
                                                                            MessageTypes.PortfolioRouteListRequest.ToInfo(new bool?(false)),
                                                                            MessageTypes.SubscriptionListRequest.ToInfo(new bool?(true)),
                                                                            MessageTypes.SecurityMapping.ToInfo(new bool?(true)),
                                                                            MessageTypes.Security.ToInfo(),
                                                                            MessageTypes.Remove.ToInfo(),
                                                                            MessageTypes.News.ToInfo(),
                                                                            new MessageTypeInfo(~MessageTypes.PortfolioLookup, new bool?(true), "RemoteFileCommand",  null),
                                                                            new MessageTypeInfo(~MessageTypes.OrderStatus, new bool?(true), "AvailableDataRequest",  null),
                                                                            new MessageTypeInfo((MessageTypes)( -11003 ), new bool?(), "ProductFeedback",  null),
                                                                            new MessageTypeInfo((MessageTypes)( -11006 ), new bool?(), "ProductPermission",  null)
                                                                        };

        private IEnumerable<MessageTypes> _supportedResultMessages = new MessageTypes[10]
                                                                        {
                                                                            MessageTypes.MarketData,
                                                                            MessageTypes.SecurityLookup,
                                                                            MessageTypes.Portfolio,
                                                                            MessageTypes.PortfolioLookup,
                                                                            MessageTypes.OrderStatus,
                                                                            MessageTypes.TimeFrameLookup,
                                                                            MessageTypes.UserLookup,
                                                                            MessageTypes.TimeFrameLookup,
                                                                            ~MessageTypes.PortfolioLookup,
                                                                            ~MessageTypes.OrderStatus
                                                                        };

        private IEnumerable<MessageTypes> _supportedOutMessages = new MessageTypes[3]
                                                                        {
                                                                            MessageTypes.SubscriptionResponse,
                                                                            MessageTypes.SubscriptionOnline,
                                                                            MessageTypes.SubscriptionFinished
                                                                        };

        private bool _convertToLatin;

        private readonly CachedSynchronizedSet<TimeSpan> _timeSpanCache = new CachedSynchronizedSet<TimeSpan>();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Fix.Dialects.DefaultFixDialect" />.
        /// </summary>
        /// <param name="transactionIdGenerator">Transaction id generator.</param>
        public DefaultFixDialect(IdGenerator transactionIdGenerator)
          : base(transactionIdGenerator, Encoding.UTF8, "FIX.4.4")
        {
        }

        /// <inheritdoc />
        public override IEnumerable<MessageTypeInfo> PossibleSupportedMessages => _supportedMsgs;

        /// <inheritdoc />
        public override IEnumerable<MessageTypes> SupportedResultMessages
        {
            get => _supportedResultMessages;
            set => _supportedResultMessages = value;
        }

        /// <inheritdoc />
        public override IEnumerable<MessageTypes> SupportedOutMessages
        {
            get => _supportedOutMessages;
            set => _supportedOutMessages = value;
        }

        /// <summary>Convert all non-latin text messages to latin.</summary>
        public bool ConvertToLatin
        {
            get => _convertToLatin;
            set => _convertToLatin = value;
        }

        private string ToLatin(string _param1) => !ConvertToLatin ? _param1 : _param1.ToLatin();

        /// <inheritdoc />
        public override bool IsSupportCandlesUpdates => true;

        /// <inheritdoc />
        protected override IEnumerable<TimeSpan> TimeFrames => _timeSpanCache.Cache;

        /// <inheritdoc />
        public override bool CheckTimeFrameByRequest => _timeSpanCache.Count == 0;

        /// <inheritdoc />
        protected override bool IsSupportMarketDataResponse => true;

        /// <inheritdoc />
        public override bool IsAutoReplyOnTransactonalUnsubscription => false;

        /// <inheritdoc />
        public override TimeSpan GetHistoryStepSize(
          DataType dataType,
          out TimeSpan iterationInterval)
        {
            TimeSpan timeSpan = base.GetHistoryStepSize(dataType, out TimeSpan _);
            if (timeSpan == TimeSpan.Zero)
                timeSpan = TimeSpan.FromDays(1.0);
            iterationInterval = TimeSpan.Zero;
            return timeSpan;
        }

        private sealed class TimeFramesInfoReader
        {
            public string _transId;
            public IFixReader _iFixReader;
            public string _timeFrameString;

            internal bool ProcessFixTags(FixTags tag)
            {
                if (tag != FixTags.MDReqID)
                {
                    if (tag != FixTags.TimeFrames)
                        return false;
                    _timeFrameString = _iFixReader.ReadString();
                    return true;
                }
                _transId = _iFixReader.ReadString();
                return true;
            }
        }

        private bool? ProcessTimeFramesInfo(IFixReader fixReader, Action<Message> handler)
        {
            TimeFramesInfoReader reader = new TimeFramesInfoReader();
            reader._iFixReader = fixReader;
            reader._transId = null;
            reader._timeFrameString = null;
            if (!reader._iFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            TimeFrameInfoMessage msg = new TimeFrameInfoMessage();
            msg.OriginalTransactionId = reader._transId.To<long?>().GetValueOrDefault();
            string mystring = reader._timeFrameString;
            msg.TimeFrames = (mystring != null ? mystring.SplitByComma().Select(x => x.To<long>().To<TimeSpan>()).ToArray() : null) ?? ArrayHelper.Empty<TimeSpan>();
            TimeFrameInfoMessage frameInfoMessage2 = msg;
            _timeSpanCache.TryAdd(frameInfoMessage2.TimeFrames);
            handler(frameInfoMessage2);
            return new bool?(true);
        }

        /// <inheritdoc />
        protected override bool? OnRead(IFixReader reader, string msgType, Action<Message> messageHandler)
        {
            if (msgType == FixExtendedMessages.TimeFramesInfo)
                return ProcessTimeFramesInfo(reader, messageHandler);

            //if ( msgType == FixExtendedMessages.ProductInfo )
            //    return reader.ProcessProductInfo( messageHandler, TimeStampParser );

            //if ( msgType == FixExtendedMessages.ProductFeedback )
            //    return ProcessProductFeedback( reader, messageHandler );

            //if ( msgType == FixExtendedMessages.ProductCategory )
            //    return ProcessProductCategory( reader, messageHandler );

            if (msgType == FixExtendedMessages.SecurityMappingInfo)
                return DefaultFixDialect.ProcessSecurityMappingInfo(reader, messageHandler);

            if (msgType == FixMessages.ExecutionReport)
                return ProcessExecutionReport(reader, messageHandler);

            if (msgType == FixExtendedMessages.Board)
                return ProcessBoard(reader, messageHandler);

            if (msgType == FixExtendedMessages.AdapterResponse)
                return DefaultFixDialect.ProcessAdapterResponse(reader, messageHandler);

            if (msgType == FixExtendedMessages.SubscriptionResponse)
                return DefaultFixDialect.ProcessSubscriptionResponse(reader, messageHandler);

            if (msgType == FixExtendedMessages.SecurityRoute)
                return DefaultFixDialect.ProcessSecurityRoute(reader, messageHandler);

            if (msgType == FixExtendedMessages.SubscriptionFinished)
                return DefaultFixDialect.ProcessSubscriptionFinished(reader, messageHandler);

            if (msgType == FixExtendedMessages.UserInfo)
                return ProcessUserInfo(reader, messageHandler);

            if (msgType == FixExtendedMessages.PortfolioRoute)
                return DefaultFixDialect.ProcessPortfolioRoute(reader, messageHandler);

            //if ( msgType == FixExtendedMessages.LicenseInfo )
            //    return ProcessLicenseInfo( reader, messageHandler );

            if (msgType == FixExtendedMessages.StrategyInfo)
                return reader.ProcessStrategyInfo(TimeStampParser, messageHandler);

            if (msgType == FixExtendedMessages.SubscriptionOnline)
                return DefaultFixDialect.ProcessSubscriptionOnline(reader, messageHandler);

            if (msgType == FixExtendedMessages.StrategyState)
                return reader.ProcessStrategyState(messageHandler);

            //if ( msgType == FixExtendedMessages.LicenseFeature )
            //    return ProcessLicenseFeature( reader, messageHandler );

            if (msgType == FixExtendedMessages.StrategyType)
                return reader.ProcessStrategyType(messageHandler);

            if (msgType == FixExtendedMessages.AvailableDataInfo)
                return ProcessAvailableDataInfo(reader, messageHandler);

            if (msgType == FixExtendedMessages.Subscription)
                return ProcessSubscription(reader, messageHandler);

            if (msgType == FixExtendedMessages.SecurityLegsInfo)
                return DefaultFixDialect.ProcessSecurityLegsInfo(reader, messageHandler);

            //if ( msgType == FixExtendedMessages.RemoteFile )
            //    return ProcessRemoteFile( reader, messageHandler );

            //if ( msgType == FixExtendedMessages.FileInfo )
            //    return ProcessFileInfo( reader, messageHandler );

            //if ( msgType == FixExtendedMessages.ProductPermission )
            //    return ProcessProductPermission( reader, messageHandler );


            return base.OnRead(reader, msgType, messageHandler);
        }

        /// <inheritdoc />
        protected override string OnWrite(IFixWriter writer, Message message)
        {
            switch (message.Type)
            {
                //case ( MessageTypes ) ( -11007 ):
                //    return WriteProductPublishMessage( writer, ( ProductPublishMessage ) message );

                //case ( MessageTypes ) ( -11006 ):
                //    return WriteProductPermissionMessage( writer, ( ProductPermissionMessage ) message );

                //case ( MessageTypes ) ( -11003 ):
                //    return WriteProductFeedbackMessage( writer, ( ProductFeedbackMessage ) message );

                //case ( MessageTypes ) ( -11001 ):
                //    writer.WriteProductInfo( ( ProductInfoMessage ) message, TimeStampParser );
                //    return FixExtendedMessages.ProductInfo;

                //case ( MessageTypes ) ( -11000 ):
                //    writer.WriteFile( ( FileInfoMessage ) message, TimeStampParser );
                //    return FixExtendedMessages.FileInfo;

                //case ( MessageTypes ) ( -10001 ):
                //    return WriteLicenseRequestMessage( writer, ( LicenseRequestMessage ) message );

                case (MessageTypes)(-5002):
                    return WriteFixUserRequestMessage(writer, (FixUserRequestMessage)message);

                case ~MessageTypes.OrderStatus:
                    return WriteAvailableDataRequestMessage(writer, (AvailableDataRequestMessage)message);

                case ~MessageTypes.PortfolioLookup:
                    return WriteRemoteFileCommandMessage(writer, (RemoteFileCommandMessage)message);

                case ~MessageTypes.NativeSecurityId:
                    writer.WriteStrategyState((StrategyStateMessage)message);
                    return FixExtendedMessages.StrategyState;

                case ~MessageTypes.PortfolioChange:
                    writer.WriteStrategyInfo((StrategyInfoMessage)message, TimeStampParser);
                    return FixExtendedMessages.StrategyInfo;

                case ~MessageTypes.QuoteChange:
                    return DefaultFixDialect.WriteChangeTimeIntervalMessage(writer, (ChangeTimeIntervalMessage)message);

                case ~MessageTypes.OrderPairReplace:
                    return WriteEmulationStateMessage(writer, (EmulationStateMessage)message);

                case MessageTypes.Security:
                    return WriteSecurityList(writer, (SecurityMessage)message);

                case MessageTypes.OrderRegister:
                    return WriteOrderRegisterMessage(writer, (OrderRegisterMessage)message);

                case MessageTypes.OrderReplace:
                    return WriteOrderReplaceMessage(writer, (OrderReplaceMessage)message);

                case MessageTypes.OrderCancel:
                    return WriteOrderCancelMessage(writer, (OrderCancelMessage)message);

                case MessageTypes.OrderGroupCancel:
                    return WriteOrderGroupCancelMessage(writer, (OrderGroupCancelMessage)message);

                case MessageTypes.News:
                    return WriteNewsMessage(writer, (NewsMessage)message);

                case MessageTypes.Portfolio:
                    return DefaultFixDialect.WritePortfolioMessage(writer, (PortfolioMessage)message);

                case MessageTypes.MarketData:
                    MarketDataMessage mdMsg = (MarketDataMessage)message;
                    string requestId = mdMsg.TransactionId != 0L ? mdMsg.TransactionId.To<string>() : throw new InvalidOperationException(nameof(mdMsg.TransactionId));
                    string responseId = mdMsg.OriginalTransactionId == 0L ? null : mdMsg.OriginalTransactionId.To<string>();
                    writer.WriteMarketDataMessage(mdMsg, requestId, responseId, TimeStampParser, new Action<IFixWriter, MarketDataMessage>(DefaultFixDialect.WriteOrderSpecificMessage));
                    return FixMessages.MarketDataRequest;

                case MessageTypes.SecurityLookup:
                    return WriteSecurityLookupMessage(writer, (SecurityLookupMessage)message);

                case MessageTypes.PortfolioLookup:
                    return WritePortfolioLookup(writer, (PortfolioLookupMessage)message);

                case MessageTypes.OrderStatus:
                    OrderStatusMessage orderStatusMessage = (OrderStatusMessage)message;
                    return orderStatusMessage.IsSubscribe && (orderStatusMessage.OrderId.HasValue || orderStatusMessage.OriginalTransactionId != 0L) ? DefaultFixDialect.WriteOrderStatusMessageSubscribed(writer, orderStatusMessage) : DefaultFixDialect.WriteOrderStatusMessage(writer, orderStatusMessage);

                case MessageTypes.Remove:
                    return DefaultFixDialect.WriteRemoveMessage(writer, (RemoveMessage)message);
                case MessageTypes.UserInfo:
                    return WriteUserInfoMessage(writer, (UserInfoMessage)message);

                case MessageTypes.UserLookup:
                    return WriteUserLookupMessage(writer, (UserLookupMessage)message);

                case MessageTypes.BoardLookup:
                    return WriteBoardLookupMessage(writer, (BoardLookupMessage)message);

                case MessageTypes.UserRequest:
                    return DefaultFixDialect.WriteUserRequestMessage(writer, (UserRequestMessage)message);

                case MessageTypes.TimeFrameLookup:
                    writer.WriteSubscriptionRequest((ISubscriptionMessage)message, TimeStampParser);
                    return FixExtendedMessages.TimeFramesRequest;

                case MessageTypes.SecurityMappingRequest:
                    return DefaultFixDialect.WriteSecurityMappingRequestMessage(writer, (SecurityMappingRequestMessage)message);

                case MessageTypes.SecurityLegsRequest:
                    return DefaultFixDialect.WriteSecurityLegsRequestMessage(writer, (SecurityLegsRequestMessage)message);

                case MessageTypes.AdapterListRequest:
                    return WriteAdapterListRequestMessage(writer, (AdapterListRequestMessage)message);

                case MessageTypes.Command:
                    writer.WriteCommand((CommandMessage)message, TimeStampParser);
                    return FixExtendedMessages.Command;

                case MessageTypes.SubscriptionListRequest:
                    return WriteSubscriptionListRequestMessage(writer, (SubscriptionListRequestMessage)message);

                case MessageTypes.SecurityRouteListRequest:
                    return DefaultFixDialect.WriteSecurityRouteListRequestMessage(writer, (SecurityRouteListRequestMessage)message);
                case MessageTypes.PortfolioRouteListRequest:
                    return DefaultFixDialect.WritePortfolioRouteListRequestMessage(writer, (PortfolioRouteListRequestMessage)message);
                case MessageTypes.SecurityMapping:
                    return DefaultFixDialect.WriteSecurityMappingMessage(writer, (SecurityMappingMessage)message);
                default:
                    return base.OnWrite(writer, message);
            }
        }

        private string WriteFixUserRequestMessage(IFixWriter fixWriter, FixUserRequestMessage msg)
        {
            if (msg.TransactionId != 0L)
            {
                fixWriter.Write(FixTags.UserRequestID);
                fixWriter.Write(msg.TransactionId);
            }
            fixWriter.Write(FixTags.UserRequestType);
            fixWriter.Write((int)msg.RequestType);
            if (!msg.OldPassword.IsEmpty())
            {
                fixWriter.Write(FixTags.Password);
                fixWriter.Write(msg.OldPassword.UnSecure());
            }
            if (!msg.NewPassword.IsEmpty())
            {
                fixWriter.Write(FixTags.NewPassword);
                fixWriter.Write(msg.NewPassword.UnSecure());
            }
            if (!msg.UserName.IsEmpty())
            {
                fixWriter.Write(FixTags.Username);
                fixWriter.Write(msg.UserName);
            }
            return FixMessages.UserRequest;
        }

        private sealed class UserInfoReader
        {
            public string _transId;
            public IFixReader _iFixReader;
            public Action<Message> _handler;

            internal bool ProcessFixTags(FixTags tag)
            {
                if (tag != FixTags.UserRequestID)
                    return false;
                _transId = _iFixReader.ReadString();
                return true;
            }

            internal void HandleMessage(Message msg)
            {
                UserInfoMessage userInfoMessage = (UserInfoMessage)msg;
                userInfoMessage.OriginalTransactionId = _transId.To<long>();

                _handler(userInfoMessage);
            }
        }

        private bool? ProcessUserInfo(IFixReader fixReader, Action<Message> handler)
        {
            UserInfoReader reader = new UserInfoReader();
            reader._iFixReader = fixReader;
            reader._handler = handler;
            reader._transId = null;
            return reader._iFixReader.ReadUserInfoMessage(TimeStampParser, new Func<FixTags, bool>(reader.ProcessFixTags), new Action<Message>(reader.HandleMessage));
        }

        private string WriteUserInfoMessage(IFixWriter fixWriter, UserInfoMessage msg)
        {
            fixWriter.Write(FixTags.UserRequestID);
            fixWriter.Write(msg.TransactionId);
            fixWriter.WriteUserInfoMessage(msg, TimeStampParser);
            return FixExtendedMessages.UserInfo;
        }

        private string WriteUserLookupMessage(IFixWriter fixWriter, UserLookupMessage msg)
        {
            fixWriter.WriteSubscriptionRequest(msg, TimeStampParser, FixTags.UserRequestID);
            if (msg.IsSubscribe)
            {
                if (!msg.Like.IsEmpty())
                {
                    fixWriter.Write(FixTags.Username);
                    fixWriter.Write(msg.Like);
                }
                if (msg.UserId.HasValue)
                {
                    fixWriter.Write(FixTags.Id);
                    fixWriter.Write(msg.UserId.Value);
                }
                if (msg.Own)
                {
                    fixWriter.Write(FixTags.Owner);
                    fixWriter.Write(msg.Own);
                }
            }
            fixWriter.Write(FixTags.UserRequestType);
            fixWriter.Write(0);
            return FixMessages.UserRequest;
        }

        private static string WriteUserRequestMessage(IFixWriter fixWriter, UserRequestMessage msg)
        {
            fixWriter.Write(FixTags.UserRequestID);
            fixWriter.Write(msg.TransactionId);

            if (!msg.Login.IsEmpty())
            {
                fixWriter.Write(FixTags.Username);
                fixWriter.Write(msg.Login);
            }

            long? id = msg.Id;
            if (id.HasValue)
            {
                fixWriter.Write(FixTags.Id);
                id = msg.Id;
                long num = id.Value;
                fixWriter.Write(num);
            }

            fixWriter.Write(FixTags.SubscriptionRequestType);
            fixWriter.Write(msg.GetSubscriptionType());
            return FixExtendedMessages.UserRequest;
        }

        private static string WriteChangeTimeIntervalMessage(IFixWriter writer, ChangeTimeIntervalMessage msg)
        {
            writer.Write(FixTags.HeartBtInt);
            writer.Write((int)msg.Interval.TotalSeconds);

            return FixExtendedMessages.HistoryInterval;
        }

        private string WriteEmulationStateMessage(IFixWriter writer, EmulationStateMessage msg)
        {
            if (msg.State == ChannelStates.Starting)
            {
                writer.Write(FixTags.StartDate);
                writer.WriteUtc(msg.StartDate, TimeStampParser);
                writer.Write(FixTags.EndDate);
                writer.WriteUtc(msg.StopDate, TimeStampParser);
            }
            return msg.State != ChannelStates.Starting ? FixExtendedMessages.HistoryEnd : FixExtendedMessages.HistoryStart;
        }

        private static void WriteOrderSpecificMessage(IFixWriter writer, SecurityMessage msg)
        {
            if (msg.SecurityId != new SecurityId())
            {
                writer.Write(FixTags.Symbol);
                writer.Write(msg.SecurityId.SecurityCode);
                writer.Write(FixTags.SecurityExchange);
                writer.Write(msg.SecurityId.BoardCode);
            }
            if (!msg.CfiCode.IsEmpty())
            {
                writer.Write(FixTags.CFICode);
                writer.Write(msg.CfiCode);
            }
            SecurityTypes? securityType = msg.SecurityType;
            if (!securityType.HasValue)
                return;
            writer.Write(FixTags.SecurityType);
            IFixWriter fixWriter = writer;
            securityType = msg.SecurityType;
            string fix = securityType.Value.ToFix();
            fixWriter.Write(fix);
        }

        //private string WriteLicenseRequestMessage( IFixWriter writer, LicenseRequestMessage msg )
        //{
        //    writer.WriteSubscriptionRequest( msg, TimeStampParser );
        //    if ( msg.BrokerId != 0L )
        //    {
        //        writer.Write( FixTags.ExecBroker );
        //        writer.Write( msg.BrokerId );
        //    }
        //    if ( !msg.Account.IsEmpty( ) )
        //    {
        //        writer.Write( FixTags.Account );
        //        writer.Write( msg.Account );
        //    }
        //    if ( msg.LicenseId != 0L )
        //    {
        //        writer.Write( FixTags.ClOrdID );
        //        writer.Write( msg.LicenseId );
        //    }
        //    if ( !msg.HardwareId.IsEmpty( ) )
        //    {
        //        writer.Write( FixTags.HardwareId );
        //        writer.Write( msg.HardwareId );
        //    }
        //    if ( msg.Command.HasValue )
        //    {
        //        writer.Write( FixTags.Command );
        //        writer.Write( ( int ) msg.Command.Value );
        //    }
        //    if ( !msg.Features.IsEmpty( ) )
        //    {
        //        writer.Write( FixTags.Text );
        //        writer.Write( msg.Features );
        //    }
        //    if ( msg.IssuedTo != 0L )
        //    {
        //        writer.Write( FixTags.Username );
        //        writer.Write( msg.IssuedTo );
        //    }
        //    if ( msg.ExpirationDate.HasValue )
        //    {
        //        writer.Write( FixTags.ExpireDate );
        //        writer.WriteUtc( msg.ExpirationDate.Value, DateParser );
        //    }
        //    return FixExtendedMessages.LicenseRequest;
        //}

        //private sealed class LicenseInfoReader
        //{
        //    public LicenseInfoMessage _licenseInfoMsg;
        //    public IFixReader _iFixReader;

        //    internal bool ProcessFixTags( FixTags tag )
        //    {
        //        switch ( tag )
        //        {
        //            case FixTags.RawDataLength:
        //                _licenseInfoMsg.Body = new byte[ _iFixReader.ReadInt( ) ];
        //                return true;
        //            case FixTags.RawData:
        //                _iFixReader.ReadBytes( _licenseInfoMsg.Body, 0, _licenseInfoMsg.Body.Length );
        //                return true;
        //            case FixTags.MDReqID:
        //                _licenseInfoMsg.OriginalTransactionId = _iFixReader.ReadLong( );
        //                return true;
        //            case FixTags.SecurityStatus:
        //                _licenseInfoMsg.IsApproved = true;
        //                return true;
        //            default:
        //                return false;
        //        }
        //    }
        //}

        //private bool? ProcessLicenseInfo( IFixReader fixReader, Action<Message> handler )
        //{
        //    LicenseInfoReader reader = new LicenseInfoReader();
        //    reader._iFixReader = fixReader;
        //    reader._licenseInfoMsg = new LicenseInfoMessage( );
        //    if ( !reader._iFixReader.ReadMessage( new Func<FixTags, bool>( reader.ProcessFixTags ) ) )
        //        return new bool?( );
        //    handler( reader._licenseInfoMsg );
        //    return new bool?( true );
        //}

        //private sealed class LicenseFeaturesReader
        //{
        //    public LicenseFeatureMessage _licenseInfoMsg;
        //    public IFixReader _iFixReader;

        //    internal bool ProcessFixTags( FixTags tag )
        //    {
        //        switch ( tag )
        //        {
        //            case FixTags.MDReqID:
        //                _licenseInfoMsg.OriginalTransactionId = _iFixReader.ReadLong( );
        //                return true;
        //            case FixTags.Name:
        //                _licenseInfoMsg.Name = _iFixReader.ReadString( );
        //                return true;
        //            case FixTags.Id:
        //                _licenseInfoMsg.Id = _iFixReader.ReadLong( );
        //                return true;
        //            default:
        //                return false;
        //        }
        //    }
        //}

        //private bool? ProcessLicenseFeature( IFixReader fixReader, Action<Message> handler )
        //{
        //    LicenseFeaturesReader reader = new LicenseFeaturesReader();
        //    reader._iFixReader = fixReader;
        //    reader._licenseInfoMsg = new LicenseFeatureMessage( );
        //    if ( !reader._iFixReader.ReadMessage( new Func<FixTags, bool>( reader.ProcessFixTags ) ) )
        //        return new bool?( );
        //    handler( reader._licenseInfoMsg );
        //    return new bool?( true );
        //}

        /// <summary>Register new candle type.</summary>
        /// <param name="code">
        /// <see cref="F:StockSharp.Fix.Native.FixTags.MDEntryType" /> value.</param>
        /// <param name="messageType">Message type.</param>
        public static void RegisterCandleType(char code, Type messageType) => StockSharp.Fix.Native.Extensions.RegisterCandleType(code, messageType);


        private sealed class SubscriptionResponseReader
        {
            public string _originalTransactionId;
            public IFixReader _iFixReader;
            public string _text;

            internal bool ProcessFixTags(FixTags tag)
            {
                if (tag != FixTags.Text)
                {
                    if (tag != FixTags.MDReqID)
                        return false;
                    _originalTransactionId = _iFixReader.ReadString();
                    return true;
                }
                _text = _iFixReader.ReadString();
                return true;
            }
        }

        private static bool? ProcessSubscriptionResponse(IFixReader _param0, Action<Message> handler)
        {
            SubscriptionResponseReader reader = new SubscriptionResponseReader();
            reader._iFixReader = _param0;
            reader._originalTransactionId = null;
            reader._text = null;
            if (!reader._iFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();

            handler(new SubscriptionResponseMessage()
            {
                OriginalTransactionId = reader._originalTransactionId.To<long>(),
                Error = (reader._text.IsEmpty() ? null : (Exception)new InvalidOperationException(reader._text))
            });
            return new bool?(true);
        }

        private sealed class SubscriptionFinishedReader
        {
            public string _originalTransactionId;
            public IFixReader _iFixReader;

            internal bool ProcessFixTags(FixTags tag)
            {
                if (tag != FixTags.MDReqID)
                    return false;
                _originalTransactionId = _iFixReader.ReadString();
                return true;
            }
        }

        private static bool? ProcessSubscriptionFinished(IFixReader _param0, Action<Message> handler)
        {
            SubscriptionFinishedReader reader = new SubscriptionFinishedReader();
            reader._iFixReader = _param0;
            reader._originalTransactionId = null;
            if (!reader._iFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            Action<Message> action = handler;
            SubscriptionFinishedMessage subscriptionFinishedMessage = new SubscriptionFinishedMessage();
            subscriptionFinishedMessage.OriginalTransactionId = reader._originalTransactionId.To<long>();
            action(subscriptionFinishedMessage);
            return new bool?(true);
        }


        private sealed class SubscriptionOnlineReader
        {
            public string _originalTransactionId;
            public IFixReader _iFixReader;

            internal bool ProcessFixTags(FixTags tag)
            {
                if (tag != FixTags.MDReqID)
                    return false;
                _originalTransactionId = _iFixReader.ReadString();
                return true;
            }
        }

        private static bool? ProcessSubscriptionOnline(IFixReader fixReader, Action<Message> handler)
        {
            SubscriptionOnlineReader reader = new SubscriptionOnlineReader();
            reader._iFixReader = fixReader;
            reader._originalTransactionId = null;
            if (!reader._iFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            handler(new SubscriptionOnlineMessage()
            {
                OriginalTransactionId = reader._originalTransactionId.To<long>()
            });
            return new bool?(true);
        }

        private string WriteSecurityLookupMessage(IFixWriter writer, SecurityLookupMessage msg)
        {
            writer.Write(FixTags.SecurityReqID);
            writer.Write(msg.TransactionId);
            if (msg.OriginalTransactionId != 0L)
            {
                writer.Write(FixTags.SecurityResponseID);
                writer.Write(msg.OriginalTransactionId);
            }
            writer.Write(FixTags.SecurityListRequestType);
            writer.Write(4);
            SecurityId securityId1 = msg.SecurityId;
            if (!securityId1.SecurityCode.IsEmpty())
            {
                writer.Write(FixTags.Symbol);
                IFixWriter fixWriter = writer;
                securityId1 = msg.SecurityId;
                string securityCode = securityId1.SecurityCode;
                fixWriter.Write(securityCode);
            }
            securityId1 = msg.SecurityId;
            if (!securityId1.BoardCode.IsEmpty())
            {
                writer.Write(FixTags.SecurityExchange);
                IFixWriter fixWriter = writer;
                securityId1 = msg.SecurityId;
                string boardCode = securityId1.BoardCode;
                fixWriter.Write(boardCode);
            }
            HashSet<SecurityTypes> securityTypes = msg.GetSecurityTypes();
            if (securityTypes.Count > 0)
            {
                writer.Write(FixTags.NoSecurityTypes);
                writer.Write(securityTypes.Count);
                foreach (SecurityTypes type in securityTypes)
                {
                    writer.Write(FixTags.SecurityType);
                    writer.Write(type.ToFix());
                }
            }
            CurrencyTypes? currency = msg.Currency;
            if (currency.HasValue)
            {
                writer.Write(FixTags.Currency);
                IFixWriter fixWriter = writer;
                currency = msg.Currency;
                string fix = currency.Value.ToFix();
                fixWriter.Write(fix);
            }
            OptionTypes? optionType = msg.OptionType;
            if (optionType.HasValue)
            {
                writer.Write(FixTags.PutOrCall);
                IFixWriter fixWriter = writer;
                optionType = msg.OptionType;
                int fix = optionType.Value.ToFix();
                fixWriter.Write(fix);
            }
            decimal? strike = msg.Strike;
            if (strike.HasValue)
            {
                writer.Write(FixTags.StrikePrice);
                IFixWriter fixWriter = writer;
                strike = msg.Strike;
                decimal num = strike.Value;
                fixWriter.Write(num);
            }
            DateTimeOffset? expiryDate = msg.ExpiryDate;
            if (expiryDate.HasValue)
            {
                writer.Write(FixTags.RedemptionDate);

                expiryDate = msg.ExpiryDate;
                DateTimeOffset dto = expiryDate.Value;
                FastDateTimeParser timeStampParser = TimeStampParser;
                writer.WriteUtc(dto, timeStampParser);
            }
            if (!msg.Name.IsEmpty())
            {
                writer.Write(FixTags.SecurityDesc);
                writer.Write(msg.Name);
            }
            if (!msg.UnderlyingSecurityCode.IsEmpty())
            {
                writer.Write(FixTags.Text);
                writer.Write(msg.UnderlyingSecurityCode);
            }
            long? nullable = msg.Skip;
            if (nullable.HasValue)
            {
                writer.Write(FixTags.MarketDataSkip);
                IFixWriter fixWriter = writer;
                nullable = msg.Skip;
                long num = nullable.Value;
                fixWriter.Write(num);
            }
            nullable = msg.Count;
            if (nullable.HasValue)
            {
                writer.Write(FixTags.MarketDataCount);
                IFixWriter fixWriter = writer;
                nullable = msg.Count;
                long num = nullable.Value;
                fixWriter.Write(num);
            }
            if (msg.OnlySecurityId)
            {
                writer.Write(FixTags.OnlyId);
                writer.Write(true);
            }
            if (msg.SecurityIds.Length != 0)
            {
                writer.Write(FixTags.NoUnderlyings);
                writer.Write(msg.SecurityIds.Length);
                foreach (SecurityId securityId2 in msg.SecurityIds)
                {
                    writer.Write(FixTags.UnderlyingSymbol);
                    writer.Write(securityId2.SecurityCode);
                    writer.Write(FixTags.UnderlyingSecurityExchange);
                    writer.Write(securityId2.BoardCode);
                }
            }
            if (!msg.BoardCode.IsEmpty())
            {
                writer.Write(FixTags.ExDestination);
                writer.Write(msg.BoardCode);
            }
            return FixMessages.SecurityListRequest;
        }

        private string WriteSecurityList(IFixWriter writer, SecurityMessage msg)
        {
            writer.WriteSecurityList(TimeStampParser, false, null, null, new SecurityMessage[1] { msg }, true);

            return FixExtendedMessages.SecurityListUpload;
        }

        private static string IfixWriteMarketDataMessage(IFixWriter writer, MarketDataMessage msg)
        {
            writer.Write(FixTags.TradSesReqID);
            writer.Write(msg.TransactionId.To<long>());
            if (msg.OriginalTransactionId != 0L)
            {
                writer.Write(FixTags.MDResponseID);
                writer.Write(msg.OriginalTransactionId.To<long>());
            }
            writer.Write(FixTags.TradingSessionID);
            writer.Write(msg.BoardCode);
            writer.Write(FixTags.SubscriptionRequestType);
            writer.Write(msg.GetSubscriptionType());
            //return nameof( -1557771508 );

            return FixMessages.MarketDataIncrementalRefresh;
        }

        private string WriteBoardLookupMessage(IFixWriter writer, BoardLookupMessage msg)
        {
            writer.WriteSubscriptionRequest(msg, TimeStampParser);
            if (!msg.Like.IsEmpty())
            {
                writer.Write(FixTags.TradingSessionID);
                writer.Write(msg.Like);
            }
            return FixExtendedMessages.BoardLookup;
        }

        private sealed class BoardReader
        {
            public string _transId;
            public IFixReader _iFixReader;
            public string _tradingSessionID;
            public string _tradingSessionSubID;
            public TimeSpan? _expireTime;
            public DefaultFixDialect _fixDialect;
            public string _sessionPeriods;
            public string _sessionSpecialDays;

            internal bool ProcessFixTags(FixTags tag)
            {
                switch (tag)
                {
                    case FixTags.ExpireTime:
                        // ISSUE: explicit non-virtual call
                        _expireTime = new TimeSpan?(_iFixReader.ReadTimeSpan(_fixDialect.TimeParser));
                        return true;
                    case FixTags.TradSesReqID:
                        _transId = _iFixReader.ReadString();
                        return true;
                    case FixTags.TradingSessionID:
                        _tradingSessionID = _iFixReader.ReadString();
                        return true;
                    case FixTags.TradingSessionSubID:
                        _tradingSessionSubID = _iFixReader.ReadString();
                        return true;
                    case FixTags.SessionPeriods:
                        _sessionPeriods = _iFixReader.ReadString();
                        return true;
                    case FixTags.SessionSpecialDays:
                        _sessionSpecialDays = _iFixReader.ReadString();
                        return true;
                    default:
                        return false;
                }
            }
        }

        private bool? ProcessBoard(IFixReader fixReader, Action<Message> handler)
        {
            BoardReader reader = new BoardReader();
            reader._iFixReader = fixReader;
            reader._fixDialect = this;
            reader._transId = null;
            reader._tradingSessionID = null;
            reader._tradingSessionSubID = null;
            reader._expireTime = new TimeSpan?();
            reader._sessionPeriods = null;
            reader._sessionSpecialDays = null;
            if (!reader._iFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            if (reader._tradingSessionID.IsEmpty())
            {
                this.AddWarningLog("Missing Trading Session ID", reader._tradingSessionSubID);
                return new bool?(true);
            }
            BoardMessage boardMessage1 = new BoardMessage();
            boardMessage1.OriginalTransactionId = reader._transId.To<long?>().GetValueOrDefault();
            boardMessage1.Code = reader._tradingSessionID;
            boardMessage1.ExchangeCode = reader._tradingSessionSubID;
            boardMessage1.ExpiryTime = reader._expireTime.GetValueOrDefault();
            BoardMessage boardMessage2 = boardMessage1;
            if (!reader._sessionPeriods.IsEmpty())
                boardMessage2.WorkingTime.Periods.AddRange(reader._sessionPeriods.DecodeToPeriods());
            if (!reader._sessionSpecialDays.IsEmpty())
                boardMessage2.WorkingTime.SpecialDays.AddRange(reader._sessionSpecialDays.DecodeToSpecialDays());
            handler(boardMessage2);
            return new bool?(true);
        }

        private sealed class SubscriptionFinishedMessageReader
        {
            public string _transId;
            public IFixReader _iFixReader;

            internal bool ProcessFixTags(FixTags tag)
            {
                if (tag != FixTags.TradSesReqID)
                    return false;
                _transId = _iFixReader.ReadString();
                return true;
            }
        }

        private static bool? ProcessSubscriptionFinishedMessage(IFixReader fixReader, Action<Message> handler)
        {
            SubscriptionFinishedMessageReader reader = new SubscriptionFinishedMessageReader();
            reader._iFixReader = fixReader;
            reader._transId = null;
            if (!reader._iFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            Action<Message> action = handler;
            SubscriptionFinishedMessage subscriptionFinishedMessage = new SubscriptionFinishedMessage();
            subscriptionFinishedMessage.OriginalTransactionId = reader._transId.To<long?>().GetValueOrDefault();
            action(subscriptionFinishedMessage);
            return new bool?(true);
        }




        //private new sealed class LinqContainer
        //{
        //    public static readonly LinqContainer _container = new LinqContainer();
        //    public static Func<string, TimeSpan> _func1;
        //    public static Func<OrderStates, string> _func2;

        //    internal TimeSpan GetTimeSpan( string timeframe ) => timeframe.To<long>( ).To<TimeSpan>( );

        //    internal string OrderStatusLinq( OrderStates states ) => states.To<string>( );
        //}

        private static string WriteSecurityMappingRequestMessage(IFixWriter writer, SecurityMappingRequestMessage msg)
        {
            writer.Write(FixTags.MassStatusReqID);
            writer.Write(msg.TransactionId);

            return FixExtendedMessages.SecurityMappingRequest;
        }

        private sealed class SecurityMappingInfoReader
        {
            public string _transId;
            public IFixReader _iFixReader;
            public string _idSource;
            public Dictionary<string, IEnumerable<SecurityIdMapping>> _securitiesMap;
            public SecurityIdMapping[] _securityIdArray;
            public int _idCount;

            internal bool ProcessFixTags(FixTags tag)
            {
                switch (tag)
                {
                    case FixTags.IDSource:
                        if (_idSource != null)
                        {
                            _securitiesMap.Add(_idSource, _securityIdArray);
                            _securityIdArray = null;
                            _idCount = -1;
                        }
                        _idSource = _iFixReader.ReadString();
                        return true;
                    case FixTags.Symbol:
                        if (!_securityIdArray[_idCount].StockSharpId.SecurityCode.IsEmpty())
                            ++_idCount;
                        SecurityId stockSharpId1 = _securityIdArray[_idCount].StockSharpId;
                        stockSharpId1.SecurityCode = _iFixReader.ReadString();
                        _securityIdArray[_idCount].StockSharpId = stockSharpId1;
                        return true;
                    case FixTags.NoRelatedSym:
                        _iFixReader.ReadInt();
                        return true;
                    case FixTags.SecurityExchange:
                        if (!_securityIdArray[_idCount].StockSharpId.BoardCode.IsEmpty())
                            ++_idCount;
                        SecurityId stockSharpId2 = _securityIdArray[_idCount].StockSharpId;
                        stockSharpId2.BoardCode = _iFixReader.ReadString();
                        _securityIdArray[_idCount].StockSharpId = stockSharpId2;
                        return true;
                    case FixTags.NoSecurityAltID:
                        _securityIdArray = new SecurityIdMapping[_iFixReader.ReadInt()];
                        for (int index = 0; index < _securityIdArray.Length; ++index)
                            _securityIdArray[index] = new SecurityIdMapping();
                        _idCount = 0;
                        return true;
                    case FixTags.SecurityAltID:
                        if (!_securityIdArray[_idCount].AdapterId.SecurityCode.IsEmpty())
                            ++_idCount;
                        SecurityId adapterId1 = _securityIdArray[_idCount].AdapterId;
                        adapterId1.SecurityCode = _iFixReader.ReadString();
                        _securityIdArray[_idCount].AdapterId = adapterId1;
                        return true;
                    case FixTags.SecurityAltIDSource:
                        if (!_securityIdArray[_idCount].AdapterId.BoardCode.IsEmpty())
                            ++_idCount;
                        SecurityId adapterId2 = _securityIdArray[_idCount].AdapterId;
                        adapterId2.BoardCode = _iFixReader.ReadString();
                        _securityIdArray[_idCount].AdapterId = adapterId2;
                        return true;
                    case FixTags.MassStatusReqID:
                        _transId = _iFixReader.ReadString();
                        return true;
                    default:
                        return false;
                }
            }
        }

        private static bool? ProcessSecurityMappingInfo(IFixReader fixReader, Action<Message> handler)
        {
            SecurityMappingInfoReader reader = new SecurityMappingInfoReader();
            reader._iFixReader = fixReader;
            reader._transId = null;
            reader._idSource = null;
            reader._securitiesMap = new Dictionary<string, IEnumerable<SecurityIdMapping>>(StringComparer.InvariantCultureIgnoreCase);
            reader._securityIdArray = null;
            reader._idCount = -1;
            if (!reader._iFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            if (reader._idSource != null)
                reader._securitiesMap.Add(reader._idSource, reader._securityIdArray);
            Action<Message> action = handler;
            SecurityMappingInfoMessage mappingInfoMessage = new SecurityMappingInfoMessage();
            mappingInfoMessage.OriginalTransactionId = reader._transId.To<long?>().GetValueOrDefault();
            mappingInfoMessage.Mapping = reader._securitiesMap;
            action(mappingInfoMessage);
            return new bool?(true);
        }

        private static string WriteSecurityLegsRequestMessage(IFixWriter writer, SecurityLegsRequestMessage msg)
        {
            writer.Write(FixTags.MassStatusReqID);
            writer.Write(msg.TransactionId);

            if (!msg.Like.IsEmpty())
            {
                writer.Write(FixTags.Symbol);
                writer.Write(msg.Like);
            }

            return FixExtendedMessages.SecurityLegsRequest;
        }

        private sealed class SecurityLegsInfoReader
        {
            public Dictionary<SecurityId, IEnumerable<SecurityId>> _securityIdsMap;
            public string _securityCode;
            public string _boardCode;
            public SecurityId[] _securityIdsArray;
            public int _idCount;
            public string _transId;
            public IFixReader _iFixReader;

            internal void NewSecurity()
            {
                _securityIdsMap.Add(new SecurityId()
                {
                    SecurityCode = _securityCode,
                    BoardCode = _boardCode
                }, _securityIdsArray);
                _securityCode = null;
                _boardCode = null;
                _securityIdsArray = null;
                _idCount = -1;
            }

            internal bool ProcessFixTags(FixTags tag)
            {
                switch (tag)
                {
                    case FixTags.Symbol:
                        if (!_securityCode.IsEmpty())
                            NewSecurity();
                        _securityCode = _iFixReader.ReadString();
                        return true;
                    case FixTags.NoRelatedSym:
                        _iFixReader.ReadInt();
                        return true;
                    case FixTags.SecurityExchange:
                        if (!_boardCode.IsEmpty())
                            NewSecurity();
                        _boardCode = _iFixReader.ReadString();
                        return true;
                    case FixTags.NoLegs:
                        _securityIdsArray = new SecurityId[_iFixReader.ReadInt()];
                        _idCount = 0;
                        return true;
                    case FixTags.MassStatusReqID:
                        _transId = _iFixReader.ReadString();
                        return true;
                    case FixTags.LegSymbol:
                        if (!_securityIdsArray[_idCount].SecurityCode.IsEmpty())
                            ++_idCount;
                        SecurityId securityId1 = _securityIdsArray[_idCount];
                        securityId1.SecurityCode = _iFixReader.ReadString();
                        _securityIdsArray[_idCount] = securityId1;
                        return true;
                    case FixTags.LegSecurityExchange:
                        if (!_securityIdsArray[_idCount].BoardCode.IsEmpty())
                            ++_idCount;
                        SecurityId securityId2 = _securityIdsArray[_idCount];
                        securityId2.BoardCode = _iFixReader.ReadString();
                        _securityIdsArray[_idCount] = securityId2;
                        return true;
                    default:
                        return false;
                }
            }
        }

        private static bool? ProcessSecurityLegsInfo(IFixReader fixReader, Action<Message> handler)
        {
            SecurityLegsInfoReader reader = new SecurityLegsInfoReader();
            reader._iFixReader = fixReader;
            reader._transId = null;
            reader._securityCode = null;
            reader._boardCode = null;
            reader._securityIdsMap = new Dictionary<SecurityId, IEnumerable<SecurityId>>();
            reader._securityIdsArray = null;
            reader._idCount = -1;
            if (!reader._iFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            if (!reader._securityCode.IsEmpty())
                reader.NewSecurity();
            Action<Message> action = handler;
            SecurityLegsInfoMessage securityLegsInfoMessage = new SecurityLegsInfoMessage();
            securityLegsInfoMessage.OriginalTransactionId = reader._transId.To<long?>().GetValueOrDefault();
            securityLegsInfoMessage.Legs = reader._securityIdsMap;
            action(securityLegsInfoMessage);
            return new bool?(true);
        }

        private string WriteAdapterListRequestMessage(IFixWriter writer, AdapterListRequestMessage msg)
        {
            writer.WriteSubscriptionRequest(msg, TimeStampParser);
            return FixExtendedMessages.AdapterListRequest;
        }

        private sealed class AdapterResponseReader
        {
            public string _transId;
            public IFixReader _iFixReader;
            public string _ids;
            public Dictionary<string, (string, string)> _noStrategyParameters;
            public string _strategyParameterName;
            public string _strategyParameterType;
            public string _strategyParameterValue;

            internal bool ProcessFixTags(FixTags tag)
            {
                switch (tag)
                {
                    case FixTags.MassStatusReqID:
                        _transId = _iFixReader.ReadString();
                        return true;
                    case FixTags.NoStrategyParameters:
                        _noStrategyParameters = new Dictionary<string, (string, string)>(_iFixReader.ReadInt());
                        return true;
                    case FixTags.StrategyParameterName:
                        _strategyParameterName = _iFixReader.ReadString();
                        return true;
                    case FixTags.StrategyParameterType:
                        _strategyParameterType = _iFixReader.ReadString();
                        return true;
                    case FixTags.StrategyParameterValue:
                        _strategyParameterValue = _iFixReader.ReadString();
                        _noStrategyParameters.Add(_strategyParameterName, (_strategyParameterType, _strategyParameterValue));
                        return true;
                    case FixTags.Id:
                        _ids = _iFixReader.ReadString();
                        return true;
                    default:
                        return false;
                }
            }
        }

        private static bool? ProcessAdapterResponse(IFixReader fixReader, Action<Message> handler)
        {
            AdapterResponseReader reader = new AdapterResponseReader();
            reader._iFixReader = fixReader;
            reader._transId = null;
            reader._noStrategyParameters = null;
            reader._strategyParameterName = null;
            reader._strategyParameterType = null;
            reader._strategyParameterValue = null;
            reader._ids = null;

            if (!reader._iFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            AdapterResponseMessage adapterResponseMessage1 = new AdapterResponseMessage();
            adapterResponseMessage1.AdapterId = reader._ids.IsEmpty() ? Guid.Empty : reader._ids.To<Guid>();
            adapterResponseMessage1.OriginalTransactionId = reader._transId.To<long?>().GetValueOrDefault();
            AdapterResponseMessage adapterResponseMessage2 = adapterResponseMessage1;
            adapterResponseMessage2.Parameters.AddRange(reader._noStrategyParameters);
            handler(adapterResponseMessage2);
            return new bool?(true);
        }

        private string WriteSubscriptionListRequestMessage(IFixWriter writer, SubscriptionListRequestMessage msg)
        {
            writer.WriteSubscriptionRequest(msg, TimeStampParser);

            return FixExtendedMessages.SubscriptionListRequest;
        }

        private bool? ProcessSubscription(IFixReader fixReader, Action<Message> handler)
        {
            List<MarketDataMessage> marketDataMessageList = new List<MarketDataMessage>();
            string mdReqId;
            string mdResponseId;
            if (!fixReader.ReadMarketDataMessages(TimeStampParser, new Action<MarketDataMessage>(marketDataMessageList.Add), out mdReqId, out mdResponseId).HasValue)
                return new bool?();
            long num1 = mdReqId.To<long>();
            long num2 = mdResponseId.To<long>();
            foreach (MarketDataMessage marketDataMessage in marketDataMessageList)
            {
                marketDataMessage.TransactionId = num1;
                marketDataMessage.OriginalTransactionId = num2;
                handler(marketDataMessage);
            }
            return new bool?(true);
        }

        private static string WriteSecurityMappingMessage(IFixWriter writer, SecurityMappingMessage msg)
        {
            writer.Write(FixTags.IDSource);
            writer.Write(msg.StorageName);
            SecurityIdMapping mapping = msg.Mapping;
            writer.Write(FixTags.MDUpdateAction);
            writer.Write(msg.IsDelete ? '2' : '1');
            writer.Write(FixTags.Symbol);
            writer.Write(mapping.StockSharpId.SecurityCode);
            writer.Write(FixTags.SecurityExchange);
            writer.Write(mapping.StockSharpId.BoardCode);
            writer.Write(FixTags.SecurityAltID);
            writer.Write(mapping.AdapterId.SecurityCode);
            writer.Write(FixTags.SecurityAltIDSource);
            writer.Write(mapping.AdapterId.BoardCode);

            return FixExtendedMessages.SecurityMapping;
        }

        private static string WriteSecurityRouteListRequestMessage(IFixWriter writer, SecurityRouteListRequestMessage msg)
        {
            writer.Write(FixTags.MassStatusReqID);
            writer.Write(msg.TransactionId);

            return FixExtendedMessages.SecurityRouteListRequest;
        }

        private sealed class SecurityRouteReader
        {
            public string _transId;
            public IFixReader _iFixReader;
            public string _ids;
            public string _securityCode;
            public string _securityExchange;
            public char? _mdEntryType;
            public string _mdEntryArg;

            internal bool ProcessFixTags(FixTags tag)
            {
                switch (tag)
                {
                    case FixTags.Symbol:
                        _securityCode = _iFixReader.ReadString();
                        return true;
                    case FixTags.SecurityExchange:
                        _securityExchange = _iFixReader.ReadString();
                        return true;
                    case FixTags.MDEntryType:
                        _mdEntryType = new char?(_iFixReader.ReadChar());
                        return true;
                    case FixTags.MassStatusReqID:
                        _transId = _iFixReader.ReadString();
                        return true;
                    case FixTags.MDEntryArg:
                        _mdEntryArg = _iFixReader.ReadString();
                        return true;
                    case FixTags.Id:
                        _ids = _iFixReader.ReadString();
                        return true;
                    default:
                        return false;
                }
            }
        }

        private static bool? ProcessSecurityRoute(IFixReader fixReader, Action<Message> handler)
        {
            SecurityRouteReader reader = new SecurityRouteReader();
            reader._iFixReader = fixReader;
            reader._transId = null;
            reader._securityCode = null;
            reader._securityExchange = null;
            reader._ids = null;
            reader._mdEntryType = new char?();
            reader._mdEntryArg = null;
            if (!reader._iFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            Action<Message> action = handler;
            SecurityRouteMessage securityRouteMessage = new SecurityRouteMessage();
            securityRouteMessage.OriginalTransactionId = reader._transId.To<long?>().GetValueOrDefault();
            securityRouteMessage.AdapterId = reader._ids.To<Guid>();
            securityRouteMessage.SecurityId = new SecurityId()
            {
                SecurityCode = reader._securityCode,
                BoardCode = reader._securityExchange
            };
            ref char? local = ref reader._mdEntryType;
            securityRouteMessage.SecurityDataType = local.HasValue ? local.GetValueOrDefault().ToDataType(reader._mdEntryArg) : null;
            action(securityRouteMessage);
            return new bool?(true);
        }

        private static string WriteRemoveMessage(IFixWriter writer, RemoveMessage msg)
        {
            writer.Write(FixTags.MDReqID);
            writer.Write(msg.TransactionId);
            writer.Write(FixTags.MDEntryId);
            writer.Write(msg.RemoveId);
            writer.Write(FixTags.MDEntryType);
            writer.Write(msg.RemoveType.To<string>());

            return FixExtendedMessages.Remove;
        }

        private sealed class RemoteFileCommandReader
        {
            public IFixWriter Writer;
            public DefaultFixDialect _fixDialect;

            internal void WriteRemoteFileCommandMessage(
              IFixWriter _param1,
              RemoteFileCommandMessage msg)
            {
                if (msg.SecurityId != new SecurityId())
                {
                    Writer.Write(FixTags.Symbol);
                    Writer.Write(msg.SecurityId.SecurityCode);
                    Writer.Write(FixTags.SecurityExchange);
                    Writer.Write(msg.SecurityId.BoardCode);
                }
                Writer.WriteDataType(msg.FileDataType);
                Writer.Write(FixTags.Format);
                Writer.Write((int)msg.Format);
                Writer.Write(FixTags.StartDate);
                // ISSUE: explicit non-virtual call
                Writer.WriteUtc(msg.StartDate, _fixDialect.DateParser);
                Writer.Write(FixTags.EndDate);
                // ISSUE: explicit non-virtual call
                Writer.WriteUtc(msg.EndDate, _fixDialect.DateParser);
                if (msg.Body.Length == 0)
                    return;
                Writer.Write(FixTags.RawDataLength);
                Writer.Write(msg.Body.Length);
                Writer.Write(FixTags.RawData);
                Writer.WriteBytes(msg.Body, 0, msg.Body.Length);
            }
        }

        private string WriteRemoteFileCommandMessage(IFixWriter wrtier, RemoteFileCommandMessage msg)
        {
            RemoteFileCommandReader reader = new RemoteFileCommandReader();
            reader.Writer = wrtier;
            reader._fixDialect = this;
            reader.Writer.WriteCommand(msg, TimeStampParser, new Action<IFixWriter, RemoteFileCommandMessage>(reader.WriteRemoteFileCommandMessage));

            return FixExtendedMessages.RemoteFileCommand;
        }

        //private bool? ProcessRemoteFile( IFixReader fixReader, Action<Message> handler )
        //{
        //    RemoteFileReader reader = new RemoteFileReader();
        //    reader._handler = handler;
        //    reader._iFixReader = fixReader;
        //    reader._fixDialect = this;
        //    reader._securityCode = null;
        //    reader._securityExchange = null;
        //    reader._mdEntryType = new char?( );
        //    reader._mdEntryArg = null;
        //    reader._format = new int?( );
        //    reader._date = new DateTimeOffset?( );
        //    return reader._iFixReader.ReadFileInfo( DateParser, new Action<RemoteFileMessage>( reader.RemoteFileHandler ), new Func<FixTags, IFixReader, bool>( reader.RemoteFileHandler2 ) );
        //}

        private sealed class AvailableDataInfoReader
        {
            public string _transId;
            public IFixReader _iFixReader;
            public string _securityCode;
            public string _securityExchange;
            public char? _mdEntryType;
            public string _mdEntryArg;
            public int? _format;
            public DateTimeOffset? _date;
            public DefaultFixDialect _fixDialect;

            internal bool ProcessFixTags(FixTags tag)
            {
                switch (tag)
                {
                    case FixTags.Symbol:
                        _securityCode = _iFixReader.ReadString();
                        return true;
                    case FixTags.SecurityExchange:
                        _securityExchange = _iFixReader.ReadString();
                        return true;
                    case FixTags.MDReqID:
                        _transId = _iFixReader.ReadString();
                        return true;
                    case FixTags.MDEntryType:
                        _mdEntryType = new char?(_iFixReader.ReadChar());
                        return true;
                    case FixTags.MDEntryDate:
                        // ISSUE: explicit non-virtual call
                        _date = new DateTimeOffset?(_iFixReader.ReadUtc(_fixDialect.DateParser));
                        return true;
                    case FixTags.MDEntryArg:
                        _mdEntryArg = _iFixReader.ReadString();
                        return true;
                    case FixTags.Format:
                        _format = new int?(_iFixReader.ReadInt());
                        return true;
                    default:
                        return false;
                }
            }
        }

        private bool? ProcessAvailableDataInfo(IFixReader fixReader, Action<Message> handler)
        {
            AvailableDataInfoReader reader = new AvailableDataInfoReader();
            reader._iFixReader = fixReader;
            reader._fixDialect = this;
            reader._transId = null;
            reader._securityCode = null;
            reader._securityExchange = null;
            reader._mdEntryType = new char?();
            reader._mdEntryArg = null;
            reader._date = new DateTimeOffset?();
            reader._format = new int?();
            if (!reader._iFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            Action<Message> action = handler;
            AvailableDataInfoMessage availableDataInfoMessage = new AvailableDataInfoMessage();
            availableDataInfoMessage.OriginalTransactionId = reader._transId.To<long?>().GetValueOrDefault();
            availableDataInfoMessage.SecurityId = new SecurityId()
            {
                SecurityCode = reader._securityCode,
                BoardCode = reader._securityExchange
            };
            ref char? local = ref reader._mdEntryType;
            availableDataInfoMessage.FileDataType = local.HasValue ? local.GetValueOrDefault().ToDataType(reader._mdEntryArg) : null;
            int? zFdySiro = reader._format;
            availableDataInfoMessage.Format = (zFdySiro.HasValue ? new StorageFormats?((StorageFormats)zFdySiro.GetValueOrDefault()) : new StorageFormats?()).GetValueOrDefault();
            availableDataInfoMessage.Date = reader._date.GetValueOrDefault();
            action(availableDataInfoMessage);
            return new bool?(true);
        }

        //private string WriteProductFeedbackMessage( IFixWriter writer, ProductFeedbackMessage msg )
        //{
        //    writer.WriteProductFeedback( msg, TimeStampParser );

        //    return FixExtendedMessages.ProductFeedback;
        //}

        //private bool? ProcessProductFeedback( IFixReader reader, Action<Message> handler ) => reader.ReadProductFeedback( handler, TimeStampParser );

        //private string WriteProductPermissionMessage( IFixWriter writer, ProductPermissionMessage msg )
        //{
        //    writer.WriteProductPermission( msg );

        //    return FixExtendedMessages.ProductPermission;
        //}

        //private bool? ProcessProductPermission( IFixReader reader, Action<Message> handler ) => reader.ReadProductPermission( handler );

        //private bool? ProcessProductCategory( IFixReader reader, Action<Message> handler ) => reader.ReadProductCategory( handler );

        //private string WriteProductPublishMessage( IFixWriter writer, ProductPublishMessage msg )
        //{
        //    if ( msg.Packages.Length == 0 )
        //        throw new ArgumentException( nameof( msg.Packages ) );
        //    writer.Write( FixTags.NoPartyIDs );
        //    writer.Write( msg.Packages.Length );
        //    foreach ( (long productId, string version, string releaseNotesEn, string releaseNotesRu) package in msg.Packages )
        //    {
        //        writer.Write( FixTags.Id );
        //        writer.Write( package.productId );
        //        writer.Write( FixTags.ApplVerId );
        //        writer.Write( package.version );
        //        writer.Write( FixTags.Text );
        //        writer.Write( package.releaseNotesEn );
        //        if ( !package.releaseNotesRu.IsEmpty( ) )
        //        {
        //            writer.Write( FixTags.Text2 );
        //            writer.Write( package.releaseNotesRu );
        //        }
        //    }
        //    return FixExtendedMessages.ProductPublish;
        //}

        private string WriteAvailableDataRequestMessage(IFixWriter writer, AvailableDataRequestMessage msg)
        {
            writer.Write(FixTags.MDReqID);
            writer.Write(msg.TransactionId);
            if (msg.SecurityId == new SecurityId())
            {
                writer.Write(FixTags.NoRelatedSym);
                writer.Write(0);
            }
            else
            {
                writer.Write(FixTags.NoRelatedSym);
                writer.Write(1);
                writer.Write(FixTags.Symbol);

                writer.Write(msg.SecurityId.SecurityCode);
                writer.Write(FixTags.SecurityExchange);
                writer.Write(msg.SecurityId.BoardCode);
            }

            if (msg.RequestDataType != null)
            {
                writer.WriteDataType(msg.RequestDataType);
            }

            StorageFormats? format = msg.Format;

            if (format.HasValue)
            {
                writer.Write(FixTags.Format);
                writer.Write((int)msg.Format.Value);
            }

            return FixExtendedMessages.AvailableDataRequest;
        }

        private string WriteNewsMessage(IFixWriter writer, NewsMessage msg)
        {
            if (msg.TransactionId > 0L)
            {
                writer.Write(FixTags.MDReqID);
                writer.Write(msg.TransactionId);
            }
            writer.WriteNews(msg, TimeStampParser);
            return FixMessages.News;
        }

        private void WriteOrderMessage(IFixWriter writer, OrderMessage msg)
        {
            WriteAccount(writer, msg);

            bool hasClientCode = !msg.ClientCode.IsEmpty();
            bool hasBrokerCode = !msg.BrokerCode.IsEmpty();

            int num = (hasClientCode ? 1 : 0) + (hasBrokerCode ? 1 : 0);
            if (num == 0)
                return;

            writer.Write(FixTags.NoPartyIDs);
            writer.Write(num);

            if (hasClientCode)
            {
                writer.Write(FixTags.PartyID);
                writer.Write(msg.ClientCode);
                writer.Write(FixTags.PartyIDSource);
                writer.Write('C');
                writer.Write(FixTags.PartyRole);
                writer.Write(3);
            }

            if (!hasBrokerCode)
                return;

            writer.Write(FixTags.PartyID);
            writer.Write(msg.BrokerCode);
            writer.Write(FixTags.PartyIDSource);
            writer.Write('C');
            writer.Write(FixTags.PartyRole);
            writer.Write(7);
        }

        private string WriteOrderRegisterMessage(IFixWriter writer, OrderRegisterMessage msg)
        {
            writer.Write(FixTags.ClOrdID);
            writer.Write(msg.TransactionId);
            SecurityId securityId = msg.SecurityId;
            DefaultFixDialect.WriteOrderSpecificMessage(writer, msg);
            writer.Write(FixTags.ExDestination);
            writer.Write(securityId.BoardCode);
            writer.WriteTransactTime(TimeStampParser);
            writer.WriteSide(msg.Side);
            writer.Write(FixTags.OrdType);
            writer.Write(msg.GetFixType());
            WriteOrderMessage(writer, msg);
            writer.Write(FixTags.OrderQty);
            writer.Write(msg.Volume);
            writer.WriteHandlInst(msg);
            char fixTimeInForce = msg.GetFixTimeInForce();
            string securityType = msg.ToSecurityType();
            if (securityType != null)
            {
                writer.Write(FixTags.SecurityType);
                writer.Write(securityType);
            }
            decimal? nullable1 = msg.VisibleVolume;
            if (nullable1.HasValue)
            {
                writer.Write(FixTags.MaxFloor);
                IFixWriter fixWriter = writer;
                nullable1 = msg.VisibleVolume;
                decimal num = nullable1.Value;
                fixWriter.Write(num);
            }
            if (msg.Condition != null)
                WriteOrderCondition(writer, msg.Condition);
            OrderTypes? orderType = msg.OrderType;
            OrderTypes orderTypes = OrderTypes.Market;
            if (!(orderType.GetValueOrDefault() == orderTypes & orderType.HasValue))
            {
                writer.Write(FixTags.Price);
                writer.Write(msg.Price);
            }
            writer.Write(FixTags.TimeInForce);
            writer.Write(fixTimeInForce);
            if (fixTimeInForce == '6')
            {
                DateTimeOffset? tillDate = msg.TillDate;
                if (tillDate.HasValue)
                {
                    tillDate = msg.TillDate;
                    DateTime utcDateTime = tillDate.Value.UtcDateTime;
                    if (utcDateTime.TimeOfDay.IsDefault())
                    {
                        writer.Write(FixTags.ExpireDate);
                        writer.Write(utcDateTime, DateParser);
                    }
                    else
                    {
                        writer.Write(FixTags.ExpireTime);
                        writer.Write(utcDateTime, TimeStampParser);
                    }
                }
            }
            if (!msg.Comment.IsEmpty())
            {
                writer.Write(FixTags.Text);
                writer.Write(ToLatin(msg.Comment));
            }
            bool? nullable2 = msg.IsMarketMaker;
            bool flag1 = true;
            if (nullable2.GetValueOrDefault() == flag1 & nullable2.HasValue)
                writer.WriteMarketMaker();
            nullable2 = msg.IsMargin;
            bool flag2 = true;
            if (nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue)
            {
                writer.Write(FixTags.CashMargin);
                writer.Write(1);
            }
            nullable1 = msg.Slippage;
            if (nullable1.HasValue)
            {
                writer.Write(FixTags.Slippage);
                IFixWriter fixWriter = writer;
                nullable1 = msg.Slippage;
                decimal num = nullable1.Value;
                fixWriter.Write(num);
            }
            nullable2 = msg.IsManual;
            if (nullable2.HasValue)
            {
                writer.Write(FixTags.ManualOrderIndicator);
                IFixWriter fixWriter = writer;
                nullable2 = msg.IsManual;
                int num = nullable2.Value ? 1 : 0;
                fixWriter.Write(num != 0);
            }
            writer.WritePositionEffect(msg.PositionEffect);
            nullable2 = msg.PostOnly;
            if (nullable2.HasValue)
            {
                writer.Write(FixTags.PostOnly);
                IFixWriter fixWriter = writer;
                nullable2 = msg.PostOnly;
                int num = nullable2.Value ? 1 : 0;
                fixWriter.Write(num != 0);
            }
            int? leverage = msg.Leverage;
            if (leverage.HasValue)
            {
                writer.Write(FixTags.Leverage);
                IFixWriter fixWriter = writer;
                leverage = msg.Leverage;
                int num = leverage.Value;
                fixWriter.Write(num);
            }
            return FixMessages.NewOrderSingle;
        }

        /// <summary>To record data by the order condition.</summary>
        /// <param name="writer">FIX data writer.</param>
        /// <param name="condition">Order condition (e.g., stop- and algo- orders parameters).</param>
        protected virtual void WriteOrderCondition(IFixWriter writer, OrderCondition condition)
        {
            FixOrderCondition fixOrderCondition = (FixOrderCondition)condition;
            if (fixOrderCondition.StopPrice.HasValue)
            {
                writer.Write(FixTags.StopPx);
                writer.Write(fixOrderCondition.StopPrice.Value);
            }
            if (!fixOrderCondition.Offset.HasValue)
                return;
            writer.Write(FixTags.PegOffsetValue);
            writer.Write(fixOrderCondition.Offset.Value);
            writer.Write(FixTags.PegOffsetType);
            writer.Write(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="tag"></param>
        /// <param name="getCondition"></param>
        /// <returns></returns>
        protected virtual bool ReadOrderCondition(
          IFixReader reader,
          FixTags tag,
          Func<OrderCondition> getCondition)
        {
            return reader.ReadOrderCondition(tag, getCondition);
        }

        private string WriteOrderCancelMessage(IFixWriter fixWriter, OrderCancelMessage msg)
        {
            WriteOrderMessage(fixWriter, msg);
            fixWriter.Write(FixTags.OrigClOrdID);
            WriteClOrdId(fixWriter, msg.OriginalTransactionId);
            fixWriter.Write(FixTags.ClOrdID);
            fixWriter.Write(msg.TransactionId);
            if (msg.OrderId.HasValue)
            {
                fixWriter.Write(FixTags.OrderID);
                fixWriter.Write(msg.OrderId.Value);
            }
            else if (!msg.OrderStringId.IsEmpty())
            {
                fixWriter.Write(FixTags.OrderID);
                fixWriter.Write(msg.OrderStringId);
            }
            DefaultFixDialect.WriteOrderSpecificMessage(fixWriter, msg);
            Sides? side = msg.Side;
            if (side.HasValue)
            {
                IFixWriter writer = fixWriter;
                side = msg.Side;
                int num = (int)side.Value;
                writer.WriteSide((Sides)num);
            }
            if (msg.OrderType.HasValue)
            {
                fixWriter.Write(FixTags.OrdType);
                fixWriter.Write(msg.GetFixType());
            }
            decimal? nullable = msg.Balance;
            if (nullable.HasValue)
            {
                fixWriter.Write(FixTags.LeavesQty);

                nullable = msg.Balance;
                decimal num = nullable.Value;
                fixWriter.Write(num);
            }
            nullable = msg.Volume;
            if (nullable.HasValue)
            {
                fixWriter.Write(FixTags.OrderQty);

                nullable = msg.Volume;
                decimal num = nullable.Value;
                fixWriter.Write(num);
            }

            if (!msg.Comment.IsEmpty())
            {
                fixWriter.Write(FixTags.Text);
                fixWriter.Write(ToLatin(msg.Comment));
            }

            fixWriter.WriteTransactTime(TimeStampParser);

            return FixMessages.OrderCancelRequest;
        }

        private string WriteOrderReplaceMessage(IFixWriter writer, OrderReplaceMessage msg)
        {
            WriteOrderMessage(writer, msg);
            writer.Write(FixTags.ClOrdID);
            writer.Write(msg.TransactionId);
            if (msg.OriginalTransactionId != 0L)
            {
                writer.Write(FixTags.OrigClOrdID);
                WriteClOrdId(writer, msg.OriginalTransactionId);
            }
            long? oldOrderId = msg.OldOrderId;
            if (oldOrderId.HasValue)
            {
                writer.Write(FixTags.OrderID);
                IFixWriter fixWriter = writer;
                oldOrderId = msg.OldOrderId;
                long num = oldOrderId.Value;
                fixWriter.Write(num);
            }
            else if (!msg.OldOrderStringId.IsEmpty())
            {
                writer.Write(FixTags.OrderID);
                writer.Write(msg.OldOrderStringId);
            }
            DefaultFixDialect.WriteOrderSpecificMessage(writer, msg);
            writer.WriteSide(msg.Side);
            writer.Write(FixTags.OrderQty);
            writer.Write(msg.Volume);
            writer.Write(FixTags.OrdType);
            writer.Write(msg.GetFixType());
            OrderTypes? orderType = msg.OrderType;
            OrderTypes orderTypes = OrderTypes.Market;
            if (!(orderType.GetValueOrDefault() == orderTypes & orderType.HasValue))
            {
                writer.Write(FixTags.Price);
                writer.Write(msg.Price);
            }
            FixOrderCondition condition = (FixOrderCondition)msg.Condition;
            decimal? nullable1;
            if (condition != null)
            {
                nullable1 = condition.StopPrice;
                if (nullable1.HasValue)
                {
                    writer.Write(FixTags.StopPx);
                    IFixWriter fixWriter = writer;
                    nullable1 = condition.StopPrice;
                    decimal num = nullable1.Value;
                    fixWriter.Write(num);
                }
            }
            char fixTimeInForce = msg.GetFixTimeInForce();
            writer.Write(FixTags.TimeInForce);
            writer.Write(fixTimeInForce);
            if (fixTimeInForce == '6')
                writer.WriteExpiryDate(msg, DateParser, TimeZone);
            writer.WriteTransactTime(TimeStampParser);
            bool? isMargin = msg.IsMargin;
            if (isMargin.GetValueOrDefault() & isMargin.HasValue)
            {
                writer.Write(FixTags.CashMargin);
                writer.Write(1);
            }
            nullable1 = msg.Slippage;
            if (nullable1.HasValue)
            {
                writer.Write(FixTags.Slippage);
                IFixWriter fixWriter = writer;
                nullable1 = msg.Slippage;
                decimal num = nullable1.Value;
                fixWriter.Write(num);
            }
            bool? nullable2 = msg.IsManual;
            if (nullable2.HasValue)
            {
                writer.Write(FixTags.ManualOrderIndicator);
                IFixWriter fixWriter = writer;
                nullable2 = msg.IsManual;
                int num = nullable2.Value ? 1 : 0;
                fixWriter.Write(num != 0);
            }
            writer.WritePositionEffect(msg.PositionEffect);
            nullable2 = msg.PostOnly;
            if (nullable2.HasValue)
            {
                writer.Write(FixTags.PostOnly);
                IFixWriter fixWriter = writer;
                nullable2 = msg.PostOnly;
                int num = nullable2.Value ? 1 : 0;
                fixWriter.Write(num != 0);
            }
            nullable1 = msg.OldOrderPrice;
            if (nullable1.HasValue)
            {
                writer.Write(FixTags.OldPrice);
                IFixWriter fixWriter = writer;
                nullable1 = msg.OldOrderPrice;
                decimal num = nullable1.Value;
                fixWriter.Write(num);
            }
            nullable1 = msg.OldOrderVolume;
            if (nullable1.HasValue)
            {
                writer.Write(FixTags.OldVolume);
                IFixWriter fixWriter = writer;
                nullable1 = msg.OldOrderVolume;
                decimal num = nullable1.Value;
                fixWriter.Write(num);
            }
            int? leverage = msg.Leverage;
            if (leverage.HasValue)
            {
                writer.Write(FixTags.Leverage);
                IFixWriter fixWriter = writer;
                leverage = msg.Leverage;
                int num = leverage.Value;
                fixWriter.Write(num);
            }
            return FixMessages.OrderCancelReplaceRequest;
        }

        private string WriteOrderGroupCancelMessage(IFixWriter writer, OrderGroupCancelMessage msg)
        {
            writer.Write(FixTags.ClOrdID);
            writer.Write(msg.TransactionId);
            writer.WriteTransactTime(TimeStampParser);
            char ch = '7';
            SecurityId securityId;

            if (!msg.SecurityId.SecurityCode.IsEmpty())
            {
                ch = '1';
                writer.Write(FixTags.Symbol);
                writer.Write(msg.SecurityId.SecurityCode);
                writer.Write(FixTags.SecurityExchange);
                IFixWriter fixWriter = writer;
                securityId = msg.SecurityId;
                string boardCode = securityId.BoardCode;
                fixWriter.Write(boardCode);
            }

            securityId = msg.SecurityId;

            if (!securityId.BoardCode.IsEmpty())
            {
                ch = '8';
                writer.Write(FixTags.SecurityExchange);
                IFixWriter fixWriter = writer;
                securityId = msg.SecurityId;
                string boardCode = securityId.BoardCode;
                fixWriter.Write(boardCode);
            }

            string securityType = msg.ToSecurityType();

            if (securityType != null)
            {
                ch = '5';
                writer.Write(FixTags.SecurityType);
                writer.Write(securityType);
            }
            Sides? side = msg.Side;
            if (side.HasValue)
            {
                side = msg.Side;
                int num = (int)side.Value;
                writer.WriteSide((Sides)num);
            }
            writer.Write(FixTags.MassCancelRequestType);
            writer.Write(ch);

            return FixMessages.OrderMassCancelRequest;
        }

        private static string WriteOrderStatusMessage(IFixWriter writer, OrderStatusMessage msg)
        {
            writer.Write(FixTags.MassStatusReqID);
            writer.Write(msg.TransactionId);
            if (msg.OriginalTransactionId != 0L)
            {
                writer.Write(FixTags.OrigClOrdID);
                writer.Write(msg.OriginalTransactionId);
            }
            writer.Write(FixTags.SubscriptionRequestType);
            writer.Write(msg.GetSubscriptionType());
            if (msg.IsSubscribe)
            {
                writer.Write(FixTags.MassStatusReqType);
                writer.Write(7);
                if (msg.States.Length != 0)
                {
                    writer.Write(FixTags.OrdStatus);
                    writer.Write(((IEnumerable<OrderStates>)msg.States).Select(s => s.To<string>()).JoinComma());
                }
            }
            return FixMessages.OrderMassStatusRequest;
        }

        private static string WriteOrderStatusMessageSubscribed(IFixWriter writer, OrderStatusMessage msg)
        {
            writer.Write(FixTags.OrdStatusReqID);
            writer.Write(msg.TransactionId);
            writer.Write(FixTags.SubscriptionRequestType);
            writer.Write(msg.GetSubscriptionType());
            if (msg.OrderId.HasValue)
            {
                writer.Write(FixTags.OrderID);
                writer.Write(msg.OrderId.Value);
            }
            else if (!msg.OrderStringId.IsEmpty())
            {
                writer.Write(FixTags.OrderID);
                writer.Write(msg.OrderStringId);
            }
            if (msg.OriginalTransactionId != 0L)
            {
                writer.Write(FixTags.ClOrdID);
                writer.Write(msg.OriginalTransactionId);
            }
            return FixMessages.OrderStatusRequest;
        }

        private string WritePortfolioLookup(IFixWriter writer, PortfolioLookupMessage msg)
        {
            writer.Write(FixTags.PosReqID);
            writer.Write(msg.TransactionId.To<long>());
            if (msg.OriginalTransactionId != 0L)
            {
                writer.Write(FixTags.MDResponseID);
                writer.Write(msg.OriginalTransactionId.To<long>());
            }
            writer.Write(FixTags.SubscriptionRequestType);
            writer.Write(msg.GetSubscriptionType());
            writer.Write(FixTags.PosReqType);
            writer.Write(0);
            if (!msg.PortfolioName.IsEmpty())
            {
                WriteAccount(writer, msg);
                writer.Write(FixTags.AccountType);
                writer.Write(1);
            }
            if (!msg.StrategyId.IsEmpty())
            {
                writer.Write(FixTags.StrategyTypeId);
                writer.Write(msg.StrategyId);
            }
            Sides? side = msg.Side;
            if (side.HasValue)
            {
                writer.Write(FixTags.Side);
                IFixWriter fixWriter = writer;
                side = msg.Side;
                int fix = side.Value.ToFix();
                fixWriter.Write((char)fix);
            }

            return FixMessages.RequestForPositions;
        }

        private static string WritePortfolioMessage(IFixWriter writer, PortfolioMessage msg)
        {
            writer.Write(FixTags.PosReqID);
            writer.Write(msg.TransactionId.To<long>());
            if (msg.OriginalTransactionId != 0L)
            {
                writer.Write(FixTags.MDResponseID);
                writer.Write(msg.OriginalTransactionId.To<long>());
            }
            writer.Write(FixTags.SubscriptionRequestType);
            writer.Write(msg.GetSubscriptionType());
            writer.Write(FixTags.PosReqType);
            writer.Write(0);
            if (!msg.PortfolioName.IsEmpty())
            {
                writer.Write(FixTags.Account);
                writer.Write(msg.PortfolioName);
                writer.Write(FixTags.AccountType);
                writer.Write(1);
            }
            return FixMessages.RequestForPositions;
        }


        private sealed class ExecutionReportReader
        {
            public DefaultFixDialect _fixDialect;
            public IFixReader _iFixReader;
            public OrderCondition _orderCondition;
            public Func<OrderCondition> _myFunc;

            internal bool ReadOrderCondition(
              FixTags tag,
              IFixReader reader,
              ExecutionReport report)
            {
                return _fixDialect.ReadOrderCondition(_iFixReader, tag, _myFunc ?? (_myFunc = new Func<OrderCondition>(CreateOrderCondition)));
            }

            internal OrderCondition CreateOrderCondition() => _orderCondition ?? (_orderCondition = _fixDialect.OrderConditionType.CreateOrderCondition());

            internal void Handler(
                    ExecutionReport report,
                    Action<Message> handler,
                    ExecutionMessage msg)
            {
                if (_orderCondition == null)
                    return;
                msg.Condition = _orderCondition;
            }
        }

        private bool? ProcessExecutionReport(IFixReader fixReader, Action<Message> handler)
        {
            ExecutionReportReader reader = new ExecutionReportReader();
            reader._fixDialect = this;
            reader._iFixReader = fixReader;
            reader._orderCondition = null;
            ExecutionReport report = new ExecutionReport();
            bool? nullable1 = ReadExecutionReport(reader._iFixReader, report, TimeStampParser, new Func<FixTags, IFixReader, ExecutionReport, bool>(reader.ReadOrderCondition));
            if (!nullable1.HasValue)
                return new bool?();
            bool? nullable2 = nullable1;
            if (!nullable2.GetValueOrDefault() & nullable2.HasValue)
                return new bool?(false);
            ProcessExecutionReport(report, handler, new Action<ExecutionReport, Action<Message>, ExecutionMessage>(reader.Handler));
            return new bool?(true);
        }

        private static string WritePortfolioRouteListRequestMessage(IFixWriter writer, PortfolioRouteListRequestMessage msg)
        {
            writer.Write(FixTags.MassStatusReqID);
            writer.Write(msg.TransactionId);
            return FixExtendedMessages.PortfolioRouteListRequest;
        }

        private static bool? ProcessPortfolioRoute(IFixReader fixReader, Action<Message> handler)
        {
            PortfolioRouteReader reader = new PortfolioRouteReader();
            reader._iFixReader = fixReader;
            reader._transId = null;
            reader._account = null;
            reader._ids = null;
            if (!reader._iFixReader.ReadMessage(new Func<FixTags, bool>(reader.ProcessFixTags)))
                return new bool?();
            Action<Message> action = handler;
            PortfolioRouteMessage msg = new PortfolioRouteMessage();
            msg.OriginalTransactionId = reader._transId.To<long?>().GetValueOrDefault();
            msg.AdapterId = reader._ids.To<Guid>();
            msg.PortfolioName = reader._account;
            action(msg);
            return new bool?(true);
        }


        private sealed class PortfolioRouteReader
        {
            public string _transId;
            public IFixReader _iFixReader;
            public string _ids;
            public string _account;

            internal bool ProcessFixTags(FixTags tag)
            {
                switch (tag)
                {
                    case FixTags.Account:
                        _account = _iFixReader.ReadString();
                        return true;
                    case FixTags.MassStatusReqID:
                        _transId = _iFixReader.ReadString();
                        return true;
                    case FixTags.Id:
                        _ids = _iFixReader.ReadString();
                        return true;
                    default:
                        return false;
                }
            }
        }
    }
}