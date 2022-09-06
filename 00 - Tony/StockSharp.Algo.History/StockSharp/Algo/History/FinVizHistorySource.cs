using Ecng.Collections;
using Ecng.Common;
using Ecng.Net;
using Ecng.Web;
using Microsoft.VisualBasic.FileIO;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using xNet;

namespace StockSharp.Algo.History
{
    public class FinVizHistorySource : BaseHistorySource, ISecurityDownloader
    {
        private static readonly Dictionary< FinVizColumns, Type > dictionary_0 = new Dictionary< FinVizColumns, Type >( )
    {
      {
        FinVizColumns.Ticker,
        typeof( string )
      },
      {
        FinVizColumns.Company,
        typeof( string )
      },
      {
        FinVizColumns.Sector,
        typeof( string )
      },
      {
        FinVizColumns.Industry,
        typeof( string )
      },
      {
        FinVizColumns.Country,
        typeof( string )
      },
      {
        FinVizColumns.MarketCap,
        typeof( Decimal )
      },
      {
        FinVizColumns.PE,
        typeof( Decimal )
      },
      {
        FinVizColumns.ForwardPE,
        typeof( Decimal )
      },
      {
        FinVizColumns.PEG,
        typeof( Decimal )
      },
      {
        FinVizColumns.PS,
        typeof( Decimal )
      },
      {
        FinVizColumns.PB,
        typeof( Decimal )
      },
      {
        FinVizColumns.PCash,
        typeof( Decimal )
      },
      {
        FinVizColumns.PFreeCash,
        typeof( Decimal )
      },
      {
        FinVizColumns.DividendYield,
        typeof( Decimal )
      },
      {
        FinVizColumns.PayoutRatio,
        typeof( Decimal )
      },
      {
        FinVizColumns.EpsTtm,
        typeof( Decimal )
      },
      {
        FinVizColumns.EpsGrowthThisYear,
        typeof( Decimal )
      },
      {
        FinVizColumns.EpsGrowthNextYear,
        typeof( Decimal )
      },
      {
        FinVizColumns.EpsGrowthPast5Years,
        typeof( Decimal )
      },
      {
        FinVizColumns.EpsGrowthNext5Years,
        typeof( Decimal )
      },
      {
        FinVizColumns.SalesGrowthPast5Years,
        typeof( Decimal )
      },
      {
        FinVizColumns.EpsGrowthQuarterOverQuarter,
        typeof( Decimal )
      },
      {
        FinVizColumns.SalesGrowthQuarterOverQuarter,
        typeof( Decimal )
      },
      {
        FinVizColumns.SharesOutstanding,
        typeof( Decimal )
      },
      {
        FinVizColumns.SharesFloat,
        typeof( Decimal )
      },
      {
        FinVizColumns.InsiderOwnership,
        typeof( Decimal )
      },
      {
        FinVizColumns.InsiderTransactions,
        typeof( Decimal )
      },
      {
        FinVizColumns.InstitutionalOwnership,
        typeof( Decimal )
      },
      {
        FinVizColumns.InstitutionalTransactions,
        typeof( Decimal )
      },
      {
        FinVizColumns.FloatShort,
        typeof( Decimal )
      },
      {
        FinVizColumns.ShortRatio,
        typeof( Decimal )
      },
      {
        FinVizColumns.ReturnOnAssets,
        typeof( Decimal )
      },
      {
        FinVizColumns.ReturnOnEquity,
        typeof( Decimal )
      },
      {
        FinVizColumns.ReturnOnInvestment,
        typeof( Decimal )
      },
      {
        FinVizColumns.CurrentRatio,
        typeof( Decimal )
      },
      {
        FinVizColumns.QuickRatio,
        typeof( Decimal )
      },
      {
        FinVizColumns.LtDebtEquity,
        typeof( Decimal )
      },
      {
        FinVizColumns.TotalDebtEquity,
        typeof( Decimal )
      },
      {
        FinVizColumns.GrossMargin,
        typeof( Decimal )
      },
      {
        FinVizColumns.OperatingMargin,
        typeof( Decimal )
      },
      {
        FinVizColumns.ProfitMargin,
        typeof( Decimal )
      },
      {
        FinVizColumns.PerformanceWeek,
        typeof( Decimal )
      },
      {
        FinVizColumns.PerformanceMonth,
        typeof( Decimal )
      },
      {
        FinVizColumns.PerformanceQuarter,
        typeof( Decimal )
      },
      {
        FinVizColumns.PerformanceHalfYear,
        typeof( Decimal )
      },
      {
        FinVizColumns.PerformanceYear,
        typeof( Decimal )
      },
      {
        FinVizColumns.PerformanceYtd,
        typeof( Decimal )
      },
      {
        FinVizColumns.Beta,
        typeof( Decimal )
      },
      {
        FinVizColumns.AverageTrueRange,
        typeof( Decimal )
      },
      {
        FinVizColumns.VolatilityWeek,
        typeof( Decimal )
      },
      {
        FinVizColumns.VolatilityMonth,
        typeof( Decimal )
      },
      {
        FinVizColumns.Sma20Days,
        typeof( Decimal )
      },
      {
        FinVizColumns.Sma50Days,
        typeof( Decimal )
      },
      {
        FinVizColumns.Sma200Days,
        typeof( Decimal )
      },
      {
        FinVizColumns.High50Days,
        typeof( Decimal )
      },
      {
        FinVizColumns.Low50Days,
        typeof( Decimal )
      },
      {
        FinVizColumns.High52Weeks,
        typeof( Decimal )
      },
      {
        FinVizColumns.Low52Weeks,
        typeof( Decimal )
      },
      {
        FinVizColumns.Rsi14,
        typeof( Decimal )
      },
      {
        FinVizColumns.ChangeFromOpen,
        typeof( Decimal )
      },
      {
        FinVizColumns.Gap,
        typeof( Decimal )
      },
      {
        FinVizColumns.AnalystRecom,
        typeof( Decimal )
      },
      {
        FinVizColumns.AverageVolume,
        typeof( Decimal )
      },
      {
        FinVizColumns.RelativeVolume,
        typeof( Decimal )
      },
      {
        FinVizColumns.Price,
        typeof( Decimal )
      },
      {
        FinVizColumns.Change,
        typeof( Decimal )
      },
      {
        FinVizColumns.Volume,
        typeof( Decimal )
      },
      {
        FinVizColumns.EarningsDate,
        typeof( DateTime )
      }
    };
        private static readonly Dictionary< FinVizColumns, Level1Fields > dictionary_1 = new Dictionary< FinVizColumns, Level1Fields >( )
    {
      {
        FinVizColumns.PE,
        Level1Fields.PriceEarnings
      },
      {
        FinVizColumns.ForwardPE,
        Level1Fields.ForwardPriceEarnings
      },
      {
        FinVizColumns.PEG,
        Level1Fields.PriceEarningsGrowth
      },
      {
        FinVizColumns.PS,
        Level1Fields.PriceSales
      },
      {
        FinVizColumns.PB,
        Level1Fields.PriceBook
      },
      {
        FinVizColumns.PCash,
        Level1Fields.PriceCash
      },
      {
        FinVizColumns.PFreeCash,
        Level1Fields.PriceFreeCash
      },
      {
        FinVizColumns.DividendYield,
        Level1Fields.Yield
      },
      {
        FinVizColumns.PayoutRatio,
        Level1Fields.Payout
      },
      {
        FinVizColumns.SharesOutstanding,
        Level1Fields.SharesOutstanding
      },
      {
        FinVizColumns.SharesFloat,
        Level1Fields.SharesFloat
      },
      {
        FinVizColumns.FloatShort,
        Level1Fields.FloatShort
      },
      {
        FinVizColumns.ShortRatio,
        Level1Fields.ShortRatio
      },
      {
        FinVizColumns.ReturnOnAssets,
        Level1Fields.ReturnOnAssets
      },
      {
        FinVizColumns.ReturnOnEquity,
        Level1Fields.ReturnOnEquity
      },
      {
        FinVizColumns.ReturnOnInvestment,
        Level1Fields.ReturnOnInvestment
      },
      {
        FinVizColumns.CurrentRatio,
        Level1Fields.CurrentRatio
      },
      {
        FinVizColumns.QuickRatio,
        Level1Fields.QuickRatio
      },
      {
        FinVizColumns.LtDebtEquity,
        Level1Fields.LongTermDebtEquity
      },
      {
        FinVizColumns.TotalDebtEquity,
        Level1Fields.TotalDebtEquity
      },
      {
        FinVizColumns.GrossMargin,
        Level1Fields.GrossMargin
      },
      {
        FinVizColumns.OperatingMargin,
        Level1Fields.OperatingMargin
      },
      {
        FinVizColumns.ProfitMargin,
        Level1Fields.ProfitMargin
      },
      {
        FinVizColumns.Beta,
        Level1Fields.Beta
      },
      {
        FinVizColumns.AverageTrueRange,
        Level1Fields.AverageTrueRange
      },
      {
        FinVizColumns.VolatilityWeek,
        Level1Fields.HistoricalVolatilityWeek
      },
      {
        FinVizColumns.VolatilityMonth,
        Level1Fields.HistoricalVolatilityMonth
      },
      {
        FinVizColumns.Price,
        Level1Fields.LastTradePrice
      },
      {
        FinVizColumns.Change,
        Level1Fields.Change
      },
      {
        FinVizColumns.Volume,
        Level1Fields.Volume
      }
    };
        private readonly IExchangeInfoProvider iexchangeInfoProvider_0;

