using Ecng.Collections;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace StockSharp.Fix.Native
{
    /// <summary>FIX messages codes.</summary>
    public static class FixMessages
    {
        private static readonly Dictionary<string, string> _msgDictionary = new Dictionary<string, string>();
        /// <summary>The response to ping.</summary>
        public const string Heartbeat = "0";
        /// <summary>Ping request.</summary>
        public const string TestRequest = "1";
        /// <summary>Get counter request.</summary>
        public const string ResendRequest = "2";
        /// <summary>The response with the error description.</summary>
        public const string Reject = "3";
        /// <summary>The request for counter reset.</summary>
        public const string SequenceReset = "4";
        /// <summary>Exit.</summary>
        public const string Logout = "5";
        /// <summary>Information about order or trade.</summary>
        public const string ExecutionReport = "8";
        /// <summary>
        /// The response with error to <see cref="F:StockSharp.Fix.Native.FixMessages.OrderCancelRequest" />.
        /// </summary>
        public const string OrderCancelReject = "9";
        /// <summary>Input.</summary>
        public const string Logon = "A";
        /// <summary>The request for the getting the orders status.</summary>
        public const string OrderMassStatusRequest = "AF";
        /// <summary>
        /// The response with error to <see cref="F:StockSharp.Fix.Native.FixMessages.QuoteRequest" />.
        /// </summary>
        public const string QuoteRequestReject = "AG";
        /// <summary>
        /// The request the following purposes in markets that employ tradeable or restricted tradeable quotes.
        /// </summary>
        public const string QuoteStatusRequest = "a";
        /// <summary>
        /// The response with error to <see cref="F:StockSharp.Fix.Native.FixMessages.QuoteStatusRequest" />.
        /// </summary>
        public const string QuoteStatusReport = "AI";
        /// <summary>The request for the getting positions.</summary>
        public const string RequestForPositions = "AN";
        /// <summary>
        /// The response to <see cref="F:StockSharp.Fix.Native.FixMessages.RequestForPositions" />.
        /// </summary>
        public const string RequestForPositionsAck = "AO";
        /// <summary>The position information.</summary>
        public const string PositionReport = "AP";
        /// <summary>Information about news.</summary>
        public const string News = "B";
        /// <summary>The request for user change.</summary>
        public const string UserRequest = "BE";
        /// <summary>
        /// The response to <see cref="F:StockSharp.Fix.Native.FixMessages.UserRequest" />.
        /// </summary>
        public const string UserResponse = "BF";
        /// <summary>The request for instruments getting.</summary>
        public const string SecurityDefinitionRequest = "c";
        /// <summary>Security info.</summary>
        public const string SecurityDefinition = "d";
        /// <summary>Order registration.</summary>
        public const string NewOrderSingle = "D";
        /// <summary>The request for order cancel.</summary>
        public const string OrderCancelRequest = "F";
        /// <summary>The request for order change.</summary>
        public const string OrderCancelReplaceRequest = "G";
        /// <summary>The response to a request with the error description.</summary>
        public const string BusinessMessageReject = "j";
        /// <summary>The request for the getting the order status.</summary>
        public const string OrderStatusRequest = "H";
        /// <summary>The request for the getting the session status.</summary>
        public const string TradingSessionStatusRequest = "g";
        /// <summary>The response with the session state.</summary>
        public const string TradingSessionStatus = "h";
        /// <summary>The request for the orders cancel.</summary>
        public const string OrderMassCancelRequest = "q";
        /// <summary>The request for the getting quotes.</summary>
        public const string QuoteRequest = "R";
        /// <summary>Information about quotes.</summary>
        public const string Quote = "S";
        /// <summary>Quotes for multiple securities.</summary>
        public const string MassQuote = "i";
        /// <summary>
        /// The response with error to <see cref="F:StockSharp.Fix.Native.FixMessages.MarketDataRequest" />.
        /// </summary>
        public const string MarketDataRequestReject = "Y";
        /// <summary>Information about securities.</summary>
        public const string SecurityList = "y";
        /// <summary>The request for the getting market data.</summary>
        public const string MarketDataRequest = "V";
        /// <summary>Information (incremental) about market data.</summary>
        public const string MarketDataIncrementalRefresh = "X";
        /// <summary>The request for instruments getting.</summary>
        public const string SecurityListRequest = "x";
        /// <summary>Information about market data.</summary>
        public const string MarketDataSnapshotFullRefresh = "W";
        /// <summary>The request for the quotes cancel.</summary>
        public const string QuoteCancel = "Z";
        /// <summary>
        /// The response to <see cref="F:StockSharp.Fix.Native.FixMessages.OrderMassCancelRequest" />.
        /// </summary>
        public const string OrderMassCancelReport = "r";
        /// <summary>Request the status of a security.</summary>
        public const string SecurityStatusRequest = "e";
        /// <summary>Report changes in status to a security.</summary>
        public const string SecurityStatus = "f";
        /// <summary>
        /// This message is used to request a retransmission of a set of one or more messages generated by the application.
        /// </summary>
        public const string ApplicationMessageRequest = "BW";
        /// <summary>
        /// This message is used to acknowledge an <see cref="F:StockSharp.Fix.Native.FixMessages.ApplicationMessageRequest" /> providing a status on the request (i.e. whether successful or not).
        /// </summary>
        public const string ApplicationMessageRequestAck = "BX";
        /// <summary>
        /// Report, initiated by <see cref="F:StockSharp.Fix.Native.FixMessages.ApplicationMessageRequest" />.
        /// </summary>
        public const string ApplicationMessageReport = "BY";
        /// <summary>Administrative custom message.</summary>
        public const string XmlNonFix = "n";

        static FixMessages()
        {
            Type[] messages = new Type[2] { typeof(FixMessages), typeof(FixExtendedMessages) };

            foreach (Type type in messages)
            {
                foreach (FieldInfo field in type.GetFields())
                {
                    if (field.IsLiteral)
                    {
                        string str = (string)field.GetValue(null);

                        if (!_msgDictionary.TryAdd2(str, field.Name))
                        {
                            throw new InvalidOperationException(string.Concat("_msgDictionary.TryAdd2 ", str, field.Name));
                        }

                    }
                }
            }
        }

        /// <summary>To get the message name by its type.</summary>
        /// <param name="msgType">Message type.</param>
        /// <returns>The message name.</returns>
        public static string GetName(string msgType) => _msgDictionary.TryGetValue(msgType) ?? msgType;
    }
}
