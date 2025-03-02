using Ecng.Collections;
using Ecng.Common;
using StockSharp.Algo;
using StockSharp.Algo.Strategies.Messages;

using StockSharp.Fix.Dialects;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace StockSharp.Fix.Native
{
    /// <summary>FIX/FAST extension methods.</summary>
    public static partial class Extensions
    {
        /// <summary>Default UTCDateOnly format.</summary>
        public const string DateFormat = "yyyyMMdd";
        /// <summary>Default MonthYear format.</summary>
        public const string YearMonthFormat = "yyyyMM";
        /// <summary>Default UTCTimeOnly format.</summary>
        public const string TimeFormat = "hh\\:mm\\:ss\\.fff";
        /// <summary>Default UTCTimestamp format.</summary>
        public const string TimeStampFormat = "yyyyMMdd-HH:mm:ss.fff";
        /// <summary>Empty tag.</summary>
        public const FixTags EmptyTag = (FixTags)(-1);
        /// <summary>
        /// <see cref="T:StockSharp.Messages.Level1ChangeMessage" />.
        ///     </summary>
        public const char Level1 = '*';
        /// <summary>
        /// <see cref="T:StockSharp.Messages.NewsMessage" />.
        ///     </summary>
        public const char News = 'n';
        /// <summary>
        /// <see cref="T:StockSharp.Messages.TimeFrameCandleMessage" />.
        ///     </summary>
        public const char CandleTimeFrame = 'W';
        /// <summary>
        /// <see cref="T:StockSharp.Messages.VolumeCandleMessage" />.
        ///     </summary>
        public const char CandleVolume = 'V';
        /// <summary>
        /// <see cref="T:StockSharp.Messages.TickCandleMessage" />.
        ///     </summary>
        public const char CandleTick = 'i';
        /// <summary>
        /// <see cref="T:StockSharp.Messages.RangeCandleMessage" />.
        ///     </summary>
        public const char CandleRange = 'z';
        /// <summary>
        /// <see cref="T:StockSharp.Messages.RenkoCandleMessage" />.
        ///     </summary>
        public const char CandleRenko = 'R';
        /// <summary>
        /// <see cref="T:StockSharp.Messages.PnFCandleMessage" />.
        ///     </summary>
        public const char CandlePnF = 'x';
        /// <summary>
        /// <see cref="T:StockSharp.Messages.HeikinAshiCandleMessage" />.
        ///     </summary>
        public const char CandleHeikin = '%';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.ExecutionTypes.OrderLog" />.
        ///     </summary>
        public const char OrderLog = 'I';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.ExecutionTypes.Transaction" />.
        ///     </summary>
        public const char Transactions = 'Q';
        /// <summary>
        /// <see cref="F:StockSharp.Fix.FixStopOrderTypes.TakeProfit" />.
        ///     </summary>
        public const char TakeProfit = 'T';
        /// <summary>
        /// <see cref="F:StockSharp.Fix.FixStopOrderTypes.TakeProfitTrailing" />.
        ///     </summary>
        public const char TakeProfitTrailing = 'W';
        /// <summary>
        /// <see cref="F:StockSharp.Fix.FixStopOrderTypes.StopLossTrailing" />.
        ///     </summary>
        public const char StopTrailing = 'Z';
        private static readonly PairSet<char, Type> _charType;
        /// <summary>
        /// <see cref="F:StockSharp.Messages.PositionChangeTypes.Commission" />.
        ///     </summary>
        public const string Commission = "COMM";
        /// <summary>
        /// <see cref="F:StockSharp.Messages.PositionChangeTypes.RealizedPnL" />.
        ///     </summary>
        public const string RealizedPnL = "RPNL";
        /// <summary>
        /// <see cref="F:StockSharp.Messages.PositionChangeTypes.UnrealizedPnL" />.
        ///     </summary>
        public const string UnrealizedPnL = "UPNL";
        /// <summary>
        /// <see cref="F:StockSharp.Messages.PositionChangeTypes.Leverage" />.
        ///     </summary>
        public const string Leverage = "LVRG";
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.PriceStep" />.
        ///     </summary>
        public const char PriceStep = '#';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.VolumeStep" />.
        ///     </summary>
        public const char VolumeStep = '&';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.Multiplier" />.
        ///     </summary>
        public const char Multiplier = '@';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.StepPrice" />.
        ///     </summary>
        public const char StepPrice = '$';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.MarginBuy" />.
        ///     </summary>
        public const char MarginBuy = '<';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.MarginSell" />.
        ///     </summary>
        public const char MarginSell = '>';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.ImpliedVolatility" />.
        ///     </summary>
        public const char ImpliedVolatility = '/';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.TheorPrice" />.
        ///     </summary>
        public const char TheorPrice = '\\';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.LastTradePrice" />.
        ///     </summary>
        public const char LastTradePrice = 'p';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.LastTradeVolume" />.
        ///     </summary>
        public const char LastTradeVolume = 'v';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.BestBidPrice" />.
        ///     </summary>
        public const char BestBidPrice = 'b';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.BestBidVolume" />.
        ///     </summary>
        public const char BestBidVolume = 'j';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.BestAskPrice" />.
        ///     </summary>
        public const char BestAskPrice = 'c';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.BestAskVolume" />.
        ///     </summary>
        public const char BestAskVolume = 'm';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.BestBidTime" />.
        ///     </summary>
        public const char BestBidTime = 'h';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.BestAskTime" />.
        ///     </summary>
        public const char BestAskTime = 'K';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.BidsCount" />.
        ///     </summary>
        public const char BidsCount = 'q';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.BidsVolume" />.
        ///     </summary>
        public const char BidsVolume = 'w';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.AsksVolume" />.
        ///     </summary>
        public const char AsksVolume = 'e';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.AsksCount" />.
        ///     </summary>
        public const char AsksCount = 'r';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.Change" />.
        ///     </summary>
        public const char Change = 't';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.MaxPrice" />.
        ///     </summary>
        public const char MaxPrice = 'y';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.MinPrice" />.
        ///     </summary>
        public const char MinPrice = 'u';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.Yield" />.
        ///     </summary>
        public const char Yield = 'o';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.AccruedCouponIncome" />.
        ///     </summary>
        public const char AccruedCouponIncome = 's';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.TradesCount" />.
        ///     </summary>
        public const char TradesCount = 'd';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.State" />.
        ///     </summary>
        public const char State = 'f';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.LastTradeId" />.
        ///     </summary>
        public const char LastTradeId = 'U';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.LastTradeTime" />.
        ///     </summary>
        public const char LastTradeTime = 'P';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.LastTradeOrigin" />.
        ///     </summary>
        public const char LastTradeOrigin = 'T';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.LastTradeUpDown" />.
        ///     </summary>
        public const char LastTradeUpDown = 'D';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.ShortRatio" />.
        ///     </summary>
        public const char ShortRatio = 'E';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.AverageTrueRange" />.
        ///     </summary>
        public const char AverageTrueRange = 'F';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.AveragePrice" />.
        ///     </summary>
        public const char AveragePrice = 'G';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.CurrentRatio" />.
        ///     </summary>
        public const char CurrentRatio = '~';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.SharesFloat" />.
        ///     </summary>
        public const char SharesFloat = 'Z';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.Duration" />.
        ///     </summary>
        public const char Duration = 'A';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.IssueSize" />.
        ///     </summary>
        public const char IssueSize = 'k';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.BuyBackDate" />.
        ///     </summary>
        public const char BuyBackDate = 'Q';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.BuyBackPrice" />.
        ///     </summary>
        public const char BuyBackPrice = 'g';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.Turnover" />.
        ///     </summary>
        public const char Turnover = 'a';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.OperatingMargin" />.
        ///     </summary>
        public const char OperatingMargin = 'M';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.GrossMargin" />.
        ///     </summary>
        public const char GrossMargin = 'L';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.Dividend" />.
        ///     </summary>
        public const char Dividend = '(';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.CouponValue" />.
        ///     </summary>
        public const char CouponValue = ')';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.CouponDate" />.
        ///     </summary>
        public const char CouponDate = 'l';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.CouponPeriod" />.
        ///     </summary>
        public const char CouponPeriod = '?';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.MarketPriceYesterday" />.
        ///     </summary>
        public const char MarketPriceYesterday = 'Y';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.MarketPriceToday" />.
        ///     </summary>
        public const char MarketPriceToday = '+';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.VWAPPrev" />.
        ///     </summary>
        public const char VWAPPrev = 'S';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.YieldVWAP" />.
        ///     </summary>
        public const char YieldVWAP = '-';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.YieldVWAPPrev" />.
        ///     </summary>
        public const char YieldVWAPPrev = 'X';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.HistoricalVolatility" />.
        ///     </summary>
        public const char HistoricalVolatility = '\x0080';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.Delta" />.
        ///     </summary>
        public const char Delta = '\x0081';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.Gamma" />.
        ///     </summary>
        public const char Gamma = '\x0082';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.Vega" />.
        ///     </summary>
        public const char Vega = '\x0083';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.Theta" />.
        ///     </summary>
        public const char Theta = '\x0084';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.Rho" />.
        ///     </summary>
        public const char Rho = '\x0085';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.PriceEarnings" />.
        ///     </summary>
        public const char PriceEarnings = '\x0086';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.ForwardPriceEarnings" />.
        ///     </summary>
        public const char ForwardPriceEarnings = '\x0087';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.PriceEarningsGrowth" />.
        ///     </summary>
        public const char PriceEarningsGrowth = '\x0088';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.PriceSales" />.
        ///     </summary>
        public const char PriceSales = '\x0089';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.PriceBook" />.
        ///     </summary>
        public const char PriceBook = '\x008A';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.PriceCash" />.
        ///     </summary>
        public const char PriceCash = '\x008B';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.PriceFreeCash" />.
        ///     </summary>
        public const char PriceFreeCash = '\x008C';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.Payout" />.
        ///     </summary>
        public const char Payout = '\x008D';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.SharesOutstanding" />.
        ///     </summary>
        public const char SharesOutstanding = '\x008E';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.FloatShort" />.
        ///     </summary>
        public const char FloatShort = '\x008F';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.ReturnOnAssets" />.
        ///     </summary>
        public const char ReturnOnAssets = '\x0090';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.ReturnOnEquity" />.
        ///     </summary>
        public const char ReturnOnEquity = '\x0091';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.ReturnOnInvestment" />.
        ///     </summary>
        public const char ReturnOnInvestment = '\x0092';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.QuickRatio" />.
        ///     </summary>
        public const char QuickRatio = '\x0093';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.LongTermDebtEquity" />.
        ///     </summary>
        public const char LongTermDebtEquity = '\x0094';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.TotalDebtEquity" />.
        ///     </summary>
        public const char TotalDebtEquity = '\x0095';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.ProfitMargin" />.
        ///     </summary>
        public const char ProfitMargin = '\x0096';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.Beta" />.
        ///     </summary>
        public const char Beta = '\x0097';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.HistoricalVolatilityWeek" />.
        ///     </summary>
        public const char HistoricalVolatilityWeek = '\x0098';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.HistoricalVolatilityMonth" />.
        ///     </summary>
        public const char HistoricalVolatilityMonth = '\x0099';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.IsSystem" />.
        ///     </summary>
        public const char IsSystem = '\x009A';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.Decimals" />.
        ///     </summary>
        public const char Decimals = '\x009B';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.AfterSplit" />.
        ///     </summary>
        public const char AfterSplit = '\x009C';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.BeforeSplit" />.
        ///     </summary>
        public const char BeforeSplit = '\x009D';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.CommissionTaker" />.
        ///     </summary>
        public const char CommissionTaker = '\x009E';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.CommissionMaker" />.
        ///     </summary>
        public const char CommissionMaker = '\x009F';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.MinVolume" />.
        ///     </summary>
        public const char MinVolume = ' ';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.UnderlyingMinVolume" />.
        ///     </summary>
        public const char UnderlyingMinVolume = '¡';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.Index" />.
        ///     </summary>
        public const char Index = '¢';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.Imbalance" />.
        ///     </summary>
        public const char Imbalance = '£';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.UnderlyingPrice" />.
        ///     </summary>
        public const char UnderlyingPrice = '¤';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.MaxVolume" />.
        ///     </summary>
        public const char MaxVolume = '¥';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.LowBidPrice" />.
        ///     </summary>
        public const char LowBidPrice = '¦';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.HighAskPrice" />.
        ///     </summary>
        public const char HighAskPrice = '§';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.LastTradeVolumeLow" />.
        ///     </summary>
        public const char LastTradeVolumeLow = '¨';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.LastTradeVolumeHigh" />.
        ///     </summary>
        public const char LastTradeVolumeHigh = '©';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.LowBidVolume" />.
        ///     </summary>
        public const char LowBidVolume = 'ª';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.HighAskVolume" />.
        ///     </summary>
        public const char HighAskVolume = '«';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.UnderlyingBestBidPrice" />.
        ///     </summary>
        public const char UnderlyingBestBidPrice = '¬';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.UnderlyingBestAskPrice" />.
        ///     </summary>
        public const char UnderlyingBestAskPrice = '\x00AD';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.MedianPrice" />.
        ///     </summary>
        public const char MedianPrice = '®';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.HighPrice52Week" />.
        ///     </summary>
        public const char HighPrice52Week = '¯';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.LowPrice52Week" />.
        ///     </summary>
        public const char LowPrice52Week = '°';
        /// <summary>
        /// <see cref="F:StockSharp.Messages.Level1Fields.LastTradeStringId" />.
        ///     </summary>
        public const char LastTradeStringId = '±';

        static Extensions()
        {
            PairSet<char, Type> pairSet = new PairSet<char, Type>();
            pairSet.Add('W', typeof(TimeFrameCandleMessage));
            pairSet.Add('V', typeof(VolumeCandleMessage));
            pairSet.Add('i', typeof(TickCandleMessage));
            pairSet.Add('z', typeof(RangeCandleMessage));
            pairSet.Add('R', typeof(RenkoCandleMessage));
            pairSet.Add('x', typeof(PnFCandleMessage));
            pairSet.Add('%', typeof(HeikinAshiCandleMessage));
            _charType = pairSet;
        }

        /// <summary>
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <returns>
        /// </returns>
        public static byte TryReplaceSoh(this byte value)
        {
            if (value == 1)
                value = 124;
            return value;
        }

        /// <summary>Copy content.</summary>
        /// <param name="dest">Destination.</param>
        /// <param name="source">Source.</param>
        public static void WriteStream(this IFixWriter dest, IFixWriter source)
        {
            MemoryStream stream = (MemoryStream)source.Stream;
            dest.WriteBytes(stream.GetBuffer(), 0, (int)stream.Position);
            source.ClearState();
            stream.Position = 0L;
        }

        /// <summary>Write FIX message.</summary>
        /// <param name="writer">Whole message writer.</param>
        /// <param name="bodyWriter">Body only writer.</param>
        /// <param name="version">Version.</param>
        /// <param name="msgType">
        /// <see cref="T:StockSharp.Fix.Native.FixMessages" /> value.</param>
        /// <param name="senderCompId">Sender ID.</param>
        /// <param name="targetCompId">Target ID.</param>
        /// <param name="sendingTimeParser">Time parser.</param>
        /// <param name="seqNum">Sequence number.</param>
        /// <param name="handler">Handler.</param>
        public static void WriteFixMessage(
                                                  this IFixWriter writer,
                                                  IFixWriter bodyWriter,
                                                  string version,
                                                  string msgType,
                                                  string senderCompId,
                                                  string targetCompId,
                                                  FastDateTimeParser sendingTimeParser,
                                                  long seqNum,
                                                  Action<IFixWriter> handler
                                          )
        {
            if (handler == null)
                throw new ArgumentNullException("handler is null");

            writer.Write(FixTags.BeginString);
            writer.Write(version);

            bodyWriter.Write(FixTags.MsgType);
            bodyWriter.Write(msgType);

            if (!senderCompId.IsEmpty())
            {
                bodyWriter.Write(FixTags.SenderCompID);
                bodyWriter.Write(senderCompId);
            }

            if (!targetCompId.IsEmpty())
            {
                bodyWriter.Write(FixTags.TargetCompID);
                bodyWriter.Write(targetCompId);
            }

            bodyWriter.Write(FixTags.SendingTime);
            bodyWriter.Write(DateTime.UtcNow, sendingTimeParser);
            bodyWriter.Write(FixTags.MsgSeqNum);
            bodyWriter.Write(seqNum);

            handler(bodyWriter);
            writer.Write(FixTags.BodyLength);
            writer.Write(bodyWriter.Stream.Position);
            writer.WriteStream(bodyWriter);

            uint checksumValue = writer.CheckSum & byte.MaxValue;
            writer.CheckSum = 0U;
            writer.Write(FixTags.CheckSum);

            /*
             * There’s nothing more to add – that’s the checksum. 
             * 
             * If you’re sending a FIX message, you’ll convert the value to a string and pad it with zeroes if necessary to make sure it’s exactly 3 characters:
             * 
             *          string checksumStr = checksum.ToString().PadLeft(3, '0');             
             */

            writer.Write(string.Format("{0:D3}", checksumValue));
        }

        private static bool IsValidTag(this IFixReader reader, FixTags tags)
        {
            FixTags fixTags = reader != null ? reader.ReadTag() : throw new ArgumentNullException("Reader is null");

            if (fixTags == (FixTags)(-1))
                return false;

            if (fixTags != tags)
                throw new InvalidOperationException("{0} and {1} not the same".Put(tags, fixTags));
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool ReadMessage(this IFixReader reader, Func<FixTags, bool> handler)
        {
            if (reader == null)
                throw new ArgumentNullException("Reader is null");
            if (handler == null)
                throw new ArgumentNullException("Handler is null");
            while (true)
            {
                FixTags fixTags;
                do
                {
                    fixTags = reader.ReadTag();
                    switch (fixTags)
                    {
                        case (FixTags)(-1):
                            return false;
                        case FixTags.CheckSum:
                            return true;
                        default:
                            continue;
                    }
                }
                while (handler(fixTags));
                reader.SkipValue();
            }
        }

        /// <summary>Read FIX header.</summary>
        /// <param name="reader">Reader.</param>
        /// <param name="skipBeginString">Skip read <see cref="F:StockSharp.Fix.Native.FixTags.BeginString" />.</param>
        /// <param name="expectedVersion">Expected <see cref="T:StockSharp.Fix.FixVersions" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Fix.Native.FixMessages" /> value.</returns>
        public static string ReadHeader(this IFixReader reader, bool skipBeginString, string expectedVersion)
        {
            reader.CheckSum = skipBeginString ? 117U : 0U;
            reader.BytesCount = skipBeginString ? 117 : 2;
            if (!skipBeginString && !reader.IsValidTag(FixTags.BeginString))
                return null;
            reader.ReadString().CompareIgnoreCase(expectedVersion);
            if (!reader.IsValidTag(FixTags.BodyLength))
                return null;
            reader.ReadInt();
            if (!reader.IsValidTag(FixTags.MsgType))
                return null;
            string str = reader.ReadString();
            reader.BytesCount = 0;
            return str;
        }

        /// <summary>Read FIX trailer.</summary>
        /// <param name="reader">Reader.</param>
        /// <param name="fullRead">
        /// <see langword="true" /> if the message was successfully read, othewise, returns <see langword="false" />.</param>
        public static void ReadTrailer(this IFixReader reader, out bool fullRead)
        {
            uint readerCheckSum = (uint)((int)reader.CheckSum - 158 & byte.MaxValue);
            int num2 = reader.ReadString().To<int>();
            fullRead = true;

            if (!reader.CheckSumDisabled && readerCheckSum != num2)
            {
                throw new InvalidOperationException("{0} {1} is not equal".Put(readerCheckSum, num2));
            }

        }

        /// <summary>Skip reading message.</summary>
        /// <param name="reader">Reader.</param>
        /// <returns>Possible error.</returns>
        public static Exception SkipMessage(this IFixReader reader)
        {
            try
            {
                while (reader.LastTag != (FixTags)(-1))
                {
                    if (!reader.IsValueRead)
                        reader.SkipValue();
                    if (reader.LastTag != FixTags.CheckSum)
                    {
                        int num = (int)reader.ReadTag();
                    }
                    else
                        break;
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        /// <summary>
        /// Convert <see cref="T:StockSharp.Messages.DataType" /> to <see cref="T:StockSharp.Fix.Native.MDEntryType" /> value.
        /// </summary>
        /// <param name="type">
        /// <see cref="T:StockSharp.Messages.DataType" /> value.</param>
        /// <param name="mdEntryArg">
        /// <see cref="F:StockSharp.Fix.Native.FixTags.MDEntryArg" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Fix.Native.MDEntryType" /> value.</returns>
        public static char ToFixMDType(this DataType type, out string mdEntryArg)
        {
            if (type == null)
                throw new ArgumentNullException("Type is null");

            mdEntryArg = !type.IsCandles || type.Arg == null ? null : type.DataTypeArgToString();

            if (type == DataType.Level1)
                return '*';

            if (type == DataType.MarketDepth)
                return '0';

            if (type == DataType.Ticks)
                return '2';

            if (type == DataType.OrderLog)
                return 'I';

            if (type == DataType.News)
                return 'n';

            if (type == DataType.Transactions)
                return 'Q';

            if (!type.IsCandles)
                throw new ArgumentOutOfRangeException("Unknown data type", type, null);

            char key;

            return _charType.TryGetKey(type.MessageType, out key) ? key : _charType[typeof(TimeFrameCandleMessage)];
        }

        /// <summary>
        /// Convert <see cref="T:StockSharp.Fix.Native.MDEntryType" /> to <see cref="T:StockSharp.Messages.DataType" /> value.
        /// </summary>
        /// <param name="mdEntryType">
        /// <see cref="T:StockSharp.Fix.Native.MDEntryType" /> value.</param>
        /// <param name="arg">
        /// <see cref="F:StockSharp.Fix.Native.FixTags.MDEntryArg" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Messages.DataType" /> value.</returns>
        public static DataType ToDataType(this char mdEntryType, string arg)
        {
            switch (mdEntryType)
            {
                case '*':
                    return DataType.Level1;
                case '0':
                case '1':
                    return DataType.MarketDepth;
                case '2':
                    return DataType.Ticks;
                case 'I':
                    return DataType.OrderLog;
                case 'Q':
                    return DataType.Transactions;
                case 'n':
                    return DataType.News;
                default:
                    Type messageType;
                    if (_charType.TryGetValue(mdEntryType, out messageType))
                        return DataType.Create(messageType, messageType.ToDataTypeArg(arg));

                    throw new InvalidOperationException(LocalizedStrings.Str1635Params.Put(mdEntryType));
            }
        }

        /// <summary>
        /// Convert <see cref="T:StockSharp.Messages.SessionStates" /> to <see cref="T:StockSharp.Fix.Native.TradSesStatus" /> value.
        /// </summary>
        /// <param name="state">
        /// <see cref="T:StockSharp.Messages.SessionStates" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Fix.Native.TradSesStatus" /> value.</returns>
        public static TradSesStatus ToFix(this SessionStates state)
        {
            switch (state)
            {
                case SessionStates.Assigned:
                    return TradSesStatus.PreOpen;
                case SessionStates.Active:
                    return TradSesStatus.Open;
                case SessionStates.Paused:
                    return TradSesStatus.Halted;
                case SessionStates.ForceStopped:
                    return TradSesStatus.Halted;
                case SessionStates.Ended:
                    return TradSesStatus.Closed;
                default:
                    throw new ArgumentOutOfRangeException("SessionStates", state, null);
            }
        }

        /// <summary>
        /// Convert <see cref="T:StockSharp.Fix.Native.TradSesStatus" /> to <see cref="T:StockSharp.Messages.SessionStates" /> value.
        /// </summary>
        /// <param name="status">
        /// <see cref="T:StockSharp.Fix.Native.TradSesStatus" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Messages.SessionStates" /> value.</returns>
        public static SessionStates FromFixStatus(this TradSesStatus status)
        {
            switch (status)
            {
                case TradSesStatus.Halted:
                    return SessionStates.Paused;
                case TradSesStatus.Open:
                case TradSesStatus.PreClose:
                    return SessionStates.Active;
                case TradSesStatus.Closed:
                    return SessionStates.Ended;
                case TradSesStatus.PreOpen:
                    return SessionStates.Assigned;
                default:
                    throw new ArgumentOutOfRangeException("TradSesStatus", status, LocalizedStrings.Str1594);
            }
        }

        /// <summary>
        /// Convert <see cref="T:StockSharp.Messages.CurrencyTypes" /> to <see cref="T:System.String" /> value.
        /// </summary>
        /// <param name="type">
        /// <see cref="T:StockSharp.Messages.CurrencyTypes" /> value.</param>
        /// <returns>
        /// <see cref="T:System.String" /> value.</returns>
        public static string ToFix(this CurrencyTypes type) => type.To<string>();

        /// <summary>
        /// Convert <see cref="T:StockSharp.Messages.OptionTypes" /> to <see cref="T:System.Int32" /> value.
        /// </summary>
        /// <param name="type">
        /// <see cref="T:StockSharp.Messages.OptionTypes" /> value.</param>
        /// <returns>
        /// <see cref="T:System.Int32" /> value.</returns>
        public static int ToFix(this OptionTypes type) => type != OptionTypes.Call ? 0 : 1;

        /// <summary>
        /// Convert <see cref="T:StockSharp.Fix.Native.PutOrCall" /> to <see cref="T:StockSharp.Messages.OptionTypes" /> value.
        /// </summary>
        /// <param name="type">
        /// <see cref="T:StockSharp.Fix.Native.PutOrCall" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Messages.OptionTypes" /> value.</returns>
        public static OptionTypes FromFixOptionType(this PutOrCall type)
        {
            if (type == PutOrCall.Put)
                return OptionTypes.Put;
            if (type == PutOrCall.Call)
                return OptionTypes.Call;
            throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// Get <see cref="T:StockSharp.Fix.Native.OrdType" /> value.
        /// </summary>
        /// <param name="message">A message containing info about the order.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Fix.Native.OrdType" /> value.</returns>
        public static char GetFixType(this OrderMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            FixOrderCondition condition = null;
            FixStopOrderTypes? stopOprderType = new FixStopOrderTypes?();

            if (message.Condition is FixOrderCondition)
            {
                condition = (FixOrderCondition)message.Condition;

                stopOprderType = condition.Type;
            }

            if (stopOprderType.HasValue)
            {
                switch (stopOprderType.GetValueOrDefault())
                {
                    case FixStopOrderTypes.StopLoss:
                        return condition.StopPrice.HasValue ? '4' : '3';
                    case FixStopOrderTypes.StopLossTrailing:
                        return 'Z';
                    case FixStopOrderTypes.TakeProfit:
                        return 'T';
                    case FixStopOrderTypes.TakeProfitTrailing:
                        return 'W';
                    case FixStopOrderTypes.MarketOnClose:
                        return '5';
                    case FixStopOrderTypes.MarketIfTouched:
                        return 'J';
                    default:
                        throw new NotSupportedException(LocalizedStrings.Str1601Params.Put(message.OrderType, message.TransactionId));
                }
            }
            else
            {
                OrderTypes? orderType = message.OrderType;
                if (orderType.HasValue)
                {
                    switch (orderType.GetValueOrDefault())
                    {
                        case OrderTypes.Limit:
                            break;
                        case OrderTypes.Market:
                            return '1';
                        default:
                            throw new ArgumentOutOfRangeException(condition.Type.ToString());
                    }
                }
                return '2';
            }
        }

        /// <summary>
        /// Set <see cref="T:StockSharp.Fix.Native.OrdType" /> value.
        /// </summary>
        /// <param name="message">A message containing info about the order.</param>
        /// <param name="ordType">
        /// <see cref="T:StockSharp.Fix.Native.OrdType" /> value.</param>
        public static void SetOrderType(this OrderMessage message, char ordType)
        {
            if (message == null)
                throw new ArgumentNullException("message is null");

            switch (ordType)
            {
                case '1':
                    message.OrderType = new OrderTypes?(OrderTypes.Market);
                    break;

                case '2':
                    message.OrderType = new OrderTypes?(OrderTypes.Limit);
                    break;

                case '3':
                case '4':
                case '5':
                case 'J':
                case 'T':
                case 'W':
                case 'Z':
                    message.OrderType = new OrderTypes?(OrderTypes.Conditional);
                    break;

                default:
                    throw new ArgumentOutOfRangeException("Invalid order Type", ordType, LocalizedStrings.Str1219);
            }
        }

        /// <summary>
        /// Convert <see cref="T:System.Boolean" /> to <see cref="T:StockSharp.Fix.Native.TickDirection" /> value.
        /// </summary>
        /// <param name="dir">
        /// <see cref="T:System.Boolean" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Fix.Native.TickDirection" /> value.</returns>
        public static char ToTickDir(this bool dir) => !dir ? '2' : '0';

        /// <summary>
        /// Convert <see cref="T:StockSharp.Fix.Native.TickDirection" /> to <see cref="T:System.Boolean" /> value.
        /// </summary>
        /// <param name="dir">
        /// <see cref="T:StockSharp.Fix.Native.TickDirection" /> value.</param>
        /// <returns>
        /// <see cref="T:System.Boolean" /> value.</returns>
        public static bool FromTickDir(this char dir)
        {
            switch (dir)
            {
                case '0':
                case '1':
                    return true;
                case '2':
                case '3':
                    return true;
                default:
                    throw new ArgumentOutOfRangeException("Invalid Tick Direction", dir, LocalizedStrings.Str1219);
            }
        }

        /// <summary>
        /// Convert <see cref="T:StockSharp.Messages.Sides" /> to <see cref="T:StockSharp.Fix.Native.Side" /> value.
        /// </summary>
        /// <param name="side">
        /// <see cref="T:StockSharp.Messages.Sides" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Fix.Native.Side" /> value.</returns>
        public static char ToFix(this Sides side) => side != Sides.Buy ? '2' : '1';

        /// <summary>
        /// Convert <see cref="T:StockSharp.Fix.Native.Side" /> to <see cref="T:StockSharp.Messages.Sides" /> value.
        /// </summary>
        /// <param name="side">
        /// <see cref="T:StockSharp.Fix.Native.Side" /> value.</param>
        /// <param name="required">
        /// <paramref name="side" /> is required.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Messages.Sides" /> value.</returns>
        public static Sides FromFixSide(this char side, bool required = false)
        {
            switch (side)
            {
                case '1':
                case '3':
                    return Sides.Buy;
                case '2':
                case '4':
                case '5':
                case '6':
                case '9':
                case 'A':
                    return Sides.Sell;

                case '7':
                    if (required)
                        throw new ArgumentNullException("Invalid Side Char");
                    return Sides.Buy;

                default:
                    throw new ArgumentOutOfRangeException("Invalid Side Char", side, LocalizedStrings.Str1219);
            }
        }

        /// <summary>
        /// Convert <see cref="T:StockSharp.Messages.ExecutionMessage" /> to <see cref="T:StockSharp.Fix.Native.OrdStatus" /> value.
        /// </summary>
        /// <param name="message">
        /// <see cref="T:StockSharp.Messages.ExecutionMessage" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Fix.Native.OrdStatus" /> value.</returns>
        public static char? ToFixOrdStatus(this ExecutionMessage message)
        {
            OrderStates? orderState = message.OrderState;
            if (orderState.HasValue)
            {
                switch (orderState.GetValueOrDefault())
                {
                    case OrderStates.Active:
                        return new char?('0');
                    case OrderStates.Done:
                        int num1;
                        if (message.Balance.HasValue)
                        {
                            decimal? balance = message.Balance;
                            decimal num2 = 0M;
                            if (!(balance.GetValueOrDefault() == num2 & balance.HasValue))
                            {
                                num1 = 52;
                                goto label_7;
                            }
                        }
                        num1 = 50;
                    label_7:
                        return new char?((char)num1);
                    case OrderStates.Failed:
                        return new char?('8');
                    case OrderStates.Pending:
                        return new char?('A');
                }
            }
            return message.Error != null ? new char?('8') : new char?();
        }

        /// <summary>
        /// Convert <see cref="T:StockSharp.Fix.Native.OrdStatus" /> to <see cref="T:StockSharp.Messages.OrderStates" /> value.
        /// </summary>
        /// <param name="status">
        /// <see cref="T:StockSharp.Fix.Native.OrdStatus" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Messages.OrderStates" /> value.</returns>
        public static OrderStates FromFixStatus(this char status)
        {
            switch (status)
            {
                case '0':
                case '1':
                case '6':
                    return OrderStates.Active;
                case '2':
                case '4':
                case 'B':
                case 'C':
                    return OrderStates.Done;
                case '5':
                case 'D':
                    return OrderStates.Active;
                case '8':
                    return OrderStates.Failed;
                case 'A':
                case 'E':
                    return OrderStates.Pending;
                default:
                    throw new ArgumentOutOfRangeException("Invalid Status", status, LocalizedStrings.Str1598);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="message">The message containing the information for the order registration.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Fix.Native.FixTimeInForce" /> value.</returns>
        public static char GetFixTimeInForce(this OrderRegisterMessage message)
        {
            StockSharp.Messages.TimeInForce? tif = message != null ? message.TimeInForce : throw new ArgumentNullException("Invalide Message");
            if (tif.HasValue)
            {
                switch (tif.GetValueOrDefault())
                {
                    case Messages.TimeInForce.PutInQueue:
                        break;
                    case Messages.TimeInForce.MatchOrCancel:
                        return '4';
                    case Messages.TimeInForce.CancelBalance:
                        return '3';
                    default:
                        throw new ArgumentOutOfRangeException("Invalid TimeInForce", message.TimeInForce, LocalizedStrings.Str1599);
                }
            }
            if (!message.TillDate.HasValue)
                return '1';
            return message.TillDate.Value.IsToday() ? '0' : '6';
        }

        /// <summary>
        /// Convert <see cref="T:StockSharp.Messages.TimeInForce" /> to <see cref="T:StockSharp.Fix.Native.FixTimeInForce" /> value.
        /// </summary>
        /// <param name="tif">
        /// <see cref="T:StockSharp.Messages.TimeInForce" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Fix.Native.FixTimeInForce" /> value.</returns>
        public static char ToFix(this StockSharp.Messages.TimeInForce tif)
        {
            switch (tif)
            {
                case Messages.TimeInForce.PutInQueue:
                    return '1';
                case Messages.TimeInForce.MatchOrCancel:
                    return '4';
                case Messages.TimeInForce.CancelBalance:
                    return '3';
                default:
                    throw new ArgumentOutOfRangeException("Invalid TimeInForce", tif, LocalizedStrings.Str1219);
            }
        }

        /// <summary>
        /// Convert <see cref="T:StockSharp.Messages.SecurityTypes" /> to <see cref="T:StockSharp.Fix.Native.SecurityType" /> value.
        /// </summary>
        /// <param name="type">
        /// <see cref="T:StockSharp.Messages.SecurityTypes" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Fix.Native.SecurityType" /> value.</returns>
        public static string ToFix(this SecurityTypes type)
        {
            switch (type)
            {
                case SecurityTypes.Stock:
                    return SecurityType.CommonStock;

                case SecurityTypes.Future:
                    return SecurityType.Future;

                case SecurityTypes.Option:
                    return SecurityType.Option;

                case SecurityTypes.Index:
                    return SecurityType.IndexedLinked;

                case SecurityTypes.Currency:
                    return SecurityType.FXSpot;

                case SecurityTypes.Bond:
                    return SecurityType.BradyBond;

                case SecurityTypes.Warrant:
                    return SecurityType.Warrant;

                case SecurityTypes.Forward:
                    return SecurityType.Forward;

                case SecurityTypes.Swap:
                    return SecurityType.CreditDefaultSwap;

                case SecurityTypes.Commodity:
                    return "COMMODITY";

                case SecurityTypes.Cfd:
                    return SecurityType.FXForward;

                case SecurityTypes.News:
                    return SecurityType.Overnite;

                case SecurityTypes.Weather:
                    return SecurityType.LetterOfCredit;

                case SecurityTypes.Fund:
                    return SecurityType.MutualFund;

                case SecurityTypes.Adr:
                    return SecurityType.YankeeCertificateOfDeposit;

                case SecurityTypes.CryptoCurrency:
                    return "CryptoCurrency";

                case SecurityTypes.Etf:
                    return SecurityType.ForeignExchangeContract;

                case SecurityTypes.MultiLeg:
                    return SecurityType.MultilegInstrument;

                case SecurityTypes.Loan:
                    return SecurityType.SecuritiesLoan;

                case SecurityTypes.Spread:
                    return "Spread";

                case SecurityTypes.Gdr:
                    return SecurityType.EuroCommercialPaper;

                case SecurityTypes.Receipt:
                    return SecurityType.DepositNotes;

                case SecurityTypes.Indicator:
                    return SecurityType.ExtendedCommNote;

                case SecurityTypes.Strategy:
                    return "STRATEGY";

                case SecurityTypes.Volatility:
                    return "VOLA";

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, LocalizedStrings.Str1603);
            }
        }

        /// <summary>
        /// Convert <see cref="T:StockSharp.Fix.Native.SecurityType" /> to <see cref="T:StockSharp.Messages.SecurityTypes" /> value.
        /// </summary>
        /// <param name="type">
        /// <see cref="T:StockSharp.Fix.Native.SecurityType" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Messages.SecurityTypes" /> value.</returns>
        public static SecurityTypes? FromFixType(this string type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (type == SecurityType.CommonStock)
                return new SecurityTypes?(SecurityTypes.Stock);

            if (type == SecurityType.IndexedLinked)
                return new SecurityTypes?(SecurityTypes.Index);

            if (type == "Spread")
                return new SecurityTypes?(SecurityTypes.Spread);

            if (type == SecurityType.FXForward)
                return new SecurityTypes?(SecurityTypes.Cfd);

            if (type == SecurityType.CreditDefaultSwap)
                return new SecurityTypes?(SecurityTypes.Swap);

            if (type == SecurityType.ExtendedCommNote)
                return new SecurityTypes?(SecurityTypes.Indicator);

            if (type == SecurityType.Forward)
                return new SecurityTypes?(SecurityTypes.Forward);

            if (type == SecurityType.Option)
                return new SecurityTypes?(SecurityTypes.Option);

            if (type == SecurityType.DepositNotes)
                return new SecurityTypes?(SecurityTypes.Receipt);

            if (type == SecurityType.BradyBond)
                return new SecurityTypes?(SecurityTypes.Bond);

            if (type == SecurityType.EuroCommercialPaper)
                return new SecurityTypes?(SecurityTypes.Gdr);

            if (type == SecurityType.SecuritiesLoan)
                return new SecurityTypes?(SecurityTypes.Loan);

            if (type == "COMMODITY")
                return new SecurityTypes?(SecurityTypes.Commodity);

            if (type == SecurityType.Warrant)
                return new SecurityTypes?(SecurityTypes.Warrant);

            if (type == "VOLA")
                return new SecurityTypes?(SecurityTypes.Volatility);

            if (type == SecurityType.MultilegInstrument)
                return new SecurityTypes?(SecurityTypes.MultiLeg);

            if (type == SecurityType.FXSpot)
                return new SecurityTypes?(SecurityTypes.Currency);

            if (type == "STRATEGY")
                return new SecurityTypes?(SecurityTypes.Strategy);

            if (type == "CryptoCurrency")
                return new SecurityTypes?(SecurityTypes.CryptoCurrency);

            if (type == SecurityType.Overnite)
                return new SecurityTypes?(SecurityTypes.News);

            if (type == SecurityType.YankeeCertificateOfDeposit)
                return new SecurityTypes?(SecurityTypes.Adr);

            if (type == SecurityType.ForeignExchangeContract)
                return new SecurityTypes?(SecurityTypes.Etf);

            if (type == SecurityType.Future)
                return new SecurityTypes?(SecurityTypes.Future);

            if (type == SecurityType.LetterOfCredit)
                return new SecurityTypes?(SecurityTypes.Weather);

            return type.Iso10962ToSecurityType();
        }



        /// <summary>
        /// </summary>
        /// <param name="message">A message containing info about the order.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Fix.Native.SecurityType" /> value.</returns>
        public static string ToSecurityType(this OrderMessage message)
        {
            SecurityTypes? securityType = message.SecurityType;
            ref SecurityTypes? local = ref securityType;
            return !local.HasValue ? null : local.GetValueOrDefault().ToFix();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="change"></param>
        /// <param name="time"></param>
        /// <param name="dateTimeParser"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static MDEntry ToFix(this KeyValuePair<Level1Fields, object> change, DateTimeOffset time, FastDateTimeParser dateTimeParser)
        {
            char ch = change.Value != null ? change.Key.ToFix() : throw new ArgumentNullException("key is null");
            bool flag = false;
            Level1Fields key = change.Key;
            if (key <= Level1Fields.IssueSize)
            {
                if (key <= Level1Fields.AsksCount)
                {
                    if (key != Level1Fields.OpenInterest && (uint)(key - 13) > 3U)
                        goto label_18;
                }
                else if (key != Level1Fields.VolumeStep)
                {
                    switch (key - 29)
                    {
                        case Level1Fields.OpenPrice:
                        case Level1Fields.HighPrice:
                        case Level1Fields.BestBid:
                        case Level1Fields.ImpliedVolatility:
                        case Level1Fields.AsksVolume:
                        case Level1Fields.MarginBuy:
                            break;
                        case Level1Fields.LowPrice:
                        case Level1Fields.ClosePrice:
                        case Level1Fields.LastTrade:
                        case Level1Fields.StepPrice:
                        case Level1Fields.BestAsk:
                        case Level1Fields.TheorPrice:
                        case Level1Fields.OpenInterest:
                        case Level1Fields.MinPrice:
                        case Level1Fields.MaxPrice:
                        case Level1Fields.BidsVolume:
                        case Level1Fields.AsksCount:
                            goto label_18;
                        case Level1Fields.BidsCount:
                        case Level1Fields.Delta:
                        case Level1Fields.Gamma:
                            goto label_16;
                        case Level1Fields.HistoricalVolatility:
                        case Level1Fields.Vega:
                        case Level1Fields.Theta:
                            return new MDEntry()
                            {
                                Type = ch,
                                OtherValue = change.Value.To<string>(),
                                Time = time
                            };
                        default:
                            if (key == Level1Fields.IssueSize)
                                break;
                            goto label_18;
                    }
                }
            }
            else if (key <= Level1Fields.UnderlyingMinVolume)
            {
                if (key != Level1Fields.BuyBackDate)
                {
                    if ((uint)(key - 91) > 1U)
                        goto label_18;
                }
                else
                    goto label_16;
            }
            else if (key != Level1Fields.CouponDate)
            {
                if (key != Level1Fields.MaxVolume)
                {
                    if (key == Level1Fields.LastTradeStringId)
                        return new MDEntry()
                        {
                            Type = ch,
                            OtherValue = (string)change.Value,
                            Time = time
                        };
                    goto label_18;
                }
            }
            else
                goto label_16;
            flag = true;
            goto label_18;
        label_16:
            return new MDEntry()
            {
                Type = ch,
                OtherValue = dateTimeParser.ToString(((DateTimeOffset)change.Value).UtcDateTime),
                Time = time
            };
        label_18:
            MDEntry mdEntry = new MDEntry()
            {
                Type = ch,
                Time = time
            };
            decimal num = change.Value.To<decimal>();
            if (flag)
                mdEntry.Size = new decimal?(num);
            else
                mdEntry.Price = new decimal?(num);
            return mdEntry;
        }

        /// <summary>Register new candle type.</summary>
        /// <param name="code">
        /// <see cref="T:StockSharp.Fix.Native.MDEntry" /> value.</param>
        /// <param name="messageType">The type of candle message.</param>
        public static void RegisterCandleType(char code, Type messageType)
        {
            PairSet<char, Type> bfL5gO3IPnht1tWw = _charType;
            int num = code;
            Type type = messageType;
            if (type == null)
                throw new ArgumentNullException(nameof(messageType));
            bfL5gO3IPnht1tWw.Add((char)num, type);
        }

        /// <summary>Check the specified type is candle.</summary>
        /// <param name="mdEntryType">
        /// <see cref="T:StockSharp.Fix.Native.MDEntry" /> value.</param>
        /// <returns>Check result.</returns>
        public static bool IsCandleEntry(this char mdEntryType) => _charType.ContainsKey(mdEntryType);

        /// <summary>
        /// Convert <see cref="T:StockSharp.Fix.Native.MDEntryType" /> to <see cref="T:StockSharp.Messages.CandleMessage" /> value.
        /// </summary>
        /// <param name="entryType">
        /// <see cref="T:StockSharp.Fix.Native.MDEntryType" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Messages.CandleMessage" /> value.</returns>
        public static CandleMessage ToCandleMessage(this char entryType)
        {
            Type type;
            if (_charType.TryGetValue(entryType, out type))
                return type.CreateInstance<CandleMessage>();
            throw new ArgumentOutOfRangeException(nameof(entryType), entryType, LocalizedStrings.Str1018);
        }

        /// <summary>
        /// Write <see cref="F:StockSharp.Fix.Native.FixTags.OrderCapacity" /> and <see cref="F:StockSharp.Fix.Native.FixTags.OrderRestrictions" />.
        /// </summary>
        /// <param name="writer">Writer.</param>
        public static void WriteMarketMaker(this IFixWriter writer)
        {
            writer.Write(FixTags.OrderCapacity);
            writer.Write('P');
            writer.Write(FixTags.OrderRestrictions);
            writer.Write('5');
        }

        /// <summary>Is the order of market-maker.</summary>
        /// <param name="orderCapacity">
        /// <see cref="F:StockSharp.Fix.Native.FixTags.OrderCapacity" /> value.</param>
        /// <param name="orderRestrictions">
        /// <see cref="F:StockSharp.Fix.Native.FixTags.OrderRestrictions" /> value.</param>
        /// <returns>Check result.</returns>
        public static bool IsMarketMaker(char? orderCapacity, string orderRestrictions)
        {
            char? nullable1 = orderCapacity;
            int? nullable2 = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault()) : new int?();
            return nullable2.GetValueOrDefault() == 80 & nullable2.HasValue && orderRestrictions == "5";
        }

        /// <summary>
        /// Convert <see cref="T:StockSharp.Fix.Native.UserRequestType" /> to <see cref="T:StockSharp.Fix.FixUserRequestTypes" /> value.
        /// </summary>
        /// <param name="type">
        /// <see cref="T:StockSharp.Fix.Native.UserRequestType" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Fix.FixUserRequestTypes" /> value.</returns>
        public static FixUserRequestTypes ToUserRequestType(this UserRequestType type)
        {
            if (type == UserRequestType.LogOffUser)
                return FixUserRequestTypes.LogoffForce;
            if (type == UserRequestType.ChangePasswordForUser)
                return FixUserRequestTypes.ChangePassword;
            throw new ArgumentOutOfRangeException(nameof(type), type, LocalizedStrings.Str1219);
        }

        /// <summary>
        /// Convert <see cref="T:StockSharp.Fix.FixUserResponseTypes" /> to <see cref="T:StockSharp.Fix.Native.UserStatus" /> value.
        /// </summary>
        /// <param name="type">
        /// <see cref="T:StockSharp.Fix.FixUserResponseTypes" /> value.</param>
        /// <param name="error">
        /// </param>
        /// <param name="userName">
        /// </param>
        /// <param name="requestType">
        /// </param>
        /// <param name="text">
        /// </param>
        /// <returns>
        /// <see cref="T:StockSharp.Fix.Native.UserStatus" /> value.</returns>
        public static UserStatus ToUserStatus(
                                                  this FixUserResponseTypes type,
                                                  Exception error,
                                                  string userName,
                                                  UserRequestType requestType,
                                                  out string text
                                            )
        {
            text = error?.Message;
            switch (type)
            {
                case FixUserResponseTypes.PasswordChanged:
                    if (text.IsEmpty())
                        text = LocalizedStrings.Str3739;
                    return UserStatus.PasswordChanged;

                case FixUserResponseTypes.UserFound:
                    text = string.Empty;
                    return UserStatus.LoggedIn;

                case FixUserResponseTypes.UserLoggedOff:
                    if (text.IsEmpty())
                        text = LocalizedStrings.Str1639;
                    return UserStatus.ForcedUserLogoutByExchange;

                case FixUserResponseTypes.UserNotFound:
                    if (text.IsEmpty())
                        text = LocalizedStrings.Str1637Params.Put(userName);
                    return UserStatus.UserNotRecognised;

                case FixUserResponseTypes.UserBlocked:
                    if (text.IsEmpty())
                        text = LocalizedStrings.Str2581;
                    return UserStatus.Other;

                case FixUserResponseTypes.OldPasswordIncorrect:
                    if (text.IsEmpty())
                        text = LocalizedStrings.Str3738;
                    return UserStatus.PasswordIncorrect;

                case FixUserResponseTypes.NewPasswordIncorrect:
                    if (text.IsEmpty())
                        text = LocalizedStrings.Str3738;
                    return UserStatus.PasswordIncorrect;

                case FixUserResponseTypes.NotSupported:
                    if (text.IsEmpty())
                        text = LocalizedStrings.Str1640Params.Put(requestType);
                    return UserStatus.Other;

                case FixUserResponseTypes.UnknownError:
                    if (text.IsEmpty())
                        text = LocalizedStrings.UnknownServerError;
                    return UserStatus.Other;

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, LocalizedStrings.Str1219);
            }
        }

        /// <summary>
        /// Convert <see cref="T:StockSharp.Fix.Native.QuoteType" /> to <see cref="T:StockSharp.Messages.SecurityStates" /> value.
        /// </summary>
        /// <param name="type">
        /// <see cref="T:StockSharp.Fix.Native.QuoteType" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Messages.SecurityStates" /> value.</returns>
        public static SecurityStates? FromQuoteType(this int? type)
        {
            if (!type.HasValue)
                return new SecurityStates?();
            switch (type.GetValueOrDefault())
            {
                case 0:
                case 1:
                case 3:
                    return new SecurityStates?(SecurityStates.Trading);
                case 2:
                    return new SecurityStates?(SecurityStates.Stoped);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type.Value, LocalizedStrings.Str1219);
            }
        }

        /// <summary>
        /// Convert <see cref="T:StockSharp.Fix.Native.Urgency" /> to <see cref="T:StockSharp.Messages.NewsPriorities" /> value.
        /// </summary>
        /// <param name="urgency">
        /// <see cref="T:StockSharp.Fix.Native.Urgency" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Messages.NewsPriorities" /> value.</returns>
        public static NewsPriorities? ToNewsPriority(this char? urgency)
        {
            if (!urgency.HasValue)
                return new NewsPriorities?();
            switch (urgency.GetValueOrDefault())
            {
                case char.MinValue:
                    return new NewsPriorities?(NewsPriorities.Regular);
                case '\x0001':
                    return new NewsPriorities?(NewsPriorities.High);
                case '\x0002':
                    return new NewsPriorities?(NewsPriorities.Low);
                default:
                    return new NewsPriorities?();
            }
        }

        /// <summary>
        /// Write <see cref="F:StockSharp.Fix.Native.FixTags.HandlInst" />.
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="message">Message.</param>
        /// <param name="defaultValue">Default value.</param>
        public static void WriteHandlInst(
          this IFixWriter writer,
          OrderRegisterMessage message,
          char defaultValue = '1')
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            writer.Write(FixTags.HandlInst);
            IFixWriter fixWriter = writer;
            bool? isManual = message.IsManual;
            int num = isManual.GetValueOrDefault() & isManual.HasValue ? 51 : defaultValue;
            fixWriter.Write((char)num);
        }

        /// <summary>
        /// Write <see cref="F:StockSharp.Fix.Native.FixTags.Side" />.
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="side">Side.</param>
        public static void WriteSide(this IFixWriter writer, Sides side)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));
            writer.Write(FixTags.Side);
            writer.Write(side.ToFix());
        }

        /// <summary>
        /// Convert <see cref="T:StockSharp.Fix.Native.SubscriptionRequestType" /> to <see cref="T:System.Boolean" /> value.
        /// </summary>
        /// <param name="type">
        /// <see cref="T:StockSharp.Fix.Native.SubscriptionRequestType" /> value.</param>
        /// <returns>
        /// <see cref="T:System.Boolean" /> value.</returns>
        public static bool IsSubscribe(this char? type) => !type.HasValue || type.Value.IsSubscribe();

        /// <summary>
        /// Convert <see cref="T:StockSharp.Fix.Native.SubscriptionRequestType" /> to <see cref="T:System.Boolean" /> value.
        /// </summary>
        /// <param name="type">
        /// <see cref="T:StockSharp.Fix.Native.SubscriptionRequestType" /> value.</param>
        /// <returns>
        /// <see cref="T:System.Boolean" /> value.</returns>
        public static bool IsSubscribe(this char type) => type == '0' || type == '1';

        /// <summary>Get request id.</summary>
        /// <param name="msg">Subscription.</param>
        /// <returns>
        /// </returns>
        public static long GetRequestId(this ISubscriptionMessage msg)
        {
            if (msg == null)
                throw new ArgumentNullException(nameof(msg));
            return !msg.IsSubscribe ? msg.OriginalTransactionId : msg.TransactionId;
        }

        /// <summary>
        /// </summary>
        /// <param name="msg">Subscription.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Fix.Native.SubscriptionRequestType" /> value.</returns>
        public static char GetSubscriptionType(this ISubscriptionMessage msg)
        {
            if (msg == null)
                throw new ArgumentNullException(nameof(msg));
            if (!msg.IsSubscribe)
                return '2';
            return msg.To.HasValue ? '0' : '1';
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="tag"></param>
        /// <param name="getCondition"></param>
        /// <returns></returns>
        public static bool ReadOrderCondition(
          this IFixReader reader,
          FixTags tag,
          Func<OrderCondition> getCondition)
        {
            if (tag != FixTags.StopPx)
            {
                if (tag != FixTags.PegOffsetValue)
                    return false;
                ((FixOrderCondition)getCondition()).Offset = new decimal?(reader.ReadDecimal());
                return true;
            }
          ((FixOrderCondition)getCondition()).StopPrice = new decimal?(reader.ReadDecimal());
            return true;
        }

        /// <summary>
        /// Convert <see cref="T:StockSharp.Fix.Native.QuoteCondition" /> to <see cref="T:StockSharp.Messages.QuoteConditions" /> value.
        /// </summary>
        /// <param name="condition">
        /// <see cref="T:StockSharp.Fix.Native.QuoteCondition" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Messages.QuoteConditions" /> value.</returns>
        public static QuoteConditions ToQuoteCondition(this string condition)
        {
            if (condition.IsEmpty())
                return QuoteConditions.Active;
            switch (condition[0])
            {
                case 'A':
                    return QuoteConditions.Active;
                case 'I':
                    return QuoteConditions.Indicative;
                default:
                    throw new ArgumentOutOfRangeException(nameof(condition), condition, LocalizedStrings.Str1219);
            }
        }

        /// <summary>
        /// Convert <see cref="T:StockSharp.Messages.QuoteConditions" /> to <see cref="T:StockSharp.Fix.Native.QuoteCondition" /> value.
        /// </summary>
        /// <param name="condition">
        /// <see cref="T:StockSharp.Messages.QuoteConditions" /> value.</param>
        /// <param name="force">
        /// </param>
        /// <returns>
        /// <see cref="T:StockSharp.Fix.Native.QuoteCondition" /> value.</returns>
        public static char? ToNative(this QuoteConditions condition, bool force = false)
        {
            if (condition != QuoteConditions.Active)
            {
                if (condition == QuoteConditions.Indicative)
                    return new char?('I');
                throw new ArgumentOutOfRangeException(nameof(condition), condition, LocalizedStrings.Str1219);
            }
            return !force ? new char?() : new char?('A');
        }

        /// <summary>
        /// Convert <see cref="T:StockSharp.Messages.QuoteChangeActions" /> to <see cref="T:StockSharp.Fix.Native.MDUpdateAction" /> value.
        /// </summary>
        /// <param name="action">
        /// <see cref="T:StockSharp.Messages.QuoteChangeActions" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Fix.Native.MDUpdateAction" /> value.</returns>
        public static char ToNative(this QuoteChangeActions action)
        {
            switch (action)
            {
                case QuoteChangeActions.New:
                    return '0';
                case QuoteChangeActions.Update:
                    return '1';
                case QuoteChangeActions.Delete:
                    return '2';
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, LocalizedStrings.Str1219);
            }
        }

        /// <summary>
        /// Convert <see cref="T:StockSharp.Fix.Native.MDUpdateAction" /> to <see cref="T:StockSharp.Messages.QuoteChangeActions" /> value.
        /// </summary>
        /// <param name="action">
        /// <see cref="T:StockSharp.Fix.Native.MDUpdateAction" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Messages.QuoteChangeActions" /> value.</returns>
        public static QuoteChangeActions ToQuoteAction(this char action)
        {
            switch (action)
            {
                case '0':
                    return QuoteChangeActions.New;
                case '1':
                    return QuoteChangeActions.Update;
                case '2':
                case '3':
                case '4':
                case '5':
                    return QuoteChangeActions.Delete;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, LocalizedStrings.Str1219);
            }
        }

        /// <summary>Read time.</summary>
        /// <param name="reader">Reader.</param>
        /// <param name="parser">Time parser.</param>
        /// <returns>Time.</returns>
        public static DateTimeOffset ReadUtc(
          this IFixReader reader,
          FastDateTimeParser parser)
        {
            return reader.ReadDateTime(parser).ApplyUtc();
        }

        /// <summary>Write time.</summary>
        /// <param name="writer">Writer.</param>
        /// <param name="dto">Time.</param>
        /// <param name="parser">Time parser.</param>
        public static void WriteUtc(
          this IFixWriter writer,
          DateTimeOffset dto,
          FastDateTimeParser parser)
        {
            writer.Write(dto.UtcDateTime, parser);
        }

        /// <summary>
        /// Write <see cref="F:StockSharp.Fix.Native.FixTags.TransactTime" />.
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="parser">Time parser.</param>
        public static void WriteTransactTime(this IFixWriter writer, FastDateTimeParser parser)
        {
            writer.Write(FixTags.TransactTime);
            writer.Write(DateTime.UtcNow, parser);
        }

        /// <summary>
        /// Write <see cref="F:StockSharp.Fix.Native.FixTags.ExpireDate" />.
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="regMsg">The message containing the information for the order registration.</param>
        /// <param name="parser">Time parser.</param>
        /// <param name="timeZone">Time zone.</param>
        public static void WriteExpiryDate(
          this IFixWriter writer,
          OrderRegisterMessage regMsg,
          FastDateTimeParser parser,
          TimeZoneInfo timeZone)
        {
            DateTimeOffset? tillDate = regMsg.TillDate;
            if (!tillDate.HasValue)
                return;
            writer.Write(FixTags.ExpireDate);
            writer.Write(tillDate.Value.ToLocalTime(timeZone), parser);
        }

        /// <summary>
        /// Convert <see cref="T:StockSharp.Fix.Native.PositionEffect" /> to <see cref="T:StockSharp.Messages.OrderPositionEffects" /> value.
        /// </summary>
        /// <param name="effect">
        /// <see cref="T:StockSharp.Fix.Native.PositionEffect" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Messages.OrderPositionEffects" /> value.</returns>
        public static OrderPositionEffects? ToPositionEffect(this char? effect)
        {
            if (!effect.HasValue)
                return new OrderPositionEffects?();
            switch (effect.GetValueOrDefault())
            {
                case 'C':
                case 'N':
                    return new OrderPositionEffects?(OrderPositionEffects.CloseOnly);
                case 'D':
                    return new OrderPositionEffects?(OrderPositionEffects.Default);
                case 'F':
                case 'O':
                case 'R':
                    return new OrderPositionEffects?(OrderPositionEffects.OpenOnly);
                default:
                    throw new ArgumentOutOfRangeException(nameof(effect), effect, LocalizedStrings.Str1219);
            }
        }

        /// <summary>
        /// Write <see cref="F:StockSharp.Fix.Native.FixTags.PositionEffect" />.
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="effect">
        /// <see cref="T:StockSharp.Messages.OrderPositionEffects" /> value.</param>
        public static void WritePositionEffect(this IFixWriter writer, OrderPositionEffects? effect)
        {
            if (!effect.HasValue)
                return;
            char ch;
            switch (effect.GetValueOrDefault())
            {
                case OrderPositionEffects.Default:
                    ch = 'D';
                    break;
                case OrderPositionEffects.OpenOnly:
                    ch = 'O';
                    break;
                case OrderPositionEffects.CloseOnly:
                    ch = 'C';
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(effect), effect, LocalizedStrings.Str1219);
            }
            writer.Write(FixTags.PositionEffect);
            writer.Write(ch);
        }

        /// <summary>Convert string to dialect type.</summary>
        /// <param name="dialect">String value.</param>
        /// <param name="logs">Logs.</param>
        /// <returns>Dialect type.</returns>
        public static Type ToDialect(this string dialect, ILogReceiver logs)
        {
            if (logs == null)
                throw new ArgumentNullException(nameof(logs));

            dialect = dialect.Replace("StockSharp.Fix.Dialects.DefaultFixDialect, StockSharp.Fix,", "StockSharp.Fix.Dialects.DefaultFixDialect, StockSharp.Fix.Core,");


            try
            {
                return dialect.To<Type>();
            }
            catch (Exception ex)
            {
                logs.AddErrorLog(ex);
                return typeof(DefaultFixDialect);
            }
        }

        /// <summary>Convert dialect type to string.</summary>
        /// <param name="type">Dialect type.</param>
        /// <returns>String value.</returns>
        public static string FromDialect(this Type type) => !(type == null) ? type.To<string>() : throw new ArgumentNullException(nameof(type));

        /// <summary>
        /// </summary>
        /// <param name="message">
        /// </param>
        /// <param name="symbol">
        /// </param>
        /// <param name="board">
        /// </param>
        /// <param name="idSource">
        /// </param>
        /// <param name="idValue">
        /// </param>
        public static void InitSecId(
                                          this SecurityMessage message,
                                          string symbol,
                                          string board,
                                          string idSource,
                                          string idValue)
        {
            SecurityId securityId = new SecurityId()
            {
                SecurityCode = symbol,
                BoardCode = board
            };
            if (!(idSource == "1"))
            {
                if (!(idSource == "2"))
                {
                    if (!(idSource == "4"))
                    {
                        if (idSource == "5")
                            securityId.Ric = idValue;
                        else
                            securityId.Native = idValue;
                    }
                    else
                        securityId.Isin = idValue;
                }
                else
                    securityId.Sedol = idValue;
            }
            else
                securityId.Cusip = idValue;
            message.SecurityId = securityId;
        }

        /// <summary>
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <param name="convertToLatin">
        /// </param>
        /// <returns>
        /// </returns>
        public static string TryConvertToLatin(this string value, bool convertToLatin) => !convertToLatin ? value : value.ToLatin();

        /// <summary>
        /// </summary>
        /// <param name="message">
        /// </param>
        /// <param name="entryType">
        /// </param>
        /// <param name="price">
        /// </param>
        /// <param name="size">
        /// </param>
        /// <param name="otherValue">
        /// </param>
        /// <param name="dateTimeParser">
        /// </param>
        public static void FillLevel1(
          this Level1ChangeMessage message,
          char entryType,
          decimal? price,
          decimal? size,
          string otherValue,
          FastDateTimeParser dateTimeParser)
        {
            Level1Fields level1 = entryType.ToLevel1();
            object obj;
            switch (level1)
            {
                case Level1Fields.OpenInterest:
                case Level1Fields.BidsVolume:
                case Level1Fields.AsksVolume:
                case Level1Fields.VolumeStep:
                case Level1Fields.LastTradeVolume:
                case Level1Fields.Volume:
                case Level1Fields.BestBidVolume:
                case Level1Fields.BestAskVolume:
                case Level1Fields.Multiplier:
                case Level1Fields.IssueSize:
                case Level1Fields.MinVolume:
                case Level1Fields.UnderlyingMinVolume:
                case Level1Fields.MaxVolume:
                    obj = size.Value;
                    break;
                case Level1Fields.BidsCount:
                case Level1Fields.AsksCount:
                case Level1Fields.TradesCount:
                    obj = (int)size.Value;
                    break;
                case Level1Fields.State:
                    obj = (SecurityStates)(int)price.Value;
                    break;
                case Level1Fields.LastTradeTime:
                case Level1Fields.BestBidTime:
                case Level1Fields.BestAskTime:
                case Level1Fields.BuyBackDate:
                case Level1Fields.CouponDate:
                    obj = (DateTimeOffset)dateTimeParser.Parse(otherValue).UtcKind();
                    break;
                case Level1Fields.LastTradeId:
                    obj = otherValue.To<long>();
                    break;
                case Level1Fields.LastTradeUpDown:
                    obj = otherValue.To<bool>();
                    break;
                case Level1Fields.LastTradeOrigin:
                    obj = otherValue.To<Sides>();
                    break;
                case Level1Fields.LastTradeStringId:
                    obj = otherValue;
                    break;
                default:
                    obj = price.Value;
                    break;
            }
            message.Add<Level1ChangeMessage, Level1Fields>(level1, obj);
        }

        /// <summary>
        /// </summary>
        /// <param name="source">
        /// </param>
        /// <returns>
        /// </returns>
        public static bool IsDump(this ILogSource source)
        {
            LogLevels logLevel = source.GetLogLevel();
            if (logLevel == LogLevels.Inherit)
            {
                LogManager logManager = ServicesRegistry.LogManager;
                if (logManager != null)
                    logLevel = logManager.Application.LogLevel;
            }
            return logLevel == LogLevels.Verbose;
        }

        /// <summary>
        /// Convert <see cref="T:StockSharp.Messages.Level1Fields" /> to <see cref="T:StockSharp.Fix.Native.MDEntryType" /> value.
        /// </summary>
        /// <param name="field">
        /// <see cref="T:StockSharp.Messages.Level1Fields" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Fix.Native.MDEntryType" /> value.</returns>
        public static char ToFix(this Level1Fields field)
        {
            switch (field)
            {
                case Level1Fields.OpenPrice:
                    return '4';
                case Level1Fields.HighPrice:
                    return '7';
                case Level1Fields.LowPrice:
                    return '8';
                case Level1Fields.ClosePrice:
                    return '5';
                case Level1Fields.StepPrice:
                    return '$';
                case Level1Fields.ImpliedVolatility:
                    return '/';
                case Level1Fields.TheorPrice:
                    return '\\';
                case Level1Fields.OpenInterest:
                    return 'C';
                case Level1Fields.MinPrice:
                    return 'u';
                case Level1Fields.MaxPrice:
                    return 'y';
                case Level1Fields.BidsVolume:
                    return 'w';
                case Level1Fields.BidsCount:
                    return 'q';
                case Level1Fields.AsksVolume:
                    return 'e';
                case Level1Fields.AsksCount:
                    return 'r';
                case Level1Fields.HistoricalVolatility:
                    return '\x0080';
                case Level1Fields.Delta:
                    return '\x0081';
                case Level1Fields.Gamma:
                    return '\x0082';
                case Level1Fields.Vega:
                    return '\x0083';
                case Level1Fields.Theta:
                    return '\x0084';
                case Level1Fields.MarginBuy:
                    return '<';
                case Level1Fields.MarginSell:
                    return '>';
                case Level1Fields.PriceStep:
                    return '#';
                case Level1Fields.VolumeStep:
                    return '&';
                case Level1Fields.State:
                    return 'f';
                case Level1Fields.LastTradePrice:
                    return 'p';
                case Level1Fields.LastTradeVolume:
                    return 'v';
                case Level1Fields.Volume:
                    return 'B';
                case Level1Fields.AveragePrice:
                    return 'G';
                case Level1Fields.SettlementPrice:
                    return '6';
                case Level1Fields.Change:
                    return 't';
                case Level1Fields.BestBidPrice:
                    return 'b';
                case Level1Fields.BestBidVolume:
                    return 'j';
                case Level1Fields.BestAskPrice:
                    return 'c';
                case Level1Fields.BestAskVolume:
                    return 'm';
                case Level1Fields.Rho:
                    return '\x0085';
                case Level1Fields.AccruedCouponIncome:
                    return 's';
                case Level1Fields.HighBidPrice:
                    return 'N';
                case Level1Fields.LowAskPrice:
                    return 'O';
                case Level1Fields.Yield:
                    return 'o';
                case Level1Fields.LastTradeTime:
                    return 'P';
                case Level1Fields.TradesCount:
                    return 'd';
                case Level1Fields.VWAP:
                    return '9';
                case Level1Fields.LastTradeId:
                    return 'U';
                case Level1Fields.BestBidTime:
                    return 'h';
                case Level1Fields.BestAskTime:
                    return 'K';
                case Level1Fields.LastTradeUpDown:
                    return 'D';
                case Level1Fields.LastTradeOrigin:
                    return 'T';
                case Level1Fields.Multiplier:
                    return '@';
                case Level1Fields.PriceEarnings:
                    return '\x0086';
                case Level1Fields.ForwardPriceEarnings:
                    return '\x0087';
                case Level1Fields.PriceEarningsGrowth:
                    return '\x0088';
                case Level1Fields.PriceSales:
                    return '\x0089';
                case Level1Fields.PriceBook:
                    return '\x008A';
                case Level1Fields.PriceCash:
                    return '\x008B';
                case Level1Fields.PriceFreeCash:
                    return '\x008C';
                case Level1Fields.Payout:
                    return '\x008D';
                case Level1Fields.SharesOutstanding:
                    return '\x008E';
                case Level1Fields.SharesFloat:
                    return 'Z';
                case Level1Fields.FloatShort:
                    return '\x008F';
                case Level1Fields.ShortRatio:
                    return 'E';
                case Level1Fields.ReturnOnAssets:
                    return '\x0090';
                case Level1Fields.ReturnOnEquity:
                    return '\x0091';
                case Level1Fields.ReturnOnInvestment:
                    return '\x0092';
                case Level1Fields.CurrentRatio:
                    return '~';
                case Level1Fields.QuickRatio:
                    return '\x0093';
                case Level1Fields.LongTermDebtEquity:
                    return '\x0094';
                case Level1Fields.TotalDebtEquity:
                    return '\x0095';
                case Level1Fields.GrossMargin:
                    return 'L';
                case Level1Fields.OperatingMargin:
                    return 'M';
                case Level1Fields.ProfitMargin:
                    return '\x0096';
                case Level1Fields.Beta:
                    return '\x0097';
                case Level1Fields.AverageTrueRange:
                    return 'F';
                case Level1Fields.HistoricalVolatilityWeek:
                    return '\x0098';
                case Level1Fields.HistoricalVolatilityMonth:
                    return '\x0099';
                case Level1Fields.IsSystem:
                    return '\x009A';
                case Level1Fields.Decimals:
                    return '\x009B';
                case Level1Fields.Duration:
                    return 'A';
                case Level1Fields.IssueSize:
                    return 'k';
                case Level1Fields.BuyBackDate:
                    return 'Q';
                case Level1Fields.BuyBackPrice:
                    return 'g';
                case Level1Fields.Turnover:
                    return 'a';
                case Level1Fields.SpreadMiddle:
                    return 'H';
                case Level1Fields.Dividend:
                    return '(';
                case Level1Fields.AfterSplit:
                    return '\x009C';
                case Level1Fields.BeforeSplit:
                    return '\x009D';
                case Level1Fields.CommissionTaker:
                    return '\x009E';
                case Level1Fields.CommissionMaker:
                    return '\x009F';
                case Level1Fields.MinVolume:
                    return ' ';
                case Level1Fields.UnderlyingMinVolume:
                    return '¡';
                case Level1Fields.CouponValue:
                    return ')';
                case Level1Fields.CouponDate:
                    return 'l';
                case Level1Fields.CouponPeriod:
                    return '?';
                case Level1Fields.MarketPriceYesterday:
                    return 'Y';
                case Level1Fields.MarketPriceToday:
                    return '+';
                case Level1Fields.VWAPPrev:
                    return 'S';
                case Level1Fields.YieldVWAP:
                    return '-';
                case Level1Fields.YieldVWAPPrev:
                    return 'X';
                case Level1Fields.Index:
                    return '¢';
                case Level1Fields.Imbalance:
                    return '£';
                case Level1Fields.UnderlyingPrice:
                    return '¤';
                case Level1Fields.MaxVolume:
                    return '¥';
                case Level1Fields.LowBidPrice:
                    return '¦';
                case Level1Fields.HighAskPrice:
                    return '§';
                case Level1Fields.LastTradeVolumeLow:
                    return '¨';
                case Level1Fields.LastTradeVolumeHigh:
                    return '©';
                case Level1Fields.LowBidVolume:
                    return 'ª';
                case Level1Fields.HighAskVolume:
                    return '«';
                case Level1Fields.UnderlyingBestBidPrice:
                    return '¬';
                case Level1Fields.UnderlyingBestAskPrice:
                    return '\x00AD';
                case Level1Fields.MedianPrice:
                    return '®';
                case Level1Fields.HighPrice52Week:
                    return '¯';
                case Level1Fields.LowPrice52Week:
                    return '°';
                case Level1Fields.LastTradeStringId:
                    return '±';
                default:
                    throw new ArgumentOutOfRangeException(nameof(field), field, LocalizedStrings.Str1608);
            }
        }

        /// <summary>
        /// Convert <see cref="T:StockSharp.Fix.Native.MDEntryType" /> to <see cref="T:StockSharp.Messages.Level1Fields" /> value.
        /// </summary>
        /// <param name="entryType">
        /// <see cref="T:StockSharp.Fix.Native.MDEntryType" /> value.</param>
        /// <returns>
        /// <see cref="T:StockSharp.Messages.Level1Fields" /> value.</returns>
        public static Level1Fields ToLevel1(this char entryType)
        {
            switch (entryType)
            {
                case '#':
                    return Level1Fields.PriceStep;
                case '$':
                    return Level1Fields.StepPrice;
                case '&':
                    return Level1Fields.VolumeStep;
                case '(':
                    return Level1Fields.Dividend;
                case ')':
                    return Level1Fields.CouponValue;
                case '+':
                    return Level1Fields.MarketPriceToday;
                case '-':
                    return Level1Fields.YieldVWAP;
                case '/':
                    return Level1Fields.ImpliedVolatility;
                case '4':
                    return Level1Fields.OpenPrice;
                case '5':
                    return Level1Fields.ClosePrice;
                case '6':
                    return Level1Fields.SettlementPrice;
                case '7':
                    return Level1Fields.HighPrice;
                case '8':
                    return Level1Fields.LowPrice;
                case '9':
                    return Level1Fields.VWAP;
                case '<':
                    return Level1Fields.MarginBuy;
                case '>':
                    return Level1Fields.MarginSell;
                case '?':
                    return Level1Fields.CouponPeriod;
                case '@':
                    return Level1Fields.Multiplier;
                case 'A':
                    return Level1Fields.Duration;
                case 'B':
                    return Level1Fields.Volume;
                case 'C':
                    return Level1Fields.OpenInterest;
                case 'D':
                    return Level1Fields.LastTradeUpDown;
                case 'E':
                    return Level1Fields.ShortRatio;
                case 'F':
                    return Level1Fields.AverageTrueRange;
                case 'G':
                    return Level1Fields.AveragePrice;
                case 'H':
                    return Level1Fields.SpreadMiddle;
                case 'K':
                    return Level1Fields.BestAskTime;
                case 'L':
                    return Level1Fields.GrossMargin;
                case 'M':
                    return Level1Fields.OperatingMargin;
                case 'N':
                    return Level1Fields.HighBidPrice;
                case 'O':
                    return Level1Fields.LowAskPrice;
                case 'P':
                    return Level1Fields.LastTradeTime;
                case 'Q':
                    return Level1Fields.BuyBackDate;
                case 'S':
                    return Level1Fields.VWAPPrev;
                case 'T':
                    return Level1Fields.LastTradeOrigin;
                case 'U':
                    return Level1Fields.LastTradeId;
                case 'X':
                    return Level1Fields.YieldVWAPPrev;
                case 'Y':
                    return Level1Fields.MarketPriceYesterday;
                case 'Z':
                    return Level1Fields.SharesFloat;
                case '\\':
                    return Level1Fields.TheorPrice;
                case 'a':
                    return Level1Fields.Turnover;
                case 'b':
                    return Level1Fields.BestBidPrice;
                case 'c':
                    return Level1Fields.BestAskPrice;
                case 'd':
                    return Level1Fields.TradesCount;
                case 'e':
                    return Level1Fields.AsksVolume;
                case 'f':
                    return Level1Fields.State;
                case 'g':
                    return Level1Fields.BuyBackPrice;
                case 'h':
                    return Level1Fields.BestBidTime;
                case 'j':
                    return Level1Fields.BestBidVolume;
                case 'k':
                    return Level1Fields.IssueSize;
                case 'l':
                    return Level1Fields.CouponDate;
                case 'm':
                    return Level1Fields.BestAskVolume;
                case 'o':
                    return Level1Fields.Yield;
                case 'p':
                    return Level1Fields.LastTradePrice;
                case 'q':
                    return Level1Fields.BidsCount;
                case 'r':
                    return Level1Fields.AsksCount;
                case 's':
                    return Level1Fields.AccruedCouponIncome;
                case 't':
                    return Level1Fields.Change;
                case 'u':
                    return Level1Fields.MinPrice;
                case 'v':
                    return Level1Fields.LastTradeVolume;
                case 'w':
                    return Level1Fields.BidsVolume;
                case 'y':
                    return Level1Fields.MaxPrice;
                case '~':
                    return Level1Fields.CurrentRatio;
                case '\x0080':
                    return Level1Fields.HistoricalVolatility;
                case '\x0081':
                    return Level1Fields.Delta;
                case '\x0082':
                    return Level1Fields.Gamma;
                case '\x0083':
                    return Level1Fields.Vega;
                case '\x0084':
                    return Level1Fields.Theta;
                case '\x0085':
                    return Level1Fields.Rho;
                case '\x0086':
                    return Level1Fields.PriceEarnings;
                case '\x0087':
                    return Level1Fields.ForwardPriceEarnings;
                case '\x0088':
                    return Level1Fields.PriceEarningsGrowth;
                case '\x0089':
                    return Level1Fields.PriceSales;
                case '\x008A':
                    return Level1Fields.PriceBook;
                case '\x008B':
                    return Level1Fields.PriceCash;
                case '\x008C':
                    return Level1Fields.PriceFreeCash;
                case '\x008D':
                    return Level1Fields.Payout;
                case '\x008E':
                    return Level1Fields.SharesOutstanding;
                case '\x008F':
                    return Level1Fields.FloatShort;
                case '\x0090':
                    return Level1Fields.ReturnOnAssets;
                case '\x0091':
                    return Level1Fields.ReturnOnEquity;
                case '\x0092':
                    return Level1Fields.ReturnOnInvestment;
                case '\x0093':
                    return Level1Fields.QuickRatio;
                case '\x0094':
                    return Level1Fields.LongTermDebtEquity;
                case '\x0095':
                    return Level1Fields.TotalDebtEquity;
                case '\x0096':
                    return Level1Fields.ProfitMargin;
                case '\x0097':
                    return Level1Fields.Beta;
                case '\x0098':
                    return Level1Fields.HistoricalVolatilityWeek;
                case '\x0099':
                    return Level1Fields.HistoricalVolatilityMonth;
                case '\x009A':
                    return Level1Fields.IsSystem;
                case '\x009B':
                    return Level1Fields.Decimals;
                case '\x009C':
                    return Level1Fields.AfterSplit;
                case '\x009D':
                    return Level1Fields.BeforeSplit;
                case '\x009E':
                    return Level1Fields.CommissionTaker;
                case '\x009F':
                    return Level1Fields.CommissionMaker;
                case ' ':
                    return Level1Fields.MinVolume;
                case '¡':
                    return Level1Fields.UnderlyingMinVolume;
                case '¢':
                    return Level1Fields.Index;
                case '£':
                    return Level1Fields.Imbalance;
                case '¤':
                    return Level1Fields.UnderlyingPrice;
                case '¥':
                    return Level1Fields.MaxVolume;
                case '¦':
                    return Level1Fields.LowBidPrice;
                case '§':
                    return Level1Fields.HighAskPrice;
                case '¨':
                    return Level1Fields.LastTradeVolumeLow;
                case '©':
                    return Level1Fields.LastTradeVolumeHigh;
                case 'ª':
                    return Level1Fields.LowBidVolume;
                case '«':
                    return Level1Fields.HighAskVolume;
                case '¬':
                    return Level1Fields.UnderlyingBestBidPrice;
                case '\x00AD':
                    return Level1Fields.UnderlyingBestAskPrice;
                case '®':
                    return Level1Fields.MedianPrice;
                case '¯':
                    return Level1Fields.HighPrice52Week;
                case '°':
                    return Level1Fields.LowPrice52Week;
                case '±':
                    return Level1Fields.LastTradeStringId;
                default:
                    throw new ArgumentOutOfRangeException(nameof(entryType), entryType, LocalizedStrings.Str1018);
            }
        }

        /// <summary>
        /// Write <see cref="T:StockSharp.Messages.SecurityMessage" /> list.
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="dateParser">Time parser.</param>
        /// <param name="convertToLatin">Convert texts to latin.</param>
        /// <param name="requestId">
        /// <see cref="F:StockSharp.Fix.Native.FixTags.MDReqID" /> value.</param>
        /// <param name="responseId">
        /// <see cref="F:StockSharp.Fix.Native.FixTags.MDResponseID" /> value.</param>
        /// <param name="securityMessages">Securities.</param>
        /// <param name="lastFragment">Last message.</param>
        public static void WriteSecurityList(
                                                  this IFixWriter writer,
                                                  FastDateTimeParser dateParser,
                                                  bool convertToLatin,
                                                  string requestId,
                                                  string responseId,
                                                  ICollection<SecurityMessage> securityMessages,
                                                  bool lastFragment
                                            )
        {
            if (!requestId.IsEmpty())
            {
                writer.Write(FixTags.SecurityReqID);
                writer.Write(requestId);
            }
            if (!responseId.IsEmpty())
            {
                writer.Write(FixTags.SecurityResponseID);
                writer.Write(responseId);
            }
            writer.Write(FixTags.SecurityRequestResult);
            writer.Write(0);
            writer.Write(FixTags.LastFragment);
            writer.Write(lastFragment);
            if (securityMessages.Count == 0)
                return;
            writer.Write(FixTags.NoRelatedSym);
            writer.Write(securityMessages.Count);

            foreach (SecurityMessage securityMessage in securityMessages)
            {
                SecurityId securityId1 = securityMessage.SecurityId;
                writer.Write(FixTags.Symbol);
                writer.Write(securityId1.SecurityCode);
                writer.Write(FixTags.SecurityExchange);
                writer.Write(securityId1.BoardCode);
                if (!securityId1.Isin.IsEmpty())
                {
                    writer.Write(FixTags.SecurityID);
                    writer.Write(securityId1.Isin);
                    writer.Write(FixTags.IDSource);
                    writer.Write("4");
                }
                if (!securityMessage.Name.IsEmpty())
                {
                    writer.Write(FixTags.SecurityDesc);
                    writer.Write(securityMessage.Name.TryConvertToLatin(convertToLatin));
                }
                if (!securityMessage.Class.IsEmpty())
                {
                    writer.Write(FixTags.Product);
                    writer.Write(securityMessage.Class);
                }
                if (!securityMessage.CfiCode.IsEmpty())
                {
                    writer.Write(FixTags.CFICode);
                    writer.Write(securityMessage.CfiCode);
                }
                if (securityMessage.VolumeStep.HasValue)
                {
                    writer.Write(FixTags.RoundLot);
                    writer.Write(securityMessage.VolumeStep.Value);
                }
                if (securityMessage.PriceStep.HasValue)
                {
                    writer.Write(FixTags.MinTradeVol);
                    writer.Write(securityMessage.PriceStep.Value);
                }
                if (securityMessage.Multiplier.HasValue)
                {
                    writer.Write(FixTags.ContractMultiplier);
                    writer.Write(securityMessage.Multiplier.Value);
                }
                if (securityMessage.Decimals.HasValue)
                {
                    writer.Write(FixTags.NoInstrAttrib);
                    writer.Write(1);
                    writer.Write(FixTags.InstrAttribType);
                    writer.Write(27);
                    writer.Write(FixTags.InstrAttribValue);
                    writer.Write(securityMessage.Decimals.To<string>());
                }
                SecurityTypes? securityType = securityMessage.SecurityType;
                if (securityType.HasValue)
                {
                    writer.Write(FixTags.SecurityType);
                    IFixWriter fixWriter = writer;
                    securityType = securityMessage.SecurityType;
                    string fix = securityType.Value.ToFix();
                    fixWriter.Write(fix);
                }
                if (securityMessage.Currency.HasValue)
                {
                    writer.Write(FixTags.Currency);
                    writer.Write(securityMessage.Currency.Value.ToFix());
                }
                if (securityMessage.ExpiryDate.HasValue)
                {
                    writer.Write(FixTags.EndDate);
                    writer.WriteUtc(securityMessage.ExpiryDate.Value, dateParser);
                }
                if (securityMessage.SettlementDate.HasValue)
                {
                    writer.Write(FixTags.ContractSettlMonth);
                    writer.WriteUtc(securityMessage.SettlementDate.Value, dateParser);
                }
                if (!securityMessage.UnderlyingSecurityCode.IsEmpty())
                {
                    writer.Write(FixTags.SymbolSfx);
                    writer.Write(securityMessage.UnderlyingSecurityCode);
                }
                if (securityMessage.IssueDate.HasValue)
                {
                    writer.Write(FixTags.IssueDate);
                    writer.WriteUtc(securityMessage.IssueDate.Value, dateParser);
                }
                if (securityMessage.IssueSize.HasValue)
                {
                    writer.Write(FixTags.IssueSize);
                    writer.Write(securityMessage.IssueSize.Value);
                }
                if (securityMessage.MinVolume.HasValue)
                {
                    writer.Write(FixTags.MinQty);
                    writer.Write(securityMessage.MinVolume.Value);
                }
                if (securityMessage.MaxVolume.HasValue)
                {
                    writer.Write(FixTags.MaxTradeVol);
                    writer.Write(securityMessage.MaxVolume.Value);
                }
                if (securityMessage.Shortable.HasValue)
                {
                    writer.Write(FixTags.Shortable);
                    writer.Write(securityMessage.Shortable.Value);
                }
                securityType = securityMessage.SecurityType;
                SecurityTypes securityTypes = SecurityTypes.Option;
                if (securityType.GetValueOrDefault() == securityTypes & securityType.HasValue)
                {
                    if (securityMessage.Strike.HasValue)
                    {
                        writer.Write(FixTags.StrikePrice);
                        writer.Write(securityMessage.Strike.Value);
                    }
                    if (securityMessage.OptionType.HasValue)
                    {
                        writer.Write(FixTags.PutOrCall);
                        writer.Write(securityMessage.OptionType.Value.ToFix());
                    }
                }
                if (securityMessage.FaceValue.HasValue)
                {
                    writer.Write(FixTags.FaceValue);
                    writer.Write(securityMessage.FaceValue.Value);
                }
                if (securityMessage.IsBasket())
                {
                    writer.Write(FixTags.Formula);
                    writer.Write(string.Concat(securityMessage.BasketCode, "_", securityMessage.BasketExpression));
                }
                SecurityId primaryId = securityMessage.PrimaryId;
                SecurityId securityId2 = new SecurityId();
                SecurityId securityId3 = securityId2;
                if (primaryId != securityId3)
                {
                    writer.Write(FixTags.PrimaryCode);
                    IFixWriter fixWriter1 = writer;
                    securityId2 = securityMessage.PrimaryId;
                    string securityCode = securityId2.SecurityCode;
                    fixWriter1.Write(securityCode);
                    writer.Write(FixTags.PrimaryBoard);
                    IFixWriter fixWriter2 = writer;
                    securityId2 = securityMessage.PrimaryId;
                    string boardCode = securityId2.BoardCode;
                    fixWriter2.Write(boardCode);
                }
            }
        }

        /// <summary>
        /// Write <see cref="T:StockSharp.Algo.Strategies.Messages.StrategyTypeMessage" />.
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="message">Message.</param>
        public static void WriteStrategyType(this IFixWriter writer, StrategyTypeMessage message)
        {
            writer.Write(FixTags.Name);
            writer.Write(message.StrategyName);
            writer.Write(FixTags.StrategyTypeId);
            writer.Write(message.StrategyTypeId);
            if (message.OriginalTransactionId != 0L)
            {
                writer.Write(FixTags.MassStatusReqID);
                writer.Write(message.OriginalTransactionId);
            }
            byte[] assembly = message.Assembly;
            if ((assembly != null ? ((uint)assembly.Length > 0U ? 1 : 0) : 0) == 0)
                return;
            writer.Write(FixTags.RawDataLength);
            writer.Write(message.Assembly.Length);
            writer.Write(FixTags.RawData);
            writer.WriteBytes(message.Assembly, 0, message.Assembly.Length);
        }

        /// <summary>Write parameters.</summary>
        /// <param name="writer">Writer.</param>
        /// <param name="parameters">Parameters.</param>
        public static void WriteParameters(this IFixWriter writer, IDictionary<string, (string type, string value)> parameters)
        {
            writer.Write(FixTags.NoStrategyParameters);
            writer.Write(parameters.Count);
            if (parameters.Count == 0)
                return;
            foreach (KeyValuePair<string, (string type, string value)> parameter in parameters)
            {
                writer.Write(FixTags.StrategyParameterName);
                writer.Write(parameter.Key);
                writer.Write(FixTags.StrategyParameterType);
                writer.Write(parameter.Value.type);
                if (!parameter.Value.value.IsEmpty())
                {
                    writer.Write(FixTags.StrategyParameterValue);
                    writer.Write(parameter.Value.value);
                }
            }
        }

        /// <summary>
        /// Write <see cref="T:StockSharp.Algo.Strategies.Messages.StrategyInfoMessage" />.
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="message">Message.</param>
        /// <param name="dateParser">Time parser.</param>
        public static void WriteStrategyInfo(
                                                  this IFixWriter writer,
                                                  StrategyInfoMessage message,
                                                  FastDateTimeParser dateParser)
        {
            if (!message.Name.IsEmpty())
            {
                writer.Write(FixTags.Name);
                writer.Write(message.Name);
            }
            if (message.ProductId != 0L)
            {
                writer.Write(FixTags.Product);
                writer.Write(message.ProductId);
            }
            if (message.StrategyId != new Guid())
            {
                writer.Write(FixTags.ClientID);
                writer.Write(message.StrategyId.To<string>());
            }
            if (!message.Id.IsDefault<long>())
            {
                writer.Write(FixTags.Id);
                writer.Write(message.Id);
            }
            if (!message.CreationDate.IsDefault<DateTimeOffset>())
            {
                writer.Write(FixTags.IssueDate);
                writer.WriteUtc(message.CreationDate, dateParser);
            }
            writer.WriteParameters(message.Parameters);
            if (message.OriginalTransactionId == 0L)
                return;
            writer.Write(FixTags.MassStatusReqID);
            writer.Write(message.OriginalTransactionId);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="mdMsg"></param>
        /// <param name="requestId"></param>
        /// <param name="responseId"></param>
        /// <param name="dataBoundDateParser"></param>
        /// <param name="writeSecurityId"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void WriteMarketDataMessage(
                                                      this IFixWriter writer,
                                                      MarketDataMessage mdMsg,
                                                      string requestId,
                                                      string responseId,
                                                      FastDateTimeParser dataBoundDateParser,
                                                      Action<IFixWriter, MarketDataMessage> writeSecurityId)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));
            if (mdMsg == null)
                throw new ArgumentNullException(nameof(mdMsg));
            if (dataBoundDateParser == null)
                throw new ArgumentNullException(nameof(dataBoundDateParser));
            if (writeSecurityId == null)
                throw new ArgumentNullException(nameof(writeSecurityId));
            writer.Write(FixTags.MDReqID);
            writer.Write(requestId);
            if (!responseId.IsEmpty())
            {
                writer.Write(FixTags.MDResponseID);
                writer.Write(responseId);
            }
            writer.Write(FixTags.SubscriptionRequestType);
            writer.Write(mdMsg.GetSubscriptionType());
            if (mdMsg.IsSubscribe)
            {
                writer.Write(FixTags.MDUpdateType);
                writer.Write(1);
            }
            string mdEntryArg = null;
            char[] chArray;
            if (mdMsg.DataType == MarketDataTypes.MarketDepth)
            {
                writer.Write(FixTags.MarketDepth);
                writer.Write(mdMsg.MaxDepth.GetValueOrDefault());
                chArray = new char[2] { '0', '1' };
            }
            else
                chArray = new char[1]
                {
          mdMsg.DataType2.ToFixMDType(out mdEntryArg)
                };
            writer.Write(FixTags.NoMDEntryTypes);
            writer.Write(chArray.Length);
            foreach (char ch in chArray)
            {
                writer.Write(FixTags.MDEntryType);
                writer.Write(ch);
                if (!mdEntryArg.IsEmpty())
                {
                    writer.Write(FixTags.MDEntryArg);
                    writer.Write(mdEntryArg);
                }
            }
            if (mdMsg.DataType2.IsCandles)
            {
                if (mdMsg.AllowBuildFromSmallerTimeFrame)
                {
                    writer.Write(FixTags.AllowBuildFromSmallerTimeFrame);
                    writer.Write(mdMsg.AllowBuildFromSmallerTimeFrame);
                }
                if (mdMsg.IsCalcVolumeProfile)
                {
                    writer.Write(FixTags.CalcVolumeProfile);
                    writer.Write(mdMsg.IsCalcVolumeProfile);
                }
                if (mdMsg.IsFinishedOnly)
                {
                    writer.Write(FixTags.FinishedCandles);
                    writer.Write(mdMsg.IsFinishedOnly);
                }
            }
            if (mdMsg.DataType2 != DataType.News || mdMsg.SecurityId != new SecurityId())
            {
                writer.Write(FixTags.NoRelatedSym);
                writer.Write(1);
                writeSecurityId(writer, mdMsg);
            }
            DateTimeOffset? nullable1;
            if (mdMsg.From.HasValue)
            {
                writer.Write(FixTags.StartDate);
                IFixWriter writer1 = writer;
                nullable1 = mdMsg.From;
                DateTimeOffset dto = nullable1.Value;
                FastDateTimeParser parser = dataBoundDateParser;
                writer1.WriteUtc(dto, parser);
            }
            nullable1 = mdMsg.To;
            if (nullable1.HasValue)
            {
                writer.Write(FixTags.EndDate);
                IFixWriter writer1 = writer;
                nullable1 = mdMsg.To;
                DateTimeOffset dto = nullable1.Value;
                FastDateTimeParser parser = dataBoundDateParser;
                writer1.WriteUtc(dto, parser);
            }
            long? nullable2;
            if (mdMsg.Skip.HasValue)
            {
                writer.Write(FixTags.MarketDataSkip);
                IFixWriter fixWriter = writer;
                nullable2 = mdMsg.Skip;
                long num = nullable2.Value;
                fixWriter.Write(num);
            }
            nullable2 = mdMsg.Count;
            if (nullable2.HasValue)
            {
                writer.Write(FixTags.MarketDataCount);
                IFixWriter fixWriter = writer;
                nullable2 = mdMsg.Count;
                long num = nullable2.Value;
                fixWriter.Write(num);
            }
            if (mdMsg.BuildMode != MarketDataBuildModes.LoadAndBuild)
            {
                writer.Write(FixTags.MarketDataBuildMode);
                writer.Write((int)mdMsg.BuildMode);
            }
            if (mdMsg.BuildFrom != null)
            {
                writer.Write(FixTags.MarketDataBuildFrom);
                writer.Write(mdMsg.BuildFrom.ToFixMDType(out string _));
            }
            if (mdMsg.BuildField.HasValue)
            {
                writer.Write(FixTags.MarketDataBuildField);
                writer.Write(mdMsg.BuildField.Value.ToFix());
            }
            if (!mdMsg.IsRegularTradingHours)
                return;
            writer.Write(FixTags.RegularTradingHours);
            writer.Write(mdMsg.IsRegularTradingHours);
        }

        /// <summary>
        /// Write <see cref="T:StockSharp.Algo.Strategies.Messages.StrategyStateMessage" />.
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="message">Message.</param>
        public static void WriteStrategyState(this IFixWriter writer, StrategyStateMessage message)
        {
            if (!message.StrategyId.IsDefault<Guid>())
            {
                writer.Write(FixTags.ClientID);
                writer.Write(message.StrategyId.To<string>());
            }
            if (!message.StrategyTypeId.IsEmpty())
            {
                writer.Write(FixTags.StrategyTypeId);
                writer.Write(message.StrategyTypeId);
            }
            writer.WriteParameters(message.Statistics);
            if (message.OriginalTransactionId == 0L && message.TransactionId == 0L)
                return;
            if (message.OriginalTransactionId != 0L)
            {
                writer.Write(FixTags.MassStatusReqID);
                writer.Write(message.OriginalTransactionId);
            }
            else
            {
                writer.Write(FixTags.MDReqID);
                writer.Write(message.TransactionId);
            }
        }






        /// <summary>
        /// Write <see cref="T:StockSharp.Messages.UserInfoMessage" />.
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="message">Message.</param>
        /// <param name="dateParser">Time parser.</param>
        public static void WriteUserInfoMessage(
          this IFixWriter writer,
          UserInfoMessage message,
          FastDateTimeParser dateParser)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (!message.Login.IsEmpty())
            {
                writer.Write(FixTags.Username);
                writer.Write(message.Login);
            }
            if (!message.Password.IsEmpty())
            {
                writer.Write(FixTags.Password);
                writer.Write(message.Password.UnSecure());
            }
            if (message.IsBlocked)
            {
                writer.Write(FixTags.UserStatus);
                writer.Write(8);
            }
            IPAddress[] array1 = message.IpRestrictions.ToArray<IPAddress>();
            writer.Write(FixTags.NoIpRestrictions);
            writer.Write(array1.Length);
            foreach (IPAddress ipAddress in array1)
            {
                writer.Write(FixTags.IpRestrictions);
                writer.Write(ipAddress.To<string>());
            }
            KeyValuePair<UserPermissions, IDictionary<Tuple<string, string, object, DateTime?>, bool>>[] array2 = message.Permissions.ToArray<KeyValuePair<UserPermissions, IDictionary<Tuple<string, string, object, DateTime?>, bool>>>();
            writer.Write(FixTags.NoPermissions);
            writer.Write(array2.Length);
            foreach (KeyValuePair<UserPermissions, IDictionary<Tuple<string, string, object, DateTime?>, bool>> keyValuePair in array2)
            {
                writer.Write(FixTags.Permissions);
                writer.Write((int)keyValuePair.Key);
                writer.Write(FixTags.NoPermissionsValues);
                writer.Write(0);
            }
            long? nullable1;
            if (message.Id.HasValue)
            {
                writer.Write(FixTags.Id);
                IFixWriter fixWriter = writer;
                nullable1 = message.Id;
                long num = nullable1.Value;
                fixWriter.Write(num);
            }
            if (!message.DisplayName.IsEmpty())
            {
                writer.Write(FixTags.DisplayName);
                writer.Write(message.DisplayName);
            }
            if (!message.Phone.IsEmpty())
            {
                writer.Write(FixTags.Phone);
                writer.Write(message.Phone);
            }
            if (!message.Homepage.IsEmpty())
            {
                writer.Write(FixTags.Homepage);
                writer.Write(message.Homepage);
            }
            if (!message.Skype.IsEmpty())
            {
                writer.Write(FixTags.Skype);
                writer.Write(message.Skype);
            }
            if (!message.City.IsEmpty())
            {
                writer.Write(FixTags.City);
                writer.Write(message.City);
            }
            bool? nullable2;
            if (message.Gender.HasValue)
            {
                writer.Write(FixTags.Gender);
                IFixWriter fixWriter = writer;
                nullable2 = message.Gender;
                int num = nullable2.Value ? 1 : 0;
                fixWriter.Write(num != 0);
            }
            nullable2 = message.IsSubscription;
            if (nullable2.HasValue)
            {
                writer.Write(FixTags.Subscription);
                IFixWriter fixWriter = writer;
                nullable2 = message.IsSubscription;
                int num = nullable2.Value ? 1 : 0;
                fixWriter.Write(num != 0);
            }
            if (!message.Language.IsEmpty())
            {
                writer.Write(FixTags.Language);
                writer.Write(message.Language);
            }
            if (message.Balance.HasValue)
            {
                writer.Write(FixTags.TradeVolume);
                writer.Write(message.Balance.Value);
            }
            nullable1 = message.Avatar;
            if (nullable1.HasValue)
            {
                writer.Write(FixTags.Picture);
                IFixWriter fixWriter = writer;
                nullable1 = message.Avatar;
                long num = nullable1.Value;
                fixWriter.Write(num);
            }
            if (message.CreationDate.HasValue)
            {
                writer.Write(FixTags.IssueDate);
                writer.WriteUtc(message.CreationDate.Value, dateParser);
            }
            if (!message.AuthToken.IsEmpty())
            {
                writer.Write(FixTags.Token);
                writer.Write(message.AuthToken);
            }
            writer.Write(FixTags.PublishTrdIndicator);
            writer.Write(message.CanPublish);
            nullable2 = message.IsAgreementAccepted;
            if (nullable2.HasValue)
            {
                writer.Write(FixTags.AgreementID);
                IFixWriter fixWriter = writer;
                nullable2 = message.IsAgreementAccepted;
                int num = nullable2.Value ? 1 : 0;
                fixWriter.Write(num != 0);
            }
            if (message.UploadLimit != 0L)
            {
                writer.Write(FixTags.MaxFloor);
                writer.Write(message.UploadLimit);
            }
            if (message.Features.Length != 0)
            {
                writer.Write(FixTags.NoPartyIDs);
                writer.Write(message.Features.Length);
                foreach (string feature in message.Features)
                {
                    writer.Write(FixTags.PartyID);
                    writer.Write(feature);
                }
            }
            if (!message.IsTrialVerified)
                return;
            writer.Write(FixTags.TrialAllow);
            writer.Write(message.IsTrialVerified);
        }



        /// <summary>
        /// Write <see cref="T:StockSharp.Messages.ISubscriptionMessage" />.
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="subscription">Message.</param>
        /// <param name="parser">Time parser.</param>
        /// <param name="requestIdTag">Request id tag.</param>
        public static void WriteSubscriptionRequest(
          this IFixWriter writer,
          ISubscriptionMessage subscription,
          FastDateTimeParser parser,
          FixTags requestIdTag = FixTags.MDReqID)
        {
            if (subscription.TransactionId != 0L)
            {
                writer.Write(requestIdTag);
                writer.Write(subscription.TransactionId);
            }
            if (subscription.OriginalTransactionId != 0L)
            {
                writer.Write(FixTags.MDResponseID);
                writer.Write(subscription.OriginalTransactionId);
            }
            writer.Write(FixTags.SubscriptionRequestType);
            writer.Write(subscription.GetSubscriptionType());
            if (subscription.From.HasValue)
            {
                writer.Write(FixTags.StartDate);
                writer.WriteUtc(subscription.From.Value, parser);
            }
            if (subscription.To.HasValue)
            {
                writer.Write(FixTags.EndDate);
                writer.WriteUtc(subscription.To.Value, parser);
            }
            if (subscription.Skip.HasValue)
            {
                writer.Write(FixTags.MarketDataSkip);
                writer.Write(subscription.Skip.Value);
            }
            if (!subscription.Count.HasValue)
                return;
            writer.Write(FixTags.MarketDataCount);
            writer.Write(subscription.Count.Value);
        }

        /// <summary>
        /// Write <see cref="T:StockSharp.Messages.ISubscriptionMessage" />.
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="msg">Message.</param>
        public static void WriteSubscription(this IFixWriter writer, ISubscriptionMessage msg)
        {
            writer.Write(FixTags.MDReqID);
            writer.Write(msg.GetRequestId());
            writer.Write(FixTags.SubscriptionRequestType);
            writer.Write(msg.GetSubscriptionType());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCommandMessage"></typeparam>
        /// <param name="writer"></param>
        /// <param name="message"></param>
        /// <param name="parser"></param>
        /// <param name="writeTags"></param>
        public static void WriteCommand<TCommandMessage>(
          this IFixWriter writer,
          TCommandMessage message,
          FastDateTimeParser parser,
          Action<IFixWriter, TCommandMessage> writeTags = null)
          where TCommandMessage : CommandMessage
        {
            writer.WriteSubscriptionRequest(message, parser);
            writer.Write(FixTags.Command);
            writer.Write((int)message.Command);
            writer.Write(FixTags.Scope);
            writer.Write((int)message.Scope);
            if (!message.ObjectId.IsEmpty())
            {
                writer.Write(FixTags.Id);
                writer.Write(message.ObjectId);
            }
            writer.WriteParameters(message.Parameters);
            if (writeTags == null)
                return;
            writeTags(writer, message);
        }

        //public static void WriteFile<TFileMessage>(
        //  this IFixWriter writer,
        //  TFileMessage message,
        //  FastDateTimeParser parser,
        //  Action<IFixWriter, TFileMessage> writeTags = null )
        //  where TFileMessage : FileInfoMessage
        //{
        //    if ( message.TransactionId != 0L )
        //    {
        //        writer.Write( FixTags.MDReqID );
        //        writer.Write( message.TransactionId );
        //    }
        //    if ( !message.FileName.IsEmpty( ) )
        //    {
        //        writer.Write( FixTags.Name );
        //        writer.Write( message.FileName );
        //    }
        //    if ( message.Id != 0L )
        //    {
        //        writer.Write( FixTags.Id );
        //        writer.Write( message.Id );
        //    }
        //    if ( message.GroupId != 0L )
        //    {
        //        writer.Write( FixTags.GroupId );
        //        writer.Write( message.GroupId );
        //    }
        //    if ( message.IsPublic )
        //    {
        //        writer.Write( FixTags.Scope );
        //        writer.Write( message.IsPublic );
        //    }
        //    if ( !message.Url.IsEmpty( ) )
        //    {
        //        writer.Write( FixTags.URLLink );
        //        writer.Write( message.Url );
        //    }
        //    if ( !message.Hash.IsEmpty( ) )
        //    {
        //        writer.Write( FixTags.Hash );
        //        writer.Write( message.Hash );
        //    }
        //    if ( message.CreationDate != new DateTimeOffset( ) )
        //    {
        //        writer.Write( FixTags.MDEntryDate );
        //        writer.WriteUtc( message.CreationDate, parser );
        //    }
        //    if ( message.Body != null )
        //    {
        //        writer.Write( FixTags.RawDataLength );
        //        writer.Write( message.Body.Length );
        //        writer.Write( FixTags.RawData );
        //        writer.WriteBytes( message.Body, 0, message.Body.Length );
        //    }
        //    else if ( message.BodyLength > 0L )
        //    {
        //        writer.Write( FixTags.RawDataLength );
        //        writer.Write( message.BodyLength );
        //    }
        //    if ( writeTags == null )
        //        return;
        //    writeTags( writer, message );
        //}



        /// <summary>
        /// Write <see cref="T:StockSharp.Messages.DataType" />.
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="dataType">Data type info.</param>
        public static void WriteDataType(this IFixWriter writer, DataType dataType)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));
            if (dataType == null)
                throw new ArgumentNullException(nameof(dataType));
            writer.Write(FixTags.MDEntryType);
            string mdEntryArg;
            writer.Write(dataType.ToFixMDType(out mdEntryArg));
            if (mdEntryArg.IsEmpty())
                return;
            writer.Write(FixTags.MDEntryArg);
            writer.Write(mdEntryArg);
        }

        /// <summary>
        /// Write <see cref="P:StockSharp.Messages.IGeneratedMessage.BuildFrom" />.
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="buildFrom">
        /// <see cref="P:StockSharp.Messages.IGeneratedMessage.BuildFrom" />.</param>
        public static void WriteBuildFrom(this IFixWriter writer, DataType buildFrom)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));
            if (buildFrom == null)
                throw new ArgumentNullException(nameof(buildFrom));
            writer.Write(FixTags.BuildFromType);
            string mdEntryArg;
            writer.Write(buildFrom.ToFixMDType(out mdEntryArg));
            if (mdEntryArg.IsEmpty())
                return;
            writer.Write(FixTags.BuildFromArg);
            writer.Write(mdEntryArg);
        }

        
        //public static void WriteProductFeedback(
        //  this IFixWriter writer,
        //  ProductFeedbackMessage message,
        //  FastDateTimeParser parser )
        //{
        //    if ( message.TransactionId > 0L )
        //    {
        //        writer.Write( FixTags.MDReqID );
        //        writer.Write( message.TransactionId );
        //    }
        //    writer.Write( FixTags.Product );
        //    writer.Write( message.ProductId );
        //    writer.Write( FixTags.Id );
        //    writer.Write( message.Id );
        //    if ( !message.Text.IsEmpty( ) )
        //    {
        //        writer.Write( FixTags.Text );
        //        writer.Write( message.Text );
        //    }
        //    writer.Write( FixTags.Owner );
        //    writer.Write( message.Author );
        //    writer.Write( FixTags.Rating );
        //    writer.Write( message.Rating );
        //    writer.Write( FixTags.IssueDate );
        //    writer.WriteUtc( message.CreationDate, parser );
        //}



        ///// <summary>
        ///// Write <see cref="T:StockSharp.Community.ProductPermissionMessage" />.
        ///// </summary>
        ///// <param name="writer">Writer.</param>
        ///// <param name="message">Message.</param>
        //public static void WriteProductPermission(
        //  this IFixWriter writer,
        //  ProductPermissionMessage message )
        //{
        //    writer.Write( FixTags.Product );
        //    writer.Write( message.ProductId );
        //    writer.Write( FixTags.Username );
        //    writer.Write( message.UserId );
        //    writer.Write( FixTags.Owner );
        //    writer.Write( message.IsManager );
        //    if ( !message.Command.HasValue )
        //        return;
        //    writer.Write( FixTags.Command );
        //    writer.Write( ( int ) message.Command.Value );
        //}



        ///// <summary>
        ///// Write <see cref="T:StockSharp.Community.ProductCategoryMessage" />.
        ///// </summary>
        ///// <param name="writer">Writer.</param>
        ///// <param name="message">Message.</param>
        //public static void WriteProductCategory( this IFixWriter writer, ProductCategoryMessage message )
        //{
        //    writer.Write( FixTags.Id );
        //    writer.Write( message.Id );
        //    writer.Write( FixTags.Name );
        //    writer.Write( message.Name );
        //}



        /// <summary>
        /// Write <see cref="T:StockSharp.Messages.NewsMessage" />.
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="newsMsg">Message.</param>
        /// <param name="parser">Time parser.</param>
        public static void WriteNews(this IFixWriter writer, NewsMessage newsMsg, FastDateTimeParser parser)
        {
            writer.Write(FixTags.Headline);
            writer.Write(newsMsg.Headline);
            writer.Write(FixTags.OrigTime);
            writer.WriteUtc(newsMsg.ServerTime, parser);
            if (!newsMsg.Url.IsEmpty())
            {
                writer.Write(FixTags.URLLink);
                writer.Write(newsMsg.Url);
            }
            if (!newsMsg.Id.IsEmpty())
            {
                writer.Write(FixTags.MDEntryId);
                writer.Write(newsMsg.Id);
            }
            DateTimeOffset? expiryDate = newsMsg.ExpiryDate;
            if (expiryDate.HasValue)
            {
                writer.Write(FixTags.ExpireTime);
                expiryDate = newsMsg.ExpiryDate;

                writer.WriteUtc(expiryDate.Value, parser);
            }
            if (!newsMsg.Language.IsEmpty())
            {
                writer.Write(FixTags.Language);
                writer.Write(newsMsg.Language);
            }
            if (!newsMsg.Story.IsEmpty())
            {
                writer.Write(FixTags.Text);
                writer.Write(newsMsg.Story);
            }
            if (!newsMsg.Source.IsEmpty())
            {
                writer.Write(FixTags.IDSource);
                writer.Write(newsMsg.Source);
            }

            if (newsMsg.SecurityId.HasValue)
            {
                if (newsMsg.SecurityId.Value != new SecurityId())
                {
                    writer.Write(FixTags.NoRelatedSym);
                    writer.Write(1);
                    writer.Write(FixTags.Symbol);
                    writer.Write(newsMsg.SecurityId.Value.SecurityCode);
                    if (!newsMsg.SecurityId.Value.BoardCode.IsEmpty())
                    {
                        writer.Write(FixTags.SecurityExchange);
                        writer.Write(newsMsg.SecurityId.Value.BoardCode);
                    }
                }
            }

            if (newsMsg.Attachments.Length == 0)
                return;

            writer.Write(FixTags.NoUnderlyings);
            writer.Write(newsMsg.Attachments.Length);

            foreach (long attachment in newsMsg.Attachments)
            {
                writer.Write(FixTags.UnderlyingSymbol);
                writer.Write(attachment);
            }
        }

        
        //public static void WriteProductInfo( this IFixWriter writer, ProductInfoMessage msg, FastDateTimeParser parser )
        //{
        //    writer.Write( FixTags.Id );
        //    writer.Write( msg.Id );

        //    if ( !msg.Name.IsEmpty( ) )
        //    {
        //        writer.Write( FixTags.Name );
        //        writer.Write( msg.Name );
        //    }

        //    if ( !msg.Description.IsEmpty( ) )
        //    {
        //        writer.Write( FixTags.Text );
        //        writer.Write( msg.Description );
        //    }

        //    if ( !msg.PackageId.IsEmpty( ) )
        //    {
        //        writer.Write( FixTags.URLLink );
        //        writer.Write( msg.PackageId );
        //    }

        //    if ( !msg.Tags.IsEmpty( ) )
        //    {
        //        writer.Write( FixTags.RefTagID );
        //        writer.Write( msg.Tags );
        //    }

        //    writer.Write( FixTags.Owner );
        //    writer.Write( msg.Author );

        //    if ( msg.AnnualPrice != null )
        //    {
        //        writer.Write( FixTags.Price );
        //        writer.Write( msg.AnnualPrice.Value );
        //        writer.Write( FixTags.Currency );
        //        writer.Write( ( int ) msg.AnnualPrice.Type );
        //    }
        //    if ( msg.RenewPrice != null )
        //    {
        //        writer.Write( FixTags.Price2 );
        //        writer.Write( ( int ) msg.RenewPrice.Value );
        //        writer.Write( FixTags.Currency2 );
        //        writer.Write( ( int ) msg.RenewPrice.Type );
        //    }
        //    if ( msg.RenewMonthlyPrice != null )
        //    {
        //        writer.Write( FixTags.RenewMonthlyPrice );
        //        writer.Write( ( int ) msg.RenewMonthlyPrice.Value );
        //        writer.Write( FixTags.RenewMonthlyPriceCurrency );
        //        writer.Write( ( int ) msg.RenewMonthlyPrice.Type );
        //    }

        //    if ( msg.RenewAnnualPrice != null )
        //    {
        //        writer.Write( FixTags.RenewAnnualPrice );
        //        writer.Write( ( int ) msg.RenewAnnualPrice.Value );
        //        writer.Write( FixTags.RenewAnnualPriceCurrency );
        //        writer.Write( ( int ) msg.RenewAnnualPrice.Type );
        //    }

        //    writer.Write( FixTags.DownloadCount );
        //    writer.Write( msg.DownloadCount );

        //    if ( msg.Rating.HasValue )
        //    {
        //        writer.Write( FixTags.Rating );                                
        //        writer.Write( msg.Rating.Value );
        //    }

        //    if ( !msg.DocUrl.IsEmpty( ) )
        //    {
        //        writer.Write( FixTags.DocUrl );
        //        writer.Write( msg.DocUrl );
        //    }
        //    if ( !msg.Extra.IsEmpty( ) )
        //    {
        //        writer.Write( FixTags.Content );
        //        writer.Write( msg.Extra );
        //    }

        //    if ( msg.SupportedPlugins.HasValue )
        //    {
        //        writer.Write( FixTags.SupportedPlugins );                
        //        writer.Write( msg.SupportedPlugins.Value );
        //    }
        //    writer.Write( FixTags.ContentType );
        //    writer.Write( ( int ) msg.ContentType );
        //    writer.Write( FixTags.Picture );
        //    writer.Write( msg.Picture );
        //    writer.Write( FixTags.Repository );
        //    writer.Write( ( int ) msg.Repository );
        //    writer.Write( FixTags.Scope );
        //    writer.Write( ( int ) msg.Scope );

        //    if ( msg.MonthlyPrice != null )
        //    {
        //        writer.Write( FixTags.MonthlyPrice );
        //        writer.Write( ( int ) msg.MonthlyPrice.Value );
        //        writer.Write( FixTags.MonthlyPriceCurrency );
        //        writer.Write( ( int ) msg.MonthlyPrice.Type );
        //    }

        //    if ( msg.LifetimePrice != null )
        //    {
        //        writer.Write( FixTags.LifetimePrice );
        //        writer.Write( ( int ) msg.LifetimePrice.Value );
        //        writer.Write( FixTags.LifetimePriceCurrency );
        //        writer.Write( ( int ) msg.LifetimePrice.Type );
        //    }

        //    if ( !msg.LatestVersion.IsEmpty( ) )
        //    {
        //        writer.Write( FixTags.CstmApplVerID );
        //        writer.Write( msg.LatestVersion );
        //    }

        //    //Tuple<string, string>[] stubVersions = msg.StubVersions;

        //    if ( ( msg.StubVersions != null ? ( ( uint ) msg.StubVersions.Length > 0U ? 1 : 0 ) : 0 ) != 0 )
        //    {
        //        writer.Write( FixTags.NoStubCount );
        //        writer.Write( msg.StubVersions.Length );

        //        foreach ( var stubVersion in msg.StubVersions )
        //        {
        //            writer.Write( FixTags.RealVersion );
        //            writer.Write( stubVersion.Item1 );
        //            writer.Write( FixTags.StubVersion );
        //            writer.Write( stubVersion.Item2 );
        //        }
        //    }

        //    if ( !msg.Target.IsEmpty( ) )
        //    {
        //        writer.Write( FixTags.OrderCategory );
        //        writer.Write( msg.Target );
        //    }

        //    if ( msg.DiscountMonthlyPrice != null )
        //    {
        //        writer.Write( FixTags.DiscountMonthlyPrice );
        //        writer.Write( ( int ) msg.DiscountMonthlyPrice.Value );
        //        writer.Write( FixTags.DiscountMonthlyPriceCurrency );
        //        writer.Write( ( int ) msg.DiscountMonthlyPrice.Type );
        //    }

        //    if ( msg.DiscountAnnualPrice != null )
        //    {
        //        writer.Write( FixTags.DiscountAnnualPrice );
        //        writer.Write( msg.DiscountAnnualPrice.Value );
        //        writer.Write( FixTags.DiscountAnnualPriceCurrency );
        //        writer.Write( ( int ) msg.DiscountAnnualPrice.Type );
        //    }

        //    if ( msg.DiscountLifetimePrice != null )
        //    {
        //        writer.Write( FixTags.DiscountLifetimePrice );
        //        writer.Write( ( int ) msg.DiscountLifetimePrice.Value );
        //        writer.Write( FixTags.DiscountLifetimePriceCurrency );
        //        writer.Write( ( int ) msg.DiscountLifetimePrice.Type );
        //    }

        //    if ( msg.Flags != ProductInfoFlags.None )
        //    {
        //        writer.Write( FixTags.ExtendedTradeStatus );
        //        writer.Write( ( int ) msg.Flags );
        //    }

        //    if ( msg.PurchasedTill.HasValue )
        //    {
        //        writer.Write( FixTags.ExpireTime );                
        //        writer.WriteUtc( msg.PurchasedTill.Value, parser );
        //    }

        //    long[] categories = msg.Categories;
        //    if ( ( categories != null ? ( ( uint ) categories.Length > 0U ? 1 : 0 ) : 0 ) == 0 )
        //        return;

        //    foreach ( long category in msg.Categories )
        //    {
        //        writer.Write( FixTags.ProductCategories );
        //        writer.Write( category );
        //    }
        //}
    }
}