        public FinVizHistorySource( IExchangeInfoProvider exchangeInfoProvider )
        {
            if( exchangeInfoProvider == null )
            {
                throw new ArgumentNullException( nameof( exchangeInfoProvider ) );
            }

            this.iexchangeInfoProvider_0 = exchangeInfoProvider;
        }

        public IExchangeInfoProvider ExchangeInfoProvider
        {
            get
            {
                return this.iexchangeInfoProvider_0;
            }
        }

        public IDictionary< Security, Level1ChangeMessage > LoadChanges(
          IEnumerable< Security > securities )
        {
            FinVizHistorySource.Class40 class40 = new FinVizHistorySource.Class40( );
            if( securities == null )
            {
                throw new ArgumentNullException( nameof( securities ) );
            }

            class40.dictionary_0 = securities.ToDictionary< Security, string, Security >( new Func< Security, string >( this.GetSecurityCode ), FinVizHistorySource.Class41.func_0 ?? ( FinVizHistorySource.Class41.func_0 = new Func< Security, Security >( FinVizHistorySource.Class41.class41_0.method_0 ) ) );
            Url url = new Url( "http://finviz.com/export.ashx?v=151&c=1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68&" );
            url.QueryString[ "t" ] = class40.dictionary_0.Keys.Join( "," );
            using( HttpRequest httpRequest = new HttpRequest( ) )
            {
                FinVizHistorySource.Class39 class39 = new FinVizHistorySource.Class39( );
                class39.class40_0 = class40;
                httpRequest.UserAgent = Http.ChromeUserAgent( );
                httpRequest.Referer = "http://finviz.com";
                class39.string_0 = Encoding.UTF8.GetString( httpRequest.Get( url, null ).ToBytes( ) );
                class39.dictionary_0 = new Dictionary< Security, Level1ChangeMessage >( );
                CultureInfo.InvariantCulture.DoInCulture( new Action( class39.method_0 ) );
                return class39.dictionary_0;
            }
        }

