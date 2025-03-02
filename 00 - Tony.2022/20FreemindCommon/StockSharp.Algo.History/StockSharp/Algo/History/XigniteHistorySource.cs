using Ecng.Common;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using XigniteCodeGen.Realtime;

namespace StockSharp.Algo.History
{
    public class XigniteHistorySource : BaseHistorySource, ISecurityDownloader
    {
        private readonly IExchangeInfoProvider iexchangeInfoProvider_0;
        private SecureString secureString_0;

        public XigniteHistorySource( IExchangeInfoProvider exchangeInfoProvider )
        {
            IExchangeInfoProvider exchangeInfoProvider1 = exchangeInfoProvider;
            if( exchangeInfoProvider1 == null )
            {
                throw new ArgumentNullException( nameof( exchangeInfoProvider ) );
            }

            this.iexchangeInfoProvider_0 = exchangeInfoProvider1;
        }

        public IExchangeInfoProvider ExchangeInfoProvider
        {
            get
            {
                return this.iexchangeInfoProvider_0;
            }
        }

        public string AuthToken
        {
            get
            {
                return this.secureString_0.To< string >( );
            }
            set
            {
                this.secureString_0 = value.To< SecureString >( );
            }
        }

        public IEnumerable< TimeFrameCandleMessage > GetCandles(
      StockSharp.BusinessEntities.Security security,
      TimeSpan timeFrame,
      DateTime beginDate,
      DateTime endDate )
        {
            XigniteHistorySource.Class150 class150 = new XigniteHistorySource.Class150( );
            class150.timeSpan_0 = timeFrame;
            if( security == null )
            {
                throw new ArgumentNullException( nameof( security ) );
            }

            if( beginDate > endDate )
            {
                throw new ArgumentOutOfRangeException( nameof( beginDate ), LocalizedStrings.Str1119Params.Put( ( object )beginDate, ( object )endDate ) );
            }

            class150.list_0 = new List< TimeFrameCandleMessage >( );
            class150.securityId_0 = security.ToSecurityId( ( SecurityIdGenerator ) null );
            XigniteGlobalRealTimeSoapClient realTimeSoapClient = new XigniteGlobalRealTimeSoapClient( );
            Header Header = new Header( )
      {
        Username = this.AuthToken
      };
            int Period;
            TickPrecisions tickPrecisions;
            if( class150.timeSpan_0.TotalMinutes < 1.0 )
            {
                Period = ( int ) class150.timeSpan_0.TotalSeconds;
                tickPrecisions = TickPrecisions.Seconds;
            }
            else if( class150.timeSpan_0.TotalHours < 1.0 )
            {
                Period = ( int ) class150.timeSpan_0.TotalMinutes;
                tickPrecisions = TickPrecisions.Minutes;
            }
            else
            {
                Period = ( int ) class150.timeSpan_0.TotalHours;
                tickPrecisions = TickPrecisions.Hours;
            }
            string Identifier;
            IdentifierTypes IdentifierType;
            if( !class150.securityId_0.Isin.IsEmpty( ) )
            {
                Identifier = class150.securityId_0.Isin;
                IdentifierType = IdentifierTypes.ISIN;
            }
            else if( !class150.securityId_0.Sedol.IsEmpty( ) )
            {
                Identifier = class150.securityId_0.Sedol;
                IdentifierType = IdentifierTypes.SEDOL;
            }
            else if( !class150.securityId_0.Cusip.IsEmpty( ) )
            {
                Identifier = class150.securityId_0.Cusip;
                IdentifierType = IdentifierTypes.CUSIP;
            }
            else
            {
                Identifier = class150.securityId_0.SecurityCode;
                IdentifierType = IdentifierTypes.Symbol;
            }
            class150.equityChartBars_0 = realTimeSoapClient.GetChartBars( Header, Identifier, IdentifierType, beginDate.ToString( "yyyy/MM/dd HH:mm", ( IFormatProvider ) CultureInfo.InvariantCulture ), endDate.ToString( "yyyy/MM/dd HH:mm", ( IFormatProvider ) CultureInfo.InvariantCulture ), tickPrecisions.To< string >( ), Period, "All", false );
            if( class150.equityChartBars_0 == null )
            {
                throw new InvalidOperationException( );
            }

            if( class150.equityChartBars_0.Outcome != OutcomeTypes.Success )
            {
                throw new InvalidOperationException( class150.equityChartBars_0.Message );
            }

            CultureInfo.InvariantCulture.DoInCulture( new Action( class150.method_0 ) );
            return ( IEnumerable< TimeFrameCandleMessage > ) class150.list_0;
        }

        public void Refresh(
      ISecurityStorage securityStorage,
      StockSharp.BusinessEntities.Security criteria,
      Action< StockSharp.BusinessEntities.Security > newSecurity,
      Func< bool > isCancelled )
        {
            if( securityStorage == null )
            {
                throw new ArgumentNullException( nameof( securityStorage ) );
            }

            if( criteria == null )
            {
                throw new ArgumentNullException( nameof( criteria ) );
            }

            string code = criteria.Board?.Code;
            XigniteGlobalRealTimeSoapClient realTimeSoapClient = new XigniteGlobalRealTimeSoapClient( );
            Header Header = new Header( )
      {
        Username = this.AuthToken
      };
            List< string > stringList = new List< string >( );
            if( code.IsEmpty( ) )
            {
                XigniteCodeGen.Realtime.ExchangeList exchangeList = realTimeSoapClient.ListExchanges( Header );
                if( exchangeList == null )
                {
                    throw new InvalidOperationException( );
                }

                if( exchangeList.Outcome != OutcomeTypes.Success )
                {
                    throw new InvalidOperationException( exchangeList.Message );
                }

                stringList.AddRange( ( ( IEnumerable< ExchangeDescription > ) exchangeList.ExchangeDescriptions ).Select< ExchangeDescription, string >( XigniteHistorySource.Class151.func_0 ?? ( XigniteHistorySource.Class151.func_0 = new Func< ExchangeDescription, string >( XigniteHistorySource.Class151.class151_0.method_0 ) ) ) );
            }
            else
            {
                stringList.Add( code );
            }

            foreach( string str in stringList )
            {
                SymbolList symbolList = realTimeSoapClient.ListSymbols( Header, str, string.Empty, string.Empty );
                if( symbolList == null )
                {
                    throw new InvalidOperationException( );
                }

                SecurityIdGenerator securityIdGenerator = new SecurityIdGenerator( );
                if( symbolList.Outcome != OutcomeTypes.Success )
                {
                    throw new InvalidOperationException( symbolList.Message );
                }

                foreach( SecurityDescription securityDescription in symbolList.SecurityDescriptions )
                {
                    string symbol = securityDescription.Symbol;
                    if( criteria.Code.IsEmpty( ) || symbol.ContainsIgnoreCase( criteria.Code ) )
                    {
                        string id = securityIdGenerator.GenerateId( securityDescription.Symbol, str );
                        if( securityStorage.LookupById( id ) == null )
                        {
                            StockSharp.BusinessEntities.Security security = new StockSharp.BusinessEntities.Security( )
              {
                Code = symbol,
                Id = id,
                Name = securityDescription.Name,
                Currency = securityDescription.Currency.FromMicexCurrencyName( ( Action< Exception > ) null ),
                Board = this.ExchangeInfoProvider.GetOrCreateBoard( str, ( Func< string, ExchangeBoard > ) null )
              };
                            newSecurity( security );
                            securityStorage.Save( security, false );
                        }
                    }
                }
            }
        }

        private sealed class Class150
        {
            public EquityChartBars equityChartBars_0;
            public List< TimeFrameCandleMessage > list_0;
            public SecurityId securityId_0;
            public TimeSpan timeSpan_0;

            internal void method_0( )
            {
                foreach( ChartBar chartBar in this.equityChartBars_0.ChartBars )
                {
                    DateTimeOffset dateTimeOffset = chartBar.StartDate.ToDateTime( "yyyy/MM/dd", ( CultureInfo ) null ).ApplyTimeZone( TimeSpan.FromHours( chartBar.UTCOffset ) ) + chartBar.StartTime.ToTimeSpan( "hh\\:mm", ( CultureInfo ) null );
                    List< TimeFrameCandleMessage > list0 = this.list_0;
                    TimeFrameCandleMessage frameCandleMessage = new TimeFrameCandleMessage( );
                    frameCandleMessage.SecurityId = this.securityId_0;
                    frameCandleMessage.TimeFrame = this.timeSpan_0;
                    frameCandleMessage.OpenTime = dateTimeOffset;
                    frameCandleMessage.OpenPrice = ( Decimal ) chartBar.Open;
                    frameCandleMessage.HighPrice = ( Decimal ) chartBar.High;
                    frameCandleMessage.LowPrice = ( Decimal ) chartBar.Low;
                    frameCandleMessage.ClosePrice = ( Decimal ) chartBar.Close;
                    frameCandleMessage.TotalVolume = ( Decimal ) chartBar.Volume;
                    frameCandleMessage.TotalTicks = chartBar.Trades == 0 ? new int?( ) : new int?( chartBar.Trades );
                    frameCandleMessage.State = CandleStates.Finished;
                    list0.Add( frameCandleMessage );
                }
            }
        }

        [Serializable]
    private sealed class Class151
    {
        public static readonly XigniteHistorySource.Class151 class151_0 = new XigniteHistorySource.Class151( );
        public static Func< ExchangeDescription, string > func_0;

        internal string method_0( ExchangeDescription exchangeDescription_0 )
        {
            return exchangeDescription_0.MarketIdentificationCode;
        }
    }
    }
}