        private static List< string[ ] > smethod_0( string string_0 )
        {
            List< string[ ] > strArrayList = new List< string[ ] >( );
            using( TextFieldParser textFieldParser = new TextFieldParser( new StringReader( string_0 ) ) )
            {
                textFieldParser.Delimiters = new string[ 1 ] { "," };
                textFieldParser.TrimWhiteSpace = true;
                textFieldParser.HasFieldsEnclosedInQuotes = true;
                string[ ] strArray;
                while( ( strArray = textFieldParser.ReadFields( ) ) != null )
                {
                    strArrayList.Add( strArray );
                }
            }
            int num = typeof( FinVizColumns ).GetNames( ).Count< string >( );
            foreach( string[ ] strArray in strArrayList )
            {
                if( strArray.Length != num )
                {
                    throw new InvalidOperationException( LocalizedStrings.Str2076Params.Put( strArray.Length, num ) );
                }
            }
            return strArrayList;
        }

        public void Refresh(
          ISecurityStorage securityStorage,
          Security criteria,
          Action< Security > newSecurity,
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

            if( newSecurity == null )
            {
                throw new ArgumentNullException( nameof( newSecurity ) );
            }

            if( isCancelled == null )
            {
                throw new ArgumentNullException( nameof( isCancelled ) );
            }

            using( HttpRequest httpRequest = new HttpRequest( ) )
            {
                httpRequest.UserAgent = Http.ChromeUserAgent( );
                httpRequest.Referer = "http://finviz.com";
                using( TextFieldParser textFieldParser = new TextFieldParser( new StringReader( httpRequest.Get( "http://finviz.com/export.ashx?o=ticker", null ).ToString() ) ) )
                {
                    textFieldParser.Delimiters = new string[ 1 ] { "," };
                    textFieldParser.TrimWhiteSpace = true;
                    textFieldParser.HasFieldsEnclosedInQuotes = true;
                    textFieldParser.ReadLine( );
                    string[ ] strArray;
                    while( ( strArray = textFieldParser.ReadFields( ) ) != null && !isCancelled( ) )
                    {
                        string str = strArray[ 1 ];
                        string str1 = strArray[ 2 ];
                        if( ( criteria.Code.IsEmpty( ) || str.ContainsIgnoreCase( criteria.Code ) ) && ( criteria.Name.IsEmpty( ) || str1.ContainsIgnoreCase( criteria.Name ) ) )
                        {
                            string id = this.SecurityIdGenerator.GenerateId( str, ExchangeBoard.Nasdaq );
                            if( securityStorage.LookupById( id ) == null )
                            {
                                Security security = new Security( )
                                {
                                    Id = id,
                                    Code = str,
                                    Board = ExchangeBoard.Nasdaq
                                };
                                securityStorage.Save( security, false );
                                newSecurity( security );
                            }
                        }
                    }
                }
            }
        }

        private sealed class Class39
        {
            public string string_0;
            public Dictionary< Security, Level1ChangeMessage > dictionary_0;
            public FinVizHistorySource.Class40 class40_0;

            internal void method_0( )
            {
                List< string[ ] > strArrayList = FinVizHistorySource.smethod_0( this.string_0 );
                for( int index1 = 1; index1 < strArrayList.Count; ++index1 )
                {
                    string[ ] strArray = strArrayList[ index1 ];
                    Security security = this.class40_0.dictionary_0[ strArray[ 0 ] ];
                    Level1ChangeMessage level1ChangeMessage = new Level1ChangeMessage( );
                    level1ChangeMessage.SecurityId = security.ToSecurityId( null );
                    level1ChangeMessage.ServerTime = TimeHelper.NowWithOffset;
                    Level1ChangeMessage message = level1ChangeMessage;
                    for( int index2 = 1; index2 < strArray.Length; ++index2 )
                    {
                        string s = strArray[ index2 ];
                        FinVizColumns key = ( FinVizColumns )( index2 + 1 );
                        Level1Fields? nullable = FinVizHistorySource.dictionary_1.TryGetValue2< FinVizColumns, Level1Fields >( key );
                        if( nullable.HasValue )
                        {
                            string str = s.Remove( "%", false );
                            if( !str.IsEmpty( ) )
                            {
                                message.Add< Level1ChangeMessage, Level1Fields >( nullable.Value, str.To( FinVizHistorySource.dictionary_0[ key ] ) );
                            }
                        }
                    }
                    this.dictionary_0.Add( security, message );
                }
            }
        }

        private sealed class Class40
        {
            public Dictionary< string, Security > dictionary_0;
        }

        [Serializable]
        private sealed class Class41
        {
            public static readonly FinVizHistorySource.Class41 class41_0 = new FinVizHistorySource.Class41( );
            public static Func< Security, Security > func_0;

            internal Security method_0( Security security_0 )
            {
                return security_0;
            }
        }
    }
}
